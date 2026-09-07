using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.SolicitudesPersonal
{
    public partial class frmListaSolicitudesPersonal : MetroFramework.Forms.MetroForm
    {
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int e1 = 0, e2 = 0;

        public frmListaSolicitudesPersonal()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
            cbxEstado.SelectedIndexChanged -= cbxEstado_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void cbxEstado_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEstado(); }

        private void frmListaSolicitudesPersonal_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaSolicitudesPersonal");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevaSolicitud.Enabled = true; }
                else { btnNuevaSolicitud.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true)
                {
                    btnBuscar.Visible = true;
                    btnExcel.Visible = true;
                }
                else
                {
                    btnBuscar.Visible = false;
                    btnExcel.Visible = false;
                }
            }

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Aprobar")
                    {
                        aprobarToolStripMenuItem.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { aprobarToolStripMenuItem.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Fecha de Entrega")
                    {
                        fechaDeEntregaToolStripMenuItem.Enabled = true;
                        i = 999; e2 = 1;
                    }
                    else { fechaDeEntregaToolStripMenuItem.Enabled = false; }
                }
            }
            else
            {
                aprobarToolStripMenuItem.Enabled = false;
                fechaDeEntregaToolStripMenuItem.Enabled = false;
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            CargarComboArea();
            CargarComboEstado();
        }


        private void CargarComboArea()
        {
            DataTable dtArea = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(2);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "Nombre";
            cbxArea.ValueMember = "CodAreaSpring";
        }

        private void CargarComboEstado()
        {
            DataTable dtEstado = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(5);
            cbxEstado.DataSource = dtEstado;
            cbxEstado.DisplayMember = "NombreEstado";
            cbxEstado.ValueMember = "idEstadoSolicitud";
        }

        public void ListarSolicitudes()
        {
            string fechin, fechfin;
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtgListaSolicitudes.DataSource = null;
                dgvListaSolicitudesVista.Columns.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitudesPersonal(Convert.ToInt32(cbxArea.SelectedValue), dtpFechaIni.Text, dtpFechaFin.Text, Convert.ToInt32(cbxEstado.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    dtgListaSolicitudes.DataSource = dt;
                    dgvListaSolicitudesVista.Columns["CodAreaSpring"].Visible = false;
                    dgvListaSolicitudesVista.Columns["CodigoPuesto"].Visible = false;
                    dgvListaSolicitudesVista.Columns["idTipo"].Visible = false;
                    dgvListaSolicitudesVista.Columns["FechaRegistra"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaSolicitudesVista.Columns["FechaRegistra"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvListaSolicitudesVista.Columns["FechaModifica"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaSolicitudesVista.Columns["FechaModifica"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvListaSolicitudesVista.BestFitColumns();
                }
            }
        }


        private void dgvListaSolicitudesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "Estado")
            {
                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (e.CellValue.ToString() == "APROBADA") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "RECHAZADA") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (e.CellValue.ToString() == "EN PROCESO") { e.Appearance.BackColor = Color.FromArgb(255, 213, 0); }

                if (e.CellValue.ToString() == "CERRADA") { e.Appearance.BackColor = Color.FromArgb(148, 0, 211); }
            }
        }

        private void dtgListaSolicitudes_DoubleClick(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Codigo"));
            if (idSolicitud >= 1)
            {
                frmNuevaSolicitudPersonal f1 = new frmNuevaSolicitudPersonal();
                f1.CargarComboArea();
                f1.CargarComboEstado();
                f1.CargarComboPuesto();
                f1.CargarComboTipo();
                f1.ListarSolicitud(idSolicitud);

                f1.cbxArea.Enabled = false;
                f1.cbxPuesto.Enabled = false;
                f1.txtNroVacante.Enabled = false;
                f1.cbxTipoSolicitud.Enabled = false;
                f1.txtReemplazo.Enabled = false;
                f1.txtObservacion.Enabled = false;
                f1.cbxEstado.Enabled = false;
                f1.cbxPrioridad.Enabled = false;
                f1.dtpFechaEntrega.Enabled = false;

                f1.btnCancelar.Enabled = false;
                f1.btnGuardar.Enabled = false;
                f1.btnAprobar.Enabled = false;
                f1.btnFechaEntrega.Enabled = false;

                f1.ShowDialog();
            }
            else { MessageBox.Show("La solicitud seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaSolicitudes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string ES = dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Estado").ToString();

                if (ES == "PENDIENTE")
                {
                    if (e1 == 1) { aprobarToolStripMenuItem.Enabled = true; }
                    fechaDeEntregaToolStripMenuItem.Enabled = false;
                    listaCandidatosToolStripMenuItem.Enabled = false;
                }

                if (ES == "APROBADA")
                {
                    aprobarToolStripMenuItem.Enabled = false;
                    if (e2 == 1) { fechaDeEntregaToolStripMenuItem.Enabled = true; }
                    listaCandidatosToolStripMenuItem.Enabled = false;
                }

                if (ES == "RECHAZADA")
                {
                    aprobarToolStripMenuItem.Enabled = false;
                    fechaDeEntregaToolStripMenuItem.Enabled = false;
                    listaCandidatosToolStripMenuItem.Enabled = false;
                }

                if (ES == "EN PROCESO" || ES == "CERRADA")
                {
                    aprobarToolStripMenuItem.Enabled = false;
                    fechaDeEntregaToolStripMenuItem.Enabled = false;
                    listaCandidatosToolStripMenuItem.Enabled = true;
                }
            }
            catch
            {
                aprobarToolStripMenuItem.Enabled = false;
                fechaDeEntregaToolStripMenuItem.Enabled = false;
                listaCandidatosToolStripMenuItem.Enabled = false;
            }
        }

        private void btnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            frmNuevaSolicitudPersonal frmNuevaSolicitudPersonal = new frmNuevaSolicitudPersonal();
            frmNuevaSolicitudPersonal.opcion = 1;       // NUEVA SOLICITUD
            frmNuevaSolicitudPersonal.RecibirDatos(this);
            frmNuevaSolicitudPersonal.CargarComboArea();
            frmNuevaSolicitudPersonal.CargarComboTipo();
            frmNuevaSolicitudPersonal.CargarComboPuesto();
            frmNuevaSolicitudPersonal.cbxPrioridad.Text = "NORMAL";

            frmNuevaSolicitudPersonal.groupBox2.Enabled = false;
            frmNuevaSolicitudPersonal.groupBox3.Enabled = false;
            frmNuevaSolicitudPersonal.cbxTipoSolicitud_DropDownClosed(sender, e);
            frmNuevaSolicitudPersonal.ShowDialog();
        }

        private void aprobarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Codigo"));
            if (idSolicitud >= 1)
            {
                frmNuevaSolicitudPersonal f1 = new frmNuevaSolicitudPersonal();
                f1.RecibirDatos(this);
                f1.idSolicitudPersonal = idSolicitud;
                f1.CargarComboArea();
                f1.CargarComboTipo();
                f1.CargarComboPuesto();

                DataTable dtEstado = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(4);
                f1.cbxEstado.DataSource = dtEstado;
                f1.cbxEstado.DisplayMember = "NombreEstado";
                f1.cbxEstado.ValueMember = "idEstadoSolicitud";

                f1.ListarSolicitud(idSolicitud);

                f1.cbxArea.Enabled = false;
                f1.cbxPuesto.Enabled = false;
                f1.txtNroVacante.Enabled = false;
                f1.cbxTipoSolicitud.Enabled = false;
                f1.txtReemplazo.Enabled = false;
                f1.txtObservacion.Enabled = false;
                f1.cbxEstado.Enabled = true;
                f1.cbxPrioridad.Enabled = false;
                f1.dtpFechaEntrega.Enabled = false;

                f1.btnCancelar.Enabled = false;
                f1.btnGuardar.Enabled = false;
                f1.btnAprobar.Enabled = true;
                f1.btnFechaEntrega.Enabled = false;

                f1.ShowDialog();
            }
            else { MessageBox.Show("La solicitud seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void fechaDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Codigo"));
            if (idSolicitud >= 1)
            {
                frmNuevaSolicitudPersonal f1 = new frmNuevaSolicitudPersonal();
                f1.RecibirDatos(this);
                f1.idSolicitudPersonal = idSolicitud;
                f1.CargarComboArea();
                f1.CargarComboEstado();
                f1.CargarComboPuesto();
                f1.CargarComboTipo();

                f1.ListarSolicitud(idSolicitud);

                f1.cbxArea.Enabled = false;
                f1.cbxPuesto.Enabled = false;
                f1.txtNroVacante.Enabled = false;
                f1.cbxTipoSolicitud.Enabled = false;
                f1.txtReemplazo.Enabled = false;
                f1.txtObservacion.Enabled = false;
                f1.cbxEstado.Enabled = false;
                f1.cbxPrioridad.Enabled = false;
                f1.dtpFechaEntrega.Enabled = true;

                f1.btnCancelar.Enabled = false;
                f1.btnGuardar.Enabled = false;
                f1.btnAprobar.Enabled = false;
                f1.btnFechaEntrega.Enabled = true;

                f1.ShowDialog();
            }
            else { MessageBox.Show("La solicitud seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void listaCandidatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Area = dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Area").ToString();
            string Puesto = dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Puesto").ToString();
            string Solicitud = dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Solicitud").ToString();
            string Estado = dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Estado").ToString();
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudesVista.GetRowCellValue(dgvListaSolicitudesVista.FocusedRowHandle, "Codigo"));

            frmListaCandidatos frmListaCandidatos = new frmListaCandidatos();

            frmListaCandidatos.dtPermisos2 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaSolicitudesPersonal");

            if (frmListaCandidatos.dtPermisos2 != null)
            {
                if (frmListaCandidatos.dtPermisos2.Rows[0]["PermisosEspeciales"].ToString() != "")
                { frmListaCandidatos.dtEspeciales2 = Utilitario.Instancia.ConvertirXMLaDatatable(frmListaCandidatos.dtPermisos2.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (frmListaCandidatos.dtEspeciales2.Rows.Count > 0)
            {
                for (int i = 0; i < frmListaCandidatos.dtEspeciales2.Rows.Count; i++)
                {
                    if (frmListaCandidatos.dtEspeciales2.Rows[i]["NombrePermiso"].ToString() == "Editar Candidatos")
                    {
                        frmListaCandidatos.btnAgregar.Enabled = true;
                        frmListaCandidatos.btnModificar.Enabled = true;
                        frmListaCandidatos.btnQuitar.Enabled = true;
                        i = 999; frmListaCandidatos.e3 = 1;
                    }
                    else
                    {
                        frmListaCandidatos.btnAgregar.Enabled = false;
                        frmListaCandidatos.btnModificar.Enabled = false;
                        frmListaCandidatos.btnQuitar.Enabled = false;
                    }
                }

                for (int i = 0; i < frmListaCandidatos.dtEspeciales2.Rows.Count; i++)
                {
                    if (frmListaCandidatos.dtEspeciales2.Rows[i]["NombrePermiso"].ToString() == "Seleccionar Candidatos")
                    {
                        frmListaCandidatos.seleccionarToolStripMenuItem.Enabled = true;
                        i = 999; frmListaCandidatos.e4 = 1;
                    }
                    else { frmListaCandidatos.seleccionarToolStripMenuItem.Enabled = false; }
                }
            }
            else
            {
                frmListaCandidatos.btnAgregar.Enabled = false;
                frmListaCandidatos.btnModificar.Enabled = false;
                frmListaCandidatos.btnQuitar.Enabled = false;
                frmListaCandidatos.seleccionarToolStripMenuItem.Enabled = false;
            }

            frmListaCandidatos.RecibirDatos(Area, Puesto, Solicitud, Estado, idSolicitud, this);

            if(Estado == "EN PROCESO")
            {
                if (frmListaCandidatos.e3 == 1)
                {
                    frmListaCandidatos.btnAgregar.Enabled = true;
                    frmListaCandidatos.btnModificar.Enabled = true;
                    frmListaCandidatos.btnQuitar.Enabled = true;
                }
            }
            else
            {
                frmListaCandidatos.btnAgregar.Enabled = false;
                frmListaCandidatos.btnModificar.Enabled = false;
                frmListaCandidatos.btnQuitar.Enabled = false;
            }

            frmListaCandidatos.ShowDialog();
        }

        public void btnBuscar_Click(object sender, EventArgs e) { ListarSolicitudes(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaSolicitudes.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Solicitudes de Personal - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaSolicitudes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e) { ListarSolicitudes(); }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSolicitudes(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSolicitudes(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarSolicitudes(); }
    }
}
