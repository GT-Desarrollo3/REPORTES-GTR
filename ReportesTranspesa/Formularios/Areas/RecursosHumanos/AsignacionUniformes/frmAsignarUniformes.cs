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
using Negocio;
using Comun;
using ReportesTranspesa.Sistema;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.AsignacionUniformes
{
    public partial class frmAsignarUniformes : Form
    {
        frmListaUniformes _formulario;

        public frmAsignarUniformes()
        {
            InitializeComponent();
            cbxUniforme.SelectedIndexChanged -= cbxUniforme_SelectedIndexChanged;
        }

        private void cbxUniforme_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUniforme(); }

        private void frmAsignarUniformes_Load(object sender, EventArgs e)
        {
            ListarEmpleados();
            CargarComboUniforme();
            ListarVidaUtil();
        }


        public void RecibirDatos(frmListaUniformes formulario) { _formulario = formulario; }

        public void ListarEmpleados()
        {
            DataTable dtEmpleados = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarEmpleados(txtPersonal.Text);
            dtgPersonalData.DataSource = dtEmpleados;
            if (dtEmpleados.Rows.Count > 0)
            {
                dgvExpressVista.Columns["ID"].Visible = false;
                dgvExpressVista.Columns["department"].Visible = false;
                dgvExpressVista.Columns["CodigoPuesto"].Visible = false;

                dgvExpressVista.BestFitColumns();
                dgvExpressVista.ExpandAllGroups();
            }
        }

        public void ListarPuestos()
        {
            DataTable dtPuesto = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarEmpleadosPuesto(txtPuesto.Text);
            dtgPersonalData.DataSource = dtPuesto;
            if (dtPuesto.Rows.Count > 0)
            {
                dgvExpressVista.Columns["ID"].Visible = false;
                dgvExpressVista.Columns["department"].Visible = false;
                dgvExpressVista.Columns["CodigoPuesto"].Visible = false;

                dgvExpressVista.BestFitColumns();
                dgvExpressVista.ExpandAllGroups();
            }
        }

        public void ListarVidaUtil()
        {
            string idArea = Convert.ToString(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "department"));
            int idCargo = Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "CodigoPuesto"));
            int idUniforme = Convert.ToInt32(cbxUniforme.SelectedValue);

            DataTable dtVidaUtil = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarVidaUtil(idArea, idCargo, idUniforme);
            if (dtVidaUtil.Rows.Count > 0) { txtVidaUtil.Text = dtVidaUtil.Rows[0]["VidaUtil"].ToString(); }
            else { txtVidaUtil.Text = "0"; }
        }

        private void CargarComboUniforme()
        {
            DataTable dtUniforme = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarUniformes(1);
            cbxUniforme.DataSource = dtUniforme;
            cbxUniforme.DisplayMember = "NombreUniforme";
            cbxUniforme.ValueMember = "idUniforme";
        }


        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e) { ListarEmpleados(); }

        private void txtPuesto_KeyPress(object sender, KeyPressEventArgs e) { ListarPuestos(); }

        private void txtTalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtVidaUtil.Focus(); }
        }

        private void txtVidaUtil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Enter) { txtCantidad.Focus(); }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtPersonal.Clear();
            txtPuesto.Clear();
            ListarEmpleados();
            cbxUniforme.SelectedValue = 1;
            txtTalla.Clear();
            txtVidaUtil.Text = "0";
            txtCantidad.Clear();
        }

        private void dtgPersonalData_Click(object sender, EventArgs e)
        {
            try { ListarVidaUtil(); }
            catch { }
        }

        private void cbxUniforme_DropDownClosed(object sender, EventArgs e)
        {
            try { ListarVidaUtil(); }
            catch { }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtTalla.Text.Length == 0 || txtVidaUtil.Text.Length == 0 || txtVidaUtil.Text == "0" || txtCantidad.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtCantidad.Text.Length == 0) { txtCantidad.Focus(); }
                else
                {
                    if (txtTalla.Text.Length == 0) { txtTalla.Focus(); }
                    else { txtVidaUtil.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int idPersonal = Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "ID"));
                string idArea = Convert.ToString(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "department"));
                int idCargo = Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "CodigoPuesto"));

                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_AsignarUniformes(Convert.ToInt32(cbxUniforme.SelectedValue), idPersonal, idArea, idCargo,
                                                                                                                Convert.ToInt32(txtVidaUtil.Text), txtTalla.Text, Convert.ToInt32(txtCantidad.Text), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _formulario.ListarUniformes();
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
