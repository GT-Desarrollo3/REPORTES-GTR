namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmGenerarPlanillaTolvas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerarPlanillaTolvas));
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTracto = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCarreta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRedondear = new System.Windows.Forms.CheckBox();
            this.cbLindley = new System.Windows.Forms.CheckBox();
            this.txtTotalDias = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxRuta = new System.Windows.Forms.ComboBox();
            this.dtpFechaViaje = new System.Windows.Forms.DateTimePicker();
            this.txtMontoEfectivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.lstCarreta = new System.Windows.Forms.ListView();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnRegistrar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.ForestGreen;
            this.label12.Location = new System.Drawing.Point(12, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(310, 24);
            this.label12.TabIndex = 42;
            this.label12.Text = "GENERAR PLANILLA - TOLVAS";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(15, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 15);
            this.label15.TabIndex = 46;
            this.label15.Text = "Conductor:";
            // 
            // txtConductor
            // 
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductor.Location = new System.Drawing.Point(87, 30);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(399, 21);
            this.txtConductor.TabIndex = 40;
            this.txtConductor.Enter += new System.EventHandler(this.txtConductor_Enter);
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            this.txtConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtConductor_KeyUp);
            this.txtConductor.Leave += new System.EventHandler(this.txtConductor_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(37, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 15);
            this.label5.TabIndex = 43;
            this.label5.Text = "Tracto:";
            // 
            // txtTracto
            // 
            this.txtTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTracto.Location = new System.Drawing.Point(87, 70);
            this.txtTracto.Name = "txtTracto";
            this.txtTracto.Size = new System.Drawing.Size(155, 21);
            this.txtTracto.TabIndex = 44;
            this.txtTracto.Enter += new System.EventHandler(this.txtTracto_Enter);
            this.txtTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTracto_KeyPress);
            this.txtTracto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTracto_KeyUp);
            this.txtTracto.Leave += new System.EventHandler(this.txtTracto_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label14.Location = new System.Drawing.Point(31, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 15);
            this.label14.TabIndex = 48;
            this.label14.Text = "F. Viaje:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(45, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 50;
            this.label1.Text = "Ruta:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCarreta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbRedondear);
            this.groupBox1.Controls.Add(this.cbLindley);
            this.groupBox1.Controls.Add(this.txtTotalDias);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxRuta);
            this.groupBox1.Controls.Add(this.dtpFechaViaje);
            this.groupBox1.Controls.Add(this.txtMontoEfectivo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTracto);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtConductor);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.lstPlaca);
            this.groupBox1.Controls.Add(this.lstCarreta);
            this.groupBox1.Controls.Add(this.lstConductor);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 263);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DE PLANILLA: ";
            // 
            // txtCarreta
            // 
            this.txtCarreta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCarreta.Location = new System.Drawing.Point(331, 70);
            this.txtCarreta.Name = "txtCarreta";
            this.txtCarreta.Size = new System.Drawing.Size(155, 21);
            this.txtCarreta.TabIndex = 142;
            this.txtCarreta.Enter += new System.EventHandler(this.txtCarreta_Enter);
            this.txtCarreta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarreta_KeyPress);
            this.txtCarreta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCarreta_KeyUp);
            this.txtCarreta.Leave += new System.EventHandler(this.txtCarreta_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(275, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 141;
            this.label4.Text = "Carreta:";
            // 
            // cbRedondear
            // 
            this.cbRedondear.AutoSize = true;
            this.cbRedondear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRedondear.Location = new System.Drawing.Point(239, 227);
            this.cbRedondear.Name = "cbRedondear";
            this.cbRedondear.Size = new System.Drawing.Size(97, 19);
            this.cbRedondear.TabIndex = 140;
            this.cbRedondear.Text = "Turno Noche";
            this.cbRedondear.UseVisualStyleBackColor = true;
            this.cbRedondear.CheckedChanged += new System.EventHandler(this.cbRedondear_CheckedChanged);
            // 
            // cbLindley
            // 
            this.cbLindley.AutoSize = true;
            this.cbLindley.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLindley.Location = new System.Drawing.Point(361, 227);
            this.cbLindley.Name = "cbLindley";
            this.cbLindley.Size = new System.Drawing.Size(125, 19);
            this.cbLindley.TabIndex = 139;
            this.cbLindley.Text = "Operación Lindley";
            this.cbLindley.UseVisualStyleBackColor = true;
            this.cbLindley.CheckedChanged += new System.EventHandler(this.cbLindley_CheckedChanged);
            // 
            // txtTotalDias
            // 
            this.txtTotalDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTotalDias.Location = new System.Drawing.Point(331, 111);
            this.txtTotalDias.Name = "txtTotalDias";
            this.txtTotalDias.Size = new System.Drawing.Size(111, 21);
            this.txtTotalDias.TabIndex = 138;
            this.txtTotalDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalDias_KeyPress);
            this.txtTotalDias.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTotalDias_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(260, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 137;
            this.label3.Text = "Total Días:";
            // 
            // cbxRuta
            // 
            this.cbxRuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxRuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.cbxRuta.FormattingEnabled = true;
            this.cbxRuta.Location = new System.Drawing.Point(87, 151);
            this.cbxRuta.Name = "cbxRuta";
            this.cbxRuta.Size = new System.Drawing.Size(399, 21);
            this.cbxRuta.TabIndex = 136;
            this.cbxRuta.SelectedIndexChanged += new System.EventHandler(this.cbxRuta_SelectedIndexChanged);
            this.cbxRuta.DropDownClosed += new System.EventHandler(this.cbxRuta_DropDownClosed);
            // 
            // dtpFechaViaje
            // 
            this.dtpFechaViaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaViaje.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaViaje.Location = new System.Drawing.Point(87, 111);
            this.dtpFechaViaje.Name = "dtpFechaViaje";
            this.dtpFechaViaje.Size = new System.Drawing.Size(102, 21);
            this.dtpFechaViaje.TabIndex = 54;
            this.dtpFechaViaje.Tag = "1";
            // 
            // txtMontoEfectivo
            // 
            this.txtMontoEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.txtMontoEfectivo.Location = new System.Drawing.Point(400, 191);
            this.txtMontoEfectivo.Name = "txtMontoEfectivo";
            this.txtMontoEfectivo.ReadOnly = true;
            this.txtMontoEfectivo.Size = new System.Drawing.Size(86, 21);
            this.txtMontoEfectivo.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(275, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "GASTO DE VIAJE:";
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(87, 90);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(155, 105);
            this.lstPlaca.TabIndex = 130;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // lstCarreta
            // 
            this.lstCarreta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstCarreta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lstCarreta.ForeColor = System.Drawing.Color.Navy;
            this.lstCarreta.FullRowSelect = true;
            this.lstCarreta.GridLines = true;
            this.lstCarreta.Location = new System.Drawing.Point(331, 90);
            this.lstCarreta.MultiSelect = false;
            this.lstCarreta.Name = "lstCarreta";
            this.lstCarreta.Size = new System.Drawing.Size(155, 105);
            this.lstCarreta.TabIndex = 143;
            this.lstCarreta.UseCompatibleStateImageBehavior = false;
            this.lstCarreta.View = System.Windows.Forms.View.Details;
            this.lstCarreta.Visible = false;
            this.lstCarreta.Enter += new System.EventHandler(this.lstCarreta_Enter);
            this.lstCarreta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCarreta_KeyPress);
            this.lstCarreta.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstCarreta_MouseDoubleClick);
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(87, 50);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(399, 145);
            this.lstConductor.TabIndex = 131;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.DoubleClick += new System.EventHandler(this.lstConductor_DoubleClick);
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            // 
            // btnCancelar
            // 
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
            this.btnCancelar.Location = new System.Drawing.Point(158, 332);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 38);
            this.btnCancelar.TabIndex = 53;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnRegistrar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnRegistrar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnRegistrar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Appearance.Options.UseBackColor = true;
            this.btnRegistrar.Appearance.Options.UseBorderColor = true;
            this.btnRegistrar.Appearance.Options.UseFont = true;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.Image")));
            this.btnRegistrar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnRegistrar.Location = new System.Drawing.Point(288, 332);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(105, 38);
            this.btnRegistrar.TabIndex = 52;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.ToolTip = "Registrar";
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // frmGenerarPlanillaTolvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(540, 388);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "frmGenerarPlanillaTolvas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGenerarPlanillaTolvas";
            this.Load += new System.EventHandler(this.frmGenerarPlanillaTolvas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTracto;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMontoEfectivo;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnRegistrar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.ListView lstPlaca;
        private System.Windows.Forms.ListView lstConductor;
        private System.Windows.Forms.DateTimePicker dtpFechaViaje;
        private System.Windows.Forms.ComboBox cbxRuta;
        private System.Windows.Forms.TextBox txtTotalDias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbLindley;
        private System.Windows.Forms.CheckBox cbRedondear;
        private System.Windows.Forms.TextBox txtCarreta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lstCarreta;
    }
}