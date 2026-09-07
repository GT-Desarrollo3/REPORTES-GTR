namespace ReportesTranspesa.Formularios.Areas.Operaciones.ItinerarioViajes
{
    partial class frmItinerarioViajes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItinerarioViajes));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConsolidado = new System.Windows.Forms.TabPage();
            this.dtgListaItinerario = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaItinerario = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.txtVehiculo = new System.Windows.Forms.TextBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnNuevoTiempo = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.tabControl1.SuspendLayout();
            this.tabConsolidado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaItinerario)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaItinerario)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConsolidado);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(20, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(958, 472);
            this.tabControl1.TabIndex = 118;
            // 
            // tabConsolidado
            // 
            this.tabConsolidado.Controls.Add(this.dtgListaItinerario);
            this.tabConsolidado.Controls.Add(this.panel4);
            this.tabConsolidado.Location = new System.Drawing.Point(4, 29);
            this.tabConsolidado.Name = "tabConsolidado";
            this.tabConsolidado.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsolidado.Size = new System.Drawing.Size(950, 439);
            this.tabConsolidado.TabIndex = 0;
            this.tabConsolidado.Text = "CONSOLIDADO";
            this.tabConsolidado.UseVisualStyleBackColor = true;
            // 
            // dtgListaItinerario
            // 
            this.dtgListaItinerario.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaItinerario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaItinerario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaItinerario.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dtgListaItinerario.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.dtgListaItinerario.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.dtgListaItinerario.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.dtgListaItinerario.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.dtgListaItinerario.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.dtgListaItinerario.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.dtgListaItinerario.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.dtgListaItinerario.Location = new System.Drawing.Point(3, 100);
            this.dtgListaItinerario.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.dtgListaItinerario.LookAndFeel.SkinName = "Stardust";
            this.dtgListaItinerario.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaItinerario.MainView = this.dgvListaItinerario;
            this.dtgListaItinerario.Name = "dtgListaItinerario";
            this.dtgListaItinerario.Size = new System.Drawing.Size(944, 336);
            this.dtgListaItinerario.TabIndex = 145;
            this.dtgListaItinerario.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaItinerario});
            this.dtgListaItinerario.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dtgListaItinerario_MouseDoubleClick);
            this.dtgListaItinerario.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaItinerario_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 26);
            // 
            // tsEliminar
            // 
            this.tsEliminar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminar.Name = "tsEliminar";
            this.tsEliminar.Size = new System.Drawing.Size(177, 22);
            this.tsEliminar.Text = "Quitar Consolidado";
            this.tsEliminar.Click += new System.EventHandler(this.tsEliminar_Click);
            // 
            // dgvListaItinerario
            // 
            this.dgvListaItinerario.GridControl = this.dtgListaItinerario;
            this.dgvListaItinerario.Name = "dgvListaItinerario";
            this.dgvListaItinerario.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvListaItinerario.OptionsBehavior.Editable = false;
            this.dgvListaItinerario.OptionsView.ColumnAutoWidth = false;
            this.dgvListaItinerario.OptionsView.ShowFooter = true;
            this.dgvListaItinerario.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaItinerario_CustomDrawCell);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtRuta);
            this.panel4.Controls.Add(this.metroLabel4);
            this.panel4.Controls.Add(this.dtpFechaIni);
            this.panel4.Controls.Add(this.metroLabel2);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.txtConductor);
            this.panel4.Controls.Add(this.dtpFechaFin);
            this.panel4.Controls.Add(this.txtVehiculo);
            this.panel4.Controls.Add(this.metroLabel3);
            this.panel4.Controls.Add(this.metroLabel1);
            this.panel4.Controls.Add(this.btnNuevoTiempo);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(944, 97);
            this.panel4.TabIndex = 143;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(156, 21);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(60, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel2.TabIndex = 95;
            this.metroLabel2.Text = "F. Viaje:";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(324, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 15);
            this.label5.TabIndex = 116;
            this.label5.Text = "--";
            // 
            // txtConductor
            // 
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductor.ForeColor = System.Drawing.Color.Black;
            this.txtConductor.Location = new System.Drawing.Point(438, 55);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(333, 21);
            this.txtConductor.TabIndex = 229;
            this.txtConductor.Tag = "";
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(343, 21);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaFin.TabIndex = 93;
            this.dtpFechaFin.Tag = "2";
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // txtVehiculo
            // 
            this.txtVehiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehiculo.ForeColor = System.Drawing.Color.Black;
            this.txtVehiculo.Location = new System.Drawing.Point(222, 55);
            this.txtVehiculo.Name = "txtVehiculo";
            this.txtVehiculo.Size = new System.Drawing.Size(100, 21);
            this.txtVehiculo.TabIndex = 228;
            this.txtVehiculo.Tag = "";
            this.txtVehiculo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVehiculo_KeyPress);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(222, 21);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(100, 21);
            this.dtpFechaIni.TabIndex = 92;
            this.dtpFechaIni.Tag = "1";
            this.dtpFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaIni_KeyPress);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.White;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel3.Location = new System.Drawing.Point(348, 55);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(84, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel3.TabIndex = 227;
            this.metroLabel3.Text = "Conductor:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.White;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(147, 55);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(69, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel1.TabIndex = 226;
            this.metroLabel1.Text = "Vehículo:";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnNuevoTiempo
            // 
            this.btnNuevoTiempo.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevoTiempo.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevoTiempo.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevoTiempo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnNuevoTiempo.Appearance.Options.UseBackColor = true;
            this.btnNuevoTiempo.Appearance.Options.UseBorderColor = true;
            this.btnNuevoTiempo.Appearance.Options.UseFont = true;
            this.btnNuevoTiempo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoTiempo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoTiempo.Image")));
            this.btnNuevoTiempo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevoTiempo.Location = new System.Drawing.Point(23, 25);
            this.btnNuevoTiempo.Name = "btnNuevoTiempo";
            this.btnNuevoTiempo.Size = new System.Drawing.Size(101, 47);
            this.btnNuevoTiempo.TabIndex = 224;
            this.btnNuevoTiempo.Tag = "5";
            this.btnNuevoTiempo.Text = "Tiempos\r\npor Ruta";
            this.btnNuevoTiempo.ToolTip = "Registrar Incidente";
            this.btnNuevoTiempo.Click += new System.EventHandler(this.btnNuevoTiempo_Click);
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
            this.btnBuscar.Location = new System.Drawing.Point(811, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 6;
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
            this.btnExcel.Location = new System.Drawing.Point(870, 25);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 7;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.ForeColor = System.Drawing.Color.Black;
            this.txtRuta.Location = new System.Drawing.Point(513, 21);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(258, 21);
            this.txtRuta.TabIndex = 231;
            this.txtRuta.Tag = "";
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.White;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel4.Location = new System.Drawing.Point(464, 21);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(43, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel4.TabIndex = 230;
            this.metroLabel4.Text = "Ruta:";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // frmItinerarioViajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 552);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmItinerarioViajes";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "ITINERARIO DE VIAJES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmItinerarioViajes_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabConsolidado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaItinerario)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaItinerario)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConsolidado;
        private System.Windows.Forms.Panel panel4;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnNuevoTiempo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        public System.Windows.Forms.TextBox txtConductor;
        public System.Windows.Forms.TextBox txtVehiculo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminar;
        public DevExpress.XtraGrid.GridControl dtgListaItinerario;
        public DevExpress.XtraGrid.Views.Grid.GridView dgvListaItinerario;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        public System.Windows.Forms.TextBox txtRuta;
        private MetroFramework.Controls.MetroLabel metroLabel4;


    }
}