namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.MovimientoComponentes
{
    partial class frmRegistroMovimientos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroMovimientos));
            this.dtgMovimientos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEditarMovimiento = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEliminarMovimiento = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMovimientosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnNuevoMovimiento = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbFiltroComponentes = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxSubSistema = new MetroFramework.Controls.MetroComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxSistema = new MetroFramework.Controls.MetroComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMovimientos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientosView)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgMovimientos
            // 
            this.dtgMovimientos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgMovimientos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgMovimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMovimientos.Location = new System.Drawing.Point(20, 184);
            this.dtgMovimientos.LookAndFeel.SkinName = "Darkroom";
            this.dtgMovimientos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgMovimientos.MainView = this.dgvMovimientosView;
            this.dtgMovimientos.Name = "dtgMovimientos";
            this.dtgMovimientos.Size = new System.Drawing.Size(1084, 345);
            this.dtgMovimientos.TabIndex = 17;
            this.dtgMovimientos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMovimientosView});
            this.dtgMovimientos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgMovimientos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEditarMovimiento,
            this.tsEliminarMovimiento});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 48);
            // 
            // tsEditarMovimiento
            // 
            this.tsEditarMovimiento.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.tsEditarMovimiento.Name = "tsEditarMovimiento";
            this.tsEditarMovimiento.Size = new System.Drawing.Size(185, 22);
            this.tsEditarMovimiento.Text = "Editar Movimiento";
            this.tsEditarMovimiento.Click += new System.EventHandler(this.tsEditarMovimiento_Click);
            // 
            // tsEliminarMovimiento
            // 
            this.tsEliminarMovimiento.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarMovimiento.Name = "tsEliminarMovimiento";
            this.tsEliminarMovimiento.Size = new System.Drawing.Size(185, 22);
            this.tsEliminarMovimiento.Text = "Eliminar Movimiento";
            this.tsEliminarMovimiento.Click += new System.EventHandler(this.tsEliminarMovimiento_Click);
            // 
            // dgvMovimientosView
            // 
            this.dgvMovimientosView.GridControl = this.dtgMovimientos;
            this.dgvMovimientosView.Name = "dgvMovimientosView";
            this.dgvMovimientosView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvMovimientosView.OptionsBehavior.Editable = false;
            this.dgvMovimientosView.OptionsView.ColumnAutoWidth = false;
            // 
            // btnNuevoMovimiento
            // 
            this.btnNuevoMovimiento.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoMovimiento.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoMovimiento.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoMovimiento.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnNuevoMovimiento.Appearance.Options.UseBackColor = true;
            this.btnNuevoMovimiento.Appearance.Options.UseBorderColor = true;
            this.btnNuevoMovimiento.Appearance.Options.UseFont = true;
            this.btnNuevoMovimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoMovimiento.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoMovimiento.Image")));
            this.btnNuevoMovimiento.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoMovimiento.Location = new System.Drawing.Point(23, 35);
            this.btnNuevoMovimiento.Name = "btnNuevoMovimiento";
            this.btnNuevoMovimiento.Size = new System.Drawing.Size(123, 47);
            this.btnNuevoMovimiento.TabIndex = 225;
            this.btnNuevoMovimiento.Tag = "5";
            this.btnNuevoMovimiento.Text = "Nuevo\r\nComponente";
            this.btnNuevoMovimiento.ToolTip = "Nuevo Componente";
            this.btnNuevoMovimiento.Click += new System.EventHandler(this.btnNuevoMovimiento_Click);
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
            this.btnBuscar.Location = new System.Drawing.Point(942, 35);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
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
            this.btnExcel.Location = new System.Drawing.Point(1003, 35);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 212;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panel4.Controls.Add(this.cbFiltroComponentes);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.btnNuevoMovimiento);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(20, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1084, 124);
            this.panel4.TabIndex = 16;
            // 
            // cbFiltroComponentes
            // 
            this.cbFiltroComponentes.AutoSize = true;
            this.cbFiltroComponentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cbFiltroComponentes.ForeColor = System.Drawing.Color.White;
            this.cbFiltroComponentes.Location = new System.Drawing.Point(541, 4);
            this.cbFiltroComponentes.Name = "cbFiltroComponentes";
            this.cbFiltroComponentes.Size = new System.Drawing.Size(130, 19);
            this.cbFiltroComponentes.TabIndex = 221;
            this.cbFiltroComponentes.Text = "COMPONENTE: ";
            this.cbFiltroComponentes.UseVisualStyleBackColor = true;
            this.cbFiltroComponentes.CheckedChanged += new System.EventHandler(this.cbFiltroComponentes_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxSubSistema);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbxSistema);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox2.Location = new System.Drawing.Point(533, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 100);
            this.groupBox2.TabIndex = 227;
            this.groupBox2.TabStop = false;
            // 
            // cbxSubSistema
            // 
            this.cbxSubSistema.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbxSubSistema.FormattingEnabled = true;
            this.cbxSubSistema.ItemHeight = 19;
            this.cbxSubSistema.Location = new System.Drawing.Point(95, 60);
            this.cbxSubSistema.Name = "cbxSubSistema";
            this.cbxSubSistema.Size = new System.Drawing.Size(259, 25);
            this.cbxSubSistema.Style = MetroFramework.MetroColorStyle.Red;
            this.cbxSubSistema.TabIndex = 220;
            this.cbxSubSistema.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cbxSubSistema.UseSelectable = true;
            this.cbxSubSistema.SelectedIndexChanged += new System.EventHandler(this.cbxSubSistema_SelectedIndexChanged);
            this.cbxSubSistema.DropDownClosed += new System.EventHandler(this.cbxSubSistema_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 219;
            this.label4.Text = "Subsistema:";
            // 
            // cbxSistema
            // 
            this.cbxSistema.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbxSistema.FormattingEnabled = true;
            this.cbxSistema.ItemHeight = 19;
            this.cbxSistema.Location = new System.Drawing.Point(95, 25);
            this.cbxSistema.Name = "cbxSistema";
            this.cbxSistema.Size = new System.Drawing.Size(259, 25);
            this.cbxSistema.Style = MetroFramework.MetroColorStyle.Red;
            this.cbxSistema.TabIndex = 218;
            this.cbxSistema.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cbxSistema.UseSelectable = true;
            this.cbxSistema.SelectedIndexChanged += new System.EventHandler(this.cbxSistema_SelectedIndexChanged);
            this.cbxSistema.DropDownClosed += new System.EventHandler(this.cbxSistema_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(34, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 217;
            this.label5.Text = "Sistema:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(175, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 100);
            this.groupBox1.TabIndex = 226;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTROS: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(49, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 218;
            this.label1.Text = "Placa:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(96, 27);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(102, 21);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPlaca.Location = new System.Drawing.Point(96, 62);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(225, 21);
            this.txtPlaca.TabIndex = 204;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 217;
            this.label3.Text = "F. Ejecución:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(200, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "--";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(219, 27);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(102, 21);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // frmRegistroMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 549);
            this.Controls.Add(this.dtgMovimientos);
            this.Controls.Add(this.panel4);
            this.Name = "frmRegistroMovimientos";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "MOVIMIENTO DE COMPONENTES";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRegistroMovimientos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMovimientos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientosView)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgMovimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMovimientosView;
        private DevExpress.XtraEditors.SimpleButton btnNuevoMovimiento;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroComboBox cbxSubSistema;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroComboBox cbxSistema;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        public System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarMovimiento;
        private System.Windows.Forms.ToolStripMenuItem tsEditarMovimiento;
        private System.Windows.Forms.CheckBox cbFiltroComponentes;

    }
}