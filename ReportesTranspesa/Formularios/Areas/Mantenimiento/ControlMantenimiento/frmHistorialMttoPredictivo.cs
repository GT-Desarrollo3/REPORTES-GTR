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
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmHistorialMttoPredictivo : Form
    {
        DataTable dtMttoPredictivo = new DataTable();
        int Tecnica;
        RepositoryItemHyperLinkEdit DirectorioPDF = new RepositoryItemHyperLinkEdit();

        public frmHistorialMttoPredictivo()
        {
            InitializeComponent();
            cbxMPTipo.SelectedIndexChanged -= cbxMPTipo_SelectedIndexChanged;
            cbxMPTecnica.SelectedIndexChanged -= cbxMPTecnica_SelectedIndexChanged;
            cbxMPSistema.SelectedIndexChanged -= cbxMPSistema_SelectedIndexChanged;
        }

        private void cbxMPTipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidad(); }

        private void cbxMPTecnica_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTecnica(); }

        private void cbxMPSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSistema(); }

        private void frmHistorialMttoPredictivo_Load(object sender, EventArgs e)
        {
            CargarComboUnidad();
            CargarComboTecnica();
            Tecnica = 1;
            cbxMPTecnica_DropDownClosed(sender, e);

            ListarHistorialPredictivo();
        }


        public void CargarComboUnidad()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(5, 1);
            cbxMPTipo.DataSource = dtTipo;
            cbxMPTipo.DisplayMember = "Descripcion";
            cbxMPTipo.ValueMember = "idTipoVehiculo";
        }

        public void CargarComboTecnica()
        {
            DataTable dtTipo2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema(1, Tecnica);
            cbxMPTecnica.DataSource = dtTipo2;
            cbxMPTecnica.DisplayMember = "Descripcion";
            cbxMPTecnica.ValueMember = "idTecnica";
        }

        public void CargarComboSistema()
        {
            DataTable dtTipo3 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema(2, Tecnica);
            cbxMPSistema.DataSource = dtTipo3;
            cbxMPSistema.DisplayMember = "Descripcion";
            cbxMPSistema.ValueMember = "idSistema";
        }

        public void ListarHistorialPredictivo()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtMttoPredictivo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarHistorial(dtpFechaInicio.Text, dtpFechaFin.Text,
                                   txtMPUnidad.Text, Convert.ToInt32(cbxMPTipo.SelectedValue), Convert.ToInt32(cbxMPTecnica.SelectedValue), Convert.ToInt32(cbxMPSistema.SelectedValue));
                dtgHistorialMtto.DataSource = dtMttoPredictivo;

                if (dtMttoPredictivo.Rows.Count > 0)
                {
                    dgvHistorialMttoVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialMttoVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvHistorialMttoVista.Columns["UltimaFecha"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialMttoVista.Columns["UltimaFecha"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvHistorialMttoVista.Columns["ESTADO"].Summary.Clear();
                    dgvHistorialMttoVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvHistorialMttoVista.Columns["INFORME"].ColumnEdit = DirectorioPDF;

                    dgvHistorialMttoVista.BestFitColumns();

                    dgvHistorialMttoVista.Columns["INFORME"].Width = 150;
                }
            }
        }


        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialPredictivo(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialPredictivo(); }
        }

        private void txtMPUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialPredictivo(); }
        }

        private void cbxMPTipo_DropDownClosed(object sender, EventArgs e) { ListarHistorialPredictivo(); }

        private void cbxMPTecnica_DropDownClosed(object sender, EventArgs e)
        {
            Tecnica = Convert.ToInt32(cbxMPTecnica.SelectedValue);
            CargarComboSistema();
        }

        private void cbxMPSistema_DropDownClosed(object sender, EventArgs e) { ListarHistorialPredictivo(); }

        private void dgvHistorialMttoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "NORMAL")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "PRECAUCIÓN")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "ALERTA")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dtgHistorialMtto_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var hi = dgvHistorialMttoVista.CalcHitInfo(dtgHistorialMtto.PointToClient(MousePosition));

                if (hi.InRowCell)
                {
                    if (dgvHistorialMttoVista.Columns[hi.Column.FieldName].ColumnEdit is RepositoryItemHyperLinkEdit)
                    {
                        string url = dgvHistorialMttoVista.GetRowCellValue(dgvHistorialMttoVista.FocusedRowHandle, "INFORME").ToString();

                        try { System.Diagnostics.Process.Start(url); }
                        catch (Exception ex) { }
                    }
                }
            }
            catch { }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarHistorialPredictivo(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgHistorialMtto.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE MANTENIMIENTOS PREDICTIVOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgHistorialMtto.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
