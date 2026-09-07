namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    partial class FrmLiquidacionPlanilla
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLiquidacionPlanilla));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvAdelantoGastos = new System.Windows.Forms.DataGridView();
            this.cmLiquidarPlanilla = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.liquidarPlanillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Label4 = new System.Windows.Forms.Label();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnReportes = new System.Windows.Forms.ToolStripButton();
            this.Label25 = new System.Windows.Forms.Label();
            this.dgvLiquidacion = new System.Windows.Forms.DataGridView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.gbConductor = new System.Windows.Forms.GroupBox();
            this.dgvconductor = new System.Windows.Forms.DataGridView();
            this.txtconductor = new System.Windows.Forms.TextBox();
            this.btnBuscarConductor = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.tsCbxCompaniaSocio = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsTxtUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdelantoGastos)).BeginInit();
            this.cmLiquidarPlanilla.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiquidacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.gbConductor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvconductor)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvAdelantoGastos);
            this.splitContainer1.Panel1.Controls.Add(this.Label4);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Label25);
            this.splitContainer1.Panel2.Controls.Add(this.dgvLiquidacion);
            this.splitContainer1.Size = new System.Drawing.Size(903, 581);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 11;
            // 
            // dgvAdelantoGastos
            // 
            this.dgvAdelantoGastos.AllowUserToAddRows = false;
            this.dgvAdelantoGastos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAdelantoGastos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdelantoGastos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAdelantoGastos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAdelantoGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdelantoGastos.ContextMenuStrip = this.cmLiquidarPlanilla;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAdelantoGastos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAdelantoGastos.Location = new System.Drawing.Point(3, 55);
            this.dgvAdelantoGastos.MultiSelect = false;
            this.dgvAdelantoGastos.Name = "dgvAdelantoGastos";
            this.dgvAdelantoGastos.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAdelantoGastos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAdelantoGastos.RowHeadersVisible = false;
            this.dgvAdelantoGastos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdelantoGastos.ShowCellErrors = false;
            this.dgvAdelantoGastos.ShowRowErrors = false;
            this.dgvAdelantoGastos.Size = new System.Drawing.Size(897, 200);
            this.dgvAdelantoGastos.TabIndex = 0;
            // 
            // cmLiquidarPlanilla
            // 
            this.cmLiquidarPlanilla.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liquidarPlanillaToolStripMenuItem});
            this.cmLiquidarPlanilla.Name = "cmLiquidarPlanilla";
            this.cmLiquidarPlanilla.Size = new System.Drawing.Size(159, 26);
            // 
            // liquidarPlanillaToolStripMenuItem
            // 
            this.liquidarPlanillaToolStripMenuItem.Name = "liquidarPlanillaToolStripMenuItem";
            this.liquidarPlanillaToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.liquidarPlanillaToolStripMenuItem.Text = "Liquidar Planilla";
            this.liquidarPlanillaToolStripMenuItem.Click += new System.EventHandler(this.liquidarPlanillaToolStripMenuItem_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(3, 35);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(188, 13);
            this.Label4.TabIndex = 12;
            this.Label4.Text = "PLANILLA (Adelanto de Gastos)";
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackColor = System.Drawing.Color.DarkKhaki;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnNuevo,
            this.tsBtnReportes});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(903, 29);
            this.toolStrip3.TabIndex = 47;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsBtnNuevo
            // 
            this.tsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNuevo.Image")));
            this.tsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNuevo.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.tsBtnNuevo.Name = "tsBtnNuevo";
            this.tsBtnNuevo.Size = new System.Drawing.Size(62, 26);
            this.tsBtnNuevo.Text = "&Nuevo";
            this.tsBtnNuevo.Click += new System.EventHandler(this.tsBtnNuevo_Click);
            // 
            // tsBtnReportes
            // 
            this.tsBtnReportes.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnReportes.Image")));
            this.tsBtnReportes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnReportes.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.tsBtnReportes.Name = "tsBtnReportes";
            this.tsBtnReportes.Size = new System.Drawing.Size(92, 26);
            this.tsBtnReportes.Text = "Ver &Reportes";
            this.tsBtnReportes.Click += new System.EventHandler(this.tsBtnReportes_Click);
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(13, 9);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(194, 13);
            this.Label25.TabIndex = 27;
            this.Label25.Text = "LIQUIDACIONES DE PLANILLAS";
            // 
            // dgvLiquidacion
            // 
            this.dgvLiquidacion.AllowUserToAddRows = false;
            this.dgvLiquidacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLiquidacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLiquidacion.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvLiquidacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLiquidacion.Location = new System.Drawing.Point(6, 25);
            this.dgvLiquidacion.MultiSelect = false;
            this.dgvLiquidacion.Name = "dgvLiquidacion";
            this.dgvLiquidacion.ReadOnly = true;
            this.dgvLiquidacion.RowHeadersVisible = false;
            this.dgvLiquidacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLiquidacion.ShowCellErrors = false;
            this.dgvLiquidacion.ShowRowErrors = false;
            this.dgvLiquidacion.Size = new System.Drawing.Size(894, 291);
            this.dgvLiquidacion.TabIndex = 17;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 47);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.gbConductor);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer.Size = new System.Drawing.Size(1202, 587);
            this.splitContainer.SplitterDistance = 289;
            this.splitContainer.TabIndex = 18;
            // 
            // gbConductor
            // 
            this.gbConductor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConductor.Controls.Add(this.dgvconductor);
            this.gbConductor.Controls.Add(this.txtconductor);
            this.gbConductor.Controls.Add(this.btnBuscarConductor);
            this.gbConductor.Location = new System.Drawing.Point(3, 3);
            this.gbConductor.Name = "gbConductor";
            this.gbConductor.Size = new System.Drawing.Size(283, 581);
            this.gbConductor.TabIndex = 11;
            this.gbConductor.TabStop = false;
            this.gbConductor.Text = "CONDUCTOR";
            // 
            // dgvconductor
            // 
            this.dgvconductor.AllowUserToAddRows = false;
            this.dgvconductor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvconductor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvconductor.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvconductor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvconductor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvconductor.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvconductor.GridColor = System.Drawing.SystemColors.Control;
            this.dgvconductor.Location = new System.Drawing.Point(6, 45);
            this.dgvconductor.MultiSelect = false;
            this.dgvconductor.Name = "dgvconductor";
            this.dgvconductor.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvconductor.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvconductor.RowHeadersVisible = false;
            this.dgvconductor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvconductor.ShowCellErrors = false;
            this.dgvconductor.ShowRowErrors = false;
            this.dgvconductor.Size = new System.Drawing.Size(273, 530);
            this.dgvconductor.TabIndex = 10;
            this.dgvconductor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvconductor_CellClick);
            this.dgvconductor.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvconductor_CellClick);
            this.dgvconductor.CellContextMenuStripChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvconductor_CellContextMenuStripChanged);
            this.dgvconductor.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvconductor_CellMouseClick);
            this.dgvconductor.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvconductor_CellValueChanged);
            // 
            // txtconductor
            // 
            this.txtconductor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtconductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconductor.Location = new System.Drawing.Point(7, 19);
            this.txtconductor.MaxLength = 60;
            this.txtconductor.Name = "txtconductor";
            this.txtconductor.Size = new System.Drawing.Size(209, 18);
            this.txtconductor.TabIndex = 8;
            // 
            // btnBuscarConductor
            // 
            this.btnBuscarConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnBuscarConductor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarConductor.Location = new System.Drawing.Point(219, 17);
            this.btnBuscarConductor.Name = "btnBuscarConductor";
            this.btnBuscarConductor.Size = new System.Drawing.Size(58, 22);
            this.btnBuscarConductor.TabIndex = 9;
            this.btnBuscarConductor.Text = "Buscar";
            this.btnBuscarConductor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarConductor.UseVisualStyleBackColor = true;
            this.btnBuscarConductor.Click += new System.EventHandler(this.btnBuscarConductor_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.DarkKhaki;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnSalir,
            this.tsCbxCompaniaSocio});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1202, 44);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(63, 41);
            this.tsBtnSalir.Text = "Salir";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // tsCbxCompaniaSocio
            // 
            this.tsCbxCompaniaSocio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsCbxCompaniaSocio.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tsCbxCompaniaSocio.Name = "tsCbxCompaniaSocio";
            this.tsCbxCompaniaSocio.Size = new System.Drawing.Size(250, 44);
            this.tsCbxCompaniaSocio.SelectedIndexChanged += new System.EventHandler(this.tsCbxCompaniaSocio_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblUsuario,
            this.tsTxtUsuario});
            this.statusStrip1.Location = new System.Drawing.Point(0, 637);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1202, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblUsuario
            // 
            this.tslblUsuario.AutoSize = false;
            this.tslblUsuario.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.tslblUsuario.Name = "tslblUsuario";
            this.tslblUsuario.Size = new System.Drawing.Size(47, 19);
            this.tslblUsuario.Text = "Usuario";
            // 
            // tsTxtUsuario
            // 
            this.tsTxtUsuario.Name = "tsTxtUsuario";
            this.tsTxtUsuario.Size = new System.Drawing.Size(0, 17);
            // 
            // FrmLiquidacionPlanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1202, 659);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmLiquidacionPlanilla";
            this.Text = "Liquidación de Planilla";
            this.Load += new System.EventHandler(this.FrmLiquidacionPlanilla_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdelantoGastos)).EndInit();
            this.cmLiquidarPlanilla.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiquidacion)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.gbConductor.ResumeLayout(false);
            this.gbConductor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvconductor)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.Button btnBuscarConductor;
        internal System.Windows.Forms.TextBox txtconductor;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.DataGridView dgvLiquidacion;
        internal System.Windows.Forms.Label Label25;
        internal System.Windows.Forms.DataGridView dgvAdelantoGastos;
        private System.Windows.Forms.SplitContainer splitContainer;
        internal System.Windows.Forms.DataGridView dgvconductor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.GroupBox gbConductor;
        private System.Windows.Forms.ContextMenuStrip cmLiquidarPlanilla;
        private System.Windows.Forms.ToolStripMenuItem liquidarPlanillaToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnReportes;
        private System.Windows.Forms.ToolStripStatusLabel tslblUsuario;
        private System.Windows.Forms.ToolStripStatusLabel tsTxtUsuario;
        private System.Windows.Forms.ToolStripComboBox tsCbxCompaniaSocio;
    }
}