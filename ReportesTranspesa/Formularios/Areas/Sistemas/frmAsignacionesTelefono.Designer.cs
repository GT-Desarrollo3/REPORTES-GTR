namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    partial class frmAsignacionesTelefono
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignacionesTelefono));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblFecha = new MetroFramework.Controls.MetroLabel();
            this.dtpFecha = new MetroFramework.Controls.MetroDateTime();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.lblNumero = new MetroFramework.Controls.MetroLabel();
            this.txtNumero = new MetroFramework.Controls.MetroTextBox();
            this.txtIdCelular = new MetroFramework.Controls.MetroTextBox();
            this.txtObservaciones = new MetroFramework.Controls.MetroTextBox();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.lblObservaciones = new MetroFramework.Controls.MetroLabel();
            this.txtIdEmpleado = new MetroFramework.Controls.MetroTextBox();
            this.txtEmpleado = new MetroFramework.Controls.MetroTextBox();
            this.lblEmpleado = new MetroFramework.Controls.MetroLabel();
            this.lvEmpleado = new System.Windows.Forms.ListView();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtMarca = new MetroFramework.Controls.MetroTextBox();
            this.txtModelo = new MetroFramework.Controls.MetroTextBox();
            this.txtIMEI = new MetroFramework.Controls.MetroTextBox();
            this.lblIMEI = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnModificar);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainer1.Panel1.Controls.Add(this.txtIdCelular);
            this.splitContainer1.Panel1.Controls.Add(this.txtObservaciones);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.lblObservaciones);
            this.splitContainer1.Panel1.Controls.Add(this.txtIdEmpleado);
            this.splitContainer1.Panel1.Controls.Add(this.txtEmpleado);
            this.splitContainer1.Panel1.Controls.Add(this.lblEmpleado);
            this.splitContainer1.Panel1.Controls.Add(this.lvEmpleado);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(1144, 557);
            this.splitContainer1.SplitterDistance = 75;
            this.splitContainer1.TabIndex = 32;
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(462, 37);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(74, 30);
            this.btnModificar.TabIndex = 101;
            this.btnModificar.Text = "Ver";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.Silver;
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(542, 35);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 34);
            this.btnBuscar.TabIndex = 102;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnCancelar);
            this.panelControl1.Controls.Add(this.btnEliminar);
            this.panelControl1.Controls.Add(this.btnGuardar);
            this.panelControl1.Controls.Add(this.lblFecha);
            this.panelControl1.Controls.Add(this.dtpFecha);
            this.panelControl1.Controls.Add(this.btnNuevo);
            this.panelControl1.Controls.Add(this.lblNumero);
            this.panelControl1.Controls.Add(this.txtNumero);
            this.panelControl1.Location = new System.Drawing.Point(729, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(414, 73);
            this.panelControl1.TabIndex = 105;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(322, 40);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 103;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(176, 38);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 25);
            this.btnEliminar.TabIndex = 104;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Visible = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(322, 11);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 102;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFecha.Location = new System.Drawing.Point(14, 34);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(47, 19);
            this.lblFecha.Style = MetroFramework.MetroColorStyle.Red;
            this.lblFecha.TabIndex = 25;
            this.lblFecha.Text = "Fecha:";
            this.lblFecha.Theme = MetroFramework.MetroThemeStyle.Light;
            this.lblFecha.Visible = false;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(68, 34);
            this.dtpFecha.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(102, 29);
            this.dtpFecha.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFecha.TabIndex = 24;
            this.dtpFecha.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtpFecha.Visible = false;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(176, 9);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 100;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Visible = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(4, 3);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(58, 19);
            this.lblNumero.Style = MetroFramework.MetroColorStyle.Red;
            this.lblNumero.TabIndex = 25;
            this.lblNumero.Text = "Persona:";
            this.lblNumero.Theme = MetroFramework.MetroThemeStyle.Light;
            this.lblNumero.Visible = false;
            // 
            // txtNumero
            // 
            this.txtNumero.Lines = new string[0];
            this.txtNumero.Location = new System.Drawing.Point(68, 3);
            this.txtNumero.MaxLength = 32767;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.PasswordChar = '\0';
            this.txtNumero.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumero.SelectedText = "";
            this.txtNumero.Size = new System.Drawing.Size(102, 29);
            this.txtNumero.Style = MetroFramework.MetroColorStyle.Red;
            this.txtNumero.TabIndex = 2;
            this.txtNumero.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNumero.UseSelectable = true;
            this.txtNumero.Visible = false;
            this.txtNumero.Click += new System.EventHandler(this.txtNumero_Click);
            // 
            // txtIdCelular
            // 
            this.txtIdCelular.Lines = new string[0];
            this.txtIdCelular.Location = new System.Drawing.Point(416, 38);
            this.txtIdCelular.MaxLength = 32767;
            this.txtIdCelular.Name = "txtIdCelular";
            this.txtIdCelular.PasswordChar = '\0';
            this.txtIdCelular.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtIdCelular.SelectedText = "";
            this.txtIdCelular.Size = new System.Drawing.Size(31, 29);
            this.txtIdCelular.Style = MetroFramework.MetroColorStyle.Red;
            this.txtIdCelular.TabIndex = 101;
            this.txtIdCelular.UseSelectable = true;
            this.txtIdCelular.Visible = false;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BackColor = System.Drawing.Color.White;
            this.txtObservaciones.Lines = new string[0];
            this.txtObservaciones.Location = new System.Drawing.Point(109, 38);
            this.txtObservaciones.MaxLength = 32767;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PasswordChar = '\0';
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtObservaciones.SelectedText = "";
            this.txtObservaciones.Size = new System.Drawing.Size(301, 29);
            this.txtObservaciones.Style = MetroFramework.MetroColorStyle.Red;
            this.txtObservaciones.TabIndex = 5;
            this.txtObservaciones.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtObservaciones.UseSelectable = true;
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.Silver;
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(628, 26);
            this.btnExcel.LookAndFeel.SkinMaskColor = System.Drawing.Color.White;
            this.btnExcel.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.White;
            this.btnExcel.LookAndFeel.SkinName = "Office 2007 Blue";
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 48;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(6, 38);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(98, 19);
            this.lblObservaciones.Style = MetroFramework.MetroColorStyle.Red;
            this.lblObservaciones.TabIndex = 30;
            this.lblObservaciones.Text = "Observaciones:";
            this.lblObservaciones.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtIdEmpleado
            // 
            this.txtIdEmpleado.Lines = new string[0];
            this.txtIdEmpleado.Location = new System.Drawing.Point(416, 3);
            this.txtIdEmpleado.MaxLength = 32767;
            this.txtIdEmpleado.Name = "txtIdEmpleado";
            this.txtIdEmpleado.PasswordChar = '\0';
            this.txtIdEmpleado.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtIdEmpleado.SelectedText = "";
            this.txtIdEmpleado.Size = new System.Drawing.Size(31, 29);
            this.txtIdEmpleado.Style = MetroFramework.MetroColorStyle.Red;
            this.txtIdEmpleado.TabIndex = 97;
            this.txtIdEmpleado.UseSelectable = true;
            this.txtIdEmpleado.Visible = false;
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Lines = new string[0];
            this.txtEmpleado.Location = new System.Drawing.Point(109, 3);
            this.txtEmpleado.MaxLength = 32767;
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.PasswordChar = '\0';
            this.txtEmpleado.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmpleado.SelectedText = "";
            this.txtEmpleado.Size = new System.Drawing.Size(301, 29);
            this.txtEmpleado.Style = MetroFramework.MetroColorStyle.Red;
            this.txtEmpleado.TabIndex = 1;
            this.txtEmpleado.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmpleado.UseSelectable = true;
            this.txtEmpleado.TextChanged += new System.EventHandler(this.txtEmpleado_TextChanged);
            this.txtEmpleado.Click += new System.EventHandler(this.txtEmpleado_Click);
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblEmpleado.Location = new System.Drawing.Point(6, 3);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(72, 19);
            this.lblEmpleado.Style = MetroFramework.MetroColorStyle.Red;
            this.lblEmpleado.TabIndex = 84;
            this.lblEmpleado.Text = "Empleado:";
            this.lblEmpleado.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lvEmpleado
            // 
            this.lvEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lvEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lvEmpleado.FullRowSelect = true;
            this.lvEmpleado.GridLines = true;
            this.lvEmpleado.Location = new System.Drawing.Point(109, 38);
            this.lvEmpleado.MultiSelect = false;
            this.lvEmpleado.Name = "lvEmpleado";
            this.lvEmpleado.Size = new System.Drawing.Size(301, 261);
            this.lvEmpleado.TabIndex = 83;
            this.lvEmpleado.UseCompatibleStateImageBehavior = false;
            this.lvEmpleado.View = System.Windows.Forms.View.Details;
            this.lvEmpleado.Visible = false;
            this.lvEmpleado.SelectedIndexChanged += new System.EventHandler(this.lvEmpleado_SelectedIndexChanged);
            this.lvEmpleado.Enter += new System.EventHandler(this.lvEmpleado_Enter);
            this.lvEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvEmpleado_KeyPress);
            this.lvEmpleado.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvEmpleado_MouseDoubleClick);
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Office 2007 Blue";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(1144, 478);
            this.dtgvData.TabIndex = 2;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            this.dtgvData.Click += new System.EventHandler(this.dtgvData_Click);
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowFooter = true;
            this.dtgvDataView.OptionsView.ShowGroupPanel = false;
            this.dtgvDataView.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dtgvDataView_CustomSummaryCalculate);
            // 
            // txtMarca
            // 
            this.txtMarca.Enabled = false;
            this.txtMarca.Lines = new string[0];
            this.txtMarca.Location = new System.Drawing.Point(395, 13);
            this.txtMarca.MaxLength = 32767;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.PasswordChar = '\0';
            this.txtMarca.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMarca.SelectedText = "";
            this.txtMarca.Size = new System.Drawing.Size(21, 29);
            this.txtMarca.Style = MetroFramework.MetroColorStyle.Red;
            this.txtMarca.TabIndex = 100;
            this.txtMarca.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtMarca.UseSelectable = true;
            this.txtMarca.Visible = false;
            // 
            // txtModelo
            // 
            this.txtModelo.Enabled = false;
            this.txtModelo.Lines = new string[0];
            this.txtModelo.Location = new System.Drawing.Point(422, 13);
            this.txtModelo.MaxLength = 32767;
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.PasswordChar = '\0';
            this.txtModelo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtModelo.SelectedText = "";
            this.txtModelo.Size = new System.Drawing.Size(21, 29);
            this.txtModelo.Style = MetroFramework.MetroColorStyle.Red;
            this.txtModelo.TabIndex = 99;
            this.txtModelo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtModelo.UseSelectable = true;
            this.txtModelo.Visible = false;
            // 
            // txtIMEI
            // 
            this.txtIMEI.Lines = new string[0];
            this.txtIMEI.Location = new System.Drawing.Point(354, 13);
            this.txtIMEI.MaxLength = 32767;
            this.txtIMEI.Name = "txtIMEI";
            this.txtIMEI.PasswordChar = '\0';
            this.txtIMEI.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtIMEI.SelectedText = "";
            this.txtIMEI.Size = new System.Drawing.Size(35, 29);
            this.txtIMEI.Style = MetroFramework.MetroColorStyle.Red;
            this.txtIMEI.TabIndex = 4;
            this.txtIMEI.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtIMEI.UseSelectable = true;
            this.txtIMEI.Visible = false;
            this.txtIMEI.Leave += new System.EventHandler(this.txtIMEI_Leave);
            // 
            // lblIMEI
            // 
            this.lblIMEI.AutoSize = true;
            this.lblIMEI.Location = new System.Drawing.Point(311, 13);
            this.lblIMEI.Name = "lblIMEI";
            this.lblIMEI.Size = new System.Drawing.Size(37, 19);
            this.lblIMEI.Style = MetroFramework.MetroColorStyle.Red;
            this.lblIMEI.TabIndex = 27;
            this.lblIMEI.Text = "IMEI:";
            this.lblIMEI.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lblIMEI.Visible = false;
            // 
            // frmAsignacionesTelefono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 637);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.txtIMEI);
            this.Controls.Add(this.lblIMEI);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.txtMarca);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAsignacionesTelefono";
            this.Text = "Asignaciones de Equipos Comunicación";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAsignacionesTelefono_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroTextBox txtIdEmpleado;
        private MetroFramework.Controls.MetroLabel lblObservaciones;
        private MetroFramework.Controls.MetroTextBox txtObservaciones;
        private MetroFramework.Controls.MetroTextBox txtNumero;
        private MetroFramework.Controls.MetroLabel lblIMEI;
        private MetroFramework.Controls.MetroLabel lblNumero;
        private MetroFramework.Controls.MetroTextBox txtIMEI;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroTextBox txtEmpleado;
        private MetroFramework.Controls.MetroLabel lblEmpleado;
        private System.Windows.Forms.ListView lvEmpleado;
        private MetroFramework.Controls.MetroTextBox txtIdCelular;
        private MetroFramework.Controls.MetroTextBox txtMarca;
        private MetroFramework.Controls.MetroTextBox txtModelo;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private MetroFramework.Controls.MetroLabel lblFecha;
        private MetroFramework.Controls.MetroDateTime dtpFecha;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}