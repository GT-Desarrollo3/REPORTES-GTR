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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmListarGastosRuta : Form
    {
        public int xClick = 0, yClick = 0, e1 = 0;
        DataTable dtPermisos = new DataTable(), dtEspeciales = new DataTable();

        public frmListarGastosRuta()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboOperacion();
        }

        private void frmListarGastosRuta_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarPlanillas");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registrar Gastos")
                    {
                        btnNuevaSolicitud.Enabled = true;
                        btnPeajes.Enabled = true;
                        e1 = 1; i = 999;
                    }
                    else
                    {
                        btnNuevaSolicitud.Enabled = false;
                        btnPeajes.Enabled = false;
                    }
                }
            }
            else
            {
                btnNuevaSolicitud.Enabled = false;
                btnPeajes.Enabled = false;
            }
            
            CargarComboOperacion();
            cbxOperaciones.SelectedValue = 5;
            ListarGatoRuta();
        }


        private void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        public void ListarGatoRuta()
        {
            dtgGastoRuta.DataSource = null;
            dgvGastoRutaVista.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoRuta(Convert.ToInt32(cbxOperaciones.SelectedValue), txtRuta.Text);
            if (dt.Rows.Count > 0)
            {
                dtgGastoRuta.DataSource = dt;
                dgvGastoRutaVista.Columns["IdRuta"].Visible = false;
                dgvGastoRutaVista.Columns["IdOperacion"].Visible = false;

                dgvGastoRutaVista.BestFitColumns();
            }
        }

        public void ListarPeajes()
        {
            dtgListaPeajes.DataSource = null;
            dtgvListaPeajesView.Columns.Clear();

            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt2.Clear();
            dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPeajes();
            if (dt2.Rows.Count > 0)
            {
                dtgListaPeajes.DataSource = dt2;
                dtgvListaPeajesView.Columns["idPeaje"].Visible = false;

                dtgvListaPeajesView.BestFitColumns();
            }
        }

        public void InsertarModificarPeaje()
        {
            if (txtPeaje.Text.Length == 0 || txtMontoPeaje.Text.Length == 0)
            {
                if (txtPeaje.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, seleccione un peaje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPeaje.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, asigne un monto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMontoPeaje.Focus();
                }
                return;
            }
            
            int idPeaje = Convert.ToInt32(dtgvListaPeajesView.GetRowCellValue(dtgvListaPeajesView.FocusedRowHandle, "idPeaje"));
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarModificarPeajes(idPeaje, Convert.ToDecimal(txtMontoPeaje.Text));
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPeaje.Clear();
                txtMontoPeaje.Clear();
                ListarPeajes();
                ListarGatoRuta();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void dtgGastoRuta_DoubleClick(object sender, EventArgs e)
        {
            if (e1 == 1)
            {
                frmAsignarGastoRuta frmAsignarGastoRuta = new frmAsignarGastoRuta();
                frmAsignarGastoRuta.formulario = this;
                frmAsignarGastoRuta.CargarComboOperacion();
                frmAsignarGastoRuta.cbxOperaciones.Text = dgvGastoRutaVista.GetRowCellValue(dgvGastoRutaVista.FocusedRowHandle, "Operacion").ToString();
                frmAsignarGastoRuta.idRuta = Convert.ToInt32(dgvGastoRutaVista.GetRowCellValue(dgvGastoRutaVista.FocusedRowHandle, "IdRuta"));
                frmAsignarGastoRuta.txtRutasActivas.Text = dgvGastoRutaVista.GetRowCellValue(dgvGastoRutaVista.FocusedRowHandle, "Ruta").ToString();
                frmAsignarGastoRuta.txtDias.Text = dgvGastoRutaVista.GetRowCellValue(dgvGastoRutaVista.FocusedRowHandle, "TotalDias").ToString();
                frmAsignarGastoRuta.txtGasto.Text = dgvGastoRutaVista.GetRowCellValue(dgvGastoRutaVista.FocusedRowHandle, "GastoTotal").ToString();
                frmAsignarGastoRuta.ShowDialog();
            }
        }

        private void dtgListaPeajes_DoubleClick(object sender, EventArgs e)
        {
            txtPeaje.Text = dtgvListaPeajesView.GetRowCellValue(dtgvListaPeajesView.FocusedRowHandle, "PEAJE").ToString();
            txtMontoPeaje.Text = dtgvListaPeajesView.GetRowCellValue(dtgvListaPeajesView.FocusedRowHandle, "PRECIO").ToString();
        }

        private void btnPeajes_Click(object sender, EventArgs e)
        {
            pViajePeaje.Visible = true;
            pViajePeaje.BringToFront();
            ListarPeajes();
        }

        private void pViajePeaje_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pViajePeaje.Left = pViajePeaje.Left + (e.X - xClick);
                pViajePeaje.Top = pViajePeaje.Top + (e.Y - yClick);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtPeaje.Clear();
            txtMontoPeaje.Clear();
            pViajePeaje.Visible = false;
            pViajePeaje.SendToBack();
        }

        private void txtPeaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { InsertarModificarPeaje(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e) { InsertarModificarPeaje(); }

        private void btnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            frmAsignarGastoRuta frmAsignarGastoRuta = new frmAsignarGastoRuta();
            frmAsignarGastoRuta.formulario = this;
            frmAsignarGastoRuta.CargarComboOperacion();
            frmAsignarGastoRuta.cbxOperaciones.SelectedValue = 2;
            frmAsignarGastoRuta.ShowDialog();
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { ListarGatoRuta(); }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { ListarGatoRuta(); }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarGatoRuta(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgGastoRuta.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE GASTOS POR RUTA - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgGastoRuta.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
