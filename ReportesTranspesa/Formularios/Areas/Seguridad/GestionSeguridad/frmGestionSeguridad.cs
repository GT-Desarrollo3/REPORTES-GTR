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

namespace ReportesTranspesa.Formularios.Areas.Seguridad.GestionSeguridad
{
    public partial class frmGestionSeguridad : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaGestion = new DataTable();
        DataTable dtPermisos = new DataTable();
        public int idPersonal, idActividad, idPersonal2;
        public string Validacion;
        public int xClick = 0, yClick = 0;

        public frmGestionSeguridad()
        {
            InitializeComponent();
            cbxEstado.SelectedIndexChanged -= cbxEstado_SelectedIndexChanged;
        }

        private void cbxEstado_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void frmGestionSeguridad_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmGestionSeguridad");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    groupBox3.Enabled = true;
                    btnListaActividades.Enabled = true;
                }
                else
                {
                    groupBox3.Enabled = false;
                    btnListaActividades.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    btAsignar.Enabled = true;
                    txtPersonalNombre.Enabled = true;
                    txtPesoPersonal.Enabled = true;
                    cambiarPesoToolStripMenuItem.Enabled = true;
                    desvincularToolStripMenuItem.Enabled = true;
                }
                else
                {
                    btAsignar.Enabled = false;
                    txtPersonalNombre.Enabled = false;
                    txtPesoPersonal.Enabled = false;
                    cambiarPesoToolStripMenuItem.Enabled = false;
                    desvincularToolStripMenuItem.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarGestionToolStripMenuItem.Enabled = true; }
                else { eliminarGestionToolStripMenuItem.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            CargarComboArea();
            cbxTipoCronograma.Text = "DIARIA";
            rbLider.Checked = true;
            rbLider_CheckedChanged(sender, e);
            ListarGestionActividades();
        }


        public void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(3, "");
            cbxEstado.DataSource = dtArea;
            cbxEstado.DisplayMember = "description";
            cbxEstado.ValueMember = "department";
        }

        public void ListarGestionActividades()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaGestion = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarGestionActividades(txtBuscaActividad.Text, cbxEstado.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                dtgGestionSeguridad.DataSource = dtListaGestion;
                if (dtListaGestion.Rows.Count > 0)
                {
                    dgvGestionSeguridadVista.Columns["idActividad"].Visible = false;

                    dgvGestionSeguridadVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvGestionSeguridadVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvGestionSeguridadVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvGestionSeguridadVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                    dgvGestionSeguridadVista.BestFitColumns();
                }
            }
        }

        public void ListarResponsables(int GestionActividad)
        {
            DataTable dtListaResponsables = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonalResponsable(GestionActividad);
            dtgvListaResponsables.DataSource = null;
            dtgvListaResponsables.DataSource = dtListaResponsables;
            if (dtListaResponsables.Rows.Count > 0)
            {
                dtgvListaResponsablesView.Columns["idResponsable"].Visible = false;
                dtgvListaResponsablesView.Columns["idGestionActividad"].Visible = false;
                dtgvListaResponsablesView.Columns["Persona"].Visible = false;

                dtgvListaResponsablesView.Columns["PESO"].Summary.Clear();
                dtgvListaResponsablesView.Columns["PESO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PESO", "Total = {0:N2}");

                dtgvListaResponsablesView.BestFitColumns();
            }
        }


        private void txtBuscaActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGestionActividades(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarGestionActividades(); }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGestionActividades(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGestionActividades(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarGestionActividades(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgGestionSeguridad.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Gestion de Seguridad - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgGestionSeguridad.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnListaActividades_Click(object sender, EventArgs e)
        {
            frmMaestroObjetivos frmMaestroObjetivos = new frmMaestroObjetivos();
            frmMaestroObjetivos.ShowDialog(this);
        }

        private void txtPersonal_Enter(object sender, EventArgs e) { txtPersonal.BackColor = Color.FromArgb(255, 224, 192); }

        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(1, txtPersonal.Text), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 316;
            lstPersonal.Columns[2].Width = 0;
            lstPersonal.Columns[3].Width = 0;
            lstPersonal.Columns[4].Width = 0;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idPersonal = 0;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                txtArea.Clear();
                txtCargo.Clear();
                txtPersonal.Focus();
            }
        }

        private void txtPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void txtPersonal_Leave(object sender, EventArgs e) { txtPersonal.BackColor = Color.White; }

        private void lstPersonal_Enter(object sender, EventArgs e)
        { if (!lstPersonal.Items.Count.Equals(0)) { lstPersonal.Items[0].Selected = true; } }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal.SelectedItems[0];

                idPersonal = Int32.Parse(ItemActual.Text);
                txtPersonal.Text = ItemActual.SubItems[1].Text;
                txtArea.Text = ItemActual.SubItems[2].Text;
                txtCargo.Text = ItemActual.SubItems[3].Text;

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                txtActividad.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idPersonal = 0;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                txtArea.Clear();
                txtCargo.Clear();
                txtPersonal.Focus();
            }
        }

        private void lstPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal.SelectedItems[0];

            idPersonal = Int32.Parse(ItemActual.Text);
            txtPersonal.Text = ItemActual.SubItems[1].Text;
            txtArea.Text = ItemActual.SubItems[2].Text;
            txtCargo.Text = ItemActual.SubItems[3].Text;

            lstPersonal.Visible = false;
            lstPersonal.SendToBack();
            txtActividad.Focus();
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void txtActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstActividad, clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(2, txtActividad.Text), true, false, false);
            lstActividad.Columns[0].Width = 0;
            lstActividad.Columns[1].Width = 316;
            lstActividad.Columns[3].Width = 0;
            lstActividad.Columns[4].Width = 0;
            lstActividad.BringToFront();
            lstActividad.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idActividad = 0;
                lstActividad.Visible = false;
                lstActividad.SendToBack();
                txtActividad.Focus();
            }
        }

        private void txtActividad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstActividad.Focus(); }
        }

        private void lstActividad_Enter(object sender, EventArgs e)
        { if (!lstActividad.Items.Count.Equals(0)) { lstActividad.Items[0].Selected = true; } }

        private void lstActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstActividad.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstActividad.SelectedItems[0];

                idActividad = Int32.Parse(ItemActual.Text);
                txtActividad.Text = ItemActual.SubItems[1].Text;

                lstActividad.Visible = false;
                lstActividad.SendToBack();
                txtPeso.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idActividad = 0;
                lstActividad.Visible = false;
                lstActividad.SendToBack();
                txtActividad.Focus();
            }
        }

        private void lstActividad_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstActividad.SelectedItems[0];

            idActividad = Int32.Parse(ItemActual.Text);
            txtActividad.Text = ItemActual.SubItems[1].Text;

            lstActividad.Visible = false;
            lstActividad.SendToBack();
            txtPeso.Focus();
        }

        private void rbLider_CheckedChanged(object sender, EventArgs e) { Validacion = "LIDER"; }

        private void rbSSOMAC_CheckedChanged(object sender, EventArgs e) { Validacion = "SSOMAC"; }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            idPersonal = 0; idActividad = 0;
            txtPersonal.Clear();
            txtArea.Clear();
            txtCargo.Clear();
            txtActividad.Clear();
            txtPeso.Clear();
            rbLider.Checked = true;
            rbLider_CheckedChanged(sender, e);
            cbxTipoCronograma.Text = "DIARIA";
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (txtPersonal.Text.Length == 0 || txtActividad.Text.Length == 0 || txtPeso.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPersonal.Text.Length == 0) { txtPersonal.Focus(); }
                else
                {
                    if (txtActividad.Text.Length == 0) { txtActividad.Focus(); }
                    else { txtPeso.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtGestion = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtGestion = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_GenerarGestionActividades(idPersonal, idActividad, Convert.ToDecimal(txtPeso.Text), Validacion, cbxTipoCronograma.Text, Usuario);
                respta = Convert.ToString(dtGestion.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    ListarGestionActividades();
                    btCancelar_Click(sender, e);
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void eliminarGestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este reporte?", "ELIMINAR REPORTE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idGestionActividad = Convert.ToInt32(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "#"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades(4, idGestionActividad);
                    ListarGestionActividades();
                }
            }
            catch { MessageBox.Show("El reporte seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void gestionarCronogramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idGestionActividad = Convert.ToInt32(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "#"));
            string Validacion = Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "VALIDACION"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            frmAsignarCronograma frmAsignarCronograma = new frmAsignarCronograma();
            frmAsignarCronograma.lblActividad.Text = Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "ACTIVIDAD"));
            frmAsignarCronograma.lblTipoCronograma.Text = Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "CRONOGRAMA"));
            
            if (Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "CRONOGRAMA")) == "AVANCE")
            {
                frmAsignarCronograma.label5.Text = "Nro.:";
                frmAsignarCronograma.label10.Visible = false;
                frmAsignarCronograma.txtMeta2.Visible = false;
                frmAsignarCronograma.txtMeta2.Text = "0.00";
            }
            
            if (Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "CRONOGRAMA")) == "DIARIA")
            {
                frmAsignarCronograma.label5.Text = "Meta:";
                frmAsignarCronograma.label10.Visible = false;
                frmAsignarCronograma.txtMeta2.Visible = false;
                frmAsignarCronograma.txtMeta2.Text = "0.00";
            }

            if (Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "CRONOGRAMA")) == "META TOTAL")
            {
                frmAsignarCronograma.label5.Text = "M. Exc:";
                frmAsignarCronograma.label10.Visible = true;
                frmAsignarCronograma.txtMeta2.Visible = true;
            }
            
            frmAsignarCronograma.idGestionActividad = idGestionActividad;
            frmAsignarCronograma.Validacion = Validacion;
            frmAsignarCronograma.Usuario = Usuario;
            frmAsignarCronograma.formulario = this;
            frmAsignarCronograma.ShowDialog(this);
        }

        private void asignarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pAsignarResponsable.Visible = true;
            pAsignarResponsable.BringToFront();

            lblGestion.Text = Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "#"));
            lblActividad.Text = Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "ACTIVIDAD"));
            ListarResponsables(Convert.ToInt32(lblGestion.Text));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pAsignarResponsable.Visible = false;
            pAsignarResponsable.SendToBack();
            lstPersonal2.Visible = false;
            lstPersonal2.SendToBack();
            idPersonal2 = 0;
            txtPersonalNombre.Clear();
            txtPesoPersonal.Clear();
            ListarGestionActividades();
            lblGestion.Text = "0";
        }

        private void pAsignarResponsable_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pAsignarResponsable.Left = pAsignarResponsable.Left + (e.X - xClick);
                pAsignarResponsable.Top = pAsignarResponsable.Top + (e.Y - yClick);
            }
        }

        private void txtPersonalNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal2, clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(1, txtPersonalNombre.Text), true, false, false);
            lstPersonal2.Columns[0].Width = 0;
            lstPersonal2.Columns[1].Width = 316;
            lstPersonal2.Columns[2].Width = 0;
            lstPersonal2.Columns[3].Width = 0;
            lstPersonal2.Columns[4].Width = 0;
            lstPersonal2.BringToFront();
            lstPersonal2.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idPersonal2 = 0;
                lstPersonal2.Visible = false;
                lstPersonal2.SendToBack();
            }
        }

        private void txtPersonalNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal2.Focus(); }
        }

        private void lstPersonal2_Enter(object sender, EventArgs e)
        { if (!lstPersonal2.Items.Count.Equals(0)) { lstPersonal2.Items[0].Selected = true; } }

        private void lstPersonal2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal2.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal2.SelectedItems[0];

                idPersonal2 = Int32.Parse(ItemActual.Text);
                txtPersonalNombre.Text = ItemActual.SubItems[1].Text;

                lstPersonal2.Visible = false;
                lstPersonal2.SendToBack();
                txtPesoPersonal.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idPersonal2 = 0;
                lstPersonal2.Visible = false;
                lstPersonal2.SendToBack();
            }
        }

        private void lstPersonal2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal2.SelectedItems[0];

            idPersonal2 = Int32.Parse(ItemActual.Text);
            txtPersonalNombre.Text = ItemActual.SubItems[1].Text;

            lstPersonal2.Visible = false;
            lstPersonal2.SendToBack();
            txtPesoPersonal.Focus();
        }

        private void txtPesoPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            { btAsignar_Click(sender, e); }
        }

        private void btAsignar_Click(object sender, EventArgs e)
        {
            if (txtPersonalNombre.Text.Length == 0 || txtPesoPersonal.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPersonalNombre.Text.Length == 0) { txtPersonalNombre.Focus(); }
                else { txtPesoPersonal.Focus(); }                
                return;
            }
            else
            {
                DataTable dtAsignarResponsables = new DataTable();
                string respta;
                int idGestionActividad = Convert.ToInt32(lblGestion.Text);

                dtAsignarResponsables = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_AsignarPersonalResponsable(idGestionActividad, idPersonal2, Convert.ToDecimal(txtPesoPersonal.Text));
                respta = Convert.ToString(dtAsignarResponsables.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    ListarResponsables(idGestionActividad);
                    idPersonal2 = 0;
                    txtPersonalNombre.Clear();
                    txtPesoPersonal.Clear();
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void cambiarPesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idResponsable = Convert.ToInt32(dtgvListaResponsablesView.GetRowCellValue(dtgvListaResponsablesView.FocusedRowHandle, "idResponsable"));
            int idGestionActividad = Convert.ToInt32(lblGestion.Text);

            DataTable dtResponsable = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_BuscarPersonalResponsable(1, idResponsable, idGestionActividad);
            
            if (dtResponsable.Rows.Count > 0)
            {
                idPersonal2 = Convert.ToInt32(dtResponsable.Rows[0]["Persona"]);
                txtPersonalNombre.Text = Convert.ToString(dtResponsable.Rows[0]["PERSONAL_RESPONSABLE"]);
                txtPesoPersonal.Text = Convert.ToString(dtResponsable.Rows[0]["Peso"]);
            }
        }

        private void desvincularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea desvincular a esta persona?", "DESVINCULAR RESPONSABLE", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idResponsable = Convert.ToInt32(dtgvListaResponsablesView.GetRowCellValue(dtgvListaResponsablesView.FocusedRowHandle, "idResponsable"));
                int idGestionActividad = Convert.ToInt32(lblGestion.Text);

                DataTable dtResponsable = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_BuscarPersonalResponsable(2, idResponsable, idGestionActividad);

                if (dtResponsable.Rows.Count > 0)
                {
                    ListarResponsables(idGestionActividad);
                    ListarGestionActividades();
                }
                else { MessageBox.Show("No puede eliminar al líder de la actividad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgGestionSeguridad_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idGestionActividad = Convert.ToString(dgvGestionSeguridadVista.GetRowCellValue(dgvGestionSeguridadVista.FocusedRowHandle, "#"));

                if (idGestionActividad != "")
                {
                    asignarPersonalToolStripMenuItem.Enabled = true;
                    gestionarCronogramaToolStripMenuItem.Enabled = true;
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarGestionToolStripMenuItem.Enabled = true; }
                }
                else
                {
                    asignarPersonalToolStripMenuItem.Enabled = false;
                    gestionarCronogramaToolStripMenuItem.Enabled = false;
                    eliminarGestionToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                asignarPersonalToolStripMenuItem.Enabled = false;
                gestionarCronogramaToolStripMenuItem.Enabled = false;
                eliminarGestionToolStripMenuItem.Enabled = false;
            }
        }
    }
}
