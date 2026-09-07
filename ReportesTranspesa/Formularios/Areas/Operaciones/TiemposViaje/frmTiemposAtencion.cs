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
using System.Xml;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Properties;
using Negocio;
using Comun;


namespace ReportesTranspesa.Formularios.Areas.Operaciones.TiemposViaje
{
    public partial class frmTiemposAtencion : Form
    {
        DataTable dtPermisos = new DataTable();

        public frmTiemposAtencion()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void frmTiemposAtencion_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaTiemposViaje");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true)
                    {
                        btnGuardar.Enabled = true;
                        btnQuitarTiempos.Enabled = true;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                        btnQuitarTiempos.Enabled = false;
                    }
                }
            }
            
            BuscarRutas();
            CargarComboOperaciones();
            cbxOperacion.Text = "LINDLEY";
            ListarTiempoAtencion();
        }


        public void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(4, "");
            cbxOperacion.DataSource = dtOperaciones;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void BuscarRutas()
        {
            dgvRutas.DataSource = null;
            dgvRutasView.Columns.Clear();

            DataTable dt = new DataTable();
            dt = clsConsultaBL.Instancia.GetRutasActivas(txtRuta.Text);

            if (dt.Rows.Count > 0)
            {
                dgvRutas.DataSource = dt;
                dgvRutasView.Columns["ID"].Visible = false;
                dgvRutasView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para mostrar.";
                m.ShowDialog();
            }
        }

        public void ListarTiempoAtencion()
        {
            DataTable dtTiempoAtencion = new DataTable();
            dtTiempoAtencion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarTiempoAtencion(txtBuscarDestino.Text);
            dtgTiempoAtencion.DataSource = dtTiempoAtencion;

            if (dtTiempoAtencion.Rows.Count > 0)
            {
                dgvTiempoAtencionVista.Columns["idTiempoAtencion"].Visible = false;

                dgvTiempoAtencionVista.BestFitColumns();
            }


            /*
            dtListaConsolidado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidado(dtpFechaIni.Text, dtpFechaFin.Text, txtVehiculo.Text, txtConductor.Text, txtRuta.Text);
            dtgListaItinerario.DataSource = dtListaConsolidado;
            if (dtListaConsolidado.Rows.Count > 0)
            {
                dgvListaItinerario.Columns["idConsolidado"].Visible = false;
                dgvListaItinerario.Columns["idRuta"].Visible = false;

                dgvListaItinerario.Columns["FECHA_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FECHA_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaItinerario.Columns["FECHA_ESTIMADA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FECHA_ESTIMADA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaItinerario.Columns["FECHA_TERMINO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FECHA_TERMINO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaItinerario.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaItinerario.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dgvListaItinerario.Columns["CONDUCTOR"].Summary.Clear();
                dgvListaItinerario.Columns["CONDUCTOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                dgvListaItinerario.BestFitColumns();
            }
            */ 
        }


        private void txtDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtHorarioLV.Focus(); }
        }

        private void txtHorarioLV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtHorarioS.Focus(); }
        }

        private void txtHorarioS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpTiempoAtencion.Focus(); }
        }

        private void dtpTiempoAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxOperacion.Focus(); }
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { BuscarRutas(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDestino.Clear();
            txtHorarioLV.Clear();
            txtHorarioS.Clear();
            dtpTiempoAtencion.Text = "00:00:00";
            cbxOperacion.Text = "LINDLEY";

            txtRuta.Clear();
            BuscarRutas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int[] filas = dgvRutasView.GetSelectedRows();

            if (txtDestino.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el destino del viaje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDestino.Focus();
                return;
            }

            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    int idRuta = Convert.ToInt32(dgvRutasView.GetRowCellValue(filas[i], "ID"));

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarTiempoAtencion(1, 0, txtDestino.Text, txtHorarioLV.Text, txtHorarioS.Text,
                                                             dtpTiempoAtencion.Value, Convert.ToInt32(cbxOperacion.SelectedValue), idRuta, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA != "0")
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                btnCancelar_Click(sender, e);
                btnBuscar_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una ruta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRuta.Focus();
                return;
            }
        }

        private void dtgTiempoAtencion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtDestino.Text = dgvTiempoAtencionVista.GetRowCellValue(dgvTiempoAtencionVista.FocusedRowHandle, "DESTINO").ToString();
                txtHorarioLV.Text = dgvTiempoAtencionVista.GetRowCellValue(dgvTiempoAtencionVista.FocusedRowHandle, "LUNES_VIERNES").ToString();
                txtHorarioS.Text = dgvTiempoAtencionVista.GetRowCellValue(dgvTiempoAtencionVista.FocusedRowHandle, "SABADO").ToString();
                dtpTiempoAtencion.Text = dgvTiempoAtencionVista.GetRowCellValue(dgvTiempoAtencionVista.FocusedRowHandle, "TIEMPO_ATENCION").ToString();
                cbxOperacion.Text = dgvTiempoAtencionVista.GetRowCellValue(dgvTiempoAtencionVista.FocusedRowHandle, "PROGRAMACION").ToString();
            }
            catch { }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarTiempoAtencion(); }

        private void btnQuitarTiempos_Click(object sender, EventArgs e)
        {
            int idTiempoAtencion;
            int[] filas = dgvTiempoAtencionVista.GetSelectedRows();

            if (filas.Length != 0)
            {
                if (MessageBox.Show("¿Desea quitar estos tiempos del registro?", "QUITAR TIEMPOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        idTiempoAtencion = Convert.ToInt32(dgvTiempoAtencionVista.GetRowCellValue(filas[i], "idTiempoAtencion"));

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;

                        dtRespuesta = dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarTiempoAtencion(2, idTiempoAtencion, "", "", "",
                                                                     DateTime.Now, 1, 0, Utilitario.Instancia.SesionUsuario.usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA != "0")
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    btnBuscar_Click(sender, e);
                }
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgTiempoAtencion.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "TIEMPOS DE ATENCIÓN POR VIAJE - " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgTiempoAtencion.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
