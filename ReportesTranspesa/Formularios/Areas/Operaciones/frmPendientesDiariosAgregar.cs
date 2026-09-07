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
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmPendientesDiariosAgregar : Form
    {
        public int Opcion, idPersonal = -1, idPendiente, Contador;
        public frmPendientesDiarios formulario;

        public frmPendientesDiariosAgregar()
        {
            InitializeComponent();
        }

        private void frmPendientesDiariosAgregar_Load(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                cbxNivel.Text = "IMPORTANTE";
                cbxEstado.Text = "PENDIENTE";
                dtpFechaInicio.Value = DateTime.Now;
                dtpFechaP1.Value = DateTime.Now;
                dtpFechaP2.Value = DateTime.Now;
                dtpFechaP3.Value = DateTime.Now;
            }
        }


        public void RecibirDatos(int idActividad)
        {
            DataTable dtActividad = clsOperacionesBL.Instancia.ReportesApp_Operaciones_PendientesDiarios_FiltrarActividades(idActividad);

            if (dtActividad.Rows.Count > 0)
            {
                idPendiente = idActividad;
                idPersonal = Convert.ToInt32(dtActividad.Rows[0]["Persona"]);
                txtResponsable.Text = dtActividad.Rows[0]["Responsable"].ToString();
                txtDescripcion.Text = dtActividad.Rows[0]["Descripcion"].ToString();
                cbxNivel.Text = dtActividad.Rows[0]["Nivel"].ToString();
                cbxEstado.Text = dtActividad.Rows[0]["Estado"].ToString();
                txtSeguimiento.Text = dtActividad.Rows[0]["Seguimiento"].ToString();
                dtpFechaInicio.Value = Convert.ToDateTime(dtActividad.Rows[0]["FechaInicio"]);
                dtpFechaP1.Value = Convert.ToDateTime(dtActividad.Rows[0]["FProyectada1"]);
                dtpFechaP2.Value = Convert.ToDateTime(dtActividad.Rows[0]["FProyectada2"]);
                dtpFechaP3.Value = Convert.ToDateTime(dtActividad.Rows[0]["FProyectada3"]);
            }
        }


        private void txtResponsable_Enter(object sender, EventArgs e) { txtResponsable.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(1, txtResponsable.Text), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 316;
            lstPersonal.Columns[2].Width = 0;
            lstPersonal.Columns[3].Width = 0;
            lstPersonal.Columns[4].Width = 0;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idPersonal = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtResponsable.Text == "OPERACION" || txtResponsable.Text == "OPERACIONES" || txtResponsable.Text == "Operaciones" ||
                    txtResponsable.Text == "SUPERVISORES" || txtResponsable.Text == "Supervisores") { idPersonal = 1; }
                if (txtResponsable.Text == "TODOS" || txtResponsable.Text == "Todos" || txtResponsable.Text == "todos") { idPersonal = 0; }

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                txtDescripcion.Focus();
            }
        }

        private void txtResponsable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void txtResponsable_Leave(object sender, EventArgs e) { txtResponsable.BackColor = Color.White; }

        private void lstPersonal_Enter(object sender, EventArgs e)
        {
            if (!lstPersonal.Items.Count.Equals(0)) { lstPersonal.Items[0].Selected = true; }
        }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal.SelectedItems[0];

                idPersonal = Int32.Parse(ItemActual.Text);
                txtResponsable.Text = ItemActual.SubItems[1].Text;

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                txtDescripcion.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idPersonal = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void lstPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal.SelectedItems[0];

            idPersonal = Int32.Parse(ItemActual.Text);
            txtResponsable.Text = ItemActual.SubItems[1].Text;

            lstPersonal.Visible = false;
            lstPersonal.SendToBack();
            txtDescripcion.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                idPersonal = -1;
                txtResponsable.Clear();
                txtDescripcion.Clear();
                cbxNivel.Text = "IMPORTANTE";
                cbxEstado.Text = "PENDIENTE";
                txtSeguimiento.Clear();
                dtpFechaInicio.Value = DateTime.Now;
                dtpFechaP1.Value = DateTime.Now;
                dtpFechaP2.Value = DateTime.Now;
                dtpFechaP3.Value = DateTime.Now;
            }

            if (Opcion == 2)
            {
                cbxEstado.Text = "PENDIENTE";
                txtSeguimiento.Clear();
            }

            if (Opcion == 3 && Contador == 0) { dtpFechaP2.Value = DateTime.Now; }

            if (Opcion == 3 && Contador == 1) { dtpFechaP3.Value = DateTime.Now; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                if (txtResponsable.Text.Length == 0 || txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txtResponsable.Text.Length == 0) { txtResponsable.Focus(); }
                    else { txtDescripcion.Focus(); }
                    return;
                }
                else
                {
                    DataTable dtAgregar = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtAgregar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_PendientesDiarios_InsertarActividad(idPersonal, txtDescripcion.Text, cbxNivel.Text,
                        dtpFechaInicio.Value, dtpFechaP1.Value, txtSeguimiento.Text, Usuario);
                    respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formulario.ListarPendientesDiarios();
                        this.Close();
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 2)
            {
                if (txtSeguimiento.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese el seguimiento.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSeguimiento.Focus();
                    return;
                }
                else
                {
                    DataTable dtModificar = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtModificar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_PendientesDiarios_ModificarActividades(2, idPendiente, idPersonal, cbxEstado.Text, txtSeguimiento.Text, Usuario);
                    respta = Convert.ToString(dtModificar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formulario.ListarPendientesDiarios();
                        this.Close();
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 3)
            {
                DataTable dtModificar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (Contador == 0)
                { dtModificar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_PendientesDiarios_ReprogramarActividades(idPendiente, dtpFechaP2.Value, Contador, txtSeguimiento.Text, Usuario); }

                if (Contador == 1)
                { dtModificar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_PendientesDiarios_ReprogramarActividades(idPendiente, dtpFechaP3.Value, Contador, txtSeguimiento.Text, Usuario); }

                respta = Convert.ToString(dtModificar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarPendientesDiarios();
                    this.Close();
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
