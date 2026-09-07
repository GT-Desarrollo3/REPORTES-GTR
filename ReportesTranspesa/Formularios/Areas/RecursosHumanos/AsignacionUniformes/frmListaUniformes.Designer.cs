namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.AsignacionUniformes
{
    partial class frmListaUniformes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaUniformes));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAsignarUniforme = new System.Windows.Forms.ToolStripButton();
            this.btnHistorialUniformes = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxUniforme = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.FechaFin = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.FechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.dtgUniformePersonal = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cambiarFechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desvincularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUniformePersonalVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pActualizarFecha = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dtpNuevaFecha = new System.Windows.Forms.DateTimePicker();
            this.btnModificar = new System.Windows.Forms.Button();
            this.pDevolverCantidad = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDevolver = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUniformePersonal)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUniformePersonalVista)).BeginInit();
            this.pActualizarFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.pDevolverCantidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAsignarUniforme,
            this.btnHistorialUniformes});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1264, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAsignarUniforme
            // 
            this.btnAsignarUniforme.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.btnAsignarUniforme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAsignarUniforme.Name = "btnAsignarUniforme";
            this.btnAsignarUniforme.Size = new System.Drawing.Size(124, 22);
            this.btnAsignarUniforme.Text = "Entregar Uniforme";
            this.btnAsignarUniforme.Click += new System.EventHandler(this.btnAsignarUniforme_Click);
            // 
            // btnHistorialUniformes
            // 
            this.btnHistorialUniformes.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.btnHistorialUniformes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHistorialUniformes.Name = "btnHistorialUniformes";
            this.btnHistorialUniformes.Size = new System.Drawing.Size(142, 22);
            this.btnHistorialUniformes.Text = "Historial de Devueltos";
            this.btnHistorialUniformes.Click += new System.EventHandler(this.btnHistorialUniformes_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.cbxUniforme);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.label58);
            this.panel1.Controls.Add(this.groupBox14);
            this.panel1.Controls.Add(this.groupBox15);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 85);
            this.panel1.TabIndex = 14;
            // 
            // cbxUniforme
            // 
            this.cbxUniforme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUniforme.FormattingEnabled = true;
            this.cbxUniforme.Location = new System.Drawing.Point(800, 39);
            this.cbxUniforme.Name = "cbxUniforme";
            this.cbxUniforme.Size = new System.Drawing.Size(249, 21);
            this.cbxUniforme.TabIndex = 15;
            this.cbxUniforme.SelectedIndexChanged += new System.EventHandler(this.cbxUniforme_SelectedIndexChanged);
            this.cbxUniforme.DropDownClosed += new System.EventHandler(this.cbxUniforme_DropDownClosed);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_zoom_115406;
            this.btnBuscar.Location = new System.Drawing.Point(1121, 18);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(46, 48);
            this.btnBuscar.TabIndex = 113;
            this.btnBuscar.Text = "&B";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(797, 18);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(106, 13);
            this.label58.TabIndex = 111;
            this.label58.Text = "Buscar por Uniforme:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtPersonal);
            this.groupBox14.Location = new System.Drawing.Point(21, 13);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(375, 58);
            this.groupBox14.TabIndex = 104;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar Personal:";
            // 
            // txtPersonal
            // 
            this.txtPersonal.Location = new System.Drawing.Point(17, 24);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(340, 20);
            this.txtPersonal.TabIndex = 0;
            this.txtPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonal_KeyPress);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.FechaFin);
            this.groupBox15.Controls.Add(this.label40);
            this.groupBox15.Controls.Add(this.FechaInicio);
            this.groupBox15.Controls.Add(this.label41);
            this.groupBox15.Location = new System.Drawing.Point(421, 13);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(353, 58);
            this.groupBox15.TabIndex = 103;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Buscar por Fecha de Asignación:";
            // 
            // FechaFin
            // 
            this.FechaFin.CustomFormat = "dd-MM-yyyy";
            this.FechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaFin.Location = new System.Drawing.Point(219, 24);
            this.FechaFin.Name = "FechaFin";
            this.FechaFin.Size = new System.Drawing.Size(116, 20);
            this.FechaFin.TabIndex = 5;
            this.FechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FechaFin_KeyPress);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(189, 27);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(24, 13);
            this.label40.TabIndex = 4;
            this.label40.Text = "Fin:";
            // 
            // FechaInicio
            // 
            this.FechaInicio.CustomFormat = "dd-MM-yyyy";
            this.FechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaInicio.Location = new System.Drawing.Point(55, 24);
            this.FechaInicio.Name = "FechaInicio";
            this.FechaInicio.Size = new System.Drawing.Size(116, 20);
            this.FechaInicio.TabIndex = 2;
            this.FechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FechaInicio_KeyPress);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(14, 28);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 13);
            this.label41.TabIndex = 0;
            this.label41.Text = "Inicio:";
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
            this.btnExcel.Location = new System.Drawing.Point(1184, 18);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(50, 48);
            this.btnExcel.TabIndex = 101;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dtgUniformePersonal
            // 
            this.dtgUniformePersonal.AllowDrop = true;
            this.dtgUniformePersonal.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgUniformePersonal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgUniformePersonal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgUniformePersonal.Location = new System.Drawing.Point(0, 110);
            this.dtgUniformePersonal.MainView = this.dgvUniformePersonalVista;
            this.dtgUniformePersonal.Name = "dtgUniformePersonal";
            this.dtgUniformePersonal.Size = new System.Drawing.Size(1264, 540);
            this.dtgUniformePersonal.TabIndex = 15;
            this.dtgUniformePersonal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvUniformePersonalVista});
            this.dtgUniformePersonal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgUniformePersonal_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarFechaToolStripMenuItem,
            this.desvincularToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 70);
            // 
            // cambiarFechaToolStripMenuItem
            // 
            this.cambiarFechaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.cambiarFechaToolStripMenuItem.Name = "cambiarFechaToolStripMenuItem";
            this.cambiarFechaToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.cambiarFechaToolStripMenuItem.Text = "Cambiar Fecha";
            this.cambiarFechaToolStripMenuItem.Click += new System.EventHandler(this.cambiarFechaToolStripMenuItem_Click);
            // 
            // desvincularToolStripMenuItem
            // 
            this.desvincularToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.desvincularToolStripMenuItem.Name = "desvincularToolStripMenuItem";
            this.desvincularToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.desvincularToolStripMenuItem.Text = "Devolver Uniforme";
            this.desvincularToolStripMenuItem.Click += new System.EventHandler(this.desvincularToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dgvUniformePersonalVista
            // 
            this.dgvUniformePersonalVista.GridControl = this.dtgUniformePersonal;
            this.dgvUniformePersonalVista.Name = "dgvUniformePersonalVista";
            this.dgvUniformePersonalVista.OptionsBehavior.Editable = false;
            this.dgvUniformePersonalVista.OptionsView.ColumnAutoWidth = false;
            this.dgvUniformePersonalVista.OptionsView.RowAutoHeight = true;
            this.dgvUniformePersonalVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvUniformePersonalVista_CustomDrawCell);
            // 
            // pActualizarFecha
            // 
            this.pActualizarFecha.BackColor = System.Drawing.SystemColors.Control;
            this.pActualizarFecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pActualizarFecha.Controls.Add(this.label10);
            this.pActualizarFecha.Controls.Add(this.btnCerrar);
            this.pActualizarFecha.Controls.Add(this.groupBox5);
            this.pActualizarFecha.Controls.Add(this.btnModificar);
            this.pActualizarFecha.Location = new System.Drawing.Point(493, 258);
            this.pActualizarFecha.Name = "pActualizarFecha";
            this.pActualizarFecha.Size = new System.Drawing.Size(278, 147);
            this.pActualizarFecha.TabIndex = 17;
            this.pActualizarFecha.Visible = false;
            this.pActualizarFecha.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pActualizarFecha_MouseMove);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(14, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 18);
            this.label10.TabIndex = 19;
            this.label10.Text = "Editar Fecha de Entrega";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.btnCerrar.Location = new System.Drawing.Point(254, 5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(19, 19);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 12;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dtpNuevaFecha);
            this.groupBox5.Location = new System.Drawing.Point(15, 41);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(247, 48);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ingresar fecha:";
            // 
            // dtpNuevaFecha
            // 
            this.dtpNuevaFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNuevaFecha.Location = new System.Drawing.Point(12, 20);
            this.dtpNuevaFecha.Name = "dtpNuevaFecha";
            this.dtpNuevaFecha.Size = new System.Drawing.Size(222, 20);
            this.dtpNuevaFecha.TabIndex = 92;
            // 
            // btnModificar
            // 
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.Location = new System.Drawing.Point(94, 103);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(99, 29);
            this.btnModificar.TabIndex = 4;
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // pDevolverCantidad
            // 
            this.pDevolverCantidad.BackColor = System.Drawing.SystemColors.Control;
            this.pDevolverCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDevolverCantidad.Controls.Add(this.label1);
            this.pDevolverCantidad.Controls.Add(this.pictureBox1);
            this.pDevolverCantidad.Controls.Add(this.groupBox1);
            this.pDevolverCantidad.Controls.Add(this.btnDevolver);
            this.pDevolverCantidad.Location = new System.Drawing.Point(493, 258);
            this.pDevolverCantidad.Name = "pDevolverCantidad";
            this.pDevolverCantidad.Size = new System.Drawing.Size(278, 147);
            this.pDevolverCantidad.TabIndex = 18;
            this.pDevolverCantidad.Visible = false;
            this.pDevolverCantidad.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDevolverCantidad_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Devolver Uniformes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(254, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCantidad);
            this.groupBox1.Location = new System.Drawing.Point(15, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingresar cantidad a devolver:";
            // 
            // btnDevolver
            // 
            this.btnDevolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDevolver.Location = new System.Drawing.Point(94, 103);
            this.btnDevolver.Name = "btnDevolver";
            this.btnDevolver.Size = new System.Drawing.Size(99, 29);
            this.btnDevolver.TabIndex = 4;
            this.btnDevolver.Text = "DEVOLVER";
            this.btnDevolver.UseVisualStyleBackColor = true;
            this.btnDevolver.Click += new System.EventHandler(this.btnDevolver_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(12, 20);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(222, 20);
            this.txtCantidad.TabIndex = 93;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // frmListaUniformes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 650);
            this.Controls.Add(this.dtgUniformePersonal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pActualizarFecha);
            this.Controls.Add(this.pDevolverCantidad);
            this.Name = "frmListaUniformes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Uniformes Asignados";
            this.Load += new System.EventHandler(this.frmListaUniformes_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUniformePersonal)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUniformePersonalVista)).EndInit();
            this.pActualizarFecha.ResumeLayout(false);
            this.pActualizarFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.pDevolverCantidad.ResumeLayout(false);
            this.pDevolverCantidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAsignarUniforme;
        private System.Windows.Forms.ToolStripButton btnHistorialUniformes;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.DateTimePicker FechaFin;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker FechaInicio;
        private System.Windows.Forms.Label label41;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.ComboBox cbxUniforme;
        private DevExpress.XtraGrid.GridControl dtgUniformePersonal;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvUniformePersonalVista;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cambiarFechaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desvincularToolStripMenuItem;
        private System.Windows.Forms.Panel pActualizarFecha;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dtpNuevaFecha;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Panel pDevolverCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDevolver;
        private System.Windows.Forms.TextBox txtCantidad;
    }
}