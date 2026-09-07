using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using Negocio;
using Comun;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmAsignarActivoSegundoUso : Form
    {
        public frmActivosDeSegundoUso frmActivosDeSegundoUso = new frmActivosDeSegundoUso();
        string xmlItemsAlmacen = "";
        bool TrueSegundoUso = false;
        bool TrueItemsAlmacen = false;
        bool TrueEmpleado = false;
        
        public frmAsignarActivoSegundoUso()
        {
            InitializeComponent();
        }

        private void frmAsignarActivoSegundoUso_Load(object sender, EventArgs e)
        {
            CargarActivoDeSegundoUso();
            CargarEmpleado();

            dgvSegundoUso.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvSegundoUso.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvSegundoUso.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvEmpleado.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvEmpleado.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvEmpleado.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvDetalleOT.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDetalleOT.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDetalleOT.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvDetalleReq.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDetalleReq.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDetalleReq.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }


        public void CargarActivoDeSegundoUso()
        {
            dgvSegundoUso.Rows.Clear();
            DataTable dtSegundoUso = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarItemsInventarioSegundoUso(txtBuscarSegundoUso.Text, cbxSucursal.Text, 0);

            if (dtSegundoUso.Rows.Count > 0)
            {
                for (int i = 0; i < dtSegundoUso.Rows.Count; i++)
                {
                    dgvSegundoUso.Rows.Add(0, dtSegundoUso.Rows[i]["idActivo"].ToString().Trim(), dtSegundoUso.Rows[i]["nombre"].ToString().Trim(), dtSegundoUso.Rows[i]["UnidadMedida"].ToString().Trim(),
                    dtSegundoUso.Rows[i]["CodigoSpring"].ToString().Trim(), dtSegundoUso.Rows[i]["Cantidad"].ToString().Trim(), dtSegundoUso.Rows[i]["Estado"].ToString(), dtSegundoUso.Rows[i]["FechaRegistro"].ToString(),
                    dtSegundoUso.Rows[i]["UsuarioRegistro"].ToString());
                }
            }
            else { dgvSegundoUso.Rows.Clear(); }

            dgvSegundoUso.Columns["NombreActivo"].ReadOnly = false;
        }

        private void CargarEmpleado()
        {
            try
            {
                dgvEmpleado.Rows.Clear();
                DataTable dtEmpleado = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Listar_Empleados_SegundoUso(txtBuscarEmpleado.Text);
                if (dtEmpleado.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmpleado.Rows.Count; i++)
                    { dgvEmpleado.Rows.Add(0, dtEmpleado.Rows[i]["Empleado"], dtEmpleado.Rows[i]["Nombre"]); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void txtBuscarSegundoUso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarActivoDeSegundoUso(); }
        }

        public void cbxSucursal_DropDownClosed(object sender, EventArgs e) { CargarActivoDeSegundoUso(); }

        private void dgvSegundoUso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSegundoUso.Rows.Count > 0)
                {
                    int posicion = dgvSegundoUso.CurrentRow.Index;

                    for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                    {
                        if (dgvSegundoUso.Rows[i].Index != posicion) { dgvSegundoUso.Rows[i].Cells["CheckActivo"].Value = false; }
                    }

                    if (Convert.ToBoolean(dgvSegundoUso.CurrentRow.Cells["CheckActivo"].Value) == false)
                    { dgvSegundoUso.CurrentRow.Cells["CheckActivo"].Value = true; }
                    else { dgvSegundoUso.CurrentRow.Cells["CheckActivo"].Value = false; }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExcelSegundoUso_Click(object sender, EventArgs e)
        {
            try
            {
                dgvActivosSegundoUso.DataSource = Utilitario.Instancia.GetContentAsDataTable(dgvSegundoUso);
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Activos de Segundo Uso" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dgvActivosSegundoUso.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void txtBuscarEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarEmpleado(); }
        }

        private void dgvEmpleado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvEmpleado.Rows.Count > 0)
                {
                    int posicion = dgvEmpleado.CurrentRow.Index;

                    for (int i = 0; i < dgvEmpleado.Rows.Count; i++)
                    {
                        if (dgvEmpleado.Rows[i].Index != posicion) { dgvEmpleado.Rows[i].Cells["CheckEmpleado"].Value = false; }
                    }

                    if (Convert.ToBoolean(dgvEmpleado.CurrentRow.Cells["CheckEmpleado"].Value) == false)
                    { dgvEmpleado.CurrentRow.Cells["CheckEmpleado"].Value = true; }
                    else { dgvEmpleado.CurrentRow.Cells["CheckEmpleado"].Value = false; }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRegistrarActivoSegundoUso OPEN = new frmRegistrarActivoSegundoUso();
                OPEN.tipoOperacion = Utilitario.TipoOperacion.Editar;
                OPEN.txtNombreActivo.Text = dgvSegundoUso.CurrentRow.Cells["NombreActivo"].Value.ToString();
                OPEN.txtUnidadMedida.Text = dgvSegundoUso.CurrentRow.Cells["UniMedidaActivo"].Value.ToString();
                OPEN.txtCodigoSpring.Text = dgvSegundoUso.CurrentRow.Cells["CodigoActivo"].Value.ToString();
                OPEN.txtCantidad.Text = dgvSegundoUso.CurrentRow.Cells["CantidadActivo"].Value.ToString();
                OPEN.idActivo = dgvSegundoUso.CurrentRow.Cells["idActivo"].Value.ToString();
                OPEN.Show();

                if (OPEN.ShowDialog() == System.Windows.Forms.DialogResult.OK) { CargarActivoDeSegundoUso(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void chkConsumible_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConsumible.Checked)
            {
                txtOT.Enabled = true;
                txtOT.Clear();
                txtPlaca.Text = "CONSUMIBLE";
                txtPlaca.Enabled = false;
                lblplaca.Text = "Operación: ";
                txtVale.Clear();
                txtVale.Enabled = false;
                CheckMantenimiento.Checked = false;
            }
            else
            {
                if (CheckMantenimiento.Checked == false)
                {
                    txtOT.Enabled = true;
                    txtOT.Clear();
                    txtPlaca.Enabled = true;
                    lblplaca.Text = "Placa: ";
                    txtPlaca.Text = "";
                    txtVale.Clear();
                    txtVale.Enabled = true;
                }
            }
        }

        private void CheckMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMantenimiento.Checked)
            {
                txtOT.Enabled = false;
                txtOT.Clear();
                txtPlaca.Enabled = false;
                lblplaca.Text = "Operación: ";
                txtPlaca.Text = "MANTENIMIENTO";
                txtVale.Clear();
                txtVale.Enabled = true;
                chkConsumible.Checked = false;
            }
            else
            {
                if (chkConsumible.Checked == false)
                {
                    txtOT.Enabled = true;
                    txtOT.Clear();
                    txtPlaca.Enabled = true;
                    lblplaca.Text = "Placa: ";
                    txtPlaca.Text = "";
                    txtVale.Clear();
                    txtVale.Enabled = true;
                }
            }
        }

        private void txtOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            clsVisuales.Instancia.LlenarLw(lstOT, clsMantenimientoBL.Instancia.Reportesapp_Mantenimiento_ListarOrdenesTrabajo(txtOT.Text), true, false, false);
            lstOT.Columns[0].Width = 0;
            lstOT.Columns[1].Width = 100;
            lstOT.BringToFront();
            lstOT.Visible = true;

            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtPlaca.Clear();
                dgvDetalleOT.DataSource = null;
                lstOT.Visible = false;
                lstOT.SendToBack();
            }
        }

        private void txtOT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstOT.Focus(); }
        }

        private void lstOT_Enter(object sender, EventArgs e)
        {
            if (!lstOT.Items.Count.Equals(0)) { lstOT.Items[0].Selected = true; }
        }

        private void lstOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstOT.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstOT.SelectedItems[0];
                txtPlaca.Text = ItemActual.Text;
                txtOT.Text = ItemActual.SubItems[1].Text;

                lstOT.Visible = false;
                lstOT.SendToBack();
                txtVale.Focus();

                if (chkConsumible.Checked == true)
                { dgvDetalleOT.DataSource = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(txtOT.Text, dgvSegundoUso.CurrentRow.Cells["CodigoActivo"].Value.ToString()); }
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                txtPlaca.Clear();
                dgvDetalleOT.DataSource = null;
                lstOT.Visible = false;
                lstOT.SendToBack();
                txtOT.Focus();
            }
        }

        private void lstOT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstOT.SelectedItems[0];
            txtPlaca.Text = ItemActual.Text;
            txtOT.Text = ItemActual.SubItems[1].Text;

            lstOT.Visible = false;
            lstOT.SendToBack();
            txtVale.Focus();

            if (chkConsumible.Checked == true)
            { dgvDetalleOT.DataSource = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(txtOT.Text, dgvSegundoUso.CurrentRow.Cells["CodigoActivo"].Value.ToString()); }
        }

        private void txtVale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtCantidadUso.Focus(); }
        }

        private void txtCantidadUso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.')) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == (char)Keys.Enter) { btnGuardar.Focus(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvSegundoUso.Rows[i].Cells["CheckActivo"].Value) == true)
                    {
                        TrueSegundoUso = true;
                        break;
                    }
                }

                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dgvSegundoUso.Rows[i].Cells["CantidadActivo"].Value) == 0 && Convert.ToBoolean(dgvSegundoUso.Rows[i].Cells["Check"].Value) == true)
                    {
                        MessageBox.Show("El stock del activo no puede ser 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                for (int i = 0; i < dgvEmpleado.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEmpleado.Rows[i].Cells["CheckEmpleado"].Value) == true)
                    {
                        TrueEmpleado = true;
                        break;
                    }
                }

                if (TrueSegundoUso == false)
                {
                    MessageBox.Show("No ha seleccionado ningún activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (TrueEmpleado == false)
                {
                    MessageBox.Show("No ha seleccionado a ningún empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int idActivo = 0;
                int idEmpleado = 0;

                if (txtOT.Text.Length == 0 && CheckMantenimiento.Checked == false && chkConsumible.Checked == false)
                {
                    MessageBox.Show("La OT no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtPlaca.Text.Length == 0 && chkConsumible.Checked == false)
                {
                    MessageBox.Show("La placa no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (Convert.ToDecimal(dgvSegundoUso.CurrentRow.Cells["CantidadActivo"].Value) < Convert.ToDecimal(txtCantidadUso.Text))
                {
                    MessageBox.Show("La cantidad solicitada no puede ser mayor que la cantidad actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvSegundoUso.CurrentRow.Cells["CheckActivo"].Value) == true)
                    {
                        idActivo = Convert.ToInt32(dgvSegundoUso.CurrentRow.Cells["idActivo"].Value);
                        break;
                    }
                }

                if (idActivo == 0)
                {
                    MessageBox.Show("El activo no se ha seleccionado correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < dgvEmpleado.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEmpleado.CurrentRow.Cells["CheckEmpleado"].Value) == true)
                    {
                        idEmpleado = Convert.ToInt32(dgvEmpleado.CurrentRow.Cells["idEmpleado"].Value);
                        break;
                    }
                }

                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dgvSegundoUso.Rows[i].Cells["CantidadActivo"].Value) < Convert.ToDecimal(txtCantidadUso.Text) && Convert.ToBoolean(dgvSegundoUso.Rows[i].Cells["CheckActivo"].Value) == true)
                    {
                        MessageBox.Show("El stock del activo no puede ser menor al ingresado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (chkConsumible.Checked)
                {
                    if (dgvDetalleOT.Rows.Count == 0)
                    {
                        string activo = dgvSegundoUso.CurrentRow.Cells["CodigoActivo"].Value.ToString().Replace("-S", "").Replace("S", "").Trim();
                        MessageBox.Show("El Activo Seleccionado " + activo + " no se encuentra en ningun Item de la OT Ingresada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (Convert.ToDecimal(dgvDetalleOT.Rows[0].Cells["Cantidad"].Value) < Convert.ToDecimal(txtCantidadUso.Text))
                    {
                        MessageBox.Show("La cantidad ingresada del Activo del segundo uso no puede ser mayor a la cantidad de la OT ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (dgvSegundoUso.CurrentRow.Cells["CodigoActivo"].Value.ToString().Replace("-S", "").Replace("S", "").Trim() != dgvDetalleOT.Rows[0].Cells["Recurso"].Value.ToString().Trim())
                    {
                        MessageBox.Show("El Activo Seleccionado no puede ser diferente al de la OT ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignarItemsAlmacen_ActivoSegundoUso_Empleado(xmlItemsAlmacen, idActivo, idEmpleado, txtOT.Text, txtVale.Text, Convert.ToDecimal(txtCantidadUso.Text), txtPlaca.Text, chkConsumible.Checked))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarActivoDeSegundoUso();
                    txtCantidadUso.Clear();
                    txtVale.Clear();
                    txtOT.Clear();
                    dgvDetalleOT.DataSource = null;
                    frmActivosDeSegundoUso.CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
                }
                else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtRequerimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            clsVisuales.Instancia.LlenarLw(lstRequerimiento, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimiento(txtRequerimiento.Text), true, false, false);
            lstRequerimiento.Columns[0].Width = 100;
            lstRequerimiento.Columns[1].Width = 0;
            lstRequerimiento.Columns[2].Width = 0;
            lstRequerimiento.Columns[3].Width = 0;
            lstRequerimiento.Columns[4].Width = 0;
            lstRequerimiento.BringToFront();
            lstRequerimiento.Visible = true;

            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                dtpFechaR.Value = DateTime.Now;
                txtDescripcionR.Clear();
                txtSolicitado.Clear();
                txtCentroCosto.Clear();
                dgvDetalleOT.DataSource = null;
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
            }
        }

        private void txtRequerimiento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstRequerimiento.Focus(); }
        }

        private void lstRequerimiento_Enter(object sender, EventArgs e)
        {
            if (!lstRequerimiento.Items.Count.Equals(0)) { lstRequerimiento.Items[0].Selected = true; }
        }

        private void lstRequerimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstRequerimiento.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstRequerimiento.SelectedItems[0];
                txtRequerimiento.Text = ItemActual.Text;
                dtpFechaR.Value = Convert.ToDateTime(ItemActual.SubItems[1].Text);
                txtDescripcionR.Text = ItemActual.SubItems[2].Text;
                txtSolicitado.Text = ItemActual.SubItems[3].Text;
                txtCentroCosto.Text = ItemActual.SubItems[4].Text;

                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
                txtCantidadUsoR.Focus();

                dgvDetalleReq.DataSource = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientoDetalle(txtRequerimiento.Text, dgvSegundoUso.CurrentRow.Cells["CodigoActivo"].Value.ToString());
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                dtpFechaR.Value = DateTime.Now;
                txtDescripcionR.Clear();
                txtSolicitado.Clear();
                txtCentroCosto.Clear();
                dgvDetalleOT.DataSource = null;
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
            }
        }

        private void lstRequerimiento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstRequerimiento.SelectedItems[0];
            txtRequerimiento.Text = ItemActual.Text;
            dtpFechaR.Value = Convert.ToDateTime(ItemActual.SubItems[1].Text);
            txtDescripcionR.Text = ItemActual.SubItems[2].Text;
            txtSolicitado.Text = ItemActual.SubItems[3].Text;
            txtCentroCosto.Text = ItemActual.SubItems[4].Text;

            lstRequerimiento.Visible = false;
            lstRequerimiento.SendToBack();
            txtCantidadUsoR.Focus();

            dgvDetalleReq.DataSource = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientoDetalle(txtRequerimiento.Text, dgvSegundoUso.CurrentRow.Cells["CodigoActivo"].Value.ToString());
        }

        private void txtCantidadUsoR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Enter) { btnGuardarR.Focus(); }
        }

        private void btnGuardarR_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvSegundoUso.Rows[i].Cells["CheckActivo"].Value) == true)
                    {
                        TrueSegundoUso = true;
                        break;
                    }
                }

                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dgvSegundoUso.Rows[i].Cells["CantidadActivo"].Value) == 0 && Convert.ToBoolean(dgvSegundoUso.Rows[i].Cells["Check"].Value) == true)
                    {
                        MessageBox.Show("El stock del activo no puede ser 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                for (int i = 0; i < dgvEmpleado.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEmpleado.Rows[i].Cells["CheckEmpleado"].Value) == true)
                    {
                        TrueEmpleado = true;
                        break;
                    }
                }

                if (TrueSegundoUso == false)
                {
                    MessageBox.Show("No ha seleccionado ningún activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (TrueEmpleado == false)
                {
                    MessageBox.Show("No ha seleccionado a ningún empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int idActivo = 0;
                int idEmpleado = 0;

                if (Convert.ToDecimal(dgvSegundoUso.CurrentRow.Cells["CantidadActivo"].Value) < Convert.ToDecimal(txtCantidadUsoR.Text))
                {
                    MessageBox.Show("La cantidad solicitada no puede ser mayor que la cantidad actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvSegundoUso.CurrentRow.Cells["CheckActivo"].Value) == true)
                    {
                        idActivo = Convert.ToInt32(dgvSegundoUso.CurrentRow.Cells["idActivo"].Value);
                        break;
                    }
                }                

                for (int i = 0; i < dgvEmpleado.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEmpleado.CurrentRow.Cells["CheckEmpleado"].Value) == true)
                    {
                        idEmpleado = Convert.ToInt32(dgvEmpleado.CurrentRow.Cells["idEmpleado"].Value);
                        break;
                    }
                }

                for (int i = 0; i < dgvSegundoUso.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dgvSegundoUso.Rows[i].Cells["CantidadActivo"].Value) < Convert.ToDecimal(txtCantidadUsoR.Text) && Convert.ToBoolean(dgvSegundoUso.Rows[i].Cells["CheckActivo"].Value) == true)
                    {
                        MessageBox.Show("El stock del activo no puede ser menor al ingresado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (idActivo == 0)
                {
                    MessageBox.Show("El activo no se ha seleccionado correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DataTable dtSegundoUsoR = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtSegundoUsoR = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_RegistrarRequerimientos(xmlItemsAlmacen, idActivo, idEmpleado,
                                    txtRequerimiento.Text, Convert.ToDecimal(txtCantidadUsoR.Text), Usuario);
                    respta = Convert.ToString(dtSegundoUsoR.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarActivoDeSegundoUso();
                        txtRequerimiento.Clear();
                        dtpFechaR.Value = DateTime.Now;
                        txtDescripcionR.Clear();
                        txtSolicitado.Clear();
                        txtCentroCosto.Clear();
                        txtCantidadUso.Clear();
                        dgvDetalleReq.DataSource = null;
                        frmActivosDeSegundoUso.ListarSegundoUsoRequerimientos();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
