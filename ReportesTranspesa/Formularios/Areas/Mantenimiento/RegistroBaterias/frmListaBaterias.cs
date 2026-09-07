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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroBaterias
{
    public partial class frmListaBaterias : Form
    {
        public int Estado, idBateria, idVehiculo, FiltroFechas;
        public string TipoFecha;
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int e1 = 0;

        public frmListaBaterias()
        {
            InitializeComponent();
        }

        private void ListaBaterias_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaBaterias");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                    else { btnAgregar.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { modificarBateriaToolStripMenuItem.Enabled = true; }
                    else { modificarBateriaToolStripMenuItem.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { anularBateriaToolStripMenuItem.Enabled = true; }
                    else { anularBateriaToolStripMenuItem.Enabled = false; }
                }

                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                if (dtEspeciales.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Traspasos")
                        {
                            traspasoToolStripMenuItem.Enabled = true;
                            i = 999; e1 = 1;
                        }
                        else { traspasoToolStripMenuItem.Enabled = false; }
                    }
                }
                else { traspasoToolStripMenuItem.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaCIni.Value = new DateTime(dtpFechaCIni.Value.Year, dtpFechaCIni.Value.Month, 1);
            dtpFechaCFin.Value = DateTime.Now;
            dtpFechaCambio.Value = DateTime.Now;
            dtpFechaInspeccion.Value = DateTime.Now;

            cbFiltroFechas.Checked = true;
            cbFechaInspeccion_CheckedChanged(sender, e);

            rbActivas.Checked = true;
            rbActivas_Click(sender, e);

            ListarBaterias();
        }


        public void ListarBaterias()
        {
            if (FiltroFechas == 1 && TipoFecha == "FC" && dtpFechaCIni.Value > dtpFechaCFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaCIni.Focus();
                return;
            }
            
            if (FiltroFechas == 1 && TipoFecha == "FI" && dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtgBaterias.DataSource = null;
                dgvBateriasVista.Columns.Clear();
                string FechaInicio = "", FechaFin = "";

                if (FiltroFechas == 1 && TipoFecha == "FI")
                {
                    FechaInicio = dtpFechaIni.Text;
                    FechaFin = dtpFechaFin.Text;
                }

                if (FiltroFechas == 1 && TipoFecha == "FC")
                {
                    FechaInicio = dtpFechaCIni.Text;
                    FechaFin = dtpFechaCFin.Text;
                }


                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_ListarBaterias(FiltroFechas, TipoFecha, txtCodigo.Text, txtPlaca.Text, FechaInicio, FechaFin, Estado);
                if (dt.Rows.Count > 0)
                {
                    dtgBaterias.DataSource = dt;

                    if (Estado == 1 || Estado == 2) { dgvBateriasVista.Columns["MOTIVO"].Visible = false; }

                    /*
                    if (Estado == 2 || Estado == 0)
                    {
                        dgvBateriasVista.Columns["VEHICULO"].Visible = false;
                        dgvBateriasVista.Columns["TIPO_VEHICULO"].Visible = false;
                    }
                    */

                    dgvBateriasVista.Columns["idBateria"].Visible = false;
                    dgvBateriasVista.Columns["idVehiculo"].Visible = false;

                    dgvBateriasVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvBateriasVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvBateriasVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvBateriasVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvBateriasVista.Columns["CODIGO"].Summary.Clear();
                    dgvBateriasVista.Columns["CODIGO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CODIGO", "Total = {0}");

                    dgvBateriasVista.BestFitColumns();
                }
            }
        }


        private void dgvBateriasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "ACTIVA") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "INACTIVA") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (e.CellValue.ToString() == "EN ALMACEN") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
            }

            if (e.Column.FieldName == "DIAS_FALTANTES")
            {
                if (Convert.ToInt32(e.CellValue) >= 0) { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToInt32(e.CellValue) < 0) { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void cbFechaInspeccion_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFiltroFechas.Checked == true)
            {
                FiltroFechas = 1;
                groupBox15.Enabled = true;

                rbFechaInspeccion.Checked = true;
                rbFechaInspeccion_Click(sender, e);
            }

            if (cbFiltroFechas.Checked == false)
            {
                FiltroFechas = 0;
                groupBox15.Enabled = false;

                rbFechaInspeccion.Checked = true;
                rbFechaInspeccion_Click(sender, e);

                ListarBaterias();
            }
        }

        private void rbFechaInspeccion_Click(object sender, EventArgs e)
        {
            TipoFecha = "FI";

            dtpFechaIni.Enabled = true;
            dtpFechaFin.Enabled = true;
            dtpFechaCIni.Enabled = false;
            dtpFechaCFin.Enabled = false;

            ListarBaterias();
        }

        private void rbFechaCambio_Click(object sender, EventArgs e)
        {
            TipoFecha = "FC";
            
            dtpFechaIni.Enabled = false;
            dtpFechaFin.Enabled = false;
            dtpFechaCIni.Enabled = true;
            dtpFechaCFin.Enabled = true;

            ListarBaterias();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmInsertarBaterias frmInsertarBaterias = new frmInsertarBaterias();
            frmInsertarBaterias.opcion = 1;
            frmInsertarBaterias.formulario = this;
            frmInsertarBaterias.label4.Visible = false;
            frmInsertarBaterias.txtDuracion.Visible = false;
            frmInsertarBaterias.FechaIni.Value = DateTime.Now;
            frmInsertarBaterias.ShowDialog();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            frmHistorialTraspasos frmHistorialTraspasos = new frmHistorialTraspasos();
            frmHistorialTraspasos.Opcion = 1;
            frmHistorialTraspasos.label1.Text = "HISTORIAL DE TRASPASOS";
            frmHistorialTraspasos.groupBox15.Text = "Buscar por Fecha de Traspaso: ";
            frmHistorialTraspasos.ShowDialog();
        }

        private void tsHistorialInspecciones_Click(object sender, EventArgs e)
        {
            frmHistorialTraspasos frmHistorialTraspasos = new frmHistorialTraspasos();
            frmHistorialTraspasos.Opcion = 2;
            frmHistorialTraspasos.label1.Text = "REGISTRO DE INSPECCIONES";
            frmHistorialTraspasos.groupBox15.Text = "Buscar por Fecha de Inspección: ";
            frmHistorialTraspasos.ShowDialog();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarBaterias(); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarBaterias(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarBaterias(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarBaterias(); }
        }

        private void rbActivas_Click(object sender, EventArgs e)
        {
            if (rbActivas.Checked == true)
            {
                rbActivas.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                rbInactivas.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                rbAlmacen.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                dtgBaterias.ContextMenuStrip = contextMenuStrip1;
                Estado = 1;

                ListarBaterias();
            }
        }

        private void rbAlmacen_Click(object sender, EventArgs e)
        {
            if (rbAlmacen.Checked == true)
            {
                rbActivas.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                rbAlmacen.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                rbInactivas.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                dtgBaterias.ContextMenuStrip = contextMenuStrip3;
                Estado = 2;

                ListarBaterias();
            }
        }

        private void rbInactivas_Click(object sender, EventArgs e)
        {
            if (rbInactivas.Checked == true)
            {
                rbActivas.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                rbAlmacen.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                rbInactivas.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                dtgBaterias.ContextMenuStrip = null;
                dtgBaterias.ContextMenuStrip = contextMenuStrip2;
                Estado = 0;

                ListarBaterias();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarBaterias(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgBaterias.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Baterías - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgBaterias.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void modificarBateriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));

            if (idBateria != 0)
            {
                frmInsertarBaterias frmInsertarBaterias = new frmInsertarBaterias();
                frmInsertarBaterias.RecibirDatos(idBateria);
                frmInsertarBaterias.opcion = 2;
                frmInsertarBaterias.formulario = this;
                frmInsertarBaterias.txtPlaca.Enabled = false;
                frmInsertarBaterias.txtTipo.Enabled = false;
                frmInsertarBaterias.ShowDialog();
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void almacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (idBateria != 0)
            {
                if (MessageBox.Show("¿Desea colocar esta batería en almacén?", "ESTADO DE BATERÍA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria(3, idBateria, " ", Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pInactivarBateria.Visible = false;
                        pInactivarBateria.SendToBack();
                        txtMotivo.Clear();
                        ListarBaterias();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsGenerarInspeccion_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));
            lblBateria.Text = Convert.ToString(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "CODIGO"));
            lblUnidad.Text = Convert.ToString(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "VEHICULO"));
            lblProgramacion.Text = Convert.ToString(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "PROGRAMACION"));

            txtIntervalo.Text = Convert.ToString(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "INTERVALO_DIAS"));
            dtpFechaCambio.Value = Convert.ToDateTime(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "FECHA_CAMBIO"));
            dtpFechaInspeccion.Value = DateTime.Now;
            txtNivelCarga.Text = "0.00";
            txtEstadoB.Text = "0.00";
            
            pAgregarInspeccion.Visible = true;
            pAgregarInspeccion.BringToFront();
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pAgregarInspeccion.Visible = false;
            pAgregarInspeccion.SendToBack();

            txtIntervalo.Clear();
            dtpFechaCambio.Value = DateTime.Now;
            dtpFechaInspeccion.Value = DateTime.Now;
            txtNivelCarga.Text = "0.00";
            txtEstadoB.Text = "0.00";
        }

        private void pAgregarInspeccion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pAgregarInspeccion.Left = pAgregarInspeccion.Left + (e.X - xClick2);
                pAgregarInspeccion.Top = pAgregarInspeccion.Top + (e.Y - yClick2);
            }
        }

        private void traspasoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));
            idVehiculo = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idVehiculo"));

            if (idBateria != 0)
            {
                frmTraspasoBateria frmTraspasoBateria = new frmTraspasoBateria();
                frmTraspasoBateria.idVehiculoANT = idVehiculo;
                frmTraspasoBateria.formulario = this;
                frmTraspasoBateria.RecibirDatos(idBateria);
                frmTraspasoBateria.ShowDialog();
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void modificarBateriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));

            if (idBateria != 0)
            {
                frmInsertarBaterias frmInsertarBaterias = new frmInsertarBaterias();
                frmInsertarBaterias.RecibirDatos(idBateria);
                frmInsertarBaterias.opcion = 2;
                frmInsertarBaterias.formulario = this;
                frmInsertarBaterias.txtPlaca.Enabled = false;
                frmInsertarBaterias.txtTipo.Enabled = false;
                frmInsertarBaterias.ShowDialog();
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void anularBateriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));
            
            pInactivarBateria.Visible = true;
            pInactivarBateria.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pInactivarBateria.Visible = false;
            pInactivarBateria.SendToBack();
        }

        private void btnInactivar_Click(object sender, EventArgs e)
        {
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (txtMotivo.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el motivo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMotivo.Focus();
                return;
            }
            else
            {
                if (idBateria != 0)
                {
                    if (MessageBox.Show("¿Desea cambiar el estado de esta batería?", "INACTIVAR BATERÍA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria(1, idBateria, txtMotivo.Text, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pInactivarBateria.Visible = false;
                            pInactivarBateria.SendToBack();
                            txtMotivo.Clear();
                            ListarBaterias();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void habilitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (idBateria != 0)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria(4, idBateria, txtMotivo.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarBaterias();
                }
                else
                { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /*
        private void eliminarBateríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idBateria = Convert.ToInt32(dgvBateriasVista.GetRowCellValue(dgvBateriasVista.FocusedRowHandle, "idBateria"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (idBateria != 0)
            {
                if (MessageBox.Show("¿Desea eliminar esta batería?", "ELIMINAR BATERÍA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria(2, idBateria, "", Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarBaterias();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        */

        private void pInactivarBateria_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pInactivarBateria.Left = pInactivarBateria.Left + (e.X - xClick);
                pInactivarBateria.Top = pInactivarBateria.Top + (e.Y - yClick);
            }
        }

        private void dtpFechaInspeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtNivelCarga.Focus(); }
        }

        private void txtNivelCarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtEstadoB.Focus(); }
        }

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtIntervalo.Focus(); }
        }

        private void txtIntervalo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaCambio.Focus(); }
        }

        private void dtpFechaCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtIntervalo.Text.Length == 0 || txtNivelCarga.Text.Length == 0 || txtEstadoB.Text.Length == 0 )
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                if (txtIntervalo.Text.Length == 0) { txtIntervalo.Focus(); }
                else
                {
                    if (txtNivelCarga.Text.Length == 0) { txtNivelCarga.Focus(); }
                    else { txtEstadoB.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlBaterias_GenerarInspeccion(idBateria, lblBateria.Text, Convert.ToInt32(txtIntervalo.Text),
                           dtpFechaCambio.Value, dtpFechaInspeccion.Value, Convert.ToDecimal(txtNivelCarga.Text), Convert.ToDecimal(txtEstadoB.Text), Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarBaterias();
                    btnCerrar2_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
