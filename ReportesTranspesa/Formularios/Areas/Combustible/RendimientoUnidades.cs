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
    public partial class RendimientoUnidades : MetroFramework.Forms.MetroForm
    {
        private Microsoft.Office.Interop.Excel.Application app;
        public RendimientoUnidades()
        {
            InitializeComponent();
        }

        private void RendimientoUnidades_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            cboTipoUnidad.SelectedIndex = 0;
            cboSubtipoUnidad.SelectedIndex = 0;
        }

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();

            campoPivote = new PivotGridField("NUMEROPLACA", PivotArea.RowArea);
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoCANTIDAD = new PivotGridField("CANTIDAD GL", PivotArea.DataArea);
            campoCANTIDAD.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            PivotGridField campoKM = new PivotGridField("KM RECORRIDO", PivotArea.DataArea);
            campoKM.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            PivotGridField campoREND = new PivotGridField("RENDIMIENTO", PivotArea.DataArea);
            campoREND.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            PivotGridField campoTotal = new PivotGridField("MONTO", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] { campoPivote, campoDia, campoMes, campoAño, 
            campoKM, campoCANTIDAD, campoREND, campoTotal });
            campoPivote.AreaIndex = 0;
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            string TipoUnidad = "";
            #region Tipo_Unidad
            switch (cboTipoUnidad.SelectedIndex)
            {
                case 0:
                    TipoUnidad = "";
                break;

                case 1:
                TipoUnidad = "REMOLCADOR";
                break;

                case 2:
                TipoUnidad = "SEMIREMOLQUE";
                break;

                case 3:
                TipoUnidad = "LIVIANOS";
                break;

                case 4:
                TipoUnidad = "CARGADOR FRONTAL";
                break;
            }
            #endregion
            string SubTipoUnidad = "";
            #region SubTipo_Unidad
            switch (cboSubtipoUnidad.SelectedIndex)
            {
                case 0:
                    SubTipoUnidad = "";
                    break;

                case 1:
                    SubTipoUnidad = "TRACTO";
                    break;

                case 2:
                    SubTipoUnidad = "PLATAFORMA";
                    break;

                case 3:
                    SubTipoUnidad = "TOLVA";
                    break;

                case 4:
                    SubTipoUnidad = "FURGON";
                    break;

                case 5:
                    SubTipoUnidad = "CORTINERA";
                    break;

                case 6:
                    SubTipoUnidad = "TERMOKING";
                    break;

                case 7:
                    SubTipoUnidad = "CAMION";
                    break;

                case 8:
                    SubTipoUnidad = "CAMIONETA";
                    break;

                case 9:
                    SubTipoUnidad = "MOTO";
                    break;
            }
            #endregion
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsCombustibleBL.Instancia.GetDataRendimientoUnidad(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
               dtpFechaFin.Value.ToShortDateString() + " 23:59:59", TipoUnidad, SubTipoUnidad);
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

        private void dtgvData_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
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
                string nombre = System.IO.Path.Combine(desktop, "rendimientoUnidades " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
