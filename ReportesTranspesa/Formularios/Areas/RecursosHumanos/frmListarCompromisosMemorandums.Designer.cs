namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class frmListarCompromisosMemorandums
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListarCompromisosMemorandums));
            this.dgvListaMemos = new DevExpress.XtraGrid.GridControl();
            this.gvListaMemos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConductor = new MetroFramework.Controls.MetroTextBox();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.lvConductor = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaMemos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaMemos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvListaMemos
            // 
            this.dgvListaMemos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListaMemos.Location = new System.Drawing.Point(0, 81);
            this.dgvListaMemos.MainView = this.gvListaMemos;
            this.dgvListaMemos.Name = "dgvListaMemos";
            this.dgvListaMemos.Size = new System.Drawing.Size(985, 405);
            this.dgvListaMemos.TabIndex = 0;
            this.dgvListaMemos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaMemos});
            // 
            // gvListaMemos
            // 
            this.gvListaMemos.GridControl = this.dgvListaMemos;
            this.gvListaMemos.Name = "gvListaMemos";
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
            this.btnExcel.Location = new System.Drawing.Point(912, 6);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(37, 29);
            this.btnExcel.TabIndex = 6;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpFechaIni);
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Location = new System.Drawing.Point(5, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 59);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fecha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 96;
            this.label2.Text = "Fin:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "Inicio:";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(43, 22);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(87, 22);
            this.dtpFechaIni.TabIndex = 1;
            this.dtpFechaIni.Tag = "1";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(194, 22);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(87, 22);
            this.dtpFechaFin.TabIndex = 2;
            this.dtpFechaFin.Tag = "2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConductor);
            this.groupBox1.Location = new System.Drawing.Point(303, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 59);
            this.groupBox1.TabIndex = 98;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nombre de Persona";
            // 
            // txtConductor
            // 
            this.txtConductor.BackColor = System.Drawing.Color.White;
            this.txtConductor.Lines = new string[0];
            this.txtConductor.Location = new System.Drawing.Point(15, 19);
            this.txtConductor.MaxLength = 32767;
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.PasswordChar = '\0';
            this.txtConductor.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtConductor.SelectedText = "";
            this.txtConductor.Size = new System.Drawing.Size(399, 29);
            this.txtConductor.Style = MetroFramework.MetroColorStyle.Red;
            this.txtConductor.TabIndex = 3;
            this.txtConductor.Tag = "3";
            this.txtConductor.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtConductor.UseSelectable = true;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(751, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(47, 47);
            this.btnBuscar.TabIndex = 96;
            this.btnBuscar.Tag = "5";
            this.btnBuscar.ToolTip = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lvConductor
            // 
            this.lvConductor.BackColor = System.Drawing.Color.LightBlue;
            this.lvConductor.ForeColor = System.Drawing.Color.Black;
            this.lvConductor.FullRowSelect = true;
            this.lvConductor.GridLines = true;
            this.lvConductor.Location = new System.Drawing.Point(318, 67);
            this.lvConductor.MultiSelect = false;
            this.lvConductor.Name = "lvConductor";
            this.lvConductor.Size = new System.Drawing.Size(399, 10);
            this.lvConductor.TabIndex = 95;
            this.lvConductor.UseCompatibleStateImageBehavior = false;
            this.lvConductor.View = System.Windows.Forms.View.Details;
            this.lvConductor.Visible = false;
            this.lvConductor.Enter += new System.EventHandler(this.lvConductor_Enter);
            this.lvConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvConductor_KeyPress);
            // 
            // frmListarCompromisosMemorandums
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(986, 488);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lvConductor);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.dgvListaMemos);
            this.Name = "frmListarCompromisosMemorandums";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMPROMISOS Y MEMORANDUMS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListarCompromisosMemorandums_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaMemos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaMemos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgvListaMemos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListaMemos;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox txtConductor;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ListView lvConductor;
    }
}