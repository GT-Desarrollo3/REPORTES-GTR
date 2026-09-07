namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmDocumentosSIG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentosSIG));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxProceso2 = new System.Windows.Forms.ComboBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubirDocumento = new DevExpress.XtraEditors.SimpleButton();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtgDocumentosSIG = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsNuevaVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEliminarDocumento = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDocumentosSIGVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pImportarDocumentos = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxProceso = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVersion2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTitulo2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxArea2 = new System.Windows.Forms.ComboBox();
            this.btnCerrarLocal = new System.Windows.Forms.Button();
            this.btnBuscarArchivo = new DevExpress.XtraEditors.SimpleButton();
            this.txtRutaLocal = new System.Windows.Forms.RichTextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocumentosSIG)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosSIGVista)).BeginInit();
            this.pImportarDocumentos.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbxProceso2);
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxArea);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnSubirDocumento);
            this.groupBox1.Controls.Add(this.txtTitulo);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1037, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Búsqueda";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(398, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 233;
            this.label8.Text = "Proceso:";
            // 
            // cbxProceso2
            // 
            this.cbxProceso2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxProceso2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProceso2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProceso2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProceso2.FormattingEnabled = true;
            this.cbxProceso2.Location = new System.Drawing.Point(459, 66);
            this.cbxProceso2.Name = "cbxProceso2";
            this.cbxProceso2.Size = new System.Drawing.Size(174, 23);
            this.cbxProceso2.TabIndex = 232;
            this.cbxProceso2.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion2_SelectedIndexChanged);
            this.cbxProceso2.DropDownClosed += new System.EventHandler(this.cbxProceso2_DropDownClosed);
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
            this.btnExcel.Location = new System.Drawing.Point(713, 39);
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
            this.label3.Location = new System.Drawing.Point(174, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 15);
            this.label3.TabIndex = 230;
            this.label3.Text = "Área:";
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(215, 66);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(167, 23);
            this.cbxArea.TabIndex = 113;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            this.cbxArea.DropDownClosed += new System.EventHandler(this.cbxArea_DropDownClosed);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(169, 34);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(40, 15);
            this.label28.TabIndex = 227;
            this.label28.Text = "Título:";
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
            this.btnBuscar.Location = new System.Drawing.Point(657, 39);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 224;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnSubirDocumento
            // 
            this.btnSubirDocumento.Appearance.BackColor = System.Drawing.Color.White;
            this.btnSubirDocumento.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnSubirDocumento.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnSubirDocumento.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirDocumento.Appearance.Options.UseBackColor = true;
            this.btnSubirDocumento.Appearance.Options.UseBorderColor = true;
            this.btnSubirDocumento.Appearance.Options.UseFont = true;
            this.btnSubirDocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubirDocumento.Image = ((System.Drawing.Image)(resources.GetObject("btnSubirDocumento.Image")));
            this.btnSubirDocumento.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSubirDocumento.Location = new System.Drawing.Point(22, 39);
            this.btnSubirDocumento.Name = "btnSubirDocumento";
            this.btnSubirDocumento.Size = new System.Drawing.Size(119, 47);
            this.btnSubirDocumento.TabIndex = 223;
            this.btnSubirDocumento.Text = "Subir\r\nDocumento";
            this.btnSubirDocumento.ToolTip = "Subir Documento";
            this.btnSubirDocumento.Click += new System.EventHandler(this.btnSubirDocumento_Click);
            // 
            // txtTitulo
            // 
            this.txtTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTitulo.Location = new System.Drawing.Point(215, 31);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(418, 21);
            this.txtTitulo.TabIndex = 112;
            this.txtTitulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTitulo_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFechaIni);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(166, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 61);
            this.groupBox2.TabIndex = 234;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fecha subida: ";
            this.groupBox2.Visible = false;
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
            // dtgDocumentosSIG
            // 
            this.dtgDocumentosSIG.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgDocumentosSIG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgDocumentosSIG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDocumentosSIG.Location = new System.Drawing.Point(20, 191);
            this.dtgDocumentosSIG.LookAndFeel.SkinMaskColor = System.Drawing.Color.DarkOrange;
            this.dtgDocumentosSIG.LookAndFeel.SkinName = "Money Twins";
            this.dtgDocumentosSIG.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDocumentosSIG.MainView = this.dgvDocumentosSIGVista;
            this.dtgDocumentosSIG.Name = "dtgDocumentosSIG";
            this.dtgDocumentosSIG.Size = new System.Drawing.Size(1037, 395);
            this.dtgDocumentosSIG.TabIndex = 185;
            this.dtgDocumentosSIG.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvDocumentosSIGVista});
            this.dtgDocumentosSIG.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgDocumentosSIG_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNuevaVersion,
            this.tsEliminarDocumento});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 48);
            // 
            // tsNuevaVersion
            // 
            this.tsNuevaVersion.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.tsNuevaVersion.Name = "tsNuevaVersion";
            this.tsNuevaVersion.Size = new System.Drawing.Size(183, 22);
            this.tsNuevaVersion.Text = "Actualizar Versión";
            this.tsNuevaVersion.Click += new System.EventHandler(this.tsNuevaVersion_Click);
            // 
            // tsEliminarDocumento
            // 
            this.tsEliminarDocumento.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsEliminarDocumento.Name = "tsEliminarDocumento";
            this.tsEliminarDocumento.Size = new System.Drawing.Size(183, 22);
            this.tsEliminarDocumento.Text = "Eliminar Documento";
            this.tsEliminarDocumento.Click += new System.EventHandler(this.tsEliminarDocumento_Click);
            // 
            // dgvDocumentosSIGVista
            // 
            this.dgvDocumentosSIGVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDocumentosSIGVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvDocumentosSIGVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDocumentosSIGVista.Appearance.Row.Options.UseFont = true;
            this.dgvDocumentosSIGVista.GridControl = this.dtgDocumentosSIG;
            this.dgvDocumentosSIGVista.Name = "dgvDocumentosSIGVista";
            this.dgvDocumentosSIGVista.OptionsBehavior.Editable = false;
            this.dgvDocumentosSIGVista.OptionsView.ColumnAutoWidth = false;
            this.dgvDocumentosSIGVista.OptionsView.RowAutoHeight = true;
            this.dgvDocumentosSIGVista.OptionsView.ShowFooter = true;
            this.dgvDocumentosSIGVista.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvDocumentosSIGVista_RowCellClick);
            this.dgvDocumentosSIGVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvDocumentosSIGVista_CustomDrawCell);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1037, 20);
            this.panel2.TabIndex = 186;
            // 
            // pImportarDocumentos
            // 
            this.pImportarDocumentos.BackColor = System.Drawing.Color.White;
            this.pImportarDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pImportarDocumentos.Controls.Add(this.label4);
            this.pImportarDocumentos.Controls.Add(this.cbxProceso);
            this.pImportarDocumentos.Controls.Add(this.btnGuardar);
            this.pImportarDocumentos.Controls.Add(this.label7);
            this.pImportarDocumentos.Controls.Add(this.txtVersion2);
            this.pImportarDocumentos.Controls.Add(this.label6);
            this.pImportarDocumentos.Controls.Add(this.txtTitulo2);
            this.pImportarDocumentos.Controls.Add(this.label5);
            this.pImportarDocumentos.Controls.Add(this.cbxArea2);
            this.pImportarDocumentos.Controls.Add(this.btnCerrarLocal);
            this.pImportarDocumentos.Controls.Add(this.btnBuscarArchivo);
            this.pImportarDocumentos.Controls.Add(this.txtRutaLocal);
            this.pImportarDocumentos.Controls.Add(this.btnCerrar);
            this.pImportarDocumentos.Controls.Add(this.label9);
            this.pImportarDocumentos.Controls.Add(this.metroLabel1);
            this.pImportarDocumentos.Controls.Add(this.txtExtension);
            this.pImportarDocumentos.Location = new System.Drawing.Point(181, 263);
            this.pImportarDocumentos.Name = "pImportarDocumentos";
            this.pImportarDocumentos.Size = new System.Drawing.Size(563, 280);
            this.pImportarDocumentos.TabIndex = 223;
            this.pImportarDocumentos.Visible = false;
            this.pImportarDocumentos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pImportarDocumentos_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(272, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 240;
            this.label4.Text = "Proceso:";
            // 
            // cbxProceso
            // 
            this.cbxProceso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxProceso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProceso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProceso.FormattingEnabled = true;
            this.cbxProceso.Location = new System.Drawing.Point(333, 182);
            this.cbxProceso.Name = "cbxProceso";
            this.cbxProceso.Size = new System.Drawing.Size(184, 23);
            this.cbxProceso.TabIndex = 239;
            this.cbxProceso.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.Peru;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(235, 226);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 38);
            this.btnGuardar.TabIndex = 237;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(376, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 236;
            this.label7.Text = "Versión:";
            // 
            // txtVersion2
            // 
            this.txtVersion2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtVersion2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtVersion2.Location = new System.Drawing.Point(433, 91);
            this.txtVersion2.Name = "txtVersion2";
            this.txtVersion2.Size = new System.Drawing.Size(84, 21);
            this.txtVersion2.TabIndex = 235;
            this.txtVersion2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVersion2_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 234;
            this.label6.Text = "Título:";
            // 
            // txtTitulo2
            // 
            this.txtTitulo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTitulo2.Location = new System.Drawing.Point(73, 127);
            this.txtTitulo2.Multiline = true;
            this.txtTitulo2.Name = "txtTitulo2";
            this.txtTitulo2.Size = new System.Drawing.Size(444, 40);
            this.txtTitulo2.TabIndex = 233;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 15);
            this.label5.TabIndex = 232;
            this.label5.Text = "Área:";
            // 
            // cbxArea2
            // 
            this.cbxArea2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxArea2.FormattingEnabled = true;
            this.cbxArea2.Location = new System.Drawing.Point(73, 182);
            this.cbxArea2.Name = "cbxArea2";
            this.cbxArea2.Size = new System.Drawing.Size(167, 23);
            this.cbxArea2.TabIndex = 231;
            this.cbxArea2.SelectedIndexChanged += new System.EventHandler(this.cbxArea2_SelectedIndexChanged);
            // 
            // btnCerrarLocal
            // 
            this.btnCerrarLocal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarLocal.ForeColor = System.Drawing.Color.Red;
            this.btnCerrarLocal.Location = new System.Drawing.Point(523, 56);
            this.btnCerrarLocal.Name = "btnCerrarLocal";
            this.btnCerrarLocal.Size = new System.Drawing.Size(26, 26);
            this.btnCerrarLocal.TabIndex = 228;
            this.btnCerrarLocal.Text = "X";
            this.btnCerrarLocal.UseVisualStyleBackColor = true;
            this.btnCerrarLocal.Click += new System.EventHandler(this.btnCerrarLocal_Click);
            // 
            // btnBuscarArchivo
            // 
            this.btnBuscarArchivo.Appearance.BackColor = System.Drawing.Color.SeaShell;
            this.btnBuscarArchivo.Appearance.BackColor2 = System.Drawing.Color.Peru;
            this.btnBuscarArchivo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscarArchivo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarArchivo.Appearance.Options.UseBackColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseBorderColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseFont = true;
            this.btnBuscarArchivo.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscarArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarArchivo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarArchivo.Image")));
            this.btnBuscarArchivo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscarArchivo.Location = new System.Drawing.Point(18, 50);
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
            this.txtRutaLocal.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtRutaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaLocal.Location = new System.Drawing.Point(123, 50);
            this.txtRutaLocal.Name = "txtRutaLocal";
            this.txtRutaLocal.ReadOnly = true;
            this.txtRutaLocal.Size = new System.Drawing.Size(394, 38);
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
            this.btnCerrar.Location = new System.Drawing.Point(537, -1);
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
            this.label9.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label9.Location = new System.Drawing.Point(10, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(247, 24);
            this.label9.TabIndex = 144;
            this.label9.Text = "SUBIR DOCUMENTO SIG";
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
            this.txtExtension.Location = new System.Drawing.Point(434, 23);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(83, 21);
            this.txtExtension.TabIndex = 238;
            this.txtExtension.Visible = false;
            // 
            // frmDocumentosSIG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 606);
            this.Controls.Add(this.dtgDocumentosSIG);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pImportarDocumentos);
            this.Name = "frmDocumentosSIG";
            this.Style = MetroFramework.MetroColorStyle.Brown;
            this.Text = "GESTIÓN DE DOCUMENTOS DEL SIG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDocumentosSIG_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocumentosSIG)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosSIGVista)).EndInit();
            this.pImportarDocumentos.ResumeLayout(false);
            this.pImportarDocumentos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private DevExpress.XtraEditors.SimpleButton btnSubirDocumento;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label28;
        private DevExpress.XtraGrid.GridControl dtgDocumentosSIG;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDocumentosSIGVista;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsNuevaVersion;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarDocumento;
        private System.Windows.Forms.Panel pImportarDocumentos;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        public System.Windows.Forms.RichTextBox txtRutaLocal;
        private DevExpress.XtraEditors.SimpleButton btnBuscarArchivo;
        public System.Windows.Forms.Button btnCerrarLocal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxArea2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVersion2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTitulo2;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.TextBox txtExtension;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxProceso;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxProceso2;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}