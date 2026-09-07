namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    partial class frmBloqueoUnidadXRuta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBloqueoUnidadXRuta));
            this.txtUnidad = new System.Windows.Forms.TextBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.lstTracto = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verDetalleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desbloquearTodasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuscarPlaca = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGuarda = new DevExpress.XtraEditors.SimpleButton();
            this.pVerRegistro = new System.Windows.Forms.Panel();
            this.dtgListaRegistros = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.liberarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaRegistrosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblidPlaca = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtgListarUnidadBloqueada = new DevExpress.XtraGrid.GridControl();
            this.dtgvListarUnidadBloqueada = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnHistoria = new DevExpress.XtraEditors.SimpleButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvRutas = new DevExpress.XtraGrid.GridControl();
            this.dgvRutasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pVerRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaRegistros)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaRegistrosView)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListarUnidadBloqueada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListarUnidadBloqueada)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutasView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUnidad
            // 
            this.txtUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtUnidad.Location = new System.Drawing.Point(74, 52);
            this.txtUnidad.Name = "txtUnidad";
            this.txtUnidad.Size = new System.Drawing.Size(108, 22);
            this.txtUnidad.TabIndex = 0;
            this.txtUnidad.Enter += new System.EventHandler(this.txtUnidad_Enter);
            this.txtUnidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnidad_KeyPress);
            this.txtUnidad.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUnidad_KeyUp);
            this.txtUnidad.Leave += new System.EventHandler(this.txtUnidad_Leave);
            // 
            // txtRuta
            // 
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtRuta.Location = new System.Drawing.Point(159, 177);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(294, 22);
            this.txtRuta.TabIndex = 61;
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            // 
            // lstTracto
            // 
            this.lstTracto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lstTracto.ForeColor = System.Drawing.Color.Navy;
            this.lstTracto.FullRowSelect = true;
            this.lstTracto.GridLines = true;
            this.lstTracto.Location = new System.Drawing.Point(74, 118);
            this.lstTracto.MultiSelect = false;
            this.lstTracto.Name = "lstTracto";
            this.lstTracto.Size = new System.Drawing.Size(309, 173);
            this.lstTracto.TabIndex = 118;
            this.lstTracto.UseCompatibleStateImageBehavior = false;
            this.lstTracto.View = System.Windows.Forms.View.Details;
            this.lstTracto.Visible = false;
            this.lstTracto.Enter += new System.EventHandler(this.lstTracto_Enter);
            this.lstTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTracto_KeyPress);
            this.lstTracto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTracto_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verDetalleToolStripMenuItem,
            this.desbloquearTodasToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 48);
            // 
            // verDetalleToolStripMenuItem
            // 
            this.verDetalleToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.binocular;
            this.verDetalleToolStripMenuItem.Name = "verDetalleToolStripMenuItem";
            this.verDetalleToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.verDetalleToolStripMenuItem.Text = "Ver Detalle";
            this.verDetalleToolStripMenuItem.Click += new System.EventHandler(this.verDetalleToolStripMenuItem_Click);
            // 
            // desbloquearTodasToolStripMenuItem
            // 
            this.desbloquearTodasToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.conductorbloqueado;
            this.desbloquearTodasToolStripMenuItem.Name = "desbloquearTodasToolStripMenuItem";
            this.desbloquearTodasToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.desbloquearTodasToolStripMenuItem.Text = "Desbloquear Todas";
            this.desbloquearTodasToolStripMenuItem.Click += new System.EventHandler(this.desbloquearTodasToolStripMenuItem_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(289, 46);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(37, 37);
            this.btnBuscar.TabIndex = 120;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscarPlaca
            // 
            this.txtBuscarPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtBuscarPlaca.Location = new System.Drawing.Point(139, 55);
            this.txtBuscarPlaca.Name = "txtBuscarPlaca";
            this.txtBuscarPlaca.Size = new System.Drawing.Size(130, 22);
            this.txtBuscarPlaca.TabIndex = 121;
            this.txtBuscarPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarPlaca_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label1.Location = new System.Drawing.Point(22, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 122;
            this.label1.Text = "Placa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.Location = new System.Drawing.Point(15, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 16);
            this.label2.TabIndex = 123;
            this.label2.Text = "Seleccione las Rutas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label3.Location = new System.Drawing.Point(19, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 124;
            this.label3.Text = "Buscar por Placa:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtMotivo.Location = new System.Drawing.Point(74, 92);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(379, 58);
            this.txtMotivo.TabIndex = 127;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label4.Location = new System.Drawing.Point(17, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 128;
            this.label4.Text = "Motivo:";
            // 
            // lblTituloGuia
            // 
            this.lblTituloGuia.BackColor = System.Drawing.Color.LimeGreen;
            this.lblTituloGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGuia.Font = new System.Drawing.Font("MS Reference Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTituloGuia.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTituloGuia.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGuia.Name = "lblTituloGuia";
            this.lblTituloGuia.Size = new System.Drawing.Size(1182, 45);
            this.lblTituloGuia.TabIndex = 129;
            this.lblTituloGuia.Text = "BLOQUEO DE UNIDAD POR RUTA";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGuarda);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 468);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(469, 64);
            this.panel2.TabIndex = 130;
            // 
            // btnGuarda
            // 
            this.btnGuarda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuarda.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnGuarda.Appearance.Options.UseFont = true;
            this.btnGuarda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuarda.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarda.Image")));
            this.btnGuarda.Location = new System.Drawing.Point(181, 14);
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(104, 37);
            this.btnGuarda.TabIndex = 118;
            this.btnGuarda.Text = "GUARDAR";
            this.btnGuarda.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // pVerRegistro
            // 
            this.pVerRegistro.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pVerRegistro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pVerRegistro.Controls.Add(this.dtgListaRegistros);
            this.pVerRegistro.Controls.Add(this.lblidPlaca);
            this.pVerRegistro.Controls.Add(this.label12);
            this.pVerRegistro.Controls.Add(this.btnCerrar);
            this.pVerRegistro.Location = new System.Drawing.Point(181, 118);
            this.pVerRegistro.Name = "pVerRegistro";
            this.pVerRegistro.Size = new System.Drawing.Size(801, 344);
            this.pVerRegistro.TabIndex = 132;
            this.pVerRegistro.Visible = false;
            this.pVerRegistro.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pVerRegistro_MouseMove);
            // 
            // dtgListaRegistros
            // 
            this.dtgListaRegistros.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgListaRegistros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaRegistros.Location = new System.Drawing.Point(21, 61);
            this.dtgListaRegistros.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            this.dtgListaRegistros.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaRegistros.MainView = this.dgvListaRegistrosView;
            this.dtgListaRegistros.Name = "dtgListaRegistros";
            this.dtgListaRegistros.Size = new System.Drawing.Size(760, 262);
            this.dtgListaRegistros.TabIndex = 131;
            this.dtgListaRegistros.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaRegistrosView});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liberarToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(168, 26);
            // 
            // liberarToolStripMenuItem
            // 
            this.liberarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.liberarToolStripMenuItem.Name = "liberarToolStripMenuItem";
            this.liberarToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.liberarToolStripMenuItem.Text = "Desbloquear Ruta";
            this.liberarToolStripMenuItem.Click += new System.EventHandler(this.liberarToolStripMenuItem_Click);
            // 
            // dgvListaRegistrosView
            // 
            this.dgvListaRegistrosView.GridControl = this.dtgListaRegistros;
            this.dgvListaRegistrosView.Name = "dgvListaRegistrosView";
            this.dgvListaRegistrosView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaRegistrosView.OptionsView.RowAutoHeight = true;
            this.dgvListaRegistrosView.OptionsView.ShowGroupPanel = false;
            // 
            // lblidPlaca
            // 
            this.lblidPlaca.AutoSize = true;
            this.lblidPlaca.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblidPlaca.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblidPlaca.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblidPlaca.Location = new System.Drawing.Point(357, 20);
            this.lblidPlaca.Name = "lblidPlaca";
            this.lblidPlaca.Size = new System.Drawing.Size(94, 24);
            this.lblidPlaca.TabIndex = 34;
            this.lblidPlaca.Text = "T4G-636";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(25, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(335, 24);
            this.label12.TabIndex = 35;
            this.label12.Text = "RUTAS BLOQUEADAS DE PLACA:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(763, 7);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(29, 29);
            this.btnCerrar.TabIndex = 132;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.pCerrar_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.dtgListarUnidadBloqueada);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(469, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(713, 532);
            this.panel3.TabIndex = 133;
            // 
            // dtgListarUnidadBloqueada
            // 
            this.dtgListarUnidadBloqueada.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListarUnidadBloqueada.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListarUnidadBloqueada.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListarUnidadBloqueada.Location = new System.Drawing.Point(0, 97);
            this.dtgListarUnidadBloqueada.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            this.dtgListarUnidadBloqueada.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListarUnidadBloqueada.MainView = this.dtgvListarUnidadBloqueada;
            this.dtgListarUnidadBloqueada.Name = "dtgListarUnidadBloqueada";
            this.dtgListarUnidadBloqueada.Size = new System.Drawing.Size(711, 433);
            this.dtgListarUnidadBloqueada.TabIndex = 134;
            this.dtgListarUnidadBloqueada.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvListarUnidadBloqueada});
            this.dtgListarUnidadBloqueada.DoubleClick += new System.EventHandler(this.dtgListarUnidadBloqueada_DoubleClick);
            // 
            // dtgvListarUnidadBloqueada
            // 
            this.dtgvListarUnidadBloqueada.GridControl = this.dtgListarUnidadBloqueada;
            this.dtgvListarUnidadBloqueada.Name = "dtgvListarUnidadBloqueada";
            this.dtgvListarUnidadBloqueada.OptionsBehavior.Editable = false;
            this.dtgvListarUnidadBloqueada.OptionsView.ColumnAutoWidth = false;
            this.dtgvListarUnidadBloqueada.OptionsView.RowAutoHeight = true;
            this.dtgvListarUnidadBloqueada.OptionsView.ShowGroupPanel = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblTitulo);
            this.panel5.Controls.Add(this.btnBuscar);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.txtBuscarPlaca);
            this.panel5.Controls.Add(this.btnHistoria);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(711, 97);
            this.panel5.TabIndex = 133;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblTitulo.Location = new System.Drawing.Point(17, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(354, 24);
            this.lblTitulo.TabIndex = 132;
            this.lblTitulo.Text = "LISTA DE UNIDADES BLOQUEADAS";
            // 
            // btnHistoria
            // 
            this.btnHistoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistoria.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistoria.Appearance.Options.UseFont = true;
            this.btnHistoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistoria.Image = ((System.Drawing.Image)(resources.GetObject("btnHistoria.Image")));
            this.btnHistoria.Location = new System.Drawing.Point(452, 47);
            this.btnHistoria.Name = "btnHistoria";
            this.btnHistoria.Size = new System.Drawing.Size(104, 37);
            this.btnHistoria.TabIndex = 133;
            this.btnHistoria.Text = "Historial";
            this.btnHistoria.Click += new System.EventHandler(this.btnHistorial_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtMotivo);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dgvRutas);
            this.panel4.Controls.Add(this.txtUnidad);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.txtRuta);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 45);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(469, 532);
            this.panel4.TabIndex = 134;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.LimeGreen;
            this.label5.Location = new System.Drawing.Point(14, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(258, 24);
            this.label5.TabIndex = 133;
            this.label5.Text = "Ingresar Datos de Bloqueo";
            // 
            // dgvRutas
            // 
            this.dgvRutas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvRutas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvRutas.Location = new System.Drawing.Point(0, 212);
            this.dgvRutas.LookAndFeel.SkinName = "Black";
            this.dgvRutas.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dgvRutas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgvRutas.MainView = this.dgvRutasView;
            this.dgvRutas.Name = "dgvRutas";
            this.dgvRutas.Size = new System.Drawing.Size(469, 256);
            this.dgvRutas.TabIndex = 131;
            this.dgvRutas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRutasView});
            // 
            // dgvRutasView
            // 
            this.dgvRutasView.GridControl = this.dgvRutas;
            this.dgvRutasView.Name = "dgvRutasView";
            this.dgvRutasView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvRutasView.OptionsBehavior.Editable = false;
            this.dgvRutasView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvRutasView.OptionsSelection.MultiSelect = true;
            this.dgvRutasView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvRutasView.OptionsView.ColumnAutoWidth = false;
            this.dgvRutasView.OptionsView.ShowFooter = true;
            // 
            // frmBloqueoUnidadXRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1182, 577);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblTituloGuia);
            this.Controls.Add(this.lstTracto);
            this.Controls.Add(this.pVerRegistro);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBloqueoUnidadXRuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bloqueo de Unidad x Ruta";
            this.Load += new System.EventHandler(this.frmBloqueoUnidadXRuta_Load);
            this.Shown += new System.EventHandler(this.frmBloqueoUnidadXRuta_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pVerRegistro.ResumeLayout(false);
            this.pVerRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaRegistros)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaRegistrosView)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListarUnidadBloqueada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListarUnidadBloqueada)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutasView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUnidad;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ListView lstTracto;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TextBox txtBuscarPlaca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem verDetalleToolStripMenuItem;
        private System.Windows.Forms.Panel pVerRegistro;
        private System.Windows.Forms.Label lblidPlaca;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraGrid.GridControl dtgListaRegistros;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaRegistrosView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraGrid.GridControl dtgListarUnidadBloqueada;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvListarUnidadBloqueada;
        private DevExpress.XtraGrid.GridControl dgvRutas;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRutasView;
        private DevExpress.XtraEditors.SimpleButton btnGuarda;
        private DevExpress.XtraEditors.SimpleButton btnHistoria;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem liberarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desbloquearTodasToolStripMenuItem;
    }
}