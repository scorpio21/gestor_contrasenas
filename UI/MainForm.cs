using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using GestorContrasenas.Servicios;
using GestorContrasenas.Dominio;
using Microsoft.VisualBasic.FileIO; // Parser CSV
using System.IO;
using System.Threading.Tasks;

namespace GestorContrasenas.UI
{
    public partial class MainForm : Form
    {
        private readonly GestorContrasenasService servicio;
        private string claveMaestra;
        private readonly int usuarioId;
        private string? filtroActual = null;
        private string? ultimoClipboard = null;
        private bool hibpAuto = true; // Ajuste: comprobar HIBP automáticamente

        public MainForm(int usuarioId, string claveMaestra)
        {
            this.usuarioId = usuarioId;
            this.claveMaestra = claveMaestra;
            servicio = new GestorContrasenasService(usuarioId);
            InitializeComponent();
            hibpAuto = true;
            CargarListado();
            // Iniciar temporizadores de seguridad
            autoLockTimer.Start();
            clipboardTimer.Start();
        }

        private void CargarListado()
        {
            lvEntradas.BeginUpdate();
            lvEntradas.Items.Clear();
            foreach (var entrada in servicio.Listar())
            {
                if (!string.IsNullOrWhiteSpace(filtroActual))
                {
                    var f = filtroActual!.Trim();
                    bool coincide =
                        (entrada.Servicio?.IndexOf(f, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (entrada.Usuario?.IndexOf(f, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (entrada.LoginUrl?.IndexOf(f, StringComparison.OrdinalIgnoreCase) >= 0);
                    if (!coincide) continue;
                }
                var item = new ListViewItem(entrada.Id.ToString());
                item.SubItems.Add(entrada.Servicio);
                item.SubItems.Add(entrada.Usuario);
                item.SubItems.Add("••••••••"); // No mostrar en claro
                item.SubItems.Add("-"); // Comprometida (pendiente)
                item.SubItems.Add("👁");
                item.UseItemStyleForSubItems = false; // permitir colores por subitem
                item.Tag = entrada; // guardar la entrada completa para descifrar al copiar
                lvEntradas.Items.Add(item);
            }
            lvEntradas.EndUpdate();
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
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
            ResetAutoLockTimer();
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
            ResetAutoLockTimer();
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
                    ultimoClipboard = secreto;
                    clipboardTimer.Stop();
                    clipboardTimer.Start(); // limpiar en diferido
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            CargarListado();
        }

        private void btnVerSecreto_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            // Alternar visibilidad de la contraseña
            txtSecreto.PasswordChar = txtSecreto.PasswordChar == '\0' ? '•' : '\0';
        }

        private void btnGenerarSecreto_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            using var gen = new GeneradorForm();
            if (gen.ShowDialog(this) == DialogResult.OK)
            {
                txtSecreto.Text = gen.ContrasenaGenerada;
            }
        }

        // Actualiza el medidor de fortaleza y la advertencia de reutilización
        private void ActualizarFortalezaYReutilizacion()
        {
            try
            {
                var texto = txtSecreto.Text ?? string.Empty;
                var (punt, desc) = servicio.CalcularFortaleza(texto);
                // ProgressBar estándar oculto; mantener por compatibilidad
                prgFortaleza.Value = Math.Max(0, Math.Min(prgFortaleza.Maximum, punt));
                lblFortaleza.Text = $"Fortaleza: {desc}";

                // Barra personalizada (paneles)
                int nivel = Math.Max(0, Math.Min(4, punt));
                int anchoMax = pnlFortaleza.Width;
                int ancho = nivel == 0 ? Math.Max(8, (int)Math.Round(anchoMax * 0.10)) : (int)Math.Round(anchoMax * (nivel / 4.0));
                if (string.IsNullOrEmpty(texto))
                {
                    ancho = 0;
                }
                pnlFortalezaValor.Width = Math.Min(Math.Max(0, ancho), anchoMax);
                pnlFortalezaValor.Height = pnlFortaleza.Height;
                pnlFortalezaValor.Left = pnlFortaleza.Left;
                pnlFortalezaValor.Top = pnlFortaleza.Top;

                // Colores por nivel (0=Muy débil, 1=Débil, 2=Media, 3=Fuerte, 4=Muy fuerte)
                Color color = Color.Red;
                switch (nivel)
                {
                    case 0:
                        color = Color.Red; // Muy débil
                        break;
                    case 1:
                        color = Color.Orange; // Débil
                        break;
                    case 2:
                        color = Color.Goldenrod; // Media
                        break;
                    case 3:
                        color = Color.YellowGreen; // Fuerte
                        break;
                    case 4:
                        color = Color.Green; // Muy fuerte
                        break;
                }
                pnlFortalezaValor.BackColor = color;

                // Reutilización (solo si hay algo escrito)
                if (!string.IsNullOrEmpty(texto))
                {
                    bool reutiliza = servicio.ExisteReutilizacionSecreto(texto, claveMaestra);
                    lblReutilizacion.Text = reutiliza ? "Advertencia: contraseña ya utilizada en otra entrada." : string.Empty;
                }
                else
                {
                    lblReutilizacion.Text = string.Empty;
                }
            }
            catch
            {
                // En caso de error, no bloquear la UI
                lblFortaleza.Text = "Fortaleza: (N/A)";
                lblReutilizacion.Text = string.Empty;
                pnlFortalezaValor.Width = 0;
            }
        }

        // Archivo > Salir
        private void salirToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // Archivo > Exportar comprometidas a CSV
        private async void exportarComprometidasToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            using var dlg = new SaveFileDialog
            {
                Title = "Guardar comprometidas como CSV",
                Filter = "CSV (*.csv)|*.csv",
                FileName = "comprometidas.csv",
                OverwritePrompt = true
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            // Aviso: se realizarán consultas a HIBP y NO se incluirán contraseñas en el CSV
            var continuar = MessageBox.Show(this,
                "Se consultará HIBP para cada entrada y el archivo NO incluirá las contraseñas. ¿Continuar?",
                "Exportar comprometidas",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (continuar != DialogResult.Yes) return;

            try
            {
                UseWaitCursor = true;
                Enabled = false;

                // Crear índice Id -> URL para incluir en CSV
                var mapaUrls = new System.Collections.Generic.Dictionary<int, string?>(capacity: 256);
                foreach (var entrada in servicio.Listar())
                {
                    mapaUrls[entrada.Id] = entrada.LoginUrl;
                }

                int total = 0, comprometidas = 0;
                using var sw = new StreamWriter(dlg.FileName, false, new System.Text.UTF8Encoding(true));

                string Esc(string? s)
                {
                    s ??= string.Empty;
                    var need = s.Contains('"') || s.Contains(',') || s.Contains('\n') || s.Contains('\r');
                    if (s.Contains('"')) s = s.Replace("\"", "\"\"");
                    return need ? "\"" + s + "\"" : s;
                }

                // Cabeceras
                sw.WriteLine("name,url,username,comprometida,veces");

                // Iterar descifrando y comprobando HIBP una a una (cache acelera repetidas)
                foreach (var fila in servicio.ListarDescifrado(claveMaestra))
                {
                    total++;
                    if (string.IsNullOrEmpty(fila.Secreto)) continue;
                    var (comp, conteo) = await servicio.EstaComprometidaAsync(fila.Secreto);
                    if (comp)
                    {
                        comprometidas++;
                        mapaUrls.TryGetValue(fila.Id, out var url);
                        var line = string.Join(",", new[]
                        {
                            Esc(fila.Servicio),
                            Esc(url ?? string.Empty),
                            Esc(fila.Usuario),
                            "Sí",
                            (conteo.HasValue ? conteo.Value.ToString() : "")
                        });
                        sw.WriteLine(line);
                    }
                }

                sw.Flush();
                MessageBox.Show(this, $"Exportación completada. Total: {total}. Comprometidas: {comprometidas}.", "Exportar comprometidas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudo exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void txtSecreto_TextChanged(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            ActualizarFortalezaYReutilizacion();
        }

        private void btnAbrirSitio_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
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
            ResetAutoLockTimer();
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
            ResetAutoLockTimer();
            // Detectar clic en la columna del "ojito" para mostrar la contraseña descifrada
            var info = lvEntradas.HitTest(e.Location);
            if (info.Item == null || info.SubItem == null) return;

            // índice de subitem clicado
            int subIndex = info.Item.SubItems.IndexOf(info.SubItem);
            // Columnas: 0=ID, 1=Servicio, 2=Usuario, 3=Contraseña, 4=Comprometida, 5=Ojito
            if (subIndex == 5)
            {
                if (info.Item.Tag is EntradaContrasena entrada)
                {
                    try
                    {
                        // Alternar visibilidad en la columna 3
                        var actual = info.Item.SubItems[3].Text;
                        bool oculto = actual == "••••••••";
                        if (oculto)
                        {
                            var secreto = servicio.ObtenerSecretoDescifrado(entrada, claveMaestra);
                            if (!string.IsNullOrEmpty(secreto))
                            {
                                info.Item.SubItems[3].Text = secreto;
                                // iniciar temporizador de auto-ocultado
                                revealTimer.Stop();
                                revealTimer.Start();

                                // Lanzar comprobación de comprometida al revelar (si está activo)
                                if (hibpAuto)
                                {
                                    _ = ComprobarComprometidaAsync(info.Item, secreto);
                                }
                            }
                        }
                        else
                        {
                            info.Item.SubItems[3].Text = "••••••••";
                            // si no quedan visibles, detener temporizador
                            if (!ExisteAlgunaVisible())
                            {
                                revealTimer.Stop();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"No se pudo descifrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool ExisteAlgunaVisible()
        {
            foreach (ListViewItem it in lvEntradas.Items)
            {
                if (it.SubItems.Count > 3 && it.SubItems[3].Text != "••••••••") return true;
            }
            return false;
        }

        private void OcultarContrasenasVisibles()
        {
            foreach (ListViewItem it in lvEntradas.Items)
            {
                if (it.SubItems.Count > 3 && it.SubItems[3].Text != "••••••••")
                {
                    it.SubItems[3].Text = "••••••••";
                }
            }
        }

        private void revealTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                OcultarContrasenasVisibles();
            }
            finally
            {
                revealTimer.Stop();
            }
        }

        private void lvEntradas_SelectedIndexChanged(object? sender, EventArgs e)
        {
            OcultarContrasenasVisibles();
            revealTimer.Stop();
            // Al seleccionar, si podemos descifrar, comprobar automáticamente (si está activo)
            if (lvEntradas.SelectedItems.Count > 0)
            {
                var item = lvEntradas.SelectedItems[0];
                if (item.Tag is EntradaContrasena entrada)
                {
                    try
                    {
                        var secreto = servicio.ObtenerSecretoDescifrado(entrada, claveMaestra);
                        if (hibpAuto && !string.IsNullOrEmpty(secreto))
                        {
                            _ = ComprobarComprometidaAsync(item, secreto);
                        }
                    }
                    catch { /* ignorar */ }
                }
            }
        }

        private void chkHibpAuto_CheckedChanged(object? sender, EventArgs e)
        {
            try
            {
                hibpAuto = chkHibpAuto.Checked;
                // Si se desactiva, limpiar estado visual a "-"
                if (!hibpAuto)
                {
                    foreach (ListViewItem it in lvEntradas.Items)
                    {
                        if (it.SubItems.Count > 4)
                        {
                            it.SubItems[4].Text = "-";
                            it.SubItems[4].ForeColor = Color.DimGray;
                        }
                    }
                }
                else
                {
                    // Si se activa y hay selección, intentar comprobar
                    if (lvEntradas.SelectedItems.Count > 0)
                    {
                        var sel = lvEntradas.SelectedItems[0];
                        if (sel.Tag is EntradaContrasena entrada)
                        {
                            try
                            {
                                var secreto = servicio.ObtenerSecretoDescifrado(entrada, claveMaestra);
                                if (!string.IsNullOrEmpty(secreto))
                                {
                                    _ = ComprobarComprometidaAsync(sel, secreto);
                                }
                            }
                            catch { /* ignorar */ }
                        }
                    }
                }
            }
            catch { /* ignorar */ }
        }

        private void lvEntradas_Leave(object? sender, EventArgs e)
        {
            OcultarContrasenasVisibles();
            revealTimer.Stop();
        }

        private void btnExportar_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
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

        private void btnExportarSeguro_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            using var dlg = new SaveFileDialog
            {
                Title = "Exportar respaldo cifrado",
                Filter = "Backup cifrado (*.gpass)|*.gpass|Todos los archivos (*.*)|*.*",
                FileName = "respaldo.gpass",
                OverwritePrompt = true
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var blob = servicio.ExportarJsonCifrado(claveMaestra); // Base64 del JSON cifrado
                File.WriteAllText(dlg.FileName, blob);
                MessageBox.Show(this, "Exportación segura completada.", "Exportar seguro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudo exportar de forma segura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportarSeguro_Click(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            using var dlg = new OpenFileDialog
            {
                Title = "Importar respaldo cifrado",
                Filter = "Backup cifrado (*.gpass)|*.gpass|Todos los archivos (*.*)|*.*",
                Multiselect = false
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var blob = File.ReadAllText(dlg.FileName);
                var count = servicio.ImportarJsonCifrado(claveMaestra, blob);
                CargarListado();
                MessageBox.Show(this, $"Importación segura completada. Entradas agregadas: {count}.", "Importar seguro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudo importar de forma segura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Búsqueda y filtrado
        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            ResetAutoLockTimer();
            filtroActual = txtBuscar.Text;
            CargarListado();
        }

        // Seguridad: Auto-lock por inactividad
        private void autoLockTimer_Tick(object? sender, EventArgs e)
        {
            // Limpiar portapapeles si procede
            try
            {
                if (!string.IsNullOrEmpty(ultimoClipboard) && Clipboard.ContainsText() && Clipboard.GetText() == ultimoClipboard)
                {
                    Clipboard.Clear();
                }
            }
            catch { /* ignorar */ }
            clipboardTimer.Stop();
            ultimoClipboard = null;

            // Solicitar reautenticación
            using var login = new LoginForm();
            var res = login.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                if (login.UsuarioId != this.usuarioId)
                {
                    MessageBox.Show(this, "El usuario no coincide con la sesión actual. Reinicia la aplicación para cambiar de usuario.", "Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Mantener usuario actual; solo actualizar clave
                }
                this.claveMaestra = login.ClaveMaestra;
                // Refrescar vistas y reiniciar timers
                CargarListado();
                ResetAutoLockTimer();
            }
            else
            {
                // Cerrar aplicación si no reautentica
                Close();
            }
        }

        // Seguridad: limpieza de portapapeles
        private void clipboardTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ultimoClipboard) && Clipboard.ContainsText() && Clipboard.GetText() == ultimoClipboard)
                {
                    Clipboard.Clear();
                    lblEstado.Text = "Portapapeles limpiado por seguridad";
                }
            }
            catch { /* ignorar acceso al clipboard */ }
            finally
            {
                ultimoClipboard = null;
                clipboardTimer.Stop();
            }
        }

        private void ResetAutoLockTimer()
        {
            try
            {
                autoLockTimer.Stop();
                autoLockTimer.Start();
            }
            catch { /* ignorar */ }
        }

        // Comprueba con el servicio HIBP y actualiza la columna "Comprometida"
        private async Task ComprobarComprometidaAsync(ListViewItem item, string secretoPlano)
        {
            try
            {
                // Mostrar estado en curso
                if (item.SubItems.Count > 4)
                {
                    item.SubItems[4].Text = "...";
                    item.SubItems[4].ForeColor = Color.DimGray;
                }

                var (comp, conteo) = await servicio.EstaComprometidaAsync(secretoPlano);
                if (item.ListView?.IsDisposed == true) return;

                if (item.SubItems.Count > 4)
                {
                    if (comp)
                    {
                        item.SubItems[4].Text = "Sí" + (conteo.HasValue && conteo.Value > 0 ? $" ({conteo.Value})" : "");
                        item.SubItems[4].ForeColor = Color.DarkRed;
                    }
                    else
                    {
                        item.SubItems[4].Text = "No";
                        item.SubItems[4].ForeColor = Color.DarkGreen;
                    }
                }
            }
            catch
            {
                if (item.SubItems.Count > 4)
                {
                    item.SubItems[4].Text = "-";
                    item.SubItems[4].ForeColor = Color.DimGray;
                }
            }
        }
    }
}
