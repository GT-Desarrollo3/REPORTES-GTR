using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas.ControlCotizacion
{
    public partial class frmNuevaCotizacion : Form
    {
        public string TipoImplemento;
        public int Opcion;
        public int CO = 0, P = 0, T = 0, CI = 0, O = 0;
        public int idCotizacionC, idRuta;
        public DataTable dtListaCotizacion = new DataTable();
        public frmControlCotizacion formulario;

        public frmNuevaCotizacion()
        {
            InitializeComponent();
            cbxImplementoSeg.SelectedIndexChanged -= cbxImplementoSeg_SelectedIndexChanged;
        }

        private void cbxImplementoSeg_SelectedIndexChanged(object sender, EventArgs e) { CargarComboImplementos(); }

        private void frmNuevaCotizacion_Load(object sender, EventArgs e) { }


        public void CargarComboImplementos()
        {
            DataTable dtImplementos = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_AgregarListarImplementos(1, TipoImplemento, " ");
            cbxImplementoSeg.DataSource = dtImplementos;
            cbxImplementoSeg.DisplayMember = "Descripcion";
            cbxImplementoSeg.ValueMember = "idImplemento";
        }

        public void ListarImplementos(int idCotizacionC)
        {
            dtListaCotizacion = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_DetalleListar(idCotizacionC);
            dtgImplementos.DataSource = dtListaCotizacion;
            if (dtListaCotizacion.Rows.Count > 0)
            {
                dgvImplementosVista.Columns["idCotizacionC"].Visible = false;
                dgvImplementosVista.Columns["idImplemento"].Visible = false;
                
                dgvImplementosVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvImplementosVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvImplementosVista.BestFitColumns();
            }
        }


        public void rbUnidad_Click(object sender, EventArgs e)
        {
            TipoImplemento = "UNIDAD";
            rbUnidad.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbConductor.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            CargarComboImplementos();
        }

        public void rbConductor_Click(object sender, EventArgs e)
        {
            TipoImplemento = "CONDUCTOR";
            rbUnidad.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbConductor.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            CargarComboImplementos();
        }

        private void btnAgregarImplemento_Click(object sender, EventArgs e)
        {
            btnAgregarImplemento.Visible = false;
            btnAgregarImplemento.SendToBack();
            btnGuardarImplemento.Visible = true;
            btnGuardarImplemento.BringToFront();
            btnCancelarImp.Visible = true;
            btnCancelarImp.BringToFront();

            cbxImplementoSeg.Visible = false;
            cbxImplementoSeg.SendToBack();

            txtImplementoSeg.Clear();
            txtImplementoSeg.Visible = true;
            txtImplementoSeg.BringToFront();
        }

        private void btnGuardarImplemento_Click(object sender, EventArgs e)
        {
            if (txtImplementoSeg.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingrese un implemento.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtImplementoSeg.Focus();
                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea ingresar este nuevo implemento?", "AGREGAR NUEVO IMPLEMENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_AgregarListarImplementos(2, TipoImplemento, txtImplementoSeg.Text);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancelarImp_Click(sender, e);
                        CargarComboImplementos();
                    }
                }
            }
        }

        private void btnCancelarImp_Click(object sender, EventArgs e)
        {
            btnAgregarImplemento.Visible = true;
            btnAgregarImplemento.BringToFront();
            btnGuardarImplemento.Visible = false;
            btnGuardarImplemento.SendToBack();
            btnCancelarImp.Visible = false;
            btnCancelarImp.SendToBack();

            cbxImplementoSeg.Visible = true;
            cbxImplementoSeg.BringToFront();

            txtImplementoSeg.Clear();
            txtImplementoSeg.Visible = false;
            txtImplementoSeg.SendToBack();
        }

        public void cbCortinera_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCortinera.Checked == true) { CO = 1; }
            if (cbCortinera.Checked == false) { CO = 0; }
        }

        public void cbPlataforma_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPlataforma.Checked == true) { P = 1; }
            if (cbPlataforma.Checked == false) { P = 0; }
        }

        public void cbTolva_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTolva.Checked == true) { T = 1; }
            if (cbTolva.Checked == false) { T = 0; }
        }

        public void cbCisterna_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCisterna.Checked == true) { CI = 1; }
            if (cbCisterna.Checked == false) { CI = 0; }
        }

        public void cbOtros_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOtros.Checked == true) { O = 1; }
            if (cbOtros.Checked == false) { O = 0; }
        }

        private void cbxTipoViaje_DropDownClosed(object sender, EventArgs e) { dtpHorasDuracion.Focus(); }

        private void txtRuta_Enter(object sender, EventArgs e) { txtRuta.BackColor = Color.FromArgb(255, 224, 192); }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lvRuta, clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_AgregarListarImplementos(3," ",txtRuta.Text), true, false, false);
            lvRuta.Columns[0].Width = 0;
            lvRuta.Columns[1].Width = 300;
            lvRuta.Columns[2].Width = 50;
            lvRuta.Columns[3].Width = 50;
            lvRuta.BringToFront();
            lvRuta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                lvRuta.SendToBack();
                idRuta = -1;
                txtKilometraje.Clear();
            }
        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lvRuta.Focus(); }
        }

        private void txtRuta_Leave(object sender, EventArgs e) { txtRuta.BackColor = Color.White; }

        private void lvRuta_Enter(object sender, EventArgs e)
        {
            if (!lvRuta.Items.Count.Equals(0)) { lvRuta.Items[0].Selected = true; }
        }

        private void lvRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lvRuta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvRuta.SelectedItems[0];
                idRuta = Int32.Parse(ItemActual.Text);
                txtRuta.Text = ItemActual.SubItems[1].Text;
                txtKilometraje.Text = ItemActual.SubItems[2].Text;

                lvRuta.Visible = false;
                lvRuta.SendToBack();
                txtPuntoInicio.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                lvRuta.SendToBack();
                idRuta = -1;
                txtRuta.Clear();
                txtKilometraje.Clear();
            }
        }

        private void lvRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lvRuta.SelectedItems[0];
            idRuta = Int32.Parse(ItemActual.Text);
            txtRuta.Text = ItemActual.SubItems[1].Text;
            txtKilometraje.Text = ItemActual.SubItems[2].Text;

            lvRuta.Visible = false;
            lvRuta.SendToBack();
            txtPuntoInicio.Focus();
        }

        private void txtPuntoInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtPuntoFin.Focus(); }
        }

        private void txtPuntoFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtRUC.Focus(); }
        }

        private void txtFrecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtTelefono.Focus(); }
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtValor.Focus(); }
        }

        private void txtRUC_Enter(object sender, EventArgs e) { txtRUC.BackColor = Color.FromArgb(255, 224, 192); }

        private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            clsVisuales.Instancia.LlenarLw(lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC(txtRUC.Text), true, false, false);
            lvRUC.Columns[0].Width = 0;
            lvRUC.Columns[1].Width = -1;
            lvRUC.Columns[2].Width = 300;
            lvRUC.BringToFront();
            lvRUC.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRUC.Visible = false;
                lvRUC.SendToBack();
                txtCliente.Clear();
            }
        }

        private void txtRUC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lvRUC.Focus(); }
        }

        private void txtRUC_Leave(object sender, EventArgs e) { txtRUC.BackColor = Color.White; }

        private void txtCliente_Enter(object sender, EventArgs e) { txtCliente.BackColor = Color.FromArgb(255, 224, 192); }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lvRUC, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC(txtCliente.Text), true, false, false);
            lvRUC.Columns[0].Width = 0;
            lvRUC.Columns[1].Width = -1;
            lvRUC.Columns[2].Width = 300;
            lvRUC.BringToFront();
            lvRUC.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRUC.Visible = false;
                lvRUC.SendToBack();
                txtRUC.Clear();
            }
        }

        private void txtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lvRUC.Focus(); }
        }

        private void txtCliente_Leave(object sender, EventArgs e) { txtCliente.BackColor = Color.White; }

        private void lvRUC_Enter(object sender, EventArgs e)
        {
            if (!lvRUC.Items.Count.Equals(0)) { lvRUC.Items[0].Selected = true; }
        }

        private void lvRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lvRUC.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvRUC.SelectedItems[0];
                txtRUC.Text = ItemActual.SubItems[1].Text;
                txtCliente.Text = ItemActual.SubItems[2].Text;

                lvRUC.Visible = false;
                lvRUC.SendToBack();
                txtFrecuencia.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRUC.Visible = false;
                lvRUC.SendToBack();
                txtCliente.Clear();
                txtRUC.Clear();
            }
        }

        private void lvRUC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lvRUC.SelectedItems[0];
            txtRUC.Text = ItemActual.SubItems[1].Text;
            txtCliente.Text = ItemActual.SubItems[2].Text;

            lvRUC.Visible = false;
            lvRUC.SendToBack();
            txtFrecuencia.Focus();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar.Focus(); }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtContacto.Focus(); }
        }

        private void txtContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtEmbalaje.Focus(); }
        }

        private void txtEmbalaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtProducto.Focus(); }
        }

        private void cbxResponsable_DropDownClosed(object sender, EventArgs e) { dtpHorasDuracion.Focus(); }

        private void dtpHorasDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpContratoIni.Focus(); }
        }

        private void dtpContratoIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpContratoFin.Focus(); }
        }

        private void dtpContratoFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtPermisos.Focus(); }
        }

        private void txtPermisos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtTonelaje.Focus(); }
        }

        private void txtTonelaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtGastosA.Focus(); }
        }

        private void txtGastosA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHorarioIni.Focus(); }
        }

        private void dtpHorarioIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHorarioFin.Focus(); }
        }

        private void dtpHorarioFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxFlota.Focus(); }
        }

        private void cbxFlota_DropDownClosed(object sender, EventArgs e) { txtMermas.Focus(); }

        private void txtMermas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxStandby.Focus(); }
        }

        private void cbxStandby_DropDownClosed(object sender, EventArgs e) { cbxPoliticas.Focus(); }

        private void cbxPoliticas_DropDownClosed(object sender, EventArgs e) { dtpFechaInicio.Focus(); }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtNumeroCond.Focus(); }
        }

        private void txtNumeroCond_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtOrigen.Focus(); }
        }

        private void txtOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar.Focus(); }
        }

        private void dtgImplementos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idCotizacionC = Convert.ToString(dgvImplementosVista.GetRowCellValue(dgvImplementosVista.FocusedRowHandle, "idCotizacionC"));

                if (idCotizacionC != "") { tsEliminar.Enabled = true; }
                else { tsEliminar.Enabled = false; }
            }
            catch { tsEliminar.Enabled = false; }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este implemento?", "ELIMINAR IMPLEMENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idCotizacionC = Convert.ToInt32(dgvImplementosVista.GetRowCellValue(dgvImplementosVista.FocusedRowHandle, "idCotizacionC"));
                    int idImplemento = Convert.ToInt32(dgvImplementosVista.GetRowCellValue(dgvImplementosVista.FocusedRowHandle, "idImplemento"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_DetalleInsertar(2, idCotizacionC, idImplemento, " ");
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarImplementos(idCotizacionC); }
                }
            }
            catch { MessageBox.Show("El archivo seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dtpHorasDuracion.Text = "00:00:00";
            dtpContratoIni.Value = new DateTime(dtpContratoIni.Value.Year, dtpContratoIni.Value.Month, 1);
            dtpContratoFin.Value = DateTime.Now.AddYears(1);
            dtpHorarioIni.Text = "08:00:00";
            dtpHorarioFin.Text = "23:00:00";
            dtpFechaInicio.Text = "00:00:00";

            cbCortinera.Checked = false;
            cbCortinera_CheckedChanged(sender, e);
            cbPlataforma.Checked = false;
            cbPlataforma_CheckedChanged(sender, e);
            cbTolva.Checked = false;
            cbTolva_CheckedChanged(sender, e);
            cbCisterna.Checked = false;
            cbCisterna_CheckedChanged(sender, e);
            cbOtros.Checked = false;
            cbOtros_CheckedChanged(sender, e);

            cbxResponsable.Text = "TRANSPESA";
            cbxFlota.Text = "SÍ";
            cbxStandby.Text = "SÍ";
            cbxPoliticas.Text = "SÍ";

            txtRuta.Clear();
            txtKilometraje.Clear();
            txtPuntoInicio.Clear();
            txtPuntoFin.Clear();
            txtFrecuencia.Clear();
            txtProducto.Clear();
            txtRUC.Clear();
            txtCliente.Clear();
            txtTelefono.Clear();
            txtContacto.Clear();
            txtValor.Clear();
            txtEmbalaje.Clear();
            txtPermisos.Clear();
            txtTonelaje.Clear();
            txtGastosA.Clear();
            txtMermas.Clear();
            txtNumeroCond.Clear();
            txtOrigen.Clear();
        }

        private void btnAniadir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_DetalleInsertar(1, idCotizacionC, Convert.ToInt32(cbxImplementoSeg.SelectedValue),Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarImplementos(idCotizacionC); }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxImplementoSeg.Focus();
                }
            }
            catch { MessageBox.Show("Error registrando el implemento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((Opcion != 2) && (idCotizacionC == 0) && (txtRUC.Text.Length == 0 || txtFrecuencia.Text.Length == 0 || txtValor.Text.Length == 0 || txtTonelaje.Text.Length == 0 ||
                txtMermas.Text.Length == 0 || txtNumeroCond.Text.Length == 0 || txtRuta.Text.Length == 0))
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtRUC.Text.Length == 0) { txtRUC.Focus(); }
                return;
            }

            if ((Opcion == 2) && (idCotizacionC != 0) && ((CO == 0 && P == 0 && T == 0 && CI == 0 && O == 0) || txtRuta.Text.Length == 0 || txtPuntoInicio.Text.Length == 0 || txtPuntoFin.Text.Length == 0 ||
                txtFrecuencia.Text.Length == 0 || txtProducto.Text.Length == 0 || txtRUC.Text.Length == 0 || txtValor.Text.Length == 0 || txtPermisos.Text.Length == 0 ||
                txtTonelaje.Text.Length == 0 || txtGastosA.Text.Length == 0 || txtMermas.Text.Length == 0 || txtNumeroCond.Text.Length == 0 || txtOrigen.Text.Length == 0))
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (idCotizacionC == 0)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion(1, 0, CO, P, T, CI, O, idRuta, cbxTipoViaje.Text, txtPuntoInicio.Text,
                                                          txtPuntoFin.Text, Convert.ToDecimal(txtFrecuencia.Text), txtProducto.Text, txtRUC.Text, Convert.ToDecimal(txtValor.Text),
                                                          txtTelefono.Text, txtContacto.Text, txtEmbalaje.Text, cbxResponsable.Text, dtpHorasDuracion.Value, dtpContratoIni.Value,
                                                          dtpContratoFin.Value, txtPermisos.Text, Convert.ToDecimal(txtTonelaje.Text), txtGastosA.Text, dtpHorarioIni.Value,
                                                          dtpHorarioFin.Value, cbxFlota.Text, Convert.ToDecimal(txtMermas.Text), cbxStandby.Text, cbxPoliticas.Text,
                                                          dtpFechaInicio.Value, Convert.ToInt32(txtNumeroCond.Text), txtOrigen.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formulario.ListarCotizaciones();
                        this.Close();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
                }
                else
                {
                    if (Opcion == 2)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion(2, idCotizacionC, CO, P, T, CI, O, idRuta, cbxTipoViaje.Text, txtPuntoInicio.Text,
                                                              txtPuntoFin.Text, Convert.ToDecimal(txtFrecuencia.Text), txtProducto.Text, txtRUC.Text, Convert.ToDecimal(txtValor.Text),
                                                              txtTelefono.Text, txtContacto.Text, txtEmbalaje.Text, cbxResponsable.Text, dtpHorasDuracion.Value, dtpContratoIni.Value,
                                                              dtpContratoFin.Value, txtPermisos.Text, Convert.ToDecimal(txtTonelaje.Text), txtGastosA.Text, dtpHorarioIni.Value,
                                                              dtpHorarioFin.Value, cbxFlota.Text, Convert.ToDecimal(txtMermas.Text), cbxStandby.Text, cbxPoliticas.Text,
                                                              dtpFechaInicio.Value, Convert.ToInt32(txtNumeroCond.Text), txtOrigen.Text, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formulario.ListarCotizaciones();
                            this.Close();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
                    }
                    else
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion(3, idCotizacionC, CO, P, T, CI, O, idRuta, cbxTipoViaje.Text, txtPuntoInicio.Text,
                                                              txtPuntoFin.Text, Convert.ToDecimal(txtFrecuencia.Text), txtProducto.Text, txtRUC.Text, Convert.ToDecimal(txtValor.Text),
                                                              txtTelefono.Text, txtContacto.Text, txtEmbalaje.Text, cbxResponsable.Text, dtpHorasDuracion.Value, dtpContratoIni.Value,
                                                              dtpContratoFin.Value, txtPermisos.Text, Convert.ToDecimal(txtTonelaje.Text), txtGastosA.Text, dtpHorarioIni.Value,
                                                              dtpHorarioFin.Value, cbxFlota.Text, Convert.ToDecimal(txtMermas.Text), cbxStandby.Text, cbxPoliticas.Text,
                                                              dtpFechaInicio.Value, Convert.ToInt32(txtNumeroCond.Text), txtOrigen.Text, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formulario.ListarCotizaciones();
                            this.Close();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
                    }
                }
            }
        }
    }
}
