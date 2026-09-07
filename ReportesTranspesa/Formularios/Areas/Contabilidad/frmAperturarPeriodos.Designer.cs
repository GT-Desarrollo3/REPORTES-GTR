namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class frmAperturarPeriodos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAperturarPeriodos));
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.btnCerrarPeriodo = new DevExpress.XtraEditors.SimpleButton();
            this.dtpFecha = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.cbxCompania = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.btnNuevoPeriodo = new DevExpress.XtraEditors.SimpleButton();
            this.dtgPeriodos = new DevExpress.XtraGrid.GridControl();
            this.dgvPeriodosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsCerrarAbrirPeriodo = new System.Windows.Forms.ToolStripMenuItem();
            this.gbFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeriodos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeriodosView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.btnCerrarPeriodo);
            this.gbFiltros.Controls.Add(this.dtpFecha);
            this.gbFiltros.Controls.Add(this.metroLabel2);
            this.gbFiltros.Controls.Add(this.btnExcel);
            this.gbFiltros.Controls.Add(this.btnBuscar);
            this.gbFiltros.Controls.Add(this.cbxCompania);
            this.gbFiltros.Controls.Add(this.metroLabel3);
            this.gbFiltros.Controls.Add(this.btnNuevoPeriodo);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbFiltros.Location = new System.Drawing.Point(20, 60);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(727, 138);
            this.gbFiltros.TabIndex = 135;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtro de Búsqueda";
            // 
            // btnCerrarPeriodo
            // 
            this.btnCerrarPeriodo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCerrarPeriodo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCerrarPeriodo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCerrarPeriodo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarPeriodo.Appearance.Options.UseBackColor = true;
            this.btnCerrarPeriodo.Appearance.Options.UseBorderColor = true;
            this.btnCerrarPeriodo.Appearance.Options.UseFont = true;
            this.btnCerrarPeriodo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarPeriodo.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarPeriodo.Image")));
            this.btnCerrarPeriodo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCerrarPeriodo.Location = new System.Drawing.Point(22, 82);
            this.btnCerrarPeriodo.Name = "btnCerrarPeriodo";
            this.btnCerrarPeriodo.Size = new System.Drawing.Size(109, 36);
            this.btnCerrarPeriodo.TabIndex = 102;
            this.btnCerrarPeriodo.Text = "Cerrar\r\nPeriodo";
            this.btnCerrarPeriodo.ToolTip = "Cerrar Periodo";
            this.btnCerrarPeriodo.Click += new System.EventHandler(this.btnCerrarPeriodo_Click);
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyyMM";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(391, 75);
            this.dtpFecha.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(84, 29);
            this.dtpFecha.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFecha.TabIndex = 96;
            this.dtpFecha.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFecha_KeyPress);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.White;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(391, 49);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(58, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 95;
            this.metroLabel2.Text = "Periodo:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(574, 53);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 7;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(514, 53);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbxCompania
            // 
            this.cbxCompania.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbxCompania.FormattingEnabled = true;
            this.cbxCompania.ItemHeight = 19;
            this.cbxCompania.Location = new System.Drawing.Point(153, 77);
            this.cbxCompania.Name = "cbxCompania";
            this.cbxCompania.Size = new System.Drawing.Size(220, 25);
            this.cbxCompania.Style = MetroFramework.MetroColorStyle.Red;
            this.cbxCompania.TabIndex = 100;
            this.cbxCompania.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cbxCompania.UseSelectable = true;
            this.cbxCompania.SelectedIndexChanged += new System.EventHandler(this.cbxCompania_SelectedIndexChanged);
            this.cbxCompania.DropDownClosed += new System.EventHandler(this.cbxCompania_DropDownClosed);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(153, 49);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(74, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 99;
            this.metroLabel3.Text = "Compañía:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btnNuevoPeriodo
            // 
            this.btnNuevoPeriodo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoPeriodo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoPeriodo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoPeriodo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoPeriodo.Appearance.Options.UseBackColor = true;
            this.btnNuevoPeriodo.Appearance.Options.UseBorderColor = true;
            this.btnNuevoPeriodo.Appearance.Options.UseFont = true;
            this.btnNuevoPeriodo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoPeriodo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoPeriodo.Image")));
            this.btnNuevoPeriodo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoPeriodo.Location = new System.Drawing.Point(22, 35);
            this.btnNuevoPeriodo.Name = "btnNuevoPeriodo";
            this.btnNuevoPeriodo.Size = new System.Drawing.Size(109, 36);
            this.btnNuevoPeriodo.TabIndex = 101;
            this.btnNuevoPeriodo.Text = "Abrir\r\nPeriodo";
            this.btnNuevoPeriodo.ToolTip = "Abrir Periodo";
            this.btnNuevoPeriodo.Click += new System.EventHandler(this.btnNuevoPeriodo_Click);
            // 
            // dtgPeriodos
            // 
            this.dtgPeriodos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgPeriodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgPeriodos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgPeriodos.Location = new System.Drawing.Point(20, 218);
            this.dtgPeriodos.LookAndFeel.SkinName = "Darkroom";
            this.dtgPeriodos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgPeriodos.MainView = this.dgvPeriodosView;
            this.dtgPeriodos.Name = "dtgPeriodos";
            this.dtgPeriodos.Size = new System.Drawing.Size(727, 344);
            this.dtgPeriodos.TabIndex = 138;
            this.dtgPeriodos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvPeriodosView});
            this.dtgPeriodos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgPeriodos_MouseUp);
            // 
            // dgvPeriodosView
            // 
            this.dgvPeriodosView.GridControl = this.dtgPeriodos;
            this.dgvPeriodosView.Name = "dgvPeriodosView";
            this.dgvPeriodosView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvPeriodosView.OptionsBehavior.Editable = false;
            this.dgvPeriodosView.OptionsView.ColumnAutoWidth = false;
            this.dgvPeriodosView.OptionsView.ShowFooter = true;
            this.dgvPeriodosView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvPeriodosView_CustomDrawCell);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 198);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(727, 20);
            this.panel2.TabIndex = 137;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCerrarAbrirPeriodo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // tsCerrarAbrirPeriodo
            // 
            this.tsCerrarAbrirPeriodo.Image = global::ReportesTranspesa.Properties.Resources.actualizarGuia;
            this.tsCerrarAbrirPeriodo.Name = "tsCerrarAbrirPeriodo";
            this.tsCerrarAbrirPeriodo.Size = new System.Drawing.Size(152, 22);
            this.tsCerrarAbrirPeriodo.Text = "Abrir periodo";
            this.tsCerrarAbrirPeriodo.Click += new System.EventHandler(this.tsCerrarAbrirPeriodo_Click);
            // 
            // frmAperturarPeriodos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 582);
            this.Controls.Add(this.dtgPeriodos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbFiltros);
            this.Name = "frmAperturarPeriodos";
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "CONTROL DE PERIODOS";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.frmAperturarPeriodos_Load);
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeriodos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeriodosView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        private MetroFramework.Controls.MetroComboBox cbxCompania;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroDateTime dtpFecha;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgPeriodos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvPeriodosView;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnNuevoPeriodo;
        private DevExpress.XtraEditors.SimpleButton btnCerrarPeriodo;
        private System.Windows.Forms.ToolStripMenuItem tsCerrarAbrirPeriodo;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}