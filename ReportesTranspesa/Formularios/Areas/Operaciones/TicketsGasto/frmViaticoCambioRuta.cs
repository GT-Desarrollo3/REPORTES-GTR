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
    public partial class frmViaticoCambioRuta : Form
    {
        public int idRutaOriginal, idRutaNueva, Redondeo, idProg;
        public int frmLPT = 0;
        public frmListaPlanillasTolvas frmListaPlanillasTolvas;
        public frmOperacion_Previajes frmOperacion_Previajes;
        public frmListarPlanillas frmListarPlanillas;

        public frmViaticoCambioRuta()
        {
            InitializeComponent();
            cbxRuta.SelectedIndexChanged -= cbxRuta_SelectedIndexChanged;
        }

        private void cbxRuta_SelectedIndexChanged(object sender, EventArgs e) { CargarComboRuta(); }

        private void frmViaticoCambioRuta_Load(object sender, EventArgs e)
        {
            cbRedondeo.Checked = false;
            cbRedondeo_CheckedChanged(sender, e);
        }


        public void CargarComboRuta()
        {
            DataTable dtRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(5, -1, 1, -1);
            cbxRuta.DataSource = dtRutas;
            cbxRuta.DisplayMember = "RUTA";
            cbxRuta.ValueMember = "IdRuta";
        }


        private void cbRedondeo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRedondeo.Checked == true) { Redondeo = 0; }

            if (cbRedondeo.Checked == false) { Redondeo = 1; }

            DataTable dtRuta = new DataTable();

            if (idProg == 1) { dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(cbxRuta.SelectedValue), idProg, Redondeo); }
            else { dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(idRutaNueva, idProg, Redondeo); }

            if (dtRuta.Rows.Count > 0)
            {
                txtNuevoGasto.Text = dtRuta.Rows[0]["GastoTotal"].ToString();
                txtGastoAdicional.Text = Convert.ToString(Convert.ToDecimal(txtNuevoGasto.Text) - Convert.ToDecimal(txtGastoOriginal.Text));

                if (Convert.ToDecimal(txtGastoAdicional.Text) <= 0) { btnGenerarAdicional.Enabled = false; }
                else { btnGenerarAdicional.Enabled = true; }
            }
        }

        public void cbxRuta_DropDownClosed(object sender, EventArgs e)
        {
            DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(Convert.ToInt32(cbxRuta.SelectedValue), 1, Redondeo);
            
            if (dtRuta.Rows.Count > 0)
            {
                idRutaNueva = Convert.ToInt32(cbxRuta.SelectedValue);
                txtNuevoGasto.Text = dtRuta.Rows[0]["GastoTotal"].ToString();
                txtGastoAdicional.Text = Convert.ToString(Convert.ToDecimal(txtNuevoGasto.Text) - Convert.ToDecimal(txtGastoOriginal.Text));

                if (Convert.ToDecimal(txtGastoAdicional.Text) <= 0) { btnGenerarAdicional.Enabled = false; }
                else { btnGenerarAdicional.Enabled = true; }
            }
        }

        private void btnGenerarAdicional_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            string Respuesta;

            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarViaticosRuta(lblPlanilla.Text, Convert.ToDecimal(txtGastoOriginal.Text), idRutaNueva,
                                                     Convert.ToDecimal(txtGastoAdicional.Text), Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                try
                {
                    DataTable dtConsultarImpresora = new DataTable();
                    dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                    DataTable dtListaTicket = new DataTable();
                    dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaRutas(lblPlanilla.Text);
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
                            ticket.AddSubHeaderLine("Motivo: CAMBIO DE RUTA");
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
                catch
                {
                    MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                
                if (frmLPT == 0) { frmOperacion_Previajes.CargarDatos(); }
                if (frmLPT == 1) { frmListaPlanillasTolvas.btnBuscar_Click(sender, e); }
                if (frmLPT == 2) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                
                this.Close();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGenerarAdicional.Focus();
            }
        }
    }
}
