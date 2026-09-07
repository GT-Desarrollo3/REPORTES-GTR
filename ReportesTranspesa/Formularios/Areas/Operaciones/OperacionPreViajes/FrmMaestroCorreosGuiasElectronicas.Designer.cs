namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class FrmMaestroCorreosGuiasElectronicas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaestroCorreosGuiasElectronicas));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupCliente = new System.Windows.Forms.GroupBox();
            this.dgvCorreos = new System.Windows.Forms.DataGridView();
            this.Persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCorreo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Correo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Principal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lstEmpresaRemitente = new System.Windows.Forms.ListView();
            this.gBuscarEmpresa = new System.Windows.Forms.GroupBox();
            this.txtEmpresaDestinatario = new System.Windows.Forms.TextBox();
            this.txtLabelMenu = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDireccionDestino = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.PictureBox();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            this.groupCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorreos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.gBuscarEmpresa.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.btnActualizar);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupCliente);
            this.panel1.Controls.Add(this.lstEmpresaRemitente);
            this.panel1.Controls.Add(this.gBuscarEmpresa);
            this.panel1.Controls.Add(this.txtLabelMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 371);
            this.panel1.TabIndex = 0;
            // 
            // groupCliente
            // 
            this.groupCliente.Controls.Add(this.btnAgregar);
            this.groupCliente.Controls.Add(this.dgvCorreos);
            this.groupCliente.Location = new System.Drawing.Point(13, 103);
            this.groupCliente.Name = "groupCliente";
            this.groupCliente.Size = new System.Drawing.Size(479, 257);
            this.groupCliente.TabIndex = 92;
            this.groupCliente.TabStop = false;
            this.groupCliente.Text = "Correos Clientes";
            // 
            // dgvCorreos
            // 
            this.dgvCorreos.AllowUserToAddRows = false;
            this.dgvCorreos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCorreos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCorreos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCorreos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCorreos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Persona,
            this.idCorreo,
            this.Correo,
            this.Principal});
            this.dgvCorreos.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvCorreos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCorreos.Location = new System.Drawing.Point(3, 16);
            this.dgvCorreos.Name = "dgvCorreos";
            this.dgvCorreos.RowHeadersVisible = false;
            this.dgvCorreos.Size = new System.Drawing.Size(473, 238);
            this.dgvCorreos.TabIndex = 19;
            this.dgvCorreos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCorreos_CellContentClick);
            this.dgvCorreos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCorreos_CellEndEdit);
            this.dgvCorreos.Enter += new System.EventHandler(this.dgvCorreos_Enter);
            this.dgvCorreos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvCorreos_PreviewKeyDown);
            // 
            // Persona
            // 
            this.Persona.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Persona.HeaderText = "Persona";
            this.Persona.Name = "Persona";
            this.Persona.Visible = false;
            // 
            // idCorreo
            // 
            this.idCorreo.HeaderText = "idCorreo";
            this.idCorreo.Name = "idCorreo";
            this.idCorreo.Visible = false;
            // 
            // Correo
            // 
            this.Correo.HeaderText = "Correo";
            this.Correo.Name = "Correo";
            // 
            // Principal
            // 
            this.Principal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Principal.FalseValue = "0";
            this.Principal.HeaderText = "Principal";
            this.Principal.Name = "Principal";
            this.Principal.Width = 53;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // lstEmpresaRemitente
            // 
            this.lstEmpresaRemitente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstEmpresaRemitente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpresaRemitente.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpresaRemitente.FullRowSelect = true;
            this.lstEmpresaRemitente.GridLines = true;
            this.lstEmpresaRemitente.Location = new System.Drawing.Point(13, 84);
            this.lstEmpresaRemitente.MultiSelect = false;
            this.lstEmpresaRemitente.Name = "lstEmpresaRemitente";
            this.lstEmpresaRemitente.Size = new System.Drawing.Size(287, 10);
            this.lstEmpresaRemitente.TabIndex = 91;
            this.lstEmpresaRemitente.UseCompatibleStateImageBehavior = false;
            this.lstEmpresaRemitente.View = System.Windows.Forms.View.Details;
            this.lstEmpresaRemitente.Visible = false;
            this.lstEmpresaRemitente.Enter += new System.EventHandler(this.lstEmpresaRemitente_Enter);
            this.lstEmpresaRemitente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpresaRemitente_KeyPress);
            this.lstEmpresaRemitente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstEmpresaRemitente_KeyUp);
            // 
            // gBuscarEmpresa
            // 
            this.gBuscarEmpresa.Controls.Add(this.txtEmpresaDestinatario);
            this.gBuscarEmpresa.Location = new System.Drawing.Point(4, 48);
            this.gBuscarEmpresa.Name = "gBuscarEmpresa";
            this.gBuscarEmpresa.Size = new System.Drawing.Size(279, 42);
            this.gBuscarEmpresa.TabIndex = 6;
            this.gBuscarEmpresa.TabStop = false;
            this.gBuscarEmpresa.Text = "Empresa o Ruc";
            // 
            // txtEmpresaDestinatario
            // 
            this.txtEmpresaDestinatario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpresaDestinatario.Location = new System.Drawing.Point(9, 16);
            this.txtEmpresaDestinatario.Name = "txtEmpresaDestinatario";
            this.txtEmpresaDestinatario.Size = new System.Drawing.Size(262, 20);
            this.txtEmpresaDestinatario.TabIndex = 0;
            this.txtEmpresaDestinatario.Enter += new System.EventHandler(this.txtEmpresaRemitente_Enter);
            this.txtEmpresaDestinatario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpresaRemitente_KeyPress);
            this.txtEmpresaDestinatario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmpresaRemitente_KeyUp);
            this.txtEmpresaDestinatario.Leave += new System.EventHandler(this.txtEmpresaRemitente_Leave);
            // 
            // txtLabelMenu
            // 
            this.txtLabelMenu.BackColor = System.Drawing.Color.DodgerBlue;
            this.txtLabelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtLabelMenu.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelMenu.Location = new System.Drawing.Point(0, 0);
            this.txtLabelMenu.Name = "txtLabelMenu";
            this.txtLabelMenu.Size = new System.Drawing.Size(591, 43);
            this.txtLabelMenu.TabIndex = 4;
            this.txtLabelMenu.Text = "CORREOS POR EMPRESA CLIENTE";
            this.txtLabelMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDireccionDestino);
            this.groupBox1.Location = new System.Drawing.Point(306, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 50);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direccion CDA";
            // 
            // txtDireccionDestino
            // 
            this.txtDireccionDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccionDestino.Location = new System.Drawing.Point(9, 16);
            this.txtDireccionDestino.Name = "txtDireccionDestino";
            this.txtDireccionDestino.ReadOnly = true;
            this.txtDireccionDestino.Size = new System.Drawing.Size(262, 20);
            this.txtDireccionDestino.TabIndex = 0;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAgregar.Location = new System.Drawing.Point(416, 202);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(41, 37);
            this.btnAgregar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnAgregar.TabIndex = 20;
            this.btnAgregar.TabStop = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.menos;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Location = new System.Drawing.Point(492, 146);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(97, 49);
            this.btnGuardar.TabIndex = 93;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.Location = new System.Drawing.Point(491, 217);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(97, 49);
            this.btnActualizar.TabIndex = 94;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // FrmMaestroCorreosGuiasElectronicas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 371);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMaestroCorreosGuiasElectronicas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maestro De Correos para Guias Electronicas";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMaestroCorreosGuiasElectronicas_FormClosed);
            this.Load += new System.EventHandler(this.FrmMaestroCorreosGuiasElectronicas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMaestroCorreosGuiasElectronicas_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FrmMaestroCorreosGuiasElectronicas_PreviewKeyDown);
            this.panel1.ResumeLayout(false);
            this.groupCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorreos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.gBuscarEmpresa.ResumeLayout(false);
            this.gBuscarEmpresa.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gBuscarEmpresa;
        private System.Windows.Forms.TextBox txtEmpresaDestinatario;
        private System.Windows.Forms.ListView lstEmpresaRemitente;
        private System.Windows.Forms.GroupBox groupCliente;
        private System.Windows.Forms.PictureBox btnAgregar;
        public System.Windows.Forms.DataGridView dgvCorreos;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        public System.Windows.Forms.Label txtLabelMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCorreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Correo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Principal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDireccionDestino;
        private DevExpress.XtraEditors.SimpleButton btnActualizar;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
    }
}