namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmListarTardanzas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarTardanzas));
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.rbNoche = new System.Windows.Forms.RadioButton();
            this.rbTarde = new System.Windows.Forms.RadioButton();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.rbManiana = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dtgTardanzas = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsGenerarMemorandum = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvTardanzasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pTardanzasUsuario = new System.Windows.Forms.Panel();
            this.btnCerrar3 = new System.Windows.Forms.Button();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtgTardanzaUsuario = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsIngresarMotivo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEliminarMotivo = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvTardanzaUsuarioVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pDescontar = new System.Windows.Forms.Panel();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dtgTardanzasExc = new DevExpress.XtraGrid.GridControl();
            this.dtgvTardanzasExcView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTardanzas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTardanzasView)).BeginInit();
            this.pTardanzasUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTardanzaUsuario)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTardanzaUsuarioVista)).BeginInit();
            this.pDescontar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTardanzasExc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTardanzasExcView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtEmpleado);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.dtpFechaFin);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.dtpFechaInicio);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.rbNoche);
            this.panel4.Controls.Add(this.rbTarde);
            this.panel4.Controls.Add(this.dtpHoraFin);
            this.panel4.Controls.Add(this.dtpHoraInicio);
            this.panel4.Controls.Add(this.rbManiana);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(20, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(872, 84);
            this.panel4.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(349, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 217;
            this.label3.Text = "Ingresar Empleado:";
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Location = new System.Drawing.Point(352, 39);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(358, 20);
            this.txtEmpleado.TabIndex = 204;
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 216;
            this.label1.Text = "Ingresar Fechas:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(182, 39);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(140, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(163, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "--";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(21, 39);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(140, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(804, 15);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 212;
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
            this.btnBuscar.Location = new System.Drawing.Point(743, 15);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // rbNoche
            // 
            this.rbNoche.AutoSize = true;
            this.rbNoche.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNoche.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.rbNoche.Location = new System.Drawing.Point(829, 23);
            this.rbNoche.Name = "rbNoche";
            this.rbNoche.Size = new System.Drawing.Size(63, 17);
            this.rbNoche.TabIndex = 218;
            this.rbNoche.TabStop = true;
            this.rbNoche.Text = "NOCHE";
            this.rbNoche.UseVisualStyleBackColor = true;
            this.rbNoche.Visible = false;
            this.rbNoche.CheckedChanged += new System.EventHandler(this.rbNoche_CheckedChanged);
            // 
            // rbTarde
            // 
            this.rbTarde.AutoSize = true;
            this.rbTarde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTarde.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.rbTarde.Location = new System.Drawing.Point(861, 13);
            this.rbTarde.Name = "rbTarde";
            this.rbTarde.Size = new System.Drawing.Size(62, 17);
            this.rbTarde.TabIndex = 215;
            this.rbTarde.TabStop = true;
            this.rbTarde.Text = "TARDE";
            this.rbTarde.UseVisualStyleBackColor = true;
            this.rbTarde.Visible = false;
            this.rbTarde.CheckedChanged += new System.EventHandler(this.rbTarde_CheckedChanged);
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFin.Location = new System.Drawing.Point(826, 37);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.Size = new System.Drawing.Size(85, 20);
            this.dtpHoraFin.TabIndex = 221;
            this.dtpHoraFin.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpHoraFin.Visible = false;
            this.dtpHoraFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpHoraFin_KeyPress);
            // 
            // dtpHoraInicio
            // 
            this.dtpHoraInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraInicio.Location = new System.Drawing.Point(826, 52);
            this.dtpHoraInicio.Name = "dtpHoraInicio";
            this.dtpHoraInicio.Size = new System.Drawing.Size(85, 20);
            this.dtpHoraInicio.TabIndex = 220;
            this.dtpHoraInicio.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpHoraInicio.Visible = false;
            this.dtpHoraInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpHoraInicio_KeyPress);
            // 
            // rbManiana
            // 
            this.rbManiana.AutoSize = true;
            this.rbManiana.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbManiana.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.rbManiana.Location = new System.Drawing.Point(828, 12);
            this.rbManiana.Name = "rbManiana";
            this.rbManiana.Size = new System.Drawing.Size(71, 17);
            this.rbManiana.TabIndex = 214;
            this.rbManiana.TabStop = true;
            this.rbManiana.Text = "MAÑANA";
            this.rbManiana.UseVisualStyleBackColor = true;
            this.rbManiana.Visible = false;
            this.rbManiana.CheckedChanged += new System.EventHandler(this.rbManiana_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(830, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 219;
            this.label4.Text = "Seleccionar Turno:";
            this.label4.Visible = false;
            // 
            // dtgTardanzas
            // 
            this.dtgTardanzas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgTardanzas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTardanzas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTardanzas.Location = new System.Drawing.Point(20, 144);
            this.dtgTardanzas.LookAndFeel.SkinName = "Darkroom";
            this.dtgTardanzas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTardanzas.MainView = this.dtgvTardanzasView;
            this.dtgTardanzas.Name = "dtgTardanzas";
            this.dtgTardanzas.Size = new System.Drawing.Size(872, 425);
            this.dtgTardanzas.TabIndex = 15;
            this.dtgTardanzas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvTardanzasView});
            this.dtgTardanzas.DoubleClick += new System.EventHandler(this.dtgTardanzas_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsGenerarMemorandum});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(196, 26);
            // 
            // tsGenerarMemorandum
            // 
            this.tsGenerarMemorandum.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.tsGenerarMemorandum.Name = "tsGenerarMemorandum";
            this.tsGenerarMemorandum.Size = new System.Drawing.Size(195, 22);
            this.tsGenerarMemorandum.Text = "Generar Memorandum";
            // 
            // dtgvTardanzasView
            // 
            this.dtgvTardanzasView.GridControl = this.dtgTardanzas;
            this.dtgvTardanzasView.Name = "dtgvTardanzasView";
            this.dtgvTardanzasView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvTardanzasView.OptionsBehavior.Editable = false;
            this.dtgvTardanzasView.OptionsView.ColumnAutoWidth = false;
            // 
            // pTardanzasUsuario
            // 
            this.pTardanzasUsuario.Controls.Add(this.btnCerrar3);
            this.pTardanzasUsuario.Controls.Add(this.lblEmpleado);
            this.pTardanzasUsuario.Controls.Add(this.label7);
            this.pTardanzasUsuario.Controls.Add(this.dtgTardanzaUsuario);
            this.pTardanzasUsuario.Controls.Add(this.pDescontar);
            this.pTardanzasUsuario.ForeColor = System.Drawing.Color.Crimson;
            this.pTardanzasUsuario.Location = new System.Drawing.Point(147, 187);
            this.pTardanzasUsuario.Name = "pTardanzasUsuario";
            this.pTardanzasUsuario.Size = new System.Drawing.Size(656, 321);
            this.pTardanzasUsuario.TabIndex = 16;
            this.pTardanzasUsuario.Visible = false;
            this.pTardanzasUsuario.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pTardanzasUsuario_MouseMove);
            // 
            // btnCerrar3
            // 
            this.btnCerrar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar3.BackColor = System.Drawing.Color.Red;
            this.btnCerrar3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar3.ForeColor = System.Drawing.Color.White;
            this.btnCerrar3.Location = new System.Drawing.Point(631, 1);
            this.btnCerrar3.Name = "btnCerrar3";
            this.btnCerrar3.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar3.TabIndex = 219;
            this.btnCerrar3.Text = "X";
            this.btnCerrar3.UseVisualStyleBackColor = false;
            this.btnCerrar3.Click += new System.EventHandler(this.btnCerrar3_Click);
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblEmpleado.Location = new System.Drawing.Point(19, 44);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(89, 20);
            this.lblEmpleado.TabIndex = 218;
            this.lblEmpleado.Text = "Empleado";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(19, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(316, 20);
            this.label7.TabIndex = 217;
            this.label7.Text = "LISTA DE TARDANZAS DEL EMPLEADO:";
            // 
            // dtgTardanzaUsuario
            // 
            this.dtgTardanzaUsuario.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgTardanzaUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgTardanzaUsuario.Location = new System.Drawing.Point(0, 79);
            this.dtgTardanzaUsuario.LookAndFeel.SkinName = "Darkroom";
            this.dtgTardanzaUsuario.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTardanzaUsuario.MainView = this.dgvTardanzaUsuarioVista;
            this.dtgTardanzaUsuario.Name = "dtgTardanzaUsuario";
            this.dtgTardanzaUsuario.Size = new System.Drawing.Size(656, 242);
            this.dtgTardanzaUsuario.TabIndex = 16;
            this.dtgTardanzaUsuario.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTardanzaUsuarioVista});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsIngresarMotivo,
            this.tsEliminarMotivo});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(159, 48);
            // 
            // tsIngresarMotivo
            // 
            this.tsIngresarMotivo.Image = global::ReportesTranspesa.Properties.Resources.liberardoc;
            this.tsIngresarMotivo.Name = "tsIngresarMotivo";
            this.tsIngresarMotivo.Size = new System.Drawing.Size(158, 22);
            this.tsIngresarMotivo.Text = "Ingresar Motivo";
            this.tsIngresarMotivo.Click += new System.EventHandler(this.tsJustificacion_Click);
            // 
            // tsEliminarMotivo
            // 
            this.tsEliminarMotivo.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarMotivo.Name = "tsEliminarMotivo";
            this.tsEliminarMotivo.Size = new System.Drawing.Size(158, 22);
            this.tsEliminarMotivo.Text = "Eliminar Motivo";
            this.tsEliminarMotivo.Click += new System.EventHandler(this.tsEliminarMotivo_Click);
            // 
            // dgvTardanzaUsuarioVista
            // 
            this.dgvTardanzaUsuarioVista.GridControl = this.dtgTardanzaUsuario;
            this.dgvTardanzaUsuarioVista.Name = "dgvTardanzaUsuarioVista";
            this.dgvTardanzaUsuarioVista.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvTardanzaUsuarioVista.OptionsBehavior.Editable = false;
            this.dgvTardanzaUsuarioVista.OptionsView.ColumnAutoWidth = false;
            this.dgvTardanzaUsuarioVista.OptionsView.ShowGroupPanel = false;
            // 
            // pDescontar
            // 
            this.pDescontar.Controls.Add(this.btnAgregar);
            this.pDescontar.Controls.Add(this.txtMotivo);
            this.pDescontar.Controls.Add(this.btnCerrar2);
            this.pDescontar.Controls.Add(this.label9);
            this.pDescontar.Location = new System.Drawing.Point(121, 72);
            this.pDescontar.Name = "pDescontar";
            this.pDescontar.Size = new System.Drawing.Size(412, 187);
            this.pDescontar.TabIndex = 220;
            this.pDescontar.Visible = false;
            this.pDescontar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDescontar_MouseMove);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.btnAgregar.Location = new System.Drawing.Point(153, 135);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(107, 35);
            this.btnAgregar.TabIndex = 221;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtMotivo
            // 
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.Location = new System.Drawing.Point(18, 52);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(376, 66);
            this.txtMotivo.TabIndex = 220;
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar2.BackColor = System.Drawing.Color.Red;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.ForeColor = System.Drawing.Color.White;
            this.btnCerrar2.Location = new System.Drawing.Point(387, 1);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar2.TabIndex = 219;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(14, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 20);
            this.label9.TabIndex = 217;
            this.label9.Text = "Ingresar Motivo:";
            // 
            // dtgTardanzasExc
            // 
            this.dtgTardanzasExc.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgTardanzasExc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTardanzasExc.Location = new System.Drawing.Point(558, 150);
            this.dtgTardanzasExc.LookAndFeel.SkinName = "Darkroom";
            this.dtgTardanzasExc.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTardanzasExc.MainView = this.dtgvTardanzasExcView;
            this.dtgTardanzasExc.Name = "dtgTardanzasExc";
            this.dtgTardanzasExc.Size = new System.Drawing.Size(370, 190);
            this.dtgTardanzasExc.TabIndex = 17;
            this.dtgTardanzasExc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvTardanzasExcView});
            this.dtgTardanzasExc.Visible = false;
            // 
            // dtgvTardanzasExcView
            // 
            this.dtgvTardanzasExcView.GridControl = this.dtgTardanzasExc;
            this.dtgvTardanzasExcView.Name = "dtgvTardanzasExcView";
            this.dtgvTardanzasExcView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvTardanzasExcView.OptionsBehavior.Editable = false;
            this.dtgvTardanzasExcView.OptionsView.ColumnAutoWidth = false;
            // 
            // frmListarTardanzas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 589);
            this.Controls.Add(this.dtgTardanzas);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dtgTardanzasExc);
            this.Controls.Add(this.pTardanzasUsuario);
            this.Name = "frmListarTardanzas";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "REGISTRO DE TARDANZAS";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.frmListarTardanzas_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTardanzas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTardanzasView)).EndInit();
            this.pTardanzasUsuario.ResumeLayout(false);
            this.pTardanzasUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTardanzaUsuario)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTardanzaUsuarioVista)).EndInit();
            this.pDescontar.ResumeLayout(false);
            this.pDescontar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTardanzasExc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTardanzasExcView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.TextBox txtEmpleado;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgTardanzas;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvTardanzasView;
        private System.Windows.Forms.RadioButton rbNoche;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbTarde;
        private System.Windows.Forms.RadioButton rbManiana;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsGenerarMemorandum;
        public System.Windows.Forms.DateTimePicker dtpHoraFin;
        public System.Windows.Forms.DateTimePicker dtpHoraInicio;
        private System.Windows.Forms.Panel pTardanzasUsuario;
        private DevExpress.XtraGrid.GridControl dtgTardanzaUsuario;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTardanzaUsuarioVista;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCerrar3;
        private System.Windows.Forms.Panel pDescontar;
        public System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Button btnCerrar2;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsIngresarMotivo;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarMotivo;
        private DevExpress.XtraGrid.GridControl dtgTardanzasExc;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvTardanzasExcView;
    }
}