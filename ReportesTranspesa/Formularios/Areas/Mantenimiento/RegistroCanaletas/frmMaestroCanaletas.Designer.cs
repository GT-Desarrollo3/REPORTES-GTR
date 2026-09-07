namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroCanaletas
{
    partial class frmMaestroCanaletas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaestroCanaletas));
            this.label2 = new System.Windows.Forms.Label();
            this.dtgMaestroCanaletas = new DevExpress.XtraGrid.GridControl();
            this.dgvMaestroCanaletasVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbxSucursal = new System.Windows.Forms.ComboBox();
            this.pNuevaProgramacion = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.dtpFProgramacion = new System.Windows.Forms.DateTimePicker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsProgramarLimpieza = new System.Windows.Forms.ToolStripMenuItem();
            this.tsProgramarCambio = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMaestroCanaletas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaestroCanaletasVista)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pNuevaProgramacion.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SaddleBrown;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(490, 47);
            this.label2.TabIndex = 20;
            this.label2.Text = "LISTA DE CANALETAS PLUVIALES";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgMaestroCanaletas
            // 
            this.dtgMaestroCanaletas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgMaestroCanaletas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgMaestroCanaletas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMaestroCanaletas.Location = new System.Drawing.Point(0, 142);
            this.dtgMaestroCanaletas.LookAndFeel.SkinMaskColor = System.Drawing.Color.Transparent;
            this.dtgMaestroCanaletas.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Transparent;
            this.dtgMaestroCanaletas.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.dtgMaestroCanaletas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgMaestroCanaletas.MainView = this.dgvMaestroCanaletasVista;
            this.dtgMaestroCanaletas.Name = "dtgMaestroCanaletas";
            this.dtgMaestroCanaletas.Size = new System.Drawing.Size(490, 398);
            this.dtgMaestroCanaletas.TabIndex = 184;
            this.dtgMaestroCanaletas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMaestroCanaletasVista});
            // 
            // dgvMaestroCanaletasVista
            // 
            this.dgvMaestroCanaletasVista.GridControl = this.dtgMaestroCanaletas;
            this.dgvMaestroCanaletasVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvMaestroCanaletasVista.Name = "dgvMaestroCanaletasVista";
            this.dgvMaestroCanaletasVista.OptionsBehavior.Editable = false;
            this.dgvMaestroCanaletasVista.OptionsView.ColumnAutoWidth = false;
            this.dgvMaestroCanaletasVista.OptionsView.RowAutoHeight = true;
            this.dgvMaestroCanaletasVista.OptionsView.ShowFooter = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.panel3.Location = new System.Drawing.Point(0, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(490, 95);
            this.panel3.TabIndex = 185;
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
            this.btnBuscar.Location = new System.Drawing.Point(332, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 220;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.cbxSucursal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(22, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 65);
            this.groupBox1.TabIndex = 219;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Búsqueda: ";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(17, 30);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(58, 15);
            this.label26.TabIndex = 218;
            this.label26.Text = "Sucursal:";
            // 
            // cbxSucursal
            // 
            this.cbxSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSucursal.FormattingEnabled = true;
            this.cbxSucursal.Items.AddRange(new object[] {
            "LARREA",
            "LA ENCALADA",
            "SALAVERRY"});
            this.cbxSucursal.Location = new System.Drawing.Point(81, 27);
            this.cbxSucursal.Name = "cbxSucursal";
            this.cbxSucursal.Size = new System.Drawing.Size(179, 21);
            this.cbxSucursal.TabIndex = 217;
            this.cbxSucursal.DropDownClosed += new System.EventHandler(this.cbxSucursal_DropDownClosed);
            // 
            // pNuevaProgramacion
            // 
            this.pNuevaProgramacion.BackColor = System.Drawing.Color.LemonChiffon;
            this.pNuevaProgramacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNuevaProgramacion.Controls.Add(this.label8);
            this.pNuevaProgramacion.Controls.Add(this.lblSucursal);
            this.pNuevaProgramacion.Controls.Add(this.label6);
            this.pNuevaProgramacion.Controls.Add(this.btnGuardar);
            this.pNuevaProgramacion.Controls.Add(this.label7);
            this.pNuevaProgramacion.Controls.Add(this.label3);
            this.pNuevaProgramacion.Controls.Add(this.btnCerrar);
            this.pNuevaProgramacion.Controls.Add(this.lblDescripcion);
            this.pNuevaProgramacion.Controls.Add(this.dtpFProgramacion);
            this.pNuevaProgramacion.Location = new System.Drawing.Point(45, 239);
            this.pNuevaProgramacion.Name = "pNuevaProgramacion";
            this.pNuevaProgramacion.Size = new System.Drawing.Size(402, 151);
            this.pNuevaProgramacion.TabIndex = 226;
            this.pNuevaProgramacion.Visible = false;
            this.pNuevaProgramacion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pNuevaProgramacion_MouseMove);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.Location = new System.Drawing.Point(15, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 15);
            this.label8.TabIndex = 212;
            this.label8.Text = "F. Programación:";
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblSucursal.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblSucursal.Location = new System.Drawing.Point(89, 68);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(72, 17);
            this.lblSucursal.TabIndex = 203;
            this.lblSucursal.Text = "T4G-852";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(15, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 17);
            this.label6.TabIndex = 202;
            this.label6.Text = "Sucursal:";
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
            this.btnGuardar.Location = new System.Drawing.Point(279, 98);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 36);
            this.btnGuardar.TabIndex = 201;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(15, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 30;
            this.label7.Text = "Descripción:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label3.Location = new System.Drawing.Point(14, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 22);
            this.label3.TabIndex = 29;
            this.label3.Text = "PROGRAMAR";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(373, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(27, 27);
            this.btnCerrar.TabIndex = 28;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblDescripcion.Location = new System.Drawing.Point(111, 44);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(266, 17);
            this.lblDescripcion.TabIndex = 27;
            this.lblDescripcion.Text = "TECHO OFICINAS ADINISTRATIVAS";
            // 
            // dtpFProgramacion
            // 
            this.dtpFProgramacion.CustomFormat = "dd/MM/yyyy";
            this.dtpFProgramacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFProgramacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFProgramacion.Location = new System.Drawing.Point(122, 105);
            this.dtpFProgramacion.Name = "dtpFProgramacion";
            this.dtpFProgramacion.Size = new System.Drawing.Size(109, 21);
            this.dtpFProgramacion.TabIndex = 211;
            this.dtpFProgramacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFProgramacion_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgramarLimpieza,
            this.tsProgramarCambio});
            this.contextMenuStrip1.Name = "contextMenuStrip3";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // tsProgramarLimpieza
            // 
            this.tsProgramarLimpieza.Image = global::ReportesTranspesa.Properties.Resources.filtrar;
            this.tsProgramarLimpieza.Name = "tsProgramarLimpieza";
            this.tsProgramarLimpieza.Size = new System.Drawing.Size(180, 22);
            this.tsProgramarLimpieza.Text = "Programar Limpieza";
            this.tsProgramarLimpieza.Click += new System.EventHandler(this.tsProgramarLimpieza_Click);
            // 
            // tsProgramarCambio
            // 
            this.tsProgramarCambio.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.tsProgramarCambio.Name = "tsProgramarCambio";
            this.tsProgramarCambio.Size = new System.Drawing.Size(180, 22);
            this.tsProgramarCambio.Text = "Programar Cambio";
            this.tsProgramarCambio.Click += new System.EventHandler(this.tsProgramarCambio_Click);
            // 
            // frmMaestroCanaletas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 540);
            this.Controls.Add(this.dtgMaestroCanaletas);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pNuevaProgramacion);
            this.MaximizeBox = false;
            this.Name = "frmMaestroCanaletas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAESTRO DE CANALETAS";
            this.Load += new System.EventHandler(this.frmMaestroCanaletas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMaestroCanaletas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaestroCanaletasVista)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pNuevaProgramacion.ResumeLayout(false);
            this.pNuevaProgramacion.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtgMaestroCanaletas;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMaestroCanaletasVista;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cbxSucursal;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Panel pNuevaProgramacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblDescripcion;
        public System.Windows.Forms.DateTimePicker dtpFProgramacion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsProgramarLimpieza;
        private System.Windows.Forms.ToolStripMenuItem tsProgramarCambio;
    }
}