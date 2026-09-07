namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmZonaDeRuta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZonaDeRuta));
            this.label2 = new System.Windows.Forms.Label();
            this.pAsignarRuta = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.cbxOperaciones = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnNuevaZona = new DevExpress.XtraEditors.SimpleButton();
            this.dtgZonaRuta = new DevExpress.XtraGrid.GridControl();
            this.dgvZonaRutaVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pNuevaZona = new System.Windows.Forms.Panel();
            this.lvRuta = new System.Windows.Forms.ListView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cbxZona = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNuevaRuta = new System.Windows.Forms.TextBox();
            this.cbxOperacion2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.metroLabel20 = new MetroFramework.Controls.MetroLabel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pAsignarRuta.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgZonaRuta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonaRutaVista)).BeginInit();
            this.pNuevaZona.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(903, 41);
            this.label2.TabIndex = 49;
            this.label2.Text = "LISTA DE ZONAS DE RUTAS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pAsignarRuta
            // 
            this.pAsignarRuta.BackColor = System.Drawing.Color.LemonChiffon;
            this.pAsignarRuta.Controls.Add(this.btnExcel);
            this.pAsignarRuta.Controls.Add(this.btnBuscar);
            this.pAsignarRuta.Controls.Add(this.cbxOperaciones);
            this.pAsignarRuta.Controls.Add(this.label13);
            this.pAsignarRuta.Controls.Add(this.groupBox14);
            this.pAsignarRuta.Controls.Add(this.btnNuevaZona);
            this.pAsignarRuta.Dock = System.Windows.Forms.DockStyle.Top;
            this.pAsignarRuta.Location = new System.Drawing.Point(0, 41);
            this.pAsignarRuta.Name = "pAsignarRuta";
            this.pAsignarRuta.Size = new System.Drawing.Size(903, 87);
            this.pAsignarRuta.TabIndex = 104;
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
            this.btnExcel.Location = new System.Drawing.Point(830, 20);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 60;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
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
            this.btnBuscar.Location = new System.Drawing.Point(770, 20);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 59;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbxOperaciones
            // 
            this.cbxOperaciones.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperaciones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperaciones.FormattingEnabled = true;
            this.cbxOperaciones.Location = new System.Drawing.Point(551, 42);
            this.cbxOperaciones.Name = "cbxOperaciones";
            this.cbxOperaciones.Size = new System.Drawing.Size(167, 21);
            this.cbxOperaciones.TabIndex = 58;
            this.cbxOperaciones.SelectedIndexChanged += new System.EventHandler(this.cbxOperaciones_SelectedIndexChanged);
            this.cbxOperaciones.DropDownClosed += new System.EventHandler(this.cbxOperaciones_DropDownClosed);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label13.Location = new System.Drawing.Point(548, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 57;
            this.label13.Text = "Operación:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtRuta);
            this.groupBox14.Location = new System.Drawing.Point(152, 15);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(371, 58);
            this.groupBox14.TabIndex = 56;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buscar por Ruta:";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(15, 24);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(340, 20);
            this.txtRuta.TabIndex = 0;
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            // 
            // btnNuevaZona
            // 
            this.btnNuevaZona.Appearance.BackColor = System.Drawing.Color.White;
            this.btnNuevaZona.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnNuevaZona.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnNuevaZona.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaZona.Appearance.Options.UseBackColor = true;
            this.btnNuevaZona.Appearance.Options.UseBorderColor = true;
            this.btnNuevaZona.Appearance.Options.UseFont = true;
            this.btnNuevaZona.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaZona.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaZona.Image")));
            this.btnNuevaZona.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnNuevaZona.Location = new System.Drawing.Point(21, 20);
            this.btnNuevaZona.Name = "btnNuevaZona";
            this.btnNuevaZona.Size = new System.Drawing.Size(101, 47);
            this.btnNuevaZona.TabIndex = 55;
            this.btnNuevaZona.Text = "Agregar";
            this.btnNuevaZona.ToolTip = "Nueva Solicitud";
            this.btnNuevaZona.Click += new System.EventHandler(this.btnNuevaZona_Click);
            // 
            // dtgZonaRuta
            // 
            this.dtgZonaRuta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgZonaRuta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgZonaRuta.Location = new System.Drawing.Point(0, 128);
            this.dtgZonaRuta.MainView = this.dgvZonaRutaVista;
            this.dtgZonaRuta.Name = "dtgZonaRuta";
            this.dtgZonaRuta.Size = new System.Drawing.Size(903, 424);
            this.dtgZonaRuta.TabIndex = 105;
            this.dtgZonaRuta.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvZonaRutaVista});
            this.dtgZonaRuta.DoubleClick += new System.EventHandler(this.dtgZonaRuta_DoubleClick);
            // 
            // dgvZonaRutaVista
            // 
            this.dgvZonaRutaVista.GridControl = this.dtgZonaRuta;
            this.dgvZonaRutaVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvZonaRutaVista.Name = "dgvZonaRutaVista";
            this.dgvZonaRutaVista.OptionsBehavior.Editable = false;
            this.dgvZonaRutaVista.OptionsBehavior.ReadOnly = true;
            this.dgvZonaRutaVista.OptionsView.ColumnAutoWidth = false;
            this.dgvZonaRutaVista.OptionsView.ShowFooter = true;
            // 
            // pNuevaZona
            // 
            this.pNuevaZona.BackColor = System.Drawing.Color.LemonChiffon;
            this.pNuevaZona.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNuevaZona.Controls.Add(this.lvRuta);
            this.pNuevaZona.Controls.Add(this.btnAgregar);
            this.pNuevaZona.Controls.Add(this.btnCancelar);
            this.pNuevaZona.Controls.Add(this.cbxZona);
            this.pNuevaZona.Controls.Add(this.label3);
            this.pNuevaZona.Controls.Add(this.txtNuevaRuta);
            this.pNuevaZona.Controls.Add(this.cbxOperacion2);
            this.pNuevaZona.Controls.Add(this.label1);
            this.pNuevaZona.Controls.Add(this.label6);
            this.pNuevaZona.Controls.Add(this.label4);
            this.pNuevaZona.Controls.Add(this.metroLabel20);
            this.pNuevaZona.Controls.Add(this.btnCerrar);
            this.pNuevaZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.pNuevaZona.Location = new System.Drawing.Point(196, 169);
            this.pNuevaZona.Name = "pNuevaZona";
            this.pNuevaZona.Size = new System.Drawing.Size(511, 277);
            this.pNuevaZona.TabIndex = 133;
            this.pNuevaZona.Visible = false;
            this.pNuevaZona.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pNuevaZona_MouseMove);
            // 
            // lvRuta
            // 
            this.lvRuta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lvRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvRuta.ForeColor = System.Drawing.Color.Navy;
            this.lvRuta.FullRowSelect = true;
            this.lvRuta.GridLines = true;
            this.lvRuta.Location = new System.Drawing.Point(97, 123);
            this.lvRuta.MultiSelect = false;
            this.lvRuta.Name = "lvRuta";
            this.lvRuta.Size = new System.Drawing.Size(385, 10);
            this.lvRuta.TabIndex = 158;
            this.lvRuta.UseCompatibleStateImageBehavior = false;
            this.lvRuta.View = System.Windows.Forms.View.Details;
            this.lvRuta.Visible = false;
            this.lvRuta.Enter += new System.EventHandler(this.lvRuta_Enter);
            this.lvRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvRuta_KeyPress);
            this.lvRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvRuta_KeyUp);
            this.lvRuta.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRuta_MouseDoubleClick);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAgregar.Location = new System.Drawing.Point(280, 208);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(114, 42);
            this.btnAgregar.TabIndex = 156;
            this.btnAgregar.Text = " Asignar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(129, 208);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(114, 42);
            this.btnCancelar.TabIndex = 157;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cbxZona
            // 
            this.cbxZona.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxZona.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZona.FormattingEnabled = true;
            this.cbxZona.Location = new System.Drawing.Point(97, 147);
            this.cbxZona.Name = "cbxZona";
            this.cbxZona.Size = new System.Drawing.Size(164, 24);
            this.cbxZona.TabIndex = 155;
            this.cbxZona.SelectedIndexChanged += new System.EventHandler(this.cbxZona_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 154;
            this.label3.Text = "Zona:";
            // 
            // txtNuevaRuta
            // 
            this.txtNuevaRuta.Location = new System.Drawing.Point(97, 102);
            this.txtNuevaRuta.Name = "txtNuevaRuta";
            this.txtNuevaRuta.Size = new System.Drawing.Size(385, 22);
            this.txtNuevaRuta.TabIndex = 152;
            this.txtNuevaRuta.Enter += new System.EventHandler(this.txtNuevaRuta_Enter);
            this.txtNuevaRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNuevaRuta_KeyPress);
            this.txtNuevaRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNuevaRuta_KeyUp);
            this.txtNuevaRuta.Leave += new System.EventHandler(this.txtNuevaRuta_Leave);
            // 
            // cbxOperacion2
            // 
            this.cbxOperacion2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperacion2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperacion2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion2.FormattingEnabled = true;
            this.cbxOperacion2.Location = new System.Drawing.Point(97, 56);
            this.cbxOperacion2.Name = "cbxOperacion2";
            this.cbxOperacion2.Size = new System.Drawing.Size(164, 24);
            this.cbxOperacion2.TabIndex = 151;
            this.cbxOperacion2.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 150;
            this.label1.Text = "Operación:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(52, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 16);
            this.label6.TabIndex = 149;
            this.label6.Text = "Ruta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(16, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(307, 20);
            this.label4.TabIndex = 144;
            this.label4.Text = "INGRESE INFORMACIÓN DE RUTA:";
            // 
            // metroLabel20
            // 
            this.metroLabel20.AutoSize = true;
            this.metroLabel20.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel20.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel20.Location = new System.Drawing.Point(97, 18);
            this.metroLabel20.Name = "metroLabel20";
            this.metroLabel20.Size = new System.Drawing.Size(0, 0);
            this.metroLabel20.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel20.TabIndex = 104;
            this.metroLabel20.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Salmon;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(476, 6);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(27, 27);
            this.btnCerrar.TabIndex = 148;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // frmZonaDeRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 552);
            this.Controls.Add(this.dtgZonaRuta);
            this.Controls.Add(this.pAsignarRuta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pNuevaZona);
            this.MaximizeBox = false;
            this.Name = "frmZonaDeRuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LISTA DE ZONAS DE RUTAS";
            this.Load += new System.EventHandler(this.frmZonaDeRuta_Load);
            this.pAsignarRuta.ResumeLayout(false);
            this.pAsignarRuta.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgZonaRuta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZonaRutaVista)).EndInit();
            this.pNuevaZona.ResumeLayout(false);
            this.pNuevaZona.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pAsignarRuta;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        public System.Windows.Forms.ComboBox cbxOperaciones;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox14;
        public System.Windows.Forms.TextBox txtRuta;
        private DevExpress.XtraEditors.SimpleButton btnNuevaZona;
        private DevExpress.XtraGrid.GridControl dtgZonaRuta;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvZonaRutaVista;
        private System.Windows.Forms.Panel pNuevaZona;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroLabel metroLabel20;
        public System.Windows.Forms.Button btnCerrar;
        public System.Windows.Forms.TextBox txtNuevaRuta;
        public System.Windows.Forms.ComboBox cbxOperacion2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cbxZona;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ListView lvRuta;
    }
}