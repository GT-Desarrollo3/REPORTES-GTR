using ReportesTranspesa.Properties;
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
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using Comun;
using System.Globalization;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmHistorialReten : Form
    {
        public frmHistorialReten()
        {
            InitializeComponent();
            fechaInicio.ValueChanged -= fechaInicio_ValueChanged;
            txtNombre.KeyPress -= txtNombre_KeyPress;
        }

        private void frmHistorialReten_Shown(object sender, EventArgs e)
        {
            txtNombre.Focus();
        }

        private void frmHistorialReten_Load(object sender, EventArgs e)
        {
            ListarHistorialReten();
            fechaInicio.ValueChanged += fechaInicio_ValueChanged;
            txtNombre.KeyPress += txtNombre_KeyPress;
        }


        private void ListarHistorialReten()
        {
            try
            {
                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ListarHistorialRetenes(fechaInicio.Text, fechaFin.Text, txtNombre.Text);

                if (dt.Rows.Count > 0)
                {
                    dtgHistorial.DataSource = dt;
                    dgvHistorialVista.Columns["idGasto"].Visible = false;
                    dgvHistorialVista.Columns["idPersona"].Visible = false;
                    dgvHistorialVista.Columns["Importe"].DisplayFormat.FormatType = FormatType.Numeric;
                    dgvHistorialVista.Columns["Importe"].DisplayFormat.FormatString = "N2";

                    dgvHistorialVista.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(dgvHistorialVista.Columns["FechaRegistro"], DevExpress.Data.ColumnSortOrder.Descending)
                    }, 1);

                    dgvHistorialVista.Columns["Importe"].Summary.Clear(); // limpia del total parte inferior
                    dgvHistorialVista.Columns["Importe"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Importe", "Total = {0:N2}");
                }

                dgvHistorialVista.BestFitColumns();
                dgvHistorialVista.ExpandAllGroups();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHistorialVista.DataSource == null)
                {
                    MessageBox.Show("No hay datos para exportar.", "AVISO");
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Historial de Gastos del " + fechaInicio.Value.ToString("dd_MM_yyyy") + " al " + fechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvHistorialVista.ExportToXlsx(nombre);
                    System.Diagnostics.Process.Start(nombre);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ListarHistorialReten();
        }

        private void fechaInicio_ValueChanged(object sender, EventArgs e)
        {
            ListarHistorialReten();
        }

        private void fechaFin_ValueChanged(object sender, EventArgs e)
        {
            ListarHistorialReten();
        }
    }
}
