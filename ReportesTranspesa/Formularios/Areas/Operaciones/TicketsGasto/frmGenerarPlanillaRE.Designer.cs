namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmGenerarPlanillaRE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerarPlanillaRE));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGastoRuta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConductorR = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupDireccionPartida = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPlanilla = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConductorP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPreviaje = new System.Windows.Forms.TextBox();
            this.btnGenerarPLR = new DevExpress.XtraEditors.SimpleButton();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.cbRedondear = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupDireccionPartida.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.MediumPurple;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(467, 39);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "GENERAR PLANILLA DE RECONOCIMIENTO";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRedondear);
            this.groupBox1.Controls.Add(this.txtGastoRuta);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtConductorR);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 198);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 124);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLANILLA DE RECONOCIMIENTO:";
            // 
            // txtGastoRuta
            // 
            this.txtGastoRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGastoRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGastoRuta.Location = new System.Drawing.Point(152, 83);
            this.txtGastoRuta.Name = "txtGastoRuta";
            this.txtGastoRuta.ReadOnly = true;
            this.txtGastoRuta.Size = new System.Drawing.Size(99, 21);
            this.txtGastoRuta.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Gasto Reconocimiento:";
            // 
            // txtConductorR
            // 
            this.txtConductorR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductorR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductorR.Location = new System.Drawing.Point(14, 48);
            this.txtConductorR.Name = "txtConductorR";
            this.txtConductorR.Size = new System.Drawing.Size(397, 20);
            this.txtConductorR.TabIndex = 7;
            this.txtConductorR.Enter += new System.EventHandler(this.txtConductorR_Enter);
            this.txtConductorR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductorR_KeyPress);
            this.txtConductorR.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtConductorR_KeyUp);
            this.txtConductorR.Leave += new System.EventHandler(this.txtConductorR_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Conductor de Apoyo:";
            // 
            // groupDireccionPartida
            // 
            this.groupDireccionPartida.Controls.Add(this.label4);
            this.groupDireccionPartida.Controls.Add(this.txtPlanilla);
            this.groupDireccionPartida.Controls.Add(this.label6);
            this.groupDireccionPartida.Controls.Add(this.txtRuta);
            this.groupDireccionPartida.Controls.Add(this.label2);
            this.groupDireccionPartida.Controls.Add(this.txtConductorP);
            this.groupDireccionPartida.Controls.Add(this.label1);
            this.groupDireccionPartida.Controls.Add(this.txtPreviaje);
            this.groupDireccionPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupDireccionPartida.Location = new System.Drawing.Point(17, 50);
            this.groupDireccionPartida.Name = "groupDireccionPartida";
            this.groupDireccionPartida.Size = new System.Drawing.Size(431, 133);
            this.groupDireccionPartida.TabIndex = 10;
            this.groupDireccionPartida.TabStop = false;
            this.groupDireccionPartida.Text = "DATOS DEL VIAJE PRINCIPAL:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(204, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Planilla:";
            // 
            // txtPlanilla
            // 
            this.txtPlanilla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlanilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanilla.Location = new System.Drawing.Point(261, 27);
            this.txtPlanilla.Name = "txtPlanilla";
            this.txtPlanilla.ReadOnly = true;
            this.txtPlanilla.Size = new System.Drawing.Size(83, 20);
            this.txtPlanilla.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ruta:";
            // 
            // txtRuta
            // 
            this.txtRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(83, 95);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(328, 20);
            this.txtRuta.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Conductor:";
            // 
            // txtConductorP
            // 
            this.txtConductorP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductorP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductorP.Location = new System.Drawing.Point(83, 61);
            this.txtConductorP.Name = "txtConductorP";
            this.txtConductorP.ReadOnly = true;
            this.txtConductorP.Size = new System.Drawing.Size(328, 20);
            this.txtConductorP.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Previaje:";
            // 
            // txtPreviaje
            // 
            this.txtPreviaje.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPreviaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviaje.Location = new System.Drawing.Point(83, 27);
            this.txtPreviaje.Name = "txtPreviaje";
            this.txtPreviaje.ReadOnly = true;
            this.txtPreviaje.Size = new System.Drawing.Size(83, 20);
            this.txtPreviaje.TabIndex = 5;
            // 
            // btnGenerarPLR
            // 
            this.btnGenerarPLR.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGenerarPLR.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGenerarPLR.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGenerarPLR.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarPLR.Appearance.Options.UseBackColor = true;
            this.btnGenerarPLR.Appearance.Options.UseBorderColor = true;
            this.btnGenerarPLR.Appearance.Options.UseFont = true;
            this.btnGenerarPLR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarPLR.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPLR.Image")));
            this.btnGenerarPLR.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGenerarPLR.Location = new System.Drawing.Point(178, 343);
            this.btnGenerarPLR.Name = "btnGenerarPLR";
            this.btnGenerarPLR.Size = new System.Drawing.Size(108, 46);
            this.btnGenerarPLR.TabIndex = 209;
            this.btnGenerarPLR.Tag = "5";
            this.btnGenerarPLR.Text = "Generar\r\nPlanilla";
            this.btnGenerarPLR.ToolTip = "Generar PLR";
            this.btnGenerarPLR.Click += new System.EventHandler(this.btnGenerarPLR_Click);
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(31, 265);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(397, 129);
            this.lstConductor.TabIndex = 210;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.DoubleClick += new System.EventHandler(this.lstConductor_DoubleClick);
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            // 
            // cbRedondear
            // 
            this.cbRedondear.AutoSize = true;
            this.cbRedondear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRedondear.Location = new System.Drawing.Point(272, 85);
            this.cbRedondear.Name = "cbRedondear";
            this.cbRedondear.Size = new System.Drawing.Size(97, 19);
            this.cbRedondear.TabIndex = 141;
            this.cbRedondear.Text = "Turno Noche";
            this.cbRedondear.UseVisualStyleBackColor = true;
            this.cbRedondear.CheckedChanged += new System.EventHandler(this.cbRedondear_CheckedChanged);
            // 
            // frmGenerarPlanillaRE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(467, 412);
            this.Controls.Add(this.btnGenerarPLR);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.groupDireccionPartida);
            this.Controls.Add(this.lstConductor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGenerarPlanillaRE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GENERAR PLANILLA RECONOCIMIENTO";
            this.Load += new System.EventHandler(this.frmGenerarPlanillaRE_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupDireccionPartida.ResumeLayout(false);
            this.groupDireccionPartida.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGenerarPLR;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtGastoRuta;
        public System.Windows.Forms.TextBox txtConductorR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupDireccionPartida;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtConductorP;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPreviaje;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ListView lstConductor;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPlanilla;
        public System.Windows.Forms.Label lblTitulo;
        public System.Windows.Forms.CheckBox cbRedondear;
        public System.Windows.Forms.Label label5;
    }
}