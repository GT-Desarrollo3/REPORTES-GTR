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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class frmKardexAlmacenes : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaKardex = new DataTable();

        public frmKardexAlmacenes()
        {
            InitializeComponent();
        }

        private void frmKardexAlmacenes_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            //ListarKardexAlmacen();
        }


        public void ListarKardexAlmacen()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaKardex = clsAlmacenBL.Instancia.ReportesApp_Almacen_Salaverry_ListarKardex(dtpFechaIni.Text, dtpFechaFin.Text, txtCliente.Text, txtProducto.Text, txtLote.Text);
                dtgKardexAlmacen.DataSource = dtListaKardex;

                if (dtListaKardex.Rows.Count > 0)
                {
                    dgvKardexAlmacenVista.Columns["FECHA_INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvKardexAlmacenVista.Columns["FECHA_INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvKardexAlmacenVista.Columns["CANTIDAD_BASE"].Summary.Clear();
                    dgvKardexAlmacenVista.Columns["CANTIDAD_BASE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD_BASE", "Total = {0:N2}");

                    dgvKardexAlmacenVista.BestFitColumns();
                }
            }
        }


        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKardexAlmacen(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKardexAlmacen(); }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKardexAlmacen(); }
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKardexAlmacen(); }
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKardexAlmacen(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarKardexAlmacen(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgKardexAlmacen.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "KARDEX DE ALMACENES - SALAVERRY - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgKardexAlmacen.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
