using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using Negocio;
using Comun;
using ReportesTranspesa.Sistema;
using System.Globalization;
using DevExpress.Utils;
using System.Diagnostics;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmActivosDeSegundoUso : Form
    {
        List<string> checkItems = new List<string>();
        List<string> uncheckItems = new List<string>();
        public int xClick = 0, yClick = 0;

        public frmActivosDeSegundoUso()
        {
            InitializeComponent();
        }

        private void frmActivosDeSegundoUso_Load(object sender, EventArgs e)
        {
            try
            {
                cbxOperacion.Text = "TODAS";
                cbxSucursal.Text = "TODAS";
                dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
                dtpFechaFin.Value = DateTime.Now;

                CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
                ListarSegundoUsoRequerimientos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void CargarRepuestosSegundoUso()
        {
            dtgListaItems.DataSource = null;
            dgvListaItemsView.Columns.Clear();

            DataTable dtRepuesto = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarRepuestosParaSegundoUso(txtNombreItem.Text);
            dtgListaItems.DataSource = dtRepuesto;

            if (dtRepuesto.Rows.Count > 0)
            {
                dgvListaItemsView.Columns["Fecha"].Visible = false;
                dgvListaItemsView.Columns["Usuario"].Visible = false;
                
                dgvListaItemsView.Columns["Nombre"].Summary.Clear();
                dgvListaItemsView.Columns["Nombre"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "Nombre", "Total: {0}");

                dgvListaItemsView.BestFitColumns();
            }
        }

        public void CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado()
        {
            dtgvData.DataSource = null;
            dgvVinculo.Columns.Clear();

            DataTable dtVinculo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Listar_VinculoRepuesto_ActivoSegundoUso_Empleado(dtpFechaInicio.Text, dtpFechaFin.Text, cbxSucursal.Text,
                                                               txtActivo.Text, txtOT.Text, cbxOperacion.Text, txtEmpleado.Text);
            if (dtVinculo.Rows.Count > 0)
            {
                dtgvData.DataSource = dtVinculo;

                dgvVinculo.Columns["idActivo"].Visible = false;
                dgvVinculo.Columns["idEmpleado"].Visible = false;

                dgvVinculo.Columns["FechaRegistro"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvVinculo.Columns["FechaRegistro"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvVinculo.Columns["NombreActivo"].Summary.Clear();
                dgvVinculo.Columns["NombreActivo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                dgvVinculo.FixedLineWidth = 1;
                dgvVinculo.BestFitColumns();
            }
        }

        public void ListarSegundoUsoRequerimientos()
        {
            dtgvLogistica.DataSource = null;
            dgvLogisticaV.Columns.Clear();

            DataTable dtRequerimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientos(dtpFechaInicio.Text, dtpFechaFin.Text, cbxSucursal.Text,
                                                                     txtActivo.Text, txtOT.Text, txtEmpleado.Text);
            if (dtRequerimiento.Rows.Count > 0)
            {
                dtgvLogistica.DataSource = dtRequerimiento;

                dgvLogisticaV.Columns["idActivo"].Visible = false;
                dgvLogisticaV.Columns["idEmpleado"].Visible = false;

                dgvLogisticaV.Columns["FechaRegistro"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvLogisticaV.Columns["FechaRegistro"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvLogisticaV.Columns["NombreActivo"].Summary.Clear();
                dgvLogisticaV.Columns["NombreActivo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                dgvLogisticaV.FixedLineWidth = 1;
                dgvLogisticaV.BestFitColumns();
            }
        }


        private void btnRegistrarActivos_Click(object sender, EventArgs e)
        {
            frmAsignarActivoSegundoUso frmAsignarActivoSegundoUso = new frmAsignarActivoSegundoUso();
            frmAsignarActivoSegundoUso.frmActivosDeSegundoUso = this;

            frmAsignarActivoSegundoUso.cbxSucursal.Text = "TRUJILLO";
            frmAsignarActivoSegundoUso.cbxSucursal_DropDownClosed(sender, e);
            frmAsignarActivoSegundoUso.Show(this);
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
                ListarSegundoUsoRequerimientos();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
                ListarSegundoUsoRequerimientos();
            }
        }

        private void cbxSucursal_DropDownClosed(object sender, EventArgs e)
        {
            CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
            ListarSegundoUsoRequerimientos();
        }

        private void txtActivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado(); }
        }

        private void txtOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
                ListarSegundoUsoRequerimientos();
            }
        }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado(); }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
                ListarSegundoUsoRequerimientos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
            ListarSegundoUsoRequerimientos();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvData.ForceInitialize();
                dgvVinculo.BestFitColumns();
                dtgvLogistica.ForceInitialize();
                dgvLogisticaV.BestFitColumns();
                dgvVinculo.OptionsView.ColumnAutoWidth = false;
                dgvLogisticaV.OptionsView.ColumnAutoWidth = false;
                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                options.ShowGridLines = true;
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE ACTIVOS - SEGUNDO USO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception) { throw; }
        }

        private void tsMaestroItem_Click(object sender, EventArgs e)
        {
            CargarRepuestosSegundoUso();
            pListarItems.Location = new System.Drawing.Point(59, 221);

            pListarItems.Visible = true;
            pListarItems.BringToFront();
        }

        private void pListarItems_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pListarItems.Left = pListarItems.Left + (e.X - xClick);
                pListarItems.Top = pListarItems.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pListarItems.Visible = false;
            pListarItems.SendToBack();
        }

        private void txtNombreItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarRepuestosSegundoUso(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dtgListaItems.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Repuestos de Segundo Uso " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaItems.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsRegistrarActivo_Click(object sender, EventArgs e)
        {
            try
            {
                frmRegistrarActivoSegundoUso OPEN = new frmRegistrarActivoSegundoUso();
                OPEN.tipoOperacion = Utilitario.TipoOperacion.Registrar;
                OPEN.cbxSucursal.Text = "TRUJILLO";
                OPEN.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsHistorialDesvinculados_Click(object sender, EventArgs e)
        {
            try
            {
                frmVerSegundoUsoDesvinculados open = new frmVerSegundoUsoDesvinculados();
                open.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void desvincularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string idActivo = Convert.ToString(dgvVinculo.GetFocusedRowCellValue("idActivo"));
                string codigoRepuesto = Convert.ToString(dgvVinculo.GetFocusedRowCellValue("CodigoRepuesto"));
                string OT = Convert.ToString(dgvVinculo.GetFocusedRowCellValue("OT"));
                string TipoOperacion = Convert.ToString(dgvVinculo.GetFocusedRowCellValue("TipoOperacion"));
                int idEmpleado = Convert.ToInt32(dgvVinculo.GetFocusedRowCellValue("idEmpleado"));
                String Respuesta = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Motivo", "Motivo de Desvinculacion");

                if (Respuesta.Length > 0)
                {
                    if (clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_DesvincularActivoSegundoUso_Repuesto_Empleado(idActivo, codigoRepuesto, idEmpleado, Respuesta, OT, TipoOperacion))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarVinculo_Repuestos_ActuvoSegundoUso_Empleado();
                        CargarRepuestosSegundoUso();
                    }
                    else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void tsDesvincularReq_Click(object sender, EventArgs e)
        {
            try
            {
                int idActivo = Convert.ToInt32(dgvLogisticaV.GetFocusedRowCellValue("idActivo"));
                string Requerimiento = Convert.ToString(dgvLogisticaV.GetFocusedRowCellValue("Requerimiento"));
                int idEmpleado = Convert.ToInt32(dgvLogisticaV.GetFocusedRowCellValue("idEmpleado"));
                String Respuesta = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Motivo", "Motivo de Desvinculacion");

                if (Respuesta.Length > 0)
                {
                    DataTable dtSegundoUsoR = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtSegundoUsoR = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_DesvincularRequerimientos(idActivo, idEmpleado, Requerimiento, Respuesta, Usuario);
                    respta = Convert.ToString(dtSegundoUsoR.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);

                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarSegundoUsoRequerimientos();
                        CargarRepuestosSegundoUso();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { }
        }
    }
}
