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
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using Comun;
using DevExpress.XtraGrid.Views.Grid;
namespace ReportesTranspesa.Formularios.Areas.Operaciones
{

    public partial class frmAsistenciaReporte : Form
    {
        public int Cesados = 0;
        public frmAsistenciaReporte()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CargarReporte();
        }

        private void frmAsistenciaReporte_Load(object sender, EventArgs e)
        {
            CargarReporte();
        }

        private void CargarReporte() 
        {
            if (chkCesados.Checked == true)
            {
                Cesados = 1;
            }
            else { Cesados = 0; }
            


            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            DataTable DTReporte = new DataTable();
            DTReporte = clsOperacionesBL.Instancia.GetDataReporteAsistenciaCondutores(dateTimePicker1.Text, dateTimePicker2.Text, Cesados);

            if (DTReporte.Rows.Count > 0)
            {
                label3.Text = "Total: " + DTReporte.Rows.Count.ToString();

                gridControl1.DataSource = DTReporte;

                gridView1.Columns["PK"].Visible = false;

                gridView1.Columns["IDPERSONA"].Fixed = FixedStyle.Left;
                gridView1.Columns["NOMBRE"].Fixed = FixedStyle.Left;
                gridView1.Columns["OPERACION"].Fixed = FixedStyle.Left;

                gridView1.Columns["IDPERSONA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "IDPERSONA", "{0}");
                gridView1.Columns["ASISTENCIAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ASISTENCIAS", "{0}");
                gridView1.Columns["FALTAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FALTAS", "{0}");
                gridView1.Columns["DESCANSOFISICO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DESCANSOFISICO", "{0}");
                gridView1.Columns["COMPENSANDO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "COMPENSANDO", "{0}");
                gridView1.Columns["SUBSIDO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SUBSIDO", "{0}");
                gridView1.Columns["VACACIONES"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "VACACIONES", "{0}");
                gridView1.Columns["DESCMEDICO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DESCMEDICO", "{0}");
                gridView1.Columns["SUSPENSION"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SUSPENSION", "{0}");
                gridView1.Columns["COMPENSANOCHE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "COMPENSANOCHE", "{0}");
                gridView1.Columns["TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL", "TOTAL: {0}");
                //gridView1.Columns["PORCOMPENSARDIA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCOMPENSARDIA", "{0}");
                //gridView1.Columns["PORCOMPENSARNOCHE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCOMPENSARNOCHE", "{0}");
                //gridView1.Columns["PORCOMPENSARADELANTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCOMPENSARADELANTADO", "{0}");
                gridView1.BestFitColumns();
            }
            else
            {
                MessageBox.Show("No datos para mostrar", "AVISO");
                label3.Text = "Total: 0";
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gridView1.DataSource == null)
            {
                MessageBox.Show("No hay data para exportar","AVISO");
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string ValorEstado = View.GetRowCellDisplayText(e.RowHandle, View.Columns["TOTAL"]);
                //if (ValorEstado == "NO VISADO")
                //{
                //    /*e.Appearance.BackColor = Color.FromArgb(100, Color.Red);
                //    e.Appearance.BackColor2 = Color.White;*/
                //    gridView1.Columns["Estado"].AppearanceCell.BackColor = Color.FromArgb(100, Color.Red);// Color.LightSalmon;
                //    gridView1.Columns["Estado"].AppearanceCell.BackColor2 = Color.White;// Color.LightSalmon;
                //}
                //if (ValorEstado == "VISADO PARCIAL")
                //{
                //    /* e.RowHandle["Estado"].Appearance.BackColor = Color.FromArgb(150, Color.Yellow);
                //     e.Appearance.BackColor2 = Color.White;*/
                //    gridView1.Columns["Estado"].AppearanceCell.BackColor = Color.FromArgb(150, Color.Yellow);
                //    gridView1.Columns["Estado"].AppearanceCell.BackColor2 = Color.White;
                //}
                //if (ValorEstado == "VISADO")
                //{
                    /*e.Appearance.BackColor = Color.YellowGreen;
                    e.Appearance.BackColor2 = Color.YellowGreen;*/
                gridView1.Columns["TOTAL"].AppearanceCell.BackColor = Color.YellowGreen;
                gridView1.Columns["TOTAL"].AppearanceCell.BackColor2 = Color.YellowGreen;
                //}
            }
        }

        private void chkCesados_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCesados.Checked == true)
            {
                Cesados = 1;
            }
            else { Cesados = 0; }
            CargarReporte();

        }
    }
}
