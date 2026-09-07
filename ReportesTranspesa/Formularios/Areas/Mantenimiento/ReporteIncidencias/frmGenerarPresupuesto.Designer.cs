namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias
{
    partial class frmGenerarPresupuesto
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerarPresupuesto));
            this.label1 = new System.Windows.Forms.Label();
            this.dtgListaPresupuesto = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarPresupuestoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaPresupuestoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRutaLocal = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSupervisor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTipoUnidad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAniadir = new System.Windows.Forms.Button();
            this.txtPrecioTotal = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.rbInsumos = new System.Windows.Forms.RadioButton();
            this.rbManoObra = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxUND = new System.Windows.Forms.ComboBox();
            this.lstItemsAlmacen = new System.Windows.Forms.ListView();
            this.txtMontoTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIGV = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCarreta2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.dtpFechaIncidente = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.txtIncidente = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtConductor2 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtOperacion2 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTracto2 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btnCerrarLocal = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtRutaLocal2 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPresupuesto)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPresupuestoView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1011, 48);
            this.label1.TabIndex = 19;
            this.label1.Text = "PRESUPUESTO DE INCIDENCIA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgListaPresupuesto
            // 
            this.dtgListaPresupuesto.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaPresupuesto.Location = new System.Drawing.Point(403, 302);
            this.dtgListaPresupuesto.LookAndFeel.SkinName = "Blue";
            this.dtgListaPresupuesto.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaPresupuesto.MainView = this.dgvListaPresupuestoView;
            this.dtgListaPresupuesto.Name = "dtgListaPresupuesto";
            this.dtgListaPresupuesto.Size = new System.Drawing.Size(578, 212);
            this.dtgListaPresupuesto.TabIndex = 102;
            this.dtgListaPresupuesto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaPresupuestoView});
            this.dtgListaPresupuesto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaPresupuesto_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarPresupuestoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 26);
            // 
            // eliminarPresupuestoToolStripMenuItem
            // 
            this.eliminarPresupuestoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarPresupuestoToolStripMenuItem.Name = "eliminarPresupuestoToolStripMenuItem";
            this.eliminarPresupuestoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.eliminarPresupuestoToolStripMenuItem.Text = "Eliminar Item";
            this.eliminarPresupuestoToolStripMenuItem.Click += new System.EventHandler(this.eliminarPresupuestoToolStripMenuItem_Click);
            // 
            // dgvListaPresupuestoView
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
            this.dgvListaPresupuestoView.FormatRules.Add(gridFormatRule1);
            this.dgvListaPresupuestoView.GridControl = this.dtgListaPresupuesto;
            this.dgvListaPresupuestoView.Name = "dgvListaPresupuestoView";
            this.dgvListaPresupuestoView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvListaPresupuestoView.OptionsBehavior.Editable = false;
            this.dgvListaPresupuestoView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvListaPresupuestoView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaPresupuestoView.OptionsView.RowAutoHeight = true;
            this.dgvListaPresupuestoView.OptionsView.ShowGroupPanel = false;
            // 
            // txtPlaca
            // 
            this.txtPlaca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtPlaca.Location = new System.Drawing.Point(89, 27);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.ReadOnly = true;
            this.txtPlaca.Size = new System.Drawing.Size(102, 20);
            this.txtPlaca.TabIndex = 113;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label10.Location = new System.Drawing.Point(42, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 15);
            this.label10.TabIndex = 112;
            this.label10.Text = "Placa:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRutaLocal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSupervisor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtConductor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOperacion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTipoUnidad);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(28, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(953, 150);
            this.groupBox1.TabIndex = 114;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DE INCIDENCIA";
            this.groupBox1.Visible = false;
            // 
            // txtRutaLocal
            // 
            this.txtRutaLocal.BackColor = System.Drawing.SystemColors.Control;
            this.txtRutaLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtRutaLocal.Location = new System.Drawing.Point(563, 68);
            this.txtRutaLocal.Name = "txtRutaLocal";
            this.txtRutaLocal.ReadOnly = true;
            this.txtRutaLocal.Size = new System.Drawing.Size(364, 61);
            this.txtRutaLocal.TabIndex = 125;
            this.txtRutaLocal.Text = "";
            this.txtRutaLocal.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtRutaLocal_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label7.Location = new System.Drawing.Point(490, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 30);
            this.label7.TabIndex = 124;
            this.label7.Text = "Informe de\r\nSeguridad:\r\n";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(833, 27);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(94, 20);
            this.dtpFecha.TabIndex = 123;
            this.dtpFecha.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label6.Location = new System.Drawing.Point(783, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 15);
            this.label6.TabIndex = 122;
            this.label6.Text = "Fecha:";
            // 
            // txtSupervisor
            // 
            this.txtSupervisor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupervisor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSupervisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtSupervisor.Location = new System.Drawing.Point(89, 109);
            this.txtSupervisor.Name = "txtSupervisor";
            this.txtSupervisor.ReadOnly = true;
            this.txtSupervisor.Size = new System.Drawing.Size(365, 20);
            this.txtSupervisor.TabIndex = 121;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label5.Location = new System.Drawing.Point(15, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 120;
            this.label5.Text = "Supervisor:";
            // 
            // txtConductor
            // 
            this.txtConductor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConductor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtConductor.Location = new System.Drawing.Point(89, 68);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.ReadOnly = true;
            this.txtConductor.Size = new System.Drawing.Size(365, 20);
            this.txtConductor.TabIndex = 119;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label4.Location = new System.Drawing.Point(17, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 118;
            this.label4.Text = "Conductor:";
            // 
            // txtOperacion
            // 
            this.txtOperacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtOperacion.Location = new System.Drawing.Point(563, 27);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(187, 20);
            this.txtOperacion.TabIndex = 117;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label3.Location = new System.Drawing.Point(490, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 116;
            this.label3.Text = "Operación:";
            // 
            // txtTipoUnidad
            // 
            this.txtTipoUnidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTipoUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTipoUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtTipoUnidad.Location = new System.Drawing.Point(267, 27);
            this.txtTipoUnidad.Name = "txtTipoUnidad";
            this.txtTipoUnidad.ReadOnly = true;
            this.txtTipoUnidad.Size = new System.Drawing.Size(187, 20);
            this.txtTipoUnidad.TabIndex = 115;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label2.Location = new System.Drawing.Point(227, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 114;
            this.label2.Text = "Tipo:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancelar);
            this.groupBox2.Controls.Add(this.btnAniadir);
            this.groupBox2.Controls.Add(this.txtPrecioTotal);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtCantidad);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtPrecioUnitario);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.rbInsumos);
            this.groupBox2.Controls.Add(this.rbManoObra);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cbxUND);
            this.groupBox2.Controls.Add(this.lstItemsAlmacen);
            this.groupBox2.Location = new System.Drawing.Point(28, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 341);
            this.groupBox2.TabIndex = 115;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS DE MATERIALES";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(66, 282);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 40);
            this.btnCancelar.TabIndex = 134;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAniadir
            // 
            this.btnAniadir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAniadir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAniadir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAniadir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.btnAniadir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAniadir.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAniadir.Location = new System.Drawing.Point(196, 282);
            this.btnAniadir.Name = "btnAniadir";
            this.btnAniadir.Size = new System.Drawing.Size(95, 40);
            this.btnAniadir.TabIndex = 133;
            this.btnAniadir.Text = " Añadir";
            this.btnAniadir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAniadir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAniadir.UseVisualStyleBackColor = false;
            this.btnAniadir.Click += new System.EventHandler(this.btnAniadir_Click);
            // 
            // txtPrecioTotal
            // 
            this.txtPrecioTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecioTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecioTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioTotal.Location = new System.Drawing.Point(270, 231);
            this.txtPrecioTotal.Name = "txtPrecioTotal";
            this.txtPrecioTotal.ReadOnly = true;
            this.txtPrecioTotal.Size = new System.Drawing.Size(70, 20);
            this.txtPrecioTotal.TabIndex = 132;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(162, 227);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(102, 26);
            this.label17.TabIndex = 131;
            this.label17.Text = "Precio Total (S/)\r\n(SIN IGV):";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCantidad
            // 
            this.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCantidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtCantidad.Location = new System.Drawing.Point(270, 194);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(70, 20);
            this.txtCantidad.TabIndex = 130;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label16.Location = new System.Drawing.Point(205, 196);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 15);
            this.label16.TabIndex = 129;
            this.label16.Text = "Cantidad:";
            // 
            // txtPrecioUnitario
            // 
            this.txtPrecioUnitario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecioUnitario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecioUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtPrecioUnitario.Location = new System.Drawing.Point(270, 157);
            this.txtPrecioUnitario.Name = "txtPrecioUnitario";
            this.txtPrecioUnitario.Size = new System.Drawing.Size(70, 20);
            this.txtPrecioUnitario.TabIndex = 128;
            this.txtPrecioUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioUnitario_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label14.Location = new System.Drawing.Point(178, 152);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 30);
            this.label14.TabIndex = 127;
            this.label14.Text = "P. Unitario (S/)\r\n(SIN IGV):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label15.Location = new System.Drawing.Point(15, 152);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 30);
            this.label15.TabIndex = 125;
            this.label15.Text = "Unidad de\r\nMedida:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtDescripcion.Location = new System.Drawing.Point(19, 88);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(321, 48);
            this.txtDescripcion.TabIndex = 124;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            this.txtDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyUp);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label13.Location = new System.Drawing.Point(16, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 15);
            this.label13.TabIndex = 123;
            this.label13.Text = "Descripción:";
            // 
            // rbInsumos
            // 
            this.rbInsumos.AutoSize = true;
            this.rbInsumos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.rbInsumos.Location = new System.Drawing.Point(124, 27);
            this.rbInsumos.Name = "rbInsumos";
            this.rbInsumos.Size = new System.Drawing.Size(72, 19);
            this.rbInsumos.TabIndex = 122;
            this.rbInsumos.Text = "Insumos";
            this.rbInsumos.UseVisualStyleBackColor = true;
            this.rbInsumos.Click += new System.EventHandler(this.rbInsumos_Click);
            // 
            // rbManoObra
            // 
            this.rbManoObra.AutoSize = true;
            this.rbManoObra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.rbManoObra.Location = new System.Drawing.Point(220, 27);
            this.rbManoObra.Name = "rbManoObra";
            this.rbManoObra.Size = new System.Drawing.Size(104, 19);
            this.rbManoObra.TabIndex = 121;
            this.rbManoObra.Text = "Mano de Obra";
            this.rbManoObra.UseVisualStyleBackColor = true;
            this.rbManoObra.Click += new System.EventHandler(this.rbManoObra_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label12.Location = new System.Drawing.Point(16, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 15);
            this.label12.TabIndex = 120;
            this.label12.Text = "Tipo de Material:";
            // 
            // cbxUND
            // 
            this.cbxUND.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUND.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUND.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUND.FormattingEnabled = true;
            this.cbxUND.Items.AddRange(new object[] {
            "MINERAL",
            "SINTETICO"});
            this.cbxUND.Location = new System.Drawing.Point(85, 156);
            this.cbxUND.Name = "cbxUND";
            this.cbxUND.Size = new System.Drawing.Size(70, 21);
            this.cbxUND.TabIndex = 149;
            this.cbxUND.SelectedIndexChanged += new System.EventHandler(this.cbxUND_SelectedIndexChanged);
            // 
            // lstItemsAlmacen
            // 
            this.lstItemsAlmacen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstItemsAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItemsAlmacen.ForeColor = System.Drawing.Color.Navy;
            this.lstItemsAlmacen.FullRowSelect = true;
            this.lstItemsAlmacen.GridLines = true;
            this.lstItemsAlmacen.Location = new System.Drawing.Point(19, 135);
            this.lstItemsAlmacen.MultiSelect = false;
            this.lstItemsAlmacen.Name = "lstItemsAlmacen";
            this.lstItemsAlmacen.Size = new System.Drawing.Size(321, 129);
            this.lstItemsAlmacen.TabIndex = 148;
            this.lstItemsAlmacen.UseCompatibleStateImageBehavior = false;
            this.lstItemsAlmacen.View = System.Windows.Forms.View.Details;
            this.lstItemsAlmacen.Visible = false;
            this.lstItemsAlmacen.Enter += new System.EventHandler(this.lstItemsAlmacen_Enter);
            this.lstItemsAlmacen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstItemsAlmacen_KeyPress);
            this.lstItemsAlmacen.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItemsAlmacen_MouseDoubleClick);
            // 
            // txtMontoTotal
            // 
            this.txtMontoTotal.BackColor = System.Drawing.SystemColors.Control;
            this.txtMontoTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMontoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtMontoTotal.Location = new System.Drawing.Point(756, 539);
            this.txtMontoTotal.Name = "txtMontoTotal";
            this.txtMontoTotal.ReadOnly = true;
            this.txtMontoTotal.Size = new System.Drawing.Size(62, 20);
            this.txtMontoTotal.TabIndex = 121;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(697, 541);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 15);
            this.label9.TabIndex = 120;
            this.label9.Text = "TOTAL:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtSubtotal.Location = new System.Drawing.Point(491, 539);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(62, 20);
            this.txtSubtotal.TabIndex = 119;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(400, 541);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 15);
            this.label8.TabIndex = 118;
            this.label8.Text = "SUB TOTAL:";
            // 
            // txtIGV
            // 
            this.txtIGV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIGV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtIGV.Location = new System.Drawing.Point(614, 539);
            this.txtIGV.Name = "txtIGV";
            this.txtIGV.ReadOnly = true;
            this.txtIGV.Size = new System.Drawing.Size(62, 20);
            this.txtIGV.TabIndex = 117;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(575, 541);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 15);
            this.label11.TabIndex = 116;
            this.label11.Text = "IGV:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(861, 530);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 42);
            this.btnGuardar.TabIndex = 122;
            this.btnGuardar.Text = " Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCarreta2);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.dtpFechaIncidente);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.txtIncidente);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.txtConductor2);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.txtOperacion2);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtTracto2);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Location = new System.Drawing.Point(28, 64);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(953, 150);
            this.groupBox3.TabIndex = 123;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DATOS DE INCIDENCIA";
            // 
            // txtCarreta2
            // 
            this.txtCarreta2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarreta2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarreta2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtCarreta2.Location = new System.Drawing.Point(789, 27);
            this.txtCarreta2.Name = "txtCarreta2";
            this.txtCarreta2.ReadOnly = true;
            this.txtCarreta2.Size = new System.Drawing.Size(102, 20);
            this.txtCarreta2.TabIndex = 125;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label18.Location = new System.Drawing.Point(687, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(96, 15);
            this.label18.TabIndex = 124;
            this.label18.Text = "Semirremolque:";
            // 
            // dtpFechaIncidente
            // 
            this.dtpFechaIncidente.Enabled = false;
            this.dtpFechaIncidente.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIncidente.Location = new System.Drawing.Point(360, 27);
            this.dtpFechaIncidente.Name = "dtpFechaIncidente";
            this.dtpFechaIncidente.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaIncidente.TabIndex = 123;
            this.dtpFechaIncidente.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label19.Location = new System.Drawing.Point(240, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 15);
            this.label19.TabIndex = 122;
            this.label19.Text = "Fecha de Incidente:";
            // 
            // txtIncidente
            // 
            this.txtIncidente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIncidente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIncidente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtIncidente.Location = new System.Drawing.Point(546, 68);
            this.txtIncidente.Multiline = true;
            this.txtIncidente.Name = "txtIncidente";
            this.txtIncidente.ReadOnly = true;
            this.txtIncidente.Size = new System.Drawing.Size(345, 61);
            this.txtIncidente.TabIndex = 121;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label20.Location = new System.Drawing.Point(480, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 15);
            this.label20.TabIndex = 120;
            this.label20.Text = "Incidente:";
            // 
            // txtConductor2
            // 
            this.txtConductor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConductor2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtConductor2.Location = new System.Drawing.Point(89, 68);
            this.txtConductor2.Name = "txtConductor2";
            this.txtConductor2.ReadOnly = true;
            this.txtConductor2.Size = new System.Drawing.Size(365, 20);
            this.txtConductor2.TabIndex = 119;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label21.Location = new System.Drawing.Point(17, 70);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 15);
            this.label21.TabIndex = 118;
            this.label21.Text = "Conductor:";
            // 
            // txtOperacion2
            // 
            this.txtOperacion2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperacion2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOperacion2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtOperacion2.Location = new System.Drawing.Point(89, 27);
            this.txtOperacion2.Name = "txtOperacion2";
            this.txtOperacion2.ReadOnly = true;
            this.txtOperacion2.Size = new System.Drawing.Size(122, 20);
            this.txtOperacion2.TabIndex = 117;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label22.Location = new System.Drawing.Point(16, 29);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 15);
            this.label22.TabIndex = 116;
            this.label22.Text = "Operación:";
            // 
            // txtTracto2
            // 
            this.txtTracto2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTracto2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTracto2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtTracto2.Location = new System.Drawing.Point(546, 27);
            this.txtTracto2.Name = "txtTracto2";
            this.txtTracto2.ReadOnly = true;
            this.txtTracto2.Size = new System.Drawing.Size(102, 20);
            this.txtTracto2.TabIndex = 113;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label24.Location = new System.Drawing.Point(496, 29);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(44, 15);
            this.label24.TabIndex = 112;
            this.label24.Text = "Tracto:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label23.Location = new System.Drawing.Point(400, 228);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(155, 15);
            this.label23.TabIndex = 126;
            this.label23.Text = "ADJUNTAR DOCUMENTO:";
            // 
            // btnCerrarLocal
            // 
            this.btnCerrarLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarLocal.Location = new System.Drawing.Point(955, 257);
            this.btnCerrarLocal.Name = "btnCerrarLocal";
            this.btnCerrarLocal.Size = new System.Drawing.Size(26, 21);
            this.btnCerrarLocal.TabIndex = 125;
            this.btnCerrarLocal.Text = "X";
            this.btnCerrarLocal.UseVisualStyleBackColor = true;
            this.btnCerrarLocal.Click += new System.EventHandler(this.btnCerrarLocal_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(925, 257);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(26, 21);
            this.btnBuscar.TabIndex = 124;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtRutaLocal2
            // 
            this.txtRutaLocal2.BackColor = System.Drawing.SystemColors.Control;
            this.txtRutaLocal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaLocal2.Location = new System.Drawing.Point(403, 253);
            this.txtRutaLocal2.Name = "txtRutaLocal2";
            this.txtRutaLocal2.ReadOnly = true;
            this.txtRutaLocal2.Size = new System.Drawing.Size(507, 30);
            this.txtRutaLocal2.TabIndex = 127;
            this.txtRutaLocal2.Text = "";
            this.txtRutaLocal2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtRutaLocal2_MouseDoubleClick);
            // 
            // frmGenerarPresupuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1011, 592);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.btnCerrarLocal);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtRutaLocal2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.txtMontoTotal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtIGV);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtgListaPresupuesto);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmGenerarPresupuesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GENERAR PRESUPUESTO";
            this.Load += new System.EventHandler(this.frmGenerarPresupuesto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaPresupuesto)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaPresupuestoView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgListaPresupuesto;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaPresupuestoView;
        internal System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtTipoUnidad;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtSupervisor;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.RichTextBox txtRutaLocal;
        internal System.Windows.Forms.TextBox txtMontoTotal;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtIGV;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbInsumos;
        private System.Windows.Forms.RadioButton rbManoObra;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.TextBox txtPrecioTotal;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.TextBox txtPrecioUnitario;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.Button btnAniadir;
        private System.Windows.Forms.ListView lstItemsAlmacen;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem eliminarPresupuestoToolStripMenuItem;
        public System.Windows.Forms.ComboBox cbxUND;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox txtCarreta2;
        public System.Windows.Forms.DateTimePicker dtpFechaIncidente;
        public System.Windows.Forms.TextBox txtIncidente;
        public System.Windows.Forms.TextBox txtConductor2;
        public System.Windows.Forms.TextBox txtOperacion2;
        public System.Windows.Forms.TextBox txtTracto2;
        public System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.Button btnCerrarLocal;
        public System.Windows.Forms.Button btnBuscar;
        public System.Windows.Forms.RichTextBox txtRutaLocal2;
    }
}