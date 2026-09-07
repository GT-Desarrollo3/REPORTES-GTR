namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    partial class frmSistemaDeColasAlmacen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSistemaDeColasAlmacen));
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCorreos = new System.Windows.Forms.ToolStripButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvListar = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imprimirTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atendidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pesajeFinalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.contadorEspera = new System.Windows.Forms.Label();
            this.contadorIngreso = new System.Windows.Forms.Label();
            this.contadorSalida = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 17F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1189, 50);
            this.label2.TabIndex = 18;
            this.label2.Text = "CONTROL DE COLAS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.contadorSalida);
            this.panel1.Controls.Add(this.contadorIngreso);
            this.panel1.Controls.Add(this.contadorEspera);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.dtpFechaFin);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpFechaInicio);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1189, 90);
            this.panel1.TabIndex = 21;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(1056, 36);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(95, 44);
            this.simpleButton2.TabIndex = 29;
            this.simpleButton2.Text = "EXPORTAR";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(515, 36);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(95, 44);
            this.btnBuscar.TabIndex = 28;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCorreos});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1189, 25);
            this.toolStrip1.TabIndex = 27;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCorreos
            // 
            this.btnCorreos.Image = global::ReportesTranspesa.Properties.Resources.bonoseguridadicono;
            this.btnCorreos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCorreos.Name = "btnCorreos";
            this.btnCorreos.Size = new System.Drawing.Size(140, 22);
            this.btnCorreos.Text = "Lista Correos Clientes";
            this.btnCorreos.Click += new System.EventHandler(this.btnCorreos_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(12, 36);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(95, 44);
            this.simpleButton1.TabIndex = 25;
            this.simpleButton1.Text = "NUEVO";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(387, 51);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaFin.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Fecha Fin:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(196, 51);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaInicio.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Fecha Inicio:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvListar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1189, 545);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Ingresos y Salidas";
            // 
            // dgvListar
            // 
            this.dgvListar.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvListar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListar.Location = new System.Drawing.Point(3, 16);
            this.dgvListar.MainView = this.gridView1;
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.Size = new System.Drawing.Size(1183, 526);
            this.dgvListar.TabIndex = 0;
            this.dgvListar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirTicketToolStripMenuItem,
            this.atendidoToolStripMenuItem,
            this.pesajeFinalToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 70);
            // 
            // imprimirTicketToolStripMenuItem
            // 
            this.imprimirTicketToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.Codigo_QR;
            this.imprimirTicketToolStripMenuItem.Name = "imprimirTicketToolStripMenuItem";
            this.imprimirTicketToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.imprimirTicketToolStripMenuItem.Text = "Imprimir Ticket";
            this.imprimirTicketToolStripMenuItem.Click += new System.EventHandler(this.imprimirTicketToolStripMenuItem_Click);
            // 
            // atendidoToolStripMenuItem
            // 
            this.atendidoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.updated1;
            this.atendidoToolStripMenuItem.Name = "atendidoToolStripMenuItem";
            this.atendidoToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.atendidoToolStripMenuItem.Text = "Pesaje Entrada";
            this.atendidoToolStripMenuItem.Click += new System.EventHandler(this.atendidoToolStripMenuItem_Click);
            // 
            // pesajeFinalToolStripMenuItem
            // 
            this.pesajeFinalToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.filtrar;
            this.pesajeFinalToolStripMenuItem.Name = "pesajeFinalToolStripMenuItem";
            this.pesajeFinalToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.pesajeFinalToolStripMenuItem.Text = "Pesaje Final";
            this.pesajeFinalToolStripMenuItem.Click += new System.EventHandler(this.pesajeFinalToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dgvListar;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(632, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "EN ESPERA:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(762, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "INGRESO:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(882, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "SALIDA:";
            // 
            // contadorEspera
            // 
            this.contadorEspera.AutoSize = true;
            this.contadorEspera.BackColor = System.Drawing.Color.OrangeRed;
            this.contadorEspera.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contadorEspera.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.contadorEspera.Location = new System.Drawing.Point(702, 43);
            this.contadorEspera.Name = "contadorEspera";
            this.contadorEspera.Size = new System.Drawing.Size(46, 31);
            this.contadorEspera.TabIndex = 33;
            this.contadorEspera.Text = "20";
            // 
            // contadorIngreso
            // 
            this.contadorIngreso.AutoSize = true;
            this.contadorIngreso.BackColor = System.Drawing.Color.YellowGreen;
            this.contadorIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contadorIngreso.Location = new System.Drawing.Point(819, 43);
            this.contadorIngreso.Name = "contadorIngreso";
            this.contadorIngreso.Size = new System.Drawing.Size(46, 31);
            this.contadorIngreso.TabIndex = 34;
            this.contadorIngreso.Text = "20";
            // 
            // contadorSalida
            // 
            this.contadorSalida.AutoSize = true;
            this.contadorSalida.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.contadorSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contadorSalida.Location = new System.Drawing.Point(929, 43);
            this.contadorSalida.Name = "contadorSalida";
            this.contadorSalida.Size = new System.Drawing.Size(46, 31);
            this.contadorSalida.TabIndex = 35;
            this.contadorSalida.Text = "20";
            // 
            // frmSistemaDeColasAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(1189, 685);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "frmSistemaDeColasAlmacen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSistemaDeColasAlmacen";
            this.Load += new System.EventHandler(this.frmSistemaDeColasAlmacen_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl dgvListar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem imprimirTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atendidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pesajeFinalToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnCorreos;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Label contadorSalida;
        private System.Windows.Forms.Label contadorIngreso;
        private System.Windows.Forms.Label contadorEspera;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}