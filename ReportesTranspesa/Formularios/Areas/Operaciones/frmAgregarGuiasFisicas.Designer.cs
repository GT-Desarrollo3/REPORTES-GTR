namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmAgregarGuiasFisicas
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCrear = new System.Windows.Forms.TabPage();
            this.tabAsignar = new System.Windows.Forms.TabPage();
            this.cbxCompania = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxSerie = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.dd = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.tabRegularizar = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCrear.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(848, 41);
            this.label2.TabIndex = 49;
            this.label2.Text = "AGREGAR GUIAS FISICAS TRANSPORTISTA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 522);
            this.panel1.TabIndex = 50;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCrear);
            this.tabControl1.Controls.Add(this.tabAsignar);
            this.tabControl1.Controls.Add(this.tabRegularizar);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(848, 522);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCrear
            // 
            this.tabCrear.Controls.Add(this.groupBox4);
            this.tabCrear.Controls.Add(this.groupBox3);
            this.tabCrear.Controls.Add(this.groupBox2);
            this.tabCrear.Controls.Add(this.groupBox1);
            this.tabCrear.Location = new System.Drawing.Point(4, 25);
            this.tabCrear.Name = "tabCrear";
            this.tabCrear.Padding = new System.Windows.Forms.Padding(3);
            this.tabCrear.Size = new System.Drawing.Size(840, 493);
            this.tabCrear.TabIndex = 0;
            this.tabCrear.Text = "Crear Guias";
            this.tabCrear.UseVisualStyleBackColor = true;
            // 
            // tabAsignar
            // 
            this.tabAsignar.Location = new System.Drawing.Point(4, 25);
            this.tabAsignar.Name = "tabAsignar";
            this.tabAsignar.Padding = new System.Windows.Forms.Padding(3);
            this.tabAsignar.Size = new System.Drawing.Size(840, 493);
            this.tabAsignar.TabIndex = 1;
            this.tabAsignar.Text = "Asignar Conductor";
            this.tabAsignar.UseVisualStyleBackColor = true;
            // 
            // cbxCompania
            // 
            this.cbxCompania.FormattingEnabled = true;
            this.cbxCompania.Items.AddRange(new object[] {
            "10000000"});
            this.cbxCompania.Location = new System.Drawing.Point(6, 21);
            this.cbxCompania.Name = "cbxCompania";
            this.cbxCompania.Size = new System.Drawing.Size(136, 24);
            this.cbxCompania.TabIndex = 0;
            this.cbxCompania.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxCompania);
            this.groupBox1.Location = new System.Drawing.Point(18, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 59);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compañia";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxSerie);
            this.groupBox2.Location = new System.Drawing.Point(176, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(113, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Serie";
            // 
            // cbxSerie
            // 
            this.cbxSerie.FormattingEnabled = true;
            this.cbxSerie.Items.AddRange(new object[] {
            "011",
            "016",
            "017"});
            this.cbxSerie.Location = new System.Drawing.Point(6, 21);
            this.cbxSerie.Name = "cbxSerie";
            this.cbxSerie.Size = new System.Drawing.Size(98, 24);
            this.cbxSerie.TabIndex = 0;
            this.cbxSerie.SelectedValueChanged += new System.EventHandler(this.cbxSerie_SelectedValueChanged);
            this.cbxSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxSerie_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dd);
            this.groupBox3.Controls.Add(this.txtHasta);
            this.groupBox3.Controls.Add(this.txtDesde);
            this.groupBox3.Location = new System.Drawing.Point(391, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 59);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Numero";
            // 
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(61, 23);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.Size = new System.Drawing.Size(77, 22);
            this.txtDesde.TabIndex = 0;
            // 
            // txtHasta
            // 
            this.txtHasta.Location = new System.Drawing.Point(199, 22);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(77, 22);
            this.txtHasta.TabIndex = 1;
            // 
            // dd
            // 
            this.dd.AutoSize = true;
            this.dd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dd.Location = new System.Drawing.Point(151, 26);
            this.dd.Name = "dd";
            this.dd.Size = new System.Drawing.Size(47, 16);
            this.dd.TabIndex = 4;
            this.dd.Text = "Hasta:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Desde:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCantidad);
            this.groupBox4.Location = new System.Drawing.Point(295, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(90, 59);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(6, 23);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(77, 22);
            this.txtCantidad.TabIndex = 6;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            // 
            // tabRegularizar
            // 
            this.tabRegularizar.Location = new System.Drawing.Point(4, 25);
            this.tabRegularizar.Name = "tabRegularizar";
            this.tabRegularizar.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegularizar.Size = new System.Drawing.Size(840, 493);
            this.tabRegularizar.TabIndex = 2;
            this.tabRegularizar.Text = "Regularizar";
            this.tabRegularizar.UseVisualStyleBackColor = true;
            // 
            // frmAgregarGuiasFisicas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 563);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "frmAgregarGuiasFisicas";
            this.Text = "Agregar Guias";
            this.Load += new System.EventHandler(this.frmAgregarGuiasFisicas_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabCrear.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCrear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxCompania;
        private System.Windows.Forms.TabPage tabAsignar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxSerie;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label dd;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TabPage tabRegularizar;
    }
}