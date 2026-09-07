namespace ReportesTranspesa.Formularios.Areas.Finanzas.ControlCotizacion
{
    partial class frmControlCotizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControlCotizacion));
            this.dtgCotizacion = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsCompletarCoti = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCotizar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsHistorialCrediticio = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCONFORME = new System.Windows.Forms.ToolStripMenuItem();
            this.tsOBSERVADO = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEliminarCoti = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvCotizacionVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.cbxFlota = new System.Windows.Forms.ComboBox();
            this.gbLeyenda = new System.Windows.Forms.GroupBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.label47 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.btnNuevaCoti = new DevExpress.XtraEditors.SimpleButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.tsModificarSolicitud = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCotizacion)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizacionVista)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbLeyenda.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgCotizacion
            // 
            this.dtgCotizacion.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgCotizacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgCotizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgCotizacion.Location = new System.Drawing.Point(20, 175);
            this.dtgCotizacion.LookAndFeel.SkinMaskColor = System.Drawing.Color.Orange;
            this.dtgCotizacion.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Cyan;
            this.dtgCotizacion.LookAndFeel.SkinName = "McSkin";
            this.dtgCotizacion.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgCotizacion.MainView = this.dgvCotizacionVista;
            this.dtgCotizacion.Name = "dtgCotizacion";
            this.dtgCotizacion.Size = new System.Drawing.Size(960, 305);
            this.dtgCotizacion.TabIndex = 239;
            this.dtgCotizacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvCotizacionVista});
            this.dtgCotizacion.DoubleClick += new System.EventHandler(this.dtgCotizacion_DoubleClick);
            this.dtgCotizacion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgCotizacion_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsModificarSolicitud,
            this.tsCompletarCoti,
            this.tsCotizar,
            this.tsHistorialCrediticio,
            this.tsEliminarCoti});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 136);
            // 
            // tsCompletarCoti
            // 
            this.tsCompletarCoti.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.tsCompletarCoti.Name = "tsCompletarCoti";
            this.tsCompletarCoti.Size = new System.Drawing.Size(189, 22);
            this.tsCompletarCoti.Text = "Completar Cotización";
            this.tsCompletarCoti.Click += new System.EventHandler(this.tsEditarCoti_Click);
            // 
            // tsCotizar
            // 
            this.tsCotizar.Image = global::ReportesTranspesa.Properties.Resources.generarbono;
            this.tsCotizar.Name = "tsCotizar";
            this.tsCotizar.Size = new System.Drawing.Size(189, 22);
            this.tsCotizar.Text = "Cotizar";
            this.tsCotizar.Click += new System.EventHandler(this.tsCotizar_Click);
            // 
            // tsHistorialCrediticio
            // 
            this.tsHistorialCrediticio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCONFORME,
            this.tsOBSERVADO});
            this.tsHistorialCrediticio.Image = global::ReportesTranspesa.Properties.Resources.ordenar;
            this.tsHistorialCrediticio.Name = "tsHistorialCrediticio";
            this.tsHistorialCrediticio.Size = new System.Drawing.Size(189, 22);
            this.tsHistorialCrediticio.Text = "Historial Crediticio";
            // 
            // tsCONFORME
            // 
            this.tsCONFORME.Name = "tsCONFORME";
            this.tsCONFORME.Size = new System.Drawing.Size(140, 22);
            this.tsCONFORME.Text = "CONFORME";
            this.tsCONFORME.Click += new System.EventHandler(this.tsCONFORME_Click);
            // 
            // tsOBSERVADO
            // 
            this.tsOBSERVADO.Name = "tsOBSERVADO";
            this.tsOBSERVADO.Size = new System.Drawing.Size(140, 22);
            this.tsOBSERVADO.Text = "OBSERVADO";
            this.tsOBSERVADO.Click += new System.EventHandler(this.tsOBSERVADO_Click);
            // 
            // tsEliminarCoti
            // 
            this.tsEliminarCoti.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarCoti.Name = "tsEliminarCoti";
            this.tsEliminarCoti.Size = new System.Drawing.Size(189, 22);
            this.tsEliminarCoti.Text = "Eliminar Cotización";
            this.tsEliminarCoti.Click += new System.EventHandler(this.tsEliminarCoti_Click);
            // 
            // dgvCotizacionVista
            // 
            this.dgvCotizacionVista.GridControl = this.dtgCotizacion;
            this.dgvCotizacionVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvCotizacionVista.Name = "dgvCotizacionVista";
            this.dgvCotizacionVista.OptionsBehavior.Editable = false;
            this.dgvCotizacionVista.OptionsBehavior.ReadOnly = true;
            this.dgvCotizacionVista.OptionsView.ColumnAutoWidth = false;
            this.dgvCotizacionVista.OptionsView.RowAutoHeight = true;
            this.dgvCotizacionVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvCotizacionVista_CustomDrawCell);
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
            this.btnExcel.Location = new System.Drawing.Point(730, 39);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(50, 47);
            this.btnExcel.TabIndex = 225;
            this.btnExcel.Tag = "6";
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
            this.btnBuscar.Location = new System.Drawing.Point(672, 39);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxEstado);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.cbxFlota);
            this.groupBox1.Controls.Add(this.gbLeyenda);
            this.groupBox1.Controls.Add(this.metroLabel4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.dtpFechaIni);
            this.groupBox1.Controls.Add(this.btnNuevaCoti);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(960, 115);
            this.groupBox1.TabIndex = 237;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTRO DE BÚSQUEDA: ";
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
            "SOLICITADO",
            "REGISTRADO",
            "COTIZADO"});
            this.cbxEstado.Location = new System.Drawing.Point(528, 65);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(110, 23);
            this.cbxEstado.TabIndex = 228;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.White;
            this.metroLabel1.Location = new System.Drawing.Point(483, 36);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(41, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 95;
            this.metroLabel1.Text = "Flota:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.White;
            this.metroLabel3.Location = new System.Drawing.Point(476, 67);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(51, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 95;
            this.metroLabel3.Text = "Estado:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // cbxFlota
            // 
            this.cbxFlota.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxFlota.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxFlota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFlota.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFlota.FormattingEnabled = true;
            this.cbxFlota.Items.AddRange(new object[] {
            "TODOS",
            "SÍ",
            "NO"});
            this.cbxFlota.Location = new System.Drawing.Point(528, 34);
            this.cbxFlota.Name = "cbxFlota";
            this.cbxFlota.Size = new System.Drawing.Size(110, 23);
            this.cbxFlota.TabIndex = 228;
            this.cbxFlota.DropDownClosed += new System.EventHandler(this.cbxFlota_DropDownClosed);
            // 
            // gbLeyenda
            // 
            this.gbLeyenda.Controls.Add(this.metroLabel5);
            this.gbLeyenda.Controls.Add(this.label47);
            this.gbLeyenda.Controls.Add(this.label50);
            this.gbLeyenda.Controls.Add(this.label51);
            this.gbLeyenda.Controls.Add(this.label52);
            this.gbLeyenda.Controls.Add(this.label53);
            this.gbLeyenda.Controls.Add(this.label57);
            this.gbLeyenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLeyenda.Location = new System.Drawing.Point(818, 16);
            this.gbLeyenda.Name = "gbLeyenda";
            this.gbLeyenda.Size = new System.Drawing.Size(111, 87);
            this.gbLeyenda.TabIndex = 231;
            this.gbLeyenda.TabStop = false;
            this.gbLeyenda.Text = "Leyenda";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.BackColor = System.Drawing.Color.White;
            this.metroLabel5.Location = new System.Drawing.Point(9, -5);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(64, 19);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel5.TabIndex = 96;
            this.metroLabel5.Text = "Leyenda: ";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(46, 63);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(48, 13);
            this.label47.TabIndex = 5;
            this.label47.Text = "Cotizado";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(46, 42);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(58, 13);
            this.label50.TabIndex = 3;
            this.label50.Text = "Registrado";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Yellow;
            this.label51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label51.Location = new System.Drawing.Point(10, 40);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(30, 15);
            this.label51.TabIndex = 2;
            this.label51.Text = "       ";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(46, 21);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(53, 13);
            this.label52.TabIndex = 1;
            this.label52.Text = "Solicitado";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.Color.Aqua;
            this.label53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label53.Location = new System.Drawing.Point(10, 19);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(30, 15);
            this.label53.TabIndex = 0;
            this.label53.Text = "       ";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Lime;
            this.label57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label57.Location = new System.Drawing.Point(10, 61);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(30, 15);
            this.label57.TabIndex = 8;
            this.label57.Text = "       ";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.White;
            this.metroLabel4.Location = new System.Drawing.Point(173, 68);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(52, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel4.TabIndex = 229;
            this.metroLabel4.Text = "Cliente:";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(334, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 16);
            this.label5.TabIndex = 116;
            this.label5.Text = "--";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(354, 35);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaFin.TabIndex = 93;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(229, 35);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaIni.TabIndex = 92;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // btnNuevaCoti
            // 
            this.btnNuevaCoti.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevaCoti.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevaCoti.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevaCoti.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaCoti.Appearance.Options.UseBackColor = true;
            this.btnNuevaCoti.Appearance.Options.UseBorderColor = true;
            this.btnNuevaCoti.Appearance.Options.UseFont = true;
            this.btnNuevaCoti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaCoti.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaCoti.Image")));
            this.btnNuevaCoti.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevaCoti.Location = new System.Drawing.Point(23, 39);
            this.btnNuevaCoti.Name = "btnNuevaCoti";
            this.btnNuevaCoti.Size = new System.Drawing.Size(110, 47);
            this.btnNuevaCoti.TabIndex = 227;
            this.btnNuevaCoti.Tag = "5";
            this.btnNuevaCoti.Text = "Registrar\r\nCotización";
            this.btnNuevaCoti.ToolTip = "Registrar Cotización";
            this.btnNuevaCoti.Click += new System.EventHandler(this.btnNuevaCoti_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.White;
            this.metroLabel2.Location = new System.Drawing.Point(153, 36);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(72, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 95;
            this.metroLabel2.Text = "F. Registro:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtCliente
            // 
            this.txtCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.Location = new System.Drawing.Point(229, 67);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(228, 22);
            this.txtCliente.TabIndex = 230;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // tsModificarSolicitud
            // 
            this.tsModificarSolicitud.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.tsModificarSolicitud.Name = "tsModificarSolicitud";
            this.tsModificarSolicitud.Size = new System.Drawing.Size(189, 22);
            this.tsModificarSolicitud.Text = "Modificar Solicitud";
            this.tsModificarSolicitud.Click += new System.EventHandler(this.tsModificarSolicitud_Click);
            // 
            // frmControlCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.dtgCotizacion);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmControlCotizacion";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "REPORTE DE CONTROL DE COTIZACIONES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmControlCotizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCotizacion)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCotizacionVista)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbLeyenda.ResumeLayout(false);
            this.gbLeyenda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgCotizacion;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvCotizacionVista;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnNuevaCoti;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsHistorialCrediticio;
        private System.Windows.Forms.ToolStripMenuItem tsCONFORME;
        private System.Windows.Forms.ToolStripMenuItem tsOBSERVADO;
        private System.Windows.Forms.ToolStripMenuItem tsCompletarCoti;
        public System.Windows.Forms.ToolStripMenuItem tsEliminarCoti;
        public System.Windows.Forms.ComboBox cbxEstado;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        public System.Windows.Forms.ComboBox cbxFlota;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        public System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.GroupBox gbLeyenda;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private System.Windows.Forms.ToolStripMenuItem tsCotizar;
        private System.Windows.Forms.ToolStripMenuItem tsModificarSolicitud;
    }
}