namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    partial class frmRegistrarUrea
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
            this.lblUrea = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.lblConductor = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnImportar = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstTracto = new System.Windows.Forms.ListView();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxSucursalUrea = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtgUrea = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.anularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUreaVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUrea)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUreaVista)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUrea
            // 
            this.lblUrea.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUrea.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrea.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.lblUrea.Location = new System.Drawing.Point(0, 0);
            this.lblUrea.Name = "lblUrea";
            this.lblUrea.Size = new System.Drawing.Size(727, 37);
            this.lblUrea.TabIndex = 0;
            this.lblUrea.Text = "Registrar Urea";
            this.lblUrea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConductor);
            this.groupBox1.Controls.Add(this.lblConductor);
            this.groupBox1.Controls.Add(this.txtCantidad);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.btnImportar);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lstTracto);
            this.groupBox1.Controls.Add(this.txtPlaca);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxSucursalUrea);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 200);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtConductor
            // 
            this.txtConductor.Location = new System.Drawing.Point(96, 158);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(219, 20);
            this.txtConductor.TabIndex = 222;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            // 
            // lblConductor
            // 
            this.lblConductor.AutoSize = true;
            this.lblConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConductor.Location = new System.Drawing.Point(26, 163);
            this.lblConductor.Name = "lblConductor";
            this.lblConductor.Size = new System.Drawing.Size(69, 16);
            this.lblConductor.TabIndex = 221;
            this.lblConductor.Text = "Conductor";
            // 
            // txtCantidad
            // 
            this.txtCantidad.DecimalPlaces = 3;
            this.txtCantidad.Location = new System.Drawing.Point(96, 115);
            this.txtCantidad.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(80, 20);
            this.txtCantidad.TabIndex = 220;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(547, 30);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(168, 20);
            this.dtpFecha.TabIndex = 219;
            // 
            // btnImportar
            // 
            this.btnImportar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnImportar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnImportar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImportar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Appearance.Options.UseBackColor = true;
            this.btnImportar.Appearance.Options.UseBorderColor = true;
            this.btnImportar.Appearance.Options.UseFont = true;
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnImportar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(287, 21);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(130, 37);
            this.btnImportar.TabIndex = 218;
            this.btnImportar.Tag = "5";
            this.btnImportar.Text = "REGISTRAR";
            this.btnImportar.ToolTip = "Importar a SPRING";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(498, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cantidad";
            // 
            // lstTracto
            // 
            this.lstTracto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTracto.ForeColor = System.Drawing.Color.Navy;
            this.lstTracto.FullRowSelect = true;
            this.lstTracto.GridLines = true;
            this.lstTracto.Location = new System.Drawing.Point(96, 89);
            this.lstTracto.MultiSelect = false;
            this.lstTracto.Name = "lstTracto";
            this.lstTracto.Size = new System.Drawing.Size(152, 10);
            this.lstTracto.TabIndex = 4;
            this.lstTracto.UseCompatibleStateImageBehavior = false;
            this.lstTracto.View = System.Windows.Forms.View.Details;
            this.lstTracto.Visible = false;
            this.lstTracto.Enter += new System.EventHandler(this.lstTracto_Enter);
            this.lstTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTracto_KeyPress);
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(96, 69);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(118, 20);
            this.txtPlaca.TabIndex = 3;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Placa";
            // 
            // cbxSucursalUrea
            // 
            this.cbxSucursalUrea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSucursalUrea.FormattingEnabled = true;
            this.cbxSucursalUrea.Location = new System.Drawing.Point(96, 29);
            this.cbxSucursalUrea.Name = "cbxSucursalUrea";
            this.cbxSucursalUrea.Size = new System.Drawing.Size(168, 21);
            this.cbxSucursalUrea.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Almacen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtgUrea);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 366);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listar Urea";
            // 
            // dtgUrea
            // 
            this.dtgUrea.AllowDrop = true;
            this.dtgUrea.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgUrea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgUrea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgUrea.Location = new System.Drawing.Point(3, 56);
            this.dtgUrea.MainView = this.dgvUreaVista;
            this.dtgUrea.Name = "dtgUrea";
            this.dtgUrea.Size = new System.Drawing.Size(721, 307);
            this.dtgUrea.TabIndex = 135;
            this.dtgUrea.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvUreaVista});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anularToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(110, 26);
            // 
            // anularToolStripMenuItem
            // 
            this.anularToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.anularToolStripMenuItem.Name = "anularToolStripMenuItem";
            this.anularToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.anularToolStripMenuItem.Text = "Anular";
            this.anularToolStripMenuItem.Click += new System.EventHandler(this.anularToolStripMenuItem_Click_1);
            // 
            // dgvUreaVista
            // 
            this.dgvUreaVista.GridControl = this.dtgUrea;
            this.dgvUreaVista.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvUreaVista.Name = "dgvUreaVista";
            this.dgvUreaVista.OptionsBehavior.Editable = false;
            this.dgvUreaVista.OptionsSelection.MultiSelect = true;
            this.dgvUreaVista.OptionsView.ColumnAutoWidth = false;
            this.dgvUreaVista.OptionsView.RowAutoHeight = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.dtpFechaFin);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtpFechaInicio);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 40);
            this.panel1.TabIndex = 136;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.BackColor2 = System.Drawing.Color.White;
            this.simpleButton1.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseBorderColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton1.Image = global::ReportesTranspesa.Properties.Resources.binocular;
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton1.Location = new System.Drawing.Point(308, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 31);
            this.simpleButton1.TabIndex = 221;
            this.simpleButton1.Tag = "5";
            this.simpleButton1.Text = "Buscar";
            this.simpleButton1.ToolTip = "Importar a SPRING";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(200, 9);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(83, 20);
            this.dtpFechaFin.TabIndex = 224;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(171, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 16);
            this.label6.TabIndex = 223;
            this.label6.Text = "Fin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 221;
            this.label5.Text = "Inicio";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(62, 9);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(83, 20);
            this.dtpFechaInicio.TabIndex = 222;
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(96, 214);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(240, 10);
            this.lstConductor.TabIndex = 223;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            // 
            // frmRegistrarUrea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(727, 603);
            this.Controls.Add(this.lstConductor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblUrea);
            this.Name = "frmRegistrarUrea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRegistrarUrea";
            this.Load += new System.EventHandler(this.frmRegistrarUrea_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgUrea)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUreaVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUrea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxSucursalUrea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstTracto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl dtgUrea;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvUreaVista;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private DevExpress.XtraEditors.SimpleButton btnImportar;
        private System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ListView lstConductor;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label lblConductor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem anularToolStripMenuItem;
    }
}