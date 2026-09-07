namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.ConstanciaNoDeudo
{
    partial class frmConstanciaNoDeudo
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCese = new MetroFramework.Controls.MetroRadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.rbVacaciones = new MetroFramework.Controls.MetroRadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOperacion = new MetroFramework.Controls.MetroTextBox();
            this.lvEmpleado = new System.Windows.Forms.ListView();
            this.txtEmpleado = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumero = new MetroFramework.Controls.MetroTextBox();
            this.txtIdEmpleado = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgvData = new DevExpress.XtraGrid.GridControl();
            this.dtgvDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtNumero);
            this.splitContainer1.Panel1.Controls.Add(this.txtIdEmpleado);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvData);
            this.splitContainer1.Size = new System.Drawing.Size(831, 679);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(642, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(177, 64);
            this.groupBox3.TabIndex = 107;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Alertar por Correo";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Image = global::ReportesTranspesa.Properties.Resources.updated1;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.Location = new System.Drawing.Point(96, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 40);
            this.button3.TabIndex = 105;
            this.button3.Text = "a Todos";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Image = global::ReportesTranspesa.Properties.Resources.updated1;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(6, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 40);
            this.button2.TabIndex = 104;
            this.button2.Text = "Solo Pendientes";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCese);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.rbVacaciones);
            this.groupBox2.Location = new System.Drawing.Point(640, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(179, 139);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones";
            // 
            // rbCese
            // 
            this.rbCese.Location = new System.Drawing.Point(26, 23);
            this.rbCese.Name = "rbCese";
            this.rbCese.Size = new System.Drawing.Size(132, 25);
            this.rbCese.TabIndex = 102;
            this.rbCese.Text = "CESE";
            this.rbCese.UseSelectable = true;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Image = global::ReportesTranspesa.Properties.Resources.doc_guia;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(26, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 40);
            this.button1.TabIndex = 99;
            this.button1.Text = "Emitir Constancia";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbVacaciones
            // 
            this.rbVacaciones.Location = new System.Drawing.Point(26, 57);
            this.rbVacaciones.Name = "rbVacaciones";
            this.rbVacaciones.Size = new System.Drawing.Size(132, 25);
            this.rbVacaciones.TabIndex = 103;
            this.rbVacaciones.Text = "VACACIONES";
            this.rbVacaciones.UseSelectable = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOperacion);
            this.groupBox1.Controls.Add(this.lvEmpleado);
            this.groupBox1.Controls.Add(this.txtEmpleado);
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 159);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Empleado:";
            // 
            // txtOperacion
            // 
            this.txtOperacion.Lines = new string[0];
            this.txtOperacion.Location = new System.Drawing.Point(20, 54);
            this.txtOperacion.MaxLength = 32767;
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.PasswordChar = '\0';
            this.txtOperacion.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOperacion.SelectedText = "";
            this.txtOperacion.Size = new System.Drawing.Size(188, 29);
            this.txtOperacion.Style = MetroFramework.MetroColorStyle.Red;
            this.txtOperacion.TabIndex = 86;
            this.txtOperacion.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOperacion.UseSelectable = true;
            // 
            // lvEmpleado
            // 
            this.lvEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lvEmpleado.ForeColor = System.Drawing.Color.Navy;
            this.lvEmpleado.FullRowSelect = true;
            this.lvEmpleado.GridLines = true;
            this.lvEmpleado.Location = new System.Drawing.Point(20, 47);
            this.lvEmpleado.MultiSelect = false;
            this.lvEmpleado.Name = "lvEmpleado";
            this.lvEmpleado.Size = new System.Drawing.Size(381, 105);
            this.lvEmpleado.TabIndex = 85;
            this.lvEmpleado.UseCompatibleStateImageBehavior = false;
            this.lvEmpleado.View = System.Windows.Forms.View.Details;
            this.lvEmpleado.Visible = false;
            this.lvEmpleado.SelectedIndexChanged += new System.EventHandler(this.lvEmpleado_SelectedIndexChanged);
            this.lvEmpleado.Enter += new System.EventHandler(this.lvEmpleado_Enter);
            this.lvEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvEmpleado_KeyPress);
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.Lines = new string[0];
            this.txtEmpleado.Location = new System.Drawing.Point(20, 19);
            this.txtEmpleado.MaxLength = 32767;
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.PasswordChar = '\0';
            this.txtEmpleado.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmpleado.SelectedText = "";
            this.txtEmpleado.Size = new System.Drawing.Size(381, 29);
            this.txtEmpleado.Style = MetroFramework.MetroColorStyle.Red;
            this.txtEmpleado.TabIndex = 84;
            this.txtEmpleado.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmpleado.UseSelectable = true;
            this.txtEmpleado.TextChanged += new System.EventHandler(this.txtEmpleado_TextChanged);
            this.txtEmpleado.Click += new System.EventHandler(this.txtEmpleado_Click);
            this.txtEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpleado_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "Pendientes:";
            // 
            // txtNumero
            // 
            this.txtNumero.Lines = new string[0];
            this.txtNumero.Location = new System.Drawing.Point(478, 52);
            this.txtNumero.MaxLength = 32767;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.PasswordChar = '\0';
            this.txtNumero.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNumero.SelectedText = "";
            this.txtNumero.Size = new System.Drawing.Size(36, 29);
            this.txtNumero.Style = MetroFramework.MetroColorStyle.Red;
            this.txtNumero.TabIndex = 100;
            this.txtNumero.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNumero.UseSelectable = true;
            this.txtNumero.Visible = false;
            // 
            // txtIdEmpleado
            // 
            this.txtIdEmpleado.Lines = new string[0];
            this.txtIdEmpleado.Location = new System.Drawing.Point(441, 52);
            this.txtIdEmpleado.MaxLength = 32767;
            this.txtIdEmpleado.Name = "txtIdEmpleado";
            this.txtIdEmpleado.PasswordChar = '\0';
            this.txtIdEmpleado.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtIdEmpleado.SelectedText = "";
            this.txtIdEmpleado.Size = new System.Drawing.Size(31, 29);
            this.txtIdEmpleado.Style = MetroFramework.MetroColorStyle.Red;
            this.txtIdEmpleado.TabIndex = 98;
            this.txtIdEmpleado.UseSelectable = true;
            this.txtIdEmpleado.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(831, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Verificación de NO ADEUDO";
            // 
            // dtgvData
            // 
            this.dtgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvData.Location = new System.Drawing.Point(0, 0);
            this.dtgvData.LookAndFeel.SkinName = "Office 2007 Blue";
            this.dtgvData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgvData.MainView = this.dtgvDataView;
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(831, 416);
            this.dtgvData.TabIndex = 3;
            this.dtgvData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgvDataView});
            // 
            // dtgvDataView
            // 
            this.dtgvDataView.GridControl = this.dtgvData;
            this.dtgvDataView.Name = "dtgvDataView";
            this.dtgvDataView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgvDataView.OptionsBehavior.Editable = false;
            this.dtgvDataView.OptionsView.ColumnAutoWidth = false;
            this.dtgvDataView.OptionsView.ShowFooter = true;
            this.dtgvDataView.OptionsView.ShowGroupPanel = false;
            // 
            // frmConstanciaNoDeudo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(831, 679);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConstanciaNoDeudo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmConstanciaNoDeudo";
            this.Load += new System.EventHandler(this.frmConstanciaNoDeudo_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox txtEmpleado;
        private System.Windows.Forms.ListView lvEmpleado;
        private MetroFramework.Controls.MetroTextBox txtIdEmpleado;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.GridControl dtgvData;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgvDataView;
        private MetroFramework.Controls.MetroTextBox txtNumero;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroRadioButton rbVacaciones;
        private MetroFramework.Controls.MetroRadioButton rbCese;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private MetroFramework.Controls.MetroTextBox txtOperacion;
    }
}