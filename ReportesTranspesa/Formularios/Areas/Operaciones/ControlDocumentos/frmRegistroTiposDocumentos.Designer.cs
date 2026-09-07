namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmRegistroTiposDocumentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroTiposDocumentos));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.tsBtnGuardar = new System.Windows.Forms.ToolStripButton();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxTipoRelacion = new System.Windows.Forms.ComboBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblRelacion = new System.Windows.Forms.Label();
            this.txtNemonico = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDias = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Khaki;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnSalir,
            this.tsBtnGuardar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(452, 33);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(49, 30);
            this.tsBtnSalir.Text = "Salir";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // tsBtnGuardar
            // 
            this.tsBtnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.tsBtnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnGuardar.Name = "tsBtnGuardar";
            this.tsBtnGuardar.Size = new System.Drawing.Size(69, 30);
            this.tsBtnGuardar.Text = "Guardar";
            this.tsBtnGuardar.Click += new System.EventHandler(this.tsBtnGuardar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Tipo Relación:";
            // 
            // cbxTipoRelacion
            // 
            this.cbxTipoRelacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoRelacion.FormattingEnabled = true;
            this.cbxTipoRelacion.Location = new System.Drawing.Point(9, 58);
            this.cbxTipoRelacion.Name = "cbxTipoRelacion";
            this.cbxTipoRelacion.Size = new System.Drawing.Size(98, 21);
            this.cbxTipoRelacion.TabIndex = 0;
            this.cbxTipoRelacion.SelectedIndexChanged += new System.EventHandler(this.cbxTipoRelacion_SelectedIndexChanged);
            this.cbxTipoRelacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxTipoRelacion_KeyPress);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(124, 107);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(314, 20);
            this.txtDescripcion.TabIndex = 3;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // lblRelacion
            // 
            this.lblRelacion.AutoSize = true;
            this.lblRelacion.Location = new System.Drawing.Point(122, 91);
            this.lblRelacion.Name = "lblRelacion";
            this.lblRelacion.Size = new System.Drawing.Size(66, 13);
            this.lblRelacion.TabIndex = 27;
            this.lblRelacion.Text = "Descripción:";
            // 
            // txtNemonico
            // 
            this.txtNemonico.Location = new System.Drawing.Point(9, 107);
            this.txtNemonico.Name = "txtNemonico";
            this.txtNemonico.Size = new System.Drawing.Size(98, 20);
            this.txtNemonico.TabIndex = 2;
            this.txtNemonico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNemonico_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 91;
            this.label8.Text = "Nemonico:";
            // 
            // txtDias
            // 
            this.txtDias.Location = new System.Drawing.Point(124, 58);
            this.txtDias.Name = "txtDias";
            this.txtDias.Size = new System.Drawing.Size(71, 20);
            this.txtDias.TabIndex = 1;
            this.txtDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDias_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 93;
            this.label1.Text = "Dias de aviso:";
            // 
            // frmRegistroTiposDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(452, 143);
            this.Controls.Add(this.txtDias);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNemonico);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblRelacion);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxTipoRelacion);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmRegistroTiposDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión Tipos de Documentos";
            this.Load += new System.EventHandler(this.frmRegistroDocumentos_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripButton tsBtnGuardar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxTipoRelacion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblRelacion;
        public System.Windows.Forms.TextBox txtNemonico;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtDias;
        private System.Windows.Forms.Label label1;
    }
}