using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class Resumen_Viajes_por_Facturar : MetroFramework.Forms.MetroForm
    {
        public Resumen_Viajes_por_Facturar()
        {
            InitializeComponent();
        }

        private void Resumen_Viajes_por_Facturar_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);

        }

        private void pvgData_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {

        }

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            if (rbPorFacturar.Checked == true)
            {
                campoPivote = new PivotGridField("POR FACTURAR", PivotArea.RowArea);
            }
            else
            {
                if (rbDocExtraviado.Checked == true)
                {
                    campoPivote = new PivotGridField("DOC EXTRAVIADO", PivotArea.RowArea);
                }
               else
                {
                    if (rbObservado.Checked == true)
                    {
                        campoPivote = new PivotGridField("OBSERVADO", PivotArea.RowArea);
                    }
                    else
                    {
                       if (rbPendientes.Checked == true)
                       {
                          campoPivote = new PivotGridField("PENDIENTES O/C", PivotArea.RowArea);
                       }
                       else
                       {
                          if (rbConOC.Checked == true)
                          {
                              campoPivote = new PivotGridField("CON O/C", PivotArea.RowArea);
                          }
                          else
                          {
                              if (rbNoReconoceCliente.Checked == true)
                              {
                                  campoPivote = new PivotGridField("NO RECONOCE EL CLIENTE", PivotArea.RowArea);
                              }
                              else
                              {
                                  if (rbCompletado.Checked == true)
                                  {
                                      campoPivote = new PivotGridField("COMPLETADO", PivotArea.RowArea);
                                  }
                              }
                          }
                       }
                    }
                 }
            }
            PivotGridField campoDia = new PivotGridField("DIA", PivotArea.ColumnArea);
            campoDia.Caption = "Dia";
            PivotGridField campoMes = new PivotGridField("MES", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("AÑO", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("IMPORTE", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, 
              campoDia,campoMes,campoAño,campoTotal});
            campoPivote.AreaIndex = 0;
            campoDia.AreaIndex = 2;
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            string filtro = "";
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            if (rbPorFacturar.Checked == true)
            {
                filtro = "PF";
            }
            else
            {
                if(rbDocExtraviado.Checked == true)
                {
                    filtro = "DOCEXTR";
                }
            }
            //#region Filtros
            //string tipofecha = "";
            /*if (rbFechaProgramacion.Checked)
            {
                tipofecha = "P";
            }
            else
            {
                 tipofecha = "C";
            }*/
            //#endregion
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetDataSumarizadoImporte(fechin,fechfin,filtro);
            //dt = clsContabilidadBL.Instancia.GetDataSumarizadoImporte(fechin, fechfin, tipofecha);
            
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
                string nombre = System.IO.Path.Combine(desktop, "Resumen de Viajes por Facturar del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
    }
}
