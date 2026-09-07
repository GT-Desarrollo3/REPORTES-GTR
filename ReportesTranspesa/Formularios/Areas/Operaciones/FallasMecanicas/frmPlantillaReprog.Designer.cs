namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    partial class frmPlantillaReprog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlantillaReprog));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBuscarPlaca = new System.Windows.Forms.TextBox();
            this.dtgPlantillaReprog = new DevExpress.XtraGrid.GridControl();
            this.dgvPlantillaReprogView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPlantillaReprog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlantillaReprogView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkRed;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(759, 43);
            this.label1.TabIndex = 19;
            this.label1.Text = "PLANTILLA DE REPROGRAMACIÓN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.cbxOperacion);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtBuscarPlaca);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(759, 104);
            this.panel3.TabIndex = 20;
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
            this.btnExcel.Location = new System.Drawing.Point(598, 29);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 45);
            this.btnExcel.TabIndex = 194;
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
            this.btnBuscar.Location = new System.Drawing.Point(535, 29);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(51, 45);
            this.btnBuscar.TabIndex = 193;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.dtpFechaIni);
            this.groupBox4.Controls.Add(this.dtpFechaFin);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.groupBox4.Location = new System.Drawing.Point(249, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 66);
            this.groupBox4.TabIndex = 189;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fecha de Cambio: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label13.Location = new System.Drawing.Point(117, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 15);
            this.label13.TabIndex = 184;
            this.label13.Text = "--";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(15, 28);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 182;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(134, 28);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 183;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label6.Location = new System.Drawing.Point(20, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 188;
            this.label6.Text = "Operación:";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(93, 58);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(127, 21);
            this.cbxOperacion.TabIndex = 187;
            this.cbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            this.cbxOperacion.DropDownClosed += new System.EventHandler(this.cbxOperacion_DropDownClosed);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label10.Location = new System.Drawing.Point(37, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 15);
            this.label10.TabIndex = 118;
            this.label10.Text = "Unidad:";
            // 
            // txtBuscarPlaca
            // 
            this.txtBuscarPlaca.BackColor = System.Drawing.SystemColors.Window;
            this.txtBuscarPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.txtBuscarPlaca.Location = new System.Drawing.Point(93, 24);
            this.txtBuscarPlaca.Name = "txtBuscarPlaca";
            this.txtBuscarPlaca.Size = new System.Drawing.Size(127, 21);
            this.txtBuscarPlaca.TabIndex = 117;
            this.txtBuscarPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarPlaca_KeyPress);
            // 
            // dtgPlantillaReprog
            // 
            this.dtgPlantillaReprog.AllowDrop = true;
            this.dtgPlantillaReprog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgPlantillaReprog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgPlantillaReprog.Location = new System.Drawing.Point(0, 147);
            this.dtgPlantillaReprog.LookAndFeel.SkinMaskColor = System.Drawing.Color.Yellow;
            this.dtgPlantillaReprog.LookAndFeel.SkinName = "Money Twins";
            this.dtgPlantillaReprog.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgPlantillaReprog.MainView = this.dgvPlantillaReprogView;
            this.dtgPlantillaReprog.Name = "dtgPlantillaReprog";
            this.dtgPlantillaReprog.Size = new System.Drawing.Size(759, 354);
            this.dtgPlantillaReprog.TabIndex = 101;
            this.dtgPlantillaReprog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvPlantillaReprogView});
            // 
            // dgvPlantillaReprogView
            // 
            this.dgvPlantillaReprogView.GridControl = this.dtgPlantillaReprog;
            this.dgvPlantillaReprogView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvPlantillaReprogView.Name = "dgvPlantillaReprogView";
            this.dgvPlantillaReprogView.OptionsBehavior.Editable = false;
            this.dgvPlantillaReprogView.OptionsView.ColumnAutoWidth = false;
            this.dgvPlantillaReprogView.OptionsView.RowAutoHeight = true;
            this.dgvPlantillaReprogView.OptionsView.ShowFooter = true;
            // 
            // frmPlantillaReprog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 501);
            this.Controls.Add(this.dtgPlantillaReprog);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Name = "frmPlantillaReprog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLANTILLA DE REPROGRAMACIÓN";
            this.Load += new System.EventHandler(this.frmPlantillaReprog_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPlantillaReprog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlantillaReprogView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxOperacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBuscarPlaca;
        private DevExpress.XtraGrid.GridControl dtgPlantillaReprog;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvPlantillaReprogView;
        public System.Windows.Forms.Label label1;
    }
}