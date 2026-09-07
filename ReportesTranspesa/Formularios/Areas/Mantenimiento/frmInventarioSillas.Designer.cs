namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    partial class frmInventarioSillas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventarioSillas));
            this.dtgInventarioSillas = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarSillas = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvInventarioSillas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTrabajador = new System.Windows.Forms.TextBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSede = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pRegistrarActivo = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.btnBuscarImagen = new System.Windows.Forms.Button();
            this.pbCodigoBarras = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTrabajador2 = new System.Windows.Forms.TextBox();
            this.cbxEstado2 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label60 = new System.Windows.Forms.Label();
            this.lblActivo = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxArea = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxSede2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.lstPersona = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventarioSillas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventarioSillas)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pRegistrarActivo.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCodigoBarras)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgInventarioSillas
            // 
            this.dtgInventarioSillas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgInventarioSillas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgInventarioSillas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgInventarioSillas.Location = new System.Drawing.Point(0, 146);
            this.dtgInventarioSillas.LookAndFeel.SkinMaskColor = System.Drawing.Color.DarkOrange;
            this.dtgInventarioSillas.LookAndFeel.SkinName = "Money Twins";
            this.dtgInventarioSillas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgInventarioSillas.MainView = this.dgvInventarioSillas;
            this.dtgInventarioSillas.Name = "dtgInventarioSillas";
            this.dtgInventarioSillas.Size = new System.Drawing.Size(857, 357);
            this.dtgInventarioSillas.TabIndex = 188;
            this.dtgInventarioSillas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvInventarioSillas});
            this.dtgInventarioSillas.DoubleClick += new System.EventHandler(this.dtgInventarioSillas_DoubleClick);
            this.dtgInventarioSillas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgInventarioSillas_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarSillas});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            // 
            // tsEliminarSillas
            // 
            this.tsEliminarSillas.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarSillas.Name = "tsEliminarSillas";
            this.tsEliminarSillas.Size = new System.Drawing.Size(136, 22);
            this.tsEliminarSillas.Text = "Quitar Sillas";
            this.tsEliminarSillas.Click += new System.EventHandler(this.tsEliminarSillas_Click);
            // 
            // dgvInventarioSillas
            // 
            this.dgvInventarioSillas.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvInventarioSillas.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvInventarioSillas.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvInventarioSillas.Appearance.Row.Options.UseFont = true;
            this.dgvInventarioSillas.GridControl = this.dtgInventarioSillas;
            this.dgvInventarioSillas.Name = "dgvInventarioSillas";
            this.dgvInventarioSillas.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvInventarioSillas.OptionsBehavior.Editable = false;
            this.dgvInventarioSillas.OptionsView.ColumnAutoWidth = false;
            this.dgvInventarioSillas.OptionsView.ShowFooter = true;
            this.dgvInventarioSillas.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvInventarioSillas_CustomDrawCell);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Controls.Add(this.btnAgregar);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(857, 96);
            this.panel4.TabIndex = 187;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbxEstado);
            this.groupBox3.Location = new System.Drawing.Point(571, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(125, 58);
            this.groupBox3.TabIndex = 218;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Estado:";
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "TODOS",
            "LIBRE",
            "OCUPADO",
            "INACTIVO"});
            this.cbxEstado.Location = new System.Drawing.Point(14, 23);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(96, 21);
            this.cbxEstado.TabIndex = 237;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTrabajador);
            this.groupBox2.Location = new System.Drawing.Point(153, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 58);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trabajador: ";
            // 
            // txtTrabajador
            // 
            this.txtTrabajador.Location = new System.Drawing.Point(16, 24);
            this.txtTrabajador.Name = "txtTrabajador";
            this.txtTrabajador.Size = new System.Drawing.Size(260, 20);
            this.txtTrabajador.TabIndex = 204;
            this.txtTrabajador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrabajador_KeyPress);
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
            this.btnExcel.Location = new System.Drawing.Point(782, 24);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 212;
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
            this.btnBuscar.Location = new System.Drawing.Point(724, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 211;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(24, 25);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(105, 47);
            this.btnAgregar.TabIndex = 216;
            this.btnAgregar.Tag = "5";
            this.btnAgregar.Text = "Agregar\r\nActivo";
            this.btnAgregar.ToolTip = "Agregar Activo";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxSede);
            this.groupBox1.Location = new System.Drawing.Point(445, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(127, 58);
            this.groupBox1.TabIndex = 217;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sede:";
            // 
            // cbxSede
            // 
            this.cbxSede.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSede.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSede.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSede.FormattingEnabled = true;
            this.cbxSede.Items.AddRange(new object[] {
            "TODAS",
            "LARREA",
            "SALAVERRY"});
            this.cbxSede.Location = new System.Drawing.Point(14, 23);
            this.cbxSede.Name = "cbxSede";
            this.cbxSede.Size = new System.Drawing.Size(99, 21);
            this.cbxSede.TabIndex = 237;
            this.cbxSede.DropDownClosed += new System.EventHandler(this.cbxSede_DropDownClosed);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SaddleBrown;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(857, 50);
            this.label1.TabIndex = 186;
            this.label1.Text = "CONTROL DE INVENTARIO DE SILLAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pRegistrarActivo
            // 
            this.pRegistrarActivo.BackColor = System.Drawing.Color.LemonChiffon;
            this.pRegistrarActivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRegistrarActivo.Controls.Add(this.groupBox6);
            this.pRegistrarActivo.Controls.Add(this.groupBox5);
            this.pRegistrarActivo.Controls.Add(this.label58);
            this.pRegistrarActivo.Controls.Add(this.btnCerrar);
            this.pRegistrarActivo.Controls.Add(this.label60);
            this.pRegistrarActivo.Controls.Add(this.lblActivo);
            this.pRegistrarActivo.Controls.Add(this.groupBox4);
            this.pRegistrarActivo.Controls.Add(this.btnGuardar);
            this.pRegistrarActivo.Controls.Add(this.lstPersona);
            this.pRegistrarActivo.Location = new System.Drawing.Point(507, 315);
            this.pRegistrarActivo.Name = "pRegistrarActivo";
            this.pRegistrarActivo.Size = new System.Drawing.Size(692, 368);
            this.pRegistrarActivo.TabIndex = 213;
            this.pRegistrarActivo.Visible = false;
            this.pRegistrarActivo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pRegistrarActivo_MouseMove);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnCerrar2);
            this.groupBox6.Controls.Add(this.btnBuscarImagen);
            this.groupBox6.Controls.Add(this.pbCodigoBarras);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(316, 193);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(357, 105);
            this.groupBox6.TabIndex = 241;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CÓDIGO DE BARRAS: ";
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.BackColor = System.Drawing.Color.Red;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.ForeColor = System.Drawing.Color.White;
            this.btnCerrar2.Location = new System.Drawing.Point(327, 23);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(19, 21);
            this.btnCerrar2.TabIndex = 242;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // btnBuscarImagen
            // 
            this.btnBuscarImagen.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBuscarImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarImagen.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscarImagen.Location = new System.Drawing.Point(12, 22);
            this.btnBuscarImagen.Name = "btnBuscarImagen";
            this.btnBuscarImagen.Size = new System.Drawing.Size(55, 37);
            this.btnBuscarImagen.TabIndex = 240;
            this.btnBuscarImagen.Text = "Adjuntar\r\nimagen";
            this.btnBuscarImagen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarImagen.UseVisualStyleBackColor = false;
            this.btnBuscarImagen.Click += new System.EventHandler(this.btnBuscarImagen_Click);
            // 
            // pbCodigoBarras
            // 
            this.pbCodigoBarras.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbCodigoBarras.Location = new System.Drawing.Point(73, 23);
            this.pbCodigoBarras.Name = "pbCodigoBarras";
            this.pbCodigoBarras.Size = new System.Drawing.Size(255, 70);
            this.pbCodigoBarras.TabIndex = 241;
            this.pbCodigoBarras.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtTrabajador2);
            this.groupBox5.Controls.Add(this.cbxEstado2);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(316, 71);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(357, 116);
            this.groupBox5.TabIndex = 232;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "DATOS DE ESTADO: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 15);
            this.label9.TabIndex = 206;
            this.label9.Text = "Trabajador:";
            // 
            // txtTrabajador2
            // 
            this.txtTrabajador2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtTrabajador2.Location = new System.Drawing.Point(16, 80);
            this.txtTrabajador2.Name = "txtTrabajador2";
            this.txtTrabajador2.Size = new System.Drawing.Size(325, 20);
            this.txtTrabajador2.TabIndex = 205;
            this.txtTrabajador2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrabajador2_KeyPress);
            this.txtTrabajador2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTrabajador2_KeyUp);
            // 
            // cbxEstado2
            // 
            this.cbxEstado2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado2.FormattingEnabled = true;
            this.cbxEstado2.Items.AddRange(new object[] {
            "LIBRE",
            "OCUPADO",
            "INACTIVO"});
            this.cbxEstado2.Location = new System.Drawing.Point(67, 27);
            this.cbxEstado2.Name = "cbxEstado2";
            this.cbxEstado2.Size = new System.Drawing.Size(96, 21);
            this.cbxEstado2.TabIndex = 238;
            this.cbxEstado2.DropDownClosed += new System.EventHandler(this.cbxEstado2_DropDownClosed);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 15);
            this.label8.TabIndex = 239;
            this.label8.Text = "Estado:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label58.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label58.Location = new System.Drawing.Point(11, 12);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(186, 22);
            this.label58.TabIndex = 29;
            this.label58.Text = "AGREGAR ACTIVO";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(660, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar.TabIndex = 28;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.Black;
            this.label60.Location = new System.Drawing.Point(15, 43);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(59, 18);
            this.label60.TabIndex = 225;
            this.label60.Text = "Activo:";
            // 
            // lblActivo
            // 
            this.lblActivo.AutoSize = true;
            this.lblActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblActivo.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblActivo.Location = new System.Drawing.Point(71, 43);
            this.lblActivo.Name = "lblActivo";
            this.lblActivo.Size = new System.Drawing.Size(51, 18);
            this.lblActivo.TabIndex = 224;
            this.lblActivo.Text = "SILLA";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtObservacion);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cbxArea);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.cbxSede2);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtTicket);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(15, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(290, 227);
            this.groupBox4.TabIndex = 231;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DATOS DE ACTIVO: ";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacion.Location = new System.Drawing.Point(13, 162);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(261, 45);
            this.txtObservacion.TabIndex = 219;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 15);
            this.label5.TabIndex = 242;
            this.label5.Text = "Observación:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 241;
            this.label4.Text = "Área:";
            // 
            // cbxArea
            // 
            this.cbxArea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxArea.FormattingEnabled = true;
            this.cbxArea.Location = new System.Drawing.Point(58, 104);
            this.cbxArea.Name = "cbxArea";
            this.cbxArea.Size = new System.Drawing.Size(218, 21);
            this.cbxArea.TabIndex = 240;
            this.cbxArea.SelectedIndexChanged += new System.EventHandler(this.cbxArea_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 239;
            this.label3.Text = "Sede:";
            // 
            // cbxSede2
            // 
            this.cbxSede2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSede2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSede2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSede2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSede2.FormattingEnabled = true;
            this.cbxSede2.Items.AddRange(new object[] {
            "LARREA",
            "SALAVERRY"});
            this.cbxSede2.Location = new System.Drawing.Point(58, 66);
            this.cbxSede2.Name = "cbxSede2";
            this.cbxSede2.Size = new System.Drawing.Size(110, 21);
            this.cbxSede2.TabIndex = 238;
            this.cbxSede2.DropDownClosed += new System.EventHandler(this.cbxSede2_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 206;
            this.label2.Text = "Ticket:";
            // 
            // txtTicket
            // 
            this.txtTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTicket.Location = new System.Drawing.Point(58, 29);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.Size = new System.Drawing.Size(110, 21);
            this.txtTicket.TabIndex = 205;
            this.txtTicket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTicket_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(296, 318);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 36);
            this.btnGuardar.TabIndex = 201;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lstPersona
            // 
            this.lstPersona.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPersona.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPersona.ForeColor = System.Drawing.Color.Navy;
            this.lstPersona.FullRowSelect = true;
            this.lstPersona.GridLines = true;
            this.lstPersona.Location = new System.Drawing.Point(332, 170);
            this.lstPersona.MultiSelect = false;
            this.lstPersona.Name = "lstPersona";
            this.lstPersona.Size = new System.Drawing.Size(325, 128);
            this.lstPersona.TabIndex = 240;
            this.lstPersona.UseCompatibleStateImageBehavior = false;
            this.lstPersona.View = System.Windows.Forms.View.Details;
            this.lstPersona.Visible = false;
            this.lstPersona.Enter += new System.EventHandler(this.lstPersona_Enter);
            this.lstPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersona_KeyPress);
            this.lstPersona.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstPersona_MouseDoubleClick);
            // 
            // frmInventarioSillas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 503);
            this.Controls.Add(this.dtgInventarioSillas);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pRegistrarActivo);
            this.Name = "frmInventarioSillas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INVENTARIO DE SILLAS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInventarioSillas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgInventarioSillas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventarioSillas)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.pRegistrarActivo.ResumeLayout(false);
            this.pRegistrarActivo.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCodigoBarras)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgInventarioSillas;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvInventarioSillas;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtTrabajador;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cbxSede;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ComboBox cbxEstado;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarSillas;
        private System.Windows.Forms.Panel pRegistrarActivo;
        public System.Windows.Forms.TextBox txtObservacion;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Label label58;
        public System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label lblActivo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtTrabajador2;
        public System.Windows.Forms.ComboBox cbxEstado2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cbxArea;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbxSede2;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.ListView lstPersona;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnCerrar2;
        public System.Windows.Forms.Button btnBuscarImagen;
        public System.Windows.Forms.PictureBox pbCodigoBarras;
    }
}