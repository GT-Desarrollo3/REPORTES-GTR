namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class FaltasConductores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaltasConductores));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.lblFechaRegistro = new MetroFramework.Controls.MetroLabel();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.lblTipoFalta = new MetroFramework.Controls.MetroLabel();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new MetroFramework.Controls.MetroLabel();
            this.txtIdConductor = new System.Windows.Forms.TextBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.lblNombreConductor = new MetroFramework.Controls.MetroLabel();
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
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaRegistro);
            this.splitContainer1.Panel1.Controls.Add(this.lblFechaRegistro);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.cboTipo);
            this.splitContainer1.Panel1.Controls.Add(this.lblTipoFalta);
            this.splitContainer1.Panel1.Controls.Add(this.txtDescripcion);
            this.splitContainer1.Panel1.Controls.Add(this.lblDescripcion);
            this.splitContainer1.Panel1.Controls.Add(this.txtIdConductor);
            this.splitContainer1.Panel1.Controls.Add(this.btnGuardar);
            this.splitContainer1.Panel1.Controls.Add(this.txtConductor);
            this.splitContainer1.Panel1.Controls.Add(this.lblNombreConductor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(893, 570);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "";
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(155, 54);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(148, 20);
            this.dtpFechaRegistro.TabIndex = 101;
            // 
            // lblFechaRegistro
            // 
            this.lblFechaRegistro.AutoSize = true;
            this.lblFechaRegistro.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFechaRegistro.Location = new System.Drawing.Point(10, 54);
            this.lblFechaRegistro.Name = "lblFechaRegistro";
            this.lblFechaRegistro.Size = new System.Drawing.Size(134, 19);
            this.lblFechaRegistro.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFechaRegistro.TabIndex = 100;
            this.lblFechaRegistro.Text = "Fecha de Incidencia: ";
            this.lblFechaRegistro.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            this.btnImprimir.Location = new System.Drawing.Point(845, 11);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(40, 37);
            this.btnImprimir.TabIndex = 99;
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // cboTipo
            // 
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Items.AddRange(new object[] {
            "POR COMBUSTIBLE",
            "POR EXCESO DE VELOCIDAD",
            "POR MERCADERIA",
            "POR INCIDENCIAS",
            "OTROS"});
            this.cboTipo.Location = new System.Drawing.Point(155, 86);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(182, 21);
            this.cboTipo.TabIndex = 97;
            // 
            // lblTipoFalta
            // 
            this.lblTipoFalta.AutoSize = true;
            this.lblTipoFalta.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblTipoFalta.Location = new System.Drawing.Point(10, 86);
            this.lblTipoFalta.Name = "lblTipoFalta";
            this.lblTipoFalta.Size = new System.Drawing.Size(94, 19);
            this.lblTipoFalta.Style = MetroFramework.MetroColorStyle.Red;
            this.lblTipoFalta.TabIndex = 96;
            this.lblTipoFalta.Text = "Tipo de Falta: ";
            this.lblTipoFalta.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(155, 119);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(321, 72);
            this.txtDescripcion.TabIndex = 94;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblDescripcion.Location = new System.Drawing.Point(10, 118);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(86, 19);
            this.lblDescripcion.Style = MetroFramework.MetroColorStyle.Red;
            this.lblDescripcion.TabIndex = 95;
            this.lblDescripcion.Text = "Descripción: ";
            this.lblDescripcion.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtIdConductor
            // 
            this.txtIdConductor.Location = new System.Drawing.Point(482, 21);
            this.txtIdConductor.Name = "txtIdConductor";
            this.txtIdConductor.Size = new System.Drawing.Size(35, 20);
            this.txtIdConductor.TabIndex = 93;
            this.txtIdConductor.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardar.Location = new System.Drawing.Point(786, 11);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(40, 37);
            this.btnGuardar.TabIndex = 92;
            this.btnGuardar.ToolTip = "Guardar cambios";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtConductor
            // 
            this.txtConductor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtConductor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtConductor.Location = new System.Drawing.Point(154, 21);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtConductor.Size = new System.Drawing.Size(322, 20);
            this.txtConductor.TabIndex = 0;
            this.txtConductor.TextChanged += new System.EventHandler(this.txtConductor_TextChanged);
            // 
            // lblNombreConductor
            // 
            this.lblNombreConductor.AutoSize = true;
            this.lblNombreConductor.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblNombreConductor.Location = new System.Drawing.Point(10, 20);
            this.lblNombreConductor.Name = "lblNombreConductor";
            this.lblNombreConductor.Size = new System.Drawing.Size(135, 19);
            this.lblNombreConductor.Style = MetroFramework.MetroColorStyle.Red;
            this.lblNombreConductor.TabIndex = 62;
            this.lblNombreConductor.Text = "Nombre Conductor: ";
            this.lblNombreConductor.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Darkroom";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(893, 368);
            this.dtgvData.TabIndex = 4;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.AppearancePrint.Preview.Image = ((System.Drawing.Image)(resources.GetObject("dtgvDataView.AppearancePrint.Preview.Image")));
            this.dtgvDataView.AppearancePrint.Preview.Options.UseImage = true;
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            // 
            // FaltasConductores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 650);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FaltasConductores";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Registro de Faltas Cometidas por Conductores";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.FaltasConductores_Load);
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
        private MetroFramework.Controls.MetroLabel lblNombreConductor;
        private System.Windows.Forms.TextBox txtConductor;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private MetroFramework.Controls.MetroLabel lblTipoFalta;
        private System.Windows.Forms.TextBox txtDescripcion;
        private MetroFramework.Controls.MetroLabel lblDescripcion;
        private System.Windows.Forms.TextBox txtIdConductor;
        private System.Windows.Forms.ComboBox cboTipo;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private MetroFramework.Controls.MetroLabel lblFechaRegistro;
    }
}