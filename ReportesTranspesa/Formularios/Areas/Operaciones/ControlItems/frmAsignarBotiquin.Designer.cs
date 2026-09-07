namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    partial class frmAsignarBotiquin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarBotiquin));
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProgramacion = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFVencimiento = new System.Windows.Forms.DateTimePicker();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cbxItem = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvBotiquinUnidad = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsQuitarItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvBotiquinUnidadView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lstItems = new System.Windows.Forms.ListView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidad)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidadView)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(510, 37);
            this.label2.TabIndex = 20;
            this.label2.Text = "GESTIONAR BOTIQUÍN DE UNIDAD";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.txtObservacion);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblProgramacion);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.lblPlaca);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.dtpFVencimiento);
            this.panel3.Controls.Add(this.txtCantidad);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.cbxItem);
            this.panel3.Controls.Add(this.btnGuardar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(510, 182);
            this.panel3.TabIndex = 184;
            // 
            // txtObservacion
            // 
            this.txtObservacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtObservacion.Location = new System.Drawing.Point(116, 123);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(276, 39);
            this.txtObservacion.TabIndex = 234;
            this.txtObservacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservacion_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(32, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 233;
            this.label1.Text = "Observación:";
            // 
            // lblProgramacion
            // 
            this.lblProgramacion.AutoSize = true;
            this.lblProgramacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramacion.ForeColor = System.Drawing.Color.Red;
            this.lblProgramacion.Location = new System.Drawing.Point(291, 15);
            this.lblProgramacion.Name = "lblProgramacion";
            this.lblProgramacion.Size = new System.Drawing.Size(176, 18);
            this.lblProgramacion.TabIndex = 232;
            this.lblProgramacion.Text = "SIN PROGRAMACION";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(183, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(116, 18);
            this.label15.TabIndex = 231;
            this.label15.Text = "OPERACIÓN: ";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblPlaca.ForeColor = System.Drawing.Color.Red;
            this.lblPlaca.Location = new System.Drawing.Point(80, 15);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(73, 18);
            this.lblPlaca.TabIndex = 230;
            this.lblPlaca.Text = "T4G-963";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(17, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 18);
            this.label12.TabIndex = 229;
            this.label12.Text = "PLACA: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(19, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 228;
            this.label4.Text = "F. Vencimiento:";
            // 
            // dtpFVencimiento
            // 
            this.dtpFVencimiento.CustomFormat = "dd/MM/yyyy";
            this.dtpFVencimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpFVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFVencimiento.Location = new System.Drawing.Point(116, 87);
            this.dtpFVencimiento.Name = "dtpFVencimiento";
            this.dtpFVencimiento.Size = new System.Drawing.Size(101, 21);
            this.dtpFVencimiento.TabIndex = 227;
            this.dtpFVencimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFVencimiento_KeyPress);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidad.Location = new System.Drawing.Point(302, 87);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(90, 21);
            this.txtCantidad.TabIndex = 225;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(237, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 226;
            this.label3.Text = "Cantidad:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label25.Location = new System.Drawing.Point(19, 52);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(34, 15);
            this.label25.TabIndex = 224;
            this.label25.Text = "Ítem:";
            // 
            // cbxItem
            // 
            this.cbxItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxItem.FormattingEnabled = true;
            this.cbxItem.Location = new System.Drawing.Point(61, 48);
            this.cbxItem.Name = "cbxItem";
            this.cbxItem.Size = new System.Drawing.Size(331, 23);
            this.cbxItem.TabIndex = 223;
            this.cbxItem.SelectedIndexChanged += new System.EventHandler(this.cbxItem_SelectedIndexChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnGuardar.Location = new System.Drawing.Point(421, 73);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(63, 65);
            this.btnGuardar.TabIndex = 222;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dtgvBotiquinUnidad
            // 
            this.dtgvBotiquinUnidad.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgvBotiquinUnidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvBotiquinUnidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvBotiquinUnidad.Location = new System.Drawing.Point(0, 219);
            this.dtgvBotiquinUnidad.LookAndFeel.SkinMaskColor = System.Drawing.Color.Red;
            this.dtgvBotiquinUnidad.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Red;
            this.dtgvBotiquinUnidad.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgvBotiquinUnidad.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvBotiquinUnidad.MainView = this.dtgvBotiquinUnidadView;
            this.dtgvBotiquinUnidad.Name = "dtgvBotiquinUnidad";
            this.dtgvBotiquinUnidad.Size = new System.Drawing.Size(510, 239);
            this.dtgvBotiquinUnidad.TabIndex = 185;
            this.dtgvBotiquinUnidad.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvBotiquinUnidadView});
            this.dtgvBotiquinUnidad.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dtgvBotiquinUnidad_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsQuitarItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 26);
            // 
            // tsQuitarItem
            // 
            this.tsQuitarItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsQuitarItem.Name = "tsQuitarItem";
            this.tsQuitarItem.Size = new System.Drawing.Size(134, 22);
            this.tsQuitarItem.Text = "Quitar Ítem";
            this.tsQuitarItem.Click += new System.EventHandler(this.tsQuitarItem_Click);
            // 
            // dtgvBotiquinUnidadView
            // 
            this.dtgvBotiquinUnidadView.GridControl = this.dtgvBotiquinUnidad;
            this.dtgvBotiquinUnidadView.Name = "dtgvBotiquinUnidadView";
            this.dtgvBotiquinUnidadView.OptionsBehavior.Editable = false;
            this.dtgvBotiquinUnidadView.OptionsView.ColumnAutoWidth = false;
            this.dtgvBotiquinUnidadView.OptionsView.RowAutoHeight = true;
            this.dtgvBotiquinUnidadView.OptionsView.ShowGroupPanel = false;
            this.dtgvBotiquinUnidadView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dtgvBotiquinUnidadView_CustomDrawCell);
            // 
            // lstItems
            // 
            this.lstItems.BackColor = System.Drawing.Color.SeaShell;
            this.lstItems.ForeColor = System.Drawing.Color.Navy;
            this.lstItems.FullRowSelect = true;
            this.lstItems.GridLines = true;
            this.lstItems.Location = new System.Drawing.Point(74, 77);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(321, 127);
            this.lstItems.TabIndex = 223;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.View = System.Windows.Forms.View.Details;
            this.lstItems.Visible = false;
            // 
            // frmAsignarBotiquin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 458);
            this.Controls.Add(this.dtgvBotiquinUnidad);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstItems);
            this.Name = "frmAsignarBotiquin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASIGNAR BOTIQUÍN";
            this.Load += new System.EventHandler(this.frmAsignarBotiquin_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidad)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBotiquinUnidadView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraGrid.GridControl dtgvBotiquinUnidad;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvBotiquinUnidadView;
        private System.Windows.Forms.ListView lstItems;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsQuitarItem;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblProgramacion;
        public System.Windows.Forms.Label lblPlaca;
        public System.Windows.Forms.ComboBox cbxItem;
        public System.Windows.Forms.TextBox txtCantidad;
        public System.Windows.Forms.DateTimePicker dtpFVencimiento;
        public System.Windows.Forms.TextBox txtObservacion;
    }
}