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
    public partial class ReporteTicketSurtidorAnexadoKardex : MetroFramework.Forms.MetroForm
    {
        public ReporteTicketSurtidorAnexadoKardex()
        {
            InitializeComponent();
        }

        private void CargaCombustible_Load(object sender, EventArgs e)
        {

            splitContainer1.SplitterDistance = 160;
            cbxFiltro.SelectedIndex = 0;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();

                return;
            }
            string conductor = txtConductor.Text;
            string tracto = txtTracto.Text;
            dtgvDespachosDiarios.DataSource = null;
            dtgvDespachosDiariosView.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();

            int filtro = 0;

            if (cbxFiltro.SelectedIndex == 0)
            {
                filtro = 0;

            }else if (cbxFiltro.SelectedIndex == 1){
                filtro = 1;
            }else{
                filtro = 2;
            }

            dt.Clear();

            dt = clsCombustibleBL.Instancia.ObtenerReporteSurtidorAnexadoAlKardex(dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString(), conductor, tracto, filtro);
            
            
            if (dt.Rows.Count > 0)
            {
                
                dtgvDespachosDiarios.DataSource = dt;

                //dtgvDespachosDiariosView.Columns["PESO(TN)"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDespachosDiariosView.Columns["PESO(TN)"].DisplayFormat.FormatString = "N2";
                //dtgvDespachosDiariosView.Columns["KM RECORRIDOS"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDespachosDiariosView.Columns["KM RECORRIDOS"].DisplayFormat.FormatString = "N2";
                //dtgvDespachosDiariosView.Columns[12].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDespachosDiariosView.Columns[12].DisplayFormat.FormatString = "N2";
                //dtgvDespachosDiariosView.Columns[13].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDespachosDiariosView.Columns[13].DisplayFormat.FormatString = "Nt2";
                //dtgvDespachosDiariosView.Columns[14].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDespachosDiariosView.Columns[14].DisplayFormat.FormatString = "N2";
                //dtgvDespachosDiariosView.Columns["CONSUMO_SW"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDespachosDiariosView.Columns["CONSUMO_SW"].DisplayFormat.FormatString = "N2";
                //dtgvDespachosDiariosView.Columns["KM RECORRIDOS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "KM RECORRIDOS", "Total = {0:N2}");
                //dtgvDespachosDiariosView.Columns["CONSUMO_FISICO(GL)"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CONSUMO_FISICO(GL)", "Total = {0:N2}");
                //dtgvDespachosDiariosView.Columns["CONSUMO_SW"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CONSUMO_SW", "Total = {0:N2}");
                //dtgvDespachosDiariosView.Columns["REND KM/GL"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "REND KM/GL", "REND PROMEDIO = {0:N2}");
                //GridFormatRule gridFormatRule = new GridFormatRule();
                //FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                //FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                //FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                //FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                //FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                ////Choose predefined icons.
                //icon1.PredefinedName = "TrafficLights3_1.png";
                //icon2.PredefinedName = "TrafficLights3_2.png";
                //icon3.PredefinedName = "TrafficLights3_3.png";

                ////Specify the type of threshold values.
                ////iconSet.ValueType = FormatConditionValueType.Percent;
                //iconSet.ValueType = FormatConditionValueType.Number;

                ////Define ranges to which icons are applied by setting threshold values.
                //icon1.Value = -99; // target range: 67% <= value
                //icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                //icon2.Value = 33; // target range: 33% <= value < 67%
                //icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                //icon3.Value = 0; // target range: 0% <= value < 33%
                //icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                ////Add icons to the icon set.
                //iconSet.Icons.Add(icon1);
                //iconSet.Icons.Add(icon2);
                //iconSet.Icons.Add(icon3);

                ////Specify the rule type.
                //gridFormatRule.Rule = formatConditionRuleIconSet;
                ////Specify the column to which formatting is applied.
                //////gridFormatRule.Column = dtgvDespachosDiariosView.Columns["DIF"];
                ////Add the formatting rule to the GridView.
                //dtgvDespachosDiariosView.FormatRules.Add(gridFormatRule);
                dtgvDespachosDiariosView.BestFitColumns();
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

                if (dtgvDespachosDiarios.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Analisis Diario " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvDespachosDiarios.ExportToXlsx(nombre);
                    Process.Start(nombre);
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

            foreach(var i in dtgvDespachosDiariosView.GetSelectedRows())
            {
                Placa = dtgvDespachosDiariosView.GetDataRow(i)["PLACA"].ToString();
                FechaProgramacion = Convert.ToDateTime(dtgvDespachosDiariosView.GetDataRow(i)["FECHAPROG_VIAJE"].ToString());
            }

            frm.Placa = Placa;
            frm.FechaProgramacion = FechaProgramacion;
            frm.ShowDialog();

        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (cbxFiltro.Text.Equals("NO ANEXADOS"))
            {
                btnRegularizar.Visible = true;
            }
            else 
            {
                btnRegularizar.Visible = false;
            }
        }

        private void btnRegularizar_Click(object sender, EventArgs e)
        {
            if (dtgvDespachosDiarios.DataSource == null)
            {
                return;
            }

            int[] filass = dtgvDespachosDiariosView.GetSelectedRows();
            string datoseleccionado = dtgvDespachosDiariosView.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                string Ticket = dtgvDespachosDiariosView.GetRowCellValue(filass[i], "TICKET").ToString();
                string Fecha = dtgvDespachosDiariosView.GetRowCellValue(filass[i], "FECHAHORA_SURTIDOR").ToString();
                string Hora = dtgvDespachosDiariosView.GetRowCellValue(filass[i], "FECHAHORA_SURTIDOR").ToString();
                string Previaje = dtgvDespachosDiariosView.GetRowCellValue(filass[i], "CODIGO_PREVIAJE").ToString();
                                
                frmRegularizarTickets frmTickets = new frmRegularizarTickets();
                frmTickets.txtTicket.Text = Ticket;
                frmTickets.fechaTicket = Fecha;
                frmTickets.codigopreviaje = Convert.ToInt32(Previaje);
                
                frmTickets.ShowDialog();                
            }          
        }
    }
}
