using System;
using System.Windows.Forms;
using System.Diagnostics;
using GestorContrasenas.Servicios;
using GestorContrasenas.Dominio;
using Microsoft.VisualBasic.FileIO; // Parser CSV
using System.IO;

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
            foreach (var entrada in servicio.Listar())
            {
                var item = new ListViewItem(entrada.Id.ToString());
                item.SubItems.Add(entrada.Servicio);
                item.SubItems.Add(entrada.Usuario);
                item.SubItems.Add("••••••••"); // No mostrar en claro
                item.SubItems.Add("👁");
                item.Tag = entrada; // guardar la entrada completa para descifrar al copiar
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

        private void btnImportar_Click(object? sender, EventArgs e)
        {
            // Importa un CSV (Edge/Chrome) con columnas: name,url,username,password,note
            using var dlg = new OpenFileDialog
            {
                Title = "Selecciona CSV de contraseñas",
                Filter = "CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*",
                Multiselect = false
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var ruta = dlg.FileName;
            int ok = 0, fallos = 0, saltos = 0, duplicados = 0;
            try
            {
                // Preparar índice de duplicados existentes en BD y vistos en el CSV
                var existentes = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var entrada in servicio.Listar())
                {
                    var clave = ($"{entrada.Servicio}".Trim().ToLowerInvariant()) + "||" + ($"{entrada.Usuario}".Trim().ToLowerInvariant());
                    existentes.Add(clave);
                }
                var vistosCsv = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);

                using var parser = new TextFieldParser(ruta);
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                // Cabeceras
                if (!parser.EndOfData)
                {
                    var headers = parser.ReadFields();
                }

                while (!parser.EndOfData)
                {
                    try
                    {
                        var fields = parser.ReadFields();
                        if (fields == null || fields.Length < 4) { saltos++; continue; }

                        var name = (fields.Length > 0 ? fields[0] : string.Empty).Trim();
                        var url = (fields.Length > 1 ? fields[1] : string.Empty).Trim();
                        var username = (fields.Length > 2 ? fields[2] : string.Empty).Trim();
                        var password = (fields.Length > 3 ? fields[3] : string.Empty);
                        // note = fields[4] si existe (ignorado por ahora)

                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                        {
                            saltos++;
                            continue;
                        }

                        var clave = name.Trim().ToLowerInvariant() + "||" + username.Trim().ToLowerInvariant();
                        if (existentes.Contains(clave) || vistosCsv.Contains(clave))
                        {
                            duplicados++;
                            continue;
                        }

                        servicio.Agregar(name, username, password, claveMaestra, string.IsNullOrWhiteSpace(url) ? null : url);
                        existentes.Add(clave);
                        vistosCsv.Add(clave);
                        ok++;
                    }
                    catch
                    {
                        fallos++;
                    }
                }

                CargarListado();
                MessageBox.Show($"Importación completada. Éxitos: {ok}. Duplicados: {duplicados}. Saltadas: {saltos}. Fallos: {fallos}.", "Importar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo importar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvEntradas_MouseClick(object? sender, MouseEventArgs e)
        {
            // Detectar clic en la columna del "ojito" para mostrar la contraseña descifrada
            var info = lvEntradas.HitTest(e.Location);
            if (info.Item == null || info.SubItem == null) return;

            // índice de subitem clicado
            int subIndex = info.Item.SubItems.IndexOf(info.SubItem);
            // Columnas: 0=ID, 1=Servicio, 2=Usuario, 3=Contraseña (oculta), 4=Ojito
            if (subIndex == 4)
            {
                if (info.Item.Tag is EntradaContrasena entrada)
                {
                    try
                    {
                        var secreto = servicio.ObtenerSecretoDescifrado(entrada, claveMaestra);
                        if (!string.IsNullOrEmpty(secreto))
                        {
                            MessageBox.Show(this, $"Contraseña: {secreto}", "Ver contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"No se pudo descifrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportar_Click(object? sender, EventArgs e)
        {
            // Exporta a CSV compatible con Excel: columnas name,url,username,password,note
            using var dlg = new SaveFileDialog
            {
                Title = "Guardar como CSV (Excel)",
                Filter = "CSV (*.csv)|*.csv",
                FileName = "contraseñas.csv",
                OverwritePrompt = true
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            // Advertencia de seguridad: guardaremos contraseñas en claro
            if (MessageBox.Show(this,
                "El archivo contendrá contraseñas en claro. ¿Deseas continuar?",
                "Exportar CSV",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Mapear login_url por Id
                var mapaUrls = new System.Collections.Generic.Dictionary<int, string?>(capacity: 256);
                foreach (var entrada in servicio.Listar())
                {
                    mapaUrls[entrada.Id] = entrada.LoginUrl;
                }

                // Escribir CSV con BOM para Excel
                using var sw = new StreamWriter(dlg.FileName, false, new System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier: true));

                string Esc(string? s)
                {
                    s ??= string.Empty;
                    var need = s.Contains('"') || s.Contains(',') || s.Contains('\n') || s.Contains('\r');
                    if (s.Contains('"')) s = s.Replace("\"", "\"\"");
                    return need ? "\"" + s + "\"" : s;
                }

                // Cabeceras estándar de navegadores
                sw.WriteLine("name,url,username,password,note");

                foreach (var fila in servicio.ListarDescifrado(claveMaestra))
                {
                    mapaUrls.TryGetValue(fila.Id, out var url);
                    var line = string.Join(",", new[]
                    {
                        Esc(fila.Servicio),
                        Esc(url ?? string.Empty),
                        Esc(fila.Usuario),
                        Esc(fila.Secreto),
                        ""
                    });
                    sw.WriteLine(line);
                }

                sw.Flush();
                MessageBox.Show(this, "Exportación completada.", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudo exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
