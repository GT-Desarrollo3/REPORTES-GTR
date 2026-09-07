namespace ReportesTranspesa.Formularios.Areas.Seguridad.MedicoOcupacional
{
    partial class frmListaEMO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaEMO));
            this.label1 = new System.Windows.Forms.Label();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabRegistro = new System.Windows.Forms.TabPage();
            this.dtgDocumentosEMO = new DevExpress.XtraGrid.GridControl();
            this.dgvDocumentosEMO = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnNuevoDocumento = new DevExpress.XtraEditors.SimpleButton();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.gbLeyenda = new System.Windows.Forms.GroupBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.tabHistorial = new System.Windows.Forms.TabPage();
            this.dtgHistorialEMO = new DevExpress.XtraGrid.GridControl();
            this.dgvHistorialEMO = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConductorH = new System.Windows.Forms.TextBox();
            this.btnExcelH = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscarH = new DevExpress.XtraEditors.SimpleButton();
            this.tabInfo.SuspendLayout();
            this.tabRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocumentosEMO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosEMO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbLeyenda.SuspendLayout();
            this.tabHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialEMO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialEMO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1128, 48);
            this.label1.TabIndex = 21;
            this.label1.Text = "DOCUMENTOS DE MÉDICO OCUPACIONAL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabRegistro);
            this.tabInfo.Controls.Add(this.tabHistorial);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfo.Location = new System.Drawing.Point(0, 48);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(1128, 501);
            this.tabInfo.TabIndex = 238;
            // 
            // tabRegistro
            // 
            this.tabRegistro.Controls.Add(this.dtgDocumentosEMO);
            this.tabRegistro.Controls.Add(this.panel3);
            this.tabRegistro.Location = new System.Drawing.Point(4, 33);
            this.tabRegistro.Name = "tabRegistro";
            this.tabRegistro.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegistro.Size = new System.Drawing.Size(1120, 464);
            this.tabRegistro.TabIndex = 0;
            this.tabRegistro.Text = "Registro";
            this.tabRegistro.UseVisualStyleBackColor = true;
            // 
            // dtgDocumentosEMO
            // 
            this.dtgDocumentosEMO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgDocumentosEMO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDocumentosEMO.Location = new System.Drawing.Point(3, 105);
            this.dtgDocumentosEMO.MainView = this.dgvDocumentosEMO;
            this.dtgDocumentosEMO.Name = "dtgDocumentosEMO";
            this.dtgDocumentosEMO.Size = new System.Drawing.Size(1114, 356);
            this.dtgDocumentosEMO.TabIndex = 186;
            this.dtgDocumentosEMO.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvDocumentosEMO,
            this.gridView1});
            this.dtgDocumentosEMO.DoubleClick += new System.EventHandler(this.dtgDocumentosEMO_DoubleClick);
            // 
            // dgvDocumentosEMO
            // 
            this.dgvDocumentosEMO.GridControl = this.dtgDocumentosEMO;
            this.dgvDocumentosEMO.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvDocumentosEMO.Name = "dgvDocumentosEMO";
            this.dgvDocumentosEMO.OptionsBehavior.Editable = false;
            this.dgvDocumentosEMO.OptionsBehavior.ReadOnly = true;
            this.dgvDocumentosEMO.OptionsView.ColumnAutoWidth = false;
            this.dgvDocumentosEMO.OptionsView.RowAutoHeight = true;
            this.dgvDocumentosEMO.OptionsView.ShowFooter = true;
            this.dgvDocumentosEMO.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvDocumentosEMO_CustomDrawCell);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgDocumentosEMO;
            this.gridView1.Name = "gridView1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.btnNuevoDocumento);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.gbLeyenda);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1114, 102);
            this.panel3.TabIndex = 185;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(891, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Vencido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(855, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 237;
            this.label4.Text = "Leyenda:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Red;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(858, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "       ";
            // 
            // btnNuevoDocumento
            // 
            this.btnNuevoDocumento.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoDocumento.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoDocumento.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoDocumento.Appearance.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.btnNuevoDocumento.Appearance.Options.UseBackColor = true;
            this.btnNuevoDocumento.Appearance.Options.UseBorderColor = true;
            this.btnNuevoDocumento.Appearance.Options.UseFont = true;
            this.btnNuevoDocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoDocumento.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoDocumento.Image")));
            this.btnNuevoDocumento.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoDocumento.Location = new System.Drawing.Point(25, 29);
            this.btnNuevoDocumento.Name = "btnNuevoDocumento";
            this.btnNuevoDocumento.Size = new System.Drawing.Size(111, 47);
            this.btnNuevoDocumento.TabIndex = 236;
            this.btnNuevoDocumento.Tag = "5";
            this.btnNuevoDocumento.Text = "Agregar\r\nDocumento";
            this.btnNuevoDocumento.ToolTip = "Agregar Documento";
            this.btnNuevoDocumento.Click += new System.EventHandler(this.btnNuevoDocumento_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(891, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Por vencer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtConductor);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(159, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 58);
            this.groupBox2.TabIndex = 220;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar Empleado: ";
            // 
            // txtConductor
            // 
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductor.Location = new System.Drawing.Point(17, 24);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(323, 20);
            this.txtConductor.TabIndex = 204;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Yellow;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(858, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "       ";
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(781, 28);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 197;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(891, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vigente";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(725, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.YellowGreen;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(858, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "       ";
            // 
            // gbLeyenda
            // 
            this.gbLeyenda.Controls.Add(this.cbxEstado);
            this.gbLeyenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLeyenda.Location = new System.Drawing.Point(536, 21);
            this.gbLeyenda.Name = "gbLeyenda";
            this.gbLeyenda.Size = new System.Drawing.Size(149, 58);
            this.gbLeyenda.TabIndex = 221;
            this.gbLeyenda.TabStop = false;
            this.gbLeyenda.Text = "Estado: ";
            // 
            // cbxEstado
            // 
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODOS",
            "VIGENTE",
            "POR VENCER",
            "VENCIDO"});
            this.cbxEstado.Location = new System.Drawing.Point(15, 24);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(117, 21);
            this.cbxEstado.TabIndex = 5;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // tabHistorial
            // 
            this.tabHistorial.Controls.Add(this.dtgHistorialEMO);
            this.tabHistorial.Controls.Add(this.panel1);
            this.tabHistorial.Location = new System.Drawing.Point(4, 33);
            this.tabHistorial.Name = "tabHistorial";
            this.tabHistorial.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistorial.Size = new System.Drawing.Size(1120, 464);
            this.tabHistorial.TabIndex = 1;
            this.tabHistorial.Text = "Historial";
            this.tabHistorial.UseVisualStyleBackColor = true;
            // 
            // dtgHistorialEMO
            // 
            this.dtgHistorialEMO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgHistorialEMO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgHistorialEMO.Location = new System.Drawing.Point(3, 105);
            this.dtgHistorialEMO.MainView = this.dgvHistorialEMO;
            this.dtgHistorialEMO.Name = "dtgHistorialEMO";
            this.dtgHistorialEMO.Size = new System.Drawing.Size(1114, 356);
            this.dtgHistorialEMO.TabIndex = 187;
            this.dtgHistorialEMO.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvHistorialEMO,
            this.gridView3});
            this.dtgHistorialEMO.DoubleClick += new System.EventHandler(this.dtgHistorialEMO_DoubleClick);
            // 
            // dgvHistorialEMO
            // 
            this.dgvHistorialEMO.GridControl = this.dtgHistorialEMO;
            this.dgvHistorialEMO.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvHistorialEMO.Name = "dgvHistorialEMO";
            this.dgvHistorialEMO.OptionsBehavior.Editable = false;
            this.dgvHistorialEMO.OptionsBehavior.ReadOnly = true;
            this.dgvHistorialEMO.OptionsView.ColumnAutoWidth = false;
            this.dgvHistorialEMO.OptionsView.RowAutoHeight = true;
            this.dgvHistorialEMO.OptionsView.ShowFooter = true;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.dtgHistorialEMO;
            this.gridView3.Name = "gridView3";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnExcelH);
            this.panel1.Controls.Add(this.btnBuscarH);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1114, 102);
            this.panel1.TabIndex = 186;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.dtpFechaIni);
            this.groupBox3.Controls.Add(this.dtpFechaFin);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(408, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(254, 58);
            this.groupBox3.TabIndex = 221;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Buscar por Fecha Creación: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(119, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "--";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(17, 24);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 1;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(136, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 2;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConductorH);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(26, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 58);
            this.groupBox1.TabIndex = 220;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar Empleado: ";
            // 
            // txtConductorH
            // 
            this.txtConductorH.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductorH.Location = new System.Drawing.Point(17, 24);
            this.txtConductorH.Name = "txtConductorH";
            this.txtConductorH.Size = new System.Drawing.Size(323, 20);
            this.txtConductorH.TabIndex = 204;
            this.txtConductorH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductorH_KeyPress);
            // 
            // btnExcelH
            // 
            this.btnExcelH.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcelH.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcelH.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcelH.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelH.Appearance.Options.UseBackColor = true;
            this.btnExcelH.Appearance.Options.UseBorderColor = true;
            this.btnExcelH.Appearance.Options.UseFont = true;
            this.btnExcelH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelH.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelH.Image")));
            this.btnExcelH.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcelH.Location = new System.Drawing.Point(767, 28);
            this.btnExcelH.Name = "btnExcelH";
            this.btnExcelH.Size = new System.Drawing.Size(51, 47);
            this.btnExcelH.TabIndex = 197;
            this.btnExcelH.Tag = "6";
            this.btnExcelH.ToolTip = "Exportar a Excel";
            this.btnExcelH.Click += new System.EventHandler(this.btnExcelH_Click);
            // 
            // btnBuscarH
            // 
            this.btnBuscarH.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscarH.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscarH.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscarH.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarH.Appearance.Options.UseBackColor = true;
            this.btnBuscarH.Appearance.Options.UseBorderColor = true;
            this.btnBuscarH.Appearance.Options.UseFont = true;
            this.btnBuscarH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarH.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarH.Image")));
            this.btnBuscarH.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscarH.Location = new System.Drawing.Point(708, 28);
            this.btnBuscarH.Name = "btnBuscarH";
            this.btnBuscarH.Size = new System.Drawing.Size(47, 47);
            this.btnBuscarH.TabIndex = 196;
            this.btnBuscarH.Tag = "5";
            this.btnBuscarH.ToolTip = "Buscar";
            this.btnBuscarH.Click += new System.EventHandler(this.btnBuscarH_Click);
            // 
            // frmListaEMO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 549);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.label1);
            this.Name = "frmListaEMO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LISTA DE DOCUMENTOS DE MÉDICO OCUPACIONAL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaEMO_Load);
            this.tabInfo.ResumeLayout(false);
            this.tabRegistro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocumentosEMO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosEMO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbLeyenda.ResumeLayout(false);
            this.tabHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialEMO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialEMO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabRegistro;
        private System.Windows.Forms.TabPage tabHistorial;
        public DevExpress.XtraGrid.GridControl dtgDocumentosEMO;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDocumentosEMO;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton btnNuevoDocumento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label6;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbLeyenda;
        private System.Windows.Forms.ComboBox cbxEstado;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraGrid.GridControl dtgHistorialEMO;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvHistorialEMO;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtConductorH;
        public DevExpress.XtraEditors.SimpleButton btnExcelH;
        public DevExpress.XtraEditors.SimpleButton btnBuscarH;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
    }
}