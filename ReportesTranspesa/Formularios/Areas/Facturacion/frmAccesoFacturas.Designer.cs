namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    partial class frmAccesoFacturas
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblFactura = new System.Windows.Forms.Label();
            this.lblDatoFactura = new System.Windows.Forms.Label();
            this.cbhDescenlace = new System.Windows.Forms.CheckBox();
            this.cbhModifcaMontos = new System.Windows.Forms.CheckBox();
            this.cbhEliminarFacturas = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(23, 135);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(140, 135);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactura.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblFactura.Location = new System.Drawing.Point(41, 30);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(64, 16);
            this.lblFactura.TabIndex = 2;
            this.lblFactura.Text = "Factura:";
            // 
            // lblDatoFactura
            // 
            this.lblDatoFactura.AutoSize = true;
            this.lblDatoFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatoFactura.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblDatoFactura.Location = new System.Drawing.Point(117, 30);
            this.lblDatoFactura.Name = "lblDatoFactura";
            this.lblDatoFactura.Size = new System.Drawing.Size(0, 16);
            this.lblDatoFactura.TabIndex = 3;
            // 
            // cbhDescenlace
            // 
            this.cbhDescenlace.AutoSize = true;
            this.cbhDescenlace.Location = new System.Drawing.Point(45, 31);
            this.cbhDescenlace.Name = "cbhDescenlace";
            this.cbhDescenlace.Size = new System.Drawing.Size(129, 17);
            this.cbhDescenlace.TabIndex = 4;
            this.cbhDescenlace.Text = "Descenlace de Viajes";
            this.cbhDescenlace.UseVisualStyleBackColor = true;
            // 
            // cbhModifcaMontos
            // 
            this.cbhModifcaMontos.AutoSize = true;
            this.cbhModifcaMontos.Location = new System.Drawing.Point(45, 60);
            this.cbhModifcaMontos.Name = "cbhModifcaMontos";
            this.cbhModifcaMontos.Size = new System.Drawing.Size(133, 17);
            this.cbhModifcaMontos.TabIndex = 5;
            this.cbhModifcaMontos.Text = "Modificar Montos Neto";
            this.cbhModifcaMontos.UseVisualStyleBackColor = true;
            // 
            // cbhEliminarFacturas
            // 
            this.cbhEliminarFacturas.AutoSize = true;
            this.cbhEliminarFacturas.Location = new System.Drawing.Point(45, 92);
            this.cbhEliminarFacturas.Name = "cbhEliminarFacturas";
            this.cbhEliminarFacturas.Size = new System.Drawing.Size(106, 17);
            this.cbhEliminarFacturas.TabIndex = 6;
            this.cbhEliminarFacturas.Text = "Eliminar Facturas";
            this.cbhEliminarFacturas.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbhEliminarFacturas);
            this.groupBox1.Controls.Add(this.cbhModifcaMontos);
            this.groupBox1.Controls.Add(this.cbhDescenlace);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Location = new System.Drawing.Point(44, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 172);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccionar Acceso:";
            // 
            // frmAccesoFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(335, 257);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDatoFactura);
            this.Controls.Add(this.lblFactura);
            this.Name = "frmAccesoFacturas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acceso Facturas";
            this.Load += new System.EventHandler(this.frmAccesoFacturas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.Label lblDatoFactura;
        private System.Windows.Forms.CheckBox cbhDescenlace;
        private System.Windows.Forms.CheckBox cbhModifcaMontos;
        private System.Windows.Forms.CheckBox cbhEliminarFacturas;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}