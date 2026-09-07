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
    public partial class Ingresos : MetroFramework.Forms.MetroForm
    {
        public Ingresos()
        {
            InitializeComponent();
        }

        private void Ingresos_Load(object sender, EventArgs e)
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
                lblSumSalida.Text = "0";
                lblSumIngreso.Text = "0";
                lblSumDiferencia.Text = "0";
                lblViajes.Text = "0";
            }
            else
            {
                double totalsal = 0;
                double totaling = 0;
                double totaldif = 0;
                for (int i = 0; i < dtgvDataView.RowCount; i++) 
                {
                    totalsal += Convert.ToDouble(dtgvDataView.GetRowCellValue(i, "PESO PUERTO"));
                    totaling += Convert.ToDouble(dtgvDataView.GetRowCellValue(i, "CANTIDAD INGRESO"));
                    totaldif += Convert.ToDouble(dtgvDataView.GetRowCellValue(i, "DIFERENCIA"));
                }
                lblSumSalida.Text = totalsal.ToString("N");
                lblSumIngreso.Text = totaling.ToString("N");
                lblSumDiferencia.Text = totaldif.ToString("N");
                lblViajes.Text = dtgvDataView.RowCount.ToString();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            DataTable dt = new DataTable();
          
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

            dt = clsAlmacenBL.Instancia.GetIngresos(fechainicio, fechafin, cliente, producto, lote);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["FECHA INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["FECHA INICIO"].DisplayFormat.FormatString = "g";
                dtgvDataView.Columns["FECHA FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["FECHA FIN"].DisplayFormat.FormatString = "g";
                dtgvDataView.Columns["PESO PUERTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["PESO PUERTO"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["CANTIDAD INGRESO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["CANTIDAD INGRESO"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["DIFERENCIA"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["DIFERENCIA"].DisplayFormat.FormatString = "n2";
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
                string nombre = System.IO.Path.Combine(desktop, "Ingresos almacén " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
      
    }
}
