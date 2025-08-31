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
            AsegurarEsquema();
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

        private void AsegurarEsquema()
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            // Tabla de usuarios
            var sqlUsuarios = @"CREATE TABLE IF NOT EXISTS usuarios (
                id INT AUTO_INCREMENT PRIMARY KEY,
                email VARCHAR(255) NOT NULL UNIQUE,
                password_hash VARBINARY(64) NOT NULL,
                password_salt VARBINARY(16) NOT NULL,
                master_key_blob TEXT NULL,
                creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";
            using (var cmdU = new MySqlCommand(sqlUsuarios, conn))
                cmdU.ExecuteNonQuery();

            // Añadir master_key_blob si no existe (MySQL 8+)
            try
            {
                using var cmdAdd = new MySqlCommand("ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS master_key_blob TEXT NULL;", conn);
                cmdAdd.ExecuteNonQuery();
            }
            catch { }

            // Tabla de entradas
            var sqlEntradas = @"CREATE TABLE IF NOT EXISTS entradas (
                id INT AUTO_INCREMENT PRIMARY KEY,
                servicio VARCHAR(255) NOT NULL,
                usuario VARCHAR(255) NOT NULL,
                secreto_cifrado TEXT NOT NULL,
                creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";
            using (var cmdE = new MySqlCommand(sqlEntradas, conn))
                cmdE.ExecuteNonQuery();

            // Columna login_url si no existe
            try
            {
                using var cmdAddUrl = new MySqlCommand("ALTER TABLE entradas ADD COLUMN IF NOT EXISTS login_url VARCHAR(500) NULL;", conn);
                cmdAddUrl.ExecuteNonQuery();
            }
            catch { }

            // Columna usuario_id si no existe (MySQL 8+)
            var sqlAddUsuarioId = @"ALTER TABLE entradas ADD COLUMN IF NOT EXISTS usuario_id INT NULL;";
            using (var cmdC = new MySqlCommand(sqlAddUsuarioId, conn))
                cmdC.ExecuteNonQuery();

            // FK si no existe (no hay IF NOT EXISTS para FK de forma estándar; intentamos y capturamos duplicado)
            try
            {
                var sqlFk = @"ALTER TABLE entradas ADD CONSTRAINT fk_entradas_usuario
                               FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE;";
                using var cmdFk = new MySqlCommand(sqlFk, conn);
                cmdFk.ExecuteNonQuery();
            }
            catch { /* puede fallar si ya existe */ }
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
