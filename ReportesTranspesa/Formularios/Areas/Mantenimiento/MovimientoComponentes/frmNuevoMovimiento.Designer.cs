namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.MovimientoComponentes
{
    partial class frmNuevoMovimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevoMovimiento));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComponente = new System.Windows.Forms.TextBox();
            this.cbxSubSistema = new MetroFramework.Controls.MetroComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxSistema = new MetroFramework.Controls.MetroComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescipcionR = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNroReq = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaEjecucion = new System.Windows.Forms.DateTimePicker();
            this.txtPlacaDest = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPlacaProd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lstPlaca2 = new System.Windows.Forms.ListView();
            this.lstPlaca1 = new System.Windows.Forms.ListView();
            this.lstRequerimiento = new System.Windows.Forms.ListView();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.lstPersonal = new System.Windows.Forms.ListView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(600, 43);
            this.label1.TabIndex = 14;
            this.label1.Text = "C";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtComponente);
            this.groupBox2.Controls.Add(this.cbxSubSistema);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbxSistema);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox2.Location = new System.Drawing.Point(12, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 152);
            this.groupBox2.TabIndex = 228;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS DEL COMPONENTE: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(15, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 222;
            this.label2.Text = "Descripción:";
            // 
            // txtComponente
            // 
            this.txtComponente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtComponente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComponente.ForeColor = System.Drawing.SystemColors.Window;
            this.txtComponente.Location = new System.Drawing.Point(96, 92);
            this.txtComponente.Multiline = true;
            this.txtComponente.Name = "txtComponente";
            this.txtComponente.Size = new System.Drawing.Size(461, 40);
            this.txtComponente.TabIndex = 221;
            // 
            // cbxSubSistema
            // 
            this.cbxSubSistema.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbxSubSistema.FormattingEnabled = true;
            this.cbxSubSistema.ItemHeight = 19;
            this.cbxSubSistema.Location = new System.Drawing.Point(298, 49);
            this.cbxSubSistema.Name = "cbxSubSistema";
            this.cbxSubSistema.Size = new System.Drawing.Size(259, 25);
            this.cbxSubSistema.Style = MetroFramework.MetroColorStyle.Red;
            this.cbxSubSistema.TabIndex = 220;
            this.cbxSubSistema.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cbxSubSistema.UseSelectable = true;
            this.cbxSubSistema.SelectedIndexChanged += new System.EventHandler(this.cbxSubSistema_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(295, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 219;
            this.label4.Text = "Subsistema:";
            // 
            // cbxSistema
            // 
            this.cbxSistema.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbxSistema.FormattingEnabled = true;
            this.cbxSistema.ItemHeight = 19;
            this.cbxSistema.Location = new System.Drawing.Point(18, 49);
            this.cbxSistema.Name = "cbxSistema";
            this.cbxSistema.Size = new System.Drawing.Size(259, 25);
            this.cbxSistema.Style = MetroFramework.MetroColorStyle.Red;
            this.cbxSistema.TabIndex = 218;
            this.cbxSistema.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cbxSistema.UseSelectable = true;
            this.cbxSistema.SelectedIndexChanged += new System.EventHandler(this.cbxSistema_SelectedIndexChanged);
            this.cbxSistema.DropDownClosed += new System.EventHandler(this.cbxSistema_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(15, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 217;
            this.label5.Text = "Sistema:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescipcionR);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtNroReq);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMotivo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpFechaEjecucion);
            this.groupBox1.Controls.Add(this.txtPlacaDest);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPlacaProd);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lstPlaca2);
            this.groupBox1.Controls.Add(this.lstPlaca1);
            this.groupBox1.Controls.Add(this.lstRequerimiento);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(12, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 235);
            this.groupBox1.TabIndex = 229;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DEL MOVIMIENTO: ";
            // 
            // txtDescipcionR
            // 
            this.txtDescipcionR.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtDescipcionR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescipcionR.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDescipcionR.Location = new System.Drawing.Point(214, 93);
            this.txtDescipcionR.Name = "txtDescipcionR";
            this.txtDescipcionR.ReadOnly = true;
            this.txtDescipcionR.Size = new System.Drawing.Size(343, 20);
            this.txtDescipcionR.TabIndex = 236;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.ForeColor = System.Drawing.SystemColors.Window;
            this.txtNombre.Location = new System.Drawing.Point(113, 134);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(444, 21);
            this.txtNombre.TabIndex = 233;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersona_KeyPress);
            this.txtNombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPersona_KeyUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label10.Location = new System.Drawing.Point(18, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 15);
            this.label10.TabIndex = 232;
            this.label10.Text = "Autorizado por:";
            // 
            // txtNroReq
            // 
            this.txtNroReq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNroReq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroReq.ForeColor = System.Drawing.SystemColors.Window;
            this.txtNroReq.Location = new System.Drawing.Point(113, 92);
            this.txtNroReq.Name = "txtNroReq";
            this.txtNroReq.Size = new System.Drawing.Size(95, 21);
            this.txtNroReq.TabIndex = 231;
            this.txtNroReq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNroReq_KeyPress);
            this.txtNroReq.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNroReq_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(15, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 15);
            this.label9.TabIndex = 230;
            this.label9.Text = "Requerimiento:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(15, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 229;
            this.label6.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.ForeColor = System.Drawing.SystemColors.Window;
            this.txtMotivo.Location = new System.Drawing.Point(67, 175);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(490, 40);
            this.txtMotivo.TabIndex = 228;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(401, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 15);
            this.label3.TabIndex = 227;
            this.label3.Text = "Fecha de Ejecución:";
            // 
            // dtpFechaEjecucion
            // 
            this.dtpFechaEjecucion.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.dtpFechaEjecucion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaEjecucion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaEjecucion.Location = new System.Drawing.Point(404, 50);
            this.dtpFechaEjecucion.Name = "dtpFechaEjecucion";
            this.dtpFechaEjecucion.Size = new System.Drawing.Size(153, 21);
            this.dtpFechaEjecucion.TabIndex = 226;
            this.dtpFechaEjecucion.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpFechaEjecucion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaEjecucion_KeyPress);
            // 
            // txtPlacaDest
            // 
            this.txtPlacaDest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPlacaDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlacaDest.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPlacaDest.Location = new System.Drawing.Point(214, 50);
            this.txtPlacaDest.Name = "txtPlacaDest";
            this.txtPlacaDest.Size = new System.Drawing.Size(136, 21);
            this.txtPlacaDest.TabIndex = 225;
            this.txtPlacaDest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlacaDest_KeyPress);
            this.txtPlacaDest.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlacaDest_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label8.Location = new System.Drawing.Point(211, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 15);
            this.label8.TabIndex = 224;
            this.label8.Text = "Unidad de Destino:";
            // 
            // txtPlacaProd
            // 
            this.txtPlacaProd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPlacaProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlacaProd.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPlacaProd.Location = new System.Drawing.Point(18, 50);
            this.txtPlacaProd.Name = "txtPlacaProd";
            this.txtPlacaProd.Size = new System.Drawing.Size(136, 21);
            this.txtPlacaProd.TabIndex = 223;
            this.txtPlacaProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlacaProd_KeyPress);
            this.txtPlacaProd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlacaProd_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label7.Location = new System.Drawing.Point(15, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 15);
            this.label7.TabIndex = 217;
            this.label7.Text = "Unidad de Procedencia:";
            // 
            // lstPlaca2
            // 
            this.lstPlaca2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaca2.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca2.FullRowSelect = true;
            this.lstPlaca2.GridLines = true;
            this.lstPlaca2.Location = new System.Drawing.Point(214, 70);
            this.lstPlaca2.MultiSelect = false;
            this.lstPlaca2.Name = "lstPlaca2";
            this.lstPlaca2.Size = new System.Drawing.Size(200, 110);
            this.lstPlaca2.TabIndex = 234;
            this.lstPlaca2.UseCompatibleStateImageBehavior = false;
            this.lstPlaca2.View = System.Windows.Forms.View.Details;
            this.lstPlaca2.Visible = false;
            this.lstPlaca2.Enter += new System.EventHandler(this.lstPlaca2_Enter);
            this.lstPlaca2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca2_KeyPress);
            this.lstPlaca2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca2_MouseDoubleClick);
            // 
            // lstPlaca1
            // 
            this.lstPlaca1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaca1.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca1.FullRowSelect = true;
            this.lstPlaca1.GridLines = true;
            this.lstPlaca1.Location = new System.Drawing.Point(18, 70);
            this.lstPlaca1.MultiSelect = false;
            this.lstPlaca1.Name = "lstPlaca1";
            this.lstPlaca1.Size = new System.Drawing.Size(200, 110);
            this.lstPlaca1.TabIndex = 230;
            this.lstPlaca1.UseCompatibleStateImageBehavior = false;
            this.lstPlaca1.View = System.Windows.Forms.View.Details;
            this.lstPlaca1.Visible = false;
            this.lstPlaca1.Enter += new System.EventHandler(this.lstPlaca1_Enter);
            this.lstPlaca1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca1_KeyPress);
            this.lstPlaca1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca1_MouseDoubleClick);
            // 
            // lstRequerimiento
            // 
            this.lstRequerimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstRequerimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRequerimiento.ForeColor = System.Drawing.Color.Navy;
            this.lstRequerimiento.FullRowSelect = true;
            this.lstRequerimiento.GridLines = true;
            this.lstRequerimiento.Location = new System.Drawing.Point(113, 112);
            this.lstRequerimiento.MultiSelect = false;
            this.lstRequerimiento.Name = "lstRequerimiento";
            this.lstRequerimiento.Size = new System.Drawing.Size(444, 110);
            this.lstRequerimiento.TabIndex = 235;
            this.lstRequerimiento.UseCompatibleStateImageBehavior = false;
            this.lstRequerimiento.View = System.Windows.Forms.View.Details;
            this.lstRequerimiento.Visible = false;
            this.lstRequerimiento.Enter += new System.EventHandler(this.lstRequerimiento_Enter);
            this.lstRequerimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRequerimiento_KeyPress);
            this.lstRequerimiento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRequerimiento_MouseDoubleClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnCancelar.Location = new System.Drawing.Point(191, 475);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(97, 36);
            this.btnCancelar.TabIndex = 236;
            this.btnCancelar.Tag = "5";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Guardar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnGuardar.Location = new System.Drawing.Point(313, 475);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(97, 36);
            this.btnGuardar.TabIndex = 235;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lstPersonal
            // 
            this.lstPersonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPersonal.ForeColor = System.Drawing.Color.Navy;
            this.lstPersonal.FullRowSelect = true;
            this.lstPersonal.GridLines = true;
            this.lstPersonal.Location = new System.Drawing.Point(125, 375);
            this.lstPersonal.MultiSelect = false;
            this.lstPersonal.Name = "lstPersonal";
            this.lstPersonal.Size = new System.Drawing.Size(444, 110);
            this.lstPersonal.TabIndex = 236;
            this.lstPersonal.UseCompatibleStateImageBehavior = false;
            this.lstPersonal.View = System.Windows.Forms.View.Details;
            this.lstPersonal.Visible = false;
            this.lstPersonal.Enter += new System.EventHandler(this.lstPersonal_Enter);
            this.lstPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersonal_KeyPress);
            this.lstPersonal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersonal_MouseDoubleClick);
            // 
            // frmNuevoMovimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(600, 530);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPersonal);
            this.Name = "frmNuevoMovimiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MOVIMIENTO DE COMPONENTES";
            this.Load += new System.EventHandler(this.frmNuevoMovimiento_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtComponente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtPlacaDest;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtPlacaProd;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtNroReq;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.ListView lstPlaca1;
        public DevExpress.XtraEditors.SimpleButton btnCancelar;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        public MetroFramework.Controls.MetroComboBox cbxSubSistema;
        public MetroFramework.Controls.MetroComboBox cbxSistema;
        private System.Windows.Forms.ListView lstPlaca2;
        private System.Windows.Forms.ListView lstRequerimiento;
        public System.Windows.Forms.DateTimePicker dtpFechaEjecucion;
        private System.Windows.Forms.ListView lstPersonal;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDescipcionR;
    }
}