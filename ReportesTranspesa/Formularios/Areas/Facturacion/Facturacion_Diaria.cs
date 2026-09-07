using System;
using System.Drawing;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class Facturacion_Diaria : MetroFramework.Forms.MetroForm
    {
        public Facturacion_Diaria()
        {
            InitializeComponent();
        }

        public string usuario;
        private void Facturacion_Diaria_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            
            //if (usuario == "MNIQUIN" || usuario == "VLOPEZ") 
            //{
            //    this.Theme = MetroFramework.MetroThemeStyle.Light;
            //    gbFecha.BackgroundColor = Color.White;
            //    gbCompania.BackgroundColor = Color.White;
            //    dtpFechaIni.Theme = MetroFramework.MetroThemeStyle.Light;
            //    dtpFechaFin.Theme = MetroFramework.MetroThemeStyle.Light;
            //    chkTranspesa.Theme = MetroFramework.MetroThemeStyle.Light;
            //    chkBra.Theme = MetroFramework.MetroThemeStyle.Light;
            //    rbFechaEmision.Theme = MetroFramework.MetroThemeStyle.Light;
            //    rbFechaPreparacion.Theme = MetroFramework.MetroThemeStyle.Light;
            //    lblInicio.Theme = MetroFramework.MetroThemeStyle.Light;
            //    lblFin.Theme = MetroFramework.MetroThemeStyle.Light;
            //    btnBuscar.Appearance.BackColor = Color.White;
            //    btnBuscar.Appearance.BackColor2 = Color.White;
            //    btnBuscar.Appearance.BorderColor = Color.White;
            //    btnExcel.Appearance.BackColor = Color.White;
            //    btnExcel.Appearance.BackColor2 = Color.White;
            //    btnExcel.Appearance.BorderColor = Color.White;
            //    btnImprimir.Appearance.BackColor = Color.White;
            //    btnImprimir.Appearance.BackColor2 = Color.White;
            //    btnImprimir.Appearance.BorderColor = Color.White;
            //    dtgvData.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            //    chartControl1.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            //}
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

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            PivotGridField campoPivote2 = new PivotGridField();

            if (rbPorFacturador.Checked == true)
            {
                campoPivote = new PivotGridField("FACTURADOR", PivotArea.RowArea);
            }
            else
            {
                if (rbPorCliente.Checked == true)
                {
                    campoPivote = new PivotGridField("CLIENTE", PivotArea.RowArea);
                }
                else 
                {
                    if (rbPorUN.Checked == true)
                    {
                        campoPivote = new PivotGridField("UNIDAD DE NEGOCIO", PivotArea.RowArea);
                        campoPivote2 = new PivotGridField("CLIENTE", PivotArea.RowArea);
                    }
                    else 
                    {
                        campoPivote = new PivotGridField("SERVICIO", PivotArea.RowArea);
                    }
                }
            }

            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("MONTO TOTAL", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            if (rbPorUN.Checked == true)
            {
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, campoPivote2,
                campoDia,campoMes,campoAño,campoTotal});
            }
            else
            {
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
                campoDia,campoMes,campoAño,campoTotal});
            }
            campoPivote.AreaIndex = 0;
            if (rbPorUN.Checked == true)
            {
                campoPivote2.AreaIndex = 1;
            }
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            #region filtros
            string tipofecha = "";
            string transpesa = "";
            string bra = "";
            string filtro = "";
            string sucursal = "";
            if (rbFechaEmision.Checked)
            {
                tipofecha = "E";
            }
            else
            {
                tipofecha = "P";
            }
            if (chkTranspesa.Checked)
            {
                transpesa = "10000000";
            }
            if (chkBra.Checked)
            {
                bra = "40000000";
            }
            if (rbPorFacturador.Checked == true)
            {
                filtro = "PF";
            }
            else 
            {
                if (rbPorCliente.Checked == true)
                {
                    filtro = "PC";
                }
                else 
                {
                    if (rbPorUN.Checked == true) 
                    {
                        filtro = "PUN";
                    }
                    else 
                    {
                        filtro = "PS";
                        if (chkTrujillo.Checked == true && chkLima.Checked == true)
                        {
                            sucursal = "T,L";
                        }
                        else
                        {
                            if (chkTrujillo.Checked == true)
                            {
                                sucursal = "T";
                            }
                            else
                            {
                                sucursal = "L";
                            }
                        }
                    }
                }
            }
            #endregion
            dt = clsFinanzasBL.Instancia.GetFacturacionDiaria(dtpFechaIni.Value.ToShortDateString()+" 00:00:00",
               dtpFechaFin.Value.ToShortDateString()+" 23:59:59", tipofecha, transpesa, bra,filtro,sucursal);
            if (dt.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvData.DataSource = dt;
                dtgvData.BestFitColumnArea();
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
                string nombre = System.IO.Path.Combine(desktop, "Facturación Diaria del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
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

        private void rbPorServicio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPorServicio.Checked == true) 
            {
                gbSucursal.Visible = true;
                chkTrujillo.Visible = true;
                chkLima.Visible = true;
                chkTrujillo.Checked = true;
                chkLima.Checked = true;
            }
            else 
            {
                gbSucursal.Visible = false;
                chkTrujillo.Visible = false;
                chkLima.Visible = false;
            }
        }
    }
}
