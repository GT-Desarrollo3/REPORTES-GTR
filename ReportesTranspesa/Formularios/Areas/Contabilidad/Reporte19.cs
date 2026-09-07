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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class Reporte19 : MetroFramework.Forms.MetroForm
    {
        public Reporte19()
        {
            InitializeComponent();
        }

        private void Reporte19_Load(object sender, EventArgs e)
        {
            cboCompania.SelectedIndex = 0;
            string mes;
            if (DateTime.Now.Month < 10)
            {
                mes = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                mes = DateTime.Now.Month.ToString();
            }
            txtPerIni.Text = DateTime.Now.Year.ToString() + mes;
            txtPerFin.Text = DateTime.Now.Year.ToString() + mes;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string compañia = "10000000";
            switch (cboCompania.SelectedIndex)
            {
                case 0: //transpesa
                    compañia = "10000000";
                    break;
                case 1: //control
                    compañia = "20000000";
                    break;
                case 2: //adriel
                    compañia = "30000000";
                    break;
                case 3: //bra
                    compañia = "40000000";
                    break;
                case 4: // almacenes
                    compañia = "ALQALM00";
                    break;

                case 5: // altra
                    compañia = "50000000";
                    break;

                case 6: // amt
                    compañia = "60000000";
                    break;

                case 7: // aduanas
                    compañia = "70000000";
                    break;
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetReporte19(txtPerIni.Text.Trim(), txtPerFin.Text.Trim(), compañia, txtCuentaIni.Text ,txtCuentaFin.Text);
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte 19 Mayor Detallado del " + txtPerIni.Text + " al " + txtPerFin.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgvData_Click(object sender, EventArgs e)
        {

        }
    }
}
