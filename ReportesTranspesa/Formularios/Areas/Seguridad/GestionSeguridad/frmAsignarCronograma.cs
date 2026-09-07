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
    public partial class frmAsignarCronograma : Form
    {
        DataTable dtPermisos = new DataTable();
        DataTable dtCronograma = new DataTable();
        public int idGestionActividad;
        public string Acceso, Validacion, Usuario;
        public frmGestionSeguridad formulario;
        public int xClick = 0, yClick = 0;

        public frmAsignarCronograma()
        {
            InitializeComponent();
        }

        private void frmAsignarResponsables_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmGestionSeguridad");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { groupBox1.Enabled = true; }
                else { groupBox1.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { modificarCronogramaToolStripMenuItem.Enabled = true; }
                else { modificarCronogramaToolStripMenuItem.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarCronogramaToolStripMenuItem.Enabled = true; }
                else { eliminarCronogramaToolStripMenuItem.Enabled = false; }
            }

            dtpFechaCronograma.Value = DateTime.Now;
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1).AddMonths(1);
            BuscarCronograma();
        }


        public void BuscarCronograma()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtCronograma = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarCronogramaActividades(idGestionActividad, dtpFechaInicio.Text, dtpFechaFin.Text, lblTipoCronograma.Text);
                dtgCronogramaActividad.DataSource = dtCronograma;
                if (dtCronograma.Rows.Count > 0)
                {
                    dgvCronogramaActividadVista.Columns["idCronograma"].Visible = false;
                    dgvCronogramaActividadVista.Columns["idGestionActividad"].Visible = false;

                    dgvCronogramaActividadVista.Columns["PORCENTAJE"].Summary.Clear();
                    dgvCronogramaActividadVista.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "PORCENTAJE", "Total = {0:N2}");

                    dgvCronogramaActividadVista.BestFitColumns();
                }
            }
        }


        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { BuscarCronograma(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { BuscarCronograma(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { BuscarCronograma(); }

        private void txtMeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (lblTipoCronograma.Text == "META TOTAL") { txtMeta2.Focus(); }
                else { btnCronograma_Click(sender, e); }
            }
        }

        private void txtMeta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnCronograma_Click(sender, e); }
        }

        private void btnCronograma_Click(object sender, EventArgs e)
        {
            if (txtMeta.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la meta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMeta.Focus();
                return;
            }
            else
            {
                if (Convert.ToDecimal(txtMeta2.Text) > Convert.ToDecimal(txtMeta.Text))
                {
                    MessageBox.Show("La Meta Aceptable no puede ser mayor a la Meta Excelente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMeta2.Focus();
                    return;
                }
                else
                {
                    DataTable dtAsignarCronograma = new DataTable();
                    int idCronograma = Convert.ToString(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "idCronograma")) == "" ? 0 : Convert.ToInt32(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "idCronograma"));
                    decimal MetaAceptable = txtMeta2.Text == "" ? 0.00M : Convert.ToDecimal(txtMeta2.Text);
                    string respta;

                    dtAsignarCronograma = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_AsignarCronograma(idCronograma, idGestionActividad, dtpFechaCronograma.Value, Convert.ToDecimal(txtMeta.Text), MetaAceptable, lblTipoCronograma.Text);
                    respta = Convert.ToString(dtAsignarCronograma.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        BuscarCronograma();
                        dtpFechaCronograma.Value = DateTime.Now;
                        txtMeta.Clear();
                        if (lblTipoCronograma.Text == "META TOTAL") { txtMeta2.Clear(); }
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void dtgCronogramaActividad_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idCronograma = Convert.ToString(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "idCronograma"));

                if (idCronograma != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { modificarCronogramaToolStripMenuItem.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarCronogramaToolStripMenuItem.Enabled = true; }

                    DataTable dtVerificarUsuario = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ValidarUsuario(idGestionActividad, Validacion, Usuario);
                    string respta = Convert.ToString(dtVerificarUsuario.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { registrarMetaToolStripMenuItem.Enabled = true; }
                    else { registrarMetaToolStripMenuItem.Enabled = false; }
                }
                else
                {
                    modificarCronogramaToolStripMenuItem.Enabled = false;
                    eliminarCronogramaToolStripMenuItem.Enabled = false;
                    registrarMetaToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                modificarCronogramaToolStripMenuItem.Enabled = false;
                eliminarCronogramaToolStripMenuItem.Enabled = false;
                registrarMetaToolStripMenuItem.Enabled = false;
            }
        }

        private void modificarCronogramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtpFechaCronograma.Value = Convert.ToDateTime(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "FECHA"));

            if (lblTipoCronograma.Text == "META TOTAL")
            {
                txtMeta.Text = Convert.ToString(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "M_EXCELENTE"));
                txtMeta2.Text = Convert.ToString(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "M_ACEPTABLE"));
            }
            else { txtMeta.Text = Convert.ToString(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "META")); }
        }

        private void eliminarCronogramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este cronograma?", "ELIMINAR CRONOGRAMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idCronograma = Convert.ToInt32(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "idCronograma"));
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ModificarCronograma(2, idGestionActividad, idCronograma, 0.00M, lblTipoCronograma.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        BuscarCronograma();
                        dtpFechaCronograma.Focus();
                        formulario.ListarGestionActividades();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El cronograma seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pAgregarPorcentaje_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pAgregarPorcentaje.Left = pAgregarPorcentaje.Left + (e.X - xClick);
                pAgregarPorcentaje.Top = pAgregarPorcentaje.Top + (e.Y - yClick);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pAgregarPorcentaje.Visible = false;
            pAgregarPorcentaje.SendToBack();
            dtpFechaMeta.Value = DateTime.Now;
            txtNuevaMeta.Clear();
        }

        private void registrarMetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtpFechaMeta.Value = Convert.ToDateTime(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "FECHA"));
            if (lblTipoCronograma.Text == "AVANCE") { txtNuevaMeta.Text = "100.00"; } 
            pAgregarPorcentaje.Visible = true;
            pAgregarPorcentaje.BringToFront();
        }

        private void txtNuevaMeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnActualizar_Click(sender, e); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtNuevaMeta.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la meta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevaMeta.Focus();
                return;
            }
            else
            {
                int idCronograma = Convert.ToInt32(dgvCronogramaActividadVista.GetRowCellValue(dgvCronogramaActividadVista.FocusedRowHandle, "idCronograma"));
                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ModificarCronograma(1, idGestionActividad, idCronograma, Convert.ToDecimal(txtNuevaMeta.Text), lblTipoCronograma.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    BuscarCronograma();
                    pictureBox3_Click(sender, e);
                    formulario.ListarGestionActividades();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvCronogramaActividadVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }

                if (e.CellValue.ToString() == "COMPLETADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }
    }
}
