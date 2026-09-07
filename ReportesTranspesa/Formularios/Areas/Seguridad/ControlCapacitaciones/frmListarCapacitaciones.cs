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

namespace ReportesTranspesa.Formularios.Areas.Seguridad.ControlCapacitaciones
{
    public partial class frmListarCapacitaciones : MetroFramework.Forms.MetroForm
    {
        public int xClick = 0, yClick = 0;
        public int Persona, idGrupo, BuscarFecha, Faltantes;
        public int idPersonal, idTitulo;
        public string Area = "TODOS";
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();

        public frmListarCapacitaciones()
        {
            InitializeComponent();
            cbxGrupo.SelectedIndexChanged -= cbxGrupo_SelectedIndexChanged;
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
        }

        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboGrupo();
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboOperacion();
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboArea();
        }

        private void frmRegistrarCapacitacion_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarCapacitaciones");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        btnAgregar.Enabled = true;
                        btnAsignarGrupo.Enabled = true;
                    }
                    else
                    {
                        btnAgregar.Enabled = false;
                        btnAsignarGrupo.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarToolStripMenuItem.Enabled = true; }
                    else { eliminarToolStripMenuItem.Enabled = false; }
                }

                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                }

                if (dtEspeciales != null)
                {
                    if (dtEspeciales.Rows.Count > 0)
                    {
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Todos") { Area = "TODOS"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Almacenes") { Area = "ALM"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Contabilidad") { Area = "CON"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "RRHH") { Area = "GTH"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Mantenimiento") { Area = "MAN"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Lindley") { Area = "LND"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Limagas") { Area = "LMG"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Tolvas") { Area = "TLV"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Finanzas") { Area = "FNZ"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Logistica") { Area = "LOG"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Combustible") { Area = "COM"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "TI") { Area = "TI"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Legal") { Area = "LGL"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Aduanas") { Area = "ADU"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Produccion") { Area = "PD"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Operaciones") { Area = "OP"; }
                    }
                }
            }

            pRegistrarGrupos.Visible = false;
            pRegistrarGrupos.SendToBack();
            cbFecha.Checked = true;
            cbFecha_CheckedChanged(sender, e);
            rbAptos.Checked = true;
            rbAptos_Click(sender, e);
            CargarComboGrupo();
            CargarComboArea();
            cbxGrupo_DropDownClosed(sender, e);
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaProg.Value = DateTime.Now;
            dtpHoraProg.Value = new DateTime(dtpHoraProg.Value.Year, dtpHoraProg.Value.Month, 1, 0, 0, 0);
        }


        private void CargarComboGrupo()
        {
            DataTable dtGrupo = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos(1, 0);
            cbxGrupo.DataSource = dtGrupo;
            cbxGrupo.DisplayMember = "Descripcion";
            cbxGrupo.ValueMember = "idGrupo";
        }

        private void CargarComboOperacion()
        {
            DataTable dtOperacion = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos(2, idGrupo);
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "idOperacion";
        }

        private void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasPermisos(Area);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "Descripcion";
            cbxArea.ValueMember = "idOperacion";
        }

        public void ListarCapacitaciones()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtgListaCapacitaciones.DataSource = null;
                dgvListaCapacitacionesVista.Columns.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarCapacitaciones(txtCapacitacion.Text, txtInstructor.Text, txtAsistente.Text, dtpFechaIni.Text, dtpFechaFin.Text, BuscarFecha, cbxArea.Text, Faltantes);
                if (dt.Rows.Count > 0)
                {
                    dtgListaCapacitaciones.DataSource = dt;

                    if (rbFaltantes.Checked == true)
                    {
                        dgvListaCapacitacionesVista.Columns["idTitulo"].Visible = false;
                        dgvListaCapacitacionesVista.Columns["idPersona"].Visible = false;

                        dgvListaCapacitacionesVista.Columns["FechaProgramacion"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvListaCapacitacionesVista.Columns["FechaProgramacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    }

                    dgvListaCapacitacionesVista.BestFitColumns();
                }
            }
        }

        private void btnProgramar_Click(object sender, EventArgs e)
        {
            frmProgramarCapacitaciones frmProgramarCapacitaciones = new frmProgramarCapacitaciones();
            frmProgramarCapacitaciones.Operacion = cbxArea.Text;
            frmProgramarCapacitaciones.dtgListaAreas.Visible = true;
            frmProgramarCapacitaciones.dtgListaAreas.BringToFront();
            frmProgramarCapacitaciones.TipoProg = 1;
            frmProgramarCapacitaciones.ListarProgramaciones();
            frmProgramarCapacitaciones.CargarAreas();
            frmProgramarCapacitaciones.ShowDialog();
        }

        private void btnProgramacionEspecifica_Click(object sender, EventArgs e)
        {
            frmProgramarCapacitaciones frmProgramarCapacitaciones = new frmProgramarCapacitaciones();
            frmProgramarCapacitaciones.dtgListaPersonal.Visible = true;
            frmProgramarCapacitaciones.dtgListaPersonal.BringToFront();
            frmProgramarCapacitaciones.TipoProg = 2;
            frmProgramarCapacitaciones.ListarProgramaciones();
            frmProgramarCapacitaciones.CargarPersonal();
            frmProgramarCapacitaciones.ShowDialog();
        }

        private void btnAsignarGrupo_Click(object sender, EventArgs e)
        {
            pRegistrarGrupos.Visible = true;
            pRegistrarGrupos.BringToFront();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmRegistrarCapacitaciones frmRegistrarCapacitaciones = new frmRegistrarCapacitaciones();
            frmRegistrarCapacitaciones.btnRegistrarAsistente.Enabled = false;
            frmRegistrarCapacitaciones._formulario = this;
            frmRegistrarCapacitaciones._idCapacitacion = 0;
            frmRegistrarCapacitaciones.CargarComboTipos();
            frmRegistrarCapacitaciones.CargarComboLugar();
            frmRegistrarCapacitaciones.opcion = 1;
            frmRegistrarCapacitaciones.dtpFecha.Value = DateTime.Now;
            frmRegistrarCapacitaciones.ShowDialog();
        }

        private void txtPersona_Enter(object sender, EventArgs e)
        {
            txtPersona.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(txtPersona.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 400;
            lstPersona.Columns[2].Width = 0;
            lstPersona.Columns[3].Width = 0;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                Persona = 0;
            }
        }

        private void txtPersona_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersona.Focus(); }
        }

        private void txtPersona_Leave(object sender, EventArgs e) { txtPersona.BackColor = Color.White; }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0)) { lstPersona.Items[0].Selected = true; }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersona.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersona.SelectedItems[0];
                Persona = Int32.Parse(ItemActual.Text);
                txtPersona.Text = ItemActual.SubItems[1].Text;
                lstPersona.Visible = false;
                cbxGrupo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                txtPersona.Focus();
            }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersona.SelectedItems[0];
            Persona = Int32.Parse(ItemActual.Text);
            txtPersona.Text = ItemActual.SubItems[1].Text;
            lstPersona.Visible = false;
            cbxGrupo.Focus();
        }

        private void cbxGrupo_DropDownClosed(object sender, EventArgs e)
        {
            idGrupo = Convert.ToInt32(cbxGrupo.SelectedValue);
            CargarComboOperacion();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pRegistrarGrupos.Visible = false;
            pRegistrarGrupos.SendToBack();
            txtPersona.Clear();
            Persona = 0;
            CargarComboGrupo();
            cbxGrupo_DropDownClosed(sender, e);
        }

        private void pRegistrarGrupos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pRegistrarGrupos.Left = pRegistrarGrupos.Left + (e.X - xClick);
                pRegistrarGrupos.Top = pRegistrarGrupos.Top + (e.Y - yClick);
            }
        }

        private void txtCapacitacion_Enter(object sender, EventArgs e) { txtCapacitacion.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCapacitacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTitulos, clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_FiltrarTemas(txtCapacitacion.Text), true, false, false);
            if (lstTitulos.Columns.Count > 0)
            {
                lstTitulos.Columns[0].Width = 0;
                lstTitulos.Columns[1].Width = 400;
            }
            lstTitulos.BringToFront();
            lstTitulos.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTitulos.Visible = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarCapacitaciones();
            }
        }

        private void txtCapacitacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lstTitulos.Focus();
            }
        }

        private void txtCapacitacion_Leave(object sender, EventArgs e)
        {
            txtCapacitacion.BackColor = Color.White;
        }

        private void lstTitulos_Enter(object sender, EventArgs e)
        {
            if (!lstTitulos.Items.Count.Equals(0))
            {
                lstTitulos.Items[0].Selected = true;
            }
        }

        private void lstTitulos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTitulos.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTitulos.SelectedItems[0];
                txtCapacitacion.Text = ItemActual.SubItems[1].Text;
                ListarCapacitaciones();
                lstTitulos.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTitulos.Visible = false;
                txtCapacitacion.Focus();
            }
        }

        private void lstTitulos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTitulos.SelectedItems[0];
            txtCapacitacion.Text = ItemActual.SubItems[1].Text;
            ListarCapacitaciones();
            lstTitulos.Visible = false;
        }

        private void txtInstructor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarCapacitaciones();
            }
        }

        private void txtAsistente_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstAsistente, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(txtAsistente.Text), true, false, false);
            lstAsistente.Columns[0].Width = 0;
            lstAsistente.Columns[1].Width = 400;
            lstAsistente.Columns[2].Width = 0;
            lstAsistente.Columns[3].Width = 0;
            lstAsistente.BringToFront();
            lstAsistente.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstAsistente.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                ListarCapacitaciones();
            }
        }

        private void txtAsistente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lstAsistente.Focus();
            }
        }

        private void lstAsistente_Enter(object sender, EventArgs e)
        {
            if (!lstAsistente.Items.Count.Equals(0))
            {
                lstAsistente.Items[0].Selected = true;
            }
        }

        private void lstAsistente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstAsistente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstAsistente.SelectedItems[0];
                txtAsistente.Text = ItemActual.SubItems[1].Text;
                ListarCapacitaciones();
                lstAsistente.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstAsistente.Visible = false;
                txtAsistente.Focus();
            }
        }

        private void lstAsistente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstAsistente.SelectedItems[0];
            txtAsistente.Text = ItemActual.SubItems[1].Text;
            ListarCapacitaciones();
            lstAsistente.Visible = false;

        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarCapacitaciones();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarCapacitaciones();
            }
        }

        private void cbFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFecha.Checked == true)
            {
                groupBox1.Enabled = true;
                BuscarFecha = 1;
            }

            if (cbFecha.Checked == false)
            {
                groupBox1.Enabled = false;
                BuscarFecha = 0;
            }
        }

        private void rbAptos_Click(object sender, EventArgs e)
        {
            if (rbAptos.Checked == true)
            {
                rbAptos.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                rbFaltantes.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                programarFechaToolStripMenuItem.Enabled = false;
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarToolStripMenuItem.Enabled = true; }
                Faltantes = 0;
            }
        }

        private void rbFaltantes_Click(object sender, EventArgs e)
        {
            if (rbFaltantes.Checked == true)
            {
                rbAptos.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                rbFaltantes.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                programarFechaToolStripMenuItem.Enabled = true;
                eliminarToolStripMenuItem.Enabled = false;
                Faltantes = 1;
            }
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e)
        {
            ListarCapacitaciones();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtPersona.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPersona.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_AsignarGrupos(Persona, Convert.ToInt32(cbxGrupo.SelectedValue), Convert.ToInt32(cbxOperacion.SelectedValue));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPersona.Clear();
                    Persona = 0;
                    CargarComboGrupo();
                    cbxGrupo_DropDownClosed(sender, e);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvListaCapacitacionesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "Condicion")
            {
                if (e.CellValue.ToString() == "APTO")
                {
                    e.Appearance.BackColor = Color.FromArgb(31, 255, 0);
                }

                if (e.CellValue.ToString() == "REPROGRAMACION")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                }

                if (e.CellValue.ToString() == "SIN EVALUAR")
                {
                    e.Appearance.BackColor = Color.FromArgb(0, 213, 255);
                }
            }
        }

        private void dtgListaCapacitaciones_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idCapacitaciones = Convert.ToString(dgvListaCapacitacionesVista.GetRowCellValue(dgvListaCapacitacionesVista.FocusedRowHandle, "Codigo"));

                if (idCapacitaciones != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarToolStripMenuItem.Enabled = true; }
                }
                else
                {
                    eliminarToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                eliminarToolStripMenuItem.Enabled = false;
            }
        }

        private void programarFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idPersonal = Convert.ToInt32(dgvListaCapacitacionesVista.GetRowCellValue(dgvListaCapacitacionesVista.FocusedRowHandle, "idPersona"));
            idTitulo = Convert.ToInt32(dgvListaCapacitacionesVista.GetRowCellValue(dgvListaCapacitacionesVista.FocusedRowHandle, "idTitulo"));

            lblCurso.Text = Convert.ToString(dgvListaCapacitacionesVista.GetRowCellValue(dgvListaCapacitacionesVista.FocusedRowHandle, "Titulo"));
            lblNombre.Text = Convert.ToString(dgvListaCapacitacionesVista.GetRowCellValue(dgvListaCapacitacionesVista.FocusedRowHandle, "NombreCompleto"));

            pProgramacion.Visible = true;
            pProgramacion.BringToFront();
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pProgramacion.Visible = false;
            pProgramacion.SendToBack();
            dtpFechaProg.Value = DateTime.Now;
            dtpHoraProg.Value = new DateTime(dtpHoraProg.Value.Year, dtpHoraProg.Value.Month, 1, 0, 0, 0);
        }

        private void btnProgramarFecha_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string FechaProgramada = dtpFechaProg.Text + ' ' + dtpHoraProg.Text;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            string Respuesta;

            dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ProgramarFechas(idPersonal, idTitulo, Convert.ToDateTime(FechaProgramada), Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pProgramacion.Visible = false;
                pProgramacion.SendToBack();
                dtpFechaProg.Value = DateTime.Now;
                dtpHoraProg.Value = new DateTime(dtpHoraProg.Value.Year, dtpHoraProg.Value.Month, 1, 0, 0, 0);
                ListarCapacitaciones();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pProgramacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pProgramacion.Left = pProgramacion.Left + (e.X - xClick);
                pProgramacion.Top = pProgramacion.Top + (e.Y - yClick);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idCapacitacion = Convert.ToInt32(dgvListaCapacitacionesVista.GetRowCellValue(dgvListaCapacitacionesVista.FocusedRowHandle, "Codigo"));
                if (MessageBox.Show("¿Desea eliminar esta capacitación de forma permanente?", "ELIMINAR CAPACITACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_EliminarCapacitacion(idCapacitacion);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarCapacitaciones();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgListaCapacitaciones_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int idCapacitaciones = Convert.ToInt32(dgvListaCapacitacionesVista.GetRowCellValue(dgvListaCapacitacionesVista.FocusedRowHandle, "Codigo"));
                if (idCapacitaciones >= 1)
                {
                    frmRegistrarCapacitaciones frmRegistrarCapacitaciones = new frmRegistrarCapacitaciones();
                    frmRegistrarCapacitaciones.CargarComboTipos();
                    frmRegistrarCapacitaciones.CargarComboLugar();
                    frmRegistrarCapacitaciones.RecibirDatos(idCapacitaciones, this);
                    frmRegistrarCapacitaciones.opcion = 2;
                    frmRegistrarCapacitaciones.txtTitulo.ReadOnly = true;
                    frmRegistrarCapacitaciones.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarCapacitaciones();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaCapacitaciones.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Capacitaciones SSOMA - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaCapacitaciones.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
