namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    partial class frmModificarFactura
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.tabModi = new System.Windows.Forms.TabControl();
            this.EnlaFac = new System.Windows.Forms.TabPage();
            this.Enlazar = new System.Windows.Forms.GroupBox();
            this.txtTipoDocR = new System.Windows.Forms.TextBox();
            this.txtSituacionF = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEnlazar = new System.Windows.Forms.Button();
            this.viaje = new System.Windows.Forms.Label();
            this.Guia = new System.Windows.Forms.Label();
            this.txtCodViaje = new System.Windows.Forms.TextBox();
            this.txtGuia1 = new System.Windows.Forms.TextBox();
            this.Desenlazar = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.GridCFactura1 = new DevExpress.XtraGrid.GridControl();
            this.EnlazaryDesenlazar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.ModFecha = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dtpFechaDocumento = new System.Windows.Forms.DateTimePicker();
            this.dtpFVencimientoOr = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.GridCFactura2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ModFac = new System.Windows.Forms.TabPage();
            this.GridCFactura3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTipDoc = new System.Windows.Forms.TextBox();
            this.txtCompSocio = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMonPenPago = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.DeleteFac = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCorrelativo = new System.Windows.Forms.TextBox();
            this.btnRestablecerCO = new System.Windows.Forms.Button();
            this.TxtCompania = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTipoDocumento = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDireLlegada = new System.Windows.Forms.TextBox();
            this.txtDirpartida = new System.Windows.Forms.TextBox();
            this.txtLLegada = new System.Windows.Forms.TextBox();
            this.txtPartida = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMontoDetraccion = new System.Windows.Forms.TextBox();
            this.lbldetra = new System.Windows.Forms.Label();
            this.tabModi.SuspendLayout();
            this.EnlaFac.SuspendLayout();
            this.Enlazar.SuspendLayout();
            this.Desenlazar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCFactura1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnlazaryDesenlazar)).BeginInit();
            this.ModFecha.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCFactura2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.ModFac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCFactura3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.DeleteFac.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabModi
            // 
            this.tabModi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabModi.Controls.Add(this.EnlaFac);
            this.tabModi.Controls.Add(this.ModFecha);
            this.tabModi.Controls.Add(this.ModFac);
            this.tabModi.Controls.Add(this.DeleteFac);
            this.tabModi.Controls.Add(this.tabPage1);
            this.tabModi.Location = new System.Drawing.Point(-4, 63);
            this.tabModi.Name = "tabModi";
            this.tabModi.SelectedIndex = 0;
            this.tabModi.Size = new System.Drawing.Size(1267, 612);
            this.tabModi.TabIndex = 2;
            this.tabModi.TabStop = false;
            this.tabModi.Click += new System.EventHandler(this.tabModi_Click);
            // 
            // EnlaFac
            // 
            this.EnlaFac.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.EnlaFac.Controls.Add(this.Enlazar);
            this.EnlaFac.Controls.Add(this.Desenlazar);
            this.EnlaFac.Location = new System.Drawing.Point(4, 22);
            this.EnlaFac.Name = "EnlaFac";
            this.EnlaFac.Padding = new System.Windows.Forms.Padding(3);
            this.EnlaFac.Size = new System.Drawing.Size(1259, 586);
            this.EnlaFac.TabIndex = 1;
            this.EnlaFac.Text = "Enlazar y Desenlazar";
            this.EnlaFac.Click += new System.EventHandler(this.EnlaFac_Click);
            // 
            // Enlazar
            // 
            this.Enlazar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Enlazar.Controls.Add(this.txtTipoDocR);
            this.Enlazar.Controls.Add(this.txtSituacionF);
            this.Enlazar.Controls.Add(this.label4);
            this.Enlazar.Controls.Add(this.label3);
            this.Enlazar.Controls.Add(this.btnEnlazar);
            this.Enlazar.Controls.Add(this.viaje);
            this.Enlazar.Controls.Add(this.Guia);
            this.Enlazar.Controls.Add(this.txtCodViaje);
            this.Enlazar.Controls.Add(this.txtGuia1);
            this.Enlazar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Enlazar.Location = new System.Drawing.Point(6, 29);
            this.Enlazar.Name = "Enlazar";
            this.Enlazar.Size = new System.Drawing.Size(1250, 153);
            this.Enlazar.TabIndex = 8;
            this.Enlazar.TabStop = false;
            this.Enlazar.Text = "Enlazar Viajes a la Factura";
            // 
            // txtTipoDocR
            // 
            this.txtTipoDocR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoDocR.Location = new System.Drawing.Point(634, 67);
            this.txtTipoDocR.Name = "txtTipoDocR";
            this.txtTipoDocR.ReadOnly = true;
            this.txtTipoDocR.Size = new System.Drawing.Size(96, 21);
            this.txtTipoDocR.TabIndex = 10;
            // 
            // txtSituacionF
            // 
            this.txtSituacionF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSituacionF.Location = new System.Drawing.Point(634, 27);
            this.txtSituacionF.Name = "txtSituacionF";
            this.txtSituacionF.ReadOnly = true;
            this.txtSituacionF.Size = new System.Drawing.Size(96, 21);
            this.txtSituacionF.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(464, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tipo de Documento Relación:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(464, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Estado de Facturación:";
            // 
            // btnEnlazar
            // 
            this.btnEnlazar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnlazar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnEnlazar.Location = new System.Drawing.Point(805, 35);
            this.btnEnlazar.Name = "btnEnlazar";
            this.btnEnlazar.Size = new System.Drawing.Size(97, 36);
            this.btnEnlazar.TabIndex = 5;
            this.btnEnlazar.Text = "Enlazar";
            this.btnEnlazar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnlazar.UseVisualStyleBackColor = true;
            this.btnEnlazar.Click += new System.EventHandler(this.button1_Click);
            // 
            // viaje
            // 
            this.viaje.AutoSize = true;
            this.viaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viaje.Location = new System.Drawing.Point(71, 35);
            this.viaje.Name = "viaje";
            this.viaje.Size = new System.Drawing.Size(83, 13);
            this.viaje.TabIndex = 4;
            this.viaje.Text = "Codigo De Viaje";
            // 
            // Guia
            // 
            this.Guia.AutoSize = true;
            this.Guia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Guia.Location = new System.Drawing.Point(71, 67);
            this.Guia.Name = "Guia";
            this.Guia.Size = new System.Drawing.Size(83, 13);
            this.Guia.TabIndex = 3;
            this.Guia.Text = "Guia Transporte";
            // 
            // txtCodViaje
            // 
            this.txtCodViaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodViaje.Location = new System.Drawing.Point(194, 29);
            this.txtCodViaje.Name = "txtCodViaje";
            this.txtCodViaje.Size = new System.Drawing.Size(195, 21);
            this.txtCodViaje.TabIndex = 1;
            this.txtCodViaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodViaje_KeyPress);
            // 
            // txtGuia1
            // 
            this.txtGuia1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuia1.Location = new System.Drawing.Point(194, 67);
            this.txtGuia1.Name = "txtGuia1";
            this.txtGuia1.Size = new System.Drawing.Size(195, 21);
            this.txtGuia1.TabIndex = 0;
            // 
            // Desenlazar
            // 
            this.Desenlazar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Desenlazar.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Desenlazar.Controls.Add(this.btnBuscar);
            this.Desenlazar.Controls.Add(this.GridCFactura1);
            this.Desenlazar.Controls.Add(this.btnQuitar);
            this.Desenlazar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desenlazar.ForeColor = System.Drawing.Color.Black;
            this.Desenlazar.Location = new System.Drawing.Point(0, 188);
            this.Desenlazar.Name = "Desenlazar";
            this.Desenlazar.Size = new System.Drawing.Size(1263, 392);
            this.Desenlazar.TabIndex = 7;
            this.Desenlazar.TabStop = false;
            this.Desenlazar.Text = "Desenlazar Viajes de la Factura";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_zoom_115406;
            this.btnBuscar.Location = new System.Drawing.Point(45, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(114, 34);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // GridCFactura1
            // 
            this.GridCFactura1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.RelationName = "Level1";
            this.GridCFactura1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridCFactura1.Location = new System.Drawing.Point(12, 65);
            this.GridCFactura1.MainView = this.EnlazaryDesenlazar;
            this.GridCFactura1.Name = "GridCFactura1";
            this.GridCFactura1.Size = new System.Drawing.Size(1239, 326);
            this.GridCFactura1.TabIndex = 6;
            this.GridCFactura1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.EnlazaryDesenlazar});
            // 
            // EnlazaryDesenlazar
            // 
            this.EnlazaryDesenlazar.GridControl = this.GridCFactura1;
            this.EnlazaryDesenlazar.Name = "EnlazaryDesenlazar";
            this.EnlazaryDesenlazar.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.EnlazaryDesenlazar.OptionsBehavior.Editable = false;
            this.EnlazaryDesenlazar.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.EnlazaryDesenlazar.OptionsView.ShowGroupPanel = false;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Enabled = false;
            this.btnQuitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.Image = global::ReportesTranspesa.Properties.Resources.menos;
            this.btnQuitar.Location = new System.Drawing.Point(811, 25);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(112, 31);
            this.btnQuitar.TabIndex = 7;
            this.btnQuitar.Text = "Desenlazar";
            this.btnQuitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // ModFecha
            // 
            this.ModFecha.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ModFecha.Controls.Add(this.groupBox3);
            this.ModFecha.Controls.Add(this.GridCFactura2);
            this.ModFecha.Location = new System.Drawing.Point(4, 22);
            this.ModFecha.Name = "ModFecha";
            this.ModFecha.Padding = new System.Windows.Forms.Padding(3);
            this.ModFecha.Size = new System.Drawing.Size(1259, 586);
            this.ModFecha.TabIndex = 0;
            this.ModFecha.Text = "Modificar Fecha ";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.dtpFechaDocumento);
            this.groupBox3.Controls.Add(this.dtpFVencimientoOr);
            this.groupBox3.Controls.Add(this.btnGuardar);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(6, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1245, 109);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Modifica la Fecha ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tipo de Pago:";
            this.label1.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "001 CONTADO",
            "002 PAGO A 30 DIAS"});
            this.comboBox1.Location = new System.Drawing.Point(587, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Visible = false;
            // 
            // dtpFechaDocumento
            // 
            this.dtpFechaDocumento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaDocumento.Location = new System.Drawing.Point(213, 24);
            this.dtpFechaDocumento.Name = "dtpFechaDocumento";
            this.dtpFechaDocumento.Size = new System.Drawing.Size(234, 20);
            this.dtpFechaDocumento.TabIndex = 11;
            // 
            // dtpFVencimientoOr
            // 
            this.dtpFVencimientoOr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFVencimientoOr.Location = new System.Drawing.Point(213, 61);
            this.dtpFVencimientoOr.Name = "dtpFVencimientoOr";
            this.dtpFVencimientoOr.Size = new System.Drawing.Size(234, 20);
            this.dtpFVencimientoOr.TabIndex = 10;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.Location = new System.Drawing.Point(840, 24);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(96, 37);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Fecha Documento:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(109, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Fecha Vencimiento:";
            // 
            // GridCFactura2
            // 
            this.GridCFactura2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode2.RelationName = "Level1";
            this.GridCFactura2.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.GridCFactura2.Location = new System.Drawing.Point(6, 118);
            this.GridCFactura2.MainView = this.gridView2;
            this.GridCFactura2.Name = "GridCFactura2";
            this.GridCFactura2.Size = new System.Drawing.Size(1245, 462);
            this.GridCFactura2.TabIndex = 8;
            this.GridCFactura2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.GridCFactura2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // ModFac
            // 
            this.ModFac.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ModFac.Controls.Add(this.GridCFactura3);
            this.ModFac.Controls.Add(this.groupBox2);
            this.ModFac.Location = new System.Drawing.Point(4, 22);
            this.ModFac.Name = "ModFac";
            this.ModFac.Size = new System.Drawing.Size(1259, 586);
            this.ModFac.TabIndex = 2;
            this.ModFac.Text = "Modificar Montos";
            // 
            // GridCFactura3
            // 
            this.GridCFactura3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridCFactura3.Location = new System.Drawing.Point(444, 12);
            this.GridCFactura3.MainView = this.gridView3;
            this.GridCFactura3.Name = "GridCFactura3";
            this.GridCFactura3.Size = new System.Drawing.Size(807, 554);
            this.GridCFactura3.TabIndex = 1;
            this.GridCFactura3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.GridCFactura3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.MultiSelect = true;
            this.gridView3.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groupBox2.Controls.Add(this.txtMontoDetraccion);
            this.groupBox2.Controls.Add(this.lbldetra);
            this.groupBox2.Controls.Add(this.txtTipDoc);
            this.groupBox2.Controls.Add(this.txtCompSocio);
            this.groupBox2.Controls.Add(this.btnAceptar);
            this.groupBox2.Controls.Add(this.btnCancelar);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtMonPenPago);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 305);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modificar Monto Pendiente Pago";
            // 
            // txtTipDoc
            // 
            this.txtTipDoc.Location = new System.Drawing.Point(173, 45);
            this.txtTipDoc.Name = "txtTipDoc";
            this.txtTipDoc.ReadOnly = true;
            this.txtTipDoc.Size = new System.Drawing.Size(205, 20);
            this.txtTipDoc.TabIndex = 12;
            // 
            // txtCompSocio
            // 
            this.txtCompSocio.Location = new System.Drawing.Point(173, 87);
            this.txtCompSocio.Name = "txtCompSocio";
            this.txtCompSocio.ReadOnly = true;
            this.txtCompSocio.Size = new System.Drawing.Size(205, 20);
            this.txtCompSocio.TabIndex = 11;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Enabled = false;
            this.btnAceptar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAceptar.Location = new System.Drawing.Point(218, 225);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(94, 41);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.btnCancelar.Location = new System.Drawing.Point(51, 225);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(87, 41);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(78, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Tipo Documento:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(80, 90);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Compañia Socio:";
            // 
            // txtMonPenPago
            // 
            this.txtMonPenPago.Location = new System.Drawing.Point(173, 139);
            this.txtMonPenPago.Name = "txtMonPenPago";
            this.txtMonPenPago.Size = new System.Drawing.Size(205, 20);
            this.txtMonPenPago.TabIndex = 2;
            this.txtMonPenPago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonPenPago_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(48, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Monto Pendiente Pago:";
            // 
            // DeleteFac
            // 
            this.DeleteFac.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.DeleteFac.Controls.Add(this.groupBox1);
            this.DeleteFac.Location = new System.Drawing.Point(4, 22);
            this.DeleteFac.Name = "DeleteFac";
            this.DeleteFac.Size = new System.Drawing.Size(1259, 586);
            this.DeleteFac.TabIndex = 3;
            this.DeleteFac.Text = "Eliminar Factura";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groupBox1.Controls.Add(this.txtCorrelativo);
            this.groupBox1.Controls.Add(this.btnRestablecerCO);
            this.groupBox1.Controls.Add(this.TxtCompania);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtTipoDocumento);
            this.groupBox1.Controls.Add(this.btnEliminar);
            this.groupBox1.Location = new System.Drawing.Point(-4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1263, 214);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elimina la  Factura";
            // 
            // txtCorrelativo
            // 
            this.txtCorrelativo.Location = new System.Drawing.Point(1022, 78);
            this.txtCorrelativo.Name = "txtCorrelativo";
            this.txtCorrelativo.ReadOnly = true;
            this.txtCorrelativo.Size = new System.Drawing.Size(141, 20);
            this.txtCorrelativo.TabIndex = 11;
            // 
            // btnRestablecerCO
            // 
            this.btnRestablecerCO.Location = new System.Drawing.Point(872, 69);
            this.btnRestablecerCO.Name = "btnRestablecerCO";
            this.btnRestablecerCO.Size = new System.Drawing.Size(144, 37);
            this.btnRestablecerCO.TabIndex = 10;
            this.btnRestablecerCO.Text = "Restablecer Correlativos";
            this.btnRestablecerCO.UseVisualStyleBackColor = true;
            this.btnRestablecerCO.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // TxtCompania
            // 
            this.TxtCompania.Location = new System.Drawing.Point(159, 47);
            this.TxtCompania.Name = "TxtCompania";
            this.TxtCompania.ReadOnly = true;
            this.TxtCompania.Size = new System.Drawing.Size(225, 20);
            this.TxtCompania.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Tipo De Documento:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(83, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Compañia:";
            // 
            // txtTipoDocumento
            // 
            this.txtTipoDocumento.Location = new System.Drawing.Point(159, 91);
            this.txtTipoDocumento.Name = "txtTipoDocumento";
            this.txtTipoDocumento.ReadOnly = true;
            this.txtTipoDocumento.Size = new System.Drawing.Size(225, 20);
            this.txtTipoDocumento.TabIndex = 5;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Image = global::ReportesTranspesa.Properties.Resources.deletedoc;
            this.btnEliminar.Location = new System.Drawing.Point(429, 60);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(99, 37);
            this.btnEliminar.TabIndex = 4;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.separatorControl1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtDireLlegada);
            this.tabPage1.Controls.Add(this.txtDirpartida);
            this.tabPage1.Controls.Add(this.txtLLegada);
            this.tabPage1.Controls.Add(this.txtPartida);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1259, 586);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Modificar y Liberar";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(35, 195);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(267, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Opción para liberar la Factura y volver a enviar al portal";
            // 
            // button2
            // 
            this.button2.Image = global::ReportesTranspesa.Properties.Resources.liberardoc;
            this.button2.Location = new System.Drawing.Point(38, 211);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(260, 42);
            this.button2.TabIndex = 10;
            this.button2.Text = "Liberar Reenvio a la Sunat";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(3, 142);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(1253, 23);
            this.separatorControl1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.button1.Location = new System.Drawing.Point(834, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 42);
            this.button1.TabIndex = 8;
            this.button1.Text = "Guardar";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtDireLlegada
            // 
            this.txtDireLlegada.Location = new System.Drawing.Point(387, 75);
            this.txtDireLlegada.Name = "txtDireLlegada";
            this.txtDireLlegada.Size = new System.Drawing.Size(360, 20);
            this.txtDireLlegada.TabIndex = 7;
            // 
            // txtDirpartida
            // 
            this.txtDirpartida.Location = new System.Drawing.Point(385, 30);
            this.txtDirpartida.Name = "txtDirpartida";
            this.txtDirpartida.Size = new System.Drawing.Size(362, 20);
            this.txtDirpartida.TabIndex = 6;
            this.txtDirpartida.TextChanged += new System.EventHandler(this.txtDirpartida_TextChanged);
            // 
            // txtLLegada
            // 
            this.txtLLegada.Location = new System.Drawing.Point(121, 75);
            this.txtLLegada.Name = "txtLLegada";
            this.txtLLegada.Size = new System.Drawing.Size(100, 20);
            this.txtLLegada.TabIndex = 5;
            // 
            // txtPartida
            // 
            this.txtPartida.Location = new System.Drawing.Point(121, 30);
            this.txtPartida.Name = "txtPartida";
            this.txtPartida.Size = new System.Drawing.Size(100, 20);
            this.txtPartida.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(288, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Dirección Llegada:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(288, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Dirección Partida:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Ubigeo Llegada:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ubigeo Partida:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(1, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Factura a Modificar :";
            // 
            // txtMontoDetraccion
            // 
            this.txtMontoDetraccion.Location = new System.Drawing.Point(173, 174);
            this.txtMontoDetraccion.Name = "txtMontoDetraccion";
            this.txtMontoDetraccion.Size = new System.Drawing.Size(205, 20);
            this.txtMontoDetraccion.TabIndex = 14;
            this.txtMontoDetraccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // lbldetra
            // 
            this.lbldetra.AutoSize = true;
            this.lbldetra.Location = new System.Drawing.Point(72, 177);
            this.lbldetra.Name = "lbldetra";
            this.lbldetra.Size = new System.Drawing.Size(95, 13);
            this.lbldetra.TabIndex = 13;
            this.lbldetra.Text = "Monto Detraccion:";
            // 
            // frmModificarFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(1263, 676);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabModi);
            this.Name = "frmModificarFactura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmModificarFactura";
            this.Load += new System.EventHandler(this.frmModificarFactura_Load);
            this.tabModi.ResumeLayout(false);
            this.EnlaFac.ResumeLayout(false);
            this.Enlazar.ResumeLayout(false);
            this.Enlazar.PerformLayout();
            this.Desenlazar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridCFactura1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnlazaryDesenlazar)).EndInit();
            this.ModFecha.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCFactura2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ModFac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridCFactura3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.DeleteFac.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage ModFecha;
        private System.Windows.Forms.TabPage EnlaFac;
        private System.Windows.Forms.TabPage ModFac;
        private System.Windows.Forms.TabPage DeleteFac;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox Desenlazar;
        private DevExpress.XtraGrid.GridControl GridCFactura1;
        private DevExpress.XtraGrid.Views.Grid.GridView EnlazaryDesenlazar;
        private System.Windows.Forms.TabControl tabModi;
        private System.Windows.Forms.GroupBox Enlazar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label viaje;
        private System.Windows.Forms.Label Guia;
        private System.Windows.Forms.TextBox txtCodViaje;
        private System.Windows.Forms.TextBox txtGuia1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.GridControl GridCFactura2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.DateTimePicker dtpFechaDocumento;
        private System.Windows.Forms.DateTimePicker dtpFVencimientoOr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTipDoc;
        private System.Windows.Forms.TextBox txtCompSocio;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMonPenPago;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTipoDocumento;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl GridCFactura3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.TextBox TxtCompania;
        private System.Windows.Forms.TextBox txtTipoDocR;
        private System.Windows.Forms.TextBox txtSituacionF;
        private System.Windows.Forms.TextBox txtCorrelativo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Button btnBuscar;
        public System.Windows.Forms.Button btnQuitar;
        public System.Windows.Forms.Button btnEnlazar;
        public System.Windows.Forms.Button btnGuardar;
        public System.Windows.Forms.Button btnEliminar;
        public System.Windows.Forms.Button btnAceptar;
        public System.Windows.Forms.Button btnRestablecerCO;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtDireLlegada;
        private System.Windows.Forms.TextBox txtDirpartida;
        private System.Windows.Forms.TextBox txtLLegada;
        private System.Windows.Forms.TextBox txtPartida;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtMontoDetraccion;
        private System.Windows.Forms.Label lbldetra;


    }
}