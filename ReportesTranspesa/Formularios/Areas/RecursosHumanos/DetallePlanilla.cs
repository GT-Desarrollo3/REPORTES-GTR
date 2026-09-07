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
using ReportesTranspesa.Formularios.Areas;
using System.Globalization;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class DetallePlanilla : MetroFramework.Forms.MetroForm
    {
        public DetallePlanilla()
        {
            InitializeComponent();
        }

     

        private void DetallePlanilla_Load(object sender, EventArgs e)
        {
            MuestraPlanilla();
        }

        public void MuestraPlanilla()
        {
            dtgvPlanillaDetalle.DataSource = null;
            dtgvPlanillaDetalleView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            const string transpesa = "10000000";
            const string bra = "40000000";
            const string altra = "50000000";
            const string amt = "60000000";
            const string aduanas = "70000000";
            string periodo = "";
            lblPeriodo.Text = PlanillasTrabajadores.ClaseCompartida.PeriodoPlanilla;
            periodo = lblPeriodo.Text;
            
            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasTrabajores(transpesa, bra, altra, amt, aduanas, periodo);
            if (dt.Rows.Count > 0)
            {
                dtgvPlanillaDetalle.DataSource = dt;
                GridView gridView = dtgvPlanillaDetalle.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["TIPOTRABAJADOR"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                dtgvPlanillaDetalleView.ExpandAllGroups();
                dtgvPlanillaDetalleView.Columns["Sueldo Bruto"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvPlanillaDetalleView.Columns["Sueldo Bruto"].DisplayFormat.FormatString = "c2";
                dtgvPlanillaDetalleView.Columns["Sueldo Bruto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Sueldo Bruto", "Total ={0:c2}");
                dtgvPlanillaDetalleView.BestFitColumns();
            }
            else
            {
               
                MessageBox.Show("No hubo resultados","Error");
                
            }
        }
    }
}
