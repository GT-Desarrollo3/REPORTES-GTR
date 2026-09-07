namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroBaterias
{
    partial class frmTraspasoBateria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTraspasoBateria));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTipoAct = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPlacaAct = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FechaIni = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIntervalo = new System.Windows.Forms.TextBox();
            this.dtpFechaCambio = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNivelCarga = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaInspeccion = new System.Windows.Forms.DateTimePicker();
            this.txtEstadoB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(771, 36);
            this.label1.TabIndex = 21;
            this.label1.Text = "TRASPASO DE BATERÍA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtTipoAct);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtPlacaAct);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(396, 50);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(363, 58);
            this.groupBox3.TabIndex = 206;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Seleccionar Vehículo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Tipo:\r\n";
            // 
            // txtTipoAct
            // 
            this.txtTipoAct.Location = new System.Drawing.Point(232, 24);
            this.txtTipoAct.Name = "txtTipoAct";
            this.txtTipoAct.ReadOnly = true;
            this.txtTipoAct.Size = new System.Drawing.Size(111, 20);
            this.txtTipoAct.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Placa:";
            // 
            // txtPlacaAct
            // 
            this.txtPlacaAct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPlacaAct.Location = new System.Drawing.Point(58, 24);
            this.txtPlacaAct.Name = "txtPlacaAct";
            this.txtPlacaAct.Size = new System.Drawing.Size(111, 20);
            this.txtPlacaAct.TabIndex = 4;
            this.txtPlacaAct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlacaAct_KeyPress);
            this.txtPlacaAct.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlacaAct_KeyUp);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label45.Location = new System.Drawing.Point(406, 136);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(42, 13);
            this.label45.TabIndex = 203;
            this.label45.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(409, 161);
            this.txtMotivo.MaxLength = 150;
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(338, 71);
            this.txtMotivo.TabIndex = 202;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(522, 295);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(114, 44);
            this.btnGuardar.TabIndex = 207;
            this.btnGuardar.Text = "Cambiar\r\nBatería";
            this.btnGuardar.ToolTip = "Cambiar Batería";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(454, 95);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(275, 104);
            this.lstPlaca.TabIndex = 208;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(104, 67);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.ReadOnly = true;
            this.txtMarca.Size = new System.Drawing.Size(237, 20);
            this.txtMarca.TabIndex = 0;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(58, 70);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(40, 13);
            this.label41.TabIndex = 1;
            this.label41.Text = "Marca:";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(104, 106);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.ReadOnly = true;
            this.txtModelo.Size = new System.Drawing.Size(237, 20);
            this.txtModelo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Modelo / Placa:";
            // 
            // FechaIni
            // 
            this.FechaIni.CustomFormat = "dd/MM/yyyy";
            this.FechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaIni.Location = new System.Drawing.Point(104, 144);
            this.FechaIni.Name = "FechaIni";
            this.FechaIni.Size = new System.Drawing.Size(96, 20);
            this.FechaIni.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fecha Inicio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "Contador\r\nde Días:";
            // 
            // txtDuracion
            // 
            this.txtDuracion.Location = new System.Drawing.Point(283, 144);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.ReadOnly = true;
            this.txtDuracion.Size = new System.Drawing.Size(58, 20);
            this.txtDuracion.TabIndex = 7;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(104, 28);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(237, 20);
            this.txtCodigo.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(55, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Codigo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Duración (Días):";
            // 
            // txtIntervalo
            // 
            this.txtIntervalo.Location = new System.Drawing.Point(104, 182);
            this.txtIntervalo.Name = "txtIntervalo";
            this.txtIntervalo.ReadOnly = true;
            this.txtIntervalo.Size = new System.Drawing.Size(58, 20);
            this.txtIntervalo.TabIndex = 15;
            // 
            // dtpFechaCambio
            // 
            this.dtpFechaCambio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaCambio.Enabled = false;
            this.dtpFechaCambio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCambio.Location = new System.Drawing.Point(245, 182);
            this.dtpFechaCambio.Name = "dtpFechaCambio";
            this.dtpFechaCambio.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaCambio.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 178);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 26);
            this.label9.TabIndex = 17;
            this.label9.Text = "Fecha\r\nCambio:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtEstadoB);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtNivelCarga);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.dtpFechaInspeccion);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.dtpFechaCambio);
            this.groupBox2.Controls.Add(this.txtIntervalo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.txtDuracion);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.FechaIni);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtModelo);
            this.groupBox2.Controls.Add(this.label41);
            this.groupBox2.Controls.Add(this.txtMarca);
            this.groupBox2.Location = new System.Drawing.Point(12, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 297);
            this.groupBox2.TabIndex = 205;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS DE BATERIA:";
            // 
            // txtNivelCarga
            // 
            this.txtNivelCarga.BackColor = System.Drawing.SystemColors.Control;
            this.txtNivelCarga.Enabled = false;
            this.txtNivelCarga.Location = new System.Drawing.Point(104, 220);
            this.txtNivelCarga.Name = "txtNivelCarga";
            this.txtNivelCarga.Size = new System.Drawing.Size(58, 20);
            this.txtNivelCarga.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 26);
            this.label6.TabIndex = 20;
            this.label6.Text = "Nivel de Carga\r\n(%):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "F. Inspección:";
            // 
            // dtpFechaInspeccion
            // 
            this.dtpFechaInspeccion.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInspeccion.Enabled = false;
            this.dtpFechaInspeccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInspeccion.Location = new System.Drawing.Point(104, 258);
            this.dtpFechaInspeccion.Name = "dtpFechaInspeccion";
            this.dtpFechaInspeccion.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaInspeccion.TabIndex = 18;
            // 
            // txtEstadoB
            // 
            this.txtEstadoB.BackColor = System.Drawing.SystemColors.Control;
            this.txtEstadoB.Enabled = false;
            this.txtEstadoB.Location = new System.Drawing.Point(283, 220);
            this.txtEstadoB.Name = "txtEstadoB";
            this.txtEstadoB.Size = new System.Drawing.Size(58, 20);
            this.txtEstadoB.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(184, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 26);
            this.label12.TabIndex = 22;
            this.label12.Text = "Estado de Batería\r\n(%):";
            // 
            // frmTraspasoBateria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(771, 362);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPlaca);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "frmTraspasoBateria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRASPASO DE BATERÍA";
            this.Load += new System.EventHandler(this.frmTraspasoBateria_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTipoAct;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPlacaAct;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtMotivo;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.ListView lstPlaca;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker FechaIni;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtIntervalo;
        public System.Windows.Forms.DateTimePicker dtpFechaCambio;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtNivelCarga;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.DateTimePicker dtpFechaInspeccion;
        public System.Windows.Forms.TextBox txtEstadoB;
        public System.Windows.Forms.Label label12;
    }
}