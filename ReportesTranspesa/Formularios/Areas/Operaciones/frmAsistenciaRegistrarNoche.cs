using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using Entidades;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using DevExpress.Utils;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmAsistenciaRegistrarNoche : Form
    {
        public int xClick = 0, yClick = 0;
        DataTable dtPermisos = new DataTable();
        int e1 = 0, e2 = 0, e3 = 0;
        int NIDPersona, Opcion = 0;
        DateTime NFecha;

        public frmAsistenciaRegistrarNoche()
        {
            InitializeComponent();
        }

        private void frmAsistenciaRegistrarNoche_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPermisos4 = Utilitario.Instancia.ObtenerPermisosPorFormulario("Operaciones_Programacion_Previajes");
                DataTable dtEspeciales4 = null;
                if (dtPermisos4 != null)
                {
                    if (dtPermisos4.Rows[0]["PermisosEspeciales"].ToString() != "")
                    { dtEspeciales4 = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos4.Rows[0]["PermisosEspeciales"].ToString()); }
                }
                if (dtEspeciales4 != null)
                {
                    for (int i = 0; i < dtEspeciales4.Rows.Count; i++)
                    {
                        if (dtEspeciales4.Rows[i]["NombrePermiso"].ToString() == "Registrar y Eliminar Asistencias")
                        {
                            btnGuardar.Enabled = true;
                            eliminarNocheToolStripMenuItem.Enabled = true;
                            e1 = 1; i = 999;
                        }
                        else
                        {
                            btnGuardar.Enabled = false;
                            eliminarNocheToolStripMenuItem.Enabled = false;
                        }
                    }

                    for (int i = 0; i < dtEspeciales4.Rows.Count; i++)
                    {
                        if (dtEspeciales4.Rows[i]["NombrePermiso"].ToString() == "Autorizar Compensacion")
                        {
                            autorizarToolStripMenuItem.Enabled = true;
                            ingresarFCompToolStripMenuItem.Enabled = true;
                            e2 = 1; i = 999;
                        }
                        else
                        {
                            autorizarToolStripMenuItem.Enabled = false;
                            ingresarFCompToolStripMenuItem.Enabled = false;
                        }
                    }

                    for (int i = 0; i < dtEspeciales4.Rows.Count; i++)
                    {
                        if (dtEspeciales4.Rows[i]["NombrePermiso"].ToString() == "Ingresar Planillas")
                        {
                            ingresarPlanillaToolStripMenuItem.Enabled = true;
                            e3 = 1; i = 999;
                        }
                        else { ingresarPlanillaToolStripMenuItem.Enabled = false; }
                    }
                }
                else
                {
                    btnGuardar.Enabled = false;
                    eliminarNocheToolStripMenuItem.Enabled = false;
                    autorizarToolStripMenuItem.Enabled = false;
                    ingresarFCompToolStripMenuItem.Enabled = false;
                    ingresarPlanillaToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                btnGuardar.Enabled = false;
                eliminarNocheToolStripMenuItem.Enabled = false;
                autorizarToolStripMenuItem.Enabled = false;
                ingresarFCompToolStripMenuItem.Enabled = false;
                ingresarPlanillaToolStripMenuItem.Enabled = false;
            }

            comboBox1.SelectedIndex = 0;
            dtpFechaComp.Value = DateTime.Now;
        }


        private void cargaFechasDisponibles(int IDPersona, string FechaIni, string FechaFin)
        {
            DataTable dt;

            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasBuscarXCompensarNoche("10000000", "CD", IDPersona, FechaIni, FechaFin);

            if (dt.Rows.Count > 0)
            {
                dtgNoches.DataSource = dt;
                dgvNochesView.Columns["IDPersona"].Visible = false;

                dgvNochesView.BestFitColumns();
            }
            else
            {
                MessageBox.Show("No se encontraron noches los ultimos 30 días.", "Aviso");
                dtgNoches.DataSource = null;
            }
        }


        private void txtRelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtRelacion.Text.Length > 0)
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {
                    clsVisuales.Instancia.LlenarLw(lstFiltroRelacion, clsControlDocumentosBL.Instancia.getDocumento_BuscarRelacion(1, txtRelacion.Text), true, false, false);

                    lstFiltroRelacion.Columns[0].Width = 0;
                    lstFiltroRelacion.Columns[1].Width = 240;
                    lstFiltroRelacion.Columns[2].Width = 70;

                    lstFiltroRelacion.Size = new System.Drawing.Size(339, 103);

                    lstFiltroRelacion.BringToFront();
                    lstFiltroRelacion.Visible = true;

                    if (lstFiltroRelacion.Items.Count.Equals(0)) { txtRelacion.Focus(); }
                    else
                    {
                        lstFiltroRelacion.Focus();
                        lstFiltroRelacion.Items[0].Selected = true;
                    }
                }
            }
            else
            {
                lstFiltroRelacion.Visible = false;
                txtRelacion.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstFiltroRelacion.Visible = false;
                dtgNoches.DataSource = null;
                dgvNochesView.Columns.Clear();
            }
        }

        private void lstFiltroRelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstFiltroRelacion.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstFiltroRelacion.SelectedItems[0];

                    txtRelacion.Text = ItemActual.SubItems[1].Text;
                    txtRelacion.Tag = ItemActual.SubItems[0].Text;

                    lstFiltroRelacion.Visible = false;

                    DateTime hoy = DateTime.Now;
                    string MiFechaFinal = hoy.ToString("dd/MM/yyyy");
                    string MiFechaInicial = hoy.AddDays(-30).ToString("dd/MM/yyyy");
                    cargaFechasDisponibles(Convert.ToInt32(txtRelacion.Tag), MiFechaInicial, MiFechaFinal);
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstFiltroRelacion.Visible = false;
                    dtgNoches.DataSource = null;
                    dgvNochesView.Columns.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRelacion.Text.Length == 0)
            {
                MessageBox.Show("No ha ingresado ningún nombre de conductor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                if (txtObservaciones.Text.Length == 0)
                {
                    MessageBox.Show("Debes ingresar una Observación Obligatoriamente por ser 0.50 Noche.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistencias_Noches_Registrar(Convert.ToInt32(txtRelacion.Tag), dtpFecha.Text, comboBox1.Text, txtObservaciones.Text, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DateTime hoy = DateTime.Now;
                string MiFechaFinal = hoy.ToString("dd/MM/yyyy");
                string MiFechaInicial = hoy.AddDays(-30).ToString("dd/MM/yyyy");
                cargaFechasDisponibles(Convert.ToInt32(txtRelacion.Tag), MiFechaInicial, MiFechaFinal);
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;
            txtRelacion.Clear();
            txtRelacion.Tag = null;
            txtObservaciones.Clear();
            dtgNoches.DataSource = null;
            dgvNochesView.Columns.Clear();
        }

        private void dgvNochesView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "AP")
            {
                if (Convert.ToString(e.CellValue) == "OK")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void eliminarNocheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar esta fecha?", "ELIMINAR NOCHE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idPersona = Convert.ToInt32(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "IDPersona"));
                    DateTime Fecha = Convert.ToDateTime(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Fecha"));
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_AsistenciasNoche_EliminarNoche(idPersona, Fecha);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DateTime hoy = DateTime.Now;
                        string MiFechaFinal = hoy.ToString("dd/MM/yyyy");
                        string MiFechaInicial = hoy.AddDays(-30).ToString("dd/MM/yyyy");
                        cargaFechasDisponibles(Convert.ToInt32(txtRelacion.Tag), MiFechaInicial, MiFechaFinal);
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void autorizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idPersona = Convert.ToInt32(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "IDPersona"));
                DateTime Fecha = Convert.ToDateTime(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Fecha"));
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_AsistenciasNoche_CompensarNoche(1, idPersona, Fecha, "", Fecha, "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    //MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DateTime hoy = DateTime.Now;
                    string MiFechaFinal = hoy.ToString("dd/MM/yyyy");
                    string MiFechaInicial = hoy.AddDays(-30).ToString("dd/MM/yyyy");
                    cargaFechasDisponibles(Convert.ToInt32(txtRelacion.Tag), MiFechaInicial, MiFechaFinal);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ingresarPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            NIDPersona = Convert.ToInt32(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "IDPersona"));
            NFecha = Convert.ToDateTime(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Fecha"));

            DateTime txtFecha = Convert.ToDateTime(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Fecha"));
            lblTitulo.Text = "INGRESAR PLANILLA";
            lblFecha.Text = txtFecha.ToShortDateString();
            lblNoche.Text = Convert.ToString(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Noche"));

            dtpFechaComp.Enabled = false;
            txtCodGasto.Enabled = true;
            pCompensar.Visible = true;
            pCompensar.BringToFront();
        }

        private void ingresarFCompToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Opcion = 3;
            NIDPersona = Convert.ToInt32(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "IDPersona"));
            NFecha = Convert.ToDateTime(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Fecha"));

            DateTime txtFecha = Convert.ToDateTime(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Fecha"));
            lblTitulo.Text = "INGRESAR FECHA COMPENSADA";
            lblFecha.Text = txtFecha.ToShortDateString();
            lblNoche.Text = Convert.ToString(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Noche"));

            dtpFechaComp.Enabled = true;
            txtCodGasto.Enabled = false;
            pCompensar.Visible = true;
            pCompensar.BringToFront();
        }

        private void txtCodGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            clsVisuales.Instancia.LlenarLw(lstPlanillas, clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_Noche_ListarPlanillas(NIDPersona, txtCodGasto.Text), true, false, false);
            lstPlanillas.Columns[0].Width = 65;
            lstPlanillas.Columns[1].Width = 222;
            lstPlanillas.BringToFront();
            lstPlanillas.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlanillas.Visible = false;
                lstPlanillas.SendToBack();
                txtCodGasto.Focus();
            }
        }

        private void txtCodGasto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlanillas.Focus(); }
        }

        private void lstPlanillas_Enter(object sender, EventArgs e)
        { if (!lstPlanillas.Items.Count.Equals(0)) { lstPlanillas.Items[0].Selected = true; } }

        private void lstPlanillas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlanillas.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlanillas.SelectedItems[0];

                txtCodGasto.Text = ItemActual.SubItems[0].Text;

                lstPlanillas.Visible = false;
                lstPlanillas.SendToBack();
                btnActualizar.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlanillas.Visible = false;
                lstPlanillas.SendToBack();
                txtCodGasto.Focus();
            }
        }

        private void lstPlanillas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlanillas.SelectedItems[0];

            txtCodGasto.Text = ItemActual.SubItems[0].Text;

            lstPlanillas.Visible = false;
            lstPlanillas.SendToBack();
            btnActualizar.Focus();
        }

        private void dtgNoches_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idPersonaText = Convert.ToString(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "IDPersona"));
                string Autorizacion = Convert.ToString(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "AP"));
                string FechaComp = Convert.ToString(dgvNochesView.GetRowCellValue(dgvNochesView.FocusedRowHandle, "Compensado"));

                if (idPersonaText != "")
                {
                    if (Autorizacion == "OK")
                    {
                        if (e2 == 1) { autorizarToolStripMenuItem.Enabled = true; }
                        if (e3 == 1) { ingresarPlanillaToolStripMenuItem.Enabled = true; }
                        ingresarFCompToolStripMenuItem.Enabled = false;
                        eliminarNocheToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        if (FechaComp != "")
                        {
                            autorizarToolStripMenuItem.Enabled = false;
                            ingresarPlanillaToolStripMenuItem.Enabled = false;
                            if (e2 == 1) { ingresarFCompToolStripMenuItem.Enabled = true; }
                            eliminarNocheToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            if (e2 == 1)
                            {
                                autorizarToolStripMenuItem.Enabled = true;
                                ingresarFCompToolStripMenuItem.Enabled = true;
                            }
                            ingresarPlanillaToolStripMenuItem.Enabled = false;
                            if (e1 == 1) { eliminarNocheToolStripMenuItem.Enabled = true; }
                        }
                    }
                }
                else
                {
                    autorizarToolStripMenuItem.Enabled = false;
                    ingresarPlanillaToolStripMenuItem.Enabled = false;
                    ingresarFCompToolStripMenuItem.Enabled = false;
                    eliminarNocheToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                autorizarToolStripMenuItem.Enabled = false;
                ingresarPlanillaToolStripMenuItem.Enabled = false;
                ingresarFCompToolStripMenuItem.Enabled = false;
                eliminarNocheToolStripMenuItem.Enabled = false;
            }
        }

        private void pCompensar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pCompensar.Left = pCompensar.Left + (e.X - xClick);
                pCompensar.Top = pCompensar.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            NIDPersona = 0;
            Opcion = 0;
            NFecha = DateTime.MinValue;

            lblTitulo.Text = "";
            lblFecha.Text = "";
            lblNoche.Text = "";
            txtCodGasto.Clear();
            dtpFechaComp.Value = DateTime.Now;

            dtpFechaComp.Enabled = false;
            txtCodGasto.Enabled = false;
            pCompensar.Visible = false;
            pCompensar.SendToBack();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_AsistenciasNoche_CompensarNoche(Opcion, NIDPersona, NFecha, txtCodGasto.Text, dtpFechaComp.Value, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar_Click(sender, e);
                    DateTime hoy = DateTime.Now;
                    string MiFechaFinal = hoy.ToString("dd/MM/yyyy");
                    string MiFechaInicial = hoy.AddDays(-30).ToString("dd/MM/yyyy");
                    cargaFechasDisponibles(Convert.ToInt32(txtRelacion.Tag), MiFechaInicial, MiFechaFinal);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
