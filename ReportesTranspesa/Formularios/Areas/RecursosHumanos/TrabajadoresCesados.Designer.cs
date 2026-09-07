namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class TrabajadoresCesados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrabajadoresCesados));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblFechaInicio = new MetroFramework.Controls.MetroLabel();
            this.lblFechaFin = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.chkCompania = new MetroFramework.Controls.MetroCheckBox();
            this.cboCompañia = new MetroFramework.Controls.MetroComboBox();
            this.gbFiltro = new ReportesTranspesa.Grouper();
            this.rbChoferes = new MetroFramework.Controls.MetroRadioButton();
            this.rbObreros = new MetroFramework.Controls.MetroRadioButton();
            this.rbEmpleados = new MetroFramework.Controls.MetroRadioButton();
            this.rbTodos = new MetroFramework.Controls.MetroRadioButton();
            this.dtgvPlanilla = new DevExpress.XtraGrid.GridControl();
            this.dtgvPlanillaView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanilla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaView)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.lblFechaInicio);
            this.splitContainer1.Panel1.Controls.Add(this.lblFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.chkCompania);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompañia);
            this.splitContainer1.Panel1.Controls.Add(this.gbFiltro);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvPlanilla);
            this.splitContainer1.Size = new System.Drawing.Size(1110, 366);
            this.splitContainer1.SplitterDistance = 58;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Location = new System.Drawing.Point(606, 27);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(39, 19);
            this.lblFechaInicio.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFechaInicio.TabIndex = 77;
            this.lblFechaInicio.Text = "Inicio";
            this.lblFechaInicio.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Location = new System.Drawing.Point(749, 27);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(26, 19);
            this.lblFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFechaFin.TabIndex = 76;
            this.lblFechaFin.Text = "Fin";
            this.lblFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(775, 23);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(84, 25);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 75;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(650, 23);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(87, 25);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 74;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            this.btnImprimir.Location = new System.Drawing.Point(1022, 23);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 54;
            this.btnImprimir.ToolTip = "Imprimir";
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
            this.btnExcel.Location = new System.Drawing.Point(956, 23);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 53;
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
            this.btnBuscar.Location = new System.Drawing.Point(894, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 52;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // chkCompania
            // 
            this.chkCompania.AutoSize = true;
            this.chkCompania.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkCompania.Location = new System.Drawing.Point(17, 23);
            this.chkCompania.Name = "chkCompania";
            this.chkCompania.Size = new System.Drawing.Size(90, 19);
            this.chkCompania.Style = MetroFramework.MetroColorStyle.Red;
            this.chkCompania.TabIndex = 49;
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
            "BRA",
            "ALTRA",
            "AMT",
            "ADUANAS"});
            this.cboCompañia.Location = new System.Drawing.Point(113, 19);
            this.cboCompañia.Name = "cboCompañia";
            this.cboCompañia.Size = new System.Drawing.Size(121, 29);
            this.cboCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCompañia.TabIndex = 48;
            this.cboCompañia.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboCompañia.UseSelectable = true;
            // 
            // gbFiltro
            // 
            this.gbFiltro.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.gbFiltro.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbFiltro.BackgroundGradientMode = ReportesTranspesa.Grouper.GroupBoxGradientMode.None;
            this.gbFiltro.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.BorderThickness = 1F;
            this.gbFiltro.Controls.Add(this.rbChoferes);
            this.gbFiltro.Controls.Add(this.rbObreros);
            this.gbFiltro.Controls.Add(this.rbEmpleados);
            this.gbFiltro.Controls.Add(this.rbTodos);
            this.gbFiltro.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltro.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbFiltro.GroupImage = null;
            this.gbFiltro.GroupTitle = "Filtrar por";
            this.gbFiltro.Location = new System.Drawing.Point(244, 5);
            this.gbFiltro.Name = "gbFiltro";
            this.gbFiltro.Padding = new System.Windows.Forms.Padding(20);
            this.gbFiltro.PaintGroupBox = false;
            this.gbFiltro.RoundCorners = 3;
            this.gbFiltro.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbFiltro.ShadowControl = false;
            this.gbFiltro.ShadowThickness = 3;
            this.gbFiltro.Size = new System.Drawing.Size(332, 57);
            this.gbFiltro.TabIndex = 47;
            // 
            // rbChoferes
            // 
            this.rbChoferes.AutoSize = true;
            this.rbChoferes.Location = new System.Drawing.Point(243, 31);
            this.rbChoferes.Name = "rbChoferes";
            this.rbChoferes.Size = new System.Drawing.Size(70, 15);
            this.rbChoferes.Style = MetroFramework.MetroColorStyle.Red;
            this.rbChoferes.TabIndex = 20;
            this.rbChoferes.Text = "Choferes";
            this.rbChoferes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbChoferes.UseSelectable = true;
            // 
            // rbObreros
            // 
            this.rbObreros.AutoSize = true;
            this.rbObreros.Location = new System.Drawing.Point(172, 31);
            this.rbObreros.Name = "rbObreros";
            this.rbObreros.Size = new System.Drawing.Size(65, 15);
            this.rbObreros.Style = MetroFramework.MetroColorStyle.Red;
            this.rbObreros.TabIndex = 19;
            this.rbObreros.Text = "Obreros";
            this.rbObreros.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbObreros.UseSelectable = true;
            // 
            // rbEmpleados
            // 
            this.rbEmpleados.AutoSize = true;
            this.rbEmpleados.Location = new System.Drawing.Point(85, 31);
            this.rbEmpleados.Name = "rbEmpleados";
            this.rbEmpleados.Size = new System.Drawing.Size(81, 15);
            this.rbEmpleados.Style = MetroFramework.MetroColorStyle.Red;
            this.rbEmpleados.TabIndex = 18;
            this.rbEmpleados.Text = "Empleados";
            this.rbEmpleados.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbEmpleados.UseSelectable = true;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Location = new System.Drawing.Point(23, 31);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(56, 15);
            this.rbTodos.Style = MetroFramework.MetroColorStyle.Red;
            this.rbTodos.TabIndex = 17;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rbTodos.UseSelectable = true;
            // 
            // dtgvPlanilla
            // 
            this.dtgvPlanilla.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvPlanilla.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvPlanilla.Location = new System.Drawing.Point(0, 0);
            this.dtgvPlanilla.LookAndFeel.SkinName = "Darkroom";
            this.dtgvPlanilla.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvPlanilla.MainView = this.dtgvPlanillaView;
            this.dtgvPlanilla.Name = "dtgvPlanilla";
            this.dtgvPlanilla.Size = new System.Drawing.Size(1110, 304);
            this.dtgvPlanilla.TabIndex = 5;
            this.dtgvPlanilla.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvPlanillaView});
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
            // TrabajadoresCesados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 446);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TrabajadoresCesados";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Lista Trabajadores Cesados";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TrabajadoresCesados_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbFiltro.ResumeLayout(false);
            this.gbFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanilla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Grouper gbFiltro;
        private MetroFramework.Controls.MetroRadioButton rbChoferes;
        private MetroFramework.Controls.MetroRadioButton rbObreros;
        private MetroFramework.Controls.MetroRadioButton rbEmpleados;
        private MetroFramework.Controls.MetroRadioButton rbTodos;
        private MetroFramework.Controls.MetroCheckBox chkCompania;
        private MetroFramework.Controls.MetroComboBox cboCompañia;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroLabel lblFechaFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel lblFechaInicio;
        private DevExpress.XtraGrid.GridControl dtgvPlanilla;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvPlanillaView;
    }
}