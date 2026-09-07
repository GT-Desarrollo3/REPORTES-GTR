namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmAsistenciaRegistrarNoche
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsistenciaRegistrarNoche));
            this.lstFiltroRelacion = new System.Windows.Forms.ListView();
            this.txtRelacion = new System.Windows.Forms.TextBox();
            this.lblRelacion = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnLimpiar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.dtgNoches = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.autorizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingresarPlanillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ingresarFCompToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarNocheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvNochesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.autorizarCompToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pCompensar = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodGasto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.dtpFechaComp = new System.Windows.Forms.DateTimePicker();
            this.lblNoche = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lstPlanillas = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNoches)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNochesView)).BeginInit();
            this.pCompensar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstFiltroRelacion
            // 
            this.lstFiltroRelacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstFiltroRelacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFiltroRelacion.ForeColor = System.Drawing.Color.Navy;
            this.lstFiltroRelacion.FullRowSelect = true;
            this.lstFiltroRelacion.GridLines = true;
            this.lstFiltroRelacion.Location = new System.Drawing.Point(17, 47);
            this.lstFiltroRelacion.MultiSelect = false;
            this.lstFiltroRelacion.Name = "lstFiltroRelacion";
            this.lstFiltroRelacion.Size = new System.Drawing.Size(337, 10);
            this.lstFiltroRelacion.TabIndex = 30;
            this.lstFiltroRelacion.UseCompatibleStateImageBehavior = false;
            this.lstFiltroRelacion.View = System.Windows.Forms.View.Details;
            this.lstFiltroRelacion.Visible = false;
            this.lstFiltroRelacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstFiltroRelacion_KeyPress);
            // 
            // txtRelacion
            // 
            this.txtRelacion.Location = new System.Drawing.Point(17, 28);
            this.txtRelacion.MaxLength = 200;
            this.txtRelacion.Name = "txtRelacion";
            this.txtRelacion.Size = new System.Drawing.Size(337, 20);
            this.txtRelacion.TabIndex = 29;
            this.txtRelacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRelacion_KeyPress);
            // 
            // lblRelacion
            // 
            this.lblRelacion.AutoSize = true;
            this.lblRelacion.Location = new System.Drawing.Point(12, 12);
            this.lblRelacion.Name = "lblRelacion";
            this.lblRelacion.Size = new System.Drawing.Size(114, 13);
            this.lblRelacion.TabIndex = 32;
            this.lblRelacion.Text = "Nombre de Conductor:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(17, 84);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(98, 20);
            this.dtpFecha.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Cantidad Noche:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1.00",
            "0.50"});
            this.comboBox1.Location = new System.Drawing.Point(148, 84);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(98, 21);
            this.comboBox1.TabIndex = 37;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnLimpiar);
            this.splitContainer1.Panel1.Controls.Add(this.btnGuardar);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtObservaciones);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFecha);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lblRelacion);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtRelacion);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lstFiltroRelacion);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgNoches);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Size = new System.Drawing.Size(576, 452);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 38;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnLimpiar.Appearance.Options.UseFont = true;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.Location = new System.Drawing.Point(403, 102);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(98, 40);
            this.btnLimpiar.TabIndex = 119;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(403, 45);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 40);
            this.btnGuardar.TabIndex = 118;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Observación:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(17, 138);
            this.txtObservaciones.MaxLength = 50;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(337, 20);
            this.txtObservaciones.TabIndex = 38;
            // 
            // dtgNoches
            // 
            this.dtgNoches.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgNoches.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgNoches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgNoches.Location = new System.Drawing.Point(0, 25);
            this.dtgNoches.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.dtgNoches.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.MidnightBlue;
            this.dtgNoches.LookAndFeel.SkinName = "Blue";
            this.dtgNoches.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgNoches.MainView = this.dgvNochesView;
            this.dtgNoches.Name = "dtgNoches";
            this.dtgNoches.Size = new System.Drawing.Size(576, 253);
            this.dtgNoches.TabIndex = 136;
            this.dtgNoches.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvNochesView});
            this.dtgNoches.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgNoches_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autorizarToolStripMenuItem,
            this.ingresarPlanillaToolStripMenuItem,
            this.toolStripSeparator1,
            this.ingresarFCompToolStripMenuItem,
            this.toolStripSeparator2,
            this.eliminarNocheToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 104);
            // 
            // autorizarToolStripMenuItem
            // 
            this.autorizarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.liberar;
            this.autorizarToolStripMenuItem.Name = "autorizarToolStripMenuItem";
            this.autorizarToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.autorizarToolStripMenuItem.Text = "Autorizar Pago";
            this.autorizarToolStripMenuItem.Click += new System.EventHandler(this.autorizarToolStripMenuItem_Click);
            // 
            // ingresarPlanillaToolStripMenuItem
            // 
            this.ingresarPlanillaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.ingresarPlanillaToolStripMenuItem.Name = "ingresarPlanillaToolStripMenuItem";
            this.ingresarPlanillaToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.ingresarPlanillaToolStripMenuItem.Text = "Ingresar Planilla";
            this.ingresarPlanillaToolStripMenuItem.Click += new System.EventHandler(this.ingresarPlanillaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // ingresarFCompToolStripMenuItem
            // 
            this.ingresarFCompToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.ingresarFCompToolStripMenuItem.Name = "ingresarFCompToolStripMenuItem";
            this.ingresarFCompToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.ingresarFCompToolStripMenuItem.Text = "Ingresar Fecha Comp.";
            this.ingresarFCompToolStripMenuItem.Click += new System.EventHandler(this.ingresarFCompToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // eliminarNocheToolStripMenuItem
            // 
            this.eliminarNocheToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarNocheToolStripMenuItem.Name = "eliminarNocheToolStripMenuItem";
            this.eliminarNocheToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.eliminarNocheToolStripMenuItem.Text = "Eliminar Noche";
            this.eliminarNocheToolStripMenuItem.Click += new System.EventHandler(this.eliminarNocheToolStripMenuItem_Click);
            // 
            // dgvNochesView
            // 
            this.dgvNochesView.GridControl = this.dtgNoches;
            this.dgvNochesView.Name = "dgvNochesView";
            this.dgvNochesView.OptionsBehavior.Editable = false;
            this.dgvNochesView.OptionsView.ColumnAutoWidth = false;
            this.dgvNochesView.OptionsView.RowAutoHeight = true;
            this.dgvNochesView.OptionsView.ShowGroupPanel = false;
            this.dgvNochesView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvNochesView_CustomDrawCell);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(576, 25);
            this.label5.TabIndex = 122;
            this.label5.Text = " Noches Registradas:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MidnightBlue;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Khaki;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(576, 40);
            this.label4.TabIndex = 121;
            this.label4.Text = "REGISTRAR NOCHES";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // autorizarCompToolStripMenuItem
            // 
            this.autorizarCompToolStripMenuItem.Name = "autorizarCompToolStripMenuItem";
            this.autorizarCompToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.autorizarCompToolStripMenuItem.Text = "Autorizar Comp.";
            // 
            // pCompensar
            // 
            this.pCompensar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pCompensar.Controls.Add(this.label11);
            this.pCompensar.Controls.Add(this.btnActualizar);
            this.pCompensar.Controls.Add(this.dtpFechaComp);
            this.pCompensar.Controls.Add(this.lblNoche);
            this.pCompensar.Controls.Add(this.label15);
            this.pCompensar.Controls.Add(this.lblFecha);
            this.pCompensar.Controls.Add(this.label12);
            this.pCompensar.Controls.Add(this.btnCerrar);
            this.pCompensar.Controls.Add(this.lblTitulo);
            this.pCompensar.Controls.Add(this.txtCodGasto);
            this.pCompensar.Controls.Add(this.label6);
            this.pCompensar.Controls.Add(this.lstPlanillas);
            this.pCompensar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.pCompensar.Location = new System.Drawing.Point(102, 138);
            this.pCompensar.Name = "pCompensar";
            this.pCompensar.Size = new System.Drawing.Size(372, 218);
            this.pCompensar.TabIndex = 214;
            this.pCompensar.Visible = false;
            this.pCompensar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pCompensar_MouseMove);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(62, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 15);
            this.label6.TabIndex = 191;
            this.label6.Text = "Ingresar Planilla:";
            // 
            // txtCodGasto
            // 
            this.txtCodGasto.Location = new System.Drawing.Point(167, 83);
            this.txtCodGasto.MaxLength = 50;
            this.txtCodGasto.Name = "txtCodGasto";
            this.txtCodGasto.Size = new System.Drawing.Size(104, 22);
            this.txtCodGasto.TabIndex = 190;
            this.txtCodGasto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodGasto_KeyPress);
            this.txtCodGasto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodGasto_KeyUp);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.Location = new System.Drawing.Point(14, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 15);
            this.label11.TabIndex = 189;
            this.label11.Text = "Fecha de Compensación:";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActualizar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnActualizar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnActualizar.Appearance.Options.UseBackColor = true;
            this.btnActualizar.Appearance.Options.UseBorderColor = true;
            this.btnActualizar.Appearance.Options.UseFont = true;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(143, 164);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(98, 40);
            this.btnActualizar.TabIndex = 142;
            this.btnActualizar.Tag = "5";
            this.btnActualizar.Text = "Confirmar";
            this.btnActualizar.ToolTip = "Confirmar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // dtpFechaComp
            // 
            this.dtpFechaComp.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaComp.CalendarForeColor = System.Drawing.Color.Blue;
            this.dtpFechaComp.CalendarMonthBackground = System.Drawing.Color.LightCyan;
            this.dtpFechaComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaComp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaComp.Location = new System.Drawing.Point(167, 122);
            this.dtpFechaComp.Name = "dtpFechaComp";
            this.dtpFechaComp.Size = new System.Drawing.Size(104, 21);
            this.dtpFechaComp.TabIndex = 181;
            this.dtpFechaComp.Tag = "1";
            // 
            // lblNoche
            // 
            this.lblNoche.AutoSize = true;
            this.lblNoche.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblNoche.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblNoche.Location = new System.Drawing.Point(243, 49);
            this.lblNoche.Name = "lblNoche";
            this.lblNoche.Size = new System.Drawing.Size(98, 18);
            this.lblNoche.TabIndex = 140;
            this.lblNoche.Text = "MIÉRCOLES";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(179, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 18);
            this.label15.TabIndex = 139;
            this.label15.Text = "NOCHE: ";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblFecha.Location = new System.Drawing.Point(73, 49);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(80, 18);
            this.lblFecha.TabIndex = 138;
            this.lblFecha.Text = "99/99/9999";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(14, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 18);
            this.label12.TabIndex = 137;
            this.label12.Text = "FECHA: ";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(336, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(29, 29);
            this.btnCerrar.TabIndex = 133;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitulo.Location = new System.Drawing.Point(13, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(103, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "INGRESAR";
            // 
            // lstPlanillas
            // 
            this.lstPlanillas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlanillas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlanillas.ForeColor = System.Drawing.Color.Navy;
            this.lstPlanillas.FullRowSelect = true;
            this.lstPlanillas.GridLines = true;
            this.lstPlanillas.Location = new System.Drawing.Point(31, 104);
            this.lstPlanillas.MultiSelect = false;
            this.lstPlanillas.Name = "lstPlanillas";
            this.lstPlanillas.Size = new System.Drawing.Size(309, 105);
            this.lstPlanillas.TabIndex = 192;
            this.lstPlanillas.UseCompatibleStateImageBehavior = false;
            this.lstPlanillas.View = System.Windows.Forms.View.Details;
            this.lstPlanillas.Visible = false;
            this.lstPlanillas.Enter += new System.EventHandler(this.lstPlanillas_Enter);
            this.lstPlanillas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlanillas_KeyPress);
            this.lstPlanillas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlanillas_MouseDoubleClick);
            // 
            // frmAsistenciaRegistrarNoche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(576, 492);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pCompensar);
            this.MaximizeBox = false;
            this.Name = "frmAsistenciaRegistrarNoche";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRAR NOCHE";
            this.Load += new System.EventHandler(this.frmAsistenciaRegistrarNoche_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgNoches)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNochesView)).EndInit();
            this.pCompensar.ResumeLayout(false);
            this.pCompensar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstFiltroRelacion;
        private System.Windows.Forms.TextBox txtRelacion;
        private System.Windows.Forms.Label lblRelacion;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton btnLimpiar;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraGrid.GridControl dtgNoches;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvNochesView;
        private System.Windows.Forms.ToolStripMenuItem autorizarCompToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem autorizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ingresarPlanillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ingresarFCompToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem eliminarNocheToolStripMenuItem;
        private System.Windows.Forms.Panel pCompensar;
        public DevExpress.XtraEditors.SimpleButton btnActualizar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpFechaComp;
        private System.Windows.Forms.Label lblNoche;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCodGasto;
        private System.Windows.Forms.ListView lstPlanillas;
    }
}