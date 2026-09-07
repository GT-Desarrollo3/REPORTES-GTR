using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Guias_por_entregar : MetroFramework.Forms.MetroForm
    {
        public Guias_por_entregar()
        {
            InitializeComponent();
        }

        private void Guias_por_entregar_Load(object sender, EventArgs e)
        {
            cboOpcion.SelectedIndex = 0;
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            DataTable dt = new DataTable();

            dt = clsOperacionesBL.Instancia.GetGuiasxEntregar();
            dtgvData.DataSource = dt;
            dtgvDataView.Columns["FECHA PROGRAMADA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtgvDataView.Columns["FECHA PROGRAMADA"].DisplayFormat.FormatString = "g";
            GridView gridView = dtgvData.FocusedView as GridView;
            //gridView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
            new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
            }, 1);
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "VIAJE";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView.GroupSummary.Add(item);
            dtgvDataView.ExpandAllGroups();
            dtgvDataView.BestFitColumns();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboOpcion.Text.Equals("CLIENTE"))
            {
                dtgvData.DataSource = null;
                dtgvDataView.Columns.Clear();
                dtgvDataView.GroupSummary.Clear();
                DataTable dt = new DataTable();

                dt = clsOperacionesBL.Instancia.GetGuiasxEntregar();
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["FECHA PROGRAMADA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dtgvDataView.Columns["FECHA PROGRAMADA"].DisplayFormat.FormatString = "g";
                GridView gridView = dtgvData.FocusedView as GridView;
                //gridView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                GridGroupSummaryItem item = new GridGroupSummaryItem();
                item.FieldName = "VIAJE";
                item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                gridView.GroupSummary.Add(item);
                dtgvDataView.ExpandAllGroups();
                dtgvDataView.BestFitColumns();
            }

            if (cboOpcion.Text.Equals("CONDUCTOR"))
            {
                CargaGuias();
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
                string nombre = System.IO.Path.Combine(desktop, "Guías por entregar " + 
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
        private void CargaGuias()
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            DataTable dt = new DataTable();

            dt = clsOperacionesBL.Instancia.GetGuiasxEntregarConductor(1);
            dtgvData.DataSource = dt;

            dtgvDataView.Columns["IdConductor"].Visible = false;
           // dtgvDataView.Columns["FECHA PROGRAMADA"].DisplayFormat.FormatString = "g";*/
            GridView gridView = dtgvData.FocusedView as GridView;
            //gridView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["Conductor"], DevExpress.Data.ColumnSortOrder.Descending), 
                }, 1);
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "Guia";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;            
            gridView.GroupSummary.Add(item);
           // dtgvDataView.ExpandAllGroups();
            dtgvDataView.BestFitColumns();
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
