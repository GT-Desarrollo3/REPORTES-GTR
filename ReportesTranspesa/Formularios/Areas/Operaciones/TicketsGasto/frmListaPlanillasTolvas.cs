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
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Properties;
using Negocio;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmListaPlanillasTolvas : Form
    {
        public int Pendientes;
        DataTable dtListaPlanillas = new DataTable();

        public frmListaPlanillasTolvas()
        {
            InitializeComponent();
        }

        private void frmListaPlanillasTolvas_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            cbPendientes.Checked = false;
            cbPendientes_CheckedChanged(sender, e);
            ListarPlanillasTolvas();
        }


        public void ListarPlanillasTolvas()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaPlanillas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasTolvas(dtpFechaInicio.Text, dtpFechaFin.Text, txtConductor.Text,
                                                                                                                        txtPlanilla.Text, Pendientes);
                dtgListaPlanillasTolvas.DataSource = dtListaPlanillas;
                if (dtListaPlanillas.Rows.Count > 0)
                {
                    dgvListaPlanillasTolvasView.Columns["idTicketGasto"].Visible = false;
                    dgvListaPlanillasTolvasView.Columns["IdRuta"].Visible = false;
                    dgvListaPlanillasTolvasView.Columns["TipoProgramacion"].Visible = false;
                    dgvListaPlanillasTolvasView.Columns["IdTracto"].Visible = false;
                    dgvListaPlanillasTolvasView.Columns["IdConductor"].Visible = false;
                    dgvListaPlanillasTolvasView.Columns["idGastoXRutaC"].Visible = false;

                    dgvListaPlanillasTolvasView.Columns["FECHA_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaPlanillasTolvasView.Columns["FECHA_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvListaPlanillasTolvasView.BestFitColumns();
                }
            }
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmGenerarPlanillaTolvas frmGenerarPlanillaTolvas = new frmGenerarPlanillaTolvas();
            frmGenerarPlanillaTolvas.formulario = this;
            frmGenerarPlanillaTolvas.ShowDialog();
        }

        private void cbPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPendientes.Checked == true)
            {
                Pendientes = 1;
                ListarPlanillasTolvas();
            }

            if (cbPendientes.Checked == false)
            {
                Pendientes = 0;
                ListarPlanillasTolvas();
            }
        }

        public void btnBuscar_Click(object sender, EventArgs e) { ListarPlanillasTolvas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaPlanillasTolvas.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE PLANILLAS - TOLVAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaPlanillasTolvas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void txtPlanilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarPlanillasTolvas(); }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarPlanillasTolvas(); }
        }

        private void dtgListaPlanillasTolvas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Caja = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "CAJA").ToString();

                imprimirPlanillaToolStripMenuItem.Enabled = true;
                imprimirDespachoToolStripMenuItem.Enabled = true;
                gastoAdicionalToolStripMenuItem.Enabled = true;

                if (Caja == " ") { eliminarPlanillaToolStripMenuItem.Enabled = true; }
                else { eliminarPlanillaToolStripMenuItem.Enabled = false; }
            }
            catch
            {
                imprimirPlanillaToolStripMenuItem.Enabled = false;
                imprimirDespachoToolStripMenuItem.Enabled = false;
                gastoAdicionalToolStripMenuItem.Enabled = false;
                eliminarPlanillaToolStripMenuItem.Enabled = false;
            }
        }

        private void dgvListaPlanillasTolvasView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "CAJA")
            {
                if (e.CellValue.ToString() == "PAGADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "GENERADO") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }

                if (e.CellValue.ToString() == " ") { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
            }

            if (e.Column.FieldName == "CAJA_ADICIONAL")
            {
                if (e.CellValue.ToString() == "PAGADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "GENERADO") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }

                if (e.CellValue.ToString() == " ") { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
            }
        }

        private void imprimirPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string CodGasto = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "PLANILLA").ToString();

                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas2(CodGasto);

                //string NombreImpresora = "EPSON M3170 Series-AB4112";
                string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                {
                    MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (dtListaTicket.Rows.Count > 0)
                    {
                        Ticket ticket = new Ticket();

                        ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                        ticket.AddSubHeaderLine2("PL - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         ");
                        ticket.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                        ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString());
                        ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                        ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                        ticket.AddSubHeaderLine("ESTADO CAJA: " + dtListaTicket.Rows[0]["CAJA"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F.Emisión: " + dtListaTicket.Rows[0]["FECHA_EMISION"].ToString());
                        ticket.AddSubHeaderLine("F.Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.AddSubHeaderLine("FIRMA: ");
                        ticket.HeaderImage = Resources.TABLA;
                        ticket.PrintTicket(NombreImpresora);
                    }
                }
            }
            catch
            {
                MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void imprimirDespachoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Conductor = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "CONDUCTOR").ToString();
            string Tracto = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "TRACTO").ToString();
            string Carreta = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "CARRETA").ToString();
            string Ruta = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "RUTA").ToString();
            int Planilla = Convert.ToInt32(dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "PLANILLA"));
            DateTime FechaViaje = Convert.ToDateTime(dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "FECHA_VIAJE"));

            frmTicketDespacho frmTicket = new frmTicketDespacho();
            frmTicket.EnviarDatos2(Conductor, Tracto, Ruta, Carreta, Planilla);
            frmTicket.dtpFecha.Value = FechaViaje;
            frmTicket.ShowDialog(this);
        }

        private void gastoAdicionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Planilla = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "PLANILLA").ToString();
            int idConductor = Convert.ToInt32(dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "IdConductor"));
            int idRuta = Convert.ToInt32(dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "IdRuta"));

            frmGastosAdicionalesTolvas frmGastosAdicionalesTolvas = new frmGastosAdicionalesTolvas();
            frmGastosAdicionalesTolvas.idConductor = idConductor;
            frmGastosAdicionalesTolvas.lblConductor.Text = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "CONDUCTOR").ToString();
            frmGastosAdicionalesTolvas.txtPlanilla2.Text = Planilla;
            frmGastosAdicionalesTolvas.NroProgramacion = -1;
            frmGastosAdicionalesTolvas.idOperacion = 1;
            frmGastosAdicionalesTolvas.idRuta = idRuta;
            frmGastosAdicionalesTolvas.frmListaPlanillasTolvas = this;
            frmGastosAdicionalesTolvas.frmLPT = 1;
            frmGastosAdicionalesTolvas.ShowDialog(this);
        }

        private void tsCambioRuta_Click(object sender, EventArgs e)
        {
            string Planilla = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "PLANILLA").ToString();
            int IdRuta = Convert.ToInt32(dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "IdRuta"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (Planilla == "")
            {
                MessageBox.Show("No puede generar un gasto adicional a este viaje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DataTable dtGastoRuta = new DataTable();
                dtGastoRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(6, -1, Convert.ToInt32(Planilla), -1);
                if (dtGastoRuta.Rows.Count > 0)
                {
                    TicketsGasto.frmViaticoCambioRuta frmViaticoCambioRuta = new TicketsGasto.frmViaticoCambioRuta();
                    frmViaticoCambioRuta.frmLPT = 1;
                    frmViaticoCambioRuta.frmListaPlanillasTolvas = this;
                    frmViaticoCambioRuta.lblPlanilla.Text = Planilla;
                    frmViaticoCambioRuta.txtRutaOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["RUTA"]);
                    frmViaticoCambioRuta.txtGastoOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["GASTO"]);
                    frmViaticoCambioRuta.CargarComboRuta();
                    frmViaticoCambioRuta.idRutaNueva = IdRuta;
                    frmViaticoCambioRuta.cbxRuta.Text = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "RUTA").ToString();
                    frmViaticoCambioRuta.idProg = 1;
                    frmViaticoCambioRuta.cbxRuta.Visible = true;
                    frmViaticoCambioRuta.cbxRuta.BringToFront();
                    frmViaticoCambioRuta.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("La ruta asignada no tiene un gasto definido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void eliminarPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Planilla = dgvListaPlanillasTolvasView.GetRowCellValue(dgvListaPlanillasTolvasView.FocusedRowHandle, "PLANILLA").ToString();
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;       

            if (Planilla == "")
            {
                MessageBox.Show("Este viaje no tiene planilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea desvincular esta planilla?", "DESVINCULAR PLANILLA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarPlanillaTolvas(Planilla, Usuario);     
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarPlanillasTolvas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
