using System;
using System.ComponentModel;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class MayorDetallado : MetroFramework.Forms.MetroForm
    {
        public MayorDetallado()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string compañia = "10000000";
            switch (cboCompania.SelectedIndex)
            {
                case 0: //transpesa
                    compañia = "10000000";
                    break;
                case 1: //control
                    compañia = "20000000";
                    break;
                case 2: //adriel
                    compañia = "30000000";
                    break;
                case 3: //bra
                    compañia = "40000000";
                    break;
                case 4: // almacenes
                    compañia = "ALQALM00";
                    break;
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetDataMayorDetallado(txtPerIni.Text.Trim(), txtPerFin.Text.Trim(), compañia, txtDocumento.Text);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                //dtgvDataView.Columns[8].GroupIndex = 0;
                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["Cuenta"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                dtgvDataView.Columns["Debe(S/.)"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["Debe(S/.)"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["Haber(S/.)"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["Haber(S/.)"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["Debe($)"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["Debe($)"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["Haber($)"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["Haber($)"].DisplayFormat.FormatString = "n2";
                dtgvDataView.ExpandAllGroups();
                dtgvDataView.BestFitColumns();
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte Mayor Detallado del " + txtPerIni.Text + " al " + txtPerFin.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void MayorDetallado_Load(object sender, EventArgs e)
        {
            cboCompania.SelectedIndex = 0;
            string mes;
            if (DateTime.Now.Month < 10) 
            {
                mes = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                mes = DateTime.Now.Month.ToString();
            }
            txtPerIni.Text = DateTime.Now.Year.ToString() + mes;
            txtPerFin.Text = DateTime.Now.Year.ToString() + mes;
        }
    }
}
