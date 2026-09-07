namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmIndicadorInspecciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndicadorInspecciones));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxOperaciones = new System.Windows.Forms.ComboBox();
            this.dtpFechaI = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.tabTiemposViaje = new System.Windows.Forms.TabControl();
            this.tabDatos = new System.Windows.Forms.TabPage();
            this.dtgDatoIndicador = new DevExpress.XtraGrid.GridControl();
            this.dgvDatoIndicadorVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabResumen = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgResumenI = new DevExpress.XtraGrid.GridControl();
            this.dgvResumenIVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExcel2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar2 = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxOperaciones2 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.pListarUnidades = new System.Windows.Forms.Panel();
            this.dtgListaTractos = new DevExpress.XtraGrid.GridControl();
            this.dgvListaTractosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabTiemposViaje.SuspendLayout();
            this.tabDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDatoIndicador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatoIndicadorVista)).BeginInit();
            this.tabResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResumenI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenIVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pListarUnidades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaTractos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTractosView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1152, 50);
            this.label1.TabIndex = 14;
            this.label1.Text = "INDICADOR DE INSPECCIONES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.txtPlaca);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbxOperaciones);
            this.panel1.Controls.Add(this.dtpFechaI);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1138, 99);
            this.panel1.TabIndex = 57;
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
            this.btnExcel.Location = new System.Drawing.Point(505, 25);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(50, 47);
            this.btnExcel.TabIndex = 57;
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
            this.btnBuscar.Location = new System.Drawing.Point(444, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(50, 47);
            this.btnBuscar.TabIndex = 56;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtPlaca
            // 
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(249, 19);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(146, 21);
            this.txtPlaca.TabIndex = 7;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(176, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Operación:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(176, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Placa:";
            // 
            // cbxOperaciones
            // 
            this.cbxOperaciones.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperaciones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxOperaciones.FormattingEnabled = true;
            this.cbxOperaciones.Items.AddRange(new object[] {
            "TODOS",
            "LINDLEY",
            "LIMAGAS",
            "TOLVAS",
            "GENERAL",
            "VOLCAN"});
            this.cbxOperaciones.Location = new System.Drawing.Point(249, 53);
            this.cbxOperaciones.Name = "cbxOperaciones";
            this.cbxOperaciones.Size = new System.Drawing.Size(146, 23);
            this.cbxOperaciones.TabIndex = 4;
            this.cbxOperaciones.DropDownClosed += new System.EventHandler(this.cbxOperaciones_DropDownClosed);
            // 
            // dtpFechaI
            // 
            this.dtpFechaI.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.dtpFechaI.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaI.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.dtpFechaI.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaI.Location = new System.Drawing.Point(26, 45);
            this.dtpFechaI.Name = "dtpFechaI";
            this.dtpFechaI.Size = new System.Drawing.Size(127, 25);
            this.dtpFechaI.TabIndex = 222;
            this.dtpFechaI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaI_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 15);
            this.label5.TabIndex = 221;
            this.label5.Text = "Seleccionar Fecha:";
            // 
            // tabTiemposViaje
            // 
            this.tabTiemposViaje.Controls.Add(this.tabDatos);
            this.tabTiemposViaje.Controls.Add(this.tabResumen);
            this.tabTiemposViaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTiemposViaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabTiemposViaje.Location = new System.Drawing.Point(0, 50);
            this.tabTiemposViaje.Name = "tabTiemposViaje";
            this.tabTiemposViaje.SelectedIndex = 0;
            this.tabTiemposViaje.Size = new System.Drawing.Size(1152, 519);
            this.tabTiemposViaje.TabIndex = 186;
            // 
            // tabDatos
            // 
            this.tabDatos.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabDatos.Controls.Add(this.dtgDatoIndicador);
            this.tabDatos.Controls.Add(this.panel1);
            this.tabDatos.Location = new System.Drawing.Point(4, 29);
            this.tabDatos.Name = "tabDatos";
            this.tabDatos.Padding = new System.Windows.Forms.Padding(3);
            this.tabDatos.Size = new System.Drawing.Size(1144, 486);
            this.tabDatos.TabIndex = 0;
            this.tabDatos.Text = "DATOS";
            // 
            // dtgDatoIndicador
            // 
            this.dtgDatoIndicador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgDatoIndicador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDatoIndicador.Location = new System.Drawing.Point(3, 102);
            this.dtgDatoIndicador.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dtgDatoIndicador.LookAndFeel.SkinName = "Money Twins";
            this.dtgDatoIndicador.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDatoIndicador.MainView = this.dgvDatoIndicadorVista;
            this.dtgDatoIndicador.Name = "dtgDatoIndicador";
            this.dtgDatoIndicador.Size = new System.Drawing.Size(1138, 381);
            this.dtgDatoIndicador.TabIndex = 184;
            this.dtgDatoIndicador.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvDatoIndicadorVista});
            // 
            // dgvDatoIndicadorVista
            // 
            this.dgvDatoIndicadorVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDatoIndicadorVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvDatoIndicadorVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDatoIndicadorVista.Appearance.Row.Options.UseFont = true;
            this.dgvDatoIndicadorVista.GridControl = this.dtgDatoIndicador;
            this.dgvDatoIndicadorVista.Name = "dgvDatoIndicadorVista";
            this.dgvDatoIndicadorVista.OptionsBehavior.Editable = false;
            this.dgvDatoIndicadorVista.OptionsView.ColumnAutoWidth = false;
            this.dgvDatoIndicadorVista.OptionsView.RowAutoHeight = true;
            this.dgvDatoIndicadorVista.OptionsView.ShowFooter = true;
            this.dgvDatoIndicadorVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvDatoIndicadorVista_CustomDrawCell);
            this.dgvDatoIndicadorVista.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgvDatoIndicadorVista_CustomSummaryCalculate);
            // 
            // tabResumen
            // 
            this.tabResumen.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabResumen.Controls.Add(this.splitContainer1);
            this.tabResumen.Controls.Add(this.panel2);
            this.tabResumen.Location = new System.Drawing.Point(4, 29);
            this.tabResumen.Name = "tabResumen";
            this.tabResumen.Padding = new System.Windows.Forms.Padding(3);
            this.tabResumen.Size = new System.Drawing.Size(1144, 486);
            this.tabResumen.TabIndex = 1;
            this.tabResumen.Text = "RESUMEN";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 102);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgResumenI);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Size = new System.Drawing.Size(1138, 381);
            this.splitContainer1.SplitterDistance = 569;
            this.splitContainer1.TabIndex = 59;
            // 
            // dtgResumenI
            // 
            this.dtgResumenI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgResumenI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgResumenI.Location = new System.Drawing.Point(0, 0);
            this.dtgResumenI.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dtgResumenI.LookAndFeel.SkinName = "Money Twins";
            this.dtgResumenI.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgResumenI.MainView = this.dgvResumenIVista;
            this.dtgResumenI.Name = "dtgResumenI";
            this.dtgResumenI.Size = new System.Drawing.Size(569, 381);
            this.dtgResumenI.TabIndex = 17;
            this.dtgResumenI.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvResumenIVista});
            // 
            // dgvResumenIVista
            // 
            this.dgvResumenIVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvResumenIVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvResumenIVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvResumenIVista.Appearance.Row.Options.UseFont = true;
            this.dgvResumenIVista.GridControl = this.dtgResumenI;
            this.dgvResumenIVista.Name = "dgvResumenIVista";
            this.dgvResumenIVista.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvResumenIVista.OptionsBehavior.Editable = false;
            this.dgvResumenIVista.OptionsView.ColumnAutoWidth = false;
            this.dgvResumenIVista.OptionsView.ShowFooter = true;
            this.dgvResumenIVista.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvResumenIVista_RowCellClick);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LemonChiffon;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(565, 381);
            this.chart1.TabIndex = 187;
            this.chart1.Text = "GRÁFICO DE LÍNEAS";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel2.Controls.Add(this.btnExcel2);
            this.panel2.Controls.Add(this.btnBuscar2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cbxOperaciones2);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1138, 99);
            this.panel2.TabIndex = 58;
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
            this.btnExcel2.Location = new System.Drawing.Point(539, 26);
            this.btnExcel2.Name = "btnExcel2";
            this.btnExcel2.Size = new System.Drawing.Size(50, 47);
            this.btnExcel2.TabIndex = 225;
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
            this.btnBuscar2.Location = new System.Drawing.Point(478, 26);
            this.btnBuscar2.Name = "btnBuscar2";
            this.btnBuscar2.Size = new System.Drawing.Size(50, 47);
            this.btnBuscar2.TabIndex = 224;
            this.btnBuscar2.Tag = "5";
            this.btnBuscar2.ToolTip = "Buscar";
            this.btnBuscar2.Click += new System.EventHandler(this.btnBuscar2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(293, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "Operación:";
            // 
            // cbxOperaciones2
            // 
            this.cbxOperaciones2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperaciones2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperaciones2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperaciones2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxOperaciones2.FormattingEnabled = true;
            this.cbxOperaciones2.Items.AddRange(new object[] {
            "LINDLEY",
            "LIMAGAS",
            "TOLVAS",
            "GENERAL",
            "VOLCAN"});
            this.cbxOperaciones2.Location = new System.Drawing.Point(296, 46);
            this.cbxOperaciones2.Name = "cbxOperaciones2";
            this.cbxOperaciones2.Size = new System.Drawing.Size(146, 23);
            this.cbxOperaciones2.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtpFechaInicio);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(26, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 58);
            this.groupBox2.TabIndex = 223;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rango de Fechas:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(133, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(114, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "--";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(15, 24);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // pListarUnidades
            // 
            this.pListarUnidades.BackColor = System.Drawing.Color.LemonChiffon;
            this.pListarUnidades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pListarUnidades.Controls.Add(this.dtgListaTractos);
            this.pListarUnidades.Controls.Add(this.lblFecha);
            this.pListarUnidades.Controls.Add(this.label13);
            this.pListarUnidades.Controls.Add(this.label11);
            this.pListarUnidades.Controls.Add(this.btnCerrar2);
            this.pListarUnidades.Controls.Add(this.lblOperacion);
            this.pListarUnidades.Controls.Add(this.label15);
            this.pListarUnidades.Location = new System.Drawing.Point(114, 218);
            this.pListarUnidades.Name = "pListarUnidades";
            this.pListarUnidades.Size = new System.Drawing.Size(355, 317);
            this.pListarUnidades.TabIndex = 226;
            this.pListarUnidades.Visible = false;
            this.pListarUnidades.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pListarUnidades_MouseMove);
            // 
            // dtgListaTractos
            // 
            this.dtgListaTractos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaTractos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgListaTractos.Location = new System.Drawing.Point(0, 97);
            this.dtgListaTractos.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dtgListaTractos.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dtgListaTractos.LookAndFeel.SkinName = "Blue";
            this.dtgListaTractos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaTractos.MainView = this.dgvListaTractosView;
            this.dtgListaTractos.Name = "dtgListaTractos";
            this.dtgListaTractos.Size = new System.Drawing.Size(353, 218);
            this.dtgListaTractos.TabIndex = 137;
            this.dtgListaTractos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaTractosView});
            // 
            // dgvListaTractosView
            // 
            this.dgvListaTractosView.GridControl = this.dtgListaTractos;
            this.dgvListaTractosView.Name = "dgvListaTractosView";
            this.dgvListaTractosView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaTractosView.OptionsView.RowAutoHeight = true;
            this.dgvListaTractosView.OptionsView.ShowFooter = true;
            this.dgvListaTractosView.OptionsView.ShowGroupPanel = false;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblFecha.Location = new System.Drawing.Point(63, 65);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(63, 18);
            this.lblFecha.TabIndex = 205;
            this.lblFecha.Text = "FECHA";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(12, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 18);
            this.label13.TabIndex = 204;
            this.label13.Text = "Fecha:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(11, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(253, 20);
            this.label11.TabIndex = 30;
            this.label11.Text = "UNIDADES PROGRAMADAS";
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar2.BackColor = System.Drawing.Color.Red;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar2.ForeColor = System.Drawing.Color.White;
            this.btnCerrar2.Location = new System.Drawing.Point(326, -1);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(28, 28);
            this.btnCerrar2.TabIndex = 29;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblOperacion.Location = new System.Drawing.Point(90, 40);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(106, 18);
            this.lblOperacion.TabIndex = 207;
            this.lblOperacion.Text = "OPERACION";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(12, 40);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 18);
            this.label15.TabIndex = 208;
            this.label15.Text = "Operación:";
            // 
            // frmIndicadorInspecciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1152, 569);
            this.Controls.Add(this.tabTiemposViaje);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pListarUnidades);
            this.Name = "frmIndicadorInspecciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INDICADOR INSPECCIONES";
            this.Load += new System.EventHandler(this.frmIndicadorInspecciones_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabTiemposViaje.ResumeLayout(false);
            this.tabDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDatoIndicador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatoIndicadorVista)).EndInit();
            this.tabResumen.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgResumenI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenIVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pListarUnidades.ResumeLayout(false);
            this.pListarUnidades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaTractos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTractosView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxOperaciones;
        private System.Windows.Forms.DateTimePicker dtpFechaI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabTiemposViaje;
        private System.Windows.Forms.TabPage tabDatos;
        private DevExpress.XtraGrid.GridControl dtgDatoIndicador;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDatoIndicadorVista;
        private System.Windows.Forms.TabPage tabResumen;
        private DevExpress.XtraGrid.GridControl dtgResumenI;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvResumenIVista;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxOperaciones2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private DevExpress.XtraEditors.SimpleButton btnBuscar2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnExcel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel pListarUnidades;
        private DevExpress.XtraGrid.GridControl dtgListaTractos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaTractosView;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Button btnCerrar2;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.Label label15;
    }
}