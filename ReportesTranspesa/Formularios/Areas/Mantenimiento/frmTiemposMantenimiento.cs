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
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Globalization;
using System.Diagnostics;
using Comun;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmTiemposMantenimiento : Form
    {
        public int xClick = 0, yClick = 0;

        public frmTiemposMantenimiento()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxTipoVehiculo.SelectedIndexChanged -= cbxTipoVehiculo_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void cbxTipoVehiculo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSubTipo(); }


        private void frmTiemposMantenimiento_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            dtpPeriodo.Value = new DateTime(date.Year, date.Month, 1);
            CargarComboOperaciones();
            CargarComboSubTipo();

            cbxOperaciones.SelectedValue = 5;
            cbxTipoVehiculo.Text = "TODOS";

            ListarTiemposMtto();
            ListarResumen();
        }


        private void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperaciones;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        private void CargarComboOperacionesInsertar()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperacionInsertar.DataSource = dtOperaciones;
            cbxOperacionInsertar.DisplayMember = "Descripcion";
            cbxOperacionInsertar.ValueMember = "IdOperacion";
        }

        private void CargarComboSubTipo()
        {
            DataTable dtSubTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(2, 5);
            cbxTipoVehiculo.DataSource = dtSubTipo;
            cbxTipoVehiculo.DisplayMember = "Descripcion";
            cbxTipoVehiculo.ValueMember = "idSubTipoVehiculo";
        }

        public void ListarTiemposMtto()
        {
            string periodo = dtpPeriodo.Value.ToString("MMyyyy");
            string placa = txtPlaca.Text.Trim();
            string operacion = string.IsNullOrEmpty(cbxOperaciones.Text) ? "TODO" : cbxOperaciones.Text.Trim();
            string tipoVehiculo = string.IsNullOrEmpty(cbxTipoVehiculo.Text) ? "TODOS" : cbxTipoVehiculo.Text.Trim();

            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TiemposMtto_ListarTiemposMtto(periodo, placa, operacion, tipoVehiculo);

            if (dt != null && dt.Rows.Count > 0)
            {
                dtgTotalTiempos.DataSource = dt;
                if (dgvTotalTiemposView.Columns["IdUnidad"] != null) { dgvTotalTiemposView.Columns["IdUnidad"].Visible = false; }

                dgvTotalTiemposView.Columns["PLACA"].Summary.Clear();
                dgvTotalTiemposView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Placas: {0}");
                dgvTotalTiemposView.Columns["TOTAL_SOLICITUDES"].Summary.Clear();
                dgvTotalTiemposView.Columns["TOTAL_SOLICITUDES"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL_SOLICITUDES", "Total: {0}");
                dgvTotalTiemposView.Columns["TOTAL_FALLAS"].Summary.Clear();
                dgvTotalTiemposView.Columns["TOTAL_FALLAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL_FALLAS", "Total: {0}");

                dgvTotalTiemposView.BestFitColumns();
            }
            else { dtgTotalTiempos.DataSource = null; }
        }

        public void ListarResumen()
        {
            string periodo = dtpPeriodo.Value.ToString("MMyyyy");
            string tipoVehiculo = string.IsNullOrEmpty(cbxTipoVehiculo.Text) ? "TODOS" : cbxTipoVehiculo.Text.Trim();

            DataTable dtMTTR = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TiemposMtto_ListarResumen(2, periodo, tipoVehiculo);
            if (dtMTTR != null && dtMTTR.Rows.Count > 0)
            {
                dtgMTTR.DataSource = dtMTTR;
                dgvMTTRView.BestFitColumns();
            }
            else { dtgMTTR.DataSource = null; }

            DataTable dtMTBF = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TiemposMtto_ListarResumen(1, periodo, tipoVehiculo);
            if (dtMTBF != null && dtMTBF.Rows.Count > 0)
            {
                dtgMTBF.DataSource = dtMTBF;
                dgvMTBFView.BestFitColumns();
            }
            else { dtgMTBF.DataSource = null; }
        }

        public void ListarTiemposOP()
        {
            string periodo = dtpPeriodoInsertar.Value.ToString("MMyyyy");
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TiemposMtto_ListarTiemposOP(periodo);

            if (dt != null && dt.Rows.Count > 0)
            {
                dtgTiemposOP.DataSource = dt;
                if (dgvTiemposOP.Columns["FechaModifica"] != null)
                {
                    dgvTiemposOP.Columns["FechaModifica"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dgvTiemposOP.Columns["FechaModifica"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
                }
                dgvTiemposOP.BestFitColumns();
            }
            else { dtgTiemposOP.DataSource = null; }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTiemposMtto(); }
        }

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarTiemposMtto();
                ListarResumen();
            }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { ListarTiemposMtto(); }

        private void cbxTipoVehiculo_DropDownClosed(object sender, EventArgs e)
        {
            ListarTiemposMtto();
            ListarResumen();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarTiemposMtto();
            ListarResumen();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgTotalTiempos.DataSource == null && dtgMTTR.DataSource == null && dtgMTBF.DataSource == null)
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay datos para exportar.";
                    m.ShowDialog();
                    return;
                }

                dtgTotalTiempos.ForceInitialize();
                dtgMTTR.ForceInitialize();
                dtgMTBF.ForceInitialize();

                PrintingSystem ps = new PrintingSystem();
                ps.XlsxDocumentCreated += (s, args) =>
                {
                    if (args.SheetNames.Length >= 3)
                    {
                        args.SheetNames[0] = "TOTAL";
                        args.SheetNames[1] = "MTTR";
                        args.SheetNames[2] = "MTBF";
                    }
                };

                CompositeLink compositeLink = new CompositeLink(ps);

                PrintableComponentLink linkTotal = new PrintableComponentLink(ps);
                linkTotal.Component = dtgTotalTiempos;

                PrintableComponentLink linkMTTR = new PrintableComponentLink(ps);
                linkMTTR.Component = dtgMTTR;

                PrintableComponentLink linkMTBF = new PrintableComponentLink(ps);
                linkMTBF.Component = dtgMTBF;

                compositeLink.Links.Add(linkTotal);
                compositeLink.Links.Add(linkMTTR);
                compositeLink.Links.Add(linkMTBF);

                compositeLink.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "TIEMPOS DE MANTENIMIENTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsertarTiempos_Click(object sender, EventArgs e)
        {
            CargarComboOperacionesInsertar();
            dtpPeriodoInsertar.Value = dtpPeriodo.Value;
            txtHoras.Text = "24";
            txtDias.Text = DateTime.DaysInMonth(dtpPeriodoInsertar.Value.Year, dtpPeriodoInsertar.Value.Month).ToString();

            pInsertarTiempos.Location = new Point((this.ClientSize.Width - pInsertarTiempos.Width) / 2, (this.ClientSize.Height - pInsertarTiempos.Height) / 2);
            pInsertarTiempos.Visible = true;
            pInsertarTiempos.BringToFront();

            ListarTiemposOP();
        }

        private void dtpPeriodoInsertar_ValueChanged(object sender, EventArgs e)
        {
            txtDias.Text = DateTime.DaysInMonth(dtpPeriodoInsertar.Value.Year, dtpPeriodoInsertar.Value.Month).ToString();
            ListarTiemposOP();
        }

        private void btnGuardarTiempos_Click(object sender, EventArgs e)
        {
            if (cbxOperacionInsertar.SelectedValue == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Seleccione una operación válida.";
                m.ShowDialog();
                return;
            }

            int horas = 0, dias = 0;
            if (!int.TryParse(txtHoras.Text.Trim(), out horas) || horas <= 0)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Ingrese una cantidad válida de horas.";
                m.ShowDialog();
                txtHoras.Focus();
                return;
            }

            if (!int.TryParse(txtDias.Text.Trim(), out dias) || dias <= 0)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Ingrese una cantidad válida de días.";
                m.ShowDialog();
                txtDias.Focus();
                return;
            }

            int idOperacion = Convert.ToInt32(cbxOperacionInsertar.SelectedValue);
            string periodo = dtpPeriodoInsertar.Value.ToString("MMyyyy");
            string usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TiemposMtto_InsertarModificarTiempos(1, periodo, idOperacion, horas, dias, usuario);
            if (dt != null && dt.Rows.Count > 0)
            {
                string respta = dt.Rows[0]["exito"].ToString();
                Mensaje m = new Mensaje();
                m.mensaje = respta;
                m.ShowDialog();
            }

            ListarTiemposOP();
            ListarTiemposMtto();
            ListarResumen();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pInsertarTiempos.Visible = false;
            pInsertarTiempos.SendToBack();
        }

        private void pInsertarTiempos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pInsertarTiempos.Left = pInsertarTiempos.Left + (e.X - xClick);
                pInsertarTiempos.Top = pInsertarTiempos.Top + (e.Y - yClick);
            }
        }

        private void txtHoras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
