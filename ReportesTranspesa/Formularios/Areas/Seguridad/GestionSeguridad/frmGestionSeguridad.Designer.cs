namespace ReportesTranspesa.Formularios.Areas.Seguridad.GestionSeguridad
{
    partial class frmGestionSeguridad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGestionSeguridad));
            this.dtgGestionSeguridad = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.asignarPersonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarCronogramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarGestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvGestionSeguridadVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxTipoCronograma = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rbSSOMAC = new System.Windows.Forms.RadioButton();
            this.rbLider = new System.Windows.Forms.RadioButton();
            this.btCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAsignar = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.txtActividad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnListaActividades = new DevExpress.XtraEditors.SimpleButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBuscaActividad = new System.Windows.Forms.TextBox();
            this.lstPersonal = new System.Windows.Forms.ListView();
            this.lstActividad = new System.Windows.Forms.ListView();
            this.pAsignarResponsable = new System.Windows.Forms.Panel();
            this.lblGestion = new System.Windows.Forms.Label();
            this.lblActividad = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btAsignar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPesoPersonal = new System.Windows.Forms.TextBox();
            this.txtPersonalNombre = new System.Windows.Forms.TextBox();
            this.metroLabel15 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.metroLabel17 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel18 = new MetroFramework.Controls.MetroLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lstPersonal2 = new System.Windows.Forms.ListView();
            this.dtgvListaResponsables = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cambiarPesoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desvincularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvListaResponsablesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGestionSeguridad)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionSeguridadVista)).BeginInit();
            this.gbFiltros.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pAsignarResponsable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaResponsables)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaResponsablesView)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgGestionSeguridad
            // 
            this.dtgGestionSeguridad.CausesValidation = false;
            this.dtgGestionSeguridad.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgGestionSeguridad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgGestionSeguridad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgGestionSeguridad.Location = new System.Drawing.Point(20, 333);
            this.dtgGestionSeguridad.MainView = this.dgvGestionSeguridadVista;
            this.dtgGestionSeguridad.Name = "dtgGestionSeguridad";
            this.dtgGestionSeguridad.Size = new System.Drawing.Size(1181, 262);
            this.dtgGestionSeguridad.TabIndex = 104;
            this.dtgGestionSeguridad.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvGestionSeguridadVista});
            this.dtgGestionSeguridad.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgGestionSeguridad_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asignarPersonalToolStripMenuItem,
            this.gestionarCronogramaToolStripMenuItem,
            this.eliminarGestionToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(253, 70);
            // 
            // asignarPersonalToolStripMenuItem
            // 
            this.asignarPersonalToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.team;
            this.asignarPersonalToolStripMenuItem.Name = "asignarPersonalToolStripMenuItem";
            this.asignarPersonalToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.asignarPersonalToolStripMenuItem.Text = "Asignar Responsable de Actividad";
            this.asignarPersonalToolStripMenuItem.Click += new System.EventHandler(this.asignarPersonalToolStripMenuItem_Click);
            // 
            // gestionarCronogramaToolStripMenuItem
            // 
            this.gestionarCronogramaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.gestionarCronogramaToolStripMenuItem.Name = "gestionarCronogramaToolStripMenuItem";
            this.gestionarCronogramaToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.gestionarCronogramaToolStripMenuItem.Text = "Gestionar Cronograma";
            this.gestionarCronogramaToolStripMenuItem.Click += new System.EventHandler(this.gestionarCronogramaToolStripMenuItem_Click);
            // 
            // eliminarGestionToolStripMenuItem
            // 
            this.eliminarGestionToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarGestionToolStripMenuItem.Name = "eliminarGestionToolStripMenuItem";
            this.eliminarGestionToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.eliminarGestionToolStripMenuItem.Text = "Eliminar Gestión";
            this.eliminarGestionToolStripMenuItem.Click += new System.EventHandler(this.eliminarGestionToolStripMenuItem_Click);
            // 
            // dgvGestionSeguridadVista
            // 
            this.dgvGestionSeguridadVista.GridControl = this.dtgGestionSeguridad;
            this.dgvGestionSeguridadVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvGestionSeguridadVista.Name = "dgvGestionSeguridadVista";
            this.dgvGestionSeguridadVista.OptionsBehavior.Editable = false;
            this.dgvGestionSeguridadVista.OptionsBehavior.ReadOnly = true;
            this.dgvGestionSeguridadVista.OptionsView.ColumnAutoWidth = false;
            this.dgvGestionSeguridadVista.OptionsView.RowAutoHeight = true;
            this.dgvGestionSeguridadVista.OptionsView.ShowFooter = true;
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.groupBox3);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbFiltros.Location = new System.Drawing.Point(20, 60);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(1181, 202);
            this.gbFiltros.TabIndex = 105;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Crear Nueva Gestión";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbxTipoCronograma);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.rbSSOMAC);
            this.groupBox3.Controls.Add(this.rbLider);
            this.groupBox3.Controls.Add(this.btCancelar);
            this.groupBox3.Controls.Add(this.btnAsignar);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtPeso);
            this.groupBox3.Controls.Add(this.txtCargo);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtArea);
            this.groupBox3.Controls.Add(this.txtActividad);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtPersonal);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(28, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1126, 152);
            this.groupBox3.TabIndex = 115;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ingresar Datos de Gestión: ";
            // 
            // cbxTipoCronograma
            // 
            this.cbxTipoCronograma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoCronograma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoCronograma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoCronograma.FormattingEnabled = true;
            this.cbxTipoCronograma.Items.AddRange(new object[] {
            "DIARIA",
            "AVANCE",
            "META TOTAL"});
            this.cbxTipoCronograma.Location = new System.Drawing.Point(727, 110);
            this.cbxTipoCronograma.Name = "cbxTipoCronograma";
            this.cbxTipoCronograma.Size = new System.Drawing.Size(129, 21);
            this.cbxTipoCronograma.TabIndex = 179;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(724, 90);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 13);
            this.label12.TabIndex = 173;
            this.label12.Text = "Tipo de Cronograma:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(495, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 172;
            this.label6.Text = "Validación de Gestión:";
            // 
            // rbSSOMAC
            // 
            this.rbSSOMAC.AutoSize = true;
            this.rbSSOMAC.Location = new System.Drawing.Point(623, 111);
            this.rbSSOMAC.Name = "rbSSOMAC";
            this.rbSSOMAC.Size = new System.Drawing.Size(70, 17);
            this.rbSSOMAC.TabIndex = 171;
            this.rbSSOMAC.TabStop = true;
            this.rbSSOMAC.Text = "SSOMAC";
            this.rbSSOMAC.UseVisualStyleBackColor = true;
            this.rbSSOMAC.CheckedChanged += new System.EventHandler(this.rbSSOMAC_CheckedChanged);
            // 
            // rbLider
            // 
            this.rbLider.AutoSize = true;
            this.rbLider.Location = new System.Drawing.Point(498, 111);
            this.rbLider.Name = "rbLider";
            this.rbLider.Size = new System.Drawing.Size(112, 17);
            this.rbLider.TabIndex = 170;
            this.rbLider.TabStop = true;
            this.rbLider.Text = "Líder de Actividad";
            this.rbLider.UseVisualStyleBackColor = true;
            this.rbLider.CheckedChanged += new System.EventHandler(this.rbLider_CheckedChanged);
            // 
            // btCancelar
            // 
            this.btCancelar.Appearance.BackColor = System.Drawing.Color.White;
            this.btCancelar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancelar.Appearance.Options.UseBackColor = true;
            this.btCancelar.Appearance.Options.UseBorderColor = true;
            this.btCancelar.Appearance.Options.UseFont = true;
            this.btCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btCancelar.Image")));
            this.btCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btCancelar.Location = new System.Drawing.Point(1009, 92);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(94, 39);
            this.btCancelar.TabIndex = 166;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.ToolTip = "Cancelar";
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAsignar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignar.Appearance.Options.UseBackColor = true;
            this.btnAsignar.Appearance.Options.UseBorderColor = true;
            this.btnAsignar.Appearance.Options.UseFont = true;
            this.btnAsignar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignar.Image")));
            this.btnAsignar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAsignar.Location = new System.Drawing.Point(895, 92);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(94, 39);
            this.btnAsignar.TabIndex = 165;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.ToolTip = "Asignar";
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(724, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 117;
            this.label3.Text = "Cargo:";
            // 
            // txtPeso
            // 
            this.txtPeso.Location = new System.Drawing.Point(393, 111);
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(66, 20);
            this.txtPeso.TabIndex = 116;
            this.txtPeso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPeso_KeyPress);
            // 
            // txtCargo
            // 
            this.txtCargo.Location = new System.Drawing.Point(727, 49);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.ReadOnly = true;
            this.txtCargo.Size = new System.Drawing.Size(300, 20);
            this.txtCargo.TabIndex = 116;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(390, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 115;
            this.label5.Text = "P. Responsable:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 115;
            this.label2.Text = "Área:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 114;
            this.label4.Text = "Actividad:";
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(393, 49);
            this.txtArea.Name = "txtArea";
            this.txtArea.ReadOnly = true;
            this.txtArea.Size = new System.Drawing.Size(300, 20);
            this.txtArea.TabIndex = 114;
            // 
            // txtActividad
            // 
            this.txtActividad.Location = new System.Drawing.Point(21, 111);
            this.txtActividad.Name = "txtActividad";
            this.txtActividad.Size = new System.Drawing.Size(337, 20);
            this.txtActividad.TabIndex = 112;
            this.txtActividad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtActividad_KeyPress);
            this.txtActividad.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtActividad_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "Ingresar Líder de la Actividad:";
            // 
            // txtPersonal
            // 
            this.txtPersonal.Location = new System.Drawing.Point(21, 49);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(337, 20);
            this.txtPersonal.TabIndex = 112;
            this.txtPersonal.Enter += new System.EventHandler(this.txtPersonal_Enter);
            this.txtPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonal_KeyPress);
            this.txtPersonal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPersonal_KeyUp);
            this.txtPersonal.Leave += new System.EventHandler(this.txtPersonal_Leave);
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
            this.btnExcel.Location = new System.Drawing.Point(1041, 11);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 7;
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
            this.btnBuscar.Location = new System.Drawing.Point(975, 11);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(51, 47);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.dtpFechaIni);
            this.panel2.Controls.Add(this.dtpFechaFin);
            this.panel2.Controls.Add(this.cbxEstado);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.btnListaActividades);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtBuscaActividad);
            this.panel2.Controls.Add(this.btnExcel);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 262);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1181, 71);
            this.panel2.TabIndex = 131;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(527, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 15);
            this.label14.TabIndex = 182;
            this.label14.Text = "Fecha:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(621, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 181;
            this.label13.Text = "--";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(530, 33);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(85, 22);
            this.dtpFechaIni.TabIndex = 179;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(642, 33);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(85, 22);
            this.dtpFechaFin.TabIndex = 180;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "NO COMPLETADO",
            "COMPLETADO"});
            this.cbxEstado.Location = new System.Drawing.Point(761, 34);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(170, 21);
            this.cbxEstado.TabIndex = 178;
            this.cbxEstado.SelectedIndexChanged += new System.EventHandler(this.cbxEstado_SelectedIndexChanged);
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(761, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 15);
            this.label11.TabIndex = 177;
            this.label11.Text = "Área:";
            // 
            // btnListaActividades
            // 
            this.btnListaActividades.Appearance.BackColor = System.Drawing.Color.White;
            this.btnListaActividades.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnListaActividades.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnListaActividades.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListaActividades.Appearance.Options.UseBackColor = true;
            this.btnListaActividades.Appearance.Options.UseBorderColor = true;
            this.btnListaActividades.Appearance.Options.UseFont = true;
            this.btnListaActividades.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnListaActividades.Image = ((System.Drawing.Image)(resources.GetObject("btnListaActividades.Image")));
            this.btnListaActividades.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnListaActividades.Location = new System.Drawing.Point(28, 11);
            this.btnListaActividades.Name = "btnListaActividades";
            this.btnListaActividades.Size = new System.Drawing.Size(124, 47);
            this.btnListaActividades.TabIndex = 166;
            this.btnListaActividades.Text = "Objetivos y\r\nActividades";
            this.btnListaActividades.ToolTip = "Lista de Objetivos y Actividades";
            this.btnListaActividades.Click += new System.EventHandler(this.btnListaActividades_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(191, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 13);
            this.label10.TabIndex = 116;
            this.label10.Text = "Buscar por Actividad:";
            // 
            // txtBuscaActividad
            // 
            this.txtBuscaActividad.Location = new System.Drawing.Point(194, 34);
            this.txtBuscaActividad.Name = "txtBuscaActividad";
            this.txtBuscaActividad.Size = new System.Drawing.Size(301, 20);
            this.txtBuscaActividad.TabIndex = 115;
            this.txtBuscaActividad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscaActividad_KeyPress);
            // 
            // lstPersonal
            // 
            this.lstPersonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstPersonal.ForeColor = System.Drawing.Color.Navy;
            this.lstPersonal.FullRowSelect = true;
            this.lstPersonal.GridLines = true;
            this.lstPersonal.Location = new System.Drawing.Point(69, 160);
            this.lstPersonal.MultiSelect = false;
            this.lstPersonal.Name = "lstPersonal";
            this.lstPersonal.Size = new System.Drawing.Size(337, 127);
            this.lstPersonal.TabIndex = 129;
            this.lstPersonal.UseCompatibleStateImageBehavior = false;
            this.lstPersonal.View = System.Windows.Forms.View.Details;
            this.lstPersonal.Visible = false;
            this.lstPersonal.Enter += new System.EventHandler(this.lstPersonal_Enter);
            this.lstPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersonal_KeyPress);
            this.lstPersonal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersonal_MouseDoubleClick);
            // 
            // lstActividad
            // 
            this.lstActividad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstActividad.ForeColor = System.Drawing.Color.Navy;
            this.lstActividad.FullRowSelect = true;
            this.lstActividad.GridLines = true;
            this.lstActividad.Location = new System.Drawing.Point(69, 222);
            this.lstActividad.MultiSelect = false;
            this.lstActividad.Name = "lstActividad";
            this.lstActividad.Size = new System.Drawing.Size(337, 111);
            this.lstActividad.TabIndex = 132;
            this.lstActividad.UseCompatibleStateImageBehavior = false;
            this.lstActividad.View = System.Windows.Forms.View.Details;
            this.lstActividad.Visible = false;
            this.lstActividad.Enter += new System.EventHandler(this.lstActividad_Enter);
            this.lstActividad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstActividad_KeyPress);
            this.lstActividad.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstActividad_MouseDoubleClick);
            // 
            // pAsignarResponsable
            // 
            this.pAsignarResponsable.BackColor = System.Drawing.Color.LemonChiffon;
            this.pAsignarResponsable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pAsignarResponsable.Controls.Add(this.lblGestion);
            this.pAsignarResponsable.Controls.Add(this.lblActividad);
            this.pAsignarResponsable.Controls.Add(this.label9);
            this.pAsignarResponsable.Controls.Add(this.btAsignar);
            this.pAsignarResponsable.Controls.Add(this.txtPesoPersonal);
            this.pAsignarResponsable.Controls.Add(this.txtPersonalNombre);
            this.pAsignarResponsable.Controls.Add(this.metroLabel15);
            this.pAsignarResponsable.Controls.Add(this.pictureBox2);
            this.pAsignarResponsable.Controls.Add(this.metroLabel17);
            this.pAsignarResponsable.Controls.Add(this.metroLabel18);
            this.pAsignarResponsable.Controls.Add(this.label7);
            this.pAsignarResponsable.Controls.Add(this.label8);
            this.pAsignarResponsable.Controls.Add(this.lstPersonal2);
            this.pAsignarResponsable.Controls.Add(this.dtgvListaResponsables);
            this.pAsignarResponsable.Location = new System.Drawing.Point(521, 202);
            this.pAsignarResponsable.Name = "pAsignarResponsable";
            this.pAsignarResponsable.Size = new System.Drawing.Size(690, 406);
            this.pAsignarResponsable.TabIndex = 133;
            this.pAsignarResponsable.Visible = false;
            this.pAsignarResponsable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pAsignarResponsable_MouseMove);
            // 
            // lblGestion
            // 
            this.lblGestion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGestion.AutoSize = true;
            this.lblGestion.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGestion.Location = new System.Drawing.Point(473, 15);
            this.lblGestion.Name = "lblGestion";
            this.lblGestion.Size = new System.Drawing.Size(21, 22);
            this.lblGestion.TabIndex = 138;
            this.lblGestion.Text = "#";
            this.lblGestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGestion.Visible = false;
            // 
            // lblActividad
            // 
            this.lblActividad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblActividad.AutoSize = true;
            this.lblActividad.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActividad.ForeColor = System.Drawing.Color.Red;
            this.lblActividad.Location = new System.Drawing.Point(26, 48);
            this.lblActividad.Name = "lblActividad";
            this.lblActividad.Size = new System.Drawing.Size(327, 23);
            this.lblActividad.TabIndex = 137;
            this.lblActividad.Text = "PERSONAL RESPONSABLE DE ACTIVIDAD";
            this.lblActividad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(45, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 16);
            this.label9.TabIndex = 134;
            this.label9.Text = "Peso:";
            // 
            // btAsignar
            // 
            this.btAsignar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btAsignar.Appearance.BackColor = System.Drawing.Color.White;
            this.btAsignar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btAsignar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btAsignar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAsignar.Appearance.Options.UseBackColor = true;
            this.btAsignar.Appearance.Options.UseBorderColor = true;
            this.btAsignar.Appearance.Options.UseFont = true;
            this.btAsignar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btAsignar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btAsignar.Image")));
            this.btAsignar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btAsignar.Location = new System.Drawing.Point(465, 98);
            this.btAsignar.Name = "btAsignar";
            this.btAsignar.Size = new System.Drawing.Size(38, 40);
            this.btAsignar.TabIndex = 131;
            this.btAsignar.Tag = "5";
            this.btAsignar.ToolTip = "Registrar";
            this.btAsignar.Click += new System.EventHandler(this.btAsignar_Click);
            // 
            // txtPesoPersonal
            // 
            this.txtPesoPersonal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPesoPersonal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPesoPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPesoPersonal.Location = new System.Drawing.Point(94, 129);
            this.txtPesoPersonal.Name = "txtPesoPersonal";
            this.txtPesoPersonal.Size = new System.Drawing.Size(71, 20);
            this.txtPesoPersonal.TabIndex = 129;
            this.txtPesoPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPesoPersonal_KeyPress);
            // 
            // txtPersonalNombre
            // 
            this.txtPersonalNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPersonalNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPersonalNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPersonalNombre.Location = new System.Drawing.Point(94, 88);
            this.txtPersonalNombre.Name = "txtPersonalNombre";
            this.txtPersonalNombre.Size = new System.Drawing.Size(343, 20);
            this.txtPersonalNombre.TabIndex = 126;
            this.txtPersonalNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonalNombre_KeyPress);
            this.txtPersonalNombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPersonalNombre_KeyUp);
            // 
            // metroLabel15
            // 
            this.metroLabel15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel15.AutoSize = true;
            this.metroLabel15.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel15.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel15.Location = new System.Drawing.Point(19, 49);
            this.metroLabel15.Name = "metroLabel15";
            this.metroLabel15.Size = new System.Drawing.Size(0, 0);
            this.metroLabel15.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel15.TabIndex = 110;
            this.metroLabel15.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox2.Location = new System.Drawing.Point(656, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 107;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // metroLabel17
            // 
            this.metroLabel17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel17.AutoSize = true;
            this.metroLabel17.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel17.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel17.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel17.Location = new System.Drawing.Point(141, 49);
            this.metroLabel17.Name = "metroLabel17";
            this.metroLabel17.Size = new System.Drawing.Size(0, 0);
            this.metroLabel17.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel17.TabIndex = 106;
            this.metroLabel17.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel18
            // 
            this.metroLabel18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel18.AutoSize = true;
            this.metroLabel18.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel18.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel18.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel18.Location = new System.Drawing.Point(109, 18);
            this.metroLabel18.Name = "metroLabel18";
            this.metroLabel18.Size = new System.Drawing.Size(0, 0);
            this.metroLabel18.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel18.TabIndex = 104;
            this.metroLabel18.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(441, 22);
            this.label7.TabIndex = 132;
            this.label7.Text = "PERSONAL RESPONSABLE DE LA ACTIVIDAD:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(28, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 16);
            this.label8.TabIndex = 133;
            this.label8.Text = "Nombre:";
            // 
            // lstPersonal2
            // 
            this.lstPersonal2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstPersonal2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstPersonal2.ForeColor = System.Drawing.Color.Navy;
            this.lstPersonal2.FullRowSelect = true;
            this.lstPersonal2.GridLines = true;
            this.lstPersonal2.Location = new System.Drawing.Point(94, 107);
            this.lstPersonal2.MultiSelect = false;
            this.lstPersonal2.Name = "lstPersonal2";
            this.lstPersonal2.Size = new System.Drawing.Size(343, 127);
            this.lstPersonal2.TabIndex = 136;
            this.lstPersonal2.UseCompatibleStateImageBehavior = false;
            this.lstPersonal2.View = System.Windows.Forms.View.Details;
            this.lstPersonal2.Visible = false;
            this.lstPersonal2.Enter += new System.EventHandler(this.lstPersonal2_Enter);
            this.lstPersonal2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersonal2_KeyPress);
            this.lstPersonal2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersonal2_MouseDoubleClick);
            // 
            // dtgvListaResponsables
            // 
            this.dtgvListaResponsables.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtgvListaResponsables.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgvListaResponsables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvListaResponsables.Location = new System.Drawing.Point(26, 173);
            this.dtgvListaResponsables.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgvListaResponsables.MainView = this.dtgvListaResponsablesView;
            this.dtgvListaResponsables.Name = "dtgvListaResponsables";
            this.dtgvListaResponsables.Size = new System.Drawing.Size(640, 216);
            this.dtgvListaResponsables.TabIndex = 135;
            this.dtgvListaResponsables.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvListaResponsablesView});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarPesoToolStripMenuItem,
            this.desvincularToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(148, 48);
            // 
            // cambiarPesoToolStripMenuItem
            // 
            this.cambiarPesoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.cambiarPesoToolStripMenuItem.Name = "cambiarPesoToolStripMenuItem";
            this.cambiarPesoToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.cambiarPesoToolStripMenuItem.Text = "Cambiar Peso";
            this.cambiarPesoToolStripMenuItem.Click += new System.EventHandler(this.cambiarPesoToolStripMenuItem_Click);
            // 
            // desvincularToolStripMenuItem
            // 
            this.desvincularToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.desvincularToolStripMenuItem.Name = "desvincularToolStripMenuItem";
            this.desvincularToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.desvincularToolStripMenuItem.Text = "Desvincular";
            this.desvincularToolStripMenuItem.Click += new System.EventHandler(this.desvincularToolStripMenuItem_Click);
            // 
            // dtgvListaResponsablesView
            // 
            this.dtgvListaResponsablesView.GridControl = this.dtgvListaResponsables;
            this.dtgvListaResponsablesView.Name = "dtgvListaResponsablesView";
            this.dtgvListaResponsablesView.OptionsBehavior.Editable = false;
            this.dtgvListaResponsablesView.OptionsView.ColumnAutoWidth = false;
            this.dtgvListaResponsablesView.OptionsView.RowAutoHeight = true;
            this.dtgvListaResponsablesView.OptionsView.ShowFooter = true;
            this.dtgvListaResponsablesView.OptionsView.ShowGroupPanel = false;
            // 
            // frmGestionSeguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 615);
            this.Controls.Add(this.dtgGestionSeguridad);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbFiltros);
            this.Controls.Add(this.lstPersonal);
            this.Controls.Add(this.lstActividad);
            this.Controls.Add(this.pAsignarResponsable);
            this.Name = "frmGestionSeguridad";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "GESTIÓN DE SEGURIDAD";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGestionSeguridad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgGestionSeguridad)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionSeguridadVista)).EndInit();
            this.gbFiltros.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pAsignarResponsable.ResumeLayout(false);
            this.pAsignarResponsable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaResponsables)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaResponsablesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgGestionSeguridad;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvGestionSeguridadVista;
        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.TextBox txtActividad;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstPersonal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lstActividad;
        public DevExpress.XtraEditors.SimpleButton btnAsignar;
        public DevExpress.XtraEditors.SimpleButton btCancelar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbSSOMAC;
        private System.Windows.Forms.RadioButton rbLider;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem asignarPersonalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarCronogramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarGestionToolStripMenuItem;
        private System.Windows.Forms.Panel pAsignarResponsable;
        internal System.Windows.Forms.TextBox txtPesoPersonal;
        internal System.Windows.Forms.TextBox txtPersonalNombre;
        private MetroFramework.Controls.MetroLabel metroLabel15;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MetroFramework.Controls.MetroLabel metroLabel17;
        private MetroFramework.Controls.MetroLabel metroLabel18;
        private DevExpress.XtraEditors.SimpleButton btAsignar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem cambiarPesoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desvincularToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl dtgvListaResponsables;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvListaResponsablesView;
        private System.Windows.Forms.ListView lstPersonal2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBuscaActividad;
        public DevExpress.XtraEditors.SimpleButton btnListaActividades;
        private System.Windows.Forms.Label lblActividad;
        public System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox cbxTipoCronograma;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblGestion;
    }
}