namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class AprobacionPlanillasMultiple
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AprobacionPlanillasMultiple));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtDocumento = new MetroFramework.Controls.MetroTextBox();
            this.lblDocumento = new MetroFramework.Controls.MetroLabel();
            this.txtAdelanto = new MetroFramework.Controls.MetroTextBox();
            this.lblAdelanto = new MetroFramework.Controls.MetroLabel();
            this.cboUsuario = new MetroFramework.Controls.MetroComboBox();
            this.lblUsuario = new MetroFramework.Controls.MetroLabel();
            this.txtCliente = new MetroFramework.Controls.MetroTextBox();
            this.lblProveedor = new MetroFramework.Controls.MetroLabel();
            this.lblFechaIni = new MetroFramework.Controls.MetroLabel();
            this.lblFechaFin = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.lvCliente = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Por_Aprobar = new System.Windows.Forms.TabPage();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Aprobadas = new System.Windows.Forms.TabPage();
            this.dtgvDataAprobadas = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataAprobadasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Por_Aprobar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            this.Aprobadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataAprobadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataAprobadasView)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtDocumento);
            this.splitContainer1.Panel1.Controls.Add(this.lblDocumento);
            this.splitContainer1.Panel1.Controls.Add(this.txtAdelanto);
            this.splitContainer1.Panel1.Controls.Add(this.lblAdelanto);
            this.splitContainer1.Panel1.Controls.Add(this.cboUsuario);
            this.splitContainer1.Panel1.Controls.Add(this.lblUsuario);
            this.splitContainer1.Panel1.Controls.Add(this.txtCliente);
            this.splitContainer1.Panel1.Controls.Add(this.lblProveedor);
            this.splitContainer1.Panel1.Controls.Add(this.lblFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.lblFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.btnGuardar);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.lvCliente);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1128, 473);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtDocumento
            // 
            this.txtDocumento.Lines = new string[0];
            this.txtDocumento.Location = new System.Drawing.Point(742, 46);
            this.txtDocumento.MaxLength = 32767;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.PasswordChar = '\0';
            this.txtDocumento.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDocumento.SelectedText = "";
            this.txtDocumento.Size = new System.Drawing.Size(109, 29);
            this.txtDocumento.Style = MetroFramework.MetroColorStyle.Red;
            this.txtDocumento.TabIndex = 127;
            this.txtDocumento.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtDocumento.UseSelectable = true;
            this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblDocumento.Location = new System.Drawing.Point(653, 51);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(88, 19);
            this.lblDocumento.Style = MetroFramework.MetroColorStyle.Red;
            this.lblDocumento.TabIndex = 126;
            this.lblDocumento.Text = "Documento :";
            this.lblDocumento.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtAdelanto
            // 
            this.txtAdelanto.Lines = new string[0];
            this.txtAdelanto.Location = new System.Drawing.Point(730, 14);
            this.txtAdelanto.MaxLength = 32767;
            this.txtAdelanto.Name = "txtAdelanto";
            this.txtAdelanto.PasswordChar = '\0';
            this.txtAdelanto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAdelanto.SelectedText = "";
            this.txtAdelanto.Size = new System.Drawing.Size(109, 29);
            this.txtAdelanto.Style = MetroFramework.MetroColorStyle.Red;
            this.txtAdelanto.TabIndex = 125;
            this.txtAdelanto.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtAdelanto.UseSelectable = true;
            // 
            // lblAdelanto
            // 
            this.lblAdelanto.AutoSize = true;
            this.lblAdelanto.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblAdelanto.Location = new System.Drawing.Point(653, 19);
            this.lblAdelanto.Name = "lblAdelanto";
            this.lblAdelanto.Size = new System.Drawing.Size(71, 19);
            this.lblAdelanto.Style = MetroFramework.MetroColorStyle.Red;
            this.lblAdelanto.TabIndex = 124;
            this.lblAdelanto.Text = "Adelanto :";
            this.lblAdelanto.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cboUsuario
            // 
            this.cboUsuario.FormattingEnabled = true;
            this.cboUsuario.ItemHeight = 23;
            this.cboUsuario.Items.AddRange(new object[] {
            "TODOS",
            "AQUITO",
            "DCASTILLO"});
            this.cboUsuario.Location = new System.Drawing.Point(285, 44);
            this.cboUsuario.Name = "cboUsuario";
            this.cboUsuario.Size = new System.Drawing.Size(121, 29);
            this.cboUsuario.Style = MetroFramework.MetroColorStyle.Red;
            this.cboUsuario.TabIndex = 114;
            this.cboUsuario.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboUsuario.UseSelectable = true;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblUsuario.Location = new System.Drawing.Point(213, 49);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(63, 19);
            this.lblUsuario.Style = MetroFramework.MetroColorStyle.Red;
            this.lblUsuario.TabIndex = 113;
            this.lblUsuario.Text = "Usuario: ";
            this.lblUsuario.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtCliente
            // 
            this.txtCliente.Lines = new string[0];
            this.txtCliente.Location = new System.Drawing.Point(285, 10);
            this.txtCliente.MaxLength = 32767;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PasswordChar = '\0';
            this.txtCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCliente.SelectedText = "";
            this.txtCliente.Size = new System.Drawing.Size(347, 29);
            this.txtCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCliente.TabIndex = 112;
            this.txtCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCliente.UseSelectable = true;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblProveedor.Location = new System.Drawing.Point(206, 15);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(79, 19);
            this.lblProveedor.Style = MetroFramework.MetroColorStyle.Red;
            this.lblProveedor.TabIndex = 111;
            this.lblProveedor.Text = "Proveedor: ";
            this.lblProveedor.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblFechaIni
            // 
            this.lblFechaIni.AutoSize = true;
            this.lblFechaIni.Location = new System.Drawing.Point(15, 19);
            this.lblFechaIni.Name = "lblFechaIni";
            this.lblFechaIni.Size = new System.Drawing.Size(84, 19);
            this.lblFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFechaIni.TabIndex = 110;
            this.lblFechaIni.Text = "Fecha Inicio :";
            this.lblFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Location = new System.Drawing.Point(28, 47);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(71, 19);
            this.lblFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFechaFin.TabIndex = 109;
            this.lblFechaFin.Text = "Fecha Fin :";
            this.lblFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(105, 45);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(87, 25);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 108;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(105, 17);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(87, 25);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 107;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnGuardar.Location = new System.Drawing.Point(998, 21);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(40, 37);
            this.btnGuardar.TabIndex = 94;
            this.btnGuardar.ToolTip = "Aprueba Obligaciones";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(1063, 19);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 54;
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
            this.btnBuscar.Location = new System.Drawing.Point(929, 11);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(58, 54);
            this.btnBuscar.TabIndex = 53;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lvCliente
            // 
            this.lvCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.lvCliente.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lvCliente.FullRowSelect = true;
            this.lvCliente.GridLines = true;
            this.lvCliente.Location = new System.Drawing.Point(285, 42);
            this.lvCliente.MultiSelect = false;
            this.lvCliente.Name = "lvCliente";
            this.lvCliente.Size = new System.Drawing.Size(347, 261);
            this.lvCliente.TabIndex = 123;
            this.lvCliente.UseCompatibleStateImageBehavior = false;
            this.lvCliente.View = System.Windows.Forms.View.Details;
            this.lvCliente.Visible = false;
            this.lvCliente.Enter += new System.EventHandler(this.lvCliente_Enter);
            this.lvCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvCliente_KeyPress);
            this.lvCliente.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvCliente_MouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Por_Aprobar);
            this.tabControl1.Controls.Add(this.Aprobadas);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1128, 396);
            this.tabControl1.TabIndex = 115;
            // 
            // Por_Aprobar
            // 
            this.Por_Aprobar.Controls.Add(this.dtgvData);
            this.Por_Aprobar.Location = new System.Drawing.Point(4, 22);
            this.Por_Aprobar.Name = "Por_Aprobar";
            this.Por_Aprobar.Padding = new System.Windows.Forms.Padding(3);
            this.Por_Aprobar.Size = new System.Drawing.Size(1120, 370);
            this.Por_Aprobar.TabIndex = 0;
            this.Por_Aprobar.Text = "Planillas Por Aprobar";
            this.Por_Aprobar.UseVisualStyleBackColor = true;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvData.Location = new System.Drawing.Point(3, 3);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(1114, 364);
            this.dtgvData.TabIndex = 115;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            this.dtgvData.Click += new System.EventHandler(this.dtgvData_Click);
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsSelection.MultiSelect = true;
            this.dtgvDataView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowAutoFilterRow = true;
            this.dtgvDataView.OptionsView.ShowFooter = true;
            // 
            // Aprobadas
            // 
            this.Aprobadas.Controls.Add(this.dtgvDataAprobadas);
            this.Aprobadas.Location = new System.Drawing.Point(4, 22);
            this.Aprobadas.Name = "Aprobadas";
            this.Aprobadas.Padding = new System.Windows.Forms.Padding(3);
            this.Aprobadas.Size = new System.Drawing.Size(1120, 370);
            this.Aprobadas.TabIndex = 1;
            this.Aprobadas.Text = "Planillas Aprobadas";
            this.Aprobadas.UseVisualStyleBackColor = true;
            // 
            // dtgvDataAprobadas
            // 
            this.dtgvDataAprobadas.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.dtgvDataAprobadas.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvDataAprobadas.Location = new System.Drawing.Point(3, 3);
            this.dtgvDataAprobadas.LookAndFeel.SkinName = "Darkroom";
            this.dtgvDataAprobadas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvDataAprobadas.MainView = this.dtgvDataAprobadasView;
            this.dtgvDataAprobadas.Name = "dtgvDataAprobadas";
            this.dtgvDataAprobadas.Size = new System.Drawing.Size(1114, 364);
            this.dtgvDataAprobadas.TabIndex = 116;
            this.dtgvDataAprobadas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataAprobadasView});
            this.dtgvDataAprobadas.Click += new System.EventHandler(this.dtgvDataAprobadas_Click);
            // 
            // dtgvDataAprobadasView
            // 
            this.dtgvDataAprobadasView.GridControl = this.dtgvDataAprobadas;
            this.dtgvDataAprobadasView.Name = "dtgvDataAprobadasView";
            this.dtgvDataAprobadasView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataAprobadasView.OptionsBehavior.Editable = false;
            this.dtgvDataAprobadasView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataAprobadasView.OptionsView.ShowAutoFilterRow = true;
            this.dtgvDataAprobadasView.OptionsView.ShowFooter = true;
            // 
            // AprobacionPlanillasMultiple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 553);
            this.Controls.Add(this.splitContainer1);
            this.Name = "AprobacionPlanillasMultiple";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Aprobación de Planillas";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AprobacionPlanillasMultiple_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Por_Aprobar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.Aprobadas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataAprobadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataAprobadasView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private MetroFramework.Controls.MetroLabel lblFechaIni;
        private MetroFramework.Controls.MetroLabel lblFechaFin;
        private MetroFramework.Controls.MetroTextBox txtCliente;
        private MetroFramework.Controls.MetroLabel lblProveedor;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Por_Aprobar;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private System.Windows.Forms.TabPage Aprobadas;
        public DevExpress.XtraGrid.GridControl dtgvDataAprobadas;
        public DevExpress.XtraGrid.Views.Grid.GridView dtgvDataAprobadasView;
        public MetroFramework.Controls.MetroDateTime dtpFechaFin;
        public MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel lblUsuario;
        private MetroFramework.Controls.MetroComboBox cboUsuario;
        private System.Windows.Forms.ListView lvCliente;
        private MetroFramework.Controls.MetroTextBox txtAdelanto;
        private MetroFramework.Controls.MetroLabel lblAdelanto;
        private MetroFramework.Controls.MetroTextBox txtDocumento;
        private MetroFramework.Controls.MetroLabel lblDocumento;
    }
}