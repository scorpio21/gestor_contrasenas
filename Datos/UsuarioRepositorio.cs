using System;
using MySql.Data.MySqlClient;
using GestorContrasenas.Dominio;

namespace GestorContrasenas.Datos
{
    // Acceso a datos de usuarios
    public class UsuarioRepositorio
    {
        private readonly string cadenaConexion;

        public UsuarioRepositorio(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion ?? throw new ArgumentNullException(nameof(cadenaConexion));
        }

        public Usuario? BuscarPorEmail(string email)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            using var cmd = new MySqlCommand("SELECT id, email, password_hash, password_salt, master_key_blob FROM usuarios WHERE email=@e", conn);
            cmd.Parameters.AddWithValue("@e", email);
            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                return new Usuario
                {
                    Id = rd.GetInt32(0),
                    Email = rd.GetString(1),
                    PasswordHash = (byte[])rd[2],
                    PasswordSalt = (byte[])rd[3],
                    MasterKeyBlob = rd.IsDBNull(4) ? null : rd.GetString(4)
                };
            }
            return null;
        }

        public int CrearUsuario(Usuario u)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            using var cmd = new MySqlCommand("INSERT INTO usuarios (email, password_hash, password_salt, master_key_blob) VALUES (@e, @h, @s, @m); SELECT LAST_INSERT_ID();", conn);
            cmd.Parameters.AddWithValue("@e", u.Email);
            cmd.Parameters.AddWithValue("@h", u.PasswordHash);
            cmd.Parameters.AddWithValue("@s", u.PasswordSalt);
            cmd.Parameters.AddWithValue("@m", (object?)u.MasterKeyBlob ?? DBNull.Value);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }

        public void ActualizarMasterKeyBlob(int usuarioId, string? blob)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            using var cmd = new MySqlCommand("UPDATE usuarios SET master_key_blob=@m WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@m", (object?)blob ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", usuarioId);
            cmd.ExecuteNonQuery();
        }

        public Usuario? BuscarPorId(int id)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            using var cmd = new MySqlCommand("SELECT id, email, password_hash, password_salt, master_key_blob FROM usuarios WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                return new Usuario
                {
                    Id = rd.GetInt32(0),
                    Email = rd.GetString(1),
                    PasswordHash = (byte[])rd[2],
                    PasswordSalt = (byte[])rd[3],
                    MasterKeyBlob = rd.IsDBNull(4) ? null : rd.GetString(4)
                };
            }
            return null;
        }
    }
}
