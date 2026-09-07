namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmRegistroDocumentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroDocumentos));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.tsBtnGuardar = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTipoDocumento = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCompania = new System.Windows.Forms.ComboBox();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtCentroCosto = new System.Windows.Forms.TextBox();
            this.gbAdjuntaDoc = new System.Windows.Forms.GroupBox();
            this.txtRutaNube = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCerrarLocal = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtRutaLocal = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxTipoMoneda = new System.Windows.Forms.ComboBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFechaInicia = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxSucursal = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxTipoRelacion = new System.Windows.Forms.ComboBox();
            this.txtRelacion = new System.Windows.Forms.TextBox();
            this.lblRelacion = new System.Windows.Forms.Label();
            this.lstFiltroRelacion = new System.Windows.Forms.ListView();
            this.lstCentroCosto = new System.Windows.Forms.ListView();
            this.txtDescripcionCC = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkEsAfecto = new System.Windows.Forms.CheckBox();
            this.chkEsVencimiento = new System.Windows.Forms.CheckBox();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.txtTipoVehiculo = new System.Windows.Forms.TextBox();
            this.lblTipoVehiculo = new System.Windows.Forms.Label();
            this.lblObservacion = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cbxCategoria = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.gbAdjuntaDoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Khaki;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnSalir,
            this.tsBtnGuardar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(493, 33);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(49, 30);
            this.tsBtnSalir.Text = "Salir";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // tsBtnGuardar
            // 
            this.tsBtnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.tsBtnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnGuardar.Name = "tsBtnGuardar";
            this.tsBtnGuardar.Size = new System.Drawing.Size(69, 30);
            this.tsBtnGuardar.Text = "Guardar";
            this.tsBtnGuardar.Click += new System.EventHandler(this.tsBtnGuardar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tipo Documento:";
            // 
            // cbxTipoDocumento
            // 
            this.cbxTipoDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoDocumento.FormattingEnabled = true;
            this.cbxTipoDocumento.Location = new System.Drawing.Point(18, 268);
            this.cbxTipoDocumento.Name = "cbxTipoDocumento";
            this.cbxTipoDocumento.Size = new System.Drawing.Size(357, 21);
            this.cbxTipoDocumento.TabIndex = 11;
            this.cbxTipoDocumento.SelectedIndexChanged += new System.EventHandler(this.cbxTipoDocumento_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Compañia:";
            // 
            // cbxCompania
            // 
            this.cbxCompania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCompania.FormattingEnabled = true;
            this.cbxCompania.Location = new System.Drawing.Point(18, 68);
            this.cbxCompania.Name = "cbxCompania";
            this.cbxCompania.Size = new System.Drawing.Size(247, 21);
            this.cbxCompania.TabIndex = 1;
            this.cbxCompania.Tag = "1";
            this.cbxCompania.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxCompania_KeyPress);
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmision.Location = new System.Drawing.Point(18, 368);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaEmision.TabIndex = 16;
            this.dtpFechaEmision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaEmision_KeyPress);
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(247, 370);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaVencimiento.TabIndex = 18;
            this.dtpFechaVencimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaVencimiento_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Fecha Vencimiento:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Fecha Emisión:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(378, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Código:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Centro Costo:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(381, 268);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(92, 20);
            this.txtCodigo.TabIndex = 12;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // txtCentroCosto
            // 
            this.txtCentroCosto.Location = new System.Drawing.Point(19, 217);
            this.txtCentroCosto.Name = "txtCentroCosto";
            this.txtCentroCosto.Size = new System.Drawing.Size(97, 20);
            this.txtCentroCosto.TabIndex = 8;
            this.txtCentroCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCentroCosto_KeyPress);
            // 
            // gbAdjuntaDoc
            // 
            this.gbAdjuntaDoc.Controls.Add(this.txtRutaNube);
            this.gbAdjuntaDoc.Controls.Add(this.label14);
            this.gbAdjuntaDoc.Controls.Add(this.label13);
            this.gbAdjuntaDoc.Controls.Add(this.btnCerrarLocal);
            this.gbAdjuntaDoc.Controls.Add(this.btnBuscar);
            this.gbAdjuntaDoc.Controls.Add(this.txtRutaLocal);
            this.gbAdjuntaDoc.Location = new System.Drawing.Point(16, 403);
            this.gbAdjuntaDoc.Name = "gbAdjuntaDoc";
            this.gbAdjuntaDoc.Size = new System.Drawing.Size(456, 147);
            this.gbAdjuntaDoc.TabIndex = 16;
            this.gbAdjuntaDoc.TabStop = false;
            this.gbAdjuntaDoc.Text = "Directorios";
            // 
            // txtRutaNube
            // 
            this.txtRutaNube.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.txtRutaNube.Location = new System.Drawing.Point(9, 106);
            this.txtRutaNube.Name = "txtRutaNube";
            this.txtRutaNube.Size = new System.Drawing.Size(439, 30);
            this.txtRutaNube.TabIndex = 18;
            this.txtRutaNube.Text = "";
            this.txtRutaNube.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtRutaNube_LinkClicked);
            this.txtRutaNube.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRutaNube_KeyPress);
            this.txtRutaNube.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtRutaNube_MouseDoubleClick);
            this.txtRutaNube.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtRutaNube_MouseUp);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Nube:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Local:";
            // 
            // btnCerrarLocal
            // 
            this.btnCerrarLocal.Location = new System.Drawing.Point(422, 47);
            this.btnCerrarLocal.Name = "btnCerrarLocal";
            this.btnCerrarLocal.Size = new System.Drawing.Size(26, 21);
            this.btnCerrarLocal.TabIndex = 13;
            this.btnCerrarLocal.Text = "X";
            this.btnCerrarLocal.UseVisualStyleBackColor = true;
            this.btnCerrarLocal.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(392, 47);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(26, 21);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtRutaLocal
            // 
            this.txtRutaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaLocal.Location = new System.Drawing.Point(9, 43);
            this.txtRutaLocal.Name = "txtRutaLocal";
            this.txtRutaLocal.ReadOnly = true;
            this.txtRutaLocal.Size = new System.Drawing.Size(377, 30);
            this.txtRutaLocal.TabIndex = 17;
            this.txtRutaLocal.Text = "";
            this.txtRutaLocal.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtRutaLocal_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 303);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Tipo Moneda:";
            // 
            // cbxTipoMoneda
            // 
            this.cbxTipoMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoMoneda.FormattingEnabled = true;
            this.cbxTipoMoneda.Location = new System.Drawing.Point(16, 319);
            this.cbxTipoMoneda.Name = "cbxTipoMoneda";
            this.cbxTipoMoneda.Size = new System.Drawing.Size(88, 21);
            this.cbxTipoMoneda.TabIndex = 13;
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(111, 320);
            this.txtMonto.MaxLength = 7;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(79, 20);
            this.txtMonto.TabIndex = 14;
            this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto_KeyPress);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(108, 305);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(82, 13);
            this.lblMonto.TabIndex = 19;
            this.lblMonto.Text = "Monto con IGV:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(130, 354);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Fecha Inicia:";
            // 
            // dtpFechaInicia
            // 
            this.dtpFechaInicia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicia.Location = new System.Drawing.Point(133, 370);
            this.dtpFechaInicia.Name = "dtpFechaInicia";
            this.dtpFechaInicia.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaInicia.TabIndex = 17;
            this.dtpFechaInicia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicia_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(279, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Sucursal (Ubicación):";
            // 
            // cbxSucursal
            // 
            this.cbxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSucursal.FormattingEnabled = true;
            this.cbxSucursal.Location = new System.Drawing.Point(282, 68);
            this.cbxSucursal.Name = "cbxSucursal";
            this.cbxSucursal.Size = new System.Drawing.Size(190, 21);
            this.cbxSucursal.TabIndex = 2;
            this.cbxSucursal.Tag = "2";
            this.cbxSucursal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxSucursal_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Tipo Relación:";
            // 
            // cbxTipoRelacion
            // 
            this.cbxTipoRelacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoRelacion.FormattingEnabled = true;
            this.cbxTipoRelacion.Location = new System.Drawing.Point(18, 121);
            this.cbxTipoRelacion.Name = "cbxTipoRelacion";
            this.cbxTipoRelacion.Size = new System.Drawing.Size(98, 21);
            this.cbxTipoRelacion.TabIndex = 3;
            this.cbxTipoRelacion.SelectedIndexChanged += new System.EventHandler(this.cbxTipoRelacion_SelectedIndexChanged);
            this.cbxTipoRelacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxTipoRelacion_KeyPress);
            // 
            // txtRelacion
            // 
            this.txtRelacion.Location = new System.Drawing.Point(133, 121);
            this.txtRelacion.MaxLength = 200;
            this.txtRelacion.Name = "txtRelacion";
            this.txtRelacion.Size = new System.Drawing.Size(339, 20);
            this.txtRelacion.TabIndex = 4;
            this.txtRelacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRelacion_KeyPress);
            this.txtRelacion.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtRelacion_MouseDoubleClick);
            // 
            // lblRelacion
            // 
            this.lblRelacion.AutoSize = true;
            this.lblRelacion.Location = new System.Drawing.Point(131, 105);
            this.lblRelacion.Name = "lblRelacion";
            this.lblRelacion.Size = new System.Drawing.Size(126, 13);
            this.lblRelacion.TabIndex = 27;
            this.lblRelacion.Text = "Descripción de Relación:";
            // 
            // lstFiltroRelacion
            // 
            this.lstFiltroRelacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstFiltroRelacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFiltroRelacion.ForeColor = System.Drawing.Color.Navy;
            this.lstFiltroRelacion.FullRowSelect = true;
            this.lstFiltroRelacion.GridLines = true;
            this.lstFiltroRelacion.Location = new System.Drawing.Point(133, 142);
            this.lstFiltroRelacion.MultiSelect = false;
            this.lstFiltroRelacion.Name = "lstFiltroRelacion";
            this.lstFiltroRelacion.Size = new System.Drawing.Size(339, 10);
            this.lstFiltroRelacion.TabIndex = 5;
            this.lstFiltroRelacion.UseCompatibleStateImageBehavior = false;
            this.lstFiltroRelacion.View = System.Windows.Forms.View.Details;
            this.lstFiltroRelacion.Visible = false;
            this.lstFiltroRelacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstFiltroRelacion_KeyPress);
            this.lstFiltroRelacion.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFiltroRelacion_MouseDoubleClick);
            // 
            // lstCentroCosto
            // 
            this.lstCentroCosto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstCentroCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCentroCosto.ForeColor = System.Drawing.Color.Navy;
            this.lstCentroCosto.FullRowSelect = true;
            this.lstCentroCosto.GridLines = true;
            this.lstCentroCosto.Location = new System.Drawing.Point(19, 238);
            this.lstCentroCosto.MultiSelect = false;
            this.lstCentroCosto.Name = "lstCentroCosto";
            this.lstCentroCosto.Size = new System.Drawing.Size(336, 10);
            this.lstCentroCosto.TabIndex = 9;
            this.lstCentroCosto.UseCompatibleStateImageBehavior = false;
            this.lstCentroCosto.View = System.Windows.Forms.View.Details;
            this.lstCentroCosto.Visible = false;
            this.lstCentroCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCentroCosto_KeyPress);
            this.lstCentroCosto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstCentroCosto_MouseDoubleClick);
            // 
            // txtDescripcionCC
            // 
            this.txtDescripcionCC.Enabled = false;
            this.txtDescripcionCC.Location = new System.Drawing.Point(135, 217);
            this.txtDescripcionCC.Name = "txtDescripcionCC";
            this.txtDescripcionCC.Size = new System.Drawing.Size(240, 20);
            this.txtDescripcionCC.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(134, 201);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 13);
            this.label12.TabIndex = 88;
            this.label12.Text = "Descripción Centro de Costo:";
            // 
            // chkEsAfecto
            // 
            this.chkEsAfecto.AutoSize = true;
            this.chkEsAfecto.Location = new System.Drawing.Point(370, 360);
            this.chkEsAfecto.Name = "chkEsAfecto";
            this.chkEsAfecto.Size = new System.Drawing.Size(91, 30);
            this.chkEsAfecto.TabIndex = 15;
            this.chkEsAfecto.Text = "Validación de\r\nProgramación";
            this.chkEsAfecto.UseVisualStyleBackColor = true;
            this.chkEsAfecto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkEsAfecto_KeyPress);
            // 
            // chkEsVencimiento
            // 
            this.chkEsVencimiento.AutoSize = true;
            this.chkEsVencimiento.Location = new System.Drawing.Point(211, 321);
            this.chkEsVencimiento.Name = "chkEsVencimiento";
            this.chkEsVencimiento.Size = new System.Drawing.Size(84, 17);
            this.chkEsVencimiento.TabIndex = 19;
            this.chkEsVencimiento.Text = "Vencimiento\r\n";
            this.chkEsVencimiento.UseVisualStyleBackColor = true;
            this.chkEsVencimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkEsVencimiento_KeyPress);
            // 
            // txtMarca
            // 
            this.txtMarca.Enabled = false;
            this.txtMarca.Location = new System.Drawing.Point(18, 170);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(214, 20);
            this.txtMarca.TabIndex = 6;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(15, 154);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(40, 13);
            this.lblMarca.TabIndex = 91;
            this.lblMarca.Text = "Marca:";
            // 
            // txtTipoVehiculo
            // 
            this.txtTipoVehiculo.Enabled = false;
            this.txtTipoVehiculo.Location = new System.Drawing.Point(248, 170);
            this.txtTipoVehiculo.Name = "txtTipoVehiculo";
            this.txtTipoVehiculo.Size = new System.Drawing.Size(224, 20);
            this.txtTipoVehiculo.TabIndex = 7;
            // 
            // lblTipoVehiculo
            // 
            this.lblTipoVehiculo.AutoSize = true;
            this.lblTipoVehiculo.Location = new System.Drawing.Point(244, 154);
            this.lblTipoVehiculo.Name = "lblTipoVehiculo";
            this.lblTipoVehiculo.Size = new System.Drawing.Size(75, 13);
            this.lblTipoVehiculo.TabIndex = 93;
            this.lblTipoVehiculo.Text = "Tipo Vehiculo:";
            // 
            // lblObservacion
            // 
            this.lblObservacion.AutoSize = true;
            this.lblObservacion.Location = new System.Drawing.Point(19, 562);
            this.lblObservacion.Name = "lblObservacion";
            this.lblObservacion.Size = new System.Drawing.Size(70, 13);
            this.lblObservacion.TabIndex = 94;
            this.lblObservacion.Text = "Observacion:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(18, 579);
            this.txtObservacion.MaxLength = 200;
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(454, 77);
            this.txtObservacion.TabIndex = 95;
            this.txtObservacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservacion_KeyPress);
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(323, 306);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(80, 13);
            this.lblCategoria.TabIndex = 97;
            this.lblCategoria.Text = "Categorización:";
            // 
            // cbxCategoria
            // 
            this.cbxCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategoria.FormattingEnabled = true;
            this.cbxCategoria.Location = new System.Drawing.Point(325, 321);
            this.cbxCategoria.Name = "cbxCategoria";
            this.cbxCategoria.Size = new System.Drawing.Size(147, 21);
            this.cbxCategoria.TabIndex = 96;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(380, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 98;
            this.label8.Text = "Operacion:";
            // 
            // txtOperacion
            // 
            this.txtOperacion.Location = new System.Drawing.Point(381, 217);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(92, 20);
            this.txtOperacion.TabIndex = 99;
            // 
            // frmRegistroDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(493, 673);
            this.Controls.Add(this.txtOperacion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.cbxCategoria);
            this.Controls.Add(this.txtObservacion);
            this.Controls.Add(this.lblObservacion);
            this.Controls.Add(this.txtTipoVehiculo);
            this.Controls.Add(this.lblTipoVehiculo);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.chkEsVencimiento);
            this.Controls.Add(this.chkEsAfecto);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtDescripcionCC);
            this.Controls.Add(this.lstCentroCosto);
            this.Controls.Add(this.lstFiltroRelacion);
            this.Controls.Add(this.txtRelacion);
            this.Controls.Add(this.lblRelacion);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxTipoRelacion);
            this.Controls.Add(this.cbxSucursal);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpFechaInicia);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxTipoMoneda);
            this.Controls.Add(this.gbAdjuntaDoc);
            this.Controls.Add(this.txtCentroCosto);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpFechaVencimiento);
            this.Controls.Add(this.dtpFechaEmision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxTipoDocumento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxCompania);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmRegistroDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión Control Documentos";
            this.Load += new System.EventHandler(this.frmRegistroDocumentos_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbAdjuntaDoc.ResumeLayout(false);
            this.gbAdjuntaDoc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCentroCosto;
        private System.Windows.Forms.GroupBox gbAdjuntaDoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxTipoMoneda;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRelacion;
        private System.Windows.Forms.Label lblRelacion;
        private System.Windows.Forms.ListView lstFiltroRelacion;
        private System.Windows.Forms.ListView lstCentroCosto;
        private System.Windows.Forms.TextBox txtDescripcionCC;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkEsAfecto;
        public System.Windows.Forms.TextBox txtCodigo;
        public System.Windows.Forms.ComboBox cbxCompania;
        public System.Windows.Forms.ComboBox cbxSucursal;
        public System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblMarca;
        public System.Windows.Forms.TextBox txtTipoVehiculo;
        private System.Windows.Forms.Label lblTipoVehiculo;
        public System.Windows.Forms.DateTimePicker dtpFechaEmision;
        public System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        public System.Windows.Forms.DateTimePicker dtpFechaInicia;
        public System.Windows.Forms.ToolStripButton tsBtnGuardar;
        private System.Windows.Forms.Label lblObservacion;
        public System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cbxCategoria;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.Button btnBuscar;
        public System.Windows.Forms.Button btnCerrarLocal;
        public System.Windows.Forms.RichTextBox txtRutaNube;
        public System.Windows.Forms.RichTextBox txtRutaLocal;
        public System.Windows.Forms.CheckBox chkEsVencimiento;
        public System.Windows.Forms.ComboBox cbxTipoDocumento;
        public System.Windows.Forms.ComboBox cbxTipoRelacion;
    }
}