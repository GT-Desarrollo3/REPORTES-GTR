namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    partial class Servicios
    {
        private System.Windows.Forms.ListView lvProveedor;
        private MetroFramework.Controls.MetroTextBox txtProveedor;
        private MetroFramework.Controls.MetroComboBox cboCompania;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TextBox txtMonModificar;
        private System.Windows.Forms.TextBox TxtMonActual;
        private System.Windows.Forms.TextBox txtOST;

        #region Windows Form Designer generated code


        #endregion

        private System.Windows.Forms.Button btnaceptar;
        private System.Windows.Forms.Button BtnCancelar;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Servicios));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.lvProveedor = new System.Windows.Forms.ListView();
            this.txtProveedor = new MetroFramework.Controls.MetroTextBox();
            this.cboCompania = new MetroFramework.Controls.MetroComboBox();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnaceptar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.txtMonModificar = new System.Windows.Forms.TextBox();
            this.TxtMonActual = new System.Windows.Forms.TextBox();
            this.txtOST = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ModificarOST = new System.Windows.Forms.Button();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cobTipMoneda = new System.Windows.Forms.ComboBox();
            this.TxtIGV = new System.Windows.Forms.Label();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // lvProveedor
            // 
            this.lvProveedor.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lvProveedor.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lvProveedor.FullRowSelect = true;
            this.lvProveedor.GridLines = true;
            this.lvProveedor.Location = new System.Drawing.Point(618, 31);
            this.lvProveedor.MultiSelect = false;
            this.lvProveedor.Name = "lvProveedor";
            this.lvProveedor.Size = new System.Drawing.Size(320, 268);
            this.lvProveedor.TabIndex = 88;
            this.lvProveedor.UseCompatibleStateImageBehavior = false;
            this.lvProveedor.View = System.Windows.Forms.View.Details;
            this.lvProveedor.Visible = false;
            this.lvProveedor.Enter += new System.EventHandler(this.lvProveedor_Enter);
            this.lvProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvProveedor_KeyPress);
            this.lvProveedor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvProveedor_MouseDoubleClick);
            // 
            // txtProveedor
            // 
            this.txtProveedor.Lines = new string[0];
            this.txtProveedor.Location = new System.Drawing.Point(618, 3);
            this.txtProveedor.MaxLength = 32767;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.PasswordChar = '\0';
            this.txtProveedor.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProveedor.SelectedText = "";
            this.txtProveedor.Size = new System.Drawing.Size(320, 29);
            this.txtProveedor.Style = MetroFramework.MetroColorStyle.Red;
            this.txtProveedor.TabIndex = 90;
            this.txtProveedor.UseSelectable = true;
            this.txtProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProveedor_KeyPress);
            // 
            // cboCompania
            // 
            this.cboCompania.FormattingEnabled = true;
            this.cboCompania.ItemHeight = 23;
            this.cboCompania.Items.AddRange(new object[] {
            "TODAS",
            "GRUPO TRANSPESA SAC",
            "FABRICACIONES BRA S.A.C."});
            this.cboCompania.Location = new System.Drawing.Point(80, 3);
            this.cboCompania.Name = "cboCompania";
            this.cboCompania.Size = new System.Drawing.Size(175, 29);
            this.cboCompania.TabIndex = 73;
            this.cboCompania.UseSelectable = true;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(450, 5);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(84, 25);
            this.dtpFechaFin.TabIndex = 71;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(312, 5);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(87, 25);
            this.dtpFechaIni.TabIndex = 70;
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
            this.btnImprimir.Location = new System.Drawing.Point(1056, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 58;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(1001, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 57;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Appearance.Options.UseForeColor = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(944, 3);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 50;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnaceptar
            // 
            this.btnaceptar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnaceptar.Location = new System.Drawing.Point(351, 225);
            this.btnaceptar.Name = "btnaceptar";
            this.btnaceptar.Size = new System.Drawing.Size(95, 31);
            this.btnaceptar.TabIndex = 97;
            this.btnaceptar.Text = "Aceptar";
            this.btnaceptar.UseVisualStyleBackColor = true;
            this.btnaceptar.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnCancelar.Location = new System.Drawing.Point(98, 225);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(100, 31);
            this.BtnCancelar.TabIndex = 96;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // txtMonModificar
            // 
            this.txtMonModificar.Location = new System.Drawing.Point(174, 113);
            this.txtMonModificar.Name = "txtMonModificar";
            this.txtMonModificar.Size = new System.Drawing.Size(206, 20);
            this.txtMonModificar.TabIndex = 95;
            this.txtMonModificar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonModificar_KeyPress);
            // 
            // TxtMonActual
            // 
            this.TxtMonActual.Location = new System.Drawing.Point(174, 77);
            this.TxtMonActual.Name = "TxtMonActual";
            this.TxtMonActual.ReadOnly = true;
            this.TxtMonActual.Size = new System.Drawing.Size(206, 20);
            this.TxtMonActual.TabIndex = 94;
            // 
            // txtOST
            // 
            this.txtOST.Location = new System.Drawing.Point(174, 41);
            this.txtOST.Name = "txtOST";
            this.txtOST.ReadOnly = true;
            this.txtOST.Size = new System.Drawing.Size(206, 20);
            this.txtOST.TabIndex = 93;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.splitContainer1.Panel1.Controls.Add(this.lvProveedor);
            this.splitContainer1.Panel1.Controls.Add(this.ModificarOST);
            this.splitContainer1.Panel1.Controls.Add(this.txtProveedor);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel12);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompania);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.splitContainer1.Panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainer1.Panel2.BackgroundImage")));
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(1544, 712);
            this.splitContainer1.SplitterDistance = 45;
            this.splitContainer1.TabIndex = 1;
            // 
            // ModificarOST
            // 
            this.ModificarOST.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ModificarOST.Location = new System.Drawing.Point(1124, 7);
            this.ModificarOST.Name = "ModificarOST";
            this.ModificarOST.Size = new System.Drawing.Size(126, 36);
            this.ModificarOST.TabIndex = 2;
            this.ModificarOST.Text = "Modificar OST";
            this.ModificarOST.UseVisualStyleBackColor = true;
            this.ModificarOST.Click += new System.EventHandler(this.ModificarOST_Click);
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel12.Location = new System.Drawing.Point(540, 7);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(72, 19);
            this.metroLabel12.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel12.TabIndex = 91;
            this.metroLabel12.Text = "Proveedor";
            this.metroLabel12.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(261, 7);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(45, 19);
            this.metroLabel2.TabIndex = 89;
            this.metroLabel2.Text = "Desde";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(405, 7);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(39, 19);
            this.metroLabel1.TabIndex = 72;
            this.metroLabel1.Text = "hasta";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(3, 7);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(71, 19);
            this.metroLabel3.TabIndex = 52;
            this.metroLabel3.Text = "Compañía";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cobTipMoneda);
            this.groupBox1.Controls.Add(this.TxtIGV);
            this.groupBox1.Controls.Add(this.btnaceptar);
            this.groupBox1.Controls.Add(this.BtnCancelar);
            this.groupBox1.Controls.Add(this.txtMonModificar);
            this.groupBox1.Controls.Add(this.TxtMonActual);
            this.groupBox1.Controls.Add(this.txtOST);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(352, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 284);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modificar OST";
            this.groupBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 103;
            this.label4.Text = "Monto Modificar:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "Monto Actual:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "OST:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Tipo de Moneda:";
            // 
            // cobTipMoneda
            // 
            this.cobTipMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobTipMoneda.FormattingEnabled = true;
            this.cobTipMoneda.Items.AddRange(new object[] {
            "SOLES",
            "DOLARES"});
            this.cobTipMoneda.Location = new System.Drawing.Point(174, 151);
            this.cobTipMoneda.Name = "cobTipMoneda";
            this.cobTipMoneda.Size = new System.Drawing.Size(169, 21);
            this.cobTipMoneda.TabIndex = 99;
            // 
            // TxtIGV
            // 
            this.TxtIGV.AutoSize = true;
            this.TxtIGV.BackColor = System.Drawing.Color.Transparent;
            this.TxtIGV.Location = new System.Drawing.Point(386, 116);
            this.TxtIGV.Name = "TxtIGV";
            this.TxtIGV.Size = new System.Drawing.Size(48, 13);
            this.TxtIGV.TabIndex = 98;
            this.TxtIGV.Text = "Mas IGV";
            // 
            // dtgvData
            // 
            this.dtgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvData.Location = new System.Drawing.Point(12, 12);
            this.dtgvData.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(1516, 637);
            this.dtgvData.TabIndex = 1;
            this.dtgvData.TabStop = false;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowGroupPanel = false;
            // 
            // Servicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackImage = ((System.Drawing.Image)(resources.GetObject("$this.BackImage")));
            this.ClientSize = new System.Drawing.Size(1544, 742);
            this.Controls.Add(this.splitContainer1);
            this.DisplayHeader = false;
            this.DoubleBuffered = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Servicios";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Ordenes de Servicios";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Servicios_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label TxtIGV;
        private System.Windows.Forms.Button ModificarOST;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobTipMoneda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    
    
    }

    
}