namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmListaTarifasOT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaTarifasOT));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxCliente = new System.Windows.Forms.ComboBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.txtBuscarRuta = new System.Windows.Forms.TextBox();
            this.btnImportar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgTarifaOT = new DevExpress.XtraGrid.GridControl();
            this.dgvTarifaOTVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pNuevaTarifa = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvTarifasRuta = new System.Windows.Forms.DataGridView();
            this.btnBuscarArchivo = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDirectorio = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.metroLabel20 = new MetroFramework.Controls.MetroLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxMoneda = new System.Windows.Forms.ComboBox();
            this.btnGenerar = new DevExpress.XtraEditors.SimpleButton();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTarifaOT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifaOTVista)).BeginInit();
            this.pNuevaTarifa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifasRuta)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1143, 50);
            this.label1.TabIndex = 14;
            this.label1.Text = "REGISTRO DE TARIFAS DE OT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.btnImportar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1143, 119);
            this.panel4.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbxCliente);
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnExcel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dtpFechaInicio);
            this.groupBox2.Controls.Add(this.txtBuscarRuta);
            this.groupBox2.Location = new System.Drawing.Point(165, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(724, 92);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtro de Búsqueda:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.Location = new System.Drawing.Point(366, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 214;
            this.label9.Text = "Cliente:";
            // 
            // cbxCliente
            // 
            this.cbxCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCliente.FormattingEnabled = true;
            this.cbxCliente.Items.AddRange(new object[] {
            "LINDLEY",
            "LIMAGAS",
            "TOLVAS",
            "VOLCAN"});
            this.cbxCliente.Location = new System.Drawing.Point(414, 55);
            this.cbxCliente.Name = "cbxCliente";
            this.cbxCliente.Size = new System.Drawing.Size(149, 21);
            this.cbxCliente.TabIndex = 213;
            this.cbxCliente.DropDownClosed += new System.EventHandler(this.cbxCliente_DropDownClosed);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(222, 56);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 206;
            this.label4.Text = "Fecha Registro:";
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
            this.btnExcel.Location = new System.Drawing.Point(656, 25);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 212;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(205, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "-";
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
            this.btnBuscar.Location = new System.Drawing.Point(597, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 205;
            this.label3.Text = "Ruta:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(104, 56);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // txtBuscarRuta
            // 
            this.txtBuscarRuta.Location = new System.Drawing.Point(55, 25);
            this.txtBuscarRuta.Name = "txtBuscarRuta";
            this.txtBuscarRuta.Size = new System.Drawing.Size(508, 20);
            this.txtBuscarRuta.TabIndex = 204;
            this.txtBuscarRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarRuta_KeyPress);
            // 
            // btnImportar
            // 
            this.btnImportar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnImportar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnImportar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImportar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Appearance.Options.UseBackColor = true;
            this.btnImportar.Appearance.Options.UseBorderColor = true;
            this.btnImportar.Appearance.Options.UseFont = true;
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.Image = ((System.Drawing.Image)(resources.GetObject("btnImportar.Image")));
            this.btnImportar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(30, 39);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(105, 47);
            this.btnImportar.TabIndex = 217;
            this.btnImportar.Tag = "5";
            this.btnImportar.Text = "Importar\r\na SPRING";
            this.btnImportar.ToolTip = "Importar a SPRING";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // dtgTarifaOT
            // 
            this.dtgTarifaOT.AllowDrop = true;
            this.dtgTarifaOT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTarifaOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTarifaOT.Location = new System.Drawing.Point(0, 169);
            this.dtgTarifaOT.MainView = this.dgvTarifaOTVista;
            this.dtgTarifaOT.Name = "dtgTarifaOT";
            this.dtgTarifaOT.Size = new System.Drawing.Size(1143, 381);
            this.dtgTarifaOT.TabIndex = 134;
            this.dtgTarifaOT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTarifaOTVista});
            // 
            // dgvTarifaOTVista
            // 
            this.dgvTarifaOTVista.GridControl = this.dtgTarifaOT;
            this.dgvTarifaOTVista.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvTarifaOTVista.Name = "dgvTarifaOTVista";
            this.dgvTarifaOTVista.OptionsBehavior.Editable = false;
            this.dgvTarifaOTVista.OptionsSelection.MultiSelect = true;
            this.dgvTarifaOTVista.OptionsView.ColumnAutoWidth = false;
            this.dgvTarifaOTVista.OptionsView.RowAutoHeight = true;
            // 
            // pNuevaTarifa
            // 
            this.pNuevaTarifa.BackColor = System.Drawing.Color.LemonChiffon;
            this.pNuevaTarifa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNuevaTarifa.Controls.Add(this.label5);
            this.pNuevaTarifa.Controls.Add(this.dgvTarifasRuta);
            this.pNuevaTarifa.Controls.Add(this.btnBuscarArchivo);
            this.pNuevaTarifa.Controls.Add(this.label8);
            this.pNuevaTarifa.Controls.Add(this.txtDirectorio);
            this.pNuevaTarifa.Controls.Add(this.btnCerrar);
            this.pNuevaTarifa.Controls.Add(this.label6);
            this.pNuevaTarifa.Controls.Add(this.metroLabel20);
            this.pNuevaTarifa.Controls.Add(this.label10);
            this.pNuevaTarifa.Controls.Add(this.cbxMoneda);
            this.pNuevaTarifa.Controls.Add(this.btnGenerar);
            this.pNuevaTarifa.Location = new System.Drawing.Point(142, 55);
            this.pNuevaTarifa.Name = "pNuevaTarifa";
            this.pNuevaTarifa.Size = new System.Drawing.Size(998, 492);
            this.pNuevaTarifa.TabIndex = 135;
            this.pNuevaTarifa.Visible = false;
            this.pNuevaTarifa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pNuevaTarifa_MouseMove);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(270, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 15);
            this.label5.TabIndex = 211;
            this.label5.Text = "FORMATO DE TARIFAS.xlsx";
            // 
            // dgvTarifasRuta
            // 
            this.dgvTarifasRuta.AllowUserToAddRows = false;
            this.dgvTarifasRuta.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTarifasRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTarifasRuta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTarifasRuta.Location = new System.Drawing.Point(21, 103);
            this.dgvTarifasRuta.Name = "dgvTarifasRuta";
            this.dgvTarifasRuta.RowHeadersVisible = false;
            this.dgvTarifasRuta.Size = new System.Drawing.Size(953, 365);
            this.dgvTarifasRuta.TabIndex = 210;
            // 
            // btnBuscarArchivo
            // 
            this.btnBuscarArchivo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscarArchivo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscarArchivo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscarArchivo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarArchivo.Appearance.Options.UseBackColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseBorderColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseFont = true;
            this.btnBuscarArchivo.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscarArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarArchivo.Image = global::ReportesTranspesa.Properties.Resources.expotexcel;
            this.btnBuscarArchivo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscarArchivo.Location = new System.Drawing.Point(21, 52);
            this.btnBuscarArchivo.Name = "btnBuscarArchivo";
            this.btnBuscarArchivo.Size = new System.Drawing.Size(90, 38);
            this.btnBuscarArchivo.TabIndex = 209;
            this.btnBuscarArchivo.Tag = "5";
            this.btnBuscarArchivo.Text = "Buscar";
            this.btnBuscarArchivo.ToolTip = "Buscar";
            this.btnBuscarArchivo.Click += new System.EventHandler(this.btnBuscarArchivo_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.Location = new System.Drawing.Point(124, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 13);
            this.label8.TabIndex = 208;
            this.label8.Text = "Seleccione el archivo titulado:";
            // 
            // txtDirectorio
            // 
            this.txtDirectorio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDirectorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtDirectorio.Location = new System.Drawing.Point(127, 70);
            this.txtDirectorio.Name = "txtDirectorio";
            this.txtDirectorio.ReadOnly = true;
            this.txtDirectorio.Size = new System.Drawing.Size(381, 20);
            this.txtDirectorio.TabIndex = 206;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(973, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 150;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(10, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 22);
            this.label6.TabIndex = 144;
            this.label6.Text = "IMPORTAR TARIFAS";
            // 
            // metroLabel20
            // 
            this.metroLabel20.AutoSize = true;
            this.metroLabel20.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel20.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel20.Location = new System.Drawing.Point(97, 18);
            this.metroLabel20.Name = "metroLabel20";
            this.metroLabel20.Size = new System.Drawing.Size(0, 0);
            this.metroLabel20.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel20.TabIndex = 104;
            this.metroLabel20.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label10.Location = new System.Drawing.Point(530, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 218;
            this.label10.Text = "Moneda:";
            // 
            // cbxMoneda
            // 
            this.cbxMoneda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxMoneda.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMoneda.FormattingEnabled = true;
            this.cbxMoneda.Items.AddRange(new object[] {
            "SOLES",
            "DÓLARES"});
            this.cbxMoneda.Location = new System.Drawing.Point(533, 69);
            this.cbxMoneda.Name = "cbxMoneda";
            this.cbxMoneda.Size = new System.Drawing.Size(104, 21);
            this.cbxMoneda.TabIndex = 217;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGenerar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGenerar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGenerar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Appearance.Options.UseBackColor = true;
            this.btnGenerar.Appearance.Options.UseBorderColor = true;
            this.btnGenerar.Appearance.Options.UseFont = true;
            this.btnGenerar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerar.Image")));
            this.btnGenerar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGenerar.Location = new System.Drawing.Point(669, 52);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(100, 38);
            this.btnGenerar.TabIndex = 207;
            this.btnGenerar.Tag = "5";
            this.btnGenerar.Text = "Importar a\r\nSPRING";
            this.btnGenerar.ToolTip = "Importar";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // frmListaTarifasOT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 550);
            this.Controls.Add(this.dtgTarifaOT);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pNuevaTarifa);
            this.Name = "frmListaTarifasOT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMPORTAR TARIFAS DE OTS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaTarifasOT_Load);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTarifaOT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifaOTVista)).EndInit();
            this.pNuevaTarifa.ResumeLayout(false);
            this.pNuevaTarifa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifasRuta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtBuscarRuta;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnImportar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl dtgTarifaOT;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTarifaOTVista;
        private System.Windows.Forms.Panel pNuevaTarifa;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroLabel metroLabel20;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxCliente;
        private DevExpress.XtraEditors.SimpleButton btnBuscarArchivo;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtDirectorio;
        private DevExpress.XtraEditors.SimpleButton btnGenerar;
        public System.Windows.Forms.DataGridView dgvTarifasRuta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxMoneda;
    }
}