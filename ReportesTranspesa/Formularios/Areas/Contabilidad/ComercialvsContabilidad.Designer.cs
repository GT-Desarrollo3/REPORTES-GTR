namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class ComercialvsContabilidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComercialvsContabilidad));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbFiltroMoneda = new ReportesTranspesa.Grouper();
            this.rbPorCliente = new MetroFramework.Controls.MetroRadioButton();
            this.rbPorFacturador = new MetroFramework.Controls.MetroRadioButton();
            this.gbFiltro = new ReportesTranspesa.Grouper();
            this.chkProveedor = new MetroFramework.Controls.MetroCheckBox();
            this.chkInvoice = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.lblProveedor = new MetroFramework.Controls.MetroLabel();
            this.txtInvoice = new MetroFramework.Controls.MetroTextBox();
            this.txtProveedor = new MetroFramework.Controls.MetroTextBox();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPeriodoFin = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lblPeriodo = new MetroFramework.Controls.MetroLabel();
            this.txtPeriodoIni = new MetroFramework.Controls.MetroTextBox();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbFiltroMoneda.SuspendLayout();
            this.gbFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.gbFiltroMoneda);
            this.splitContainer1.Panel1.Controls.Add(this.gbFiltro);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.lblProveedor);
            this.splitContainer1.Panel1.Controls.Add(this.txtInvoice);
            this.splitContainer1.Panel1.Controls.Add(this.txtProveedor);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.txtPeriodoFin);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.lblPeriodo);
            this.splitContainer1.Panel1.Controls.Add(this.txtPeriodoIni);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pivotGridControl1);
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(1190, 334);
            this.splitContainer1.SplitterDistance = 87;
            this.splitContainer1.TabIndex = 0;
            // 
            // gbFiltroMoneda
            // 
            this.gbFiltroMoneda.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFiltroMoneda.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFiltroMoneda.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFiltroMoneda.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltroMoneda.BorderThickness = 1F;
            this.gbFiltroMoneda.Controls.Add(this.rbPorCliente);
            this.gbFiltroMoneda.Controls.Add(this.rbPorFacturador);
            this.gbFiltroMoneda.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFiltroMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltroMoneda.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltroMoneda.GroupImage = null;
            this.gbFiltroMoneda.GroupTitle = "Filtro Moneda";
            this.gbFiltroMoneda.Location = new System.Drawing.Point(553, 15);
            this.gbFiltroMoneda.Name = "gbFiltroMoneda";
            this.gbFiltroMoneda.Padding = new System.Windows.Forms.Padding(20);
            this.gbFiltroMoneda.PaintGroupBox = false;
            this.gbFiltroMoneda.RoundCorners = 3;
            this.gbFiltroMoneda.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFiltroMoneda.ShadowControl = false;
            this.gbFiltroMoneda.ShadowThickness = 3;
            this.gbFiltroMoneda.Size = new System.Drawing.Size(167, 61);
            this.gbFiltroMoneda.TabIndex = 123;
            // 
            // rbPorCliente
            // 
            this.rbPorCliente.AutoSize = true;
            this.rbPorCliente.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPorCliente.Location = new System.Drawing.Point(83, 32);
            this.rbPorCliente.Name = "rbPorCliente";
            this.rbPorCliente.Size = new System.Drawing.Size(71, 19);
            this.rbPorCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPorCliente.TabIndex = 21;
            this.rbPorCliente.Text = "Dolares";
            this.rbPorCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPorCliente.UseSelectable = true;
            // 
            // rbPorFacturador
            // 
            this.rbPorFacturador.AutoSize = true;
            this.rbPorFacturador.Checked = true;
            this.rbPorFacturador.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rbPorFacturador.Location = new System.Drawing.Point(14, 32);
            this.rbPorFacturador.Name = "rbPorFacturador";
            this.rbPorFacturador.Size = new System.Drawing.Size(56, 19);
            this.rbPorFacturador.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPorFacturador.TabIndex = 19;
            this.rbPorFacturador.TabStop = true;
            this.rbPorFacturador.Text = "Soles";
            this.rbPorFacturador.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPorFacturador.UseSelectable = true;
            // 
            // gbFiltro
            // 
            this.gbFiltro.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFiltro.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFiltro.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFiltro.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.BorderThickness = 1F;
            this.gbFiltro.Controls.Add(this.chkProveedor);
            this.gbFiltro.Controls.Add(this.chkInvoice);
            this.gbFiltro.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltro.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.GroupImage = null;
            this.gbFiltro.GroupTitle = "Filtrar por";
            this.gbFiltro.Location = new System.Drawing.Point(336, 15);
            this.gbFiltro.Name = "gbFiltro";
            this.gbFiltro.Padding = new System.Windows.Forms.Padding(20);
            this.gbFiltro.PaintGroupBox = false;
            this.gbFiltro.RoundCorners = 3;
            this.gbFiltro.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFiltro.ShadowControl = false;
            this.gbFiltro.ShadowThickness = 3;
            this.gbFiltro.Size = new System.Drawing.Size(211, 61);
            this.gbFiltro.TabIndex = 122;
            this.gbFiltro.Visible = false;
            // 
            // chkProveedor
            // 
            this.chkProveedor.AutoSize = true;
            this.chkProveedor.Checked = true;
            this.chkProveedor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProveedor.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkProveedor.Location = new System.Drawing.Point(23, 31);
            this.chkProveedor.Name = "chkProveedor";
            this.chkProveedor.Size = new System.Drawing.Size(88, 19);
            this.chkProveedor.Style = MetroFramework.MetroColorStyle.Red;
            this.chkProveedor.TabIndex = 118;
            this.chkProveedor.Text = "Proveedor";
            this.chkProveedor.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkProveedor.UseSelectable = true;
            this.chkProveedor.CheckedChanged += new System.EventHandler(this.chkProveedor_CheckedChanged);
            // 
            // chkInvoice
            // 
            this.chkInvoice.AutoSize = true;
            this.chkInvoice.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkInvoice.Location = new System.Drawing.Point(128, 31);
            this.chkInvoice.Name = "chkInvoice";
            this.chkInvoice.Size = new System.Drawing.Size(72, 19);
            this.chkInvoice.Style = MetroFramework.MetroColorStyle.Red;
            this.chkInvoice.TabIndex = 119;
            this.chkInvoice.Text = "Invoice ";
            this.chkInvoice.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkInvoice.UseSelectable = true;
            this.chkInvoice.CheckedChanged += new System.EventHandler(this.chkInvoice_CheckedChanged);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(29, 86);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(63, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 121;
            this.metroLabel2.Text = "Invoice : ";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel2.Visible = false;
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblProveedor.Location = new System.Drawing.Point(15, 51);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(79, 19);
            this.lblProveedor.Style = MetroFramework.MetroColorStyle.Red;
            this.lblProveedor.TabIndex = 120;
            this.lblProveedor.Text = "Proveedor :";
            this.lblProveedor.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lblProveedor.Visible = false;
            // 
            // txtInvoice
            // 
            this.txtInvoice.Lines = new string[0];
            this.txtInvoice.Location = new System.Drawing.Point(100, 82);
            this.txtInvoice.MaxLength = 32767;
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.PasswordChar = '\0';
            this.txtInvoice.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtInvoice.SelectedText = "";
            this.txtInvoice.Size = new System.Drawing.Size(141, 29);
            this.txtInvoice.Style = MetroFramework.MetroColorStyle.Red;
            this.txtInvoice.TabIndex = 116;
            this.txtInvoice.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtInvoice.UseSelectable = true;
            this.txtInvoice.Visible = false;
            // 
            // txtProveedor
            // 
            this.txtProveedor.Lines = new string[0];
            this.txtProveedor.Location = new System.Drawing.Point(100, 47);
            this.txtProveedor.MaxLength = 32767;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.PasswordChar = '\0';
            this.txtProveedor.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProveedor.SelectedText = "";
            this.txtProveedor.Size = new System.Drawing.Size(220, 29);
            this.txtProveedor.Style = MetroFramework.MetroColorStyle.Red;
            this.txtProveedor.TabIndex = 114;
            this.txtProveedor.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtProveedor.UseSelectable = true;
            this.txtProveedor.Visible = false;
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
            this.btnImprimir.Location = new System.Drawing.Point(1132, 17);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 112;
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
            this.btnExcel.Location = new System.Drawing.Point(1053, 17);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 111;
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
            this.btnBuscar.Location = new System.Drawing.Point(974, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 110;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtPeriodoFin
            // 
            this.txtPeriodoFin.Lines = new string[0];
            this.txtPeriodoFin.Location = new System.Drawing.Point(258, 12);
            this.txtPeriodoFin.MaxLength = 6;
            this.txtPeriodoFin.Name = "txtPeriodoFin";
            this.txtPeriodoFin.PasswordChar = '\0';
            this.txtPeriodoFin.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPeriodoFin.SelectedText = "";
            this.txtPeriodoFin.Size = new System.Drawing.Size(62, 29);
            this.txtPeriodoFin.Style = MetroFramework.MetroColorStyle.Red;
            this.txtPeriodoFin.TabIndex = 109;
            this.txtPeriodoFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtPeriodoFin.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(197, 17);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(55, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 108;
            this.metroLabel1.Text = "Hasta : ";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPeriodo.Location = new System.Drawing.Point(14, 17);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(109, 19);
            this.lblPeriodo.Style = MetroFramework.MetroColorStyle.Red;
            this.lblPeriodo.TabIndex = 107;
            this.lblPeriodo.Text = "Periodo Desde : ";
            this.lblPeriodo.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtPeriodoIni
            // 
            this.txtPeriodoIni.Lines = new string[0];
            this.txtPeriodoIni.Location = new System.Drawing.Point(122, 12);
            this.txtPeriodoIni.MaxLength = 6;
            this.txtPeriodoIni.Name = "txtPeriodoIni";
            this.txtPeriodoIni.PasswordChar = '\0';
            this.txtPeriodoIni.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPeriodoIni.SelectedText = "";
            this.txtPeriodoIni.Size = new System.Drawing.Size(62, 29);
            this.txtPeriodoIni.Style = MetroFramework.MetroColorStyle.Red;
            this.txtPeriodoIni.TabIndex = 106;
            this.txtPeriodoIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtPeriodoIni.UseSelectable = true;
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.LookAndFeel.SkinName = "Darkroom";
            this.pivotGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsPrint.PageSettings.Landscape = true;
            this.pivotGridControl1.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.pivotGridControl1.Size = new System.Drawing.Size(1190, 243);
            this.pivotGridControl1.TabIndex = 4;
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
            this.dtgvData.Size = new System.Drawing.Size(1190, 243);
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
            this.dtgvDataView.OptionsSelection.MultiSelect = true;
            this.dtgvDataView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            // 
            // ComercialvsContabilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 414);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ComercialvsContabilidad";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Comercial vs Contabilidad";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ComercialvsContabilidad_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbFiltroMoneda.ResumeLayout(false);
            this.gbFiltroMoneda.PerformLayout();
            this.gbFiltro.ResumeLayout(false);
            this.gbFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroTextBox txtPeriodoIni;
        private MetroFramework.Controls.MetroLabel lblPeriodo;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtPeriodoFin;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private MetroFramework.Controls.MetroTextBox txtProveedor;
        private MetroFramework.Controls.MetroTextBox txtInvoice;
        private MetroFramework.Controls.MetroCheckBox chkInvoice;
        private MetroFramework.Controls.MetroCheckBox chkProveedor;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel lblProveedor;
        private Grouper gbFiltro;
        private Grouper gbFiltroMoneda;
        private MetroFramework.Controls.MetroRadioButton rbPorCliente;
        private MetroFramework.Controls.MetroRadioButton rbPorFacturador;
    }
}