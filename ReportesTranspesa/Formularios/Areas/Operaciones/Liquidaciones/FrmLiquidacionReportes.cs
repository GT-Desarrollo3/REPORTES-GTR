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
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmLiquidacionReportes : Form
    {
        public FrmLiquidacionReportes()
        {
            InitializeComponent();
        }

        private void FrmLiquidacionReportes_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 74;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (cbxTipoReporte.SelectedIndex == 0)
            {

                if (dgvLiquidaciones.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Liquidaciones Diarias " + DateTime.Now.Year + " " + Environment.UserName.ToUpper() + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvLiquidaciones.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }

            }

            if (cbxTipoReporte.SelectedIndex == 1)
            {
                if (dgvLiquidaciones.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Planillas Pendientes " + DateTime.Now.Year + " " + Environment.UserName.ToUpper() + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvLiquidaciones.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }

        private void btnReportar_Click(object sender, EventArgs e)
        {
            dgvLiquidaciones.DataSource = null;

            if (cbxTipoReporte.SelectedIndex == 0)
            {
                DataTable dtData = clsLiquidacionPlanillaBL.Instancia.ReportarLiquidacionesDiario(dtpFecha.Value.Day, dtpFecha.Value.Month, dtpFecha.Value.Year, Environment.UserName.ToUpper());

                if (dtData.Rows.Count > 0)
                {
                    dgvLiquidaciones.DataSource = dtData;

                }

            }

            if (cbxTipoReporte.SelectedIndex == 1)
            {
                DataTable dtData = clsLiquidacionPlanillaBL.Instancia.ReportarPlanillasPendientes(Environment.UserName.ToUpper());

                if (dtData.Rows.Count > 0)
                {
                    dgvLiquidaciones.DataSource = dtData;

                }


            }
        }




    }
}
