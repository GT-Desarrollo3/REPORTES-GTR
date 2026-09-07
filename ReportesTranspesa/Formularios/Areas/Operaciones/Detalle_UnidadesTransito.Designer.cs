namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class Detalle_UnidadesTransito
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
            this.dtgvUnidadTransito = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataViewUnidTransito = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblmensaje = new System.Windows.Forms.Label();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvUnidadTransito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataViewUnidTransito)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvUnidadTransito
            // 
            this.dtgvUnidadTransito.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.dtgvUnidadTransito.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.dtgvUnidadTransito.Location = new System.Drawing.Point(20, 60);
            this.dtgvUnidadTransito.LookAndFeel.SkinName = "Darkroom";
            this.dtgvUnidadTransito.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvUnidadTransito.MainView = this.dtgvDataViewUnidTransito;
            this.dtgvUnidadTransito.Name = "dtgvUnidadTransito";
            this.dtgvUnidadTransito.Size = new System.Drawing.Size(823, 410);
            this.dtgvUnidadTransito.TabIndex = 6;
            this.dtgvUnidadTransito.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataViewUnidTransito});
            // 
            // dtgvDataViewUnidTransito
            // 
            this.dtgvDataViewUnidTransito.GridControl = this.dtgvUnidadTransito;
            this.dtgvDataViewUnidTransito.Name = "dtgvDataViewUnidTransito";
            this.dtgvDataViewUnidTransito.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataViewUnidTransito.OptionsBehavior.Editable = false;
            this.dtgvDataViewUnidTransito.OptionsView.ColumnAutoWidth = false;
            // 
            // lblmensaje
            // 
            this.lblmensaje.AutoSize = true;
            this.lblmensaje.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblmensaje.Location = new System.Drawing.Point(661, 36);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(35, 13);
            this.lblmensaje.TabIndex = 7;
            this.lblmensaje.Text = "label1";
            // 
            // btnbuscar
            // 
            this.btnbuscar.Location = new System.Drawing.Point(580, 31);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(75, 23);
            this.btnbuscar.TabIndex = 8;
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.UseVisualStyleBackColor = true;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(458, 25);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(103, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 68;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Detalle_UnidadesTransito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 490);
            this.Controls.Add(this.dtpFechaIni);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.lblmensaje);
            this.Controls.Add(this.dtgvUnidadTransito);
            this.Name = "Detalle_UnidadesTransito";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Unidades en Transito desde 5 dias antes";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Detalle_UnidadesTransito_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvUnidadTransito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataViewUnidTransito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgvUnidadTransito;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataViewUnidTransito;
        private System.Windows.Forms.Label lblmensaje;
        private System.Windows.Forms.Button btnbuscar;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
    }
}