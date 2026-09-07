namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class FrmLiquidaPlanilla
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLiquidaPlanilla));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.tsBtnGuardar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscarViajes = new System.Windows.Forms.Button();
            this.lblRuta = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCodigoViaje = new System.Windows.Forms.Label();
            this.lblTituloViaje = new System.Windows.Forms.Label();
            this.gbFecha = new System.Windows.Forms.GroupBox();
            this.DTPfecha = new System.Windows.Forms.DateTimePicker();
            this.gbPllanilaLiq = new System.Windows.Forms.GroupBox();
            this.lblTituloPlanilla = new System.Windows.Forms.Label();
            this.lblPlanilla = new System.Windows.Forms.Label();
            this.lblTituloImporteTrujillo = new System.Windows.Forms.Label();
            this.lblTituloImporteLima = new System.Windows.Forms.Label();
            this.lblImporteLima = new System.Windows.Forms.Label();
            this.lblImporteTrujillo = new System.Windows.Forms.Label();
            this.gbGastos = new System.Windows.Forms.GroupBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btoGrabaVuelto = new System.Windows.Forms.Button();
            this.chkvuelto = new System.Windows.Forms.CheckBox();
            this.txtdescuento = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.txtReintegro = new System.Windows.Forms.TextBox();
            this.txtporrendir = new System.Windows.Forms.TextBox();
            this.txtxrendir = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.cbxGastos = new System.Windows.Forms.ComboBox();
            this.dgvDetalleliquidacion = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaGasto = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbFecha.SuspendLayout();
            this.gbPllanilaLiq.SuspendLayout();
            this.gbGastos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleliquidacion)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DarkKhaki;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsBtnSalir,
            this.tsBtnGuardar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(954, 43);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(332, 40);
            this.toolStripLabel1.Text = "LIQUIDACIÓN DE PLANILLA";
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(63, 40);
            this.tsBtnSalir.Text = "Salir";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // tsBtnGuardar
            // 
            this.tsBtnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.tsBtnGuardar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnGuardar.Margin = new System.Windows.Forms.Padding(30, 1, 0, 2);
            this.tsBtnGuardar.Name = "tsBtnGuardar";
            this.tsBtnGuardar.Size = new System.Drawing.Size(85, 40);
            this.tsBtnGuardar.Text = "&Guardar";
            this.tsBtnGuardar.Click += new System.EventHandler(this.tsBtnGuardar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscarViajes);
            this.groupBox1.Controls.Add(this.lblRuta);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblPlaca);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblCodigoViaje);
            this.groupBox1.Controls.Add(this.lblTituloViaje);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(471, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 67);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VIAJE";
            // 
            // btnBuscarViajes
            // 
            this.btnBuscarViajes.Location = new System.Drawing.Point(375, 16);
            this.btnBuscarViajes.Name = "btnBuscarViajes";
            this.btnBuscarViajes.Size = new System.Drawing.Size(89, 30);
            this.btnBuscarViajes.TabIndex = 50;
            this.btnBuscarViajes.Text = "Buscar viaje";
            this.btnBuscarViajes.UseVisualStyleBackColor = true;
            this.btnBuscarViajes.Click += new System.EventHandler(this.btnBuscarViajes_Click);
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuta.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblRuta.Location = new System.Drawing.Point(89, 43);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(13, 17);
            this.lblRuta.TabIndex = 47;
            this.lblRuta.Text = " ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Ruta de Viaje:";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaca.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPlaca.Location = new System.Drawing.Point(263, 21);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(13, 17);
            this.lblPlaca.TabIndex = 45;
            this.lblPlaca.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(177, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Unidad del Viaje:";
            // 
            // lblCodigoViaje
            // 
            this.lblCodigoViaje.AutoSize = true;
            this.lblCodigoViaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCodigoViaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoViaje.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCodigoViaje.Location = new System.Drawing.Point(88, 19);
            this.lblCodigoViaje.Name = "lblCodigoViaje";
            this.lblCodigoViaje.Size = new System.Drawing.Size(13, 17);
            this.lblCodigoViaje.TabIndex = 43;
            this.lblCodigoViaje.Text = " ";
            // 
            // lblTituloViaje
            // 
            this.lblTituloViaje.AutoSize = true;
            this.lblTituloViaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloViaje.Location = new System.Drawing.Point(17, 22);
            this.lblTituloViaje.Name = "lblTituloViaje";
            this.lblTituloViaje.Size = new System.Drawing.Size(71, 13);
            this.lblTituloViaje.TabIndex = 44;
            this.lblTituloViaje.Text = "Nro. de Viaje:";
            // 
            // gbFecha
            // 
            this.gbFecha.Controls.Add(this.DTPfecha);
            this.gbFecha.Enabled = false;
            this.gbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFecha.Location = new System.Drawing.Point(12, 50);
            this.gbFecha.Name = "gbFecha";
            this.gbFecha.Size = new System.Drawing.Size(116, 67);
            this.gbFecha.TabIndex = 39;
            this.gbFecha.TabStop = false;
            this.gbFecha.Text = "FECHA";
            // 
            // DTPfecha
            // 
            this.DTPfecha.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPfecha.CalendarMonthBackground = System.Drawing.SystemColors.MenuHighlight;
            this.DTPfecha.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DTPfecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPfecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPfecha.Location = new System.Drawing.Point(7, 27);
            this.DTPfecha.Name = "DTPfecha";
            this.DTPfecha.Size = new System.Drawing.Size(98, 20);
            this.DTPfecha.TabIndex = 10;
            // 
            // gbPllanilaLiq
            // 
            this.gbPllanilaLiq.Controls.Add(this.lblTituloPlanilla);
            this.gbPllanilaLiq.Controls.Add(this.lblPlanilla);
            this.gbPllanilaLiq.Controls.Add(this.lblTituloImporteTrujillo);
            this.gbPllanilaLiq.Controls.Add(this.lblTituloImporteLima);
            this.gbPllanilaLiq.Controls.Add(this.lblImporteLima);
            this.gbPllanilaLiq.Controls.Add(this.lblImporteTrujillo);
            this.gbPllanilaLiq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPllanilaLiq.Location = new System.Drawing.Point(134, 50);
            this.gbPllanilaLiq.Name = "gbPllanilaLiq";
            this.gbPllanilaLiq.Size = new System.Drawing.Size(331, 67);
            this.gbPllanilaLiq.TabIndex = 45;
            this.gbPllanilaLiq.TabStop = false;
            this.gbPllanilaLiq.Text = "PLANILLA";
            // 
            // lblTituloPlanilla
            // 
            this.lblTituloPlanilla.AutoSize = true;
            this.lblTituloPlanilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloPlanilla.Location = new System.Drawing.Point(22, 22);
            this.lblTituloPlanilla.Name = "lblTituloPlanilla";
            this.lblTituloPlanilla.Size = new System.Drawing.Size(63, 13);
            this.lblTituloPlanilla.TabIndex = 8;
            this.lblTituloPlanilla.Text = "Nro Planilla:";
            // 
            // lblPlanilla
            // 
            this.lblPlanilla.AutoSize = true;
            this.lblPlanilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPlanilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanilla.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPlanilla.Location = new System.Drawing.Point(86, 19);
            this.lblPlanilla.Name = "lblPlanilla";
            this.lblPlanilla.Size = new System.Drawing.Size(13, 17);
            this.lblPlanilla.TabIndex = 2;
            this.lblPlanilla.Text = " ";
            // 
            // lblTituloImporteTrujillo
            // 
            this.lblTituloImporteTrujillo.AutoSize = true;
            this.lblTituloImporteTrujillo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloImporteTrujillo.Location = new System.Drawing.Point(8, 45);
            this.lblTituloImporteTrujillo.Name = "lblTituloImporteTrujillo";
            this.lblTituloImporteTrujillo.Size = new System.Drawing.Size(78, 13);
            this.lblTituloImporteTrujillo.TabIndex = 9;
            this.lblTituloImporteTrujillo.Text = "Importe Trujillo:";
            // 
            // lblTituloImporteLima
            // 
            this.lblTituloImporteLima.AutoSize = true;
            this.lblTituloImporteLima.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloImporteLima.Location = new System.Drawing.Point(175, 44);
            this.lblTituloImporteLima.Name = "lblTituloImporteLima";
            this.lblTituloImporteLima.Size = new System.Drawing.Size(70, 13);
            this.lblTituloImporteLima.TabIndex = 42;
            this.lblTituloImporteLima.Text = "Importe Lima:";
            // 
            // lblImporteLima
            // 
            this.lblImporteLima.AutoSize = true;
            this.lblImporteLima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImporteLima.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporteLima.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblImporteLima.Location = new System.Drawing.Point(246, 43);
            this.lblImporteLima.Name = "lblImporteLima";
            this.lblImporteLima.Size = new System.Drawing.Size(13, 17);
            this.lblImporteLima.TabIndex = 41;
            this.lblImporteLima.Text = " ";
            // 
            // lblImporteTrujillo
            // 
            this.lblImporteTrujillo.AutoSize = true;
            this.lblImporteTrujillo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImporteTrujillo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporteTrujillo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblImporteTrujillo.Location = new System.Drawing.Point(86, 43);
            this.lblImporteTrujillo.Name = "lblImporteTrujillo";
            this.lblImporteTrujillo.Size = new System.Drawing.Size(13, 17);
            this.lblImporteTrujillo.TabIndex = 3;
            this.lblImporteTrujillo.Text = " ";
            // 
            // gbGastos
            // 
            this.gbGastos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGastos.Controls.Add(this.dtpFechaGasto);
            this.gbGastos.Controls.Add(this.label3);
            this.gbGastos.Controls.Add(this.label1);
            this.gbGastos.Controls.Add(this.btnQuitar);
            this.gbGastos.Controls.Add(this.dgvDetalleliquidacion);
            this.gbGastos.Controls.Add(this.cbxGastos);
            this.gbGastos.Controls.Add(this.txtImporte);
            this.gbGastos.Controls.Add(this.btoGrabaVuelto);
            this.gbGastos.Controls.Add(this.btnAgregar);
            this.gbGastos.Controls.Add(this.chkvuelto);
            this.gbGastos.Controls.Add(this.txtdescuento);
            this.gbGastos.Controls.Add(this.Label11);
            this.gbGastos.Controls.Add(this.txtReintegro);
            this.gbGastos.Controls.Add(this.txtporrendir);
            this.gbGastos.Controls.Add(this.txtxrendir);
            this.gbGastos.Controls.Add(this.Label9);
            this.gbGastos.Controls.Add(this.Label10);
            this.gbGastos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGastos.Location = new System.Drawing.Point(12, 136);
            this.gbGastos.Name = "gbGastos";
            this.gbGastos.Size = new System.Drawing.Size(930, 388);
            this.gbGastos.TabIndex = 37;
            this.gbGastos.TabStop = false;
            this.gbGastos.Text = "DETALLE DE GASTOS";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(461, 32);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(60, 20);
            this.txtImporte.TabIndex = 12;
            this.txtImporte.TextChanged += new System.EventHandler(this.txtImporte_TextChanged);
            this.txtImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporte_KeyPress);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(745, 22);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAgregar.Size = new System.Drawing.Size(81, 37);
            this.btnAgregar.TabIndex = 30;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btoGrabaVuelto
            // 
            this.btoGrabaVuelto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btoGrabaVuelto.Enabled = false;
            this.btoGrabaVuelto.Image = ((System.Drawing.Image)(resources.GetObject("btoGrabaVuelto.Image")));
            this.btoGrabaVuelto.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btoGrabaVuelto.Location = new System.Drawing.Point(892, 349);
            this.btoGrabaVuelto.Name = "btoGrabaVuelto";
            this.btoGrabaVuelto.Size = new System.Drawing.Size(30, 27);
            this.btoGrabaVuelto.TabIndex = 28;
            this.btoGrabaVuelto.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btoGrabaVuelto.UseVisualStyleBackColor = true;
            // 
            // chkvuelto
            // 
            this.chkvuelto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkvuelto.AutoSize = true;
            this.chkvuelto.Location = new System.Drawing.Point(734, 354);
            this.chkvuelto.Name = "chkvuelto";
            this.chkvuelto.Size = new System.Drawing.Size(66, 17);
            this.chkvuelto.TabIndex = 27;
            this.chkvuelto.Text = "Descto";
            this.chkvuelto.UseVisualStyleBackColor = true;
            // 
            // txtdescuento
            // 
            this.txtdescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtdescuento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdescuento.Enabled = false;
            this.txtdescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdescuento.Location = new System.Drawing.Point(806, 352);
            this.txtdescuento.Name = "txtdescuento";
            this.txtdescuento.Size = new System.Drawing.Size(63, 22);
            this.txtdescuento.TabIndex = 25;
            this.txtdescuento.Text = "0.00";
            this.txtdescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label11
            // 
            this.Label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(654, 338);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(66, 13);
            this.Label11.TabIndex = 24;
            this.Label11.Text = "Reintegro:";
            // 
            // txtReintegro
            // 
            this.txtReintegro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReintegro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReintegro.Enabled = false;
            this.txtReintegro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReintegro.Location = new System.Drawing.Point(657, 354);
            this.txtReintegro.Name = "txtReintegro";
            this.txtReintegro.Size = new System.Drawing.Size(64, 22);
            this.txtReintegro.TabIndex = 23;
            this.txtReintegro.Text = "0.00";
            this.txtReintegro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtporrendir
            // 
            this.txtporrendir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtporrendir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtporrendir.Enabled = false;
            this.txtporrendir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtporrendir.ForeColor = System.Drawing.Color.Blue;
            this.txtporrendir.Location = new System.Drawing.Point(495, 354);
            this.txtporrendir.Name = "txtporrendir";
            this.txtporrendir.Size = new System.Drawing.Size(64, 22);
            this.txtporrendir.TabIndex = 17;
            this.txtporrendir.Text = "0.00";
            this.txtporrendir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtxrendir
            // 
            this.txtxrendir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtxrendir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtxrendir.Enabled = false;
            this.txtxrendir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtxrendir.Location = new System.Drawing.Point(575, 354);
            this.txtxrendir.Name = "txtxrendir";
            this.txtxrendir.Size = new System.Drawing.Size(64, 22);
            this.txtxrendir.TabIndex = 21;
            this.txtxrendir.Text = "0.00";
            this.txtxrendir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label9
            // 
            this.Label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(572, 338);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(47, 13);
            this.Label9.TabIndex = 18;
            this.Label9.Text = "Vuelto:";
            // 
            // Label10
            // 
            this.Label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(492, 338);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(58, 13);
            this.Label10.TabIndex = 20;
            this.Label10.Text = "Rendido:";
            // 
            // cbxGastos
            // 
            this.cbxGastos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxGastos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxGastos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGastos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGastos.FormattingEnabled = true;
            this.cbxGastos.Location = new System.Drawing.Point(132, 31);
            this.cbxGastos.Name = "cbxGastos";
            this.cbxGastos.Size = new System.Drawing.Size(269, 21);
            this.cbxGastos.TabIndex = 31;
            this.cbxGastos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxGastos_KeyPress);
            // 
            // dgvDetalleliquidacion
            // 
            this.dgvDetalleliquidacion.AllowUserToAddRows = false;
            this.dgvDetalleliquidacion.AllowUserToDeleteRows = false;
            this.dgvDetalleliquidacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalleliquidacion.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalleliquidacion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalleliquidacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalleliquidacion.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetalleliquidacion.Location = new System.Drawing.Point(10, 74);
            this.dgvDetalleliquidacion.Name = "dgvDetalleliquidacion";
            this.dgvDetalleliquidacion.RowHeadersVisible = false;
            this.dgvDetalleliquidacion.Size = new System.Drawing.Size(912, 261);
            this.dgvDetalleliquidacion.TabIndex = 0;
            // 
            // btnQuitar
            // 
            this.btnQuitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuitar.Location = new System.Drawing.Point(865, 22);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnQuitar.Size = new System.Drawing.Size(57, 37);
            this.btnQuitar.TabIndex = 32;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuitar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Concepto de Gasto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(413, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Importe:";
            // 
            // dtpFechaGasto
            // 
            this.dtpFechaGasto.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaGasto.CalendarMonthBackground = System.Drawing.SystemColors.MenuHighlight;
            this.dtpFechaGasto.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtpFechaGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaGasto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaGasto.Location = new System.Drawing.Point(541, 32);
            this.dtpFechaGasto.Name = "dtpFechaGasto";
            this.dtpFechaGasto.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaGasto.TabIndex = 35;
            // 
            // FrmLiquidaPlanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(954, 531);
            this.Controls.Add(this.gbFecha);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbGastos);
            this.Controls.Add(this.gbPllanilaLiq);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmLiquidaPlanilla";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liquidación Planilla";
            this.Load += new System.EventHandler(this.FrmLiquidacionViaticos_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbFecha.ResumeLayout(false);
            this.gbPllanilaLiq.ResumeLayout(false);
            this.gbPllanilaLiq.PerformLayout();
            this.gbGastos.ResumeLayout(false);
            this.gbGastos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleliquidacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label lblRuta;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label lblPlaca;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblCodigoViaje;
        internal System.Windows.Forms.Label lblTituloViaje;
        private System.Windows.Forms.GroupBox gbFecha;
        internal System.Windows.Forms.DateTimePicker DTPfecha;
        private System.Windows.Forms.GroupBox gbPllanilaLiq;
        internal System.Windows.Forms.Label lblTituloPlanilla;
        internal System.Windows.Forms.Label lblPlanilla;
        internal System.Windows.Forms.Label lblTituloImporteTrujillo;
        internal System.Windows.Forms.Label lblTituloImporteLima;
        internal System.Windows.Forms.Label lblImporteLima;
        internal System.Windows.Forms.Label lblImporteTrujillo;
        private System.Windows.Forms.GroupBox gbGastos;
        internal System.Windows.Forms.Button btnAgregar;
        internal System.Windows.Forms.CheckBox chkvuelto;
        internal System.Windows.Forms.TextBox txtdescuento;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.TextBox txtReintegro;
        internal System.Windows.Forms.TextBox txtxrendir;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox txtporrendir;
        private System.Windows.Forms.ToolStripButton tsBtnGuardar;
        internal System.Windows.Forms.Button btoGrabaVuelto;
        private System.Windows.Forms.Button btnBuscarViajes;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.ComboBox cbxGastos;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnQuitar;
        internal System.Windows.Forms.DataGridView dgvDetalleliquidacion;
        internal System.Windows.Forms.DateTimePicker dtpFechaGasto;
    }
}