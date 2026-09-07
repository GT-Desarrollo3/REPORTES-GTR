namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    partial class ListaBancos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaBancos));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleIconSet formatConditionRuleIconSet1 = new DevExpress.XtraEditors.FormatConditionRuleIconSet();
            DevExpress.XtraEditors.FormatConditionIconSet formatConditionIconSet1 = new DevExpress.XtraEditors.FormatConditionIconSet();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon1 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon2 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            DevExpress.XtraEditors.FormatConditionIconSetIcon formatConditionIconSetIcon3 = new DevExpress.XtraEditors.FormatConditionIconSetIcon();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.txtBanco = new MetroFramework.Controls.MetroTextBox();
            this.lblBanco = new MetroFramework.Controls.MetroLabel();
            this.dtgvListaBancos = new DevExpress.XtraGrid.GridControl();
            this.dtgvViewListaBancos = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaBancos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViewListaBancos)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtBanco);
            this.splitContainer1.Panel1.Controls.Add(this.lblBanco);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvListaBancos);
            this.splitContainer1.Size = new System.Drawing.Size(663, 276);
            this.splitContainer1.SplitterDistance = 37;
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
            this.btnBuscar.Location = new System.Drawing.Point(351, 0);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(58, 36);
            this.btnBuscar.TabIndex = 122;
            this.btnBuscar.ToolTip = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBanco
            // 
            this.txtBanco.BackColor = System.Drawing.Color.White;
            this.txtBanco.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBanco.Lines = new string[0];
            this.txtBanco.Location = new System.Drawing.Point(65, 6);
            this.txtBanco.MaxLength = 32767;
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.PasswordChar = '\0';
            this.txtBanco.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtBanco.SelectedText = "";
            this.txtBanco.Size = new System.Drawing.Size(280, 29);
            this.txtBanco.TabIndex = 122;
            this.txtBanco.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBanco.UseCustomForeColor = true;
            this.txtBanco.UseSelectable = true;
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Enabled = false;
            this.lblBanco.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblBanco.Location = new System.Drawing.Point(12, 9);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(53, 19);
            this.lblBanco.Style = MetroFramework.MetroColorStyle.Red;
            this.lblBanco.TabIndex = 121;
            this.lblBanco.Text = "Banco: ";
            this.lblBanco.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // dtgvListaBancos
            // 
            this.dtgvListaBancos.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvListaBancos.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvListaBancos.Location = new System.Drawing.Point(0, 0);
            this.dtgvListaBancos.LookAndFeel.SkinName = "Darkroom";
            this.dtgvListaBancos.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvListaBancos.MainView = this.dtgvViewListaBancos;
            this.dtgvListaBancos.Name = "dtgvListaBancos";
            this.dtgvListaBancos.Size = new System.Drawing.Size(663, 235);
            this.dtgvListaBancos.TabIndex = 9;
            this.dtgvListaBancos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvViewListaBancos});
            this.dtgvListaBancos.DoubleClick += new System.EventHandler(this.dtgvListaBancos_DoubleClick);
            // 
            // dtgvViewListaBancos
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
            this.dtgvViewListaBancos.FormatRules.Add(gridFormatRule1);
            this.dtgvViewListaBancos.GridControl = this.dtgvListaBancos;
            this.dtgvViewListaBancos.Name = "dtgvViewListaBancos";
            this.dtgvViewListaBancos.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvViewListaBancos.OptionsBehavior.Editable = false;
            this.dtgvViewListaBancos.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dtgvViewListaBancos.OptionsView.ColumnAutoWidth = false;
            this.dtgvViewListaBancos.OptionsView.ShowFooter = true;
            // 
            // ListaBancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 356);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ListaBancos";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Lista de Bancos";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListaBancos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvViewListaBancos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroTextBox txtBanco;
        private MetroFramework.Controls.MetroLabel lblBanco;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraGrid.GridControl dtgvListaBancos;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvViewListaBancos;
    }
}