namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    partial class frmAnalisisFallas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalisisFallas));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.rbListaPendientes = new System.Windows.Forms.RadioButton();
            this.rbListaSolucionadas = new System.Windows.Forms.RadioButton();
            this.rbListaTodas = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBuscarPlaca = new System.Windows.Forms.TextBox();
            this.dtgAnalisisFallas = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsBorrarRaizFalla = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAnalisisFallasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pRaizFalla = new System.Windows.Forms.Panel();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRaizFalla = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblSR = new System.Windows.Forms.Label();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblTracto = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAnalisisFallas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalisisFallasView)).BeginInit();
            this.pRaizFalla.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkRed;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(881, 43);
            this.label1.TabIndex = 18;
            this.label1.Text = "ANÁLISIS DE FALLAS DE UNIDADES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.rbListaPendientes);
            this.panel3.Controls.Add(this.rbListaSolucionadas);
            this.panel3.Controls.Add(this.rbListaTodas);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.cbxOperacion);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtBuscarPlaca);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(881, 104);
            this.panel3.TabIndex = 19;
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
            this.btnExcel.Location = new System.Drawing.Point(741, 29);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 45);
            this.btnExcel.TabIndex = 194;
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
            this.btnBuscar.Location = new System.Drawing.Point(677, 29);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(51, 45);
            this.btnBuscar.TabIndex = 193;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // rbListaPendientes
            // 
            this.rbListaPendientes.AutoSize = true;
            this.rbListaPendientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.rbListaPendientes.Location = new System.Drawing.Point(526, 41);
            this.rbListaPendientes.Name = "rbListaPendientes";
            this.rbListaPendientes.Size = new System.Drawing.Size(87, 19);
            this.rbListaPendientes.TabIndex = 192;
            this.rbListaPendientes.Text = "Pendientes";
            this.rbListaPendientes.UseVisualStyleBackColor = true;
            this.rbListaPendientes.Click += new System.EventHandler(this.rbListaPendientes_Click);
            // 
            // rbListaSolucionadas
            // 
            this.rbListaSolucionadas.AutoSize = true;
            this.rbListaSolucionadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.rbListaSolucionadas.Location = new System.Drawing.Point(526, 64);
            this.rbListaSolucionadas.Name = "rbListaSolucionadas";
            this.rbListaSolucionadas.Size = new System.Drawing.Size(98, 19);
            this.rbListaSolucionadas.TabIndex = 191;
            this.rbListaSolucionadas.Text = "Completadas";
            this.rbListaSolucionadas.UseVisualStyleBackColor = true;
            this.rbListaSolucionadas.Click += new System.EventHandler(this.rbListaSolucionadas_Click);
            // 
            // rbListaTodas
            // 
            this.rbListaTodas.AutoSize = true;
            this.rbListaTodas.Checked = true;
            this.rbListaTodas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.rbListaTodas.Location = new System.Drawing.Point(526, 17);
            this.rbListaTodas.Name = "rbListaTodas";
            this.rbListaTodas.Size = new System.Drawing.Size(59, 19);
            this.rbListaTodas.TabIndex = 190;
            this.rbListaTodas.TabStop = true;
            this.rbListaTodas.Text = "Todas";
            this.rbListaTodas.UseVisualStyleBackColor = true;
            this.rbListaTodas.Click += new System.EventHandler(this.rbListaTodas_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.dtpFechaIni);
            this.groupBox4.Controls.Add(this.dtpFechaFin);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.groupBox4.Location = new System.Drawing.Point(249, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 66);
            this.groupBox4.TabIndex = 189;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buscar por Fecha de Inicio: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label13.Location = new System.Drawing.Point(117, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 15);
            this.label13.TabIndex = 184;
            this.label13.Text = "--";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(15, 28);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 182;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.2F);
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(134, 28);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 183;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label6.Location = new System.Drawing.Point(20, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 188;
            this.label6.Text = "Operación:";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(93, 58);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(127, 21);
            this.cbxOperacion.TabIndex = 187;
            this.cbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            this.cbxOperacion.DropDownClosed += new System.EventHandler(this.cbxOperacion_DropDownClosed);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label10.Location = new System.Drawing.Point(37, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 15);
            this.label10.TabIndex = 118;
            this.label10.Text = "Unidad:";
            // 
            // txtBuscarPlaca
            // 
            this.txtBuscarPlaca.BackColor = System.Drawing.SystemColors.Window;
            this.txtBuscarPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.txtBuscarPlaca.Location = new System.Drawing.Point(93, 24);
            this.txtBuscarPlaca.Name = "txtBuscarPlaca";
            this.txtBuscarPlaca.Size = new System.Drawing.Size(127, 21);
            this.txtBuscarPlaca.TabIndex = 117;
            this.txtBuscarPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarPlaca_KeyPress);
            // 
            // dtgAnalisisFallas
            // 
            this.dtgAnalisisFallas.AllowDrop = true;
            this.dtgAnalisisFallas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgAnalisisFallas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgAnalisisFallas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgAnalisisFallas.Location = new System.Drawing.Point(0, 147);
            this.dtgAnalisisFallas.LookAndFeel.SkinMaskColor = System.Drawing.Color.Red;
            this.dtgAnalisisFallas.LookAndFeel.SkinName = "Money Twins";
            this.dtgAnalisisFallas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgAnalisisFallas.MainView = this.dgvAnalisisFallasView;
            this.dtgAnalisisFallas.Name = "dtgAnalisisFallas";
            this.dtgAnalisisFallas.Size = new System.Drawing.Size(881, 358);
            this.dtgAnalisisFallas.TabIndex = 100;
            this.dtgAnalisisFallas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvAnalisisFallasView});
            this.dtgAnalisisFallas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dtgAnalisisFallas_MouseDoubleClick);
            this.dtgAnalisisFallas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgAnalisisFallas_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBorrarRaizFalla});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 26);
            // 
            // tsBorrarRaizFalla
            // 
            this.tsBorrarRaizFalla.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.tsBorrarRaizFalla.Name = "tsBorrarRaizFalla";
            this.tsBorrarRaizFalla.Size = new System.Drawing.Size(165, 22);
            this.tsBorrarRaizFalla.Text = "Borrar Causa Raíz";
            this.tsBorrarRaizFalla.Click += new System.EventHandler(this.tsBorrarRaizFalla_Click);
            // 
            // dgvAnalisisFallasView
            // 
            this.dgvAnalisisFallasView.GridControl = this.dtgAnalisisFallas;
            this.dgvAnalisisFallasView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvAnalisisFallasView.Name = "dgvAnalisisFallasView";
            this.dgvAnalisisFallasView.OptionsBehavior.Editable = false;
            this.dgvAnalisisFallasView.OptionsView.ColumnAutoWidth = false;
            this.dgvAnalisisFallasView.OptionsView.RowAutoHeight = true;
            this.dgvAnalisisFallasView.OptionsView.ShowFooter = true;
            this.dgvAnalisisFallasView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvAnalisisFallasView_CustomDrawCell);
            // 
            // pRaizFalla
            // 
            this.pRaizFalla.BackColor = System.Drawing.Color.LemonChiffon;
            this.pRaizFalla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRaizFalla.Controls.Add(this.btnActualizar);
            this.pRaizFalla.Controls.Add(this.groupBox2);
            this.pRaizFalla.Controls.Add(this.btnCerrar);
            this.pRaizFalla.Controls.Add(this.label8);
            this.pRaizFalla.Controls.Add(this.label18);
            this.pRaizFalla.Controls.Add(this.lblSR);
            this.pRaizFalla.Controls.Add(this.lblMotivo);
            this.pRaizFalla.Controls.Add(this.label15);
            this.pRaizFalla.Controls.Add(this.lblTracto);
            this.pRaizFalla.Controls.Add(this.label12);
            this.pRaizFalla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.pRaizFalla.Location = new System.Drawing.Point(495, 230);
            this.pRaizFalla.Name = "pRaizFalla";
            this.pRaizFalla.Size = new System.Drawing.Size(386, 275);
            this.pRaizFalla.TabIndex = 229;
            this.pRaizFalla.Visible = false;
            this.pRaizFalla.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pRaizFalla_MouseMove);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnActualizar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnActualizar.Appearance.Options.UseBackColor = true;
            this.btnActualizar.Appearance.Options.UseBorderColor = true;
            this.btnActualizar.Appearance.Options.UseFont = true;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(143, 223);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(99, 37);
            this.btnActualizar.TabIndex = 142;
            this.btnActualizar.Tag = "5";
            this.btnActualizar.Text = "Guardar";
            this.btnActualizar.ToolTip = "Guardar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRaizFalla);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox2.Location = new System.Drawing.Point(14, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 113);
            this.groupBox2.TabIndex = 141;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ingresar Raíz de Falla: ";
            // 
            // txtRaizFalla
            // 
            this.txtRaizFalla.BackColor = System.Drawing.SystemColors.Window;
            this.txtRaizFalla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.txtRaizFalla.Location = new System.Drawing.Point(13, 26);
            this.txtRaizFalla.Multiline = true;
            this.txtRaizFalla.Name = "txtRaizFalla";
            this.txtRaizFalla.Size = new System.Drawing.Size(328, 69);
            this.txtRaizFalla.TabIndex = 193;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(357, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(28, 28);
            this.btnCerrar.TabIndex = 133;
            this.btnCerrar.Text = "X";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DarkRed;
            this.label8.Location = new System.Drawing.Point(10, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(183, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "RAÍZ DE LA FALLA";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(190, 43);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(36, 18);
            this.label18.TabIndex = 144;
            this.label18.Text = "SR:";
            // 
            // lblSR
            // 
            this.lblSR.AutoSize = true;
            this.lblSR.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblSR.ForeColor = System.Drawing.Color.LightCoral;
            this.lblSR.Location = new System.Drawing.Point(227, 43);
            this.lblSR.Name = "lblSR";
            this.lblSR.Size = new System.Drawing.Size(73, 18);
            this.lblSR.TabIndex = 143;
            this.lblSR.Text = "T4G-963";
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotivo.ForeColor = System.Drawing.Color.LightCoral;
            this.lblMotivo.Location = new System.Drawing.Point(70, 71);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(16, 15);
            this.lblMotivo.TabIndex = 140;
            this.lblMotivo.Text = "S";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(11, 69);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 18);
            this.label15.TabIndex = 139;
            this.label15.Text = "FALLA: ";
            // 
            // lblTracto
            // 
            this.lblTracto.AutoSize = true;
            this.lblTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTracto.ForeColor = System.Drawing.Color.LightCoral;
            this.lblTracto.Location = new System.Drawing.Point(89, 43);
            this.lblTracto.Name = "lblTracto";
            this.lblTracto.Size = new System.Drawing.Size(73, 18);
            this.lblTracto.TabIndex = 138;
            this.lblTracto.Text = "T4G-963";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(11, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 137;
            this.label12.Text = "TRACTO:";
            // 
            // frmAnalisisFallas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 505);
            this.Controls.Add(this.dtgAnalisisFallas);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pRaizFalla);
            this.Name = "frmAnalisisFallas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ANÁLISIS DE FALLAS DE UNIDADES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAnalisisFallas_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAnalisisFallas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalisisFallasView)).EndInit();
            this.pRaizFalla.ResumeLayout(false);
            this.pRaizFalla.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBuscarPlaca;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxOperacion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.RadioButton rbListaPendientes;
        private System.Windows.Forms.RadioButton rbListaSolucionadas;
        private System.Windows.Forms.RadioButton rbListaTodas;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgAnalisisFallas;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvAnalisisFallasView;
        private System.Windows.Forms.Panel pRaizFalla;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblSR;
        public DevExpress.XtraEditors.SimpleButton btnActualizar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRaizFalla;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblTracto;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsBorrarRaizFalla;
    }
}