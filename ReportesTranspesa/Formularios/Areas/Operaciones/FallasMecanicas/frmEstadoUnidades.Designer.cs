namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    partial class frmEstadoUnidades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstadoUnidades));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rbProgramado = new System.Windows.Forms.RadioButton();
            this.rbNoProgramado = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.cbxRepuesto = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxBase = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.dtgEstadoMtto = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsProgramarSolicitud = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCambiarUbicacion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTerminarMantenimiento = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvEstadoMtto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pRegistrarProg = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label60 = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.dtpFechaProg = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnReprog = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCambioUbicaciones = new System.Windows.Forms.ToolStripButton();
            this.pAsignacionUnidad = new System.Windows.Forms.Panel();
            this.label79 = new System.Windows.Forms.Label();
            this.txtUbicacionTaller = new System.Windows.Forms.TextBox();
            this.btnGuardarUbicacion = new System.Windows.Forms.PictureBox();
            this.label78 = new System.Windows.Forms.Label();
            this.txtNuevaUbicacion = new System.Windows.Forms.TextBox();
            this.btnAgregarUbicacion = new System.Windows.Forms.PictureBox();
            this.label67 = new System.Windows.Forms.Label();
            this.cbxBase2 = new System.Windows.Forms.ComboBox();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.btnFechaAsignacion = new System.Windows.Forms.Button();
            this.label77 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.dtpFechaRecepcion = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraRecepcion = new System.Windows.Forms.DateTimePicker();
            this.lstTaller = new System.Windows.Forms.ListView();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEstadoMtto)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadoMtto)).BeginInit();
            this.pRegistrarProg.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pAsignacionUnidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGuardarUbicacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregarUbicacion)).BeginInit();
            this.groupBox19.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkRed;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1107, 45);
            this.label1.TabIndex = 17;
            this.label1.Text = "ESTADO DE UNIDADES EN MANTENIMIENTO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.groupBox14);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1107, 145);
            this.panel3.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbTodos);
            this.groupBox2.Controls.Add(this.rbProgramado);
            this.groupBox2.Controls.Add(this.rbNoProgramado);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupBox2.Location = new System.Drawing.Point(729, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 112);
            this.groupBox2.TabIndex = 120;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PROGRAMACIÓN DE VIAJE:  ";
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodos.Location = new System.Drawing.Point(16, 24);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(66, 20);
            this.rbTodos.TabIndex = 101;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.Click += new System.EventHandler(this.rbTodos_Click);
            // 
            // rbProgramado
            // 
            this.rbProgramado.AutoSize = true;
            this.rbProgramado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProgramado.Location = new System.Drawing.Point(16, 50);
            this.rbProgramado.Name = "rbProgramado";
            this.rbProgramado.Size = new System.Drawing.Size(102, 20);
            this.rbProgramado.TabIndex = 100;
            this.rbProgramado.TabStop = true;
            this.rbProgramado.Text = "Programado";
            this.rbProgramado.UseVisualStyleBackColor = true;
            this.rbProgramado.CheckedChanged += new System.EventHandler(this.rbProgramado_CheckedChanged);
            // 
            // rbNoProgramado
            // 
            this.rbNoProgramado.AutoSize = true;
            this.rbNoProgramado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNoProgramado.Location = new System.Drawing.Point(16, 76);
            this.rbNoProgramado.Name = "rbNoProgramado";
            this.rbNoProgramado.Size = new System.Drawing.Size(123, 20);
            this.rbNoProgramado.TabIndex = 99;
            this.rbNoProgramado.TabStop = true;
            this.rbNoProgramado.Text = "No Programado";
            this.rbNoProgramado.UseVisualStyleBackColor = true;
            this.rbNoProgramado.CheckedChanged += new System.EventHandler(this.rbNoProgramado_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(974, 49);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(49, 49);
            this.btnBuscar.TabIndex = 115;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(1036, 49);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(49, 49);
            this.btnExcel.TabIndex = 116;
            this.btnExcel.Tag = "6";
            this.btnExcel.Text = "\r\n";
            this.btnExcel.ToolTip = "Importar";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.cbxRepuesto);
            this.groupBox14.Controls.Add(this.label4);
            this.groupBox14.Controls.Add(this.cbxBase);
            this.groupBox14.Controls.Add(this.label3);
            this.groupBox14.Controls.Add(this.dtpFechaFin);
            this.groupBox14.Controls.Add(this.label2);
            this.groupBox14.Controls.Add(this.label40);
            this.groupBox14.Controls.Add(this.txtPlaca);
            this.groupBox14.Controls.Add(this.dtpFechaIni);
            this.groupBox14.Controls.Add(this.label41);
            this.groupBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(29, 16);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(682, 112);
            this.groupBox14.TabIndex = 113;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "FILTRO DE BÚSQUEDA: ";
            // 
            // cbxRepuesto
            // 
            this.cbxRepuesto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxRepuesto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxRepuesto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRepuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbxRepuesto.FormattingEnabled = true;
            this.cbxRepuesto.Items.AddRange(new object[] {
            "TODOS",
            "PEDIDO",
            "ENTREGADO"});
            this.cbxRepuesto.Location = new System.Drawing.Point(485, 68);
            this.cbxRepuesto.Name = "cbxRepuesto";
            this.cbxRepuesto.Size = new System.Drawing.Size(174, 24);
            this.cbxRepuesto.TabIndex = 118;
            this.cbxRepuesto.DropDownClosed += new System.EventHandler(this.cbxRepuesto_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(313, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 16);
            this.label4.TabIndex = 119;
            this.label4.Text = "Estado Requerimiento:";
            // 
            // cbxBase
            // 
            this.cbxBase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxBase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbxBase.FormattingEnabled = true;
            this.cbxBase.Location = new System.Drawing.Point(105, 68);
            this.cbxBase.Name = "cbxBase";
            this.cbxBase.Size = new System.Drawing.Size(174, 24);
            this.cbxBase.TabIndex = 118;
            this.cbxBase.SelectedIndexChanged += new System.EventHandler(this.cbxBase_SelectedIndexChanged);
            this.cbxBase.DropDownClosed += new System.EventHandler(this.cbxBase_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ubicación:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(483, 30);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Placa:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(459, 34);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(18, 16);
            this.label40.TabIndex = 4;
            this.label40.Text = "--";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(75, 30);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(99, 22);
            this.txtPlaca.TabIndex = 0;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(350, 30);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaIni.TabIndex = 2;
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(215, 32);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(129, 16);
            this.label41.TabIndex = 0;
            this.label41.Text = "Fecha Solicitada:";
            // 
            // dtgEstadoMtto
            // 
            this.dtgEstadoMtto.AllowDrop = true;
            this.dtgEstadoMtto.ContextMenuStrip = this.contextMenuStrip;
            this.dtgEstadoMtto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgEstadoMtto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgEstadoMtto.Location = new System.Drawing.Point(0, 215);
            this.dtgEstadoMtto.MainView = this.dgvEstadoMtto;
            this.dtgEstadoMtto.Name = "dtgEstadoMtto";
            this.dtgEstadoMtto.Size = new System.Drawing.Size(1107, 264);
            this.dtgEstadoMtto.TabIndex = 99;
            this.dtgEstadoMtto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvEstadoMtto});
            this.dtgEstadoMtto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgEstadoMtto_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgramarSolicitud,
            this.tsCambiarUbicacion,
            this.tsTerminarMantenimiento});
            this.contextMenuStrip.Name = "contextMenuStrip2";
            this.contextMenuStrip.Size = new System.Drawing.Size(206, 70);
            // 
            // tsProgramarSolicitud
            // 
            this.tsProgramarSolicitud.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.tsProgramarSolicitud.Name = "tsProgramarSolicitud";
            this.tsProgramarSolicitud.Size = new System.Drawing.Size(205, 22);
            this.tsProgramarSolicitud.Text = "Programar Solicitud";
            this.tsProgramarSolicitud.Click += new System.EventHandler(this.tsProgramarSolicitud_Click);
            // 
            // tsCambiarUbicacion
            // 
            this.tsCambiarUbicacion.Image = global::ReportesTranspesa.Properties.Resources.segfinal;
            this.tsCambiarUbicacion.Name = "tsCambiarUbicacion";
            this.tsCambiarUbicacion.Size = new System.Drawing.Size(205, 22);
            this.tsCambiarUbicacion.Text = "Cambiar Ubicación";
            this.tsCambiarUbicacion.Click += new System.EventHandler(this.tsCambiarUbicacion_Click);
            // 
            // tsTerminarMantenimiento
            // 
            this.tsTerminarMantenimiento.Image = global::ReportesTranspesa.Properties.Resources.liberar;
            this.tsTerminarMantenimiento.Name = "tsTerminarMantenimiento";
            this.tsTerminarMantenimiento.Size = new System.Drawing.Size(205, 22);
            this.tsTerminarMantenimiento.Text = "Terminar Mantenimiento";
            this.tsTerminarMantenimiento.Click += new System.EventHandler(this.tsTerminarMantenimiento_Click);
            // 
            // dgvEstadoMtto
            // 
            this.dgvEstadoMtto.GridControl = this.dtgEstadoMtto;
            this.dgvEstadoMtto.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvEstadoMtto.Name = "dgvEstadoMtto";
            this.dgvEstadoMtto.OptionsBehavior.Editable = false;
            this.dgvEstadoMtto.OptionsView.ColumnAutoWidth = false;
            this.dgvEstadoMtto.OptionsView.RowAutoHeight = true;
            this.dgvEstadoMtto.OptionsView.ShowFooter = true;
            this.dgvEstadoMtto.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvEstadoMtto_CustomDrawCell);
            // 
            // pRegistrarProg
            // 
            this.pRegistrarProg.BackColor = System.Drawing.Color.LemonChiffon;
            this.pRegistrarProg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRegistrarProg.Controls.Add(this.lblEstado);
            this.pRegistrarProg.Controls.Add(this.label49);
            this.pRegistrarProg.Controls.Add(this.txtObservacion);
            this.pRegistrarProg.Controls.Add(this.label50);
            this.pRegistrarProg.Controls.Add(this.label54);
            this.pRegistrarProg.Controls.Add(this.btnGuardar);
            this.pRegistrarProg.Controls.Add(this.lblTitulo);
            this.pRegistrarProg.Controls.Add(this.btnCerrar);
            this.pRegistrarProg.Controls.Add(this.label60);
            this.pRegistrarProg.Controls.Add(this.lblPlaca);
            this.pRegistrarProg.Controls.Add(this.dtpFechaProg);
            this.pRegistrarProg.Location = new System.Drawing.Point(768, 275);
            this.pRegistrarProg.Name = "pRegistrarProg";
            this.pRegistrarProg.Size = new System.Drawing.Size(339, 314);
            this.pRegistrarProg.TabIndex = 213;
            this.pRegistrarProg.Visible = false;
            this.pRegistrarProg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pRegistrarProg_MouseMove);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEstado.Location = new System.Drawing.Point(81, 81);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(151, 18);
            this.lblEstado.TabIndex = 231;
            this.lblEstado.Text = "REPROGRAMADO";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(18, 117);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(59, 18);
            this.label49.TabIndex = 230;
            this.label49.Text = "Fecha:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtObservacion.Location = new System.Drawing.Point(21, 176);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(294, 59);
            this.txtObservacion.TabIndex = 219;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(18, 151);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(108, 18);
            this.label50.TabIndex = 218;
            this.label50.Text = "Observación:";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(18, 81);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(66, 18);
            this.label54.TabIndex = 213;
            this.label54.Text = "Estado:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(119, 260);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 36);
            this.btnGuardar.TabIndex = 201;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTitulo.Location = new System.Drawing.Point(14, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(246, 22);
            this.lblTitulo.TabIndex = 29;
            this.lblTitulo.Text = "PROGRAMAR SOLICITUD";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(310, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(27, 27);
            this.btnCerrar.TabIndex = 28;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.Black;
            this.label60.Location = new System.Drawing.Point(18, 50);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(55, 18);
            this.label60.TabIndex = 225;
            this.label60.Text = "Placa:";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblPlaca.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPlaca.Location = new System.Drawing.Point(73, 50);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(72, 18);
            this.lblPlaca.TabIndex = 224;
            this.lblPlaca.Text = "ABF-999";
            // 
            // dtpFechaProg
            // 
            this.dtpFechaProg.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.dtpFechaProg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaProg.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaProg.Location = new System.Drawing.Point(83, 115);
            this.dtpFechaProg.Name = "dtpFechaProg";
            this.dtpFechaProg.Size = new System.Drawing.Size(186, 22);
            this.dtpFechaProg.TabIndex = 232;
            this.dtpFechaProg.Tag = "1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LemonChiffon;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnReprog,
            this.toolStripButton1,
            this.btnCambioUbicaciones});
            this.toolStrip1.Location = new System.Drawing.Point(0, 190);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1107, 25);
            this.toolStrip1.TabIndex = 132;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnReprog
            // 
            this.btnReprog.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.btnReprog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReprog.Name = "btnReprog";
            this.btnReprog.Size = new System.Drawing.Size(176, 22);
            this.btnReprog.Text = "Plantilla de Reprogramación";
            this.btnReprog.Click += new System.EventHandler(this.btnReprog_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCambioUbicaciones
            // 
            this.btnCambioUbicaciones.Image = global::ReportesTranspesa.Properties.Resources.segsinfin;
            this.btnCambioUbicaciones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCambioUbicaciones.Name = "btnCambioUbicaciones";
            this.btnCambioUbicaciones.Size = new System.Drawing.Size(152, 22);
            this.btnCambioUbicaciones.Text = "Cambio de Ubicaciones";
            this.btnCambioUbicaciones.Click += new System.EventHandler(this.btnCambioUbicaciones_Click);
            // 
            // pAsignacionUnidad
            // 
            this.pAsignacionUnidad.BackColor = System.Drawing.Color.LemonChiffon;
            this.pAsignacionUnidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pAsignacionUnidad.Controls.Add(this.label79);
            this.pAsignacionUnidad.Controls.Add(this.txtUbicacionTaller);
            this.pAsignacionUnidad.Controls.Add(this.btnGuardarUbicacion);
            this.pAsignacionUnidad.Controls.Add(this.label78);
            this.pAsignacionUnidad.Controls.Add(this.txtNuevaUbicacion);
            this.pAsignacionUnidad.Controls.Add(this.btnAgregarUbicacion);
            this.pAsignacionUnidad.Controls.Add(this.label67);
            this.pAsignacionUnidad.Controls.Add(this.cbxBase2);
            this.pAsignacionUnidad.Controls.Add(this.btnCerrar2);
            this.pAsignacionUnidad.Controls.Add(this.btnFechaAsignacion);
            this.pAsignacionUnidad.Controls.Add(this.label77);
            this.pAsignacionUnidad.Controls.Add(this.groupBox19);
            this.pAsignacionUnidad.Controls.Add(this.lstTaller);
            this.pAsignacionUnidad.Location = new System.Drawing.Point(805, 276);
            this.pAsignacionUnidad.Name = "pAsignacionUnidad";
            this.pAsignacionUnidad.Size = new System.Drawing.Size(301, 321);
            this.pAsignacionUnidad.TabIndex = 214;
            this.pAsignacionUnidad.Visible = false;
            this.pAsignacionUnidad.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pAsignacionUnidad_MouseMove);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label79.Location = new System.Drawing.Point(21, 170);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(41, 15);
            this.label79.TabIndex = 161;
            this.label79.Text = "Taller:";
            // 
            // txtUbicacionTaller
            // 
            this.txtUbicacionTaller.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtUbicacionTaller.Location = new System.Drawing.Point(68, 168);
            this.txtUbicacionTaller.Name = "txtUbicacionTaller";
            this.txtUbicacionTaller.Size = new System.Drawing.Size(120, 20);
            this.txtUbicacionTaller.TabIndex = 162;
            this.txtUbicacionTaller.Enter += new System.EventHandler(this.txtUbicacionTaller_Enter);
            this.txtUbicacionTaller.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUbicacionTaller_KeyPress);
            this.txtUbicacionTaller.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUbicacionTaller_KeyUp);
            this.txtUbicacionTaller.Leave += new System.EventHandler(this.txtUbicacionTaller_Leave);
            // 
            // btnGuardarUbicacion
            // 
            this.btnGuardarUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarUbicacion.Image = global::ReportesTranspesa.Properties.Resources.savemini;
            this.btnGuardarUbicacion.Location = new System.Drawing.Point(251, 221);
            this.btnGuardarUbicacion.Name = "btnGuardarUbicacion";
            this.btnGuardarUbicacion.Size = new System.Drawing.Size(23, 24);
            this.btnGuardarUbicacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnGuardarUbicacion.TabIndex = 157;
            this.btnGuardarUbicacion.TabStop = false;
            this.btnGuardarUbicacion.Visible = false;
            this.btnGuardarUbicacion.Click += new System.EventHandler(this.btnGuardarUbicacion_Click);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label78.Location = new System.Drawing.Point(21, 201);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(111, 15);
            this.label78.TabIndex = 156;
            this.label78.Text = "Agregar Ubicación:";
            this.label78.Visible = false;
            // 
            // txtNuevaUbicacion
            // 
            this.txtNuevaUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtNuevaUbicacion.Location = new System.Drawing.Point(24, 224);
            this.txtNuevaUbicacion.Name = "txtNuevaUbicacion";
            this.txtNuevaUbicacion.Size = new System.Drawing.Size(221, 20);
            this.txtNuevaUbicacion.TabIndex = 155;
            this.txtNuevaUbicacion.Visible = false;
            this.txtNuevaUbicacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNuevaUbicacion_KeyPress);
            // 
            // btnAgregarUbicacion
            // 
            this.btnAgregarUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarUbicacion.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAgregarUbicacion.Location = new System.Drawing.Point(222, 130);
            this.btnAgregarUbicacion.Name = "btnAgregarUbicacion";
            this.btnAgregarUbicacion.Size = new System.Drawing.Size(23, 24);
            this.btnAgregarUbicacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnAgregarUbicacion.TabIndex = 154;
            this.btnAgregarUbicacion.TabStop = false;
            this.btnAgregarUbicacion.Click += new System.EventHandler(this.btnAgregarUbicacion_Click);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label67.Location = new System.Drawing.Point(21, 109);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(65, 15);
            this.label67.TabIndex = 152;
            this.label67.Text = "Ubicación:";
            // 
            // cbxBase2
            // 
            this.cbxBase2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxBase2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxBase2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBase2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.cbxBase2.FormattingEnabled = true;
            this.cbxBase2.Location = new System.Drawing.Point(24, 131);
            this.cbxBase2.Name = "cbxBase2";
            this.cbxBase2.Size = new System.Drawing.Size(189, 21);
            this.cbxBase2.TabIndex = 153;
            this.cbxBase2.SelectedIndexChanged += new System.EventHandler(this.cbxBase2_SelectedIndexChanged);
            this.cbxBase2.DropDownClosed += new System.EventHandler(this.cbxBase2_DropDownClosed);
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar2.BackColor = System.Drawing.Color.Red;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.ForeColor = System.Drawing.Color.White;
            this.btnCerrar2.Location = new System.Drawing.Point(273, -2);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(28, 30);
            this.btnCerrar2.TabIndex = 101;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // btnFechaAsignacion
            // 
            this.btnFechaAsignacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFechaAsignacion.BackColor = System.Drawing.Color.DarkRed;
            this.btnFechaAsignacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechaAsignacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechaAsignacion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFechaAsignacion.Image = ((System.Drawing.Image)(resources.GetObject("btnFechaAsignacion.Image")));
            this.btnFechaAsignacion.Location = new System.Drawing.Point(101, 270);
            this.btnFechaAsignacion.Name = "btnFechaAsignacion";
            this.btnFechaAsignacion.Size = new System.Drawing.Size(102, 38);
            this.btnFechaAsignacion.TabIndex = 98;
            this.btnFechaAsignacion.Text = " Guardar";
            this.btnFechaAsignacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFechaAsignacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechaAsignacion.UseVisualStyleBackColor = false;
            this.btnFechaAsignacion.Click += new System.EventHandler(this.btnFechaAsignacion_Click);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label77.ForeColor = System.Drawing.Color.DarkRed;
            this.label77.Location = new System.Drawing.Point(8, 10);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(217, 24);
            this.label77.TabIndex = 49;
            this.label77.Text = "CAMBIAR UBICACIÓN";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.dtpFechaRecepcion);
            this.groupBox19.Controls.Add(this.dtpHoraRecepcion);
            this.groupBox19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.groupBox19.Location = new System.Drawing.Point(14, 42);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(231, 56);
            this.groupBox19.TabIndex = 102;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Fecha de Cambio:";
            // 
            // dtpFechaRecepcion
            // 
            this.dtpFechaRecepcion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaRecepcion.Location = new System.Drawing.Point(10, 23);
            this.dtpFechaRecepcion.Name = "dtpFechaRecepcion";
            this.dtpFechaRecepcion.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaRecepcion.TabIndex = 100;
            this.dtpFechaRecepcion.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpFechaRecepcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaRecepcion_KeyPress);
            // 
            // dtpHoraRecepcion
            // 
            this.dtpHoraRecepcion.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraRecepcion.Location = new System.Drawing.Point(120, 23);
            this.dtpHoraRecepcion.Name = "dtpHoraRecepcion";
            this.dtpHoraRecepcion.ShowUpDown = true;
            this.dtpHoraRecepcion.Size = new System.Drawing.Size(98, 20);
            this.dtpHoraRecepcion.TabIndex = 99;
            this.dtpHoraRecepcion.Value = new System.DateTime(2023, 5, 27, 11, 36, 15, 0);
            this.dtpHoraRecepcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpHoraRecepcion_KeyPress);
            // 
            // lstTaller
            // 
            this.lstTaller.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTaller.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTaller.ForeColor = System.Drawing.Color.Navy;
            this.lstTaller.FullRowSelect = true;
            this.lstTaller.GridLines = true;
            this.lstTaller.Location = new System.Drawing.Point(68, 187);
            this.lstTaller.MultiSelect = false;
            this.lstTaller.Name = "lstTaller";
            this.lstTaller.Size = new System.Drawing.Size(120, 96);
            this.lstTaller.TabIndex = 160;
            this.lstTaller.UseCompatibleStateImageBehavior = false;
            this.lstTaller.View = System.Windows.Forms.View.Details;
            this.lstTaller.Visible = false;
            this.lstTaller.Enter += new System.EventHandler(this.lstTaller_Enter);
            this.lstTaller.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTaller_KeyPress);
            this.lstTaller.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTaller_MouseDoubleClick);
            // 
            // frmEstadoUnidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 479);
            this.Controls.Add(this.dtgEstadoMtto);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pAsignacionUnidad);
            this.Controls.Add(this.pRegistrarProg);
            this.Name = "frmEstadoUnidades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ESTADO DE UNIDADES EN MANTENIMIENTO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEstadoUnidades_Load);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEstadoMtto)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadoMtto)).EndInit();
            this.pRegistrarProg.ResumeLayout(false);
            this.pRegistrarProg.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pAsignacionUnidad.ResumeLayout(false);
            this.pAsignacionUnidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGuardarUbicacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregarUbicacion)).EndInit();
            this.groupBox19.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.Label label41;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraGrid.GridControl dtgEstadoMtto;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvEstadoMtto;
        private System.Windows.Forms.ComboBox cbxBase;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxRepuesto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbProgramado;
        private System.Windows.Forms.RadioButton rbNoProgramado;
        private System.Windows.Forms.RadioButton rbTodos;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsProgramarSolicitud;
        private System.Windows.Forms.ToolStripMenuItem tsTerminarMantenimiento;
        private System.Windows.Forms.Panel pRegistrarProg;
        public System.Windows.Forms.Label label49;
        public System.Windows.Forms.TextBox txtObservacion;
        public System.Windows.Forms.Label label50;
        public System.Windows.Forms.Label label54;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Label lblTitulo;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.DateTimePicker dtpFechaProg;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnReprog;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton btnCambioUbicaciones;
        private System.Windows.Forms.ToolStripMenuItem tsCambiarUbicacion;
        private System.Windows.Forms.Panel pAsignacionUnidad;
        public System.Windows.Forms.Label label79;
        public System.Windows.Forms.TextBox txtUbicacionTaller;
        private System.Windows.Forms.PictureBox btnGuardarUbicacion;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.TextBox txtNuevaUbicacion;
        private System.Windows.Forms.PictureBox btnAgregarUbicacion;
        private System.Windows.Forms.Label label67;
        public System.Windows.Forms.ComboBox cbxBase2;
        private System.Windows.Forms.Button btnCerrar2;
        private System.Windows.Forms.Button btnFechaAsignacion;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.DateTimePicker dtpFechaRecepcion;
        private System.Windows.Forms.DateTimePicker dtpHoraRecepcion;
        private System.Windows.Forms.ListView lstTaller;
    }
}