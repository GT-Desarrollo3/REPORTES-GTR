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
    public partial class frmHistorialInspecciones : Form
    {
        DataTable dtInspecciones = new DataTable();
        public int Opcion;

        public frmHistorialInspecciones()
        {
            InitializeComponent();
            cbxTipo.SelectedIndexChanged -= cbxTipo_SelectedIndexChanged;
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas(); }

        private void frmHistorialInspecciones_Load(object sender, EventArgs e)
        {
            string UsuarioAcceso = Utilitario.Instancia.SesionUsuario.usuario;

            if (UsuarioAcceso == "PNAVARRO" || UsuarioAcceso == "LQUEZADA" || UsuarioAcceso == "MADELEINEC") { cbxSucursal.Text = "LIMA"; }
            else { cbxSucursal.Text = "TRUJILLO"; }
            
            CargarComboMaquinas();
            
            if (Opcion == 1) { ListarInspecciones(); }
            if (Opcion == 2) { ListarPedidos(); }
        }


        public void CargarComboMaquinas()
        {
            DataTable dtTipo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(3);
            cbxTipo.DataSource = dtTipo;
            cbxTipo.DisplayMember = "DescripcionLocal";
            cbxTipo.ValueMember = "TipoMaquina";
        }

        public void ListarInspecciones()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtInspecciones = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones(2, "", dtpFechaInicio.Text, dtpFechaFin.Text, txtPlaca.Text, cbxTipo.Text, cbxSucursal.Text);
                dtgInspecciones.DataSource = dtInspecciones;
                if (dtInspecciones.Rows.Count > 0)
                {
                    dgvInspeccionesVista.Columns["Mecanico1"].Visible = false;
                    dgvInspeccionesVista.Columns["Electrico2"].Visible = false;
                    dgvInspeccionesVista.Columns["Neumatico3"].Visible = false;
                    dgvInspeccionesVista.Columns["Soldador4"].Visible = false;

                    dgvInspeccionesVista.Columns["PROX_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["PROX_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvInspeccionesVista.Columns["PROX_MTTO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["PROX_MTTO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvInspeccionesVista.Columns["PLACA"].Summary.Clear();
                    dgvInspeccionesVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvInspeccionesVista.BestFitColumns();
                }
            }
        }

        public void ListarPedidos()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtInspecciones = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones(3, "", dtpFechaInicio.Text, dtpFechaFin.Text, txtPlaca.Text, cbxTipo.Text, cbxSucursal.Text);
                dtgInspecciones.DataSource = dtInspecciones;
                if (dtInspecciones.Rows.Count > 0)
                {
                    dgvInspeccionesVista.Columns["idInspeccionD"].Visible = false;
                    dgvInspeccionesVista.Columns["idInspeccionC"].Visible = false;

                    dgvInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvInspeccionesVista.Columns["PLACA"].Summary.Clear();
                    dgvInspeccionesVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvInspeccionesVista.BestFitColumns();
                }
            }
        }


        private void dtgInspecciones_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Vacio = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "PLACA").ToString();

                if (Vacio != "")
                {
                    frmInspeccionUnidades frmInspeccionUnidades = new frmInspeccionUnidades();
                    frmInspeccionUnidades.txtElectrico.Enabled = false;
                    frmInspeccionUnidades.txtMecanico.Enabled = false;
                    frmInspeccionUnidades.txtNeumatico.Enabled = false;
                    frmInspeccionUnidades.txtSoldador.Enabled = false;
                    frmInspeccionUnidades.btnAgregar.Enabled = false;
                    frmInspeccionUnidades.btnGuardar.Enabled = false;
                    frmInspeccionUnidades.btnAnular.Enabled = false;
                    frmInspeccionUnidades.btnGuardarPedido.Enabled = false;
                    frmInspeccionUnidades.btnRequerimiento.Enabled = false;
                    frmInspeccionUnidades.EliminarPedido = 0;
                    frmInspeccionUnidades.idInspeccionC = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "NRO"));
                    frmInspeccionUnidades.lblPlaca.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "PLACA").ToString();
                    frmInspeccionUnidades.lblOperacion.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "OPERACION").ToString();
                    frmInspeccionUnidades.lblMarca.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MARCA").ToString();
                    frmInspeccionUnidades.lblModelo.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MODELO").ToString();

                    frmInspeccionUnidades.Persona1 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Mecanico1"));
                    frmInspeccionUnidades.txtMecanico.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MECÁNICO").ToString();
                    frmInspeccionUnidades.Persona2 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Electrico2"));
                    frmInspeccionUnidades.txtElectrico.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "ELÉCTRICO").ToString();
                    frmInspeccionUnidades.Persona3 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Neumatico3"));
                    frmInspeccionUnidades.txtNeumatico.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "NEUMÁTICO").ToString();
                    frmInspeccionUnidades.Persona4 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Soldador4"));
                    frmInspeccionUnidades.txtSoldador.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "SOLDADOR").ToString();
                    frmInspeccionUnidades.dtpNuevaFechaI.Value = Convert.ToDateTime(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "INICIO_INSPECCION"));
                    frmInspeccionUnidades.dtpNuevaFechaF.Value = Convert.ToDateTime(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "FIN_INSPECCION"));

                    string Subtipo = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "SUBTIPO_UNIDAD").ToString();
                    if (Subtipo == "TRACTO") { frmInspeccionUnidades.TipoInspeccion = 1; }
                    else { frmInspeccionUnidades.TipoInspeccion = 2; }

                    frmInspeccionUnidades.Opcion = 2;
                    frmInspeccionUnidades.BuscarInspeccion();
                    frmInspeccionUnidades.Show();
                }
            }
            catch { }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarInspecciones(); }
                if (Opcion == 2) { ListarPedidos(); }
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarInspecciones(); }
                if (Opcion == 2) { ListarPedidos(); }
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Opcion == 1) { ListarInspecciones(); }
                if (Opcion == 2) { ListarPedidos(); }
            }
        }

        private void cbxTipo_DropDownClosed(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarInspecciones(); }
            if (Opcion == 2) { ListarPedidos(); }
        }

        private void cbxSucursal_DropDownClosed(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarInspecciones(); }
            if (Opcion == 2) { ListarPedidos(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1) { ListarInspecciones(); }
            if (Opcion == 2) { ListarPedidos(); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgInspecciones.DataSource == null)
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
                string nombre = "";
                if (Opcion == 1)
                { nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE INSPECCIONES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx"); }
                if (Opcion == 2)
                { nombre = System.IO.Path.Combine(desktop, "REGISTRO DE OBSERVACIONES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx"); }
                dtgInspecciones.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvInspeccionesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "OBSERVADO")
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }
        }
    }
}
