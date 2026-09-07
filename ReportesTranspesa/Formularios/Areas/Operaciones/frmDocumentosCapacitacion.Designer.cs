namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmDocumentosCapacitacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentosCapacitacion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBConductor = new System.Windows.Forms.TextBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtBCapacitacion = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnNuevaCapacitacion = new DevExpress.XtraEditors.SimpleButton();
            this.dtgDocumentos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarDocumento = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDocumentosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pImportarDocumentos = new System.Windows.Forms.Panel();
            this.cbCircuito = new System.Windows.Forms.CheckBox();
            this.cbRevision = new System.Windows.Forms.CheckBox();
            this.chBloque = new System.Windows.Forms.CheckBox();
            this.btnAgregar = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.txtNotaExamen = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtgAsistentes = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsQuitarAsistente = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAsistentesVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInstructor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxBase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtTituloD = new System.Windows.Forms.TextBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCerrarLocal = new System.Windows.Forms.Button();
            this.txtRutaLocal = new System.Windows.Forms.RichTextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscarArchivo = new DevExpress.XtraEditors.SimpleButton();
            this.txtCapacitacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocumentos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosVista)).BeginInit();
            this.pImportarDocumentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAsistentes)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistentesVista)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBConductor);
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtBCapacitacion);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnNuevaCapacitacion);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 111);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Búsqueda";
            // 
            // txtBConductor
            // 
            this.txtBConductor.BackColor = System.Drawing.Color.Honeydew;
            this.txtBConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtBConductor.Location = new System.Drawing.Point(514, 67);
            this.txtBConductor.Name = "txtBConductor";
            this.txtBConductor.Size = new System.Drawing.Size(363, 21);
            this.txtBConductor.TabIndex = 235;
            this.txtBConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(960, 39);
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
            this.label3.Location = new System.Drawing.Point(442, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 230;
            this.label3.Text = "Conductor:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(427, 34);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(81, 15);
            this.label28.TabIndex = 227;
            this.label28.Text = "Capacitación:";
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
            this.btnBuscar.Location = new System.Drawing.Point(904, 39);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 224;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBCapacitacion
            // 
            this.txtBCapacitacion.BackColor = System.Drawing.Color.Honeydew;
            this.txtBCapacitacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtBCapacitacion.Location = new System.Drawing.Point(514, 31);
            this.txtBCapacitacion.Name = "txtBCapacitacion";
            this.txtBCapacitacion.Size = new System.Drawing.Size(363, 21);
            this.txtBCapacitacion.TabIndex = 112;
            this.txtBCapacitacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFechaIni);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(167, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 61);
            this.groupBox2.TabIndex = 234;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar por Fecha: ";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(9, 25);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 1;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(111, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 229;
            this.label2.Text = "--";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(130, 25);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 2;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // btnNuevaCapacitacion
            // 
            this.btnNuevaCapacitacion.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevaCapacitacion.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevaCapacitacion.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevaCapacitacion.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaCapacitacion.Appearance.Options.UseBackColor = true;
            this.btnNuevaCapacitacion.Appearance.Options.UseBorderColor = true;
            this.btnNuevaCapacitacion.Appearance.Options.UseFont = true;
            this.btnNuevaCapacitacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaCapacitacion.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaCapacitacion.Image")));
            this.btnNuevaCapacitacion.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevaCapacitacion.Location = new System.Drawing.Point(22, 39);
            this.btnNuevaCapacitacion.Name = "btnNuevaCapacitacion";
            this.btnNuevaCapacitacion.Size = new System.Drawing.Size(120, 47);
            this.btnNuevaCapacitacion.TabIndex = 236;
            this.btnNuevaCapacitacion.Text = "Agregar\r\nCapacitación";
            this.btnNuevaCapacitacion.ToolTip = "Agregar Capacitación";
            this.btnNuevaCapacitacion.Click += new System.EventHandler(this.btnNuevaCapacitacion_Click);
            // 
            // dtgDocumentos
            // 
            this.dtgDocumentos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgDocumentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDocumentos.Location = new System.Drawing.Point(20, 191);
            this.dtgDocumentos.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dtgDocumentos.LookAndFeel.SkinName = "Money Twins";
            this.dtgDocumentos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDocumentos.MainView = this.dgvDocumentosVista;
            this.dtgDocumentos.Name = "dtgDocumentos";
            this.dtgDocumentos.Size = new System.Drawing.Size(1035, 395);
            this.dtgDocumentos.TabIndex = 187;
            this.dtgDocumentos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvDocumentosVista});
            this.dtgDocumentos.DoubleClick += new System.EventHandler(this.dtgDocumentos_DoubleClick);
            this.dtgDocumentos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgDocumentos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarDocumento});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 26);
            // 
            // tsEliminarDocumento
            // 
            this.tsEliminarDocumento.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsEliminarDocumento.Name = "tsEliminarDocumento";
            this.tsEliminarDocumento.Size = new System.Drawing.Size(179, 22);
            this.tsEliminarDocumento.Text = "Quitar Capacitación";
            this.tsEliminarDocumento.Click += new System.EventHandler(this.tsEliminarDocumento_Click);
            // 
            // dgvDocumentosVista
            // 
            this.dgvDocumentosVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDocumentosVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvDocumentosVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDocumentosVista.Appearance.Row.Options.UseFont = true;
            this.dgvDocumentosVista.GridControl = this.dtgDocumentos;
            this.dgvDocumentosVista.Name = "dgvDocumentosVista";
            this.dgvDocumentosVista.OptionsBehavior.Editable = false;
            this.dgvDocumentosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvDocumentosVista.OptionsView.RowAutoHeight = true;
            this.dgvDocumentosVista.OptionsView.ShowFooter = true;
            this.dgvDocumentosVista.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvDocumentosVista_RowCellClick);
            this.dgvDocumentosVista.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.dgvDocumentosVista_CellMerge);
            this.dgvDocumentosVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvDocumentosVista_CustomDrawCell);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1035, 20);
            this.panel2.TabIndex = 188;
            // 
            // pImportarDocumentos
            // 
            this.pImportarDocumentos.BackColor = System.Drawing.Color.White;
            this.pImportarDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pImportarDocumentos.Controls.Add(this.cbCircuito);
            this.pImportarDocumentos.Controls.Add(this.cbRevision);
            this.pImportarDocumentos.Controls.Add(this.chBloque);
            this.pImportarDocumentos.Controls.Add(this.btnAgregar);
            this.pImportarDocumentos.Controls.Add(this.label11);
            this.pImportarDocumentos.Controls.Add(this.cbxEstado);
            this.pImportarDocumentos.Controls.Add(this.txtNotaExamen);
            this.pImportarDocumentos.Controls.Add(this.label10);
            this.pImportarDocumentos.Controls.Add(this.dtgAsistentes);
            this.pImportarDocumentos.Controls.Add(this.label8);
            this.pImportarDocumentos.Controls.Add(this.txtInstructor);
            this.pImportarDocumentos.Controls.Add(this.label7);
            this.pImportarDocumentos.Controls.Add(this.label4);
            this.pImportarDocumentos.Controls.Add(this.cbxBase);
            this.pImportarDocumentos.Controls.Add(this.label1);
            this.pImportarDocumentos.Controls.Add(this.dtpFecha);
            this.pImportarDocumentos.Controls.Add(this.txtTituloD);
            this.pImportarDocumentos.Controls.Add(this.txtConductor);
            this.pImportarDocumentos.Controls.Add(this.btnGuardar);
            this.pImportarDocumentos.Controls.Add(this.btnCerrarLocal);
            this.pImportarDocumentos.Controls.Add(this.txtRutaLocal);
            this.pImportarDocumentos.Controls.Add(this.btnCerrar);
            this.pImportarDocumentos.Controls.Add(this.label9);
            this.pImportarDocumentos.Controls.Add(this.metroLabel1);
            this.pImportarDocumentos.Controls.Add(this.txtExtension);
            this.pImportarDocumentos.Controls.Add(this.label5);
            this.pImportarDocumentos.Controls.Add(this.btnBuscarArchivo);
            this.pImportarDocumentos.Controls.Add(this.txtCapacitacion);
            this.pImportarDocumentos.Controls.Add(this.label6);
            this.pImportarDocumentos.Controls.Add(this.lstConductor);
            this.pImportarDocumentos.Location = new System.Drawing.Point(129, 120);
            this.pImportarDocumentos.Name = "pImportarDocumentos";
            this.pImportarDocumentos.Size = new System.Drawing.Size(934, 465);
            this.pImportarDocumentos.TabIndex = 224;
            this.pImportarDocumentos.Visible = false;
            this.pImportarDocumentos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pImportarDocumentos_MouseMove);
            // 
            // cbCircuito
            // 
            this.cbCircuito.AutoSize = true;
            this.cbCircuito.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCircuito.Location = new System.Drawing.Point(709, 113);
            this.cbCircuito.Name = "cbCircuito";
            this.cbCircuito.Size = new System.Drawing.Size(135, 19);
            this.cbCircuito.TabIndex = 260;
            this.cbCircuito.Text = "Circuito Conducción";
            this.cbCircuito.UseVisualStyleBackColor = true;
            this.cbCircuito.CheckedChanged += new System.EventHandler(this.cbCircuito_CheckedChanged);
            // 
            // cbRevision
            // 
            this.cbRevision.AutoSize = true;
            this.cbRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRevision.Location = new System.Drawing.Point(575, 113);
            this.cbRevision.Name = "cbRevision";
            this.cbRevision.Size = new System.Drawing.Size(109, 19);
            this.cbRevision.TabIndex = 259;
            this.cbRevision.Text = "Revisión Visual";
            this.cbRevision.UseVisualStyleBackColor = true;
            this.cbRevision.CheckedChanged += new System.EventHandler(this.cbRevision_CheckedChanged);
            // 
            // chBloque
            // 
            this.chBloque.AutoSize = true;
            this.chBloque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chBloque.Location = new System.Drawing.Point(440, 113);
            this.chBloque.Name = "chBloque";
            this.chBloque.Size = new System.Drawing.Size(109, 19);
            this.chBloque.TabIndex = 258;
            this.chBloque.Text = "Bloque Teórico";
            this.chBloque.UseVisualStyleBackColor = true;
            this.chBloque.CheckedChanged += new System.EventHandler(this.chBloque_CheckedChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAgregar.Location = new System.Drawing.Point(887, 73);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(30, 30);
            this.btnAgregar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnAgregar.TabIndex = 257;
            this.btnAgregar.TabStop = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(655, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 15);
            this.label11.TabIndex = 255;
            this.label11.Text = "Estado:";
            // 
            // cbxEstado
            // 
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            " ",
            "HABILITADO",
            "NO HABILITADO"});
            this.cbxEstado.Location = new System.Drawing.Point(709, 77);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(164, 23);
            this.cbxEstado.TabIndex = 254;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // txtNotaExamen
            // 
            this.txtNotaExamen.BackColor = System.Drawing.Color.Honeydew;
            this.txtNotaExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNotaExamen.Location = new System.Drawing.Point(517, 79);
            this.txtNotaExamen.Name = "txtNotaExamen";
            this.txtNotaExamen.Size = new System.Drawing.Size(78, 21);
            this.txtNotaExamen.TabIndex = 253;
            this.txtNotaExamen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNotaExamen_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(455, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 15);
            this.label10.TabIndex = 252;
            this.label10.Text = "Examen:";
            // 
            // dtgAsistentes
            // 
            this.dtgAsistentes.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgAsistentes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgAsistentes.Location = new System.Drawing.Point(435, 146);
            this.dtgAsistentes.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dtgAsistentes.LookAndFeel.SkinName = "Money Twins";
            this.dtgAsistentes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgAsistentes.MainView = this.dgvAsistentesVista;
            this.dtgAsistentes.Name = "dtgAsistentes";
            this.dtgAsistentes.Size = new System.Drawing.Size(498, 318);
            this.dtgAsistentes.TabIndex = 251;
            this.dtgAsistentes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvAsistentesVista});
            this.dtgAsistentes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgAsistentes_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsQuitarAsistente});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(159, 26);
            // 
            // tsQuitarAsistente
            // 
            this.tsQuitarAsistente.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsQuitarAsistente.Name = "tsQuitarAsistente";
            this.tsQuitarAsistente.Size = new System.Drawing.Size(158, 22);
            this.tsQuitarAsistente.Text = "Quitar Asistente";
            this.tsQuitarAsistente.Click += new System.EventHandler(this.tsQuitarAsistente_Click);
            // 
            // dgvAsistentesVista
            // 
            this.dgvAsistentesVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAsistentesVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvAsistentesVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAsistentesVista.Appearance.Row.Options.UseFont = true;
            this.dgvAsistentesVista.GridControl = this.dtgAsistentes;
            this.dgvAsistentesVista.Name = "dgvAsistentesVista";
            this.dgvAsistentesVista.OptionsBehavior.Editable = false;
            this.dgvAsistentesVista.OptionsView.ColumnAutoWidth = false;
            this.dgvAsistentesVista.OptionsView.RowAutoHeight = true;
            this.dgvAsistentesVista.OptionsView.ShowFooter = true;
            this.dgvAsistentesVista.OptionsView.ShowGroupPanel = false;
            this.dgvAsistentesVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvAsistentesVista_CustomDrawCell);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 15);
            this.label8.TabIndex = 250;
            this.label8.Text = "Adjuntar documento:";
            // 
            // txtInstructor
            // 
            this.txtInstructor.BackColor = System.Drawing.Color.Honeydew;
            this.txtInstructor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtInstructor.Location = new System.Drawing.Point(18, 212);
            this.txtInstructor.Name = "txtInstructor";
            this.txtInstructor.Size = new System.Drawing.Size(393, 21);
            this.txtInstructor.TabIndex = 249;
            this.txtInstructor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInstructor_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 248;
            this.label7.Text = "Instructor:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(203, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 247;
            this.label4.Text = "Base:";
            // 
            // cbxBase
            // 
            this.cbxBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxBase.FormattingEnabled = true;
            this.cbxBase.Items.AddRange(new object[] {
            "TRUJILLO",
            "LIMA",
            "PIURA",
            "AREQUIPA"});
            this.cbxBase.Location = new System.Drawing.Point(247, 146);
            this.cbxBase.Name = "cbxBase";
            this.cbxBase.Size = new System.Drawing.Size(164, 23);
            this.cbxBase.TabIndex = 246;
            this.cbxBase.DropDownClosed += new System.EventHandler(this.cbxBase_DropDownClosed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 245;
            this.label1.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(65, 146);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(100, 21);
            this.dtpFecha.TabIndex = 244;
            this.dtpFecha.Tag = "1";
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFecha_KeyPress);
            // 
            // txtTituloD
            // 
            this.txtTituloD.BackColor = System.Drawing.Color.White;
            this.txtTituloD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTituloD.Location = new System.Drawing.Point(315, 13);
            this.txtTituloD.Name = "txtTituloD";
            this.txtTituloD.Size = new System.Drawing.Size(83, 21);
            this.txtTituloD.TabIndex = 243;
            this.txtTituloD.Visible = false;
            // 
            // txtConductor
            // 
            this.txtConductor.BackColor = System.Drawing.Color.Honeydew;
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductor.Location = new System.Drawing.Point(517, 46);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(356, 20);
            this.txtConductor.TabIndex = 241;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor2_KeyPress);
            this.txtConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtConductor2_KeyUp);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(158, 402);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 38);
            this.btnGuardar.TabIndex = 237;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCerrarLocal
            // 
            this.btnCerrarLocal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarLocal.ForeColor = System.Drawing.Color.Red;
            this.btnCerrarLocal.Location = new System.Drawing.Point(385, 291);
            this.btnCerrarLocal.Name = "btnCerrarLocal";
            this.btnCerrarLocal.Size = new System.Drawing.Size(26, 26);
            this.btnCerrarLocal.TabIndex = 228;
            this.btnCerrarLocal.Text = "X";
            this.btnCerrarLocal.UseVisualStyleBackColor = true;
            this.btnCerrarLocal.Click += new System.EventHandler(this.btnCerrarLocal_Click);
            // 
            // txtRutaLocal
            // 
            this.txtRutaLocal.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtRutaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaLocal.Location = new System.Drawing.Point(82, 285);
            this.txtRutaLocal.Name = "txtRutaLocal";
            this.txtRutaLocal.ReadOnly = true;
            this.txtRutaLocal.Size = new System.Drawing.Size(297, 38);
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
            this.btnCerrar.Location = new System.Drawing.Point(908, -1);
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
            this.label9.ForeColor = System.Drawing.Color.ForestGreen;
            this.label9.Location = new System.Drawing.Point(10, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(271, 24);
            this.label9.TabIndex = 144;
            this.label9.Text = "DATOS DE CAPACITACIÓN";
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
            this.txtExtension.Location = new System.Drawing.Point(404, 13);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(83, 21);
            this.txtExtension.TabIndex = 238;
            this.txtExtension.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(437, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 15);
            this.label5.TabIndex = 232;
            this.label5.Text = "Conductor:";
            // 
            // btnBuscarArchivo
            // 
            this.btnBuscarArchivo.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.btnBuscarArchivo.Appearance.BackColor2 = System.Drawing.Color.PaleGreen;
            this.btnBuscarArchivo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscarArchivo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarArchivo.Appearance.Options.UseBackColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseBorderColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseFont = true;
            this.btnBuscarArchivo.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscarArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarArchivo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscarArchivo.Location = new System.Drawing.Point(18, 285);
            this.btnBuscarArchivo.Name = "btnBuscarArchivo";
            this.btnBuscarArchivo.Size = new System.Drawing.Size(57, 38);
            this.btnBuscarArchivo.TabIndex = 227;
            this.btnBuscarArchivo.Tag = "5";
            this.btnBuscarArchivo.Text = "Buscar\r\nArchivo";
            this.btnBuscarArchivo.ToolTip = "Buscar Archivo";
            this.btnBuscarArchivo.Click += new System.EventHandler(this.btnBuscarArchivo_Click);
            // 
            // txtCapacitacion
            // 
            this.txtCapacitacion.BackColor = System.Drawing.Color.Honeydew;
            this.txtCapacitacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCapacitacion.Location = new System.Drawing.Point(18, 81);
            this.txtCapacitacion.Multiline = true;
            this.txtCapacitacion.Name = "txtCapacitacion";
            this.txtCapacitacion.Size = new System.Drawing.Size(393, 40);
            this.txtCapacitacion.TabIndex = 233;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 15);
            this.label6.TabIndex = 234;
            this.label6.Text = "Capacitación:";
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.Honeydew;
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(517, 65);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(356, 109);
            this.lstConductor.TabIndex = 242;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            this.lstConductor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstConductor_MouseDoubleClick);
            // 
            // frmDocumentosCapacitacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 606);
            this.Controls.Add(this.dtgDocumentos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pImportarDocumentos);
            this.Name = "frmDocumentosCapacitacion";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "REGISTRO DE DOCUMENTOS DE CAPACITACIÓN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDocumentosCapacitacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocumentos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosVista)).EndInit();
            this.pImportarDocumentos.ResumeLayout(false);
            this.pImportarDocumentos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAsistentes)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistentesVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label28;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TextBox txtBCapacitacion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.TextBox txtBConductor;
        private DevExpress.XtraGrid.GridControl dtgDocumentos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDocumentosVista;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarDocumento;
        private System.Windows.Forms.Panel pImportarDocumentos;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCapacitacion;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btnCerrarLocal;
        private DevExpress.XtraEditors.SimpleButton btnBuscarArchivo;
        public System.Windows.Forms.RichTextBox txtRutaLocal;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.ListView lstConductor;
        private DevExpress.XtraEditors.SimpleButton btnNuevaCapacitacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtTituloD;
        private System.Windows.Forms.TextBox txtInstructor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxBase;
        private DevExpress.XtraGrid.GridControl dtgAsistentes;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvAsistentesVista;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNotaExamen;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsQuitarAsistente;
        private System.Windows.Forms.PictureBox btnAgregar;
        private System.Windows.Forms.CheckBox cbCircuito;
        private System.Windows.Forms.CheckBox cbRevision;
        private System.Windows.Forms.CheckBox chBloque;

    }
}