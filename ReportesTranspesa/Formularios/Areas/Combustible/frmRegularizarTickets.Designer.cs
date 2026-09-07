namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    partial class frmRegularizarTickets
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.grbCodigoPreviaje = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigoPreviaje = new System.Windows.Forms.TextBox();
            this.grbPlaca = new System.Windows.Forms.GroupBox();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOdometro = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.grbCodigoPreviaje.SuspendLayout();
            this.grbPlaca.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(38, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro. Ticket Despacho:";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(202, 21);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.ReadOnly = true;
            this.txtTicket.Size = new System.Drawing.Size(191, 20);
            this.txtTicket.TabIndex = 1;
            // 
            // grbCodigoPreviaje
            // 
            this.grbCodigoPreviaje.Controls.Add(this.label2);
            this.grbCodigoPreviaje.Controls.Add(this.txtCodigoPreviaje);
            this.grbCodigoPreviaje.Enabled = false;
            this.grbCodigoPreviaje.Location = new System.Drawing.Point(35, 83);
            this.grbCodigoPreviaje.Name = "grbCodigoPreviaje";
            this.grbCodigoPreviaje.Size = new System.Drawing.Size(456, 67);
            this.grbCodigoPreviaje.TabIndex = 2;
            this.grbCodigoPreviaje.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Codigo Correcto:";
            // 
            // txtCodigoPreviaje
            // 
            this.txtCodigoPreviaje.Location = new System.Drawing.Point(110, 33);
            this.txtCodigoPreviaje.Name = "txtCodigoPreviaje";
            this.txtCodigoPreviaje.Size = new System.Drawing.Size(164, 20);
            this.txtCodigoPreviaje.TabIndex = 1;
            // 
            // grbPlaca
            // 
            this.grbPlaca.Controls.Add(this.lstPlaca);
            this.grbPlaca.Controls.Add(this.txtDni);
            this.grbPlaca.Controls.Add(this.label6);
            this.grbPlaca.Controls.Add(this.txtConductor);
            this.grbPlaca.Controls.Add(this.label5);
            this.grbPlaca.Controls.Add(this.txtOdometro);
            this.grbPlaca.Controls.Add(this.label4);
            this.grbPlaca.Controls.Add(this.txtPlaca);
            this.grbPlaca.Controls.Add(this.label3);
            this.grbPlaca.Enabled = false;
            this.grbPlaca.Location = new System.Drawing.Point(35, 174);
            this.grbPlaca.Name = "grbPlaca";
            this.grbPlaca.Size = new System.Drawing.Size(456, 163);
            this.grbPlaca.TabIndex = 3;
            this.grbPlaca.TabStop = false;
            // 
            // lstPlaca
            // 
            this.lstPlaca.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPlaca.BackColor = System.Drawing.Color.PaleGreen;
            this.lstPlaca.ForeColor = System.Drawing.Color.Blue;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(101, 47);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(128, 0);
            this.lstPlaca.TabIndex = 61;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(101, 131);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(128, 20);
            this.txtDni.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Dni:";
            // 
            // txtConductor
            // 
            this.txtConductor.Location = new System.Drawing.Point(101, 96);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(337, 20);
            this.txtConductor.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Conductor:";
            // 
            // txtOdometro
            // 
            this.txtOdometro.Location = new System.Drawing.Point(101, 63);
            this.txtOdometro.Name = "txtOdometro";
            this.txtOdometro.Size = new System.Drawing.Size(128, 20);
            this.txtOdometro.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Odometro:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(101, 26);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(128, 20);
            this.txtPlaca.TabIndex = 3;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Placa Correcta:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(35, 82);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(139, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Error de Codigo Previaje";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(35, 171);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(92, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Error de Placa";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(92, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 16);
            this.label7.TabIndex = 6;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.Location = new System.Drawing.Point(370, 414);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(89, 40);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // button1
            // 
            this.button1.Image = global::ReportesTranspesa.Properties.Resources.salida;
            this.button1.Location = new System.Drawing.Point(179, 414);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Salir";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(35, 355);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(152, 17);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Cantidad  de Despacho (0)";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // frmRegularizarTickets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(516, 466);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.grbPlaca);
            this.Controls.Add(this.grbCodigoPreviaje);
            this.Controls.Add(this.txtTicket);
            this.Controls.Add(this.label1);
            this.Name = "frmRegularizarTickets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Regularizar Tickets";
            this.Load += new System.EventHandler(this.frmRegularizarTickets_Load);
            this.grbCodigoPreviaje.ResumeLayout(false);
            this.grbCodigoPreviaje.PerformLayout();
            this.grbPlaca.ResumeLayout(false);
            this.grbPlaca.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbCodigoPreviaje;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox grbPlaca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigoPreviaje;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOdometro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ListView lstPlaca;
        public System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}