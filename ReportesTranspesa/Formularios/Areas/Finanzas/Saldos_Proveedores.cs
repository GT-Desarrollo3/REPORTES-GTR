using System;
using System.Data;
using System.Windows.Forms;
using Negocio;
using System.Globalization;
using System.Diagnostics;
using ReportesTranspesa.Sistema;
using System.Text;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class Saldos_Proveedores : MetroFramework.Forms.MetroForm
    {
        public Saldos_Proveedores()
        {
            InitializeComponent();
        }
        //FINANZAS
        decimal sumaSoles;
        decimal sumaDolares;
        decimal totalSoles = 0;
        decimal totalDolares = 0;
        //CONTA
        decimal sumaSoles2;
        decimal sumaDolares2;
        private void Saldos_Proveedores_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            lblContabilidad.Left = (this.Width / 2) - 20;
        }
        private DataTable MakeDtLongitud(Int32 Columns)
        {
            try
            {
                DataTable dt = new DataTable("WidthColumns");
                for (int i = 0; i < Columns; i++)
                {
                    dt.Columns.Add("C" + i.ToString(), typeof(Object));
                }
                return dt;
            }
            catch
            {
                return new DataTable();
            }
        }
        private void FormatLV(ListView MyList, DataTable WidthColumns)
        {
            try
            {
                if (WidthColumns.Rows.Count <= 0) return;

                for (int i = 0; i < MyList.Columns.Count; i++)
                {
                    MyList.Columns[i].Width = Int32.Parse(WidthColumns.Rows[0]["C" + i].ToString());
                }
            }
            catch
            {

            }
        }
        public void LlenarLw(ListView MyLista, DataTable Registro, Boolean LlenaCabezeras, Boolean CheckedItems, Boolean TamanioAut)
        {
            DataTable dtTamanioColumns;
            Int32 TamanioActual = 0;
            Int32 TamanioTitulo = 0;
            Int32 Tamanio = 0;

            int Columnas;
            ListViewItem ItemLista;

            MyLista.BeginUpdate();

            Columnas = Registro.Columns.Count;

            dtTamanioColumns = MakeDtLongitud(Columnas);

            if (LlenaCabezeras)
            {
                MyLista.Columns.Clear();
                foreach (DataColumn dtc in Registro.Columns)
                {
                    MyLista.Columns.Add(dtc.ColumnName.ToString(), 100, 0).Name = "ch" + dtc.ColumnName.ToString();
                }
            }

            MyLista.Items.Clear();

            if (Registro.Rows.Count <= 0) { MyLista.EndUpdate(); return; }

            DataRow dtrTC = dtTamanioColumns.NewRow();

            foreach (DataRow dr in Registro.Rows)
            {
                ItemLista = MyLista.Items.Add(dr[0].ToString());

                dtrTC["C" + 0] = 0;

                for (int I = 1; I < Columnas; I++)
                {
                    if (dr[I] != null)
                    {
                        if (dr[I].GetType() == typeof(DateTime))
                        {
                            ItemLista.SubItems.Add(DateTime.Parse(dr[I].ToString()).ToShortDateString());
                        }
                        else
                        {
                            ItemLista.SubItems.Add(dr[I].ToString());
                        }

                        //Guardamos el tamanio
                        Tamanio = dr[I].ToString().Length;
                        TamanioTitulo = Registro.Columns[I].ColumnName.Length;

                        if (Tamanio > TamanioTitulo)
                        {
                            if (Tamanio > TamanioActual)
                            {
                                dtrTC["C" + I] = Tamanio;
                                TamanioActual = Tamanio;
                            }
                        }
                        else
                        {
                            dtrTC["C" + I] = TamanioTitulo;
                            TamanioActual = TamanioTitulo;
                        }
                    }
                    else
                    {
                        ItemLista.SubItems.Add("");
                        TamanioTitulo = Registro.Columns[I].ColumnName.Length;
                        if (TamanioTitulo > TamanioActual)
                        {
                            dtrTC["C" + I] = TamanioTitulo;
                            TamanioActual = TamanioTitulo;
                        }
                    }
                }
            }
            dtTamanioColumns.Rows.Add(dtrTC);

            if (TamanioAut) FormatLV(MyLista, dtTamanioColumns);

            //if (Tipo == true)
            //{
            //    for (int I = 0; I < MyLista.Items.Count; I++)
            //    {
            //        MyLista.Items[I].Checked = true;
            //    }
            //    Tipo = false;
            //}
            MyLista.EndUpdate();
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                LlenarLw(lvProveedor, clsPersonaBL.Instancia.GetProveedor(txtProveedor.Text.Trim()), true, false, false);

                lvProveedor.Columns[0].Width = 0;
                lvProveedor.Columns[1].Width = 250;
                lvProveedor.Columns[2].Width = 120;

                lvProveedor.BringToFront();
                lvProveedor.Visible = true;
                lvProveedor.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedorId.Text = "";
                txtProveedor.Focus();
            }
        }

        private void lvProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];
                txtProveedor.Text = ItemActual.SubItems[1].Text.Trim();
                txtProveedorId.Text = ItemActual.Text;
                lvProveedor.Visible = false;
                btnBuscar.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedor.Focus();
            }
        }

        private void lvProveedor_Enter(object sender, EventArgs e)
        {
            if (!lvProveedor.Items.Count.Equals(0))
            {
                lvProveedor.Items[0].Selected = true;
            }
        }

        private void lvProveedor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];
                txtProveedor.Text = ItemActual.SubItems[1].Text.Trim();
                txtProveedorId.Text = ItemActual.Text;
                lvProveedor.Visible = false;
                btnBuscar.Focus();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            dtgvData2.DataSource = null;
            dtgvDataView2.Columns.Clear();
            dtgvDataView2.GroupSummary.Clear();
            lblTotal.Text = "";
            totalSoles = 0;
            totalDolares = 0;
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetSaldosProveedoresFinan(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59",txtProveedorId.Text.Trim());
            dt2 = clsFinanzasBL.Instancia.GetSaldosProveedoresCont(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
               dtpFechaFin.Value.ToShortDateString() + " 23:59:59", txtProveedorId.Text.Trim());
            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                //FINANZAS////////////////////
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["Monto"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["Monto"].DisplayFormat.FormatString = "n2";

                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["Proveedor"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);

                GridGroupSummaryItem item = new GridGroupSummaryItem();
                item.FieldName = "Monto";
                item.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                item.DisplayFormat = "{0}";
                gridView.GroupSummary.Add(item);

                dtgvDataView.BestFitColumns();
                lblTotal.Text = "(S/. "+totalSoles+" || $ "+totalDolares+")";
                //dtgvDataView.Columns.RemoveAt(7);
                //////CONTA////////////
                //////////////////////////////////////////////////////////////////////////////////
                dtgvData2.DataSource = dt2;
                dtgvDataView2.Columns["Monto Local"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView2.Columns["Monto Local"].DisplayFormat.FormatString = "c2";
                dtgvDataView2.Columns["Monto Dolar"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView2.Columns["Monto Dolar"].DisplayFormat.FormatString = "n2";

                GridView gridView2 = dtgvData2.FocusedView as GridView;
                gridView2.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView2.Columns["Proveedor"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);

                GridGroupSummaryItem item2 = new GridGroupSummaryItem();
                item2.FieldName = "Monto Local";
                item2.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                item2.DisplayFormat = "{0}";
                item2.Tag = 1;
                

                GridGroupSummaryItem item3 = new GridGroupSummaryItem();
                item3.FieldName = "Monto Dolar";
                item3.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                item3.DisplayFormat = " | {0}";
                item3.Tag = 2;

                gridView2.GroupSummary.Add(item2);
                gridView2.GroupSummary.Add(item3);

                dtgvDataView2.BestFitColumns();
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
            GridView View = sender as GridView;
            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                sumaSoles = 0;
                sumaDolares = 0;
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                if (View.GetRowCellValue(e.RowHandle, "Moneda").ToString() == "LO")
                {
                    sumaSoles += Convert.ToDecimal(e.FieldValue);
                }
                else 
                {
                    sumaDolares += Convert.ToDecimal(e.FieldValue);
                }
                
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                e.TotalValue = "S/. " + sumaSoles + " | $ " + sumaDolares;
                totalSoles = totalSoles + sumaSoles;
                totalDolares = totalDolares + sumaDolares;
            } 
        }
        private void dtgvDataView2_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                sumaSoles2 = 0;
                sumaDolares2 = 0;
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        if (View.GetRowCellValue(e.RowHandle, "Moneda").ToString() == "LO")
                        {
                            sumaSoles2 += Convert.ToDecimal(e.FieldValue);
                        }
                        break;
                    case 2:
                        if (View.GetRowCellValue(e.RowHandle, "Moneda").ToString() == "EX")
                        {
                            sumaDolares2 += Convert.ToDecimal(e.FieldValue);
                        }
                        break;
                }
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                //e.TotalValue = "S/. " + sumaSoles2 + " | $ " + sumaDolares2;
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = "S/. " + sumaSoles2;
                        break;
                    case 2:
                        e.TotalValue = "$ " + sumaDolares2;
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
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Saldos a Proveedores(Finanzas)" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                string nombre2 = System.IO.Path.Combine(desktop, "Reporte Saldos a Proveedores(Contabilidad)" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
                dtgvData2.ExportToXlsx(nombre2);
                Process.Start(nombre2);
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
                dtgvData2.ShowPrintPreview();
            }
        }
    }
}
