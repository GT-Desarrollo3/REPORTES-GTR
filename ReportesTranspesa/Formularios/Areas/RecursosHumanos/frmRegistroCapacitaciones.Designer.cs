namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmRegistroCapacitaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroCapacitaciones));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevoRegistro = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmpleadoC = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgListaCapacitacion = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsModificarVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAnularVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaCapacitacionVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pImportarDocumentos = new System.Windows.Forms.Panel();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.btnCerrarLocal = new System.Windows.Forms.Button();
            this.btnBuscarArchivo = new DevExpress.XtraEditors.SimpleButton();
            this.txtRutaLocal = new System.Windows.Forms.RichTextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lstPersona = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaCapacitacion)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCapacitacionVista)).BeginInit();
            this.pImportarDocumentos.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbxEstado);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.dtpFechaIni);
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnNuevoRegistro);
            this.groupBox1.Controls.Add(this.txtEmpleadoC);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 111);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Búsqueda";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(474, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 15);
            this.label8.TabIndex = 238;
            this.label8.Text = "Estado:";
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODOS",
            "PENDIENTE",
            "EN CURSO",
            "CONCLUIDO"});
            this.cbxEstado.Location = new System.Drawing.Point(528, 66);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(132, 23);
            this.cbxEstado.TabIndex = 237;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(328, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 15);
            this.label4.TabIndex = 235;
            this.label4.Text = "--";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(347, 67);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 236;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(226, 67);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 234;
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(748, 39);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 231;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(168, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 230;
            this.label3.Text = "F. Inicio:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(153, 34);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(67, 15);
            this.label28.TabIndex = 227;
            this.label28.Text = "Empleado:";
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
            this.btnBuscar.Location = new System.Drawing.Point(692, 39);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 224;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnNuevoRegistro
            // 
            this.btnNuevoRegistro.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoRegistro.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoRegistro.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoRegistro.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoRegistro.Appearance.Options.UseBackColor = true;
            this.btnNuevoRegistro.Appearance.Options.UseBorderColor = true;
            this.btnNuevoRegistro.Appearance.Options.UseFont = true;
            this.btnNuevoRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoRegistro.Image")));
            this.btnNuevoRegistro.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoRegistro.Location = new System.Drawing.Point(22, 39);
            this.btnNuevoRegistro.Name = "btnNuevoRegistro";
            this.btnNuevoRegistro.Size = new System.Drawing.Size(109, 47);
            this.btnNuevoRegistro.TabIndex = 223;
            this.btnNuevoRegistro.Text = "Nuevo\r\nRegistro";
            this.btnNuevoRegistro.ToolTip = "Nuevo Registro";
            this.btnNuevoRegistro.Click += new System.EventHandler(this.btnNuevoRegistro_Click);
            // 
            // txtEmpleadoC
            // 
            this.txtEmpleadoC.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtEmpleadoC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEmpleadoC.Location = new System.Drawing.Point(226, 31);
            this.txtEmpleadoC.Name = "txtEmpleadoC";
            this.txtEmpleadoC.Size = new System.Drawing.Size(434, 21);
            this.txtEmpleadoC.TabIndex = 112;
            this.txtEmpleadoC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleadoC_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1035, 20);
            this.panel2.TabIndex = 187;
            // 
            // dtgListaCapacitacion
            // 
            this.dtgListaCapacitacion.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaCapacitacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaCapacitacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaCapacitacion.Location = new System.Drawing.Point(20, 191);
            this.dtgListaCapacitacion.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.dtgListaCapacitacion.LookAndFeel.SkinName = "Money Twins";
            this.dtgListaCapacitacion.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaCapacitacion.MainView = this.dgvListaCapacitacionVista;
            this.dtgListaCapacitacion.Name = "dtgListaCapacitacion";
            this.dtgListaCapacitacion.Size = new System.Drawing.Size(1035, 395);
            this.dtgListaCapacitacion.TabIndex = 188;
            this.dtgListaCapacitacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaCapacitacionVista});
            this.dtgListaCapacitacion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaCapacitacion_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsModificarVersion,
            this.tsAnularVersion});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // tsModificarVersion
            // 
            this.tsModificarVersion.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.tsModificarVersion.Name = "tsModificarVersion";
            this.tsModificarVersion.Size = new System.Drawing.Size(180, 22);
            this.tsModificarVersion.Text = "Actualizar Convenio";
            this.tsModificarVersion.Click += new System.EventHandler(this.tsModificarVersion_Click);
            // 
            // tsAnularVersion
            // 
            this.tsAnularVersion.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsAnularVersion.Name = "tsAnularVersion";
            this.tsAnularVersion.Size = new System.Drawing.Size(180, 22);
            this.tsAnularVersion.Text = "Anular Convenio";
            this.tsAnularVersion.Click += new System.EventHandler(this.tsAnularVersion_Click);
            // 
            // dgvListaCapacitacionVista
            // 
            this.dgvListaCapacitacionVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListaCapacitacionVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvListaCapacitacionVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvListaCapacitacionVista.Appearance.Row.Options.UseFont = true;
            this.dgvListaCapacitacionVista.GridControl = this.dtgListaCapacitacion;
            this.dgvListaCapacitacionVista.Name = "dgvListaCapacitacionVista";
            this.dgvListaCapacitacionVista.OptionsBehavior.Editable = false;
            this.dgvListaCapacitacionVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaCapacitacionVista.OptionsView.RowAutoHeight = true;
            this.dgvListaCapacitacionVista.OptionsView.ShowFooter = true;
            this.dgvListaCapacitacionVista.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvListaCapacitacionVista_RowCellClick);
            this.dgvListaCapacitacionVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaCapacitacionVista_CustomDrawCell);
            // 
            // pImportarDocumentos
            // 
            this.pImportarDocumentos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pImportarDocumentos.BackColor = System.Drawing.Color.White;
            this.pImportarDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pImportarDocumentos.Controls.Add(this.txtTitulo);
            this.pImportarDocumentos.Controls.Add(this.label5);
            this.pImportarDocumentos.Controls.Add(this.txtProveedor);
            this.pImportarDocumentos.Controls.Add(this.label1);
            this.pImportarDocumentos.Controls.Add(this.txtMonto);
            this.pImportarDocumentos.Controls.Add(this.label7);
            this.pImportarDocumentos.Controls.Add(this.txtDuracion);
            this.pImportarDocumentos.Controls.Add(this.btnGuardar);
            this.pImportarDocumentos.Controls.Add(this.label6);
            this.pImportarDocumentos.Controls.Add(this.txtEmpleado);
            this.pImportarDocumentos.Controls.Add(this.btnCerrarLocal);
            this.pImportarDocumentos.Controls.Add(this.btnBuscarArchivo);
            this.pImportarDocumentos.Controls.Add(this.txtRutaLocal);
            this.pImportarDocumentos.Controls.Add(this.btnCerrar);
            this.pImportarDocumentos.Controls.Add(this.label9);
            this.pImportarDocumentos.Controls.Add(this.metroLabel1);
            this.pImportarDocumentos.Controls.Add(this.txtExtension);
            this.pImportarDocumentos.Controls.Add(this.dtpInicio);
            this.pImportarDocumentos.Controls.Add(this.label2);
            this.pImportarDocumentos.Controls.Add(this.lstPersona);
            this.pImportarDocumentos.Location = new System.Drawing.Point(825, 273);
            this.pImportarDocumentos.Name = "pImportarDocumentos";
            this.pImportarDocumentos.Size = new System.Drawing.Size(490, 264);
            this.pImportarDocumentos.TabIndex = 237;
            this.pImportarDocumentos.Visible = false;
            this.pImportarDocumentos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pImportarDocumentos_MouseMove);
            // 
            // txtTitulo
            // 
            this.txtTitulo.BackColor = System.Drawing.Color.White;
            this.txtTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTitulo.Location = new System.Drawing.Point(320, 23);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(59, 21);
            this.txtTitulo.TabIndex = 250;
            this.txtTitulo.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 249;
            this.label5.Text = "Proveedor:";
            // 
            // txtProveedor
            // 
            this.txtProveedor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtProveedor.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtProveedor.Location = new System.Drawing.Point(88, 85);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(356, 21);
            this.txtProveedor.TabIndex = 248;
            this.txtProveedor.Enter += new System.EventHandler(this.txtProveedor_Enter);
            this.txtProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProveedor_KeyPress);
            this.txtProveedor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtProveedor_KeyUp);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 240;
            this.label1.Text = "Monto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMonto.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMonto.Location = new System.Drawing.Point(88, 215);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(90, 21);
            this.txtMonto.TabIndex = 239;
            this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 30);
            this.label7.TabIndex = 246;
            this.label7.Text = "Duración\r\n(Meses):";
            // 
            // txtDuracion
            // 
            this.txtDuracion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDuracion.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtDuracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDuracion.Location = new System.Drawing.Point(88, 179);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(90, 21);
            this.txtDuracion.TabIndex = 245;
            this.txtDuracion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuracion_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.Plum;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(371, 193);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 38);
            this.btnGuardar.TabIndex = 237;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 234;
            this.label6.Text = "Empleado:";
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtEmpleado.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEmpleado.Location = new System.Drawing.Point(88, 50);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(356, 21);
            this.txtEmpleado.TabIndex = 233;
            this.txtEmpleado.Enter += new System.EventHandler(this.txtEmpleado_Enter);
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            this.txtEmpleado.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmpleado_KeyUp);
            // 
            // btnCerrarLocal
            // 
            this.btnCerrarLocal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCerrarLocal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarLocal.ForeColor = System.Drawing.Color.Red;
            this.btnCerrarLocal.Location = new System.Drawing.Point(450, 129);
            this.btnCerrarLocal.Name = "btnCerrarLocal";
            this.btnCerrarLocal.Size = new System.Drawing.Size(26, 26);
            this.btnCerrarLocal.TabIndex = 228;
            this.btnCerrarLocal.Text = "X";
            this.btnCerrarLocal.UseVisualStyleBackColor = true;
            this.btnCerrarLocal.Click += new System.EventHandler(this.btnCerrarLocal_Click);
            // 
            // btnBuscarArchivo
            // 
            this.btnBuscarArchivo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscarArchivo.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.btnBuscarArchivo.Appearance.BackColor2 = System.Drawing.Color.Plum;
            this.btnBuscarArchivo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscarArchivo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarArchivo.Appearance.Options.UseBackColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseBorderColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseFont = true;
            this.btnBuscarArchivo.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscarArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarArchivo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarArchivo.Image")));
            this.btnBuscarArchivo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscarArchivo.Location = new System.Drawing.Point(18, 123);
            this.btnBuscarArchivo.Name = "btnBuscarArchivo";
            this.btnBuscarArchivo.Size = new System.Drawing.Size(90, 38);
            this.btnBuscarArchivo.TabIndex = 227;
            this.btnBuscarArchivo.Tag = "5";
            this.btnBuscarArchivo.Text = "Buscar\r\nArchivo";
            this.btnBuscarArchivo.ToolTip = "Buscar Archivo";
            this.btnBuscarArchivo.Click += new System.EventHandler(this.btnBuscarArchivo_Click);
            // 
            // txtRutaLocal
            // 
            this.txtRutaLocal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtRutaLocal.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtRutaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaLocal.Location = new System.Drawing.Point(123, 123);
            this.txtRutaLocal.Name = "txtRutaLocal";
            this.txtRutaLocal.ReadOnly = true;
            this.txtRutaLocal.Size = new System.Drawing.Size(320, 38);
            this.txtRutaLocal.TabIndex = 226;
            this.txtRutaLocal.Text = "";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(464, -1);
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
            this.label9.ForeColor = System.Drawing.Color.Purple;
            this.label9.Location = new System.Drawing.Point(10, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(226, 24);
            this.label9.TabIndex = 144;
            this.label9.Text = "SUBIR CAPACITACIÓN";
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
            // txtExtension
            // 
            this.txtExtension.BackColor = System.Drawing.Color.White;
            this.txtExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtExtension.Location = new System.Drawing.Point(385, 23);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(59, 21);
            this.txtExtension.TabIndex = 238;
            this.txtExtension.Visible = false;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInicio.Location = new System.Drawing.Point(245, 179);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(100, 21);
            this.dtpInicio.TabIndex = 242;
            this.dtpInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpInicio_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(200, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 241;
            this.label2.Text = "Inicio:";
            // 
            // lstPersona
            // 
            this.lstPersona.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lstPersona.BackColor = System.Drawing.Color.LavenderBlush;
            this.lstPersona.ForeColor = System.Drawing.Color.Navy;
            this.lstPersona.FullRowSelect = true;
            this.lstPersona.GridLines = true;
            this.lstPersona.Location = new System.Drawing.Point(88, 105);
            this.lstPersona.MultiSelect = false;
            this.lstPersona.Name = "lstPersona";
            this.lstPersona.Size = new System.Drawing.Size(356, 110);
            this.lstPersona.TabIndex = 247;
            this.lstPersona.UseCompatibleStateImageBehavior = false;
            this.lstPersona.View = System.Windows.Forms.View.Details;
            this.lstPersona.Visible = false;
            this.lstPersona.Enter += new System.EventHandler(this.lstPersona_Enter);
            this.lstPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersona_KeyPress);
            this.lstPersona.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersona_MouseDoubleClick);
            // 
            // frmRegistroCapacitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 606);
            this.Controls.Add(this.dtgListaCapacitacion);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pImportarDocumentos);
            this.Name = "frmRegistroCapacitaciones";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "CONVENIOS DE CAPACITACIÓN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRegistroCapacitaciones_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaCapacitacion)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCapacitacionVista)).EndInit();
            this.pImportarDocumentos.ResumeLayout(false);
            this.pImportarDocumentos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Label label28;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnNuevoRegistro;
        private System.Windows.Forms.TextBox txtEmpleadoC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl dtgListaCapacitacion;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaCapacitacionVista;
        private System.Windows.Forms.Panel pImportarDocumentos;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmpleado;
        public System.Windows.Forms.Button btnCerrarLocal;
        private DevExpress.XtraEditors.SimpleButton btnBuscarArchivo;
        public System.Windows.Forms.RichTextBox txtRutaLocal;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lstPersona;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsModificarVersion;
        private System.Windows.Forms.ToolStripMenuItem tsAnularVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox cbxEstado;
    }
}