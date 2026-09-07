namespace ReportesTranspesa.Formularios.Areas.Seguridad.GestionSeguridad
{
    partial class frmMaestroObjetivos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaestroObjetivos));
            this.label1 = new System.Windows.Forms.Label();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabOEstrategico = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBuscarObjetivoE = new System.Windows.Forms.TextBox();
            this.dtgOEstrategicos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarOEstrategicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvOEstrategicosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGuardarE = new DevExpress.XtraEditors.SimpleButton();
            this.txtObjetivoE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabOOperativos = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBuscarObjetivoO = new System.Windows.Forms.TextBox();
            this.dtgOOperativos = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.desvincularOEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarOOperativoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvOOperativosVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dtgListaObjetivosE = new System.Windows.Forms.DataGridView();
            this.Marca = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idObjetivoE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjetivoE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGuardarO = new DevExpress.XtraEditors.SimpleButton();
            this.txtObjetivoO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabActividades = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBuscarActividad = new System.Windows.Forms.TextBox();
            this.dtgActividades = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.desvincularOOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarActividadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvActividadesVista = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label7 = new System.Windows.Forms.Label();
            this.dtgListaObjetivosO = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnActividad = new DevExpress.XtraEditors.SimpleButton();
            this.txtActividad = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Marca2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idObjetivoO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjetivoO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vincularOEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vincularOOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabInfo.SuspendLayout();
            this.tabOEstrategico.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOEstrategicos)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOEstrategicosVista)).BeginInit();
            this.tabOOperativos.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOOperativos)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOOperativosVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaObjetivosE)).BeginInit();
            this.tabActividades.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgActividades)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActividadesVista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaObjetivosO)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1008, 43);
            this.label1.TabIndex = 15;
            this.label1.Text = "REGISTRO DE OBJETIVOS Y ACTIVIDADES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabOEstrategico);
            this.tabInfo.Controls.Add(this.tabOOperativos);
            this.tabInfo.Controls.Add(this.tabActividades);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabInfo.Location = new System.Drawing.Point(0, 43);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(1008, 534);
            this.tabInfo.TabIndex = 211;
            // 
            // tabOEstrategico
            // 
            this.tabOEstrategico.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabOEstrategico.Controls.Add(this.panel1);
            this.tabOEstrategico.Controls.Add(this.label3);
            this.tabOEstrategico.Controls.Add(this.btnGuardarE);
            this.tabOEstrategico.Controls.Add(this.txtObjetivoE);
            this.tabOEstrategico.Controls.Add(this.label5);
            this.tabOEstrategico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabOEstrategico.Location = new System.Drawing.Point(4, 33);
            this.tabOEstrategico.Name = "tabOEstrategico";
            this.tabOEstrategico.Padding = new System.Windows.Forms.Padding(3);
            this.tabOEstrategico.Size = new System.Drawing.Size(1000, 497);
            this.tabOEstrategico.TabIndex = 0;
            this.tabOEstrategico.Text = "Objetivos Estratégicos";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtBuscarObjetivoE);
            this.panel1.Controls.Add(this.dtgOEstrategicos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(430, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 491);
            this.panel1.TabIndex = 127;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 124;
            this.label10.Text = "Buscar:";
            // 
            // txtBuscarObjetivoE
            // 
            this.txtBuscarObjetivoE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarObjetivoE.Location = new System.Drawing.Point(68, 14);
            this.txtBuscarObjetivoE.Name = "txtBuscarObjetivoE";
            this.txtBuscarObjetivoE.Size = new System.Drawing.Size(340, 20);
            this.txtBuscarObjetivoE.TabIndex = 113;
            this.txtBuscarObjetivoE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarObjetivoE_KeyPress);
            // 
            // dtgOEstrategicos
            // 
            this.dtgOEstrategicos.CausesValidation = false;
            this.dtgOEstrategicos.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgOEstrategicos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgOEstrategicos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgOEstrategicos.Location = new System.Drawing.Point(0, 48);
            this.dtgOEstrategicos.MainView = this.dgvOEstrategicosVista;
            this.dtgOEstrategicos.Name = "dtgOEstrategicos";
            this.dtgOEstrategicos.Size = new System.Drawing.Size(567, 443);
            this.dtgOEstrategicos.TabIndex = 106;
            this.dtgOEstrategicos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvOEstrategicosVista});
            this.dtgOEstrategicos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgOEstrategicos_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarOEstrategicoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(194, 26);
            // 
            // eliminarOEstrategicoToolStripMenuItem
            // 
            this.eliminarOEstrategicoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarOEstrategicoToolStripMenuItem.Name = "eliminarOEstrategicoToolStripMenuItem";
            this.eliminarOEstrategicoToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.eliminarOEstrategicoToolStripMenuItem.Text = "Eliminar O. Estratégico";
            this.eliminarOEstrategicoToolStripMenuItem.Click += new System.EventHandler(this.eliminarOEstrategicoToolStripMenuItem_Click);
            // 
            // dgvOEstrategicosVista
            // 
            this.dgvOEstrategicosVista.GridControl = this.dtgOEstrategicos;
            this.dgvOEstrategicosVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvOEstrategicosVista.Name = "dgvOEstrategicosVista";
            this.dgvOEstrategicosVista.OptionsBehavior.Editable = false;
            this.dgvOEstrategicosVista.OptionsBehavior.ReadOnly = true;
            this.dgvOEstrategicosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvOEstrategicosVista.OptionsView.RowAutoHeight = true;
            this.dgvOEstrategicosVista.OptionsView.ShowFooter = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(331, 20);
            this.label3.TabIndex = 126;
            this.label3.Text = "INGRESAR OBJETIVO ESTRATÉGICO:";
            // 
            // btnGuardarE
            // 
            this.btnGuardarE.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardarE.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardarE.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardarE.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarE.Appearance.Options.UseBackColor = true;
            this.btnGuardarE.Appearance.Options.UseBorderColor = true;
            this.btnGuardarE.Appearance.Options.UseFont = true;
            this.btnGuardarE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarE.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarE.Image")));
            this.btnGuardarE.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardarE.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGuardarE.Location = new System.Drawing.Point(152, 439);
            this.btnGuardarE.Name = "btnGuardarE";
            this.btnGuardarE.Size = new System.Drawing.Size(114, 41);
            this.btnGuardarE.TabIndex = 125;
            this.btnGuardarE.Text = "  Guardar";
            this.btnGuardarE.Click += new System.EventHandler(this.btnGuardarE_Click);
            // 
            // txtObjetivoE
            // 
            this.txtObjetivoE.BackColor = System.Drawing.Color.White;
            this.txtObjetivoE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObjetivoE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjetivoE.Location = new System.Drawing.Point(23, 80);
            this.txtObjetivoE.MaxLength = 250;
            this.txtObjetivoE.Multiline = true;
            this.txtObjetivoE.Name = "txtObjetivoE";
            this.txtObjetivoE.Size = new System.Drawing.Size(377, 93);
            this.txtObjetivoE.TabIndex = 124;
            this.txtObjetivoE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObjetivoE_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 123;
            this.label5.Text = "Descripción:";
            // 
            // tabOOperativos
            // 
            this.tabOOperativos.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabOOperativos.Controls.Add(this.panel2);
            this.tabOOperativos.Controls.Add(this.label6);
            this.tabOOperativos.Controls.Add(this.dtgListaObjetivosE);
            this.tabOOperativos.Controls.Add(this.label2);
            this.tabOOperativos.Controls.Add(this.btnGuardarO);
            this.tabOOperativos.Controls.Add(this.txtObjetivoO);
            this.tabOOperativos.Controls.Add(this.label4);
            this.tabOOperativos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabOOperativos.Location = new System.Drawing.Point(4, 33);
            this.tabOOperativos.Name = "tabOOperativos";
            this.tabOOperativos.Padding = new System.Windows.Forms.Padding(3);
            this.tabOOperativos.Size = new System.Drawing.Size(1000, 497);
            this.tabOOperativos.TabIndex = 1;
            this.tabOOperativos.Text = "Objetivos Operativos";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtBuscarObjetivoO);
            this.panel2.Controls.Add(this.dtgOOperativos);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(430, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 491);
            this.panel2.TabIndex = 154;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(19, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 124;
            this.label11.Text = "Buscar:";
            // 
            // txtBuscarObjetivoO
            // 
            this.txtBuscarObjetivoO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarObjetivoO.Location = new System.Drawing.Point(68, 14);
            this.txtBuscarObjetivoO.Name = "txtBuscarObjetivoO";
            this.txtBuscarObjetivoO.Size = new System.Drawing.Size(340, 20);
            this.txtBuscarObjetivoO.TabIndex = 113;
            this.txtBuscarObjetivoO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarObjetivoO_KeyPress);
            // 
            // dtgOOperativos
            // 
            this.dtgOOperativos.CausesValidation = false;
            this.dtgOOperativos.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgOOperativos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgOOperativos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgOOperativos.Location = new System.Drawing.Point(0, 48);
            this.dtgOOperativos.MainView = this.dgvOOperativosVista;
            this.dtgOOperativos.Name = "dtgOOperativos";
            this.dtgOOperativos.Size = new System.Drawing.Size(567, 443);
            this.dtgOOperativos.TabIndex = 106;
            this.dtgOOperativos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvOOperativosVista});
            this.dtgOOperativos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgOOperativos_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vincularOEToolStripMenuItem,
            this.desvincularOEToolStripMenuItem,
            this.eliminarOOperativoToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(196, 70);
            // 
            // desvincularOEToolStripMenuItem
            // 
            this.desvincularOEToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.desvincularOEToolStripMenuItem.Name = "desvincularOEToolStripMenuItem";
            this.desvincularOEToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.desvincularOEToolStripMenuItem.Text = "Desvincular";
            this.desvincularOEToolStripMenuItem.Click += new System.EventHandler(this.desvincularOEToolStripMenuItem_Click);
            // 
            // eliminarOOperativoToolStripMenuItem
            // 
            this.eliminarOOperativoToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarOOperativoToolStripMenuItem.Name = "eliminarOOperativoToolStripMenuItem";
            this.eliminarOOperativoToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.eliminarOOperativoToolStripMenuItem.Text = "Eliminar O. Operativo";
            this.eliminarOOperativoToolStripMenuItem.Click += new System.EventHandler(this.eliminarOOperativoToolStripMenuItem_Click);
            // 
            // dgvOOperativosVista
            // 
            this.dgvOOperativosVista.GridControl = this.dtgOOperativos;
            this.dgvOOperativosVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvOOperativosVista.Name = "dgvOOperativosVista";
            this.dgvOOperativosVista.OptionsBehavior.Editable = false;
            this.dgvOOperativosVista.OptionsBehavior.ReadOnly = true;
            this.dgvOOperativosVista.OptionsView.ColumnAutoWidth = false;
            this.dgvOOperativosVista.OptionsView.RowAutoHeight = true;
            this.dgvOOperativosVista.OptionsView.ShowFooter = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 16);
            this.label6.TabIndex = 153;
            this.label6.Text = "Seleccionar Objetivos Estratégicos:";
            // 
            // dtgListaObjetivosE
            // 
            this.dtgListaObjetivosE.AllowDrop = true;
            this.dtgListaObjetivosE.AllowUserToAddRows = false;
            this.dtgListaObjetivosE.AllowUserToDeleteRows = false;
            this.dtgListaObjetivosE.AllowUserToResizeRows = false;
            this.dtgListaObjetivosE.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgListaObjetivosE.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgListaObjetivosE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgListaObjetivosE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgListaObjetivosE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Marca,
            this.idObjetivoE,
            this.ObjetivoE});
            this.dtgListaObjetivosE.Location = new System.Drawing.Point(23, 219);
            this.dtgListaObjetivosE.Name = "dtgListaObjetivosE";
            this.dtgListaObjetivosE.ReadOnly = true;
            this.dtgListaObjetivosE.RowHeadersVisible = false;
            this.dtgListaObjetivosE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgListaObjetivosE.Size = new System.Drawing.Size(377, 201);
            this.dtgListaObjetivosE.TabIndex = 152;
            this.dtgListaObjetivosE.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgListaObjetivosE_CellContentClick);
            this.dtgListaObjetivosE.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgListaObjetivosE_ColumnHeaderMouseClick);
            // 
            // Marca
            // 
            this.Marca.FillWeight = 50F;
            this.Marca.HeaderText = "Marca";
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            this.Marca.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Marca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Marca.Width = 50;
            // 
            // idObjetivoE
            // 
            this.idObjetivoE.FillWeight = 5F;
            this.idObjetivoE.HeaderText = "idObjetivoE";
            this.idObjetivoE.Name = "idObjetivoE";
            this.idObjetivoE.ReadOnly = true;
            this.idObjetivoE.Visible = false;
            this.idObjetivoE.Width = 5;
            // 
            // ObjetivoE
            // 
            this.ObjetivoE.FillWeight = 500F;
            this.ObjetivoE.HeaderText = "Objetivo Estratégico";
            this.ObjetivoE.Name = "ObjetivoE";
            this.ObjetivoE.ReadOnly = true;
            this.ObjetivoE.Width = 500;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 20);
            this.label2.TabIndex = 131;
            this.label2.Text = "INGRESAR OBJETIVO OPERATIVO:";
            // 
            // btnGuardarO
            // 
            this.btnGuardarO.Appearance.BackColor = System.Drawing.Color.White;
            this.btnGuardarO.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnGuardarO.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnGuardarO.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarO.Appearance.Options.UseBackColor = true;
            this.btnGuardarO.Appearance.Options.UseBorderColor = true;
            this.btnGuardarO.Appearance.Options.UseFont = true;
            this.btnGuardarO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarO.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarO.Image")));
            this.btnGuardarO.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGuardarO.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGuardarO.Location = new System.Drawing.Point(152, 439);
            this.btnGuardarO.Name = "btnGuardarO";
            this.btnGuardarO.Size = new System.Drawing.Size(114, 41);
            this.btnGuardarO.TabIndex = 130;
            this.btnGuardarO.Text = "  Guardar";
            this.btnGuardarO.Click += new System.EventHandler(this.btnGuardarO_Click);
            // 
            // txtObjetivoO
            // 
            this.txtObjetivoO.BackColor = System.Drawing.Color.White;
            this.txtObjetivoO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObjetivoO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjetivoO.Location = new System.Drawing.Point(23, 80);
            this.txtObjetivoO.MaxLength = 250;
            this.txtObjetivoO.Multiline = true;
            this.txtObjetivoO.Name = "txtObjetivoO";
            this.txtObjetivoO.Size = new System.Drawing.Size(377, 93);
            this.txtObjetivoO.TabIndex = 129;
            this.txtObjetivoO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObjetivoO_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 128;
            this.label4.Text = "Descripción:";
            // 
            // tabActividades
            // 
            this.tabActividades.BackColor = System.Drawing.Color.LemonChiffon;
            this.tabActividades.Controls.Add(this.panel3);
            this.tabActividades.Controls.Add(this.label7);
            this.tabActividades.Controls.Add(this.dtgListaObjetivosO);
            this.tabActividades.Controls.Add(this.label8);
            this.tabActividades.Controls.Add(this.btnActividad);
            this.tabActividades.Controls.Add(this.txtActividad);
            this.tabActividades.Controls.Add(this.label9);
            this.tabActividades.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabActividades.Location = new System.Drawing.Point(4, 33);
            this.tabActividades.Name = "tabActividades";
            this.tabActividades.Padding = new System.Windows.Forms.Padding(3);
            this.tabActividades.Size = new System.Drawing.Size(1000, 497);
            this.tabActividades.TabIndex = 2;
            this.tabActividades.Text = "Actividades";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.txtBuscarActividad);
            this.panel3.Controls.Add(this.dtgActividades);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(430, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(567, 491);
            this.panel3.TabIndex = 161;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(19, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 124;
            this.label12.Text = "Buscar:";
            // 
            // txtBuscarActividad
            // 
            this.txtBuscarActividad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarActividad.Location = new System.Drawing.Point(68, 14);
            this.txtBuscarActividad.Name = "txtBuscarActividad";
            this.txtBuscarActividad.Size = new System.Drawing.Size(340, 20);
            this.txtBuscarActividad.TabIndex = 113;
            this.txtBuscarActividad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarActividad_KeyPress);
            // 
            // dtgActividades
            // 
            this.dtgActividades.CausesValidation = false;
            this.dtgActividades.ContextMenuStrip = this.contextMenuStrip3;
            this.dtgActividades.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtgActividades.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtgActividades.Location = new System.Drawing.Point(0, 48);
            this.dtgActividades.MainView = this.dgvActividadesVista;
            this.dtgActividades.Name = "dtgActividades";
            this.dtgActividades.Size = new System.Drawing.Size(567, 443);
            this.dtgActividades.TabIndex = 106;
            this.dtgActividades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvActividadesVista});
            this.dtgActividades.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgActividades_MouseUp);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vincularOOToolStripMenuItem,
            this.desvincularOOToolStripMenuItem,
            this.eliminarActividadToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(171, 70);
            // 
            // desvincularOOToolStripMenuItem
            // 
            this.desvincularOOToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.desplazamiento;
            this.desvincularOOToolStripMenuItem.Name = "desvincularOOToolStripMenuItem";
            this.desvincularOOToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.desvincularOOToolStripMenuItem.Text = "Desvincular";
            this.desvincularOOToolStripMenuItem.Click += new System.EventHandler(this.desvincularOOToolStripMenuItem_Click);
            // 
            // eliminarActividadToolStripMenuItem
            // 
            this.eliminarActividadToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources.cancel;
            this.eliminarActividadToolStripMenuItem.Name = "eliminarActividadToolStripMenuItem";
            this.eliminarActividadToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.eliminarActividadToolStripMenuItem.Text = "Eliminar Actividad";
            this.eliminarActividadToolStripMenuItem.Click += new System.EventHandler(this.eliminarActividadToolStripMenuItem_Click);
            // 
            // dgvActividadesVista
            // 
            this.dgvActividadesVista.GridControl = this.dtgActividades;
            this.dgvActividadesVista.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Importe", null, "")});
            this.dgvActividadesVista.Name = "dgvActividadesVista";
            this.dgvActividadesVista.OptionsBehavior.Editable = false;
            this.dgvActividadesVista.OptionsBehavior.ReadOnly = true;
            this.dgvActividadesVista.OptionsView.ColumnAutoWidth = false;
            this.dgvActividadesVista.OptionsView.RowAutoHeight = true;
            this.dgvActividadesVista.OptionsView.ShowFooter = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(212, 16);
            this.label7.TabIndex = 160;
            this.label7.Text = "Seleccionar Objetivos Operativos:";
            // 
            // dtgListaObjetivosO
            // 
            this.dtgListaObjetivosO.AllowDrop = true;
            this.dtgListaObjetivosO.AllowUserToAddRows = false;
            this.dtgListaObjetivosO.AllowUserToDeleteRows = false;
            this.dtgListaObjetivosO.AllowUserToResizeRows = false;
            this.dtgListaObjetivosO.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgListaObjetivosO.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgListaObjetivosO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgListaObjetivosO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgListaObjetivosO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Marca2,
            this.idObjetivoO,
            this.ObjetivoO});
            this.dtgListaObjetivosO.Location = new System.Drawing.Point(23, 219);
            this.dtgListaObjetivosO.Name = "dtgListaObjetivosO";
            this.dtgListaObjetivosO.ReadOnly = true;
            this.dtgListaObjetivosO.RowHeadersVisible = false;
            this.dtgListaObjetivosO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgListaObjetivosO.Size = new System.Drawing.Size(377, 201);
            this.dtgListaObjetivosO.TabIndex = 159;
            this.dtgListaObjetivosO.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgListaObjetivosO_CellContentClick);
            this.dtgListaObjetivosO.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgListaObjetivosO_ColumnHeaderMouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(209, 20);
            this.label8.TabIndex = 158;
            this.label8.Text = "INGRESAR ACTIVIDAD:";
            // 
            // btnActividad
            // 
            this.btnActividad.Appearance.BackColor = System.Drawing.Color.White;
            this.btnActividad.Appearance.BackColor2 = System.Drawing.Color.White;
            this.btnActividad.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.btnActividad.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActividad.Appearance.Options.UseBackColor = true;
            this.btnActividad.Appearance.Options.UseBorderColor = true;
            this.btnActividad.Appearance.Options.UseFont = true;
            this.btnActividad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActividad.Image = ((System.Drawing.Image)(resources.GetObject("btnActividad.Image")));
            this.btnActividad.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnActividad.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnActividad.Location = new System.Drawing.Point(152, 439);
            this.btnActividad.Name = "btnActividad";
            this.btnActividad.Size = new System.Drawing.Size(114, 41);
            this.btnActividad.TabIndex = 157;
            this.btnActividad.Text = "  Guardar";
            this.btnActividad.Click += new System.EventHandler(this.btnActividad_Click);
            // 
            // txtActividad
            // 
            this.txtActividad.BackColor = System.Drawing.Color.White;
            this.txtActividad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtActividad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActividad.Location = new System.Drawing.Point(23, 80);
            this.txtActividad.MaxLength = 250;
            this.txtActividad.Multiline = true;
            this.txtActividad.Name = "txtActividad";
            this.txtActividad.Size = new System.Drawing.Size(377, 93);
            this.txtActividad.TabIndex = 156;
            this.txtActividad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtActividad_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 155;
            this.label9.Text = "Descripción:";
            // 
            // Marca2
            // 
            this.Marca2.FillWeight = 50F;
            this.Marca2.HeaderText = "Marca";
            this.Marca2.Name = "Marca2";
            this.Marca2.ReadOnly = true;
            this.Marca2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Marca2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Marca2.Width = 50;
            // 
            // idObjetivoO
            // 
            this.idObjetivoO.FillWeight = 5F;
            this.idObjetivoO.HeaderText = "idObjetivoO";
            this.idObjetivoO.Name = "idObjetivoO";
            this.idObjetivoO.ReadOnly = true;
            this.idObjetivoO.Visible = false;
            this.idObjetivoO.Width = 5;
            // 
            // ObjetivoO
            // 
            this.ObjetivoO.FillWeight = 500F;
            this.ObjetivoO.HeaderText = "Objetivo Operativo";
            this.ObjetivoO.Name = "ObjetivoO";
            this.ObjetivoO.ReadOnly = true;
            this.ObjetivoO.Width = 500;
            // 
            // vincularOEToolStripMenuItem
            // 
            this.vincularOEToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.vincularOEToolStripMenuItem.Name = "vincularOEToolStripMenuItem";
            this.vincularOEToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.vincularOEToolStripMenuItem.Text = "Asignar O. Estratégicos";
            this.vincularOEToolStripMenuItem.Click += new System.EventHandler(this.vincularOEToolStripMenuItem_Click);
            // 
            // vincularOOToolStripMenuItem
            // 
            this.vincularOOToolStripMenuItem.Image = global::ReportesTranspesa.Properties.Resources._3775736_backlink_chain_connection_link_multimedia_108983;
            this.vincularOOToolStripMenuItem.Name = "vincularOOToolStripMenuItem";
            this.vincularOOToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.vincularOOToolStripMenuItem.Text = "Vincular";
            this.vincularOOToolStripMenuItem.Click += new System.EventHandler(this.vincularOOToolStripMenuItem_Click);
            // 
            // frmMaestroObjetivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1008, 577);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmMaestroObjetivos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAESTRO DE OBJETIVOS";
            this.Load += new System.EventHandler(this.frmMaestroObjetivos_Load);
            this.tabInfo.ResumeLayout(false);
            this.tabOEstrategico.ResumeLayout(false);
            this.tabOEstrategico.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOEstrategicos)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOEstrategicosVista)).EndInit();
            this.tabOOperativos.ResumeLayout(false);
            this.tabOOperativos.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOOperativos)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOOperativosVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaObjetivosE)).EndInit();
            this.tabActividades.ResumeLayout(false);
            this.tabActividades.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgActividades)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActividadesVista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListaObjetivosO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabOEstrategico;
        private System.Windows.Forms.TabPage tabOOperativos;
        private System.Windows.Forms.TabPage tabActividades;
        private DevExpress.XtraGrid.GridControl dtgOEstrategicos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvOEstrategicosVista;
        private DevExpress.XtraEditors.SimpleButton btnGuardarE;
        public System.Windows.Forms.TextBox txtObjetivoE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnGuardarO;
        public System.Windows.Forms.TextBox txtObjetivoO;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView dtgListaObjetivosE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn idObjetivoE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjetivoE;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DataGridView dtgListaObjetivosO;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton btnActividad;
        public System.Windows.Forms.TextBox txtActividad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarOEstrategicoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem eliminarOOperativoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem eliminarActividadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desvincularOEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desvincularOOToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBuscarObjetivoE;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBuscarObjetivoO;
        private DevExpress.XtraGrid.GridControl dtgOOperativos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvOOperativosVista;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBuscarActividad;
        private DevExpress.XtraGrid.GridControl dtgActividades;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvActividadesVista;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Marca2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idObjetivoO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjetivoO;
        private System.Windows.Forms.ToolStripMenuItem vincularOEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vincularOOToolStripMenuItem;
    }
}