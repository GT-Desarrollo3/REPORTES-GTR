namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    partial class frmRevisionConosTacos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRevisionConosTacos));
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxTipo = new System.Windows.Forms.ComboBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnBuscarHallazgo = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.pbHallazgo = new System.Windows.Forms.PictureBox();
            this.txtResponsable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLugarRevision = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.dtpFechaRevision = new System.Windows.Forms.DateTimePicker();
            this.txtTracto = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lstTracto = new System.Windows.Forms.ListView();
            this.lstEmpleado = new System.Windows.Forms.ListView();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.btnCerrar2 = new System.Windows.Forms.Button();
            this.btnBuscarReparacion = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pbReparacion = new System.Windows.Forms.PictureBox();
            this.dtpFechaReparacion = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbHallazgo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbReparacion)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(685, 40);
            this.label2.TabIndex = 20;
            this.label2.Text = "REVISIÓN DE INVENTARIOS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.ForeColor = System.Drawing.Color.Black;
            this.txtCantidad.Location = new System.Drawing.Point(537, 71);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(71, 21);
            this.txtCantidad.TabIndex = 271;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "OK",
            "CAMBIO",
            "NO TIENE"});
            this.cbxEstado.Location = new System.Drawing.Point(358, 126);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(113, 23);
            this.cbxEstado.TabIndex = 268;
            this.cbxEstado.DropDownClosed += new System.EventHandler(this.cbxEstado_DropDownClosed);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(355, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 15);
            this.label14.TabIndex = 267;
            this.label14.Text = "Estado:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(534, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 15);
            this.label11.TabIndex = 263;
            this.label11.Text = "Cantidad:";
            // 
            // cbxTipo
            // 
            this.cbxTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipo.FormattingEnabled = true;
            this.cbxTipo.Items.AddRange(new object[] {
            "CONOS",
            "CINTAS LUMINISCENTES",
            "TACOS",
            "CORTINAS",
            "CINTURONES",
            "RADIO",
            "PARLANTES",
            "BOTIQUÍN",
            "STICKER TANQUE COMB.",
            "DUPLICADO"});
            this.cbxTipo.Location = new System.Drawing.Point(289, 70);
            this.cbxTipo.Name = "cbxTipo";
            this.cbxTipo.Size = new System.Drawing.Size(214, 23);
            this.cbxTipo.TabIndex = 260;
            this.cbxTipo.DropDownClosed += new System.EventHandler(this.cbxTipo_DropDownClosed);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(309, 249);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(23, 25);
            this.btnCerrar.TabIndex = 242;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnBuscarHallazgo
            // 
            this.btnBuscarHallazgo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBuscarHallazgo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarHallazgo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnBuscarHallazgo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscarHallazgo.Location = new System.Drawing.Point(168, 220);
            this.btnBuscarHallazgo.Name = "btnBuscarHallazgo";
            this.btnBuscarHallazgo.Size = new System.Drawing.Size(103, 21);
            this.btnBuscarHallazgo.TabIndex = 240;
            this.btnBuscarHallazgo.Text = "Adjuntar imagen";
            this.btnBuscarHallazgo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarHallazgo.UseVisualStyleBackColor = false;
            this.btnBuscarHallazgo.Click += new System.EventHandler(this.btnBuscarHallazgo_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(18, 222);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 15);
            this.label13.TabIndex = 239;
            this.label13.Text = "Imagen de Revisión:";
            // 
            // pbHallazgo
            // 
            this.pbHallazgo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbHallazgo.Location = new System.Drawing.Point(21, 249);
            this.pbHallazgo.Name = "pbHallazgo";
            this.pbHallazgo.Size = new System.Drawing.Size(289, 180);
            this.pbHallazgo.TabIndex = 241;
            this.pbHallazgo.TabStop = false;
            // 
            // txtResponsable
            // 
            this.txtResponsable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResponsable.ForeColor = System.Drawing.Color.Black;
            this.txtResponsable.Location = new System.Drawing.Point(21, 184);
            this.txtResponsable.Name = "txtResponsable";
            this.txtResponsable.Size = new System.Drawing.Size(300, 21);
            this.txtResponsable.TabIndex = 238;
            this.txtResponsable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResponsable_KeyPress);
            this.txtResponsable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtResponsable_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(18, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 237;
            this.label4.Text = "Responsable:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(286, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 15);
            this.label9.TabIndex = 234;
            this.label9.Text = "Tipo:";
            // 
            // txtLugarRevision
            // 
            this.txtLugarRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLugarRevision.ForeColor = System.Drawing.Color.Black;
            this.txtLugarRevision.Location = new System.Drawing.Point(21, 127);
            this.txtLugarRevision.Name = "txtLugarRevision";
            this.txtLugarRevision.Size = new System.Drawing.Size(300, 21);
            this.txtLugarRevision.TabIndex = 226;
            this.txtLugarRevision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLugarRevision_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 231;
            this.label1.Text = "Lugar de Revisión:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(150, 50);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(67, 15);
            this.label34.TabIndex = 224;
            this.label34.Text = "Operación:";
            // 
            // dtpFechaRevision
            // 
            this.dtpFechaRevision.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaRevision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaRevision.Location = new System.Drawing.Point(505, 127);
            this.dtpFechaRevision.Name = "dtpFechaRevision";
            this.dtpFechaRevision.Size = new System.Drawing.Size(103, 21);
            this.dtpFechaRevision.TabIndex = 210;
            this.dtpFechaRevision.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpFechaRevision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaRevision_KeyPress);
            // 
            // txtTracto
            // 
            this.txtTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTracto.ForeColor = System.Drawing.Color.Black;
            this.txtTracto.Location = new System.Drawing.Point(21, 71);
            this.txtTracto.Name = "txtTracto";
            this.txtTracto.Size = new System.Drawing.Size(103, 21);
            this.txtTracto.TabIndex = 209;
            this.txtTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTracto_KeyPress);
            this.txtTracto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTracto_KeyUp);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(502, 105);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(94, 15);
            this.label26.TabIndex = 228;
            this.label26.Text = "Fecha Revisión:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(18, 50);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(50, 15);
            this.label20.TabIndex = 227;
            this.label20.Text = "Unidad:";
            // 
            // lstTracto
            // 
            this.lstTracto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTracto.ForeColor = System.Drawing.Color.Navy;
            this.lstTracto.FullRowSelect = true;
            this.lstTracto.GridLines = true;
            this.lstTracto.Location = new System.Drawing.Point(21, 91);
            this.lstTracto.MultiSelect = false;
            this.lstTracto.Name = "lstTracto";
            this.lstTracto.Size = new System.Drawing.Size(103, 100);
            this.lstTracto.TabIndex = 252;
            this.lstTracto.UseCompatibleStateImageBehavior = false;
            this.lstTracto.View = System.Windows.Forms.View.Details;
            this.lstTracto.Visible = false;
            this.lstTracto.Enter += new System.EventHandler(this.lstTracto_Enter);
            this.lstTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTracto_KeyPress);
            this.lstTracto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTracto_MouseDoubleClick);
            // 
            // lstEmpleado
            // 
            this.lstEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpleado.FullRowSelect = true;
            this.lstEmpleado.GridLines = true;
            this.lstEmpleado.Location = new System.Drawing.Point(21, 204);
            this.lstEmpleado.MultiSelect = false;
            this.lstEmpleado.Name = "lstEmpleado";
            this.lstEmpleado.Size = new System.Drawing.Size(300, 120);
            this.lstEmpleado.TabIndex = 247;
            this.lstEmpleado.UseCompatibleStateImageBehavior = false;
            this.lstEmpleado.View = System.Windows.Forms.View.Details;
            this.lstEmpleado.Visible = false;
            this.lstEmpleado.Enter += new System.EventHandler(this.lstEmpleado_Enter);
            this.lstEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpleado_KeyPress);
            this.lstEmpleado.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEmpleado_MouseDoubleClick);
            // 
            // txtOperacion
            // 
            this.txtOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOperacion.ForeColor = System.Drawing.Color.Black;
            this.txtOperacion.Location = new System.Drawing.Point(153, 71);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(102, 21);
            this.txtOperacion.TabIndex = 276;
            // 
            // btnCerrar2
            // 
            this.btnCerrar2.BackColor = System.Drawing.Color.Red;
            this.btnCerrar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar2.ForeColor = System.Drawing.Color.White;
            this.btnCerrar2.Location = new System.Drawing.Point(647, 249);
            this.btnCerrar2.Name = "btnCerrar2";
            this.btnCerrar2.Size = new System.Drawing.Size(23, 25);
            this.btnCerrar2.TabIndex = 282;
            this.btnCerrar2.Text = "X";
            this.btnCerrar2.UseVisualStyleBackColor = false;
            this.btnCerrar2.Click += new System.EventHandler(this.btnCerrar2_Click);
            // 
            // btnBuscarReparacion
            // 
            this.btnBuscarReparacion.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBuscarReparacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarReparacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnBuscarReparacion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscarReparacion.Location = new System.Drawing.Point(518, 219);
            this.btnBuscarReparacion.Name = "btnBuscarReparacion";
            this.btnBuscarReparacion.Size = new System.Drawing.Size(103, 21);
            this.btnBuscarReparacion.TabIndex = 280;
            this.btnBuscarReparacion.Text = "Adjuntar imagen";
            this.btnBuscarReparacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarReparacion.UseVisualStyleBackColor = false;
            this.btnBuscarReparacion.Click += new System.EventHandler(this.btnBuscarReparacion_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(355, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 15);
            this.label5.TabIndex = 279;
            this.label5.Text = "Imagen de Reparación:";
            // 
            // pbReparacion
            // 
            this.pbReparacion.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbReparacion.Location = new System.Drawing.Point(359, 249);
            this.pbReparacion.Name = "pbReparacion";
            this.pbReparacion.Size = new System.Drawing.Size(289, 180);
            this.pbReparacion.TabIndex = 281;
            this.pbReparacion.TabStop = false;
            // 
            // dtpFechaReparacion
            // 
            this.dtpFechaReparacion.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaReparacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaReparacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaReparacion.Location = new System.Drawing.Point(358, 184);
            this.dtpFechaReparacion.Name = "dtpFechaReparacion";
            this.dtpFechaReparacion.Size = new System.Drawing.Size(102, 21);
            this.dtpFechaReparacion.TabIndex = 277;
            this.dtpFechaReparacion.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            this.dtpFechaReparacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaReparacion_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(355, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 15);
            this.label8.TabIndex = 278;
            this.label8.Text = "Fecha Reparación:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(226, 448);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 37);
            this.btnCancelar.TabIndex = 284;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(355, 448);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(103, 37);
            this.btnAgregar.TabIndex = 283;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // frmRevisionConosTacos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(685, 502);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnCerrar2);
            this.Controls.Add(this.btnBuscarReparacion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbReparacion);
            this.Controls.Add(this.dtpFechaReparacion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pbHallazgo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbxTipo);
            this.Controls.Add(this.btnBuscarHallazgo);
            this.Controls.Add(this.txtOperacion);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtTracto);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.dtpFechaRevision);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLugarRevision);
            this.Controls.Add(this.cbxEstado);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtResponsable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstTracto);
            this.Controls.Add(this.lstEmpleado);
            this.MaximizeBox = false;
            this.Name = "frmRevisionConosTacos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DATOS DE REVISIÓN";
            this.Load += new System.EventHandler(this.frmRevisionConosTacos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbHallazgo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbReparacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCantidad;
        public System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox cbxTipo;
        public System.Windows.Forms.Button btnCerrar;
        public System.Windows.Forms.Button btnBuscarHallazgo;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.PictureBox pbHallazgo;
        public System.Windows.Forms.TextBox txtResponsable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtLugarRevision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.DateTimePicker dtpFechaRevision;
        public System.Windows.Forms.TextBox txtTracto;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ListView lstTracto;
        private System.Windows.Forms.ListView lstEmpleado;
        public System.Windows.Forms.TextBox txtOperacion;
        public System.Windows.Forms.Button btnCerrar2;
        public System.Windows.Forms.Button btnBuscarReparacion;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.PictureBox pbReparacion;
        public System.Windows.Forms.DateTimePicker dtpFechaReparacion;
        private System.Windows.Forms.Label label8;
        public DevExpress.XtraEditors.SimpleButton btnCancelar;
        public DevExpress.XtraEditors.SimpleButton btnAgregar;
    }
}