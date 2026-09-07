namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class Resumen_Viajes_por_Facturar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resumen_Viajes_por_Facturar));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.gbFiltro = new ReportesTranspesa.Grouper();
            this.rbCompletado = new MetroFramework.Controls.MetroRadioButton();
            this.rbNoReconoceCliente = new MetroFramework.Controls.MetroRadioButton();
            this.rbConOC = new MetroFramework.Controls.MetroRadioButton();
            this.rbPendientes = new MetroFramework.Controls.MetroRadioButton();
            this.rbDocExtraviado = new MetroFramework.Controls.MetroRadioButton();
            this.rbObservado = new MetroFramework.Controls.MetroRadioButton();
            this.rbPorFacturar = new MetroFramework.Controls.MetroRadioButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.gbFecha = new ReportesTranspesa.Grouper();
            this.rbFechaCreacion = new MetroFramework.Controls.MetroRadioButton();
            this.rbFechaProgramacion = new MetroFramework.Controls.MetroRadioButton();
            this.lblInicio = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.lblFin = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dtgvData = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbFiltro.SuspendLayout();
            this.gbFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.gbFiltro);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.gbFecha);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(914, 641);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 0;
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
            this.btnImprimir.Location = new System.Drawing.Point(850, 52);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 60;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // gbFiltro
            // 
            this.gbFiltro.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFiltro.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFiltro.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFiltro.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.BorderThickness = 1F;
            this.gbFiltro.Controls.Add(this.rbCompletado);
            this.gbFiltro.Controls.Add(this.rbNoReconoceCliente);
            this.gbFiltro.Controls.Add(this.rbConOC);
            this.gbFiltro.Controls.Add(this.rbPendientes);
            this.gbFiltro.Controls.Add(this.rbDocExtraviado);
            this.gbFiltro.Controls.Add(this.rbObservado);
            this.gbFiltro.Controls.Add(this.rbPorFacturar);
            this.gbFiltro.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltro.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.GroupImage = null;
            this.gbFiltro.GroupTitle = "Situacion";
            this.gbFiltro.Location = new System.Drawing.Point(230, 3);
            this.gbFiltro.Name = "gbFiltro";
            this.gbFiltro.Padding = new System.Windows.Forms.Padding(20);
            this.gbFiltro.PaintGroupBox = false;
            this.gbFiltro.RoundCorners = 3;
            this.gbFiltro.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFiltro.ShadowControl = false;
            this.gbFiltro.ShadowThickness = 3;
            this.gbFiltro.Size = new System.Drawing.Size(414, 123);
            this.gbFiltro.TabIndex = 57;
            // 
            // rbCompletado
            // 
            this.rbCompletado.AutoSize = true;
            this.rbCompletado.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbCompletado.Location = new System.Drawing.Point(294, 33);
            this.rbCompletado.Name = "rbCompletado";
            this.rbCompletado.Size = new System.Drawing.Size(100, 19);
            this.rbCompletado.Style = MetroFramework.MetroColorStyle.Red;
            this.rbCompletado.TabIndex = 25;
            this.rbCompletado.Text = "Completado";
            this.rbCompletado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbCompletado.UseSelectable = true;
            // 
            // rbNoReconoceCliente
            // 
            this.rbNoReconoceCliente.AutoSize = true;
            this.rbNoReconoceCliente.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbNoReconoceCliente.Location = new System.Drawing.Point(147, 94);
            this.rbNoReconoceCliente.Name = "rbNoReconoceCliente";
            this.rbNoReconoceCliente.Size = new System.Drawing.Size(151, 19);
            this.rbNoReconoceCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.rbNoReconoceCliente.TabIndex = 24;
            this.rbNoReconoceCliente.Text = "No Reconoce Cliente";
            this.rbNoReconoceCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbNoReconoceCliente.UseSelectable = true;
            // 
            // rbConOC
            // 
            this.rbConOC.AutoSize = true;
            this.rbConOC.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbConOC.Location = new System.Drawing.Point(146, 63);
            this.rbConOC.Name = "rbConOC";
            this.rbConOC.Size = new System.Drawing.Size(79, 19);
            this.rbConOC.Style = MetroFramework.MetroColorStyle.Red;
            this.rbConOC.TabIndex = 23;
            this.rbConOC.Text = "Con O/C";
            this.rbConOC.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbConOC.UseSelectable = true;
            // 
            // rbPendientes
            // 
            this.rbPendientes.AutoSize = true;
            this.rbPendientes.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPendientes.Location = new System.Drawing.Point(145, 33);
            this.rbPendientes.Name = "rbPendientes";
            this.rbPendientes.Size = new System.Drawing.Size(121, 19);
            this.rbPendientes.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPendientes.TabIndex = 22;
            this.rbPendientes.Text = "Pendientes O/C";
            this.rbPendientes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPendientes.UseSelectable = true;
            // 
            // rbDocExtraviado
            // 
            this.rbDocExtraviado.AutoSize = true;
            this.rbDocExtraviado.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbDocExtraviado.Location = new System.Drawing.Point(14, 63);
            this.rbDocExtraviado.Name = "rbDocExtraviado";
            this.rbDocExtraviado.Size = new System.Drawing.Size(116, 19);
            this.rbDocExtraviado.Style = MetroFramework.MetroColorStyle.Red;
            this.rbDocExtraviado.TabIndex = 21;
            this.rbDocExtraviado.Text = "Doc Extraviado";
            this.rbDocExtraviado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbDocExtraviado.UseSelectable = true;
            // 
            // rbObservado
            // 
            this.rbObservado.AutoSize = true;
            this.rbObservado.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbObservado.Location = new System.Drawing.Point(14, 94);
            this.rbObservado.Name = "rbObservado";
            this.rbObservado.Size = new System.Drawing.Size(92, 19);
            this.rbObservado.Style = MetroFramework.MetroColorStyle.Red;
            this.rbObservado.TabIndex = 20;
            this.rbObservado.Text = "Observado";
            this.rbObservado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbObservado.UseSelectable = true;
            // 
            // rbPorFacturar
            // 
            this.rbPorFacturar.AutoSize = true;
            this.rbPorFacturar.Checked = true;
            this.rbPorFacturar.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPorFacturar.Location = new System.Drawing.Point(14, 32);
            this.rbPorFacturar.Name = "rbPorFacturar";
            this.rbPorFacturar.Size = new System.Drawing.Size(100, 19);
            this.rbPorFacturar.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPorFacturar.TabIndex = 19;
            this.rbPorFacturar.TabStop = true;
            this.rbPorFacturar.Text = "Por Facturar";
            this.rbPorFacturar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPorFacturar.UseSelectable = true;
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
            this.btnExcel.Location = new System.Drawing.Point(776, 52);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 59;
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
            this.btnBuscar.Location = new System.Drawing.Point(705, 52);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 58;
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
            this.gbFecha.Controls.Add(this.rbFechaCreacion);
            this.gbFecha.Controls.Add(this.rbFechaProgramacion);
            this.gbFecha.Controls.Add(this.lblInicio);
            this.gbFecha.Controls.Add(this.dtpFechaIni);
            this.gbFecha.Controls.Add(this.lblFin);
            this.gbFecha.Controls.Add(this.dtpFechaFin);
            this.gbFecha.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFecha.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFecha.GroupImage = null;
            this.gbFecha.GroupTitle = "Fecha";
            this.gbFecha.Location = new System.Drawing.Point(17, 2);
            this.gbFecha.Name = "gbFecha";
            this.gbFecha.Padding = new System.Windows.Forms.Padding(20);
            this.gbFecha.PaintGroupBox = false;
            this.gbFecha.RoundCorners = 3;
            this.gbFecha.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFecha.ShadowControl = false;
            this.gbFecha.ShadowThickness = 3;
            this.gbFecha.Size = new System.Drawing.Size(206, 123);
            this.gbFecha.TabIndex = 56;
            // 
            // rbFechaCreacion
            // 
            this.rbFechaCreacion.AutoSize = true;
            this.rbFechaCreacion.Location = new System.Drawing.Point(120, 98);
            this.rbFechaCreacion.Name = "rbFechaCreacion";
            this.rbFechaCreacion.Size = new System.Drawing.Size(79, 15);
            this.rbFechaCreacion.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaCreacion.TabIndex = 18;
            this.rbFechaCreacion.Text = "F.Creación";
            this.rbFechaCreacion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaCreacion.UseSelectable = true;
            // 
            // rbFechaProgramacion
            // 
            this.rbFechaProgramacion.AutoSize = true;
            this.rbFechaProgramacion.Checked = true;
            this.rbFechaProgramacion.Location = new System.Drawing.Point(12, 98);
            this.rbFechaProgramacion.Name = "rbFechaProgramacion";
            this.rbFechaProgramacion.Size = new System.Drawing.Size(107, 15);
            this.rbFechaProgramacion.Style = MetroFramework.MetroColorStyle.Red;
            this.rbFechaProgramacion.TabIndex = 17;
            this.rbFechaProgramacion.TabStop = true;
            this.rbFechaProgramacion.Text = "F.Programación";
            this.rbFechaProgramacion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbFechaProgramacion.UseSelectable = true;
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
            this.lblFin.Location = new System.Drawing.Point(22, 68);
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
            this.dtpFechaFin.Location = new System.Drawing.Point(59, 63);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 14;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dtgvData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartControl1);
            this.splitContainer2.Size = new System.Drawing.Size(914, 489);
            this.splitContainer2.SplitterDistance = 258;
            this.splitContainer2.TabIndex = 0;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.OptionsPrint.PageSettings.Landscape = true;
            this.dtgvData.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.dtgvData.Size = new System.Drawing.Size(914, 258);
            this.dtgvData.TabIndex = 2;
            // 
            // chartControl1
            // 
            this.chartControl1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.LookAndFeel.SkinName = "Darkroom";
            this.chartControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(914, 227);
            this.chartControl1.TabIndex = 1;
            // 
            // Resumen_Viajes_por_Facturar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(954, 721);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Resumen_Viajes_por_Facturar";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Resumen Viajes por Facturar";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Resumen_Viajes_por_Facturar_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbFiltro.ResumeLayout(false);
            this.gbFiltro.PerformLayout();
            this.gbFecha.ResumeLayout(false);
            this.gbFecha.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private Grouper gbFiltro;
        private MetroFramework.Controls.MetroRadioButton rbCompletado;
        private MetroFramework.Controls.MetroRadioButton rbNoReconoceCliente;
        private MetroFramework.Controls.MetroRadioButton rbConOC;
        private MetroFramework.Controls.MetroRadioButton rbPendientes;
        private MetroFramework.Controls.MetroRadioButton rbDocExtraviado;
        private MetroFramework.Controls.MetroRadioButton rbObservado;
        private MetroFramework.Controls.MetroRadioButton rbPorFacturar;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private Grouper gbFecha;
        private MetroFramework.Controls.MetroRadioButton rbFechaCreacion;
        private MetroFramework.Controls.MetroRadioButton rbFechaProgramacion;
        private MetroFramework.Controls.MetroLabel lblInicio;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel lblFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraPivotGrid.PivotGridControl dtgvData;
        private DevExpress.XtraCharts.ChartControl chartControl1;

    }
}