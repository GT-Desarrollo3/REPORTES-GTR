namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmNuevoTipoEPPS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevoTipoEPPS));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtgTipoEPPS = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activarEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvTipoEPPS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtNombreEPPS = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTipoEPPS)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTipoEPPS)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.dtgTipoEPPS);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // dtgTipoEPPS
            // 
            this.dtgTipoEPPS.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgTipoEPPS.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.dtgTipoEPPS, "dtgTipoEPPS");
            this.dtgTipoEPPS.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgTipoEPPS.MainView = this.dgvTipoEPPS;
            this.dtgTipoEPPS.Name = "dtgTipoEPPS";
            this.dtgTipoEPPS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvTipoEPPS});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activarEstadoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // activarEstadoToolStripMenuItem
            // 
            this.activarEstadoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.liberar;
            this.activarEstadoToolStripMenuItem.Name = "activarEstadoToolStripMenuItem";
            resources.ApplyResources(this.activarEstadoToolStripMenuItem, "activarEstadoToolStripMenuItem");
            this.activarEstadoToolStripMenuItem.Click += new System.EventHandler(this.activarEstadoToolStripMenuItem_Click_1);
            // 
            // editarToolStripMenuItem
            // 
            resources.ApplyResources(this.editarToolStripMenuItem, "editarToolStripMenuItem");
            this.editarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            resources.ApplyResources(this.eliminarToolStripMenuItem, "eliminarToolStripMenuItem");
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dgvTipoEPPS
            // 
            this.dgvTipoEPPS.GridControl = this.dtgTipoEPPS;
            this.dgvTipoEPPS.Name = "dgvTipoEPPS";
            this.dgvTipoEPPS.OptionsBehavior.Editable = false;
            this.dgvTipoEPPS.OptionsView.ShowGroupPanel = false;
            this.dgvTipoEPPS.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.dgvTipoEPPS_RowCellClick);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtNombreEPPS);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtNombreEPPS
            // 
            this.txtNombreEPPS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtNombreEPPS, "txtNombreEPPS");
            this.txtNombreEPPS.Name = "txtNombreEPPS";
            this.txtNombreEPPS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreEPPS_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // frmNuevoTipoEPPS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNuevoTipoEPPS";
            this.Load += new System.EventHandler(this.frmNuevoTipoEPPS_Load);
            this.Shown += new System.EventHandler(this.frmNuevoTipoEPPS_Shown);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTipoEPPS)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTipoEPPS)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNombreEPPS;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgTipoEPPS;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvTipoEPPS;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activarEstadoToolStripMenuItem;
    }
}