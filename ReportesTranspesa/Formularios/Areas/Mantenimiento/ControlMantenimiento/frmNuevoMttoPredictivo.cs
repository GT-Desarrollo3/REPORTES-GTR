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
    public partial class frmNuevoMttoPredictivo : Form
    {
        public int Opcion, Tecnica, Sistema, idVehiculo;
        public frmListaMantenimiento formulario;
        DataTable dtPermisos = new DataTable();
        
        public frmNuevoMttoPredictivo()
        {
            InitializeComponent();
            cbxTipoUnidad.SelectedIndexChanged -= cbxTipoUnidad_SelectedIndexChanged;
        }

        private void cbxTipoUnidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidad(); }

        private void frmNuevoMttoPredictivo_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaMantenimiento");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                else { btnAgregar.Enabled = false; }
            }
        }


        public void CargarComboUnidad()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(4, 1);
            cbxTipoUnidad.DataSource = dtTipo;
            cbxTipoUnidad.DisplayMember = "Descripcion";
            cbxTipoUnidad.ValueMember = "idTipoVehiculo";
        }


        public void cbxTipoUnidad_DropDownClosed(object sender, EventArgs e)
        {
            idVehiculo = 0;
            txtPlaca.Clear();
            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtPlaca.Focus();

            txtOperacion.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
        }

        private void txtPlaca_Enter(object sender, EventArgs e) { txtPlaca.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarTractos(txtPlaca.Text, Convert.ToInt32(cbxTipoUnidad.SelectedValue)), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 80;
            lstPlaca.Columns[2].Width = 0;
            lstPlaca.Columns[3].Width = 116;
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
                txtOperacion.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
                idVehiculo = 0;
            }
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void txtPlaca_Leave(object sender, EventArgs e) { txtPlaca.BackColor = Color.White; }

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
                txtOperacion.Text = ItemActual.SubItems[3].Text;
                txtMarca.Text = ItemActual.SubItems[4].Text;
                txtModelo.Text = ItemActual.SubItems[5].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtLubricante.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtOperacion.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
                idVehiculo = 0;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];
            idVehiculo = Int32.Parse(ItemActual.Text);
            txtPlaca.Text = ItemActual.SubItems[1].Text;
            txtOperacion.Text = ItemActual.SubItems[3].Text;
            txtMarca.Text = ItemActual.SubItems[4].Text;
            txtModelo.Text = ItemActual.SubItems[5].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtLubricante.Focus();
        }

        private void txtLubricante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFecha.Focus(); }
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxEstado.Focus(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { txtRecomendacion.Focus(); }

        private void txtRecomendacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar.Focus(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtRutaLocal.Clear();
                        txtRutaLocal.Text = "file:///" + nuevoEnlace;
                        btnAgregar.Focus();
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e) { txtRutaLocal.Clear(); }

        private void txtRutaLocal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try { System.Diagnostics.Process.Start(txtRutaLocal.Text); }
            catch { }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            idVehiculo = 0;
            CargarComboUnidad();
            cbxTipoUnidad_DropDownClosed(sender, e);

            txtLubricante.Clear();
            dtpFecha.Value = DateTime.Now;
            cbxEstado.Text = "NORMAL";
            txtRecomendacion.Clear();
            txtRutaLocal.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 || txtLubricante.Text.Length == 0)
            {
                if (txtPlaca.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese una placa.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPlaca.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese el tipo de lubricante.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtLubricante.Focus();
                }
                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_RegistrarModificar(txtPlaca.Text, Tecnica, Sistema, dtpFecha.Value,
                                                         txtLubricante.Text, txtRecomendacion.Text, cbxEstado.Text, txtRutaLocal.Text, Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarMttoPredictivo();
                    this.Close();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
