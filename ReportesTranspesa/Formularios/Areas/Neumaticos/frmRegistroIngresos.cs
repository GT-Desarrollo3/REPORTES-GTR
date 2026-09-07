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
using System.IO;
using System.Drawing.Imaging;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Neumaticos
{
    public partial class frmRegistroIngresos : Form
    {
        int xClick = 0, yClick = 0;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        public frmSegundoUsoNeumaticos formulario = new frmSegundoUsoNeumaticos();
        int idIngreso, e1 = 0;

        public frmRegistroIngresos()
        {
            InitializeComponent();
        }

        private void frmRegistroIngresos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmSegundoUsoNeumaticos");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                if (dtEspeciales.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Gestionar Salidas y Retornos")
                        {
                            desactivarIngresoToolStripMenuItem.Enabled = true;
                            ingresarReclamoToolStripMenuItem.Enabled = true;
                            i = 999; e1 = 1;
                        }
                        else
                        {
                            desactivarIngresoToolStripMenuItem.Enabled = false;
                            ingresarReclamoToolStripMenuItem.Enabled = false;
                        }
                    }
                }
                else
                {
                    desactivarIngresoToolStripMenuItem.Enabled = false;
                    ingresarReclamoToolStripMenuItem.Enabled = false;
                }
            }
            
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpReclamoIni.Value = new DateTime(dtpReclamoIni.Value.Year, dtpReclamoIni.Value.Month, 1);
            dtpReclamoFin.Value = DateTime.Now;
            dtpFechaReclamo.Value = DateTime.Now;
            ListarIngresos();
            ListarReclamos();
        }


        public void ListarIngresos()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                DataTable dt = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_SegundoUso_ListarIngreso(1, txtGuiaRemitente.Text, txtNeumatico.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                dtgRegistroIngresos.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgvRegistroIngresos.Columns["idRegistro"].Visible = false;
                    
                    dgvRegistroIngresos.BestFitColumns();
                }
            }
        }

        public void ListarReclamos()
        {
            if (dtpReclamoIni.Value > dtpReclamoFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpReclamoIni.Focus();
                return;
            }
            else
            {
                DataTable dt = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_SegundoUso_ListarIngreso(2, txtGuiaReclamo.Text, txtNeumaticoReclamo.Text, dtpReclamoIni.Value.ToShortDateString(), dtpReclamoFin.Value.ToShortDateString());
                dtgReclamo.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgvReclamoVista.Columns["idReclamo"].Visible = false;
                    dgvReclamoVista.Columns["idRegistro"].Visible = false;

                    dgvReclamoVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReclamoVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    
                    dgvReclamoVista.BestFitColumns();
                }
            }
        }


        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIngresos(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIngresos(); }
        }

        private void txtGuiaRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIngresos(); }
        }

        private void txtNeumatico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIngresos(); }
        }

        private void dtgRegistroIngresos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idRegistro = Convert.ToString(dgvRegistroIngresos.GetRowCellValue(dgvRegistroIngresos.FocusedRowHandle, "idRegistro"));

                if (idRegistro != "")
                {
                    if (e1 == 1)
                    {
                        desactivarIngresoToolStripMenuItem.Enabled = true;
                        ingresarReclamoToolStripMenuItem.Enabled = true;
                    }
                }
                else
                {
                    desactivarIngresoToolStripMenuItem.Enabled = false;
                    ingresarReclamoToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                desactivarIngresoToolStripMenuItem.Enabled = false;
                ingresarReclamoToolStripMenuItem.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarIngresos(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgRegistroIngresos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Ingresos de Neumáticos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgRegistroIngresos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtpReclamoIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReclamos(); }
        }

        private void dtpReclamoFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReclamos(); }
        }

        private void txtGuiaReclamo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReclamos(); }
        }

        private void txtNeumaticoReclamo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReclamos(); }
        }

        private void btnBuscarReclamo_Click(object sender, EventArgs e) { ListarReclamos(); }

        private void btnExcelReclamo_Click(object sender, EventArgs e)
        {
            if (dtgReclamo.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Reclamos de Neumáticos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgReclamo.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void ingresarReclamoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                idIngreso = Convert.ToInt32(dgvRegistroIngresos.GetRowCellValue(dgvRegistroIngresos.FocusedRowHandle, "idRegistro"));

                lblNombreNeumatico.Text = dgvRegistroIngresos.GetRowCellValue(dgvRegistroIngresos.FocusedRowHandle, "NEUMÁTICO").ToString();
                lblCantidadNeumatico.Text = dgvRegistroIngresos.GetRowCellValue(dgvRegistroIngresos.FocusedRowHandle, "CANTIDAD").ToString();
                lblGRR.Text = dgvRegistroIngresos.GetRowCellValue(dgvRegistroIngresos.FocusedRowHandle, "GUIA_REMITENTE").ToString();

                pIngresarReclamo.Visible = true;
                pIngresarReclamo.BringToFront();
            }
            catch (Exception ex) { MessageBox.Show("El ingreso seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        private void pIngresarReclamo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pIngresarReclamo.Left = pIngresarReclamo.Left + (e.X - xClick);
                pIngresarReclamo.Top = pIngresarReclamo.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pIngresarReclamo.Visible = false;
            pIngresarReclamo.SendToBack();

            lblNombreNeumatico.Text = "";
            lblGRR.Text = "";
            lblCantidadNeumatico.Text = "";

            idIngreso = 0;
            txtMotivo.Clear();
            dtpFechaReclamo.Value = DateTime.Now;
            txtCantidadR.Clear();
            txtGRR2.Clear();
        }

        private void txtMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaReclamo.Focus(); }
        }

        private void dtpFechaReclamo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCantidadR.Focus(); }
        }

        private void txtCantidadR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtGRR2.Focus(); }
        }

        private void txtGRR2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarReclamo_Click(sender, e); }
        }

        private void btnGuardarReclamo_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text.Length == 0 || txtCantidadR.Text.Length == 0 || txtGRR2.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtMotivo.Text.Length == 0) { txtMotivo.Focus(); }
                else
                {
                    if (txtCantidadR.Text.Length == 0) { txtCantidadR.Focus(); }
                    else { txtGRR2.Focus(); }
                }
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_RegistrarReclamo_SegundoUso(idIngreso, txtMotivo.Text, dtpFechaReclamo.Value,
                              Convert.ToInt32(txtCantidadR.Text), txtGRR2.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    formulario.ListarIngresos();
                    ListarIngresos();
                    ListarReclamos();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvReclamoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (Convert.ToString(e.CellValue) == "ATENDIDO")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void desactivarIngresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea anular este ingreso de neumático?", "QUITAR INGRESO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idRegistro = Convert.ToInt32(dgvRegistroIngresos.GetRowCellValue(dgvRegistroIngresos.FocusedRowHandle, "idRegistro"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_AnularSalida_SegundoUso(idRegistro);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formulario.ListarIngresos();
                        ListarIngresos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsActualizarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea actualizar el estado de este reclamo?", "ACTUALIZAR ESTADO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idReclamo = Convert.ToInt32(dgvReclamoVista.GetRowCellValue(dgvReclamoVista.FocusedRowHandle, "idReclamo"));
                    string Estado = Convert.ToString(dgvReclamoVista.GetRowCellValue(dgvReclamoVista.FocusedRowHandle, "ESTADO"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ActualizarReclamo_SegundoUso(idReclamo, Estado);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarReclamos(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsQuitarReclamo_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea retirar este reclamo de la lista?", "QUITAR RECLAMO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idReclamo = Convert.ToInt32(dgvReclamoVista.GetRowCellValue(dgvReclamoVista.FocusedRowHandle, "idReclamo"));
                    int idRegistro = Convert.ToInt32(dgvReclamoVista.GetRowCellValue(dgvReclamoVista.FocusedRowHandle, "idRegistro"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_QuitarReclamo_SegundoUso(idRegistro, idReclamo);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formulario.ListarIngresos();
                        ListarIngresos();
                        ListarReclamos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
