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
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class Buscar_Facturas : MetroFramework.Forms.MetroForm
    {
        public Buscar_Facturas()
        {
            InitializeComponent();
        }
        int cliente;
        private void Buscar_Facturas_Load(object sender, EventArgs e)
        {
            cboCompania.SelectedIndex = 0;
            cbxFiltro.SelectedIndex = 0;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCliente.Text.Trim() == "") { cliente = -1; }
            string compania = "";

            if (cboCompania.SelectedIndex == 1) { compania = "10000000"; }
            if (cboCompania.SelectedIndex == 2) { compania = "40000000"; }
            if (cboCompania.SelectedIndex == 3) { compania = "50000000"; }
            if (cboCompania.SelectedIndex == 4) { compania = "60000000"; }
            if (cboCompania.SelectedIndex == 5) { compania = "70000000"; }
            if (cboCompania.SelectedIndex == 6) { compania = "90000000"; }

            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetFacturas(compania, dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
             dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente, txtBusqueda.Text, cbxFiltro.SelectedIndex);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;

                /*
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["CLIENTE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                */

                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "n2";

                dtgvDataView.Columns["FECHA_PREPARACION"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["FECHA_PREPARACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Facturas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void cboCompania_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
