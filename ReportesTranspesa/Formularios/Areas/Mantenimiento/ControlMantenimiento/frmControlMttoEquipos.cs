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
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmControlMttoEquipos : Form
    {
        public int idRegistroE;
        public int xClick = 0, yClick = 0;
        DataTable dtPermisos = new DataTable();
        
        public frmControlMttoEquipos()
        {
            InitializeComponent();
            cbxAccesorio.SelectedIndexChanged -= cbxAccesorio_SelectedIndexChanged;
            cbxBuscar.SelectedIndexChanged -= cbxBuscar_SelectedIndexChanged;
        }

        private void cbxBuscar_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAccesorio(); }

        private void cbxAccesorio_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAccesorio2(); }

        private void frmControlMttoEquipos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaMantenimiento");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                else { btnAgregar.Enabled = false; }

                /*
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    tAsignarRecursos.Enabled = true;
                    tsManoObra.Enabled = true;
                }
                else
                {
                    tAsignarRecursos.Enabled = false;
                }
                */

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                {
                    tsEliminarAccesorio.Enabled = true;
                    tsEliminarHistorial.Enabled = true;
                }
                else
                {
                    tsEliminarAccesorio.Enabled = false;
                    tsEliminarHistorial.Enabled = false;
                }
            }

            CargarComboAccesorio();
            CargarComboAccesorio2();
            dtpFechaCambio.Value = DateTime.Now;
            txtIntervalo.Text = "0";
            cbHistorial.Checked = false;
            cbHistorial_CheckedChanged(sender, e);
            cbxBuscar.Text = "TODOS";
            ListarControlMtto();
        }


        public void CargarComboAccesorio()
        {
            DataTable dtAccesorio = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto("A");
            cbxAccesorio.DataSource = dtAccesorio;
            cbxAccesorio.DisplayMember = "Descripcion";
            cbxAccesorio.ValueMember = "idAccesorio";
        }

        public void CargarComboAccesorio2()
        {
            DataTable dtAccesorio2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto("B");
            cbxBuscar.DataSource = dtAccesorio2;
            cbxBuscar.DisplayMember = "Descripcion";
            cbxBuscar.ValueMember = "idAccesorio";
        }

        public void ListarControlMtto()
        {
            DataTable dtListaMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoEquipos(idRegistroE);
            dtgControlMtto.DataSource = dtListaMantenimiento;
            if (dtListaMantenimiento.Rows.Count > 0)
            {
                dgvControlMttoVista.Columns["idProcesoMtto"].Visible = false;
                dgvControlMttoVista.Columns["idAccesorio"].Visible = false;
                dgvControlMttoVista.Columns["idRegistro"].Visible = false;

                dgvControlMttoVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlMttoVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvControlMttoVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlMttoVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvControlMttoVista.Columns["PROXIMA_FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlMttoVista.Columns["PROXIMA_FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvControlMttoVista.Columns["FechaRegistro"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlMttoVista.Columns["FechaRegistro"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvControlMttoVista.BestFitColumns();
            }
        }

        public void ListarHistorial()
        {
            DataTable dtListaHistorial = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosEquipos(idRegistroE, Convert.ToInt32(cbxBuscar.SelectedValue));
            dtgHistorial.DataSource = dtListaHistorial;
            if (dtListaHistorial.Rows.Count > 0)
            {
                dgvHistorialVista.Columns["idProcesoMtto"].Visible = false;
                dgvHistorialVista.Columns["idRegistro"].Visible = false;

                dgvHistorialVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvHistorialVista.Columns["Fecha"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvHistorialVista.Columns["Fecha"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dgvHistorialVista.BestFitColumns();
            }
        }

        private void cbHistorial_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHistorial.Checked == true)
            {
                label1.Text = "HISTORIAL DE MANTENIMIENTO DE EQUIPOS";
                groupBox2.Enabled = false;
                btnCancelar.Enabled = false;
                btnAgregar.Enabled = false;
                cbxBuscar.Enabled = true;
                ListarHistorial();
                dtgHistorial.BringToFront();
            }

            if (cbHistorial.Checked == false)
            {
                label1.Text = "CONTROL DE MANTENIMIENTO DE EQUIPOS";
                groupBox2.Enabled = true;
                btnCancelar.Enabled = true;
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                cbxBuscar.Enabled = false;
                dtgHistorial.SendToBack();
            }
        }

        private void dtpFechaCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtIntervalo.Focus(); }
        }

        private void txtIntervalo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar.Focus(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cbxAccesorio.SelectedValue = 1;
            dtpFechaCambio.Value = DateTime.Now;
            txtIntervalo.Text = "0";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbxAccesorio.Text.Length == 0 || txtIntervalo.Text.Length == 0)
            {
                if (txtIntervalo.Text.Length == 0)
                {
                    MessageBox.Show("El Intervalo no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtIntervalo.Focus();
                }
                else
                {
                    MessageBox.Show("El Accesorio no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbxAccesorio.Focus();
                }

                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlEquipos(idRegistroE, Convert.ToInt32(cbxAccesorio.SelectedValue), dtpFechaCambio.Value,
                                                                                                                                 Convert.ToInt32(txtIntervalo.Text), Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFechaCambio.Value = DateTime.Now;
                    txtIntervalo.Text = "0";
                    ListarControlMtto();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvControlMttoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "%")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) > 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) > 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = true;
            pNuevo.BringToFront();
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = false;
            pNuevo.SendToBack();
            txtAccesorio.Clear();
        }

        private void tsEliminarAccesorio_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este proceso de la tabla?", "CONTROL DE MANTENIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idProceso = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idProcesoMtto"));
                int idRegistro = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idRegistro"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosEquipos(1, idProceso, idRegistro, 0);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarControlMtto(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarHistorial_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este proceso del historial?", "CONTROL DE MANTENIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idProceso = Convert.ToInt32(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "idProcesoMtto"));
                int idRegistro = Convert.ToInt32(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "idRegistro"));
                int Periodo = Convert.ToInt32(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "PERIODO"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosEquipos(2, idProceso, idRegistro, Periodo);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarHistorial(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgControlMtto_DoubleClick(object sender, EventArgs e)
        {
            cbxAccesorio.Text = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "ACCESORIO").ToString();
            dtpFechaCambio.Value = Convert.ToDateTime(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "FECHA_CAMBIO"));
            txtIntervalo.Text = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "PERIODO").ToString();
        }

        private void dtgControlMtto_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "ACCESORIO").ToString();
                
                if (Vacio != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarAccesorio.Enabled = true; }
                    
                    /*
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        tAsignarRecursos.Enabled = true;
                        tsManoObra.Enabled = true;
                    }
                    */
                }
                else { tsEliminarAccesorio.Enabled = false; }
            }
            catch
            {
                tsEliminarAccesorio.Enabled = false;
                // tAsignarRecursos.Enabled = false;
                // tsManoObra.Enabled = false;
            }
        }

        private void dtgHistorial_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "ACCESORIO").ToString();
                
                if (Vacio != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarHistorial.Enabled = true; }
                }
            }
            catch { tsEliminarHistorial.Enabled = false; }
        }

        private void cbxBuscar_DropDownClosed(object sender, EventArgs e) { ListarHistorial(); }

        private void pNuevo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevo.Left = pNuevo.Left + (e.X - xClick);
                pNuevo.Top = pNuevo.Top + (e.Y - yClick);
            }
        }

        private void txtAccesorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnNuevoAccesorio_Click(sender, e); }
        }

        private void btnNuevoAccesorio_Click(object sender, EventArgs e)
        {
            if (txtAccesorio.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un nuevo accesorio.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAccesorio.Focus();
                return;
            }
            else
            {
                DataTable dtNuevo = new DataTable();
                string respta;

                dtNuevo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_InsertarAccesorio(txtAccesorio.Text);
                respta = Convert.ToString(dtNuevo.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccesorio.Clear();
                    CargarComboAccesorio();
                    CargarComboAccesorio2();
                    btnCerrar3_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (cbHistorial.Checked == true)
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
                    string nombre = "";
                    nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE CONTROL DE MANTENIMIENTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgHistorial.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }

            if (cbHistorial.Checked == false)
            {
                if (dtgControlMtto.DataSource == null)
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
                    string nombre = "";
                    nombre = System.IO.Path.Combine(desktop, "CONTROL DE MANTENIMIENTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgControlMtto.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }
    }
}
