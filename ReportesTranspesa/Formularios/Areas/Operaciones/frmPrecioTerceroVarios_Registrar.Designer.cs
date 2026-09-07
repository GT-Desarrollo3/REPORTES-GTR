namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmPrecioTerceroVarios_Registrar
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvBotiquinUnidad = new DevExpress.XtraGrid.GridControl();
            this.dtgvBotiquinUnidadView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidadView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1047, 37);
            this.label2.TabIndex = 21;
            this.label2.Text = "PRECIO TERCEROS PROGRAMACION VARIOS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidad.Location = new System.Drawing.Point(86, 18);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(215, 21);
            this.txtCantidad.TabIndex = 227;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(35, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 228;
            this.label3.Text = "Cliente";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(332, 18);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 229;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtgvBotiquinUnidad
            // 
            this.dtgvBotiquinUnidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvBotiquinUnidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvBotiquinUnidad.Location = new System.Drawing.Point(0, 0);
            this.dtgvBotiquinUnidad.LookAndFeel.SkinMaskColor = System.Drawing.Color.Red;
            this.dtgvBotiquinUnidad.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Red;
            this.dtgvBotiquinUnidad.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgvBotiquinUnidad.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvBotiquinUnidad.MainView = this.dtgvBotiquinUnidadView;
            this.dtgvBotiquinUnidad.Name = "dtgvBotiquinUnidad";
            this.dtgvBotiquinUnidad.Size = new System.Drawing.Size(1047, 524);
            this.dtgvBotiquinUnidad.TabIndex = 230;
            this.dtgvBotiquinUnidad.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvBotiquinUnidadView});
            // 
            // dtgvBotiquinUnidadView
            // 
            this.dtgvBotiquinUnidadView.GridControl = this.dtgvBotiquinUnidad;
            this.dtgvBotiquinUnidadView.Name = "dtgvBotiquinUnidadView";
            this.dtgvBotiquinUnidadView.OptionsBehavior.Editable = false;
            this.dtgvBotiquinUnidadView.OptionsView.ColumnAutoWidth = false;
            this.dtgvBotiquinUnidadView.OptionsView.RowAutoHeight = true;
            this.dtgvBotiquinUnidadView.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCantidad);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1047, 100);
            this.panel1.TabIndex = 231;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgvBotiquinUnidad);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1047, 524);
            this.panel2.TabIndex = 232;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(35, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 230;
            this.label1.Text = "Cliente";
            // 
            // frmPrecioTerceroVarios_Registrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1047, 661);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "frmPrecioTerceroVarios_Registrar";
            this.Text = "frmPrecioTerceroVarios_Registrar";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidadView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgvBotiquinUnidad;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvBotiquinUnidadView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
    }
}