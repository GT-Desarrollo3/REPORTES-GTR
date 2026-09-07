namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmControlMttoEquipos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControlMttoEquipos));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.cbxBuscar = new System.Windows.Forms.ComboBox();
            this.cbHistorial = new System.Windows.Forms.CheckBox();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNuevo = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIntervalo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaCambio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAccesorio = new System.Windows.Forms.ComboBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgControlMtto = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarAccesorio = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvControlMttoVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgHistorial = new DevExpress.XtraGrid.GridControl();
            this.dgvHistorialVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pNuevo = new System.Windows.Forms.Panel();
            this.btnCerrar3 = new System.Windows.Forms.Button();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.txtAccesorio = new System.Windows.Forms.TextBox();
            this.btnNuevoAccesorio = new DevExpress.XtraEditors.SimpleButton();
            this.label59 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarHistorial = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNuevo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgControlMtto)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvControlMttoVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialVista)).BeginInit();
            this.pNuevo.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(700, 43);
            this.label1.TabIndex = 28;
            this.label1.Text = "C";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.btnImprimir);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.cbxBuscar);
            this.panel4.Controls.Add(this.cbHistorial);
            this.panel4.Controls.Add(this.btnCancelar);
            this.panel4.Controls.Add(this.btnAgregar);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.lblModelo);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.lblMarca);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.lblPlaca);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(700, 229);
            this.panel4.TabIndex = 29;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.BackColor = System.Drawing.Color.White;
            this.btnImprimir.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnImprimir.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseBackColor = true;
            this.btnImprimir.Appearance.Options.UseBorderColor = true;
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnImprimir.Location = new System.Drawing.Point(697, 140);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(51, 47);
            this.btnImprimir.TabIndex = 220;
            this.btnImprimir.Tag = "6";
            this.btnImprimir.ToolTip = "Exportar a Excel";
            this.btnImprimir.Visible = false;
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
            this.btnExcel.Location = new System.Drawing.Point(632, 140);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 219;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // cbxBuscar
            // 
            this.cbxBuscar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxBuscar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBuscar.FormattingEnabled = true;
            this.cbxBuscar.Location = new System.Drawing.Point(158, 79);
            this.cbxBuscar.Name = "cbxBuscar";
            this.cbxBuscar.Size = new System.Drawing.Size(290, 21);
            this.cbxBuscar.TabIndex = 217;
            this.cbxBuscar.SelectedIndexChanged += new System.EventHandler(this.cbxBuscar_SelectedIndexChanged);
            this.cbxBuscar.DropDownClosed += new System.EventHandler(this.cbxBuscar_DropDownClosed);
            // 
            // cbHistorial
            // 
            this.cbHistorial.AutoSize = true;
            this.cbHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cbHistorial.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbHistorial.Location = new System.Drawing.Point(20, 81);
            this.cbHistorial.Name = "cbHistorial";
            this.cbHistorial.Size = new System.Drawing.Size(136, 17);
            this.cbHistorial.TabIndex = 214;
            this.cbHistorial.Text = "Mostrar Historial de";
            this.cbHistorial.UseVisualStyleBackColor = true;
            this.cbHistorial.CheckedChanged += new System.EventHandler(this.cbHistorial_CheckedChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(518, 124);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(93, 35);
            this.btnCancelar.TabIndex = 213;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(518, 168);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(93, 35);
            this.btnAgregar.TabIndex = 212;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNuevo);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtIntervalo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dtpFechaCambio);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbxAccesorio);
            this.groupBox2.Location = new System.Drawing.Point(20, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 96);
            this.groupBox2.TabIndex = 211;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos del Proceso:";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnNuevo.Location = new System.Drawing.Point(437, 25);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 24);
            this.btnNuevo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnNuevo.TabIndex = 223;
            this.btnNuevo.TabStop = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(229, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 222;
            this.label9.Text = "Periodo:";
            // 
            // txtIntervalo
            // 
            this.txtIntervalo.Location = new System.Drawing.Point(281, 58);
            this.txtIntervalo.Name = "txtIntervalo";
            this.txtIntervalo.Size = new System.Drawing.Size(111, 20);
            this.txtIntervalo.TabIndex = 221;
            this.txtIntervalo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIntervalo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 219;
            this.label5.Text = "Fecha Cambio:";
            // 
            // dtpFechaCambio
            // 
            this.dtpFechaCambio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaCambio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCambio.Location = new System.Drawing.Point(98, 58);
            this.dtpFechaCambio.Name = "dtpFechaCambio";
            this.dtpFechaCambio.Size = new System.Drawing.Size(103, 20);
            this.dtpFechaCambio.TabIndex = 218;
            this.dtpFechaCambio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaCambio_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 217;
            this.label2.Text = "Accesorio:";
            // 
            // cbxAccesorio
            // 
            this.cbxAccesorio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxAccesorio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAccesorio.FormattingEnabled = true;
            this.cbxAccesorio.Location = new System.Drawing.Point(98, 27);
            this.cbxAccesorio.Name = "cbxAccesorio";
            this.cbxAccesorio.Size = new System.Drawing.Size(330, 21);
            this.cbxAccesorio.TabIndex = 216;
            this.cbxAccesorio.SelectedIndexChanged += new System.EventHandler(this.cbxAccesorio_SelectedIndexChanged);
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblModelo.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblModelo.Location = new System.Drawing.Point(351, 41);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(21, 23);
            this.lblModelo.TabIndex = 111;
            this.lblModelo.Text = "F";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(269, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 19);
            this.label8.TabIndex = 110;
            this.label8.Text = "MODELO:";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblMarca.Location = new System.Drawing.Point(88, 41);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(21, 23);
            this.lblMarca.TabIndex = 109;
            this.lblMarca.Text = "F";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(17, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 19);
            this.label6.TabIndex = 108;
            this.label6.Text = "MARCA:";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.lblPlaca.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblPlaca.Location = new System.Drawing.Point(84, 13);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(22, 23);
            this.lblPlaca.TabIndex = 105;
            this.lblPlaca.Text = "P";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(17, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 19);
            this.label3.TabIndex = 104;
            this.label3.Text = "PLACA:";
            // 
            // dtgControlMtto
            // 
            this.dtgControlMtto.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgControlMtto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgControlMtto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgControlMtto.Location = new System.Drawing.Point(0, 272);
            this.dtgControlMtto.MainView = this.dgvControlMttoVista;
            this.dtgControlMtto.Name = "dtgControlMtto";
            this.dtgControlMtto.Size = new System.Drawing.Size(700, 242);
            this.dtgControlMtto.TabIndex = 31;
            this.dtgControlMtto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvControlMttoVista});
            this.dtgControlMtto.DoubleClick += new System.EventHandler(this.dtgControlMtto_DoubleClick);
            this.dtgControlMtto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgControlMtto_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarAccesorio});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // tsEliminarAccesorio
            // 
            this.tsEliminarAccesorio.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarAccesorio.Name = "tsEliminarAccesorio";
            this.tsEliminarAccesorio.Size = new System.Drawing.Size(117, 22);
            this.tsEliminarAccesorio.Text = "Eliminar";
            this.tsEliminarAccesorio.Click += new System.EventHandler(this.tsEliminarAccesorio_Click);
            // 
            // dgvControlMttoVista
            // 
            this.dgvControlMttoVista.GridControl = this.dtgControlMtto;
            this.dgvControlMttoVista.Name = "dgvControlMttoVista";
            this.dgvControlMttoVista.OptionsBehavior.Editable = false;
            this.dgvControlMttoVista.OptionsView.ColumnAutoWidth = false;
            this.dgvControlMttoVista.OptionsView.RowAutoHeight = true;
            this.dgvControlMttoVista.OptionsView.ShowGroupPanel = false;
            this.dgvControlMttoVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvControlMttoVista_CustomDrawCell);
            // 
            // dtgHistorial
            // 
            this.dtgHistorial.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgHistorial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgHistorial.Location = new System.Drawing.Point(0, 272);
            this.dtgHistorial.MainView = this.dgvHistorialVista;
            this.dtgHistorial.Name = "dtgHistorial";
            this.dtgHistorial.Size = new System.Drawing.Size(700, 242);
            this.dtgHistorial.TabIndex = 32;
            this.dtgHistorial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvHistorialVista});
            this.dtgHistorial.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgHistorial_MouseUp);
            // 
            // dgvHistorialVista
            // 
            this.dgvHistorialVista.GridControl = this.dtgHistorial;
            this.dgvHistorialVista.Name = "dgvHistorialVista";
            this.dgvHistorialVista.OptionsBehavior.Editable = false;
            this.dgvHistorialVista.OptionsView.ColumnAutoWidth = false;
            this.dgvHistorialVista.OptionsView.RowAutoHeight = true;
            this.dgvHistorialVista.OptionsView.ShowGroupPanel = false;
            // 
            // pNuevo
            // 
            this.pNuevo.BackColor = System.Drawing.Color.LemonChiffon;
            this.pNuevo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNuevo.Controls.Add(this.btnCerrar3);
            this.pNuevo.Controls.Add(this.groupBox18);
            this.pNuevo.Controls.Add(this.label59);
            this.pNuevo.Location = new System.Drawing.Point(188, 207);
            this.pNuevo.Name = "pNuevo";
            this.pNuevo.Size = new System.Drawing.Size(381, 129);
            this.pNuevo.TabIndex = 221;
            this.pNuevo.Visible = false;
            this.pNuevo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pNuevo_MouseMove);
            // 
            // btnCerrar3
            // 
            this.btnCerrar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar3.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar3.Location = new System.Drawing.Point(354, -2);
            this.btnCerrar3.Name = "btnCerrar3";
            this.btnCerrar3.Size = new System.Drawing.Size(27, 27);
            this.btnCerrar3.TabIndex = 53;
            this.btnCerrar3.Text = "X";
            this.btnCerrar3.UseVisualStyleBackColor = false;
            this.btnCerrar3.Click += new System.EventHandler(this.btnCerrar3_Click);
            // 
            // groupBox18
            // 
            this.groupBox18.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox18.Controls.Add(this.txtAccesorio);
            this.groupBox18.Controls.Add(this.btnNuevoAccesorio);
            this.groupBox18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBox18.Location = new System.Drawing.Point(15, 37);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(347, 73);
            this.groupBox18.TabIndex = 52;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Ingresar Nuevo Tipo Mantenimiento:";
            // 
            // txtAccesorio
            // 
            this.txtAccesorio.Location = new System.Drawing.Point(12, 24);
            this.txtAccesorio.Multiline = true;
            this.txtAccesorio.Name = "txtAccesorio";
            this.txtAccesorio.Size = new System.Drawing.Size(258, 35);
            this.txtAccesorio.TabIndex = 53;
            this.txtAccesorio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccesorio_KeyPress);
            // 
            // btnNuevoAccesorio
            // 
            this.btnNuevoAccesorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevoAccesorio.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoAccesorio.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoAccesorio.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoAccesorio.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoAccesorio.Appearance.Options.UseBackColor = true;
            this.btnNuevoAccesorio.Appearance.Options.UseBorderColor = true;
            this.btnNuevoAccesorio.Appearance.Options.UseFont = true;
            this.btnNuevoAccesorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoAccesorio.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoAccesorio.Image")));
            this.btnNuevoAccesorio.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNuevoAccesorio.Location = new System.Drawing.Point(292, 24);
            this.btnNuevoAccesorio.Name = "btnNuevoAccesorio";
            this.btnNuevoAccesorio.Size = new System.Drawing.Size(42, 35);
            this.btnNuevoAccesorio.TabIndex = 213;
            this.btnNuevoAccesorio.ToolTip = "Guardar";
            this.btnNuevoAccesorio.Click += new System.EventHandler(this.btnNuevoAccesorio_Click);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label59.ForeColor = System.Drawing.Color.ForestGreen;
            this.label59.Location = new System.Drawing.Point(11, 9);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(294, 20);
            this.label59.TabIndex = 49;
            this.label59.Text = "ACCESORIO DE MANTENIMIENTO";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarHistorial});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(181, 26);
            // 
            // tsEliminarHistorial
            // 
            this.tsEliminarHistorial.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarHistorial.Name = "tsEliminarHistorial";
            this.tsEliminarHistorial.Size = new System.Drawing.Size(180, 22);
            this.tsEliminarHistorial.Text = "Eliminar de Historial";
            this.tsEliminarHistorial.Click += new System.EventHandler(this.tsEliminarHistorial_Click);
            // 
            // frmControlMttoEquipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 514);
            this.Controls.Add(this.dtgHistorial);
            this.Controls.Add(this.dtgControlMtto);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pNuevo);
            this.Name = "frmControlMttoEquipos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmControlMttoEquipos";
            this.Load += new System.EventHandler(this.frmControlMttoEquipos_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNuevo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgControlMtto)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvControlMttoVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialVista)).EndInit();
            this.pNuevo.ResumeLayout(false);
            this.pNuevo.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        public DevExpress.XtraEditors.SimpleButton btnImprimir;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.ComboBox cbxBuscar;
        private System.Windows.Forms.CheckBox cbHistorial;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox btnNuevo;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtIntervalo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaCambio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAccesorio;
        public System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl dtgControlMtto;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvControlMttoVista;
        private DevExpress.XtraGrid.GridControl dtgHistorial;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvHistorialVista;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarAccesorio;
        private System.Windows.Forms.Panel pNuevo;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.TextBox txtAccesorio;
        private DevExpress.XtraEditors.SimpleButton btnNuevoAccesorio;
        private System.Windows.Forms.Label label59;
        public System.Windows.Forms.Button btnCerrar3;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarHistorial;
    }
}