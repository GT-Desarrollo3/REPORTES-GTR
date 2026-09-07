namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmGastosReten
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaGuiaRemitenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombrePersonal = new System.Windows.Forms.TextBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgLista = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pActualizar = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNuevoImporte = new System.Windows.Forms.TextBox();
            this.txtEmpleadoEditar = new System.Windows.Forms.TextBox();
            this.txtImporteActual = new System.Windows.Forms.TextBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblImporteActual = new System.Windows.Forms.Label();
            this.lblEmpleadoNombre = new System.Windows.Forms.Label();
            this.historialRetenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaExpressVista)).BeginInit();
            this.pActualizar.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1126, 65);
            this.label1.TabIndex = 2;
            this.label1.Text = "GASTOS DE RETEN - CAJA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.nuevaGuiaRemitenteToolStripMenuItem,
            this.toolStripMenuItem12,
            this.toolStripMenuItem11,
            this.toolStripMenuItem10,
            this.toolStripMenuItem9,
            this.toolStripMenuItem8,
            this.toolStripMenuItem7,
            this.toolStripMenuItem6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem14,
            this.toolStripMenuItem13,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripMenuItem15,
            this.historialRetenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 65);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1126, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.Image = global::ReportesTranspesa.Properties.Resources.gastosentregados;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 20);
            this.toolStripMenuItem1.Text = "Registrar - Vale Caja";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // nuevaGuiaRemitenteToolStripMenuItem
            // 
            this.nuevaGuiaRemitenteToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.nuevaGuiaRemitenteToolStripMenuItem.Name = "nuevaGuiaRemitenteToolStripMenuItem";
            this.nuevaGuiaRemitenteToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nuevaGuiaRemitenteToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.nuevaGuiaRemitenteToolStripMenuItem.Text = "Registrar -  Reten";
            this.nuevaGuiaRemitenteToolStripMenuItem.Click += new System.EventHandler(this.nuevaGuiaRemitenteToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNombrePersonal);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1126, 82);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTROS";
            // 
            // button2
            // 
            this.button2.Image = global::ReportesTranspesa.Properties.Resources.expotexcel;
            this.button2.Location = new System.Drawing.Point(1024, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "Exportar";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(275, 33);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(74, 20);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.ValueChanged += new System.EventHandler(this.dtpFechaFin_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "FECHA FIN:";
            // 
            // txtNombrePersonal
            // 
            this.txtNombrePersonal.Location = new System.Drawing.Point(466, 32);
            this.txtNombrePersonal.Name = "txtNombrePersonal";
            this.txtNombrePersonal.Size = new System.Drawing.Size(256, 20);
            this.txtNombrePersonal.TabIndex = 3;
            this.txtNombrePersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombrePersonal_KeyPress);
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd-MM-yyyy";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(113, 34);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(74, 20);
            this.dtpFecha.TabIndex = 2;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(374, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "POR NOMBRES:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "FECHA INICIO:";
            // 
            // dtgLista
            // 
            this.dtgLista.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgLista.Location = new System.Drawing.Point(0, 171);
            this.dtgLista.MainView = this.dgvListaExpressVista;
            this.dtgLista.Name = "dtgLista";
            this.dtgLista.Size = new System.Drawing.Size(1126, 423);
            this.dtgLista.TabIndex = 15;
            this.dtgLista.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaExpressVista});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.recargar;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dgvListaExpressVista
            // 
            this.dgvListaExpressVista.GridControl = this.dtgLista;
            this.dgvListaExpressVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvListaExpressVista.Name = "dgvListaExpressVista";
            this.dgvListaExpressVista.OptionsBehavior.Editable = false;
            this.dgvListaExpressVista.OptionsBehavior.ReadOnly = true;
            this.dgvListaExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaExpressVista.OptionsView.ShowFooter = true;
            // 
            // pActualizar
            // 
            this.pActualizar.Controls.Add(this.button1);
            this.pActualizar.Controls.Add(this.txtNuevoImporte);
            this.pActualizar.Controls.Add(this.txtEmpleadoEditar);
            this.pActualizar.Controls.Add(this.txtImporteActual);
            this.pActualizar.Controls.Add(this.btnActualizar);
            this.pActualizar.Controls.Add(this.label5);
            this.pActualizar.Controls.Add(this.lblImporteActual);
            this.pActualizar.Controls.Add(this.lblEmpleadoNombre);
            this.pActualizar.Location = new System.Drawing.Point(433, 318);
            this.pActualizar.Name = "pActualizar";
            this.pActualizar.Size = new System.Drawing.Size(305, 156);
            this.pActualizar.TabIndex = 17;
            this.pActualizar.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.IndianRed;
            this.button1.Location = new System.Drawing.Point(272, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNuevoImporte
            // 
            this.txtNuevoImporte.Location = new System.Drawing.Point(100, 78);
            this.txtNuevoImporte.Name = "txtNuevoImporte";
            this.txtNuevoImporte.Size = new System.Drawing.Size(100, 20);
            this.txtNuevoImporte.TabIndex = 6;
            this.txtNuevoImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNuevoImporte_KeyPress);
            // 
            // txtEmpleadoEditar
            // 
            this.txtEmpleadoEditar.Location = new System.Drawing.Point(83, 9);
            this.txtEmpleadoEditar.Name = "txtEmpleadoEditar";
            this.txtEmpleadoEditar.ReadOnly = true;
            this.txtEmpleadoEditar.Size = new System.Drawing.Size(172, 20);
            this.txtEmpleadoEditar.TabIndex = 5;
            // 
            // txtImporteActual
            // 
            this.txtImporteActual.Location = new System.Drawing.Point(100, 52);
            this.txtImporteActual.Name = "txtImporteActual";
            this.txtImporteActual.ReadOnly = true;
            this.txtImporteActual.Size = new System.Drawing.Size(100, 20);
            this.txtImporteActual.TabIndex = 4;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(114, 116);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Nuevo Importe:";
            // 
            // lblImporteActual
            // 
            this.lblImporteActual.AutoSize = true;
            this.lblImporteActual.Location = new System.Drawing.Point(16, 55);
            this.lblImporteActual.Name = "lblImporteActual";
            this.lblImporteActual.Size = new System.Drawing.Size(78, 13);
            this.lblImporteActual.TabIndex = 1;
            this.lblImporteActual.Text = "Importe Actual:";
            // 
            // lblEmpleadoNombre
            // 
            this.lblEmpleadoNombre.AutoSize = true;
            this.lblEmpleadoNombre.BackColor = System.Drawing.Color.MistyRose;
            this.lblEmpleadoNombre.Location = new System.Drawing.Point(16, 12);
            this.lblEmpleadoNombre.Name = "lblEmpleadoNombre";
            this.lblEmpleadoNombre.Size = new System.Drawing.Size(60, 13);
            this.lblEmpleadoNombre.TabIndex = 0;
            this.lblEmpleadoNombre.Text = "Empleado: ";
            // 
            // historialRetenToolStripMenuItem
            // 
            this.historialRetenToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cierreperiodo;
            this.historialRetenToolStripMenuItem.Name = "historialRetenToolStripMenuItem";
            this.historialRetenToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.historialRetenToolStripMenuItem.Text = "Historial Reten";
            this.historialRetenToolStripMenuItem.Click += new System.EventHandler(this.historialRetenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem2.Text = " ";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem3.Text = " ";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem4.Text = " ";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem5.Text = " ";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem6.Text = " ";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem7.Text = " ";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem8.Text = " ";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem9.Text = " ";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem10.Text = " ";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem11.Text = " ";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem12.Text = " ";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem13.Text = " ";
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem14.Text = " ";
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem15.Text = " ";
            // 
            // frmGastosReten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1126, 594);
            this.Controls.Add(this.pActualizar);
            this.Controls.Add(this.dtgLista);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Name = "frmGastosReten";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGastosReten";
            this.Load += new System.EventHandler(this.frmGastosReten_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaExpressVista)).EndInit();
            this.pActualizar.ResumeLayout(false);
            this.pActualizar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nuevaGuiaRemitenteToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNombrePersonal;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtgLista;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaExpressVista;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Panel pActualizar;
        private System.Windows.Forms.TextBox txtNuevoImporte;
        private System.Windows.Forms.TextBox txtEmpleadoEditar;
        private System.Windows.Forms.TextBox txtImporteActual;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblImporteActual;
        private System.Windows.Forms.Label lblEmpleadoNombre;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem historialRetenToolStripMenuItem;
    }
}