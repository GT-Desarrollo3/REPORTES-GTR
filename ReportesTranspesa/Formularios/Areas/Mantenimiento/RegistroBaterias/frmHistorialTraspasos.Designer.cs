namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroBaterias
{
    partial class frmHistorialTraspasos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistorialTraspasos));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgTraspasos = new DevExpress.XtraGrid.GridControl();
            this.dgvTraspasosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTraspasos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraspasosVista)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(978, 43);
            this.label1.TabIndex = 20;
            this.label1.Text = "HISTORIAL DE TRASPASOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox15);
            this.panel3.Controls.Add(this.groupBox14);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(978, 96);
            this.panel3.TabIndex = 182;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Location = new System.Drawing.Point(209, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 58);
            this.groupBox1.TabIndex = 200;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por Placa:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(9, 24);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(144, 20);
            this.txtPlaca.TabIndex = 0;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.dtpFechaFin);
            this.groupBox15.Controls.Add(this.label40);
            this.groupBox15.Controls.Add(this.dtpFechaIni);
            this.groupBox15.Location = new System.Drawing.Point(397, 19);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(243, 58);
            this.groupBox15.TabIndex = 199;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Buscar por Fecha de Traspaso:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(133, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FechaProgFin_KeyPress);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(116, 28);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(11, 13);
            this.label40.TabIndex = 4;
            this.label40.Text = "-";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(15, 24);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaIni.TabIndex = 2;
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FechaProgIni_KeyPress);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtCodigo);
            this.groupBox14.Location = new System.Drawing.Point(21, 19);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(164, 58);
            this.groupBox14.TabIndex = 198;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar por Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(9, 24);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(144, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(739, 26);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 197;
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
            this.btnBuscar.Location = new System.Drawing.Point(678, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgTraspasos
            // 
            this.dtgTraspasos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTraspasos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTraspasos.Location = new System.Drawing.Point(0, 139);
            this.dtgTraspasos.MainView = this.dgvTraspasosVista;
            this.dtgTraspasos.Name = "dtgTraspasos";
            this.dtgTraspasos.Size = new System.Drawing.Size(978, 407);
            this.dtgTraspasos.TabIndex = 183;
            this.dtgTraspasos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTraspasosVista});
            // 
            // dgvTraspasosVista
            // 
            this.dgvTraspasosVista.GridControl = this.dtgTraspasos;
            this.dgvTraspasosVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvTraspasosVista.Name = "dgvTraspasosVista";
            this.dgvTraspasosVista.OptionsBehavior.Editable = false;
            this.dgvTraspasosVista.OptionsBehavior.ReadOnly = true;
            this.dgvTraspasosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvTraspasosVista.OptionsView.RowAutoHeight = true;
            this.dgvTraspasosVista.OptionsView.ShowFooter = true;
            // 
            // frmHistorialTraspasos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 546);
            this.Controls.Add(this.dtgTraspasos);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Name = "frmHistorialTraspasos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HISTORIAL DE TRASPASOS";
            this.Load += new System.EventHandler(this.frmHistorialTraspasos_Load);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTraspasos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraspasosVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtCodigo;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgTraspasos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTraspasosVista;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlaca;
        public System.Windows.Forms.GroupBox groupBox15;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        public System.Windows.Forms.DateTimePicker dtpFechaIni;
        public System.Windows.Forms.Label label1;
    }
}