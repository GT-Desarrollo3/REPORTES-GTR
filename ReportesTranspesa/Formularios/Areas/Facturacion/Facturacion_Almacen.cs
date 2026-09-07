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
using DevExpress.XtraPivotGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class Facturacion_Almacen : MetroFramework.Forms.MetroForm
    {
        public Facturacion_Almacen()
        {
            InitializeComponent();
        }
        private void Facturacion_Almacen_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            dtgvDataDetalle.DataSource = null;
            dtgvDataDetalleView.Columns.Clear();
            dtgvDataDetalleView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            char tipofecha = ' ';
            if (rbFechaEmision.Checked)
            {
                tipofecha = 'E';
            }
            else
            {
                tipofecha = 'P';
            }
            dt = clsAlmacenBL.Instancia.GetFacturacionAlmacen(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", tipofecha, 'R');
            dt2 = clsAlmacenBL.Instancia.GetFacturacionAlmacen(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", tipofecha, 'D');

            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                    CreaColumnasPivotGrid();
                    dtgvData.DataSource = dt;
                    dtgvDataDetalle.DataSource = dt2;
                    GridView gridView = dtgvDataDetalle.FocusedView as GridView;
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["SERVICIO"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    dtgvDataDetalleView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataDetalleView.Columns["MONTO"].DisplayFormat.FormatString = "c2";

                    GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                    item1.FieldName = "MONTO";
                    item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item1.DisplayFormat = " || TOTAL SERVICIO {0:c2}";
                    dtgvDataDetalleView.GroupSummary.Add(item1);

                    dtgvDataDetalleView.ExpandAllGroups();
                    dtgvDataDetalleView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            PivotGridField campoPivote2 = new PivotGridField();
            campoPivote = new PivotGridField("SERVICIO", PivotArea.RowArea);
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("MONTO TOTAL", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
            campoDia,campoMes,campoAño,campoTotal});
            campoPivote.AreaIndex = 0;
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void dtgvData_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
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
                    string nombre = System.IO.Path.Combine(desktop, "Facturación de Almacenes (Resumen) del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            else
            {
                if (dtgvDataDetalle.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Facturación de Almacenes (Detalle) del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvDataDetalle.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
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
            else
            {
                if (dtgvDataDetalle.DataSource == null)
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data a imprimir";
                    m.ShowDialog();
                }
                else
                {
                    dtgvDataDetalle.ShowPrintPreview();
                }
            }
        }
    }
}
