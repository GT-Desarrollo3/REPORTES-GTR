using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Negocio;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmInventarioValorizadoPeriodoCerrado_043 : Form
    {
        string compania = "";
        string almacen = "";
        public frmInventarioValorizadoPeriodoCerrado_043()
        {
            InitializeComponent();
            cbxCompania2.SelectedIndexChanged -= cbxCompania2_SelectedIndexChanged;
            cbxAlmacen2.SelectedIndexChanged -= cbxAlmacen2_SelectedIndexChanged;
        }

        private void cbxCompania2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAlmacenes(1," "); }

        private void cbxAlmacen2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAlmacenes(2, compania); }

        private void frmInventarioValorizadoPeriodoCerrado_043_Load(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
            splitContainer2.Panel2Collapsed = true;
            dtpPeriodo2.Value = DateTime.Now;

            CargarComboAlmacenes(1, " ");
            cbxCompania2_DropDownClosed(sender, e);
        }


        private void CargarComboAlmacenes(int Opcion, string Compania)
        {
            if (Opcion == 1)    // LISTAR COMPAÑIAS
            {
                DataTable dtCondicion = clsLogisticaBL.Instancia.ReportesApp_Logistica_ListarAlmacenesInventario(Opcion, " ");
                cbxCompania2.DataSource = dtCondicion;
                cbxCompania2.DisplayMember = "DescripcionLarga";
                cbxCompania2.ValueMember = "CompaniaCodigo";
            }

            if (Opcion == 2)    // LISTAR ALMACENES
            {
                DataTable dtCondicion = clsLogisticaBL.Instancia.ReportesApp_Logistica_ListarAlmacenesInventario(Opcion, Compania);
                cbxAlmacen2.DataSource = dtCondicion;
                cbxAlmacen2.DisplayMember = "DescripcionLocal";
                cbxAlmacen2.ValueMember = "AlmacenCodigo";
            }
        }


        private void cbxCompania2_DropDownClosed(object sender, EventArgs e)
        {
            compania = Convert.ToString(cbxCompania2.SelectedValue);
            CargarComboAlmacenes(2, compania);
            cbxAlmacen2_DropDownClosed(sender, e);
        }

        private void cbxAlmacen2_DropDownClosed(object sender, EventArgs e) { almacen = Convert.ToString(cbxAlmacen2.SelectedValue); }

        private void dtpPeriodo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { button1_Click(sender, e); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if (cbxCompania.SelectedIndex == 0)
            {
                compania = "10000000";
            }
            if (cbxCompania.SelectedIndex == 1)
            {
                compania = "40000000";
            }
            if (cbxCompania.SelectedIndex == 2)
            {
                compania = "50000000";
            }
            if (cbxCompania.SelectedIndex == 3)
            {
                compania = "60000000";
            }
            if (cbxCompania.SelectedIndex == 4)
            {
                compania = "70000000";
            }

            if (cbxAlmacen.SelectedIndex == 0)
            {
                almacen = "A001";
            }
            if (cbxAlmacen.SelectedIndex == 1)
            {
                almacen = "A002";
            }
            if (cbxAlmacen.SelectedIndex == 2)
            {
                almacen = "A003";
            }
            if (cbxAlmacen.SelectedIndex == 3)
            {
                almacen = "A004";
            }
            if (cbxAlmacen.SelectedIndex == 4)
            {
                almacen = "A005";
            }
            if (cbxAlmacen.SelectedIndex == 5)
            {
                almacen = "A006";
            }
            if (cbxAlmacen.SelectedIndex == 6)
            {
                almacen = "A007";
            }
            if (cbxAlmacen.SelectedIndex == 7)
            {
                almacen = "A008";
            }
            if (cbxAlmacen.SelectedIndex == 8)
            {
                almacen = "A009";
            }
            if (cbxAlmacen.SelectedIndex == 9)
            {
                almacen = "A010";
            }
            */

            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsLogisticaBL.Instancia.GetLogistica_InventarioValorizadoPeriodoCerrado(compania, almacen, dtpPeriodo2.Text);

            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            
            if (dtRespuesta.Rows.Count > 0)
            {
                //No visibles
                dtgvData.DataSource = dtRespuesta;
                dtgvDataView.BestFitColumns();

                splitContainer2.Panel1Collapsed = false;
                splitContainer2.Panel2Collapsed = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) { verReporte(compania, almacen, dtpPeriodo2.Text); }

        private void verReporte(string  Compania, string Almacen, string Periodo)
        {
            CrystalReportViewer rv = new CrystalReportViewer();

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Logistica\Reportes\";
            string rpt = "crvReporte_InventarioValorizadoPerCerrado.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@Compania", Compania);
            r.SetParameterValue("@AlmacenCodigo", Almacen);
            r.SetParameterValue("@Periodo", Periodo);
            r.SetParameterValue("p_EmpresaNombre", cbxCompania2.Text);
            r.SetParameterValue("p_AlmacenNombre", cbxAlmacen2.Text);
            crvReporte.ReportSource = r;

            splitContainer2.Panel1Collapsed = true;
            splitContainer2.Panel2Collapsed = false;

            this.WindowState = FormWindowState.Maximized;

        }
        private Microsoft.Office.Interop.Excel.Application app;
        private void btnExcelDetalle_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                MessageBox.Show("No hay datos para Exportar");
                return;
            }
            else
            {

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Inventario Valorizado" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
            }
        }
    }
}
