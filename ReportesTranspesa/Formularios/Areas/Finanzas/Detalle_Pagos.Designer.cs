namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    partial class Detalle_Pagos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Detalle_Pagos));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbEmpresa = new System.Windows.Forms.GroupBox();
            this.cbxEmpresa = new System.Windows.Forms.ComboBox();
            this.gbProveedor = new System.Windows.Forms.GroupBox();
            this.txtCliente = new MetroFramework.Controls.MetroTextBox();
            this.gbPago = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.rbPagados = new System.Windows.Forms.RadioButton();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.rbPendientes = new System.Windows.Forms.RadioButton();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lvCliente = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbEmpresa.SuspendLayout();
            this.gbProveedor.SuspendLayout();
            this.gbPago.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.gbEmpresa);
            this.splitContainer1.Panel1.Controls.Add(this.gbProveedor);
            this.splitContainer1.Panel1.Controls.Add(this.gbPago);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(1216, 575);
            this.splitContainer1.SplitterDistance = 83;
            this.splitContainer1.TabIndex = 2;
            // 
            // gbEmpresa
            // 
            this.gbEmpresa.Controls.Add(this.cbxEmpresa);
            this.gbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gbEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbEmpresa.Location = new System.Drawing.Point(22, 8);
            this.gbEmpresa.Name = "gbEmpresa";
            this.gbEmpresa.Size = new System.Drawing.Size(328, 69);
            this.gbEmpresa.TabIndex = 90;
            this.gbEmpresa.TabStop = false;
            this.gbEmpresa.Text = "EMPRESA";
            // 
            // cbxEmpresa
            // 
            this.cbxEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEmpresa.FormattingEnabled = true;
            this.cbxEmpresa.Location = new System.Drawing.Point(20, 28);
            this.cbxEmpresa.Name = "cbxEmpresa";
            this.cbxEmpresa.Size = new System.Drawing.Size(291, 21);
            this.cbxEmpresa.TabIndex = 0;
            // 
            // gbProveedor
            // 
            this.gbProveedor.Controls.Add(this.txtCliente);
            this.gbProveedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbProveedor.Location = new System.Drawing.Point(703, 8);
            this.gbProveedor.Name = "gbProveedor";
            this.gbProveedor.Size = new System.Drawing.Size(349, 69);
            this.gbProveedor.TabIndex = 89;
            this.gbProveedor.TabStop = false;
            this.gbProveedor.Text = "PROVEEDOR:";
            // 
            // txtCliente
            // 
            this.txtCliente.Lines = new string[0];
            this.txtCliente.Location = new System.Drawing.Point(15, 21);
            this.txtCliente.MaxLength = 32767;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PasswordChar = '\0';
            this.txtCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCliente.SelectedText = "";
            this.txtCliente.Size = new System.Drawing.Size(320, 29);
            this.txtCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCliente.TabIndex = 82;
            this.txtCliente.UseSelectable = true;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // gbPago
            // 
            this.gbPago.Controls.Add(this.dtpFechaFin);
            this.gbPago.Controls.Add(this.rbPagados);
            this.gbPago.Controls.Add(this.dtpFechaIni);
            this.gbPago.Controls.Add(this.rbPendientes);
            this.gbPago.Controls.Add(this.metroLabel7);
            this.gbPago.Controls.Add(this.metroLabel2);
            this.gbPago.Controls.Add(this.metroLabel1);
            this.gbPago.Location = new System.Drawing.Point(367, 8);
            this.gbPago.Name = "gbPago";
            this.gbPago.Size = new System.Drawing.Size(318, 69);
            this.gbPago.TabIndex = 88;
            this.gbPago.TabStop = false;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(204, 38);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(93, 25);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Blue;
            this.dtpFechaFin.TabIndex = 71;
            // 
            // rbPagados
            // 
            this.rbPagados.AutoSize = true;
            this.rbPagados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.rbPagados.Location = new System.Drawing.Point(173, -1);
            this.rbPagados.Name = "rbPagados";
            this.rbPagados.Size = new System.Drawing.Size(77, 17);
            this.rbPagados.TabIndex = 87;
            this.rbPagados.Text = "PAGADOS";
            this.rbPagados.UseVisualStyleBackColor = true;
            this.rbPagados.CheckedChanged += new System.EventHandler(this.rbPagados_CheckedChanged);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CalendarMonthBackground = System.Drawing.Color.Black;
            this.dtpFechaIni.CalendarTitleBackColor = System.Drawing.Color.Cyan;
            this.dtpFechaIni.CalendarTrailingForeColor = System.Drawing.Color.Black;
            this.dtpFechaIni.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(55, 38);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 25);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(101, 25);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Black;
            this.dtpFechaIni.TabIndex = 70;
            // 
            // rbPendientes
            // 
            this.rbPendientes.AutoSize = true;
            this.rbPendientes.Checked = true;
            this.rbPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.rbPendientes.Location = new System.Drawing.Point(6, -1);
            this.rbPendientes.Name = "rbPendientes";
            this.rbPendientes.Size = new System.Drawing.Size(120, 17);
            this.rbPendientes.TabIndex = 86;
            this.rbPendientes.TabStop = true;
            this.rbPendientes.Text = "PENDIENTE PAGO";
            this.rbPendientes.UseVisualStyleBackColor = true;
            this.rbPendientes.CheckedChanged += new System.EventHandler(this.rbPendientes_CheckedChanged);
            // 
            // metroLabel7
            // 
            this.metroLabel7.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.Location = new System.Drawing.Point(21, 20);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(258, 20);
            this.metroLabel7.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel7.TabIndex = 83;
            this.metroLabel7.Text = "Fecha:  VENCIMIENTO";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(179, 44);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(24, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.TabIndex = 85;
            this.metroLabel2.Text = "Al:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(19, 44);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(32, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.TabIndex = 84;
            this.metroLabel1.Text = "Del:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Appearance.BackColor = System.Drawing.Color.White;
            this.btnImprimir.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnImprimir.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseBackColor = true;
            this.btnImprimir.Appearance.Options.UseBorderColor = true;
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnImprimir.Location = new System.Drawing.Point(1155, 33);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 58;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(1107, 33);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 57;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.Gray;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBuscar.Location = new System.Drawing.Point(1061, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 50;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Money Twins";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(1216, 488);
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
            // 
            // lvCliente
            // 
            this.lvCliente.BackColor = System.Drawing.Color.LightBlue;
            this.lvCliente.ForeColor = System.Drawing.Color.Black;
            this.lvCliente.FullRowSelect = true;
            this.lvCliente.GridLines = true;
            this.lvCliente.Location = new System.Drawing.Point(738, 124);
            this.lvCliente.MultiSelect = false;
            this.lvCliente.Name = "lvCliente";
            this.lvCliente.Size = new System.Drawing.Size(320, 149);
            this.lvCliente.TabIndex = 0;
            this.lvCliente.UseCompatibleStateImageBehavior = false;
            this.lvCliente.View = System.Windows.Forms.View.Details;
            this.lvCliente.Visible = false;
            this.lvCliente.SelectedIndexChanged += new System.EventHandler(this.lvCliente_SelectedIndexChanged);
            this.lvCliente.Enter += new System.EventHandler(this.lvCliente_Enter);
            this.lvCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvCliente_KeyPress);
            this.lvCliente.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvCliente_MouseDoubleClick);
            // 
            // Detalle_Pagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 655);
            this.Controls.Add(this.lvCliente);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Detalle_Pagos";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "DETALLE DE PAGOS";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Detalle_Pagos_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbEmpresa.ResumeLayout(false);
            this.gbProveedor.ResumeLayout(false);
            this.gbPago.ResumeLayout(false);
            this.gbPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroTextBox txtCliente;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.ListView lvCliente;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.RadioButton rbPagados;
        private System.Windows.Forms.RadioButton rbPendientes;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.GroupBox gbPago;
        private System.Windows.Forms.GroupBox gbEmpresa;
        private System.Windows.Forms.ComboBox cbxEmpresa;
        private System.Windows.Forms.GroupBox gbProveedor;
    }
}