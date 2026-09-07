using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class Costo_por_Unidades : MetroFramework.Forms.MetroForm
    {
        public Costo_por_Unidades()
        {
            InitializeComponent();
        }

        decimal sumaPrecioUnitario;
        decimal sumaPrecioUnitarioDolar;
        decimal sumaMontoTotal;
        decimal sumaMontoTotalDolar;
        int proyectos;
        private void Costo_por_Unidades_Load(object sender, EventArgs e)
        {

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvDataCosto.DataSource = null;
            gridView2.Columns.Clear();

           // string fechin = "01/01/1980";
            //string fechfin = "31/12/2030";
            //fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            //fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsMantenimientoBL.Instancia.GetDataCosto_Unidades();
           
           // dt = clsMantenimientoBL.Instancia.GetDataCosto_Unidades(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //  dtpFechaFin.Value.ToShortDateString() + " 23:59:59");
            if (dt.Rows.Count > 0)
            {
                dtgvDataCosto.DataSource = dt;
                //gridView2.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                //gridView2.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                gridView2.Columns["MONTO TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView2.Columns["MONTO TOTAL"].DisplayFormat.FormatString = "c2";
                //Monto de Precio Unitario
                gridView2.Columns["PRECIO UNITARIO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "PRECIO UNITARIO", "PrecioUnitario={0:c2}");
                gridView2.Columns["PRECIO UNITARIO"].SummaryItem.Tag = 1;
                //Monto de Precio Unitario Dolar
                gridView2.Columns["PRECIO UNITARIO DOLAR"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "PRECIO UNITARIO DOLAR", "PrecioUnitarioDolar={0:c2}");
                gridView2.Columns["PRECIO UNITARIO DOLAR"].SummaryItem.Tag = 2;
                //Monto Total
                gridView2.Columns["MONTO TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO TOTAL", "MontoTotal={0:c2}");
                gridView2.Columns["MONTO TOTAL"].SummaryItem.Tag = 3;
                //Monto Total Dolar 
                gridView2.Columns["MONTO TOTAL DOLAR"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO TOTAL DOLAR", "MontoTotalDolar={0:c2}");
                gridView2.Columns["MONTO TOTAL DOLAR"].SummaryItem.Tag = 4;
                //Numero de Proyectos
                gridView2.Columns["PROYECTOS"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "PROYECTOS", "proyectos={0}");
                gridView2.Columns["PROYECTOS"].SummaryItem.Tag = 5;
                //dtoProductos.ProductosRow rowTotal = datos.Productos.NewProductosRow();
                //datos.Productos.Rows.Add(rowTotal);  
               // DataGridViewRow row = gridView2.Rows[gridView2.Rows.Count - 1];
              //  row.ReadOnly = true;
                gridView2.UpdateSummary();
                gridView2.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void gridView2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                sumaPrecioUnitario = 0;
                sumaPrecioUnitarioDolar = 0;
                sumaMontoTotal = 0;
                sumaMontoTotalDolar = 0;
                proyectos = 0;

            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        if (View.GetRowCellValue(e.RowHandle, "PrecioUnitario").ToString() == "SI") sumaPrecioUnitario += Convert.ToDecimal(e.FieldValue);
                        break;
                    case 2:
                        if (View.GetRowCellValue(e.RowHandle, "PrecioUnitarioDolar").ToString() == "SI") sumaPrecioUnitarioDolar += Convert.ToDecimal(e.FieldValue);
                        break;
                    case 3:
                        if (View.GetRowCellValue(e.RowHandle, "MontoTotal").ToString() == "SI") sumaMontoTotal += Convert.ToDecimal(e.FieldValue);
                        break;
                    case 4:
                        if (View.GetRowCellValue(e.RowHandle, "MontoTotalDolar").ToString() == "SI") sumaMontoTotalDolar += Convert.ToDecimal(e.FieldValue);
                        break;
                    case 5:
                        if (View.GetRowCellValue(e.RowHandle, "Proyectos").ToString() != "") { proyectos = proyectos + 1; }
                        break;
                }
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = sumaPrecioUnitario;
                        break;
                    case 2:
                        e.TotalValue = sumaPrecioUnitarioDolar;
                        break;
                    case 3:
                        e.TotalValue = sumaMontoTotal;
                        break;
                    case 4:
                        e.TotalValue = sumaMontoTotalDolar;
                        break;
                    case 5:
                        e.TotalValue = proyectos;
                        break;
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvDataCosto.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Costo de Mantenimiento del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") +
                    " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " +
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvDataCosto.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvDataCosto.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvDataCosto.ShowPrintPreview();
            }
        }

    }
}
