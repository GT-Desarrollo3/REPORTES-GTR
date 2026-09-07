namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    partial class frmDesbloqueoKit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesbloqueoKit));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTracto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaCompromiso = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgRutaKN = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsQuitarDesbloqueo = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvRutaKN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lstRuta = new System.Windows.Forms.ListView();
            this.lstTracto = new System.Windows.Forms.ListView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRutaKN)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRutaKN)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MediumPurple;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(602, 42);
            this.label1.TabIndex = 22;
            this.label1.Text = "DESBLOQUEAR UNIDAD POR KIT DE NEUMÁTICO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.txtTracto);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.dtpFechaCompromiso);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.btnGuardar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(602, 76);
            this.panel3.TabIndex = 184;
            // 
            // txtTracto
            // 
            this.txtTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTracto.Location = new System.Drawing.Point(66, 28);
            this.txtTracto.Name = "txtTracto";
            this.txtTracto.Size = new System.Drawing.Size(123, 20);
            this.txtTracto.TabIndex = 100;
            this.txtTracto.Enter += new System.EventHandler(this.txtTracto_Enter);
            this.txtTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTracto_KeyPress);
            this.txtTracto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTracto_KeyUp);
            this.txtTracto.Leave += new System.EventHandler(this.txtTracto_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 202;
            this.label2.Text = "Tracto:";
            // 
            // dtpFechaCompromiso
            // 
            this.dtpFechaCompromiso.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaCompromiso.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCompromiso.Location = new System.Drawing.Point(340, 28);
            this.dtpFechaCompromiso.Name = "dtpFechaCompromiso";
            this.dtpFechaCompromiso.Size = new System.Drawing.Size(95, 20);
            this.dtpFechaCompromiso.TabIndex = 200;
            this.dtpFechaCompromiso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaCompromiso_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(214, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(120, 30);
            this.label21.TabIndex = 199;
            this.label21.Text = "Fecha\r\nBloqueo Automático:";
            // 
            // txtRuta
            // 
            this.txtRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRuta.Location = new System.Drawing.Point(64, 59);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(393, 20);
            this.txtRuta.TabIndex = 196;
            this.txtRuta.Visible = false;
            this.txtRuta.Enter += new System.EventHandler(this.txtRuta_Enter);
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            this.txtRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRuta_KeyUp);
            this.txtRuta.Leave += new System.EventHandler(this.txtRuta_Leave);
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
            this.btnBuscar.Location = new System.Drawing.Point(528, 17);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(42, 42);
            this.btnBuscar.TabIndex = 201;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.btnGuardar.Location = new System.Drawing.Point(475, 17);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(42, 42);
            this.btnGuardar.TabIndex = 197;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dtgRutaKN
            // 
            this.dtgRutaKN.ContextMenuStrip = this.contextMenuStrip3;
            this.dtgRutaKN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRutaKN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRutaKN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgRutaKN.Location = new System.Drawing.Point(0, 118);
            this.dtgRutaKN.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgRutaKN.MainView = this.dtgvRutaKN;
            this.dtgRutaKN.Name = "dtgRutaKN";
            this.dtgRutaKN.Size = new System.Drawing.Size(602, 254);
            this.dtgRutaKN.TabIndex = 200;
            this.dtgRutaKN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvRutaKN});
            this.dtgRutaKN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgRutaKN_MouseUp);
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
            // dtgvRutaKN
            // 
            this.dtgvRutaKN.GridControl = this.dtgRutaKN;
            this.dtgvRutaKN.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "TIEMPO", null, "Total: {HH:mm:ss}", new System.DateTime(2024, 6, 20, 17, 14, 48, 220))});
            this.dtgvRutaKN.Name = "dtgvRutaKN";
            this.dtgvRutaKN.OptionsBehavior.Editable = false;
            this.dtgvRutaKN.OptionsView.ColumnAutoWidth = false;
            this.dtgvRutaKN.OptionsView.RowAutoHeight = true;
            this.dtgvRutaKN.OptionsView.ShowGroupPanel = false;
            // 
            // lstRuta
            // 
            this.lstRuta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRuta.ForeColor = System.Drawing.Color.Navy;
            this.lstRuta.FullRowSelect = true;
            this.lstRuta.GridLines = true;
            this.lstRuta.Location = new System.Drawing.Point(64, 81);
            this.lstRuta.MultiSelect = false;
            this.lstRuta.Name = "lstRuta";
            this.lstRuta.Size = new System.Drawing.Size(393, 145);
            this.lstRuta.TabIndex = 202;
            this.lstRuta.UseCompatibleStateImageBehavior = false;
            this.lstRuta.View = System.Windows.Forms.View.Details;
            this.lstRuta.Visible = false;
            this.lstRuta.Enter += new System.EventHandler(this.lstRuta_Enter);
            this.lstRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRuta_KeyPress);
            this.lstRuta.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRuta_MouseDoubleClick);
            // 
            // lstTracto
            // 
            this.lstTracto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTracto.ForeColor = System.Drawing.Color.Navy;
            this.lstTracto.FullRowSelect = true;
            this.lstTracto.GridLines = true;
            this.lstTracto.Location = new System.Drawing.Point(66, 89);
            this.lstTracto.MultiSelect = false;
            this.lstTracto.Name = "lstTracto";
            this.lstTracto.Size = new System.Drawing.Size(123, 113);
            this.lstTracto.TabIndex = 239;
            this.lstTracto.UseCompatibleStateImageBehavior = false;
            this.lstTracto.View = System.Windows.Forms.View.Details;
            this.lstTracto.Visible = false;
            this.lstTracto.Enter += new System.EventHandler(this.lstTracto_Enter);
            this.lstTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTracto_KeyPress);
            this.lstTracto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTracto_MouseDoubleClick);
            // 
            // frmDesbloqueoKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 372);
            this.Controls.Add(this.dtgRutaKN);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstRuta);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.lstTracto);
            this.MaximizeBox = false;
            this.Name = "frmDesbloqueoKit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DESBLOQUEAR UNIDAD POR KIT DE NEUMÁTICO";
            this.Load += new System.EventHandler(this.frmDesbloqueoKit_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRutaKN)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRutaKN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaCompromiso;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtRuta;
        public DevExpress.XtraEditors.SimpleButton btnBuscar;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.TextBox txtTracto;
        private DevExpress.XtraGrid.GridControl dtgRutaKN;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvRutaKN;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem tsQuitarDesbloqueo;
        private System.Windows.Forms.ListView lstRuta;
        private System.Windows.Forms.ListView lstTracto;
    }
}