namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class frmAsistenciaModificar
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblnombre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTipoAsis = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTipoAsis)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtCodigo);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.lblnombre);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvTipoAsis);
            this.splitContainer1.Size = new System.Drawing.Size(323, 277);
            this.splitContainer1.SplitterDistance = 59;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(249, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(29, 20);
            this.txtCodigo.TabIndex = 3;
            this.txtCodigo.Visible = false;
            // 
            // button1
            // 
            this.button1.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.button1.Location = new System.Drawing.Point(281, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 33);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblnombre
            // 
            this.lblnombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnombre.ForeColor = System.Drawing.Color.Blue;
            this.lblnombre.Location = new System.Drawing.Point(12, 34);
            this.lblnombre.Name = "lblnombre";
            this.lblnombre.Size = new System.Drawing.Size(308, 18);
            this.lblnombre.TabIndex = 1;
            this.lblnombre.Text = "ROJAS RODRIGUEZ JOEL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trabajador:";
            // 
            // dgvTipoAsis
            // 
            this.dgvTipoAsis.AllowUserToAddRows = false;
            this.dgvTipoAsis.BackgroundColor = System.Drawing.Color.White;
            this.dgvTipoAsis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTipoAsis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTipoAsis.Location = new System.Drawing.Point(0, 0);
            this.dgvTipoAsis.Name = "dgvTipoAsis";
            this.dgvTipoAsis.ReadOnly = true;
            this.dgvTipoAsis.RowHeadersVisible = false;
            this.dgvTipoAsis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTipoAsis.ShowRowErrors = false;
            this.dgvTipoAsis.Size = new System.Drawing.Size(323, 214);
            this.dgvTipoAsis.TabIndex = 0;
            this.dgvTipoAsis.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTipoAsis_CellClick);
            this.dgvTipoAsis.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTipoAsis_CellContentClick);
            this.dgvTipoAsis.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTipoAsis_CellEnter);
            // 
            // frmAsistenciaModificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(323, 277);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAsistenciaModificar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAsistenciaModificar";
            this.Load += new System.EventHandler(this.frmAsistenciaModificar_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTipoAsis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblnombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTipoAsis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCodigo;
    }
}