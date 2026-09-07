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
    public partial class Faltas_Suspensiones : MetroFramework.Forms.MetroForm
    {
        public Faltas_Suspensiones()
        {
            InitializeComponent();
        }
        private void Faltas_Suspensiones_Load(object sender, EventArgs e)
        {
            //String sDate = DateTime.Now.ToString();
            //DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            ////String dy = datevalue.Day.ToString();
            //String mn = "";
            //if (datevalue.Month < 10)
            //{
            //    mn = "0" + datevalue.Month;
            //}
            //String yy = datevalue.Year.ToString();
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;
            cbxEstado.SelectedIndex = 0;
        }
        private void CreaColumnasPivotGrid()
        {
            PivotGridField campoPivote = new PivotGridField();
            PivotGridField campoPivote2 = new PivotGridField();
            PivotGridField campoPivote3 = new PivotGridField();
            campoPivote = new PivotGridField("TIPO PLANILLA", PivotArea.RowArea);
            campoPivote2 = new PivotGridField("CODIGO", PivotArea.RowArea);
            campoPivote3 = new PivotGridField("NOMBRE", PivotArea.RowArea);

            PivotGridField campoPeriodo = new PivotGridField("PERIODO", PivotArea.ColumnArea);
            campoPeriodo.Caption = "PERIODO";
            
            PivotGridField campoTotal = new PivotGridField("CANTIDAD", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "n";
            dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, campoPivote2, campoPivote3,
            campoPeriodo,campoTotal});
            campoPivote.AreaIndex = 0;
            campoPivote2.AreaIndex = 2;
            campoPivote3.AreaIndex = 1;
            campoPeriodo.AreaIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string concepto;
            if (rbFaltas.Checked == true)
            {
                concepto = "FALT";
            }
            else
            {
                concepto = "SUSP";
            }
            dt = clsRecursosHumanosBL.Instancia.GetFaltasySuspensiones(dtpDesde.Text + (dtpDesde.Text).Substring(4, 2), dtpHasta.Text + (dtpHasta.Text).Substring(4, 2), concepto, Convert.ToInt32(cbxEstado.SelectedIndex)+1);
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
                string concepto;
                if (rbFaltas.Checked == true)
                {
                    concepto = "Faltas";
                }
                else
                {
                    concepto = "Suspensiones";
                } 
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de "+ concepto + " del " + dtpDesde.Text + " al " + dtpHasta.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void txtPerIni_Click(object sender, EventArgs e)
        {

        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }
        private void dtpDesde_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || (e.KeyCode == Keys.Return || e.KeyData == Keys.Enter))
            {
                MessageBox.Show("Enter Buen Trabajo!");
                dtpHasta.Focus();
            }
        }
        private void dtpHasta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxEstado.Focus();
            }
        }
        private void cbxEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.Focus();
            }
        }
    }
}
