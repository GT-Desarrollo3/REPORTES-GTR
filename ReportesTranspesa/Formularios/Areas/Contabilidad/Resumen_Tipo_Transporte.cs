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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraPivotGrid;
using DevExpress.LookAndFeel;
//using System.Data.OleDb;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class Resumen_Tipo_Transporte : MetroFramework.Forms.MetroForm
    {
        public Resumen_Tipo_Transporte()
        {
            InitializeComponent();
        }

        decimal sumaFacturados;
        decimal sumaNoFacturados;
        int cuentaGuias;
        List<string> codigosviajes;
        List<string> cantidadviajes;
        //int tra = 0, bra = 0, altra = 0;

        private void Resumen_Tipo_Transporte_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string tipofecha;
            if (rbFechaProg.Checked)
            {
                tipofecha = "PROGRAMADA";
            }
            else
            {
                tipofecha = "CREACION";
            }
            //switch (cboCompañia.SelectedIndex)
            //{
            //    case 0: cboCompañia.Text = "";
            //        break;
            //    case 1: tra = 10000000;
            //        //cboCompañia.Text = "10000000";
            //        //var tra = Convert.ToInt32(cboCompañia.Text);
            //        //tra = Convert.ToInt32(cboCompañia.Text.ToString());
            //        break;
            //    case 2: bra = 40000000;
            //        //cboCompañia.Text = "40000000";
            //        //var bra = Convert.ToInt32(cboCompañia.Text);
            //        //bra = Convert.ToInt32(cboCompañia.Text.ToString());
            //        break;
            //    case 3: altra = 50000000;
            //        //cboCompañia.Text = "50000000";
            //        //var altra = Convert.ToInt32(cboCompañia.Text);
            //        //altra = Convert.ToInt32(cboCompañia.Text.ToString());
            //        break;
            //}
            //int compañia = 0;
            string compañia = "";
            switch (cboCompañia.SelectedIndex)
            {
                case 0: compañia = "";
                    break;

                case 1: compañia = "10000000";
                    break;

                case 2: compañia = "40000000";
                    break;

                case 3: compañia = "50000000";
                    break;

                case 4: compañia = "60000000";
                    break;

                case 5: compañia = "70000000";
                    break;
            }
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            dtgvDataView.Columns.Clear();
            dtgvData1.DataSource = null;
            System.Data.DataTable dt1 = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            //dt1 = clsContabilidadBL.Instancia.GetTipoServicios(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //    dtpFechaFin.Value.ToShortDateString() + " 23:59:59", tipofecha);
            dt1 = clsContabilidadBL.Instancia.GetTipoServiciosCompañia(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", tipofecha, compañia);
            dt2 = clsContabilidadBL.Instancia.GetTipoServicios(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", tipofecha);
            if (dt1.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvData.DataSource = dt1;
                dtgvData.BestFitRowArea();

                dtgvData1.DataSource = dt2;
                //************************FILTRO*******************************************************
                string filtro = "";
                int contafiltros = 0;
                if (chkSucursal.Checked)
                {
                    filtro = "[SUCURSAL] = '" + cboSucursal.Text + "'";
                    contafiltros = contafiltros + 1;
                }
                if (chkEstado.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[ESTADO] = '" + cboEstado.Text + "'";
                    contafiltros = contafiltros + 1;
                }
                if (chkTipo.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[TIPO] = '" + cboTipo.Text + "'";
                    contafiltros = contafiltros + 1;
                }
                if (chkFacturado.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[FACTURADO] = '" + cboFacturado.Text + "'";
                    contafiltros = contafiltros + 1;
                }
                if (chkTransporte.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[TRANSPORTE] = '" + cboTransporte.Text + "' AND [PROVEEDOR] LIKE '%" + txtTransporte.Text + "%'";
                    contafiltros = contafiltros + 1;
                }
                if (chkServicios.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + "[SERVICIOS] = '" + cboServicio.Text + "'";
                    }
                }
                if (chkCompañia.Checked)
                {
                    if (cboCompañia.SelectedIndex == 0)
                    {
                        cboCompañia.Text = "";
                        if (contafiltros > 0)
                        {
                            filtro = filtro + " AND ";
                        }
                        filtro = filtro + "[COMPAÑIA] IN '" + cboCompañia.Text + "'";
                        contafiltros = contafiltros + 1;
                    }
                    else
                    {
                        if (contafiltros > 0)
                        {
                            filtro = filtro + " AND ";
                        }
                        filtro = filtro + "[COMPAÑIA] = '" + cboCompañia.Text + "'";
                        contafiltros = contafiltros + 1;
                    }
                }
                //**********************FIN DEL FILTRO*************************************************
                if (filtro != "")
                {
                    dtgvDataView.Columns["SUCURSAL"].FilterInfo = new ColumnFilterInfo(filtro);
                }
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                //dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatString = "c2";
                //dtgvDataView.Columns["COSTO"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["COSTO"].DisplayFormat.FormatString = "c2";
                //dtgvDataView.Columns["MONTO FACTURA"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["MONTO FACTURA"].DisplayFormat.FormatString = "c2";
                //******************CALCULO DE MONTOS TOTALES******************************************
                //Lo facturado
                dtgvDataView.Columns["FACTURADO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO", "Facturado={0:c2}");
                dtgvDataView.Columns["FACTURADO"].SummaryItem.Tag = 1;
                //Lo no facturado
                dtgvDataView.Columns["DOCUMENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO", "No Facturado={0:c2}");
                dtgvDataView.Columns["DOCUMENTO"].SummaryItem.Tag = 2;
                //Cantidad de guías

                dtgvDataView.Columns["GUÍA TRANSP."].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "GUÍA TRANSP.", "Guías={0}");
                dtgvDataView.Columns["GUÍA TRANSP."].SummaryItem.Tag = 3;
                //Cantidad de viajes
                dtgvDataView.Columns["CODIGO VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO VIAJE", "Viajes={0}");
                dtgvDataView.Columns["CODIGO VIAJE"].SummaryItem.Tag = 4;

                //dtgvDataView.Columns["CANTIDAD VIAJES"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CANTIDAD VIAJES", "Viajes={0}");
                //dtgvDataView.Columns["CANTIDAD VIAJES"].SummaryItem.Tag = 5;

                dtgvDataView.UpdateSummary();
                //Total
                dtgvDataView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total={0:c2}");
                //dtgvDataView.Columns["MONTO TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO TOTAL", "Total={0:c2}");
                dtgvDataView.Columns["CANTIDAD VIAJES"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD VIAJES", "Total={0}");
                //*******************************************************************************************************************************************************************************************************//
                if (Utilitario.Instancia.SesionUsuario.usuario == "MLOPEZ" || Utilitario.Instancia.SesionUsuario.usuario == "COMBUSTIBLE2" || Utilitario.Instancia.SesionUsuario.usuario == "COMBUSTIBLE3")
                //if (Utilitario.Instancia.SesionUsuario.usuario == "MLOPEZ" || Utilitario.Instancia.SesionUsuario.usuario == "COMBUSTIBLE2" || Utilitario.Instancia.SesionUsuario.usuario == "COMBUSTIBLE3" || Utilitario.Instancia.SesionUsuario.usuario == "LSANDOVAL") 
                {
                    //dtgvDataView.Columns["PROYECTO"].Visible = false;
                    //dtgvDataView.Columns["CENTRO DE COSTOS"].Visible = false;
                    //dtgvDataView.Columns["TARIFA"].Visible = false;
                    dtgvDataView.Columns["MONTO"].Visible = false;
                    //dtgvDataView.Columns["COSTO"].Visible = false;
                    //dtgvDataView.Columns["FACTURADO"].SummaryItem.Assign = 0;
                    dtgvDataView.Columns["FACTURADO"].Summary.Remove(dtgvDataView.Columns["FACTURADO"].SummaryItem);
                    dtgvDataView.Columns["DOCUMENTO"].Summary.Remove(dtgvDataView.Columns["DOCUMENTO"].SummaryItem);
                }
                dtgvDataView.BestFitColumns();
                LimpiaFiltros();
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
            campoPivote = new PivotGridField("TIPO DE SERVICIO", PivotArea.RowArea);
            // Create a row Pivot Grid Control field bound to the Country datasource field.
            PivotGridField fieldViajes = new PivotGridField("TRANSPORTE", PivotArea.RowArea);

            // Create a row Pivot Grid Control field bound to the Sales Person datasource field.
            PivotGridField fieldCustomer = new PivotGridField("SUCURSAL", PivotArea.RowArea);
            fieldCustomer.Caption = "SUCURSAL";

            // Create a row Pivot Grid Control field bound to the Sales Person datasource field.
            //PivotGridField fieldCustomer2 = new PivotGridField("COMPAÑIA", PivotArea.RowArea);
            //fieldCustomer2.Caption = "COMPAÑIA";

            //// Create a column Pivot Grid Control field bound to the OrderDate datasource field.
            //PivotGridField fieldYear = new PivotGridField("AÑO", PivotArea.ColumnArea);
            //fieldYear.Caption = "AÑO";
            //// Group field values by years.
            //fieldYear.GroupInterval = PivotGroupInterval.DateYear;

            //// Create a column Pivot Grid Control field bound to the CategoryName datasource field.
            //PivotGridField fieldCategoryName = new PivotGridField("DIA", PivotArea.ColumnArea);
            //fieldCategoryName.Caption = "DIA";

            // Create a filter Pivot Grid Control field bound to the ProductName datasource field.
            PivotGridField fieldProductName = new PivotGridField("ProductName", PivotArea.FilterArea);
            fieldProductName.Caption = "Product Name";

            // Create a data Pivot Grid Control field bound to the 'Extended Price' datasource field.
            PivotGridField fieldExtendedPrice = new PivotGridField("CANTIDAD VIAJES", PivotArea.DataArea);
            fieldExtendedPrice.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // Specify the formatting setting to format summary values as integer currency amount.
            //fieldExtendedPrice.CellFormat.FormatString = "c0";0'

            // Create a data Pivot Grid Control field bound to the 'Extended Price' datasource field.
            //PivotGridField fieldExtendedPrice2 = new PivotGridField("COMPAÑIA", PivotArea.DataArea);
            //fieldExtendedPrice.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            

            // Add the fields to the control's field collection.         
            dtgvData.Fields.AddRange(new PivotGridField[] {fieldViajes, fieldCustomer, 
            fieldProductName, fieldExtendedPrice});
            //dtgvData.Fields.AddRange(new PivotGridField[] {fieldViajes, fieldCustomer, 
            //    fieldCategoryName, fieldProductName, fieldYear, fieldExtendedPrice});

            // Arrange the row fields within the Row Header Area.
            fieldViajes.AreaIndex = 0;
            fieldCustomer.AreaIndex = 1;

            // Arrange the column fields within the Column Header Area.
            //fieldCategoryName.AreaIndex = 0;
            //fieldYear.AreaIndex = 1;

            // Customize the control's look-and-feel via the Default LookAndFeel object.
            UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";

            if (Utilitario.Instancia.SesionUsuario.usuario == "RMEDINA" || Utilitario.Instancia.SesionUsuario.usuario == "AVELASQUEZ")
            {
                PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
                campoDia.Caption = "Dia";
                //PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
                //campoMes.Caption = "Mes";
                //PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
                //campoAño.Caption = "Año";
                PivotGridField campoTotal = new PivotGridField("MONTO", PivotArea.DataArea);
                //PivotGridField campoTotal = new PivotGridField("MONTO TOTAL", PivotArea.DataArea);
                //PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
                //campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //campoTotal.CellFormat.FormatString = "c2";
                //dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                //campoDia,campoMes,campoAño,campoTotal});
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                campoDia,campoTotal});
                campoPivote.AreaIndex = 0;
                campoDia.AreaIndex = 2;
                //campoMes.AreaIndex = 1;
                //campoAño.AreaIndex = 0;


                //// Create a connection object.
                //OleDbConnection connection =
                //new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\NWIND.MDB");
                //// Create a data adapter.
                //OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM SalesPerson", connection);

                //// Create and fill a dataset.
                //DataSet sourceDataSet = new DataSet();
                //adapter.Fill(sourceDataSet, "SalesPerson");

                //// Assign the data source to the PivotGrid control.
                //pivotGridControl1.DataSource = sourceDataSet.Tables["SalesPerson"];

            }
            else
            {
                PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
                campoDia.Caption = "Dia";
                PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
                campoMes.Caption = "Mes";
                PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
                campoAño.Caption = "Año";
                PivotGridField campoTotal = new PivotGridField("MONTO", PivotArea.DataArea);
                //PivotGridField campoTotal = new PivotGridField("MONTO TOTAL", PivotArea.DataArea);
                //PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
                //campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //campoTotal.CellFormat.FormatString = "c2";
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                campoDia,campoMes,campoAño,campoTotal});
                //dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                ////campoMes,campoTotal});
                //campoDia,campoMes,campoTotal});
                campoPivote.AreaIndex = 0;
                campoDia.AreaIndex = 2;
                campoMes.AreaIndex = 1;
                campoAño.AreaIndex = 0;
            }

        }

        private void LimpiaFiltros()
        {
            chkSucursal.Checked = false;
            chkEstado.Checked = false;
            chkTipo.Checked = false;
            chkFacturado.Checked = false;
            chkTransporte.Checked = false;
        }

        private void chkSucursal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSucursal.Checked)
            {
                cboSucursal.Enabled = true;
                cboSucursal.SelectedIndex = 0;
            }
            else
            {
                cboSucursal.Enabled = false;
                cboSucursal.SelectedIndex = -1;
            }
        }

        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstado.Checked)
            {
                cboEstado.Enabled = true;
                cboEstado.SelectedIndex = 0;
            }
            else
            {
                cboEstado.Enabled = false;
                cboEstado.SelectedIndex = -1;
            }
        }

        private void chkTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTipo.Checked)
            {
                cboTipo.Enabled = true;
                cboTipo.SelectedIndex = 0;
            }
            else
            {
                cboTipo.Enabled = false;
                cboTipo.SelectedIndex = -1;
            }
        }

        private void chkFacturado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFacturado.Checked)
            {
                cboFacturado.Enabled = true;
                cboFacturado.SelectedIndex = 0;
            }
            else
            {
                cboFacturado.Enabled = false;
                cboFacturado.SelectedIndex = -1;
            }
        }

        private void chkTransporte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTransporte.Checked)
            {
                cboTransporte.Enabled = true;
                cboTransporte.SelectedIndex = 0;
                txtTransporte.Enabled = true;
                txtTransporte.Visible = true;
            }
            else
            {
                cboTransporte.Enabled = false;
                cboTransporte.SelectedIndex = -1;
                txtTransporte.Text = "";
                txtTransporte.Enabled = false;
                txtTransporte.Visible = false;
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

        private void dtgvDataView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                sumaFacturados = 0;
                sumaNoFacturados = 0;
                cuentaGuias = 0;
                codigosviajes = new List<string>();
                cantidadviajes = new List<string>();
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        if (View.GetRowCellValue(e.RowHandle, "FACTURADO").ToString() == "SI") sumaFacturados += Convert.ToDecimal(e.FieldValue);
                        break;
                    case 2:
                        if (View.GetRowCellValue(e.RowHandle, "FACTURADO").ToString() == "NO") { sumaNoFacturados += Convert.ToDecimal(e.FieldValue); }
                        break;
                    case 3:
                        if (View.GetRowCellValue(e.RowHandle, "GUÍA TRANSP.").ToString() != "") { cuentaGuias = cuentaGuias + 1; }
                        break;
                    case 4:
                        codigosviajes.Add(View.GetRowCellValue(e.RowHandle, "CODIGO VIAJE").ToString());
                        break;
                    case 5:
                        cantidadviajes.Add(View.GetRowCellValue(e.RowHandle, "CANTIDAD VIAJES").ToString());
                        break;
                }
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = sumaFacturados;
                        break;
                    case 2:
                        e.TotalValue = sumaNoFacturados;
                        break;
                    case 3:
                        e.TotalValue = cuentaGuias;
                        break;
                    case 4:
                        e.TotalValue = codigosviajes.Distinct().Count();
                        break;
                    case 5:
                        e.TotalValue = cantidadviajes.Distinct().Count();
                        break;
                }
            }    
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData1.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Resumen de Viajes por tipo Servicio del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData1.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void chkServicios_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServicios.Checked)
            {
                cboServicio.Enabled = true;
            }
            else
            {
                cboServicio.Enabled = false;
            }
        }

        private void chkCompañia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompañia.Checked)
            {
                cboCompañia.Enabled = true;
                cboCompañia.SelectedIndex = 0;
            }
            else
            {
                cboCompañia.Enabled = false;
                cboCompañia.SelectedIndex = -1;
            }
        }
    }
}
