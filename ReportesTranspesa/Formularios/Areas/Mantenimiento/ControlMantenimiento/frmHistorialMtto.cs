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
    public partial class frmHistorialMtto : Form
    {
        public int Opcion;
        DataTable dtListaMantenimiento = new DataTable();

        public frmHistorialMtto()
        {
            InitializeComponent();
            cbxTipoUnidad.SelectedIndexChanged -= cbxTipoUnidad_SelectedIndexChanged;
            cbxTipoMaquina.SelectedIndexChanged -= cbxTipoMaquina_SelectedIndexChanged;
            cbxTipoEquipo.SelectedIndexChanged -= cbxTipoEquipo_SelectedIndexChanged;
        }

        private void cbxTipoUnidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidad(); }

        private void cbxTipoMaquina_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas(); }

        private void cbxTipoEquipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEquipo(); }

        private void frmHistorialMtto_Load(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarMantenimientos(); }
            if (Opcion == 2) { ListarMantenimientoMaquinas(); }
            if (Opcion == 3) { ListarMantenimientoEquipo(); }
        }


        public void CargarComboUnidad()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(5, 1);
            cbxTipoUnidad.DataSource = dtTipo;
            cbxTipoUnidad.DisplayMember = "Descripcion";
            cbxTipoUnidad.ValueMember = "idTipoVehiculo";
        }

        public void CargarComboMaquinas()
        {
            DataTable dtTipo2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(2);
            cbxTipoMaquina.DataSource = dtTipo2;
            cbxTipoMaquina.DisplayMember = "DescripcionLocal";
            cbxTipoMaquina.ValueMember = "TipoMaquina";
        }

        public void CargarComboEquipo()
        {
            DataTable dtTipo3 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(4);
            cbxTipoEquipo.DataSource = dtTipo3;
            cbxTipoEquipo.DisplayMember = "EquipoNombre";
            cbxTipoEquipo.ValueMember = "Equipo";
        }

        public void ListarMantenimientos()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMtto(txtPlaca.Text, dtpFechaInicio.Text, dtpFechaFin.Text, Convert.ToInt32(cbxTipoUnidad.SelectedValue));
                dtgHistorialMtto.DataSource = dtListaMantenimiento;
                if (dtListaMantenimiento.Rows.Count > 0)
                {
                    dgvHistorialMttoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialMttoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvHistorialMttoVista.BestFitColumns();
                }
            }
        }

        public void ListarMantenimientoMaquinas()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMaquinarias(txtPlaca.Text, dtpFechaInicio.Text, dtpFechaFin.Text, Convert.ToString(cbxTipoMaquina.SelectedValue));
                dtgHistorialMtto.DataSource = dtListaMantenimiento;
                if (dtListaMantenimiento.Rows.Count > 0)
                {
                    dgvHistorialMttoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialMttoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvHistorialMttoVista.BestFitColumns();
                }
            }
        }

        public void ListarMantenimientoEquipo()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialEquipos(txtPlaca.Text, dtpFechaInicio.Text, dtpFechaFin.Text, Convert.ToString(cbxTipoEquipo.Text));
                dtgHistorialMtto.DataSource = dtListaMantenimiento;
                if (dtListaMantenimiento.Rows.Count > 0)
                {
                    dgvHistorialMttoVista.Columns["FECHA_ULTIMA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialMttoVista.Columns["FECHA_ULTIMA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvHistorialMttoVista.Columns["FechaRegistro"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialMttoVista.Columns["FechaRegistro"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvHistorialMttoVista.BestFitColumns();
                }
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarMantenimientos(); }
            if (Opcion == 2) { ListarMantenimientoMaquinas(); }
            if (Opcion == 3) { ListarMantenimientoEquipo(); }
        }

        private void cbxTipoUnidad_DropDownClosed(object sender, EventArgs e) { ListarMantenimientos(); }

        private void cbxTipoMaquina_DropDownClosed(object sender, EventArgs e) { ListarMantenimientoMaquinas(); }

        private void cbxTipoEquipo_DropDownClosed(object sender, EventArgs e) { ListarMantenimientoEquipo(); }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarMantenimientos(); }
                if (Opcion == 2) { ListarMantenimientoMaquinas(); }
                if (Opcion == 3) { ListarMantenimientoEquipo(); }
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarMantenimientos(); }
                if (Opcion == 2) { ListarMantenimientoMaquinas(); }
                if (Opcion == 3) { ListarMantenimientoEquipo(); }
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarMantenimientos(); }
                if (Opcion == 2) { ListarMantenimientoMaquinas(); }
                if (Opcion == 3) { ListarMantenimientoEquipo(); }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgHistorialMtto.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE MANTENIMIENTOS REALIZADOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgHistorialMtto.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
