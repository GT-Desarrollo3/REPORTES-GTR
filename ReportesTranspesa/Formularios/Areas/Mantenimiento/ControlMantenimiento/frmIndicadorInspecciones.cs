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
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using System.Windows.Forms.DataVisualization.Charting;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmIndicadorInspecciones : Form
    {
        DataTable dtIndicador = new DataTable();
        DataTable dtResumen = new DataTable();
        DataTable dtProg = new DataTable();
        DataTable dtIns = new DataTable();
        int xClick = 0, yClick = 0;
        int totalViajes, totalInspecciones;

        public frmIndicadorInspecciones()
        {
            InitializeComponent();
            dgvDatoIndicadorVista.CustomSummaryCalculate += dgvDatoIndicadorVista_CustomSummaryCalculate;
        }

        private void frmIndicadorInspecciones_Load(object sender, EventArgs e)
        {
            dtpFechaI.Value = DateTime.Now;
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            
            cbxOperaciones.Text = "TODOS";
            cbxOperaciones2.Text = "LINDLEY";

            ListarIndicadorI();
        }


        public void ListarIndicadorI()
        {
            int focusedRowHandle = dgvDatoIndicadorVista.FocusedRowHandle;
            dtIndicador = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorInspeccion(1, dtpFechaI.Value, dtpFechaInicio.Text, dtpFechaFin.Text, txtPlaca.Text, cbxOperaciones.Text);
            dtgDatoIndicador.DataSource = dtIndicador;
            
            if (dtIndicador.Rows.Count > 0)
            {
                dgvDatoIndicadorVista.Columns["PLACA"].Summary.Clear();
                dgvDatoIndicadorVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                dgvDatoIndicadorVista.Columns["PROGRAMACION"].Summary.Clear();
                dgvDatoIndicadorVista.Columns["PROGRAMACION"].Summary.Add(SummaryItemType.Custom, "PROGRAMACION", "VIAJES = {0}");

                dgvDatoIndicadorVista.Columns["INSPECCION_DIA"].Summary.Clear();
                dgvDatoIndicadorVista.Columns["INSPECCION_DIA"].Summary.Add(SummaryItemType.Custom, "INSPECCION_DIA", "INSPECCION_DIA = {0}");

                dgvDatoIndicadorVista.Columns["INSPECCION_NOCHE"].Summary.Clear();
                dgvDatoIndicadorVista.Columns["INSPECCION_NOCHE"].Summary.Add(SummaryItemType.Custom, "INSPECCION_NOCHE", "INSPECCION_NOCHE = {0}");
                
                dgvDatoIndicadorVista.BestFitColumns();

                if (focusedRowHandle >= 0 && focusedRowHandle < dgvDatoIndicadorVista.RowCount) { dgvDatoIndicadorVista.FocusedRowHandle = focusedRowHandle; }
            }
        }

        public void ListarResumenI()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtResumen = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorInspeccion(2, dtpFechaI.Value, dtpFechaInicio.Text, dtpFechaFin.Text, "", cbxOperaciones2.Text);
                dtgResumenI.DataSource = dtResumen;

                if (dtResumen.Rows.Count > 0)
                {
                    dgvResumenIVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvResumenIVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvResumenIVista.BestFitColumns();
                }

                chart1.DataSource = dtResumen;
                chart1.Series[0].ChartType = SeriesChartType.Line;
                chart1.Series["Series1"].XValueMember = "FECHA";
                chart1.Series["Series1"].YValueMembers = "PORCENTAJE (%)";
                chart1.Series["Series1"].IsXValueIndexed = true;

                // Grosor de la línea
                chart1.Series["Series1"].BorderWidth = 4;

                // Marcadores en cada punto
                chart1.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
                chart1.Series["Series1"].MarkerSize = 10;

                // Mostrar los números de cada punto
                chart1.Series["Series1"].IsValueShownAsLabel = true;

                // Que las fechas se vean como fechas
                /*
                chart1.Series["Series1"].XValueType = ChartValueType.DateTime;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";
                */ 
            }
        }

        public void ListarProgramaciones()
        {
            dtProg = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorUnidades(1, Convert.ToDateTime(lblFecha.Text), lblOperacion.Text);
            dtgListaTractos.DataSource = dtProg;

            if (dtProg.Rows.Count > 0)
            {
                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                dgvListaTractosView.BestFitColumns();
            }
        }

        public void ListarInspecciones()
        {
            dtProg = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorUnidades(2, Convert.ToDateTime(lblFecha.Text), lblOperacion.Text);
            dtgListaTractos.DataSource = dtProg;

            if (dtProg.Rows.Count > 0)
            {
                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                dgvListaTractosView.BestFitColumns();
            }
        }


        private void dtpFechaI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIndicadorI(); }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIndicadorI(); }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { ListarIndicadorI(); }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarIndicadorI(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgDatoIndicador.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "INDICADOR DE INSPECCIONES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgDatoIndicador.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvDatoIndicadorVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PROGRAMACION")
            {
                if (Convert.ToString(e.CellValue) == "VIAJE") { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }
            }

            if (e.Column.FieldName == "INSPECCION_DIA")
            {
                if (Convert.ToString(e.CellValue) == "INSPECCION") { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }
            }

            if (e.Column.FieldName == "INSPECCION_NOCHE")
            {
                if (Convert.ToString(e.CellValue) == "INSPECCION") { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }
            }
        }

        private void dgvDatoIndicadorVista_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            GridSummaryItem item = e.Item as GridSummaryItem;

            if (!e.IsTotalSummary)
                return;

            if (e.SummaryProcess != CustomSummaryProcess.Finalize)
                return;

            if (item.FieldName == "PROGRAMACION")
            {
                int viajes = 0;

                for (int i = 0; i < view.DataController.ListSourceRowCount; i++)
                {
                    var val = view.DataController.GetListSourceRowValue(i, "PROGRAMACION");
                    if (val != null && val.ToString() == "VIAJE")
                        viajes++;
                }

                e.TotalValue = viajes;
            }
            else if (item.FieldName == "INSPECCION_DIA")
            {
                int inspecciones = 0;

                for (int i = 0; i < view.DataController.ListSourceRowCount; i++)
                {
                    var val = view.DataController.GetListSourceRowValue(i, "INSPECCION_DIA");
                    if (val != null && val.ToString() == "INSPECCION")
                        inspecciones++;
                }

                e.TotalValue = inspecciones;
            }
            else if (item.FieldName == "INSPECCION_NOCHE")
            {
                int inspecciones = 0;

                for (int i = 0; i < view.DataController.ListSourceRowCount; i++)
                {
                    var val = view.DataController.GetListSourceRowValue(i, "INSPECCION_NOCHE");
                    if (val != null && val.ToString() == "INSPECCION")
                        inspecciones++;
                }

                e.TotalValue = inspecciones;
            }
        }

        private void dgvResumenIVista_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string col = e.Column.FieldName;
            pListarUnidades.Location = new System.Drawing.Point(114, 218);

            lblOperacion.Text = Convert.ToString(dgvResumenIVista.GetRowCellValue(dgvResumenIVista.FocusedRowHandle, "OPERACION"));
            lblFecha.Text = Convert.ToString(dgvResumenIVista.GetRowCellValue(dgvResumenIVista.FocusedRowHandle, "FECHA"));

            if (col == "PROG")
            {
                ListarProgramaciones();
                label11.Text = "UNIDADES PROGRAMADAS";
                pListarUnidades.Visible = true;
                pListarUnidades.BringToFront();
            }

            if (col == "INSPE")
            {
                ListarInspecciones();
                label11.Text = "UNIDADES INSPECCIONADAS";
                pListarUnidades.Visible = true;
                pListarUnidades.BringToFront();
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pListarUnidades.Visible = false;
            pListarUnidades.SendToBack();

            lblOperacion.Text = "";
            lblFecha.Text = "";
        }

        private void pListarUnidades_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pListarUnidades.Left = pListarUnidades.Left + (e.X - xClick);
                pListarUnidades.Top = pListarUnidades.Top + (e.Y - yClick);
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResumenI(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResumenI(); }
        }

        private void btnBuscar2_Click(object sender, EventArgs e) { ListarResumenI(); }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            if (dtgResumenI.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "RESUMEN DE INDICADORES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgResumenI.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
