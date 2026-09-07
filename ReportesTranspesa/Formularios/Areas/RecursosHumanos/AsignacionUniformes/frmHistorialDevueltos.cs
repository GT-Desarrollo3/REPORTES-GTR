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
using Negocio;
using Comun;
using ReportesTranspesa.Sistema;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.AsignacionUniformes
{
    public partial class frmHistorialDevueltos : Form
    {
        public frmHistorialDevueltos()
        {
            InitializeComponent();
            cbxUniforme.SelectedIndexChanged -= cbxUniforme_SelectedIndexChanged;
        }

        private void cbxUniforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboUniforme();
        }

        private void frmHistorialDevueltos_Load(object sender, EventArgs e)
        {
            FechaInicio.Value = new DateTime(FechaInicio.Value.Year, FechaInicio.Value.Month, 1);
            FechaFin.Value = DateTime.Now;
            CargarComboUniforme();
        }


        private void CargarComboUniforme()
        {
            DataTable dtUniforme = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarUniformes(2);
            cbxUniforme.DataSource = dtUniforme;
            cbxUniforme.DisplayMember = "NombreUniforme";
            cbxUniforme.ValueMember = "idUniforme";
        }

        public void ListarDevueltos()
        {
            string fechin, fechfin;
            fechin = FechaInicio.Value.ToShortDateString() + " 00:00:00";
            fechfin = FechaFin.Value.ToShortDateString() + " 23:59:59";

            if (FechaInicio.Value > FechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FechaInicio.Focus();
                return;
            }
            else
            {
                dtgHistorialDevueltos.DataSource = null;
                dgvHistorialDevueltosVista.Columns.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_ControlUniformes_ListarHistorial(txtPersonal.Text, FechaInicio.Text, FechaFin.Text, Convert.ToInt32(cbxUniforme.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    dtgHistorialDevueltos.DataSource = dt;
                    dgvHistorialDevueltosVista.Columns["idAsignarUniforme"].Visible = false;
                    dgvHistorialDevueltosVista.Columns["idPersonal"].Visible = false;
                    dgvHistorialDevueltosVista.Columns["department"].Visible = false;
                    dgvHistorialDevueltosVista.Columns["CodigoPuesto"].Visible = false;
                    dgvHistorialDevueltosVista.Columns["idUniforme"].Visible = false;

                    dgvHistorialDevueltosVista.BestFitColumns();
                }
            }
        }


        private void dgvHistorialDevueltosVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "DiasRestantes")
            {
                if (Convert.ToInt32(e.CellValue) <= 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                }

                if (Convert.ToInt32(e.CellValue) > 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(31, 255, 0);
                }
            }
        }

        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ListarDevueltos();
            }
        }

        private void FechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ListarDevueltos();
            }
        }

        private void FechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ListarDevueltos();
            }
        }

        private void cbxUniforme_DropDownClosed(object sender, EventArgs e)
        {
            ListarDevueltos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarDevueltos();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgHistorialDevueltos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Historial de Uniformes Devueltos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgHistorialDevueltos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
