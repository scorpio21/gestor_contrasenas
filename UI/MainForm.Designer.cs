namespace GestorContrasenas.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView lvEntradas;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colServicio;
        private System.Windows.Forms.ColumnHeader colUsuario;
        private System.Windows.Forms.ColumnHeader colSecreto;
        private System.Windows.Forms.ColumnHeader colVer;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtSecreto;
        private System.Windows.Forms.Label lblServicio;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblSecreto;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnVerSecreto;
        private System.Windows.Forms.Button btnGenerarSecreto;
        private System.Windows.Forms.Label lblLoginUrl;
        private System.Windows.Forms.TextBox txtLoginUrl;
        private System.Windows.Forms.Button btnAbrirSitio;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnExportar;

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
            this.lvEntradas = new System.Windows.Forms.ListView();
            this.colId = new System.Windows.Forms.ColumnHeader();
            this.colServicio = new System.Windows.Forms.ColumnHeader();
            this.colUsuario = new System.Windows.Forms.ColumnHeader();
            this.colSecreto = new System.Windows.Forms.ColumnHeader();
            this.txtServicio = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtSecreto = new System.Windows.Forms.TextBox();
            this.lblServicio = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblSecreto = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnVerSecreto = new System.Windows.Forms.Button();
            this.btnGenerarSecreto = new System.Windows.Forms.Button();
            this.lblLoginUrl = new System.Windows.Forms.Label();
            this.txtLoginUrl = new System.Windows.Forms.TextBox();
            this.btnAbrirSitio = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvEntradas
            // 
            this.lvEntradas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEntradas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colServicio,
            this.colUsuario,
            this.colSecreto,
            this.colVer});
            this.lvEntradas.FullRowSelect = true;
            this.lvEntradas.GridLines = true;
            this.lvEntradas.Location = new System.Drawing.Point(12, 12);
            this.lvEntradas.MultiSelect = false;
            this.lvEntradas.Name = "lvEntradas";
            this.lvEntradas.Size = new System.Drawing.Size(660, 300);
            this.lvEntradas.TabIndex = 0;
            this.lvEntradas.UseCompatibleStateImageBehavior = false;
            this.lvEntradas.View = System.Windows.Forms.View.Details;
            this.lvEntradas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvEntradas_MouseClick);
            // 
            // colId
            // 
            this.colId.Text = "ID";
            this.colId.Width = 50;
            // 
            // colServicio
            // 
            this.colServicio.Text = "Servicio";
            this.colServicio.Width = 180;
            // 
            // colUsuario
            // 
            this.colUsuario.Text = "Usuario";
            this.colUsuario.Width = 180;
            // 
            // colSecreto
            // 
            this.colSecreto.Text = "Contraseña";
            this.colSecreto.Width = 200;
            // 
            // colVer
            // 
            this.colVer.Text = "";
            this.colVer.Width = 40;
            // 
            // lblServicio
            // 
            this.lblServicio.AutoSize = true;
            this.lblServicio.Location = new System.Drawing.Point(12, 325);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(50, 15);
            this.lblServicio.TabIndex = 1;
            this.lblServicio.Text = "Servicio";
            // 
            // txtServicio
            // 
            this.txtServicio.Location = new System.Drawing.Point(12, 343);
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.Size = new System.Drawing.Size(200, 23);
            this.txtServicio.TabIndex = 2;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(222, 325);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(47, 15);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(222, 343);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(200, 23);
            this.txtUsuario.TabIndex = 4;
            // 
            // lblSecreto
            // 
            this.lblSecreto.AutoSize = true;
            this.lblSecreto.Location = new System.Drawing.Point(432, 325);
            this.lblSecreto.Name = "lblSecreto";
            this.lblSecreto.Size = new System.Drawing.Size(69, 15);
            this.lblSecreto.TabIndex = 5;
            this.lblSecreto.Text = "Contraseña";
            // 
            // txtSecreto
            // 
            this.txtSecreto.Location = new System.Drawing.Point(432, 343);
            this.txtSecreto.Name = "txtSecreto";
            this.txtSecreto.PasswordChar = '•';
            this.txtSecreto.Size = new System.Drawing.Size(180, 23);
            this.txtSecreto.TabIndex = 6;
            // 
            // btnVerSecreto
            // 
            this.btnVerSecreto.Location = new System.Drawing.Point(618, 343);
            this.btnVerSecreto.Name = "btnVerSecreto";
            this.btnVerSecreto.Size = new System.Drawing.Size(28, 23);
            this.btnVerSecreto.TabIndex = 6;
            this.btnVerSecreto.Text = "👁";
            this.btnVerSecreto.UseVisualStyleBackColor = true;
            this.btnVerSecreto.Click += new System.EventHandler(this.btnVerSecreto_Click);
            // 
            // btnGenerarSecreto
            // 
            this.btnGenerarSecreto.Location = new System.Drawing.Point(648, 343);
            this.btnGenerarSecreto.Name = "btnGenerarSecreto";
            this.btnGenerarSecreto.Size = new System.Drawing.Size(28, 23);
            this.btnGenerarSecreto.TabIndex = 6;
            this.btnGenerarSecreto.Text = "🎲";
            this.btnGenerarSecreto.UseVisualStyleBackColor = true;
            this.btnGenerarSecreto.Click += new System.EventHandler(this.btnGenerarSecreto_Click);
            // 
            // lblLoginUrl
            // 
            this.lblLoginUrl.AutoSize = true;
            this.lblLoginUrl.Location = new System.Drawing.Point(12, 372);
            this.lblLoginUrl.Name = "lblLoginUrl";
            this.lblLoginUrl.Size = new System.Drawing.Size(63, 15);
            this.lblLoginUrl.TabIndex = 12;
            this.lblLoginUrl.Text = "URL login";
            // 
            // txtLoginUrl
            // 
            this.txtLoginUrl.Location = new System.Drawing.Point(12, 390);
            this.txtLoginUrl.Name = "txtLoginUrl";
            this.txtLoginUrl.Size = new System.Drawing.Size(440, 23);
            this.txtLoginUrl.TabIndex = 13;
            // 
            // btnAbrirSitio
            // 
            this.btnAbrirSitio.Location = new System.Drawing.Point(458, 389);
            this.btnAbrirSitio.Name = "btnAbrirSitio";
            this.btnAbrirSitio.Size = new System.Drawing.Size(100, 28);
            this.btnAbrirSitio.TabIndex = 14;
            this.btnAbrirSitio.Text = "Abrir sitio";
            this.btnAbrirSitio.UseVisualStyleBackColor = true;
            this.btnAbrirSitio.Click += new System.EventHandler(this.btnAbrirSitio_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(12, 425);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 28);
            this.btnAgregar.TabIndex = 15;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(118, 425);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 28);
            this.btnEliminar.TabIndex = 16;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCopiar
            // 
            this.btnCopiar.Location = new System.Drawing.Point(224, 425);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(100, 28);
            this.btnCopiar.TabIndex = 17;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Location = new System.Drawing.Point(330, 425);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(100, 28);
            this.btnRefrescar.TabIndex = 18;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(436, 425);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(100, 28);
            this.btnImportar.TabIndex = 19;
            this.btnImportar.Text = "Importar CSV";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(542, 425);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(100, 28);
            this.btnExportar.TabIndex = 20;
            this.btnExportar.Text = "Exportar CSV";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(12, 465);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 15);
            this.lblEstado.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 495);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.btnAbrirSitio);
            this.Controls.Add(this.txtLoginUrl);
            this.Controls.Add(this.lblLoginUrl);
            this.Controls.Add(this.btnGenerarSecreto);
            this.Controls.Add(this.btnVerSecreto);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtSecreto);
            this.Controls.Add(this.lblSecreto);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtServicio);
            this.Controls.Add(this.lblServicio);
            this.Controls.Add(this.lvEntradas);
            this.MinimumSize = new System.Drawing.Size(700, 540);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestor de Contraseñas";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
