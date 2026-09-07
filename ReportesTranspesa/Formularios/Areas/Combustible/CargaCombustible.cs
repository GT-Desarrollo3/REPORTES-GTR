using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class CargaCombustible : MetroFramework.Forms.MetroForm
    {
        public CargaCombustible()
        {
            InitializeComponent();
        }

        private void CargaCombustible_Load(object sender, EventArgs e)
        {
            rbDespachos.Checked = true;
            splitContainer1.SplitterDistance = 160;

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        public void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            string conductor = txtConductor.Text;
            string tracto = txtTracto.Text;

            dtgvDespachosDiarios.DataSource = null;
            dtgvDespachosDiariosView.Columns.Clear();

            dtgvAbastecimientos.DataSource = null;
            dtgvAbastecimientosView.Columns.Clear();

            dtgvDespachoUrea.DataSource = null;
            dtgvDespachoUreaView.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();

            int filtro = 0;

            if (rbDespachos.Checked == true)
            {
                filtro = 0;
                dtgvDespachosDiarios.Visible = true;
                dtgvAbastecimientos.Visible = false;
                dtgvDespachoUrea.Visible = false;
            }
            else
            {
                if (rbVerSurtidor.Checked == true)
                {
                    filtro = 1;
                    dtgvDespachosDiarios.Visible = false;
                    dtgvAbastecimientos.Visible = true;
                    dtgvDespachoUrea.Visible = false;
                }
                else
                {
                    filtro = 2;
                    dtgvDespachosDiarios.Visible = false;
                    dtgvAbastecimientos.Visible = false;
                    dtgvDespachoUrea.Visible = true;
                }
            }

            dt.Clear();
            dt = clsCombustibleBL.Instancia.GetDataDespachosDiarios(dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString(), conductor, tracto, filtro);

            if (dt.Rows.Count > 0)
            {
                if (filtro == 0)
                {
                    dtgvDespachosDiarios.DataSource = dt;
                    dtgvDespachosDiariosView.Columns["CONSUMO_SW"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDespachosDiariosView.Columns["CONSUMO_SW"].DisplayFormat.FormatString = "N2";
                    dtgvDespachosDiariosView.Columns["KM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "KM", "Total = {0:N2}");
                    dtgvDespachosDiariosView.Columns["CONSUMO_FISICO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CONSUMO_FISICO", "Total = {0:N2}");
                    dtgvDespachosDiariosView.Columns["CONSUMO_SW"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CONSUMO_SW", "Total = {0:N2}");
                    dtgvDespachosDiariosView.Columns["REND KM/GL"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "REND KM/GL", "REND PROMEDIO = {0:N2}");
                    GridFormatRule gridFormatRule = new GridFormatRule();
                    FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                    FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                    FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                    icon1.PredefinedName = "TrafficLights3_1.png";
                    icon2.PredefinedName = "TrafficLights3_2.png";
                    icon3.PredefinedName = "TrafficLights3_3.png";
                    iconSet.ValueType = FormatConditionValueType.Number;

                    icon1.Value = -99; // target range: 67% <= value
                    icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon2.Value = 33; // target range: 33% <= value < 67%
                    icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon3.Value = 0; // target range: 0% <= value < 33%
                    icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                    iconSet.Icons.Add(icon1);
                    iconSet.Icons.Add(icon2);
                    iconSet.Icons.Add(icon3);

                    gridFormatRule.Rule = formatConditionRuleIconSet;
                    gridFormatRule.Column = dtgvDespachosDiariosView.Columns["DIF"];
                    dtgvDespachosDiariosView.FormatRules.Add(gridFormatRule);
                    dtgvDespachosDiariosView.BestFitColumns();
                }
                else
                {
                    if (filtro == 1)
                    {
                        dtgvAbastecimientos.DataSource = dt;
                        dtgvAbastecimientosView.BestFitColumns();
                    }
                    else
                    {
                        dtgvDespachoUrea.DataSource = dt;
                        dtgvDespachoUreaView.BestFitColumns();

                        dtgvDespachoUreaView.Columns["FECHA_CREACION"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvDespachoUreaView.Columns["FECHA_CREACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    }
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para mostrar.";
                m.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (rbVerSurtidor.Checked == true)
            {
                if (dtgvAbastecimientos.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Abastecimiento de Surtidor " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvAbastecimientos.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            else
            {
                if (rbDespachos.Checked == true)
                {
                    if (dtgvDespachosDiarios.DataSource == null)
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
                        string nombre = System.IO.Path.Combine(desktop, "Reporte Despachos Diarios " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                        dtgvDespachosDiarios.ExportToXlsx(nombre);
                        Process.Start(nombre);
                    }
                }
                else
                {
                    if (dtgvDespachoUrea.DataSource == null)
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
                        string nombre = System.IO.Path.Combine(desktop, "Reporte Despachos de Urea " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                        dtgvDespachoUrea.ExportToXlsx(nombre);
                        Process.Start(nombre);
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvDespachosDiarios.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvDespachosDiarios.ShowPrintPreview();
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }

        private void verInformacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInformacionDespacho frm = new FrmInformacionDespacho();

            string Placa = "";
            DateTime FechaProgramacion = DateTime.Now;

            foreach (var i in dtgvDespachosDiariosView.GetSelectedRows())
            {
                Placa = dtgvDespachosDiariosView.GetDataRow(i)["PLACA"].ToString();
                FechaProgramacion = Convert.ToDateTime(dtgvDespachosDiariosView.GetDataRow(i)["FECHAPROG_VIAJE"].ToString());
            }

            frm.Placa = Placa;
            frm.FechaProgramacion = FechaProgramacion;
            frm.ShowDialog();
        }
    }
}
