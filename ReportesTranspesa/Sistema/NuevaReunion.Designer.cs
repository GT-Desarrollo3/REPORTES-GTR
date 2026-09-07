namespace ReportesTranspesa.Sistema
{
    partial class NuevaReunion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevaReunion));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaReunion = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gEquipo = new System.Windows.Forms.GroupBox();
            this.dgvEquipos = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreEquipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Disponibilidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idOficina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreOficina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroSala = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreSala = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEquipoMaestro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbxOficina = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxSala = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLinkReunion = new DevExpress.XtraEditors.HyperLinkEdit();
            this.btnAgendar = new System.Windows.Forms.Button();
            this.idEquiposXML = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gEquipo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipos)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkReunion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpHoraInicio);
            this.groupBox4.Controls.Add(this.dtpHoraFin);
            this.groupBox4.Location = new System.Drawing.Point(262, 133);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(199, 53);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Hora Inicio - Fin";
            // 
            // dtpHoraInicio
            // 
            this.dtpHoraInicio.CustomFormat = "HH:00:00";
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraInicio.Location = new System.Drawing.Point(15, 24);
            this.dtpHoraInicio.Name = "dtpHoraInicio";
            this.dtpHoraInicio.Size = new System.Drawing.Size(70, 20);
            this.dtpHoraInicio.TabIndex = 14;
            this.dtpHoraInicio.ValueChanged += new System.EventHandler(this.dtpHoraInicio_ValueChanged);
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.CustomFormat = "HH:00:00";
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraFin.Location = new System.Drawing.Point(106, 24);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.Size = new System.Drawing.Size(70, 20);
            this.dtpHoraFin.TabIndex = 15;
            this.dtpHoraFin.ValueChanged += new System.EventHandler(this.dtpHoraFin_ValueChanged);
            this.dtpHoraFin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtpHoraFin_KeyUp);
            // 
            // dtpFechaReunion
            // 
            this.dtpFechaReunion.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaReunion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaReunion.Location = new System.Drawing.Point(15, 20);
            this.dtpFechaReunion.Name = "dtpFechaReunion";
            this.dtpFechaReunion.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaReunion.TabIndex = 1;
            this.dtpFechaReunion.ValueChanged += new System.EventHandler(this.dtpFechaFin_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFechaReunion);
            this.groupBox3.Location = new System.Drawing.Point(262, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(132, 53);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fecha Inicio Reunion";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(549, 49);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nueva Reunion";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gEquipo
            // 
            this.gEquipo.Controls.Add(this.dgvEquipos);
            this.gEquipo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gEquipo.Location = new System.Drawing.Point(0, 262);
            this.gEquipo.Name = "gEquipo";
            this.gEquipo.Size = new System.Drawing.Size(549, 193);
            this.gEquipo.TabIndex = 10;
            this.gEquipo.TabStop = false;
            this.gEquipo.Text = "    Solicitar Equipo";
            // 
            // dgvEquipos
            // 
            this.dgvEquipos.AllowUserToAddRows = false;
            this.dgvEquipos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEquipos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEquipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEquipos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.idEquipo,
            this.CodEquipo,
            this.NombreEquipo,
            this.Disponibilidad,
            this.idOficina,
            this.NombreOficina,
            this.NumeroSala,
            this.NombreSala,
            this.Estado,
            this.idEquipoMaestro,
            this.idEquiposXML});
            this.dgvEquipos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEquipos.Location = new System.Drawing.Point(3, 16);
            this.dgvEquipos.Name = "dgvEquipos";
            this.dgvEquipos.RowHeadersVisible = false;
            this.dgvEquipos.Size = new System.Drawing.Size(543, 174);
            this.dgvEquipos.TabIndex = 11;
            // 
            // Check
            // 
            this.Check.HeaderText = "Check";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.Width = 50;
            // 
            // idEquipo
            // 
            this.idEquipo.HeaderText = "idEquipo";
            this.idEquipo.Name = "idEquipo";
            this.idEquipo.Visible = false;
            // 
            // CodEquipo
            // 
            this.CodEquipo.HeaderText = "CodEquipo";
            this.CodEquipo.Name = "CodEquipo";
            this.CodEquipo.Visible = false;
            // 
            // NombreEquipo
            // 
            this.NombreEquipo.HeaderText = "NombreEquipo";
            this.NombreEquipo.Name = "NombreEquipo";
            // 
            // Disponibilidad
            // 
            this.Disponibilidad.HeaderText = "Disponibilidad";
            this.Disponibilidad.Name = "Disponibilidad";
            // 
            // idOficina
            // 
            this.idOficina.HeaderText = "idOficina";
            this.idOficina.Name = "idOficina";
            this.idOficina.Visible = false;
            // 
            // NombreOficina
            // 
            this.NombreOficina.HeaderText = "NombreOficina";
            this.NombreOficina.Name = "NombreOficina";
            // 
            // NumeroSala
            // 
            this.NumeroSala.HeaderText = "NumeroSala";
            this.NumeroSala.Name = "NumeroSala";
            this.NumeroSala.Visible = false;
            // 
            // NombreSala
            // 
            this.NombreSala.HeaderText = "NombreSala";
            this.NombreSala.Name = "NombreSala";
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.Visible = false;
            // 
            // idEquipoMaestro
            // 
            this.idEquipoMaestro.HeaderText = "idEquipoMaestro";
            this.idEquipoMaestro.Name = "idEquipoMaestro";
            this.idEquipoMaestro.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbxOficina);
            this.groupBox5.Location = new System.Drawing.Point(14, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(223, 49);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Oficina";
            // 
            // cbxOficina
            // 
            this.cbxOficina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOficina.FormattingEnabled = true;
            this.cbxOficina.Location = new System.Drawing.Point(12, 19);
            this.cbxOficina.Name = "cbxOficina";
            this.cbxOficina.Size = new System.Drawing.Size(205, 21);
            this.cbxOficina.TabIndex = 5;
            this.cbxOficina.SelectedIndexChanged += new System.EventHandler(this.cbxOficina_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxSala);
            this.groupBox2.Location = new System.Drawing.Point(14, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 49);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sala Nr°";
            // 
            // cbxSala
            // 
            this.cbxSala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSala.FormattingEnabled = true;
            this.cbxSala.Location = new System.Drawing.Point(15, 19);
            this.cbxSala.Name = "cbxSala";
            this.cbxSala.Size = new System.Drawing.Size(205, 21);
            this.cbxSala.TabIndex = 5;
            this.cbxSala.SelectedIndexChanged += new System.EventHandler(this.cbxSala_SelectedIndexChanged);
            this.cbxSala.SelectionChangeCommitted += new System.EventHandler(this.cbxSala_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLinkReunion);
            this.groupBox1.Location = new System.Drawing.Point(14, 202);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 47);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Link Reunion";
            // 
            // txtLinkReunion
            // 
            this.txtLinkReunion.Location = new System.Drawing.Point(10, 19);
            this.txtLinkReunion.Name = "txtLinkReunion";
            this.txtLinkReunion.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtLinkReunion.Size = new System.Drawing.Size(479, 20);
            this.txtLinkReunion.TabIndex = 12;
            // 
            // btnAgendar
            // 
            this.btnAgendar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgendar.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.btnAgendar.Location = new System.Drawing.Point(411, 76);
            this.btnAgendar.Name = "btnAgendar";
            this.btnAgendar.Size = new System.Drawing.Size(115, 43);
            this.btnAgendar.TabIndex = 18;
            this.btnAgendar.Text = "Agendar Reunion";
            this.btnAgendar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgendar.UseVisualStyleBackColor = true;
            this.btnAgendar.Click += new System.EventHandler(this.AgendarReunon_Click);
            // 
            // idEquiposXML
            // 
            this.idEquiposXML.HeaderText = "idEquiposXML";
            this.idEquiposXML.Name = "idEquiposXML";
            this.idEquiposXML.Visible = false;
            // 
            // NuevaReunion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ClientSize = new System.Drawing.Size(549, 455);
            this.Controls.Add(this.btnAgendar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.gEquipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NuevaReunion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NuevaReunion";
            this.Load += new System.EventHandler(this.NuevaReunion_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.gEquipo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipos)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkReunion.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpFechaReunion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpHoraFin;
        private System.Windows.Forms.DateTimePicker dtpHoraInicio;
        private System.Windows.Forms.GroupBox gEquipo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbxOficina;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxSala;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAgendar;
        public System.Windows.Forms.DataGridView dgvEquipos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreEquipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Disponibilidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn idOficina;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreOficina;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroSala;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreSala;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEquipoMaestro;
        private DevExpress.XtraEditors.HyperLinkEdit txtLinkReunion;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEquiposXML;


    }
}