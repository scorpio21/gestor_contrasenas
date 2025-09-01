using System;
using System.Windows.Forms;

namespace GestorContrasenas.UI
{
    partial class GeneradorForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtResultado;
        private Button btnCopiar;
        private TabControl tabs;
        private TabPage tabContrasena;
        private TabPage tabFrase;
        private NumericUpDown nudLongitud;
        private CheckBox chkAZ;
        private CheckBox chkaz;
        private CheckBox chk09;
        private CheckBox chkEspeciales;
        private CheckBox chkEvitarAmbiguos;
        private Label lblLongitud;
        private Button btnGenerar;
        private Button btnUsar;
        private Button btnCancelar;
        // Controles de Frase
        private NumericUpDown nudNumPalabras;
        private TextBox txtSeparador;
        private CheckBox chkMayusInicial;
        private CheckBox chkIncluirNumero;
        private Label lblNumPalabras;
        private Label lblSeparador;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtResultado = new TextBox();
            btnCopiar = new Button();
            tabs = new TabControl();
            tabContrasena = new TabPage();
            lblLongitud = new Label();
            nudLongitud = new NumericUpDown();
            chkAZ = new CheckBox();
            chkaz = new CheckBox();
            chk09 = new CheckBox();
            chkEspeciales = new CheckBox();
            chkEvitarAmbiguos = new CheckBox();
            tabFrase = new TabPage();
            lblNumPalabras = new Label();
            nudNumPalabras = new NumericUpDown();
            lblSeparador = new Label();
            txtSeparador = new TextBox();
            chkMayusInicial = new CheckBox();
            chkIncluirNumero = new CheckBox();
            btnGenerar = new Button();
            btnUsar = new Button();
            btnCancelar = new Button();
            tabs.SuspendLayout();
            tabContrasena.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLongitud).BeginInit();
            tabFrase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumPalabras).BeginInit();
            SuspendLayout();
            // 
            // txtResultado
            // 
            txtResultado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtResultado.Location = new Point(17, 20);
            txtResultado.Margin = new Padding(4, 5, 4, 5);
            txtResultado.Name = "txtResultado";
            txtResultado.ReadOnly = true;
            txtResultado.Size = new Size(655, 31);
            txtResultado.TabIndex = 0;
            // 
            // btnCopiar
            // 
            btnCopiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopiar.Location = new Point(580, 52);
            btnCopiar.Margin = new Padding(4, 5, 4, 5);
            btnCopiar.Name = "btnCopiar";
            btnCopiar.Size = new Size(86, 38);
            btnCopiar.TabIndex = 6;
            btnCopiar.Text = "Copiar";
            btnCopiar.UseVisualStyleBackColor = true;
            btnCopiar.Click += btnCopiar_Click;
            // 
            // tabs
            // 
            tabs.Controls.Add(tabContrasena);
            tabs.Controls.Add(tabFrase);
            tabs.Location = new Point(17, 83);
            tabs.Margin = new Padding(4, 5, 4, 5);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(657, 270);
            tabs.TabIndex = 7;
            // 
            // tabContrasena
            // 
            tabContrasena.Controls.Add(lblLongitud);
            tabContrasena.Controls.Add(nudLongitud);
            tabContrasena.Controls.Add(chkAZ);
            tabContrasena.Controls.Add(chkaz);
            tabContrasena.Controls.Add(chk09);
            tabContrasena.Controls.Add(chkEspeciales);
            tabContrasena.Controls.Add(chkEvitarAmbiguos);
            tabContrasena.Location = new Point(4, 34);
            tabContrasena.Margin = new Padding(4, 5, 4, 5);
            tabContrasena.Name = "tabContrasena";
            tabContrasena.Size = new Size(649, 232);
            tabContrasena.TabIndex = 0;
            tabContrasena.Text = "Contraseña";
            tabContrasena.UseVisualStyleBackColor = true;
            // 
            // lblLongitud
            // 
            lblLongitud.AutoSize = true;
            lblLongitud.Location = new Point(20, 40);
            lblLongitud.Margin = new Padding(4, 0, 4, 0);
            lblLongitud.Name = "lblLongitud";
            lblLongitud.Size = new Size(83, 25);
            lblLongitud.TabIndex = 0;
            lblLongitud.Text = "Longitud";
            // 
            // nudLongitud
            // 
            nudLongitud.Location = new Point(111, 34);
            nudLongitud.Margin = new Padding(4, 5, 4, 5);
            nudLongitud.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            nudLongitud.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            nudLongitud.Name = "nudLongitud";
            nudLongitud.Size = new Size(171, 31);
            nudLongitud.TabIndex = 1;
            nudLongitud.Value = new decimal(new int[] { 14, 0, 0, 0 });
            // 
            // chkAZ
            // 
            chkAZ.AutoSize = true;
            chkAZ.Checked = true;
            chkAZ.CheckState = CheckState.Checked;
            chkAZ.Location = new Point(20, 91);
            chkAZ.Margin = new Padding(4, 5, 4, 5);
            chkAZ.Name = "chkAZ";
            chkAZ.Size = new Size(67, 29);
            chkAZ.TabIndex = 2;
            chkAZ.Text = "A-Z";
            // 
            // chkaz
            // 
            chkaz.AutoSize = true;
            chkaz.Checked = true;
            chkaz.CheckState = CheckState.Checked;
            chkaz.Location = new Point(120, 91);
            chkaz.Margin = new Padding(4, 5, 4, 5);
            chkaz.Name = "chkaz";
            chkaz.Size = new Size(66, 29);
            chkaz.TabIndex = 3;
            chkaz.Text = "a-z";
            // 
            // chk09
            // 
            chk09.AutoSize = true;
            chk09.Checked = true;
            chk09.CheckState = CheckState.Checked;
            chk09.Location = new Point(220, 91);
            chk09.Margin = new Padding(4, 5, 4, 5);
            chk09.Name = "chk09";
            chk09.Size = new Size(65, 29);
            chk09.TabIndex = 3;
            chk09.Text = "0-9";
            // 
            // chkEspeciales
            // 
            chkEspeciales.AutoSize = true;
            chkEspeciales.Checked = true;
            chkEspeciales.CheckState = CheckState.Checked;
            chkEspeciales.Location = new Point(320, 91);
            chkEspeciales.Margin = new Padding(4, 5, 4, 5);
            chkEspeciales.Name = "chkEspeciales";
            chkEspeciales.Size = new Size(112, 29);
            chkEspeciales.TabIndex = 4;
            chkEspeciales.Text = "!@#$%&*?";
            // 
            // chkEvitarAmbiguos
            // 
            chkEvitarAmbiguos.AutoSize = true;
            chkEvitarAmbiguos.Checked = true;
            chkEvitarAmbiguos.CheckState = CheckState.Checked;
            chkEvitarAmbiguos.Location = new Point(22, 133);
            chkEvitarAmbiguos.Margin = new Padding(4, 5, 4, 5);
            chkEvitarAmbiguos.Name = "chkEvitarAmbiguos";
            chkEvitarAmbiguos.Size = new Size(241, 29);
            chkEvitarAmbiguos.TabIndex = 5;
            chkEvitarAmbiguos.Text = "Evitar ambiguos (l, I, O, 0)";
            // 
            // tabFrase
            // 
            tabFrase.Controls.Add(lblNumPalabras);
            tabFrase.Controls.Add(nudNumPalabras);
            tabFrase.Controls.Add(lblSeparador);
            tabFrase.Controls.Add(txtSeparador);
            tabFrase.Controls.Add(chkMayusInicial);
            tabFrase.Controls.Add(chkIncluirNumero);
            tabFrase.Location = new Point(4, 34);
            tabFrase.Margin = new Padding(4, 5, 4, 5);
            tabFrase.Name = "tabFrase";
            tabFrase.Size = new Size(649, 232);
            tabFrase.TabIndex = 1;
            tabFrase.Text = "Frase";
            tabFrase.UseVisualStyleBackColor = true;
            // 
            // lblNumPalabras
            // 
            lblNumPalabras.AutoSize = true;
            lblNumPalabras.Location = new Point(14, 20);
            lblNumPalabras.Margin = new Padding(4, 0, 4, 0);
            lblNumPalabras.Name = "lblNumPalabras";
            lblNumPalabras.Size = new Size(105, 25);
            lblNumPalabras.TabIndex = 0;
            lblNumPalabras.Text = "Nº palabras";
            // 
            // nudNumPalabras
            // 
            nudNumPalabras.Location = new Point(143, 17);
            nudNumPalabras.Margin = new Padding(4, 5, 4, 5);
            nudNumPalabras.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudNumPalabras.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            nudNumPalabras.Name = "nudNumPalabras";
            nudNumPalabras.Size = new Size(171, 31);
            nudNumPalabras.TabIndex = 1;
            nudNumPalabras.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // lblSeparador
            // 
            lblSeparador.AutoSize = true;
            lblSeparador.Location = new Point(14, 70);
            lblSeparador.Margin = new Padding(4, 0, 4, 0);
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(94, 25);
            lblSeparador.TabIndex = 2;
            lblSeparador.Text = "Separador";
            // 
            // txtSeparador
            // 
            txtSeparador.Location = new Point(143, 65);
            txtSeparador.Margin = new Padding(4, 5, 4, 5);
            txtSeparador.Name = "txtSeparador";
            txtSeparador.Size = new Size(55, 31);
            txtSeparador.TabIndex = 3;
            txtSeparador.Text = "-";
            // 
            // chkMayusInicial
            // 
            chkMayusInicial.AutoSize = true;
            chkMayusInicial.Checked = true;
            chkMayusInicial.CheckState = CheckState.Checked;
            chkMayusInicial.Location = new Point(19, 120);
            chkMayusInicial.Margin = new Padding(4, 5, 4, 5);
            chkMayusInicial.Name = "chkMayusInicial";
            chkMayusInicial.Size = new Size(194, 29);
            chkMayusInicial.TabIndex = 4;
            chkMayusInicial.Text = "Mayúsculas iniciales";
            // 
            // chkIncluirNumero
            // 
            chkIncluirNumero.AutoSize = true;
            chkIncluirNumero.Checked = true;
            chkIncluirNumero.CheckState = CheckState.Checked;
            chkIncluirNumero.Location = new Point(243, 120);
            chkIncluirNumero.Margin = new Padding(4, 5, 4, 5);
            chkIncluirNumero.Name = "chkIncluirNumero";
            chkIncluirNumero.Size = new Size(152, 29);
            chkIncluirNumero.TabIndex = 5;
            chkIncluirNumero.Text = "Incluir número";
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = Color.MediumSpringGreen;
            btnGenerar.Location = new Point(21, 363);
            btnGenerar.Margin = new Padding(4, 5, 4, 5);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(129, 47);
            btnGenerar.TabIndex = 10;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // btnUsar
            // 
            btnUsar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnUsar.BackColor = Color.Lime;
            btnUsar.Location = new Point(423, 375);
            btnUsar.Margin = new Padding(4, 5, 4, 5);
            btnUsar.Name = "btnUsar";
            btnUsar.Size = new Size(121, 47);
            btnUsar.TabIndex = 9;
            btnUsar.Text = "Usar";
            btnUsar.UseVisualStyleBackColor = false;
            btnUsar.Click += btnUsar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.BackColor = Color.Red;
            btnCancelar.ForeColor = Color.Yellow;
            btnCancelar.Location = new Point(553, 375);
            btnCancelar.Margin = new Padding(4, 5, 4, 5);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(121, 47);
            btnCancelar.TabIndex = 8;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // GeneradorForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(691, 442);
            Controls.Add(tabs);
            Controls.Add(btnCopiar);
            Controls.Add(txtResultado);
            Controls.Add(btnCancelar);
            Controls.Add(btnUsar);
            Controls.Add(btnGenerar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GeneradorForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Generador de contraseñas";
            tabs.ResumeLayout(false);
            tabContrasena.ResumeLayout(false);
            tabContrasena.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLongitud).EndInit();
            tabFrase.ResumeLayout(false);
            tabFrase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumPalabras).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
