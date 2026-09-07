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
    public partial class frmRegistrarCapacitaciones : Form
    {
        public int xClick = 0, yClick = 0;
        public int _idCapacitacion;
        public frmListarCapacitaciones _formulario;
        public int personal, idGrupo, idOperacion;
        public int idAsistente, opcion, idLugar, idProgramacion, idDetalle;
        DataTable dtCapacitacion, dtPermisos;

        public frmRegistrarCapacitaciones()
        {
            InitializeComponent();
            cbxTipo.SelectedIndexChanged -= cbxTipo_SelectedIndexChanged;
            cbxLugar.SelectedIndexChanged -= cbxLugar_SelectedIndexChanged;
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboTipos();
        }

        private void cbxLugar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboLugar();
        }

        private void frmRegistrarCapacitaciones_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarCapacitaciones");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    btnGuardar.Enabled = true;
                    if (opcion == 2) { btnRegistrarAsistente.Enabled = true; }
                }
                else
                {
                    btnGuardar.Enabled = false;
                    btnRegistrarAsistente.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                {
                    eliminarToolStripMenuItem.Enabled = true;
                }
                else { eliminarToolStripMenuItem.Enabled = false; }
            }

            pRegistrarAsistente.Visible = false;
            ListarAsistentes();
        }


        public void CargarComboTipos()
        {
            DataTable dtTipos = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos(3, 0);
            cbxTipo.DataSource = dtTipos;
            cbxTipo.DisplayMember = "Descripcion";
            cbxTipo.ValueMember = "idTipo";
        }

        public void CargarComboLugar()
        {
            DataTable dtLugar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos(4, 0);
            cbxLugar.DataSource = dtLugar;
            cbxLugar.DisplayMember = "Descripcion";
            cbxLugar.ValueMember = "idLugar";
        }

        public void RecibirDatos(int idCapacitacion, frmListarCapacitaciones formulario)
        {
            _idCapacitacion = idCapacitacion;
            _formulario = formulario;

            dtCapacitacion = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_FiltrarCapacitacion(_idCapacitacion);
            if (dtCapacitacion.Rows.Count > 0)
            {
                txtTitulo.Text = dtCapacitacion.Rows[0]["Titulo"].ToString();
                cbxTipo.Text = dtCapacitacion.Rows[0]["Descripcion"].ToString();
                dtpFecha.Value = Convert.ToDateTime(dtCapacitacion.Rows[0]["Fecha"]);
                txtHoras.Text = dtCapacitacion.Rows[0]["Horas"].ToString();
                txtInstructor.Text = dtCapacitacion.Rows[0]["NombreCompleto"].ToString();
                cbxLugar.Text = dtCapacitacion.Rows[0]["Lugar"].ToString();
            }
        }

        public void ListarAsistentes()
        {
            dtgvListaAsistentes.DataSource = null;
            dtgvListaAsistentesView.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAsistentes(_idCapacitacion);
            if (dt.Rows.Count > 0)
            {
                dtgvListaAsistentes.DataSource = dt;
                dtgvListaAsistentesView.Columns["FechaRegistra"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaAsistentesView.Columns["FechaRegistra"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dtgvListaAsistentesView.BestFitColumns();
            }
        }


        private void txtTitulo_Enter(object sender, EventArgs e)
        {
            txtTitulo.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTemas, clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_FiltrarProgramaciones(txtTitulo.Text), true, false, false);
            if (lstTemas.Columns.Count > 0)
            {
                lstTemas.Columns[0].Width = 0;
                lstTemas.Columns[1].Width = 400;
            }
            lstTemas.BringToFront();
            lstTemas.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTemas.Visible = false;
                idProgramacion = 0;
            }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbxTipo.Focus();
            }
        }

        private void txtTitulo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lstTemas.Focus();
            }
        }

        private void txtTitulo_Leave(object sender, EventArgs e)
        {
            txtTitulo.BackColor = Color.White;
        }

        private void lstTemas_Enter(object sender, EventArgs e)
        {
            if (!lstTemas.Items.Count.Equals(0))
            {
                lstTemas.Items[0].Selected = true;
            }
        }

        private void lstTemas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTemas.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTemas.SelectedItems[0];
                idProgramacion = Int32.Parse(ItemActual.Text);
                txtTitulo.Text = ItemActual.SubItems[1].Text;
                lstTemas.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTemas.Visible = false;
                txtTitulo.Focus();
            }
        }

        private void lstTemas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTemas.SelectedItems[0];
            idProgramacion = Int32.Parse(ItemActual.Text);
            txtTitulo.Text = ItemActual.SubItems[1].Text;
            lstTemas.Visible = false;
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtHoras.Focus();
            }
        }

        private void txtHoras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtInstructor.Focus();
            }
        }

        private void txtInstructor_Enter(object sender, EventArgs e)
        {
            txtInstructor.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtInstructor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstInstructor, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(txtInstructor.Text), true, false, false);
            lstInstructor.Columns[0].Width = 0;
            lstInstructor.Columns[1].Width = 400;
            lstInstructor.Columns[2].Width = 0;
            lstInstructor.Columns[3].Width = 0;
            lstInstructor.BringToFront();
            lstInstructor.Visible = true;

            if (e.KeyChar == (char)Keys.Back) { lstInstructor.Visible = false; }
        }

        private void txtInstructor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lstInstructor.Focus();
            }
        }

        private void txtInstructor_Leave(object sender, EventArgs e)
        {
            txtInstructor.BackColor = Color.White;
        }

        private void lstInstructor_Enter(object sender, EventArgs e)
        {
            if (!lstInstructor.Items.Count.Equals(0))
            {
                lstInstructor.Items[0].Selected = true;
            }
        }

        private void lstInstructor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstInstructor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstInstructor.SelectedItems[0];
                txtInstructor.Text = ItemActual.SubItems[1].Text;
                lstInstructor.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstInstructor.Visible = false;
                txtInstructor.Focus();
            }
        }

        private void lstInstructor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstInstructor.SelectedItems[0];
            txtInstructor.Text = ItemActual.SubItems[1].Text;
            lstInstructor.Visible = false;
        }

        private void btnRegistrarAsistente_Click(object sender, EventArgs e)
        {
            pRegistrarAsistente.Visible = true;
            personal = 0;
            idGrupo = 0;
            idOperacion = 0;
            txtPersona.Clear();
            txtDNI.Clear();
            txtCargo.Clear();
            txtGrupo.Clear();
            txtOperacion.Clear();
            txtNota.Clear();
            txtCondicion.Clear();
            txtCondicion.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void txtPersona_Enter(object sender, EventArgs e)
        {
            txtPersona.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarPersonal(txtPersona.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 400;
            lstPersona.Columns[2].Width = 0;
            lstPersona.Columns[3].Width = 0;
            lstPersona.Columns[4].Width = 0;
            lstPersona.Columns[5].Width = 0;
            lstPersona.Columns[6].Width = 0;
            lstPersona.Columns[7].Width = 0;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                txtPersona.Focus();
                txtDNI.Clear();
                txtGrupo.Clear();
                txtOperacion.Clear();
                txtCargo.Clear();
                personal = 0;
                idGrupo = 0;
                idOperacion = 0;
            }
        }

        private void txtPersona_Leave(object sender, EventArgs e)
        {
            txtPersona.BackColor = Color.White;
        }

        private void txtPersona_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lstPersona.Focus();
            }
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0))
            {
                lstPersona.Items[0].Selected = true;
            }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersona.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersona.SelectedItems[0];
                personal = Int32.Parse(ItemActual.Text);
                txtPersona.Text = ItemActual.SubItems[1].Text;
                txtDNI.Text = ItemActual.SubItems[2].Text;
                idGrupo = Int32.Parse(ItemActual.SubItems[3].Text);
                txtGrupo.Text = ItemActual.SubItems[4].Text;
                idOperacion = Int32.Parse(ItemActual.SubItems[5].Text);
                txtOperacion.Text = ItemActual.SubItems[6].Text;
                txtCargo.Text = ItemActual.SubItems[7].Text;
                lstPersona.Visible = false;
                txtNota.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                txtPersona.Focus();
                txtDNI.Clear();
                txtGrupo.Clear();
                txtOperacion.Clear();
                txtCargo.Clear();
                personal = 0;
                idGrupo = 0;
                idOperacion = 0;
            }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersona.SelectedItems[0];
            personal = Int32.Parse(ItemActual.Text);
            txtPersona.Text = ItemActual.SubItems[1].Text;
            txtDNI.Text = ItemActual.SubItems[2].Text;
            idGrupo = Int32.Parse(ItemActual.SubItems[3].Text);
            txtGrupo.Text = ItemActual.SubItems[4].Text;
            idOperacion = Int32.Parse(ItemActual.SubItems[5].Text);
            txtOperacion.Text = ItemActual.SubItems[6].Text;
            txtCargo.Text = ItemActual.SubItems[7].Text;
            lstPersona.Visible = false;
            txtNota.Focus();
        }

        private void txtNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnAgregar_Click(sender, e);
            }
        }

        private void txtNota_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNota.Text.Length > 0 && Convert.ToInt32(txtNota.Text) < 11)
            {
                txtCondicion.BackColor = Color.Salmon;
                txtCondicion.Text = "REPROGRAMACION";
            }

            if (txtNota.Text.Length > 0 && Convert.ToInt32(txtNota.Text) >= 11)
            {
                txtCondicion.BackColor = Color.FromArgb(192, 255, 192);
                txtCondicion.Text = "APTO";
            }

            if (txtNota.Text.Length > 0 && Convert.ToInt32(txtNota.Text) > 20)
            {
                txtCondicion.BackColor = Color.FromArgb(240, 240, 240);
                txtCondicion.Text = "NO VÁLIDO";
            }

            if (txtNota.Text.Length == 0)
            {
                txtCondicion.BackColor = Color.FromArgb(240, 240, 240);
                txtCondicion.Clear();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pRegistrarAsistente.Visible = false;
        }

        private void pRegistrarAsistente_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pRegistrarAsistente.Left = pRegistrarAsistente.Left + (e.X - xClick);
                pRegistrarAsistente.Top = pRegistrarAsistente.Top + (e.Y - yClick);
            }
        }

        private void dtgvListaSolicitudes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                idAsistente = Convert.ToInt32(dtgvListaAsistentesView.GetRowCellValue(dtgvListaAsistentesView.FocusedRowHandle, "#"));

                if (idAsistente != 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarToolStripMenuItem.Enabled = true; }
                }
                else { eliminarToolStripMenuItem.Enabled = false; }
            }
            catch
            { eliminarToolStripMenuItem.Enabled = false; }
        }

        private void dtgvListaAsistentesView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idAsistente = Convert.ToInt32(dtgvListaAsistentesView.GetRowCellValue(dtgvListaAsistentesView.FocusedRowHandle, "#"));
                DataTable dtAgregarA = new DataTable();
                string Respuesta;
                dtAgregarA = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_RegistrarAsistente(2, idAsistente, _idCapacitacion, 0, 0, 0, 0, "", "");
                Respuesta = Convert.ToString(dtAgregarA.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    ListarAsistentes();
                    _formulario.ListarCapacitaciones();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("El asistente seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            personal = 0;
            idGrupo = 0;
            idOperacion = 0;
            txtPersona.Clear();
            txtDNI.Clear();
            txtCargo.Clear();
            txtGrupo.Clear();
            txtOperacion.Clear();
            txtNota.Clear();
            txtCondicion.Clear();
            txtCondicion.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPersona.Text.Length == 0 || txtDNI.Text.Length == 0 || txtNota.Text.Length == 0 || txtCondicion.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPersona.Text.Length == 0 || txtDNI.Text.Length == 0) { txtPersona.Focus(); }
                else { txtNota.Focus(); }
                return;
            }
            else
            {
                DataTable dtAgregarA = new DataTable();
                string respta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregarA = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_RegistrarAsistente(1, 0, _idCapacitacion, personal, idGrupo, idOperacion,
                             Convert.ToInt32(txtNota.Text), txtCondicion.Text, Usuario);
                respta = Convert.ToString(dtAgregarA.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    ListarAsistentes();
                    _formulario.ListarCapacitaciones();
                    btnCancelar_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCancelar_Click(sender, e);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text.Length == 0 || txtHoras.Text.Length == 0 || txtInstructor.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtTitulo.Text.Length == 0) { txtTitulo.Focus(); }
                else
                {
                    if (txtHoras.Text.Length == 0) { txtHoras.Focus(); }
                    else { txtInstructor.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtAgregarC = new DataTable();
                string respta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregarC = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_InsertarModificarCapacitacion(opcion, _idCapacitacion, txtTitulo.Text, Convert.ToInt32(cbxTipo.SelectedValue),
                                                      Convert.ToInt32(cbxLugar.SelectedValue), dtpFecha.Value, Convert.ToDecimal(txtHoras.Text), txtInstructor.Text, idProgramacion, idDetalle, Usuario);
                respta = Convert.ToString(dtAgregarC.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _formulario.ListarCapacitaciones();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
