namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class FrmPreviajeTolvasPlantillas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreviajeTolvasPlantillas));
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.g_Fecha = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCodPreviaje = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verViajesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CerrarOPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteSumarizadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnViajesPendientes = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRendProm = new System.Windows.Forms.TextBox();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.btnBuscarTolvas = new DevExpress.XtraEditors.SimpleButton();
            this.dtgListaPreviajeTolvas = new DevExpress.XtraGrid.GridControl();
            this.dgvListaPreviajeTolvasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCerrarImpresoras = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvImpresoras = new DevExpress.XtraGrid.GridControl();
            this.dgvImpresorasVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dgvSumarizado = new DevExpress.XtraGrid.GridControl();
            this.dgvSumarizadoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevaOP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnListaPlanillas = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnListaImpresoras = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reporteviajespendientes = new DevExpress.XtraGrid.GridControl();
            this.reporteviajespendientesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.g_Fecha.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPreviajeTolvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPreviajeTolvasView)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImpresoras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImpresorasVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSumarizado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSumarizadoView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reporteviajespendientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteviajespendientesView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTituloGuia
            // 
            this.lblTituloGuia.BackColor = System.Drawing.Color.LimeGreen;
            this.lblTituloGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloGuia.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTituloGuia.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGuia.Name = "lblTituloGuia";
            this.lblTituloGuia.Size = new System.Drawing.Size(1303, 58);
            this.lblTituloGuia.TabIndex = 1;
            this.lblTituloGuia.Text = "LISTA DE OPERACIONES - TOLVAS";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTituloGuia.Click += new System.EventHandler(this.lblTituloGuia_Click);
            // 
            // g_Fecha
            // 
            this.g_Fecha.Controls.Add(this.dtpFechaFin);
            this.g_Fecha.Controls.Add(this.dtpFechaInicio);
            this.g_Fecha.Controls.Add(this.label2);
            this.g_Fecha.Controls.Add(this.label1);
            this.g_Fecha.Location = new System.Drawing.Point(439, 21);
            this.g_Fecha.Name = "g_Fecha";
            this.g_Fecha.Size = new System.Drawing.Size(331, 61);
            this.g_Fecha.TabIndex = 12;
            this.g_Fecha.TabStop = false;
            this.g_Fecha.Text = "Buscar por Fecha:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(213, 25);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaFin.TabIndex = 3;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(64, 25);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fin:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inicio:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCliente);
            this.groupBox4.Location = new System.Drawing.Point(144, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(295, 61);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ingresar Cliente:";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(15, 25);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(266, 20);
            this.txtCliente.TabIndex = 5;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodPreviaje);
            this.groupBox2.Location = new System.Drawing.Point(20, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(124, 61);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ingrese NroTicket";
            // 
            // txtCodPreviaje
            // 
            this.txtCodPreviaje.Location = new System.Drawing.Point(13, 25);
            this.txtCodPreviaje.Name = "txtCodPreviaje";
            this.txtCodPreviaje.Size = new System.Drawing.Size(95, 20);
            this.txtCodPreviaje.TabIndex = 3;
            this.txtCodPreviaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodPreviaje_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verViajesToolStripMenuItem,
            this.CerrarOPToolStripMenuItem,
            this.reporteSumarizadoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // verViajesToolStripMenuItem
            // 
            this.verViajesToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.binocular;
            this.verViajesToolStripMenuItem.Name = "verViajesToolStripMenuItem";
            this.verViajesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.verViajesToolStripMenuItem.Text = "Ver Viajes";
            this.verViajesToolStripMenuItem.Click += new System.EventHandler(this.verViajesToolStripMenuItem_Click);
            // 
            // CerrarOPToolStripMenuItem
            // 
            this.CerrarOPToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.mensajero;
            this.CerrarOPToolStripMenuItem.Name = "CerrarOPToolStripMenuItem";
            this.CerrarOPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CerrarOPToolStripMenuItem.Text = "Cerrar Operacion";
            this.CerrarOPToolStripMenuItem.Click += new System.EventHandler(this.CerrarOPToolStripMenuItem_Click);
            // 
            // reporteSumarizadoToolStripMenuItem
            // 
            this.reporteSumarizadoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.business_graph_pie_chart;
            this.reporteSumarizadoToolStripMenuItem.Name = "reporteSumarizadoToolStripMenuItem";
            this.reporteSumarizadoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reporteSumarizadoToolStripMenuItem.Text = "Reporte Sumarizado";
            this.reporteSumarizadoToolStripMenuItem.Click += new System.EventHandler(this.reporteSumarizadoToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.btnViajesPendientes);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnBuscarTolvas);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.g_Fecha);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1303, 105);
            this.panel1.TabIndex = 19;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = global::ReportesTranspesa.Properties.Resources.expotexcel;
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton1.Location = new System.Drawing.Point(1183, 24);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(96, 58);
            this.simpleButton1.TabIndex = 24;
            this.simpleButton1.Text = "SUMARIZADO";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnViajesPendientes
            // 
            this.btnViajesPendientes.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViajesPendientes.Appearance.Options.UseFont = true;
            this.btnViajesPendientes.Image = global::ReportesTranspesa.Properties.Resources.expotexcel;
            this.btnViajesPendientes.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnViajesPendientes.Location = new System.Drawing.Point(1080, 24);
            this.btnViajesPendientes.Name = "btnViajesPendientes";
            this.btnViajesPendientes.Size = new System.Drawing.Size(76, 58);
            this.btnViajesPendientes.TabIndex = 23;
            this.btnViajesPendientes.Text = "IMPORTAR";
            this.btnViajesPendientes.Click += new System.EventHandler(this.btnViajesPendientes_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRendProm);
            this.groupBox1.Controls.Add(this.txtOperacion);
            this.groupBox1.Location = new System.Drawing.Point(770, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 61);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rendimiento Combustible";
            // 
            // txtRendProm
            // 
            this.txtRendProm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRendProm.Location = new System.Drawing.Point(93, 25);
            this.txtRendProm.Name = "txtRendProm";
            this.txtRendProm.ReadOnly = true;
            this.txtRendProm.Size = new System.Drawing.Size(73, 20);
            this.txtRendProm.TabIndex = 4;
            // 
            // txtOperacion
            // 
            this.txtOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOperacion.Location = new System.Drawing.Point(14, 25);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(73, 20);
            this.txtOperacion.TabIndex = 3;
            this.txtOperacion.Text = "TOLVAS";
            // 
            // btnBuscarTolvas
            // 
            this.btnBuscarTolvas.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarTolvas.Appearance.Options.UseFont = true;
            this.btnBuscarTolvas.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarTolvas.Image")));
            this.btnBuscarTolvas.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnBuscarTolvas.Location = new System.Drawing.Point(982, 24);
            this.btnBuscarTolvas.Name = "btnBuscarTolvas";
            this.btnBuscarTolvas.Size = new System.Drawing.Size(70, 58);
            this.btnBuscarTolvas.TabIndex = 21;
            this.btnBuscarTolvas.Text = "BUSCAR";
            this.btnBuscarTolvas.Click += new System.EventHandler(this.btnBuscarTolvas_Click);
            // 
            // dtgListaPreviajeTolvas
            // 
            this.dtgListaPreviajeTolvas.AllowDrop = true;
            this.dtgListaPreviajeTolvas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaPreviajeTolvas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaPreviajeTolvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaPreviajeTolvas.Location = new System.Drawing.Point(0, 188);
            this.dtgListaPreviajeTolvas.MainView = this.dgvListaPreviajeTolvasView;
            this.dtgListaPreviajeTolvas.Name = "dtgListaPreviajeTolvas";
            this.dtgListaPreviajeTolvas.Size = new System.Drawing.Size(1303, 540);
            this.dtgListaPreviajeTolvas.TabIndex = 20;
            this.dtgListaPreviajeTolvas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaPreviajeTolvasView});
            this.dtgListaPreviajeTolvas.DoubleClick += new System.EventHandler(this.dtgListaPreviajeTolvas_DoubleClick);
            this.dtgListaPreviajeTolvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaPreviajeTolvas_MouseUp);
            // 
            // dgvListaPreviajeTolvasView
            // 
            this.dgvListaPreviajeTolvasView.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvListaPreviajeTolvasView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.dgvListaPreviajeTolvasView.GridControl = this.dtgListaPreviajeTolvas;
            this.dgvListaPreviajeTolvasView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvListaPreviajeTolvasView.Name = "dgvListaPreviajeTolvasView";
            this.dgvListaPreviajeTolvasView.OptionsBehavior.Editable = false;
            this.dgvListaPreviajeTolvasView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaPreviajeTolvasView.OptionsView.RowAutoHeight = true;
            this.dgvListaPreviajeTolvasView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaPreviajeTolvasView_CustomDrawCell);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCerrarImpresoras);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dgvImpresoras);
            this.panel2.Location = new System.Drawing.Point(705, 467);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(390, 257);
            this.panel2.TabIndex = 21;
            this.panel2.Visible = false;
            // 
            // btnCerrarImpresoras
            // 
            this.btnCerrarImpresoras.Location = new System.Drawing.Point(351, 5);
            this.btnCerrarImpresoras.Name = "btnCerrarImpresoras";
            this.btnCerrarImpresoras.Size = new System.Drawing.Size(36, 23);
            this.btnCerrarImpresoras.TabIndex = 22;
            this.btnCerrarImpresoras.Text = "X";
            this.btnCerrarImpresoras.UseVisualStyleBackColor = true;
            this.btnCerrarImpresoras.Click += new System.EventHandler(this.btnCerrarImpresoras_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LimeGreen;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(390, 35);
            this.label3.TabIndex = 23;
            this.label3.Text = "IMPRESORAS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvImpresoras
            // 
            this.dgvImpresoras.AllowDrop = true;
            this.dgvImpresoras.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvImpresoras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvImpresoras.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.GreenYellow;
            this.dgvImpresoras.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.dgvImpresoras.Location = new System.Drawing.Point(0, 38);
            this.dgvImpresoras.MainView = this.dgvImpresorasVista;
            this.dgvImpresoras.Name = "dgvImpresoras";
            this.dgvImpresoras.Size = new System.Drawing.Size(387, 216);
            this.dgvImpresoras.TabIndex = 21;
            this.dgvImpresoras.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvImpresorasVista});
            this.dgvImpresoras.DoubleClick += new System.EventHandler(this.dgvImpresoras_DoubleClick);
            // 
            // dgvImpresorasVista
            // 
            this.dgvImpresorasVista.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvImpresorasVista.Appearance.SelectedRow.Options.UseBackColor = true;
            this.dgvImpresorasVista.GridControl = this.dgvImpresoras;
            this.dgvImpresorasVista.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvImpresorasVista.Name = "dgvImpresorasVista";
            this.dgvImpresorasVista.OptionsBehavior.Editable = false;
            this.dgvImpresorasVista.OptionsView.ColumnAutoWidth = false;
            this.dgvImpresorasVista.OptionsView.RowAutoHeight = true;
            // 
            // dgvSumarizado
            // 
            this.dgvSumarizado.AllowDrop = true;
            this.dgvSumarizado.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvSumarizado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvSumarizado.Location = new System.Drawing.Point(389, 151);
            this.dgvSumarizado.MainView = this.dgvSumarizadoView;
            this.dgvSumarizado.Name = "dgvSumarizado";
            this.dgvSumarizado.Size = new System.Drawing.Size(187, 88);
            this.dgvSumarizado.TabIndex = 22;
            this.dgvSumarizado.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSumarizadoView});
            // 
            // dgvSumarizadoView
            // 
            this.dgvSumarizadoView.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvSumarizadoView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.dgvSumarizadoView.GridControl = this.dgvSumarizado;
            this.dgvSumarizadoView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvSumarizadoView.Name = "dgvSumarizadoView";
            this.dgvSumarizadoView.OptionsBehavior.Editable = false;
            this.dgvSumarizadoView.OptionsView.ColumnAutoWidth = false;
            this.dgvSumarizadoView.OptionsView.RowAutoHeight = true;
            this.dgvSumarizadoView.OptionsView.ShowFooter = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevaOP,
            this.toolStripSeparator1,
            this.btnListaPlanillas,
            this.toolStripButton1,
            this.btnListaImpresoras,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 58);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1303, 25);
            this.toolStrip1.TabIndex = 131;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevaOP
            // 
            this.btnNuevaOP.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnNuevaOP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevaOP.Name = "btnNuevaOP";
            this.btnNuevaOP.Size = new System.Drawing.Size(119, 22);
            this.btnNuevaOP.Text = "Nueva Operación";
            this.btnNuevaOP.Click += new System.EventHandler(this.btnNuevaOP_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnListaPlanillas
            // 
            this.btnListaPlanillas.Image = global::ReportesTranspesa.Properties.Resources.gastosentregados;
            this.btnListaPlanillas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnListaPlanillas.Name = "btnListaPlanillas";
            this.btnListaPlanillas.Size = new System.Drawing.Size(132, 22);
            this.btnListaPlanillas.Text = "Ver Lista de Planillas";
            this.btnListaPlanillas.Click += new System.EventHandler(this.btnListaPlanillas_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnListaImpresoras
            // 
            this.btnListaImpresoras.Image = global::ReportesTranspesa.Properties.Resources.impresora;
            this.btnListaImpresoras.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnListaImpresoras.Name = "btnListaImpresoras";
            this.btnListaImpresoras.Size = new System.Drawing.Size(104, 22);
            this.btnListaImpresoras.Text = "Ver Impresoras";
            this.btnListaImpresoras.Click += new System.EventHandler(this.btnListaImpresoras_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // reporteviajespendientes
            // 
            this.reporteviajespendientes.AllowDrop = true;
            this.reporteviajespendientes.ContextMenuStrip = this.contextMenuStrip1;
            this.reporteviajespendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reporteviajespendientes.Location = new System.Drawing.Point(863, 285);
            this.reporteviajespendientes.MainView = this.reporteviajespendientesView;
            this.reporteviajespendientes.Name = "reporteviajespendientes";
            this.reporteviajespendientes.Size = new System.Drawing.Size(339, 176);
            this.reporteviajespendientes.TabIndex = 132;
            this.reporteviajespendientes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.reporteviajespendientesView});
            this.reporteviajespendientes.Visible = false;
            // 
            // reporteviajespendientesView
            // 
            this.reporteviajespendientesView.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.reporteviajespendientesView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.reporteviajespendientesView.GridControl = this.reporteviajespendientes;
            this.reporteviajespendientesView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.reporteviajespendientesView.Name = "reporteviajespendientesView";
            this.reporteviajespendientesView.OptionsBehavior.Editable = false;
            this.reporteviajespendientesView.OptionsView.ColumnAutoWidth = false;
            this.reporteviajespendientesView.OptionsView.RowAutoHeight = true;
            this.reporteviajespendientesView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.reporteviajespendientesView_CustomDrawCell);
            // 
            // FrmPreviajeTolvasPlantillas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1303, 728);
            this.Controls.Add(this.reporteviajespendientes);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dtgListaPreviajeTolvas);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblTituloGuia);
            this.Controls.Add(this.dgvSumarizado);
            this.Name = "FrmPreviajeTolvasPlantillas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPreviajeTolvasPlantillas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPreviajeTolvasPlantillas_Load);
            this.Shown += new System.EventHandler(this.FrmPreviajeTolvasPlantillas_Shown);
            this.g_Fecha.ResumeLayout(false);
            this.g_Fecha.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPreviajeTolvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPreviajeTolvasView)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImpresoras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImpresorasVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSumarizado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSumarizadoView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reporteviajespendientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteviajespendientesView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCodPreviaje;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CerrarOPToolStripMenuItem;
        private System.Windows.Forms.GroupBox g_Fecha;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        public DevExpress.XtraGrid.GridControl dtgListaPreviajeTolvas;
        public DevExpress.XtraGrid.Views.Grid.GridView dgvListaPreviajeTolvasView;
        private System.Windows.Forms.ToolStripMenuItem verViajesToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnBuscarTolvas;
        private System.Windows.Forms.Panel panel2;
        public DevExpress.XtraGrid.GridControl dgvImpresoras;
        public DevExpress.XtraGrid.Views.Grid.GridView dgvImpresorasVista;
        private System.Windows.Forms.Button btnCerrarImpresoras;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem reporteSumarizadoToolStripMenuItem;
        public DevExpress.XtraGrid.GridControl dgvSumarizado;
        public DevExpress.XtraGrid.Views.Grid.GridView dgvSumarizadoView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevaOP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnListaPlanillas;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton btnListaImpresoras;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.TextBox txtRendProm;
        private DevExpress.XtraEditors.SimpleButton btnViajesPendientes;
        public DevExpress.XtraGrid.GridControl reporteviajespendientes;
        public DevExpress.XtraGrid.Views.Grid.GridView reporteviajespendientesView;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}