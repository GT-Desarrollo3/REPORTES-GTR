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

namespace ReportesTranspesa.Formularios.Areas.Neumaticos
{
    public partial class ConsumoNeumaticos : MetroFramework.Forms.MetroForm
    {
        public ConsumoNeumaticos()
        {
            InitializeComponent();
        }

        private void ConsumoNeumaticos_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechainicio = "01/01/1980";
            string fechafin = "31/12/2030";
            fechainicio = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsNeumaticoBL.Instancia.GetNeumaticoConsumo(fechainicio, fechafin);
            //dt = clsMantenimientoBL.Instancia.GetDataConsumo(fechainicio, fechafin);
            //dt = clsMantenimientoBL.Instancia.GetDataConsumo(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            // dtpFechaFin.Value.ToShortDateString() + " 23:59:59");
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
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
            if (dtgvData.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Consumo de Mantenimiento del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") +
                    " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " +
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvData.ShowPrintPreview();
            }
        }
    }
}
