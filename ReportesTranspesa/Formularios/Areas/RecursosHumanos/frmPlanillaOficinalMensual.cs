using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using System.Linq;
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
    public partial class frmPlanillaOficinalMensual : MetroFramework.Forms.MetroForm
    {
        public frmPlanillaOficinalMensual()
        {
            InitializeComponent();
        }
        
        GridSummaryItem summaryItem;

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

        private void frmPlanillaOficinalMensual_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetPeriodoRRHH();
            cboPeriodo.DataSource = dt;
            cboPeriodo.ValueMember = "Id";
            cboPeriodo.DisplayMember = "Descripcion";
            cboPeriodo.SelectedIndex = dt.Rows.Count - 1;
            //summaryItem = dtgvDataView.GroupSummary.Add(SummaryItemType.Custom, "SueldoBasico", null, "| SueldoBasico={0:0.00}");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData1.DataSource = null;
            dtgvDataView.Columns.Clear();

            string Compania = "10000000";
            string TipoPlanilla = "TO";
            string TodaPlanilla = "N";

            switch (cboCompañia.SelectedIndex)
            {
                case 0: Compania = "10000000"; break;

                case 1: Compania = "50000000"; break;

                case 2: Compania = "70000000"; break;

                case 3: Compania = "40000000"; break;

                case 4: Compania = "90000000"; break;
            }

            switch (cboPlanilla.SelectedIndex)
            {
                case 0: TipoPlanilla = "TO"; TodaPlanilla = "S"; break;

                case 1: TipoPlanilla = "EM"; TodaPlanilla = "N"; break;

                case 2: TipoPlanilla = "OB"; TodaPlanilla = "N"; break;

                case 3: TipoPlanilla = "CO"; TodaPlanilla = "N"; break;

                case 4: TipoPlanilla = "PR"; TodaPlanilla = "N"; break;
            }
         
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillaOficialMensual(Compania, cboPeriodo.Text , TipoPlanilla, TodaPlanilla);
            if (dt.Rows.Count > 0)
            {
                //***GRID PRINCIPAL*******************
                dtgvData.DataSource = dt;
                dtgvData1.DataSource = dt;
                lblTrabajadores.Text = dt.Rows.Count.ToString();

                dtgvDataView.Columns["SueldoBasico"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["SueldoBasico"].DisplayFormat.FormatString = "{0:0.00}";

                dtgvDataView.Columns[1].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Codigo", "Total={0:n2}");
                dtgvDataView.Columns[1].SummaryItem.Tag = 1; //TAG SEGUN LA SUMATARIA EN CustomSummaryCalculate  CASE 1,2,....
                dtgvDataView.Columns[2].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CESADO", "ActivosTotal={0:n2}");
                dtgvDataView.Columns[2].SummaryItem.Tag = 2; //TAG SEGUN LA SUMATARIA EN CustomSummaryCalculate  CASE 1,2,....
                dtgvDataView.Columns[3].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CESADO", "CesadosTotal={0:n2}");
                dtgvDataView.Columns[3].SummaryItem.Tag = 3;
                dtgvDataView.Columns[4].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "FechaIngreso", "IngresosTotal={0:n2}");
                dtgvDataView.Columns[4].SummaryItem.Tag = 4;

                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Aún no hay Datos para este período. Revise los Filtros";
                m.ShowDialog();
            }
        }

        decimal Total = 0;
        decimal ActivosTotal = 0;
        decimal CesadosTotal = 0;
        decimal IngresosTotal = 0; 
      
        private void dtgvDataView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                Total = 0;
                ActivosTotal = 0;
                CesadosTotal = 0;
                IngresosTotal = 0;
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        Total += 1;
                    break;
                    case 2:
                        if (View.GetRowCellValue(e.RowHandle, "CESADO").ToString()=="NO") { ActivosTotal += 1; }
                    break;
                    case 3:
                        if (View.GetRowCellValue(e.RowHandle, "CESADO").ToString()=="SI") { CesadosTotal += 1; }
                    break;
                    case 4:
                        if (Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, "FechaIngreso")).ToString("yyyyMM") ==cboPeriodo.Text)
                        { IngresosTotal += 1; }
                    break;
                }
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = Total;
                        lblTrabajadores.Text = Total.ToString();
                    break;
                    case 2:
                        e.TotalValue = ActivosTotal;
                        lblActivos.Text = ActivosTotal.ToString();
                    break;
                    case 3:
                        e.TotalValue = CesadosTotal;
                        lblCesados.Text = CesadosTotal.ToString();
                    break;
                    case 4:
                        e.TotalValue = IngresosTotal;
                        lblIngresos.Text = IngresosTotal.ToString();
                    break;
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
                string fecha = cboPeriodo.Text;

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte PlanillaOficial - " + fecha + " " + cboPlanilla.Text + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
            else { dtgvData.ShowPrintPreview(); }
        }

        private void btnImprimirResumen_Click(object sender, EventArgs e)
        {
            
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
                            e.Graphics.DrawString("Reporte de Planilla Oficial " + fecha, Fuentetitulos,
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Reporte de Planilla Oficial " + fecha, Fuentetitulos, e.MarginBounds.Width).Height - 13);
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
                                    e.Graphics.DrawString("Total: " + lblTrabajadores.Text, Cel.InheritedStyle.Font,
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
                    { e.HasMorePages = true; }
                    else //RANGOS
                    {
                        if (cuentapagina < paginafin) { e.HasMorePages = true; }
                        else { e.HasMorePages = false; }
                    }
                }
                else { e.HasMorePages = false; }
            }
            catch (Exception exc) { MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                
                foreach (DataGridViewColumn dgvGridCol in dtgvData1.Columns) { iTotalWidth += dgvGridCol.Width; }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
