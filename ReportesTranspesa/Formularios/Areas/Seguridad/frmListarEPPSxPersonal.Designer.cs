namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmListarEPPSxPersonal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarEPPSxPersonal));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevoTipoEPPS = new System.Windows.Forms.ToolStripButton();
            this.btnVidaUtilEPPS = new System.Windows.Forms.ToolStripButton();
            this.btnAsignarEPPS = new System.Windows.Forms.ToolStripButton();
            this.btnNuevoEPPS = new System.Windows.Forms.ToolStripButton();
            this.dtgEPPSPersonal = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cambiarFechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspenderEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desvincularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxTipoEPPS = new System.Windows.Forms.ComboBox();
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
            this.dtpNuevaFecha = new System.Windows.Forms.DateTimePicker();
            this.pActualizarFecha = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEPPSPersonal)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.pActualizarFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoTipoEPPS,
            this.btnVidaUtilEPPS,
            this.btnAsignarEPPS,
            this.btnNuevoEPPS});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1264, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoTipoEPPS
            // 
            this.btnNuevoTipoEPPS.Image = global::ReportesTranspesa.Properties.Resources.duplica;
            this.btnNuevoTipoEPPS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoTipoEPPS.Name = "btnNuevoTipoEPPS";
            this.btnNuevoTipoEPPS.Size = new System.Drawing.Size(94, 22);
            this.btnNuevoTipoEPPS.Text = "Tipos de EPP";
            this.btnNuevoTipoEPPS.Click += new System.EventHandler(this.btnNuevoTipoEPPS_Click);
            // 
            // btnVidaUtilEPPS
            // 
            this.btnVidaUtilEPPS.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnVidaUtilEPPS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVidaUtilEPPS.Name = "btnVidaUtilEPPS";
            this.btnVidaUtilEPPS.Size = new System.Drawing.Size(110, 22);
            this.btnVidaUtilEPPS.Text = "Vida Útil de EPP";
            this.btnVidaUtilEPPS.Click += new System.EventHandler(this.btnVidaUtilEPPS_Click);
            // 
            // btnAsignarEPPS
            // 
            this.btnAsignarEPPS.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.btnAsignarEPPS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAsignarEPPS.Name = "btnAsignarEPPS";
            this.btnAsignarEPPS.Size = new System.Drawing.Size(90, 22);
            this.btnAsignarEPPS.Text = "Asignar EPP";
            this.btnAsignarEPPS.Click += new System.EventHandler(this.btnAsignarEPPS_Click);
            // 
            // btnNuevoEPPS
            // 
            this.btnNuevoEPPS.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.btnNuevoEPPS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoEPPS.Name = "btnNuevoEPPS";
            this.btnNuevoEPPS.Size = new System.Drawing.Size(110, 22);
            this.btnNuevoEPPS.Text = "Historial de EPP";
            this.btnNuevoEPPS.Click += new System.EventHandler(this.btnNuevoEPPS_Click);
            // 
            // dtgEPPSPersonal
            // 
            this.dtgEPPSPersonal.AllowDrop = true;
            this.dtgEPPSPersonal.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgEPPSPersonal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgEPPSPersonal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgEPPSPersonal.Location = new System.Drawing.Point(0, 110);
            this.dtgEPPSPersonal.MainView = this.dgvExpressVista;
            this.dtgEPPSPersonal.Name = "dtgEPPSPersonal";
            this.dtgEPPSPersonal.Size = new System.Drawing.Size(1264, 540);
            this.dtgEPPSPersonal.TabIndex = 12;
            this.dtgEPPSPersonal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvExpressVista});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarFechaToolStripMenuItem,
            this.suspenderEstadoToolStripMenuItem,
            this.desvincularToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 70);
            // 
            // cambiarFechaToolStripMenuItem
            // 
            this.cambiarFechaToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.cambiarFechaToolStripMenuItem.Name = "cambiarFechaToolStripMenuItem";
            this.cambiarFechaToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.cambiarFechaToolStripMenuItem.Text = "Cambiar Fecha";
            this.cambiarFechaToolStripMenuItem.Click += new System.EventHandler(this.cambiarFechaToolStripMenuItem_Click);
            // 
            // suspenderEstadoToolStripMenuItem
            // 
            this.suspenderEstadoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.dormido;
            this.suspenderEstadoToolStripMenuItem.Name = "suspenderEstadoToolStripMenuItem";
            this.suspenderEstadoToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.suspenderEstadoToolStripMenuItem.Text = "Suspender / Liberar";
            this.suspenderEstadoToolStripMenuItem.Click += new System.EventHandler(this.suspenderEstadoToolStripMenuItem_Click);
            // 
            // desvincularToolStripMenuItem
            // 
            this.desvincularToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.desvincularToolStripMenuItem.Name = "desvincularToolStripMenuItem";
            this.desvincularToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.desvincularToolStripMenuItem.Text = "Desvincular";
            this.desvincularToolStripMenuItem.Click += new System.EventHandler(this.desvincularToolStripMenuItem_Click);
            // 
            // dgvExpressVista
            // 
            this.dgvExpressVista.GridControl = this.dtgEPPSPersonal;
            this.dgvExpressVista.Name = "dgvExpressVista";
            this.dgvExpressVista.OptionsBehavior.Editable = false;
            this.dgvExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvExpressVista.OptionsView.RowAutoHeight = true;
            this.dgvExpressVista.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvExpressVista_CustomDrawCell);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.cbxTipoEPPS);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.label58);
            this.panel1.Controls.Add(this.groupBox14);
            this.panel1.Controls.Add(this.groupBox15);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 85);
            this.panel1.TabIndex = 13;
            // 
            // cbxTipoEPPS
            // 
            this.cbxTipoEPPS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoEPPS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoEPPS.FormattingEnabled = true;
            this.cbxTipoEPPS.Location = new System.Drawing.Point(800, 37);
            this.cbxTipoEPPS.Name = "cbxTipoEPPS";
            this.cbxTipoEPPS.Size = new System.Drawing.Size(231, 21);
            this.cbxTipoEPPS.TabIndex = 114;
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
            this.label58.Size = new System.Drawing.Size(85, 13);
            this.label58.TabIndex = 111;
            this.label58.Text = "Buscar por EPP:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtPersonal);
            this.groupBox14.Location = new System.Drawing.Point(21, 13);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(375, 58);
            this.groupBox14.TabIndex = 104;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar por Empleado:";
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
            // dtpNuevaFecha
            // 
            this.dtpNuevaFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNuevaFecha.Location = new System.Drawing.Point(12, 20);
            this.dtpNuevaFecha.Name = "dtpNuevaFecha";
            this.dtpNuevaFecha.Size = new System.Drawing.Size(222, 20);
            this.dtpNuevaFecha.TabIndex = 92;
            this.dtpNuevaFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpNuevaFecha_KeyPress);
            // 
            // pActualizarFecha
            // 
            this.pActualizarFecha.BackColor = System.Drawing.SystemColors.Control;
            this.pActualizarFecha.Controls.Add(this.label10);
            this.pActualizarFecha.Controls.Add(this.pictureBox1);
            this.pActualizarFecha.Controls.Add(this.groupBox5);
            this.pActualizarFecha.Controls.Add(this.btnModificar);
            this.pActualizarFecha.Location = new System.Drawing.Point(499, 241);
            this.pActualizarFecha.Name = "pActualizarFecha";
            this.pActualizarFecha.Size = new System.Drawing.Size(278, 135);
            this.pActualizarFecha.TabIndex = 14;
            this.pActualizarFecha.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(32, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(213, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Editar Fecha de Asignación";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(252, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dtpNuevaFecha);
            this.groupBox5.Location = new System.Drawing.Point(17, 40);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(247, 48);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ingresar fecha:";
            // 
            // btnModificar
            // 
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.Location = new System.Drawing.Point(99, 96);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(99, 29);
            this.btnModificar.TabIndex = 4;
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // frmListarEPPSxPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 650);
            this.Controls.Add(this.pActualizarFecha);
            this.Controls.Add(this.dtgEPPSPersonal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmListarEPPSxPersonal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listar EPPS por Personal";
            this.Load += new System.EventHandler(this.frmListarEPPSxPersonal_Load);
            this.Shown += new System.EventHandler(this.frmListarEPPSxPersonal_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEPPSPersonal)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.pActualizarFecha.ResumeLayout(false);
            this.pActualizarFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevoEPPS;
        private System.Windows.Forms.ToolStripButton btnAsignarEPPS;
        private System.Windows.Forms.ToolStripButton btnNuevoTipoEPPS;
        private System.Windows.Forms.ToolStripButton btnVidaUtilEPPS;
        private DevExpress.XtraGrid.GridControl dtgEPPSPersonal;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvExpressVista;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem desvincularToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.ToolStripMenuItem cambiarFechaToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtpNuevaFecha;
        private System.Windows.Forms.Panel pActualizarFecha;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem suspenderEstadoToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.DateTimePicker FechaFin;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker FechaInicio;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label58;
        public System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cbxTipoEPPS;
    }
}