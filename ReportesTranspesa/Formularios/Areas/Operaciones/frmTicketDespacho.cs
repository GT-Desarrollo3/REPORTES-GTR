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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmTicketDespacho : Form
    {
        public int _NroTicket = 0, _Planilla;
        public string _Conductor, _Tracto, _Ruta, _Carreta;
        public string Correlativo;

        public frmTicketDespacho()
        {
            InitializeComponent();
        }

        private void frmTicketDespacho_Load(object sender, EventArgs e)
        {
            ListarRegistro();

            if (txtCarreta.Text.Length == 0 && txtProgramacion.Text == "TOLVAS") { btnImprimir.Enabled = false; }
            else { btnImprimir.Enabled = true; }
        }


        public void EnviarDatos(int NroTicket) { _NroTicket = NroTicket; }

        public void EnviarDatos2(string Conductor, string Tracto, string Ruta, string Carreta, int Planilla)
        {
            _Conductor = Conductor;
            _Tracto = Tracto;
            _Ruta = Ruta;
            _Carreta = Carreta;
            _Planilla = Planilla;
        }

        public void ListarRegistro()
        {
            DataTable dtCorrelativo = new DataTable();
            dtCorrelativo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(6, "");
            Correlativo = Convert.ToString(dtCorrelativo.Rows[0]["Codigo"]);
            lblCorrelativo.Text = Correlativo;

            if (_NroTicket == 0)
            {
                txtConductor.Text = _Conductor;
                txtTracto.Text = _Tracto;
                txtCarreta.Text = _Carreta;
                txtRuta.Text = _Ruta;
                txtProgramacion.Text = "TOLVAS";
            }
            else
            {
                DataTable dtListaRegistros = new DataTable();
                dtListaRegistros = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro(_NroTicket);

                if (dtListaRegistros.Rows.Count > 0)
                {
                    for (int i = 0; i < dtListaRegistros.Rows.Count; i++)
                    {
                        txtConductor.Text = dtListaRegistros.Rows[i]["CONDUCTOR"].ToString();
                        txtTracto.Text = dtListaRegistros.Rows[i]["TRACTO"].ToString();
                        txtCarreta.Text = dtListaRegistros.Rows[i]["SEMIRREMOLQUE"].ToString();
                        txtRuta.Text = dtListaRegistros.Rows[i]["RUTA"].ToString();
                        txtProgramacion.Text = dtListaRegistros.Rows[i]["PROGRAMACION"].ToString();
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (_NroTicket == 0) { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarDespacho(_Planilla, dtpFecha.Value, Utilitario.Instancia.SesionUsuario.usuario); }
                else { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarDespacho(_NroTicket, dtpFecha.Value, Utilitario.Instancia.SesionUsuario.usuario); }
                
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                
                if (NroRPTA == "0")
                {
                    DataTable dtConsultarImpresora = new DataTable();
                    dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
                    string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                    if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                    {
                        MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        Ticket ticket = new Ticket();

                        ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                        ticket.AddSubHeaderLine2("ORDEN DE TRABAJO");
                        //ticket.AddSubHeaderLine2("TICKET DE DESPACHO");
                        //ticket.AddSubHeaderLine2("DE UNIDAD");
                        ticket.AddSubHeaderLine2(" ");
                        ticket.AddSubHeaderLine2("N° " + Correlativo + "                         ");

                        ticket.AddSubHeaderLine("F. Salida: " + dtpFecha.Text);
                        if (_NroTicket == 0) { ticket.AddSubHeaderLine("Tracto: " + txtTracto.Text); }
                        else { ticket.AddSubHeaderLine("Tracto: " + txtTracto.Text + "   Carreta: " + txtCarreta.Text); }
                        ticket.AddSubHeaderLine("Conductor: " + txtConductor.Text);
                        ticket.AddSubHeaderLine("Destino: " + txtRuta.Text);
                        ticket.AddSubHeaderLine("Programación: " + txtProgramacion.Text);
                        ticket.HeaderImage = Resources.DESPACHO;
                        ticket.PrintTicket(NombreImpresora);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpFecha.Focus();
                }
            }
            catch
            {
                MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
