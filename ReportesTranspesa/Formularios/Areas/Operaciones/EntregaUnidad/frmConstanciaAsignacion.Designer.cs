namespace ReportesTranspesa.Formularios.Areas.Operaciones.EntregaUnidad
{
    partial class frmConstanciaAsignacion
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxObservacion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNConductor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPreviaje = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConductorViaje = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPlacaViaje = new System.Windows.Forms.TextBox();
            this.lstPlacas = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMotivo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbxObservacion);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNConductor);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox2.Location = new System.Drawing.Point(18, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 183);
            this.groupBox2.TabIndex = 211;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS DE CONSTANCIA: ";
            // 
            // cbxObservacion
            // 
            this.cbxObservacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxObservacion.FormattingEnabled = true;
            this.cbxObservacion.Location = new System.Drawing.Point(93, 70);
            this.cbxObservacion.Name = "cbxObservacion";
            this.cbxObservacion.Size = new System.Drawing.Size(271, 23);
            this.cbxObservacion.TabIndex = 184;
            this.cbxObservacion.SelectedIndexChanged += new System.EventHandler(this.cbxObservacion_SelectedIndexChanged);
            this.cbxObservacion.DropDownClosed += new System.EventHandler(this.cbxObservacion_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Motivo:\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 30);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nuevo\r\nconductor:";
            // 
            // txtNConductor
            // 
            this.txtNConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNConductor.Location = new System.Drawing.Point(93, 32);
            this.txtNConductor.Name = "txtNConductor";
            this.txtNConductor.Size = new System.Drawing.Size(353, 21);
            this.txtNConductor.TabIndex = 120;
            this.txtNConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNConductor_KeyPress);
            this.txtNConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNConductor_KeyUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(502, 39);
            this.label1.TabIndex = 210;
            this.label1.Text = "CONSTANCIA DE ASIGNACIÓN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(111, 227);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(356, 114);
            this.lstConductor.TabIndex = 214;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            this.lstConductor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstConductor_MouseDoubleClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Navy;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(122, 373);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 42);
            this.btnCancelar.TabIndex = 213;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Navy;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAgregar.Location = new System.Drawing.Point(265, 373);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(117, 42);
            this.btnAgregar.TabIndex = 212;
            this.btnAgregar.Text = " Registrar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPreviaje);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtConductorViaje);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPlacaViaje);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox1.Location = new System.Drawing.Point(16, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 107);
            this.groupBox1.TabIndex = 215;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DE VIAJE ATENDIDO: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 30);
            this.label4.TabIndex = 112;
            this.label4.Text = "Última\r\nAsignación:";
            // 
            // txtPreviaje
            // 
            this.txtPreviaje.BackColor = System.Drawing.SystemColors.Control;
            this.txtPreviaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviaje.Location = new System.Drawing.Point(327, 28);
            this.txtPreviaje.Name = "txtPreviaje";
            this.txtPreviaje.ReadOnly = true;
            this.txtPreviaje.Size = new System.Drawing.Size(121, 21);
            this.txtPreviaje.TabIndex = 111;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 30);
            this.label6.TabIndex = 1;
            this.label6.Text = "Último\r\nconductor:";
            // 
            // txtConductorViaje
            // 
            this.txtConductorViaje.BackColor = System.Drawing.SystemColors.Control;
            this.txtConductorViaje.Location = new System.Drawing.Point(90, 66);
            this.txtConductorViaje.Name = "txtConductorViaje";
            this.txtConductorViaje.ReadOnly = true;
            this.txtConductorViaje.Size = new System.Drawing.Size(358, 21);
            this.txtConductorViaje.TabIndex = 110;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Tracto:\r\n";
            // 
            // txtPlacaViaje
            // 
            this.txtPlacaViaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPlacaViaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlacaViaje.Location = new System.Drawing.Point(90, 28);
            this.txtPlacaViaje.Name = "txtPlacaViaje";
            this.txtPlacaViaje.Size = new System.Drawing.Size(125, 21);
            this.txtPlacaViaje.TabIndex = 100;
            this.txtPlacaViaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlacaViaje_KeyPress);
            this.txtPlacaViaje.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlacaViaje_KeyUp);
            // 
            // lstPlacas
            // 
            this.lstPlacas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlacas.ForeColor = System.Drawing.Color.Navy;
            this.lstPlacas.FullRowSelect = true;
            this.lstPlacas.GridLines = true;
            this.lstPlacas.Location = new System.Drawing.Point(106, 101);
            this.lstPlacas.MultiSelect = false;
            this.lstPlacas.Name = "lstPlacas";
            this.lstPlacas.Size = new System.Drawing.Size(273, 122);
            this.lstPlacas.TabIndex = 215;
            this.lstPlacas.UseCompatibleStateImageBehavior = false;
            this.lstPlacas.View = System.Windows.Forms.View.Details;
            this.lstPlacas.Visible = false;
            this.lstPlacas.Enter += new System.EventHandler(this.lstPlacas_Enter);
            this.lstPlacas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlacas_KeyPress);
            this.lstPlacas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlacas_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 30);
            this.label7.TabIndex = 185;
            this.label7.Text = "Especificar\r\nmotivo:\r\n";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(93, 110);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(353, 52);
            this.txtMotivo.TabIndex = 186;
            // 
            // frmConstanciaAsignacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(502, 431);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPlacas);
            this.Controls.Add(this.lstConductor);
            this.MaximizeBox = false;
            this.Name = "frmConstanciaAsignacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONSTANCIA DE ASIGNACIÓN";
            this.Load += new System.EventHandler(this.frmConstanciaAsignacion_Load);
            this.Shown += new System.EventHandler(this.frmConstanciaAsignacion_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtNConductor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstConductor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtPlacaViaje;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtConductorViaje;
        private System.Windows.Forms.ListView lstPlacas;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPreviaje;
        public System.Windows.Forms.ComboBox cbxObservacion;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label7;
    }
}