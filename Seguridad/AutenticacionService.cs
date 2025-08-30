using System;
using System.Security.Cryptography;

namespace GestorContrasenas.Seguridad
{
    // Hash y verificación de contraseñas con PBKDF2-SHA256
    public class AutenticacionService
    {
        private const int Iteraciones = 100_000;
        private const int TamSalt = 16;
        private const int TamHash = 32;

        public (byte[] Hash, byte[] Salt) GenerarHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("La contraseña es obligatoria.");
            var salt = RandomNumberGenerator.GetBytes(TamSalt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iteraciones, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(TamHash);
            return (hash, salt);
        }

        public bool Verificar(string password, byte[] salt, byte[] hashEsperado)
        {
            if (salt == null || hashEsperado == null) return false;
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iteraciones, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(TamHash);
            return CryptographicOperations.FixedTimeEquals(hash, hashEsperado);
        }
    }
}
