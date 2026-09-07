namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    partial class AsistenciaExterna
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsistenciaExterna));
            this.lblTituloGuia = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtPersona = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.rbtIngreso = new System.Windows.Forms.RadioButton();
            this.rbtSalida = new System.Windows.Forms.RadioButton();
            this.btnRegistrar = new DevExpress.XtraEditors.SimpleButton();
            this.cbxCompania = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtIngresoDescanso = new System.Windows.Forms.RadioButton();
            this.rbtTerminoDescanso = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
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
            this.lblTituloGuia.Size = new System.Drawing.Size(438, 58);
            this.lblTituloGuia.TabIndex = 1;
            this.lblTituloGuia.Text = "REGISTRAR ASISTENCIA";
            this.lblTituloGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblFecha.Location = new System.Drawing.Point(59, 95);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(282, 34);
            this.lblFecha.TabIndex = 88;
            this.lblFecha.Text = "28-05-2024 09:08:01";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "PERSONAL:";
            // 
            // txtPersona
            // 
            this.txtPersona.Location = new System.Drawing.Point(135, 293);
            this.txtPersona.Name = "txtPersona";
            this.txtPersona.ReadOnly = true;
            this.txtPersona.Size = new System.Drawing.Size(243, 20);
            this.txtPersona.TabIndex = 90;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 91;
            this.label2.Text = "USUARIO:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(136, 255);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(111, 20);
            this.txtUsuario.TabIndex = 92;
            this.txtUsuario.TextChanged += new System.EventHandler(this.txtUsuario_TextChanged);
            // 
            // rbtIngreso
            // 
            this.rbtIngreso.AutoSize = true;
            this.rbtIngreso.Checked = true;
            this.rbtIngreso.Location = new System.Drawing.Point(47, 199);
            this.rbtIngreso.Name = "rbtIngreso";
            this.rbtIngreso.Size = new System.Drawing.Size(60, 17);
            this.rbtIngreso.TabIndex = 93;
            this.rbtIngreso.Text = "Ingreso";
            this.rbtIngreso.UseVisualStyleBackColor = true;
            // 
            // rbtSalida
            // 
            this.rbtSalida.AutoSize = true;
            this.rbtSalida.Location = new System.Drawing.Point(116, 200);
            this.rbtSalida.Name = "rbtSalida";
            this.rbtSalida.Size = new System.Drawing.Size(54, 17);
            this.rbtSalida.TabIndex = 94;
            this.rbtSalida.Text = "Salida";
            this.rbtSalida.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.Image")));
            this.btnRegistrar.Location = new System.Drawing.Point(143, 347);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(113, 53);
            this.btnRegistrar.TabIndex = 95;
            this.btnRegistrar.Text = "REGISTRAR";
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // cbxCompania
            // 
            this.cbxCompania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCompania.FormattingEnabled = true;
            this.cbxCompania.Location = new System.Drawing.Point(136, 146);
            this.cbxCompania.Name = "cbxCompania";
            this.cbxCompania.Size = new System.Drawing.Size(211, 21);
            this.cbxCompania.TabIndex = 96;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "COMPAÑIA:";
            // 
            // rbtIngresoDescanso
            // 
            this.rbtIngresoDescanso.AutoSize = true;
            this.rbtIngresoDescanso.Location = new System.Drawing.Point(201, 200);
            this.rbtIngresoDescanso.Name = "rbtIngresoDescanso";
            this.rbtIngresoDescanso.Size = new System.Drawing.Size(111, 17);
            this.rbtIngresoDescanso.TabIndex = 98;
            this.rbtIngresoDescanso.Text = "Ingreso Descanso";
            this.rbtIngresoDescanso.UseVisualStyleBackColor = true;
            // 
            // rbtTerminoDescanso
            // 
            this.rbtTerminoDescanso.AutoSize = true;
            this.rbtTerminoDescanso.Location = new System.Drawing.Point(317, 200);
            this.rbtTerminoDescanso.Name = "rbtTerminoDescanso";
            this.rbtTerminoDescanso.Size = new System.Drawing.Size(90, 17);
            this.rbtTerminoDescanso.TabIndex = 99;
            this.rbtTerminoDescanso.Text = "Fin Descanso";
            this.rbtTerminoDescanso.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(9, 13);
            this.label4.TabIndex = 100;
            this.label4.Text = "|";
            // 
            // AsistenciaExterna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(438, 467);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rbtTerminoDescanso);
            this.Controls.Add(this.rbtIngresoDescanso);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxCompania);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.rbtSalida);
            this.Controls.Add(this.rbtIngreso);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPersona);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblTituloGuia);
            this.Name = "AsistenciaExterna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AsistenciaExterna";
            this.Load += new System.EventHandler(this.AsistenciaExterna_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTituloGuia;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPersona;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.RadioButton rbtIngreso;
        private System.Windows.Forms.RadioButton rbtSalida;
        private DevExpress.XtraEditors.SimpleButton btnRegistrar;
        private System.Windows.Forms.ComboBox cbxCompania;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtIngresoDescanso;
        private System.Windows.Forms.RadioButton rbtTerminoDescanso;
        private System.Windows.Forms.Label label4;
    }
}