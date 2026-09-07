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
    public partial class frmProgramarEquipo : Form
    {
        public int idRegistroE;
        public int Opcion;
        public frmListaMantenimiento formulario;
        DataTable dtPermisos = new DataTable();   

        public frmProgramarEquipo()
        {
            InitializeComponent();
            cbxTipoUnidad.SelectedIndexChanged -= cbxTipoUnidad_SelectedIndexChanged;
        }

        private void cbxTipoUnidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidad(); }

        private void frmProgramarEquipo_Load(object sender, EventArgs e)
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
            DataTable dtTipo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(1);
            cbxTipoUnidad.DataSource = dtTipo;
            cbxTipoUnidad.DisplayMember = "DescripcionLocal";
            cbxTipoUnidad.ValueMember = "TipoMaquinaGrupo";
        }


        private void cbxTipoUnidad_DropDownClosed(object sender, EventArgs e) { txtPlaca.Focus(); }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtPeriodo.Focus(); }
        }

        private void txtPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtEquipo.Focus(); }
        }

        private void txtEquipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDescripcion.Focus(); }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtUbicacion.Focus(); }
        }

        private void txtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtMarca.Focus(); }
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtModelo.Focus(); }
        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpUltFecha.Focus(); }
        }

        private void dtpUltFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { btnAgregar.Focus(); }
                else { btnActualizar.Focus(); }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 || txtEquipo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtPeriodo.Text.Length == 0 ||
                txtUbicacion.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtPlaca.Text.Length == 0) { txtPlaca.Focus(); }
                else
                {
                    if (txtEquipo.Text.Length == 0) { txtEquipo.Focus(); }
                    else
                    {
                        if (txtDescripcion.Text.Length == 0) { txtDescripcion.Focus(); }
                        else
                        {
                            if (txtPeriodo.Text.Length == 0) { txtPeriodo.Focus(); }
                            else { txtUbicacion.Focus(); }
                        }
                    }
                }

                return;
            }
            else
            {
                DataTable dtActualizar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                Opcion = 3;

                dtActualizar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarEquipos(Opcion, idRegistroE, txtPlaca.Text, Convert.ToInt32(txtPeriodo.Text), 
                                                            cbxTipoUnidad.Text, txtEquipo.Text, txtDescripcion.Text, txtUbicacion.Text, txtMarca.Text, txtModelo.Text, dtpUltFecha.Value, Usuario);
                respta = Convert.ToString(dtActualizar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);

                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.CargarComboEquipo();
                    formulario.ListarMantenimientoEquipos();
                    Opcion = 2;
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 || txtEquipo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtPeriodo.Text.Length == 0 ||
                txtUbicacion.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtPlaca.Text.Length == 0) { txtPlaca.Focus(); }
                else
                {
                    if (txtEquipo.Text.Length == 0) { txtEquipo.Focus(); }
                    else
                    {
                        if (txtDescripcion.Text.Length == 0) { txtDescripcion.Focus(); }
                        else
                        {
                            if (txtPeriodo.Text.Length == 0) { txtPeriodo.Focus(); }
                            else { txtUbicacion.Focus(); }
                        }
                    }
                }

                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarEquipos(Opcion, idRegistroE, txtPlaca.Text, Convert.ToInt32(txtPeriodo.Text),
                                                         cbxTipoUnidad.Text, txtEquipo.Text, txtDescripcion.Text, txtUbicacion.Text, txtMarca.Text, txtModelo.Text, dtpUltFecha.Value, Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);

                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.CargarComboEquipo();

                    if (Opcion == 2)
                    {
                        frmControlMttoEquipos frmControlMttoEquipos = new frmControlMttoEquipos();
                        frmControlMttoEquipos.idRegistroE = idRegistroE;
                        frmControlMttoEquipos.lblPlaca.Text = txtPlaca.Text;
                        frmControlMttoEquipos.lblMarca.Text = txtMarca.Text;
                        frmControlMttoEquipos.lblModelo.Text = txtModelo.Text;
                        frmControlMttoEquipos.ShowDialog();
                    }

                    this.Close();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                cbxTipoUnidad.Text = "UNIDAD ESTACIONARIA";
                txtPlaca.Clear();
                txtPeriodo.Clear();
                txtEquipo.Clear();
                txtDescripcion.Clear();
                txtUbicacion.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
            }

            dtpUltFecha.Value = DateTime.Now;
        }
    }
}
