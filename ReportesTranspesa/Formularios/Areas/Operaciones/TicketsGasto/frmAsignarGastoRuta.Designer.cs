namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmAsignarGastoRuta
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
            this.pAsignarRuta = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lvRuta = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDias = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.txtRutasActivas = new System.Windows.Forms.TextBox();
            this.cbxOperaciones = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGasto = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pAsignarRuta.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pAsignarRuta
            // 
            this.pAsignarRuta.BackColor = System.Drawing.Color.LemonChiffon;
            this.pAsignarRuta.Controls.Add(this.btnAgregar);
            this.pAsignarRuta.Controls.Add(this.btnCancelar);
            this.pAsignarRuta.Controls.Add(this.lvRuta);
            this.pAsignarRuta.Controls.Add(this.groupBox2);
            this.pAsignarRuta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pAsignarRuta.Location = new System.Drawing.Point(0, 41);
            this.pAsignarRuta.Name = "pAsignarRuta";
            this.pAsignarRuta.Size = new System.Drawing.Size(533, 233);
            this.pAsignarRuta.TabIndex = 102;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAgregar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnAgregar.Location = new System.Drawing.Point(279, 174);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(114, 42);
            this.btnAgregar.TabIndex = 42;
            this.btnAgregar.Text = " Asignar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancelar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(139, 174);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(114, 42);
            this.btnCancelar.TabIndex = 43;
            this.btnCancelar.Text = " Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lvRuta
            // 
            this.lvRuta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lvRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvRuta.ForeColor = System.Drawing.Color.Navy;
            this.lvRuta.FullRowSelect = true;
            this.lvRuta.GridLines = true;
            this.lvRuta.Location = new System.Drawing.Point(145, 104);
            this.lvRuta.MultiSelect = false;
            this.lvRuta.Name = "lvRuta";
            this.lvRuta.Size = new System.Drawing.Size(348, 10);
            this.lvRuta.TabIndex = 118;
            this.lvRuta.UseCompatibleStateImageBehavior = false;
            this.lvRuta.View = System.Windows.Forms.View.Details;
            this.lvRuta.Visible = false;
            this.lvRuta.Enter += new System.EventHandler(this.lvRuta_Enter);
            this.lvRuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvRuta_KeyPress);
            this.lvRuta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvRuta_KeyUp);
            this.lvRuta.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRuta_MouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox2.Controls.Add(this.txtDias);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnSeleccionar);
            this.groupBox2.Controls.Add(this.txtRutasActivas);
            this.groupBox2.Controls.Add(this.cbxOperaciones);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtGasto);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(22, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 140);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ingrese información de ruta:";
            // 
            // txtDias
            // 
            this.txtDias.Location = new System.Drawing.Point(124, 104);
            this.txtDias.Name = "txtDias";
            this.txtDias.Size = new System.Drawing.Size(100, 22);
            this.txtDias.TabIndex = 102;
            this.txtDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDias_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 103;
            this.label1.Text = "Duración (Días):";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(442, 104);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(29, 22);
            this.btnSeleccionar.TabIndex = 101;
            this.btnSeleccionar.Text = "...";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // txtRutasActivas
            // 
            this.txtRutasActivas.Location = new System.Drawing.Point(123, 67);
            this.txtRutasActivas.Name = "txtRutasActivas";
            this.txtRutasActivas.Size = new System.Drawing.Size(348, 22);
            this.txtRutasActivas.TabIndex = 44;
            this.txtRutasActivas.Enter += new System.EventHandler(this.txtRutasActivas_Enter);
            this.txtRutasActivas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRutasActivas_KeyPress);
            this.txtRutasActivas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRutasActivas_KeyUp);
            this.txtRutasActivas.Leave += new System.EventHandler(this.txtRutasActivas_Leave);
            // 
            // cbxOperaciones
            // 
            this.cbxOperaciones.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperaciones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperaciones.FormattingEnabled = true;
            this.cbxOperaciones.Location = new System.Drawing.Point(123, 29);
            this.cbxOperaciones.Name = "cbxOperaciones";
            this.cbxOperaciones.Size = new System.Drawing.Size(164, 24);
            this.cbxOperaciones.TabIndex = 43;
            this.cbxOperaciones.SelectedIndexChanged += new System.EventHandler(this.cbxOperaciones_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(43, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 16);
            this.label13.TabIndex = 40;
            this.label13.Text = "Operación:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(78, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "Ruta:";
            // 
            // txtGasto
            // 
            this.txtGasto.Location = new System.Drawing.Point(336, 104);
            this.txtGasto.Name = "txtGasto";
            this.txtGasto.ReadOnly = true;
            this.txtGasto.Size = new System.Drawing.Size(100, 22);
            this.txtGasto.TabIndex = 5;
            this.txtGasto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGasto_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(283, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 16);
            this.label18.TabIndex = 29;
            this.label18.Text = "Gasto:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkRed;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(533, 41);
            this.label2.TabIndex = 47;
            this.label2.Text = "ASIGNAR GASTO A RUTA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAsignarGastoRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 274);
            this.Controls.Add(this.pAsignarRuta);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAsignarGastoRuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Gasto de Ruta";
            this.Load += new System.EventHandler(this.frmAsignarGastoRuta_Load);
            this.Shown += new System.EventHandler(this.frmAsignarGastoRuta_Shown);
            this.pAsignarRuta.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pAsignarRuta;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvRuta;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtGasto;
        public System.Windows.Forms.ComboBox cbxOperaciones;
        public System.Windows.Forms.TextBox txtRutasActivas;
        public System.Windows.Forms.TextBox txtDias;
    }
}