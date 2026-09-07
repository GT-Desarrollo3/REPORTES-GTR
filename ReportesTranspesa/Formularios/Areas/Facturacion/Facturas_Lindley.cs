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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class Facturas_Lindley : MetroFramework.Forms.MetroForm
    {
        public Facturas_Lindley()
        {
            InitializeComponent();
        }

        private void Facturas_Lindley_Load(object sender, EventArgs e)
        {
            //dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            char tipofecha;
            if (rbFechaDoc.Checked)
            {
                tipofecha = 'D';
            }
            else
            {
                tipofecha = 'P';
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetFacturasLindley(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59",tipofecha);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["DOCUMENTO"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "c2";

                

                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                item1.FieldName = "MONTO";
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item1.DisplayFormat = " | {0:c2}";
                dtgvDataView.GroupSummary.Add(item1);

                dtgvDataView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total={0:c2}");
                
                //dtgvDataView.UpdateSummary();

                dtgvDataView.ExpandAllGroups();
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Ventas Detalladas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void dtgvDataView_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystemBase pb = (DevExpress.XtraPrinting.PrintingSystemBase)(e.PrintingSystem);
            pb.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            pb.PageSettings.LeftMargin = 50;
            //pb.PageSettings.MarginsF.Left = 13;
            pb.PageSettings.RightMargin = 50;
            //pb.PageSettings.MarginsF.Right = 13;
            pb.PageSettings.TopMargin = 50;
            pb.PageSettings.BottomMargin = 50;
            //pb.PageSettings.Landscape = True;
        }
    }
}
