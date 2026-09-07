namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmVacacionesPendientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVacacionesPendientes));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.dtgVacacionesPend = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarVacaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvVacacionesPendVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pProgramarVacaciones = new System.Windows.Forms.Panel();
            this.txtEmpleado2 = new System.Windows.Forms.TextBox();
            this.dtpVFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.dtpVFechaIni = new System.Windows.Forms.DateTimePicker();
            this.txtDiasPendientes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReemplazo = new System.Windows.Forms.TextBox();
            this.lstEmpleado = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVacacionesPend)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVacacionesPendVista)).BeginInit();
            this.pProgramarVacaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExcel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxArea);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtEmpleado);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1037, 111);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Búsqueda: ";
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
            this.btnExcel.Location = new System.Drawing.Point(721, 39);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 231;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(442, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 230;
            this.label3.Text = "Área:";
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(445, 60);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(187, 24);
            this.cbxArea.TabIndex = 113;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            this.cbxArea.DropDownClosed += new System.EventHandler(this.cbxArea_DropDownClosed);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label28.Location = new System.Drawing.Point(24, 37);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(75, 17);
            this.label28.TabIndex = 227;
            this.label28.Text = "Empleado:";
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
            this.btnBuscar.Location = new System.Drawing.Point(665, 39);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 224;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.BackColor = System.Drawing.Color.LightCyan;
            this.txtEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtEmpleado.Location = new System.Drawing.Point(27, 61);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(394, 23);
            this.txtEmpleado.TabIndex = 112;
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            // 
            // dtgVacacionesPend
            // 
            this.dtgVacacionesPend.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgVacacionesPend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgVacacionesPend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgVacacionesPend.Location = new System.Drawing.Point(20, 191);
            this.dtgVacacionesPend.LookAndFeel.SkinMaskColor = System.Drawing.Color.Cyan;
            this.dtgVacacionesPend.LookAndFeel.SkinName = "Money Twins";
            this.dtgVacacionesPend.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgVacacionesPend.MainView = this.dgvVacacionesPendVista;
            this.dtgVacacionesPend.Name = "dtgVacacionesPend";
            this.dtgVacacionesPend.Size = new System.Drawing.Size(1037, 393);
            this.dtgVacacionesPend.TabIndex = 187;
            this.dtgVacacionesPend.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvVacacionesPendVista});
            this.dtgVacacionesPend.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dtgVacacionesPend_MouseDoubleClick);
            this.dtgVacacionesPend.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgVacacionesPend_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarVacaciones});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // tsEliminarVacaciones
            // 
            this.tsEliminarVacaciones.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsEliminarVacaciones.Name = "tsEliminarVacaciones";
            this.tsEliminarVacaciones.Size = new System.Drawing.Size(117, 22);
            this.tsEliminarVacaciones.Text = "Eliminar";
            this.tsEliminarVacaciones.Click += new System.EventHandler(this.tsEliminarVacaciones_Click);
            // 
            // dgvVacacionesPendVista
            // 
            this.dgvVacacionesPendVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVacacionesPendVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvVacacionesPendVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVacacionesPendVista.Appearance.Row.Options.UseFont = true;
            this.dgvVacacionesPendVista.GridControl = this.dtgVacacionesPend;
            this.dgvVacacionesPendVista.Name = "dgvVacacionesPendVista";
            this.dgvVacacionesPendVista.OptionsBehavior.Editable = false;
            this.dgvVacacionesPendVista.OptionsView.ColumnAutoWidth = false;
            this.dgvVacacionesPendVista.OptionsView.RowAutoHeight = true;
            this.dgvVacacionesPendVista.OptionsView.ShowFooter = true;
            this.dgvVacacionesPendVista.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.dgvVacacionesPendVista_CellMerge);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1037, 20);
            this.panel2.TabIndex = 188;
            // 
            // pProgramarVacaciones
            // 
            this.pProgramarVacaciones.BackColor = System.Drawing.Color.White;
            this.pProgramarVacaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pProgramarVacaciones.Controls.Add(this.txtEmpleado2);
            this.pProgramarVacaciones.Controls.Add(this.dtpVFechaFin);
            this.pProgramarVacaciones.Controls.Add(this.label2);
            this.pProgramarVacaciones.Controls.Add(this.label18);
            this.pProgramarVacaciones.Controls.Add(this.dtpVFechaIni);
            this.pProgramarVacaciones.Controls.Add(this.txtDiasPendientes);
            this.pProgramarVacaciones.Controls.Add(this.label5);
            this.pProgramarVacaciones.Controls.Add(this.label1);
            this.pProgramarVacaciones.Controls.Add(this.btnCerrar);
            this.pProgramarVacaciones.Controls.Add(this.label9);
            this.pProgramarVacaciones.Controls.Add(this.metroLabel1);
            this.pProgramarVacaciones.Controls.Add(this.btnAgregar);
            this.pProgramarVacaciones.Controls.Add(this.label4);
            this.pProgramarVacaciones.Controls.Add(this.txtReemplazo);
            this.pProgramarVacaciones.Controls.Add(this.lstEmpleado);
            this.pProgramarVacaciones.Location = new System.Drawing.Point(720, 296);
            this.pProgramarVacaciones.Name = "pProgramarVacaciones";
            this.pProgramarVacaciones.Size = new System.Drawing.Size(378, 316);
            this.pProgramarVacaciones.TabIndex = 224;
            this.pProgramarVacaciones.Visible = false;
            this.pProgramarVacaciones.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pProgramarVacaciones_MouseMove);
            // 
            // txtEmpleado2
            // 
            this.txtEmpleado2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpleado2.Location = new System.Drawing.Point(19, 71);
            this.txtEmpleado2.Multiline = true;
            this.txtEmpleado2.Name = "txtEmpleado2";
            this.txtEmpleado2.ReadOnly = true;
            this.txtEmpleado2.Size = new System.Drawing.Size(336, 41);
            this.txtEmpleado2.TabIndex = 252;
            // 
            // dtpVFechaFin
            // 
            this.dtpVFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpVFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVFechaFin.Location = new System.Drawing.Point(260, 212);
            this.dtpVFechaFin.Name = "dtpVFechaFin";
            this.dtpVFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpVFechaFin.TabIndex = 245;
            this.dtpVFechaFin.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpVFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpVFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(141, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 250;
            this.label2.Text = "Ingresar Fechas:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(241, 214);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 15);
            this.label18.TabIndex = 243;
            this.label18.Text = "--";
            // 
            // dtpVFechaIni
            // 
            this.dtpVFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpVFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVFechaIni.Location = new System.Drawing.Point(144, 212);
            this.dtpVFechaIni.Name = "dtpVFechaIni";
            this.dtpVFechaIni.Size = new System.Drawing.Size(95, 20);
            this.dtpVFechaIni.TabIndex = 244;
            this.dtpVFechaIni.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpVFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpVFechaIni_KeyPress);
            // 
            // txtDiasPendientes
            // 
            this.txtDiasPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasPendientes.Location = new System.Drawing.Point(19, 211);
            this.txtDiasPendientes.Name = "txtDiasPendientes";
            this.txtDiasPendientes.ReadOnly = true;
            this.txtDiasPendientes.Size = new System.Drawing.Size(75, 21);
            this.txtDiasPendientes.TabIndex = 247;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(16, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 246;
            this.label5.Text = "Días Pendientes:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 242;
            this.label1.Text = "Empleado:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(352, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 224;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.label9.Location = new System.Drawing.Point(10, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(299, 25);
            this.label9.TabIndex = 144;
            this.label9.Text = "PROGRAMAR VACACIONES";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel1.Location = new System.Drawing.Point(97, 18);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(0, 0);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 104;
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(138, 259);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 38);
            this.btnAgregar.TabIndex = 251;
            this.btnAgregar.Tag = "5";
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(16, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 15);
            this.label4.TabIndex = 253;
            this.label4.Text = "Reemplazo:";
            // 
            // txtReemplazo
            // 
            this.txtReemplazo.BackColor = System.Drawing.Color.LightCyan;
            this.txtReemplazo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReemplazo.Location = new System.Drawing.Point(19, 150);
            this.txtReemplazo.Name = "txtReemplazo";
            this.txtReemplazo.Size = new System.Drawing.Size(336, 21);
            this.txtReemplazo.TabIndex = 254;
            this.txtReemplazo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReemplazo_KeyPress);
            this.txtReemplazo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtReemplazo_KeyUp);
            // 
            // lstEmpleado
            // 
            this.lstEmpleado.BackColor = System.Drawing.Color.LightCyan;
            this.lstEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpleado.FullRowSelect = true;
            this.lstEmpleado.GridLines = true;
            this.lstEmpleado.Location = new System.Drawing.Point(19, 170);
            this.lstEmpleado.MultiSelect = false;
            this.lstEmpleado.Name = "lstEmpleado";
            this.lstEmpleado.Size = new System.Drawing.Size(336, 127);
            this.lstEmpleado.TabIndex = 255;
            this.lstEmpleado.UseCompatibleStateImageBehavior = false;
            this.lstEmpleado.View = System.Windows.Forms.View.Details;
            this.lstEmpleado.Visible = false;
            this.lstEmpleado.Enter += new System.EventHandler(this.lstEmpleado_Enter);
            this.lstEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpleado_KeyPress);
            this.lstEmpleado.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEmpleado_MouseDoubleClick);
            // 
            // frmVacacionesPendientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 604);
            this.Controls.Add(this.dtgVacacionesPend);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pProgramarVacaciones);
            this.Name = "frmVacacionesPendientes";
            this.Text = "REGISTRO DE VACACIONES PENDIENTES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVacacionesPendientes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVacacionesPend)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVacacionesPendVista)).EndInit();
            this.pProgramarVacaciones.ResumeLayout(false);
            this.pProgramarVacaciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.Label label28;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.TextBox txtEmpleado;
        private DevExpress.XtraGrid.GridControl dtgVacacionesPend;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvVacacionesPendVista;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarVacaciones;
        private System.Windows.Forms.Panel pProgramarVacaciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpVFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DateTimePicker dtpVFechaIni;
        private System.Windows.Forms.TextBox txtDiasPendientes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmpleado2;
        public DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.TextBox txtReemplazo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lstEmpleado;
    }
}