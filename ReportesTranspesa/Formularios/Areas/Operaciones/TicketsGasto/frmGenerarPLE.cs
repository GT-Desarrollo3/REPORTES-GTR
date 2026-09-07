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
    public partial class frmGenerarPLE : Form
    {
        public int idConductorA, idConductorN, idRuta, Redondeo, NroTicket, idOperacion;
        public string Planilla;
        
        public frmGenerarPLE()
        {
            InitializeComponent();
        }

        private void frmGenerarPLE_Load(object sender, EventArgs e)
        {
            ListarPlanillaOriginal();
            cbRedondeo.Checked = false;
            cbRedondeo_CheckedChanged(sender, e);
        }


        public void ListarPlanillaOriginal()
        {
            DataTable TablaConductor = new DataTable();
            TablaConductor = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConductorAnterior(txtPlanilla.Text);

            if (TablaConductor.Rows.Count > 0)
            {
                idOperacion = Convert.ToInt32(TablaConductor.Rows[0]["IdOperacion"]);
                txtConductorA.Text = Convert.ToString(TablaConductor.Rows[0]["NOMBRE"]);
            }
        }


        private void cbRedondeo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRedondeo.Checked == true) { Redondeo = 0; }

            if (cbRedondeo.Checked == false) { Redondeo = 1; }

            DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(idRuta, idOperacion, Redondeo);

            if (dtRuta.Rows.Count > 0) { txtGastoRuta.Text = dtRuta.Rows[0]["GastoTotal"].ToString(); }
        }

        private void btnGenerarPLE_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPlanilla.Text.Length == 0)
                { MessageBox.Show("No se pudo generar la planilla de evento porque no se encontró la planilla anterior.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento(txtPlanilla.Text, NroTicket, idOperacion,
                                                             idRuta, idConductorN, Convert.ToDecimal(txtGastoRuta.Text), Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    Planilla = Respuesta.Substring(41, 8);
                    
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            DataTable dtConsultarImpresora = new DataTable();
                            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                            DataTable dtListaTicket = new DataTable();
                            dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(Planilla);

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
                                    ticket.AddSubHeaderLine2("PLE - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         ");
                                    ticket.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                                    ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                                    ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                                    ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                                    ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                                    ticket.AddSubHeaderLine("                              ");
                                    ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
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
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("No se pudo generar la planilla de evento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
