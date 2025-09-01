using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Markdig;
using GestorContrasenas.Seguridad;

namespace GestorContrasenas.UI
{
    public partial class AyudaForm : Form
    {
        private readonly string carpetaAyuda;
        private const int SplitPreferido = 220;

        public AyudaForm()
        {
            InitializeComponent();
            carpetaAyuda = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "ayuda");
            try
            {
                splitContainer1.SizeChanged += (_, __) =>
                {
                    try { AjustarSplitterSeguro(); } catch { }
                };
            }
            catch { /* no crítico */ }
        }

        private void AyudaForm_Load(object? sender, EventArgs e)
        {
            try
            {
                // Diferir para asegurar que el SplitContainer tenga tamaño real
                BeginInvoke(new Action(() =>
                {
                    try
                    {
                        // Fijar mínimos deseados ahora que habrá medidas
                        splitContainer1.Panel1MinSize = 160;
                        splitContainer1.Panel2MinSize = 200;
                    }
                    catch { }
                    try { AjustarSplitterSeguro(SplitPreferido); } catch { }
                    try { RestaurarPreferenciasUI(); } catch { }
                    try { CargarIndice(); } catch { }
                }));
            }
            catch (Exception ex)
            {
                try { Logger.Error(ex, "AyudaForm_Load"); } catch { }
                MessageBox.Show(this, $"No se pudo cargar la Ayuda: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try { Close(); } catch { }
            }
        }

        private void AjustarSplitterSeguro(int? preferido = null)
        {
            var total = splitContainer1.ClientSize.Width;
            var minIzq = splitContainer1.Panel1MinSize;
            var minDer = splitContainer1.Panel2MinSize;
            if (total <= 0) return; // aún sin medir
            var maxIzq = Math.Max(minIzq, total - minDer);
            var pref = preferido ?? SplitPreferido;
            var dist = Math.Min(Math.Max(pref, minIzq), maxIzq);
            // Si el contenedor es más pequeño que la suma de mínimos, dejar el mínimo izquierdo
            if (total < (minIzq + minDer)) dist = minIzq;
            splitContainer1.SplitterDistance = dist;
        }

        private void CargarIndice()
        {
            tvIndice.Nodes.Clear();
            if (!Directory.Exists(carpetaAyuda))
            {
                wbContenido.DocumentText = "<html><body style='font-family:Segoe UI'>No se encontraron archivos de ayuda (docs/ayuda).</body></html>";
                return;
            }
            var root = new TreeNode("Contenido") { Tag = null };
            foreach (var file in Directory.GetFiles(carpetaAyuda, "*.md").OrderBy(f => f))
            {
                var nombre = Path.GetFileNameWithoutExtension(file);
                var texto = TituloAmigable(nombre);
                var nodo = new TreeNode(texto) { Tag = file };
                root.Nodes.Add(nodo);
            }
            tvIndice.Nodes.Add(root);
            root.Expand();
            if (root.Nodes.Count > 0)
            {
                tvIndice.SelectedNode = root.Nodes[0];
            }
        }

        private static string TituloAmigable(string nombre)
        {
            // convierte 'keyboard_layout' o 'inicio' a 'Keyboard layout' / 'Inicio'
            var conEspacios = nombre.Replace('_', ' ').Replace('-', ' ');
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(conEspacios);
        }

        private void tvIndice_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is string ruta && File.Exists(ruta))
            {
                try
                {
                    var md = File.ReadAllText(ruta);
                    var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                    var htmlBody = Markdown.ToHtml(md, pipeline);
                    var html = $@"<!DOCTYPE html>
<html>
<head>
  <meta charset='utf-8'>
  <style>
    body {{ font-family: 'Segoe UI', Arial, sans-serif; margin: 12px; color: #1b1b1b; }}
    h1, h2, h3 {{ color: #0b5cad; }}
    pre, code {{ background:#f5f5f5; padding:2px 4px; border-radius:4px; }}
    pre {{ padding:10px; overflow:auto; }}
    table {{ border-collapse: collapse; }}
    th, td {{ border: 1px solid #ddd; padding: 6px 10px; }}
    a {{ color: #0b5cad; }}
    ul {{ margin-left: 20px; }}
  </style>
  <base href='file:///{Path.GetFullPath(carpetaAyuda).Replace("\\", "/")}/' />
  <title>Ayuda</title>
  </head>
<body>
{htmlBody}
</body>
</html>";
                    wbContenido.DocumentText = html;
                }
                catch (Exception ex)
                {
                    wbContenido.DocumentText = $"<html><body style='font-family:Segoe UI'>No se pudo cargar la ayuda: {System.Net.WebUtility.HtmlEncode(ex.Message)}</body></html>";
                }
            }
        }

        private static string RutaPrefs()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gestor_contrasenas");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "ayuda_split.txt");
        }

        private void RestaurarPreferenciasUI()
        {
            try
            {
                var ruta = RutaPrefs();
                if (File.Exists(ruta))
                {
                    var txt = File.ReadAllText(ruta).Trim();
                    if (int.TryParse(txt, out var dist))
                    {
                        // Limitar con base en el tamaño real del SplitContainer
                        var total = splitContainer1.ClientSize.Width;
                        var minIzq = splitContainer1.Panel1MinSize;
                        var minDer = splitContainer1.Panel2MinSize;
                        if (total > 0)
                        {
                            var maxIzq = Math.Max(minIzq, total - minDer);
                            if (dist < minIzq) dist = minIzq;
                            if (dist > maxIzq) dist = maxIzq;
                            splitContainer1.SplitterDistance = dist;
                        }
                    }
                }
            }
            catch { /* no crítico */ }
        }

        private void AyudaForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                File.WriteAllText(RutaPrefs(), splitContainer1.SplitterDistance.ToString());
            }
            catch { /* no crítico */ }
        }
    }
}
