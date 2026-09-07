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
using DevExpress.Data;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmEstadoUnidades : Form
    {
        public string EstadoProg;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int e1 = 0, e2 = 0, idSolicitud, Opcion;
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public frmListaFallasMecanicas _frmLista;

        public frmEstadoUnidades()
        {
            InitializeComponent();
            cbxBase.SelectedIndexChanged -= cbxBase_SelectedIndexChanged;
            cbxBase2.SelectedIndexChanged -= cbxBase2_SelectedIndexChanged;
        }

        private void cbxBase_SelectedIndexChanged(object sender, EventArgs e) { CargarComboBase(1); }

        private void cbxBase2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboBase2(2); }

        private void frmEstadoUnidades_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaFallasMecanicas");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Programar Solicitud")
                    {
                        tsProgramarSolicitud.Enabled = true;
                        tsCambiarUbicacion.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else
                    {
                        tsProgramarSolicitud.Enabled = false;
                        tsCambiarUbicacion.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Nueva Solicitud")
                    {
                        tsTerminarMantenimiento.Enabled = true;
                        i = 999; e2 = 1;
                    }
                    else { tsTerminarMantenimiento.Enabled = false; }
                }
            }
            else
            {
                tsProgramarSolicitud.Enabled = false;
                tsCambiarUbicacion.Enabled = false;
                tsTerminarMantenimiento.Enabled = false;
            }
            
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaProg.Value = DateTime.Now;
            cbxRepuesto.Text = "TODOS";

            rbTodos.Checked = true;
            rbTodos_Click(sender, e);
            CargarComboBase(1);
            ListarEstadoUnidades();
        }


        public void CargarComboBase(int Opcion)
        {
            DataTable dtBase = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarBases(Opcion);
            cbxBase.DataSource = dtBase;
            cbxBase.DisplayMember = "DescripcionBase";
            cbxBase.ValueMember = "idBase";
        }

        public void CargarComboBase2(int Opcion)
        {
            DataTable dtBase2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarBases(Opcion);

            if (Opcion == 1 || Opcion == 2)
            {
                cbxBase2.DataSource = dtBase2;
                cbxBase2.DisplayMember = "DescripcionBase";
                cbxBase2.ValueMember = "idBase";
            }
        }

        public void ListarEstadoUnidades()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                DataTable dtListaEstado = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarEstado(txtPlaca.Text, dtpFechaIni.Text, dtpFechaFin.Text, Convert.ToInt32(cbxBase.SelectedValue), cbxRepuesto.Text, EstadoProg);
                dtgEstadoMtto.DataSource = dtListaEstado;
                if (dtListaEstado.Rows.Count > 0)
                {
                    dgvEstadoMtto.Columns["idSolicitud"].Visible = false;

                    dgvEstadoMtto.Columns["PLACA"].Summary.Clear();
                    dgvEstadoMtto.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL_PLACAS", "Total = {0}");

                    dgvEstadoMtto.BestFitColumns();
                }
            }
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarEstadoUnidades(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarEstadoUnidades(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarEstadoUnidades(); }
        }

        private void cbxBase_DropDownClosed(object sender, EventArgs e) { ListarEstadoUnidades(); }

        private void cbxRepuesto_DropDownClosed(object sender, EventArgs e) { ListarEstadoUnidades(); }

        private void dtgEstadoMtto_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string ES = dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "ESTADO").ToString();
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                tsProgramarSolicitud.Text = "Programar Solicitud";

                if (ES == "RECEPCIONADO")
                {
                    if (e1 == 1) 
                    {
                        tsProgramarSolicitud.Enabled = true;
                        tsCambiarUbicacion.Enabled = true;
                    }
                    tsTerminarMantenimiento.Enabled = false;
                }

                if (ES == "PROGRAMADO" || ES == "REPROGRAMADO")
                {
                    tsProgramarSolicitud.Text = "Reprogramar Solicitud";

                    if (e1 == 1)
                    {
                        tsProgramarSolicitud.Enabled = true;
                        tsCambiarUbicacion.Enabled = true;
                    }
                    else
                    {
                        tsProgramarSolicitud.Enabled = false;
                        tsCambiarUbicacion.Enabled = false;
                    }

                    if (e2 == 1) { tsTerminarMantenimiento.Enabled = true; }
                }

                if (ES == "COMPLETADO")
                {
                    tsProgramarSolicitud.Enabled = false;
                    tsCambiarUbicacion.Enabled = false;
                    tsTerminarMantenimiento.Enabled = false;
                }
            }
            catch
            {
                tsProgramarSolicitud.Enabled = false;
                tsCambiarUbicacion.Enabled = false;
                tsTerminarMantenimiento.Enabled = false;
            }
        }

        private void dgvEstadoMtto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "DÍAS")
            {
                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) <= 5)
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToInt32(e.CellValue) > 5 && Convert.ToInt32(e.CellValue) <= 15)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (Convert.ToInt32(e.CellValue) >= 16)
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "DÍAS_PEDIDO")
            {
                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) <= 5)
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToInt32(e.CellValue) > 5 && Convert.ToInt32(e.CellValue) <= 15)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (Convert.ToInt32(e.CellValue) >= 16)
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "PROGRAMACION_VIAJE")
            {
                if (Convert.ToString(e.CellValue) == "NO PROGRAMADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "PROGRAMADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarEstadoUnidades(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgEstadoMtto.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "ESTADO DE UNIDADES DE MANTENIMIENTO - " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgEstadoMtto.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnReprog_Click(object sender, EventArgs e)
        {
            frmPlantillaReprog frmPlantillaReprog = new frmPlantillaReprog();
            frmPlantillaReprog.Opcion = 1;
            frmPlantillaReprog.label1.Text = "PLANTILLA DE REPROGRAMACIÓN";
            frmPlantillaReprog.ShowDialog();
        }

        private void btnCambioUbicaciones_Click(object sender, EventArgs e)
        {
            frmPlantillaReprog frmPlantillaReprog = new frmPlantillaReprog();
            frmPlantillaReprog.Opcion = 2;
            frmPlantillaReprog.label1.Text = "REGISTRO DE CAMBIO DE UBICACIONES";
            frmPlantillaReprog.ShowDialog();
        }

        private void rbTodos_Click(object sender, EventArgs e)
        {
            EstadoProg = "TODOS";
            ListarEstadoUnidades();
        }

        private void rbProgramado_CheckedChanged(object sender, EventArgs e)
        {
            EstadoProg = "PROGRAMADO";
            ListarEstadoUnidades();
        }

        private void rbNoProgramado_CheckedChanged(object sender, EventArgs e)
        {
            EstadoProg = "NO PROGRAMADO";
            ListarEstadoUnidades();
        }

        private void tsProgramarSolicitud_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            idSolicitud = Convert.ToInt32(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "idSolicitud"));
            lblPlaca.Text = Convert.ToString(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "PLACA"));
            lblEstado.Text = Convert.ToString(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "ESTADO"));

            if (lblEstado.Text == "RECEPCIONADO") { lblTitulo.Text = "PROGRAMAR SOLICITUD"; }
            if (lblEstado.Text == "PROGRAMADO" || lblEstado.Text == "REPROGRAMADO") { lblTitulo.Text = "REPROGRAMAR SOLICITUD"; }
            
            pRegistrarProg.BringToFront();
            pRegistrarProg.Visible = true;
        }

        private void tsCambiarUbicacion_Click(object sender, EventArgs e)
        {
            idSolicitud = Convert.ToInt32(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "idSolicitud"));
            
            CargarComboBase2(2);
            cbxBase2.Text = dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "UBICACION").ToString();
            cbxBase2_DropDownClosed(sender, e);
            dtpFechaRecepcion.Value = Convert.ToDateTime(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "FECHA_INICIO"));
            dtpHoraRecepcion.Value = Convert.ToDateTime(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "FECHA_INICIO"));

            label78.Visible = false;
            txtNuevaUbicacion.Clear();
            txtNuevaUbicacion.Visible = false;
            btnGuardarUbicacion.Visible = false;

            pAsignacionUnidad.Visible = true;
            pAsignacionUnidad.BringToFront();
        }

        private void pAsignacionUnidad_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pAsignacionUnidad.Left = pAsignacionUnidad.Left + (e.X - xClick2);
                pAsignacionUnidad.Top = pAsignacionUnidad.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            idSolicitud = 0;
            pAsignacionUnidad.Visible = false;
            pAsignacionUnidad.SendToBack();
            dtpFechaRecepcion.Value = DateTime.Now;
            dtpHoraRecepcion.Value = new DateTime(dtpFechaRecepcion.Value.Year, dtpFechaRecepcion.Value.Month, 1, 0, 0, 0);
            txtUbicacionTaller.Clear();
            txtNuevaUbicacion.Clear();
        }

        private void dtpFechaRecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraRecepcion.Focus(); }
        }

        private void dtpHoraRecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxBase2.Focus(); }
        }

        private void cbxBase2_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxBase2.Text == "GT TRU (MTTO)" || cbxBase2.Text == "GT TRU (BRA)" || cbxBase2.Text == "GT LIMA (MTTO)")
            {
                txtUbicacionTaller.Enabled = true;
                txtUbicacionTaller.Clear();
            }
            else
            {
                txtUbicacionTaller.Enabled = false;
                txtUbicacionTaller.Clear();
            }
        }

        private void btnAgregarUbicacion_Click(object sender, EventArgs e)
        {
            if (txtNuevaUbicacion.Visible == false)
            {
                label78.Visible = true;
                txtNuevaUbicacion.Clear();
                txtNuevaUbicacion.Visible = true;
                btnGuardarUbicacion.Visible = true;
            }
            else
            {
                label78.Visible = false;
                txtNuevaUbicacion.Clear();
                txtNuevaUbicacion.Visible = false;
                btnGuardarUbicacion.Visible = false;
            }
        }

        private void txtUbicacionTaller_Enter(object sender, EventArgs e) { txtUbicacionTaller.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtUbicacionTaller_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTaller, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarTalleres(txtUbicacionTaller.Text), true, false, false);
            lstTaller.Columns[0].Width = 0;
            lstTaller.Columns[1].Width = 90;
            lstTaller.BringToFront();
            lstTaller.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }
        }

        private void txtUbicacionTaller_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTaller.Focus(); }
        }

        private void txtUbicacionTaller_Leave(object sender, EventArgs e) { txtUbicacionTaller.BackColor = Color.White; }

        private void lstTaller_Enter(object sender, EventArgs e)
        {
            if (!lstTaller.Items.Count.Equals(0)) { lstTaller.Items[0].Selected = true; } 
        }

        private void lstTaller_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTaller.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTaller.SelectedItems[0];
                txtUbicacionTaller.Text = ItemActual.SubItems[1].Text;

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(txtUbicacionTaller.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { btnFechaAsignacion.Focus(); }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUbicacionTaller.Clear();
                    txtUbicacionTaller.Focus();
                }

                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }
        }

        private void lstTaller_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTaller.SelectedItems[0];
            txtUbicacionTaller.Text = ItemActual.SubItems[1].Text;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(txtUbicacionTaller.Text);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0") { btnFechaAsignacion.Focus(); }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUbicacionTaller.Clear();
                txtUbicacionTaller.Focus();
            }

            lstTaller.Visible = false;
            lstTaller.SendToBack();
        }

        private void txtNuevaUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarUbicacion_Click(sender, e); }
        }

        private void btnGuardarUbicacion_Click(object sender, EventArgs e)
        {
            if (txtNuevaUbicacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingrese una ubicación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevaUbicacion.Focus();
                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea ingresar esta nueva ubicación?", "AGREGAR NUEVA UBICACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarUbicacion(txtNuevaUbicacion.Text);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarComboBase2(2);
                        label78.Visible = false;
                        txtNuevaUbicacion.Visible = false;
                        btnGuardarUbicacion.Visible = false;
                        txtNuevaUbicacion.Clear();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnFechaAsignacion_Click(object sender, EventArgs e)
        {
            
            string FechaRecepcion = dtpFechaRecepcion.Text + ' ' + dtpHoraRecepcion.Text;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarFechaRecepcion(2, idSolicitud, Convert.ToDateTime(FechaRecepcion),
                                                       Convert.ToInt32(cbxBase2.SelectedValue), txtUbicacionTaller.Text, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCerrar2_Click(sender, e);
                ListarEstadoUnidades();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsTerminarMantenimiento_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            idSolicitud = Convert.ToInt32(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "idSolicitud"));
            lblPlaca.Text = Convert.ToString(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "PLACA"));
            lblEstado.Text = Convert.ToString(dgvEstadoMtto.GetRowCellValue(dgvEstadoMtto.FocusedRowHandle, "ESTADO"));
            lblTitulo.Text = "COMPLETAR SOLICITUD";

            pRegistrarProg.BringToFront();
            pRegistrarProg.Visible = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            idSolicitud = 0;
            Opcion = 0;
            lblPlaca.Text = "";
            lblEstado.Text = "";
            dtpFechaProg.Value = DateTime.Now;
            txtObservacion.Clear();
            pRegistrarProg.Visible = false;
            pRegistrarProg.SendToBack();
        }

        private void pRegistrarProg_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pRegistrarProg.Left = pRegistrarProg.Left + (e.X - xClick);
                pRegistrarProg.Top = pRegistrarProg.Top + (e.Y - yClick);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                if (lblEstado.Text == "PROGRAMADO" || lblEstado.Text == "REPROGRAMADO")
                {
                    if (txtObservacion.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor ingrese la observación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtObservacion.Focus();
                        return;
                    }
                }

                if (lblEstado.Text == "RECEPCIONADO")
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Programar(1, idSolicitud, dtpFechaProg.Value, txtObservacion.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCerrar_Click(sender, e);
                        _frmLista.btnBuscarS_Click(sender, e);
                        ListarEstadoUnidades();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                if (lblEstado.Text == "PROGRAMADO" || lblEstado.Text == "REPROGRAMADO")
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Programar(2, idSolicitud, dtpFechaProg.Value, txtObservacion.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _frmLista.btnBuscarS_Click(sender, e);
                        btnCerrar_Click(sender, e);
                        ListarEstadoUnidades();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            
            if (Opcion == 2)
            {
                if (txtObservacion.Text.Length == 0)
                {
                    MessageBox.Show("Por favor ingrese la observación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtObservacion.Focus();
                    return;
                }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Programar(3, idSolicitud, dtpFechaProg.Value, txtObservacion.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _frmLista.btnBuscarS_Click(sender, e);
                        btnCerrar_Click(sender, e);
                        ListarEstadoUnidades();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
