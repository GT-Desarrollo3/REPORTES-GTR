namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmPendientesDiarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPendientesDiarios));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnNuevaActividad = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResponsable = new System.Windows.Forms.TextBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgListaPendientes = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actualizarEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprogramarActividadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarActividadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaPendientesVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cbFechaProyectada = new System.Windows.Forms.CheckBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPendientes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPendientesVista)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(994, 50);
            this.label1.TabIndex = 21;
            this.label1.Text = "LISTA DE PENDIENTES DIARIOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.cbxArea);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.btnNuevaActividad);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.dtpFechaFin);
            this.panel3.Controls.Add(this.dtpFechaInicio);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtResponsable);
            this.panel3.Controls.Add(this.cbxEstado);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.cbFechaProyectada);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(994, 112);
            this.panel3.TabIndex = 183;
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(603, 27);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(164, 21);
            this.cbxArea.TabIndex = 225;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            this.cbxArea.DropDownClosed += new System.EventHandler(this.cbxArea_DropDownClosed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(565, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 224;
            this.label7.Text = "Área:";
            // 
            // btnNuevaActividad
            // 
            this.btnNuevaActividad.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevaActividad.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevaActividad.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevaActividad.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaActividad.Appearance.Options.UseBackColor = true;
            this.btnNuevaActividad.Appearance.Options.UseBorderColor = true;
            this.btnNuevaActividad.Appearance.Options.UseFont = true;
            this.btnNuevaActividad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaActividad.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaActividad.Image")));
            this.btnNuevaActividad.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevaActividad.Location = new System.Drawing.Point(32, 33);
            this.btnNuevaActividad.Name = "btnNuevaActividad";
            this.btnNuevaActividad.Size = new System.Drawing.Size(104, 47);
            this.btnNuevaActividad.TabIndex = 223;
            this.btnNuevaActividad.Tag = "5";
            this.btnNuevaActividad.Text = "Nueva\r\nActividad";
            this.btnNuevaActividad.ToolTip = "Nueva Actividad";
            this.btnNuevaActividad.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(361, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "--";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(382, 62);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(260, 62);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 218;
            this.label2.Text = "Responsable:";
            // 
            // txtResponsable
            // 
            this.txtResponsable.Location = new System.Drawing.Point(245, 27);
            this.txtResponsable.Name = "txtResponsable";
            this.txtResponsable.Size = new System.Drawing.Size(280, 20);
            this.txtResponsable.TabIndex = 204;
            this.txtResponsable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResponsable_KeyPress);
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODOS",
            "PENDIENTE",
            "EN PROCESO",
            "EJECUTADO",
            "DESESTIMADO"});
            this.cbxEstado.Location = new System.Drawing.Point(603, 64);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(164, 21);
            this.cbxEstado.TabIndex = 217;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(554, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 216;
            this.label3.Text = "Estado:";
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
            this.btnExcel.Location = new System.Drawing.Point(914, 34);
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
            this.btnBuscar.Location = new System.Drawing.Point(852, 34);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgListaPendientes
            // 
            this.dtgListaPendientes.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaPendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaPendientes.Location = new System.Drawing.Point(0, 162);
            this.dtgListaPendientes.MainView = this.dgvListaPendientesVista;
            this.dtgListaPendientes.Name = "dtgListaPendientes";
            this.dtgListaPendientes.Size = new System.Drawing.Size(994, 408);
            this.dtgListaPendientes.TabIndex = 184;
            this.dtgListaPendientes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaPendientesVista});
            this.dtgListaPendientes.DoubleClick += new System.EventHandler(this.dtgListaPendientes_DoubleClick);
            this.dtgListaPendientes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaPendientes_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarEstadoToolStripMenuItem,
            this.reprogramarActividadToolStripMenuItem,
            this.eliminarActividadToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 92);
            // 
            // actualizarEstadoToolStripMenuItem
            // 
            this.actualizarEstadoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.actualizarEstadoToolStripMenuItem.Name = "actualizarEstadoToolStripMenuItem";
            this.actualizarEstadoToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.actualizarEstadoToolStripMenuItem.Text = "Actualizar Actividad";
            this.actualizarEstadoToolStripMenuItem.Click += new System.EventHandler(this.actualizarEstadoToolStripMenuItem_Click);
            // 
            // reprogramarActividadToolStripMenuItem
            // 
            this.reprogramarActividadToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.reprogramarActividadToolStripMenuItem.Name = "reprogramarActividadToolStripMenuItem";
            this.reprogramarActividadToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.reprogramarActividadToolStripMenuItem.Text = "Reprogramar Actividad";
            this.reprogramarActividadToolStripMenuItem.Click += new System.EventHandler(this.reprogramarActividadToolStripMenuItem_Click);
            // 
            // eliminarActividadToolStripMenuItem
            // 
            this.eliminarActividadToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.eliminarActividadToolStripMenuItem.Name = "eliminarActividadToolStripMenuItem";
            this.eliminarActividadToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.eliminarActividadToolStripMenuItem.Text = "Eliminar Actividad";
            this.eliminarActividadToolStripMenuItem.Click += new System.EventHandler(this.eliminarActividadToolStripMenuItem_Click);
            // 
            // dgvListaPendientesVista
            // 
            this.dgvListaPendientesVista.GridControl = this.dtgListaPendientes;
            this.dgvListaPendientesVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvListaPendientesVista.Name = "dgvListaPendientesVista";
            this.dgvListaPendientesVista.OptionsBehavior.Editable = false;
            this.dgvListaPendientesVista.OptionsBehavior.ReadOnly = true;
            this.dgvListaPendientesVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaPendientesVista.OptionsView.RowAutoHeight = true;
            this.dgvListaPendientesVista.OptionsView.ShowFooter = true;
            this.dgvListaPendientesVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaPendientesVista_CustomDrawCell);
            // 
            // cbFechaProyectada
            // 
            this.cbFechaProyectada.AutoSize = true;
            this.cbFechaProyectada.Location = new System.Drawing.Point(170, 64);
            this.cbFechaProyectada.Name = "cbFechaProyectada";
            this.cbFechaProyectada.Size = new System.Drawing.Size(87, 17);
            this.cbFechaProyectada.TabIndex = 226;
            this.cbFechaProyectada.Text = "Fecha Inicio:";
            this.cbFechaProyectada.UseVisualStyleBackColor = true;
            this.cbFechaProyectada.CheckedChanged += new System.EventHandler(this.cbFechaProyectada_CheckedChanged);
            // 
            // frmPendientesDiarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 570);
            this.Controls.Add(this.dtgListaPendientes);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Name = "frmPendientesDiarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PENDIENTES DIARIOS - OPERACIONES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPendientesDiarios_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPendientes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPendientesVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResponsable;
        private System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.Label label3;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgListaPendientes;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaPendientesVista;
        private DevExpress.XtraEditors.SimpleButton btnNuevaActividad;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actualizarEstadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reprogramarActividadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarActividadToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbFechaProyectada;

    }
}