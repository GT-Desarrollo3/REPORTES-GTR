namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    partial class frmRegistrarActivoSegundoUso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistrarActivoSegundoUso));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombreActivo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigoSpring = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desactivarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUnidadMedida = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxSucursal = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnRegistrarA = new DevExpress.XtraEditors.SimpleButton();
            this.lstItems = new System.Windows.Forms.ListView();
            this.dtgActivos = new DevExpress.XtraGrid.GridControl();
            this.dgvActivosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgActivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivosVista)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(527, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "REGISTRAR ACTIVO - SEGUNDO USO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre de Activo:";
            // 
            // txtNombreActivo
            // 
            this.txtNombreActivo.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombreActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreActivo.Location = new System.Drawing.Point(20, 30);
            this.txtNombreActivo.Name = "txtNombreActivo";
            this.txtNombreActivo.Size = new System.Drawing.Size(411, 21);
            this.txtNombreActivo.TabIndex = 6;
            this.txtNombreActivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreActivo_KeyPress);
            this.txtNombreActivo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNombreActivo_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Codigo Spring:";
            // 
            // txtCodigoSpring
            // 
            this.txtCodigoSpring.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoSpring.Location = new System.Drawing.Point(126, 62);
            this.txtCodigoSpring.Name = "txtCodigoSpring";
            this.txtCodigoSpring.ReadOnly = true;
            this.txtCodigoSpring.Size = new System.Drawing.Size(90, 21);
            this.txtCodigoSpring.TabIndex = 8;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidad.Location = new System.Drawing.Point(332, 62);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(99, 21);
            this.txtCantidad.TabIndex = 10;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(268, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Cantidad:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activarToolStripMenuItem,
            this.desactivarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 48);
            // 
            // activarToolStripMenuItem
            // 
            this.activarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.updated1;
            this.activarToolStripMenuItem.Name = "activarToolStripMenuItem";
            this.activarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.activarToolStripMenuItem.Text = "Activar";
            this.activarToolStripMenuItem.Click += new System.EventHandler(this.activarToolStripMenuItem_Click);
            // 
            // desactivarToolStripMenuItem
            // 
            this.desactivarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.menos;
            this.desactivarToolStripMenuItem.Name = "desactivarToolStripMenuItem";
            this.desactivarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.desactivarToolStripMenuItem.Text = "Desactivar";
            this.desactivarToolStripMenuItem.Click += new System.EventHandler(this.desactivarToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Turquoise;
            this.label5.Location = new System.Drawing.Point(11, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 19);
            this.label5.TabIndex = 13;
            this.label5.Text = "Lista de Activos:";
            // 
            // txtUnidadMedida
            // 
            this.txtUnidadMedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtUnidadMedida.Location = new System.Drawing.Point(126, 93);
            this.txtUnidadMedida.Name = "txtUnidadMedida";
            this.txtUnidadMedida.ReadOnly = true;
            this.txtUnidadMedida.Size = new System.Drawing.Size(90, 21);
            this.txtUnidadMedida.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Unidad Medida:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.cbxSucursal);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btnRegistrarA);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNombreActivo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCodigoSpring);
            this.panel1.Controls.Add(this.txtCantidad);
            this.panel1.Controls.Add(this.txtUnidadMedida);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 158);
            this.panel1.TabIndex = 19;
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
            this.cbxSucursal.Location = new System.Drawing.Point(332, 93);
            this.cbxSucursal.Name = "cbxSucursal";
            this.cbxSucursal.Size = new System.Drawing.Size(99, 21);
            this.cbxSucursal.TabIndex = 227;
            this.cbxSucursal.DropDownClosed += new System.EventHandler(this.cbxSucursal_DropDownClosed);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(268, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 15);
            this.label13.TabIndex = 226;
            this.label13.Text = "Sucursal:";
            // 
            // btnRegistrarA
            // 
            this.btnRegistrarA.Appearance.BackColor = System.Drawing.Color.White;
            this.btnRegistrarA.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnRegistrarA.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnRegistrarA.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarA.Appearance.Options.UseBackColor = true;
            this.btnRegistrarA.Appearance.Options.UseBorderColor = true;
            this.btnRegistrarA.Appearance.Options.UseFont = true;
            this.btnRegistrarA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarA.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrarA.Image")));
            this.btnRegistrarA.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRegistrarA.Location = new System.Drawing.Point(455, 47);
            this.btnRegistrarA.Name = "btnRegistrarA";
            this.btnRegistrarA.Size = new System.Drawing.Size(50, 50);
            this.btnRegistrarA.TabIndex = 197;
            this.btnRegistrarA.Tag = "5";
            this.btnRegistrarA.ToolTip = "Buscar";
            this.btnRegistrarA.Click += new System.EventHandler(this.btnRegistrarA_Click);
            // 
            // lstItems
            // 
            this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItems.ForeColor = System.Drawing.Color.Navy;
            this.lstItems.FullRowSelect = true;
            this.lstItems.GridLines = true;
            this.lstItems.Location = new System.Drawing.Point(23, 87);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(385, 120);
            this.lstItems.TabIndex = 110;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.View = System.Windows.Forms.View.Details;
            this.lstItems.Visible = false;
            this.lstItems.Enter += new System.EventHandler(this.lstItems_Enter);
            this.lstItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstItems_KeyPress);
            this.lstItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDoubleClick);
            // 
            // dtgActivos
            // 
            this.dtgActivos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgActivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgActivos.Location = new System.Drawing.Point(0, 195);
            this.dtgActivos.LookAndFeel.SkinMaskColor = System.Drawing.Color.Aqua;
            this.dtgActivos.LookAndFeel.SkinName = "Money Twins";
            this.dtgActivos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgActivos.MainView = this.dgvActivosVista;
            this.dtgActivos.Name = "dtgActivos";
            this.dtgActivos.Size = new System.Drawing.Size(527, 233);
            this.dtgActivos.TabIndex = 112;
            this.dtgActivos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvActivosVista});
            this.dtgActivos.DoubleClick += new System.EventHandler(this.dtgActivos_DoubleClick);
            // 
            // dgvActivosVista
            // 
            this.dgvActivosVista.GridControl = this.dtgActivos;
            this.dgvActivosVista.Name = "dgvActivosVista";
            this.dgvActivosVista.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvActivosVista.OptionsBehavior.Editable = false;
            this.dgvActivosVista.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvActivosVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvActivosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvActivosVista.OptionsView.ShowFooter = true;
            this.dgvActivosVista.OptionsView.ShowGroupPanel = false;
            // 
            // frmRegistrarActivoSegundoUso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 428);
            this.Controls.Add(this.dtgActivos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstItems);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegistrarActivoSegundoUso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRAR ACTIVO - SEGUNDO USO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRegistrarActivoSegundoUso_FormClosed);
            this.Load += new System.EventHandler(this.frmRegistrarActivoSegundoUso_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgActivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivosVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem activarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desactivarToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtNombreActivo;
        public System.Windows.Forms.TextBox txtCodigoSpring;
        public System.Windows.Forms.TextBox txtCantidad;
        public System.Windows.Forms.TextBox txtUnidadMedida;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lstItems;
        private DevExpress.XtraGrid.GridControl dtgActivos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvActivosVista;
        public DevExpress.XtraEditors.SimpleButton btnRegistrarA;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.ComboBox cbxSucursal;
    }
}