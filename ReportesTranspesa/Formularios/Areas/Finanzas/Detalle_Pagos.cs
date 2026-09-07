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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class Detalle_Pagos : MetroFramework.Forms.MetroForm
    {
        public Detalle_Pagos()
        {
            InitializeComponent();
        }
        int cliente;
        private void Detalle_Pagos_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);

            LlenarControlesFormulario();
            splitContainer1.SplitterDistance = 85;

        }


        public void LlenarControlesFormulario()
        {
            DataTable dtCtrl = new DataTable();

            dtCtrl = clsFinanzasBL.Instancia.ObtenerLlenadoControlReporteDetallePagos();

            if (dtCtrl == null)
            {
                MessageBox.Show("No se puedo cargar controles");
            }
            else
            {
                if (dtCtrl.Rows.Count> 0)
                {
                    cbxEmpresa.DataSource = dtCtrl;
                    cbxEmpresa.DisplayMember = "DescripcionCorta";
                    cbxEmpresa.ValueMember = "CompaniaCodigo";
                }
               
            }
                 
        }



        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);

                lvCliente.Columns[0].Width = 0;
                lvCliente.Columns[1].Width = 206;
                lvCliente.Columns[2].Width = 110;

                lvCliente.BringToFront();
                lvCliente.Visible = true;
                lvCliente.Focus();
                //splitContainer1.SplitterDistance = lvCliente.Top + lvCliente.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                //splitContainer1.SplitterDistance = 74;
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
                //splitContainer1.SplitterDistance = 74;

                if (rbPendientes.Checked == true)
                {
                    BuscarPendientesPago();
                }
                else
                {
                    BuscarPagados();
                }

            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                //splitContainer1.SplitterDistance = 74;
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
                splitContainer1.SplitterDistance = 85;

                if (rbPendientes.Checked == true)
                {
                    BuscarPendientesPago();
                }
                else
                {
                    BuscarPagados();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La fecha incial no debe ser mayor a la fecha fin.","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;

            }

            if (rbPendientes.Checked == true)
            {
                BuscarPendientesPago();
            }
            else
            {
                BuscarPagados();
            }
        }
        void BuscarPendientesPago()
        {
            if (txtCliente.Text.Trim() == "") { cliente = -1; }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetDetallePendientePagos(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
             dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente, cbxEmpresa.SelectedValue.ToString());

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["PROVEEDOR"], DevExpress.Data.ColumnSortOrder.Ascending),
                //new GridColumnSortInfo(gridView.Columns["T. PAGO"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 2);

                dtgvDataView.Columns["MONTO PAGO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO PAGO"].DisplayFormat.FormatString = "n2";

                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                item1.FieldName = "MONTO PAGO";
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item1.DisplayFormat = " | TOTAL:  {0:n2}";
                dtgvDataView.GroupSummary.Add(item1);

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

        void BuscarPagados()
        {

            if (txtCliente.Text.Trim() == "") { cliente = -1; }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetDetallePagos(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
             dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente, cbxEmpresa.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["PROVEEDOR"], DevExpress.Data.ColumnSortOrder.Ascending),
                //new GridColumnSortInfo(gridView.Columns["T. PAGO"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 2);

                dtgvDataView.Columns["MONTO PAGO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO PAGO"].DisplayFormat.FormatString = "n2";

                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                item1.FieldName = "MONTO PAGO";
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item1.DisplayFormat = " | TOTAL:  {0:n2}";
                dtgvDataView.GroupSummary.Add(item1);

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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Detalle de Pagos " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void rbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            metroLabel7.Text = "Fecha:  VENCIMIENTO";
        }

        private void rbPagados_CheckedChanged(object sender, EventArgs e)
        {
            metroLabel7.Text = "Fecha:  PAGO";
        }

        private void lvCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
