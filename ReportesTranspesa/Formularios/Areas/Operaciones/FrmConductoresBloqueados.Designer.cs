namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class FrmConductoresBloqueados
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
            this.dgvListaConductores = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotalCBloqueados = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaConductores)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListaConductores
            // 
            this.dgvListaConductores.AllowUserToAddRows = false;
            this.dgvListaConductores.AllowUserToDeleteRows = false;
            this.dgvListaConductores.AllowUserToResizeColumns = false;
            this.dgvListaConductores.AllowUserToResizeRows = false;
            this.dgvListaConductores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListaConductores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListaConductores.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvListaConductores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvListaConductores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvListaConductores.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvListaConductores.Location = new System.Drawing.Point(7, 56);
            this.dgvListaConductores.Name = "dgvListaConductores";
            this.dgvListaConductores.ReadOnly = true;
            this.dgvListaConductores.RowHeadersVisible = false;
            this.dgvListaConductores.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvListaConductores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaConductores.ShowEditingIcon = false;
            this.dgvListaConductores.Size = new System.Drawing.Size(1135, 408);
            this.dgvListaConductores.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(19, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(398, 22);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "LISTA DE CONDUCTORES BLOQUEADOS";
            // 
            // lblTotalCBloqueados
            // 
            this.lblTotalCBloqueados.AutoSize = true;
            this.lblTotalCBloqueados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCBloqueados.Location = new System.Drawing.Point(862, 22);
            this.lblTotalCBloqueados.Name = "lblTotalCBloqueados";
            this.lblTotalCBloqueados.Size = new System.Drawing.Size(0, 13);
            this.lblTotalCBloqueados.TabIndex = 2;
            // 
            // FrmConductoresBloqueados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1147, 470);
            this.Controls.Add(this.lblTotalCBloqueados);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvListaConductores);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConductoresBloqueados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Conductores Bloqueados";
            this.Load += new System.EventHandler(this.FrmConductoresBloqueados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaConductores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListaConductores;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotalCBloqueados;
    }
}