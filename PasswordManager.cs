#if false
using System;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class PasswordManager
{
    private readonly string _dbPath = "passwords.db";
    private readonly string _masterKey;

    public PasswordManager(string masterKey)
    {
        _masterKey = masterKey;
        if (!File.Exists(_dbPath))
            CreateDatabase();
    }
#endif

#if false
    private void CreateDatabase()
    {
        SQLiteConnection.CreateFile(_dbPath);
        using (var conn = new SQLiteConnection($"Data Source={_dbPath};"))
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"CREATE TABLE Passwords (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Service TEXT,
                Username TEXT,
                Password TEXT
            )";
            cmd.ExecuteNonQuery();
        }
    }
#endif
 #if false
    public void AddPassword(string service, string username, string password)
    {
        var encrypted = Encrypt(password, _masterKey);
        using (var conn = new SQLiteConnection($"Data Source={_dbPath};"))
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Passwords (Service, Username, Password) VALUES (@s, @u, @p)";
            cmd.Parameters.AddWithValue("@s", service);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", encrypted);
            cmd.ExecuteNonQuery();
        }
    }
#endif
 #if false
    public (string Service, string Username, string Password)[] GetPasswords()
    {
        using (var conn = new SQLiteConnection($"Data Source={_dbPath};"))
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Service, Username, Password FROM Passwords";
            using (var reader = cmd.ExecuteReader())
            {
                var list = new System.Collections.Generic.List<(string, string, string)>();
                while (reader.Read())
                {
                    var service = reader.GetString(0);
                    var username = reader.GetString(1);
                    var password = Decrypt(reader.GetString(2), _masterKey);
                    list.Add((service, username, password));
                }
                return list.ToArray();
            }
        }
    }

    private string Encrypt(string plainText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.GenerateIV();
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private string Decrypt(string cipherText, string key)
    {
        var bytes = Convert.FromBase64String(cipherText);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            var iv = new byte[16];
            Array.Copy(bytes, iv, 16);
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(bytes, 16, bytes.Length - 16))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
#endif