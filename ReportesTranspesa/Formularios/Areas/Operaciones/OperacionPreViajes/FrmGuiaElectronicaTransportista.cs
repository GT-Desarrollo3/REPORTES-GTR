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
using System.IO;
using System.Threading;
using ReportesTranspesa.Properties;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using System.Runtime.InteropServices;



namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmGuiaElectronicaTransportista : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);


        public DataTable dtDepartamento = new DataTable();
        public DataTable dtProvincia = new DataTable();
        public DataTable dtCiudad = new DataTable();
        public DataTable dtTipoServicio = new DataTable();
        public DataTable dtEmpresasGrupo;
        public DataTable dtDireccionesRuta;
        public DataTable dtDireccionesRutaDestinatario;
        public DataTable dtOts;
        public DataTable dtCorreos;
        public DataTable dtFechaHora;
        public DataTable dtRespuestaSunat_Guardado;
        public DataTable dtRespuesta_CDR;
        public DataTable dtRespuestaXML_CDR;
        public DataTable dtDestinatario;
        public int idotSeleccionado = -1;
         

        public string TipoProgramacion = "LIMAGAS"; // (1) TOLVAS , (2) LINLEY , (3) LIMAGAS , (4) General
        public int TipoOperacion = -777;
        public bool TipoTrasladoTotaldeBienes = false; // EN CASO LOS PRODUCTOS SE REFERENCIEN AL DE LA GUIA DE REMISION REMITENTE
        public bool TipoTrasladoProgramado = false; // EN CASO EL TRANSPORTE SEA CON VARIAS UNIDADES Y CONDUCTORES
        public bool TipoTransoporteSubcontratado = false; // ES LA EMPRESA QUIEN NOS SUBCONTRATA 
        public bool TipoTransoporteContratista = false; // CUANDO PAGA EL SERVICIO UN TERCERO 07 - NO ESRegistrarCorreosSecundarios SUBCONTRATADO NI REMITENTE
        Boolean VALIDACIONES = true;
        Boolean esRespuesta = false;
        public string DireccionUbigeoOrigen;
        public string DireccionUbigeoFin;
        string[] TipoServiciosActualizar;
        bool esGuiaElectronica = false;
        string TipoVehiculoTracto = "";
        string TipoVehiculoCarreta = "";
        string esTercero= "";
        string esTerceroCarreta = "";
        public bool esTiempoExcedido = false;
        public string PesoTotal = string.Empty;
        public bool esConsolidado = false;
        public bool esRegistroExitoso = false;
        // datos Tolvas
        public DataTable dtTolvasOT;
   
        public entProductos enProductoTolvas = new entProductos();
        // entidad guia transportista
        public clsGRT entGuiaTransportista = new clsGRT();
        public entConductor entNuevoConductor = new entConductor();
        //public string CarpetaAlacenamientoLogErrores = @"D:\\Proyectos\Proyect_Guias_Electronicas\LogErrores\";
        //public string CarpetaLogSoapErrro = @"D:\\Proyectos\Proyect_Guias_Electronicas\SOAP\";
        public string CarpetaAlacenamientoLogErrores = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\LogErrores\";
        public string CarpetaLogSoapError = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\SOAP\";
        public Boolean AprobadoSunat = false; // atributo true cuando retorne  el codigo 2  
        public Boolean RechazadoSunat = false; // atributo false cuando retorne un codigo mayor a 2
        ErrorProvider error = new ErrorProvider();
        public string CDR_Respuesta_Descripcion;

        // INSTANCIAS DE REGISTRO DE GUIA
        ServicioGuiaRemisionTransportistaClient request;
        ens_Respuesta respuesta;
        ene_ConsultarComprobanteIndividual respuestaSunat;
        ens_ConsultarComprobanteIndividual consultaIndividual;
        ene_ConsultarXML consultarXML_CDR;
        ens_ConsultarXML responseXML;
        ens_ConsultarEstadoGR consultarOtorgamiento;
        en_ResultadoEstadoComprobanteGR respuestaCorreo;
        ens_ResultadoRI resultadoRI;
        

        PictureBox imgPictureBox = new PictureBox();

        public event CargarListaEventHandler CargarLista;
        public delegate void CargarListaEventHandler(FrmGuiaElectronicaTransportista transportista);

        public event CargarListaTolvasEventHandler CargarListaTolvas;
        public delegate void CargarListaTolvasEventHandler(bool esRegisteroExitoso,FrmGuiaElectronicaTransportista transportista);

        public event CargarDocEventHandler CargarDoc;
        public delegate void CargarDocEventHandler();

        public FrmGuiaElectronicaTransportista()
        {
            InitializeComponent();
            btnAgregarProducto.Enabled = true;
            btnQuitarProducto.Enabled = true; 
            cbxTipoServicios.Popup -= cbxTipoServicios_Popup;
            cbxSerieGuia.SelectedValueChanged -= cbxSerieGuia_SelectedValueChanged;
            dtgListaGuiasTransportista.FocusedViewChanged += dtgListaGuiasTransportista_FocusedViewChanged;
        }

        private void FrmGenerarGuia_Load(object sender, EventArgs e)
        {
            try
            {
                dtTipoServicio.Columns.Add("CodServicio", typeof(String));
                dtTipoServicio.Columns.Add("Descripcion", typeof(String));
                CrearGif();
                CargaInicial();
                ListarOTs();
                HabilitarBotonCorreo();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CrearGif()
        {
            imgPictureBox.Location = new System.Drawing.Point(662, 253);
            imgPictureBox.Size = new System.Drawing.Size(239, 226);
            imgPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            imgPictureBox.Image = Resources.cargando_resultados;
        }
       
        private void ListarOTs()
        {
           if (TipoProgramacion != "TOLVAS")
           {
            dtOts = clsOperacionesBL.Instancia.ReportesApp_ListarOtsXProgramacion(Convert.ToInt32(txtCodProgramacion.Tag), entGuiaTransportista.AnioProgramacion, TipoProgramacion, entGuiaTransportista.idviaje);

            if (dtOts != null )
            {
                if (dtOts.Rows.Count > 0)
                {
                    dtgListaGuiasTransportista.DataSource = dtOts;

                    if (dgvListaGuiaTraspExpressVista.RowCount > 0)
                    {
                        dgvListaGuiaTraspExpressVista.FocusedRowHandle = 0;
                    }

                    // dgvListaGuiaTraspExpressVista.ClearSelection();
                    dgvListaGuiaTraspExpressVista.Columns["IdProgramacion"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["Anio"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["NroTicket"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["IdRuta"].Visible = false;
                    dgvListaGuiaTraspExpressVista.Columns["Linea"].Width = 35;
                    dgvListaGuiaTraspExpressVista.Columns["OT"].Width = 42;
                    dgvListaGuiaTraspExpressVista.Columns["GT"].Width = 80;
                    txtCodviaje.Text = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje").ToString();

                    /*if (TipoProgramacion == "LINDLEY" || TipoProgramacion == "VOLCAN")
                    {
                        entGuiaTransportista.idOT = Convert.ToInt32(dtOts.Rows[0]["OT"]);
                        txtOT.Text = entGuiaTransportista.idOT.ToString();
                    }*/

                    if (esConsolidado)
                    {
                        if (idotSeleccionado == -1) { entGuiaTransportista.idOT  = Convert.ToInt32(dtOts.Rows[0]["OT"]); }
                        else { entGuiaTransportista.idOT = idotSeleccionado; }
                        
                        txtOT.Text =  entGuiaTransportista.idOT.ToString();

                        for (int i = 0; i < dgvListaGuiaTraspExpressVista.RowCount; i++)
                        {
                            if (dgvListaGuiaTraspExpressVista.GetRowCellValue(i, "OT").ToString() == entGuiaTransportista.idOT.ToString())
                            {
                                 dgvListaGuiaTraspExpressVista.ClearSelection();
                                 dgvListaGuiaTraspExpressVista.FocusedRowHandle = i;
                                 dgvListaGuiaTraspExpressVista.SelectRow(i);
                                 dgvListaGuiaTraspExpressVista.MakeRowVisible(i);
                           }
                        }

                        txtPesoTotal.Value = Convert.ToDecimal(PesoTotal);
                    }
                    else
                    {
                        if(Convert.ToInt32(dtOts.Rows[0]["Linea"]) != 0)
                        {
                            entGuiaTransportista.LineaOT = Convert.ToInt32(dtOts.Rows[0]["Linea"]);
                            entGuiaTransportista.idOT = Convert.ToInt32(dtOts.Rows[0]["OT"]);
                            txtOT.Text = entGuiaTransportista.idOT.ToString();
                        }
                    }
                }
                DesactivarViajeCompleto();
                }
           }
           else
           {
               dtTolvasOT = clsOperacionesBL.Instancia.ReportesApp_ListarOtsXProgramacion(Convert.ToInt32(txtCodProgramacion.Tag), entGuiaTransportista.AnioProgramacion,TipoProgramacion,entGuiaTransportista.idviaje);

               if (TipoProgramacion == "TOLVAS")
               {
                   txtOT.Text = entGuiaTransportista.idOT.ToString();
                   entGuiaTransportista.LineaOT = Convert.ToInt32(dtTolvasOT.Rows[0]["Linea"]);
               }

               if (dtTolvasOT.Rows.Count > 0 )
               {
                   dtgListaGuiasTransportista.DataSource = dtTolvasOT;
                   dgvListaGuiaTraspExpressVista.Columns["IdProgramacion"].Visible = false;
                   dgvListaGuiaTraspExpressVista.Columns["Anio"].Visible = false;
                   dgvListaGuiaTraspExpressVista.Columns["NroTicket"].Visible = false;
                   dgvListaGuiaTraspExpressVista.Columns["IdRuta"].Visible = false;
                   dgvListaGuiaTraspExpressVista.Columns["Linea"].Width = 35;
                   dgvListaGuiaTraspExpressVista.Columns["OT"].Width = 42;
                   dgvListaGuiaTraspExpressVista.Columns["GT"].Width = 80;
                   txtCodviaje.Text = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje").ToString();
               }
               else { dtgListaGuiasTransportista.DataSource = null; }
           }
        }

        private void HabilitarBotonCorreo()
        {
            if (txtEmpresaDestinatario.Tag != null) { btnCorreos.Enabled = true; }
            else
            {
                btnCorreos.Enabled = false;
                txtDocIdentidadDesti.Clear();
            }
        }

        private void CargaInicial()
        {
            if (TipoOperacion == Utilitario.TipoOperacion.Editar)
            {
               
                dtpFechaRegistro.Text = entGuiaTransportista.entGRT_Generales_FechaEmision_M;
                dtpFechaTraslado.Text = entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M;
                cbxEmpresasGrupo.SelectedValue = entGuiaTransportista.compania;
                txtCodProgramacion.Text = entGuiaTransportista.CodigoProgramacion;
                txtCodProgramacion.Tag = entGuiaTransportista.idProgramacion;
                txtCodviaje.Text = entGuiaTransportista.viaje;
                txtCodviaje.Tag = entGuiaTransportista.idviaje.ToString();
                txtObservaciones.Text = entGuiaTransportista.entGRT_Generales_Observacion;
                lblEstado.Text = entGuiaTransportista.entGRT_Respuesta_EstadoSunat;
                txtOT.Text = entGuiaTransportista.idOT.ToString();
                txtCliente.Text = entGuiaTransportista.Cliente;
                txtCliente.Tag = Convert.ToString(entGuiaTransportista.idcliente);
                txtPlaca.ReadOnly = true;
                txtCarreta.ReadOnly = true;
                txtConductor.ReadOnly = true;
                txtRuta.ReadOnly = true;
                txtTarjetaCirculacion.ReadOnly = true;
                txtOT.ReadOnly = true;

                if (entGuiaTransportista.TipoViaje == "SALIDA") { rbtSalida.Checked = true; }
                if (entGuiaTransportista.TipoViaje == "RETORNO") { rbtRetorno.Checked = true; }
                
                CargarCombos();
                CargarRemitente();
                CargarEmpresDestinatario();
                CargarRuta();
                CargarTipoServicioActualizar();
                CargarPlacaConductor();
                CargarProductos();
                DesactivarViajeCompleto();
            }

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar && (TipoProgramacion == "LIMAGAS" || TipoProgramacion == "SOLGAS"  || TipoProgramacion == "LINDLEY" || TipoProgramacion == "GENERAL" || TipoProgramacion == "TOLVAS" || TipoProgramacion == "LOCAL" || TipoProgramacion == "VOLCAN"))
            {
                entGuiaTransportista.esConsolidado = esConsolidado;
                cbxTipoServicios_EditValueChanged(this, null);
                cbxTipoServicios.EditValueChanged -= cbxTipoServicios_EditValueChanged;
                cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                txtPesoTotal.Value = Convert.ToDecimal(PesoTotal);
                dtFechaHora = clsOperacionesBL.Instancia.ObtenerFechaHoraServidorGuiaElectronica();
                dtpFechaRegistro.Text = Convert.ToDateTime(dtFechaHora.Rows[0]["FechaServidor"].ToString()).ToString("dd/MM/yyyy");
                CargarCombos();
                CargarRemitente();
                txtCodProgramacion.Text = entGuiaTransportista.CodigoProgramacion;
                txtCodProgramacion.Tag = entGuiaTransportista.idProgramacion;
                txtConductor.Text = entGuiaTransportista.conductor;
                txtConductor.Tag = entGuiaTransportista.idconductor.ToString();
                txtCodviaje.Text = "";
                txtCodviaje.Tag = Convert.ToString(0);
                txtPlaca.Text = entGuiaTransportista.tracto;
                txtPlaca.Tag = entGuiaTransportista.idtracto;
                txtCarreta.Text = entGuiaTransportista.carreta.Replace(".","").Replace("-","");
                txtCarreta.Tag = entGuiaTransportista.idCarreta.ToString() == "-1" ? null : entGuiaTransportista.idCarreta.ToString();
                CargarEmpresDestinatario();
                CargarRuta();
                //if (TipoProgramacion == "TOLVAS") { txtEmpresaRemitente.ReadOnly = true; txtEmpresaDestinatario.ReadOnly = true; txtRuta.ReadOnly = false; txtDireccionPartida.ReadOnly = false; txtDireccionDestino.ReadOnly = false; }
                
                

                DataTable dtTarjetaTracto = clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idtracto));
          

                if (dtTarjetaTracto.Rows.Count > 0)
                {
                    TipoVehiculoTracto = dtTarjetaTracto.Rows[0]["TIPO"].ToString();
                    esTercero = dtTarjetaTracto.Rows[0]["ORIGEN"].ToString();

                    if (dtTarjetaTracto.Rows[0]["TIPO"].ToString() != "CISTERNA" && esTercero == "P")
                    {
                        if (clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idtracto)).Rows[0]["TarjetaCirculacion"].ToString() != "")
                        {
                            String tarjetaCirculacion = clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idtracto)).Rows[0]["TarjetaCirculacion"].ToString();
                            entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion = tarjetaCirculacion.Length == 9 ? "0" + tarjetaCirculacion : tarjetaCirculacion;

                            rbTUC.Checked = false;
                            rbMatpel.Checked = false;
                            rbPrueba.Checked = true;
                            
                            txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;
                        }
                    }
    
                }
               
                else
                {
                    MessageBox.Show("Tracto " + txtPlaca.Text + " no Tiene Tarjeta de Circulacion MTC, Favor solicitar registro a Control Documentario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl1.Enabled = false;
                    return;
                }


                DataTable dtTarjetaCarreta = clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idCarreta));

                if (dtTarjetaCarreta.Rows.Count > 0)
                {
                    TipoVehiculoCarreta = dtTarjetaCarreta.Rows[0]["TIPO"].ToString();
                    esTerceroCarreta = dtTarjetaCarreta.Rows[0]["ORIGEN"].ToString();

                        if (clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idCarreta)).Rows[0]["TarjetaCirculacion"].ToString() != "")
                        {
                            String tarjetaCirculacionCarreta = clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idCarreta)).Rows[0]["TarjetaCirculacion"].ToString();
                            entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = tarjetaCirculacionCarreta.Length == 9 ? "0" + tarjetaCirculacionCarreta : tarjetaCirculacionCarreta;
                            //txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta;
                        }
                }
              

                entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M = txtPlaca.Text;
                //Obtener Correos de clientes a los cuales se les enviará la guia


                //Obtener datos del Emisor (Transpesa)
                entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = dtEmpresasGrupo.Rows[0]["NumeroDocumento"].ToString();
                entGuiaTransportista.entGRT_Emisor_RazonSocial_M = dtEmpresasGrupo.Rows[0]["RazonSocial"].ToString();
                entGuiaTransportista.entGRT_Emisor_NombreComercial = dtEmpresasGrupo.Rows[0]["NombreComercial"].ToString();
                entGuiaTransportista.entGRT_Emisor_NumeroMTC = dtEmpresasGrupo.Rows[0]["NumeroMTC"].ToString();
                entGuiaTransportista.entGRT_Emisor_Telefono = dtEmpresasGrupo.Rows[0]["Telefono"].ToString();
                entGuiaTransportista.entGRT_Emisor_CorreoContacto = dtEmpresasGrupo.Rows[0]["CorreoContacto"].ToString();
                entGuiaTransportista.entGRT_Emisor_SitioWeb = dtEmpresasGrupo.Rows[0]["SitioWeb"].ToString();



                // DatosGenerales de guia 
                dtpFechaTraslado.Value = Convert.ToDateTime(entGuiaTransportista.FechaProgramacion).Date;
                entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M = dtpFechaTraslado.Value.ToString();

                //Obtener la ruta de la programacion
 /*               if (rbtRetorno.Checked == false)
                {
                    DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica(entGuiaTransportista.ruta);
                    if (dtRuta.Rows.Count > 0)
                    {
              
                            txtRuta.Text = entGuiaTransportista.ruta;
                            txtRuta.Tag = entGuiaTransportista.idRuta.ToString();
                            entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = dtRuta.Rows[0]["UbigeoOrigen"].ToString();// ubigeo partida
                            DireccionUbigeoOrigen = dtRuta.Rows[0]["DescripcionOrigen"].ToString();
                            entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = dtRuta.Rows[0]["UbigeoFin"].ToString(); // ubigeo destino
                            DireccionUbigeoFin = dtRuta.Rows[0]["DescripcionFin"].ToString();

                            txtDireccionPartida.Enabled = true;
                            txtDireccionDestino.Enabled = true;
                     }

                }
                */
                if (txtConductor.Tag != null)
                {

                    // obtener nombre y apellidos por separado del CONDUCTOR
                    DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor.Tag));

                    txtLicencia.Text = dt.Rows[0]["Brevete"].ToString();
                    txtDocIdentidad.Text = dt.Rows[0]["Documento"].ToString();
                    txtTipoDocumentoIdentidad.Text = dt.Rows[0]["TipoDocumento"].ToString();
                    txtTipoDocumentoIdentidad.Tag = dt.Rows[0]["Codigo"].ToString();
                    
                    entNuevoConductor.entGRT_Conductor_Nombres_M = dt.Rows[0]["Nombres"].ToString();
                    entNuevoConductor.entGRT_Conductor_Apellidos_M = dt.Rows[0]["Apellidos"].ToString();
                    entNuevoConductor.entGRT_Conductor_Licencia_M = dt.Rows[0]["Brevete"].ToString();
                    entNuevoConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M = dt.Rows[0]["Documento"].ToString();
                    entNuevoConductor.entGRT_Conductor_TipoDocumentoIdentidad_M = dt.Rows[0]["Codigo"].ToString();
                    entGuiaTransportista.idconductor = Convert.ToInt32(dt.Rows[0]["idConductor"]);
                    entGuiaTransportista.entConductor = entNuevoConductor;


                }

                if (entGuiaTransportista.entGRT_Remitente_RazonSocial_M == "LIMA GAS S A")
                {
                    CargarEmpresDestinatario();
                }
            }

        }

        private void CargarRemitente()
        {
            //Obtener datos del cliente "remitente"
            if (entGuiaTransportista.entGRT_Remitente_RazonSocial_M != null)
            {
                txtEmpresaRemitente.Text = entGuiaTransportista.entGRT_Remitente_RazonSocial_M;
                txtEmpresaRemitente.Tag = entGuiaTransportista.idRemitente;
                txtEmpresaRemitente_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                lstEmpresaRemitente.Select();
                lstEmpresaRemitente_KeyUp(this, new KeyEventArgs(Keys.Down));
                lstEmpresaRemitente_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));


                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    DataTable dtClienteProgramacion = clsOperacionesBL.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(Convert.ToInt32(txtEmpresaRemitente.Tag));
                    entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["DocumentoFiscal"].ToString();
                    entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["Codigo"].ToString();
                }
            }
        }

        private void CargarProductos()
        {
            txtPesoTotal.Value = entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M;
            cbxUnidadMedidaTotal.SelectedValue = entGuiaTransportista.entGRT_PesoBruto_CodigoUnidadMedida_M;
            txtObservacionCargaTotal.Text = entGuiaTransportista.entGRT_PesoBruto_DescripcionAdicional;

            if(entGuiaTransportista.xml_entGRT_Productos_Bienes  != "")
            {

                DataTable dtProductos = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaTransportista.xml_entGRT_Productos_Bienes); 
                if(dtProductos.Rows.Count > 0 )
                {
                    for(int i = 0 ; i < dtProductos.Rows.Count ; i ++)
                    {
                        dgvProductosGuia.Rows.Add(dtProductos.Rows[i]["Codigo_Producto"],
                                                  dtProductos.Rows[i]["Descripcion_Producto"],
                                                  dtProductos.Rows[i]["Cantidad_Producto"],
                                                  dtProductos.Rows[i]["CodUnidadMedida_Producto"],
                                                  dtProductos.Rows[i]["Peso_Producto"]);
                    }
                
                }
            }



        }

        private void CargarTipoServicioActualizar()
        {

            cbxTipoServicios_EditValueChanged(this, null);
            cbxTipoServicios.EditValueChanged -= cbxTipoServicios_EditValueChanged;
            cbxTipoServicios.Popup += cbxTipoServicios_Popup;

            TipoServiciosActualizar = entGuiaTransportista.xml_entGRT_TipoServicio.Split(',');
            if (TipoServiciosActualizar.Length > 0)
            {

                

                for (int i = 0; i < TipoServiciosActualizar.Length; i++)
                {

                    if (TipoServiciosActualizar[i] == "00") // vehiculo y conductor
                    {
                        
                        cbxTipoServicios.Properties.Items[1].CheckState = CheckState.Unchecked;
                        cbxTipoServicios.Properties.Items[0].CheckState = CheckState.Checked;
                        tabPage2.Parent = tabContingencia; 
                        btnAgregarProgramacion.Visible = false;
                        groupVehiculoConductor.Size = new Size(956, 93);

                    }

                    if (TipoServiciosActualizar[i] == "01") // Trasbordo Programado
                    {
                        
                        entGuiaTransportista.TipoTrasladoProgramado = true;
                        cbxTipoServicios.Properties.Items[0].CheckState = CheckState.Unchecked;
                        tabPage4.Parent = tabContingencia;
                        btnAgregarProgramacion.Visible = true;
                        groupVehiculoConductor.Size = new Size(956, 157);
                        TipoTrasladoProgramado = true;
                    }

                    if (TipoServiciosActualizar[i] == "04") // Subcontratado
                    {
                        tabPage5.Parent = tabContingencia; // subcontratado
                        cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Checked; // Transporte SubContratado
                        
                        entGuiaTransportista.TipoTransoporteSubcontratado = true;
                        TipoTransoporteSubcontratado = true;

                        txtRazonSocialSubContra.Text =  entGuiaTransportista.entGRT_SubContratista_RazonSocial;
                        txtRazonSocialSubContra_KeyUp(this, new KeyEventArgs((Keys.Enter)));
                        lstSubcontratado.Select();
                        lstSubcontratado_KeyUp(this, new KeyEventArgs(Keys.Down));
                        lstSubcontratado_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    }

                    if (TipoServiciosActualizar[i] == "05") // "Pago de flete de Remitente"
                    {
                        cbxTipoServicios.Properties.Items[5].CheckState = CheckState.Checked;
                       
                    }

                    if (TipoServiciosActualizar[i] == "06")
                    {

                        cbxTipoServicios.Properties.Items[6].CheckState = CheckState.Checked; // Activo el check de  "Pago de flete de Subcontratador"

                    }
                    if (TipoServiciosActualizar[i] == "07") // Pago flete Tercero
                    {
                        tabPage6.Parent = tabContingencia;
                        cbxTipoServicios.Properties.Items[7].CheckState = CheckState.Checked;
                        entGuiaTransportista.TipoFleteTercero = true;
                        TipoTransoporteContratista = true;
                        txtRazonSocialPagadorTercero.Text = entGuiaTransportista.entGRT_Contratista_RazonSocial;

                        txtRazonSocialPagadorTercero_KeyUp(this, new KeyEventArgs(Keys.Escape));
                        lstFleteTercero.Select();
                        lstFleteTercero_KeyUp(this, new KeyEventArgs(Keys.Down));
                        lstFleteTercero_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                    }

                    if (TipoServiciosActualizar[i] == "08") // Traslado total de
                    {
                        dgvProductosGuia.ReadOnly = false;
                        btnAgregarProducto.Enabled = false;
                        btnQuitarProducto.Enabled = false;
                        TipoTrasladoTotaldeBienes = true;
                        entGuiaTransportista.TipoTrasladoTotaldeBienes = true;
                    }
                }
            }
        }

        private void CargarPlacaConductor()
        {

            DataTable dtConductoresEditar = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaTransportista.xml_entGTR_Conductor_M);


            if (TipoTrasladoProgramado)
            {
                for (int i = 0; i < dtConductoresEditar.Rows.Count; i++)
                {

                    if (Convert.ToInt32(dtConductoresEditar.Rows[i]["idConductor"]) == 0)
                    {
                        VALIDACIONES = false;
                        MessageBox.Show("No se pudo cargar el id del Conductor " + dtConductoresEditar.Rows[i]["Nombres_Conductor"].ToString() + " " + dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString() , "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }

                    dgvTrasladoProgramado.Rows.Add(dtConductoresEditar.Rows[i]["idConductor"].ToString(),
                                                   dtConductoresEditar.Rows[i]["Nombres_Conductor"].ToString() + " " + dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString(),
                                                   dtConductoresEditar.Rows[i]["idPlaca"].ToString(),
                                                   dtConductoresEditar.Rows[i]["Tracto"].ToString(),
                                                   dtConductoresEditar.Rows[i]["Licencia_Conductor"].ToString(),
                                                   dtConductoresEditar.Rows[i]["NumeroDocIdentidad_Conductor"].ToString(),
                                                   dtConductoresEditar.Rows[i]["TipoDocIdentidad_Conductor"].ToString(),
                                                   dtConductoresEditar.Rows[i]["Nombres_Conductor"].ToString(),
                                                   dtConductoresEditar.Rows[i]["Apellidos_Conductor"].ToString(),
                                                   dtConductoresEditar.Rows[i]["idCarreta"].ToString(),
                                                   dtConductoresEditar.Rows[i]["Carreta"].ToString(),
                                                   dtConductoresEditar.Rows[i]["PlacaTarjetaCircula"].ToString(),
                                                   dtConductoresEditar.Rows[i]["CarretaTarjetaCircula"].ToString());
                    


                }
            }
            else
            {
                txtPlaca.Text = dtConductoresEditar.Rows[0]["Tracto"].ToString();
                txtPlaca.Tag = dtConductoresEditar.Rows[0]["idPlaca"].ToString();
                entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M = dtConductoresEditar.Rows[0]["Tracto"].ToString();
                entGuiaTransportista.idtracto = dtConductoresEditar.Rows[0]["idPlaca"].ToString(); ;

                txtCarreta.Text = dtConductoresEditar.Rows[0]["Carreta"].ToString();
                txtCarreta.Tag =  dtConductoresEditar.Rows[0]["idCarreta"].ToString();
                entGuiaTransportista.carreta = dtConductoresEditar.Rows[0]["Carreta"].ToString() ;
                entGuiaTransportista.idCarreta = dtConductoresEditar.Rows[0]["idCarreta"].ToString();


                if (dtConductoresEditar.Rows[0]["PlacaTarjetaCircula"].ToString().Length > 0)
                {
                    rbTUC.Checked = false;
                    rbMatpel.Checked = false;
                    rbPrueba.Checked = true;

                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null)
                    {
                        txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;
                    }
                }

                if (dtConductoresEditar.Rows[0]["CarretaTarjetaCircula"].ToString().Length > 0)
                {
                    entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = dtConductoresEditar.Rows[0]["CarretaTarjetaCircula"].ToString();
                }
                
                
                
                //txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;

                entGuiaTransportista.idconductor = Convert.ToInt32(dtConductoresEditar.Rows[0]["idConductor"]);
                entGuiaTransportista.conductor = dtConductoresEditar.Rows[0]["Nombres_Conductor"].ToString() + " " + dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString();
                txtConductor.Text = dtConductoresEditar.Rows[0]["Nombres_Conductor"].ToString() + " " + dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString();
                entNuevoConductor.entGRT_Conductor_Nombres_M = dtConductoresEditar.Rows[0]["Nombres_Conductor"].ToString();
                entNuevoConductor.entGRT_Conductor_Apellidos_M = dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString();
                entNuevoConductor.entGRT_Conductor_Licencia_M = dtConductoresEditar.Rows[0]["Licencia_Conductor"].ToString();
                txtLicencia.Text = dtConductoresEditar.Rows[0]["Licencia_Conductor"].ToString();
                txtDocIdentidad.Text = dtConductoresEditar.Rows[0]["NumeroDocIdentidad_Conductor"].ToString();
                entNuevoConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M = dtConductoresEditar.Rows[0]["NumeroDocIdentidad_Conductor"].ToString();
                entNuevoConductor.entGRT_Conductor_TipoDocumentoIdentidad_M = dtConductoresEditar.Rows[0]["TipoDocIdentidad_Conductor"].ToString();
                txtTipoDocumentoIdentidad.Text = dtConductoresEditar.Rows[0]["NombreTipoDoc"].ToString();
                txtTipoDocumentoIdentidad.Tag = dtConductoresEditar.Rows[0]["TipoDocIdentidad_Conductor"].ToString();
                entGuiaTransportista.entConductor = entNuevoConductor;
               
                DataTable dtTarjetaCarreta = clsOperacionesBL.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(Convert.ToInt32(entGuiaTransportista.idCarreta));

                if (dtTarjetaCarreta.Rows.Count > 0)
                {
                    TipoVehiculoCarreta = dtTarjetaCarreta.Rows[0]["TIPO"].ToString();
                }
            }

        }

        private void CargarRuta()
        {
            txtRuta.Text = entGuiaTransportista.ruta;
            txtRuta.Tag = entGuiaTransportista.idRuta;
            DireccionUbigeoOrigen = entGuiaTransportista.entGRT_PuntoPartida_Nombre_Ubigeo_M;
            DireccionUbigeoFin = entGuiaTransportista.entGRT_PuntoDestino_Nombre_Ubigeo_M;
            txtUbigeoPartida.Text = DireccionUbigeoOrigen;
            txtUbigeoDestino.Text = DireccionUbigeoFin;
           


            txtRuta_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstRuta.Select();
            lstRuta_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstRuta_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
            CargarSecuenciaDireccion();
  

        }

        private void CargarSecuenciaDireccion()
        {
            txtDireccionPartida.Text = entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M;
            txtDireccionPartida_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstDireccionPartida.Select();
            lstDireccionPartida_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstDireccionPartida_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

            txtDireccionDestino.Text = entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M;
            txtDireccionDestino_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstDireccionDestino.Select();
            lstDireccionDestino_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstDireccionDestino_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
        }

        private void CargarEmpresDestinatario()
        {
            txtDireccionDestino.Text = entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M;
            txtEmpresaDestinatario.Text = entGuiaTransportista.entGRT_Destinatario_RazonSocial_M;
            txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstEmpresaDestinatario.Select();
            lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
            if (entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M != null)
            {
                txtCorreoPrimario.Text = entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M;
            }
            
        }



        private void CargarCombos()
        {

           

            dtEmpresasGrupo = clsOperacionesBL.Instancia.ReportesApp_ListarEmpresasGrupo();

            if (dtEmpresasGrupo.Rows.Count > 0)
            {

                cbxEmpresasGrupo.DataSource = dtEmpresasGrupo;
                cbxEmpresasGrupo.DisplayMember = "RazonSocial";
                cbxEmpresasGrupo.ValueMember = "Compania";
                cbxEmpresasGrupo.SelectedIndex = 0;

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = dtEmpresasGrupo.Rows[0]["NumeroDocumento"].ToString();
                    entGuiaTransportista.entGRT_Emisor_RazonSocial_M = dtEmpresasGrupo.Rows[0]["RazonSocial"].ToString();
                    entGuiaTransportista.entGRT_Emisor_NombreComercial = dtEmpresasGrupo.Rows[0]["NombreComercial"].ToString();
                    entGuiaTransportista.entGRT_Emisor_NumeroMTC = dtEmpresasGrupo.Rows[0]["NumeroMTC"].ToString();
                    entGuiaTransportista.entGRT_Emisor_CodigoPais_M = dtEmpresasGrupo.Rows[0]["CodigoPais"].ToString();
                    entGuiaTransportista.entGRT_Emisor_Telefono = dtEmpresasGrupo.Rows[0]["Telefono"].ToString();
                    entGuiaTransportista.entGRT_Emisor_SitioWeb = dtEmpresasGrupo.Rows[0]["SitioWeb"].ToString();
                    entGuiaTransportista.entGRT_Emisor_CorreoContacto = dtEmpresasGrupo.Rows[0]["CorreoContacto"].ToString();
                    entGuiaTransportista.entGRT_Emisor_Ubigeo_M = dtEmpresasGrupo.Rows[0]["UbigeoEmpresa"].ToString();
                    entGuiaTransportista.entGRT_Emisor_Provincia = dtEmpresasGrupo.Rows[0]["Provincia"].ToString();
                    entGuiaTransportista.entGRT_Emisor_Departamento = dtEmpresasGrupo.Rows[0]["Departamento"].ToString();
                    entGuiaTransportista.entGRT_Emisor_Distrito = dtEmpresasGrupo.Rows[0]["Distrito"].ToString();
                    entGuiaTransportista.entGRT_Emisor_DireccionDetallada = dtEmpresasGrupo.Rows[0]["DireccionFiscal"].ToString();
                    entGuiaTransportista.compania = cbxEmpresasGrupo.SelectedValue.ToString();
                }



            }
            else
            {
                MessageBox.Show("Combobox de Empresas no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }


            DataSet ds = clsOperacionesBL.Instancia.ReportesApp_Listar_Departamentos_Provincias_Ciudades();
            if (ds.Tables.Count > 0)
            {
                dtDepartamento = ds.Tables["Departamento"];
                dtProvincia = ds.Tables["Provincia"];
                dtCiudad = ds.Tables["Ciudad"];
            }

            // Combobox de Unidad de Medida

            //carga unidad medida producto
            cbxUnidadMedidaTotal.DataSource = clsOperacionesBL.Instancia.ReportesApp_ListarUnidadMedidaCargaTotal_Sunat();
            cbxUnidadMedidaTotal.DisplayMember = "Descripcion";
            cbxUnidadMedidaTotal.ValueMember = "Codigo";
            cbxUnidadMedidaTotal.SelectedValue = "KGM";

            if (TipoProgramacion == "VOLCAN")
            {
                cbxUnidadMedidaTotal.SelectedValue = "TNE";
            }



            //combo para datagriedview
            DataGridViewComboBoxColumn UnidadMedida = dgvProductosGuia.Columns["UnidadMedida"] as DataGridViewComboBoxColumn;
            UnidadMedida.DataSource = clsOperacionesBL.Instancia.ReportesApp_ListarUnidadMedida_Sunat();
            UnidadMedida.DisplayMember = "Descripcion";
            UnidadMedida.ValueMember = "Codigo";

       
           

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar )
            {

                if (TipoProgramacion == "TOLVAS" || TipoProgramacion == "LOCAL")
                {
                    if (UnidadMedida.Items.Count > 0 && enProductoTolvas.entGTR_ProductoBienes_Descripcion.Length > 0 )
                    {
                        dgvProductosGuia.Rows.Add(enProductoTolvas.entGTR_ProductoBienes_Codigo, enProductoTolvas.entGTR_ProductoBienes_Descripcion, "1.00", "", "");
                        dgvProductosGuia.Rows[0].Cells["UnidadMedida"].Value = cbxUnidadMedidaTotal.SelectedValue.ToString();
                    }
                }
                else
                {
                    if (UnidadMedida.Items.Count > 0)
                    {
                        dgvProductosGuia.Rows.Add("", "TRASLADO TOTAL DE BIENES", "", "", "");
                    }
                }
   
              
            }


            // Llenado de Combobox de Tipo de Documentos en ComboBox y Datagriedview

            DataTable dtTipoDocFiscal = clsOperacionesBL.Instancia.ReportesApp_Listar_TipoDocumentoFiscal_GuiaElectronica();
            if (dtTipoDocFiscal.Rows.Count > 0)
            {
                cbxTipoDocumentoFiscal.DataSource = dtTipoDocFiscal;
                cbxTipoDocumentoFiscal.DisplayMember = "Descripcion";
                cbxTipoDocumentoFiscal.ValueMember = "Codigo";
                cbxTipoDocumentoFiscal.SelectedIndex = 0;


                if (TipoProgramacion == "TOLVAS")
                {

                    int i = 0;
                    foreach (var item in cbxTipoDocumentoFiscal.Items)
                    {

                        DataRowView dataRowView = item as DataRowView;

                        if (dataRowView["Descripcion"].ToString() == "Otros")
                        {
                            cbxTipoDocumentoFiscal.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }

            }
            else
            {
                MessageBox.Show("Combobox de TipoProducto no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


            DataGridViewComboBoxColumn dtDocRelacion = dgvDocumentosRelacionados.Columns["TipoDocRelacion"] as DataGridViewComboBoxColumn;
            dtDocRelacion.DataSource = clsOperacionesBL.Instancia.ReportesApp_Listar_TipoDocumentoFiscal_GuiaElectronica();
            dtDocRelacion.DisplayMember = "Descripcion";
            dtDocRelacion.ValueMember = "Codigo";

            DataGridViewComboBoxColumn dtDocRelacionIdentidad = dgvDocumentosRelacionados.Columns["TipoDocumentoIdentidad"] as DataGridViewComboBoxColumn;
            dtDocRelacionIdentidad.DataSource = clsOperacionesBL.Instancia.ReportesApp_Listar_TipoDocumentoIdentidad_GuiaElectronica();
            dtDocRelacionIdentidad.DisplayMember = "Descripcion";
            dtDocRelacionIdentidad.ValueMember = "Codigo";

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                if (dtDocRelacion.Items.Count > 0)
                {
                    dgvDocumentosRelacionados.Rows.Add("", "", "", "", "","");
                    dgvDocumentosRelacionados.Rows[0].Cells["TipoDocRelacion"].Value = (dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
                    dgvDocumentosRelacionados.Rows[0].Cells["NombreDocumento"].Value = Utilitario.Instancia.QuitarTildes((dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[1].ToString());
                    dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocumentoIdentidad"].Value = entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M;
                    dgvDocumentosRelacionados.Rows[0].Cells["TipoDocumentoIdentidad"].Value = entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M;
                    dgvDocumentosRelacionados.Rows[0].Cells["TipoDocumentoIdentidad"].Value = "0.00";
                   
                }
            }
            if (TipoOperacion == Utilitario.TipoOperacion.Editar)
            {
                if (entGuiaTransportista.xml_entGRT_DocumentosRelacion.Length > 0)
                {
                    checkDocRelacion.Checked = true;
                    DataTable dtDocRelacion_Editar = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaTransportista.xml_entGRT_DocumentosRelacion);

                    if (dtDocRelacion_Editar.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDocRelacion_Editar.Rows.Count; i++)
                        {
                            dgvDocumentosRelacionados.Rows.Add(dtDocRelacion_Editar.Rows[i]["TipoComprobante_Relacion"],
                                                               dtDocRelacion_Editar.Rows[i]["NombreComprobante_Relacion"],
                                                               dtDocRelacion_Editar.Rows[i]["NumeroComprobante_Relacion"],
                                                               dtDocRelacion_Editar.Rows[i]["NumeroDocIdentidad_Relacion"],
                                                               dtDocRelacion_Editar.Rows[i]["TipoDocIdentidad_Relacion"],
                                                               dtDocRelacion_Editar.Rows[i]["PesoGuia"]);
                        }

                        txtDocRelacionAnexar.Text = dtDocRelacion_Editar.Rows[0]["NumeroComprobante_Relacion"].ToString();

                        if (dtDocRelacion_Editar.Rows[0]["NombreComprobante_Relacion"].ToString() == "Guia de remision" || dtDocRelacion_Editar.Rows[0]["NombreComprobante_Relacion"].ToString() == "Guía de remisión")
                        {
                            cbxTipoDocumentoFiscal.SelectedIndex = 0;
                            dgvDocumentosRelacionados.Rows[0].Cells["TipoDocRelacion"].Value = (dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
                        }
                        if (dtDocRelacion_Editar.Rows[0]["NombreComprobante_Relacion"].ToString() == "Otros")
                        {
                            cbxTipoDocumentoFiscal.SelectedIndex = 4;
                            dgvDocumentosRelacionados.Rows[0].Cells["TipoDocRelacion"].Value = (dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
                        }
                        if (dtDocRelacion_Editar.Rows[0]["NombreComprobante_Relacion"].ToString() == "Factura")
                        {
                            cbxTipoDocumentoFiscal.SelectedIndex = 3;
                            dgvDocumentosRelacionados.Rows[0].Cells["TipoDocRelacion"].Value = (dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
                        }
                        if (dtDocRelacion_Editar.Rows[0]["NombreComprobante_Relacion"].ToString() == "Boleta de Venta")
                        {
                            cbxTipoDocumentoFiscal.SelectedIndex = 2;
                            dgvDocumentosRelacionados.Rows[0].Cells["TipoDocRelacion"].Value = (dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
                        }
                    }

                }
                
            }

            

            // CARGAR SERIES  SEGUN EL TIPO DE GUIA  TRASPORTISTA(T) , REMITENTE (R)

            DataTable dtSerieGuia = clsOperacionesBL.Instancia.ReportesApp_Listar_SerieGuiasElectronicas("T");


            if (dtSerieGuia.Rows.Count > 0)
            {
                cbxSerieGuia.DataSource = dtSerieGuia;
                cbxSerieGuia.DisplayMember = "SerieGuia";
                cbxSerieGuia.ValueMember = "SerieGuia";
                cbxSerieGuia.SelectedIndex = 0;

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    entGuiaTransportista.entGRT_Generales_Serie_M = dtSerieGuia.Rows[0]["SerieGuia"].ToString();
                    cbxSerieGuia.SelectedValueChanged += cbxSerieGuia_SelectedValueChanged;
                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    cbxSerieGuia.SelectedValue = entGuiaTransportista.entGRT_Generales_Serie_M;
                    lblNumero.Text = entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8");
                    cbxSerieGuia.SelectedValueChanged += cbxSerieGuia_SelectedValueChanged;
                    cbxSerieGuia.Enabled = false;

                }

            }



            DataTable dtTipoServicio = clsOperacionesBL.Instancia.ReportesApp_TipoServicioGuiaElectronica("T");
            if (dtTipoServicio.Rows.Count > 0)
            {
                cbxTipoServicios.Properties.DataSource = dtTipoServicio;
                cbxTipoServicios.Properties.DisplayMember = "Descripcion";
                cbxTipoServicios.Properties.ValueMember = "CodServicio";
                cbxTipoServicios.Properties.SeparatorChar = ',';

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar && (TipoProgramacion != "TOLVAS" && TipoProgramacion != "LOCAL"))
                {
                    cbxTipoServicios.SetEditValue("00,05,08");
                    //cbxTipoServicios.Properties.Items[8].Enabled = false;
                }
                else
                {
                    cbxTipoServicios.SetEditValue("00,05");
                    cbxTipoServicios.Properties.Items[8].Enabled = true;
                    //dgvProductosGuia.Enabled = false;
                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    if (TipoProgramacion == "TOLVAS" || TipoProgramacion == "LOCAL")
                    {
                        cbxTipoServicios.SetEditValue(entGuiaTransportista.xml_entGRT_TipoServicio);
                        cbxTipoServicios.Properties.Items[8].Enabled = true;
                        //dgvProductosGuia.Enabled = true;
                    }
                    else
                    {

                        cbxTipoServicios.SetEditValue(entGuiaTransportista.xml_entGRT_TipoServicio);
                        //cbxTipoServicios.Properties.Items[8].Enabled = false;
                        //dgvProductosGuia.Enabled = true;
                    }
                   
                }
            }



        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                dtFechaHora = clsOperacionesBL.Instancia.ObtenerFechaHoraServidorGuiaElectronica();
                dtpFechaRegistro.Text = Convert.ToDateTime(dtFechaHora.Rows[0]["FechaServidor"].ToString()).ToString("dd/MM/yyyy");
                entGuiaTransportista.entGRT_Generales_FechaEmision_M = dtFechaHora.Rows[0]["FechaServidor"].ToString();
                entGuiaTransportista.entGRT_Generales_HoraEmision_M = dtFechaHora.Rows[0]["HoraServidor"].ToString();
                entGuiaTransportista.entGRT_ControlOtorgamiento_Estado_M = true;
                entGuiaTransportista.entGRT_Generales_Serie_M = cbxSerieGuia.SelectedValue.ToString();

                VALIDACIONES = true;
                obtenerDocumentosRelacionados();
                obtenerXMLTipoServicio();
                ValidarCarga();
                ValidarAlcancedeViaje();
                ene_GuiaRemisionTransportista registrar = new ene_GuiaRemisionTransportista();

                if (VALIDACIONES)
                {
                    if (entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Emisor_RazonSocial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Emisor_Ubigeo_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Emisor_CodigoPais_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Remitente_RazonSocial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Destinatario_NumeroDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Destinatario_TipoDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Destinatario_RazonSocial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Generales_FechaEmision_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Generales_HoraEmision_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Generales_Serie_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_PesoBruto_CodigoUnidadMedida_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion == null && esTercero == "P") { VALIDACIONES = false; MessageBox.Show("Tracto no tiene Tarjeta de Circulacion, favor agregar.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
                    if (txtObservaciones.Text == "") { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_PuntoPartida_Departamento == null) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_PuntoDestino_Departamento == null) { VALIDACIONES = false; }
                    if (txtOT.Text.Length == 0 ) { VALIDACIONES = false; }
                    if (cbxSerieGuia.Items.Count == 0) { VALIDACIONES = false; }
                    if (entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia == 0) { VALIDACIONES = false; MessageBox.Show("Direccion de Empresa no existe en el Sistema, Comuniarse con Contabilidad", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    if (entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia == 0) { VALIDACIONES = false; MessageBox.Show("Direccion de Empresa no existe en el Sistema, Comuniarse con Contabilidad", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    if (entGuiaTransportista.LineaOT == 0) { VALIDACIONES = false; MessageBox.Show("LineaOT No se ha seleccionado , favor seleccionar una OT", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);  }

                    if (VALIDACIONES)
                    {
                        registrar.at_ControlOtorgamiento = entGuiaTransportista.entGRT_ControlOtorgamiento_Estado_M;
                        registrar.ent_TransportistaGRT = new en_TransportistaGRT();
                        registrar.ent_TransportistaGRT.at_NumeroDocumentoIdentidad = entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M;
                        registrar.ent_TransportistaGRT.at_RazonSocial = entGuiaTransportista.entGRT_Emisor_RazonSocial_M;
                        //registrar.ent_TransportistaGRT.at_NombreComercial = entGuiaTransportista.entGRT_Emisor_NombreComercial;
                        registrar.ent_TransportistaGRT.at_NumeroMTC = entGuiaTransportista.entGRT_Emisor_NumeroMTC;
                        registrar.ent_TransportistaGRT.at_Telefono = entGuiaTransportista.entGRT_Emisor_Telefono;
                        registrar.ent_TransportistaGRT.at_CorreoContacto = entGuiaTransportista.entGRT_Emisor_CorreoContacto;
                        registrar.ent_TransportistaGRT.at_SitioWeb = entGuiaTransportista.entGRT_Emisor_SitioWeb;
                        registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT = new en_DireccionFiscalTransportistaGRT();
                        registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Ubigeo = entGuiaTransportista.entGRT_Emisor_Ubigeo_M;
                        registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_DireccionDetallada = entGuiaTransportista.entGRT_Emisor_DireccionDetallada;
                        registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Provincia = entGuiaTransportista.entGRT_Emisor_Provincia;
                        registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Departamento = entGuiaTransportista.entGRT_Emisor_Departamento;
                        registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_Distrito = entGuiaTransportista.entGRT_Emisor_Distrito;
                        registrar.ent_TransportistaGRT.ent_DireccionFiscalTransportistaGRT.at_CodigoPais = entGuiaTransportista.entGRT_Emisor_CodigoPais_M;
                        registrar.ent_TransportistaGRT.ent_CorreoGRT = new en_CorreoGRT();
                        registrar.ent_TransportistaGRT.ent_CorreoGRT.at_CorreoPrincipal = entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M;
                        registrar.ent_TransportistaGRT.ent_CorreoGRT.aa_CorreoSecundario = new ArrayOfString();
                        RegistrarCorreosSecundarios(registrar);
                        registrar.ent_RemitenteGRT = new en_RemitenteGRT();
                        registrar.ent_RemitenteGRT.at_NumeroDocumentoIdentidad = entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M;
                        registrar.ent_RemitenteGRT.at_TipoDocumentoIdentidad = entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M;
                        registrar.ent_RemitenteGRT.at_RazonSocial = entGuiaTransportista.entGRT_Remitente_RazonSocial_M;
                        registrar.ent_DestinatarioGRT = new en_DestinatarioGRT();
                        registrar.ent_DestinatarioGRT.at_NumeroDocumentoIdentidad = entGuiaTransportista.entGRT_Destinatario_NumeroDocumentoIdentidad_M;
                        registrar.ent_DestinatarioGRT.at_TipoDocumentoIdentidad = entGuiaTransportista.entGRT_Destinatario_TipoDocumentoIdentidad_M;
                        registrar.ent_DestinatarioGRT.at_RazonSocial = entGuiaTransportista.entGRT_Destinatario_RazonSocial_M;
                        if (TipoTransoporteContratista) // CUANDO PAGA EL SERVICIO UN TERCERO 07 - NO ES SUBCONTRATADO NI REMITENTE
                        {
                            registrar.ent_ContratistaGRT = new en_ContratistaGRT();
                            registrar.ent_ContratistaGRT.at_NumeroDocumentoIdentidad = entGuiaTransportista.entGRT_Contratista_NumeroDocumentoIdentidad;
                            registrar.ent_ContratistaGRT.at_TipoDocumentoIdentidad = entGuiaTransportista.entGRT_Contratista_TipoDocumentoIdentidad;
                            registrar.ent_ContratistaGRT.at_RazonSocial = entGuiaTransportista.entGRT_Contratista_RazonSocial;
                        }
                        if (TipoTransoporteSubcontratado) // CUANDO SE CONTRATA ANA UNIDAD DE TERCERO
                        {
                            registrar.ent_EmpresaSubcontratadaGRT = new en_EmpresaSubcontratadaGRT();
                            registrar.ent_EmpresaSubcontratadaGRT.at_NumeroDocumentoIdentidad = entGuiaTransportista.entGRT_SubContratista_NumeroDocumentoIdentidad;
                            registrar.ent_EmpresaSubcontratadaGRT.at_TipoDocumentoIdentidad = entGuiaTransportista.entGRT_SubContratista_TipoDocumentoIdentidad;
                            registrar.ent_EmpresaSubcontratadaGRT.at_RazonSocial = entGuiaTransportista.entGRT_SubContratista_RazonSocial;
                        }
                        registrar.ent_DatosGeneralesGRT = new en_DatosGeneralesGRT();
                        registrar.ent_DatosGeneralesGRT.at_FechaEmision = entGuiaTransportista.entGRT_Generales_FechaEmision_M;
                        registrar.ent_DatosGeneralesGRT.at_HoraEmision = entGuiaTransportista.entGRT_Generales_HoraEmision_M;
                        registrar.ent_DatosGeneralesGRT.at_Serie = entGuiaTransportista.entGRT_Generales_Serie_M;
                        registrar.ent_DatosGeneralesGRT.aa_Observacion = new ArrayOfString();
                        //registrar.ent_DatosGeneralesGRT.aa_Observacion.Add(txtObservaciones.Text);


                        //if (cbxTipoDocumentoFiscal.Text != "Otros") 
                        //{
                            CargarDocumentosRelacion(registrar);
                        //}
                            
                       
                        
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT = new en_InformacionTrasladoGRT();
                        CargarTipoServicios(registrar);
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.at_FechaInicio = entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT = new en_InformacionPesoBrutoGRT();
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT.at_Peso = entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT.at_UnidadMedida = entGuiaTransportista.entGRT_PesoBruto_CodigoUnidadMedida_M;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT.aa_DescripcionAdicional = new ArrayOfString();

                        if (TipoTrasladoTotaldeBienes)
                        {
                            if (dgvDocumentosRelacionados.Rows.Count > 0)
                            {
                                for (int i = 0; i < dgvDocumentosRelacionados.Rows.Count; i++)
                                {
                                    if (dgvDocumentosRelacionados.Rows[i].Cells["NumeroDocRelacion"].Value.ToString().Substring(0, 1) == "T" )
                                    {
                                        esGuiaElectronica = true;
                                    }
                                }
                            }

                            if (esGuiaElectronica == false)
                            {
                                registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_InformacionPesoBrutoGRT.aa_DescripcionAdicional.Add(entGuiaTransportista.entGRT_PesoBruto_DescripcionAdicional);
                            }
                                
                        }

                        
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT = new en_PuntoPartidaGRT();
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Ubigeo = entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_DireccionCompleta = entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Departamento = entGuiaTransportista.entGRT_PuntoPartida_Departamento;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Provincia = entGuiaTransportista.entGRT_PuntoPartida_Provincia;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoPartidaGRT.at_Distrito = entGuiaTransportista.entGRT_PuntoPartida_Distrito;

                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT = new en_PuntoLlegadaGRT();
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Ubigeo = entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_DireccionCompleta = entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Departamento = entGuiaTransportista.entGRT_PuntoDestino_Departamento;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Provincia = entGuiaTransportista.entGRT_PuntoDestino_Provincia;
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.ent_PuntoLlegadaGRT.at_Distrito = entGuiaTransportista.entGRT_PuntoDestino_Distrito;

                        CargarTipoViaje();
                        CargarVehiculos(registrar);
                        CargarConductores(registrar);

                        if (TipoTrasladoTotaldeBienes == false)
                        {
                            CargarBienes(registrar);
                        }
                        else
                        {
                            ServiceGRT_QA.en_BienesGRT entBienes = new en_BienesGRT();
                            entBienes.aa_Descripcion = new ArrayOfString();
                            entBienes.aa_Descripcion.Add(Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaTransportista.xml_entGRT_Productos_Bienes).Rows[0]["Descripcion"].ToString());
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_BienesGRT = new ArrayOfEn_BienesGRT();
                         
                        }

                        // GENERO Y ENVIAR LA GUIA A TCI
                        if (VALIDACIONES)
                        {
                            entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(lblNumero.Text);
                            entGuiaTransportista.viaje = entGuiaTransportista.viaje.Replace("(C)","");
                            entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "ACEPTADO";


                            if (MessageBox.Show("La guia se enviará a SUNAT, ¿Desea Continuar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                            {
                                return;
                            }
                             
                                this.Cursor = Cursors.WaitCursor;
                            // GUARDO LA GUIA APROBADA Y GENERO EL VIAJE CON EL CORRELATIVO ASIGNADO
                            if (clsOperacionesBL.Instancia.ReportesApp_RegistrarGuiaElectronica(ref entGuiaTransportista))
                            {
                                esRegistroExitoso = true;
                                ListarOTs();
                                registrar.ent_DatosGeneralesGRT.at_Numero = entGuiaTransportista.entGRT_Generales_Numero_M;
                                txtCodviaje.Text = entGuiaTransportista.viaje;
                                txtCodviaje.Tag = entGuiaTransportista.idviaje;
                                registrar.ent_DatosGeneralesGRT.aa_Observacion.Add("Viaje:" + txtCodviaje.Text.ToString() + " / "+ entGuiaTransportista.TipoViaje+ " - "+ txtObservaciones.Text); 
                                lblNumero.Text = entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8");
                                dgvListaGuiaTraspExpressVista.SetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje", entGuiaTransportista.viaje);
                                DesactivarViajeCompleto();
                                respuesta = new ens_Respuesta();
                                request = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
                                respuesta = request.RegistrarGRT(registrar);
                                //request.Close();
                             if (respuesta.at_NivelResultado) // INDICA SI SE ACEPTÓ LA GUIA POR SUNAT : (1) ACEPTADO | (0) RECHAZADO
                                {


                                    //SerializarSOAP(registrar);
                                    // ******************  GENERA LA INSTANCIA CON LOS DATOS DE LA GUIA INDIVIDUAL *************
                                    ConsultarGuiaIndividual(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);
                                    entGuiaTransportista.entGRT_Respuesta_MensajeResultado = respuesta.at_MensajeResultado;
                                    entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = respuesta.at_CodigoError.ToString();


                                    entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "ACEPTADO";
                                    txtCodigoHash.Text = respuesta.at_CodigoHash;
                                    entGuiaTransportista.entGRT_Respuesta_CodigoHash = respuesta.at_CodigoHash;

                                    // consultaIndividual.at_NivelResultado : Valor negativo representa que existe un error, 0 que no se encontró guia , 1 comprobante encontrado
                                    if (entGuiaTransportista.entGRT_Respuesta_NivelResultado == 1)
                                    {
                                            
                                        entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";
                                        tabControl1.Enabled = false;
                                        MessageBox.Show(respuesta.at_MensajeResultado + "\n SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Cursor = Cursors.Default;
                                        backgroundWorker1.RunWorkerAsync();
                                    }
                                    else
                                    {
                                        if (entGuiaTransportista.entGRT_Respuesta_NivelResultado == 0)
                                        {
                                            entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "NO ENCONTRADO";
                                            entGuiaTransportista.entGRT_Respuesta_MensajeResultado = "No se encontro guia indicada, favor verificar si se guardó correctamente";
                                            entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = "0";
                                        }
                                        if (entGuiaTransportista.entGRT_Respuesta_NivelResultado < 0)
                                        {
                                            entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "ERROR DE CONSULTA";

                                            if (consultaIndividual.ent_InformacionComprobante.l_respuestas.Count > 0)
                                            {
                                                entGuiaTransportista.entGRT_Respuesta_MensajeResultado = CDR_Respuesta_Descripcion;//consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion;
                                                entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta;
                                            }
                                        }
                                        GenerarLog();
                                    }
                                }
                                else
                                {
                                        
                                    entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "NO EXISTE";
                                    entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "PENDIENTE";
                                    entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = "03";//respuesta.at_CodigoError.ToString();
                                    entGuiaTransportista.entGRT_Respuesta_MensajeResultado = respuesta.at_MensajeResultado;
                                    entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "PENDIENTE";
                                    clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaTransportista);
                                    MessageBox.Show("SUNAT:"+respuesta.at_MensajeResultado.ToString() + " SQL: " + Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        
                                }

                            
                        }
                        else
                        {
                            //request.Close();
                            esRegistroExitoso = false;
                            this.Cursor = Cursors.Default; 
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        }
                    }


                }
                else
                {

                    /*using (StreamWriter log = new StreamWriter(CarpetaAlacenamientoLogErrores + "Error_Guia_Serie_" + entGuiaTransportista.entGRT_Generales_Serie_M + "_Numero_" + entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt"))
                    {
                        log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Algunos datos del formulario no fueron llenados: " + "\n SQL: " + Utilitario.Instancia.Advertencia);
                    }*/
                    MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + "Algunos datos del formulario no fueron llenados o contienen datos incorrectos: "  +Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                /*using (StreamWriter log = new StreamWriter(CarpetaAlacenamientoLogErrores + "Error_Guia_Serie_" + entGuiaTransportista.entGRT_Generales_Serie_M + "_Numero_" + entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt"))
                {
                    log.WriteLine(ex.ToString() + "\n SQL: ");
                }*/

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void DesactivarViajeCompleto()
        {
            if (txtCodviaje.Text.Length > 1)
            {
                checkCompletado.Checked = false;
                checkCompletado.Enabled = false;
            }

        }

        private void ValidarAlcancedeViaje()
        {
            if (checkCompletado.Checked)
            {
                if (MessageBox.Show("A seleccionado la opcion 'VIAJE COMPLETO' , Si continua ya no se podrá generar CONSOLIDADOS Para este VIAJE , ¿Desea Continuar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    entGuiaTransportista.CompletadoViaje = 1; // (1) GENERAR COMPLETADO (0) NO GENERAR COMPELTADO
                }
                else
                {
                    entGuiaTransportista.CompletadoViaje = 0; // (1) GENERAR COMPLETADO (0) NO GENERAR COMPELTADO
                }
            }
            else
            {
                entGuiaTransportista.CompletadoViaje = 0; // (1) GENERAR COMPLETADO (0) NO GENERAR COMPELTADO
            }

            if (TipoProgramacion == "LIMAGAS" && (cbxSerieGuia.Text != "V017" && cbxSerieGuia.Text != "V016" && cbxSerieGuia.Text != "V018"))
            {
                Utilitario.Instancia.Advertencia = "La Serie seleccionada no le pertenece a la Programacion LIMAGAS";
                VALIDACIONES = false;
            }
            if (TipoProgramacion == "LINDLEY" && (cbxSerieGuia.Text != "V011"))
            {
                Utilitario.Instancia.Advertencia = "La Serie seleccionada no le pertenece a la Programacion LINDLEY";
                VALIDACIONES = false;
            }
            if (TipoProgramacion == "GENERAL" && (Convert.ToInt32(txtCliente.Tag) == 20323) && (cbxSerieGuia.Text) != "V011")
            {
                Utilitario.Instancia.Advertencia = "La Serie seleccionada no le pertenece a la Programacion LINDLEY";
                VALIDACIONES = false;
            }
            if (TipoProgramacion == "TOLVAS" && (cbxSerieGuia.Text != "V013" && cbxSerieGuia.Text != "V015"))
            {
                Utilitario.Instancia.Advertencia = "La Serie seleccionada no le pertenece a la Programacion TOLVAS";
                VALIDACIONES = false;
            }
            if (TipoProgramacion == "LOCAL" && (cbxSerieGuia.Text != "V017" && cbxSerieGuia.Text != "V016" && cbxSerieGuia.Text != "V001"))
            {
                Utilitario.Instancia.Advertencia = "La Serie seleccionada no le pertenece a la Programacion LOCAL";
                VALIDACIONES = false;
            }
            if (TipoProgramacion == "VOLCAN" && (cbxSerieGuia.Text != "V019"  && cbxSerieGuia.Text != "V001"))
            {
                Utilitario.Instancia.Advertencia = "La Serie seleccionada no le pertenece a la Programacion VOLCAN";
                VALIDACIONES = false;
            }
        }


        private void SerializarSOAP(ene_GuiaRemisionTransportista registrar)
        {



            var path = CarpetaLogSoapError + entGuiaTransportista.entGRT_Generales_Serie_M +"-"+ entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8");

            if (Directory.Exists(path))
            {
                System.IO.FileStream file = System.IO.File.Create(path);
                XmlSerializer serializador = new XmlSerializer(typeof(ene_GuiaRemisionTransportista));
                StringBuilder sb = new StringBuilder();
                TextWriter tw = new StringWriter(sb);
                serializador.Serialize(file, registrar);
                file.Close();
            }



        }

        private void CargarTipoViaje()
        {
            if (rbtSalida.Checked)
            {
                entGuiaTransportista.TipoViaje = "SALIDA";
            }
            else
            {
                entGuiaTransportista.TipoViaje = "RETORNO";
            }
        }


        private void GenerarLog()
        {


         
                if (entGuiaTransportista.entGRT_Respuesta_NivelResultado == 1)
                {
                    if (consultaIndividual.ent_InformacionComprobante.l_respuestas.Count > 0)
                    {
                       
                        MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= " + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta + "\n Mensaje=" + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        
                        MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= 1" + "Guia encontrada - ACEPTADA " + "\n Mensaje=" + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
         
                }

                if (entGuiaTransportista.entGRT_Respuesta_NivelResultado == 0)
                {
                   
                    MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= 0 " + respuesta.at_CodigoError.ToString() + "\n Mensaje=" + "No se encontró Guia Consultada", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (entGuiaTransportista.entGRT_Respuesta_NivelResultado < 0)
                {
                   
                    MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= " + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta + "\n Mensaje=" + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            
           

        }

        private void CargarRespuestaSunatCDR()
        {


            if (consultaIndividual.at_NivelResultado == 1) // UNO SIGNIFICA QUE SI ENCONTRO LA RESPUESTA DEL CDR
            {
                ens_ConsultarXML Respuesta_XML_CDR = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, Convert.ToInt32(dtRespuestaSunat_Guardado.Rows[0]["NroRespuesta"]));
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
                            entGuiaTransportista.entGRT_Respuesta_URL_GuiaSunat = dtRespuestaXML_CDR.Rows[0]["Descripcion"].ToString();
                            xmlCDR = Utilitario.Instancia.DatatableToXml(dtRespuestaXML_CDR);
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
            
               
                    entGuiaTransportista.entGRT_Respuesta_Xml_CDR = Encoding.UTF8.GetString(Respuesta_XML_CDR.ent_ResultadoXML.at_XML);
                    entGuiaTransportista.entGRT_Respuesta_Fecha_CDR = Respuesta_XML_CDR.ent_ResultadoXML.at_FechaXML;

                }
            }

        }



        private DataTable obtenerRespuestaGuardado()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta.Columns.Add("NroRespuesta", typeof(String));
            dtRespuesta.Columns.Add("CodigoRespuesta", typeof(String));
            dtRespuesta.Columns.Add("Descripcion", typeof(String));
            dtRespuesta.Columns.Add("FechaSunat", typeof(String));

            
            //CONSULTAR RESPUESTA
            ServiceGRT_QA.ene_ConsultarRespuesta consultarRespuestaTransportista = new ServiceGRT_QA.ene_ConsultarRespuesta();
            consultarRespuestaTransportista.at_CantidadConsultar = 1;
            consultarRespuestaTransportista.at_NumeroDocumentoIdentidad = "20439331918";
            ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient requestTransportista = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
            ServiceGRT_QA.ens_ConsultarRespuesta response = requestTransportista.ConsultarRespuestaGRT(consultarRespuestaTransportista);
            
            //CONFIRMAR RESPUESTA
            ServiceGRT_QA.ene_ConfirmarRespuesta empresa = new ene_ConfirmarRespuesta();
            empresa.at_NumeroDocumentoIdentidad ="20439331918";
            empresa.l_Comprobante = new ArrayOfEn_ComprobanteConfirmarRespuesta();
          
            ens_ConfirmarRespuesta responseConfirmar = requestTransportista.ConfirmarRespuestaGRT(empresa);

            for (int i = 0; i < response.l_ResultadoRespuestaComprobante.Count; i++)
            {
                if(entGuiaTransportista.entGRT_Generales_Numero_M == response.l_ResultadoRespuestaComprobante[i].at_Numero &&
                    entGuiaTransportista.entGRT_Generales_Serie_M == response.l_ResultadoRespuestaComprobante[i].at_Serie)
                {
                    String TipoRespuesta = response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_TipoRespuesta.ToString();
                    String CodigoRespuesta = response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_CodigoRespuesta.ToString();
                    String fechaSunat = response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_FechaRespuesta;

                    if (response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_Mensaje != null)
                    {
                        for (int j = 0; j < response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_Mensaje.Count; j++)
                        {
                            String DescripcionRespuesta = response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_Mensaje[j].ToString();
                            dtRespuesta.Rows.Add(CodigoRespuesta, TipoRespuesta, DescripcionRespuesta, fechaSunat);
                        }
                    }
                    else
                    {
                        dtRespuesta.Rows.Add(CodigoRespuesta, TipoRespuesta, "SUANT NO ENVIÓ MENSAJE DE RESPUESTA (NULL)", fechaSunat);
                    }
                    
                }
                
                ServiceGRT_QA.en_ComprobanteConfirmarRespuesta ConfirmarRpta = new en_ComprobanteConfirmarRespuesta();
                ConfirmarRpta.at_Serie = response.l_ResultadoRespuestaComprobante[i].at_Serie;
                ConfirmarRpta.at_Numero = response.l_ResultadoRespuestaComprobante[i].at_Numero;
                ConfirmarRpta.at_CodigoRespuesta = response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_CodigoRespuesta;
                empresa.l_Comprobante.Add(ConfirmarRpta);

            }

            requestTransportista.ConfirmarRespuestaGRT(empresa);


  
               /* if (lista.Count > 0)
                {
                    foreach (en_Respuestas item in lista)
                    {
                        
                        dtRespuesta.Rows.Add(item.at_NroRespuesta, item.at_CodigoRespuesta, item.at_Descripcion, item.at_FechaSunat);
                    }
                }
                else { dtRespuesta = null; }*/


            return dtRespuesta;
        }

        private void CargarBienes(ene_GuiaRemisionTransportista registrar)
        {

            if (dgvProductosGuia.Rows.Count > 0)
            {
                
                //registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT = new ArrayOfEn_ConductorGRT();
                registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_BienesGRT = new ArrayOfEn_BienesGRT();
             
                for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
                {
                    en_BienesGRT entBienes = new en_BienesGRT();
                    entBienes.aa_Descripcion = new ArrayOfString();

                    
                    entBienes.aa_Descripcion.Add(dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString());
                    entBienes.at_Codigo = dgvProductosGuia.Rows[i].Cells["Codigo"].Value.ToString();
                    entBienes.at_Cantidad = Convert.ToDecimal(dgvProductosGuia.Rows[i].Cells["Cantidad"].Value);
                    entBienes.at_UnidadMedida = dgvProductosGuia.Rows[i].Cells["UnidadMedida"].Value.ToString();
                    
                   
                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_BienesGRT.Add(entBienes);
                }
            }
            else
            {
                VALIDACIONES = false;
                MessageBox.Show("Favor agregar al menos un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }

        private void CargarConductores(ene_GuiaRemisionTransportista registrar)
        {
            if (TipoTrasladoProgramado)
            {
                if (dgvTrasladoProgramado.Rows.Count > 0)
                {
                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT = new ArrayOfEn_ConductorGRT();

                    for (int i = 0; i < dgvTrasladoProgramado.Rows.Count; i++)
                    {

                        ServiceGRT_QA.en_ConductorGRT entConductorGuia = new en_ConductorGRT();
                        entConductorGuia.at_TipoDocumentoIdentidad = dgvTrasladoProgramado.Rows[i].Cells["TipoDocumento"].Value.ToString();
                        entConductorGuia.at_NumeroDocumentoIdentidad = dgvTrasladoProgramado.Rows[i].Cells["Documento"].Value.ToString().TrimEnd(); 
                        entConductorGuia.at_Licencia = dgvTrasladoProgramado.Rows[i].Cells["Brevete"].Value.ToString().TrimEnd(); 
                        if (dgvTrasladoProgramado.Rows[i].Cells["Nombres"].Value.ToString() == " ")
                        {
                            MessageBox.Show("Campo de la Columna Nombre vacio, verificar Columnas ocultas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            VALIDACIONES = false;
                            break;
                        }
                        if (dgvTrasladoProgramado.Rows[i].Cells["Apellidos"].Value.ToString() == " ")
                        {
                            MessageBox.Show("Campo de la Columna Apellido vacio, verificar Columnas ocultas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            VALIDACIONES = false;
                            break;
                        }
                        entConductorGuia.at_Nombres = dgvTrasladoProgramado.Rows[i].Cells["Nombres"].Value.ToString();
                        entConductorGuia.at_Apellidos = dgvTrasladoProgramado.Rows[i].Cells["Apellidos"].Value.ToString();
                        
                        
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT.Add(entConductorGuia);

                    }

                }
                else
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No se agregó ningun vehiculo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (entNuevoConductor.entGRT_Conductor_TipoDocumentoIdentidad_M.Length == 0 || entNuevoConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M.Length == 0 ||
                    entNuevoConductor.entGRT_Conductor_Licencia_M.Length == 0 || entNuevoConductor.entGRT_Conductor_Nombres_M.Length == 0 || entNuevoConductor.entGRT_Conductor_Apellidos_M.Length == 0)
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No se cargaron todo los datos del conductor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    ServiceGRT_QA.en_ConductorGRT entConductorGuia = new en_ConductorGRT();
                    entConductorGuia.at_TipoDocumentoIdentidad = entNuevoConductor.entGRT_Conductor_TipoDocumentoIdentidad_M;
                    entConductorGuia.at_NumeroDocumentoIdentidad = entNuevoConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M;
                    if (entNuevoConductor.entGRT_Conductor_Nombres_M == null || entNuevoConductor.entGRT_Conductor_Nombres_M == "")
                    {
                        MessageBox.Show("Campo de la Columna Nombre vacio, verificar Columnas ocultas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        VALIDACIONES = false;
                        
                    }
                    if (entNuevoConductor.entGRT_Conductor_Apellidos_M == null && entNuevoConductor.entGRT_Conductor_Apellidos_M == "")
                    {
                        MessageBox.Show("Campo de la Columna Apellido vacio, verificar Columnas ocultas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        VALIDACIONES = false;
                       
                    }
                    entConductorGuia.at_Licencia = entNuevoConductor.entGRT_Conductor_Licencia_M.TrimEnd();
                    entConductorGuia.at_Nombres = entNuevoConductor.entGRT_Conductor_Nombres_M;
                    entConductorGuia.at_Apellidos = entNuevoConductor.entGRT_Conductor_Apellidos_M;
                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT = new ArrayOfEn_ConductorGRT();
                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT.Add(entConductorGuia);

                }

            }

            //registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_ConductorGRT

        }

        private void CargarVehiculos(ene_GuiaRemisionTransportista registrar)
        {
            if(entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null) 
            {
                if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length < 9)
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Formato de la tarjeta de Cirulacion es incorrecto:" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (esTercero == "P")
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Tarjeta de Circulacion no registrada, comuncarse con control documentario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }    

            }


            if (TipoProgramacion != "LOCAL")
            {


                if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta != null)
                {

                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.Length < 9)
                    {

                        VALIDACIONES = false;
                        MessageBox.Show("Tarjeta de Circulacion de la carreta mal tiene un formato incorrecto: " + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                else
                {

                    if (TipoVehiculoCarreta != "CISTERNA" && esTerceroCarreta == "P")
                    {
                        VALIDACIONES = false;
                        MessageBox.Show("Tarjeta de Circulacion de la Carreta " + txtCarreta.Text + " no fue registrado, comuncarse con Control Documentario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }

            if (TipoTrasladoProgramado)
            {
                if (dgvTrasladoProgramado.Rows.Count > 0)
                {

                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT = new ArrayOfEn_VehiculoGRT();

                    for (int i = 0; i < dgvTrasladoProgramado.Rows.Count; i++)
                    {

                        en_VehiculoGRT entVehiculo = new en_VehiculoGRT();
                        entVehiculo.at_NumeroPlaca = dgvTrasladoProgramado.Rows[i].Cells["Placa"].Value.ToString().TrimEnd();

                        en_VehiculoGRT entCarreta = new en_VehiculoGRT();
                        entCarreta.at_NumeroPlaca = dgvTrasladoProgramado.Rows[i].Cells["Carreta"].Value.ToString().TrimEnd();

                        if (clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica(dgvTrasladoProgramado.Rows[i].Cells["Placa"].Value.ToString()).Rows[0]["TarjetaCirculacion"].ToString() != "")
                        {
                            String TarjetaCirculacio = clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica(dgvTrasladoProgramado.Rows[i].Cells["Placa"].Value.ToString()).Rows[0]["TarjetaCirculacion"].ToString();
                            entVehiculo.at_TarjetaCirculacion = TarjetaCirculacio.Length == 9 ? "0" + TarjetaCirculacio : TarjetaCirculacio;
                        }

                        if (clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica(dgvTrasladoProgramado.Rows[i].Cells["Carreta"].Value.ToString()).Rows[0]["TarjetaCirculacion"].ToString() != "")
                        {
                            String TarjetaCirculacioCarreta = clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica(dgvTrasladoProgramado.Rows[i].Cells["Carreta"].Value.ToString()).Rows[0]["TarjetaCirculacion"].ToString();
                            entCarreta.at_TarjetaCirculacion = TarjetaCirculacioCarreta.Length == 9 ? "0" + TarjetaCirculacioCarreta : TarjetaCirculacioCarreta;
                        }
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT.Add(entVehiculo);

                        if (entCarreta.at_NumeroPlaca.Length == 6)
                        {
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT.Add(entCarreta);
                        }
                        else if (entCarreta.at_NumeroPlaca.Length > 0 && entCarreta.at_NumeroPlaca.Length < 6)
                        {
                            VALIDACIONES = false;
                            MessageBox.Show("Formato de Carreta es incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
 
                        
                    }

                }
                else
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No se agregó ningun vehiculo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (txtPlaca.Text.Length > 0)
                {
                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT = new ArrayOfEn_VehiculoGRT();
                    ServiceGRT_QA.en_VehiculoGRT entVehiculo = new en_VehiculoGRT();
                    entVehiculo.at_NumeroPlaca = txtPlaca.Text.TrimEnd();

                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null)
                    {
                        
                        entVehiculo.at_TarjetaCirculacion = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length == 9 ? "0"+entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;
                    }

                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT.Add(entVehiculo);

                    if (txtCarreta.Text.Length > 0)
                    {
                        ServiceGRT_QA.en_VehiculoGRT entCarreta = new en_VehiculoGRT();
                        entCarreta.at_NumeroPlaca = txtCarreta.Text.TrimEnd();
                        if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta != null)
                        {
                            if (TipoVehiculoCarreta != "CISTERNA" && esTerceroCarreta == "P")
                            {
                                entCarreta.at_TarjetaCirculacion = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.Length == 9 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta;
                            }
                            
                        }
                        registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.l_VehiculoGRT.Add(entCarreta);
                    }
                    else if (txtCarreta.Text.Length > 0 && txtCarreta.Text.Length < 6 && TipoProgramacion != "LOCAL")
                    {
                        VALIDACIONES = false;
                        MessageBox.Show("Formato de Carreta es incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No se agregó ningun vehiculo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarTipoServicios(ene_GuiaRemisionTransportista registrar)
        {
            if (entGuiaTransportista.xml_entGRT_TipoServicio.Length > 0)
            {
                DataTable dtTipServicioGuia = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaTransportista.xml_entGRT_TipoServicio);
                if (dtTipServicioGuia.Rows.Count > 0)
                {
                    registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.aa_IndicadorServicio = new ArrayOfString();

                    for (int i = 0; i < dtTipServicioGuia.Rows.Count; i++)
                    {
                        if (dtTipServicioGuia.Rows[i]["CodServicio"].ToString() != "00")
                        {
                            registrar.ent_DatosGeneralesGRT.ent_InformacionTrasladoGRT.aa_IndicadorServicio.Add(dtTipServicioGuia.Rows[i]["CodServicio"].ToString());
                        }
                        
                    }
                }
                else
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No se agregó ningun Tipo de Servicio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void CargarDocumentosRelacion(ene_GuiaRemisionTransportista registrar)
        {
            bool esGuia = false;
            en_DocumentoRelacionadoGRT entDocumentosRelacionados;

            if (dgvDocumentosRelacionados.Rows.Count > 0)
            {

                if (Convert.ToString(dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value).Length > 5 /*&& char.IsLetter(char.Parse(dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value.ToString().Substring(0, 1)))*/)
                {
                    for (int j = 0; j < dgvDocumentosRelacionados.Rows.Count; j++ )
                    {
                        if (dgvDocumentosRelacionados.Rows[j].Cells["NombreDocumento"].Value.ToString() != "Otros")
                        {
                            esGuia = true;
                            registrar.ent_DatosGeneralesGRT.l_DocumentoRelacionadoGRT = new ArrayOfEn_DocumentoRelacionadoGRT();
                            break;
                        }
                    }

                    if (esGuia)
                    {
                        for (int i = 0; i < dgvDocumentosRelacionados.Rows.Count; i++)
                        {
                            if (dgvDocumentosRelacionados.Rows[i].Cells["NombreDocumento"].Value.ToString() != "Otros")
                            {
                                entDocumentosRelacionados = new en_DocumentoRelacionadoGRT();
                                entDocumentosRelacionados.at_NumeroComprobante = dgvDocumentosRelacionados.Rows[i].Cells["NumeroDocRelacion"].Value.ToString();
                                entDocumentosRelacionados.at_TipoComprobante = dgvDocumentosRelacionados.Rows[i].Cells["TipoDocRelacion"].Value.ToString();
                                entDocumentosRelacionados.at_NombreComprobante = dgvDocumentosRelacionados.Rows[i].Cells["NombreDocumento"].Value.ToString();
                                entDocumentosRelacionados.at_NumeroDocumentoIdentidad = dgvDocumentosRelacionados.Rows[i].Cells["NumeroDocumentoIdentidad"].Value.ToString();
                                entDocumentosRelacionados.at_TipoDocumentoIdentidad = dgvDocumentosRelacionados.Rows[i].Cells["TipoDocumentoIdentidad"].Value.ToString();
                                registrar.ent_DatosGeneralesGRT.l_DocumentoRelacionadoGRT.Add(entDocumentosRelacionados);
                            }

                        }
                    }
            

                }
                else
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No se agregó ningun documento relacionado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
            {
                VALIDACIONES = false;
                MessageBox.Show("No se agregó ningun documento relacionado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private bool RegistrarCorreosSecundarios(ene_GuiaRemisionTransportista registrar)
        {
            Boolean estado = false;
            List<String> correosSecundarios = new List<string>();
            DataTable dtCorreoSecundarioEnvio = new DataTable();
            if (entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario != null)
            {
                estado = true;
                if (entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario.Length > 0)
                {
                    dtCorreoSecundarioEnvio = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario);
                }

                if (dtCorreoSecundarioEnvio.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCorreoSecundarioEnvio.Rows.Count; i++)
                    {
                        registrar.ent_TransportistaGRT.ent_CorreoGRT.aa_CorreoSecundario.Add(dtCorreoSecundarioEnvio.Rows[i]["Correo"].ToString());
                    }

                }
                else
                {
                    estado = false;
                }
            }

            return estado;

        }



        private void obtenerDocumentosRelacionados()
        {
            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                if (txtDocRelacionAnexar.TextLength > 0 )
                {
                    if (checkDocRelacion.Checked == false)
                    {
                        dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value = txtDocRelacionAnexar.Text;
                    }

                    
                }
                /*else
                {
                    entGuiaTransportista.xml_entGRT_DocumentosRelacion = null;
                    for (int i = 0; i < dgvDocumentosRelacionados.Rows.Count; i++)
                    {
                        dgvDocumentosRelacionados.Rows.RemoveAt(i);
                    }
                }*/
            }


            if (dgvDocumentosRelacionados.Rows.Count > 0)
            {
                if (dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value.ToString().Length > 5 /*&& char.IsLetter(char.Parse(dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value.ToString().Substring(0, 1)))*/)
                {
                    entGuiaTransportista.xml_entGRT_DocumentosRelacion = Utilitario.Instancia.QuitarTildes(Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvDocumentosRelacionados)));
                }
                else
                {
                    MessageBox.Show("El documento relacionado ingresado tiene un formato incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    VALIDACIONES = false;
                }
            }
            else
            {
                MessageBox.Show("No se agregó ningun documento relacionado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                VALIDACIONES = false;
            }
        }

        private void ValidarCarga()
        {
            entGuiaTransportista.entGRT_Generales_Observacion = txtObservaciones.Text;
            entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M = Convert.ToDateTime(dtpFechaTraslado.Text).ToString("yyyy-MM-dd");
           // entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M = Convert.ToDecimal(txtPesoTotal.Value);

            entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;
            entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;

            string[] UbigeoDireccionPartida = DireccionUbigeoOrigen.Split(',');
            string[] UbigeoDireccionDestino = DireccionUbigeoFin.Split(',');

            if (txtDireccionPartida.Text.Length > 0 && txtDireccionDestino.Text.Length > 0)
            {
                entGuiaTransportista.entGRT_PuntoPartida_Departamento = UbigeoDireccionPartida[0];
                entGuiaTransportista.entGRT_PuntoPartida_Provincia = UbigeoDireccionPartida[1];
                entGuiaTransportista.entGRT_PuntoPartida_Distrito = UbigeoDireccionPartida[2];

                entGuiaTransportista.entGRT_PuntoDestino_Departamento = UbigeoDireccionDestino[0];
                entGuiaTransportista.entGRT_PuntoDestino_Provincia = UbigeoDireccionDestino[1];
                entGuiaTransportista.entGRT_PuntoDestino_Distrito = UbigeoDireccionDestino[2];
            }
            else
            {
                VALIDACIONES = false;
            }




            if (txtTituloAdicionalGrupo.Text.Length > 0 && txtEtiquetaGrupo.Text.Length > 0 && txtContenidoGrupo.Text.Length > 0)
            {
                entGuiaTransportista.entGRT_GrupoInformacionAdicional_Titulo = txtTituloAdicionalGrupo.Text;
                entGuiaTransportista.entGRT_GrupoInformacionAdicional_Etiqueta = txtEtiquetaGrupo.Text;
                entGuiaTransportista.entGRT_GrupoInformacionAdicional_Valor = txtContenidoGrupo.Text;
            }


            if (txtDireccionPartida.Text.Length == 0 && txtDireccionDestino.Text.Length == 0)
            {
                VALIDACIONES = false;
            }


            if (Convert.ToDecimal(txtPesoTotal.Value) <= 0)
            {
                Utilitario.Instancia.Advertencia = " El peso total no puede ser 0";
                VALIDACIONES = false;
            }
            else
            {
                entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M = Math.Round(Convert.ToDecimal(txtPesoTotal.Value),3);
            }

            if( TipoTrasladoTotaldeBienes)
            {
                string documentos_asociados = string.Empty;

                for (int i = 0; i < dgvDocumentosRelacionados.Rows.Count; i++)
                {

                   documentos_asociados = documentos_asociados +" " + dgvDocumentosRelacionados.Rows[i].Cells["NumeroDocRelacion"].Value.ToString() + ",";

                }

                entGuiaTransportista.entGRT_PesoBruto_DescripcionAdicional = "LOS BIENES SE REFERENCIAN AL DOCUMENTO RELACIONADO:" + documentos_asociados.TrimEnd(',');;
                txtObservacionCargaTotal.Text = entGuiaTransportista.entGRT_PesoBruto_DescripcionAdicional;
            }

            entGuiaTransportista.entGRT_PesoBruto_CodigoUnidadMedida_M = cbxUnidadMedidaTotal.SelectedValue.ToString();

            if (TipoTransoporteContratista)
            {

                if (txtRucPagadorTercer.Text.Length == 0 || txtRazonSocialPagadorTercero.Text.Length == 0 || txtTipoDocTerceroFlete.Text.Length == 0)
                {
                    MessageBox.Show("Faltan llenar campos de Tercero", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    VALIDACIONES = false;
                }
                else
                {
                    entGuiaTransportista.entGRT_Contratista_RazonSocial = txtRazonSocialPagadorTercero.Text;
                    entGuiaTransportista.entGRT_Contratista_TipoDocumentoIdentidad = txtTipoDocTerceroFlete.Tag.ToString();
                    entGuiaTransportista.entGRT_Contratista_NumeroDocumentoIdentidad = txtRucPagadorTercer.Text;
                }
            }

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                if (dtpFechaTraslado.Value < dtpFechaRegistro.Value)
                {
                    MessageBox.Show("Fecha de Transporte no puede ser menor a la fecha de Registro de Guia", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    VALIDACIONES = false;
                }


            }

            if (entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M.Length == 0)
            {

                MessageBox.Show("Ingrese el correo, Campo obligatorio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                VALIDACIONES = false;
            }

            for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
            {
                if (dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString().Length == 0)
                {
                    MessageBox.Show("En la fila: " + i.ToString() + " ,No ha ingresado nombre del producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    VALIDACIONES = false;
                    break;
                }


            }

            if (cbxSerieGuia.Items.Count == 0)
            {
                VALIDACIONES = false;
                MessageBox.Show("Usted no tiene asignada ninguna Serie, Favor comunicarse con TI", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (cbxUnidadMedidaTotal.SelectedValue.ToString() != "KGM" && cbxUnidadMedidaTotal.SelectedValue.ToString() != "TNE")
            {
                VALIDACIONES = false;
                MessageBox.Show("La unidad de medida total no puede ser diferente a KILOGRAMO O TONELADA", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private DataTable obtenerXMLProductosBienes()
        {

            DataTable dtproductoTotalBienes = new DataTable();

            if (dgvProductosGuia.Rows.Count > 0)
            {
                dtproductoTotalBienes = Utilitario.Instancia.GetContentAsDataTable(dgvProductosGuia);


                if (dtproductoTotalBienes.Rows.Count > 0)
                {

                    IEnumerable<DataRow> ieRegistro = from fila in dtproductoTotalBienes.AsEnumerable()
                                                      where fila.Field<string>("Descripcion") == "TRASLADO TOTAL DE BIENES"
                                                      select fila;

                    if (ieRegistro.Any())
                    {
                        dtproductoTotalBienes = ieRegistro.CopyToDataTable();
                    }
                    else
                    {
                        VALIDACIONES = false;
                        MessageBox.Show("Usted a seleccionado el tipo de Servicio Traslado Total de Bienes, favor verificar que exista el Item 'TRASLADO TOTAL DE BIENES'", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return dtproductoTotalBienes;
        }
        
       

        private void obtenerXMLTipoServicio()
        {

            dtTipoServicio.Rows.Clear();

            for (int i = 0; i < cbxTipoServicios.Properties.Items.Count; i++)
            {
                if (cbxTipoServicios.Properties.Items[i].CheckState == CheckState.Checked /*&& cbxTipoServicios.Properties.Items[i].Description != "Placa y Vehiculo"*/)
                {
                    dtTipoServicio.Rows.Add(cbxTipoServicios.Properties.Items[i].Value.ToString(), cbxTipoServicios.Properties.Items[i].Description);

                }
                if (cbxTipoServicios.Properties.Items[i].CheckState == CheckState.Checked && cbxTipoServicios.Properties.Items[i].Description == "Traslado total de bienes")
                {
                    TipoTrasladoTotaldeBienes = true;
                }
                else if (cbxTipoServicios.Properties.Items[i].Description == "Traslado total de bienes")
                {
                    TipoTrasladoTotaldeBienes = false;
                }
                if (cbxTipoServicios.Properties.Items[i].CheckState == CheckState.Checked && cbxTipoServicios.Properties.Items[i].Description == "Trasbordo Programado")
                {
                    TipoTrasladoProgramado = true;
                }
                else if (cbxTipoServicios.Properties.Items[i].Description == "Trasbordo Programado")
                {
                    TipoTrasladoProgramado = false;
                }
                if (cbxTipoServicios.Properties.Items[i].CheckState == CheckState.Checked && cbxTipoServicios.Properties.Items[i].Description == "Transporte Subcontratado")
                {
                    TipoTransoporteSubcontratado = true;
                }
                else if (cbxTipoServicios.Properties.Items[i].Description == "Transporte Subcontratado")
                {
                    TipoTransoporteSubcontratado = false;
                }

                if (cbxTipoServicios.Properties.Items[i].CheckState == CheckState.Checked && cbxTipoServicios.Properties.Items[i].Description == "Pago de flete de Tercero")
                {
                    TipoTransoporteContratista = true;
                }
                else if (cbxTipoServicios.Properties.Items[i].Description == "Pago de flete de Tercero")
                {
                    TipoTransoporteContratista = false;
                }
            }
            if (dtTipoServicio.Rows.Count > 0)
            {

                entGuiaTransportista.xml_entGRT_TipoServicio = Utilitario.Instancia.DatatableToXml(dtTipoServicio);
            }
            else
            {

                MessageBox.Show("No ha seleccionado ningun tipo de servicio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            if (TipoTrasladoTotaldeBienes)
            {
                entGuiaTransportista.TipoTrasladoTotaldeBienes = TipoTrasladoTotaldeBienes;
                DataTable dtProductos = obtenerXMLProductosBienes();
                if (dtProductos != null)
                {
                    entGuiaTransportista.xml_entGRT_Productos_Bienes = Utilitario.Instancia.DatatableToXml(dtProductos);
                }
                else
                {
                    entGuiaTransportista.xml_entGRT_Productos_Bienes = null;
                }
                
            }
            else
            {
                if (dgvProductosGuia.Rows.Count > 0)
                {
                    entGuiaTransportista.xml_entGRT_Productos_Bienes = Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvProductosGuia));
                }
                else
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Usted no ha seleccionado el item de servicio Carga total de Bienes, por lo tanto debe llenar los items de productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


            if (TipoTrasladoProgramado)
            {

                if (dgvTrasladoProgramado.Rows.Count > 0)
                {
                    entGuiaTransportista.TipoTrasladoProgramado = TipoTrasladoProgramado;
                    entGuiaTransportista.xml_entGTR_Conductor_M = Utilitario.Instancia.QuitarTildes( Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvTrasladoProgramado)));
                }
                else
                {
                    Utilitario.Instancia.Advertencia = "No registró ningun Trasbordo Programado";
                    VALIDACIONES = false;
                }
            }

            if (TipoTransoporteSubcontratado)
            {
                if (txtTipoDocumentoSubContra.Text.Length == 0 || txtRucSubContra.Text.Length == 0 || txtTipoDocumentoSubContra.Text.Length == 0)
                {
                    VALIDACIONES = false;
                }
                else
                {
                    entGuiaTransportista.TipoTransoporteSubcontratado = TipoTransoporteSubcontratado;
                    entGuiaTransportista.entGRT_SubContratista_TipoDocumentoIdentidad = txtTipoDocumentoSubContra.Tag.ToString();
                    entGuiaTransportista.entGRT_SubContratista_NumeroDocumentoIdentidad = txtRucSubContra.Text.ToString();
                    entGuiaTransportista.entGRT_SubContratista_RazonSocial = txtRazonSocialSubContra.Text;
                }

            }


        }

        private void txtEmpresaRemitente_Enter(object sender, EventArgs e)
        {
            txtEmpresaRemitente.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtEmpresaRemitente_Leave(object sender, EventArgs e)
        {
            txtEmpresaRemitente.BackColor = Color.White;
        }


        private void FrmGenerarGuia_Activated(object sender, EventArgs e)
        {


            if (TipoOperacion == Utilitario.TipoOperacion.Registrar && (TipoProgramacion == "LIMAGAS" || TipoProgramacion == "SOLGAS"  || TipoProgramacion == "LINLEY" || TipoProgramacion == "GENERAL" || TipoProgramacion == "LIMAGAS"))
            {
                txtDocRelacionAnexar.Focus();
            }
            else
            {
                btnGuardar.Select();
                btnGuardar.Focus();
            }

            
        }

        private void pEstado_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstEmpresaRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEmpresaRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmpresaRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEmpresaRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    groupFechaTraslado.Select();
                    dtpFechaTraslado.Focus();
                }
                if (txtEmpresaRemitente.Tag == null)
                {
                    txtDireccionPartida.Clear();
                    txtDireccionPartida.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaRemitente_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEmpresaRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstEmpresaRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEmpresaRemitente, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    groupFechaTraslado.Select();
                    dtpFechaTraslado.Focus();

                    txtDireccionPartida.Enabled = true;


                    if (txtEmpresaRemitente.Tag == null)
                    {
                        txtDireccionPartida.Clear();
                        txtDireccionPartida.Enabled = false;

                    }
                    else
                    {
                        //txtEmpresaRemitente.Text = entGuiaTransportista.entGRT_Remitente_RazonSocial_M;
                        DataTable dtClienteProgramacion = clsOperacionesBL.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(Convert.ToInt32(txtEmpresaRemitente.Tag));
                        entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["DocumentoFiscal"].ToString();
                        entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["Codigo"].ToString();
                        entGuiaTransportista.entGRT_Remitente_RazonSocial_M = txtEmpresaRemitente.Text;
                        entGuiaTransportista.idRemitente = Convert.ToInt32(txtEmpresaRemitente.Tag);

                        if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                        {
                            dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocumentoIdentidad"].Value = entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M;
                            dgvDocumentosRelacionados.Rows[0].Cells["TipoDocumentoIdentidad"].Value = entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M;
                        }
           
                      //dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtEmpresaRemitente.Tag)); // RUTAS TODAS DEL CLIENTE
                        dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(Convert.ToInt32(txtCliente.Tag), entGuiaTransportista.idRuta, entGuiaTransportista.idRemitente, 0); // RUTAS DE AUTOCOMPLETADO
                        

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void txtEmpresaRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEmpresaRemitente, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    groupFechaTraslado.Select();
                    dtpFechaTraslado.Focus();
                }


                if (txtEmpresaRemitente.Tag == null)
                {
                    txtDireccionPartida.Clear();
                    txtDireccionPartida.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }

        }

        private void txtDepartamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDepartamentoPartida, ref  lstDepartamentoPartida, null, dtDepartamento))
                {
                    groupProvincia.Select();
                    txtProvinciaPartida.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtDepartamento_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDepartamentoPartida, ref  lstDepartamentoPartida, null, dtDepartamento);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstDepartamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDepartamentoPartida, ref  lstDepartamentoPartida, null, dtDepartamento))
                {
                    groupProvincia.Select();
                    txtProvinciaPartida.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstDepartamento_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDepartamentoPartida, ref  lstDepartamentoPartida, null, dtDepartamento);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstDepartamento_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDepartamentoPartida, ref lstDepartamentoPartida, null, dtDepartamento);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dtpFechaTraslado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gAnexarGR.Select();
                txtDocRelacionAnexar.Focus();
            }

        }

        private void txtAnexarRem_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                if (txtDocRelacionAnexar.Text.Length > 0)
                {
                    dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value = txtDocRelacionAnexar.Text.TrimEnd();
                    dgvDocumentosRelacionados.Rows[0].Cells["NombreDocumento"].Value = cbxTipoDocumentoFiscal.Text;
                    //dgvDocumentosRelacionados.Rows.Add("", txtDocRelacionAnexar.Text);
                }
                groupDestinatario.Select();
                txtEmpresaDestinatario.Focus();
            }
        }

        private void txtProvinciaPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtProvinciaPartida, ref  lstProvinciaPartida, null, dtProvincia))
                {
                    groupDistrito.Select();
                    txtDistritoPartida.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProvinciaPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtProvinciaPartida, ref  lstProvinciaPartida, null, dtProvincia); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstProvinciaPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtProvinciaPartida, ref  lstProvinciaPartida, null, dtProvincia))
                {
                    groupDistrito.Select();
                    txtDistritoPartida.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstProvinciaPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtProvinciaPartida, ref  lstProvinciaPartida, null, dtProvincia); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Correos_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMaestroCorreosGuiasElectronicas OPEN = new FrmMaestroCorreosGuiasElectronicas();
                OPEN.TipOperacion = Utilitario.TipoOperacion.Lectura;
                OPEN.txtLabelMenu.Text = "VER CORREOS POR EMPRESA CLIENTE";
                OPEN.Cliente = txtEmpresaDestinatario.Text;
                OPEN.idCliente = Convert.ToString(txtEmpresaDestinatario.Tag);
                OPEN.direccionDestino = txtDireccionDestino.Text;
         

                string ruta;
                ruta = txtRuta.Text;


                if (OPEN.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtEmpresaDestinatario.Text = entGuiaTransportista.entGRT_Destinatario_RazonSocial_M;
                    txtEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Escape)));
                    lstEmpresaDestinatario.Select();
                    lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                    if (ruta.Length > 0)
                    {
                        txtRuta.Text = ruta;
                        txtRuta_KeyPress(this, new KeyPressEventArgs((char)(Keys.Escape)));
                        lstRuta.Select();
                        lstRuta_KeyUp(this, new KeyEventArgs(Keys.Down));
                        lstRuta_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    }

                    groupDepDestin.Select();
                    txtDepartamentoLLegada.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstEmpresaDestinatario_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEmpresaDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstEmpresaDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEmpresaDestinatario, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstEmpresaDestinatario.SelectedItems[0];
                    txtDocIdentidadDesti.Text = ItemActual.SubItems[2].Text;
                    txtRuta.Enabled = true;

                    //dtDireccionesRutaDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtEmpresaDestinatario.Tag)); // direcciones por empresa
                    dtDireccionesRutaDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(Convert.ToInt32(txtCliente.Tag),entGuiaTransportista.idRuta,Convert.ToInt32(txtEmpresaRemitente.Tag),Convert.ToInt32(txtEmpresaDestinatario.Tag)); // direcciones por emprresa y ruta
                    dtCorreos = clsOperacionesBL.Instancia.ReportesApp_ListarCorreos_Master(Convert.ToInt32(txtEmpresaDestinatario.Tag), txtDireccionDestino.Text);

                    if (dtCorreos.Rows.Count > 0)
                    {


                        IEnumerable<DataRow> ieRegistro = from fila in dtCorreos.AsEnumerable()
                                                          where fila.Field<bool>("Principal") == true
                                                          select fila;

                        if (ieRegistro.Any())
                        {
                            DataTable dtcorreoPrincipal = ieRegistro.CopyToDataTable();
                            entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = dtcorreoPrincipal.Rows[0]["Correo"].ToString();
                            entGuiaTransportista.entGRT_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtcorreoPrincipal.Rows[0]["idCorreo"].ToString());
                            txtCorreoPrimario.Text = dtcorreoPrincipal.Rows[0]["Correo"].ToString();

                            //CORREOS SECUNDARIOS
                            IEnumerable<DataRow> ieCorreoSecundario = from fila in dtCorreos.AsEnumerable()
                                                                      where fila.Field<bool>("Principal") == false
                                                                      select fila;

                            if (ieCorreoSecundario.Any())
                            {
                                DataTable dtCorreoSecundario = ieCorreoSecundario.CopyToDataTable();
                                entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreoSecundario);
                            }

                        }
                        else
                        {
                            entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = dtCorreos.Rows[0]["Correo"].ToString();
                            entGuiaTransportista.entGRT_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtCorreos.Rows[0]["idCorreo"].ToString());
                            txtCorreoPrimario.Text = dtCorreos.Rows[0]["Correo"].ToString();

                            dtCorreos.Rows.RemoveAt(0);
                            if (dtCorreos.Rows.Count > 0)
                            {
                                entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreos);
                            }
                        }
                    }
                    else
                    {
                        entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = "";
                        txtCorreoPrimario.Clear();
                    }

                    entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = txtEmpresaDestinatario.Text;
                    DataTable dtClienteProgramacion = clsOperacionesBL.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(Convert.ToInt32(txtEmpresaDestinatario.Tag));
                    entGuiaTransportista.entGRT_Destinatario_NumeroDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["DocumentoFiscal"].ToString();
                    entGuiaTransportista.entGRT_Destinatario_TipoDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["Codigo"].ToString();
                    entGuiaTransportista.idDestinatario = Convert.ToInt32(txtEmpresaDestinatario.Tag);

                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                        txtDireccionPartida.Enabled = false;
                        txtRuta.Enabled = false;
                    }

                    HabilitarBotonCorreo();
                    groupPuntoPartida.Select();
                    txtRuta.Focus();


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEmpresaDestinatario_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEmpresaDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmpresaDestinatario_Enter(object sender, EventArgs e)
        {
            txtEmpresaDestinatario.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtEmpresaDestinatario_Leave(object sender, EventArgs e)
        {
            txtEmpresaDestinatario.BackColor = Color.White;
        }

        private void txtEmpresaDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
  
            /* try
            {
                dtDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarClientesDestinatario_GuiaElectronica(txtEmpresaRemitente.Text,entGuiaTransportista.idRuta);
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEmpresaDestinatario, ref  lstEmpresaDestinatario, null, dtDestinatario))
                {


                    groupDepDestin.Select();
                    txtDepartamentoLLegada.Focus();
                    txtDireccionDestino.Enabled = true;


                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                    }

                }

                HabilitarBotonCorreo();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/

        }

        private void txtEmpresaDestinatario_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // if ((Keys)vkCode == Keys.Alt)
  
                if (e.Modifiers != Keys.Alt) 
                    { 
                        

                    dtDestinatario = clsOperacionesBL.Instancia.ReportesApp_ListarClientesDestinatario_GuiaElectronica(txtCliente.Text, entGuiaTransportista.idRuta);
                    if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEmpresaDestinatario, ref lstEmpresaDestinatario, null, dtDestinatario))
                    {

                        groupDepDestin.Select();
                        txtDepartamentoLLegada.Focus();
                        txtDireccionDestino.Enabled = true;


                        if (txtEmpresaDestinatario.Tag == null)
                        {
                            txtDireccionDestino.Clear();
                            //txtDireccionDestino.Enabled = false;
                        }
                        HabilitarBotonCorreo();
                    }   
                    }
         

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstProvinciaPartida_Enter_1(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtProvinciaPartida, ref lstProvinciaPartida, null, dtProvincia);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstProvinciaPartida_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtProvinciaPartida, ref  lstProvinciaPartida, null, dtProvincia))
                {
                    groupDistrito.Select();
                    txtDistritoPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstProvinciaPartida_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtProvinciaPartida, ref  lstProvinciaPartida, null, dtProvincia);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDistritoPartida_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDistritoPartida, ref lstDistritoPartida, null, dtCiudad);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDistritoPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDistritoPartida, ref  lstDistritoPartida, null, dtCiudad))
                {
                    groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDistritoPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDistritoPartida, ref  lstDistritoPartida, null, dtCiudad);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDepartamentoPartida_Enter(object sender, EventArgs e)
        {
            txtDepartamentoPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDepartamentoPartida_Leave(object sender, EventArgs e)
        {
            txtDepartamentoPartida.BackColor = Color.White;
        }

        private void txtProvinciaPartida_Leave(object sender, EventArgs e)
        {
            txtProvinciaPartida.BackColor = Color.White;
        }

        private void txtProvinciaPartida_Enter(object sender, EventArgs e)
        {
            txtProvinciaPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDistritoPartida_Enter(object sender, EventArgs e)
        {
            txtDistritoPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDistritoPartida_Leave(object sender, EventArgs e)
        {
            txtDistritoPartida.BackColor = Color.White;
        }

        private void txtDistritoPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDistritoPartida, ref  lstDistritoPartida, null, dtCiudad))
                {
                    groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtDistritoPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == (char)Keys.Escape)
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;

                }

                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDistritoPartida, ref  lstDistritoPartida, null, dtCiudad))
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                    lstDireccionPartida.Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (e.KeyChar == (char)Keys.Escape)
            {
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text ;

            }

            if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRuta))
            {
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text ;
                groupDireccionDestino.Select();
                txtDireccionDestino.Focus();
                lstDireccionPartida.Visible = false;
            }*/
        }

        private void txtDepartamentoLLegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDepartamentoLLegada, ref  lstDepartamentoLlegada, null, dtDepartamento))
                {
                    groupProvinciaLLegada.Select();
                    txtProvinciaLlegada.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtProvinciaLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtProvinciaLlegada, ref  lstProvinciaLlegada, null, dtProvincia))
                {
                    groupDistritoLlegada.Select();
                    txtDistritoLlegada.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtDistritoLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDistritoLlegada, ref  lstDistritoLlegada, null, dtCiudad))
                {
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtDireccionLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                groupObservacion.Select();
                txtObservaciones.Focus();
            }
        }

        private void txtAnexarRem_Enter(object sender, EventArgs e)
        {
            txtDocRelacionAnexar.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtAnexarRem_Leave(object sender, EventArgs e)
        {
            if (txtDocRelacionAnexar.Text.Length > 0)
            {
                dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value = txtDocRelacionAnexar.Text.TrimEnd();
                dgvDocumentosRelacionados.Rows[0].Cells["NombreDocumento"].Value = cbxTipoDocumentoFiscal.Text;
                //dgvDocumentosRelacionados.Rows.Add("", txtDocRelacionAnexar.Text);
            }
            txtDocRelacionAnexar.BackColor = Color.White;


        }

        private void txtDireccionPartida_Enter(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionPartida_Leave(object sender, EventArgs e)
        {
            txtDireccionPartida.BackColor = Color.White;
        }

        private void txtDepartamentoLLegada_Enter(object sender, EventArgs e)
        {
            txtDepartamentoLLegada.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDepartamentoLLegada_Leave(object sender, EventArgs e)
        {
            txtDepartamentoLLegada.BackColor = Color.White;
        }

        private void txtProvinciaLlegada_Enter(object sender, EventArgs e)
        {

            txtProvinciaLlegada.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtProvinciaLlegada_Leave(object sender, EventArgs e)
        {

            txtProvinciaLlegada.BackColor = Color.White;
        }

        private void txtDistritoLlegada_Enter(object sender, EventArgs e)
        {
            txtDistritoLlegada.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDistritoLlegada_Leave(object sender, EventArgs e)
        {
            txtDistritoLlegada.BackColor = Color.White;
        }

        private void txtDireccionLlegada_Enter(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionLlegada_Leave(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.White;
        }

        private void txtObservaciones_Enter(object sender, EventArgs e)
        {
            txtObservaciones.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtObservaciones_Leave(object sender, EventArgs e)
        {
            txtObservaciones.BackColor = Color.White;
        }

        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                txtPesoTotal.Focus();
                txtPesoTotal.Select();
            }
        }

        private void btnRegistraCorreo_Click(object sender, EventArgs e)
        {
            FrmMaestroCorreosGuiasElectronicas OPEN = new FrmMaestroCorreosGuiasElectronicas();
            OPEN.TipOperacion = Utilitario.TipoOperacion.Registrar;
            OPEN.txtLabelMenu.Text = "REGISTRAR CORREOS POR EMPRESA CLIENTE";
            OPEN.Cliente = txtEmpresaDestinatario.Text;
            OPEN.idCliente = Convert.ToString(txtEmpresaDestinatario.Tag);
            OPEN.Show(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMaestroCorreosGuiasElectronicas OPEN = new FrmMaestroCorreosGuiasElectronicas();
            OPEN.TipOperacion = Utilitario.TipoOperacion.Anular;
            OPEN.txtLabelMenu.Text = "ELIMINAR CORREOS POR EMPRESA CLIENTE";
            OPEN.Cliente = txtEmpresaDestinatario.Text;
            OPEN.idCliente = Convert.ToString(txtEmpresaDestinatario.Tag);
            OPEN.Show(this);
        }

        private void lstDepartamentoLlegada_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDepartamentoLLegada, ref lstDepartamentoLlegada, null, dtDepartamento);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDepartamentoLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDepartamentoLLegada, ref  lstDepartamentoLlegada, null, dtDepartamento))
                {
                    groupProvinciaLLegada.Select();
                    txtProvinciaLlegada.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDepartamentoLlegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDepartamentoLLegada, ref  lstDepartamentoLlegada, null, dtDepartamento);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDepartamentoLLegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDepartamentoLLegada, ref  lstDepartamentoLlegada, null, dtDepartamento);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstProvinciaLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtProvinciaLlegada, ref  lstProvinciaLlegada, null, dtProvincia))
                {
                    groupDistritoLlegada.Select();
                    txtDistritoLlegada.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstProvinciaLlegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtProvinciaLlegada, ref  lstProvinciaLlegada, null, dtProvincia);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProvinciaLlegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtProvinciaLlegada, ref  lstProvinciaLlegada, null, dtProvincia);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstProvinciaLlegada_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtProvinciaLlegada, ref lstProvinciaLlegada, null, dtProvincia);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDistritoLlegada_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDistritoLlegada, ref lstDistritoLlegada, null, dtCiudad);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDistritoLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDistritoLlegada, ref  lstDistritoLlegada, null, dtCiudad))
                {
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDistritoLlegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDistritoLlegada, ref  lstDistritoLlegada, null, dtCiudad);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDistritoLlegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDistritoLlegada, ref  lstDistritoLlegada, null, dtCiudad);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void txtLicenciaConducir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                groupObservacion.Select();
                txtObservaciones.Focus();
            }
        }



        private void cbxSerieGuia_SelectedValueChanged(object sender, EventArgs e)
        {
            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                entGuiaTransportista.entGRT_Generales_Serie_M = cbxSerieGuia.Text;
            }


       
        }

        private void cbxSerieGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dgvProductosGuia_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstConductor.SelectedItems[0];
                    txtConductor.Text = ItemActual.SubItems[1].Text;

                    if (txtConductor.Tag != null)
                    {
                        DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor.Tag));
                        txtLicencia.Text = dt.Rows[0]["Brevete"].ToString();
                        txtDocIdentidad.Text = dt.Rows[0]["Documento"].ToString();
                        txtTipoDocumentoIdentidad.Text = dt.Rows[0]["TipoDocumento"].ToString();
                        txtTipoDocumentoIdentidad.Tag = dt.Rows[0]["Codigo"].ToString();

                        
                        entNuevoConductor.entGRT_Conductor_Nombres_M = dt.Rows[0]["Nombres"].ToString();
                        entNuevoConductor.entGRT_Conductor_Apellidos_M = dt.Rows[0]["Apellidos"].ToString();
                        entNuevoConductor.entGRT_Conductor_Licencia_M = dt.Rows[0]["Brevete"].ToString();
                        entNuevoConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M = dt.Rows[0]["Documento"].ToString();
                        entNuevoConductor.entGRT_Conductor_TipoDocumentoIdentidad_M = dt.Rows[0]["Codigo"].ToString();
                        entGuiaTransportista.entConductor = entNuevoConductor;

                    }


                    groupPlacaVehiculo.Select();
                    txtPlaca.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void lstConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtConductor_Enter(object sender, EventArgs e)
        {
            txtConductor.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtConductor_Leave(object sender, EventArgs e)
        {
            txtConductor.BackColor = Color.White;
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    groupObservacion.Select();
                    txtObservaciones.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaca_Enter(object sender, EventArgs e) { txtPlaca.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca_Leave(object sender, EventArgs e) { txtConductor.BackColor = Color.White; }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    gConductor.Select();
                    txtConductor.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPlaca, ref  lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstPlaca.SelectedItems[0];
                    txtPlaca.Text = ItemActual.SubItems[1].Text.TrimEnd();
                    entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M = txtPlaca.Text.TrimEnd();
                    entGuiaTransportista.idtracto = txtPlaca.Tag.ToString();

                    rbTUC.Checked = true;
                    rbTUC_Click(sender, e);

                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null)
                    {
                        txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;
                    }

                    groupObservacion.Select();
                    txtObservaciones.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ; }
        }

        private void lstPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarProgramacion_Click(object sender, EventArgs e)
        {
            try
            {
                bool repetido = false;
                if (txtConductor2.Tag != null || txtConductor2.Text.Length > 0 | txtPlaca2.Tag != null || txtPlaca2.Text.Length > 0 || txtLicencia2.Text.Length > 0)
                {
                    for (int i = 0; i < dgvTrasladoProgramado.Rows.Count; i++)
                    {
                        if (txtConductor2.Tag.ToString() == dgvTrasladoProgramado.Rows[i].Cells["idConductor"].Value.ToString())
                        {
                            MessageBox.Show("No es posible agregar, conductor ya existe en la lista", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            repetido = true;
                            break;
                        }
                    }
                    if (repetido == false)
                    {
                        if (txtConductor2.Tag != null)
                        {
                            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor2.Tag));
                            dgvTrasladoProgramado.Rows.Add(dt.Rows[0]["idConductor"], 
                                                           txtConductor2.Text, 
                                                           txtPlaca2.Tag.ToString(), 
                                                           txtPlaca2.Text, 
                                                           txtLicencia2.Text, 
                                                           txtDocIdentidad2.Text, 
                                                           txtTipoDocumento2.Tag.ToString(), 
                                                           dt.Rows[0]["Nombres"].ToString(), 
                                                           dt.Rows[0]["Apellidos"].ToString(), 
                                                           txtCarreta2.Tag.ToString(), 
                                                           txtCarreta2.Text, 
                                                           entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion == null ? "" : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion, 
                                                           entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta == null ? "" : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta);
                        }
                        else
                        {
                            MessageBox.Show("No se ha seleccionado la placa o el conductor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                          
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void lstPlaca2_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlaca2, ref lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstPlaca2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPlaca2, ref  lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstPlaca2.SelectedItems[0];
                    txtPlaca2.Text = ItemActual.SubItems[1].Text.TrimEnd();
                    entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M = txtPlaca2.Text.TrimEnd();
                    entGuiaTransportista.idtracto = txtPlaca2.Tag.ToString();
                    entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion = ItemActual.SubItems[2].Text;

                    rbTUC.Checked = true;
                    rbTUC_Click(sender, e);
                    
                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null)
                    {
                        txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;
                    }
                    
                    groupConductor2.Select();
                    txtConductor2.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }


        }

        private void lstPlaca2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPlaca2, ref lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaca2_Enter(object sender, EventArgs e)
        {
            txtPlaca2.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPlaca2_Leave(object sender, EventArgs e)
        {
            txtPlaca2.BackColor = Color.White;
        }

        private void txtPlaca2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPlaca2, ref  lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
            {
                groupConductor2.Select();
                txtConductor2.Focus();

            }
        }

        private void txtPlaca2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPlaca2, ref lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtxConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtConductor2, ref  lstConductor2, clsConsultaBL.Instancia.GetConductores))
                {
                    groupCarreta2.Select();
                    txtCarreta2.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtxConductor2_Enter(object sender, EventArgs e)
        {
            txtConductor2.BackColor = Color.FromArgb(192, 255, 192);

        }

        private void txtConductor2_Leave(object sender, EventArgs e)
        {
            txtConductor2.BackColor = Color.White;
        }

        private void txtConductor2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtConductor2, ref lstConductor2, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstConductor2_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtConductor2, ref lstConductor2, clsConsultaBL.Instancia.GetConductores);

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtConductor2, ref  lstConductor2, clsConsultaBL.Instancia.GetConductores))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstConductor2.SelectedItems[0];
                    txtConductor2.Text = ItemActual.SubItems[1].Text;

                    if (txtConductor2.Tag != null)
                    {
                        DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor2.Tag));
                        txtLicencia2.Text = dt.Rows[0]["Brevete"].ToString();
                        txtDocIdentidad2.Text = dt.Rows[0]["Documento"].ToString();
                        txtTipoDocumento2.Text = dt.Rows[0]["TipoDocumento"].ToString();
                        txtTipoDocumento2.Tag = dt.Rows[0]["Codigo"].ToString();

                        entGuiaTransportista.idconductor = Convert.ToInt32(dt.Rows[0]["idConductor"]);
                        entNuevoConductor.entGRT_Conductor_Nombres_M = dt.Rows[0]["Nombres"].ToString();
                        entNuevoConductor.entGRT_Conductor_Apellidos_M = dt.Rows[0]["Apellidos"].ToString();
                        entNuevoConductor.entGRT_Conductor_Licencia_M = dt.Rows[0]["Brevete"].ToString();
                        entNuevoConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M = dt.Rows[0]["Documento"].ToString();
                        entNuevoConductor.entGRT_Conductor_TipoDocumentoIdentidad_M = dt.Rows[0]["Codigo"].ToString();

                    }


                    groupCarreta2.Select();
                    txtCarreta2.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstConductor2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtConductor2, ref lstConductor2, clsConsultaBL.Instancia.GetConductores);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void checkDocRelacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkDocRelacion.Checked)
                {
                    groupDocRelacionTabla.Size = new Size(956, 101);

                    lstConductor.Location = new Point(114, 367);
                    lstPlaca.Location = new Point(20, 387);
                    lstPlaca2.Location = new Point(21, 388);
                    lstCarreta.Location = new Point(347, 388);
                    lstCarreta2.Location = new Point(347, 388);
                    lstConductor2.Location = new Point(115, 387);
                    lstFleteTercero.Location = new Point(27, 387);
                    lstSubcontratado.Location = new Point(30, 387);
                    g_glosaFlete.Visible = false;
                }
                if (checkDocRelacion.Checked == false)
                {
                    groupDocRelacionTabla.Size = new Size(956, 16);

                    lstConductor.Location = new Point(114, 303);
                    lstPlaca.Location = new Point(88, 303);
                    lstPlaca2.Location = new Point(21, 303);
                    lstCarreta.Location = new Point(347, 305);
                    lstCarreta2.Location = new Point(347, 305);
                    lstConductor2.Location = new Point(115, 305);
                    lstFleteTercero.Location = new Point(27, 306);
                    lstSubcontratado.Location = new Point(30, 305);
                    g_glosaFlete.Visible = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxTipoDocumentoFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvDocumentosRelacionados.Rows.Count > 0)


            dgvDocumentosRelacionados.Rows[0].Cells["TipoDocRelacion"].Value = (cbxTipoDocumentoFiscal.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
            if (cbxTipoDocumentoFiscal.Text == "Guía de remisión")
            {
                txtDocRelacionAnexar.Text = "T001-";
            }
            if (cbxTipoDocumentoFiscal.Text == "Liquidación de compra.")
            {
                txtDocRelacionAnexar.Text = "OC001-";
            }
            if (cbxTipoDocumentoFiscal.SelectedText == "Boleta de Venta")
            {
                txtDocRelacionAnexar.Text = "BV001-";
            }
            if (cbxTipoDocumentoFiscal.Text == "Factura")
            {
                txtDocRelacionAnexar.Text = "F001-";
            }
            if (cbxTipoDocumentoFiscal.Text == "Otros")
            {
                txtDocRelacionAnexar.Text = "";
            }

        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dgvProductosGuia.Rows.Count ;
                if (i < 0) { i = 0; }
                dgvProductosGuia.Rows.Add((i + 1).ToString("D10"), "", "", "", "");
                dgvProductosGuia.Rows[dgvProductosGuia.RowCount - 1].Cells["UnidadMedida"].Value = (UnidadMedida.Items[1] as DataRowView).Row[0].ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarDocRelacion_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDocumentosRelacionados.Rows.Add("", "", "", "", "");

                dgvDocumentosRelacionados.Rows[dgvDocumentosRelacionados.RowCount - 1].Cells["TipoDocRelacion"].Value = (cbxTipoDocumentoFiscal.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
                dgvDocumentosRelacionados.Rows[dgvDocumentosRelacionados.RowCount - 1].Cells["NombreDocumento"].Value = (cbxTipoDocumentoFiscal.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[1].ToString();
                dgvDocumentosRelacionados.Rows[dgvDocumentosRelacionados.RowCount - 1].Cells["NumeroDocumentoIdentidad"].Value = entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M;
                dgvDocumentosRelacionados.Rows[dgvDocumentosRelacionados.RowCount - 1].Cells["TipoDocumentoIdentidad"].Value = entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M;
                
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductosGuia.Rows.Count > 0)
                {
                    dgvProductosGuia.Rows.RemoveAt(dgvProductosGuia.CurrentRow.Index);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnQuitarDocRelacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDocumentosRelacionados.Rows.Count > 0)
                {
                    dgvDocumentosRelacionados.Rows.RemoveAt(dgvDocumentosRelacionados.CurrentRow.Index);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxTipoDocumentoFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = true;

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void txtPuntoPartida_Enter(object sender, EventArgs e)
        {
            txtRuta.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtPuntoPartida_Leave(object sender, EventArgs e)
        {
            txtRuta.BackColor = Color.White;
        }


        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRuta, ref  lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {

                    groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {
                    groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRuta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRuta, ref  lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica))
                {

                    if (rbtRetorno.Checked == false)
                    {
                       // entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = txtRuta.Tag.ToString(); // ubigeo partida
                       // entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = lstRuta.SelectedItems[0].SubItems[2].Text; // ubigeo destino

                        //DireccionUbigeoOrigen = lstRuta.SelectedItems[0].SubItems[3].Text;
                        //DireccionUbigeoFin = lstRuta.SelectedItems[0].SubItems[4].Text;

                        if (txtRuta.Tag != null)
                        {
                            txtDireccionDestino.Enabled = true;
                            txtDireccionPartida.Enabled = true;
                        }
                        else
                        {
                            txtDireccionDestino.Enabled = false;
                            txtDireccionPartida.Enabled = false;
                        }
                        groupDireccionPartida.Select();
                        txtDireccionPartida.Focus();
                    }


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRuta, ref lstRuta, clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyValue == (char)Keys.Escape)
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;

                }
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta))
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                    lstDireccionPartida.Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionPartida_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRuta))
                {
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text; //+ ", "+ DireccionUbigeoOrigen;





                    ListViewItem ItemActual;
                    ItemActual = lstDireccionPartida.SelectedItems[0];
                    entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia = Convert.ToInt32(ItemActual.SubItems[2].Text);

                    if (rbtSalida.Checked)
                    {
                        entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = ItemActual.SubItems[3].Text; // ubigeo destino
                        DireccionUbigeoOrigen = ItemActual.SubItems[4].Text;
                        txtUbigeoPartida.Text = ItemActual.SubItems[4].Text;
                    }
                    if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                    {
                        entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = ItemActual.SubItems[3].Text; // ubigeo destino
                        DireccionUbigeoOrigen = ItemActual.SubItems[4].Text;
                        txtUbigeoPartida.Text = ItemActual.SubItems[4].Text;
                    }
                    
             
         


                    dtCorreos = clsOperacionesBL.Instancia.ReportesApp_ListarCorreos_Master(Convert.ToInt32(txtEmpresaDestinatario.Tag), txtDireccionDestino.Text);

                    if (dtCorreos.Rows.Count > 0)
                    {


                        IEnumerable<DataRow> ieRegistro = from fila in dtCorreos.AsEnumerable()
                                                          where fila.Field<bool>("Principal") == true
                                                          select fila;

                        if (ieRegistro.Any())
                        {
                            DataTable dtcorreoPrincipal = ieRegistro.CopyToDataTable();
                            entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = dtcorreoPrincipal.Rows[0]["Correo"].ToString();
                            entGuiaTransportista.entGRT_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtcorreoPrincipal.Rows[0]["idCorreo"].ToString());
                            txtCorreoPrimario.Text = dtcorreoPrincipal.Rows[0]["Correo"].ToString();

                            //CORREOS SECUNDARIOS
                            IEnumerable<DataRow> ieCorreoSecundario = from fila in dtCorreos.AsEnumerable()
                                                                      where fila.Field<bool>("Principal") == false
                                                                      select fila;

                            if (ieCorreoSecundario.Any())
                            {
                                DataTable dtCorreoSecundario = ieCorreoSecundario.CopyToDataTable();
                                entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreoSecundario);
                            }

                        }
                        else
                        {
                            entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = dtCorreos.Rows[0]["Correo"].ToString();
                            entGuiaTransportista.entGRT_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtCorreos.Rows[0]["idCorreo"].ToString());
                            txtCorreoPrimario.Text = dtCorreos.Rows[0]["Correo"].ToString();

                            dtCorreos.Rows.RemoveAt(0);
                            if (dtCorreos.Rows.Count > 0)
                            {
                                entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreos);
                            }
                        }
                    }
                    else
                    {
                        entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = "";
                        txtCorreoPrimario.Clear();
                    }

                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();




                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDireccionDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar == (char)Keys.Escape)
            {
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;
            }



            if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRutaDestinatario))
            {
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;


                if (txtPlaca.Tag == null)
                {
                    groupPlacaVehiculo.Select();
                    txtPlaca.Focus();
                }
                else
                {
                    lstDireccionDestino.Visible = false;
                    groupObservacion.Select();
                    txtObservaciones.Focus();
                }
            }*/
        }

        private void txtDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == (char)Keys.Escape)
                {
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;
                }


                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario))
                {
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;


                    if (txtPlaca.Tag == null)
                    {
                        groupPlacaVehiculo.Select();
                        txtPlaca.Focus();
                    }
                    else
                    {
                        lstDireccionDestino.Visible = false;
                        groupObservacion.Select();
                        txtObservaciones.Focus();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionDestino_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtDireccionDestino_Enter(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtDireccionDestino_Leave(object sender, EventArgs e)
        {
            txtDireccionDestino.BackColor = Color.White;
        }

        private void lstDireccionDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRutaDestinatario))
                {
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = "";
                    entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;// + ", " + DireccionUbigeoFin;

                    ListViewItem ItemActual;
                    ItemActual = lstDireccionDestino.SelectedItems[0];
                    entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia = Convert.ToInt32(ItemActual.SubItems[2].Text);

                    if (rbtSalida.Checked) 
                    {
                        entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = ItemActual.SubItems[3].Text; // ubigeo destino
                        DireccionUbigeoFin = ItemActual.SubItems[4].Text;
                        txtUbigeoDestino.Text = ItemActual.SubItems[4].Text;
                    }

                    if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                    {
                        entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = ItemActual.SubItems[3].Text; // ubigeo destino
                        DireccionUbigeoFin = ItemActual.SubItems[4].Text;
                        txtUbigeoDestino.Text = ItemActual.SubItems[4].Text;
                    }
                 

                    dtCorreos = clsOperacionesBL.Instancia.ReportesApp_ListarCorreos_Master(Convert.ToInt32(txtEmpresaDestinatario.Tag), txtDireccionDestino.Text);

                    if (dtCorreos.Rows.Count > 0)
                    {


                        IEnumerable<DataRow> ieRegistro = from fila in dtCorreos.AsEnumerable()
                                                          where fila.Field<bool>("Principal") == true
                                                          select fila;

                        if (ieRegistro.Any())
                        {
                            DataTable dtcorreoPrincipal = ieRegistro.CopyToDataTable();
                            entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = dtcorreoPrincipal.Rows[0]["Correo"].ToString();
                            entGuiaTransportista.entGRT_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtcorreoPrincipal.Rows[0]["idCorreo"].ToString());
                            txtCorreoPrimario.Text = dtcorreoPrincipal.Rows[0]["Correo"].ToString();

                            //CORREOS SECUNDARIOS
                            IEnumerable<DataRow> ieCorreoSecundario = from fila in dtCorreos.AsEnumerable()
                                                                      where fila.Field<bool>("Principal") == false
                                                                      select fila;

                            if (ieCorreoSecundario.Any())
                            {
                                DataTable dtCorreoSecundario = ieCorreoSecundario.CopyToDataTable();
                                entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreoSecundario);
                            }

                        }
                        else
                        {
                            entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = dtCorreos.Rows[0]["Correo"].ToString();
                            entGuiaTransportista.entGRT_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtCorreos.Rows[0]["idCorreo"].ToString());
                            txtCorreoPrimario.Text = dtCorreos.Rows[0]["Correo"].ToString();

                            dtCorreos.Rows.RemoveAt(0);
                            if (dtCorreos.Rows.Count > 0)
                            {
                                entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreos);
                            }
                        }
                    }
                    else
                    {
                        entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M = "";
                        txtCorreoPrimario.Clear();
                    }


                    if (txtPlaca.Tag == null)
                    {
                        groupPlacaVehiculo.Select();
                        txtPlaca.Focus();
                    }
                    else
                    {
                        groupObservacion.Select();
                        txtObservaciones.Focus();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRutaDestinatario);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgListaGuiasTransportista_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            try
            {
                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    if (dgvListaGuiaTraspExpressVista.RowCount > 0)
                    {
                        txtCodviaje.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje")).Length > 0 ? Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje")) : "";
                        entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "OT"));
                        txtOT.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "OT"));
                        entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Linea"));

                        if (Convert.ToInt16(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdRuta")) > 0)
                        {
                            txtRuta.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                            txtRuta.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdRuta"));
                            entGuiaTransportista.idRuta = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdRuta"));
                            entGuiaTransportista.ruta = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                        }

                        txtPesoTotal.Value = Convert.ToDecimal(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Cantidad"));
                    }
                }

              

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductosGuia_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductosGuia.Columns[e.ColumnIndex].Name == "Peso")
            {
                Decimal totalColumna = 0;
                foreach (DataGridViewRow row in dgvProductosGuia.Rows)
                {
                    Decimal pedido = 0;
                    if (!Decimal.TryParse(Convert.ToString(row.Cells["Peso"].Value), out pedido))
                        continue;

                    Decimal totalFila = Convert.ToDecimal(row.Cells["Peso"].Value);
                    totalColumna = Convert.ToDecimal(totalFila + totalColumna);
                }

                DataGridViewRow rowTotal = dgvProductosGuia.Rows[dgvProductosGuia.Rows.Count - 1];
                txtPesoTotal.Text = string.Format("{0:0.00}", totalColumna);

                if (Convert.ToDecimal(txtPesoTotal.Text) > 0)
                {
                    btnGuardar.Enabled = true;
                }
                else
                {
                    btnGuardar.Enabled = false;
                }
            }


        }

        private void dgvProductosGuia_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {


            if (dgvProductosGuia.Columns[e.ColumnIndex].Name == "Descripcion")
            {

                if (e.FormattedValue == "")
                {
                
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }

            if (dgvProductosGuia.Columns[e.ColumnIndex].Name == "Peso" || dgvProductosGuia.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                
                decimal pedido = 0;
                DataGridViewRow row;

                if (!decimal.TryParse(e.FormattedValue.ToString(), out pedido))
                {
                    row = dgvProductosGuia.Rows[e.RowIndex];

                    dgvProductosGuia.ShowCellErrors = true;
                    dgvProductosGuia.ShowRowErrors = true;
                    row.ErrorText = "Debe ingresar un numero valido";
                    error.SetError(dgvProductosGuia, "No a ingresado ningun valor");
                    e.Cancel = true;
                }
                else
                {
                  
                    error.Clear();
                    dgvProductosGuia.ShowRowErrors = false;
                    dgvProductosGuia.ShowRowErrors = false;
                    e.Cancel = false;
                }
               
            }
        }

        private void dgvProductosGuia_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dgvProductosGuia.CurrentCell.ColumnIndex;

            if (dgvProductosGuia.Columns[columnIndex].Name == "Descripcion" || dgvProductosGuia.Columns[columnIndex].Name == "Codigo")
            {
                DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

                dText.KeyPress -= new KeyPressEventHandler(dgvProductosGuia_KeyPress);
                dText.KeyPress += new KeyPressEventHandler(dgvProductosGuia_KeyPress);
            }

        }

        private void dgvProductosGuia_KeyPress(object sender, KeyPressEventArgs e)
        {

            int columnIndex = dgvProductosGuia.CurrentCell.ColumnIndex;

            if (dgvProductosGuia.Columns[columnIndex].Name == "Descripcion" || dgvProductosGuia.Columns[columnIndex].Name == "Codigo")
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }

            if (dgvProductosGuia.Columns[columnIndex].Name == "Descripcion")
            {
                if (!char.IsSeparator(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '"' && e.KeyChar != '-' && e.KeyChar != '/' && e.KeyChar != '*' && e.KeyChar != '%' && e.KeyChar != '&' && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '!')
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPesoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cbxUnidadMedidaTotal.Select();
                cbxUnidadMedidaTotal.Focus();
            }
        }

        private void txtObservacionCargaTotal_Enter(object sender, EventArgs e)
        {
            txtObservacionCargaTotal.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtObservacionCargaTotal_Leave(object sender, EventArgs e)
        {
            txtObservacionCargaTotal.BackColor = Color.White;
        }

        private void cbxUnidadMedidaTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtObservacionCargaTotal.Select();
                txtObservacionCargaTotal.Focus();
            }
        }

        private void txtObservacionCargaTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dgvProductosGuia.Select();
                dgvProductosGuia.Focus();
            }
        }

        private void dtpFechaTraslado_ValueChanged(object sender, EventArgs e)
        {
            entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M = dtpFechaTraslado.Value.ToString();
        }



        private void cbxTipoServicios_EditValueChanged(object sender, EventArgs e)
        {

            btnAgregarProgramacion.Visible = false;
            tabPage4.Parent = null;
            tabPage5.Parent = null;
            tabPage6.Parent = null;
        }

        private void cbxTipoServicios_Popup(object sender, EventArgs e)
        {

            var popup = (IPopupControl)sender;

            var control = popup.PopupWindow.Controls.OfType<PopupContainerControl>().First().Controls.OfType<CheckedListBoxControl>().First();

            control.ItemCheck += control_ItemCheck;
            cbxTipoServicios.Popup -= cbxTipoServicios_Popup;
            
            
            /*control.Pop -= cbxTipoServicios_Popup;
            var edit = sender as CheckedComboBoxEdit;
            edit.control_ItemCheck += control_ItemCheck;
            edit.Popup -= CheckedComboBoxEdit1_Popup;*/


        }

        private void control_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            try
            {
                var checkedListBoxControl = (CheckedListBoxControl)sender;
                var current = checkedListBoxControl.Items[e.Index];

                //dtTipoServicio.Rows.Clear();
                var edit = sender as CheckedListBoxControl;



                if (cbxTipoServicios.Properties.Items.Count > 0)
                {
                    if (current.CheckState == CheckState.Checked && current.Description == "Placa y Vehiculo") // Placa y Vehiculo 00
                    {
                        // desactivo el tipo de servicio trasbordo programado

                        edit.SetItemChecked(1, false);
                        cbxTipoServicios.Properties.Items[1].CheckState = CheckState.Unchecked;

                        tabPage2.Parent = tabContingencia;
                        tabPage4.Parent = null;
                        btnAgregarProgramacion.Visible = false;
                        groupVehiculoConductor.Size = new Size(956, 93);

                    }

                    else
                    {
                        if (current.CheckState == CheckState.Checked && current.Description == "Trasbordo Programado")
                        {
                            tabPage2.Parent = null;
                            edit.SetItemChecked(1, true);
                            cbxTipoServicios.Properties.Items[1].CheckState = CheckState.Checked;
                            btnAgregarProgramacion.Visible = true;
                        }

                    }

                    if (current.CheckState == CheckState.Checked && current.Description == "Trasbordo Programado") // Trasbordo Programado 01
                    {

                        // desactivo el tipo de general de un solo conductor y vehiculo
                        cbxTipoServicios.Properties.Items[0].CheckState = CheckState.Unchecked;
                        edit.SetItemChecked(0, false);
                        tabPage2.Parent = null;
                        tabPage4.Parent = tabContingencia;
                        //tabPage5.Parent = null;
                        //tabPage6.Parent = null;
                        btnAgregarProgramacion.Visible = true;
                        groupVehiculoConductor.Size = new Size(956, 157);
                        TipoTrasladoProgramado = true;
                        g_glosaFlete.Visible = false;

                    }
                    else
                    {
                        if (current.CheckState == CheckState.Unchecked && current.Description == "Trasbordo Programado")
                        {
                            TipoTrasladoProgramado = false;
                            edit.SetItemChecked(0, true);
                            btnAgregarProgramacion.Visible = false;
                            tabPage4.Parent = null;
                            g_glosaFlete.Visible = true;
                        }
                    }

                    if (current.CheckState == CheckState.Checked && current.Description == "Retorno de vehiculo con envaseso o embalajes vacios") // Retorno de vehiculo con envaseso o embalajes vacios 02
                    {

                    }
                    if (current.CheckState == CheckState.Checked && current.Description == "Retorno de vehiculo vacio") // Retorno de vehiculo vacio 03
                    {

                    }
                    if (current.CheckState == CheckState.Checked && current.Description == "Transporte Subcontratado") // Transporte Subcontratado 04 // EMPRESA A LA QUE SUBCONTRATAMOS PARA EL TRASLADO
                    {

                        tabPage5.Parent = tabContingencia;// subcontratado
                        cbxTipoServicios.Properties.Items[6].CheckState = CheckState.Checked; // Activo el check de  "Pago de flete de Subcontratador"
                        edit.SetItemChecked(6, true);

                        edit.SetItemChecked(7, false); //desactivo el check de  "Pago de flete de Tercer"
                        cbxTipoServicios.Properties.Items[7].CheckState = CheckState.Unchecked;

                    }
                    else
                    {
                        if (current.CheckState == CheckState.Unchecked && current.Description == "Transporte Subcontratado")
                        {
                            cbxTipoServicios.Properties.Items[6].CheckState = CheckState.Unchecked;
                            edit.SetItemChecked(6, false);
                            tabPage5.Parent = null;
                            edit.Items[7].Enabled = true;
                        }

                    }

                    if (current.CheckState == CheckState.Checked && current.Description == "Pago de flete de Remitente") // Pago de flete de Remitente 05
                    {
                        edit.SetItemChecked(6, false); //desactivo el check de  "Pago de flete de Remitente"
                        cbxTipoServicios.Properties.Items[6].CheckState = CheckState.Unchecked;
                        edit.Items[6].Enabled = false;

                        edit.SetItemChecked(7, false); //desactivo el check de  "Pago de flete de Tercer"
                        cbxTipoServicios.Properties.Items[7].CheckState = CheckState.Unchecked;
                        edit.Items[7].Enabled = false;

                    }
                    else if (current.CheckState == CheckState.Unchecked && current.Description == "Pago de flete de Remitente")
                    {
                        edit.Items[6].Enabled = true;
                        edit.Items[7].Enabled = true;
                    }



                    if (current.CheckState == CheckState.Checked && current.Description == "Pago de flete de Subcontratador") // Pago de flete de Subcontratador 06 <---- item 6
                    {
                        edit.SetItemChecked(4, true); //Activo el check de  "Pago de flete de subcontratado"
                        cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Checked;

                        edit.SetItemChecked(5, false); //desactivo el check de  "Pago de flete de Remitente"
                        cbxTipoServicios.Properties.Items[5].CheckState = CheckState.Unchecked;
                        //edit.Items[5].Enabled = false;

                        edit.SetItemChecked(7, false); //desactivo el check de  "Pago de flete de Tercero"
                        cbxTipoServicios.Properties.Items[7].CheckState = CheckState.Unchecked;
                        edit.Items[7].Enabled = false;
                    }
                    else
                    {
                        if (current.CheckState == CheckState.Unchecked && current.Description == "Pago de flete de Subcontratador")
                        {
                            edit.SetItemChecked(5, true); //Activo el check de  "Pago de flete de Remitente"
                            cbxTipoServicios.Properties.Items[5].CheckState = CheckState.Checked;
                            edit.Items[5].Enabled = true;
                            edit.Items[7].Enabled = true;
                        }
                    }


                    if (current.CheckState == CheckState.Checked && current.Description == "Pago de flete de Tercero") // Pago de flete de Tercero 07 <--- item 7
                    {
                        tabPage6.Parent = tabContingencia;
                        cbxTipoServicios.Properties.Items[5].CheckState = CheckState.Unchecked;
                        edit.SetItemChecked(5, false);
                        edit.Items[5].Enabled = false;
                        entGuiaTransportista.TipoFleteTercero = true;
                        TipoTransoporteContratista = true;

                        edit.SetItemChecked(6, false); //desactivo el check de  "Pago de flete de Tercero"
                        cbxTipoServicios.Properties.Items[6].CheckState = CheckState.Unchecked;
                        edit.Items[6].Enabled = false;


                    }
                    else
                    {
                        if (current.CheckState == CheckState.Unchecked && current.Description == "Pago de flete de Tercero")
                        {
                            tabPage6.Parent = null;
                            cbxTipoServicios.Properties.Items[5].CheckState = CheckState.Checked;
                            edit.SetItemChecked(5, true);
                            edit.Items[5].Enabled = true;
                            entGuiaTransportista.TipoFleteTercero = false;


                            edit.Items[6].Enabled = true;

                        }

                    }
                    if (current.CheckState == CheckState.Checked && current.Description == "Traslado total de bienes") // Traslado total de bienes 08
                    {

                        dgvProductosGuia.ReadOnly = true;
                        btnAgregarProducto.Enabled = false;
                        btnQuitarProducto.Enabled = false;
                        ValidarProductoEstado(current);
                    }
                    else
                    {
                        if (current.CheckState == CheckState.Unchecked && current.Description == "Traslado total de bienes")
                        {
                            dgvProductosGuia.ReadOnly = false;
                            dgvProductosGuia.Enabled = true;
                            btnAgregarProducto.Enabled = true;
                            btnQuitarProducto.Enabled = true;
                            ValidarProductoEstado(current);

                        }

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public void ValidarProductoEstado(DevExpress.XtraEditors.Controls.CheckedListBoxItem check)
        {
            if (dgvProductosGuia.Rows.Count > 0)
            {
                if (check.CheckState == CheckState.Unchecked)
                {
                    for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
                    {
                        if (dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString() == "TRASLADO TOTAL DE BIENES")
                        {
                            dgvProductosGuia.Rows.RemoveAt(i);
                        }

                        DataTable datosOT = clsOperacionesBL.Instancia.GetOperaciones_ListarDatosOts(Convert.ToInt32(txtOT.Text));
                        enProductoTolvas.entGTR_ProductoBienes_Descripcion = datosOT.Rows[0]["Nombre"].ToString();
                        enProductoTolvas.entGTR_ProductoBienes_Codigo = datosOT.Rows[0]["Producto"].ToString();

                        dgvProductosGuia.Rows.Add(enProductoTolvas.entGTR_ProductoBienes_Codigo, enProductoTolvas.entGTR_ProductoBienes_Descripcion, "1.00", "", "");
                        dgvProductosGuia.Rows[0].Cells["UnidadMedida"].Value = cbxUnidadMedidaTotal.SelectedValue.ToString();
                    }
                }
            }
                if (check.CheckState == CheckState.Checked)
                {
                    bool totalbienestraslado = false;

                    if (dgvProductosGuia.Rows.Count == 0)
                    {
                        dgvProductosGuia.Rows.Add("", "TRASLADO TOTAL DE BIENES", "", "", "");
                        //dgvProductosGuia.Rows[dgvProductosGuia.RowCount - 1].Cells["UnidadMedida"].Value = (UnidadMedida.Items[1] as DataRowView).Row[0].ToString();
                    }
                    else
                    {
                        dgvProductosGuia.Rows.Clear();
                        for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
                        {
                            if (dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString() == "TRASLADO TOTAL DE BIENES")
                            {
                                totalbienestraslado = true;
                                break;
                            }
                            else
                            {
                                totalbienestraslado = false;
                            }
                        }

                        if (totalbienestraslado == false)
                        {
                            dgvProductosGuia.Rows.Add("", "TRASLADO TOTAL DE BIENES", "", "", "");
                            //dgvProductosGuia.Rows[dgvProductosGuia.RowCount - 1].Cells["UnidadMedida"].Value = (UnidadMedida.Items[1] as DataRowView).Row[0].ToString();

                        }
                    }

                    
                }


        }

        private void Cargando()
        {
            dtRespuestaSunat_Guardado = new DataTable();
            int i = 0;

            if (TipoProgramacion != "TOLVAS" && TipoProgramacion != "LINDLEY" && TipoProgramacion != "VOLCAN")
            {

                while (esRespuesta == false)
                {
                    if (i == 15000) 
                    { 
                        esTiempoExcedido = true;
                        backgroundWorker1.CancelAsync();
                        break; 
                    }

                    dtRespuestaSunat_Guardado = obtenerRespuestaGuardado();

                    if (dtRespuestaSunat_Guardado != null)
                    {
                        if (dtRespuestaSunat_Guardado.Rows.Count > 0)
                        {
                            esRespuesta = true;
                        }

                    }

                    backgroundWorker1.ReportProgress(i, esRespuesta);
                    i++;

                }
            }
            else
            {

                backgroundWorker1.ReportProgress(i, true);


                

            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (backgroundWorker1.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }

                Cargando();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        public void CrearImagenCarga(int iteracion, Boolean esRespuesta)
        {
            if (esRespuesta == false && iteracion == 0) // CREA LA IMAGEN DE CARGA POR PRIMERA VEZ Y LA MUESTRA
            {

                Controls.Add(imgPictureBox);
                imgPictureBox.Visible = true;
                imgPictureBox.BringToFront();
                imgPictureBox.Show();
                btnGuardar.Enabled = false;
                tabControl1.Enabled = false;
            }
            if (esRespuesta) // AL OBTENER RESPUESTA LA IMAGEN SE OCULTA
            {
                imgPictureBox.Visible = false;
                imgPictureBox.Hide();
                btnGuardar.Enabled = true;
            }


        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (TipoProgramacion != "TOLVAS" && TipoProgramacion != "LINDLEY" && TipoProgramacion != "VOLCAN")
            {
                CrearImagenCarga(e.ProgressPercentage, Convert.ToBoolean(e.UserState));
                String ConcatenarRespuesta = "";
                bool esAprobado = false;


                if (esRespuesta)
                {
                    for (int i = 0; i < dtRespuestaSunat_Guardado.Rows.Count; i++)
                    {
                        if (dtRespuestaSunat_Guardado.Rows[i]["CodigoRespuesta"].ToString() == "2" || dtRespuestaSunat_Guardado.Rows[i]["CodigoRespuesta"].ToString() == "1")
                        {
                            esAprobado = true;
                        }
                    }

                        if (esAprobado)
                        {

                            CargarArchivoXML();
                            CargarGuiaEnPDF();
                            CargarRespuestaSunatCDR();
                            ConfirmarOtorgadoLeido();
                            ConsultarGuiaIndividual(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);

                            esAprobado = true;
                            entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "APROBADO";


                            for (int i = 0; i < dtRespuestaSunat_Guardado.Rows.Count; i++)
                            {
                                ConcatenarRespuesta = ConcatenarRespuesta + dtRespuestaSunat_Guardado.Rows[i]["Descripcion"].ToString() + "\n";

                            }

                            if (dtRespuestaSunat_Guardado.Rows.Count > 0)
                            {
                                entGuiaTransportista.entGRT_Respuesta_MensajeResultado = ConcatenarRespuesta;
                                entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = dtRespuestaSunat_Guardado.Rows[0]["CodigoRespuesta"].ToString();

                            }

                            clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaTransportista);

                        }
                        else
                        {
                            esAprobado = false;
                            entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "RECHAZADO";
                            RechazadoSunat = true;
                            AprobadoSunat = false;
                            CargarRespuestaSunatCDR();
                            ConfirmarOtorgadoLeido();
                            ConsultarGuiaIndividual(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);

                            for (int i = 0; i < dtRespuestaSunat_Guardado.Rows.Count; i++)
                            {
                                ConcatenarRespuesta = ConcatenarRespuesta + dtRespuestaSunat_Guardado.Rows[i]["Descripcion"].ToString() + "\n";

                            }

                            if (dtRespuestaSunat_Guardado.Rows.Count > 0)
                            {
                                entGuiaTransportista.entGRT_Respuesta_MensajeResultado = ConcatenarRespuesta;
                                entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = dtRespuestaSunat_Guardado.Rows[0]["CodigoRespuesta"].ToString();

                            }

                            clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaTransportista);


                        }
                }

                if (esRespuesta)
                {
                    if (esAprobado)
                    {
                        lblEstado.Text = entGuiaTransportista.entGRT_Respuesta_EstadoSunat;
                        pEstado.BackColor = Color.Lime;

                        if (resultadoRI.ent_Resultado != null)
                        {
                            AbrirPDF();
                            MessageBox.Show(entGuiaTransportista.entGRT_Respuesta_EstadoSunat + ": " + ConcatenarRespuesta, "Respuesta CDR SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Se Guardó Correctamente, pero el PDF no se pudo generar: " + resultadoRI.at_MensajeResultado, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        backgroundWorker1.CancelAsync();

    
                       

                    }
                    else
                    {
                        lblEstado.Text = entGuiaTransportista.entGRT_Respuesta_EstadoSunat;
                        pEstado.BackColor = Color.Red;
                        MessageBox.Show(entGuiaTransportista.entGRT_Respuesta_EstadoSunat + ": " + ConcatenarRespuesta, "Respuesta CDR SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                }


            }
            else // CUANDO LA OPERACION ES "TOLVAS o  LINDLEY" GENERA GUIA SIN RESPUESTA DE SUNAT
            {

                
                CargarArchivoXML();
                CargarGuiaEnPDF();
                ConfirmarOtorgadoLeido();
                entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "PENDIENTE";
                entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "ACEPTADO";
                entGuiaTransportista.entGRT_Respuesta_URL_GuiaSunat = "";
                entGuiaTransportista.entGRT_Respuesta_MensajeResultado = "PENDIENTE VALIDAR APROBACION O RECHAZO DE SUNAT";
                entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = "3";

                if (TipoProgramacion == "TOLVAS")
                {
                    ImprimirTicket();
                    ImprimirTicket();
                }

                ConsultarGuiaIndividual(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);
                


                if (clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaTransportista))
                {
                    lblEstado.Text = entGuiaTransportista.entGRT_Respuesta_EstadoSunat;
                    pEstado.BackColor = Color.Lime;
                    if (resultadoRI.ent_Resultado != null)
                    {
                        AbrirPDF();
                        MessageBox.Show("Generado Correctamente, PENDIENTE VALIDACION DE ESTADO DE GUIA EN SUNAT", "Respuesta Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Se Guardó Correctamente, pero el PDF no se pudo generar: " + resultadoRI.at_MensajeResultado, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    backgroundWorker1.CancelAsync();

                   
                }
                
            }
           


       }


        

        private void ConfirmarOtorgadoLeido()
        {


            // ********* CONSULTAR MASIVO ESTADO GUIAS **********

            ServiceGRT_QA.ene_ConsultarEstado consultarEstado = new ServiceGRT_QA.ene_ConsultarEstado();
            consultarEstado.at_CantidadConsultar = 1;
            consultarEstado.at_NumeroDocumentoIdentidad = "20439331918";

            // ********** CONFIRMAR ESTADO ***********

            ServiceGRT_QA.ene_ConfirmarEstado confirmarEstado = new ServiceGRT_QA.ene_ConfirmarEstado();
            confirmarEstado.at_NumeroDocumentoIdentidad = "20439331918";
            confirmarEstado.l_Comprobante = new ServiceGRT_QA.ArrayOfEn_ComprobanteConfirmarEstado();

            
            // TRAER RESPUESTA DE CONSLTA (DATOS DE GUIA)
           
            ServiceGRT_QA.ens_ConsultarEstadoGR responseConsulta = new ServiceGRT_QA.ens_ConsultarEstadoGR();
            responseConsulta = request.ConsultarEstadoGRT(consultarEstado);

            for (int i = 0; i < responseConsulta.l_ResultadoEstadoComprobante.Count; i++)
            {
                if (entGuiaTransportista.entGRT_Generales_Numero_M == responseConsulta.l_ResultadoEstadoComprobante[i].at_Numero &&
                    entGuiaTransportista.entGRT_Generales_Serie_M == responseConsulta.l_ResultadoEstadoComprobante[i].at_Serie)
                {

                    entGuiaTransportista.entGRT_Respuesta_FechaLeido = responseConsulta.l_ResultadoEstadoComprobante[i].ent_EstadoLeido.at_FechaLeido.ToString();
                    entGuiaTransportista.entGRT_Respuesta_FechaOtorgamiento = responseConsulta.l_ResultadoEstadoComprobante[i].ent_EstadoOtorgado.at_FechaOtorgado.ToString();
                
                }

                // ************ CONFIRMAR GUIA ESPECIFICA ******************
                ServiceGRT_QA.en_ComprobanteConfirmarEstado comprobanteConfirmarEstado = new ServiceGRT_QA.en_ComprobanteConfirmarEstado();
                comprobanteConfirmarEstado.at_Serie = Convert.ToString(responseConsulta.l_ResultadoEstadoComprobante[i].at_Serie);
                comprobanteConfirmarEstado.at_Numero = Convert.ToInt32(responseConsulta.l_ResultadoEstadoComprobante[i].at_Numero);
                confirmarEstado.l_Comprobante.Add(comprobanteConfirmarEstado);
          
            }

           
            request.ConfirmarEstadoGRT(confirmarEstado);

          
        }

        private void   CargarArchivoXML()
        {
            ens_ConsultarXML Archivo_XML = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, 0);

            while (Archivo_XML.at_NivelResultado == 0)
            {
                Archivo_XML = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, 0);
            }
            entGuiaTransportista.entGRT_Respuesta_XML_Archivo = Encoding.UTF8.GetString(Archivo_XML.ent_ResultadoXML.at_XML);
            
        }

        private void CargarGuiaEnPDF()
        {
            ene_ConsultarRI consultaRI = new ene_ConsultarRI();
            consultaRI.at_NumeroDocumentoIdentidad = entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M;
            consultaRI.ent_Comprobante = new en_ComprobanteConsultarRI();
            consultaRI.ent_Comprobante.at_Serie = entGuiaTransportista.entGRT_Generales_Serie_M;
            consultaRI.ent_Comprobante.at_Numero = entGuiaTransportista.entGRT_Generales_Numero_M;
            resultadoRI = new ens_ResultadoRI();
            resultadoRI = request.ConsultarRI_GRT(consultaRI);
 
            int contador = 0;
            while (resultadoRI.at_NivelResultado == 0)
            {
                resultadoRI = request.ConsultarRI_GRT(consultaRI);
                
                if (resultadoRI.at_NivelResultado != 0)
                {
                    entGuiaTransportista.entGRT_Respuesta_ArchivoPDF = resultadoRI.ent_Resultado.at_ArchivoRI;
                    entGuiaTransportista.entGRT_Respuesta_NombreRI = resultadoRI.ent_Resultado.at_NombreRI;
                    entGuiaTransportista.entGRT_Respuesta_FechaRI = resultadoRI.ent_Resultado.at_FechaGenerado;
                    break;
                }
                if (contador >= 300)
                {
                    break;
                }
                contador++;

            }

           


        }

        private void AbrirPDF()
        {

            Stream stream = new MemoryStream(resultadoRI.ent_Resultado.at_ArchivoRI);
            AbrirPdf open = new AbrirPdf();
            open.pdfViewer1.LoadDocument(stream);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.FileName = entGuiaTransportista.entGRT_Generales_Serie_M + "-" + entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") + " " + entGuiaTransportista.conductor.Replace(",", "");
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "PDF files (.pdf)|.pdf|All files (.)|.";
            //saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            open.ShowDialog();

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

        private void ConsultarGuiaIndividual(string NumroDocumentoIdentidad, string Serie, int Numero)
        {
            //INSTANCIO LA CLASE NECESARIA PARA INDICARLE A SUNAT QUE GUIA QUIERO CONSULTAR (SERIE , NUEMRO)
            respuestaSunat = new ene_ConsultarComprobanteIndividual();
            respuestaSunat.at_NumeroDocumentoIdentidad = NumroDocumentoIdentidad;
            respuestaSunat.at_Serie = Serie;
            respuestaSunat.at_Numero = Numero;

            // INSTANCIO LA CLASE QUE ALMACENARÁ LA RESPUESTA DE SUNAT
            consultaIndividual = new ens_ConsultarComprobanteIndividual();
            consultaIndividual = request.ConsultaIndividualGRT(respuestaSunat); // AQUI OBTENGO LA RESPUESTA SUNAT "consultaIndividual"

            
            entGuiaTransportista.entGRT_Respuesta_FechaGeneracion = consultaIndividual.ent_InformacionComprobante.at_FechaGeneracion;
            entGuiaTransportista.entGRT_Respuesta_NivelResultado = consultaIndividual.at_NivelResultado;
            entGuiaTransportista.entGRT_Respuesta_Guardar_Sunat = consultaIndividual.at_MensajeResultado;
            entGuiaTransportista.entGRT_Respuesta_FechaTransmision = consultaIndividual.ent_InformacionComprobante.at_FechaTransmision;
            

        }

        private void FrmGuiaElectronicaTransportista_FormClosing(object sender, FormClosingEventArgs e)
        {
            //backgroundWorker1.CancelAsync();
        }

        private void cbxUnidadMedidaTotal_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (dgvProductosGuia.Rows.Count > 0)
                {
                    dgvProductosGuia.Rows[dgvProductosGuia.RowCount - 1].Cells["UnidadMedida"].Value = cbxUnidadMedidaTotal.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstSubcontratado_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRazonSocialSubContra, ref lstSubcontratado, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstSubcontratado_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRazonSocialSubContra, ref  lstSubcontratado, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstSubcontratado.SelectedItems[0];
                    txtRazonSocialSubContra.Text = ItemActual.SubItems[1].Text;
                    txtRucSubContra.Text = ItemActual.SubItems[2].Text;
                    txtTipoDocumentoSubContra.Tag = ItemActual.SubItems[3].Text;
                    txtTipoDocumentoSubContra.Text = ItemActual.SubItems[4].Text;
         

                    entGuiaTransportista.entGRT_SubContratista_RazonSocial = txtRazonSocialSubContra.Text;
                    entGuiaTransportista.entGRT_SubContratista_NumeroDocumentoIdentidad = txtRucSubContra.Text;
                    entGuiaTransportista.entGRT_SubContratista_TipoDocumentoIdentidad = txtTipoDocumentoSubContra.Tag.ToString();

                    if (txtRazonSocialSubContra.Tag == null)
                    {
                        txtTipoDocumentoSubContra.Clear();
                        txtRucSubContra.Clear();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstSubcontratado_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRazonSocialSubContra, ref lstSubcontratado, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRazonSocialSubContra_Enter(object sender, EventArgs e)
        {
            txtRazonSocialSubContra.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRazonSocialSubContra_Leave(object sender, EventArgs e)
        {
            txtRazonSocialSubContra.BackColor = Color.White;
        }

        private void txtRazonSocialSubContra_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRazonSocialSubContra, ref  lstSubcontratado, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    groupDepDestin.Select();
                    txtDepartamentoLLegada.Focus();
                    txtDireccionDestino.Enabled = true;


                    if (txtRazonSocialSubContra.Tag == null)
                    {
                        txtTipoDocumentoSubContra.Clear();
                        txtRucSubContra.Clear();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtRazonSocialSubContra_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRazonSocialSubContra, ref lstSubcontratado, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstFleteTercero_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRazonSocialPagadorTercero, ref lstFleteTercero, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstFleteTercero_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRazonSocialPagadorTercero, ref  lstFleteTercero, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstFleteTercero.SelectedItems[0];
                    txtRazonSocialPagadorTercero.Text = ItemActual.SubItems[1].Text;
                    txtRucPagadorTercer.Text = ItemActual.SubItems[2].Text;
                    txtTipoDocTerceroFlete.Tag = ItemActual.SubItems[3].Text;
                    txtTipoDocTerceroFlete.Text = ItemActual.SubItems[4].Text;


                    entGuiaTransportista.entGRT_SubContratista_RazonSocial = txtRazonSocialPagadorTercero.Text;
                    entGuiaTransportista.entGRT_SubContratista_NumeroDocumentoIdentidad = txtRucPagadorTercer.Text;
                    entGuiaTransportista.entGRT_SubContratista_TipoDocumentoIdentidad = txtTipoDocTerceroFlete.Tag.ToString();

                    if (txtRazonSocialPagadorTercero.Tag == null)
                    {
                        txtTipoDocumentoSubContra.Clear();
                        txtRucSubContra.Clear();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lstFleteTercero_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRazonSocialPagadorTercero, ref lstFleteTercero, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRazonSocialPagadorTercero_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRazonSocialPagadorTercero, ref  lstFleteTercero, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista))
                {
                    groupDepDestin.Select();
                    txtDepartamentoLLegada.Focus();
                    txtDireccionDestino.Enabled = true;


                    if (txtRazonSocialPagadorTercero.Tag == null)
                    {
                        txtTipoDocTerceroFlete.Clear();
                        txtRazonSocialPagadorTercero.Clear();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void txtRazonSocialPagadorTercero_Enter(object sender, EventArgs e)
        {
            txtRazonSocialPagadorTercero.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRazonSocialPagadorTercero_Leave(object sender, EventArgs e)
        {
            txtRazonSocialPagadorTercero.BackColor = Color.White;
        }

        private void txtRazonSocialPagadorTercero_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRazonSocialPagadorTercero, ref lstFleteTercero, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstCarreta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstCarreta.SelectedItems[0];
                    txtCarreta.Text = ItemActual.SubItems[1].Text;
                    entGuiaTransportista.carreta = txtCarreta.Text.Replace("-","").Replace(".","");
                    entGuiaTransportista.idCarreta = txtCarreta.Tag.ToString();
                    TipoVehiculoCarreta = ItemActual.SubItems[4].Text;
                    if (TipoVehiculoCarreta != "CISTERNA")
                    {
                        entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = ItemActual.SubItems[2].Text;


                        if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta != "")
                        {
                            entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta;
                        }
                    }


                    groupConductor2.Select();
                    txtConductor2.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }

        }

        private void lstCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCarreta_Enter(object sender, EventArgs e)
        {
            txtCarreta.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtCarreta_Leave(object sender, EventArgs e)
        {
            txtCarreta.BackColor = Color.White;
        }

        private void txtCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    txtObservaciones.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstCarreta2_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCarreta2, ref lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstCarreta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCarreta2, ref  lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstCarreta2.SelectedItems[0];
                    txtCarreta2.Text = ItemActual.SubItems[1].Text;
                    entGuiaTransportista.carreta = txtCarreta2.Text;
                    entGuiaTransportista.idCarreta = txtCarreta2.Tag.ToString();
                    entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = ItemActual.SubItems[2].Text;
                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null)
                    {
                        entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta;
                    }

                    
                    txtObservaciones.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstCarreta2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCarreta2, ref lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCarreta2_Enter(object sender, EventArgs e)
        {
            txtCarreta2.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtCarreta2_Leave(object sender, EventArgs e)
        {
            txtCarreta2.BackColor = Color.White;
        }

        private void txtCarreta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtCarreta2, ref  lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
            {
                txtObservaciones.Select();
                

            }
        }

        private void txtCarreta2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtCarreta2, ref lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDocumentosRelacionados_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            int columnIndex = dgvDocumentosRelacionados.CurrentCell.ColumnIndex;

            if (dgvDocumentosRelacionados.Columns[columnIndex].Name == "TipoDocRelacion")
            {

                DataGridViewComboBoxEditingControl dgvCombo = e.Control as DataGridViewComboBoxEditingControl;

                if (dgvCombo != null)
                {
                    //
                    // se remueve el handler previo que pudiera tener asociado, a causa ediciones previas de la celda
                    // evitando asi que se ejecuten varias veces el evento
                    //
                    dgvCombo.SelectedIndexChanged -= new EventHandler(dvgCombo_SelectedIndexChanged);
                    dgvCombo.SelectedIndexChanged += new EventHandler(dvgCombo_SelectedIndexChanged);

        
                }
            }

        }

        private void dvgCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var combo = sender as System.Windows.Forms.ComboBox;

                if (combo != null)
                {
                    if (combo.SelectedIndex < 0)
                        return;
                   
                    dgvDocumentosRelacionados.CurrentRow.Cells["NombreDocumento"].Value = Utilitario.Instancia.QuitarTildes((combo.Items[combo.SelectedIndex] as DataRowView).Row[1].ToString());
                   
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }

        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvTrasladoProgramado.Rows.RemoveAt(dgvTrasladoProgramado.CurrentRow.Index);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void dtgListaGuiasTransportista_Click(object sender, EventArgs e)
        {
            try
            {
                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    if (dgvListaGuiaTraspExpressVista.RowCount > 0)
                    {
                        txtCodviaje.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje")).Length > 0 ? Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje")) : "";
                        entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "OT"));
                        txtOT.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "OT"));
                        entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Linea"));


                        if (Convert.ToInt16(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdRuta")) > 0)
                        {
                            txtRuta.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                            txtRuta.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdRuta"));
                            entGuiaTransportista.idRuta = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdRuta"));
                            entGuiaTransportista.ruta = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                        }

                        txtPesoTotal.Value = Convert.ToDecimal(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Cantidad"));
                    }
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFechaTraslado_ValueChanged_1(object sender, EventArgs e)
        {
            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                    dtpFechaTraslado.MinDate = DateTime.Now;
            }
        }

        public void FrmGuiaElectronicaTransportista_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
            try
            {
                if (TipoProgramacion != "TOLVAS")
                {
                    if (esConsolidado == false && TipoOperacion != Utilitario.TipoOperacion.Editar) { CargarLista(this); }
                    else if (esConsolidado == true && TipoOperacion != Utilitario.TipoOperacion.Editar) { this.DialogResult = System.Windows.Forms.DialogResult.OK; }
                }
                else { CargarListaTolvas(esRegistroExitoso,this); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */ 
        }

        private void rbtRetorno_CheckedChanged(object sender, EventArgs e)
        {
            string destinatario = string.Empty;
            string remitente = string.Empty;
            string direccionPartida = string.Empty;
            string direccionDestino = string.Empty;

           // if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            //{
                if (rbtRetorno.Checked)
                {
                    destinatario = txtEmpresaDestinatario.Text;
                    remitente = txtEmpresaRemitente.Text;

                    txtEmpresaRemitente.Text = destinatario;
                    txtEmpresaRemitente_KeyUp(this, new KeyEventArgs((Keys.Space)));
                    lstEmpresaRemitente.Select();
                    lstEmpresaRemitente_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstEmpresaRemitente_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    txtEmpresaDestinatario.Text = remitente;
                    txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Space)));
                    lstEmpresaDestinatario.Select();
                    lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    //txtCorreoPrimario.Text = entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M;

                    //INVIERTO EL UBIGEO
                   // DataTable dtRuta = clsOperacionesBL.Instancia.ReportesApp_ListarRuta_GuiaElectronica(entGuiaTransportista.ruta);
                   // if (dtRuta.Rows.Count > 0)
                   // {
                        string tempPuntoPartida_Ubigeo = "";
                        string tempPuntoDestino_Ubigeo = "";
                        string tempDireccionPartida = "";
                        string tempDireccionDestino = "";

                        //entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M;

                        // cambiar orden 
                        tempPuntoDestino_Ubigeo = entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M;
                        tempPuntoPartida_Ubigeo = entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M;
                        tempDireccionPartida = DireccionUbigeoFin;
                        tempDireccionDestino = DireccionUbigeoOrigen;

                        entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = tempPuntoPartida_Ubigeo; // ubigeo destino
                        DireccionUbigeoFin = tempDireccionDestino;
                        txtUbigeoDestino.Text = tempDireccionDestino;

                        entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = tempPuntoDestino_Ubigeo; // ubigeo partida
                        DireccionUbigeoOrigen = tempDireccionPartida;
                        txtUbigeoPartida.Text = tempDireccionPartida;

                        /*entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = dtRuta.Rows[0]["UbigeoFin"].ToString(); // ubigeo destino
                        DireccionUbigeoOrigen = dtRuta.Rows[0]["DescripcionFin"].ToString();

                        entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = dtRuta.Rows[0]["UbigeoOrigen"].ToString();// ubigeo partida
                        DireccionUbigeoFin = dtRuta.Rows[0]["DescripcionOrigen"].ToString();*/

                        txtDireccionPartida.Enabled = true;
                        txtDireccionDestino.Enabled = true;

                    direccionPartida = txtDireccionPartida.Text;
                    direccionDestino = txtDireccionDestino.Text;

                    txtDireccionPartida.Text = direccionDestino;
                    txtDireccionPartida_KeyUp(this, new KeyEventArgs((Keys.Space)));
                    lstDireccionPartida.Select();
                    lstDireccionPartida_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstDireccionPartida_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                    txtDireccionDestino.Text = direccionPartida;
                    txtDireccionDestino_KeyUp(this, new KeyEventArgs((Keys.Space)));
                    lstDireccionDestino.Select();
                    lstDireccionDestino_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstDireccionDestino_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                }

            /*if (TipoOperacion == Utilitario.TipoOperacion.Editar)
              {

              }*/
       }

        private void groupBox17_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtPesoTotal_ValueChanged(object sender, EventArgs e)
        {
            if(dgvProductosGuia.Rows.Count > 0)
            {
                dgvProductosGuia.Rows[0].Cells["Peso"].Value = txtPesoTotal.Value.ToString();
            }
            
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }


        private void ImprimirTicket()
        {
            PrintDocument printDoc = new PrintDocument();
            string remitentes = "";

                    Ticket ticket = new Ticket();
                    ticket.MaxChar = 40;
                    ticket.MaxCharDescription = 30;
            
                    ticket.AddSubHeaderLine2("GRUPO TRANSPESA S.A.C.");
                    ticket.FontCodigo = 12;
                    ticket.AddSubHeaderLine("        GUÍA DE TRANSPORTISTA" + "                                                               ");
                    ticket.FontSize = 8;
                    ticket.AddSubHeaderLine("GUIA NRO: " + entGuiaTransportista.entGRT_Generales_Serie_M + "-" + entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") + "                          ");
                    ticket.AddSubHeaderLine("FECHA EMISION: " + Convert.ToDateTime(entGuiaTransportista.entGRT_Generales_FechaEmision_M).ToShortDateString()+" " + entGuiaTransportista.entGRT_Generales_HoraEmision_M  +"                          ");
                    ticket.AddSubHeaderLine("FECHA TRASLADO: " + Convert.ToDateTime(entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M).Date.ToString() + "                          ");
                    ticket.AddSubHeaderLine("PUNTO PARTIDA: " + "");
                    ticket.AddSubHeaderLine(entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M + ", " + entGuiaTransportista.entGRT_PuntoPartida_Departamento + ", " + entGuiaTransportista.entGRT_PuntoPartida_Provincia + ", " + entGuiaTransportista.entGRT_PuntoPartida_Distrito + "                    ");
                    ticket.AddSubHeaderLine("                                         ");
                    ticket.AddSubHeaderLine("PUNTO LLEGADA: " + "");
                    ticket.AddSubHeaderLine(entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M + ", " + entGuiaTransportista.entGRT_PuntoDestino_Departamento + ", " + entGuiaTransportista.entGRT_PuntoDestino_Provincia + ", " + entGuiaTransportista.entGRT_PuntoDestino_Distrito + "                    ");
                    ticket.AddSubHeaderLine("                                         ");
                    ticket.AddSubHeaderLine("RUC REMITENTE: " + entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M + "                                         ");
                    for (int i = 0; i < dgvDocumentosRelacionados.Rows.Count; i++)
                    {
                        remitentes = remitentes + dgvDocumentosRelacionados.Rows[i].Cells["NumeroDocRelacion"].Value.ToString() + ", "; 
                    }
                    ticket.AddSubHeaderLine("GUIA NRO REMITENTE: " + remitentes);    
                    //ticket.AddSubHeaderLine("========================================");
                    ticket.AddSubHeaderLine("REMITENTE: " + entGuiaTransportista.entGRT_Remitente_RazonSocial_M + "                                   ");
                    ticket.AddSubHeaderLine("RUC DESTINATARIO: " + entGuiaTransportista.entGRT_Destinatario_NumeroDocumentoIdentidad_M + "                                   ");
                    ticket.AddSubHeaderLine("DESTINATARIO: " + entGuiaTransportista.entGRT_Destinatario_RazonSocial_M );
                    //ticket.AddSubHeaderLine("========================================");
                    ticket.AddSubHeaderLine("TRACTO: " + entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M + "                                   ");
                    ticket.AddSubHeaderLine("CARRETA: " + entGuiaTransportista.carreta + "                                   ");
                    ticket.AddSubHeaderLine("CONDUCTOR: " + txtConductor.Text + "                                   ");
                    ticket.AddSubHeaderLine("BREVETE: " + txtLicencia.Text + "                                   ");
                    ticket.AddSubHeaderLine("                                         ");
                   //ticket.AddSubHeaderLine("========================================");
                    ticket.AddSubHeaderLine("PRODUCTO: " );
                    ticket.AddSubHeaderLine(dgvProductosGuia.Rows[0].Cells["Descripcion"].Value.ToString() + "                                   ");

                   /* for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
                    {
                        ticket.AddItem(dgvProductosGuia.Rows[i].Cells["Cantidad"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Codigo"].Value.ToString());
                       
                    }*/

                    ticket.AddSubHeaderLine("PESO BRUTO: " + entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M + "                                   ");
                    ticket.HeaderImage = Resources.Codigo_QR;
                    ticket.AddFooterLine("      " + entGuiaTransportista.entGRT_Respuesta_CodigoHash);
                    ticket.AddFooterLine("                                   ");
                    ticket.AddFooterLine("        ** VIAJA CON CUIDADO **");
                
                    if (printDoc.PrinterSettings.IsDefaultPrinter)
                    {
                        if (Utilitario.Instancia.SesionUsuario.Equals("SRUIZ") || Utilitario.Instancia.SesionUsuario.Equals("TDELGAD") || Utilitario.Instancia.SesionUsuario.Equals("LGRADOS"))
                        {
                            ticket.copias = 2;
                        }

                        string x = printDoc.PrinterSettings.PrinterName;
                        ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    }
                    else
                    {
                        MessageBox.Show("No tiene una impresora configurada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }

          }

        private void checkCompletado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtEmpresaDestinatario_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void lblNumero_Click(object sender, EventArgs e)
        {

        }

        private void dgvDocumentosRelacionados_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDocumentosRelacionados.Columns[e.ColumnIndex].Name == "PesoGuia")
                {
                    Decimal totalColumna = 0;
                    foreach (DataGridViewRow row in dgvDocumentosRelacionados.Rows)
                    {
                        Decimal pedido = 0;
                        if (!Decimal.TryParse(Convert.ToString(row.Cells["PesoGuia"].Value), out pedido))
                            continue;

                        Decimal totalFila = Convert.ToDecimal(row.Cells["PesoGuia"].Value);
                        totalColumna = Convert.ToDecimal(totalFila + totalColumna);
                    }

                    DataGridViewRow rowTotal = dgvDocumentosRelacionados.Rows[dgvDocumentosRelacionados.Rows.Count - 1];
                    txtPesoTotal.Text = string.Format("{0:0.00}", totalColumna);

                    if (Convert.ToDecimal(txtPesoTotal.Text) > 0)
                    {
                        btnGuardar.Enabled = true;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void dgvDocumentosRelacionados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDocumentosRelacionados.Columns[e.ColumnIndex].Name == "PesoGuia")
                {
                   /* double suma = Convert.ToDouble(txtPesoTotal.Value) + Convert.ToDouble(dgvDocumentosRelacionados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * Convert.ToDouble("1.00");
                    txtPesoTotal.Value = Convert.ToDecimal(suma);*/

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        public void rbTUC_Click(object sender, EventArgs e)
        {
            if (rbTUC.Checked == true)
            {
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_TarjetaCirculacion(txtPlaca.Text, 5);
                entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion = Convert.ToString(dtRespuesta.Rows[0]["CODIGO"]);

                if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null)
                {
                    txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;
                }
            }
        }

        private void rbMatpel_Click(object sender, EventArgs e)
        {
            if (rbMatpel.Checked == true)
            {
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_TarjetaCirculacion(txtPlaca.Text, 34);
                entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion = Convert.ToString(dtRespuesta.Rows[0]["CODIGO"]);

                if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion != null)
                {
                    txtTarjetaCirculacion.Text = entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion.Length == 0 ? "0" + entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion : entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion;
                }
            }
        }
    }

}

