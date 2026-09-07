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
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmRegistroCapacitacion : Form
    {
        public int xClick = 0, yClick = 0;
        public int Persona = -1, idCapacitacionC = 0, Opcion = 0;
        public DataSet dsTabla;

        public frmRegistroCapacitacion()
        {
            InitializeComponent();
        }

        private void frmRegistroCapacitacion_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            cbxEstado.Text = "TODAS";

            ListarCapacitacion();
        }


        public void ListarAsistencias(int idCapacitacionC)
        {
            DataTable dtListaCapacitacion = new DataTable();
            dtListaCapacitacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarAsistentes(idCapacitacionC);
            dtgCapacitacion.DataSource = dtListaCapacitacion;

            if (dtListaCapacitacion.Rows.Count > 0)
            {
                dgvCapacitacionView.Columns["idCapacitacionD"].Visible = false;
                dgvCapacitacionView.Columns["idCapacitacionC"].Visible = false;

                dgvCapacitacionView.BestFitColumns();
            }
        }

        public void ListarCapacitacion()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dsTabla = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarCapacitaciones(dtpFechaIni.Text, dtpFechaFin.Text, cbxEstado.Text, txtBuscarTema.Text, txtBuscarAsist.Text);
                dtgRegistro.DataSource = dsTabla.Tables[0];
                dtgAsistencia.DataSource = dsTabla.Tables[1];

                if (dsTabla.Tables[0].Rows.Count > 0)
                {
                    dgvRegistroView.Columns["INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroView.Columns["INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvRegistroView.Columns["FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroView.Columns["FIN"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvRegistroView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvRegistroView.Columns["TEMA"].Summary.Clear();
                    dgvRegistroView.Columns["TEMA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TEMA", "Total: {0}");

                    dgvRegistroView.BestFitColumns();
                }

                if (dsTabla.Tables[1].Rows.Count > 0)
                {
                    dgvAsistenciaView.Columns["idCapacitacionC"].Visible = false;
                    dgvAsistenciaView.Columns["Persona"].Visible = false;

                    dgvAsistenciaView.Columns["INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAsistenciaView.Columns["INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvAsistenciaView.Columns["FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAsistenciaView.Columns["FIN"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvAsistenciaView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAsistenciaView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvAsistenciaView.Columns["ASISTENTE"].Summary.Clear();
                    dgvAsistenciaView.Columns["ASISTENTE"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ASISTENTE", "Total: {0}");

                    dgvAsistenciaView.BestFitColumns();
                }
            }
        }


        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            idCapacitacionC = 0;
            txtEmpresa.Focus();
            ListarAsistencias(0);
            dtpCFechaIni.Value = DateTime.Now;
            dtpCFechaFin.Value = DateTime.Now;

            pDatosCapacitacion.Location = new System.Drawing.Point(555, 260);
            pDatosCapacitacion.Visible = true;
            pDatosCapacitacion.BringToFront();
            btnNuevoRegistro.Enabled = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pDatosCapacitacion.Visible = false;
            pDatosCapacitacion.SendToBack();

            txtEmpresa.Clear();
            txtCapacitador.Clear();
            txtTema.Clear();
            Persona = -1;
            txtPersona.Clear();
            dtpCFechaIni.Value = DateTime.Now;
            dtpCFechaFin.Value = DateTime.Now;
            btnNuevoRegistro.Enabled = true;
        }

        private void pDatosCapacitacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pDatosCapacitacion.Left = pDatosCapacitacion.Left + (e.X - xClick);
                pDatosCapacitacion.Top = pDatosCapacitacion.Top + (e.Y - yClick);
            }
        }

        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCapacitador.Focus(); }
        }

        private void txtCapacitador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtTema.Focus(); }
        }

        private void txtTema_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpCFechaIni.Focus(); }
        }

        private void dtpCFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpCFechaFin.Focus(); }
        }

        private void dtpCFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtPersona.Focus(); }
        }

        private void txtPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Capacitaciones_ListarEmpleados(1, txtPersona.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 250;
            lstPersona.Columns[2].Width = 80;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                lstPersona.Visible = false;
                lstPersona.SendToBack();
            }
        }

        private void txtPersona_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersona.Focus(); }
        }

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
                btnAgregar_Click(sender, e);

                lstPersona.Visible = false;
                lstPersona.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                lstPersona.Visible = false;
                lstPersona.SendToBack();
            }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersona.SelectedItems[0];
            Persona = Int32.Parse(ItemActual.Text);
            txtPersona.Text = ItemActual.SubItems[1].Text;
            btnAgregar_Click(sender, e);

            lstPersona.Visible = false;
            lstPersona.SendToBack();
        }

        private void dtgCapacitacion_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string DNI = dgvCapacitacionView.GetRowCellValue(dgvCapacitacionView.FocusedRowHandle, "DNI").ToString();

                if (DNI != "") { tsEliminarAsistencia.Enabled = true; }
                else { tsEliminarAsistencia.Enabled = false; }
            }
            catch { tsEliminarAsistencia.Enabled = false; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPersona.Text.Length == 0 || Persona == -1)
            {
                MessageBox.Show("El asistente ingresado no es válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPersona.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta = "";
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(1, 0, idCapacitacionC, "PENDIENTE", Persona, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                
                if (NroRPTA == "0")
                {
                    Persona = -1;
                    txtPersona.Clear();
                    ListarAsistencias(idCapacitacionC);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Persona = -1;
                    lstPersona.Visible = false;
                    lstPersona.SendToBack();
                    txtPersona.Clear();
                }
            }
        }

        private void tsEliminarAsistencia_Click(object sender, EventArgs e)
        {
            try
            {
                int idCapacitacionD = Convert.ToInt32(dgvCapacitacionView.GetRowCellValue(dgvCapacitacionView.FocusedRowHandle, "idCapacitacionD"));

                DataTable dtRespuesta = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(3, idCapacitacionD, idCapacitacionC, "", 0, Usuario);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                
                if (NroRspta == "0") { ListarAsistencias(idCapacitacionC); }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("No se pudo eliminar al asistente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtEmpresa.Text.Length == 0 || txtCapacitador.Text.Length == 0 || txtTema.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtEmpresa.Text.Length == 0) { txtEmpresa.Focus(); }
                else
                {
                    if (txtCapacitador.Text.Length == 0) { txtCapacitador.Focus(); }
                    else { txtTema.Focus(); }
                }

                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta = "";
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarCapacitacion(Opcion, idCapacitacionC, txtEmpresa.Text,
                                                           txtCapacitador.Text, txtTema.Text, dtpCFechaIni.Value, dtpCFechaFin.Value, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarCapacitacion();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCapacitacion(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCapacitacion(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarCapacitacion(); }

        private void txtBuscarTema_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCapacitacion(); }
        }

        private void txtBuscarAsist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCapacitacion(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarCapacitacion(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            dtfi.TimeSeparator = ".";

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string nombreFinal = Path.Combine(desktop, "REGISTRO DE CAPACITACIONES - " + Utilitario.Instancia.SesionUsuario.usuario
                                              + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");

            string tempRegistro = Path.Combine(Path.GetTempPath(), "Registro_tmp.xlsx");
            string tempAsistencia = Path.Combine(Path.GetTempPath(), "Asistencia_tmp.xlsx");

            Excel.Application excelApp = null;
            Excel.Workbook libroFinal = null;

            try
            {
                if (dtgRegistro.DataSource != null)
                    dtgRegistro.ExportToXlsx(tempRegistro);

                if (dtgAsistencia.DataSource != null)
                    dtgAsistencia.ExportToXlsx(tempAsistencia);

                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;

                libroFinal = excelApp.Workbooks.Add();

                while (libroFinal.Worksheets.Count > 1)
                { ((Excel.Worksheet)libroFinal.Worksheets[libroFinal.Worksheets.Count]).Delete(); }

                bool primeraHojaUsada = false;

                if (File.Exists(tempAsistencia))
                {
                    Excel.Workbook wbTemp1 = excelApp.Workbooks.Open(tempAsistencia);
                    Excel.Worksheet wsTemp1 = (Excel.Worksheet)wbTemp1.Worksheets[1];

                    Excel.Worksheet wsDestino;
                    if (!primeraHojaUsada)
                    {
                        wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                        primeraHojaUsada = true;
                    }
                    else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                    wsDestino.Name = "ASISTENCIAS";
                    wsTemp1.Cells.Copy(wsDestino.Cells);

                    wbTemp1.Close(false);
                }

                if (File.Exists(tempRegistro))
                {
                    Excel.Workbook wbTemp2 = excelApp.Workbooks.Open(tempRegistro);
                    Excel.Worksheet wsTemp2 = (Excel.Worksheet)wbTemp2.Worksheets[1];

                    Excel.Worksheet wsDestino;
                    if (!primeraHojaUsada)
                    {
                        wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                        primeraHojaUsada = true;
                    }
                    else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                    wsDestino.Name = "REGISTRO";
                    wsTemp2.Cells.Copy(wsDestino.Cells);

                    wbTemp2.Close(false);
                }

                libroFinal.SaveAs(nombreFinal);
                libroFinal.Close();
                excelApp.Quit();

                Process.Start(nombreFinal);
            }
            catch (Exception ex) { MessageBox.Show("Error al exportar: " + ex.Message); }
            finally
            {
                if (libroFinal != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(libroFinal);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                if (File.Exists(tempAsistencia)) File.Delete(tempAsistencia);
                if (File.Exists(tempRegistro)) File.Delete(tempRegistro);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void dgvAsistenciaView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "EJECUTADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "REPROGRAMADO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgRegistro_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "NRO").ToString();

                if (Nro != "")
                {
                    tsActualizarRegistro.Enabled = true;
                    tsAnularRegistro.Enabled = true;
                }
                else
                {
                    tsActualizarRegistro.Enabled = false;
                    tsAnularRegistro.Enabled = false;
                }
            }
            catch
            {
                tsActualizarRegistro.Enabled = false;
                tsAnularRegistro.Enabled = false;
            }
        }

        private void dtgAsistencia_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvAsistenciaView.GetRowCellValue(dgvAsistenciaView.FocusedRowHandle, "NRO").ToString();

                if (Nro != "")
                {
                    tsActualizarRegistro.Enabled = true;
                    tsAnularRegistro.Enabled = true;
                }
                else
                {
                    tsActualizarRegistro.Enabled = false;
                    tsAnularRegistro.Enabled = false;
                }
            }
            catch
            {
                tsActualizarRegistro.Enabled = false;
                tsAnularRegistro.Enabled = false;
            }
        }

        private void tsActualizarRegistro_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            btnNuevoRegistro.Enabled = false;
            pDatosCapacitacion.Location = new System.Drawing.Point(555, 260);

            idCapacitacionC = Convert.ToInt32(dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "NRO"));
            txtEmpresa.Text = dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "EMPRESA").ToString();
            txtCapacitador.Text = dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "CAPACITADOR").ToString();
            txtTema.Text = dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "TEMA").ToString();
            dtpCFechaIni.Value = Convert.ToDateTime(dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "INICIO"));
            dtpCFechaFin.Value = Convert.ToDateTime(dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "FIN"));
            ListarAsistencias(idCapacitacionC);

            pDatosCapacitacion.Visible = true;
            pDatosCapacitacion.BringToFront();
            txtEmpresa.Focus();
        }

        private void tsAnularRegistro_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "ANULAR REGISTRO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int Nro2 = Convert.ToInt32(dgvRegistroView.GetRowCellValue(dgvRegistroView.FocusedRowHandle, "NRO"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarCapacitacion(3, Nro2, "", "", "", DateTime.Now, DateTime.Now, "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarCapacitacion(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsActPendiente_Click(object sender, EventArgs e)
        {
            int[] filas = dgvAsistenciaView.GetSelectedRows();

            if (filas.Length != 0)
            {
                DataTable dtRespuesta = new DataTable();
                string respta;
                int Correcto = 0;

                for (int i = 0; i < filas.Length; i++)
                {
                    int CapacitacionC = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "idCapacitacionC"));
                    int CapacitacionD = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "NRO"));

                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(2, CapacitacionD, CapacitacionC, "PENDIENTE", 0, "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                ListarCapacitacion();
            }
        }

        private void tsActEjecutado_Click(object sender, EventArgs e)
        {
            int[] filas = dgvAsistenciaView.GetSelectedRows();

            if (filas.Length != 0)
            {
                DataTable dtRespuesta = new DataTable();
                string respta;
                int Correcto = 0;

                for (int i = 0; i < filas.Length; i++)
                {
                    int CapacitacionC = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "idCapacitacionC"));
                    int CapacitacionD = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "NRO"));

                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(2, CapacitacionD, CapacitacionC, "EJECUTADO", 0, "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                ListarCapacitacion();
            }
        }

        private void tsActReprogramado_Click(object sender, EventArgs e)
        {
            int[] filas = dgvAsistenciaView.GetSelectedRows();

            if (filas.Length != 0)
            {
                DataTable dtRespuesta = new DataTable();
                string respta;
                int Correcto = 0;

                for (int i = 0; i < filas.Length; i++)
                {
                    int CapacitacionC = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "idCapacitacionC"));
                    int CapacitacionD = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "NRO"));

                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(2, CapacitacionD, CapacitacionC, "REPROGRAMADO", 0, "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                ListarCapacitacion();
            }
        }

        private void tsAnularAsist_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea anular estas asistencias?", "ANULAR REGISTRO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int[] filas = dgvAsistenciaView.GetSelectedRows();

                if (filas.Length != 0)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;
                    int Correcto = 0;

                    for (int i = 0; i < filas.Length; i++)
                    {
                        int CapacitacionC = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "idCapacitacionC"));
                        int CapacitacionD = Convert.ToInt32(dgvAsistenciaView.GetRowCellValue(filas[i], "NRO"));

                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(3, CapacitacionD, CapacitacionC, "REPROGRAMADO", 0, "");
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0") { Correcto = Correcto + 1; }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    ListarCapacitacion();
                }
            }
        }
    }
}
