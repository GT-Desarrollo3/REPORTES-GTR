namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.AsignacionUniformes
{
    partial class frmAsignarUniformes
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtgPersonalData = new DevExpress.XtraGrid.GridControl();
            this.dgvExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cbxUniforme = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVidaUtil = new System.Windows.Forms.TextBox();
            this.txtTalla = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPuesto = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPersonalData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(930, 43);
            this.label1.TabIndex = 14;
            this.label1.Text = "ASIGNAR UNIFORME A PERSONAL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgPersonalData
            // 
            this.dtgPersonalData.AllowDrop = true;
            this.dtgPersonalData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgPersonalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgPersonalData.Location = new System.Drawing.Point(0, 0);
            this.dtgPersonalData.MainView = this.dgvExpressVista;
            this.dtgPersonalData.Name = "dtgPersonalData";
            this.dtgPersonalData.Size = new System.Drawing.Size(592, 518);
            this.dtgPersonalData.TabIndex = 11;
            this.dtgPersonalData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvExpressVista});
            this.dtgPersonalData.Click += new System.EventHandler(this.dtgPersonalData_Click);
            // 
            // dgvExpressVista
            // 
            this.dgvExpressVista.GridControl = this.dtgPersonalData;
            this.dgvExpressVista.Name = "dgvExpressVista";
            this.dgvExpressVista.OptionsBehavior.Editable = false;
            this.dgvExpressVista.OptionsView.ColumnAutoWidth = false;
            this.dgvExpressVista.OptionsView.RowAutoHeight = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgPersonalData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 518);
            this.panel2.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(19, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Empleado:";
            // 
            // txtPersonal
            // 
            this.txtPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPersonal.Location = new System.Drawing.Point(22, 49);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(263, 21);
            this.txtPersonal.TabIndex = 0;
            this.txtPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonal_KeyPress);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(592, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(338, 518);
            this.panel3.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCantidad);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnCancelar);
            this.groupBox2.Controls.Add(this.btnGuardar);
            this.groupBox2.Controls.Add(this.cbxUniforme);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtVidaUtil);
            this.groupBox2.Controls.Add(this.txtTalla);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox2.Location = new System.Drawing.Point(16, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 267);
            this.groupBox2.TabIndex = 115;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccionar Uniforme:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.txtCantidad.Location = new System.Drawing.Point(135, 164);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(150, 21);
            this.txtCantidad.TabIndex = 121;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label6.Location = new System.Drawing.Point(64, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 16);
            this.label6.TabIndex = 122;
            this.label6.Text = "Cantidad:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cerrar;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(29, 213);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 37);
            this.btnCancelar.TabIndex = 120;
            this.btnCancelar.Text = "         CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(167, 213);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(109, 37);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "           ASIGNAR";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cbxUniforme
            // 
            this.cbxUniforme.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUniforme.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUniforme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUniforme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.cbxUniforme.FormattingEnabled = true;
            this.cbxUniforme.Location = new System.Drawing.Point(22, 35);
            this.cbxUniforme.Name = "cbxUniforme";
            this.cbxUniforme.Size = new System.Drawing.Size(263, 23);
            this.cbxUniforme.TabIndex = 111;
            this.cbxUniforme.SelectedIndexChanged += new System.EventHandler(this.cbxUniforme_SelectedIndexChanged);
            this.cbxUniforme.DropDownClosed += new System.EventHandler(this.cbxUniforme_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label4.Location = new System.Drawing.Point(16, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 16);
            this.label4.TabIndex = 119;
            this.label4.Text = "Vida Útil (meses):";
            // 
            // txtVidaUtil
            // 
            this.txtVidaUtil.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.txtVidaUtil.Location = new System.Drawing.Point(135, 124);
            this.txtVidaUtil.Name = "txtVidaUtil";
            this.txtVidaUtil.Size = new System.Drawing.Size(150, 21);
            this.txtVidaUtil.TabIndex = 118;
            this.txtVidaUtil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVidaUtil_KeyPress);
            // 
            // txtTalla
            // 
            this.txtTalla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.txtTalla.Location = new System.Drawing.Point(135, 83);
            this.txtTalla.Name = "txtTalla";
            this.txtTalla.Size = new System.Drawing.Size(150, 21);
            this.txtTalla.TabIndex = 116;
            this.txtTalla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTalla_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label3.Location = new System.Drawing.Point(87, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 117;
            this.label3.Text = "Talla:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPuesto);
            this.groupBox1.Controls.Add(this.txtPersonal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox1.Location = new System.Drawing.Point(16, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 152);
            this.groupBox1.TabIndex = 123;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar Personal:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(19, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 122;
            this.label5.Text = "Puesto:";
            // 
            // txtPuesto
            // 
            this.txtPuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.txtPuesto.Location = new System.Drawing.Point(22, 108);
            this.txtPuesto.Name = "txtPuesto";
            this.txtPuesto.Size = new System.Drawing.Size(263, 21);
            this.txtPuesto.TabIndex = 121;
            this.txtPuesto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPuesto_KeyPress);
            // 
            // frmAsignarUniformes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmAsignarUniformes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Uniformes";
            this.Load += new System.EventHandler(this.frmAsignarUniformes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPersonalData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgPersonalData;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvExpressVista;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPersonal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxUniforme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVidaUtil;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTalla;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPuesto;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label6;
    }
}