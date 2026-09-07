using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Negocio;
using ReportesTranspesa.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmBonoConductoresDetalle : Form
    {
         int _IdConductorDetalle ;
         string _ConductorDetalle;
         string _FechaInicioDetalle;
         string _FechaFinDetalle;
         int _tipo;
        decimal _Ppuntaje,_idOperacion;
        public frmBonoConductoresDetalle()
        {
            InitializeComponent();
        }

        public void EnviarDatosDetalle(int tipo, int IdPersonaDetalle, string ConductorDetalle, string FechaInicioDetalle, string FechaFinDetalle, decimal Puntaje,decimal idOperacion)
        {
            _tipo = tipo;
            _IdConductorDetalle = IdPersonaDetalle ;
            _ConductorDetalle = ConductorDetalle;
            _FechaInicioDetalle = FechaInicioDetalle;
            _FechaFinDetalle = FechaFinDetalle;
            _Ppuntaje = Puntaje;
            _idOperacion = idOperacion;
        }

        private void frmBonoConductoresDetalle_Load(object sender, EventArgs e)
        {
            
            if (_tipo == 1)//DETALLE COMBUSTIBLE
            {
                label1.Text = "Conductor: " + _ConductorDetalle;
                label2.Text = "Desde: " + _FechaInicioDetalle + " - Hasta: " + _FechaFinDetalle;
               // label3.Text = "DETALLE CALIFICATIVO DE: COMBUSTIBLE";
                tolTitulo.Text = "DETALLE CALIFICATIVO DE COMBUSTIBLE";
                if (_idOperacion == 1)
                {
                    if (_Ppuntaje < 21)
                    { btnPunateje.BackColor = Color.PaleGreen;
                    btnPunateje.Text = _Ppuntaje.ToString();}
                    else
                    { btnPunateje.BackColor = Color.Red; btnPunateje.Text = _Ppuntaje.ToString(); }
                }
                else 
                {
                    if (_Ppuntaje <3)
                    { btnPunateje.BackColor = Color.PaleGreen; btnPunateje.Text = _Ppuntaje.ToString(); }
                    else
                    { btnPunateje.BackColor = Color.Red; btnPunateje.Text = _Ppuntaje.ToString(); }
                }
                

                string fechin = "01/01/1980";
                string fechfin = "31/12/2030";
                fechin = _FechaInicioDetalle + " 00:00:00";
                fechfin = _FechaFinDetalle + " 23:59:59";

                dtgvAbastecimientos.DataSource = null;
                //dtgvDespachosDiariosView.Columns.Clear();

                dtgvAbastecimientos.DataSource = null;
                dtgvAbastecimientosView.Columns.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();

                dt = clsCombustibleBL.Instancia.GetDataDespachosDetalleDiarios(_IdConductorDetalle,_FechaInicioDetalle, _FechaFinDetalle, _ConductorDetalle);

                if (dt.Rows.Count > 0)
                {
                    lblTotal.Text = "Total: " + dt.Rows.Count.ToString() + " Registros.";
                    dtgvAbastecimientos.DataSource = dt;
                    dtgvAbastecimientosView.OptionsBehavior.Editable = false;                    
                    dtgvAbastecimientosView.Columns["KM RECORRIDOS"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvAbastecimientosView.Columns["KM RECORRIDOS"].DisplayFormat.FormatString = "N2";
                    dtgvAbastecimientosView.Columns["% DIF. CSF vs CSW"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvAbastecimientosView.Columns["% DIF. CSF vs CSW"].DisplayFormat.FormatString = "{0:N2} %";
                    dtgvAbastecimientosView.Columns["CONSUMO_SW"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvAbastecimientosView.Columns["CONSUMO_SW"].DisplayFormat.FormatString = "N2";
                    dtgvAbastecimientosView.Columns["KM RECORRIDOS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "KM RECORRIDOS", "Total = {0:N2}");                  
                    dtgvAbastecimientosView.Columns["CONSUMO_SW"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CONSUMO_SW", "Total = {0:N2}");
                    
                    GridFormatRule gridFormatRule = new GridFormatRule();
                    FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                    FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                    FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                    //Choose predefined icons.
                    icon1.PredefinedName = "TrafficLights3_1.png";
                    icon2.PredefinedName = "TrafficLights3_2.png";
                    icon3.PredefinedName = "TrafficLights3_3.png";

                    //Specify the type of threshold values.
                    //iconSet.ValueType = FormatConditionValueType.Percent;
                    iconSet.ValueType = FormatConditionValueType.Number;

                    //Define ranges to which icons are applied by setting threshold values.
                    icon1.Value = -99; // target range: 67% <= value
                    icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon2.Value = 33; // target range: 33% <= value < 67%
                    icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon3.Value = 0; // target range: 0% <= value < 33%
                    icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                    //Add icons to the icon set.
                    iconSet.Icons.Add(icon1);
                    iconSet.Icons.Add(icon2);
                    iconSet.Icons.Add(icon3);

                    //Specify the rule type.
                    gridFormatRule.Rule = formatConditionRuleIconSet;
                    //Specify the column to which formatting is applied.
                    gridFormatRule.Column = dtgvAbastecimientosView.Columns["DIF"];
                    //Add the formatting rule to the GridView.
                    dtgvAbastecimientosView.FormatRules.Add(gridFormatRule);
                    dtgvAbastecimientosView.BestFitColumns();

                    dtgvAbastecimientosView.BestFitColumns();
                }
                else
                {
                    lblTotal.Text = "Total: 0 Registros.";
                    dtgvAbastecimientos.DataSource = null;
                    MessageBox.Show("No hay data para mostrar","Mensaje");                  
                }
            }
            else if (_tipo == 2)//DETALLE DE SEGURIDAD
            {
                label1.Text = "Conductor: " + _ConductorDetalle;
                label2.Text = "Desde: " + _FechaInicioDetalle + " - Hasta: " + _FechaFinDetalle;
               // label3.Text = "DETALLE CALIFICATIVO DE: SEGURIDAD";
                tolTitulo.Text = "DETALLE CALIFICATIVO DE SEGURIDAD";

                if (_Ppuntaje >= 75)
                { btnPunateje.BackColor = Color.PaleGreen; btnPunateje.Text = _Ppuntaje.ToString(); }
                else
                { btnPunateje.BackColor = Color.Red; btnPunateje.Text = _Ppuntaje.ToString(); }   

                dtgvAbastecimientos.DataSource = null;
                dtgvAbastecimientosView.Columns.Clear();

                DataTable dtDetalleBonoSegu = new DataTable();
                dtDetalleBonoSegu = clsSeguridadBL.Instancia.GetListarDetalleCalificativoCoductores(_FechaInicioDetalle, _FechaFinDetalle, _IdConductorDetalle);

                if (dtDetalleBonoSegu.Rows.Count > 0)
                {
                    lblTotal.Text = "Total: " + dtDetalleBonoSegu.Rows.Count.ToString() + " Registros.";
                    dtgvAbastecimientos.DataSource = dtDetalleBonoSegu;
                    dtgvAbastecimientosView.OptionsBehavior.Editable = false;
                    GridFormatRule gridFormatRule = new GridFormatRule();
                    FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                    FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                    FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                    //Choose predefined icons.
                    icon1.PredefinedName = "TrafficLights3_1.png";
                    icon2.PredefinedName = "TrafficLights3_2.png";
                    icon3.PredefinedName = "TrafficLights3_3.png";

                    //Specify the type of threshold values.
                    //iconSet.ValueType = FormatConditionValueType.Percent;
                    iconSet.ValueType = FormatConditionValueType.Percent;

                    //Define ranges to which icons are applied by setting threshold values.
                    icon1.Value = 75; // target range: 67% <= value
                    icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon2.Value = 0; // target range: 33% <= value < 67%
                    icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon3.Value = 0; // target range: 0% <= value < 33%
                    icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                    //Add icons to the icon set.
                    iconSet.Icons.Add(icon1);
                    iconSet.Icons.Add(icon3);
                    iconSet.Icons.Add(icon3);

                    //Specify the rule type.
                    gridFormatRule.Rule = formatConditionRuleIconSet;
                    //Specify the column to which formatting is applied.
                    gridFormatRule.Column = dtgvAbastecimientosView.Columns["PuntajeDiario"];
                    //Add the formatting rule to the GridView.
                    dtgvAbastecimientosView.FormatRules.Add(gridFormatRule);

                    /* dtgvAbastecimientosView.Columns["Unidad"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "Unidad", "TOTALES:");
                     dtgvAbastecimientosView.Columns["VelocidadKMxH"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "VelocidadKMxH", "{0:N2}");
                     dtgvAbastecimientosView.Columns["CurbasCerradas"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "CurbasCerradas", "{0:N2}");
                     dtgvAbastecimientosView.Columns["GiroBruscos"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "GiroBruscos", "{0:N2}");
                     dtgvAbastecimientosView.Columns["ManejoContinuo"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "ManejoContinuo", "{0:N2}");
                     dtgvAbastecimientosView.Columns["VentanaHoraria"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "VentanaHoraria", "{0:N2}");
                     dtgvAbastecimientosView.Columns["PuntajeDiario"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "PuntajeDiario", "{0:N2}");*/
                    dtgvAbastecimientosView.BestFitColumns();
                }
                else
                {
                    lblTotal.Text = "Total: 0 Registros.";
                    dtgvAbastecimientos.DataSource = null;
                    MessageBox.Show("No hay data para mostrar", "Mensaje");
                }
            }
            else ///DETALLE DE MERMAS
            {
                label1.Text = "Conductor: " + _ConductorDetalle;
                label2.Text = "Desde: " + _FechaInicioDetalle + " - Hasta: " + _FechaFinDetalle;
                //label3.Text = "DETALLE CALIFICATIVO DE: MERMAS";
                tolTitulo.Text = "DETALLE CALIFICATIVO DE MERMAS";
                btnPunateje.Visible = false;
                DataTable dtMermas = new DataTable();

                dtMermas = clsOperacionesBL.Instancia.GetVerMermasxConductor(_IdConductorDetalle, _FechaInicioDetalle, _FechaFinDetalle);
                if (dtMermas.Rows.Count > 0)
                {
                    lblTotal.Text = "Total: " + dtMermas.Rows.Count.ToString() + " Registros.";
                    dtgvAbastecimientos.DataSource = dtMermas;
                    dtgvAbastecimientosView.OptionsBehavior.Editable = false;
                    dtgvAbastecimientosView.BestFitColumns();
                }
                else
                {
                    lblTotal.Text = "Total: 0 Registros.";
                    dtgvAbastecimientos.DataSource = null;
                    MessageBox.Show("No hay data para mostrar", "Mensaje");
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvAbastecimientos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop,this.Text.ToString() + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvAbastecimientosView.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
