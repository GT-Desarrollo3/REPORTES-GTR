namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias
{
    partial class frmReporteIncidencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReporteIncidencias));
            this.label6 = new System.Windows.Forms.Label();
            this.dtgIncidentesR = new DevExpress.XtraGrid.GridControl();
            this.dgvIncidentesRVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtpPeriodo = new System.Windows.Forms.DateTimePicker();
            this.rbIncidenciasOperacion = new System.Windows.Forms.RadioButton();
            this.rbIncidenciasPersona = new System.Windows.Forms.RadioButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.cbPeriodo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgIncidentesR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidentesRVista)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(410, 44);
            this.label6.TabIndex = 47;
            this.label6.Text = "REPORTE DE INCIDENCIAS";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtgIncidentesR
            // 
            this.dtgIncidentesR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgIncidentesR.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgIncidentesR.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dtgIncidentesR.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.dtgIncidentesR.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.dtgIncidentesR.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.dtgIncidentesR.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.dtgIncidentesR.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.dtgIncidentesR.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.dtgIncidentesR.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.dtgIncidentesR.Location = new System.Drawing.Point(0, 166);
            this.dtgIncidentesR.LookAndFeel.SkinName = "Office 2013 Dark Gray";
            this.dtgIncidentesR.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgIncidentesR.MainView = this.dgvIncidentesRVista;
            this.dtgIncidentesR.Name = "dtgIncidentesR";
            this.dtgIncidentesR.Size = new System.Drawing.Size(410, 260);
            this.dtgIncidentesR.TabIndex = 136;
            this.dtgIncidentesR.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvIncidentesRVista});
            // 
            // dgvIncidentesRVista
            // 
            this.dgvIncidentesRVista.GridControl = this.dtgIncidentesR;
            this.dgvIncidentesRVista.Name = "dgvIncidentesRVista";
            this.dgvIncidentesRVista.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvIncidentesRVista.OptionsBehavior.Editable = false;
            this.dgvIncidentesRVista.OptionsView.ColumnAutoWidth = false;
            this.dgvIncidentesRVista.OptionsView.ShowFooter = true;
            this.dgvIncidentesRVista.OptionsView.ShowGroupPanel = false;
            // 
            // dtpPeriodo
            // 
            this.dtpPeriodo.CalendarForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtpPeriodo.CustomFormat = "MMyyyy";
            this.dtpPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPeriodo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodo.Location = new System.Drawing.Point(137, 59);
            this.dtpPeriodo.Name = "dtpPeriodo";
            this.dtpPeriodo.ShowUpDown = true;
            this.dtpPeriodo.Size = new System.Drawing.Size(87, 24);
            this.dtpPeriodo.TabIndex = 137;
            this.dtpPeriodo.Value = new System.DateTime(2024, 8, 23, 0, 0, 0, 0);
            this.dtpPeriodo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpPeriodo_KeyPress);
            // 
            // rbIncidenciasOperacion
            // 
            this.rbIncidenciasOperacion.AutoSize = true;
            this.rbIncidenciasOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbIncidenciasOperacion.Location = new System.Drawing.Point(23, 130);
            this.rbIncidenciasOperacion.Name = "rbIncidenciasOperacion";
            this.rbIncidenciasOperacion.Size = new System.Drawing.Size(207, 20);
            this.rbIncidenciasOperacion.TabIndex = 222;
            this.rbIncidenciasOperacion.TabStop = true;
            this.rbIncidenciasOperacion.Text = "N° de Incidencias X Operación";
            this.rbIncidenciasOperacion.UseVisualStyleBackColor = true;
            this.rbIncidenciasOperacion.Click += new System.EventHandler(this.rbIncidenciasOperacion_Click);
            // 
            // rbIncidenciasPersona
            // 
            this.rbIncidenciasPersona.AutoSize = true;
            this.rbIncidenciasPersona.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbIncidenciasPersona.Location = new System.Drawing.Point(23, 99);
            this.rbIncidenciasPersona.Name = "rbIncidenciasPersona";
            this.rbIncidenciasPersona.Size = new System.Drawing.Size(205, 20);
            this.rbIncidenciasPersona.TabIndex = 221;
            this.rbIncidenciasPersona.TabStop = true;
            this.rbIncidenciasPersona.Text = "N° de Incidencias X Conductor";
            this.rbIncidenciasPersona.UseVisualStyleBackColor = true;
            this.rbIncidenciasPersona.Click += new System.EventHandler(this.rbIncidenciasPersona_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(346, 103);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(51, 47);
            this.btnExcel.TabIndex = 223;
            this.btnExcel.Tag = "6";
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // cbPeriodo
            // 
            this.cbPeriodo.AutoSize = true;
            this.cbPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.cbPeriodo.Location = new System.Drawing.Point(23, 61);
            this.cbPeriodo.Name = "cbPeriodo";
            this.cbPeriodo.Size = new System.Drawing.Size(108, 22);
            this.cbPeriodo.TabIndex = 224;
            this.cbPeriodo.Text = "PERIODO:";
            this.cbPeriodo.UseVisualStyleBackColor = true;
            this.cbPeriodo.CheckedChanged += new System.EventHandler(this.cbPeriodo_CheckedChanged);
            // 
            // frmReporteIncidencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(410, 426);
            this.Controls.Add(this.cbPeriodo);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.rbIncidenciasOperacion);
            this.Controls.Add(this.rbIncidenciasPersona);
            this.Controls.Add(this.dtpPeriodo);
            this.Controls.Add(this.dtgIncidentesR);
            this.Controls.Add(this.label6);
            this.Name = "frmReporteIncidencias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTE DE INCIDENCIAS";
            this.Load += new System.EventHandler(this.frmReporteIncidencias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgIncidentesR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidentesRVista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl dtgIncidentesR;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvIncidentesRVista;
        private System.Windows.Forms.DateTimePicker dtpPeriodo;
        public System.Windows.Forms.RadioButton rbIncidenciasOperacion;
        public System.Windows.Forms.RadioButton rbIncidenciasPersona;
        public DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.CheckBox cbPeriodo;
    }
}