namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmMaestroNumerosMTCxEmpresa
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
            this.components = new System.ComponentModel.Container();
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gempresa = new System.Windows.Forms.GroupBox();
            this.txtEmpresaCliente = new System.Windows.Forms.TextBox();
            this.gMTC = new System.Windows.Forms.GroupBox();
            this.txtMTC = new System.Windows.Forms.TextBox();
            this.dgvMTC = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lstEmpresaDestinatario = new System.Windows.Forms.ListView();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gempresa.SuspendLayout();
            this.gMTC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTC)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            this.lblTituloGuia.Size = new System.Drawing.Size(649, 58);
            this.lblTituloGuia.TabIndex = 1;
            this.lblTituloGuia.Text = "VINCULAR EMPRESA - CODIGO MTC";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.gempresa);
            this.groupBox1.Controls.Add(this.btnRegistrar);
            this.groupBox1.Controls.Add(this.gMTC);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 85);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registrar Unidad";
            // 
            // gempresa
            // 
            this.gempresa.Controls.Add(this.txtEmpresaCliente);
            this.gempresa.Location = new System.Drawing.Point(7, 29);
            this.gempresa.Name = "gempresa";
            this.gempresa.Size = new System.Drawing.Size(228, 39);
            this.gempresa.TabIndex = 1;
            this.gempresa.TabStop = false;
            this.gempresa.Text = "Empresa";
            // 
            // txtEmpresaCliente
            // 
            this.txtEmpresaCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpresaCliente.Location = new System.Drawing.Point(7, 14);
            this.txtEmpresaCliente.Name = "txtEmpresaCliente";
            this.txtEmpresaCliente.Size = new System.Drawing.Size(215, 20);
            this.txtEmpresaCliente.TabIndex = 0;
            this.txtEmpresaCliente.Enter += new System.EventHandler(this.txtEmpresaCliente_Enter);
            this.txtEmpresaCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpresaCliente_KeyPress);
            this.txtEmpresaCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmpresaCliente_KeyUp);
            this.txtEmpresaCliente.Leave += new System.EventHandler(this.txtEmpresaCliente_Leave);
            // 
            // gMTC
            // 
            this.gMTC.Controls.Add(this.txtMTC);
            this.gMTC.Location = new System.Drawing.Point(241, 30);
            this.gMTC.Name = "gMTC";
            this.gMTC.Size = new System.Drawing.Size(120, 38);
            this.gMTC.TabIndex = 1;
            this.gMTC.TabStop = false;
            this.gMTC.Text = "CODIGO  MTC";
            // 
            // txtMTC
            // 
            this.txtMTC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMTC.Location = new System.Drawing.Point(7, 13);
            this.txtMTC.MaxLength = 13;
            this.txtMTC.Name = "txtMTC";
            this.txtMTC.Size = new System.Drawing.Size(100, 20);
            this.txtMTC.TabIndex = 1;
            // 
            // dgvMTC
            // 
            this.dgvMTC.AllowUserToAddRows = false;
            this.dgvMTC.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMTC.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMTC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMTC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMTC.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvMTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMTC.Location = new System.Drawing.Point(0, 143);
            this.dgvMTC.Name = "dgvMTC";
            this.dgvMTC.RowHeadersVisible = false;
            this.dgvMTC.Size = new System.Drawing.Size(649, 334);
            this.dgvMTC.TabIndex = 20;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // lstEmpresaDestinatario
            // 
            this.lstEmpresaDestinatario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstEmpresaDestinatario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpresaDestinatario.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpresaDestinatario.FullRowSelect = true;
            this.lstEmpresaDestinatario.GridLines = true;
            this.lstEmpresaDestinatario.Location = new System.Drawing.Point(13, 121);
            this.lstEmpresaDestinatario.MultiSelect = false;
            this.lstEmpresaDestinatario.Name = "lstEmpresaDestinatario";
            this.lstEmpresaDestinatario.Size = new System.Drawing.Size(239, 10);
            this.lstEmpresaDestinatario.TabIndex = 94;
            this.lstEmpresaDestinatario.UseCompatibleStateImageBehavior = false;
            this.lstEmpresaDestinatario.View = System.Windows.Forms.View.Details;
            this.lstEmpresaDestinatario.Visible = false;
            this.lstEmpresaDestinatario.Enter += new System.EventHandler(this.lstEmpresaDestinatario_Enter);
            this.lstEmpresaDestinatario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpresaDestinatario_KeyPress);
            this.lstEmpresaDestinatario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstEmpresaDestinatario_KeyUp);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.Location = new System.Drawing.Point(396, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(83, 60);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "REGISTRAR";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnRegistrar.Location = new System.Drawing.Point(705, 13);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(83, 60);
            this.btnRegistrar.TabIndex = 2;
            this.btnRegistrar.Text = "REGISTRAR";
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // frmMaestroNumerosMTCxEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(649, 477);
            this.Controls.Add(this.lstEmpresaDestinatario);
            this.Controls.Add(this.dgvMTC);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTituloGuia);
            this.Name = "frmMaestroNumerosMTCxEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMaestroNumerosMTCxEmpresa";
            this.Load += new System.EventHandler(this.frmMaestroNumerosMTCxEmpresa_Load);
            this.groupBox1.ResumeLayout(false);
            this.gempresa.ResumeLayout(false);
            this.gempresa.PerformLayout();
            this.gMTC.ResumeLayout(false);
            this.gMTC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMTC)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gempresa;
        private System.Windows.Forms.TextBox txtEmpresaCliente;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.GroupBox gMTC;
        private System.Windows.Forms.TextBox txtMTC;
        public System.Windows.Forms.DataGridView dgvMTC;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ListView lstEmpresaDestinatario;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
    }
}