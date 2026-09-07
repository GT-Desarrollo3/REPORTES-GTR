namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class Seguimiento_Guias
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.dtgvSeguimientoGuias = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtpFechaFin = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.cboEstado = new MetroFramework.Controls.MetroComboBox();
            this.dtpFechaIni = new MetroFramework.Controls.MetroDateTime();
            this.txtNumero = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.cboParametro = new MetroFramework.Controls.MetroComboBox();
            this.txtParametro = new MetroFramework.Controls.MetroTextBox();
            this.txtSerie = new MetroFramework.Controls.MetroTextBox();
            this.btnBuscar = new MetroFramework.Controls.MetroButton();
            this.btnExcel = new MetroFramework.Controls.MetroButton();
            this.btnImprimir = new MetroFramework.Controls.MetroButton();
            this.chkNumero = new System.Windows.Forms.CheckBox();
            this.chkSerie = new System.Windows.Forms.CheckBox();
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSeguimientoGuias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgvSeguimientoGuias
            // 
            this.dtgvSeguimientoGuias.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dtgvSeguimientoGuias.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgvSeguimientoGuias.Location = new System.Drawing.Point(0, 0);
            this.dtgvSeguimientoGuias.LookAndFeel.SkinName = "Darkroom";
            this.dtgvSeguimientoGuias.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvSeguimientoGuias.MainView = this.dtgvDataView;
            this.dtgvSeguimientoGuias.Name = "dtgvSeguimientoGuias";
            this.dtgvSeguimientoGuias.Size = new System.Drawing.Size(879, 617);
            this.dtgvSeguimientoGuias.TabIndex = 2;
            this.dtgvSeguimientoGuias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView,
            this.gridView1});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvSeguimientoGuias;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dtgvSeguimientoGuias;
            this.gridView1.Name = "gridView1";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(227, 38);
            this.dtpFechaFin.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(121, 29);
            this.dtpFechaFin.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaFin.TabIndex = 22;
            this.dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(192, 42);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(29, 19);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel5.TabIndex = 24;
            this.metroLabel5.Text = "Fin:";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cboEstado
            // 
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.ItemHeight = 23;
            this.cboEstado.Items.AddRange(new object[] {
            "TODOS",
            "PREPARADO",
            "ASIGNADO",
            "COMPLETADO",
            "ANULADO",
            "ENTREGADO",
            "RECEPCIONADO",
            "ARCHIVADO",
            "NO ARCHIVADO"});
            this.cboEstado.Location = new System.Drawing.Point(430, 3);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(146, 29);
            this.cboEstado.Style = MetroFramework.MetroColorStyle.Red;
            this.cboEstado.TabIndex = 19;
            this.cboEstado.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboEstado.UseSelectable = true;
            this.cboEstado.SelectedIndexChanged += new System.EventHandler(this.cboEstado_SelectedIndexChanged);
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(227, 3);
            this.dtpFechaIni.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(121, 29);
            this.dtpFechaIni.Style = MetroFramework.MetroColorStyle.Red;
            this.dtpFechaIni.TabIndex = 21;
            this.dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // txtNumero
            // 
            this.txtNumero.Lines = new string[0];
            this.txtNumero.Location = new System.Drawing.Point(84, 38);
            this.txtNumero.MaxLength = 32767;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.PasswordChar = '\0';
            this.txtNumero.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumero.SelectedText = "";
            this.txtNumero.Size = new System.Drawing.Size(92, 29);
            this.txtNumero.Style = MetroFramework.MetroColorStyle.Red;
            this.txtNumero.TabIndex = 0;
            this.txtNumero.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtNumero.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(179, 7);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(42, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroLabel4.TabIndex = 23;
            this.metroLabel4.Text = "Inicio:";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // cboParametro
            // 
            this.cboParametro.FormattingEnabled = true;
            this.cboParametro.ItemHeight = 23;
            this.cboParametro.Items.AddRange(new object[] {
            "Usuario",
            "Conductor"});
            this.cboParametro.Location = new System.Drawing.Point(363, 38);
            this.cboParametro.Name = "cboParametro";
            this.cboParametro.Size = new System.Drawing.Size(105, 29);
            this.cboParametro.Style = MetroFramework.MetroColorStyle.Red;
            this.cboParametro.TabIndex = 25;
            this.cboParametro.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cboParametro.UseSelectable = true;
            this.cboParametro.SelectedIndexChanged += new System.EventHandler(this.cboParametro_SelectedIndexChanged);
            // 
            // txtParametro
            // 
            this.txtParametro.Lines = new string[0];
            this.txtParametro.Location = new System.Drawing.Point(474, 38);
            this.txtParametro.MaxLength = 32767;
            this.txtParametro.Name = "txtParametro";
            this.txtParametro.PasswordChar = '\0';
            this.txtParametro.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtParametro.SelectedText = "";
            this.txtParametro.Size = new System.Drawing.Size(92, 29);
            this.txtParametro.Style = MetroFramework.MetroColorStyle.Red;
            this.txtParametro.TabIndex = 26;
            this.txtParametro.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtParametro.UseSelectable = true;
            // 
            // txtSerie
            // 
            this.txtSerie.Lines = new string[0];
            this.txtSerie.Location = new System.Drawing.Point(85, 3);
            this.txtSerie.MaxLength = 32767;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.PasswordChar = '\0';
            this.txtSerie.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSerie.SelectedText = "";
            this.txtSerie.Size = new System.Drawing.Size(92, 29);
            this.txtSerie.Style = MetroFramework.MetroColorStyle.Red;
            this.txtSerie.TabIndex = 0;
            this.txtSerie.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtSerie.UseSelectable = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnBuscar.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnBuscar.Location = new System.Drawing.Point(579, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 43);
            this.btnBuscar.Style = MetroFramework.MetroColorStyle.Red;
            this.btnBuscar.TabIndex = 27;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnBuscar.UseSelectable = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click_1);
            // 
            // btnExcel
            // 
            this.btnExcel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExcel.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExcel.Location = new System.Drawing.Point(673, 23);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(108, 43);
            this.btnExcel.Style = MetroFramework.MetroColorStyle.Red;
            this.btnExcel.TabIndex = 28;
            this.btnExcel.Text = "Exportar a Excel";
            this.btnExcel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnExcel.UseSelectable = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click_1);
            // 
            // btnImprimir
            // 
            this.btnImprimir.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnImprimir.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnImprimir.Location = new System.Drawing.Point(786, 23);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(88, 43);
            this.btnImprimir.Style = MetroFramework.MetroColorStyle.Red;
            this.btnImprimir.TabIndex = 29;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnImprimir.UseSelectable = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // chkNumero
            // 
            this.chkNumero.AutoSize = true;
            this.chkNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNumero.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.chkNumero.Location = new System.Drawing.Point(3, 42);
            this.chkNumero.Name = "chkNumero";
            this.chkNumero.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkNumero.Size = new System.Drawing.Size(75, 20);
            this.chkNumero.TabIndex = 77;
            this.chkNumero.Text = "Numero";
            this.chkNumero.UseVisualStyleBackColor = true;
            this.chkNumero.CheckedChanged += new System.EventHandler(this.chkNumero_CheckedChanged);
            // 
            // chkSerie
            // 
            this.chkSerie.AutoSize = true;
            this.chkSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSerie.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.chkSerie.Location = new System.Drawing.Point(19, 7);
            this.chkSerie.Name = "chkSerie";
            this.chkSerie.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSerie.Size = new System.Drawing.Size(59, 20);
            this.chkSerie.TabIndex = 78;
            this.chkSerie.Text = "Serie";
            this.chkSerie.UseVisualStyleBackColor = true;
            this.chkSerie.CheckedChanged += new System.EventHandler(this.chkSerie_CheckedChanged);
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEstado.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.chkEstado.Location = new System.Drawing.Point(355, 7);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkEstado.Size = new System.Drawing.Size(70, 20);
            this.chkEstado.TabIndex = 79;
            this.chkEstado.Text = "Estado";
            this.chkEstado.UseVisualStyleBackColor = true;
            this.chkEstado.CheckedChanged += new System.EventHandler(this.chkEstado_CheckedChanged);
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
            this.splitContainer1.Panel1.Controls.Add(this.chkEstado);
            this.splitContainer1.Panel1.Controls.Add(this.chkSerie);
            this.splitContainer1.Panel1.Controls.Add(this.chkNumero);
            this.splitContainer1.Panel1.Controls.Add(this.btnImprimir);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            this.splitContainer1.Panel1.Controls.Add(this.txtSerie);
            this.splitContainer1.Panel1.Controls.Add(this.txtParametro);
            this.splitContainer1.Panel1.Controls.Add(this.cboParametro);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel4);
            this.splitContainer1.Panel1.Controls.Add(this.txtNumero);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaIni);
            this.splitContainer1.Panel1.Controls.Add(this.cboEstado);
            this.splitContainer1.Panel1.Controls.Add(this.metroLabel5);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFechaFin);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvSeguimientoGuias);
            this.splitContainer1.Size = new System.Drawing.Size(879, 700);
            this.splitContainer1.SplitterDistance = 79;
            this.splitContainer1.TabIndex = 27;
            // 
            // Seguimiento_Guias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 780);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Seguimiento_Guias";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Seguimiento de Guías";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Operaciones_Seguimiento_Guias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSeguimientoGuias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgvSeguimientoGuias;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private MetroFramework.Controls.MetroDateTime dtpFechaFin;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox cboEstado;
        private MetroFramework.Controls.MetroDateTime dtpFechaIni;
        private MetroFramework.Controls.MetroTextBox txtNumero;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox cboParametro;
        private MetroFramework.Controls.MetroTextBox txtParametro;
        private MetroFramework.Controls.MetroTextBox txtSerie;
        private MetroFramework.Controls.MetroButton btnBuscar;
        private MetroFramework.Controls.MetroButton btnExcel;
        private MetroFramework.Controls.MetroButton btnImprimir;
        private System.Windows.Forms.CheckBox chkNumero;
        private System.Windows.Forms.CheckBox chkSerie;
        private System.Windows.Forms.CheckBox chkEstado;
        private System.Windows.Forms.SplitContainer splitContainer1;


    }
}