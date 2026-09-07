namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    partial class frmCalcularMonto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalcularMonto));
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxRecibo = new System.Windows.Forms.ComboBox();
            this.dtpFechaGasto = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtComprobante = new System.Windows.Forms.TextBox();
            this.btnAniadir = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txMonto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgLista = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaExpressVista)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.cbxRecibo);
            this.panel4.Controls.Add(this.dtpFechaGasto);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtComprobante);
            this.panel4.Controls.Add(this.btnAniadir);
            this.panel4.Controls.Add(this.txtTotal);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txMonto);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.txtDescripcion);
            this.panel4.Controls.Add(this.btnAgregar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(482, 171);
            this.panel4.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(71, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 107;
            this.label5.Text = "Recibo:";
            // 
            // cbxRecibo
            // 
            this.cbxRecibo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxRecibo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxRecibo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRecibo.FormattingEnabled = true;
            this.cbxRecibo.Location = new System.Drawing.Point(132, 76);
            this.cbxRecibo.Name = "cbxRecibo";
            this.cbxRecibo.Size = new System.Drawing.Size(194, 21);
            this.cbxRecibo.TabIndex = 106;
            this.cbxRecibo.SelectedIndexChanged += new System.EventHandler(this.cbxRecibo_SelectedIndexChanged);
            this.cbxRecibo.DropDownClosed += new System.EventHandler(this.cbxRecibo_DropDownClosed);
            // 
            // dtpFechaGasto
            // 
            this.dtpFechaGasto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaGasto.Location = new System.Drawing.Point(132, 136);
            this.dtpFechaGasto.Name = "dtpFechaGasto";
            this.dtpFechaGasto.Size = new System.Drawing.Size(115, 20);
            this.dtpFechaGasto.TabIndex = 105;
            this.dtpFechaGasto.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(77, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 104;
            this.label4.Text = "Fecha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 103;
            this.label3.Text = "N° Comprobante:";
            // 
            // txtComprobante
            // 
            this.txtComprobante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComprobante.Location = new System.Drawing.Point(132, 107);
            this.txtComprobante.Name = "txtComprobante";
            this.txtComprobante.Size = new System.Drawing.Size(194, 20);
            this.txtComprobante.TabIndex = 102;
            // 
            // btnAniadir
            // 
            this.btnAniadir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAniadir.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAniadir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAniadir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAniadir.Location = new System.Drawing.Point(346, 36);
            this.btnAniadir.Name = "btnAniadir";
            this.btnAniadir.Size = new System.Drawing.Size(115, 43);
            this.btnAniadir.TabIndex = 101;
            this.btnAniadir.Text = "            AÑADIR";
            this.btnAniadir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAniadir.UseVisualStyleBackColor = true;
            this.btnAniadir.Click += new System.EventHandler(this.btnAñadir_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(271, 138);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(55, 20);
            this.txtTotal.TabIndex = 98;
            this.txtTotal.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 97;
            this.label1.Text = "Descripción:";
            // 
            // txMonto
            // 
            this.txMonto.Location = new System.Drawing.Point(132, 45);
            this.txMonto.Name = "txMonto";
            this.txMonto.Size = new System.Drawing.Size(194, 20);
            this.txMonto.TabIndex = 96;
            this.txMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txMonto_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(78, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 41;
            this.label6.Text = "Monto:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(132, 16);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(194, 20);
            this.txtDescripcion.TabIndex = 0;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAgregar.Location = new System.Drawing.Point(346, 93);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(115, 43);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "           REGISTRAR";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(482, 41);
            this.panel2.TabIndex = 6;
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
            this.label2.Size = new System.Drawing.Size(482, 41);
            this.label2.TabIndex = 3;
            this.label2.Text = "CALCULAR MONTO DE AUXILIOS MECÁNICOS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgLista
            // 
            this.dtgLista.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgLista.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgLista.Location = new System.Drawing.Point(0, 212);
            this.dtgLista.MainView = this.dgvListaExpressVista;
            this.dtgLista.Name = "dtgLista";
            this.dtgLista.Size = new System.Drawing.Size(482, 250);
            this.dtgLista.TabIndex = 98;
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
            this.eliminarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eliminarToolStripMenuItem.Image")));
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
            this.dgvListaExpressVista.OptionsView.ShowFooter = true;
            // 
            // frmCalcularMonto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 462);
            this.Controls.Add(this.dtgLista);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalcularMonto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calcular Monto";
            this.Load += new System.EventHandler(this.frmCalcularMonto_Load);
            this.Shown += new System.EventHandler(this.frmCalcularMonto_Shown);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaExpressVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgLista;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaExpressVista;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.TextBox txMonto;
        public System.Windows.Forms.TextBox txtTotal;
        public System.Windows.Forms.Button btnAgregar;
        public System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        public System.Windows.Forms.TextBox txtDescripcion;
        public System.Windows.Forms.Button btnAniadir;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtComprobante;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaGasto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxRecibo;
    }
}