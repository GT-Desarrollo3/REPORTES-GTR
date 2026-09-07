namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    partial class ListaClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaClientes));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtCliente = new MetroFramework.Controls.MetroTextBox();
            this.lblCliente = new MetroFramework.Controls.MetroLabel();
            this.dtgvListaClientes = new DevExpress.XtraGrid.GridControl();
            this.dtgvViewListaClientes = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViewListaClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.txtCliente);
            this.splitContainer1.Panel1.Controls.Add(this.lblCliente);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvListaClientes);
            this.splitContainer1.Size = new System.Drawing.Size(647, 279);
            this.splitContainer1.SplitterDistance = 41;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Appearance.Options.UseBackColor = true;
            this.btnBuscar.Appearance.Options.UseBorderColor = true;
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBuscar.Location = new System.Drawing.Point(351, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(58, 36);
            this.btnBuscar.TabIndex = 121;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.BackColor = System.Drawing.Color.White;
            this.txtCliente.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtCliente.Lines = new string[0];
            this.txtCliente.Location = new System.Drawing.Point(65, 5);
            this.txtCliente.MaxLength = 32767;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PasswordChar = '\0';
            this.txtCliente.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCliente.SelectedText = "";
            this.txtCliente.Size = new System.Drawing.Size(280, 29);
            this.txtCliente.TabIndex = 120;
            this.txtCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCliente.UseCustomForeColor = true;
            this.txtCliente.UseSelectable = true;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Enabled = false;
            this.lblCliente.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblCliente.Location = new System.Drawing.Point(12, 8);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(58, 19);
            this.lblCliente.Style = MetroFramework.MetroColorStyle.Red;
            this.lblCliente.TabIndex = 119;
            this.lblCliente.Text = "Cliente: ";
            this.lblCliente.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtgvListaClientes
            // 
            this.dtgvListaClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvListaClientes.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvListaClientes.Location = new System.Drawing.Point(0, 0);
            this.dtgvListaClientes.LookAndFeel.SkinName = "Darkroom";
            this.dtgvListaClientes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvListaClientes.MainView = this.dtgvViewListaClientes;
            this.dtgvListaClientes.Name = "dtgvListaClientes";
            this.dtgvListaClientes.Size = new System.Drawing.Size(647, 234);
            this.dtgvListaClientes.TabIndex = 8;
            this.dtgvListaClientes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvViewListaClientes});
            this.dtgvListaClientes.DoubleClick += new System.EventHandler(this.dtgvListaClientes_DoubleClick);
            // 
            // dtgvViewListaClientes
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
            this.dtgvViewListaClientes.FormatRules.Add(gridFormatRule1);
            this.dtgvViewListaClientes.GridControl = this.dtgvListaClientes;
            this.dtgvViewListaClientes.Name = "dtgvViewListaClientes";
            this.dtgvViewListaClientes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvViewListaClientes.OptionsBehavior.Editable = false;
            this.dtgvViewListaClientes.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvViewListaClientes.OptionsView.ColumnAutoWidth = false;
            this.dtgvViewListaClientes.OptionsView.ShowFooter = true;
            // 
            // ListaClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 359);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ListaClientes";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Lista de Clientes";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViewListaClientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroLabel lblCliente;
        private MetroFramework.Controls.MetroTextBox txtCliente;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgvListaClientes;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvViewListaClientes;
    }
}