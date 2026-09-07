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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmReporteReqServicios : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaReqServicio = new DataTable();
        
        public frmReporteReqServicios()
        {
            InitializeComponent();
            cbxCCosto.SelectedIndexChanged -= cbxCCosto_SelectedIndexChanged;
        }

        private void cbxCCosto_SelectedIndexChanged(object sender, EventArgs e) { CargarComboCCosto(); }

        private void frmReporteReqServicios_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            CargarComboCCosto();
            ListarReqServicios();
        }


        public void CargarComboCCosto()
        {
            DataTable dtCCosto = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RequerimientoServicio_ListarCCosto("MARANDAR");
            cbxCCosto.DataSource = dtCCosto;
            cbxCCosto.DisplayMember = "DESCRIPCION";
            cbxCCosto.ValueMember = "CENTRO_COSTOS";
        }

        public void ListarReqServicios()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaReqServicio = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RequerimientoServicio_ListarReqServicio(Convert.ToString(cbxCCosto.SelectedValue), txtDescripcion.Text, txtPlaca.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                dtgvData.DataSource = dtListaReqServicio;
                if (dtListaReqServicio.Rows.Count > 0)
                {
                    dtgvDataView.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dtgvDataView.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                    dtgvDataView.Columns["REQUERIMIENTO"].Summary.Clear();
                    dtgvDataView.Columns["REQUERIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "REQUERIMIENTO", "Total = {0}");

                    dtgvDataView.BestFitColumns();
                }
            }
        }


        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReqServicios(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReqServicios(); }
        }

        private void cbxCCosto_DropDownClosed(object sender, EventArgs e) { ListarReqServicios(); }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReqServicios(); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReqServicios(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarReqServicios(); }

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
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE REQUERIMIENTOS DE SERVICIOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
