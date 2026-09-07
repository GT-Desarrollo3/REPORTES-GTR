namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    partial class frmRequerimientosCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRequerimientosCompras));
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.chkResumen = new System.Windows.Forms.CheckBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgRequerimientosCompras = new DevExpress.XtraGrid.GridControl();
            this.dgvRequerimientosComprasVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRequerimientosCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequerimientosComprasVista)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.chkResumen);
            this.gbFiltros.Controls.Add(this.dtpFechaIni);
            this.gbFiltros.Controls.Add(this.dtpFechaFin);
            this.gbFiltros.Controls.Add(this.metroLabel2);
            this.gbFiltros.Controls.Add(this.btnExcel);
            this.gbFiltros.Controls.Add(this.metroLabel1);
            this.gbFiltros.Controls.Add(this.btnBuscar);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbFiltros.Location = new System.Drawing.Point(20, 60);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(788, 96);
            this.gbFiltros.TabIndex = 106;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtro de Búsqueda";
            // 
            // chkResumen
            // 
            this.chkResumen.AutoSize = true;
            this.chkResumen.Location = new System.Drawing.Point(663, 44);
            this.chkResumen.Name = "chkResumen";
            this.chkResumen.Size = new System.Drawing.Size(116, 24);
            this.chkResumen.TabIndex = 96;
            this.chkResumen.Text = "RESUMEN";
            this.chkResumen.UseVisualStyleBackColor = true;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(110, 44);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(110, 22);
            this.dtpFechaIni.TabIndex = 92;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(331, 44);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(110, 22);
            this.dtpFechaFin.TabIndex = 93;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.White;
            this.metroLabel2.Location = new System.Drawing.Point(24, 44);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(80, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 95;
            this.metroLabel2.Text = "Fecha Inicio:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
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
            this.btnExcel.Location = new System.Drawing.Point(599, 29);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 7;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(258, 44);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(67, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Black;
            this.metroLabel1.TabIndex = 94;
            this.metroLabel1.Text = "Fecha Fin:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
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
            this.btnBuscar.Location = new System.Drawing.Point(538, 29);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(788, 20);
            this.panel2.TabIndex = 132;
            // 
            // dtgRequerimientosCompras
            // 
            this.dtgRequerimientosCompras.CausesValidation = false;
            this.dtgRequerimientosCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRequerimientosCompras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRequerimientosCompras.Location = new System.Drawing.Point(20, 176);
            this.dtgRequerimientosCompras.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRequerimientosCompras.MainView = this.dgvRequerimientosComprasVista;
            this.dtgRequerimientosCompras.Name = "dtgRequerimientosCompras";
            this.dtgRequerimientosCompras.Size = new System.Drawing.Size(788, 303);
            this.dtgRequerimientosCompras.TabIndex = 133;
            this.dtgRequerimientosCompras.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRequerimientosComprasVista});
            // 
            // dgvRequerimientosComprasVista
            // 
            this.dgvRequerimientosComprasVista.GridControl = this.dtgRequerimientosCompras;
            this.dgvRequerimientosComprasVista.Name = "dgvRequerimientosComprasVista";
            this.dgvRequerimientosComprasVista.OptionsBehavior.Editable = false;
            this.dgvRequerimientosComprasVista.OptionsBehavior.ReadOnly = true;
            this.dgvRequerimientosComprasVista.OptionsView.ColumnAutoWidth = false;
            this.dgvRequerimientosComprasVista.OptionsView.RowAutoHeight = true;
            this.dgvRequerimientosComprasVista.OptionsView.ShowFooter = true;
            this.dgvRequerimientosComprasVista.OptionsView.ShowGroupedColumns = true;
            this.dgvRequerimientosComprasVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvRequerimientosComprasVista_CustomDrawCell);
            // 
            // frmRequerimientosCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 499);
            this.Controls.Add(this.dtgRequerimientosCompras);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbFiltros);
            this.Name = "frmRequerimientosCompras";
            this.Text = "REQUERIMIENTOS - COMPRAS DE MANTENIMIENTO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRequerimientosCompras_Load);
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRequerimientosCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequerimientosComprasVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl dtgRequerimientosCompras;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRequerimientosComprasVista;
        private System.Windows.Forms.CheckBox chkResumen;

    }
}