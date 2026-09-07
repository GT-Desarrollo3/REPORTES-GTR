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
using System.Xml;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class frmInspeccionTanque : Form
    {
        DataTable dtPermisos = new DataTable();
        DataTable dtListaInspecciones = new DataTable();
        int xClick = 0, yClick = 0;
        int idVehiculo;
        string Inspeccion;

        public frmInspeccionTanque()
        {
            InitializeComponent();
        }

        private void frmInspeccionTanque_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmInspeccionTanque");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregarI.Enabled = true; }
                    else { btnAgregarI.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsActualizarI.Enabled = true; }
                    else { tsActualizarI.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsAnularI.Enabled = true; }
                    else { tsAnularI.Enabled = false; }
                }
            }

            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddYears(1);
            rbTodos_Click(sender, e);
        }


        public void ListarInspecciones()
        {
            dtListaInspecciones = clsCombustibleBL.Instancia.ReportesApp_Combustible_InspeccionTanque_ListarInspeccionTanque(txtTracto.Text, dtpFechaInicio.Text, dtpFechaFin.Text, Inspeccion);
            dtgTanquesGNL.DataSource = dtListaInspecciones;
            if (dtListaInspecciones.Rows.Count > 0)
            {
                dgvTanquesGNLView.Columns["idInspeccionT"].Visible = false;
                dgvTanquesGNLView.Columns["idVehiculo"].Visible = false;

                dgvTanquesGNLView.Columns["FECHA_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTanquesGNLView.Columns["FECHA_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvTanquesGNLView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTanquesGNLView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvTanquesGNLView.Columns["FECHA_INSPECCION"].Summary.Clear();
                dgvTanquesGNLView.Columns["FECHA_INSPECCION"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "FECHA", "Total: {0}");

                dgvTanquesGNLView.BestFitColumns();
            }
            else { dtgTanquesGNL.DataSource = null; }
        }


        private void btnAgregarI_Click(object sender, EventArgs e)
        {
            dtpFechaKit.Value = DateTime.Now;
            dtpFechaCilindro.Value = DateTime.Now;
            cbxInspeccionT.Text = "ANUAL";
            cbxInspeccionT_DropDownClosed(sender, e);

            pNuevaInspeccion.Location = new System.Drawing.Point(797, 279);
            pNuevaInspeccion.Visible = true;
            pNuevaInspeccion.BringToFront();
        }

        private void pNuevaInspeccion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevaInspeccion.Left = pNuevaInspeccion.Left + (e.X - xClick);
                pNuevaInspeccion.Top = pNuevaInspeccion.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            txtNuevaPlaca.Clear();
            idVehiculo = -1;
            txtOperacion.Clear();
 
            pNuevaInspeccion.Visible = false;
            pNuevaInspeccion.SendToBack();
        }

        private void txtNuevaPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtNuevaPlaca.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 80;
            lstPlaca.Columns[2].Width = 100;
            lstPlaca.Columns[3].Width = 0;
            lstPlaca.Columns[4].Width = 130;
            lstPlaca.Columns[5].Width = 0;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                txtOperacion.Clear();
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
            }
        }

        private void txtNuevaPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            if (!lstPlaca.Items.Count.Equals(0)) { lstPlaca.Items[0].Selected = true; }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlaca.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlaca.SelectedItems[0];

                idVehiculo = Int32.Parse(ItemActual.Text);
                txtNuevaPlaca.Text = ItemActual.SubItems[1].Text;
                txtOperacion.Text = ItemActual.SubItems[4].Text;
                cbxInspeccionT.Focus();

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                txtOperacion.Clear();
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idVehiculo = Int32.Parse(ItemActual.Text);
            txtNuevaPlaca.Text = ItemActual.SubItems[1].Text;
            txtOperacion.Text = ItemActual.SubItems[4].Text;
            cbxInspeccionT.Focus();

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
        }

        private void cbxInspeccionT_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxInspeccionT.Text == "ANUAL")
            {
                dtpFechaKit.Enabled = true;
                dtpFechaCilindro.Enabled = false;
                dtpFechaKit.Focus();
            }

            if (cbxInspeccionT.Text == "QUINQUENAL")
            {
                dtpFechaKit.Enabled = false;
                dtpFechaCilindro.Enabled = true;
                dtpFechaCilindro.Focus();
            }
        }

        private void dtpFechaKit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar.Focus(); }
        }

        private void dtpFechaCilindro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar.Focus(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtOperacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese correctamente la placa.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevaPlaca.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    if (cbxInspeccionT.Text == "ANUAL")
                    {
                        dtRespuesta = clsCombustibleBL.Instancia.ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque(1, 0, idVehiculo, cbxInspeccionT.Text,
                                                                 dtpFechaKit.Value, Usuario);
                    }

                    if (cbxInspeccionT.Text == "QUINQUENAL")
                    {
                        dtRespuesta = clsCombustibleBL.Instancia.ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque(1, 0, idVehiculo, cbxInspeccionT.Text,
                                                                 dtpFechaCilindro.Value, Usuario);
                    }
                    
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCerrar_Click(sender, e);
                        ListarInspecciones();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo asignar el tecle de la unidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarInspecciones(); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarInspecciones(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarInspecciones(); }
        }

        private void rbTodos_Click(object sender, EventArgs e)
        {
            Inspeccion = "TODAS";
            ListarInspecciones();
        }

        private void rbAnual_Click(object sender, EventArgs e)
        {
            Inspeccion = "ANUAL";
            ListarInspecciones();
        }

        private void rbQuinquenal_Click(object sender, EventArgs e)
        {
            Inspeccion = "QUINQUENAL";
            ListarInspecciones();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarInspecciones(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgTanquesGNL.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE INSPECCIONES DE TANQUES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgTanquesGNL.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgTanquesGNL_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvTanquesGNLView.GetRowCellValue(dgvTanquesGNLView.FocusedRowHandle, "VEHICULO").ToString();

                if (Vacio != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsActualizarI.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsAnularI.Enabled = true; }
                }
            }
            catch
            {
                tsActualizarI.Enabled = false;
                tsAnularI.Enabled = false;
            }
        }

        private void dgvTanquesGNLView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO_INSPECCION")
            {
                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (e.CellValue.ToString() == "REVISADA") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }

            if (e.Column.FieldName == "DIAS_FALTANTES")
            {
                if (Convert.ToInt32(e.CellValue) > 12)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) <= 12)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToInt32(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void tsActualizarI_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea actualizar esta inspección?", "ACTUALIZAR INSPECCIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idInspeccionT = Convert.ToInt32(dgvTanquesGNLView.GetRowCellValue(dgvTanquesGNLView.FocusedRowHandle, "idInspeccionT"));
                    int idUnidad = Convert.ToInt32(dgvTanquesGNLView.GetRowCellValue(dgvTanquesGNLView.FocusedRowHandle, "idVehiculo"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsCombustibleBL.Instancia.ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque(2, idInspeccionT, idUnidad, 
                                                             "", DateTime.Now, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarInspecciones(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void tsAnularI_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar esta inspección?", "ELIMINAR INSPECCIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idInspeccionT = Convert.ToInt32(dgvTanquesGNLView.GetRowCellValue(dgvTanquesGNLView.FocusedRowHandle, "idInspeccionT"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsCombustibleBL.Instancia.ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque(3, idInspeccionT, 0,
                                                             "", DateTime.Now, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarInspecciones(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }
    }
}
