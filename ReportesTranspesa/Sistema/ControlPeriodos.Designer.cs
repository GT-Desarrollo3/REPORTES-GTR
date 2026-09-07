namespace ReportesTranspesa.Sistema
{
    partial class ControlPeriodos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPeriodos));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.cboReporte = new MetroFramework.Controls.MetroComboBox();
            this.lblReporte = new MetroFramework.Controls.MetroLabel();
            this.txtFechaFin = new MetroFramework.Controls.MetroTextBox();
            this.lblFechaFin = new MetroFramework.Controls.MetroLabel();
            this.txtFechaIni = new MetroFramework.Controls.MetroTextBox();
            this.lblFechaInicio = new MetroFramework.Controls.MetroLabel();
            this.cboPeriodo = new MetroFramework.Controls.MetroComboBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.lblPeriodo = new MetroFramework.Controls.MetroLabel();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnAgregar);
            this.splitContainer1.Panel1.Controls.Add(this.cboReporte);
            this.splitContainer1.Panel1.Controls.Add(this.lblReporte);
            this.splitContainer1.Panel1.Controls.Add(this.txtFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.lblFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.txtFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.lblFechaInicio);
            this.splitContainer1.Panel1.Controls.Add(this.cboPeriodo);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.lblPeriodo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(725, 351);
            this.splitContainer1.SplitterDistance = 94;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(287, 19);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(70, 23);
            this.btnAgregar.TabIndex = 121;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // cboReporte
            // 
            this.cboReporte.FormattingEnabled = true;
            this.cboReporte.ItemHeight = 23;
            this.cboReporte.Items.AddRange(new object[] {
            "Operaciones_Reporte_GuiasxEstado",
            "Operaciones_Reporte_Bonificacion"});
            this.cboReporte.Location = new System.Drawing.Point(460, 56);
            this.cboReporte.Name = "cboReporte";
            this.cboReporte.Size = new System.Drawing.Size(253, 29);
            this.cboReporte.Style = MetroFramework.MetroColorStyle.Red;
            this.cboReporte.TabIndex = 120;
            this.cboReporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboReporte.UseSelectable = true;
            // 
            // lblReporte
            // 
            this.lblReporte.AutoSize = true;
            this.lblReporte.Location = new System.Drawing.Point(391, 60);
            this.lblReporte.Name = "lblReporte";
            this.lblReporte.Size = new System.Drawing.Size(63, 19);
            this.lblReporte.Style = MetroFramework.MetroColorStyle.Red;
            this.lblReporte.TabIndex = 119;
            this.lblReporte.Text = "Reporte :";
            this.lblReporte.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtFechaFin.Lines = new string[0];
            this.txtFechaFin.Location = new System.Drawing.Point(287, 56);
            this.txtFechaFin.MaxLength = 32767;
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.PasswordChar = '\0';
            this.txtFechaFin.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFechaFin.SelectedText = "";
            this.txtFechaFin.Size = new System.Drawing.Size(86, 29);
            this.txtFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.txtFechaFin.TabIndex = 117;
            this.txtFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtFechaFin.UseSelectable = true;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Location = new System.Drawing.Point(208, 60);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(71, 19);
            this.lblFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFechaFin.TabIndex = 116;
            this.lblFechaFin.Text = "Fecha Fin :";
            this.lblFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtFechaIni
            // 
            this.txtFechaIni.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtFechaIni.Lines = new string[0];
            this.txtFechaIni.Location = new System.Drawing.Point(108, 56);
            this.txtFechaIni.MaxLength = 32767;
            this.txtFechaIni.Name = "txtFechaIni";
            this.txtFechaIni.PasswordChar = '\0';
            this.txtFechaIni.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFechaIni.SelectedText = "";
            this.txtFechaIni.Size = new System.Drawing.Size(88, 29);
            this.txtFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.txtFechaIni.TabIndex = 115;
            this.txtFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtFechaIni.UseSelectable = true;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Location = new System.Drawing.Point(18, 60);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(84, 19);
            this.lblFechaInicio.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFechaInicio.TabIndex = 114;
            this.lblFechaInicio.Text = "Fecha Inicio :";
            this.lblFechaInicio.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cboPeriodo
            // 
            this.cboPeriodo.FormattingEnabled = true;
            this.cboPeriodo.ItemHeight = 23;
            this.cboPeriodo.Items.AddRange(new object[] {
            "TODOS"});
            this.cboPeriodo.Location = new System.Drawing.Point(81, 13);
            this.cboPeriodo.Name = "cboPeriodo";
            this.cboPeriodo.Size = new System.Drawing.Size(115, 29);
            this.cboPeriodo.Style = MetroFramework.MetroColorStyle.Red;
            this.cboPeriodo.TabIndex = 66;
            this.cboPeriodo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboPeriodo.UseSelectable = true;
            this.cboPeriodo.SelectedIndexChanged += new System.EventHandler(this.cboPeriodo_SelectedIndexChanged);
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
            this.btnBuscar.Location = new System.Drawing.Point(218, 11);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(58, 36);
            this.btnBuscar.TabIndex = 65;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPeriodo.Location = new System.Drawing.Point(17, 17);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(59, 19);
            this.lblPeriodo.Style = MetroFramework.MetroColorStyle.Red;
            this.lblPeriodo.TabIndex = 64;
            this.lblPeriodo.Text = "Periodo:";
            this.lblPeriodo.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(725, 253);
            this.dtgvData.TabIndex = 116;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowAutoFilterRow = true;
            // 
            // ControlPeriodos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 431);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ControlPeriodos";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Control de Periodos";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.ControlPeriodos_Load);
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
        private MetroFramework.Controls.MetroComboBox cboPeriodo;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroLabel lblPeriodo;
        private MetroFramework.Controls.MetroTextBox txtFechaFin;
        private MetroFramework.Controls.MetroLabel lblFechaFin;
        private MetroFramework.Controls.MetroTextBox txtFechaIni;
        private MetroFramework.Controls.MetroLabel lblFechaInicio;
        private MetroFramework.Controls.MetroLabel lblReporte;
        private MetroFramework.Controls.MetroComboBox cboReporte;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
    }
}