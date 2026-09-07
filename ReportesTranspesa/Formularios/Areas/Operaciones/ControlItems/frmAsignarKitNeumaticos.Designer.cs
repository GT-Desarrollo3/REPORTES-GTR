namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    partial class frmAsignarKitNeumaticos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarKitNeumaticos));
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lstEmpleado = new System.Windows.Forms.ListView();
            this.btnAsignar = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDevolver = new System.Windows.Forms.Button();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.dgvItems = new DevExpress.XtraGrid.GridControl();
            this.dgvItemsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnBuscarHtta = new System.Windows.Forms.Button();
            this.txtHtta = new System.Windows.Forms.TextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsHttasD = new System.Windows.Forms.ToolStripLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTracto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstTracto = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsView)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(829, 40);
            this.label2.TabIndex = 21;
            this.label2.Text = "ASIGNACIÓN DE KIT DE NEUMÁTICOS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(227, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 16);
            this.label8.TabIndex = 230;
            this.label8.Text = "Empleado:";
            // 
            // lstEmpleado
            // 
            this.lstEmpleado.BackColor = System.Drawing.Color.SeaShell;
            this.lstEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lstEmpleado.FullRowSelect = true;
            this.lstEmpleado.GridLines = true;
            this.lstEmpleado.Location = new System.Drawing.Point(307, 86);
            this.lstEmpleado.MultiSelect = false;
            this.lstEmpleado.Name = "lstEmpleado";
            this.lstEmpleado.Size = new System.Drawing.Size(353, 144);
            this.lstEmpleado.TabIndex = 232;
            this.lstEmpleado.UseCompatibleStateImageBehavior = false;
            this.lstEmpleado.View = System.Windows.Forms.View.Details;
            this.lstEmpleado.Visible = false;
            this.lstEmpleado.Enter += new System.EventHandler(this.lstEmpleado_Enter);
            this.lstEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstEmpleado_KeyPress);
            this.lstEmpleado.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEmpleado_MouseDoubleClick);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Appearance.BackColor = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnAsignar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnAsignar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignar.Appearance.Options.UseBackColor = true;
            this.btnAsignar.Appearance.Options.UseBorderColor = true;
            this.btnAsignar.Appearance.Options.UseFont = true;
            this.btnAsignar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignar.Image")));
            this.btnAsignar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAsignar.Location = new System.Drawing.Point(711, 18);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(92, 38);
            this.btnAsignar.TabIndex = 231;
            this.btnAsignar.Tag = "5";
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.ToolTip = "Asignar";
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpleado.Location = new System.Drawing.Point(307, 25);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(353, 22);
            this.txtEmpleado.TabIndex = 229;
            this.txtEmpleado.Enter += new System.EventHandler(this.txtEmpleado_Enter);
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            this.txtEmpleado.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmpleado_KeyUp);
            this.txtEmpleado.Leave += new System.EventHandler(this.txtEmpleado_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Controls.Add(this.btnDevolver);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.dgvItems);
            this.panel1.Controls.Add(this.btnBuscarHtta);
            this.panel1.Controls.Add(this.txtHtta);
            this.panel1.Controls.Add(this.toolStrip3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(829, 379);
            this.panel1.TabIndex = 233;
            // 
            // btnDevolver
            // 
            this.btnDevolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDevolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDevolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDevolver.Location = new System.Drawing.Point(526, 1);
            this.btnDevolver.Name = "btnDevolver";
            this.btnDevolver.Size = new System.Drawing.Size(90, 23);
            this.btnDevolver.TabIndex = 231;
            this.btnDevolver.Text = "<<  Quitar Htta.";
            this.btnDevolver.UseVisualStyleBackColor = false;
            this.btnDevolver.Visible = false;
            this.btnDevolver.Click += new System.EventHandler(this.btnDevolver_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Appearance.Options.UseBackColor = true;
            this.btnExcel.Appearance.Options.UseBorderColor = true;
            this.btnExcel.Appearance.Options.UseFont = true;
            this.btnExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExcel.Location = new System.Drawing.Point(485, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(22, 20);
            this.btnExcel.TabIndex = 119;
            this.btnExcel.ToolTip = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dgvItems
            // 
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 25);
            this.dgvItems.LookAndFeel.SkinName = "Black";
            this.dgvItems.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dgvItems.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgvItems.MainView = this.dgvItemsView;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(829, 354);
            this.dgvItems.TabIndex = 118;
            this.dgvItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvItemsView});
            // 
            // dgvItemsView
            // 
            this.dgvItemsView.GridControl = this.dgvItems;
            this.dgvItemsView.Name = "dgvItemsView";
            this.dgvItemsView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgvItemsView.OptionsBehavior.Editable = false;
            this.dgvItemsView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.dgvItemsView.OptionsSelection.MultiSelect = true;
            this.dgvItemsView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.dgvItemsView.OptionsView.ColumnAutoWidth = false;
            this.dgvItemsView.OptionsView.ShowFooter = true;
            this.dgvItemsView.OptionsView.ShowGroupPanel = false;
            // 
            // btnBuscarHtta
            // 
            this.btnBuscarHtta.Image = global::ReportesTranspesa.Properties.Resources.view_search_find_9565;
            this.btnBuscarHtta.Location = new System.Drawing.Point(456, 3);
            this.btnBuscarHtta.Name = "btnBuscarHtta";
            this.btnBuscarHtta.Size = new System.Drawing.Size(24, 20);
            this.btnBuscarHtta.TabIndex = 117;
            this.btnBuscarHtta.UseVisualStyleBackColor = true;
            this.btnBuscarHtta.Click += new System.EventHandler(this.btnBuscarHtta_Click);
            // 
            // txtHtta
            // 
            this.txtHtta.Location = new System.Drawing.Point(237, 3);
            this.txtHtta.Name = "txtHtta";
            this.txtHtta.Size = new System.Drawing.Size(213, 20);
            this.txtHtta.TabIndex = 116;
            this.txtHtta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHtta_KeyPress);
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.DarkRed;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsHttasD});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(829, 25);
            this.toolStrip3.TabIndex = 115;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsHttasD
            // 
            this.tsHttasD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsHttasD.ForeColor = System.Drawing.Color.White;
            this.tsHttasD.Name = "tsHttasD";
            this.tsHttasD.Size = new System.Drawing.Size(150, 22);
            this.tsHttasD.Text = "Herramientas disponibles:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtTracto);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtEmpleado);
            this.panel2.Controls.Add(this.btnAsignar);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(829, 74);
            this.panel2.TabIndex = 234;
            // 
            // txtTracto
            // 
            this.txtTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTracto.Location = new System.Drawing.Point(73, 25);
            this.txtTracto.Name = "txtTracto";
            this.txtTracto.Size = new System.Drawing.Size(123, 22);
            this.txtTracto.TabIndex = 233;
            this.txtTracto.Enter += new System.EventHandler(this.txtTracto_Enter);
            this.txtTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTracto_KeyPress);
            this.txtTracto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTracto_KeyUp);
            this.txtTracto.Leave += new System.EventHandler(this.txtTracto_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 232;
            this.label1.Text = "Tracto:";
            // 
            // lstTracto
            // 
            this.lstTracto.BackColor = System.Drawing.Color.SeaShell;
            this.lstTracto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTracto.ForeColor = System.Drawing.Color.Navy;
            this.lstTracto.FullRowSelect = true;
            this.lstTracto.GridLines = true;
            this.lstTracto.Location = new System.Drawing.Point(73, 86);
            this.lstTracto.MultiSelect = false;
            this.lstTracto.Name = "lstTracto";
            this.lstTracto.Size = new System.Drawing.Size(353, 144);
            this.lstTracto.TabIndex = 233;
            this.lstTracto.UseCompatibleStateImageBehavior = false;
            this.lstTracto.View = System.Windows.Forms.View.Details;
            this.lstTracto.Visible = false;
            this.lstTracto.Enter += new System.EventHandler(this.lstTracto_Enter);
            this.lstTracto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstTracto_KeyPress);
            this.lstTracto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTracto_MouseDoubleClick);
            // 
            // frmAsignarKitNeumaticos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(829, 493);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstEmpleado);
            this.Controls.Add(this.lstTracto);
            this.Name = "frmAsignarKitNeumaticos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASIGNAR KIT DE NEUMÁTICOS";
            this.Load += new System.EventHandler(this.frmAsignarKitNeumaticos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsView)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView lstEmpleado;
        public DevExpress.XtraEditors.SimpleButton btnAsignar;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl dgvItems;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvItemsView;
        private System.Windows.Forms.Button btnBuscarHtta;
        private System.Windows.Forms.TextBox txtHtta;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel tsHttasD;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.Button btnDevolver;
        private System.Windows.Forms.TextBox txtTracto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstTracto;
    }
}