namespace GestorContrasenas.Dominio
{
    // Representa un usuario del sistema
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = System.Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = System.Array.Empty<byte>();
        // Clave maestra cifrada como blob (Base64 con salt/nonce/tag) usando CifradoService y la contraseña como clave
        public string? MasterKeyBlob { get; set; }
    }
}
