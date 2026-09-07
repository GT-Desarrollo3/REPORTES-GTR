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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using Entidades;
using DevExpress.XtraPivotGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class ComercialvsContabilidad : MetroFramework.Forms.MetroForm
    {
        public ComercialvsContabilidad()
        {
            InitializeComponent();
        }

        private void ComercialvsContabilidad_Load(object sender, EventArgs e)
        {
            txtInvoice.Enabled = false;
            DateTime fecha = DateTime.Now;
            //string mes = fecha.Month.ToString();
            //string periodo = fecha.Year + mes;
            //txtPeriodoIni.Text = periodo;
            //txtPeriodoFin.Text = periodo;

            if (fecha.Month > 10)
            {
                string mes = fecha.Month.ToString();
                string periodo = fecha.Year + mes;
                txtPeriodoIni.Text = periodo;
                txtPeriodoFin.Text = periodo;
            }
            else
            {
                string mes = '0' + fecha.Month.ToString();
                string periodo = fecha.Year + mes;
                txtPeriodoIni.Text = periodo;
                txtPeriodoFin.Text = periodo;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            pivotGridControl1.DataSource = null;
            pivotGridControl1.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetComercialvsContabilidad(txtPeriodoIni.Text, txtPeriodoFin.Text);
            if (dt.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                //dt.Columns.Add("Verificacion",Type.GetType("System.Boolean"));
                //dt.Columns.Add("Verificacion", typeof(Boolean));
                //DataColumn newColumn = new DataColumn("Verificación", typeof(System.Boolean));
                //newColumn.DefaultValue = true;
                //dt.Columns.Add(newColumn);
                dtgvData.DataSource = dt;
                //dtgvDataView.Columns["Comercial"].DisplayFormat.FormatType = FormatType.Numeric;
                ////dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatString = "N2";
                //dtgvDataView.Columns["Comercial"].DisplayFormat.FormatString = "C2";
                //dtgvDataView.Columns["Contabilidad"].DisplayFormat.FormatType = FormatType.Numeric;
                ////dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatString = "N2";
                //dtgvDataView.Columns["Contabilidad"].DisplayFormat.FormatString = "C2";
                //dtgvDataView.Columns["Diferencia"].DisplayFormat.FormatType = FormatType.Numeric;
                ////dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatString = "N2";
                //dtgvDataView.Columns["Diferencia"].DisplayFormat.FormatString = "C2";
                dtgvDataView.BestFitColumns();
                pivotGridControl1.DataSource = dt;
                //pivotGridControl1.BestFitColumnArea();
                pivotGridControl1.BestFitRowArea();
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
            //if (chkProveedor.Checked == true)
            //{ 
            //    campoPivote = new PivotGridField("Busqueda", PivotArea.RowArea);
            //    PivotGridField campofiltro = new PivotGridField("Fuente", PivotArea.ColumnArea);
            //    campofiltro.Caption = "Fuente";
            //    PivotGridField campoSoles = new PivotGridField("MontoLocal", PivotArea.DataArea);
            //    campoSoles.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    PivotGridField campoDolares = new PivotGridField("MontoDolar", PivotArea.DataArea);
            //    campoDolares.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    //campoTotal.CellFormat.FormatString = "c2";
            //    pivotGridControl1.Fields.AddRange(new PivotGridField[] {campoPivote,campofiltro,campoSoles,campoDolares});
            //    campoPivote.AreaIndex = 0;
            //    campofiltro.AreaIndex = 1;
            //}
            //else
            //{ 
            //    campoPivote2 = new PivotGridField("invoice", PivotArea.RowArea);
            //    PivotGridField campofiltro = new PivotGridField("Fuente", PivotArea.ColumnArea);
            //    campofiltro.Caption = "Fuente";
            //    PivotGridField campoSoles = new PivotGridField("MontoLocal", PivotArea.DataArea);
            //    campoSoles.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    PivotGridField campoDolares = new PivotGridField("MontoDolar", PivotArea.DataArea);
            //    campoDolares.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    //campoTotal.CellFormat.FormatString = "c2";
            //    pivotGridControl1.Fields.AddRange(new PivotGridField[] {campoPivote2,campofiltro,campoSoles,campoDolares});
            //    campoPivote2.AreaIndex = 0;
            //    campofiltro.AreaIndex = 1;
            //}
            //campoPivote = new PivotGridField("Busqueda", PivotArea.RowArea);
            campoPivote2 = new PivotGridField("invoice", PivotArea.RowArea);
            PivotGridField campofiltro = new PivotGridField("Fuente", PivotArea.ColumnArea);
            campofiltro.Caption = "Fuente";
            //PivotGridField campofiltro1 = new PivotGridField("MontoLocal", PivotArea.ColumnArea);
            //campofiltro1.Caption = "MontoLocal";
            //PivotGridField campofiltro2 = new PivotGridField("MontoDolar", PivotArea.ColumnArea);
            //campofiltro2.Caption = "MontoDolar";
            PivotGridField campoSoles = new PivotGridField("MontoLocal", PivotArea.DataArea);
            campoSoles.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            PivotGridField campoDolares = new PivotGridField("MontoDolar", PivotArea.DataArea);
            campoDolares.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //campoTotal.CellFormat.FormatString = "c2";
            //pivotGridControl1.Fields.AddRange(new PivotGridField[] {campoPivote, campoPivote2,
            //campofiltro,campoSoles,campoDolares});
            pivotGridControl1.Fields.AddRange(new PivotGridField[] {campoPivote2, 
            campofiltro,campoSoles,campoDolares});
            //pivotGridControl1.Fields.AddRange(new PivotGridField[] {campoPivote2, 
            //campofiltro,campofiltro1,campofiltro2});
            campoPivote2.AreaIndex = 0;
            campofiltro.AreaIndex = 1;
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (pivotGridControl1.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Validacion de Cuentas por Cobrar del Periodo " + txtPeriodoIni.Text + " al " + txtPeriodoFin.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                pivotGridControl1.ExportToXlsx(nombre);
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

        private void chkProveedor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProveedor.Checked)
            {
                txtProveedor.Enabled = true;
                txtInvoice.ReadOnly = true;
                chkInvoice.Checked = false;
                txtInvoice.Clear();
            }
            else
            {
                txtProveedor.Enabled = false;
                txtInvoice.ReadOnly = false;
            }
        }

        private void chkInvoice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInvoice.Checked)
            {
                txtInvoice.Enabled = true;
                txtProveedor.ReadOnly = true;
                chkProveedor.Checked = false;
                txtProveedor.Clear();
            }
            else
            {
                txtInvoice.Enabled = false;
                txtProveedor.ReadOnly = false;
            }
        }
    }
}

