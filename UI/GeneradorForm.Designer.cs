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
            this.txtResultado = new TextBox();
            this.btnCopiar = new Button();
            this.tabs = new TabControl();
            this.tabContrasena = new TabPage();
            this.tabFrase = new TabPage();
            this.nudLongitud = new NumericUpDown();
            this.chkAZ = new CheckBox();
            this.chkaz = new CheckBox();
            this.chk09 = new CheckBox();
            this.chkEspeciales = new CheckBox();
            this.chkEvitarAmbiguos = new CheckBox();
            this.btnGenerar = new Button();
            this.btnUsar = new Button();
            this.btnCancelar = new Button();
            this.lblLongitud = new Label();
            this.lblNumPalabras = new Label();
            this.nudNumPalabras = new NumericUpDown();
            this.lblSeparador = new Label();
            this.txtSeparador = new TextBox();
            this.chkMayusInicial = new CheckBox();
            this.chkIncluirNumero = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudLongitud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumPalabras)).BeginInit();
            this.tabs.SuspendLayout();
            this.tabContrasena.SuspendLayout();
            this.tabFrase.SuspendLayout();
            this.SuspendLayout();
            // txtResultado
            this.txtResultado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtResultado.Location = new System.Drawing.Point(12, 12);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.Size = new System.Drawing.Size(460, 23);
            this.txtResultado.TabIndex = 0;
            // btnCopiar
            this.btnCopiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnCopiar.Location = new System.Drawing.Point(412, 12);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(60, 23);
            this.btnCopiar.TabIndex = 6;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new EventHandler(this.btnCopiar_Click);
            // tabs
            this.tabs.Location = new System.Drawing.Point(12, 50);
            this.tabs.Name = "tabs";
            this.tabs.Size = new System.Drawing.Size(460, 120);
            this.tabs.TabIndex = 7;
            this.tabs.Controls.Add(this.tabContrasena);
            this.tabs.Controls.Add(this.tabFrase);
            // tabContrasena
            this.tabContrasena.Text = "Contraseña";
            this.tabContrasena.UseVisualStyleBackColor = true;
            // lblLongitud
            this.lblLongitud.AutoSize = true;
            this.lblLongitud.Location = new System.Drawing.Point(12, 50);
            this.lblLongitud.Text = "Longitud";
            // nudLongitud
            this.nudLongitud.Location = new System.Drawing.Point(70, 48);
            this.nudLongitud.Minimum = 5;
            this.nudLongitud.Maximum = 128;
            this.nudLongitud.Value = 14;
            this.nudLongitud.TabIndex = 1;
            // chkAZ
            this.chkAZ.AutoSize = true;
            this.chkAZ.Checked = true;
            this.chkAZ.Location = new System.Drawing.Point(12, 80);
            this.chkAZ.Text = "A-Z";
            // chkaz
            this.chkaz.AutoSize = true;
            this.chkaz.Checked = true;
            this.chkaz.Location = new System.Drawing.Point(82, 80);
            this.chkaz.Text = "a-z";
            // chk09
            this.chk09.AutoSize = true;
            this.chk09.Checked = true;
            this.chk09.Location = new System.Drawing.Point(152, 80);
            this.chk09.Text = "0-9";
            // chkEspeciales
            this.chkEspeciales.AutoSize = true;
            this.chkEspeciales.Checked = true;
            this.chkEspeciales.Location = new System.Drawing.Point(222, 80);
            this.chkEspeciales.Text = "!@#$%&*?";
            // chkEvitarAmbiguos
            this.chkEvitarAmbiguos.AutoSize = true;
            this.chkEvitarAmbiguos.Checked = true;
            this.chkEvitarAmbiguos.Location = new System.Drawing.Point(13, 105);
            this.chkEvitarAmbiguos.Text = "Evitar ambiguos (l, I, O, 0)";
            // Añadir a tabContrasena
            this.tabContrasena.Controls.Add(this.lblLongitud);
            this.tabContrasena.Controls.Add(this.nudLongitud);
            this.tabContrasena.Controls.Add(this.chkAZ);
            this.tabContrasena.Controls.Add(this.chkaz);
            this.tabContrasena.Controls.Add(this.chk09);
            this.tabContrasena.Controls.Add(this.chkEspeciales);
            this.tabContrasena.Controls.Add(this.chkEvitarAmbiguos);
            // tabFrase
            this.tabFrase.Text = "Frase";
            this.tabFrase.UseVisualStyleBackColor = true;
            // lblNumPalabras
            this.lblNumPalabras.AutoSize = true;
            this.lblNumPalabras.Location = new System.Drawing.Point(10, 12);
            this.lblNumPalabras.Text = "Nº palabras";
            // nudNumPalabras
            this.nudNumPalabras.Location = new System.Drawing.Point(100, 10);
            this.nudNumPalabras.Minimum = 3;
            this.nudNumPalabras.Maximum = 20;
            this.nudNumPalabras.Value = 6;
            // lblSeparador
            this.lblSeparador.AutoSize = true;
            this.lblSeparador.Location = new System.Drawing.Point(10, 42);
            this.lblSeparador.Text = "Separador";
            // txtSeparador
            this.txtSeparador.Location = new System.Drawing.Point(100, 39);
            this.txtSeparador.Text = "-";
            this.txtSeparador.Width = 40;
            // chkMayusInicial
            this.chkMayusInicial.AutoSize = true;
            this.chkMayusInicial.Checked = true;
            this.chkMayusInicial.Location = new System.Drawing.Point(13, 72);
            this.chkMayusInicial.Text = "Mayúsculas iniciales";
            // chkIncluirNumero
            this.chkIncluirNumero.AutoSize = true;
            this.chkIncluirNumero.Checked = true;
            this.chkIncluirNumero.Location = new System.Drawing.Point(170, 72);
            this.chkIncluirNumero.Text = "Incluir número";
            // Añadir a tabFrase
            this.tabFrase.Controls.Add(this.lblNumPalabras);
            this.tabFrase.Controls.Add(this.nudNumPalabras);
            this.tabFrase.Controls.Add(this.lblSeparador);
            this.tabFrase.Controls.Add(this.txtSeparador);
            this.tabFrase.Controls.Add(this.chkMayusInicial);
            this.tabFrase.Controls.Add(this.chkIncluirNumero);
            // btnGenerar
            this.btnGenerar.Location = new System.Drawing.Point(12, 185);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(90, 28);
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new EventHandler(this.btnGenerar_Click);
            // btnUsar
            this.btnUsar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnUsar.Location = new System.Drawing.Point(296, 225);
            this.btnUsar.Name = "btnUsar";
            this.btnUsar.Size = new System.Drawing.Size(85, 28);
            this.btnUsar.Text = "Usar";
            this.btnUsar.UseVisualStyleBackColor = true;
            this.btnUsar.Click += new EventHandler(this.btnUsar_Click);
            // btnCancelar
            this.btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancelar.Location = new System.Drawing.Point(387, 225);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 28);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new EventHandler(this.btnCancelar_Click);
            // GeneradorForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 265);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnUsar);
            this.Controls.Add(this.btnGenerar);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneradorForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Generador de contraseñas";
            ((System.ComponentModel.ISupportInitialize)(this.nudLongitud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumPalabras)).EndInit();
            this.tabs.ResumeLayout(false);
            this.tabContrasena.ResumeLayout(false);
            this.tabContrasena.PerformLayout();
            this.tabFrase.ResumeLayout(false);
            this.tabFrase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
