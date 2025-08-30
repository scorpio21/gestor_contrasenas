using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GestorContrasenas.Servicios;

namespace GestorContrasenas.UI
{
    public partial class RegistroForm : Form
    {
        public string EmailRegistrado { get; private set; } = string.Empty;

        public RegistroForm()
        {
            InitializeComponent();
        }

        private bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                // Validación simple
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            catch { return false; }
        }

        private void btnCrear_Click(object? sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var pwd = txtPassword.Text;
            var conf = txtConfirmar.Text;
            var cm = txtClaveMaestra.Text;
            var cmc = txtClaveMaestraConf.Text;

            if (!EmailValido(email))
            {
                MessageBox.Show("Introduce un email válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(pwd) || pwd.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (pwd != conf)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(cm) || cm.Length < 8)
            {
                MessageBox.Show("La clave maestra debe tener al menos 8 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cm != cmc)
            {
                MessageBox.Show("Las claves maestras no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var auth = new AuthService();
                var id = auth.Registrar(email, pwd, cm);
                if (id > 0)
                {
                    EmailRegistrado = email;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo registrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
