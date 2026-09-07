namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmNuevoEPPS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevoEPPS));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnExcelS = new DevExpress.XtraEditors.SimpleButton();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.dtgData = new DevExpress.XtraGrid.GridControl();
            this.dgvExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxTipoEPPS = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.FechaFin = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.FechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.cbxTipoEPPS);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.label58);
            this.panel4.Controls.Add(this.groupBox14);
            this.panel4.Controls.Add(this.groupBox15);
            this.panel4.Controls.Add(this.btnExcelS);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 56);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1258, 89);
            this.panel4.TabIndex = 2;
            // 
            // btnExcelS
            // 
            this.btnExcelS.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcelS.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcelS.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcelS.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelS.Appearance.Options.UseBackColor = true;
            this.btnExcelS.Appearance.Options.UseBorderColor = true;
            this.btnExcelS.Appearance.Options.UseFont = true;
            this.btnExcelS.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcelS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelS.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelS.Image")));
            this.btnExcelS.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcelS.Location = new System.Drawing.Point(1180, 20);
            this.btnExcelS.Name = "btnExcelS";
            this.btnExcelS.Size = new System.Drawing.Size(48, 48);
            this.btnExcelS.TabIndex = 101;
            this.btnExcelS.Click += new System.EventHandler(this.btnExcelS_Click);
            // 
            // txtPersonal
            // 
            this.txtPersonal.Location = new System.Drawing.Point(9, 24);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(356, 20);
            this.txtPersonal.TabIndex = 0;
            this.txtPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonal_KeyPress);
            // 
            // dtgData
            // 
            this.dtgData.AllowDrop = true;
            this.dtgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgData.Location = new System.Drawing.Point(0, 145);
            this.dtgData.MainView = this.dgvExpressVista;
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(1258, 395);
            this.dtgData.TabIndex = 10;
            this.dtgData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvExpressVista,
            this.gridView1});
            // 
            // dgvExpressVista
            // 
            this.dgvExpressVista.GridControl = this.dtgData;
            this.dgvExpressVista.Name = "dgvExpressVista";
            this.dgvExpressVista.OptionsBehavior.Editable = false;
            this.dgvExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvExpressVista.OptionsView.RowAutoHeight = true;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgData;
            this.gridView1.Name = "gridView1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1258, 56);
            this.label1.TabIndex = 3;
            this.label1.Text = "HISTORIAL DE EPPS DEVUELTOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxTipoEPPS
            // 
            this.cbxTipoEPPS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoEPPS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoEPPS.FormattingEnabled = true;
            this.cbxTipoEPPS.Location = new System.Drawing.Point(801, 39);
            this.cbxTipoEPPS.Name = "cbxTipoEPPS";
            this.cbxTipoEPPS.Size = new System.Drawing.Size(231, 21);
            this.cbxTipoEPPS.TabIndex = 119;
            this.cbxTipoEPPS.SelectedIndexChanged += new System.EventHandler(this.cbxTipoEPPS_SelectedIndexChanged);
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
            this.label58.Size = new System.Drawing.Size(85, 13);
            this.label58.TabIndex = 117;
            this.label58.Text = "Buscar por EPP:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtPersonal);
            this.groupBox14.Location = new System.Drawing.Point(22, 15);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(375, 58);
            this.groupBox14.TabIndex = 116;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar por Empleado:";
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
            this.groupBox15.Text = "Buscar por Fecha de Desvinculación:";
            // 
            // FechaFin
            // 
            this.FechaFin.CustomFormat = "dd-MM-yyyy";
            this.FechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaFin.Location = new System.Drawing.Point(219, 24);
            this.FechaFin.Name = "FechaFin";
            this.FechaFin.Size = new System.Drawing.Size(116, 20);
            this.FechaFin.TabIndex = 5;
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
            // frmNuevoEPPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 540);
            this.Controls.Add(this.dtgData);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Name = "frmNuevoEPPS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de EPP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmNuevoEPPS_Load);
            this.Shown += new System.EventHandler(this.frmNuevoEPPS_Shown);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraGrid.GridControl dtgData;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvExpressVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnExcelS;
        private System.Windows.Forms.ComboBox cbxTipoEPPS;
        public System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.DateTimePicker FechaFin;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker FechaInicio;
        private System.Windows.Forms.Label label41;
    }
}