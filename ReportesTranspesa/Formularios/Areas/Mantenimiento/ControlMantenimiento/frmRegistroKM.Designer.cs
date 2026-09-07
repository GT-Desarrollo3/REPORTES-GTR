namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmRegistroKM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroKM));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.pActualizar = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBuscarPlaca = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUltKM = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroLabel20 = new MetroFramework.Controls.MetroLabel();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.lstPlaca = new System.Windows.Forms.ListView();
            this.dtgRegistroKM = new DevExpress.XtraGrid.GridControl();
            this.dgvRegistroKMVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pImportarKM = new System.Windows.Forms.Panel();
            this.dgvKilometraje = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBuscarArchivo = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnCerrar2 = new System.Windows.Forms.PictureBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnGenerar = new DevExpress.XtraEditors.SimpleButton();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pActualizar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroKM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroKMVista)).BeginInit();
            this.pImportarKM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKilometraje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(897, 50);
            this.label1.TabIndex = 13;
            this.label1.Text = "REGISTRO DIARIO DE KM";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.btnActualizar);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.btnAgregar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(897, 89);
            this.panel4.TabIndex = 14;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnActualizar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Appearance.Options.UseBackColor = true;
            this.btnActualizar.Appearance.Options.UseBorderColor = true;
            this.btnActualizar.Appearance.Options.UseFont = true;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnActualizar.Location = new System.Drawing.Point(756, 21);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(47, 47);
            this.btnActualizar.TabIndex = 214;
            this.btnActualizar.Tag = "5";
            this.btnActualizar.ToolTip = "Actualizar KM";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPlaca);
            this.groupBox2.Location = new System.Drawing.Point(168, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 58);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Placa:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(16, 24);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(144, 20);
            this.txtPlaca.TabIndex = 204;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Location = new System.Drawing.Point(368, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 58);
            this.groupBox1.TabIndex = 213;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(133, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(116, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "-";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(15, 24);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(818, 21);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 212;
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
            this.btnBuscar.Location = new System.Drawing.Point(695, 21);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(24, 23);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(123, 47);
            this.btnAgregar.TabIndex = 216;
            this.btnAgregar.Tag = "5";
            this.btnAgregar.Text = "Importar\r\nKilometrajes";
            this.btnAgregar.ToolTip = "Agregar Unidad";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // pActualizar
            // 
            this.pActualizar.BackColor = System.Drawing.Color.LemonChiffon;
            this.pActualizar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pActualizar.Controls.Add(this.label5);
            this.pActualizar.Controls.Add(this.txtBuscarPlaca);
            this.pActualizar.Controls.Add(this.label4);
            this.pActualizar.Controls.Add(this.label3);
            this.pActualizar.Controls.Add(this.txtUltKM);
            this.pActualizar.Controls.Add(this.pictureBox1);
            this.pActualizar.Controls.Add(this.metroLabel20);
            this.pActualizar.Controls.Add(this.dtpFecha);
            this.pActualizar.Controls.Add(this.label10);
            this.pActualizar.Controls.Add(this.btnGuardar);
            this.pActualizar.Controls.Add(this.lstPlaca);
            this.pActualizar.Location = new System.Drawing.Point(240, 172);
            this.pActualizar.Name = "pActualizar";
            this.pActualizar.Size = new System.Drawing.Size(415, 192);
            this.pActualizar.TabIndex = 132;
            this.pActualizar.Visible = false;
            this.pActualizar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pActualizar_MouseMove);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(40, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 146;
            this.label5.Text = "Placa:";
            // 
            // txtBuscarPlaca
            // 
            this.txtBuscarPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtBuscarPlaca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBuscarPlaca.Location = new System.Drawing.Point(83, 51);
            this.txtBuscarPlaca.Name = "txtBuscarPlaca";
            this.txtBuscarPlaca.Size = new System.Drawing.Size(126, 20);
            this.txtBuscarPlaca.TabIndex = 145;
            this.txtBuscarPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarPlaca_KeyPress);
            this.txtBuscarPlaca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarPlaca_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(16, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(351, 20);
            this.label4.TabIndex = 144;
            this.label4.Text = "ACTUALIZAR KILOMETRAJE DE UNIDAD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(211, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 143;
            this.label3.Text = "Últ. KM:";
            // 
            // txtUltKM
            // 
            this.txtUltKM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUltKM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtUltKM.Location = new System.Drawing.Point(262, 89);
            this.txtUltKM.Name = "txtUltKM";
            this.txtUltKM.Size = new System.Drawing.Size(96, 20);
            this.txtUltKM.TabIndex = 138;
            this.txtUltKM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUltKM_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(385, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 107;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // metroLabel20
            // 
            this.metroLabel20.AutoSize = true;
            this.metroLabel20.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel20.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel20.Location = new System.Drawing.Point(97, 18);
            this.metroLabel20.Name = "metroLabel20";
            this.metroLabel20.Size = new System.Drawing.Size(0, 0);
            this.metroLabel20.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel20.TabIndex = 104;
            this.metroLabel20.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(83, 89);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(96, 20);
            this.dtpFecha.TabIndex = 136;
            this.dtpFecha.Tag = "2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label10.Location = new System.Drawing.Point(37, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 141;
            this.label10.Text = "Fecha:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(165, 133);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 38);
            this.btnGuardar.TabIndex = 139;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lstPlaca
            // 
            this.lstPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPlaca.ForeColor = System.Drawing.Color.Navy;
            this.lstPlaca.FullRowSelect = true;
            this.lstPlaca.GridLines = true;
            this.lstPlaca.Location = new System.Drawing.Point(83, 70);
            this.lstPlaca.MultiSelect = false;
            this.lstPlaca.Name = "lstPlaca";
            this.lstPlaca.Size = new System.Drawing.Size(275, 104);
            this.lstPlaca.TabIndex = 149;
            this.lstPlaca.UseCompatibleStateImageBehavior = false;
            this.lstPlaca.View = System.Windows.Forms.View.Details;
            this.lstPlaca.Visible = false;
            this.lstPlaca.Enter += new System.EventHandler(this.lstPlaca_Enter);
            this.lstPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPlaca_KeyPress);
            this.lstPlaca.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPlaca_MouseDoubleClick);
            // 
            // dtgRegistroKM
            // 
            this.dtgRegistroKM.AllowDrop = true;
            this.dtgRegistroKM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRegistroKM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRegistroKM.Location = new System.Drawing.Point(0, 139);
            this.dtgRegistroKM.MainView = this.dgvRegistroKMVista;
            this.dtgRegistroKM.Name = "dtgRegistroKM";
            this.dtgRegistroKM.Size = new System.Drawing.Size(897, 401);
            this.dtgRegistroKM.TabIndex = 133;
            this.dtgRegistroKM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRegistroKMVista});
            this.dtgRegistroKM.DoubleClick += new System.EventHandler(this.dtgRegistroKM_DoubleClick);
            // 
            // dgvRegistroKMVista
            // 
            this.dgvRegistroKMVista.GridControl = this.dtgRegistroKM;
            this.dgvRegistroKMVista.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvRegistroKMVista.Name = "dgvRegistroKMVista";
            this.dgvRegistroKMVista.OptionsBehavior.Editable = false;
            this.dgvRegistroKMVista.OptionsView.ColumnAutoWidth = false;
            this.dgvRegistroKMVista.OptionsView.RowAutoHeight = true;
            // 
            // pImportarKM
            // 
            this.pImportarKM.BackColor = System.Drawing.Color.LemonChiffon;
            this.pImportarKM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pImportarKM.Controls.Add(this.dgvKilometraje);
            this.pImportarKM.Controls.Add(this.label6);
            this.pImportarKM.Controls.Add(this.btnBuscarArchivo);
            this.pImportarKM.Controls.Add(this.label7);
            this.pImportarKM.Controls.Add(this.label8);
            this.pImportarKM.Controls.Add(this.txtRuta);
            this.pImportarKM.Controls.Add(this.btnCerrar2);
            this.pImportarKM.Controls.Add(this.metroLabel1);
            this.pImportarKM.Controls.Add(this.btnGenerar);
            this.pImportarKM.Location = new System.Drawing.Point(26, 21);
            this.pImportarKM.Name = "pImportarKM";
            this.pImportarKM.Size = new System.Drawing.Size(846, 492);
            this.pImportarKM.TabIndex = 134;
            this.pImportarKM.Visible = false;
            // 
            // dgvKilometraje
            // 
            this.dgvKilometraje.AllowUserToAddRows = false;
            this.dgvKilometraje.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvKilometraje.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvKilometraje.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKilometraje.Location = new System.Drawing.Point(25, 150);
            this.dgvKilometraje.Name = "dgvKilometraje";
            this.dgvKilometraje.RowHeadersVisible = false;
            this.dgvKilometraje.Size = new System.Drawing.Size(794, 318);
            this.dgvKilometraje.TabIndex = 147;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(196, 16);
            this.label6.TabIndex = 146;
            this.label6.Text = "LISTA DE KILOMETRAJES:";
            // 
            // btnBuscarArchivo
            // 
            this.btnBuscarArchivo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscarArchivo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscarArchivo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscarArchivo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarArchivo.Appearance.Options.UseBackColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseBorderColor = true;
            this.btnBuscarArchivo.Appearance.Options.UseFont = true;
            this.btnBuscarArchivo.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscarArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarArchivo.Image = global::ReportesTranspesa.Properties.Resources.expotexcel;
            this.btnBuscarArchivo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscarArchivo.Location = new System.Drawing.Point(25, 59);
            this.btnBuscarArchivo.Name = "btnBuscarArchivo";
            this.btnBuscarArchivo.Size = new System.Drawing.Size(90, 38);
            this.btnBuscarArchivo.TabIndex = 145;
            this.btnBuscarArchivo.Tag = "5";
            this.btnBuscarArchivo.Text = "Buscar";
            this.btnBuscarArchivo.ToolTip = "Buscar";
            this.btnBuscarArchivo.Click += new System.EventHandler(this.btnBuscarArchivo_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(419, 24);
            this.label7.TabIndex = 144;
            this.label7.Text = "IMPORTAR KILOMETRAJES DE UNIDADES";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.Location = new System.Drawing.Point(139, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 13);
            this.label8.TabIndex = 143;
            this.label8.Text = "Seleccione un archivo:";
            // 
            // txtRuta
            // 
            this.txtRuta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtRuta.Location = new System.Drawing.Point(142, 77);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(561, 20);
            this.txtRuta.TabIndex = 138;
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.btnCerrar2.Location = new System.Drawing.Point(816, 6);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(20, 20);
            this.btnCerrar2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar2.TabIndex = 107;
            this.btnCerrar2.TabStop = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
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
            // btnGenerar
            // 
            this.btnGenerar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGenerar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGenerar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGenerar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Appearance.Options.UseBackColor = true;
            this.btnGenerar.Appearance.Options.UseBorderColor = true;
            this.btnGenerar.Appearance.Options.UseFont = true;
            this.btnGenerar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerar.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerar.Image")));
            this.btnGenerar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGenerar.Location = new System.Drawing.Point(729, 59);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(90, 38);
            this.btnGenerar.TabIndex = 139;
            this.btnGenerar.Tag = "5";
            this.btnGenerar.Text = "Importar\r\nKM";
            this.btnGenerar.ToolTip = "Importar";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // frmRegistroKM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 540);
            this.Controls.Add(this.dtgRegistroKM);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pActualizar);
            this.Controls.Add(this.pImportarKM);
            this.MaximizeBox = false;
            this.Name = "frmRegistroKM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRO DE KM - VEHÍCULOS";
            this.Load += new System.EventHandler(this.frmRegistroKM_Load);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pActualizar.ResumeLayout(false);
            this.pActualizar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistroKM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroKMVista)).EndInit();
            this.pImportarKM.ResumeLayout(false);
            this.pImportarKM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKilometraje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Panel pActualizar;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtBuscarPlaca;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        internal System.Windows.Forms.TextBox txtUltKM;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel metroLabel20;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ListView lstPlaca;
        public DevExpress.XtraEditors.SimpleButton btnActualizar;
        private DevExpress.XtraGrid.GridControl dtgRegistroKM;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRegistroKMVista;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.Panel pImportarKM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.PictureBox btnCerrar2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private DevExpress.XtraEditors.SimpleButton btnGenerar;
        private DevExpress.XtraEditors.SimpleButton btnBuscarArchivo;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DataGridView dgvKilometraje;
    }
}