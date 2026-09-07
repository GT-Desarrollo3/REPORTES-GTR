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
using Negocio;
using Comun;
using ReportesTranspesa.Sistema;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.AsignacionUniformes
{
    public partial class frmListaUniformes : Form
    {
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        DataTable dtPermisos = new DataTable();
        int idAsignarUniforme, VidaUtil;

        public frmListaUniformes()
        {
            InitializeComponent();
            cbxUniforme.SelectedIndexChanged -= cbxUniforme_SelectedIndexChanged;
        }

        private void cbxUniforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboUniforme();
        }

        private void frmListaUniformes_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaUniformes");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    btnAsignarUniforme.Enabled = true;
                }
                else { btnAsignarUniforme.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    cambiarFechaToolStripMenuItem.Enabled = true;
                    desvincularToolStripMenuItem.Enabled = true;
                }
                else
                {
                    cambiarFechaToolStripMenuItem.Enabled = false;
                    desvincularToolStripMenuItem.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                {
                    eliminarToolStripMenuItem.Enabled = true;
                }
                else { eliminarToolStripMenuItem.Enabled = false; }
            }

            FechaInicio.Value = new DateTime(FechaInicio.Value.Year, FechaInicio.Value.Month, 1);
            FechaFin.Value = DateTime.Now;
            CargarComboUniforme();
        }


        private void CargarComboUniforme()
        {
            DataTable dtUniforme = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarUniformes(2);
            cbxUniforme.DataSource = dtUniforme;
            cbxUniforme.DisplayMember = "NombreUniforme";
            cbxUniforme.ValueMember = "idUniforme";
        }

        public void ListarUniformes()
        {
            string fechin, fechfin;
            fechin = FechaInicio.Value.ToShortDateString() + " 00:00:00";
            fechfin = FechaFin.Value.ToShortDateString() + " 23:59:59";

            if (FechaInicio.Value > FechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FechaInicio.Focus();
                return;
            }
            else
            {
                dtgUniformePersonal.DataSource = null;
                dgvUniformePersonalVista.Columns.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarRegistros(txtPersonal.Text, FechaInicio.Text, FechaFin.Text, Convert.ToInt32(cbxUniforme.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    dtgUniformePersonal.DataSource = dt;
                    dgvUniformePersonalVista.Columns["idAsignarUniforme"].Visible = false;
                    dgvUniformePersonalVista.Columns["idPersonal"].Visible = false;
                    dgvUniformePersonalVista.Columns["department"].Visible = false;
                    dgvUniformePersonalVista.Columns["CodigoPuesto"].Visible = false;
                    dgvUniformePersonalVista.Columns["idUniforme"].Visible = false;

                    dgvUniformePersonalVista.BestFitColumns();
                }
            }
        }


        private void dtgUniformePersonal_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string ES = dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "Estado").ToString();

                if (ES == "EN USO")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        cambiarFechaToolStripMenuItem.Enabled = true;
                        desvincularToolStripMenuItem.Enabled = true;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                    { eliminarToolStripMenuItem.Enabled = true; }
                }
            }
            catch
            {
                cambiarFechaToolStripMenuItem.Enabled = false;
                desvincularToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem.Enabled = false;
            }
        }

        private void btnAsignarUniforme_Click(object sender, EventArgs e)
        {
            frmAsignarUniformes frmAsignarUniformes = new frmAsignarUniformes();
            frmAsignarUniformes.RecibirDatos(this);
            frmAsignarUniformes.ShowDialog();
        }

        private void btnHistorialUniformes_Click(object sender, EventArgs e)
        {
            frmHistorialDevueltos frmHistorialDevueltos = new frmHistorialDevueltos();
            frmHistorialDevueltos.ShowDialog();
        }

        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { ListarUniformes(); }
        }

        private void FechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { ListarUniformes(); }
        }

        private void FechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { ListarUniformes(); }
        }

        private void cbxUniforme_DropDownClosed(object sender, EventArgs e) { ListarUniformes(); }

        private void cambiarFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime FechaUniforme = Convert.ToDateTime(dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "FechaAsignación"));
                pActualizarFecha.Visible = true;
                pActualizarFecha.BringToFront();
                dtpNuevaFecha.Value = FechaUniforme;
                dtpNuevaFecha.Focus();
            }
            catch
            {
                dtpNuevaFecha.Value = DateTime.Now;
                dtpNuevaFecha.Focus();
            }
        }

        private void desvincularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idAsignarUniforme = Convert.ToInt32(dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "idAsignarUniforme"));
            VidaUtil = Convert.ToInt32(dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "Meses"));

            pDevolverCantidad.Visible = true;
            pDevolverCantidad.BringToFront();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            int idAsignarUniforme = Convert.ToInt32(dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "idAsignarUniforme"));
            int VidaUtil = Convert.ToInt32(dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "Meses"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (MessageBox.Show("¿Desea eliminar esta asignación?", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado(3, idAsignarUniforme, VidaUtil, dtpNuevaFecha.Value, 0, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarUniformes();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvUniformePersonalVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "DiasRestantes")
            {
                if (Convert.ToInt32(e.CellValue) <= 0)
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (Convert.ToInt32(e.CellValue) > 0)
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            int idAsignarUniforme = Convert.ToInt32(dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "idAsignarUniforme"));
            int VidaUtil = Convert.ToInt32(dgvUniformePersonalVista.GetRowCellValue(dgvUniformePersonalVista.FocusedRowHandle, "Meses"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado(1, idAsignarUniforme, VidaUtil, dtpNuevaFecha.Value, 0, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pActualizarFecha.Visible = false;
                pActualizarFecha.SendToBack();
                ListarUniformes();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            dtpNuevaFecha.Value = DateTime.Now;
            pActualizarFecha.Visible = false;
            pActualizarFecha.SendToBack();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarUniformes(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgUniformePersonal.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Uniformes Asignados - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgUniformePersonal.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void pActualizarFecha_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pActualizarFecha.Left = pActualizarFecha.Left + (e.X - xClick);
                pActualizarFecha.Top = pActualizarFecha.Top + (e.Y - yClick);
            }
        }

        private void pDevolverCantidad_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pDevolverCantidad.Left = pDevolverCantidad.Left + (e.X - xClick2);
                pDevolverCantidad.Top = pDevolverCantidad.Top + (e.Y - yClick2);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            idAsignarUniforme = 0; VidaUtil = 0;
            txtCantidad.Clear();
            pDevolverCantidad.Visible = false;
            pDevolverCantidad.SendToBack();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "0" || txtCantidad.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad a devolver.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtCantidad.Text.Length == 0) { txtCantidad.Focus(); }
                else { txtCantidad.Focus(); }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (MessageBox.Show("¿Desea devolver este uniforme?", "DEVOLVER UNIFORME", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado(2, idAsignarUniforme, VidaUtil, dtpNuevaFecha.Value, Convert.ToInt32(txtCantidad.Text), Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarUniformes();
                        pictureBox1_Click(sender, e);
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
