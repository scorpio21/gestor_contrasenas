namespace GestorContrasenas.Dominio
{
    // Representa una entrada de contraseña en el sistema
    public class EntradaContrasena
    {
        public int Id { get; set; }
        public string Servicio { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        // Almacenado cifrado en BD
        public string SecretoCifrado { get; set; } = string.Empty;
        // URL de acceso/login del servicio
        public string? LoginUrl { get; set; }
        // FK opcional (para migraciones suaves)
        public int? UsuarioId { get; set; }
    }
}
