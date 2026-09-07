namespace ReportesTranspesa.Formularios.Areas.Operaciones.LavadoUnidades
{
    partial class frmListaTicketsLavadero
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaTicketsLavadero));
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaProg = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTipoUnidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.btnAsignar = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNroTicket = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBuscarPlaca = new System.Windows.Forms.TextBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgLavadoUnidades = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reimprimirTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvLavadoUnidadesVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.pActualizar = new System.Windows.Forms.Panel();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbxNuevoEstado = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpNuevaFecha = new System.Windows.Forms.DateTimePicker();
            this.lblProgramacion = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblNroTicket = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.gbFiltros.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLavadoUnidades)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLavadoUnidadesVista)).BeginInit();
            this.pActualizar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.groupBox1);
            this.gbFiltros.Controls.Add(this.groupBox3);
            this.gbFiltros.Controls.Add(this.btnAsignar);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbFiltros.Location = new System.Drawing.Point(20, 60);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(1052, 144);
            this.gbFiltros.TabIndex = 106;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Registrar Nueva Unidad";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaProg);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.groupBox1.Location = new System.Drawing.Point(550, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 93);
            this.groupBox1.TabIndex = 116;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingresar Fecha:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(18, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 16);
            this.label5.TabIndex = 181;
            this.label5.Text = "Fecha Programada:";
            // 
            // dtpFechaProg
            // 
            this.dtpFechaProg.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaProg.CalendarForeColor = System.Drawing.Color.Blue;
            this.dtpFechaProg.CalendarMonthBackground = System.Drawing.Color.LightCyan;
            this.dtpFechaProg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaProg.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaProg.Location = new System.Drawing.Point(21, 51);
            this.dtpFechaProg.Name = "dtpFechaProg";
            this.dtpFechaProg.Size = new System.Drawing.Size(104, 21);
            this.dtpFechaProg.TabIndex = 180;
            this.dtpFechaProg.Tag = "1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtOperacion);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtTipoUnidad);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtPlaca);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.groupBox3.Location = new System.Drawing.Point(19, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(508, 93);
            this.groupBox3.TabIndex = 115;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ingresar Datos de Vehículo / Maquinaria: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 117;
            this.label3.Text = "Operación:";
            // 
            // txtOperacion
            // 
            this.txtOperacion.Location = new System.Drawing.Point(342, 51);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(145, 21);
            this.txtOperacion.TabIndex = 116;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 115;
            this.label2.Text = "Tipo de Unidad:";
            // 
            // txtTipoUnidad
            // 
            this.txtTipoUnidad.Location = new System.Drawing.Point(159, 51);
            this.txtTipoUnidad.Name = "txtTipoUnidad";
            this.txtTipoUnidad.ReadOnly = true;
            this.txtTipoUnidad.Size = new System.Drawing.Size(145, 21);
            this.txtTipoUnidad.TabIndex = 114;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 113;
            this.label1.Text = "Ingresar Placa:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtPlaca.Location = new System.Drawing.Point(21, 51);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(99, 21);
            this.txtPlaca.TabIndex = 112;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            this.txtPlaca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlaca_KeyUp);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAsignar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignar.Appearance.Options.UseBackColor = true;
            this.btnAsignar.Appearance.Options.UseBorderColor = true;
            this.btnAsignar.Appearance.Options.UseFont = true;
            this.btnAsignar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignar.Image")));
            this.btnAsignar.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnAsignar.Location = new System.Drawing.Point(760, 47);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(81, 66);
            this.btnAsignar.TabIndex = 167;
            this.btnAsignar.Text = "Registrar";
            this.btnAsignar.ToolTip = "Registrar";
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtNroTicket);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cbxOperacion);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cbxEstado);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.dtpFechaIni);
            this.panel2.Controls.Add(this.dtpFechaFin);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtBuscarPlaca);
            this.panel2.Controls.Add(this.btnExcel);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.panel2.Location = new System.Drawing.Point(20, 204);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1052, 91);
            this.panel2.TabIndex = 132;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label7.Location = new System.Drawing.Point(16, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 15);
            this.label7.TabIndex = 188;
            this.label7.Text = "Nro. Ticket:";
            // 
            // txtNroTicket
            // 
            this.txtNroTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtNroTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.txtNroTicket.Location = new System.Drawing.Point(90, 53);
            this.txtNroTicket.Name = "txtNroTicket";
            this.txtNroTicket.Size = new System.Drawing.Size(122, 21);
            this.txtNroTicket.TabIndex = 187;
            this.txtNroTicket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNroTicket_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label6.Location = new System.Drawing.Point(672, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 186;
            this.label6.Text = "Operación:";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(675, 44);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(154, 23);
            this.cbxOperacion.TabIndex = 185;
            this.cbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            this.cbxOperacion.DropDownClosed += new System.EventHandler(this.cbxOperacion_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label4.Location = new System.Drawing.Point(506, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 184;
            this.label4.Text = "Estado:";
            // 
            // cbxEstado
            // 
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODOS",
            "PENDIENTE",
            "TERMINADO",
            "ANULADO"});
            this.cbxEstado.Location = new System.Drawing.Point(509, 44);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(131, 23);
            this.cbxEstado.TabIndex = 183;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label14.Location = new System.Drawing.Point(244, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 15);
            this.label14.TabIndex = 182;
            this.label14.Text = "Fecha:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label13.Location = new System.Drawing.Point(353, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 15);
            this.label13.TabIndex = 181;
            this.label13.Text = "--";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(247, 45);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 179;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(374, 46);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 180;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label10.Location = new System.Drawing.Point(43, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 15);
            this.label10.TabIndex = 116;
            this.label10.Text = "Placa:";
            // 
            // txtBuscarPlaca
            // 
            this.txtBuscarPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBuscarPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.txtBuscarPlaca.Location = new System.Drawing.Point(90, 18);
            this.txtBuscarPlaca.Name = "txtBuscarPlaca";
            this.txtBuscarPlaca.Size = new System.Drawing.Size(122, 21);
            this.txtBuscarPlaca.TabIndex = 115;
            this.txtBuscarPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarPlaca_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(968, 23);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 45);
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
            this.btnBuscar.Location = new System.Drawing.Point(902, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(51, 45);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgLavadoUnidades
            // 
            this.dtgLavadoUnidades.CausesValidation = false;
            this.dtgLavadoUnidades.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgLavadoUnidades.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgLavadoUnidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgLavadoUnidades.Location = new System.Drawing.Point(20, 295);
            this.dtgLavadoUnidades.MainView = this.dgvLavadoUnidadesVista;
            this.dtgLavadoUnidades.Name = "dtgLavadoUnidades";
            this.dtgLavadoUnidades.Size = new System.Drawing.Size(1052, 274);
            this.dtgLavadoUnidades.TabIndex = 133;
            this.dtgLavadoUnidades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvLavadoUnidadesVista});
            this.dtgLavadoUnidades.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgLavadoUnidades_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reimprimirTicketToolStripMenuItem,
            this.actualizarRegistroToolStripMenuItem,
            this.eliminarRegistroToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 70);
            // 
            // reimprimirTicketToolStripMenuItem
            // 
            this.reimprimirTicketToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.impresora;
            this.reimprimirTicketToolStripMenuItem.Name = "reimprimirTicketToolStripMenuItem";
            this.reimprimirTicketToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.reimprimirTicketToolStripMenuItem.Text = "Reimprimir Ticket";
            this.reimprimirTicketToolStripMenuItem.Click += new System.EventHandler(this.reimprimirTicketToolStripMenuItem_Click);
            // 
            // actualizarRegistroToolStripMenuItem
            // 
            this.actualizarRegistroToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.actualizarRegistroToolStripMenuItem.Name = "actualizarRegistroToolStripMenuItem";
            this.actualizarRegistroToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.actualizarRegistroToolStripMenuItem.Text = "Actualizar Ticket";
            this.actualizarRegistroToolStripMenuItem.Click += new System.EventHandler(this.actualizarRegistroToolStripMenuItem_Click);
            // 
            // eliminarRegistroToolStripMenuItem
            // 
            this.eliminarRegistroToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarRegistroToolStripMenuItem.Name = "eliminarRegistroToolStripMenuItem";
            this.eliminarRegistroToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.eliminarRegistroToolStripMenuItem.Text = "Eliminar Ticket";
            this.eliminarRegistroToolStripMenuItem.Click += new System.EventHandler(this.eliminarRegistroToolStripMenuItem_Click);
            // 
            // dgvLavadoUnidadesVista
            // 
            this.dgvLavadoUnidadesVista.GridControl = this.dtgLavadoUnidades;
            this.dgvLavadoUnidadesVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvLavadoUnidadesVista.Name = "dgvLavadoUnidadesVista";
            this.dgvLavadoUnidadesVista.OptionsBehavior.Editable = false;
            this.dgvLavadoUnidadesVista.OptionsBehavior.ReadOnly = true;
            this.dgvLavadoUnidadesVista.OptionsView.ColumnAutoWidth = false;
            this.dgvLavadoUnidadesVista.OptionsView.RowAutoHeight = true;
            this.dgvLavadoUnidadesVista.OptionsView.ShowFooter = true;
            this.dgvLavadoUnidadesVista.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvLavadoUnidadesVista_RowCellClick);
            this.dgvLavadoUnidadesVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvLavadoUnidadesVista_CustomDrawCell);
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(60, 162);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(376, 120);
            this.lstPlaca.TabIndex = 212;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // pActualizar
            // 
            this.pActualizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pActualizar.Controls.Add(this.btnActualizar);
            this.pActualizar.Controls.Add(this.groupBox2);
            this.pActualizar.Controls.Add(this.lblProgramacion);
            this.pActualizar.Controls.Add(this.label15);
            this.pActualizar.Controls.Add(this.lblPlaca);
            this.pActualizar.Controls.Add(this.label12);
            this.pActualizar.Controls.Add(this.lblNroTicket);
            this.pActualizar.Controls.Add(this.label9);
            this.pActualizar.Controls.Add(this.btnCerrar);
            this.pActualizar.Controls.Add(this.label8);
            this.pActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.pActualizar.Location = new System.Drawing.Point(655, 324);
            this.pActualizar.Name = "pActualizar";
            this.pActualizar.Size = new System.Drawing.Size(432, 262);
            this.pActualizar.TabIndex = 213;
            this.pActualizar.Visible = false;
            this.pActualizar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pActualizar_MouseMove);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnActualizar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Appearance.Options.UseBackColor = true;
            this.btnActualizar.Appearance.Options.UseBorderColor = true;
            this.btnActualizar.Appearance.Options.UseFont = true;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnActualizar.Location = new System.Drawing.Point(312, 165);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(71, 65);
            this.btnActualizar.TabIndex = 142;
            this.btnActualizar.Tag = "5";
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.ToolTip = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.cbxNuevoEstado);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.dtpNuevaFecha);
            this.groupBox2.Location = new System.Drawing.Point(25, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 95);
            this.groupBox2.TabIndex = 141;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS DEL TICKET: ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label16.Location = new System.Drawing.Point(13, 60);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 16);
            this.label16.TabIndex = 191;
            this.label16.Text = "Estado:";
            // 
            // cbxNuevoEstado
            // 
            this.cbxNuevoEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNuevoEstado.FormattingEnabled = true;
            this.cbxNuevoEstado.Items.AddRange(new object[] {
            "PENDIENTE",
            "TERMINADO",
            "ANULADO"});
            this.cbxNuevoEstado.Location = new System.Drawing.Point(73, 57);
            this.cbxNuevoEstado.Name = "cbxNuevoEstado";
            this.cbxNuevoEstado.Size = new System.Drawing.Size(158, 24);
            this.cbxNuevoEstado.TabIndex = 190;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label11.Location = new System.Drawing.Point(13, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 16);
            this.label11.TabIndex = 189;
            this.label11.Text = "Fecha:";
            // 
            // dtpNuevaFecha
            // 
            this.dtpNuevaFecha.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNuevaFecha.CalendarForeColor = System.Drawing.Color.Blue;
            this.dtpNuevaFecha.CalendarMonthBackground = System.Drawing.Color.LightCyan;
            this.dtpNuevaFecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpNuevaFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.dtpNuevaFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNuevaFecha.Location = new System.Drawing.Point(73, 25);
            this.dtpNuevaFecha.Name = "dtpNuevaFecha";
            this.dtpNuevaFecha.Size = new System.Drawing.Size(159, 22);
            this.dtpNuevaFecha.TabIndex = 181;
            this.dtpNuevaFecha.Tag = "1";
            // 
            // lblProgramacion
            // 
            this.lblProgramacion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblProgramacion.AutoSize = true;
            this.lblProgramacion.Font = new System.Drawing.Font("Arial", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblProgramacion.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblProgramacion.Location = new System.Drawing.Point(130, 112);
            this.lblProgramacion.Name = "lblProgramacion";
            this.lblProgramacion.Size = new System.Drawing.Size(178, 19);
            this.lblProgramacion.TabIndex = 140;
            this.lblProgramacion.Text = "SIN PROGRAMACION";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 12.5F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(19, 112);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 19);
            this.label15.TabIndex = 139;
            this.label15.Text = "OPERACIÓN: ";
            // 
            // lblPlaca
            // 
            this.lblPlaca.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Arial", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblPlaca.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblPlaca.Location = new System.Drawing.Point(86, 83);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(89, 19);
            this.lblPlaca.TabIndex = 138;
            this.lblPlaca.Text = "RETROEX";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 12.5F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(19, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 19);
            this.label12.TabIndex = 137;
            this.label12.Text = "PLACA: ";
            // 
            // lblNroTicket
            // 
            this.lblNroTicket.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNroTicket.AutoSize = true;
            this.lblNroTicket.Font = new System.Drawing.Font("Arial", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblNroTicket.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblNroTicket.Location = new System.Drawing.Point(135, 55);
            this.lblNroTicket.Name = "lblNroTicket";
            this.lblNroTicket.Size = new System.Drawing.Size(81, 19);
            this.lblNroTicket.TabIndex = 136;
            this.lblNroTicket.Text = "24001245";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12.5F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(19, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 19);
            this.label9.TabIndex = 135;
            this.label9.Text = "NRO. TICKET: ";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(396, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(29, 29);
            this.btnCerrar.TabIndex = 133;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label8.Location = new System.Drawing.Point(9, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(362, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "ACTUALIZAR TICKET DE LAVADO";
            // 
            // frmListaTicketsLavadero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 589);
            this.Controls.Add(this.dtgLavadoUnidades);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbFiltros);
            this.Controls.Add(this.lstPlaca);
            this.Controls.Add(this.pActualizar);
            this.Name = "frmListaTicketsLavadero";
            this.Text = "REGISTRO DE LAVADO DE UNIDADES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaTicketsLavadero_Load);
            this.gbFiltros.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLavadoUnidades)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLavadoUnidadesVista)).EndInit();
            this.pActualizar.ResumeLayout(false);
            this.pActualizar.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTipoUnidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaProg;
        public DevExpress.XtraEditors.SimpleButton btnAsignar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBuscarPlaca;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgLavadoUnidades;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvLavadoUnidadesVista;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actualizarRegistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarRegistroToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.ListView lstPlaca;
        private System.Windows.Forms.ToolStripMenuItem reimprimirTicketToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxOperacion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNroTicket;
        private System.Windows.Forms.Panel pActualizar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblNroTicket;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblProgramacion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbxNuevoEstado;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpNuevaFecha;
        public DevExpress.XtraEditors.SimpleButton btnActualizar;
    }
}