using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace GestorContrasenas.Seguridad
{
    // Servicio de cifrado con AES-GCM y derivación de clave por versión:
    // v1: PBKDF2-SHA256
    // v2: Argon2id (recomendada)
    public class CifradoService
    {
        private const int TamSalt = 16; // 128 bits
        private const int TamNonce = 12; // 96 bits recomendado para GCM
        private const int Iteraciones = 100_000; // PBKDF2 (v1)
        private const int TamClave = 32; // 256 bits
        private const int TamTag = 16; // 128 bits
        // Parámetros Argon2id (v2) — valores seguros por defecto para desktop
        private const int ArgonMemKB = 64 * 1024; // 64 MB
        private const int ArgonIter = 3;
        private const int ArgonParalelo = 2;

        // Formato de salida (Base64): [1 byte version][16 salt][12 nonce][n cipher][16 tag]
        public string CifrarTexto(string textoPlano, string claveMaestra)
        {
            if (textoPlano == null) throw new ArgumentNullException(nameof(textoPlano));
            if (string.IsNullOrWhiteSpace(claveMaestra)) throw new ArgumentException("La clave maestra es obligatoria.");

            var salt = RandomNumberGenerator.GetBytes(TamSalt);
            var nonce = RandomNumberGenerator.GetBytes(TamNonce);
            // A partir de ahora usamos versión 2 (Argon2id) por defecto
            var clave = DerivarClaveV2Argon2id(claveMaestra, salt);

            var datos = Encoding.UTF8.GetBytes(textoPlano);
            var cifrado = new byte[datos.Length];
            var tag = new byte[TamTag];

            using (var aesgcm = new AesGcm(clave, TamTag))
            {
                aesgcm.Encrypt(nonce, datos, cifrado, tag, associatedData: null);
            }

            var salida = new byte[1 + TamSalt + TamNonce + cifrado.Length + TamTag];
            salida[0] = 2; // versión 2 (Argon2id)
            Buffer.BlockCopy(salt, 0, salida, 1, TamSalt);
            Buffer.BlockCopy(nonce, 0, salida, 1 + TamSalt, TamNonce);
            Buffer.BlockCopy(cifrado, 0, salida, 1 + TamSalt + TamNonce, cifrado.Length);
            Buffer.BlockCopy(tag, 0, salida, 1 + TamSalt + TamNonce + cifrado.Length, TamTag);
            return Convert.ToBase64String(salida);
        }

        public string DescifrarTexto(string textoCifradoBase64, string claveMaestra)
        {
            if (string.IsNullOrWhiteSpace(textoCifradoBase64)) return string.Empty;
            if (string.IsNullOrWhiteSpace(claveMaestra)) throw new ArgumentException("La clave maestra es obligatoria.");

            var blob = Convert.FromBase64String(textoCifradoBase64);
            if (blob.Length < 1 + TamSalt + TamNonce + TamTag) throw new FormatException("Formato de secreto no válido.");
            var version = blob[0];
            if (version != 1 && version != 2) throw new NotSupportedException("Versión de cifrado no soportada.");

            var salt = new byte[TamSalt];
            var nonce = new byte[TamNonce];
            Buffer.BlockCopy(blob, 1, salt, 0, TamSalt);
            Buffer.BlockCopy(blob, 1 + TamSalt, nonce, 0, TamNonce);

            var totalCifrado = blob.Length - (1 + TamSalt + TamNonce + TamTag);
            var cifrado = new byte[totalCifrado];
            var tag = new byte[TamTag];
            Buffer.BlockCopy(blob, 1 + TamSalt + TamNonce, cifrado, 0, totalCifrado);
            Buffer.BlockCopy(blob, 1 + TamSalt + TamNonce + totalCifrado, tag, 0, TamTag);

            byte[] clave = version == 1
                ? DerivarClaveV1PBKDF2(claveMaestra, salt)
                : DerivarClaveV2Argon2id(claveMaestra, salt);
            var plano = new byte[totalCifrado];
            using (var aesgcm = new AesGcm(clave, TamTag))
            {
                aesgcm.Decrypt(nonce, cifrado, tag, plano, associatedData: null);
            }
            return Encoding.UTF8.GetString(plano);
        }

        // v1: PBKDF2-SHA256
        private static byte[] DerivarClaveV1PBKDF2(string claveMaestra, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(claveMaestra, salt, Iteraciones, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(TamClave);
        }

        // v2: Argon2id
        private static byte[] DerivarClaveV2Argon2id(string claveMaestra, byte[] salt)
        {
            var pwdBytes = Encoding.UTF8.GetBytes(claveMaestra);
            using var argon = new Argon2id(pwdBytes)
            {
                Salt = salt,
                DegreeOfParallelism = ArgonParalelo,
                MemorySize = ArgonMemKB,
                Iterations = ArgonIter
            };
            return argon.GetBytes(TamClave);
        }
    }
}
