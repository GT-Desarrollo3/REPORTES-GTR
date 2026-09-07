namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    partial class frmVerSegundoUsoDesvinculados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVerSegundoUsoDesvinculados));
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dgvVinculo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabSegundoUsoH = new System.Windows.Forms.TabControl();
            this.tabMtto = new System.Windows.Forms.TabPage();
            this.tabLogistica = new System.Windows.Forms.TabPage();
            this.dtgvLogistica = new DevExpress.XtraGrid.GridControl();
            this.dgvVinculoR = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculo)).BeginInit();
            this.tabSegundoUsoH.SuspendLayout();
            this.tabMtto.SuspendLayout();
            this.tabLogistica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLogistica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculoR)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTituloGuia
            // 
            this.lblTituloGuia.BackColor = System.Drawing.Color.DarkTurquoise;
            this.lblTituloGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloGuia.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTituloGuia.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGuia.Name = "lblTituloGuia";
            this.lblTituloGuia.Size = new System.Drawing.Size(1020, 45);
            this.lblTituloGuia.TabIndex = 1;
            this.lblTituloGuia.Text = "HISTORIAL DE ACTIVOS DESVINCULADOS";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgvData
            // 
            this.dtgvData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(3, 3);
            this.dtgvData.LookAndFeel.SkinName = "Office 2007 Blue";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dgvVinculo;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(1006, 408);
            this.dtgvData.TabIndex = 112;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvVinculo});
            // 
            // dgvVinculo
            // 
            this.dgvVinculo.GridControl = this.dtgvData;
            this.dgvVinculo.Name = "dgvVinculo";
            this.dgvVinculo.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvVinculo.OptionsBehavior.Editable = false;
            this.dgvVinculo.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvVinculo.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvVinculo.OptionsView.ColumnAutoWidth = false;
            this.dgvVinculo.OptionsView.ShowFooter = true;
            // 
            // tabSegundoUsoH
            // 
            this.tabSegundoUsoH.Controls.Add(this.tabMtto);
            this.tabSegundoUsoH.Controls.Add(this.tabLogistica);
            this.tabSegundoUsoH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSegundoUsoH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSegundoUsoH.Location = new System.Drawing.Point(0, 45);
            this.tabSegundoUsoH.Name = "tabSegundoUsoH";
            this.tabSegundoUsoH.SelectedIndex = 0;
            this.tabSegundoUsoH.Size = new System.Drawing.Size(1020, 447);
            this.tabSegundoUsoH.TabIndex = 113;
            // 
            // tabMtto
            // 
            this.tabMtto.Controls.Add(this.dtgvData);
            this.tabMtto.Location = new System.Drawing.Point(4, 29);
            this.tabMtto.Name = "tabMtto";
            this.tabMtto.Padding = new System.Windows.Forms.Padding(3);
            this.tabMtto.Size = new System.Drawing.Size(1012, 414);
            this.tabMtto.TabIndex = 0;
            this.tabMtto.Text = "MANTENIMIENTO";
            this.tabMtto.UseVisualStyleBackColor = true;
            // 
            // tabLogistica
            // 
            this.tabLogistica.Controls.Add(this.dtgvLogistica);
            this.tabLogistica.Location = new System.Drawing.Point(4, 29);
            this.tabLogistica.Name = "tabLogistica";
            this.tabLogistica.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogistica.Size = new System.Drawing.Size(1012, 414);
            this.tabLogistica.TabIndex = 1;
            this.tabLogistica.Text = "LOGÍSTICA";
            this.tabLogistica.UseVisualStyleBackColor = true;
            // 
            // dtgvLogistica
            // 
            this.dtgvLogistica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvLogistica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvLogistica.Location = new System.Drawing.Point(3, 3);
            this.dtgvLogistica.LookAndFeel.SkinName = "Office 2007 Green";
            this.dtgvLogistica.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvLogistica.MainView = this.dgvVinculoR;
            this.dtgvLogistica.Name = "dtgvLogistica";
            this.dtgvLogistica.Size = new System.Drawing.Size(1006, 408);
            this.dtgvLogistica.TabIndex = 113;
            this.dtgvLogistica.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvVinculoR});
            // 
            // dgvVinculoR
            // 
            this.dgvVinculoR.GridControl = this.dtgvLogistica;
            this.dgvVinculoR.Name = "dgvVinculoR";
            this.dgvVinculoR.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvVinculoR.OptionsBehavior.Editable = false;
            this.dgvVinculoR.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvVinculoR.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvVinculoR.OptionsView.ColumnAutoWidth = false;
            this.dgvVinculoR.OptionsView.ShowFooter = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(293, 49);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 21);
            this.btnBuscar.TabIndex = 155;
            this.btnBuscar.Text = "Actualizar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // frmVerSegundoUsoDesvinculados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1020, 492);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.tabSegundoUsoH);
            this.Controls.Add(this.lblTituloGuia);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVerSegundoUsoDesvinculados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACTIVOS DESVINCULADOS";
            this.Load += new System.EventHandler(this.frmVerSegundoUsoDesvinculados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculo)).EndInit();
            this.tabSegundoUsoH.ResumeLayout(false);
            this.tabMtto.ResumeLayout(false);
            this.tabLogistica.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLogistica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculoR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvVinculo;
        private System.Windows.Forms.TabControl tabSegundoUsoH;
        private System.Windows.Forms.TabPage tabMtto;
        private System.Windows.Forms.TabPage tabLogistica;
        private DevExpress.XtraGrid.GridControl dtgvLogistica;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvVinculoR;
        public System.Windows.Forms.Button btnBuscar;
    }
}