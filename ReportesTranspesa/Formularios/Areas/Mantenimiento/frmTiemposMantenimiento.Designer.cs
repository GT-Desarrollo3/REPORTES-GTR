namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    partial class frmTiemposMantenimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTiemposMantenimiento));
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInsertarTiempos = new DevExpress.XtraEditors.SimpleButton();
            this.label20 = new System.Windows.Forms.Label();
            this.cbxTipoVehiculo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxOperaciones = new System.Windows.Forms.ComboBox();
            this.dtpPeriodo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTotalHoras = new System.Windows.Forms.TabPage();
            this.dtgTotalTiempos = new DevExpress.XtraGrid.GridControl();
            this.dgvTotalTiemposView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabPorcentaje = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgMTTR = new DevExpress.XtraGrid.GridControl();
            this.dgvMTTRView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgMTBF = new DevExpress.XtraGrid.GridControl();
            this.dgvMTBFView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pInsertarTiempos = new System.Windows.Forms.Panel();
            this.btnGuardarTiempos = new DevExpress.XtraEditors.SimpleButton();
            this.txtDias = new System.Windows.Forms.TextBox();
            this.lblDias = new System.Windows.Forms.Label();
            this.txtHoras = new System.Windows.Forms.TextBox();
            this.lblHoras = new System.Windows.Forms.Label();
            this.cbxOperacionInsertar = new System.Windows.Forms.ComboBox();
            this.lblOperacionInsertar = new System.Windows.Forms.Label();
            this.dtpPeriodoInsertar = new System.Windows.Forms.DateTimePicker();
            this.lblPeriodoInsertar = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.dtgTiemposOP = new DevExpress.XtraGrid.GridControl();
            this.dgvTiemposOP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblTituloInsertar = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabTotalHoras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTotalTiempos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotalTiemposView)).BeginInit();
            this.tabPorcentaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMTTR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTTRView)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMTBF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTBFView)).BeginInit();
            this.panel3.SuspendLayout();
            this.pInsertarTiempos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiemposOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiemposOP)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Aquamarine;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1018, 44);
            this.label2.TabIndex = 19;
            this.label2.Text = "REGISTRO DE TIEMPOS DE MANTENIMIENTO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.btnInsertarTiempos);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.cbxTipoVehiculo);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtPlaca);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cbxOperaciones);
            this.panel1.Controls.Add(this.dtpPeriodo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1018, 89);
            this.panel1.TabIndex = 20;
            // 
            // btnInsertarTiempos
            // 
            this.btnInsertarTiempos.Appearance.BackColor = System.Drawing.Color.White;
            this.btnInsertarTiempos.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnInsertarTiempos.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnInsertarTiempos.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertarTiempos.Appearance.Options.UseBackColor = true;
            this.btnInsertarTiempos.Appearance.Options.UseBorderColor = true;
            this.btnInsertarTiempos.Appearance.Options.UseFont = true;
            this.btnInsertarTiempos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInsertarTiempos.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertarTiempos.Image")));
            this.btnInsertarTiempos.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnInsertarTiempos.Location = new System.Drawing.Point(26, 22);
            this.btnInsertarTiempos.Name = "btnInsertarTiempos";
            this.btnInsertarTiempos.Size = new System.Drawing.Size(105, 47);
            this.btnInsertarTiempos.TabIndex = 222;
            this.btnInsertarTiempos.Text = "Insertar\r\nTiempos";
            this.btnInsertarTiempos.ToolTip = "Insertar Tiempos";
            this.btnInsertarTiempos.Click += new System.EventHandler(this.btnInsertarTiempos_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(675, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(108, 16);
            this.label20.TabIndex = 117;
            this.label20.Text = "Tipo Vehículo:";
            // 
            // cbxTipoVehiculo
            // 
            this.cbxTipoVehiculo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoVehiculo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoVehiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoVehiculo.FormattingEnabled = true;
            this.cbxTipoVehiculo.Location = new System.Drawing.Point(678, 45);
            this.cbxTipoVehiculo.Name = "cbxTipoVehiculo";
            this.cbxTipoVehiculo.Size = new System.Drawing.Size(152, 24);
            this.cbxTipoVehiculo.TabIndex = 116;
            this.cbxTipoVehiculo.SelectedIndexChanged += new System.EventHandler(this.cbxTipoVehiculo_SelectedIndexChanged);
            this.cbxTipoVehiculo.DropDownClosed += new System.EventHandler(this.cbxTipoVehiculo_DropDownClosed);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(495, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 16);
            this.label10.TabIndex = 112;
            this.label10.Text = "Programación:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPlaca.Location = new System.Drawing.Point(393, 47);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(81, 22);
            this.txtPlaca.TabIndex = 111;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(390, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 16);
            this.label9.TabIndex = 110;
            this.label9.Text = "Placa:";
            // 
            // cbxOperaciones
            // 
            this.cbxOperaciones.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperaciones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxOperaciones.FormattingEnabled = true;
            this.cbxOperaciones.Location = new System.Drawing.Point(498, 45);
            this.cbxOperaciones.Name = "cbxOperaciones";
            this.cbxOperaciones.Size = new System.Drawing.Size(156, 24);
            this.cbxOperaciones.TabIndex = 109;
            this.cbxOperaciones.SelectedIndexChanged += new System.EventHandler(this.cbxOperaciones_SelectedIndexChanged);
            this.cbxOperaciones.DropDownClosed += new System.EventHandler(this.cbxOperaciones_DropDownClosed);
            // 
            // dtpPeriodo
            // 
            this.dtpPeriodo.CalendarForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtpPeriodo.CustomFormat = "MMyyyy";
            this.dtpPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPeriodo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodo.Location = new System.Drawing.Point(280, 33);
            this.dtpPeriodo.Name = "dtpPeriodo";
            this.dtpPeriodo.ShowUpDown = true;
            this.dtpPeriodo.Size = new System.Drawing.Size(89, 29);
            this.dtpPeriodo.TabIndex = 28;
            this.dtpPeriodo.Value = new System.DateTime(2022, 10, 31, 0, 0, 0, 0);
            this.dtpPeriodo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpPeriodo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Location = new System.Drawing.Point(146, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 26);
            this.label1.TabIndex = 27;
            this.label1.Text = "PERIODO:";
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(932, 20);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(50, 49);
            this.btnExcel.TabIndex = 19;
            this.btnExcel.Tag = "6";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(871, 20);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(50, 49);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTotalHoras);
            this.tabControl1.Controls.Add(this.tabPorcentaje);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 133);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1018, 405);
            this.tabControl1.TabIndex = 21;
            // 
            // tabTotalHoras
            // 
            this.tabTotalHoras.Controls.Add(this.dtgTotalTiempos);
            this.tabTotalHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabTotalHoras.Location = new System.Drawing.Point(4, 29);
            this.tabTotalHoras.Name = "tabTotalHoras";
            this.tabTotalHoras.Padding = new System.Windows.Forms.Padding(3);
            this.tabTotalHoras.Size = new System.Drawing.Size(1010, 372);
            this.tabTotalHoras.TabIndex = 2;
            this.tabTotalHoras.Text = "TOTAL";
            this.tabTotalHoras.UseVisualStyleBackColor = true;
            // 
            // dtgTotalTiempos
            // 
            this.dtgTotalTiempos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTotalTiempos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTotalTiempos.Location = new System.Drawing.Point(3, 3);
            this.dtgTotalTiempos.LookAndFeel.SkinMaskColor = System.Drawing.Color.Aqua;
            this.dtgTotalTiempos.LookAndFeel.SkinName = "Money Twins";
            this.dtgTotalTiempos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTotalTiempos.MainView = this.dgvTotalTiemposView;
            this.dtgTotalTiempos.Name = "dtgTotalTiempos";
            this.dtgTotalTiempos.Size = new System.Drawing.Size(1004, 366);
            this.dtgTotalTiempos.TabIndex = 18;
            this.dtgTotalTiempos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTotalTiemposView});
            // 
            // dgvTotalTiemposView
            // 
            this.dgvTotalTiemposView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTotalTiemposView.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvTotalTiemposView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTotalTiemposView.Appearance.Row.Options.UseFont = true;
            this.dgvTotalTiemposView.GridControl = this.dtgTotalTiempos;
            this.dgvTotalTiemposView.Name = "dgvTotalTiemposView";
            this.dgvTotalTiemposView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvTotalTiemposView.OptionsBehavior.Editable = false;
            this.dgvTotalTiemposView.OptionsView.ColumnAutoWidth = false;
            this.dgvTotalTiemposView.OptionsView.ShowFooter = true;
            // 
            // tabPorcentaje
            // 
            this.tabPorcentaje.Controls.Add(this.splitContainer1);
            this.tabPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPorcentaje.Location = new System.Drawing.Point(4, 29);
            this.tabPorcentaje.Name = "tabPorcentaje";
            this.tabPorcentaje.Padding = new System.Windows.Forms.Padding(3);
            this.tabPorcentaje.Size = new System.Drawing.Size(1010, 372);
            this.tabPorcentaje.TabIndex = 3;
            this.tabPorcentaje.Text = "PROMEDIO";
            this.tabPorcentaje.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgMTTR);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgMTBF);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 366);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 12;
            // 
            // dtgMTTR
            // 
            this.dtgMTTR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgMTTR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMTTR.Location = new System.Drawing.Point(0, 34);
            this.dtgMTTR.LookAndFeel.SkinMaskColor = System.Drawing.Color.Aqua;
            this.dtgMTTR.LookAndFeel.SkinName = "Money Twins";
            this.dtgMTTR.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgMTTR.MainView = this.dgvMTTRView;
            this.dtgMTTR.Name = "dtgMTTR";
            this.dtgMTTR.Size = new System.Drawing.Size(1004, 142);
            this.dtgMTTR.TabIndex = 19;
            this.dtgMTTR.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMTTRView});
            // 
            // dgvMTTRView
            // 
            this.dgvMTTRView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMTTRView.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvMTTRView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMTTRView.Appearance.Row.Options.UseFont = true;
            this.dgvMTTRView.GridControl = this.dtgMTTR;
            this.dgvMTTRView.Name = "dgvMTTRView";
            this.dgvMTTRView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvMTTRView.OptionsBehavior.Editable = false;
            this.dgvMTTRView.OptionsView.ColumnAutoWidth = false;
            this.dgvMTTRView.OptionsView.ShowGroupPanel = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 34);
            this.panel2.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label3.Location = new System.Drawing.Point(15, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(329, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Tiempo Medio de Reparación (MTTR):";
            // 
            // dtgMTBF
            // 
            this.dtgMTBF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgMTBF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMTBF.Location = new System.Drawing.Point(0, 34);
            this.dtgMTBF.LookAndFeel.SkinMaskColor = System.Drawing.Color.Aqua;
            this.dtgMTBF.LookAndFeel.SkinName = "Money Twins";
            this.dtgMTBF.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgMTBF.MainView = this.dgvMTBFView;
            this.dtgMTBF.Name = "dtgMTBF";
            this.dtgMTBF.Size = new System.Drawing.Size(1004, 152);
            this.dtgMTBF.TabIndex = 20;
            this.dtgMTBF.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMTBFView});
            // 
            // dgvMTBFView
            // 
            this.dgvMTBFView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMTBFView.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvMTBFView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMTBFView.Appearance.Row.Options.UseFont = true;
            this.dgvMTBFView.GridControl = this.dtgMTBF;
            this.dgvMTBFView.Name = "dgvMTBFView";
            this.dgvMTBFView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvMTBFView.OptionsBehavior.Editable = false;
            this.dgvMTBFView.OptionsView.ColumnAutoWidth = false;
            this.dgvMTBFView.OptionsView.ShowGroupPanel = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1004, 34);
            this.panel3.TabIndex = 21;
            // 
            // pInsertarTiempos
            // 
            this.pInsertarTiempos.BackColor = System.Drawing.Color.LemonChiffon;
            this.pInsertarTiempos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pInsertarTiempos.Controls.Add(this.btnGuardarTiempos);
            this.pInsertarTiempos.Controls.Add(this.txtDias);
            this.pInsertarTiempos.Controls.Add(this.lblDias);
            this.pInsertarTiempos.Controls.Add(this.txtHoras);
            this.pInsertarTiempos.Controls.Add(this.lblHoras);
            this.pInsertarTiempos.Controls.Add(this.cbxOperacionInsertar);
            this.pInsertarTiempos.Controls.Add(this.lblOperacionInsertar);
            this.pInsertarTiempos.Controls.Add(this.dtpPeriodoInsertar);
            this.pInsertarTiempos.Controls.Add(this.lblPeriodoInsertar);
            this.pInsertarTiempos.Controls.Add(this.btnCerrar);
            this.pInsertarTiempos.Controls.Add(this.dtgTiemposOP);
            this.pInsertarTiempos.Controls.Add(this.lblTituloInsertar);
            this.pInsertarTiempos.Location = new System.Drawing.Point(200, 100);
            this.pInsertarTiempos.Name = "pInsertarTiempos";
            this.pInsertarTiempos.Size = new System.Drawing.Size(600, 480);
            this.pInsertarTiempos.TabIndex = 224;
            this.pInsertarTiempos.Visible = false;
            this.pInsertarTiempos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pInsertarTiempos_MouseMove);
            // 
            // btnGuardarTiempos
            // 
            this.btnGuardarTiempos.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardarTiempos.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardarTiempos.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardarTiempos.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarTiempos.Appearance.Options.UseBackColor = true;
            this.btnGuardarTiempos.Appearance.Options.UseBorderColor = true;
            this.btnGuardarTiempos.Appearance.Options.UseFont = true;
            this.btnGuardarTiempos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarTiempos.Location = new System.Drawing.Point(456, 78);
            this.btnGuardarTiempos.Name = "btnGuardarTiempos";
            this.btnGuardarTiempos.Size = new System.Drawing.Size(120, 30);
            this.btnGuardarTiempos.TabIndex = 10;
            this.btnGuardarTiempos.Text = "Guardar";
            this.btnGuardarTiempos.ToolTip = "Guardar Tiempos";
            this.btnGuardarTiempos.Click += new System.EventHandler(this.btnGuardarTiempos_Click);
            // 
            // txtDias
            // 
            this.txtDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtDias.Location = new System.Drawing.Point(286, 83);
            this.txtDias.Name = "txtDias";
            this.txtDias.Size = new System.Drawing.Size(60, 22);
            this.txtDias.TabIndex = 9;
            this.txtDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDias_KeyPress);
            // 
            // lblDias
            // 
            this.lblDias.AutoSize = true;
            this.lblDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDias.Location = new System.Drawing.Point(196, 85);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(44, 16);
            this.lblDias.TabIndex = 8;
            this.lblDias.Text = "Días:";
            // 
            // txtHoras
            // 
            this.txtHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtHoras.Location = new System.Drawing.Point(88, 83);
            this.txtHoras.Name = "txtHoras";
            this.txtHoras.Size = new System.Drawing.Size(60, 22);
            this.txtHoras.TabIndex = 7;
            this.txtHoras.Text = "24";
            this.txtHoras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHoras_KeyPress);
            // 
            // lblHoras
            // 
            this.lblHoras.AutoSize = true;
            this.lblHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoras.Location = new System.Drawing.Point(16, 85);
            this.lblHoras.Name = "lblHoras";
            this.lblHoras.Size = new System.Drawing.Size(54, 16);
            this.lblHoras.TabIndex = 6;
            this.lblHoras.Text = "Horas:";
            // 
            // cbxOperacionInsertar
            // 
            this.cbxOperacionInsertar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperacionInsertar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperacionInsertar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacionInsertar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxOperacionInsertar.FormattingEnabled = true;
            this.cbxOperacionInsertar.Location = new System.Drawing.Point(286, 44);
            this.cbxOperacionInsertar.Name = "cbxOperacionInsertar";
            this.cbxOperacionInsertar.Size = new System.Drawing.Size(290, 24);
            this.cbxOperacionInsertar.TabIndex = 5;
            // 
            // lblOperacionInsertar
            // 
            this.lblOperacionInsertar.AutoSize = true;
            this.lblOperacionInsertar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperacionInsertar.Location = new System.Drawing.Point(196, 48);
            this.lblOperacionInsertar.Name = "lblOperacionInsertar";
            this.lblOperacionInsertar.Size = new System.Drawing.Size(84, 16);
            this.lblOperacionInsertar.TabIndex = 4;
            this.lblOperacionInsertar.Text = "Operación:";
            // 
            // dtpPeriodoInsertar
            // 
            this.dtpPeriodoInsertar.CustomFormat = "MMyyyy";
            this.dtpPeriodoInsertar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPeriodoInsertar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodoInsertar.Location = new System.Drawing.Point(88, 44);
            this.dtpPeriodoInsertar.Name = "dtpPeriodoInsertar";
            this.dtpPeriodoInsertar.ShowUpDown = true;
            this.dtpPeriodoInsertar.Size = new System.Drawing.Size(90, 23);
            this.dtpPeriodoInsertar.TabIndex = 3;
            this.dtpPeriodoInsertar.ValueChanged += new System.EventHandler(this.dtpPeriodoInsertar_ValueChanged);
            // 
            // lblPeriodoInsertar
            // 
            this.lblPeriodoInsertar.AutoSize = true;
            this.lblPeriodoInsertar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodoInsertar.Location = new System.Drawing.Point(16, 48);
            this.lblPeriodoInsertar.Name = "lblPeriodoInsertar";
            this.lblPeriodoInsertar.Size = new System.Drawing.Size(67, 16);
            this.lblPeriodoInsertar.TabIndex = 2;
            this.lblPeriodoInsertar.Text = "Periodo:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(568, 2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(28, 28);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // dtgTiemposOP
            // 
            this.dtgTiemposOP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgTiemposOP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTiemposOP.Location = new System.Drawing.Point(12, 122);
            this.dtgTiemposOP.LookAndFeel.SkinMaskColor = System.Drawing.Color.Aqua;
            this.dtgTiemposOP.LookAndFeel.SkinName = "Money Twins";
            this.dtgTiemposOP.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTiemposOP.MainView = this.dgvTiemposOP;
            this.dtgTiemposOP.Name = "dtgTiemposOP";
            this.dtgTiemposOP.Size = new System.Drawing.Size(574, 344);
            this.dtgTiemposOP.TabIndex = 19;
            this.dtgTiemposOP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTiemposOP});
            // 
            // dgvTiemposOP
            // 
            this.dgvTiemposOP.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTiemposOP.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvTiemposOP.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTiemposOP.Appearance.Row.Options.UseFont = true;
            this.dgvTiemposOP.GridControl = this.dtgTiemposOP;
            this.dgvTiemposOP.Name = "dgvTiemposOP";
            this.dgvTiemposOP.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvTiemposOP.OptionsBehavior.Editable = false;
            this.dgvTiemposOP.OptionsView.ColumnAutoWidth = false;
            this.dgvTiemposOP.OptionsView.ShowGroupPanel = false;
            // 
            // lblTituloInsertar
            // 
            this.lblTituloInsertar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.lblTituloInsertar.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloInsertar.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloInsertar.ForeColor = System.Drawing.Color.White;
            this.lblTituloInsertar.Location = new System.Drawing.Point(0, 0);
            this.lblTituloInsertar.Name = "lblTituloInsertar";
            this.lblTituloInsertar.Size = new System.Drawing.Size(598, 32);
            this.lblTituloInsertar.TabIndex = 0;
            this.lblTituloInsertar.Text = "REGISTRO DE TIEMPOS POR OPERACIÓN";
            this.lblTituloInsertar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTituloInsertar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pInsertarTiempos_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label4.Location = new System.Drawing.Point(15, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 20);
            this.label4.TabIndex = 29;
            this.label4.Text = "Tiempo Medio Entre Fallas (MTBF):";
            // 
            // frmTiemposMantenimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1018, 538);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pInsertarTiempos);
            this.Name = "frmTiemposMantenimiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRO DE TIEMPOS DE MANTENIMIENTO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTiemposMantenimiento_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabTotalHoras.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTotalTiempos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotalTiemposView)).EndInit();
            this.tabPorcentaje.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMTTR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTTRView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMTBF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTBFView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pInsertarTiempos.ResumeLayout(false);
            this.pInsertarTiempos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiemposOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiemposOP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbxTipoVehiculo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxOperaciones;
        private System.Windows.Forms.DateTimePicker dtpPeriodo;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTotalHoras;
        private System.Windows.Forms.TabPage tabPorcentaje;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl dtgTotalTiempos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTotalTiemposView;
        private DevExpress.XtraGrid.GridControl dtgMTTR;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMTTRView;
        private DevExpress.XtraGrid.GridControl dtgMTBF;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMTBFView;
        private System.Windows.Forms.Panel pInsertarTiempos;
        private System.Windows.Forms.Label lblTituloInsertar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblPeriodoInsertar;
        private System.Windows.Forms.DateTimePicker dtpPeriodoInsertar;
        private System.Windows.Forms.Label lblOperacionInsertar;
        private System.Windows.Forms.ComboBox cbxOperacionInsertar;
        private System.Windows.Forms.Label lblHoras;
        private System.Windows.Forms.TextBox txtHoras;
        private System.Windows.Forms.Label lblDias;
        private System.Windows.Forms.TextBox txtDias;
        private DevExpress.XtraEditors.SimpleButton btnGuardarTiempos;
        private DevExpress.XtraEditors.SimpleButton btnInsertarTiempos;
        private DevExpress.XtraGrid.GridControl dtgTiemposOP;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTiemposOP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
    }
}