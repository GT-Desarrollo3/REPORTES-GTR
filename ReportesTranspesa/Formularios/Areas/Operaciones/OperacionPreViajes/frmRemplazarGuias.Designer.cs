namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmRemplazarGuias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRemplazarGuias));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemplazar = new DevExpress.XtraEditors.SimpleButton();
            this.txtViaje = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgGuias = new DevExpress.XtraGrid.GridControl();
            this.dgvGuiasVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemitente = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGuiaOtros = new System.Windows.Forms.TextBox();
            this.cbxDireccionDestino = new System.Windows.Forms.ComboBox();
            this.cbxDireccionPartida = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGuias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuiasVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 559);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxDireccionDestino);
            this.groupBox1.Controls.Add(this.cbxDireccionPartida);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtGuiaOtros);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtRemitente);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNumero);
            this.groupBox1.Controls.Add(this.dtgGuias);
            this.groupBox1.Controls.Add(this.btnRemplazar);
            this.groupBox1.Controls.Add(this.txtViaje);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSerie);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 497);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion";
            // 
            // btnRemplazar
            // 
            this.btnRemplazar.Enabled = false;
            this.btnRemplazar.Image = ((System.Drawing.Image)(resources.GetObject("btnRemplazar.Image")));
            this.btnRemplazar.Location = new System.Drawing.Point(182, 442);
            this.btnRemplazar.Name = "btnRemplazar";
            this.btnRemplazar.Size = new System.Drawing.Size(129, 45);
            this.btnRemplazar.TabIndex = 7;
            this.btnRemplazar.Text = "REMPLAZAR";
            this.btnRemplazar.Click += new System.EventHandler(this.btnRemplazar_Click);
            // 
            // txtViaje
            // 
            this.txtViaje.Location = new System.Drawing.Point(65, 52);
            this.txtViaje.Name = "txtViaje";
            this.txtViaje.ReadOnly = true;
            this.txtViaje.Size = new System.Drawing.Size(94, 24);
            this.txtViaje.TabIndex = 120;
            this.txtViaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 121;
            this.label4.Text = "Viajes";
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(45, 386);
            this.txtSerie.MaxLength = 3;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(54, 24);
            this.txtSerie.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 367);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Guia Transportista";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Guias Actuales";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(485, 62);
            this.label1.TabIndex = 1;
            this.label1.Text = "REMPLAZAR GUIAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgGuias
            // 
            this.dtgGuias.Location = new System.Drawing.Point(12, 118);
            this.dtgGuias.MainView = this.dgvGuiasVista;
            this.dtgGuias.Name = "dtgGuias";
            this.dtgGuias.Size = new System.Drawing.Size(440, 142);
            this.dtgGuias.TabIndex = 103;
            this.dtgGuias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvGuiasVista,
            this.gridView1});
            // 
            // dgvGuiasVista
            // 
            this.dgvGuiasVista.GridControl = this.dtgGuias;
            this.dgvGuiasVista.Name = "dgvGuiasVista";
            this.dgvGuiasVista.OptionsBehavior.Editable = false;
            this.dgvGuiasVista.OptionsView.ColumnAutoWidth = false;
            this.dgvGuiasVista.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.dgvGuiasVista.OptionsView.RowAutoHeight = true;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgGuias;
            this.gridView1.Name = "gridView1";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(117, 386);
            this.txtNumero.MaxLength = 7;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(74, 24);
            this.txtNumero.TabIndex = 4;
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero_KeyPress);
            this.txtNumero.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNumero_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 16);
            this.label5.TabIndex = 105;
            this.label5.Text = "-";
            // 
            // txtRemitente
            // 
            this.txtRemitente.Location = new System.Drawing.Point(209, 386);
            this.txtRemitente.Name = "txtRemitente";
            this.txtRemitente.ReadOnly = true;
            this.txtRemitente.Size = new System.Drawing.Size(116, 24);
            this.txtRemitente.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(209, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 107;
            this.label6.Text = "Guia Remitente";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(351, 367);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 109;
            this.label7.Text = "Guia Otros";
            // 
            // txtGuiaOtros
            // 
            this.txtGuiaOtros.Location = new System.Drawing.Point(351, 386);
            this.txtGuiaOtros.Name = "txtGuiaOtros";
            this.txtGuiaOtros.ReadOnly = true;
            this.txtGuiaOtros.Size = new System.Drawing.Size(101, 24);
            this.txtGuiaOtros.TabIndex = 6;
            // 
            // cbxDireccionDestino
            // 
            this.cbxDireccionDestino.FormattingEnabled = true;
            this.cbxDireccionDestino.Location = new System.Drawing.Point(118, 321);
            this.cbxDireccionDestino.Name = "cbxDireccionDestino";
            this.cbxDireccionDestino.Size = new System.Drawing.Size(290, 24);
            this.cbxDireccionDestino.TabIndex = 113;
            // 
            // cbxDireccionPartida
            // 
            this.cbxDireccionPartida.FormattingEnabled = true;
            this.cbxDireccionPartida.Location = new System.Drawing.Point(118, 292);
            this.cbxDireccionPartida.Name = "cbxDireccionPartida";
            this.cbxDireccionPartida.Size = new System.Drawing.Size(290, 24);
            this.cbxDireccionPartida.TabIndex = 112;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(40, 323);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 111;
            this.label10.Text = "Dir Destino:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 297);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 16);
            this.label9.TabIndex = 110;
            this.label9.Text = "Dir Partida:";
            // 
            // frmRemplazarGuias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(485, 559);
            this.Controls.Add(this.panel1);
            this.Name = "frmRemplazarGuias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRemplazarGuias";
            this.Load += new System.EventHandler(this.frmRemplazarGuias_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGuias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuiasVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtViaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnRemplazar;
        private DevExpress.XtraGrid.GridControl dtgGuias;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvGuiasVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemitente;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGuiaOtros;
        private System.Windows.Forms.ComboBox cbxDireccionDestino;
        private System.Windows.Forms.ComboBox cbxDireccionPartida;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}