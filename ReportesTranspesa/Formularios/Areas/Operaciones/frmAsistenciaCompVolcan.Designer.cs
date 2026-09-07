namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmAsistenciaCompVolcan
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNroComp = new System.Windows.Forms.TextBox();
            this.dtgListaComp = new DevExpress.XtraGrid.GridControl();
            this.dgvListaCompView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnLiberar = new System.Windows.Forms.Button();
            this.dtpCompAdelantada = new System.Windows.Forms.DateTimePicker();
            this.pCompAdelantada = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCompView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pCompAdelantada.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.lblFecha);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblNombre);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtNroComp);
            this.panel1.Controls.Add(this.pCompAdelantada);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 93);
            this.panel1.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.Blue;
            this.lblTitulo.Location = new System.Drawing.Point(10, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(66, 18);
            this.lblTitulo.TabIndex = 136;
            this.lblTitulo.Text = "TITULO";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.Location = new System.Drawing.Point(343, 56);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(43, 33);
            this.btnGuardar.TabIndex = 135;
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.Blue;
            this.lblFecha.Location = new System.Drawing.Point(59, 63);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(82, 16);
            this.lblFecha.TabIndex = 134;
            this.lblFecha.Text = "15/08/2025";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(10, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 133;
            this.label6.Text = "Conductor:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.Blue;
            this.lblNombre.Location = new System.Drawing.Point(85, 38);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(206, 16);
            this.lblNombre.TabIndex = 126;
            this.lblNombre.Text = "NOMBRE DEL CONDUCTOR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 125;
            this.label2.Text = "Fecha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(163, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 130;
            this.label3.Text = "Comp. Pend.:";
            // 
            // txtNroComp
            // 
            this.txtNroComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroComp.ForeColor = System.Drawing.Color.Black;
            this.txtNroComp.Location = new System.Drawing.Point(260, 60);
            this.txtNroComp.Name = "txtNroComp";
            this.txtNroComp.ReadOnly = true;
            this.txtNroComp.Size = new System.Drawing.Size(65, 22);
            this.txtNroComp.TabIndex = 129;
            // 
            // dtgListaComp
            // 
            this.dtgListaComp.AllowDrop = true;
            this.dtgListaComp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgListaComp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgListaComp.Location = new System.Drawing.Point(0, 136);
            this.dtgListaComp.LookAndFeel.SkinName = "The Asphalt World";
            this.dtgListaComp.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgListaComp.MainView = this.dgvListaCompView;
            this.dtgListaComp.Name = "dtgListaComp";
            this.dtgListaComp.Size = new System.Drawing.Size(412, 188);
            this.dtgListaComp.TabIndex = 23;
            this.dtgListaComp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListaCompView});
            // 
            // dgvListaCompView
            // 
            this.dgvListaCompView.Appearance.SelectedRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvListaCompView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.dgvListaCompView.GridControl = this.dtgListaComp;
            this.dgvListaCompView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgvListaCompView.Name = "dgvListaCompView";
            this.dgvListaCompView.OptionsBehavior.Editable = false;
            this.dgvListaCompView.OptionsSelection.MultiSelect = true;
            this.dgvListaCompView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvListaCompView.OptionsView.ColumnAutoWidth = false;
            this.dgvListaCompView.OptionsView.RowAutoHeight = true;
            this.dgvListaCompView.OptionsView.ShowGroupPanel = false;
            this.dgvListaCompView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgvListaCompView_CustomDrawCell);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dtpHasta);
            this.panel2.Controls.Add(this.dtpDesde);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(412, 43);
            this.panel2.TabIndex = 22;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_search_find_9565;
            this.btnBuscar.Location = new System.Drawing.Point(284, 6);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(31, 21);
            this.btnBuscar.TabIndex = 23;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(163, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "--";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Buscar:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHasta.Location = new System.Drawing.Point(180, 6);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(95, 20);
            this.dtpHasta.TabIndex = 20;
            this.dtpHasta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpHasta_KeyPress);
            // 
            // dtpDesde
            // 
            this.dtpDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.Location = new System.Drawing.Point(66, 6);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(95, 20);
            this.dtpDesde.TabIndex = 19;
            this.dtpDesde.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpDesde_KeyPress);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.btnImprimir);
            this.panel3.Controls.Add(this.btnLiberar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 324);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(412, 31);
            this.panel3.TabIndex = 24;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Yi Baiti", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(336, 5);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(74, 21);
            this.btnImprimir.TabIndex = 18;
            this.btnImprimir.Text = "IMPRIMIR";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnLiberar
            // 
            this.btnLiberar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLiberar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLiberar.ForeColor = System.Drawing.Color.Red;
            this.btnLiberar.Location = new System.Drawing.Point(256, 5);
            this.btnLiberar.Name = "btnLiberar";
            this.btnLiberar.Size = new System.Drawing.Size(75, 21);
            this.btnLiberar.TabIndex = 17;
            this.btnLiberar.Text = "Liberar";
            this.btnLiberar.UseVisualStyleBackColor = false;
            this.btnLiberar.Click += new System.EventHandler(this.btnLiberar_Click);
            // 
            // dtpCompAdelantada
            // 
            this.dtpCompAdelantada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCompAdelantada.CustomFormat = "dd/MM/yyyy";
            this.dtpCompAdelantada.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCompAdelantada.Location = new System.Drawing.Point(90, 6);
            this.dtpCompAdelantada.Name = "dtpCompAdelantada";
            this.dtpCompAdelantada.Size = new System.Drawing.Size(95, 20);
            this.dtpCompAdelantada.TabIndex = 137;
            // 
            // pCompAdelantada
            // 
            this.pCompAdelantada.Controls.Add(this.label4);
            this.pCompAdelantada.Controls.Add(this.dtpCompAdelantada);
            this.pCompAdelantada.Location = new System.Drawing.Point(140, 56);
            this.pCompAdelantada.Name = "pCompAdelantada";
            this.pCompAdelantada.Size = new System.Drawing.Size(197, 32);
            this.pCompAdelantada.TabIndex = 138;
            this.pCompAdelantada.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 26);
            this.label4.TabIndex = 139;
            this.label4.Text = "Compensar\r\npor el Día:";
            // 
            // frmAsistenciaCompVolcan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 355);
            this.Controls.Add(this.dtgListaComp);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAsistenciaCompVolcan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMPENSACIONES VOLCAN";
            this.Load += new System.EventHandler(this.frmAsistenciaCompVolcan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaCompView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pCompAdelantada.ResumeLayout(false);
            this.pCompAdelantada.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraGrid.GridControl dtgListaComp;
        public DevExpress.XtraGrid.Views.Grid.GridView dgvListaCompView;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lblFecha;
        public System.Windows.Forms.TextBox txtNroComp;
        public System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Button btnImprimir;
        public System.Windows.Forms.Button btnLiberar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnGuardar;
        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DateTimePicker dtpCompAdelantada;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Panel pCompAdelantada;
    }
}