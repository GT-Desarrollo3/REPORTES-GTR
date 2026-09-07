namespace ReportesTranspesa.Sistema
{
    partial class CambiarBD
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
            this.lblDataSource = new MetroFramework.Controls.MetroLabel();
            this.lblCatalagoBD = new MetroFramework.Controls.MetroLabel();
            this.lblUserID = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnAceptar = new MetroFramework.Controls.MetroButton();
            this.txtUserID = new MetroFramework.Controls.MetroTextBox();
            this.txtPaswword = new MetroFramework.Controls.MetroTextBox();
            this.cboCatalagoBD = new MetroFramework.Controls.MetroComboBox();
            this.cboDataSource = new MetroFramework.Controls.MetroComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblDataSource.Location = new System.Drawing.Point(36, 40);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(108, 25);
            this.lblDataSource.Style = MetroFramework.MetroColorStyle.Red;
            this.lblDataSource.TabIndex = 1;
            this.lblDataSource.Text = "DataSource: ";
            this.lblDataSource.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblCatalagoBD
            // 
            this.lblCatalagoBD.AutoSize = true;
            this.lblCatalagoBD.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCatalagoBD.Location = new System.Drawing.Point(36, 76);
            this.lblCatalagoBD.Name = "lblCatalagoBD";
            this.lblCatalagoBD.Size = new System.Drawing.Size(115, 25);
            this.lblCatalagoBD.Style = MetroFramework.MetroColorStyle.Red;
            this.lblCatalagoBD.TabIndex = 2;
            this.lblCatalagoBD.Text = "Catalago BD: ";
            this.lblCatalagoBD.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblUserID.Location = new System.Drawing.Point(36, 114);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(76, 25);
            this.lblUserID.Style = MetroFramework.MetroColorStyle.Red;
            this.lblUserID.TabIndex = 3;
            this.lblUserID.Text = "User ID: ";
            this.lblUserID.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(36, 152);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(91, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Password: ";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // btnAceptar
            // 
            this.btnAceptar.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnAceptar.Location = new System.Drawing.Point(140, 195);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(76, 42);
            this.btnAceptar.Style = MetroFramework.MetroColorStyle.Red;
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnAceptar.UseSelectable = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtUserID
            // 
            this.txtUserID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUserID.Lines = new string[0];
            this.txtUserID.Location = new System.Drawing.Point(169, 109);
            this.txtUserID.MaxLength = 32767;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.PasswordChar = '\0';
            this.txtUserID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserID.SelectedText = "";
            this.txtUserID.Size = new System.Drawing.Size(166, 29);
            this.txtUserID.Style = MetroFramework.MetroColorStyle.Red;
            this.txtUserID.TabIndex = 120;
            this.txtUserID.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtUserID.UseSelectable = true;
            // 
            // txtPaswword
            // 
            this.txtPaswword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPaswword.Lines = new string[0];
            this.txtPaswword.Location = new System.Drawing.Point(169, 151);
            this.txtPaswword.MaxLength = 32767;
            this.txtPaswword.Name = "txtPaswword";
            this.txtPaswword.PasswordChar = '*';
            this.txtPaswword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPaswword.SelectedText = "";
            this.txtPaswword.Size = new System.Drawing.Size(166, 29);
            this.txtPaswword.Style = MetroFramework.MetroColorStyle.Red;
            this.txtPaswword.TabIndex = 121;
            this.txtPaswword.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtPaswword.UseSelectable = true;
            // 
            // cboCatalagoBD
            // 
            this.cboCatalagoBD.FormattingEnabled = true;
            this.cboCatalagoBD.ItemHeight = 23;
            this.cboCatalagoBD.Items.AddRange(new object[] {
            "TRANSPESA",
            "BPM",
            "springpruebas",
            "spring"});
            this.cboCatalagoBD.Location = new System.Drawing.Point(169, 71);
            this.cboCatalagoBD.Name = "cboCatalagoBD";
            this.cboCatalagoBD.Size = new System.Drawing.Size(166, 29);
            this.cboCatalagoBD.Style = MetroFramework.MetroColorStyle.Red;
            this.cboCatalagoBD.TabIndex = 127;
            this.cboCatalagoBD.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboCatalagoBD.UseSelectable = true;
            // 
            // cboDataSource
            // 
            this.cboDataSource.FormattingEnabled = true;
            this.cboDataSource.ItemHeight = 23;
            this.cboDataSource.Items.AddRange(new object[] {
            "192.168.4.234",
            "192.168.4.237",
            "192.168.4.15"});
            this.cboDataSource.Location = new System.Drawing.Point(169, 36);
            this.cboDataSource.Name = "cboDataSource";
            this.cboDataSource.Size = new System.Drawing.Size(166, 29);
            this.cboDataSource.Style = MetroFramework.MetroColorStyle.Red;
            this.cboDataSource.TabIndex = 128;
            this.cboDataSource.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboDataSource.UseSelectable = true;
            this.cboDataSource.SelectedIndexChanged += new System.EventHandler(this.cboDataSource_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtServidor);
            this.groupBox1.Controls.Add(this.lblCatalagoBD);
            this.groupBox1.Controls.Add(this.cboDataSource);
            this.groupBox1.Controls.Add(this.lblDataSource);
            this.groupBox1.Controls.Add(this.cboCatalagoBD);
            this.groupBox1.Controls.Add(this.lblUserID);
            this.groupBox1.Controls.Add(this.txtPaswword);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.txtUserID);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(23, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 257);
            this.groupBox1.TabIndex = 129;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instancia - Base de Datos";
            // 
            // txtServidor
            // 
            this.txtServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServidor.Location = new System.Drawing.Point(41, 206);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.ReadOnly = true;
            this.txtServidor.Size = new System.Drawing.Size(85, 20);
            this.txtServidor.TabIndex = 130;
            this.txtServidor.Visible = false;
            // 
            // CambiarBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 343);
            this.Controls.Add(this.groupBox1);
            this.Name = "CambiarBD";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Cadena de Conexión ";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.CambiarBD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblDataSource;
        private MetroFramework.Controls.MetroLabel lblCatalagoBD;
        private MetroFramework.Controls.MetroLabel lblUserID;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton btnAceptar;
        private MetroFramework.Controls.MetroTextBox txtUserID;
        private MetroFramework.Controls.MetroTextBox txtPaswword;
        private MetroFramework.Controls.MetroComboBox cboCatalagoBD;
        private MetroFramework.Controls.MetroComboBox cboDataSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServidor;
    }
}