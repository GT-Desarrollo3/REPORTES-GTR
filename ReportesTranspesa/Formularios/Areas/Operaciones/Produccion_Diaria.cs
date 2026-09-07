using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using DevExpress.XtraPivotGrid;
using ReportesTranspesa.Sistema;
using System.IO;
using DevExpress.DataAccess.ConnectionParameters;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Produccion_Diaria : MetroFramework.Forms.MetroForm
    {
        public Produccion_Diaria()
        {
            InitializeComponent();
        }

        private void Operaciones_Produccion_Diaria_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetPeriodoProduccDiaria();
            cboPeriodo.DataSource = dt;
            cboPeriodo.ValueMember = "Id";
            cboPeriodo.DisplayMember = "Descripcion";
            cboPeriodo.SelectedIndex = dt.Rows.Count - 1;
            PivotGridField campoSucursal = new PivotGridField("Sucursal", PivotArea.RowArea);
            PivotGridField campoCuenta = new PivotGridField("Cuenta", PivotArea.RowArea);
            campoCuenta.Caption = "Cuenta";
            PivotGridField campoDia = new PivotGridField("Dia", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoTotal = new PivotGridField("Total", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c0";
            pvgData.Fields.AddRange(new PivotGridField[] {campoSucursal, campoCuenta, 
            campoDia, campoTotal});
            campoSucursal.AreaIndex = 0;
            campoCuenta.AreaIndex = 1;
            campoDia.AreaIndex = 0;
            dashboardViewer1.Dashboard = null;
            ////dashboardViewer1.LoadDashboard(@"C:\Transpesa\Dashboards\dashboard.xml");
            ////dashboardViewer1.LoadDashboard(@"..\..\dashboard.xml");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            pvgData.DataSource = null;
            //dashboardViewer1.Dashboard = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetAllDataProduccDiaria(Convert.ToInt32(cboPeriodo.SelectedValue));
            pvgData.DataSource = dt;
            pvgData.RefreshData();
            //chartControl1.DataSource = pvgData;
            //chartControl1.PivotGridDataSourceOptions.MaxAllowedPointCountInSeries = 100;
            //chartControl1.PivotGridDataSourceOptions.MaxAllowedSeriesCount = 100;
            DevExpress.DashboardCommon.Dashboard d = new DevExpress.DashboardCommon.Dashboard();
            d.LoadFromXml(@"C:\Transpesa\Dashboards\dashboard.xml");
            dashboardViewer1.Dashboard = d;
            dashboardViewer1.Dashboard.Parameters["PeriodoId"].Value = Convert.ToInt32(cboPeriodo.SelectedValue);
            dashboardViewer1.ReloadData();
            dashboardViewer1.Refresh();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (pvgData.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte Diario " + cboPeriodo.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                if (File.Exists(nombre))
                {
                    File.Delete(nombre);
                }
                pvgData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (pvgData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                pvgData.Print();
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

        //private void dashboardViewer1_ConfigureDataConnection_1(object sender, DevExpress.DataAccess.ConfigureDataConnectionEventArgs e)
        //{
        //    SqlServerConnectionParametersBase parameters = e.ConnectionParameters as SqlServerConnectionParametersBase;
        //    if (parameters != null)
        //    {
        //        parameters.ServerName = "192.168.4.234";
        //        parameters.UserName = "sa";
        //        parameters.DatabaseName = "SPRING";
        //        parameters.Password = "s!stema5";
        //    }
        //}

        private void dashboardViewer1_ConfigureDataConnection_1(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {
            SqlServerConnectionParametersBase parameters = e.ConnectionParameters as SqlServerConnectionParametersBase;
            if (parameters != null)
            {
                parameters.ServerName = "192.168.4.234";
                parameters.UserName = "sa";
                parameters.DatabaseName = "SPRING";
                parameters.Password = "s!stema5";
            }
        }

    }
}
