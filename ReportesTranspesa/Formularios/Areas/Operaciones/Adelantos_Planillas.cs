using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;
using Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.IO;
using System.Diagnostics;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Adelantos_Planillas : MetroFramework.Forms.MetroForm
    {
        public Adelantos_Planillas()
        {
            InitializeComponent();
        }

        private void Adelantos_Planillas_Load(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetDataAdelantosPlanillas();
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                dtgvDataView.Columns["PAGADO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["PAGADO"].DisplayFormat.FormatString = "c2";
                dtgvDataView.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["FECHA"].DisplayFormat.FormatString = "g";
                dtgvDataView.Columns["PAGO"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataView.Columns["PAGO"].DisplayFormat.FormatString = "g";
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }
    }
}