using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Entidades;
using Negocio;
using ReportesTranspesa.ServiceGRT_QA;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors;
using DevExpress.Utils.Win;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.Threading;
using ReportesTranspesa.Properties;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmPreviajeTolvasPlantillas : Form
    {
        DataTable dtLista = new DataTable();
        public bool registrarGuia = false;
        string ImpresoraSeleccionada = "";
  
        public FrmPreviajeTolvasPlantillas()
        {
            InitializeComponent();
        }

        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);
        }

        private void FrmPreviajeTolvasPlantillas_Shown(object sender, EventArgs e) { dtpFechaInicio.Focus(); }

        private void FrmPreviajeTolvasPlantillas_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
            VerificarPermisosFormulario();
            ListarPreviajesTolvas();
        }

        private void VerificarPermisosFormulario()
        {
            clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(Utilitario.Instancia.SesionUsuario.usuario); //Trae los permisos del usuario
            DataTable dtPermisosEspeciales = null;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("FrmListaGuiasElectronicas");

            CerrarOPToolStripMenuItem.Enabled = false;
            verViajesToolStripMenuItem.Enabled = false;
            btnNuevaOP.Enabled = false;

            if (dtPermisos.Rows.Count > 0)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                if (dtPermisosEspeciales != null)
                {
                    if (dtPermisosEspeciales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                        {
                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Cerrar Previaje") { CerrarOPToolStripMenuItem.Enabled = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Abrir Previaje") { CerrarOPToolStripMenuItem.Enabled = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Ver Viajes Tolvas") { verViajesToolStripMenuItem.Enabled = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Nuevo Previaje") { btnNuevaOP.Enabled = true; }
                        }
                    }
                }
            }
        }

        private void dgvListaPreviajeTolvasView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO_OPERACIÓN")
            {
                if (e.CellValue.ToString() == "PROGRAMADO")
                { e.Appearance.BackColor = Color.FromArgb(0,213,255); }

                if (e.CellValue.ToString() == "ATENDIDO")
                { e.Appearance.BackColor = Color.FromArgb(129,199,132); }

                if (e.CellValue.ToString() == "ANULADO")
                { e.Appearance.BackColor = Color.FromArgb(240,98,146); }
            }
        }


        private void ListarPreviajesTolvas()
        {
            DataTable dtRendProm = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_CalcularRendProm(txtOperacion.Text);

            if (dtRendProm.Rows.Count > 0) { txtRendProm.Text = dtRendProm.Rows[0]["REND KM/GL"].ToString(); }
            else { txtRendProm.Text = "-"; }
            
            dtLista = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_Listar(txtCodPreviaje.Text, txtCliente.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
            dtgListaPreviajeTolvas.DataSource = dtLista;
            if (dtLista.Rows.Count > 0)
            {
                dgvListaPreviajeTolvasView.Columns["idPreviajeTolvas"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["Anio"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["idCliente"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["idRemitente"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["idDestinatario"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["idPartida"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["idDestino"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["idProducto"].Visible = false;
                dgvListaPreviajeTolvasView.Columns["idRuta"].Visible = false;

                
                dgvListaPreviajeTolvasView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaPreviajeTolvasView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaPreviajeTolvasView.Columns["UltimaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaPreviajeTolvasView.Columns["UltimaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                /*
                dgvListaFallasMecanicasView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[]
                { 
                    new GridColumnSortInfo(dgvListaFallasMecanicasView.Columns["TIPO_FALLA"], DevExpress.Data.ColumnSortOrder.Descending)
                }, 1);
                */

                dgvListaPreviajeTolvasView.BestFitColumns();
                dgvListaPreviajeTolvasView.ExpandAllGroups();
            }
        }

        private void VerTolva (FrmNuevoPreviajeTolvas formulario)
        {
            int idPreviajeTolvas = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "idPreviajeTolvas"));
            int Anio = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "Anio"));
            formulario.entPreviajeTolvas.TipoOperacion = Utilitario.TipoOperacion.Editar;
            formulario.txtOT.Enabled = false;
            DataTable dtListaTolva = new DataTable();
            
            dtListaTolva = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_SeleccionarPreviaje(idPreviajeTolvas, Anio);
            if (dtListaTolva.Rows.Count > 0)
            {
                formulario.entPreviajeTolvas.idPreviajeTolvas = Convert.ToInt32(dtListaTolva.Rows[0]["idPreviajeTolvas"]);
                formulario.entPreviajeTolvas.anio = Convert.ToInt32(dtListaTolva.Rows[0]["Anio"]);
                formulario.txtOT.Text = dtListaTolva.Rows[0]["IdOT"].ToString();
                formulario.entPreviajeTolvas.IdOT = Convert.ToInt32(dtListaTolva.Rows[0]["IdOT"]);
                formulario.txtNroTicket.Text = dtListaTolva.Rows[0]["NroTicket"].ToString();
                formulario.entPreviajeTolvas.NroTicket = dtListaTolva.Rows[0]["NroTicket"].ToString();
                formulario.lblEstado.Text = dtListaTolva.Rows[0]["Estado"].ToString();
                formulario.txtTarifa.Text = dtListaTolva.Rows[0]["Tarifa"].ToString();
                formulario.entPreviajeTolvas.Tarifa = Convert.ToDecimal(dtListaTolva.Rows[0]["Tarifa"].ToString());
                formulario.txtRemitente.Text = dtListaTolva.Rows[0]["Remitente"].ToString();
                formulario.entPreviajeTolvas.Remitente = dtListaTolva.Rows[0]["Remitente"].ToString();
                formulario.entPreviajeTolvas.idRemitente = Convert.ToInt32(dtListaTolva.Rows[0]["idRemitente"]);
                formulario.txtRemitente.Tag = dtListaTolva.Rows[0]["idRemitente"].ToString();
                formulario.txtDireccionPartida.Text = dtListaTolva.Rows[0]["DireccionPartida"].ToString();
                formulario.entPreviajeTolvas.idPartida = Convert.ToInt32(dtListaTolva.Rows[0]["idPartida"].ToString());
                formulario.txtDireccionPartida.Tag = Convert.ToInt32(dtListaTolva.Rows[0]["idPartida"].ToString());
                formulario.entPreviajeTolvas.Estado = dtListaTolva.Rows[0]["Estado"].ToString();

                formulario.txtDireccionDestino.Text = dtListaTolva.Rows[0]["DireccionDestino"].ToString();
                formulario.txtDestinatario.Text = dtListaTolva.Rows[0]["Destinatario"].ToString();
                formulario.entPreviajeTolvas.Destinatario = dtListaTolva.Rows[0]["Destinatario"].ToString();
                formulario.entPreviajeTolvas.idDestinatario = Convert.ToInt32(dtListaTolva.Rows[0]["idDestinatario"]);
                formulario.entPreviajeTolvas.idDestino = Convert.ToInt32(dtListaTolva.Rows[0]["idDestino"].ToString());
                formulario.txtDireccionDestino.Tag = Convert.ToInt32(dtListaTolva.Rows[0]["idDestino"].ToString());
                formulario.txtCliente.Text = dtListaTolva.Rows[0]["Cliente"].ToString();
                formulario.txtCliente.Tag = dtListaTolva.Rows[0]["idCliente"].ToString();
                formulario.entPreviajeTolvas.idClinte = Convert.ToInt32(dtListaTolva.Rows[0]["idCliente"].ToString());
                formulario.entPreviajeTolvas.Cliente = dtListaTolva.Rows[0]["Cliente"].ToString();
                formulario.dtpFechaTraslado.Text = dtListaTolva.Rows[0]["FechaProgramacion"].ToString();
                formulario.txtProducto.Tag = dtListaTolva.Rows[0]["idProducto"].ToString();
                formulario.entPreviajeTolvas.Producto = dtListaTolva.Rows[0]["Producto"].ToString();

                formulario.cbxOperacion.Text = dtListaTolva.Rows[0]["TipoProceso"].ToString();
                formulario.txtMotonave.Text = dtListaTolva.Rows[0]["Motonave"].ToString();
                formulario.txtTonelaje.Text = dtListaTolva.Rows[0]["Tonelaje"].ToString();
                formulario.txtProducto.Text = dtListaTolva.Rows[0]["Producto"].ToString();
                formulario.entPreviajeTolvas.idProducto = Convert.ToInt32(dtListaTolva.Rows[0]["idProducto"].ToString());
                formulario.txtRuta.Tag = dtListaTolva.Rows[0]["idRuta"].ToString();
                formulario.txtRuta.Text = dtListaTolva.Rows[0]["Ruta"].ToString();
                formulario.entPreviajeTolvas.Ruta = dtListaTolva.Rows[0]["Ruta"].ToString();
                formulario.entPreviajeTolvas.idRuta = Convert.ToInt32(dtListaTolva.Rows[0]["idRuta"]);
                formulario.txtUmUso.Text = dtListaTolva.Rows[0]["UMUso"].ToString();
                formulario.txtTiempo.Text = dtListaTolva.Rows[0]["Tiempo"].ToString();
                formulario.txtDistancia.Text = dtListaTolva.Rows[0]["Distancia"].ToString();
                

                for (int i = 0; i < dtListaTolva.Rows.Count; i++)
                {

                    formulario.dgvConductor.Rows.Add(dtListaTolva.Rows[i]["idPlaca"], dtListaTolva.Rows[i]["Placa"], dtListaTolva.Rows[i]["idCarreta"], dtListaTolva.Rows[i]["Carreta"], dtListaTolva.Rows[i]["idConductor"], dtListaTolva.Rows[i]["Conductor"], dtListaTolva.Rows[i]["TarjetaCirculacionTracto"], dtListaTolva.Rows[i]["TarjetaCirculacionCarreta"], dtListaTolva.Rows[i]["Cantidad"]);
                }
            }
            else
            {
                formulario.dgvConductor.DataSource = null;
            }
        }



        private void txtCodPreviaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarPreviajesTolvas();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarPreviajesTolvas();
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarPreviajesTolvas();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarPreviajesTolvas();
            }
        }

        private void dtgListaPreviajeTolvas_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FrmNuevoPreviajeTolvas open = new FrmNuevoPreviajeTolvas();
                open.CargarComboTipo();
                open.EnviarCodigo(Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "idPreviajeTolvas")), Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "Anio")));
                string estado = dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "ESTADO_OPERACIÓN").ToString();
                open.estado = dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "ESTADO_OPERACIÓN").ToString();
                open.registrarGuia = registrarGuia;
                open.btnOT.Enabled = false;
                open.txtRemitente.Enabled = false;
                open.txtDireccionPartida.Enabled = false;
                open.txtDestinatario.Enabled = false;
                open.txtDireccionDestino.Enabled = false;
                

                if (estado == "PROGRAMADO")
                {
                    open.btnGuardar.Visible = false;
                    open.dtpFechaTraslado.Enabled = true;
                    open.btnModificar.Text = "Actualizar";
                    open.quitarToolStripMenuItem.Enabled = true;
                    open.lblEstado.BackColor = Color.FromArgb(0, 213, 255);
                    open.txtDireccionPartida.Enabled = true;
                    open.txtDireccionDestino.Enabled = true;
                    open.cbxOperacion.Enabled = true;
                    open.txtMotonave.Enabled = true;
                    open.btnOT.Enabled =true;
                    open.txtRemitente.Enabled = true;
                    open.txtDestinatario.Enabled = true;
   
                }
                else
                {
                    open.btnModificar.Text = "Actualizar";
                    open.btnGuardar.Visible = false;
                    open.dtpFechaTraslado.Enabled = false;
                    open.asignarViajeToolStripMenuItem.Enabled = true;
                    open.lblEstado.BackColor = Color.LawnGreen;
                    open.txtDireccionPartida.Enabled = false;
                    open.txtDireccionDestino.Enabled = false;
                    open.cbxOperacion.Enabled = false;
                    open.txtMotonave.Enabled = false;
                    open.quitarToolStripMenuItem.Enabled = true;

                    if (estado == "ATENDIDO")
                    {
                        open.lblEstado.BackColor = Color.FromArgb(129, 199, 132);
                    }
                    if (estado == "ANULADO")
                    {
                        open.lblEstado.BackColor = Color.FromArgb(240, 98, 146);
                    }
                }

                VerTolva(open);
                open.Show();
      

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CerrarOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idTolva = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "idPreviajeTolvas"));
            int anio = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "Anio"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
           

            DataTable dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_CerrarOperacion(idTolva, anio, Usuario);
            string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarPreviajesTolvas();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtgListaPreviajeTolvas.Focus();
            }
        }

        private void reabrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idTolva = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "idPreviajeTolvas"));
            int anio = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "Anio"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_CerrarOperacion(idTolva, anio, Usuario);
            string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarPreviajesTolvas();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtgListaPreviajeTolvas.Focus();
            }
        }

        private void dtgListaPreviajeTolvas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
              
                string estado = dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "ESTADO_OPERACIÓN").ToString();

                if (estado == "PROGRAMADO")
                {
                    CerrarOPToolStripMenuItem.Text = "Cerrar Operacion";
                }
                else
                {
                    if (estado == "ATENDIDO")
                    {
                        CerrarOPToolStripMenuItem.Text = "Reabrir Operacion";
                    }
                }
            }
            catch
            {
                CerrarOPToolStripMenuItem.Enabled = false;
            }
        }

        private void lblTituloGuia_Click(object sender, EventArgs e)
        {

        }

        private void verViajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmListaViajesTolvas viajes = new frmListaViajesTolvas();
                viajes.txtNroTicket.Text = dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "NroTicket").ToString();
                viajes.anio = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "Anio"));
                viajes.idPreviajeTolvas = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "idPreviajeTolvas").ToString());
                viajes.Show();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarTolvas_Click(object sender, EventArgs e)
        {
            try { ListarPreviajesTolvas(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNuevaOP_Click(object sender, EventArgs e)
        {
            try
            {
                FrmNuevoPreviajeTolvas open = new FrmNuevoPreviajeTolvas();
                open.CargarComboTipo();
                open.dtpFechaTraslado.Value = DateTime.Now;
                open.btnModificar.Visible = false;
                open.registrarGuia = registrarGuia;
                open.estado = "PENDIENTE";
                open.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListaPlanillas_Click(object sender, EventArgs e)
        {
            frmListaPlanillasTolvas frmListaPlanillasTolvas = new frmListaPlanillasTolvas();
            frmListaPlanillasTolvas.ShowDialog();
        }

        private void btnListaImpresoras_Click(object sender, EventArgs e)
        {
            try
            {
                panel2.Visible = true;
                DataTable dt = new DataTable();
                dt.Columns.Add("Nombre", typeof(String));
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters) { dt.Rows.Add(printer); }

                if (dt.Rows.Count > 0)
                {
                    dgvImpresoras.DataSource = dt;
                    dgvListaPreviajeTolvasView.BestFitColumns();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvImpresoras_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ImpresoraSeleccionada = dgvImpresorasVista.GetRowCellValue(dgvImpresorasVista.FocusedRowHandle, "Nombre").ToString();
                myPrinters.SetDefaultPrinter(ImpresoraSeleccionada);

                MessageBox.Show("Actualizado Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel2.Visible = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrarImpresoras_Click(object sender, EventArgs e) { panel2.Visible = false; }

        private void reporteSumarizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvSumarizado.DataSource = null;
                dgvSumarizadoView.Columns.Clear();
                DataTable dtSumarizado = new DataTable();
                int NroTicket = Convert.ToInt32(dgvListaPreviajeTolvasView.GetRowCellValue(dgvListaPreviajeTolvasView.FocusedRowHandle, "NroTicket"));

                dtSumarizado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_ImportarReporteSumarizado(NroTicket,dtpFechaInicio.Text,dtpFechaFin.Text);
                dgvSumarizado.DataSource = dtSumarizado;
                if (dtSumarizado.Rows.Count > 0)
                {
                    dgvSumarizadoView.Columns["NroTicket"].Visible = false;

                    dgvSumarizadoView.Columns["PESO_DESCARGA"].Summary.Clear();
                    dgvSumarizadoView.Columns["PESO_DESCARGA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PESO_DESCARGA", "{0:N2} TN");
                    dgvSumarizadoView.Columns["PESO_CARGA"].Summary.Clear();
                    dgvSumarizadoView.Columns["PESO_CARGA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PESO_CARGA", "{0:N2} TN");
                    dgvSumarizadoView.Columns["MERMA"].Summary.Clear();
                    dgvSumarizadoView.Columns["MERMA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MERMA", "{0:N2}");
                    dgvSumarizadoView.Columns["TOTAL"].Summary.Clear();
                    dgvSumarizadoView.Columns["TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL", "{0:N2}");
                }

                if (dgvSumarizado.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "REPORTE DE SUMARIZADOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvSumarizado.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error generando el reporte.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnViajesPendientes_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgListaPreviajeTolvas.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "LISTA DE OPERACIONES DE TOLVAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgListaPreviajeTolvas.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                
                /*
                DataTable dtviajespendientes = new DataTable();
                

                dtviajespendientes = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ReporteViajesPendientes_Tolvas(dtpFechaInicio.Text,dtpFechaFin.Text);
           
                reporteviajespendientes.DataSource = dtviajespendientes;
                if (dtviajespendientes.Rows.Count > 0)
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "REPORTE PENDIENTES TOLVAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    reporteviajespendientes.ExportToXlsx(nombre);
                    Process.Start(nombre);

                }
                 */ 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reporteviajespendientesView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "EstadoSunat")
            {
                if (e.CellValue.ToString() == "APROBADO")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

             
            }
            if (e.Column.FieldName == "EstadoGuia")
            {
                if (e.CellValue.ToString() == "REVERSION")
                { e.Appearance.BackColor = Color.FromArgb(240, 98, 146); }
            }

       
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvSumarizado.DataSource = null;
                dgvSumarizadoView.Columns.Clear();
                DataTable dtSumarizado = new DataTable();
                int NroTicket = 0;

                dtSumarizado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_ImportarReporteSumarizado(NroTicket, dtpFechaInicio.Text, dtpFechaFin.Text);
                dgvSumarizado.DataSource = dtSumarizado;
                if (dtSumarizado.Rows.Count > 0)
                {
                    dgvSumarizadoView.Columns["NroTicket"].Visible = false;

                    dgvSumarizadoView.Columns["PESO_DESCARGA"].Summary.Clear();
                    dgvSumarizadoView.Columns["PESO_DESCARGA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PESO_DESCARGA", "{0:N2} TN");
                    dgvSumarizadoView.Columns["PESO_CARGA"].Summary.Clear();
                    dgvSumarizadoView.Columns["PESO_CARGA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PESO_CARGA", "{0:N2} TN");
                    dgvSumarizadoView.Columns["MERMA"].Summary.Clear();
                    dgvSumarizadoView.Columns["MERMA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MERMA", "{0:N2}");
                    dgvSumarizadoView.Columns["TOTAL"].Summary.Clear();
                    dgvSumarizadoView.Columns["TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TOTAL", "{0:N2}");
                }

                if (dgvSumarizado.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "REPORTE DE SUMARIZADOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvSumarizado.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error generando el reporte.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
