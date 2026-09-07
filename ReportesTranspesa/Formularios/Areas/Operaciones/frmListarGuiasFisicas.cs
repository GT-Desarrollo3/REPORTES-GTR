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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using Comun;
using Negocio;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmListarGuiasFisicas : MetroFramework.Forms.MetroForm
    {
        DataTable dtPermisos = new DataTable();
        DataTable dtListaGuia = new DataTable();
        
        public frmListarGuiasFisicas()
        {
            InitializeComponent();
            cbxSerieGuia.SelectedIndexChanged -= cbxSerieGuia_SelectedIndexChanged;
        }

        private void cbxSerieGuia_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSerie(); }

        private void frmListarGuiasFisicas_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarGuiasFisicas");
            
            CargarComboSerie();
            cbxSerieGuia.Text = "TODAS";
            cbxEstado.Text = "TODAS";
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            ListarGuiasFisicas();
        }


        public void CargarComboSerie()
        {
            DataTable dtSerieGuia = clsOperacionesBL.Instancia.ReportesApp_Listar_SerieGuiasManuales();
            cbxSerieGuia.DataSource = dtSerieGuia;
            cbxSerieGuia.DisplayMember = "SerieGuia";
            cbxSerieGuia.ValueMember = "Empresa";
        }

        public void ListarGuiasFisicas()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaGuia = clsOperacionesBL.Instancia.ReportesApp_Listar_GuiasManuales(dtpFechaIni.Text, dtpFechaFin.Text, cbxSerieGuia.Text,
                                                              txtNumeroGuia.Text, cbxEstado.Text);
                dtgListaGuiasFisicas.DataSource = dtListaGuia;
                if (dtListaGuia.Rows.Count > 0)
                {
                    dgvListaGuiasFisicasVista.Columns["idSolicitud"].Visible = false;
                    
                    dgvListaGuiasFisicasVista.Columns["FECHA_EMISION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaGuiasFisicasVista.Columns["FECHA_EMISION"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvListaGuiasFisicasVista.Columns["FECHA_CREACION_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaGuiasFisicasVista.Columns["FECHA_CREACION_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvListaGuiasFisicasVista.Columns["FECHA_SOLICITUD"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaGuiasFisicasVista.Columns["FECHA_SOLICITUD"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvListaGuiasFisicasVista.Columns["FECHA_ANULA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaGuiasFisicasVista.Columns["FECHA_ANULA"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                    dgvListaGuiasFisicasVista.Columns["NUMERO"].Summary.Clear();
                    dgvListaGuiasFisicasVista.Columns["NUMERO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "NUMERO_GUIA", "Total = {0}");

                    dgvListaGuiasFisicasVista.BestFitColumns();
                }
            }
        }


        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGuiasFisicas(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGuiasFisicas(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarGuiasFisicas(); }

        private void cbxSerieGuia_DropDownClosed(object sender, EventArgs e) { ListarGuiasFisicas(); }

        private void txtNumeroGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGuiasFisicas(); }
        }

        private void dtgListaGuiasTransportista_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string CodViaje = dgvListaGuiasFisicasVista.GetRowCellValue(dgvListaGuiasFisicasVista.FocusedRowHandle, "COD_VIAJE").ToString();
                string Estado = dgvListaGuiasFisicasVista.GetRowCellValue(dgvListaGuiasFisicasVista.FocusedRowHandle, "ESTADO_SOLICITUD").ToString();

                if (CodViaje != "")
                {
                    if (Estado == "")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { tsSolicitarAnulacion.Enabled = true; }
                        tsAnularGuia.Enabled = false;
                    }

                    if (Estado == "PENDIENTE")
                    {
                        tsSolicitarAnulacion.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsAnularGuia.Enabled = true; }
                    }

                    if (Estado == "ANULADA")
                    {
                        tsSolicitarAnulacion.Enabled = false;
                        tsAnularGuia.Enabled = false;
                    }
                }
                else
                {
                    tsSolicitarAnulacion.Enabled = false;
                    tsAnularGuia.Enabled = true;
                }
            }
            catch
            {
                tsSolicitarAnulacion.Enabled = false;
                tsAnularGuia.Enabled = true;
            }
        }

        private void dgvListaGuiasFisicasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();
            if (e.Column.FieldName == "ESTADO_SOLICITUD")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 255); }

                if (Convert.ToString(e.CellValue) == "ANULADA")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarGuiasFisicas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaGuiasFisicas.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Guías Físicas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaGuiasFisicas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsSolicitarAnulacion_Click(object sender, EventArgs e)
        {
            try
            {
                string Serie = Convert.ToString(dgvListaGuiasFisicasVista.GetRowCellValue(dgvListaGuiasFisicasVista.FocusedRowHandle, "SERIE"));
                string Numero = Convert.ToString(dgvListaGuiasFisicasVista.GetRowCellValue(dgvListaGuiasFisicasVista.FocusedRowHandle, "NUMERO"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_GuiasManuales_SolicitarAnularGuias(1, 1, Serie, Numero, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarGuiasFisicas();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("No se pudo generar la solicitud.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsAnularGuia_Click(object sender, EventArgs e)
        {
            try
            {
                int idSolicitud = Convert.ToInt32(dgvListaGuiasFisicasVista.GetRowCellValue(dgvListaGuiasFisicasVista.FocusedRowHandle, "idSolicitud"));
                string Serie = Convert.ToString(dgvListaGuiasFisicasVista.GetRowCellValue(dgvListaGuiasFisicasVista.FocusedRowHandle, "SERIE"));
                string Numero = Convert.ToString(dgvListaGuiasFisicasVista.GetRowCellValue(dgvListaGuiasFisicasVista.FocusedRowHandle, "NUMERO"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (MessageBox.Show("¿Desea anular esta guía?", "ANULAR GUÍA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_GuiasManuales_SolicitarAnularGuias(2, idSolicitud, Serie, Numero, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarGuiasFisicas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("No se pudo anular la solicitud.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
