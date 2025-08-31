using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
