namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmProgramarMtto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgramarMtto));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProgramacion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFrecuencia = new System.Windows.Forms.TextBox();
            this.cbxAceite = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPS = new System.Windows.Forms.TextBox();
            this.dtpUltFecha = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUltimoKM = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUltMtto = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.txtKilometraje = new System.Windows.Forms.TextBox();
            this.cbxTipoMtto = new System.Windows.Forms.ComboBox();
            this.dtpFechaMtto = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxTipoUnidad = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.txtUbigeo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(637, 43);
            this.label1.TabIndex = 21;
            this.label1.Text = "ACTUALIZAR MANTENIMIENTO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUbigeo);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtModelo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMarca);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtProgramacion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtFrecuencia);
            this.groupBox1.Controls.Add(this.cbxAceite);
            this.groupBox1.Location = new System.Drawing.Point(12, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 125);
            this.groupBox1.TabIndex = 203;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DE UNIDAD:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(58, 25);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(126, 20);
            this.txtPlaca.TabIndex = 12;
            this.txtPlaca.Enter += new System.EventHandler(this.txtPlaca_Enter);
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            this.txtPlaca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlaca_KeyUp);
            this.txtPlaca.Leave += new System.EventHandler(this.txtPlaca_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(424, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Frec / KM:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Modelo:";
            // 
            // txtModelo
            // 
            this.txtModelo.BackColor = System.Drawing.SystemColors.Control;
            this.txtModelo.Location = new System.Drawing.Point(282, 57);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.ReadOnly = true;
            this.txtModelo.Size = new System.Drawing.Size(126, 20);
            this.txtModelo.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Marca:";
            // 
            // txtMarca
            // 
            this.txtMarca.BackColor = System.Drawing.SystemColors.Control;
            this.txtMarca.Location = new System.Drawing.Point(58, 57);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.ReadOnly = true;
            this.txtMarca.Size = new System.Drawing.Size(126, 20);
            this.txtMarca.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Programación:";
            // 
            // txtProgramacion
            // 
            this.txtProgramacion.BackColor = System.Drawing.SystemColors.Control;
            this.txtProgramacion.Location = new System.Drawing.Point(282, 25);
            this.txtProgramacion.Name = "txtProgramacion";
            this.txtProgramacion.ReadOnly = true;
            this.txtProgramacion.Size = new System.Drawing.Size(126, 20);
            this.txtProgramacion.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(442, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Aceite:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Placa:";
            // 
            // txtFrecuencia
            // 
            this.txtFrecuencia.Location = new System.Drawing.Point(488, 57);
            this.txtFrecuencia.Name = "txtFrecuencia";
            this.txtFrecuencia.Size = new System.Drawing.Size(105, 20);
            this.txtFrecuencia.TabIndex = 13;
            this.txtFrecuencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrecuencia_KeyPress);
            // 
            // cbxAceite
            // 
            this.cbxAceite.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxAceite.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAceite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAceite.FormattingEnabled = true;
            this.cbxAceite.Items.AddRange(new object[] {
            "MINERAL",
            "MINERAL - VISTONE",
            "SINTETICO",
            "SINTETICO - VISTONE"});
            this.cbxAceite.Location = new System.Drawing.Point(488, 24);
            this.cbxAceite.Name = "cbxAceite";
            this.cbxAceite.Size = new System.Drawing.Size(105, 21);
            this.cbxAceite.TabIndex = 111;
            this.cbxAceite.DropDownClosed += new System.EventHandler(this.cbxAceite_DropDownClosed);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtPS);
            this.groupBox2.Controls.Add(this.dtpUltFecha);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtUltimoKM);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtUltMtto);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(12, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(613, 62);
            this.groupBox2.TabIndex = 204;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ANTERIOR MANTENIMIENTO REALIZADO:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(335, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "PS:";
            // 
            // txtPS
            // 
            this.txtPS.BackColor = System.Drawing.SystemColors.Control;
            this.txtPS.Location = new System.Drawing.Point(365, 25);
            this.txtPS.Name = "txtPS";
            this.txtPS.ReadOnly = true;
            this.txtPS.Size = new System.Drawing.Size(52, 20);
            this.txtPS.TabIndex = 7;
            // 
            // dtpUltFecha
            // 
            this.dtpUltFecha.CustomFormat = "dd/MM/yyyy";
            this.dtpUltFecha.Enabled = false;
            this.dtpUltFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpUltFecha.Location = new System.Drawing.Point(66, 25);
            this.dtpUltFecha.Name = "dtpUltFecha";
            this.dtpUltFecha.Size = new System.Drawing.Size(96, 20);
            this.dtpUltFecha.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(185, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "KM:";
            // 
            // txtUltimoKM
            // 
            this.txtUltimoKM.BackColor = System.Drawing.SystemColors.Control;
            this.txtUltimoKM.Location = new System.Drawing.Point(217, 25);
            this.txtUltimoKM.Name = "txtUltimoKM";
            this.txtUltimoKM.ReadOnly = true;
            this.txtUltimoKM.Size = new System.Drawing.Size(95, 20);
            this.txtUltimoKM.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(437, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Tipo Mtto:";
            // 
            // txtUltMtto
            // 
            this.txtUltMtto.Location = new System.Drawing.Point(498, 25);
            this.txtUltMtto.Name = "txtUltMtto";
            this.txtUltMtto.ReadOnly = true;
            this.txtUltMtto.Size = new System.Drawing.Size(95, 20);
            this.txtUltMtto.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Fecha:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.cbxOperacion);
            this.groupBox3.Controls.Add(this.txtKilometraje);
            this.groupBox3.Controls.Add(this.cbxTipoMtto);
            this.groupBox3.Controls.Add(this.dtpFechaMtto);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(14, 310);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(611, 88);
            this.groupBox3.TabIndex = 205;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NUEVO MANTENIMIENTO:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(342, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 13);
            this.label15.TabIndex = 113;
            this.label15.Text = "Operación:";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(407, 19);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(184, 21);
            this.cbxOperacion.TabIndex = 112;
            this.cbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            this.cbxOperacion.DropDownClosed += new System.EventHandler(this.cbxOperacion_DropDownClosed);
            // 
            // txtKilometraje
            // 
            this.txtKilometraje.Location = new System.Drawing.Point(182, 48);
            this.txtKilometraje.Name = "txtKilometraje";
            this.txtKilometraje.Size = new System.Drawing.Size(120, 20);
            this.txtKilometraje.TabIndex = 111;
            this.txtKilometraje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKilometraje_KeyPress);
            // 
            // cbxTipoMtto
            // 
            this.cbxTipoMtto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoMtto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoMtto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoMtto.FormattingEnabled = true;
            this.cbxTipoMtto.Location = new System.Drawing.Point(407, 53);
            this.cbxTipoMtto.Name = "cbxTipoMtto";
            this.cbxTipoMtto.Size = new System.Drawing.Size(184, 21);
            this.cbxTipoMtto.TabIndex = 110;
            this.cbxTipoMtto.SelectedIndexChanged += new System.EventHandler(this.cbxTipoMtto_SelectedIndexChanged);
            // 
            // dtpFechaMtto
            // 
            this.dtpFechaMtto.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaMtto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaMtto.Location = new System.Drawing.Point(22, 48);
            this.dtpFechaMtto.Name = "dtpFechaMtto";
            this.dtpFechaMtto.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaMtto.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Kilometraje:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(346, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tipo Mtto:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Fecha de Mantenimiento:";
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(70, 138);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(223, 104);
            this.lstPlaca.TabIndex = 130;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 62);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 13);
            this.label14.TabIndex = 208;
            this.label14.Text = "Tipo de Unidad:";
            // 
            // cbxTipoUnidad
            // 
            this.cbxTipoUnidad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoUnidad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoUnidad.FormattingEnabled = true;
            this.cbxTipoUnidad.Location = new System.Drawing.Point(113, 59);
            this.cbxTipoUnidad.Name = "cbxTipoUnidad";
            this.cbxTipoUnidad.Size = new System.Drawing.Size(168, 21);
            this.cbxTipoUnidad.TabIndex = 209;
            this.cbxTipoUnidad.SelectedIndexChanged += new System.EventHandler(this.cbxTipoUnidad_SelectedIndexChanged);
            this.cbxTipoUnidad.DropDownClosed += new System.EventHandler(this.cbxTipoUnidad_DropDownClosed);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(198, 414);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 34);
            this.btnCancelar.TabIndex = 207;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(332, 414);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(107, 34);
            this.btnAgregar.TabIndex = 206;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtUbigeo
            // 
            this.txtUbigeo.Location = new System.Drawing.Point(58, 89);
            this.txtUbigeo.Name = "txtUbigeo";
            this.txtUbigeo.Size = new System.Drawing.Size(535, 20);
            this.txtUbigeo.TabIndex = 113;
            this.txtUbigeo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUbigeo_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 92);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 112;
            this.label17.Text = "Ubigeo:";
            // 
            // frmProgramarMtto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(637, 463);
            this.Controls.Add(this.cbxTipoUnidad);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPlaca);
            this.MaximizeBox = false;
            this.Name = "frmProgramarMtto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PROGRAMAR MANTENIMIENTO - VEHÍCULOS";
            this.Load += new System.EventHandler(this.frmProgramarMtto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtProgramacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtUltimoKM;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtUltMtto;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.DateTimePicker dtpUltFecha;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.DateTimePicker dtpFechaMtto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.ListView lstPlaca;
        public System.Windows.Forms.TextBox txtPlaca;
        public System.Windows.Forms.TextBox txtFrecuencia;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox cbxAceite;
        public System.Windows.Forms.TextBox txtKilometraje;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.ComboBox cbxTipoUnidad;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.ComboBox cbxOperacion;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtPS;
        public System.Windows.Forms.ComboBox cbxTipoMtto;
        public System.Windows.Forms.TextBox txtUbigeo;
        private System.Windows.Forms.Label label17;
    }
}