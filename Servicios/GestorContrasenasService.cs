using System.Collections.Generic;
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
            repo.Actualizar(new EntradaContrasena { Id = id, Servicio = servicio, Usuario = usuario, SecretoCifrado = cif, LoginUrl = string.IsNullOrWhiteSpace(loginUrl) ? null : loginUrl });
        }

        public string ObtenerSecretoDescifrado(EntradaContrasena e, string claveMaestra)
        {
            return cifrado.DescifrarTexto(e.SecretoCifrado, claveMaestra);
        }
    }
}
