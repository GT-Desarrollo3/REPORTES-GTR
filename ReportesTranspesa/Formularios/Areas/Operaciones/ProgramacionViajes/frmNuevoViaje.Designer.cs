namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    partial class frmNuevoViaje
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
            this.btnAgregarOt = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.txtOT = new System.Windows.Forms.TextBox();
            this.lblCodViaje = new System.Windows.Forms.Label();
            this.txtViaje = new System.Windows.Forms.TextBox();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblCatidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lbMedida = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.lblRuta = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAgregarOt
            // 
            this.btnAgregarOt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarOt.Location = new System.Drawing.Point(378, 252);
            this.btnAgregarOt.Name = "btnAgregarOt";
            this.btnAgregarOt.Size = new System.Drawing.Size(91, 39);
            this.btnAgregarOt.TabIndex = 5;
            this.btnAgregarOt.Text = "Agregar OT";
            this.btnAgregarOt.UseVisualStyleBackColor = true;
            this.btnAgregarOt.Click += new System.EventHandler(this.btnAgregarOt_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(91, 66);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(25, 13);
            this.label25.TabIndex = 130;
            this.label25.Text = "OT:";
            // 
            // txtOT
            // 
            this.txtOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOT.Location = new System.Drawing.Point(127, 63);
            this.txtOT.Name = "txtOT";
            this.txtOT.Size = new System.Drawing.Size(76, 20);
            this.txtOT.TabIndex = 0;
            this.txtOT.TextChanged += new System.EventHandler(this.txtOT_TextChanged);
            this.txtOT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOT_KeyPress);
            // 
            // lblCodViaje
            // 
            this.lblCodViaje.AutoSize = true;
            this.lblCodViaje.Location = new System.Drawing.Point(103, 12);
            this.lblCodViaje.Name = "lblCodViaje";
            this.lblCodViaje.Size = new System.Drawing.Size(69, 13);
            this.lblCodViaje.TabIndex = 134;
            this.lblCodViaje.Text = "Codigo Viaje:";
            this.lblCodViaje.Click += new System.EventHandler(this.lblCodViaje_Click);
            // 
            // txtViaje
            // 
            this.txtViaje.Location = new System.Drawing.Point(196, 9);
            this.txtViaje.Name = "txtViaje";
            this.txtViaje.ReadOnly = true;
            this.txtViaje.Size = new System.Drawing.Size(106, 20);
            this.txtViaje.TabIndex = 135;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(2, 32);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(496, 26);
            this.separatorControl1.TabIndex = 136;
            // 
            // lblCatidad
            // 
            this.lblCatidad.AutoSize = true;
            this.lblCatidad.Location = new System.Drawing.Point(64, 234);
            this.lblCatidad.Name = "lblCatidad";
            this.lblCatidad.Size = new System.Drawing.Size(52, 13);
            this.lblCatidad.TabIndex = 137;
            this.lblCatidad.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(127, 231);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(75, 20);
            this.txtCantidad.TabIndex = 4;
            // 
            // lbMedida
            // 
            this.lbMedida.AutoSize = true;
            this.lbMedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMedida.Location = new System.Drawing.Point(217, 233);
            this.lbMedida.Name = "lbMedida";
            this.lbMedida.Size = new System.Drawing.Size(0, 15);
            this.lbMedida.TabIndex = 145;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(127, 115);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(354, 20);
            this.txtCliente.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 146;
            this.label4.Text = "Cliente:";
            // 
            // txtProducto
            // 
            this.txtProducto.Location = new System.Drawing.Point(127, 174);
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Size = new System.Drawing.Size(217, 20);
            this.txtProducto.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 148;
            this.label5.Text = "Producto:";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(127, 145);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(217, 20);
            this.txtRuta.TabIndex = 2;
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Location = new System.Drawing.Point(83, 148);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(33, 13);
            this.lblRuta.TabIndex = 150;
            this.lblRuta.Text = "Ruta:";
            // 
            // frmNuevoViaje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(497, 317);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.lblRuta);
            this.Controls.Add(this.txtProducto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbMedida);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.lblCatidad);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.txtViaje);
            this.Controls.Add(this.lblCodViaje);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtOT);
            this.Controls.Add(this.btnAgregarOt);
            this.Name = "frmNuevoViaje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar OT - Consolidado";
            this.Load += new System.EventHandler(this.frmNuevoViaje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAgregarOt;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtOT;
        private System.Windows.Forms.Label lblCodViaje;
        private System.Windows.Forms.TextBox txtViaje;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.Label lblCatidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lbMedida;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProducto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label lblRuta;

    }
}