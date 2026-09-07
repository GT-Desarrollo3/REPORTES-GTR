namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmViaticoCambioRuta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViaticoCambioRuta));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupDireccionPartida = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRutaOriginal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGastoOriginal = new System.Windows.Forms.TextBox();
            this.lblPlanilla = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGastoAdicional = new System.Windows.Forms.TextBox();
            this.cbRedondeo = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNuevaRuta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNuevoGasto = new System.Windows.Forms.TextBox();
            this.btnGenerarAdicional = new DevExpress.XtraEditors.SimpleButton();
            this.cbxRuta = new System.Windows.Forms.ComboBox();
            this.groupDireccionPartida.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.lblTitulo.Size = new System.Drawing.Size(524, 42);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "ADICIONAR MONTO POR RUTA";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupDireccionPartida
            // 
            this.groupDireccionPartida.Controls.Add(this.label2);
            this.groupDireccionPartida.Controls.Add(this.txtRutaOriginal);
            this.groupDireccionPartida.Controls.Add(this.label1);
            this.groupDireccionPartida.Controls.Add(this.txtGastoOriginal);
            this.groupDireccionPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupDireccionPartida.Location = new System.Drawing.Point(17, 94);
            this.groupDireccionPartida.Name = "groupDireccionPartida";
            this.groupDireccionPartida.Size = new System.Drawing.Size(488, 114);
            this.groupDireccionPartida.TabIndex = 11;
            this.groupDireccionPartida.TabStop = false;
            this.groupDireccionPartida.Text = "GASTO DE RUTA ORIGINAL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ruta\r\nOriginal:";
            // 
            // txtRutaOriginal
            // 
            this.txtRutaOriginal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRutaOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaOriginal.Location = new System.Drawing.Point(71, 34);
            this.txtRutaOriginal.Multiline = true;
            this.txtRutaOriginal.Name = "txtRutaOriginal";
            this.txtRutaOriginal.ReadOnly = true;
            this.txtRutaOriginal.Size = new System.Drawing.Size(399, 21);
            this.txtRutaOriginal.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Gasto\r\nde Ruta:";
            // 
            // txtGastoOriginal
            // 
            this.txtGastoOriginal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGastoOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.txtGastoOriginal.Location = new System.Drawing.Point(71, 76);
            this.txtGastoOriginal.Name = "txtGastoOriginal";
            this.txtGastoOriginal.ReadOnly = true;
            this.txtGastoOriginal.Size = new System.Drawing.Size(92, 21);
            this.txtGastoOriginal.TabIndex = 5;
            // 
            // lblPlanilla
            // 
            this.lblPlanilla.AutoSize = true;
            this.lblPlanilla.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold);
            this.lblPlanilla.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblPlanilla.Location = new System.Drawing.Point(113, 56);
            this.lblPlanilla.Name = "lblPlanilla";
            this.lblPlanilla.Size = new System.Drawing.Size(0, 26);
            this.lblPlanilla.TabIndex = 105;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 21);
            this.label3.TabIndex = 104;
            this.label3.Text = "PLANILLA:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtGastoAdicional);
            this.groupBox1.Controls.Add(this.cbRedondeo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNuevaRuta);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNuevoGasto);
            this.groupBox1.Controls.Add(this.cbxRuta);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 119);
            this.groupBox1.TabIndex = 106;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NUEVO GASTO DE RUTA: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(298, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 34);
            this.label6.TabIndex = 163;
            this.label6.Text = "Gasto\r\nAdicional:";
            // 
            // txtGastoAdicional
            // 
            this.txtGastoAdicional.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGastoAdicional.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.txtGastoAdicional.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtGastoAdicional.Location = new System.Drawing.Point(383, 76);
            this.txtGastoAdicional.Name = "txtGastoAdicional";
            this.txtGastoAdicional.ReadOnly = true;
            this.txtGastoAdicional.Size = new System.Drawing.Size(87, 24);
            this.txtGastoAdicional.TabIndex = 162;
            // 
            // cbRedondeo
            // 
            this.cbRedondeo.AutoSize = true;
            this.cbRedondeo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRedondeo.ForeColor = System.Drawing.Color.Firebrick;
            this.cbRedondeo.Location = new System.Drawing.Point(178, 78);
            this.cbRedondeo.Name = "cbRedondeo";
            this.cbRedondeo.Size = new System.Drawing.Size(92, 19);
            this.cbRedondeo.TabIndex = 161;
            this.cbRedondeo.Text = "Redondeo";
            this.cbRedondeo.UseVisualStyleBackColor = true;
            this.cbRedondeo.CheckedChanged += new System.EventHandler(this.cbRedondeo_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "Nueva\r\nRuta:";
            // 
            // txtNuevaRuta
            // 
            this.txtNuevaRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNuevaRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNuevaRuta.Location = new System.Drawing.Point(71, 34);
            this.txtNuevaRuta.Multiline = true;
            this.txtNuevaRuta.Name = "txtNuevaRuta";
            this.txtNuevaRuta.ReadOnly = true;
            this.txtNuevaRuta.Size = new System.Drawing.Size(399, 21);
            this.txtNuevaRuta.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 30);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nuevo\r\nGasto:";
            // 
            // txtNuevoGasto
            // 
            this.txtNuevoGasto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNuevoGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNuevoGasto.Location = new System.Drawing.Point(71, 76);
            this.txtNuevoGasto.Name = "txtNuevoGasto";
            this.txtNuevoGasto.ReadOnly = true;
            this.txtNuevoGasto.Size = new System.Drawing.Size(95, 21);
            this.txtNuevoGasto.TabIndex = 5;
            // 
            // btnGenerarAdicional
            // 
            this.btnGenerarAdicional.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGenerarAdicional.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGenerarAdicional.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGenerarAdicional.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarAdicional.Appearance.Options.UseBackColor = true;
            this.btnGenerarAdicional.Appearance.Options.UseBorderColor = true;
            this.btnGenerarAdicional.Appearance.Options.UseFont = true;
            this.btnGenerarAdicional.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarAdicional.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarAdicional.Image")));
            this.btnGenerarAdicional.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGenerarAdicional.Location = new System.Drawing.Point(204, 358);
            this.btnGenerarAdicional.Name = "btnGenerarAdicional";
            this.btnGenerarAdicional.Size = new System.Drawing.Size(114, 50);
            this.btnGenerarAdicional.TabIndex = 210;
            this.btnGenerarAdicional.Tag = "5";
            this.btnGenerarAdicional.Text = "Adicionar\r\nGasto";
            this.btnGenerarAdicional.ToolTip = "Adicionar Gasto";
            this.btnGenerarAdicional.Click += new System.EventHandler(this.btnGenerarAdicional_Click);
            // 
            // cbxRuta
            // 
            this.cbxRuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxRuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.cbxRuta.FormattingEnabled = true;
            this.cbxRuta.Location = new System.Drawing.Point(71, 34);
            this.cbxRuta.Name = "cbxRuta";
            this.cbxRuta.Size = new System.Drawing.Size(399, 21);
            this.cbxRuta.TabIndex = 211;
            this.cbxRuta.SelectedIndexChanged += new System.EventHandler(this.cbxRuta_SelectedIndexChanged);
            this.cbxRuta.DropDownClosed += new System.EventHandler(this.cbxRuta_DropDownClosed);
            // 
            // frmViaticoCambioRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(524, 426);
            this.Controls.Add(this.btnGenerarAdicional);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblPlanilla);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupDireccionPartida);
            this.Controls.Add(this.lblTitulo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViaticoCambioRuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIÁTICO POR CAMBIO DE RUTA";
            this.Load += new System.EventHandler(this.frmViaticoCambioRuta_Load);
            this.groupDireccionPartida.ResumeLayout(false);
            this.groupDireccionPartida.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupDireccionPartida;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtRutaOriginal;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtGastoOriginal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtNuevaRuta;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtNuevoGasto;
        public System.Windows.Forms.CheckBox cbRedondeo;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtGastoAdicional;
        private DevExpress.XtraEditors.SimpleButton btnGenerarAdicional;
        public System.Windows.Forms.ComboBox cbxRuta;
        public System.Windows.Forms.Label lblPlanilla;
    }
}