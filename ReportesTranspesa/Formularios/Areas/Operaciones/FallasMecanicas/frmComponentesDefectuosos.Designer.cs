namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    partial class frmComponentesDefectuosos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComponentesDefectuosos));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblporcentaje = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTerminados = new System.Windows.Forms.Label();
            this.lblPendientes = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.rbListaPendientes = new System.Windows.Forms.RadioButton();
            this.rbListaSolucionadas = new System.Windows.Forms.RadioButton();
            this.rbListaTodas = new System.Windows.Forms.RadioButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fechaFin = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.fechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.dtgListaComponentes = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignarOTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaComponentesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pListaOT = new System.Windows.Forms.Panel();
            this.btnBuscarOT = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPlacaOT = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtgListaOT = new DevExpress.XtraGrid.GridControl();
            this.dgvListaOTVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.pModificar = new System.Windows.Forms.Panel();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxPosicionLlanta = new System.Windows.Forms.ComboBox();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.cbxDetalle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxComponente = new System.Windows.Forms.ComboBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSubTipo = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPlacaUnidad = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaComponentes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaComponentesView)).BeginInit();
            this.pListaOT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaOT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaOTVista)).BeginInit();
            this.pModificar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Cyan;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1148, 47);
            this.label1.TabIndex = 17;
            this.label1.Text = "REPORTE DE FALLAS EN COMPONENTES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.lblTerminados);
            this.panel3.Controls.Add(this.lblPendientes);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.rbListaPendientes);
            this.panel3.Controls.Add(this.rbListaSolucionadas);
            this.panel3.Controls.Add(this.rbListaTodas);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1148, 96);
            this.panel3.TabIndex = 18;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblporcentaje);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(822, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(149, 58);
            this.groupBox3.TabIndex = 58;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // lblporcentaje
            // 
            this.lblporcentaje.AutoSize = true;
            this.lblporcentaje.BackColor = System.Drawing.Color.Transparent;
            this.lblporcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblporcentaje.ForeColor = System.Drawing.Color.Red;
            this.lblporcentaje.Location = new System.Drawing.Point(34, 24);
            this.lblporcentaje.Name = "lblporcentaje";
            this.lblporcentaje.Size = new System.Drawing.Size(64, 20);
            this.lblporcentaje.TabIndex = 57;
            this.lblporcentaje.Text = "0.00 %";
            this.lblporcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, -1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 16);
            this.label4.TabIndex = 56;
            this.label4.Text = "% Cumplimiento: ";
            // 
            // lblTerminados
            // 
            this.lblTerminados.AutoSize = true;
            this.lblTerminados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblTerminados.Location = new System.Drawing.Point(754, 63);
            this.lblTerminados.Name = "lblTerminados";
            this.lblTerminados.Size = new System.Drawing.Size(16, 16);
            this.lblTerminados.TabIndex = 55;
            this.lblTerminados.Text = "0";
            this.lblTerminados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPendientes
            // 
            this.lblPendientes.AutoSize = true;
            this.lblPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.lblPendientes.Location = new System.Drawing.Point(754, 40);
            this.lblPendientes.Name = "lblPendientes";
            this.lblPendientes.Size = new System.Drawing.Size(16, 16);
            this.lblPendientes.TabIndex = 54;
            this.lblPendientes.Text = "0";
            this.lblPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_zoom_115406;
            this.btnBuscar.Location = new System.Drawing.Point(1017, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(46, 48);
            this.btnBuscar.TabIndex = 53;
            this.btnBuscar.Text = "&B";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // rbListaPendientes
            // 
            this.rbListaPendientes.AutoSize = true;
            this.rbListaPendientes.Location = new System.Drawing.Point(673, 39);
            this.rbListaPendientes.Name = "rbListaPendientes";
            this.rbListaPendientes.Size = new System.Drawing.Size(78, 17);
            this.rbListaPendientes.TabIndex = 52;
            this.rbListaPendientes.Text = "Pendientes";
            this.rbListaPendientes.UseVisualStyleBackColor = true;
            this.rbListaPendientes.Click += new System.EventHandler(this.rbListaPendientes_Click);
            // 
            // rbListaSolucionadas
            // 
            this.rbListaSolucionadas.AutoSize = true;
            this.rbListaSolucionadas.Location = new System.Drawing.Point(673, 62);
            this.rbListaSolucionadas.Name = "rbListaSolucionadas";
            this.rbListaSolucionadas.Size = new System.Drawing.Size(80, 17);
            this.rbListaSolucionadas.TabIndex = 51;
            this.rbListaSolucionadas.Text = "Terminadas";
            this.rbListaSolucionadas.UseVisualStyleBackColor = true;
            this.rbListaSolucionadas.Click += new System.EventHandler(this.rbListaSolucionadas_Click);
            // 
            // rbListaTodas
            // 
            this.rbListaTodas.AutoSize = true;
            this.rbListaTodas.Checked = true;
            this.rbListaTodas.Location = new System.Drawing.Point(673, 16);
            this.rbListaTodas.Name = "rbListaTodas";
            this.rbListaTodas.Size = new System.Drawing.Size(55, 17);
            this.rbListaTodas.TabIndex = 50;
            this.rbListaTodas.TabStop = true;
            this.rbListaTodas.Text = "Todas";
            this.rbListaTodas.UseVisualStyleBackColor = true;
            this.rbListaTodas.Click += new System.EventHandler(this.rbListaTodas_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(1076, 24);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(48, 48);
            this.btnExcel.TabIndex = 17;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.fechaFin);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.fechaInicio);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(319, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 58);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buscar por Fecha de Inicio:";
            // 
            // fechaFin
            // 
            this.fechaFin.CustomFormat = "dd-MM-yyyy";
            this.fechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fechaFin.Location = new System.Drawing.Point(208, 24);
            this.fechaFin.Name = "fechaFin";
            this.fechaFin.Size = new System.Drawing.Size(94, 20);
            this.fechaFin.TabIndex = 5;
            this.fechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fechaFin_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Fin:";
            // 
            // fechaInicio
            // 
            this.fechaInicio.CustomFormat = "dd-MM-yyyy";
            this.fechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fechaInicio.Location = new System.Drawing.Point(55, 24);
            this.fechaInicio.Name = "fechaInicio";
            this.fechaInicio.Size = new System.Drawing.Size(94, 20);
            this.fechaInicio.TabIndex = 2;
            this.fechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fechaInicio_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Inicio:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Location = new System.Drawing.Point(28, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 58);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por Placa:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(9, 24);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(237, 20);
            this.txtPlaca.TabIndex = 0;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // dtgListaComponentes
            // 
            this.dtgListaComponentes.AllowDrop = true;
            this.dtgListaComponentes.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaComponentes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaComponentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaComponentes.Location = new System.Drawing.Point(0, 143);
            this.dtgListaComponentes.MainView = this.dgvListaComponentesView;
            this.dtgListaComponentes.Name = "dtgListaComponentes";
            this.dtgListaComponentes.Size = new System.Drawing.Size(1148, 372);
            this.dtgListaComponentes.TabIndex = 99;
            this.dtgListaComponentes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaComponentesView});
            this.dtgListaComponentes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaComponentes_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarToolStripMenuItem,
            this.asignarOTToolStripMenuItem,
            this.terminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(132, 70);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // asignarOTToolStripMenuItem
            // 
            this.asignarOTToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.ordenar;
            this.asignarOTToolStripMenuItem.Name = "asignarOTToolStripMenuItem";
            this.asignarOTToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.asignarOTToolStripMenuItem.Text = "Asignar OT";
            this.asignarOTToolStripMenuItem.Click += new System.EventHandler(this.asignarOTToolStripMenuItem_Click);
            // 
            // terminarToolStripMenuItem
            // 
            this.terminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.confdestinos;
            this.terminarToolStripMenuItem.Name = "terminarToolStripMenuItem";
            this.terminarToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.terminarToolStripMenuItem.Text = "Terminar";
            this.terminarToolStripMenuItem.Click += new System.EventHandler(this.terminarToolStripMenuItem_Click);
            // 
            // dgvListaComponentesView
            // 
            this.dgvListaComponentesView.GridControl = this.dtgListaComponentes;
            this.dgvListaComponentesView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvListaComponentesView.Name = "dgvListaComponentesView";
            this.dgvListaComponentesView.OptionsBehavior.Editable = false;
            this.dgvListaComponentesView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaComponentesView.OptionsView.RowAutoHeight = true;
            this.dgvListaComponentesView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaComponentesView_CustomDrawCell);
            // 
            // pListaOT
            // 
            this.pListaOT.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pListaOT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pListaOT.Controls.Add(this.btnBuscarOT);
            this.pListaOT.Controls.Add(this.label12);
            this.pListaOT.Controls.Add(this.txtPlacaOT);
            this.pListaOT.Controls.Add(this.label9);
            this.pListaOT.Controls.Add(this.dtgListaOT);
            this.pListaOT.Controls.Add(this.btnCerrar2);
            this.pListaOT.Location = new System.Drawing.Point(381, 132);
            this.pListaOT.Name = "pListaOT";
            this.pListaOT.Size = new System.Drawing.Size(611, 381);
            this.pListaOT.TabIndex = 139;
            this.pListaOT.Visible = false;
            this.pListaOT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pListaOT_MouseMove);
            // 
            // btnBuscarOT
            // 
            this.btnBuscarOT.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscarOT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.btnBuscarOT.Location = new System.Drawing.Point(291, 53);
            this.btnBuscarOT.Name = "btnBuscarOT";
            this.btnBuscarOT.Size = new System.Drawing.Size(67, 26);
            this.btnBuscarOT.TabIndex = 105;
            this.btnBuscarOT.Text = "Buscar";
            this.btnBuscarOT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarOT.UseVisualStyleBackColor = false;
            this.btnBuscarOT.Click += new System.EventHandler(this.btnBuscarOT_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label12.Location = new System.Drawing.Point(18, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 15);
            this.label12.TabIndex = 104;
            this.label12.Text = "Buscar por Placa:";
            // 
            // txtPlacaOT
            // 
            this.txtPlacaOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtPlacaOT.Location = new System.Drawing.Point(127, 57);
            this.txtPlacaOT.Name = "txtPlacaOT";
            this.txtPlacaOT.Size = new System.Drawing.Size(136, 20);
            this.txtPlacaOT.TabIndex = 103;
            this.txtPlacaOT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlacaOT_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(17, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(422, 24);
            this.label9.TabIndex = 102;
            this.label9.Text = "SELECCIONAR UNA ORDEN DE TRABAJO:";
            // 
            // dtgListaOT
            // 
            this.dtgListaOT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaOT.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgListaOT.Location = new System.Drawing.Point(0, 96);
            this.dtgListaOT.MainView = this.dgvListaOTVista;
            this.dtgListaOT.Name = "dtgListaOT";
            this.dtgListaOT.Size = new System.Drawing.Size(609, 283);
            this.dtgListaOT.TabIndex = 101;
            this.dtgListaOT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaOTVista});
            this.dtgListaOT.DoubleClick += new System.EventHandler(this.dtgListaOT_DoubleClick);
            // 
            // dgvListaOTVista
            // 
            this.dgvListaOTVista.GridControl = this.dtgListaOT;
            this.dgvListaOTVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvListaOTVista.Name = "dgvListaOTVista";
            this.dgvListaOTVista.OptionsBehavior.Editable = false;
            this.dgvListaOTVista.OptionsBehavior.ReadOnly = true;
            this.dgvListaOTVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaOTVista.OptionsView.RowAutoHeight = true;
            this.dgvListaOTVista.OptionsView.ShowFooter = true;
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.Location = new System.Drawing.Point(571, 10);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(28, 30);
            this.btnCerrar2.TabIndex = 99;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // pModificar
            // 
            this.pModificar.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pModificar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pModificar.Controls.Add(this.btnRegistrar);
            this.pModificar.Controls.Add(this.groupBox2);
            this.pModificar.Controls.Add(this.groupBox12);
            this.pModificar.Controls.Add(this.label3);
            this.pModificar.Controls.Add(this.button2);
            this.pModificar.Location = new System.Drawing.Point(523, 12);
            this.pModificar.Name = "pModificar";
            this.pModificar.Size = new System.Drawing.Size(468, 500);
            this.pModificar.TabIndex = 140;
            this.pModificar.Visible = false;
            this.pModificar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pModificar_MouseMove);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.btnRegistrar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegistrar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnRegistrar.Location = new System.Drawing.Point(192, 445);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(105, 42);
            this.btnRegistrar.TabIndex = 109;
            this.btnRegistrar.Text = " Guardar";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cbxPosicionLlanta);
            this.groupBox2.Controls.Add(this.txtObservacion);
            this.groupBox2.Controls.Add(this.cbxDetalle);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbxComponente);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.groupBox2.Location = new System.Drawing.Point(13, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 256);
            this.groupBox2.TabIndex = 104;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Solicitud:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label10.Location = new System.Drawing.Point(31, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 15);
            this.label10.TabIndex = 158;
            this.label10.Text = "Observación:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label8.Location = new System.Drawing.Point(15, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 15);
            this.label8.TabIndex = 155;
            this.label8.Text = "Posición Llanta:";
            // 
            // cbxPosicionLlanta
            // 
            this.cbxPosicionLlanta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxPosicionLlanta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxPosicionLlanta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPosicionLlanta.FormattingEnabled = true;
            this.cbxPosicionLlanta.Location = new System.Drawing.Point(115, 101);
            this.cbxPosicionLlanta.Name = "cbxPosicionLlanta";
            this.cbxPosicionLlanta.Size = new System.Drawing.Size(304, 21);
            this.cbxPosicionLlanta.TabIndex = 156;
            this.cbxPosicionLlanta.SelectedIndexChanged += new System.EventHandler(this.cbxPosicionLlanta_SelectedIndexChanged);
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(115, 138);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(304, 99);
            this.txtObservacion.TabIndex = 157;
            // 
            // cbxDetalle
            // 
            this.cbxDetalle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxDetalle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxDetalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDetalle.FormattingEnabled = true;
            this.cbxDetalle.Location = new System.Drawing.Point(115, 65);
            this.cbxDetalle.Name = "cbxDetalle";
            this.cbxDetalle.Size = new System.Drawing.Size(304, 21);
            this.cbxDetalle.TabIndex = 154;
            this.cbxDetalle.SelectedIndexChanged += new System.EventHandler(this.cbxDetalle_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label2.Location = new System.Drawing.Point(60, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 148;
            this.label2.Text = "Detalle:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label6.Location = new System.Drawing.Point(28, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 15);
            this.label6.TabIndex = 38;
            this.label6.Text = "Componente:";
            // 
            // cbxComponente
            // 
            this.cbxComponente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxComponente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxComponente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxComponente.FormattingEnabled = true;
            this.cbxComponente.Location = new System.Drawing.Point(115, 28);
            this.cbxComponente.Name = "cbxComponente";
            this.cbxComponente.Size = new System.Drawing.Size(304, 21);
            this.cbxComponente.TabIndex = 153;
            this.cbxComponente.SelectedIndexChanged += new System.EventHandler(this.cbxComponente_SelectedIndexChanged);
            this.cbxComponente.DropDownClosed += new System.EventHandler(this.cbxComponente_DropDownClosed);
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groupBox12.Controls.Add(this.txtOperacion);
            this.groupBox12.Controls.Add(this.label13);
            this.groupBox12.Controls.Add(this.txtSubTipo);
            this.groupBox12.Controls.Add(this.label34);
            this.groupBox12.Controls.Add(this.label17);
            this.groupBox12.Controls.Add(this.txtPlacaUnidad);
            this.groupBox12.Controls.Add(this.label16);
            this.groupBox12.Controls.Add(this.txtTipo);
            this.groupBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.groupBox12.Location = new System.Drawing.Point(14, 59);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(438, 103);
            this.groupBox12.TabIndex = 103;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Datos de Unidad:";
            // 
            // txtOperacion
            // 
            this.txtOperacion.Location = new System.Drawing.Point(296, 28);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(120, 20);
            this.txtOperacion.TabIndex = 147;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label13.Location = new System.Drawing.Point(223, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 15);
            this.label13.TabIndex = 148;
            this.label13.Text = "Operación:";
            // 
            // txtSubTipo
            // 
            this.txtSubTipo.Location = new System.Drawing.Point(296, 64);
            this.txtSubTipo.Name = "txtSubTipo";
            this.txtSubTipo.ReadOnly = true;
            this.txtSubTipo.Size = new System.Drawing.Size(120, 20);
            this.txtSubTipo.TabIndex = 149;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label34.Location = new System.Drawing.Point(17, 30);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(41, 15);
            this.label34.TabIndex = 38;
            this.label34.Text = "Placa:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label17.Location = new System.Drawing.Point(238, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 15);
            this.label17.TabIndex = 150;
            this.label17.Text = "Subtipo:";
            // 
            // txtPlacaUnidad
            // 
            this.txtPlacaUnidad.Location = new System.Drawing.Point(64, 28);
            this.txtPlacaUnidad.Name = "txtPlacaUnidad";
            this.txtPlacaUnidad.ReadOnly = true;
            this.txtPlacaUnidad.Size = new System.Drawing.Size(120, 20);
            this.txtPlacaUnidad.TabIndex = 152;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label16.Location = new System.Drawing.Point(24, 66);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 15);
            this.label16.TabIndex = 139;
            this.label16.Text = "Tipo:";
            // 
            // txtTipo
            // 
            this.txtTipo.Location = new System.Drawing.Point(64, 64);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.ReadOnly = true;
            this.txtTipo.Size = new System.Drawing.Size(120, 20);
            this.txtTipo.TabIndex = 151;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(17, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(376, 25);
            this.label3.TabIndex = 102;
            this.label3.Text = "MODIFICAR SOLICITUD DE UNIDAD";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gainsboro;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(434, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 26);
            this.button2.TabIndex = 99;
            this.button2.Text = "X";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmComponentesDefectuosos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 515);
            this.Controls.Add(this.dtgListaComponentes);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pModificar);
            this.Controls.Add(this.pListaOT);
            this.Name = "frmComponentesDefectuosos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LISTA DE COMPONENTES DEFECTUOSOS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmComponentesDefectuosos_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaComponentes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaComponentesView)).EndInit();
            this.pListaOT.ResumeLayout(false);
            this.pListaOT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaOT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaOTVista)).EndInit();
            this.pModificar.ResumeLayout(false);
            this.pModificar.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.RadioButton rbListaPendientes;
        private System.Windows.Forms.RadioButton rbListaSolucionadas;
        private System.Windows.Forms.RadioButton rbListaTodas;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker fechaFin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker fechaInicio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlaca;
        private DevExpress.XtraGrid.GridControl dtgListaComponentes;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaComponentesView;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem asignarOTToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem terminarToolStripMenuItem;
        private System.Windows.Forms.Panel pListaOT;
        private System.Windows.Forms.Button btnBuscarOT;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPlacaOT;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraGrid.GridControl dtgListaOT;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaOTVista;
        private System.Windows.Forms.Button btnCerrar2;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.Panel pModificar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSubTipo;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPlacaUnidad;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTipo;
        public System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxComponente;
        private System.Windows.Forms.ComboBox cbxDetalle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxPosicionLlanta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtObservacion;
        public System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblTerminados;
        private System.Windows.Forms.Label lblPendientes;
        private System.Windows.Forms.Label lblporcentaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}