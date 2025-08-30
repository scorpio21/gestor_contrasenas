using System;
using System.Windows.Forms;
using System.Diagnostics;
using GestorContrasenas.Servicios;
using GestorContrasenas.Dominio;

namespace GestorContrasenas.UI
{
    public partial class MainForm : Form
    {
        private readonly GestorContrasenasService servicio;
        private readonly string claveMaestra;
        private readonly int usuarioId;

        public MainForm(int usuarioId, string claveMaestra)
        {
            this.usuarioId = usuarioId;
            this.claveMaestra = claveMaestra;
            servicio = new GestorContrasenasService(usuarioId);
            InitializeComponent();
            CargarListado();
        }

        private void CargarListado()
        {
            lvEntradas.BeginUpdate();
            lvEntradas.Items.Clear();
            foreach (var e in servicio.Listar())
            {
                var item = new ListViewItem(e.Id.ToString());
                item.SubItems.Add(e.Servicio);
                item.SubItems.Add(e.Usuario);
                item.SubItems.Add("••••••••"); // No mostrar en claro
                item.Tag = e; // guardar la entrada completa para descifrar al copiar
                lvEntradas.Items.Add(item);
            }
            lvEntradas.EndUpdate();
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            var servicioTxt = txtServicio.Text.Trim();
            var usuarioTxt = txtUsuario.Text.Trim();
            var secretoTxt = txtSecreto.Text;
            var loginUrlTxt = txtLoginUrl.Text.Trim();
            if (servicioTxt.Length == 0 || usuarioTxt.Length == 0 || secretoTxt.Length == 0)
            {
                MessageBox.Show("Completa servicio, usuario y contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                servicio.Agregar(servicioTxt, usuarioTxt, secretoTxt, claveMaestra, string.IsNullOrWhiteSpace(loginUrlTxt) ? null : loginUrlTxt);
                txtServicio.Clear();
                txtUsuario.Clear();
                txtSecreto.Clear();
                txtLoginUrl.Clear();
                CargarListado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (lvEntradas.SelectedItems.Count == 0) return;
            var sel = lvEntradas.SelectedItems[0];
            if (!int.TryParse(sel.SubItems[0].Text, out var id)) return;
            if (MessageBox.Show("¿Eliminar la entrada seleccionada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    servicio.Eliminar(id);
                    CargarListado();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCopiar_Click(object? sender, EventArgs e)
        {
            if (lvEntradas.SelectedItems.Count == 0) return;
            var eSel = lvEntradas.SelectedItems[0].Tag as EntradaContrasena;
            if (eSel == null) return;
            try
            {
                var secreto = servicio.ObtenerSecretoDescifrado(eSel, claveMaestra);
                if (!string.IsNullOrEmpty(secreto))
                {
                    Clipboard.SetText(secreto);
                    lblEstado.Text = "Contraseña copiada al portapapeles";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object? sender, EventArgs e)
        {
            CargarListado();
        }

        private void btnVerSecreto_Click(object? sender, EventArgs e)
        {
            // Alternar visibilidad de la contraseña
            txtSecreto.PasswordChar = txtSecreto.PasswordChar == '\0' ? '•' : '\0';
        }

        private void btnGenerarSecreto_Click(object? sender, EventArgs e)
        {
            using var gen = new GeneradorForm();
            if (gen.ShowDialog(this) == DialogResult.OK)
            {
                txtSecreto.Text = gen.ContrasenaGenerada;
            }
        }

        private void btnAbrirSitio_Click(object? sender, EventArgs e)
        {
            string? url = null;
            var txt = txtLoginUrl.Text.Trim();
            if (!string.IsNullOrWhiteSpace(txt))
            {
                url = txt;
            }
            else if (lvEntradas.SelectedItems.Count > 0)
            {
                if (lvEntradas.SelectedItems[0].Tag is EntradaContrasena eSel)
                {
                    url = eSel.LoginUrl;
                }
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("No hay URL de login. Escribe una en 'URL login' o selecciona una entrada que la tenga.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Asegurar esquema válido
                if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    url = "https://" + url;
                }
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el sitio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
