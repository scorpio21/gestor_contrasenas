using System;
using GestorContrasenas.Datos;
using GestorContrasenas.Dominio;
using GestorContrasenas.Seguridad;

namespace GestorContrasenas.Servicios
{
    // Servicio de autenticación y registro de usuarios
    public class AuthService
    {
        private readonly string cadenaConexion;
        private readonly AutenticacionService auth;
        private readonly UsuarioRepositorio repo;
        private readonly CifradoService cifrado;

        public AuthService()
        {
            cadenaConexion = Environment.GetEnvironmentVariable("GESTOR_DB_CONN")
                ?? throw new InvalidOperationException("Falta la variable de entorno GESTOR_DB_CONN con la cadena de conexión a MySQL.");
            auth = new AutenticacionService();
            repo = new UsuarioRepositorio(cadenaConexion);
            cifrado = new CifradoService();
        }

        // Registrar guardando la clave maestra cifrada con la contraseña del usuario
        public int Registrar(string email, string password, string claveMaestra)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email requerido");
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8) throw new ArgumentException("La contraseña debe tener al menos 8 caracteres");
            if (string.IsNullOrWhiteSpace(claveMaestra) || claveMaestra.Length < 8) throw new ArgumentException("La clave maestra debe tener al menos 8 caracteres");
            var existente = repo.BuscarPorEmail(email);
            if (existente != null) throw new InvalidOperationException("El email ya está registrado");
            var (hash, salt) = auth.GenerarHash(password);
            // Ciframos la clave maestra usando la contraseña como clave (CifradoService aplica PBKDF2 + salt por blob)
            var blob = cifrado.CifrarTexto(claveMaestra, password);
            var usuario = new Usuario { Email = email, PasswordHash = hash, PasswordSalt = salt, MasterKeyBlob = blob };
            return repo.CrearUsuario(usuario);
        }

        // Login y obtención de la clave maestra descifrada desde BD
        public (int UsuarioId, string ClaveMaestra) LoginYObtenerClave(string email, string password)
        {
            var u = repo.BuscarPorEmail(email);
            if (u == null) return (-1, string.Empty);
            var ok = auth.Verificar(password, u.PasswordSalt, u.PasswordHash);
            if (!ok) return (-1, string.Empty);
            if (string.IsNullOrEmpty(u.MasterKeyBlob)) return (u.Id, string.Empty);
            var clave = string.Empty;
            try { clave = cifrado.DescifrarTexto(u.MasterKeyBlob!, password); }
            catch { clave = string.Empty; }
            return (u.Id, clave);
        }

        // Permite actualizar/rotar la clave maestra (re-cifra el blob con la contraseña actual)
        public void ActualizarClaveMaestra(int usuarioId, string password, string nuevaClaveMaestra)
        {
            if (string.IsNullOrWhiteSpace(nuevaClaveMaestra) || nuevaClaveMaestra.Length < 8)
                throw new ArgumentException("La clave maestra debe tener al menos 8 caracteres");
            var blob = cifrado.CifrarTexto(nuevaClaveMaestra, password);
            repo.ActualizarMasterKeyBlob(usuarioId, blob);
        }
    }
}
