namespace ReportesTranspesa.Sistema
{
    partial class AgendarReuniones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgendarReuniones));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbxSolicitud = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxOficina = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSala = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dgvReuniones = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aPROBARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dESAPROBARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fINALIZARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNuevaReunion = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReuniones)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1202, 49);
            this.label1.TabIndex = 8;
            this.label1.Text = "Agendar Reunion";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.btnNuevaReunion);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1202, 90);
            this.panel3.TabIndex = 9;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbxSolicitud);
            this.groupBox5.Location = new System.Drawing.Point(780, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 47);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Estado Solicitud";
            // 
            // cbxSolicitud
            // 
            this.cbxSolicitud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSolicitud.FormattingEnabled = true;
            this.cbxSolicitud.Items.AddRange(new object[] {
            "",
            "AUTORIZADO",
            "RECHAZADO",
            "PENDIENTE"});
            this.cbxSolicitud.Location = new System.Drawing.Point(6, 19);
            this.cbxSolicitud.Name = "cbxSolicitud";
            this.cbxSolicitud.Size = new System.Drawing.Size(158, 21);
            this.cbxSolicitud.TabIndex = 8;
            this.cbxSolicitud.SelectedIndexChanged += new System.EventHandler(this.cbxSolicitud_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxOficina);
            this.groupBox2.Location = new System.Drawing.Point(12, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 49);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Oficina";
            // 
            // cbxOficina
            // 
            this.cbxOficina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOficina.FormattingEnabled = true;
            this.cbxOficina.Location = new System.Drawing.Point(12, 19);
            this.cbxOficina.Name = "cbxOficina";
            this.cbxOficina.Size = new System.Drawing.Size(205, 21);
            this.cbxOficina.TabIndex = 5;
            this.cbxOficina.SelectedIndexChanged += new System.EventHandler(this.cbxOficina_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxSala);
            this.groupBox1.Location = new System.Drawing.Point(253, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 49);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sala Nr°";
            // 
            // cbxSala
            // 
            this.cbxSala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSala.FormattingEnabled = true;
            this.cbxSala.Location = new System.Drawing.Point(15, 19);
            this.cbxSala.Name = "cbxSala";
            this.cbxSala.Size = new System.Drawing.Size(205, 21);
            this.cbxSala.TabIndex = 5;
            this.cbxSala.SelectedIndexChanged += new System.EventHandler(this.cbxSala_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpFechaFin);
            this.groupBox4.Location = new System.Drawing.Point(647, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(122, 47);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fecha Fin";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(6, 19);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaFin.TabIndex = 1;
            this.dtpFechaFin.ValueChanged += new System.EventHandler(this.dtpFechaFin_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFechaIni);
            this.groupBox3.Location = new System.Drawing.Point(494, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(143, 47);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fecha Inicio";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(22, 19);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaIni.TabIndex = 0;
            this.dtpFechaIni.ValueChanged += new System.EventHandler(this.dtpFechaIni_ValueChanged);
            // 
            // dgvReuniones
            // 
            this.dgvReuniones.AllowUserToAddRows = false;
            this.dgvReuniones.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReuniones.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvReuniones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvReuniones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReuniones.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvReuniones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReuniones.Location = new System.Drawing.Point(0, 139);
            this.dgvReuniones.Name = "dgvReuniones";
            this.dgvReuniones.ReadOnly = true;
            this.dgvReuniones.RowHeadersVisible = false;
            this.dgvReuniones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReuniones.Size = new System.Drawing.Size(1202, 358);
            this.dgvReuniones.TabIndex = 10;
            this.dgvReuniones.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvReuniones_CellFormatting);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aPROBARToolStripMenuItem,
            this.dESAPROBARToolStripMenuItem,
            this.fINALIZARToolStripMenuItem,
            this.editarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 92);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // aPROBARToolStripMenuItem
            // 
            this.aPROBARToolStripMenuItem.Enabled = false;
            this.aPROBARToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.mensajero;
            this.aPROBARToolStripMenuItem.Name = "aPROBARToolStripMenuItem";
            this.aPROBARToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.aPROBARToolStripMenuItem.Text = "AUTORIZAR";
            this.aPROBARToolStripMenuItem.Click += new System.EventHandler(this.aPROBARToolStripMenuItem_Click);
            // 
            // dESAPROBARToolStripMenuItem
            // 
            this.dESAPROBARToolStripMenuItem.Enabled = false;
            this.dESAPROBARToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.dESAPROBARToolStripMenuItem.Name = "dESAPROBARToolStripMenuItem";
            this.dESAPROBARToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dESAPROBARToolStripMenuItem.Text = "RECHAZADO";
            this.dESAPROBARToolStripMenuItem.Click += new System.EventHandler(this.dESAPROBARToolStripMenuItem_Click);
            // 
            // fINALIZARToolStripMenuItem
            // 
            this.fINALIZARToolStripMenuItem.Enabled = false;
            this.fINALIZARToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.liberar;
            this.fINALIZARToolStripMenuItem.Name = "fINALIZARToolStripMenuItem";
            this.fINALIZARToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fINALIZARToolStripMenuItem.Text = "FINALIZAR";
            this.fINALIZARToolStripMenuItem.Click += new System.EventHandler(this.fINALIZARToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.editarToolStripMenuItem.Text = "EDITAR";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // btnNuevaReunion
            // 
            this.btnNuevaReunion.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnNuevaReunion.Location = new System.Drawing.Point(1025, 27);
            this.btnNuevaReunion.Name = "btnNuevaReunion";
            this.btnNuevaReunion.Size = new System.Drawing.Size(90, 39);
            this.btnNuevaReunion.TabIndex = 4;
            this.btnNuevaReunion.Text = "Nueva Reunion";
            this.btnNuevaReunion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevaReunion.UseVisualStyleBackColor = true;
            this.btnNuevaReunion.Click += new System.EventHandler(this.btnNuevaReunion_Click);
            // 
            // AgendarReuniones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 497);
            this.Controls.Add(this.dgvReuniones);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AgendarReuniones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgendarReuniones";
            this.Load += new System.EventHandler(this.AgendarReuniones_Load);
            this.panel3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReuniones)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnNuevaReunion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        public System.Windows.Forms.DataGridView dgvReuniones;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxOficina;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxSala;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbxSolicitud;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aPROBARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dESAPROBARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fINALIZARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;

    }
}