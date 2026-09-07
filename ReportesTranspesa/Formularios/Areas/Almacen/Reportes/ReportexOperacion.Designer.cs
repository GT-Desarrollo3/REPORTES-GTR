namespace ReportesTranspesa.Formularios.Areas.Almacen.Reportes
{
    partial class ReporteXOperacion
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.tabReportes = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.crvIngresos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crvOrdenesRetiro = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.crvDespachos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.crvDespachosSacos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.crvSaldos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabReportes.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabReportes);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1243, 484);
            this.splitContainer1.SplitterDistance = 51;
            this.splitContainer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1157, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 38);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblOperacion);
            this.groupBox2.Location = new System.Drawing.Point(373, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 38);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operación:";
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperacion.ForeColor = System.Drawing.Color.Blue;
            this.lblOperacion.Location = new System.Drawing.Point(53, 16);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(80, 19);
            this.lblOperacion.TabIndex = 1;
            this.lblOperacion.Text = "Operacion";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 38);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cliente";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.ForeColor = System.Drawing.Color.Blue;
            this.lblCliente.Location = new System.Drawing.Point(6, 16);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(56, 19);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente";
            // 
            // tabReportes
            // 
            this.tabReportes.Controls.Add(this.tabPage1);
            this.tabReportes.Controls.Add(this.tabPage2);
            this.tabReportes.Controls.Add(this.tabPage3);
            this.tabReportes.Controls.Add(this.tabPage4);
            this.tabReportes.Controls.Add(this.tabPage5);
            this.tabReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReportes.Location = new System.Drawing.Point(0, 25);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.SelectedIndex = 0;
            this.tabReportes.Size = new System.Drawing.Size(1243, 404);
            this.tabReportes.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.crvIngresos);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1235, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "INGRESOS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // crvIngresos
            // 
            this.crvIngresos.ActiveViewIndex = -1;
            this.crvIngresos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvIngresos.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvIngresos.Location = new System.Drawing.Point(3, 3);
            this.crvIngresos.Name = "crvIngresos";
            this.crvIngresos.ShowCloseButton = false;
            this.crvIngresos.ShowGroupTreeButton = false;
            this.crvIngresos.ShowLogo = false;
            this.crvIngresos.ShowParameterPanelButton = false;
            this.crvIngresos.Size = new System.Drawing.Size(1229, 372);
            this.crvIngresos.TabIndex = 0;
            this.crvIngresos.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.crvOrdenesRetiro);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1235, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ORDENES RETIRO";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // crvOrdenesRetiro
            // 
            this.crvOrdenesRetiro.ActiveViewIndex = -1;
            this.crvOrdenesRetiro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvOrdenesRetiro.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvOrdenesRetiro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvOrdenesRetiro.Location = new System.Drawing.Point(3, 3);
            this.crvOrdenesRetiro.Name = "crvOrdenesRetiro";
            this.crvOrdenesRetiro.ShowCloseButton = false;
            this.crvOrdenesRetiro.ShowGroupTreeButton = false;
            this.crvOrdenesRetiro.ShowLogo = false;
            this.crvOrdenesRetiro.ShowParameterPanelButton = false;
            this.crvOrdenesRetiro.Size = new System.Drawing.Size(1229, 372);
            this.crvOrdenesRetiro.TabIndex = 1;
            this.crvOrdenesRetiro.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.crvDespachos);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1235, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DESPACHOS GRANEL";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // crvDespachos
            // 
            this.crvDespachos.ActiveViewIndex = -1;
            this.crvDespachos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvDespachos.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvDespachos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvDespachos.Location = new System.Drawing.Point(0, 0);
            this.crvDespachos.Name = "crvDespachos";
            this.crvDespachos.ShowCloseButton = false;
            this.crvDespachos.ShowGroupTreeButton = false;
            this.crvDespachos.ShowLogo = false;
            this.crvDespachos.ShowParameterPanelButton = false;
            this.crvDespachos.Size = new System.Drawing.Size(1235, 378);
            this.crvDespachos.TabIndex = 1;
            this.crvDespachos.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.crvDespachosSacos);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1235, 378);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "DESPACHOS SACOS";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // crvDespachosSacos
            // 
            this.crvDespachosSacos.ActiveViewIndex = -1;
            this.crvDespachosSacos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvDespachosSacos.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvDespachosSacos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvDespachosSacos.Location = new System.Drawing.Point(0, 0);
            this.crvDespachosSacos.Name = "crvDespachosSacos";
            this.crvDespachosSacos.ShowCloseButton = false;
            this.crvDespachosSacos.ShowGroupTreeButton = false;
            this.crvDespachosSacos.ShowLogo = false;
            this.crvDespachosSacos.ShowParameterPanelButton = false;
            this.crvDespachosSacos.Size = new System.Drawing.Size(1235, 378);
            this.crvDespachosSacos.TabIndex = 1;
            this.crvDespachosSacos.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.crvSaldos);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1235, 378);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "SALDOS";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // crvSaldos
            // 
            this.crvSaldos.ActiveViewIndex = -1;
            this.crvSaldos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvSaldos.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvSaldos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvSaldos.Location = new System.Drawing.Point(0, 0);
            this.crvSaldos.Name = "crvSaldos";
            this.crvSaldos.ShowCloseButton = false;
            this.crvSaldos.ShowGroupTreeButton = false;
            this.crvSaldos.ShowLogo = false;
            this.crvSaldos.ShowParameterPanelButton = false;
            this.crvSaldos.Size = new System.Drawing.Size(1235, 378);
            this.crvSaldos.TabIndex = 2;
            this.crvSaldos.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1243, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ReporteXOperacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 484);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReporteXOperacion";
            this.Text = "Reportes OPERACIONES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReporteXOperacion_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabReportes.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabReportes;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblOperacion;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvIngresos;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvOrdenesRetiro;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvDespachos;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvDespachosSacos;
        private System.Windows.Forms.TabPage tabPage5;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvSaldos;
    }
}