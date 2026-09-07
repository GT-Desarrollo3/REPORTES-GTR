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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmPendientesDiarios : Form
    {
        DataTable dtListaPendientes = new DataTable();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int e1 = 0, FechaP = 0;

        public frmPendientesDiarios()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void frmPendientesDiarios_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmPendientesDiarios");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevaActividad.Enabled = true; }
                else { btnNuevaActividad.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { actualizarEstadoToolStripMenuItem.Enabled = true; }
                else { actualizarEstadoToolStripMenuItem.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarActividadToolStripMenuItem.Enabled = true; }
                else { eliminarActividadToolStripMenuItem.Enabled = false; }
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
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Reprogramar Actividades")
                    {
                        reprogramarActividadToolStripMenuItem.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { reprogramarActividadToolStripMenuItem.Enabled = false; }
                }
            }
            else
            { reprogramarActividadToolStripMenuItem.Enabled = false; }

            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            cbxEstado.Text = "TODOS";
            CargarComboArea();
            cbFechaProyectada.Checked = false;
            cbFechaProyectada_CheckedChanged(sender, e);
            ListarPendientesDiarios();
        }


        public void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(3, "");
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "description";
            cbxArea.ValueMember = "department";
        }

        public void ListarPendientesDiarios()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaPendientes = clsOperacionesBL.Instancia.ReportesApp_Operaciones_PendientesDiarios_ListarActividades(FechaP, txtResponsable.Text, cbxArea.Text,
                    cbxEstado.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgListaPendientes.DataSource = dtListaPendientes;
                if (dtListaPendientes.Rows.Count > 0)
                {
                    dgvListaPendientesVista.Columns["Persona"].Visible = false;

                    dgvListaPendientesVista.Columns["ESTADO"].Summary.Clear();
                    dgvListaPendientesVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvListaPendientesVista.BestFitColumns();
                }
            }
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmPendientesDiariosAgregar frmPendientesDiariosAgregar = new frmPendientesDiariosAgregar();
            frmPendientesDiariosAgregar.Opcion = 1;
            frmPendientesDiariosAgregar.label5.Visible = false;
            frmPendientesDiariosAgregar.cbxEstado.Visible = false;
            frmPendientesDiariosAgregar.label9.Visible = false;
            frmPendientesDiariosAgregar.dtpFechaP2.Visible = false;
            frmPendientesDiariosAgregar.label10.Visible = false;
            frmPendientesDiariosAgregar.dtpFechaP3.Visible = false;
            frmPendientesDiariosAgregar.formulario = this;
            frmPendientesDiariosAgregar.ShowDialog();
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e) { ListarPendientesDiarios(); }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarPendientesDiarios(); }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { ListarPendientesDiarios(); }
        }

        private void cbFechaProyectada_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFechaProyectada.Checked == true)
            {
                FechaP = 1;
                dtpFechaInicio.Enabled = true;
                dtpFechaFin.Enabled = true;
                ListarPendientesDiarios();
            }

            if (cbFechaProyectada.Checked == false)
            {
                FechaP = 0;
                dtpFechaInicio.Enabled = false;
                dtpFechaFin.Enabled = false;
                ListarPendientesDiarios();
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { ListarPendientesDiarios(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { ListarPendientesDiarios(); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaPendientes.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Pendientes Diarios - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaPendientes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarPendientesDiarios(); }

        private void dgvListaPendientesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "DESESTIMADO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (Convert.ToString(e.CellValue) == "EJECUTADO")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "EN PROCESO")
                { e.Appearance.BackColor = Color.FromArgb(255, 165, 0); }
            }

            if (e.Column.FieldName == "Reprog")
            {
                if(Convert.ToString(e.CellValue) == "0")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "1")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (Convert.ToString(e.CellValue) == "2")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgListaPendientes_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int idActividad = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(dgvListaPendientesVista.FocusedRowHandle, "NRO"));

                if (idActividad > 0)
                {
                    frmPendientesDiariosAgregar frmPendientesDiariosAgregar = new frmPendientesDiariosAgregar();
                    frmPendientesDiariosAgregar.txtResponsable.ReadOnly = true;
                    frmPendientesDiariosAgregar.txtDescripcion.ReadOnly = true;
                    frmPendientesDiariosAgregar.cbxNivel.Enabled = false;
                    frmPendientesDiariosAgregar.cbxEstado.Enabled = false;
                    frmPendientesDiariosAgregar.txtSeguimiento.ReadOnly = true;
                    frmPendientesDiariosAgregar.dtpFechaInicio.Enabled = false;
                    frmPendientesDiariosAgregar.dtpFechaP1.Enabled = false;
                    frmPendientesDiariosAgregar.dtpFechaP2.Enabled = false;
                    frmPendientesDiariosAgregar.dtpFechaP3.Enabled = false;
                    frmPendientesDiariosAgregar.btnCancelar.Enabled = false;
                    frmPendientesDiariosAgregar.btnAgregar.Enabled = false;
                    frmPendientesDiariosAgregar.Opcion = 0;
                    frmPendientesDiariosAgregar.RecibirDatos(idActividad);
                    frmPendientesDiariosAgregar.ShowDialog();
                }

            }
            catch { MessageBox.Show("La actividad seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaPendientes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idActividad = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(dgvListaPendientesVista.FocusedRowHandle, "NRO"));
                string Estado = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(dgvListaPendientesVista.FocusedRowHandle, "ESTADO"));

                if (idActividad != "")
                {
                    if (Estado == "EJECUTADO")
                    {
                        actualizarEstadoToolStripMenuItem.Enabled = false;
                        reprogramarActividadToolStripMenuItem.Enabled = false;
                        eliminarActividadToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { actualizarEstadoToolStripMenuItem.Enabled = true; }
                        if (e1 == 1) { reprogramarActividadToolStripMenuItem.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarActividadToolStripMenuItem.Enabled = true; }
                    }
                }
                else
                {
                    actualizarEstadoToolStripMenuItem.Enabled = false;
                    reprogramarActividadToolStripMenuItem.Enabled = false;
                    eliminarActividadToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                actualizarEstadoToolStripMenuItem.Enabled = false;
                reprogramarActividadToolStripMenuItem.Enabled = false;
                eliminarActividadToolStripMenuItem.Enabled = false;
            }
        }

        private void actualizarEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idActividad = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(dgvListaPendientesVista.FocusedRowHandle, "NRO"));

            frmPendientesDiariosAgregar frmPendientesDiariosAgregar = new frmPendientesDiariosAgregar();
            frmPendientesDiariosAgregar.txtResponsable.ReadOnly = false;
            frmPendientesDiariosAgregar.txtDescripcion.ReadOnly = true;
            frmPendientesDiariosAgregar.cbxNivel.Enabled = false;
            frmPendientesDiariosAgregar.dtpFechaInicio.Enabled = false;
            frmPendientesDiariosAgregar.dtpFechaP1.Enabled = false;
            frmPendientesDiariosAgregar.dtpFechaP2.Enabled = false;
            frmPendientesDiariosAgregar.dtpFechaP3.Enabled = false;
            frmPendientesDiariosAgregar.Opcion = 2;
            frmPendientesDiariosAgregar.RecibirDatos(idActividad);
            frmPendientesDiariosAgregar.formulario = this;
            frmPendientesDiariosAgregar.ShowDialog();
        }

        private void eliminarActividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar esta actividad?", "ELIMINAR ACTIVIDAD", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idActividad = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(dgvListaPendientesVista.FocusedRowHandle, "NRO"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                DataTable dtPendiente = clsOperacionesBL.Instancia.ReportesApp_Operaciones_PendientesDiarios_ModificarActividades(1, idActividad, 0, "", "", Usuario);
                string Respuesta = Convert.ToString(dtPendiente.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarPendientesDiarios(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void reprogramarActividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idActividad = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(dgvListaPendientesVista.FocusedRowHandle, "NRO"));
            int Contador = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(dgvListaPendientesVista.FocusedRowHandle, "Reprog"));

            if (Contador == 2) { MessageBox.Show("No puede reprogramar esta actividad más de 2 veces.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                frmPendientesDiariosAgregar frmPendientesDiariosAgregar = new frmPendientesDiariosAgregar();
                frmPendientesDiariosAgregar.txtResponsable.ReadOnly = true;
                frmPendientesDiariosAgregar.txtDescripcion.ReadOnly = true;
                frmPendientesDiariosAgregar.cbxNivel.Enabled = false;
                frmPendientesDiariosAgregar.cbxEstado.Enabled = false;
                frmPendientesDiariosAgregar.dtpFechaInicio.Enabled = false;

                if (Contador == 0)
                {
                    frmPendientesDiariosAgregar.dtpFechaP1.Enabled = false;
                    frmPendientesDiariosAgregar.label10.Visible = false;
                    frmPendientesDiariosAgregar.dtpFechaP3.Visible = false;
                }

                if (Contador == 1)
                {
                    frmPendientesDiariosAgregar.dtpFechaP1.Enabled = false;
                    frmPendientesDiariosAgregar.dtpFechaP2.Enabled = false;
                }

                frmPendientesDiariosAgregar.Opcion = 3;
                frmPendientesDiariosAgregar.Contador = Contador;
                frmPendientesDiariosAgregar.RecibirDatos(idActividad);
                frmPendientesDiariosAgregar.formulario = this;
                frmPendientesDiariosAgregar.ShowDialog();
            }
        }
    }
}
