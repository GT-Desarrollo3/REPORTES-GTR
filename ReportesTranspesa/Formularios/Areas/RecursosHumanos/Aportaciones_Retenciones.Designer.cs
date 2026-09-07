namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class Aportaciones_Retenciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aportaciones_Retenciones));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cboProceso = new MetroFramework.Controls.MetroComboBox();
            this.cboPlanilla = new MetroFramework.Controls.MetroComboBox();
            this.chkProceso = new MetroFramework.Controls.MetroCheckBox();
            this.chkPlanilla = new MetroFramework.Controls.MetroCheckBox();
            this.chkPeriodo = new MetroFramework.Controls.MetroCheckBox();
            this.chkCompania = new MetroFramework.Controls.MetroCheckBox();
            this.cboCompañia = new MetroFramework.Controls.MetroComboBox();
            this.txtPeriodo = new MetroFramework.Controls.MetroTextBox();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvData = new DevExpress.XtraPivotGrid.PivotGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.cboProceso);
            this.splitContainer1.Panel1.Controls.Add(this.cboPlanilla);
            this.splitContainer1.Panel1.Controls.Add(this.chkProceso);
            this.splitContainer1.Panel1.Controls.Add(this.chkPlanilla);
            this.splitContainer1.Panel1.Controls.Add(this.chkPeriodo);
            this.splitContainer1.Panel1.Controls.Add(this.chkCompania);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompañia);
            this.splitContainer1.Panel1.Controls.Add(this.txtPeriodo);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(958, 552);
            this.splitContainer1.SplitterDistance = 70;
            this.splitContainer1.TabIndex = 2;
            // 
            // cboProceso
            // 
            this.cboProceso.Enabled = false;
            this.cboProceso.FormattingEnabled = true;
            this.cboProceso.ItemHeight = 23;
            this.cboProceso.Items.AddRange(new object[] {
            "FIN DE MES",
            "LIQUIDACION",
            "GRATIFICACION",
            "UTILIDADES"});
            this.cboProceso.Location = new System.Drawing.Point(318, 38);
            this.cboProceso.Name = "cboProceso";
            this.cboProceso.Size = new System.Drawing.Size(142, 29);
            this.cboProceso.Style = MetroFramework.MetroColorStyle.Red;
            this.cboProceso.TabIndex = 90;
            this.cboProceso.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboProceso.UseSelectable = true;
            // 
            // cboPlanilla
            // 
            this.cboPlanilla.Enabled = false;
            this.cboPlanilla.FormattingEnabled = true;
            this.cboPlanilla.ItemHeight = 23;
            this.cboPlanilla.Items.AddRange(new object[] {
            "EMPLEADO",
            "OBRERO",
            "PRACTICANTE"});
            this.cboPlanilla.Location = new System.Drawing.Point(318, 3);
            this.cboPlanilla.Name = "cboPlanilla";
            this.cboPlanilla.Size = new System.Drawing.Size(142, 29);
            this.cboPlanilla.Style = MetroFramework.MetroColorStyle.Red;
            this.cboPlanilla.TabIndex = 89;
            this.cboPlanilla.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboPlanilla.UseSelectable = true;
            // 
            // chkProceso
            // 
            this.chkProceso.AutoSize = true;
            this.chkProceso.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkProceso.Location = new System.Drawing.Point(242, 42);
            this.chkProceso.Name = "chkProceso";
            this.chkProceso.Size = new System.Drawing.Size(76, 19);
            this.chkProceso.Style = MetroFramework.MetroColorStyle.Red;
            this.chkProceso.TabIndex = 88;
            this.chkProceso.Text = "Proceso:";
            this.chkProceso.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkProceso.UseSelectable = true;
            this.chkProceso.CheckedChanged += new System.EventHandler(this.chkProceso_CheckedChanged);
            // 
            // chkPlanilla
            // 
            this.chkPlanilla.AutoSize = true;
            this.chkPlanilla.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkPlanilla.Location = new System.Drawing.Point(242, 7);
            this.chkPlanilla.Name = "chkPlanilla";
            this.chkPlanilla.Size = new System.Drawing.Size(70, 19);
            this.chkPlanilla.Style = MetroFramework.MetroColorStyle.Red;
            this.chkPlanilla.TabIndex = 87;
            this.chkPlanilla.Text = "Planilla:";
            this.chkPlanilla.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkPlanilla.UseSelectable = true;
            this.chkPlanilla.CheckedChanged += new System.EventHandler(this.chkPlanilla_CheckedChanged);
            // 
            // chkPeriodo
            // 
            this.chkPeriodo.AutoSize = true;
            this.chkPeriodo.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkPeriodo.Location = new System.Drawing.Point(3, 42);
            this.chkPeriodo.Name = "chkPeriodo";
            this.chkPeriodo.Size = new System.Drawing.Size(75, 19);
            this.chkPeriodo.Style = MetroFramework.MetroColorStyle.Red;
            this.chkPeriodo.TabIndex = 86;
            this.chkPeriodo.Text = "Período:";
            this.chkPeriodo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkPeriodo.UseSelectable = true;
            this.chkPeriodo.CheckedChanged += new System.EventHandler(this.chkPeriodo_CheckedChanged);
            // 
            // chkCompania
            // 
            this.chkCompania.AutoSize = true;
            this.chkCompania.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkCompania.Location = new System.Drawing.Point(3, 7);
            this.chkCompania.Name = "chkCompania";
            this.chkCompania.Size = new System.Drawing.Size(90, 19);
            this.chkCompania.Style = MetroFramework.MetroColorStyle.Red;
            this.chkCompania.TabIndex = 85;
            this.chkCompania.Text = "Compañía:";
            this.chkCompania.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkCompania.UseSelectable = true;
            this.chkCompania.CheckedChanged += new System.EventHandler(this.chkCompania_CheckedChanged);
            // 
            // cboCompañia
            // 
            this.cboCompañia.Enabled = false;
            this.cboCompañia.FormattingEnabled = true;
            this.cboCompañia.ItemHeight = 23;
            this.cboCompañia.Items.AddRange(new object[] {
            "TRANSPESA",
            "BRA"});
            this.cboCompañia.Location = new System.Drawing.Point(99, 3);
            this.cboCompañia.Name = "cboCompañia";
            this.cboCompañia.Size = new System.Drawing.Size(117, 29);
            this.cboCompañia.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCompañia.TabIndex = 84;
            this.cboCompañia.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboCompañia.UseSelectable = true;
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.Lines = new string[0];
            this.txtPeriodo.Location = new System.Drawing.Point(99, 38);
            this.txtPeriodo.MaxLength = 32767;
            this.txtPeriodo.Name = "txtPeriodo";
            this.txtPeriodo.PasswordChar = '\0';
            this.txtPeriodo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPeriodo.SelectedText = "";
            this.txtPeriodo.Size = new System.Drawing.Size(117, 29);
            this.txtPeriodo.Style = MetroFramework.MetroColorStyle.Red;
            this.txtPeriodo.TabIndex = 83;
            this.txtPeriodo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtPeriodo.UseSelectable = true;
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
            this.btnImprimir.Location = new System.Drawing.Point(696, 30);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 52;
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
            this.btnExcel.Location = new System.Drawing.Point(618, 30);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 51;
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
            this.btnBuscar.Location = new System.Drawing.Point(541, 30);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 50;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.OptionsPrint.PageSettings.Landscape = true;
            this.dtgvData.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.dtgvData.Size = new System.Drawing.Size(958, 478);
            this.dtgvData.TabIndex = 1;
            // 
            // Aportaciones_Retenciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 632);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Aportaciones_Retenciones";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Aportaciones y Retenciones";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Aportaciones_Retenciones_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroTextBox txtPeriodo;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraPivotGrid.PivotGridControl dtgvData;
        private MetroFramework.Controls.MetroComboBox cboCompañia;
        private MetroFramework.Controls.MetroCheckBox chkPeriodo;
        private MetroFramework.Controls.MetroCheckBox chkCompania;
        private MetroFramework.Controls.MetroCheckBox chkPlanilla;
        private MetroFramework.Controls.MetroComboBox cboProceso;
        private MetroFramework.Controls.MetroComboBox cboPlanilla;
        private MetroFramework.Controls.MetroCheckBox chkProceso;
    }
}