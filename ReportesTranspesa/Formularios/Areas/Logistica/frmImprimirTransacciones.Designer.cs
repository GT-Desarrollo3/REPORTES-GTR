namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    partial class frmImprimirTransacciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImprimirTransacciones));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevoItem = new DevExpress.XtraEditors.SimpleButton();
            this.txtOrdenTrabajo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgTransacciones = new DevExpress.XtraGrid.GridControl();
            this.dtgvTransaccionesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pDetalleTransaccion = new System.Windows.Forms.Panel();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnImprimir = new DevExpress.XtraEditors.SimpleButton();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dtgDetalleTransaccion = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDetalleTransaccionVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtNroOT = new System.Windows.Forms.TextBox();
            this.lstItems = new System.Windows.Forms.ListView();
            this.tabDivemotor = new System.Windows.Forms.TabControl();
            this.tabOrdenTrabajo = new System.Windows.Forms.TabPage();
            this.tabListaTickets = new System.Windows.Forms.TabPage();
            this.dtgRegistroOT = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAnularImpresion = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvRegistroOTView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransacciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTransaccionesView)).BeginInit();
            this.pDetalleTransaccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetalleTransaccion)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleTransaccionVista)).BeginInit();
            this.tabDivemotor.SuspendLayout();
            this.tabOrdenTrabajo.SuspendLayout();
            this.tabListaTickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroOT)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroOTView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.btnNuevoItem);
            this.panel4.Controls.Add(this.txtOrdenTrabajo);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.dtpFechaFin);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.dtpFechaInicio);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.ForeColor = System.Drawing.Color.Transparent;
            this.panel4.Location = new System.Drawing.Point(20, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(965, 88);
            this.panel4.TabIndex = 15;
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
            this.btnExcel.Location = new System.Drawing.Point(697, 14);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 225;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnNuevoItem
            // 
            this.btnNuevoItem.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoItem.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoItem.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoItem.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoItem.Appearance.Options.UseBackColor = true;
            this.btnNuevoItem.Appearance.Options.UseBorderColor = true;
            this.btnNuevoItem.Appearance.Options.UseFont = true;
            this.btnNuevoItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoItem.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoItem.Image")));
            this.btnNuevoItem.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoItem.Location = new System.Drawing.Point(7, 14);
            this.btnNuevoItem.Name = "btnNuevoItem";
            this.btnNuevoItem.Size = new System.Drawing.Size(100, 47);
            this.btnNuevoItem.TabIndex = 224;
            this.btnNuevoItem.Text = "Agregar\r\nÍtem";
            this.btnNuevoItem.ToolTip = "Agregar Ítem";
            this.btnNuevoItem.Click += new System.EventHandler(this.btnNuevoItem_Click);
            // 
            // txtOrdenTrabajo
            // 
            this.txtOrdenTrabajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOrdenTrabajo.Location = new System.Drawing.Point(147, 39);
            this.txtOrdenTrabajo.Name = "txtOrdenTrabajo";
            this.txtOrdenTrabajo.Size = new System.Drawing.Size(148, 23);
            this.txtOrdenTrabajo.TabIndex = 204;
            this.txtOrdenTrabajo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrdenTrabajo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(335, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 216;
            this.label1.Text = "Buscar por Fecha:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(463, 39);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(101, 23);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(441, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "--";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(338, 39);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(101, 23);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
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
            this.btnBuscar.Location = new System.Drawing.Point(635, 14);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label3.Location = new System.Drawing.Point(144, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 16);
            this.label3.TabIndex = 217;
            this.label3.Text = "Buscar por Orden Trabajo:";
            // 
            // dtgTransacciones
            // 
            this.dtgTransacciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTransacciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTransacciones.Location = new System.Drawing.Point(3, 3);
            this.dtgTransacciones.LookAndFeel.SkinName = "Money Twins";
            this.dtgTransacciones.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTransacciones.MainView = this.dtgvTransaccionesView;
            this.dtgTransacciones.Name = "dtgTransacciones";
            this.dtgTransacciones.Size = new System.Drawing.Size(951, 407);
            this.dtgTransacciones.TabIndex = 16;
            this.dtgTransacciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvTransaccionesView});
            this.dtgTransacciones.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dtgTransacciones_MouseDoubleClick);
            // 
            // dtgvTransaccionesView
            // 
            this.dtgvTransaccionesView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvTransaccionesView.Appearance.HeaderPanel.Options.UseFont = true;
            this.dtgvTransaccionesView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvTransaccionesView.Appearance.Row.Options.UseFont = true;
            this.dtgvTransaccionesView.GridControl = this.dtgTransacciones;
            this.dtgvTransaccionesView.Name = "dtgvTransaccionesView";
            this.dtgvTransaccionesView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvTransaccionesView.OptionsBehavior.Editable = false;
            this.dtgvTransaccionesView.OptionsView.ColumnAutoWidth = false;
            this.dtgvTransaccionesView.OptionsView.ShowFooter = true;
            // 
            // pDetalleTransaccion
            // 
            this.pDetalleTransaccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDetalleTransaccion.Controls.Add(this.btnAgregar);
            this.pDetalleTransaccion.Controls.Add(this.txtDescripcion);
            this.pDetalleTransaccion.Controls.Add(this.btnImprimir);
            this.pDetalleTransaccion.Controls.Add(this.btnCerrar);
            this.pDetalleTransaccion.Controls.Add(this.lblTitulo);
            this.pDetalleTransaccion.Controls.Add(this.dtgDetalleTransaccion);
            this.pDetalleTransaccion.Controls.Add(this.txtNroOT);
            this.pDetalleTransaccion.Controls.Add(this.lstItems);
            this.pDetalleTransaccion.Location = new System.Drawing.Point(118, 208);
            this.pDetalleTransaccion.Name = "pDetalleTransaccion";
            this.pDetalleTransaccion.Size = new System.Drawing.Size(885, 400);
            this.pDetalleTransaccion.TabIndex = 17;
            this.pDetalleTransaccion.Visible = false;
            this.pDetalleTransaccion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDetalleTransaccion_MouseMove);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(636, 40);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(102, 40);
            this.btnAgregar.TabIndex = 224;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.ToolTip = "Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(122, 51);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(488, 22);
            this.txtDescripcion.TabIndex = 223;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            this.txtDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyUp);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Appearance.BackColor = System.Drawing.Color.White;
            this.btnImprimir.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnImprimir.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Appearance.Options.UseBackColor = true;
            this.btnImprimir.Appearance.Options.UseBorderColor = true;
            this.btnImprimir.Appearance.Options.UseFont = true;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(636, 40);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(102, 40);
            this.btnImprimir.TabIndex = 221;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.ToolTip = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(858, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 219;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTitulo.Location = new System.Drawing.Point(14, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(283, 22);
            this.lblTitulo.TabIndex = 217;
            this.lblTitulo.Text = "N° DE ORDEN DE TRABAJO: ";
            // 
            // dtgDetalleTransaccion
            // 
            this.dtgDetalleTransaccion.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgDetalleTransaccion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgDetalleTransaccion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgDetalleTransaccion.Location = new System.Drawing.Point(0, 94);
            this.dtgDetalleTransaccion.LookAndFeel.SkinName = "Money Twins";
            this.dtgDetalleTransaccion.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDetalleTransaccion.MainView = this.dgvDetalleTransaccionVista;
            this.dtgDetalleTransaccion.Name = "dtgDetalleTransaccion";
            this.dtgDetalleTransaccion.Size = new System.Drawing.Size(883, 304);
            this.dtgDetalleTransaccion.TabIndex = 16;
            this.dtgDetalleTransaccion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvDetalleTransaccionVista});
            this.dtgDetalleTransaccion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgDetalleTransaccion_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 26);
            // 
            // tsEliminarItem
            // 
            this.tsEliminarItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarItem.Name = "tsEliminarItem";
            this.tsEliminarItem.Size = new System.Drawing.Size(134, 22);
            this.tsEliminarItem.Text = "Quitar Ítem";
            this.tsEliminarItem.Click += new System.EventHandler(this.tsEliminarItem_Click);
            // 
            // dgvDetalleTransaccionVista
            // 
            this.dgvDetalleTransaccionVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDetalleTransaccionVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvDetalleTransaccionVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDetalleTransaccionVista.Appearance.Row.Options.UseFont = true;
            this.dgvDetalleTransaccionVista.GridControl = this.dtgDetalleTransaccion;
            this.dgvDetalleTransaccionVista.Name = "dgvDetalleTransaccionVista";
            this.dgvDetalleTransaccionVista.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvDetalleTransaccionVista.OptionsBehavior.Editable = false;
            this.dgvDetalleTransaccionVista.OptionsSelection.MultiSelect = true;
            this.dgvDetalleTransaccionVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvDetalleTransaccionVista.OptionsView.ColumnAutoWidth = false;
            this.dgvDetalleTransaccionVista.OptionsView.ShowGroupPanel = false;
            this.dgvDetalleTransaccionVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvDetalleTransaccionVista_CustomDrawCell);
            // 
            // txtNroOT
            // 
            this.txtNroOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroOT.Location = new System.Drawing.Point(18, 51);
            this.txtNroOT.Name = "txtNroOT";
            this.txtNroOT.ReadOnly = true;
            this.txtNroOT.Size = new System.Drawing.Size(91, 22);
            this.txtNroOT.TabIndex = 222;
            // 
            // lstItems
            // 
            this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstItems.ForeColor = System.Drawing.Color.Navy;
            this.lstItems.FullRowSelect = true;
            this.lstItems.GridLines = true;
            this.lstItems.Location = new System.Drawing.Point(122, 72);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(488, 127);
            this.lstItems.TabIndex = 225;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.View = System.Windows.Forms.View.Details;
            this.lstItems.Visible = false;
            this.lstItems.Enter += new System.EventHandler(this.lstItems_Enter);
            this.lstItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstItems_KeyPress);
            this.lstItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDoubleClick);
            // 
            // tabDivemotor
            // 
            this.tabDivemotor.Controls.Add(this.tabOrdenTrabajo);
            this.tabDivemotor.Controls.Add(this.tabListaTickets);
            this.tabDivemotor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDivemotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold);
            this.tabDivemotor.Location = new System.Drawing.Point(20, 148);
            this.tabDivemotor.Name = "tabDivemotor";
            this.tabDivemotor.SelectedIndex = 0;
            this.tabDivemotor.Size = new System.Drawing.Size(965, 443);
            this.tabDivemotor.TabIndex = 18;
            // 
            // tabOrdenTrabajo
            // 
            this.tabOrdenTrabajo.Controls.Add(this.dtgTransacciones);
            this.tabOrdenTrabajo.Location = new System.Drawing.Point(4, 26);
            this.tabOrdenTrabajo.Name = "tabOrdenTrabajo";
            this.tabOrdenTrabajo.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrdenTrabajo.Size = new System.Drawing.Size(957, 413);
            this.tabOrdenTrabajo.TabIndex = 0;
            this.tabOrdenTrabajo.Text = "LISTA DE OT";
            this.tabOrdenTrabajo.UseVisualStyleBackColor = true;
            // 
            // tabListaTickets
            // 
            this.tabListaTickets.Controls.Add(this.dtgRegistroOT);
            this.tabListaTickets.Location = new System.Drawing.Point(4, 26);
            this.tabListaTickets.Name = "tabListaTickets";
            this.tabListaTickets.Padding = new System.Windows.Forms.Padding(3);
            this.tabListaTickets.Size = new System.Drawing.Size(957, 413);
            this.tabListaTickets.TabIndex = 1;
            this.tabListaTickets.Text = "TICKETS IMPRESOS";
            this.tabListaTickets.UseVisualStyleBackColor = true;
            // 
            // dtgRegistroOT
            // 
            this.dtgRegistroOT.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgRegistroOT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRegistroOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRegistroOT.Location = new System.Drawing.Point(3, 3);
            this.dtgRegistroOT.LookAndFeel.SkinName = "Money Twins";
            this.dtgRegistroOT.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRegistroOT.MainView = this.dgvRegistroOTView;
            this.dtgRegistroOT.Name = "dtgRegistroOT";
            this.dtgRegistroOT.Size = new System.Drawing.Size(951, 407);
            this.dtgRegistroOT.TabIndex = 17;
            this.dtgRegistroOT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRegistroOTView});
            this.dtgRegistroOT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgRegistroOT_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAnularImpresion});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(166, 26);
            // 
            // tsAnularImpresion
            // 
            this.tsAnularImpresion.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsAnularImpresion.Name = "tsAnularImpresion";
            this.tsAnularImpresion.Size = new System.Drawing.Size(165, 22);
            this.tsAnularImpresion.Text = "Anular Impresión";
            this.tsAnularImpresion.Click += new System.EventHandler(this.tsAnularImpresion_Click);
            // 
            // dgvRegistroOTView
            // 
            this.dgvRegistroOTView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRegistroOTView.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvRegistroOTView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRegistroOTView.Appearance.Row.Options.UseFont = true;
            this.dgvRegistroOTView.GridControl = this.dtgRegistroOT;
            this.dgvRegistroOTView.Name = "dgvRegistroOTView";
            this.dgvRegistroOTView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvRegistroOTView.OptionsBehavior.Editable = false;
            this.dgvRegistroOTView.OptionsView.ColumnAutoWidth = false;
            this.dgvRegistroOTView.OptionsView.ShowFooter = true;
            // 
            // frmImprimirTransacciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 611);
            this.Controls.Add(this.tabDivemotor);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pDetalleTransaccion);
            this.Name = "frmImprimirTransacciones";
            this.Text = "ÓRDENES DE TRABAJO - DIVEMOTOR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmImprimirTransacciones_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransacciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTransaccionesView)).EndInit();
            this.pDetalleTransaccion.ResumeLayout(false);
            this.pDetalleTransaccion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetalleTransaccion)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleTransaccionVista)).EndInit();
            this.tabDivemotor.ResumeLayout(false);
            this.tabOrdenTrabajo.ResumeLayout(false);
            this.tabListaTickets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroOT)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroOTView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtOrdenTrabajo;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgTransacciones;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvTransaccionesView;
        private System.Windows.Forms.Panel pDetalleTransaccion;
        public System.Windows.Forms.TextBox txtDescripcion;
        private DevExpress.XtraEditors.SimpleButton btnImprimir;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private DevExpress.XtraGrid.GridControl dtgDetalleTransaccion;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDetalleTransaccionVista;
        public System.Windows.Forms.TextBox txtNroOT;
        private DevExpress.XtraEditors.SimpleButton btnNuevoItem;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarItem;
        private System.Windows.Forms.ListView lstItems;
        private System.Windows.Forms.TabControl tabDivemotor;
        private System.Windows.Forms.TabPage tabOrdenTrabajo;
        private System.Windows.Forms.TabPage tabListaTickets;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraGrid.GridControl dtgRegistroOT;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRegistroOTView;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsAnularImpresion;
    }
}