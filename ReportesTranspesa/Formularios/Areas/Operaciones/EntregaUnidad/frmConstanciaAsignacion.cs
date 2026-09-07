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
    public partial class frmConstanciaAsignacion : Form
    {
        public int idConductorViaje = -1, idTracto = -1, idConductor = -1;
        public frmConstanciaUnidades frmConstanciaUnidades = new frmConstanciaUnidades();
        
        public frmConstanciaAsignacion()
        {
            InitializeComponent();
            cbxObservacion.SelectedIndexChanged -= cbxObservacion_SelectedIndexChanged;
        }

        private void cbxObservacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMotivo(); }

        private void frmConstanciaAsignacion_Shown(object sender, EventArgs e) { txtPlacaViaje.Focus(); }

        private void frmConstanciaAsignacion_Load(object sender, EventArgs e) { }


        public void CargarComboMotivo()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarMotivos(2, "");
            cbxObservacion.DataSource = dtOperacion;
            cbxObservacion.DisplayMember = "Descripcion";
            cbxObservacion.ValueMember = "idMotivo";
        }

        public void BuscarUltimoViaje(int idTracto)
        {
            DataTable dtUltimoViaje = new DataTable();
            dtUltimoViaje = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_BuscarConductor(idTracto);

            if (dtUltimoViaje.Rows.Count > 0)
            {
                txtPreviaje.Text = dtUltimoViaje.Rows[0]["NroTicket"].ToString();
                idConductorViaje = Convert.ToInt32(dtUltimoViaje.Rows[0]["IdConductor"]);
                txtConductorViaje.Text = dtUltimoViaje.Rows[0]["Nombre"].ToString();
                txtNConductor.Focus();
            }
            else
            {
                MessageBox.Show("No existe un viaje asignado a esta unidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPlacaViaje.Clear();
                idTracto = -1;
                txtPreviaje.Clear();
                txtConductorViaje.Clear();
                idConductorViaje = -1;
                txtPlacaViaje.Focus();
            }
        }


        private void txtPlacaViaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlacas, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtPlacaViaje.Text), true, false, false);
            lstPlacas.Columns[0].Width = 0;
            lstPlacas.Columns[1].Width = 80;
            lstPlacas.Columns[2].Width = 100;
            lstPlacas.Columns[3].Width = 0;
            lstPlacas.Columns[4].Width = 130;
            lstPlacas.Columns[5].Width = 0;
            lstPlacas.BringToFront();
            lstPlacas.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                txtPreviaje.Clear();
                txtConductorViaje.Clear();
                idConductorViaje = -1;

                lstPlacas.Visible = false;
                lstPlacas.SendToBack();
            }
        }

        private void txtPlacaViaje_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlacas.Focus(); }
        }

        private void lstPlacas_Enter(object sender, EventArgs e)
        {
            if (!lstPlacas.Items.Count.Equals(0)) { lstPlacas.Items[0].Selected = true; }
        }

        private void lstPlacas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlacas.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlacas.SelectedItems[0];

                idTracto = Int32.Parse(ItemActual.Text);
                txtPlacaViaje.Text = ItemActual.SubItems[1].Text;

                BuscarUltimoViaje(idTracto);

                lstPlacas.Visible = false;
                lstPlacas.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                txtPreviaje.Clear();
                txtConductorViaje.Clear();
                idConductorViaje = -1;

                lstPlacas.Visible = false;
                lstPlacas.SendToBack();
            }
        }

        private void lstPlacas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlacas.SelectedItems[0];

            idTracto = Int32.Parse(ItemActual.Text);
            txtPlacaViaje.Text = ItemActual.SubItems[1].Text;

            BuscarUltimoViaje(idTracto);

            lstPlacas.Visible = false;
            lstPlacas.SendToBack();
        }

        private void txtNConductor_KeyPress(object sender, KeyPressEventArgs e)
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
                idConductor = -1;
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
                cbxObservacion.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductor = -1;
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
            cbxObservacion.Focus();
        }

        private void cbxObservacion_DropDownClosed(object sender, EventArgs e)
        {
            /*
            if (cbxObservacion.Text == "VACACIONES" || cbxObservacion.Text == "COMPENSACIÓN" || cbxObservacion.Text == "DESCANSO MÉDICO" || 
                cbxObservacion.Text == "CESE DE CONDUCTOR")
            {
                idConductor = -1;
                txtNConductor.Clear();
                txtNConductor.Enabled = false;
            }
            else { txtNConductor.Enabled = true; }
            */

            txtMotivo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtPlacaViaje.Clear();
            idTracto = -1;
            txtPreviaje.Clear();
            txtConductorViaje.Clear();
            idConductorViaje = -1;

            txtNConductor.Clear();
            idConductor = -1;
            cbxObservacion.Text = "ASIGNACIÓN DE UNIDAD";
            cbxObservacion_DropDownClosed(sender, e);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPlacaViaje.Text.Length == 0 || txtMotivo.Text.Length == 0)
            {
                if (txtPlacaViaje.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese la placa de la unidad a entregar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPlacaViaje.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, especifique el motivo de la asignación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMotivo.Focus();
                }

                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion(1, "", idTracto, Convert.ToInt32(txtPreviaje.Text),
                                                             idConductorViaje, idConductor, cbxObservacion.Text, txtMotivo.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA != "-")
                    {
                        MessageBox.Show("Asignación Registrada. N° " + Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmConstanciaUnidades.ListarAsignacion();
                        this.Close();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("Error generando la constancia de asignación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
