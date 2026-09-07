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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas;
using ReportesTranspesa.Formularios.Areas.Seguridad.RegistroAccidentes;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias
{
    public partial class frmGenerarPresupuesto : Form
    {
        public int _idIncidencia, Elimino, idFalla, Opcion;
        public string Material;
        public frmListaIncidencias formulario;
        public frmListaFallasMecanicas formulario2;
        public frmListaIncidentesSSOMAC formulario3;
        public DataTable dtListaPresupuestos = new DataTable();
    
        public frmGenerarPresupuesto()
        {
            InitializeComponent();
            cbxUND.SelectedIndexChanged -= cbxUND_SelectedIndexChanged;
        }

        private void cbxUND_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidades(); }

        private void frmGenerarPresupuesto_Load(object sender, EventArgs e)
        {
            CargarComboUnidades();
            rbInsumos.Checked = true;
            rbInsumos_Click(sender, e);
            txtCantidad.Text = "1";
            if (Opcion == 0 || Opcion == 1) { ListarPresupuestos(); }
            else { ListarPresupuestoSSOMAC(); }
        }


        public void CargarComboUnidades()
        {
            DataTable dtUnidad = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(4, _idIncidencia);
            cbxUND.DataSource = dtUnidad;
            cbxUND.DisplayMember = "CODIGO";
            cbxUND.ValueMember = "UNIDAD";
        }

        public void FiltrarIncidencia(int idIncidencia)
        {
            _idIncidencia = idIncidencia;

            DataTable dtListaIncidencias = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(1, idIncidencia);
            if (dtListaIncidencias.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaIncidencias.Rows.Count; i++)
                {
                    txtPlaca.Text = dtListaIncidencias.Rows[0]["PLACA"].ToString();
                    txtTipoUnidad.Text = dtListaIncidencias.Rows[0]["TIPO_UNIDAD"].ToString();
                    txtOperacion.Text = dtListaIncidencias.Rows[0]["OPERACION"].ToString();
                    dtpFecha.Value = Convert.ToDateTime(dtListaIncidencias.Rows[0]["FECHA_INCIDENTE"]);
                    txtConductor.Text = dtListaIncidencias.Rows[0]["CONDUCTOR"].ToString();
                    txtSupervisor.Text = dtListaIncidencias.Rows[0]["SUPERVISOR"].ToString();
                    txtRutaLocal.Text = dtListaIncidencias.Rows[0]["INFORME_SEGURIDAD"].ToString();
                    txtSubtotal.Text = dtListaIncidencias.Rows[0]["SubTotal"].ToString();
                    txtIGV.Text = dtListaIncidencias.Rows[0]["IGV"].ToString();
                    txtMontoTotal.Text = dtListaIncidencias.Rows[0]["MontoTotal"].ToString();
                }
            }
        }

        public void ListarPresupuestos()
        {
            decimal IGV, MontoTotal;

            dtListaPresupuestos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(3, _idIncidencia);
            dtgListaPresupuesto.DataSource = dtListaPresupuestos;
            if (dtListaPresupuestos.Rows.Count > 0)
            {
                dgvListaPresupuestoView.Columns["idIncidenteC"].Visible = false;
                dgvListaPresupuestoView.BestFitColumns();

                dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].Summary.Clear();
                dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "IMPORTE_TOTAL", "{0:N2}");

                txtSubtotal.Text = Convert.ToString(dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].SummaryText);
                IGV = Math.Round((Convert.ToDecimal(txtSubtotal.Text) * 0.18M), 2);
                txtIGV.Text = Convert.ToString(IGV);
                MontoTotal = IGV + Convert.ToDecimal(txtSubtotal.Text);
                txtMontoTotal.Text = Convert.ToString(MontoTotal);
            }
            else
            {
                txtSubtotal.Text = "0.00";
                txtIGV.Text = "0.00";
                txtMontoTotal.Text = "0.00";
            }
        }

        public void ListarPresupuestoSSOMAC()
        {
            decimal IGV, MontoTotal;

            dtListaPresupuestos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(5, _idIncidencia);
            dtgListaPresupuesto.DataSource = dtListaPresupuestos;
            if (dtListaPresupuestos.Rows.Count > 0)
            {
                dgvListaPresupuestoView.Columns["idRegistroInc"].Visible = false;
                dgvListaPresupuestoView.BestFitColumns();

                dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].Summary.Clear();
                dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "IMPORTE_TOTAL", "{0:N2}");

                txtSubtotal.Text = Convert.ToString(dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].SummaryText);
                IGV = Math.Round((Convert.ToDecimal(txtSubtotal.Text) * 0.18M), 2);
                txtIGV.Text = Convert.ToString(IGV);
                MontoTotal = IGV + Convert.ToDecimal(txtSubtotal.Text);
                txtMontoTotal.Text = Convert.ToString(MontoTotal);
            }
            else
            {
                txtSubtotal.Text = "0.00";
                txtIGV.Text = "0.00";
                txtMontoTotal.Text = "0.00";
            }
        }

        public void Limpiar()
        {
            txtDescripcion.Clear();
            cbxUND.SelectedValue = "UNI";
            txtPrecioUnitario.Clear();
            txtCantidad.Text = "1";
            txtPrecioTotal.Clear();
        }


        private void txtRutaLocal_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(e.LinkText); }
            catch { MessageBox.Show("No se pudo abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                        txtRutaLocal2.Clear();
                        txtRutaLocal2.Text = "file:///" + nuevoEnlace;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e) { txtRutaLocal2.Clear(); }

        private void txtRutaLocal2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try { System.Diagnostics.Process.Start(txtRutaLocal2.Text); }
            catch { MessageBox.Show("No se pudo abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void rbInsumos_Click(object sender, EventArgs e)
        {
            Limpiar();
            Material = "INSUMO";
            cbxUND.Enabled = false;
            txtPrecioUnitario.ReadOnly = true;
        }

        private void rbManoObra_Click(object sender, EventArgs e)
        {
            Limpiar();
            Material = "MANO DE OBRA";
            cbxUND.Enabled = true;
            txtPrecioUnitario.ReadOnly = false;
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Material == "INSUMO")
            {
                clsVisuales.Instancia.LlenarLw(lstItemsAlmacen, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems(txtDescripcion.Text), true, false, false);
                lstItemsAlmacen.Columns[0].Width = 0;
                lstItemsAlmacen.Columns[1].Width = 400;
                lstItemsAlmacen.Columns[2].Width = 0;
                lstItemsAlmacen.Columns[3].Width = 0;
                lstItemsAlmacen.Columns[4].Width = 0;
                lstItemsAlmacen.BringToFront();
                lstItemsAlmacen.Visible = true;

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstItemsAlmacen.Visible = false;
                    lstItemsAlmacen.SendToBack();
                    txtDescripcion.Focus();
                }
            }
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (Material == "INSUMO")
            { if (e.KeyCode == Keys.Down) { lstItemsAlmacen.Focus(); } }
        }

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

                txtDescripcion.Text = ItemActual.SubItems[1].Text;
                cbxUND.SelectedValue = ItemActual.SubItems[2].Text;
                txtPrecioUnitario.Text = ItemActual.SubItems[3].Text;
                txtPrecioTotal.Text = Convert.ToString(Convert.ToDecimal(txtPrecioUnitario.Text) * Convert.ToDecimal(txtCantidad.Text));

                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                txtCantidad.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                Limpiar();
                txtDescripcion.Focus();
            }
        }

        private void lstItemsAlmacen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItemsAlmacen.SelectedItems[0];

            txtDescripcion.Text = ItemActual.SubItems[1].Text;
            cbxUND.SelectedValue = ItemActual.SubItems[2].Text;
            txtPrecioUnitario.Text = ItemActual.SubItems[3].Text;
            txtPrecioTotal.Text = Convert.ToString(Convert.ToDecimal(txtPrecioUnitario.Text) * Convert.ToDecimal(txtCantidad.Text));

            lstItemsAlmacen.Visible = false;
            lstItemsAlmacen.SendToBack();
            txtCantidad.Focus();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    decimal Cantidad;
                    Cantidad = Convert.ToDecimal(txtCantidad.Text == "" ? 1 : Convert.ToDecimal(txtCantidad.Text));
                    txtCantidad.Text = Cantidad.ToString();
                    btnAniadir.Focus();

                    txtPrecioTotal.Text = Convert.ToString(Convert.ToDecimal(txtPrecioUnitario.Text) * Convert.ToDecimal(txtCantidad.Text));
                }
            }
            catch { MessageBox.Show("Error calculando el monto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtUND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtPrecioUnitario.Focus(); }
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    decimal Precio;
                    Precio = Convert.ToDecimal(txtPrecioUnitario.Text == "" ? 0 : Convert.ToDecimal(txtPrecioUnitario.Text));
                    txtPrecioUnitario.Text = Precio.ToString();
                    txtCantidad.Focus();

                    txtPrecioTotal.Text = Convert.ToString(Convert.ToDecimal(txtPrecioUnitario.Text) * Convert.ToDecimal(txtCantidad.Text));
                }
            }
            catch { MessageBox.Show("Error calculando el monto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancelar_Click(object sender, EventArgs e) { Limpiar(); }

        private void btnAniadir_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtPrecioUnitario.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtPrecioTotal.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtDescripcion.Text.Length == 0) { txtDescripcion.Focus(); }
                else
                {
                    if (txtPrecioUnitario.Text.Length == 0) { txtPrecioUnitario.Focus(); }
                    else { txtCantidad.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (Opcion == 0 || Opcion == 1)
                {
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_RegistrarPresupuesto(1, _idIncidencia, Material, txtDescripcion.Text, cbxUND.Text, Convert.ToDecimal(txtPrecioUnitario.Text),
                                                               Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(txtPrecioTotal.Text));
                }
                else
                {
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_RegistrarPresupuesto(2, _idIncidencia, Material, txtDescripcion.Text, cbxUND.Text, Convert.ToDecimal(txtPrecioUnitario.Text),
                                                               Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(txtPrecioTotal.Text));
                }
                
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    if (Opcion == 0 || Opcion == 1) { ListarPresupuestos(); }
                    else { ListarPresupuestoSSOMAC(); }
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescripcion.Focus();
                }
            }
        }

        private void dtgListaPresupuesto_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idIncidenteD = Convert.ToInt32(dgvListaPresupuestoView.GetRowCellValue(dgvListaPresupuestoView.FocusedRowHandle, "#"));

                if (idIncidenteD != 0 && Elimino == 1) { eliminarPresupuestoToolStripMenuItem.Enabled = true; }
                else { eliminarPresupuestoToolStripMenuItem.Enabled = false; }
            }
            catch { eliminarPresupuestoToolStripMenuItem.Enabled = false; }
        }

        private void eliminarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (Opcion == 0 || Opcion == 1)
                {
                    int idIncidenteD = Convert.ToInt32(dgvListaPresupuestoView.GetRowCellValue(dgvListaPresupuestoView.FocusedRowHandle, "#"));
                    int idIncidenteC = Convert.ToInt32(dgvListaPresupuestoView.GetRowCellValue(dgvListaPresupuestoView.FocusedRowHandle, "idIncidenteC"));
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_EliminarPresupuesto(1, idIncidenteC, idIncidenteD);
                }
                else
                {
                    int idIncidenteD = Convert.ToInt32(dgvListaPresupuestoView.GetRowCellValue(dgvListaPresupuestoView.FocusedRowHandle, "#"));
                    int idIncidenteC = Convert.ToInt32(dgvListaPresupuestoView.GetRowCellValue(dgvListaPresupuestoView.FocusedRowHandle, "idRegistroInc"));
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_EliminarPresupuesto(2, idIncidenteC, idIncidenteD);
                }
                
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    if (Opcion == 0 || Opcion == 1) { ListarPresupuestos(); }
                    else { ListarPresupuestoSSOMAC(); }
                    txtDescripcion.Focus();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("El comprobante seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Opcion == 0 || Opcion == 1)
            { dtListaPresupuestos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(3, _idIncidencia); }
            else
            { dtListaPresupuestos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(5, _idIncidencia); }
            
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            if (dtListaPresupuestos.Rows.Count == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescripcion.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (txtRutaLocal2.Text.Length == 0) { txtRutaLocal2.Text = " "; }

                if (Opcion == 0 || Opcion == 1)
                {
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarMonto(idFalla, Convert.ToDecimal(txtSubtotal.Text),
                                  Convert.ToDecimal(txtIGV.Text), Convert.ToDecimal(txtMontoTotal.Text), txtRutaLocal2.Text, Usuario);
                }
                else
                {
                    int idIncidenteC = Convert.ToInt32(dgvListaPresupuestoView.GetRowCellValue(dgvListaPresupuestoView.FocusedRowHandle, "idRegistroInc"));

                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ActualizarMontoSSOMAC(idIncidenteC, Convert.ToDecimal(txtSubtotal.Text),
                                  Convert.ToDecimal(txtIGV.Text), Convert.ToDecimal(txtMontoTotal.Text), txtRutaLocal2.Text, Usuario);
                }

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (Opcion == 1) { formulario.ListarIncidencias(); }
                    if (Opcion == 0) { formulario2.ListarFallasMecanicas(); }
                    if (Opcion == 2) { formulario3.ListarIncidencias(); }
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
