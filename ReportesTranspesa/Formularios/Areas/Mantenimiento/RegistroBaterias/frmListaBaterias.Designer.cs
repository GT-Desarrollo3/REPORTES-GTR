namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroBaterias
{
    partial class frmListaBaterias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaBaterias));
            this.label1 = new System.Windows.Forms.Label();
            this.dtgBaterias = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modificarBateriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almacenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGenerarInspeccion = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvBateriasVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAgregar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHistorial = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsHistorialInspecciones = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.habilitarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pInactivarBateria = new System.Windows.Forms.Panel();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.btnInactivar = new DevExpress.XtraEditors.SimpleButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.traspasoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarBateriaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.anularBateriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbInactivas = new System.Windows.Forms.RadioButton();
            this.rbActivas = new System.Windows.Forms.RadioButton();
            this.rbAlmacen = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.rbFechaCambio = new System.Windows.Forms.RadioButton();
            this.rbFechaInspeccion = new System.Windows.Forms.RadioButton();
            this.dtpFechaCFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaCIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.cbFiltroFechas = new System.Windows.Forms.CheckBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pAgregarInspeccion = new System.Windows.Forms.Panel();
            this.txtEstadoB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNivelCarga = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpFechaCambio = new System.Windows.Forms.DateTimePicker();
            this.txtIntervalo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblProgramacion = new System.Windows.Forms.Label();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.lblBateria = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaInspeccion = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBaterias)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBateriasVista)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.pInactivarBateria.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pAgregarInspeccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(900, 43);
            this.label1.TabIndex = 19;
            this.label1.Text = "REGISTRO DE BATERÍAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgBaterias
            // 
            this.dtgBaterias.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgBaterias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgBaterias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgBaterias.Location = new System.Drawing.Point(0, 186);
            this.dtgBaterias.MainView = this.dgvBateriasVista;
            this.dtgBaterias.Name = "dtgBaterias";
            this.dtgBaterias.Size = new System.Drawing.Size(900, 304);
            this.dtgBaterias.TabIndex = 182;
            this.dtgBaterias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvBateriasVista});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarBateriaToolStripMenuItem,
            this.almacenToolStripMenuItem,
            this.tsGenerarInspeccion});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 70);
            // 
            // modificarBateriaToolStripMenuItem
            // 
            this.modificarBateriaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.modificarBateriaToolStripMenuItem.Name = "modificarBateriaToolStripMenuItem";
            this.modificarBateriaToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.modificarBateriaToolStripMenuItem.Text = "Modificar Batería";
            this.modificarBateriaToolStripMenuItem.Click += new System.EventHandler(this.modificarBateriaToolStripMenuItem_Click);
            // 
            // almacenToolStripMenuItem
            // 
            this.almacenToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.almacenToolStripMenuItem.Name = "almacenToolStripMenuItem";
            this.almacenToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.almacenToolStripMenuItem.Text = "Enviar a Almacén";
            this.almacenToolStripMenuItem.Click += new System.EventHandler(this.almacenToolStripMenuItem_Click);
            // 
            // tsGenerarInspeccion
            // 
            this.tsGenerarInspeccion.Image = global::ReportesTranspesa.Properties.Resources.bonoseguridad;
            this.tsGenerarInspeccion.Name = "tsGenerarInspeccion";
            this.tsGenerarInspeccion.Size = new System.Drawing.Size(175, 22);
            this.tsGenerarInspeccion.Text = "Generar Inspección";
            this.tsGenerarInspeccion.Click += new System.EventHandler(this.tsGenerarInspeccion_Click);
            // 
            // dgvBateriasVista
            // 
            this.dgvBateriasVista.GridControl = this.dtgBaterias;
            this.dgvBateriasVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvBateriasVista.Name = "dgvBateriasVista";
            this.dgvBateriasVista.OptionsBehavior.Editable = false;
            this.dgvBateriasVista.OptionsBehavior.ReadOnly = true;
            this.dgvBateriasVista.OptionsView.ColumnAutoWidth = false;
            this.dgvBateriasVista.OptionsView.RowAutoHeight = true;
            this.dgvBateriasVista.OptionsView.ShowFooter = true;
            this.dgvBateriasVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvBateriasVista_CustomDrawCell);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LemonChiffon;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAgregar,
            this.toolStripSeparator1,
            this.btnHistorial,
            this.toolStripSeparator2,
            this.tsHistorialInspecciones});
            this.toolStrip1.Location = new System.Drawing.Point(0, 161);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(900, 25);
            this.toolStrip1.TabIndex = 184;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAgregar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(112, 22);
            this.btnAgregar.Text = "Registrar Batería";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnHistorial
            // 
            this.btnHistorial.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.btnHistorial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(140, 22);
            this.btnHistorial.Text = "Historial de Traspasos";
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsHistorialInspecciones
            // 
            this.tsHistorialInspecciones.Image = global::ReportesTranspesa.Properties.Resources.ordenar;
            this.tsHistorialInspecciones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsHistorialInspecciones.Name = "tsHistorialInspecciones";
            this.tsHistorialInspecciones.Size = new System.Drawing.Size(158, 22);
            this.tsHistorialInspecciones.Text = "Historial de Inspecciones";
            this.tsHistorialInspecciones.Click += new System.EventHandler(this.tsHistorialInspecciones_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.habilitarToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(120, 26);
            // 
            // habilitarToolStripMenuItem
            // 
            this.habilitarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.liberar;
            this.habilitarToolStripMenuItem.Name = "habilitarToolStripMenuItem";
            this.habilitarToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.habilitarToolStripMenuItem.Text = "Habilitar";
            this.habilitarToolStripMenuItem.Click += new System.EventHandler(this.habilitarToolStripMenuItem_Click);
            // 
            // pInactivarBateria
            // 
            this.pInactivarBateria.BackColor = System.Drawing.SystemColors.Control;
            this.pInactivarBateria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pInactivarBateria.Controls.Add(this.txtMotivo);
            this.pInactivarBateria.Controls.Add(this.btnInactivar);
            this.pInactivarBateria.Controls.Add(this.label5);
            this.pInactivarBateria.Controls.Add(this.label3);
            this.pInactivarBateria.Controls.Add(this.btnCerrar);
            this.pInactivarBateria.Location = new System.Drawing.Point(512, 257);
            this.pInactivarBateria.Name = "pInactivarBateria";
            this.pInactivarBateria.Size = new System.Drawing.Size(383, 219);
            this.pInactivarBateria.TabIndex = 185;
            this.pInactivarBateria.Visible = false;
            this.pInactivarBateria.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pInactivarBateria_MouseMove);
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(21, 76);
            this.txtMotivo.MaxLength = 150;
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(338, 71);
            this.txtMotivo.TabIndex = 203;
            // 
            // btnInactivar
            // 
            this.btnInactivar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnInactivar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnInactivar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnInactivar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInactivar.Appearance.Options.UseBackColor = true;
            this.btnInactivar.Appearance.Options.UseBorderColor = true;
            this.btnInactivar.Appearance.Options.UseFont = true;
            this.btnInactivar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInactivar.Image = ((System.Drawing.Image)(resources.GetObject("btnInactivar.Image")));
            this.btnInactivar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnInactivar.Location = new System.Drawing.Point(133, 164);
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.Size = new System.Drawing.Size(114, 38);
            this.btnInactivar.TabIndex = 122;
            this.btnInactivar.Text = "Inactivar";
            this.btnInactivar.ToolTip = "Inactivar";
            this.btnInactivar.Click += new System.EventHandler(this.btnInactivar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 120;
            this.label5.Text = "Ingresar Motivo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "INACTIVAR BATERÍA";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(345, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar.TabIndex = 18;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.traspasoToolStripMenuItem,
            this.modificarBateriaToolStripMenuItem1,
            this.anularBateriaToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip1";
            this.contextMenuStrip3.Size = new System.Drawing.Size(165, 70);
            // 
            // traspasoToolStripMenuItem
            // 
            this.traspasoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.traspasoToolStripMenuItem.Name = "traspasoToolStripMenuItem";
            this.traspasoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.traspasoToolStripMenuItem.Text = "Traspaso";
            this.traspasoToolStripMenuItem.Click += new System.EventHandler(this.traspasoToolStripMenuItem_Click);
            // 
            // modificarBateriaToolStripMenuItem1
            // 
            this.modificarBateriaToolStripMenuItem1.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.modificarBateriaToolStripMenuItem1.Name = "modificarBateriaToolStripMenuItem1";
            this.modificarBateriaToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.modificarBateriaToolStripMenuItem1.Text = "Modificar Batería";
            this.modificarBateriaToolStripMenuItem1.Click += new System.EventHandler(this.modificarBateriaToolStripMenuItem1_Click);
            // 
            // anularBateriaToolStripMenuItem
            // 
            this.anularBateriaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.stop;
            this.anularBateriaToolStripMenuItem.Name = "anularBateriaToolStripMenuItem";
            this.anularBateriaToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.anularBateriaToolStripMenuItem.Text = "Inactivar Batería";
            this.anularBateriaToolStripMenuItem.Click += new System.EventHandler(this.anularBateriaToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbInactivas);
            this.groupBox1.Controls.Add(this.rbActivas);
            this.groupBox1.Controls.Add(this.rbAlmacen);
            this.groupBox1.Location = new System.Drawing.Point(582, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 85);
            this.groupBox1.TabIndex = 202;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Estado:";
            // 
            // rbInactivas
            // 
            this.rbInactivas.AutoSize = true;
            this.rbInactivas.Location = new System.Drawing.Point(15, 59);
            this.rbInactivas.Name = "rbInactivas";
            this.rbInactivas.Size = new System.Drawing.Size(68, 17);
            this.rbInactivas.TabIndex = 201;
            this.rbInactivas.Text = "Inactivas";
            this.rbInactivas.UseVisualStyleBackColor = true;
            this.rbInactivas.Click += new System.EventHandler(this.rbInactivas_Click);
            // 
            // rbActivas
            // 
            this.rbActivas.AutoSize = true;
            this.rbActivas.Location = new System.Drawing.Point(15, 17);
            this.rbActivas.Name = "rbActivas";
            this.rbActivas.Size = new System.Drawing.Size(60, 17);
            this.rbActivas.TabIndex = 200;
            this.rbActivas.Text = "Activas";
            this.rbActivas.UseVisualStyleBackColor = true;
            this.rbActivas.Click += new System.EventHandler(this.rbActivas_Click);
            // 
            // rbAlmacen
            // 
            this.rbAlmacen.AutoSize = true;
            this.rbAlmacen.Location = new System.Drawing.Point(15, 38);
            this.rbAlmacen.Name = "rbAlmacen";
            this.rbAlmacen.Size = new System.Drawing.Size(82, 17);
            this.rbAlmacen.TabIndex = 202;
            this.rbAlmacen.Text = "En Almacén";
            this.rbAlmacen.UseVisualStyleBackColor = true;
            this.rbAlmacen.Click += new System.EventHandler(this.rbAlmacen_Click);
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
            this.btnBuscar.Location = new System.Drawing.Point(756, 36);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(817, 36);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 197;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.rbFechaCambio);
            this.groupBox15.Controls.Add(this.rbFechaInspeccion);
            this.groupBox15.Controls.Add(this.dtpFechaCFin);
            this.groupBox15.Controls.Add(this.label2);
            this.groupBox15.Controls.Add(this.dtpFechaCIni);
            this.groupBox15.Controls.Add(this.dtpFechaFin);
            this.groupBox15.Controls.Add(this.label40);
            this.groupBox15.Controls.Add(this.dtpFechaIni);
            this.groupBox15.Location = new System.Drawing.Point(236, 16);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(321, 85);
            this.groupBox15.TabIndex = 199;
            this.groupBox15.TabStop = false;
            // 
            // rbFechaCambio
            // 
            this.rbFechaCambio.AutoSize = true;
            this.rbFechaCambio.Location = new System.Drawing.Point(13, 53);
            this.rbFechaCambio.Name = "rbFechaCambio";
            this.rbFechaCambio.Size = new System.Drawing.Size(78, 17);
            this.rbFechaCambio.TabIndex = 208;
            this.rbFechaCambio.Text = "F. Cambio: ";
            this.rbFechaCambio.UseVisualStyleBackColor = true;
            this.rbFechaCambio.Click += new System.EventHandler(this.rbFechaCambio_Click);
            // 
            // rbFechaInspeccion
            // 
            this.rbFechaInspeccion.AutoSize = true;
            this.rbFechaInspeccion.Location = new System.Drawing.Point(13, 24);
            this.rbFechaInspeccion.Name = "rbFechaInspeccion";
            this.rbFechaInspeccion.Size = new System.Drawing.Size(83, 17);
            this.rbFechaInspeccion.TabIndex = 207;
            this.rbFechaInspeccion.Text = "Inspección: ";
            this.rbFechaInspeccion.UseVisualStyleBackColor = true;
            this.rbFechaInspeccion.Click += new System.EventHandler(this.rbFechaInspeccion_Click);
            // 
            // dtpFechaCFin
            // 
            this.dtpFechaCFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaCFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCFin.Location = new System.Drawing.Point(212, 52);
            this.dtpFechaCFin.Name = "dtpFechaCFin";
            this.dtpFechaCFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaCFin.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "-";
            // 
            // dtpFechaCIni
            // 
            this.dtpFechaCIni.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaCIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCIni.Location = new System.Drawing.Point(102, 52);
            this.dtpFechaCIni.Name = "dtpFechaCIni";
            this.dtpFechaCIni.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaCIni.TabIndex = 2;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(212, 23);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(199, 27);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(11, 13);
            this.label40.TabIndex = 4;
            this.label40.Text = "-";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(102, 23);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaIni.TabIndex = 2;
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // cbFiltroFechas
            // 
            this.cbFiltroFechas.AutoSize = true;
            this.cbFiltroFechas.Location = new System.Drawing.Point(245, 15);
            this.cbFiltroFechas.Name = "cbFiltroFechas";
            this.cbFiltroFechas.Size = new System.Drawing.Size(102, 17);
            this.cbFiltroFechas.TabIndex = 206;
            this.cbFiltroFechas.Text = "Filtro de Fecha: ";
            this.cbFiltroFechas.UseVisualStyleBackColor = true;
            this.cbFiltroFechas.CheckedChanged += new System.EventHandler(this.cbFechaInspeccion_CheckedChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(67, 33);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(144, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 203;
            this.label9.Text = "Código:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(67, 70);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(144, 20);
            this.txtPlaca.TabIndex = 204;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 205;
            this.label10.Text = "Placa:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtPlaca);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtCodigo);
            this.panel3.Controls.Add(this.cbFiltroFechas);
            this.panel3.Controls.Add(this.groupBox15);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 118);
            this.panel3.TabIndex = 181;
            // 
            // pAgregarInspeccion
            // 
            this.pAgregarInspeccion.BackColor = System.Drawing.Color.LemonChiffon;
            this.pAgregarInspeccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pAgregarInspeccion.Controls.Add(this.txtEstadoB);
            this.pAgregarInspeccion.Controls.Add(this.label15);
            this.pAgregarInspeccion.Controls.Add(this.txtNivelCarga);
            this.pAgregarInspeccion.Controls.Add(this.label4);
            this.pAgregarInspeccion.Controls.Add(this.label8);
            this.pAgregarInspeccion.Controls.Add(this.label13);
            this.pAgregarInspeccion.Controls.Add(this.dtpFechaCambio);
            this.pAgregarInspeccion.Controls.Add(this.txtIntervalo);
            this.pAgregarInspeccion.Controls.Add(this.label14);
            this.pAgregarInspeccion.Controls.Add(this.lblProgramacion);
            this.pAgregarInspeccion.Controls.Add(this.lblUnidad);
            this.pAgregarInspeccion.Controls.Add(this.label6);
            this.pAgregarInspeccion.Controls.Add(this.label7);
            this.pAgregarInspeccion.Controls.Add(this.label11);
            this.pAgregarInspeccion.Controls.Add(this.btnCerrar2);
            this.pAgregarInspeccion.Controls.Add(this.lblBateria);
            this.pAgregarInspeccion.Controls.Add(this.label12);
            this.pAgregarInspeccion.Controls.Add(this.dtpFechaInspeccion);
            this.pAgregarInspeccion.Controls.Add(this.btnGuardar);
            this.pAgregarInspeccion.Location = new System.Drawing.Point(485, 193);
            this.pAgregarInspeccion.Name = "pAgregarInspeccion";
            this.pAgregarInspeccion.Size = new System.Drawing.Size(413, 295);
            this.pAgregarInspeccion.TabIndex = 186;
            this.pAgregarInspeccion.Visible = false;
            this.pAgregarInspeccion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pAgregarInspeccion_MouseMove);
            // 
            // txtEstadoB
            // 
            this.txtEstadoB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstadoB.Location = new System.Drawing.Point(306, 152);
            this.txtEstadoB.Name = "txtEstadoB";
            this.txtEstadoB.Size = new System.Drawing.Size(75, 22);
            this.txtEstadoB.TabIndex = 216;
            this.txtEstadoB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEstado_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(223, 155);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 16);
            this.label15.TabIndex = 215;
            this.label15.Text = "Estado (%):";
            // 
            // txtNivelCarga
            // 
            this.txtNivelCarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNivelCarga.Location = new System.Drawing.Point(125, 152);
            this.txtNivelCarga.Name = "txtNivelCarga";
            this.txtNivelCarga.Size = new System.Drawing.Size(75, 22);
            this.txtNivelCarga.TabIndex = 214;
            this.txtNivelCarga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNivelCarga_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 32);
            this.label4.TabIndex = 213;
            this.label4.Text = "Nivel de Carga\r\n(%):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 16);
            this.label8.TabIndex = 212;
            this.label8.Text = "F. Inspección:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(223, 186);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 32);
            this.label13.TabIndex = 210;
            this.label13.Text = "Fecha\r\nCambio:";
            // 
            // dtpFechaCambio
            // 
            this.dtpFechaCambio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaCambio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCambio.Location = new System.Drawing.Point(287, 191);
            this.dtpFechaCambio.Name = "dtpFechaCambio";
            this.dtpFechaCambio.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaCambio.TabIndex = 209;
            this.dtpFechaCambio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaCambio_KeyPress);
            // 
            // txtIntervalo
            // 
            this.txtIntervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntervalo.Location = new System.Drawing.Point(125, 191);
            this.txtIntervalo.Name = "txtIntervalo";
            this.txtIntervalo.Size = new System.Drawing.Size(75, 22);
            this.txtIntervalo.TabIndex = 208;
            this.txtIntervalo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalo_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(15, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 16);
            this.label14.TabIndex = 207;
            this.label14.Text = "Duración (Días):";
            // 
            // lblProgramacion
            // 
            this.lblProgramacion.AutoSize = true;
            this.lblProgramacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblProgramacion.ForeColor = System.Drawing.Color.Red;
            this.lblProgramacion.Location = new System.Drawing.Point(304, 72);
            this.lblProgramacion.Name = "lblProgramacion";
            this.lblProgramacion.Size = new System.Drawing.Size(75, 18);
            this.lblProgramacion.TabIndex = 205;
            this.lblProgramacion.Text = "LINDLEY";
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblUnidad.ForeColor = System.Drawing.Color.Red;
            this.lblUnidad.Location = new System.Drawing.Point(78, 72);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(73, 18);
            this.lblUnidad.TabIndex = 203;
            this.lblUnidad.Text = "T4G-852";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(15, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 18);
            this.label6.TabIndex = 202;
            this.label6.Text = "Unidad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(15, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "Batería:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(14, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(232, 22);
            this.label11.TabIndex = 29;
            this.label11.Text = "GENERAR INSPECCIÓN";
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar2.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.Location = new System.Drawing.Point(384, 0);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(27, 27);
            this.btnCerrar2.TabIndex = 28;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // lblBateria
            // 
            this.lblBateria.AutoSize = true;
            this.lblBateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblBateria.ForeColor = System.Drawing.Color.Red;
            this.lblBateria.Location = new System.Drawing.Point(80, 45);
            this.lblBateria.Name = "lblBateria";
            this.lblBateria.Size = new System.Drawing.Size(73, 18);
            this.lblBateria.TabIndex = 27;
            this.lblBateria.Text = "GT-2867";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(187, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 18);
            this.label12.TabIndex = 206;
            this.label12.Text = "Programación:";
            // 
            // dtpFechaInspeccion
            // 
            this.dtpFechaInspeccion.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInspeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInspeccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInspeccion.Location = new System.Drawing.Point(111, 112);
            this.dtpFechaInspeccion.Name = "dtpFechaInspeccion";
            this.dtpFechaInspeccion.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaInspeccion.TabIndex = 211;
            this.dtpFechaInspeccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInspeccion_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(156, 242);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 36);
            this.btnGuardar.TabIndex = 201;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmListaBaterias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 490);
            this.Controls.Add(this.dtgBaterias);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pInactivarBateria);
            this.Controls.Add(this.pAgregarInspeccion);
            this.Name = "frmListaBaterias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MANTENEDOR DE BATERÍAS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ListaBaterias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgBaterias)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBateriasVista)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.pInactivarBateria.ResumeLayout(false);
            this.pInactivarBateria.PerformLayout();
            this.contextMenuStrip3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pAgregarInspeccion.ResumeLayout(false);
            this.pAgregarInspeccion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgBaterias;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvBateriasVista;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modificarBateriaToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAgregar;
        private System.Windows.Forms.ToolStripButton btnHistorial;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Panel pInactivarBateria;
        private DevExpress.XtraEditors.SimpleButton btnInactivar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.ToolStripMenuItem almacenToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem traspasoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habilitarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anularBateriaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarBateriaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsHistorialInspecciones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbInactivas;
        private System.Windows.Forms.RadioButton rbActivas;
        private System.Windows.Forms.RadioButton rbAlmacen;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.RadioButton rbFechaCambio;
        private System.Windows.Forms.RadioButton rbFechaInspeccion;
        private System.Windows.Forms.DateTimePicker dtpFechaCFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaCIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.CheckBox cbFiltroFechas;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem tsGenerarInspeccion;
        private System.Windows.Forms.Panel pAgregarInspeccion;
        private System.Windows.Forms.Label lblProgramacion;
        private System.Windows.Forms.Label lblUnidad;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Button btnCerrar2;
        private System.Windows.Forms.Label lblBateria;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtNivelCarga;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.DateTimePicker dtpFechaInspeccion;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.DateTimePicker dtpFechaCambio;
        public System.Windows.Forms.TextBox txtIntervalo;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox txtEstadoB;
        public System.Windows.Forms.Label label15;
    }
}