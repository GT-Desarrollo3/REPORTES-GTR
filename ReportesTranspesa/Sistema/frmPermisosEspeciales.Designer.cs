namespace ReportesTranspesa.Sistema
{
    partial class frmPermisosEspeciales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPermisosEspeciales));
            this.txtFormulario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.dtgvLsitarPermisos = new System.Windows.Forms.DataGridView();
            this.Check2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.dtPermisosEsp = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLsitarPermisos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPermisosEsp)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFormulario
            // 
            this.txtFormulario.Location = new System.Drawing.Point(73, 19);
            this.txtFormulario.Name = "txtFormulario";
            this.txtFormulario.ReadOnly = true;
            this.txtFormulario.Size = new System.Drawing.Size(369, 20);
            this.txtFormulario.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Formulario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(521, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(580, 19);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(374, 20);
            this.txtUsuario.TabIndex = 10;
            // 
            // dtgvLsitarPermisos
            // 
            this.dtgvLsitarPermisos.AllowUserToAddRows = false;
            this.dtgvLsitarPermisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvLsitarPermisos.BackgroundColor = System.Drawing.Color.White;
            this.dtgvLsitarPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvLsitarPermisos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check2});
            this.dtgvLsitarPermisos.Location = new System.Drawing.Point(524, 51);
            this.dtgvLsitarPermisos.MultiSelect = false;
            this.dtgvLsitarPermisos.Name = "dtgvLsitarPermisos";
            this.dtgvLsitarPermisos.RowHeadersVisible = false;
            this.dtgvLsitarPermisos.ShowCellErrors = false;
            this.dtgvLsitarPermisos.ShowCellToolTips = false;
            this.dtgvLsitarPermisos.ShowEditingIcon = false;
            this.dtgvLsitarPermisos.ShowRowErrors = false;
            this.dtgvLsitarPermisos.Size = new System.Drawing.Size(430, 220);
            this.dtgvLsitarPermisos.TabIndex = 11;
            // 
            // Check2
            // 
            this.Check2.HeaderText = "Check";
            this.Check2.Name = "Check2";
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.White;
            this.btnQuitar.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitar.Image")));
            this.btnQuitar.Location = new System.Drawing.Point(448, 177);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(70, 57);
            this.btnQuitar.TabIndex = 12;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // dtPermisosEsp
            // 
            this.dtPermisosEsp.AllowUserToAddRows = false;
            this.dtPermisosEsp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtPermisosEsp.BackgroundColor = System.Drawing.Color.White;
            this.dtPermisosEsp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtPermisosEsp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dtPermisosEsp.Location = new System.Drawing.Point(12, 51);
            this.dtPermisosEsp.MultiSelect = false;
            this.dtPermisosEsp.Name = "dtPermisosEsp";
            this.dtPermisosEsp.RowHeadersVisible = false;
            this.dtPermisosEsp.ShowCellErrors = false;
            this.dtPermisosEsp.ShowCellToolTips = false;
            this.dtPermisosEsp.ShowEditingIcon = false;
            this.dtPermisosEsp.ShowRowErrors = false;
            this.dtPermisosEsp.Size = new System.Drawing.Size(430, 220);
            this.dtPermisosEsp.TabIndex = 4;
            // 
            // check
            // 
            this.check.HeaderText = "Check";
            this.check.Name = "check";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.Location = new System.Drawing.Point(448, 66);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(70, 57);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmPermisosEspeciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(961, 283);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.dtgvLsitarPermisos);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtPermisosEsp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFormulario);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPermisosEspeciales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPermisosEspeciales";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPermisosEspeciales_FormClosed);
            this.Load += new System.EventHandler(this.frmPermisosEspeciales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLsitarPermisos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPermisosEsp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFormulario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnQuitar;
        public System.Windows.Forms.DataGridView dtgvLsitarPermisos;
        private System.Windows.Forms.DataGridView dtPermisosEsp;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check2;
    }
}