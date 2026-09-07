using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using DevExpress.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class ListaViajes : Form//MetroFramework.Forms.MetroForm
    {
        public ListaViajes()
        {
            InitializeComponent();
           
        }

        string fechaini;
        string fechafin;
        string transporte;
        List<string> codigosviajes;

        private void ListaViajes_Load(object sender, EventArgs e)
        {
            Periodos();
            cboPeriodo.SelectedIndex = 0;
            cboServicio.SelectedIndex = 0;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            txtCantidad.ReadOnly = true;
            txtMontoTotal.ReadOnly = true;
            splitContainer2.Panel2Collapsed = true;
        }
      

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
            splitContainer2.Panel2Collapsed = true;

            if (chkFecha.Checked == true)
                {
                    BuscarPorFecha();
                }
            else
                {
                    BuscarPorPeriodo();
                }
        }
        private void BuscarPorFecha()
        {
            string cliente = "";
            cliente = txtCliente.Text;
            string ruta = "";
            ruta = txtRuta.Text;
            string producto = "";
            producto = txtProducto.Text;
            string unidad = "";
            switch (cboServicio.SelectedIndex)
            {
                case 0: //Completado
                    transporte = "P";
                    break;
                case 1: //Ejecucion
                    transporte = "T";
                    break;
            }
            int EstadoViajes;
            if (rbViajesProceso.Checked)
            {
                EstadoViajes = 0;
            }
            else
            {
                if (rbViajesProceso.Checked)
                {
                    EstadoViajes = 1;
                }
                else
                {
                    EstadoViajes = 2;
                }
                
            }
            int TipoTransporte;
            TipoTransporte = cboServicio.SelectedIndex;

            unidad = txtUnidad.Text;
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            dtgvViajes.DataSource = null;
            dtgvViajesView.Columns.Clear();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            System.Data.DataTable dt3 = new System.Data.DataTable();
            dt1 = clsOperacionesBL.Instancia.GetViajes_Detallado(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", transporte, cliente, ruta, producto, unidad, EstadoViajes, TipoTransporte);
            //dt1 = clsOperacionesBL.Instancia.GetLista_Viajes_Detalle(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //    dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente, ruta, producto, unidad);
            dt3 = clsOperacionesBL.Instancia.GetViajes_Detallado(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", transporte, cliente, ruta, producto, unidad, EstadoViajes,TipoTransporte);
            //dt3 = clsOperacionesBL.Instancia.GetLista_Viajes_Detalle(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //    dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente, ruta, producto, unidad);
            if (dt1.Rows.Count > 0 && dt3.Rows.Count > 0)
            {
                CreaColumnasPivotGridDetalle();
                dtgvData.DataSource = dt1;
                dtgvData.BestFitRowArea();
                dtgvViajes.DataSource = dt3;
                GridView gridView = dtgvViajes.FocusedView as GridView;
                dtgvViajesView.Columns["AÑO"].Visible = false;
                dtgvViajesView.Columns["MES"].Visible = false;
                dtgvViajesView.Columns["DIA"].Visible = false;
                dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                dtgvViajesView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Monto Total ={0:C2}");
                dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CANTIDAD", "Total ={0}");
                //Cantidad de viajes
                dtgvViajesView.Columns["CODIGO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO", "Viajes={0}");
                //gridView.SortInfo.Clear();
                if (rbCliente.Checked)
                {
                    gridView.SortInfo.Clear();
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    gridView.GroupSummary.Clear();
                    GridGroupSummaryItem item = new GridGroupSummaryItem();
                    item.FieldName = "VIAJE";
                    item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    gridView.GroupSummary.Add(item);
                    dtgvViajesView.ExpandAllGroups();
                }
                if (rbRutas.Checked)
                {
                    gridView.SortInfo.Clear();
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["RUTAS"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    //GridGroupSummaryItem item = new GridGroupSummaryItem();
                    //item.FieldName = "RUTAS";
                    //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    //gridView.GroupSummary.Add(item);
                    dtgvViajesView.ExpandAllGroups();
                }
                if (rbProducto.Checked)
                {
                    gridView.SortInfo.Clear();
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["PRODUCTO"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    //GridGroupSummaryItem item = new GridGroupSummaryItem();
                    //item.FieldName = "PRODUCTO";
                    //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    //gridView.GroupSummary.Add(item);
                    dtgvViajesView.ExpandAllGroups();
                }
                if (rbUnidad.Checked)
                {
                    gridView.SortInfo.Clear();
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["UNIDAD"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    //GridGroupSummaryItem item = new GridGroupSummaryItem();
                    //item.FieldName = "UNIDAD";
                    //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    //gridView.GroupSummary.Add(item);
                    dtgvViajesView.ExpandAllGroups();
                }
                if (rbTransporte.Checked)
                {
                    if (cboServicio.SelectedIndex == 0)
                    {
                        dtgvViajes.DataSource = null;
                        dtgvViajesView.Columns.Clear();
                        System.Data.DataTable dt2 = new System.Data.DataTable();
                        dt2 = clsOperacionesBL.Instancia.GetViajes_Detallado_Tipo_Transporte(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                        dtpFechaFin.Value.ToShortDateString() + " 23:59:59", transporte, cliente, ruta, producto, unidad);
                        if (dt2.Rows.Count > 0)
                        {
                            //CreaColumnasPivotGridDetalle();
                            //dtgvData.DataSource = dt1;
                            //dtgvData.BestFitRowArea();
                            dtgvViajes.DataSource = dt2;
                            dtgvViajesView.Columns["AÑO"].Visible = false;
                            dtgvViajesView.Columns["MES"].Visible = false;
                            dtgvViajesView.Columns["DIA"].Visible = false;
                            dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                            dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                            dtgvViajesView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Monto Total ={0:C2}");
                            dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                            dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CANTIDAD", "Total ={0}");
                        }
                        gridView.SortInfo.Clear();
                        //gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                        //new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                        //}, 1);
                        gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                        new GridColumnSortInfo(gridView.Columns["TRANSPORTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                        }, 1);
                        gridView.GroupSummary.Clear();
                        GridGroupSummaryItem item = new GridGroupSummaryItem();
                        item.FieldName = "TRANSPORTE";
                        item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        gridView.GroupSummary.Add(item);
                        dtgvViajesView.ExpandAllGroups();
                    }
                    else
                    {
                        //CreaColumnasPivotGridDetalle();
                        //dtgvData.DataSource = dt1;
                        //dtgvData.BestFitRowArea();
                        dtgvViajes.DataSource = null;
                        dtgvViajesView.Columns.Clear();
                        System.Data.DataTable dt2 = new System.Data.DataTable();
                        dt2 = clsOperacionesBL.Instancia.GetViajes_Detallado_Tipo_Transporte(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                        dtpFechaFin.Value.ToShortDateString() + " 23:59:59", transporte, cliente, ruta, producto, unidad);
                        if (dt2.Rows.Count > 0)
                        {
                            dtgvViajes.DataSource = dt2;
                            dtgvViajesView.Columns["AÑO"].Visible = false;
                            dtgvViajesView.Columns["MES"].Visible = false;
                            dtgvViajesView.Columns["DIA"].Visible = false;
                            dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                            dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                            dtgvViajesView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Monto Total ={0:C2}");
                            dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                            dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CANTIDAD", "Total ={0}");
                        }
                        gridView.SortInfo.Clear();
                        //gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                        //new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                        //}, 1);
                        gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                        new GridColumnSortInfo(gridView.Columns["TRANSPORTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                        }, 1);
                        gridView.GroupSummary.Clear();
                        GridGroupSummaryItem item = new GridGroupSummaryItem();
                        item.FieldName = "TRANSPORTE";
                        item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        gridView.GroupSummary.Add(item);
                        dtgvViajesView.ExpandAllGroups();
                    }
                }
                if (rbLindley.Checked)
                {
                    Operacion_Lindley();
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(gridView.Columns["CDA"], DevExpress.Data.ColumnSortOrder.Ascending), 
                    }, 1);
                    //GridGroupSummaryItem item = new GridGroupSummaryItem();
                    //item.FieldName = "VIAJE";
                    //gridView.GroupSummary.Clear();
                    //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                    //gridView.GroupSummary.Add(item);
                    dtgvViajesView.ExpandAllGroups();
                }
                dtgvViajesView.BestFitColumns();
                chartControl1.DataSource = dtgvData;
                chartControl1.PivotGridDataSourceOptions.MaxAllowedPointCountInSeries = 100;
                chartControl1.PivotGridDataSourceOptions.MaxAllowedSeriesCount = 100;
                chartControl1.PivotGridDataSourceOptions.RetrieveDataByColumns = false;
                chartControl1.CrosshairEnabled = DefaultBoolean.False;
                chartControl1.ToolTipEnabled = DefaultBoolean.True;
                ToolTipController controller = new ToolTipController();
                chartControl1.ToolTipController = controller;
                controller.ShowBeak = true;
                var Cantidad = dtgvViajesView.Columns["CANTIDAD"].SummaryItem.SummaryValue;
                txtCantidad.EditValue = Cantidad;
                var MontoFacturado = dtgvViajesView.Columns["MONTO"].SummaryItem.SummaryValue;
                string monto = Convert.ToString(MontoFacturado).Substring(0, 9);
                string s2 = string.Format("{0:C2}", float.Parse(monto));
                txtMontoTotal.EditValue = s2;
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void BuscarPorPeriodo()
        {
            string cliente = "";
            cliente = txtCliente.Text;
            string ruta = "";
            ruta = txtRuta.Text;
            string periodo = "";
            periodo = cboPeriodo.Text;
            #region Periodo
           
            #endregion
            dtgvViajes.DataSource = null;
            dtgvViajesView.Columns.Clear();
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetLista_Viajes_Por_Periodo(periodo, cliente, ruta, 'R');
            //dt = clsOperacionesBL.Instancia.GetLista_Viajes(periodo, cliente, ruta, 'R');
            dt2 = clsOperacionesBL.Instancia.GetLista_Viajes_Por_Periodo(periodo, cliente, ruta, 'D');
  
            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvViajes.DataSource = dt;
                dtgvData.DataSource = dt2;
                dtgvData.BestFitRowArea();
                dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                dtgvViajesView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Monto Total ={0:C2}");
                dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total ={0}");
                //Cantidad de viajes
                dtgvViajesView.Columns["CODIGO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO", "Viajes={0}");
                dtgvViajesView.BestFitColumns();
                //dtgvData.Appearance.RowHeaderArea.ForeColor = Color.Cyan;
                chartControl1.DataSource = dtgvData;
                chartControl1.PivotGridDataSourceOptions.MaxAllowedPointCountInSeries = 100;
                chartControl1.PivotGridDataSourceOptions.MaxAllowedSeriesCount = 100;
                chartControl1.PivotGridDataSourceOptions.RetrieveDataByColumns = false;
                chartControl1.CrosshairEnabled = DefaultBoolean.False;
                chartControl1.ToolTipEnabled = DefaultBoolean.True;
                ToolTipController controller = new ToolTipController();
                chartControl1.ToolTipController = controller;
                controller.ShowBeak = true;
                var Cantidad = dtgvViajesView.Columns["CANTIDAD"].SummaryItem.SummaryValue;
                txtCantidad.EditValue = Cantidad;
                var MontoFacturado = dtgvViajesView.Columns["MONTO"].SummaryItem.SummaryValue;
                string monto = Convert.ToString(MontoFacturado).Substring(0, 9);
                string s2 = string.Format("{0:C2}", float.Parse(monto));
                txtMontoTotal.EditValue = s2;
                //txtMontoTotal.EditValue = MontoFacturado;
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }

        }

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            PivotGridField campoPivote2 = new PivotGridField();
            campoPivote = new PivotGridField("CLIENTE", PivotArea.RowArea);
            if (Utilitario.Instancia.SesionUsuario.usuario == "RMEDINA" || Utilitario.Instancia.SesionUsuario.usuario == "AVELASQUEZ")
            {
                PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
                campoDia.Caption = "Dia";
                //PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
                //campoMes.Caption = "Mes";
                //PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
                //campoAño.Caption = "Año";
                PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
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
            }
            else
            {
                PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
                campoDia.Caption = "Dia";
                PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
                campoMes.Caption = "Mes";
                //PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
                //campoAño.Caption = "Año";
                PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
                //campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //campoTotal.CellFormat.FormatString = "c2";
                //dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                //campoDia,campoMes,campoAño,campoTotal});
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                campoDia,campoMes,campoTotal});
                campoPivote.AreaIndex = 0;
                campoDia.AreaIndex = 1;
                campoMes.AreaIndex = 0;
                //campoAño.AreaIndex = 0;
            }

        }


        private void CreaColumnasPivotGridDetalle()
        {
            PivotGridField campoPivote = new PivotGridField();
            PivotGridField campoPivote2 = new PivotGridField();
            PivotGridField campoPivote3 = new PivotGridField();
            PivotGridField campoPivote4 = new PivotGridField();
            PivotGridField campoPivote5 = new PivotGridField();
            PivotGridField campoPivote6 = new PivotGridField();
            //campoPivote = new PivotGridField("CLIENTE", PivotArea.RowArea);
            //campoPivote2 = new PivotGridField("RUTAS", PivotArea.RowArea);
            if (rbCliente.Checked == true)
            {
                campoPivote = new PivotGridField("CLIENTE", PivotArea.RowArea);
            }
            else
            {
                if (rbRutas.Checked == true)
                {
                    campoPivote2 = new PivotGridField("RUTAS", PivotArea.RowArea);
                }
                else
                {
                    if (rbProducto.Checked == true)
                    {
                        campoPivote3 = new PivotGridField("PRODUCTO", PivotArea.RowArea);
                    }
                    else
                    {
                        if (rbUnidad.Checked == true)
                        {
                            campoPivote4 = new PivotGridField("UNIDAD", PivotArea.RowArea);
                        }
                        else
                        {
                            if (rbLindley.Checked == true)
                            {
                                campoPivote5 = new PivotGridField("CDA", PivotArea.RowArea);
                            }
                            else
                            {
                                if (rbTransporte.Checked == true)
                                {
                                    campoPivote6 = new PivotGridField("TRANSPORTE", PivotArea.RowArea);
                                }
                            }
                            //campoPivote5 = new PivotGridField("CDA", PivotArea.RowArea);
                            //if(chkLindley.Checked == true)
                            //{
                            //    campoPivote5 = new PivotGridField("CDA", PivotArea.RowArea);
                            //}
                        }    
                    }

                    //if (rbProducto.Checked == true)
                    //{
                    //    campoPivote3 = new PivotGridField("PRODUCTO", PivotArea.RowArea);
                    //}
                    //else
                    //{
                    //    campoPivote4 = new PivotGridField("UNIDAD", PivotArea.RowArea);
                    //}
                }
            }
            if (Utilitario.Instancia.SesionUsuario.usuario == "RMEDINA" || Utilitario.Instancia.SesionUsuario.usuario == "AVELASQUEZ")
                //if (Utilitario.Instancia.SesionUsuario.usuario == "RMEDINA" || Utilitario.Instancia.SesionUsuario.usuario == "AVELASQUEZ" || Utilitario.Instancia.SesionUsuario.usuario == "JBOBADILLA")
            {
                //PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
                //campoDia.Caption = "Dia";
                PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
                campoMes.Caption = "Mes";
                //PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
                //campoAño.Caption = "Año";
                PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
                //campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //campoTotal.CellFormat.FormatString = "c2";
                //dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote,campoDia,campoMes,campoAño,campoTotal});
                //dtgvData.Fields.AddRange(new PivotGridField[] { campoPivote, campoPivote2, campoPivote3, campoPivote4,
                //campoMes, campoAño, campoTotal });
                dtgvData.Fields.AddRange(new PivotGridField[] { campoPivote, campoPivote2, campoPivote3, campoPivote4, campoPivote5, campoPivote6,
                campoMes, campoTotal });
                campoPivote.AreaIndex = 0;
                //campoDia.AreaIndex = 2;
                campoMes.AreaIndex = 1;
                //campoAño.AreaIndex = 0;
            }
            else
            {
                //PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
                //campoDia.Caption = "Dia";
                PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
                campoMes.Caption = "Mes";
                PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
                campoAño.Caption = "Año";
                PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
                //campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //campoTotal.CellFormat.FormatString = "c2";
                //dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote,campoDia,campoMes,campoAño,campoTotal});
                dtgvData.Fields.AddRange(new PivotGridField[] { campoPivote, campoPivote2, campoPivote3, campoPivote4, campoPivote5, campoPivote6,
                campoMes, campoAño, campoTotal });
                campoPivote.AreaIndex = 0;
                //campoDia.AreaIndex = 2;
                campoMes.AreaIndex = 1;
                campoAño.AreaIndex = 0;
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            //--------------------------------------- RESUMEN DE VIAJES-----------------------------------------------------------------------//
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
                string nombre = System.IO.Path.Combine(desktop, "Cantidad de Viajes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvViajes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvViajes.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvViajes.ShowPrintPreview();
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

            //dtgvData.Appearance.FieldHeader.BackColor = Color.Cyan;
            dtgvData.Appearance.FocusedCell.BackColor = Color.Cyan;
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFecha.Checked == true)
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
                fechaini = dtpFechaIni.Value.ToShortDateString();
                fechafin = dtpFechaFin.Value.ToShortDateString();
                gbFiltro.Visible = true;
                lblPeriodo.Enabled = false;
                cboPeriodo.Enabled = false;
                btnExcelDetalle.Visible = true;
                btnExcel.Visible = false;
                gbServicios.Visible = true;

            }
            else
            {
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
                fechaini = "01/01/1950";
                fechafin = "30/12/2050";
                gbFiltro.Visible = false;
                lblPeriodo.Enabled = true;
                cboPeriodo.Enabled = true;
                btnExcelDetalle.Visible = false;
                btnExcel.Visible = true;
                gbServicios.Visible = false;
            }
        }

        private void btnExcelDetalle_Click(object sender, EventArgs e)
        {
            //--------------------------------------- DETALLE DE VIAJES - RESUMEN-----------------------------------------------------------------------//
            #region Resumen
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
                string nombre = System.IO.Path.Combine(desktop, "Detalle - Cantidad de Viajes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                //chartControl1.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            #endregion 
            //--------------------------------------- DETALLE DE VIAJES - DETALLADO-----------------------------------------------------------------------//
            #region Detallado
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
                string nombre = System.IO.Path.Combine(desktop, "Detalle - Cantidad de Viajes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvViajes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            #endregion 
            //--------------------------------------- DETALLE DE VIAJES - GRAFICO-----------------------------------------------------------------------//
            #region Grafico
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
                string nombre = System.IO.Path.Combine(desktop, "Garfico - Cantidad de Viajes " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                chartControl1.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            #endregion 
        }

        public void Operacion_Lindley()
        { 
            string cliente = "";
            cliente = txtCliente.Text;
            string ruta = "";
            ruta = txtRuta.Text;
            string producto = "";
            producto = txtProducto.Text;
            string unidad = "";
            unidad = txtUnidad.Text;
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            dtgvViajes.DataSource = null;
            dtgvViajesView.Columns.Clear();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt2 = clsOperacionesBL.Instancia.GetLista_Operacion_Lindley(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente, ruta, producto, unidad);
            if (dt2.Rows.Count > 0)
            {
                CreaColumnasPivotGridDetalle();
                dtgvData.DataSource = dt2;
                dtgvData.BestFitRowArea();
                dtgvViajes.DataSource = dt2;
                GridView gridView = dtgvViajes.FocusedView as GridView;
                dtgvViajesView.Columns["AÑO"].Visible = false;
                dtgvViajesView.Columns["MES"].Visible = false;
                dtgvViajesView.Columns["DIA"].Visible = false;
                dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViajesView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                dtgvViajesView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Monto Total ={0:C2}");
                dtgvViajesView.Columns["CANTIDAD"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViajesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CANTIDAD", "Total ={0}");
                //Cantidad de viajes
                dtgvViajesView.Columns["CODIGO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO", "Viajes={0}");
                var Cantidad = dtgvViajesView.Columns["CANTIDAD"].SummaryItem.SummaryValue;
                txtCantidad.EditValue = Cantidad;
                var MontoFacturado = dtgvViajesView.Columns["MONTO"].SummaryItem.SummaryValue;
                string monto = Convert.ToString(MontoFacturado).Substring(0, 9);
                string s2 = string.Format("{0:C2}", float.Parse(monto));
                txtMontoTotal.EditValue = s2;
                //txtMontoTotal.EditValue = MontoFacturado;
            }
        }

        private void rbTransporte_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTransporte.Checked == true)
            {
                gbServicios.Visible = true;
                cboServicio.Visible = true;
            }
            else
            {
                gbServicios.Visible = false;
                cboServicio.Visible = false;
            }
        }

        public void Periodos()
        {
            DataTable dt;
            dt = clsOperacionesBL.Instancia.GetLista_ListarPeriodosOperaciones();

            cboPeriodo.DataSource = dt;
            cboPeriodo.DisplayMember = "PERIODO";
            cboPeriodo.ValueMember = "IdPeriodo";

            //cboPeriodo.Items.Add("Seleccione:");
            //string[] meses = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            //string[] años = { "2021", "2021" };
            ////string[] años = {"2015","2016","2017","2018","2019","2020","2021","2022","2023","2025"};
            //foreach (var año in años)
            //{
            //    foreach (var mes in meses)
            //    {
            //        var fecha = año + mes;
            //        cboPeriodo.Items.Add(fecha);
            //    }
            //}
            ////////////////////////////////////////////
            #region tablasperiodo
            //DataTable dsmeses = new DataTable("meses");
            ////Agregamos las Columnas codigo y desripcion
            //DataColumn colInt = new DataColumn("Codigo");
            //colInt.DataType = System.Type.GetType("System.Int32");
            //dsmeses.Columns.Add(colInt);
            //DataColumn colString = new DataColumn("Descripcion");
            //colString.DataType = System.Type.GetType("System.String");
            //dsmeses.Columns.Add(colString);
            //dsmeses.Columns.Add("Codigo", typeof(Int16));
            //dsmeses.Columns.Add("Codigo", typeof(String));
            //dsmeses.Columns.Add("Descripcion", typeof(String));

            //Agregamos las filas
            //DataRow myNewRow; 
            //myNewRow = dsmeses.NewRow();
            //myNewRow["Codigo"] = 01;
            //myNewRow["Descripcion"] = "Enero";
            //myNewRow["Codigo"] = 02;
            //myNewRow["Descripcion"] = "Febrero";
            //myNewRow["Codigo"] = 03;
            //myNewRow["Descripcion"] = "Marzo";
            //myNewRow["Codigo"] = 04;
            //myNewRow["Descripcion"] = "Abril";
            //myNewRow["Codigo"] = 05;
            //myNewRow["Descripcion"] = "Mayo";
            //myNewRow["Codigo"] = 06;
            //myNewRow["Descripcion"] = "Junio";
            //myNewRow["Codigo"] = 07;
            //myNewRow["Descripcion"] = "Julio";
            //myNewRow["Codigo"] = 08;
            //myNewRow["Descripcion"] = "Agosto";
            //myNewRow["Codigo"] = 09;
            //myNewRow["Descripcion"] = "Setimebre";
            //myNewRow["Codigo"] = 10;
            //myNewRow["Descripcion"] = "Octubre";
            //myNewRow["Codigo"] = 11;
            //myNewRow["Descripcion"] = "Noviembre";
            //myNewRow["Codigo"] = 12;
            //myNewRow["Descripcion"] = "Diciembre";
            //dsmeses.Rows.Add(myNewRow);
            //dsmeses.Rows.Add(new object[] { "01", "Enero" });
            //dsmeses.Rows.Add(new object[] { "02", "Febrero" });
            //dsmeses.Rows.Add(new object[] { "03", "Marzo" });
            //dsmeses.Rows.Add(new object[] { "04", "Abril" });
            //dsmeses.Rows.Add(new object[] { "05", "Mayo" });
            //dsmeses.Rows.Add(new object[] { "06", "Junio" });
            //dsmeses.Rows.Add(new object[] { "07", "Julio" });
            //dsmeses.Rows.Add(new object[] { "08", "Agosto" });
            //dsmeses.Rows.Add(new object[] { "09", "Setiembre" });
            //dsmeses.Rows.Add(new object[] { "10", "Octubre" });
            //dsmeses.Rows.Add(new object[] { "11", "Noviembre" });
            //dsmeses.Rows.Add(new object[] { "12", "Dicimebre" });
            //dsmeses.Rows.Add(new object[] { 01, "Enero" });
            //dsmeses.Rows.Add(new object[] { 02, "Febrero" });
            //dsmeses.Rows.Add(new object[] { 03, "Marzo" });
            //dsmeses.Rows.Add(new object[] { 04, "Abril" });
            //dsmeses.Rows.Add(new object[] { 05, "Mayo" });
            //dsmeses.Rows.Add(new object[] { 06, "Junio" });
            //dsmeses.Rows.Add(new object[] { 07, "Julio" });
            //dsmeses.Rows.Add(new object[] { 08, "Agosto" });
            //dsmeses.Rows.Add(new object[] { 09, "Setiembre" });
            //dsmeses.Rows.Add(new object[] { 10, "Octubre" });
            //dsmeses.Rows.Add(new object[] { 11, "Noviembre" });
            //dsmeses.Rows.Add(new object[] { 12, "Dicimebre" });
            //dsmeses.AcceptChanges();

            //Establecemos la Tabla con fuente de datos de nuestro comboBox
            //cboPeriodo.DataSource = dsmeses;
            //cboPeriodo.ValueMember = "Codigo";
            //cboPeriodo.DisplayMember = "Codigo";
            //cboPeriodo.DisplayMember = "Descripcion";

            //Seleccionamos en mes actual
            //cboPeriodo.SelectedValue = DateTime.Now.Month;
            //var mes = DateTime.Now.Month;
            //DateTime mes = new DateTime();
            //string.Format("MM",mes);
            //string.Format("{0:MM}", mes.Month);
            //cboPeriodo.Items.Add(mes);
            //cboPeriodo.Items.Add(DateTime.Now.Month);
            #endregion
        }

        private void cboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cboPeriodo.Text, "Informacion");
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
                codigosviajes.Add(View.GetRowCellValue(e.RowHandle, "CODIGO").ToString());
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                e.TotalValue = codigosviajes.Distinct().Count();
            }     
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            //txtCliente.Text = txtCliente.Text.ToUpper();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUnidadesActivas_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = true;
            splitContainer2.Panel2Collapsed = false;
            verReporteUnidadesActivas();
        }
        private void verReporteUnidadesActivas()
        {
            CrystalReportViewer rv = new CrystalReportViewer();

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Operaciones\Reportes\";
            string rpt = "crvRptUnidadesActivas.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);

            crvReporte.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }
        private void verReporteUnidadesDistribucion()
        {
            CrystalReportViewer rv = new CrystalReportViewer();

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Operaciones\Reportes\";
            string rpt = "crvRptUnidadesEnCliente.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@FECHA_INI", dtpFechaIni.Text.Trim());
            r.SetParameterValue("@FECHA_FIN", dtpFechaFin.Text.Trim());

            crvReporte.ReportSource = r;

            this.WindowState = FormWindowState.Maximized;

            //button3.Enabled = true;
            //txtCopias.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = true;
            splitContainer2.Panel2Collapsed = false;
            verReporteUnidadesDistribucion();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDistriViajes_Click(object sender, EventArgs e)
        {
            
        }

     
    }
}

