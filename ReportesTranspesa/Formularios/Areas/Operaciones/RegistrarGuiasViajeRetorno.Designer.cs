namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    partial class RegistrarGuiasViajeRetorno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarGuiasViajeRetorno));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvGuias = new System.Windows.Forms.DataGridView();
            this.IdOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaEmision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdGuia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUIATRANSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoViaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuiaRemitente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuiaOtros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdViaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idConductor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conductor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAnexar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFechaViaje = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblConductor = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCodigoViaje = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDesvincular = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuias)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1164, 511);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 126);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1164, 385);
            this.panel3.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dgvGuias);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1164, 385);
            this.panel5.TabIndex = 1;
            // 
            // dgvGuias
            // 
            this.dgvGuias.AllowUserToAddRows = false;
            this.dgvGuias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvGuias.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvGuias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvGuias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGuias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdOT,
            this.Tipo,
            this.FechaEmision,
            this.IdGuia,
            this.GUIATRANSP,
            this.SERIE,
            this.Numero,
            this.CodigoViaje,
            this.GuiaRemitente,
            this.GuiaOtros,
            this.Observaciones,
            this.IdViaje,
            this.idConductor,
            this.Conductor});
            this.dgvGuias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGuias.Location = new System.Drawing.Point(0, 0);
            this.dgvGuias.Name = "dgvGuias";
            this.dgvGuias.RowHeadersVisible = false;
            this.dgvGuias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGuias.Size = new System.Drawing.Size(1164, 385);
            this.dgvGuias.TabIndex = 12;
            this.dgvGuias.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvGuias_CellBeginEdit_1);
            this.dgvGuias.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGuias_CellEndEdit);
            this.dgvGuias.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvGuias_CellValidating_1);
            this.dgvGuias.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvGuias_EditingControlShowing);
            this.dgvGuias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvGuias_KeyPress);
            // 
            // IdOT
            // 
            this.IdOT.HeaderText = "IdOt";
            this.IdOT.Name = "IdOT";
            this.IdOT.ReadOnly = true;
            this.IdOT.Visible = false;
            this.IdOT.Width = 52;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 53;
            // 
            // FechaEmision
            // 
            this.FechaEmision.HeaderText = "FechaAsignacion";
            this.FechaEmision.Name = "FechaEmision";
            this.FechaEmision.ReadOnly = true;
            this.FechaEmision.Width = 114;
            // 
            // IdGuia
            // 
            this.IdGuia.HeaderText = "IdGuia";
            this.IdGuia.Name = "IdGuia";
            this.IdGuia.ReadOnly = true;
            this.IdGuia.Visible = false;
            this.IdGuia.Width = 63;
            // 
            // GUIATRANSP
            // 
            this.GUIATRANSP.HeaderText = "GUIA/TR";
            this.GUIATRANSP.Name = "GUIATRANSP";
            this.GUIATRANSP.Width = 78;
            // 
            // SERIE
            // 
            this.SERIE.HeaderText = "SERIE";
            this.SERIE.Name = "SERIE";
            this.SERIE.ReadOnly = true;
            this.SERIE.Width = 64;
            // 
            // Numero
            // 
            this.Numero.HeaderText = "Numero";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            this.Numero.Width = 69;
            // 
            // CodigoViaje
            // 
            this.CodigoViaje.HeaderText = "CodigoViaje";
            this.CodigoViaje.Name = "CodigoViaje";
            this.CodigoViaje.ReadOnly = true;
            this.CodigoViaje.Width = 88;
            // 
            // GuiaRemitente
            // 
            this.GuiaRemitente.HeaderText = "GuiaRemitente";
            this.GuiaRemitente.Name = "GuiaRemitente";
            this.GuiaRemitente.Width = 102;
            // 
            // GuiaOtros
            // 
            this.GuiaOtros.HeaderText = "GuiasOtros";
            this.GuiaOtros.Name = "GuiaOtros";
            this.GuiaOtros.Width = 84;
            // 
            // Observaciones
            // 
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.Width = 103;
            // 
            // IdViaje
            // 
            this.IdViaje.HeaderText = "IdViaje";
            this.IdViaje.Name = "IdViaje";
            this.IdViaje.ReadOnly = true;
            this.IdViaje.Visible = false;
            this.IdViaje.Width = 64;
            // 
            // idConductor
            // 
            this.idConductor.HeaderText = "idConductor";
            this.idConductor.Name = "idConductor";
            this.idConductor.ReadOnly = true;
            this.idConductor.Visible = false;
            this.idConductor.Width = 89;
            // 
            // Conductor
            // 
            this.Conductor.HeaderText = "Conductor";
            this.Conductor.Name = "Conductor";
            this.Conductor.ReadOnly = true;
            this.Conductor.Width = 81;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDesvincular);
            this.panel2.Controls.Add(this.btnAnexar);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1164, 83);
            this.panel2.TabIndex = 4;
            // 
            // btnAnexar
            // 
            this.btnAnexar.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnAnexar.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.btnAnexar.Location = new System.Drawing.Point(12, 13);
            this.btnAnexar.Name = "btnAnexar";
            this.btnAnexar.Size = new System.Drawing.Size(79, 60);
            this.btnAnexar.TabIndex = 7;
            this.btnAnexar.Text = "Anexar";
            this.btnAnexar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAnexar.UseVisualStyleBackColor = false;
            this.btnAnexar.Click += new System.EventHandler(this.btnAnexar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRuta);
            this.groupBox3.Location = new System.Drawing.Point(803, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 41);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ruta";
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.BackColor = System.Drawing.Color.Yellow;
            this.lblRuta.Location = new System.Drawing.Point(16, 19);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(40, 13);
            this.lblRuta.TabIndex = 1;
            this.lblRuta.Text = "lblRuta";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFechaViaje);
            this.groupBox2.Location = new System.Drawing.Point(538, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 41);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fecha Viaje";
            // 
            // lblFechaViaje
            // 
            this.lblFechaViaje.AutoSize = true;
            this.lblFechaViaje.BackColor = System.Drawing.Color.Yellow;
            this.lblFechaViaje.Location = new System.Drawing.Point(16, 19);
            this.lblFechaViaje.Name = "lblFechaViaje";
            this.lblFechaViaje.Size = new System.Drawing.Size(47, 13);
            this.lblFechaViaje.TabIndex = 1;
            this.lblFechaViaje.Text = "lblFecha";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblConductor);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(273, 32);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(259, 41);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CONDUCTOR";
            // 
            // lblConductor
            // 
            this.lblConductor.AutoSize = true;
            this.lblConductor.BackColor = System.Drawing.Color.Yellow;
            this.lblConductor.Location = new System.Drawing.Point(16, 19);
            this.lblConductor.Name = "lblConductor";
            this.lblConductor.Size = new System.Drawing.Size(66, 13);
            this.lblConductor.TabIndex = 1;
            this.lblConductor.Text = "lblConductor";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCodigoViaje);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(104, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 41);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CODIGO VIAJE";
            // 
            // lblCodigoViaje
            // 
            this.lblCodigoViaje.AutoSize = true;
            this.lblCodigoViaje.BackColor = System.Drawing.Color.Yellow;
            this.lblCodigoViaje.Location = new System.Drawing.Point(21, 20);
            this.lblCodigoViaje.Name = "lblCodigoViaje";
            this.lblCodigoViaje.Size = new System.Drawing.Size(73, 13);
            this.lblCodigoViaje.TabIndex = 0;
            this.lblCodigoViaje.Text = "lblCodigoViaje";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1164, 43);
            this.label1.TabIndex = 3;
            this.label1.Text = "REGISTRAR GUIAS A VIAJE RETORNO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.label1_PreviewKeyDown);
            // 
            // btnDesvincular
            // 
            this.btnDesvincular.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnDesvincular.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.btnDesvincular.Location = new System.Drawing.Point(1080, 13);
            this.btnDesvincular.Name = "btnDesvincular";
            this.btnDesvincular.Size = new System.Drawing.Size(72, 60);
            this.btnDesvincular.TabIndex = 8;
            this.btnDesvincular.Text = "Desvincular";
            this.btnDesvincular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDesvincular.UseVisualStyleBackColor = false;
            this.btnDesvincular.Click += new System.EventHandler(this.btnDesvincular_Click);
            // 
            // RegistrarGuiasViajeRetorno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1164, 511);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistrarGuiasViajeRetorno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistrarGuiasViajeRetorno";
            this.Load += new System.EventHandler(this.RegistrarGuiasViaje_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RegistrarGuiasViaje_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuias)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblConductor;
        private System.Windows.Forms.Label lblCodigoViaje;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblFechaViaje;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRuta;
        public System.Windows.Forms.DataGridView dgvGuias;
        private System.Windows.Forms.Button btnAnexar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaEmision;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdGuia;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUIATRANSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoViaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuiaRemitente;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuiaOtros;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdViaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn idConductor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conductor;
        private System.Windows.Forms.Button btnDesvincular;
    }
}