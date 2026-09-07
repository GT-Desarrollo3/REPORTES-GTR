namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmSolicitarCambiosGuia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSolicitarCambiosGuia));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscarGuia = new DevExpress.XtraEditors.SimpleButton();
            this.txtNroSolicitud = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxSerieGuia = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroGuia = new System.Windows.Forms.TextBox();
            this.p_MoficiarGuia = new System.Windows.Forms.GroupBox();
            this.btnActualizarDatos = new DevExpress.XtraEditors.SimpleButton();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarreta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTarjetaCirculacion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGuia = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstCarreta = new System.Windows.Forms.ListView();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.p_solicitud = new System.Windows.Forms.GroupBox();
            this.btnSolicitarCambio = new DevExpress.XtraEditors.SimpleButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNuevoValor = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMotivoSolicitud = new System.Windows.Forms.TextBox();
            this.cbxCampo = new System.Windows.Forms.ComboBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.p_MoficiarGuia.SuspendLayout();
            this.p_solicitud.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(796, 41);
            this.label2.TabIndex = 48;
            this.label2.Text = "SOLICITAR CAMBIOS EN GUIA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "BUSCAR GUIA:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscarGuia);
            this.groupBox1.Controls.Add(this.txtNroSolicitud);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cbxSerieGuia);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNumeroGuia);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 56);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // btnBuscarGuia
            // 
            this.btnBuscarGuia.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarGuia.Image")));
            this.btnBuscarGuia.Location = new System.Drawing.Point(273, 11);
            this.btnBuscarGuia.Name = "btnBuscarGuia";
            this.btnBuscarGuia.Size = new System.Drawing.Size(97, 35);
            this.btnBuscarGuia.TabIndex = 97;
            this.btnBuscarGuia.Text = "BUSCAR";
            this.btnBuscarGuia.Click += new System.EventHandler(this.btnBuscarGuia_Click);
            // 
            // txtNroSolicitud
            // 
            this.txtNroSolicitud.Enabled = false;
            this.txtNroSolicitud.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroSolicitud.ForeColor = System.Drawing.Color.DarkRed;
            this.txtNroSolicitud.Location = new System.Drawing.Point(666, 16);
            this.txtNroSolicitud.Name = "txtNroSolicitud";
            this.txtNroSolicitud.Size = new System.Drawing.Size(92, 28);
            this.txtNroSolicitud.TabIndex = 56;
            this.txtNroSolicitud.Text = "00000000";
            this.txtNroSolicitud.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(552, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 13);
            this.label12.TabIndex = 55;
            this.label12.Text = "NRO SOLICITUD";
            // 
            // cbxSerieGuia
            // 
            this.cbxSerieGuia.FormattingEnabled = true;
            this.cbxSerieGuia.Location = new System.Drawing.Point(115, 17);
            this.cbxSerieGuia.Name = "cbxSerieGuia";
            this.cbxSerieGuia.Size = new System.Drawing.Size(56, 22);
            this.cbxSerieGuia.TabIndex = 53;
            this.cbxSerieGuia.SelectedIndexChanged += new System.EventHandler(this.cbxSerieGuia_SelectedIndexChanged);
            this.cbxSerieGuia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxSerieGuia_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(177, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "-";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtNumeroGuia
            // 
            this.txtNumeroGuia.Location = new System.Drawing.Point(191, 18);
            this.txtNumeroGuia.Name = "txtNumeroGuia";
            this.txtNumeroGuia.Size = new System.Drawing.Size(70, 21);
            this.txtNumeroGuia.TabIndex = 51;
            this.txtNumeroGuia.TextChanged += new System.EventHandler(this.txtNumeroGuia_TextChanged);
            // 
            // p_MoficiarGuia
            // 
            this.p_MoficiarGuia.Controls.Add(this.btnActualizarDatos);
            this.p_MoficiarGuia.Controls.Add(this.txtPlaca);
            this.p_MoficiarGuia.Controls.Add(this.label8);
            this.p_MoficiarGuia.Controls.Add(this.txtCarreta);
            this.p_MoficiarGuia.Controls.Add(this.label6);
            this.p_MoficiarGuia.Controls.Add(this.txtTarjetaCirculacion);
            this.p_MoficiarGuia.Controls.Add(this.label7);
            this.p_MoficiarGuia.Controls.Add(this.txtConductor);
            this.p_MoficiarGuia.Controls.Add(this.label5);
            this.p_MoficiarGuia.Controls.Add(this.txtGuia);
            this.p_MoficiarGuia.Controls.Add(this.label4);
            this.p_MoficiarGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.p_MoficiarGuia.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p_MoficiarGuia.Location = new System.Drawing.Point(0, 97);
            this.p_MoficiarGuia.Name = "p_MoficiarGuia";
            this.p_MoficiarGuia.Size = new System.Drawing.Size(796, 91);
            this.p_MoficiarGuia.TabIndex = 51;
            this.p_MoficiarGuia.TabStop = false;
            this.p_MoficiarGuia.Text = "Informacion de la guia";
            // 
            // btnActualizarDatos
            // 
            this.btnActualizarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarDatos.Image")));
            this.btnActualizarDatos.Location = new System.Drawing.Point(624, 34);
            this.btnActualizarDatos.Name = "btnActualizarDatos";
            this.btnActualizarDatos.Size = new System.Drawing.Size(133, 35);
            this.btnActualizarDatos.TabIndex = 96;
            this.btnActualizarDatos.Text = "Actualizar Datos";
            this.btnActualizarDatos.Click += new System.EventHandler(this.btnActualizarDatos_Click);
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(124, 43);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(70, 21);
            this.txtPlaca.TabIndex = 63;
            this.txtPlaca.Enter += new System.EventHandler(this.txtTracto_Enter);
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTracto_KeyPress);
            this.txtPlaca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlaca_KeyUp);
            this.txtPlaca.Leave += new System.EventHandler(this.txtTracto_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 14);
            this.label8.TabIndex = 62;
            this.label8.Text = "Tracto";
            // 
            // txtCarreta
            // 
            this.txtCarreta.Location = new System.Drawing.Point(405, 43);
            this.txtCarreta.Name = "txtCarreta";
            this.txtCarreta.Size = new System.Drawing.Size(70, 21);
            this.txtCarreta.TabIndex = 61;
            this.txtCarreta.Enter += new System.EventHandler(this.txtCarreta_Enter);
            this.txtCarreta.Leave += new System.EventHandler(this.txtCarreta_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(404, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 14);
            this.label6.TabIndex = 60;
            this.label6.Text = "Carreta";
            // 
            // txtTarjetaCirculacion
            // 
            this.txtTarjetaCirculacion.Location = new System.Drawing.Point(495, 43);
            this.txtTarjetaCirculacion.Name = "txtTarjetaCirculacion";
            this.txtTarjetaCirculacion.Size = new System.Drawing.Size(96, 21);
            this.txtTarjetaCirculacion.TabIndex = 59;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(494, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 14);
            this.label7.TabIndex = 58;
            this.label7.Text = "Tarjeta Circulacion";
            // 
            // txtConductor
            // 
            this.txtConductor.Location = new System.Drawing.Point(211, 43);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(181, 21);
            this.txtConductor.TabIndex = 57;
            this.txtConductor.Enter += new System.EventHandler(this.txtConductor_Enter);
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            this.txtConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtConductor_KeyUp);
            this.txtConductor.Leave += new System.EventHandler(this.txtConductor_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(212, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 14);
            this.label5.TabIndex = 56;
            this.label5.Text = "Conductor";
            // 
            // txtGuia
            // 
            this.txtGuia.BackColor = System.Drawing.Color.SeaShell;
            this.txtGuia.Location = new System.Drawing.Point(10, 42);
            this.txtGuia.Name = "txtGuia";
            this.txtGuia.Size = new System.Drawing.Size(97, 21);
            this.txtGuia.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Guia";
            // 
            // lstCarreta
            // 
            this.lstCarreta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstCarreta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCarreta.ForeColor = System.Drawing.Color.Navy;
            this.lstCarreta.FullRowSelect = true;
            this.lstCarreta.GridLines = true;
            this.lstCarreta.Location = new System.Drawing.Point(405, 160);
            this.lstCarreta.MultiSelect = false;
            this.lstCarreta.Name = "lstCarreta";
            this.lstCarreta.Size = new System.Drawing.Size(84, 10);
            this.lstCarreta.TabIndex = 95;
            this.lstCarreta.UseCompatibleStateImageBehavior = false;
            this.lstCarreta.View = System.Windows.Forms.View.Details;
            this.lstCarreta.Visible = false;
            this.lstCarreta.Enter += new System.EventHandler(this.lstCarreta_Enter);
            this.lstCarreta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCarreta_KeyPress);
            this.lstCarreta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstCarreta_KeyUp);
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(210, 160);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(189, 10);
            this.lstConductor.TabIndex = 94;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            this.lstConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstConductor_KeyUp);
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(123, 160);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(80, 10);
            this.lstPlaca.TabIndex = 93;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            // 
            // p_solicitud
            // 
            this.p_solicitud.Controls.Add(this.btnSolicitarCambio);
            this.p_solicitud.Controls.Add(this.label11);
            this.p_solicitud.Controls.Add(this.label10);
            this.p_solicitud.Controls.Add(this.txtNuevoValor);
            this.p_solicitud.Controls.Add(this.label9);
            this.p_solicitud.Controls.Add(this.txtMotivoSolicitud);
            this.p_solicitud.Controls.Add(this.cbxCampo);
            this.p_solicitud.Dock = System.Windows.Forms.DockStyle.Top;
            this.p_solicitud.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p_solicitud.Location = new System.Drawing.Point(0, 188);
            this.p_solicitud.Name = "p_solicitud";
            this.p_solicitud.Size = new System.Drawing.Size(796, 113);
            this.p_solicitud.TabIndex = 52;
            this.p_solicitud.TabStop = false;
            this.p_solicitud.Text = "Solicitar Cambio";
            // 
            // btnSolicitarCambio
            // 
            this.btnSolicitarCambio.Image = ((System.Drawing.Image)(resources.GetObject("btnSolicitarCambio.Image")));
            this.btnSolicitarCambio.Location = new System.Drawing.Point(632, 47);
            this.btnSolicitarCambio.Name = "btnSolicitarCambio";
            this.btnSolicitarCambio.Size = new System.Drawing.Size(126, 35);
            this.btnSolicitarCambio.TabIndex = 97;
            this.btnSolicitarCambio.Text = "Solicitar Cambio";
            this.btnSolicitarCambio.Click += new System.EventHandler(this.btnSolicitarCambio_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(133, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 14);
            this.label11.TabIndex = 66;
            this.label11.Text = "Nuevo Valor:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 14);
            this.label10.TabIndex = 65;
            this.label10.Text = "Campo Guia:";
            // 
            // txtNuevoValor
            // 
            this.txtNuevoValor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNuevoValor.Location = new System.Drawing.Point(136, 57);
            this.txtNuevoValor.Name = "txtNuevoValor";
            this.txtNuevoValor.Size = new System.Drawing.Size(230, 21);
            this.txtNuevoValor.TabIndex = 64;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(377, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 14);
            this.label9.TabIndex = 64;
            this.label9.Text = "Motivo Solicitud:";
            // 
            // txtMotivoSolicitud
            // 
            this.txtMotivoSolicitud.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMotivoSolicitud.Location = new System.Drawing.Point(377, 57);
            this.txtMotivoSolicitud.Name = "txtMotivoSolicitud";
            this.txtMotivoSolicitud.Size = new System.Drawing.Size(236, 21);
            this.txtMotivoSolicitud.TabIndex = 55;
            // 
            // cbxCampo
            // 
            this.cbxCampo.FormattingEnabled = true;
            this.cbxCampo.Items.AddRange(new object[] {
            "Tracto",
            "Carreta",
            "Tarjeta Circulacion",
            "Conductor"});
            this.cbxCampo.Location = new System.Drawing.Point(10, 55);
            this.cbxCampo.Name = "cbxCampo";
            this.cbxCampo.Size = new System.Drawing.Size(114, 22);
            this.cbxCampo.TabIndex = 55;
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtEstado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEstado.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.Location = new System.Drawing.Point(688, 10);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(96, 20);
            this.txtEstado.TabIndex = 68;
            this.txtEstado.Text = "PENDIENTE";
            this.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmSolicitarCambiosGuia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(796, 314);
            this.Controls.Add(this.lstPlaca);
            this.Controls.Add(this.lstCarreta);
            this.Controls.Add(this.lstConductor);
            this.Controls.Add(this.txtEstado);
            this.Controls.Add(this.p_solicitud);
            this.Controls.Add(this.p_MoficiarGuia);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSolicitarCambiosGuia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSolicitarCambiosGuia";
            this.Load += new System.EventHandler(this.frmSolicitarCambiosGuia_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.p_MoficiarGuia.ResumeLayout(false);
            this.p_MoficiarGuia.PerformLayout();
            this.p_solicitud.ResumeLayout(false);
            this.p_solicitud.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox p_MoficiarGuia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGuia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox p_solicitud;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEstado;
        private DevExpress.XtraEditors.SimpleButton btnActualizarDatos;
        private System.Windows.Forms.ListView lstCarreta;
        private System.Windows.Forms.ListView lstConductor;
        private System.Windows.Forms.ListView lstPlaca;
        private DevExpress.XtraEditors.SimpleButton btnSolicitarCambio;
        public System.Windows.Forms.TextBox txtCarreta;
        public System.Windows.Forms.TextBox txtTarjetaCirculacion;
        public System.Windows.Forms.TextBox txtConductor;
        public System.Windows.Forms.TextBox txtPlaca;
        public System.Windows.Forms.TextBox txtNumeroGuia;
        public System.Windows.Forms.TextBox txtNroSolicitud;
        public DevExpress.XtraEditors.SimpleButton btnBuscarGuia;
        public System.Windows.Forms.ComboBox cbxSerieGuia;
        public System.Windows.Forms.TextBox txtNuevoValor;
        public System.Windows.Forms.TextBox txtMotivoSolicitud;
        public System.Windows.Forms.ComboBox cbxCampo;
    }
}