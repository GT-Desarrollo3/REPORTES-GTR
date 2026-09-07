using System;
using System.Collections.Generic;
using System.Linq;
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
using DevExpress.XtraGrid.Views.Base;
using System.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class Sumarizado : MetroFramework.Forms.MetroForm
    {
        public Sumarizado()
        {
            InitializeComponent();
        }

        decimal sumaFacturados;
        decimal sumaNoFacturados;
        int cuentaGuias;
        List<string> codigosviajes;
        string Viaje;
        DateTime FechaGuia;
        string Area;

        private void Sumarizado_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            if (Utilitario.Instancia.SesionUsuario.usuario == "GREYES")
            {
                dtgvDataView.OptionsBehavior.Editable = true;
                btnGuardar.Visible = true;
            }
            else
            {
                dtgvDataView.OptionsBehavior.Editable = false;
                btnGuardar.Visible = false;
            }

            string UsuarioAcceso = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(UsuarioAcceso);
            if (dtAreaUsuario.Rows.Count > 0) { Area = dtAreaUsuario.Rows[0]["AREA"].ToString(); }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string tipofecha="";
            if (rbFechaProg.Checked)
            {
                tipofecha = "PROGRAMADA";
            }
            else if (rbFechaCreacion.Checked)
            {
                tipofecha = "CREACION";
            }
            else if (rbAnulados.Checked)
            {
                tipofecha = "ANULADOS";
            }
            else if (rbTolvas.Checked)
            {
                tipofecha = "TOLVAS";
            }

            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetDataSumarizado(dtpFechaIni.Value.ToShortDateString()+" 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", tipofecha);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                if (tipofecha == "TOLVAS")
                {
                    goto Fin;
                }
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
                    //    string transpesa = "";
                    //    string bra = "";
                    //    string altra = "";
                    //    switch (cboCompañia.SelectedIndex)
                    //    {
                    //        case 1: transpesa = "10000000";
                    //            //cboCompañia.Text = "10000000";
                    //            //transpesa = cboCompañia.Text;
                    //            break;

                    //        case 2: bra = "40000000";
                    //            //cboCompañia.Text = "40000000";
                    //            //bra = cboCompañia.Text;
                    //            break;

                    //        case 3: altra = "50000000";
                    //            //cboCompañia.Text = "50000000";
                    //            //altra = cboCompañia.Text;
                    //            break;
                    //    }
                    //    if (contafiltros > 0)
                    //    {
                    //        filtro = filtro + " AND ";
                    //    }
                    //    filtro = filtro + "[COMPAÑIA] IN ('" + transpesa + "','" + bra + "','" + altra + "')";
                    //    //filtro = filtro + "[COMPAÑIA] = '" + cboCompañia.Text + "'";
                    //    contafiltros = contafiltros + 1;
                    }
                }
                //**********************FIN DEL FILTRO*************************************************
                if (filtro != "")
                {
                    dtgvDataView.Columns["SUCURSAL"].FilterInfo = new ColumnFilterInfo(filtro);
                }

                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "c2";

                //******************CALCULO DE MONTOS TOTALES******************************************
                /*
                //Lo facturado
                dtgvDataView.Columns["FACTURADO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO", "Facturado={0:c2}");
                dtgvDataView.Columns["FACTURADO"].SummaryItem.Tag = 1;
                //Lo no facturado
                dtgvDataView.Columns["DOCUMENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO", "No Facturado={0:c2}");
                dtgvDataView.Columns["DOCUMENTO"].SummaryItem.Tag = 2;
                */

                dtgvDataView.Columns["GUÍA TRANSP."].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "GUÍA TRANSP.", "Guías={0}");
                dtgvDataView.Columns["GUÍA TRANSP."].SummaryItem.Tag = 3;
                //Cantidad de viajes
                dtgvDataView.Columns["CODIGO VIAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CODIGO VIAJE", "Viajes={0}");
                dtgvDataView.Columns["CODIGO VIAJE"].SummaryItem.Tag = 4;
                dtgvDataView.UpdateSummary();
                //Total
                
                //**************************************************************************************

                if (Area == "PRESUPUESTOS" || Area == "CONTABILIDAD")
                {
                    dtgvDataView.Columns["MONTO"].Visible = true;
                    dtgvDataView.Columns["MontoLocal"].Visible = true;

                    dtgvDataView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total={0:c2}");
                }
                else
                {
                    dtgvDataView.Columns["MONTO"].Visible = false;
                    dtgvDataView.Columns["MontoLocal"].Visible = false;
                }

                Fin: ;

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
        private void dtgvDataView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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
                }
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
                chkCompañia.Visible = false;
                cboCompañia.Visible = false;
            } 
            else 
            {
                cboTransporte.Enabled = false;
                cboTransporte.SelectedIndex = -1;
                txtTransporte.Text = "";
                txtTransporte.Enabled = false;
                txtTransporte.Visible = false;
                chkCompañia.Visible = true;
                cboCompañia.Visible = true;
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte Sumarizado del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void chkCompañia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompañia.Checked)
            {
                cboCompañia.Enabled = true;
                cboCompañia.SelectedIndex = 0;
                //txtTransporte.Enabled = true;
                //txtTransporte.Visible = true;
            }
            else
            {
                cboCompañia.Enabled = false;
                cboCompañia.SelectedIndex = -1;
                //txtTransporte.Text = "";
                //txtTransporte.Enabled = false;
                //txtTransporte.Visible = false;
            }
        }

        private void dtgvDataView_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            //ColumnView view = sender as ColumnView;
            //string proveedor = view.GetRowCellValue(e.ListSourceRow, "PROVEEDOR").ToString();
            //// Check whether the current row contains "USA" in the "Country" field. 
            //if (proveedor == "GRUPO DISOR S.A.C.")
            //{
            //    // Make the current row visible. 
            //    e.Visible = true;
            //    // Prevent default processing, so the row will be visible  
            //    // regardless of the view's filter. 
            //    e.Handled = true;
            //}
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int renglonesSeleccionados = dtgvDataView.SelectedRowsCount;
            if (renglonesSeleccionados == 0)
            {
                MessageBox.Show("Tienes que seleccionar por lo menos una fila.");
            }
            else
            {
                System.Data.DataTable tabla = new System.Data.DataTable();
                tabla.Columns.Add("FECHA ENVIO GUIAS", typeof(DateTime));
                //tabla.Columns.Add("OBSERVACIONES", typeof(string));  
                foreach (int indice in dtgvDataView.GetSelectedRows())
                {
                    try
                    {
                        DataRow fila = tabla.NewRow();
                        fila["FECHA ENVIO GUIAS"] = dtgvDataView.GetRowCellValue(indice, "FECHA ENVIO GUIAS").ToString();
                        //fila["OBSERVACIONES"] = dtgvDataView.GetRowCellValue(indice, "OBSERVACIONES").ToString();
                        //DataRow row = dtgvDataView.GetDataRow(dtgvDataView.GetSelectedRows()[0]);
                        //var documento = row["DOCUMENTO"].ToString();
                        var viaje = dtgvDataView.GetRowCellValue(indice, "CODIGO VIAJE").ToString();
                        var detalle = dtgvDataView.GetRowCellValue(indice, "OBSERVACIONES").ToString();
                        Viaje = viaje.ToString();
                        tabla.Rows.Add(fila);

                        if (fila["FECHA ENVIO GUIAS"].ToString() != null)
                        {
                            FechaGuia = Convert.ToDateTime(fila["FECHA ENVIO GUIAS"].ToString());
                            clsContabilidadBL.Instancia.UpdateFechaGuia_Sumarizado(FechaGuia, Viaje, detalle);
                            Mensaje m = new Mensaje();
                            m.mensaje = "Se Actualizo la Data";
                            m.ShowDialog();
                        }
                        else
                        {
                            Mensaje m = new Mensaje();
                            m.mensaje = "No Se Actualizo Fecha Recepción";
                            m.ShowDialog();
                        }
                    }
                    catch
                    {
                        Mensaje m = new Mensaje();
                        m.mensaje = "Seleccionar bien el Viaje";
                        m.ShowDialog();
                    }
                }
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
