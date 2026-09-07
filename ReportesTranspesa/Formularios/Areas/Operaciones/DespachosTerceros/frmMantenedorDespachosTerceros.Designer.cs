namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmMantenedorDespachosTerceros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantenedorDespachosTerceros));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.tsBtnGuardar = new System.Windows.Forms.ToolStripButton();
            this.dtpFechaDespacho = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblRelacion = new System.Windows.Forms.Label();
            this.lstFiltroPlacas = new System.Windows.Forms.ListView();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKilometraje = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.lstFiltroConductor = new System.Windows.Forms.ListView();
            this.cbxProducto = new System.Windows.Forms.ComboBox();
            this.cbxProveedor = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.cbxLugar = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPreviaje = new System.Windows.Forms.Label();
            this.txtCodPreviaje = new System.Windows.Forms.TextBox();
            this.txtTotalizador = new System.Windows.Forms.TextBox();
            this.lblTotalizador = new System.Windows.Forms.Label();
            this.pInsUnidades = new System.Windows.Forms.Panel();
            this.lblInspeccion = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDiasPendientes = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.txtFechaInspeccion = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.pInsUnidades.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(452, 33);
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
            // dtpFechaDespacho
            // 
            this.dtpFechaDespacho.CustomFormat = "";
            this.dtpFechaDespacho.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDespacho.Location = new System.Drawing.Point(12, 205);
            this.dtpFechaDespacho.Name = "dtpFechaDespacho";
            this.dtpFechaDespacho.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaDespacho.TabIndex = 6;
            this.dtpFechaDespacho.Value = new System.DateTime(2022, 7, 2, 10, 47, 51, 0);
            this.dtpFechaDespacho.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaDespacho_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Fecha Despacho:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Código Ticket:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(343, 205);
            this.txtCodigo.MaxLength = 8;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(85, 20);
            this.txtCodigo.TabIndex = 10;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(146, 257);
            this.txtCantidad.MaxLength = 9;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(115, 20);
            this.txtCantidad.TabIndex = 12;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(143, 242);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(52, 13);
            this.lblMonto.TabIndex = 19;
            this.lblMonto.Text = "Cantidad:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(230, 205);
            this.txtPlaca.MaxLength = 9;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(103, 20);
            this.txtPlaca.TabIndex = 8;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            this.txtPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtPlaca_MouseDoubleClick);
            // 
            // lblRelacion
            // 
            this.lblRelacion.AutoSize = true;
            this.lblRelacion.Location = new System.Drawing.Point(228, 189);
            this.lblRelacion.Name = "lblRelacion";
            this.lblRelacion.Size = new System.Drawing.Size(37, 13);
            this.lblRelacion.TabIndex = 27;
            this.lblRelacion.Text = "Placa:";
            // 
            // lstFiltroPlacas
            // 
            this.lstFiltroPlacas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstFiltroPlacas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFiltroPlacas.ForeColor = System.Drawing.Color.Navy;
            this.lstFiltroPlacas.FullRowSelect = true;
            this.lstFiltroPlacas.GridLines = true;
            this.lstFiltroPlacas.Location = new System.Drawing.Point(231, 227);
            this.lstFiltroPlacas.MultiSelect = false;
            this.lstFiltroPlacas.Name = "lstFiltroPlacas";
            this.lstFiltroPlacas.Size = new System.Drawing.Size(102, 10);
            this.lstFiltroPlacas.TabIndex = 9;
            this.lstFiltroPlacas.UseCompatibleStateImageBehavior = false;
            this.lstFiltroPlacas.View = System.Windows.Forms.View.Details;
            this.lstFiltroPlacas.Visible = false;
            this.lstFiltroPlacas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstFiltroPlacas_KeyPress);
            this.lstFiltroPlacas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFiltroPlacas_MouseDoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(302, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 88;
            this.label12.Text = "Producto:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(400, 258);
            this.txtPrecio.MaxLength = 8;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.ReadOnly = true;
            this.txtPrecio.Size = new System.Drawing.Size(16, 20);
            this.txtPrecio.TabIndex = 13;
            this.txtPrecio.Text = "0";
            this.txtPrecio.Visible = false;
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "Precio Unitario:";
            this.label1.Visible = false;
            // 
            // txtKilometraje
            // 
            this.txtKilometraje.Location = new System.Drawing.Point(11, 257);
            this.txtKilometraje.MaxLength = 9;
            this.txtKilometraje.Name = "txtKilometraje";
            this.txtKilometraje.Size = new System.Drawing.Size(124, 20);
            this.txtKilometraje.TabIndex = 11;
            this.txtKilometraje.Text = "0";
            this.txtKilometraje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKilometraje_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "Odometro:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 99;
            this.label2.Text = "Proveedor:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "Conductor:";
            // 
            // txtConductor
            // 
            this.txtConductor.Location = new System.Drawing.Point(11, 149);
            this.txtConductor.MaxLength = 200;
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(276, 20);
            this.txtConductor.TabIndex = 3;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(303, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 103;
            this.label7.Text = "Dni:";
            // 
            // txtDni
            // 
            this.txtDni.Enabled = false;
            this.txtDni.Location = new System.Drawing.Point(306, 149);
            this.txtDni.MaxLength = 8;
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(121, 20);
            this.txtDni.TabIndex = 5;
            // 
            // lstFiltroConductor
            // 
            this.lstFiltroConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstFiltroConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFiltroConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstFiltroConductor.FullRowSelect = true;
            this.lstFiltroConductor.GridLines = true;
            this.lstFiltroConductor.Location = new System.Drawing.Point(10, 171);
            this.lstFiltroConductor.MultiSelect = false;
            this.lstFiltroConductor.Name = "lstFiltroConductor";
            this.lstFiltroConductor.Size = new System.Drawing.Size(277, 10);
            this.lstFiltroConductor.TabIndex = 4;
            this.lstFiltroConductor.UseCompatibleStateImageBehavior = false;
            this.lstFiltroConductor.View = System.Windows.Forms.View.Details;
            this.lstFiltroConductor.Visible = false;
            this.lstFiltroConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstFiltroConductor_KeyPress);
            this.lstFiltroConductor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFiltroConductor_MouseDoubleClick);
            // 
            // cbxProducto
            // 
            this.cbxProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProducto.FormattingEnabled = true;
            this.cbxProducto.Items.AddRange(new object[] {
            "PETROLEO",
            "GAS GNL",
            "GASOLINA 95",
            "UREA"});
            this.cbxProducto.Location = new System.Drawing.Point(306, 58);
            this.cbxProducto.Name = "cbxProducto";
            this.cbxProducto.Size = new System.Drawing.Size(121, 21);
            this.cbxProducto.TabIndex = 2;
            this.cbxProducto.TextChanged += new System.EventHandler(this.cbxProducto_TextChanged);
            this.cbxProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxProducto_KeyPress);
            // 
            // cbxProveedor
            // 
            this.cbxProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProveedor.FormattingEnabled = true;
            this.cbxProveedor.Location = new System.Drawing.Point(11, 58);
            this.cbxProveedor.Name = "cbxProveedor";
            this.cbxProveedor.Size = new System.Drawing.Size(276, 21);
            this.cbxProveedor.TabIndex = 1;
            this.cbxProveedor.SelectedIndexChanged += new System.EventHandler(this.cbxProveedor_SelectedIndexChanged);
            this.cbxProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxProveedor_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(118, 189);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 13);
            this.label17.TabIndex = 104;
            this.label17.Text = "Hora:";
            // 
            // dtpHora
            // 
            this.dtpHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHora.Location = new System.Drawing.Point(121, 205);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.Size = new System.Drawing.Size(97, 20);
            this.dtpHora.TabIndex = 7;
            this.dtpHora.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpHora_KeyPress);
            // 
            // cbxLugar
            // 
            this.cbxLugar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLugar.FormattingEnabled = true;
            this.cbxLugar.Location = new System.Drawing.Point(119, 85);
            this.cbxLugar.Name = "cbxLugar";
            this.cbxLugar.Size = new System.Drawing.Size(168, 21);
            this.cbxLugar.TabIndex = 105;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 106;
            this.label8.Text = "Lugar:";
            // 
            // lblPreviaje
            // 
            this.lblPreviaje.AutoSize = true;
            this.lblPreviaje.Location = new System.Drawing.Point(271, 242);
            this.lblPreviaje.Name = "lblPreviaje";
            this.lblPreviaje.Size = new System.Drawing.Size(73, 13);
            this.lblPreviaje.TabIndex = 107;
            this.lblPreviaje.Text = "Cod. Previaje:";
            this.lblPreviaje.Visible = false;
            // 
            // txtCodPreviaje
            // 
            this.txtCodPreviaje.Location = new System.Drawing.Point(272, 258);
            this.txtCodPreviaje.MaxLength = 8;
            this.txtCodPreviaje.Name = "txtCodPreviaje";
            this.txtCodPreviaje.Size = new System.Drawing.Size(121, 20);
            this.txtCodPreviaje.TabIndex = 108;
            this.txtCodPreviaje.Text = "0";
            this.txtCodPreviaje.Visible = false;
            // 
            // txtTotalizador
            // 
            this.txtTotalizador.Location = new System.Drawing.Point(304, 104);
            this.txtTotalizador.MaxLength = 8;
            this.txtTotalizador.Name = "txtTotalizador";
            this.txtTotalizador.Size = new System.Drawing.Size(121, 20);
            this.txtTotalizador.TabIndex = 110;
            this.txtTotalizador.Visible = false;
            // 
            // lblTotalizador
            // 
            this.lblTotalizador.AutoSize = true;
            this.lblTotalizador.Location = new System.Drawing.Point(303, 88);
            this.lblTotalizador.Name = "lblTotalizador";
            this.lblTotalizador.Size = new System.Drawing.Size(64, 13);
            this.lblTotalizador.TabIndex = 109;
            this.lblTotalizador.Text = "Contometro:";
            this.lblTotalizador.Visible = false;
            // 
            // pInsUnidades
            // 
            this.pInsUnidades.BackColor = System.Drawing.Color.Bisque;
            this.pInsUnidades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pInsUnidades.Controls.Add(this.lblInspeccion);
            this.pInsUnidades.Controls.Add(this.label47);
            this.pInsUnidades.Controls.Add(this.lblUnidad);
            this.pInsUnidades.Controls.Add(this.label10);
            this.pInsUnidades.Controls.Add(this.txtDiasPendientes);
            this.pInsUnidades.Controls.Add(this.label54);
            this.pInsUnidades.Controls.Add(this.txtFechaInspeccion);
            this.pInsUnidades.Controls.Add(this.label52);
            this.pInsUnidades.Controls.Add(this.label55);
            this.pInsUnidades.Controls.Add(this.btnCerrar);
            this.pInsUnidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pInsUnidades.Location = new System.Drawing.Point(99, 138);
            this.pInsUnidades.Name = "pInsUnidades";
            this.pInsUnidades.Size = new System.Drawing.Size(255, 170);
            this.pInsUnidades.TabIndex = 155;
            this.pInsUnidades.Visible = false;
            // 
            // lblInspeccion
            // 
            this.lblInspeccion.AutoSize = true;
            this.lblInspeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspeccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(193)))), ((int)(((byte)(0)))));
            this.lblInspeccion.Location = new System.Drawing.Point(10, 133);
            this.lblInspeccion.Name = "lblInspeccion";
            this.lblInspeccion.Size = new System.Drawing.Size(96, 15);
            this.lblInspeccion.TabIndex = 161;
            this.lblInspeccion.Text = "QUINQUENAL";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(10, 109);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(69, 15);
            this.label47.TabIndex = 160;
            this.label47.Text = "Inspección:";
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(193)))), ((int)(((byte)(0)))));
            this.lblUnidad.Location = new System.Drawing.Point(10, 78);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(62, 15);
            this.lblUnidad.TabIndex = 159;
            this.lblUnidad.Text = "ABC-999";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 15);
            this.label10.TabIndex = 158;
            this.label10.Text = "Unidad:";
            // 
            // txtDiasPendientes
            // 
            this.txtDiasPendientes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDiasPendientes.Location = new System.Drawing.Point(123, 130);
            this.txtDiasPendientes.MaxLength = 8;
            this.txtDiasPendientes.Name = "txtDiasPendientes";
            this.txtDiasPendientes.ReadOnly = true;
            this.txtDiasPendientes.Size = new System.Drawing.Size(113, 21);
            this.txtDiasPendientes.TabIndex = 3;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(120, 109);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(88, 15);
            this.label54.TabIndex = 1;
            this.label54.Text = "Días restantes:";
            // 
            // txtFechaInspeccion
            // 
            this.txtFechaInspeccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFechaInspeccion.Location = new System.Drawing.Point(123, 75);
            this.txtFechaInspeccion.MaxLength = 4;
            this.txtFechaInspeccion.Name = "txtFechaInspeccion";
            this.txtFechaInspeccion.ReadOnly = true;
            this.txtFechaInspeccion.Size = new System.Drawing.Size(113, 21);
            this.txtFechaInspeccion.TabIndex = 2;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(193)))), ((int)(((byte)(0)))));
            this.label52.Location = new System.Drawing.Point(10, 9);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(201, 32);
            this.label52.TabIndex = 4;
            this.label52.Text = "INSPECCIÓN DE TANQUES\r\nGNL PENDIENTE";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(120, 54);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(106, 15);
            this.label55.TabIndex = 0;
            this.label55.Text = "Fecha Inspección:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(235, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(19, 21);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // frmMantenedorDespachosTerceros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(452, 325);
            this.Controls.Add(this.txtTotalizador);
            this.Controls.Add(this.lblTotalizador);
            this.Controls.Add(this.txtCodPreviaje);
            this.Controls.Add(this.lblPreviaje);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbxLugar);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dtpHora);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxProveedor);
            this.Controls.Add(this.cbxProducto);
            this.Controls.Add(this.lstFiltroConductor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtConductor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKilometraje);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lstFiltroPlacas);
            this.Controls.Add(this.txtPlaca);
            this.Controls.Add(this.lblRelacion);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFechaDespacho);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pInsUnidades);
            this.Name = "frmMantenedorDespachosTerceros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenedor de Tickets Lima y Terceros";
            this.Load += new System.EventHandler(this.frmRegistroDocumentos_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pInsUnidades.ResumeLayout(false);
            this.pInsUnidades.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripButton tsBtnGuardar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label lblRelacion;
        private System.Windows.Forms.ListView lstFiltroPlacas;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtCodigo;
        public System.Windows.Forms.DateTimePicker dtpFechaDespacho;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCantidad;
        public System.Windows.Forms.TextBox txtPlaca;
        public System.Windows.Forms.TextBox txtPrecio;
        public System.Windows.Forms.TextBox txtKilometraje;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.ListView lstFiltroConductor;
        public System.Windows.Forms.ComboBox cbxProducto;
        public System.Windows.Forms.ComboBox cbxProveedor;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox cbxLugar;
        private System.Windows.Forms.Label lblPreviaje;
        public System.Windows.Forms.TextBox txtCodPreviaje;
        public System.Windows.Forms.TextBox txtTotalizador;
        private System.Windows.Forms.Label lblTotalizador;
        private System.Windows.Forms.Panel pInsUnidades;
        private System.Windows.Forms.TextBox txtDiasPendientes;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txtFechaInspeccion;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Button btnCerrar;
        public System.Windows.Forms.Label lblInspeccion;
        private System.Windows.Forms.Label label47;
        public System.Windows.Forms.Label lblUnidad;
        private System.Windows.Forms.Label label10;
    }
}