namespace ReportesTranspesa.Formularios.Areas.Seguridad.ControlCapacitaciones
{
    partial class frmListarCapacitaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarCapacitaciones));
            this.dtgListaCapacitaciones = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.programarFechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaCapacitacionesVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.rbFaltantes = new System.Windows.Forms.RadioButton();
            this.rbAptos = new System.Windows.Forms.RadioButton();
            this.cbFecha = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAsistente = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInstructor = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtCapacitacion = new System.Windows.Forms.TextBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.pRegistrarGrupos = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPersona = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lstPersona = new System.Windows.Forms.ListView();
            this.lstAsistente = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAsignarGrupo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProgramar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProgramacionEspecifica = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAgregar = new System.Windows.Forms.ToolStripButton();
            this.lstTitulos = new System.Windows.Forms.ListView();
            this.pProgramacion = new System.Windows.Forms.Panel();
            this.dtpFechaProg = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraProg = new System.Windows.Forms.DateTimePicker();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCurso = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProgramarFecha = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaCapacitaciones)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCapacitacionesVista)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbFiltros.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.pRegistrarGrupos.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pProgramacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgListaCapacitaciones
            // 
            this.dtgListaCapacitaciones.CausesValidation = false;
            this.dtgListaCapacitaciones.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaCapacitaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaCapacitaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaCapacitaciones.Location = new System.Drawing.Point(20, 294);
            this.dtgListaCapacitaciones.MainView = this.dgvListaCapacitacionesVista;
            this.dtgListaCapacitaciones.Name = "dtgListaCapacitaciones";
            this.dtgListaCapacitaciones.Size = new System.Drawing.Size(1062, 255);
            this.dtgListaCapacitaciones.TabIndex = 103;
            this.dtgListaCapacitaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaCapacitacionesVista});
            this.dtgListaCapacitaciones.DoubleClick += new System.EventHandler(this.dtgListaCapacitaciones_DoubleClick);
            this.dtgListaCapacitaciones.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaCapacitaciones_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programarFechaToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 48);
            // 
            // programarFechaToolStripMenuItem
            // 
            this.programarFechaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.programarFechaToolStripMenuItem.Name = "programarFechaToolStripMenuItem";
            this.programarFechaToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.programarFechaToolStripMenuItem.Text = "Programar Fecha";
            this.programarFechaToolStripMenuItem.Click += new System.EventHandler(this.programarFechaToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar Capacitación";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dgvListaCapacitacionesVista
            // 
            this.dgvListaCapacitacionesVista.GridControl = this.dtgListaCapacitaciones;
            this.dgvListaCapacitacionesVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvListaCapacitacionesVista.Name = "dgvListaCapacitacionesVista";
            this.dgvListaCapacitacionesVista.OptionsBehavior.Editable = false;
            this.dgvListaCapacitacionesVista.OptionsBehavior.ReadOnly = true;
            this.dgvListaCapacitacionesVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaCapacitacionesVista.OptionsView.RowAutoHeight = true;
            this.dgvListaCapacitacionesVista.OptionsView.ShowFooter = true;
            this.dgvListaCapacitacionesVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaCapacitacionesVista_CustomDrawCell);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 249);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1062, 20);
            this.panel2.TabIndex = 105;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbFiltros);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1062, 189);
            this.panel1.TabIndex = 104;
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.groupBox4);
            this.gbFiltros.Controls.Add(this.rbFaltantes);
            this.gbFiltros.Controls.Add(this.rbAptos);
            this.gbFiltros.Controls.Add(this.cbFecha);
            this.gbFiltros.Controls.Add(this.groupBox3);
            this.gbFiltros.Controls.Add(this.groupBox2);
            this.gbFiltros.Controls.Add(this.groupBox1);
            this.gbFiltros.Controls.Add(this.groupBox14);
            this.gbFiltros.Controls.Add(this.btnExcel);
            this.gbFiltros.Controls.Add(this.btnBuscar);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbFiltros.Location = new System.Drawing.Point(0, 0);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(1062, 189);
            this.gbFiltros.TabIndex = 0;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Búsqueda";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbxArea);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(664, 33);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(191, 61);
            this.groupBox4.TabIndex = 120;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buscar por Área";
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(18, 24);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(154, 21);
            this.cbxArea.TabIndex = 113;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            this.cbxArea.DropDownClosed += new System.EventHandler(this.cbxArea_DropDownClosed);
            // 
            // rbFaltantes
            // 
            this.rbFaltantes.AutoSize = true;
            this.rbFaltantes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFaltantes.Location = new System.Drawing.Point(778, 141);
            this.rbFaltantes.Name = "rbFaltantes";
            this.rbFaltantes.Size = new System.Drawing.Size(68, 17);
            this.rbFaltantes.TabIndex = 119;
            this.rbFaltantes.TabStop = true;
            this.rbFaltantes.Text = "Faltantes";
            this.rbFaltantes.UseVisualStyleBackColor = true;
            this.rbFaltantes.Click += new System.EventHandler(this.rbFaltantes_Click);
            // 
            // rbAptos
            // 
            this.rbAptos.AutoSize = true;
            this.rbAptos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAptos.Location = new System.Drawing.Point(778, 114);
            this.rbAptos.Name = "rbAptos";
            this.rbAptos.Size = new System.Drawing.Size(75, 17);
            this.rbAptos.TabIndex = 118;
            this.rbAptos.TabStop = true;
            this.rbAptos.Text = "Evaluados";
            this.rbAptos.UseVisualStyleBackColor = true;
            this.rbAptos.Click += new System.EventHandler(this.rbAptos_Click);
            // 
            // cbFecha
            // 
            this.cbFecha.AutoSize = true;
            this.cbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFecha.Location = new System.Drawing.Point(411, 32);
            this.cbFecha.Name = "cbFecha";
            this.cbFecha.Size = new System.Drawing.Size(110, 17);
            this.cbFecha.TabIndex = 117;
            this.cbFecha.Text = "Buscar por Fecha";
            this.cbFecha.UseVisualStyleBackColor = true;
            this.cbFecha.CheckedChanged += new System.EventHandler(this.cbFecha_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAsistente);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(403, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(358, 61);
            this.groupBox3.TabIndex = 115;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Buscar por Asistente";
            // 
            // txtAsistente
            // 
            this.txtAsistente.Location = new System.Drawing.Point(21, 25);
            this.txtAsistente.Name = "txtAsistente";
            this.txtAsistente.Size = new System.Drawing.Size(317, 20);
            this.txtAsistente.TabIndex = 112;
            this.txtAsistente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAsistente_KeyPress);
            this.txtAsistente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAsistente_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtInstructor);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(27, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 61);
            this.groupBox2.TabIndex = 114;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar por Instructor";
            // 
            // txtInstructor
            // 
            this.txtInstructor.Location = new System.Drawing.Point(21, 25);
            this.txtInstructor.Name = "txtInstructor";
            this.txtInstructor.Size = new System.Drawing.Size(317, 20);
            this.txtInstructor.TabIndex = 112;
            this.txtInstructor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInstructor_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaIni);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(403, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 61);
            this.groupBox1.TabIndex = 113;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por Fecha";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(21, 24);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(87, 21);
            this.dtpFechaIni.TabIndex = 1;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(114, 26);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(15, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Black;
            this.metroLabel3.TabIndex = 92;
            this.metroLabel3.Text = "-";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(135, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(87, 21);
            this.dtpFechaFin.TabIndex = 2;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtCapacitacion);
            this.groupBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(27, 33);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(358, 61);
            this.groupBox14.TabIndex = 112;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar por Capacitación";
            // 
            // txtCapacitacion
            // 
            this.txtCapacitacion.Location = new System.Drawing.Point(21, 25);
            this.txtCapacitacion.Name = "txtCapacitacion";
            this.txtCapacitacion.Size = new System.Drawing.Size(317, 20);
            this.txtCapacitacion.TabIndex = 112;
            this.txtCapacitacion.Enter += new System.EventHandler(this.txtCapacitacion_Enter);
            this.txtCapacitacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapacitacion_KeyPress);
            this.txtCapacitacion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCapacitacion_KeyUp);
            this.txtCapacitacion.Leave += new System.EventHandler(this.txtCapacitacion_Leave);
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
            this.btnExcel.Location = new System.Drawing.Point(984, 75);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 7;
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
            this.btnBuscar.Location = new System.Drawing.Point(923, 75);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pRegistrarGrupos
            // 
            this.pRegistrarGrupos.BackColor = System.Drawing.SystemColors.Control;
            this.pRegistrarGrupos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRegistrarGrupos.Controls.Add(this.label1);
            this.pRegistrarGrupos.Controls.Add(this.cbxOperacion);
            this.pRegistrarGrupos.Controls.Add(this.label4);
            this.pRegistrarGrupos.Controls.Add(this.cbxGrupo);
            this.pRegistrarGrupos.Controls.Add(this.btnGuardar);
            this.pRegistrarGrupos.Controls.Add(this.txtPersona);
            this.pRegistrarGrupos.Controls.Add(this.label5);
            this.pRegistrarGrupos.Controls.Add(this.label10);
            this.pRegistrarGrupos.Controls.Add(this.btnCerrar);
            this.pRegistrarGrupos.Controls.Add(this.lstPersona);
            this.pRegistrarGrupos.Location = new System.Drawing.Point(543, 321);
            this.pRegistrarGrupos.Name = "pRegistrarGrupos";
            this.pRegistrarGrupos.Size = new System.Drawing.Size(539, 227);
            this.pRegistrarGrupos.TabIndex = 106;
            this.pRegistrarGrupos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pRegistrarGrupos_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 126;
            this.label1.Text = "Operación - Área:";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(113, 146);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(240, 21);
            this.cbxOperacion.TabIndex = 125;
            this.cbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 124;
            this.label4.Text = "Grupo:";
            // 
            // cbxGrupo
            // 
            this.cbxGrupo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxGrupo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxGrupo.FormattingEnabled = true;
            this.cbxGrupo.Location = new System.Drawing.Point(113, 104);
            this.cbxGrupo.Name = "cbxGrupo";
            this.cbxGrupo.Size = new System.Drawing.Size(240, 21);
            this.cbxGrupo.TabIndex = 123;
            this.cbxGrupo.SelectedIndexChanged += new System.EventHandler(this.cbxGrupo_SelectedIndexChanged);
            this.cbxGrupo.DropDownClosed += new System.EventHandler(this.cbxGrupo_DropDownClosed);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(403, 112);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(114, 47);
            this.btnGuardar.TabIndex = 122;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtPersona
            // 
            this.txtPersona.BackColor = System.Drawing.Color.White;
            this.txtPersona.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPersona.Location = new System.Drawing.Point(113, 64);
            this.txtPersona.MaxLength = 250;
            this.txtPersona.Name = "txtPersona";
            this.txtPersona.Size = new System.Drawing.Size(404, 20);
            this.txtPersona.TabIndex = 121;
            this.txtPersona.Enter += new System.EventHandler(this.txtPersona_Enter);
            this.txtPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersona_KeyPress);
            this.txtPersona.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPersona_KeyUp);
            this.txtPersona.Leave += new System.EventHandler(this.txtPersona_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 120;
            this.label5.Text = "Ingrese Personal:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(17, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(406, 24);
            this.label10.TabIndex = 20;
            this.label10.Text = "REGISTRO DE GRUPOS DE PERSONAL";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(501, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar.TabIndex = 18;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lstPersona
            // 
            this.lstPersona.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPersona.ForeColor = System.Drawing.Color.Navy;
            this.lstPersona.FullRowSelect = true;
            this.lstPersona.GridLines = true;
            this.lstPersona.Location = new System.Drawing.Point(113, 83);
            this.lstPersona.MultiSelect = false;
            this.lstPersona.Name = "lstPersona";
            this.lstPersona.Size = new System.Drawing.Size(404, 127);
            this.lstPersona.TabIndex = 127;
            this.lstPersona.UseCompatibleStateImageBehavior = false;
            this.lstPersona.View = System.Windows.Forms.View.Details;
            this.lstPersona.Visible = false;
            this.lstPersona.Enter += new System.EventHandler(this.lstPersona_Enter);
            this.lstPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersona_KeyPress);
            this.lstPersona.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersona_MouseDoubleClick);
            // 
            // lstAsistente
            // 
            this.lstAsistente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstAsistente.ForeColor = System.Drawing.Color.Navy;
            this.lstAsistente.FullRowSelect = true;
            this.lstAsistente.GridLines = true;
            this.lstAsistente.Location = new System.Drawing.Point(444, 208);
            this.lstAsistente.MultiSelect = false;
            this.lstAsistente.Name = "lstAsistente";
            this.lstAsistente.Size = new System.Drawing.Size(317, 127);
            this.lstAsistente.TabIndex = 128;
            this.lstAsistente.UseCompatibleStateImageBehavior = false;
            this.lstAsistente.View = System.Windows.Forms.View.Details;
            this.lstAsistente.Visible = false;
            this.lstAsistente.Enter += new System.EventHandler(this.lstAsistente_Enter);
            this.lstAsistente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstAsistente_KeyPress);
            this.lstAsistente.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAsistente_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAsignarGrupo,
            this.toolStripButton1,
            this.btnProgramar,
            this.toolStripSeparator1,
            this.btnProgramacionEspecifica,
            this.toolStripSeparator2,
            this.btnAgregar});
            this.toolStrip1.Location = new System.Drawing.Point(20, 269);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1062, 25);
            this.toolStrip1.TabIndex = 129;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAsignarGrupo
            // 
            this.btnAsignarGrupo.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.btnAsignarGrupo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAsignarGrupo.Name = "btnAsignarGrupo";
            this.btnAsignarGrupo.Size = new System.Drawing.Size(103, 22);
            this.btnAsignarGrupo.Text = "Asignar Grupo";
            this.btnAsignarGrupo.Click += new System.EventHandler(this.btnAsignarGrupo_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProgramar
            // 
            this.btnProgramar.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.btnProgramar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProgramar.Name = "btnProgramar";
            this.btnProgramar.Size = new System.Drawing.Size(136, 22);
            this.btnProgramar.Text = "Programar por Áreas";
            this.btnProgramar.Click += new System.EventHandler(this.btnProgramar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProgramacionEspecifica
            // 
            this.btnProgramacionEspecifica.Image = global::ReportesTranspesa.Properties.Resources.bonoseguridad;
            this.btnProgramacionEspecifica.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProgramacionEspecifica.Name = "btnProgramacionEspecifica";
            this.btnProgramacionEspecifica.Size = new System.Drawing.Size(152, 22);
            this.btnProgramacionEspecifica.Text = "Programar por Personal";
            this.btnProgramacionEspecifica.Click += new System.EventHandler(this.btnProgramacionEspecifica_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAgregar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(134, 22);
            this.btnAgregar.Text = "Añadir Capacitación";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lstTitulos
            // 
            this.lstTitulos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTitulos.ForeColor = System.Drawing.Color.Navy;
            this.lstTitulos.FullRowSelect = true;
            this.lstTitulos.GridLines = true;
            this.lstTitulos.Location = new System.Drawing.Point(68, 137);
            this.lstTitulos.MultiSelect = false;
            this.lstTitulos.Name = "lstTitulos";
            this.lstTitulos.Size = new System.Drawing.Size(317, 127);
            this.lstTitulos.TabIndex = 129;
            this.lstTitulos.UseCompatibleStateImageBehavior = false;
            this.lstTitulos.View = System.Windows.Forms.View.Details;
            this.lstTitulos.Visible = false;
            this.lstTitulos.Enter += new System.EventHandler(this.lstTitulos_Enter);
            this.lstTitulos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTitulos_KeyPress);
            this.lstTitulos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTitulos_MouseDoubleClick);
            // 
            // pProgramacion
            // 
            this.pProgramacion.BackColor = System.Drawing.SystemColors.Control;
            this.pProgramacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pProgramacion.Controls.Add(this.dtpFechaProg);
            this.pProgramacion.Controls.Add(this.dtpHoraProg);
            this.pProgramacion.Controls.Add(this.lblNombre);
            this.pProgramacion.Controls.Add(this.label9);
            this.pProgramacion.Controls.Add(this.lblCurso);
            this.pProgramacion.Controls.Add(this.label2);
            this.pProgramacion.Controls.Add(this.btnProgramarFecha);
            this.pProgramacion.Controls.Add(this.label6);
            this.pProgramacion.Controls.Add(this.label7);
            this.pProgramacion.Controls.Add(this.btnCerrar2);
            this.pProgramacion.Location = new System.Drawing.Point(650, 315);
            this.pProgramacion.Name = "pProgramacion";
            this.pProgramacion.Size = new System.Drawing.Size(423, 227);
            this.pProgramacion.TabIndex = 130;
            this.pProgramacion.Visible = false;
            this.pProgramacion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pProgramacion_MouseMove);
            // 
            // dtpFechaProg
            // 
            this.dtpFechaProg.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaProg.Location = new System.Drawing.Point(114, 168);
            this.dtpFechaProg.Name = "dtpFechaProg";
            this.dtpFechaProg.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaProg.TabIndex = 128;
            this.dtpFechaProg.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            // 
            // dtpHoraProg
            // 
            this.dtpHoraProg.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraProg.Location = new System.Drawing.Point(224, 168);
            this.dtpHoraProg.Name = "dtpHoraProg";
            this.dtpHoraProg.ShowUpDown = true;
            this.dtpHoraProg.Size = new System.Drawing.Size(98, 20);
            this.dtpHoraProg.TabIndex = 127;
            this.dtpHoraProg.Value = new System.DateTime(2023, 5, 27, 11, 36, 15, 0);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.Red;
            this.lblNombre.Location = new System.Drawing.Point(18, 127);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(59, 15);
            this.lblNombre.TabIndex = 126;
            this.lblNombre.Text = "TÍTULO:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(18, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(187, 16);
            this.label9.TabIndex = 125;
            this.label9.Text = "NOMBRE DE PERSONAL:";
            // 
            // lblCurso
            // 
            this.lblCurso.AutoSize = true;
            this.lblCurso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurso.ForeColor = System.Drawing.Color.Red;
            this.lblCurso.Location = new System.Drawing.Point(18, 76);
            this.lblCurso.Name = "lblCurso";
            this.lblCurso.Size = new System.Drawing.Size(59, 15);
            this.lblCurso.TabIndex = 124;
            this.lblCurso.Text = "TÍTULO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 123;
            this.label2.Text = "CURSO:";
            // 
            // btnProgramarFecha
            // 
            this.btnProgramarFecha.Appearance.BackColor = System.Drawing.Color.White;
            this.btnProgramarFecha.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnProgramarFecha.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnProgramarFecha.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProgramarFecha.Appearance.Options.UseBackColor = true;
            this.btnProgramarFecha.Appearance.Options.UseBorderColor = true;
            this.btnProgramarFecha.Appearance.Options.UseFont = true;
            this.btnProgramarFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProgramarFecha.Image = ((System.Drawing.Image)(resources.GetObject("btnProgramarFecha.Image")));
            this.btnProgramarFecha.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnProgramarFecha.Location = new System.Drawing.Point(350, 155);
            this.btnProgramarFecha.Name = "btnProgramarFecha";
            this.btnProgramarFecha.Size = new System.Drawing.Size(49, 45);
            this.btnProgramarFecha.TabIndex = 122;
            this.btnProgramarFecha.Click += new System.EventHandler(this.btnProgramarFecha_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 26);
            this.label6.TabIndex = 120;
            this.label6.Text = "Ingrese Fecha\r\nde Programación:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(17, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(217, 24);
            this.label7.TabIndex = 20;
            this.label7.Text = "PROGRAMAR FECHA";
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.Location = new System.Drawing.Point(387, 4);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar2.TabIndex = 18;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // frmListarCapacitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 569);
            this.Controls.Add(this.lstAsistente);
            this.Controls.Add(this.dtgListaCapacitaciones);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lstTitulos);
            this.Controls.Add(this.pRegistrarGrupos);
            this.Controls.Add(this.pProgramacion);
            this.Name = "frmListarCapacitaciones";
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "REGISTRO DE CAPACITACIONES - SSOMA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRegistrarCapacitacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaCapacitaciones)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCapacitacionesVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.pRegistrarGrupos.ResumeLayout(false);
            this.pRegistrarGrupos.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pProgramacion.ResumeLayout(false);
            this.pProgramacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgListaCapacitaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaCapacitacionesVista;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.GroupBox groupBox14;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TextBox txtCapacitacion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAsistente;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInstructor;
        private System.Windows.Forms.Panel pRegistrarGrupos;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtPersona;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbxOperacion;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cbxGrupo;
        private System.Windows.Forms.ListView lstPersona;
        private System.Windows.Forms.ListView lstAsistente;
        private System.Windows.Forms.CheckBox cbFecha;
        private System.Windows.Forms.RadioButton rbFaltantes;
        private System.Windows.Forms.RadioButton rbAptos;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnProgramar;
        private System.Windows.Forms.ToolStripButton btnAgregar;
        private System.Windows.Forms.ToolStripButton btnAsignarGrupo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ListView lstTitulos;
        private System.Windows.Forms.ToolStripMenuItem programarFechaToolStripMenuItem;
        private System.Windows.Forms.Panel pProgramacion;
        private DevExpress.XtraEditors.SimpleButton btnProgramarFecha;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Button btnCerrar2;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCurso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaProg;
        private System.Windows.Forms.DateTimePicker dtpHoraProg;
        private System.Windows.Forms.ToolStripButton btnProgramacionEspecifica;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}