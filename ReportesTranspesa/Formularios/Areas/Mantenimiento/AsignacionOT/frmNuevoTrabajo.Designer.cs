namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    partial class frmNuevoTrabajo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNuevoTrabajo));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtProyectoD = new System.Windows.Forms.TextBox();
            this.txtProyecto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCentroCostoD = new System.Windows.Forms.TextBox();
            this.txtCentroCosto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNroReq = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dtpFechaProgramada = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.lstRequerimiento = new System.Windows.Forms.ListView();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(825, 40);
            this.label1.TabIndex = 187;
            this.label1.Text = "INGRESO DE NUEVO TRABAJO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(297, 290);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 36);
            this.btnCancelar.TabIndex = 203;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(426, 290);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(98, 36);
            this.btnAgregar.TabIndex = 202;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtProyectoD);
            this.groupBox4.Controls.Add(this.txtProyecto);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtCentroCostoD);
            this.groupBox4.Controls.Add(this.txtCentroCosto);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtNroReq);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label41);
            this.groupBox4.Controls.Add(this.txtDescripcion);
            this.groupBox4.Controls.Add(this.dtpFechaProgramada);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.lstRequerimiento);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(19, 56);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(784, 213);
            this.groupBox4.TabIndex = 220;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DATOS DE REQUERIMIENTO: ";
            // 
            // txtProyectoD
            // 
            this.txtProyectoD.BackColor = System.Drawing.SystemColors.Control;
            this.txtProyectoD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtProyectoD.Location = new System.Drawing.Point(20, 167);
            this.txtProyectoD.Name = "txtProyectoD";
            this.txtProyectoD.ReadOnly = true;
            this.txtProyectoD.Size = new System.Drawing.Size(395, 22);
            this.txtProyectoD.TabIndex = 221;
            // 
            // txtProyecto
            // 
            this.txtProyecto.BackColor = System.Drawing.SystemColors.Control;
            this.txtProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtProyecto.Location = new System.Drawing.Point(90, 139);
            this.txtProyecto.Name = "txtProyecto";
            this.txtProyecto.ReadOnly = true;
            this.txtProyecto.Size = new System.Drawing.Size(115, 22);
            this.txtProyecto.TabIndex = 220;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label3.Location = new System.Drawing.Point(19, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 219;
            this.label3.Text = "Proyecto:";
            // 
            // txtCentroCostoD
            // 
            this.txtCentroCostoD.BackColor = System.Drawing.SystemColors.Control;
            this.txtCentroCostoD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtCentroCostoD.Location = new System.Drawing.Point(20, 99);
            this.txtCentroCostoD.Name = "txtCentroCostoD";
            this.txtCentroCostoD.ReadOnly = true;
            this.txtCentroCostoD.Size = new System.Drawing.Size(395, 22);
            this.txtCentroCostoD.TabIndex = 218;
            // 
            // txtCentroCosto
            // 
            this.txtCentroCosto.BackColor = System.Drawing.SystemColors.Control;
            this.txtCentroCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtCentroCosto.Location = new System.Drawing.Point(131, 71);
            this.txtCentroCosto.Name = "txtCentroCosto";
            this.txtCentroCosto.ReadOnly = true;
            this.txtCentroCosto.Size = new System.Drawing.Size(115, 22);
            this.txtCentroCosto.TabIndex = 217;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.Location = new System.Drawing.Point(18, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 216;
            this.label2.Text = "Centro de Costo:";
            // 
            // txtNroReq
            // 
            this.txtNroReq.BackColor = System.Drawing.SystemColors.Window;
            this.txtNroReq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtNroReq.Location = new System.Drawing.Point(171, 31);
            this.txtNroReq.Name = "txtNroReq";
            this.txtNroReq.Size = new System.Drawing.Size(149, 22);
            this.txtNroReq.TabIndex = 215;
            this.txtNroReq.Enter += new System.EventHandler(this.txtNroReq_Enter);
            this.txtNroReq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNroReq_KeyPress);
            this.txtNroReq.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNroReq_KeyUp);
            this.txtNroReq.Leave += new System.EventHandler(this.txtNroReq_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label6.Location = new System.Drawing.Point(18, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 16);
            this.label6.TabIndex = 213;
            this.label6.Text = "Ingrese Requerimiento:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label41.Location = new System.Drawing.Point(440, 34);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(83, 16);
            this.label41.TabIndex = 211;
            this.label41.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(443, 58);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(319, 88);
            this.txtDescripcion.TabIndex = 210;
            // 
            // dtpFechaProgramada
            // 
            this.dtpFechaProgramada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.dtpFechaProgramada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaProgramada.Location = new System.Drawing.Point(574, 167);
            this.dtpFechaProgramada.Name = "dtpFechaProgramada";
            this.dtpFechaProgramada.Size = new System.Drawing.Size(111, 22);
            this.dtpFechaProgramada.TabIndex = 209;
            this.dtpFechaProgramada.Value = new System.DateTime(2023, 5, 27, 11, 36, 20, 0);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label18.Location = new System.Drawing.Point(440, 170);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 16);
            this.label18.TabIndex = 208;
            this.label18.Text = "Fecha Programada:";
            // 
            // lstRequerimiento
            // 
            this.lstRequerimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstRequerimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRequerimiento.ForeColor = System.Drawing.Color.Navy;
            this.lstRequerimiento.FullRowSelect = true;
            this.lstRequerimiento.GridLines = true;
            this.lstRequerimiento.Location = new System.Drawing.Point(20, 52);
            this.lstRequerimiento.MultiSelect = false;
            this.lstRequerimiento.Name = "lstRequerimiento";
            this.lstRequerimiento.Size = new System.Drawing.Size(395, 137);
            this.lstRequerimiento.TabIndex = 214;
            this.lstRequerimiento.UseCompatibleStateImageBehavior = false;
            this.lstRequerimiento.View = System.Windows.Forms.View.Details;
            this.lstRequerimiento.Visible = false;
            this.lstRequerimiento.Enter += new System.EventHandler(this.lstRequerimiento_Enter);
            this.lstRequerimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRequerimiento_KeyPress);
            this.lstRequerimiento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRequerimiento_MouseDoubleClick);
            // 
            // frmNuevoTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(825, 346);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Name = "frmNuevoTrabajo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUEVO TRABAJO";
            this.Load += new System.EventHandler(this.frmNuevoTrabajo_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lstRequerimiento;
        public System.Windows.Forms.DateTimePicker dtpFechaProgramada;
        public System.Windows.Forms.TextBox txtNroReq;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCentroCosto;
        public System.Windows.Forms.TextBox txtCentroCostoD;
        public System.Windows.Forms.TextBox txtProyectoD;
        public System.Windows.Forms.TextBox txtProyecto;
        private System.Windows.Forms.Label label3;
    }
}