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
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class AnalisisFondoGV : MetroFramework.Forms.MetroForm
    {
        public AnalisisFondoGV()
        {
            InitializeComponent();
        }

        private void AnalisisFondoGV_Load(object sender, EventArgs e)
        {
            string mes;
            if (DateTime.Now.Month < 10)
            {
                mes = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                mes = DateTime.Now.Month.ToString();
            }
            txtPeriodo1.Text = DateTime.Now.Year.ToString() + mes;
            txtPeriodo2.Text = DateTime.Now.Year.ToString() + mes;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetAnalisisGV(txtPeriodo1.Text.Trim(),txtPeriodo2.Text.Trim(),txtCuenta1.Text,txtCuenta2.Text);
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte Analisis de Fondo de GV del " + txtPeriodo1.Text + " al " + txtPeriodo2.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
