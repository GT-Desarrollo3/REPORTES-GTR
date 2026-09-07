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
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmGastosAdicionalesTolvas : Form
    {
        public string Planilla;
        public int idConductor, idOperacion, idRuta, idGastoXRutaC, NroProgramacion, frmLPT = 0, e1 = 0;
        public frmListaPlanillasTolvas frmListaPlanillasTolvas;
        public frmOperacion_Previajes frmOperacion_Previajes;
        public frmListarPlanillas frmListarPlanillas;
        DataTable dtAdicional = new DataTable();

        public frmGastosAdicionalesTolvas()
        {
            InitializeComponent();
            cbxTipoGasto.SelectedIndexChanged -= cbxTipoGasto_SelectedIndexChanged;
        }

        private void cbxTipoGasto_SelectedIndexChanged(object sender, EventArgs e) { CargarComboGasto(); }

        private void frmGastosAdicionalesTolvas_Load(object sender, EventArgs e)
        {
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarPlanillas");
            DataTable dtEspeciales = new DataTable();

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registrar Viaticos")
                    {
                        btnGuardar.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { btnGuardar.Enabled = false; }
                }
            }
            else { btnGuardar.Enabled = false; }
            
            dtpFechaViatico.Value = DateTime.Now;
            CargarComboGasto();
            cbxTipoGasto_DropDownClosed(sender, e);
            ListarViatico();
        }


        public void CargarComboGasto()
        {
            DataTable dtTipoGasto = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(1, idRuta, idOperacion, -1);
            cbxTipoGasto.DataSource = dtTipoGasto;
            cbxTipoGasto.DisplayMember = "Descripcion";
            cbxTipoGasto.ValueMember = "idGastoxRutaD";
        }

        public void ListarViatico()
        {
            dtAdicional = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarViaticos(1, idConductor);
            dtgvViaticos.DataSource = dtAdicional;

            if (dtAdicional.Rows.Count > 0)
            {
                dtgvViaticosView.Columns["idViatico"].Visible = false;
                dtgvViaticosView.Columns["idConductor"].Visible = false;
                //dtgvViaticosView.Columns["NroTicket"].Visible = false;

                dtgvViaticosView.Columns["IMPORTE"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViaticosView.Columns["IMPORTE"].DisplayFormat.FormatString = "n2";
                dtgvViaticosView.Columns["FECHA_VIATICO"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvViaticosView.Columns["FECHA_VIATICO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dtgvViaticosView.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvViaticosView.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dtgvViaticosView.BestFitColumns();
            }
        }


        private void txtMonto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void cbxTipoGasto_DropDownClosed(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(2, -1, -1, Convert.ToInt32(cbxTipoGasto.SelectedValue));
            if (dtRespuesta.Rows.Count > 0)
            {
                txtMonto2.Text = dtRespuesta.Rows[0]["Gasto"].ToString();
                txtMotivo.Text = cbxTipoGasto.Text;
                txtMotivo.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            string Respuesta;

            if (txtMonto2.Text.Length == 0 || txtMotivo.Text.Length == 0 || txtMotivo.Text == " ")
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtMonto2.Text.Length == 0) { txtMonto2.Focus(); }
                else { txtMotivo.Focus(); }

                return;
            }
            else
            {
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarViaticosTolvas(NroProgramacion, txtPlanilla2.Text, idConductor, Convert.ToDateTime(dtpFechaViatico.Value),
                                                         Convert.ToInt32(cbxTipoGasto.SelectedValue), Convert.ToDecimal(txtMonto2.Text), cbxTipoGasto.Text, txtMotivo.Text.ToUpper(), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    ListarViatico();
                    dtpFechaViatico.Value = DateTime.Now;

                    if (frmLPT == 0) { frmOperacion_Previajes.CargarDatos(); }
                    if (frmLPT == 1) { frmListaPlanillasTolvas.btnBuscar_Click(sender, e); }
                    if (frmLPT == 2) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpFechaViatico.Focus();
                }
            }
        }

        private void dtgvViaticos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Planilla = dtgvViaticosView.GetRowCellValue(dtgvViaticosView.FocusedRowHandle, "PLANILLA").ToString();

                if (Planilla == "") { reimprimirToolStripMenuItem.Enabled = false; }
                else { reimprimirToolStripMenuItem.Enabled = true; }
            }
            catch { reimprimirToolStripMenuItem.Enabled = false; }
        }

        private void dtgvViaticosView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "CAJA")
            {
                if (e.CellValue.ToString() == "PAGADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "GENERADO") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }

                if (e.CellValue.ToString() == " ") { e.Appearance.BackColor = Color.FromArgb(255, 255, 255); }
            }
        }

        private void reimprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Planilla2 = dtgvViaticosView.GetRowCellValue(dtgvViaticosView.FocusedRowHandle, "PLANILLA").ToString();
                int idViatico = Convert.ToInt32(dtgvViaticosView.GetRowCellValue(dtgvViaticosView.FocusedRowHandle, "idViatico"));
                
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas(Planilla2, idViatico);

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
                        ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                        ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                        ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                        ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("Al conductor se le añadió un gasto adicional de S/. " + dtListaTicket.Rows[0]["ADICIONAL"].ToString());
                        ticket.AddSubHeaderLine("Motivo: " + dtListaTicket.Rows[0]["MOTIVO"].ToString());
                        ticket.AddSubHeaderLine("ESTADO ADICIONAL: " + dtListaTicket.Rows[0]["CAJA_ADICIONAL"].ToString());
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

                    MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este gasto?", "ELIMINAR GASTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idViatico = Convert.ToInt32(dtgvViaticosView.GetRowCellValue(dtgvViaticosView.FocusedRowHandle, "idViatico"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarViaticos(NroProgramacion, idViatico, idConductor, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarViatico();
                        dtpFechaViatico.Focus();

                        if (frmLPT == 0) { frmOperacion_Previajes.CargarDatos(); }
                        if (frmLPT == 1) { frmListaPlanillasTolvas.btnBuscar_Click(sender, e); }
                        if (frmLPT == 2) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El viático seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnImprimirViatico_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas(Planilla);

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
                        ticket.AddSubHeaderLine("Chofer: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                        ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString());
                        ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                        ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("Al conductor se le añadió un gasto adicional de S/. " + dtListaTicket.Rows[0]["ADICIONAL"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.AddSubHeaderLine("FIRMA: ");
                        ticket.HeaderImage = Resources.TABLA;
                        ticket.PrintTicket(NombreImpresora);
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    MessageBox.Show("Ticket impreso.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */
        }
    }
}
