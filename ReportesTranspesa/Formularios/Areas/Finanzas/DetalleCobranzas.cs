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
using System.Text.RegularExpressions;
using DevExpress.XtraCharts;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class DetalleCobranzas : MetroFramework.Forms.MetroForm
    {
        public DetalleCobranzas()
        {
            InitializeComponent();
        }

        private void DetalleCobranzas_Load(object sender, EventArgs e)
        {
            txtCliente.Visible = false;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            //dtgvDetalleClientes.Visible = false;
            //dtgvDetalleBancos.Visible = false;
            dtgvDetalleCobranza.Visible = false;
            //DataTable dt = new DataTable();
            //dt = clsFinanzasBL.Instancia.GetClientes();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    //txtClientes.Text = txtClientes.Text.Trim();
            //    txtClientes.Text = "";
            //    txtClientes.Text = dt.Rows[i]["Busqueda"].ToString().Trim();
            //    txtClientes.AutoCompleteCustomSource.Add(txtClientes.Text.Trim());
            //}
            //Filtros();
        }

        private void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCliente.Checked == true)
            {
                lblBanco.Visible = false;
                lblCliente.Visible = true;
                txtClientes.Text = "";
                btnBanco.Visible = false;
                btnCliente.Visible = true;
            }            
        }

        private void rbBancos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBancos.Checked == true)
            {
                lblBanco.Visible = true;
                lblCliente.Visible = false;
                txtClientes.Text = "";
                btnCliente.Visible = false;
                btnBanco.Visible = true;
            }
        }


        private void CreaColumnasPivotGridClientes()
        {
            PivotGridField campoPivote = new PivotGridField();
            campoPivote = new PivotGridField("CLIENTE", PivotArea.RowArea);
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("MONTO", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] { campoPivote, campoDia, campoMes, campoAño, campoTotal });
            campoPivote.AreaIndex = 0;
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void CreaColumnasPivotGridBancos()
        {
            PivotGridField campoPivote = new PivotGridField();
            PivotGridField campoPivote1 = new PivotGridField();
            campoPivote = new PivotGridField("BANCO", PivotArea.RowArea);
            campoPivote1 = new PivotGridField("DESCRIPCION", PivotArea.RowArea);
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("MONTOPAGADO", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] { campoPivote, campoPivote1, 
            campoDia, campoMes, campoAño, campoTotal });
            campoPivote.AreaIndex = 0;
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            //dtgvDetalleCobranza.DataSource = null;
            string fechaini = "";
            string fechafin = "";
            //fechaini = dtpFechaIni.Value.ToShortDateString();
            //fechafin = dtpFechaFin.Value.ToShortDateString();
            //fechaini = Regex.Replace(dtpFechaIni.Value.ToShortDateString(), @"[a.m.p.m.]", "") + " 00:00:00";
            //fechafin = Regex.Replace(dtpFechaFin.Value.ToShortDateString(), @"[a.m.p.m.]", "") + " 23:59:59";
            fechaini = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            string cliente = "";
            cliente = txtClientes.Text.Trim();
            string banco = "";
            banco = txtClientes.Text.Trim();
            //cliente = txtCliente.Text.Trim();
            //var f1 = fechaini.Substring(0,11);
            //var f2 = fechafin.Substring(0,11);
            if (rbCliente.Checked == true)
            {
                #region Clientes
                //dtgvDetalleCobranza.DataSource = null;
                dtgvDetalleClientes.DataSource = null;
                DataTable dt1 = new DataTable();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsFinanzasBL.Instancia.GetListaCobranzasxClientes(fechaini, fechafin, cliente);
                dt1 = clsFinanzasBL.Instancia.GetListaClientesCobranzaDetallado(fechaini, fechafin, cliente);
                //dt1 = clsFinanzasBL.Instancia.GetListaClientesCobranzaDetallado(f1, f2);
                //dt1 = clsFinanzasBL.Instancia.GetListaClientesCobranzaDetallado(fechaini, fechafin, cliente);
                if (dt.Rows.Count > 0 || dt1.Rows.Count > 0)
                {
                    CreaColumnasPivotGridClientes();
                    dtgvData.DataSource = dt;
                    //dtgvDetalleCobranza.DataSource = dt1;
                    //dtgvDetalleCobranzaView.BestFitColumns();
                    dtgvDetalleClientes.DataSource = dt1;
                    dtgvViewDetalleClientes.BestFitColumns();
                    GridView gridView = dtgvDetalleClientes.FocusedView as GridView;
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    gridView.GroupSummary.Clear();
                    GridGroupSummaryItem item = new GridGroupSummaryItem();
                    item.FieldName = "MONTOPAGADO";
                    item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    gridView.GroupSummary.Add(item);
                    dtgvViewDetalleClientes.ExpandAllGroups();
                    dtgvData.BestFitRowArea();
                    chartControl1.DataSource = dtgvData;
                    chartControl1.PivotGridDataSourceOptions.MaxAllowedPointCountInSeries = 100;
                    chartControl1.PivotGridDataSourceOptions.MaxAllowedSeriesCount = 100;
                    chartControl1.PivotGridDataSourceOptions.RetrieveDataByColumns = false;
                    chartControl1.CrosshairEnabled = DefaultBoolean.False;
                    chartControl1.ToolTipEnabled = DefaultBoolean.True;
                    ToolTipController controller = new ToolTipController();
                    chartControl1.ToolTipController = controller;
                    controller.ShowBeak = true;
                    //ChartControl rangeAreaChart = new ChartControl();
                    //((XYDiagram)rangeAreaChart.Diagram).AxisX.GridLines.Visible = true;
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hubo resultados";
                    m.ShowDialog();
                }
                #endregion
            }
            else
            {
                #region Bancos
                //dtgvDetalleCobranza.DataSource = null;
                dtgvDetalleBancos.DataSource = null;
                DataTable dt2 = new DataTable();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsFinanzasBL.Instancia.GetListaCobranzasxBancos(fechaini, fechafin, banco);
                dt2 = clsFinanzasBL.Instancia.GetListaBancosCobranzaDetallado(fechaini, fechafin, banco);
                if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
                {
                    CreaColumnasPivotGridBancos();
                    dtgvData.DataSource = dt;
                    //dtgvDetalleCobranza.DataSource = dt2;
                    //dtgvDetalleCobranzaView.BestFitColumns();
                    dtgvDetalleBancos.DataSource = dt2;
                    dtgvViewDetalleBancos.BestFitColumns();
                    GridView gridView = dtgvDetalleBancos.FocusedView as GridView;
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["DESCRIPCION"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    gridView.GroupSummary.Clear();
                    GridGroupSummaryItem item = new GridGroupSummaryItem();
                    item.FieldName = "MONTOPAGADO";
                    item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    gridView.GroupSummary.Add(item);
                    dtgvViewDetalleBancos.ExpandAllGroups();
                    dtgvData.BestFitRowArea();
                    chartControl1.DataSource = dtgvData;
                    chartControl1.PivotGridDataSourceOptions.MaxAllowedPointCountInSeries = 100;
                    chartControl1.PivotGridDataSourceOptions.MaxAllowedSeriesCount = 100;
                    chartControl1.PivotGridDataSourceOptions.RetrieveDataByColumns = false;
                    chartControl1.CrosshairEnabled = DefaultBoolean.False;
                    chartControl1.ToolTipEnabled = DefaultBoolean.True;
                    ToolTipController controller = new ToolTipController();
                    chartControl1.ToolTipController = controller;
                    controller.ShowBeak = true;
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hubo resultados";
                    m.ShowDialog();
                }
                #endregion
            }
        }

        private void btnExcelDetalle_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                if (rbCliente.Checked == true)
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Lista de Cobranzas por Clientes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Lista de Cobranzas por Bancos " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                //CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                //DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                //dtfi.TimeSeparator = ".";
                //string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //string nombre = System.IO.Path.Combine(desktop, "Lista de Cobranzas por Clientes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                //dtgvData.ExportToXlsx(nombre);
                //Process.Start(nombre);
            }

            if (rbCliente.Checked == true)
            {
                #region Detalle Clientes
                if (dtgvDetalleClientes.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Lista de Cobranzas Detalle por Clientes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvDetalleClientes.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                #endregion

            }
            
            #region Detalle Bancos
            if (dtgvDetalleBancos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Cobranzas Detalle por Bancos " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvDetalleBancos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            #endregion
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

        private void btnCliente_Click(object sender, EventArgs e)
        {
            ListaClientes frm = new ListaClientes();
            frm.Show();
            this.Close();
        }

        private void btnBanco_Click(object sender, EventArgs e)
        {
            ListaBancos frm = new ListaBancos();
            frm.Show();
            this.Close();
        }

        private void rbResumido_CheckedChanged(object sender, EventArgs e)
        {
            if (rbResumido.Checked == true)
            {
                dtgvData.Visible = true;
                dtgvDetalleCobranza.Visible = false;
                dtgvDetalleClientes.Visible = false;
                dtgvDetalleBancos.Visible = false;
            }            
        }

        private void rbDetallado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDetallado.Checked == true)
            {
                //dtgvDetalleCobranza.Visible = true;
                //dtgvData.Visible = false;
                if (rbCliente.Checked == true && rbDetallado.Checked == true)
                {
                    dtgvDetalleClientes.Visible = true;
                    dtgvData.Visible = false;
                }
                else
                {
                    if (rbBancos.Checked == true)
                    {
                        dtgvDetalleBancos.Visible = true;
                        dtgvData.Visible = false;
                    }
                }
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

        private void txtClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }
    }
}
