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
        private System.Windows.Forms.ToolStripMenuItem exportarComprometidasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verAyudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.Panel pnlFortaleza;
        private System.Windows.Forms.Panel pnlFortalezaValor;
        private System.Windows.Forms.CheckBox chkHibpAuto;
        private System.Windows.Forms.ContextMenuStrip contextMenuEntradas;
        private System.Windows.Forms.ToolStripMenuItem abrirSitioCambiarToolStripMenuItem;
        private System.Windows.Forms.Button btnGuardar;

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
            components = new System.ComponentModel.Container();
            lvEntradas = new ListView();
            colId = new ColumnHeader();
            colServicio = new ColumnHeader();
            colUsuario = new ColumnHeader();
            colSecreto = new ColumnHeader();
            colComprometida = new ColumnHeader();
            colVer = new ColumnHeader();
            contextMenuEntradas = new ContextMenuStrip(components);
            abrirSitioCambiarToolStripMenuItem = new ToolStripMenuItem();
            txtServicio = new TextBox();
            txtUsuario = new TextBox();
            txtSecreto = new TextBox();
            lblServicio = new Label();
            lblUsuario = new Label();
            lblSecreto = new Label();
            btnAgregar = new Button();
            btnEliminar = new Button();
            btnCopiar = new Button();
            btnRefrescar = new Button();
            btnGuardar = new Button();
            lblEstado = new Label();
            btnVerSecreto = new Button();
            btnGenerarSecreto = new Button();
            lblLoginUrl = new Label();
            txtLoginUrl = new TextBox();
            btnAbrirSitio = new Button();
            btnImportar = new Button();
            btnExportar = new Button();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            autoLockTimer = new System.Windows.Forms.Timer(components);
            clipboardTimer = new System.Windows.Forms.Timer(components);
            revealTimer = new System.Windows.Forms.Timer(components);
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            exportarComprometidasToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
            verAyudaToolStripMenuItem = new ToolStripMenuItem();
            acercaDeToolStripMenuItem = new ToolStripMenuItem();
            pnlFortaleza = new Panel();
            pnlFortalezaValor = new Panel();
            chkHibpAuto = new CheckBox();
            lblFortaleza = new Label();
            prgFortaleza = new ProgressBar();
            lblReutilizacion = new Label();
            btnImportarSeguro = new Button();
            btnExportarSeguro = new Button();
            contextMenuEntradas.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lvEntradas
            // 
            lvEntradas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvEntradas.Columns.AddRange(new ColumnHeader[] { colId, colServicio, colUsuario, colSecreto, colComprometida, colVer });
            lvEntradas.ContextMenuStrip = contextMenuEntradas;
            lvEntradas.FullRowSelect = true;
            lvEntradas.GridLines = true;
            lvEntradas.Location = new Point(17, 45);
            lvEntradas.Margin = new Padding(4, 5, 4, 5);
            lvEntradas.MultiSelect = false;
            lvEntradas.Name = "lvEntradas";
            lvEntradas.Size = new Size(1182, 497);
            lvEntradas.TabIndex = 0;
            lvEntradas.UseCompatibleStateImageBehavior = false;
            lvEntradas.View = View.Details;
            lvEntradas.SelectedIndexChanged += lvEntradas_SelectedIndexChanged;
            lvEntradas.Leave += lvEntradas_Leave;
            lvEntradas.MouseClick += lvEntradas_MouseClick;
            // 
            // colId
            // 
            colId.Text = "ID";
            colId.Width = 50;
            // 
            // colServicio
            // 
            colServicio.Text = "Servicio";
            colServicio.Width = 180;
            // 
            // colUsuario
            // 
            colUsuario.Text = "Usuario";
            colUsuario.Width = 180;
            // 
            // colSecreto
            // 
            colSecreto.Text = "Contraseña";
            colSecreto.Width = 200;
            // 
            // colComprometida
            // 
            colComprometida.Text = "Comprometida";
            colComprometida.Width = 110;
            // 
            // colVer
            // 
            colVer.Text = "";
            colVer.Width = 40;
            // 
            // contextMenuEntradas
            // 
            contextMenuEntradas.ImageScalingSize = new Size(24, 24);
            contextMenuEntradas.Items.AddRange(new ToolStripItem[] { abrirSitioCambiarToolStripMenuItem });
            contextMenuEntradas.Name = "contextMenuEntradas";
            contextMenuEntradas.Size = new Size(361, 36);
            contextMenuEntradas.Opening += contextMenuEntradas_Opening;
            // 
            // abrirSitioCambiarToolStripMenuItem
            // 
            abrirSitioCambiarToolStripMenuItem.Name = "abrirSitioCambiarToolStripMenuItem";
            abrirSitioCambiarToolStripMenuItem.Size = new Size(360, 32);
            abrirSitioCambiarToolStripMenuItem.Text = "Abrir sitio para cambiar contraseña";
            abrirSitioCambiarToolStripMenuItem.Click += abrirSitioCambiarToolStripMenuItem_Click;
            // 
            // txtServicio
            // 
            txtServicio.Location = new Point(17, 630);
            txtServicio.Margin = new Padding(4, 5, 4, 5);
            txtServicio.Name = "txtServicio";
            txtServicio.Size = new Size(284, 31);
            txtServicio.TabIndex = 2;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(317, 630);
            txtUsuario.Margin = new Padding(4, 5, 4, 5);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(284, 31);
            txtUsuario.TabIndex = 4;
            // 
            // txtSecreto
            // 
            txtSecreto.Location = new Point(617, 630);
            txtSecreto.Margin = new Padding(4, 5, 4, 5);
            txtSecreto.Name = "txtSecreto";
            txtSecreto.PasswordChar = '•';
            txtSecreto.Size = new Size(255, 31);
            txtSecreto.TabIndex = 6;
            txtSecreto.TextChanged += txtSecreto_TextChanged;
            // 
            // lblServicio
            // 
            lblServicio.AutoSize = true;
            lblServicio.Location = new Point(17, 600);
            lblServicio.Margin = new Padding(4, 0, 4, 0);
            lblServicio.Name = "lblServicio";
            lblServicio.Size = new Size(73, 25);
            lblServicio.TabIndex = 1;
            lblServicio.Text = "Servicio";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(317, 600);
            lblUsuario.Margin = new Padding(4, 0, 4, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(72, 25);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuario";
            // 
            // lblSecreto
            // 
            lblSecreto.AutoSize = true;
            lblSecreto.Location = new Point(617, 600);
            lblSecreto.Margin = new Padding(4, 0, 4, 0);
            lblSecreto.Name = "lblSecreto";
            lblSecreto.Size = new Size(101, 25);
            lblSecreto.TabIndex = 5;
            lblSecreto.Text = "Contraseña";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(16, 796);
            btnAgregar.Margin = new Padding(4, 5, 4, 5);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(143, 47);
            btnAgregar.TabIndex = 15;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(356, 796);
            btnEliminar.Margin = new Padding(4, 5, 4, 5);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(143, 47);
            btnEliminar.TabIndex = 16;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCopiar
            // 
            btnCopiar.Location = new Point(516, 796);
            btnCopiar.Margin = new Padding(4, 5, 4, 5);
            btnCopiar.Name = "btnCopiar";
            btnCopiar.Size = new Size(143, 47);
            btnCopiar.TabIndex = 17;
            btnCopiar.Text = "Copiar";
            btnCopiar.UseVisualStyleBackColor = true;
            btnCopiar.Click += btnCopiar_Click;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(667, 796);
            btnRefrescar.Margin = new Padding(4, 5, 4, 5);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(143, 47);
            btnRefrescar.TabIndex = 18;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(176, 796);
            btnGuardar.Margin = new Padding(4, 5, 4, 5);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(171, 47);
            btnGuardar.TabIndex = 15;
            btnGuardar.Text = "Guardar cambios";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lblEstado
            // 
            lblEstado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(17, 858);
            lblEstado.Margin = new Padding(4, 0, 4, 0);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(0, 25);
            lblEstado.TabIndex = 11;
            // 
            // btnVerSecreto
            // 
            btnVerSecreto.Location = new Point(883, 630);
            btnVerSecreto.Margin = new Padding(4, 5, 4, 5);
            btnVerSecreto.Name = "btnVerSecreto";
            btnVerSecreto.Size = new Size(40, 38);
            btnVerSecreto.TabIndex = 6;
            btnVerSecreto.Text = "👁";
            btnVerSecreto.UseVisualStyleBackColor = true;
            btnVerSecreto.Click += btnVerSecreto_Click;
            // 
            // btnGenerarSecreto
            // 
            btnGenerarSecreto.Location = new Point(926, 630);
            btnGenerarSecreto.Margin = new Padding(4, 5, 4, 5);
            btnGenerarSecreto.Name = "btnGenerarSecreto";
            btnGenerarSecreto.Size = new Size(40, 38);
            btnGenerarSecreto.TabIndex = 6;
            btnGenerarSecreto.Text = "🎲";
            btnGenerarSecreto.UseVisualStyleBackColor = true;
            btnGenerarSecreto.Click += btnGenerarSecreto_Click;
            // 
            // lblLoginUrl
            // 
            lblLoginUrl.AutoSize = true;
            lblLoginUrl.Location = new Point(17, 712);
            lblLoginUrl.Margin = new Padding(4, 0, 4, 0);
            lblLoginUrl.Name = "lblLoginUrl";
            lblLoginUrl.Size = new Size(88, 25);
            lblLoginUrl.TabIndex = 12;
            lblLoginUrl.Text = "URL login";
            // 
            // txtLoginUrl
            // 
            txtLoginUrl.Location = new Point(17, 742);
            txtLoginUrl.Margin = new Padding(4, 5, 4, 5);
            txtLoginUrl.Name = "txtLoginUrl";
            txtLoginUrl.Size = new Size(627, 31);
            txtLoginUrl.TabIndex = 13;
            // 
            // btnAbrirSitio
            // 
            btnAbrirSitio.Location = new Point(654, 741);
            btnAbrirSitio.Margin = new Padding(4, 5, 4, 5);
            btnAbrirSitio.Name = "btnAbrirSitio";
            btnAbrirSitio.Size = new Size(143, 47);
            btnAbrirSitio.TabIndex = 14;
            btnAbrirSitio.Text = "Abrir sitio";
            btnAbrirSitio.UseVisualStyleBackColor = true;
            btnAbrirSitio.Click += btnAbrirSitio_Click;
            // 
            // btnImportar
            // 
            btnImportar.Location = new Point(967, 796);
            btnImportar.Margin = new Padding(4, 5, 4, 5);
            btnImportar.Name = "btnImportar";
            btnImportar.Size = new Size(143, 47);
            btnImportar.TabIndex = 19;
            btnImportar.Text = "Importar CSV";
            btnImportar.UseVisualStyleBackColor = true;
            btnImportar.Click += btnImportar_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(816, 796);
            btnExportar.Margin = new Padding(4, 5, 4, 5);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(143, 47);
            btnExportar.TabIndex = 20;
            btnExportar.Text = "Exportar CSV";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(17, 555);
            lblBuscar.Margin = new Padding(4, 0, 4, 0);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(63, 25);
            lblBuscar.TabIndex = 26;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(89, 550);
            txtBuscar.Margin = new Padding(4, 5, 4, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(427, 31);
            txtBuscar.TabIndex = 27;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // autoLockTimer
            // 
            autoLockTimer.Interval = 300000;
            autoLockTimer.Tick += autoLockTimer_Tick;
            // 
            // clipboardTimer
            // 
            clipboardTimer.Interval = 20000;
            clipboardTimer.Tick += clipboardTimer_Tick;
            // 
            // revealTimer
            // 
            revealTimer.Interval = 8000;
            revealTimer.Tick += revealTimer_Tick;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, ayudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(9, 3, 0, 3);
            menuStrip1.AutoSize = false;
            menuStrip1.Size = new Size(1218, 36);
            menuStrip1.Dock = DockStyle.Top;
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(91, 29);
            archivoToolStripMenuItem.Text = "Archivo";
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                exportarComprometidasToolStripMenuItem,
                cerrarSesionToolStripMenuItem,
                salirToolStripMenuItem
            });
            // 
            // exportarComprometidasToolStripMenuItem
            // 
            exportarComprometidasToolStripMenuItem.Name = "exportarComprometidasToolStripMenuItem";
            exportarComprometidasToolStripMenuItem.Size = new Size(395, 34);
            exportarComprometidasToolStripMenuItem.Text = "Exportar comprometidas a CSV";
            exportarComprometidasToolStripMenuItem.Click += exportarComprometidasToolStripMenuItem_Click;
            // 
            // cerrarSesionToolStripMenuItem
            // 
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesionToolStripMenuItem.Size = new Size(395, 34);
            cerrarSesionToolStripMenuItem.Text = "Cerrar sesión";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(395, 34);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(85, 29);
            ayudaToolStripMenuItem.Text = "Ayuda";
            ayudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                verAyudaToolStripMenuItem,
                acercaDeToolStripMenuItem
            });
            // 
            // verAyudaToolStripMenuItem
            // 
            verAyudaToolStripMenuItem.Name = "verAyudaToolStripMenuItem";
            verAyudaToolStripMenuItem.Size = new Size(214, 34);
            verAyudaToolStripMenuItem.Text = "Ver ayuda";
            verAyudaToolStripMenuItem.ShortcutKeys = Keys.F1;
            verAyudaToolStripMenuItem.Click += verAyudaToolStripMenuItem_Click;
            // 
            // acercaDeToolStripMenuItem
            // 
            acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            acercaDeToolStripMenuItem.Size = new Size(214, 34);
            acercaDeToolStripMenuItem.Text = "Acerca de…";
            acercaDeToolStripMenuItem.Click += acercaDeToolStripMenuItem_Click;
            pnlFortaleza.Margin = new Padding(4, 5, 4, 5);
            pnlFortaleza.Name = "pnlFortaleza";
            pnlFortaleza.Size = new Size(171, 20);
            pnlFortaleza.TabIndex = 26;
            // 
            // pnlFortalezaValor
            // 
            pnlFortalezaValor.BackColor = Color.Red;
            pnlFortalezaValor.Location = new Point(800, 670);
            pnlFortalezaValor.Margin = new Padding(4, 5, 4, 5);
            pnlFortalezaValor.Name = "pnlFortalezaValor";
            pnlFortalezaValor.Size = new Size(0, 20);
            pnlFortalezaValor.TabIndex = 27;
            // 
            // chkHibpAuto
            // 
            chkHibpAuto.AutoSize = true;
            chkHibpAuto.Checked = true;
            chkHibpAuto.CheckState = CheckState.Checked;
            chkHibpAuto.Location = new Point(543, 550);
            chkHibpAuto.Margin = new Padding(4, 5, 4, 5);
            chkHibpAuto.Name = "chkHibpAuto";
            chkHibpAuto.Size = new Size(396, 29);
            chkHibpAuto.TabIndex = 28;
            chkHibpAuto.Text = "Comprobar comprometida automáticamente";
            chkHibpAuto.UseVisualStyleBackColor = true;
            chkHibpAuto.CheckedChanged += chkHibpAuto_CheckedChanged;
            // 
            // lblFortaleza
            // 
            lblFortaleza.AutoSize = true;
            lblFortaleza.Location = new Point(617, 670);
            lblFortaleza.Margin = new Padding(4, 0, 4, 0);
            lblFortaleza.Name = "lblFortaleza";
            lblFortaleza.Size = new Size(134, 25);
            lblFortaleza.TabIndex = 21;
            lblFortaleza.Text = "Fortaleza: (N/A)";
            // 
            // prgFortaleza
            // 
            prgFortaleza.Location = new Point(617, 670);
            prgFortaleza.Margin = new Padding(4, 5, 4, 5);
            prgFortaleza.Maximum = 4;
            prgFortaleza.Name = "prgFortaleza";
            prgFortaleza.Size = new Size(171, 33);
            prgFortaleza.Step = 1;
            prgFortaleza.TabIndex = 22;
            prgFortaleza.Visible = false;
            // 
            // lblReutilizacion
            // 
            lblReutilizacion.AutoSize = true;
            lblReutilizacion.ForeColor = Color.DarkRed;
            lblReutilizacion.Location = new Point(17, 787);
            lblReutilizacion.Margin = new Padding(4, 0, 4, 0);
            lblReutilizacion.Name = "lblReutilizacion";
            lblReutilizacion.Size = new Size(0, 25);
            lblReutilizacion.TabIndex = 23;
            // 
            // btnImportarSeguro
            // 
            btnImportarSeguro.Location = new Point(16, 852);
            btnImportarSeguro.Margin = new Padding(4, 5, 4, 5);
            btnImportarSeguro.Name = "btnImportarSeguro";
            btnImportarSeguro.Size = new Size(200, 47);
            btnImportarSeguro.TabIndex = 24;
            btnImportarSeguro.Text = "Importar seguro";
            btnImportarSeguro.UseVisualStyleBackColor = true;
            btnImportarSeguro.Click += btnImportarSeguro_Click;
            // 
            // btnExportarSeguro
            // 
            btnExportarSeguro.Location = new Point(233, 852);
            btnExportarSeguro.Margin = new Padding(4, 5, 4, 5);
            btnExportarSeguro.Name = "btnExportarSeguro";
            btnExportarSeguro.Size = new Size(200, 47);
            btnExportarSeguro.TabIndex = 25;
            btnExportarSeguro.Text = "Exportar seguro";
            btnExportarSeguro.UseVisualStyleBackColor = true;
            btnExportarSeguro.Click += btnExportarSeguro_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1218, 917);
            // Agregar primero el menu para que reserve el espacio superior
            Controls.Add(menuStrip1);
            Controls.Add(chkHibpAuto);
            Controls.Add(pnlFortalezaValor);
            Controls.Add(pnlFortaleza);
            Controls.Add(btnExportarSeguro);
            Controls.Add(btnImportarSeguro);
            Controls.Add(lblReutilizacion);
            Controls.Add(prgFortaleza);
            Controls.Add(lblFortaleza);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Controls.Add(btnExportar);
            Controls.Add(btnImportar);
            Controls.Add(btnAbrirSitio);
            Controls.Add(txtLoginUrl);
            Controls.Add(lblLoginUrl);
            Controls.Add(btnGenerarSecreto);
            Controls.Add(btnVerSecreto);
            Controls.Add(lblEstado);
            Controls.Add(btnRefrescar);
            Controls.Add(btnCopiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnAgregar);
            Controls.Add(txtSecreto);
            Controls.Add(lblSecreto);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(txtServicio);
            Controls.Add(lblServicio);
            Controls.Add(lvEntradas);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(991, 863);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestor de Contraseñas";
            contextMenuEntradas.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
