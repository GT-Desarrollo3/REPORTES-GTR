namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmBonoConductoresDetalle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBonoConductoresDetalle));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgvAbastecimientos = new DevExpress.XtraGrid.GridControl();
            this.dtgvAbastecimientosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnPunateje = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tolTitulo = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAbastecimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAbastecimientosView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Conductor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Desde:";
            // 
            // dtgvAbastecimientos
            // 
            this.dtgvAbastecimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvAbastecimientos.Location = new System.Drawing.Point(9, 85);
            this.dtgvAbastecimientos.MainView = this.dtgvAbastecimientosView;
            this.dtgvAbastecimientos.Name = "dtgvAbastecimientos";
            this.dtgvAbastecimientos.Size = new System.Drawing.Size(1259, 563);
            this.dtgvAbastecimientos.TabIndex = 14;
            this.dtgvAbastecimientos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvAbastecimientosView});
            // 
            // dtgvAbastecimientosView
            // 
            this.dtgvAbastecimientosView.Appearance.HeaderPanel.BackColor = System.Drawing.Color.White;
            this.dtgvAbastecimientosView.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.dtgvAbastecimientosView.GridControl = this.dtgvAbastecimientos;
            this.dtgvAbastecimientosView.Name = "dtgvAbastecimientosView";
            this.dtgvAbastecimientosView.OptionsPrint.AutoWidth = false;
            this.dtgvAbastecimientosView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.dtgvAbastecimientosView.OptionsView.ColumnAutoWidth = false;
            this.dtgvAbastecimientosView.OptionsView.ShowFooter = true;
            this.dtgvAbastecimientosView.OptionsView.ShowGroupPanel = false;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(1186, 36);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 37);
            this.btnExcel.TabIndex = 52;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(847, 9);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 53;
            this.lblTotal.Text = "Total:";
            // 
            // btnPunateje
            // 
            this.btnPunateje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPunateje.Location = new System.Drawing.Point(642, 0);
            this.btnPunateje.Name = "btnPunateje";
            this.btnPunateje.Size = new System.Drawing.Size(90, 28);
            this.btnPunateje.TabIndex = 55;
            this.btnPunateje.Text = "0";
            this.btnPunateje.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Khaki;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolTitulo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1278, 28);
            this.toolStrip1.TabIndex = 56;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tolTitulo
            // 
            this.tolTitulo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tolTitulo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tolTitulo.Name = "tolTitulo";
            this.tolTitulo.Size = new System.Drawing.Size(253, 25);
            this.tolTitulo.Text = "DETALLE CALIFICATIVO DE:";
            // 
            // frmBonoConductoresDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1278, 657);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnPunateje);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dtgvAbastecimientos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmBonoConductoresDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalles:";
            this.Load += new System.EventHandler(this.frmBonoConductoresDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAbastecimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAbastecimientosView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtgvAbastecimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvAbastecimientosView;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnPunateje;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tolTitulo;
    }
}