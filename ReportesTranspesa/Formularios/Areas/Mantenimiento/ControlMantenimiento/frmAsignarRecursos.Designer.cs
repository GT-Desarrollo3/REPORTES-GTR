namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    partial class frmAsignarRecursos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarRecursos));
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAccesorio = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lstItemsAlmacen = new System.Windows.Forms.ListView();
            this.dtgRecursos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminarRecurso = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvRecursosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgRecursosMaquina = new DevExpress.XtraGrid.GridControl();
            this.dgvRecursosMaquinaVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigoItem = new System.Windows.Forms.TextBox();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecursos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecursosVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecursosMaquina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecursosMaquinaVista)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(714, 40);
            this.label1.TabIndex = 14;
            this.label1.Text = "ASIGNAR RECURSOS PARA ACCESORIOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.btnCancelar);
            this.panel4.Controls.Add(this.btnAgregar);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.lblAccesorio);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 40);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(714, 194);
            this.panel4.TabIndex = 15;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(589, 59);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(101, 35);
            this.btnCancelar.TabIndex = 215;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTip = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAgregar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAgregar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Appearance.Options.UseBackColor = true;
            this.btnAgregar.Appearance.Options.UseBorderColor = true;
            this.btnAgregar.Appearance.Options.UseFont = true;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(589, 107);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(101, 35);
            this.btnAgregar.TabIndex = 214;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.ToolTip = "Guardar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodigoItem);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCantidad);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(21, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(544, 124);
            this.groupBox2.TabIndex = 212;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Materiales:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCantidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtCantidad.Location = new System.Drawing.Point(442, 79);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(78, 20);
            this.txtCantidad.TabIndex = 225;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 224;
            this.label2.Text = "Cantidad:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtDescripcion.Location = new System.Drawing.Point(92, 59);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(321, 48);
            this.txtDescripcion.TabIndex = 223;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 220;
            this.label7.Text = "Descripción:";
            // 
            // lblAccesorio
            // 
            this.lblAccesorio.AutoSize = true;
            this.lblAccesorio.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold);
            this.lblAccesorio.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblAccesorio.Location = new System.Drawing.Point(139, 15);
            this.lblAccesorio.Name = "lblAccesorio";
            this.lblAccesorio.Size = new System.Drawing.Size(24, 26);
            this.lblAccesorio.TabIndex = 111;
            this.lblAccesorio.Text = "F";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(17, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 21);
            this.label6.TabIndex = 110;
            this.label6.Text = "ACCESORIO:";
            // 
            // lstItemsAlmacen
            // 
            this.lstItemsAlmacen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstItemsAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItemsAlmacen.ForeColor = System.Drawing.Color.Navy;
            this.lstItemsAlmacen.FullRowSelect = true;
            this.lstItemsAlmacen.GridLines = true;
            this.lstItemsAlmacen.Location = new System.Drawing.Point(113, 137);
            this.lstItemsAlmacen.MultiSelect = false;
            this.lstItemsAlmacen.Name = "lstItemsAlmacen";
            this.lstItemsAlmacen.Size = new System.Drawing.Size(428, 129);
            this.lstItemsAlmacen.TabIndex = 224;
            this.lstItemsAlmacen.UseCompatibleStateImageBehavior = false;
            this.lstItemsAlmacen.View = System.Windows.Forms.View.Details;
            this.lstItemsAlmacen.Visible = false;
            this.lstItemsAlmacen.Enter += new System.EventHandler(this.lstItemsAlmacen_Enter);
            this.lstItemsAlmacen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstItemsAlmacen_KeyPress);
            this.lstItemsAlmacen.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItemsAlmacen_MouseDoubleClick);
            // 
            // dtgRecursos
            // 
            this.dtgRecursos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgRecursos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRecursos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRecursos.Location = new System.Drawing.Point(0, 234);
            this.dtgRecursos.MainView = this.dgvRecursosVista;
            this.dtgRecursos.Name = "dtgRecursos";
            this.dtgRecursos.Size = new System.Drawing.Size(714, 261);
            this.dtgRecursos.TabIndex = 225;
            this.dtgRecursos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRecursosVista});
            this.dtgRecursos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgRecursos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminarRecurso});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // tsEliminarRecurso
            // 
            this.tsEliminarRecurso.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminarRecurso.Name = "tsEliminarRecurso";
            this.tsEliminarRecurso.Size = new System.Drawing.Size(117, 22);
            this.tsEliminarRecurso.Text = "Eliminar";
            this.tsEliminarRecurso.Click += new System.EventHandler(this.tsEliminarRecurso_Click);
            // 
            // dgvRecursosVista
            // 
            this.dgvRecursosVista.GridControl = this.dtgRecursos;
            this.dgvRecursosVista.Name = "dgvRecursosVista";
            this.dgvRecursosVista.OptionsBehavior.Editable = false;
            this.dgvRecursosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvRecursosVista.OptionsView.RowAutoHeight = true;
            this.dgvRecursosVista.OptionsView.ShowGroupPanel = false;
            // 
            // dtgRecursosMaquina
            // 
            this.dtgRecursosMaquina.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgRecursosMaquina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgRecursosMaquina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgRecursosMaquina.Location = new System.Drawing.Point(0, 234);
            this.dtgRecursosMaquina.MainView = this.dgvRecursosMaquinaVista;
            this.dtgRecursosMaquina.Name = "dtgRecursosMaquina";
            this.dtgRecursosMaquina.Size = new System.Drawing.Size(714, 261);
            this.dtgRecursosMaquina.TabIndex = 226;
            this.dtgRecursosMaquina.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvRecursosMaquinaVista});
            // 
            // dgvRecursosMaquinaVista
            // 
            this.dgvRecursosMaquinaVista.GridControl = this.dtgRecursosMaquina;
            this.dgvRecursosMaquinaVista.Name = "dgvRecursosMaquinaVista";
            this.dgvRecursosMaquinaVista.OptionsBehavior.Editable = false;
            this.dgvRecursosMaquinaVista.OptionsView.ColumnAutoWidth = false;
            this.dgvRecursosMaquinaVista.OptionsView.RowAutoHeight = true;
            this.dgvRecursosMaquinaVista.OptionsView.ShowGroupPanel = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 226;
            this.label3.Text = "Ingresar Item:";
            // 
            // txtCodigoItem
            // 
            this.txtCodigoItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigoItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.txtCodigoItem.Location = new System.Drawing.Point(92, 25);
            this.txtCodigoItem.Name = "txtCodigoItem";
            this.txtCodigoItem.Size = new System.Drawing.Size(212, 20);
            this.txtCodigoItem.TabIndex = 221;
            this.txtCodigoItem.Enter += new System.EventHandler(this.txtCodigoItem_Enter);
            this.txtCodigoItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoItem_KeyPress);
            this.txtCodigoItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoItem_KeyUp);
            this.txtCodigoItem.Leave += new System.EventHandler(this.txtCodigoItem_Leave);
            // 
            // frmAsignarRecursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 495);
            this.Controls.Add(this.dtgRecursosMaquina);
            this.Controls.Add(this.dtgRecursos);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstItemsAlmacen);
            this.Name = "frmAsignarRecursos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Recursos";
            this.Load += new System.EventHandler(this.frmAsignarRecursos_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecursos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecursosVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecursosMaquina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecursosMaquinaVista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Label lblAccesorio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.ListView lstItemsAlmacen;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtCantidad;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRecursosVista;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminarRecurso;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvRecursosMaquinaVista;
        public DevExpress.XtraGrid.GridControl dtgRecursosMaquina;
        public DevExpress.XtraGrid.GridControl dtgRecursos;
        internal System.Windows.Forms.TextBox txtCodigoItem;
        private System.Windows.Forms.Label label3;
    }
}