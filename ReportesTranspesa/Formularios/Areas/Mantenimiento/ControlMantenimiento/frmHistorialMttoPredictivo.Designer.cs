namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmHistorialMttoPredictivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistorialMttoPredictivo));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbxMPSistema = new System.Windows.Forms.ComboBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.cbxMPTecnica = new System.Windows.Forms.ComboBox();
            this.label87 = new System.Windows.Forms.Label();
            this.txtMPUnidad = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.cbxMPTipo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgHistorialMtto = new DevExpress.XtraGrid.GridControl();
            this.dgvHistorialMttoVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialMtto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialMttoVista)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(907, 50);
            this.label1.TabIndex = 13;
            this.label1.Text = "HISTORIAL DE MTTOS. PREDICTIVOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.cbxMPSistema);
            this.panel4.Controls.Add(this.label90);
            this.panel4.Controls.Add(this.label89);
            this.panel4.Controls.Add(this.cbxMPTecnica);
            this.panel4.Controls.Add(this.label87);
            this.panel4.Controls.Add(this.txtMPUnidad);
            this.panel4.Controls.Add(this.label88);
            this.panel4.Controls.Add(this.cbxMPTipo);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(907, 89);
            this.panel4.TabIndex = 14;
            // 
            // cbxMPSistema
            // 
            this.cbxMPSistema.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxMPSistema.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMPSistema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMPSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMPSistema.FormattingEnabled = true;
            this.cbxMPSistema.Location = new System.Drawing.Point(529, 51);
            this.cbxMPSistema.Name = "cbxMPSistema";
            this.cbxMPSistema.Size = new System.Drawing.Size(140, 21);
            this.cbxMPSistema.TabIndex = 233;
            this.cbxMPSistema.SelectedIndexChanged += new System.EventHandler(this.cbxMPSistema_SelectedIndexChanged);
            this.cbxMPSistema.DropDownClosed += new System.EventHandler(this.cbxMPSistema_DropDownClosed);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(476, 55);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(47, 13);
            this.label90.TabIndex = 232;
            this.label90.Text = "Sistema:";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(468, 19);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(55, 26);
            this.label89.TabIndex = 230;
            this.label89.Text = "Técnica a\r\nControlar:";
            // 
            // cbxMPTecnica
            // 
            this.cbxMPTecnica.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxMPTecnica.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMPTecnica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMPTecnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMPTecnica.FormattingEnabled = true;
            this.cbxMPTecnica.Location = new System.Drawing.Point(529, 22);
            this.cbxMPTecnica.Name = "cbxMPTecnica";
            this.cbxMPTecnica.Size = new System.Drawing.Size(193, 21);
            this.cbxMPTecnica.TabIndex = 231;
            this.cbxMPTecnica.SelectedIndexChanged += new System.EventHandler(this.cbxMPTecnica_SelectedIndexChanged);
            this.cbxMPTecnica.DropDownClosed += new System.EventHandler(this.cbxMPTecnica_DropDownClosed);
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(279, 55);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(31, 13);
            this.label87.TabIndex = 229;
            this.label87.Text = "Tipo:";
            // 
            // txtMPUnidad
            // 
            this.txtMPUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPUnidad.Location = new System.Drawing.Point(316, 22);
            this.txtMPUnidad.Name = "txtMPUnidad";
            this.txtMPUnidad.Size = new System.Drawing.Size(127, 20);
            this.txtMPUnidad.TabIndex = 226;
            this.txtMPUnidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMPUnidad_KeyPress);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(266, 25);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(44, 13);
            this.label88.TabIndex = 228;
            this.label88.Text = "Unidad:";
            // 
            // cbxMPTipo
            // 
            this.cbxMPTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxMPTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMPTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMPTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMPTipo.FormattingEnabled = true;
            this.cbxMPTipo.Location = new System.Drawing.Point(316, 51);
            this.cbxMPTipo.Name = "cbxMPTipo";
            this.cbxMPTipo.Size = new System.Drawing.Size(127, 21);
            this.cbxMPTipo.TabIndex = 227;
            this.cbxMPTipo.SelectedIndexChanged += new System.EventHandler(this.cbxMPTipo_SelectedIndexChanged);
            this.cbxMPTipo.DropDownClosed += new System.EventHandler(this.cbxMPTipo_DropDownClosed);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Location = new System.Drawing.Point(18, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 58);
            this.groupBox1.TabIndex = 213;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha de Mantenimiento:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(123, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "--";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(9, 24);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnExcel.Location = new System.Drawing.Point(836, 21);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 212;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnBuscar.Location = new System.Drawing.Point(779, 21);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgHistorialMtto
            // 
            this.dtgHistorialMtto.AllowDrop = true;
            this.dtgHistorialMtto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgHistorialMtto.Location = new System.Drawing.Point(0, 139);
            this.dtgHistorialMtto.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dtgHistorialMtto.LookAndFeel.SkinName = "Stardust";
            this.dtgHistorialMtto.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgHistorialMtto.MainView = this.dgvHistorialMttoVista;
            this.dtgHistorialMtto.Name = "dtgHistorialMtto";
            this.dtgHistorialMtto.Size = new System.Drawing.Size(907, 356);
            this.dtgHistorialMtto.TabIndex = 15;
            this.dtgHistorialMtto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvHistorialMttoVista});
            this.dtgHistorialMtto.DoubleClick += new System.EventHandler(this.dtgHistorialMtto_DoubleClick);
            // 
            // dgvHistorialMttoVista
            // 
            this.dgvHistorialMttoVista.GridControl = this.dtgHistorialMtto;
            this.dgvHistorialMttoVista.Name = "dgvHistorialMttoVista";
            this.dgvHistorialMttoVista.OptionsBehavior.Editable = false;
            this.dgvHistorialMttoVista.OptionsView.ColumnAutoWidth = false;
            this.dgvHistorialMttoVista.OptionsView.RowAutoHeight = true;
            this.dgvHistorialMttoVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvHistorialMttoVista_CustomDrawCell);
            // 
            // frmHistorialMttoPredictivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 495);
            this.Controls.Add(this.dtgHistorialMtto);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Name = "frmHistorialMttoPredictivo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HISTORIAL MTTO. PREDICTIVO";
            this.Load += new System.EventHandler(this.frmHistorialMttoPredictivo_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialMtto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialMttoVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgHistorialMtto;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvHistorialMttoVista;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label88;
        public System.Windows.Forms.ComboBox cbxMPTipo;
        public System.Windows.Forms.ComboBox cbxMPSistema;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label89;
        public System.Windows.Forms.ComboBox cbxMPTecnica;
        public System.Windows.Forms.TextBox txtMPUnidad;
    }
}