namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlDocumentos
{
    partial class frmHistorialDocumentos
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet2 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet2 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon4 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon5 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon6 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dgvHistorialDocumentos = new DevExpress.XtraGrid.GridControl();
            this.dgvHistorialDocumentosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConductor = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtVehiculo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbVehiculos = new System.Windows.Forms.RadioButton();
            this.rbConductores = new System.Windows.Forms.RadioButton();
            this.FechaModFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.FechaModIni = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvHistorialVehiculos = new DevExpress.XtraGrid.GridControl();
            this.dgvHistorialVehiculosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialDocumentosView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialVehiculos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialVehiculosView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Khaki;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1295, 33);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(395, 30);
            this.toolStripLabel1.Text = "HISTORIAL DE DOCUMENTOS RENOVADOS";
            // 
            // dgvHistorialDocumentos
            // 
            this.dgvHistorialDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorialDocumentos.Location = new System.Drawing.Point(0, 132);
            this.dgvHistorialDocumentos.LookAndFeel.SkinName = "Blue";
            this.dgvHistorialDocumentos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgvHistorialDocumentos.MainView = this.dgvHistorialDocumentosView;
            this.dgvHistorialDocumentos.Name = "dgvHistorialDocumentos";
            this.dgvHistorialDocumentos.Size = new System.Drawing.Size(1295, 532);
            this.dgvHistorialDocumentos.TabIndex = 9;
            this.dgvHistorialDocumentos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvHistorialDocumentosView});
            // 
            // dgvHistorialDocumentosView
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
            this.dgvHistorialDocumentosView.FormatRules.Add(gridFormatRule1);
            this.dgvHistorialDocumentosView.GridControl = this.dgvHistorialDocumentos;
            this.dgvHistorialDocumentosView.Name = "dgvHistorialDocumentosView";
            this.dgvHistorialDocumentosView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvHistorialDocumentosView.OptionsBehavior.Editable = false;
            this.dgvHistorialDocumentosView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvHistorialDocumentosView.OptionsView.ColumnAutoWidth = false;
            this.dgvHistorialDocumentosView.OptionsView.ShowFooter = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Image = global::ReportesTranspesa.Properties.Resources.view_zoom_115406;
            this.btnBuscar.Location = new System.Drawing.Point(900, 42);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(43, 44);
            this.btnBuscar.TabIndex = 10;
            this.btnBuscar.Text = "&B";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Conductor:";
            // 
            // txtConductor
            // 
            this.txtConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConductor.Location = new System.Drawing.Point(92, 53);
            this.txtConductor.Name = "txtConductor";
            this.txtConductor.Size = new System.Drawing.Size(309, 22);
            this.txtConductor.TabIndex = 27;
            this.txtConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConductor_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rbVehiculos);
            this.panel1.Controls.Add(this.rbConductores);
            this.panel1.Controls.Add(this.FechaModFin);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.FechaModIni);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.txtConductor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1295, 99);
            this.panel1.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtVehiculo);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(12, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(403, 40);
            this.panel2.TabIndex = 35;
            // 
            // txtVehiculo
            // 
            this.txtVehiculo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehiculo.Location = new System.Drawing.Point(80, 7);
            this.txtVehiculo.Name = "txtVehiculo";
            this.txtVehiculo.Size = new System.Drawing.Size(309, 22);
            this.txtVehiculo.TabIndex = 37;
            this.txtVehiculo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVehiculo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "Vehículo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "Tipo de Relación:";
            // 
            // rbVehiculos
            // 
            this.rbVehiculos.AutoSize = true;
            this.rbVehiculos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbVehiculos.Location = new System.Drawing.Point(325, 17);
            this.rbVehiculos.Name = "rbVehiculos";
            this.rbVehiculos.Size = new System.Drawing.Size(111, 20);
            this.rbVehiculos.TabIndex = 33;
            this.rbVehiculos.TabStop = true;
            this.rbVehiculos.Text = "VEHICULOS";
            this.rbVehiculos.UseVisualStyleBackColor = true;
            this.rbVehiculos.Click += new System.EventHandler(this.rbVehiculos_Click);
            // 
            // rbConductores
            // 
            this.rbConductores.AutoSize = true;
            this.rbConductores.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbConductores.Location = new System.Drawing.Point(146, 17);
            this.rbConductores.Name = "rbConductores";
            this.rbConductores.Size = new System.Drawing.Size(142, 20);
            this.rbConductores.TabIndex = 32;
            this.rbConductores.TabStop = true;
            this.rbConductores.Text = "CONDUCTORES";
            this.rbConductores.UseVisualStyleBackColor = true;
            this.rbConductores.Click += new System.EventHandler(this.rbConductores_Click);
            // 
            // FechaModFin
            // 
            this.FechaModFin.CustomFormat = "dd-MM-yyyy";
            this.FechaModFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaModFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaModFin.Location = new System.Drawing.Point(730, 53);
            this.FechaModFin.Name = "FechaModFin";
            this.FechaModFin.Size = new System.Drawing.Size(106, 22);
            this.FechaModFin.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(707, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = " - ";
            // 
            // FechaModIni
            // 
            this.FechaModIni.CustomFormat = "dd-MM-yyyy";
            this.FechaModIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaModIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FechaModIni.Location = new System.Drawing.Point(599, 53);
            this.FechaModIni.Name = "FechaModIni";
            this.FechaModIni.Size = new System.Drawing.Size(106, 22);
            this.FechaModIni.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(446, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Fecha de Modificación:";
            // 
            // dgvHistorialVehiculos
            // 
            this.dgvHistorialVehiculos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorialVehiculos.Location = new System.Drawing.Point(0, 132);
            this.dgvHistorialVehiculos.LookAndFeel.SkinName = "Blue";
            this.dgvHistorialVehiculos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgvHistorialVehiculos.MainView = this.dgvHistorialVehiculosView;
            this.dgvHistorialVehiculos.Name = "dgvHistorialVehiculos";
            this.dgvHistorialVehiculos.Size = new System.Drawing.Size(1295, 532);
            this.dgvHistorialVehiculos.TabIndex = 29;
            this.dgvHistorialVehiculos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvHistorialVehiculosView});
            // 
            // dgvHistorialVehiculosView
            // 
            gridFormatRule2.Name = "Format0";
            formatConditionIconSet2.CategoryName = "Ratings";
            formatConditionIconSetIcon4.PredefinedName = "TrafficLights3_3.png";
            formatConditionIconSetIcon4.Value = new decimal(new int[] {
            67,
            0,
            0,
            0});
            formatConditionIconSetIcon4.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon5.PredefinedName = "TrafficLights3_1.png";
            formatConditionIconSetIcon5.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
            formatConditionIconSetIcon5.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSetIcon6.PredefinedName = "Stars3_3.png";
            formatConditionIconSetIcon6.ValueComparison = DevExpress.XtraEditors.FormatConditionComparisonType.GreaterOrEqual;
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon4);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon5);
            formatConditionIconSet2.Icons.Add(formatConditionIconSetIcon6);
            formatConditionIconSet2.Name = "Stars3";
            formatConditionIconSet2.ValueType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            formatConditionRuleIconSet2.IconSet = formatConditionIconSet2;
            gridFormatRule2.Rule = formatConditionRuleIconSet2;
            this.dgvHistorialVehiculosView.FormatRules.Add(gridFormatRule2);
            this.dgvHistorialVehiculosView.GridControl = this.dgvHistorialVehiculos;
            this.dgvHistorialVehiculosView.Name = "dgvHistorialVehiculosView";
            this.dgvHistorialVehiculosView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvHistorialVehiculosView.OptionsBehavior.Editable = false;
            this.dgvHistorialVehiculosView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvHistorialVehiculosView.OptionsView.ColumnAutoWidth = false;
            this.dgvHistorialVehiculosView.OptionsView.ShowFooter = true;
            // 
            // frmHistorialDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1295, 664);
            this.Controls.Add(this.dgvHistorialVehiculos);
            this.Controls.Add(this.dgvHistorialDocumentos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.Name = "frmHistorialDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de Documentos Renovados";
            this.Load += new System.EventHandler(this.frmHistorialDocumentos_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialDocumentosView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialVehiculos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialVehiculosView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private DevExpress.XtraGrid.GridControl dgvHistorialDocumentos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvHistorialDocumentosView;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConductor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker FechaModFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker FechaModIni;
        private System.Windows.Forms.RadioButton rbVehiculos;
        private System.Windows.Forms.RadioButton rbConductores;
        private DevExpress.XtraGrid.GridControl dgvHistorialVehiculos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvHistorialVehiculosView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtVehiculo;
        private System.Windows.Forms.Label label5;
    }
}