namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class AnexarGuiasRetorno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnexarGuiasRetorno));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgViajesData = new DevExpress.XtraGrid.GridControl();
            this.dgvViajeExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dgvViajes = new System.Windows.Forms.DataGridView();
            this.CODVIAJE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RUTA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idRuta1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONDUCTOR1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idConductor1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLIENTE1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCliente1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUIATRANSP1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUIAREM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idGuia1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRACTO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROYECTO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARRETA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTADO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkEstadoViaje = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.grFiltroFecha = new System.Windows.Forms.GroupBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCodViaje = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtNombreConductor = new System.Windows.Forms.TextBox();
            this.grEstadoGrupo = new System.Windows.Forms.GroupBox();
            this.rbtEjecucion = new System.Windows.Forms.RadioButton();
            this.rbtProgramado = new System.Windows.Forms.RadioButton();
            this.rbtCompletado = new System.Windows.Forms.RadioButton();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.lvRuta = new System.Windows.Forms.ListView();
            this.dgv = new DevExpress.XtraGrid.GridControl();
            this.dgvExportar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViajesData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajeExpressVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajes)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grFiltroFecha.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grEstadoGrupo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExportar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1266, 43);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1266, 43);
            this.label1.TabIndex = 2;
            this.label1.Text = "ANEXAR GUIAS A VIAJE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Controls.Add(this.dtgViajesData);
            this.panel2.Controls.Add(this.dgvViajes);
            this.panel2.Controls.Add(this.chkEstadoViaje);
            this.panel2.Controls.Add(this.grEstadoGrupo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1266, 454);
            this.panel2.TabIndex = 2;
            // 
            // dtgViajesData
            // 
            this.dtgViajesData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgViajesData.Location = new System.Drawing.Point(0, 0);
            this.dtgViajesData.MainView = this.dgvViajeExpressVista;
            this.dtgViajesData.Name = "dtgViajesData";
            this.dtgViajesData.Size = new System.Drawing.Size(1266, 454);
            this.dtgViajesData.TabIndex = 8;
            this.dtgViajesData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvViajeExpressVista});
            // 
            // dgvViajeExpressVista
            // 
            this.dgvViajeExpressVista.GridControl = this.dtgViajesData;
            this.dgvViajeExpressVista.Name = "dgvViajeExpressVista";
            this.dgvViajeExpressVista.OptionsBehavior.Editable = false;
            this.dgvViajeExpressVista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvViajeExpressVista_KeyPress);
            this.dgvViajeExpressVista.DoubleClick += new System.EventHandler(this.dgvViajeExpressVista_DoubleClick);
            // 
            // dgvViajes
            // 
            this.dgvViajes.AllowUserToAddRows = false;
            this.dgvViajes.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvViajes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvViajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvViajes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CODVIAJE1,
            this.FECHA1,
            this.RUTA1,
            this.idRuta1,
            this.KM1,
            this.CONDUCTOR1,
            this.idConductor1,
            this.CLIENTE1,
            this.idCliente1,
            this.GUIATRANSP1,
            this.GUIAREM1,
            this.idGuia1,
            this.TRACTO1,
            this.PROYECTO1,
            this.CARRETA1,
            this.ESTADO1});
            this.dgvViajes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvViajes.Location = new System.Drawing.Point(0, 0);
            this.dgvViajes.Name = "dgvViajes";
            this.dgvViajes.RowHeadersVisible = false;
            this.dgvViajes.Size = new System.Drawing.Size(1266, 454);
            this.dgvViajes.TabIndex = 7;
            // 
            // CODVIAJE1
            // 
            this.CODVIAJE1.HeaderText = "CODVIAJE";
            this.CODVIAJE1.Name = "CODVIAJE1";
            this.CODVIAJE1.ReadOnly = true;
            // 
            // FECHA1
            // 
            this.FECHA1.HeaderText = "FECHA";
            this.FECHA1.Name = "FECHA1";
            this.FECHA1.ReadOnly = true;
            // 
            // RUTA1
            // 
            this.RUTA1.HeaderText = "RUTA";
            this.RUTA1.Name = "RUTA1";
            this.RUTA1.ReadOnly = true;
            // 
            // idRuta1
            // 
            this.idRuta1.HeaderText = "idRuta";
            this.idRuta1.Name = "idRuta1";
            this.idRuta1.ReadOnly = true;
            this.idRuta1.Visible = false;
            // 
            // KM1
            // 
            this.KM1.HeaderText = "KM";
            this.KM1.Name = "KM1";
            this.KM1.ReadOnly = true;
            // 
            // CONDUCTOR1
            // 
            this.CONDUCTOR1.HeaderText = "CONDUCTOR";
            this.CONDUCTOR1.Name = "CONDUCTOR1";
            this.CONDUCTOR1.ReadOnly = true;
            // 
            // idConductor1
            // 
            this.idConductor1.HeaderText = "idConductor";
            this.idConductor1.Name = "idConductor1";
            this.idConductor1.ReadOnly = true;
            this.idConductor1.Visible = false;
            // 
            // CLIENTE1
            // 
            this.CLIENTE1.HeaderText = "CLIENTE";
            this.CLIENTE1.Name = "CLIENTE1";
            this.CLIENTE1.ReadOnly = true;
            // 
            // idCliente1
            // 
            this.idCliente1.HeaderText = "idCliente";
            this.idCliente1.Name = "idCliente1";
            this.idCliente1.ReadOnly = true;
            this.idCliente1.Visible = false;
            // 
            // GUIATRANSP1
            // 
            this.GUIATRANSP1.HeaderText = "GUIA/TRANSP";
            this.GUIATRANSP1.Name = "GUIATRANSP1";
            this.GUIATRANSP1.ReadOnly = true;
            // 
            // GUIAREM1
            // 
            this.GUIAREM1.HeaderText = "GUIA/REM";
            this.GUIAREM1.Name = "GUIAREM1";
            this.GUIAREM1.ReadOnly = true;
            // 
            // idGuia1
            // 
            this.idGuia1.HeaderText = "idGuia";
            this.idGuia1.Name = "idGuia1";
            this.idGuia1.ReadOnly = true;
            this.idGuia1.Visible = false;
            // 
            // TRACTO1
            // 
            this.TRACTO1.HeaderText = "TRACTO";
            this.TRACTO1.Name = "TRACTO1";
            this.TRACTO1.ReadOnly = true;
            // 
            // PROYECTO1
            // 
            this.PROYECTO1.HeaderText = "PROYECTO";
            this.PROYECTO1.Name = "PROYECTO1";
            this.PROYECTO1.ReadOnly = true;
            // 
            // CARRETA1
            // 
            this.CARRETA1.HeaderText = "CARRETA";
            this.CARRETA1.Name = "CARRETA1";
            this.CARRETA1.ReadOnly = true;
            // 
            // ESTADO1
            // 
            this.ESTADO1.HeaderText = "ESTADO";
            this.ESTADO1.Name = "ESTADO1";
            this.ESTADO1.ReadOnly = true;
            // 
            // chkEstadoViaje
            // 
            this.chkEstadoViaje.AutoSize = true;
            this.chkEstadoViaje.Location = new System.Drawing.Point(12, 391);
            this.chkEstadoViaje.Name = "chkEstadoViaje";
            this.chkEstadoViaje.Size = new System.Drawing.Size(15, 14);
            this.chkEstadoViaje.TabIndex = 7;
            this.chkEstadoViaje.UseVisualStyleBackColor = true;
            this.chkEstadoViaje.Visible = false;
            this.chkEstadoViaje.CheckedChanged += new System.EventHandler(this.chkEstadoViaje_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.simpleButton1);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.grFiltroFecha);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1266, 85);
            this.panel3.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(968, 22);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(214, 38);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "EXPORTAR VIAJES DE RETORNO";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtRuta);
            this.groupBox4.Location = new System.Drawing.Point(574, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(228, 51);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filtrar por Ruta";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(7, 22);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(215, 20);
            this.txtRuta.TabIndex = 0;
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            this.txtRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRuta_KeyUp);
            // 
            // grFiltroFecha
            // 
            this.grFiltroFecha.Controls.Add(this.dtpFechaInicio);
            this.grFiltroFecha.Location = new System.Drawing.Point(5, 9);
            this.grFiltroFecha.Name = "grFiltroFecha";
            this.grFiltroFecha.Size = new System.Drawing.Size(118, 51);
            this.grFiltroFecha.TabIndex = 13;
            this.grFiltroFecha.TabStop = false;
            this.grFiltroFecha.Text = "Fecha Inicio";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(6, 21);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(103, 20);
            this.dtpFechaInicio.TabIndex = 1;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Location = new System.Drawing.Point(129, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 51);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha Fin";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(6, 21);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(102, 20);
            this.dtpFechaFin.TabIndex = 2;
            this.dtpFechaFin.ValueChanged += new System.EventHandler(this.dtpFechaFin_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodViaje);
            this.groupBox2.Location = new System.Drawing.Point(251, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(107, 51);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrar por Viaje";
            // 
            // txtCodViaje
            // 
            this.txtCodViaje.Location = new System.Drawing.Point(7, 22);
            this.txtCodViaje.Name = "txtCodViaje";
            this.txtCodViaje.Size = new System.Drawing.Size(91, 20);
            this.txtCodViaje.TabIndex = 0;
            this.txtCodViaje.TextChanged += new System.EventHandler(this.txtCodViaje_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtNombreConductor);
            this.groupBox3.Location = new System.Drawing.Point(364, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 51);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtrar por Conductor";
            // 
            // txtNombreConductor
            // 
            this.txtNombreConductor.Location = new System.Drawing.Point(7, 22);
            this.txtNombreConductor.Name = "txtNombreConductor";
            this.txtNombreConductor.Size = new System.Drawing.Size(191, 20);
            this.txtNombreConductor.TabIndex = 0;
            this.txtNombreConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreConductor_KeyPress);
            this.txtNombreConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNombreConductor_KeyUp);
            // 
            // grEstadoGrupo
            // 
            this.grEstadoGrupo.Controls.Add(this.rbtEjecucion);
            this.grEstadoGrupo.Controls.Add(this.rbtProgramado);
            this.grEstadoGrupo.Controls.Add(this.rbtCompletado);
            this.grEstadoGrupo.Enabled = false;
            this.grEstadoGrupo.Location = new System.Drawing.Point(26, 391);
            this.grEstadoGrupo.Name = "grEstadoGrupo";
            this.grEstadoGrupo.Size = new System.Drawing.Size(293, 51);
            this.grEstadoGrupo.TabIndex = 17;
            this.grEstadoGrupo.TabStop = false;
            this.grEstadoGrupo.Text = "Estado Viaje";
            this.grEstadoGrupo.Visible = false;
            // 
            // rbtEjecucion
            // 
            this.rbtEjecucion.AutoSize = true;
            this.rbtEjecucion.Location = new System.Drawing.Point(212, 23);
            this.rbtEjecucion.Name = "rbtEjecucion";
            this.rbtEjecucion.Size = new System.Drawing.Size(73, 17);
            this.rbtEjecucion.TabIndex = 2;
            this.rbtEjecucion.Text = "Ejecutado";
            this.rbtEjecucion.UseVisualStyleBackColor = true;
            this.rbtEjecucion.CheckedChanged += new System.EventHandler(this.rbtEjecucion_CheckedChanged);
            // 
            // rbtProgramado
            // 
            this.rbtProgramado.AutoSize = true;
            this.rbtProgramado.Location = new System.Drawing.Point(115, 23);
            this.rbtProgramado.Name = "rbtProgramado";
            this.rbtProgramado.Size = new System.Drawing.Size(82, 17);
            this.rbtProgramado.TabIndex = 1;
            this.rbtProgramado.Text = "Programado";
            this.rbtProgramado.UseVisualStyleBackColor = true;
            this.rbtProgramado.CheckedChanged += new System.EventHandler(this.rbtProgramado_CheckedChanged);
            // 
            // rbtCompletado
            // 
            this.rbtCompletado.AutoSize = true;
            this.rbtCompletado.Location = new System.Drawing.Point(18, 23);
            this.rbtCompletado.Name = "rbtCompletado";
            this.rbtCompletado.Size = new System.Drawing.Size(81, 17);
            this.rbtCompletado.TabIndex = 0;
            this.rbtCompletado.Text = "Completado";
            this.rbtCompletado.UseVisualStyleBackColor = true;
            this.rbtCompletado.CheckedChanged += new System.EventHandler(this.rbtCompletado_CheckedChanged);
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(371, 94);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(333, 10);
            this.lstConductor.TabIndex = 89;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.DoubleClick += new System.EventHandler(this.lstConductor_DoubleClick);
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            this.lstConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstConductor_KeyUp);
            // 
            // lvRuta
            // 
            this.lvRuta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lvRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvRuta.ForeColor = System.Drawing.Color.Navy;
            this.lvRuta.FullRowSelect = true;
            this.lvRuta.GridLines = true;
            this.lvRuta.Location = new System.Drawing.Point(581, 94);
            this.lvRuta.MultiSelect = false;
            this.lvRuta.Name = "lvRuta";
            this.lvRuta.Size = new System.Drawing.Size(335, 10);
            this.lvRuta.TabIndex = 117;
            this.lvRuta.UseCompatibleStateImageBehavior = false;
            this.lvRuta.View = System.Windows.Forms.View.Details;
            this.lvRuta.Visible = false;
            this.lvRuta.DoubleClick += new System.EventHandler(this.lvRuta_DoubleClick);
            this.lvRuta.Enter += new System.EventHandler(this.lvRuta_Enter);
            this.lvRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvRuta_KeyPress);
            this.lvRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvRuta_KeyUp);
            // 
            // dgv
            // 
            this.dgv.Location = new System.Drawing.Point(11, 60);
            this.dgv.MainView = this.dgvExportar;
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(275, 218);
            this.dgv.TabIndex = 9;
            this.dgv.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvExportar});
            this.dgv.Visible = false;
            // 
            // dgvExportar
            // 
            this.dgvExportar.GridControl = this.dgv;
            this.dgvExportar.Name = "dgvExportar";
            this.dgvExportar.OptionsBehavior.Editable = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.binocular;
            this.btnBuscar.Location = new System.Drawing.Point(856, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(94, 35);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // AnexarGuiasRetorno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1266, 582);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lstConductor);
            this.Controls.Add(this.lvRuta);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnexarGuiasRetorno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anexar Guias de Retorno";
            this.Load += new System.EventHandler(this.AnexarGuias_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViajesData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajeExpressVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajes)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grFiltroFecha.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grEstadoGrupo.ResumeLayout(false);
            this.grEstadoGrupo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExportar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkEstadoViaje;
        public System.Windows.Forms.DataGridView dgvViajes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox grFiltroFecha;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCodViaje;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNombreConductor;
        private System.Windows.Forms.GroupBox grEstadoGrupo;
        private System.Windows.Forms.RadioButton rbtEjecucion;
        private System.Windows.Forms.RadioButton rbtProgramado;
        private System.Windows.Forms.RadioButton rbtCompletado;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ListView lvRuta;
        private System.Windows.Forms.ListView lstConductor;
        private DevExpress.XtraGrid.GridControl dtgViajesData;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvViajeExpressVista;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODVIAJE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RUTA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idRuta1;
        private System.Windows.Forms.DataGridViewTextBoxColumn KM1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONDUCTOR1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idConductor1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLIENTE1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCliente1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUIATRANSP1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUIAREM1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idGuia1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRACTO1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROYECTO1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARRETA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTADO1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl dgv;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvExportar;
        private System.Windows.Forms.Button btnBuscar;
    }
}