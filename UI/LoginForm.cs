using System;
using System.Windows.Forms;
using GestorContrasenas.Servicios;

namespace GestorContrasenas.UI
{
    public partial class LoginForm : Form
    {
        public string ClaveMaestra { get; private set; } = string.Empty;
        public int UsuarioId { get; private set; } = -1;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnAcceder_Click(object? sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var pwd = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Introduce tu email.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(pwd) || pwd.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // La clave maestra se recupera automáticamente desde BD si existe

            try
            {
                var auth = new AuthService();
                var resultado = auth.LoginYObtenerClave(email, pwd);
                if (resultado.UsuarioId <= 0)
                {
                    MessageBox.Show("Credenciales incorrectas.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UsuarioId = resultado.UsuarioId;
                // Si la clave maestra fue recuperada de BD úsala; si no, pedirla una sola vez en un formulario dedicado
                if (string.IsNullOrEmpty(resultado.ClaveMaestra))
                {
                    using var cfg = new ConfigurarClaveForm();
                    if (cfg.ShowDialog(this) != DialogResult.OK)
                    {
                        // Usuario canceló
                        return;
                    }
                    ClaveMaestra = cfg.ClaveMaestra;
                    try
                    {
                        auth.ActualizarClaveMaestra(UsuarioId, pwd, ClaveMaestra);
                        MessageBox.Show("Clave maestra guardada para próximas sesiones.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"No se pudo guardar la clave maestra: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ClaveMaestra = resultado.ClaveMaestra;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarse_Click(object? sender, EventArgs e)
        {
            using var reg = new RegistroForm();
            if (reg.ShowDialog(this) == DialogResult.OK)
            {
                // Prefill email tras registro
                if (!string.IsNullOrWhiteSpace(reg.EmailRegistrado))
                    txtEmail.Text = reg.EmailRegistrado;
            }
        }
    }
}
