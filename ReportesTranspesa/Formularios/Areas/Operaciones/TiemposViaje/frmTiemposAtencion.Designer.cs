namespace ReportesTranspesa.Formularios.Areas.Operaciones.TiemposViaje
{
    partial class frmTiemposAtencion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTiemposAtencion));
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHorarioS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpTiempoAtencion = new System.Windows.Forms.DateTimePicker();
            this.txtHorarioLV = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvRutas = new DevExpress.XtraGrid.GridControl();
            this.dgvRutasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dtgTiempoAtencion = new DevExpress.XtraGrid.GridControl();
            this.dgvTiempoAtencionVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBuscarDestino = new System.Windows.Forms.TextBox();
            this.btnQuitarTiempos = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutasView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiempoAtencion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiempoAtencionVista)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SaddleBrown;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1160, 42);
            this.label2.TabIndex = 22;
            this.label2.Text = "TIEMPOS DE ATENCIÓN POR VIAJE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.cbxOperacion);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.txtHorarioS);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.dtpTiempoAtencion);
            this.panel4.Controls.Add(this.txtHorarioLV);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dgvRutas);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.txtDestino);
            this.panel4.Controls.Add(this.txtRuta);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(484, 514);
            this.panel4.TabIndex = 135;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label9.Location = new System.Drawing.Point(247, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 244;
            this.label9.Text = "Operación:";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Location = new System.Drawing.Point(320, 182);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(142, 23);
            this.cbxOperacion.TabIndex = 243;
            this.cbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cbxOperacion_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(14, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 15);
            this.label7.TabIndex = 241;
            this.label7.Text = "T. Atención Promedio:";
            // 
            // txtHorarioS
            // 
            this.txtHorarioS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtHorarioS.Location = new System.Drawing.Point(117, 131);
            this.txtHorarioS.Multiline = true;
            this.txtHorarioS.Name = "txtHorarioS";
            this.txtHorarioS.Size = new System.Drawing.Size(345, 37);
            this.txtHorarioS.TabIndex = 239;
            this.txtHorarioS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHorarioS_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(14, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 30);
            this.label6.TabIndex = 240;
            this.label6.Text = "Horario\r\n(Sábado):";
            // 
            // dtpTiempoAtencion
            // 
            this.dtpTiempoAtencion.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.dtpTiempoAtencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpTiempoAtencion.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTiempoAtencion.Location = new System.Drawing.Point(147, 182);
            this.dtpTiempoAtencion.Name = "dtpTiempoAtencion";
            this.dtpTiempoAtencion.Size = new System.Drawing.Size(75, 21);
            this.dtpTiempoAtencion.TabIndex = 238;
            this.dtpTiempoAtencion.Value = new System.DateTime(2023, 5, 27, 0, 0, 0, 0);
            this.dtpTiempoAtencion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpTiempoAtencion_KeyPress);
            // 
            // txtHorarioLV
            // 
            this.txtHorarioLV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtHorarioLV.Location = new System.Drawing.Point(117, 80);
            this.txtHorarioLV.Multiline = true;
            this.txtHorarioLV.Name = "txtHorarioLV";
            this.txtHorarioLV.Size = new System.Drawing.Size(345, 37);
            this.txtHorarioLV.TabIndex = 127;
            this.txtHorarioLV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHorarioLV_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(14, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 30);
            this.label4.TabIndex = 128;
            this.label4.Text = "Horario\r\n(Lunes-Viernes):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(14, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 122;
            this.label1.Text = "Destino:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label5.Location = new System.Drawing.Point(13, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(245, 20);
            this.label5.TabIndex = 133;
            this.label5.Text = "Ingresar Tiempo de Atención:";
            // 
            // dgvRutas
            // 
            this.dgvRutas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvRutas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvRutas.Location = new System.Drawing.Point(0, 253);
            this.dgvRutas.LookAndFeel.SkinName = "Black";
            this.dgvRutas.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dgvRutas.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgvRutas.MainView = this.dgvRutasView;
            this.dgvRutas.Name = "dgvRutas";
            this.dgvRutas.Size = new System.Drawing.Size(484, 205);
            this.dgvRutas.TabIndex = 131;
            this.dgvRutas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRutasView});
            // 
            // dgvRutasView
            // 
            this.dgvRutasView.GridControl = this.dgvRutas;
            this.dgvRutasView.Name = "dgvRutasView";
            this.dgvRutasView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvRutasView.OptionsBehavior.Editable = false;
            this.dgvRutasView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvRutasView.OptionsSelection.MultiSelect = true;
            this.dgvRutasView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvRutasView.OptionsView.ColumnAutoWidth = false;
            this.dgvRutasView.OptionsView.ShowFooter = true;
            this.dgvRutasView.OptionsView.ShowGroupPanel = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.btnGuardar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 458);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(484, 56);
            this.panel2.TabIndex = 242;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(137, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(96, 36);
            this.btnCancelar.TabIndex = 119;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(252, 10);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(96, 36);
            this.btnGuardar.TabIndex = 118;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtDestino
            // 
            this.txtDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDestino.Location = new System.Drawing.Point(72, 45);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(390, 21);
            this.txtDestino.TabIndex = 0;
            this.txtDestino.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestino_KeyPress);
            // 
            // txtRuta
            // 
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtRuta.Location = new System.Drawing.Point(167, 222);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(295, 21);
            this.txtRuta.TabIndex = 61;
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 15);
            this.label3.TabIndex = 123;
            this.label3.Text = "Seleccione las Rutas:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel5.Controls.Add(this.dtgTiempoAtencion);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(484, 42);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(676, 514);
            this.panel5.TabIndex = 242;
            // 
            // dtgTiempoAtencion
            // 
            this.dtgTiempoAtencion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgTiempoAtencion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTiempoAtencion.Location = new System.Drawing.Point(0, 63);
            this.dtgTiempoAtencion.LookAndFeel.SkinMaskColor = System.Drawing.Color.DarkOrange;
            this.dtgTiempoAtencion.LookAndFeel.SkinName = "Money Twins";
            this.dtgTiempoAtencion.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTiempoAtencion.MainView = this.dgvTiempoAtencionVista;
            this.dtgTiempoAtencion.Name = "dtgTiempoAtencion";
            this.dtgTiempoAtencion.Size = new System.Drawing.Size(676, 451);
            this.dtgTiempoAtencion.TabIndex = 185;
            this.dtgTiempoAtencion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTiempoAtencionVista});
            this.dtgTiempoAtencion.DoubleClick += new System.EventHandler(this.dtgTiempoAtencion_DoubleClick);
            // 
            // dgvTiempoAtencionVista
            // 
            this.dgvTiempoAtencionVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTiempoAtencionVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvTiempoAtencionVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTiempoAtencionVista.Appearance.Row.Options.UseFont = true;
            this.dgvTiempoAtencionVista.GridControl = this.dtgTiempoAtencion;
            this.dgvTiempoAtencionVista.Name = "dgvTiempoAtencionVista";
            this.dgvTiempoAtencionVista.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvTiempoAtencionVista.OptionsBehavior.Editable = false;
            this.dgvTiempoAtencionVista.OptionsSelection.MultiSelect = true;
            this.dgvTiempoAtencionVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvTiempoAtencionVista.OptionsView.ColumnAutoWidth = false;
            this.dgvTiempoAtencionVista.OptionsView.ShowFooter = true;
            this.dgvTiempoAtencionVista.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuitarTiempos);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtBuscarDestino);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 63);
            this.panel1.TabIndex = 134;
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
            this.btnExcel.Location = new System.Drawing.Point(530, 13);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(37, 37);
            this.btnExcel.TabIndex = 125;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(481, 13);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(37, 37);
            this.btnBuscar.TabIndex = 120;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.Location = new System.Drawing.Point(15, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 15);
            this.label8.TabIndex = 124;
            this.label8.Text = "Buscar por Destino:";
            // 
            // txtBuscarDestino
            // 
            this.txtBuscarDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtBuscarDestino.Location = new System.Drawing.Point(135, 21);
            this.txtBuscarDestino.Name = "txtBuscarDestino";
            this.txtBuscarDestino.Size = new System.Drawing.Size(317, 21);
            this.txtBuscarDestino.TabIndex = 121;
            // 
            // btnQuitarTiempos
            // 
            this.btnQuitarTiempos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnQuitarTiempos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitarTiempos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarTiempos.Location = new System.Drawing.Point(579, 14);
            this.btnQuitarTiempos.Name = "btnQuitarTiempos";
            this.btnQuitarTiempos.Size = new System.Drawing.Size(70, 36);
            this.btnQuitarTiempos.TabIndex = 232;
            this.btnQuitarTiempos.Text = "Quitar\r\nTiempos";
            this.btnQuitarTiempos.UseVisualStyleBackColor = false;
            this.btnQuitarTiempos.Click += new System.EventHandler(this.btnQuitarTiempos_Click);
            // 
            // frmTiemposAtencion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 556);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.Name = "frmTiemposAtencion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TIEMPOS DE ATENCIÓN";
            this.Load += new System.EventHandler(this.frmTiemposAtencion_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutasView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiempoAtencion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiempoAtencionVista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraGrid.GridControl dgvRutas;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRutasView;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHorarioS;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DateTimePicker dtpTiempoAtencion;
        private System.Windows.Forms.TextBox txtHorarioLV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBuscarDestino;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraGrid.GridControl dtgTiempoAtencion;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTiempoAtencionVista;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxOperacion;
        private System.Windows.Forms.Button btnQuitarTiempos;
    }
}