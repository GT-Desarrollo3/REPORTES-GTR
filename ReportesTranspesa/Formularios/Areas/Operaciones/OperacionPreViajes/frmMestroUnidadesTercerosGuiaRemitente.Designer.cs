namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmMestroUnidadesTercerosGuiaRemitente
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
            this.gTipoVehiculo = new System.Windows.Forms.GroupBox();
            this.cbxTipoVehiculo = new System.Windows.Forms.ComboBox();
            this.gempresa = new System.Windows.Forms.GroupBox();
            this.txtEmpresaCliente = new System.Windows.Forms.TextBox();
            this.gtarjeta = new System.Windows.Forms.GroupBox();
            this.txtTarjetaCirculacion = new System.Windows.Forms.TextBox();
            this.gPlaca = new System.Windows.Forms.GroupBox();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.dgvUnidades = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lstEmpresaDestinatario = new System.Windows.Forms.ListView();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gTipoVehiculo.SuspendLayout();
            this.gempresa.SuspendLayout();
            this.gtarjeta.SuspendLayout();
            this.gPlaca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidades)).BeginInit();
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
            this.lblTituloGuia.Size = new System.Drawing.Size(800, 58);
            this.lblTituloGuia.TabIndex = 1;
            this.lblTituloGuia.Text = "GESTIONAR UNIDADES TERCERO";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.gTipoVehiculo);
            this.groupBox1.Controls.Add(this.gempresa);
            this.groupBox1.Controls.Add(this.btnRegistrar);
            this.groupBox1.Controls.Add(this.gtarjeta);
            this.groupBox1.Controls.Add(this.gPlaca);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 101);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registrar Unidad";
            // 
            // gTipoVehiculo
            // 
            this.gTipoVehiculo.Controls.Add(this.cbxTipoVehiculo);
            this.gTipoVehiculo.Location = new System.Drawing.Point(514, 44);
            this.gTipoVehiculo.Name = "gTipoVehiculo";
            this.gTipoVehiculo.Size = new System.Drawing.Size(138, 38);
            this.gTipoVehiculo.TabIndex = 2;
            this.gTipoVehiculo.TabStop = false;
            this.gTipoVehiculo.Text = "Tipo Vehiculo";
            // 
            // cbxTipoVehiculo
            // 
            this.cbxTipoVehiculo.FormattingEnabled = true;
            this.cbxTipoVehiculo.Items.AddRange(new object[] {
            "TRACTO",
            "PLATAFORMA",
            "TOLVA",
            "FURGON",
            "TERMOKING",
            "CAMIONETA",
            "CAMION",
            "CISTERNA"});
            this.cbxTipoVehiculo.Location = new System.Drawing.Point(7, 14);
            this.cbxTipoVehiculo.Name = "cbxTipoVehiculo";
            this.cbxTipoVehiculo.Size = new System.Drawing.Size(121, 21);
            this.cbxTipoVehiculo.TabIndex = 0;
            this.cbxTipoVehiculo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            // 
            // gempresa
            // 
            this.gempresa.Controls.Add(this.txtEmpresaCliente);
            this.gempresa.Location = new System.Drawing.Point(8, 43);
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
            // gtarjeta
            // 
            this.gtarjeta.Controls.Add(this.txtTarjetaCirculacion);
            this.gtarjeta.Location = new System.Drawing.Point(368, 44);
            this.gtarjeta.Name = "gtarjeta";
            this.gtarjeta.Size = new System.Drawing.Size(132, 38);
            this.gtarjeta.TabIndex = 2;
            this.gtarjeta.TabStop = false;
            this.gtarjeta.Text = "Tarjeta Circulacion";
            // 
            // txtTarjetaCirculacion
            // 
            this.txtTarjetaCirculacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTarjetaCirculacion.Location = new System.Drawing.Point(7, 14);
            this.txtTarjetaCirculacion.MaxLength = 15;
            this.txtTarjetaCirculacion.Name = "txtTarjetaCirculacion";
            this.txtTarjetaCirculacion.Size = new System.Drawing.Size(118, 20);
            this.txtTarjetaCirculacion.TabIndex = 2;
            this.txtTarjetaCirculacion.Enter += new System.EventHandler(this.txtTarjetaCirculacion_Enter);
            this.txtTarjetaCirculacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarjetaCirculacion_KeyPress);
            this.txtTarjetaCirculacion.Leave += new System.EventHandler(this.txtTarjetaCirculacion_Leave);
            // 
            // gPlaca
            // 
            this.gPlaca.Controls.Add(this.txtPlaca);
            this.gPlaca.Location = new System.Drawing.Point(242, 44);
            this.gPlaca.Name = "gPlaca";
            this.gPlaca.Size = new System.Drawing.Size(120, 38);
            this.gPlaca.TabIndex = 1;
            this.gPlaca.TabStop = false;
            this.gPlaca.Text = "Placa";
            this.gPlaca.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtPlaca
            // 
            this.txtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlaca.Location = new System.Drawing.Point(7, 13);
            this.txtPlaca.MaxLength = 6;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(100, 20);
            this.txtPlaca.TabIndex = 1;
            this.txtPlaca.Enter += new System.EventHandler(this.txtPlaca_Enter);
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            this.txtPlaca.Leave += new System.EventHandler(this.txtPlaca_Leave);
            // 
            // dgvUnidades
            // 
            this.dgvUnidades.AllowUserToAddRows = false;
            this.dgvUnidades.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvUnidades.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUnidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUnidades.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvUnidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnidades.Location = new System.Drawing.Point(0, 159);
            this.dgvUnidades.Name = "dgvUnidades";
            this.dgvUnidades.RowHeadersVisible = false;
            this.dgvUnidades.Size = new System.Drawing.Size(800, 182);
            this.dgvUnidades.TabIndex = 19;
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
            this.lstEmpresaDestinatario.Location = new System.Drawing.Point(14, 135);
            this.lstEmpresaDestinatario.MultiSelect = false;
            this.lstEmpresaDestinatario.Name = "lstEmpresaDestinatario";
            this.lstEmpresaDestinatario.Size = new System.Drawing.Size(239, 10);
            this.lstEmpresaDestinatario.TabIndex = 93;
            this.lstEmpresaDestinatario.UseCompatibleStateImageBehavior = false;
            this.lstEmpresaDestinatario.View = System.Windows.Forms.View.Details;
            this.lstEmpresaDestinatario.Visible = false;
            this.lstEmpresaDestinatario.Enter += new System.EventHandler(this.lstEmpresaDestinatario_Enter);
            this.lstEmpresaDestinatario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpresaDestinatario_KeyPress);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
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
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 13);
            this.label1.TabIndex = 96;
            this.label1.Text = "NOTA: Si el transportista es persona natural (DNI o RUC 10), Ingresar en Empresa " +
    "\"TRANSPESA\"";
            // 
            // frmMestroUnidadesTercerosGuiaRemitente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 341);
            this.Controls.Add(this.lstEmpresaDestinatario);
            this.Controls.Add(this.dgvUnidades);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTituloGuia);
            this.Name = "frmMestroUnidadesTercerosGuiaRemitente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMestroUnidadesTerceros";
            this.Load += new System.EventHandler(this.frmMestroUnidadesTercerosGuiaRemitente_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gTipoVehiculo.ResumeLayout(false);
            this.gempresa.ResumeLayout(false);
            this.gempresa.PerformLayout();
            this.gtarjeta.ResumeLayout(false);
            this.gtarjeta.PerformLayout();
            this.gPlaca.ResumeLayout(false);
            this.gPlaca.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnidades)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.GroupBox gtarjeta;
        private System.Windows.Forms.TextBox txtTarjetaCirculacion;
        private System.Windows.Forms.GroupBox gPlaca;
        private System.Windows.Forms.TextBox txtPlaca;
        public System.Windows.Forms.DataGridView dgvUnidades;
        private System.Windows.Forms.GroupBox gempresa;
        private System.Windows.Forms.ListView lstEmpresaDestinatario;
        private System.Windows.Forms.GroupBox gTipoVehiculo;
        private System.Windows.Forms.ComboBox cbxTipoVehiculo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        public System.Windows.Forms.TextBox txtEmpresaCliente;
        private System.Windows.Forms.Label label1;
    }
}