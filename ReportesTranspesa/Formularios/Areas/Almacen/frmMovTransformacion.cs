using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class frmMovTransformacion : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaTransformaciones = new DataTable();

        public frmMovTransformacion()
        {
            InitializeComponent();
        }

        private void frmMovTransformacion_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            ListarTransformacion();
        }


        public void ListarTransformacion()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaTransformaciones = clsAlmacenBL.Instancia.ReportesApp_Almacen_Salaverry_ListarTransformaciones(dtpFechaIni.Text, dtpFechaFin.Text, txtCliente.Text, txtProducto.Text, txtLote.Text);
                dtgTransformacion.DataSource = dtListaTransformaciones;
                if (dtListaTransformaciones.Rows.Count > 0)
                {
                    dgvTransformacionVista.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvTransformacionVista.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvTransformacionVista.Columns["CANTIDAD_USO"].Summary.Clear();
                    dgvTransformacionVista.Columns["CANTIDAD_USO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD_USO", "Total = {0:N2}");

                    dgvTransformacionVista.BestFitColumns();
                }
            }
        }


        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTransformacion(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTransformacion(); }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTransformacion(); }
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTransformacion(); }
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTransformacion(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarTransformacion(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgTransformacion.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE TRANSFORMACIONES - SALAVERRY - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgTransformacion.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
