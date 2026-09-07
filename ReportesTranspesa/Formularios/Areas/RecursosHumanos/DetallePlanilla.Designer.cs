namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class DetallePlanilla
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgvPlanillaDetalle = new DevExpress.XtraGrid.GridControl();
            this.dtgvPlanillaDetalleView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaDetalleView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtgvPlanillaDetalle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 332);
            this.panel1.TabIndex = 0;
            // 
            // dtgvPlanillaDetalle
            // 
            this.dtgvPlanillaDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvPlanillaDetalle.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvPlanillaDetalle.Location = new System.Drawing.Point(0, 0);
            this.dtgvPlanillaDetalle.LookAndFeel.SkinName = "McSkin";
            this.dtgvPlanillaDetalle.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvPlanillaDetalle.MainView = this.dtgvPlanillaDetalleView;
            this.dtgvPlanillaDetalle.Name = "dtgvPlanillaDetalle";
            this.dtgvPlanillaDetalle.Size = new System.Drawing.Size(808, 332);
            this.dtgvPlanillaDetalle.TabIndex = 5;
            this.dtgvPlanillaDetalle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvPlanillaDetalleView});
            // 
            // dtgvPlanillaDetalleView
            // 
            this.dtgvPlanillaDetalleView.GridControl = this.dtgvPlanillaDetalle;
            this.dtgvPlanillaDetalleView.Name = "dtgvPlanillaDetalleView";
            this.dtgvPlanillaDetalleView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvPlanillaDetalleView.OptionsBehavior.Editable = false;
            this.dtgvPlanillaDetalleView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvPlanillaDetalleView.OptionsSelection.MultiSelect = true;
            this.dtgvPlanillaDetalleView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dtgvPlanillaDetalleView.OptionsView.ColumnAutoWidth = false;
            this.dtgvPlanillaDetalleView.OptionsView.ShowFooter = true;
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPeriodo.Location = new System.Drawing.Point(389, 29);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(42, 13);
            this.lblPeriodo.TabIndex = 1;
            this.lblPeriodo.Text = "periodo";
            // 
            // DetallePlanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 412);
            this.Controls.Add(this.lblPeriodo);
            this.Controls.Add(this.panel1);
            this.Name = "DetallePlanilla";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Detalle de Planilla";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.DetallePlanilla_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPlanillaDetalleView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl dtgvPlanillaDetalle;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvPlanillaDetalleView;
        private System.Windows.Forms.Label lblPeriodo;
    }
}