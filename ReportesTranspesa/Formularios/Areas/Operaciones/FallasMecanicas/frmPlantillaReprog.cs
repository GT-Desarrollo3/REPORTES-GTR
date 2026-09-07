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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmPlantillaReprog : Form
    {
        public int Opcion;
        
        public frmPlantillaReprog()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void frmPlantillaReprog_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            CargarComboOperacion();
            cbxOperacion.Text = "TODO";

            if (Opcion == 1) { ListarReprogramacion(); }
            else { ListarUbicaciones(); }
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(5, "");
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void ListarReprogramacion()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                DataTable dtListaReprog = new DataTable();
                dtListaReprog = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarReprogramacion(txtBuscarPlaca.Text, cbxOperacion.Text,
                                                             dtpFechaIni.Text, dtpFechaFin.Text);
                dtgPlantillaReprog.DataSource = dtListaReprog;

                if (dtListaReprog.Rows.Count > 0)
                {
                    dgvPlantillaReprogView.Columns["FECHA_ORIGINAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvPlantillaReprogView.Columns["FECHA_ORIGINAL"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvPlantillaReprogView.Columns["FECHA_REPROG"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvPlantillaReprogView.Columns["FECHA_REPROG"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvPlantillaReprogView.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvPlantillaReprogView.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvPlantillaReprogView.Columns["PLACA"].Summary.Clear();
                    dgvPlantillaReprogView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvPlantillaReprogView.BestFitColumns();
                }
            }
        }

        public void ListarUbicaciones()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                DataTable dtListaUbicacion = new DataTable();
                dtListaUbicacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUbicaciones(txtBuscarPlaca.Text, cbxOperacion.Text,
                                                             dtpFechaIni.Text, dtpFechaFin.Text);
                dtgPlantillaReprog.DataSource = dtListaUbicacion;

                if (dtListaUbicacion.Rows.Count > 0)
                {
                    dgvPlantillaReprogView.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvPlantillaReprogView.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvPlantillaReprogView.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvPlantillaReprogView.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvPlantillaReprogView.Columns["PLACA"].Summary.Clear();
                    dgvPlantillaReprogView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvPlantillaReprogView.BestFitColumns();
                }
            }
        }


        private void txtBuscarPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarReprogramacion(); }
                else { ListarUbicaciones(); }
            }
        }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarReprogramacion(); }
            else { ListarUbicaciones(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarReprogramacion(); }
                else { ListarUbicaciones(); }
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarReprogramacion(); }
                else { ListarUbicaciones(); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarReprogramacion(); }
            else { ListarUbicaciones(); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgPlantillaReprog.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                if (Opcion == 1)
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "PLANTILLA DE REPROGRAMACION - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgPlantillaReprog.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "CAMBIOS DE UBICACIÓN - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgPlantillaReprog.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }
    }
}
