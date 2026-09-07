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
    public partial class frmAsignarRecursos : Form
    {
        public int idProcesoMtto, idVehiculo, Opcion;
        public string Item, MaquinaCodigo;

        public frmAsignarRecursos()
        {
            InitializeComponent();
        }

        private void frmAsignarRecursos_Load(object sender, EventArgs e)
        {
            ListarRecursos();
            ListarRecursosMaquina();
        }


        public void ListarRecursos()
        {
            DataTable dtListaRecursos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorio(idProcesoMtto, idVehiculo);
            dtgRecursos.DataSource = dtListaRecursos;
            if (dtListaRecursos.Rows.Count > 0)
            {
                dgvRecursosVista.Columns["idRecursoMtto"].Visible = false;
                dgvRecursosVista.Columns["idProcesoMtto"].Visible = false;
                dgvRecursosVista.Columns["idVehiculo"].Visible = false;

                dgvRecursosVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvRecursosVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dgvRecursosVista.BestFitColumns();
            }
        }

        public void ListarRecursosMaquina()
        {
            DataTable dtListaRecursosMaquina = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorioMaquina(idProcesoMtto, MaquinaCodigo);
            dtgRecursosMaquina.DataSource = dtListaRecursosMaquina;
            if (dtListaRecursosMaquina.Rows.Count > 0)
            {
                dgvRecursosMaquinaVista.Columns["idRecursoMtto"].Visible = false;
                dgvRecursosMaquinaVista.Columns["idProcesoMtto"].Visible = false;
                dgvRecursosMaquinaVista.Columns["MaquinaCodigo"].Visible = false;

                dgvRecursosMaquinaVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvRecursosMaquinaVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dgvRecursosMaquinaVista.BestFitColumns();
            }
        }

        private void txtCodigoItem_Enter(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCodigoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstItemsAlmacen, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems(txtCodigoItem.Text), true, false, false);
            lstItemsAlmacen.Columns[0].Width = 80;
            lstItemsAlmacen.Columns[1].Width = 400;
            lstItemsAlmacen.BringToFront();
            lstItemsAlmacen.Visible = true;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                Item = "";
                txtCodigoItem.Focus();
            }
        }

        private void txtCodigoItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstItemsAlmacen.Focus(); }
        }

        private void txtCodigoItem_Leave(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.White; }

        private void lstItemsAlmacen_Enter(object sender, EventArgs e)
        {
            if (!lstItemsAlmacen.Items.Count.Equals(0)) { lstItemsAlmacen.Items[0].Selected = true; }
        }

        private void lstItemsAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstItemsAlmacen.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstItemsAlmacen.SelectedItems[0];

                Item = ItemActual.SubItems[0].Text;
                txtCodigoItem.Text = ItemActual.SubItems[0].Text;
                txtDescripcion.Text = ItemActual.SubItems[1].Text;

                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                txtCantidad.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                Item = "";
                txtCodigoItem.Focus();
            }
        }

        private void lstItemsAlmacen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItemsAlmacen.SelectedItems[0];

            Item = ItemActual.SubItems[0].Text;
            txtCodigoItem.Text = ItemActual.SubItems[0].Text;
            txtDescripcion.Text = ItemActual.SubItems[1].Text;

            lstItemsAlmacen.Visible = false;
            lstItemsAlmacen.SendToBack();
            txtCantidad.Focus();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar_Click(sender, e); }
        }

        private void dtgRecursos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Item = dgvRecursosVista.GetRowCellValue(dgvRecursosVista.FocusedRowHandle, "CÓDIGO").ToString();
                if (Item != "") { tsEliminarRecurso.Enabled = true; }
            }
            catch { tsEliminarRecurso.Enabled = false; }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Item = "";
            txtDescripcion.Clear();
            txtCodigoItem.Clear();
            txtCantidad.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtCantidad.Text == "0")
            {
                if (txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Por favor ingrese un ítem.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDescripcion.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor ingrese la cantidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCantidad.Focus();
                }
                return;
            }
            else
            {
                if (Opcion == 1)
                {
                    DataTable dtAgregar = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursos(1, 0, idProcesoMtto, idVehiculo, Item,
                                                             txtDescripcion.Text, Convert.ToDecimal(txtCantidad.Text), Usuario);
                    respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarRecursos();
                        btnCancelar_Click(sender, e);
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    DataTable dtAgregar2 = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtAgregar2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursosMaquina(1, 0, idProcesoMtto, MaquinaCodigo, Item,
                                                             txtDescripcion.Text, Convert.ToDecimal(txtCantidad.Text), Usuario);
                    respta = Convert.ToString(dtAgregar2.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarRecursosMaquina();
                        btnCancelar_Click(sender, e);
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void tsEliminarRecurso_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                if (MessageBox.Show("¿Desea eliminar el ítem seleccionado?", "ASIGNACIÓN DE RECURSOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idRecurso = Convert.ToInt32(dgvRecursosVista.GetRowCellValue(dgvRecursosVista.FocusedRowHandle, "idRecursoMtto"));
                    int idProceso = Convert.ToInt32(dgvRecursosVista.GetRowCellValue(dgvRecursosVista.FocusedRowHandle, "idProcesoMtto"));
                    int idVehiculo = Convert.ToInt32(dgvRecursosVista.GetRowCellValue(dgvRecursosVista.FocusedRowHandle, "idVehiculo"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursos(2, idRecurso, idProceso, idVehiculo, "", "", 0, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarRecursos(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            else
            {
                if (MessageBox.Show("¿Desea eliminar el ítem seleccionado?", "ASIGNACIÓN DE RECURSOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idRecurso = Convert.ToInt32(dgvRecursosMaquinaVista.GetRowCellValue(dgvRecursosMaquinaVista.FocusedRowHandle, "idRecursoMtto"));
                    int idProceso = Convert.ToInt32(dgvRecursosMaquinaVista.GetRowCellValue(dgvRecursosMaquinaVista.FocusedRowHandle, "idProcesoMtto"));
                    string MaquinaCodigo = Convert.ToString(dgvRecursosMaquinaVista.GetRowCellValue(dgvRecursosMaquinaVista.FocusedRowHandle, "MaquinaCodigo"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursosMaquina(2, idRecurso, idProceso, MaquinaCodigo, "", "", 0, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarRecursosMaquina(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
