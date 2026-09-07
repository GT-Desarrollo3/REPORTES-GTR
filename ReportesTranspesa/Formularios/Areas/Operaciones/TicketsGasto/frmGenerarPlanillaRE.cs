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
    public partial class frmGenerarPlanillaRE : Form
    {
        public frmListarPlanillas frmListarPlanillas;
        public int idOperacion, idRuta, idConductorP, idConductorR, redondeo;
        public string CodGasto;

        public frmGenerarPlanillaRE()
        {
            InitializeComponent();
        }

        private void frmGenerarPlanillaRE_Load(object sender, EventArgs e)
        {
            ListarPreviajeR();
        }


        public void ListarPreviajeR()
        {
            DataTable dtPreviajeR = new DataTable();
            dtPreviajeR = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPreviajeR(Convert.ToInt32(txtPreviaje.Text));

            if (dtPreviajeR.Rows.Count > 0)
            {
                idConductorR = Convert.ToInt32(dtPreviajeR.Rows[0]["IdConductorApoyo"]);
                txtConductorR.Text = Convert.ToString(dtPreviajeR.Rows[0]["CONDUCTOR_APOYO"]);
                txtGastoRuta.Text = Convert.ToString(dtPreviajeR.Rows[0]["GASTO"]);
            }

            /*
            if (idOperacion == 2 || idOperacion == 9 || idOperacion == 3) { }

            if (idOperacion == 10)
            {
                DataTable dtPreviajeR2 = new DataTable();

                idConductorR = Convert.ToInt32(dtPreviajeR.Rows[0]["IdConductorApoyo"]);
                txtConductorR.Text = Convert.ToString(dtPreviajeR.Rows[0]["CONDUCTOR_APOYO"]);
                dtPreviajeR2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(idRuta, idOperacion, redondeo);

                if (dtPreviajeR2.Rows.Count > 0) { txtGastoRuta.Text = dtPreviajeR2.Rows[0]["GastoTotal"].ToString(); }
            }
            */
        }


        private void txtConductorR_Enter(object sender, EventArgs e) { txtConductorR.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtConductorR_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstConductor, clsConsultaBL.Instancia.GetConductores(txtConductorR.Text), true, false, false);
            lstConductor.Columns[0].Width = 0;
            lstConductor.Columns[1].Width = 280;
            lstConductor.Columns[2].Width = 100;
            lstConductor.BringToFront();
            lstConductor.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductorR = -1;
            }
        }

        private void txtConductorR_KeyUp(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Down) { lstConductor.Focus(); } }

        private void txtConductorR_Leave(object sender, EventArgs e) { txtConductorR.BackColor = Color.White; }

        private void lstConductor_Enter(object sender, EventArgs e)
        { if (!lstConductor.Items.Count.Equals(0)) { lstConductor.Items[0].Selected = true; } }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];

                idConductorR = Int32.Parse(ItemActual.Text);
                txtConductorR.Text = ItemActual.SubItems[1].Text;

                lstConductor.Visible = false;
                lstConductor.SendToBack();
                btnGenerarPLR.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductorR = -1;
            }
        }

        private void lstConductor_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstConductor.SelectedItems[0];

            idConductorR = Int32.Parse(ItemActual.Text);
            txtConductorR.Text = ItemActual.SubItems[1].Text;

            lstConductor.Visible = false;
            lstConductor.SendToBack();
            btnGenerarPLR.Focus();
        }

        public void cbRedondear_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRedondear.Checked == true)
            {
                redondeo = 0;
                ListarPreviajeR();
            }

            if (cbRedondear.Checked == false)
            {
                redondeo = 1;
                ListarPreviajeR();
            }
        }

        private void btnGenerarPLR_Click(object sender, EventArgs e)
        {
            if (txtConductorR.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese al conductor de apoyo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConductorR.Focus();
                return;
            }

            if (Convert.ToDecimal(txtGastoRuta.Text) == 0)
            {
                MessageBox.Show("Esta ruta no cuenta con gasto de reconocimiento.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConductorR.Focus();
                return;
            }

            if (txtPreviaje.Text.Length == 0 || txtPlanilla.Text.Length == 0 || txtConductorP.Text.Length == 0 || txtRuta.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaR(Convert.ToInt32(txtPreviaje.Text), txtPlanilla.Text,
                                                         idConductorR, Convert.ToDecimal(txtGastoRuta.Text), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodGasto = Respuesta.Substring(30, 8);

                    try
                    {
                        DataTable dtConsultarImpresora = new DataTable();
                        dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                        DataTable dtListaTicket = new DataTable();
                        dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(CodGasto);

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
                                if (idOperacion == 2)
                                { ticket.AddSubHeaderLine2("PLR - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }
                                if (idOperacion == 3 || idOperacion == 10)
                                { ticket.AddSubHeaderLine2("PLA - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }
                                ticket.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                                ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                                ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                                ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                                ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                                ticket.AddSubHeaderLine("                              ");
                                ticket.AddSubHeaderLine("TOTAL VIÁTICO R.: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                                ticket.AddSubHeaderLine("                              ");
                                ticket.AddSubHeaderLine("F.Emisión: " + dtListaTicket.Rows[0]["FECHA_EMISION"].ToString());
                                ticket.AddSubHeaderLine("F.Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                ticket.AddSubHeaderLine("FIRMA: ");
                                ticket.HeaderImage = Resources.TABLA;

                                ticket.PrintTicket(NombreImpresora);

                                string Usuario2 = Utilitario.Instancia.SesionUsuario.usuario;

                                if (Usuario2 != "EGENNELL" && Usuario != "LQUEZADA" && Usuario != "RCCAMA" && Usuario != "MADELEINEC" && Usuario != "JALBAN")
                                {
                                    DataTable dtCorrelativo = new DataTable();
                                    dtCorrelativo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(6, "");
                                    string Correlativo = Convert.ToString(dtCorrelativo.Rows[0]["Codigo"]);

                                    Ticket ticket2 = new Ticket();
                                    ticket2.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                                    ticket2.AddSubHeaderLine2("TICKET DE DESPACHO");
                                    ticket2.AddSubHeaderLine2("DE UNIDAD");
                                    ticket2.AddSubHeaderLine2("N° " + Correlativo + "                         ");
                                    ticket2.AddSubHeaderLine("F. Salida: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                                    ticket2.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   Carreta: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                                    ticket2.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                                    ticket2.AddSubHeaderLine("Destino: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                                    ticket2.AddSubHeaderLine("Programación: " + dtListaTicket.Rows[0]["PROGRAMACION"].ToString());
                                    ticket2.HeaderImage = Resources.DESPACHO;
                                    ticket2.PrintTicket(NombreImpresora);
                                }
                            }
                        }
                    }
                    catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    if (frmListarPlanillas != null) { frmListarPlanillas.btnBuscar_Click(sender, e); }
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
