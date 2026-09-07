using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class frmAlmacenSalaverryMovimientos : Form
    {
        public frmAlmacenSalaverryMovimientos()
        {
            InitializeComponent();
        }

        private void frmAlmacenSalaverryMovimientos_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);

            btnBuscar_Click(sender, e);
        }


        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void chkAnulados_CheckedChanged(object sender, EventArgs e) { btnBuscar_Click(sender, e); }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int bd = 0;   // 1: larrea / 2 :salaverry

            try
            {
                if (dtpFechaIni.Value > dtpFechaFin.Value)
                {
                    MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtpFechaIni.Focus();
                    return;
                }
                else
                {
                    if (rbtLarrea.Checked) { bd = 1; }
                    if (rbtSalaverry.Checked) { bd = 2; }

                    DataTable dt = clsAlmacenBL.Instancia.ReportesApp_Almacen_Movimientos_Almacen(dtpFechaIni.Text, dtpFechaFin.Text, txtCliente.Text, chkAnulados.Checked,bd);
                    
                    if (dt.Rows.Count > 0)
                    {
                        dtgvData.DataSource = dt;

                        //dgvViajesGuia.Columns["CLASEPRODUCTO"].Visible = false;
                        dgvViajesGuia.Columns["FECHAINICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvViajesGuia.Columns["FECHAINICIO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                        dgvViajesGuia.Columns["FECHAFIN"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvViajesGuia.Columns["FECHAFIN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                        dgvViajesGuia.Columns["TIPOMOVIMIENTO"].Summary.Clear();
                        dgvViajesGuia.Columns["TIPOMOVIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                        dgvViajesGuia.BestFitColumns();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE MOVIMIENTOS SALAVERRY - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
