namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmListaSolicitudDeCambios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaSolicitudDeCambios));
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumeroGuia = new System.Windows.Forms.TextBox();
            this.btnBuscarGuia = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSerieGuia = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNuevaSolicitud = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgListaGuiasTransportista = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.atenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListaGuiaTraspExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuiasTransportista)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiaTraspExpressVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(908, 41);
            this.label2.TabIndex = 49;
            this.label2.Text = "LISTAR SOLICITUD CAMBIOS EN GUIA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNumeroGuia
            // 
            this.txtNumeroGuia.Location = new System.Drawing.Point(161, 31);
            this.txtNumeroGuia.Name = "txtNumeroGuia";
            this.txtNumeroGuia.Size = new System.Drawing.Size(75, 20);
            this.txtNumeroGuia.TabIndex = 99;
            // 
            // btnBuscarGuia
            // 
            this.btnBuscarGuia.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarGuia.Image")));
            this.btnBuscarGuia.Location = new System.Drawing.Point(265, 22);
            this.btnBuscarGuia.Name = "btnBuscarGuia";
            this.btnBuscarGuia.Size = new System.Drawing.Size(97, 37);
            this.btnBuscarGuia.TabIndex = 102;
            this.btnBuscarGuia.Text = "BUSCAR";
            this.btnBuscarGuia.Click += new System.EventHandler(this.btnBuscarGuia_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxSerieGuia);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnNuevaSolicitud);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnBuscarGuia);
            this.groupBox1.Controls.Add(this.txtNumeroGuia);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 77);
            this.groupBox1.TabIndex = 103;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros Datos";
            // 
            // cbxSerieGuia
            // 
            this.cbxSerieGuia.FormattingEnabled = true;
            this.cbxSerieGuia.Location = new System.Drawing.Point(40, 31);
            this.cbxSerieGuia.Name = "cbxSerieGuia";
            this.cbxSerieGuia.Size = new System.Drawing.Size(68, 21);
            this.cbxSerieGuia.TabIndex = 108;
            this.cbxSerieGuia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxSerieGuia_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Serie:";
            // 
            // btnNuevaSolicitud
            // 
            this.btnNuevaSolicitud.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaSolicitud.Image")));
            this.btnNuevaSolicitud.Location = new System.Drawing.Point(389, 22);
            this.btnNuevaSolicitud.Name = "btnNuevaSolicitud";
            this.btnNuevaSolicitud.Size = new System.Drawing.Size(132, 35);
            this.btnNuevaSolicitud.TabIndex = 106;
            this.btnNuevaSolicitud.Text = "NUEVA SOLICITUD";
            this.btnNuevaSolicitud.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 105;
            this.label3.Text = "Numero:";
            // 
            // dtgListaGuiasTransportista
            // 
            this.dtgListaGuiasTransportista.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgListaGuiasTransportista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaGuiasTransportista.Location = new System.Drawing.Point(0, 118);
            this.dtgListaGuiasTransportista.MainView = this.dgvListaGuiaTraspExpressVista;
            this.dtgListaGuiasTransportista.Name = "dtgListaGuiasTransportista";
            this.dtgListaGuiasTransportista.Size = new System.Drawing.Size(908, 300);
            this.dtgListaGuiasTransportista.TabIndex = 104;
            this.dtgListaGuiasTransportista.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaGuiaTraspExpressVista,
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atenderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 26);
            // 
            // atenderToolStripMenuItem
            // 
            this.atenderToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.liberar;
            this.atenderToolStripMenuItem.Name = "atenderToolStripMenuItem";
            this.atenderToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.atenderToolStripMenuItem.Text = "Atender Solicitud";
            this.atenderToolStripMenuItem.Visible = false;
            this.atenderToolStripMenuItem.Click += new System.EventHandler(this.atenderToolStripMenuItem_Click);
            // 
            // dgvListaGuiaTraspExpressVista
            // 
            this.dgvListaGuiaTraspExpressVista.GridControl = this.dtgListaGuiasTransportista;
            this.dgvListaGuiaTraspExpressVista.Name = "dgvListaGuiaTraspExpressVista";
            this.dgvListaGuiaTraspExpressVista.OptionsBehavior.Editable = false;
            this.dgvListaGuiaTraspExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvListaGuiaTraspExpressVista.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.dgvListaGuiaTraspExpressVista.OptionsView.RowAutoHeight = true;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgListaGuiasTransportista;
            this.gridView1.Name = "gridView1";
            // 
            // frmListaSolicitudDeCambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(908, 418);
            this.Controls.Add(this.dtgListaGuiasTransportista);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Name = "frmListaSolicitudDeCambios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListaSolicitudDeCambios";
            this.Load += new System.EventHandler(this.frmListaSolicitudDeCambios_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaGuiasTransportista)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaGuiaTraspExpressVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnBuscarGuia;
        private System.Windows.Forms.TextBox txtNumeroGuia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl dtgListaGuiasTransportista;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaGuiaTraspExpressVista;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnNuevaSolicitud;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxSerieGuia;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem atenderToolStripMenuItem;
    }
}