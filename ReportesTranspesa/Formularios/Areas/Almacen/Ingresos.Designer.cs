namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    partial class Ingresos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ingresos));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblViajes = new MetroFramework.Controls.MetroLabel();
            this.lblSumDiferencia = new MetroFramework.Controls.MetroLabel();
            this.lblSumIngreso = new MetroFramework.Controls.MetroLabel();
            this.lblSumSalida = new MetroFramework.Controls.MetroLabel();
            this.lbl4 = new MetroFramework.Controls.MetroLabel();
            this.lbl3 = new MetroFramework.Controls.MetroLabel();
            this.lbl2 = new MetroFramework.Controls.MetroLabel();
            this.lbl1 = new MetroFramework.Controls.MetroLabel();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.txtLote = new MetroFramework.Controls.MetroTextBox();
            this.txtCliente = new MetroFramework.Controls.MetroTextBox();
            this.txtProducto = new MetroFramework.Controls.MetroTextBox();
            this.chkProducto = new MetroFramework.Controls.MetroCheckBox();
            this.chkLote = new MetroFramework.Controls.MetroCheckBox();
            this.chkCliente = new MetroFramework.Controls.MetroCheckBox();
            this.chkFechas = new MetroFramework.Controls.MetroCheckBox();
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
            this.splitContainer1.Panel1.Controls.Add(this.lblViajes);
            this.splitContainer1.Panel1.Controls.Add(this.lblSumDiferencia);
            this.splitContainer1.Panel1.Controls.Add(this.lblSumIngreso);
            this.splitContainer1.Panel1.Controls.Add(this.lblSumSalida);
            this.splitContainer1.Panel1.Controls.Add(this.lbl4);
            this.splitContainer1.Panel1.Controls.Add(this.lbl3);
            this.splitContainer1.Panel1.Controls.Add(this.lbl2);
            this.splitContainer1.Panel1.Controls.Add(this.lbl1);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel3);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaFin);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.txtLote);
            this.splitContainer1.Panel1.Controls.Add(this.txtCliente);
            this.splitContainer1.Panel1.Controls.Add(this.txtProducto);
            this.splitContainer1.Panel1.Controls.Add(this.chkProducto);
            this.splitContainer1.Panel1.Controls.Add(this.chkLote);
            this.splitContainer1.Panel1.Controls.Add(this.chkCliente);
            this.splitContainer1.Panel1.Controls.Add(this.chkFechas);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(952, 590);
            this.splitContainer1.SplitterDistance = 103;
            this.splitContainer1.TabIndex = 1;
            // 
            // lblViajes
            // 
            this.lblViajes.AutoSize = true;
            this.lblViajes.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblViajes.Location = new System.Drawing.Point(610, 73);
            this.lblViajes.Name = "lblViajes";
            this.lblViajes.Size = new System.Drawing.Size(17, 19);
            this.lblViajes.Style = MetroFramework.MetroColorStyle.Red;
            this.lblViajes.TabIndex = 61;
            this.lblViajes.Text = "0";
            this.lblViajes.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblSumDiferencia
            // 
            this.lblSumDiferencia.AutoSize = true;
            this.lblSumDiferencia.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblSumDiferencia.Location = new System.Drawing.Point(610, 41);
            this.lblSumDiferencia.Name = "lblSumDiferencia";
            this.lblSumDiferencia.Size = new System.Drawing.Size(17, 19);
            this.lblSumDiferencia.Style = MetroFramework.MetroColorStyle.Red;
            this.lblSumDiferencia.TabIndex = 60;
            this.lblSumDiferencia.Text = "0";
            this.lblSumDiferencia.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblSumIngreso
            // 
            this.lblSumIngreso.AutoSize = true;
            this.lblSumIngreso.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblSumIngreso.Location = new System.Drawing.Point(412, 73);
            this.lblSumIngreso.Name = "lblSumIngreso";
            this.lblSumIngreso.Size = new System.Drawing.Size(17, 19);
            this.lblSumIngreso.Style = MetroFramework.MetroColorStyle.Red;
            this.lblSumIngreso.TabIndex = 59;
            this.lblSumIngreso.Text = "0";
            this.lblSumIngreso.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblSumSalida
            // 
            this.lblSumSalida.AutoSize = true;
            this.lblSumSalida.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblSumSalida.Location = new System.Drawing.Point(412, 41);
            this.lblSumSalida.Name = "lblSumSalida";
            this.lblSumSalida.Size = new System.Drawing.Size(17, 19);
            this.lblSumSalida.Style = MetroFramework.MetroColorStyle.Red;
            this.lblSumSalida.TabIndex = 58;
            this.lblSumSalida.Text = "0";
            this.lblSumSalida.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl4.Location = new System.Drawing.Point(489, 73);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(77, 19);
            this.lbl4.Style = MetroFramework.MetroColorStyle.Red;
            this.lbl4.TabIndex = 57;
            this.lbl4.Text = "N° VIAJES :";
            this.lbl4.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl3.Location = new System.Drawing.Point(489, 41);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(102, 19);
            this.lbl3.Style = MetroFramework.MetroColorStyle.Red;
            this.lbl3.TabIndex = 56;
            this.lbl3.Text = "Σ DIFERENCIA :";
            this.lbl3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl2.Location = new System.Drawing.Point(307, 73);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(84, 19);
            this.lbl2.Style = MetroFramework.MetroColorStyle.Red;
            this.lbl2.TabIndex = 55;
            this.lbl2.Text = "Σ INGRESO :";
            this.lbl2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lbl1.Location = new System.Drawing.Point(307, 41);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(73, 19);
            this.lbl1.Style = MetroFramework.MetroColorStyle.Red;
            this.lbl1.TabIndex = 16;
            this.lbl1.Text = "Σ SALIDA :";
            this.lbl1.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            this.btnImprimir.Location = new System.Drawing.Point(892, 55);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 54;
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
            this.btnExcel.Location = new System.Drawing.Point(800, 55);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 53;
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
            this.btnBuscar.Location = new System.Drawing.Point(714, 55);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 37);
            this.btnBuscar.TabIndex = 52;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(521, 8);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(64, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 48;
            this.metroLabel3.Text = "Fecha Fin";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Enabled = false;
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(591, 3);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 47;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Enabled = false;
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(412, 3);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 46;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtLote
            // 
            this.txtLote.Enabled = false;
            this.txtLote.Lines = new string[0];
            this.txtLote.Location = new System.Drawing.Point(90, 73);
            this.txtLote.MaxLength = 32767;
            this.txtLote.Name = "txtLote";
            this.txtLote.PasswordChar = '\0';
            this.txtLote.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLote.SelectedText = "";
            this.txtLote.Size = new System.Drawing.Size(173, 29);
            this.txtLote.Style = MetroFramework.MetroColorStyle.Red;
            this.txtLote.TabIndex = 45;
            this.txtLote.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtLote.UseSelectable = true;
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Lines = new string[0];
            this.txtCliente.Location = new System.Drawing.Point(90, 3);
            this.txtCliente.MaxLength = 32767;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PasswordChar = '\0';
            this.txtCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCliente.SelectedText = "";
            this.txtCliente.Size = new System.Drawing.Size(173, 29);
            this.txtCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCliente.TabIndex = 44;
            this.txtCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCliente.UseSelectable = true;
            // 
            // txtProducto
            // 
            this.txtProducto.Enabled = false;
            this.txtProducto.Lines = new string[0];
            this.txtProducto.Location = new System.Drawing.Point(90, 38);
            this.txtProducto.MaxLength = 32767;
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.PasswordChar = '\0';
            this.txtProducto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProducto.SelectedText = "";
            this.txtProducto.Size = new System.Drawing.Size(173, 29);
            this.txtProducto.Style = MetroFramework.MetroColorStyle.Red;
            this.txtProducto.TabIndex = 43;
            this.txtProducto.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtProducto.UseSelectable = true;
            // 
            // chkProducto
            // 
            this.chkProducto.AutoSize = true;
            this.chkProducto.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkProducto.Location = new System.Drawing.Point(3, 41);
            this.chkProducto.Name = "chkProducto";
            this.chkProducto.Size = new System.Drawing.Size(81, 19);
            this.chkProducto.Style = MetroFramework.MetroColorStyle.Red;
            this.chkProducto.TabIndex = 42;
            this.chkProducto.Text = "Producto";
            this.chkProducto.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkProducto.UseSelectable = true;
            this.chkProducto.CheckedChanged += new System.EventHandler(this.chkProducto_CheckedChanged);
            // 
            // chkLote
            // 
            this.chkLote.AutoSize = true;
            this.chkLote.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkLote.Location = new System.Drawing.Point(3, 76);
            this.chkLote.Name = "chkLote";
            this.chkLote.Size = new System.Drawing.Size(52, 19);
            this.chkLote.Style = MetroFramework.MetroColorStyle.Red;
            this.chkLote.TabIndex = 41;
            this.chkLote.Text = "Lote";
            this.chkLote.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkLote.UseSelectable = true;
            this.chkLote.CheckedChanged += new System.EventHandler(this.chkLote_CheckedChanged);
            // 
            // chkCliente
            // 
            this.chkCliente.AutoSize = true;
            this.chkCliente.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkCliente.Location = new System.Drawing.Point(3, 8);
            this.chkCliente.Name = "chkCliente";
            this.chkCliente.Size = new System.Drawing.Size(67, 19);
            this.chkCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.chkCliente.TabIndex = 40;
            this.chkCliente.Text = "Cliente";
            this.chkCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkCliente.UseSelectable = true;
            this.chkCliente.CheckedChanged += new System.EventHandler(this.chkCliente_CheckedChanged);
            // 
            // chkFechas
            // 
            this.chkFechas.AutoSize = true;
            this.chkFechas.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkFechas.Location = new System.Drawing.Point(307, 8);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(96, 19);
            this.chkFechas.Style = MetroFramework.MetroColorStyle.Red;
            this.chkFechas.TabIndex = 39;
            this.chkFechas.Text = "Fecha Inicio";
            this.chkFechas.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkFechas.UseSelectable = true;
            this.chkFechas.CheckedChanged += new System.EventHandler(this.chkFechas_CheckedChanged);
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(952, 483);
            this.dtgvData.TabIndex = 5;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.AppearancePrint.Preview.Options.UseImage = true;
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            // 
            // Ingresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 670);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ingresos";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Ingresos";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ingresos_Load);
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
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroTextBox txtLote;
        private MetroFramework.Controls.MetroTextBox txtCliente;
        private MetroFramework.Controls.MetroTextBox txtProducto;
        private MetroFramework.Controls.MetroCheckBox chkProducto;
        private MetroFramework.Controls.MetroCheckBox chkLote;
        private MetroFramework.Controls.MetroCheckBox chkCliente;
        private MetroFramework.Controls.MetroCheckBox chkFechas;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroLabel lbl4;
        private MetroFramework.Controls.MetroLabel lbl3;
        private MetroFramework.Controls.MetroLabel lbl2;
        private MetroFramework.Controls.MetroLabel lbl1;
        private MetroFramework.Controls.MetroLabel lblViajes;
        private MetroFramework.Controls.MetroLabel lblSumDiferencia;
        private MetroFramework.Controls.MetroLabel lblSumIngreso;
        private MetroFramework.Controls.MetroLabel lblSumSalida;
    }
}