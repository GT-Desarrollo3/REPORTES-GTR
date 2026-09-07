namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class Buscar_Facturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buscar_Facturas));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.cbxFiltro = new MetroFramework.Controls.MetroComboBox();
            this.txtBusqueda = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtCliente = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.cboCompania = new MetroFramework.Controls.MetroComboBox();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.lvCliente = new System.Windows.Forms.ListView();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel4);
            this.splitContainer1.Panel1.Controls.Add(this.cbxFiltro);
            this.splitContainer1.Panel1.Controls.Add(this.txtBusqueda);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.txtCliente);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel10);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompania);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel3);
            this.splitContainer1.Panel1.Controls.Add(this.lvCliente);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(912, 557);
            this.splitContainer1.SplitterDistance = 108;
            this.splitContainer1.TabIndex = 2;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(31, 69);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(43, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel4.TabIndex = 86;
            this.metroLabel4.Text = "Filtro.";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cbxFiltro
            // 
            this.cbxFiltro.FormattingEnabled = true;
            this.cbxFiltro.ItemHeight = 23;
            this.cbxFiltro.Items.AddRange(new object[] {
            "CON DETALLE (sin IGV)",
            "SIN DETALLE  (con IGV)"});
            this.cbxFiltro.Location = new System.Drawing.Point(80, 69);
            this.cbxFiltro.Name = "cbxFiltro";
            this.cbxFiltro.Size = new System.Drawing.Size(187, 29);
            this.cbxFiltro.Style = MetroFramework.MetroColorStyle.Red;
            this.cbxFiltro.TabIndex = 74;
            this.cbxFiltro.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cbxFiltro.UseSelectable = true;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Lines = new string[0];
            this.txtBusqueda.Location = new System.Drawing.Point(340, 38);
            this.txtBusqueda.MaxLength = 32767;
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.PasswordChar = '\0';
            this.txtBusqueda.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBusqueda.SelectedText = "";
            this.txtBusqueda.Size = new System.Drawing.Size(320, 29);
            this.txtBusqueda.Style = MetroFramework.MetroColorStyle.Red;
            this.txtBusqueda.TabIndex = 85;
            this.txtBusqueda.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBusqueda.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(283, 39);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(49, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 84;
            this.metroLabel2.Text = "Buscar";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(3, 41);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(73, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 83;
            this.metroLabel1.Text = "Fecha doc.";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtCliente
            // 
            this.txtCliente.Lines = new string[0];
            this.txtCliente.Location = new System.Drawing.Point(340, 3);
            this.txtCliente.MaxLength = 32767;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PasswordChar = '\0';
            this.txtCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCliente.SelectedText = "";
            this.txtCliente.Size = new System.Drawing.Size(320, 29);
            this.txtCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCliente.TabIndex = 82;
            this.txtCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCliente.UseSelectable = true;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.Location = new System.Drawing.Point(283, 7);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(51, 19);
            this.metroLabel10.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel10.TabIndex = 81;
            this.metroLabel10.Text = "Cliente";
            this.metroLabel10.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cboCompania
            // 
            this.cboCompania.FormattingEnabled = true;
            this.cboCompania.ItemHeight = 23;
            this.cboCompania.Items.AddRange(new object[] {
            "TODOS",
            "GRUPO TRANSPESA",
            "FABRICACIONES BRA",
            "ALMACENES ALTRA",
            "CONSORCIO AMT",
            "AGENCIA DE ADUANAS",
            "INOMAC"});
            this.cboCompania.Location = new System.Drawing.Point(80, 3);
            this.cboCompania.Name = "cboCompania";
            this.cboCompania.Size = new System.Drawing.Size(187, 29);
            this.cboCompania.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCompania.TabIndex = 73;
            this.cboCompania.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboCompania.UseSelectable = true;
            this.cboCompania.SelectedIndexChanged += new System.EventHandler(this.cboCompania_SelectedIndexChanged);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(177, 39);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(90, 25);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 71;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(80, 38);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(91, 25);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 70;
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
            this.btnImprimir.Location = new System.Drawing.Point(855, 28);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 58;
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
            this.btnExcel.Location = new System.Drawing.Point(776, 28);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 57;
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
            this.btnBuscar.Location = new System.Drawing.Point(698, 27);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 50;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(3, 7);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(71, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 52;
            this.metroLabel3.Text = "Compañía";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lvCliente
            // 
            this.lvCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.lvCliente.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lvCliente.FullRowSelect = true;
            this.lvCliente.GridLines = true;
            this.lvCliente.Location = new System.Drawing.Point(340, 42);
            this.lvCliente.MultiSelect = false;
            this.lvCliente.Name = "lvCliente";
            this.lvCliente.Size = new System.Drawing.Size(320, 261);
            this.lvCliente.TabIndex = 0;
            this.lvCliente.UseCompatibleStateImageBehavior = false;
            this.lvCliente.View = System.Windows.Forms.View.Details;
            this.lvCliente.Visible = false;
            this.lvCliente.Enter += new System.EventHandler(this.lvCliente_Enter);
            this.lvCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvCliente_KeyPress);
            this.lvCliente.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvCliente_MouseDoubleClick);
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(912, 445);
            this.dtgvData.TabIndex = 1;
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
            // 
            // Buscar_Facturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 637);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Buscar_Facturas";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Buscar Facturas";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Buscar_Facturas_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroTextBox txtBusqueda;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtCliente;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroComboBox cboCompania;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.ListView lvCliente;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox cbxFiltro;
    }
}