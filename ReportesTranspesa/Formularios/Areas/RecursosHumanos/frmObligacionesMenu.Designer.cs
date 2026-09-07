namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmObligacionesMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObligacionesMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBuscarArchivo = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvMenuDetalle = new System.Windows.Forms.DataGridView();
            this.chkCTS = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1141, 65);
            this.label1.TabIndex = 2;
            this.label1.Text = "OBLIGACIONES MASIVAS DE ALMUERZOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCTS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscarArchivo);
            this.groupBox1.Controls.Add(this.txtRuta);
            this.groupBox1.Controls.Add(this.btnGenerar);
            this.groupBox1.Location = new System.Drawing.Point(25, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1092, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SELECCIONE UN ARCHIVO DE DESCUENTOS DE MENÚ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ruta del Archivo:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.cargardatos;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(1033, 10);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(96, 50);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "          Guardar       Excel";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnBuscarArchivo
            // 
            this.btnBuscarArchivo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarArchivo.Image")));
            this.btnBuscarArchivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarArchivo.Location = new System.Drawing.Point(23, 30);
            this.btnBuscarArchivo.Name = "btnBuscarArchivo";
            this.btnBuscarArchivo.Size = new System.Drawing.Size(97, 52);
            this.btnBuscarArchivo.TabIndex = 1;
            this.btnBuscarArchivo.Text = "             Buscar";
            this.btnBuscarArchivo.UseVisualStyleBackColor = true;
            this.btnBuscarArchivo.Click += new System.EventHandler(this.btnBuscarArchivo_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(145, 56);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(672, 20);
            this.txtRuta.TabIndex = 0;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerar.Image")));
            this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerar.Location = new System.Drawing.Point(857, 27);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(96, 52);
            this.btnGenerar.TabIndex = 2;
            this.btnGenerar.Text = "             Generar          Tabla";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "DETALLE DE OBLIGACIÓN:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvMenuDetalle);
            this.panel1.Location = new System.Drawing.Point(25, 232);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1092, 378);
            this.panel1.TabIndex = 8;
            // 
            // dgvMenuDetalle
            // 
            this.dgvMenuDetalle.AllowUserToAddRows = false;
            this.dgvMenuDetalle.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMenuDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMenuDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMenuDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMenuDetalle.Location = new System.Drawing.Point(0, 0);
            this.dgvMenuDetalle.Name = "dgvMenuDetalle";
            this.dgvMenuDetalle.RowHeadersVisible = false;
            this.dgvMenuDetalle.Size = new System.Drawing.Size(1092, 378);
            this.dgvMenuDetalle.TabIndex = 8;
            // 
            // chkCTS
            // 
            this.chkCTS.AutoSize = true;
            this.chkCTS.Location = new System.Drawing.Point(972, 45);
            this.chkCTS.Name = "chkCTS";
            this.chkCTS.Size = new System.Drawing.Size(83, 17);
            this.chkCTS.TabIndex = 7;
            this.chkCTS.Text = "esPagoCTS";
            this.chkCTS.UseVisualStyleBackColor = true;
            // 
            // frmObligacionesMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(1141, 635);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmObligacionesMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OBLIGACIONES MASIVAS DE ALMUERZOS";
            this.Load += new System.EventHandler(this.frmObligacionesMenu_Load);
            this.Shown += new System.EventHandler(this.frmObligacionesMenu_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenuDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnBuscarArchivo;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dgvMenuDetalle;
        private System.Windows.Forms.CheckBox chkCTS;
    }
}