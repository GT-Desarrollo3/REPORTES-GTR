namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmListaViajesTolvas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaViajesTolvas));
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGenerarPeso = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnGenerarViajes1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.chkAnulados = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNroTicket = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.menuFecha = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnActualizarFecha = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dtpFechaModifica = new System.Windows.Forms.DateTimePicker();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verGuiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarEstadoSUNATToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anularViajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarFechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.envioMasivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarFechaViajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvViajesGuia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnActualizarEstado = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.menuFecha.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajesGuia)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTituloGuia
            // 
            this.lblTituloGuia.BackColor = System.Drawing.Color.LimeGreen;
            this.lblTituloGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloGuia.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTituloGuia.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGuia.Name = "lblTituloGuia";
            this.lblTituloGuia.Size = new System.Drawing.Size(1150, 62);
            this.lblTituloGuia.TabIndex = 3;
            this.lblTituloGuia.Text = "LISTAR  VIAJE  TOLVAS";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnActualizarEstado);
            this.groupBox1.Controls.Add(this.btnGenerarPeso);
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.btnGenerarViajes1);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.chkAnulados);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1150, 110);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // btnGenerarPeso
            // 
            this.btnGenerarPeso.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarPeso.Appearance.Options.UseFont = true;
            this.btnGenerarPeso.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPeso.Image")));
            this.btnGenerarPeso.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGenerarPeso.Location = new System.Drawing.Point(686, 16);
            this.btnGenerarPeso.Name = "btnGenerarPeso";
            this.btnGenerarPeso.Size = new System.Drawing.Size(139, 38);
            this.btnGenerarPeso.TabIndex = 1007;
            this.btnGenerarPeso.Text = "GENERAR PESO";
            this.btnGenerarPeso.Click += new System.EventHandler(this.btnGenerarPeso_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton2.Location = new System.Drawing.Point(939, 28);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(68, 56);
            this.simpleButton2.TabIndex = 1006;
            this.simpleButton2.Text = "Exportar";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton1.Location = new System.Drawing.Point(1017, 28);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(111, 56);
            this.simpleButton1.TabIndex = 1005;
            this.simpleButton1.Text = "Importar Terceros";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnGenerarViajes1
            // 
            this.btnGenerarViajes1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarViajes1.Appearance.Options.UseFont = true;
            this.btnGenerarViajes1.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarViajes1.Image")));
            this.btnGenerarViajes1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGenerarViajes1.Location = new System.Drawing.Point(686, 60);
            this.btnGenerarViajes1.Name = "btnGenerarViajes1";
            this.btnGenerarViajes1.Size = new System.Drawing.Size(139, 38);
            this.btnGenerarViajes1.TabIndex = 1004;
            this.btnGenerarViajes1.Text = "GENERAR VIAJES";
            this.btnGenerarViajes1.Click += new System.EventHandler(this.btnGenerarViajes1_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(543, 27);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 40);
            this.btnBuscar.TabIndex = 1003;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar1_Click);
            // 
            // chkAnulados
            // 
            this.chkAnulados.AutoSize = true;
            this.chkAnulados.Checked = true;
            this.chkAnulados.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAnulados.Location = new System.Drawing.Point(543, 75);
            this.chkAnulados.Name = "chkAnulados";
            this.chkAnulados.Size = new System.Drawing.Size(123, 17);
            this.chkAnulados.TabIndex = 0;
            this.chkAnulados.Text = "Con Viajes Anulados";
            this.chkAnulados.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtConductor);
            this.groupBox4.Location = new System.Drawing.Point(238, 34);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 45);
            this.groupBox4.TabIndex = 99;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buscar Conductor";
            // 
            // txtConductor
            // 
            this.txtConductor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConductor.Location = new System.Drawing.Point(6, 19);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(268, 20);
            this.txtConductor.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPlaca);
            this.groupBox3.Location = new System.Drawing.Point(112, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 45);
            this.groupBox3.TabIndex = 99;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Buscar Placa";
            // 
            // txtPlaca
            // 
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Location = new System.Drawing.Point(6, 19);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(100, 20);
            this.txtPlaca.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNroTicket);
            this.groupBox2.Location = new System.Drawing.Point(12, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(91, 45);
            this.groupBox2.TabIndex = 999;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nro Ticket";
            // 
            // txtNroTicket
            // 
            this.txtNroTicket.Location = new System.Drawing.Point(6, 19);
            this.txtNroTicket.Name = "txtNroTicket";
            this.txtNroTicket.Size = new System.Drawing.Size(74, 20);
            this.txtNroTicket.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.menuFecha);
            this.groupBox5.Controls.Add(this.dtgvData);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 172);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1150, 393);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Viajes";
            // 
            // menuFecha
            // 
            this.menuFecha.Controls.Add(this.btnCerrar);
            this.menuFecha.Controls.Add(this.btnActualizarFecha);
            this.menuFecha.Controls.Add(this.groupBox6);
            this.menuFecha.Location = new System.Drawing.Point(492, 115);
            this.menuFecha.Name = "menuFecha";
            this.menuFecha.Size = new System.Drawing.Size(336, 101);
            this.menuFecha.TabIndex = 25;
            this.menuFecha.Visible = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(307, 1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(29, 23);
            this.btnCerrar.TabIndex = 1008;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnActualizarFecha
            // 
            this.btnActualizarFecha.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarFecha.Appearance.Options.UseFont = true;
            this.btnActualizarFecha.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarFecha.Image")));
            this.btnActualizarFecha.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnActualizarFecha.Location = new System.Drawing.Point(185, 37);
            this.btnActualizarFecha.Name = "btnActualizarFecha";
            this.btnActualizarFecha.Size = new System.Drawing.Size(108, 34);
            this.btnActualizarFecha.TabIndex = 1007;
            this.btnActualizarFecha.Text = "Actualizar";
            this.btnActualizarFecha.Click += new System.EventHandler(this.btnActualizarFecha_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dtpFechaModifica);
            this.groupBox6.Location = new System.Drawing.Point(37, 23);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(129, 59);
            this.groupBox6.TabIndex = 1000;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Modificar Fecha Guia";
            // 
            // dtpFechaModifica
            // 
            this.dtpFechaModifica.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaModifica.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaModifica.Location = new System.Drawing.Point(13, 25);
            this.dtpFechaModifica.MaxDate = new System.DateTime(2800, 12, 31, 0, 0, 0, 0);
            this.dtpFechaModifica.Name = "dtpFechaModifica";
            this.dtpFechaModifica.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaModifica.TabIndex = 0;
            // 
            // dtgvData
            // 
            this.dtgvData.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(3, 16);
            this.dtgvData.LookAndFeel.SkinName = "Office 2007 Green";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dgvViajesGuia;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(1144, 374);
            this.dtgvData.TabIndex = 24;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvViajesGuia});
            this.dtgvData.DoubleClick += new System.EventHandler(this.dtgvData_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verGuiaToolStripMenuItem,
            this.actualizarEstadoSUNATToolStripMenuItem,
            this.anularViajeToolStripMenuItem,
            this.modificarFechaToolStripMenuItem,
            this.envioMasivoToolStripMenuItem,
            this.modificarFechaViajeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 136);
            // 
            // verGuiaToolStripMenuItem
            // 
            this.verGuiaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.mensajero;
            this.verGuiaToolStripMenuItem.Name = "verGuiaToolStripMenuItem";
            this.verGuiaToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.verGuiaToolStripMenuItem.Text = "Ver Guia";
            this.verGuiaToolStripMenuItem.Click += new System.EventHandler(this.verGuiaToolStripMenuItem_Click);
            // 
            // actualizarEstadoSUNATToolStripMenuItem
            // 
            this.actualizarEstadoSUNATToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.recargar;
            this.actualizarEstadoSUNATToolStripMenuItem.Name = "actualizarEstadoSUNATToolStripMenuItem";
            this.actualizarEstadoSUNATToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.actualizarEstadoSUNATToolStripMenuItem.Text = "Actualizar Estado SUNAT";
            this.actualizarEstadoSUNATToolStripMenuItem.Click += new System.EventHandler(this.actualizarEstadoSUNATToolStripMenuItem_Click);
            // 
            // anularViajeToolStripMenuItem
            // 
            this.anularViajeToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.stop;
            this.anularViajeToolStripMenuItem.Name = "anularViajeToolStripMenuItem";
            this.anularViajeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.anularViajeToolStripMenuItem.Text = "Anular Viaje";
            this.anularViajeToolStripMenuItem.Click += new System.EventHandler(this.anularViajeToolStripMenuItem_Click);
            // 
            // modificarFechaToolStripMenuItem
            // 
            this.modificarFechaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.modificarFechaToolStripMenuItem.Name = "modificarFechaToolStripMenuItem";
            this.modificarFechaToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.modificarFechaToolStripMenuItem.Text = "Modificar Fecha Guia";
            this.modificarFechaToolStripMenuItem.Click += new System.EventHandler(this.modificarFechaToolStripMenuItem_Click);
            // 
            // envioMasivoToolStripMenuItem
            // 
            this.envioMasivoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.mover;
            this.envioMasivoToolStripMenuItem.Name = "envioMasivoToolStripMenuItem";
            this.envioMasivoToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.envioMasivoToolStripMenuItem.Text = "Envio Masivo";
            this.envioMasivoToolStripMenuItem.Click += new System.EventHandler(this.envioMasivoToolStripMenuItem_Click);
            // 
            // modificarFechaViajeToolStripMenuItem
            // 
            this.modificarFechaViajeToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.modificarFechaViajeToolStripMenuItem.Name = "modificarFechaViajeToolStripMenuItem";
            this.modificarFechaViajeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.modificarFechaViajeToolStripMenuItem.Text = "Modificar Fecha Viaje";
            this.modificarFechaViajeToolStripMenuItem.Click += new System.EventHandler(this.modificarFechaViajeToolStripMenuItem_Click);
            // 
            // dgvViajesGuia
            // 
            this.dgvViajesGuia.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvViajesGuia.Appearance.FocusedRow.Options.UseFont = true;
            this.dgvViajesGuia.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvViajesGuia.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.dgvViajesGuia.Appearance.SelectedRow.Options.UseFont = true;
            this.dgvViajesGuia.Appearance.SelectedRow.Options.UseForeColor = true;
            this.dgvViajesGuia.GridControl = this.dtgvData;
            this.dgvViajesGuia.Name = "dgvViajesGuia";
            this.dgvViajesGuia.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvViajesGuia.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvViajesGuia.OptionsSelection.MultiSelect = true;
            this.dgvViajesGuia.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvViajesGuia.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.dgvViajesGuia.OptionsView.ColumnAutoWidth = false;
            this.dgvViajesGuia.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.dgvViajesGuia.OptionsView.RowAutoHeight = true;
            this.dgvViajesGuia.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.dgvViajesGuia.OptionsView.ShowFooter = true;
            this.dgvViajesGuia.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvViajesGuia_CustomDrawCell);
            this.dgvViajesGuia.HiddenEditor += new System.EventHandler(this.dgvViajesGuia_HiddenEditor);
            // 
            // btnActualizarEstado
            // 
            this.btnActualizarEstado.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarEstado.Appearance.Options.UseFont = true;
            this.btnActualizarEstado.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarEstado.Image")));
            this.btnActualizarEstado.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnActualizarEstado.Location = new System.Drawing.Point(838, 16);
            this.btnActualizarEstado.Name = "btnActualizarEstado";
            this.btnActualizarEstado.Size = new System.Drawing.Size(88, 82);
            this.btnActualizarEstado.TabIndex = 1008;
            this.btnActualizarEstado.Text = "Actualizar\r\nEstado SUNAT";
            this.btnActualizarEstado.Click += new System.EventHandler(this.btnActualizarEstado_Click);
            // 
            // frmListaViajesTolvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1150, 565);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTituloGuia);
            this.Name = "frmListaViajesTolvas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListaViajesTolvas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaViajesTolvas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.menuFecha.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajesGuia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.TextBox txtConductor;
        public System.Windows.Forms.TextBox txtPlaca;
        public System.Windows.Forms.TextBox txtNroTicket;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem anularViajeToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvViajesGuia;
        private System.Windows.Forms.CheckBox chkAnulados;
        private System.Windows.Forms.ToolStripMenuItem verGuiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualizarEstadoSUNATToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnGenerarViajes1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Panel menuFecha;
        private DevExpress.XtraEditors.SimpleButton btnActualizarFecha;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DateTimePicker dtpFechaModifica;
        private System.Windows.Forms.ToolStripMenuItem modificarFechaToolStripMenuItem;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.ToolStripMenuItem envioMasivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarFechaViajeToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnGenerarPeso;
        private DevExpress.XtraEditors.SimpleButton btnActualizarEstado;
    }
}