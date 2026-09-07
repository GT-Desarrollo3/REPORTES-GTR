namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    partial class frmMovTransformacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMovTransformacion));
            this.dtgTransformacion = new DevExpress.XtraGrid.GridControl();
            this.dgvTransformacionVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.txtLote = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txtProducto = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.txtCliente = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransformacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransformacionVista)).BeginInit();
            this.gbFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgTransformacion
            // 
            this.dtgTransformacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTransformacion.Location = new System.Drawing.Point(20, 199);
            this.dtgTransformacion.LookAndFeel.SkinName = "Darkroom";
            this.dtgTransformacion.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTransformacion.MainView = this.dgvTransformacionVista;
            this.dtgTransformacion.Name = "dtgTransformacion";
            this.dtgTransformacion.Size = new System.Drawing.Size(1100, 345);
            this.dtgTransformacion.TabIndex = 139;
            this.dtgTransformacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTransformacionVista});
            // 
            // dgvTransformacionVista
            // 
            this.dgvTransformacionVista.GridControl = this.dtgTransformacion;
            this.dgvTransformacionVista.Name = "dgvTransformacionVista";
            this.dgvTransformacionVista.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvTransformacionVista.OptionsBehavior.Editable = false;
            this.dgvTransformacionVista.OptionsView.ColumnAutoWidth = false;
            this.dgvTransformacionVista.OptionsView.ShowFooter = true;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 179);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1100, 20);
            this.panel2.TabIndex = 138;
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.txtLote);
            this.gbFiltros.Controls.Add(this.metroLabel3);
            this.gbFiltros.Controls.Add(this.txtProducto);
            this.gbFiltros.Controls.Add(this.metroLabel5);
            this.gbFiltros.Controls.Add(this.txtCliente);
            this.gbFiltros.Controls.Add(this.metroLabel4);
            this.gbFiltros.Controls.Add(this.dtpFechaFin);
            this.gbFiltros.Controls.Add(this.dtpFechaIni);
            this.gbFiltros.Controls.Add(this.metroLabel1);
            this.gbFiltros.Controls.Add(this.metroLabel2);
            this.gbFiltros.Controls.Add(this.btnExcel);
            this.gbFiltros.Controls.Add(this.btnBuscar);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.Color.Black;
            this.gbFiltros.Location = new System.Drawing.Point(20, 60);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(1100, 119);
            this.gbFiltros.TabIndex = 137;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtro de Búsqueda";
            // 
            // txtLote
            // 
            this.txtLote.BackColor = System.Drawing.Color.White;
            this.txtLote.Lines = new string[0];
            this.txtLote.Location = new System.Drawing.Point(452, 73);
            this.txtLote.MaxLength = 32767;
            this.txtLote.Name = "txtLote";
            this.txtLote.PasswordChar = '\0';
            this.txtLote.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLote.SelectedText = "";
            this.txtLote.Size = new System.Drawing.Size(273, 25);
            this.txtLote.TabIndex = 106;
            this.txtLote.Tag = "4";
            this.txtLote.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtLote.UseSelectable = true;
            this.txtLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLote_KeyPress);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(407, 75);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(39, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 105;
            this.metroLabel3.Text = "Lote:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtProducto
            // 
            this.txtProducto.BackColor = System.Drawing.Color.White;
            this.txtProducto.Lines = new string[0];
            this.txtProducto.Location = new System.Drawing.Point(452, 35);
            this.txtProducto.MaxLength = 32767;
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.PasswordChar = '\0';
            this.txtProducto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProducto.SelectedText = "";
            this.txtProducto.Size = new System.Drawing.Size(273, 25);
            this.txtProducto.TabIndex = 104;
            this.txtProducto.Tag = "4";
            this.txtProducto.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProducto.UseSelectable = true;
            this.txtProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProducto_KeyPress);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(378, 37);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(68, 19);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel5.TabIndex = 103;
            this.metroLabel5.Text = "Producto:";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtCliente
            // 
            this.txtCliente.BackColor = System.Drawing.Color.White;
            this.txtCliente.Lines = new string[0];
            this.txtCliente.Location = new System.Drawing.Point(82, 73);
            this.txtCliente.MaxLength = 32767;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PasswordChar = '\0';
            this.txtCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCliente.SelectedText = "";
            this.txtCliente.Size = new System.Drawing.Size(273, 25);
            this.txtCliente.TabIndex = 102;
            this.txtCliente.Tag = "4";
            this.txtCliente.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCliente.UseSelectable = true;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(22, 75);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(54, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel4.TabIndex = 101;
            this.metroLabel4.Text = "Cliente:";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(252, 32);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 97;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(126, 32);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 96;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(230, 32);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(21, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 98;
            this.metroLabel1.Text = "__";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.White;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(22, 37);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(98, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 95;
            this.metroLabel2.Text = "Fecha Emisión:";
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
            this.btnExcel.Location = new System.Drawing.Point(826, 41);
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
            this.btnBuscar.Location = new System.Drawing.Point(766, 41);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // frmMovTransformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 564);
            this.Controls.Add(this.dtgTransformacion);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbFiltros);
            this.Name = "frmMovTransformacion";
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "REPORTE DE TRANSFORMACIONES - SALAVERRY";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMovTransformacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransformacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransformacionVista)).EndInit();
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgTransformacion;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTransformacionVista;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbFiltros;
        private MetroFramework.Controls.MetroTextBox txtProducto;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroTextBox txtCliente;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroTextBox txtLote;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}