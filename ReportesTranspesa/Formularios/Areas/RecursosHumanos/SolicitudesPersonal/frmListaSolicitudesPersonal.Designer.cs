namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.SolicitudesPersonal
{
    partial class frmListaSolicitudesPersonal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaSolicitudesPersonal));
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.btnNuevaSolicitud = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgListaSolicitudes = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aprobarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fechaDeEntregaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listaCandidatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaSolicitudesVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbFiltros.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaSolicitudes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaSolicitudesVista)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.groupBox2);
            this.gbFiltros.Controls.Add(this.groupBox1);
            this.gbFiltros.Controls.Add(this.groupBox14);
            this.gbFiltros.Controls.Add(this.btnNuevaSolicitud);
            this.gbFiltros.Controls.Add(this.btnExcel);
            this.gbFiltros.Controls.Add(this.btnBuscar);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbFiltros.Location = new System.Drawing.Point(0, 0);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(1095, 111);
            this.gbFiltros.TabIndex = 0;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Búsqueda";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxEstado);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(672, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 61);
            this.groupBox2.TabIndex = 114;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccionar Estado:";
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Location = new System.Drawing.Point(20, 24);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(163, 23);
            this.cbxEstado.TabIndex = 111;
            this.cbxEstado.SelectedIndexChanged += new System.EventHandler(this.cbxEstado_SelectedIndexChanged);
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaIni);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(423, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 61);
            this.groupBox1.TabIndex = 113;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por Fecha de Solicitud:";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(15, 24);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(87, 21);
            this.dtpFechaIni.TabIndex = 1;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(108, 26);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(15, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Black;
            this.metroLabel3.TabIndex = 92;
            this.metroLabel3.Text = "-";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(129, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(87, 21);
            this.dtpFechaFin.TabIndex = 2;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.cbxArea);
            this.groupBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(202, 27);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(204, 61);
            this.groupBox14.TabIndex = 112;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar por Área:";
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(20, 24);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(163, 23);
            this.cbxArea.TabIndex = 111;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            this.cbxArea.DropDownClosed += new System.EventHandler(this.cbxArea_DropDownClosed);
            // 
            // btnNuevaSolicitud
            // 
            this.btnNuevaSolicitud.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevaSolicitud.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevaSolicitud.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevaSolicitud.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaSolicitud.Appearance.Options.UseBackColor = true;
            this.btnNuevaSolicitud.Appearance.Options.UseBorderColor = true;
            this.btnNuevaSolicitud.Appearance.Options.UseFont = true;
            this.btnNuevaSolicitud.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaSolicitud.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaSolicitud.Image")));
            this.btnNuevaSolicitud.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevaSolicitud.Location = new System.Drawing.Point(24, 36);
            this.btnNuevaSolicitud.Name = "btnNuevaSolicitud";
            this.btnNuevaSolicitud.Size = new System.Drawing.Size(140, 47);
            this.btnNuevaSolicitud.TabIndex = 100;
            this.btnNuevaSolicitud.Text = "Nueva Solicitud";
            this.btnNuevaSolicitud.ToolTip = "Nueva Solicitud";
            this.btnNuevaSolicitud.Click += new System.EventHandler(this.btnNuevaSolicitud_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(1022, 36);
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
            this.btnBuscar.Location = new System.Drawing.Point(961, 36);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgListaSolicitudes
            // 
            this.dtgListaSolicitudes.CausesValidation = false;
            this.dtgListaSolicitudes.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaSolicitudes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaSolicitudes.Location = new System.Drawing.Point(20, 197);
            this.dtgListaSolicitudes.MainView = this.dgvListaSolicitudesVista;
            this.dtgListaSolicitudes.Name = "dtgListaSolicitudes";
            this.dtgListaSolicitudes.Size = new System.Drawing.Size(1095, 314);
            this.dtgListaSolicitudes.TabIndex = 100;
            this.dtgListaSolicitudes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaSolicitudesVista});
            this.dtgListaSolicitudes.DoubleClick += new System.EventHandler(this.dtgListaSolicitudes_DoubleClick);
            this.dtgListaSolicitudes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaSolicitudes_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aprobarToolStripMenuItem,
            this.fechaDeEntregaToolStripMenuItem,
            this.listaCandidatosToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 70);
            // 
            // aprobarToolStripMenuItem
            // 
            this.aprobarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.liberar;
            this.aprobarToolStripMenuItem.Name = "aprobarToolStripMenuItem";
            this.aprobarToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.aprobarToolStripMenuItem.Text = "Aprobar";
            this.aprobarToolStripMenuItem.Click += new System.EventHandler(this.aprobarToolStripMenuItem_Click);
            // 
            // fechaDeEntregaToolStripMenuItem
            // 
            this.fechaDeEntregaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.fechaDeEntregaToolStripMenuItem.Name = "fechaDeEntregaToolStripMenuItem";
            this.fechaDeEntregaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.fechaDeEntregaToolStripMenuItem.Text = "Fecha de Entrega";
            this.fechaDeEntregaToolStripMenuItem.Click += new System.EventHandler(this.fechaDeEntregaToolStripMenuItem_Click);
            // 
            // listaCandidatosToolStripMenuItem
            // 
            this.listaCandidatosToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.persona_logo_icon_169946;
            this.listaCandidatosToolStripMenuItem.Name = "listaCandidatosToolStripMenuItem";
            this.listaCandidatosToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.listaCandidatosToolStripMenuItem.Text = "Lista de Candidatos";
            this.listaCandidatosToolStripMenuItem.Click += new System.EventHandler(this.listaCandidatosToolStripMenuItem_Click);
            // 
            // dgvListaSolicitudesVista
            // 
            this.dgvListaSolicitudesVista.GridControl = this.dtgListaSolicitudes;
            this.dgvListaSolicitudesVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvListaSolicitudesVista.Name = "dgvListaSolicitudesVista";
            this.dgvListaSolicitudesVista.OptionsBehavior.Editable = false;
            this.dgvListaSolicitudesVista.OptionsBehavior.ReadOnly = true;
            this.dgvListaSolicitudesVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaSolicitudesVista.OptionsView.RowAutoHeight = true;
            this.dgvListaSolicitudesVista.OptionsView.ShowFooter = true;
            this.dgvListaSolicitudesVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaSolicitudesVista_CustomDrawCell);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbFiltros);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 111);
            this.panel1.TabIndex = 101;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1095, 26);
            this.panel2.TabIndex = 102;
            // 
            // frmListaSolicitudesPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 531);
            this.Controls.Add(this.dtgListaSolicitudes);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmListaSolicitudesPersonal";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "LISTA DE SOLICITUDES DE PERSONAL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaSolicitudesPersonal_Load);
            this.gbFiltros.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaSolicitudes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaSolicitudesVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private DevExpress.XtraGrid.GridControl dtgListaSolicitudes;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaSolicitudesVista;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem listaCandidatosToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnNuevaSolicitud;
        private System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ToolStripMenuItem aprobarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fechaDeEntregaToolStripMenuItem;
    }
}