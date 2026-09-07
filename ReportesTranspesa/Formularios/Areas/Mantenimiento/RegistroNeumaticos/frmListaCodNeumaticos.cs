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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroNeumaticos
{
    public partial class frmListaCodNeumaticos : Form
    {
        DataTable dtPermisos = new DataTable();
        int xClick = 0, yClick = 0;
        int idVehiculo, Estado, idMovimientoN = 0;

        public frmListaCodNeumaticos()
        {
            InitializeComponent();
            cbxMarca.SelectedIndexChanged -= cbxMarca_SelectedIndexChanged;
        }

        private void cbxMarca_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMarca(); }

        private void frmListaCodNeumaticos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaCodNeumaticos");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevoNeumatico.Enabled = true; }
                else { btnNuevoNeumatico.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    tsActualizarNeumatico.Enabled = true;
                    tsNuevoMov.Enabled = true;
                    tsDesinstalarNeu.Enabled = true;
                }
                else
                {
                    tsActualizarNeumatico.Enabled = false;
                    tsNuevoMov.Enabled = false;
                    tsDesinstalarNeu.Enabled = false;
                }
            }
            
            CargarComboMarca();

            cbxMarca.Text = "TODAS";
            cbxEstado.Text = "TODOS";
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = new DateTime(dtpFechaFin.Value.Year, dtpFechaFin.Value.Month, 1).AddMonths(1);

            ListarRegistroNeumaticos();
            ListarMovimientos();
        }


        public void CargarComboMarca()
        {
            DataTable dtMarca = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos(1);
            cbxMarca.DataSource = dtMarca;
            cbxMarca.DisplayMember = "Descripcion";
            cbxMarca.ValueMember = "idMarca";
        }

        public void ListarRegistroNeumaticos()
        {
            DataTable dtListaNeumaticos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarNeumaticos(txtCodigo.Text, cbxMarca.Text, cbxEstado.Text);
            dtgNeumaticos.DataSource = dtListaNeumaticos;
            if (dtListaNeumaticos.Rows.Count > 0)
            {
                dgvNeumaticosVista.Columns["FECHA_INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvNeumaticosVista.Columns["FECHA_INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvNeumaticosVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvNeumaticosVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvNeumaticosVista.Columns["ESTADO"].Summary.Clear();
                dgvNeumaticosVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total = {0}");

                dgvNeumaticosVista.BestFitColumns();
            }
        }

        public void ListarMovimientos()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtListaMovimientos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarMovimientos(dtpFechaInicio.Text, dtpFechaFin.Text,
                                                                        txtCodigo2.Text, txtPlaca.Text);
                dtgMovimientos.DataSource = dtListaMovimientos;
                if (dtListaMovimientos.Rows.Count > 0)
                {
                    dgvMovimientosVista.Columns["idMovimientoN"].Visible = false;
                    dgvMovimientosVista.Columns["idVehiculo"].Visible = false;

                    dgvMovimientosVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMovimientosVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvMovimientosVista.Columns["FechaModifica"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMovimientosVista.Columns["FechaModifica"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvMovimientosVista.Columns["FECHA_ACTUAL"].Summary.Clear();
                    dgvMovimientosVista.Columns["FECHA_ACTUAL"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "FECHA_ACTUAL", "Total = {0}");

                    dgvMovimientosVista.BestFitColumns();
                }
            }
        }


        private void dtgNeumaticos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "ESTADO").ToString();

                if (Vacio != "")
                {
                    if (Vacio == "DISPONIBLE")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                        {
                            tsActualizarNeumatico.Enabled = true;
                            tsNuevoMov.Enabled = true;
                        }
                    }

                    if (Vacio == "ASIGNADO")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsActualizarNeumatico.Enabled = true; }
                        tsNuevoMov.Enabled = false;
                    }

                    if (Vacio == "DESCARTADO")
                    {
                        tsActualizarNeumatico.Enabled = false;
                        tsNuevoMov.Enabled = false;
                    }
                }
            }
            catch
            {
                tsActualizarNeumatico.Enabled = false;
                tsNuevoMov.Enabled = false;
            }
        }

        private void dgvNeumaticosVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "DISPONIBLE") { e.Appearance.BackColor = Color.FromArgb(128, 255, 255); }

                if (e.CellValue.ToString() == "ASIGNADO") { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (e.CellValue.ToString() == "DESCARTADO") { e.Appearance.BackColor = Color.LightCoral; }
            }
        }

        private void tsActualizarNeumatico_Click(object sender, EventArgs e)
        {
            frmNuevoNeumatico frmNuevoNeumatico = new frmNuevoNeumatico();
            frmNuevoNeumatico.label1.Text = "ACTUALIZAR REGISTRO NEUMÁTICO";
            frmNuevoNeumatico.frmListaCodNeumaticos = this;
            
            frmNuevoNeumatico.Opcion = 2;
            frmNuevoNeumatico.CargarComboMarca();
            frmNuevoNeumatico.CargarComboMedida();
            frmNuevoNeumatico.CargarComboModelo();

            frmNuevoNeumatico.txtCodigo.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "COD").ToString();
            frmNuevoNeumatico.txtDOT.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "DOT").ToString();
            frmNuevoNeumatico.cbxMarca.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "MARCA").ToString();
            frmNuevoNeumatico.cbxMedida.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "MEDIDA").ToString();
            frmNuevoNeumatico.cbxModelo.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "MODELO").ToString();
            frmNuevoNeumatico.dtpFechaInicio.Value = Convert.ToDateTime(dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "FECHA_INICIO"));
            frmNuevoNeumatico.cbxTipo.Text = "1R";
            frmNuevoNeumatico.cbxTipo_DropDownClosed(sender, e);
            frmNuevoNeumatico.txtKM.Text = "0.00";
            frmNuevoNeumatico.txtPrecio.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "PRECIO").ToString();
            frmNuevoNeumatico.txtNSK.Text = "0.00";

            frmNuevoNeumatico.txtCodigo.Enabled = false;
            frmNuevoNeumatico.ShowDialog();
        }

        private void btnNuevoNeumatico_Click(object sender, EventArgs e)
        {
            frmNuevoNeumatico frmNuevoNeumatico = new frmNuevoNeumatico();
            frmNuevoNeumatico.label1.Text = "REGISTRAR NEUMÁTICO NUEVO";
            frmNuevoNeumatico.frmListaCodNeumaticos = this;

            frmNuevoNeumatico.CargarComboMarca();
            frmNuevoNeumatico.CargarComboMedida();
            frmNuevoNeumatico.CargarComboModelo();
            
            frmNuevoNeumatico.Opcion = 1;
            frmNuevoNeumatico.dtpFechaInicio.Value = DateTime.Now;
            frmNuevoNeumatico.cbxTipo.Enabled = false;
            frmNuevoNeumatico.cbxTipo.Text = "ORIGINAL";
            frmNuevoNeumatico.txtNSK.Text = "0.00";
            frmNuevoNeumatico.txtKM.Text = "0.00";
            frmNuevoNeumatico.txtCodigo.Focus();

            frmNuevoNeumatico.ShowDialog();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRegistroNeumaticos(); }
        }

        private void cbxMarca_DropDownClosed(object sender, EventArgs e) { ListarRegistroNeumaticos(); }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarRegistroNeumaticos(); }

        private void tsNuevoMov_Click(object sender, EventArgs e)
        {
            string Vacio = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "ESTADO").ToString();

            if (Vacio == "DISPONIBLE")
            {
                lblInstalacion.Text = "INSTALACIÓN DE NEUMÁTICO";
                Estado = 1;
                idMovimientoN = 0;
                txtPlaca2.Enabled = true;
                cbxTipo.Enabled = true;
                txtPosicion.Enabled = true;
                txtCOD.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "COD").ToString();
                txtMarca.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "MARCA").ToString();
                txtModelo.Text = dgvNeumaticosVista.GetRowCellValue(dgvNeumaticosVista.FocusedRowHandle, "MODELO").ToString();
                dtpFecha.Value = DateTime.Now;
                cbxTipo.Text = "1R";

                pInstalaciones.Location = new System.Drawing.Point(725, 260);
                pInstalaciones.Visible = true;
                pInstalaciones.BringToFront();
                txtPlaca2.Focus();
            }
        }

        private void pInstalaciones_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pInstalaciones.Left = pInstalaciones.Left + (e.X - xClick);
                pInstalaciones.Top = pInstalaciones.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            lblInstalacion.Text = "";
            Estado = -1; idMovimientoN = 0;

            txtPlaca2.Clear();
            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            txtCOD.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            cbxTipo.Text = "1R";
            txtPosicion.Clear();
            txtKM.Clear();
            txtNSK.Clear();

            pInstalaciones.Visible = false;
            pInstalaciones.SendToBack();
        }

        private void txtPlaca2_Enter(object sender, EventArgs e) { txtPlaca2.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtPlaca2.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 80;
            lstPlaca.Columns[2].Width = 100;
            lstPlaca.Columns[3].Width = 0;
            lstPlaca.Columns[4].Width = 100;
            lstPlaca.Columns[5].Width = 0;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
            }
        }

        private void txtPlaca2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void txtPlaca2_Leave(object sender, EventArgs e) { txtPlaca2.BackColor = Color.White; }

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
                txtPlaca2.Text = ItemActual.SubItems[1].Text;
                cbxTipo.Focus();

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idVehiculo = Int32.Parse(ItemActual.Text);
            txtPlaca2.Text = ItemActual.SubItems[1].Text;
            cbxTipo.Focus();

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarRegistroNeumaticos(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgNeumaticos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE NEUMÁTICOS " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgNeumaticos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void cbxTipo_DropDownClosed(object sender, EventArgs e) { txtPosicion.Focus(); }

        private void txtPosicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFecha.Focus(); }
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtKM.Focus(); }
        }

        private void txtKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtNSK.Focus(); }
        }

        private void txtNSK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar.Focus(); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtPlaca2.Text.Length == 0 || txtPosicion.Text.Length == 0 || txtKM.Text.Length == 0 || txtNSK.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtPlaca2.Text.Length == 0) { txtPlaca2.Focus(); }
                else
                {
                    if (txtPosicion.Text.Length == 0) { txtPosicion.Focus(); }
                    else
                    {
                        if (txtKM.Text.Length == 0) { txtKM.Focus(); }
                        else { txtNSK.Focus(); }
                    }
                }
                return;
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_InstalarDesinstalar(Estado, idMovimientoN, txtCOD.Text, txtMarca.Text, idVehiculo, cbxTipo.Text,
                                                         Convert.ToInt32(txtPosicion.Text), dtpFecha.Value, Convert.ToDecimal(txtKM.Text), Convert.ToDecimal(txtNSK.Text), Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarRegistroNeumaticos();
                    ListarMovimientos();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgMovimientos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "PLACA").ToString();

                if (Vacio != "")
                {
                    string NSKD = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "NSK_D").ToString();

                    if (NSKD == "0.00")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsDesinstalarNeu.Enabled = true; }
                    }
                    else { tsDesinstalarNeu.Enabled = false; } 
                }
                else { tsDesinstalarNeu.Enabled = false; }
            }
            catch { tsDesinstalarNeu.Enabled = false; }
        }

        private void dgvMovimientosVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "FECHA_ACTUAL") { e.Appearance.BackColor = Color.FromArgb(0, 192, 192); }

            if (e.Column.FieldName == "KM_ACTUAL") { e.Appearance.BackColor = Color.FromArgb(0, 192, 192); }
        }

        private void tsDesinstalarNeu_Click(object sender, EventArgs e)
        {
            lblInstalacion.Text = "DESINSTALACIÓN DE NEUMÁTICO";
            Estado = 2;
            idMovimientoN = Convert.ToInt32(dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "idMovimientoN"));
            idVehiculo = Convert.ToInt32(dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "idVehiculo"));
            txtPlaca2.Text = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "PLACA").ToString();
            txtPlaca2.Enabled = false;
            txtPosicion.Enabled = false;
            cbxTipo.Enabled = false;
            txtCOD.Text = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "COD").ToString();
            txtMarca.Text = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "MARCA").ToString();
            txtModelo.Text = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "MODELO").ToString();
            cbxTipo.Text = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "TIPO").ToString();
            txtPosicion.Text = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "POSICION").ToString();
            txtKM.Text = dgvMovimientosVista.GetRowCellValue(dgvMovimientosVista.FocusedRowHandle, "KM_ACTUAL").ToString();
            dtpFecha.Value = DateTime.Now;

            pInstalaciones.Location = new System.Drawing.Point(725, 260);
            pInstalaciones.Visible = true;
            pInstalaciones.BringToFront();
            dtpFecha.Focus();
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMovimientos(); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMovimientos(); }
        }

        private void txtCodigo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMovimientos(); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMovimientos(); }
        }

        private void btnBuscar2_Click(object sender, EventArgs e) { ListarMovimientos(); }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            if (dtgMovimientos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE MOVIMIENTOS " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgMovimientos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
