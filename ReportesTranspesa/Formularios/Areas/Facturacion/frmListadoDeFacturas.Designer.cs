namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    partial class frmListadoDeFacturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoDeFacturas));
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.Fecha_Inicio = new System.Windows.Forms.Label();
            this.Fecha_Fin = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ModificarFactura = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.Cliente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCompania = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstCliente = new System.Windows.Forms.ListView();
            this.ListadoFactura = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAccesos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.ListadoFactura.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(395, 16);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(139, 20);
            this.dtpFechaIni.TabIndex = 1;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(395, 49);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(139, 20);
            this.dtpFechaFin.TabIndex = 2;
            // 
            // Fecha_Inicio
            // 
            this.Fecha_Inicio.AutoSize = true;
            this.Fecha_Inicio.Location = new System.Drawing.Point(323, 20);
            this.Fecha_Inicio.Name = "Fecha_Inicio";
            this.Fecha_Inicio.Size = new System.Drawing.Size(68, 13);
            this.Fecha_Inicio.TabIndex = 3;
            this.Fecha_Inicio.Text = "Fecha Inicio:";
            // 
            // Fecha_Fin
            // 
            this.Fecha_Fin.AutoSize = true;
            this.Fecha_Fin.Location = new System.Drawing.Point(334, 52);
            this.Fecha_Fin.Name = "Fecha_Fin";
            this.Fecha_Fin.Size = new System.Drawing.Size(57, 13);
            this.Fecha_Fin.TabIndex = 4;
            this.Fecha_Fin.Text = "Fecha Fin:";
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
            this.btnBuscar.ImageIndex = 0;
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(887, 15);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(48, 48);
            this.btnBuscar.TabIndex = 51;
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btn_BuscarClick);
            // 
            // btn_ModificarFactura
            // 
            this.btn_ModificarFactura.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.btn_ModificarFactura.Location = new System.Drawing.Point(128, 78);
            this.btn_ModificarFactura.Name = "btn_ModificarFactura";
            this.btn_ModificarFactura.Size = new System.Drawing.Size(83, 33);
            this.btn_ModificarFactura.TabIndex = 52;
            this.btn_ModificarFactura.Text = "Modificar";
            this.btn_ModificarFactura.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_ModificarFactura.UseVisualStyleBackColor = true;
            this.btn_ModificarFactura.Visible = false;
            this.btn_ModificarFactura.Click += new System.EventHandler(this.btn_ModificarFactura_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "NOTA DE CREDITO",
            "FACTURACION ELECTRONICA",
            "BOLETA DE VENTA"});
            this.comboBox1.Location = new System.Drawing.Point(117, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 21);
            this.comboBox1.TabIndex = 53;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.gridControl1.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.gridControl1.Location = new System.Drawing.Point(-2, 119);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1173, 486);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(618, 21);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(263, 20);
            this.txtCliente.TabIndex = 55;
            this.txtCliente.TextChanged += new System.EventHandler(this.txtCliente_TextChanged);
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            // 
            // Cliente
            // 
            this.Cliente.AutoSize = true;
            this.Cliente.Location = new System.Drawing.Point(570, 24);
            this.Cliente.Name = "Cliente";
            this.Cliente.Size = new System.Drawing.Size(42, 13);
            this.Cliente.TabIndex = 56;
            this.Cliente.Text = "Cliente:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Tipo De Documento:";
            // 
            // cboCompania
            // 
            this.cboCompania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompania.FormattingEnabled = true;
            this.cboCompania.Items.AddRange(new object[] {
            "TODO",
            "GRUPO TRANSPESA",
            "FABRICACIONES BRA"});
            this.cboCompania.Location = new System.Drawing.Point(117, 19);
            this.cboCompania.Name = "cboCompania";
            this.cboCompania.Size = new System.Drawing.Size(184, 21);
            this.cboCompania.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Compania:";
            // 
            // lstCliente
            // 
            this.lstCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCliente.BackColor = System.Drawing.Color.PaleGreen;
            this.lstCliente.ForeColor = System.Drawing.Color.Blue;
            this.lstCliente.FullRowSelect = true;
            this.lstCliente.GridLines = true;
            this.lstCliente.Location = new System.Drawing.Point(618, 53);
            this.lstCliente.MultiSelect = false;
            this.lstCliente.Name = "lstCliente";
            this.lstCliente.Size = new System.Drawing.Size(263, 10);
            this.lstCliente.TabIndex = 60;
            this.lstCliente.UseCompatibleStateImageBehavior = false;
            this.lstCliente.View = System.Windows.Forms.View.Details;
            this.lstCliente.Visible = false;
            this.lstCliente.Enter += new System.EventHandler(this.lstCliente_Enter);
            this.lstCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCliente_KeyPress);
            this.lstCliente.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstCliente_MouseDoubleClick);
            // 
            // ListadoFactura
            // 
            this.ListadoFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListadoFactura.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ListadoFactura.Controls.Add(this.button1);
            this.ListadoFactura.Controls.Add(this.btnAccesos);
            this.ListadoFactura.Controls.Add(this.Fecha_Inicio);
            this.ListadoFactura.Controls.Add(this.btnBuscar);
            this.ListadoFactura.Controls.Add(this.Fecha_Fin);
            this.ListadoFactura.Controls.Add(this.Cliente);
            this.ListadoFactura.Controls.Add(this.dtpFechaFin);
            this.ListadoFactura.Controls.Add(this.label2);
            this.ListadoFactura.Controls.Add(this.dtpFechaIni);
            this.ListadoFactura.Controls.Add(this.txtCliente);
            this.ListadoFactura.Controls.Add(this.btn_ModificarFactura);
            this.ListadoFactura.Controls.Add(this.cboCompania);
            this.ListadoFactura.Controls.Add(this.comboBox1);
            this.ListadoFactura.Controls.Add(this.label1);
            this.ListadoFactura.Location = new System.Drawing.Point(0, 2);
            this.ListadoFactura.Name = "ListadoFactura";
            this.ListadoFactura.Size = new System.Drawing.Size(1171, 114);
            this.ListadoFactura.TabIndex = 61;
            this.ListadoFactura.TabStop = false;
            this.ListadoFactura.Text = "Listado de Facturas";
            // 
            // button1
            // 
            this.button1.Image = global::ReportesTranspesa.Properties.Resources.history;
            this.button1.Location = new System.Drawing.Point(1062, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 61;
            this.button1.Text = "Historico";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAccesos
            // 
            this.btnAccesos.Image = global::ReportesTranspesa.Properties.Resources.conductorbloqueado;
            this.btnAccesos.Location = new System.Drawing.Point(30, 78);
            this.btnAccesos.Name = "btnAccesos";
            this.btnAccesos.Size = new System.Drawing.Size(80, 33);
            this.btnAccesos.TabIndex = 60;
            this.btnAccesos.Text = "Accesos";
            this.btnAccesos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccesos.UseVisualStyleBackColor = true;
            this.btnAccesos.Visible = false;
            this.btnAccesos.Click += new System.EventHandler(this.btnAccesos_Click);
            // 
            // frmListadoDeFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 605);
            this.Controls.Add(this.lstCliente);
            this.Controls.Add(this.ListadoFactura);
            this.Controls.Add(this.gridControl1);
            this.Name = "frmListadoDeFacturas";
            this.Text = "frmListadoDeFacturas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoDeFacturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.ListadoFactura.ResumeLayout(false);
            this.ListadoFactura.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label Fecha_Inicio;
        private System.Windows.Forms.Label Fecha_Fin;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.Button btn_ModificarFactura;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label Cliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCompania;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lstCliente;
        private System.Windows.Forms.GroupBox ListadoFactura;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.Button btnAccesos;
        private System.Windows.Forms.Button button1;
    }
}