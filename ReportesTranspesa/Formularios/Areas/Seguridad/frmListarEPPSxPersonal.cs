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

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmListarEPPSxPersonal : Form
    {
        public frmListarEPPSxPersonal()
        {
            InitializeComponent();
            cbxTipoEPPS.SelectedIndexChanged -= cbxTipoEPPS_SelectedIndexChanged;
        }

        private void cbxTipoEPPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void frmListarEPPSxPersonal_Shown(object sender, EventArgs e)
        {
            txtPersonal.Focus();
        }

        public void frmListarEPPSxPersonal_Load(object sender, EventArgs e)
        {
            CargarCombo();
            FechaInicio.Value = new DateTime(FechaInicio.Value.Year, FechaInicio.Value.Month, 1);
            FechaFin.Value = DateTime.Now;
            cbxTipoEPPS.Text = "";
            cbxTipoEPPS.SelectedValue = 0;
            ListarEPPSxPersona();
        }


        private void CargarCombo()
        {
            DataTable dtTipoEPPS = clsSeguridadBL.Instancia.ReportesApp_ListarComboTiposEPPS();
            cbxTipoEPPS.DataSource = dtTipoEPPS;
            cbxTipoEPPS.DisplayMember = "Nombre";
            cbxTipoEPPS.ValueMember = "TipoEPPS";
        }

        public void ListarEPPSxPersona()
        {
            DataTable dtEPPSPersona = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ListarEPPSxPersona(txtPersonal.Text, FechaInicio.Text, FechaFin.Text, Convert.ToInt32(cbxTipoEPPS.SelectedValue));
            dtgEPPSPersonal.DataSource = dtEPPSPersona;
            if (dtEPPSPersona.Rows.Count > 0)
            {
                dgvExpressVista.Columns["idEPPSPersonal"].Visible = false;
                dgvExpressVista.Columns["idEstado"].Visible = false;
                /*
                dgvExpressVista.SortInfo.ClearAndAddRange(new GridColumnSortInfo[]
                { 
                    new GridColumnSortInfo(dgvExpressVista.Columns["EPP"], DevExpress.Data.ColumnSortOrder.Descending)
                }, 1);
                */
                dgvExpressVista.BestFitColumns();
            }
        }

        private void ModificarFecha()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_EditarFechaAsignacion(Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "idEPPSPersonal")), Convert.ToDateTime(dtpNuevaFecha.Value), Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pActualizarFecha.Visible = false;
                ListarEPPSxPersona();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuspenderTempEPPS()
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_SuspenderTempEPPS(Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "idEPPSPersonal")), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ListarEPPSxPersona();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnNuevoEPPS_Click(object sender, EventArgs e)
        {
            try
            {
                frmNuevoEPPS open = new frmNuevoEPPS();
                open.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevoTipoEPPS_Click(object sender, EventArgs e)
        {
            try
            {
                frmNuevoTipoEPPS open = new frmNuevoTipoEPPS();
                open.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarEPPS_Click(object sender, EventArgs e)
        {
            try
            {
                frmAsignarEPPS_Personal open = new frmAsignarEPPS_Personal();

                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ListarEPPSxPersona();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVidaUtilEPPS_Click(object sender, EventArgs e)
        {
            frmVidaUtilEPPS open = new frmVidaUtilEPPS();
            open.ShowDialog();
        }

        private void cambiarFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pActualizarFecha.Visible = true;
            dtpNuevaFecha.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pActualizarFecha.Visible = false;
            txtPersonal.Focus();
        }

        private void suspenderEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuspenderTempEPPS();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarFecha();
        }

        private void desvincularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_EliminarEPPSxPersona(Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "idEPPSPersonal")), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ListarEPPSxPersona();
                txtPersonal.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarEPPSxPersona();
            }
        }

        private void dtpNuevaFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ModificarFecha();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarEPPSxPersona();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgEPPSPersonal.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de EPPS Asignados - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgEPPSPersonal.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvExpressVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "DÍAS RESTANTES")
            {
                if (Convert.ToDecimal(e.CellValue) > 30)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) <= 30 && Convert.ToDecimal(e.CellValue) > 15)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) <= 15)
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }
        }
    }
}
