namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmConstanciaImprimir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConstanciaImprimir));
            this.rbMemorandum = new MetroFramework.Controls.MetroRadioButton();
            this.rbCompromiso = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtAsunto = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.dtpFecha = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.txtCuerpo = new MetroFramework.Controls.MetroTextBox();
            this.cboFirma = new MetroFramework.Controls.MetroCheckBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // rbMemorandum
            // 
            this.rbMemorandum.AutoSize = true;
            this.rbMemorandum.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.rbMemorandum.Location = new System.Drawing.Point(162, 63);
            this.rbMemorandum.Name = "rbMemorandum";
            this.rbMemorandum.Size = new System.Drawing.Size(142, 25);
            this.rbMemorandum.Style = MetroFramework.MetroColorStyle.Red;
            this.rbMemorandum.TabIndex = 20;
            this.rbMemorandum.Text = "Memorándum";
            this.rbMemorandum.UseSelectable = true;
            this.rbMemorandum.Visible = false;
            // 
            // rbCompromiso
            // 
            this.rbCompromiso.AutoSize = true;
            this.rbCompromiso.Checked = true;
            this.rbCompromiso.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.rbCompromiso.Location = new System.Drawing.Point(23, 63);
            this.rbCompromiso.Name = "rbCompromiso";
            this.rbCompromiso.Size = new System.Drawing.Size(133, 25);
            this.rbCompromiso.Style = MetroFramework.MetroColorStyle.Red;
            this.rbCompromiso.TabIndex = 19;
            this.rbCompromiso.TabStop = true;
            this.rbCompromiso.Text = "Compromiso";
            this.rbCompromiso.UseSelectable = true;
            this.rbCompromiso.Visible = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 104);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(53, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 24;
            this.metroLabel1.Text = "Asunto";
            // 
            // txtAsunto
            // 
            this.txtAsunto.Lines = new string[0];
            this.txtAsunto.Location = new System.Drawing.Point(89, 104);
            this.txtAsunto.MaxLength = 32767;
            this.txtAsunto.Multiline = true;
            this.txtAsunto.Name = "txtAsunto";
            this.txtAsunto.PasswordChar = '\0';
            this.txtAsunto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAsunto.SelectedText = "";
            this.txtAsunto.Size = new System.Drawing.Size(508, 50);
            this.txtAsunto.Style = MetroFramework.MetroColorStyle.Red;
            this.txtAsunto.TabIndex = 84;
            this.txtAsunto.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(23, 165);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(44, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 85;
            this.metroLabel2.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(89, 160);
            this.dtpFecha.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(103, 29);
            this.dtpFecha.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFecha.TabIndex = 86;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(23, 195);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(54, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 87;
            this.metroLabel3.Text = "Cuerpo";
            // 
            // txtCuerpo
            // 
            this.txtCuerpo.Lines = new string[0];
            this.txtCuerpo.Location = new System.Drawing.Point(89, 195);
            this.txtCuerpo.MaxLength = 32767;
            this.txtCuerpo.Multiline = true;
            this.txtCuerpo.Name = "txtCuerpo";
            this.txtCuerpo.PasswordChar = '\0';
            this.txtCuerpo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCuerpo.SelectedText = "";
            this.txtCuerpo.Size = new System.Drawing.Size(508, 180);
            this.txtCuerpo.Style = MetroFramework.MetroColorStyle.Red;
            this.txtCuerpo.TabIndex = 88;
            this.txtCuerpo.UseSelectable = true;
            // 
            // cboFirma
            // 
            this.cboFirma.AutoSize = true;
            this.cboFirma.Checked = true;
            this.cboFirma.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboFirma.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.cboFirma.Location = new System.Drawing.Point(89, 391);
            this.cboFirma.Name = "cboFirma";
            this.cboFirma.Size = new System.Drawing.Size(107, 19);
            this.cboFirma.Style = MetroFramework.MetroColorStyle.Red;
            this.cboFirma.TabIndex = 90;
            this.cboFirma.Text = "Insertar firma";
            this.cboFirma.UseSelectable = true;
            this.cboFirma.Visible = false;
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
            this.btnGuardar.Location = new System.Drawing.Point(557, 380);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(40, 43);
            this.btnGuardar.TabIndex = 89;
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmConstanciaImprimir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 433);
            this.Controls.Add(this.cboFirma);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtCuerpo);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.txtAsunto);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.rbMemorandum);
            this.Controls.Add(this.rbCompromiso);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConstanciaImprimir";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Generar Constancia No Deudo";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.Memos_Compromisos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroRadioButton rbMemorandum;
        private MetroFramework.Controls.MetroRadioButton rbCompromiso;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtAsunto;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime dtpFecha;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox txtCuerpo;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private MetroFramework.Controls.MetroCheckBox cboFirma;
    }
}