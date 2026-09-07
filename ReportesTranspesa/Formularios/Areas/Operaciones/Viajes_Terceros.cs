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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using ReportesTranspesa.Sistema;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Viajes_Terceros : MetroFramework.Forms.MetroForm
    {
        public Viajes_Terceros()
        {
            InitializeComponent();
        }

        private void Viajes_Terceros_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetViajesTerceros(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
             dtpFechaFin.Value.ToShortDateString() + " 23:59:59");
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;

                dtgvDataView.Columns["TOTAL OS"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["TOTAL OS"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["TOTAL OS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL OS", "Total ={0:c2}");

                if (chkPesos.Checked == true)
                {
                    //dtgvDataView.Columns["PESO TERCERO"].Visible = true;
                    //dtgvDataView.Columns["PESO TERCERO"].VisibleIndex = 4;
                    dtgvDataView.Columns["PESO CONTROL"].Visible = true;
                    dtgvDataView.Columns["PESO CONTROL"].VisibleIndex = 4;
                    dtgvDataView.Columns["PESO TRANSPESA"].Visible = true;
                    dtgvDataView.Columns["PESO TRANSPESA"].VisibleIndex = 6;
                }
                else
                {
                    //dtgvDataView.Columns["PESO TERCERO"].Visible = false;
                    dtgvDataView.Columns["PESO CONTROL"].Visible = false;
                    dtgvDataView.Columns["PESO TRANSPESA"].Visible = false;
                }

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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Viajes Terceros " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void chkPesos_CheckedChanged(object sender, EventArgs e)
        {
            if (dtgvData.DataSource != null) 
            {
                if (chkPesos.Checked == true)
                {
                    //dtgvDataView.Columns["PESO TERCERO"].Visible = true;
                    //dtgvDataView.Columns["PESO TERCERO"].VisibleIndex = 4;
                    dtgvDataView.Columns["PESO CONTROL"].Visible = true;
                    dtgvDataView.Columns["PESO CONTROL"].VisibleIndex = 4;
                    dtgvDataView.Columns["PESO TRANSPESA"].Visible = true;
                    dtgvDataView.Columns["PESO TRANSPESA"].VisibleIndex = 6;
                }
                else
                {
                    //dtgvDataView.Columns["PESO TERCERO"].Visible = false;
                    dtgvDataView.Columns["PESO CONTROL"].Visible = false;
                    dtgvDataView.Columns["PESO TRANSPESA"].Visible = false;
                }
                dtgvDataView.BestFitColumns();
            }
        }
    }
}
