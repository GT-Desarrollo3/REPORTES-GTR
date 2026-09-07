using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraPivotGrid;
using Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Consolidado_Viajes : MetroFramework.Forms.MetroForm
    {
        public Consolidado_Viajes()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (pvgData.DataSource == null && dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                if (IsControlAtFront(pvgData))
                {
                    pvgData.ShowPrintPreview();
                }
                else
                {
                    dtgvData.ShowPrintPreview();
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (pvgData.DataSource == null && dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                string tipo;
                string detallado;
                string fini;
                string ffin;
                fini = dtpFechaIni.Value.ToString("dd_MM_yyyy");
                ffin = dtpFechaFin.Value.ToString("dd_MM_yyyy");
                if (chkTodos.Checked == true)
                {
                    tipo = " (todos) ";
                }
                else
                {
                    if (rdbFacturar.Checked == true)
                    {
                        tipo = "por facturar";
                    }
                    else
                    {
                        tipo = "por completar";
                    }
                }
                if (rdbDetallado.Checked == true)
                {
                    detallado = "detallado";
                }
                else
                {
                    detallado = "resumido";
                }
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Consolidado de viajes " + tipo + "("
                    + detallado + ") del " + fini + " al " + ffin + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                if (File.Exists(nombre))
                {
                    File.Delete(nombre);
                }
                if (IsControlAtFront(pvgData))
                {
                    pvgData.ExportToXlsx(nombre);
                }
                else
                {
                    dtgvData.ExportToXlsx(nombre);
                }
                Process.Start(nombre);
            }
        }

        private bool IsControlAtFront(Control control)
        {
            return control.Parent.Controls.GetChildIndex(control) == 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            pvgData.DataSource = null;
            pvgData.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();

            dt = clsOperacionesBL.Instancia.GetDataConsolidado(dtpFechaIni.Value.ToShortDateString() + " 00:00:00", 
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", Convert.ToInt32(rdbDetallado.Checked), 
                Convert.ToInt32(chkTodos.Checked), Convert.ToInt32(rdbFacturar.Checked), rbFechaProg.Checked);
            if (dt.Rows.Count > 0)
            {
                if (rdbDetallado.Checked == true)
                {
                    dtgvData.DataSource = dt;
                    dtgvData.BringToFront();
                    dtgvDataView.BestFitColumns();
                }
                else
                {
                    CreaColumnasPivotGrid();
                    pvgData.DataSource = dt;
                    pvgData.BestFitRowArea();
                    pvgData.BringToFront();
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
            bool es;
            es = IsControlAtFront(dtgvData);
            es = IsControlAtFront(pvgData);
        }

        private void Operaciones_Consolidado_Viajes_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void CreaColumnasPivotGrid() 
        {
            PivotGridField campoCliente = new PivotGridField("CLIENTE", PivotArea.RowArea);
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoSituacionViaje = new PivotGridField("SITUACION VIAJE", PivotArea.FilterArea);
            campoSituacionViaje.Caption = "Situación Viaje";
            PivotGridField campoSituacionFacturado = new PivotGridField("SITUACION FACTURADO", PivotArea.FilterArea);
            campoSituacionFacturado.Caption = "Situación Facturado";
            PivotGridField campoTotal = new PivotGridField("MONTO TOTAL", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            pvgData.Fields.AddRange(new PivotGridField[] {campoCliente, 
            campoDia,campoMes,campoAño,campoSituacionViaje,campoSituacionFacturado,campoTotal});
            campoCliente.AreaIndex = 0;
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void pvgData_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.Appearance.BackColor = Color.DarkRed;
            if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.Appearance.BackColor = Color.DarkRed;
            if (e.ColumnValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.Appearance.BackColor = Color.DarkRed;
            if (e.ColumnValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.Appearance.BackColor = Color.DarkRed;
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked == true)
            {
                rdbFacturar.Checked = false;
                rdbCompletar.Checked = false;
                rdbCompletar.Enabled = false;
                rdbFacturar.Enabled = false;
                rdbDetallado.Checked = true;
                rdbResumido.Checked = false;
                rdbResumido.Enabled = false;
            }
            else
            {
                rdbCompletar.Enabled = true;
                rdbFacturar.Enabled = true;
                rdbFacturar.Checked = true;
                rdbCompletar.Checked = false;
                rdbDetallado.Enabled = true;
                rdbDetallado.Checked = false;
                rdbResumido.Enabled = true;
                rdbResumido.Checked = true;
            }
        }
    }
}
