namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmListarGuiasFisicas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarGuiasFisicas));
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroGuia = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.cbxSerieGuia = new System.Windows.Forms.ComboBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgListaGuiasFisicas = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsSolicitarAnulacion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAnularGuia = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaGuiasFisicasVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbFiltros.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuiasFisicas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiasFisicasVista)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.cbxEstado);
            this.gbFiltros.Controls.Add(this.metroLabel3);
            this.gbFiltros.Controls.Add(this.label5);
            this.gbFiltros.Controls.Add(this.groupBox1);
            this.gbFiltros.Controls.Add(this.dtpFechaFin);
            this.gbFiltros.Controls.Add(this.dtpFechaIni);
            this.gbFiltros.Controls.Add(this.btnExcel);
            this.gbFiltros.Controls.Add(this.metroLabel2);
            this.gbFiltros.Controls.Add(this.btnBuscar);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbFiltros.Location = new System.Drawing.Point(20, 60);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(905, 138);
            this.gbFiltros.TabIndex = 108;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtro de Búsqueda";
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODAS",
            "PENDIENTE",
            "ANULADA"});
            this.cbxEstado.Location = new System.Drawing.Point(132, 85);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(174, 24);
            this.cbxEstado.TabIndex = 221;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.White;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(20, 87);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(112, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 118;
            this.metroLabel3.Text = "Estado Solicitud: ";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(183, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 16);
            this.label5.TabIndex = 116;
            this.label5.Text = "--";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNumeroGuia);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.cbxSerieGuia);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(337, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 85);
            this.groupBox1.TabIndex = 117;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(124, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 221;
            this.label1.Text = "Número:";
            // 
            // txtNumeroGuia
            // 
            this.txtNumeroGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroGuia.Location = new System.Drawing.Point(127, 46);
            this.txtNumeroGuia.Name = "txtNumeroGuia";
            this.txtNumeroGuia.Size = new System.Drawing.Size(130, 22);
            this.txtNumeroGuia.TabIndex = 205;
            this.txtNumeroGuia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroGuia_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(14, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 16);
            this.label16.TabIndex = 219;
            this.label16.Text = "Serie:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.White;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(8, -4);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(50, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 95;
            this.metroLabel1.Text = "Guías: ";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // cbxSerieGuia
            // 
            this.cbxSerieGuia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSerieGuia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSerieGuia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSerieGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSerieGuia.FormattingEnabled = true;
            this.cbxSerieGuia.Items.AddRange(new object[] {
            "TODAS",
            "EG03",
            "V011",
            "V013",
            "V015",
            "V016",
            "V017",
            "0002",
            "028",
            "030"});
            this.cbxSerieGuia.Location = new System.Drawing.Point(17, 45);
            this.cbxSerieGuia.Name = "cbxSerieGuia";
            this.cbxSerieGuia.Size = new System.Drawing.Size(91, 24);
            this.cbxSerieGuia.TabIndex = 220;
            this.cbxSerieGuia.SelectedIndexChanged += new System.EventHandler(this.cbxSerieGuia_SelectedIndexChanged);
            this.cbxSerieGuia.DropDownClosed += new System.EventHandler(this.cbxSerieGuia_DropDownClosed);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(203, 44);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaFin.TabIndex = 93;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(78, 44);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaIni.TabIndex = 92;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(702, 51);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 7;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.White;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(20, 45);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(57, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 95;
            this.metroLabel2.Text = "Fechas: ";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
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
            this.btnBuscar.Location = new System.Drawing.Point(643, 51);
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
            this.panel2.Location = new System.Drawing.Point(20, 198);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(905, 20);
            this.panel2.TabIndex = 134;
            // 
            // dtgListaGuiasFisicas
            // 
            this.dtgListaGuiasFisicas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaGuiasFisicas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaGuiasFisicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaGuiasFisicas.Location = new System.Drawing.Point(20, 218);
            this.dtgListaGuiasFisicas.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dtgListaGuiasFisicas.LookAndFeel.SkinName = "Stardust";
            this.dtgListaGuiasFisicas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaGuiasFisicas.MainView = this.dgvListaGuiasFisicasVista;
            this.dtgListaGuiasFisicas.Name = "dtgListaGuiasFisicas";
            this.dtgListaGuiasFisicas.Size = new System.Drawing.Size(905, 384);
            this.dtgListaGuiasFisicas.TabIndex = 135;
            this.dtgListaGuiasFisicas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaGuiasFisicasVista});
            this.dtgListaGuiasFisicas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaGuiasTransportista_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSolicitarAnulacion,
            this.tsAnularGuia});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 48);
            // 
            // tsSolicitarAnulacion
            // 
            this.tsSolicitarAnulacion.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.tsSolicitarAnulacion.Name = "tsSolicitarAnulacion";
            this.tsSolicitarAnulacion.Size = new System.Drawing.Size(173, 22);
            this.tsSolicitarAnulacion.Text = "Solicitar Anulación";
            this.tsSolicitarAnulacion.Click += new System.EventHandler(this.tsSolicitarAnulacion_Click);
            // 
            // tsAnularGuia
            // 
            this.tsAnularGuia.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsAnularGuia.Name = "tsAnularGuia";
            this.tsAnularGuia.Size = new System.Drawing.Size(173, 22);
            this.tsAnularGuia.Text = "Anular Guía";
            this.tsAnularGuia.Click += new System.EventHandler(this.tsAnularGuia_Click);
            // 
            // dgvListaGuiasFisicasVista
            // 
            this.dgvListaGuiasFisicasVista.GridControl = this.dtgListaGuiasFisicas;
            this.dgvListaGuiasFisicasVista.Name = "dgvListaGuiasFisicasVista";
            this.dgvListaGuiasFisicasVista.OptionsBehavior.Editable = false;
            this.dgvListaGuiasFisicasVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaGuiasFisicasVista.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.dgvListaGuiasFisicasVista.OptionsView.RowAutoHeight = true;
            this.dgvListaGuiasFisicasVista.OptionsView.ShowFooter = true;
            this.dgvListaGuiasFisicasVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaGuiasFisicasVista_CustomDrawCell);
            // 
            // frmListarGuiasFisicas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 622);
            this.Controls.Add(this.dtgListaGuiasFisicas);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbFiltros);
            this.Name = "frmListarGuiasFisicas";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "LISTA DE GUÍAS FÍSICAS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListarGuiasFisicas_Load);
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuiasFisicas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiasFisicasVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroGuia;
        private System.Windows.Forms.Label label16;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.ComboBox cbxSerieGuia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl dtgListaGuiasFisicas;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaGuiasFisicasVista;
        private System.Windows.Forms.ComboBox cbxEstado;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsSolicitarAnulacion;
        public System.Windows.Forms.ToolStripMenuItem tsAnularGuia;
    }
}