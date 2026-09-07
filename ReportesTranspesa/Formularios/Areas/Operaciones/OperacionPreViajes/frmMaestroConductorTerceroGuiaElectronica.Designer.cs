namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmMaestroConductorTerceroGuiaElectronica
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
            this.gDocumento = new System.Windows.Forms.GroupBox();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.gApellidos = new System.Windows.Forms.GroupBox();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.gTipoDocumento = new System.Windows.Forms.GroupBox();
            this.cbxTipoDocumento = new System.Windows.Forms.ComboBox();
            this.gempresa = new System.Windows.Forms.GroupBox();
            this.txtEmpresaCliente = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.gLicencia = new System.Windows.Forms.GroupBox();
            this.txtLicenciaConducir = new System.Windows.Forms.TextBox();
            this.gNombres = new System.Windows.Forms.GroupBox();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.dgvConductores = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstEmpresaDestinatario = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gDocumento.SuspendLayout();
            this.gApellidos.SuspendLayout();
            this.gTipoDocumento.SuspendLayout();
            this.gempresa.SuspendLayout();
            this.gLicencia.SuspendLayout();
            this.gNombres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConductores)).BeginInit();
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
            this.lblTituloGuia.Size = new System.Drawing.Size(794, 58);
            this.lblTituloGuia.TabIndex = 2;
            this.lblTituloGuia.Text = "GESTIONAR CONDUCTOR TERCERO";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.gDocumento);
            this.groupBox1.Controls.Add(this.gApellidos);
            this.groupBox1.Controls.Add(this.gTipoDocumento);
            this.groupBox1.Controls.Add(this.gempresa);
            this.groupBox1.Controls.Add(this.btnRegistrar);
            this.groupBox1.Controls.Add(this.gLicencia);
            this.groupBox1.Controls.Add(this.gNombres);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(794, 135);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registrar Unidad";
            // 
            // gDocumento
            // 
            this.gDocumento.Controls.Add(this.txtDocumento);
            this.gDocumento.Location = new System.Drawing.Point(206, 80);
            this.gDocumento.Name = "gDocumento";
            this.gDocumento.Size = new System.Drawing.Size(115, 38);
            this.gDocumento.TabIndex = 3;
            this.gDocumento.TabStop = false;
            this.gDocumento.Text = "Documento";
            // 
            // txtDocumento
            // 
            this.txtDocumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDocumento.Location = new System.Drawing.Point(7, 13);
            this.txtDocumento.MaxLength = 15;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(99, 20);
            this.txtDocumento.TabIndex = 1;
            this.txtDocumento.Enter += new System.EventHandler(this.txtDocumento_Enter);
            this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            this.txtDocumento.Leave += new System.EventHandler(this.txtDocumento_Leave);
            // 
            // gApellidos
            // 
            this.gApellidos.Controls.Add(this.txtApellidos);
            this.gApellidos.Location = new System.Drawing.Point(434, 30);
            this.gApellidos.Name = "gApellidos";
            this.gApellidos.Size = new System.Drawing.Size(187, 38);
            this.gApellidos.TabIndex = 2;
            this.gApellidos.TabStop = false;
            this.gApellidos.Text = "Apellidos";
            // 
            // txtApellidos
            // 
            this.txtApellidos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApellidos.Location = new System.Drawing.Point(7, 13);
            this.txtApellidos.MaxLength = 30;
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(170, 20);
            this.txtApellidos.TabIndex = 1;
            this.txtApellidos.Enter += new System.EventHandler(this.txtApellidos_Enter);
            this.txtApellidos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApellidos_KeyPress);
            this.txtApellidos.Leave += new System.EventHandler(this.txtApellidos_Leave);
            // 
            // gTipoDocumento
            // 
            this.gTipoDocumento.Controls.Add(this.cbxTipoDocumento);
            this.gTipoDocumento.Location = new System.Drawing.Point(9, 80);
            this.gTipoDocumento.Name = "gTipoDocumento";
            this.gTipoDocumento.Size = new System.Drawing.Size(191, 38);
            this.gTipoDocumento.TabIndex = 2;
            this.gTipoDocumento.TabStop = false;
            this.gTipoDocumento.Text = "Tipo Documento";
            // 
            // cbxTipoDocumento
            // 
            this.cbxTipoDocumento.FormattingEnabled = true;
            this.cbxTipoDocumento.Items.AddRange(new object[] {
            "TRACTO",
            "PLATAFORMA",
            "TOLVA",
            "FURGON",
            "TERMOKING",
            "CAMIONETA",
            "CAMION",
            "CISTERNA"});
            this.cbxTipoDocumento.Location = new System.Drawing.Point(8, 13);
            this.cbxTipoDocumento.Name = "cbxTipoDocumento";
            this.cbxTipoDocumento.Size = new System.Drawing.Size(178, 21);
            this.cbxTipoDocumento.TabIndex = 0;
            this.cbxTipoDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxTipoDocumento_KeyPress);
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
            // btnRegistrar
            // 
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnRegistrar.Location = new System.Drawing.Point(711, 29);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(71, 66);
            this.btnRegistrar.TabIndex = 2;
            this.btnRegistrar.Text = "REGISTRAR";
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // gLicencia
            // 
            this.gLicencia.Controls.Add(this.txtLicenciaConducir);
            this.gLicencia.Location = new System.Drawing.Point(327, 80);
            this.gLicencia.Name = "gLicencia";
            this.gLicencia.Size = new System.Drawing.Size(122, 38);
            this.gLicencia.TabIndex = 2;
            this.gLicencia.TabStop = false;
            this.gLicencia.Text = "Licencia Conducir";
            // 
            // txtLicenciaConducir
            // 
            this.txtLicenciaConducir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLicenciaConducir.Location = new System.Drawing.Point(7, 14);
            this.txtLicenciaConducir.MaxLength = 15;
            this.txtLicenciaConducir.Name = "txtLicenciaConducir";
            this.txtLicenciaConducir.Size = new System.Drawing.Size(110, 20);
            this.txtLicenciaConducir.TabIndex = 2;
            this.txtLicenciaConducir.Enter += new System.EventHandler(this.txtLicenciaConducir_Enter);
            this.txtLicenciaConducir.Leave += new System.EventHandler(this.txtLicenciaConducir_Leave);
            // 
            // gNombres
            // 
            this.gNombres.Controls.Add(this.txtNombres);
            this.gNombres.Location = new System.Drawing.Point(241, 30);
            this.gNombres.Name = "gNombres";
            this.gNombres.Size = new System.Drawing.Size(187, 38);
            this.gNombres.TabIndex = 1;
            this.gNombres.TabStop = false;
            this.gNombres.Text = "Nombres";
            // 
            // txtNombres
            // 
            this.txtNombres.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombres.Location = new System.Drawing.Point(7, 13);
            this.txtNombres.MaxLength = 30;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(170, 20);
            this.txtNombres.TabIndex = 1;
            this.txtNombres.Enter += new System.EventHandler(this.txtConductor_Enter);
            this.txtNombres.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            this.txtNombres.Leave += new System.EventHandler(this.txtConductor_Leave);
            // 
            // dgvConductores
            // 
            this.dgvConductores.AllowUserToAddRows = false;
            this.dgvConductores.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvConductores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvConductores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvConductores.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvConductores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConductores.Location = new System.Drawing.Point(0, 193);
            this.dgvConductores.Name = "dgvConductores";
            this.dgvConductores.RowHeadersVisible = false;
            this.dgvConductores.Size = new System.Drawing.Size(794, 293);
            this.dgvConductores.TabIndex = 20;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.menos;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // lstEmpresaDestinatario
            // 
            this.lstEmpresaDestinatario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstEmpresaDestinatario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpresaDestinatario.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpresaDestinatario.FullRowSelect = true;
            this.lstEmpresaDestinatario.GridLines = true;
            this.lstEmpresaDestinatario.Location = new System.Drawing.Point(14, 121);
            this.lstEmpresaDestinatario.MultiSelect = false;
            this.lstEmpresaDestinatario.Name = "lstEmpresaDestinatario";
            this.lstEmpresaDestinatario.Size = new System.Drawing.Size(239, 10);
            this.lstEmpresaDestinatario.TabIndex = 94;
            this.lstEmpresaDestinatario.UseCompatibleStateImageBehavior = false;
            this.lstEmpresaDestinatario.View = System.Windows.Forms.View.Details;
            this.lstEmpresaDestinatario.Visible = false;
            this.lstEmpresaDestinatario.Enter += new System.EventHandler(this.lstEmpresaDestinatario_Enter);
            this.lstEmpresaDestinatario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpresaDestinatario_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(472, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "NOTA: Si el transportista es persona natural,  \r\ncolocar Empresa \'TRANSPESA\'";
            // 
            // frmMaestroConductorTerceroGuiaElectronica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(794, 486);
            this.Controls.Add(this.lstEmpresaDestinatario);
            this.Controls.Add(this.dgvConductores);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTituloGuia);
            this.Name = "frmMaestroConductorTerceroGuiaElectronica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMaestroConductorTerceroGuiaElectronica";
            this.Load += new System.EventHandler(this.frmMaestroConductorTerceroGuiaElectronica_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gDocumento.ResumeLayout(false);
            this.gDocumento.PerformLayout();
            this.gApellidos.ResumeLayout(false);
            this.gApellidos.PerformLayout();
            this.gTipoDocumento.ResumeLayout(false);
            this.gempresa.ResumeLayout(false);
            this.gempresa.PerformLayout();
            this.gLicencia.ResumeLayout(false);
            this.gLicencia.PerformLayout();
            this.gNombres.ResumeLayout(false);
            this.gNombres.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConductores)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gTipoDocumento;
        private System.Windows.Forms.ComboBox cbxTipoDocumento;
        private System.Windows.Forms.GroupBox gempresa;
        private System.Windows.Forms.TextBox txtEmpresaCliente;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.GroupBox gLicencia;
        private System.Windows.Forms.TextBox txtLicenciaConducir;
        private System.Windows.Forms.GroupBox gNombres;
        private System.Windows.Forms.TextBox txtNombres;
        public System.Windows.Forms.DataGridView dgvConductores;
        private System.Windows.Forms.ListView lstEmpresaDestinatario;
        private System.Windows.Forms.GroupBox gApellidos;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.GroupBox gDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Label label1;

    }
}