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

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.SolicitudesPersonal
{
    public partial class frmNuevaSolicitudPersonal : MetroFramework.Forms.MetroForm
    {
        public int reemplazo = 0, opcion = 0;
        public int idSolicitudPersonal;
        frmListaSolicitudesPersonal _formulario;
        DataTable dtSolicitud;

        public frmNuevaSolicitudPersonal()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
            cbxTipoSolicitud.SelectedIndexChanged -= cbxTipoSolicitud_SelectedIndexChanged;
            cbxPuesto.SelectedIndexChanged -= cbxPuesto_SelectedIndexChanged;
            cbxEstado.SelectedIndexChanged -= cbxEstado_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void cbxTipoSolicitud_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTipo(); }

        private void cbxPuesto_SelectedIndexChanged(object sender, EventArgs e) { CargarComboPuesto(); }

        private void cbxEstado_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEstado(); }

        private void frmNuevaSolicitud_Load(object sender, EventArgs e)
        {
            
        }


        public void RecibirDatos(frmListaSolicitudesPersonal formulario) {  _formulario = formulario; }

        public void CargarComboArea()
        {
            DataTable dtArea = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(1);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "Nombre";
            cbxArea.ValueMember = "CodAreaSpring";
        }

        public void CargarComboTipo()
        {
            DataTable dtTipo = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(3);
            cbxTipoSolicitud.DataSource = dtTipo;
            cbxTipoSolicitud.DisplayMember = "NombreTipo";
            cbxTipoSolicitud.ValueMember = "idTipo";
        }

        public void CargarComboPuesto()
        {
            DataTable dtPuesto = clsSeguridadBL.Instancia.GetListarPuestos();
            cbxPuesto.DataSource = dtPuesto;
            cbxPuesto.DisplayMember = "Descripcion";
            cbxPuesto.ValueMember = "CodigoPuesto";
        }

        public void CargarComboEstado()
        {
            DataTable dtEstado = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(5);
            cbxEstado.DataSource = dtEstado;
            cbxEstado.DisplayMember = "NombreEstado";
            cbxEstado.ValueMember = "idEstadoSolicitud";
        }

        public void ListarSolicitud(int idSolicitudPersonal)
        {
            dtSolicitud = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitud(idSolicitudPersonal);
            if (dtSolicitud.Rows.Count > 0)
            {
                cbxArea.Text = dtSolicitud.Rows[0]["Area"].ToString();
                cbxPuesto.Text = dtSolicitud.Rows[0]["Puesto"].ToString();
                txtNroVacante.Text = dtSolicitud.Rows[0]["NroVacantes"].ToString();
                cbxTipoSolicitud.Text = dtSolicitud.Rows[0]["Solicitud"].ToString();
                txtReemplazo.Text = dtSolicitud.Rows[0]["Personal"].ToString();
                txtObservacion.Text = dtSolicitud.Rows[0]["Observacion"].ToString();
                cbxEstado.Text = dtSolicitud.Rows[0]["Estado"].ToString();
                cbxPrioridad.Text = dtSolicitud.Rows[0]["Prioridad"].ToString();

                dtpFechaEntrega.Value = Convert.ToDateTime(dtSolicitud.Rows[0]["FechaEntrega"]);
            }
        }


        private void txtNroVacante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        public void cbxTipoSolicitud_DropDownClosed(object sender, EventArgs e)
        {
            int idTipo = Convert.ToInt32(cbxTipoSolicitud.SelectedValue);
            if (idTipo != 1)
            {
                txtReemplazo.Enabled = true;
                txtReemplazo.BackColor = Color.LightCyan;
            }
            else
            {
                txtReemplazo.Enabled = false;
                txtReemplazo.BackColor = Color.Gainsboro;
            }
        }

        private void txtPersonalReemplazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(txtReemplazo.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 400;
            lstPersona.Columns[2].Width = 0;
            lstPersona.Columns[3].Width = 0;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                reemplazo = 0;
            }
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0)) { lstPersona.Items[0].Selected = true; }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersona.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersona.SelectedItems[0];
                reemplazo = Int32.Parse(ItemActual.Text);
                txtReemplazo.Text = ItemActual.SubItems[1].Text;
                lstPersona.Visible = false;
                txtObservacion.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                txtReemplazo.Focus();
            }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersona.SelectedItems[0];
            reemplazo = Int32.Parse(ItemActual.Text);
            txtReemplazo.Text = ItemActual.SubItems[1].Text;
            lstPersona.Visible = false;
            txtObservacion.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cbxTipoSolicitud.SelectedValue = 1;
            cbxTipoSolicitud_DropDownClosed(sender, e);
            CargarComboArea();
            CargarComboPuesto();
            txtReemplazo.Clear();
            reemplazo = 0;
            cbxPrioridad.Text = "NORMAL";
            txtNroVacante.Clear();
            txtObservacion.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (opcion == 1)
            {
                if (txtReemplazo.Text.Length == 0)
                {
                    if (Convert.ToInt32(cbxTipoSolicitud.SelectedValue) != 1)
                    {
                        MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReemplazo.Focus();
                        return;
                    }
                }

                if (cbxPuesto.Text.Length == 0 || txtNroVacante.Text.Length == 0 || txtObservacion.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (cbxPuesto.Text.Length == 0) { cbxPuesto.Focus(); }
                    else
                    {
                        if (txtNroVacante.Text.Length == 0) { txtNroVacante.Focus(); }
                        else { txtObservacion.Focus(); }
                    }
                    return;
                }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_RegistrarSolicitudPersonal(Convert.ToInt32(cbxArea.SelectedValue), Convert.ToInt32(cbxPuesto.SelectedValue), Convert.ToInt32(txtNroVacante.Text),
                                                                                                                                 Convert.ToInt32(cbxTipoSolicitud.SelectedValue), cbxPrioridad.Text, reemplazo, txtObservacion.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _formulario.ListarSolicitudes();
                        this.Close();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_EditarSolicitud(1, idSolicitudPersonal, Convert.ToInt32(cbxEstado.SelectedValue), DateTime.Now, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _formulario.ListarSolicitudes();
                this.Close();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnFechaEntrega_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_EditarSolicitud(2, idSolicitudPersonal, Convert.ToInt32(cbxEstado.SelectedValue), dtpFechaEntrega.Value, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _formulario.ListarSolicitudes();
                this.Close();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
