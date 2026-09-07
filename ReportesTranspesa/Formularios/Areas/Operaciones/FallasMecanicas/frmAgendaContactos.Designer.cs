namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    partial class frmAgendaContactos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgendaContactos));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAgenda = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.cbxRubro = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.dtgContactos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvContactosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pDatosContacto = new System.Windows.Forms.Panel();
            this.cbxRubro2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label49 = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgContactos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContactosVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.pDatosContacto.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkRed;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(986, 45);
            this.label1.TabIndex = 17;
            this.label1.Text = "AGENDA DE CONTACTOS - AUXILIOS MECÁNICOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel4.Controls.Add(this.btnAgenda);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.cbxRubro);
            this.panel4.Controls.Add(this.label58);
            this.panel4.Controls.Add(this.groupBox14);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 45);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(986, 89);
            this.panel4.TabIndex = 18;
            // 
            // btnAgenda
            // 
            this.btnAgenda.BackColor = System.Drawing.Color.Lime;
            this.btnAgenda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgenda.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAgenda.Image = global::ReportesTranspesa.Properties.Resources.persona_logo_icon_169946;
            this.btnAgenda.Location = new System.Drawing.Point(18, 23);
            this.btnAgenda.Name = "btnAgenda";
            this.btnAgenda.Size = new System.Drawing.Size(115, 43);
            this.btnAgenda.TabIndex = 122;
            this.btnAgenda.Text = "Nuevo Contacto";
            this.btnAgenda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgenda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgenda.UseVisualStyleBackColor = false;
            this.btnAgenda.Click += new System.EventHandler(this.btnAgenda_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_zoom_115406;
            this.btnBuscar.Location = new System.Drawing.Point(858, 20);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(46, 48);
            this.btnBuscar.TabIndex = 121;
            this.btnBuscar.Text = "&B";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(920, 20);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(48, 48);
            this.btnExcel.TabIndex = 120;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // cbxRubro
            // 
            this.cbxRubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRubro.FormattingEnabled = true;
            this.cbxRubro.Location = new System.Drawing.Point(562, 42);
            this.cbxRubro.Name = "cbxRubro";
            this.cbxRubro.Size = new System.Drawing.Size(139, 21);
            this.cbxRubro.TabIndex = 119;
            this.cbxRubro.SelectedIndexChanged += new System.EventHandler(this.cbxRubro_SelectedIndexChanged);
            this.cbxRubro.DropDownClosed += new System.EventHandler(this.cbxRubro_DropDownClosed);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(559, 20);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(93, 13);
            this.label58.TabIndex = 117;
            this.label58.Text = "Buscar por Rubro:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtRuta);
            this.groupBox14.Location = new System.Drawing.Point(158, 15);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(375, 58);
            this.groupBox14.TabIndex = 116;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar Ubicacion:";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(9, 24);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(356, 20);
            this.txtRuta.TabIndex = 0;
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            // 
            // dtgContactos
            // 
            this.dtgContactos.AllowDrop = true;
            this.dtgContactos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgContactos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgContactos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgContactos.Location = new System.Drawing.Point(0, 134);
            this.dtgContactos.MainView = this.dgvContactosVista;
            this.dtgContactos.Name = "dtgContactos";
            this.dtgContactos.Size = new System.Drawing.Size(986, 441);
            this.dtgContactos.TabIndex = 19;
            this.dtgContactos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvContactosVista,
            this.gridView1});
            this.dtgContactos.DoubleClick += new System.EventHandler(this.dtgContactos_DoubleClick);
            this.dtgContactos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgContactos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dgvContactosVista
            // 
            this.dgvContactosVista.GridControl = this.dtgContactos;
            this.dgvContactosVista.Name = "dgvContactosVista";
            this.dgvContactosVista.OptionsBehavior.Editable = false;
            this.dgvContactosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvContactosVista.OptionsView.RowAutoHeight = true;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgContactos;
            this.gridView1.Name = "gridView1";
            // 
            // pDatosContacto
            // 
            this.pDatosContacto.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pDatosContacto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDatosContacto.Controls.Add(this.cbxRubro2);
            this.pDatosContacto.Controls.Add(this.label3);
            this.pDatosContacto.Controls.Add(this.label2);
            this.pDatosContacto.Controls.Add(this.txtUbicacion);
            this.pDatosContacto.Controls.Add(this.label19);
            this.pDatosContacto.Controls.Add(this.txtNumero);
            this.pDatosContacto.Controls.Add(this.label20);
            this.pDatosContacto.Controls.Add(this.txtNombre);
            this.pDatosContacto.Controls.Add(this.btnCancelar);
            this.pDatosContacto.Controls.Add(this.btnCerrar);
            this.pDatosContacto.Controls.Add(this.label49);
            this.pDatosContacto.Controls.Add(this.btnRegistrar);
            this.pDatosContacto.Location = new System.Drawing.Point(261, 150);
            this.pDatosContacto.Name = "pDatosContacto";
            this.pDatosContacto.Size = new System.Drawing.Size(461, 287);
            this.pDatosContacto.TabIndex = 100;
            this.pDatosContacto.Visible = false;
            this.pDatosContacto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDatosContacto_MouseMove);
            // 
            // cbxRubro2
            // 
            this.cbxRubro2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRubro2.FormattingEnabled = true;
            this.cbxRubro2.Location = new System.Drawing.Point(94, 182);
            this.cbxRubro2.Name = "cbxRubro2";
            this.cbxRubro2.Size = new System.Drawing.Size(164, 21);
            this.cbxRubro2.TabIndex = 121;
            this.cbxRubro2.SelectedIndexChanged += new System.EventHandler(this.cbxRubro2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 120;
            this.label3.Text = "Rubro:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label2.Location = new System.Drawing.Point(23, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "Ubicación:";
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Location = new System.Drawing.Point(94, 144);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(344, 20);
            this.txtUbicacion.TabIndex = 56;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label19.Location = new System.Drawing.Point(14, 109);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 15);
            this.label19.TabIndex = 55;
            this.label19.Text = "Nro. Tel/Cel:";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(94, 106);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(164, 20);
            this.txtNumero.TabIndex = 54;
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label20.Location = new System.Drawing.Point(33, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 15);
            this.label20.TabIndex = 53;
            this.label20.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(94, 68);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(344, 20);
            this.txtNombre.TabIndex = 52;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(105, 231);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(116, 38);
            this.btnCancelar.TabIndex = 51;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(423, 6);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar.TabIndex = 51;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.DarkRed;
            this.label49.Location = new System.Drawing.Point(105, 17);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(259, 25);
            this.label49.TabIndex = 49;
            this.label49.Text = "DATOS DE CONTACTO";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.DarkRed;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegistrar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnRegistrar.Location = new System.Drawing.Point(242, 231);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(116, 38);
            this.btnRegistrar.TabIndex = 42;
            this.btnRegistrar.Text = "Guardar";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // frmAgendaContactos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 575);
            this.Controls.Add(this.pDatosContacto);
            this.Controls.Add(this.dtgContactos);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Name = "frmAgendaContactos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGENDA DE CONTACTOS";
            this.Load += new System.EventHandler(this.frmAgendaContactos_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgContactos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContactosVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.pDatosContacto.ResumeLayout(false);
            this.pDatosContacto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbxRubro;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtRuta;
        public System.Windows.Forms.Button btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraGrid.GridControl dtgContactos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvContactosVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button btnAgenda;
        private System.Windows.Forms.Panel pDatosContacto;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRegistrar;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ComboBox cbxRubro2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
    }
}