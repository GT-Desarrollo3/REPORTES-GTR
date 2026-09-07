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
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using System.Drawing.Printing;
using ReportesTranspesa.ServiceGRT_QA;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class frmListaViajesTolvas : Form
    {
        public int idPreviajeTolvas = 0;
        public int anio = 0;
        public int idViaje = 0;
        public int posicionCelda = 0;
        public int posicionFila = 0;
        public DataTable dtPreviajes = null;
        ene_ConsultarComprobanteIndividual respuestaSunat;
        ens_ConsultarComprobanteIndividual consultaIndividual;
        ServicioGuiaRemisionTransportistaClient request;
        public clsGRT entGuiaTransportista = new clsGRT();
        public DataTable dtRespuestaXML_CDR;
        public Boolean esReversion = false; 
        ene_ConsultarXML consultarXML_CDR;
        public string CarpetaAlacenamientoLogErrores = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\LogErrores\";
        public string CarpetaLogSoapError = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\SOAP\";
        string cadenaError = string.Empty;
        bool conError = false;
        public string CDR_Respuesta_Descripcion;
        string tipoFecha = string.Empty;

        public frmListaViajesTolvas()
        {
            InitializeComponent();
        }

        private void frmListaViajesTolvas_Load(object sender, EventArgs e)
        {
            btnBuscar.PerformClick();
            VerificarPermisosFormulario();
        }


        private void ListarViajesTolvas()
        {
            dtgvData.DataSource = null;
            dgvViajesGuia.Columns.Clear();

            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarViajesTolvas(txtNroTicket.Text, idPreviajeTolvas, anio, txtPlaca.Text, txtConductor.Text, Convert.ToInt32(chkAnulados.Checked));
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dgvViajesGuia.Columns["idPreviajeTolvas"].Visible = false;
                dgvViajesGuia.Columns["Anio"].Visible = false;
                dgvViajesGuia.Columns["TipoGuia"].Visible = false;
                dgvViajesGuia.Columns["idCliente"].Visible = false;
                dgvViajesGuia.Columns["idGuiaElectronica"].Visible = false;
                dgvViajesGuia.Columns["idViaje"].Visible = false;
                dgvViajesGuia.Columns["idConductor"].Visible = false;
                dgvViajesGuia.Columns["idUnidad"].Visible = false;
                dgvViajesGuia.Columns["idCarreta"].Visible = false;
                dgvViajesGuia.Columns["idRuta"].Visible = false;
                dgvViajesGuia.Columns["idPartida"].Visible = false;
                dgvViajesGuia.Columns["idDestino"].Visible = false;
                dgvViajesGuia.Columns["idProducto"].Visible = false;
                dgvViajesGuia.Columns["NombresConductor"].Visible = false;
                dgvViajesGuia.Columns["ApellidosConductor"].Visible = false;
                dgvViajesGuia.Columns["idTipoProgramacion"].Visible = false;
                dgvViajesGuia.Columns["CodProgramaProceso"].Visible = true;

                dgvViajesGuia.Columns["LineaOT"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["idOT"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Cliente"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["NroTicket"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Viaje"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["FechaViaje"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["TipoGuia"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Serie"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Numero"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["GuiaRemision"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["IndTercero"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Tracto"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Carreta"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Ruta"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Producto"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["UMBase"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Distancia"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Tiempo"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Peso"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Merma"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Conductor"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["Observacion"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["OrdenServicio"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["UsuarioRegistraPeso"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["FechaRegistraPeso"].OptionsColumn.AllowEdit = false;
                dgvViajesGuia.Columns["EstadoViaje"].OptionsColumn.AllowEdit = false;

                GridView gridView = dtgvData.FocusedView as GridView;

                dgvViajesGuia.Columns["EstadoSunat"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "EstadoSunat", "Cantidad = {0}");
                dgvViajesGuia.Columns["Cliente"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PesoCliente", "Peso Cliente = {0}");

                decimal PesoCarga, Tonelaje;

                dgvViajesGuia.Columns["Peso"].Summary.Clear();
                dgvViajesGuia.Columns["Peso"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Peso", "Peso Carga = {0:N2}");
                dgvViajesGuia.Columns["Tonelaje"].Summary.Clear();
                dgvViajesGuia.Columns["Tonelaje"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "Tonelaje", "Tonelaje = {0:N2}");

                PesoCarga = Convert.ToDecimal(dgvViajesGuia.Columns["Peso"].SummaryText.Substring(13));
                Tonelaje = Convert.ToDecimal(dgvViajesGuia.Columns["Tonelaje"].SummaryText.Substring(11));

                dgvViajesGuia.Columns["Tonelaje"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "Tonelaje", "Total = " + Convert.ToString(PesoCarga - Tonelaje));

                dgvViajesGuia.BestFitColumns();
            }
            else { dtgvData.DataSource = null; }
        }

        private void VerificarPermisosFormulario()
        {
            clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(Utilitario.Instancia.SesionUsuario.usuario); //Trae los permisos del usuario
            DataTable dtPermisosEspeciales = null;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("FrmListaGuiasElectronicas");

            btnGenerarViajes1.Enabled = false;

            if (dtPermisos.Rows.Count > 0)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "") { dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                if (dtPermisosEspeciales != null)
                {
                    if (dtPermisosEspeciales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                        {
                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Generar Viaje Tolvas") { btnGenerarViajes1.Enabled = true; }
                        }
                    }
                }
            }
        }


        private void anularViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("El viaje se Anulará, ¿Desea Continuar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                if (dgvViajesGuia.GetFocusedRowCellValue("Viaje").ToString().Length == 0)
                {
                    MessageBox.Show("Viaje aun no ha sido creado.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                idViaje = Convert.ToInt32(dgvViajesGuia.GetFocusedRowCellValue("idViaje"));
                idPreviajeTolvas = Convert.ToInt32(dgvViajesGuia.GetFocusedRowCellValue("idPreviajeTolvas"));
                anio = Convert.ToInt32(dgvViajesGuia.GetFocusedRowCellValue("Anio"));

                String Respuesta = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Motivo", "Motivo Anulacion");

                if (Respuesta.Length > 0)
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_AnularViajeTolvas(idPreviajeTolvas,anio,idViaje,Respuesta ))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBuscar.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

          

            }
            catch (Exception ex)
            {  
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void dgvViajesGuia_HiddenEditor(object sender, EventArgs e)
        {
            if (dgvViajesGuia.Columns[dgvViajesGuia.FocusedColumn.AbsoluteIndex].FieldName == "PesoDescarga" )
            {
                if (dgvViajesGuia.GetFocusedRowCellValue("idViaje").ToString() == "0" || dgvViajesGuia.GetFocusedRowCellValue("idViaje").ToString() == "")
                {
                    int idViaje = Convert.ToInt32(dgvViajesGuia.GetFocusedRowCellValue("idViaje"));
                    posicionCelda = dgvViajesGuia.FocusedColumn.AbsoluteIndex;
                    posicionFila = dgvViajesGuia.FocusedRowHandle;
                    
                    if (clsOperacionesBL.Instancia.ReportesApp_RegistrarPesoNroTicketClienteTolvas("PESO", dgvViajesGuia.GetFocusedRowCellValue("PesoDescarga").ToString(), "", idViaje, dgvViajesGuia.GetFocusedRowCellValue("Serie").ToString(), dgvViajesGuia.GetFocusedRowCellValue("Numero").ToString()))
                    {
                        decimal merma =  Convert.ToDecimal(dgvViajesGuia.GetFocusedRowCellValue("Peso").ToString()) - Convert.ToDecimal(dgvViajesGuia.GetFocusedRowCellValue("PesoDescarga").ToString());
                        dgvViajesGuia.SetFocusedRowCellValue("Merma", merma);
                        //btnBuscar.PerformClick();
                        //dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        //dgvViajesGuia.FocusedRowHandle = posicionFila;
                        //dgvViajesGuia.TopRowIndex = posicionFila;
                        //dgvViajesGuia.EndDataUpdate();
                    }
                    else
                    {
                        btnBuscar.PerformClick();
                        //dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        //dgvViajesGuia.FocusedRowHandle = posicionFila;
                        //dgvViajesGuia.EndDataUpdate();  
                    }

                   
                }
                else
                {



                    MessageBox.Show("Codigo de Viaje ya fue generado, no es posible modificar Peso Cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnBuscar.PerformClick();
                    return;
                }
            }


            if (dgvViajesGuia.Columns[dgvViajesGuia.FocusedColumn.AbsoluteIndex].FieldName == "PesoCliente")
            {
                if (dgvViajesGuia.GetFocusedRowCellValue("idViaje").ToString() == "0" || dgvViajesGuia.GetFocusedRowCellValue("idViaje").ToString() == "")
                {
                    int idViaje = Convert.ToInt32(dgvViajesGuia.GetFocusedRowCellValue("idViaje"));
                    posicionCelda = dgvViajesGuia.FocusedColumn.AbsoluteIndex;
                    posicionFila = dgvViajesGuia.FocusedRowHandle;

                    if (clsOperacionesBL.Instancia.ReportesApp_RegistrarPesoNroTicketClienteTolvas("PesoCliente", dgvViajesGuia.GetFocusedRowCellValue("PesoCliente").ToString(), "", idViaje, dgvViajesGuia.GetFocusedRowCellValue("Serie").ToString(), dgvViajesGuia.GetFocusedRowCellValue("Numero").ToString()))
                    {
                        //decimal merma = Convert.ToDecimal(dgvViajesGuia.GetFocusedRowCellValue("Peso").ToString()) - Convert.ToDecimal(dgvViajesGuia.GetFocusedRowCellValue("PesoCliente").ToString());
                        //dgvViajesGuia.SetFocusedRowCellValue("Merma", merma);
                        //btnBuscar.PerformClick();
                        //dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        //dgvViajesGuia.FocusedRowHandle = posicionFila;
                        //dgvViajesGuia.TopRowIndex = posicionFila;
                        //dgvViajesGuia.EndDataUpdate();
                    }
                    else
                    {
                        btnBuscar.PerformClick();
                        //dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        //dgvViajesGuia.FocusedRowHandle = posicionFila;
                        //dgvViajesGuia.EndDataUpdate();  
                    }


                }
                else
                {



                    MessageBox.Show("Codigo de Viaje ya fue generado, no es posible modificar Peso Cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnBuscar.PerformClick();
                    return;
                }
            }



            if (dgvViajesGuia.FocusedColumn.AbsoluteIndex > -1)
            {
                if (dgvViajesGuia.Columns[dgvViajesGuia.FocusedColumn.AbsoluteIndex].FieldName == "NroTicketPesajeCliente")
                {

                    int idViaje = Convert.ToInt32(dgvViajesGuia.GetFocusedRowCellValue("idViaje"));
                    posicionCelda = dgvViajesGuia.FocusedColumn.AbsoluteIndex;
                    posicionFila = dgvViajesGuia.FocusedRowHandle;

                    if (clsOperacionesBL.Instancia.ReportesApp_RegistrarPesoNroTicketClienteTolvas("TICKET", "", dgvViajesGuia.GetFocusedRowCellValue("NroTicketPesajeCliente").ToString(), idViaje, dgvViajesGuia.GetFocusedRowCellValue("Serie").ToString(), dgvViajesGuia.GetFocusedRowCellValue("Numero").ToString()))
                    {
                        btnBuscar.PerformClick();
                        dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        dgvViajesGuia.FocusedRowHandle = posicionFila;
                    }
                    else
                    {
                        btnBuscar.PerformClick();
                        dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        dgvViajesGuia.FocusedRowHandle = posicionFila;
                    }
                }
            }


            if (dgvViajesGuia.FocusedColumn.AbsoluteIndex > -1)
            {
                if (dgvViajesGuia.Columns[dgvViajesGuia.FocusedColumn.AbsoluteIndex].FieldName == "CodProgramaProceso")
                {
                    int idViaje = Convert.ToInt32(dgvViajesGuia.GetFocusedRowCellValue("idViaje"));
                    posicionCelda = dgvViajesGuia.FocusedColumn.AbsoluteIndex;
                    posicionFila = dgvViajesGuia.FocusedRowHandle;


                    if (clsOperacionesBL.Instancia.ReportesApp_RegistrarPesoNroTicketClienteTolvas("CodProgramaTolva", "", dgvViajesGuia.GetFocusedRowCellValue("CodProgramaProceso").ToString(), idViaje, dgvViajesGuia.GetFocusedRowCellValue("Serie").ToString(), dgvViajesGuia.GetFocusedRowCellValue("Numero").ToString()))
                    {
                        btnBuscar.PerformClick();
                        dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        dgvViajesGuia.FocusedRowHandle = posicionFila;
                    }
                    else
                    {
                        btnBuscar.PerformClick();
                        dgvViajesGuia.FocusedColumn.ColumnHandle = posicionCelda;
                        dgvViajesGuia.FocusedRowHandle = posicionFila;
                    }
                }
            }
        }


        private void verGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmListaGuiasElectronicas listarGuia = new FrmListaGuiasElectronicas();
                //listarGuia.txtviaje.Text = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Viaje"));
                listarGuia.txtNumeroFiltro.Text = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Numero"));
                //listarGuia.cbxSerieFiltro.SelectedText = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Serie"));
                listarGuia.dtpFechaInicio.Text = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("FechaViaje"));
                listarGuia.nroSerie = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Serie"));
          
                listarGuia.TipoOperacion = Utilitario.TipoOperacion.Editar;
                listarGuia.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void actualizarEstadoSUNATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvViajesGuia.RowCount > 0 && dgvViajesGuia.SelectedRowsCount > 0)
                {
                    int[] filas = dgvViajesGuia.GetSelectedRows();
                    this.Cursor = Cursors.WaitCursor;

                    for (int i = 0; i < filas.Length; i++)
                    {
                        if (dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "EstadoSunat").ToString() != "ANULADO" && dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "EstadoSunat").ToString()  != "APROBADO")
                        {
                            string Serie = dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Serie").ToString();
                            int Numero = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Numero"));
                            entGuiaTransportista.compania = "10000000";
                            entGuiaTransportista.idcliente = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idCliente"));
                            entGuiaTransportista.idOT = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idOT"));
                            entGuiaTransportista.TipoGuia = Convert.ToString(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "TipoGuia"));
                            entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idGuiaElectronica"));
                            entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "EstadoGuia").ToString();

                            ActualizarEstadoSunatTransportista(Serie, Numero);
                        }
                    }

                    if (conError)
                    {
                        MessageBox.Show("No todas las guias se actualizaron", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ListarViajesTolvas();
                    }
                    else { ListarViajesTolvas(); }

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               this.Cursor = Cursors.Default;
            }
        }


        private void ConsultarGuiaIndividualTransportista(string NumroDocumentoIdentidad, string Serie, int Numero)
        {
            //INSTANCIO LA CLASE NECESARIA PARA INDICARLE A SUNAT QUE GUIA QUIERO CONSULTAR (SERIE , NUEMRO)
            respuestaSunat = new ene_ConsultarComprobanteIndividual();
            respuestaSunat.at_NumeroDocumentoIdentidad = NumroDocumentoIdentidad;
            respuestaSunat.at_Serie = Serie;
            respuestaSunat.at_Numero = Numero;

            // INSTANCIO LA CLASE QUE ALMACENARÁ LA RESPUESTA DE SUNAT
            consultaIndividual = new ens_ConsultarComprobanteIndividual();
            request = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
            consultaIndividual = request.ConsultaIndividualGRT(respuestaSunat); // AQUI OBTENGO LA RESPUESTA SUNAT "consultaIndividual"

            if (consultaIndividual.ent_InformacionComprobante != null)
            {
                entGuiaTransportista.entGRT_Respuesta_FechaGeneracion = consultaIndividual.ent_InformacionComprobante.at_FechaGeneracion;
                entGuiaTransportista.entGRT_Respuesta_NivelResultado = consultaIndividual.at_NivelResultado;
                entGuiaTransportista.entGRT_Respuesta_Guardar_Sunat = consultaIndividual.at_MensajeResultado;
                entGuiaTransportista.entGRT_Respuesta_FechaTransmision = consultaIndividual.ent_InformacionComprobante.at_FechaTransmision;
            }
            else
            {
                conError = true;
                MessageBox.Show(consultaIndividual.at_MensajeResultado.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }




        }

        private void ActualizarEstadoSunatTransportista(string Serie, int Numero)
        {


            //DataTable GuiaSistema = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConsultarEstadoGuiaReporteador(Serie, Numero);
            //if (GuiaSistema.Rows.Count > 0)
           

                    entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = "20439331918";
                    entGuiaTransportista.entGRT_Generales_Serie_M = Serie;
                    entGuiaTransportista.entGRT_Generales_Numero_M = Numero;

                    ConsultarGuiaIndividualTransportista(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);

                    if (consultaIndividual.ent_InformacionComprobante != null && consultaIndividual.ent_InformacionComprobante.l_respuestas.Count > 0)
                    {

                       

                        if (consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "2" || consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "1")
                        {
                            entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "APROBADO";
                            entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "ACEPTADO";
                        }
                        else
                        {
                            entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "RECHAZADO";
                        }

                        CargarArchivoXML();
                        CargarRespuestaSunatCDR();

              
                            if (consultaIndividual.ent_InformacionComprobante != null)
                            {
                                entGuiaTransportista.entGRT_Respuesta_MensajeResultado = CDR_Respuesta_Descripcion;//consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion;
                                entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta;
                            }
                            else
                            {
                                entGuiaTransportista.entGRT_Respuesta_MensajeResultado = consultaIndividual.at_MensajeResultado;
                                entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = "03";
                            }
                         
                        entGuiaTransportista.entGRT_Respuesta_FechaGeneracion = consultaIndividual.ent_InformacionComprobante.at_FechaGeneracion;
                        entGuiaTransportista.entGRT_Respuesta_FechaOtorgamiento = consultaIndividual.ent_InformacionComprobante.at_FechaOtorgamiento;
                        entGuiaTransportista.entGRT_Respuesta_FechaTransmision = consultaIndividual.ent_InformacionComprobante.at_FechaTransmision;
                        entGuiaTransportista.entGRT_Respuesta_FechaReversion = consultaIndividual.ent_InformacionComprobante.at_FechaReversion;
                        entGuiaTransportista.entGRT_Respuesta_CodigoHash = consultaIndividual.ent_InformacionComprobante.at_CodigoHash;
                        entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";

                    
                        if (clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaTransportista))
                        {

                        }
                        else
                        {
                            conError = true;
                            cadenaError = cadenaError + Utilitario.Instancia.Advertencia + "\n";
                        }
                    }
                    else
                    {
                        conError = true;
                        cadenaError =  "La guia "+ Serie+"-"+Numero +" aun no tiene respuesta de Sunat";
                    }




   
        }


        private void CargarRespuestaSunatCDR()
        {


            if (consultaIndividual.at_NivelResultado == 1) // UNO SIGNIFICA QUE SI ENCONTRO LA RESPUESTA DEL CDR
            {
                ServiceGRT_QA.ens_ConsultarXML Respuesta_XML_CDR = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, Convert.ToInt32(consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_NroRespuesta));
                String xmlCDR = "";
                CDR_Respuesta_Descripcion = "";
                if (Respuesta_XML_CDR.at_MensajeResultado != "No hay XML para consultar")
                {
                    dtRespuestaXML_CDR = Utilitario.Instancia.ObtenerNodoXML(Encoding.UTF8.GetString((Respuesta_XML_CDR.ent_ResultadoXML.at_XML)));
                    CDR_Respuesta_Descripcion = Utilitario.Instancia.ObtenerNodoXML_RespuestaCDR(Encoding.UTF8.GetString((Respuesta_XML_CDR.ent_ResultadoXML.at_XML)));

                    if (dtRespuestaXML_CDR != null)
                    {
                        if (dtRespuestaXML_CDR.Rows.Count > 0)
                        {
                            xmlCDR = Utilitario.Instancia.DatatableToXml(dtRespuestaXML_CDR);
                            entGuiaTransportista.entGRT_Respuesta_URL_GuiaSunat = dtRespuestaXML_CDR.Rows[0]["Descripcion"].ToString();
                        }
                        else
                        {
                            xmlCDR = "ERROR AL TARER ALGUN DATO: " + " METODO= Utilitario.Instancia.ObtenerNodoXML(Encoding.UTF8.GetString((Respuesta_XML_CDR.ent_ResultadoXML.at_XML))";
                        }
                    }

                }
                else
                {
                    xmlCDR = "No hay XML para consultar";
                }



                if (xmlCDR.Length > 0)
                {
                    /*using (StreamWriter log = new StreamWriter(CarpetaAlacenamientoLogErrores + "CDR=Guia_Serie_" + entGuiaTransportista.entGRT_Generales_Serie_M + "_Numero_" + entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt"))
                    {
                        log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + "Mensaje: " + consultaIndividual.at_MensajeResultado + " Detalle: " + xmlCDR);
                    }*/

                    entGuiaTransportista.entGRT_Respuesta_Xml_CDR = Encoding.UTF8.GetString(Respuesta_XML_CDR.ent_ResultadoXML.at_XML);
                    entGuiaTransportista.entGRT_Respuesta_Fecha_CDR = Respuesta_XML_CDR.ent_ResultadoXML.at_FechaXML;

                }
            }

        }


        private void CargarArchivoXML()
        {
            ens_ConsultarXML Archivo_XML = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, 0);

            while (Archivo_XML.at_NivelResultado == 0)
            {
                Archivo_XML = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, 0);
            }
            entGuiaTransportista.entGRT_Respuesta_XML_Archivo = Encoding.UTF8.GetString(Archivo_XML.ent_ResultadoXML.at_XML);

        }

        private ens_ConsultarXML CargarGuiaEnXML(string NumroDocumentoIdentidad, string Serie, int Numero, int NumeroRespuesta = 0)
        {

            consultarXML_CDR = new ene_ConsultarXML();
            consultarXML_CDR.ent_ComprobanteConsultarXML = new en_ComprobanteConsultarXML();
            consultarXML_CDR.at_NumeroDocumentoIdentidad = NumroDocumentoIdentidad;
            consultarXML_CDR.ent_ComprobanteConsultarXML.at_Serie = Serie;
            consultarXML_CDR.ent_ComprobanteConsultarXML.at_Numero = Numero;
            consultarXML_CDR.ent_ComprobanteConsultarXML.at_NumeroRespuesta = NumeroRespuesta;
            ens_ConsultarXML Respuesta_XML_CDR = new ens_ConsultarXML();
            Respuesta_XML_CDR = request.ConsultarXMLGRT(consultarXML_CDR);

            return Respuesta_XML_CDR;
        }

        private void dgvViajesGuia_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "EstadoGuia")
            {
                if (e.CellValue.ToString() == "REVERSION")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 155, 155);
                }
                if (e.CellValue.ToString() == "ACEPTADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(252, 254, 149);
                }
            }

            if (e.Column.FieldName == "EstadoSunat")
            {
                if (e.CellValue.ToString() == "APROBADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(167, 250, 140); //Color.Lime; /
                }

                if (e.CellValue.ToString() == "ANULADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(240, 98, 146);
                }
            }

            if (e.Column.FieldName == "PesoCliente")
            {

                e.Appearance.BackColor = Color.FromArgb(184, 228, 254);
                
            }
        }

        private void btnBuscar1_Click(object sender, EventArgs e)
        {
            dtPreviajes = new DataTable();
            dtPreviajes.Columns.Add("idPreviajeTolvas", typeof(int));
            dtPreviajes.Columns.Add("Anio", typeof(int));
            dtPreviajes.Columns.Add("idOT", typeof(int));
            dtPreviajes.Columns.Add("LineaOT", typeof(int));
            dtPreviajes.Columns.Add("NroTicket", typeof(int));
            dtPreviajes.Columns.Add("FechaViaje", typeof(String));
            dtPreviajes.Columns.Add("TipoGuia", typeof(String));
            dtPreviajes.Columns.Add("Serie", typeof(String));
            dtPreviajes.Columns.Add("Numero", typeof(String));
            dtPreviajes.Columns.Add("TipoGuiaRem", typeof(String));
            dtPreviajes.Columns.Add("Remitente", typeof(String));
            dtPreviajes.Columns.Add("IndTercero", typeof(String));
            dtPreviajes.Columns.Add("idConductor", typeof(int));
            dtPreviajes.Columns.Add("idPlaca", typeof(int));
            dtPreviajes.Columns.Add("idCarreta", typeof(int));
            dtPreviajes.Columns.Add("idPartida", typeof(int));
            dtPreviajes.Columns.Add("idDestino", typeof(int));
            dtPreviajes.Columns.Add("idTipoProgramacion", typeof(int));
            dtPreviajes.Columns.Add("idRuta", typeof(int));
            dtPreviajes.Columns.Add("idProducto", typeof(int));
            dtPreviajes.Columns.Add("PesoCliente", typeof(float));
            dtPreviajes.Columns.Add("PesoDescarga", typeof(float));
            dtPreviajes.Columns.Add("NroTicketPesajeCliente", typeof(String));
            dtPreviajes.Columns.Add("Observacion", typeof(String));
            ListarViajesTolvas();
        }

        private void btnGenerarViajes1_Click(object sender, EventArgs e)
        {
            try
            {
                int[] filas = dgvViajesGuia.GetSelectedRows();


                if (filas.Count() > 0)
                {
                    for (int i = 0; i < filas.Count(); i++)
                    {
                        string x = dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "EstadoSunat").ToString();
                        if (dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "EstadoSunat").ToString() != "ANULADO" && 
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "EstadoSunat").ToString() != "PENDIENTE")
                        {
                            dtPreviajes.Rows.Add(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idPreviajeTolvas"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Anio"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idOT"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "LineaOT"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "NroTicket"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "FechaViaje"),
                            "T",
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Serie"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Numero"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "TipoRem"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "GuiaRemision"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "IndTercero"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idConductor"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idUnidad"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idCarreta"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idPartida"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idDestino"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idTipoProgramacion"), // id TOLVAS
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idRuta"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idProducto"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "PesoCliente").ToString() == "" ? 0.00 : dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "PesoCliente"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "PesoDescarga").ToString() == "" ? 0.00 : dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "PesoDescarga"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "NroTicketPesajeCliente"),
                            dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Observacion")
                            );
                        }

                    }

                    IEnumerable<DataRow> ieRegistro = from fila in dtPreviajes.AsEnumerable()
                                                      where fila.Field<float>("PesoCliente") == 0.00
                                                      select fila;
                    if (ieRegistro.Count() > 0)
                    {
                        MessageBox.Show("Existen Pesos de Cliente en blanco o con cantidad 0, verificar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    IEnumerable<DataRow> ieRegistroDescarga = from fila in dtPreviajes.AsEnumerable()
                                                      where fila.Field<float>("PesoDescarga") == 0.00
                                                      select fila;
                    if (ieRegistroDescarga.Count() > 0)
                    {
                        MessageBox.Show("Existen Pesos de Cliente en blanco o con cantidad 0, verificar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (clsOperacionesBL.Instancia.ReportesApp_GenerarViajesTolvas_GuiasElectronicas(Utilitario.Instancia.DatatableToXml(dtPreviajes)))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBuscar.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnBuscar.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                int filas = dgvViajesGuia.DataRowCount;
                
                if (filas > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    for (int i = 0; i < filas; i++)
                    {
                        int rowHandle = dgvViajesGuia.GetVisibleRowHandle(i);

                        if (rowHandle < 0) continue;

                        if (dgvViajesGuia.GetRowCellValue(rowHandle, "EstadoSunat").ToString() != "ANULADO" && dgvViajesGuia.GetRowCellValue(rowHandle, "EstadoSunat").ToString() != "APROBADO")
                        {
                            string Serie = dgvViajesGuia.GetRowCellValue(rowHandle, "Serie").ToString();
                            int Numero = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(rowHandle, "Numero"));
                            entGuiaTransportista.compania = "10000000";
                            entGuiaTransportista.idcliente = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(rowHandle, "idCliente"));
                            entGuiaTransportista.idOT = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(rowHandle, "idOT"));
                            entGuiaTransportista.TipoGuia = Convert.ToString(dgvViajesGuia.GetRowCellValue(rowHandle, "TipoGuia"));
                            entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(rowHandle, "idGuiaElectronica"));
                            entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = dgvViajesGuia.GetRowCellValue(rowHandle, "EstadoGuia").ToString();

                            ActualizarEstadoSunatTransportista(Serie, Numero);
                        }
                    }
                }

                if (conError)
                {
                    MessageBox.Show("No todas las guías se actualizaron", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListarViajesTolvas();
                }
                else { ListarViajesTolvas(); }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                FrmImportarGuias import = new FrmImportarGuias();
                import.txtModulo.Visible = true;
                import.esElectronico = true;
                import.idProgramacion = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(0, "idPreviajeTolvas"));
                import.AnioProgramacion = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(0, "Anio"));

                if (import.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    btnBuscar.PerformClick();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvViajesGuia.RowCount == 0)
                {
                    MessageBox.Show("No se encontraron Datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte viajes " + dgvViajesGuia.GetRowCellValue(0, "NroTicket").ToString() + " " + Utilitario.Instancia.SesionUsuario.usuario +" " +DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData.ExportToXlsx(nombre);
                }

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizarFecha_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtGuias = new DataTable();
                dtGuias.Columns.Add("Serie", typeof(String));  
                dtGuias.Columns.Add("Numero", typeof(String));
                dtGuias.Columns.Add("idViaje", typeof(String));  
                string xmlGuias = "";

             

                  int[] filas = dgvViajesGuia.GetSelectedRows();


                if (filas.Count() > 0)
                {
                    for (int i = 0; i < filas.Count(); i++)
                    {
                        if (tipoFecha == "Fecha Viaje")
                        {
                            if (dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Viaje").ToString() != "")
                            {
                                dtGuias.Rows.Add(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Serie").ToString(),
                                                 dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Numero").ToString(),
                                                 dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idViaje").ToString() == "" ? "0" : dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idViaje").ToString());

                            }
                        }
                        else
                        {
                            dtGuias.Rows.Add(dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Serie").ToString(),
                                                 dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Numero").ToString(),
                                                 dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idViaje").ToString() == "" ? "0" : dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "idViaje").ToString());
                        }
        

                    }
                }

                if (dtGuias.Rows.Count > 0)
                {
                    xmlGuias = Utilitario.Instancia.DatatableToXml(dtGuias);
                }
                else
                {
                    MessageBox.Show("No cargaron datos en el XML", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                

                if (xmlGuias.Length > 0)
                {
                   
                 
                        if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ActualizarFechaGuias(dtpFechaModifica.Text, xmlGuias,tipoFecha))
                        {
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                      
                }
                else
                {
                    MessageBox.Show("Usted no a seleccionado ninguna guia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dtpFechaModifica.MinDate = DateTime.Now;
            menuFecha.Visible = true;
            tipoFecha = "Fecha Guia";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            menuFecha.Visible = false;
        }

        private void envioMasivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

      
                 entProductos enProductoTolvas = new entProductos();
                 //entidad guia transportista
                 clsGRT entGuiaTransportista = new clsGRT();
                 entConductor entNuevoConductor = new entConductor();
   
                 Boolean RechazadoSunat = false; // atributo false cuando retorne un codigo mayor a 2


                // INSTANCIAS DE REGISTRO DE GUIA
                ServicioGuiaRemisionTransportistaClient request;
                ens_Respuesta respuesta = null;
                ene_ConsultarComprobanteIndividual respuestaSunat;
                ens_ConsultarComprobanteIndividual consultaIndividual;
                ene_ConsultarXML consultarXML_CDR;
                ens_ConsultarXML responseXML;
                ens_ConsultarEstadoGR consultarOtorgamiento;
                en_ResultadoEstadoComprobanteGR respuestaCorreo;
                ens_ResultadoRI resultadoRI;

                ene_GuiaRemisionTransportista registrar = new ene_GuiaRemisionTransportista();



                int[] filas = dgvViajesGuia.GetSelectedRows();


                if (filas.Count() > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    for (int i = 0; i < filas.Count(); i++)
                    {
                        
                        string serie = dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Serie").ToString();
                        string numero = dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "Numero").ToString();
                        DataTable dtFechaHora = clsOperacionesBL.Instancia.ObtenerFechaHoraServidorGuiaElectronica();
                   

                        DataTable DatosGuia = clsOperacionesBL.Instancia.ReportesApp_Operaciones_BuscarGuiaIndividual("T", serie, numero);

                        string[] UbigeoDireccionPartida = DatosGuia.Rows[0]["NombreUbigeoPartida"].ToString().Split(',');
                        string[] UbigeoDireccionDestino = DatosGuia.Rows[0]["NombreUbigeoLlegada"].ToString().Split(',');

                        if (DatosGuia.Rows.Count > 0)
                        {

                            registrar.at_ControlOtorgamiento = true;
                            registrar.ent_TransportistaGRT = new en_TransportistaGRT();
                            registrar.ent_TransportistaGRT.at_NumeroDocumentoIdentidad = DatosGuia.Rows[0]["NumeroDocIdentidad_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.at_RazonSocial = DatosGuia.Rows[0]["RazonSocial_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.at_NumeroMTC = DatosGuia.Rows[0]["NumeroMTC_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.at_Telefono = DatosGuia.Rows[0]["Telefono_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.at_CorreoContacto = DatosGuia.Rows[0]["Correo_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.at_SitioWeb = DatosGuia.Rows[0]["SitioWeb_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT = new en_DireccionFiscalTransportistaGRT();
                            registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Ubigeo = DatosGuia.Rows[0]["Ubigeo_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_DireccionDetallada = DatosGuia.Rows[0]["DireccionDetallada_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Provincia = DatosGuia.Rows[0]["Provincia_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Departamento = DatosGuia.Rows[0]["Departamento_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Distrito = DatosGuia.Rows[0]["Distrito_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_CodigoPais = DatosGuia.Rows[0]["CodigoPais_Emisor"].ToString();
                            registrar.ent_TransportistaGRT.ent_CorreoGRT = new en_CorreoGRT();
                            registrar.ent_TransportistaGRT.ent_CorreoGRT.at_CorreoPrincipal = DatosGuia.Rows[0]["CorreoPrincipal_Cliente"].ToString();
                            registrar.ent_RemitenteGRT = new en_RemitenteGRT();
                            registrar.ent_RemitenteGRT.at_NumeroDocumentoIdentidad = DatosGuia.Rows[0]["NumeroDocIdentidad_Rem"].ToString().Trim();
                            registrar.ent_RemitenteGRT.at_TipoDocumentoIdentidad = DatosGuia.Rows[0]["TipoDocIdentidad_Rem"].ToString().Trim();
                            registrar.ent_RemitenteGRT.at_RazonSocial = DatosGuia.Rows[0]["RazonSocial_Rem"].ToString().Trim();
                            registrar.ent_DestinatarioGRT = new en_DestinatarioGRT();
                            registrar.ent_DestinatarioGRT.at_NumeroDocumentoIdentidad = DatosGuia.Rows[0]["NumeroDocIdentidad_Dest"].ToString().Trim();
                            registrar.ent_DestinatarioGRT.at_TipoDocumentoIdentidad = DatosGuia.Rows[0]["TipoDocIdentidad_Dest"].ToString().Trim();
                            registrar.ent_DestinatarioGRT.at_RazonSocial = DatosGuia.Rows[0]["RazonSocial_Rem"].ToString().Trim();

                            registrar.ent_DatosGeneralesGRT = new en_DatosGeneralesGRT();
                            registrar.ent_DatosGeneralesGRT.at_FechaEmision = dtFechaHora.Rows[0]["FechaServidor"].ToString();
                            registrar.ent_DatosGeneralesGRT.at_HoraEmision = dtFechaHora.Rows[0]["HoraServidor"].ToString();
                            registrar.ent_DatosGeneralesGRT.at_Serie = serie;
                            registrar.ent_DatosGeneralesGRT.at_Numero = Convert.ToInt32(numero);
                            registrar.ent_DatosGeneralesGRT.aa_Observacion = new ArrayOfString();
                            registrar.ent_DatosGeneralesGRT.aa_Observacion.Add(DatosGuia.Rows[0]["ObservacionGuia"].ToString());


                            DataTable dtDocumentos = Utilitario.Instancia.ConvertirXMLaDatatable(DatosGuia.Rows[0]["xml_DocumentosRelacion"].ToString());

                            //Cargar Documentos Relacionados
                            if (dgvViajesGuia.GetRowCellValue(Convert.ToInt32(filas.GetValue(i)), "TipoRem").ToString() != "Otros")
                            {
                                if (dtDocumentos.Rows.Count > 0)
                                {
                                    en_DocumentoRelacionadoGRT entDocumentosRelacionados;
                                    registrar.ent_DatosGeneralesGRT.l_DocumentoRelacionadoGRT = new ArrayOfEn_DocumentoRelacionadoGRT();
                                    entDocumentosRelacionados = new en_DocumentoRelacionadoGRT();
                                    entDocumentosRelacionados.at_NumeroComprobante = dtDocumentos.Rows[0]["NumeroComprobante_Relacion"].ToString();
                                    entDocumentosRelacionados.at_TipoComprobante = dtDocumentos.Rows[0]["TipoComprobante_Relacion"].ToString();
                                    entDocumentosRelacionados.at_NombreComprobante = dtDocumentos.Rows[0]["NombreComprobante_Relacion"].ToString();
                                    entDocumentosRelacionados.at_NumeroDocumentoIdentidad = dtDocumentos.Rows[0]["NumeroDocIdentidad_Relacion"].ToString();
                                    entDocumentosRelacionados.at_TipoDocumentoIdentidad = dtDocumentos.Rows[0]["TipoDocIdentidad_Relacion"].ToString();
                                    registrar.ent_DatosGeneralesGRT.l_DocumentoRelacionadoGRT.Add(entDocumentosRelacionados);
                                }

                            }


                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT = new en_InformacionTrasladoGRT();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.aa_IndicadorServicio = new ArrayOfString();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.aa_IndicadorServicio.Add("05");
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.at_FechaInicio = Convert.ToDateTime(DatosGuia.Rows[0]["FechaInicio_Traslado"]).ToString("yyyy-MM-dd");
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT = new en_InformacionPesoBrutoGRT();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT.at_Peso = Convert.ToDecimal(Convert.ToDecimal(DatosGuia.Rows[0]["PesoBruto"]).ToString("N3"));
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT.at_UnidadMedida = DatosGuia.Rows[0]["CodUnidadMedida_Peso"].ToString();
                            //registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT.aa_DescripcionAdicional = new ArrayOfString();


                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT = new en_PuntoPartidaGRT();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Ubigeo = DatosGuia.Rows[0]["UbigeoPuntoPartida"].ToString();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_DireccionCompleta = DatosGuia.Rows[0]["DireccionPuntoPartida"].ToString();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Departamento = UbigeoDireccionPartida[0];
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Provincia = UbigeoDireccionPartida[1];
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Distrito = UbigeoDireccionPartida[2];

                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT = new en_PuntoLlegadaGRT();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Ubigeo = DatosGuia.Rows[0]["UbigeoPuntoLlegada"].ToString();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_DireccionCompleta = DatosGuia.Rows[0]["DireccionPuntoPartida"].ToString();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Departamento = UbigeoDireccionDestino[0];
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Provincia = UbigeoDireccionDestino[1];
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Distrito = UbigeoDireccionDestino[2];

                            // CARGAR UNIDADES

                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT = new ArrayOfEn_VehiculoGRT();
                            ServiceGRT_QA.en_VehiculoGRT entVehiculo = new en_VehiculoGRT();
                            entVehiculo.at_NumeroPlaca = DatosGuia.Rows[0]["NumeroPlaca"].ToString().Trim();
                            ServiceGRT_QA.en_VehiculoGRT entCarreta = new en_VehiculoGRT();
                            entCarreta.at_NumeroPlaca = DatosGuia.Rows[0]["Carreta"].ToString().Trim();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT.Add(entVehiculo);
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT.Add(entCarreta);

                            // CARGAR CONDUCTOR

                            DataTable dtConductor = Utilitario.Instancia.ConvertirXMLaDatatable(DatosGuia.Rows[0]["xml_Conductores"].ToString());

                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT = new ArrayOfEn_ConductorGRT();
                            ServiceGRT_QA.en_ConductorGRT entConductorGuia = new en_ConductorGRT();
                            entConductorGuia.at_TipoDocumentoIdentidad = dtConductor.Rows[0]["TipoDocIdentidad_Conductor"].ToString().Trim();
                            entConductorGuia.at_NumeroDocumentoIdentidad = dtConductor.Rows[0]["NumeroDocIdentidad_Conductor"].ToString().Trim();
                            entConductorGuia.at_Licencia = dtConductor.Rows[0]["Licencia_Conductor"].ToString().Trim();
                            entConductorGuia.at_Nombres = dtConductor.Rows[0]["Nombres_Conductor"].ToString();
                            entConductorGuia.at_Apellidos = dtConductor.Rows[0]["Apellidos_Conductor"].ToString();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT.Add(entConductorGuia);
    
                            //CARGAR PRODUCTO

                            DataTable dtProducto = Utilitario.Instancia.ConvertirXMLaDatatable(DatosGuia.Rows[0]["xml_Productos"].ToString());

                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_BienesGRT = new ArrayOfEn_BienesGRT();
                            en_BienesGRT entBienes = new en_BienesGRT();
                            entBienes.aa_Descripcion = new ArrayOfString();
                            entBienes.aa_Descripcion.Add(dtProducto.Rows[0]["Descripcion_Producto"].ToString());
                            entBienes.at_Codigo = dtProducto.Rows[0]["Codigo_Producto"].ToString().Trim();
                            entBienes.at_Cantidad = Convert.ToDecimal(dtProducto.Rows[0]["Cantidad_Producto"].ToString());
                            entBienes.at_UnidadMedida = dtProducto.Rows[0]["CodUnidadMedida_Producto"].ToString().Trim();
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_BienesGRT.Add(entBienes);


                            // ENVIAR DATOS

                            respuesta = new ens_Respuesta();
                            request = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
                            respuesta = request.RegistrarGRT(registrar);
                            
                            if(respuesta.at_NivelResultado)
                            {

                            }
                            else
                            {
                                RechazadoSunat = true;
                                
                            }
                            //request.Close();   
                        }

                        
                    }

                    this.Cursor = Cursors.Default;
                    if (RechazadoSunat)
                    {
                        MessageBox.Show("No todas las guias fueron enviadas: "+ respuesta.at_MensajeResultado.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Guias Enviadas Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void RegistrarCorreosSecundarios(ene_GuiaRemisionTransportista registrar)
        {
            DataTable dtCorreos = clsOperacionesBL.Instancia.ReportesApp_ListarCorreos_Master(Convert.ToInt32(dgvViajesGuia.GetRowCellValue(0, "idCliente")), "");
            registrar.ent_TransportistaGRT.ent_CorreoGRT.aa_CorreoSecundario.Add(dtCorreos.Rows[0]["Correo"].ToString());
        }

        private void modificarFechaViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuFecha.Visible = true;
            tipoFecha = "Fecha Viaje";
        }

        private void dtgvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FrmListaGuiasElectronicas listarGuia = new FrmListaGuiasElectronicas();
                //listarGuia.txtviaje.Text = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Viaje"));
                listarGuia.txtNumeroFiltro.Text = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Numero"));
                //listarGuia.cbxSerieFiltro.SelectedText = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Serie"));
                listarGuia.dtpFechaInicio.Text = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("FechaViaje"));
                listarGuia.nroSerie = Convert.ToString(dgvViajesGuia.GetFocusedRowCellValue("Serie"));

                listarGuia.TipoOperacion = Utilitario.TipoOperacion.Editar;
                listarGuia.Show();
            }
            catch (Exception ex) { MessageBox.Show("El viaje seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGenerarPeso_Click(object sender, EventArgs e)
        {
            try
            {
                int[] filas = dgvViajesGuia.GetSelectedRows();

                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        string SerieT = dgvViajesGuia.GetRowCellValue(filas[i], "Serie").ToString();
                        string NumeroT = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(filas[i], "Numero")).ToString();
                        int idRuta = Convert.ToInt32(dgvViajesGuia.GetRowCellValue(filas[i], "idRuta"));
                        string idViaje = Convert.ToString(dgvViajesGuia.GetRowCellValue(filas[i], "Viaje"));

                        if (SerieT.Length != 0 || NumeroT.Length != 0)
                        {
                            if (idViaje.Length == 0)
                            {
                                DataTable dtRespuesta = new DataTable();
                                string Respuesta;
                                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_ActualizarPeso(SerieT, NumeroT, idRuta);
                                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                string NroRPTA = Respuesta.Substring(0, 1);
                                if (NroRPTA != "0") { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                            else { MessageBox.Show("Este viaje ya cuenta con el código: " + idViaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else
                        { MessageBox.Show("El viaje no tiene una guía de transportista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    btnBuscar.PerformClick();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un viaje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
