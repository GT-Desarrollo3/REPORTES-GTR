namespace ReportesTranspesa.Formularios.Areas.Logistica.CuadroComparativo
{
    partial class frmListaOferta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaOferta));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtgListaEvaluacion = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsModificarEvaluacion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAnadirAdicional = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEliminarEvaluacion = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaEvaluacionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pDatosAdicionales = new System.Windows.Forms.Panel();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaEvaluacion)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaEvaluacionView)).BeginInit();
            this.pDatosAdicionales.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LimeGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(817, 47);
            this.label1.TabIndex = 21;
            this.label1.Text = "LISTA DE EVALUACIONES DE OFERTAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SeaShell;
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Controls.Add(this.btnAgregar);
            this.panel5.Controls.Add(this.btnExcel);
            this.panel5.Controls.Add(this.btnBuscar);
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 47);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(817, 97);
            this.panel5.TabIndex = 212;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(429, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 58);
            this.groupBox1.TabIndex = 224;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(15, 24);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(113, 20);
            this.txtCodigo.TabIndex = 220;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(26, 25);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(116, 47);
            this.btnAgregar.TabIndex = 223;
            this.btnAgregar.Tag = "5";
            this.btnAgregar.Text = "Nueva\r\nEvaluación";
            this.btnAgregar.ToolTip = "Nueva Evaluación";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(677, 25);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 197;
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
            this.btnBuscar.Location = new System.Drawing.Point(617, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 196;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dtpFechaFin);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.dtpFechaInicio);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(168, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(243, 58);
            this.groupBox5.TabIndex = 219;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Fecha Evaluación:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(133, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(114, 28);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 13);
            this.label26.TabIndex = 4;
            this.label26.Text = "--";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(15, 24);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // dtgListaEvaluacion
            // 
            this.dtgListaEvaluacion.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaEvaluacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaEvaluacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaEvaluacion.Location = new System.Drawing.Point(0, 144);
            this.dtgListaEvaluacion.LookAndFeel.SkinMaskColor = System.Drawing.Color.Lime;
            this.dtgListaEvaluacion.LookAndFeel.SkinName = "Money Twins";
            this.dtgListaEvaluacion.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaEvaluacion.MainView = this.dgvListaEvaluacionView;
            this.dtgListaEvaluacion.Name = "dtgListaEvaluacion";
            this.dtgListaEvaluacion.Size = new System.Drawing.Size(817, 271);
            this.dtgListaEvaluacion.TabIndex = 213;
            this.dtgListaEvaluacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaEvaluacionView});
            this.dtgListaEvaluacion.DoubleClick += new System.EventHandler(this.dtgListaEvaluacion_DoubleClick);
            this.dtgListaEvaluacion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaEvaluacion_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsModificarEvaluacion,
            this.tsAnadirAdicional,
            this.tsEliminarEvaluacion});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 70);
            // 
            // tsModificarEvaluacion
            // 
            this.tsModificarEvaluacion.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.tsModificarEvaluacion.Name = "tsModificarEvaluacion";
            this.tsModificarEvaluacion.Size = new System.Drawing.Size(185, 22);
            this.tsModificarEvaluacion.Text = "Modificar Evaluación";
            this.tsModificarEvaluacion.Click += new System.EventHandler(this.tsModificarEvaluacion_Click);
            // 
            // tsAnadirAdicional
            // 
            this.tsAnadirAdicional.Image = global::ReportesTranspesa.Properties.Resources.settingscol;
            this.tsAnadirAdicional.Name = "tsAnadirAdicional";
            this.tsAnadirAdicional.Size = new System.Drawing.Size(185, 22);
            this.tsAnadirAdicional.Text = "Añadir Adicional";
            this.tsAnadirAdicional.Click += new System.EventHandler(this.tsAnadirAdicional_Click);
            // 
            // tsEliminarEvaluacion
            // 
            this.tsEliminarEvaluacion.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarEvaluacion.Name = "tsEliminarEvaluacion";
            this.tsEliminarEvaluacion.Size = new System.Drawing.Size(185, 22);
            this.tsEliminarEvaluacion.Text = "Eliminar Evaluación";
            this.tsEliminarEvaluacion.Click += new System.EventHandler(this.tsEliminarEvaluacion_Click);
            // 
            // dgvListaEvaluacionView
            // 
            gridFormatRule1.Name = "Format0";
            formatConditionIconSet1.CategoryName = "Ratings";
            formatConditionIconSetIcon1.PredefinedName = "TrafficLights3_3.png";
            formatConditionIconSetIcon1.Value = new decimal(new int[] {
            67,
            0,
            0,
            0});
            formatConditionIconSetIcon1.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon2.PredefinedName = "TrafficLights3_1.png";
            formatConditionIconSetIcon2.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
            formatConditionIconSetIcon2.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon3.PredefinedName = "Stars3_3.png";
            formatConditionIconSetIcon3.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon1);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon2);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon3);
            formatConditionIconSet1.Name = "Stars3";
            formatConditionIconSet1.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet1.IconSet = formatConditionIconSet1;
            gridFormatRule1.Rule = formatConditionRuleIconSet1;
            this.dgvListaEvaluacionView.FormatRules.Add(gridFormatRule1);
            this.dgvListaEvaluacionView.GridControl = this.dtgListaEvaluacion;
            this.dgvListaEvaluacionView.Name = "dgvListaEvaluacionView";
            this.dgvListaEvaluacionView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvListaEvaluacionView.OptionsBehavior.Editable = false;
            this.dgvListaEvaluacionView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvListaEvaluacionView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaEvaluacionView.OptionsView.RowAutoHeight = true;
            // 
            // pDatosAdicionales
            // 
            this.pDatosAdicionales.BackColor = System.Drawing.Color.SeaShell;
            this.pDatosAdicionales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDatosAdicionales.Controls.Add(this.txtDescripcion);
            this.pDatosAdicionales.Controls.Add(this.label3);
            this.pDatosAdicionales.Controls.Add(this.txtDescuento);
            this.pDatosAdicionales.Controls.Add(this.label15);
            this.pDatosAdicionales.Controls.Add(this.btnGuardar);
            this.pDatosAdicionales.Controls.Add(this.lblCodigo);
            this.pDatosAdicionales.Controls.Add(this.label4);
            this.pDatosAdicionales.Controls.Add(this.btnCerrar2);
            this.pDatosAdicionales.Controls.Add(this.label2);
            this.pDatosAdicionales.Location = new System.Drawing.Point(273, 155);
            this.pDatosAdicionales.Name = "pDatosAdicionales";
            this.pDatosAdicionales.Size = new System.Drawing.Size(270, 258);
            this.pDatosAdicionales.TabIndex = 214;
            this.pDatosAdicionales.Visible = false;
            this.pDatosAdicionales.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDatosAdicionales_MouseMove);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtDescripcion.Location = new System.Drawing.Point(13, 127);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(241, 59);
            this.txtDescripcion.TabIndex = 167;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label3.Location = new System.Drawing.Point(10, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 166;
            this.label3.Text = "Conclusiones Finales:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescuento
            // 
            this.txtDescuento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescuento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtDescuento.Location = new System.Drawing.Point(151, 72);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(103, 20);
            this.txtDescuento.TabIndex = 161;
            this.txtDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescuento_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label15.Location = new System.Drawing.Point(10, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(135, 15);
            this.label15.TabIndex = 165;
            this.label15.Text = "Ingrese Descuento (%):";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(79, 201);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(111, 42);
            this.btnGuardar.TabIndex = 164;
            this.btnGuardar.Text = " Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblCodigo.Location = new System.Drawing.Point(77, 44);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(30, 16);
            this.lblCodigo.TabIndex = 111;
            this.lblCodigo.Text = "CT-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(10, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 110;
            this.label4.Text = "CÓDIGO:";
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar2.BackColor = System.Drawing.Color.Red;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.ForeColor = System.Drawing.Color.White;
            this.btnCerrar2.Location = new System.Drawing.Point(246, -1);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(23, 23);
            this.btnCerrar2.TabIndex = 109;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 21);
            this.label2.TabIndex = 105;
            this.label2.Text = "DATOS ADICIONALES";
            // 
            // frmListaOferta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 415);
            this.Controls.Add(this.dtgListaEvaluacion);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pDatosAdicionales);
            this.Name = "frmListaOferta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LISTA DE OFERTAS DE SERVICIOS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaOferta_Load);
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaEvaluacion)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaEvaluacionView)).EndInit();
            this.pDatosAdicionales.ResumeLayout(false);
            this.pDatosAdicionales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtCodigo;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private DevExpress.XtraGrid.GridControl dtgListaEvaluacion;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaEvaluacionView;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsModificarEvaluacion;
        public System.Windows.Forms.ToolStripMenuItem tsEliminarEvaluacion;
        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ToolStripMenuItem tsAnadirAdicional;
        private System.Windows.Forms.Panel pDatosAdicionales;
        internal System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCerrar2;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtDescripcion;
    }
}