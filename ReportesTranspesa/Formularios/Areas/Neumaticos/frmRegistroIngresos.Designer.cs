namespace ReportesTranspesa.Formularios.Areas.Neumaticos
{
    partial class frmRegistroIngresos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroIngresos));
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtNeumatico = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtGuiaRemitente = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgRegistroIngresos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ingresarReclamoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desactivarIngresoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvRegistroIngresos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabIngresos = new System.Windows.Forms.TabPage();
            this.tabReclamos = new System.Windows.Forms.TabPage();
            this.dtgReclamo = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsActualizarEstado = new System.Windows.Forms.ToolStripMenuItem();
            this.tsQuitarReclamo = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvReclamoVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtNeumaticoReclamo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtGuiaReclamo = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dtpReclamoIni = new System.Windows.Forms.DateTimePicker();
            this.dtpReclamoFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExcelReclamo = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscarReclamo = new DevExpress.XtraEditors.SimpleButton();
            this.pIngresarReclamo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGRR2 = new System.Windows.Forms.TextBox();
            this.lblCantidadNeumatico = new System.Windows.Forms.Label();
            this.lblNombreNeumatico = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardarReclamo = new DevExpress.XtraEditors.SimpleButton();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.txtCantidadR = new System.Windows.Forms.TextBox();
            this.dtpFechaReclamo = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.lblGRR = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroIngresos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroIngresos)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.tabIngresos.SuspendLayout();
            this.tabReclamos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReclamo)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReclamoVista)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.pIngresarReclamo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1087, 45);
            this.label3.TabIndex = 120;
            this.label3.Text = "REGISTRO DE INGRESOS Y RECLAMOS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1073, 95);
            this.panel3.TabIndex = 183;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtNeumatico);
            this.groupBox3.Location = new System.Drawing.Point(460, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(344, 58);
            this.groupBox3.TabIndex = 235;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Descripción Neumático:  ";
            // 
            // txtNeumatico
            // 
            this.txtNeumatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNeumatico.Location = new System.Drawing.Point(14, 23);
            this.txtNeumatico.Name = "txtNeumatico";
            this.txtNeumatico.Size = new System.Drawing.Size(315, 20);
            this.txtNeumatico.TabIndex = 221;
            this.txtNeumatico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNeumatico_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtGuiaRemitente);
            this.groupBox2.Location = new System.Drawing.Point(286, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 58);
            this.groupBox2.TabIndex = 234;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Guía Remitente:  ";
            // 
            // txtGuiaRemitente
            // 
            this.txtGuiaRemitente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuiaRemitente.Location = new System.Drawing.Point(14, 23);
            this.txtGuiaRemitente.Name = "txtGuiaRemitente";
            this.txtGuiaRemitente.Size = new System.Drawing.Size(125, 20);
            this.txtGuiaRemitente.TabIndex = 221;
            this.txtGuiaRemitente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGuiaRemitente_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaIni);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(22, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 58);
            this.groupBox1.TabIndex = 233;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha Ingreso: ";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(17, 23);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaIni.TabIndex = 2;
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(135, 23);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(114, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "--";
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
            this.btnExcel.Location = new System.Drawing.Point(907, 25);
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
            this.btnBuscar.Location = new System.Drawing.Point(845, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgRegistroIngresos
            // 
            this.dtgRegistroIngresos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgRegistroIngresos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRegistroIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRegistroIngresos.Location = new System.Drawing.Point(3, 98);
            this.dtgRegistroIngresos.LookAndFeel.SkinName = "Office 2007 Blue";
            this.dtgRegistroIngresos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRegistroIngresos.MainView = this.dgvRegistroIngresos;
            this.dtgRegistroIngresos.Name = "dtgRegistroIngresos";
            this.dtgRegistroIngresos.Size = new System.Drawing.Size(1073, 383);
            this.dtgRegistroIngresos.TabIndex = 184;
            this.dtgRegistroIngresos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRegistroIngresos});
            this.dtgRegistroIngresos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgRegistroIngresos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ingresarReclamoToolStripMenuItem,
            this.desactivarIngresoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip3";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 48);
            // 
            // ingresarReclamoToolStripMenuItem
            // 
            this.ingresarReclamoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.excepciones;
            this.ingresarReclamoToolStripMenuItem.Name = "ingresarReclamoToolStripMenuItem";
            this.ingresarReclamoToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.ingresarReclamoToolStripMenuItem.Text = "Ingresar Reclamo";
            this.ingresarReclamoToolStripMenuItem.Click += new System.EventHandler(this.ingresarReclamoToolStripMenuItem_Click);
            // 
            // desactivarIngresoToolStripMenuItem
            // 
            this.desactivarIngresoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.desactivarIngresoToolStripMenuItem.Name = "desactivarIngresoToolStripMenuItem";
            this.desactivarIngresoToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.desactivarIngresoToolStripMenuItem.Text = "Quitar Ingreso";
            this.desactivarIngresoToolStripMenuItem.Click += new System.EventHandler(this.desactivarIngresoToolStripMenuItem_Click);
            // 
            // dgvRegistroIngresos
            // 
            this.dgvRegistroIngresos.GridControl = this.dtgRegistroIngresos;
            this.dgvRegistroIngresos.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvRegistroIngresos.Name = "dgvRegistroIngresos";
            this.dgvRegistroIngresos.OptionsBehavior.ReadOnly = true;
            this.dgvRegistroIngresos.OptionsView.ColumnAutoWidth = false;
            this.dgvRegistroIngresos.OptionsView.RowAutoHeight = true;
            this.dgvRegistroIngresos.OptionsView.ShowFooter = true;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabIngresos);
            this.tabInfo.Controls.Add(this.tabReclamos);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfo.Location = new System.Drawing.Point(0, 45);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(1087, 517);
            this.tabInfo.TabIndex = 187;
            // 
            // tabIngresos
            // 
            this.tabIngresos.Controls.Add(this.dtgRegistroIngresos);
            this.tabIngresos.Controls.Add(this.panel3);
            this.tabIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabIngresos.Location = new System.Drawing.Point(4, 29);
            this.tabIngresos.Name = "tabIngresos";
            this.tabIngresos.Padding = new System.Windows.Forms.Padding(3);
            this.tabIngresos.Size = new System.Drawing.Size(1079, 484);
            this.tabIngresos.TabIndex = 0;
            this.tabIngresos.Text = "INGRESOS";
            this.tabIngresos.UseVisualStyleBackColor = true;
            // 
            // tabReclamos
            // 
            this.tabReclamos.Controls.Add(this.dtgReclamo);
            this.tabReclamos.Controls.Add(this.panel1);
            this.tabReclamos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabReclamos.Location = new System.Drawing.Point(4, 29);
            this.tabReclamos.Name = "tabReclamos";
            this.tabReclamos.Padding = new System.Windows.Forms.Padding(3);
            this.tabReclamos.Size = new System.Drawing.Size(1079, 484);
            this.tabReclamos.TabIndex = 1;
            this.tabReclamos.Text = "RECLAMOS";
            this.tabReclamos.UseVisualStyleBackColor = true;
            // 
            // dtgReclamo
            // 
            this.dtgReclamo.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgReclamo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgReclamo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgReclamo.Location = new System.Drawing.Point(3, 98);
            this.dtgReclamo.LookAndFeel.SkinName = "Office 2007 Blue";
            this.dtgReclamo.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgReclamo.MainView = this.dgvReclamoVista;
            this.dtgReclamo.Name = "dtgReclamo";
            this.dtgReclamo.Size = new System.Drawing.Size(1073, 383);
            this.dtgReclamo.TabIndex = 184;
            this.dtgReclamo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvReclamoVista});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsActualizarEstado,
            this.tsQuitarReclamo});
            this.contextMenuStrip2.Name = "contextMenuStrip3";
            this.contextMenuStrip2.Size = new System.Drawing.Size(165, 70);
            // 
            // tsActualizarEstado
            // 
            this.tsActualizarEstado.Image = global::ReportesTranspesa.Properties.Resources.recargar;
            this.tsActualizarEstado.Name = "tsActualizarEstado";
            this.tsActualizarEstado.Size = new System.Drawing.Size(164, 22);
            this.tsActualizarEstado.Text = "Actualizar Estado";
            this.tsActualizarEstado.Click += new System.EventHandler(this.tsActualizarEstado_Click);
            // 
            // tsQuitarReclamo
            // 
            this.tsQuitarReclamo.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsQuitarReclamo.Name = "tsQuitarReclamo";
            this.tsQuitarReclamo.Size = new System.Drawing.Size(164, 22);
            this.tsQuitarReclamo.Text = "Quitar Reclamo";
            this.tsQuitarReclamo.Click += new System.EventHandler(this.tsQuitarReclamo_Click);
            // 
            // dgvReclamoVista
            // 
            this.dgvReclamoVista.GridControl = this.dtgReclamo;
            this.dgvReclamoVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvReclamoVista.Name = "dgvReclamoVista";
            this.dgvReclamoVista.OptionsBehavior.ReadOnly = true;
            this.dgvReclamoVista.OptionsView.ColumnAutoWidth = false;
            this.dgvReclamoVista.OptionsView.RowAutoHeight = true;
            this.dgvReclamoVista.OptionsView.ShowFooter = true;
            this.dgvReclamoVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvReclamoVista_CustomDrawCell);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.btnExcelReclamo);
            this.panel1.Controls.Add(this.btnBuscarReclamo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1073, 95);
            this.panel1.TabIndex = 183;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtNeumaticoReclamo);
            this.groupBox4.Location = new System.Drawing.Point(460, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(344, 58);
            this.groupBox4.TabIndex = 235;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Descripción Neumático:  ";
            // 
            // txtNeumaticoReclamo
            // 
            this.txtNeumaticoReclamo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNeumaticoReclamo.Location = new System.Drawing.Point(14, 23);
            this.txtNeumaticoReclamo.Name = "txtNeumaticoReclamo";
            this.txtNeumaticoReclamo.Size = new System.Drawing.Size(315, 20);
            this.txtNeumaticoReclamo.TabIndex = 221;
            this.txtNeumaticoReclamo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNeumaticoReclamo_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtGuiaReclamo);
            this.groupBox5.Location = new System.Drawing.Point(286, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(153, 58);
            this.groupBox5.TabIndex = 234;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Guía Reclamo:  ";
            // 
            // txtGuiaReclamo
            // 
            this.txtGuiaReclamo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuiaReclamo.Location = new System.Drawing.Point(14, 23);
            this.txtGuiaReclamo.Name = "txtGuiaReclamo";
            this.txtGuiaReclamo.Size = new System.Drawing.Size(125, 20);
            this.txtGuiaReclamo.TabIndex = 221;
            this.txtGuiaReclamo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGuiaReclamo_KeyPress);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dtpReclamoIni);
            this.groupBox6.Controls.Add(this.dtpReclamoFin);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Location = new System.Drawing.Point(22, 18);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(243, 58);
            this.groupBox6.TabIndex = 233;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Fecha Reclamo: ";
            // 
            // dtpReclamoIni
            // 
            this.dtpReclamoIni.CustomFormat = "dd-MM-yyyy";
            this.dtpReclamoIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReclamoIni.Location = new System.Drawing.Point(17, 23);
            this.dtpReclamoIni.Name = "dtpReclamoIni";
            this.dtpReclamoIni.Size = new System.Drawing.Size(91, 20);
            this.dtpReclamoIni.TabIndex = 2;
            this.dtpReclamoIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpReclamoIni_KeyPress);
            // 
            // dtpReclamoFin
            // 
            this.dtpReclamoFin.CustomFormat = "dd-MM-yyyy";
            this.dtpReclamoFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReclamoFin.Location = new System.Drawing.Point(135, 23);
            this.dtpReclamoFin.Name = "dtpReclamoFin";
            this.dtpReclamoFin.Size = new System.Drawing.Size(91, 20);
            this.dtpReclamoFin.TabIndex = 5;
            this.dtpReclamoFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpReclamoFin_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "--";
            // 
            // btnExcelReclamo
            // 
            this.btnExcelReclamo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcelReclamo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcelReclamo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcelReclamo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelReclamo.Appearance.Options.UseBackColor = true;
            this.btnExcelReclamo.Appearance.Options.UseBorderColor = true;
            this.btnExcelReclamo.Appearance.Options.UseFont = true;
            this.btnExcelReclamo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelReclamo.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelReclamo.Image")));
            this.btnExcelReclamo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcelReclamo.Location = new System.Drawing.Point(907, 25);
            this.btnExcelReclamo.Name = "btnExcelReclamo";
            this.btnExcelReclamo.Size = new System.Drawing.Size(51, 47);
            this.btnExcelReclamo.TabIndex = 197;
            this.btnExcelReclamo.Tag = "6";
            this.btnExcelReclamo.ToolTip = "Exportar a Excel";
            this.btnExcelReclamo.Click += new System.EventHandler(this.btnExcelReclamo_Click);
            // 
            // btnBuscarReclamo
            // 
            this.btnBuscarReclamo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscarReclamo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscarReclamo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscarReclamo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarReclamo.Appearance.Options.UseBackColor = true;
            this.btnBuscarReclamo.Appearance.Options.UseBorderColor = true;
            this.btnBuscarReclamo.Appearance.Options.UseFont = true;
            this.btnBuscarReclamo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarReclamo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarReclamo.Image")));
            this.btnBuscarReclamo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscarReclamo.Location = new System.Drawing.Point(845, 25);
            this.btnBuscarReclamo.Name = "btnBuscarReclamo";
            this.btnBuscarReclamo.Size = new System.Drawing.Size(47, 47);
            this.btnBuscarReclamo.TabIndex = 196;
            this.btnBuscarReclamo.Tag = "5";
            this.btnBuscarReclamo.ToolTip = "Buscar";
            this.btnBuscarReclamo.Click += new System.EventHandler(this.btnBuscarReclamo_Click);
            // 
            // pIngresarReclamo
            // 
            this.pIngresarReclamo.BackColor = System.Drawing.Color.LemonChiffon;
            this.pIngresarReclamo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pIngresarReclamo.Controls.Add(this.label2);
            this.pIngresarReclamo.Controls.Add(this.txtGRR2);
            this.pIngresarReclamo.Controls.Add(this.lblCantidadNeumatico);
            this.pIngresarReclamo.Controls.Add(this.lblNombreNeumatico);
            this.pIngresarReclamo.Controls.Add(this.label9);
            this.pIngresarReclamo.Controls.Add(this.label8);
            this.pIngresarReclamo.Controls.Add(this.label7);
            this.pIngresarReclamo.Controls.Add(this.label6);
            this.pIngresarReclamo.Controls.Add(this.lblTitulo);
            this.pIngresarReclamo.Controls.Add(this.btnCerrar);
            this.pIngresarReclamo.Controls.Add(this.btnGuardarReclamo);
            this.pIngresarReclamo.Controls.Add(this.txtMotivo);
            this.pIngresarReclamo.Controls.Add(this.txtCantidadR);
            this.pIngresarReclamo.Controls.Add(this.dtpFechaReclamo);
            this.pIngresarReclamo.Controls.Add(this.label12);
            this.pIngresarReclamo.Controls.Add(this.lblGRR);
            this.pIngresarReclamo.Controls.Add(this.label5);
            this.pIngresarReclamo.Location = new System.Drawing.Point(339, 135);
            this.pIngresarReclamo.Name = "pIngresarReclamo";
            this.pIngresarReclamo.Size = new System.Drawing.Size(408, 359);
            this.pIngresarReclamo.TabIndex = 188;
            this.pIngresarReclamo.Visible = false;
            this.pIngresarReclamo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pIngresarReclamo_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(15, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 244;
            this.label2.Text = "Guía de Remisión:";
            // 
            // txtGRR2
            // 
            this.txtGRR2.BackColor = System.Drawing.SystemColors.Window;
            this.txtGRR2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGRR2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGRR2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtGRR2.Location = new System.Drawing.Point(130, 251);
            this.txtGRR2.Name = "txtGRR2";
            this.txtGRR2.Size = new System.Drawing.Size(253, 21);
            this.txtGRR2.TabIndex = 245;
            this.txtGRR2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGRR2_KeyPress);
            // 
            // lblCantidadNeumatico
            // 
            this.lblCantidadNeumatico.AutoSize = true;
            this.lblCantidadNeumatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCantidadNeumatico.ForeColor = System.Drawing.Color.Red;
            this.lblCantidadNeumatico.Location = new System.Drawing.Point(289, 98);
            this.lblCantidadNeumatico.Name = "lblCantidadNeumatico";
            this.lblCantidadNeumatico.Size = new System.Drawing.Size(72, 17);
            this.lblCantidadNeumatico.TabIndex = 128;
            this.lblCantidadNeumatico.Text = "Cantidad";
            // 
            // lblNombreNeumatico
            // 
            this.lblNombreNeumatico.AutoSize = true;
            this.lblNombreNeumatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombreNeumatico.ForeColor = System.Drawing.Color.Red;
            this.lblNombreNeumatico.Location = new System.Drawing.Point(15, 72);
            this.lblNombreNeumatico.Name = "lblNombreNeumatico";
            this.lblNombreNeumatico.Size = new System.Drawing.Size(76, 15);
            this.lblNombreNeumatico.TabIndex = 127;
            this.lblNombreNeumatico.Text = "Neumático";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(214, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 17);
            this.label9.TabIndex = 126;
            this.label9.Text = "Cantidad:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(15, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 17);
            this.label8.TabIndex = 125;
            this.label8.Text = "Neumático:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(248, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 15);
            this.label7.TabIndex = 122;
            this.label7.Text = "Cantidad:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(15, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 121;
            this.label6.Text = "Motivo:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Red;
            this.lblTitulo.Location = new System.Drawing.Point(14, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(212, 22);
            this.lblTitulo.TabIndex = 120;
            this.lblTitulo.Text = "INGRESAR RECLAMO";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(373, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(29, 29);
            this.btnCerrar.TabIndex = 119;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGuardarReclamo
            // 
            this.btnGuardarReclamo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardarReclamo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnGuardarReclamo.Appearance.Options.UseFont = true;
            this.btnGuardarReclamo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarReclamo.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarReclamo.Image")));
            this.btnGuardarReclamo.Location = new System.Drawing.Point(155, 299);
            this.btnGuardarReclamo.Name = "btnGuardarReclamo";
            this.btnGuardarReclamo.Size = new System.Drawing.Size(104, 44);
            this.btnGuardarReclamo.TabIndex = 117;
            this.btnGuardarReclamo.Text = "GUARDAR";
            this.btnGuardarReclamo.Click += new System.EventHandler(this.btnGuardarReclamo_Click);
            // 
            // txtMotivo
            // 
            this.txtMotivo.BackColor = System.Drawing.SystemColors.Window;
            this.txtMotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMotivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMotivo.Location = new System.Drawing.Point(67, 133);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(316, 53);
            this.txtMotivo.TabIndex = 123;
            this.txtMotivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMotivo_KeyPress);
            // 
            // txtCantidadR
            // 
            this.txtCantidadR.BackColor = System.Drawing.SystemColors.Window;
            this.txtCantidadR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCantidadR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCantidadR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidadR.Location = new System.Drawing.Point(313, 208);
            this.txtCantidadR.Name = "txtCantidadR";
            this.txtCantidadR.Size = new System.Drawing.Size(70, 21);
            this.txtCantidadR.TabIndex = 124;
            this.txtCantidadR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadR_KeyPress);
            // 
            // dtpFechaReclamo
            // 
            this.dtpFechaReclamo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaReclamo.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaReclamo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaReclamo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaReclamo.Location = new System.Drawing.Point(118, 208);
            this.dtpFechaReclamo.Name = "dtpFechaReclamo";
            this.dtpFechaReclamo.Size = new System.Drawing.Size(104, 21);
            this.dtpFechaReclamo.TabIndex = 239;
            this.dtpFechaReclamo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaReclamo_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label12.Location = new System.Drawing.Point(15, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 15);
            this.label12.TabIndex = 238;
            this.label12.Text = "Fecha Reclamo:";
            // 
            // lblGRR
            // 
            this.lblGRR.AutoSize = true;
            this.lblGRR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblGRR.ForeColor = System.Drawing.Color.Red;
            this.lblGRR.Location = new System.Drawing.Point(60, 98);
            this.lblGRR.Name = "lblGRR";
            this.lblGRR.Size = new System.Drawing.Size(123, 17);
            this.lblGRR.TabIndex = 243;
            this.lblGRR.Text = "T101-00003851";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(15, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 242;
            this.label5.Text = "GRR:";
            // 
            // frmRegistroIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 562);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pIngresarReclamo);
            this.Name = "frmRegistroIngresos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRegistroIngresos";
            this.Load += new System.EventHandler(this.frmRegistroIngresos_Load);
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroIngresos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroIngresos)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.tabIngresos.ResumeLayout(false);
            this.tabReclamos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgReclamo)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReclamoVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.pIngresarReclamo.ResumeLayout(false);
            this.pIngresarReclamo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtGuiaRemitente;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNeumatico;
        private DevExpress.XtraGrid.GridControl dtgRegistroIngresos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRegistroIngresos;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem desactivarIngresoToolStripMenuItem;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabIngresos;
        private System.Windows.Forms.TabPage tabReclamos;
        private DevExpress.XtraGrid.GridControl dtgReclamo;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvReclamoVista;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtNeumaticoReclamo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtGuiaReclamo;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DateTimePicker dtpReclamoIni;
        private System.Windows.Forms.DateTimePicker dtpReclamoFin;
        private System.Windows.Forms.Label label1;
        public DevExpress.XtraEditors.SimpleButton btnExcelReclamo;
        public DevExpress.XtraEditors.SimpleButton btnBuscarReclamo;
        private System.Windows.Forms.ToolStripMenuItem ingresarReclamoToolStripMenuItem;
        private System.Windows.Forms.Panel pIngresarReclamo;
        private System.Windows.Forms.Label lblGRR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.DateTimePicker dtpFechaReclamo;
        private System.Windows.Forms.Label lblCantidadNeumatico;
        private System.Windows.Forms.Label lblNombreNeumatico;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTitulo;
        public System.Windows.Forms.Button btnCerrar;
        private DevExpress.XtraEditors.SimpleButton btnGuardarReclamo;
        internal System.Windows.Forms.TextBox txtMotivo;
        internal System.Windows.Forms.TextBox txtCantidadR;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtGRR2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsActualizarEstado;
        private System.Windows.Forms.ToolStripMenuItem tsQuitarReclamo;
    }
}