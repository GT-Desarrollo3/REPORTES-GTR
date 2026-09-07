namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmProgramarVacaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgramarVacaciones));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtReemplazo = new MetroFramework.Controls.MetroTextBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.cbActivar = new System.Windows.Forms.CheckBox();
            this.txtEmpleado = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.cbTodos = new System.Windows.Forms.CheckBox();
            this.btnArea = new System.Windows.Forms.Button();
            this.txtAnticipacion = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.lblDias = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgListaVacaciones = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvListaVacacionesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dtgExportar = new DevExpress.XtraGrid.GridControl();
            this.dtgvExportarView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgPendientesGoce = new DevExpress.XtraGrid.GridControl();
            this.dtgvPendientesGoceView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.dtgListaAreas = new System.Windows.Forms.DataGridView();
            this.Marca = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lvReemplazo = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaVacaciones)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaVacacionesView)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgExportar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvExportarView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPendientesGoce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPendientesGoceView)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaAreas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtReemplazo);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.cbActivar);
            this.panel1.Controls.Add(this.txtEmpleado);
            this.panel1.Controls.Add(this.metroLabel10);
            this.panel1.Controls.Add(this.metroLabel8);
            this.panel1.Controls.Add(this.cbTodos);
            this.panel1.Controls.Add(this.btnArea);
            this.panel1.Controls.Add(this.txtAnticipacion);
            this.panel1.Controls.Add(this.metroLabel6);
            this.panel1.Controls.Add(this.metroLabel5);
            this.panel1.Controls.Add(this.metroLabel4);
            this.panel1.Controls.Add(this.lblDias);
            this.panel1.Controls.Add(this.metroLabel3);
            this.panel1.Controls.Add(this.metroLabel2);
            this.panel1.Controls.Add(this.dtpFechaFin);
            this.panel1.Controls.Add(this.dtpFechaIni);
            this.panel1.Controls.Add(this.metroLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1480, 166);
            this.panel1.TabIndex = 0;
            // 
            // txtReemplazo
            // 
            this.txtReemplazo.Lines = new string[0];
            this.txtReemplazo.Location = new System.Drawing.Point(100, 114);
            this.txtReemplazo.MaxLength = 32767;
            this.txtReemplazo.Name = "txtReemplazo";
            this.txtReemplazo.PasswordChar = '\0';
            this.txtReemplazo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtReemplazo.SelectedText = "";
            this.txtReemplazo.Size = new System.Drawing.Size(455, 29);
            this.txtReemplazo.Style = MetroFramework.MetroColorStyle.Red;
            this.txtReemplazo.TabIndex = 126;
            this.txtReemplazo.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtReemplazo.UseSelectable = true;
            this.txtReemplazo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReemplazo_KeyPress);
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
            this.btnBuscar.Location = new System.Drawing.Point(854, 105);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(39, 43);
            this.btnBuscar.TabIndex = 119;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardar.Location = new System.Drawing.Point(750, 51);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(51, 55);
            this.btnGuardar.TabIndex = 118;
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
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
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(909, 105);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(39, 43);
            this.btnExcel.TabIndex = 120;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // cbActivar
            // 
            this.cbActivar.AutoSize = true;
            this.cbActivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbActivar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbActivar.Location = new System.Drawing.Point(575, 118);
            this.cbActivar.Name = "cbActivar";
            this.cbActivar.Size = new System.Drawing.Size(66, 20);
            this.cbActivar.TabIndex = 115;
            this.cbActivar.Text = "Alertar";
            this.cbActivar.UseVisualStyleBackColor = true;
            this.cbActivar.CheckedChanged += new System.EventHandler(this.cbActivar_CheckedChanged);
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Lines = new string[0];
            this.txtEmpleado.Location = new System.Drawing.Point(972, 119);
            this.txtEmpleado.MaxLength = 32767;
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.PasswordChar = '\0';
            this.txtEmpleado.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmpleado.SelectedText = "";
            this.txtEmpleado.Size = new System.Drawing.Size(315, 29);
            this.txtEmpleado.Style = MetroFramework.MetroColorStyle.Red;
            this.txtEmpleado.TabIndex = 84;
            this.txtEmpleado.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmpleado.UseSelectable = true;
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.ForeColor = System.Drawing.Color.Transparent;
            this.metroLabel10.Location = new System.Drawing.Point(972, 94);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(72, 19);
            this.metroLabel10.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel10.TabIndex = 83;
            this.metroLabel10.Text = "Empleado:";
            this.metroLabel10.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel8.ForeColor = System.Drawing.Color.Transparent;
            this.metroLabel8.Location = new System.Drawing.Point(16, 118);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(78, 19);
            this.metroLabel8.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel8.TabIndex = 125;
            this.metroLabel8.Text = "Reemplazo:";
            this.metroLabel8.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // cbTodos
            // 
            this.cbTodos.AutoSize = true;
            this.cbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTodos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbTodos.Location = new System.Drawing.Point(185, 70);
            this.cbTodos.Name = "cbTodos";
            this.cbTodos.Size = new System.Drawing.Size(126, 20);
            this.cbTodos.TabIndex = 124;
            this.cbTodos.Text = "Todas las áreas";
            this.cbTodos.UseVisualStyleBackColor = true;
            this.cbTodos.CheckedChanged += new System.EventHandler(this.cbTodos_CheckedChanged);
            // 
            // btnArea
            // 
            this.btnArea.Location = new System.Drawing.Point(131, 68);
            this.btnArea.Name = "btnArea";
            this.btnArea.Size = new System.Drawing.Size(39, 23);
            this.btnArea.TabIndex = 123;
            this.btnArea.Text = "...";
            this.btnArea.UseVisualStyleBackColor = true;
            this.btnArea.Click += new System.EventHandler(this.btnArea_Click);
            // 
            // txtAnticipacion
            // 
            this.txtAnticipacion.Lines = new string[0];
            this.txtAnticipacion.Location = new System.Drawing.Point(553, 63);
            this.txtAnticipacion.MaxLength = 32767;
            this.txtAnticipacion.Name = "txtAnticipacion";
            this.txtAnticipacion.PasswordChar = '\0';
            this.txtAnticipacion.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAnticipacion.SelectedText = "";
            this.txtAnticipacion.Size = new System.Drawing.Size(88, 29);
            this.txtAnticipacion.Style = MetroFramework.MetroColorStyle.Red;
            this.txtAnticipacion.TabIndex = 117;
            this.txtAnticipacion.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAnticipacion.UseSelectable = true;
            this.txtAnticipacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnticipacion_KeyPress);
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.Location = new System.Drawing.Point(414, 68);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(133, 19);
            this.metroLabel6.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel6.TabIndex = 116;
            this.metroLabel6.Text = "Días de anticipación:";
            this.metroLabel6.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(16, 69);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(109, 19);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel5.TabIndex = 92;
            this.metroLabel5.Text = "Seleccionar área:";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel4.Location = new System.Drawing.Point(516, 21);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(60, 25);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel4.TabIndex = 91;
            this.metroLabel4.Text = "día(s)";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblDias
            // 
            this.lblDias.AutoSize = true;
            this.lblDias.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblDias.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblDias.Location = new System.Drawing.Point(485, 21);
            this.lblDias.Name = "lblDias";
            this.lblDias.Size = new System.Drawing.Size(0, 0);
            this.lblDias.Style = MetroFramework.MetroColorStyle.Red;
            this.lblDias.TabIndex = 90;
            this.lblDias.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(414, 24);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(71, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 89;
            this.metroLabel3.Text = "Total Días:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(223, 24);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(69, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 88;
            this.metroLabel2.Text = "Fecha Fin:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(298, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(87, 22);
            this.dtpFechaFin.TabIndex = 87;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.ValueChanged += new System.EventHandler(this.dtpFechaFin_ValueChanged);
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(105, 24);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(87, 22);
            this.dtpFechaIni.TabIndex = 86;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.ValueChanged += new System.EventHandler(this.dtpFechaIni_ValueChanged);
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(16, 24);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(83, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 85;
            this.metroLabel1.Text = "Fecha Inicio:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgListaVacaciones);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(856, 226);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(644, 424);
            this.panel2.TabIndex = 86;
            // 
            // dtgListaVacaciones
            // 
            this.dtgListaVacaciones.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgListaVacaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaVacaciones.Location = new System.Drawing.Point(0, 0);
            this.dtgListaVacaciones.LookAndFeel.SkinMaskColor = System.Drawing.Color.Cyan;
            this.dtgListaVacaciones.LookAndFeel.SkinName = "Office 2010 Blue";
            this.dtgListaVacaciones.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaVacaciones.MainView = this.dtgvListaVacacionesView;
            this.dtgListaVacaciones.Name = "dtgListaVacaciones";
            this.dtgListaVacaciones.Size = new System.Drawing.Size(644, 424);
            this.dtgListaVacaciones.TabIndex = 2;
            this.dtgListaVacaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvListaVacacionesView});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(118, 26);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dtgvListaVacacionesView
            // 
            this.dtgvListaVacacionesView.GridControl = this.dtgListaVacaciones;
            this.dtgvListaVacacionesView.Name = "dtgvListaVacacionesView";
            this.dtgvListaVacacionesView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvListaVacacionesView.OptionsBehavior.Editable = false;
            this.dtgvListaVacacionesView.OptionsView.ColumnAutoWidth = false;
            this.dtgvListaVacacionesView.OptionsView.ShowGroupPanel = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(20, 226);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(836, 424);
            this.panel4.TabIndex = 89;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dtgPendientesGoce);
            this.panel6.Controls.Add(this.dtgExportar);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 50);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(836, 374);
            this.panel6.TabIndex = 5;
            // 
            // dtgExportar
            // 
            this.dtgExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgExportar.Location = new System.Drawing.Point(70, 89);
            this.dtgExportar.MainView = this.dtgvExportarView;
            this.dtgExportar.Name = "dtgExportar";
            this.dtgExportar.Size = new System.Drawing.Size(359, 184);
            this.dtgExportar.TabIndex = 102;
            this.dtgExportar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvExportarView});
            // 
            // dtgvExportarView
            // 
            this.dtgvExportarView.GridControl = this.dtgExportar;
            this.dtgvExportarView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dtgvExportarView.Name = "dtgvExportarView";
            this.dtgvExportarView.OptionsBehavior.Editable = false;
            this.dtgvExportarView.OptionsBehavior.ReadOnly = true;
            this.dtgvExportarView.OptionsView.ColumnAutoWidth = false;
            this.dtgvExportarView.OptionsView.RowAutoHeight = true;
            this.dtgvExportarView.OptionsView.ShowFooter = true;
            // 
            // dtgPendientesGoce
            // 
            this.dtgPendientesGoce.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgPendientesGoce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgPendientesGoce.Location = new System.Drawing.Point(0, 0);
            this.dtgPendientesGoce.MainView = this.dtgvPendientesGoceView;
            this.dtgPendientesGoce.Name = "dtgPendientesGoce";
            this.dtgPendientesGoce.Size = new System.Drawing.Size(836, 374);
            this.dtgPendientesGoce.TabIndex = 101;
            this.dtgPendientesGoce.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvPendientesGoceView});
            // 
            // dtgvPendientesGoceView
            // 
            this.dtgvPendientesGoceView.GridControl = this.dtgPendientesGoce;
            this.dtgvPendientesGoceView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dtgvPendientesGoceView.Name = "dtgvPendientesGoceView";
            this.dtgvPendientesGoceView.OptionsBehavior.Editable = false;
            this.dtgvPendientesGoceView.OptionsBehavior.ReadOnly = true;
            this.dtgvPendientesGoceView.OptionsView.ColumnAutoWidth = false;
            this.dtgvPendientesGoceView.OptionsView.RowAutoHeight = true;
            this.dtgvPendientesGoceView.OptionsView.ShowFooter = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnExportar);
            this.panel5.Controls.Add(this.metroLabel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(836, 50);
            this.panel5.TabIndex = 4;
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.Location = new System.Drawing.Point(735, 9);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(79, 31);
            this.btnExportar.TabIndex = 5;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel7.Location = new System.Drawing.Point(16, 11);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(329, 25);
            this.metroLabel7.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel7.TabIndex = 92;
            this.metroLabel7.Text = "PENDIENTES DE GOCE POR PERIODO";
            this.metroLabel7.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // dtgListaAreas
            // 
            this.dtgListaAreas.AllowDrop = true;
            this.dtgListaAreas.AllowUserToAddRows = false;
            this.dtgListaAreas.AllowUserToDeleteRows = false;
            this.dtgListaAreas.AllowUserToResizeRows = false;
            this.dtgListaAreas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgListaAreas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgListaAreas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgListaAreas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgListaAreas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Marca,
            this.Codigo,
            this.Area});
            this.dtgListaAreas.Location = new System.Drawing.Point(154, 130);
            this.dtgListaAreas.Name = "dtgListaAreas";
            this.dtgListaAreas.ReadOnly = true;
            this.dtgListaAreas.RowHeadersVisible = false;
            this.dtgListaAreas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgListaAreas.Size = new System.Drawing.Size(261, 131);
            this.dtgListaAreas.TabIndex = 124;
            this.dtgListaAreas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgListaAreas_CellContentClick);
            this.dtgListaAreas.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgListaAreas_ColumnHeaderMouseClick);
            this.dtgListaAreas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtgListaAreas_KeyPress);
            // 
            // Marca
            // 
            this.Marca.FillWeight = 50F;
            this.Marca.HeaderText = "Marca";
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            this.Marca.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Marca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Marca.Width = 50;
            // 
            // Codigo
            // 
            this.Codigo.FillWeight = 5F;
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Visible = false;
            this.Codigo.Width = 5;
            // 
            // Area
            // 
            this.Area.FillWeight = 200F;
            this.Area.HeaderText = "Area";
            this.Area.Name = "Area";
            this.Area.ReadOnly = true;
            this.Area.Width = 200;
            // 
            // lvReemplazo
            // 
            this.lvReemplazo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lvReemplazo.ForeColor = System.Drawing.Color.Navy;
            this.lvReemplazo.FullRowSelect = true;
            this.lvReemplazo.GridLines = true;
            this.lvReemplazo.Location = new System.Drawing.Point(120, 205);
            this.lvReemplazo.MultiSelect = false;
            this.lvReemplazo.Name = "lvReemplazo";
            this.lvReemplazo.Size = new System.Drawing.Size(455, 170);
            this.lvReemplazo.TabIndex = 125;
            this.lvReemplazo.UseCompatibleStateImageBehavior = false;
            this.lvReemplazo.View = System.Windows.Forms.View.Details;
            this.lvReemplazo.Visible = false;
            this.lvReemplazo.Enter += new System.EventHandler(this.lvReemplazo_Enter);
            this.lvReemplazo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvReemplazo_KeyPress);
            this.lvReemplazo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvReemplazo_MouseDoubleClick);
            // 
            // frmProgramarVacaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1520, 670);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lvReemplazo);
            this.Controls.Add(this.dtgListaAreas);
            this.Name = "frmProgramarVacaciones";
            this.Text = "Programación de Vacaciones";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProgramarVacaciones_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaVacaciones)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaVacacionesView)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgExportar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvExportarView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPendientesGoce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPendientesGoceView)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaAreas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroTextBox txtEmpleado;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel lblDias;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.CheckBox cbActivar;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroTextBox txtAnticipacion;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl dtgListaVacaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvListaVacacionesView;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExportar;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private DevExpress.XtraGrid.GridControl dtgPendientesGoce;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvPendientesGoceView;
        private System.Windows.Forms.Button btnArea;
        public System.Windows.Forms.DataGridView dtgListaAreas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Area;
        private System.Windows.Forms.CheckBox cbTodos;
        private MetroFramework.Controls.MetroTextBox txtReemplazo;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private System.Windows.Forms.ListView lvReemplazo;
        private DevExpress.XtraGrid.GridControl dtgExportar;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvExportarView;
    }
}