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
    public partial class frmControlMtto : Form
    {
        public int idVehiculo, idProcesoMtto;
        public decimal KMActual;
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int OpcionC = 0;
        DataTable dtPermisos = new DataTable();

        public frmControlMtto()
        {
            InitializeComponent();
            cbxAccesorio.SelectedIndexChanged -= cbxAccesorio_SelectedIndexChanged;
            cbxBuscar.SelectedIndexChanged -= cbxBuscar_SelectedIndexChanged;
            cbxEspecialidad.SelectedIndexChanged -= cbxEspecialidad_SelectedIndexChanged;
        }

        private void cbxAccesorio_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAccesorio(); }

        private void cbxBuscar_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAccesorio2(); }

        private void cbxEspecialidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEspecialidad(); }

        private void frmControlMtto_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaMantenimiento");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                else { btnAgregar.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    tAsignarRecursos.Enabled = true;
                    tsManoObra.Enabled = true;
                }
                else
                {
                    tAsignarRecursos.Enabled = false;
                    tsManoObra.Enabled = true;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                {
                    eliminarToolStripMenuItem.Enabled = true;
                    eliminarHistorialToolStripItem.Enabled = true;
                }
                else
                {
                    eliminarToolStripMenuItem.Enabled = false;
                    eliminarHistorialToolStripItem.Enabled = false;
                }
            }

            CargarComboAccesorio();
            CargarComboAccesorio2();
            dtpFechaCambio.Value = DateTime.Now;
            txtKMCambio.Text = "0";
            txtIntervalo.Text = "0";
            cbHistorial.Checked = false;
            cbHistorial_CheckedChanged(sender, e);
            cbxBuscar.Text = "TODOS";
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

        public void CargarComboEspecialidad()
        {
            DataTable dtEspecialidad = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto("C-1");
            cbxEspecialidad.DataSource = dtEspecialidad;
            cbxEspecialidad.DisplayMember = "Descripcion";
            cbxEspecialidad.ValueMember = "idEspecialidad";
        }

        public void ListarControlMtto()
        {
            dtgControlMtto.DataSource = null;
            dgvControlMttoVista.Columns.Clear();

            if (cbHistorial.Checked == true)
            {
                DataTable dtListaHistorial = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesos(idVehiculo, Convert.ToInt32(cbxBuscar.SelectedValue));
                dtgControlMtto.DataSource = dtListaHistorial;
                if (dtListaHistorial.Rows.Count > 0)
                {
                    dgvControlMttoVista.Columns["idProcesoMtto"].Visible = false;
                    dgvControlMttoVista.Columns["idVehiculo"].Visible = false;

                    dgvControlMttoVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlMttoVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvControlMttoVista.Columns["Fecha"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlMttoVista.Columns["Fecha"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                }
            }

            if (cbHistorial.Checked == false)
            {
                DataTable dtListaMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto(1, idVehiculo);
                dtgControlMtto.DataSource = dtListaMantenimiento;
                if (dtListaMantenimiento.Rows.Count > 0)
                {
                    dgvControlMttoVista.Columns["idProcesoMtto"].Visible = false;
                    dgvControlMttoVista.Columns["idVehiculo"].Visible = false;
                    dgvControlMttoVista.Columns["idAccesorio"].Visible = false;

                    dgvControlMttoVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlMttoVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvControlMttoVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlMttoVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvControlMttoVista.Columns["Fecha"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlMttoVista.Columns["Fecha"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                }
            }
            
            dgvControlMttoVista.BestFitColumns();
        }

        private void cbHistorial_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHistorial.Checked == true)
            {
                label1.Text = "HISTORIAL DE MANTENIMIENTO DE UNIDADES";
                groupBox2.Enabled = false;
                btnCancelar.Enabled = false;
                btnAgregar.Enabled = false;
                cbxBuscar.Enabled = true;

                btnActualizarAccesorio.Enabled = false;
                label12.Visible = false;
                dtpFechaC2.Value = DateTime.Now;
                dtpFechaC2.Visible = false;
                label11.Visible = false;
                txtKMC2.Clear();
                txtKMC2.Visible = false;
                pbGuardar.Visible = false;
                dgvControlMttoVista.OptionsSelection.MultiSelect = false;
                dgvControlMttoVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                OpcionC = 0;
            }

            if (cbHistorial.Checked == false)
            {
                label1.Text = "CONTROL DE MANTENIMIENTO DE UNIDADES";
                groupBox2.Enabled = true;
                btnCancelar.Enabled = true;
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                cbxBuscar.Enabled = false;
                btnActualizarAccesorio.Enabled = true;
            }

            ListarControlMtto();
        }

        private void txtKMCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtIntervalo.Focus(); }
        }

        private void txtIntervalo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar_Click(sender, e); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cbxAccesorio.SelectedValue = 1;
            dtpFechaCambio.Value = DateTime.Now;
            txtKMCambio.Text = "0";
            txtIntervalo.Text = "0";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbxAccesorio.Text.Length == 0 || txtKMCambio.Text.Length == 0 || txtIntervalo.Text.Length == 0)
            {
                if (txtKMCambio.Text.Length == 0)
                {
                    MessageBox.Show("El KM de Cambio no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtKMCambio.Focus();
                }
                else
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
                }
                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControl(idVehiculo, Convert.ToInt32(cbxAccesorio.SelectedValue), Convert.ToDecimal(txtKMCambio.Text),
                                                                                                                          dtpFechaCambio.Value, Convert.ToDecimal(txtIntervalo.Text), Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtKMCambio.Text = "0";
                    dtpFechaCambio.Value = DateTime.Now;
                    txtIntervalo.Text = "0";
                    ListarControlMtto();
                }
                else
                { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        }

        private void tAsignarRecursos_Click(object sender, EventArgs e)
        {
            frmAsignarRecursos frmAsignarRecursos = new frmAsignarRecursos();
            frmAsignarRecursos.idProcesoMtto = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idProcesoMtto"));
            frmAsignarRecursos.idVehiculo = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idVehiculo"));
            frmAsignarRecursos.lblAccesorio.Text = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "ACCESORIO").ToString();
            frmAsignarRecursos.Opcion = 1;
            frmAsignarRecursos.dtgRecursosMaquina.SendToBack();
            frmAsignarRecursos.dtgRecursos.BringToFront();
            frmAsignarRecursos.ShowDialog();
        }

        private void tsManoObra_Click(object sender, EventArgs e)
        {
            idProcesoMtto = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idProcesoMtto"));
            dtpHoras.Text = "00:00:00";
            CargarComboEspecialidad();
            pManoObra.Visible = true;
            pManoObra.BringToFront();
            tsManoObra.Enabled = false;
        }

        private void pManoObra_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pManoObra.Left = pManoObra.Left + (e.X - xClick2);
                pManoObra.Top = pManoObra.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            idProcesoMtto = -1;
            pManoObra.Visible = false;
            pManoObra.SendToBack();
            if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsManoObra.Enabled = true; }
        }

        private void cbxEspecialidad_DropDownClosed(object sender, EventArgs e) { dtpHoras.Focus(); }

        private void dtpHoras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dtpHoras.Text == "00:00:00")
            {
                MessageBox.Show("Por favor ingrese la cantidad de horas.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpHoras.Focus();
                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMO(1, 0, idProcesoMtto, idVehiculo,
                                                         cbxEspecialidad.Text, dtpHoras.Text, Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);

                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este proceso de la tabla?", "CONTROL DE MANTENIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idProceso = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idProcesoMtto"));
                int idVehiculo = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idVehiculo"));
                
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesos(1, idProceso, idVehiculo, 0.00m, 0.00m);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    //MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarControlMtto();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void eliminarHistorialToolStripItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este proceso del historial?", "CONTROL DE MANTENIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idProceso = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idProcesoMtto"));
                int idVehiculo = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "idVehiculo"));
                decimal Kilometraje = Convert.ToDecimal(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "KM_CAMBIO"));
                decimal Intervalo = Convert.ToDecimal(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "INTERVALO"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesos(2, idProceso, idVehiculo, Kilometraje, Intervalo);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarControlMtto(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgControlMtto_DoubleClick(object sender, EventArgs e)
        {
            if (cbHistorial.Checked == false)
            {
                cbxAccesorio.Text = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "ACCESORIO").ToString();
                dtpFechaCambio.Value = Convert.ToDateTime(dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "FECHA_CAMBIO"));
                txtKMCambio.Text = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "KM_CAMBIO").ToString();
                txtIntervalo.Text = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "INTERVALO").ToString();
            }
        }

        private void dtgControlMtto_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var view = dgvControlMttoVista;
            var hit = view.CalcHitInfo(e.Location);

            if (hit.InRow || hit.InRowCell) { view.FocusedRowHandle = hit.RowHandle; }

            dtgControlMtto.ContextMenuStrip = cbHistorial.Checked ? contextMenuStrip2 : contextMenuStrip1;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool tieneAccesorio = false;

            try
            {
                var v = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "ACCESORIO");
                tieneAccesorio = v != null && v.ToString() != "";
            }
            catch { }

            eliminarToolStripMenuItem.Enabled = tieneAccesorio && Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]);
            tAsignarRecursos.Enabled = tieneAccesorio && Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
            tsManoObra.Enabled = tieneAccesorio && Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            bool tieneAccesorio = false;

            try
            {
                var v = dgvControlMttoVista.GetRowCellValue(dgvControlMttoVista.FocusedRowHandle, "ACCESORIO");
                tieneAccesorio = v != null && v.ToString() != "";
            }
            catch { }

            eliminarHistorialToolStripItem.Enabled = tieneAccesorio && Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]);
        }

        private void cbxBuscar_DropDownClosed(object sender, EventArgs e) { ListarControlMtto(); }

        private void pNuevo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevo.Left = pNuevo.Left + (e.X - xClick);
                pNuevo.Top = pNuevo.Top + (e.Y - yClick);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = true;
            pNuevo.BringToFront();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = false;
            pNuevo.SendToBack();
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
                    pictureBox6_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
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
                if (cbHistorial.Checked == true) { nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE CONTROL DE MANTENIMIENTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx"); }
                if (cbHistorial.Checked == false) { nombre = System.IO.Path.Combine(desktop, "CONTROL DE MANTENIMIENTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx"); }
                dtgControlMtto.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimirRegistro_Click(object sender, EventArgs e)
        {
            DataTable dtListaMantenimiento2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto(2, idVehiculo);
            
            if (dtListaMantenimiento2.Rows.Count > 0)
            {
                DataTable dtListaMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto(2, idVehiculo);

                DataRow Espacio1, Espacio2, Placa, Operacion, Marca, Modelo;
                DataRow Espacio3, Espacio4, JefeArea, PlanerMtto, SupervisorMtto;

                Espacio1 = dtListaMantenimiento.NewRow();
                Espacio1[0] = " "; Espacio1[1] = " "; Espacio1[2] = " "; Espacio1[3] = " ";
                Espacio1[4] = " "; Espacio1[5] = " "; Espacio1[6] = " "; Espacio1[7] = " ";

                Espacio2 = dtListaMantenimiento.NewRow();
                Espacio2[0] = " "; Espacio2[1] = " "; Espacio2[2] = " "; Espacio2[3] = " ";
                Espacio2[4] = " "; Espacio2[5] = " "; Espacio2[6] = " "; Espacio2[7] = " ";

                Placa = dtListaMantenimiento.NewRow();
                Placa[0] = "PLACA: "; Placa[1] = lblPlaca.Text; Placa[2] = " "; Placa[3] = " ";
                Placa[4] = " "; Placa[5] = " "; Placa[6] = " "; Placa[7] = " ";

                Operacion = dtListaMantenimiento.NewRow();
                Operacion[0] = "OPERACIÓN: "; Operacion[1] = lblOperacion.Text; Operacion[2] = " "; Operacion[3] = " ";
                Operacion[4] = " "; Operacion[5] = " "; Operacion[6] = " "; Operacion[7] = " ";

                Marca = dtListaMantenimiento.NewRow();
                Marca[0] = "MARCA: "; Marca[1] = lblMarca.Text; Marca[2] = " "; Marca[3] = " ";
                Marca[4] = " "; Marca[5] = " "; Marca[6] = " "; Marca[7] = " ";

                Modelo = dtListaMantenimiento.NewRow();
                Modelo[0] = "MODELO: "; Modelo[1] = lblModelo.Text; Modelo[2] = " "; Modelo[3] = " ";
                Modelo[4] = " "; Modelo[5] = " "; Modelo[6] = " "; Modelo[7] = " ";

                Espacio3 = dtListaMantenimiento.NewRow();
                Espacio3[0] = " "; Espacio3[1] = " "; Espacio3[2] = " "; Espacio3[3] = " ";
                Espacio3[4] = " "; Espacio3[5] = " "; Espacio3[6] = " "; Espacio3[7] = " ";

                Espacio4 = dtListaMantenimiento.NewRow();
                Espacio4[0] = " "; Espacio4[1] = " "; Espacio4[2] = " "; Espacio4[3] = " ";
                Espacio4[4] = " "; Espacio4[5] = " "; Espacio4[6] = " "; Espacio4[7] = " ";

                JefeArea = dtListaMantenimiento.NewRow();
                JefeArea[0] = "JEFE AREA: "; JefeArea[1] = " "; JefeArea[2] = " "; JefeArea[3] = " ";
                JefeArea[4] = " "; JefeArea[5] = " "; JefeArea[6] = " "; JefeArea[7] = " ";

                PlanerMtto = dtListaMantenimiento.NewRow();
                PlanerMtto[0] = "PLANER MTTO: "; PlanerMtto[1] = " "; PlanerMtto[2] = " "; PlanerMtto[3] = " ";
                PlanerMtto[4] = " "; PlanerMtto[5] = " "; PlanerMtto[6] = " "; PlanerMtto[7] = " ";

                SupervisorMtto = dtListaMantenimiento.NewRow();
                SupervisorMtto[0] = "SUPERV. MTTO: "; SupervisorMtto[1] = " "; SupervisorMtto[2] = " "; SupervisorMtto[3] = " ";
                SupervisorMtto[4] = " "; SupervisorMtto[5] = " "; SupervisorMtto[6] = " "; SupervisorMtto[7] = " ";

                dtListaMantenimiento.Rows.Add(Espacio1);
                dtListaMantenimiento.Rows.Add(Espacio2);
                dtListaMantenimiento.Rows.Add(Placa);
                dtListaMantenimiento.Rows.Add(Operacion);
                dtListaMantenimiento.Rows.Add(Marca);
                dtListaMantenimiento.Rows.Add(Modelo);
                dtListaMantenimiento.Rows.Add(Espacio3);
                dtListaMantenimiento.Rows.Add(Espacio4);
                dtListaMantenimiento.Rows.Add(JefeArea);
                dtListaMantenimiento.Rows.Add(PlanerMtto);
                dtListaMantenimiento.Rows.Add(SupervisorMtto);

                dtgReporteAct.DataSource = dtListaMantenimiento;
                dgvReporteAct.BestFitColumns();
            }

            if (dtgReporteAct.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para imprimir.";
                m.ShowDialog();
            }
            else { dtgReporteAct.ShowPrintPreview(); }
        }

        private void btnActualizarAccesorio_Click(object sender, EventArgs e)
        {
            if (OpcionC == 0)
            {
                label12.Visible = true;
                dtpFechaC2.Value = DateTime.Now;
                dtpFechaC2.Visible = true;
                label11.Visible = true;
                txtKMC2.Clear();
                txtKMC2.Visible = true;
                pbGuardar.Visible = true;
                dgvControlMttoVista.OptionsSelection.MultiSelect = true;
                dgvControlMttoVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                OpcionC = 1;
            }
            else
            {
                label12.Visible = false;
                dtpFechaC2.Visible = false;
                label11.Visible = false;
                txtKMC2.Visible = false;
                pbGuardar.Visible = false;
                dgvControlMttoVista.OptionsSelection.MultiSelect = false;
                dgvControlMttoVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                OpcionC = 0;
            }
        }

        private void dtpFechaC2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtKMC2.Focus(); }
        }

        private void txtKMC2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { pbGuardar.Focus(); }
        }

        private void pbGuardar_Click(object sender, EventArgs e)
        {
            if (txtKMC2.Text.Length == 0)
            {
                MessageBox.Show("El KM de Cambio no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtKMC2.Focus();
                return;
            }
            else
            {
                int[] filas = dgvControlMttoVista.GetSelectedRows();

                if (filas.Length != 0)
                {
                    DataTable dtAgregar = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    int Correcto = 0;

                    if (MessageBox.Show("¿Desea actualizar las actividades de estos elementos?", "ACTUALIZAR ACTIVIDADES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < filas.Length; i++)
                        {
                            int idVehiculo = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(filas[i], "idVehiculo"));
                            int idAccesorio = Convert.ToInt32(dgvControlMttoVista.GetRowCellValue(filas[i], "idAccesorio"));
                            decimal Intervalo = Convert.ToDecimal(dgvControlMttoVista.GetRowCellValue(filas[i], "INTERVALO"));

                            dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControl(idVehiculo, idAccesorio, Convert.ToDecimal(txtKMC2.Text),
                                                                                                                                      dtpFechaC2.Value, Intervalo, Usuario);
                            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                            string NroRspta = respta.Substring(0, 1);
                            if (NroRspta == "0") { Correcto = Correcto + 1; }
                            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }

                        if (Correcto == filas.Length) { MessageBox.Show("0 = Las actividades han sido actualizadas exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                        ListarControlMtto();
                        OpcionC = 1;
                        btnActualizarAccesorio_Click(sender, e);
                    }
                }
            }
        }
    }
}
