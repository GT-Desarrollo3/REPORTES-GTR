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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using Comun;
using Negocio;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.EntregaUnidad
{
    public partial class frmGenerarConstancia : Form
    {
        public int idConductorViaje, idTracto, idCarreta, idConductor = -1;
         
        public frmGenerarConstancia()
        {
            InitializeComponent();
            cbxMotivo.SelectedIndexChanged -= cbxMotivo_SelectedIndexChanged;
        }

        private void cbxMotivo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMotivo(); }

        private void frmGenerarConstancia_Shown(object sender, EventArgs e) { txtNConductor.Focus(); }

        private void frmGenerarConstancia_Load(object sender, EventArgs e)
        {
            dtpFechaProg.Value = DateTime.Now;
            dtpHoraProg.Value = DateTime.Now;
            CargarComboMotivo();
        }


        private void CargarComboMotivo()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarMotivos(2,"");
            cbxMotivo.DataSource = dtOperacion;
            cbxMotivo.DisplayMember = "Descripcion";
            cbxMotivo.ValueMember = "idMotivo";
        }

        public void Imprimir(string NroTicket)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarTicket(NroTicket);

                //string NombreImpresora = "EPSON L380 Series";
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
                        ticket.AddSubHeaderLine2("CONSTANCIA DE");
                        ticket.AddSubHeaderLine2("ENTREGA UNIDAD");
                        ticket.AddSubHeaderLine2("EU - " + dtListaTicket.Rows[0]["CodConstancia"].ToString() + "                         ");
                        ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["CARRETA"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("Conductor Anterior: " + dtListaTicket.Rows[0]["COND_ANT"].ToString());
                        ticket.AddSubHeaderLine("Conductor Nuevo: " + dtListaTicket.Rows[0]["COND_NUEVO"].ToString());
                        ticket.AddSubHeaderLine("Motivo: " + dtListaTicket.Rows[0]["MOTIVO"].ToString());
                        ticket.AddSubHeaderLine("F.Programada: " + dtListaTicket.Rows[0]["FECHA"].ToString());
                        ticket.HeaderImage = Resources.TANQUES;
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("______________________________");
                        ticket.AddSubHeaderLine("RESPONSABLE DE OPERACIONES");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("______________________________");
                        ticket.AddSubHeaderLine("RESPONSABLE DE INVENTARIOS");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("______________________________");
                        ticket.AddSubHeaderLine("FIRMA DE CONDUCTOR");
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.PrintTicket(NombreImpresora);
                    }
                }
            }
            catch { MessageBox.Show("Error tratando de imprimir la constancia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void txtNuevoConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstConductor, clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarMotivos(1, txtNConductor.Text), true, false, false);
            lstConductor.Columns[0].Width = 0;
            lstConductor.Columns[1].Width = 320;
            lstConductor.BringToFront();
            lstConductor.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductor = 0;
            }
        }

        private void txtNConductor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstConductor.Focus(); }
        }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            if (!lstConductor.Items.Count.Equals(0)) { lstConductor.Items[0].Selected = true; }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];

                idConductor = Int32.Parse(ItemActual.Text);
                txtNConductor.Text = ItemActual.SubItems[1].Text;

                lstConductor.Visible = false;
                lstConductor.SendToBack();
                dtpFechaProg.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductor = 0;
            }
        }

        private void lstConductor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstConductor.SelectedItems[0];

            idConductor = Int32.Parse(ItemActual.Text);
            txtNConductor.Text = ItemActual.SubItems[1].Text;

            lstConductor.Visible = false;
            lstConductor.SendToBack();
            dtpFechaProg.Focus();
        }

        private void dtpFechaProg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraProg.Focus(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNConductor.Clear();
            idConductor = 0;
            dtpFechaProg.Value = DateTime.Now;
            dtpHoraProg.Value = DateTime.Now;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNConductor.Text.Length == 0 && cbxMotivo.Text == "CAMBIO DE CONDUCTOR")
            {
                MessageBox.Show("Por favor, ingrese el nombre de un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNConductor.Focus();
                return;
            }

            if (dtpFechaProg.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("No puede generar una constancia en una fecha menor a la actual.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaProg.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarConstancia(1, 0, idTracto, idCarreta, idConductorViaje,
                                  idConductor, dtpFechaProg.Value.ToShortDateString(), dtpHoraProg.Value.ToShortTimeString(), cbxMotivo.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    MessageBox.Show("Constancia Generada. N° " + Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Imprimir(Respuesta);
                    this.Close();
                }
                catch { MessageBox.Show("Error generando la constancia de entrega.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
