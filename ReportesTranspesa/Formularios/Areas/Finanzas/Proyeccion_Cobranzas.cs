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
using DevExpress.Data;
using DevExpress.XtraPivotGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class Proyeccion_Cobranzas : MetroFramework.Forms.MetroForm
    {
        public Proyeccion_Cobranzas()
        {
            InitializeComponent();
        }

        int cliente = -1;
        int documentos;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string compañia = "";
            switch (cboCompañia.SelectedIndex)
            {
                case 0: compañia = "10000000";
                    break;

                case 1: compañia = "40000000";
                    break;

                case 2: compañia = "50000000";                
                    break;
            }
            dtgvDataResumen.Visible = false;
            dtgvData.Visible = true;
            if (txtCliente.Text.Trim() == "")
            {
                cliente = -1;
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            //dt = clsFinanzasBL.Instancia.GetProyeccionCobranza2(compañia, dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString(), cliente);
            dt = clsFinanzasBL.Instancia.GetProyeccionCobranza2(compañia, dtpFechaIni.Text, dtpFechaFin.Text, cliente);
            //dt = clsFinanzasBL.Instancia.GetProyeccionCobranza2(transpesa, bra, altra, dtpFechaIni.Text, dtpFechaFin.Text, cliente);
            //dt = clsFinanzasBL.Instancia.GetProyeccionCobranza(dtpFechaIni.Text, dtpFechaFin.Text, cliente);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["Mes"].Visible = false;
                dtgvDataView.Columns["Nombre del Mes"].Visible = false;
                dtgvDataView.Columns["Proyeccion"].DisplayFormat.FormatType = FormatType.Custom;
                dtgvDataView.Columns["Proyeccion"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatString = "N2";
                dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatString = "C2";
                dtgvDataView.Columns["SaldoPendiente"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["SaldoPendiente"].DisplayFormat.FormatString = "N2";
                dtgvDataView.Columns["SaldoPendiente"].DisplayFormat.FormatString = "C2";
                dtgvDataView.Columns["MontoPagado"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["MontoPagado"].DisplayFormat.FormatString = "N2";
                dtgvDataView.Columns["MontoPagado"].DisplayFormat.FormatString = "C2";
                dtgvDataView.Columns["MontoTotal"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MontoTotal", "Total={0:c2}");
                dtgvDataView.Columns["MontoPagado"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MontoPagado", "Total={0:c2}");
                dtgvDataView.Columns["SaldoPendiente"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SaldoPendiente", "Total={0:c2}");
                dtgvDataView.Columns["NumeroDocumento"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "NumeroDocumento", "Total Documentos={0}");
                txtPendientes.Text = documentos.ToString();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);

                lvCliente.Columns[0].Width = 0;
                lvCliente.Columns[1].Width = 206;
                lvCliente.Columns[2].Width = 110;

                lvCliente.BringToFront();
                lvCliente.Visible = true;
                lvCliente.Focus();
                splitContainer1.SplitterDistance = lvCliente.Top + lvCliente.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_Enter(object sender, EventArgs e)
        {
            if (!lvCliente.Items.Count.Equals(0))
            {
                lvCliente.Items[0].Selected = true;
            }
        }

        private void lvCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void Proyeccion_Cobranzas_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtgvDataResumen.Visible = false;
            cboCompañia.SelectedIndex = 0;
            cboCompañia.Enabled = true;
            //btnResumen.Visible = false;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvDataResumen.Visible == false)
            {
                #region Detalle
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
                    string nombre = System.IO.Path.Combine(desktop, "Proyeccion de Cobranzas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                #endregion
            }
            else
            {
                #region Resumen
                if (dtgvDataResumen.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Proyeccion de Cobranzas Resumen " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvDataResumen.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                #endregion
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

        private void dtgvDataView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                documentos = 0;
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
               if (View.GetRowCellValue(e.RowHandle, "NumeroDocumento").ToString() != "") { documentos = documentos + 1; }
                 
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
               e.TotalValue = documentos;
            }     
        }

        private void btnResumen_Click(object sender, EventArgs e)
        {
            dtgvData.Visible = false;
            dtgvDataResumen.Visible = true;
            string compañia = "";
            switch (cboCompañia.SelectedIndex)
            {
                case 0: compañia = "10000000";
                    break;

                case 1: compañia = "40000000";
                    break;

                case 2: compañia = "50000000";
                    break;
            }
            if (txtCliente.Text.Trim() == "")
            {
                cliente = -1;
            }
            dtgvDataResumen.DataSource = null;
            dtgvDataResumen.Fields.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            //dt = clsFinanzasBL.Instancia.GetProyeccionCobranza(dtpFechaIni.Text, dtpFechaFin.Text, cliente);
            dt = clsFinanzasBL.Instancia.GetProyeccionCobranza2Resumen(compañia, dtpFechaIni.Text, dtpFechaFin.Text, cliente);
            if (dt.Rows.Count > 0)
            {
                CreaColumnasPivotGrid();
                dtgvDataResumen.DataSource = dt;
                dtgvDataResumen.BestFitRowArea();
                dtgvDataResumen.BestFitColumnArea();
                //dtgvDataView.Columns["SaldoPendiente"].DisplayFormat.FormatType = FormatType.Numeric;                
                //dtgvDataView.Columns["SaldoPendiente"].DisplayFormat.FormatString = "C2";
                //dtgvDataView.Columns["SaldoPendiente"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SaldoPendiente", "Total={0:c2}");
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
            campoPivote = new PivotGridField("ClienteNombre", PivotArea.RowArea);
            PivotGridField campoProyeccion = new PivotGridField("Proyeccion", PivotArea.ColumnArea);
            campoProyeccion.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime; 
            campoProyeccion.CellFormat.FormatString = "dd/MMM/yyyy";
            campoProyeccion.Caption = "Proyeccion";
            PivotGridField campoTotal = new PivotGridField("SaldoPendiente", PivotArea.DataArea);
            campoTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            campoTotal.CellFormat.FormatString = "c2";
            dtgvDataResumen.Fields.AddRange(new PivotGridField[] {campoPivote, 
            campoProyeccion,campoTotal});
            campoPivote.AreaIndex = 0;
            campoProyeccion.AreaIndex = 2;
            //campoMes.AreaIndex = 1;s
            //campoAño.AreaIndex = 0;
        }

        private void dtgvDataResumen_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
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
    }
}
