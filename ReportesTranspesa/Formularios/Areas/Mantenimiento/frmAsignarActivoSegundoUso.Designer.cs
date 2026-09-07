namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    partial class frmAsignarActivoSegundoUso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarActivoSegundoUso));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSucursal = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvSegundoUso = new System.Windows.Forms.DataGridView();
            this.CheckActivo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UniMedidaActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaRegistroActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuarioRegistroActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExcelSegundoUso = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBuscarSegundoUso = new System.Windows.Forms.TextBox();
            this.dgvActivosSegundoUso = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvEmpleado = new System.Windows.Forms.DataGridView();
            this.CheckEmpleado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBuscarEmpleado = new System.Windows.Forms.TextBox();
            this.lstOT = new System.Windows.Forms.ListView();
            this.txtOT = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.dgvDetalleOT = new System.Windows.Forms.DataGridView();
            this.txtCantidadUso = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVale = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblplaca = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CheckMantenimiento = new System.Windows.Forms.CheckBox();
            this.chkConsumible = new System.Windows.Forms.CheckBox();
            this.tabAsignacion = new System.Windows.Forms.TabControl();
            this.tabMtto = new System.Windows.Forms.TabPage();
            this.tabLogistica = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRequerimiento = new System.Windows.Forms.TextBox();
            this.lstRequerimiento = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescripcionR = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFechaR = new System.Windows.Forms.DateTimePicker();
            this.txtCentroCosto = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dgvDetalleReq = new System.Windows.Forms.DataGridView();
            this.txtSolicitado = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCantidadUsoR = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnGuardarR = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSegundoUso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivosSegundoUso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleOT)).BeginInit();
            this.tabAsignacion.SuspendLayout();
            this.tabMtto.SuspendLayout();
            this.tabLogistica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleReq)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(923, 41);
            this.label1.TabIndex = 5;
            this.label1.Text = "ASIGNAR ACTIVO - SEGUNDO USO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxSucursal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dgvSegundoUso);
            this.groupBox1.Controls.Add(this.btnExcelSegundoUso);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtBuscarSegundoUso);
            this.groupBox1.Controls.Add(this.dgvActivosSegundoUso);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 274);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ACTIVOS DE SEGUNDO USO: ";
            // 
            // cbxSucursal
            // 
            this.cbxSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSucursal.FormattingEnabled = true;
            this.cbxSucursal.Items.AddRange(new object[] {
            "TRUJILLO",
            "LIMA"});
            this.cbxSucursal.Location = new System.Drawing.Point(287, 45);
            this.cbxSucursal.Name = "cbxSucursal";
            this.cbxSucursal.Size = new System.Drawing.Size(111, 21);
            this.cbxSucursal.TabIndex = 235;
            this.cbxSucursal.DropDownClosed += new System.EventHandler(this.cbxSucursal_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(284, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 234;
            this.label3.Text = "Sucursal:";
            // 
            // dgvSegundoUso
            // 
            this.dgvSegundoUso.AllowUserToAddRows = false;
            this.dgvSegundoUso.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSegundoUso.BackgroundColor = System.Drawing.Color.White;
            this.dgvSegundoUso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSegundoUso.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckActivo,
            this.idActivo,
            this.NombreActivo,
            this.UniMedidaActivo,
            this.CodigoActivo,
            this.CantidadActivo,
            this.EstadoActivo,
            this.fechaRegistroActivo,
            this.usuarioRegistroActivo});
            this.dgvSegundoUso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvSegundoUso.Location = new System.Drawing.Point(3, 80);
            this.dgvSegundoUso.MultiSelect = false;
            this.dgvSegundoUso.Name = "dgvSegundoUso";
            this.dgvSegundoUso.ReadOnly = true;
            this.dgvSegundoUso.RowHeadersVisible = false;
            this.dgvSegundoUso.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSegundoUso.ShowRowErrors = false;
            this.dgvSegundoUso.Size = new System.Drawing.Size(505, 191);
            this.dgvSegundoUso.TabIndex = 12;
            this.dgvSegundoUso.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSegundoUso_CellContentClick);
            // 
            // CheckActivo
            // 
            this.CheckActivo.FalseValue = "false";
            this.CheckActivo.HeaderText = "Check";
            this.CheckActivo.Name = "CheckActivo";
            this.CheckActivo.ReadOnly = true;
            this.CheckActivo.TrueValue = "true";
            this.CheckActivo.Width = 50;
            // 
            // idActivo
            // 
            this.idActivo.HeaderText = "idActivo";
            this.idActivo.Name = "idActivo";
            this.idActivo.ReadOnly = true;
            this.idActivo.Visible = false;
            // 
            // NombreActivo
            // 
            this.NombreActivo.HeaderText = "Nombre";
            this.NombreActivo.Name = "NombreActivo";
            this.NombreActivo.ReadOnly = true;
            this.NombreActivo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NombreActivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NombreActivo.Width = 150;
            // 
            // UniMedidaActivo
            // 
            this.UniMedidaActivo.HeaderText = "UniMedida";
            this.UniMedidaActivo.Name = "UniMedidaActivo";
            this.UniMedidaActivo.ReadOnly = true;
            // 
            // CodigoActivo
            // 
            this.CodigoActivo.HeaderText = "Codigo";
            this.CodigoActivo.Name = "CodigoActivo";
            this.CodigoActivo.ReadOnly = true;
            this.CodigoActivo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CodigoActivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CantidadActivo
            // 
            this.CantidadActivo.HeaderText = "Cantidad";
            this.CantidadActivo.Name = "CantidadActivo";
            this.CantidadActivo.ReadOnly = true;
            // 
            // EstadoActivo
            // 
            this.EstadoActivo.HeaderText = "Estado";
            this.EstadoActivo.Name = "EstadoActivo";
            this.EstadoActivo.ReadOnly = true;
            this.EstadoActivo.Visible = false;
            // 
            // fechaRegistroActivo
            // 
            this.fechaRegistroActivo.HeaderText = "FechaRegistro";
            this.fechaRegistroActivo.Name = "fechaRegistroActivo";
            this.fechaRegistroActivo.ReadOnly = true;
            this.fechaRegistroActivo.Visible = false;
            // 
            // usuarioRegistroActivo
            // 
            this.usuarioRegistroActivo.HeaderText = "UsuarioRegistro";
            this.usuarioRegistroActivo.Name = "usuarioRegistroActivo";
            this.usuarioRegistroActivo.ReadOnly = true;
            this.usuarioRegistroActivo.Visible = false;
            // 
            // btnExcelSegundoUso
            // 
            this.btnExcelSegundoUso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelSegundoUso.FlatAppearance.BorderSize = 0;
            this.btnExcelSegundoUso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelSegundoUso.Image = global::ReportesTranspesa.Properties.Resources.expotexcel;
            this.btnExcelSegundoUso.Location = new System.Drawing.Point(415, 29);
            this.btnExcelSegundoUso.Name = "btnExcelSegundoUso";
            this.btnExcelSegundoUso.Size = new System.Drawing.Size(44, 37);
            this.btnExcelSegundoUso.TabIndex = 11;
            this.btnExcelSegundoUso.UseVisualStyleBackColor = true;
            this.btnExcelSegundoUso.Click += new System.EventHandler(this.btnExcelSegundoUso_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "Nombre de Activo:";
            // 
            // txtBuscarSegundoUso
            // 
            this.txtBuscarSegundoUso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarSegundoUso.Location = new System.Drawing.Point(15, 45);
            this.txtBuscarSegundoUso.Name = "txtBuscarSegundoUso";
            this.txtBuscarSegundoUso.Size = new System.Drawing.Size(251, 20);
            this.txtBuscarSegundoUso.TabIndex = 4;
            this.txtBuscarSegundoUso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarSegundoUso_KeyPress);
            // 
            // dgvActivosSegundoUso
            // 
            this.dgvActivosSegundoUso.Location = new System.Drawing.Point(199, 97);
            this.dgvActivosSegundoUso.LookAndFeel.SkinName = "Office 2007 Blue";
            this.dgvActivosSegundoUso.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgvActivosSegundoUso.MainView = this.gridView2;
            this.dgvActivosSegundoUso.Name = "dgvActivosSegundoUso";
            this.dgvActivosSegundoUso.Size = new System.Drawing.Size(297, 148);
            this.dgvActivosSegundoUso.TabIndex = 115;
            this.dgvActivosSegundoUso.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.dgvActivosSegundoUso.Visible = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.dgvActivosSegundoUso;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowFooter = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvEmpleado);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtBuscarEmpleado);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(12, 336);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 256);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ASIGNAR EMPLEADO: ";
            // 
            // dgvEmpleado
            // 
            this.dgvEmpleado.AllowUserToAddRows = false;
            this.dgvEmpleado.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmpleado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckEmpleado,
            this.idEmpleado,
            this.Empleado});
            this.dgvEmpleado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvEmpleado.Location = new System.Drawing.Point(3, 62);
            this.dgvEmpleado.Name = "dgvEmpleado";
            this.dgvEmpleado.ReadOnly = true;
            this.dgvEmpleado.RowHeadersVisible = false;
            this.dgvEmpleado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleado.ShowRowErrors = false;
            this.dgvEmpleado.Size = new System.Drawing.Size(505, 191);
            this.dgvEmpleado.TabIndex = 14;
            this.dgvEmpleado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleado_CellContentClick);
            // 
            // CheckEmpleado
            // 
            this.CheckEmpleado.FalseValue = "false";
            this.CheckEmpleado.HeaderText = "Check";
            this.CheckEmpleado.Name = "CheckEmpleado";
            this.CheckEmpleado.ReadOnly = true;
            this.CheckEmpleado.TrueValue = "true";
            this.CheckEmpleado.Width = 50;
            // 
            // idEmpleado
            // 
            this.idEmpleado.HeaderText = "idEmpleado";
            this.idEmpleado.Name = "idEmpleado";
            this.idEmpleado.ReadOnly = true;
            this.idEmpleado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.idEmpleado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idEmpleado.Visible = false;
            // 
            // Empleado
            // 
            this.Empleado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Empleado.HeaderText = "Empleado";
            this.Empleado.Name = "Empleado";
            this.Empleado.ReadOnly = true;
            this.Empleado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Empleado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Empleado.Width = 85;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nombre:";
            // 
            // txtBuscarEmpleado
            // 
            this.txtBuscarEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarEmpleado.Location = new System.Drawing.Point(73, 28);
            this.txtBuscarEmpleado.Name = "txtBuscarEmpleado";
            this.txtBuscarEmpleado.Size = new System.Drawing.Size(258, 20);
            this.txtBuscarEmpleado.TabIndex = 13;
            this.txtBuscarEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarEmpleado_KeyPress);
            // 
            // lstOT
            // 
            this.lstOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOT.ForeColor = System.Drawing.Color.Navy;
            this.lstOT.FullRowSelect = true;
            this.lstOT.GridLines = true;
            this.lstOT.Location = new System.Drawing.Point(101, 67);
            this.lstOT.MultiSelect = false;
            this.lstOT.Name = "lstOT";
            this.lstOT.Size = new System.Drawing.Size(128, 103);
            this.lstOT.TabIndex = 106;
            this.lstOT.UseCompatibleStateImageBehavior = false;
            this.lstOT.View = System.Windows.Forms.View.Details;
            this.lstOT.Visible = false;
            this.lstOT.Enter += new System.EventHandler(this.lstOT_Enter);
            this.lstOT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstOT_KeyPress);
            this.lstOT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstOT_MouseDoubleClick);
            // 
            // txtOT
            // 
            this.txtOT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOT.Location = new System.Drawing.Point(101, 48);
            this.txtOT.Name = "txtOT";
            this.txtOT.Size = new System.Drawing.Size(108, 20);
            this.txtOT.TabIndex = 105;
            this.txtOT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOT_KeyPress);
            this.txtOT.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOT_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 104;
            this.label9.Text = "Registrar OT:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.label20.ForeColor = System.Drawing.Color.Turquoise;
            this.label20.Location = new System.Drawing.Point(11, 221);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(129, 16);
            this.label20.TabIndex = 199;
            this.label20.Text = "DETALLE DE OT:";
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
            this.btnGuardar.Location = new System.Drawing.Point(131, 452);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 44);
            this.btnGuardar.TabIndex = 198;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Asignar";
            this.btnGuardar.ToolTip = "Buscar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dgvDetalleOT
            // 
            this.dgvDetalleOT.AllowUserToAddRows = false;
            this.dgvDetalleOT.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalleOT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleOT.Location = new System.Drawing.Point(14, 245);
            this.dgvDetalleOT.MultiSelect = false;
            this.dgvDetalleOT.Name = "dgvDetalleOT";
            this.dgvDetalleOT.ReadOnly = true;
            this.dgvDetalleOT.RowHeadersVisible = false;
            this.dgvDetalleOT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleOT.ShowRowErrors = false;
            this.dgvDetalleOT.Size = new System.Drawing.Size(332, 120);
            this.dgvDetalleOT.TabIndex = 9;
            // 
            // txtCantidadUso
            // 
            this.txtCantidadUso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadUso.Location = new System.Drawing.Point(101, 177);
            this.txtCantidadUso.Name = "txtCantidadUso";
            this.txtCantidadUso.Size = new System.Drawing.Size(102, 20);
            this.txtCantidadUso.TabIndex = 117;
            this.txtCantidadUso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadUso_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 118;
            this.label6.Text = "Cantidad Uso:";
            // 
            // txtVale
            // 
            this.txtVale.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVale.Location = new System.Drawing.Point(101, 134);
            this.txtVale.Name = "txtVale";
            this.txtVale.Size = new System.Drawing.Size(245, 20);
            this.txtVale.TabIndex = 116;
            this.txtVale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVale_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 115;
            this.label10.Text = "Registrar Vale:";
            // 
            // lblplaca
            // 
            this.lblplaca.AutoSize = true;
            this.lblplaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblplaca.Location = new System.Drawing.Point(13, 94);
            this.lblplaca.Name = "lblplaca";
            this.lblplaca.Size = new System.Drawing.Size(37, 13);
            this.lblplaca.TabIndex = 114;
            this.lblplaca.Text = "Placa:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(101, 91);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(108, 20);
            this.txtPlaca.TabIndex = 113;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 112;
            this.label4.Text = "Tipo Operación:";
            // 
            // CheckMantenimiento
            // 
            this.CheckMantenimiento.AutoSize = true;
            this.CheckMantenimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.CheckMantenimiento.Location = new System.Drawing.Point(210, 13);
            this.CheckMantenimiento.Name = "CheckMantenimiento";
            this.CheckMantenimiento.Size = new System.Drawing.Size(114, 20);
            this.CheckMantenimiento.TabIndex = 200;
            this.CheckMantenimiento.Text = "Mantenimiento";
            this.CheckMantenimiento.UseVisualStyleBackColor = true;
            this.CheckMantenimiento.CheckedChanged += new System.EventHandler(this.CheckMantenimiento_CheckedChanged);
            // 
            // chkConsumible
            // 
            this.chkConsumible.AutoSize = true;
            this.chkConsumible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.chkConsumible.Location = new System.Drawing.Point(101, 13);
            this.chkConsumible.Name = "chkConsumible";
            this.chkConsumible.Size = new System.Drawing.Size(98, 20);
            this.chkConsumible.TabIndex = 201;
            this.chkConsumible.Text = "Consumible";
            this.chkConsumible.UseVisualStyleBackColor = true;
            this.chkConsumible.CheckedChanged += new System.EventHandler(this.chkConsumible_CheckedChanged);
            // 
            // tabAsignacion
            // 
            this.tabAsignacion.Controls.Add(this.tabMtto);
            this.tabAsignacion.Controls.Add(this.tabLogistica);
            this.tabAsignacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAsignacion.Location = new System.Drawing.Point(539, 53);
            this.tabAsignacion.Name = "tabAsignacion";
            this.tabAsignacion.SelectedIndex = 0;
            this.tabAsignacion.Size = new System.Drawing.Size(369, 541);
            this.tabAsignacion.TabIndex = 108;
            // 
            // tabMtto
            // 
            this.tabMtto.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabMtto.Controls.Add(this.label20);
            this.tabMtto.Controls.Add(this.chkConsumible);
            this.tabMtto.Controls.Add(this.btnGuardar);
            this.tabMtto.Controls.Add(this.CheckMantenimiento);
            this.tabMtto.Controls.Add(this.dgvDetalleOT);
            this.tabMtto.Controls.Add(this.txtCantidadUso);
            this.tabMtto.Controls.Add(this.label9);
            this.tabMtto.Controls.Add(this.label6);
            this.tabMtto.Controls.Add(this.txtOT);
            this.tabMtto.Controls.Add(this.txtVale);
            this.tabMtto.Controls.Add(this.label4);
            this.tabMtto.Controls.Add(this.label10);
            this.tabMtto.Controls.Add(this.txtPlaca);
            this.tabMtto.Controls.Add(this.lblplaca);
            this.tabMtto.Controls.Add(this.lstOT);
            this.tabMtto.Location = new System.Drawing.Point(4, 25);
            this.tabMtto.Name = "tabMtto";
            this.tabMtto.Padding = new System.Windows.Forms.Padding(3);
            this.tabMtto.Size = new System.Drawing.Size(361, 512);
            this.tabMtto.TabIndex = 0;
            this.tabMtto.Text = "MANTENIMIENTO";
            // 
            // tabLogistica
            // 
            this.tabLogistica.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabLogistica.Controls.Add(this.btnGuardarR);
            this.tabLogistica.Controls.Add(this.txtCantidadUsoR);
            this.tabLogistica.Controls.Add(this.label15);
            this.tabLogistica.Controls.Add(this.label14);
            this.tabLogistica.Controls.Add(this.txtDescripcionR);
            this.tabLogistica.Controls.Add(this.label13);
            this.tabLogistica.Controls.Add(this.dgvDetalleReq);
            this.tabLogistica.Controls.Add(this.label12);
            this.tabLogistica.Controls.Add(this.dtpFechaR);
            this.tabLogistica.Controls.Add(this.label8);
            this.tabLogistica.Controls.Add(this.label7);
            this.tabLogistica.Controls.Add(this.lstRequerimiento);
            this.tabLogistica.Controls.Add(this.label5);
            this.tabLogistica.Controls.Add(this.txtRequerimiento);
            this.tabLogistica.Controls.Add(this.txtSolicitado);
            this.tabLogistica.Controls.Add(this.txtCentroCosto);
            this.tabLogistica.Location = new System.Drawing.Point(4, 25);
            this.tabLogistica.Name = "tabLogistica";
            this.tabLogistica.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogistica.Size = new System.Drawing.Size(361, 512);
            this.tabLogistica.TabIndex = 1;
            this.tabLogistica.Text = "LOGÍSTICA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 107;
            this.label5.Text = "Requerimiento:";
            // 
            // txtRequerimiento
            // 
            this.txtRequerimiento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRequerimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequerimiento.Location = new System.Drawing.Point(103, 13);
            this.txtRequerimiento.Name = "txtRequerimiento";
            this.txtRequerimiento.Size = new System.Drawing.Size(104, 20);
            this.txtRequerimiento.TabIndex = 108;
            this.txtRequerimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRequerimiento_KeyPress);
            this.txtRequerimiento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRequerimiento_KeyUp);
            // 
            // lstRequerimiento
            // 
            this.lstRequerimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstRequerimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRequerimiento.ForeColor = System.Drawing.Color.Navy;
            this.lstRequerimiento.FullRowSelect = true;
            this.lstRequerimiento.GridLines = true;
            this.lstRequerimiento.Location = new System.Drawing.Point(103, 32);
            this.lstRequerimiento.MultiSelect = false;
            this.lstRequerimiento.Name = "lstRequerimiento";
            this.lstRequerimiento.Size = new System.Drawing.Size(123, 103);
            this.lstRequerimiento.TabIndex = 109;
            this.lstRequerimiento.UseCompatibleStateImageBehavior = false;
            this.lstRequerimiento.View = System.Windows.Forms.View.Details;
            this.lstRequerimiento.Visible = false;
            this.lstRequerimiento.Enter += new System.EventHandler(this.lstRequerimiento_Enter);
            this.lstRequerimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRequerimiento_KeyPress);
            this.lstRequerimiento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRequerimiento_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 117;
            this.label7.Text = "Fecha:";
            // 
            // txtDescripcionR
            // 
            this.txtDescripcionR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcionR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionR.Location = new System.Drawing.Point(103, 90);
            this.txtDescripcionR.Multiline = true;
            this.txtDescripcionR.Name = "txtDescripcionR";
            this.txtDescripcionR.ReadOnly = true;
            this.txtDescripcionR.Size = new System.Drawing.Size(245, 46);
            this.txtDescripcionR.TabIndex = 120;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 119;
            this.label8.Text = "Descripción:";
            // 
            // dtpFechaR
            // 
            this.dtpFechaR.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFechaR.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaR.Enabled = false;
            this.dtpFechaR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaR.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaR.Location = new System.Drawing.Point(103, 51);
            this.dtpFechaR.Name = "dtpFechaR";
            this.dtpFechaR.Size = new System.Drawing.Size(102, 21);
            this.dtpFechaR.TabIndex = 224;
            // 
            // txtCentroCosto
            // 
            this.txtCentroCosto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCentroCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCentroCosto.Location = new System.Drawing.Point(103, 213);
            this.txtCentroCosto.Name = "txtCentroCosto";
            this.txtCentroCosto.ReadOnly = true;
            this.txtCentroCosto.Size = new System.Drawing.Size(245, 20);
            this.txtCentroCosto.TabIndex = 228;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 216);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 227;
            this.label12.Text = "Centro Costo:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.Turquoise;
            this.label13.Location = new System.Drawing.Point(11, 287);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 16);
            this.label13.TabIndex = 231;
            this.label13.Text = "DETALLE:";
            // 
            // dgvDetalleReq
            // 
            this.dgvDetalleReq.AllowUserToAddRows = false;
            this.dgvDetalleReq.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalleReq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleReq.Location = new System.Drawing.Point(14, 311);
            this.dgvDetalleReq.MultiSelect = false;
            this.dgvDetalleReq.Name = "dgvDetalleReq";
            this.dgvDetalleReq.ReadOnly = true;
            this.dgvDetalleReq.RowHeadersVisible = false;
            this.dgvDetalleReq.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleReq.ShowRowErrors = false;
            this.dgvDetalleReq.Size = new System.Drawing.Size(334, 120);
            this.dgvDetalleReq.TabIndex = 229;
            // 
            // txtSolicitado
            // 
            this.txtSolicitado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSolicitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSolicitado.Location = new System.Drawing.Point(103, 154);
            this.txtSolicitado.Multiline = true;
            this.txtSolicitado.Name = "txtSolicitado";
            this.txtSolicitado.ReadOnly = true;
            this.txtSolicitado.Size = new System.Drawing.Size(245, 41);
            this.txtSolicitado.TabIndex = 233;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 234;
            this.label14.Text = "Solicitada por:";
            // 
            // txtCantidadUsoR
            // 
            this.txtCantidadUsoR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadUsoR.Location = new System.Drawing.Point(103, 251);
            this.txtCantidadUsoR.Name = "txtCantidadUsoR";
            this.txtCantidadUsoR.Size = new System.Drawing.Size(102, 20);
            this.txtCantidadUsoR.TabIndex = 236;
            this.txtCantidadUsoR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadUsoR_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 255);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 13);
            this.label15.TabIndex = 237;
            this.label15.Text = "Cantidad Uso:";
            // 
            // btnGuardarR
            // 
            this.btnGuardarR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardarR.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardarR.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardarR.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardarR.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarR.Appearance.Options.UseBackColor = true;
            this.btnGuardarR.Appearance.Options.UseBorderColor = true;
            this.btnGuardarR.Appearance.Options.UseFont = true;
            this.btnGuardarR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarR.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarR.Image")));
            this.btnGuardarR.Location = new System.Drawing.Point(131, 452);
            this.btnGuardarR.Name = "btnGuardarR";
            this.btnGuardarR.Size = new System.Drawing.Size(100, 44);
            this.btnGuardarR.TabIndex = 238;
            this.btnGuardarR.Tag = "5";
            this.btnGuardarR.Text = "Asignar";
            this.btnGuardarR.ToolTip = "Buscar";
            this.btnGuardarR.Click += new System.EventHandler(this.btnGuardarR_Click);
            // 
            // frmAsignarActivoSegundoUso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(923, 606);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabAsignacion);
            this.MaximizeBox = false;
            this.Name = "frmAsignarActivoSegundoUso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASIGNAR ACTIVO - SEGUNDO USO";
            this.Load += new System.EventHandler(this.frmAsignarActivoSegundoUso_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSegundoUso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivosSegundoUso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleOT)).EndInit();
            this.tabAsignacion.ResumeLayout(false);
            this.tabMtto.ResumeLayout(false);
            this.tabMtto.PerformLayout();
            this.tabLogistica.ResumeLayout(false);
            this.tabLogistica.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleReq)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBuscarSegundoUso;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnExcelSegundoUso;
        private System.Windows.Forms.DataGridView dgvSegundoUso;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn UniMedidaActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaRegistroActivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuarioRegistroActivo;
        private DevExpress.XtraGrid.GridControl dgvActivosSegundoUso;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuscarEmpleado;
        private System.Windows.Forms.DataGridView dgvEmpleado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empleado;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbxSucursal;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ListView lstOT;
        private System.Windows.Forms.TextBox txtOT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblplaca;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.TextBox txtVale;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCantidadUso;
        private System.Windows.Forms.Label label6;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.DataGridView dgvDetalleOT;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox CheckMantenimiento;
        private System.Windows.Forms.CheckBox chkConsumible;
        private System.Windows.Forms.TabControl tabAsignacion;
        private System.Windows.Forms.TabPage tabLogistica;
        private System.Windows.Forms.TabPage tabMtto;
        private System.Windows.Forms.TextBox txtDescripcionR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lstRequerimiento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRequerimiento;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dgvDetalleReq;
        private System.Windows.Forms.TextBox txtCentroCosto;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.DateTimePicker dtpFechaR;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSolicitado;
        public DevExpress.XtraEditors.SimpleButton btnGuardarR;
        private System.Windows.Forms.TextBox txtCantidadUsoR;
        private System.Windows.Forms.Label label15;
    }
}