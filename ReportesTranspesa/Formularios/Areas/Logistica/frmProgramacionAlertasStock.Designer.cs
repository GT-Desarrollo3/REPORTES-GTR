namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    partial class frmProgramacionAlertasStock
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxAlmacen = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.txtTiempo = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.btnBuscarCodigo = new System.Windows.Forms.Button();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscarItem = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtgListaAlertas = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaAlertasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label6 = new System.Windows.Forms.Label();
            this.pBuscarItem = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.dtgListaItems = new DevExpress.XtraGrid.GridControl();
            this.dgvListaItemsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtDescripcion2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaAlertas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaAlertasView)).BeginInit();
            this.pBuscarItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaItemsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxAlmacen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnNuevo);
            this.groupBox1.Controls.Add(this.txtTiempo);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.lblStock);
            this.groupBox1.Controls.Add(this.btnBuscarCodigo);
            this.groupBox1.Controls.Add(this.lblCodigo);
            this.groupBox1.Controls.Add(this.txtStockMinimo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 95);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar Item:";
            // 
            // cbxAlmacen
            // 
            this.cbxAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxAlmacen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAlmacen.FormattingEnabled = true;
            this.cbxAlmacen.Location = new System.Drawing.Point(506, 58);
            this.cbxAlmacen.Name = "cbxAlmacen";
            this.cbxAlmacen.Size = new System.Drawing.Size(133, 21);
            this.cbxAlmacen.TabIndex = 110;
            this.cbxAlmacen.SelectedIndexChanged += new System.EventHandler(this.cbxAlmacen_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(449, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Almacén:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(205, 23);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(523, 20);
            this.txtDescripcion.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "DÍAS ANTES";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.Enabled = false;
            this.btnNuevo.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnNuevo.Location = new System.Drawing.Point(663, 50);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(37, 34);
            this.btnNuevo.TabIndex = 16;
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtTiempo
            // 
            this.txtTiempo.Location = new System.Drawing.Point(296, 59);
            this.txtTiempo.Name = "txtTiempo";
            this.txtTiempo.Size = new System.Drawing.Size(51, 20);
            this.txtTiempo.TabIndex = 23;
            this.txtTiempo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTiempo_KeyPress);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(62, 23);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(132, 20);
            this.txtCodigo.TabIndex = 21;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(15, 62);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(76, 13);
            this.lblStock.TabIndex = 20;
            this.lblStock.Text = "Stock Mínimo:";
            // 
            // btnBuscarCodigo
            // 
            this.btnBuscarCodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCodigo.Location = new System.Drawing.Point(744, 18);
            this.btnBuscarCodigo.Name = "btnBuscarCodigo";
            this.btnBuscarCodigo.Size = new System.Drawing.Size(75, 29);
            this.btnBuscarCodigo.TabIndex = 18;
            this.btnBuscarCodigo.Text = "Buscar Ítem";
            this.btnBuscarCodigo.UseVisualStyleBackColor = true;
            this.btnBuscarCodigo.Click += new System.EventHandler(this.btnBuscarCodigo_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(15, 26);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(43, 13);
            this.lblCodigo.TabIndex = 10;
            this.lblCodigo.Text = "Codigo:";
            // 
            // txtStockMinimo
            // 
            this.txtStockMinimo.Location = new System.Drawing.Point(97, 59);
            this.txtStockMinimo.Name = "txtStockMinimo";
            this.txtStockMinimo.Size = new System.Drawing.Size(70, 20);
            this.txtStockMinimo.TabIndex = 15;
            this.txtStockMinimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tiempo de Alerta:";
            // 
            // txtBuscarItem
            // 
            this.txtBuscarItem.Location = new System.Drawing.Point(93, 23);
            this.txtBuscarItem.Name = "txtBuscarItem";
            this.txtBuscarItem.Size = new System.Drawing.Size(451, 20);
            this.txtBuscarItem.TabIndex = 23;
            this.txtBuscarItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarItem_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtgListaAlertas);
            this.groupBox2.Controls.Add(this.txtBuscarItem);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(14, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 506);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar Alerta:";
            // 
            // dtgListaAlertas
            // 
            this.dtgListaAlertas.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaAlertas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaAlertas.Location = new System.Drawing.Point(19, 57);
            this.dtgListaAlertas.MainView = this.dgvListaAlertasView;
            this.dtgListaAlertas.Name = "dtgListaAlertas";
            this.dtgListaAlertas.Size = new System.Drawing.Size(866, 437);
            this.dtgListaAlertas.TabIndex = 24;
            this.dtgListaAlertas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaAlertasView});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // EliminarToolStripMenuItem
            // 
            this.EliminarToolStripMenuItem.Enabled = false;
            this.EliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem";
            this.EliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.EliminarToolStripMenuItem.Text = "Eliminar";
            this.EliminarToolStripMenuItem.Click += new System.EventHandler(this.EliminarToolStripMenuItem_Click);
            // 
            // dgvListaAlertasView
            // 
            this.dgvListaAlertasView.GridControl = this.dtgListaAlertas;
            this.dgvListaAlertasView.Name = "dgvListaAlertasView";
            this.dgvListaAlertasView.OptionsView.ShowGroupPanel = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ingresar Item:";
            // 
            // pBuscarItem
            // 
            this.pBuscarItem.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pBuscarItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pBuscarItem.Controls.Add(this.label3);
            this.pBuscarItem.Controls.Add(this.btnSeleccionar);
            this.pBuscarItem.Controls.Add(this.dtgListaItems);
            this.pBuscarItem.Controls.Add(this.pictureBox1);
            this.pBuscarItem.Controls.Add(this.groupBox6);
            this.pBuscarItem.Location = new System.Drawing.Point(64, 94);
            this.pBuscarItem.Name = "pBuscarItem";
            this.pBuscarItem.Size = new System.Drawing.Size(801, 483);
            this.pBuscarItem.TabIndex = 25;
            this.pBuscarItem.Visible = false;
            this.pBuscarItem.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pBuscarItem_MouseMove);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(29, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(299, 24);
            this.label3.TabIndex = 27;
            this.label3.Text = "BUSCAR ÍTEMS DE ALMACÉN";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeleccionar.Location = new System.Drawing.Point(559, 76);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(105, 28);
            this.btnSeleccionar.TabIndex = 26;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // dtgListaItems
            // 
            this.dtgListaItems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaItems.Location = new System.Drawing.Point(21, 133);
            this.dtgListaItems.MainView = this.dgvListaItemsView;
            this.dtgListaItems.Name = "dtgListaItems";
            this.dtgListaItems.Size = new System.Drawing.Size(760, 329);
            this.dtgListaItems.TabIndex = 25;
            this.dtgListaItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaItemsView});
            this.dtgListaItems.DoubleClick += new System.EventHandler(this.dtgListaItems_DoubleClick);
            // 
            // dgvListaItemsView
            // 
            this.dgvListaItemsView.GridControl = this.dtgListaItems;
            this.dgvListaItemsView.Name = "dgvListaItemsView";
            this.dgvListaItemsView.OptionsView.ShowGroupPanel = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(765, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtDescripcion2);
            this.groupBox6.Location = new System.Drawing.Point(21, 56);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(518, 61);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Ingrese Item:";
            // 
            // txtDescripcion2
            // 
            this.txtDescripcion2.Location = new System.Drawing.Point(12, 25);
            this.txtDescripcion2.MaxLength = 10;
            this.txtDescripcion2.Name = "txtDescripcion2";
            this.txtDescripcion2.Size = new System.Drawing.Size(493, 20);
            this.txtDescripcion2.TabIndex = 0;
            this.txtDescripcion2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion2_KeyPress);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.SkyBlue;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(930, 50);
            this.label5.TabIndex = 26;
            this.label5.Text = "PROGRAMACIÓN DE ALERTAS DE STOCK";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmProgramacionAlertasStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(930, 699);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pBuscarItem);
            this.MaximizeBox = false;
            this.Name = "frmProgramacionAlertasStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programación de Alertas de Stock";
            this.Load += new System.EventHandler(this.frmProgramacionAlertasStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaAlertas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaAlertasView)).EndInit();
            this.pBuscarItem.ResumeLayout(false);
            this.pBuscarItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaItemsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Button btnBuscarCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtStockMinimo;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTiempo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuscarItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl dtgListaAlertas;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaAlertasView;
        private System.Windows.Forms.Panel pBuscarItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtDescripcion2;
        private DevExpress.XtraGrid.GridControl dtgListaItems;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaItemsView;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem EliminarToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxAlmacen;
    }
}