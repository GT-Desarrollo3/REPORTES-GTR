namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class FrmMaestroRutaClienteTransportista
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
            this.txtBuscarRuta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtRemitente = new System.Windows.Forms.TextBox();
            this.txtBuscarRemitente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtDestinatario = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtDireccionDestino = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDireccionPartida = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.dgvMaestro = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstEmpresaCliente = new System.Windows.Forms.ListView();
            this.lstRuta = new System.Windows.Forms.ListView();
            this.lstDestinatario = new System.Windows.Forms.ListView();
            this.lstDireccionPartida = new System.Windows.Forms.ListView();
            this.lstDireccionDestino = new System.Windows.Forms.ListView();
            this.lstRemitente = new System.Windows.Forms.ListView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaestro)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTituloGuia
            // 
            this.lblTituloGuia.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblTituloGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloGuia.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTituloGuia.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGuia.Name = "lblTituloGuia";
            this.lblTituloGuia.Size = new System.Drawing.Size(697, 37);
            this.lblTituloGuia.TabIndex = 1;
            this.lblTituloGuia.Text = "CONFIGURAR MAESTRO RUTA - CLIENTE PREDETERMINADO";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.txtBuscarRuta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBuscarRemitente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(697, 366);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registrar Datos Cliente y Guia Remitente";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtBuscarRuta
            // 
            this.txtBuscarRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscarRuta.Location = new System.Drawing.Point(391, 332);
            this.txtBuscarRuta.Name = "txtBuscarRuta";
            this.txtBuscarRuta.Size = new System.Drawing.Size(203, 21);
            this.txtBuscarRuta.TabIndex = 101;
            this.txtBuscarRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarRuta_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 102;
            this.label2.Text = "Buscar Ruta:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtRemitente);
            this.groupBox7.Location = new System.Drawing.Point(5, 22);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(229, 51);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Remitente";
            // 
            // txtRemitente
            // 
            this.txtRemitente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemitente.Location = new System.Drawing.Point(6, 20);
            this.txtRemitente.Name = "txtRemitente";
            this.txtRemitente.Size = new System.Drawing.Size(203, 21);
            this.txtRemitente.TabIndex = 1;
            this.txtRemitente.Enter += new System.EventHandler(this.txtRemitente_Enter);
            this.txtRemitente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemitente_KeyPress);
            this.txtRemitente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRemitente_KeyUp);
            this.txtRemitente.Leave += new System.EventHandler(this.txtRemitente_Leave);
            // 
            // txtBuscarRemitente
            // 
            this.txtBuscarRemitente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscarRemitente.Location = new System.Drawing.Point(97, 334);
            this.txtBuscarRemitente.Name = "txtBuscarRemitente";
            this.txtBuscarRemitente.Size = new System.Drawing.Size(200, 21);
            this.txtBuscarRemitente.TabIndex = 6;
            this.txtBuscarRemitente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarRemitente_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 14);
            this.label1.TabIndex = 100;
            this.label1.Text = "Buscar Cliente:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtDestinatario);
            this.groupBox6.Location = new System.Drawing.Point(6, 84);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(229, 51);
            this.groupBox6.TabIndex = 99;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Destinatario";
            // 
            // txtDestinatario
            // 
            this.txtDestinatario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDestinatario.Location = new System.Drawing.Point(6, 21);
            this.txtDestinatario.Name = "txtDestinatario";
            this.txtDestinatario.Size = new System.Drawing.Size(203, 21);
            this.txtDestinatario.TabIndex = 2;
            this.txtDestinatario.Enter += new System.EventHandler(this.txtDestinatario_Enter);
            this.txtDestinatario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestinatario_KeyPress);
            this.txtDestinatario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDestinatario_KeyUp);
            this.txtDestinatario.Leave += new System.EventHandler(this.txtDestinatario_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtDireccionDestino);
            this.groupBox5.Location = new System.Drawing.Point(261, 84);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(229, 51);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Direccion Destino";
            // 
            // txtDireccionDestino
            // 
            this.txtDireccionDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccionDestino.Location = new System.Drawing.Point(6, 20);
            this.txtDireccionDestino.Name = "txtDireccionDestino";
            this.txtDireccionDestino.Size = new System.Drawing.Size(203, 21);
            this.txtDireccionDestino.TabIndex = 5;
            this.txtDireccionDestino.Enter += new System.EventHandler(this.txtDireccionDestino_Enter);
            this.txtDireccionDestino.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccionDestino_KeyPress);
            this.txtDireccionDestino.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDireccionDestino_KeyUp);
            this.txtDireccionDestino.Leave += new System.EventHandler(this.txtDireccionDestino_Leave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtDireccionPartida);
            this.groupBox4.Location = new System.Drawing.Point(262, 23);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(229, 51);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Direccion Partida";
            // 
            // txtDireccionPartida
            // 
            this.txtDireccionPartida.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccionPartida.Location = new System.Drawing.Point(6, 19);
            this.txtDireccionPartida.Name = "txtDireccionPartida";
            this.txtDireccionPartida.Size = new System.Drawing.Size(203, 21);
            this.txtDireccionPartida.TabIndex = 4;
            this.txtDireccionPartida.Enter += new System.EventHandler(this.txtDireccionPartida_Enter);
            this.txtDireccionPartida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccionPartida_KeyPress);
            this.txtDireccionPartida.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDireccionPartida_KeyUp);
            this.txtDireccionPartida.Leave += new System.EventHandler(this.txtDireccionPartida_Leave);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.nuevo_button;
            this.btnAgregar.Location = new System.Drawing.Point(354, 161);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(117, 46);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCliente);
            this.groupBox3.Location = new System.Drawing.Point(15, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(229, 51);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DATOS CLIENTE A FACTURAR";
            // 
            // txtCliente
            // 
            this.txtCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCliente.Location = new System.Drawing.Point(6, 20);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(203, 21);
            this.txtCliente.TabIndex = 1;
            this.txtCliente.Enter += new System.EventHandler(this.txtCliente_Enter);
            this.txtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCliente_KeyPress);
            this.txtCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCliente_KeyUp);
            this.txtCliente.Leave += new System.EventHandler(this.txtCliente_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRuta);
            this.groupBox2.Location = new System.Drawing.Point(8, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 51);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ruta";
            // 
            // txtRuta
            // 
            this.txtRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRuta.Location = new System.Drawing.Point(6, 22);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(291, 21);
            this.txtRuta.TabIndex = 3;
            this.txtRuta.Enter += new System.EventHandler(this.txtRuta_Enter);
            this.txtRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuta_KeyPress);
            this.txtRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRuta_KeyUp);
            this.txtRuta.Leave += new System.EventHandler(this.txtRuta_Leave);
            // 
            // dgvMaestro
            // 
            this.dgvMaestro.AllowUserToAddRows = false;
            this.dgvMaestro.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMaestro.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMaestro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMaestro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMaestro.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvMaestro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaestro.Location = new System.Drawing.Point(0, 403);
            this.dgvMaestro.Name = "dgvMaestro";
            this.dgvMaestro.ReadOnly = true;
            this.dgvMaestro.RowHeadersVisible = false;
            this.dgvMaestro.RowTemplate.ErrorText = "No a ingersado un dato correcto";
            this.dgvMaestro.Size = new System.Drawing.Size(697, 270);
            this.dgvMaestro.TabIndex = 19;
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
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancelmini;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // lstEmpresaCliente
            // 
            this.lstEmpresaCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstEmpresaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpresaCliente.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpresaCliente.FullRowSelect = true;
            this.lstEmpresaCliente.GridLines = true;
            this.lstEmpresaCliente.Location = new System.Drawing.Point(21, 107);
            this.lstEmpresaCliente.MultiSelect = false;
            this.lstEmpresaCliente.Name = "lstEmpresaCliente";
            this.lstEmpresaCliente.Size = new System.Drawing.Size(224, 10);
            this.lstEmpresaCliente.TabIndex = 91;
            this.lstEmpresaCliente.UseCompatibleStateImageBehavior = false;
            this.lstEmpresaCliente.View = System.Windows.Forms.View.Details;
            this.lstEmpresaCliente.Visible = false;
            this.lstEmpresaCliente.Enter += new System.EventHandler(this.lstEmpresaDestinatario_Enter);
            this.lstEmpresaCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpresaCliente_KeyPress);
            this.lstEmpresaCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstEmpresaCliente_KeyUp);
            // 
            // lstRuta
            // 
            this.lstRuta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRuta.ForeColor = System.Drawing.Color.Navy;
            this.lstRuta.FullRowSelect = true;
            this.lstRuta.GridLines = true;
            this.lstRuta.Location = new System.Drawing.Point(25, 324);
            this.lstRuta.MultiSelect = false;
            this.lstRuta.Name = "lstRuta";
            this.lstRuta.Size = new System.Drawing.Size(291, 10);
            this.lstRuta.TabIndex = 92;
            this.lstRuta.UseCompatibleStateImageBehavior = false;
            this.lstRuta.View = System.Windows.Forms.View.Details;
            this.lstRuta.Visible = false;
            this.lstRuta.Enter += new System.EventHandler(this.lstRuta_Enter);
            this.lstRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRuta_KeyPress);
            this.lstRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstRuta_KeyUp);
            // 
            // lstDestinatario
            // 
            this.lstDestinatario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstDestinatario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDestinatario.ForeColor = System.Drawing.Color.Navy;
            this.lstDestinatario.FullRowSelect = true;
            this.lstDestinatario.GridLines = true;
            this.lstDestinatario.Location = new System.Drawing.Point(23, 249);
            this.lstDestinatario.MultiSelect = false;
            this.lstDestinatario.Name = "lstDestinatario";
            this.lstDestinatario.Size = new System.Drawing.Size(224, 10);
            this.lstDestinatario.TabIndex = 93;
            this.lstDestinatario.UseCompatibleStateImageBehavior = false;
            this.lstDestinatario.View = System.Windows.Forms.View.Details;
            this.lstDestinatario.Visible = false;
            this.lstDestinatario.Enter += new System.EventHandler(this.lstDestinatario_Enter);
            this.lstDestinatario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDestinatario_KeyPress);
            this.lstDestinatario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstDestinatario_KeyUp);
            // 
            // lstDireccionPartida
            // 
            this.lstDireccionPartida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstDireccionPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDireccionPartida.ForeColor = System.Drawing.Color.Navy;
            this.lstDireccionPartida.FullRowSelect = true;
            this.lstDireccionPartida.GridLines = true;
            this.lstDireccionPartida.Location = new System.Drawing.Point(279, 186);
            this.lstDireccionPartida.MultiSelect = false;
            this.lstDireccionPartida.Name = "lstDireccionPartida";
            this.lstDireccionPartida.Size = new System.Drawing.Size(224, 10);
            this.lstDireccionPartida.TabIndex = 95;
            this.lstDireccionPartida.UseCompatibleStateImageBehavior = false;
            this.lstDireccionPartida.View = System.Windows.Forms.View.Details;
            this.lstDireccionPartida.Visible = false;
            this.lstDireccionPartida.Enter += new System.EventHandler(this.lstDireccionPartida_Enter);
            this.lstDireccionPartida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDireccionPartida_KeyPress);
            this.lstDireccionPartida.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstDireccionPartida_KeyUp);
            // 
            // lstDireccionDestino
            // 
            this.lstDireccionDestino.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstDireccionDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDireccionDestino.ForeColor = System.Drawing.Color.Navy;
            this.lstDireccionDestino.FullRowSelect = true;
            this.lstDireccionDestino.GridLines = true;
            this.lstDireccionDestino.Location = new System.Drawing.Point(278, 248);
            this.lstDireccionDestino.MultiSelect = false;
            this.lstDireccionDestino.Name = "lstDireccionDestino";
            this.lstDireccionDestino.Size = new System.Drawing.Size(224, 10);
            this.lstDireccionDestino.TabIndex = 96;
            this.lstDireccionDestino.UseCompatibleStateImageBehavior = false;
            this.lstDireccionDestino.View = System.Windows.Forms.View.Details;
            this.lstDireccionDestino.Visible = false;
            this.lstDireccionDestino.Enter += new System.EventHandler(this.lstDireccionDestino_Enter);
            this.lstDireccionDestino.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDireccionDestino_KeyPress);
            this.lstDireccionDestino.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstDireccionDestino_KeyUp);
            // 
            // lstRemitente
            // 
            this.lstRemitente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstRemitente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRemitente.ForeColor = System.Drawing.Color.Navy;
            this.lstRemitente.FullRowSelect = true;
            this.lstRemitente.GridLines = true;
            this.lstRemitente.Location = new System.Drawing.Point(22, 186);
            this.lstRemitente.MultiSelect = false;
            this.lstRemitente.Name = "lstRemitente";
            this.lstRemitente.Size = new System.Drawing.Size(224, 10);
            this.lstRemitente.TabIndex = 97;
            this.lstRemitente.UseCompatibleStateImageBehavior = false;
            this.lstRemitente.View = System.Windows.Forms.View.Details;
            this.lstRemitente.Visible = false;
            this.lstRemitente.Enter += new System.EventHandler(this.lstRemitente_Enter);
            this.lstRemitente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRemitente_KeyPress);
            this.lstRemitente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstRemitente_KeyUp);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.groupBox7);
            this.groupBox8.Controls.Add(this.groupBox4);
            this.groupBox8.Controls.Add(this.groupBox5);
            this.groupBox8.Controls.Add(this.groupBox2);
            this.groupBox8.Controls.Add(this.groupBox6);
            this.groupBox8.Controls.Add(this.btnAgregar);
            this.groupBox8.Location = new System.Drawing.Point(11, 86);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(630, 226);
            this.groupBox8.TabIndex = 103;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "DATOS GUIA REMITENTE";
            // 
            // FrmMaestroRutaClienteTransportista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(697, 673);
            this.Controls.Add(this.lstRemitente);
            this.Controls.Add(this.lstDireccionDestino);
            this.Controls.Add(this.lstDireccionPartida);
            this.Controls.Add(this.lstDestinatario);
            this.Controls.Add(this.lstRuta);
            this.Controls.Add(this.lstEmpresaCliente);
            this.Controls.Add(this.dgvMaestro);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTituloGuia);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmMaestroRutaClienteTransportista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MaestroRutaClienteTransportista";
            this.Load += new System.EventHandler(this.FrmMaestroRutaClienteTransportista_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaestro)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView dgvMaestro;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtDireccionDestino;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtDireccionPartida;
        private System.Windows.Forms.ListView lstEmpresaCliente;
        private System.Windows.Forms.ListView lstRuta;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtDestinatario;
        private System.Windows.Forms.ListView lstDestinatario;
        private System.Windows.Forms.TextBox txtBuscarRemitente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ListView lstDireccionPartida;
        private System.Windows.Forms.ListView lstDireccionDestino;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListView lstRemitente;
        public System.Windows.Forms.TextBox txtCliente;
        public System.Windows.Forms.TextBox txtRuta;
        public System.Windows.Forms.TextBox txtRemitente;
        private System.Windows.Forms.TextBox txtBuscarRuta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox8;
    }
}