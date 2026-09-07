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
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing.Imaging;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroCanaletas
{
    public partial class frmListaCanaletas : Form
    {
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int idCanaleta, idCCambio, Opcion;
        DataTable dtPermisos = new DataTable();

        public frmListaCanaletas()
        {
            InitializeComponent();
        }

        private void frmListaCanaletas_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaCanaletas");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        btnMaestroCanaletas.Enabled = true;
                        btnGuardarC.Enabled = true;
                    }
                    else
                    {
                        btnMaestroCanaletas.Enabled = false;
                        btnGuardarC.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        tsActualizarEstado.Enabled = true;
                        tsCambiarProg.Enabled = true;
                    }
                    else
                    {
                        tsActualizarEstado.Enabled = false;
                        tsCambiarProg.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                    {
                        tsQuitarProgramacion.Enabled = true;
                        tsEliminarProg.Enabled = true;
                        tsEliminarCambio.Enabled = true;
                    }
                    else
                    {
                        tsQuitarProgramacion.Enabled = false;
                        tsEliminarProg.Enabled = false;
                        tsEliminarCambio.Enabled = false;
                    }
                }
            }
            
            cbxSucursal.Text = "LARREA";
            cbxEstado2.Text = "TODOS";
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            ListarProgramaciones();
            ListarCambios();
        }


        public void ListarProgramaciones()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarProgramacion(cbxSucursal.Text, cbxEstado2.Text, txtCanaleta.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgControlCanaletas.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgvControlCanaletasVista.Columns["idCanaletaProg"].Visible = false;

                    dgvControlCanaletasVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlCanaletasVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvControlCanaletasVista.Columns["CANALETA"].Summary.Clear();
                    dgvControlCanaletasVista.Columns["CANALETA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CANALETA", "Total = {0}");

                    dgvControlCanaletasVista.BestFitColumns();
                }
            }
        }

        public void ListarCambios()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarCambios(cbxSucursal.Text, txtCanaleta.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgControlCambios.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgvControlCambiosView.Columns["idCCambio"].Visible = false;
                    dgvControlCambiosView.Columns["LongitudRestante"].Visible = false;

                    dgvControlCambiosView.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlCambiosView.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvControlCambiosView.Columns["CANALETA"].Summary.Clear();
                    dgvControlCambiosView.Columns["CANALETA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CANALETA", "Total = {0}");

                    dgvControlCambiosView.BestFitColumns();
                }
            }
        }

        public void ListarCambioDetalle()
        {
            DataTable dt = new System.Data.DataTable();
            dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarCambiosDetalle(idCCambio);
            dtgCambiosC.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                dgvCambiosCView.Columns["idCCambioDetalle"].Visible = false;

                dgvCambiosCView.Columns["LONGITUD"].Summary.Clear();
                dgvCambiosCView.Columns["LONGITUD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "LONGITUD", "Total = {0}");

                dgvCambiosCView.BestFitColumns();
            }
        }


        private void dgvControlCanaletasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "COMPLETADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "VENCIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                    e.Appearance.ForeColor = Color.White;
                }

                if (e.CellValue.ToString() == "PROGRAMADO") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
            }

            if (e.Column.FieldName == "DIAS_RESTANTES")
            {
                if (Convert.ToInt32(e.CellValue) >= 20) { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToInt32(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                    e.Appearance.ForeColor = Color.White;
                }

                if (Convert.ToInt32(e.CellValue) >= 0 && Convert.ToInt32(e.CellValue) < 20) { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }
            }
        }

        private void dtgControlCanaletas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Estado = dgvControlCanaletasVista.GetRowCellValue(dgvControlCanaletasVista.FocusedRowHandle, "ESTADO").ToString();

                if (Estado != "")
                {
                    if (Estado != "COMPLETADO")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsActualizarEstado.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsQuitarProgramacion.Enabled = true; }
                    }
                    else
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsActualizarEstado.Enabled = true; }
                        tsQuitarProgramacion.Enabled = false;
                    }
                }
                else
                {
                    tsActualizarEstado.Enabled = false;
                    tsQuitarProgramacion.Enabled = false;
                }
            }
            catch
            {
                tsActualizarEstado.Enabled = false;
                tsQuitarProgramacion.Enabled = false;
            }
        }

        private void dtgControlCambios_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Porcentaje = dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "PORCENTAJE (%)").ToString();

                if (Porcentaje != "")
                {
                    if (Porcentaje != "100.00")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnGuardarC.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsCambiarProg.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarProg.Enabled = true; }
                    }
                    else
                    {
                        btnGuardarC.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsCambiarProg.Enabled = true; }
                        tsEliminarProg.Enabled = false;
                    }
                }
                else
                {
                    btnGuardarC.Enabled = false;
                    tsCambiarProg.Enabled = false;
                    tsEliminarProg.Enabled = false;
                }
            }
            catch
            {
                btnGuardarC.Enabled = false;
                tsCambiarProg.Enabled = false;
                tsEliminarProg.Enabled = false;
            }
        }

        private void dtgControlCambios_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                idCCambio = Convert.ToInt32(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "idCCambio"));
                lblDescripcionC.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "CANALETA"));
                lblSucursalC.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "SUCURSAL"));
                lblLongitud.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "LongitudRestante"));
                dtpFechaC.Value = Convert.ToDateTime(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "FECHA_PROGRAMADA"));
                ListarCambioDetalle();

                pAgregarCambio.Visible = true;
                pAgregarCambio.BringToFront();
            }
            catch { }
        }

        private void btnMaestroCanaletas_Click(object sender, EventArgs e)
        {
            frmMaestroCanaletas frmMaestroCanaletas = new frmMaestroCanaletas();
            frmMaestroCanaletas.formulario = this;
            frmMaestroCanaletas.ShowDialog();
        }

        private void cbxSucursal_DropDownClosed(object sender, EventArgs e)
        {
            ListarProgramaciones();
            ListarCambios();
        }

        private void cbxEstado2_DropDownClosed(object sender, EventArgs e)
        {
            ListarProgramaciones();
            ListarCambios();
        }

        private void txtCanaleta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarProgramaciones();
                ListarCambios();
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarProgramaciones();
                ListarCambios();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarProgramaciones();
                ListarCambios();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarProgramaciones();
            ListarCambios();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgControlCanaletas.DataSource == null && dtgControlCambios.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                try
                {
                    dtgControlCanaletas.ForceInitialize();
                    dtgControlCambios.ForceInitialize();
                    compositeLink1.CreatePageForEachLink();

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                    XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                    options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    string nombre = System.IO.Path.Combine(desktop, "Plan de Mantenimiento de Canaletas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    compositeLink1.ExportToXlsx(nombre, options);
                    Process.Start(nombre);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsActualizarEstado_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            idCanaleta = Convert.ToInt32(dgvControlCanaletasVista.GetRowCellValue(dgvControlCanaletasVista.FocusedRowHandle, "idCanaletaProg"));
            lblDescripcion.Text = Convert.ToString(dgvControlCanaletasVista.GetRowCellValue(dgvControlCanaletasVista.FocusedRowHandle, "CANALETA"));
            lblSucursal.Text = Convert.ToString(dgvControlCanaletasVista.GetRowCellValue(dgvControlCanaletasVista.FocusedRowHandle, "SUCURSAL"));
            dtpNuevaFecha.Value = Convert.ToDateTime(dgvControlCanaletasVista.GetRowCellValue(dgvControlCanaletasVista.FocusedRowHandle, "FECHA_PROGRAMADA"));
            txtObservacion.Text = Convert.ToString(dgvControlCanaletasVista.GetRowCellValue(dgvControlCanaletasVista.FocusedRowHandle, "OBSERVACION"));
            label5.Visible = true;
            cbxEstado.Visible = true;
            cbxEstado.Text = "PROGRAMADO";

            pEditarProgramacion.Visible = true;
            pEditarProgramacion.BringToFront();
        }

        private void tsQuitarProgramacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar esta programación?", "ELIMINAR PROGRAMACION", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    idCanaleta = Convert.ToInt32(dgvControlCanaletasVista.GetRowCellValue(dgvControlCanaletasVista.FocusedRowHandle, "idCanaletaProg"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_EliminarProgramacion(1, idCanaleta);
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["Exito"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarProgramaciones(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El archivo seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pEditarProgramacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pEditarProgramacion.Left = pEditarProgramacion.Left + (e.X - xClick);
                pEditarProgramacion.Top = pEditarProgramacion.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            idCanaleta = -1;
            lblDescripcion.Text = "";
            lblSucursal.Text = "";
            txtObservacion.Clear();

            pEditarProgramacion.Visible = false;
            pEditarProgramacion.SendToBack();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1)        // EDITAR LIMPIEZA
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarProgramacion(2, idCanaleta, 0, dtpNuevaFecha.Value, cbxEstado.Text, txtObservacion.Text, Usuario);

                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarProgramaciones();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (Opcion == 2)        // EDITAR CAMBIO
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarCambios(2, idCCambio, 0, dtpNuevaFecha.Value, txtObservacion.Text, Usuario);

                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCambios();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsAniadirCambio_Click(object sender, EventArgs e)
        {
            idCCambio = Convert.ToInt32(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "idCCambio"));
            lblDescripcionC.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "CANALETA"));
            lblSucursalC.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "SUCURSAL"));
            lblLongitud.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "LongitudRestante"));
            dtpFechaC.Value = Convert.ToDateTime(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "FECHA_PROGRAMADA"));
            ListarCambioDetalle();

            pAgregarCambio.Visible = true;
            pAgregarCambio.BringToFront();
        }

        private void tsCambiarProg_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            idCCambio = Convert.ToInt32(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "idCCambio"));
            lblDescripcion.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "CANALETA"));
            lblSucursal.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "SUCURSAL"));
            dtpNuevaFecha.Value = Convert.ToDateTime(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "FECHA_PROGRAMADA"));
            txtObservacion.Text = Convert.ToString(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "OBSERVACION"));
            label5.Visible = false;
            cbxEstado.Visible = false;
            cbxEstado.Text = "PROGRAMADO";

            pEditarProgramacion.Visible = true;
            pEditarProgramacion.BringToFront();
        }

        private void tsEliminarProg_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar esta programación?", "ELIMINAR PROGRAMACION", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    idCCambio = Convert.ToInt32(dgvControlCambiosView.GetRowCellValue(dgvControlCambiosView.FocusedRowHandle, "idCCambio"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_EliminarProgramacion(2, idCCambio);
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["Exito"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarCambios(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El archivo seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvControlCambiosView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "PORCENTAJE (%)")
            {
                if (Convert.ToInt32(e.CellValue) >= 0 && Convert.ToInt32(e.CellValue) < 50)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                    e.Appearance.ForeColor = Color.White;
                }

                if (Convert.ToInt32(e.CellValue) >= 50 && Convert.ToInt32(e.CellValue) < 90) { e.Appearance.BackColor = Color.Yellow; }
                    
                if (Convert.ToInt32(e.CellValue) >= 90) { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }

            if (e.Column.FieldName == "DIAS_RESTANTES")
            {
                if (Convert.ToInt32(e.CellValue) >= 20) { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToInt32(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                    e.Appearance.ForeColor = Color.White;
                }

                if (Convert.ToInt32(e.CellValue) >= 0 && Convert.ToInt32(e.CellValue) < 20) { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }
            }
        }

        private void txtLongitud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarC_Click(sender, e); }
        }

        private void dtpFechaC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtLongitud.Focus(); }
        }

        private void pAgregarCambio_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pAgregarCambio.Left = pAgregarCambio.Left + (e.X - xClick);
                pAgregarCambio.Top = pAgregarCambio.Top + (e.Y - yClick);
            }
        }

        private void btnCerrarCambio_Click(object sender, EventArgs e)
        {
            idCCambio = -1;
            lblDescripcionC.Text = "";
            lblSucursalC.Text = "";
            lblLongitud.Text = "";

            txtLongitud.Clear();

            pAgregarCambio.Visible = false;
            pAgregarCambio.SendToBack();
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (txtLongitud.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la longitud.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLongitud.Focus();
                return;
            }
            else
            {
                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarCambioDetalle(1, idCCambio, 0, dtpFechaC.Value, Convert.ToDecimal(txtLongitud.Text));
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    ListarCambios();
                    ListarCambioDetalle();
                    txtLongitud.Clear();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgCambiosC_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Longitud = dgvCambiosCView.GetRowCellValue(dgvCambiosCView.FocusedRowHandle, "LONGITUD").ToString();

                if (Longitud != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarCambio.Enabled = true; }
                }
                else { tsEliminarCambio.Enabled = false; }
            }
            catch { tsEliminarCambio.Enabled = false; }
        }

        private void tsEliminarCambio_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este cambio?", "ELIMINAR CAMBIO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idCCambioDetalle = Convert.ToInt32(dgvCambiosCView.GetRowCellValue(dgvCambiosCView.FocusedRowHandle, "idCCambioDetalle"));
                    
                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarCambioDetalle(2, idCCambio, idCCambioDetalle, DateTime.Now, 0.00M);
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["Exito"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarCambios();
                        ListarCambioDetalle();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El archivo seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
