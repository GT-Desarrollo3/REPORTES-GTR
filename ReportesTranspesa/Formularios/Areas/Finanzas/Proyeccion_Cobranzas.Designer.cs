namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    partial class Proyeccion_Cobranzas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Proyeccion_Cobranzas));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblCompañia = new MetroFramework.Controls.MetroLabel();
            this.chkCompania = new MetroFramework.Controls.MetroCheckBox();
            this.cboCompañia = new MetroFramework.Controls.MetroComboBox();
            this.btnResumen = new MetroFramework.Controls.MetroButton();
            this.txtPendientes = new MetroFramework.Controls.MetroTextBox();
            this.lblPendiente = new MetroFramework.Controls.MetroLabel();
            this.lvCliente = new System.Windows.Forms.ListView();
            this.lblCliente = new MetroFramework.Controls.MetroLabel();
            this.txtCliente = new MetroFramework.Controls.MetroTextBox();
            this.lblIni = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.lblFin = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvDataResumen = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataResumen)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.lblCompañia);
            this.splitContainer1.Panel1.Controls.Add(this.chkCompania);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompañia);
            this.splitContainer1.Panel1.Controls.Add(this.btnResumen);
            this.splitContainer1.Panel1.Controls.Add(this.txtPendientes);
            this.splitContainer1.Panel1.Controls.Add(this.lblPendiente);
            this.splitContainer1.Panel1.Controls.Add(this.lvCliente);
            this.splitContainer1.Panel1.Controls.Add(this.lblCliente);
            this.splitContainer1.Panel1.Controls.Add(this.txtCliente);
            this.splitContainer1.Panel1.Controls.Add(this.lblIni);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.lblFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvDataResumen);
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(1296, 342);
            this.splitContainer1.SplitterDistance = 74;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblCompañia
            // 
            this.lblCompañia.AutoSize = true;
            this.lblCompañia.Cursor = System.Windows.Forms.Cursors.No;
            this.lblCompañia.Enabled = false;
            this.lblCompañia.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblCompañia.Location = new System.Drawing.Point(25, 24);
            this.lblCompañia.Name = "lblCompañia";
            this.lblCompañia.Size = new System.Drawing.Size(78, 19);
            this.lblCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.lblCompañia.TabIndex = 128;
            this.lblCompañia.Text = "Compañia: ";
            this.lblCompañia.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // chkCompania
            // 
            this.chkCompania.AutoSize = true;
            this.chkCompania.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkCompania.Location = new System.Drawing.Point(10, 24);
            this.chkCompania.Name = "chkCompania";
            this.chkCompania.Size = new System.Drawing.Size(90, 19);
            this.chkCompania.Style = MetroFramework.MetroColorStyle.Red;
            this.chkCompania.TabIndex = 127;
            this.chkCompania.Text = "Compañía:";
            this.chkCompania.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkCompania.UseSelectable = true;
            this.chkCompania.Visible = false;
            this.chkCompania.CheckedChanged += new System.EventHandler(this.chkCompania_CheckedChanged);
            // 
            // cboCompañia
            // 
            this.cboCompañia.Enabled = false;
            this.cboCompañia.FormattingEnabled = true;
            this.cboCompañia.ItemHeight = 23;
            this.cboCompañia.Items.AddRange(new object[] {
            "TRANSPESA",
            "BRA",
            "ALTRA"});
            this.cboCompañia.Location = new System.Drawing.Point(106, 20);
            this.cboCompañia.Name = "cboCompañia";
            this.cboCompañia.Size = new System.Drawing.Size(121, 29);
            this.cboCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCompañia.TabIndex = 126;
            this.cboCompañia.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboCompañia.UseSelectable = true;
            // 
            // btnResumen
            // 
            this.btnResumen.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnResumen.Location = new System.Drawing.Point(1204, 30);
            this.btnResumen.Name = "btnResumen";
            this.btnResumen.Size = new System.Drawing.Size(81, 23);
            this.btnResumen.Style = MetroFramework.MetroColorStyle.Red;
            this.btnResumen.TabIndex = 125;
            this.btnResumen.Text = "Resumen";
            this.btnResumen.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnResumen.UseSelectable = true;
            this.btnResumen.Click += new System.EventHandler(this.btnResumen_Click);
            // 
            // txtPendientes
            // 
            this.txtPendientes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPendientes.Lines = new string[0];
            this.txtPendientes.Location = new System.Drawing.Point(151, 55);
            this.txtPendientes.MaxLength = 32767;
            this.txtPendientes.Name = "txtPendientes";
            this.txtPendientes.PasswordChar = '\0';
            this.txtPendientes.ReadOnly = true;
            this.txtPendientes.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPendientes.SelectedText = "";
            this.txtPendientes.Size = new System.Drawing.Size(43, 29);
            this.txtPendientes.Style = MetroFramework.MetroColorStyle.Red;
            this.txtPendientes.TabIndex = 123;
            this.txtPendientes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtPendientes.UseSelectable = true;
            // 
            // lblPendiente
            // 
            this.lblPendiente.AutoSize = true;
            this.lblPendiente.Cursor = System.Windows.Forms.Cursors.No;
            this.lblPendiente.Enabled = false;
            this.lblPendiente.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPendiente.Location = new System.Drawing.Point(6, 55);
            this.lblPendiente.Name = "lblPendiente";
            this.lblPendiente.Size = new System.Drawing.Size(134, 19);
            this.lblPendiente.Style = MetroFramework.MetroColorStyle.Red;
            this.lblPendiente.TabIndex = 116;
            this.lblPendiente.Text = "Facturas Pendientes:";
            this.lblPendiente.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lvCliente
            // 
            this.lvCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.lvCliente.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lvCliente.FullRowSelect = true;
            this.lvCliente.GridLines = true;
            this.lvCliente.Location = new System.Drawing.Point(627, 55);
            this.lvCliente.MultiSelect = false;
            this.lvCliente.Name = "lvCliente";
            this.lvCliente.Size = new System.Drawing.Size(405, 261);
            this.lvCliente.TabIndex = 122;
            this.lvCliente.UseCompatibleStateImageBehavior = false;
            this.lvCliente.View = System.Windows.Forms.View.Details;
            this.lvCliente.Visible = false;
            this.lvCliente.Enter += new System.EventHandler(this.lvCliente_Enter);
            this.lvCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvCliente_KeyPress);
            this.lvCliente.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvCliente_MouseDoubleClick);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Enabled = false;
            this.lblCliente.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblCliente.Location = new System.Drawing.Point(567, 24);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(54, 19);
            this.lblCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.lblCliente.TabIndex = 120;
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtCliente
            // 
            this.txtCliente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCliente.Lines = new string[0];
            this.txtCliente.Location = new System.Drawing.Point(631, 20);
            this.txtCliente.MaxLength = 32767;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PasswordChar = '\0';
            this.txtCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCliente.SelectedText = "";
            this.txtCliente.Size = new System.Drawing.Size(392, 29);
            this.txtCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCliente.TabIndex = 119;
            this.txtCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCliente.UseSelectable = true;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // lblIni
            // 
            this.lblIni.AutoSize = true;
            this.lblIni.Cursor = System.Windows.Forms.Cursors.No;
            this.lblIni.Enabled = false;
            this.lblIni.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblIni.Location = new System.Drawing.Point(234, 24);
            this.lblIni.Name = "lblIni";
            this.lblIni.Size = new System.Drawing.Size(50, 19);
            this.lblIni.Style = MetroFramework.MetroColorStyle.Red;
            this.lblIni.TabIndex = 115;
            this.lblIni.Text = "Desde:";
            this.lblIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(289, 20);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 112;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.Enabled = false;
            this.lblFin.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFin.Location = new System.Drawing.Point(396, 24);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(47, 19);
            this.lblFin.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFin.TabIndex = 114;
            this.lblFin.Text = "Hasta:";
            this.lblFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(448, 19);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 113;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            this.btnImprimir.Location = new System.Drawing.Point(1149, 23);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 111;
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
            this.btnExcel.Location = new System.Drawing.Point(1091, 23);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 110;
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
            this.btnBuscar.Location = new System.Drawing.Point(1038, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 109;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgvDataResumen
            // 
            this.dtgvDataResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvDataResumen.Location = new System.Drawing.Point(0, 0);
            this.dtgvDataResumen.LookAndFeel.SkinName = "Darkroom";
            this.dtgvDataResumen.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvDataResumen.Name = "dtgvDataResumen";
            this.dtgvDataResumen.OptionsPrint.PageSettings.Landscape = true;
            this.dtgvDataResumen.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.dtgvDataResumen.Size = new System.Drawing.Size(1296, 264);
            this.dtgvDataResumen.TabIndex = 4;
            this.dtgvDataResumen.CustomAppearance += new DevExpress.XtraPivotGrid.PivotCustomAppearanceEventHandler(this.dtgvDataResumen_CustomAppearance);
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
            this.dtgvData.Size = new System.Drawing.Size(1296, 264);
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
            this.dtgvDataView.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dtgvDataView_CustomSummaryCalculate);
            // 
            // Proyeccion_Cobranzas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 422);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Proyeccion_Cobranzas";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Proyecciones de Cobranzas";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Proyeccion_Cobranzas_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataResumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroLabel lblIni;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel lblFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroTextBox txtCliente;
        private MetroFramework.Controls.MetroLabel lblCliente;
        private System.Windows.Forms.ListView lvCliente;
        private MetroFramework.Controls.MetroLabel lblPendiente;
        private MetroFramework.Controls.MetroTextBox txtPendientes;
        private MetroFramework.Controls.MetroButton btnResumen;
        private DevExpress.XtraPivotGrid.PivotGridControl dtgvDataResumen;
        private MetroFramework.Controls.MetroCheckBox chkCompania;
        private MetroFramework.Controls.MetroComboBox cboCompañia;
        private MetroFramework.Controls.MetroLabel lblCompañia;
    }
}