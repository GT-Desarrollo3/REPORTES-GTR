using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmReporteIngresoSubsidios : MetroFramework.Forms.MetroForm
    {
        public frmReporteIngresoSubsidios()
        {
            InitializeComponent();
        }

        private void frmReporteIngresoSubsidios_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha de Inicio debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Reporte_IngresoXSubsidios(dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString());
            if (dt.Rows.Count > 0)
            {
                dtgvIngresosSubsidios.DataSource = dt;

                dtgvIngresosSubsidiosView.Columns["Monto"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvIngresosSubsidiosView.Columns["Monto"].DisplayFormat.FormatString = "N2";
                
                dtgvIngresosSubsidiosView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para mostrar.";
                m.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvIngresosSubsidios.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Ingresos por Subsidios " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvIngresosSubsidios.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
