namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    partial class frmCompensarTE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompensarTE));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.dtgCompensacion = new DevExpress.XtraGrid.GridControl();
            this.dtgvCompensacionView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pCompensacion = new System.Windows.Forms.Panel();
            this.dtgCompensarUsuario = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsImprimirTicket = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvCompensarUsuarioView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.txtMecanico = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpCHoraFin = new System.Windows.Forms.DateTimePicker();
            this.dtpCFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpCHoraIni = new System.Windows.Forms.DateTimePicker();
            this.dtpCFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCompensacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCompensacionView)).BeginInit();
            this.pCompensacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCompensarUsuario)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCompensarUsuarioView)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(770, 43);
            this.label1.TabIndex = 186;
            this.label1.Text = "COMPENSACIÓN DE TIEMPO EXTRA DE MECÁNICOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 117);
            this.panel1.TabIndex = 187;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(15, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(424, 24);
            this.label2.TabIndex = 216;
            this.label2.Text = "LISTA DE HORAS EXTRA POR MECÁNICO:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNombre.Location = new System.Drawing.Point(89, 31);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(350, 21);
            this.txtNombre.TabIndex = 211;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(19, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 212;
            this.label7.Text = "Mecánico:";
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
            this.btnBuscar.Location = new System.Drawing.Point(473, 17);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 214;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(534, 17);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 215;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dtgCompensacion
            // 
            this.dtgCompensacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgCompensacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgCompensacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgCompensacion.Location = new System.Drawing.Point(0, 160);
            this.dtgCompensacion.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgCompensacion.MainView = this.dtgvCompensacionView;
            this.dtgCompensacion.Name = "dtgCompensacion";
            this.dtgCompensacion.Size = new System.Drawing.Size(770, 359);
            this.dtgCompensacion.TabIndex = 188;
            this.dtgCompensacion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvCompensacionView});
            this.dtgCompensacion.DoubleClick += new System.EventHandler(this.dtgCompensacion_DoubleClick);
            // 
            // dtgvCompensacionView
            // 
            this.dtgvCompensacionView.GridControl = this.dtgCompensacion;
            this.dtgvCompensacionView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "TIEMPO", null, "Total: {HH:mm:ss}", new System.DateTime(2024, 6, 20, 17, 14, 48, 220))});
            this.dtgvCompensacionView.Name = "dtgvCompensacionView";
            this.dtgvCompensacionView.OptionsBehavior.Editable = false;
            this.dtgvCompensacionView.OptionsView.ColumnAutoWidth = false;
            this.dtgvCompensacionView.OptionsView.RowAutoHeight = true;
            this.dtgvCompensacionView.OptionsView.ShowFooter = true;
            this.dtgvCompensacionView.OptionsView.ShowGroupPanel = false;
            // 
            // pCompensacion
            // 
            this.pCompensacion.BackColor = System.Drawing.Color.LemonChiffon;
            this.pCompensacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pCompensacion.Controls.Add(this.dtgCompensarUsuario);
            this.pCompensacion.Controls.Add(this.btnAgregar);
            this.pCompensacion.Controls.Add(this.txtMecanico);
            this.pCompensacion.Controls.Add(this.label5);
            this.pCompensacion.Controls.Add(this.groupBox4);
            this.pCompensacion.Controls.Add(this.label15);
            this.pCompensacion.Controls.Add(this.btnCerrar);
            this.pCompensacion.Location = new System.Drawing.Point(165, 43);
            this.pCompensacion.Name = "pCompensacion";
            this.pCompensacion.Size = new System.Drawing.Size(451, 422);
            this.pCompensacion.TabIndex = 221;
            this.pCompensacion.Visible = false;
            this.pCompensacion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pCompensacion_MouseMove);
            // 
            // dtgCompensarUsuario
            // 
            this.dtgCompensarUsuario.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgCompensarUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgCompensarUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgCompensarUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgCompensarUsuario.Location = new System.Drawing.Point(0, 220);
            this.dtgCompensarUsuario.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgCompensarUsuario.MainView = this.dtgvCompensarUsuarioView;
            this.dtgCompensarUsuario.Name = "dtgCompensarUsuario";
            this.dtgCompensarUsuario.Size = new System.Drawing.Size(449, 200);
            this.dtgCompensarUsuario.TabIndex = 222;
            this.dtgCompensarUsuario.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvCompensarUsuarioView});
            this.dtgCompensarUsuario.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgCompensarUsuario_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsImprimirTicket,
            this.tsEliminar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 48);
            // 
            // tsImprimirTicket
            // 
            this.tsImprimirTicket.Image = global::ReportesTranspesa.Properties.Resources.impresora;
            this.tsImprimirTicket.Name = "tsImprimirTicket";
            this.tsImprimirTicket.Size = new System.Drawing.Size(156, 22);
            this.tsImprimirTicket.Text = "Imprimir Ticket";
            this.tsImprimirTicket.Click += new System.EventHandler(this.imprimirTicketToolStripMenuItem_Click);
            // 
            // tsEliminar
            // 
            this.tsEliminar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminar.Name = "tsEliminar";
            this.tsEliminar.Size = new System.Drawing.Size(156, 22);
            this.tsEliminar.Text = "Eliminar Comp.";
            this.tsEliminar.Click += new System.EventHandler(this.tsEliminar_Click);
            // 
            // dtgvCompensarUsuarioView
            // 
            this.dtgvCompensarUsuarioView.GridControl = this.dtgCompensarUsuario;
            this.dtgvCompensarUsuarioView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "TIEMPO", null, "Total: {HH:mm:ss}", new System.DateTime(2024, 6, 20, 17, 14, 48, 220))});
            this.dtgvCompensarUsuarioView.Name = "dtgvCompensarUsuarioView";
            this.dtgvCompensarUsuarioView.OptionsBehavior.Editable = false;
            this.dtgvCompensarUsuarioView.OptionsView.ColumnAutoWidth = false;
            this.dtgvCompensarUsuarioView.OptionsView.RowAutoHeight = true;
            this.dtgvCompensarUsuarioView.OptionsView.ShowFooter = true;
            this.dtgvCompensarUsuarioView.OptionsView.ShowGroupPanel = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnAgregar.Location = new System.Drawing.Point(352, 123);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 65);
            this.btnAgregar.TabIndex = 221;
            this.btnAgregar.Tag = "5";
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtMecanico
            // 
            this.txtMecanico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMecanico.Location = new System.Drawing.Point(20, 65);
            this.txtMecanico.Name = "txtMecanico";
            this.txtMecanico.ReadOnly = true;
            this.txtMecanico.Size = new System.Drawing.Size(405, 21);
            this.txtMecanico.TabIndex = 214;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(17, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 208;
            this.label5.Text = "Mecánico:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpCHoraFin);
            this.groupBox4.Controls.Add(this.dtpCFechaFin);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.dtpCHoraIni);
            this.groupBox4.Controls.Add(this.dtpCFechaIni);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox4.Location = new System.Drawing.Point(18, 101);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(318, 104);
            this.groupBox4.TabIndex = 219;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ingresar Horas de Compensación:";
            // 
            // dtpCHoraFin
            // 
            this.dtpCHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpCHoraFin.Location = new System.Drawing.Point(171, 64);
            this.dtpCHoraFin.Name = "dtpCHoraFin";
            this.dtpCHoraFin.ShowUpDown = true;
            this.dtpCHoraFin.Size = new System.Drawing.Size(100, 21);
            this.dtpCHoraFin.TabIndex = 213;
            this.dtpCHoraFin.Value = new System.DateTime(2023, 5, 27, 11, 36, 15, 0);
            this.dtpCHoraFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpCHoraFin_KeyPress);
            // 
            // dtpCFechaFin
            // 
            this.dtpCFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCFechaFin.Location = new System.Drawing.Point(61, 64);
            this.dtpCFechaFin.Name = "dtpCFechaFin";
            this.dtpCFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpCFechaFin.TabIndex = 212;
            this.dtpCFechaFin.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpCFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpCFechaFin_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label14.Location = new System.Drawing.Point(28, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 15);
            this.label14.TabIndex = 211;
            this.label14.Text = "Fin:";
            // 
            // dtpCHoraIni
            // 
            this.dtpCHoraIni.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpCHoraIni.Location = new System.Drawing.Point(171, 28);
            this.dtpCHoraIni.Name = "dtpCHoraIni";
            this.dtpCHoraIni.ShowUpDown = true;
            this.dtpCHoraIni.Size = new System.Drawing.Size(100, 21);
            this.dtpCHoraIni.TabIndex = 210;
            this.dtpCHoraIni.Value = new System.DateTime(2023, 5, 27, 11, 36, 15, 0);
            this.dtpCHoraIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpCHoraIni_KeyPress);
            // 
            // dtpCFechaIni
            // 
            this.dtpCFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCFechaIni.Location = new System.Drawing.Point(61, 28);
            this.dtpCFechaIni.Name = "dtpCFechaIni";
            this.dtpCFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpCFechaIni.TabIndex = 209;
            this.dtpCFechaIni.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpCFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpCFechaIni_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label18.Location = new System.Drawing.Point(16, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 15);
            this.label18.TabIndex = 208;
            this.label18.Text = "Inicio:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label15.Location = new System.Drawing.Point(16, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(348, 22);
            this.label15.TabIndex = 49;
            this.label15.Text = "COMPENSACIÓN DE HORAS EXTRA";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.btnCerrar.Location = new System.Drawing.Point(423, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(23, 23);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCerrar.TabIndex = 12;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // frmCompensarTE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 519);
            this.Controls.Add(this.dtgCompensacion);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pCompensacion);
            this.MaximizeBox = false;
            this.Name = "frmCompensarTE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMPENSAR TIEMPO EXTRA";
            this.Load += new System.EventHandler(this.frmCompensarTE_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCompensacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCompensacionView)).EndInit();
            this.pCompensacion.ResumeLayout(false);
            this.pCompensacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCompensarUsuario)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCompensarUsuarioView)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label7;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtgCompensacion;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvCompensacionView;
        private System.Windows.Forms.Panel pCompensacion;
        private System.Windows.Forms.TextBox txtMecanico;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpCHoraFin;
        private System.Windows.Forms.DateTimePicker dtpCFechaFin;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpCHoraIni;
        private System.Windows.Forms.DateTimePicker dtpCFechaIni;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox btnCerrar;
        private DevExpress.XtraGrid.GridControl dtgCompensarUsuario;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvCompensarUsuarioView;
        public DevExpress.XtraEditors.SimpleButton btnAgregar;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminar;
        private System.Windows.Forms.ToolStripMenuItem tsImprimirTicket;
    }
}