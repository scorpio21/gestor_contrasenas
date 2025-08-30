using System;
using System.Windows.Forms;

namespace GestorContrasenas.UI
{
    partial class RegistroForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private Label lblClaveMaestra;
        private TextBox txtClaveMaestra;
        private Label lblClaveMaestraConf;
        private TextBox txtClaveMaestraConf;
        private Button btnCrear;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblConfirmar = new Label();
            this.txtConfirmar = new TextBox();
            this.lblClaveMaestra = new Label();
            this.txtClaveMaestra = new TextBox();
            this.lblClaveMaestraConf = new Label();
            this.txtClaveMaestraConf = new TextBox();
            this.btnCrear = new Button();
            this.btnCancelar = new Button();
            this.SuspendLayout();
            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 15);
            this.lblEmail.Text = "Email";
            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(15, 33);
            this.txtEmail.Size = new System.Drawing.Size(310, 23);
            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 65);
            this.lblPassword.Text = "Contraseña";
            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(15, 83);
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(310, 23);
            // lblConfirmar
            this.lblConfirmar.AutoSize = true;
            this.lblConfirmar.Location = new System.Drawing.Point(12, 115);
            this.lblConfirmar.Text = "Confirmar contraseña";
            // txtConfirmar
            this.txtConfirmar.Location = new System.Drawing.Point(15, 133);
            this.txtConfirmar.PasswordChar = '•';
            this.txtConfirmar.Size = new System.Drawing.Size(310, 23);
            // lblClaveMaestra
            this.lblClaveMaestra.AutoSize = true;
            this.lblClaveMaestra.Location = new System.Drawing.Point(12, 173);
            this.lblClaveMaestra.Text = "Clave maestra";
            // txtClaveMaestra
            this.txtClaveMaestra.Location = new System.Drawing.Point(15, 191);
            this.txtClaveMaestra.PasswordChar = '•';
            this.txtClaveMaestra.Size = new System.Drawing.Size(310, 23);
            // lblClaveMaestraConf
            this.lblClaveMaestraConf.AutoSize = true;
            this.lblClaveMaestraConf.Location = new System.Drawing.Point(12, 223);
            this.lblClaveMaestraConf.Text = "Confirmar clave maestra";
            // txtClaveMaestraConf
            this.txtClaveMaestraConf.Location = new System.Drawing.Point(15, 241);
            this.txtClaveMaestraConf.PasswordChar = '•';
            this.txtClaveMaestraConf.Size = new System.Drawing.Size(310, 23);
            // btnCrear
            this.btnCrear.Location = new System.Drawing.Point(250, 280);
            this.btnCrear.Size = new System.Drawing.Size(75, 27);
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new EventHandler(this.btnCrear_Click);
            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(15, 280);
            this.btnCancelar.Size = new System.Drawing.Size(75, 27);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new EventHandler(this.btnCancelar_Click);
            // RegistroForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 323);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.txtClaveMaestraConf);
            this.Controls.Add(this.lblClaveMaestraConf);
            this.Controls.Add(this.txtClaveMaestra);
            this.Controls.Add(this.lblClaveMaestra);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.lblConfirmar);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegistroForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Crear cuenta";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
