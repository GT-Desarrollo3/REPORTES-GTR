namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class FrmListarPreviajesLibres
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtPreviaje = new System.Windows.Forms.TextBox();
            this.g_fecha = new System.Windows.Forms.GroupBox();
            this.chkFecha = new System.Windows.Forms.CheckBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtgListaGuiasTransportista = new DevExpress.XtraGrid.GridControl();
            this.dgvListaGuiaTraspExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTicketActual = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtGuiaTransportsta = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.vincularYGenerarViajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.g_fecha.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuiasTransportista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiaTraspExpressVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.g_fecha);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar Nuevo Ticket";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtPreviaje);
            this.groupBox5.Location = new System.Drawing.Point(385, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(86, 55);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Nro Ticket - Previaje";
            // 
            // txtPreviaje
            // 
            this.txtPreviaje.Location = new System.Drawing.Point(7, 29);
            this.txtPreviaje.Name = "txtPreviaje";
            this.txtPreviaje.Size = new System.Drawing.Size(69, 20);
            this.txtPreviaje.TabIndex = 4;
            // 
            // g_fecha
            // 
            this.g_fecha.Controls.Add(this.chkFecha);
            this.g_fecha.Controls.Add(this.dtpFechaFin);
            this.g_fecha.Controls.Add(this.dtpFechaInicio);
            this.g_fecha.Location = new System.Drawing.Point(248, 9);
            this.g_fecha.Name = "g_fecha";
            this.g_fecha.Size = new System.Drawing.Size(121, 65);
            this.g_fecha.TabIndex = 1;
            this.g_fecha.TabStop = false;
            this.g_fecha.Text = "Fecha Inicio";
            this.g_fecha.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // chkFecha
            // 
            this.chkFecha.AutoSize = true;
            this.chkFecha.Location = new System.Drawing.Point(71, 0);
            this.chkFecha.Name = "chkFecha";
            this.chkFecha.Size = new System.Drawing.Size(15, 14);
            this.chkFecha.TabIndex = 4;
            this.chkFecha.UseVisualStyleBackColor = true;
            this.chkFecha.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(8, 40);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaFin.TabIndex = 1;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(7, 16);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaInicio.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxOperacion);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 44);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operacion";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(7, 17);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(212, 21);
            this.cbxOperacion.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(499, 40);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgListaGuiasTransportista
            // 
            this.dtgListaGuiasTransportista.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaGuiasTransportista.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgListaGuiasTransportista.Location = new System.Drawing.Point(0, 141);
            this.dtgListaGuiasTransportista.MainView = this.dgvListaGuiaTraspExpressVista;
            this.dtgListaGuiasTransportista.Name = "dtgListaGuiasTransportista";
            this.dtgListaGuiasTransportista.Size = new System.Drawing.Size(606, 257);
            this.dtgListaGuiasTransportista.TabIndex = 13;
            this.dtgListaGuiasTransportista.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaGuiaTraspExpressVista,
            this.gridView1});
            // 
            // dgvListaGuiaTraspExpressVista
            // 
            this.dgvListaGuiaTraspExpressVista.GridControl = this.dtgListaGuiasTransportista;
            this.dgvListaGuiaTraspExpressVista.Name = "dgvListaGuiaTraspExpressVista";
            this.dgvListaGuiaTraspExpressVista.OptionsBehavior.Editable = false;
            this.dgvListaGuiaTraspExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaGuiaTraspExpressVista.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.dgvListaGuiaTraspExpressVista.OptionsView.RowAutoHeight = true;
            this.dgvListaGuiaTraspExpressVista.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.dgvListaGuiaTraspExpressVista_RowClick);
            this.dgvListaGuiaTraspExpressVista.DoubleClick += new System.EventHandler(this.dgvListaGuiaTraspExpressVista_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgListaGuiasTransportista;
            this.gridView1.Name = "gridView1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTicketActual);
            this.groupBox3.Location = new System.Drawing.Point(126, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(110, 45);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Nro Ticket Actual";
            // 
            // txtTicketActual
            // 
            this.txtTicketActual.Location = new System.Drawing.Point(14, 18);
            this.txtTicketActual.Name = "txtTicketActual";
            this.txtTicketActual.ReadOnly = true;
            this.txtTicketActual.Size = new System.Drawing.Size(76, 20);
            this.txtTicketActual.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtGuiaTransportsta);
            this.groupBox4.Location = new System.Drawing.Point(6, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(110, 45);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Guia Transportista";
            // 
            // txtGuiaTransportsta
            // 
            this.txtGuiaTransportsta.Location = new System.Drawing.Point(14, 18);
            this.txtGuiaTransportsta.Name = "txtGuiaTransportsta";
            this.txtGuiaTransportsta.ReadOnly = true;
            this.txtGuiaTransportsta.Size = new System.Drawing.Size(76, 20);
            this.txtGuiaTransportsta.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vincularYGenerarViajeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(199, 48);
            // 
            // vincularYGenerarViajeToolStripMenuItem
            // 
            this.vincularYGenerarViajeToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.vincularYGenerarViajeToolStripMenuItem.Name = "vincularYGenerarViajeToolStripMenuItem";
            this.vincularYGenerarViajeToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.vincularYGenerarViajeToolStripMenuItem.Text = "Vincular y Generar Viaje";
            this.vincularYGenerarViajeToolStripMenuItem.Click += new System.EventHandler(this.vincularYGenerarViajeToolStripMenuItem_Click);
            // 
            // FrmListarPreviajesLibres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(606, 398);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtgListaGuiasTransportista);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmListarPreviajesLibres";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListarPreviajesLibrescs";
            this.Load += new System.EventHandler(this.FrmListarPreviajesLibrescs_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.g_fecha.ResumeLayout(false);
            this.g_fecha.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuiasTransportista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiaTraspExpressVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.GroupBox g_fecha;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxOperacion;
        private DevExpress.XtraGrid.GridControl dtgListaGuiasTransportista;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaGuiaTraspExpressVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtPreviaje;
        private System.Windows.Forms.CheckBox chkFecha;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txtTicketActual;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.TextBox txtGuiaTransportsta;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem vincularYGenerarViajeToolStripMenuItem;
    }
}