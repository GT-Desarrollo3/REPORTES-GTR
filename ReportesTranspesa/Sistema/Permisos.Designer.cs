namespace ReportesTranspesa.Sistema
{
    partial class Permisos
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Permisos));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.coparPermisosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgvData2 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.permisosEspecialesMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permisosEspecialesUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvDataView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnGrabar = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnCopiar = new MetroFramework.Controls.MetroButton();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.lstPersonal = new System.Windows.Forms.ListView();
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgvData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData2);
            this.splitContainer1.Size = new System.Drawing.Size(858, 445);
            this.splitContainer1.SplitterDistance = 428;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtgvData
            // 
            this.dtgvData.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvData.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Office 2007 Silver";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(428, 445);
            this.dtgvData.TabIndex = 4;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coparPermisosToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(158, 26);
            // 
            // coparPermisosToolStripMenuItem
            // 
            this.coparPermisosToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.actualizarGuia;
            this.coparPermisosToolStripMenuItem.Name = "coparPermisosToolStripMenuItem";
            this.coparPermisosToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.coparPermisosToolStripMenuItem.Text = "Copar Permisos";
            this.coparPermisosToolStripMenuItem.Click += new System.EventHandler(this.coparPermisosToolStripMenuItem_Click);
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowGroupPanel = false;
            this.dtgvDataView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dtgvDataView_FocusedRowChanged);
            // 
            // dtgvData2
            // 
            this.dtgvData2.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgvData2.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.dtgvData2.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvData2.Location = new System.Drawing.Point(0, 0);
            this.dtgvData2.LookAndFeel.SkinName = "Office 2007 Silver";
            this.dtgvData2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData2.MainView = this.dtgvDataView2;
            this.dtgvData2.Name = "dtgvData2";
            this.dtgvData2.Size = new System.Drawing.Size(426, 445);
            this.dtgvData2.TabIndex = 4;
            this.dtgvData2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView2});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.permisosEspecialesMasterToolStripMenuItem,
            this.permisosEspecialesUsuarioToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(218, 48);
            // 
            // permisosEspecialesMasterToolStripMenuItem
            // 
            this.permisosEspecialesMasterToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.ordenar;
            this.permisosEspecialesMasterToolStripMenuItem.Name = "permisosEspecialesMasterToolStripMenuItem";
            this.permisosEspecialesMasterToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.permisosEspecialesMasterToolStripMenuItem.Text = "Maestro de Permisos";
            this.permisosEspecialesMasterToolStripMenuItem.Click += new System.EventHandler(this.permisosEspecialesMasterToolStripMenuItem_Click);
            // 
            // permisosEspecialesUsuarioToolStripMenuItem
            // 
            this.permisosEspecialesUsuarioToolStripMenuItem.DoubleClickEnabled = true;
            this.permisosEspecialesUsuarioToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.bonoseguridadicono;
            this.permisosEspecialesUsuarioToolStripMenuItem.Name = "permisosEspecialesUsuarioToolStripMenuItem";
            this.permisosEspecialesUsuarioToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.permisosEspecialesUsuarioToolStripMenuItem.Text = "Asignar Permisos a Usuario";
            this.permisosEspecialesUsuarioToolStripMenuItem.Click += new System.EventHandler(this.permisosEspecialesUsuarioToolStripMenuItem_Click2);
            // 
            // dtgvDataView2
            // 
            this.dtgvDataView2.GridControl = this.dtgvData2;
            this.dtgvDataView2.Name = "dtgvDataView2";
            this.dtgvDataView2.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView2.OptionsBehavior.Editable = false;
            this.dtgvDataView2.OptionsSelection.MultiSelect = true;
            this.dtgvDataView2.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.dtgvDataView2.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.True;
            this.dtgvDataView2.OptionsSelection.UseIndicatorForSelection = false;
            this.dtgvDataView2.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView2.OptionsView.ShowGroupPanel = false;
            this.dtgvDataView2.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dtgvDataView2_RowCellClick);
            // 
            // btnGrabar
            // 
            this.btnGrabar.BackColor = System.Drawing.Color.Silver;
            this.btnGrabar.Location = new System.Drawing.Point(136, 26);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(135, 28);
            this.btnGrabar.TabIndex = 1;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseSelectable = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Controls.Add(this.btnCopiar);
            this.panel1.Controls.Add(this.txtPersonal);
            this.panel1.Controls.Add(this.lstPersonal);
            this.panel1.Controls.Add(this.lblTituloGuia);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(243, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 265);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(436, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(32, 30);
            this.btnCerrar.TabIndex = 96;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(0, 30);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(469, 37);
            this.lblUsuario.TabIndex = 95;
            this.lblUsuario.Text = "JUAN PEREZ ALVERTO";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCopiar
            // 
            this.btnCopiar.BackColor = System.Drawing.Color.Silver;
            this.btnCopiar.Location = new System.Drawing.Point(320, 107);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(135, 28);
            this.btnCopiar.TabIndex = 3;
            this.btnCopiar.Text = "COPIAR";
            this.btnCopiar.UseSelectable = true;
            this.btnCopiar.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // txtPersonal
            // 
            this.txtPersonal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPersonal.Location = new System.Drawing.Point(20, 110);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(280, 20);
            this.txtPersonal.TabIndex = 93;
            this.txtPersonal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPersonal_KeyUp);
            // 
            // lstPersonal
            // 
            this.lstPersonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPersonal.ForeColor = System.Drawing.Color.Navy;
            this.lstPersonal.FullRowSelect = true;
            this.lstPersonal.GridLines = true;
            this.lstPersonal.Location = new System.Drawing.Point(20, 130);
            this.lstPersonal.MultiSelect = false;
            this.lstPersonal.Name = "lstPersonal";
            this.lstPersonal.Size = new System.Drawing.Size(287, 10);
            this.lstPersonal.TabIndex = 94;
            this.lstPersonal.UseCompatibleStateImageBehavior = false;
            this.lstPersonal.View = System.Windows.Forms.View.Details;
            this.lstPersonal.Visible = false;
            this.lstPersonal.Enter += new System.EventHandler(this.lstPersonal_Enter);
            this.lstPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersonal_KeyPress);
            this.lstPersonal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstPersonal_KeyUp);
            // 
            // lblTituloGuia
            // 
            this.lblTituloGuia.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblTituloGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloGuia.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTituloGuia.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGuia.Name = "lblTituloGuia";
            this.lblTituloGuia.Size = new System.Drawing.Size(469, 30);
            this.lblTituloGuia.TabIndex = 2;
            this.lblTituloGuia.Text = "NUEVO USUARIO";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empleado Nuevo";
            // 
            // Permisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 525);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Permisos";
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Permisos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPermisos_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroButton btnGrabar;
        private DevExpress.XtraGrid.GridControl dtgvData2;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem permisosEspecialesMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permisosEspecialesUsuarioToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.Label lblUsuario;
        private MetroFramework.Controls.MetroButton btnCopiar;
        public System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.ListView lstPersonal;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem coparPermisosToolStripMenuItem;
        private System.Windows.Forms.Button btnCerrar;
    }
}