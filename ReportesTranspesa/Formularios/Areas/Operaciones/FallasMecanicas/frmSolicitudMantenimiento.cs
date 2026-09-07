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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmSolicitudMantenimiento : Form
    {
        public string esVALE, UnidadFalla, Estado;
        public int idTracto = -1, TipoProgramacion;
        public int idComponente, idSolicitud, idc, Opcion;   
        public int xClick = 0, yClick = 0;
        DataTable dtDetalle;
        DataTable dtSolicitud;
        frmListaFallasMecanicas _frmLista;

        public void frmListarSolicitudes(frmListaFallasMecanicas frmListaFallasMecanicas)
        { _frmLista = frmListaFallasMecanicas; }

        public frmSolicitudMantenimiento()
        {
            InitializeComponent();
            cbxComponente.SelectedIndexChanged -= cbxComponente_SelectedIndexChanged;
            cbxDetalle.SelectedIndexChanged -= cbxDetalle_SelectedIndexChanged;
            cbxPosicionLlanta.SelectedIndexChanged -= cbxPosicionLlanta_SelectedIndexChanged;
            cbxCisterna.SelectedIndexChanged -= cbxCisterna_SelectedIndexChanged;
        }

        private void cbxComponente_SelectedIndexChanged(object sender, EventArgs e) { CargarComboComponente(); }

        private void cbxDetalle_SelectedIndexChanged(object sender, EventArgs e) { CargarComboDetalle(); }

        private void cbxPosicionLlanta_SelectedIndexChanged(object sender, EventArgs e) { CargarComboPosicion(); }

        private void cbxCisterna_SelectedIndexChanged(object sender, EventArgs e) { CargarComboBase(3); }

        private void frmSolicitudMantenimiento_Load(object sender, EventArgs e)
        {
            idComponente = 1;
        }


        public void CargarComboComponente()
        {
            DataTable dtComponente = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(1, 0);
            cbxComponente.DataSource = dtComponente;
            cbxComponente.DisplayMember = "Descripcion";
            cbxComponente.ValueMember = "idComponente";
        }

        public void CargarComboDetalle()
        {
            DataTable dtDetalle = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(2, idComponente);
            cbxDetalle.DataSource = dtDetalle;
            cbxDetalle.DisplayMember = "Descripcion";
            cbxDetalle.ValueMember = "idComponenteDetalle";
        }

        public void CargarComboPosicion()
        {
            DataTable dtPosicion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(3, 0);
            cbxPosicionLlanta.DataSource = dtPosicion;
            cbxPosicionLlanta.DisplayMember = "Descripcion";
            cbxPosicionLlanta.ValueMember = "idPosicionLlanta";
        }

        public void CargarComboBase(int Opcion)
        {
            DataTable dtBase = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarBases(Opcion);

            if (Opcion == 3)
            {
                cbxCisterna.DataSource = dtBase;
                cbxCisterna.DisplayMember = "Descripcion";
                cbxCisterna.ValueMember = "idCisterna";
            }
        }

        private void InsertarDetalle()
        {
            if (txtObservacion.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtObservacion.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar(Convert.ToInt32(cbxComponente.SelectedValue), Convert.ToInt32(cbxDetalle.SelectedValue),
                                                                                                                           Convert.ToInt32(cbxPosicionLlanta.SelectedValue), txtObservacion.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    ListarDetalle(1,0,0);
                    txtObservacion.Clear();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxComponente.Focus();
                }
            }
        }

        public void ListarDetalle(int Opcion, int idSolicitud, int idSolicitudDetalle)
        {
            dtgvListaSolicitudes.DataSource = null;
            dtDetalle = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Listar(Opcion, idSolicitud, idSolicitudDetalle);
            dtgvListaSolicitudes.DataSource = dtDetalle;
            if (dtDetalle.Rows.Count > 0)
            {
                dtgvListaSolicitudesView.Columns["idSolicitudDetalle"].Visible = false;
                dtgvListaSolicitudesView.Columns["idSolicitud"].Visible = false;
                dtgvListaSolicitudesView.BestFitColumns();
            }
        }

        public void EliminarDetalle()
        {
            int idSolicitudDetalle = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitudDetalle"));
            int IDC2 = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitud"));
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Editar(1, idSolicitudDetalle, IDC2, "");
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                ListarSolicitud(idSolicitud);
                cbxComponente.Focus();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void EliminarDetalleVacio()
        {
            int idSolicitudDetalle = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitudDetalle"));
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Editar(1, idSolicitudDetalle, 0, "");
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                ListarDetalle(1, 0, 0);
                cbxComponente.Focus();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void FiltrarOT()
        {
            DataTable dtListaOT = new DataTable();
            dtListaOT = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_FiltrarOT(txtPlacaOT.Text);
            dtgListaOT.DataSource = dtListaOT;
            if (dtListaOT.Rows.Count > 0) { dgvListaOTVista.BestFitColumns(); }
        }

        private void AsignarOT()
        {
            try
            {
                string OT = Convert.ToString(dgvListaOTVista.GetRowCellValue(dgvListaOTVista.FocusedRowHandle, "Nro_OT"));
                int idSolicitudDetalle = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitudDetalle"));
                int IDC = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "IDC"));
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Editar(2, idSolicitudDetalle, IDC, OT);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    ListarSolicitud(idSolicitud);
                    dtpFechaProgrI.Value = DateTime.Now;
                    dtpHoraProgrI.Value = DateTime.Now;
                    dtpFechaEntrega.Value = DateTime.Now;
                    dtpHoraEntrega.Value = DateTime.Now;
                    pListaOT.Visible = false;
                    pListaOT.SendToBack();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("La OT seleccionada no es válida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        public void TerminarEstado()
        {
            int idSolicitudDetalle = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitudDetalle"));
            int IDC = Convert.ToInt32(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "IDC"));
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Editar(3, idSolicitudDetalle, IDC, "");
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0") { ListarSolicitud(idSolicitud); }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void GenerarSolicitud(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 || txtKilometraje.Text.Length == 0 || dtgvListaSolicitudesView.RowCount == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPlaca.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese la placa de la unidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPlaca.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese el kilometraje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtKilometraje.Focus();
                }
                
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Insertar(idTracto, TipoProgramacion, cbxTipoMtto.Text,
                              cbxTipoTrabajo.Text, Convert.ToDecimal(txtKilometraje.Text), Convert.ToInt32(cbxCisterna.SelectedValue), UnidadFalla, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _frmLista.btnBuscarS_Click(sender, e);
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void ProgramarSolicitud(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            string FechaProgramacion = dtpFechaProgrI.Text + " " + dtpHoraProgrI.Text;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Programar(1, idSolicitud, Convert.ToDateTime(FechaProgramacion), txtMotivo.Text, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _frmLista.btnBuscarS_Click(sender, e);
                this.Close();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void ReprogramarSolicitud(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            string FechaReprogramacion = dtpFechaReprog.Text + " " + dtpHoraReprog.Text;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Programar(2, idSolicitud, Convert.ToDateTime(FechaReprogramacion), txtMotivo.Text, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _frmLista.btnBuscarS_Click(sender, e);
                this.Close();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void TerminarMantenimiento(object sender, EventArgs e)
        {
            /* -- GERARDO - 19/04
            if (dtSolicitud.Rows.Count > 0)
            {
                for (int i = 0; i < dtSolicitud.Rows.Count; i++)
                {
                    if (dtSolicitud.Rows[i]["OT"].ToString() == "")
                    {
                        MessageBox.Show("Por favor, asigne la OT antes de terminar el mantenimiento.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            */

            if (txtSolucion.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSolucion.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                string FechaReprogramacion = dtpFechaEntrega.Text + " " + dtpHoraEntrega.Text;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Programar(3, idSolicitud, Convert.ToDateTime(FechaReprogramacion), txtSolucion.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _frmLista.btnBuscarS_Click(sender, e);
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        public void ListarSolicitud (int idSolicitud)
        {
            cbxCisterna.Visible = false;

            dtSolicitud = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarSolicitud(idSolicitud);
            dtgvListaSolicitudes.DataSource = dtSolicitud;
            if (dtSolicitud.Rows.Count > 0)
            {
                txtPlaca.Text = dtSolicitud.Rows[0]["PLACA"].ToString();
                txtOperacion.Text = dtSolicitud.Rows[0]["OPERACION"].ToString();
                cbxTipoMtto.Text = dtSolicitud.Rows[0]["TIPO_MTTO"].ToString();
                cbxTipoTrabajo.Text = dtSolicitud.Rows[0]["TIPO_TRABAJO"].ToString();
                txtTipo.Text = dtSolicitud.Rows[0]["TIPO"].ToString();
                txtKilometraje.Text = dtSolicitud.Rows[0]["Kilometraje"].ToString();
                txtBase.Text = dtSolicitud.Rows[0]["DescripcionBase"].ToString();
                txtCisterna.Text = dtSolicitud.Rows[0]["CISTERNA"].ToString();
                txtUbicacionTaller.Text = dtSolicitud.Rows[0]["UbicacionTaller"].ToString();
                if (dtSolicitud.Rows[0]["FALLA"].ToString() == "TRACTO") { rbTracto.Checked = true; }
                else
                {
                    if (dtSolicitud.Rows[0]["FALLA"].ToString() == "SEMIRREMOLQUE") { rbRemolque.Checked = true; }
                    else { rbAmbos.Checked = true; }
                }
                dtpFechaProgrI.Value = Convert.ToDateTime(dtSolicitud.Rows[0]["FechaProg"]);
                dtpHoraProgrI.Value = Convert.ToDateTime(dtSolicitud.Rows[0]["FechaProg"]);
                dtpFechaReprog.Value = Convert.ToDateTime(dtSolicitud.Rows[0]["FechaReprog"]);
                dtpHoraReprog.Value = Convert.ToDateTime(dtSolicitud.Rows[0]["FechaReprog"]);
                txtMotivo.Text = dtSolicitud.Rows[0]["Motivo"].ToString();
                dtpFechaEntrega.Value = Convert.ToDateTime(dtSolicitud.Rows[0]["FechaEntrega"]);
                dtpHoraEntrega.Value = Convert.ToDateTime(dtSolicitud.Rows[0]["FechaEntrega"]);
                txtSolucion.Text = dtSolicitud.Rows[0]["OB"].ToString();

                dtgvListaSolicitudesView.Columns["idSolicitud"].Visible = false;
                dtgvListaSolicitudesView.Columns["TIPO"].Visible = false;
                dtgvListaSolicitudesView.Columns["TIPO_MTTO"].Visible = false;
                dtgvListaSolicitudesView.Columns["UbicacionTaller"].Visible = false;
                dtgvListaSolicitudesView.Columns["PLACA"].Visible = false;
                dtgvListaSolicitudesView.Columns["FALLA"].Visible = false;
                dtgvListaSolicitudesView.Columns["Kilometraje"].Visible = false;
                dtgvListaSolicitudesView.Columns["DescripcionBase"].Visible = false;
                dtgvListaSolicitudesView.Columns["CISTERNA"].Visible = false;
                dtgvListaSolicitudesView.Columns["FechaProg"].Visible = false;
                dtgvListaSolicitudesView.Columns["FechaReprog"].Visible = false;
                dtgvListaSolicitudesView.Columns["Motivo"].Visible = false;
                dtgvListaSolicitudesView.Columns["FechaEntrega"].Visible = false;
                dtgvListaSolicitudesView.Columns["OB"].Visible = false;
                dtgvListaSolicitudesView.Columns["IDC"].Visible = false;
                dtgvListaSolicitudesView.Columns["OPERACION"].Visible = false;
                dtgvListaSolicitudesView.Columns["idSolicitudDetalle"].Visible = false;
                dtgvListaSolicitudesView.Columns["idComponente"].Visible = false;
                dtgvListaSolicitudesView.Columns["idComponenteDetalle"].Visible = false;
                dtgvListaSolicitudesView.Columns["idPosicionLlanta"].Visible = false;

                dtgvListaSolicitudesView.BestFitColumns();
                dtgvListaSolicitudesView.ExpandAllGroups();

                UnidadFalla = dtSolicitud.Rows[0]["FALLA"].ToString();
            }
        }


        private void txtPlaca_Enter(object sender, EventArgs e) { txtPlaca.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtPlaca.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 70;
            lstPlaca.Columns[2].Width = 70;
            lstPlaca.Columns[3].Width = 0;
            lstPlaca.Columns[4].Width = 100;
            lstPlaca.Columns[5].Width = 90;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtTipo.Clear();
                txtOperacion.Clear();
                txtKilometraje.Clear();
                TipoProgramacion = 0;
                idTracto = -1;
            }
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void txtPlaca_Leave(object sender, EventArgs e)
        { txtPlaca.BackColor = Color.White; }

        private void lstPlaca_Enter(object sender, EventArgs e)
        { if (!lstPlaca.Items.Count.Equals(0)) { lstPlaca.Items[0].Selected = true; } }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlaca.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlaca.SelectedItems[0];

                idTracto = Int32.Parse(ItemActual.Text);
                txtPlaca.Text = ItemActual.SubItems[1].Text;
                txtTipo.Text = ItemActual.SubItems[2].Text;
                TipoProgramacion = Convert.ToInt32(ItemActual.SubItems[3].Text);
                txtOperacion.Text = ItemActual.SubItems[4].Text;
                txtKilometraje.Text = ItemActual.SubItems[5].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtKilometraje.Focus();

                if (TipoProgramacion == 3)
                {
                    label4.Visible = true;
                    cbxCisterna.Visible = true;
                }
                else
                {
                    label4.Visible = false;
                    cbxCisterna.Visible = false;
                }
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtPlaca.Focus();
                txtTipo.Clear();
                txtOperacion.Clear();
                txtKilometraje.Clear();
                label4.Visible = false;
                cbxCisterna.Visible = false;
                TipoProgramacion = 0;
                idTracto = -1;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idTracto = Int32.Parse(ItemActual.Text);
            txtPlaca.Text = ItemActual.SubItems[1].Text;
            txtTipo.Text = ItemActual.SubItems[2].Text;
            TipoProgramacion = Convert.ToInt32(ItemActual.SubItems[3].Text);
            txtOperacion.Text = ItemActual.SubItems[4].Text;
            txtKilometraje.Text = ItemActual.SubItems[5].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtKilometraje.Focus();

            if (TipoProgramacion == 3)
            {
                label4.Visible = true;
                cbxCisterna.Visible = true;
            }
            else
            {
                label4.Visible = false;
                cbxCisterna.Visible = false;
            }
        }

        public void rbTracto_Click(object sender, EventArgs e)
        {
            if (rbTracto.Checked == true)
            {
                txtPlaca.Focus();
                rbTracto.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                rbRemolque.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbAmbos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                UnidadFalla = rbTracto.Text;
            }
        }

        private void rbRemolque_Click(object sender, EventArgs e)
        {
            if (rbRemolque.Checked == true)
            {
                txtPlaca.Focus();
                rbTracto.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbRemolque.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                rbAmbos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                UnidadFalla = rbRemolque.Text;
            }
        }

        private void rbAmbos_Click(object sender, EventArgs e)
        {
            if (rbAmbos.Checked == true)
            {
                rbTracto.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbRemolque.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                rbAmbos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                UnidadFalla = rbAmbos.Text;
            }
        }

        private void txtKilometraje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtPlaca.Clear();
            txtPlaca.Focus();
            txtTipo.Clear();
            txtOperacion.Clear();
            txtKilometraje.Clear();
            label4.Visible = false;
            cbxCisterna.Visible = false;
            TipoProgramacion = 0;
            idTracto = -1;
        }

        private void cbxComponente_DropDownClosed(object sender, EventArgs e)
        {
            idComponente = Convert.ToInt32(cbxComponente.SelectedValue);
            CargarComboDetalle();
            if(idComponente == 1)
            {
                label8.Visible = true;
                cbxPosicionLlanta.Visible = true;
                cbxPosicionLlanta.SelectedValue = 0;
            }
            else
            {
                label8.Visible = false;
                cbxPosicionLlanta.Visible = false;
                cbxPosicionLlanta.SelectedValue = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cbxComponente.SelectedValue = 1;
            cbxComponente_DropDownClosed(sender, e);
            txtObservacion.Clear();
        }

        private void btnAniadir_Click(object sender, EventArgs e)
        {
            if (Opcion == 0) { InsertarDetalle(); }

            if (Opcion == 1)
            {
                if (txtObservacion.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtObservacion.Focus();
                    return;
                }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar2(idSolicitud, Convert.ToInt32(cbxComponente.SelectedValue), Convert.ToInt32(cbxDetalle.SelectedValue),
                                                                                                                               Convert.ToInt32(cbxPosicionLlanta.SelectedValue), txtObservacion.Text);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarSolicitud(idSolicitud);
                        txtObservacion.Clear();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cbxComponente.Focus();
                    }
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string IDC2 = Convert.ToString(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "idSolicitud"));
                if(IDC2 == "") { EliminarDetalleVacio(); }
                else { EliminarDetalle(); }
            }
            catch { MessageBox.Show("El dato seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void asignarOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtPlacaOT.Text = txtPlaca.Text;

            if (txtPlacaOT.Text.Length == 0)
            { MessageBox.Show("Por favor, ingrese la placa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                btnBuscarOT_Click(sender, e);

                if (Estado == "RECEPCIONADO" || Estado == "PROGRAMADO" || Estado == "REPROGRAMADO") { btnNuevaOT.Enabled = true; }
                else { btnNuevaOT.Enabled = false; }

                pListaOT.Visible = true;
                pListaOT.BringToFront();
            }
        }

        private void terminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TerminarEstado();
                dtpFechaEntrega.Value = DateTime.Now;
                dtpHoraEntrega.Value = DateTime.Now;
            }
            catch { MessageBox.Show("El dato seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void btnCerrar2_Click(object sender, EventArgs e)
        {
            pListaOT.Visible = false;
            pListaOT.SendToBack();
        }

        private void btnBuscarOT_Click(object sender, EventArgs e) { FiltrarOT(); }

        private void btnNuevaOT_Click(object sender, EventArgs e)
        {
            frmGenerarOrdenTrabajo frmGenerarOrdenTrabajo = new frmGenerarOrdenTrabajo();
            frmGenerarOrdenTrabajo.lblPlaca.Text = txtPlaca.Text;
            frmGenerarOrdenTrabajo.lblTipoUnidad.Text = txtTipo.Text;
            frmGenerarOrdenTrabajo.lblProgramacion.Text = txtOperacion.Text;
            frmGenerarOrdenTrabajo.idSolicitud = idSolicitud;
            frmGenerarOrdenTrabajo.txtDescripcion.Text = Convert.ToString(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "OBSERVACION"));
            frmGenerarOrdenTrabajo.formulario = this;
            frmGenerarOrdenTrabajo.ShowDialog();
        }

        private void txtPlacaOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarOT_Click(sender, e); }
        }

        private void dtgListaOT_DoubleClick(object sender, EventArgs e)
        {
            try { AsignarOT(); }
            catch { MessageBox.Show("El dato seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgvListaSolicitudes_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cbxComponente.Text = Convert.ToString(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "COMPONENTE"));
                cbxComponente_DropDownClosed(sender, e);
                cbxDetalle.Text = Convert.ToString(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "DETALLE"));
                cbxPosicionLlanta.Text = Convert.ToString(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "POSICION_LLANTA"));
                txtObservacion.Text = Convert.ToString(dtgvListaSolicitudesView.GetRowCellValue(dtgvListaSolicitudesView.FocusedRowHandle, "OBSERVACION"));
            }
            catch
            { MessageBox.Show("El dato seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            GenerarSolicitud(sender, e);

            /*
            DataTable dtSolicitud = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado(txtPlaca.Text);
            DataTable dtSolicitud2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado(txtSemirremolque.Text);
            string EstadoP, UnidadFallaP, EstadoR, UnidadFallaR;
            if (dtSolicitud.Rows.Count > 0)
            {
                EstadoP = dtSolicitud.Rows[0]["ESTADO"].ToString();
                UnidadFallaP = dtSolicitud.Rows[0]["FALLA"].ToString();
            }
            else
            {
                EstadoP = "COMPLETADO";
                UnidadFallaP = "";
            }
            if (dtSolicitud2.Rows.Count > 0)
            {
                EstadoR = dtSolicitud2.Rows[0]["ESTADO"].ToString();
                UnidadFallaR = dtSolicitud2.Rows[0]["FALLA"].ToString();
            }
            else
            {
                EstadoR = "COMPLETADO";
                UnidadFallaR = "";
            }
            if (UnidadFallaP == "TRACTO" || UnidadFallaP == "AMBOS")
            {
                if (EstadoP != "COMPLETADO")
                {
                    MessageBox.Show("No puede seleccionar la unidad " + txtPlaca.Text + " porque está en mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lstPlaca.Visible = false;
                    txtPlaca.Focus();
                    Limpiar();
                    return;
                }
            }
            if (UnidadFallaR == "SEMIRREMOLQUE" || UnidadFallaP == "AMBOS")
            {
                if (EstadoR != "COMPLETADO")
                {
                    MessageBox.Show("No puede seleccionar la unidad " + txtSemirremolque.Text + " porque está en mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpiar();
                    lstPlaca.Visible = false;
                    txtPlaca.Focus();
                    return;
                }
            }
            */
        }

        private void btnProgramar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text.Length == 0)
            {
                if (groupBox1.Enabled == true) { ProgramarSolicitud(sender, e); }
                else
                {
                    MessageBox.Show("Por favor, ingrese el motivo de la reprogramación", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMotivo.Focus();
                    return;
                }
            }
            else { ReprogramarSolicitud(sender, e); }
        }

        private void btnTerminar_Click(object sender, EventArgs e) { TerminarMantenimiento(sender, e); }

        private void pListaOT_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pListaOT.Left = pListaOT.Left + (e.X - xClick);
                pListaOT.Top = pListaOT.Top + (e.Y - yClick);
            }
        }
    }
}
