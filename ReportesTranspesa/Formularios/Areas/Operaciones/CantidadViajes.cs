using DevExpress.XtraPivotGrid;
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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class CantidadViajes : MetroFramework.Forms.MetroForm
    {
        public CantidadViajes()
        {
            InitializeComponent();
        }

        string fechaini;
        string fechafin;
        string cliente = "";
        List<string> codigosviajes;

        private void CantidadViajes_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            campoPivote = new PivotGridField("CLIENTE", PivotArea.RowArea);
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            //PivotGridField campoTotal = new PivotGridField("Total", PivotArea.DataArea);
            PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
            campoDia,campoMes,campoAño,campoTotal});
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            fechaini=dtpFechaIni.Value.ToShortDateString();
            fechafin=dtpFechaFin.Value.ToShortDateString();
            dtgvViajes.DataSource = null;
            dtgvViajesView.Columns.Clear();
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            cliente = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetCantidad_Viajes(fechaini, fechafin, cliente);
            if (dt.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvViajes.DataSource = dt;
                dtgvData.DataSource = dt;
                dtgvData.BestFitRowArea();
                //dtgvViajesView.Columns["CANTIDAD VIAJE"].Visible = false;
                //dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total ={0}");
                //dtgvViajesView.Columns["CANTIDAD VIAJE"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvViajesView.Columns["CANTIDAD VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD VIAJE", "Cant. Viaje ={0}");
                //dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Cant. Viaje ={0}");
                //Cantidad de viajes
                dtgvViajesView.Columns["CODIGO VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO VIAJE", "Viajes={0}");
                //dtgvViajesView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn());
                //dtgvViajesView.Columns["VIAJES"].Visible = true;
                dtgvViajesView.BestFitColumns();
                #region crear columns
                //GridColumn col=new GridColumn();
                //col.Caption = "Samples";
                //col.FieldName = "Samples";
                //dtgvViajesView.Columns.AddVisible(col.FieldName, string.Empty);
                //dtgvViajesView.Columns.Add(col);
                ////dtgvViajesView.AddNewRow();
                ////dtgvViajesView.SetRowCellValue(10, "FieldName",1);
                //GridColumn column = dtgvViajesView.Columns.AddVisible("FieldName", string.Empty);
                //dtgvViajesView.Columns.Add(column);

                //// Create an unbound column.
                //GridColumn unbColumn = dtgvViajesView.Columns.AddField("Total");
                //unbColumn.VisibleIndex = dtgvViajesView.Columns.Count;
                //unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                //// Disable editing.
                //unbColumn.OptionsColumn.AllowEdit = false;
                //// Specify format settings.
                //unbColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ////unbColumn.DisplayFormat.FormatString = "c";
                //// Customize the appearance settings.
                //unbColumn.AppearanceCell.BackColor = Color.LemonChiffon;
                ////Total de viajes
                //dtgvViajesView.Columns["Total"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvViajesView.Columns["Total"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Total", "Total ={0}");
                //dtgvViajesView.BestFitColumns();
                #endregion
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void dtgvViajes_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                DataRow row = dtgvViajesView.GetDataRow(dtgvViajesView.GetSelectedRows()[0]);
                cliente = row["CLIENTE"].ToString();
            }
            catch
            {
                //No selecciona cliente
            }
            
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFecha.Checked == true)
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
                dtpFechaIni.Visible = true;
                dtpFechaFin.Visible = true;
            }
            else
            {
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
            }
        }

        private void dtgvViajesView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                codigosviajes = new List<string>();
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                codigosviajes.Add(View.GetRowCellValue(e.RowHandle, "CODIGO VIAJE").ToString());
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                e.TotalValue = codigosviajes.Distinct().Count();
            }     
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

        private void dtgvViajesView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row = dtgvViajesView.GetDataRow(dtgvViajesView.GetSelectedRows()[0]);
                cliente = row["CLIENTE"].ToString();
                fechaini = dtpFechaIni.Value.ToShortDateString();
                fechafin = dtpFechaFin.Value.ToShortDateString();
                dtgvViajes.DataSource = null;
                dtgvViajesView.Columns.Clear();
                dtgvData.DataSource = null;
                dtgvData.Fields.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsOperacionesBL.Instancia.GetCantidad_Viajes(fechaini, fechafin, cliente);
                if (dt.Rows.Count > 0)
                {
                    CreaColumnasPivotGrid();
                    dtgvViajes.DataSource = dt;
                    dtgvData.DataSource = dt;
                    dtgvData.BestFitRowArea();
                    //dtgvViajesView.Columns["CANTIDAD VIAJE"].Visible = false;
                    //dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                    //dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total ={0}");
                    //dtgvViajesView.Columns["CANTIDAD VIAJE"].DisplayFormat.FormatType = FormatType.Numeric;
                    //dtgvViajesView.Columns["CANTIDAD VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD VIAJE", "Cant. Viaje ={0}");
                    //dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                    //dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Cant. Viaje ={0}");
                    //dtgvViajesView.Columns["Total"].DisplayFormat.FormatType = FormatType.Numeric;
                    //dtgvViajesView.Columns["Total"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Total", "Total ={0}");
                    //Cantidad de viajes
                    dtgvViajesView.Columns["CODIGO VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO VIAJE", "Viajes={0}");
                    dtgvViajesView.BestFitColumns();
                }
            }
            catch
            {
                //No Selecciona Cliente
            }
            #region cliente
            //DataRow row = dtgvViajesView.GetDataRow(dtgvViajesView.GetSelectedRows()[0]);
            //cliente = row["CLIENTE"].ToString();
            //fechaini = dtpFechaIni.Value.ToShortDateString();
            //fechafin = dtpFechaFin.Value.ToShortDateString();
            //dtgvViajes.DataSource = null;
            //dtgvViajesView.Columns.Clear();
            //dtgvData.DataSource = null;
            //dtgvData.Fields.Clear();
            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt = clsOperacionesBL.Instancia.GetCantidad_Viajes(fechaini, fechafin, cliente);
            //if (dt.Rows.Count > 0)
            //{
            //    CreaColumnasPivotGrid();
            //    dtgvViajes.DataSource = dt;
            //    dtgvData.DataSource = dt;
            //    dtgvData.BestFitRowArea();
            //    //dtgvViajesView.Columns["CANTIDAD VIAJE"].Visible = false;
            //    //dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
            //    //dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total ={0}");
            //    //dtgvViajesView.Columns["CANTIDAD VIAJE"].DisplayFormat.FormatType = FormatType.Numeric;
            //    //dtgvViajesView.Columns["CANTIDAD VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD VIAJE", "Cant. Viaje ={0}");
            //    dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
            //    dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Cant. Viaje ={0}");
            //    //dtgvViajesView.Columns["Total"].DisplayFormat.FormatType = FormatType.Numeric;
            //    //dtgvViajesView.Columns["Total"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Total", "Total ={0}");
            //    //Cantidad de viajes
            //    dtgvViajesView.Columns["CODIGO VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO VIAJE", "Viajes={0}");
            //    dtgvViajesView.BestFitColumns();
            //}
            #endregion
        }

        private void dtgvViajesView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Total" && e.IsGetData) e.Value = getTotalValue(e.ListSourceRowIndex);
        }

        // Returns the total amount for a specific row.
        decimal getTotalValue(int listSourceRowIndex)
        {
            //decimal cantidad = codigosviajes.Distinct().Count();
            //return cantidad;
            DataRow row = dtgvViajesView.GetDataRow(dtgvViajesView.GetSelectedRows()[0]);
            decimal cantidad = Convert.ToDecimal(row["Cantidad"]);
            return cantidad;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvViajes.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Cantidad de Viajes del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvViajes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}

