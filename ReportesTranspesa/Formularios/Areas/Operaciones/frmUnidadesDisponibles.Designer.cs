namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmUnidadesDisponibles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnidadesDisponibles));
            this.label2 = new System.Windows.Forms.Label();
            this.dtgListaTractos = new DevExpress.XtraGrid.GridControl();
            this.dgvListaTractosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rbCarretas = new System.Windows.Forms.RadioButton();
            this.rbTractos = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaTractos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTractosView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(434, 38);
            this.label2.TabIndex = 18;
            this.label2.Text = "LISTA DE UNIDADES DISPONIBLES";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgListaTractos
            // 
            this.dtgListaTractos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaTractos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgListaTractos.Location = new System.Drawing.Point(0, 121);
            this.dtgListaTractos.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dtgListaTractos.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dtgListaTractos.LookAndFeel.SkinName = "Blue";
            this.dtgListaTractos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaTractos.MainView = this.dgvListaTractosView;
            this.dtgListaTractos.Name = "dtgListaTractos";
            this.dtgListaTractos.Size = new System.Drawing.Size(434, 252);
            this.dtgListaTractos.TabIndex = 138;
            this.dtgListaTractos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaTractosView});
            // 
            // dgvListaTractosView
            // 
            this.dgvListaTractosView.GridControl = this.dtgListaTractos;
            this.dgvListaTractosView.Name = "dgvListaTractosView";
            this.dgvListaTractosView.OptionsBehavior.Editable = false;
            this.dgvListaTractosView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaTractosView.OptionsView.RowAutoHeight = true;
            this.dgvListaTractosView.OptionsView.ShowFooter = true;
            this.dgvListaTractosView.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.txtPlaca);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.rbCarretas);
            this.panel1.Controls.Add(this.rbTractos);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 83);
            this.panel1.TabIndex = 139;
            // 
            // txtPlaca
            // 
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPlaca.Location = new System.Drawing.Point(137, 45);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(154, 21);
            this.txtPlaca.TabIndex = 120;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(11, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 15);
            this.label9.TabIndex = 119;
            this.label9.Text = "Buscar por Placa:";
            // 
            // rbCarretas
            // 
            this.rbCarretas.AutoSize = true;
            this.rbCarretas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCarretas.Location = new System.Drawing.Point(241, 14);
            this.rbCarretas.Name = "rbCarretas";
            this.rbCarretas.Size = new System.Drawing.Size(100, 20);
            this.rbCarretas.TabIndex = 118;
            this.rbCarretas.TabStop = true;
            this.rbCarretas.Text = "CARRETAS";
            this.rbCarretas.UseVisualStyleBackColor = true;
            this.rbCarretas.Click += new System.EventHandler(this.rbCarretas_Click);
            // 
            // rbTractos
            // 
            this.rbTractos.AutoSize = true;
            this.rbTractos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTractos.Location = new System.Drawing.Point(131, 14);
            this.rbTractos.Name = "rbTractos";
            this.rbTractos.Size = new System.Drawing.Size(91, 20);
            this.rbTractos.TabIndex = 117;
            this.rbTractos.TabStop = true;
            this.rbTractos.Text = "TRACTOS";
            this.rbTractos.UseVisualStyleBackColor = true;
            this.rbTractos.Click += new System.EventHandler(this.rbTractos_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(11, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 15);
            this.label19.TabIndex = 116;
            this.label19.Text = "TIPO VEHÍCULO:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(372, 17);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(44, 49);
            this.btnBuscar.TabIndex = 121;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // frmUnidadesDisponibles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 373);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtgListaTractos);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.Name = "frmUnidadesDisponibles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNIDADES DISPONIBLES";
            this.Load += new System.EventHandler(this.frmUnidadesDisponibles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaTractos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTractosView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtgListaTractos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListaTractosView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbCarretas;
        private System.Windows.Forms.RadioButton rbTractos;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
    }
}