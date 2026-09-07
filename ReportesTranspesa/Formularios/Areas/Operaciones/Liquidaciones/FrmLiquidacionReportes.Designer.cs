namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class FrmLiquidacionReportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLiquidacionReportes));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.cbxTipoReporte = new System.Windows.Forms.ComboBox();
            this.gbFecha = new System.Windows.Forms.GroupBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnReportar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.gbTipoReporte = new System.Windows.Forms.GroupBox();
            this.dgvLiquidaciones = new DevExpress.XtraGrid.GridControl();
            this.dtgvDespachosDiariosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1.SuspendLayout();
            this.gbFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbTipoReporte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiquidaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDespachosDiariosView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DarkKhaki;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsBtnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(960, 35);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(457, 32);
            this.toolStripLabel1.Text = "REPORTE LIQUIDACIÓN DE PLANILLAS";
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(49, 32);
            this.tsBtnSalir.Text = "Salir";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // cbxTipoReporte
            // 
            this.cbxTipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoReporte.FormattingEnabled = true;
            this.cbxTipoReporte.Items.AddRange(new object[] {
            "Reporte Diario de Planillas",
            "Reporte Planillas por Liquidar"});
            this.cbxTipoReporte.Location = new System.Drawing.Point(6, 17);
            this.cbxTipoReporte.Name = "cbxTipoReporte";
            this.cbxTipoReporte.Size = new System.Drawing.Size(302, 20);
            this.cbxTipoReporte.TabIndex = 15;
            // 
            // gbFecha
            // 
            this.gbFecha.Controls.Add(this.dtpFecha);
            this.gbFecha.Location = new System.Drawing.Point(352, 11);
            this.gbFecha.Name = "gbFecha";
            this.gbFecha.Size = new System.Drawing.Size(128, 46);
            this.gbFecha.TabIndex = 40;
            this.gbFecha.TabStop = false;
            this.gbFecha.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.CalendarMonthBackground = System.Drawing.SystemColors.MenuHighlight;
            this.dtpFecha.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(11, 17);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(103, 20);
            this.dtpFecha.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnReportar);
            this.splitContainer1.Panel1.Controls.Add(this.btnExportar);
            this.splitContainer1.Panel1.Controls.Add(this.gbTipoReporte);
            this.splitContainer1.Panel1.Controls.Add(this.gbFecha);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvLiquidaciones);
            this.splitContainer1.Size = new System.Drawing.Size(960, 538);
            this.splitContainer1.SplitterDistance = 63;
            this.splitContainer1.TabIndex = 41;
            // 
            // btnReportar
            // 
            this.btnReportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReportar.Location = new System.Drawing.Point(789, 28);
            this.btnReportar.Name = "btnReportar";
            this.btnReportar.Size = new System.Drawing.Size(75, 29);
            this.btnReportar.TabIndex = 43;
            this.btnReportar.Text = "Reportar";
            this.btnReportar.UseVisualStyleBackColor = true;
            this.btnReportar.Click += new System.EventHandler(this.btnReportar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(870, 28);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 29);
            this.btnExportar.TabIndex = 42;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // gbTipoReporte
            // 
            this.gbTipoReporte.Controls.Add(this.cbxTipoReporte);
            this.gbTipoReporte.Location = new System.Drawing.Point(12, 11);
            this.gbTipoReporte.Name = "gbTipoReporte";
            this.gbTipoReporte.Size = new System.Drawing.Size(325, 46);
            this.gbTipoReporte.TabIndex = 41;
            this.gbTipoReporte.TabStop = false;
            this.gbTipoReporte.Text = "Tipo Reporte";
            // 
            // dgvLiquidaciones
            // 
            this.dgvLiquidaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dgvLiquidaciones.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dgvLiquidaciones.Location = new System.Drawing.Point(0, 0);
            this.dgvLiquidaciones.LookAndFeel.SkinName = "Blue";
            this.dgvLiquidaciones.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgvLiquidaciones.MainView = this.dtgvDespachosDiariosView;
            this.dgvLiquidaciones.Name = "dgvLiquidaciones";
            this.dgvLiquidaciones.Size = new System.Drawing.Size(960, 471);
            this.dgvLiquidaciones.TabIndex = 7;
            this.dgvLiquidaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDespachosDiariosView});
            // 
            // dtgvDespachosDiariosView
            // 
            gridFormatRule1.Name = "Format0";
            formatConditionIconSet1.CategoryName = "Ratings";
            formatConditionIconSetIcon1.PredefinedName = "TrafficLights3_3.png";
            formatConditionIconSetIcon1.Value = new decimal(new int[] {
            67,
            0,
            0,
            0});
            formatConditionIconSetIcon1.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon2.PredefinedName = "TrafficLights3_1.png";
            formatConditionIconSetIcon2.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
            formatConditionIconSetIcon2.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon3.PredefinedName = "Stars3_3.png";
            formatConditionIconSetIcon3.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon1);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon2);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon3);
            formatConditionIconSet1.Name = "Stars3";
            formatConditionIconSet1.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet1.IconSet = formatConditionIconSet1;
            gridFormatRule1.Rule = formatConditionRuleIconSet1;
            this.dtgvDespachosDiariosView.FormatRules.Add(gridFormatRule1);
            this.dtgvDespachosDiariosView.GridControl = this.dgvLiquidaciones;
            this.dtgvDespachosDiariosView.Name = "dtgvDespachosDiariosView";
            this.dtgvDespachosDiariosView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDespachosDiariosView.OptionsBehavior.Editable = false;
            this.dtgvDespachosDiariosView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvDespachosDiariosView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDespachosDiariosView.OptionsView.ShowFooter = true;
            // 
            // FrmLiquidacionReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(960, 573);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmLiquidacionReportes";
            this.Text = "FrmLiquidacionReportes";
            this.Load += new System.EventHandler(this.FrmLiquidacionReportes_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbFecha.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbTipoReporte.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiquidaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDespachosDiariosView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        internal System.Windows.Forms.ComboBox cbxTipoReporte;
        private System.Windows.Forms.GroupBox gbFecha;
        internal System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnReportar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.GroupBox gbTipoReporte;
        private DevExpress.XtraGrid.GridControl dgvLiquidaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDespachosDiariosView;
    }
}