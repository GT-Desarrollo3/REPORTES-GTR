namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    partial class frmActualizarDatosViajes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActualizarDatosViajes));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dtgViajeGuias = new DevExpress.XtraGrid.GridControl();
            this.dgvViajeGuiasView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtGuiaR = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGuiaT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxOperacion = new System.Windows.Forms.ComboBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViajeGuias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajeGuiasView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SlateBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 49);
            this.label1.TabIndex = 3;
            this.label1.Text = "ACTUALIZAR GUÍAS DE VIAJES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.dtgViajeGuias);
            this.panel3.Controls.Add(this.txtGuiaR);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtGuiaT);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtOT);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtCodigo);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnGuardar);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(413, 459);
            this.panel3.TabIndex = 185;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 16);
            this.label6.TabIndex = 244;
            this.label6.Text = "Guías enlazadas al viaje:";
            // 
            // dtgViajeGuias
            // 
            this.dtgViajeGuias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgViajeGuias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtgViajeGuias.Location = new System.Drawing.Point(20, 247);
            this.dtgViajeGuias.LookAndFeel.SkinMaskColor = System.Drawing.Color.DarkSlateBlue;
            this.dtgViajeGuias.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgViajeGuias.MainView = this.dgvViajeGuiasView;
            this.dtgViajeGuias.Name = "dtgViajeGuias";
            this.dtgViajeGuias.Size = new System.Drawing.Size(371, 138);
            this.dtgViajeGuias.TabIndex = 243;
            this.dtgViajeGuias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvViajeGuiasView,
            this.gridView1});
            this.dtgViajeGuias.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgViajeGuias_MouseUp);
            // 
            // dgvViajeGuiasView
            // 
            this.dgvViajeGuiasView.GridControl = this.dtgViajeGuias;
            this.dgvViajeGuiasView.Name = "dgvViajeGuiasView";
            this.dgvViajeGuiasView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvViajeGuiasView.OptionsBehavior.Editable = false;
            this.dgvViajeGuiasView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvViajeGuiasView.OptionsSelection.MultiSelect = true;
            this.dgvViajeGuiasView.OptionsView.ColumnAutoWidth = false;
            this.dgvViajeGuiasView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.dgvViajeGuiasView.OptionsView.RowAutoHeight = true;
            this.dgvViajeGuiasView.OptionsView.ShowGroupPanel = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgViajeGuias;
            this.gridView1.Name = "gridView1";
            // 
            // txtGuiaR
            // 
            this.txtGuiaR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuiaR.Location = new System.Drawing.Point(144, 181);
            this.txtGuiaR.Name = "txtGuiaR";
            this.txtGuiaR.Size = new System.Drawing.Size(247, 22);
            this.txtGuiaR.TabIndex = 241;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 242;
            this.label4.Text = "Guía Remitente:";
            // 
            // txtGuiaT
            // 
            this.txtGuiaT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuiaT.Location = new System.Drawing.Point(144, 142);
            this.txtGuiaT.Name = "txtGuiaT";
            this.txtGuiaT.Size = new System.Drawing.Size(247, 22);
            this.txtGuiaT.TabIndex = 239;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 240;
            this.label2.Text = "Guía Transportista:";
            // 
            // txtOT
            // 
            this.txtOT.BackColor = System.Drawing.SystemColors.Window;
            this.txtOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOT.Location = new System.Drawing.Point(301, 103);
            this.txtOT.Name = "txtOT";
            this.txtOT.Size = new System.Drawing.Size(90, 22);
            this.txtOT.TabIndex = 237;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(229, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 16);
            this.label5.TabIndex = 238;
            this.label5.Text = "N° de OT:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.Thistle;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(112, 103);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(90, 22);
            this.txtCodigo.TabIndex = 225;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 226;
            this.label3.Text = "Código Viaje:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(157, 403);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(97, 39);
            this.btnGuardar.TabIndex = 222;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.ToolTip = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxOperacion);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 69);
            this.groupBox1.TabIndex = 236;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccionar Operación: ";
            // 
            // cbxOperacion
            // 
            this.cbxOperacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxOperacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbxOperacion.FormattingEnabled = true;
            this.cbxOperacion.Items.AddRange(new object[] {
            "ELIMINAR GUÍA"});
            this.cbxOperacion.Location = new System.Drawing.Point(14, 28);
            this.cbxOperacion.Name = "cbxOperacion";
            this.cbxOperacion.Size = new System.Drawing.Size(232, 23);
            this.cbxOperacion.TabIndex = 223;
            this.cbxOperacion.DropDownClosed += new System.EventHandler(this.cbxOperacion_DropDownClosed);
            // 
            // frmActualizarDatosViajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 508);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmActualizarDatosViajes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACTUALIZAR DATOS DE VIAJES";
            this.Load += new System.EventHandler(this.frmActualizarDatosViajes_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViajeGuias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViajeGuiasView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbxOperacion;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtOT;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtGuiaR;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtGuiaT;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtgViajeGuias;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvViajeGuiasView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label6;
    }
}