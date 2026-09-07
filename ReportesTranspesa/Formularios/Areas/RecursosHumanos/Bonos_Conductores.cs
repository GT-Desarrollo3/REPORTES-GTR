using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using System.Globalization;
using System.Collections;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class Bonos_Conductores : MetroFramework.Forms.MetroForm
    {
        public Bonos_Conductores()
        {
            InitializeComponent();
        }
        
        GridSummaryItem summaryItem;
        decimal sumaKM;
        double totalBonos;
        //*****IMPRESION RESUMIDA
        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        int iCellHeight = 0;
        int iTotalWidth = 0;
        int iRow = 0;
        bool bFirstPage = false;
        bool bNewPage = false;
        int iHeaderHeight = 0;
        int paginainicio = 0;
        int paginafin = 0;
        int cuentapagina = 0;
        private void Bonos_Conductores_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetPeriodoBonos();
            cboPeriodo.DataSource = dt;
            cboPeriodo.ValueMember = "Id";
            cboPeriodo.DisplayMember = "Descripcion";
            cboPeriodo.SelectedIndex = dt.Rows.Count - 1;
            summaryItem = dtgvDataView.GroupSummary.Add(SummaryItemType.Custom, "KM", null, "| KM={0:0.00}");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvData1.Columns.Clear();
            dtgvData1.DataSource = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetDataBonos(Convert.ToInt32(cboPeriodo.SelectedValue), 1);
            if (dt.Rows.Count > 0)
            {
                //****GRID OCULTO
                dtgvData1.DataSource = clsOperacionesBL.Instancia.GetDataBonos(Convert.ToInt32(cboPeriodo.SelectedValue), 2);
                foreach (DataGridViewColumn dc in dtgvData1.Columns)
                {
                    dc.ReadOnly = true;
                }
                //***GRID PRINCIPAL*******************
                dtgvData.DataSource = dt;
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["CONDUCTOR"], DevExpress.Data.ColumnSortOrder.Descending), 
                }, 1);

                //GridColumn summaryColumn = gridView.Columns["KM"];
                GridColumn firstGroupingColumn = gridView.SortInfo[0].Column;

                dtgvDataView.Columns["KM"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["KM"].DisplayFormat.FormatString = "{0:0.00}";
                totalBonos = 0;
                gridView.GroupSummarySortInfo.Add(summaryItem, ColumnSortOrder.Descending,
                firstGroupingColumn);
                dtgvDataView.BestFitColumns();
                lblBonos.Text = "S/. " + totalBonos.ToString();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Aún no hay bonos para este período.";
                m.ShowDialog();
            }
        }

        private void dtgvDataView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                sumaKM = 0;
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                sumaKM += Convert.ToDecimal(e.FieldValue);
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (sumaKM > 10000 && sumaKM < 10500)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 50";
                    totalBonos = totalBonos + 50;
                    return;
                }
                if (sumaKM > 10500 && sumaKM < 11000)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 100";
                    totalBonos = totalBonos + 100;
                    return;
                }
                if (sumaKM > 11000 && sumaKM < 11500)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 150";
                    totalBonos = totalBonos + 150;
                    return;
                }
                if (sumaKM > 11500 && sumaKM < 12000)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 200";
                    totalBonos = totalBonos + 200;
                    return;
                }
                if (sumaKM > 12000 && sumaKM < 12500)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 250";
                    totalBonos = totalBonos + 250;
                    return;
                }
                if (sumaKM > 12500 && sumaKM < 13000)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 300";
                    totalBonos = totalBonos + 300;
                    return;
                }
                if (sumaKM > 13000 && sumaKM < 13500)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 350";
                    totalBonos = totalBonos + 350;
                    return;
                }
                if (sumaKM > 13500)
                {
                    e.TotalValue = sumaKM.ToString("0.00") + " | S/. 400";
                    totalBonos = totalBonos + 400;
                    return;
                }
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
                string fecha = "";
                string año;
                string mes;

                año = (cboPeriodo.Text).Substring(0, 4);
                mes = (cboPeriodo.Text).Substring(5, 2);

                switch (mes)
                {
                    case "01":
                        fecha = "del 16_12_" + Convert.ToString(Convert.ToInt32(año) - 1) + " al 15_01_" + año;
                        break;
                    case "02":
                        fecha = "del 16_01_" + año + " al 15_02_" + año;
                        break;
                    case "03":
                        fecha = "del 16_02_" + año + " al 15_03_" + año;
                        break;
                    case "04":
                        fecha = "del 16_03_" + año + " al 15_04_" + año;
                        break;
                    case "05":
                        fecha = "del 16_04_" + año + " al 15_05_" + año;
                        break;
                    case "06":
                        fecha = "del 16_05_" + año + " al 15_06_" + año;
                        break;
                    case "07":
                        fecha = "del 16_06_" + año + " al 15_07_" + año;
                        break;
                    case "08":
                        fecha = "del 16_07_" + año + " al 15_08_" + año;
                        break;
                    case "09":
                        fecha = "del 16_08_" + año + " al 15_09_" + año;
                        break;
                    case "10":
                        fecha = "del 16_09_" + año + " al 15_10_" + año;
                        break;
                    case "11":
                        fecha = "del 16_10_" + año + " al 15_11_" + año;
                        break;
                    case "12":
                        fecha = "del 16_11_" + año + " al 15_12_" + año;
                        break;
                }
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de bonos " + fecha + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void btnImprimirResumen_Click(object sender, EventArgs e)
        {
            if (dtgvData1.Columns.Count != 0)
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument1;
                printDialog.UseEXDialog = true;
                printDialog.AllowSomePages = true;
                if (DialogResult.OK == printDialog.ShowDialog())
                {
                    paginainicio = printDialog.PrinterSettings.FromPage;
                    paginafin = printDialog.PrinterSettings.ToPage;
                    printDocument1.DocumentName = "Reporte de Bonos";
                    printDocument1.Print();
                    cuentapagina = 0;
                }
            }
            else
            {
                MessageBox.Show("No hay data para imprimir", "Mensaje");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            cuentapagina++;
            try
            {
                string fecha = "";
                string año;
                string mes;
                año = (cboPeriodo.Text).Substring(0, 4);
                mes = (cboPeriodo.Text).Substring(5, 2);
                switch (mes) 
                {
                    case "01":
                        fecha = "del 16/12/"+Convert.ToString(Convert.ToInt32(año)-1)+" al 15/01/"+año;
                        break;
                    case "02":
                        fecha = "del 16/01/"+año+" al 15/02/"+año;
                        break;
                    case "03":
                        fecha = "del 16/02/" + año + " al 15/03/" + año;
                        break;
                    case "04":
                        fecha = "del 16/03/" + año + " al 15/04/" + año;
                        break;
                    case "05":
                        fecha = "del 16/04/" + año + " al 15/05/" + año;
                        break;
                    case "06":
                        fecha = "del 16/05/" + año + " al 15/06/" + año;
                        break;
                    case "07":
                        fecha = "del 16/06/" + año + " al 15/07/" + año;
                        break;
                    case "08":
                        fecha = "del 16/07/" + año + " al 15/08/" + año;
                        break;
                    case "09":
                        fecha = "del 16/08/" + año + " al 15/09/" + año;
                        break;
                    case "10":
                        fecha = "del 16/09/" + año + " al 15/10/" + año;
                        break;
                    case "11":
                        fecha = "del 16/10/" + año + " al 15/11/" + año;
                        break;
                    case "12":
                        fecha = "del 16/11/" + año + " al 15/12/" + año;
                        break;
                }
                System.Drawing.Font Fuentetitulos = new System.Drawing.Font(dtgvData1.Font.ToString(), 25,
                FontStyle.Bold,GraphicsUnit.Pixel);
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;
                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dtgvData1.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dtgvData1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dtgvData1.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Reporte de bonos " + fecha, Fuentetitulos,
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Reporte de bonos " + fecha, Fuentetitulos, e.MarginBounds.Width).Height - 13);
                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dtgvData1.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.LightGray),
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            // imprimir el total,fecha y usuario
                            if (iRow == dtgvData1.Rows.Count - 1) // si es la ultima fila
                            {
                                if (Cel.ColumnIndex == 2) // si es la ultima celda
                                {
                                    iTopMargin += iCellHeight; // debajo de esa celda -total
                                    e.Graphics.DrawString("Total: " + lblBonos.Text, Cel.InheritedStyle.Font,
                                            new SolidBrush(System.Drawing.Color.Black),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);

                                    e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                                    iTopMargin += iCellHeight; // debajo de esa celda - fecha
                                    String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                                    e.Graphics.DrawString("Fecha: "+strDate, Cel.InheritedStyle.Font,
                                            new SolidBrush(System.Drawing.Color.Black),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);

                                    e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                                    iTopMargin += iCellHeight; // debajo de esa celda - usuario
                                    e.Graphics.DrawString("Usuario: "+ Environment.UserName, Cel.InheritedStyle.Font,
                                            new SolidBrush(System.Drawing.Color.Black),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);

                                    e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));
                                }
                            }
                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                {
                    if (paginainicio == 0 && paginafin == 0) // TODO
                    {
                        e.HasMorePages = true;
                    }
                    else //RANGOS
                    {
                        if (cuentapagina < paginafin)
                        {
                            e.HasMorePages = true;
                        }
                        else
                        {
                            e.HasMorePages = false;
                        }
                    }
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;
                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;
                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dtgvData1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
