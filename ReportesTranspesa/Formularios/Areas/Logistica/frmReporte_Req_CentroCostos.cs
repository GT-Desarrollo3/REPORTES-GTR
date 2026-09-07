using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Negocio;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.Diagnostics;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmReporte_Req_CentroCostos : Form
    {
        DataTable dtCentroCostos;
        DataTable dtLista;
        DataTable totales;
    
        public frmReporte_Req_CentroCostos()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string centrocosto = string.Empty;

                if (checkCC.Checked)
                {
                    centrocosto = cbxCentroCosto.SelectedValue.ToString();
                }
                else
                {
                    centrocosto = "";
                }

                dtLista = clsLogisticaBL.Instancia.ReportesApp_Logistica_Listar_Req_CentroCostos(dtpFechaInicio.Text, dtpFechaFin.Text, centrocosto);
                dtgLista.DataSource = dtLista;
                dgvListaVista.Columns["IDProveedor"].Visible = false;
                dgvListaVista.Columns["Monto"].Summary.Clear();
                dgvListaVista.Columns["Monto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Monto", "Monto Total = {0:N2}");
                dgvListaVista.BestFitColumns();


                totales = clsLogisticaBL.Instancia.ReportesApp_Logistica_Listar_Req_CentroCostos_Grafico(dtpFechaInicio.Text, dtpFechaFin.Text);



                /*DataView dv1 = new DataView(totales);
                DataView dv2 = new DataView(totales);

                dv1.RowFilter = "Total>'0'";
                dv2.RowFilter = "CentoCosto<>''";*/
             
                grafico.DataSource = totales;
                //grafico.Series[0].ChartType = SeriesChartType.Bar;
                grafico.Legends[0].Enabled = true;
                grafico.Series["Series1"].XValueMember = "CentroCosto";
                grafico.Series["Series1"].YValueMembers = "Total";
                grafico.Series["Series1"].IsXValueIndexed = true;
                //grafico.Titles.Add("Prueba");
                //grafico.DataBind();


                //grafico.DataBindTable(totales.DefaultView, "CentroCosto");
                //grafico.Series[0].Points.Add(110,154,554);

               
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReporte_Req_CentroCostos_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex )
            {
                
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ListarCentroCostos()
        {

                dtCentroCostos = clsLogisticaBL.Instancia.ReportesApp_Logistica_CentroCostos();
                cbxCentroCosto.DataSource = dtCentroCostos;
                cbxCentroCosto.DisplayMember = "Descripcion";
                cbxCentroCosto.ValueMember = "CostCenter";
                cbxCentroCosto.SelectedIndex = 0;
          
  

        }

        private void checkCC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCC.Checked)
            {
                ListarCentroCostos();
            }
            else
            {
                cbxCentroCosto.DataSource = null;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
            if (dtgLista.DataSource == null)
            {
                MessageBox.Show("No hay datos que exportar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else  
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REPORTE GASTOS POR CENTRO DE COSTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgLista.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        
    }
}
