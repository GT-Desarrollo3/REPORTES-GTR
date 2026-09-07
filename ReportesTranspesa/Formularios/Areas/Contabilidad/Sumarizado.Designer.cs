namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class Sumarizado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sumarizado));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.grouper1 = new ReportesTranspesa.Grouper();
            this.cboCompañia = new MetroFramework.Controls.MetroComboBox();
            this.chkCompañia = new MetroFramework.Controls.MetroCheckBox();
            this.txtTransporte = new MetroFramework.Controls.MetroTextBox();
            this.cboTransporte = new MetroFramework.Controls.MetroComboBox();
            this.chkTransporte = new MetroFramework.Controls.MetroCheckBox();
            this.chkSucursal = new MetroFramework.Controls.MetroCheckBox();
            this.cboFacturado = new MetroFramework.Controls.MetroComboBox();
            this.chkFacturado = new MetroFramework.Controls.MetroCheckBox();
            this.cboSucursal = new MetroFramework.Controls.MetroComboBox();
            this.chkEstado = new MetroFramework.Controls.MetroCheckBox();
            this.cboTipo = new MetroFramework.Controls.MetroComboBox();
            this.chkTipo = new MetroFramework.Controls.MetroCheckBox();
            this.cboEstado = new MetroFramework.Controls.MetroComboBox();
            this.gbFecha = new ReportesTranspesa.Grouper();
            this.rbTolvas = new MetroFramework.Controls.MetroRadioButton();
            this.rbAnulados = new MetroFramework.Controls.MetroRadioButton();
            this.rbFechaCreacion = new MetroFramework.Controls.MetroRadioButton();
            this.rbFechaProg = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RUTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CLIENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GUÍATRANSP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CODIGOVIAJE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TRACTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ESTADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SITUACION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DESCRIPCIÓNDELACARGA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FACTURADO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DOCUMENTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MONTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SUCURSAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TIPO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TRANSPORTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FECHA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PROVEEDOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.grouper2 = new ReportesTranspesa.Grouper();
            this.button1 = new System.Windows.Forms.Button();
            this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton2 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton3 = new MetroFramework.Controls.MetroRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.gbFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            this.grouper2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.grouper1);
            this.splitContainer1.Panel1.Controls.Add(this.gbFecha);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1161, 595);
            this.splitContainer1.SplitterDistance = 114;
            this.splitContainer1.TabIndex = 29;
            // 
            // btnBuscar
            // 
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
            this.btnBuscar.Location = new System.Drawing.Point(933, 36);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 44);
            this.btnBuscar.TabIndex = 49;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnExcel
            // 
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
            this.btnExcel.Location = new System.Drawing.Point(990, 39);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 50;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.cboCompañia);
            this.grouper1.Controls.Add(this.chkCompañia);
            this.grouper1.Controls.Add(this.txtTransporte);
            this.grouper1.Controls.Add(this.cboTransporte);
            this.grouper1.Controls.Add(this.chkTransporte);
            this.grouper1.Controls.Add(this.chkSucursal);
            this.grouper1.Controls.Add(this.cboFacturado);
            this.grouper1.Controls.Add(this.chkFacturado);
            this.grouper1.Controls.Add(this.cboSucursal);
            this.grouper1.Controls.Add(this.chkEstado);
            this.grouper1.Controls.Add(this.cboTipo);
            this.grouper1.Controls.Add(this.chkTipo);
            this.grouper1.Controls.Add(this.cboEstado);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grouper1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Filtros";
            this.grouper1.Location = new System.Drawing.Point(284, 0);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 3;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(636, 112);
            this.grouper1.TabIndex = 43;
            // 
            // cboCompañia
            // 
            this.cboCompañia.Enabled = false;
            this.cboCompañia.FormattingEnabled = true;
            this.cboCompañia.ItemHeight = 23;
            this.cboCompañia.Items.AddRange(new object[] {
            "TODOS\t",
            "TRANSPESA",
            "BRA",
            "ALTRA",
            "AMT",
            "ADUANAS",
            "INOMAC"});
            this.cboCompañia.Location = new System.Drawing.Point(517, 68);
            this.cboCompañia.Name = "cboCompañia";
            this.cboCompañia.Size = new System.Drawing.Size(109, 29);
            this.cboCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCompañia.TabIndex = 46;
            this.cboCompañia.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboCompañia.UseSelectable = true;
            // 
            // chkCompañia
            // 
            this.chkCompañia.AutoSize = true;
            this.chkCompañia.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkCompañia.Location = new System.Drawing.Point(417, 73);
            this.chkCompañia.Name = "chkCompañia";
            this.chkCompañia.Size = new System.Drawing.Size(90, 19);
            this.chkCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.chkCompañia.TabIndex = 45;
            this.chkCompañia.Text = "Compañia:";
            this.chkCompañia.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkCompañia.UseSelectable = true;
            this.chkCompañia.CheckedChanged += new System.EventHandler(this.chkCompañia_CheckedChanged);
            // 
            // txtTransporte
            // 
            this.txtTransporte.Enabled = false;
            this.txtTransporte.Lines = new string[0];
            this.txtTransporte.Location = new System.Drawing.Point(418, 68);
            this.txtTransporte.MaxLength = 32767;
            this.txtTransporte.Name = "txtTransporte";
            this.txtTransporte.PasswordChar = '\0';
            this.txtTransporte.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTransporte.SelectedText = "";
            this.txtTransporte.Size = new System.Drawing.Size(209, 29);
            this.txtTransporte.Style = MetroFramework.MetroColorStyle.Red;
            this.txtTransporte.TabIndex = 44;
            this.txtTransporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtTransporte.UseSelectable = true;
            this.txtTransporte.Visible = false;
            // 
            // cboTransporte
            // 
            this.cboTransporte.Enabled = false;
            this.cboTransporte.FormattingEnabled = true;
            this.cboTransporte.ItemHeight = 23;
            this.cboTransporte.Items.AddRange(new object[] {
            "Propio",
            "Tercero"});
            this.cboTransporte.Location = new System.Drawing.Point(517, 33);
            this.cboTransporte.Name = "cboTransporte";
            this.cboTransporte.Size = new System.Drawing.Size(109, 29);
            this.cboTransporte.Style = MetroFramework.MetroColorStyle.Red;
            this.cboTransporte.TabIndex = 43;
            this.cboTransporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboTransporte.UseSelectable = true;
            // 
            // chkTransporte
            // 
            this.chkTransporte.AutoSize = true;
            this.chkTransporte.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkTransporte.Location = new System.Drawing.Point(417, 37);
            this.chkTransporte.Name = "chkTransporte";
            this.chkTransporte.Size = new System.Drawing.Size(93, 19);
            this.chkTransporte.Style = MetroFramework.MetroColorStyle.Red;
            this.chkTransporte.TabIndex = 42;
            this.chkTransporte.Text = "Transporte:";
            this.chkTransporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkTransporte.UseSelectable = true;
            this.chkTransporte.CheckedChanged += new System.EventHandler(this.chkTransporte_CheckedChanged);
            // 
            // chkSucursal
            // 
            this.chkSucursal.AutoSize = true;
            this.chkSucursal.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkSucursal.Location = new System.Drawing.Point(8, 37);
            this.chkSucursal.Name = "chkSucursal";
            this.chkSucursal.Size = new System.Drawing.Size(78, 19);
            this.chkSucursal.Style = MetroFramework.MetroColorStyle.Red;
            this.chkSucursal.TabIndex = 37;
            this.chkSucursal.Text = "Sucursal:";
            this.chkSucursal.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkSucursal.UseSelectable = true;
            this.chkSucursal.CheckedChanged += new System.EventHandler(this.chkSucursal_CheckedChanged);
            // 
            // cboFacturado
            // 
            this.cboFacturado.Enabled = false;
            this.cboFacturado.FormattingEnabled = true;
            this.cboFacturado.ItemHeight = 23;
            this.cboFacturado.Items.AddRange(new object[] {
            "SI",
            "NO"});
            this.cboFacturado.Location = new System.Drawing.Point(302, 68);
            this.cboFacturado.Name = "cboFacturado";
            this.cboFacturado.Size = new System.Drawing.Size(109, 29);
            this.cboFacturado.Style = MetroFramework.MetroColorStyle.Red;
            this.cboFacturado.TabIndex = 40;
            this.cboFacturado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboFacturado.UseSelectable = true;
            // 
            // chkFacturado
            // 
            this.chkFacturado.AutoSize = true;
            this.chkFacturado.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkFacturado.Location = new System.Drawing.Point(207, 73);
            this.chkFacturado.Name = "chkFacturado";
            this.chkFacturado.Size = new System.Drawing.Size(89, 19);
            this.chkFacturado.Style = MetroFramework.MetroColorStyle.Red;
            this.chkFacturado.TabIndex = 41;
            this.chkFacturado.Text = "Facturado:";
            this.chkFacturado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkFacturado.UseSelectable = true;
            this.chkFacturado.CheckedChanged += new System.EventHandler(this.chkFacturado_CheckedChanged);
            // 
            // cboSucursal
            // 
            this.cboSucursal.Enabled = false;
            this.cboSucursal.FormattingEnabled = true;
            this.cboSucursal.ItemHeight = 23;
            this.cboSucursal.Items.AddRange(new object[] {
            "Trujillo",
            "Lima"});
            this.cboSucursal.Location = new System.Drawing.Point(92, 33);
            this.cboSucursal.Name = "cboSucursal";
            this.cboSucursal.Size = new System.Drawing.Size(109, 29);
            this.cboSucursal.Style = MetroFramework.MetroColorStyle.Red;
            this.cboSucursal.TabIndex = 36;
            this.cboSucursal.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboSucursal.UseSelectable = true;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkEstado.Location = new System.Drawing.Point(8, 73);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(69, 19);
            this.chkEstado.Style = MetroFramework.MetroColorStyle.Red;
            this.chkEstado.TabIndex = 39;
            this.chkEstado.Text = "Estado:";
            this.chkEstado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkEstado.UseSelectable = true;
            this.chkEstado.CheckedChanged += new System.EventHandler(this.chkEstado_CheckedChanged);
            // 
            // cboTipo
            // 
            this.cboTipo.Enabled = false;
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.ItemHeight = 23;
            this.cboTipo.Items.AddRange(new object[] {
            "Nacional",
            "Local"});
            this.cboTipo.Location = new System.Drawing.Point(302, 33);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(109, 29);
            this.cboTipo.Style = MetroFramework.MetroColorStyle.Red;
            this.cboTipo.TabIndex = 33;
            this.cboTipo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboTipo.UseSelectable = true;
            // 
            // chkTipo
            // 
            this.chkTipo.AutoSize = true;
            this.chkTipo.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkTipo.Location = new System.Drawing.Point(207, 37);
            this.chkTipo.Name = "chkTipo";
            this.chkTipo.Size = new System.Drawing.Size(54, 19);
            this.chkTipo.Style = MetroFramework.MetroColorStyle.Red;
            this.chkTipo.TabIndex = 34;
            this.chkTipo.Text = "Tipo:";
            this.chkTipo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkTipo.UseSelectable = true;
            this.chkTipo.CheckedChanged += new System.EventHandler(this.chkTipo_CheckedChanged);
            // 
            // cboEstado
            // 
            this.cboEstado.Enabled = false;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.ItemHeight = 23;
            this.cboEstado.Items.AddRange(new object[] {
            "Pendiente",
            "Programado",
            "Ejecución",
            "Completado"});
            this.cboEstado.Location = new System.Drawing.Point(92, 68);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(109, 29);
            this.cboEstado.Style = MetroFramework.MetroColorStyle.Red;
            this.cboEstado.TabIndex = 38;
            this.cboEstado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboEstado.UseSelectable = true;
            // 
            // gbFecha
            // 
            this.gbFecha.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFecha.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFecha.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFecha.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.BorderThickness = 1F;
            this.gbFecha.Controls.Add(this.rbTolvas);
            this.gbFecha.Controls.Add(this.rbAnulados);
            this.gbFecha.Controls.Add(this.rbFechaCreacion);
            this.gbFecha.Controls.Add(this.rbFechaProg);
            this.gbFecha.Controls.Add(this.metroLabel1);
            this.gbFecha.Controls.Add(this.dtpFechaIni);
            this.gbFecha.Controls.Add(this.metroLabel2);
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
            this.gbFecha.Size = new System.Drawing.Size(274, 112);
            this.gbFecha.TabIndex = 42;
            // 
            // rbTolvas
            // 
            this.rbTolvas.AutoSize = true;
            this.rbTolvas.Location = new System.Drawing.Point(168, 87);
            this.rbTolvas.Name = "rbTolvas";
            this.rbTolvas.Size = new System.Drawing.Size(55, 15);
            this.rbTolvas.Style = MetroFramework.MetroColorStyle.Red;
            this.rbTolvas.TabIndex = 20;
            this.rbTolvas.Text = "Tolvas";
            this.rbTolvas.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbTolvas.UseSelectable = true;
            // 
            // rbAnulados
            // 
            this.rbAnulados.AutoSize = true;
            this.rbAnulados.Location = new System.Drawing.Point(168, 67);
            this.rbAnulados.Name = "rbAnulados";
            this.rbAnulados.Size = new System.Drawing.Size(73, 15);
            this.rbAnulados.Style = MetroFramework.MetroColorStyle.Red;
            this.rbAnulados.TabIndex = 19;
            this.rbAnulados.Text = "Anulados";
            this.rbAnulados.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbAnulados.UseSelectable = true;
            // 
            // rbFechaCreacion
            // 
            this.rbFechaCreacion.AutoSize = true;
            this.rbFechaCreacion.Location = new System.Drawing.Point(168, 47);
            this.rbFechaCreacion.Name = "rbFechaCreacion";
            this.rbFechaCreacion.Size = new System.Drawing.Size(79, 15);
            this.rbFechaCreacion.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaCreacion.TabIndex = 18;
            this.rbFechaCreacion.Text = "F.Creación";
            this.rbFechaCreacion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaCreacion.UseSelectable = true;
            // 
            // rbFechaProg
            // 
            this.rbFechaProg.AutoSize = true;
            this.rbFechaProg.Checked = true;
            this.rbFechaProg.Location = new System.Drawing.Point(168, 26);
            this.rbFechaProg.Name = "rbFechaProg";
            this.rbFechaProg.Size = new System.Drawing.Size(97, 15);
            this.rbFechaProg.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaProg.TabIndex = 17;
            this.rbFechaProg.TabStop = true;
            this.rbFechaProg.Text = "F.Programada";
            this.rbFechaProg.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaProg.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(6, 39);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(44, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 15;
            this.metroLabel1.Text = "Inicio:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(51, 34);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 13;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(20, 75);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(30, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 16;
            this.metroLabel2.Text = "Fin:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(51, 71);
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
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dtgvData);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(1161, 477);
            this.splitContainer2.SplitterDistance = 656;
            this.splitContainer2.TabIndex = 1;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(1161, 477);
            this.dtgvData.TabIndex = 0;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.RUTA,
            this.CLIENTE,
            this.GUÍATRANSP,
            this.CODIGOVIAJE,
            this.GR,
            this.TRACTO,
            this.ESTADO,
            this.SITUACION,
            this.DESCRIPCIÓNDELACARGA,
            this.FACTURADO,
            this.DOCUMENTO,
            this.MONTO,
            this.SUCURSAL,
            this.TIPO,
            this.TRANSPORTE,
            this.FECHA,
            this.PROVEEDOR});
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsSelection.MultiSelect = true;
            this.dtgvDataView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowFooter = true;
            this.dtgvDataView.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dtgvDataView_CustomSummaryCalculate);
            this.dtgvDataView.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler(this.dtgvDataView_CustomRowFilter);
            // 
            // RUTA
            // 
            this.RUTA.Caption = "RUTA";
            this.RUTA.FieldName = "RUTA";
            this.RUTA.Name = "RUTA";
            this.RUTA.Visible = true;
            this.RUTA.VisibleIndex = 3;
            // 
            // CLIENTE
            // 
            this.CLIENTE.Caption = "CLIENTE";
            this.CLIENTE.FieldName = "CLIENTE";
            this.CLIENTE.Name = "CLIENTE";
            this.CLIENTE.Visible = true;
            this.CLIENTE.VisibleIndex = 4;
            // 
            // GUÍATRANSP
            // 
            this.GUÍATRANSP.Caption = "GUÍA TRANSP.";
            this.GUÍATRANSP.FieldName = "GUÍA TRANSP.";
            this.GUÍATRANSP.Name = "GUÍATRANSP";
            this.GUÍATRANSP.Visible = true;
            this.GUÍATRANSP.VisibleIndex = 5;
            this.GUÍATRANSP.Width = 86;
            // 
            // CODIGOVIAJE
            // 
            this.CODIGOVIAJE.Caption = "CODIGO VIAJE";
            this.CODIGOVIAJE.Name = "CODIGOVIAJE";
            this.CODIGOVIAJE.Visible = true;
            this.CODIGOVIAJE.VisibleIndex = 1;
            // 
            // GR
            // 
            this.GR.Caption = "G/R";
            this.GR.FieldName = "G/R";
            this.GR.Name = "GR";
            this.GR.Visible = true;
            this.GR.VisibleIndex = 6;
            // 
            // TRACTO
            // 
            this.TRACTO.Caption = "TRACTO";
            this.TRACTO.FieldName = "TRACTO";
            this.TRACTO.Name = "TRACTO";
            this.TRACTO.Visible = true;
            this.TRACTO.VisibleIndex = 7;
            // 
            // ESTADO
            // 
            this.ESTADO.Caption = "ESTADO";
            this.ESTADO.FieldName = "ESTADO";
            this.ESTADO.Name = "ESTADO";
            this.ESTADO.Visible = true;
            this.ESTADO.VisibleIndex = 8;
            // 
            // SITUACION
            // 
            this.SITUACION.Caption = "SITUACION";
            this.SITUACION.FieldName = "SITUACION";
            this.SITUACION.Name = "SITUACION";
            this.SITUACION.Visible = true;
            this.SITUACION.VisibleIndex = 9;
            // 
            // DESCRIPCIÓNDELACARGA
            // 
            this.DESCRIPCIÓNDELACARGA.Caption = "DESCRIPCIÓN DE LA CARGA";
            this.DESCRIPCIÓNDELACARGA.FieldName = "DESCRIPCIÓN DE LA CARGA";
            this.DESCRIPCIÓNDELACARGA.Name = "DESCRIPCIÓNDELACARGA";
            this.DESCRIPCIÓNDELACARGA.Visible = true;
            this.DESCRIPCIÓNDELACARGA.VisibleIndex = 10;
            // 
            // FACTURADO
            // 
            this.FACTURADO.Caption = "FACTURADO";
            this.FACTURADO.FieldName = "FACTURADO";
            this.FACTURADO.Name = "FACTURADO";
            this.FACTURADO.Visible = true;
            this.FACTURADO.VisibleIndex = 11;
            // 
            // DOCUMENTO
            // 
            this.DOCUMENTO.Caption = "DOCUMENTO";
            this.DOCUMENTO.FieldName = "DOCUMENTO";
            this.DOCUMENTO.Name = "DOCUMENTO";
            this.DOCUMENTO.Visible = true;
            this.DOCUMENTO.VisibleIndex = 12;
            // 
            // MONTO
            // 
            this.MONTO.Caption = "MONTO";
            this.MONTO.DisplayFormat.FormatString = "({0:c2})";
            this.MONTO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.MONTO.FieldName = "MONTO";
            this.MONTO.Name = "MONTO";
            this.MONTO.Visible = true;
            this.MONTO.VisibleIndex = 13;
            // 
            // SUCURSAL
            // 
            this.SUCURSAL.Caption = "SUCURSAL";
            this.SUCURSAL.FieldName = "SUCURSAL";
            this.SUCURSAL.Name = "SUCURSAL";
            this.SUCURSAL.Visible = true;
            this.SUCURSAL.VisibleIndex = 14;
            // 
            // TIPO
            // 
            this.TIPO.Caption = "TIPO";
            this.TIPO.FieldName = "TIPO";
            this.TIPO.Name = "TIPO";
            this.TIPO.Visible = true;
            this.TIPO.VisibleIndex = 15;
            // 
            // TRANSPORTE
            // 
            this.TRANSPORTE.Caption = "TRANSPORTE";
            this.TRANSPORTE.FieldName = "TRANSPORTE";
            this.TRANSPORTE.Name = "TRANSPORTE";
            this.TRANSPORTE.Visible = true;
            this.TRANSPORTE.VisibleIndex = 16;
            // 
            // FECHA
            // 
            this.FECHA.Caption = "FECHA";
            this.FECHA.FieldName = "FECHA";
            this.FECHA.Name = "FECHA";
            this.FECHA.Visible = true;
            this.FECHA.VisibleIndex = 2;
            // 
            // PROVEEDOR
            // 
            this.PROVEEDOR.Caption = "PROVEEDOR";
            this.PROVEEDOR.FieldName = "PROVEEDOR";
            this.PROVEEDOR.Name = "PROVEEDOR";
            this.PROVEEDOR.Visible = true;
            this.PROVEEDOR.VisibleIndex = 17;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardar.Location = new System.Drawing.Point(1065, 99);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(40, 37);
            this.btnGuardar.TabIndex = 92;
            this.btnGuardar.ToolTip = "Guardar cambios";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnImprimir
            // 
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
            this.btnImprimir.Location = new System.Drawing.Point(1120, 99);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 51;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.grouper2.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.button1);
            this.grouper2.Controls.Add(this.metroRadioButton1);
            this.grouper2.Controls.Add(this.metroRadioButton2);
            this.grouper2.Controls.Add(this.metroRadioButton3);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grouper2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Reportes";
            this.grouper2.Location = new System.Drawing.Point(994, 59);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = false;
            this.grouper2.RoundCorners = 3;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 3;
            this.grouper2.Size = new System.Drawing.Size(199, 112);
            this.grouper2.TabIndex = 95;
            this.grouper2.Visible = false;
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(146, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Ver";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // metroRadioButton1
            // 
            this.metroRadioButton1.AutoSize = true;
            this.metroRadioButton1.Location = new System.Drawing.Point(13, 27);
            this.metroRadioButton1.Name = "metroRadioButton1";
            this.metroRadioButton1.Size = new System.Drawing.Size(88, 15);
            this.metroRadioButton1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroRadioButton1.TabIndex = 19;
            this.metroRadioButton1.Text = "Viajes x User";
            this.metroRadioButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroRadioButton1.UseSelectable = true;
            // 
            // metroRadioButton2
            // 
            this.metroRadioButton2.AutoSize = true;
            this.metroRadioButton2.Location = new System.Drawing.Point(13, 62);
            this.metroRadioButton2.Name = "metroRadioButton2";
            this.metroRadioButton2.Size = new System.Drawing.Size(134, 15);
            this.metroRadioButton2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroRadioButton2.TabIndex = 18;
            this.metroRadioButton2.Text = "Facturación x Cliente";
            this.metroRadioButton2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroRadioButton2.UseSelectable = true;
            // 
            // metroRadioButton3
            // 
            this.metroRadioButton3.AutoSize = true;
            this.metroRadioButton3.Checked = true;
            this.metroRadioButton3.Location = new System.Drawing.Point(13, 45);
            this.metroRadioButton3.Name = "metroRadioButton3";
            this.metroRadioButton3.Size = new System.Drawing.Size(141, 15);
            this.metroRadioButton3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroRadioButton3.TabIndex = 17;
            this.metroRadioButton3.TabStop = true;
            this.metroRadioButton3.Text = "Viajes Anulados x User";
            this.metroRadioButton3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroRadioButton3.UseSelectable = true;
            // 
            // Sumarizado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 675);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.grouper2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sumarizado";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Reporte Sumarizado de Guías";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Sumarizado_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.gbFecha.ResumeLayout(false);
            this.gbFecha.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.grouper2.ResumeLayout(false);
            this.grouper2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroComboBox cboTipo;
        private MetroFramework.Controls.MetroCheckBox chkTipo;
        private MetroFramework.Controls.MetroCheckBox chkSucursal;
        private MetroFramework.Controls.MetroComboBox cboSucursal;
        private MetroFramework.Controls.MetroCheckBox chkFacturado;
        private MetroFramework.Controls.MetroComboBox cboFacturado;
        private MetroFramework.Controls.MetroCheckBox chkEstado;
        private MetroFramework.Controls.MetroComboBox cboEstado;
        private Grouper gbFecha;
        private MetroFramework.Controls.MetroRadioButton rbFechaCreacion;
        private MetroFramework.Controls.MetroRadioButton rbFechaProg;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private Grouper grouper1;
        private MetroFramework.Controls.MetroCheckBox chkTransporte;
        private MetroFramework.Controls.MetroComboBox cboTransporte;
        private MetroFramework.Controls.MetroTextBox txtTransporte;
        private DevExpress.XtraGrid.Columns.GridColumn CODIGOVIAJE;
        private DevExpress.XtraGrid.Columns.GridColumn FECHA;
        private DevExpress.XtraGrid.Columns.GridColumn RUTA;
        private DevExpress.XtraGrid.Columns.GridColumn CLIENTE;
        private DevExpress.XtraGrid.Columns.GridColumn GUÍATRANSP;
        private DevExpress.XtraGrid.Columns.GridColumn GR;
        private DevExpress.XtraGrid.Columns.GridColumn TRACTO;
        private DevExpress.XtraGrid.Columns.GridColumn ESTADO;
        private DevExpress.XtraGrid.Columns.GridColumn SITUACION;
        private DevExpress.XtraGrid.Columns.GridColumn DESCRIPCIÓNDELACARGA;
        private DevExpress.XtraGrid.Columns.GridColumn FACTURADO;
        private DevExpress.XtraGrid.Columns.GridColumn DOCUMENTO;
        private DevExpress.XtraGrid.Columns.GridColumn MONTO;
        private DevExpress.XtraGrid.Columns.GridColumn SUCURSAL;
        private DevExpress.XtraGrid.Columns.GridColumn TIPO;
        private DevExpress.XtraGrid.Columns.GridColumn TRANSPORTE;
        private DevExpress.XtraGrid.Columns.GridColumn PROVEEDOR;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private MetroFramework.Controls.MetroComboBox cboCompañia;
        private MetroFramework.Controls.MetroCheckBox chkCompañia;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private MetroFramework.Controls.MetroRadioButton rbAnulados;
        private Grouper grouper2;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton2;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroRadioButton rbTolvas;
    }
}