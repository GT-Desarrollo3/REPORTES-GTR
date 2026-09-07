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

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class Compras : MetroFramework.Forms.MetroForm
    {
        public Compras()
        {
            InitializeComponent();
        }

        int proveedor = -1;

        private void Compras_Load(object sender, EventArgs e)
        {
            cboCompania.SelectedIndex = 0;
            string mes;
            if (DateTime.Now.Month < 10)
            {
                mes = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                mes = DateTime.Now.Month.ToString();
            }
            txtPerIni.Text = DateTime.Now.Year.ToString() + mes;
            txtPerFin.Text = DateTime.Now.Year.ToString() + mes;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtProveedor.Text.Trim() == "") { proveedor = -1; }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string compania = "";
            switch (cboCompania.SelectedIndex)
            {
                case 0:
                    compania = "";
                    break;
                case 1:
                    compania = "100000";
                    break;
                case 2:
                    compania = "400000";
                    break;
            }
            dt = clsLogisticaBL.Instancia.GetCompras(compania, txtPerIni.Text.Trim(), txtPerFin.Text.Trim(), proveedor);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                //GridView gridView = dtgvData.FocusedView as GridView;
                //gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                //new GridColumnSortInfo(gridView.Columns["CUENTA CONTABLE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                //}, 1);
                dtgvDataView.Columns["PRECIO UNITARIO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["PRECIO UNITARIO"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["TOTAL"].DisplayFormat.FormatString = "n2";

                dtgvDataView.Columns["FECHA REQUERIMIENTO"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["FECHA REQUERIMIENTO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dtgvDataView.Columns["FECHA PREPARACION"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["FECHA PREPARACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dtgvDataView.Columns["FECHA APROBACION"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["FECHA APROBACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dtgvDataView.Columns["ULTIMA MODIFICACION"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["ULTIMA MODIFICACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Ordenes de Compra " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvProveedor, clsConsultaBL.Instancia.GetPersona(txtProveedor.Text), true, false, false);

                lvProveedor.Columns[0].Width = 0;
                lvProveedor.Columns[1].Width = 206;
                lvProveedor.Columns[2].Width = 110;

                lvProveedor.BringToFront();
                lvProveedor.Visible = true;
                lvProveedor.Focus();
                splitContainer1.SplitterDistance = lvProveedor.Top + lvProveedor.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }
        }

        private void lvProveedor_Enter(object sender, EventArgs e)
        {
            if (!lvProveedor.Items.Count.Equals(0))
            {
                lvProveedor.Items[0].Selected = true;
            }
        }

        private void lvProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];

                proveedor = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[1].Text;
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }
        }

        private void lvProveedor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];

                proveedor = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[1].Text;
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }
        }
    }
}
