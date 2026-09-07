namespace ReportesTranspesa.Formularios.Areas
{
    partial class ConfirmacionAprobacion
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtDocumento = new MetroFramework.Controls.MetroTextBox();
            this.lblObligacion = new MetroFramework.Controls.MetroLabel();
            this.txtCajaChica = new MetroFramework.Controls.MetroTextBox();
            this.lblCajaChica = new MetroFramework.Controls.MetroLabel();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPeriodo = new MetroFramework.Controls.MetroTextBox();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtDocumento);
            this.splitContainer1.Panel1.Controls.Add(this.lblObligacion);
            this.splitContainer1.Panel1.Controls.Add(this.txtCajaChica);
            this.splitContainer1.Panel1.Controls.Add(this.lblCajaChica);
            this.splitContainer1.Panel1.Controls.Add(this.btnCancelar);
            this.splitContainer1.Panel1.Controls.Add(this.btnAceptar);
            this.splitContainer1.Panel1.Controls.Add(this.txtPeriodo);
            this.splitContainer1.Panel1.Controls.Add(this.lblPeriodo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(730, 279);
            this.splitContainer1.SplitterDistance = 42;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtDocumento
            // 
            this.txtDocumento.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDocumento.Lines = new string[0];
            this.txtDocumento.Location = new System.Drawing.Point(452, 9);
            this.txtDocumento.MaxLength = 32767;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.PasswordChar = '\0';
            this.txtDocumento.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDocumento.SelectedText = "";
            this.txtDocumento.Size = new System.Drawing.Size(103, 29);
            this.txtDocumento.Style = MetroFramework.MetroColorStyle.Red;
            this.txtDocumento.TabIndex = 119;
            this.txtDocumento.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtDocumento.UseSelectable = true;
            // 
            // lblObligacion
            // 
            this.lblObligacion.AutoSize = true;
            this.lblObligacion.Location = new System.Drawing.Point(366, 14);
            this.lblObligacion.Name = "lblObligacion";
            this.lblObligacion.Size = new System.Drawing.Size(80, 19);
            this.lblObligacion.Style = MetroFramework.MetroColorStyle.Red;
            this.lblObligacion.TabIndex = 118;
            this.lblObligacion.Text = "Obligación :";
            this.lblObligacion.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtCajaChica
            // 
            this.txtCajaChica.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCajaChica.Lines = new string[0];
            this.txtCajaChica.Location = new System.Drawing.Point(300, 7);
            this.txtCajaChica.MaxLength = 32767;
            this.txtCajaChica.Name = "txtCajaChica";
            this.txtCajaChica.PasswordChar = '\0';
            this.txtCajaChica.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCajaChica.SelectedText = "";
            this.txtCajaChica.Size = new System.Drawing.Size(63, 29);
            this.txtCajaChica.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCajaChica.TabIndex = 117;
            this.txtCajaChica.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCajaChica.UseSelectable = true;
            // 
            // lblCajaChica
            // 
            this.lblCajaChica.AutoSize = true;
            this.lblCajaChica.Location = new System.Drawing.Point(163, 14);
            this.lblCajaChica.Name = "lblCajaChica";
            this.lblCajaChica.Size = new System.Drawing.Size(131, 19);
            this.lblCajaChica.Style = MetroFramework.MetroColorStyle.Red;
            this.lblCajaChica.TabIndex = 116;
            this.lblCajaChica.Text = "Numero Caja Chica :";
            this.lblCajaChica.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(647, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(70, 23);
            this.btnCancelar.TabIndex = 115;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(571, 14);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(70, 23);
            this.btnAceptar.TabIndex = 114;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPeriodo.Lines = new string[] {
        "2019-11"};
            this.txtPeriodo.Location = new System.Drawing.Point(84, 9);
            this.txtPeriodo.MaxLength = 32767;
            this.txtPeriodo.Name = "txtPeriodo";
            this.txtPeriodo.PasswordChar = '\0';
            this.txtPeriodo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPeriodo.SelectedText = "";
            this.txtPeriodo.Size = new System.Drawing.Size(63, 29);
            this.txtPeriodo.Style = MetroFramework.MetroColorStyle.Red;
            this.txtPeriodo.TabIndex = 113;
            this.txtPeriodo.Text = "2019-11";
            this.txtPeriodo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtPeriodo.UseSelectable = true;
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Location = new System.Drawing.Point(16, 14);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(63, 19);
            this.lblPeriodo.Style = MetroFramework.MetroColorStyle.Red;
            this.lblPeriodo.TabIndex = 111;
            this.lblPeriodo.Text = "Periodo :";
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
            this.dtgvData.Size = new System.Drawing.Size(730, 233);
            this.dtgvData.TabIndex = 116;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowAutoFilterRow = true;
            this.dtgvDataView.OptionsView.ShowFooter = true;
            // 
            // ConfirmacionAprobacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 359);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ConfirmacionAprobacion";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Confirmación de Aprobación de Obligación";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.ConfirmacionAprobacion_Load);
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
        private MetroFramework.Controls.MetroLabel lblPeriodo;
        private MetroFramework.Controls.MetroTextBox txtPeriodo;
        private DevExpress.XtraEditors.SimpleButton btnAceptar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroTextBox txtCajaChica;
        private MetroFramework.Controls.MetroLabel lblCajaChica;
        private MetroFramework.Controls.MetroTextBox txtDocumento;
        private MetroFramework.Controls.MetroLabel lblObligacion;
    }
}