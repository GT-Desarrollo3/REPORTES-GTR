namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmResumenRecursos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResumenRecursos));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgResumenRecursos = new DevExpress.XtraGrid.GridControl();
            this.dgvResumenRecursosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabProcesos = new System.Windows.Forms.TabControl();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.tabManoObra = new System.Windows.Forms.TabPage();
            this.dtgResumenMO = new DevExpress.XtraGrid.GridControl();
            this.dgvResumenMO = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxEspecialidad = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaFin2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExcel2 = new DevExpress.XtraEditors.SimpleButton();
            this.dtpFechaInicio2 = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar2 = new DevExpress.XtraEditors.SimpleButton();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResumenRecursos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenRecursosVista)).BeginInit();
            this.tabProcesos.SuspendLayout();
            this.tabItems.SuspendLayout();
            this.tabManoObra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResumenMO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenMO)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(767, 40);
            this.label1.TabIndex = 15;
            this.label1.Text = "RESUMEN DE INSUMOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtItem);
            this.panel4.Controls.Add(this.dtpFechaFin);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.btnExcel);
            this.panel4.Controls.Add(this.dtpFechaInicio);
            this.panel4.Controls.Add(this.btnBuscar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(753, 102);
            this.panel4.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(59, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 226;
            this.label3.Text = "Ingresar Ítem:";
            // 
            // txtItem
            // 
            this.txtItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem.Location = new System.Drawing.Point(167, 57);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(424, 22);
            this.txtItem.TabIndex = 221;
            this.txtItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItem_KeyPress);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(295, 21);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(104, 22);
            this.dtpFechaFin.TabIndex = 5;
            this.dtpFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 16);
            this.label2.TabIndex = 225;
            this.label2.Text = "Seleccionar Fecha:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(274, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 16);
            this.label15.TabIndex = 4;
            this.label15.Text = "--";
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
            this.btnExcel.Location = new System.Drawing.Point(692, 26);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 224;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(167, 21);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(104, 22);
            this.dtpFechaInicio.TabIndex = 2;
            this.dtpFechaInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio_KeyPress);
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
            this.btnBuscar.Location = new System.Drawing.Point(633, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 223;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgResumenRecursos
            // 
            this.dtgResumenRecursos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgResumenRecursos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgResumenRecursos.Location = new System.Drawing.Point(3, 105);
            this.dtgResumenRecursos.MainView = this.dgvResumenRecursosVista;
            this.dtgResumenRecursos.Name = "dtgResumenRecursos";
            this.dtgResumenRecursos.Size = new System.Drawing.Size(753, 245);
            this.dtgResumenRecursos.TabIndex = 227;
            this.dtgResumenRecursos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvResumenRecursosVista});
            // 
            // dgvResumenRecursosVista
            // 
            this.dgvResumenRecursosVista.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvResumenRecursosVista.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvResumenRecursosVista.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvResumenRecursosVista.Appearance.Row.Options.UseFont = true;
            this.dgvResumenRecursosVista.GridControl = this.dtgResumenRecursos;
            this.dgvResumenRecursosVista.Name = "dgvResumenRecursosVista";
            this.dgvResumenRecursosVista.OptionsBehavior.Editable = false;
            this.dgvResumenRecursosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvResumenRecursosVista.OptionsView.RowAutoHeight = true;
            this.dgvResumenRecursosVista.OptionsView.ShowFooter = true;
            this.dgvResumenRecursosVista.OptionsView.ShowGroupPanel = false;
            // 
            // tabProcesos
            // 
            this.tabProcesos.Controls.Add(this.tabItems);
            this.tabProcesos.Controls.Add(this.tabManoObra);
            this.tabProcesos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProcesos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabProcesos.Location = new System.Drawing.Point(0, 40);
            this.tabProcesos.Name = "tabProcesos";
            this.tabProcesos.SelectedIndex = 0;
            this.tabProcesos.Size = new System.Drawing.Size(767, 386);
            this.tabProcesos.TabIndex = 228;
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.dtgResumenRecursos);
            this.tabItems.Controls.Add(this.panel4);
            this.tabItems.Location = new System.Drawing.Point(4, 29);
            this.tabItems.Name = "tabItems";
            this.tabItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabItems.Size = new System.Drawing.Size(759, 353);
            this.tabItems.TabIndex = 0;
            this.tabItems.Text = "INSUMOS DE RECURSOS";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // tabManoObra
            // 
            this.tabManoObra.Controls.Add(this.dtgResumenMO);
            this.tabManoObra.Controls.Add(this.panel1);
            this.tabManoObra.Location = new System.Drawing.Point(4, 29);
            this.tabManoObra.Name = "tabManoObra";
            this.tabManoObra.Padding = new System.Windows.Forms.Padding(3);
            this.tabManoObra.Size = new System.Drawing.Size(759, 353);
            this.tabManoObra.TabIndex = 1;
            this.tabManoObra.Text = "INSUMOS DE MANO DE OBRA";
            this.tabManoObra.UseVisualStyleBackColor = true;
            // 
            // dtgResumenMO
            // 
            this.dtgResumenMO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgResumenMO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgResumenMO.Location = new System.Drawing.Point(3, 105);
            this.dtgResumenMO.MainView = this.dgvResumenMO;
            this.dtgResumenMO.Name = "dtgResumenMO";
            this.dtgResumenMO.Size = new System.Drawing.Size(753, 245);
            this.dtgResumenMO.TabIndex = 228;
            this.dtgResumenMO.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvResumenMO});
            // 
            // dgvResumenMO
            // 
            this.dgvResumenMO.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvResumenMO.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgvResumenMO.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvResumenMO.Appearance.Row.Options.UseFont = true;
            this.dgvResumenMO.GridControl = this.dtgResumenMO;
            this.dgvResumenMO.Name = "dgvResumenMO";
            this.dgvResumenMO.OptionsBehavior.Editable = false;
            this.dgvResumenMO.OptionsView.ColumnAutoWidth = false;
            this.dgvResumenMO.OptionsView.RowAutoHeight = true;
            this.dgvResumenMO.OptionsView.ShowFooter = true;
            this.dgvResumenMO.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.cbxEspecialidad);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpFechaFin2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnExcel2);
            this.panel1.Controls.Add(this.dtpFechaInicio2);
            this.panel1.Controls.Add(this.btnBuscar2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(753, 102);
            this.panel1.TabIndex = 17;
            // 
            // cbxEspecialidad
            // 
            this.cbxEspecialidad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEspecialidad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEspecialidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxEspecialidad.FormattingEnabled = true;
            this.cbxEspecialidad.Location = new System.Drawing.Point(167, 57);
            this.cbxEspecialidad.Name = "cbxEspecialidad";
            this.cbxEspecialidad.Size = new System.Drawing.Size(232, 23);
            this.cbxEspecialidad.TabIndex = 227;
            this.cbxEspecialidad.SelectedIndexChanged += new System.EventHandler(this.cbxEspecialidad_SelectedIndexChanged);
            this.cbxEspecialidad.DropDownClosed += new System.EventHandler(this.cbxEspecialidad_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 226;
            this.label4.Text = "Especialidad:";
            // 
            // dtpFechaFin2
            // 
            this.dtpFechaFin2.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaFin2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin2.Location = new System.Drawing.Point(295, 21);
            this.dtpFechaFin2.Name = "dtpFechaFin2";
            this.dtpFechaFin2.Size = new System.Drawing.Size(104, 22);
            this.dtpFechaFin2.TabIndex = 5;
            this.dtpFechaFin2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFin2_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 16);
            this.label5.TabIndex = 225;
            this.label5.Text = "Seleccionar Fecha:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(274, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "--";
            // 
            // btnExcel2
            // 
            this.btnExcel2.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel2.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel2.Appearance.Options.UseBackColor = true;
            this.btnExcel2.Appearance.Options.UseBorderColor = true;
            this.btnExcel2.Appearance.Options.UseFont = true;
            this.btnExcel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel2.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel2.Image")));
            this.btnExcel2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel2.Location = new System.Drawing.Point(503, 26);
            this.btnExcel2.Name = "btnExcel2";
            this.btnExcel2.Size = new System.Drawing.Size(51, 47);
            this.btnExcel2.TabIndex = 224;
            this.btnExcel2.Tag = "6";
            this.btnExcel2.ToolTip = "Exportar a Excel";
            this.btnExcel2.Click += new System.EventHandler(this.btnExcel2_Click);
            // 
            // dtpFechaInicio2
            // 
            this.dtpFechaInicio2.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInicio2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio2.Location = new System.Drawing.Point(167, 21);
            this.dtpFechaInicio2.Name = "dtpFechaInicio2";
            this.dtpFechaInicio2.Size = new System.Drawing.Size(104, 22);
            this.dtpFechaInicio2.TabIndex = 2;
            this.dtpFechaInicio2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaInicio2_KeyPress);
            // 
            // btnBuscar2
            // 
            this.btnBuscar2.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar2.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar2.Appearance.Options.UseBackColor = true;
            this.btnBuscar2.Appearance.Options.UseBorderColor = true;
            this.btnBuscar2.Appearance.Options.UseFont = true;
            this.btnBuscar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar2.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar2.Image")));
            this.btnBuscar2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar2.Location = new System.Drawing.Point(444, 26);
            this.btnBuscar2.Name = "btnBuscar2";
            this.btnBuscar2.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar2.TabIndex = 223;
            this.btnBuscar2.Tag = "5";
            this.btnBuscar2.ToolTip = "Buscar";
            this.btnBuscar2.Click += new System.EventHandler(this.btnBuscar2_Click);
            // 
            // frmResumenRecursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(767, 426);
            this.Controls.Add(this.tabProcesos);
            this.Controls.Add(this.label1);
            this.Name = "frmResumenRecursos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RESUMEN DE RECURSOS";
            this.Load += new System.EventHandler(this.frmResumenRecursos_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResumenRecursos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenRecursosVista)).EndInit();
            this.tabProcesos.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            this.tabManoObra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgResumenMO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenMO)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        public DevExpress.XtraGrid.GridControl dtgResumenRecursos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvResumenRecursosVista;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtItem;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker dtpFechaFin;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.TabControl tabProcesos;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.TabPage tabManoObra;
        public DevExpress.XtraGrid.GridControl dtgResumenMO;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvResumenMO;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DateTimePicker dtpFechaFin2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public DevExpress.XtraEditors.SimpleButton btnExcel2;
        public System.Windows.Forms.DateTimePicker dtpFechaInicio2;
        public DevExpress.XtraEditors.SimpleButton btnBuscar2;
        public System.Windows.Forms.ComboBox cbxEspecialidad;
    }
}