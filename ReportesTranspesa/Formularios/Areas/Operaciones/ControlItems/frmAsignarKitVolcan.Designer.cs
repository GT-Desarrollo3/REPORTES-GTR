namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    partial class frmAsignarKitVolcan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarKitVolcan));
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCarreta = new System.Windows.Forms.TextBox();
            this.txtTracto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAsignar = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtgImplementos = new DevExpress.XtraGrid.GridControl();
            this.dgvImplementosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsHttasD = new System.Windows.Forms.ToolStripLabel();
            this.lstTracto = new System.Windows.Forms.ListView();
            this.btnEliminarImp = new System.Windows.Forms.Button();
            this.btnNuevoImp = new System.Windows.Forms.Button();
            this.pNuevoImplemento = new System.Windows.Forms.Panel();
            this.cbxCategoria = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblInterno = new System.Windows.Forms.Label();
            this.txtCodMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.txtCodAlmacen = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodInterno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.lstItems = new System.Windows.Forms.ListView();
            this.lstEmpleado = new System.Windows.Forms.ListView();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImplementos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImplementosView)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.pNuevoImplemento.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LimeGreen;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(525, 40);
            this.label2.TabIndex = 22;
            this.label2.Text = "ASIGNACIÓN DE KIT DE VOLCAN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtCarreta);
            this.panel2.Controls.Add(this.txtTracto);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnAsignar);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtEmpleado);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(525, 87);
            this.panel2.TabIndex = 235;
            // 
            // txtCarreta
            // 
            this.txtCarreta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCarreta.Location = new System.Drawing.Point(301, 50);
            this.txtCarreta.Name = "txtCarreta";
            this.txtCarreta.Size = new System.Drawing.Size(90, 21);
            this.txtCarreta.TabIndex = 102;
            this.txtCarreta.Visible = false;
            this.txtCarreta.Enter += new System.EventHandler(this.txtCarreta_Enter);
            this.txtCarreta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarreta_KeyPress);
            this.txtCarreta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCarreta_KeyUp);
            this.txtCarreta.Leave += new System.EventHandler(this.txtCarreta_Leave);
            // 
            // txtTracto
            // 
            this.txtTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTracto.Location = new System.Drawing.Point(85, 50);
            this.txtTracto.Name = "txtTracto";
            this.txtTracto.Size = new System.Drawing.Size(90, 21);
            this.txtTracto.TabIndex = 104;
            this.txtTracto.Enter += new System.EventHandler(this.txtTracto_Enter);
            this.txtTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTracto_KeyPress);
            this.txtTracto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTracto_KeyUp);
            this.txtTracto.Leave += new System.EventHandler(this.txtTracto_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(35, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 232;
            this.label1.Text = "Tracto:";
            // 
            // btnAsignar
            // 
            this.btnAsignar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAsignar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignar.Appearance.Options.UseBackColor = true;
            this.btnAsignar.Appearance.Options.UseBorderColor = true;
            this.btnAsignar.Appearance.Options.UseFont = true;
            this.btnAsignar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignar.Image")));
            this.btnAsignar.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnAsignar.Location = new System.Drawing.Point(448, 14);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(59, 58);
            this.btnAsignar.TabIndex = 100;
            this.btnAsignar.Tag = "5";
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.ToolTip = "Asignar";
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(199, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 15);
            this.label3.TabIndex = 234;
            this.label3.Text = "Semirremolque:";
            this.label3.Visible = false;
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEmpleado.Location = new System.Drawing.Point(85, 17);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(342, 21);
            this.txtEmpleado.TabIndex = 235;
            this.txtEmpleado.Enter += new System.EventHandler(this.txtEmpleado_Enter);
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            this.txtEmpleado.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmpleado_KeyUp);
            this.txtEmpleado.Leave += new System.EventHandler(this.txtEmpleado_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.Location = new System.Drawing.Point(12, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 236;
            this.label8.Text = "Empleado:";
            // 
            // dtgImplementos
            // 
            this.dtgImplementos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgImplementos.Location = new System.Drawing.Point(0, 152);
            this.dtgImplementos.LookAndFeel.SkinName = "Black";
            this.dtgImplementos.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgImplementos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgImplementos.MainView = this.dgvImplementosView;
            this.dtgImplementos.Name = "dtgImplementos";
            this.dtgImplementos.Size = new System.Drawing.Size(525, 272);
            this.dtgImplementos.TabIndex = 237;
            this.dtgImplementos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvImplementosView});
            // 
            // dgvImplementosView
            // 
            this.dgvImplementosView.GridControl = this.dtgImplementos;
            this.dgvImplementosView.Name = "dgvImplementosView";
            this.dgvImplementosView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvImplementosView.OptionsBehavior.Editable = false;
            this.dgvImplementosView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvImplementosView.OptionsSelection.MultiSelect = true;
            this.dgvImplementosView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvImplementosView.OptionsView.ColumnAutoWidth = false;
            this.dgvImplementosView.OptionsView.ShowFooter = true;
            this.dgvImplementosView.OptionsView.ShowGroupPanel = false;
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.DarkGreen;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsHttasD});
            this.toolStrip3.Location = new System.Drawing.Point(0, 127);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(525, 25);
            this.toolStrip3.TabIndex = 236;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsHttasD
            // 
            this.tsHttasD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsHttasD.ForeColor = System.Drawing.Color.White;
            this.tsHttasD.Name = "tsHttasD";
            this.tsHttasD.Size = new System.Drawing.Size(148, 22);
            this.tsHttasD.Text = "Implementos disponibles:";
            // 
            // lstTracto
            // 
            this.lstTracto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTracto.ForeColor = System.Drawing.Color.Navy;
            this.lstTracto.FullRowSelect = true;
            this.lstTracto.GridLines = true;
            this.lstTracto.Location = new System.Drawing.Point(85, 110);
            this.lstTracto.MultiSelect = false;
            this.lstTracto.Name = "lstTracto";
            this.lstTracto.Size = new System.Drawing.Size(210, 144);
            this.lstTracto.TabIndex = 238;
            this.lstTracto.UseCompatibleStateImageBehavior = false;
            this.lstTracto.View = System.Windows.Forms.View.Details;
            this.lstTracto.Visible = false;
            this.lstTracto.Enter += new System.EventHandler(this.lstTracto_Enter);
            this.lstTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTracto_KeyPress);
            this.lstTracto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTracto_MouseDoubleClick);
            // 
            // btnEliminarImp
            // 
            this.btnEliminarImp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarImp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEliminarImp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarImp.Location = new System.Drawing.Point(455, 128);
            this.btnEliminarImp.Name = "btnEliminarImp";
            this.btnEliminarImp.Size = new System.Drawing.Size(64, 22);
            this.btnEliminarImp.TabIndex = 240;
            this.btnEliminarImp.Text = "Eliminar";
            this.btnEliminarImp.UseVisualStyleBackColor = false;
            this.btnEliminarImp.Click += new System.EventHandler(this.btnEliminarImp_Click);
            // 
            // btnNuevoImp
            // 
            this.btnNuevoImp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevoImp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnNuevoImp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoImp.Location = new System.Drawing.Point(385, 128);
            this.btnNuevoImp.Name = "btnNuevoImp";
            this.btnNuevoImp.Size = new System.Drawing.Size(64, 22);
            this.btnNuevoImp.TabIndex = 239;
            this.btnNuevoImp.Text = "Añadir";
            this.btnNuevoImp.UseVisualStyleBackColor = false;
            this.btnNuevoImp.Click += new System.EventHandler(this.btnNuevoImp_Click);
            // 
            // pNuevoImplemento
            // 
            this.pNuevoImplemento.BackColor = System.Drawing.Color.LemonChiffon;
            this.pNuevoImplemento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNuevoImplemento.Controls.Add(this.cbxCategoria);
            this.pNuevoImplemento.Controls.Add(this.label13);
            this.pNuevoImplemento.Controls.Add(this.lblInterno);
            this.pNuevoImplemento.Controls.Add(this.txtCodMax);
            this.pNuevoImplemento.Controls.Add(this.label6);
            this.pNuevoImplemento.Controls.Add(this.txtItem);
            this.pNuevoImplemento.Controls.Add(this.txtCodAlmacen);
            this.pNuevoImplemento.Controls.Add(this.label5);
            this.pNuevoImplemento.Controls.Add(this.txtCodInterno);
            this.pNuevoImplemento.Controls.Add(this.label4);
            this.pNuevoImplemento.Controls.Add(this.btnCerrar);
            this.pNuevoImplemento.Controls.Add(this.label44);
            this.pNuevoImplemento.Controls.Add(this.btnGuardar);
            this.pNuevoImplemento.Controls.Add(this.lstItems);
            this.pNuevoImplemento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pNuevoImplemento.Location = new System.Drawing.Point(100, 131);
            this.pNuevoImplemento.Name = "pNuevoImplemento";
            this.pNuevoImplemento.Size = new System.Drawing.Size(327, 270);
            this.pNuevoImplemento.TabIndex = 241;
            this.pNuevoImplemento.Visible = false;
            this.pNuevoImplemento.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pNuevoImplemento_MouseMove);
            // 
            // cbxCategoria
            // 
            this.cbxCategoria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCategoria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategoria.FormattingEnabled = true;
            this.cbxCategoria.Location = new System.Drawing.Point(80, 178);
            this.cbxCategoria.Name = "cbxCategoria";
            this.cbxCategoria.Size = new System.Drawing.Size(231, 23);
            this.cbxCategoria.TabIndex = 253;
            this.cbxCategoria.SelectedIndexChanged += new System.EventHandler(this.cbxCategoria_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(11, 182);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 15);
            this.label13.TabIndex = 252;
            this.label13.Text = "Categoría:";
            // 
            // lblInterno
            // 
            this.lblInterno.AutoSize = true;
            this.lblInterno.Location = new System.Drawing.Point(170, 136);
            this.lblInterno.Name = "lblInterno";
            this.lblInterno.Size = new System.Drawing.Size(71, 30);
            this.lblInterno.TabIndex = 251;
            this.lblInterno.Text = "Último Cod.\r\nInterno:";
            // 
            // txtCodMax
            // 
            this.txtCodMax.Location = new System.Drawing.Point(247, 142);
            this.txtCodMax.Name = "txtCodMax";
            this.txtCodMax.ReadOnly = true;
            this.txtCodMax.Size = new System.Drawing.Size(64, 21);
            this.txtCodMax.TabIndex = 250;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(11, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 249;
            this.label6.Text = "Ítem:";
            // 
            // txtItem
            // 
            this.txtItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem.Location = new System.Drawing.Point(14, 89);
            this.txtItem.Multiline = true;
            this.txtItem.Name = "txtItem";
            this.txtItem.ReadOnly = true;
            this.txtItem.Size = new System.Drawing.Size(297, 34);
            this.txtItem.TabIndex = 232;
            this.txtItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImplemento_KeyPress);
            // 
            // txtCodAlmacen
            // 
            this.txtCodAlmacen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCodAlmacen.Location = new System.Drawing.Point(103, 42);
            this.txtCodAlmacen.Name = "txtCodAlmacen";
            this.txtCodAlmacen.Size = new System.Drawing.Size(120, 21);
            this.txtCodAlmacen.TabIndex = 240;
            this.txtCodAlmacen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodAlmacen_KeyPress);
            this.txtCodAlmacen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodAlmacen_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(11, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 239;
            this.label5.Text = "Cod. Almacén:";
            // 
            // txtCodInterno
            // 
            this.txtCodInterno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCodInterno.Location = new System.Drawing.Point(65, 141);
            this.txtCodInterno.Name = "txtCodInterno";
            this.txtCodInterno.Size = new System.Drawing.Size(64, 21);
            this.txtCodInterno.TabIndex = 237;
            this.txtCodInterno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodInterno_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(11, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 30);
            this.label4.TabIndex = 238;
            this.label4.Text = "Código\r\nInterno:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(302, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 235;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label44.ForeColor = System.Drawing.Color.LimeGreen;
            this.label44.Location = new System.Drawing.Point(11, 12);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(180, 18);
            this.label44.TabIndex = 226;
            this.label44.Text = "NUEVO IMPLEMENTO";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(116, 219);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(93, 37);
            this.btnGuardar.TabIndex = 222;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lstItems
            // 
            this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItems.ForeColor = System.Drawing.Color.Navy;
            this.lstItems.FullRowSelect = true;
            this.lstItems.GridLines = true;
            this.lstItems.Location = new System.Drawing.Point(14, 62);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(297, 139);
            this.lstItems.TabIndex = 248;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.View = System.Windows.Forms.View.Details;
            this.lstItems.Visible = false;
            this.lstItems.Enter += new System.EventHandler(this.lstItems_Enter);
            this.lstItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstItems_KeyPress);
            this.lstItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDoubleClick);
            // 
            // lstEmpleado
            // 
            this.lstEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpleado.FullRowSelect = true;
            this.lstEmpleado.GridLines = true;
            this.lstEmpleado.Location = new System.Drawing.Point(85, 77);
            this.lstEmpleado.MultiSelect = false;
            this.lstEmpleado.Name = "lstEmpleado";
            this.lstEmpleado.Size = new System.Drawing.Size(342, 144);
            this.lstEmpleado.TabIndex = 237;
            this.lstEmpleado.UseCompatibleStateImageBehavior = false;
            this.lstEmpleado.View = System.Windows.Forms.View.Details;
            this.lstEmpleado.Visible = false;
            this.lstEmpleado.Enter += new System.EventHandler(this.lstEmpleado_Enter);
            this.lstEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpleado_KeyPress);
            this.lstEmpleado.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEmpleado_MouseDoubleClick);
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
            this.btnExcel.Location = new System.Drawing.Point(358, 129);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(22, 20);
            this.btnExcel.TabIndex = 242;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // frmAsignarKitVolcan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(525, 424);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnEliminarImp);
            this.Controls.Add(this.btnNuevoImp);
            this.Controls.Add(this.dtgImplementos);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstEmpleado);
            this.Controls.Add(this.lstTracto);
            this.Controls.Add(this.pNuevoImplemento);
            this.MaximizeBox = false;
            this.Name = "frmAsignarKitVolcan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASIGNAR KIT VOLCAN";
            this.Load += new System.EventHandler(this.frmAsignarKitVolcan_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImplementos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImplementosView)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.pNuevoImplemento.ResumeLayout(false);
            this.pNuevoImplemento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTracto;
        private System.Windows.Forms.Label label1;
        public DevExpress.XtraEditors.SimpleButton btnAsignar;
        private System.Windows.Forms.TextBox txtCarreta;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl dtgImplementos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvImplementosView;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel tsHttasD;
        private System.Windows.Forms.ListView lstTracto;
        private System.Windows.Forms.Button btnEliminarImp;
        private System.Windows.Forms.Button btnNuevoImp;
        private System.Windows.Forms.Panel pNuevoImplemento;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label44;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.TextBox txtCodAlmacen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodInterno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lstItems;
        private System.Windows.Forms.Label lblInterno;
        private System.Windows.Forms.TextBox txtCodMax;
        public System.Windows.Forms.ComboBox cbxCategoria;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView lstEmpleado;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        public System.Windows.Forms.Label label2;
    }
}