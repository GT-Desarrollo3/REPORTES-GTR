namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmIngresoTerceros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIngresoTerceros));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label28 = new System.Windows.Forms.Label();
            this.txtPerExt = new System.Windows.Forms.TextBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnRegistrarIngreso = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmpExt = new System.Windows.Forms.TextBox();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtgRegistroExterno = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsValidarAcceso = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAniadirIngreso = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEliminarRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvRegistroExternoVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pIngresarFechas = new System.Windows.Forms.Panel();
            this.dtpFechaIngreso = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroExterno)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroExternoVista)).BeginInit();
            this.pIngresarFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.txtPerExt);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnRegistrarIngreso);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtEmpExt);
            this.groupBox1.Controls.Add(this.cbxArea);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpFechaIni);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1007, 111);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Búsqueda";
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
            this.btnExcel.Location = new System.Drawing.Point(934, 40);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 231;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(438, 38);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(82, 15);
            this.label28.TabIndex = 227;
            this.label28.Text = "Personal Ext.:";
            // 
            // txtPerExt
            // 
            this.txtPerExt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPerExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerExt.Location = new System.Drawing.Point(526, 36);
            this.txtPerExt.Name = "txtPerExt";
            this.txtPerExt.Size = new System.Drawing.Size(320, 20);
            this.txtPerExt.TabIndex = 112;
            this.txtPerExt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPerExt_KeyPress);
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
            this.btnBuscar.Location = new System.Drawing.Point(878, 40);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 224;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnRegistrarIngreso
            // 
            this.btnRegistrarIngreso.Appearance.BackColor = System.Drawing.Color.White;
            this.btnRegistrarIngreso.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnRegistrarIngreso.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnRegistrarIngreso.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarIngreso.Appearance.Options.UseBackColor = true;
            this.btnRegistrarIngreso.Appearance.Options.UseBorderColor = true;
            this.btnRegistrarIngreso.Appearance.Options.UseFont = true;
            this.btnRegistrarIngreso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarIngreso.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrarIngreso.Image")));
            this.btnRegistrarIngreso.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnRegistrarIngreso.Location = new System.Drawing.Point(22, 39);
            this.btnRegistrarIngreso.Name = "btnRegistrarIngreso";
            this.btnRegistrarIngreso.Size = new System.Drawing.Size(99, 47);
            this.btnRegistrarIngreso.TabIndex = 223;
            this.btnRegistrarIngreso.Text = "Registrar\r\nIngreso";
            this.btnRegistrarIngreso.ToolTip = "Subir Documento";
            this.btnRegistrarIngreso.Click += new System.EventHandler(this.btnRegistrarIngreso_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(437, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 233;
            this.label4.Text = "Empresa Ext.:";
            // 
            // txtEmpExt
            // 
            this.txtEmpExt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtEmpExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpExt.Location = new System.Drawing.Point(526, 69);
            this.txtEmpExt.Name = "txtEmpExt";
            this.txtEmpExt.Size = new System.Drawing.Size(320, 20);
            this.txtEmpExt.TabIndex = 232;
            this.txtEmpExt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpExt_KeyPress);
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(210, 68);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(209, 21);
            this.cbxArea.TabIndex = 113;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            this.cbxArea.DropDownClosed += new System.EventHandler(this.cbxArea_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(169, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 15);
            this.label3.TabIndex = 237;
            this.label3.Text = "Área:";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(210, 36);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaIni.TabIndex = 1;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(307, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 229;
            this.label2.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 236;
            this.label1.Text = "F. Ingreso:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(324, 36);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 2;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtgRegistroExterno
            // 
            this.dtgRegistroExterno.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgRegistroExterno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRegistroExterno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRegistroExterno.Location = new System.Drawing.Point(20, 191);
            this.dtgRegistroExterno.LookAndFeel.SkinMaskColor = System.Drawing.Color.Yellow;
            this.dtgRegistroExterno.LookAndFeel.SkinName = "Money Twins";
            this.dtgRegistroExterno.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRegistroExterno.MainView = this.dgvRegistroExternoVista;
            this.dtgRegistroExterno.Name = "dtgRegistroExterno";
            this.dtgRegistroExterno.Size = new System.Drawing.Size(1007, 276);
            this.dtgRegistroExterno.TabIndex = 187;
            this.dtgRegistroExterno.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRegistroExternoVista});
            this.dtgRegistroExterno.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgRegistroExterno_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsValidarAcceso,
            this.tsAniadirIngreso,
            this.tsEliminarRegistro});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 70);
            // 
            // tsValidarAcceso
            // 
            this.tsValidarAcceso.Image = global::ReportesTranspesa.Properties.Resources.ordenar;
            this.tsValidarAcceso.Name = "tsValidarAcceso";
            this.tsValidarAcceso.Size = new System.Drawing.Size(159, 22);
            this.tsValidarAcceso.Text = "Validar Acceso";
            this.tsValidarAcceso.Click += new System.EventHandler(this.tsValidarAcceso_Click);
            // 
            // tsAniadirIngreso
            // 
            this.tsAniadirIngreso.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.tsAniadirIngreso.Name = "tsAniadirIngreso";
            this.tsAniadirIngreso.Size = new System.Drawing.Size(159, 22);
            this.tsAniadirIngreso.Text = "Añadir Tiempos";
            this.tsAniadirIngreso.Click += new System.EventHandler(this.tsAniadirIngreso_Click);
            // 
            // tsEliminarRegistro
            // 
            this.tsEliminarRegistro.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsEliminarRegistro.Name = "tsEliminarRegistro";
            this.tsEliminarRegistro.Size = new System.Drawing.Size(159, 22);
            this.tsEliminarRegistro.Text = "Eliminar Ingreso";
            this.tsEliminarRegistro.Click += new System.EventHandler(this.tsEliminarRegistro_Click);
            // 
            // dgvRegistroExternoVista
            // 
            this.dgvRegistroExternoVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRegistroExternoVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvRegistroExternoVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRegistroExternoVista.Appearance.Row.Options.UseFont = true;
            this.dgvRegistroExternoVista.GridControl = this.dtgRegistroExterno;
            this.dgvRegistroExternoVista.Name = "dgvRegistroExternoVista";
            this.dgvRegistroExternoVista.OptionsBehavior.Editable = false;
            this.dgvRegistroExternoVista.OptionsView.ColumnAutoWidth = false;
            this.dgvRegistroExternoVista.OptionsView.RowAutoHeight = true;
            this.dgvRegistroExternoVista.OptionsView.ShowFooter = true;
            this.dgvRegistroExternoVista.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvRegistroExternoVista_RowCellClick);
            this.dgvRegistroExternoVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvRegistroExternoVista_CustomDrawCell);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1007, 20);
            this.panel2.TabIndex = 188;
            // 
            // pIngresarFechas
            // 
            this.pIngresarFechas.BackColor = System.Drawing.Color.White;
            this.pIngresarFechas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pIngresarFechas.Controls.Add(this.label20);
            this.pIngresarFechas.Controls.Add(this.dtpFechaSalida);
            this.pIngresarFechas.Controls.Add(this.label19);
            this.pIngresarFechas.Controls.Add(this.btnGuardar);
            this.pIngresarFechas.Controls.Add(this.btnCerrar);
            this.pIngresarFechas.Controls.Add(this.label9);
            this.pIngresarFechas.Controls.Add(this.metroLabel1);
            this.pIngresarFechas.Controls.Add(this.dtpFechaIngreso);
            this.pIngresarFechas.Location = new System.Drawing.Point(781, 326);
            this.pIngresarFechas.Name = "pIngresarFechas";
            this.pIngresarFechas.Size = new System.Drawing.Size(333, 184);
            this.pIngresarFechas.TabIndex = 224;
            this.pIngresarFechas.Visible = false;
            this.pIngresarFechas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pIngresarFechas_MouseMove);
            // 
            // dtpFechaIngreso
            // 
            this.dtpFechaIngreso.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.dtpFechaIngreso.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIngreso.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIngreso.Location = new System.Drawing.Point(91, 49);
            this.dtpFechaIngreso.Name = "dtpFechaIngreso";
            this.dtpFechaIngreso.Size = new System.Drawing.Size(158, 22);
            this.dtpFechaIngreso.TabIndex = 239;
            this.dtpFechaIngreso.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpFechaIngreso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIngreso_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(15, 52);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 16);
            this.label20.TabIndex = 243;
            this.label20.Text = "F. Ingreso:";
            // 
            // dtpFechaSalida
            // 
            this.dtpFechaSalida.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaSalida.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaSalida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaSalida.Location = new System.Drawing.Point(91, 83);
            this.dtpFechaSalida.Name = "dtpFechaSalida";
            this.dtpFechaSalida.Size = new System.Drawing.Size(158, 22);
            this.dtpFechaSalida.TabIndex = 241;
            this.dtpFechaSalida.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpFechaSalida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaSalida_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(15, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 16);
            this.label19.TabIndex = 244;
            this.label19.Text = "F. Salida:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.PaleGoldenrod;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(120, 127);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 38);
            this.btnGuardar.TabIndex = 237;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(307, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 224;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(10, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(283, 24);
            this.label9.TabIndex = 144;
            this.label9.Text = "AÑADIR HORA DE INGRESO";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel1.Location = new System.Drawing.Point(97, 18);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(0, 0);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 104;
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // frmIngresoTerceros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 487);
            this.Controls.Add(this.dtgRegistroExterno);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pIngresarFechas);
            this.Name = "frmIngresoTerceros";
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "REGISTRO DE CONTROL DE EMPRESAS EXTERNAS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmIngresoTerceros_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroExterno)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroExternoVista)).EndInit();
            this.pIngresarFechas.ResumeLayout(false);
            this.pIngresarFechas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtPerExt;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnRegistrarIngreso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmpExt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgRegistroExterno;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRegistroExternoVista;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsValidarAcceso;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarRegistro;
        private System.Windows.Forms.ToolStripMenuItem tsAniadirIngreso;
        private System.Windows.Forms.Panel pIngresarFechas;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        public System.Windows.Forms.DateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.DateTimePicker dtpFechaSalida;
        private System.Windows.Forms.Label label19;
    }
}