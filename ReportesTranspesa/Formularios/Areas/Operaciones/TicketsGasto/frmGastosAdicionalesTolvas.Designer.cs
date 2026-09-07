namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmGastosAdicionalesTolvas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGastosAdicionalesTolvas));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.dtpFechaViatico = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConductor = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMonto2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxTipoGasto = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnImprimirViatico = new DevExpress.XtraEditors.SimpleButton();
            this.dtgvViaticos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reimprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvViaticosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPlanilla2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticosView)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFechaViatico
            // 
            this.dtpFechaViatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.dtpFechaViatico.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaViatico.Location = new System.Drawing.Point(254, 82);
            this.dtpFechaViatico.Name = "dtpFechaViatico";
            this.dtpFechaViatico.Size = new System.Drawing.Size(106, 22);
            this.dtpFechaViatico.TabIndex = 155;
            this.dtpFechaViatico.Tag = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(386, 20);
            this.label1.TabIndex = 156;
            this.label1.Text = "GASTOS ADICIONALES DEL CONDUCTOR:";
            // 
            // lblConductor
            // 
            this.lblConductor.AutoSize = true;
            this.lblConductor.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold);
            this.lblConductor.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConductor.Location = new System.Drawing.Point(15, 41);
            this.lblConductor.Name = "lblConductor";
            this.lblConductor.Size = new System.Drawing.Size(140, 26);
            this.lblConductor.TabIndex = 157;
            this.lblConductor.Text = "CONDUCTOR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label10.Location = new System.Drawing.Point(199, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 16);
            this.label10.TabIndex = 158;
            this.label10.Text = "Fecha:";
            // 
            // txtMonto2
            // 
            this.txtMonto2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonto2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtMonto2.Location = new System.Drawing.Point(431, 117);
            this.txtMonto2.Name = "txtMonto2";
            this.txtMonto2.Size = new System.Drawing.Size(80, 22);
            this.txtMonto2.TabIndex = 160;
            this.txtMonto2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto2_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label6.Location = new System.Drawing.Point(377, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 162;
            this.label6.Text = "Monto:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label7.Location = new System.Drawing.Point(25, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 16);
            this.label7.TabIndex = 161;
            this.label7.Text = "Gasto:";
            // 
            // cbxTipoGasto
            // 
            this.cbxTipoGasto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoGasto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxTipoGasto.FormattingEnabled = true;
            this.cbxTipoGasto.Location = new System.Drawing.Point(78, 118);
            this.cbxTipoGasto.Name = "cbxTipoGasto";
            this.cbxTipoGasto.Size = new System.Drawing.Size(282, 21);
            this.cbxTipoGasto.TabIndex = 159;
            this.cbxTipoGasto.DropDownClosed += new System.EventHandler(this.cbxTipoGasto_DropDownClosed);
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
            this.btnGuardar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardar.Location = new System.Drawing.Point(531, 158);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(47, 44);
            this.btnGuardar.TabIndex = 163;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.ToolTip = "Registrar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnImprimirViatico
            // 
            this.btnImprimirViatico.Appearance.BackColor = System.Drawing.Color.White;
            this.btnImprimirViatico.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnImprimirViatico.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimirViatico.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirViatico.Appearance.Options.UseBackColor = true;
            this.btnImprimirViatico.Appearance.Options.UseBorderColor = true;
            this.btnImprimirViatico.Appearance.Options.UseFont = true;
            this.btnImprimirViatico.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnImprimirViatico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirViatico.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirViatico.Image")));
            this.btnImprimirViatico.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnImprimirViatico.Location = new System.Drawing.Point(510, 158);
            this.btnImprimirViatico.Name = "btnImprimirViatico";
            this.btnImprimirViatico.Size = new System.Drawing.Size(47, 44);
            this.btnImprimirViatico.TabIndex = 164;
            this.btnImprimirViatico.Tag = "5";
            this.btnImprimirViatico.ToolTip = "Impirmir Viático";
            this.btnImprimirViatico.Visible = false;
            this.btnImprimirViatico.Click += new System.EventHandler(this.btnImprimirViatico_Click);
            // 
            // dtgvViaticos
            // 
            this.dtgvViaticos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgvViaticos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgvViaticos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgvViaticos.Location = new System.Drawing.Point(0, 228);
            this.dtgvViaticos.LookAndFeel.SkinName = "Blue";
            this.dtgvViaticos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvViaticos.MainView = this.dtgvViaticosView;
            this.dtgvViaticos.Name = "dtgvViaticos";
            this.dtgvViaticos.Size = new System.Drawing.Size(601, 199);
            this.dtgvViaticos.TabIndex = 165;
            this.dtgvViaticos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvViaticosView});
            this.dtgvViaticos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgvViaticos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reimprimirToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip2";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 48);
            // 
            // reimprimirToolStripMenuItem
            // 
            this.reimprimirToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.impresora;
            this.reimprimirToolStripMenuItem.Name = "reimprimirToolStripMenuItem";
            this.reimprimirToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.reimprimirToolStripMenuItem.Text = "Imprimir";
            this.reimprimirToolStripMenuItem.Click += new System.EventHandler(this.reimprimirToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // dtgvViaticosView
            // 
            gridFormatRule1.Name = "Format0";
            formatConditionIconSet1.CategoryName = "Ratings";
            formatConditionIconSetIcon1.PredefinedName = "TrafficLights3_3.png";
            formatConditionIconSetIcon1.Value = new decimal(new int[] {
            67,
            0,
            0,
            0});
            formatConditionIconSetIcon1.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon2.PredefinedName = "TrafficLights3_1.png";
            formatConditionIconSetIcon2.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
            formatConditionIconSetIcon2.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon3.PredefinedName = "Stars3_3.png";
            formatConditionIconSetIcon3.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon1);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon2);
            formatConditionIconSet1.Icons.Add(formatConditionIconSetIcon3);
            formatConditionIconSet1.Name = "Stars3";
            formatConditionIconSet1.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet1.IconSet = formatConditionIconSet1;
            gridFormatRule1.Rule = formatConditionRuleIconSet1;
            this.dtgvViaticosView.FormatRules.Add(gridFormatRule1);
            this.dtgvViaticosView.GridControl = this.dtgvViaticos;
            this.dtgvViaticosView.Name = "dtgvViaticosView";
            this.dtgvViaticosView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvViaticosView.OptionsBehavior.Editable = false;
            this.dtgvViaticosView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvViaticosView.OptionsView.ColumnAutoWidth = false;
            this.dtgvViaticosView.OptionsView.RowAutoHeight = true;
            this.dtgvViaticosView.OptionsView.ShowGroupPanel = false;
            this.dtgvViaticosView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dtgvViaticosView_CustomDrawCell);
            // 
            // txtMotivo
            // 
            this.txtMotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.Location = new System.Drawing.Point(78, 154);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(433, 52);
            this.txtMotivo.TabIndex = 166;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label8.Location = new System.Drawing.Point(21, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 167;
            this.label8.Text = "Motivo:";
            // 
            // txtPlanilla2
            // 
            this.txtPlanilla2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanilla2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtPlanilla2.Location = new System.Drawing.Point(78, 82);
            this.txtPlanilla2.Name = "txtPlanilla2";
            this.txtPlanilla2.ReadOnly = true;
            this.txtPlanilla2.Size = new System.Drawing.Size(100, 22);
            this.txtPlanilla2.TabIndex = 168;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.Location = new System.Drawing.Point(17, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 169;
            this.label2.Text = "Planilla:";
            // 
            // frmGastosAdicionalesTolvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(601, 427);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtgvViaticos);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dtpFechaViatico);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblConductor);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMonto2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxTipoGasto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPlanilla2);
            this.Controls.Add(this.btnImprimirViatico);
            this.MaximizeBox = false;
            this.Name = "frmGastosAdicionalesTolvas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GASTOS ADICIONALES";
            this.Load += new System.EventHandler(this.frmGastosAdicionalesTolvas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticosView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DateTimePicker dtpFechaViatico;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblConductor;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtMonto2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cbxTipoGasto;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        public DevExpress.XtraEditors.SimpleButton btnImprimirViatico;
        private DevExpress.XtraGrid.GridControl dtgvViaticos;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvViaticosView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        public System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtPlanilla2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem reimprimirToolStripMenuItem;
    }
}