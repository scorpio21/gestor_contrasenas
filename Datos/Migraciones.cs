using System;
using MySql.Data.MySqlClient;

namespace GestorContrasenas.Datos
{
    // Utilidad de migraciones y validación de esquema MySQL
    public static class Migraciones
    {
        // Asegura que el esquema mínimo exista. Idempotente.
        public static void AsegurarEsquema(string cadenaConexion)
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
            using (var cmdC = new MySqlCommand(@"ALTER TABLE entradas ADD COLUMN IF NOT EXISTS usuario_id INT NULL;", conn))
                cmdC.ExecuteNonQuery();

            // FK si no existe
            try
            {
                var sqlFk = @"ALTER TABLE entradas ADD CONSTRAINT fk_entradas_usuario
                               FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE;";
                using var cmdFk = new MySqlCommand(sqlFk, conn);
                cmdFk.ExecuteNonQuery();
            }
            catch { /* puede fallar si ya existe */ }
        }

        // Valida que existan tablas/columnas mínimas. Lanza excepción si falta algo.
        public static void ValidarEsquema(string cadenaConexion)
        {
            using var conn = new MySqlConnection(cadenaConexion);
            conn.Open();

            bool ExisteTabla(string nombre)
            {
                using var cmd = new MySqlCommand("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = @t;", conn);
                cmd.Parameters.AddWithValue("@t", nombre);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }

            bool ExisteColumna(string tabla, string columna)
            {
                using var cmd = new MySqlCommand("SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = @t AND column_name = @c;", conn);
                cmd.Parameters.AddWithValue("@t", tabla);
                cmd.Parameters.AddWithValue("@c", columna);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }

            var errores = string.Empty;
            if (!ExisteTabla("usuarios")) errores += "Falta tabla 'usuarios'. ";
            if (!ExisteTabla("entradas")) errores += "Falta tabla 'entradas'. ";

            if (!ExisteColumna("usuarios", "email")) errores += "Falta columna usuarios.email. ";
            if (!ExisteColumna("usuarios", "password_hash")) errores += "Falta columna usuarios.password_hash. ";
            if (!ExisteColumna("usuarios", "password_salt")) errores += "Falta columna usuarios.password_salt. ";

            if (!ExisteColumna("entradas", "servicio")) errores += "Falta columna entradas.servicio. ";
            if (!ExisteColumna("entradas", "usuario")) errores += "Falta columna entradas.usuario. ";
            if (!ExisteColumna("entradas", "secreto_cifrado")) errores += "Falta columna entradas.secreto_cifrado. ";

            if (!string.IsNullOrWhiteSpace(errores))
                throw new InvalidOperationException("Esquema incompleto: " + errores.Trim());
        }
    }
}
