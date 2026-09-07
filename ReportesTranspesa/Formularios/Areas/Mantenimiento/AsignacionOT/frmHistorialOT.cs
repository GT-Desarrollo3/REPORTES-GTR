using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    public partial class frmHistorialOT : Form
    {
        public int HorasExtra, idHistorial = -1, idPersona = -1;
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public string Codigo;
        public DataTable dtPermisos = new DataTable();
        public DataSet dsTabla;

        public frmHistorialOT()
        {
            InitializeComponent();
        }

        private void frmHistorialNeumaticos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroOT");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        aprobarHEToolStripMenuItem.Enabled = true;
                        btnHorasExtra.Enabled = true;
                    }
                    else
                    {
                        aprobarHEToolStripMenuItem.Enabled = false;
                        btnHorasExtra.Enabled = false;
                    }
                }
            }
            
            dtpHistorialInicio.Value = DateTime.Now;
            dtpHistorialFin.Value = DateTime.Now;
            cbHorasExtra.Checked = false;
            cbHorasExtra_CheckedChanged(sender, e);
            cbxEstado.Text = "TODOS";
            ListarHistorial();

            dtpAnio.Value = DateTime.Now;
            ListarReportes();
        }


        public void ListarHistorial()
        {
            if (dtpHistorialInicio.Value > dtpHistorialFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpHistorialInicio.Focus();
                return;
            }
            else
            {
                DataTable dtHistorialMecanicos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarHistorial(txtNombreHistorial.Text, dtpHistorialInicio.Text, dtpHistorialFin.Text, HorasExtra, cbxEstado.Text);
                dtgHistorial.DataSource = dtHistorialMecanicos;
                if (dtHistorialMecanicos.Rows.Count > 0)
                {
                    dtgvHistorialView.Columns["idHistorial"].Visible = false;
                    dtgvHistorialView.Columns["TotalHoras"].Visible = false;

                    dtgvHistorialView.Columns["INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dtgvHistorialView.Columns["INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dtgvHistorialView.Columns["FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                    dtgvHistorialView.Columns["FIN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dtgvHistorialView.Columns["TIEMPO"].Summary.Clear();
                    if (HorasExtra == 1)
                    { dtgvHistorialView.Columns["TIEMPO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TIEMPO", "Tiempo Total: " + Convert.ToString(dtgvHistorialView.GetRowCellValue(0, "TotalHoras"))); }

                    dtgvHistorialView.BestFitColumns();
                }
            }
        }

        public void BuscarMecanico()
        {
            DataTable dtMecanico = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(2, txtCodigo.Text);
            if (dtMecanico.Rows.Count > 0)
            {
                idPersona = Convert.ToInt32(dtMecanico.Rows[0]["Persona"]);
                Codigo = dtMecanico.Rows[0]["CODIGO"].ToString();
                txtHEMecanico.Text = dtMecanico.Rows[0]["NOMBRE"].ToString();
                cbxTurno.Text = dtMecanico.Rows[0]["TURNO"].ToString();
            }
        }

        public void ListarReportes()
        {
            dsTabla = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarRegistros(dtpAnio.Value.Year);
            
            dtgListaMes.DataSource = dsTabla.Tables[0];
            if (dsTabla.Tables[0].Rows.Count > 0) { dgvListaMesView.BestFitColumns(); }

            dtgListaCargo.DataSource = dsTabla.Tables[1];
            if (dsTabla.Tables[1].Rows.Count > 0) { dgvListaCargoView.BestFitColumns(); }

            dtgListaPersonal.DataSource = dsTabla.Tables[2];
            if (dsTabla.Tables[2].Rows.Count > 0) { dgvListaPersonalView.BestFitColumns(); }
        }


        private void cbHorasExtra_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHorasExtra.Checked == true)
            {
                HorasExtra = 1;
                label17.Visible = true;
                cbxEstado.Visible = true;
                cbxEstado.Text = "TODOS";
                ListarHistorial();
            }

            if (cbHorasExtra.Checked == false)
            {
                HorasExtra = 0;
                label17.Visible = false;
                cbxEstado.Visible = false;
                ListarHistorial();
            }
        }

        private void txtNombreHistorial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorial(); }
        }

        private void dtpHistorialInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorial(); }
        }

        private void dtpHistorialFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorial(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarHistorial(); }

        private void btnHorasExtra_Click(object sender, EventArgs e)
        {
            cbxTurno.Text = "MAÑANA";
            pNuevaHoraExtra.Location = new System.Drawing.Point(759, 266);
            pNuevaHoraExtra.Visible = true;
            pNuevaHoraExtra.BringToFront();

            dtpHoraExtraIF.Value = DateTime.Now;
            dtpHoraExtraIH.Value = DateTime.Now;
            dtpHoraExtraTF.Value = DateTime.Now;
            dtpHoraExtraTH.Value = DateTime.Now;
        }

        private void btnCompensarHE_Click(object sender, EventArgs e)
        {
            frmCompensarTE frmCompensarTE = new frmCompensarTE();
            frmCompensarTE.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            cbxTurno.Text = "MAÑANA";
            txtHEMecanico.Clear();
            txtHEOT.Clear();
            txtHEDescripcion.Clear();

            dtpHoraExtraIF.Value = DateTime.Now;
            dtpHoraExtraIH.Value = DateTime.Now;
            dtpHoraExtraTF.Value = DateTime.Now;
            dtpHoraExtraTH.Value = DateTime.Now;

            idPersona = -1;
            pNuevaHoraExtra.Visible = false;
            pNuevaHoraExtra.SendToBack();
        }

        private void pNuevaHoraExtra_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pNuevaHoraExtra.Left = pNuevaHoraExtra.Left + (e.X - xClick2);
                pNuevaHoraExtra.Top = pNuevaHoraExtra.Top + (e.Y - yClick2);
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { BuscarMecanico(); }

            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                idPersona = -1;
                Codigo = "";
                txtHEMecanico.Clear();
            }
        }

        private void txtHEOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstOT, clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ListarOT_SegundoUso(txtHEOT.Text), true, false, false);
            lstOT.Columns[0].Width = 0;
            lstOT.Columns[1].Width = 100;
            lstOT.Columns[2].Width = 350;
            lstOT.Columns[3].Width = 0;
            lstOT.Columns[4].Width = 0;
            lstOT.BringToFront();
            lstOT.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                txtHEDescripcion.Clear();
                lstOT.Visible = false;
                lstOT.SendToBack();
            }
        }

        private void txtHEOT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstOT.Focus(); }
        }

        private void lstOT_Enter(object sender, EventArgs e)
        {
            if (!lstOT.Items.Count.Equals(0)) { lstOT.Items[0].Selected = true; }
        }

        private void lstOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstOT.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstOT.SelectedItems[0];

                txtHEOT.Text = ItemActual.SubItems[1].Text;
                txtHEDescripcion.Text = ItemActual.SubItems[2].Text;

                lstOT.Visible = false;
                lstOT.SendToBack();
                dtpHoraExtraIF.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstOT.Visible = false;
                lstOT.SendToBack();
            }
        }

        private void lstOT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstOT.SelectedItems[0];

            txtHEOT.Text = ItemActual.SubItems[1].Text;
            txtHEDescripcion.Text = ItemActual.SubItems[2].Text;

            lstOT.Visible = false;
            lstOT.SendToBack();
            dtpHoraExtraIF.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtHEMecanico.Text.Length == 0 || txtHEDescripcion.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtHEMecanico.Text.Length == 0) { txtCodigo.Focus(); }
                else { txtHEOT.Focus(); }
            }
            else
            {
                if (MessageBox.Show("¿Desea asignarle horas extras al mecánico?", "ASIGNAR HORAS EXTRAS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtAsignarHE = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtAsignarHE = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarHorasExtra(idPersona, txtHEOT.Text.TrimEnd(), cbxTurno.Text, dtpHoraExtraIF.Text, dtpHoraExtraIH.Text, dtpHoraExtraTF.Text, dtpHoraExtraTH.Text);
                    respta = Convert.ToString(dtAsignarHE.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pictureBox2_Click(sender, e);
                        ListarHistorial();
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnBuscarHistorial_Click(object sender, EventArgs e) { ListarHistorial(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgHistorial.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Historial de Asignación de Mecánicos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgHistorial.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgvHistorialView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "APROBADO")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "RECHAZADO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgHistorial_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Motivo = dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "MOTIVO").ToString();

                if (Motivo.Contains("TIEMPO EXTRA"))
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { aprobarHEToolStripMenuItem.Enabled = true; }
                }
                else { aprobarHEToolStripMenuItem.Enabled = false; }
            }
            catch { aprobarHEToolStripMenuItem.Enabled = false; }
        }

        private void aprobarHEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                idHistorial = Convert.ToInt32(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "idHistorial"));

                txtMecanico.Text = Convert.ToString(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "NOMBRE"));
                txtPlaca.Text = Convert.ToString(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "PLACA"));
                txtOT.Text = Convert.ToString(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "DESCRIPCION"));

                dtpNFechaIni.Text = Convert.ToString(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "INICIO"));
                dtpNHoraIni.Text = Convert.ToString(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "INICIO"));
                dtpNFechaFin.Text = Convert.ToString(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "FIN"));
                dtpNHoraFin.Text = Convert.ToString(dtgvHistorialView.GetRowCellValue(dtgvHistorialView.FocusedRowHandle, "FIN"));

                pHorasExtra.Location = new System.Drawing.Point(755, 161);
                pHorasExtra.Visible = true;
                pHorasExtra.BringToFront();
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtMecanico.Clear();
            txtPlaca.Clear();
            txtOT.Clear();

            dtpNFechaIni.Value = DateTime.Now;
            dtpNHoraIni.Value = DateTime.Now;
            dtpNFechaFin.Value = DateTime.Now;
            dtpNHoraFin.Value = DateTime.Now;

            idHistorial = -1;
            pHorasExtra.Visible = false;
            pHorasExtra.SendToBack();
        }

        private void pHorasExtra_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pHorasExtra.Left = pHorasExtra.Left + (e.X - xClick);
                pHorasExtra.Top = pHorasExtra.Top + (e.Y - yClick);
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea aprobar las horas extras del mecánico?", "APROBAR HORAS EXTRAS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtActualizarEstado = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtActualizarEstado = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AprobarHorasExtra(idHistorial, 1, dtpNFechaIni.Text, dtpNHoraIni.Text, dtpNFechaFin.Text, dtpNHoraFin.Text);
                respta = Convert.ToString(dtActualizarEstado.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pictureBox1_Click(sender, e);
                    ListarHistorial();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea anular las horas extras del mecánico?", "ANULAR HORAS EXTRAS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtActualizarEstado = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtActualizarEstado = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AprobarHorasExtra(idHistorial, 0, dtpNFechaIni.Text, dtpNHoraIni.Text, dtpNFechaFin.Text, dtpNHoraFin.Text);
                respta = Convert.ToString(dtActualizarEstado.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pictureBox1_Click(sender, e);
                    ListarHistorial();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsRoosterProyectado_Click(object sender, EventArgs e)
        {
            frmRoosterProyectado frmRoosterProyectado = new frmRoosterProyectado();
            frmRoosterProyectado.ShowDialog();
        }

        private void tsRegistroCapacitacion_Click(object sender, EventArgs e)
        {
            frmRegistroCapacitacion frmRegistroCapacitacion = new frmRegistroCapacitacion();
            frmRegistroCapacitacion.ShowDialog();
        }

        private void dtpAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarReportes(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarReportes(); }

        private void dgvListaPersonalView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "TOTAL")
            {
                if (Convert.ToDecimal(e.CellValue) < 2)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 2 && Convert.ToDecimal(e.CellValue) < 4)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 4)
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }
        }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            dtfi.TimeSeparator = ".";

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string nombreFinal = Path.Combine(desktop, "REPORTE DE HORAS POR COMPENSAR - " + Utilitario.Instancia.SesionUsuario.usuario
                                              + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");

            string tempPersonal = Path.Combine(Path.GetTempPath(), "Personal_tmp.xlsx");
            string tempCargo = Path.Combine(Path.GetTempPath(), "Cargo_tmp.xlsx");
            string tempMes = Path.Combine(Path.GetTempPath(), "Mes_tmp.xlsx");

            Excel.Application excelApp = null;
            Excel.Workbook libroFinal = null;

            try
            {
                if (dtgListaPersonal.DataSource != null)
                    dtgListaPersonal.ExportToXlsx(tempPersonal);

                if (dtgListaCargo.DataSource != null)
                    dtgListaCargo.ExportToXlsx(tempCargo);

                if (dtgListaMes.DataSource != null)
                    dtgListaMes.ExportToXlsx(tempMes);

                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;

                libroFinal = excelApp.Workbooks.Add();

                while (libroFinal.Worksheets.Count > 1)
                { ((Excel.Worksheet)libroFinal.Worksheets[libroFinal.Worksheets.Count]).Delete(); }

                bool primeraHojaUsada = false;

                if (File.Exists(tempPersonal))
                {
                    Excel.Workbook wbTemp1 = excelApp.Workbooks.Open(tempPersonal);
                    Excel.Worksheet wsTemp1 = (Excel.Worksheet)wbTemp1.Worksheets[1];

                    Excel.Worksheet wsDestino;
                    if (!primeraHojaUsada)
                    {
                        wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                        primeraHojaUsada = true;
                    }
                    else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                    wsDestino.Name = "PERSONAL";
                    wsTemp1.Cells.Copy(wsDestino.Cells);

                    wbTemp1.Close(false);
                }

                if (File.Exists(tempCargo))
                {
                    Excel.Workbook wbTemp2 = excelApp.Workbooks.Open(tempCargo);
                    Excel.Worksheet wsTemp2 = (Excel.Worksheet)wbTemp2.Worksheets[1];

                    Excel.Worksheet wsDestino;
                    if (!primeraHojaUsada)
                    {
                        wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                        primeraHojaUsada = true;
                    }
                    else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                    wsDestino.Name = "CARGO";
                    wsTemp2.Cells.Copy(wsDestino.Cells);

                    wbTemp2.Close(false);
                }

                if (File.Exists(tempMes))
                {
                    Excel.Workbook wbTemp3 = excelApp.Workbooks.Open(tempMes);
                    Excel.Worksheet wsTemp3 = (Excel.Worksheet)wbTemp3.Worksheets[1];

                    Excel.Worksheet wsDestino;
                    if (!primeraHojaUsada)
                    {
                        wsDestino = (Excel.Worksheet)libroFinal.Worksheets[1];
                        primeraHojaUsada = true;
                    }
                    else { wsDestino = (Excel.Worksheet)libroFinal.Worksheets.Add(); }

                    wsDestino.Name = "MES";
                    wsTemp3.Cells.Copy(wsDestino.Cells);

                    wbTemp3.Close(false);
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

                if (File.Exists(tempPersonal)) File.Delete(tempPersonal);
                if (File.Exists(tempCargo)) File.Delete(tempCargo);
                if (File.Exists(tempMes)) File.Delete(tempMes);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
