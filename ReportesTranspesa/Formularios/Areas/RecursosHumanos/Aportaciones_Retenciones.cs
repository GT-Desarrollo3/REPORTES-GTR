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
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class Aportaciones_Retenciones : MetroFramework.Forms.MetroForm
    {
        public Aportaciones_Retenciones()
        {
            InitializeComponent();
        }

        private void Aportaciones_Retenciones_Load(object sender, EventArgs e)
        {

        }

        private void chkCompania_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompania.Checked)
            {
                cboCompañia.Enabled = true;
                cboCompañia.SelectedIndex = 0;
            }
            else
            {
                cboCompañia.Enabled = false;
                cboCompañia.SelectedIndex = -1;
            }
        }

        private void chkPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPeriodo.Checked)
            {
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                //String dy = datevalue.Day.ToString();
                String mn = "";
                if (datevalue.Month < 10)
                {
                    mn = "0" + datevalue.Month;
                }
                String yy = datevalue.Year.ToString();
                txtPeriodo.Text = yy + mn;
                txtPeriodo.Text = yy + mn;
                txtPeriodo.Enabled = true;
            }
            else
            {
                txtPeriodo.Text = "";
                txtPeriodo.Enabled = false;
            }
        }

        private void chkPlanilla_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlanilla.Checked)
            {
                cboPlanilla.Enabled = true;
                cboPlanilla.SelectedIndex = 0;
            }
            else
            {
                cboPlanilla.Enabled = false;
                cboPlanilla.SelectedIndex = -1;
            }
        }

        private void chkProceso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProceso.Checked)
            {
                cboProceso.Enabled = true;
                cboProceso.SelectedIndex = 0;
            }
            else
            {
                cboProceso.Enabled = false;
                cboProceso.SelectedIndex = -1;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string compañia = "";
            string periodo = "";
            string planilla = "";
            string proceso = "";
            if (chkCompania.Checked == true)
            {
                if (cboCompañia.SelectedIndex == 0)
                {
                    compañia = "10000000";
                }
                else
                {
                    compañia = "40000000";
                }
            }
            if (chkPeriodo.Checked == true)
            {
                periodo = txtPeriodo.Text.Trim() + (txtPeriodo.Text).Substring(4, 2);
            }
            if (chkPlanilla.Checked == true)
            {
                switch (cboPlanilla.SelectedIndex) 
                {
                    case 0: planilla = "EM";
                            break;
                    case 1: planilla = "OB";
                            break;
                    case 2: planilla = "PR";
                            break;
                }
            }
            if (chkProceso.Checked == true)
            {
                switch (cboProceso.SelectedIndex)
                {
                    case 0: proceso = "NO0";
                            break;
                    case 1: proceso = "LI0";
                            break;
                    case 2: proceso = "GR0";
                            break;
                    case 3: proceso = "UT0";
                            break;
                }
            }
            dt = clsRecursosHumanosBL.Instancia.GetAportacionesRetenciones(compañia,periodo,planilla,proceso);
            if (dt.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvData.DataSource = dt;
                dtgvData.BestFit();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }

        }

        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            PivotGridField campoPivote2 = new PivotGridField();
            campoPivote = new PivotGridField("PLANILLA", PivotArea.RowArea);
            campoPivote2 = new PivotGridField("PROCESO", PivotArea.RowArea);

            PivotGridField campoPeriodo = new PivotGridField("CONCEPTO", PivotArea.ColumnArea);
            campoPeriodo.Caption = "CONCEPTO";

            PivotGridField campoTotal = new PivotGridField("MONTO", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, campoPivote2,
            campoPeriodo,campoTotal});
            campoPivote.AreaIndex = 0;
            campoPivote2.AreaIndex = 1;
            campoPeriodo.AreaIndex = 0;
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Aportaciones y Retenciones del " + txtPeriodo.Text + " " +  Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
