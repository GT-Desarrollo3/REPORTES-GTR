namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.AsignacionUniformes
{
    partial class frmHistorialDevueltos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistorialDevueltos));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbxUniforme = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.FechaFin = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.FechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.dtgHistorialDevueltos = new DevExpress.XtraGrid.GridControl();
            this.dgvHistorialDevueltosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialDevueltos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialDevueltosVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1264, 50);
            this.label1.TabIndex = 4;
            this.label1.Text = "HISTORIAL DE UNIFORMES DEVUELTOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.cbxUniforme);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.label58);
            this.panel4.Controls.Add(this.groupBox14);
            this.panel4.Controls.Add(this.groupBox15);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1264, 89);
            this.panel4.TabIndex = 5;
            // 
            // cbxUniforme
            // 
            this.cbxUniforme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUniforme.FormattingEnabled = true;
            this.cbxUniforme.Location = new System.Drawing.Point(801, 42);
            this.cbxUniforme.Name = "cbxUniforme";
            this.cbxUniforme.Size = new System.Drawing.Size(249, 21);
            this.cbxUniforme.TabIndex = 119;
            this.cbxUniforme.SelectedIndexChanged += new System.EventHandler(this.cbxUniforme_SelectedIndexChanged);
            this.cbxUniforme.DropDownClosed += new System.EventHandler(this.cbxUniforme_DropDownClosed);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_zoom_115406;
            this.btnBuscar.Location = new System.Drawing.Point(1118, 20);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(46, 48);
            this.btnBuscar.TabIndex = 118;
            this.btnBuscar.Text = "&B";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(798, 20);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(106, 13);
            this.label58.TabIndex = 117;
            this.label58.Text = "Buscar por Uniforme:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtPersonal);
            this.groupBox14.Location = new System.Drawing.Point(22, 15);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(375, 58);
            this.groupBox14.TabIndex = 116;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar Personal:";
            // 
            // txtPersonal
            // 
            this.txtPersonal.Location = new System.Drawing.Point(9, 24);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(356, 20);
            this.txtPersonal.TabIndex = 0;
            this.txtPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonal_KeyPress);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.FechaFin);
            this.groupBox15.Controls.Add(this.label40);
            this.groupBox15.Controls.Add(this.FechaInicio);
            this.groupBox15.Controls.Add(this.label41);
            this.groupBox15.Location = new System.Drawing.Point(422, 15);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(353, 58);
            this.groupBox15.TabIndex = 115;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Buscar por Fecha de Devolución:";
            // 
            // FechaFin
            // 
            this.FechaFin.CustomFormat = "dd-MM-yyyy";
            this.FechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaFin.Location = new System.Drawing.Point(219, 24);
            this.FechaFin.Name = "FechaFin";
            this.FechaFin.Size = new System.Drawing.Size(116, 20);
            this.FechaFin.TabIndex = 5;
            this.FechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FechaFin_KeyPress);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(189, 27);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(24, 13);
            this.label40.TabIndex = 4;
            this.label40.Text = "Fin:";
            // 
            // FechaInicio
            // 
            this.FechaInicio.CustomFormat = "dd-MM-yyyy";
            this.FechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaInicio.Location = new System.Drawing.Point(55, 24);
            this.FechaInicio.Name = "FechaInicio";
            this.FechaInicio.Size = new System.Drawing.Size(116, 20);
            this.FechaInicio.TabIndex = 2;
            this.FechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FechaInicio_KeyPress);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(14, 28);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 13);
            this.label41.TabIndex = 0;
            this.label41.Text = "Inicio:";
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
            this.btnExcel.Location = new System.Drawing.Point(1180, 20);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(48, 48);
            this.btnExcel.TabIndex = 101;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dtgHistorialDevueltos
            // 
            this.dtgHistorialDevueltos.AllowDrop = true;
            this.dtgHistorialDevueltos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgHistorialDevueltos.Location = new System.Drawing.Point(0, 139);
            this.dtgHistorialDevueltos.MainView = this.dgvHistorialDevueltosVista;
            this.dtgHistorialDevueltos.Name = "dtgHistorialDevueltos";
            this.dtgHistorialDevueltos.Size = new System.Drawing.Size(1264, 511);
            this.dtgHistorialDevueltos.TabIndex = 11;
            this.dtgHistorialDevueltos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvHistorialDevueltosVista,
            this.gridView1});
            // 
            // dgvHistorialDevueltosVista
            // 
            this.dgvHistorialDevueltosVista.GridControl = this.dtgHistorialDevueltos;
            this.dgvHistorialDevueltosVista.Name = "dgvHistorialDevueltosVista";
            this.dgvHistorialDevueltosVista.OptionsBehavior.Editable = false;
            this.dgvHistorialDevueltosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvHistorialDevueltosVista.OptionsView.RowAutoHeight = true;
            this.dgvHistorialDevueltosVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvHistorialDevueltosVista_CustomDrawCell);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgHistorialDevueltos;
            this.gridView1.Name = "gridView1";
            // 
            // frmHistorialDevueltos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 650);
            this.Controls.Add(this.dtgHistorialDevueltos);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Name = "frmHistorialDevueltos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistorialDevueltos";
            this.Load += new System.EventHandler(this.frmHistorialDevueltos_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorialDevueltos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialDevueltosVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.DateTimePicker FechaFin;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker FechaInicio;
        private System.Windows.Forms.Label label41;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.ComboBox cbxUniforme;
        private DevExpress.XtraGrid.GridControl dtgHistorialDevueltos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvHistorialDevueltosVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}