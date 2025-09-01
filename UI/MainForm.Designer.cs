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
        private System.Windows.Forms.ColumnHeader colComprometida;
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
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Timer autoLockTimer;
        private System.Windows.Forms.Timer clipboardTimer;
        private System.Windows.Forms.Timer revealTimer;
        private System.Windows.Forms.Label lblFortaleza;
        private System.Windows.Forms.ProgressBar prgFortaleza;
        private System.Windows.Forms.Label lblReutilizacion;
        private System.Windows.Forms.Button btnExportarSeguro;
        private System.Windows.Forms.Button btnImportarSeguro;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Panel pnlFortaleza;
        private System.Windows.Forms.Panel pnlFortalezaValor;
        private System.Windows.Forms.CheckBox chkHibpAuto;

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
            this.components = new System.ComponentModel.Container();
            this.lvEntradas = new System.Windows.Forms.ListView();
            this.colId = new System.Windows.Forms.ColumnHeader();
            this.colServicio = new System.Windows.Forms.ColumnHeader();
            this.colUsuario = new System.Windows.Forms.ColumnHeader();
            this.colSecreto = new System.Windows.Forms.ColumnHeader();
            this.colComprometida = new System.Windows.Forms.ColumnHeader();
            this.colVer = new System.Windows.Forms.ColumnHeader();
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
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.autoLockTimer = new System.Windows.Forms.Timer(this.components);
            this.clipboardTimer = new System.Windows.Forms.Timer(this.components);
            this.revealTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlFortaleza = new System.Windows.Forms.Panel();
            this.pnlFortalezaValor = new System.Windows.Forms.Panel();
            this.chkHibpAuto = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 100;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
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
            this.colComprometida,
            this.colVer});
            this.lvEntradas.FullRowSelect = true;
            this.lvEntradas.GridLines = true;
            this.lvEntradas.Location = new System.Drawing.Point(12, 27);
            this.lvEntradas.MultiSelect = false;
            this.lvEntradas.Name = "lvEntradas";
            this.lvEntradas.Size = new System.Drawing.Size(660, 300);
            this.lvEntradas.TabIndex = 0;
            this.lvEntradas.UseCompatibleStateImageBehavior = false;
            this.lvEntradas.View = System.Windows.Forms.View.Details;
            this.lvEntradas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvEntradas_MouseClick);
            this.lvEntradas.SelectedIndexChanged += new System.EventHandler(this.lvEntradas_SelectedIndexChanged);
            this.lvEntradas.Leave += new System.EventHandler(this.lvEntradas_Leave);
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
            // colComprometida
            // 
            this.colComprometida.Text = "Comprometida";
            this.colComprometida.Width = 110;
            // 
            // colVer
            // 
            this.colVer.Text = "";
            this.colVer.Width = 40;
            // 
            // lblServicio
            // 
            this.lblServicio.AutoSize = true;
            this.lblServicio.Location = new System.Drawing.Point(12, 360);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(50, 15);
            this.lblServicio.TabIndex = 1;
            this.lblServicio.Text = "Servicio";
            // 
            // txtServicio
            // 
            this.txtServicio.Location = new System.Drawing.Point(12, 378);
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.Size = new System.Drawing.Size(200, 23);
            this.txtServicio.TabIndex = 2;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(222, 360);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(47, 15);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(222, 378);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(200, 23);
            this.txtUsuario.TabIndex = 4;
            // 
            // lblSecreto
            // 
            this.lblSecreto.AutoSize = true;
            this.lblSecreto.Location = new System.Drawing.Point(432, 360);
            this.lblSecreto.Name = "lblSecreto";
            this.lblSecreto.Size = new System.Drawing.Size(69, 15);
            this.lblSecreto.TabIndex = 5;
            this.lblSecreto.Text = "Contraseña";
            // 
            // txtSecreto
            // 
            this.txtSecreto.Location = new System.Drawing.Point(432, 378);
            this.txtSecreto.Name = "txtSecreto";
            this.txtSecreto.PasswordChar = '•';
            this.txtSecreto.Size = new System.Drawing.Size(180, 23);
            this.txtSecreto.TabIndex = 6;
            this.txtSecreto.TextChanged += new System.EventHandler(this.txtSecreto_TextChanged);
            // 
            // btnVerSecreto
            // 
            this.btnVerSecreto.Location = new System.Drawing.Point(618, 378);
            this.btnVerSecreto.Name = "btnVerSecreto";
            this.btnVerSecreto.Size = new System.Drawing.Size(28, 23);
            this.btnVerSecreto.TabIndex = 6;
            this.btnVerSecreto.Text = "👁";
            this.btnVerSecreto.UseVisualStyleBackColor = true;
            this.btnVerSecreto.Click += new System.EventHandler(this.btnVerSecreto_Click);
            // 
            // btnGenerarSecreto
            // 
            this.btnGenerarSecreto.Location = new System.Drawing.Point(648, 378);
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
            this.lblLoginUrl.Location = new System.Drawing.Point(12, 407);
            this.lblLoginUrl.Name = "lblLoginUrl";
            this.lblLoginUrl.Size = new System.Drawing.Size(63, 15);
            this.lblLoginUrl.TabIndex = 12;
            this.lblLoginUrl.Text = "URL login";
            // 
            // txtLoginUrl
            // 
            this.txtLoginUrl.Location = new System.Drawing.Point(12, 425);
            this.txtLoginUrl.Name = "txtLoginUrl";
            this.txtLoginUrl.Size = new System.Drawing.Size(440, 23);
            this.txtLoginUrl.TabIndex = 13;
            // 
            // btnAbrirSitio
            // 
            this.btnAbrirSitio.Location = new System.Drawing.Point(458, 424);
            this.btnAbrirSitio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbrirSitio.Name = "btnAbrirSitio";
            this.btnAbrirSitio.Size = new System.Drawing.Size(100, 28);
            this.btnAbrirSitio.TabIndex = 14;
            this.btnAbrirSitio.Text = "Abrir sitio";
            this.btnAbrirSitio.UseVisualStyleBackColor = true;
            this.btnAbrirSitio.Click += new System.EventHandler(this.btnAbrirSitio_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(12, 460);
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 28);
            this.btnAgregar.TabIndex = 15;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(124, 460);
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 28);
            this.btnEliminar.TabIndex = 16;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCopiar
            // 
            this.btnCopiar.Location = new System.Drawing.Point(236, 460);
            this.btnCopiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(100, 28);
            this.btnCopiar.TabIndex = 17;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Location = new System.Drawing.Point(348, 460);
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(100, 28);
            this.btnRefrescar.TabIndex = 18;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(460, 460);
            this.btnImportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(100, 28);
            this.btnImportar.TabIndex = 19;
            this.btnImportar.Text = "Importar CSV";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(572, 460);
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(100, 28);
            this.btnExportar.TabIndex = 20;
            this.btnExportar.Text = "Exportar CSV";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // lblBuscar
            // 
            this.lblBuscar = new System.Windows.Forms.Label();
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(12, 333);
            this.lblBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(44, 15);
            this.lblBuscar.TabIndex = 26;
            this.lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.txtBuscar.Location = new System.Drawing.Point(62, 330);
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 23);
            this.txtBuscar.TabIndex = 27;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblFortaleza
            // 
            this.lblFortaleza = new System.Windows.Forms.Label();
            this.lblFortaleza.AutoSize = true;
            this.lblFortaleza.Location = new System.Drawing.Point(432, 402);
            this.lblFortaleza.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFortaleza.Name = "lblFortaleza";
            this.lblFortaleza.Size = new System.Drawing.Size(105, 15);
            this.lblFortaleza.TabIndex = 21;
            this.lblFortaleza.Text = "Fortaleza: (N/A)";
            // 
            // prgFortaleza
            // 
            this.prgFortaleza = new System.Windows.Forms.ProgressBar();
            this.prgFortaleza.Location = new System.Drawing.Point(432, 402);
            this.prgFortaleza.Maximum = 4;
            this.prgFortaleza.Name = "prgFortaleza";
            this.prgFortaleza.Size = new System.Drawing.Size(120, 20);
            this.prgFortaleza.Step = 1;
            this.prgFortaleza.TabIndex = 22;
            this.prgFortaleza.Visible = false;
            // 
            // pnlFortaleza
            // 
            this.pnlFortaleza.Location = new System.Drawing.Point(560, 402);
            this.pnlFortaleza.Name = "pnlFortaleza";
            this.pnlFortaleza.Size = new System.Drawing.Size(120, 12);
            this.pnlFortaleza.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlFortaleza.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlFortaleza.TabIndex = 26;
            // 
            // pnlFortalezaValor
            // 
            this.pnlFortalezaValor.Location = new System.Drawing.Point(560, 402);
            this.pnlFortalezaValor.Name = "pnlFortalezaValor";
            this.pnlFortalezaValor.Size = new System.Drawing.Size(0, 12);
            this.pnlFortalezaValor.BackColor = System.Drawing.Color.Red;
            this.pnlFortalezaValor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlFortalezaValor.TabIndex = 27;
            // 
            // chkHibpAuto
            // 
            this.chkHibpAuto.AutoSize = true;
            this.chkHibpAuto.Location = new System.Drawing.Point(12, 330);
            this.chkHibpAuto.Name = "chkHibpAuto";
            this.chkHibpAuto.Size = new System.Drawing.Size(256, 19);
            this.chkHibpAuto.Text = "Comprobar comprometida automáticamente";
            this.chkHibpAuto.Checked = true;
            this.chkHibpAuto.TabIndex = 28;
            this.chkHibpAuto.UseVisualStyleBackColor = true;
            this.chkHibpAuto.CheckedChanged += new System.EventHandler(this.chkHibpAuto_CheckedChanged);
            // 
            // lblReutilizacion
            // 
            this.lblReutilizacion = new System.Windows.Forms.Label();
            this.lblReutilizacion.AutoSize = true;
            this.lblReutilizacion.ForeColor = System.Drawing.Color.DarkRed;
            this.lblReutilizacion.Location = new System.Drawing.Point(12, 472);
            this.lblReutilizacion.Name = "lblReutilizacion";
            this.lblReutilizacion.Size = new System.Drawing.Size(0, 15);
            this.lblReutilizacion.TabIndex = 23;
            // 
            // btnImportarSeguro
            // 
            this.btnImportarSeguro = new System.Windows.Forms.Button();
            this.btnImportarSeguro.Location = new System.Drawing.Point(12, 494);
            this.btnImportarSeguro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportarSeguro.Name = "btnImportarSeguro";
            this.btnImportarSeguro.Size = new System.Drawing.Size(140, 28);
            this.btnImportarSeguro.TabIndex = 24;
            this.btnImportarSeguro.Text = "Importar seguro";
            this.btnImportarSeguro.UseVisualStyleBackColor = true;
            this.btnImportarSeguro.Click += new System.EventHandler(this.btnImportarSeguro_Click);
            // 
            // btnExportarSeguro
            // 
            this.btnExportarSeguro = new System.Windows.Forms.Button();
            this.btnExportarSeguro.Location = new System.Drawing.Point(164, 494);
            this.btnExportarSeguro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportarSeguro.Name = "btnExportarSeguro";
            this.btnExportarSeguro.Size = new System.Drawing.Size(140, 28);
            this.btnExportarSeguro.TabIndex = 25;
            this.btnExportarSeguro.Text = "Exportar seguro";
            this.btnExportarSeguro.UseVisualStyleBackColor = true;
            this.btnExportarSeguro.Click += new System.EventHandler(this.btnExportarSeguro_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(12, 515);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 15);
            this.lblEstado.TabIndex = 11;
            // 
            // autoLockTimer
            // 
            this.autoLockTimer.Interval = 300000; // 5 minutos
            this.autoLockTimer.Tick += new System.EventHandler(this.autoLockTimer_Tick);
            // 
            // clipboardTimer
            // 
            this.clipboardTimer.Interval = 20000; // 20 segundos
            this.clipboardTimer.Tick += new System.EventHandler(this.clipboardTimer_Tick);
            // 
            // revealTimer
            // 
            this.revealTimer.Interval = 8000; // 8 segundos visible
            this.revealTimer.Tick += new System.EventHandler(this.revealTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 550);
            this.Controls.Add(this.chkHibpAuto);
            this.Controls.Add(this.pnlFortalezaValor);
            this.Controls.Add(this.pnlFortaleza);
            this.Controls.Add(this.btnExportarSeguro);
            this.Controls.Add(this.btnImportarSeguro);
            this.Controls.Add(this.lblReutilizacion);
            this.Controls.Add(this.prgFortaleza);
            this.Controls.Add(this.lblFortaleza);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
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
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(700, 540);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestor de Contraseñas";
            this.MainMenuStrip = this.menuStrip1;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
