namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmGenerarPLE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerarPLE));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerarPLE = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRedondeo = new System.Windows.Forms.CheckBox();
            this.txtGastoRuta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConductorN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupDireccionPartida = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConductorA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlanilla = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupDireccionPartida.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(614, 42);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "REGISTRO DE PLANILLAS DE EVENTO";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.btnGenerarPLE);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupDireccionPartida);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 251);
            this.panel1.TabIndex = 3;
            // 
            // btnGenerarPLE
            // 
            this.btnGenerarPLE.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGenerarPLE.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGenerarPLE.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGenerarPLE.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarPLE.Appearance.Options.UseBackColor = true;
            this.btnGenerarPLE.Appearance.Options.UseBorderColor = true;
            this.btnGenerarPLE.Appearance.Options.UseFont = true;
            this.btnGenerarPLE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarPLE.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPLE.Image")));
            this.btnGenerarPLE.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnGenerarPLE.Location = new System.Drawing.Point(494, 125);
            this.btnGenerarPLE.Name = "btnGenerarPLE";
            this.btnGenerarPLE.Size = new System.Drawing.Size(73, 79);
            this.btnGenerarPLE.TabIndex = 209;
            this.btnGenerarPLE.Tag = "5";
            this.btnGenerarPLE.Text = "Generar\r\nPLE";
            this.btnGenerarPLE.ToolTip = "Generar PLE";
            this.btnGenerarPLE.Click += new System.EventHandler(this.btnGenerarPLE_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRedondeo);
            this.groupBox1.Controls.Add(this.txtGastoRuta);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtRuta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtConductorN);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 145);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DEL NUEVO VIAJE:";
            // 
            // cbRedondeo
            // 
            this.cbRedondeo.AutoSize = true;
            this.cbRedondeo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRedondeo.ForeColor = System.Drawing.Color.Firebrick;
            this.cbRedondeo.Location = new System.Drawing.Point(205, 109);
            this.cbRedondeo.Name = "cbRedondeo";
            this.cbRedondeo.Size = new System.Drawing.Size(92, 19);
            this.cbRedondeo.TabIndex = 160;
            this.cbRedondeo.Text = "Redondeo";
            this.cbRedondeo.UseVisualStyleBackColor = true;
            this.cbRedondeo.CheckedChanged += new System.EventHandler(this.cbRedondeo_CheckedChanged);
            // 
            // txtGastoRuta
            // 
            this.txtGastoRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGastoRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGastoRuta.Location = new System.Drawing.Point(83, 108);
            this.txtGastoRuta.Name = "txtGastoRuta";
            this.txtGastoRuta.ReadOnly = true;
            this.txtGastoRuta.Size = new System.Drawing.Size(104, 20);
            this.txtGastoRuta.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 30);
            this.label5.TabIndex = 12;
            this.label5.Text = "Gasto\r\nde Ruta:";
            // 
            // txtRuta
            // 
            this.txtRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(83, 70);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(328, 20);
            this.txtRuta.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ruta:";
            // 
            // txtConductorN
            // 
            this.txtConductorN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductorN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductorN.Location = new System.Drawing.Point(83, 31);
            this.txtConductorN.Name = "txtConductorN";
            this.txtConductorN.ReadOnly = true;
            this.txtConductorN.Size = new System.Drawing.Size(328, 20);
            this.txtConductorN.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 30);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nuevo\r\nConductor:";
            // 
            // groupDireccionPartida
            // 
            this.groupDireccionPartida.Controls.Add(this.label2);
            this.groupDireccionPartida.Controls.Add(this.txtConductorA);
            this.groupDireccionPartida.Controls.Add(this.label1);
            this.groupDireccionPartida.Controls.Add(this.txtPlanilla);
            this.groupDireccionPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupDireccionPartida.Location = new System.Drawing.Point(13, 13);
            this.groupDireccionPartida.Name = "groupDireccionPartida";
            this.groupDireccionPartida.Size = new System.Drawing.Size(588, 64);
            this.groupDireccionPartida.TabIndex = 10;
            this.groupDireccionPartida.TabStop = false;
            this.groupDireccionPartida.Text = "DATOS DE VIAJE ORIGINAL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(170, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Conductor:";
            // 
            // txtConductorA
            // 
            this.txtConductorA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductorA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductorA.Location = new System.Drawing.Point(242, 27);
            this.txtConductorA.Name = "txtConductorA";
            this.txtConductorA.ReadOnly = true;
            this.txtConductorA.Size = new System.Drawing.Size(328, 20);
            this.txtConductorA.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Planilla:";
            // 
            // txtPlanilla
            // 
            this.txtPlanilla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlanilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanilla.Location = new System.Drawing.Point(68, 27);
            this.txtPlanilla.Name = "txtPlanilla";
            this.txtPlanilla.ReadOnly = true;
            this.txtPlanilla.Size = new System.Drawing.Size(83, 20);
            this.txtPlanilla.TabIndex = 5;
            // 
            // frmGenerarPLE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 293);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitulo);
            this.Name = "frmGenerarPLE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Planilla Evento";
            this.Load += new System.EventHandler(this.frmGenerarPLE_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupDireccionPartida.ResumeLayout(false);
            this.groupDireccionPartida.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupDireccionPartida;
        public System.Windows.Forms.TextBox txtPlanilla;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtConductorA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtConductorN;
        public System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtGastoRuta;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox cbRedondeo;
        private DevExpress.XtraEditors.SimpleButton btnGenerarPLE;
    }
}