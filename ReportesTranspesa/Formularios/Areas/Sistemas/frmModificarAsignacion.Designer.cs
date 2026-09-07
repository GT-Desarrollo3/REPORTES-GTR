namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    partial class frmModificarAsignacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModificarAsignacion));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvEquipos = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvLineas = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblIDEmpleado = new System.Windows.Forms.Label();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvRadios = new System.Windows.Forms.DataGridView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.btnAceptarRadio = new System.Windows.Forms.ToolStripButton();
            this.btnCancelarRadio = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnAceptarEquipo = new System.Windows.Forms.ToolStripButton();
            this.btnCancelarEquipo = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btnAceptarLinea = new System.Windows.Forms.ToolStripButton();
            this.btnCancelarLinea = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipos)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineas)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRadios)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerControl1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(23, 63);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(992, 355);
            this.splitContainerControl1.SplitterPosition = 173;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvEquipos);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(988, 173);
            this.splitContainer1.SplitterDistance = 809;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvEquipos
            // 
            this.dgvEquipos.AllowUserToAddRows = false;
            this.dgvEquipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEquipos.Location = new System.Drawing.Point(0, 25);
            this.dgvEquipos.Name = "dgvEquipos";
            this.dgvEquipos.ReadOnly = true;
            this.dgvEquipos.RowHeadersVisible = false;
            this.dgvEquipos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipos.Size = new System.Drawing.Size(809, 148);
            this.dgvEquipos.TabIndex = 0;
            this.dgvEquipos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEquipos_CellContentClick);
            this.dgvEquipos.Click += new System.EventHandler(this.dgvEquipos_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.btnAceptarEquipo,
            this.btnCancelarEquipo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(809, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(31, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Desvincular Equipo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Vincular Equipo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvLineas);
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button4);
            this.splitContainer2.Panel2.Controls.Add(this.button3);
            this.splitContainer2.Size = new System.Drawing.Size(988, 173);
            this.splitContainer2.SplitterDistance = 809;
            this.splitContainer2.TabIndex = 1;
            // 
            // dgvLineas
            // 
            this.dgvLineas.AllowUserToAddRows = false;
            this.dgvLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLineas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLineas.Location = new System.Drawing.Point(0, 25);
            this.dgvLineas.Name = "dgvLineas";
            this.dgvLineas.ReadOnly = true;
            this.dgvLineas.RowHeadersVisible = false;
            this.dgvLineas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineas.Size = new System.Drawing.Size(809, 148);
            this.dgvLineas.TabIndex = 1;
            this.dgvLineas.Click += new System.EventHandler(this.dgvLineas_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.btnAceptarLinea,
            this.btnCancelarLinea});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(809, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(31, 65);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "Desvincular Linea";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(31, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Vincular Linea";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblIDEmpleado
            // 
            this.lblIDEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIDEmpleado.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lblIDEmpleado.Location = new System.Drawing.Point(23, 28);
            this.lblIDEmpleado.Name = "lblIDEmpleado";
            this.lblIDEmpleado.Size = new System.Drawing.Size(73, 23);
            this.lblIDEmpleado.TabIndex = 1;
            this.lblIDEmpleado.Text = "label1";
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEmpleado.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lblEmpleado.Image = global::ReportesTranspesa.Properties.Resources.persona_logo_icon_169946;
            this.lblEmpleado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmpleado.Location = new System.Drawing.Point(102, 28);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(913, 23);
            this.lblEmpleado.TabIndex = 2;
            this.lblEmpleado.Text = "label1";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer3.Location = new System.Drawing.Point(25, 423);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvRadios);
            this.splitContainer3.Panel1.Controls.Add(this.toolStrip3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer3.Panel2.Controls.Add(this.button5);
            this.splitContainer3.Panel2.Controls.Add(this.button6);
            this.splitContainer3.Size = new System.Drawing.Size(988, 172);
            this.splitContainer3.SplitterDistance = 809;
            this.splitContainer3.TabIndex = 3;
            // 
            // dgvRadios
            // 
            this.dgvRadios.AllowUserToAddRows = false;
            this.dgvRadios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRadios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRadios.Location = new System.Drawing.Point(0, 25);
            this.dgvRadios.Name = "dgvRadios";
            this.dgvRadios.ReadOnly = true;
            this.dgvRadios.RowHeadersVisible = false;
            this.dgvRadios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRadios.Size = new System.Drawing.Size(809, 147);
            this.dgvRadios.TabIndex = 3;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.btnAceptarRadio,
            this.btnCancelarRadio});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(809, 25);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(29, 64);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(117, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Desvincular Radio";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(29, 25);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(117, 23);
            this.button6.TabIndex = 3;
            this.button6.Text = "Vincular Radio";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel3.ForeColor = System.Drawing.Color.DarkGreen;
            this.toolStripLabel3.Image = global::ReportesTranspesa.Properties.Resources.radio_porta;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(118, 22);
            this.toolStripLabel3.Text = "RADIO PORTATIL";
            // 
            // btnAceptarRadio
            // 
            this.btnAceptarRadio.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAceptarRadio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAceptarRadio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarRadio.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptarRadio.Image")));
            this.btnAceptarRadio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptarRadio.Name = "btnAceptarRadio";
            this.btnAceptarRadio.Size = new System.Drawing.Size(132, 22);
            this.btnAceptarRadio.Text = "Vincular Seleccionado";
            this.btnAceptarRadio.Visible = false;
            this.btnAceptarRadio.Click += new System.EventHandler(this.btnAceptarRadio_Click);
            // 
            // btnCancelarRadio
            // 
            this.btnCancelarRadio.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.btnCancelarRadio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCancelarRadio.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarRadio.Image")));
            this.btnCancelarRadio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelarRadio.Name = "btnCancelarRadio";
            this.btnCancelarRadio.Size = new System.Drawing.Size(57, 22);
            this.btnCancelarRadio.Text = "Cancelar";
            this.btnCancelarRadio.Visible = false;
            this.btnCancelarRadio.Click += new System.EventHandler(this.btnCancelarRadio_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripLabel1.Image = global::ReportesTranspesa.Properties.Resources.mobile_phone_14405;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(120, 22);
            this.toolStripLabel1.Text = "EQUIPO CELULAR";
            // 
            // btnAceptarEquipo
            // 
            this.btnAceptarEquipo.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAceptarEquipo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAceptarEquipo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAceptarEquipo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarEquipo.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptarEquipo.Image")));
            this.btnAceptarEquipo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptarEquipo.Name = "btnAceptarEquipo";
            this.btnAceptarEquipo.Size = new System.Drawing.Size(132, 22);
            this.btnAceptarEquipo.Text = "Vincular Seleccionado";
            this.btnAceptarEquipo.Visible = false;
            this.btnAceptarEquipo.Click += new System.EventHandler(this.btnAceptarEquipo_Click);
            // 
            // btnCancelarEquipo
            // 
            this.btnCancelarEquipo.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.btnCancelarEquipo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCancelarEquipo.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarEquipo.Image")));
            this.btnCancelarEquipo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelarEquipo.Name = "btnCancelarEquipo";
            this.btnCancelarEquipo.Size = new System.Drawing.Size(57, 22);
            this.btnCancelarEquipo.Text = "Cancelar";
            this.btnCancelarEquipo.Visible = false;
            this.btnCancelarEquipo.Click += new System.EventHandler(this.btnCancelarEquipo_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.ForeColor = System.Drawing.Color.Firebrick;
            this.toolStripLabel2.Image = global::ReportesTranspesa.Properties.Resources.wifi1_40495;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(109, 22);
            this.toolStripLabel2.Text = "LINEA CELULAR";
            // 
            // btnAceptarLinea
            // 
            this.btnAceptarLinea.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAceptarLinea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAceptarLinea.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarLinea.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptarLinea.Image")));
            this.btnAceptarLinea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptarLinea.Name = "btnAceptarLinea";
            this.btnAceptarLinea.Size = new System.Drawing.Size(132, 22);
            this.btnAceptarLinea.Text = "Vincular Seleccionado";
            this.btnAceptarLinea.Visible = false;
            this.btnAceptarLinea.Click += new System.EventHandler(this.btnAceptarLinea_Click);
            // 
            // btnCancelarLinea
            // 
            this.btnCancelarLinea.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.btnCancelarLinea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCancelarLinea.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarLinea.Image")));
            this.btnCancelarLinea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelarLinea.Name = "btnCancelarLinea";
            this.btnCancelarLinea.Size = new System.Drawing.Size(57, 22);
            this.btnCancelarLinea.Text = "Cancelar";
            this.btnCancelarLinea.Visible = false;
            this.btnCancelarLinea.Click += new System.EventHandler(this.btnCancelarLinea_Click);
            // 
            // frmModificarAsignacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1038, 609);
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.lblIDEmpleado);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmModificarAsignacion";
            this.Load += new System.EventHandler(this.frmModificarAsignacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipos)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineas)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRadios)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Label lblIDEmpleado;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvEquipos;
        private System.Windows.Forms.DataGridView dgvLineas;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripButton btnAceptarEquipo;
        private System.Windows.Forms.ToolStripButton btnCancelarEquipo;
        private System.Windows.Forms.ToolStripButton btnAceptarLinea;
        private System.Windows.Forms.ToolStripButton btnCancelarLinea;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dgvRadios;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton btnAceptarRadio;
        private System.Windows.Forms.ToolStripButton btnCancelarRadio;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}