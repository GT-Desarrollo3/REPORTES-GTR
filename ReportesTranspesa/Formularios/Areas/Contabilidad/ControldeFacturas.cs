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
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class ControldeFacturas : MetroFramework.Forms.MetroForm
    {
        public ControldeFacturas()
        {
            InitializeComponent();
        }

        string fechaini;
        string fechafin;
        decimal totalSoles;
        decimal canceladoSoles;
        decimal pendienteSoles;
        decimal totalDolares;
        decimal canceladoDolares;
        decimal pendienteDolares;
        DateTime FechaRecepcion;
        string Documento;
        int cliente;

        private void ControldeFacturas_Load(object sender, EventArgs e)
        {
            cboCompania.SelectedIndex = 0;
            cboFactRecep.SelectedIndex = 0;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaIni.Enabled = false;
            dtpFechaFin.Enabled = false;
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);

                lvCliente.Columns[0].Width = 0;
                lvCliente.Columns[1].Width = 206;
                lvCliente.Columns[2].Width = 110;

                lvCliente.BringToFront();
                lvCliente.Visible = true;
                lvCliente.Focus();
                splitContainer1.SplitterDistance = lvCliente.Top + lvCliente.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 74;
            }
        }

        private void lvCliente_Enter(object sender, EventArgs e)
        {
            if (!lvCliente.Items.Count.Equals(0))
            {
                lvCliente.Items[0].Selected = true;
            }
        }

        private void lvCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 74;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 74;
            }
        }

        private void lvCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 74;
            }
        }

        public void MostrarData()
        {
            if (txtCliente.Text.Trim() == "") { cliente = -1; }
            string compania = "";
            string facturas = "";
            if (cboCompania.SelectedIndex == 1)
            {
                compania = "10000000";
            }
            if (cboCompania.SelectedIndex == 2)
            {
                compania = "40000000";
            }
            if (cboCompania.SelectedIndex == 3)
            {
                compania = "50000000";
            }
            if (cboCompania.SelectedIndex == 4)
            {
                compania = "60000000";
            }
            if (cboCompania.SelectedIndex == 5)
            {
                compania = "70000000";
            }
            facturas = cboFactRecep.Text;
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();

            if (chkFecha.Checked == true)
            {
                fechaini = dtpFechaIni.Value.ToShortDateString();
                fechafin = dtpFechaFin.Value.ToShortDateString();
            }
            else
            {
                fechaini = "01/01/1950";
                fechafin = "30/12/2050";
            }
            dt = clsContabilidadBL.Instancia.GetControlFacturas(compania, fechaini + " 00:00:00",
             fechafin + " 23:59:59", cliente,facturas);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["TIPO"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["TIPO"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["DOCUMENTO"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["DOCUMENTO"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["FECHA"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["FECHA"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["DIAS VENCIDOS"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["DIAS VENCIDOS"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["CLIENTE"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["CLIENTE"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["MONEDA"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["MONEDA"].OptionsColumn.ReadOnly = true;
                
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);

                dtgvDataView.ExpandAllGroups();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCliente.Text.Trim() == "") { cliente = -1; }
            string compania = "";
            string facturas = "";
            if (cboCompania.SelectedIndex == 1)
            {
                compania = "10000000";
            }
            if (cboCompania.SelectedIndex == 2)
            {
                compania = "40000000";
            }
            if (cboCompania.SelectedIndex == 3)
            {
                compania = "50000000";
            }
            if (cboCompania.SelectedIndex == 4)
            {
                compania = "60000000";
            }
            if (cboCompania.SelectedIndex == 5)
            {
                compania = "70000000";
            }
            facturas = cboFactRecep.Text;
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();

            if (chkFecha.Checked == true)
            {
                fechaini = dtpFechaIni.Value.ToShortDateString();
                fechafin = dtpFechaFin.Value.ToShortDateString();
            }
            else
            {
                fechaini = "01/01/1950";
                fechafin = "30/12/2050";
            }
            dt = clsContabilidadBL.Instancia.GetControlFacturas(compania, fechaini + " 00:00:00",
             fechafin + " 23:59:59", cliente,facturas);
            if (dt.Rows.Count > 0)
            {
                Editar();
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["TIPO"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["TIPO"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["DOCUMENTO"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["DOCUMENTO"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["FECHA"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["FECHA"].OptionsColumn.ReadOnly = true;
                dtgvDataView.Columns["CLIENTE"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["CLIENTE"].OptionsColumn.ReadOnly = true;
                
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                
                dtgvDataView.UpdateSummary();

                dtgvDataView.ExpandAllGroups();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void dtgvDataView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                totalSoles = 0;
                canceladoSoles = 0;
                pendienteSoles = 0;
                totalDolares = 0;
                canceladoDolares = 0;
                pendienteDolares = 0;
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        if (View.GetRowCellValue(e.RowHandle, "MONEDA").ToString() == "Local")
                        {
                            totalSoles += Convert.ToDecimal(e.FieldValue);
                        }
                        break;
                    case 2:
                        if (View.GetRowCellValue(e.RowHandle, "MONEDA").ToString() == "Local")
                        {
                            canceladoSoles += Convert.ToDecimal(e.FieldValue);
                        }
                        break;
                    case 3:
                        if (View.GetRowCellValue(e.RowHandle, "MONEDA").ToString() == "Local")
                        {
                            pendienteSoles += Convert.ToDecimal(e.FieldValue);
                        }
                        break;
                    case 4:
                        if (View.GetRowCellValue(e.RowHandle, "MONEDA").ToString() == "Extranjera")
                        {
                            totalDolares += Convert.ToDecimal(e.FieldValue);
                        };
                        break;
                    case 5:
                        if (View.GetRowCellValue(e.RowHandle, "MONEDA").ToString() == "Extranjera")
                        {
                            canceladoDolares += Convert.ToDecimal(e.FieldValue);
                        };
                        break;
                    case 6:
                        if (View.GetRowCellValue(e.RowHandle, "MONEDA").ToString() == "Extranjera")
                        {
                            pendienteDolares += Convert.ToDecimal(e.FieldValue);
                        };
                        break;
                }
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = totalSoles;
                        break;
                    case 2:
                        e.TotalValue = canceladoSoles;
                        break;
                    case 3:
                        e.TotalValue = pendienteSoles;
                        break;
                    case 4:
                        e.TotalValue = totalDolares;
                        break;
                    case 5:
                        e.TotalValue = canceladoDolares;
                        break;
                    case 6:
                        e.TotalValue = pendienteDolares;
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Ventas Detalladas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFecha.Checked == true)
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
                fechaini = dtpFechaIni.Value.ToShortDateString();
                fechafin = dtpFechaFin.Value.ToShortDateString();
            }
            else
            {
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
                fechaini = "01/01/1950"+" 00:00:00";
                fechafin = "30/12/2050"+" 23:59:59";
            }
        }

        private void dtgvDataView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "FECHA RECEP." && cboFactRecep.SelectedIndex == 1)
            {
                e.Cancel = false;
            }

            else
            {
                e.Cancel = true;
            }
        }

        public void Editar()
        {
            if (cboFactRecep.SelectedIndex == 0)
            {
                this.dtgvDataView.OptionsSelection.MultiSelect = false;
            }
            else
            {
                this.dtgvDataView.OptionsSelection.MultiSelect = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardaFechaRecepcion();
            MostrarData();
        }

        public void GuardaFechaRecepcion()
        {
            int renglonesSeleccionados = dtgvDataView.SelectedRowsCount;
            if (renglonesSeleccionados == 0)
            {
                MessageBox.Show("Tienes que seleccionar por lo menos una fila.");
            }
            else
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("FECHA RECEP.", typeof(DateTime));
                foreach (int indice in dtgvDataView.GetSelectedRows())
                {
                    try
                    {
                        DataRow fila = tabla.NewRow();
                        fila["FECHA RECEP."] = dtgvDataView.GetRowCellValue(indice, "FECHA RECEP.").ToString();
                        var documento = dtgvDataView.GetRowCellValue(indice, "DOCUMENTO").ToString();
                        Documento = documento.ToString();
                        tabla.Rows.Add(fila);

                        if (fila["FECHA RECEP."].ToString() != null)
                        {
                            FechaRecepcion = Convert.ToDateTime(fila["FECHA RECEP."].ToString());
                            clsFinanzasBL.Instancia.GetUpdateFechaRecepcion(FechaRecepcion, Documento);
                            Mensaje m = new Mensaje();                            
                            m.mensaje = "Se Actualizo Fecha Recepción" + Environment.NewLine + " del Documento: " + Documento;
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
                        m.mensaje = "Seleccionar bien el Documento";
                        m.ShowDialog();
                    }
                }
            }
        }

        private void cboFactRecep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFactRecep.SelectedIndex == 0)
            {
                btnGuardar.Visible = false;
                dtgvData.DataSource = null;
            }
            else
            {
                btnGuardar.Visible = true;
                dtgvData.DataSource = null;
            }
        }
    }
}
