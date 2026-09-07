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
using DevExpress.Utils;
using DevExpress.Data;
using System.Globalization;
using System.Diagnostics;
using Comun;
namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Lista_de_Condutores : MetroFramework.Forms.MetroForm
    {
        public Lista_de_Condutores()
        {
            InitializeComponent();
        }

        private void Lista_de_Condutores_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //   DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            dtgvTrujillo.DataSource = null;
            dtgvFueraTrujillo.DataSource = null;
            dtgvNoViajaron.DataSource = null;
            dtgvTrujilloView.Columns.Clear();
            dtgvFueraTrujilloView.Columns.Clear();
            dtgvNoViajaronView.Columns.Clear();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            dt1 = clsOperacionesBL.Instancia.GetUbicacionConductores(dtpFechaIni.Value.ToShortDateString(),
             dtpFechaFin.Value.ToShortDateString(), "Trujillo");
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt2 = clsOperacionesBL.Instancia.GetUbicacionConductores(dtpFechaIni.Value.ToShortDateString(),
             dtpFechaFin.Value.ToShortDateString(),"FueraTrujillo");
            System.Data.DataTable dt3 = new System.Data.DataTable();
            dt3 = clsOperacionesBL.Instancia.GetUbicacionConductoresNoViajaron(dtpFechaIni.Value.ToShortDateString(),
             dtpFechaFin.Value.ToShortDateString());

            if (dt1.Rows.Count > 0)
            {
                dtgvTrujillo.DataSource = dt1;
                dtgvFueraTrujillo.DataSource = dt2;
                dtgvNoViajaron.DataSource = dt3;

                dtgvTrujilloView.BestFitColumns();
                dtgvFueraTrujilloView.BestFitColumns();
                dtgvNoViajaronView.BestFitColumns();
                lblEncontrados.Text = dt1.Rows.Count.ToString();
                lblEncontrados1.Text = dt2.Rows.Count.ToString();
                lblEncontrados2.Text = dt3.Rows.Count.ToString();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //Ubicación en Trujillo
            if (dtgvTrujillo.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Conductores Ubicados en Trujillo del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") +
                    " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " +
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvTrujillo.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            //Ubicación Fuera de Trujillo
            if (dtgvFueraTrujillo.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Conductores Ubicados Fuera de Trujillo del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") +
                    " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " +
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvFueraTrujillo.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            //Ubicación de los que no viajan
            if (dtgvNoViajaron.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Conductores que no Viajaron del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") +
                    " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " +
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvNoViajaron.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void chkFechaIni_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechaIni.Checked == false)
            {
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
            }

            else
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
            }
        }
    }
}
