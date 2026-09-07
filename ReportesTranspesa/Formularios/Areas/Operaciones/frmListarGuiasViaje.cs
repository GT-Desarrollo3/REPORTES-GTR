using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using Comun;
using Negocio;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmListarGuiasViaje : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaGuiaViaje = new DataTable();

        public frmListarGuiasViaje()
        {
            InitializeComponent();
        }

        private void frmListarGuiasViaje_Load(object sender, EventArgs e)
        {
            cbxSerieGuia.Text = "TODAS";
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            ListarReporteGuias();
        }


        public void ListarReporteGuias()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaGuiaViaje = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarReporteGuias(dtpFechaIni.Text, dtpFechaFin.Text, cbxSerieGuia.Text,
                                                              txtNumeroGuia.Text, txtTracto.Text, txtCarreta.Text);
                dtgReporteGuias.DataSource = dtListaGuiaViaje;
                if (dtListaGuiaViaje.Rows.Count > 0)
                {
                    dgvReporteGuiasVista.Columns["FECHA_EMISIÓN_GUÍA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteGuiasVista.Columns["FECHA_EMISIÓN_GUÍA"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvReporteGuiasVista.Columns["FECHA_CREACIÓN_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteGuiasVista.Columns["FECHA_CREACIÓN_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                    dgvReporteGuiasVista.Columns["NÚMERO_GUÍA"].Summary.Clear();
                    dgvReporteGuiasVista.Columns["NÚMERO_GUÍA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "NÚMERO_GUÍA", "Total = {0}");

                    dgvReporteGuiasVista.BestFitColumns();
                }
            }
        }


        private void txtNumeroGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReporteGuias(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReporteGuias(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReporteGuias(); }
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReporteGuias(); }
        }

        private void txtCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReporteGuias(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarReporteGuias(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgReporteGuias.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Guías por Viaje - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgReporteGuias.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnEmitidos_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
