using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmItemsPendientes : MetroFramework.Forms.MetroForm
    {
        public int Opcion;
        
        public frmItemsPendientes()
        {
            InitializeComponent();
            cbxCentroCosto.SelectedIndexChanged -= cbxCentroCosto_SelectedIndexChanged;
        }

        private void cbxCentroCosto_SelectedIndexChanged(object sender, EventArgs e) { CargarCentroCosto(); }

        private void frmItemsPendientes_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            CargarCentroCosto();
            cbCentroCosto.Checked = false;
            cbCentroCosto_CheckedChanged(sender, e);
            cbxCentroCosto.Text = "Tractos";
            ListarItemsPendientes();
        }


        public void CargarCentroCosto()
        {
            DataTable dtCentroCosto = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarCentroCosto(Utilitario.Instancia.SesionUsuario.usuario);
            cbxCentroCosto.DataSource = dtCentroCosto;
            cbxCentroCosto.DisplayMember = "LocalName";
            cbxCentroCosto.ValueMember = "CostCenter";
        }

        public void ListarItemsPendientes()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtItemsPendientes = clsLogisticaBL.Instancia.ReportesApp_Logistica_ItemsPendientes_Listar(dtpFechaInicio.Text, dtpFechaFin.Text, Convert.ToString(cbxCentroCosto.SelectedValue), txtItem.Text, Opcion);
                dtgItemsPendientes.DataSource = dtItemsPendientes;
                if (dtItemsPendientes.Rows.Count > 0)
                {
                    /*
                    dtgvItemsPendientesView.Columns["NRO"].Visible = false;
                    
                    dtgvItemsPendientesView.Columns["FECHA_DOCUMENTO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dtgvItemsPendientesView.Columns["FECHA_DOCUMENTO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    */

                    dtgvItemsPendientesView.Columns["CANTIDAD"].Summary.Clear();
                    dtgvItemsPendientesView.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total: {0:N2}");

                    dtgvItemsPendientesView.BestFitColumns();
                }
            }
        }


        private void dtgvItemsPendientesView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "CANTIDAD")
            {
                if (Convert.ToString(e.CellValue).Contains("-")) { e.Appearance.ForeColor = Color.Red; }
                else { e.Appearance.ForeColor = Color.Green; }
            }
        }

        private void cbCentroCosto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCentroCosto.Checked == true)
            {
                Opcion = 1;
                cbxCentroCosto.Enabled = true;
            }

            if (cbCentroCosto.Checked == false)
            {
                Opcion = 0;
                cbxCentroCosto.Enabled = false;
            }
        }

        private void cbxCentroCosto_DropDownClosed(object sender, EventArgs e) { ListarItemsPendientes(); }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarItemsPendientes(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarItemsPendientes(); }
        }

        private void txtItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarItemsPendientes(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarItemsPendientes(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgItemsPendientes.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE ITEMS PENDIENTES DE DESPACHO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgItemsPendientes.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
