using System;
using System.Collections.Generic;
using System.Linq;
using GestorContrasenas.Datos;
using GestorContrasenas.Dominio;
using GestorContrasenas.Seguridad;
using GestorContrasenas.Servicios;
using Xunit;

namespace GestorContrasenas.Tests
{
    // Repositorio en memoria para pruebas
    internal class RepoMemoria : IEntradasRepositorio
    {
        private readonly List<EntradaContrasena> _items = new();
        private int _seq = 1;

        public int Agregar(EntradaContrasena e)
        {
            e.Id = _seq++;
            _items.Add(Clone(e));
            return e.Id;
        }

        public int Agregar(EntradaContrasena e, int usuarioId)
        {
            e.UsuarioId = usuarioId;
            return Agregar(e);
        }

        public void Actualizar(EntradaContrasena e)
        {
            var ix = _items.FindIndex(x => x.Id == e.Id);
            if (ix >= 0) _items[ix] = Clone(e);
        }

        public void Eliminar(int id)
        {
            _items.RemoveAll(x => x.Id == id);
        }

        public IEnumerable<EntradaContrasena> Listar()
        {
            // devolver copias para evitar mutaciones desde fuera
            return _items.Select(Clone).ToList();
        }

        public IEnumerable<EntradaContrasena> ListarPorUsuario(int usuarioId)
        {
            return _items.Where(x => x.UsuarioId == usuarioId).Select(Clone).ToList();
        }

        private static EntradaContrasena Clone(EntradaContrasena e) => new EntradaContrasena
        {
            Id = e.Id,
            Servicio = e.Servicio,
            Usuario = e.Usuario,
            SecretoCifrado = e.SecretoCifrado,
            LoginUrl = e.LoginUrl,
            UsuarioId = e.UsuarioId
        };
    }

    public class GestorContrasenasServiceTests
    {
        private readonly string clave = "clave-prueba-123";

        [Fact]
        public void Agregar_y_listar_descifrado_filtrado_por_usuario()
        {
            // Arrange
            var repo = new RepoMemoria();
            var svc = new GestorContrasenasService(usuarioId: 7, repo, new CifradoService());

            // Act
            var id1 = svc.Agregar("gmail", "user@gmail.com", "pass1", clave, "https://mail.google.com");
            var id2 = svc.Agregar("github", "scorpio", "token2", clave, null);

            // Assert
            Assert.True(id1 > 0 && id2 > 0);

            var descifrados = svc.ListarDescifrado(clave).ToList();
            Assert.Equal(2, descifrados.Count);
            Assert.Contains(descifrados, x => x.Servicio == "gmail" && x.Secreto == "pass1");
            Assert.Contains(descifrados, x => x.Servicio == "github" && x.Secreto == "token2");

            // Verifica que el repositorio guardó cifrado
            var crudos = repo.ListarPorUsuario(7).ToList();
            Assert.All(crudos, x => Assert.NotEqual(string.Empty, x.SecretoCifrado));
            Assert.All(crudos, x => Assert.NotEqual("pass1", x.SecretoCifrado));
        }

        [Fact]
        public void Actualizar_y_eliminar()
        {
            // Arrange
            var repo = new RepoMemoria();
            var svc = new GestorContrasenasService(usuarioId: 1, repo, new CifradoService());
            var id = svc.Agregar("twitter", "user", "p1", clave);

            // Act: actualizar
            svc.Actualizar(id, "twitter", "user2", "p2", clave, "https://x.com");

            // Assert actualización
            var lista = repo.ListarPorUsuario(1).ToList();
            var e = lista.Single(x => x.Id == id);
            Assert.Equal("user2", e.Usuario);
            Assert.Equal("https://x.com", e.LoginUrl);
            // debe estar cifrado
            Assert.NotEqual("p2", e.SecretoCifrado);

            // Act: eliminar
            svc.Eliminar(id);
            Assert.DoesNotContain(repo.ListarPorUsuario(1), x => x.Id == id);
        }

        [Fact]
        public void Listar_sin_usuario_muestra_todas()
        {
            // Arrange
            var repo = new RepoMemoria();
            var cif = new CifradoService();
            var svcGlobal = new GestorContrasenasService(usuarioId: 0, repo, cif);
            var svcU1 = new GestorContrasenasService(usuarioId: 10, repo, cif);
            var svcU2 = new GestorContrasenasService(usuarioId: 20, repo, cif);

            // Datos
            svcU1.Agregar("serv1", "u1", "a", clave);
            svcU2.Agregar("serv2", "u2", "b", clave);

            // Act
            var todas = svcGlobal.Listar().ToList();

            // Assert
            Assert.Equal(2, todas.Count);
        }
    }
}
