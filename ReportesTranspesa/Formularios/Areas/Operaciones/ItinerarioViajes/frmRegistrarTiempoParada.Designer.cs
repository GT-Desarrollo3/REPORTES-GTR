namespace ReportesTranspesa.Formularios.Areas.Operaciones.ItinerarioViajes
{
    partial class frmRegistrarTiempoParada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistrarTiempoParada));
            this.label1 = new System.Windows.Forms.Label();
            this.dtgTiempoParada = new DevExpress.XtraGrid.GridControl();
            this.dgvTiempoParada = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFechaFin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFechaInicio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpTiempoViaje = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPuntoParada = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPuntoInicio = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminar = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiempoParada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiempoParada)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SlateBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(493, 41);
            this.label1.TabIndex = 18;
            this.label1.Text = "REGISTRO DE TIEMPOS DE PARADA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgTiempoParada
            // 
            this.dtgTiempoParada.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgTiempoParada.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTiempoParada.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgTiempoParada.Location = new System.Drawing.Point(0, 341);
            this.dtgTiempoParada.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.dtgTiempoParada.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Red;
            this.dtgTiempoParada.LookAndFeel.SkinName = "VS2010";
            this.dtgTiempoParada.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTiempoParada.MainView = this.dgvTiempoParada;
            this.dtgTiempoParada.Name = "dtgTiempoParada";
            this.dtgTiempoParada.Size = new System.Drawing.Size(493, 214);
            this.dtgTiempoParada.TabIndex = 186;
            this.dtgTiempoParada.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTiempoParada});
            this.dtgTiempoParada.Click += new System.EventHandler(this.dtgTiempoParada_Click);
            this.dtgTiempoParada.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgTiempoParada_MouseUp);
            // 
            // dgvTiempoParada
            // 
            this.dgvTiempoParada.GridControl = this.dtgTiempoParada;
            this.dgvTiempoParada.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvTiempoParada.Name = "dgvTiempoParada";
            this.dgvTiempoParada.OptionsBehavior.Editable = false;
            this.dgvTiempoParada.OptionsBehavior.ReadOnly = true;
            this.dgvTiempoParada.OptionsView.ColumnAutoWidth = false;
            this.dgvTiempoParada.OptionsView.RowAutoHeight = true;
            this.dgvTiempoParada.OptionsView.ShowFooter = true;
            this.dgvTiempoParada.OptionsView.ShowGroupPanel = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFechaFin);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtFechaInicio);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtRuta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtConductor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 132);
            this.groupBox1.TabIndex = 187;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATOS DE VIAJE: ";
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaFin.Location = new System.Drawing.Point(308, 99);
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.ReadOnly = true;
            this.txtFechaFin.Size = new System.Drawing.Size(142, 20);
            this.txtFechaFin.TabIndex = 275;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SlateBlue;
            this.label7.Location = new System.Drawing.Point(263, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 274;
            this.label7.Text = "F. FIN:";
            // 
            // txtFechaInicio
            // 
            this.txtFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaInicio.Location = new System.Drawing.Point(96, 99);
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.ReadOnly = true;
            this.txtFechaInicio.Size = new System.Drawing.Size(142, 20);
            this.txtFechaInicio.TabIndex = 273;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SlateBlue;
            this.label4.Location = new System.Drawing.Point(36, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 272;
            this.label4.Text = "F. INICIO:";
            // 
            // txtRuta
            // 
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(96, 55);
            this.txtRuta.Multiline = true;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(354, 34);
            this.txtRuta.TabIndex = 271;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SlateBlue;
            this.label2.Location = new System.Drawing.Point(50, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 266;
            this.label2.Text = "RUTA:";
            // 
            // txtConductor
            // 
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductor.Location = new System.Drawing.Point(96, 25);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.ReadOnly = true;
            this.txtConductor.Size = new System.Drawing.Size(354, 20);
            this.txtConductor.TabIndex = 265;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SlateBlue;
            this.label3.Location = new System.Drawing.Point(11, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 264;
            this.label3.Text = "CONDUCTOR:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpTiempoViaje);
            this.groupBox2.Controls.Add(this.btnGuardar);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPuntoParada);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtPuntoInicio);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 134);
            this.groupBox2.TabIndex = 188;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ingreso de Tiempos: ";
            // 
            // dtpTiempoViaje
            // 
            this.dtpTiempoViaje.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTiempoViaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTiempoViaje.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTiempoViaje.Location = new System.Drawing.Point(116, 94);
            this.dtpTiempoViaje.Name = "dtpTiempoViaje";
            this.dtpTiempoViaje.Size = new System.Drawing.Size(78, 20);
            this.dtpTiempoViaje.TabIndex = 276;
            this.dtpTiempoViaje.Tag = "1";
            this.dtpTiempoViaje.Value = new System.DateTime(2024, 9, 27, 0, 0, 0, 0);
            this.dtpTiempoViaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpTiempoViaje_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(350, 90);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 33);
            this.btnGuardar.TabIndex = 275;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SlateBlue;
            this.label5.Location = new System.Drawing.Point(9, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 268;
            this.label5.Text = "Tiempo Llegada:";
            // 
            // txtPuntoParada
            // 
            this.txtPuntoParada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPuntoParada.Location = new System.Drawing.Point(96, 60);
            this.txtPuntoParada.Name = "txtPuntoParada";
            this.txtPuntoParada.ReadOnly = true;
            this.txtPuntoParada.Size = new System.Drawing.Size(354, 20);
            this.txtPuntoParada.TabIndex = 267;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(9, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 266;
            this.label6.Text = "Punto Parada:";
            // 
            // txtPuntoInicio
            // 
            this.txtPuntoInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPuntoInicio.Location = new System.Drawing.Point(96, 26);
            this.txtPuntoInicio.Name = "txtPuntoInicio";
            this.txtPuntoInicio.ReadOnly = true;
            this.txtPuntoInicio.Size = new System.Drawing.Size(354, 20);
            this.txtPuntoInicio.TabIndex = 265;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(9, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 264;
            this.label9.Text = "Punto de Inicio:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 26);
            // 
            // tsEliminar
            // 
            this.tsEliminar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminar.Name = "tsEliminar";
            this.tsEliminar.Size = new System.Drawing.Size(155, 22);
            this.tsEliminar.Text = "Quitar Tiempos";
            this.tsEliminar.Click += new System.EventHandler(this.tsEliminar_Click);
            // 
            // frmRegistrarTiempoParada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(493, 555);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtgTiempoParada);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmRegistrarTiempoParada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRO DE TIEMPOS DE PARADA";
            this.Load += new System.EventHandler(this.frmRegistrarTiempoParada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiempoParada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiempoParada)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgTiempoParada;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTiempoParada;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtFechaInicio;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtPuntoParada;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtPuntoInicio;
        private System.Windows.Forms.Label label9;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        public System.Windows.Forms.TextBox txtFechaFin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpTiempoViaje;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminar;
    }
}