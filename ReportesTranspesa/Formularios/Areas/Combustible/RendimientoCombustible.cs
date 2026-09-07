using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using Negocio;
using ReportesTranspesa.Sistema;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class RendimientoCombustible : MetroFramework.Forms.MetroForm
    {
        public RendimientoCombustible()
        {
            InitializeComponent();
        }

        private void RendimientoCombustible_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            //PivotGridField campoPivote2 = new PivotGridField();
            //PivotGridField campoPivote3 = new PivotGridField();

            //campoPivote = new PivotGridField("PLACA", PivotArea.RowArea);
            //campoPivote2 = new PivotGridField("MARCA", PivotArea.RowArea);
            //campoPivote3 = new PivotGridField("CLIENTE", PivotArea.RowArea);

            campoPivote = new PivotGridField("PLACA", PivotArea.RowArea);
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("RENDIMIENTO", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
            campoDia,campoMes,campoAño,campoTotal});
            campoPivote.AreaIndex = 0;
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
            /*
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("MONTO TOTAL", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
           
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, campoPivote2,
                campoDia,campoMes,campoAño,campoTotal});
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                campoDia,campoMes,campoAño,campoTotal});
           
            campoPivote.AreaIndex = 0;
            campoPivote2.AreaIndex = 1;
            
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;*/
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsCombustibleBL.Instancia.GetDataRendimiento(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
               dtpFechaFin.Value.ToShortDateString() + " 23:59:59", txtPlaca.Text, txtCliente.Text);
            if (dt.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvData.DataSource = dt;
                dtgvData.BestFitRowArea();
                chartControl1.DataSource = dtgvData;
                chartControl1.PivotGridDataSourceOptions.MaxAllowedPointCountInSeries = 100;
                chartControl1.PivotGridDataSourceOptions.MaxAllowedSeriesCount = 100;
                chartControl1.PivotGridDataSourceOptions.RetrieveDataByColumns = false;
                chartControl1.CrosshairEnabled = DefaultBoolean.False;
                chartControl1.ToolTipEnabled = DefaultBoolean.True;
                ToolTipController controller = new ToolTipController();
                chartControl1.ToolTipController = controller;
                controller.ShowBeak = true;
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog(); 
            }
        }

        private void pvgData_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.Appearance.BackColor = Color.DarkRed;
            if (e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.Appearance.BackColor = Color.DarkRed;
            if (e.ColumnValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.Appearance.BackColor = Color.DarkRed;
            if (e.ColumnValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.Appearance.BackColor = Color.DarkRed;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Rendimiento Diario de Unidades del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                chartControl1.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvData.ShowPrintPreview();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }
    }
}
