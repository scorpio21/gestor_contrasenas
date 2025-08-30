using System;
using System.Security.Cryptography;
using System.Text;

namespace GestorContrasenas.Seguridad
{
    // Servicio de cifrado con PBKDF2 + AES-GCM
    public class CifradoService
    {
        private const int TamSalt = 16; // 128 bits
        private const int TamNonce = 12; // 96 bits recomendado para GCM
        private const int Iteraciones = 100_000;
        private const int TamClave = 32; // 256 bits
        private const int TamTag = 16; // 128 bits

        // Formato de salida (Base64): [1 byte version=1][16 salt][12 nonce][n cipher][16 tag]
        public string CifrarTexto(string textoPlano, string claveMaestra)
        {
            if (textoPlano == null) throw new ArgumentNullException(nameof(textoPlano));
            if (string.IsNullOrWhiteSpace(claveMaestra)) throw new ArgumentException("La clave maestra es obligatoria.");

            var salt = RandomNumberGenerator.GetBytes(TamSalt);
            var nonce = RandomNumberGenerator.GetBytes(TamNonce);
            var clave = DerivarClave(claveMaestra, salt);

            var datos = Encoding.UTF8.GetBytes(textoPlano);
            var cifrado = new byte[datos.Length];
            var tag = new byte[TamTag];

            using (var aesgcm = new AesGcm(clave, TamTag))
            {
                aesgcm.Encrypt(nonce, datos, cifrado, tag, associatedData: null);
            }

            var salida = new byte[1 + TamSalt + TamNonce + cifrado.Length + TamTag];
            salida[0] = 1; // versión
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
            if (blob[0] != 1) throw new NotSupportedException("Versión de cifrado no soportada.");

            var salt = new byte[TamSalt];
            var nonce = new byte[TamNonce];
            Buffer.BlockCopy(blob, 1, salt, 0, TamSalt);
            Buffer.BlockCopy(blob, 1 + TamSalt, nonce, 0, TamNonce);

            var totalCifrado = blob.Length - (1 + TamSalt + TamNonce + TamTag);
            var cifrado = new byte[totalCifrado];
            var tag = new byte[TamTag];
            Buffer.BlockCopy(blob, 1 + TamSalt + TamNonce, cifrado, 0, totalCifrado);
            Buffer.BlockCopy(blob, 1 + TamSalt + TamNonce + totalCifrado, tag, 0, TamTag);

            var clave = DerivarClave(claveMaestra, salt);
            var plano = new byte[totalCifrado];
            using (var aesgcm = new AesGcm(clave, TamTag))
            {
                aesgcm.Decrypt(nonce, cifrado, tag, plano, associatedData: null);
            }
            return Encoding.UTF8.GetString(plano);
        }

        private static byte[] DerivarClave(string claveMaestra, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(claveMaestra, salt, Iteraciones, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(TamClave);
        }
    }
}
