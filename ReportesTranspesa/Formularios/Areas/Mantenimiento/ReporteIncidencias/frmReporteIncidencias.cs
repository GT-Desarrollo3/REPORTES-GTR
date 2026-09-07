using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using System.Drawing.Imaging;
using System.IO;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias
{
    public partial class frmReporteIncidencias : Form
    {
        public DataTable dtListaIncidencias = new DataTable();
        int Estado, FiltroFechas;

        public frmReporteIncidencias()
        {
            InitializeComponent();
        }

        private void frmReporteIncidencias_Load(object sender, EventArgs e)
        {
            dtpPeriodo.Value = DateTime.Now;

            cbPeriodo.Checked = true;
            cbPeriodo_CheckedChanged(sender, e);
            rbIncidenciasPersona.Checked = true;
            rbIncidenciasPersona_Click(sender, e);
        }


        public void ListarReporte()
        {
            dtListaIncidencias = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_GenerarReporte(Estado, FiltroFechas, dtpPeriodo.Text);
            dtgIncidentesR.DataSource = null;
            dgvIncidentesRVista.Columns.Clear(); 
            dtgIncidentesR.DataSource = dtListaIncidencias;
            if (dtListaIncidencias.Rows.Count > 0)
            {
                dgvIncidentesRVista.Columns["NRO_INCIDENTES"].Summary.Clear();
                dgvIncidentesRVista.Columns["NRO_INCIDENTES"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NRO_INCIDENTES", "Total = {0}");

                dgvIncidentesRVista.BestFitColumns();
            }
        }


        private void cbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPeriodo.Checked == true)
            {
                FiltroFechas = 1;
                dtpPeriodo.Enabled = true;
            }

            if (cbPeriodo.Checked == false)
            {
                FiltroFechas = 0;
                dtpPeriodo.Enabled = false;
            }

            ListarReporte();
        }

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReporte(); }
        }

        private void rbIncidenciasPersona_Click(object sender, EventArgs e)
        {
            if (rbIncidenciasPersona.Checked == true)
            {
                rbIncidenciasPersona.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                rbIncidenciasOperacion.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                Estado = 1;

                ListarReporte();
            }
        }

        private void rbIncidenciasOperacion_Click(object sender, EventArgs e)
        {
            if (rbIncidenciasOperacion.Checked == true)
            {
                rbIncidenciasPersona.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbIncidenciasOperacion.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                Estado = 2;

                ListarReporte();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgIncidentesR.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE INCIDENCIAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgIncidentesR.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
