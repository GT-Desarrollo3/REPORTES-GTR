namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    partial class frmListarReclamosClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarReclamosClientes));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.p_Descargo = new System.Windows.Forms.Panel();
            this.txtResponsable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaProyectado = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGuardarDescargo = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtDescargo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGuardarSolucion = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtSolucion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtgLista = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.registrarDescargoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subsanarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noSolucionadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnPDF = new DevExpress.XtraEditors.SimpleButton();
            this.txtNombrePDF = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.p_Descargo.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Blue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1257, 44);
            this.label2.TabIndex = 19;
            this.label2.Text = "LISTA DE RECLAMOS CLIENTES";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.btnNuevo);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1257, 78);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.BackColor2 = System.Drawing.Color.White;
            this.simpleButton1.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseBorderColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton1.Location = new System.Drawing.Point(282, 23);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(83, 40);
            this.simpleButton1.TabIndex = 20;
            this.simpleButton1.Tag = "5";
            this.simpleButton1.Text = "Buscar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.Appearance.Options.UseBackColor = true;
            this.btnNuevo.Appearance.Options.UseBorderColor = true;
            this.btnNuevo.Appearance.Options.UseFont = true;
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(399, 22);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(97, 42);
            this.btnNuevo.TabIndex = 19;
            this.btnNuevo.Tag = "5";
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpFechaFin);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(126, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(120, 59);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fecha Fin";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(9, 23);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(101, 20);
            this.dtpFechaFin.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFechaInicio);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(3, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 59);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fecha Inicio";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(7, 23);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(103, 20);
            this.dtpFechaInicio.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.p_Descargo);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.dtgLista);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1257, 482);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // p_Descargo
            // 
            this.p_Descargo.Controls.Add(this.txtNombrePDF);
            this.p_Descargo.Controls.Add(this.btnPDF);
            this.p_Descargo.Controls.Add(this.txtResponsable);
            this.p_Descargo.Controls.Add(this.label4);
            this.p_Descargo.Controls.Add(this.dtpFechaProyectado);
            this.p_Descargo.Controls.Add(this.label3);
            this.p_Descargo.Controls.Add(this.button1);
            this.p_Descargo.Controls.Add(this.btnGuardarDescargo);
            this.p_Descargo.Controls.Add(this.groupBox5);
            this.p_Descargo.Controls.Add(this.label1);
            this.p_Descargo.Location = new System.Drawing.Point(268, 112);
            this.p_Descargo.Name = "p_Descargo";
            this.p_Descargo.Size = new System.Drawing.Size(442, 291);
            this.p_Descargo.TabIndex = 14;
            this.p_Descargo.Visible = false;
            // 
            // txtResponsable
            // 
            this.txtResponsable.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResponsable.Location = new System.Drawing.Point(194, 75);
            this.txtResponsable.Name = "txtResponsable";
            this.txtResponsable.Size = new System.Drawing.Size(176, 20);
            this.txtResponsable.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Personal Relacionado (Opcional):";
            // 
            // dtpFechaProyectado
            // 
            this.dtpFechaProyectado.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaProyectado.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaProyectado.Location = new System.Drawing.Point(34, 74);
            this.dtpFechaProyectado.Name = "dtpFechaProyectado";
            this.dtpFechaProyectado.Size = new System.Drawing.Size(140, 20);
            this.dtpFechaProyectado.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Fecha Proyectado Solucion:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(404, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 24);
            this.button1.TabIndex = 22;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGuardarDescargo
            // 
            this.btnGuardarDescargo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardarDescargo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardarDescargo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardarDescargo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarDescargo.Appearance.Options.UseBackColor = true;
            this.btnGuardarDescargo.Appearance.Options.UseBorderColor = true;
            this.btnGuardarDescargo.Appearance.Options.UseFont = true;
            this.btnGuardarDescargo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarDescargo.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarDescargo.Image")));
            this.btnGuardarDescargo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardarDescargo.Location = new System.Drawing.Point(175, 247);
            this.btnGuardarDescargo.Name = "btnGuardarDescargo";
            this.btnGuardarDescargo.Size = new System.Drawing.Size(89, 40);
            this.btnGuardarDescargo.TabIndex = 21;
            this.btnGuardarDescargo.Tag = "5";
            this.btnGuardarDescargo.Text = "Guardar";
            this.btnGuardarDescargo.Click += new System.EventHandler(this.btnGuardarDescargo_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtDescargo);
            this.groupBox5.Location = new System.Drawing.Point(34, 155);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(358, 91);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Detalle Descargo";
            // 
            // txtDescargo
            // 
            this.txtDescargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescargo.Location = new System.Drawing.Point(23, 20);
            this.txtDescargo.Multiline = true;
            this.txtDescargo.Name = "txtDescargo";
            this.txtDescargo.Size = new System.Drawing.Size(313, 65);
            this.txtDescargo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 44);
            this.label1.TabIndex = 20;
            this.label1.Text = "REGISTRAR DESCARGO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnGuardarSolucion);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(294, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 256);
            this.panel1.TabIndex = 25;
            this.panel1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(393, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 24);
            this.button2.TabIndex = 22;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGuardarSolucion
            // 
            this.btnGuardarSolucion.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardarSolucion.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardarSolucion.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardarSolucion.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarSolucion.Appearance.Options.UseBackColor = true;
            this.btnGuardarSolucion.Appearance.Options.UseBorderColor = true;
            this.btnGuardarSolucion.Appearance.Options.UseFont = true;
            this.btnGuardarSolucion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarSolucion.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarSolucion.Image")));
            this.btnGuardarSolucion.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardarSolucion.Location = new System.Drawing.Point(179, 188);
            this.btnGuardarSolucion.Name = "btnGuardarSolucion";
            this.btnGuardarSolucion.Size = new System.Drawing.Size(89, 40);
            this.btnGuardarSolucion.TabIndex = 21;
            this.btnGuardarSolucion.Tag = "5";
            this.btnGuardarSolucion.Text = "Guardar";
            this.btnGuardarSolucion.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtSolucion);
            this.groupBox6.Location = new System.Drawing.Point(38, 82);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(358, 91);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Detalle Solucion";
            // 
            // txtSolucion
            // 
            this.txtSolucion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSolucion.Location = new System.Drawing.Point(23, 20);
            this.txtSolucion.Multiline = true;
            this.txtSolucion.Name = "txtSolucion";
            this.txtSolucion.Size = new System.Drawing.Size(313, 65);
            this.txtSolucion.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LimeGreen;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(428, 44);
            this.label5.TabIndex = 20;
            this.label5.Text = "REGISTRAR SOLUCION";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgLista
            // 
            this.dtgLista.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgLista.Location = new System.Drawing.Point(3, 16);
            this.dtgLista.MainView = this.dgvListaVista;
            this.dtgLista.Name = "dtgLista";
            this.dtgLista.Size = new System.Drawing.Size(1251, 463);
            this.dtgLista.TabIndex = 26;
            this.dtgLista.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaVista,
            this.gridView1});
            this.dtgLista.DoubleClick += new System.EventHandler(this.dtgLista_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarDescargoToolStripMenuItem,
            this.subsanarToolStripMenuItem,
            this.noSolucionadoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 70);
            // 
            // registrarDescargoToolStripMenuItem
            // 
            this.registrarDescargoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("registrarDescargoToolStripMenuItem.Image")));
            this.registrarDescargoToolStripMenuItem.Name = "registrarDescargoToolStripMenuItem";
            this.registrarDescargoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.registrarDescargoToolStripMenuItem.Text = "Registrar Descargo";
            this.registrarDescargoToolStripMenuItem.Click += new System.EventHandler(this.registrarDescargoToolStripMenuItem_Click);
            // 
            // subsanarToolStripMenuItem
            // 
            this.subsanarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("subsanarToolStripMenuItem.Image")));
            this.subsanarToolStripMenuItem.Name = "subsanarToolStripMenuItem";
            this.subsanarToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.subsanarToolStripMenuItem.Text = "Registrar Solucion";
            this.subsanarToolStripMenuItem.Click += new System.EventHandler(this.subsanarToolStripMenuItem_Click);
            // 
            // noSolucionadoToolStripMenuItem
            // 
            this.noSolucionadoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("noSolucionadoToolStripMenuItem.Image")));
            this.noSolucionadoToolStripMenuItem.Name = "noSolucionadoToolStripMenuItem";
            this.noSolucionadoToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.noSolucionadoToolStripMenuItem.Text = "No Solucionado";
            this.noSolucionadoToolStripMenuItem.Click += new System.EventHandler(this.noSolucionadoToolStripMenuItem_Click);
            // 
            // dgvListaVista
            // 
            this.dgvListaVista.GridControl = this.dtgLista;
            this.dgvListaVista.Name = "dgvListaVista";
            this.dgvListaVista.OptionsBehavior.Editable = false;
            this.dgvListaVista.OptionsPrint.PrintSelectedRowsOnly = true;
            this.dgvListaVista.OptionsSelection.MultiSelect = true;
            this.dgvListaVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaVista.OptionsView.RowAutoHeight = true;
            this.dgvListaVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaVista_CustomDrawCell_1);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgLista;
            this.gridView1.Name = "gridView1";
            // 
            // btnPDF
            // 
            this.btnPDF.Appearance.BackColor = System.Drawing.Color.White;
            this.btnPDF.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnPDF.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnPDF.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPDF.Appearance.Options.UseBackColor = true;
            this.btnPDF.Appearance.Options.UseBorderColor = true;
            this.btnPDF.Appearance.Options.UseFont = true;
            this.btnPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnPDF.Image")));
            this.btnPDF.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnPDF.Location = new System.Drawing.Point(36, 102);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(99, 40);
            this.btnPDF.TabIndex = 27;
            this.btnPDF.Tag = "5";
            this.btnPDF.Text = "Subir PDF";
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // txtNombrePDF
            // 
            this.txtNombrePDF.AutoSize = true;
            this.txtNombrePDF.Location = new System.Drawing.Point(141, 116);
            this.txtNombrePDF.Name = "txtNombrePDF";
            this.txtNombrePDF.Size = new System.Drawing.Size(83, 13);
            this.txtNombrePDF.TabIndex = 28;
            this.txtNombrePDF.Text = "Nombre doc.pdf";
            // 
            // frmListarReclamosClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(1257, 604);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Name = "frmListarReclamosClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListarReclamosClientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListarReclamosClientes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.p_Descargo.ResumeLayout(false);
            this.p_Descargo.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private DevExpress.XtraEditors.SimpleButton btnNuevo;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem registrarDescargoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subsanarToolStripMenuItem;
        private System.Windows.Forms.Panel p_Descargo;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.SimpleButton btnGuardarDescargo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtDescargo;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpFechaProyectado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraEditors.SimpleButton btnGuardarSolucion;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtSolucion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem noSolucionadoToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl dtgLista;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TextBox txtResponsable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txtNombrePDF;
        private DevExpress.XtraEditors.SimpleButton btnPDF;
    }
}