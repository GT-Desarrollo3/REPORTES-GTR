namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class Consolidado_Viajes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Consolidado_Viajes));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.gbTipo = new ReportesTranspesa.Grouper();
            this.rdbResumido = new MetroFramework.Controls.MetroRadioButton();
            this.rdbDetallado = new MetroFramework.Controls.MetroRadioButton();
            this.gbPendiente = new ReportesTranspesa.Grouper();
            this.rdbFacturar = new MetroFramework.Controls.MetroRadioButton();
            this.rdbCompletar = new MetroFramework.Controls.MetroRadioButton();
            this.gbFecha = new ReportesTranspesa.Grouper();
            this.rbFechaCreacion = new MetroFramework.Controls.MetroRadioButton();
            this.rbFechaProg = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.chkTodos = new MetroFramework.Controls.MetroCheckBox();
            this.pvgData = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbTipo.SuspendLayout();
            this.gbPendiente.SuspendLayout();
            this.gbFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.gbTipo);
            this.splitContainer1.Panel1.Controls.Add(this.gbPendiente);
            this.splitContainer1.Panel1.Controls.Add(this.gbFecha);
            this.splitContainer1.Panel1.Controls.Add(this.chkTodos);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pvgData);
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(816, 552);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 5;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseBackColor = true;
            this.btnImprimir.Appearance.Options.UseBorderColor = true;
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnImprimir.Location = new System.Drawing.Point(571, 86);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 55;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(497, 86);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 54;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(426, 86);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 53;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // gbTipo
            // 
            this.gbTipo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbTipo.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbTipo.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbTipo.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbTipo.BorderThickness = 1F;
            this.gbTipo.Controls.Add(this.rdbResumido);
            this.gbTipo.Controls.Add(this.rdbDetallado);
            this.gbTipo.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTipo.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbTipo.GroupImage = null;
            this.gbTipo.GroupTitle = "Mostrar reporte";
            this.gbTipo.Location = new System.Drawing.Point(212, 68);
            this.gbTipo.Name = "gbTipo";
            this.gbTipo.Padding = new System.Windows.Forms.Padding(20);
            this.gbTipo.PaintGroupBox = false;
            this.gbTipo.RoundCorners = 3;
            this.gbTipo.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbTipo.ShadowControl = false;
            this.gbTipo.ShadowThickness = 3;
            this.gbTipo.Size = new System.Drawing.Size(208, 55);
            this.gbTipo.TabIndex = 20;
            // 
            // rdbResumido
            // 
            this.rdbResumido.AutoSize = true;
            this.rdbResumido.Checked = true;
            this.rdbResumido.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rdbResumido.Location = new System.Drawing.Point(112, 31);
            this.rdbResumido.Name = "rdbResumido";
            this.rdbResumido.Size = new System.Drawing.Size(85, 19);
            this.rdbResumido.Style = MetroFramework.MetroColorStyle.Red;
            this.rdbResumido.TabIndex = 10;
            this.rdbResumido.TabStop = true;
            this.rdbResumido.Text = "Resumido";
            this.rdbResumido.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rdbResumido.UseSelectable = true;
            // 
            // rdbDetallado
            // 
            this.rdbDetallado.AutoSize = true;
            this.rdbDetallado.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rdbDetallado.Location = new System.Drawing.Point(17, 31);
            this.rdbDetallado.Name = "rdbDetallado";
            this.rdbDetallado.Size = new System.Drawing.Size(83, 19);
            this.rdbDetallado.Style = MetroFramework.MetroColorStyle.Red;
            this.rdbDetallado.TabIndex = 9;
            this.rdbDetallado.Text = "Detallado";
            this.rdbDetallado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rdbDetallado.UseSelectable = true;
            // 
            // gbPendiente
            // 
            this.gbPendiente.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbPendiente.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbPendiente.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbPendiente.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbPendiente.BorderThickness = 1F;
            this.gbPendiente.Controls.Add(this.rdbFacturar);
            this.gbPendiente.Controls.Add(this.rdbCompletar);
            this.gbPendiente.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPendiente.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbPendiente.GroupImage = null;
            this.gbPendiente.GroupTitle = "Viajes pendientes por";
            this.gbPendiente.Location = new System.Drawing.Point(212, 2);
            this.gbPendiente.Name = "gbPendiente";
            this.gbPendiente.Padding = new System.Windows.Forms.Padding(20);
            this.gbPendiente.PaintGroupBox = false;
            this.gbPendiente.RoundCorners = 3;
            this.gbPendiente.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbPendiente.ShadowControl = false;
            this.gbPendiente.ShadowThickness = 3;
            this.gbPendiente.Size = new System.Drawing.Size(208, 55);
            this.gbPendiente.TabIndex = 19;
            // 
            // rdbFacturar
            // 
            this.rdbFacturar.AutoSize = true;
            this.rdbFacturar.Checked = true;
            this.rdbFacturar.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rdbFacturar.Location = new System.Drawing.Point(110, 30);
            this.rdbFacturar.Name = "rdbFacturar";
            this.rdbFacturar.Size = new System.Drawing.Size(75, 19);
            this.rdbFacturar.Style = MetroFramework.MetroColorStyle.Red;
            this.rdbFacturar.TabIndex = 9;
            this.rdbFacturar.TabStop = true;
            this.rdbFacturar.Text = "Facturar";
            this.rdbFacturar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rdbFacturar.UseSelectable = true;
            // 
            // rdbCompletar
            // 
            this.rdbCompletar.AutoSize = true;
            this.rdbCompletar.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rdbCompletar.Location = new System.Drawing.Point(17, 30);
            this.rdbCompletar.Name = "rdbCompletar";
            this.rdbCompletar.Size = new System.Drawing.Size(89, 19);
            this.rdbCompletar.Style = MetroFramework.MetroColorStyle.Red;
            this.rdbCompletar.TabIndex = 10;
            this.rdbCompletar.Text = "Completar";
            this.rdbCompletar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rdbCompletar.UseSelectable = true;
            // 
            // gbFecha
            // 
            this.gbFecha.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFecha.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFecha.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFecha.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.BorderThickness = 1F;
            this.gbFecha.Controls.Add(this.rbFechaCreacion);
            this.gbFecha.Controls.Add(this.rbFechaProg);
            this.gbFecha.Controls.Add(this.metroLabel1);
            this.gbFecha.Controls.Add(this.dtpFechaIni);
            this.gbFecha.Controls.Add(this.metroLabel2);
            this.gbFecha.Controls.Add(this.dtpFechaFin);
            this.gbFecha.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFecha.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.GroupImage = null;
            this.gbFecha.GroupTitle = "Fecha";
            this.gbFecha.Location = new System.Drawing.Point(0, 0);
            this.gbFecha.Name = "gbFecha";
            this.gbFecha.Padding = new System.Windows.Forms.Padding(20);
            this.gbFecha.PaintGroupBox = false;
            this.gbFecha.RoundCorners = 3;
            this.gbFecha.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFecha.ShadowControl = false;
            this.gbFecha.ShadowThickness = 3;
            this.gbFecha.Size = new System.Drawing.Size(206, 123);
            this.gbFecha.TabIndex = 18;
            // 
            // rbFechaCreacion
            // 
            this.rbFechaCreacion.AutoSize = true;
            this.rbFechaCreacion.Location = new System.Drawing.Point(114, 99);
            this.rbFechaCreacion.Name = "rbFechaCreacion";
            this.rbFechaCreacion.Size = new System.Drawing.Size(79, 15);
            this.rbFechaCreacion.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaCreacion.TabIndex = 18;
            this.rbFechaCreacion.Text = "F.Creación";
            this.rbFechaCreacion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaCreacion.UseSelectable = true;
            // 
            // rbFechaProg
            // 
            this.rbFechaProg.AutoSize = true;
            this.rbFechaProg.Checked = true;
            this.rbFechaProg.Location = new System.Drawing.Point(11, 99);
            this.rbFechaProg.Name = "rbFechaProg";
            this.rbFechaProg.Size = new System.Drawing.Size(97, 15);
            this.rbFechaProg.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaProg.TabIndex = 17;
            this.rbFechaProg.TabStop = true;
            this.rbFechaProg.Text = "F.Programada";
            this.rbFechaProg.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaProg.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(9, 32);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(44, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 15;
            this.metroLabel1.Text = "Inicio:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(59, 28);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 13;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(22, 68);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(30, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 16;
            this.metroLabel2.Text = "Fin:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(59, 63);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 14;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkTodos.Location = new System.Drawing.Point(426, 42);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(115, 19);
            this.chkTodos.Style = MetroFramework.MetroColorStyle.Red;
            this.chkTodos.TabIndex = 17;
            this.chkTodos.Text = "Mostrar Todos";
            this.chkTodos.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkTodos.UseSelectable = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // pvgData
            // 
            this.pvgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvgData.Location = new System.Drawing.Point(0, 0);
            this.pvgData.LookAndFeel.SkinName = "Darkroom";
            this.pvgData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pvgData.Name = "pvgData";
            this.pvgData.Size = new System.Drawing.Size(816, 439);
            this.pvgData.TabIndex = 0;
            this.pvgData.CustomAppearance += new DevExpress.XtraPivotGrid.PivotCustomAppearanceEventHandler(this.pvgData_CustomAppearance);
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(816, 439);
            this.dtgvData.TabIndex = 1;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsBehavior.ReadOnly = true;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            // 
            // Consolidado_Viajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 632);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Consolidado_Viajes";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Consolidado de Viajes";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Operaciones_Consolidado_Viajes_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbTipo.ResumeLayout(false);
            this.gbTipo.PerformLayout();
            this.gbPendiente.ResumeLayout(false);
            this.gbPendiente.PerformLayout();
            this.gbFecha.ResumeLayout(false);
            this.gbFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroRadioButton rdbCompletar;
        private MetroFramework.Controls.MetroRadioButton rdbFacturar;
        private MetroFramework.Controls.MetroRadioButton rdbDetallado;
        private MetroFramework.Controls.MetroRadioButton rdbResumido;
        private MetroFramework.Controls.MetroCheckBox chkTodos;
        private Grouper gbFecha;
        private MetroFramework.Controls.MetroRadioButton rbFechaCreacion;
        private MetroFramework.Controls.MetroRadioButton rbFechaProg;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private Grouper gbTipo;
        private Grouper gbPendiente;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private DevExpress.XtraPivotGrid.PivotGridControl pvgData;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
    }
}