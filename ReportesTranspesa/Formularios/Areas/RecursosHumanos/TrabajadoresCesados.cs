using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportesTranspesa.Sistema;
using Negocio;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class TrabajadoresCesados : MetroFramework.Forms.MetroForm
    {
        public TrabajadoresCesados()
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
            dtgvPlanilla.DataSource = null;
            dtgvPlanillaView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string transpesa = "10000000";
            string bra = "40000000";
            string altra = "50000000";
            string amt = "60000000";
            string aduanas = "70000000";
            //string filtro = "";
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

            dt = clsRecursosHumanosBL.Instancia.GetDataTrabajoresCesados(transpesa, bra, altra, amt, aduanas, dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59");
            if (dt.Rows.Count > 0)
            {
                dtgvPlanilla.DataSource = dt;
                GridView gridView = dtgvPlanilla.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["TIPOTRABAJADOR"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                dtgvPlanillaView.ExpandAllGroups();
                dtgvPlanillaView.Columns["ID"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ID", "Total ={0}");
                dtgvPlanillaView.BestFitColumns();

                if (rbEmpleados.Checked)
                {
                    dtgvPlanillaView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'EMPLEADOS'");
                    return;
                }
                if (rbObreros.Checked)
                {
                    dtgvPlanillaView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'OBREROS' AND [CARGO] != 'CONDUCTOR'");
                    return;
                }
                if (rbChoferes.Checked)
                {
                    dtgvPlanillaView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'OBREROS' AND [CARGO] = 'CONDUCTOR'");                    
                    return;
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private Microsoft.Office.Interop.Excel.Application app;

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvPlanilla.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
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
                string nombre = System.IO.Path.Combine(desktop, "Listado general de trabajadores Cesados (" + tipo + ") " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvPlanilla.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
            }
        }

        private void TrabajadoresCesados_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }
    }
}
