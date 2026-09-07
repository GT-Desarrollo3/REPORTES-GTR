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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class Saldos_Almacen : MetroFramework.Forms.MetroForm
    {
        public Saldos_Almacen()
        {
            InitializeComponent();
        }

        private void Saldos_Almacen_Load(object sender, EventArgs e)
        {

        }

        private void chkCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCliente.Checked)
            {
                txtCliente.Enabled = true;
            }
            else
            {
                txtCliente.Text = "";
                txtCliente.Enabled = false;
            }
        }

        private void chkProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProducto.Checked)
            {
                txtProducto.Enabled = true;
            }
            else
            {
                txtProducto.Text = "";
                txtProducto.Enabled = false;
            }
        }

        private void chkLote_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLote.Checked)
            {
                txtLote.Enabled = true;
            }
            else
            {
                txtLote.Text = "";
                txtLote.Enabled = false;
            }
        }

        private void chkFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechas.Checked)
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
            }
            else
            {
                dtpFechaIni.Value = DateTime.Now;
                dtpFechaFin.Value = DateTime.Now;
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            DataTable dt = new DataTable();
            dtgvData2.DataSource = null;
            dtgvDataView2.Columns.Clear();
            DataTable dt2 = new DataTable();

            string fechainicio = "01/01/1980";
            string fechafin = "31/12/2030";
            string cliente = "";
            string producto = "";
            string lote = "";

            if (chkFechas.Checked)
            {
                fechainicio = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
                fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            }
            if (chkCliente.Checked) { cliente = txtCliente.Text; }
            if (chkProducto.Checked) { producto = txtProducto.Text; }
            if (chkLote.Checked) { lote = txtLote.Text; }

            dt = clsAlmacenBL.Instancia.GetSaldos(fechainicio, fechafin, cliente, producto, lote,1);
            dt2 = clsAlmacenBL.Instancia.GetSaldos(fechainicio, fechafin, cliente, producto, lote,2);
            if (dt.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvData2.DataSource = dt2;
                dtgvDataView.Columns["SACOS"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["SACOS"].DisplayFormat.FormatString = "n0";
                dtgvDataView.Columns["TN"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["TN"].DisplayFormat.FormatString = "n2";
                dtgvDataView2.Columns["TN"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView2.Columns["TN"].DisplayFormat.FormatString = "n2";
                dtgvDataView.BestFitColumns();
                dtgvDataView2.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void dtgvDataView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns[" "]);
                if (category == "SALDO PRODUCTO ENSACADO")
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.BackColor2 = Color.DarkRed;
                }
            }
        }

        private void dtgvDataView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns[" "]);
                if (category == "SALDO GRANEL")
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.BackColor2 = Color.DarkRed;
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            GridControl[] grids = new GridControl[] { dtgvData, dtgvData2 };
            PrintingSystem ps = new PrintingSystem();
            CompositeLink compositeLink = new CompositeLink();
            compositeLink.PrintingSystem = ps;
            foreach (GridControl grid in grids)
            {
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = grid;
                compositeLink.Links.Add(link);
                Link l = new Link();
                compositeLink.Links.Add(l);
            }
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            dtfi.TimeSeparator = ".";
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string nombre = System.IO.Path.Combine(desktop, "Saldos almacén " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
            //compositeLink.CreatePageForEachLink();
            compositeLink.ExportToXlsx(nombre);
            Process.Start(nombre);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GridControl[] grids = new GridControl[] { dtgvData, dtgvData2 };
            PrintingSystem ps = new PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
            compositeLink.PrintingSystem = ps;
            foreach (GridControl grid in grids)
            {
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = grid;
                compositeLink.Links.Add(link);
            }
            compositeLink.CreateDocument();
            compositeLink.ShowPreview();
        }
    }
}
