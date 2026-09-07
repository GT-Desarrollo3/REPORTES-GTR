namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class PlanillasTrabajadores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanillasTrabajadores));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkAnio = new System.Windows.Forms.CheckBox();
            this.chkPlanilla = new System.Windows.Forms.CheckBox();
            this.chkCompania = new System.Windows.Forms.CheckBox();
            this.chkDetalle = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboAnio = new MetroFramework.Controls.MetroComboBox();
            this.cboPlanilla = new MetroFramework.Controls.MetroComboBox();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.gbFiltro = new ReportesTranspesa.Grouper();
            this.rbChoferes = new MetroFramework.Controls.MetroRadioButton();
            this.rbObreros = new MetroFramework.Controls.MetroRadioButton();
            this.rbEmpleados = new MetroFramework.Controls.MetroRadioButton();
            this.rbTodos = new MetroFramework.Controls.MetroRadioButton();
            this.cboCompañia = new MetroFramework.Controls.MetroComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dtgvPlanilla = new DevExpress.XtraGrid.GridControl();
            this.dtgvPlanillaView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgvData = new DevExpress.XtraPivotGrid.PivotGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanilla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.splitContainer1.Panel1.Controls.Add(this.chkAnio);
            this.splitContainer1.Panel1.Controls.Add(this.chkPlanilla);
            this.splitContainer1.Panel1.Controls.Add(this.chkCompania);
            this.splitContainer1.Panel1.Controls.Add(this.chkDetalle);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cboAnio);
            this.splitContainer1.Panel1.Controls.Add(this.cboPlanilla);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.gbFiltro);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompañia);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.PaleGoldenrod;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1310, 479);
            this.splitContainer1.SplitterDistance = 89;
            this.splitContainer1.TabIndex = 0;
            // 
            // chkAnio
            // 
            this.chkAnio.AutoSize = true;
            this.chkAnio.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkAnio.Location = new System.Drawing.Point(587, 39);
            this.chkAnio.Name = "chkAnio";
            this.chkAnio.Size = new System.Drawing.Size(48, 17);
            this.chkAnio.TabIndex = 59;
            this.chkAnio.Text = "Año:";
            this.chkAnio.UseVisualStyleBackColor = true;
            this.chkAnio.CheckedChanged += new System.EventHandler(this.chkAnio_CheckedChanged_1);
            // 
            // chkPlanilla
            // 
            this.chkPlanilla.AutoSize = true;
            this.chkPlanilla.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkPlanilla.Location = new System.Drawing.Point(15, 57);
            this.chkPlanilla.Name = "chkPlanilla";
            this.chkPlanilla.Size = new System.Drawing.Size(98, 17);
            this.chkPlanilla.TabIndex = 58;
            this.chkPlanilla.Text = "Remuneración:";
            this.chkPlanilla.UseVisualStyleBackColor = true;
            this.chkPlanilla.CheckedChanged += new System.EventHandler(this.chkPlanilla_CheckedChanged_1);
            // 
            // chkCompania
            // 
            this.chkCompania.AutoSize = true;
            this.chkCompania.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkCompania.Location = new System.Drawing.Point(37, 24);
            this.chkCompania.Name = "chkCompania";
            this.chkCompania.Size = new System.Drawing.Size(76, 17);
            this.chkCompania.TabIndex = 57;
            this.chkCompania.Text = "Compañia:";
            this.chkCompania.UseVisualStyleBackColor = true;
            this.chkCompania.CheckedChanged += new System.EventHandler(this.chkCompania_CheckedChanged_1);
            // 
            // chkDetalle
            // 
            this.chkDetalle.AutoSize = true;
            this.chkDetalle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkDetalle.Location = new System.Drawing.Point(645, 62);
            this.chkDetalle.Name = "chkDetalle";
            this.chkDetalle.Size = new System.Drawing.Size(90, 17);
            this.chkDetalle.TabIndex = 21;
            this.chkDetalle.Text = "DETALLADO";
            this.chkDetalle.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(694, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // cboAnio
            // 
            this.cboAnio.Enabled = false;
            this.cboAnio.ForeColor = System.Drawing.SystemColors.Window;
            this.cboAnio.FormattingEnabled = true;
            this.cboAnio.ItemHeight = 23;
            this.cboAnio.Items.AddRange(new object[] {
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.cboAnio.Location = new System.Drawing.Point(645, 31);
            this.cboAnio.Name = "cboAnio";
            this.cboAnio.Size = new System.Drawing.Size(82, 29);
            this.cboAnio.Style = MetroFramework.MetroColorStyle.Red;
            this.cboAnio.TabIndex = 54;
            this.cboAnio.UseSelectable = true;
            // 
            // cboPlanilla
            // 
            this.cboPlanilla.Enabled = false;
            this.cboPlanilla.ForeColor = System.Drawing.SystemColors.Window;
            this.cboPlanilla.FormattingEnabled = true;
            this.cboPlanilla.ItemHeight = 23;
            this.cboPlanilla.Items.AddRange(new object[] {
            "Sueldo Bruto",
            "Sueldo Neto"});
            this.cboPlanilla.Location = new System.Drawing.Point(128, 51);
            this.cboPlanilla.Name = "cboPlanilla";
            this.cboPlanilla.Size = new System.Drawing.Size(112, 29);
            this.cboPlanilla.Style = MetroFramework.MetroColorStyle.Red;
            this.cboPlanilla.TabIndex = 52;
            this.cboPlanilla.UseSelectable = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.ForeColor = System.Drawing.SystemColors.Window;
            this.btnImprimir.Appearance.Options.UseBackColor = true;
            this.btnImprimir.Appearance.Options.UseBorderColor = true;
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.Appearance.Options.UseForeColor = true;
            this.btnImprimir.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnImprimir.Location = new System.Drawing.Point(995, 30);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 51;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.ForeColor = System.Drawing.SystemColors.Window;
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Appearance.Options.UseForeColor = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(929, 30);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 50;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.Gold;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.Gold;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.ForeColor = System.Drawing.SystemColors.Window;
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Appearance.Options.UseForeColor = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(867, 30);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 49;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // gbFiltro
            // 
            this.gbFiltro.BackgroundColor = System.Drawing.Color.PaleGoldenrod;
            this.gbFiltro.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFiltro.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFiltro.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.BorderThickness = 1F;
            this.gbFiltro.Controls.Add(this.rbChoferes);
            this.gbFiltro.Controls.Add(this.rbObreros);
            this.gbFiltro.Controls.Add(this.rbEmpleados);
            this.gbFiltro.Controls.Add(this.rbTodos);
            this.gbFiltro.CustomGroupBoxColor = System.Drawing.Color.PaleGoldenrod;
            this.gbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltro.ForeColor = System.Drawing.SystemColors.WindowText;
            this.gbFiltro.GroupImage = null;
            this.gbFiltro.GroupTitle = "Filtrar por";
            this.gbFiltro.Location = new System.Drawing.Point(249, 12);
            this.gbFiltro.Name = "gbFiltro";
            this.gbFiltro.Padding = new System.Windows.Forms.Padding(20);
            this.gbFiltro.PaintGroupBox = false;
            this.gbFiltro.RoundCorners = 3;
            this.gbFiltro.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFiltro.ShadowControl = false;
            this.gbFiltro.ShadowThickness = 3;
            this.gbFiltro.Size = new System.Drawing.Size(332, 68);
            this.gbFiltro.TabIndex = 46;
            // 
            // rbChoferes
            // 
            this.rbChoferes.AutoSize = true;
            this.rbChoferes.Location = new System.Drawing.Point(243, 39);
            this.rbChoferes.Name = "rbChoferes";
            this.rbChoferes.Size = new System.Drawing.Size(70, 15);
            this.rbChoferes.Style = MetroFramework.MetroColorStyle.Red;
            this.rbChoferes.TabIndex = 20;
            this.rbChoferes.Text = "Choferes";
            this.rbChoferes.UseSelectable = true;
            // 
            // rbObreros
            // 
            this.rbObreros.AutoSize = true;
            this.rbObreros.Location = new System.Drawing.Point(172, 39);
            this.rbObreros.Name = "rbObreros";
            this.rbObreros.Size = new System.Drawing.Size(65, 15);
            this.rbObreros.Style = MetroFramework.MetroColorStyle.Red;
            this.rbObreros.TabIndex = 19;
            this.rbObreros.Text = "Obreros";
            this.rbObreros.UseSelectable = true;
            // 
            // rbEmpleados
            // 
            this.rbEmpleados.AutoSize = true;
            this.rbEmpleados.Location = new System.Drawing.Point(85, 39);
            this.rbEmpleados.Name = "rbEmpleados";
            this.rbEmpleados.Size = new System.Drawing.Size(81, 15);
            this.rbEmpleados.Style = MetroFramework.MetroColorStyle.Red;
            this.rbEmpleados.TabIndex = 18;
            this.rbEmpleados.Text = "Empleados";
            this.rbEmpleados.UseSelectable = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Location = new System.Drawing.Point(23, 39);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(54, 15);
            this.rbTodos.Style = MetroFramework.MetroColorStyle.Red;
            this.rbTodos.TabIndex = 17;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseSelectable = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // cboCompañia
            // 
            this.cboCompañia.Enabled = false;
            this.cboCompañia.ForeColor = System.Drawing.SystemColors.Window;
            this.cboCompañia.FormattingEnabled = true;
            this.cboCompañia.ItemHeight = 23;
            this.cboCompañia.Items.AddRange(new object[] {
            "TRANSPESA",
            "BRA",
            "ALTRA",
            "AMT",
            "ADUANAS"});
            this.cboCompañia.Location = new System.Drawing.Point(119, 16);
            this.cboCompañia.Name = "cboCompañia";
            this.cboCompañia.Size = new System.Drawing.Size(121, 29);
            this.cboCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCompañia.TabIndex = 44;
            this.cboCompañia.UseSelectable = true;
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
            this.splitContainer2.Panel1.Controls.Add(this.dtgvPlanilla);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer2.Size = new System.Drawing.Size(1310, 386);
            this.splitContainer2.SplitterDistance = 177;
            this.splitContainer2.TabIndex = 0;
            // 
            // dtgvPlanilla
            // 
            this.dtgvPlanilla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode2.RelationName = "Level1";
            this.dtgvPlanilla.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvPlanilla.Location = new System.Drawing.Point(10, 0);
            this.dtgvPlanilla.LookAndFeel.SkinName = "Seven";
            this.dtgvPlanilla.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvPlanilla.MainView = this.dtgvPlanillaView;
            this.dtgvPlanilla.Name = "dtgvPlanilla";
            this.dtgvPlanilla.Size = new System.Drawing.Size(1280, 170);
            this.dtgvPlanilla.TabIndex = 4;
            this.dtgvPlanilla.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvPlanillaView});
            this.dtgvPlanilla.Click += new System.EventHandler(this.dtgvPlanilla_Click);
            this.dtgvPlanilla.DoubleClick += new System.EventHandler(this.dtgvPlanilla_DoubleClick);
            // 
            // dtgvPlanillaView
            // 
            this.dtgvPlanillaView.GridControl = this.dtgvPlanilla;
            this.dtgvPlanillaView.Name = "dtgvPlanillaView";
            this.dtgvPlanillaView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvPlanillaView.OptionsBehavior.Editable = false;
            this.dtgvPlanillaView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvPlanillaView.OptionsSelection.MultiSelect = true;
            this.dtgvPlanillaView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dtgvPlanillaView.OptionsView.ColumnAutoWidth = false;
            this.dtgvPlanillaView.OptionsView.ShowFooter = true;
            // 
            // dtgvData
            // 
            this.dtgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvData.Location = new System.Drawing.Point(10, 0);
            this.dtgvData.LookAndFeel.SkinName = "Seven";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.OptionsPrint.PageSettings.Landscape = true;
            this.dtgvData.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.dtgvData.Size = new System.Drawing.Size(1280, 192);
            this.dtgvData.TabIndex = 2;
            this.dtgvData.CustomAppearance += new DevExpress.XtraPivotGrid.PivotCustomAppearanceEventHandler(this.dtgvData_CustomAppearance);
            // 
            // PlanillasTrabajadores
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1310, 539);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "PlanillasTrabajadores";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Planillas de Trabajadores";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PlanillasTrabajadores_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbFiltro.ResumeLayout(false);
            this.gbFiltro.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanilla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Grouper gbFiltro;
        private MetroFramework.Controls.MetroRadioButton rbChoferes;
        private MetroFramework.Controls.MetroRadioButton rbObreros;
        private MetroFramework.Controls.MetroRadioButton rbEmpleados;
        private MetroFramework.Controls.MetroRadioButton rbTodos;
        private MetroFramework.Controls.MetroComboBox cboCompañia;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroComboBox cboPlanilla;
        private MetroFramework.Controls.MetroComboBox cboAnio;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraPivotGrid.PivotGridControl dtgvData;
        private DevExpress.XtraGrid.GridControl dtgvPlanilla;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvPlanillaView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDetalle;
        private System.Windows.Forms.CheckBox chkPlanilla;
        private System.Windows.Forms.CheckBox chkCompania;
        private System.Windows.Forms.CheckBox chkAnio;
    }
}