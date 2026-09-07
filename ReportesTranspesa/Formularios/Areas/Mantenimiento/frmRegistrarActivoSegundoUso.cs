using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.IO;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmRegistrarActivoSegundoUso : Form
    {
        DataTable dtRespuesta;
        public int tipoOperacion = 0;
        public string idActivo = string.Empty;

        public frmRegistrarActivoSegundoUso()
        {
            InitializeComponent();
        }

        private void frmRegistrarActivoSegundoUso_Load(object sender, EventArgs e)
        {
            try
            {
                if (tipoOperacion == Utilitario.TipoOperacion.Registrar) { txtNombreActivo.ReadOnly = false; }

                if (tipoOperacion == Utilitario.TipoOperacion.Editar) { txtNombreActivo.ReadOnly = true; }

                Listar();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void Listar()
        {
            DataTable dtSegundoUso = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarItemsInventarioSegundoUso("", cbxSucursal.Text, 1);
            dtgActivos.DataSource = dtSegundoUso;

            if (dtSegundoUso.Rows.Count > 0)
            {
                dgvActivosVista.Columns["idActivo"].Visible = false;
                dgvActivosVista.Columns["UnidadMedida"].Visible = false;
                dgvActivosVista.Columns["CodigoSpring"].Visible = false;
                dgvActivosVista.Columns["FechaRegistro"].Visible = false;
                dgvActivosVista.Columns["UsuarioRegistro"].Visible = false;

                dgvActivosVista.Columns["Cantidad"].Summary.Clear();
                dgvActivosVista.Columns["Cantidad"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Cantidad", "Total = {0}");

                dgvActivosVista.BestFitColumns();
            }
        }


        private void frmRegistrarActivoSegundoUso_FormClosed(object sender, FormClosedEventArgs e)
        { this.DialogResult = System.Windows.Forms.DialogResult.OK; }

        private void txtNombreActivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                clsVisuales.Instancia.LlenarLw(lstItems, clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_ItemsAlmacenListar(txtNombreActivo.Text), true, false, false);
                lstItems.Columns[0].Width = 0;
                lstItems.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lstItems.Columns[2].Width = 0;
                lstItems.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lstItems.BringToFront();
                lstItems.Visible = true;

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstItems.Visible = false;
                    lstItems.SendToBack();
                    txtCodigoSpring.Clear();
                    txtUnidadMedida.Clear();
                }
            }
        }

        private void txtNombreActivo_KeyUp(object sender, KeyEventArgs e)
        {
            if (tipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                if (e.KeyCode == Keys.Down) { lstItems.Focus(); }
            }
        }

        private void lstItems_Enter(object sender, EventArgs e)
        {
            if (!lstItems.Items.Count.Equals(0)) { lstItems.Items[0].Selected = true; }
        }

        private void lstItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstItems.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstItems.SelectedItems[0];

                txtCodigoSpring.Text = ItemActual.SubItems[0].Text;
                txtNombreActivo.Text = ItemActual.SubItems[1].Text;
                txtUnidadMedida.Text = ItemActual.SubItems[3].Text;

                lstItems.Visible = false;
                lstItems.SendToBack();
                txtCantidad.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItems.Visible = false;
                lstItems.SendToBack();
                txtNombreActivo.Focus();
                txtCodigoSpring.Clear();
                txtUnidadMedida.Clear();
            }
        }

        private void lstItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItems.SelectedItems[0];

            txtCodigoSpring.Text = ItemActual.SubItems[0].Text;
            txtNombreActivo.Text = ItemActual.SubItems[1].Text;
            txtUnidadMedida.Text = ItemActual.SubItems[3].Text;

            lstItems.Visible = false;
            lstItems.SendToBack();
            txtCantidad.Focus();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '.'))
            { e.Handled = true; }
        }

        private void dtgActivos_DoubleClick(object sender, EventArgs e)
        {
            if (tipoOperacion == Utilitario.TipoOperacion.Editar)
            {
                idActivo = dgvActivosVista.GetRowCellValue(dgvActivosVista.FocusedRowHandle, "idActivo").ToString();
                txtNombreActivo.Text = dgvActivosVista.GetRowCellValue(dgvActivosVista.FocusedRowHandle, "nombre").ToString().TrimEnd();
                txtCodigoSpring.Text = dgvActivosVista.GetRowCellValue(dgvActivosVista.FocusedRowHandle, "CodigoSpring").ToString().TrimEnd();
                txtUnidadMedida.Text = dgvActivosVista.GetRowCellValue(dgvActivosVista.FocusedRowHandle, "UnidadMedida").ToString().TrimEnd();
                txtCantidad.Text = dgvActivosVista.GetRowCellValue(dgvActivosVista.FocusedRowHandle, "Cantidad").ToString().TrimEnd();
            }
        }

        private void btnRegistrarA_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreActivo.Text.Length == 0)
                {
                    MessageBox.Show("No ha ingresado un nombre valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtCodigoSpring.Text.Length == 0)
                {
                    MessageBox.Show("No ha ingresado un codigo valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtCantidad.Text.Length == 0)
                {
                    MessageBox.Show("No ha ingresado una cantidad valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                float numericValue;
                bool isNumber = float.TryParse(txtCantidad.Text, out numericValue);

                if (isNumber == false)
                {
                    MessageBox.Show("No ha ingresado una cantidad valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (tipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    if (clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistrarActivosSegundoUso(txtNombreActivo.Text, txtCodigoSpring.Text, txtCantidad.Text, txtUnidadMedida.Text, cbxSucursal.Text))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Listar();
                    }
                    else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }

                if (tipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    if (clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Quitar_Editar_ActivoSegundoUSo(Utilitario.TipoOperacion.Editar, idActivo, txtNombreActivo.Text, txtUnidadMedida.Text, Convert.ToDecimal(txtCantidad.Text)))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Listar();
                    }
                    else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cbxSucursal_DropDownClosed(object sender, EventArgs e) { Listar(); }

        private void activarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string idActivo2 = dgvActivosVista.GetRowCellValue(dgvActivosVista.FocusedRowHandle, "idActivo").ToString();

                if (clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Quitar_Editar_ActivoSegundoUSo(Utilitario.TipoOperacion.Activar, idActivo2, "", "", 0.0M))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                }
                else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string idActivo2 = dgvActivosVista.GetRowCellValue(dgvActivosVista.FocusedRowHandle, "idActivo").ToString();

                if (clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Quitar_Editar_ActivoSegundoUSo(Utilitario.TipoOperacion.Anular, idActivo2, "", "", 0.0M))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar();
                }
                else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
