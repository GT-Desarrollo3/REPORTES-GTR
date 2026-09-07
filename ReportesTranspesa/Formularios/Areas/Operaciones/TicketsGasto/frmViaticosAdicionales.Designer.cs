namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    partial class frmViaticosAdicionales
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViaticosAdicionales));
            this.dtgvViaticos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.adjuntarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtgvViaticosView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.cbxTipoViatico = new System.Windows.Forms.ComboBox();
            this.dtpFechaViatico = new System.Windows.Forms.DateTimePicker();
            this.btnImprimirViatico = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConductor = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtViaticoConductor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.metroLabel20 = new MetroFramework.Controls.MetroLabel();
            this.btnGuardar2 = new DevExpress.XtraEditors.SimpleButton();
            this.lstConductores = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImprimir2 = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMonto2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxTipoGasto = new System.Windows.Forms.ComboBox();
            this.cbxRuta = new System.Windows.Forms.ComboBox();
            this.cbRuta = new System.Windows.Forms.CheckBox();
            this.cbRedondeo = new System.Windows.Forms.CheckBox();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPlanilla = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticos)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticosView)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgvViaticos
            // 
            this.dtgvViaticos.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgvViaticos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgvViaticos.Location = new System.Drawing.Point(0, 246);
            this.dtgvViaticos.LookAndFeel.SkinName = "Blue";
            this.dtgvViaticos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvViaticos.MainView = this.dtgvViaticosView;
            this.dtgvViaticos.Name = "dtgvViaticos";
            this.dtgvViaticos.Size = new System.Drawing.Size(759, 240);
            this.dtgvViaticos.TabIndex = 128;
            this.dtgvViaticos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvViaticosView});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adjuntarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(121, 48);
            // 
            // adjuntarToolStripMenuItem
            // 
            this.adjuntarToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.adjuntarToolStripMenuItem.Name = "adjuntarToolStripMenuItem";
            this.adjuntarToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.adjuntarToolStripMenuItem.Text = "Adjuntar";
            this.adjuntarToolStripMenuItem.Click += new System.EventHandler(this.adjuntarToolStripMenuItem_Click);
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
            // 
            // txtMonto
            // 
            this.txtMonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtMonto.Location = new System.Drawing.Point(312, 10);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(73, 22);
            this.txtMonto.TabIndex = 126;
            this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto_KeyPress);
            // 
            // cbxTipoViatico
            // 
            this.cbxTipoViatico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTipoViatico.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoViatico.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoViatico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoViatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxTipoViatico.FormattingEnabled = true;
            this.cbxTipoViatico.Location = new System.Drawing.Point(70, 10);
            this.cbxTipoViatico.Name = "cbxTipoViatico";
            this.cbxTipoViatico.Size = new System.Drawing.Size(169, 21);
            this.cbxTipoViatico.TabIndex = 113;
            this.cbxTipoViatico.SelectedIndexChanged += new System.EventHandler(this.cbxTipoViatico_SelectedIndexChanged);
            this.cbxTipoViatico.DropDownClosed += new System.EventHandler(this.cbxTipoViatico_DropDownClosed);
            // 
            // dtpFechaViatico
            // 
            this.dtpFechaViatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.dtpFechaViatico.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaViatico.Location = new System.Drawing.Point(96, 132);
            this.dtpFechaViatico.Name = "dtpFechaViatico";
            this.dtpFechaViatico.Size = new System.Drawing.Size(106, 22);
            this.dtpFechaViatico.TabIndex = 108;
            this.dtpFechaViatico.Tag = "2";
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
            this.btnImprimirViatico.Location = new System.Drawing.Point(690, 108);
            this.btnImprimirViatico.Name = "btnImprimirViatico";
            this.btnImprimirViatico.Size = new System.Drawing.Size(47, 44);
            this.btnImprimirViatico.TabIndex = 130;
            this.btnImprimirViatico.Tag = "5";
            this.btnImprimirViatico.ToolTip = "Impirmir Viático";
            this.btnImprimirViatico.Visible = false;
            this.btnImprimirViatico.Click += new System.EventHandler(this.btnImprimirViatico_Click);
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
            this.btnGuardar.Location = new System.Drawing.Point(632, 108);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(47, 44);
            this.btnGuardar.TabIndex = 129;
            this.btnGuardar.Tag = "5";
            this.btnGuardar.ToolTip = "Registrar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(386, 20);
            this.label1.TabIndex = 131;
            this.label1.Text = "GASTOS ADICIONALES DEL CONDUCTOR:";
            // 
            // lblConductor
            // 
            this.lblConductor.AutoSize = true;
            this.lblConductor.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold);
            this.lblConductor.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConductor.Location = new System.Drawing.Point(14, 42);
            this.lblConductor.Name = "lblConductor";
            this.lblConductor.Size = new System.Drawing.Size(140, 26);
            this.lblConductor.TabIndex = 132;
            this.lblConductor.Text = "CONDUCTOR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label10.Location = new System.Drawing.Point(41, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 16);
            this.label10.TabIndex = 133;
            this.label10.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 134;
            this.label2.Text = "Viático:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label3.Location = new System.Drawing.Point(258, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 135;
            this.label3.Text = "Monto:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.txtViaticoConductor);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.metroLabel20);
            this.panel4.Location = new System.Drawing.Point(0, 11);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(608, 72);
            this.panel4.TabIndex = 136;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label5.Location = new System.Drawing.Point(18, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 146;
            this.label5.Text = "Conductor:";
            // 
            // txtViaticoConductor
            // 
            this.txtViaticoConductor.BackColor = System.Drawing.SystemColors.Window;
            this.txtViaticoConductor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtViaticoConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtViaticoConductor.Location = new System.Drawing.Point(96, 43);
            this.txtViaticoConductor.Name = "txtViaticoConductor";
            this.txtViaticoConductor.Size = new System.Drawing.Size(498, 22);
            this.txtViaticoConductor.TabIndex = 145;
            this.txtViaticoConductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtViaticoConductor_KeyPress);
            this.txtViaticoConductor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtViaticoConductor_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(16, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(377, 20);
            this.label4.TabIndex = 144;
            this.label4.Text = "VIÁTICO PARA CONDUCTORES SIN VIAJE";
            // 
            // metroLabel20
            // 
            this.metroLabel20.AutoSize = true;
            this.metroLabel20.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel20.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.metroLabel20.Location = new System.Drawing.Point(97, 18);
            this.metroLabel20.Name = "metroLabel20";
            this.metroLabel20.Size = new System.Drawing.Size(0, 0);
            this.metroLabel20.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel20.TabIndex = 104;
            this.metroLabel20.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnGuardar2
            // 
            this.btnGuardar2.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardar2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardar2.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardar2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar2.Appearance.Options.UseBackColor = true;
            this.btnGuardar2.Appearance.Options.UseBorderColor = true;
            this.btnGuardar2.Appearance.Options.UseFont = true;
            this.btnGuardar2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnGuardar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar2.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar2.Image")));
            this.btnGuardar2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnGuardar2.Location = new System.Drawing.Point(8, 16);
            this.btnGuardar2.Name = "btnGuardar2";
            this.btnGuardar2.Size = new System.Drawing.Size(99, 38);
            this.btnGuardar2.TabIndex = 139;
            this.btnGuardar2.Tag = "5";
            this.btnGuardar2.Text = "Registrar";
            this.btnGuardar2.ToolTip = "Registrar";
            this.btnGuardar2.Click += new System.EventHandler(this.btnGuardar2_Click);
            // 
            // lstConductores
            // 
            this.lstConductores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstConductores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConductores.ForeColor = System.Drawing.Color.Navy;
            this.lstConductores.FullRowSelect = true;
            this.lstConductores.GridLines = true;
            this.lstConductores.Location = new System.Drawing.Point(96, 75);
            this.lstConductores.MultiSelect = false;
            this.lstConductores.Name = "lstConductores";
            this.lstConductores.Size = new System.Drawing.Size(498, 118);
            this.lstConductores.TabIndex = 147;
            this.lstConductores.UseCompatibleStateImageBehavior = false;
            this.lstConductores.View = System.Windows.Forms.View.Details;
            this.lstConductores.Visible = false;
            this.lstConductores.Enter += new System.EventHandler(this.lstConductores_Enter);
            this.lstConductores.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstConductores_KeyPress);
            this.lstConductores.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstConductores_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGuardar2);
            this.panel1.Controls.Add(this.btnImprimir2);
            this.panel1.Location = new System.Drawing.Point(625, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(116, 124);
            this.panel1.TabIndex = 149;
            // 
            // btnImprimir2
            // 
            this.btnImprimir2.Appearance.BackColor = System.Drawing.Color.White;
            this.btnImprimir2.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnImprimir2.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnImprimir2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir2.Appearance.Options.UseBackColor = true;
            this.btnImprimir2.Appearance.Options.UseBorderColor = true;
            this.btnImprimir2.Appearance.Options.UseFont = true;
            this.btnImprimir2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnImprimir2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir2.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir2.Image")));
            this.btnImprimir2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnImprimir2.Location = new System.Drawing.Point(8, 69);
            this.btnImprimir2.Name = "btnImprimir2";
            this.btnImprimir2.Size = new System.Drawing.Size(99, 38);
            this.btnImprimir2.TabIndex = 148;
            this.btnImprimir2.Tag = "5";
            this.btnImprimir2.Text = "Imprimir";
            this.btnImprimir2.ToolTip = "Imprimir";
            this.btnImprimir2.Click += new System.EventHandler(this.btnImprimir2_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.txtMonto);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbxTipoViatico);
            this.panel2.Location = new System.Drawing.Point(208, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 42);
            this.panel2.TabIndex = 150;
            // 
            // txtMonto2
            // 
            this.txtMonto2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonto2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonto2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtMonto2.Location = new System.Drawing.Point(520, 133);
            this.txtMonto2.Name = "txtMonto2";
            this.txtMonto2.ReadOnly = true;
            this.txtMonto2.Size = new System.Drawing.Size(73, 22);
            this.txtMonto2.TabIndex = 152;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label6.Location = new System.Drawing.Point(466, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 154;
            this.label6.Text = "Monto:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label7.Location = new System.Drawing.Point(225, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 16);
            this.label7.TabIndex = 153;
            this.label7.Text = "Gasto:";
            // 
            // cbxTipoGasto
            // 
            this.cbxTipoGasto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTipoGasto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoGasto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxTipoGasto.FormattingEnabled = true;
            this.cbxTipoGasto.Location = new System.Drawing.Point(278, 133);
            this.cbxTipoGasto.Name = "cbxTipoGasto";
            this.cbxTipoGasto.Size = new System.Drawing.Size(169, 21);
            this.cbxTipoGasto.TabIndex = 151;
            this.cbxTipoGasto.SelectedIndexChanged += new System.EventHandler(this.cbxTipoGasto_SelectedIndexChanged);
            this.cbxTipoGasto.DropDownClosed += new System.EventHandler(this.cbxTipoGasto_DropDownClosed);
            // 
            // cbxRuta
            // 
            this.cbxRuta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxRuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxRuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbxRuta.FormattingEnabled = true;
            this.cbxRuta.Location = new System.Drawing.Point(96, 94);
            this.cbxRuta.Name = "cbxRuta";
            this.cbxRuta.Size = new System.Drawing.Size(418, 21);
            this.cbxRuta.TabIndex = 155;
            this.cbxRuta.SelectedIndexChanged += new System.EventHandler(this.cbxRuta_SelectedIndexChanged);
            this.cbxRuta.DropDownClosed += new System.EventHandler(this.cbxRuta_DropDownClosed);
            // 
            // cbRuta
            // 
            this.cbRuta.AutoSize = true;
            this.cbRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.cbRuta.Location = new System.Drawing.Point(32, 95);
            this.cbRuta.Name = "cbRuta";
            this.cbRuta.Size = new System.Drawing.Size(58, 20);
            this.cbRuta.TabIndex = 157;
            this.cbRuta.Text = "Ruta:";
            this.cbRuta.UseVisualStyleBackColor = true;
            this.cbRuta.CheckedChanged += new System.EventHandler(this.cbRuta_CheckedChanged);
            // 
            // cbRedondeo
            // 
            this.cbRedondeo.AutoSize = true;
            this.cbRedondeo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbRedondeo.Location = new System.Drawing.Point(524, 94);
            this.cbRedondeo.Name = "cbRedondeo";
            this.cbRedondeo.Size = new System.Drawing.Size(84, 19);
            this.cbRedondeo.TabIndex = 158;
            this.cbRedondeo.Text = "Redondeo";
            this.cbRedondeo.UseVisualStyleBackColor = true;
            this.cbRedondeo.CheckedChanged += new System.EventHandler(this.cbRedondeo_CheckedChanged);
            // 
            // txtMotivo
            // 
            this.txtMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.Location = new System.Drawing.Point(278, 170);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(315, 52);
            this.txtMotivo.TabIndex = 159;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label8.Location = new System.Drawing.Point(221, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 160;
            this.label8.Text = "Motivo:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.label9.Location = new System.Drawing.Point(35, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 16);
            this.label9.TabIndex = 161;
            this.label9.Text = "Planilla:";
            // 
            // txtPlanilla
            // 
            this.txtPlanilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlanilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.txtPlanilla.Location = new System.Drawing.Point(96, 170);
            this.txtPlanilla.Name = "txtPlanilla";
            this.txtPlanilla.ReadOnly = true;
            this.txtPlanilla.Size = new System.Drawing.Size(106, 22);
            this.txtPlanilla.TabIndex = 162;
            // 
            // frmViaticosAdicionales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(759, 486);
            this.Controls.Add(this.txtPlanilla);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbRedondeo);
            this.Controls.Add(this.cbRuta);
            this.Controls.Add(this.cbxRuta);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dtgvViaticos);
            this.Controls.Add(this.dtpFechaViatico);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblConductor);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMonto2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxTipoGasto);
            this.Controls.Add(this.lstConductores);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnImprimirViatico);
            this.Controls.Add(this.btnGuardar);
            this.Name = "frmViaticosAdicionales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIÁTICOS ADICIONALES";
            this.Load += new System.EventHandler(this.frmViaticosAdicionales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticos)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViaticosView)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgvViaticos;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvViaticosView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblConductor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtViaticoConductor;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton btnGuardar2;
        private MetroFramework.Controls.MetroLabel metroLabel20;
        private System.Windows.Forms.ListView lstConductores;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnImprimir2;
        public System.Windows.Forms.ToolStripMenuItem adjuntarToolStripMenuItem;
        public System.Windows.Forms.TextBox txtMonto;
        public System.Windows.Forms.ComboBox cbxTipoViatico;
        public System.Windows.Forms.DateTimePicker dtpFechaViatico;
        public DevExpress.XtraEditors.SimpleButton btnImprimirViatico;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        public System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        public System.Windows.Forms.TextBox txtMonto2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cbxTipoGasto;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox cbxRuta;
        public System.Windows.Forms.CheckBox cbRuta;
        public System.Windows.Forms.CheckBox cbRedondeo;
        public System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtPlanilla;
    }
}