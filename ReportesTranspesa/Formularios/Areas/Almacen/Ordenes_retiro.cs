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
    public partial class Ordenes_retiro : MetroFramework.Forms.MetroForm
    {
        public Ordenes_retiro()
        {
            InitializeComponent();
        }

        private void Ordenes_retiro_Load(object sender, EventArgs e)
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
                fechainicio = dtpFechaIni.Value.ToShortDateString()+" 00:00:00";
                fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            }
            if (chkCliente.Checked) { cliente = txtCliente.Text; }
            if (chkProducto.Checked) { producto = txtProducto.Text; }
            if (chkLote.Checked) { lote = txtLote.Text; }

            dt = clsAlmacenBL.Instancia.GetOrdenesRetiro(fechainicio,fechafin,cliente,producto,lote);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
            }
            else
            {
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
                string nombre = System.IO.Path.Combine(desktop, "Órdenes de retiro " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
