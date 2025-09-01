using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorContrasenas.Dominio;

namespace GestorContrasenas.Datos
{
    // Acceso a datos MySQL. Usa la cadena de conexión desde la variable de entorno GESTOR_DB_CONN
    public class MySqlRepositorio : IEntradasRepositorio
    {
        private readonly string cadenaConexion;

        public MySqlRepositorio()
        {
            cadenaConexion = Environment.GetEnvironmentVariable("GESTOR_DB_CONN")
                ?? throw new InvalidOperationException("Falta la variable de entorno GESTOR_DB_CONN con la cadena de conexión a MySQL.");
            Migraciones.AsegurarEsquema(cadenaConexion);
        }

        // Listar filtrado por usuario_id
        public IEnumerable<EntradaContrasena> ListarPorUsuario(int usuarioId)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            using var cmd = new MySqlCommand("SELECT id, servicio, usuario, secreto_cifrado, login_url FROM entradas WHERE usuario_id=@uid ORDER BY id DESC", conn);
            cmd.Parameters.AddWithValue("@uid", usuarioId);
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                yield return new EntradaContrasena
                {
                    Id = rd.GetInt32(0),
                    Servicio = rd.GetString(1),
                    Usuario = rd.GetString(2),
                    SecretoCifrado = rd.GetString(3),
                    LoginUrl = rd.IsDBNull(4) ? null : rd.GetString(4)
                };
            }
        }


        public IEnumerable<EntradaContrasena> Listar()
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            using var cmd = new MySqlCommand("SELECT id, servicio, usuario, secreto_cifrado, login_url FROM entradas ORDER BY id DESC", conn);
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                yield return new EntradaContrasena
                {
                    Id = rd.GetInt32(0),
                    Servicio = rd.GetString(1),
                    Usuario = rd.GetString(2),
                    SecretoCifrado = rd.GetString(3),
                    LoginUrl = rd.IsDBNull(4) ? null : rd.GetString(4)
                };
            }
        }

        public int Agregar(EntradaContrasena e)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            var sql = "INSERT INTO entradas (servicio, usuario, secreto_cifrado, login_url) VALUES (@s, @u, @c, @lu); SELECT LAST_INSERT_ID();";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@s", e.Servicio);
            cmd.Parameters.AddWithValue("@u", e.Usuario);
            cmd.Parameters.AddWithValue("@c", e.SecretoCifrado);
            cmd.Parameters.AddWithValue("@lu", (object?)e.LoginUrl ?? DBNull.Value);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }

        // Agregar asignando usuario_id
        public int Agregar(EntradaContrasena e, int usuarioId)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            var sql = "INSERT INTO entradas (servicio, usuario, secreto_cifrado, login_url, usuario_id) VALUES (@s, @u, @c, @lu, @uid); SELECT LAST_INSERT_ID();";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@s", e.Servicio);
            cmd.Parameters.AddWithValue("@u", e.Usuario);
            cmd.Parameters.AddWithValue("@c", e.SecretoCifrado);
            cmd.Parameters.AddWithValue("@lu", (object?)e.LoginUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@uid", usuarioId);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }

        public void Eliminar(int id)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            using var cmd = new MySqlCommand("DELETE FROM entradas WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(EntradaContrasena e)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            var sql = "UPDATE entradas SET servicio=@s, usuario=@u, secreto_cifrado=@c, login_url=@lu WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@s", e.Servicio);
            cmd.Parameters.AddWithValue("@u", e.Usuario);
            cmd.Parameters.AddWithValue("@c", e.SecretoCifrado);
            cmd.Parameters.AddWithValue("@lu", (object?)e.LoginUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", e.Id);
            cmd.ExecuteNonQuery();
        }
    }
}
