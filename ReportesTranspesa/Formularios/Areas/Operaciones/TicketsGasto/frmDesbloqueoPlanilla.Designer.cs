namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmDesbloqueoPlanilla
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesbloqueoPlanilla));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpFechaCompromiso = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.lstConductor = new System.Windows.Forms.ListView();
            this.dtgListaDesbloqueo = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsQuitarDesbloqueo = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvListaDesbloqueo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaDesbloqueo)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaDesbloqueo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(602, 42);
            this.label1.TabIndex = 21;
            this.label1.Text = "DESBLOQUEAR CONDUCTOR POR PLANILLA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.dtpFechaCompromiso);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.txtConductor);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.btnGuardar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(602, 91);
            this.panel3.TabIndex = 183;
            // 
            // dtpFechaCompromiso
            // 
            this.dtpFechaCompromiso.CustomFormat = "dd-MM-yyyy";
            this.dtpFechaCompromiso.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCompromiso.Location = new System.Drawing.Point(155, 52);
            this.dtpFechaCompromiso.Name = "dtpFechaCompromiso";
            this.dtpFechaCompromiso.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaCompromiso.TabIndex = 200;
            this.dtpFechaCompromiso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaCompromiso_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(16, 54);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(133, 15);
            this.label21.TabIndex = 199;
            this.label21.Text = "F. Bloqueo Automático:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardar.Location = new System.Drawing.Point(486, 25);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(42, 42);
            this.btnGuardar.TabIndex = 197;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtConductor
            // 
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductor.Location = new System.Drawing.Point(88, 20);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(371, 20);
            this.txtConductor.TabIndex = 196;
            this.txtConductor.Enter += new System.EventHandler(this.txtConductor_Enter);
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            this.txtConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtConductor_KeyUp);
            this.txtConductor.Leave += new System.EventHandler(this.txtConductor_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(16, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 15);
            this.label18.TabIndex = 195;
            this.label18.Text = "Conductor:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(539, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(42, 42);
            this.btnBuscar.TabIndex = 201;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lstConductor
            // 
            this.lstConductor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductor.ForeColor = System.Drawing.Color.Navy;
            this.lstConductor.FullRowSelect = true;
            this.lstConductor.GridLines = true;
            this.lstConductor.Location = new System.Drawing.Point(91, 80);
            this.lstConductor.MultiSelect = false;
            this.lstConductor.Name = "lstConductor";
            this.lstConductor.Size = new System.Drawing.Size(371, 127);
            this.lstConductor.TabIndex = 198;
            this.lstConductor.UseCompatibleStateImageBehavior = false;
            this.lstConductor.View = System.Windows.Forms.View.Details;
            this.lstConductor.Visible = false;
            this.lstConductor.Enter += new System.EventHandler(this.lstConductor_Enter);
            this.lstConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductor_KeyPress);
            this.lstConductor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstConductor_MouseDoubleClick);
            // 
            // dtgListaDesbloqueo
            // 
            this.dtgListaDesbloqueo.ContextMenuStrip = this.contextMenuStrip3;
            this.dtgListaDesbloqueo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaDesbloqueo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaDesbloqueo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgListaDesbloqueo.Location = new System.Drawing.Point(0, 133);
            this.dtgListaDesbloqueo.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgListaDesbloqueo.MainView = this.dtgvListaDesbloqueo;
            this.dtgListaDesbloqueo.Name = "dtgListaDesbloqueo";
            this.dtgListaDesbloqueo.Size = new System.Drawing.Size(602, 259);
            this.dtgListaDesbloqueo.TabIndex = 199;
            this.dtgListaDesbloqueo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvListaDesbloqueo});
            this.dtgListaDesbloqueo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgListaDesbloqueo_MouseUp);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsQuitarDesbloqueo});
            this.contextMenuStrip3.Name = "contextMenuStrip1";
            this.contextMenuStrip3.Size = new System.Drawing.Size(174, 26);
            // 
            // tsQuitarDesbloqueo
            // 
            this.tsQuitarDesbloqueo.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsQuitarDesbloqueo.Name = "tsQuitarDesbloqueo";
            this.tsQuitarDesbloqueo.Size = new System.Drawing.Size(173, 22);
            this.tsQuitarDesbloqueo.Text = "Quitar Desbloqueo";
            this.tsQuitarDesbloqueo.Click += new System.EventHandler(this.tsQuitarDesbloqueo_Click);
            // 
            // dtgvListaDesbloqueo
            // 
            this.dtgvListaDesbloqueo.GridControl = this.dtgListaDesbloqueo;
            this.dtgvListaDesbloqueo.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "TIEMPO", null, "Total: {HH:mm:ss}", new System.DateTime(2024, 6, 20, 17, 14, 48, 220))});
            this.dtgvListaDesbloqueo.Name = "dtgvListaDesbloqueo";
            this.dtgvListaDesbloqueo.OptionsBehavior.Editable = false;
            this.dtgvListaDesbloqueo.OptionsView.ColumnAutoWidth = false;
            this.dtgvListaDesbloqueo.OptionsView.RowAutoHeight = true;
            this.dtgvListaDesbloqueo.OptionsView.ShowGroupPanel = false;
            // 
            // frmDesbloqueoPlanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 392);
            this.Controls.Add(this.dtgListaDesbloqueo);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstConductor);
            this.MaximizeBox = false;
            this.Name = "frmDesbloqueoPlanilla";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DESBLOQUEAR CONDUCTOR POR PLANILLA";
            this.Load += new System.EventHandler(this.frmDesbloqueoPlanilla_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaDesbloqueo)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaDesbloqueo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpFechaCompromiso;
        private System.Windows.Forms.Label label21;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Label label18;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ListView lstConductor;
        private DevExpress.XtraGrid.GridControl dtgListaDesbloqueo;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvListaDesbloqueo;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem tsQuitarDesbloqueo;
    }
}