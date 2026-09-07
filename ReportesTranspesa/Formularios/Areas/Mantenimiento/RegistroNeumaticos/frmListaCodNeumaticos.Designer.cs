namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroNeumaticos
{
    partial class frmListaCodNeumaticos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaCodNeumaticos));
            this.label1 = new System.Windows.Forms.Label();
            this.dtgNeumaticos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsActualizarNeumatico = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNuevoMov = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvNeumaticosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLista = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxMarca = new System.Windows.Forms.ComboBox();
            this.btnNuevoNeumatico = new DevExpress.XtraEditors.SimpleButton();
            this.tabMovimientos = new System.Windows.Forms.TabPage();
            this.dtgMovimientos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsDesinstalarNeu = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMovimientosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar2 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtCodigo2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.pInstalaciones = new System.Windows.Forms.Panel();
            this.txtNSK = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtKM = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPosicion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.txtCOD = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPlaca2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblInstalacion = new System.Windows.Forms.Label();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.cbxTipo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNeumaticos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNeumaticosVista)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabLista.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabMovimientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMovimientos)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientosVista)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pInstalaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(999, 50);
            this.label1.TabIndex = 21;
            this.label1.Text = "REGISTRO DE NEUMÁTICOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgNeumaticos
            // 
            this.dtgNeumaticos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgNeumaticos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgNeumaticos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgNeumaticos.Location = new System.Drawing.Point(3, 94);
            this.dtgNeumaticos.LookAndFeel.SkinName = "Money Twins";
            this.dtgNeumaticos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgNeumaticos.MainView = this.dgvNeumaticosVista;
            this.dtgNeumaticos.Name = "dtgNeumaticos";
            this.dtgNeumaticos.Size = new System.Drawing.Size(985, 308);
            this.dtgNeumaticos.TabIndex = 212;
            this.dtgNeumaticos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvNeumaticosVista});
            this.dtgNeumaticos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgNeumaticos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsActualizarNeumatico,
            this.tsNuevoMov});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 48);
            // 
            // tsActualizarNeumatico
            // 
            this.tsActualizarNeumatico.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.tsActualizarNeumatico.Name = "tsActualizarNeumatico";
            this.tsActualizarNeumatico.Size = new System.Drawing.Size(188, 22);
            this.tsActualizarNeumatico.Text = "Actualizar Neumático";
            this.tsActualizarNeumatico.Click += new System.EventHandler(this.tsActualizarNeumatico_Click);
            // 
            // tsNuevoMov
            // 
            this.tsNuevoMov.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.tsNuevoMov.Name = "tsNuevoMov";
            this.tsNuevoMov.Size = new System.Drawing.Size(188, 22);
            this.tsNuevoMov.Text = "Instalar a Unidad";
            this.tsNuevoMov.Click += new System.EventHandler(this.tsNuevoMov_Click);
            // 
            // dgvNeumaticosVista
            // 
            this.dgvNeumaticosVista.GridControl = this.dtgNeumaticos;
            this.dgvNeumaticosVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvNeumaticosVista.Name = "dgvNeumaticosVista";
            this.dgvNeumaticosVista.OptionsBehavior.Editable = false;
            this.dgvNeumaticosVista.OptionsBehavior.ReadOnly = true;
            this.dgvNeumaticosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvNeumaticosVista.OptionsView.RowAutoHeight = true;
            this.dgvNeumaticosVista.OptionsView.ShowFooter = true;
            this.dgvNeumaticosVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvNeumaticosVista_CustomDrawCell);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLista);
            this.tabControl1.Controls.Add(this.tabMovimientos);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(999, 442);
            this.tabControl1.TabIndex = 213;
            // 
            // tabLista
            // 
            this.tabLista.Controls.Add(this.dtgNeumaticos);
            this.tabLista.Controls.Add(this.panel3);
            this.tabLista.Location = new System.Drawing.Point(4, 33);
            this.tabLista.Name = "tabLista";
            this.tabLista.Padding = new System.Windows.Forms.Padding(3);
            this.tabLista.Size = new System.Drawing.Size(991, 405);
            this.tabLista.TabIndex = 0;
            this.tabLista.Text = "Neumáticos";
            this.tabLista.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightCyan;
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.btnNuevoNeumatico);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(985, 91);
            this.panel3.TabIndex = 213;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbxEstado);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(524, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(153, 58);
            this.groupBox4.TabIndex = 222;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Estado: ";
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODOS",
            "DISPONIBLE",
            "ASIGNADO",
            "SCRAP"});
            this.cbxEstado.Location = new System.Drawing.Point(12, 22);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(128, 23);
            this.cbxEstado.TabIndex = 217;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
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
            this.btnExcel.Location = new System.Drawing.Point(763, 22);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 197;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
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
            this.btnBuscar.Location = new System.Drawing.Point(705, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(159, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 58);
            this.groupBox2.TabIndex = 220;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Código: ";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(12, 23);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(127, 21);
            this.txtCodigo.TabIndex = 204;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbxMarca);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(324, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(187, 58);
            this.groupBox3.TabIndex = 221;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Marca: ";
            // 
            // cbxMarca
            // 
            this.cbxMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMarca.FormattingEnabled = true;
            this.cbxMarca.Location = new System.Drawing.Point(12, 22);
            this.cbxMarca.Name = "cbxMarca";
            this.cbxMarca.Size = new System.Drawing.Size(162, 23);
            this.cbxMarca.TabIndex = 217;
            this.cbxMarca.SelectedIndexChanged += new System.EventHandler(this.cbxMarca_SelectedIndexChanged);
            this.cbxMarca.DropDownClosed += new System.EventHandler(this.cbxMarca_DropDownClosed);
            // 
            // btnNuevoNeumatico
            // 
            this.btnNuevoNeumatico.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoNeumatico.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoNeumatico.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoNeumatico.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoNeumatico.Appearance.Options.UseBackColor = true;
            this.btnNuevoNeumatico.Appearance.Options.UseBorderColor = true;
            this.btnNuevoNeumatico.Appearance.Options.UseFont = true;
            this.btnNuevoNeumatico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoNeumatico.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoNeumatico.Image")));
            this.btnNuevoNeumatico.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoNeumatico.Location = new System.Drawing.Point(26, 22);
            this.btnNuevoNeumatico.Name = "btnNuevoNeumatico";
            this.btnNuevoNeumatico.Size = new System.Drawing.Size(107, 47);
            this.btnNuevoNeumatico.TabIndex = 226;
            this.btnNuevoNeumatico.Tag = "5";
            this.btnNuevoNeumatico.Text = "Agregar\r\nNeumático";
            this.btnNuevoNeumatico.ToolTip = "Agregar Neumático";
            this.btnNuevoNeumatico.Click += new System.EventHandler(this.btnNuevoNeumatico_Click);
            // 
            // tabMovimientos
            // 
            this.tabMovimientos.Controls.Add(this.dtgMovimientos);
            this.tabMovimientos.Controls.Add(this.panel1);
            this.tabMovimientos.Location = new System.Drawing.Point(4, 33);
            this.tabMovimientos.Name = "tabMovimientos";
            this.tabMovimientos.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovimientos.Size = new System.Drawing.Size(991, 405);
            this.tabMovimientos.TabIndex = 1;
            this.tabMovimientos.Text = "Movimientos";
            this.tabMovimientos.UseVisualStyleBackColor = true;
            // 
            // dtgMovimientos
            // 
            this.dtgMovimientos.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgMovimientos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgMovimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMovimientos.Location = new System.Drawing.Point(3, 94);
            this.dtgMovimientos.LookAndFeel.SkinMaskColor = System.Drawing.Color.Aquamarine;
            this.dtgMovimientos.LookAndFeel.SkinName = "Money Twins";
            this.dtgMovimientos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgMovimientos.MainView = this.dgvMovimientosVista;
            this.dtgMovimientos.Name = "dtgMovimientos";
            this.dtgMovimientos.Size = new System.Drawing.Size(985, 308);
            this.dtgMovimientos.TabIndex = 214;
            this.dtgMovimientos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMovimientosVista});
            this.dtgMovimientos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgMovimientos_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDesinstalarNeu});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(194, 26);
            // 
            // tsDesinstalarNeu
            // 
            this.tsDesinstalarNeu.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.tsDesinstalarNeu.Name = "tsDesinstalarNeu";
            this.tsDesinstalarNeu.Size = new System.Drawing.Size(193, 22);
            this.tsDesinstalarNeu.Text = "Desinstalar Neumático";
            this.tsDesinstalarNeu.Click += new System.EventHandler(this.tsDesinstalarNeu_Click);
            // 
            // dgvMovimientosVista
            // 
            this.dgvMovimientosVista.GridControl = this.dtgMovimientos;
            this.dgvMovimientosVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvMovimientosVista.Name = "dgvMovimientosVista";
            this.dgvMovimientosVista.OptionsBehavior.Editable = false;
            this.dgvMovimientosVista.OptionsBehavior.ReadOnly = true;
            this.dgvMovimientosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvMovimientosVista.OptionsView.RowAutoHeight = true;
            this.dgvMovimientosVista.OptionsView.ShowFooter = true;
            this.dgvMovimientosVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvMovimientosVista_CustomDrawCell);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightCyan;
            this.panel1.Controls.Add(this.btnExcel2);
            this.panel1.Controls.Add(this.btnBuscar2);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(985, 91);
            this.panel1.TabIndex = 215;
            // 
            // btnExcel2
            // 
            this.btnExcel2.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel2.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel2.Appearance.Options.UseBackColor = true;
            this.btnExcel2.Appearance.Options.UseBorderColor = true;
            this.btnExcel2.Appearance.Options.UseFont = true;
            this.btnExcel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel2.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel2.Image")));
            this.btnExcel2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel2.Location = new System.Drawing.Point(681, 22);
            this.btnExcel2.Name = "btnExcel2";
            this.btnExcel2.Size = new System.Drawing.Size(51, 47);
            this.btnExcel2.TabIndex = 197;
            this.btnExcel2.Tag = "6";
            this.btnExcel2.ToolTip = "Exportar a Excel";
            this.btnExcel2.Click += new System.EventHandler(this.btnExcel2_Click);
            // 
            // btnBuscar2
            // 
            this.btnBuscar2.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar2.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar2.Appearance.Options.UseBackColor = true;
            this.btnBuscar2.Appearance.Options.UseBorderColor = true;
            this.btnBuscar2.Appearance.Options.UseFont = true;
            this.btnBuscar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar2.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar2.Image")));
            this.btnBuscar2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar2.Location = new System.Drawing.Point(623, 22);
            this.btnBuscar2.Name = "btnBuscar2";
            this.btnBuscar2.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar2.TabIndex = 196;
            this.btnBuscar2.Tag = "5";
            this.btnBuscar2.ToolTip = "Buscar";
            this.btnBuscar2.Click += new System.EventHandler(this.btnBuscar2_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtPlaca);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(443, 14);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(152, 58);
            this.groupBox5.TabIndex = 220;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Placa: ";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(12, 23);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(127, 21);
            this.txtPlaca.TabIndex = 204;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtCodigo2);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(278, 14);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(152, 58);
            this.groupBox7.TabIndex = 227;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Código: ";
            // 
            // txtCodigo2
            // 
            this.txtCodigo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo2.Location = new System.Drawing.Point(12, 23);
            this.txtCodigo2.Name = "txtCodigo2";
            this.txtCodigo2.Size = new System.Drawing.Size(127, 21);
            this.txtCodigo2.TabIndex = 204;
            this.txtCodigo2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo2_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(26, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 58);
            this.groupBox1.TabIndex = 228;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha Instalación:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(129, 23);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(112, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "--";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(10, 23);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // pInstalaciones
            // 
            this.pInstalaciones.BackColor = System.Drawing.Color.LightCyan;
            this.pInstalaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pInstalaciones.Controls.Add(this.txtNSK);
            this.pInstalaciones.Controls.Add(this.label10);
            this.pInstalaciones.Controls.Add(this.txtKM);
            this.pInstalaciones.Controls.Add(this.label9);
            this.pInstalaciones.Controls.Add(this.dtpFecha);
            this.pInstalaciones.Controls.Add(this.label8);
            this.pInstalaciones.Controls.Add(this.txtPosicion);
            this.pInstalaciones.Controls.Add(this.label7);
            this.pInstalaciones.Controls.Add(this.txtModelo);
            this.pInstalaciones.Controls.Add(this.label2);
            this.pInstalaciones.Controls.Add(this.txtMarca);
            this.pInstalaciones.Controls.Add(this.txtCOD);
            this.pInstalaciones.Controls.Add(this.label6);
            this.pInstalaciones.Controls.Add(this.label4);
            this.pInstalaciones.Controls.Add(this.label3);
            this.pInstalaciones.Controls.Add(this.btnCerrar);
            this.pInstalaciones.Controls.Add(this.btnAgregar);
            this.pInstalaciones.Controls.Add(this.txtPlaca2);
            this.pInstalaciones.Controls.Add(this.label5);
            this.pInstalaciones.Controls.Add(this.lblInstalacion);
            this.pInstalaciones.Controls.Add(this.lstPlaca);
            this.pInstalaciones.Controls.Add(this.cbxTipo);
            this.pInstalaciones.Location = new System.Drawing.Point(725, 260);
            this.pInstalaciones.Name = "pInstalaciones";
            this.pInstalaciones.Size = new System.Drawing.Size(404, 298);
            this.pInstalaciones.TabIndex = 223;
            this.pInstalaciones.Visible = false;
            this.pInstalaciones.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pInstalaciones_MouseMove);
            // 
            // txtNSK
            // 
            this.txtNSK.BackColor = System.Drawing.SystemColors.Window;
            this.txtNSK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNSK.Location = new System.Drawing.Point(268, 199);
            this.txtNSK.Name = "txtNSK";
            this.txtNSK.Size = new System.Drawing.Size(111, 21);
            this.txtNSK.TabIndex = 253;
            this.txtNSK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNSK_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.Location = new System.Drawing.Point(231, 202);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 15);
            this.label10.TabIndex = 252;
            this.label10.Text = "NSK:";
            // 
            // txtKM
            // 
            this.txtKM.BackColor = System.Drawing.SystemColors.Window;
            this.txtKM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtKM.Location = new System.Drawing.Point(268, 163);
            this.txtKM.Name = "txtKM";
            this.txtKM.Size = new System.Drawing.Size(111, 21);
            this.txtKM.TabIndex = 251;
            this.txtKM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKM_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label9.Location = new System.Drawing.Point(237, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 15);
            this.label9.TabIndex = 250;
            this.label9.Text = "KM:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "";
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(268, 127);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(111, 21);
            this.dtpFecha.TabIndex = 249;
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFecha_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.Location = new System.Drawing.Point(222, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 15);
            this.label8.TabIndex = 248;
            this.label8.Text = "Fecha:";
            // 
            // txtPosicion
            // 
            this.txtPosicion.BackColor = System.Drawing.SystemColors.Window;
            this.txtPosicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPosicion.Location = new System.Drawing.Point(268, 91);
            this.txtPosicion.Name = "txtPosicion";
            this.txtPosicion.Size = new System.Drawing.Size(111, 21);
            this.txtPosicion.TabIndex = 247;
            this.txtPosicion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPosicion_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(35, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 15);
            this.label7.TabIndex = 234;
            this.label7.Text = "Tipo:";
            // 
            // txtModelo
            // 
            this.txtModelo.BackColor = System.Drawing.SystemColors.Control;
            this.txtModelo.Enabled = false;
            this.txtModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtModelo.Location = new System.Drawing.Point(75, 163);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(111, 21);
            this.txtModelo.TabIndex = 233;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(17, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 232;
            this.label2.Text = "Modelo:";
            // 
            // txtMarca
            // 
            this.txtMarca.BackColor = System.Drawing.SystemColors.Control;
            this.txtMarca.Enabled = false;
            this.txtMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMarca.Location = new System.Drawing.Point(75, 127);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(111, 21);
            this.txtMarca.TabIndex = 231;
            // 
            // txtCOD
            // 
            this.txtCOD.BackColor = System.Drawing.SystemColors.Control;
            this.txtCOD.Enabled = false;
            this.txtCOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCOD.Location = new System.Drawing.Point(75, 91);
            this.txtCOD.Name = "txtCOD";
            this.txtCOD.Size = new System.Drawing.Size(111, 21);
            this.txtCOD.TabIndex = 230;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(24, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 229;
            this.label6.Text = "Marca:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(209, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 228;
            this.label4.Text = "Posición:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(33, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 224;
            this.label3.Text = "COD:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(378, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 223;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(146, 247);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 34);
            this.btnAgregar.TabIndex = 221;
            this.btnAgregar.Tag = "5";
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtPlaca2
            // 
            this.txtPlaca2.BackColor = System.Drawing.SystemColors.Window;
            this.txtPlaca2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca2.Location = new System.Drawing.Point(75, 47);
            this.txtPlaca2.Name = "txtPlaca2";
            this.txtPlaca2.Size = new System.Drawing.Size(111, 21);
            this.txtPlaca2.TabIndex = 214;
            this.txtPlaca2.Enter += new System.EventHandler(this.txtPlaca2_Enter);
            this.txtPlaca2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca2_KeyPress);
            this.txtPlaca2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlaca2_KeyUp);
            this.txtPlaca2.Leave += new System.EventHandler(this.txtPlaca2_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 208;
            this.label5.Text = "Placa:";
            // 
            // lblInstalacion
            // 
            this.lblInstalacion.AutoSize = true;
            this.lblInstalacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblInstalacion.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblInstalacion.Location = new System.Drawing.Point(15, 12);
            this.lblInstalacion.Name = "lblInstalacion";
            this.lblInstalacion.Size = new System.Drawing.Size(132, 22);
            this.lblInstalacion.TabIndex = 49;
            this.lblInstalacion.Text = "DSDRSDTDT";
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(75, 67);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(304, 133);
            this.lstPlaca.TabIndex = 226;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // cbxTipo
            // 
            this.cbxTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipo.FormattingEnabled = true;
            this.cbxTipo.Items.AddRange(new object[] {
            "ORIGINAL",
            "1R",
            "2R",
            "3R"});
            this.cbxTipo.Location = new System.Drawing.Point(75, 199);
            this.cbxTipo.Name = "cbxTipo";
            this.cbxTipo.Size = new System.Drawing.Size(111, 23);
            this.cbxTipo.TabIndex = 246;
            this.cbxTipo.DropDownClosed += new System.EventHandler(this.cbxTipo_DropDownClosed);
            // 
            // frmListaCodNeumaticos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(999, 492);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pInstalaciones);
            this.Name = "frmListaCodNeumaticos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaCodNeumaticos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgNeumaticos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNeumaticosVista)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabLista.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabMovimientos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMovimientos)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientosVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pInstalaciones.ResumeLayout(false);
            this.pInstalaciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public DevExpress.XtraGrid.GridControl dtgNeumaticos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvNeumaticosVista;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsActualizarNeumatico;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLista;
        private System.Windows.Forms.TabPage tabMovimientos;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbxEstado;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbxMarca;
        private DevExpress.XtraEditors.SimpleButton btnNuevoNeumatico;
        private System.Windows.Forms.ToolStripMenuItem tsNuevoMov;
        public DevExpress.XtraGrid.GridControl dtgMovimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMovimientosVista;
        public System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.SimpleButton btnExcel2;
        public DevExpress.XtraEditors.SimpleButton btnBuscar2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtCodigo2;
        private System.Windows.Forms.Panel pInstalaciones;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCerrar;
        public DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.TextBox txtPlaca2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInstalacion;
        private System.Windows.Forms.ListView lstPlaca;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.TextBox txtCOD;
        public System.Windows.Forms.ComboBox cbxTipo;
        private System.Windows.Forms.TextBox txtPosicion;
        public System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtKM;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNSK;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsDesinstalarNeu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
    }
}