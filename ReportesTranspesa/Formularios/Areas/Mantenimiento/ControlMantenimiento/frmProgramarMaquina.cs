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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmProgramarMaquina : Form
    {
        public int idVehiculo = 0, idProgramacion = 0, Opcion;
        public int _idRegistroM = 0;
        public frmListaMantenimiento formulario;
        DataTable dtPermisos = new DataTable();   

        public frmProgramarMaquina()
        {
            InitializeComponent();
            cbxTipoMtto.SelectedIndexChanged -= cbxTipoMtto_SelectedIndexChanged;
            cbxTipoUnidad.SelectedIndexChanged -= cbxTipoUnidad_SelectedIndexChanged;
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxTipoMtto_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMtto(Convert.ToInt32(cbxOperacion.SelectedValue)); }

        private void cbxTipoUnidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidad(); }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }


        private void frmProgramarMaquina_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaMantenimiento");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                else { btnAgregar.Enabled = false; }
            }
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto(cbxAceite.Text);
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "idMantenimientoOP";
        }

        public void CargarComboMtto(int idMttoOP)
        {
            DataTable dtMtto = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarOperaciones(idMttoOP);
            cbxTipoMtto.DataSource = dtMtto;
            cbxTipoMtto.DisplayMember = "TipoMantenimiento";
            cbxTipoMtto.ValueMember = "Posicion";
        }

        public void CargarComboUnidad()
        {
            DataTable dtTipo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(1);
            cbxTipoUnidad.DataSource = dtTipo;
            cbxTipoUnidad.DisplayMember = "DescripcionLocal";
            cbxTipoUnidad.ValueMember = "TipoMaquinaGrupo";
        }

        public void FiltrarMantenimiento(int idRegistro, object sender, EventArgs e)
        {
            _idRegistroM = idRegistro;

            DataTable dtMtto = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_FiltrarMttos(2, idRegistro);
            if (dtMtto.Rows.Count > 0)
            {
                txtPlaca.Text = dtMtto.Rows[0]["PLACA"].ToString();
                cbxAceite.Text = dtMtto.Rows[0]["ACEITE"].ToString();
                cbxAceite_DropDownClosed(sender, e);
                txtMarca.Text = dtMtto.Rows[0]["MARCA"].ToString();
                txtModelo.Text = dtMtto.Rows[0]["MODELO"].ToString();
                txtFrecuencia.Text = dtMtto.Rows[0]["FREC/KM"].ToString();
                txtUbicacion.Text = dtMtto.Rows[0]["UBICACION"].ToString();
                txtUsuario.Text = dtMtto.Rows[0]["USUARIO"].ToString();
                dtpUltFecha.Value = Convert.ToDateTime(dtMtto.Rows[0]["FECHA_UM"]);
                txtUltimoKM.Text = dtMtto.Rows[0]["KM1"].ToString();
                txtUltMtto.Text = dtMtto.Rows[0]["TIPO_MTTO"].ToString();
                txtKilometraje.Text = dtMtto.Rows[0]["KM2"].ToString();
                cbxTipoMtto.Text = dtMtto.Rows[0]["TIPO_MTTO"].ToString();
                txtPS.Text = dtMtto.Rows[0]["PS"].ToString();
            }
        }


        public void cbxTipoUnidad_DropDownClosed(object sender, EventArgs e)
        {
            label5.Visible = true;
            cbxAceite.Visible = true;

            DataTable dtOperacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto("");
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "idMantenimientoOP";
           
            CargarComboOperacion();
            CargarComboMtto(Convert.ToInt32(cbxOperacion.SelectedValue));

            txtPlaca.Clear();
            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtPlaca.Focus();
            txtProgramacion.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtKilometraje.Clear();
            idVehiculo = 0;
            idProgramacion = 0;
        }

        public void cbxAceite_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbxTipoUnidad.SelectedValue) != 2)
            {
                CargarComboOperacion();
                CargarComboMtto(Convert.ToInt32(cbxOperacion.SelectedValue));
            }
        }

        public void cbxOperacion_DropDownClosed(object sender, EventArgs e) { CargarComboMtto(Convert.ToInt32(cbxOperacion.SelectedValue)); }

        private void txtPlaca_Enter(object sender, EventArgs e) { txtPlaca.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarMaquinas(1, txtPlaca.Text, Convert.ToString(cbxTipoUnidad.SelectedValue)), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 86;
            lstPlaca.Columns[2].Width = 0;
            lstPlaca.Columns[3].Width = 120;
            lstPlaca.Columns[4].Width = 0;
            lstPlaca.Columns[5].Width = 0;
            lstPlaca.Columns[6].Width = 0;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtProgramacion.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
                txtKilometraje.Clear();
                idVehiculo = 0;
                idProgramacion = 0;
            }
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        {
            txtPlaca.BackColor = Color.White;
            cbxAceite_DropDownClosed(sender, e);
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            if (!lstPlaca.Items.Count.Equals(0)) { lstPlaca.Items[0].Selected = true; }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlaca.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlaca.SelectedItems[0];
                idVehiculo = Int32.Parse(ItemActual.Text);
                txtPlaca.Text = ItemActual.SubItems[1].Text;
                idProgramacion = Convert.ToInt32(ItemActual.SubItems[2].Text);
                txtProgramacion.Text = ItemActual.SubItems[3].Text;
                txtMarca.Text = ItemActual.SubItems[4].Text;
                txtModelo.Text = ItemActual.SubItems[5].Text;
                txtKilometraje.Text = ItemActual.SubItems[6].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtFrecuencia.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtProgramacion.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
                txtKilometraje.Clear();
                idVehiculo = 0;
                idProgramacion = 0;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];
            idVehiculo = Int32.Parse(ItemActual.Text);
            txtPlaca.Text = ItemActual.SubItems[1].Text;
            idProgramacion = Convert.ToInt32(ItemActual.SubItems[2].Text);
            txtProgramacion.Text = ItemActual.SubItems[3].Text;
            txtMarca.Text = ItemActual.SubItems[4].Text;
            txtModelo.Text = ItemActual.SubItems[5].Text;
            txtKilometraje.Text = ItemActual.SubItems[6].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtFrecuencia.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtProgramacion.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
                txtKilometraje.Clear();
                txtFrecuencia.Clear();
                txtUsuario.Clear();
                txtUbicacion.Clear();

                idVehiculo = 0;
                idProgramacion = 0;
                cbxAceite_DropDownClosed(sender, e);
            }

            if (Opcion == 2)
            {
                dtpFechaMtto.Value = DateTime.Now;
                txtKilometraje.Clear();
                txtUsuario.Clear();
                txtUbicacion.Clear();
                cbxAceite_DropDownClosed(sender, e);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 || txtFrecuencia.Text.Length == 0 || txtKilometraje.Text.Length == 0 || txtUbicacion.Text.Length == 0 || txtUsuario.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPlaca.Text.Length == 0) { txtPlaca.Focus(); }
                else
                {
                    if (txtFrecuencia.Text.Length == 0) { txtFrecuencia.Focus(); }
                    else
                    {
                        if (txtKilometraje.Text.Length == 0) { txtKilometraje.Focus(); }
                        else
                        {
                            if (txtUbicacion.Text.Length == 0) { txtUbicacion.Focus(); }
                            else { txtUsuario.Focus(); }
                        }
                    }
                }
                return;
            }

            /*
            if (Opcion == 2 && Convert.ToDecimal(txtUltimoKM.Text) > Convert.ToDecimal(txtKilometraje.Text))
            {
                MessageBox.Show("No puede ingresar un kilometraje menor al más reciente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtKilometraje.Focus();
                return;
            }
            else
            {*/
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMaquina(Opcion, _idRegistroM, txtPlaca.Text, cbxAceite.Text,
                            Convert.ToInt32(txtFrecuencia.Text), txtUsuario.Text, txtUbicacion.Text, dtpFechaMtto.Value, Convert.ToDecimal(txtKilometraje.Text), cbxTipoMtto.Text,
                            Convert.ToInt32(cbxOperacion.SelectedValue), Convert.ToInt32(cbxTipoMtto.SelectedValue), Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Opcion == 2)
                    {
                        frmControlMttoMaquina frmControlMttoMaquina = new frmControlMttoMaquina();
                        frmControlMttoMaquina.CodigoMaquina = txtPlaca.Text;
                        frmControlMttoMaquina.lblPlaca.Text = txtPlaca.Text;
                        frmControlMttoMaquina.KMActual = Convert.ToDecimal(txtKilometraje.Text);
                        frmControlMttoMaquina.lblMarca.Text = txtMarca.Text;
                        frmControlMttoMaquina.lblModelo.Text = txtModelo.Text;
                        frmControlMttoMaquina.ShowDialog();
                    }

                    this.Close();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            /*}*/
        }

        private void txtKilometraje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void txtFrecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtUsuario.Focus(); }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtUbicacion.Focus(); }
        }

        private void txtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaMtto.Focus(); }
        }
    }
}
