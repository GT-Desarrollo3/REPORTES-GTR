using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using Negocio;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmGenerarTicketGasto : Form
    {
        int _NroProgramacion, idRuta, idOperacion, idGastoXRutaC;
        int e1 = 0;
        decimal _Gasto;
        string _Usuario;
        string CodGasto;

        public frmGenerarTicketGasto()
        {
            InitializeComponent();
        }

        private void frmGenerarTicketGasto_Shown(object sender, EventArgs e)
        {
            txtMontoEfectivo.Focus();
        }

        private void frmGenerarTicketGasto_Load(object sender, EventArgs e)
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
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Pagar Planillas")
                    {
                        btnGuardar.Enabled = true;
                        e1 = 1; i = 999;
                    }
                    else { btnGuardar.Enabled = false; }
                }
            }
            else
            { btnGuardar.Enabled = false; }
            
            ListarRegistro();
            BuscarGasto();

            DataTable dtBuscar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGasto(_NroProgramacion);
            if (dtBuscar.Rows.Count == 0)
            {
                btnImprimir.Enabled = true;
                btnGuardar.Enabled = false;
                btnVoucher.Enabled = false;
            }
            else
            {
                //txtMontoEfectivo.Text = dtBuscar.Rows[0]["TotalEntregado"].ToString();
                btnImprimir.Enabled = false;
                if (e1 == 1) { btnGuardar.Enabled = true; }
                btnVoucher.Enabled = true;
            }
        }


        public void EnviarDatos(int NroProgramacion, string Usuario, decimal Gasto)
        {
            _NroProgramacion = NroProgramacion;
            _Usuario = Usuario;
            _Gasto = Gasto;
        }

        private void ListarRegistro()
        {
            DataTable dtListaRegistros = new DataTable();
            dtListaRegistros = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro(_NroProgramacion);

            if (dtListaRegistros.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaRegistros.Rows.Count; i++)
                {
                    txtConductor.Text = dtListaRegistros.Rows[i]["CONDUCTOR"].ToString();
                    txtTracto.Text = dtListaRegistros.Rows[i]["TRACTO"].ToString();
                    txtSemirremolque.Text = dtListaRegistros.Rows[i]["SEMIRREMOLQUE"].ToString();
                    txtRuta.Text = dtListaRegistros.Rows[i]["RUTA"].ToString();
                    txtCliente.Text = dtListaRegistros.Rows[i]["CLIENTE"].ToString();
                    txtFechaViaje.Text = dtListaRegistros.Rows[i]["FECHA_VIAJE"].ToString();
                    txtProgramacion.Text = dtListaRegistros.Rows[i]["PROGRAMACION"].ToString();

                    idRuta = Convert.ToInt32(dtListaRegistros.Rows[i]["IdRuta"]);
                    idOperacion = Convert.ToInt32(dtListaRegistros.Rows[i]["IdOperacion"]);
                    txtMontoEfectivo.Text = _Gasto.ToString();
                }
            }

            lblNroTicket.Text = _NroProgramacion.ToString();
            
        }

        private void BuscarGasto()
        {
            DataTable dtListaGasto = new DataTable();
            dtListaGasto = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGasto(idRuta, idOperacion);

            if (dtListaGasto.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaGasto.Rows.Count; i++)
                { idGastoXRutaC = Convert.ToInt32(dtListaGasto.Rows[i]["idGastoxRutaC"]); }
            }
        }

        public void GuardarTicket()
        {
            if (txtMontoEfectivo.Text.Length == 0)
            {
                MessageBox.Show("Esta ruta no tiene un gasto asignado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMontoEfectivo.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto(_NroProgramacion, idGastoXRutaC, _Gasto, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodGasto = Respuesta.Substring(30, 8);

                    Imprimir(CodGasto);
                    btnImprimir.Enabled = false;
                    if (e1 == 1) { btnGuardar.Enabled = true; }
                    btnVoucher.Enabled = true;
                    btnGuardar.Focus();
                    this.Close();
                    frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                    f1.CargarDatos();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RegistrarTicket()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ImprimirPago(_NroProgramacion, idGastoXRutaC, "0", Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                f1.CargarDatos();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void Imprimir(string CodGasto)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(CodGasto);

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

                        if (dtListaTicket.Rows[0]["OBSERVACION"].ToString() == "PLANILLA EVENTO")
                        { ticket.AddSubHeaderLine2("PLE - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }
                        else
                        { ticket.AddSubHeaderLine2("PL - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         "); }

                        ticket.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                        ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                        ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                        ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                        ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + dtListaTicket.Rows[0]["GASTO_TOTAL"].ToString());
                        ticket.AddSubHeaderLine("ESTADO CAJA: " + dtListaTicket.Rows[0]["CAJA"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        /*
                        ticket.AddSubHeaderLine("==============================");
                        ticket.AddSubHeaderLine("Duración: " + dtListaTicket.Rows[0]["DIAS"].ToString() + " DÍA(S) APROX.");
                        ticket.AddSubHeaderLine("Días:");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("==============================");

                        for (int i = 0; i < dtListaTicket.Rows.Count; i++)
                        {
                            ticket.AddSubHeaderLine(dtListaTicket.Rows[i]["TIPO_GASTO"].ToString() + ": S/. " + dtListaTicket.Rows[i]["GASTO"].ToString());
                            ticket.AddSubHeaderLine("S/.:");
                            ticket.AddSubHeaderLine("                              ");
                        }

                        ticket.AddSubHeaderLine("                              ");
                        */
                        ticket.AddSubHeaderLine("F.Emisión: " + dtListaTicket.Rows[0]["FECHA_EMISION"].ToString());
                        ticket.AddSubHeaderLine("F.Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.AddSubHeaderLine("FIRMA: ");
                        ticket.HeaderImage = Resources.TABLA;
                        //ticket.AddFooterLine("    ** VIAJA CON CUIDADO **");
                        ticket.PrintTicket(NombreImpresora);

                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        if (Usuario != "EGENNELL" && Usuario != "MADELEINEC" && Usuario != "JALBAN" && Usuario != "RCCAMA" && Usuario != "LQUEZADA")    
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
                            ticket2.AddSubHeaderLine("Programación: " + txtProgramacion.Text);
                            ticket2.HeaderImage = Resources.DESPACHO;
                            ticket2.PrintTicket(NombreImpresora);
                        }
                    }

                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e) { RegistrarTicket(); }

        private void btnImprimir_Click(object sender, EventArgs e) { GuardarTicket(); }

        private void btnVoucher_Click(object sender, EventArgs e) { Imprimir(CodGasto); }
    }
}
