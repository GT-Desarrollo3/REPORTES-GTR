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

using System.Xml;
using System.IO;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones;

namespace ReportesTranspesa.Formularios.Areas.Seguridad.MedicoOcupacional
{
    public partial class frmListaEMO : Form
    {
        public DataTable DtCompania;
        public DataTable DtTipoDoc;
        public DataTable DtSucursal;
        public DataTable DtTipoRelacion;
        public DataTable DtTipoMoneda;
        public DataTable DtAccesos;
        public DataTable DtTipoUnidades;
        public DataTable DtCategoria;
        DataTable dtListaEMOS = new DataTable();
        DataTable dtListaHistorialEMOS = new DataTable();
        DataTable dtPermisos = new DataTable();

        public int AccionTipoDoc = 0;
        public int cnt;
        public int Opcion = 0;

        
        public frmListaEMO()
        {
            InitializeComponent();  
        }

        private void frmListaEMO_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaEMO");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevoDocumento.Enabled = true; }
                else { btnNuevoDocumento.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            cbxEstado.Text = "TODOS";
            ListarEMO();
            ListarHistorialEMO();
        }


        public void ListarEMO()
        {
            dtListaEMOS = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroEMO_ListarEMO(txtConductor.Text, cbxEstado.Text);
            dtgDocumentosEMO.DataSource = dtListaEMOS;
            if (dtListaEMOS.Rows.Count > 0)
            {
                dgvDocumentosEMO.Columns["idEMO"].Visible = false;
                dgvDocumentosEMO.Columns["idPersona"].Visible = false;
                dgvDocumentosEMO.Columns["TipoDocumento"].Visible = false;
                dgvDocumentosEMO.Columns["ENFERMEDAD"].Visible = false;
                dgvDocumentosEMO.Columns["Resultados"].Visible = false;
                dgvDocumentosEMO.Columns["Resultados2"].Visible = false;
                dgvDocumentosEMO.Columns["Resultados3"].Visible = false;

                dgvDocumentosEMO.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvDocumentosEMO.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvDocumentosEMO.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvDocumentosEMO.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";

                RepositoryItemHyperLinkEdit InformeSeguridad = new RepositoryItemHyperLinkEdit();
                dgvDocumentosEMO.Columns["DIRECTORIO"].ColumnEdit = InformeSeguridad;
                dgvDocumentosEMO.Columns["DIRECTORIO"].Width = 200;

                dgvDocumentosEMO.Columns["EMPLEADO"].Summary.Clear();
                dgvDocumentosEMO.Columns["EMPLEADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                dgvDocumentosEMO.BestFitColumns();
            }
        }

        public void ListarHistorialEMO()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaHistorialEMOS = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroEMO_ListarHistorialEMO(txtConductor.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                dtgHistorialEMO.DataSource = dtListaHistorialEMOS;
                if (dtListaHistorialEMOS.Rows.Count > 0)
                {
                    dgvHistorialEMO.Columns["idEMO"].Visible = false;
                    dgvHistorialEMO.Columns["idPersona"].Visible = false;
                    dgvHistorialEMO.Columns["TipoDocumento"].Visible = false;
                    dgvHistorialEMO.Columns["ENFERMEDAD"].Visible = false;
                    dgvHistorialEMO.Columns["DIRECTORIO"].Visible = false;
                    dgvHistorialEMO.Columns["Resultados"].Visible = false;
                    dgvHistorialEMO.Columns["Resultados2"].Visible = false;
                    dgvHistorialEMO.Columns["Resultados3"].Visible = false;

                    dgvHistorialEMO.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialEMO.Columns["FECHA_INICIO_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvHistorialEMO.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialEMO.Columns["FECHA_FIN_VALIDEZ"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvHistorialEMO.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialEMO.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                    dgvHistorialEMO.Columns["EMPLEADO"].Summary.Clear();
                    dgvHistorialEMO.Columns["EMPLEADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvHistorialEMO.BestFitColumns();
                }
            }
        }


        private void dgvDocumentosEMO_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "VIGENTE") { e.Appearance.BackColor = Color.YellowGreen; }

                if (e.CellValue.ToString() == "POR VENCER") { e.Appearance.BackColor = Color.Yellow; }

                if (e.CellValue.ToString() == "VENCIDO") { e.Appearance.BackColor = Color.Red; }
            }
        }

        private void btnNuevoDocumento_Click(object sender, EventArgs e)
        {
            frmNuevoEMO frmNuevoEMO = new frmNuevoEMO();
            frmNuevoEMO.Opcion = 1;
            frmNuevoEMO.Formulario = this;
            frmNuevoEMO.cbxCategoria.Text = "APTO";
            frmNuevoEMO.cbxTipoSangre.Text = "A+";
            frmNuevoEMO.dtpFechaIni.Value = DateTime.Now;
            frmNuevoEMO.dtpFechaFin.Value = DateTime.Now.Date.AddYears(1);
            frmNuevoEMO.ShowDialog();
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarEMO(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarEMO(); }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarEMO();
            ListarHistorialEMO();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgDocumentosEMO.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "DOCUMENTOS DE MÉDICO OCUPACIONAL - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgDocumentosEMO.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgDocumentosEMO_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    frmNuevoEMO frmNuevoEMO = new frmNuevoEMO();
                    frmNuevoEMO.Formulario = this;
                    frmNuevoEMO.Opcion = 2;
                    frmNuevoEMO.txtPersonal.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "EMPLEADO").ToString();
                    frmNuevoEMO.Persona = Convert.ToInt32(dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "idPersona"));
                    frmNuevoEMO.txtPersonal.ReadOnly = true;
                    frmNuevoEMO.txtDNI.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "DNI").ToString();
                    frmNuevoEMO.txtArea.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "ÁREA").ToString();
                    frmNuevoEMO.txtPuesto.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "PUESTO").ToString();
                    frmNuevoEMO.txtRutaLocal.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "DIRECTORIO").ToString();
                    frmNuevoEMO.dtpFechaIni.Value = Convert.ToDateTime(dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "FECHA_INICIO_VALIDEZ"));

                    string Vacio = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "FECHA_FIN_VALIDEZ").ToString();
                    if (Vacio != "")
                    { frmNuevoEMO.dtpFechaFin.Value = Convert.ToDateTime(dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "FECHA_FIN_VALIDEZ")); }
                    else
                    { frmNuevoEMO.dtpFechaFin.Value = DateTime.Now; }

                    frmNuevoEMO.cbxCategoria.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "CATEGORIA").ToString();
                    frmNuevoEMO.txtResultados.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "Resultados").ToString();
                    frmNuevoEMO.txtResultados2.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "Resultados2").ToString();
                    frmNuevoEMO.txtResultados3.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "Resultados3").ToString();

                    frmNuevoEMO.btnCancelar.Enabled = false;
                    frmNuevoEMO.ShowDialog();
                }
            }
            catch { }
        }

        private void txtConductorH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialEMO(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialEMO(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHistorialEMO(); }
        }

        private void btnBuscarH_Click(object sender, EventArgs e) { ListarHistorialEMO(); }

        private void btnExcelH_Click(object sender, EventArgs e)
        {
            if (dtgHistorialEMO.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE DOCUMENTOS DE MÉDICO OCUPACIONAL - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgHistorialEMO.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgHistorialEMO_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmNuevoEMO frmNuevoEMO = new frmNuevoEMO();
                frmNuevoEMO.Formulario = this;
                frmNuevoEMO.txtPersonal.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "EMPLEADO").ToString();
                frmNuevoEMO.Persona = Convert.ToInt32(dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "idPersona"));
                frmNuevoEMO.txtPersonal.ReadOnly = true;
                frmNuevoEMO.txtDNI.Text = dgvDocumentosEMO.GetRowCellValue(dgvDocumentosEMO.FocusedRowHandle, "DNI").ToString();
                frmNuevoEMO.txtArea.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "ÁREA").ToString();
                frmNuevoEMO.txtPuesto.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "PUESTO").ToString();
                frmNuevoEMO.txtRutaLocal.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "DIRECTORIO").ToString();
                frmNuevoEMO.dtpFechaIni.Value = Convert.ToDateTime(dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "FECHA_INICIO_VALIDEZ"));

                string Vacio = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "FECHA_FIN_VALIDEZ").ToString();
                if (Vacio != "")
                { frmNuevoEMO.dtpFechaFin.Value = Convert.ToDateTime(dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "FECHA_FIN_VALIDEZ")); }
                else
                { frmNuevoEMO.dtpFechaFin.Value = DateTime.Now; }

                frmNuevoEMO.cbxCategoria.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "CATEGORIA").ToString();
                frmNuevoEMO.txtResultados.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "Resultados").ToString();
                frmNuevoEMO.txtResultados2.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "Resultados2").ToString();
                frmNuevoEMO.txtResultados3.Text = dgvHistorialEMO.GetRowCellValue(dgvHistorialEMO.FocusedRowHandle, "Resultados3").ToString();

                frmNuevoEMO.btnCancelar.Enabled = false;
                frmNuevoEMO.btnAgregar.Enabled = false;
                frmNuevoEMO.btnBuscar.Enabled = false;
                frmNuevoEMO.btnCerrarLocal.Enabled = false;
                frmNuevoEMO.btnBuscar2.Enabled = false;
                frmNuevoEMO.btnCerrarResultado.Enabled = false;
                frmNuevoEMO.btnBuscar3.Enabled = false;
                frmNuevoEMO.btnCerrarResultado2.Enabled = false;
                frmNuevoEMO.btnBuscar4.Enabled = false;
                frmNuevoEMO.btnCerrarResultado3.Enabled = false;
                frmNuevoEMO.ShowDialog();
            }
            catch { }
        }
    }
}
