namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    partial class Facturacion_Diaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facturacion_Diaria));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbSucursal = new ReportesTranspesa.Grouper();
            this.chkLima = new MetroFramework.Controls.MetroCheckBox();
            this.chkTrujillo = new MetroFramework.Controls.MetroCheckBox();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.gbFiltro = new ReportesTranspesa.Grouper();
            this.rbPorUN = new MetroFramework.Controls.MetroRadioButton();
            this.rbPorCliente = new MetroFramework.Controls.MetroRadioButton();
            this.rbPorServicio = new MetroFramework.Controls.MetroRadioButton();
            this.rbPorFacturador = new MetroFramework.Controls.MetroRadioButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.gbCompania = new ReportesTranspesa.Grouper();
            this.chkBra = new MetroFramework.Controls.MetroCheckBox();
            this.chkTranspesa = new MetroFramework.Controls.MetroCheckBox();
            this.gbFecha = new ReportesTranspesa.Grouper();
            this.rbFechaPreparacion = new MetroFramework.Controls.MetroRadioButton();
            this.rbFechaEmision = new MetroFramework.Controls.MetroRadioButton();
            this.lblInicio = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.lblFin = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dtgvData = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbSucursal.SuspendLayout();
            this.gbFiltro.SuspendLayout();
            this.gbCompania.SuspendLayout();
            this.gbFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbSucursal);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.gbFiltro);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.gbCompania);
            this.splitContainer1.Panel1.Controls.Add(this.gbFecha);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(958, 552);
            this.splitContainer1.SplitterDistance = 116;
            this.splitContainer1.TabIndex = 0;
            // 
            // gbSucursal
            // 
            this.gbSucursal.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbSucursal.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbSucursal.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbSucursal.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbSucursal.BorderThickness = 1F;
            this.gbSucursal.Controls.Add(this.chkLima);
            this.gbSucursal.Controls.Add(this.chkTrujillo);
            this.gbSucursal.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSucursal.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbSucursal.GroupImage = null;
            this.gbSucursal.GroupTitle = "Sucursal";
            this.gbSucursal.Location = new System.Drawing.Point(626, 0);
            this.gbSucursal.Name = "gbSucursal";
            this.gbSucursal.Padding = new System.Windows.Forms.Padding(20);
            this.gbSucursal.PaintGroupBox = false;
            this.gbSucursal.RoundCorners = 3;
            this.gbSucursal.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbSucursal.ShadowControl = false;
            this.gbSucursal.ShadowThickness = 3;
            this.gbSucursal.Size = new System.Drawing.Size(110, 123);
            this.gbSucursal.TabIndex = 41;
            this.gbSucursal.Visible = false;
            // 
            // chkLima
            // 
            this.chkLima.AutoSize = true;
            this.chkLima.Checked = true;
            this.chkLima.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLima.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkLima.Location = new System.Drawing.Point(12, 73);
            this.chkLima.Name = "chkLima";
            this.chkLima.Size = new System.Drawing.Size(54, 19);
            this.chkLima.Style = MetroFramework.MetroColorStyle.Red;
            this.chkLima.TabIndex = 41;
            this.chkLima.Text = "Lima";
            this.chkLima.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkLima.UseSelectable = true;
            this.chkLima.Visible = false;
            // 
            // chkTrujillo
            // 
            this.chkTrujillo.AutoSize = true;
            this.chkTrujillo.Checked = true;
            this.chkTrujillo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrujillo.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkTrujillo.Location = new System.Drawing.Point(12, 32);
            this.chkTrujillo.Name = "chkTrujillo";
            this.chkTrujillo.Size = new System.Drawing.Size(64, 19);
            this.chkTrujillo.Style = MetroFramework.MetroColorStyle.Red;
            this.chkTrujillo.TabIndex = 40;
            this.chkTrujillo.Text = "Trujillo";
            this.chkTrujillo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkTrujillo.UseSelectable = true;
            this.chkTrujillo.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseBackColor = true;
            this.btnImprimir.Appearance.Options.UseBorderColor = true;
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnImprimir.Location = new System.Drawing.Point(887, 86);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 52;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // gbFiltro
            // 
            this.gbFiltro.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFiltro.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFiltro.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFiltro.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.BorderThickness = 1F;
            this.gbFiltro.Controls.Add(this.rbPorUN);
            this.gbFiltro.Controls.Add(this.rbPorCliente);
            this.gbFiltro.Controls.Add(this.rbPorServicio);
            this.gbFiltro.Controls.Add(this.rbPorFacturador);
            this.gbFiltro.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltro.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.GroupImage = null;
            this.gbFiltro.GroupTitle = "Filtro";
            this.gbFiltro.Location = new System.Drawing.Point(378, 0);
            this.gbFiltro.Name = "gbFiltro";
            this.gbFiltro.Padding = new System.Windows.Forms.Padding(20);
            this.gbFiltro.PaintGroupBox = false;
            this.gbFiltro.RoundCorners = 3;
            this.gbFiltro.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFiltro.ShadowControl = false;
            this.gbFiltro.ShadowThickness = 3;
            this.gbFiltro.Size = new System.Drawing.Size(241, 123);
            this.gbFiltro.TabIndex = 40;
            // 
            // rbPorUN
            // 
            this.rbPorUN.AutoSize = true;
            this.rbPorUN.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPorUN.Location = new System.Drawing.Point(136, 23);
            this.rbPorUN.Name = "rbPorUN";
            this.rbPorUN.Size = new System.Drawing.Size(97, 38);
            this.rbPorUN.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPorUN.TabIndex = 22;
            this.rbPorUN.Text = "Por Unidad \r\nde Negocio";
            this.rbPorUN.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPorUN.UseSelectable = true;
            // 
            // rbPorCliente
            // 
            this.rbPorCliente.AutoSize = true;
            this.rbPorCliente.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPorCliente.Location = new System.Drawing.Point(14, 63);
            this.rbPorCliente.Name = "rbPorCliente";
            this.rbPorCliente.Size = new System.Drawing.Size(91, 19);
            this.rbPorCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPorCliente.TabIndex = 21;
            this.rbPorCliente.Text = "Por Cliente";
            this.rbPorCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPorCliente.UseSelectable = true;
            // 
            // rbPorServicio
            // 
            this.rbPorServicio.AutoSize = true;
            this.rbPorServicio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPorServicio.Location = new System.Drawing.Point(14, 94);
            this.rbPorServicio.Name = "rbPorServicio";
            this.rbPorServicio.Size = new System.Drawing.Size(95, 19);
            this.rbPorServicio.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPorServicio.TabIndex = 20;
            this.rbPorServicio.Text = "Por Servicio";
            this.rbPorServicio.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPorServicio.UseSelectable = true;
            this.rbPorServicio.CheckedChanged += new System.EventHandler(this.rbPorServicio_CheckedChanged);
            // 
            // rbPorFacturador
            // 
            this.rbPorFacturador.AutoSize = true;
            this.rbPorFacturador.Checked = true;
            this.rbPorFacturador.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPorFacturador.Location = new System.Drawing.Point(14, 32);
            this.rbPorFacturador.Name = "rbPorFacturador";
            this.rbPorFacturador.Size = new System.Drawing.Size(115, 19);
            this.rbPorFacturador.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPorFacturador.TabIndex = 19;
            this.rbPorFacturador.TabStop = true;
            this.rbPorFacturador.Text = "Por Facturador";
            this.rbPorFacturador.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPorFacturador.UseSelectable = true;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(813, 86);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 51;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(742, 86);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 50;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // gbCompania
            // 
            this.gbCompania.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbCompania.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbCompania.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbCompania.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbCompania.BorderThickness = 1F;
            this.gbCompania.Controls.Add(this.chkBra);
            this.gbCompania.Controls.Add(this.chkTranspesa);
            this.gbCompania.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbCompania.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCompania.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbCompania.GroupImage = null;
            this.gbCompania.GroupTitle = "Compañía";
            this.gbCompania.Location = new System.Drawing.Point(215, 0);
            this.gbCompania.Name = "gbCompania";
            this.gbCompania.Padding = new System.Windows.Forms.Padding(20);
            this.gbCompania.PaintGroupBox = false;
            this.gbCompania.RoundCorners = 3;
            this.gbCompania.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbCompania.ShadowControl = false;
            this.gbCompania.ShadowThickness = 3;
            this.gbCompania.Size = new System.Drawing.Size(157, 123);
            this.gbCompania.TabIndex = 20;
            // 
            // chkBra
            // 
            this.chkBra.AutoSize = true;
            this.chkBra.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkBra.Location = new System.Drawing.Point(14, 73);
            this.chkBra.Name = "chkBra";
            this.chkBra.Size = new System.Drawing.Size(135, 19);
            this.chkBra.Style = MetroFramework.MetroColorStyle.Red;
            this.chkBra.TabIndex = 39;
            this.chkBra.Text = "Fabricaciones BRA";
            this.chkBra.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkBra.UseSelectable = true;
            // 
            // chkTranspesa
            // 
            this.chkTranspesa.AutoSize = true;
            this.chkTranspesa.Checked = true;
            this.chkTranspesa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTranspesa.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkTranspesa.Location = new System.Drawing.Point(14, 32);
            this.chkTranspesa.Name = "chkTranspesa";
            this.chkTranspesa.Size = new System.Drawing.Size(85, 19);
            this.chkTranspesa.Style = MetroFramework.MetroColorStyle.Red;
            this.chkTranspesa.TabIndex = 38;
            this.chkTranspesa.Text = "Transpesa";
            this.chkTranspesa.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkTranspesa.UseSelectable = true;
            // 
            // gbFecha
            // 
            this.gbFecha.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFecha.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFecha.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFecha.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.BorderThickness = 1F;
            this.gbFecha.Controls.Add(this.rbFechaPreparacion);
            this.gbFecha.Controls.Add(this.rbFechaEmision);
            this.gbFecha.Controls.Add(this.lblInicio);
            this.gbFecha.Controls.Add(this.dtpFechaIni);
            this.gbFecha.Controls.Add(this.lblFin);
            this.gbFecha.Controls.Add(this.dtpFechaFin);
            this.gbFecha.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFecha.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.GroupImage = null;
            this.gbFecha.GroupTitle = "Fecha";
            this.gbFecha.Location = new System.Drawing.Point(3, 0);
            this.gbFecha.Name = "gbFecha";
            this.gbFecha.Padding = new System.Windows.Forms.Padding(20);
            this.gbFecha.PaintGroupBox = false;
            this.gbFecha.RoundCorners = 3;
            this.gbFecha.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFecha.ShadowControl = false;
            this.gbFecha.ShadowThickness = 3;
            this.gbFecha.Size = new System.Drawing.Size(206, 123);
            this.gbFecha.TabIndex = 19;
            // 
            // rbFechaPreparacion
            // 
            this.rbFechaPreparacion.AutoSize = true;
            this.rbFechaPreparacion.Location = new System.Drawing.Point(100, 98);
            this.rbFechaPreparacion.Name = "rbFechaPreparacion";
            this.rbFechaPreparacion.Size = new System.Drawing.Size(95, 15);
            this.rbFechaPreparacion.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaPreparacion.TabIndex = 18;
            this.rbFechaPreparacion.Text = "F.Preparación";
            this.rbFechaPreparacion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaPreparacion.UseSelectable = true;
            // 
            // rbFechaEmision
            // 
            this.rbFechaEmision.AutoSize = true;
            this.rbFechaEmision.Checked = true;
            this.rbFechaEmision.Location = new System.Drawing.Point(20, 98);
            this.rbFechaEmision.Name = "rbFechaEmision";
            this.rbFechaEmision.Size = new System.Drawing.Size(74, 15);
            this.rbFechaEmision.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaEmision.TabIndex = 17;
            this.rbFechaEmision.TabStop = true;
            this.rbFechaEmision.Text = "F.Emisión";
            this.rbFechaEmision.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaEmision.UseSelectable = true;
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblInicio.Location = new System.Drawing.Point(9, 32);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(44, 19);
            this.lblInicio.Style = MetroFramework.MetroColorStyle.Red;
            this.lblInicio.TabIndex = 15;
            this.lblInicio.Text = "Inicio:";
            this.lblInicio.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(59, 28);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 13;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFin.Location = new System.Drawing.Point(22, 68);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(30, 19);
            this.lblFin.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFin.TabIndex = 16;
            this.lblFin.Text = "Fin:";
            this.lblFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(59, 63);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 14;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dtgvData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartControl1);
            this.splitContainer2.Size = new System.Drawing.Size(958, 432);
            this.splitContainer2.SplitterDistance = 241;
            this.splitContainer2.TabIndex = 0;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.OptionsPrint.PageSettings.Landscape = true;
            this.dtgvData.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.dtgvData.Size = new System.Drawing.Size(958, 241);
            this.dtgvData.TabIndex = 1;
            this.dtgvData.CustomAppearance += new DevExpress.XtraPivotGrid.PivotCustomAppearanceEventHandler(this.pvgData_CustomAppearance);
            // 
            // chartControl1
            // 
            this.chartControl1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.LookAndFeel.SkinName = "Darkroom";
            this.chartControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(958, 187);
            this.chartControl1.TabIndex = 0;
            // 
            // Facturacion_Diaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 632);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Facturacion_Diaria";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Facturación Diaria";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Facturacion_Diaria_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbSucursal.ResumeLayout(false);
            this.gbSucursal.PerformLayout();
            this.gbFiltro.ResumeLayout(false);
            this.gbFiltro.PerformLayout();
            this.gbCompania.ResumeLayout(false);
            this.gbCompania.PerformLayout();
            this.gbFecha.ResumeLayout(false);
            this.gbFecha.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraPivotGrid.PivotGridControl dtgvData;
        private Grouper gbFecha;
        private MetroFramework.Controls.MetroRadioButton rbFechaPreparacion;
        private MetroFramework.Controls.MetroRadioButton rbFechaEmision;
        private MetroFramework.Controls.MetroLabel lblInicio;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel lblFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private Grouper gbCompania;
        private MetroFramework.Controls.MetroCheckBox chkBra;
        private MetroFramework.Controls.MetroCheckBox chkTranspesa;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private Grouper gbFiltro;
        private MetroFramework.Controls.MetroRadioButton rbPorServicio;
        private MetroFramework.Controls.MetroRadioButton rbPorFacturador;
        private Grouper gbSucursal;
        private MetroFramework.Controls.MetroCheckBox chkLima;
        private MetroFramework.Controls.MetroCheckBox chkTrujillo;
        private MetroFramework.Controls.MetroRadioButton rbPorCliente;
        private MetroFramework.Controls.MetroRadioButton rbPorUN;
    }
}