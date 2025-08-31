using System.Collections.Generic;
using GestorContrasenas.Dominio;

namespace GestorContrasenas.Datos
{
    // Contrato mínimo para operar entradas de contraseñas
    public interface IEntradasRepositorio
    {
        IEnumerable<EntradaContrasena> Listar();
        IEnumerable<EntradaContrasena> ListarPorUsuario(int usuarioId);
        int Agregar(EntradaContrasena e);
        int Agregar(EntradaContrasena e, int usuarioId);
        void Eliminar(int id);
        void Actualizar(EntradaContrasena e);
    }
}
