namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmComponenteDesgaste
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComponenteDesgaste));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.tsHistReportes = new System.Windows.Forms.ToolStripButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnGuardarComp = new DevExpress.XtraEditors.SimpleButton();
            this.txtTipoUnidad = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuscarUnidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dtgReporteTracto = new DevExpress.XtraGrid.GridControl();
            this.dgvReporteTractoVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelT = new System.Windows.Forms.Label();
            this.dtgReporteCarreta = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsActualizarC = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvReporteCarretaVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelKP = new System.Windows.Forms.Label();
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.Tornamesa = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
            this.Kinpin = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
            this.compositeLink1 = new DevExpress.XtraPrintingLinks.CompositeLink(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsActualizarT = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip6.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReporteTracto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteTractoVista)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReporteCarreta)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteCarretaVista)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(967, 44);
            this.label1.TabIndex = 16;
            this.label1.Text = "REPORTE DE COMPONENTES DESGASTE ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip6
            // 
            this.toolStrip6.BackColor = System.Drawing.Color.LemonChiffon;
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsHistReportes});
            this.toolStrip6.Location = new System.Drawing.Point(0, 44);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(967, 25);
            this.toolStrip6.TabIndex = 221;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // tsHistReportes
            // 
            this.tsHistReportes.Image = global::ReportesTranspesa.Properties.Resources.ordenar;
            this.tsHistReportes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsHistReportes.Name = "tsHistReportes";
            this.tsHistReportes.Size = new System.Drawing.Size(142, 22);
            this.tsHistReportes.Text = "Historial de Desgastes";
            this.tsHistReportes.Click += new System.EventHandler(this.tsHistReportes_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel6.Controls.Add(this.groupBox6);
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 69);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(967, 111);
            this.panel6.TabIndex = 222;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnGuardarComp);
            this.groupBox6.Controls.Add(this.txtTipoUnidad);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.txtPlaca);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(19, 11);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(378, 85);
            this.groupBox6.TabIndex = 219;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "AÑADIR NUEVA UNIDAD: ";
            // 
            // btnGuardarComp
            // 
            this.btnGuardarComp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarComp.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardarComp.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardarComp.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardarComp.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarComp.Appearance.Options.UseBackColor = true;
            this.btnGuardarComp.Appearance.Options.UseBorderColor = true;
            this.btnGuardarComp.Appearance.Options.UseFont = true;
            this.btnGuardarComp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarComp.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarComp.Image")));
            this.btnGuardarComp.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardarComp.Location = new System.Drawing.Point(315, 22);
            this.btnGuardarComp.Name = "btnGuardarComp";
            this.btnGuardarComp.Size = new System.Drawing.Size(47, 47);
            this.btnGuardarComp.TabIndex = 222;
            this.btnGuardarComp.ToolTip = "Guardar";
            this.btnGuardarComp.Click += new System.EventHandler(this.btnGuardarComp_Click);
            // 
            // txtTipoUnidad
            // 
            this.txtTipoUnidad.BackColor = System.Drawing.SystemColors.Control;
            this.txtTipoUnidad.Location = new System.Drawing.Point(151, 47);
            this.txtTipoUnidad.Name = "txtTipoUnidad";
            this.txtTipoUnidad.ReadOnly = true;
            this.txtTipoUnidad.Size = new System.Drawing.Size(140, 21);
            this.txtTipoUnidad.TabIndex = 219;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(12, 25);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(50, 15);
            this.label28.TabIndex = 218;
            this.label28.Text = "Unidad:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(15, 47);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(119, 21);
            this.txtPlaca.TabIndex = 204;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            this.txtPlaca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlaca_KeyUp);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft PhagsPa", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(148, 24);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(78, 16);
            this.label29.TabIndex = 216;
            this.label29.Text = "Tipo Unidad: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtBuscarUnidad);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxOperacion);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(416, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 85);
            this.groupBox1.TabIndex = 220;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTRO DE BÚSQUEDA: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(162, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 227;
            this.label5.Text = "--";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(179, 53);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaFin.TabIndex = 226;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 225;
            this.label4.Text = "Fecha:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(60, 53);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaInicio.TabIndex = 224;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnExcel.Location = new System.Drawing.Point(458, 22);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 197;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 218;
            this.label2.Text = "Unidad:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnBuscar.Location = new System.Drawing.Point(402, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscarUnidad
            // 
            this.txtBuscarUnidad.BackColor = System.Drawing.Color.White;
            this.txtBuscarUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarUnidad.Location = new System.Drawing.Point(60, 24);
            this.txtBuscarUnidad.Name = "txtBuscarUnidad";
            this.txtBuscarUnidad.Size = new System.Drawing.Size(100, 20);
            this.txtBuscarUnidad.TabIndex = 204;
            this.txtBuscarUnidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarUnidad_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(176, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 216;
            this.label3.Text = "Operación:";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(241, 23);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(135, 21);
            this.cbxOperacion.TabIndex = 223;
            this.cbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            this.cbxOperacion.DropDownClosed += new System.EventHandler(this.cbxOperacion_DropDownClosed);
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(34, 147);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(185, 104);
            this.lstPlaca.TabIndex = 223;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.LemonChiffon;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 180);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dtgReporteTracto);
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dtgReporteCarreta);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(967, 368);
            this.splitContainer2.SplitterDistance = 480;
            this.splitContainer2.TabIndex = 224;
            // 
            // dtgReporteTracto
            // 
            this.dtgReporteTracto.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgReporteTracto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgReporteTracto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgReporteTracto.Location = new System.Drawing.Point(0, 32);
            this.dtgReporteTracto.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dtgReporteTracto.LookAndFeel.SkinName = "Money Twins";
            this.dtgReporteTracto.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgReporteTracto.MainView = this.dgvReporteTractoVista;
            this.dtgReporteTracto.Name = "dtgReporteTracto";
            this.dtgReporteTracto.Size = new System.Drawing.Size(480, 336);
            this.dtgReporteTracto.TabIndex = 185;
            this.dtgReporteTracto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvReporteTractoVista});
            this.dtgReporteTracto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgReporteTracto_MouseUp);
            // 
            // dgvReporteTractoVista
            // 
            this.dgvReporteTractoVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReporteTractoVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvReporteTractoVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReporteTractoVista.Appearance.Row.Options.UseFont = true;
            this.dgvReporteTractoVista.GridControl = this.dtgReporteTracto;
            this.dgvReporteTractoVista.Name = "dgvReporteTractoVista";
            this.dgvReporteTractoVista.OptionsView.ColumnAutoWidth = false;
            this.dgvReporteTractoVista.OptionsView.RowAutoHeight = true;
            this.dgvReporteTractoVista.OptionsView.ShowFooter = true;
            this.dgvReporteTractoVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvReporteTractoVista_CustomDrawCell);
            this.dgvReporteTractoVista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvReporteTractoVista_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.labelT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 32);
            this.panel1.TabIndex = 186;
            // 
            // labelT
            // 
            this.labelT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelT.AutoSize = true;
            this.labelT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelT.ForeColor = System.Drawing.Color.Green;
            this.labelT.Location = new System.Drawing.Point(8, 6);
            this.labelT.Name = "labelT";
            this.labelT.Size = new System.Drawing.Size(119, 20);
            this.labelT.TabIndex = 2;
            this.labelT.Text = "TORNAMESA";
            // 
            // dtgReporteCarreta
            // 
            this.dtgReporteCarreta.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgReporteCarreta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgReporteCarreta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgReporteCarreta.Location = new System.Drawing.Point(0, 32);
            this.dtgReporteCarreta.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.dtgReporteCarreta.LookAndFeel.SkinName = "Money Twins";
            this.dtgReporteCarreta.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgReporteCarreta.MainView = this.dgvReporteCarretaVista;
            this.dtgReporteCarreta.Name = "dtgReporteCarreta";
            this.dtgReporteCarreta.Size = new System.Drawing.Size(483, 336);
            this.dtgReporteCarreta.TabIndex = 186;
            this.dtgReporteCarreta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvReporteCarretaVista});
            this.dtgReporteCarreta.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgReporteCarreta_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsActualizarC});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 26);
            // 
            // tsActualizarC
            // 
            this.tsActualizarC.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.tsActualizarC.Name = "tsActualizarC";
            this.tsActualizarC.Size = new System.Drawing.Size(176, 22);
            this.tsActualizarC.Text = "Actualizar Desgaste";
            this.tsActualizarC.Click += new System.EventHandler(this.tsActualizarC_Click);
            // 
            // dgvReporteCarretaVista
            // 
            this.dgvReporteCarretaVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReporteCarretaVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvReporteCarretaVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReporteCarretaVista.Appearance.Row.Options.UseFont = true;
            this.dgvReporteCarretaVista.GridControl = this.dtgReporteCarreta;
            this.dgvReporteCarretaVista.Name = "dgvReporteCarretaVista";
            this.dgvReporteCarretaVista.OptionsView.ColumnAutoWidth = false;
            this.dgvReporteCarretaVista.OptionsView.RowAutoHeight = true;
            this.dgvReporteCarretaVista.OptionsView.ShowFooter = true;
            this.dgvReporteCarretaVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvReporteCarretaVista_CustomDrawCell);
            this.dgvReporteCarretaVista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvReporteCarretaVista_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel2.Controls.Add(this.labelKP);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 32);
            this.panel2.TabIndex = 187;
            // 
            // labelKP
            // 
            this.labelKP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelKP.AutoSize = true;
            this.labelKP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKP.ForeColor = System.Drawing.Color.Navy;
            this.labelKP.Location = new System.Drawing.Point(8, 6);
            this.labelKP.Name = "labelKP";
            this.labelKP.Size = new System.Drawing.Size(67, 20);
            this.labelKP.TabIndex = 2;
            this.labelKP.Text = "KINPIN";
            // 
            // printingSystem1
            // 
            this.printingSystem1.Links.AddRange(new object[] {
            this.Tornamesa,
            this.Kinpin,
            this.compositeLink1});
            // 
            // Tornamesa
            // 
            this.Tornamesa.Component = this.dtgReporteTracto;
            this.Tornamesa.PrintingSystemBase = this.printingSystem1;
            this.Tornamesa.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Exact;
            // 
            // Kinpin
            // 
            this.Kinpin.Component = this.dtgReporteCarreta;
            this.Kinpin.PrintingSystemBase = this.printingSystem1;
            this.Kinpin.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Exact;
            // 
            // compositeLink1
            // 
            this.compositeLink1.Links.AddRange(new object[] {
            this.Tornamesa,
            this.Kinpin});
            this.compositeLink1.PrintingSystemBase = this.printingSystem1;
            this.compositeLink1.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Exact;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsActualizarT});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(177, 48);
            // 
            // tsActualizarT
            // 
            this.tsActualizarT.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.tsActualizarT.Name = "tsActualizarT";
            this.tsActualizarT.Size = new System.Drawing.Size(176, 22);
            this.tsActualizarT.Text = "Actualizar Desgaste";
            this.tsActualizarT.Click += new System.EventHandler(this.tsActualizarT_Click);
            // 
            // frmComponenteDesgaste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 548);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.toolStrip6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPlaca);
            this.Name = "frmComponenteDesgaste";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMPONENTE DESGASTE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmComponenteDesgaste_Load);
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgReporteTracto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteTractoVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReporteCarreta)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporteCarretaVista)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ToolStrip toolStrip6;
        private System.Windows.Forms.ToolStripButton tsHistReportes;
        public System.Windows.Forms.Panel panel6;
        private DevExpress.XtraEditors.SimpleButton btnGuardarComp;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox txtTipoUnidad;
        private System.Windows.Forms.ListView lstPlaca;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuscarUnidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxOperacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraGrid.GridControl dtgReporteTracto;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvReporteTractoVista;
        private DevExpress.XtraGrid.GridControl dtgReporteCarreta;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvReporteCarretaVista;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelT;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelKP;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsActualizarC;
        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
        public DevExpress.XtraPrinting.PrintableComponentLink Tornamesa;
        public DevExpress.XtraPrinting.PrintableComponentLink Kinpin;
        private DevExpress.XtraPrintingLinks.CompositeLink compositeLink1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsActualizarT;
    }
}