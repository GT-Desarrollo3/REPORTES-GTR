namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    partial class frmMecanicosAsignados
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblCodigoOT = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTurno = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnHorasExtra = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgListaAsignaciones = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.terminarOTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pausarTrabajoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitarMecanicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaAsignacionesVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pMotivoPausa = new System.Windows.Forms.Panel();
            this.btnAgregarMotivo = new System.Windows.Forms.Button();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtNombrePausa = new System.Windows.Forms.TextBox();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaAsignaciones)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaAsignacionesVista)).BeginInit();
            this.pMotivoPausa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 18.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1161, 51);
            this.label1.TabIndex = 184;
            this.label1.Text = "ASIGNACIÓN DE MECÁNICOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.lblCodigoOT);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblDescripcion);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.lblPlaca);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F);
            this.panel3.Location = new System.Drawing.Point(0, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1161, 236);
            this.panel3.TabIndex = 185;
            // 
            // lblCodigoOT
            // 
            this.lblCodigoOT.AutoSize = true;
            this.lblCodigoOT.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold);
            this.lblCodigoOT.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCodigoOT.Location = new System.Drawing.Point(357, 55);
            this.lblCodigoOT.Name = "lblCodigoOT";
            this.lblCodigoOT.Size = new System.Drawing.Size(95, 29);
            this.lblCodigoOT.TabIndex = 215;
            this.lblCodigoOT.Text = "AAA-999";
            this.lblCodigoOT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(247, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 29);
            this.label4.TabIndex = 214;
            this.label4.Text = "CÓDIGO:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDescripcion.Location = new System.Drawing.Point(64, 18);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(416, 29);
            this.lblDescripcion.TabIndex = 211;
            this.lblDescripcion.Text = "PERSONAL RESPONSABLE DE ACTIVIDAD";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(20, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 29);
            this.label7.TabIndex = 210;
            this.label7.Text = "OT:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold);
            this.lblPlaca.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblPlaca.Location = new System.Drawing.Point(116, 55);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(95, 29);
            this.lblPlaca.TabIndex = 213;
            this.lblPlaca.Text = "AAA-999";
            this.lblPlaca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(20, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 29);
            this.label11.TabIndex = 212;
            this.label11.Text = "PLACA:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTurno);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnHorasExtra);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnRegistrar);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(24, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1114, 117);
            this.groupBox1.TabIndex = 216;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Mecánico";
            // 
            // txtTurno
            // 
            this.txtTurno.Location = new System.Drawing.Point(718, 67);
            this.txtTurno.Name = "txtTurno";
            this.txtTurno.ReadOnly = true;
            this.txtTurno.Size = new System.Drawing.Size(110, 29);
            this.txtTurno.TabIndex = 213;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F);
            this.label8.Location = new System.Drawing.Point(713, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 25);
            this.label8.TabIndex = 214;
            this.label8.Text = "Turno:";
            // 
            // btnHorasExtra
            // 
            this.btnHorasExtra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHorasExtra.BackColor = System.Drawing.Color.Yellow;
            this.btnHorasExtra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHorasExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHorasExtra.ForeColor = System.Drawing.Color.Red;
            this.btnHorasExtra.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.btnHorasExtra.Location = new System.Drawing.Point(992, 38);
            this.btnHorasExtra.Name = "btnHorasExtra";
            this.btnHorasExtra.Size = new System.Drawing.Size(101, 59);
            this.btnHorasExtra.TabIndex = 212;
            this.btnHorasExtra.Text = " Asignar\r\n Tiempo Extra";
            this.btnHorasExtra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHorasExtra.UseVisualStyleBackColor = false;
            this.btnHorasExtra.Click += new System.EventHandler(this.btnHorasExtra_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(155, 67);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(540, 29);
            this.txtNombre.TabIndex = 210;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F);
            this.label5.Location = new System.Drawing.Point(150, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 25);
            this.label5.TabIndex = 211;
            this.label5.Text = "Nombre:";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrar.BackColor = System.Drawing.Color.GreenYellow;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegistrar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnRegistrar.Location = new System.Drawing.Point(873, 38);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(101, 59);
            this.btnRegistrar.TabIndex = 209;
            this.btnRegistrar.Text = " Asignar\r\n OT";
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F, System.Drawing.FontStyle.Bold);
            this.txtCodigo.Location = new System.Drawing.Point(20, 67);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(111, 29);
            this.txtCodigo.TabIndex = 204;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F);
            this.label3.Location = new System.Drawing.Point(15, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 25);
            this.label3.TabIndex = 208;
            this.label3.Text = "Código:";
            // 
            // dtgListaAsignaciones
            // 
            this.dtgListaAsignaciones.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaAsignaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaAsignaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaAsignaciones.Location = new System.Drawing.Point(0, 287);
            this.dtgListaAsignaciones.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgListaAsignaciones.MainView = this.dgvListaAsignacionesVista;
            this.dtgListaAsignaciones.Name = "dtgListaAsignaciones";
            this.dtgListaAsignaciones.Size = new System.Drawing.Size(1161, 237);
            this.dtgListaAsignaciones.TabIndex = 186;
            this.dtgListaAsignaciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaAsignacionesVista});
            this.dtgListaAsignaciones.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaAsignaciones_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminarOTToolStripMenuItem,
            this.pausarTrabajoToolStripMenuItem,
            this.quitarMecanicoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip3";
            this.contextMenuStrip1.Size = new System.Drawing.Size(226, 94);
            // 
            // terminarOTToolStripMenuItem
            // 
            this.terminarOTToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.terminarOTToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.bonoseguridad;
            this.terminarOTToolStripMenuItem.Name = "terminarOTToolStripMenuItem";
            this.terminarOTToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.terminarOTToolStripMenuItem.Text = "Terminar Trabajo";
            this.terminarOTToolStripMenuItem.Click += new System.EventHandler(this.terminarOTToolStripMenuItem_Click);
            // 
            // pausarTrabajoToolStripMenuItem
            // 
            this.pausarTrabajoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.excepciones;
            this.pausarTrabajoToolStripMenuItem.Name = "pausarTrabajoToolStripMenuItem";
            this.pausarTrabajoToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.pausarTrabajoToolStripMenuItem.Text = "Pausar Trabajo";
            this.pausarTrabajoToolStripMenuItem.Click += new System.EventHandler(this.pausarTrabajoToolStripMenuItem_Click);
            // 
            // quitarMecanicoToolStripMenuItem
            // 
            this.quitarMecanicoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.quitarMecanicoToolStripMenuItem.Name = "quitarMecanicoToolStripMenuItem";
            this.quitarMecanicoToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.quitarMecanicoToolStripMenuItem.Text = "Quitar Mecánico";
            this.quitarMecanicoToolStripMenuItem.Click += new System.EventHandler(this.quitarMecanicoToolStripMenuItem_Click);
            // 
            // dgvListaAsignacionesVista
            // 
            this.dgvListaAsignacionesVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 14.5F);
            this.dgvListaAsignacionesVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvListaAsignacionesVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 14.5F);
            this.dgvListaAsignacionesVista.Appearance.Row.Options.UseFont = true;
            this.dgvListaAsignacionesVista.GridControl = this.dtgListaAsignaciones;
            this.dgvListaAsignacionesVista.Name = "dgvListaAsignacionesVista";
            this.dgvListaAsignacionesVista.OptionsBehavior.Editable = false;
            this.dgvListaAsignacionesVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaAsignacionesVista.OptionsView.RowAutoHeight = true;
            this.dgvListaAsignacionesVista.OptionsView.ShowGroupPanel = false;
            // 
            // pMotivoPausa
            // 
            this.pMotivoPausa.BackColor = System.Drawing.Color.LemonChiffon;
            this.pMotivoPausa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMotivoPausa.Controls.Add(this.btnAgregarMotivo);
            this.pMotivoPausa.Controls.Add(this.txtMotivo);
            this.pMotivoPausa.Controls.Add(this.label2);
            this.pMotivoPausa.Controls.Add(this.label59);
            this.pMotivoPausa.Controls.Add(this.label6);
            this.pMotivoPausa.Controls.Add(this.pictureBox1);
            this.pMotivoPausa.Controls.Add(this.txtNombrePausa);
            this.pMotivoPausa.Location = new System.Drawing.Point(176, 114);
            this.pMotivoPausa.Name = "pMotivoPausa";
            this.pMotivoPausa.Size = new System.Drawing.Size(684, 307);
            this.pMotivoPausa.TabIndex = 219;
            this.pMotivoPausa.Visible = false;
            this.pMotivoPausa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pMotivoPausa_MouseMove);
            // 
            // btnAgregarMotivo
            // 
            this.btnAgregarMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarMotivo.BackColor = System.Drawing.Color.GreenYellow;
            this.btnAgregarMotivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarMotivo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAgregarMotivo.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAgregarMotivo.Location = new System.Drawing.Point(289, 246);
            this.btnAgregarMotivo.Name = "btnAgregarMotivo";
            this.btnAgregarMotivo.Size = new System.Drawing.Size(105, 40);
            this.btnAgregarMotivo.TabIndex = 215;
            this.btnAgregarMotivo.Text = " Guardar";
            this.btnAgregarMotivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarMotivo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarMotivo.UseVisualStyleBackColor = false;
            this.btnAgregarMotivo.Click += new System.EventHandler(this.btnAgregarMotivo_Click);
            // 
            // txtMotivo
            // 
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.Location = new System.Drawing.Point(113, 120);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(539, 107);
            this.txtMotivo.TabIndex = 214;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F);
            this.label2.Location = new System.Drawing.Point(31, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 25);
            this.label2.TabIndex = 213;
            this.label2.Text = "Motivo:";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.5F, System.Drawing.FontStyle.Bold);
            this.label59.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label59.Location = new System.Drawing.Point(16, 15);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(449, 29);
            this.label59.TabIndex = 49;
            this.label59.Text = "PAUSAR TRABAJO DE MECÁNICO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F);
            this.label6.Location = new System.Drawing.Point(20, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 25);
            this.label6.TabIndex = 212;
            this.label6.Text = "Nombre:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(648, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtNombrePausa
            // 
            this.txtNombrePausa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F);
            this.txtNombrePausa.Location = new System.Drawing.Point(113, 70);
            this.txtNombrePausa.Name = "txtNombrePausa";
            this.txtNombrePausa.ReadOnly = true;
            this.txtNombrePausa.Size = new System.Drawing.Size(539, 29);
            this.txtNombrePausa.TabIndex = 211;
            // 
            // dtpHora
            // 
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHora.Location = new System.Drawing.Point(1043, 16);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.ShowUpDown = true;
            this.dtpHora.Size = new System.Drawing.Size(80, 20);
            this.dtpHora.TabIndex = 217;
            this.dtpHora.Value = new System.DateTime(2023, 5, 27, 11, 36, 15, 0);
            this.dtpHora.Visible = false;
            // 
            // frmMecanicosAsignados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 524);
            this.Controls.Add(this.dtpHora);
            this.Controls.Add(this.dtgListaAsignaciones);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pMotivoPausa);
            this.MaximizeBox = false;
            this.Name = "frmMecanicosAsignados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASIGNAR MECÁNICO";
            this.Load += new System.EventHandler(this.frmMecanicosAsignados_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaAsignaciones)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaAsignacionesVista)).EndInit();
            this.pMotivoPausa.ResumeLayout(false);
            this.pMotivoPausa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigo;
        public System.Windows.Forms.Button btnRegistrar;
        public System.Windows.Forms.Label lblCodigoOT;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraGrid.GridControl dtgListaAsignaciones;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaAsignacionesVista;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem terminarOTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pausarTrabajoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitarMecanicoToolStripMenuItem;
        private System.Windows.Forms.Panel pMotivoPausa;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtNombrePausa;
        public System.Windows.Forms.Button btnAgregarMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnHorasExtra;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.TextBox txtTurno;
        private System.Windows.Forms.Label label8;
    }
}