namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmListaPlanillasTolvas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaPlanillasTolvas));
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.cbPendientes = new System.Windows.Forms.CheckBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlanilla = new System.Windows.Forms.TextBox();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.dtgListaPlanillasTolvas = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imprimirPlanillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirDespachoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gastoAdicionalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarPlanillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaPlanillasTolvasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tsCambioRuta = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPlanillasTolvas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPlanillasTolvasView)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SpringGreen;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.5F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1109, 46);
            this.label2.TabIndex = 104;
            this.label2.Text = "LISTA DE PLANILLAS - TOLVAS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.cbPendientes);
            this.panel3.Controls.Add(this.txtConductor);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtPlanilla);
            this.panel3.Controls.Add(this.btnNuevo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1109, 102);
            this.panel3.TabIndex = 183;
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(856, 27);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 197;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(798, 27);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbPendientes
            // 
            this.cbPendientes.AutoSize = true;
            this.cbPendientes.Location = new System.Drawing.Point(784, 31);
            this.cbPendientes.Name = "cbPendientes";
            this.cbPendientes.Size = new System.Drawing.Size(83, 43);
            this.cbPendientes.TabIndex = 222;
            this.cbPendientes.Text = "Ver Planillas\r\nPendientes\r\npor Liquidar";
            this.cbPendientes.UseVisualStyleBackColor = true;
            this.cbPendientes.Visible = false;
            this.cbPendientes.CheckedChanged += new System.EventHandler(this.cbPendientes_CheckedChanged);
            // 
            // txtConductor
            // 
            this.txtConductor.Location = new System.Drawing.Point(227, 58);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(266, 20);
            this.txtConductor.TabIndex = 221;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(162, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 220;
            this.label5.Text = "Conductor:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Location = new System.Drawing.Point(519, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 58);
            this.groupBox1.TabIndex = 219;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha de Viaje:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(133, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(116, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "-";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(15, 24);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaInicio.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 218;
            this.label1.Text = "Planilla:";
            // 
            // txtPlanilla
            // 
            this.txtPlanilla.Location = new System.Drawing.Point(227, 23);
            this.txtPlanilla.Name = "txtPlanilla";
            this.txtPlanilla.Size = new System.Drawing.Size(266, 20);
            this.txtPlanilla.TabIndex = 204;
            this.txtPlanilla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlanilla_KeyPress);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.Appearance.Options.UseBackColor = true;
            this.btnNuevo.Appearance.Options.UseBorderColor = true;
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(31, 27);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(104, 47);
            this.btnNuevo.TabIndex = 208;
            this.btnNuevo.Tag = "5";
            this.btnNuevo.Text = "Agregar\r\nPlanilla";
            this.btnNuevo.ToolTip = "Agregar Planilla";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // dtgListaPlanillasTolvas
            // 
            this.dtgListaPlanillasTolvas.AllowDrop = true;
            this.dtgListaPlanillasTolvas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaPlanillasTolvas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaPlanillasTolvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaPlanillasTolvas.Location = new System.Drawing.Point(0, 148);
            this.dtgListaPlanillasTolvas.MainView = this.dgvListaPlanillasTolvasView;
            this.dtgListaPlanillasTolvas.Name = "dtgListaPlanillasTolvas";
            this.dtgListaPlanillasTolvas.Size = new System.Drawing.Size(1109, 502);
            this.dtgListaPlanillasTolvas.TabIndex = 184;
            this.dtgListaPlanillasTolvas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaPlanillasTolvasView});
            this.dtgListaPlanillasTolvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaPlanillasTolvas_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirPlanillaToolStripMenuItem,
            this.imprimirDespachoToolStripMenuItem,
            this.gastoAdicionalToolStripMenuItem,
            this.tsCambioRuta,
            this.eliminarPlanillaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 136);
            // 
            // imprimirPlanillaToolStripMenuItem
            // 
            this.imprimirPlanillaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.impresora;
            this.imprimirPlanillaToolStripMenuItem.Name = "imprimirPlanillaToolStripMenuItem";
            this.imprimirPlanillaToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.imprimirPlanillaToolStripMenuItem.Text = "Imprimir Planilla";
            this.imprimirPlanillaToolStripMenuItem.Click += new System.EventHandler(this.imprimirPlanillaToolStripMenuItem_Click);
            // 
            // imprimirDespachoToolStripMenuItem
            // 
            this.imprimirDespachoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.text;
            this.imprimirDespachoToolStripMenuItem.Name = "imprimirDespachoToolStripMenuItem";
            this.imprimirDespachoToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.imprimirDespachoToolStripMenuItem.Text = "Imprimir Despacho";
            this.imprimirDespachoToolStripMenuItem.Click += new System.EventHandler(this.imprimirDespachoToolStripMenuItem_Click);
            // 
            // gastoAdicionalToolStripMenuItem
            // 
            this.gastoAdicionalToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.persona_logo_icon_169946;
            this.gastoAdicionalToolStripMenuItem.Name = "gastoAdicionalToolStripMenuItem";
            this.gastoAdicionalToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.gastoAdicionalToolStripMenuItem.Text = "Gasto Adicional";
            this.gastoAdicionalToolStripMenuItem.Click += new System.EventHandler(this.gastoAdicionalToolStripMenuItem_Click);
            // 
            // eliminarPlanillaToolStripMenuItem
            // 
            this.eliminarPlanillaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarPlanillaToolStripMenuItem.Name = "eliminarPlanillaToolStripMenuItem";
            this.eliminarPlanillaToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.eliminarPlanillaToolStripMenuItem.Text = "Eliminar Planilla";
            this.eliminarPlanillaToolStripMenuItem.Click += new System.EventHandler(this.eliminarPlanillaToolStripMenuItem_Click);
            // 
            // dgvListaPlanillasTolvasView
            // 
            this.dgvListaPlanillasTolvasView.GridControl = this.dtgListaPlanillasTolvas;
            this.dgvListaPlanillasTolvasView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvListaPlanillasTolvasView.Name = "dgvListaPlanillasTolvasView";
            this.dgvListaPlanillasTolvasView.OptionsBehavior.Editable = false;
            this.dgvListaPlanillasTolvasView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaPlanillasTolvasView.OptionsView.RowAutoHeight = true;
            this.dgvListaPlanillasTolvasView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaPlanillasTolvasView_CustomDrawCell);
            // 
            // tsCambioRuta
            // 
            this.tsCambioRuta.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.tsCambioRuta.Name = "tsCambioRuta";
            this.tsCambioRuta.Size = new System.Drawing.Size(204, 22);
            this.tsCambioRuta.Text = "Agregar Cambio de Ruta";
            this.tsCambioRuta.Click += new System.EventHandler(this.tsCambioRuta_Click);
            // 
            // frmListaPlanillasTolvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 650);
            this.Controls.Add(this.dtgListaPlanillasTolvas);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Name = "frmListaPlanillasTolvas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LISTA DE PLANILLAS - TOLVAS";
            this.Load += new System.EventHandler(this.frmListaPlanillasTolvas_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPlanillasTolvas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPlanillasTolvasView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlanilla;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private System.Windows.Forms.CheckBox cbPendientes;
        public DevExpress.XtraGrid.GridControl dtgListaPlanillasTolvas;
        public DevExpress.XtraGrid.Views.Grid.GridView dgvListaPlanillasTolvasView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem imprimirPlanillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gastoAdicionalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarPlanillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirDespachoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsCambioRuta;

    }
}