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
            var clave = txtClave.Text;

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
            // La clave maestra ya no es obligatoria si está almacenada cifrada en BD

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
                // Si la clave maestra fue recuperada de BD úsala; de lo contrario, requiere que el usuario la introduzca
                if (!string.IsNullOrEmpty(resultado.ClaveMaestra))
                {
                    ClaveMaestra = resultado.ClaveMaestra;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(clave) && clave.Length >= 8)
                    {
                        ClaveMaestra = clave;
                        // Guardar la clave maestra cifrada para futuras sesiones
                        try
                        {
                            auth.ActualizarClaveMaestra(UsuarioId, pwd, ClaveMaestra);
                            MessageBox.Show("Clave maestra guardada para próximas sesiones.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"No se pudo guardar la clave maestra: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Tu cuenta no tiene clave maestra guardada. Introduce tu clave maestra para continuar.", "Clave maestra requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClave.Focus();
                        return;
                    }
                }
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
