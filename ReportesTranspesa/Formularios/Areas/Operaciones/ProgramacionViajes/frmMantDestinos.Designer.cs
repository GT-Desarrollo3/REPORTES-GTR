namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    partial class frmMantDestinos
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
            this.lblOtMaes = new System.Windows.Forms.Label();
            this.txtMaesDestino = new System.Windows.Forms.TextBox();
            this.lblDestino = new System.Windows.Forms.Label();
            this.txtMaesdias = new System.Windows.Forms.TextBox();
            this.txtMaesKm = new System.Windows.Forms.TextBox();
            this.lblKm = new System.Windows.Forms.Label();
            this.lblDias = new System.Windows.Forms.Label();
            this.lblZona = new System.Windows.Forms.Label();
            this.txtZona = new System.Windows.Forms.TextBox();
            this.chbModificar = new System.Windows.Forms.CheckBox();
            this.txtOrden = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOtMaes
            // 
            this.lblOtMaes.AutoSize = true;
            this.lblOtMaes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtMaes.Location = new System.Drawing.Point(40, 12);
            this.lblOtMaes.Name = "lblOtMaes";
            this.lblOtMaes.Size = new System.Drawing.Size(0, 16);
            this.lblOtMaes.TabIndex = 2;
            // 
            // txtMaesDestino
            // 
            this.txtMaesDestino.Location = new System.Drawing.Point(77, 65);
            this.txtMaesDestino.Name = "txtMaesDestino";
            this.txtMaesDestino.Size = new System.Drawing.Size(194, 20);
            this.txtMaesDestino.TabIndex = 0;
            this.txtMaesDestino.TextChanged += new System.EventHandler(this.txtMaesDestino_TextChanged);
            // 
            // lblDestino
            // 
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(23, 68);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(46, 13);
            this.lblDestino.TabIndex = 4;
            this.lblDestino.Text = "Destino:";
            // 
            // txtMaesdias
            // 
            this.txtMaesdias.Location = new System.Drawing.Point(77, 151);
            this.txtMaesdias.Name = "txtMaesdias";
            this.txtMaesdias.Size = new System.Drawing.Size(66, 20);
            this.txtMaesdias.TabIndex = 3;
            this.txtMaesdias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaesdias_KeyPress);
            // 
            // txtMaesKm
            // 
            this.txtMaesKm.Location = new System.Drawing.Point(77, 120);
            this.txtMaesKm.Name = "txtMaesKm";
            this.txtMaesKm.Size = new System.Drawing.Size(66, 20);
            this.txtMaesKm.TabIndex = 2;
            this.txtMaesKm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaesKm_KeyPress);
            // 
            // lblKm
            // 
            this.lblKm.AutoSize = true;
            this.lblKm.Location = new System.Drawing.Point(44, 123);
            this.lblKm.Name = "lblKm";
            this.lblKm.Size = new System.Drawing.Size(25, 13);
            this.lblKm.TabIndex = 7;
            this.lblKm.Text = "Km:";
            // 
            // lblDias
            // 
            this.lblDias.AutoSize = true;
            this.lblDias.Location = new System.Drawing.Point(38, 154);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(31, 13);
            this.lblDias.TabIndex = 8;
            this.lblDias.Text = "Dias:";
            // 
            // lblZona
            // 
            this.lblZona.AutoSize = true;
            this.lblZona.Location = new System.Drawing.Point(34, 93);
            this.lblZona.Name = "lblZona";
            this.lblZona.Size = new System.Drawing.Size(35, 13);
            this.lblZona.TabIndex = 10;
            this.lblZona.Text = "Zona:";
            // 
            // txtZona
            // 
            this.txtZona.Location = new System.Drawing.Point(77, 90);
            this.txtZona.Name = "txtZona";
            this.txtZona.Size = new System.Drawing.Size(132, 20);
            this.txtZona.TabIndex = 1;
            // 
            // chbModificar
            // 
            this.chbModificar.AutoSize = true;
            this.chbModificar.Location = new System.Drawing.Point(37, 42);
            this.chbModificar.Name = "chbModificar";
            this.chbModificar.Size = new System.Drawing.Size(69, 17);
            this.chbModificar.TabIndex = 6;
            this.chbModificar.Text = "Modificar";
            this.chbModificar.UseVisualStyleBackColor = true;
            this.chbModificar.CheckedChanged += new System.EventHandler(this.chbModificar_CheckedChanged);
            // 
            // txtOrden
            // 
            this.txtOrden.Location = new System.Drawing.Point(77, 182);
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.Size = new System.Drawing.Size(66, 20);
            this.txtOrden.TabIndex = 11;
            this.txtOrden.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Orden:";
            this.label1.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.btnCancelar.Location = new System.Drawing.Point(89, 219);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 33);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.savemini;
            this.btnGuardar.Location = new System.Drawing.Point(229, 219);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 33);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmMantDestinos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(316, 264);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOrden);
            this.Controls.Add(this.chbModificar);
            this.Controls.Add(this.lblZona);
            this.Controls.Add(this.txtZona);
            this.Controls.Add(this.lblDias);
            this.Controls.Add(this.lblKm);
            this.Controls.Add(this.txtMaesKm);
            this.Controls.Add(this.txtMaesdias);
            this.Controls.Add(this.lblDestino);
            this.Controls.Add(this.txtMaesDestino);
            this.Controls.Add(this.lblOtMaes);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Name = "frmMantDestinos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maestro Destinos";
            this.Load += new System.EventHandler(this.frmMantDestinos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblOtMaes;
        private System.Windows.Forms.TextBox txtMaesDestino;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.TextBox txtMaesdias;
        private System.Windows.Forms.TextBox txtMaesKm;
        private System.Windows.Forms.Label lblKm;
        private System.Windows.Forms.Label lblDias;
        private System.Windows.Forms.Label lblZona;
        private System.Windows.Forms.TextBox txtZona;
        private System.Windows.Forms.CheckBox chbModificar;
        private System.Windows.Forms.TextBox txtOrden;
        private System.Windows.Forms.Label label1;
    }
}