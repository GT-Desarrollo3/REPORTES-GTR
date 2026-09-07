namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmIngresarGastoManual
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbAdicional = new System.Windows.Forms.CheckBox();
            this.cbxTipoGasto = new System.Windows.Forms.ComboBox();
            this.lblProgramacion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRuta = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAniadir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGasto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxTiempo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtgLista = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label25 = new System.Windows.Forms.Label();
            this.cbxPeaje = new System.Windows.Forms.ComboBox();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaExpressVista)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkRed;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(525, 41);
            this.label2.TabIndex = 4;
            this.label2.Text = "INGRESAR GASTOS DE VIAJE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.cbxPeaje);
            this.panel4.Controls.Add(this.cbAdicional);
            this.panel4.Controls.Add(this.cbxTipoGasto);
            this.panel4.Controls.Add(this.lblProgramacion);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.lblRuta);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btnAniadir);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txtGasto);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.btnRegistrar);
            this.panel4.Controls.Add(this.txtTotal);
            this.panel4.Controls.Add(this.txtDescripcion);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.cbxTiempo);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(525, 517);
            this.panel4.TabIndex = 5;
            // 
            // cbAdicional
            // 
            this.cbAdicional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAdicional.AutoSize = true;
            this.cbAdicional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAdicional.Location = new System.Drawing.Point(24, 205);
            this.cbAdicional.Name = "cbAdicional";
            this.cbAdicional.Size = new System.Drawing.Size(145, 20);
            this.cbAdicional.TabIndex = 115;
            this.cbAdicional.Text = "Reintegro Adicional";
            this.cbAdicional.UseVisualStyleBackColor = true;
            this.cbAdicional.CheckedChanged += new System.EventHandler(this.cbAdicional_CheckedChanged);
            // 
            // cbxTipoGasto
            // 
            this.cbxTipoGasto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoGasto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoGasto.FormattingEnabled = true;
            this.cbxTipoGasto.Location = new System.Drawing.Point(122, 72);
            this.cbxTipoGasto.Name = "cbxTipoGasto";
            this.cbxTipoGasto.Size = new System.Drawing.Size(207, 21);
            this.cbxTipoGasto.TabIndex = 107;
            this.cbxTipoGasto.SelectedIndexChanged += new System.EventHandler(this.cbxTipoGasto_SelectedIndexChanged);
            this.cbxTipoGasto.DropDownClosed += new System.EventHandler(this.cbxTipoGasto_DropDownClosed);
            // 
            // lblProgramacion
            // 
            this.lblProgramacion.AutoSize = true;
            this.lblProgramacion.BackColor = System.Drawing.Color.LemonChiffon;
            this.lblProgramacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramacion.ForeColor = System.Drawing.Color.DarkRed;
            this.lblProgramacion.Location = new System.Drawing.Point(128, 41);
            this.lblProgramacion.Name = "lblProgramacion";
            this.lblProgramacion.Size = new System.Drawing.Size(0, 16);
            this.lblProgramacion.TabIndex = 105;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 16);
            this.label5.TabIndex = 104;
            this.label5.Text = "Programación:";
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.BackColor = System.Drawing.Color.LemonChiffon;
            this.lblRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuta.ForeColor = System.Drawing.Color.DarkRed;
            this.lblRuta.Location = new System.Drawing.Point(63, 15);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(0, 16);
            this.lblRuta.TabIndex = 103;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 102;
            this.label3.Text = "Ruta:";
            // 
            // btnAniadir
            // 
            this.btnAniadir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAniadir.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAniadir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAniadir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAniadir.Location = new System.Drawing.Point(394, 90);
            this.btnAniadir.Name = "btnAniadir";
            this.btnAniadir.Size = new System.Drawing.Size(111, 37);
            this.btnAniadir.TabIndex = 101;
            this.btnAniadir.Text = "            AÑADIR";
            this.btnAniadir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAniadir.UseVisualStyleBackColor = true;
            this.btnAniadir.Click += new System.EventHandler(this.btnAniadir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 97;
            this.label1.Text = "Tipo de gasto:";
            // 
            // txtGasto
            // 
            this.txtGasto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGasto.Location = new System.Drawing.Point(122, 173);
            this.txtGasto.Name = "txtGasto";
            this.txtGasto.Size = new System.Drawing.Size(75, 20);
            this.txtGasto.TabIndex = 96;
            this.txtGasto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txMonto_KeyPress);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(57, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 41;
            this.label6.Text = "Efectivo:";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnRegistrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRegistrar.Location = new System.Drawing.Point(394, 139);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(111, 37);
            this.btnRegistrar.TabIndex = 3;
            this.btnRegistrar.Text = "           REGISTRAR";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(335, 73);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(39, 20);
            this.txtTotal.TabIndex = 98;
            this.txtTotal.Visible = false;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDescripcion.Location = new System.Drawing.Point(122, 140);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(252, 20);
            this.txtDescripcion.TabIndex = 109;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 108;
            this.label4.Text = "Descripción:";
            // 
            // cbxTiempo
            // 
            this.cbxTiempo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxTiempo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTiempo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTiempo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTiempo.FormattingEnabled = true;
            this.cbxTiempo.Location = new System.Drawing.Point(283, 173);
            this.cbxTiempo.Name = "cbxTiempo";
            this.cbxTiempo.Size = new System.Drawing.Size(91, 21);
            this.cbxTiempo.TabIndex = 113;
            this.cbxTiempo.SelectedIndexChanged += new System.EventHandler(this.cbxTiempo_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(219, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 16);
            this.label7.TabIndex = 112;
            this.label7.Text = "Tiempo:";
            // 
            // dtgLista
            // 
            this.dtgLista.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgLista.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgLista.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgLista.Location = new System.Drawing.Point(0, 278);
            this.dtgLista.MainView = this.dgvListaExpressVista;
            this.dtgLista.Name = "dtgLista";
            this.dtgLista.Size = new System.Drawing.Size(525, 280);
            this.dtgLista.TabIndex = 99;
            this.dtgLista.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaExpressVista});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dgvListaExpressVista
            // 
            this.dgvListaExpressVista.GridControl = this.dtgLista;
            this.dgvListaExpressVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvListaExpressVista.Name = "dgvListaExpressVista";
            this.dgvListaExpressVista.OptionsBehavior.Editable = false;
            this.dgvListaExpressVista.OptionsBehavior.ReadOnly = true;
            this.dgvListaExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaExpressVista.OptionsView.RowAutoHeight = true;
            this.dgvListaExpressVista.OptionsView.ShowFooter = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(69, 108);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(47, 16);
            this.label25.TabIndex = 145;
            this.label25.Text = "Peaje:";
            // 
            // cbxPeaje
            // 
            this.cbxPeaje.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxPeaje.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxPeaje.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPeaje.FormattingEnabled = true;
            this.cbxPeaje.Location = new System.Drawing.Point(122, 106);
            this.cbxPeaje.Name = "cbxPeaje";
            this.cbxPeaje.Size = new System.Drawing.Size(252, 21);
            this.cbxPeaje.TabIndex = 144;
            this.cbxPeaje.SelectedIndexChanged += new System.EventHandler(this.cbxPeaje_SelectedIndexChanged);
            this.cbxPeaje.DropDownClosed += new System.EventHandler(this.cbxPeaje_DropDownClosed);
            // 
            // frmIngresarGastoManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 558);
            this.Controls.Add(this.dtgLista);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIngresarGastoManual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingresar Gastos de Viaje";
            this.Load += new System.EventHandler(this.frmIngresarGastoManual_Load);
            this.Shown += new System.EventHandler(this.frmIngresarGastoManual_Shown);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaExpressVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Button btnAniadir;
        public System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtGasto;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl dtgLista;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaExpressVista;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProgramacion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.ComboBox cbxTipoGasto;
        public System.Windows.Forms.Button btnRegistrar;
        public System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxTiempo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbAdicional;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbxPeaje;
    }
}