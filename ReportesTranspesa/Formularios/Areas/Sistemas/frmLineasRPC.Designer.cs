namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    partial class frmLineasRPC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLineasRPC));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbFiltro = new ReportesTranspesa.Grouper();
            this.rbPlan79 = new MetroFramework.Controls.MetroRadioButton();
            this.rbPlan75 = new MetroFramework.Controls.MetroRadioButton();
            this.rbPlan180 = new MetroFramework.Controls.MetroRadioButton();
            this.rbPlan69 = new MetroFramework.Controls.MetroRadioButton();
            this.rbPlan29 = new MetroFramework.Controls.MetroRadioButton();
            this.rbPlan12 = new MetroFramework.Controls.MetroRadioButton();
            this.rbTodos = new MetroFramework.Controls.MetroRadioButton();
            this.chkCompania = new MetroFramework.Controls.MetroCheckBox();
            this.cboCompañia = new MetroFramework.Controls.MetroComboBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbFiltro.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.gbFiltro);
            this.splitContainer1.Panel1.Controls.Add(this.chkCompania);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompañia);
            this.splitContainer1.Panel1.Controls.Add(this.btnGuardar);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(938, 316);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 0;
            // 
            // gbFiltro
            // 
            this.gbFiltro.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFiltro.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFiltro.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFiltro.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.BorderThickness = 1F;
            this.gbFiltro.Controls.Add(this.rbPlan79);
            this.gbFiltro.Controls.Add(this.rbPlan75);
            this.gbFiltro.Controls.Add(this.rbPlan180);
            this.gbFiltro.Controls.Add(this.rbPlan69);
            this.gbFiltro.Controls.Add(this.rbPlan29);
            this.gbFiltro.Controls.Add(this.rbPlan12);
            this.gbFiltro.Controls.Add(this.rbTodos);
            this.gbFiltro.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltro.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.GroupImage = null;
            this.gbFiltro.GroupTitle = "Filtrar por";
            this.gbFiltro.Location = new System.Drawing.Point(232, 9);
            this.gbFiltro.Name = "gbFiltro";
            this.gbFiltro.Padding = new System.Windows.Forms.Padding(20);
            this.gbFiltro.PaintGroupBox = false;
            this.gbFiltro.RoundCorners = 3;
            this.gbFiltro.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFiltro.ShadowControl = false;
            this.gbFiltro.ShadowThickness = 3;
            this.gbFiltro.Size = new System.Drawing.Size(496, 48);
            this.gbFiltro.TabIndex = 96;
            // 
            // rbPlan79
            // 
            this.rbPlan79.AutoSize = true;
            this.rbPlan79.Location = new System.Drawing.Point(353, 26);
            this.rbPlan79.Name = "rbPlan79";
            this.rbPlan79.Size = new System.Drawing.Size(61, 15);
            this.rbPlan79.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPlan79.TabIndex = 21;
            this.rbPlan79.Text = "Plan 79";
            this.rbPlan79.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPlan79.UseSelectable = true;
            this.rbPlan79.Visible = false;
            // 
            // rbPlan75
            // 
            this.rbPlan75.AutoSize = true;
            this.rbPlan75.Location = new System.Drawing.Point(286, 26);
            this.rbPlan75.Name = "rbPlan75";
            this.rbPlan75.Size = new System.Drawing.Size(61, 15);
            this.rbPlan75.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPlan75.TabIndex = 98;
            this.rbPlan75.Text = "Plan 75";
            this.rbPlan75.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPlan75.UseSelectable = true;
            // 
            // rbPlan180
            // 
            this.rbPlan180.AutoSize = true;
            this.rbPlan180.Location = new System.Drawing.Point(421, 26);
            this.rbPlan180.Name = "rbPlan180";
            this.rbPlan180.Size = new System.Drawing.Size(67, 15);
            this.rbPlan180.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPlan180.TabIndex = 97;
            this.rbPlan180.Text = "Plan 180";
            this.rbPlan180.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPlan180.UseSelectable = true;
            this.rbPlan180.Visible = false;
            // 
            // rbPlan69
            // 
            this.rbPlan69.AutoSize = true;
            this.rbPlan69.Location = new System.Drawing.Point(219, 26);
            this.rbPlan69.Name = "rbPlan69";
            this.rbPlan69.Size = new System.Drawing.Size(61, 15);
            this.rbPlan69.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPlan69.TabIndex = 20;
            this.rbPlan69.Text = "Plan 69";
            this.rbPlan69.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPlan69.UseSelectable = true;
            this.rbPlan69.Visible = false;
            // 
            // rbPlan29
            // 
            this.rbPlan29.AutoSize = true;
            this.rbPlan29.Location = new System.Drawing.Point(152, 26);
            this.rbPlan29.Name = "rbPlan29";
            this.rbPlan29.Size = new System.Drawing.Size(61, 15);
            this.rbPlan29.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPlan29.TabIndex = 19;
            this.rbPlan29.Text = "Plan 29";
            this.rbPlan29.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPlan29.UseSelectable = true;
            // 
            // rbPlan12
            // 
            this.rbPlan12.AutoSize = true;
            this.rbPlan12.Location = new System.Drawing.Point(85, 27);
            this.rbPlan12.Name = "rbPlan12";
            this.rbPlan12.Size = new System.Drawing.Size(61, 15);
            this.rbPlan12.Style = MetroFramework.MetroColorStyle.Red;
            this.rbPlan12.TabIndex = 18;
            this.rbPlan12.Text = "Plan 12";
            this.rbPlan12.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbPlan12.UseSelectable = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Location = new System.Drawing.Point(23, 27);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(56, 15);
            this.rbTodos.Style = MetroFramework.MetroColorStyle.Red;
            this.rbTodos.TabIndex = 17;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbTodos.UseSelectable = true;
            // 
            // chkCompania
            // 
            this.chkCompania.AutoSize = true;
            this.chkCompania.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkCompania.Location = new System.Drawing.Point(9, 24);
            this.chkCompania.Name = "chkCompania";
            this.chkCompania.Size = new System.Drawing.Size(90, 19);
            this.chkCompania.Style = MetroFramework.MetroColorStyle.Red;
            this.chkCompania.TabIndex = 95;
            this.chkCompania.Text = "Compañía:";
            this.chkCompania.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkCompania.UseSelectable = true;
            this.chkCompania.CheckedChanged += new System.EventHandler(this.chkCompania_CheckedChanged);
            // 
            // cboCompañia
            // 
            this.cboCompañia.Enabled = false;
            this.cboCompañia.FormattingEnabled = true;
            this.cboCompañia.ItemHeight = 23;
            this.cboCompañia.Items.AddRange(new object[] {
            "TRANSPESA",
            "AMT",
            "DISOR"});
            this.cboCompañia.Location = new System.Drawing.Point(105, 20);
            this.cboCompañia.Name = "cboCompañia";
            this.cboCompañia.Size = new System.Drawing.Size(121, 29);
            this.cboCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCompañia.TabIndex = 94;
            this.cboCompañia.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboCompañia.UseSelectable = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardar.Location = new System.Drawing.Point(830, 20);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(40, 37);
            this.btnGuardar.TabIndex = 93;
            this.btnGuardar.ToolTip = "Guardar cambios";
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
            this.btnExcel.Location = new System.Drawing.Point(885, 20);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 52;
            this.btnExcel.ToolTip = "Exportar a Excel";
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
            this.btnBuscar.Location = new System.Drawing.Point(773, 20);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 51;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.dtgvData.Size = new System.Drawing.Size(938, 245);
            this.dtgvData.TabIndex = 4;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
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
            this.dtgvDataView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dtgvDataView_CustomDrawCell);
            this.dtgvDataView.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.dtgvDataView_InitNewRow);
            this.dtgvDataView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.dtgvDataView_CustomUnboundColumnData);
            // 
            // frmLineasRPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 396);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmLineasRPC";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Lineas de Celulares - Red Privada Claro";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLineasRPC_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbFiltro.ResumeLayout(false);
            this.gbFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private Grouper gbFiltro;
        private MetroFramework.Controls.MetroRadioButton rbPlan29;
        private MetroFramework.Controls.MetroRadioButton rbPlan12;
        private MetroFramework.Controls.MetroRadioButton rbTodos;
        private MetroFramework.Controls.MetroCheckBox chkCompania;
        private MetroFramework.Controls.MetroComboBox cboCompañia;
        private MetroFramework.Controls.MetroRadioButton rbPlan69;
        private MetroFramework.Controls.MetroRadioButton rbPlan79;
        private MetroFramework.Controls.MetroRadioButton rbPlan180;
        private MetroFramework.Controls.MetroRadioButton rbPlan75;
    }
}