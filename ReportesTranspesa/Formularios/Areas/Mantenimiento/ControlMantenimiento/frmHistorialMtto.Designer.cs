namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmHistorialMtto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistorialMtto));
            this.dtgHistorialMtto = new DevExpress.XtraGrid.GridControl();
            this.dgvHistorialMttoVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbxTipoEquipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.cbxTipoMaquina = new System.Windows.Forms.ComboBox();
            this.cbxTipoUnidad = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialMtto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialMttoVista)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgHistorialMtto
            // 
            this.dtgHistorialMtto.AllowDrop = true;
            this.dtgHistorialMtto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgHistorialMtto.Location = new System.Drawing.Point(0, 139);
            this.dtgHistorialMtto.MainView = this.dgvHistorialMttoVista;
            this.dtgHistorialMtto.Name = "dtgHistorialMtto";
            this.dtgHistorialMtto.Size = new System.Drawing.Size(890, 376);
            this.dtgHistorialMtto.TabIndex = 14;
            this.dtgHistorialMtto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvHistorialMttoVista});
            // 
            // dgvHistorialMttoVista
            // 
            this.dgvHistorialMttoVista.GridControl = this.dtgHistorialMtto;
            this.dgvHistorialMttoVista.Name = "dgvHistorialMttoVista";
            this.dgvHistorialMttoVista.OptionsBehavior.Editable = false;
            this.dgvHistorialMttoVista.OptionsView.ColumnAutoWidth = false;
            this.dgvHistorialMttoVista.OptionsView.RowAutoHeight = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.cbxTipoEquipo);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.cbxTipoMaquina);
            this.panel4.Controls.Add(this.cbxTipoUnidad);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(890, 89);
            this.panel4.TabIndex = 13;
            // 
            // cbxTipoEquipo
            // 
            this.cbxTipoEquipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoEquipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoEquipo.FormattingEnabled = true;
            this.cbxTipoEquipo.Location = new System.Drawing.Point(495, 39);
            this.cbxTipoEquipo.Name = "cbxTipoEquipo";
            this.cbxTipoEquipo.Size = new System.Drawing.Size(190, 21);
            this.cbxTipoEquipo.TabIndex = 217;
            this.cbxTipoEquipo.SelectedIndexChanged += new System.EventHandler(this.cbxTipoEquipo_SelectedIndexChanged);
            this.cbxTipoEquipo.DropDownClosed += new System.EventHandler(this.cbxTipoEquipo_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(492, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 214;
            this.label3.Text = "Tipo:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPlaca);
            this.groupBox2.Location = new System.Drawing.Point(31, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 58);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Máquina:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(16, 24);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(144, 20);
            this.txtPlaca.TabIndex = 204;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Location = new System.Drawing.Point(231, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 58);
            this.groupBox1.TabIndex = 213;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Última Fecha de Mantenimiento:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(129, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(112, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "--";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(15, 24);
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
            this.btnExcel.Location = new System.Drawing.Point(807, 21);
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
            this.btnBuscar.Location = new System.Drawing.Point(746, 21);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbxTipoMaquina
            // 
            this.cbxTipoMaquina.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoMaquina.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoMaquina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoMaquina.FormattingEnabled = true;
            this.cbxTipoMaquina.Location = new System.Drawing.Point(495, 39);
            this.cbxTipoMaquina.Name = "cbxTipoMaquina";
            this.cbxTipoMaquina.Size = new System.Drawing.Size(190, 21);
            this.cbxTipoMaquina.TabIndex = 216;
            this.cbxTipoMaquina.SelectedIndexChanged += new System.EventHandler(this.cbxTipoMaquina_SelectedIndexChanged);
            this.cbxTipoMaquina.DropDownClosed += new System.EventHandler(this.cbxTipoMaquina_DropDownClosed);
            // 
            // cbxTipoUnidad
            // 
            this.cbxTipoUnidad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoUnidad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoUnidad.FormattingEnabled = true;
            this.cbxTipoUnidad.Location = new System.Drawing.Point(495, 39);
            this.cbxTipoUnidad.Name = "cbxTipoUnidad";
            this.cbxTipoUnidad.Size = new System.Drawing.Size(158, 21);
            this.cbxTipoUnidad.TabIndex = 215;
            this.cbxTipoUnidad.SelectedIndexChanged += new System.EventHandler(this.cbxTipoUnidad_SelectedIndexChanged);
            this.cbxTipoUnidad.DropDownClosed += new System.EventHandler(this.cbxTipoUnidad_DropDownClosed);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(890, 50);
            this.label1.TabIndex = 12;
            this.label1.Text = "HISTORIAL DE MANTENIMIENTOS REALIZADOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmHistorialMtto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 515);
            this.Controls.Add(this.dtgHistorialMtto);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Name = "frmHistorialMtto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HISTORIAL DE MANTENIMIENTO";
            this.Load += new System.EventHandler(this.frmHistorialMtto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialMtto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialMttoVista)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgHistorialMtto;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvHistorialMttoVista;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        public System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbxTipoMaquina;
        public System.Windows.Forms.ComboBox cbxTipoUnidad;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public System.Windows.Forms.ComboBox cbxTipoEquipo;
    }
}