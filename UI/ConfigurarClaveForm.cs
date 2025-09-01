using System;
using System.Windows.Forms;

namespace GestorContrasenas.UI
{
    public partial class ConfigurarClaveForm : Form
    {
        public string ClaveMaestra { get; private set; } = string.Empty;

        public ConfigurarClaveForm()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object? sender, EventArgs e)
        {
            var c1 = txtClave.Text;
            var c2 = txtConfirmar.Text;
            if (string.IsNullOrWhiteSpace(c1) || c1.Length < 8)
            {
                MessageBox.Show("La clave maestra debe tener al menos 8 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Focus();
                return;
            }
            if (c1 != c2)
            {
                MessageBox.Show("Las claves no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Focus();
                return;
            }
            ClaveMaestra = c1;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
