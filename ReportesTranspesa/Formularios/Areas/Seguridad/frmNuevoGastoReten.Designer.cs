namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmNuevoGastoReten
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
            this.txtPersona = new System.Windows.Forms.Panel();
            this.lstDestino = new System.Windows.Forms.ListView();
            this.lstPersona = new System.Windows.Forms.ListView();
            this.gNumeroPlantilla = new System.Windows.Forms.GroupBox();
            this.txtNroPlantilla = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gObservacion = new System.Windows.Forms.GroupBox();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.gDestino = new System.Windows.Forms.GroupBox();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.gImporte = new System.Windows.Forms.GroupBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.gNombrePersona = new System.Windows.Forms.GroupBox();
            this.txtNombrePersona = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gFechaRegistra = new System.Windows.Forms.GroupBox();
            this.dtpNuevaFecha = new System.Windows.Forms.DateTimePicker();
            this.txtPersona.SuspendLayout();
            this.gNumeroPlantilla.SuspendLayout();
            this.gObservacion.SuspendLayout();
            this.gDestino.SuspendLayout();
            this.gImporte.SuspendLayout();
            this.gNombrePersona.SuspendLayout();
            this.gFechaRegistra.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPersona
            // 
            this.txtPersona.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtPersona.Controls.Add(this.gFechaRegistra);
            this.txtPersona.Controls.Add(this.lstDestino);
            this.txtPersona.Controls.Add(this.lstPersona);
            this.txtPersona.Controls.Add(this.gNumeroPlantilla);
            this.txtPersona.Controls.Add(this.btnGuardar);
            this.txtPersona.Controls.Add(this.gObservacion);
            this.txtPersona.Controls.Add(this.gDestino);
            this.txtPersona.Controls.Add(this.gImporte);
            this.txtPersona.Controls.Add(this.gNombrePersona);
            this.txtPersona.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPersona.Location = new System.Drawing.Point(0, 47);
            this.txtPersona.Name = "txtPersona";
            this.txtPersona.Size = new System.Drawing.Size(560, 313);
            this.txtPersona.TabIndex = 4;
            // 
            // lstDestino
            // 
            this.lstDestino.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDestino.ForeColor = System.Drawing.Color.Navy;
            this.lstDestino.FullRowSelect = true;
            this.lstDestino.GridLines = true;
            this.lstDestino.Location = new System.Drawing.Point(178, 137);
            this.lstDestino.MultiSelect = false;
            this.lstDestino.Name = "lstDestino";
            this.lstDestino.Size = new System.Drawing.Size(317, 10);
            this.lstDestino.TabIndex = 92;
            this.lstDestino.UseCompatibleStateImageBehavior = false;
            this.lstDestino.View = System.Windows.Forms.View.Details;
            this.lstDestino.Visible = false;
            this.lstDestino.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstDestino_KeyPress);
            this.lstDestino.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstDestino_KeyUp);
            // 
            // lstPersona
            // 
            this.lstPersona.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstPersona.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPersona.ForeColor = System.Drawing.Color.Navy;
            this.lstPersona.FullRowSelect = true;
            this.lstPersona.GridLines = true;
            this.lstPersona.Location = new System.Drawing.Point(16, 64);
            this.lstPersona.MultiSelect = false;
            this.lstPersona.Name = "lstPersona";
            this.lstPersona.Size = new System.Drawing.Size(231, 10);
            this.lstPersona.TabIndex = 91;
            this.lstPersona.UseCompatibleStateImageBehavior = false;
            this.lstPersona.View = System.Windows.Forms.View.Details;
            this.lstPersona.Visible = false;
            this.lstPersona.Enter += new System.EventHandler(this.lstPersona_Enter);
            this.lstPersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPersona_KeyPress);
            this.lstPersona.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstPersona_KeyUp);
            // 
            // gNumeroPlantilla
            // 
            this.gNumeroPlantilla.Controls.Add(this.txtNroPlantilla);
            this.gNumeroPlantilla.Location = new System.Drawing.Point(252, 20);
            this.gNumeroPlantilla.Name = "gNumeroPlantilla";
            this.gNumeroPlantilla.Size = new System.Drawing.Size(115, 57);
            this.gNumeroPlantilla.TabIndex = 756;
            this.gNumeroPlantilla.TabStop = false;
            this.gNumeroPlantilla.Text = "Nro Plantilla";
            // 
            // txtNroPlantilla
            // 
            this.txtNroPlantilla.Location = new System.Drawing.Point(6, 24);
            this.txtNroPlantilla.Name = "txtNroPlantilla";
            this.txtNroPlantilla.Size = new System.Drawing.Size(99, 20);
            this.txtNroPlantilla.TabIndex = 1;
            this.txtNroPlantilla.Enter += new System.EventHandler(this.txtNroPlantilla_Enter);
            this.txtNroPlantilla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNroPlantilla_KeyPress);
            this.txtNroPlantilla.Leave += new System.EventHandler(this.txtNroPlantilla_Leave);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.Location = new System.Drawing.Point(172, 245);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 56);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.button1_Click);
            // 
            // gObservacion
            // 
            this.gObservacion.Controls.Add(this.txtObservacion);
            this.gObservacion.Location = new System.Drawing.Point(12, 156);
            this.gObservacion.Name = "gObservacion";
            this.gObservacion.Size = new System.Drawing.Size(483, 72);
            this.gObservacion.TabIndex = 75;
            this.gObservacion.TabStop = false;
            this.gObservacion.Text = "Observacion";
            // 
            // txtObservacion
            // 
            this.txtObservacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacion.Location = new System.Drawing.Point(11, 21);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(466, 39);
            this.txtObservacion.TabIndex = 5;
            this.txtObservacion.Enter += new System.EventHandler(this.txtObservacion_Enter);
            this.txtObservacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservacion_KeyPress);
            this.txtObservacion.Leave += new System.EventHandler(this.txtObservacion_Leave);
            // 
            // gDestino
            // 
            this.gDestino.Controls.Add(this.txtDestino);
            this.gDestino.Location = new System.Drawing.Point(172, 93);
            this.gDestino.Name = "gDestino";
            this.gDestino.Size = new System.Drawing.Size(329, 57);
            this.gDestino.TabIndex = 78;
            this.gDestino.TabStop = false;
            this.gDestino.Text = "Destino";
            // 
            // txtDestino
            // 
            this.txtDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDestino.Location = new System.Drawing.Point(6, 24);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(317, 20);
            this.txtDestino.TabIndex = 4;
            this.txtDestino.Enter += new System.EventHandler(this.txtDestino_Enter);
            this.txtDestino.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDestino_KeyPress);
            this.txtDestino.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDestino_KeyUp);
            this.txtDestino.Leave += new System.EventHandler(this.txtDestino_Leave);
            // 
            // gImporte
            // 
            this.gImporte.Controls.Add(this.txtImporte);
            this.gImporte.Location = new System.Drawing.Point(12, 93);
            this.gImporte.Name = "gImporte";
            this.gImporte.Size = new System.Drawing.Size(145, 57);
            this.gImporte.TabIndex = 55;
            this.gImporte.TabStop = false;
            this.gImporte.Text = "Importe";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(11, 24);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(123, 20);
            this.txtImporte.TabIndex = 3;
            this.txtImporte.Enter += new System.EventHandler(this.txtImporte_Enter);
            this.txtImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporte_KeyPress);
            this.txtImporte.Leave += new System.EventHandler(this.txtImporte_Leave);
            // 
            // gNombrePersona
            // 
            this.gNombrePersona.Controls.Add(this.txtNombrePersona);
            this.gNombrePersona.Location = new System.Drawing.Point(4, 20);
            this.gNombrePersona.Name = "gNombrePersona";
            this.gNombrePersona.Size = new System.Drawing.Size(242, 57);
            this.gNombrePersona.TabIndex = 0;
            this.gNombrePersona.TabStop = false;
            this.gNombrePersona.Text = "Nombre Persona";
            // 
            // txtNombrePersona
            // 
            this.txtNombrePersona.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombrePersona.Location = new System.Drawing.Point(12, 24);
            this.txtNombrePersona.Name = "txtNombrePersona";
            this.txtNombrePersona.Size = new System.Drawing.Size(206, 20);
            this.txtNombrePersona.TabIndex = 0;
            this.txtNombrePersona.Enter += new System.EventHandler(this.txtNombrePersona_Enter);
            this.txtNombrePersona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombrePersona_KeyPress);
            this.txtNombrePersona.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNombrePersona_KeyUp);
            this.txtNombrePersona.Leave += new System.EventHandler(this.txtNombrePersona_Leave);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(560, 47);
            this.label1.TabIndex = 3;
            this.label1.Text = "NUEVO GASTO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gFechaRegistra
            // 
            this.gFechaRegistra.Controls.Add(this.dtpNuevaFecha);
            this.gFechaRegistra.Location = new System.Drawing.Point(379, 20);
            this.gFechaRegistra.Name = "gFechaRegistra";
            this.gFechaRegistra.Size = new System.Drawing.Size(122, 57);
            this.gFechaRegistra.TabIndex = 757;
            this.gFechaRegistra.TabStop = false;
            this.gFechaRegistra.Text = "Fecha Registro";
            // 
            // dtpNuevaFecha
            // 
            this.dtpNuevaFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNuevaFecha.Location = new System.Drawing.Point(7, 23);
            this.dtpNuevaFecha.Name = "dtpNuevaFecha";
            this.dtpNuevaFecha.Size = new System.Drawing.Size(109, 20);
            this.dtpNuevaFecha.TabIndex = 0;
            // 
            // frmNuevoGastoReten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 360);
            this.Controls.Add(this.txtPersona);
            this.Controls.Add(this.label1);
            this.Name = "frmNuevoGastoReten";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNuevoGastoReten";
            this.Load += new System.EventHandler(this.frmNuevoGastoReten_Load);
            this.txtPersona.ResumeLayout(false);
            this.gNumeroPlantilla.ResumeLayout(false);
            this.gNumeroPlantilla.PerformLayout();
            this.gObservacion.ResumeLayout(false);
            this.gObservacion.PerformLayout();
            this.gDestino.ResumeLayout(false);
            this.gDestino.PerformLayout();
            this.gImporte.ResumeLayout(false);
            this.gImporte.PerformLayout();
            this.gNombrePersona.ResumeLayout(false);
            this.gNombrePersona.PerformLayout();
            this.gFechaRegistra.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel txtPersona;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gObservacion;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.GroupBox gDestino;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.GroupBox gImporte;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.GroupBox gNombrePersona;
        private System.Windows.Forms.TextBox txtNombrePersona;
        private System.Windows.Forms.GroupBox gNumeroPlantilla;
        private System.Windows.Forms.TextBox txtNroPlantilla;
        private System.Windows.Forms.ListView lstDestino;
        private System.Windows.Forms.ListView lstPersona;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gFechaRegistra;
        private System.Windows.Forms.DateTimePicker dtpNuevaFecha;
    }
}