using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GestorContrasenas.Datos;
using GestorContrasenas.Dominio;
using GestorContrasenas.Seguridad;

namespace GestorContrasenas.Servicios
{
    // Orquesta cifrado y acceso a datos
    public class GestorContrasenasService
    {
        private readonly IEntradasRepositorio repo;
        private readonly CifradoService cifrado;
        private readonly int usuarioId;
        private static readonly HttpClient http = new HttpClient();
        private static readonly Dictionary<string, (bool comprometida, int? conteo)> cachePwned = new(StringComparer.OrdinalIgnoreCase);
        private static DateTime lastHibpCallUtc = DateTime.MinValue;
        public static int HibpIntervaloMinimoMs { get; set; } = 200; // rate limit simple

        public GestorContrasenasService(int usuarioId)
        {
            repo = new MySqlRepositorio();
            cifrado = new CifradoService();
            this.usuarioId = usuarioId;
        }

        // Constructor para pruebas/inyección
        public GestorContrasenasService(int usuarioId, IEntradasRepositorio repositorio, CifradoService? cifradoService = null)
        {
            repo = repositorio;
            cifrado = cifradoService ?? new CifradoService();
            this.usuarioId = usuarioId;
        }

        public IEnumerable<EntradaContrasena> Listar()
        {
            if (usuarioId > 0) return repo.ListarPorUsuario(usuarioId);
            return repo.Listar();
        }

        public IEnumerable<(int Id, string Servicio, string Usuario, string Secreto)> ListarDescifrado(string claveMaestra)
        {
            var fuente = usuarioId > 0 ? repo.ListarPorUsuario(usuarioId) : repo.Listar();
            foreach (var e in fuente)
            {
                var secreto = string.Empty;
                try { secreto = cifrado.DescifrarTexto(e.SecretoCifrado, claveMaestra); }
                catch { secreto = ""; }
                yield return (e.Id, e.Servicio, e.Usuario, secreto);
            }
        }

        public int Agregar(string servicio, string usuario, string secretoPlano, string claveMaestra, string? loginUrl = null)
        {
            var cif = cifrado.CifrarTexto(secretoPlano, claveMaestra);
            var e = new EntradaContrasena { Servicio = servicio, Usuario = usuario, SecretoCifrado = cif, LoginUrl = string.IsNullOrWhiteSpace(loginUrl) ? null : loginUrl };
            if (usuarioId > 0) return repo.Agregar(e, usuarioId);
            return repo.Agregar(e);
        }

        public void Eliminar(int id)
        {
            repo.Eliminar(id);
        }

        public void Actualizar(int id, string servicio, string usuario, string secretoPlano, string claveMaestra, string? loginUrl = null)
        {
            var cif = cifrado.CifrarTexto(secretoPlano, claveMaestra);
            // Preservar UsuarioId de la entrada existente para no romper ListarPorUsuario()
            var origen = usuarioId > 0 ? repo.ListarPorUsuario(usuarioId) : repo.Listar();
            var existente = origen.FirstOrDefault(e => e.Id == id);
            int? usuarioIdExistente = existente?.UsuarioId;

            repo.Actualizar(new EntradaContrasena
            {
                Id = id,
                Servicio = servicio,
                Usuario = usuario,
                SecretoCifrado = cif,
                LoginUrl = string.IsNullOrWhiteSpace(loginUrl) ? null : loginUrl,
                UsuarioId = usuarioIdExistente
            });
        }

        public string ObtenerSecretoDescifrado(EntradaContrasena e, string claveMaestra)
        {
            return cifrado.DescifrarTexto(e.SecretoCifrado, claveMaestra);
        }

        // Comprueba si una contraseña está comprometida usando HIBP (k-anonimato).
        // Retorna (true, conteo) si aparece en filtraciones, de lo contrario (false, 0).
        public async Task<(bool comprometida, int? conteo)> EstaComprometidaAsync(string contrasenaPlano)
        {
            if (string.IsNullOrEmpty(contrasenaPlano)) return (false, 0);

            // SHA-1 en mayúsculas
            string hash;
            using (var sha1 = SHA1.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(contrasenaPlano);
                var h = sha1.ComputeHash(bytes);
                hash = BitConverter.ToString(h).Replace("-", string.Empty).ToUpperInvariant();
            }

            if (cachePwned.TryGetValue(hash, out var cached))
            {
                return cached;
            }

            var prefix = hash.Substring(0, 5);
            var suffix = hash.Substring(5);

            // Rate limit simple entre llamadas
            var ahora = DateTime.UtcNow;
            var elapsed = ahora - lastHibpCallUtc;
            if (elapsed.TotalMilliseconds < HibpIntervaloMinimoMs)
            {
                await Task.Delay(HibpIntervaloMinimoMs - (int)elapsed.TotalMilliseconds);
            }

            using var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.pwnedpasswords.com/range/{prefix}");
            req.Headers.TryAddWithoutValidation("User-Agent", "gestor_contrasenas/1.0 (+https://github.com/scorpio21/gestor_contrasenas)");
            using var resp = await http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            var body = await resp.Content.ReadAsStringAsync();
            lastHibpCallUtc = DateTime.UtcNow;

            int? count = null;
            bool found = false;
            using (var sr = new System.IO.StringReader(body))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Formato: SUFFIX:COUNT
                    var idx = line.IndexOf(':');
                    if (idx <= 0) continue;
                    var suf = line.Substring(0, idx).Trim();
                    if (suf.Equals(suffix, System.StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        if (int.TryParse(line.Substring(idx + 1).Trim(), out var c)) count = c;
                        break;
                    }
                }
            }

            var result = (found, count ?? (found ? 1 : 0));
            cachePwned[hash] = result;
            return result;
        }

        // Calcula fortaleza simple: 0 (muy débil) a 4 (muy fuerte)
        public (int puntuacion, string descripcion) CalcularFortaleza(string contrasena)
        {
            if (string.IsNullOrEmpty(contrasena)) return (0, "Vacía");
            int puntos = 0;
            if (contrasena.Length >= 8) puntos++;
            if (contrasena.Length >= 12) puntos++;
            if (contrasena.Any(char.IsLower) && contrasena.Any(char.IsUpper)) puntos++;
            if (contrasena.Any(char.IsDigit) && contrasena.Any(c => !char.IsLetterOrDigit(c))) puntos++;

            string desc = puntos switch
            {
                0 => "Muy débil",
                1 => "Débil",
                2 => "Media",
                3 => "Fuerte",
                _ => "Muy fuerte"
            };
            // Normalizar a 0..4
            if (puntos > 4) puntos = 4;
            return (puntos, desc);
        }

        // Verifica si la contraseña ya existe en otra entrada (reutilización)
        public bool ExisteReutilizacionSecreto(string contrasenaPlano, string claveMaestra)
        {
            if (string.IsNullOrEmpty(contrasenaPlano)) return false;
            foreach (var e in Listar())
            {
                string plano;
                try { plano = cifrado.DescifrarTexto(e.SecretoCifrado, claveMaestra); }
                catch { continue; }
                if (!string.IsNullOrEmpty(plano) && string.Equals(plano, contrasenaPlano))
                {
                    return true;
                }
            }
            return false;
        }

        // Exporta todas las entradas del usuario (o todas) a un JSON cifrado con la clave maestra
        public string ExportarJsonCifrado(string claveMaestra)
        {
            var lista = new List<object>();
            foreach (var e in Listar())
            {
                string secretoPlano = string.Empty;
                try { secretoPlano = cifrado.DescifrarTexto(e.SecretoCifrado, claveMaestra); } catch { secretoPlano = string.Empty; }
                lista.Add(new
                {
                    e.Servicio,
                    e.Usuario,
                    Secreto = secretoPlano,
                    e.LoginUrl
                });
            }
            var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = false });
            return cifrado.CifrarTexto(json, claveMaestra);
        }

        // Importa un JSON cifrado (mismo formato) y agrega entradas
        public int ImportarJsonCifrado(string claveMaestra, string jsonCifradoBase64)
        {
            var jsonPlano = cifrado.DescifrarTexto(jsonCifradoBase64, claveMaestra);
            var datos = JsonSerializer.Deserialize<List<ModeloImport>>(jsonPlano) ?? new List<ModeloImport>();
            int agregadas = 0;
            foreach (var d in datos)
            {
                if (string.IsNullOrWhiteSpace(d.Servicio) || string.IsNullOrWhiteSpace(d.Usuario) || string.IsNullOrEmpty(d.Secreto))
                    continue;
                Agregar(d.Servicio, d.Usuario, d.Secreto, claveMaestra, string.IsNullOrWhiteSpace(d.LoginUrl) ? null : d.LoginUrl);
                agregadas++;
            }
            return agregadas;
        }

        private class ModeloImport
        {
            public string Servicio { get; set; } = string.Empty;
            public string Usuario { get; set; } = string.Empty;
            public string Secreto { get; set; } = string.Empty; // plano dentro del JSON (pero el JSON completo está cifrado)
            public string? LoginUrl { get; set; }
        }
    }
}
