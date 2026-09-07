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
using Comun;


namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class Despachos : MetroFramework.Forms.MetroForm
    {
        public Despachos()
        {
            InitializeComponent();
        }

        private void Despachos_Load(object sender, EventArgs e)
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

        private void CalcularTotales(bool reset)
        {
            if (reset == true)
            {
                lblDespachos.Text = "0";
            }
            else
            {
                double total = 0;
                for (int i = 0; i < dtgvDataView.RowCount; i++) 
                {
                    total += Convert.ToDouble(dtgvDataView.GetRowCellValue(i, "TN"));
                }
                lblDespachos.Text = total.ToString("N");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            DataTable dtDespacho = new DataTable();

            string fechainicio = "01/01/1980";
            string fechafin = "31/12/2030";
            string cliente = "";
            string producto = "";
            string lote = "";
            string modo = "G";

            if (chkFechas.Checked)
            {
                fechainicio = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
                fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            }
            if (chkCliente.Checked) { cliente = txtCliente.Text; }
            if (chkProducto.Checked) { producto = txtProducto.Text; }
            if (chkLote.Checked) { lote = txtLote.Text; }
            if (rbSacos.Checked) { modo = "S"; }
            if (rbBigbags.Checked) { modo = "B"; }
            if (rbTodos.Checked) { modo = "T"; }

            dtDespacho = clsAlmacenBL.Instancia.GetDespachos(fechainicio, fechafin, cliente, producto, lote, modo);
            if (dtDespacho.Rows.Count > 0)
            {
                dtgvData.DataSource = dtDespacho;
                dtgvDataView.Columns["TN"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["TN"].DisplayFormat.FormatString = "n2";
                CalcularTotales(false);
                dtgvDataView.BestFitColumns();
            }
            else
            {
                CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
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
                string nombre = System.IO.Path.Combine(desktop, "Despachos almacén " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void rbSacos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSacos.Checked) 
            {
                this.Text  = "Despachos en Sacos";
                this.Refresh();
            }
        }

        private void rbGranel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGranel.Checked)
            {
                this.Text = "Despachos a Granel";
                this.Refresh();
            }
        }

        private void rbBigbags_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBigbags.Checked)
            {
                this.Text = "Despachos en Big Bags";
                this.Refresh();
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                this.Text = "Todos los Despachos";
                this.Refresh();
            }
        }

    }
}
