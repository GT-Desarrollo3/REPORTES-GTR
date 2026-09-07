using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Diagnostics;
using System.Drawing.Printing;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;
using System.Globalization;
using System.Collections;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class GuiasxEstado : MetroFramework.Forms.MetroForm
    {
      /*  StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        int iCellHeight = 0;
        int iTotalWidth = 0;
        int iRow = 0;
        bool bFirstPage = false;
        bool bNewPage = false;
        int iHeaderHeight = 0;
        int paginainicio = 0;
        int paginafin = 0;
        int cuentapagina = 0;*/

        public GuiasxEstado()
        {
            InitializeComponent();
        }

        private void GuiasxEstado_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetPeriodoGuiasxEstado();
            cboPeriodo.DataSource = dt;
            cboPeriodo.ValueMember = "Id";
            cboPeriodo.DisplayMember = "Descripcion";
            cboPeriodo.SelectedIndex = 3;
            cboEstado.SelectedIndex = 0;
            cboFacturado.SelectedIndex = 0;
            lblTotalFact.Left = Convert.ToInt32(this.Width / 1.75);
            lblTotalFactT.Left = lblTotalFact.Left + lblTotalFactT.Width + 20;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData1View1.Columns.Clear();
            dtgvData1.DataSource = null;
            dtgvData2View2.Columns.Clear();
            dtgvData2.DataSource = null;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            dt = clsOperacionesBL.Instancia.GetDataGuiasxEstadoResumido(Convert.ToInt32(cboPeriodo.SelectedValue), cboEstado.Text);
            dt2 = clsOperacionesBL.Instancia.GetDataGuiasxEstadoDetalle(Convert.ToInt32(cboPeriodo.SelectedValue), cboEstado.Text);

            if (dt.Rows.Count > 0 || dt2.Rows.Count > 0)
            {
                dtgvData1.DataSource = dt;
                dtgvData2.DataSource = dt2;
                dtgvData1View1.BestFitColumns();
                dtgvData2View2.BestFitColumns();
                lblTotal.Text = dt2.Rows.Count.ToString();
                if (cboEstado.Text == "COMPLETADO")
                {
                    cboFacturado.Visible = true;
                    lblFacturado.Visible = true;
                    cboFacturado.Enabled = true;
                    lblTotalFact.Visible = true;
                    lblTotalFactT.Visible = true;
                    lblTotalFactT.Text = lblTotal.Text;
                }
                else
                {
                    cboFacturado.Visible = false;
                    lblFacturado.Visible = false;
                    cboFacturado.Enabled = false;
                    //lblTotalFact.Visible = true;
                    lblTotalFactT.Visible = true;
                    lblTotalFactT.Text = "";
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
                lblTotal.Text = "0";
            } 
        }

        private void cboFacturado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtgvData2.DataSource != null)
            {
                switch (cboFacturado.SelectedIndex)
                {
                    case 0: (dtgvData2.DataSource as DataTable).DefaultView.RowFilter = String.Empty;
                        lblTotalFactT.Text = lblTotal.Text;
                        break;
                    case 1: (dtgvData2.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] = '{1}'", "FACT.", "SI");
                        lblTotalFactT.Text = dtgvData2View2.RowCount.ToString();// dtgvData2.Rows.Count.ToString();
                        break;
                    case 2: (dtgvData2.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] = '{1}'", "FACT.", "NO");
                        lblTotalFactT.Text = dtgvData2View2.RowCount.ToString();//dtgvData2.Rows.Count.ToString();
                        break;
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData2.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Guias por estado Resumen" + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData1.ExportToXlsx(nombre);
                Process.Start(nombre);

                CultureInfo culture1 = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi1 = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop1 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre1 = System.IO.Path.Combine(desktop, "Reporte de Guias por estado Detalle" + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData2.ExportToXlsx(nombre1);
                Process.Start(nombre1);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
