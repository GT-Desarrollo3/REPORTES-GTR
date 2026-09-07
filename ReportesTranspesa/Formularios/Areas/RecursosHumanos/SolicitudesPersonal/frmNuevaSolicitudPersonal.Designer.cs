namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.SolicitudesPersonal
{
    partial class frmNuevaSolicitudPersonal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevaSolicitudPersonal));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.txtReemplazo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxTipoSolicitud = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNroVacante = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxPuesto = new System.Windows.Forms.ComboBox();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAprobar = new DevExpress.XtraEditors.SimpleButton();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.btnFechaEntrega = new DevExpress.XtraEditors.SimpleButton();
            this.lstPersona = new System.Windows.Forms.ListView();
            this.cbxPrioridad = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Área:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxPrioridad);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtObservacion);
            this.groupBox1.Controls.Add(this.txtReemplazo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxTipoSolicitud);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNroVacante);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxPuesto);
            this.groupBox1.Controls.Add(this.cbxArea);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 252);
            this.groupBox1.TabIndex = 113;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Solicitud";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 122;
            this.label6.Text = "Observación:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.BackColor = System.Drawing.Color.LightCyan;
            this.txtObservacion.Location = new System.Drawing.Point(134, 175);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(402, 58);
            this.txtObservacion.TabIndex = 121;
            // 
            // txtReemplazo
            // 
            this.txtReemplazo.BackColor = System.Drawing.Color.LightCyan;
            this.txtReemplazo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReemplazo.Location = new System.Drawing.Point(134, 139);
            this.txtReemplazo.MaxLength = 250;
            this.txtReemplazo.Name = "txtReemplazo";
            this.txtReemplazo.Size = new System.Drawing.Size(402, 20);
            this.txtReemplazo.TabIndex = 119;
            this.txtReemplazo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonalReemplazo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 118;
            this.label5.Text = "Personal a reemplazar:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(292, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 117;
            this.label4.Text = "Tipo de Solicitud:";
            // 
            // cbxTipoSolicitud
            // 
            this.cbxTipoSolicitud.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoSolicitud.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoSolicitud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoSolicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxTipoSolicitud.FormattingEnabled = true;
            this.cbxTipoSolicitud.Location = new System.Drawing.Point(387, 101);
            this.cbxTipoSolicitud.Name = "cbxTipoSolicitud";
            this.cbxTipoSolicitud.Size = new System.Drawing.Size(149, 21);
            this.cbxTipoSolicitud.TabIndex = 116;
            this.cbxTipoSolicitud.SelectedIndexChanged += new System.EventHandler(this.cbxTipoSolicitud_SelectedIndexChanged);
            this.cbxTipoSolicitud.DropDownClosed += new System.EventHandler(this.cbxTipoSolicitud_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 115;
            this.label3.Text = "N° Vacante(s):";
            // 
            // txtNroVacante
            // 
            this.txtNroVacante.BackColor = System.Drawing.Color.LightCyan;
            this.txtNroVacante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNroVacante.Location = new System.Drawing.Point(134, 101);
            this.txtNroVacante.MaxLength = 250;
            this.txtNroVacante.Name = "txtNroVacante";
            this.txtNroVacante.Size = new System.Drawing.Size(83, 20);
            this.txtNroVacante.TabIndex = 114;
            this.txtNroVacante.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNroVacante_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 113;
            this.label2.Text = "Puesto:";
            // 
            // cbxPuesto
            // 
            this.cbxPuesto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxPuesto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxPuesto.BackColor = System.Drawing.Color.White;
            this.cbxPuesto.FormattingEnabled = true;
            this.cbxPuesto.Location = new System.Drawing.Point(134, 63);
            this.cbxPuesto.Name = "cbxPuesto";
            this.cbxPuesto.Size = new System.Drawing.Size(402, 21);
            this.cbxPuesto.TabIndex = 112;
            this.cbxPuesto.SelectedIndexChanged += new System.EventHandler(this.cbxPuesto_SelectedIndexChanged);
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(134, 26);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(163, 21);
            this.cbxArea.TabIndex = 111;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(763, 281);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(91, 40);
            this.btnGuardar.TabIndex = 123;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar Solicitud";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(645, 281);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(91, 40);
            this.btnCancelar.TabIndex = 124;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar Solicitud";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAprobar);
            this.groupBox2.Controls.Add(this.cbxEstado);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(597, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 78);
            this.groupBox2.TabIndex = 114;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Aprobación de Solicitud";
            // 
            // btnAprobar
            // 
            this.btnAprobar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAprobar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAprobar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAprobar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnAprobar.Appearance.Options.UseBackColor = true;
            this.btnAprobar.Appearance.Options.UseBorderColor = true;
            this.btnAprobar.Appearance.Options.UseFont = true;
            this.btnAprobar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAprobar.Image = ((System.Drawing.Image)(resources.GetObject("btnAprobar.Image")));
            this.btnAprobar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAprobar.Location = new System.Drawing.Point(244, 20);
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.Size = new System.Drawing.Size(48, 44);
            this.btnAprobar.TabIndex = 123;
            this.btnAprobar.ToolTip = "Registrar";
            this.btnAprobar.Click += new System.EventHandler(this.btnAprobar_Click);
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Location = new System.Drawing.Point(60, 34);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(153, 21);
            this.cbxEstado.TabIndex = 111;
            this.cbxEstado.SelectedIndexChanged += new System.EventHandler(this.cbxEstado_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Estado:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.dtpFechaEntrega);
            this.groupBox3.Controls.Add(this.btnFechaEntrega);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(597, 163);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 78);
            this.groupBox3.TabIndex = 125;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ingresar Fecha de Entrega";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 125;
            this.label7.Text = "Fecha Estimada:";
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(103, 33);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(110, 21);
            this.dtpFechaEntrega.TabIndex = 124;
            this.dtpFechaEntrega.Tag = "1";
            // 
            // btnFechaEntrega
            // 
            this.btnFechaEntrega.Appearance.BackColor = System.Drawing.Color.White;
            this.btnFechaEntrega.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnFechaEntrega.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnFechaEntrega.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnFechaEntrega.Appearance.Options.UseBackColor = true;
            this.btnFechaEntrega.Appearance.Options.UseBorderColor = true;
            this.btnFechaEntrega.Appearance.Options.UseFont = true;
            this.btnFechaEntrega.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechaEntrega.Image = ((System.Drawing.Image)(resources.GetObject("btnFechaEntrega.Image")));
            this.btnFechaEntrega.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnFechaEntrega.Location = new System.Drawing.Point(244, 20);
            this.btnFechaEntrega.Name = "btnFechaEntrega";
            this.btnFechaEntrega.Size = new System.Drawing.Size(48, 44);
            this.btnFechaEntrega.TabIndex = 123;
            this.btnFechaEntrega.ToolTip = "Registrar";
            this.btnFechaEntrega.Click += new System.EventHandler(this.btnFechaEntrega_Click);
            // 
            // lstPersona
            // 
            this.lstPersona.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstPersona.ForeColor = System.Drawing.Color.Navy;
            this.lstPersona.FullRowSelect = true;
            this.lstPersona.GridLines = true;
            this.lstPersona.Location = new System.Drawing.Point(154, 228);
            this.lstPersona.MultiSelect = false;
            this.lstPersona.Name = "lstPersona";
            this.lstPersona.Size = new System.Drawing.Size(402, 127);
            this.lstPersona.TabIndex = 126;
            this.lstPersona.UseCompatibleStateImageBehavior = false;
            this.lstPersona.View = System.Windows.Forms.View.Details;
            this.lstPersona.Visible = false;
            this.lstPersona.Enter += new System.EventHandler(this.lstPersona_Enter);
            this.lstPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersona_KeyPress);
            this.lstPersona.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersona_MouseDoubleClick);
            // 
            // cbxPrioridad
            // 
            this.cbxPrioridad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxPrioridad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrioridad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxPrioridad.FormattingEnabled = true;
            this.cbxPrioridad.Items.AddRange(new object[] {
            "BAJA",
            "NORMAL",
            "URGENTE"});
            this.cbxPrioridad.Location = new System.Drawing.Point(387, 26);
            this.cbxPrioridad.Name = "cbxPrioridad";
            this.cbxPrioridad.Size = new System.Drawing.Size(149, 21);
            this.cbxPrioridad.TabIndex = 124;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(330, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 123;
            this.label8.Text = "Prioridad:";
            // 
            // frmNuevaSolicitudPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 364);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstPersona);
            this.MaximizeBox = false;
            this.Name = "frmNuevaSolicitudPersonal";
            this.Text = "NUEVA SOLICITUD DE PERSONAL";
            this.Load += new System.EventHandler(this.frmNuevaSolicitud_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cbxArea;
        public System.Windows.Forms.ComboBox cbxPuesto;
        public System.Windows.Forms.TextBox txtNroVacante;
        public System.Windows.Forms.ComboBox cbxTipoSolicitud;
        public System.Windows.Forms.ComboBox cbxEstado;
        public System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.ListView lstPersona;
        public System.Windows.Forms.TextBox txtReemplazo;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
        public DevExpress.XtraEditors.SimpleButton btnCancelar;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        public DevExpress.XtraEditors.SimpleButton btnAprobar;
        public DevExpress.XtraEditors.SimpleButton btnFechaEntrega;
        public System.Windows.Forms.ComboBox cbxPrioridad;
        private System.Windows.Forms.Label label8;
    }
}