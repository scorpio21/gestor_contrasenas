using System;
using System.Text;
using GestorContrasenas.Seguridad;
using Xunit;

namespace GestorContrasenas.Tests
{
    public class CifradoServiceTests
    {
        private readonly CifradoService _svc = new CifradoService();

        [Fact]
        public void Debe_cifrar_y_descifrar_texto()
        {
            // Arrange
            var plano = "secreto-123";
            var clave = "clave-maestra-segura";

            // Act
            var cifrado = _svc.CifrarTexto(plano, clave);
            var descifrado = _svc.DescifrarTexto(cifrado, clave);

            // Assert
            Assert.Equal(plano, descifrado);
        }

        [Fact]
        public void Cifrar_debe_fallar_con_clave_vacia()
        {
            // Arrange
            var plano = "abc";

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() => _svc.CifrarTexto(plano, " "));
            Assert.Contains("clave maestra", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Descifrar_devuelve_vacio_si_entrada_vacia()
        {
            // Act
            var res1 = _svc.DescifrarTexto(null!, "clave");
            var res2 = _svc.DescifrarTexto("", "clave");
            var res3 = _svc.DescifrarTexto("   ", "clave");

            // Assert
            Assert.Equal(string.Empty, res1);
            Assert.Equal(string.Empty, res2);
            Assert.Equal(string.Empty, res3);
        }

        [Fact]
        public void Descifrar_debe_fallar_con_blob_invalido()
        {
            // Arrange
            var clave = "clave";
            var invalido = Convert.ToBase64String(Encoding.UTF8.GetBytes("xx"));

            // Act + Assert
            Assert.Throws<FormatException>(() => _svc.DescifrarTexto(invalido, clave));
        }

        [Fact]
        public void Descifrar_debe_fallar_con_version_no_soportada()
        {
            // Arrange
            var clave = "clave-maestra-x";
            var cifrado = _svc.CifrarTexto("hola", clave);
            var blob = Convert.FromBase64String(cifrado);
            blob[0] = 9; // forzamos version inválida
            var noSoportado = Convert.ToBase64String(blob);

            // Act + Assert
            Assert.Throws<NotSupportedException>(() => _svc.DescifrarTexto(noSoportado, clave));
        }
    }
}
