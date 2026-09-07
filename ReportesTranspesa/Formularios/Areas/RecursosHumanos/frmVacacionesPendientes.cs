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

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmVacacionesPendientes : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaVacaciones = new DataTable();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int idPersona, idReemplazo;
        public int xClick = 0, yClick = 0;
        string Area;
        
        public frmVacacionesPendientes()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
            dgvVacacionesPendVista.CellMerge += dgvVacacionesPendVista_CellMerge;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void frmVacacionesPendientes_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmVacacionesPendientes");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarVacaciones.Enabled = true; }
                    else { tsEliminarVacaciones.Enabled = false; }
                }
            }

            CargarComboArea();

            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtAreaUsuario.Rows.Count > 0)
            {
                Area = dtAreaUsuario.Rows[0]["AREA"].ToString();
                cbxArea.Text = Area;
                cbxArea.Enabled = false;
            }

            if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
            { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

            if (dtEspeciales != null)
            {
                if (dtEspeciales.Rows.Count > 0)
                {
                    cbxArea.Text = "TODAS";
                    cbxArea.Enabled = true;
                }
            }

            ListarVacacionesP();
        }


        private void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarAreas(1);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "AREA";
            cbxArea.ValueMember = "CODIGO";
        }

        public void ListarVacacionesP()
        {
            int focusedRowHandle = dgvVacacionesPendVista.FocusedRowHandle;

            DataTable dtOriginal = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarVacacionesPendientes(txtEmpleado.Text, cbxArea.Text);
            dtListaVacaciones = dtOriginal.Copy();
            DescontarDiasPorAnios(dtListaVacaciones);

            dtgVacacionesPend.DataSource = null;
            dgvVacacionesPendVista.Columns.Clear();
            dtgVacacionesPend.DataSource = dtListaVacaciones;

            if (dtListaVacaciones.Rows.Count > 0)
            {
                dgvVacacionesPendVista.Columns["Empleado"].Visible = false;
                dgvVacacionesPendVista.Columns["idProgV"].Visible = false;

                dgvVacacionesPendVista.OptionsView.AllowCellMerge = true;
                dgvVacacionesPendVista.Columns["NOMBRES"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                dgvVacacionesPendVista.Columns["AREA"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                dgvVacacionesPendVista.Columns["CARGO"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                dgvVacacionesPendVista.Columns["OPERACION"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                dgvVacacionesPendVista.Columns["FECHA_INGRESO"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

                for (int i = 1; i <= 3; i++)
                {
                    string nombreColumna = (DateTime.Now.Year - i).ToString();

                    if (dgvVacacionesPendVista.Columns[nombreColumna] != null)
                    { dgvVacacionesPendVista.Columns[nombreColumna].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True; }
                }

                dgvVacacionesPendVista.Columns["idProgV"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                dgvVacacionesPendVista.Columns["FECHA_INICIO"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                dgvVacacionesPendVista.Columns["FECHA_FIN"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                dgvVacacionesPendVista.Columns["NRO_DIAS"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                dgvVacacionesPendVista.Columns["REEMPLAZO"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

                dgvVacacionesPendVista.Columns["AREA"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                dgvVacacionesPendVista.BestFitColumns();
            }

            if (focusedRowHandle >= 0 && focusedRowHandle < dgvVacacionesPendVista.RowCount) { dgvVacacionesPendVista.FocusedRowHandle = focusedRowHandle; }
        }

        public int ObtenerValorCeldaEntero(int fila, string nombreColumna)
        {
            var valor = dgvVacacionesPendVista.GetRowCellValue(fila, nombreColumna);

            if (valor == null || valor == DBNull.Value || string.IsNullOrWhiteSpace(valor.ToString()))
                return 0;

            int numero;
            return int.TryParse(valor.ToString(), out numero) ? numero : 0;
        }

        private void DescontarDiasPorAnios(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            List<string> columnasAnios = new List<string>();

            foreach (DataColumn col in dt.Columns)
            {
                int anio;
                if (int.TryParse(col.ColumnName, out anio))
                {
                    columnasAnios.Add(col.ColumnName);
                    col.ReadOnly = false;
                }
            }

            columnasAnios = columnasAnios
                .OrderBy(x => Convert.ToInt32(x))
                .ToList();

            var grupos = dt.AsEnumerable()
                           .GroupBy(r => r["Empleado"].ToString());

            foreach (var grupo in grupos)
            {
                List<DataRow> filas = grupo.ToList();
                
                if (filas.Count == 0)
                    continue;

                DataRow primeraFila = filas[0];
                int totalDiasTomados = 0;

                foreach (DataRow fila in filas)
                {
                    if (fila["NRO_DIAS"] != DBNull.Value)
                        totalDiasTomados += Convert.ToInt32(fila["NRO_DIAS"]);
                }

                Dictionary<string, int> saldosFinales = new Dictionary<string, int>();

                foreach (string col in columnasAnios)
                {
                    int saldo = 0;

                    if (primeraFila[col] != DBNull.Value && !string.IsNullOrWhiteSpace(primeraFila[col].ToString()))
                        saldo = Convert.ToInt32(primeraFila[col]);

                    saldosFinales[col] = saldo;
                }

                foreach (string col in columnasAnios)
                {
                    int saldo = saldosFinales[col];

                    if (saldo <= 0)
                        continue;

                    if (totalDiasTomados >= saldo)
                    {
                        totalDiasTomados -= saldo;
                        saldosFinales[col] = 0;
                    }
                    else
                    {
                        saldosFinales[col] = saldo - totalDiasTomados;
                        totalDiasTomados = 0;
                        break;
                    }
                }

                for (int i = 0; i < filas.Count; i++)
                {
                    DataRow fila = filas[i];

                    foreach (string col in columnasAnios)
                    { fila[col] = saldosFinales[col] == 0 ? (object)DBNull.Value : saldosFinales[col]; }
                }
            }
        }


        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarVacacionesP(); }
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e) { ListarVacacionesP(); }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarVacacionesP(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgVacacionesPend.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Vacaciones Pendientes - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgVacacionesPend.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvVacacionesPendVista_CellMerge(object sender, CellMergeEventArgs e)
        {
            string nombre1 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle1, dgvVacacionesPendVista.Columns["NOMBRES"]);
            string nombre2 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle2, dgvVacacionesPendVista.Columns["NOMBRES"]);

            if (e.Column.FieldName == "FECHA_INGRESO")
            {
                string FechaIngreso1 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle1, dgvVacacionesPendVista.Columns["FECHA_INGRESO"]);
                string FechaIngreso2 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle2, dgvVacacionesPendVista.Columns["FECHA_INGRESO"]);
                
                e.Merge = (nombre1 == nombre2) && (FechaIngreso1 == FechaIngreso2);
                e.Handled = true;
            }
            
            if (e.Column.FieldName == "CARGO")
            {
                string cargo1 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string cargo2 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle2, e.Column);

                e.Merge = (cargo1 == cargo2) && (nombre1 == nombre2);
                e.Handled = true;
            }

            if (e.Column.FieldName == "OPERACION")
            {
                string operacion1 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string operacion2 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle2, e.Column);

                e.Merge = (operacion1 == operacion2) && (nombre1 == nombre2);
                e.Handled = true;
            }

            if (e.Column.FieldName == "REEMPLAZO")
            {
                string Reemplazo1 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string Reemplazo2 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle2, e.Column);

                e.Merge = (Reemplazo1 == Reemplazo2) && (nombre1 == nombre2);
                e.Handled = true;
            }

            for (int i = 3; i >= 1; i--)
            {
                if (e.Column.FieldName == Convert.ToString(DateTime.Now.Year - i))
                {
                    string Anio1 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle1, e.Column);
                    string Anio2 = dgvVacacionesPendVista.GetRowCellDisplayText(e.RowHandle2, e.Column);

                    e.Merge = (Anio1 == Anio2) && (nombre1 == nombre2);
                    e.Handled = true;
                }
            }
        }

        private void dtgVacacionesPend_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string FechaInicio = dgvVacacionesPendVista.GetRowCellValue(dgvVacacionesPendVista.FocusedRowHandle, "FECHA_INICIO").ToString();

                if (FechaInicio != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarVacaciones.Enabled = true; }
                }
                else { tsEliminarVacaciones.Enabled = false; }
            }
            catch { tsEliminarVacaciones.Enabled = false; }
        }

        private void dtgVacacionesPend_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
            {
                if (dgvVacacionesPendVista.FocusedColumn.FieldName == "NOMBRES")
                {
                    txtEmpleado2.Clear();
                    idPersona = -1;
                    idReemplazo = -1;
                    txtReemplazo.Clear();
                    lstEmpleado.Visible = false;
                    lstEmpleado.SendToBack();
                    txtDiasPendientes.Clear();
                    dtpVFechaIni.Value = DateTime.Now;
                    dtpVFechaFin.Value = DateTime.Now;

                    idPersona = Convert.ToInt32(dgvVacacionesPendVista.GetRowCellValue(dgvVacacionesPendVista.FocusedRowHandle, "Empleado"));
                    txtEmpleado2.Text = Convert.ToString(dgvVacacionesPendVista.GetRowCellValue(dgvVacacionesPendVista.FocusedRowHandle, "NOMBRES"));

                    string col1 = (DateTime.Now.Year - 3).ToString();
                    string col2 = (DateTime.Now.Year - 2).ToString();
                    string col3 = (DateTime.Now.Year - 1).ToString();

                    int dias1 = ObtenerValorCeldaEntero(dgvVacacionesPendVista.FocusedRowHandle, col1);
                    int dias2 = ObtenerValorCeldaEntero(dgvVacacionesPendVista.FocusedRowHandle, col2);
                    int dias3 = ObtenerValorCeldaEntero(dgvVacacionesPendVista.FocusedRowHandle, col3);
                    int totalDias = dias1 + dias2 + dias3;
                    txtDiasPendientes.Text = totalDias.ToString();

                    pProgramarVacaciones.Visible = true;
                    pProgramarVacaciones.BringToFront();
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            idPersona = -1;
            idReemplazo = -1;
            txtReemplazo.Clear();
            lstEmpleado.Visible = false;
            lstEmpleado.SendToBack();
            pProgramarVacaciones.Location = new System.Drawing.Point(720, 296);

            pProgramarVacaciones.Visible = false;
            pProgramarVacaciones.SendToBack();
        }

        private void pProgramarVacaciones_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pProgramarVacaciones.Left = pProgramarVacaciones.Left + (e.X - xClick);
                pProgramarVacaciones.Top = pProgramarVacaciones.Top + (e.Y - yClick);
            }
        }

        private void txtReemplazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtReemplazo.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 310;
            lstEmpleado.Columns[2].Width = 0;
            lstEmpleado.BringToFront();
            lstEmpleado.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                idReemplazo = -1;
            }
        }

        private void txtReemplazo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleado.Focus(); }
        }

        private void lstEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lstEmpleado.Items.Count.Equals(0)) { lstEmpleado.Items[0].Selected = true; }
        }

        private void lstEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleado.SelectedItems[0];
                idReemplazo = Int32.Parse(ItemActual.Text);
                txtReemplazo.Text = ItemActual.SubItems[1].Text;

                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                dtpVFechaIni.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtReemplazo.Focus();
                idReemplazo = -1;
            }
        }

        private void lstEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstEmpleado.SelectedItems[0];
            idReemplazo = Int32.Parse(ItemActual.Text);
            txtReemplazo.Text = ItemActual.SubItems[1].Text;

            lstEmpleado.Visible = false;
            lstEmpleado.SendToBack();
            dtpVFechaIni.Focus();
        }

        private void dtpVFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpVFechaFin.Focus(); }
        }

        private void dtpVFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar.Focus(); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dtpVFechaIni.Value.Date > dtpVFechaFin.Value.Date)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta = "";
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ProgramarVacacionesPendientes(1, 0, idPersona, Convert.ToInt32(txtDiasPendientes.Text),
                                                             dtpVFechaIni.Value, dtpVFechaFin.Value, idReemplazo, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarVacacionesP();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarVacaciones_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar esta programación?", "ELIMINAR PROGRAMACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idProgV = Convert.ToInt32(dgvVacacionesPendVista.GetRowCellValue(dgvVacacionesPendVista.FocusedRowHandle, "idProgV"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ProgramarVacacionesPendientes(2, idProgV, 0, 0, DateTime.Now, DateTime.Now, 0, "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarVacacionesP(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
