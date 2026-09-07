namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    partial class frmGenerarOrdenTrabajo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerarOrdenTrabajo));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxUbicacion = new System.Windows.Forms.ComboBox();
            this.label76 = new System.Windows.Forms.Label();
            this.cbxTipoMtto = new System.Windows.Forms.ComboBox();
            this.cbxClasificacion = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTarea = new System.Windows.Forms.TextBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtgvListaSolicitudes = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvListaSolicitudesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lstTarea = new System.Windows.Forms.ListView();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.lblTipoUnidad = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProgramacion = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRegistrar = new DevExpress.XtraEditors.SimpleButton();
            this.lstEmpleado = new System.Windows.Forms.ListView();
            this.groupBox19.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaSolicitudes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaSolicitudesView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkRed;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(641, 45);
            this.label1.TabIndex = 17;
            this.label1.Text = "CREACIÓN DE ORDEN DE TRABAJO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.txtEmpleado);
            this.groupBox19.Controls.Add(this.label10);
            this.groupBox19.Controls.Add(this.txtDescripcion);
            this.groupBox19.Controls.Add(this.label2);
            this.groupBox19.Controls.Add(this.groupBox1);
            this.groupBox19.Controls.Add(this.label76);
            this.groupBox19.Controls.Add(this.cbxTipoMtto);
            this.groupBox19.Controls.Add(this.cbxClasificacion);
            this.groupBox19.Controls.Add(this.label77);
            this.groupBox19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox19.Location = new System.Drawing.Point(17, 119);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(607, 175);
            this.groupBox19.TabIndex = 116;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "DATOS DE ORDEN DE TRABAJO: ";
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpleado.Location = new System.Drawing.Point(103, 138);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(485, 21);
            this.txtEmpleado.TabIndex = 213;
            this.txtEmpleado.Enter += new System.EventHandler(this.txtEmpleado_Enter);
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            this.txtEmpleado.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmpleado_KeyUp);
            this.txtEmpleado.Leave += new System.EventHandler(this.txtEmpleado_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 15);
            this.label10.TabIndex = 212;
            this.label10.Text = "Persona Asig.:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(103, 103);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(485, 21);
            this.txtDescripcion.TabIndex = 211;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 119;
            this.label2.Text = "Descripción:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxUbicacion);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(388, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 70);
            this.groupBox1.TabIndex = 118;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ubicación: ";
            // 
            // cbxUbicacion
            // 
            this.cbxUbicacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUbicacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUbicacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUbicacion.FormattingEnabled = true;
            this.cbxUbicacion.Items.AddRange(new object[] {
            "TRUJILLO",
            "MOCHE"});
            this.cbxUbicacion.Location = new System.Drawing.Point(14, 29);
            this.cbxUbicacion.Name = "cbxUbicacion";
            this.cbxUbicacion.Size = new System.Drawing.Size(171, 23);
            this.cbxUbicacion.TabIndex = 119;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(33, 31);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(64, 15);
            this.label76.TabIndex = 117;
            this.label76.Text = "Tipo Mtto.:";
            // 
            // cbxTipoMtto
            // 
            this.cbxTipoMtto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoMtto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoMtto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoMtto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoMtto.FormattingEnabled = true;
            this.cbxTipoMtto.Location = new System.Drawing.Point(103, 27);
            this.cbxTipoMtto.Name = "cbxTipoMtto";
            this.cbxTipoMtto.Size = new System.Drawing.Size(263, 23);
            this.cbxTipoMtto.TabIndex = 116;
            this.cbxTipoMtto.SelectedIndexChanged += new System.EventHandler(this.cbxTipoMtto_SelectedIndexChanged);
            // 
            // cbxClasificacion
            // 
            this.cbxClasificacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxClasificacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxClasificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClasificacion.FormattingEnabled = true;
            this.cbxClasificacion.Location = new System.Drawing.Point(103, 65);
            this.cbxClasificacion.Name = "cbxClasificacion";
            this.cbxClasificacion.Size = new System.Drawing.Size(263, 23);
            this.cbxClasificacion.TabIndex = 115;
            this.cbxClasificacion.SelectedIndexChanged += new System.EventHandler(this.cbxClasificacion_SelectedIndexChanged);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(18, 69);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(79, 15);
            this.label77.TabIndex = 114;
            this.label77.Text = "Clasificación:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAgregar);
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTarea);
            this.groupBox2.Controls.Add(this.dtpFechaInicio);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dtgvListaSolicitudes);
            this.groupBox2.Controls.Add(this.lstTarea);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(17, 304);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(607, 244);
            this.groupBox2.TabIndex = 117;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS DE ACTIVIDAD: ";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(502, 62);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(86, 26);
            this.btnAgregar.TabIndex = 222;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy hh:mm";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(333, 64);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(137, 21);
            this.dtpFechaFin.TabIndex = 220;
            this.dtpFechaFin.Tag = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(263, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 219;
            this.label7.Text = "Fecha Fin:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(56, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 218;
            this.label6.Text = "Tarea:";
            // 
            // txtTarea
            // 
            this.txtTarea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTarea.Location = new System.Drawing.Point(104, 27);
            this.txtTarea.Name = "txtTarea";
            this.txtTarea.Size = new System.Drawing.Size(484, 21);
            this.txtTarea.TabIndex = 217;
            this.txtTarea.Enter += new System.EventHandler(this.txtTarea_Enter);
            this.txtTarea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarea_KeyPress);
            this.txtTarea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTarea_KeyUp);
            this.txtTarea.Leave += new System.EventHandler(this.txtTarea_Leave);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy hh:mm";
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(104, 64);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(137, 21);
            this.dtpFechaInicio.TabIndex = 216;
            this.dtpFechaInicio.Tag = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 15);
            this.label3.TabIndex = 212;
            this.label3.Text = "Seleccione una solicitud para asignarle una tarea:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 117;
            this.label4.Text = "Fecha Inicio:";
            // 
            // dtgvListaSolicitudes
            // 
            this.dtgvListaSolicitudes.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgvListaSolicitudes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvListaSolicitudes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgvListaSolicitudes.Location = new System.Drawing.Point(3, 119);
            this.dtgvListaSolicitudes.LookAndFeel.SkinName = "Blue";
            this.dtgvListaSolicitudes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvListaSolicitudes.MainView = this.dtgvListaSolicitudesView;
            this.dtgvListaSolicitudes.Name = "dtgvListaSolicitudes";
            this.dtgvListaSolicitudes.Size = new System.Drawing.Size(601, 122);
            this.dtgvListaSolicitudes.TabIndex = 214;
            this.dtgvListaSolicitudes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvListaSolicitudesView});
            this.dtgvListaSolicitudes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgvListaSolicitudes_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 26);
            // 
            // tsEliminar
            // 
            this.tsEliminar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminar.Name = "tsEliminar";
            this.tsEliminar.Size = new System.Drawing.Size(137, 22);
            this.tsEliminar.Text = "Quitar Tarea";
            this.tsEliminar.Click += new System.EventHandler(this.tsEliminar_Click);
            // 
            // dtgvListaSolicitudesView
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
            this.dtgvListaSolicitudesView.FormatRules.Add(gridFormatRule1);
            this.dtgvListaSolicitudesView.GridControl = this.dtgvListaSolicitudes;
            this.dtgvListaSolicitudesView.Name = "dtgvListaSolicitudesView";
            this.dtgvListaSolicitudesView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvListaSolicitudesView.OptionsBehavior.Editable = false;
            this.dtgvListaSolicitudesView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvListaSolicitudesView.OptionsView.ColumnAutoWidth = false;
            this.dtgvListaSolicitudesView.OptionsView.RowAutoHeight = true;
            this.dtgvListaSolicitudesView.OptionsView.ShowGroupPanel = false;
            // 
            // lstTarea
            // 
            this.lstTarea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTarea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTarea.ForeColor = System.Drawing.Color.Navy;
            this.lstTarea.FullRowSelect = true;
            this.lstTarea.GridLines = true;
            this.lstTarea.Location = new System.Drawing.Point(104, 47);
            this.lstTarea.MultiSelect = false;
            this.lstTarea.Name = "lstTarea";
            this.lstTarea.Size = new System.Drawing.Size(484, 162);
            this.lstTarea.TabIndex = 221;
            this.lstTarea.UseCompatibleStateImageBehavior = false;
            this.lstTarea.View = System.Windows.Forms.View.Details;
            this.lstTarea.Visible = false;
            this.lstTarea.Enter += new System.EventHandler(this.lstTarea_Enter);
            this.lstTarea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTarea_KeyPress);
            this.lstTarea.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTarea_MouseDoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(19, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 22);
            this.label9.TabIndex = 213;
            this.label9.Text = "PLACA:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblPlaca.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPlaca.Location = new System.Drawing.Point(82, 59);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(22, 21);
            this.lblPlaca.TabIndex = 212;
            this.lblPlaca.Text = "P";
            this.lblPlaca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTipoUnidad
            // 
            this.lblTipoUnidad.AutoSize = true;
            this.lblTipoUnidad.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTipoUnidad.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTipoUnidad.Location = new System.Drawing.Point(129, 87);
            this.lblTipoUnidad.Name = "lblTipoUnidad";
            this.lblTipoUnidad.Size = new System.Drawing.Size(23, 21);
            this.lblTipoUnidad.TabIndex = 214;
            this.lblTipoUnidad.Text = "C";
            this.lblTipoUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(19, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 22);
            this.label5.TabIndex = 215;
            this.label5.Text = "TIPO UNIDAD:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProgramacion
            // 
            this.lblProgramacion.AutoSize = true;
            this.lblProgramacion.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblProgramacion.ForeColor = System.Drawing.Color.DarkRed;
            this.lblProgramacion.Location = new System.Drawing.Point(412, 59);
            this.lblProgramacion.Name = "lblProgramacion";
            this.lblProgramacion.Size = new System.Drawing.Size(21, 21);
            this.lblProgramacion.TabIndex = 216;
            this.lblProgramacion.Text = "L";
            this.lblProgramacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(272, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 22);
            this.label8.TabIndex = 217;
            this.label8.Text = "PROGRAMACIÓN:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnRegistrar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnRegistrar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnRegistrar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Appearance.Options.UseBackColor = true;
            this.btnRegistrar.Appearance.Options.UseBorderColor = true;
            this.btnRegistrar.Appearance.Options.UseFont = true;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.Image")));
            this.btnRegistrar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnRegistrar.Location = new System.Drawing.Point(272, 563);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(111, 39);
            this.btnRegistrar.TabIndex = 218;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.ToolTip = "Registrar";
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lstEmpleado
            // 
            this.lstEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpleado.FullRowSelect = true;
            this.lstEmpleado.GridLines = true;
            this.lstEmpleado.Location = new System.Drawing.Point(120, 277);
            this.lstEmpleado.MultiSelect = false;
            this.lstEmpleado.Name = "lstEmpleado";
            this.lstEmpleado.Size = new System.Drawing.Size(485, 142);
            this.lstEmpleado.TabIndex = 222;
            this.lstEmpleado.UseCompatibleStateImageBehavior = false;
            this.lstEmpleado.View = System.Windows.Forms.View.Details;
            this.lstEmpleado.Visible = false;
            this.lstEmpleado.Enter += new System.EventHandler(this.lstEmpleado_Enter);
            this.lstEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpleado_KeyPress);
            this.lstEmpleado.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEmpleado_MouseDoubleClick);
            // 
            // frmGenerarOrdenTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(641, 617);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblProgramacion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblTipoUnidad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPlaca);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox19);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstEmpleado);
            this.MaximizeBox = false;
            this.Name = "frmGenerarOrdenTrabajo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GENERAR ORDEN TRABAJO";
            this.Load += new System.EventHandler(this.frmGenerarOrdenTrabajo_Load);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaSolicitudes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaSolicitudesView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label label76;
        public System.Windows.Forms.ComboBox cbxTipoMtto;
        public System.Windows.Forms.ComboBox cbxClasificacion;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cbxUbicacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label lblPlaca;
        public System.Windows.Forms.Label lblTipoUnidad;
        public System.Windows.Forms.Label lblProgramacion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl dtgvListaSolicitudes;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvListaSolicitudesView;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.TextBox txtTarea;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lstTarea;
        private System.Windows.Forms.Button btnAgregar;
        private DevExpress.XtraEditors.SimpleButton btnRegistrar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminar;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lstEmpleado;
        public System.Windows.Forms.TextBox txtDescripcion;
    }
}