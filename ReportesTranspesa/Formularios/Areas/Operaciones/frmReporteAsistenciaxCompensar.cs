using Comun;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmReporteAsistenciaxCompensar : Form
    {
        public int Cesados = 0;
        public frmReporteAsistenciaxCompensar()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDatos();

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            DataTable DTReportexCompensar;
            DTReportexCompensar = clsOperacionesBL.Instancia.GetDataReporteAsistenciaxCompensar(Cesados);

            if (DTReportexCompensar.Rows.Count > 0)
            {
                label3.Text = "Total: " + DTReportexCompensar.Rows.Count.ToString();

                gridControl1.DataSource = DTReportexCompensar;

                gridView1.Columns["PK"].Visible = false;

                gridView1.Columns["IDPERSONA"].Fixed = FixedStyle.Left;
                gridView1.Columns["NOMBRE"].Fixed = FixedStyle.Left;
                gridView1.Columns["OPERACION"].Fixed = FixedStyle.Left;

                gridView1.Columns["IDPERSONA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "IDPERSONA", "{0}");
                gridView1.Columns["PORCOMPENSARDIA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCOMPENSARDIA", "{0}");
                gridView1.Columns["PORCOMPENSARNOCHE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCOMPENSARNOCHE", "{0}");
                gridView1.Columns["PORCOMPENSARADELANTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCOMPENSARADELANTADO", "{0}");
                gridView1.BestFitColumns();
            }
            else
            {
                MessageBox.Show("No datos para mostrar", "AVISO");
                label3.Text = "Total: 0";
            }
        }

        private void chkCesados_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCesados.Checked)
            {
                Cesados = 1;
            }
            else
            {
                Cesados = 0;
            }

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gridView1.DataSource == null)
            {
                MessageBox.Show("No hay data para exportar", "AVISO");
            }
            else
            {

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Asistenci de Conductores del " + dateTimePicker1.Value.ToString("dd_MM_yyyy") + " al " + dateTimePicker2.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gridView1.ExportToXlsx(nombre);
                System.Diagnostics.Process.Start(nombre);
            }
        }

        private void frmReporteAsistenciaxCompensar_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_EmbeddedNavigator_StyleChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
          /*  GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
               /* string ValorEstado = View.GetRowCellDisplayText(e.RowHandle, View.Columns["TOTAL"]);

                gridView1.Columns["TOTAL"].AppearanceCell.BackColor = Color.YellowGreen;
                gridView1.Columns["TOTAL"].AppearanceCell.BackColor2 = Color.YellowGreen;*/
                //}}*/
            
        }
    }
}
