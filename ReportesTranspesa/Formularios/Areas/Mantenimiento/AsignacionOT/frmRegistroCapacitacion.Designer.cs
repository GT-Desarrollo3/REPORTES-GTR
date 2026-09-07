namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    partial class frmRegistroCapacitacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroCapacitacion));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBuscarAsist = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBuscarTema = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnNuevoRegistro = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgRegistro = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsActualizarRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAnularRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvRegistroView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabRegistro = new System.Windows.Forms.TabPage();
            this.tabAsistencia = new System.Windows.Forms.TabPage();
            this.dtgAsistencia = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsActualizarAsist = new System.Windows.Forms.ToolStripMenuItem();
            this.tsActPendiente = new System.Windows.Forms.ToolStripMenuItem();
            this.tsActEjecutado = new System.Windows.Forms.ToolStripMenuItem();
            this.tsActReprogramado = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAnularAsist = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAsistenciaView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pDatosCapacitacion = new System.Windows.Forms.Panel();
            this.btnRegistrar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPersona = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpCFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTema = new System.Windows.Forms.TextBox();
            this.dtpCFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCapacitador = new System.Windows.Forms.TextBox();
            this.dtgCapacitacion = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarAsistencia = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvCapacitacionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lstPersona = new System.Windows.Forms.ListView();
            this.btnAgregar = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistro)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroView)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.tabRegistro.SuspendLayout();
            this.tabAsistencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAsistencia)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistenciaView)).BeginInit();
            this.pDatosCapacitacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCapacitacion)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacitacionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(249)))), ((int)(((byte)(39)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1085, 46);
            this.label1.TabIndex = 19;
            this.label1.Text = "REGISTRO DE CAPACITACIONES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(189)))));
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtBuscarAsist);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtBuscarTema);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.cbxEstado);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.dtpFechaIni);
            this.panel3.Controls.Add(this.dtpFechaFin);
            this.panel3.Controls.Add(this.btnNuevoRegistro);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1085, 101);
            this.panel3.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(455, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 15);
            this.label10.TabIndex = 235;
            this.label10.Text = "Asistente:";
            // 
            // txtBuscarAsist
            // 
            this.txtBuscarAsist.BackColor = System.Drawing.SystemColors.Window;
            this.txtBuscarAsist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarAsist.Location = new System.Drawing.Point(520, 56);
            this.txtBuscarAsist.Name = "txtBuscarAsist";
            this.txtBuscarAsist.Size = new System.Drawing.Size(282, 21);
            this.txtBuscarAsist.TabIndex = 234;
            this.txtBuscarAsist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarAsist_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(472, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 15);
            this.label9.TabIndex = 233;
            this.label9.Text = "Tema:";
            // 
            // txtBuscarTema
            // 
            this.txtBuscarTema.BackColor = System.Drawing.SystemColors.Window;
            this.txtBuscarTema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarTema.Location = new System.Drawing.Point(520, 24);
            this.txtBuscarTema.Name = "txtBuscarTema";
            this.txtBuscarTema.Size = new System.Drawing.Size(282, 21);
            this.txtBuscarTema.TabIndex = 117;
            this.txtBuscarTema.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarTema_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(159, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 15);
            this.label8.TabIndex = 232;
            this.label8.Text = "Estado:";
            // 
            // cbxEstado
            // 
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODAS",
            "PENDIENTE",
            "EJECUTADO",
            "REPROGRAMADO"});
            this.cbxEstado.Location = new System.Drawing.Point(213, 55);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(178, 23);
            this.cbxEstado.TabIndex = 187;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(315, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 15);
            this.label13.TabIndex = 184;
            this.label13.Text = "--";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(163, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 15);
            this.label7.TabIndex = 231;
            this.label7.Text = "Fecha:";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(213, 24);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 182;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(334, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 183;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // btnNuevoRegistro
            // 
            this.btnNuevoRegistro.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoRegistro.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoRegistro.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoRegistro.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoRegistro.Appearance.Options.UseBackColor = true;
            this.btnNuevoRegistro.Appearance.Options.UseBorderColor = true;
            this.btnNuevoRegistro.Appearance.Options.UseFont = true;
            this.btnNuevoRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoRegistro.Image")));
            this.btnNuevoRegistro.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoRegistro.Location = new System.Drawing.Point(24, 28);
            this.btnNuevoRegistro.Name = "btnNuevoRegistro";
            this.btnNuevoRegistro.Size = new System.Drawing.Size(109, 45);
            this.btnNuevoRegistro.TabIndex = 224;
            this.btnNuevoRegistro.Text = "Nuevo\r\nRegistro";
            this.btnNuevoRegistro.ToolTip = "Nuevo Registro";
            this.btnNuevoRegistro.Click += new System.EventHandler(this.btnNuevoRegistro_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(893, 28);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 45);
            this.btnExcel.TabIndex = 194;
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
            this.btnBuscar.Location = new System.Drawing.Point(831, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(51, 45);
            this.btnBuscar.TabIndex = 193;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgRegistro
            // 
            this.dtgRegistro.AllowDrop = true;
            this.dtgRegistro.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRegistro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRegistro.Location = new System.Drawing.Point(3, 3);
            this.dtgRegistro.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(126)))));
            this.dtgRegistro.LookAndFeel.SkinName = "Money Twins";
            this.dtgRegistro.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRegistro.MainView = this.dgvRegistroView;
            this.dtgRegistro.Name = "dtgRegistro";
            this.dtgRegistro.Size = new System.Drawing.Size(1071, 381);
            this.dtgRegistro.TabIndex = 101;
            this.dtgRegistro.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRegistroView});
            this.dtgRegistro.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgRegistro_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsActualizarRegistro,
            this.tsAnularRegistro});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 48);
            // 
            // tsActualizarRegistro
            // 
            this.tsActualizarRegistro.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.tsActualizarRegistro.Name = "tsActualizarRegistro";
            this.tsActualizarRegistro.Size = new System.Drawing.Size(172, 22);
            this.tsActualizarRegistro.Text = "Actualizar Registro";
            this.tsActualizarRegistro.Click += new System.EventHandler(this.tsActualizarRegistro_Click);
            // 
            // tsAnularRegistro
            // 
            this.tsAnularRegistro.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsAnularRegistro.Name = "tsAnularRegistro";
            this.tsAnularRegistro.Size = new System.Drawing.Size(172, 22);
            this.tsAnularRegistro.Text = "Anular Registro";
            this.tsAnularRegistro.Click += new System.EventHandler(this.tsAnularRegistro_Click);
            // 
            // dgvRegistroView
            // 
            this.dgvRegistroView.GridControl = this.dtgRegistro;
            this.dgvRegistroView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvRegistroView.Name = "dgvRegistroView";
            this.dgvRegistroView.OptionsBehavior.Editable = false;
            this.dgvRegistroView.OptionsView.ColumnAutoWidth = false;
            this.dgvRegistroView.OptionsView.RowAutoHeight = true;
            this.dgvRegistroView.OptionsView.ShowFooter = true;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabRegistro);
            this.tabInfo.Controls.Add(this.tabAsistencia);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfo.Location = new System.Drawing.Point(0, 147);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(1085, 420);
            this.tabInfo.TabIndex = 231;
            // 
            // tabRegistro
            // 
            this.tabRegistro.Controls.Add(this.dtgRegistro);
            this.tabRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabRegistro.Location = new System.Drawing.Point(4, 29);
            this.tabRegistro.Name = "tabRegistro";
            this.tabRegistro.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegistro.Size = new System.Drawing.Size(1077, 387);
            this.tabRegistro.TabIndex = 1;
            this.tabRegistro.Text = "REGISTRO";
            this.tabRegistro.UseVisualStyleBackColor = true;
            // 
            // tabAsistencia
            // 
            this.tabAsistencia.Controls.Add(this.dtgAsistencia);
            this.tabAsistencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAsistencia.Location = new System.Drawing.Point(4, 29);
            this.tabAsistencia.Name = "tabAsistencia";
            this.tabAsistencia.Padding = new System.Windows.Forms.Padding(3);
            this.tabAsistencia.Size = new System.Drawing.Size(1077, 387);
            this.tabAsistencia.TabIndex = 0;
            this.tabAsistencia.Text = "ASISTENCIA";
            this.tabAsistencia.UseVisualStyleBackColor = true;
            // 
            // dtgAsistencia
            // 
            this.dtgAsistencia.AllowDrop = true;
            this.dtgAsistencia.ContextMenuStrip = this.contextMenuStrip3;
            this.dtgAsistencia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgAsistencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgAsistencia.Location = new System.Drawing.Point(3, 3);
            this.dtgAsistencia.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(52)))));
            this.dtgAsistencia.LookAndFeel.SkinName = "Money Twins";
            this.dtgAsistencia.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgAsistencia.MainView = this.dgvAsistenciaView;
            this.dtgAsistencia.Name = "dtgAsistencia";
            this.dtgAsistencia.Size = new System.Drawing.Size(1071, 381);
            this.dtgAsistencia.TabIndex = 102;
            this.dtgAsistencia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvAsistenciaView});
            this.dtgAsistencia.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgAsistencia_MouseUp);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsActualizarAsist,
            this.tsAnularAsist});
            this.contextMenuStrip3.Name = "contextMenuStrip1";
            this.contextMenuStrip3.Size = new System.Drawing.Size(183, 70);
            // 
            // tsActualizarAsist
            // 
            this.tsActualizarAsist.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsActPendiente,
            this.tsActEjecutado,
            this.tsActReprogramado});
            this.tsActualizarAsist.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.tsActualizarAsist.Name = "tsActualizarAsist";
            this.tsActualizarAsist.Size = new System.Drawing.Size(182, 22);
            this.tsActualizarAsist.Text = "Actualizar Asistencia";
            // 
            // tsActPendiente
            // 
            this.tsActPendiente.Name = "tsActPendiente";
            this.tsActPendiente.Size = new System.Drawing.Size(169, 22);
            this.tsActPendiente.Text = "PENDIENTE";
            this.tsActPendiente.Click += new System.EventHandler(this.tsActPendiente_Click);
            // 
            // tsActEjecutado
            // 
            this.tsActEjecutado.Name = "tsActEjecutado";
            this.tsActEjecutado.Size = new System.Drawing.Size(169, 22);
            this.tsActEjecutado.Text = "EJECUTADO";
            this.tsActEjecutado.Click += new System.EventHandler(this.tsActEjecutado_Click);
            // 
            // tsActReprogramado
            // 
            this.tsActReprogramado.Name = "tsActReprogramado";
            this.tsActReprogramado.Size = new System.Drawing.Size(169, 22);
            this.tsActReprogramado.Text = "REPROGRAMADO";
            this.tsActReprogramado.Click += new System.EventHandler(this.tsActReprogramado_Click);
            // 
            // tsAnularAsist
            // 
            this.tsAnularAsist.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsAnularAsist.Name = "tsAnularAsist";
            this.tsAnularAsist.Size = new System.Drawing.Size(182, 22);
            this.tsAnularAsist.Text = "Anular Asistencia";
            this.tsAnularAsist.Click += new System.EventHandler(this.tsAnularAsist_Click);
            // 
            // dgvAsistenciaView
            // 
            this.dgvAsistenciaView.GridControl = this.dtgAsistencia;
            this.dgvAsistenciaView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvAsistenciaView.Name = "dgvAsistenciaView";
            this.dgvAsistenciaView.OptionsBehavior.Editable = false;
            this.dgvAsistenciaView.OptionsSelection.MultiSelect = true;
            this.dgvAsistenciaView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvAsistenciaView.OptionsView.ColumnAutoWidth = false;
            this.dgvAsistenciaView.OptionsView.RowAutoHeight = true;
            this.dgvAsistenciaView.OptionsView.ShowFooter = true;
            this.dgvAsistenciaView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvAsistenciaView_CustomDrawCell);
            // 
            // pDatosCapacitacion
            // 
            this.pDatosCapacitacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(189)))));
            this.pDatosCapacitacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDatosCapacitacion.Controls.Add(this.btnRegistrar);
            this.pDatosCapacitacion.Controls.Add(this.txtPersona);
            this.pDatosCapacitacion.Controls.Add(this.label6);
            this.pDatosCapacitacion.Controls.Add(this.dtpCFechaFin);
            this.pDatosCapacitacion.Controls.Add(this.label18);
            this.pDatosCapacitacion.Controls.Add(this.label5);
            this.pDatosCapacitacion.Controls.Add(this.txtTema);
            this.pDatosCapacitacion.Controls.Add(this.dtpCFechaIni);
            this.pDatosCapacitacion.Controls.Add(this.label4);
            this.pDatosCapacitacion.Controls.Add(this.txtCapacitador);
            this.pDatosCapacitacion.Controls.Add(this.dtgCapacitacion);
            this.pDatosCapacitacion.Controls.Add(this.txtEmpresa);
            this.pDatosCapacitacion.Controls.Add(this.label2);
            this.pDatosCapacitacion.Controls.Add(this.btnCerrar);
            this.pDatosCapacitacion.Controls.Add(this.label3);
            this.pDatosCapacitacion.Controls.Add(this.label15);
            this.pDatosCapacitacion.Controls.Add(this.lstPersona);
            this.pDatosCapacitacion.Controls.Add(this.btnAgregar);
            this.pDatosCapacitacion.Location = new System.Drawing.Point(173, 187);
            this.pDatosCapacitacion.Name = "pDatosCapacitacion";
            this.pDatosCapacitacion.Size = new System.Drawing.Size(859, 375);
            this.pDatosCapacitacion.TabIndex = 232;
            this.pDatosCapacitacion.Visible = false;
            this.pDatosCapacitacion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDatosCapacitacion_MouseMove);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRegistrar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnRegistrar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnRegistrar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnRegistrar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Appearance.Options.UseBackColor = true;
            this.btnRegistrar.Appearance.Options.UseBorderColor = true;
            this.btnRegistrar.Appearance.Options.UseFont = true;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.Image")));
            this.btnRegistrar.Location = new System.Drawing.Point(157, 321);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(95, 36);
            this.btnRegistrar.TabIndex = 232;
            this.btnRegistrar.Tag = "5";
            this.btnRegistrar.Text = "Guardar";
            this.btnRegistrar.ToolTip = "Guardar";
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // txtPersona
            // 
            this.txtPersona.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPersona.Location = new System.Drawing.Point(464, 49);
            this.txtPersona.Name = "txtPersona";
            this.txtPersona.Size = new System.Drawing.Size(356, 20);
            this.txtPersona.TabIndex = 231;
            this.txtPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersona_KeyPress);
            this.txtPersona.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPersona_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(408, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 230;
            this.label6.Text = "Asistente:";
            // 
            // dtpCFechaFin
            // 
            this.dtpCFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCFechaFin.Location = new System.Drawing.Point(138, 265);
            this.dtpCFechaFin.Name = "dtpCFechaFin";
            this.dtpCFechaFin.Size = new System.Drawing.Size(100, 20);
            this.dtpCFechaFin.TabIndex = 212;
            this.dtpCFechaFin.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpCFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpCFechaFin_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(119, 267);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 15);
            this.label18.TabIndex = 208;
            this.label18.Text = "--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(14, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 229;
            this.label5.Text = "Duración:";
            // 
            // txtTema
            // 
            this.txtTema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTema.Location = new System.Drawing.Point(17, 179);
            this.txtTema.Multiline = true;
            this.txtTema.Name = "txtTema";
            this.txtTema.Size = new System.Drawing.Size(372, 52);
            this.txtTema.TabIndex = 228;
            this.txtTema.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTema_KeyPress);
            // 
            // dtpCFechaIni
            // 
            this.dtpCFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCFechaIni.Location = new System.Drawing.Point(17, 265);
            this.dtpCFechaIni.Name = "dtpCFechaIni";
            this.dtpCFechaIni.Size = new System.Drawing.Size(100, 20);
            this.dtpCFechaIni.TabIndex = 209;
            this.dtpCFechaIni.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpCFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpCFechaIni_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(14, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 227;
            this.label4.Text = "Tema:";
            // 
            // txtCapacitador
            // 
            this.txtCapacitador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCapacitador.Location = new System.Drawing.Point(17, 125);
            this.txtCapacitador.Name = "txtCapacitador";
            this.txtCapacitador.Size = new System.Drawing.Size(372, 21);
            this.txtCapacitador.TabIndex = 215;
            this.txtCapacitador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapacitador_KeyPress);
            // 
            // dtgCapacitacion
            // 
            this.dtgCapacitacion.AllowDrop = true;
            this.dtgCapacitacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgCapacitacion.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgCapacitacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgCapacitacion.Location = new System.Drawing.Point(409, 85);
            this.dtgCapacitacion.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(52)))));
            this.dtgCapacitacion.LookAndFeel.SkinName = "Money Twins";
            this.dtgCapacitacion.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgCapacitacion.MainView = this.dgvCapacitacionView;
            this.dtgCapacitacion.Name = "dtgCapacitacion";
            this.dtgCapacitacion.Size = new System.Drawing.Size(449, 289);
            this.dtgCapacitacion.TabIndex = 226;
            this.dtgCapacitacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvCapacitacionView});
            this.dtgCapacitacion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgCapacitacion_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarAsistencia});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(118, 26);
            // 
            // tsEliminarAsistencia
            // 
            this.tsEliminarAsistencia.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsEliminarAsistencia.Name = "tsEliminarAsistencia";
            this.tsEliminarAsistencia.Size = new System.Drawing.Size(117, 22);
            this.tsEliminarAsistencia.Text = "Eliminar";
            this.tsEliminarAsistencia.Click += new System.EventHandler(this.tsEliminarAsistencia_Click);
            // 
            // dgvCapacitacionView
            // 
            this.dgvCapacitacionView.GridControl = this.dtgCapacitacion;
            this.dgvCapacitacionView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvCapacitacionView.Name = "dgvCapacitacionView";
            this.dgvCapacitacionView.OptionsBehavior.Editable = false;
            this.dgvCapacitacionView.OptionsView.ColumnAutoWidth = false;
            this.dgvCapacitacionView.OptionsView.RowAutoHeight = true;
            this.dgvCapacitacionView.OptionsView.ShowGroupPanel = false;
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEmpresa.Location = new System.Drawing.Point(17, 70);
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(372, 21);
            this.txtEmpresa.TabIndex = 214;
            this.txtEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpresa_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(14, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 211;
            this.label2.Text = "Capacitador:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(833, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 225;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(14, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 208;
            this.label3.Text = "Empresa:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(93)))), ((int)(((byte)(100)))));
            this.label15.Location = new System.Drawing.Point(11, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(294, 25);
            this.label15.TabIndex = 49;
            this.label15.Text = "DATOS DE CAPACITACIÓN";
            // 
            // lstPersona
            // 
            this.lstPersona.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lstPersona.BackColor = System.Drawing.Color.LavenderBlush;
            this.lstPersona.ForeColor = System.Drawing.Color.Navy;
            this.lstPersona.FullRowSelect = true;
            this.lstPersona.GridLines = true;
            this.lstPersona.Location = new System.Drawing.Point(464, 68);
            this.lstPersona.MultiSelect = false;
            this.lstPersona.Name = "lstPersona";
            this.lstPersona.Size = new System.Drawing.Size(356, 110);
            this.lstPersona.TabIndex = 248;
            this.lstPersona.UseCompatibleStateImageBehavior = false;
            this.lstPersona.View = System.Windows.Forms.View.Details;
            this.lstPersona.Visible = false;
            this.lstPersona.Enter += new System.EventHandler(this.lstPersona_Enter);
            this.lstPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersona_KeyPress);
            this.lstPersona.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersona_MouseDoubleClick);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAgregar.Location = new System.Drawing.Point(777, 47);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(23, 24);
            this.btnAgregar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnAgregar.TabIndex = 249;
            this.btnAgregar.TabStop = false;
            this.btnAgregar.Visible = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmRegistroCapacitacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(255)))), ((int)(((byte)(189)))));
            this.ClientSize = new System.Drawing.Size(1085, 567);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pDatosCapacitacion);
            this.Name = "frmRegistroCapacitacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONVENIOS DE CAPACITACIÓN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRegistroCapacitacion_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistro)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroView)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.tabRegistro.ResumeLayout(false);
            this.tabAsistencia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAsistencia)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistenciaView)).EndInit();
            this.pDatosCapacitacion.ResumeLayout(false);
            this.pDatosCapacitacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCapacitacion)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapacitacionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.TextBox txtBuscarTema;
        private DevExpress.XtraGrid.GridControl dtgRegistro;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRegistroView;
        private DevExpress.XtraEditors.SimpleButton btnNuevoRegistro;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsActualizarRegistro;
        private System.Windows.Forms.ToolStripMenuItem tsAnularRegistro;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabRegistro;
        private System.Windows.Forms.TabPage tabAsistencia;
        private DevExpress.XtraGrid.GridControl dtgAsistencia;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvAsistenciaView;
        private System.Windows.Forms.Panel pDatosCapacitacion;
        private System.Windows.Forms.TextBox txtCapacitador;
        private System.Windows.Forms.TextBox txtEmpresa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpCFechaFin;
        private System.Windows.Forms.DateTimePicker dtpCFechaIni;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnCerrar;
        private DevExpress.XtraGrid.GridControl dtgCapacitacion;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvCapacitacionView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTema;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPersona;
        private System.Windows.Forms.Label label6;
        public DevExpress.XtraEditors.SimpleButton btnRegistrar;
        private System.Windows.Forms.ListView lstPersona;
        private System.Windows.Forms.PictureBox btnAgregar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarAsistencia;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem tsActualizarAsist;
        private System.Windows.Forms.ToolStripMenuItem tsActPendiente;
        private System.Windows.Forms.ToolStripMenuItem tsActEjecutado;
        private System.Windows.Forms.ToolStripMenuItem tsActReprogramado;
        private System.Windows.Forms.ToolStripMenuItem tsAnularAsist;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBuscarAsist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}