namespace ReportesTranspesa.Formularios.Administrador
{
    partial class NuevosReportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevosReportes));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.txtDescripcionReporte = new MetroFramework.Controls.MetroTextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnModificar = new DevExpress.XtraEditors.SimpleButton();
            this.txtCodigoReporte = new MetroFramework.Controls.MetroTextBox();
            this.lblCodigoReporte = new System.Windows.Forms.Label();
            this.txtReporte = new MetroFramework.Controls.MetroTextBox();
            this.lblReporte = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.cboIdArea = new MetroFramework.Controls.MetroComboBox();
            this.cboEstado = new MetroFramework.Controls.MetroComboBox();
            this.cboArea = new MetroFramework.Controls.MetroComboBox();
            this.txtFormulario = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuscarNombreReporte = new System.Windows.Forms.TextBox();
            this.txtBuscarCodigoReporte = new System.Windows.Forms.TextBox();
            this.lblReporteCodigo = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvReportes = new DevExpress.XtraGrid.GridControl();
            this.dtgvReportesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReportes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReportesView)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitter1);
            this.splitContainer1.Panel1.Controls.Add(this.txtDescripcionReporte);
            this.splitContainer1.Panel1.Controls.Add(this.lblDescripcion);
            this.splitContainer1.Panel1.Controls.Add(this.btnModificar);
            this.splitContainer1.Panel1.Controls.Add(this.txtCodigoReporte);
            this.splitContainer1.Panel1.Controls.Add(this.lblCodigoReporte);
            this.splitContainer1.Panel1.Controls.Add(this.txtReporte);
            this.splitContainer1.Panel1.Controls.Add(this.lblReporte);
            this.splitContainer1.Panel1.Controls.Add(this.btnGuardar);
            this.splitContainer1.Panel1.Controls.Add(this.cboIdArea);
            this.splitContainer1.Panel1.Controls.Add(this.cboEstado);
            this.splitContainer1.Panel1.Controls.Add(this.cboArea);
            this.splitContainer1.Panel1.Controls.Add(this.txtFormulario);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lblArea);
            this.splitContainer1.Panel1.Controls.Add(this.lblFormulario);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(781, 428);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.White;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 184);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(781, 3);
            this.splitter1.TabIndex = 124;
            this.splitter1.TabStop = false;
            // 
            // txtDescripcionReporte
            // 
            this.txtDescripcionReporte.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDescripcionReporte.Lines = new string[0];
            this.txtDescripcionReporte.Location = new System.Drawing.Point(120, 148);
            this.txtDescripcionReporte.MaxLength = 32767;
            this.txtDescripcionReporte.Name = "txtDescripcionReporte";
            this.txtDescripcionReporte.PasswordChar = '\0';
            this.txtDescripcionReporte.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescripcionReporte.SelectedText = "";
            this.txtDescripcionReporte.Size = new System.Drawing.Size(294, 29);
            this.txtDescripcionReporte.Style = MetroFramework.MetroColorStyle.Red;
            this.txtDescripcionReporte.TabIndex = 123;
            this.txtDescripcionReporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtDescripcionReporte.UseSelectable = true;
            this.txtDescripcionReporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcionReporte_KeyPress);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDescripcion.Location = new System.Drawing.Point(17, 156);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(95, 13);
            this.lblDescripcion.TabIndex = 122;
            this.lblDescripcion.Text = "DESCRIPCION:";
            // 
            // btnModificar
            // 
            this.btnModificar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnModificar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnModificar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnModificar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Appearance.Options.UseBackColor = true;
            this.btnModificar.Appearance.Options.UseBorderColor = true;
            this.btnModificar.Appearance.Options.UseFont = true;
            this.btnModificar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnModificar.Location = new System.Drawing.Point(711, 137);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(40, 37);
            this.btnModificar.TabIndex = 121;
            this.btnModificar.ToolTip = "Modificar Reporte";
            // 
            // txtCodigoReporte
            // 
            this.txtCodigoReporte.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCodigoReporte.Lines = new string[] {
        "0000000"};
            this.txtCodigoReporte.Location = new System.Drawing.Point(120, 80);
            this.txtCodigoReporte.MaxLength = 32767;
            this.txtCodigoReporte.Name = "txtCodigoReporte";
            this.txtCodigoReporte.PasswordChar = '\0';
            this.txtCodigoReporte.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCodigoReporte.SelectedText = "";
            this.txtCodigoReporte.Size = new System.Drawing.Size(96, 29);
            this.txtCodigoReporte.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCodigoReporte.TabIndex = 120;
            this.txtCodigoReporte.Text = "0000000";
            this.txtCodigoReporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCodigoReporte.UseSelectable = true;
            // 
            // lblCodigoReporte
            // 
            this.lblCodigoReporte.AutoSize = true;
            this.lblCodigoReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoReporte.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCodigoReporte.Location = new System.Drawing.Point(8, 89);
            this.lblCodigoReporte.Name = "lblCodigoReporte";
            this.lblCodigoReporte.Size = new System.Drawing.Size(104, 13);
            this.lblCodigoReporte.TabIndex = 118;
            this.lblCodigoReporte.Text = "COD. REPORTE:";
            // 
            // txtReporte
            // 
            this.txtReporte.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtReporte.Lines = new string[0];
            this.txtReporte.Location = new System.Drawing.Point(120, 115);
            this.txtReporte.MaxLength = 32767;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.PasswordChar = '\0';
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtReporte.SelectedText = "";
            this.txtReporte.Size = new System.Drawing.Size(294, 29);
            this.txtReporte.Style = MetroFramework.MetroColorStyle.Red;
            this.txtReporte.TabIndex = 117;
            this.txtReporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtReporte.UseSelectable = true;
            this.txtReporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReporte_KeyPress);
            // 
            // lblReporte
            // 
            this.lblReporte.AutoSize = true;
            this.lblReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReporte.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblReporte.Location = new System.Drawing.Point(41, 123);
            this.lblReporte.Name = "lblReporte";
            this.lblReporte.Size = new System.Drawing.Size(70, 13);
            this.lblReporte.TabIndex = 116;
            this.lblReporte.Text = "REPORTE:";
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
            this.btnGuardar.Location = new System.Drawing.Point(659, 137);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(39, 36);
            this.btnGuardar.TabIndex = 104;
            this.btnGuardar.ToolTip = "Guardar cambios";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cboIdArea
            // 
            this.cboIdArea.FormattingEnabled = true;
            this.cboIdArea.ItemHeight = 23;
            this.cboIdArea.Location = new System.Drawing.Point(336, 45);
            this.cboIdArea.Name = "cboIdArea";
            this.cboIdArea.Size = new System.Drawing.Size(78, 29);
            this.cboIdArea.Style = MetroFramework.MetroColorStyle.Red;
            this.cboIdArea.TabIndex = 103;
            this.cboIdArea.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboIdArea.UseSelectable = true;
            // 
            // cboEstado
            // 
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.ItemHeight = 23;
            this.cboEstado.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cboEstado.Location = new System.Drawing.Point(292, 79);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(123, 29);
            this.cboEstado.Style = MetroFramework.MetroColorStyle.Red;
            this.cboEstado.TabIndex = 102;
            this.cboEstado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboEstado.UseSelectable = true;
            // 
            // cboArea
            // 
            this.cboArea.FormattingEnabled = true;
            this.cboArea.ItemHeight = 23;
            this.cboArea.Location = new System.Drawing.Point(120, 45);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(210, 29);
            this.cboArea.Style = MetroFramework.MetroColorStyle.Red;
            this.cboArea.TabIndex = 101;
            this.cboArea.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboArea.UseSelectable = true;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // txtFormulario
            // 
            this.txtFormulario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtFormulario.Lines = new string[0];
            this.txtFormulario.Location = new System.Drawing.Point(120, 10);
            this.txtFormulario.MaxLength = 32767;
            this.txtFormulario.Name = "txtFormulario";
            this.txtFormulario.PasswordChar = '\0';
            this.txtFormulario.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFormulario.SelectedText = "";
            this.txtFormulario.Size = new System.Drawing.Size(294, 29);
            this.txtFormulario.Style = MetroFramework.MetroColorStyle.Red;
            this.txtFormulario.TabIndex = 100;
            this.txtFormulario.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtFormulario.UseSelectable = true;
            this.txtFormulario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFormulario_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(222, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "ESTADO:";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArea.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblArea.Location = new System.Drawing.Point(66, 55);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(44, 13);
            this.lblArea.TabIndex = 28;
            this.lblArea.Text = "AREA:";
            // 
            // lblFormulario
            // 
            this.lblFormulario.AutoSize = true;
            this.lblFormulario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormulario.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFormulario.Location = new System.Drawing.Point(20, 18);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(92, 13);
            this.lblFormulario.TabIndex = 27;
            this.lblFormulario.Text = "FORMULARIO:";
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
            this.splitContainer2.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer2.Panel1.Controls.Add(this.txtBuscarNombreReporte);
            this.splitContainer2.Panel1.Controls.Add(this.txtBuscarCodigoReporte);
            this.splitContainer2.Panel1.Controls.Add(this.lblReporteCodigo);
            this.splitContainer2.Panel1.Controls.Add(this.btnBuscar);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dtgvReportes);
            this.splitContainer2.Size = new System.Drawing.Size(781, 237);
            this.splitContainer2.SplitterDistance = 43;
            this.splitContainer2.TabIndex = 0;
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
            this.btnExcel.Location = new System.Drawing.Point(711, 2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 115;
            this.btnExcel.ToolTip = "Exportar a Excel";
            // 
            // txtBuscarNombreReporte
            // 
            this.txtBuscarNombreReporte.Location = new System.Drawing.Point(238, 11);
            this.txtBuscarNombreReporte.Name = "txtBuscarNombreReporte";
            this.txtBuscarNombreReporte.Size = new System.Drawing.Size(282, 20);
            this.txtBuscarNombreReporte.TabIndex = 24;
            // 
            // txtBuscarCodigoReporte
            // 
            this.txtBuscarCodigoReporte.Location = new System.Drawing.Point(129, 12);
            this.txtBuscarCodigoReporte.Name = "txtBuscarCodigoReporte";
            this.txtBuscarCodigoReporte.Size = new System.Drawing.Size(100, 20);
            this.txtBuscarCodigoReporte.TabIndex = 23;
            // 
            // lblReporteCodigo
            // 
            this.lblReporteCodigo.AutoSize = true;
            this.lblReporteCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReporteCodigo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblReporteCodigo.Location = new System.Drawing.Point(19, 15);
            this.lblReporteCodigo.Name = "lblReporteCodigo";
            this.lblReporteCodigo.Size = new System.Drawing.Size(104, 13);
            this.lblReporteCodigo.TabIndex = 22;
            this.lblReporteCodigo.Text = "COD. REPORTE:";
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
            this.btnBuscar.Location = new System.Drawing.Point(647, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(58, 36);
            this.btnBuscar.TabIndex = 114;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgvReportes
            // 
            this.dtgvReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvReportes.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvReportes.Location = new System.Drawing.Point(0, 0);
            this.dtgvReportes.LookAndFeel.SkinName = "Darkroom";
            this.dtgvReportes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvReportes.MainView = this.dtgvReportesView;
            this.dtgvReportes.Name = "dtgvReportes";
            this.dtgvReportes.Size = new System.Drawing.Size(781, 190);
            this.dtgvReportes.TabIndex = 9;
            this.dtgvReportes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvReportesView});
            // 
            // dtgvReportesView
            // 
            this.dtgvReportesView.GridControl = this.dtgvReportes;
            this.dtgvReportesView.Name = "dtgvReportesView";
            this.dtgvReportesView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvReportesView.OptionsBehavior.Editable = false;
            this.dtgvReportesView.OptionsView.ColumnAutoWidth = false;
            this.dtgvReportesView.OptionsView.ShowFooter = true;
            this.dtgvReportesView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dtgvReportesView_RowClick);
            // 
            // NuevosReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 508);
            this.Controls.Add(this.splitContainer1);
            this.Name = "NuevosReportes";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Registro de Nuevos Reportes";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.NuevosReportes_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReportes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReportesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblFormulario;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox txtFormulario;
        private MetroFramework.Controls.MetroComboBox cboEstado;
        private MetroFramework.Controls.MetroComboBox cboArea;
        private MetroFramework.Controls.MetroComboBox cboIdArea;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroTextBox txtReporte;
        private System.Windows.Forms.Label lblReporte;
        private MetroFramework.Controls.MetroTextBox txtCodigoReporte;
        private System.Windows.Forms.Label lblCodigoReporte;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraGrid.GridControl dtgvReportes;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvReportesView;
        private System.Windows.Forms.TextBox txtBuscarNombreReporte;
        private System.Windows.Forms.TextBox txtBuscarCodigoReporte;
        private System.Windows.Forms.Label lblReporteCodigo;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnModificar;
        private MetroFramework.Controls.MetroTextBox txtDescripcionReporte;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Splitter splitter1;
    }
}