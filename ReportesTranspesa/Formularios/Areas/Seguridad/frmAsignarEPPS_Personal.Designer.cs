namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    partial class frmAsignarEPPS_Personal
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPersonal = new System.Windows.Forms.TextBox();
            this.dtgPersonalData = new DevExpress.XtraGrid.GridControl();
            this.dgvExpressVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgListaEPPS = new System.Windows.Forms.DataGridView();
            this.Marca = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoEPP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoriaVidaUtil = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AreaProceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MesesVidaUtil = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPersonalData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaEPPS)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtPersonal);
            this.panel2.Controls.Add(this.dtgPersonalData);
            this.panel2.Location = new System.Drawing.Point(-1, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 551);
            this.panel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Nombre de Empleado:";
            // 
            // txtPersonal
            // 
            this.txtPersonal.Location = new System.Drawing.Point(131, 17);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.Size = new System.Drawing.Size(346, 20);
            this.txtPersonal.TabIndex = 0;
            this.txtPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPersonal_KeyPress);
            // 
            // dtgPersonalData
            // 
            this.dtgPersonalData.AllowDrop = true;
            this.dtgPersonalData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgPersonalData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgPersonalData.Location = new System.Drawing.Point(0, 53);
            this.dtgPersonalData.MainView = this.dgvExpressVista;
            this.dtgPersonalData.Name = "dtgPersonalData";
            this.dtgPersonalData.Size = new System.Drawing.Size(501, 498);
            this.dtgPersonalData.TabIndex = 11;
            this.dtgPersonalData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvExpressVista});
            this.dtgPersonalData.Click += new System.EventHandler(this.dtgPersonalData_Click);
            // 
            // dgvExpressVista
            // 
            this.dgvExpressVista.GridControl = this.dtgPersonalData;
            this.dgvExpressVista.Name = "dgvExpressVista";
            this.dgvExpressVista.OptionsBehavior.Editable = false;
            this.dgvExpressVista.OptionsView.RowAutoHeight = true;
            // 
            // dtgListaEPPS
            // 
            this.dtgListaEPPS.AllowDrop = true;
            this.dtgListaEPPS.AllowUserToAddRows = false;
            this.dtgListaEPPS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgListaEPPS.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgListaEPPS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgListaEPPS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgListaEPPS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Marca,
            this.Numero,
            this.ID,
            this.TipoEPP,
            this.CategoriaVidaUtil,
            this.AreaProceso,
            this.MesesVidaUtil});
            this.dtgListaEPPS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgListaEPPS.Location = new System.Drawing.Point(0, 0);
            this.dtgListaEPPS.Name = "dtgListaEPPS";
            this.dtgListaEPPS.ReadOnly = true;
            this.dtgListaEPPS.RowHeadersVisible = false;
            this.dtgListaEPPS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgListaEPPS.Size = new System.Drawing.Size(502, 500);
            this.dtgListaEPPS.TabIndex = 11;
            this.dtgListaEPPS.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgListaEPPS_CellContentClick);
            // 
            // Marca
            // 
            this.Marca.HeaderText = "Marca";
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            this.Marca.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Marca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Numero
            // 
            this.Numero.HeaderText = "Numero";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            this.Numero.Visible = false;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // TipoEPP
            // 
            this.TipoEPP.HeaderText = "TipoEPP";
            this.TipoEPP.Name = "TipoEPP";
            this.TipoEPP.ReadOnly = true;
            // 
            // CategoriaVidaUtil
            // 
            this.CategoriaVidaUtil.HeaderText = "CategoriaVidaUtil";
            this.CategoriaVidaUtil.Name = "CategoriaVidaUtil";
            this.CategoriaVidaUtil.ReadOnly = true;
            // 
            // AreaProceso
            // 
            this.AreaProceso.HeaderText = "AreaProceso";
            this.AreaProceso.Name = "AreaProceso";
            this.AreaProceso.ReadOnly = true;
            // 
            // MesesVidaUtil
            // 
            this.MesesVidaUtil.HeaderText = "MesesVidaUtil";
            this.MesesVidaUtil.Name = "MesesVidaUtil";
            this.MesesVidaUtil.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtgListaEPPS);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Location = new System.Drawing.Point(499, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(502, 551);
            this.panel3.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.AllowDrop = true;
            this.panel5.Controls.Add(this.btnGuardar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 500);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(502, 51);
            this.panel5.TabIndex = 12;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::ReportesTranspesa.Properties.Resources.Icon_Save1;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(381, 6);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(109, 37);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "           ASIGNAR";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1001, 41);
            this.label1.TabIndex = 4;
            this.label1.Text = "ASIGNAR EPP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAsignarEPPS_Personal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 592);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmAsignarEPPS_Personal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar EPP al Personal";
            this.Load += new System.EventHandler(this.frmAsignarEPPS_Personal_Load);
            this.Shown += new System.EventHandler(this.frmAsignarEPPS_Personal_Shown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPersonalData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpressVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaEPPS)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPersonal;
        private DevExpress.XtraGrid.GridControl dtgPersonalData;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvExpressVista;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.DataGridView dtgListaEPPS;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoEPP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoriaVidaUtil;
        private System.Windows.Forms.DataGridViewTextBoxColumn AreaProceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn MesesVidaUtil;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}