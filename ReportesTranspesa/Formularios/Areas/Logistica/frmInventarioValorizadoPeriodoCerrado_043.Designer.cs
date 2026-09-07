namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    partial class frmInventarioValorizadoPeriodoCerrado_043
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventarioValorizadoPeriodoCerrado_043));
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbxAlmacen2 = new System.Windows.Forms.ComboBox();
            this.dtpPeriodo2 = new System.Windows.Forms.DateTimePicker();
            this.btnExcelDetalle = new DevExpress.XtraEditors.SimpleButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAlmacen = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCompania = new System.Windows.Forms.ComboBox();
            this.cbxCompania2 = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.crvReporte = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.GridControl = this.dtgvData;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.LevelTemplate = this.bandedGridView1;
            gridLevelNode2.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(467, 421);
            this.dtgvData.TabIndex = 3;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView,
            this.bandedGridView1});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvDataView.OptionsSelection.MultiSelect = true;
            this.dtgvDataView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowFooter = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbxAlmacen2);
            this.splitContainer1.Panel1.Controls.Add(this.dtpPeriodo2);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcelDetalle);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cbxAlmacen);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cbxCompania);
            this.splitContainer1.Panel1.Controls.Add(this.cbxCompania2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(978, 478);
            this.splitContainer1.SplitterDistance = 53;
            this.splitContainer1.TabIndex = 0;
            // 
            // cbxAlmacen2
            // 
            this.cbxAlmacen2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxAlmacen2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAlmacen2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAlmacen2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAlmacen2.FormattingEnabled = true;
            this.cbxAlmacen2.Location = new System.Drawing.Point(131, 29);
            this.cbxAlmacen2.Name = "cbxAlmacen2";
            this.cbxAlmacen2.Size = new System.Drawing.Size(255, 21);
            this.cbxAlmacen2.TabIndex = 117;
            this.cbxAlmacen2.SelectedIndexChanged += new System.EventHandler(this.cbxAlmacen2_SelectedIndexChanged);
            this.cbxAlmacen2.DropDownClosed += new System.EventHandler(this.cbxAlmacen2_DropDownClosed);
            // 
            // dtpPeriodo2
            // 
            this.dtpPeriodo2.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtpPeriodo2.CalendarForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtpPeriodo2.CustomFormat = "yyyyMM";
            this.dtpPeriodo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtpPeriodo2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodo2.Location = new System.Drawing.Point(413, 27);
            this.dtpPeriodo2.Name = "dtpPeriodo2";
            this.dtpPeriodo2.ShowUpDown = true;
            this.dtpPeriodo2.Size = new System.Drawing.Size(76, 20);
            this.dtpPeriodo2.TabIndex = 115;
            this.dtpPeriodo2.Value = new System.DateTime(2022, 10, 31, 0, 0, 0, 0);
            this.dtpPeriodo2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpPeriodo2_KeyPress);
            // 
            // btnExcelDetalle
            // 
            this.btnExcelDetalle.Appearance.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.btnExcelDetalle.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnExcelDetalle.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnExcelDetalle.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelDetalle.Appearance.Options.UseBackColor = true;
            this.btnExcelDetalle.Appearance.Options.UseBorderColor = true;
            this.btnExcelDetalle.Appearance.Options.UseFont = true;
            this.btnExcelDetalle.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcelDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelDetalle.Image")));
            this.btnExcelDetalle.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcelDetalle.Location = new System.Drawing.Point(709, 11);
            this.btnExcelDetalle.Name = "btnExcelDetalle";
            this.btnExcelDetalle.Size = new System.Drawing.Size(40, 38);
            this.btnExcelDetalle.TabIndex = 114;
            this.btnExcelDetalle.Text = "Detalle";
            this.btnExcelDetalle.ToolTip = "Exportar a Excel";
            this.btnExcelDetalle.Click += new System.EventHandler(this.btnExcelDetalle_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(605, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Ver Reporte";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(524, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Seleccione Periodo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Seleccione Almacén:";
            // 
            // cbxAlmacen
            // 
            this.cbxAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAlmacen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxAlmacen.FormattingEnabled = true;
            this.cbxAlmacen.Items.AddRange(new object[] {
            "Almacén Trujillo              ",
            "Almacén Servicios             ",
            "Almacén Encalada              ",
            "Almacén Lima                  ",
            "Almacén Mina-Trujillo         ",
            "Almacén Mina- Lima            ",
            "Almacén Commodities           ",
            "Almacén para Herramientas     ",
            "Almacen inmovilizado          ",
            "Almacen Salaverry             ",
            "Almacen Principal             ",
            "ALMACEN ALTRA                 ",
            "Almacen Ventas Trujillo       ",
            "Almacén Ventas Lima           ",
            "Bra Commodities               ",
            "Bra Materia Prima Encalada    ",
            "Bra Materia Prima Trujillo    ",
            "Bra Produccion Trujillo       ",
            "Bra Product Terminado Trujillo",
            "Consiganción  Consumo         ",
            "Metalpren - LIMA              ",
            "SAVSA LIMA                    ",
            "MERC. TRANSITO LIMA           ",
            "MERC. TRANSITO RAZURI         ",
            "Almacén de Desechos           ",
            "Almacén de Artículos Extra    ",
            "Fadesa - TRUJILLO             ",
            "Metalpren - TRUJILLO          ",
            "SAVSA TRUJILLO                ",
            "MERC. TRANSITO TRUJILLO"});
            this.cbxAlmacen.Location = new System.Drawing.Point(731, 30);
            this.cbxAlmacen.Name = "cbxAlmacen";
            this.cbxAlmacen.Size = new System.Drawing.Size(171, 21);
            this.cbxAlmacen.TabIndex = 2;
            this.cbxAlmacen.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione Compañia:";
            // 
            // cbxCompania
            // 
            this.cbxCompania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCompania.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxCompania.FormattingEnabled = true;
            this.cbxCompania.Items.AddRange(new object[] {
            "GRUPO TRANSPESA",
            "FABRICACIONES BRA",
            "ALMACENES ALTRA",
            "CONSORCIO AMT",
            "AGENCIA DE ADUANAS"});
            this.cbxCompania.Location = new System.Drawing.Point(733, 6);
            this.cbxCompania.Name = "cbxCompania";
            this.cbxCompania.Size = new System.Drawing.Size(171, 21);
            this.cbxCompania.TabIndex = 0;
            this.cbxCompania.Visible = false;
            // 
            // cbxCompania2
            // 
            this.cbxCompania2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCompania2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCompania2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCompania2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCompania2.FormattingEnabled = true;
            this.cbxCompania2.Location = new System.Drawing.Point(131, 6);
            this.cbxCompania2.Name = "cbxCompania2";
            this.cbxCompania2.Size = new System.Drawing.Size(255, 21);
            this.cbxCompania2.TabIndex = 116;
            this.cbxCompania2.SelectedIndexChanged += new System.EventHandler(this.cbxCompania2_SelectedIndexChanged);
            this.cbxCompania2.DropDownClosed += new System.EventHandler(this.cbxCompania2_DropDownClosed);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dtgvData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.crvReporte);
            this.splitContainer2.Size = new System.Drawing.Size(978, 421);
            this.splitContainer2.SplitterDistance = 467;
            this.splitContainer2.TabIndex = 0;
            // 
            // crvReporte
            // 
            this.crvReporte.ActiveViewIndex = -1;
            this.crvReporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReporte.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReporte.Location = new System.Drawing.Point(0, 0);
            this.crvReporte.Name = "crvReporte";
            this.crvReporte.ShowGotoPageButton = false;
            this.crvReporte.ShowGroupTreeButton = false;
            this.crvReporte.ShowLogo = false;
            this.crvReporte.ShowParameterPanelButton = false;
            this.crvReporte.Size = new System.Drawing.Size(507, 421);
            this.crvReporte.TabIndex = 1;
            this.crvReporte.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmInventarioValorizadoPeriodoCerrado_043
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(978, 478);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmInventarioValorizadoPeriodoCerrado_043";
            this.Text = "InventarioValorizadoPeriodoCerrado_043";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInventarioValorizadoPeriodoCerrado_043_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAlmacen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCompania;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReporte;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraEditors.SimpleButton btnExcelDetalle;
        private System.Windows.Forms.DateTimePicker dtpPeriodo2;
        private System.Windows.Forms.ComboBox cbxAlmacen2;
        private System.Windows.Forms.ComboBox cbxCompania2;
    }
}