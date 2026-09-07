namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class Detalle_Mantenimiento
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.dtgvMantenimiento = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataViewMant = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMantenimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataViewMant)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvMantenimiento
            // 
            this.dtgvMantenimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.dtgvMantenimiento.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvMantenimiento.Location = new System.Drawing.Point(20, 60);
            this.dtgvMantenimiento.LookAndFeel.SkinName = "Darkroom";
            this.dtgvMantenimiento.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvMantenimiento.MainView = this.dtgvDataViewMant;
            this.dtgvMantenimiento.Name = "dtgvMantenimiento";
            this.dtgvMantenimiento.Size = new System.Drawing.Size(803, 461);
            this.dtgvMantenimiento.TabIndex = 5;
            this.dtgvMantenimiento.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataViewMant});
            // 
            // dtgvDataViewMant
            // 
            this.dtgvDataViewMant.GridControl = this.dtgvMantenimiento;
            this.dtgvDataViewMant.Name = "dtgvDataViewMant";
            this.dtgvDataViewMant.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataViewMant.OptionsBehavior.Editable = false;
            this.dtgvDataViewMant.OptionsView.ColumnAutoWidth = false;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.Name = "sqlDataSource1";
            // 
            // Detalle_Mantenimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 541);
            this.Controls.Add(this.dtgvMantenimiento);
            this.Name = "Detalle_Mantenimiento";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Detalle Mantenimiento";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Detalle_Mantenimiento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMantenimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataViewMant)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgvMantenimiento;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataViewMant;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    }
}