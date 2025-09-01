using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GestorContrasenas.Seguridad
{
    // Utilidad para recordar sesión del usuario actual (cifrado con DPAPI por usuario)
    public static class RecordarSesion
    {
        private static string RutaArchivo()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gestor_contrasenas");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "session.dat");
        }

        public static void Guardar(int usuarioId, string claveMaestra)
        {
            if (usuarioId <= 0) throw new ArgumentException("usuarioId inválido");
            if (string.IsNullOrEmpty(claveMaestra)) throw new ArgumentException("claveMaestra requerida");
            var texto = $"{usuarioId}|{claveMaestra}";
            var datos = Encoding.UTF8.GetBytes(texto);
            var protegido = ProtectedData.Protect(datos, optionalEntropy: null, scope: DataProtectionScope.CurrentUser);
            File.WriteAllBytes(RutaArchivo(), protegido);
        }

        public static (int UsuarioId, string ClaveMaestra)? Cargar()
        {
            var ruta = RutaArchivo();
            if (!File.Exists(ruta)) return null;
            try
            {
                var protegido = File.ReadAllBytes(ruta);
                var datos = ProtectedData.Unprotect(protegido, optionalEntropy: null, scope: DataProtectionScope.CurrentUser);
                var texto = Encoding.UTF8.GetString(datos);
                var partes = texto.Split('|');
                if (partes.Length != 2) return null;
                if (!int.TryParse(partes[0], out var uid)) return null;
                var clave = partes[1];
                return (uid, clave);
            }
            catch
            {
                return null;
            }
        }
        
        public static void Limpiar()
        {
            var ruta = RutaArchivo();
            try { if (File.Exists(ruta)) File.Delete(ruta); } catch { }
        }
    }
}
