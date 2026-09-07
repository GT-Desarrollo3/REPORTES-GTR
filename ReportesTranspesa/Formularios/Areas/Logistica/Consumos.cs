using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using System.IO;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class Consumos : Form
    {
        DataTable dt;
        public Consumos()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dt = clsLogisticaBL.Instancia.ReportesApp_Logistica_ReporteConsumibles(dtpFechaInicio.Text, dtpFechaFin.Text);
                if (dt.Rows.Count > 0)
                {
                    dtgTarifaOT.DataSource = dt;
                }
                else
                {
                    dtgTarifaOT.DataSource = null;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void Consumos_Load(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgTarifaOT.DataSource == null)
            {
                MessageBox.Show("No hay registros.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE CONSUMOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgTarifaOT.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvTarifaOTVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "CentroCosto")
            {
                string valor = Convert.ToString(e.CellValue);

                if (string.IsNullOrWhiteSpace(valor))
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }

            if (e.Column.FieldName == "CuentaContable")
            {
                string valor = Convert.ToString(e.CellValue);

                if (string.IsNullOrWhiteSpace(valor))
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }
    }
}
