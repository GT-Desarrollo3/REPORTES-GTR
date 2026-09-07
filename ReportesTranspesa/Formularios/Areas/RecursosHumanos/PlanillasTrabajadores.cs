using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid.Columns;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class PlanillasTrabajadores : MetroFramework.Forms.MetroForm
    {
        public PlanillasTrabajadores()
        {
            InitializeComponent();
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
               

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvData.Fields.Clear();
            dtgvPlanilla.DataSource = null;
            dtgvPlanillaView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            string transpesa = "10000000";
            string bra = "40000000";
            string altra = "50000000";
            string amt = "60000000";
            string aduanas = "70000000";
            string periodo = "";
            if(chkAnio.Checked == false)
            {
                periodo = DateTime.Now.Year.ToString();
            }
            else
            {
                periodo = cboAnio.Text;
            }
            string filtro = "";
            #region Compañia
            if (chkCompania.Checked == true)
            {
                switch (cboCompañia.SelectedIndex)
                {
                    case 0: transpesa = "10000000";
                        bra = "";
                        altra = "";
                        amt = "";
                        aduanas = "";
                        break;

                    case 1: bra = "40000000";
                        transpesa = "";
                        altra = "";
                        amt = "";
                        aduanas = "";
                        break;

                    case 2: altra = "50000000";
                        transpesa = "";
                        bra = "";
                        amt = "";
                        aduanas = "";
                        break;

                    case 3: amt = "60000000";
                        transpesa = "";
                        bra = "";
                        altra = "";
                        aduanas = "";
                        break;

                    case 4: aduanas = "70000000";
                        transpesa = "";
                        bra = "";
                        altra = "";
                        amt = "";
                        break;
                }
            }
            #endregion
            #region FIltros
            
            if (rbTodos.Checked == true)
            {
                if (chkDetalle.Checked == true)
                {
                    filtro = "TDdet";
                }
                else
                {
                    filtro = "TD";
                }
                
            }
            else
            {
                if (rbEmpleados.Checked == true)
                {
                    if (chkDetalle.Checked == true)
                    {
                        filtro = "EMdet";
                    }
                    else
                    {
                        filtro = "EM";
                    }
                }
                else
                {
                    if (rbObreros.Checked == true)
                    {
                        if (chkDetalle.Checked == true)
                        {
                            filtro = "OBdet";
                        }
                        else
                        {
                            filtro = "OB";
                        }
                    }
                    else
                    {
                        if (chkDetalle.Checked == true)
                        {
                            filtro = "CDdet";
                        }
                        else
                        {
                            filtro = "CD";
                        }
                    }
                }
            }
            #endregion
            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasTrabajores(transpesa, bra, altra, amt, aduanas, periodo);
            dt2 = clsRecursosHumanosBL.Instancia.GetDataPlanillasTrabajoresPeriodo(transpesa, bra, altra, amt, aduanas, periodo, filtro);
            if (dt.Rows.Count > 0 || dt2.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvData.DataSource = dt;
                dtgvData.BestFitRowArea();
                dtgvData.BestFitColumnArea();
                dtgvPlanilla.DataSource = dt;
                GridView gridView = dtgvPlanilla.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["TIPOTRABAJADOR"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                dtgvPlanillaView.ExpandAllGroups();
                dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatString = "c2";
                dtgvPlanillaView.Columns["Sueldo Bruto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Sueldo Bruto", "Total ={0:c2}");
                dtgvPlanillaView.BestFitColumns();

                if (rbEmpleados.Checked == true)
                {
                    dtgvPlanillaView.Columns.Clear();
                    CreaColumnasPivotGrid2();
                    dtgvData.DataSource = dt2;
                    dtgvData.BestFitRowArea();
                    dtgvData.BestFitColumnArea();
                    dtgvPlanilla.DataSource = dt2;
                    dtgvPlanillaView.ExpandAllGroups();
                    dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatString = "c2";
                    dtgvPlanillaView.Columns["Sueldo Bruto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Sueldo Bruto", "Total ={0:c2}");
                    dtgvPlanillaView.Columns["Numero de Trabajadores"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Numero de Trabajadores", "Total ={0}");
                    dtgvPlanillaView.BestFitColumns();
                }
                if (rbObreros.Checked == true)
                {
                    dtgvPlanillaView.Columns.Clear();
                    CreaColumnasPivotGrid2();
                    dtgvData.DataSource = dt2;
                    dtgvData.BestFitRowArea();
                    dtgvData.BestFitColumnArea();
                    dtgvPlanilla.DataSource = dt2;
                    dtgvPlanillaView.ExpandAllGroups();
                    dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatString = "c2";
                    dtgvPlanillaView.Columns["Sueldo Bruto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Sueldo Bruto", "Total ={0:c2}");
                    dtgvPlanillaView.Columns["Numero de Trabajadores"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Numero de Trabajadores", "Total ={0}");
                    dtgvPlanillaView.BestFitColumns();
                }
                if (rbChoferes.Checked == true)
                {
                    dtgvPlanillaView.Columns.Clear();
                    CreaColumnasPivotGrid2();
                    dtgvData.DataSource = dt2;
                    dtgvData.BestFitRowArea();
                    dtgvData.BestFitColumnArea();
                    dtgvPlanilla.DataSource = dt2;
                    dtgvPlanillaView.ExpandAllGroups();
                    dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvPlanillaView.Columns["Sueldo Bruto"].DisplayFormat.FormatString = "c2";
                    dtgvPlanillaView.Columns["Sueldo Bruto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Sueldo Bruto", "Total ={0:c2}");
                    dtgvPlanillaView.Columns["Numero de Trabajadores"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Numero de Trabajadores", "Total ={0}");
                    dtgvPlanillaView.BestFitColumns();
                }

            }
            else
            {
               MessageBox.Show("No hubo resultados","Mensaje");
               
            }
        }

        private void PlanillasTrabajadores_Load(object sender, EventArgs e)
        {
            cboAnio.SelectedIndex = 0;
            cboCompañia.SelectedIndex = 0;
            cboPlanilla.SelectedIndex = 0;
            if (Utilitario.Instancia.SesionUsuario.usuario == "SESCOBEDO" || Utilitario.Instancia.SesionUsuario.usuario == "JDIAZ")
            {
                chkPlanilla.Visible = false;
                cboPlanilla.Visible = false;
                chkCompania.Location = new Point(23, 35);
                cboCompañia.Location = new Point(119, 31);
            }
        }

        private void dtgvData_CustomAppearance(object sender, DevExpress.XtraPivotGrid.PivotCustomAppearanceEventArgs e)
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
            campoPivote = new PivotGridField("N°", PivotArea.RowArea);
            PivotGridField campoMes = new PivotGridField("Mes", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("N°", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("Sueldo Bruto", PivotArea.DataArea);
            PivotGridField campoTotal2 = new PivotGridField("Numero de Trabajadores", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            if (rbTodos.Checked == true)
            {
                dtgvData.Fields.AddRange(new PivotGridField[] {campoPivote, campoPivote2,
                campoMes,campoAño,campoTotal,campoTotal2});
            }
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private void CreaColumnasPivotGrid2()
        {
            PivotGridField campoPivote = new PivotGridField();
            campoPivote = new PivotGridField("TipoPlanilla", PivotArea.RowArea);
            PivotGridField campoMes = new PivotGridField("Mes", PivotArea.ColumnArea);
            campoMes.Caption = "Mes";
            PivotGridField campoAño = new PivotGridField("N°", PivotArea.ColumnArea);
            campoAño.Caption = "Año";
            PivotGridField campoTotal = new PivotGridField("Sueldo Bruto", PivotArea.DataArea);
            PivotGridField campoTotal2 = new PivotGridField("Numero de Trabajadores", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvData.Fields.AddRange(new PivotGridField[] { campoPivote, campoMes, campoAño, campoTotal, campoTotal2 });
            campoMes.AreaIndex = 1;
            campoAño.AreaIndex = 0;
        }

        private Microsoft.Office.Interop.Excel.Application app;

        private void btnExcel_Click(object sender, EventArgs e)
        {

            if (dtgvData.DataSource == null)
            {
                MessageBox.Show("No hay data para exportar","Mensaje");
              //  m.ShowDialog();
            }
            else
            {
                string tipo = "";
                if (rbTodos.Checked)
                {
                    tipo = "Todos";
                }
                else
                {
                    if (rbEmpleados.Checked)
                    {
                        tipo = "Empleados";
                    }
                    else
                    {
                        if (rbObreros.Checked)
                        {
                            tipo = "Obreros";
                        }
                        else
                        {
                            tipo = "Choferes";
                        }
                    }
                }
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Listado de Planilla de trabajadores (" + tipo + ") " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                MessageBox.Show("No hay data a imprimir", "Mensaje");
            }
            else
            {
                dtgvData.ShowPrintPreview();
            }
        }

        private void dtgvPlanilla_DoubleClick(object sender, EventArgs e)
        {
            DetallePlanilla frm = new DetallePlanilla();
            frm.Show();
        }

        public static class ClaseCompartida
        {
            public static string PeriodoPlanilla;
        }

        private void dtgvPlanilla_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = dtgvPlanillaView.GetDataRow(dtgvPlanillaView.GetSelectedRows()[0]);
                label1.Text = row["MES"].ToString();
                ClaseCompartida.PeriodoPlanilla = row["MES"].ToString();
            }
            catch
            { 
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkCompania_CheckedChanged_1(object sender, EventArgs e)
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

        private void chkPlanilla_CheckedChanged_1(object sender, EventArgs e)
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

        private void chkAnio_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkAnio.Checked)
            {
                cboAnio.Enabled = true;
                cboAnio.SelectedIndex = 0;
            }
            else
            {
                cboAnio.Enabled = false;
                cboAnio.SelectedIndex = -1;
            }
        }
    }
}

