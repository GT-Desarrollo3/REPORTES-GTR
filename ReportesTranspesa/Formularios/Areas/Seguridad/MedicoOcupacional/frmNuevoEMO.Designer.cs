namespace ReportesTranspesa.Formularios.Areas.Seguridad.MedicoOcupacional
{
    partial class frmNuevoEMO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevoEMO));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.lstPersona = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCerrarLocal = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtRutaLocal = new System.Windows.Forms.RichTextBox();
            this.cbxCategoria = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCerrarResultado = new System.Windows.Forms.Button();
            this.btnBuscar2 = new System.Windows.Forms.Button();
            this.txtResultados = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtResultados3 = new System.Windows.Forms.RichTextBox();
            this.btnBuscar4 = new System.Windows.Forms.Button();
            this.btnCerrarResultado3 = new System.Windows.Forms.Button();
            this.txtResultados2 = new System.Windows.Forms.RichTextBox();
            this.btnBuscar3 = new System.Windows.Forms.Button();
            this.btnCerrarResultado2 = new System.Windows.Forms.Button();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPuesto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTipoEnfermedad = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.cbxTipoSangre = new System.Windows.Forms.ComboBox();
            this.txtTipoSangre = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1006, 41);
            this.label1.TabIndex = 22;
            this.label1.Text = "REGISTRAR EXAMEN MÉDICO OCUPACIONAL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
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
            this.btnCancelar.Location = new System.Drawing.Point(394, 401);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 36);
            this.btnCancelar.TabIndex = 203;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
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
            this.btnAgregar.Location = new System.Drawing.Point(516, 401);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(98, 36);
            this.btnAgregar.TabIndex = 202;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lstPersona
            // 
            this.lstPersona.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPersona.ForeColor = System.Drawing.Color.Navy;
            this.lstPersona.FullRowSelect = true;
            this.lstPersona.GridLines = true;
            this.lstPersona.Location = new System.Drawing.Point(143, 102);
            this.lstPersona.MultiSelect = false;
            this.lstPersona.Name = "lstPersona";
            this.lstPersona.Size = new System.Drawing.Size(325, 104);
            this.lstPersona.TabIndex = 205;
            this.lstPersona.UseCompatibleStateImageBehavior = false;
            this.lstPersona.View = System.Windows.Forms.View.Details;
            this.lstPersona.Visible = false;
            this.lstPersona.Enter += new System.EventHandler(this.lstPersona_Enter);
            this.lstPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersona_KeyPress);
            this.lstPersona.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersona_MouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnCerrarLocal);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.txtRutaLocal);
            this.groupBox2.Controls.Add(this.cbxCategoria);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dtpFechaIni);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(17, 293);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(967, 90);
            this.groupBox2.TabIndex = 206;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EMISIÓN DE EXAMEN MÉDICO: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label13.Location = new System.Drawing.Point(511, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 15);
            this.label13.TabIndex = 20;
            this.label13.Text = "Examen Médico:";
            // 
            // btnCerrarLocal
            // 
            this.btnCerrarLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarLocal.Location = new System.Drawing.Point(928, 46);
            this.btnCerrarLocal.Name = "btnCerrarLocal";
            this.btnCerrarLocal.Size = new System.Drawing.Size(26, 21);
            this.btnCerrarLocal.TabIndex = 19;
            this.btnCerrarLocal.Text = "X";
            this.btnCerrarLocal.UseVisualStyleBackColor = true;
            this.btnCerrarLocal.Click += new System.EventHandler(this.btnCerrarLocal_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(898, 46);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(26, 21);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtRutaLocal
            // 
            this.txtRutaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaLocal.Location = new System.Drawing.Point(515, 42);
            this.txtRutaLocal.Name = "txtRutaLocal";
            this.txtRutaLocal.ReadOnly = true;
            this.txtRutaLocal.Size = new System.Drawing.Size(377, 30);
            this.txtRutaLocal.TabIndex = 21;
            this.txtRutaLocal.Text = "";
            this.txtRutaLocal.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtRutaLocal_LinkClicked);
            // 
            // cbxCategoria
            // 
            this.cbxCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCategoria.FormattingEnabled = true;
            this.cbxCategoria.Items.AddRange(new object[] {
            "APTO",
            "APTO CON RESTRICCION",
            "OBSERVADO",
            "NO APTO"});
            this.cbxCategoria.Location = new System.Drawing.Point(275, 49);
            this.cbxCategoria.Name = "cbxCategoria";
            this.cbxCategoria.Size = new System.Drawing.Size(209, 23);
            this.cbxCategoria.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(122, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "--";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(141, 49);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(102, 21);
            this.dtpFechaFin.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tiempo de Validez:";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(18, 49);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(102, 21);
            this.dtpFechaIni.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(272, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Categoría:\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(319, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "Agregar Resultados (Solo Médico Ocupacional): ";
            // 
            // btnCerrarResultado
            // 
            this.btnCerrarResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarResultado.Location = new System.Drawing.Point(429, 54);
            this.btnCerrarResultado.Name = "btnCerrarResultado";
            this.btnCerrarResultado.Size = new System.Drawing.Size(26, 21);
            this.btnCerrarResultado.TabIndex = 23;
            this.btnCerrarResultado.Text = "X";
            this.btnCerrarResultado.UseVisualStyleBackColor = true;
            this.btnCerrarResultado.Click += new System.EventHandler(this.btnCerrarResultado_Click);
            // 
            // btnBuscar2
            // 
            this.btnBuscar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar2.Location = new System.Drawing.Point(399, 54);
            this.btnBuscar2.Name = "btnBuscar2";
            this.btnBuscar2.Size = new System.Drawing.Size(26, 21);
            this.btnBuscar2.TabIndex = 22;
            this.btnBuscar2.Text = "...";
            this.btnBuscar2.UseVisualStyleBackColor = true;
            this.btnBuscar2.Click += new System.EventHandler(this.btnBuscar2_Click);
            // 
            // txtResultados
            // 
            this.txtResultados.BackColor = System.Drawing.SystemColors.Control;
            this.txtResultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultados.Location = new System.Drawing.Point(16, 50);
            this.txtResultados.Name = "txtResultados";
            this.txtResultados.ReadOnly = true;
            this.txtResultados.Size = new System.Drawing.Size(377, 30);
            this.txtResultados.TabIndex = 25;
            this.txtResultados.Text = "";
            this.txtResultados.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtResultados_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtResultados3);
            this.groupBox3.Controls.Add(this.btnBuscar4);
            this.groupBox3.Controls.Add(this.btnCerrarResultado3);
            this.groupBox3.Controls.Add(this.txtResultados2);
            this.groupBox3.Controls.Add(this.btnBuscar3);
            this.groupBox3.Controls.Add(this.btnCerrarResultado2);
            this.groupBox3.Controls.Add(this.txtResultados);
            this.groupBox3.Controls.Add(this.btnBuscar2);
            this.groupBox3.Controls.Add(this.btnCerrarResultado);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(516, 57);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(468, 210);
            this.groupBox3.TabIndex = 207;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RESULTADOS DE EXAMEN: ";
            // 
            // txtResultados3
            // 
            this.txtResultados3.BackColor = System.Drawing.SystemColors.Control;
            this.txtResultados3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultados3.Location = new System.Drawing.Point(16, 157);
            this.txtResultados3.Name = "txtResultados3";
            this.txtResultados3.ReadOnly = true;
            this.txtResultados3.Size = new System.Drawing.Size(377, 30);
            this.txtResultados3.TabIndex = 32;
            this.txtResultados3.Text = "";
            this.txtResultados3.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtResultados3_LinkClicked);
            // 
            // btnBuscar4
            // 
            this.btnBuscar4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar4.Location = new System.Drawing.Point(399, 161);
            this.btnBuscar4.Name = "btnBuscar4";
            this.btnBuscar4.Size = new System.Drawing.Size(26, 21);
            this.btnBuscar4.TabIndex = 30;
            this.btnBuscar4.Text = "...";
            this.btnBuscar4.UseVisualStyleBackColor = true;
            this.btnBuscar4.Click += new System.EventHandler(this.btnBuscar4_Click);
            // 
            // btnCerrarResultado3
            // 
            this.btnCerrarResultado3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarResultado3.Location = new System.Drawing.Point(429, 161);
            this.btnCerrarResultado3.Name = "btnCerrarResultado3";
            this.btnCerrarResultado3.Size = new System.Drawing.Size(26, 21);
            this.btnCerrarResultado3.TabIndex = 31;
            this.btnCerrarResultado3.Text = "X";
            this.btnCerrarResultado3.UseVisualStyleBackColor = true;
            this.btnCerrarResultado3.Click += new System.EventHandler(this.btnCerrarResultado3_Click);
            // 
            // txtResultados2
            // 
            this.txtResultados2.BackColor = System.Drawing.SystemColors.Control;
            this.txtResultados2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultados2.Location = new System.Drawing.Point(16, 104);
            this.txtResultados2.Name = "txtResultados2";
            this.txtResultados2.ReadOnly = true;
            this.txtResultados2.Size = new System.Drawing.Size(377, 30);
            this.txtResultados2.TabIndex = 29;
            this.txtResultados2.Text = "";
            this.txtResultados2.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtResultados2_LinkClicked);
            // 
            // btnBuscar3
            // 
            this.btnBuscar3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar3.Location = new System.Drawing.Point(399, 108);
            this.btnBuscar3.Name = "btnBuscar3";
            this.btnBuscar3.Size = new System.Drawing.Size(26, 21);
            this.btnBuscar3.TabIndex = 26;
            this.btnBuscar3.Text = "...";
            this.btnBuscar3.UseVisualStyleBackColor = true;
            this.btnBuscar3.Click += new System.EventHandler(this.btnBuscar3_Click);
            // 
            // btnCerrarResultado2
            // 
            this.btnCerrarResultado2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarResultado2.Location = new System.Drawing.Point(429, 108);
            this.btnCerrarResultado2.Name = "btnCerrarResultado2";
            this.btnCerrarResultado2.Size = new System.Drawing.Size(26, 21);
            this.btnCerrarResultado2.TabIndex = 27;
            this.btnCerrarResultado2.Text = "X";
            this.btnCerrarResultado2.UseVisualStyleBackColor = true;
            this.btnCerrarResultado2.Click += new System.EventHandler(this.btnCerrarResultado2_Click);
            // 
            // txtPersonal
            // 
            this.txtPersonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPersonal.Location = new System.Drawing.Point(126, 26);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(325, 20);
            this.txtPersonal.TabIndex = 0;
            this.txtPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonal_KeyPress);
            this.txtPersonal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPersonal_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Nombres y Apellidos:";
            // 
            // txtPuesto
            // 
            this.txtPuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPuesto.Location = new System.Drawing.Point(64, 129);
            this.txtPuesto.Name = "txtPuesto";
            this.txtPuesto.ReadOnly = true;
            this.txtPuesto.Size = new System.Drawing.Size(387, 20);
            this.txtPuesto.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Puesto:\r\n";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtTipoEnfermedad);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDNI);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPuesto);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtArea);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPersonal);
            this.groupBox1.Controls.Add(this.cbxTipoSangre);
            this.groupBox1.Controls.Add(this.txtTipoSangre);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 227);
            this.groupBox1.TabIndex = 204;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DE PERSONAL: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 167);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 26);
            this.label11.TabIndex = 11;
            this.label11.Text = "Tipo de\r\nEnfermedad:\r\n";
            // 
            // txtTipoEnfermedad
            // 
            this.txtTipoEnfermedad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoEnfermedad.Location = new System.Drawing.Point(88, 164);
            this.txtTipoEnfermedad.Multiline = true;
            this.txtTipoEnfermedad.Name = "txtTipoEnfermedad";
            this.txtTipoEnfermedad.Size = new System.Drawing.Size(363, 44);
            this.txtTipoEnfermedad.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(211, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Tipo de Sangre:\r\n";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(26, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "DNI:\r\n";
            // 
            // txtDNI
            // 
            this.txtDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNI.Location = new System.Drawing.Point(64, 61);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.ReadOnly = true;
            this.txtDNI.Size = new System.Drawing.Size(120, 20);
            this.txtDNI.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Área:\r\n";
            // 
            // txtArea
            // 
            this.txtArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.Location = new System.Drawing.Point(64, 95);
            this.txtArea.Name = "txtArea";
            this.txtArea.ReadOnly = true;
            this.txtArea.Size = new System.Drawing.Size(387, 20);
            this.txtArea.TabIndex = 2;
            // 
            // cbxTipoSangre
            // 
            this.cbxTipoSangre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoSangre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoSangre.FormattingEnabled = true;
            this.cbxTipoSangre.Items.AddRange(new object[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.cbxTipoSangre.Location = new System.Drawing.Point(300, 60);
            this.cbxTipoSangre.Name = "cbxTipoSangre";
            this.cbxTipoSangre.Size = new System.Drawing.Size(120, 21);
            this.cbxTipoSangre.TabIndex = 13;
            // 
            // txtTipoSangre
            // 
            this.txtTipoSangre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoSangre.Location = new System.Drawing.Point(300, 61);
            this.txtTipoSangre.Name = "txtTipoSangre";
            this.txtTipoSangre.Size = new System.Drawing.Size(120, 20);
            this.txtTipoSangre.TabIndex = 8;
            // 
            // frmNuevoEMO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1006, 454);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPersona);
            this.Name = "frmNuevoEMO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRAR EMO";
            this.Load += new System.EventHandler(this.frmNuevoEMO_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstPersona;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker dtpFechaIni;
        public System.Windows.Forms.ComboBox cbxCategoria;
        public DevExpress.XtraEditors.SimpleButton btnCancelar;
        public System.Windows.Forms.Button btnCerrarLocal;
        public System.Windows.Forms.Button btnBuscar;
        public System.Windows.Forms.RichTextBox txtRutaLocal;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button btnCerrarResultado;
        public System.Windows.Forms.Button btnBuscar2;
        public System.Windows.Forms.RichTextBox txtResultados;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.RichTextBox txtResultados3;
        public System.Windows.Forms.Button btnBuscar4;
        public System.Windows.Forms.Button btnCerrarResultado3;
        public System.Windows.Forms.RichTextBox txtResultados2;
        public System.Windows.Forms.Button btnBuscar3;
        public System.Windows.Forms.Button btnCerrarResultado2;
        public DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtPuesto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtTipoEnfermedad;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtTipoSangre;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtArea;
        public System.Windows.Forms.ComboBox cbxTipoSangre;
    }
}