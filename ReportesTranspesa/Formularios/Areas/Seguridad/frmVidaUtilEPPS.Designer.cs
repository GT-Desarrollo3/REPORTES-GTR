namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmVidaUtilEPPS
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVidaUtilEPPS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxTipoEPPS = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxArea2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cbxAreaProceso = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pActualizarVidaUtil = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVidaUtilEPPS_Act = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.cbxArea2_Act = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxTipoEPPS_Act = new System.Windows.Forms.ComboBox();
            this.cbxAreaProceso_Act = new System.Windows.Forms.ComboBox();
            this.dtgVidaUtilEPPS = new DevExpress.XtraGrid.GridControl();
            this.dgvExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pActualizarVidaUtil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVidaUtilEPPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.AllowDrop = true;
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(718, 161);
            this.panel3.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtVidaUtilEPPS);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxTipoEPPS);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxArea2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.cbxAreaProceso);
            this.groupBox1.Location = new System.Drawing.Point(34, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 138);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Vida Útil";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Meses de Vida Útil:";
            // 
            // txtVidaUtilEPPS
            // 
            this.txtVidaUtilEPPS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVidaUtilEPPS.Location = new System.Drawing.Point(124, 95);
            this.txtVidaUtilEPPS.MaxLength = 250;
            this.txtVidaUtilEPPS.Name = "txtVidaUtilEPPS";
            this.txtVidaUtilEPPS.Size = new System.Drawing.Size(160, 20);
            this.txtVidaUtilEPPS.TabIndex = 2;
            this.txtVidaUtilEPPS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVidaUtilEPPS_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tipo EPP:";
            // 
            // cbxTipoEPPS
            // 
            this.cbxTipoEPPS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoEPPS.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoEPPS.FormattingEnabled = true;
            this.cbxTipoEPPS.Location = new System.Drawing.Point(401, 59);
            this.cbxTipoEPPS.Name = "cbxTipoEPPS";
            this.cbxTipoEPPS.Size = new System.Drawing.Size(231, 21);
            this.cbxTipoEPPS.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Área:";
            // 
            // cbxArea2
            // 
            this.cbxArea2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea2.FormattingEnabled = true;
            this.cbxArea2.Items.AddRange(new object[] {
            "STAFF ADM",
            "OPERATIVO"});
            this.cbxArea2.Location = new System.Drawing.Point(62, 59);
            this.cbxArea2.Name = "cbxArea2";
            this.cbxArea2.Size = new System.Drawing.Size(222, 21);
            this.cbxArea2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Categoría de Vida Útil:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.Location = new System.Drawing.Point(576, 87);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(55, 43);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cbxAreaProceso
            // 
            this.cbxAreaProceso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxAreaProceso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAreaProceso.FormattingEnabled = true;
            this.cbxAreaProceso.Items.AddRange(new object[] {
            "SUPERVISOR DE OPERACIONES",
            "ASISTENTE/PROGRAMADOR DE OPERACIONES",
            "JEFE DE SEGURIDAD",
            "SUPERVISOR DE SEGURIDAD",
            "ASISTENTE DE SEGURIDAD",
            "SUPERVISOR DE FLOTA",
            "ASISTENTE DE FLOTA",
            "AYUDANTE DE PATIO",
            "COORDINADOR DE ALMACENES GRANELES",
            "CONDUCTORES DE RUTA",
            "OPERADOR DE LÍNEA AMARILLA",
            "PALERO ALMACÉN",
            "PERSONAL DE LAVADO",
            "PERSONAL DE CAMPO / OBREROS",
            "PERSONAL DE MANTENIMIENTO "});
            this.cbxAreaProceso.Location = new System.Drawing.Point(140, 23);
            this.cbxAreaProceso.Name = "cbxAreaProceso";
            this.cbxAreaProceso.Size = new System.Drawing.Size(492, 21);
            this.cbxAreaProceso.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(718, 43);
            this.label1.TabIndex = 12;
            this.label1.Text = "VIDA ÚTIL DE EPP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.AutoToolTip = true;
            this.editarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // pActualizarVidaUtil
            // 
            this.pActualizarVidaUtil.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pActualizarVidaUtil.Controls.Add(this.label10);
            this.pActualizarVidaUtil.Controls.Add(this.label9);
            this.pActualizarVidaUtil.Controls.Add(this.pictureBox1);
            this.pActualizarVidaUtil.Controls.Add(this.label6);
            this.pActualizarVidaUtil.Controls.Add(this.txtVidaUtilEPPS_Act);
            this.pActualizarVidaUtil.Controls.Add(this.label7);
            this.pActualizarVidaUtil.Controls.Add(this.btnActualizar);
            this.pActualizarVidaUtil.Controls.Add(this.cbxArea2_Act);
            this.pActualizarVidaUtil.Controls.Add(this.label8);
            this.pActualizarVidaUtil.Controls.Add(this.cbxTipoEPPS_Act);
            this.pActualizarVidaUtil.Controls.Add(this.cbxAreaProceso_Act);
            this.pActualizarVidaUtil.Location = new System.Drawing.Point(140, 162);
            this.pActualizarVidaUtil.Name = "pActualizarVidaUtil";
            this.pActualizarVidaUtil.Size = new System.Drawing.Size(444, 170);
            this.pActualizarVidaUtil.TabIndex = 13;
            this.pActualizarVidaUtil.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(131, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 19);
            this.label10.TabIndex = 18;
            this.label10.Text = "Editar Vida Útil de EPP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Meses de Vida Útil:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.pictureBox1.Location = new System.Drawing.Point(412, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Categoría de Vida Útil:";
            // 
            // txtVidaUtilEPPS_Act
            // 
            this.txtVidaUtilEPPS_Act.Location = new System.Drawing.Point(130, 129);
            this.txtVidaUtilEPPS_Act.MaxLength = 10;
            this.txtVidaUtilEPPS_Act.Name = "txtVidaUtilEPPS_Act";
            this.txtVidaUtilEPPS_Act.Size = new System.Drawing.Size(130, 20);
            this.txtVidaUtilEPPS_Act.TabIndex = 0;
            this.txtVidaUtilEPPS_Act.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVidaUtilEPPS_Act_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Área:";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.Image = global::ReportesTranspesa.Properties.Resources.editar;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(304, 123);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(116, 33);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "        ACTUALIZAR";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // cbxArea2_Act
            // 
            this.cbxArea2_Act.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxArea2_Act.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxArea2_Act.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxArea2_Act.FormattingEnabled = true;
            this.cbxArea2_Act.Items.AddRange(new object[] {
            "STAFF ADM",
            "OPERATIVO"});
            this.cbxArea2_Act.Location = new System.Drawing.Point(63, 90);
            this.cbxArea2_Act.Name = "cbxArea2_Act";
            this.cbxArea2_Act.Size = new System.Drawing.Size(122, 21);
            this.cbxArea2_Act.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Tipo EPP:";
            // 
            // cbxTipoEPPS_Act
            // 
            this.cbxTipoEPPS_Act.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoEPPS_Act.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoEPPS_Act.FormattingEnabled = true;
            this.cbxTipoEPPS_Act.Location = new System.Drawing.Point(276, 90);
            this.cbxTipoEPPS_Act.Name = "cbxTipoEPPS_Act";
            this.cbxTipoEPPS_Act.Size = new System.Drawing.Size(144, 21);
            this.cbxTipoEPPS_Act.TabIndex = 4;
            this.cbxTipoEPPS_Act.SelectedIndexChanged += new System.EventHandler(this.cbxTipoEPPS_Act_SelectedIndexChanged);
            // 
            // cbxAreaProceso_Act
            // 
            this.cbxAreaProceso_Act.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxAreaProceso_Act.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAreaProceso_Act.FormattingEnabled = true;
            this.cbxAreaProceso_Act.Items.AddRange(new object[] {
            "SUPERVISOR DE OPERACIONES",
            "ASISTENTE/PROGRAMADOR DE OPERACIONES",
            "JEFE DE SEGURIDAD",
            "SUPERVISOR DE SEGURIDAD",
            "ASISTENTE DE SEGURIDAD",
            "SUPERVISOR DE FLOTA",
            "ASISTENTE DE FLOTA",
            "AYUDANTE DE PATIO",
            "COORDINADOR DE ALMACENES GRANELES",
            "CONDUCTORES DE RUTA",
            "OPERADOR DE LÍNEA AMARILLA",
            "PALERO ALMACÉN",
            "PERSONAL DE LAVADO",
            "PERSONAL DE CAMPO / OBREROS",
            "PERSONAL DE MANTENIMIENTO "});
            this.cbxAreaProceso_Act.Location = new System.Drawing.Point(145, 51);
            this.cbxAreaProceso_Act.Name = "cbxAreaProceso_Act";
            this.cbxAreaProceso_Act.Size = new System.Drawing.Size(275, 21);
            this.cbxAreaProceso_Act.TabIndex = 2;
            // 
            // dtgVidaUtilEPPS
            // 
            this.dtgVidaUtilEPPS.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgVidaUtilEPPS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgVidaUtilEPPS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgVidaUtilEPPS.Location = new System.Drawing.Point(0, 204);
            this.dtgVidaUtilEPPS.MainView = this.dgvExpressVista;
            this.dtgVidaUtilEPPS.Name = "dtgVidaUtilEPPS";
            this.dtgVidaUtilEPPS.Size = new System.Drawing.Size(718, 321);
            this.dtgVidaUtilEPPS.TabIndex = 14;
            this.dtgVidaUtilEPPS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvExpressVista});
            // 
            // dgvExpressVista
            // 
            this.dgvExpressVista.GridControl = this.dtgVidaUtilEPPS;
            this.dgvExpressVista.Name = "dgvExpressVista";
            this.dgvExpressVista.OptionsBehavior.Editable = false;
            this.dgvExpressVista.OptionsView.RowAutoHeight = true;
            // 
            // frmVidaUtilEPPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 525);
            this.Controls.Add(this.pActualizarVidaUtil);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgVidaUtilEPPS);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVidaUtilEPPS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vida Útil de EPP";
            this.Load += new System.EventHandler(this.frmVidaUtilEPPS_Load);
            this.Shown += new System.EventHandler(this.frmVidaUtilEPPS_Shown);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.pActualizarVidaUtil.ResumeLayout(false);
            this.pActualizarVidaUtil.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgVidaUtilEPPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxAreaProceso;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtVidaUtilEPPS;
        private System.Windows.Forms.ComboBox cbxTipoEPPS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.Panel pActualizarVidaUtil;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtVidaUtilEPPS_Act;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.ComboBox cbxAreaProceso_Act;
        private System.Windows.Forms.ComboBox cbxTipoEPPS_Act;
        private DevExpress.XtraGrid.GridControl dtgVidaUtilEPPS;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvExpressVista;
        private System.Windows.Forms.ComboBox cbxArea2;
        private System.Windows.Forms.ComboBox cbxArea2_Act;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
    }
}