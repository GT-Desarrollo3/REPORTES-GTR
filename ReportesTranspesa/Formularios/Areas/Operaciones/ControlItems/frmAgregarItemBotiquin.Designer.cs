namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    partial class frmAgregarItemBotiquin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgregarItemBotiquin));
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtCodigoItem = new System.Windows.Forms.TextBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvListaBotiquin = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvListaBotiquinView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lstItems = new System.Windows.Forms.ListView();
            this.rbQuemadura = new System.Windows.Forms.RadioButton();
            this.rbMTC = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaBotiquin)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaBotiquinView)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(709, 37);
            this.label2.TabIndex = 19;
            this.label2.Text = "ÍTEMS DE BOTIQUÍN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtCantidad);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtDescripcion);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.btnGuardar);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.rbQuemadura);
            this.panel3.Controls.Add(this.rbMTC);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtDuracion);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(709, 172);
            this.panel3.TabIndex = 183;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(387, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 30);
            this.label5.TabIndex = 227;
            this.label5.Text = "Ingresar 0 si no\r\ntiene F. Vencimiento\r\n";
            // 
            // txtDuracion
            // 
            this.txtDuracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDuracion.Location = new System.Drawing.Point(300, 58);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(79, 21);
            this.txtDuracion.TabIndex = 225;
            this.txtDuracion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuracion_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(198, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 226;
            this.label4.Text = "Duración (Días):";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidad.Location = new System.Drawing.Point(82, 58);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(84, 21);
            this.txtCantidad.TabIndex = 223;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(17, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 224;
            this.label3.Text = "Cantidad:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtDescripcion.Location = new System.Drawing.Point(57, 19);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(210, 21);
            this.txtDescripcion.TabIndex = 203;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 220;
            this.label1.Text = "Ítem:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.txtCodigoItem);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBox1.Location = new System.Drawing.Point(17, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(546, 65);
            this.groupBox1.TabIndex = 219;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Código de Almacén";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCodigo.Location = new System.Drawing.Point(17, 27);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(114, 21);
            this.txtCodigo.TabIndex = 224;
            // 
            // txtCodigoItem
            // 
            this.txtCodigoItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCodigoItem.Location = new System.Drawing.Point(145, 27);
            this.txtCodigoItem.Name = "txtCodigoItem";
            this.txtCodigoItem.Size = new System.Drawing.Size(383, 21);
            this.txtCodigoItem.TabIndex = 204;
            this.txtCodigoItem.Enter += new System.EventHandler(this.txtCodigoItem_Enter);
            this.txtCodigoItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoItem_KeyPress);
            this.txtCodigoItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoItem_KeyUp);
            this.txtCodigoItem.Leave += new System.EventHandler(this.txtCodigoItem_Leave);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(582, 110);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(101, 40);
            this.btnGuardar.TabIndex = 222;
            this.btnGuardar.Text = "Agregar";
            this.btnGuardar.ToolTip = "Agregar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dtgvListaBotiquin
            // 
            this.dtgvListaBotiquin.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgvListaBotiquin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvListaBotiquin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvListaBotiquin.Location = new System.Drawing.Point(0, 209);
            this.dtgvListaBotiquin.LookAndFeel.SkinMaskColor = System.Drawing.Color.Red;
            this.dtgvListaBotiquin.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Red;
            this.dtgvListaBotiquin.LookAndFeel.SkinName = "Office 2010 Silver";
            this.dtgvListaBotiquin.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvListaBotiquin.MainView = this.dtgvListaBotiquinView;
            this.dtgvListaBotiquin.Name = "dtgvListaBotiquin";
            this.dtgvListaBotiquin.Size = new System.Drawing.Size(709, 245);
            this.dtgvListaBotiquin.TabIndex = 184;
            this.dtgvListaBotiquin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvListaBotiquinView});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEliminar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // tsEliminar
            // 
            this.tsEliminar.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.tsEliminar.Name = "tsEliminar";
            this.tsEliminar.Size = new System.Drawing.Size(117, 22);
            this.tsEliminar.Text = "Eliminar";
            this.tsEliminar.Click += new System.EventHandler(this.tsEliminar_Click);
            // 
            // dtgvListaBotiquinView
            // 
            this.dtgvListaBotiquinView.GridControl = this.dtgvListaBotiquin;
            this.dtgvListaBotiquinView.Name = "dtgvListaBotiquinView";
            this.dtgvListaBotiquinView.OptionsBehavior.Editable = false;
            this.dtgvListaBotiquinView.OptionsView.ColumnAutoWidth = false;
            this.dtgvListaBotiquinView.OptionsView.RowAutoHeight = true;
            this.dtgvListaBotiquinView.OptionsView.ShowFooter = true;
            this.dtgvListaBotiquinView.OptionsView.ShowGroupPanel = false;
            // 
            // lstItems
            // 
            this.lstItems.BackColor = System.Drawing.Color.SeaShell;
            this.lstItems.ForeColor = System.Drawing.Color.Navy;
            this.lstItems.FullRowSelect = true;
            this.lstItems.GridLines = true;
            this.lstItems.Location = new System.Drawing.Point(162, 178);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(383, 127);
            this.lstItems.TabIndex = 222;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.View = System.Windows.Forms.View.Details;
            this.lstItems.Visible = false;
            this.lstItems.Enter += new System.EventHandler(this.lstItems_Enter);
            this.lstItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstItems_KeyPress);
            this.lstItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDoubleClick);
            // 
            // rbQuemadura
            // 
            this.rbQuemadura.AutoSize = true;
            this.rbQuemadura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQuemadura.Location = new System.Drawing.Point(403, 20);
            this.rbQuemadura.Name = "rbQuemadura";
            this.rbQuemadura.Size = new System.Drawing.Size(103, 19);
            this.rbQuemadura.TabIndex = 229;
            this.rbQuemadura.TabStop = true;
            this.rbQuemadura.Text = "QUEMADURA";
            this.rbQuemadura.UseVisualStyleBackColor = true;
            this.rbQuemadura.CheckedChanged += new System.EventHandler(this.rbQuemadura_CheckedChanged);
            // 
            // rbMTC
            // 
            this.rbMTC.AutoSize = true;
            this.rbMTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMTC.Location = new System.Drawing.Point(335, 20);
            this.rbMTC.Name = "rbMTC";
            this.rbMTC.Size = new System.Drawing.Size(51, 19);
            this.rbMTC.TabIndex = 228;
            this.rbMTC.TabStop = true;
            this.rbMTC.Text = "MTC";
            this.rbMTC.UseVisualStyleBackColor = true;
            this.rbMTC.CheckedChanged += new System.EventHandler(this.rbMTC_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(295, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 231;
            this.label6.Text = "Tipo:";
            // 
            // frmAgregarItemBotiquin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 454);
            this.Controls.Add(this.dtgvListaBotiquin);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstItems);
            this.MaximizeBox = false;
            this.Name = "frmAgregarItemBotiquin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ÍTEMS DE BOTIQUÍN";
            this.Load += new System.EventHandler(this.frmAgregarItemBotiquin_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaBotiquin)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaBotiquinView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCodigoItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl dtgvListaBotiquin;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvListaBotiquinView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsEliminar;
        private System.Windows.Forms.ListView lstItems;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.RadioButton rbQuemadura;
        public System.Windows.Forms.RadioButton rbMTC;
    }
}