namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    partial class Facturacion_Contado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facturacion_Contado));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rbSalaverry = new MetroFramework.Controls.MetroRadioButton();
            this.cboAlmacen = new MetroFramework.Controls.MetroComboBox();
            this.rbLima = new MetroFramework.Controls.MetroRadioButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.rbTrujillo = new MetroFramework.Controls.MetroRadioButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.gbFecha = new ReportesTranspesa.Grouper();
            this.lblInicio = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.lblFin = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbFecha.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.rbSalaverry);
            this.splitContainer1.Panel1.Controls.Add(this.cboAlmacen);
            this.splitContainer1.Panel1.Controls.Add(this.rbLima);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.rbTrujillo);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.gbFecha);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(804, 552);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 2;
            // 
            // rbSalaverry
            // 
            this.rbSalaverry.AutoSize = true;
            this.rbSalaverry.Location = new System.Drawing.Point(507, 32);
            this.rbSalaverry.Name = "rbSalaverry";
            this.rbSalaverry.Size = new System.Drawing.Size(70, 15);
            this.rbSalaverry.Style = MetroFramework.MetroColorStyle.Red;
            this.rbSalaverry.TabIndex = 54;
            this.rbSalaverry.Text = "Salaverry";
            this.rbSalaverry.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbSalaverry.UseSelectable = true;
            this.rbSalaverry.Visible = false;
            this.rbSalaverry.CheckedChanged += new System.EventHandler(this.rbSalaverry_CheckedChanged);
            // 
            // cboAlmacen
            // 
            this.cboAlmacen.FormattingEnabled = true;
            this.cboAlmacen.ItemHeight = 23;
            this.cboAlmacen.Items.AddRange(new object[] {
            "Almacen Trujillo",
            "Almacen Lima",
            "Almacen Salaverry"});
            this.cboAlmacen.Location = new System.Drawing.Point(358, 24);
            this.cboAlmacen.Name = "cboAlmacen";
            this.cboAlmacen.Size = new System.Drawing.Size(145, 29);
            this.cboAlmacen.Style = MetroFramework.MetroColorStyle.Red;
            this.cboAlmacen.TabIndex = 53;
            this.cboAlmacen.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboAlmacen.UseSelectable = true;
            // 
            // rbLima
            // 
            this.rbLima.AutoSize = true;
            this.rbLima.Location = new System.Drawing.Point(439, 32);
            this.rbLima.Name = "rbLima";
            this.rbLima.Size = new System.Drawing.Size(49, 15);
            this.rbLima.Style = MetroFramework.MetroColorStyle.Red;
            this.rbLima.TabIndex = 18;
            this.rbLima.Text = "Lima";
            this.rbLima.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbLima.UseSelectable = true;
            this.rbLima.Visible = false;
            this.rbLima.CheckedChanged += new System.EventHandler(this.rbLima_CheckedChanged);
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
            this.btnExcel.Location = new System.Drawing.Point(666, 25);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 51;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
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
            this.btnImprimir.Location = new System.Drawing.Point(730, 25);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 52;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // rbTrujillo
            // 
            this.rbTrujillo.AutoSize = true;
            this.rbTrujillo.Checked = true;
            this.rbTrujillo.Location = new System.Drawing.Point(359, 32);
            this.rbTrujillo.Name = "rbTrujillo";
            this.rbTrujillo.Size = new System.Drawing.Size(60, 15);
            this.rbTrujillo.Style = MetroFramework.MetroColorStyle.Red;
            this.rbTrujillo.TabIndex = 17;
            this.rbTrujillo.TabStop = true;
            this.rbTrujillo.Text = "Trujillo";
            this.rbTrujillo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbTrujillo.UseSelectable = true;
            this.rbTrujillo.Visible = false;
            this.rbTrujillo.CheckedChanged += new System.EventHandler(this.rbTrujillo_CheckedChanged);
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
            this.btnBuscar.Location = new System.Drawing.Point(598, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 50;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // gbFecha
            // 
            this.gbFecha.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFecha.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFecha.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFecha.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.BorderThickness = 1F;
            this.gbFecha.Controls.Add(this.lblInicio);
            this.gbFecha.Controls.Add(this.dtpFechaIni);
            this.gbFecha.Controls.Add(this.lblFin);
            this.gbFecha.Controls.Add(this.dtpFechaFin);
            this.gbFecha.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFecha.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.GroupImage = null;
            this.gbFecha.GroupTitle = "Fecha";
            this.gbFecha.Location = new System.Drawing.Point(3, 0);
            this.gbFecha.Name = "gbFecha";
            this.gbFecha.Padding = new System.Windows.Forms.Padding(20);
            this.gbFecha.PaintGroupBox = false;
            this.gbFecha.RoundCorners = 3;
            this.gbFecha.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFecha.ShadowControl = false;
            this.gbFecha.ShadowThickness = 3;
            this.gbFecha.Size = new System.Drawing.Size(335, 73);
            this.gbFecha.TabIndex = 19;
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblInicio.Location = new System.Drawing.Point(9, 32);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(44, 19);
            this.lblInicio.Style = MetroFramework.MetroColorStyle.Red;
            this.lblInicio.TabIndex = 15;
            this.lblInicio.Text = "Inicio:";
            this.lblInicio.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFin.Location = new System.Drawing.Point(168, 32);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(30, 19);
            this.lblFin.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFin.TabIndex = 16;
            this.lblFin.Text = "Fin:";
            this.lblFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(204, 28);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 14;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(804, 475);
            this.dtgvData.TabIndex = 3;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowFooter = true;
            this.dtgvDataView.PrintInitialize += new DevExpress.XtraGrid.Views.Base.PrintInitializeEventHandler(this.dtgvDataView_PrintInitialize);
            // 
            // Facturacion_Contado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 632);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Facturacion_Contado";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Facturación Contado";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Facturacion_Contado_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbFecha.ResumeLayout(false);
            this.gbFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private Grouper gbFecha;
        private MetroFramework.Controls.MetroRadioButton rbLima;
        private MetroFramework.Controls.MetroLabel lblInicio;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroRadioButton rbTrujillo;
        private MetroFramework.Controls.MetroLabel lblFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroComboBox cboAlmacen;
        private MetroFramework.Controls.MetroRadioButton rbSalaverry;
    }
}