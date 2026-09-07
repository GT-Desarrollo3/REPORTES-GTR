namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmListaFaltantesMercaderia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaFaltantesMercaderia));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNuevoFaltante = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.rbListaPendientes = new System.Windows.Forms.RadioButton();
            this.rbListaSolucionadas = new System.Windows.Forms.RadioButton();
            this.rbListaTodas = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fechaFin = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.fechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.dtgListaFaltantes = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ModificarFaltanteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditarFaltanteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarFaltanteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaFaltantesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pFacturarFaltante = new System.Windows.Forms.Panel();
            this.lblNroTicket = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDolares = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.rbSoles = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbxAsume = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pFaltanteSinViaje = new System.Windows.Forms.Panel();
            this.lstClientes = new System.Windows.Forms.ListView();
            this.btnModificar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAgregar2 = new System.Windows.Forms.Button();
            this.btnCancelar2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaIncidente = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxMotivo = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaFaltantes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaFaltantesView)).BeginInit();
            this.pFacturarFaltante.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pFaltanteSinViaje.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1099, 56);
            this.label1.TabIndex = 12;
            this.label1.Text = "LISTA DE FALTANTES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnNuevoFaltante);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.rbListaPendientes);
            this.panel3.Controls.Add(this.rbListaSolucionadas);
            this.panel3.Controls.Add(this.rbListaTodas);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.btnExcel);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1099, 94);
            this.panel3.TabIndex = 11;
            // 
            // btnNuevoFaltante
            // 
            this.btnNuevoFaltante.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoFaltante.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNuevoFaltante.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnNuevoFaltante.Location = new System.Drawing.Point(15, 24);
            this.btnNuevoFaltante.Name = "btnNuevoFaltante";
            this.btnNuevoFaltante.Size = new System.Drawing.Size(114, 45);
            this.btnNuevoFaltante.TabIndex = 57;
            this.btnNuevoFaltante.Text = " Faltante sin viaje";
            this.btnNuevoFaltante.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevoFaltante.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevoFaltante.UseVisualStyleBackColor = true;
            this.btnNuevoFaltante.Click += new System.EventHandler(this.btnNuevoFaltante_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_zoom_115406;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(983, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(38, 44);
            this.btnBuscar.TabIndex = 56;
            this.btnBuscar.Text = "&B";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // rbListaPendientes
            // 
            this.rbListaPendientes.AutoSize = true;
            this.rbListaPendientes.Location = new System.Drawing.Point(809, 38);
            this.rbListaPendientes.Name = "rbListaPendientes";
            this.rbListaPendientes.Size = new System.Drawing.Size(124, 17);
            this.rbListaPendientes.TabIndex = 55;
            this.rbListaPendientes.Text = "Faltantes Pendientes";
            this.rbListaPendientes.UseVisualStyleBackColor = true;
            this.rbListaPendientes.Click += new System.EventHandler(this.rbListaPendientes_Click);
            // 
            // rbListaSolucionadas
            // 
            this.rbListaSolucionadas.AutoSize = true;
            this.rbListaSolucionadas.Location = new System.Drawing.Point(809, 61);
            this.rbListaSolucionadas.Name = "rbListaSolucionadas";
            this.rbListaSolucionadas.Size = new System.Drawing.Size(113, 17);
            this.rbListaSolucionadas.TabIndex = 54;
            this.rbListaSolucionadas.Text = "Faltantes Cerrados";
            this.rbListaSolucionadas.UseVisualStyleBackColor = true;
            this.rbListaSolucionadas.Click += new System.EventHandler(this.rbListaSolucionadas_Click);
            // 
            // rbListaTodas
            // 
            this.rbListaTodas.AutoSize = true;
            this.rbListaTodas.Checked = true;
            this.rbListaTodas.Location = new System.Drawing.Point(809, 15);
            this.rbListaTodas.Name = "rbListaTodas";
            this.rbListaTodas.Size = new System.Drawing.Size(55, 17);
            this.rbListaTodas.TabIndex = 53;
            this.rbListaTodas.TabStop = true;
            this.rbListaTodas.Text = "Todos";
            this.rbListaTodas.UseVisualStyleBackColor = true;
            this.rbListaTodas.Click += new System.EventHandler(this.rbListaTodas_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.fechaFin);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.fechaInicio);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(458, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 58);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buscar por Fecha de Viaje o Incidente:";
            // 
            // fechaFin
            // 
            this.fechaFin.CustomFormat = "dd-MM-yyyy";
            this.fechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fechaFin.Location = new System.Drawing.Point(206, 24);
            this.fechaFin.Name = "fechaFin";
            this.fechaFin.Size = new System.Drawing.Size(98, 20);
            this.fechaFin.TabIndex = 5;
            this.fechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fechaFin_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Fin:";
            // 
            // fechaInicio
            // 
            this.fechaInicio.CustomFormat = "dd-MM-yyyy";
            this.fechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fechaInicio.Location = new System.Drawing.Point(55, 24);
            this.fechaInicio.Name = "fechaInicio";
            this.fechaInicio.Size = new System.Drawing.Size(98, 20);
            this.fechaInicio.TabIndex = 2;
            this.fechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fechaInicio_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Inicio:";
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(1037, 28);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 9;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtConductor);
            this.groupBox3.Location = new System.Drawing.Point(145, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 58);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Buscar cliente o conductor:";
            // 
            // txtConductor
            // 
            this.txtConductor.Location = new System.Drawing.Point(13, 24);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(256, 20);
            this.txtConductor.TabIndex = 0;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            // 
            // dtgListaFaltantes
            // 
            this.dtgListaFaltantes.AllowDrop = true;
            this.dtgListaFaltantes.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaFaltantes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaFaltantes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaFaltantes.Location = new System.Drawing.Point(0, 150);
            this.dtgListaFaltantes.MainView = this.dgvListaFaltantesView;
            this.dtgListaFaltantes.Name = "dtgListaFaltantes";
            this.dtgListaFaltantes.Size = new System.Drawing.Size(1099, 461);
            this.dtgListaFaltantes.TabIndex = 13;
            this.dtgListaFaltantes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaFaltantesView});
            this.dtgListaFaltantes.DoubleClick += new System.EventHandler(this.dtgListaFaltantes_DoubleClick);
            this.dtgListaFaltantes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaFaltantes_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModificarFaltanteToolStripMenuItem,
            this.EditarFaltanteToolStripMenuItem,
            this.cerrarFaltanteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 70);
            // 
            // ModificarFaltanteToolStripMenuItem
            // 
            this.ModificarFaltanteToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.ModificarFaltanteToolStripMenuItem.Name = "ModificarFaltanteToolStripMenuItem";
            this.ModificarFaltanteToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ModificarFaltanteToolStripMenuItem.Text = "Editar Faltante Sin Viaje";
            this.ModificarFaltanteToolStripMenuItem.Click += new System.EventHandler(this.ModificarFaltanteToolStripMenuItem_Click);
            // 
            // EditarFaltanteToolStripMenuItem
            // 
            this.EditarFaltanteToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.EditarFaltanteToolStripMenuItem.Name = "EditarFaltanteToolStripMenuItem";
            this.EditarFaltanteToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.EditarFaltanteToolStripMenuItem.Text = "Facturar Faltante";
            this.EditarFaltanteToolStripMenuItem.Click += new System.EventHandler(this.EditarFaltanteToolStripMenuItem_Click);
            // 
            // cerrarFaltanteToolStripMenuItem
            // 
            this.cerrarFaltanteToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.cerrarFaltanteToolStripMenuItem.Name = "cerrarFaltanteToolStripMenuItem";
            this.cerrarFaltanteToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.cerrarFaltanteToolStripMenuItem.Text = "Cerrar Faltante";
            this.cerrarFaltanteToolStripMenuItem.Click += new System.EventHandler(this.cerrarFaltanteToolStripMenuItem_Click);
            // 
            // dgvListaFaltantesView
            // 
            this.dgvListaFaltantesView.GridControl = this.dtgListaFaltantes;
            this.dgvListaFaltantesView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvListaFaltantesView.Name = "dgvListaFaltantesView";
            this.dgvListaFaltantesView.OptionsBehavior.Editable = false;
            this.dgvListaFaltantesView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaFaltantesView.OptionsView.RowAutoHeight = true;
            // 
            // pFacturarFaltante
            // 
            this.pFacturarFaltante.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pFacturarFaltante.Controls.Add(this.lblNroTicket);
            this.pFacturarFaltante.Controls.Add(this.label12);
            this.pFacturarFaltante.Controls.Add(this.btnAgregar);
            this.pFacturarFaltante.Controls.Add(this.btnCancelar);
            this.pFacturarFaltante.Controls.Add(this.groupBox2);
            this.pFacturarFaltante.Controls.Add(this.pictureBox1);
            this.pFacturarFaltante.Location = new System.Drawing.Point(106, 142);
            this.pFacturarFaltante.Name = "pFacturarFaltante";
            this.pFacturarFaltante.Size = new System.Drawing.Size(887, 341);
            this.pFacturarFaltante.TabIndex = 14;
            this.pFacturarFaltante.Visible = false;
            this.pFacturarFaltante.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pFacturarFaltante_MouseMove);
            // 
            // lblNroTicket
            // 
            this.lblNroTicket.AutoSize = true;
            this.lblNroTicket.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.lblNroTicket.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNroTicket.ForeColor = System.Drawing.Color.Red;
            this.lblNroTicket.Location = new System.Drawing.Point(519, 20);
            this.lblNroTicket.Name = "lblNroTicket";
            this.lblNroTicket.Size = new System.Drawing.Size(0, 29);
            this.lblNroTicket.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Swis721 Blk BT", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(279, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(242, 29);
            this.label12.TabIndex = 45;
            this.label12.Text = "PROGRAMACIÓN:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAgregar.Location = new System.Drawing.Point(465, 280);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(114, 42);
            this.btnAgregar.TabIndex = 42;
            this.btnAgregar.Text = " Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(325, 280);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(114, 42);
            this.btnCancelar.TabIndex = 43;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groupBox2.Controls.Add(this.rbDolares);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.rbSoles);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.cbxEstado);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtMonto);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtComentarios);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.cbxAsume);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txtFactura);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(20, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(847, 191);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información de Faltante";
            // 
            // rbDolares
            // 
            this.rbDolares.AutoSize = true;
            this.rbDolares.Location = new System.Drawing.Point(363, 124);
            this.rbDolares.Name = "rbDolares";
            this.rbDolares.Size = new System.Drawing.Size(33, 20);
            this.rbDolares.TabIndex = 59;
            this.rbDolares.Text = "$";
            this.rbDolares.UseVisualStyleBackColor = true;
            this.rbDolares.Click += new System.EventHandler(this.rbDolares_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(261, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 58;
            this.label6.Text = "Monto:";
            // 
            // rbSoles
            // 
            this.rbSoles.AutoSize = true;
            this.rbSoles.Location = new System.Drawing.Point(315, 124);
            this.rbSoles.Name = "rbSoles";
            this.rbSoles.Size = new System.Drawing.Size(42, 20);
            this.rbSoles.TabIndex = 56;
            this.rbSoles.Text = "S/.";
            this.rbSoles.UseVisualStyleBackColor = true;
            this.rbSoles.Click += new System.EventHandler(this.rbSoles_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(442, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 43;
            this.label4.Text = "adicional:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 42;
            this.label3.Text = "de Faltante:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtDescripcion.Location = new System.Drawing.Point(114, 33);
            this.txtDescripcion.MaxLength = 300;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(305, 75);
            this.txtDescripcion.TabIndex = 37;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Location = new System.Drawing.Point(700, 32);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(122, 24);
            this.cbxEstado.TabIndex = 41;
            this.cbxEstado.SelectedIndexChanged += new System.EventHandler(this.cbxEstado_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(248, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 16);
            this.label10.TabIndex = 40;
            this.label10.Text = "Moneda:";
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(315, 150);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(104, 22);
            this.txtMonto.TabIndex = 38;
            this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(641, 37);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 16);
            this.label17.TabIndex = 39;
            this.label17.Text = "Estado:";
            // 
            // txtComentarios
            // 
            this.txtComentarios.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtComentarios.Location = new System.Drawing.Point(525, 70);
            this.txtComentarios.MaxLength = 300;
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(297, 83);
            this.txtComentarios.TabIndex = 37;
            this.txtComentarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComentarios_KeyPress);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(442, 74);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(77, 16);
            this.label26.TabIndex = 12;
            this.label26.Text = "Comentario";
            // 
            // cbxAsume
            // 
            this.cbxAsume.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxAsume.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAsume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAsume.FormattingEnabled = true;
            this.cbxAsume.Location = new System.Drawing.Point(501, 32);
            this.cbxAsume.Name = "cbxAsume";
            this.cbxAsume.Size = new System.Drawing.Size(122, 24);
            this.cbxAsume.TabIndex = 32;
            this.cbxAsume.SelectedIndexChanged += new System.EventHandler(this.cbxAsume_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(16, 137);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 16);
            this.label20.TabIndex = 31;
            this.label20.Text = "N° de Factura:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(114, 134);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(117, 22);
            this.txtFactura.TabIndex = 5;
            this.txtFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFactura_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(442, 37);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 16);
            this.label21.TabIndex = 29;
            this.label21.Text = "Asume:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Descripción";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(858, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pFaltanteSinViaje
            // 
            this.pFaltanteSinViaje.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pFaltanteSinViaje.Controls.Add(this.lstClientes);
            this.pFaltanteSinViaje.Controls.Add(this.btnModificar);
            this.pFaltanteSinViaje.Controls.Add(this.label8);
            this.pFaltanteSinViaje.Controls.Add(this.btnAgregar2);
            this.pFaltanteSinViaje.Controls.Add(this.btnCancelar2);
            this.pFaltanteSinViaje.Controls.Add(this.groupBox1);
            this.pFaltanteSinViaje.Controls.Add(this.pictureBox2);
            this.pFaltanteSinViaje.Location = new System.Drawing.Point(285, 169);
            this.pFaltanteSinViaje.Name = "pFaltanteSinViaje";
            this.pFaltanteSinViaje.Size = new System.Drawing.Size(536, 289);
            this.pFaltanteSinViaje.TabIndex = 15;
            this.pFaltanteSinViaje.Visible = false;
            this.pFaltanteSinViaje.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pFaltanteSinViaje_MouseMove);
            // 
            // lstClientes
            // 
            this.lstClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstClientes.ForeColor = System.Drawing.Color.Navy;
            this.lstClientes.FullRowSelect = true;
            this.lstClientes.GridLines = true;
            this.lstClientes.Location = new System.Drawing.Point(168, 160);
            this.lstClientes.MultiSelect = false;
            this.lstClientes.Name = "lstClientes";
            this.lstClientes.Size = new System.Drawing.Size(330, 10);
            this.lstClientes.TabIndex = 93;
            this.lstClientes.UseCompatibleStateImageBehavior = false;
            this.lstClientes.View = System.Windows.Forms.View.Details;
            this.lstClientes.Visible = false;
            this.lstClientes.Enter += new System.EventHandler(this.lstClientes_Enter);
            this.lstClientes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstClientes_KeyPress);
            this.lstClientes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstClientes_KeyUp);
            this.lstClientes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstClientes_MouseDoubleClick);
            // 
            // btnModificar
            // 
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnModificar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnModificar.Location = new System.Drawing.Point(282, 233);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(114, 42);
            this.btnModificar.TabIndex = 46;
            this.btnModificar.Text = " Modificar";
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Swis721 Blk BT", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(43, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(446, 29);
            this.label8.TabIndex = 45;
            this.label8.Text = "REGISTRAR FALTANTE SIN VIAJE";
            // 
            // btnAgregar2
            // 
            this.btnAgregar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar2.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAgregar2.Location = new System.Drawing.Point(282, 233);
            this.btnAgregar2.Name = "btnAgregar2";
            this.btnAgregar2.Size = new System.Drawing.Size(114, 42);
            this.btnAgregar2.TabIndex = 42;
            this.btnAgregar2.Text = " Agregar";
            this.btnAgregar2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregar2.UseVisualStyleBackColor = true;
            this.btnAgregar2.Click += new System.EventHandler(this.btnAgregar2_Click);
            // 
            // btnCancelar2
            // 
            this.btnCancelar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancelar2.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.btnCancelar2.Location = new System.Drawing.Point(142, 233);
            this.btnCancelar2.Name = "btnCancelar2";
            this.btnCancelar2.Size = new System.Drawing.Size(114, 42);
            this.btnCancelar2.TabIndex = 43;
            this.btnCancelar2.Text = " Cancelar";
            this.btnCancelar2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar2.UseVisualStyleBackColor = true;
            this.btnCancelar2.Click += new System.EventHandler(this.btnCancelar2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groupBox1.Controls.Add(this.dtpFechaIncidente);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbxMotivo);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 150);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Incidente";
            // 
            // dtpFechaIncidente
            // 
            this.dtpFechaIncidente.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIncidente.Location = new System.Drawing.Point(148, 30);
            this.dtpFechaIncidente.Name = "dtpFechaIncidente";
            this.dtpFechaIncidente.Size = new System.Drawing.Size(140, 22);
            this.dtpFechaIncidente.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(17, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 16);
            this.label13.TabIndex = 40;
            this.label13.Text = "Fecha de Incidente:";
            // 
            // cbxMotivo
            // 
            this.cbxMotivo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxMotivo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMotivo.FormattingEnabled = true;
            this.cbxMotivo.Location = new System.Drawing.Point(148, 109);
            this.cbxMotivo.Name = "cbxMotivo";
            this.cbxMotivo.Size = new System.Drawing.Size(140, 24);
            this.cbxMotivo.TabIndex = 32;
            this.cbxMotivo.SelectedIndexChanged += new System.EventHandler(this.cbxMotivo_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(90, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 16);
            this.label16.TabIndex = 31;
            this.label16.Text = "Cliente:";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(148, 69);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(330, 22);
            this.txtCliente.TabIndex = 5;
            this.txtCliente.Enter += new System.EventHandler(this.txtCliente_Enter);
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            this.txtCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCliente_KeyUp);
            this.txtCliente.Leave += new System.EventHandler(this.txtCliente_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(91, 112);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 16);
            this.label18.TabIndex = 29;
            this.label18.Text = "Motivo:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox2.Location = new System.Drawing.Point(508, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(23, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // frmListaFaltantesMercaderia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1099, 611);
            this.Controls.Add(this.pFaltanteSinViaje);
            this.Controls.Add(this.pFacturarFaltante);
            this.Controls.Add(this.dtgListaFaltantes);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Name = "frmListaFaltantesMercaderia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Faltantes de Viaje";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaFaltantesMercaderia_Load);
            this.Shown += new System.EventHandler(this.frmRegistroFaltantesMercaderia_Shown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaFaltantes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaFaltantesView)).EndInit();
            this.pFacturarFaltante.ResumeLayout(false);
            this.pFacturarFaltante.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pFaltanteSinViaje.ResumeLayout(false);
            this.pFaltanteSinViaje.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtConductor;
        private DevExpress.XtraGrid.GridControl dtgListaFaltantes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem EditarFaltanteToolStripMenuItem;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaFaltantesView;
        private System.Windows.Forms.Panel pFacturarFaltante;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cbxAsume;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem cerrarFaltanteToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker fechaFin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker fechaInicio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbListaPendientes;
        private System.Windows.Forms.RadioButton rbListaSolucionadas;
        private System.Windows.Forms.RadioButton rbListaTodas;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNuevoFaltante;
        private System.Windows.Forms.Label lblNroTicket;
        private System.Windows.Forms.Panel pFaltanteSinViaje;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAgregar2;
        private System.Windows.Forms.Button btnCancelar2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxMotivo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DateTimePicker dtpFechaIncidente;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListView lstClientes;
        private System.Windows.Forms.ToolStripMenuItem ModificarFaltanteToolStripMenuItem;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.RadioButton rbDolares;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbSoles;
    }
}