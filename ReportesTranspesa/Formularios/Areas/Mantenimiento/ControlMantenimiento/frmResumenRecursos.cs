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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmResumenRecursos : Form
    {
        DataTable dtResumenRecursos = new DataTable();
        DataTable dtResumenMO = new DataTable();

        public frmResumenRecursos()
        {
            InitializeComponent();
            cbxEspecialidad.SelectedIndexChanged -= cbxEspecialidad_SelectedIndexChanged;
        }

        private void cbxEspecialidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEspecialidades(); }

        private void frmResumenRecursos_Load(object sender, EventArgs e)
        {
            ListarResumenRecursos();
            CargarComboEspecialidades();
        }

        public void CargarComboEspecialidades()
        {
            DataTable dtTipo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto("C-2");
            cbxEspecialidad.DataSource = dtTipo;
            cbxEspecialidad.DisplayMember = "Descripcion";
            cbxEspecialidad.ValueMember = "idEspecialidad";
        }

        public void ListarResumenRecursos()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtResumenRecursos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarResumenRecursos(dtpFechaInicio.Text, dtpFechaFin.Text, txtItem.Text);
                dtgResumenRecursos.DataSource = dtResumenRecursos;
                if (dtResumenRecursos.Rows.Count > 0)
                {
                    dgvResumenRecursosVista.Columns["FechaProyectada"].Visible = false;

                    dgvResumenRecursosVista.Columns["TOTAL"].Summary.Clear();
                    dgvResumenRecursosVista.Columns["TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL", "Total = {0}");

                    dgvResumenRecursosVista.BestFitColumns();
                }
            }
        }

        public void ListarResumenMO()
        {
            if (dtpFechaInicio2.Value > dtpFechaFin2.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio2.Focus();
                return;
            }
            else
            {
                dtResumenMO = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_ListarResumen(dtpFechaInicio2.Text, dtpFechaFin2.Text, cbxEspecialidad.Text);
                dtgResumenMO.DataSource = dtResumenMO;
                if (dtResumenMO.Rows.Count > 0)
                {
                    dgvResumenMO.Columns["TIEMPO_DURACION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvResumenMO.Columns["TIEMPO_DURACION"].DisplayFormat.FormatString = "HH:mm:ss";

                    dgvResumenMO.BestFitColumns();
                }
            }
        }


        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResumenRecursos(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResumenRecursos(); }
        }

        private void txtItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResumenRecursos(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarResumenRecursos(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgResumenRecursos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "RESUMEN DE INSUMOS DE RECURSOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgResumenRecursos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtpFechaInicio2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResumenMO(); }
        }

        private void dtpFechaFin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResumenMO(); }
        }

        private void cbxEspecialidad_DropDownClosed(object sender, EventArgs e) { ListarResumenMO(); }

        private void btnBuscar2_Click(object sender, EventArgs e) { ListarResumenMO(); }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            if (dtgResumenMO.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "RESUMEN DE INSUMOS DE MANO DE OBRA - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgResumenMO.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
