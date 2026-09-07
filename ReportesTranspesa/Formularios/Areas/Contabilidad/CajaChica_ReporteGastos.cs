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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class CajaChica_ReporteGastos : MetroFramework.Forms.MetroForm
    {
        public CajaChica_ReporteGastos()
        {
            InitializeComponent();
        }

        int beneficiario = -1;
        int proveedor = -1;
        string centrocosto = "";

        private void CajaChica_ReporteGastos_Load(object sender, EventArgs e)
        {
            #region llenaCombos
            DataTable dt = new DataTable();
            dt = clsConsultaBL.Instancia.GetCompañias();
            cboCompania.DataSource = dt;
            cboCompania.DisplayMember = "COMPDESC";
            cboCompania.ValueMember = "IDCOMP";
            cboCompania.SelectedIndex = 0;
            dt = clsConsultaBL.Instancia.GetUnidadesReplicacion();
            cboUnidadRep.DataSource = dt;
            cboUnidadRep.DisplayMember = "REPDESC";
            cboUnidadRep.ValueMember = "IDREP";
            cboUnidadRep.SelectedIndex = 0;

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("IDTIPODOC");
            dt2.Columns.Add("TIPODOCDESC");
            dt2.Columns.Add("GVAUTOMATICO");
            DataRow fila = dt2.NewRow();
            fila["IDTIPODOC"] = "C";
            fila["TIPODOCDESC"] = "Caja Chica";
            dt2.Rows.Add(fila);
            DataRow fila2 = dt2.NewRow();
            fila2["IDTIPODOC"] = "R";
            fila2["TIPODOCDESC"] = "Reporte de Gastos";
            dt2.Rows.Add(fila2);
            DataRow fila3 = dt2.NewRow();
            fila3["IDTIPODOC"] = "R";
            fila3["TIPODOCDESC"] = "Reporte GV Automatico";
            dt2.Rows.Add(fila3);
            cboTipoDoc.DataSource = dt2;
            cboTipoDoc.DisplayMember = "TIPODOCDESC";
            cboTipoDoc.ValueMember = "IDTIPODOC";
            cboTipoDoc.SelectedIndex = 0;

            dt = clsConsultaBL.Instancia.GetUnidadesNegocio();
            cboUnidadNeg.DataSource = dt;
            cboUnidadNeg.DisplayMember = "NEGDESC";
            cboUnidadNeg.ValueMember = "IDNEG";
            cboUnidadNeg.SelectedIndex = 0;
            dt = clsConsultaBL.Instancia.GetConceptos();
            cboConcepto.DataSource = dt;
            cboConcepto.DisplayMember = "CONCEPDESC";
            cboConcepto.ValueMember = "IDCONCEP";
            cboConcepto.SelectedIndex = 0;

            DataTable dt3 = new DataTable();
            dt3.Columns.Add("IDESTADO");
            dt3.Columns.Add("ESTADODESC");
            DataRow fila4 = dt3.NewRow();
            fila4["IDESTADO"] = "";
            fila4["ESTADODESC"] = "TODOS";
            dt3.Rows.Add(fila4);
            DataRow fila5 = dt3.NewRow();
            fila5["IDESTADO"] = "PR";
            fila5["ESTADODESC"] = "En Preparación";
            dt3.Rows.Add(fila5);
            DataRow fila6 = dt3.NewRow();
            fila6["IDESTADO"] = "AP";
            fila6["ESTADODESC"] = "Aprobado";
            dt3.Rows.Add(fila6);
            DataRow fila7 = dt3.NewRow();
            fila7["IDESTADO"] = "TR";
            fila7["ESTADODESC"] = "Transferido";
            dt3.Rows.Add(fila7);
            DataRow fila8 = dt3.NewRow();
            fila8["IDESTADO"] = "PA";
            fila8["ESTADODESC"] = "Pagado";
            dt3.Rows.Add(fila8);
            cboEstado.DataSource = dt3;
            cboEstado.DisplayMember = "ESTADODESC";
            cboEstado.ValueMember = "IDESTADO";
            cboEstado.SelectedIndex = 0;
            #endregion
        }

        private void chkFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechas.Checked)
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
                dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            }
            else
            {
                dtpFechaIni.Value = DateTime.Now;
                dtpFechaFin.Value = DateTime.Now;
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
            }
        }

        private void txtBeneficiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvBeneficiario, clsConsultaBL.Instancia.GetPersona(txtBeneficiario.Text),true, false, false);

                lvBeneficiario.Columns[0].Width = 0;
                lvBeneficiario.Columns[1].Width = 206;
                lvBeneficiario.Columns[2].Width = 110;

                lvBeneficiario.BringToFront();
                lvBeneficiario.Visible = true;
                lvBeneficiario.Focus();
                splitContainer1.SplitterDistance = lvBeneficiario.Top + lvBeneficiario.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvBeneficiario.Visible = false;
                txtBeneficiario.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void lvBeneficiario_Enter(object sender, EventArgs e)
        {
            if (!lvBeneficiario.Items.Count.Equals(0))
            {
                lvBeneficiario.Items[0].Selected = true;
            }
        }

        private void lvBeneficiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvBeneficiario.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvBeneficiario.SelectedItems[0];

                beneficiario = Int32.Parse(ItemActual.Text);
                txtBeneficiario.Text = ItemActual.SubItems[1].Text;
                lvBeneficiario.Visible = false;
                txtBeneficiario.Focus();
                splitContainer1.SplitterDistance = 138;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvBeneficiario.Visible = false;
                txtBeneficiario.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void lvBeneficiario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvBeneficiario.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvBeneficiario.SelectedItems[0];

                beneficiario = Int32.Parse(ItemActual.Text);
                txtBeneficiario.Text = ItemActual.SubItems[1].Text;
                lvBeneficiario.Visible = false;
                txtBeneficiario.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvProveedor, clsConsultaBL.Instancia.GetPersona(txtProveedor.Text), true, false, false);

                lvProveedor.Columns[0].Width = 0;
                lvProveedor.Columns[1].Width = 206;
                lvProveedor.Columns[2].Width = 110;

                lvProveedor.BringToFront();
                lvProveedor.Visible = true;
                lvProveedor.Focus();
                splitContainer1.SplitterDistance = lvProveedor.Top + lvProveedor.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void lvProveedor_Enter(object sender, EventArgs e)
        {
            if (!lvProveedor.Items.Count.Equals(0))
            {
                lvProveedor.Items[0].Selected = true;
            }
        }

        private void lvProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];

                proveedor = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[1].Text;
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 138;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void lvProveedor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];

                proveedor  = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[1].Text;
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void txtCCostos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCentroCostos, clsConsultaBL.Instancia.GetCentroCosto(txtCCostos.Text), true, false, false);

                lvCentroCostos.Columns[0].Width = 89;
                lvCentroCostos.Columns[1].Width = 210;

                lvCentroCostos.BringToFront();
                lvCentroCostos.Visible = true;
                lvCentroCostos.Focus();
                splitContainer1.SplitterDistance = lvCentroCostos.Top + lvCentroCostos.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCentroCostos.Visible = false;
                txtCCostos.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void lvCentroCostos_Enter(object sender, EventArgs e)
        {
            if (!lvCentroCostos.Items.Count.Equals(0))
            {
                lvCentroCostos.Items[0].Selected = true;
            }
        }

        private void lvCentroCostos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvCentroCostos.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCentroCostos.SelectedItems[0];

                centrocosto = ItemActual.Text;
                txtCCostos.Text = ItemActual.SubItems[1].Text;
                lvCentroCostos.Visible = false;
                txtCCostos.Focus();
                splitContainer1.SplitterDistance = 138;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCentroCostos.Visible = false;
                txtCCostos.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void lvCentroCostos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvCentroCostos.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCentroCostos.SelectedItems[0];

                centrocosto = ItemActual.Text;
                txtCCostos.Text = ItemActual.SubItems[1].Text;
                lvCentroCostos.Visible = false;
                txtCCostos.Focus();
                splitContainer1.SplitterDistance = 138;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int docdesde,dochasta;
            if (txtBeneficiario.Text.Trim() == "") { beneficiario = -1; }
            if (txtProveedor.Text.Trim() == "") { proveedor = -1; }
            if (txtCCostos.Text.Trim() == "") { centrocosto = ""; }
            if (txtDocDesde.Text.Trim() == "")
            {
                docdesde = 0;
            }
            else
            {
                docdesde = Convert.ToInt32(txtDocDesde.Text);
            }
            if(txtDocHasta.Text.Trim() == "")
            {
                dochasta = 999999;
            }
            else
            {
                dochasta = Convert.ToInt32(txtDocHasta.Text);
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            if (cboTipoDoc.SelectedIndex == 2)
            {
                dt = clsContabilidadBL.Instancia.GetDataRepGastosViaje(cboCompania.SelectedValue.ToString(), cboUnidadRep.SelectedValue.ToString(),
                cboUnidadNeg.SelectedValue.ToString(), dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", docdesde, dochasta, cboConcepto.SelectedValue.ToString(),
                cboEstado.SelectedValue.ToString(), beneficiario, proveedor, centrocosto);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    GridView gridView = dtgvData.FocusedView as GridView;
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["CUENTA CONTABLE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                    dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatString = "c2";
                    dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                    dtgvDataView.Columns["MONTO AFECTO"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["MONTO AFECTO"].DisplayFormat.FormatString = "c2";
                    dtgvDataView.Columns["IGV"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["IGV"].DisplayFormat.FormatString = "c2";
                    

                    GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                    item1.FieldName = "MONTO";
                    item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item1.DisplayFormat = "TOTAL CUENTA {0:c2}";
                    dtgvDataView.GroupSummary.Add(item1);

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
            else
            {
                dt = clsContabilidadBL.Instancia.GetCajaChicaRepGastos(cboCompania.SelectedValue.ToString(), cboUnidadRep.SelectedValue.ToString(),
                cboTipoDoc.SelectedValue.ToString(), cboUnidadNeg.SelectedValue.ToString(), dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", docdesde, dochasta, cboConcepto.SelectedValue.ToString(),
                cboEstado.SelectedValue.ToString(), beneficiario, proveedor, centrocosto);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    GridView gridView = dtgvData.FocusedView as GridView;
                    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["CUENTA CONTABLE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                    dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatString = "c2";
                    dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "c2";
                    //dtgvDataView.Columns["MONTO AFECTO"].DisplayFormat.FormatType = FormatType.Numeric;
                    //dtgvDataView.Columns["MONTO AFECTO"].DisplayFormat.FormatString = "c2";
                    //dtgvDataView.Columns["IGV"].DisplayFormat.FormatType = FormatType.Numeric;
                    //dtgvDataView.Columns["IGV"].DisplayFormat.FormatString = "c2";

                    GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                    item1.FieldName = "MONTO";
                    item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item1.DisplayFormat = "TOTAL CUENTA {0:c2}";
                    dtgvDataView.GroupSummary.Add(item1);

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
            //dt = clsContabilidadBL.Instancia.GetCajaChicaRepGastos(cboCompania.SelectedValue.ToString(),cboUnidadRep.SelectedValue.ToString(),
            //    cboTipoDoc.SelectedValue.ToString(), cboUnidadNeg.SelectedValue.ToString(), dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //    dtpFechaFin.Value.ToShortDateString() + " 23:59:59",docdesde,dochasta,cboConcepto.SelectedValue.ToString(),
            //    cboEstado.SelectedValue.ToString(),beneficiario,proveedor,centrocosto);
            //if (dt.Rows.Count > 0)
            //{
            //    dtgvData.DataSource = dt;
            //    GridView gridView = dtgvData.FocusedView as GridView;
            //    gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
            //    new GridColumnSortInfo(gridView.Columns["CUENTA CONTABLE"], DevExpress.Data.ColumnSortOrder.Ascending), 
            //    }, 1);
            //    dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
            //    dtgvDataView.Columns["MONTO TOTAL"].DisplayFormat.FormatString = "c2";
            //    dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
            //    dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "c2";

            //    GridGroupSummaryItem item1 = new GridGroupSummaryItem();
            //    item1.FieldName = "MONTO";
            //    item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //    item1.DisplayFormat = "TOTAL CUENTA {0:c2}";
            //    dtgvDataView.GroupSummary.Add(item1);

            //    dtgvDataView.ExpandAllGroups();
            //    dtgvDataView.BestFitColumns();
            //}
            //else
            //{
            //    Mensaje m = new Mensaje();
            //    m.mensaje = "No hay data para mostrar";
            //    m.ShowDialog();
            //}
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Caja Chica-Gastos de Viaje " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void cboCompania_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
