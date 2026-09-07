namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmGestionarSeries
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
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkInactivos = new System.Windows.Forms.CheckBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupTipoGuia = new System.Windows.Forms.GroupBox();
            this.cbxTipoGuia = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxEmpresasGrupo = new System.Windows.Forms.ComboBox();
            this.GroupSucursal = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.groupSerie = new System.Windows.Forms.GroupBox();
            this.txtSeries = new System.Windows.Forms.TextBox();
            this.dtgListaGuias = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.desactivarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vincularAnexoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vincularUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaGuiaExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            this.groupTipoGuia.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GroupSucursal.SuspendLayout();
            this.groupSerie.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuias)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiaExpressVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTituloGuia
            // 
            this.lblTituloGuia.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblTituloGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloGuia.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTituloGuia.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGuia.Name = "lblTituloGuia";
            this.lblTituloGuia.Size = new System.Drawing.Size(594, 58);
            this.lblTituloGuia.TabIndex = 1;
            this.lblTituloGuia.Text = "GESTIONAR SERIES";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkInactivos);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.groupTipoGuia);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.GroupSucursal);
            this.panel1.Controls.Add(this.groupSerie);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 154);
            this.panel1.TabIndex = 2;
            // 
            // chkInactivos
            // 
            this.chkInactivos.AutoSize = true;
            this.chkInactivos.Location = new System.Drawing.Point(404, 131);
            this.chkInactivos.Name = "chkInactivos";
            this.chkInactivos.Size = new System.Drawing.Size(88, 17);
            this.chkInactivos.TabIndex = 3;
            this.chkInactivos.Text = "Ver Inactivos";
            this.chkInactivos.UseVisualStyleBackColor = true;
            this.chkInactivos.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnGuardar.Location = new System.Drawing.Point(404, 47);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(57, 59);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Agregar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupTipoGuia
            // 
            this.groupTipoGuia.Controls.Add(this.cbxTipoGuia);
            this.groupTipoGuia.Location = new System.Drawing.Point(193, 29);
            this.groupTipoGuia.Name = "groupTipoGuia";
            this.groupTipoGuia.Size = new System.Drawing.Size(163, 48);
            this.groupTipoGuia.TabIndex = 1;
            this.groupTipoGuia.TabStop = false;
            this.groupTipoGuia.Text = "TipoGuia";
            // 
            // cbxTipoGuia
            // 
            this.cbxTipoGuia.FormattingEnabled = true;
            this.cbxTipoGuia.Location = new System.Drawing.Point(6, 18);
            this.cbxTipoGuia.Name = "cbxTipoGuia";
            this.cbxTipoGuia.Size = new System.Drawing.Size(151, 21);
            this.cbxTipoGuia.TabIndex = 1;
            this.cbxTipoGuia.SelectedValueChanged += new System.EventHandler(this.cbxTipoGuia_SelectedValueChanged);
            this.cbxTipoGuia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxTipoGuia_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxEmpresasGrupo);
            this.groupBox1.Location = new System.Drawing.Point(9, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Empresa";
            // 
            // cbxEmpresasGrupo
            // 
            this.cbxEmpresasGrupo.FormattingEnabled = true;
            this.cbxEmpresasGrupo.Location = new System.Drawing.Point(7, 19);
            this.cbxEmpresasGrupo.Name = "cbxEmpresasGrupo";
            this.cbxEmpresasGrupo.Size = new System.Drawing.Size(161, 21);
            this.cbxEmpresasGrupo.TabIndex = 0;
            this.cbxEmpresasGrupo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxEmpresasGrupo_KeyPress);
            // 
            // GroupSucursal
            // 
            this.GroupSucursal.Controls.Add(this.txtDescripcion);
            this.GroupSucursal.Location = new System.Drawing.Point(105, 83);
            this.GroupSucursal.Name = "GroupSucursal";
            this.GroupSucursal.Size = new System.Drawing.Size(245, 48);
            this.GroupSucursal.TabIndex = 1;
            this.GroupSucursal.TabStop = false;
            this.GroupSucursal.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(6, 19);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(233, 20);
            this.txtDescripcion.TabIndex = 1;
            // 
            // groupSerie
            // 
            this.groupSerie.Controls.Add(this.txtSeries);
            this.groupSerie.Location = new System.Drawing.Point(9, 83);
            this.groupSerie.Name = "groupSerie";
            this.groupSerie.Size = new System.Drawing.Size(90, 48);
            this.groupSerie.TabIndex = 0;
            this.groupSerie.TabStop = false;
            this.groupSerie.Text = "Serie";
            // 
            // txtSeries
            // 
            this.txtSeries.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSeries.Location = new System.Drawing.Point(6, 19);
            this.txtSeries.MaxLength = 4;
            this.txtSeries.Name = "txtSeries";
            this.txtSeries.Size = new System.Drawing.Size(74, 20);
            this.txtSeries.TabIndex = 0;
            this.txtSeries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSeries_KeyPress);
            // 
            // dtgListaGuias
            // 
            this.dtgListaGuias.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaGuias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaGuias.Location = new System.Drawing.Point(0, 212);
            this.dtgListaGuias.MainView = this.dgvListaGuiaExpressVista;
            this.dtgListaGuias.Name = "dtgListaGuias";
            this.dtgListaGuias.Size = new System.Drawing.Size(594, 273);
            this.dtgListaGuias.TabIndex = 13;
            this.dtgListaGuias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaGuiaExpressVista,
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desactivarToolStripMenuItem,
            this.activarToolStripMenuItem,
            this.vincularAnexoToolStripMenuItem,
            this.vincularUsuarioToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(210, 92);
            // 
            // desactivarToolStripMenuItem
            // 
            this.desactivarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.desactivarToolStripMenuItem.Name = "desactivarToolStripMenuItem";
            this.desactivarToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.desactivarToolStripMenuItem.Text = "Desactivar";
            this.desactivarToolStripMenuItem.Click += new System.EventHandler(this.desactivarToolStripMenuItem_Click);
            // 
            // activarToolStripMenuItem
            // 
            this.activarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.updated1;
            this.activarToolStripMenuItem.Name = "activarToolStripMenuItem";
            this.activarToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.activarToolStripMenuItem.Text = "Activar";
            this.activarToolStripMenuItem.Click += new System.EventHandler(this.activarToolStripMenuItem_Click);
            // 
            // vincularAnexoToolStripMenuItem
            // 
            this.vincularAnexoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.vincularAnexoToolStripMenuItem.Name = "vincularAnexoToolStripMenuItem";
            this.vincularAnexoToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.vincularAnexoToolStripMenuItem.Text = "Vincular Establecimientos";
            this.vincularAnexoToolStripMenuItem.Click += new System.EventHandler(this.vincularAnexoToolStripMenuItem_Click);
            // 
            // vincularUsuarioToolStripMenuItem
            // 
            this.vincularUsuarioToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.bonoseguridad;
            this.vincularUsuarioToolStripMenuItem.Name = "vincularUsuarioToolStripMenuItem";
            this.vincularUsuarioToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.vincularUsuarioToolStripMenuItem.Text = "Vincular Usuarios";
            this.vincularUsuarioToolStripMenuItem.Click += new System.EventHandler(this.vincularUsuarioToolStripMenuItem_Click);
            // 
            // dgvListaGuiaExpressVista
            // 
            this.dgvListaGuiaExpressVista.GridControl = this.dtgListaGuias;
            this.dgvListaGuiaExpressVista.Name = "dgvListaGuiaExpressVista";
            this.dgvListaGuiaExpressVista.OptionsBehavior.Editable = false;
            this.dgvListaGuiaExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaGuiaExpressVista.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.dgvListaGuiaExpressVista.OptionsView.RowAutoHeight = true;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgListaGuias;
            this.gridView1.Name = "gridView1";
            // 
            // frmGestionarSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(594, 485);
            this.Controls.Add(this.dtgListaGuias);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTituloGuia);
            this.Name = "frmGestionarSeries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGestionarSeries";
            this.Load += new System.EventHandler(this.frmGestionarSeries_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupTipoGuia.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.GroupSucursal.ResumeLayout(false);
            this.GroupSucursal.PerformLayout();
            this.groupSerie.ResumeLayout(false);
            this.groupSerie.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuias)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiaExpressVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxEmpresasGrupo;
        private System.Windows.Forms.GroupBox GroupSucursal;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.GroupBox groupSerie;
        private System.Windows.Forms.TextBox txtSeries;
        private DevExpress.XtraGrid.GridControl dtgListaGuias;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaGuiaExpressVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupTipoGuia;
        private System.Windows.Forms.ComboBox cbxTipoGuia;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.CheckBox chkInactivos;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem desactivarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vincularAnexoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vincularUsuarioToolStripMenuItem;
    }
}