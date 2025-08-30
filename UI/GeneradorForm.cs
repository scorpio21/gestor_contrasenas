using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace GestorContrasenas.UI
{
    public partial class GeneradorForm : Form
    {
        public string ContrasenaGenerada { get; private set; } = string.Empty;

        public GeneradorForm()
        {
            InitializeComponent();
            Generar();
        }

        private void btnGenerar_Click(object? sender, EventArgs e)
        {
            Generar();
        }

        private void btnUsar_Click(object? sender, EventArgs e)
        {
            ContrasenaGenerada = txtResultado.Text;
            if (string.IsNullOrEmpty(ContrasenaGenerada))
            {
                MessageBox.Show("Genera una contraseña primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Generar()
        {
            if (tabs.SelectedTab == tabFrase)
            {
                GenerarFrase();
            }
            else
            {
                GenerarContrasena();
            }
        }

        private void GenerarContrasena()
        {
            int len = (int)nudLongitud.Value;
            if (len < 5) len = 5;

            var sbPool = new StringBuilder();
            const string AZ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string az = "abcdefghijklmnopqrstuvwxyz";
            const string d0 = "0123456789";
            const string sp = "!@#$%^&*()-_=+[]{};:,.?/\\|`~";
            const string ambiguos = "Il1O0";

            if (chkAZ.Checked) sbPool.Append(AZ);
            if (chkaz.Checked) sbPool.Append(az);
            if (chk09.Checked) sbPool.Append(d0);
            if (chkEspeciales.Checked) sbPool.Append(sp);

            var pool = sbPool.ToString();
            if (string.IsNullOrEmpty(pool))
            {
                MessageBox.Show("Selecciona al menos un conjunto de caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (chkEvitarAmbiguos.Checked)
            {
                pool = new string(pool.Where(c => !ambiguos.Contains(c)).ToArray());
            }

            var res = new char[len];
            var bytes = new byte[len * 4];
            RandomNumberGenerator.Fill(bytes);
            for (int i = 0; i < len; i++)
            {
                uint val = BitConverter.ToUInt32(bytes, i * 4);
                res[i] = pool[(int)(val % (uint)pool.Length)];
            }
            txtResultado.Text = new string(res);
        }

        private void GenerarFrase()
        {
            int n = (int)nudNumPalabras.Value;
            if (n < 3) n = 3;
            string sep = string.IsNullOrEmpty(txtSeparador.Text) ? "-" : txtSeparador.Text;
            bool mayIni = chkMayusInicial.Checked;
            bool incNum = chkIncluirNumero.Checked;

            // Lista pequeña de palabras comunes (se puede ampliar o cargar de recurso)
            string[] palabras = new string[]
            {
                "cielo","bosque","lluvia","sol","norte","sur","este","oeste","gato","perro","cobre","plata","oro","río","lago","luz","nube","viento","roble","pino","hoja","luna","mar","arena","roca","fuego","trueno","nieve","ave","pez"
            };

            var bytes = new byte[n * 4];
            RandomNumberGenerator.Fill(bytes);
            var sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                uint idx = BitConverter.ToUInt32(bytes, i * 4);
                string w = palabras[(int)(idx % (uint)palabras.Length)];
                if (mayIni && w.Length > 0)
                    w = char.ToUpperInvariant(w[0]) + w.Substring(1);
                if (i > 0) sb.Append(sep);
                sb.Append(w);
            }
            if (incNum)
            {
                // añadir un número de 0-9 al final
                var b = new byte[1];
                RandomNumberGenerator.Fill(b);
                sb.Append((char)('0' + (b[0] % 10)));
            }
            txtResultado.Text = sb.ToString();
        }

        private void btnCopiar_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResultado.Text))
            {
                Clipboard.SetText(txtResultado.Text);
            }
        }
    }
}
