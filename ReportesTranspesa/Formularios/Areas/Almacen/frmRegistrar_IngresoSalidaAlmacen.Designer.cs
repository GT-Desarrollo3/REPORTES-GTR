namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    partial class frmRegistrar_IngresoSalidaAlmacen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistrar_IngresoSalidaAlmacen));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbtEnsacado = new System.Windows.Forms.RadioButton();
            this.rbtGranel = new System.Windows.Forms.RadioButton();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtOrdenCliente = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtVehiculo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lstCliente = new System.Windows.Forms.ListView();
            this.lstTracto = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkCliente = new DevExpress.XtraEditors.CheckEdit();
            this.checkConductor = new DevExpress.XtraEditors.CheckEdit();
            this.checkVehiculo = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtPesoGuia = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkConductor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkVehiculo.Properties)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPesoGuia)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 17F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(375, 50);
            this.label2.TabIndex = 19;
            this.label2.Text = "REGISTRAR INGRESO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.checkCliente);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.lblFecha);
            this.groupBox1.Controls.Add(this.lstConductor);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 312);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registrar";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbtEnsacado);
            this.groupBox6.Controls.Add(this.rbtGranel);
            this.groupBox6.Location = new System.Drawing.Point(185, 63);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(141, 63);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Grupo";
            // 
            // rbtEnsacado
            // 
            this.rbtEnsacado.AutoSize = true;
            this.rbtEnsacado.Location = new System.Drawing.Point(21, 39);
            this.rbtEnsacado.Name = "rbtEnsacado";
            this.rbtEnsacado.Size = new System.Drawing.Size(84, 17);
            this.rbtEnsacado.TabIndex = 1;
            this.rbtEnsacado.Text = "ENSACADO";
            this.rbtEnsacado.UseVisualStyleBackColor = true;
            // 
            // rbtGranel
            // 
            this.rbtGranel.AutoSize = true;
            this.rbtGranel.Checked = true;
            this.rbtGranel.Location = new System.Drawing.Point(21, 19);
            this.rbtGranel.Name = "rbtGranel";
            this.rbtGranel.Size = new System.Drawing.Size(69, 17);
            this.rbtGranel.TabIndex = 0;
            this.rbtGranel.TabStop = true;
            this.rbtGranel.Text = "GRANEL";
            this.rbtGranel.UseVisualStyleBackColor = true;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblFecha.Location = new System.Drawing.Point(44, 26);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(282, 34);
            this.lblFecha.TabIndex = 87;
            this.lblFecha.Text = "28-05-2024 09:08:01";
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(26, 218);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(309, 11);
            this.lstConductor.TabIndex = 85;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtOrdenCliente);
            this.groupBox5.Location = new System.Drawing.Point(19, 76);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(141, 44);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Orden Cliente";
            // 
            // txtOrdenCliente
            // 
            this.txtOrdenCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOrdenCliente.Location = new System.Drawing.Point(7, 16);
            this.txtOrdenCliente.Name = "txtOrdenCliente";
            this.txtOrdenCliente.Size = new System.Drawing.Size(124, 20);
            this.txtOrdenCliente.TabIndex = 0;
            this.txtOrdenCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrdenCliente_KeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkConductor);
            this.groupBox4.Controls.Add(this.txtConductor);
            this.groupBox4.Location = new System.Drawing.Point(19, 182);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(322, 44);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Conductor";
            // 
            // txtConductor
            // 
            this.txtConductor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductor.Location = new System.Drawing.Point(7, 16);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(309, 20);
            this.txtConductor.TabIndex = 2;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkVehiculo);
            this.groupBox3.Controls.Add(this.txtVehiculo);
            this.groupBox3.Location = new System.Drawing.Point(19, 232);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(115, 44);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vehiculo";
            // 
            // txtVehiculo
            // 
            this.txtVehiculo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVehiculo.Location = new System.Drawing.Point(7, 16);
            this.txtVehiculo.Name = "txtVehiculo";
            this.txtVehiculo.Size = new System.Drawing.Size(100, 20);
            this.txtVehiculo.TabIndex = 3;
            this.txtVehiculo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVehiculo_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCliente);
            this.groupBox2.Location = new System.Drawing.Point(19, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 44);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCliente.Location = new System.Drawing.Point(7, 16);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(294, 20);
            this.txtCliente.TabIndex = 1;
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // lstCliente
            // 
            this.lstCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCliente.ForeColor = System.Drawing.Color.Navy;
            this.lstCliente.FullRowSelect = true;
            this.lstCliente.GridLines = true;
            this.lstCliente.Location = new System.Drawing.Point(26, 218);
            this.lstCliente.MultiSelect = false;
            this.lstCliente.Name = "lstCliente";
            this.lstCliente.Size = new System.Drawing.Size(294, 10);
            this.lstCliente.TabIndex = 89;
            this.lstCliente.UseCompatibleStateImageBehavior = false;
            this.lstCliente.View = System.Windows.Forms.View.Details;
            this.lstCliente.Visible = false;
            this.lstCliente.Enter += new System.EventHandler(this.lstCliente_Enter);
            this.lstCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCliente_KeyPress);
            // 
            // lstTracto
            // 
            this.lstTracto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTracto.ForeColor = System.Drawing.Color.Navy;
            this.lstTracto.FullRowSelect = true;
            this.lstTracto.GridLines = true;
            this.lstTracto.Location = new System.Drawing.Point(26, 318);
            this.lstTracto.MultiSelect = false;
            this.lstTracto.Name = "lstTracto";
            this.lstTracto.Size = new System.Drawing.Size(108, 10);
            this.lstTracto.TabIndex = 86;
            this.lstTracto.UseCompatibleStateImageBehavior = false;
            this.lstTracto.View = System.Windows.Forms.View.Details;
            this.lstTracto.Visible = false;
            this.lstTracto.Enter += new System.EventHandler(this.lstTracto_Enter);
            this.lstTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTracto_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkCliente
            // 
            this.checkCliente.EditValue = true;
            this.checkCliente.Location = new System.Drawing.Point(64, 129);
            this.checkCliente.Name = "checkCliente";
            this.checkCliente.Properties.Caption = "";
            this.checkCliente.Size = new System.Drawing.Size(21, 19);
            this.checkCliente.TabIndex = 88;
            // 
            // checkConductor
            // 
            this.checkConductor.EditValue = true;
            this.checkConductor.Location = new System.Drawing.Point(61, -3);
            this.checkConductor.Name = "checkConductor";
            this.checkConductor.Properties.Caption = "";
            this.checkConductor.Size = new System.Drawing.Size(21, 19);
            this.checkConductor.TabIndex = 89;
            // 
            // checkVehiculo
            // 
            this.checkVehiculo.EditValue = true;
            this.checkVehiculo.Location = new System.Drawing.Point(59, -3);
            this.checkVehiculo.Name = "checkVehiculo";
            this.checkVehiculo.Properties.Caption = "";
            this.checkVehiculo.Size = new System.Drawing.Size(21, 19);
            this.checkVehiculo.TabIndex = 90;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtPesoGuia);
            this.groupBox7.Location = new System.Drawing.Point(142, 234);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(99, 44);
            this.groupBox7.TabIndex = 91;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Peso Guia (TN)";
            // 
            // txtPesoGuia
            // 
            this.txtPesoGuia.DecimalPlaces = 3;
            this.txtPesoGuia.Location = new System.Drawing.Point(10, 17);
            this.txtPesoGuia.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtPesoGuia.Name = "txtPesoGuia";
            this.txtPesoGuia.Size = new System.Drawing.Size(80, 20);
            this.txtPesoGuia.TabIndex = 24;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(259, 235);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 51);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Registrar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmRegistrar_IngresoSalidaAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(375, 366);
            this.Controls.Add(this.lstCliente);
            this.Controls.Add(this.lstTracto);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Name = "frmRegistrar_IngresoSalidaAlmacen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRegistrar_IngresoSalidaAlmacen";
            this.Load += new System.EventHandler(this.frmRegistrar_IngresoSalidaAlmacen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkConductor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkVehiculo.Properties)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPesoGuia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtOrdenCliente;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtVehiculo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.ListView lstCliente;
        private System.Windows.Forms.ListView lstConductor;
        private System.Windows.Forms.ListView lstTracto;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbtEnsacado;
        private System.Windows.Forms.RadioButton rbtGranel;
        private DevExpress.XtraEditors.CheckEdit checkCliente;
        private DevExpress.XtraEditors.CheckEdit checkConductor;
        private DevExpress.XtraEditors.CheckEdit checkVehiculo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown txtPesoGuia;
    }
}