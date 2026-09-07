using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using System.Windows.Forms;
using ReportesTranspesa.Formularios.Areas.Operaciones;
using Comun;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    
    public partial class Asistencia : MetroFramework.Forms.MetroForm
    {
        DataTable dtPermisos = new DataTable();
        private frmAsistenciaMapeo frmAsistenciaMapeo;
        private Logistica.frmInventarioValorizadoPeriodoCerrado_043 frmInventario;
        private frmProgramacionGeneral frmProgramacionGeneral;
        private frmListarTardanzas frmListarTardanzas;
        public int idPersona;
        public DateTime Fecha;
        int xClick = 0, yClick = 0;
        int xClick2 = 0, yClick2 = 0;

        public Asistencia()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void Asistencia_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("RRHH_Asistencia");
            
            string FI = DateTime.Now.ToShortDateString() + " 00:00:00";
            dtpFechaInicio.Value = Convert.ToDateTime(FI);
            dtpFechaFin.Value = DateTime.Now;
            string FT = DateTime.Now.ToShortDateString() + " 08:05:59";
            dtpFechaTIni.Value = Convert.ToDateTime(FT);
            dtpFechaTFin.Value = DateTime.Now;
            dtpHoraTIni.Value = Convert.ToDateTime(FT);
            dtpHoraTFin.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00:00");
            CargarComboArea();
            cbxSucursal.Text = "TODAS";
            cbxArea.Text = "TODAS";
            btnBuscarA_Click(sender, e);
            btnBuscarT_Click(sender, e);
        }


        private void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarAreas(1);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "AREA";
            cbxArea.ValueMember = "CODIGO";
        }


        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarA_Click(sender, e); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarA_Click(sender, e); }
        }

        private void cbxSucursal_DropDownClosed(object sender, EventArgs e)
        {
            btnBuscarA_Click(sender, e);
            btnBuscarT_Click(sender, e);
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e)
        {
            btnBuscarA_Click(sender, e);
            btnBuscarT_Click(sender, e);
        }

        private void txtEmpleado_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnBuscarA_Click(sender, e);
                btnBuscarT_Click(sender, e);
            }
        }

        private void btnBuscarA_Click(object sender, EventArgs e)
        {
            if (dtpFechaInicio.Value >= dtpFechaFin.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.", "Error");
                return;
            }

            dtgMarcaciones.DataSource = null;
            dgvMarcacionesView.Columns.Clear();
            dtgFaltas.DataSource = null;
            dgvFaltasView.Columns.Clear();
            dtgFaltaDetalle.DataSource = null;
            dgvFaltaDetalleView.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            System.Data.DataTable dt3 = new System.Data.DataTable();

            dt = clsRecursosHumanosBL.Instancia.GetAsistencia(dtpFechaInicio.Text, dtpFechaFin.Text, "Marcas", cbxSucursal.Text, cbxArea.Text, txtEmpleado.Text);
            dt2 = clsRecursosHumanosBL.Instancia.GetAsistencia(dtpFechaInicio.Text, dtpFechaFin.Text, "Faltas", cbxSucursal.Text, cbxArea.Text, txtEmpleado.Text);
            dt3 = clsRecursosHumanosBL.Instancia.GetAsistencia(dtpFechaInicio.Text, dtpFechaFin.Text, "FaltasD", cbxSucursal.Text, cbxArea.Text, txtEmpleado.Text);

            if (dt.Rows.Count > 0)
            {
                dtgMarcaciones.DataSource = dt;
                dtgFaltas.DataSource = dt2;
                dtgFaltaDetalle.DataSource = dt3;

                dgvMarcacionesView.Columns["Codigo"].Visible = false;
                dgvMarcacionesView.Columns["idAsistenciaN"].Visible = false;
                dgvMarcacionesView.BestFitColumns();

                dgvFaltasView.Columns["Persona"].Visible = false;
                dgvFaltasView.Columns["Nro Dias"].Visible = false;
                dgvFaltasView.Columns["Fecha Retorno"].Visible = false;
                dgvFaltasView.BestFitColumns();

                dgvFaltaDetalleView.Columns["Persona"].Visible = false;
                dgvFaltaDetalleView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para mostrar.";
                m.ShowDialog();
            }
        }

        private void btnExcelA_Click(object sender, EventArgs e)
        {
            if (dtgMarcaciones.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
                return;
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";

                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombreFinal = Path.Combine(desktop, "REPORTE DE MARCACIONES - " + Utilitario.Instancia.SesionUsuario.usuario
                                                  + " " + DateTime.Now.ToString("T", dtfi) +".xlsx");

                string tempAsistencias = Path.Combine(Path.GetTempPath(), "Asistencias_tmp.xlsx");
                string tempTardanzas = Path.Combine(Path.GetTempPath(), "Tardanzas_tmp.xlsx");
                string tempVacaciones = Path.Combine(Path.GetTempPath(), "Vacaciones_tmp.xlsx");
                string tempFaltas = Path.Combine(Path.GetTempPath(), "Faltas_tmp.xlsx");

                Excel.Application excelApp = null;
                Excel.Workbook libroFinal = null;

                try
                {
                    if (dtgTardanzas.DataSource != null)
                        dtgTardanzas.ExportToXlsx(tempTardanzas);

                    if (dtgFaltaDetalle.DataSource != null)
                        dtgFaltaDetalle.ExportToXlsx(tempVacaciones);

                    if (dtgFaltas.DataSource != null)
                        dtgFaltas.ExportToXlsx(tempFaltas);

                    if (dtgMarcaciones.DataSource != null)
                        dtgMarcaciones.ExportToXlsx(tempAsistencias);

                    excelApp = new Excel.Application();
                    excelApp.DisplayAlerts = false;

                    libroFinal = excelApp.Workbooks.Add();

                    while (libroFinal.Worksheets.Count > 1)
                    { ((Excel.Worksheet)libroFinal.Worksheets[libroFinal.Worksheets.Count]).Delete(); }

                    bool primeraHojaUsada = false;

                    if (File.Exists(tempTardanzas))
                    {
                        Excel.Workbook wbTemp1 = excelApp.Workbooks.Open(tempTardanzas);
                        Excel.Worksheet wsTemp1 = (Excel.Worksheet)wbTemp1.Worksheets[1];

                        Excel.Worksheet wsDestino;
                        if (!primeraHojaUsada)
                        {
                            wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                            primeraHojaUsada = true;
                        }
                        else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                        wsDestino.Name = "TARDANZAS";
                        wsTemp1.Cells.Copy(wsDestino.Cells);

                        wbTemp1.Close(false);
                    }

                    if (File.Exists(tempVacaciones))
                    {
                        Excel.Workbook wbTemp2 = excelApp.Workbooks.Open(tempVacaciones);
                        Excel.Worksheet wsTemp2 = (Excel.Worksheet)wbTemp2.Worksheets[1];

                        Excel.Worksheet wsDestino;
                        if (!primeraHojaUsada)
                        {
                            wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                            primeraHojaUsada = true;
                        }
                        else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                        wsDestino.Name = "VACACIONES Y DESCANSOS";
                        wsTemp2.Cells.Copy(wsDestino.Cells);

                        wbTemp2.Close(false);
                    }

                    if (File.Exists(tempFaltas))
                    {
                        Excel.Workbook wbTemp3 = excelApp.Workbooks.Open(tempFaltas);
                        Excel.Worksheet wsTemp3 = (Excel.Worksheet)wbTemp3.Worksheets[1];

                        Excel.Worksheet wsDestino;
                        if (!primeraHojaUsada)
                        {
                            wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                            primeraHojaUsada = true;
                        }
                        else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                        wsDestino.Name = "FALTAS";
                        wsTemp3.Cells.Copy(wsDestino.Cells);

                        wbTemp3.Close(false);
                    }

                    if (File.Exists(tempAsistencias))
                    {
                        Excel.Workbook wbTemp4 = excelApp.Workbooks.Open(tempAsistencias);
                        Excel.Worksheet wsTemp4 = (Excel.Worksheet)wbTemp4.Worksheets[1];

                        Excel.Worksheet wsDestino;
                        if (!primeraHojaUsada)
                        {
                            wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                            primeraHojaUsada = true;
                        }
                        else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                        wsDestino.Name = "ASISTENCIAS";
                        wsTemp4.Cells.Copy(wsDestino.Cells);

                        wbTemp4.Close(false);
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

                    if (File.Exists(tempTardanzas)) File.Delete(tempTardanzas);
                    if (File.Exists(tempVacaciones)) File.Delete(tempVacaciones);
                    if (File.Exists(tempFaltas)) File.Delete(tempFaltas);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        private void dtpFechaTIni_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarT_Click(sender, e); }
        }

        private void dtpFechaTFin_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarT_Click(sender, e); }
        }

        private void dtpHoraTIni_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarT_Click(sender, e); }
        }

        private void dtpHoraTFin_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarT_Click(sender, e); }
        }

        private void btnBuscarT_Click(object sender, EventArgs e)
        {
            if (dtpFechaTIni.Value >= dtpFechaTFin.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.", "Error");
                return;
            }

            dtgTardanzas.DataSource = null;
            dgvTardanzasView.Columns.Clear();
            dtgTardanzaDetalle.DataSource = null;
            dgvTardanzaDetalleView.Columns.Clear();

            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            System.Data.DataTable dt3 = new System.Data.DataTable();
            dt3 = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarTardanzas(1, dtpFechaTIni.Text+' '+dtpHoraTIni.Text,
                                                 dtpFechaTFin.Text + ' ' + dtpHoraTFin.Text, cbxSucursal.Text, cbxArea.Text, txtEmpleado.Text, Usuario);
            System.Data.DataTable dt4 = new System.Data.DataTable();
            dt4 = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarTardanzas(2, dtpFechaTIni.Text + ' ' + dtpHoraTIni.Text,
                                                 dtpFechaTFin.Text + ' ' + dtpHoraTFin.Text, cbxSucursal.Text, cbxArea.Text, txtEmpleado.Text, Usuario);

            if (dt3.Rows.Count > 0)
            {
                dtgTardanzas.DataSource = dt3;

                dgvTardanzasView.Columns["Codigo"].Visible = false;
                dgvTardanzasView.Columns["idAsistenciaN"].Visible = false;
                dgvTardanzasView.Columns["Motivo"].Visible = false;
                dgvTardanzasView.Columns["Tardanzas"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTardanzasView.Columns["Tardanzas"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvTardanzasView.BestFitColumns();
            }

            if (dt4.Rows.Count > 0)
            {
                dtgTardanzaDetalle.DataSource = dt4;

                dgvTardanzaDetalleView.Columns["Codigo"].Visible = false;
                dgvTardanzaDetalleView.Columns["Tardanzas"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTardanzaDetalleView.Columns["Tardanzas"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvTardanzaDetalleView.BestFitColumns();
            }
        }

        private void dtgMarcaciones_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                string idAsistenciaN = dgvMarcacionesView.GetRowCellValue(dgvMarcacionesView.FocusedRowHandle, "idAsistenciaN").ToString();

                if (idAsistenciaN != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsEliminarAsistencia.Enabled = true; }
                }
                else { tsEliminarAsistencia.Enabled = false; }
            }
            catch { tsEliminarAsistencia.Enabled = false; }
        }

        private void dtgFaltas_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvFaltasView.GetRowCellValue(dgvFaltasView.FocusedRowHandle, "Fecha Retorno").ToString();

                if (Codigo == " ")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { tsAnadirAsistencia.Enabled = true; }                
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsIngresarMotivo2.Enabled = true; }
                }
                else
                {
                    tsAnadirAsistencia.Enabled = false;
                    tsIngresarMotivo2.Enabled = false;
                }

                tsEliminarMotivo2.Enabled = false;
            }
            catch
            {
                tsAnadirAsistencia.Enabled = false;
                tsIngresarMotivo2.Enabled = false;
                tsEliminarMotivo2.Enabled = false;
            }
        }

        private void dtgFaltaDetalle_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvFaltaDetalleView.GetRowCellValue(dgvFaltaDetalleView.FocusedRowHandle, "Fecha Retorno").ToString();

                if (Codigo == " ")
                {
                    tsAnadirAsistencia.Enabled = false;
                    tsIngresarMotivo2.Enabled = false;
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsEliminarMotivo2.Enabled = true; }
                }
                else
                {
                    tsAnadirAsistencia.Enabled = false;
                    tsIngresarMotivo2.Enabled = false;
                    tsEliminarMotivo2.Enabled = false;
                }
            }
            catch
            {
                tsAnadirAsistencia.Enabled = false;
                tsIngresarMotivo2.Enabled = false;
                tsEliminarMotivo2.Enabled = false;
            }
        }

        private void tsAnadirAsistencia_Click(object sender, EventArgs e)
        {
            idPersona = Convert.ToInt32(dgvFaltasView.GetRowCellValue(dgvFaltasView.FocusedRowHandle, "Persona"));
            lblEmpleado2.Text = Convert.ToString(dgvFaltasView.GetRowCellValue(dgvFaltasView.FocusedRowHandle, "NombreCompleto"));
            lblArea.Text = Convert.ToString(dgvFaltasView.GetRowCellValue(dgvFaltasView.FocusedRowHandle, "Area"));
            cbxSucursalA.Text = "LAREA02";
            dtpFechaAsist.Text = Convert.ToString(dtpFechaFin.Text);

            pNuevaAsist.Location = new System.Drawing.Point(643, 365);
            pNuevaAsist.Visible = true;
            pNuevaAsist.BringToFront();
        }

        private void btnCerrarA_Click(object sender, EventArgs e)
        {
            pNuevaAsist.Visible = false;
            pNuevaAsist.SendToBack();

            idPersona = -1;
            lblEmpleado2.Text = "";
        }

        private void pNuevaAsist_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pNuevaAsist.Left = pNuevaAsist.Left + (e.X - xClick2);
                pNuevaAsist.Top = pNuevaAsist.Top + (e.Y - yClick2);
            }
        }

        private void dtpFechaAsist_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnNuevaAsist_Click(sender, e); }
        }

        private void btnNuevaAsist_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            dtAgregar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarAsistencia(1, 0, idPersona, cbxSucursalA.Text,
                                             Convert.ToDateTime(dtpFechaAsist.Text), Usuario);
            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);
            if (NroRspta == "0")
            {
                MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCerrarA_Click(sender, e);
                btnBuscarA_Click(sender, e);
                btnBuscarT_Click(sender, e);
            }
            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsIngresarMotivo2_Click(object sender, EventArgs e)
        {
            idPersona = Convert.ToInt32(dgvFaltasView.GetRowCellValue(dgvFaltasView.FocusedRowHandle, "Persona"));
            lblEmpleado.Text = Convert.ToString(dgvFaltasView.GetRowCellValue(dgvFaltasView.FocusedRowHandle, "NombreCompleto"));
            lblFecha.Text = Convert.ToString(dtpFechaInicio.Text);

            pDescontar.Location = new System.Drawing.Point(643, 365);
            pDescontar.Visible = true;
            pDescontar.BringToFront();
        }

        private void tsEliminarMotivo2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el motivo de esta falta?", "MOTIVO DE FALTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                idPersona = Convert.ToInt32(dgvFaltaDetalleView.GetRowCellValue(dgvFaltaDetalleView.FocusedRowHandle, "Persona"));
                DateTime FechaF = Convert.ToDateTime(dtpFechaInicio.Text);

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(2, 0, idPersona, FechaF, "", Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar2_Click(sender, e);
                    btnBuscarA_Click(sender, e);
                    btnBuscarT_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgTardanzas_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvTardanzasView.GetRowCellValue(dgvTardanzasView.FocusedRowHandle, "Codigo").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsIngresarMotivo.Enabled = true; }
                }
                else { tsIngresarMotivo.Enabled = false; }
                
                tsEliminarMotivo.Enabled = false;
            }
            catch
            {
                tsIngresarMotivo.Enabled = false;
                tsEliminarMotivo.Enabled = false;
            }
        }

        private void dtgTardanzaDetalle_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvTardanzaDetalleView.GetRowCellValue(dgvTardanzaDetalleView.FocusedRowHandle, "Codigo").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsEliminarMotivo.Enabled = true; }
                }
                else { tsEliminarMotivo.Enabled = false; }

                tsIngresarMotivo.Enabled = false;
            }
            catch
            {
                tsIngresarMotivo.Enabled = false;
                tsEliminarMotivo.Enabled = false;
            }
        }

        private void tsIngresarMotivo_Click(object sender, EventArgs e)
        {
            idPersona = Convert.ToInt32(dgvTardanzasView.GetRowCellValue(dgvTardanzasView.FocusedRowHandle, "Codigo"));
            lblEmpleado.Text = Convert.ToString(dgvTardanzasView.GetRowCellValue(dgvTardanzasView.FocusedRowHandle, "Nombre"));
            lblFecha.Text = Convert.ToString(dgvTardanzasView.GetRowCellValue(dgvTardanzasView.FocusedRowHandle, "Tardanzas"));
            
            pDescontar.Location = new System.Drawing.Point(643, 365);
            pDescontar.Visible = true;
            pDescontar.BringToFront();
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pDescontar.Visible = false;
            pDescontar.SendToBack();
            
            idPersona = -1;
            lblEmpleado.Text = "";
            lblFecha.Text = "";
            txtMotivo.Clear();
        }

        private void pDescontar_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pDescontar.Left = pDescontar.Left + (e.X - xClick);
                pDescontar.Top = pDescontar.Top + (e.Y - yClick);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingrese el motivo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMotivo.Focus();
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(1, 0, idPersona, Convert.ToDateTime(lblFecha.Text),
                                                           txtMotivo.Text, Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar2_Click(sender, e);
                    btnBuscarA_Click(sender, e);
                    btnBuscarT_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarMotivo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el motivo de esta tardanza?", "MOTIVO DE TARDANZA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                idPersona = Convert.ToInt32(dgvTardanzaDetalleView.GetRowCellValue(dgvTardanzaDetalleView.FocusedRowHandle, "Codigo"));
                DateTime FechaT = Convert.ToDateTime(dgvTardanzaDetalleView.GetRowCellValue(dgvTardanzaDetalleView.FocusedRowHandle, "Tardanzas"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarMotivo(2, 0, idPersona, FechaT, "", Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar2_Click(sender, e);
                    btnBuscarT_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarAsistencia_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar la asistencia de este empleado?", "QUITAR ASISTENCIA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                idPersona = Convert.ToInt32(dgvMarcacionesView.GetRowCellValue(dgvMarcacionesView.FocusedRowHandle, "Codigo"));
                int idAsistenciaN = Convert.ToInt32(dgvMarcacionesView.GetRowCellValue(dgvMarcacionesView.FocusedRowHandle, "idAsistenciaN"));
                string FechaI = Convert.ToString(dgvMarcacionesView.GetRowCellValue(dgvMarcacionesView.FocusedRowHandle, "Fecha"));
                string HoraI = Convert.ToString(dgvMarcacionesView.GetRowCellValue(dgvMarcacionesView.FocusedRowHandle, "Hora"));
                DateTime FechaIngreso = Convert.ToDateTime(FechaI + ' ' + HoraI);

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_CrearEliminarAsistencia(2, idAsistenciaN, idPersona, "", FechaIngreso, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = Respuesta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrarA_Click(sender, e);
                    btnBuscarA_Click(sender, e);
                    btnBuscarT_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }        
    }
}
