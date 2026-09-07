namespace ReportesTranspesa.Formularios.Areas.Seguridad.GestionSeguridad
{
    partial class frmAsignarCronograma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarCronograma));
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modificarCronogramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarMetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarCronogramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMeta2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMeta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaCronograma = new System.Windows.Forms.DateTimePicker();
            this.btnCronograma = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.lblActividad = new System.Windows.Forms.Label();
            this.dtgCronogramaActividad = new DevExpress.XtraGrid.GridControl();
            this.dgvCronogramaActividadVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.pAgregarPorcentaje = new System.Windows.Forms.Panel();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.txtNuevaMeta = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFechaMeta = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTipoCronograma = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.contextMenuStrip3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCronogramaActividad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCronogramaActividadVista)).BeginInit();
            this.pAgregarPorcentaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(546, 43);
            this.label1.TabIndex = 16;
            this.label1.Text = "GESTIONAR CRONOGRAMA DE ACTIVIDADES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarCronogramaToolStripMenuItem,
            this.registrarMetaToolStripMenuItem,
            this.eliminarCronogramaToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(202, 70);
            // 
            // modificarCronogramaToolStripMenuItem
            // 
            this.modificarCronogramaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.modificarCronogramaToolStripMenuItem.Name = "modificarCronogramaToolStripMenuItem";
            this.modificarCronogramaToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.modificarCronogramaToolStripMenuItem.Text = "Modificar Cronograma";
            this.modificarCronogramaToolStripMenuItem.Click += new System.EventHandler(this.modificarCronogramaToolStripMenuItem_Click);
            // 
            // registrarMetaToolStripMenuItem
            // 
            this.registrarMetaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cargardatos;
            this.registrarMetaToolStripMenuItem.Name = "registrarMetaToolStripMenuItem";
            this.registrarMetaToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.registrarMetaToolStripMenuItem.Text = "Ingresar Valor Cumplido";
            this.registrarMetaToolStripMenuItem.Click += new System.EventHandler(this.registrarMetaToolStripMenuItem_Click);
            // 
            // eliminarCronogramaToolStripMenuItem
            // 
            this.eliminarCronogramaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarCronogramaToolStripMenuItem.Name = "eliminarCronogramaToolStripMenuItem";
            this.eliminarCronogramaToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.eliminarCronogramaToolStripMenuItem.Text = "Eliminar Cronograma";
            this.eliminarCronogramaToolStripMenuItem.Click += new System.EventHandler(this.eliminarCronogramaToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 166;
            this.label2.Text = "Fecha:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMeta2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMeta);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaCronograma);
            this.groupBox1.Controls.Add(this.btnCronograma);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 105);
            this.groupBox1.TabIndex = 169;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INGRESAR DATOS DE CRONOGRAMA: ";
            // 
            // txtMeta2
            // 
            this.txtMeta2.Location = new System.Drawing.Point(251, 65);
            this.txtMeta2.Name = "txtMeta2";
            this.txtMeta2.Size = new System.Drawing.Size(95, 21);
            this.txtMeta2.TabIndex = 180;
            this.txtMeta2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMeta2_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(195, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 15);
            this.label10.TabIndex = 179;
            this.label10.Text = "M. Acp:";
            // 
            // txtMeta
            // 
            this.txtMeta.Location = new System.Drawing.Point(251, 31);
            this.txtMeta.Name = "txtMeta";
            this.txtMeta.Size = new System.Drawing.Size(95, 21);
            this.txtMeta.TabIndex = 178;
            this.txtMeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMeta_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(195, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 177;
            this.label5.Text = "Meta:";
            // 
            // dtpFechaCronograma
            // 
            this.dtpFechaCronograma.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaCronograma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaCronograma.Location = new System.Drawing.Point(70, 31);
            this.dtpFechaCronograma.Name = "dtpFechaCronograma";
            this.dtpFechaCronograma.Size = new System.Drawing.Size(101, 21);
            this.dtpFechaCronograma.TabIndex = 174;
            this.dtpFechaCronograma.Tag = "1";
            // 
            // btnCronograma
            // 
            this.btnCronograma.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCronograma.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCronograma.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCronograma.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCronograma.Appearance.Options.UseBackColor = true;
            this.btnCronograma.Appearance.Options.UseBorderColor = true;
            this.btnCronograma.Appearance.Options.UseFont = true;
            this.btnCronograma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCronograma.Image = ((System.Drawing.Image)(resources.GetObject("btnCronograma.Image")));
            this.btnCronograma.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCronograma.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCronograma.Location = new System.Drawing.Point(386, 37);
            this.btnCronograma.Name = "btnCronograma";
            this.btnCronograma.Size = new System.Drawing.Size(97, 41);
            this.btnCronograma.TabIndex = 171;
            this.btnCronograma.Text = " Generar";
            this.btnCronograma.Click += new System.EventHandler(this.btnCronograma_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(15, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 21);
            this.label7.TabIndex = 170;
            this.label7.Text = "ACTIVIDAD:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActividad
            // 
            this.lblActividad.AutoSize = true;
            this.lblActividad.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.lblActividad.ForeColor = System.Drawing.Color.Red;
            this.lblActividad.Location = new System.Drawing.Point(130, 56);
            this.lblActividad.Name = "lblActividad";
            this.lblActividad.Size = new System.Drawing.Size(324, 22);
            this.lblActividad.TabIndex = 171;
            this.lblActividad.Text = "PERSONAL RESPONSABLE DE ACTIVIDAD";
            this.lblActividad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgCronogramaActividad
            // 
            this.dtgCronogramaActividad.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dtgCronogramaActividad.ContextMenuStrip = this.contextMenuStrip3;
            this.dtgCronogramaActividad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgCronogramaActividad.Location = new System.Drawing.Point(0, 280);
            this.dtgCronogramaActividad.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgCronogramaActividad.MainView = this.dgvCronogramaActividadVista;
            this.dtgCronogramaActividad.Name = "dtgCronogramaActividad";
            this.dtgCronogramaActividad.Size = new System.Drawing.Size(546, 225);
            this.dtgCronogramaActividad.TabIndex = 172;
            this.dtgCronogramaActividad.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvCronogramaActividadVista});
            this.dtgCronogramaActividad.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgCronogramaActividad_MouseUp);
            // 
            // dgvCronogramaActividadVista
            // 
            this.dgvCronogramaActividadVista.GridControl = this.dtgCronogramaActividad;
            this.dgvCronogramaActividadVista.Name = "dgvCronogramaActividadVista";
            this.dgvCronogramaActividadVista.OptionsBehavior.Editable = false;
            this.dgvCronogramaActividadVista.OptionsView.ColumnAutoWidth = false;
            this.dgvCronogramaActividadVista.OptionsView.RowAutoHeight = true;
            this.dgvCronogramaActividadVista.OptionsView.ShowFooter = true;
            this.dgvCronogramaActividadVista.OptionsView.ShowGroupPanel = false;
            this.dgvCronogramaActividadVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvCronogramaActividadVista_CustomDrawCell);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(186, 244);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(101, 21);
            this.dtpFechaInicio.TabIndex = 175;
            this.dtpFechaInicio.Tag = "1";
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 15);
            this.label3.TabIndex = 176;
            this.label3.Text = "Buscar por Rango de Fecha:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(293, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 15);
            this.label4.TabIndex = 177;
            this.label4.Text = "-";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(311, 245);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(101, 21);
            this.dtpFechaFin.TabIndex = 178;
            this.dtpFechaFin.Tag = "1";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(447, 241);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(56, 29);
            this.btnBuscar.TabIndex = 179;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pAgregarPorcentaje
            // 
            this.pAgregarPorcentaje.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pAgregarPorcentaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pAgregarPorcentaje.Controls.Add(this.btnActualizar);
            this.pAgregarPorcentaje.Controls.Add(this.txtNuevaMeta);
            this.pAgregarPorcentaje.Controls.Add(this.label8);
            this.pAgregarPorcentaje.Controls.Add(this.dtpFechaMeta);
            this.pAgregarPorcentaje.Controls.Add(this.label9);
            this.pAgregarPorcentaje.Controls.Add(this.pictureBox3);
            this.pAgregarPorcentaje.Controls.Add(this.label6);
            this.pAgregarPorcentaje.Location = new System.Drawing.Point(62, 242);
            this.pAgregarPorcentaje.Name = "pAgregarPorcentaje";
            this.pAgregarPorcentaje.Size = new System.Drawing.Size(418, 86);
            this.pAgregarPorcentaje.TabIndex = 180;
            this.pAgregarPorcentaje.Visible = false;
            this.pAgregarPorcentaje.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pAgregarPorcentaje_MouseMove);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnActualizar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnActualizar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Appearance.Options.UseBackColor = true;
            this.btnActualizar.Appearance.Options.UseBorderColor = true;
            this.btnActualizar.Appearance.Options.UseFont = true;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnActualizar.Location = new System.Drawing.Point(348, 37);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(35, 38);
            this.btnActualizar.TabIndex = 183;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // txtNuevaMeta
            // 
            this.txtNuevaMeta.Location = new System.Drawing.Point(268, 47);
            this.txtNuevaMeta.Name = "txtNuevaMeta";
            this.txtNuevaMeta.Size = new System.Drawing.Size(56, 20);
            this.txtNuevaMeta.TabIndex = 182;
            this.txtNuevaMeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNuevaMeta_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(186, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 15);
            this.label8.TabIndex = 181;
            this.label8.Text = "V. Cumplido:";
            // 
            // dtpFechaMeta
            // 
            this.dtpFechaMeta.Enabled = false;
            this.dtpFechaMeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaMeta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaMeta.Location = new System.Drawing.Point(65, 47);
            this.dtpFechaMeta.Name = "dtpFechaMeta";
            this.dtpFechaMeta.Size = new System.Drawing.Size(101, 21);
            this.dtpFechaMeta.TabIndex = 180;
            this.dtpFechaMeta.Tag = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 15);
            this.label9.TabIndex = 179;
            this.label9.Text = "Fecha:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox3.Location = new System.Drawing.Point(390, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 168;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(233, 18);
            this.label6.TabIndex = 167;
            this.label6.Text = "INGRESE VALOR CUMPLIDO";
            // 
            // lblTipoCronograma
            // 
            this.lblTipoCronograma.AutoSize = true;
            this.lblTipoCronograma.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.lblTipoCronograma.ForeColor = System.Drawing.Color.Red;
            this.lblTipoCronograma.Location = new System.Drawing.Point(166, 86);
            this.lblTipoCronograma.Name = "lblTipoCronograma";
            this.lblTipoCronograma.Size = new System.Drawing.Size(188, 22);
            this.lblTipoCronograma.TabIndex = 182;
            this.lblTipoCronograma.Text = "TIPO DE CRONOGRAMA";
            this.lblTipoCronograma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(15, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 21);
            this.label11.TabIndex = 181;
            this.label11.Text = "CRONOGRAMA:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAsignarCronograma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(546, 505);
            this.Controls.Add(this.dtgCronogramaActividad);
            this.Controls.Add(this.lblActividad);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.lblTipoCronograma);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pAgregarPorcentaje);
            this.MaximizeBox = false;
            this.Name = "frmAsignarCronograma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GESTIONAR CRONOGRAMA";
            this.Load += new System.EventHandler(this.frmAsignarResponsables_Load);
            this.contextMenuStrip3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCronogramaActividad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCronogramaActividadVista)).EndInit();
            this.pAgregarPorcentaje.ResumeLayout(false);
            this.pAgregarPorcentaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaCronograma;
        public System.Windows.Forms.Label lblActividad;
        private DevExpress.XtraGrid.GridControl dtgCronogramaActividad;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvCronogramaActividadVista;
        private System.Windows.Forms.ToolStripMenuItem registrarMetaToolStripMenuItem;
        public System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.SimpleButton btnCronograma;
        private System.Windows.Forms.TextBox txtMeta;
        private System.Windows.Forms.ToolStripMenuItem eliminarCronogramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarCronogramaToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Panel pAgregarPorcentaje;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaMeta;
        private System.Windows.Forms.Label label9;
        public DevExpress.XtraEditors.SimpleButton btnActualizar;
        public System.Windows.Forms.Label lblTipoCronograma;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtNuevaMeta;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtMeta2;
    }
}