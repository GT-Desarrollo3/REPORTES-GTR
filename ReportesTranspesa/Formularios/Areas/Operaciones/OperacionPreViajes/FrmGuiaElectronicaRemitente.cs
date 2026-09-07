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
using ReportesTranspesa.ServiceGRR_QA;
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


namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmGuiaElectronicaRemitente: Form
    {
        public DataTable dtDepartamento = new DataTable();
        public DataTable dtProvincia = new DataTable();
        public DataTable dtCiudad = new DataTable();
        public DataTable dtTipoServicio = new DataTable();
        public DataTable dtEmpresasGrupo;
        public DataTable dtDireccionesRuta;
        public DataTable dtDireccionesRuta2;
        public DataTable dtOts;
        public DataTable dtCorreos;
        public DataTable dtFechaHora;
        public DataTable dtRespuestaSunat_Guardado;
        public DataTable dtRespuesta_CDR;
        public DataTable dtRespuestaXML_CDR;

        public bool esTiempoExcedido = false;
        public bool Publico = false;
        public bool Privado = true;
        public string TipoProgramacion = "LIMAGAS"; // (1) TOLVAS , (2) LINLEY , (3) LIMAGAS , (4) General
        public int TipoOperacion = -777;
        public bool TipoTrasladoProgramado = false; // EN CASO EL TRANSPORTE SEA CON VARIAS UNIDADES Y CONDUCTORES
        public bool ConProveedor = false; // ES LA EMPRESA QUIEN NOS SUBCONTRATA 
        public bool ConTransportista = false; // CUANDO PAGA EL SERVICIO UN TERCERO 07 - NO ES SUBCONTRATADO NI REMITENTE
        public bool ConVehiculo = false;
        public bool esEstablecimientoPropio = false;
        string[] TipoServiciosActualizar;
        Boolean VALIDACIONES = true;
        Boolean esRespuesta = false;
        // entidad guia transportista
        public clsGRR entGuiaRemitente = new clsGRR();
        public entConductorR entNuevoConductor = new entConductorR();
        public string CarpetaAlacenamientoLogErrores = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\LogErrores\";
        public string CarpetaLogSoapError = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\SOAP\";
        public Boolean AprobadoSunat = false; // atributo true cuando retorne  el codigo 2  
        public Boolean RechazadoSunat = false; // atributo false cuando retorne un codigo mayor a 2

        // INSTANCIAS DE REGISTRO DE GUIA
        ServicioGuiaRemisionRemitenteClient request;
        ens_Respuesta respuesta;
        ene_ConsultarComprobanteIndividual respuestaSunat;
        ens_ConsultarComprobanteIndividual consultaIndividual;
        ene_ConsultarXML consultarXML_CDR;
        ens_ConsultarXML responseXML;
        ens_ResultadoRI resultadoRI;
        PictureBox imgPictureBox = new PictureBox();
        public event CargarListaRemitenteEventHandler CargarListaRemitente;
        public delegate void CargarListaRemitenteEventHandler(FrmGuiaElectronicaRemitente remitente);

        // DATOS DE TERCERO
        string placaTercero = string.Empty;
        string tarjetaCirculacion = string.Empty;
        string nombretipovehiculo = string.Empty;
        int idtipovehiculo = 0;

        public FrmGuiaElectronicaRemitente()
        {
            InitializeComponent();
            AsegurarColumnasCarretaTrasladoProgramado();

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                cbxTipoServicios.Popup -= cbxTipoServicios_Popup;
                cbxSerieGuia.SelectedValueChanged -= cbxSerieGuia_SelectedValueChanged;
            }

            if (TipoOperacion == Utilitario.TipoOperacion.Editar) { cbxSerieGuia.SelectedValueChanged -= cbxSerieGuia_SelectedValueChanged; }
        }

        private void FrmGenerarGuia_Load(object sender, EventArgs e)
        {
            try
            {
                dtTipoServicio.Columns.Add("CodServicio", typeof(String));
                dtTipoServicio.Columns.Add("Descripcion", typeof(String));
                CrearGif();
                CargaInicial();
                HabilitarBotonCorreo();

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios_EditValueChanged(this, null);
                    cbxTipoServicios.EditValueChanged -= cbxTipoServicios_EditValueChanged;
                    cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                    cbxModalidadTransporte_SelectedValueChanged(this, null);
                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    if (entGuiaRemitente.entGRR_Respuesta_EstadoSunat == "APROBADO")
                    {
                        btnGuardar.Enabled = false;
                        MessageBox.Show("La guia se encuentra aprobada, no es posible editar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    txtEmpresaDestinatario.Text = entGuiaRemitente.entGRR_Destinatario_RazonSocial_M;
                    txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                    lstEmpresaDestinatario.Select();
                    lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                    lblEstado.Text = entGuiaRemitente.entGRR_Respuesta_EstadoSunat;
                    
                    CargarProductos();
                    CargarTipoServicioActualizar();
                    CargarUbigeo();
                    CargarDireccion();
                    CargarProveedor();
                    CargarTransportista();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CargarTransportista()
        {
            if (ConTransportista)
            {
                txtRazonSocialTransportista.Text = entGuiaRemitente.entGRR_Transportista_RazonSocial;
                txtRazonSocialPagadorTercero_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                lstTransportista.Select();
                lstFleteTercero_KeyUp(this, new KeyEventArgs(Keys.Down));
                lstFleteTercero_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
            }
        }

        private void CargarProveedor()
        {
            if (ConProveedor)
            {
                txtRazonSocialProveedor.Text = entGuiaRemitente.entGRR_Proveedor_RazonSocial;
                txtRazonSocialSubContra_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                lstProveedor.Select();
                lstSubcontratado_KeyUp(this, new KeyEventArgs(Keys.Down));
                lstSubcontratado_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
            }
        }

        private void CargarPlacaConductor()
        {
            if (entGuiaRemitente.xml_entGRR_Conductor_M.Length > 0)
            {
                DataTable dtConductoresEditar = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaRemitente.xml_entGRR_Conductor_M);

                if (dtConductoresEditar.Rows.Count > 0)
                {
                    if (TipoTrasladoProgramado)
                    {
                        for (int i = 0; i < dtConductoresEditar.Rows.Count; i++)
                        {

                            if (Convert.ToInt32(dtConductoresEditar.Rows[i]["idConductor"]) == 0)
                            {
                                VALIDACIONES = false;
                                MessageBox.Show("No se pudo cargar el id del Conductor " + dtConductoresEditar.Rows[i]["Nombres_Conductor"].ToString() + " " + dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        txtCarreta.Text = dtConductoresEditar.Rows[0]["Carreta"].ToString();
                        txtCarreta.Tag = dtConductoresEditar.Rows[0]["idCarreta"].ToString();
                        entGuiaRemitente.entGRR_Vehiculo_NumeroPlaca_M = dtConductoresEditar.Rows[0]["Tracto"].ToString();
                        entGuiaRemitente.idtracto = dtConductoresEditar.Rows[0]["idPlaca"].ToString(); 

                        if (dtConductoresEditar.Rows[0]["idPlaca"].ToString() == "0")
                        {
                            checkTercero.CheckedChanged -= checkTercero_CheckedChanged;
                            checkTercero.Checked = true;
                        }

                        entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion = dtConductoresEditar.Rows[0]["PlacaTarjetaCircula"].ToString();
                        txtTarjetaCirculacion.Text = entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion;
                        entGuiaRemitente.idconductor = Convert.ToInt32(dtConductoresEditar.Rows[0]["idConductor"]);
                        entGuiaRemitente.conductor = dtConductoresEditar.Rows[0]["Nombres_Conductor"].ToString() + " " + dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString();
                        txtConductor.Text = dtConductoresEditar.Rows[0]["Nombres_Conductor"].ToString() + " " + dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString();
                        entNuevoConductor.entGRR_Conductor_Nombres_M = dtConductoresEditar.Rows[0]["Nombres_Conductor"].ToString();
                        entNuevoConductor.entGRR_Conductor_Apellidos_M = dtConductoresEditar.Rows[0]["Apellidos_Conductor"].ToString();
                        entNuevoConductor.entGRR_Conductor_Licencia_M = dtConductoresEditar.Rows[0]["Licencia_Conductor"].ToString();
                        txtLicencia.Text = dtConductoresEditar.Rows[0]["Licencia_Conductor"].ToString();
                        txtDocIdentidad.Text = dtConductoresEditar.Rows[0]["NumeroDocIdentidad_Conductor"].ToString();
                        entNuevoConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M = dtConductoresEditar.Rows[0]["NumeroDocIdentidad_Conductor"].ToString();
                        entNuevoConductor.entGRR_Conductor_TipoDocumentoIdentidad_M = dtConductoresEditar.Rows[0]["TipoDocIdentidad_Conductor"].ToString();
                        txtTipoDocumentoIdentidad.Text = dtConductoresEditar.Rows[0]["NombreTipoDoc"].ToString();
                        txtTipoDocumentoIdentidad.Tag = dtConductoresEditar.Rows[0]["TipoDocIdentidad_Conductor"].ToString();
                        
                        entGuiaRemitente.entConductor = entNuevoConductor;
                    }
                }
            }
        }

        private void AsegurarColumnasCarretaTrasladoProgramado()
        {
            if (!dgvTrasladoProgramado.Columns.Contains("idCarreta"))
            {
                DataGridViewTextBoxColumn idCarreta = new DataGridViewTextBoxColumn();
                idCarreta.Name = "idCarreta";
                idCarreta.HeaderText = "idCarreta";
                idCarreta.Visible = false;

                dgvTrasladoProgramado.Columns.Insert(dgvTrasladoProgramado.Columns["PlacaTarjetaCircula"].Index, idCarreta);
            }

            if (!dgvTrasladoProgramado.Columns.Contains("Carreta"))
            {
                DataGridViewTextBoxColumn carreta = new DataGridViewTextBoxColumn();
                carreta.Name = "Carreta";
                carreta.HeaderText = "Carreta";

                dgvTrasladoProgramado.Columns.Insert(dgvTrasladoProgramado.Columns["PlacaTarjetaCircula"].Index, carreta);
            }

            if (!dgvTrasladoProgramado.Columns.Contains("CarretaTarjetaCircula"))
            {
                DataGridViewTextBoxColumn carretaTarjeta = new DataGridViewTextBoxColumn();
                carretaTarjeta.Name = "CarretaTarjetaCircula";
                carretaTarjeta.HeaderText = "CarretaTarjetaCircula";
                carretaTarjeta.Visible = false;

                dgvTrasladoProgramado.Columns.Insert(dgvTrasladoProgramado.Columns["PlacaTarjetaCircula"].Index + 1, carretaTarjeta);
            }
        }

        private void CargarDireccion()
        {
            txtDireccionPartida.Text = entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M;
            txtDireccionDestino.Text = entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M;
        }

        private void CargarUbigeo()
        {
            txtUbigeoPartida.Text = entGuiaRemitente.entGRR_PuntoPartida_NombreUbigeo;
            txtUbigeoPartida_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstUbigeoPartida.Select();
            lstUbigeoPartida_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstUbigeoPartida_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

            txtUbigeoLlegada.Text = entGuiaRemitente.entGRR_PuntoDestino_NombreUbigeo;
            txtUbigeoLlegada_KeyUp(this, new KeyEventArgs((Keys.Escape)));
            lstUbigeoLlegada.Select();
            lstUbigeoLlegada_KeyUp(this, new KeyEventArgs(Keys.Down));
            lstUbigeoLlegada_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
        }

        private void CrearGif()
        {
            imgPictureBox.Location = new System.Drawing.Point(360, 253);
            imgPictureBox.Size = new System.Drawing.Size(239, 226);
            imgPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            imgPictureBox.Image = Resources.cargando_resultados;
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
            dtFechaHora = clsOperacionesBL.Instancia.ObtenerFechaHoraServidorGuiaElectronica();
            entGuiaRemitente.entGRR_ControlOtorgamiento_Estado_M = true;
            dtpFechaRegistro.Text = Convert.ToDateTime(dtFechaHora.Rows[0]["FechaServidor"].ToString()).ToString("dd/MM/yyyy");

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                entGuiaRemitente.entGRR_Generales_FechaIncioTraslado = dtpFechaTraslado.Value.ToString();
                CargarCombos();
            }

            if (TipoOperacion == Utilitario.TipoOperacion.Editar)
            {
                dtpFechaRegistro.Text = entGuiaRemitente.entGRR_Generales_FechaEmision_M;
                dtpFechaTraslado.Text = entGuiaRemitente.entGRR_Generales_FechaIncioTraslado;
                txtObservaciones.Text = entGuiaRemitente.entGRR_Generales_Observacion;
                lblEstado.Text = entGuiaRemitente.entGRR_Respuesta_EstadoSunat;
                CargarCombos();
            }
        }
        
        private void CargarTipoServicioActualizar()
        {
            cbxTipoServicios_EditValueChanged(this, null);
            cbxTipoServicios.EditValueChanged -= cbxTipoServicios_EditValueChanged;
            cbxTipoServicios.Popup += cbxTipoServicios_Popup;

            TipoServiciosActualizar = entGuiaRemitente.xml_entGRR_TipoServicio.Split(',');
            if (TipoServiciosActualizar.Length > 0)
            {
                for (int i = 0; i < TipoServiciosActualizar.Length; i++)
                {
                    if (TipoServiciosActualizar[i] == "00") // vehiculo y conductor
                    {
                        CargarPlacaConductor();
                        MostrarOcultarOpciones();
                        TipoTrasladoProgramado = false;
                    }

                    if (TipoServiciosActualizar[i] == "01") // Trasbordo Programado
                    {
                        CargarPlacaConductor();
                        MostrarOcultarOpciones();
                        TipoTrasladoProgramado = true;
                    }

                    if (TipoServiciosActualizar[i] == "02") // Traslado de vehiculo M1 y L
                    {
                         cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Checked;
                         MostrarOcultarOpciones();
                         txtRazonSocialProveedor.Text = entGuiaRemitente.entGRR_Proveedor_RazonSocial;
                         txtRazonSocialSubContra_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                         lstProveedor.Select();
                         lstSubcontratado_KeyUp(this, new KeyEventArgs(Keys.Down));
                         lstSubcontratado_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    }

                    if (TipoServiciosActualizar[i] == "03") // "Retorno de vehiculo con envases vacios"
                    { cbxTipoServicios.Properties.Items[3].CheckState = CheckState.Checked; }

                    if (TipoServiciosActualizar[i] == "04") // Retorno de vehiculo vacio
                    { cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Checked; } // Activo el check de  "Pago de flete de subcontratado"
                }
            }
        }

        private void CargarProductos()
        {
            txtPesoTotal.Value = entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M;
            cbxUnidadMedidaTotal.SelectedValue = entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M;

            if (entGuiaRemitente.xml_entGRR_Productos_Bienes != "")
            {
                DataTable dtProductos = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaRemitente.xml_entGRR_Productos_Bienes);
                if (dtProductos.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProductos.Rows.Count; i++)
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

        private void CargarCombos()
        {
            dtEmpresasGrupo = clsOperacionesBL.Instancia.ReportesApp_ListarEmpresasGrupo();

            if (dtEmpresasGrupo.Rows.Count > 0)
            {
                entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M = dtEmpresasGrupo.Rows[0]["NumeroDocumento"].ToString();
                entGuiaRemitente.entGRR_Emisor_RazonSocial_M = dtEmpresasGrupo.Rows[0]["RazonSocial"].ToString();
                entGuiaRemitente.entGRR_Emisor_NombreComercial = dtEmpresasGrupo.Rows[0]["NombreComercial"].ToString();
                entGuiaRemitente.entGRR_Emisor_NumeroMTC = dtEmpresasGrupo.Rows[0]["NumeroMTC"].ToString();
                entGuiaRemitente.entGRR_Emisor_CodigoPais_M = dtEmpresasGrupo.Rows[0]["CodigoPais"].ToString();
                entGuiaRemitente.entGRR_Emisor_Telefono = dtEmpresasGrupo.Rows[0]["Telefono"].ToString();
                entGuiaRemitente.entGRR_Emisor_SitioWeb = dtEmpresasGrupo.Rows[0]["SitioWeb"].ToString();
                entGuiaRemitente.entGRR_Emisor_CorreoContacto = dtEmpresasGrupo.Rows[0]["CorreoContacto"].ToString();
                entGuiaRemitente.entGRR_Emisor_Ubigeo_M = dtEmpresasGrupo.Rows[0]["UbigeoEmpresa"].ToString();
                entGuiaRemitente.entGRR_Emisor_Provincia = dtEmpresasGrupo.Rows[0]["Provincia"].ToString();
                entGuiaRemitente.entGRR_Emisor_Departamento = dtEmpresasGrupo.Rows[0]["Departamento"].ToString();
                entGuiaRemitente.entGRR_Emisor_Distrito = dtEmpresasGrupo.Rows[0]["Distrito"].ToString();
                entGuiaRemitente.entGRR_Emisor_DireccionDetallada= dtEmpresasGrupo.Rows[0]["DireccionFiscal"].ToString();
                entGuiaRemitente.compania = dtEmpresasGrupo.Rows[0]["Compania"].ToString();
                
                // Obtener datos del remitente
                txtEmpresaRemitente.Text = dtEmpresasGrupo.Rows[0]["RazonSocial"].ToString();
                txtEmpresaRemitente.Tag = dtEmpresasGrupo.Rows[0]["Proveedor"].ToString();
                entGuiaRemitente.idRemitente = Convert.ToInt32(dtEmpresasGrupo.Rows[0]["Proveedor"]);
                entGuiaRemitente.entGRR_Remitente_RazonSocial_M = dtEmpresasGrupo.Rows[0]["RazonSocial"].ToString();
                entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M = dtEmpresasGrupo.Rows[0]["CodTipoDocIden"].ToString();
                entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M = dtEmpresasGrupo.Rows[0]["NumeroDocumento"].ToString();

                dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtEmpresaRemitente.Tag));
            }
            else
            {
                MessageBox.Show("Combobox de Empresas no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            //carga total
            DataTable dtUnidadMedida = clsOperacionesBL.Instancia.ReportesApp_ListarUnidadMedida_SunatTotal();
            cbxUnidadMedidaTotal.DataSource = dtUnidadMedida;
            cbxUnidadMedidaTotal.DisplayMember = "Descripcion";
            cbxUnidadMedidaTotal.ValueMember = "Codigo";

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar) { cbxUnidadMedidaTotal.SelectedIndex = 1; }
            if (TipoOperacion == Utilitario.TipoOperacion.Editar) { cbxUnidadMedidaTotal.SelectedValue = entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M; }

            //combo para datagriedview
            DataGridViewComboBoxColumn UnidadMedida = dgvProductosGuia.Columns["UnidadMedida"] as DataGridViewComboBoxColumn;
            UnidadMedida.DataSource = clsOperacionesBL.Instancia.ReportesApp_ListarUnidadMedida_Sunat();
            UnidadMedida.DisplayMember = "Descripcion";
            UnidadMedida.ValueMember = "Codigo";

            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                if (UnidadMedida.Items.Count > 0)
                {
                    dgvProductosGuia.Rows.Add("0000000001", "", "1", "", "");
                    dgvProductosGuia.Rows[dgvProductosGuia.RowCount - 1].Cells["UnidadMedida"].Value = (UnidadMedida.Items[0] as DataRowView).Row[0].ToString();
                }
            }
 
            // Llenado de Combobox de Tipo de Documentos en ComboBox y Datagriedview
            DataTable dtTipoDocFiscal = clsOperacionesBL.Instancia.ReportesApp_Listar_TipoDocumentoFiscal_GuiaElectronica();
            if (dtTipoDocFiscal.Rows.Count > 0)
            {
                cbxTipoDocumentoFiscal.DataSource = dtTipoDocFiscal;
                cbxTipoDocumentoFiscal.DisplayMember = "Descripcion";
                cbxTipoDocumentoFiscal.ValueMember = "Codigo";
                cbxTipoDocumentoFiscal.SelectedIndex = 3;
            }
            else { MessageBox.Show("Combobox de TipoProducto no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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
                    dgvDocumentosRelacionados.Rows.Add("", "", "", "", "");
                    dgvDocumentosRelacionados.Rows[0].Cells["TipoDocRelacion"].Value = (dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[0].ToString();
                    dgvDocumentosRelacionados.Rows[0].Cells["NombreDocumento"].Value = Utilitario.Instancia.QuitarTildes((dtDocRelacion.Items[cbxTipoDocumentoFiscal.SelectedIndex] as DataRowView).Row[1].ToString());
                    dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M;
                    dgvDocumentosRelacionados.Rows[0].Cells["TipoDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M;
                }
            }

            if (TipoOperacion == Utilitario.TipoOperacion.Editar)
            {
                if (entGuiaRemitente.xml_entGRR_DocumentosRelacion.Length > 0)
                {
                    checkDocRelacion.Checked = true;
                    DataTable dtDocRelacion_Editar = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaRemitente.xml_entGRR_DocumentosRelacion);
                    for (int i = 0; i < dtDocRelacion_Editar.Rows.Count; i++)
                    {
                        dgvDocumentosRelacionados.Rows.Add(dtDocRelacion_Editar.Rows[i]["TipoComprobante_Relacion"],
                                                           dtDocRelacion_Editar.Rows[i]["NombreComprobante_Relacion"],
                                                           dtDocRelacion_Editar.Rows[i]["NumeroComprobante_Relacion"],
                                                           dtDocRelacion_Editar.Rows[i]["NumeroDocIdentidad_Relacion"],
                                                           dtDocRelacion_Editar.Rows[i]["TipoDocIdentidad_Relacion"]);
                    }
                }
            }

            // CARGAR SERIES  SEGUN EL TIPO DE GUIA  TRASPORTISTA(T) , REMITENTE (R)
            DataTable dtSerieGuia = clsOperacionesBL.Instancia.ReportesApp_Listar_SerieGuiasElectronicas("R");

            if (dtSerieGuia.Rows.Count > 0)
            {
                cbxSerieGuia.DataSource = dtSerieGuia;
                cbxSerieGuia.DisplayMember = "SerieGuia";
                cbxSerieGuia.ValueMember = "SerieGuia";

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoDocumentoFiscal.SelectedIndex = 3;
                    entGuiaRemitente.entGRR_Generales_Serie_M = dtSerieGuia.Rows[0]["SerieGuia"].ToString();
                    cbxSerieGuia.SelectedValueChanged += cbxSerieGuia_SelectedValueChanged;
                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    cbxSerieGuia.SelectedValueChanged += cbxSerieGuia_SelectedValueChanged;
                    cbxSerieGuia.SelectedValue = entGuiaRemitente.entGRR_Generales_Serie_M;
                    lblNumero.Text = entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8");
                }
            }

            DataTable dtTipoServicio = clsOperacionesBL.Instancia.ReportesApp_TipoServicioGuiaElectronica("R");
            if (dtTipoServicio.Rows.Count > 0)
            {
                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios.Properties.DataSource = dtTipoServicio;
                    cbxTipoServicios.Properties.DisplayMember = "Descripcion";
                    cbxTipoServicios.Properties.ValueMember = "CodServicio";
                    cbxTipoServicios.Properties.SeparatorChar = ',';
                    cbxTipoServicios.SetEditValue("00,04");
                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    cbxTipoServicios.Properties.DataSource = dtTipoServicio;
                    cbxTipoServicios.Properties.DisplayMember = "Descripcion";
                    cbxTipoServicios.Properties.ValueMember = "CodServicio";
                    cbxTipoServicios.Properties.SeparatorChar = ',';
                    cbxTipoServicios.SetEditValue(entGuiaRemitente.xml_entGRR_TipoServicio);
                }
            }

            DataTable dtMotivoTraslado = clsOperacionesBL.Instancia.ReportesApp_ListarMotivoTraslado_GuiaElectronica();
            if (dtMotivoTraslado.Rows.Count > 0)
            {
                cbxMotivoTraslado.SelectedValueChanged -= cbxMotivoTraslado_SelectedValueChanged;
                cbxMotivoTraslado.DataSource = dtMotivoTraslado;
                cbxMotivoTraslado.DisplayMember = "Descripcion";
                cbxMotivoTraslado.ValueMember = "CodMotivoTraslado";

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxMotivoTraslado.SelectedIndex = 0;
                    cbxMotivoTraslado.SelectedValueChanged += cbxMotivoTraslado_SelectedValueChanged;
                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    cbxMotivoTraslado.SelectedValue = entGuiaRemitente.entGRR_Generales_CodigoMotivo_M;
                    cbxMotivoTraslado.SelectedValueChanged += cbxMotivoTraslado_SelectedValueChanged;
                }
            }

            DataTable dtModalidadTransporte = clsOperacionesBL.Instancia.ReportesApp_ListarModalidadTransporte();
            if (dtModalidadTransporte.Rows.Count > 0)
            {
                cbxModalidadTransporte.SelectedValueChanged -= cbxModalidadTransporte_SelectedValueChanged;
                cbxModalidadTransporte.DataSource = dtModalidadTransporte;
                cbxModalidadTransporte.DisplayMember = "Descripcion";
                cbxModalidadTransporte.ValueMember = "Codigo";

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxModalidadTransporte.SelectedIndex = 1;
                    cbxModalidadTransporte.SelectedValueChanged += cbxModalidadTransporte_SelectedValueChanged;
                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    cbxModalidadTransporte.SelectedValue = entGuiaRemitente.entGRR_Generales_Modalidad_M;
                    cbxModalidadTransporte.SelectedValueChanged += cbxModalidadTransporte_SelectedValueChanged;
                }
                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    if (entGuiaRemitente.entGRR_Generales_CodigoMotivo_M == "13") //OTROS
                    {
                        groupOtrosDetalle.Visible = true;
                        txtOtrosDescripcion.Visible = true;
                        txtOtrosDescripcion.Text = entGuiaRemitente.entGRR_Generales_DescripcionMotivo;
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
                entGuiaRemitente.entGRR_Generales_FechaEmision_M = dtFechaHora.Rows[0]["FechaServidor"].ToString();
                entGuiaRemitente.entGRR_Generales_HoraEmision_M = dtFechaHora.Rows[0]["HoraServidor"].ToString();
                entGuiaRemitente.entGRR_Generales_Serie_M = cbxSerieGuia.SelectedValue.ToString();
                
                VALIDACIONES = true;

                if (chkActivarDocumentoRelcion.Checked) { obtenerDocumentosRelacionados(); }
                entGuiaRemitente.checkTercero = Convert.ToInt32(checkTercero.Checked);
                AsignarCarretaGuia();
                obtenerXMLTipoServicio();
                ValidarCarga();
                ValidarMotivoTraslado();
               
                ene_GuiaRemisionRemitente registrar = new ene_GuiaRemisionRemitente();

                if (VALIDACIONES)
                {
                    if (entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Emisor_RazonSocial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Emisor_Ubigeo_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Emisor_CodigoPais_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Remitente_RazonSocial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Destinatario_RazonSocial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Generales_FechaEmision_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Generales_HoraEmision_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Generales_Serie_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Generales_FechaIncioTraslado.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M.Length == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.entGRR_Generales_CodigoMotivo_M == null) { VALIDACIONES = false; }
                    if (entGuiaRemitente.idcliente == 0) { VALIDACIONES = false; }
                    if (ConVehiculo) { if (txtPlaca.Tag == null) { VALIDACIONES = false; } }
                    if (entGuiaRemitente.entGRR_Remitente_Otorga_idCorreoPrincial_M == 0) { VALIDACIONES = false; }
                    if (cbxSerieGuia.Items.Count == 0) { VALIDACIONES = false; }
                    if (entGuiaRemitente.idRemitente == 0 ) { VALIDACIONES = false; }
                    if (entGuiaRemitente.idDestinatario == 0 ) { VALIDACIONES = false; }
         
                    if (VALIDACIONES)
                    {
                        registrar.at_ControlOtorgamiento = entGuiaRemitente.entGRR_ControlOtorgamiento_Estado_M;
                        registrar.ent_RemitenteGRR = new en_RemitenteGRR();
                        registrar.ent_RemitenteGRR.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M;
                        registrar.ent_RemitenteGRR.at_RazonSocial = entGuiaRemitente.entGRR_Emisor_RazonSocial_M;
                        registrar.ent_RemitenteGRR.at_Telefono = entGuiaRemitente.entGRR_Emisor_Telefono;
                        registrar.ent_RemitenteGRR.at_CorreoContacto = entGuiaRemitente.entGRR_Emisor_CorreoContacto;
                        registrar.ent_RemitenteGRR.at_SitioWeb = entGuiaRemitente.entGRR_Emisor_SitioWeb;
                        registrar.ent_RemitenteGRR.ent_DireccionFiscal = new en_DireccionFiscalRemitente();
                        registrar.ent_RemitenteGRR.ent_DireccionFiscal.at_Ubigeo = entGuiaRemitente.entGRR_Emisor_Ubigeo_M;
                        registrar.ent_RemitenteGRR.ent_DireccionFiscal.at_DireccionDetallada = entGuiaRemitente.entGRR_Emisor_DireccionDetallada;
                        registrar.ent_RemitenteGRR.ent_DireccionFiscal.at_Provincia = entGuiaRemitente.entGRR_Emisor_Provincia;
                        registrar.ent_RemitenteGRR.ent_DireccionFiscal.at_Departamento = entGuiaRemitente.entGRR_Emisor_Departamento;
                        registrar.ent_RemitenteGRR.ent_DireccionFiscal.at_Distrito = entGuiaRemitente.entGRR_Emisor_Distrito;
                        registrar.ent_RemitenteGRR.ent_DireccionFiscal.at_CodigoPais = entGuiaRemitente.entGRR_Emisor_CodigoPais_M;
                        registrar.ent_DestinatarioGRR = new en_DestinatarioGRR();
                        registrar.ent_DestinatarioGRR.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M;
                        registrar.ent_DestinatarioGRR.at_TipoDocumentoIdentidad = entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M;
                        registrar.ent_DestinatarioGRR.at_RazonSocial = entGuiaRemitente.entGRR_Destinatario_RazonSocial_M;
                        registrar.ent_DestinatarioGRR.ent_Correo = new en_Correo();
                        registrar.ent_DestinatarioGRR.ent_Correo.at_CorreoPrincipal = entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M;
                        registrar.ent_DestinatarioGRR.ent_Correo.aa_CorreoSecundario = new ArrayOfString();
                        RegistrarCorreosSecundarios(registrar);

                        if (ConProveedor) // CUANDO ES PRIVADO O PUBLICO
                        {
                            registrar.ent_ProveedorGRR = new en_ProveedorGRR();
                            registrar.ent_ProveedorGRR.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Proveedor_NumeroDocumentoIdentidad;
                            registrar.ent_ProveedorGRR.at_TipoDocumentoIdentidad = entGuiaRemitente.entGRR_Proveedor_TipoDocumentoIdentidad;
                            registrar.ent_ProveedorGRR.at_RazonSocial = entGuiaRemitente.entGRR_Proveedor_RazonSocial;
                        }

                        // DATOS GENERALES CABECERA
                        registrar.ent_DatosGeneralesGRR = new en_DatosGeneralesGRR();
                        registrar.ent_DatosGeneralesGRR.at_FechaEmision = entGuiaRemitente.entGRR_Generales_FechaEmision_M;
                        registrar.ent_DatosGeneralesGRR.at_HoraEmision = entGuiaRemitente.entGRR_Generales_HoraEmision_M;
                        registrar.ent_DatosGeneralesGRR.at_Serie = entGuiaRemitente.entGRR_Generales_Serie_M;
                        registrar.ent_DatosGeneralesGRR.at_Observacion = txtObservaciones.Text;
                        registrar.ent_DatosGeneralesGRR.at_FechaEnvio = entGuiaRemitente.entGRR_Generales_FechaIncioTraslado + " 00:00:00";
                        
                       // GUARDAR DOCUMENTOS
                        if (chkActivarDocumentoRelcion.Checked) { CargarDocumentosRelacion(registrar); }

                        // DATOS GENERALES DETALLE
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR = new en_InformacionTrasladoGRR();
                        CargarTipoServicios(registrar);
 
                        // DATOS DEL BIEN (PRODUCTO)
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_InformacionPesoBrutoGRR = new en_InformacionPesoBrutoGRR();
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_InformacionPesoBrutoGRR.at_Peso = entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_InformacionPesoBrutoGRR.at_UnidadMedida = entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M;

                        // DATOS DEL ORIGEN Y DESTINO (DIRECCION)
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR = new en_PuntoPartidaGRR();
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR.at_Ubigeo = entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR.at_DireccionCompleta = entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR.at_Departamento = entGuiaRemitente.entGRR_PuntoPartida_Departamento;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR.at_Provincia = entGuiaRemitente.entGRR_PuntoPartida_Provincia;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR.at_Distrito = entGuiaRemitente.entGRR_PuntoPartida_Distrito;

                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR = new en_PuntoLlegadaGRR();
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR.at_Ubigeo = entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR.at_DireccionCompleta = entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR.at_Departamento = entGuiaRemitente.entGRR_PuntoDestino_Departamento;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR.at_Provincia = entGuiaRemitente.entGRR_PuntoDestino_Provincia;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR.at_Distrito = entGuiaRemitente.entGRR_PuntoDestino_Distrito;

                        if (esEstablecimientoPropio)
                        { 
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR.at_CodigoEstablecimiento = entGuiaRemitente.CodigoEstablecimientoOrigen;
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR.at_CodigoEstablecimiento = entGuiaRemitente.CodigoEstablecimientoDestino;
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoPartidaGRR.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M;
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.ent_PuntoLlegadaGRR.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M;
                        }
                       
                        //SI PUBLICO NO NECESITO ENVIAR DATOS DE VEHICULO
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR = new ArrayOfEn_InformacionTransporteGRR();

                        en_InformacionTransporteGRR InformacionTransporte = new en_InformacionTransporteGRR();
                        InformacionTransporte.at_FechaInicio = entGuiaRemitente.entGRR_Generales_FechaIncioTraslado;
                        InformacionTransporte.at_Modalidad = entGuiaRemitente.entGRR_Generales_Modalidad_M;
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR.Add(InformacionTransporte);

                        if (Publico)
                        { 
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR = new en_TransportePublicoGRR();
                            
                            if (ConTransportista) 
                            {
                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.at_RazonSocial = entGuiaRemitente.entGRR_Transportista_RazonSocial;
                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Transportista_NumeroDocumentoIdentidad;
                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.at_TipoDocumentoIdentidad = entGuiaRemitente.entGRR_Transportista_TipoDocumentoIdentidad;
                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.at_NumeroMTC = entGuiaRemitente.entGRR_Transportista_NroMTC;
                            }
                        }

                        if (Privado)
                        { registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR = new en_TransportePrivadoGRR(); }

                        CargarVehiculos(registrar);
                        CargarConductores(registrar);
                        CargarBienes(registrar);

                        // GENERO Y ENVIAR LA GUIA A TCI
                        if (VALIDACIONES)
                        {
                            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                            {
                                entGuiaRemitente.entGRR_Generales_Numero_M = Convert.ToInt32(lblNumero.Text);
                                entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "PENDIENTE";
                                entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "PENDIENTE";
                            }

                            this.Cursor = Cursors.WaitCursor;
                            
                            // GUARDO LA GUIA APROBADA Y GENERO EL VIAJE CON EL CORRELATIVO ASIGNADO
                            if (clsOperacionesBL.Instancia.ReportesApp_RegistrarGuiaElectronica(ref entGuiaRemitente))
                            {
                                this.Cursor = Cursors.Default; 
                                registrar.ent_DatosGeneralesGRR.at_Numero = entGuiaRemitente.entGRR_Generales_Numero_M;
                                lblNumero.Text = entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8");

                                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                                { MessageBox.Show("SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                 
                                    if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                                    {
                                        if (MessageBox.Show("La guia se enviará a SUNAT, ¿Desea Continuar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                        { return; }

                                        respuesta = new ens_Respuesta();
                                        request = new ServiceGRR_QA.ServicioGuiaRemisionRemitenteClient();
                                        respuesta = request.RegistrarGRR20(registrar);

                                        if (respuesta.at_NivelResultado) // INDICA SI SE ACEPTÓ LA GUIA POR SUNAT : (1) ACEPTADO | (0) RECHAZADO
                                        {
                                            // ******************  GENERA LA INSTANCIA CON LOS DATOS DE LA GUIA INDIVIDUAL *************
                                            ConsultarGuiaIndividual(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M);
                                            entGuiaRemitente.entGRR_Respuesta_MensajeResultado = respuesta.at_MensajeResultado;
                                            entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = respuesta.at_CodigoError.ToString();

                                            entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "ACEPTADO";
                                            txtCodigoHash.Text = respuesta.at_CodigoHash;
                                            entGuiaRemitente.entGRR_Respuesta_CodigoHash = respuesta.at_CodigoHash;

                                            // consultaIndividual.at_NivelResultado : Valor negativo representa que existe un error, 0 que no se encontró guia , 1 comprobante encontrado
                                            if (entGuiaRemitente.entGRR_Respuesta_NivelResultado == 1)
                                            {
                                                entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";
                                                MessageBox.Show(respuesta.at_MensajeResultado + "\n SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                backgroundWorker1.RunWorkerAsync();
                                            }
                                            else
                                            {
                                                if (entGuiaRemitente.entGRR_Respuesta_NivelResultado == 0)
                                                {
                                                    entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado = "NO ENCONTRADO";
                                                    entGuiaRemitente.entGRR_Respuesta_MensajeResultado = "No se encontro guia indicada, favor verificar si se guardó correctamente";
                                                    entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = "0";
                                                }

                                                if (entGuiaRemitente.entGRR_Respuesta_NivelResultado < 0)
                                                {
                                                    entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado = "ERROR DE CONSULTA";
                                                    
                                                    if (consultaIndividual.ent_InformacionComprobante.l_respuestas.Count > 0)
                                                    {
                                                        entGuiaRemitente.entGRR_Respuesta_MensajeResultado = consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion;
                                                        entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado = "NO EXISTE";

                                            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                                            { entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "PENDIENTE"; }
                                            
                                            if (respuesta.at_CodigoError == -1) // cuando ya fue enviada la guia
                                            { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "ACEPTADO"; }
                           
                                            entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = respuesta.at_CodigoError.ToString();
                                            entGuiaRemitente.entGRR_Respuesta_MensajeResultado = respuesta.at_MensajeResultado;
                                            
                                            clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaRemitente);
                                            this.Cursor = Cursors.Default; 
                                            MessageBox.Show("SUNAT: " + respuesta.at_MensajeResultado.ToString() + "\n SQL: " + Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                            }
                            else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }
                }
                else
                { MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + "Errro= Algunos datos del formulario no fueron llenados correctamente o contienen datos incorrectos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ValidarMotivoTraslado()
        {
            if (cbxMotivoTraslado.Text == "Devolucion")
            {
                if (txtEmpresaRemitente.Tag.ToString() == txtEmpresaDestinatario.Tag.ToString())
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Remitente no puede ser igual a Destinatario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M != "6")
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Destinatario no puede ser diferente de RUC", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (chkActivarDocumentoRelcion.Checked)
            {
                if (dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value.ToString() == "")
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Para El check De documento Relacion Activo tiene que llenar el RUC del emisor del Documento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (cbxMotivoTraslado.Text == "Compra")
            {
                if (txtEmpresaRemitente.Tag.ToString() != txtEmpresaDestinatario.Tag.ToString())
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Para este Motivo de Traslado el Destinatario tiene que ser el mismo que el Remitente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (esEstablecimientoPropio)
            {
                if (entGuiaRemitente.CodigoEstablecimientoOrigen == null)
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No a ingresado correctamente el establecimiento Origen", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (entGuiaRemitente.CodigoEstablecimientoDestino == null)
                {
                    VALIDACIONES = false;
                    MessageBox.Show("No a ingresado correctamente el establecimiento Destino", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (cbxMotivoTraslado.Text == "Traslado entre establecimientos de la misma empresa")
            {
                if (txtEmpresaRemitente.Tag.ToString() != txtEmpresaDestinatario.Tag.ToString())
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Para este Motivo de Traslado el Destinatario tiene que ser el mismo que el Remitente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (entGuiaRemitente.CodigoEstablecimientoOrigen == entGuiaRemitente.CodigoEstablecimientoDestino)
                {
                    VALIDACIONES = false;
                    MessageBox.Show("Para este Motivo de Traslado la sucursal origen no puede ser la misma que el destino", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void SerializarSOAP(ene_GuiaRemisionRemitente registrar)
        {
            var path = CarpetaLogSoapError + entGuiaRemitente.entGRR_Generales_Serie_M +"-"+ entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8"); ;
            System.IO.FileStream file = System.IO.File.Create(path);
            XmlSerializer serializador = new XmlSerializer(typeof(ene_GuiaRemisionRemitente));
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            serializador.Serialize(file, registrar);
            file.Close();
        }

        private void GenerarLog()
        {
            using (StreamWriter log = new StreamWriter(CarpetaAlacenamientoLogErrores + "Error_Guia_Serie_" + entGuiaRemitente.entGRR_Generales_Serie_M + "_Numero_" + entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt"))
            {
                if (entGuiaRemitente.entGRR_Respuesta_NivelResultado == 1)
                {
                    if (consultaIndividual.ent_InformacionComprobante.l_respuestas.Count > 0)
                    {
                        log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo: 1" + " - " + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion);
                        MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= " + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta + "\n Mensaje=" + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo: 1" + " - " + "Guia encontrada - ACEPTADA ");
                        MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= 1" + "Guia encontrada - ACEPTADA " + "\n Mensaje=" + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
         
                }

                if (entGuiaRemitente.entGRR_Respuesta_NivelResultado == 0)
                {
                    log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo: 0" + respuesta.at_CodigoError.ToString() + "No se encontró Guia Consultada");
                    MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= 0 " + respuesta.at_CodigoError.ToString() + "\n Mensaje=" + "No se encontró Guia Consultada", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (entGuiaRemitente.entGRR_Respuesta_NivelResultado < 0)
                {
                    log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo: " + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta + " - " + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion);
                    MessageBox.Show("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + " Detalle: " + "Codigo= " + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta + "\n Mensaje=" + consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void CargarRespuestaSunatCDR()
        {
            if (consultaIndividual.at_NivelResultado == 1) // UNO SIGNIFICA QUE SI ENCONTRO LA RESPUESTA DEL CDR
            {
                ens_ConsultarXML Respuesta_XML_CDR = CargarGuiaEnXML(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M, Convert.ToInt32(dtRespuestaSunat_Guardado.Rows[0]["NroRespuesta"]));
                String xmlCDR = "";
                if (Respuesta_XML_CDR.at_MensajeResultado != "No hay XML para consultar")
                {
                    dtRespuestaXML_CDR = Utilitario.Instancia.ObtenerNodoXML(Encoding.UTF8.GetString((Respuesta_XML_CDR.ent_ResultadoXML.at_XML)));
                    
                    if (dtRespuestaXML_CDR.Rows.Count > 0)
                    { entGuiaRemitente.entGRR_Respuesta_URL_GuiaSunat = dtRespuestaXML_CDR.Rows[0]["Descripcion"].ToString(); }

                    if (dtRespuestaXML_CDR != null)
                    {
                        if (dtRespuestaXML_CDR.Rows.Count > 0) { xmlCDR = Utilitario.Instancia.DatatableToXml(dtRespuestaXML_CDR); }
                        else
                        { xmlCDR = "ERROR AL TARER ALGUN DATO: " + " METODO= Utilitario.Instancia.ObtenerNodoXML(Encoding.UTF8.GetString((Respuesta_XML_CDR.ent_ResultadoXML.at_XML))"; }
                    }
                }
                else { xmlCDR = "No hay XML para consultar"; }

                if (xmlCDR.Length > 0)
                {
                    entGuiaRemitente.entGRR_Respuesta_Xml_CDR = Encoding.UTF8.GetString(Respuesta_XML_CDR.ent_ResultadoXML.at_XML);
                    entGuiaRemitente.entGRR_Respuesta_Fecha_CDR = Respuesta_XML_CDR.ent_ResultadoXML.at_FechaXML;
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
            ServiceGRR_QA.ene_ConsultarRespuesta consultarRespuestaRemitente = new ServiceGRR_QA.ene_ConsultarRespuesta();
            consultarRespuestaRemitente.at_CantidadConsultar = 1;
            consultarRespuestaRemitente.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M;
            
            ServiceGRR_QA.ens_ConsultarRespuesta response = request.ConsultarRespuestaGRR(consultarRespuestaRemitente);
            
            //CONFIRMAR RESPUESTA
            ServiceGRR_QA.ene_ConfirmarRespuesta empresa = new ene_ConfirmarRespuesta();
            empresa.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M;
            empresa.l_Comprobante = new ArrayOfEn_ComprobanteConfirmarRespuesta();
          
            ens_ConfirmarRespuesta responseConfirmar = request.ConfirmarRespuestaGRR(empresa);

            for (int i = 0; i < response.l_ResultadoRespuestaComprobante.Count; i++)
            {
                if (entGuiaRemitente.entGRR_Generales_Numero_M == response.l_ResultadoRespuestaComprobante[i].at_Numero &&
                    entGuiaRemitente.entGRR_Generales_Serie_M == response.l_ResultadoRespuestaComprobante[i].at_Serie)
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
                    else { dtRespuesta.Rows.Add(CodigoRespuesta, TipoRespuesta, "SUANT NO ENVIÓ MENSAJE DE RESPUESTA (NULL)", fechaSunat); }
                }

                ServiceGRR_QA.en_ComprobanteConfirmarRespuesta ConfirmarRpta = new en_ComprobanteConfirmarRespuesta();
                ConfirmarRpta.at_Serie = response.l_ResultadoRespuestaComprobante[i].at_Serie;
                ConfirmarRpta.at_Numero = response.l_ResultadoRespuestaComprobante[i].at_Numero;
                ConfirmarRpta.at_CodigoRespuesta = response.l_ResultadoRespuestaComprobante[i].ent_RespuestaComprobante.at_CodigoRespuesta;
                empresa.l_Comprobante.Add(ConfirmarRpta);
            }

            request.ConfirmarRespuestaGRR(empresa);

            return dtRespuesta;
        }

        private void CargarBienes(ene_GuiaRemisionRemitente registrar)
        {

            if (dgvProductosGuia.Rows.Count > 0)
            {
                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_BienesGRR = new ArrayOfEn_BienesGRR();

                for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
                {

                    en_BienesGRR Bienes = new en_BienesGRR();
                    Bienes.at_Descripcion = dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString();
                    Bienes.at_Codigo = dgvProductosGuia.Rows[i].Cells["Codigo"].Value.ToString();
                    Bienes.at_Cantidad = Convert.ToDecimal(dgvProductosGuia.Rows[i].Cells["Cantidad"].Value);
                    Bienes.at_UnidadMedida = dgvProductosGuia.Rows[i].Cells["UnidadMedida"].Value.ToString();
                    registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_BienesGRR.Add(Bienes);
                }
            }
            else
            {
                VALIDACIONES = false;
                MessageBox.Show("Favor agregar al menos un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (cbxUnidadMedidaTotal.SelectedValue.ToString() != "KGM" && cbxUnidadMedidaTotal.SelectedValue.ToString() != "TNE")
            {
                VALIDACIONES = false; 
                MessageBox.Show("La unidad de medida total no puede ser diferente a KILOGRAMO O TONELADA", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarConductores(ene_GuiaRemisionRemitente registrar)
        {
            if (ConVehiculo)
            {
                if (TipoTrasladoProgramado)
                {
                    if (dgvTrasladoProgramado.Rows.Count > 0)
                    {
                        if (Publico)
                        {
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_ConductorGRR = new ArrayOfEn_ConductorGRR();

                            for (int i = 0; i < dgvTrasladoProgramado.Rows.Count; i++)
                            {
                                en_ConductorGRR Conductor = new en_ConductorGRR();

                                Conductor.at_TipoDocumentoIdentidad = dgvTrasladoProgramado.Rows[i].Cells["TipoDocumento"].Value.ToString();
                                Conductor.at_NumeroDocumentoIdentidad = dgvTrasladoProgramado.Rows[i].Cells["Documento"].Value.ToString().TrimEnd();
                                Conductor.at_Licencia = dgvTrasladoProgramado.Rows[i].Cells["Brevete"].Value.ToString().TrimEnd(); 
                                Conductor.at_Nombres = dgvTrasladoProgramado.Rows[i].Cells["Nombres"].Value.ToString();
                                Conductor.at_Apellidos = dgvTrasladoProgramado.Rows[i].Cells["Apellidos"].Value.ToString();

                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_ConductorGRR.Add(Conductor);
                            }
                        }

                        if (Privado)
                        {
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_ConductorGRR = new ArrayOfEn_ConductorGRR();

                            for (int i = 0; i < dgvTrasladoProgramado.Rows.Count; i++)
                            {
                                en_ConductorGRR Conductor = new en_ConductorGRR();

                                Conductor.at_TipoDocumentoIdentidad = dgvTrasladoProgramado.Rows[i].Cells["TipoDocumento"].Value.ToString();
                                Conductor.at_NumeroDocumentoIdentidad = dgvTrasladoProgramado.Rows[i].Cells["Documento"].Value.ToString().TrimEnd();
                                Conductor.at_Licencia = dgvTrasladoProgramado.Rows[i].Cells["Brevete"].Value.ToString().TrimEnd(); ;
                                Conductor.at_Nombres = dgvTrasladoProgramado.Rows[i].Cells["Nombres"].Value.ToString();
                                Conductor.at_Apellidos = dgvTrasladoProgramado.Rows[i].Cells["Apellidos"].Value.ToString();

                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_ConductorGRR.Add(Conductor);
                            }
                        }
                    }
                }
                else
                {
                    if (entNuevoConductor.entGRR_Conductor_TipoDocumentoIdentidad_M.Length == 0 || entNuevoConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M.Length == 0 ||
                        entNuevoConductor.entGRR_Conductor_Licencia_M.Length == 0 || entNuevoConductor.entGRR_Conductor_Nombres_M.Length == 0 || entNuevoConductor.entGRR_Conductor_Apellidos_M.Length == 0)
                    {
                        VALIDACIONES = false;
                        MessageBox.Show("No se cargaron todo los datos del conductor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    
                    en_ConductorGRR Conductor = new en_ConductorGRR();
                    Conductor.at_TipoDocumentoIdentidad = entNuevoConductor.entGRR_Conductor_TipoDocumentoIdentidad_M;
                    Conductor.at_NumeroDocumentoIdentidad = entNuevoConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M;
                    Conductor.at_Licencia = entNuevoConductor.entGRR_Conductor_Licencia_M.TrimEnd();
                    Conductor.at_Nombres = entNuevoConductor.entGRR_Conductor_Nombres_M;
                    Conductor.at_Apellidos = entNuevoConductor.entGRR_Conductor_Apellidos_M;
                    
                    if (Publico)
                    {
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_ConductorGRR = new ArrayOfEn_ConductorGRR();
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_ConductorGRR.Add(Conductor);
                    }
                    
                    if (Privado)
                    {
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_ConductorGRR = new ArrayOfEn_ConductorGRR();
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_ConductorGRR.Add(Conductor);
                    }
                }
            }
        }

        private void CargarVehiculos(ene_GuiaRemisionRemitente registrar)
        {
            if (ConVehiculo)
            {
                entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion = txtTarjetaCirculacion.Text;

                if (checkTercero.Checked == false  )
                {
                    if (entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion != null)
                    {
                        if (entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion != "")
                        {
                            if (entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.Length < 9 && entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.Substring(0, 2) != "13")
                            {
                                VALIDACIONES = false;
                                MessageBox.Show("Formato de la tarjeta de Cirulacion es incorrecto:" + entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }

                if (TipoTrasladoProgramado)
                {
                    if (dgvTrasladoProgramado.Rows.Count > 0)
                    {
                        if (Publico)
                        {
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_VehiculoGRR = new ArrayOfEn_VehiculoGRR();
                            en_VehiculoGRR Vehiculo = new en_VehiculoGRR();
                            Vehiculo.aa_NumeroPlaca = new ArrayOfString();

                            for (int i = 0; i < dgvTrasladoProgramado.Rows.Count; i++)
                            {
                                Vehiculo.aa_NumeroPlaca.Add(dgvTrasladoProgramado.Rows[i].Cells["Placa"].Value.ToString());
                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_VehiculoGRR.Add(Vehiculo);
                            }
                        }

                        if (Privado)
                        {
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_VehiculoGRR = new ArrayOfEn_VehiculoGRR();
                            en_VehiculoGRR Vehiculo = new en_VehiculoGRR();
                            Vehiculo.aa_NumeroPlaca = new ArrayOfString();

                            for (int i = 0; i < dgvTrasladoProgramado.Rows.Count; i++)
                            {
                                Vehiculo.aa_NumeroPlaca.Add(dgvTrasladoProgramado.Rows[i].Cells["Placa"].Value.ToString());
                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_VehiculoGRR.Add(Vehiculo);
                            }
                        }
                        else
                        {
                            VALIDACIONES = false;
                            MessageBox.Show("No se agregó ningun vehiculo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    if (Publico)
                    {
                        if (txtPlaca.Text.Length > 0)
                        {
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_VehiculoGRR = new ArrayOfEn_VehiculoGRR();
                            en_VehiculoGRR Vehiculo = new en_VehiculoGRR();
                            Vehiculo.aa_NumeroPlaca = new ArrayOfString();
                            Vehiculo.aa_NumeroPlaca.Add(txtPlaca.Text.TrimEnd());
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePublicoGRR.l_VehiculoGRR.Add(Vehiculo);
                        }
                        else
                        {
                            VALIDACIONES = false;
                            MessageBox.Show("No se agregó ningun vehiculo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (Privado)
                    {
                        if (txtPlaca.Text.Length > 0)
                        {
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_VehiculoGRR = new ArrayOfEn_VehiculoGRR();
                            en_VehiculoGRR Vehiculo = new en_VehiculoGRR();
                            Vehiculo.aa_NumeroPlaca = new ArrayOfString();
                            Vehiculo.aa_NumeroPlaca.Add(txtPlaca.Text.TrimEnd());
                            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.l_InformacionTransporteGRR[0].ent_TransportePrivadoGRR.l_VehiculoGRR.Add(Vehiculo);
                        }
                        else
                        {
                            VALIDACIONES = false;
                            MessageBox.Show("No se agregó ningun vehiculo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void CargarTipoServicios(ene_GuiaRemisionRemitente registrar)
        {
            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.at_CodigoMotivo = entGuiaRemitente.entGRR_Generales_CodigoMotivo_M;
            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.at_DescripcionMotivo = entGuiaRemitente.entGRR_Generales_DescripcionMotivo;
            registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.at_IndicadorMotivo = TipoTrasladoProgramado;

            if (entGuiaRemitente.xml_entGRR_TipoServicio != null)
            {
                if (entGuiaRemitente.xml_entGRR_TipoServicio.Length > 0)
                {
                    DataTable dtTipServicioGuia = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaRemitente.xml_entGRR_TipoServicio);
                    if (dtTipServicioGuia.Rows.Count > 0)
                    {
                        registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.aa_IndicadorServicio = new ArrayOfString();

                        for (int i = 0; i < dtTipServicioGuia.Rows.Count; i++)
                        {
                            if (dtTipServicioGuia.Rows[i]["CodServicio"].ToString() != "00")
                            {
                                registrar.ent_DatosGeneralesGRR.ent_InformacionTrasladoGRR.aa_IndicadorServicio.Add(dtTipServicioGuia.Rows[i]["CodServicio"].ToString());
                            }
                        }
                    }
                    else if (cbxMotivoTraslado.Text.ToString() != "Traslado entre establecimientos de la misma empresa"  && cbxMotivoTraslado.Text.ToString() != "Otros")
                    {
                        entGuiaRemitente.xml_entGRR_TipoServicio = "";
                        VALIDACIONES = false;
                        MessageBox.Show("No se agregó ningun Tipo de Servicio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void CargarDocumentosRelacion(ene_GuiaRemisionRemitente registrar)
        {
            if (dgvDocumentosRelacionados.Rows.Count > 0)
            {
                if (Convert.ToString(dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value).Length > 5 && char.IsLetter(char.Parse(dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value.ToString().Substring(0, 1))))
                {
                    en_DocumentoRelacionadoGRR entDocumentosRelacionados;

                    for (int i = 0; i < dgvDocumentosRelacionados.Rows.Count; i++)
                    {
                        entDocumentosRelacionados = new en_DocumentoRelacionadoGRR();
                        entDocumentosRelacionados.at_NumeroComprobante = dgvDocumentosRelacionados.Rows[i].Cells["NumeroDocRelacion"].Value.ToString();
                        entDocumentosRelacionados.at_TipoComprobante = dgvDocumentosRelacionados.Rows[i].Cells["TipoDocRelacion"].Value.ToString();
                        entDocumentosRelacionados.at_NombreComprobante = dgvDocumentosRelacionados.Rows[i].Cells["NombreDocumento"].Value.ToString();
                        entDocumentosRelacionados.at_NumeroDocumentoIdentidad = dgvDocumentosRelacionados.Rows[i].Cells["NumeroDocumentoIdentidad"].Value.ToString();
                        entDocumentosRelacionados.at_TipoDocumentoIdentidad = dgvDocumentosRelacionados.Rows[i].Cells["TipoDocumentoIdentidad"].Value.ToString();
                        registrar.ent_DatosGeneralesGRR.l_DocumentoRelacionadoGRR = new ArrayOfEn_DocumentoRelacionadoGRR();
                        registrar.ent_DatosGeneralesGRR.l_DocumentoRelacionadoGRR.Add(entDocumentosRelacionados);
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

        private bool RegistrarCorreosSecundarios(ene_GuiaRemisionRemitente registrar)
        {
            Boolean estado = false;
            List<String> correosSecundarios = new List<string>();
            DataTable dtCorreoSecundarioEnvio = new DataTable();
            if (entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario != null)
            {
                estado = true;
                if (entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario.Length > 0)
                { dtCorreoSecundarioEnvio = Utilitario.Instancia.ConvertirXMLaDatatable(entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario); }

                if (dtCorreoSecundarioEnvio.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCorreoSecundarioEnvio.Rows.Count; i++)
                    { registrar.ent_DestinatarioGRR.ent_Correo.aa_CorreoSecundario.Add(dtCorreoSecundarioEnvio.Rows[i]["Correo"].ToString()); }
                }
                else { estado = false; }
            }

            return estado;
        }

        private void obtenerDocumentosRelacionados()
        {
            if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                if (txtDocRelacionAnexar.TextLength > 0) { dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value = txtDocRelacionAnexar.Text; }
            }
            if (dgvDocumentosRelacionados.Rows.Count > 0)
            {
                if (dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value.ToString().Length > 5 && char.IsLetter(char.Parse(dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value.ToString().Substring(0, 1))))
                { entGuiaRemitente.xml_entGRR_DocumentosRelacion = Utilitario.Instancia.QuitarTildes(Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvDocumentosRelacionados))); }
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
           string[] UbigeoDireccionPartida = txtUbigeoPartida.Text.Split(',');
           string[] UbigeoDireccionDestino = txtUbigeoLlegada.Text.Split(',');
            
            entGuiaRemitente.entGRR_Generales_Observacion = txtObservaciones.Text;
            entGuiaRemitente.entGRR_Generales_FechaIncioTraslado = Convert.ToDateTime(dtpFechaTraslado.Text).ToString("yyyy-MM-dd");
            entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M = Convert.ToDecimal(txtPesoTotal.Value);
            entGuiaRemitente.entGRR_Generales_CodigoMotivo_M = cbxMotivoTraslado.SelectedValue.ToString();
            if (cbxMotivoTraslado.Text.ToString() == "Otros")
            {
                entGuiaRemitente.entGRR_Generales_DescripcionMotivo = txtOtrosDescripcion.Text;
            }
            else
            {
                entGuiaRemitente.entGRR_Generales_DescripcionMotivo = "Por " + cbxMotivoTraslado.Text.ToString();
            }
            
            entGuiaRemitente.entGRR_Generales_Modalidad_M = cbxModalidadTransporte.SelectedValue.ToString();
            entGuiaRemitente.Publico = Publico;
            entGuiaRemitente.Privado = Privado;
            
            if (txtTituloAdicionalGrupo.Text.Length > 0 && txtEtiquetaGrupo.Text.Length > 0 && txtContenidoGrupo.Text.Length > 0)
            {
                entGuiaRemitente.entGRR_GrupoInformacionAdicional_Titulo = txtTituloAdicionalGrupo.Text;
                entGuiaRemitente.entGRR_GrupoInformacionAdicional_Etiqueta = txtEtiquetaGrupo.Text;
                entGuiaRemitente.entGRR_GrupoInformacionAdicional_Valor = txtContenidoGrupo.Text;
            }

            if (txtDireccionPartida.Text.Length > 0 && txtDireccionDestino.Text.Length > 0)
            {
                entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
                entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;

                entGuiaRemitente.entGRR_PuntoPartida_Departamento = UbigeoDireccionPartida[0];
                entGuiaRemitente.entGRR_PuntoPartida_Provincia = UbigeoDireccionPartida[1];
                entGuiaRemitente.entGRR_PuntoPartida_Distrito = UbigeoDireccionPartida[2];

                entGuiaRemitente.entGRR_PuntoDestino_Departamento = UbigeoDireccionDestino[0];
                entGuiaRemitente.entGRR_PuntoDestino_Provincia = UbigeoDireccionDestino[1];
                entGuiaRemitente.entGRR_PuntoDestino_Distrito = UbigeoDireccionDestino[2];
            }
            else { VALIDACIONES = false; }

            if (Convert.ToDecimal(txtPesoTotal.Value) <= 0) { VALIDACIONES = false; }
            else { entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M = Convert.ToDecimal(txtPesoTotal.Value); }

            entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M = cbxUnidadMedidaTotal.SelectedValue.ToString();

            if (ConTransportista)
            {
                if (txtRucTransportista.Text.Length == 0 || txtRazonSocialTransportista.Text.Length == 0 || txtTipoDocTransportista.Text.Length == 0 || txtNroMTCTransportista.Text.Length == 0)
                {
                    MessageBox.Show("Faltan llenar campos del Transportista", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    VALIDACIONES = false;
                }
                else
                {
                    entGuiaRemitente.ConTransportista = true;
                    entGuiaRemitente.entGRR_Transportista_RazonSocial = txtRazonSocialTransportista.Text;
                    entGuiaRemitente.entGRR_Transportista_TipoDocumentoIdentidad = txtTipoDocTransportista.Tag.ToString();
                    entGuiaRemitente.entGRR_Transportista_NumeroDocumentoIdentidad = txtRucTransportista.Text;
                    entGuiaRemitente.entGRR_Transportista_NroMTC = txtNroMTCTransportista.Text;
                }
            }

            if (dtpFechaTraslado.Value < dtpFechaRegistro.Value)
            {
                MessageBox.Show("Fecha de Traslado no puede ser menor a la fecha de Registro de Guia", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                VALIDACIONES = false;
            }
            
            if (entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M.Length == 0)
            {
                MessageBox.Show("Ingrese el correo, Campo obligatorio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                VALIDACIONES = false;
            }

            if ((entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M != "TNE" && entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M != "KGM")) 
            {
                MessageBox.Show("La Unidad de Medida no puede ser diferente a Kiogramo o Tonelada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                VALIDACIONES = false; 
            }

            for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
            {
                if (dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString().Length == 0)
                {
                    MessageBox.Show("En la fila: "+ i.ToString() + " ,No ha ingresado nombre del producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    VALIDACIONES = false;
                    break;
                }
            }
        }

        private string ObtenerIdCarreta(TextBox txt)
        {
            if (txt == null || string.IsNullOrWhiteSpace(txt.Text))
                return "-1";

            if (txt.Tag == null || string.IsNullOrWhiteSpace(txt.Tag.ToString()))
                return "-1";

            return txt.Tag.ToString();
        }

        private void AsignarCarretaGuia()
        {
            // Para guía normal: Vehículo y Conductor
            if (!TipoTrasladoProgramado)
            {
                string idCarreta = ObtenerIdCarreta(txtCarreta);

                entGuiaRemitente.idcarreta = idCarreta;
                entGuiaRemitente.carreta = idCarreta == "-1" ? "" : txtCarreta.Text.TrimEnd();
                entGuiaRemitente.entGRR_Vehiculo_NumeroCarreta_M = entGuiaRemitente.carreta;

                if (idCarreta == "-1")
                {
                    entGuiaRemitente.entGRR_Carreta_TarjetaCirculacion = "";
                }
            }
        }

        private void obtenerXMLTipoServicio()
        {
            if (cbxTipoServicios.Properties.Items.Count > 0)
            {
                dtTipoServicio.Rows.Clear();

                for (int i = 0; i < cbxTipoServicios.Properties.Items.Count; i++)
                {
                    if (cbxTipoServicios.Properties.Items[i].CheckState == CheckState.Checked)
                    { dtTipoServicio.Rows.Add(cbxTipoServicios.Properties.Items[i].Value.ToString(), cbxTipoServicios.Properties.Items[i].Description); }

                    if (cbxTipoServicios.Properties.Items[i].CheckState == CheckState.Checked && cbxTipoServicios.Properties.Items[i].Description == "Trasbordo Programado")
                    { TipoTrasladoProgramado = true; }
                    else if (cbxTipoServicios.Properties.Items[i].Description == "Trasbordo Programado") { TipoTrasladoProgramado = false; }
                }
                
                if (dtTipoServicio.Rows.Count > 0) { entGuiaRemitente.xml_entGRR_TipoServicio = Utilitario.Instancia.DatatableToXml(dtTipoServicio); }
            }
            else if (cbxMotivoTraslado.Text.ToString() != "Otros")             
            {
                entGuiaRemitente.xml_entGRR_TipoServicio = "";
                VALIDACIONES = false;
                MessageBox.Show("No ha seleccionado ningun tipo de servicio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (dgvProductosGuia.Rows.Count > 0)
            { entGuiaRemitente.xml_entGRR_Productos_Bienes = Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvProductosGuia)); }
            else
            {
                VALIDACIONES = false;
                MessageBox.Show("Al menos debe tener un bien ingresado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TipoTrasladoProgramado)
            {
                if (dgvTrasladoProgramado.Rows.Count > 0)
                {
                    entGuiaRemitente.TipoTrasladoProgramado = TipoTrasladoProgramado;
                    entGuiaRemitente.xml_entGRR_Conductor_M = Utilitario.Instancia.DatatableToXml(Utilitario.Instancia.GetContentAsDataTable(dgvTrasladoProgramado));
                }
                else
                {
                    Utilitario.Instancia.Advertencia = "No registró ningun Trasbordo Programado";
                    VALIDACIONES = false;
                }
            }

            if (ConProveedor)
            {
                if (txtTipoDocumentoProveedor.Text.Length == 0 || txtRucProveedor.Text.Length == 0 || txtTipoDocumentoProveedor.Text.Length == 0)
                { VALIDACIONES = false; }
                else
                {
                    entGuiaRemitente.ConProveedor = ConProveedor;
                    entGuiaRemitente.entGRR_Proveedor_TipoDocumentoIdentidad = txtTipoDocumentoProveedor.Tag.ToString();
                    entGuiaRemitente.entGRR_Proveedor_NumeroDocumentoIdentidad = txtRucProveedor.Text.ToString();
                    entGuiaRemitente.entGRR_Proveedor_RazonSocial = txtRazonSocialProveedor.Text;
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
            btnGuardar.Focus();

        }

        private void pEstado_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstEmpresaRemitente_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEmpresaRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
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
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEmpresaRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    groupFechaTraslado.Select();
                    dtpFechaTraslado.Focus();
                }
                if (txtEmpresaRemitente.Tag == null)
                {
                    txtDireccionPartida.Clear();
                    txtDireccionPartida.Enabled = false;
                }
                else
                {
                    txtDireccionPartida.Enabled = false;
                    txtDireccionPartida.Clear();
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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEmpresaRemitente, ref lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);

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
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEmpresaRemitente, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
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
                        txtEmpresaRemitente.Text = entGuiaRemitente.entGRR_Remitente_RazonSocial_M;
                        DataTable dtClienteProgramacion = clsOperacionesBL.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(Convert.ToInt32(txtEmpresaRemitente.Tag));
                        entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["DocumentoFiscal"].ToString();
                        entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["Codigo"].ToString();
                        entGuiaRemitente.idRemitente = Convert.ToInt32(txtEmpresaRemitente.Tag);
                        dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtEmpresaRemitente.Tag));

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
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEmpresaRemitente, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    groupFechaTraslado.Select();
                    dtpFechaTraslado.Focus();
                }


                if (txtEmpresaRemitente.Tag == null)
                {
                    txtDireccionPartida.Clear();
                    txtDireccionPartida.Enabled = false;
                }
                else
                {
                    txtDireccionPartida.Enabled = false;
                    txtDireccionPartida.Clear();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/

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
                    dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocRelacion"].Value = txtDocRelacionAnexar.Text;
                    dgvDocumentosRelacionados.Rows[0].Cells["NombreDocumento"].Value = cbxTipoDocumentoFiscal.Text;
                    //dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M;
                    //dgvDocumentosRelacionados.Rows[0].Cells["TipoDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M; 

                    

                    //dgvDocumentosRelacionados.Rows.Add("", txtDocRelacionAnexar.Text);
                }
                groupDestinatario.Select();
                txtEmpresaDestinatario.Focus();
            }
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

                string ciudadOrigen;
                string codOrigen ;
                string ciudadDestino;
                string codDestino;

                ciudadOrigen = txtUbigeoPartida.Text;
                codOrigen = txtUbigeoPartida.Tag == null ? codOrigen = "" : codOrigen = txtUbigeoPartida.Tag.ToString();
                ciudadDestino = txtUbigeoLlegada.Text;
                codDestino = txtUbigeoLlegada.Tag == null ? codDestino = "" : codDestino = txtUbigeoLlegada.Tag.ToString();
                

                string direccionOrigen = txtDireccionPartida.Text;
                string direccionDestino = txtDireccionDestino.Text;
                
                if (OPEN.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    

                    txtEmpresaDestinatario.Text = entGuiaRemitente.entGRR_Destinatario_RazonSocial_M;
                    //txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                    lstEmpresaDestinatario.Select();
                    lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                    if (codOrigen.Length > 0)
                    {
                        txtUbigeoPartida.Text = ciudadOrigen;
                        txtUbigeoPartida.Tag = codOrigen;
                        txtUbigeoLlegada.Text = ciudadDestino;
                        txtUbigeoLlegada.Tag = codDestino;
                    }
                    if (codDestino.Length > 0)
                    {
                        txtDireccionPartida.Text = direccionOrigen;
                        txtDireccionDestino.Text = direccionDestino;
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

        private void lstEmpresaDestinatario_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEmpresaDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);

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
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEmpresaDestinatario, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstEmpresaDestinatario.SelectedItems[0];
                    entGuiaRemitente.idcliente = Convert.ToInt32(txtEmpresaDestinatario.Tag);
                    entGuiaRemitente.idDestinatario = Convert.ToInt32(txtEmpresaDestinatario.Tag);
                    txtDocIdentidadDesti.Text = ItemActual.SubItems[2].Text;

                    dtDireccionesRuta2 = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtEmpresaDestinatario.Tag));
                    dtCorreos = clsOperacionesBL.Instancia.ReportesApp_ListarCorreos_Master(Convert.ToInt32(txtEmpresaDestinatario.Tag), txtDireccionDestino.Text);

                    if (dtCorreos.Rows.Count > 0)
                    {


                        IEnumerable<DataRow> ieRegistro = from fila in dtCorreos.AsEnumerable()
                                                          where fila.Field<bool>("Principal") == true
                                                          select fila;

                        if (ieRegistro.Any())
                        {
                            DataTable dtcorreoPrincipal = ieRegistro.CopyToDataTable();
                            entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M = dtcorreoPrincipal.Rows[0]["Correo"].ToString();
                            entGuiaRemitente.entGRR_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtcorreoPrincipal.Rows[0]["idCorreo"]);
                            txtCorreoPrimario.Text = dtcorreoPrincipal.Rows[0]["Correo"].ToString();

                            //CORREOS SECUNDARIOS
                            IEnumerable<DataRow> ieCorreoSecundario = from fila in dtCorreos.AsEnumerable()
                                                                      where fila.Field<bool>("Principal") == false
                                                                      select fila;

                            if (ieCorreoSecundario.Any())
                            {
                                DataTable dtCorreoSecundario = ieCorreoSecundario.CopyToDataTable();
                                entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreoSecundario);
                            }

                        }
                        else
                        {
                            entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M = dtCorreos.Rows[0]["Correo"].ToString();
                            entGuiaRemitente.entGRR_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtCorreos.Rows[0]["idCorreo"]);
                            txtCorreoPrimario.Text = dtCorreos.Rows[0]["Correo"].ToString();
                            dtCorreos.Rows.RemoveAt(0);
                            if (dtCorreos.Rows.Count > 0)
                            {
                                entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreos);
                            }
                            

                        }
                    }
                    else
                    {
                        entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M = "";
                        txtCorreoPrimario.Clear();
                    }

                    entGuiaRemitente.entGRR_Destinatario_RazonSocial_M = txtEmpresaDestinatario.Text;
                    DataTable dtClienteProgramacion = clsOperacionesBL.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(Convert.ToInt32(txtEmpresaDestinatario.Tag));
                    if (dtClienteProgramacion.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron datos de la empresa destino, verificar si empresa se encuentra registrada  correctamente en el Spring", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["DocumentoFiscal"].ToString();
                    entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["Codigo"].ToString();
                  

                    if (esEstablecimientoPropio == false)
                    {
                       // txtDireccionDestino.Clear();
                        //lstUbigeoLlegada.Clear();
                        //txtUbigeoPartida.Clear();
                        txtDireccionDestino.Enabled = true;
                        txtDireccionPartida.Enabled = true;
                        txtUbigeoLlegada.Enabled = true;
                        txtUbigeoPartida.Enabled = true;
                        groupUbigeoLlegada.Select();
                        txtUbigeoLlegada.Focus();
                    }
                    else
                    {
                        tabContingencia.SelectedTab = tabPage3;
                        groupEstablecimientoOrigen.Select();
                        txtEstablecimientoOrigen.Focus();
                    }




                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                        txtDireccionPartida.Enabled = false;
                  
                    }



                    HabilitarBotonCorreo();
                    


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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEmpresaDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
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
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEmpresaDestinatario, ref  lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {


                  
                    txtDireccionDestino.Enabled = true;
                    txtUbigeoLlegada.Enabled = true;
                    


                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                    }
                    else
                    {
                        groupUbigeoPartida.Select();
                        txtUbigeoPartida.Focus();
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
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEmpresaDestinatario, ref lstEmpresaDestinatario, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    txtDireccionDestino.Enabled = true;
                    txtUbigeoLlegada.Enabled = true;



                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                    }
                    else
                    {
                        groupUbigeoPartida.Select();
                        txtUbigeoPartida.Focus();
                    }

                }
                HabilitarBotonCorreo();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void txtDireccionPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionPartida, ref  lstDireccionPartida, null, dtDireccionesRuta))
            {

                if (txtEmpresaDestinatario.Tag == null)
                {
                    txtDireccionPartida.Clear();
                    txtDireccionPartida.Enabled = false;
                    txtDireccionDestino.Clear();
                    txtDireccionDestino.Enabled = false;
                }
                else
                {
                    groupUbigeoLlegada.Select();
                    txtUbigeoLlegada.Focus();
                    lstDireccionPartida.Visible = false;
                }
            }*/
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


           

            if (esEstablecimientoPropio == true)
            {
                if ((cbxSerieGuia.Items.Count > 0))
                {

                DataTable dtVinculados = clsOperacionesBL.Instancia.ReportesApp_Listar_Establecimientos_Vinculados_Series(cbxSerieGuia.SelectedValue.ToString(), "R", "10000000");

                    if (dtVinculados.Rows.Count > 0)
                    {
                        txtEstablecimientoOrigen.Text = dtVinculados.Rows[0]["NombreEstablecimiento"].ToString();
                        txtEstablecimientoOrigen.Tag = dtVinculados.Rows[0]["CodigoEstablecimiento"].ToString();
                        entGuiaRemitente.CodigoEstablecimientoOrigen = txtEstablecimientoOrigen.Tag.ToString();

                        esEstablecimientoPropio = true;

                        txtDireccionPartida.Text = dtVinculados.Rows[0]["NombreEstablecimiento"].ToString();
                        txtUbigeoPartida.Text = dtVinculados.Rows[0]["Departamento"].ToString() + "," + dtVinculados.Rows[0]["Provincia"].ToString() + "," + dtVinculados.Rows[0]["Distrito"].ToString();
                        txtUbigeoPartida.Tag = dtVinculados.Rows[0]["Ubigeo"].ToString();
                        entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M = txtUbigeoPartida.Tag.ToString();



                        /* lstEstablecimientoOrigen_KeyPress(this, new KeyPressEventArgs((char)(Keys.Space)));
                         lstEstablecimientoOrigen.Select();
                         lstEstablecimientoOrigen_KeyUp(this, new KeyEventArgs(Keys.Down));
                         lstEstablecimientoOrigen_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));*/


                        txtEmpresaDestinatario.Text = "GRUPO TRANSPESA S.A.C";
                        txtEmpresaDestinatario.Tag = "1553";
                        txtEmpresaDestinatario.Enabled = false;

                        txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                        lstEmpresaDestinatario.Select();
                        lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                        lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                }
                    
                }
                
            }
        }

        private void cbxSerieGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dgvProductosGuia_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


            int cantidad = 0;
            for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
            {
                if(dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString() == dgvProductosGuia.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString()){
                    cantidad++;
                    if (cantidad >= 2)
                    {
                        dgvProductosGuia.Rows[e.RowIndex].Cells["Descripcion"].Value = "";
                        MessageBox.Show("El Producto ya existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    }
                 
                }
            }
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
                        txtLicencia.Text = dt.Rows[0]["Brevete"].ToString().TrimEnd();
                        txtDocIdentidad.Text = dt.Rows[0]["Documento"].ToString();
                        txtTipoDocumentoIdentidad.Text = dt.Rows[0]["TipoDocumento"].ToString();
                        txtTipoDocumentoIdentidad.Tag = dt.Rows[0]["Codigo"].ToString();

                        entGuiaRemitente.idconductor = Convert.ToInt32(dt.Rows[0]["idConductor"]);
                        entNuevoConductor.entGRR_Conductor_Nombres_M = dt.Rows[0]["Nombres"].ToString();
                        entNuevoConductor.entGRR_Conductor_Apellidos_M = dt.Rows[0]["Apellidos"].ToString();
                        entNuevoConductor.entGRR_Conductor_Licencia_M = dt.Rows[0]["Brevete"].ToString().TrimEnd();
                        entNuevoConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M = dt.Rows[0]["Documento"].ToString().TrimEnd();
                        entNuevoConductor.entGRR_Conductor_TipoDocumentoIdentidad_M = dt.Rows[0]["Codigo"].ToString();
                        entGuiaRemitente.entConductor = entNuevoConductor;
                    }

                    groupBox19.Select();
                    txtPesoTotal.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtConductor, ref lstConductor2, clsConsultaBL.Instancia.GetConductores); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtConductor_Enter(object sender, EventArgs e) { txtConductor.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtConductor_Leave(object sender, EventArgs e) { txtConductor.BackColor = Color.White; }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
          /*  try
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
            }*/
        }

        private void txtConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores))
                {
                    groupObservacion.Select();
                    txtObservaciones.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaca_Enter(object sender, EventArgs e) { txtPlaca.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca_Leave(object sender, EventArgs e) { txtConductor.BackColor = Color.White; }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    gConductor.Select();
                    txtConductor.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            try { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
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
                    entGuiaRemitente.entGRR_Vehiculo_NumeroPlaca_M = txtPlaca.Text;
                    entGuiaRemitente.idtracto = txtPlaca.Tag.ToString();
                    entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion = ItemActual.SubItems[2].Text;
                    txtTarjetaCirculacion.Text = entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.Length == 9 ? "0" + entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.ToString() : entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.ToString();
                    gConductor.Select();
                    txtConductor.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ; }
        }

        private void lstPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPlaca, ref lstPlaca, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtCarreta_Enter(object sender, EventArgs e) { txtCarreta.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCarreta_Leave(object sender, EventArgs e) { txtCarreta.BackColor = Color.White; }

        private void txtCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    groupBox3.Select();
                    txtObservaciones.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstCarreta_Enter(object sender, EventArgs e)
        {
            try { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCarreta, ref  lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstCarreta.SelectedItems[0];
                    txtCarreta.Text = ItemActual.SubItems[1].Text.TrimEnd();
                    entGuiaRemitente.entGRR_Vehiculo_NumeroCarreta_M = txtCarreta.Text;
                    entGuiaRemitente.idcarreta = txtCarreta.Tag.ToString();
                    entGuiaRemitente.entGRR_Carreta_TarjetaCirculacion = ItemActual.SubItems[2].Text;
                    groupBox3.Select();
                    txtObservaciones.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ; }
        }

        private void lstCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCarreta, ref lstCarreta, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtCarreta2_Enter(object sender, EventArgs e) { txtCarreta2.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCarreta2_Leave(object sender, EventArgs e) { txtCarreta2.BackColor = Color.White; }

        private void txtCarreta2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtCarreta2, ref lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    groupBox3.Select();
                    txtObservaciones.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstCarreta2_Enter(object sender, EventArgs e)
        {
            try { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtCarreta2, ref lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstCarreta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtCarreta2, ref lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstCarreta2.SelectedItems[0];
                    txtCarreta2.Text = ItemActual.SubItems[1].Text.TrimEnd();
                    entGuiaRemitente.entGRR_Vehiculo_NumeroCarreta_M = txtCarreta2.Text;
                    entGuiaRemitente.idcarreta = txtCarreta2.Tag.ToString();
                    entGuiaRemitente.entGRR_Carreta_TarjetaCirculacion = ItemActual.SubItems[2].Text;
                    txtTarjetaCirculacion.Text = entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.Length == 9 ? "0" + entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.ToString() : entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.ToString();
                    groupConductor2.Select();
                    txtConductor2.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstCarreta2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtCarreta2, ref lstCarreta2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                            string idCarreta = ObtenerIdCarreta(txtCarreta2);
                            string carreta = idCarreta == "-1" ? "" : txtCarreta2.Text.TrimEnd();
                            string tarjetaCarreta = idCarreta == "-1" ? "" : entGuiaRemitente.entGRR_Carreta_TarjetaCirculacion;

                            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(Convert.ToInt32(txtConductor2.Tag));
                            dgvTrasladoProgramado.Rows.Add(dt.Rows[0]["idConductor"], txtConductor2.Text, txtPlaca2.Tag.ToString(), txtPlaca2.Text, txtLicencia2.Text, 
                                                           txtDocIdentidad2.Text, txtTipoDocumento2.Tag.ToString(), dt.Rows[0]["Nombres"].ToString(), dt.Rows[0]["Apellidos"].ToString(),
                                                           idCarreta, carreta, entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion, tarjetaCarreta);
                        }
                        else { MessageBox.Show("No se ha seleccionado la placa o el conductor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void lstPlaca2_Enter(object sender, EventArgs e)
        {
            try { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlaca2, ref lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPlaca2, ref  lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstPlaca2.SelectedItems[0];
                    txtPlaca2.Text = ItemActual.SubItems[1].Text.TrimEnd(); ;
                    entGuiaRemitente.entGRR_Vehiculo_NumeroPlaca_M = txtPlaca2.Text;
                    entGuiaRemitente.idtracto = txtPlaca2.Tag.ToString();
                    entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion = ItemActual.SubItems[2].Text;
                    txtTarjetaCirculacion.Text = entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.Length == 9 ? "0" + entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.ToString() : entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion.ToString();
                    groupConductor2.Select();
                    txtConductor2.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPlaca2, ref lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtPlaca2_Enter(object sender, EventArgs e) { txtPlaca2.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlaca2_Leave(object sender, EventArgs e) { txtPlaca2.BackColor = Color.White; }

        private void txtPlaca2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPlaca2, ref lstPlaca2, clsOperacionesBL.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica))
                {
                    groupConductor2.Select();
                    txtConductor2.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtxConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtConductor2, ref  lstConductor2, clsConsultaBL.Instancia.GetConductores))
                {
                    groupObservacion.Select();
                    txtObservaciones.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
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
               if(Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtConductor2, ref lstConductor2, clsConsultaBL.Instancia.GetConductores))
               {
                    groupObservacion.Select();
                    txtObservaciones.Focus();
               }
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

                        entGuiaRemitente.idconductor = Convert.ToInt32(dt.Rows[0]["idConductor"]);
                        entNuevoConductor.entGRR_Conductor_Nombres_M = dt.Rows[0]["Nombres"].ToString();
                        entNuevoConductor.entGRR_Conductor_Apellidos_M = dt.Rows[0]["Apellidos"].ToString();
                        entNuevoConductor.entGRR_Conductor_Licencia_M = dt.Rows[0]["Brevete"].ToString();
                        entNuevoConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M = dt.Rows[0]["Documento"].ToString();
                        entNuevoConductor.entGRR_Conductor_TipoDocumentoIdentidad_M = dt.Rows[0]["Codigo"].ToString();

                    }


                    groupObservacion.Select();
                    txtObservaciones.Focus();
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

                    lstConductor.Location = new Point (114, 367);
                    lstPlaca.Location = new Point(20, 387);
                    lstPlaca2.Location = new Point(21, 388);
                    lstConductor2.Location = new Point(115, 387);
                    lstTransportista.Location = new Point(287, 387);
                    lstProveedor.Location = new Point(30, 387);
                    lstEstablecimientoOrigen.Location = new Point(33, 387);
                    lstEstablecimientoDestino.Location = new Point(343, 387);
                }
                if (checkDocRelacion.Checked == false)
                {
                    groupDocRelacionTabla.Size = new Size(956, 16);

                    lstConductor.Location = new Point(114, 303);
                    lstPlaca.Location = new Point(88, 303);
                    lstPlaca2.Location = new Point(21, 303);
                    lstConductor2.Location = new Point(115, 303);
                    lstTransportista.Location = new Point(287, 303);
                    lstProveedor.Location = new Point(30, 303);
                    lstEstablecimientoOrigen.Location = new Point(33, 305);
                    lstEstablecimientoDestino.Location = new Point(343, 304);
                    
                   
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
                txtDocRelacionAnexar.Text = "OT001-";
            }

        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {

                int i = dgvProductosGuia.Rows.Count;
                if (i < 0) { i = 0; }
                dgvProductosGuia.Rows.Add((i + 1).ToString("D10"), "", "1", "", "");
                dgvProductosGuia.Rows[dgvProductosGuia.RowCount - 1].Cells["UnidadMedida"].Value = (UnidadMedida.Items[0] as DataRowView).Row[0].ToString();

              

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
                dgvDocumentosRelacionados.Rows[dgvDocumentosRelacionados.RowCount - 1].Cells["NumeroDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M;
                dgvDocumentosRelacionados.Rows[dgvDocumentosRelacionados.RowCount - 1].Cells["TipoDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M;

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




        private void txtDireccionPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionPartida, ref lstDireccionPartida, null, dtDireccionesRuta))
                {
                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionPartida.Clear();
                        txtDireccionPartida.Enabled = false;
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                      
                    }
                    else
                    {
                        groupUbigeoLlegada.Select();
                        txtUbigeoLlegada.Focus();
                        lstDireccionPartida.Visible = false;
                    }
                   /* if (txtDireccionPartida.Text.Length == 0)
                    {
                        txtDireccionPartida.Focus();
                    }*/
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
                    entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
                    groupUbigeoLlegada.Select();
                    txtUbigeoLlegada.Focus();
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
            if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRuta2))
            {
               
                if (txtEmpresaDestinatario.Tag == null)
                {
                    groupDestinatario.Select();
                    txtEmpresaDestinatario.Focus();
                }
                else
                {

                    lstDireccionDestino.Visible = false;
                    groupFechaTraslado.Select();
                    dtpFechaTraslado.Focus();
                }
            }
        }

        private void txtDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRuta))
                {
                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        groupDestinatario.Select();
                        txtEmpresaDestinatario.Focus();
                    }
                    else
                    {

                        lstDireccionDestino.Visible = false;
                        groupFechaTraslado.Select();
                        dtpFechaTraslado.Focus();
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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRuta2);

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
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtDireccionDestino, ref  lstDireccionDestino, null, dtDireccionesRuta2))
                {
                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                    entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;

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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtDireccionDestino, ref lstDireccionDestino, null, dtDireccionesRuta2);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dgvProductosGuia_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProductosGuia_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvProductosGuia.Columns[e.ColumnIndex].Name == "Descripcion")
            {

                if (e.FormattedValue == "" )
                {
                    dgvProductosGuia.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Debe Ingresar un valor Valido";
                    e.Cancel = true;
                }
                else
                {
                    dgvProductosGuia.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                    e.Cancel = false;
                }
            }



            if (dgvProductosGuia.Columns[e.ColumnIndex].Name == "Cantidad")
            {

                decimal pedido = 0;
                DataGridViewRow row;

                if (!decimal.TryParse(e.FormattedValue.ToString(), out pedido))
                {
                    row = dgvProductosGuia.Rows[e.RowIndex];
                    dgvProductosGuia.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Debe Ingresar un valor Valido";

                    e.Cancel = true;
                }
                else
                {
                    dgvProductosGuia.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                    e.Cancel = false;
                }
            }
        }

        private void dgvProductosGuia_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dgvProductosGuia.CurrentCell.ColumnIndex;

            if (dgvProductosGuia.Columns[columnIndex].Name == "Descripcion" || dgvProductosGuia.Columns[columnIndex].Name == "Codigo" || dgvProductosGuia.Columns[columnIndex].Name == "Cantidad")
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
                if (!char.IsSeparator(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '"' && e.KeyChar != '-' && e.KeyChar != '/' && e.KeyChar != '*' && e.KeyChar != '%' && e.KeyChar != '&' && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '!' )
                {
       
                    e.Handled = true;
                }
                else
                {
                    if (e.KeyChar == 'Ñ')
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                   
                }
            }


            if (dgvProductosGuia.Columns[columnIndex].Name == "Cantidad")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
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



        private void cbxUnidadMedidaTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cbxMotivoTraslado.Select();
                cbxMotivoTraslado.Focus();
            }
        }


        private void dtpFechaTraslado_ValueChanged(object sender, EventArgs e)
        {
            entGuiaRemitente.entGRR_Generales_FechaIncioTraslado = dtpFechaTraslado.Value.ToString();
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



        }

        private void control_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
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
                    btnAgregarProgramacion.Visible = true;
                    groupVehiculoConductor.Size = new Size(956, 157);
                    TipoTrasladoProgramado = true;


                }
                else 
                {
                    if (current.CheckState == CheckState.Unchecked && current.Description == "Trasbordo Programado")
                    {
                        TipoTrasladoProgramado = false;
                        edit.SetItemChecked(0, true);
                        btnAgregarProgramacion.Visible = false;
                        tabPage4.Parent = null;
                    }
                }

                if (current.CheckState == CheckState.Checked && current.Description == "Retorno de vehiculo con envaseso o embalajes vacios") // Retorno de vehiculo con envaseso o embalajes vacios 02
                {

                }
                if (current.CheckState == CheckState.Checked && current.Description == "Retorno de vehiculo vacio") // Retorno de vehiculo vacio 03
                {

                }
                if (current.CheckState == CheckState.Checked && current.Description == "Traslado de vehiculo M1 y L") // Transporte  02 
                {
                   
                        tabPage2.Parent = null;// Placa Conductor
                        ConVehiculo = false;
                        entGuiaRemitente.ConVehiculo = false;
                        checkTercero.Visible = false;
                        edit.SetItemChecked(0, true);
                    
                }
                else 
                {
                    /*if (current.CheckState == CheckState.Unchecked && current.Description == "Traslado de vehiculo M1 y L"  && cbxModalidadTransporte.Text != "Devolucion" || cbxModalidadTransporte.Text != "Compra") // quito al proveedor
                    {
                        tabPage5.Parent = null; // proveedor
                        cbxTipoServicios.Properties.Items[4].Enabled = true;
                        cbxTipoServicios.Properties.Items[3].Enabled = true;
                    }*/
                    if (current.CheckState == CheckState.Unchecked && current.Description == "Traslado de vehiculo M1 y L" && cbxModalidadTransporte.Text == "Transporte Propio") // agreho placa y vehiculo
                    {
                        tabPage2.Parent = tabContingencia;// Placa Conductor
                        ConVehiculo = true;
                        entGuiaRemitente.ConVehiculo = true;
                        checkTercero.Visible = true;
                    }

                }

              

            }

        }

        public void ValidarProductoEstado(DevExpress.XtraEditors.Controls.CheckedListBoxItem check)
        {

        }

        private void Cargando()
        {
            dtRespuestaSunat_Guardado = new DataTable();
            int i = 0;

            while (esRespuesta == false)
            {
                if (i == 15000) {

                    backgroundWorker1.CancelAsync();
                    esTiempoExcedido = true;
                    break; 
                }
                //consultaIndividual = request.ConsultaIndividualGRR(respuestaSunat);
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            Cargando();

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

            CrearImagenCarga(e.ProgressPercentage, Convert.ToBoolean(e.UserState));
            String ConcatenarRespuesta = "";
            bool esAprobado = false;

            if (esRespuesta)
            {

                    if (dtRespuestaSunat_Guardado.Rows[0]["CodigoRespuesta"].ToString() == "2" || dtRespuestaSunat_Guardado.Rows[0]["CodigoRespuesta"].ToString() == "1") // (1) aceptado - (2) aceptado con observaciones
                    {

                        CargarArchivoXML();
                        CargarGuiaEnPDF();
                        CargarRespuestaSunatCDR();
                        ConfirmarOtorgadoLeido();
                        ConsultarGuiaIndividual(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M);

                        esAprobado = true;
                        entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "APROBADO";

                        for (int i = 0; i < dtRespuestaSunat_Guardado.Rows.Count; i++)
                        {
                            ConcatenarRespuesta = ConcatenarRespuesta + dtRespuestaSunat_Guardado.Rows[i]["Descripcion"].ToString() + "\n";
                        }

                        if (dtRespuestaSunat_Guardado.Rows.Count > 0)
                        {
                            entGuiaRemitente.entGRR_Respuesta_MensajeResultado = ConcatenarRespuesta;
                            entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = dtRespuestaSunat_Guardado.Rows[0]["CodigoRespuesta"].ToString();
                        }

                        clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaRemitente);
                        

                    }
                    else
                    {
                        esAprobado = false;
                        entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "RECHAZADO";
                        RechazadoSunat = true;
                        AprobadoSunat = false;
                        CargarRespuestaSunatCDR();
                        ConfirmarOtorgadoLeido();
                        ConsultarGuiaIndividual(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M);

                        for (int i = 0; i < dtRespuestaSunat_Guardado.Rows.Count; i++)
                        {
                            ConcatenarRespuesta = ConcatenarRespuesta + dtRespuestaSunat_Guardado.Rows[i]["Descripcion"].ToString() + "\n";
                        }

                        if (dtRespuestaSunat_Guardado.Rows.Count > 0)
                        {
                            entGuiaRemitente.entGRR_Respuesta_MensajeResultado = ConcatenarRespuesta;
                            entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = dtRespuestaSunat_Guardado.Rows[0]["CodigoRespuesta"].ToString();
                        }
                        else
                        {
                            entGuiaRemitente.entGRR_Respuesta_MensajeResultado = consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion;
                            entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = "03";
                        }

                        clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaRemitente);
                       
                    }



                    if (esRespuesta)
                    {
                        if (esAprobado)
                        {
                            lblEstado.Text = entGuiaRemitente.entGRR_Respuesta_EstadoSunat;
                            pEstado.BackColor = Color.Lime;
                            AbrirPDF();
                            MessageBox.Show(entGuiaRemitente.entGRR_Respuesta_EstadoSunat + ": " + ConcatenarRespuesta, "Respuesta CDR SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            lblEstado.Text = entGuiaRemitente.entGRR_Respuesta_EstadoSunat;
                            pEstado.BackColor = Color.Red;
                            MessageBox.Show(entGuiaRemitente.entGRR_Respuesta_EstadoSunat + ": " + ConcatenarRespuesta, "Respuesta CDR SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        
                        backgroundWorker1.CancelAsync();
                    }
                    else if (esTiempoExcedido)
                    {
                        MessageBox.Show(entGuiaRemitente.entGRR_Respuesta_EstadoSunat + ": " + ConcatenarRespuesta, "Respuesta CDR SUNAT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

            }

        }

        private void ConfirmarOtorgadoLeido()
        {
            // ********* CONSULTAR MASIVO ESTADO GUIAS **********

            ServiceGRR_QA.ene_ConsultarEstado consultarEstado = new ServiceGRR_QA.ene_ConsultarEstado();
            consultarEstado.at_CantidadConsultar = 1;
            consultarEstado.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M;

            // ********** CONFIRMAR ESTADO ***********

            ServiceGRR_QA.ene_ConfirmarEstado confirmarEstado = new ServiceGRR_QA.ene_ConfirmarEstado();
            confirmarEstado.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M;
            confirmarEstado.l_Comprobante = new ServiceGRR_QA.ArrayOfEn_ComprobanteConfirmarEstado();




            // TRAER RESPUESTA DE CONSLTA (DATOS DE GUIA)

            ServiceGRR_QA.ens_ConsultarEstadoGR responseConsulta = new ServiceGRR_QA.ens_ConsultarEstadoGR();
            responseConsulta = request.ConsultarEstadoGRR(consultarEstado);

            for (int i = 0; i < responseConsulta.l_ResultadoEstadoComprobante.Count; i++)
            {
                if (entGuiaRemitente.entGRR_Generales_Numero_M == responseConsulta.l_ResultadoEstadoComprobante[i].at_Numero &&
                    entGuiaRemitente.entGRR_Generales_Serie_M == responseConsulta.l_ResultadoEstadoComprobante[i].at_Serie)
                {


                    entGuiaRemitente.entGRR_Respuesta_FechaLeido = responseConsulta.l_ResultadoEstadoComprobante[i].ent_EstadoLeido.at_FechaLeido.ToString();
                    entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento = responseConsulta.l_ResultadoEstadoComprobante[i].ent_EstadoOtorgado.at_FechaOtorgado.ToString();

                }

                // ************ CONFIRMAR GUIA ESPECIFICA ******************
                ServiceGRR_QA.en_ComprobanteConfirmarEstado comprobanteConfirmarEstado = new ServiceGRR_QA.en_ComprobanteConfirmarEstado();
                comprobanteConfirmarEstado.at_Serie = Convert.ToString(responseConsulta.l_ResultadoEstadoComprobante[i].at_Serie);
                comprobanteConfirmarEstado.at_Numero = Convert.ToInt32(responseConsulta.l_ResultadoEstadoComprobante[i].at_Numero);
                confirmarEstado.l_Comprobante.Add(comprobanteConfirmarEstado);

            }


            request.ConfirmarEstadoGRR(confirmarEstado);
            
        }

        private void CargarArchivoXML()
        {
            ens_ConsultarXML Archivo_XML = CargarGuiaEnXML(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M, 0);

            while (Archivo_XML.at_NivelResultado == 0)
            {
                Archivo_XML = CargarGuiaEnXML(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M, 0);
            }
            entGuiaRemitente.entGRR_Respuesta_XML_Archivo = Encoding.UTF8.GetString(Archivo_XML.ent_ResultadoXML.at_XML);
            
        }

        private void CargarGuiaEnPDF()
        {
            ene_ConsultarRI consultaRI = new ene_ConsultarRI();
            consultaRI.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M;
            consultaRI.ent_Comprobante = new en_ComprobanteConsultarRI();
            consultaRI.ent_Comprobante.at_Serie = entGuiaRemitente.entGRR_Generales_Serie_M;
            consultaRI.ent_Comprobante.at_Numero = entGuiaRemitente.entGRR_Generales_Numero_M;
            resultadoRI = new ens_ResultadoRI();
            resultadoRI = request.ConsultarRI_GRR(consultaRI);
            while (resultadoRI.at_NivelResultado == 0)
            {
                resultadoRI = request.ConsultarRI_GRR(consultaRI);

                if (resultadoRI.at_NivelResultado != 0)
                {
                    entGuiaRemitente.entGRR_Respuesta_ArchivoPDF = resultadoRI.ent_Resultado.at_ArchivoRI;
                    entGuiaRemitente.entGRR_Respuesta_NombreRI = resultadoRI.ent_Resultado.at_NombreRI;
                    entGuiaRemitente.entGRR_Respuesta_FechaRI = resultadoRI.ent_Resultado.at_FechaGenerado;
                }

            }
        }

        private void AbrirPDF()
        {

            Stream stream = new MemoryStream(resultadoRI.ent_Resultado.at_ArchivoRI);
            AbrirPdf open = new AbrirPdf();
            //open.pdfFileSaveAsBarItem1.Enabled = false;
            open.pdfViewer1.LoadDocument(stream);
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
            Respuesta_XML_CDR = request.ConsultarXMLGRR(consultarXML_CDR);

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
            consultaIndividual = request.ConsultaIndividualGRR(respuestaSunat); // AQUI OBTENGO LA RESPUESTA SUNAT "consultaIndividual"


            entGuiaRemitente.entGRR_Respuesta_FechaGeneracion = consultaIndividual.ent_InformacionComprobante.at_FechaGeneracion;
            entGuiaRemitente.entGRR_Respuesta_NivelResultado = consultaIndividual.at_NivelResultado;
            entGuiaRemitente.entGRR_Respuesta_Guardar_Sunat = consultaIndividual.at_MensajeResultado;
            entGuiaRemitente.entGRR_Respuesta_FechaTransmision = consultaIndividual.ent_InformacionComprobante.at_FechaTransmision;

            
        }
        private void FrmGuiaElectronicaTransportista_FormClosing(object sender, FormClosingEventArgs e)
        {
            //backgroundWorker1.CancelAsync();
        }

        private void cbxUnidadMedidaTotal_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
 
                /*if (dgvProductosGuia.Rows.Count > 0)
                {
                    dgvProductosGuia.Rows[dgvProductosGuia.RowCount - 1].Cells["UnidadMedida"].Value = cbxUnidadMedidaTotal.SelectedValue;
                }*/
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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRazonSocialProveedor, ref lstProveedor, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);

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
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRazonSocialProveedor, ref  lstProveedor, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstProveedor.SelectedItems[0];
                    txtRazonSocialProveedor.Text = ItemActual.SubItems[1].Text;
                    txtRucProveedor.Text = ItemActual.SubItems[2].Text;
                    txtTipoDocumentoProveedor.Tag = ItemActual.SubItems[3].Text;
                    txtTipoDocumentoProveedor.Text = ItemActual.SubItems[4].Text;
                       

                    entGuiaRemitente.entGRR_Proveedor_RazonSocial = txtRazonSocialProveedor.Text;
                    entGuiaRemitente.entGRR_Proveedor_NumeroDocumentoIdentidad = txtRucProveedor.Text;
                    entGuiaRemitente.entGRR_Proveedor_TipoDocumentoIdentidad = txtTipoDocumentoProveedor.Tag.ToString();

                    if (dgvDocumentosRelacionados.Rows.Count > 0)
                    {
                        dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Proveedor_NumeroDocumentoIdentidad;
                        dgvDocumentosRelacionados.Rows[0].Cells["TipoDocumentoIdentidad"].Value = entGuiaRemitente.entGRR_Proveedor_TipoDocumentoIdentidad;

                    }

                    if (txtRazonSocialProveedor.Tag == null)
                    {
                        txtTipoDocumentoProveedor.Clear();
                        txtRucProveedor.Clear();
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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRazonSocialProveedor, ref lstProveedor, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRazonSocialSubContra_Enter(object sender, EventArgs e)
        {
            txtRazonSocialProveedor.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRazonSocialSubContra_Leave(object sender, EventArgs e)
        {
            txtRazonSocialProveedor.BackColor = Color.White;
        }

        private void txtRazonSocialSubContra_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRazonSocialProveedor, ref  lstProveedor, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    
                    txtDireccionDestino.Enabled = true;


                    if (txtRazonSocialProveedor.Tag == null)
                    {
                        txtTipoDocumentoProveedor.Clear();
                        txtRucProveedor.Clear();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtRazonSocialSubContra_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRazonSocialProveedor, ref lstProveedor, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    txtDireccionDestino.Enabled = true;


                    if (txtRazonSocialProveedor.Tag == null)
                    {
                        txtTipoDocumentoProveedor.Clear();
                        txtRucProveedor.Clear();
                    }
                }
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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRazonSocialTransportista, ref lstTransportista, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);

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
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRazonSocialTransportista, ref  lstTransportista, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {

                    ListViewItem ItemActual;
                    ItemActual = lstTransportista.SelectedItems[0];
                    txtRazonSocialTransportista.Text = ItemActual.SubItems[1].Text;
                    txtRucTransportista.Text = ItemActual.SubItems[2].Text;
                    txtTipoDocTransportista.Tag = ItemActual.SubItems[3].Text;
                    txtTipoDocTransportista.Text = ItemActual.SubItems[4].Text;
                    txtNroMTCTransportista.Text = ItemActual.SubItems[5].Text;

                    entGuiaRemitente.entGRR_Transportista_RazonSocial = txtRazonSocialTransportista.Text;
                    entGuiaRemitente.entGRR_Transportista_NumeroDocumentoIdentidad = txtRucTransportista.Text;
                    entGuiaRemitente.entGRR_Transportista_TipoDocumentoIdentidad = txtTipoDocTransportista.Tag.ToString();

                  /*  if (entGuiaRemitente.entGRR_Transportista_NumeroDocumentoIdentidad == "20439331918")
                    {
                        txtNroMTCTransportista.Text = "130068CNG";
                    }*/

                    if (txtRazonSocialTransportista.Tag == null)
                    {
                        txtTipoDocTransportista.Clear();
                        txtRucTransportista.Clear();
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
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRazonSocialTransportista, ref lstTransportista, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRazonSocialPagadorTercero_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRazonSocialTransportista, ref  lstTransportista, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {


                    if (txtRazonSocialTransportista.Tag == null)
                    {
                        txtTipoDocTransportista.Clear();
                        txtRazonSocialTransportista.Clear();
                    }
                 
                    groupMTCTrans.Select();
                    txtNroMTCTransportista.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }*/
        }

        private void txtRazonSocialPagadorTercero_Enter(object sender, EventArgs e)
        {
            txtRazonSocialTransportista.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtRazonSocialPagadorTercero_Leave(object sender, EventArgs e)
        {
            txtRazonSocialTransportista.BackColor = Color.White;
        }

        private void txtRazonSocialPagadorTercero_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRazonSocialTransportista, ref lstTransportista, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
                {
                    if (txtRazonSocialTransportista.Tag == null)
                    {
                        txtTipoDocTransportista.Clear();
                        txtRazonSocialTransportista.Clear();
                    }

                    groupMTCTrans.Select();
                    txtNroMTCTransportista.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUbigeoLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {

           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtUbigeoLlegada, ref  lstUbigeoLlegada, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                {



                    txtDireccionDestino.Enabled = true;
                    lstUbigeoLlegada.Visible = false;

                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                    }


                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();
                }

           
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
           
        }

        private void txtUbigeoLlegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtUbigeoLlegada, ref lstUbigeoLlegada, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                {
                    txtDireccionDestino.Enabled = true;
                    lstUbigeoLlegada.Visible = false;

                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
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

        private void txtUbigeoLlegada_Enter(object sender, EventArgs e)
        {
            txtUbigeoLlegada.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtUbigeoLlegada_Leave(object sender, EventArgs e)
        {
            txtUbigeoLlegada.BackColor = Color.White;
        }

        private void lstUbigeoLlegada_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtUbigeoLlegada, ref lstUbigeoLlegada, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstUbigeoLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtUbigeoLlegada, ref  lstUbigeoLlegada, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                {

                    groupDireccionDestino.Select();
                    txtDireccionDestino.Focus();

                    entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M = txtUbigeoLlegada.Tag.ToString(); // ubigeo destino

                    if (txtUbigeoLlegada.Tag == null)
                    {
                        txtUbigeoLlegada.Clear();
                    }
        

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstUbigeoLlegada_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtUbigeoLlegada, ref lstUbigeoLlegada, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUbigeoPartida_Enter(object sender, EventArgs e)
        {
            txtUbigeoPartida.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtUbigeoPartida_Leave(object sender, EventArgs e)
        {
            txtUbigeoPartida.BackColor = Color.White;
        }

        private void txtUbigeoPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtUbigeoPartida, ref  lstUbigeoPartida, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                {


                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionPartida.Clear();
                        txtDireccionPartida.Enabled = false;
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                    }
                    else
                    {
                        groupDireccionPartida.Select();
                        txtDireccionPartida.Focus();
                    }

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtUbigeoPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtUbigeoPartida, ref lstUbigeoPartida, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                {
                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionPartida.Clear();
                        txtDireccionPartida.Enabled = false;
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                    }
                    else
                    {
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

        private void lstUbigeoPartida_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtUbigeoPartida, ref lstUbigeoPartida, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstUbigeoPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtUbigeoPartida, ref  lstUbigeoPartida, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica))
                {

                   // groupDireccionPartida.Select();
                    txtDireccionPartida.Focus();

                    entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M = txtUbigeoPartida.Tag.ToString();// ubigeo partida

                    if (txtUbigeoPartida.Tag == null)
                    {
                        txtUbigeoPartida.Clear();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstUbigeoPartida_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtUbigeoPartida, ref lstUbigeoPartida, clsOperacionesBL.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxModalidadTransporte_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                MostrarOcultarOpciones();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarOcultarOpciones()
        {
            if (cbxModalidadTransporte.Text.ToString() == "Transporte Tercero")
            {
                Publico = true;
                Privado = false;
                cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Unchecked;
                cbxTipoServicios.Properties.Items[2].Enabled = false;
                chkActivarDocumentoRelcion.Enabled = true;
                MostrarProveedor();
                MostrarTransportista();
                MostrarOcultarOtros();
                OcultarVehiculo();
                MostrarOcultarEstablecimientos();

                /*if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    txtRazonSocialTransportista.Text = entGuiaRemitente.entGRR_Transportista_RazonSocial;
                    txtRazonSocialPagadorTercero_KeyPress(this, new KeyPressEventArgs((char)(Keys.Escape)));
                    lstTransportista.Select();
                    lstFleteTercero_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstFleteTercero_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                }*/

            }

            if (cbxModalidadTransporte.Text.ToString() == "Transporte Propio")
            {
                Privado = true;
                Publico = false;
                chkActivarDocumentoRelcion.Checked = false;
                MostrarProveedor();
                OcultarTransportista();
                MostrarOcultarOtros();
                MostrarVehiculo();
                MostrarOcultarEstablecimientos();

            }


        }

        private void MostrarOcultarEstablecimientos()
        {
            if (cbxMotivoTraslado.Text.ToString() == "Traslado entre establecimientos de la misma empresa")
            {
                txtDireccionDestino.Enabled = true;
                txtUbigeoPartida.Enabled = true;
                txtDireccionPartida.Enabled = true;
                txtUbigeoLlegada.Enabled = true;
                tabPage3.Parent = tabContingencia;

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                   /* txtEstablecimientoOrigen.Text = "NRO. 4808 OTR. PARCELA RUSTICA UC 4808 FUNDO LARREA LA LIBERTAD - TRUJILLO - MOCHE";
                    txtDireccionPartida.Text = "NRO. 4808 OTR. PARCELA RUSTICA UC 4808 FUNDO LARREA";

                    txtUbigeoPartida.Text = "LA LIBERTAD,TRUJILLO,MOCHE";
                    txtUbigeoPartida.Tag = "130107";
                    entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M = "130107";


                    txtEstablecimientoOrigen.Tag = "0013";
                    entGuiaRemitente.CodigoEstablecimientoOrigen = txtEstablecimientoOrigen.Tag.ToString();

                    esEstablecimientoPropio = true;*/
                    if (cbxSerieGuia.Items.Count > 0)
                    {
                        cbxSerieGuia_SelectedValueChanged(null, null);
                    }
                   

                    txtEmpresaDestinatario.Text = "GRUPO TRANSPESA S.A.C";
                    txtEmpresaDestinatario.Tag = "1553";
                    txtEmpresaDestinatario.Enabled = false;

                    txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                    lstEmpresaDestinatario.Select();
                    lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));

                }

                if (TipoOperacion == Utilitario.TipoOperacion.Editar)
                {


                    /*txtEmpresaDestinatario.Text = entGuiaRemitente.entGRR_Destinatario_RazonSocial_M;
                    txtEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Space)));
                    lstEmpresaDestinatario.Select();
                    txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                    lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));*/

                    txtEstablecimientoOrigen.Text = entGuiaRemitente.NombreEstablecimientoOrigen;
                    txtEstablecimientoOrigen.Tag = entGuiaRemitente.CodigoEstablecimientoOrigen;

                    txtEstablecimientoDestino.Text = entGuiaRemitente.NombreEstablecimientoDestino;
                    txtEstablecimientoDestino.Tag = entGuiaRemitente.CodigoEstablecimientoDestino;
                }
              
                
                
               
                
            }
            else
            {
                tabPage3.Parent = null;
                esEstablecimientoPropio = false;
                txtDireccionDestino.Enabled = true;
                txtDireccionPartida.Enabled = true;
                txtEmpresaDestinatario.Enabled = true;
                txtUbigeoPartida.Enabled = true;
                txtUbigeoLlegada.Enabled = true;

            }
        }

        private void MostrarOcultarOtros()
        {
            if (cbxMotivoTraslado.Text.ToString() == "Otros")
            {
                cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Unchecked;
                groupOtrosDetalle.Visible = true;
            }
            else
            {
                
                groupOtrosDetalle.Visible = false;
            }
        }




        private void MostrarVehiculo()
        {
            if (cbxModalidadTransporte.Text.ToString() == "Transporte Propio" )
            {
                tabPage2.Parent = tabContingencia;
                ConVehiculo = true;
                entGuiaRemitente.ConVehiculo = true;
                checkTercero.Visible = true;
            }
            if (cbxTipoServicios.Properties.Items[2].CheckState == CheckState.Checked)
            {
                tabPage2.Parent = null;
                ConVehiculo = false;
                entGuiaRemitente.ConVehiculo = false;
                checkTercero.Visible = false;
            }
            if (cbxTipoServicios.Properties.Items[2].CheckState == CheckState.Unchecked && cbxModalidadTransporte.Text.ToString() == "Transporte Propio")
            {
                tabPage2.Parent = tabContingencia;
                ConVehiculo = true;
                entGuiaRemitente.ConVehiculo = true;
                checkTercero.Visible = true;
            }
        }
        private void OcultarVehiculo()
        {
            
                tabPage2.Parent = null;
                ConVehiculo = false;
                entGuiaRemitente.ConVehiculo = false;
                checkTercero.Visible = false;
        }


        private void OcultarTransportista()
        {
            tabPage6.Parent = null;
            ConTransportista = false;
           
        }

        private void MostrarTransportista()
        {
            tabPage6.Parent = tabContingencia; //Con Dam
            tabContingencia.SelectedTab = tabPage6; // seleccionar el tab
            ConTransportista = true;

        }

        private void MostrarProveedor()
        {


            if (cbxMotivoTraslado.Text.ToString() != "Traslado entre establecimientos de la misma empresa" && cbxModalidadTransporte.Text.ToString() == "Transporte Tercero")
            {
                tabPage5.Parent = tabContingencia; //Provedor
                tabContingencia.SelectedTab = tabPage5; // seleccionar el tab
                ConProveedor = true;
                esEstablecimientoPropio = false;
            }
            else if (cbxMotivoTraslado.Text.ToString() == "Traslado entre establecimientos de la misma empresa" && cbxModalidadTransporte.Text.ToString() == "Transporte Propio")
            {
                tabPage5.Parent = null; //Provedor
                ConProveedor = false;
                esEstablecimientoPropio = true;

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                    cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[2].Enabled = true;
                    cbxTipoServicios.Properties.Items[3].Enabled = true;
                    cbxTipoServicios.Properties.Items[4].Enabled = true;
                }



            }
            else if (cbxMotivoTraslado.Text.ToString() == "Traslado entre establecimientos de la misma empresa" && cbxModalidadTransporte.Text.ToString() == "Transporte Tercero")
            {


                tabPage5.Parent = null; //Provedor
                ConProveedor = false;
                esEstablecimientoPropio = true;

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                    cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[2].Enabled = true;
                    cbxTipoServicios.Properties.Items[3].Enabled = true;
                    cbxTipoServicios.Properties.Items[4].Enabled = true;
                }


            }
            if (cbxMotivoTraslado.Text.ToString() == "Devolucion" && cbxModalidadTransporte.Text.ToString() == "Transporte Tercero")
            {

                /*tabPage5.Parent = tabContingencia; //Provedor
                tabContingencia.SelectedTab = tabPage5; // seleccionar el tab
                ConProveedor = true;*/

                tabPage5.Parent = null; //Provedor
                ConProveedor = false;
                esEstablecimientoPropio = false;
                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                    cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[3].Enabled = true;
                    cbxTipoServicios.Properties.Items[4].Enabled = true;
                }

            }
            else if (cbxMotivoTraslado.Text.ToString() == "Devolucion" && cbxModalidadTransporte.Text.ToString() == "Transporte Propio")
            {

                tabPage5.Parent = null; //Provedor no deberia ir
                txtRazonSocialProveedor.Enabled = false; // para que no se vea feo solo desactivola caja de texto 
                ConProveedor = false;
                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                    cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Checked;
                    cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Checked;
                }

            }

            if (cbxMotivoTraslado.Text.ToString() == "Otros")
            {
                
                checkConProveedor.Visible = true;
                checkConProveedor.Checked = true;
                
            }
            if (cbxMotivoTraslado.Text.ToString() == "Otros" && cbxModalidadTransporte.Text.ToString() == "Transporte Propio")
            {

                cbxTipoServicios.Properties.Items[4].Enabled = true;
                cbxTipoServicios.Properties.Items[3].Enabled = true;
                tabPage2.Parent = tabContingencia;
                ConVehiculo = true;
                entGuiaRemitente.ConVehiculo = true;
            }
            
            if (cbxMotivoTraslado.Text.ToString() == "Compra")
            {
                txtRazonSocialProveedor.Enabled = true;
                ConProveedor = true;

            }

            entGuiaRemitente.Publico = Publico;
            entGuiaRemitente.Privado = Privado;



            // actualizo el tipo de servicio correcto

            if (cbxMotivoTraslado.Text.ToString() == "Compra" && cbxModalidadTransporte.Text.ToString() == "Transporte Tercero")
            {
                tabPage5.Parent = tabContingencia; //Provedor
                tabContingencia.SelectedTab = tabPage5; // seleccionar el tab
                ConProveedor = true;


                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                    cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[3].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[2].Enabled = false;
                    cbxTipoServicios.Properties.Items[3].Enabled = false;
                    cbxTipoServicios.Properties.Items[4].Enabled = false;
                }


            }else if(cbxMotivoTraslado.Text.ToString() == "Compra" && cbxModalidadTransporte.Text.ToString() == "Transporte Propio")
            {
                tabPage5.Parent = tabContingencia; //Provedor
                tabContingencia.SelectedTab = tabPage5; // seleccionar el tab
                ConProveedor = true;

                if (TipoOperacion == Utilitario.TipoOperacion.Registrar)
                {
                    cbxTipoServicios.Popup += cbxTipoServicios_Popup;
                    cbxTipoServicios.Properties.Items[2].CheckState = CheckState.Unchecked; //CheckState.Checked;
                    cbxTipoServicios.Properties.Items[2].Enabled = true;
                    cbxTipoServicios.Properties.Items[4].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[4].Enabled = false;
                    cbxTipoServicios.Properties.Items[3].CheckState = CheckState.Unchecked;
                    cbxTipoServicios.Properties.Items[3].Enabled = false;
                }


                txtEmpresaDestinatario.Text = "GRUPO TRANSPESA S.A.C";
                txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                lstEmpresaDestinatario.Select();
                lstEmpresaDestinatario_KeyUp(this, new KeyEventArgs(Keys.Down));
                lstEmpresaDestinatario_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                txtEmpresaDestinatario_KeyUp(this, new KeyEventArgs((Keys.Enter)));

            }


        }

        public void OcultarProveedor()
        {
            tabPage5.Parent = null;
            ConProveedor = false;
            entGuiaRemitente.Publico = Publico;
            entGuiaRemitente.Privado = Privado;

        }

        private void cbxMotivoTraslado_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                MostrarOcultarOpciones();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductosGuia_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            /*if (cbxUnidadMedidaTotal.Text == "UNIDAD")
            {
                if (dgvProductosGuia.Columns[e.ColumnIndex].Name == "Cantidad")
                {
                    Decimal totalColumna = 0;
                    foreach (DataGridViewRow row in dgvProductosGuia.Rows)
                    {
                        Decimal pedido = 0;
                        if (!Decimal.TryParse(Convert.ToString(row.Cells["Cantidad"].Value), out pedido))
                            continue;

                        Decimal totalFila = Convert.ToDecimal(row.Cells["Cantidad"].Value);
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
            }*/

        }

        private void chkActivarDocumentoRelcion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivarDocumentoRelcion.Checked)
            {
                gAnexarGR.Enabled = true;
                checkDocRelacion.Enabled = true;
            }
            else
            {
                gAnexarGR.Enabled = false;
                checkDocRelacion.Checked = false;
                checkDocRelacion.Enabled = false;
            }
           
           
        }

        private void txtNroMTCTransportista_Enter(object sender, EventArgs e)
        {
            txtNroMTCTransportista.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtNroMTCTransportista_Leave(object sender, EventArgs e)
        {
            txtNroMTCTransportista.BackColor = Color.White;
        }

        private void checkConProveedor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkConProveedor.Checked)
            {
                ConProveedor = checkConProveedor.Checked;
                
            }
            else
            {
                ConProveedor = false;
                tabPage5.Parent = null;
            }
        }

        private void txtEstablecimientoOrigen_Enter(object sender, EventArgs e)
        {
            txtEstablecimientoOrigen.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtEstablecimientoOrigen_Leave(object sender, EventArgs e)
        {
            txtEstablecimientoOrigen.BackColor = Color.White;
        }

        private void txtEstablecimientoOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {

           /* try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEstablecimientoOrigen, ref  lstEstablecimientoOrigen, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos))
                {

 
                    if (txtEstablecimientoOrigen.Tag == null)
                    {
                        txtEstablecimientoOrigen.Clear();
                       
                    }
                    groupEstablecimientoDestino.Select();
                    txtEstablecimientoDestino.Focus();


                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtEstablecimientoOrigen_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEstablecimientoOrigen, ref lstEstablecimientoOrigen, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos))
                {

                    if (txtEstablecimientoOrigen.Tag == null)
                    {
                        txtEstablecimientoOrigen.Clear();

                    }
                    groupEstablecimientoDestino.Select();
                    txtEstablecimientoDestino.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEstablecimientoOrigen_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEstablecimientoOrigen, ref lstEstablecimientoOrigen, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstEstablecimientoOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEstablecimientoOrigen, ref  lstEstablecimientoOrigen, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos))
                {


                    if (txtEstablecimientoOrigen.Tag == null)
                    {
                        txtEstablecimientoOrigen.Clear();
                    }

                    txtDireccionPartida.Clear();
                    txtDireccionPartida.Text = txtEstablecimientoOrigen.Text;
                    entGuiaRemitente.CodigoEstablecimientoOrigen = txtEstablecimientoOrigen.Tag.ToString();
                    

                    ListViewItem ItemActual;
                    ItemActual = lstEstablecimientoOrigen.SelectedItems[0];
                    txtUbigeoPartida.Text = ItemActual.SubItems[2].Text +","+ ItemActual.SubItems[3].Text +","+ ItemActual.SubItems[4].Text;
                    txtUbigeoPartida.Tag = ItemActual.SubItems[5].Text;
                    entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
                    entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M = txtUbigeoPartida.Tag.ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstEstablecimientoOrigen_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEstablecimientoOrigen, ref lstEstablecimientoDestino, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEstablecimientoDestino_Enter(object sender, EventArgs e)
        {
            txtEstablecimientoDestino.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtEstablecimientoDestino_Leave(object sender, EventArgs e)
        {
            txtEstablecimientoDestino.BackColor = Color.White;
        }

        private void txtEstablecimientoDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtEstablecimientoDestino, ref  lstEstablecimientoDestino, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos))
                {


                    if (txtEstablecimientoDestino.Tag == null)
                    {
                        txtEstablecimientoDestino.Clear();

                    }

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtEstablecimientoDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtEstablecimientoDestino, ref lstEstablecimientoDestino, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos))
                {
                    if (txtEstablecimientoDestino.Tag == null) { txtEstablecimientoDestino.Clear(); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstEstablecimientoDestino_Enter(object sender, EventArgs e)
        {
            try { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEstablecimientoDestino, ref lstEstablecimientoDestino, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstEstablecimientoDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtEstablecimientoDestino, ref  lstEstablecimientoDestino, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos))
                {
                    if (txtEstablecimientoDestino.Tag == null) { txtEstablecimientoDestino.Clear(); }

                    txtDireccionDestino.Clear();
                    entGuiaRemitente.CodigoEstablecimientoDestino = txtEstablecimientoDestino.Tag.ToString();
                    txtDireccionDestino.Text = txtEstablecimientoDestino.Text;

                    ListViewItem ItemActual;
                    ItemActual = lstEstablecimientoDestino.SelectedItems[0];
                    txtUbigeoLlegada.Text = ItemActual.SubItems[2].Text + "," + ItemActual.SubItems[3].Text + "," + ItemActual.SubItems[4].Text;
                    txtUbigeoLlegada.Tag = ItemActual.SubItems[5].Text;
                    entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;
                    entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M = txtUbigeoLlegada.Tag.ToString();

                    if (Privado == true && ConVehiculo == true)
                    {
                        tabContingencia.SelectedTab = tabPage2;
                        groupPlacaVehiculo.Select();
                        txtPlaca.Focus();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstEstablecimientoDestino_KeyUp(object sender, KeyEventArgs e)
        {
            try { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtEstablecimientoDestino, ref lstEstablecimientoDestino, clsOperacionesBL.Instancia.ReportesApp_ListarEstablecimientos_Anexos); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                    if (combo.SelectedIndex < 0) return;

                    dgvDocumentosRelacionados.CurrentRow.Cells["NombreDocumento"].Value = Utilitario.Instancia.QuitarTildes((combo.Items[combo.SelectedIndex] as DataRowView).Row[1].ToString());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { dgvTrasladoProgramado.Rows.RemoveAt(dgvTrasladoProgramado.CurrentRow.Index); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void checkTercero_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTercero.Checked)
            {
                if (txtEmpresaDestinatario.Tag != null)
                {
                    frmSeleccionarPlacaConductorGuiaElectronica open = new frmSeleccionarPlacaConductorGuiaElectronica();
                    open.txtxEmpresaCliente.Tag = txtEmpresaDestinatario.Tag;
                    open.txtxEmpresaCliente.Text = txtEmpresaDestinatario.Text;
  
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        placaTercero = open.placaTercero;
                        tarjetaCirculacion = open.tarjetaCirculacion;
                        nombretipovehiculo = open.nombretipovehiculo;
                        idtipovehiculo = open.idtipovehiculo;


                        entGuiaRemitente.idconductor = open.idConductor;
                        entNuevoConductor.entGRR_Conductor_Nombres_M = open.nombres;
                        entNuevoConductor.entGRR_Conductor_Apellidos_M = open.apellidos;
                        entNuevoConductor.entGRR_Conductor_Licencia_M = open.licencia;
                        entNuevoConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M = open.documento;
                        entNuevoConductor.entGRR_Conductor_TipoDocumentoIdentidad_M = open.codigoDocumento;
                        entGuiaRemitente.entConductor = entNuevoConductor;

                        if (TipoTrasladoProgramado == false)
                        {
                            txtPlaca.Text = placaTercero;
                            txtPlaca_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                            lstPlaca.Select();
                            lstPlaca_KeyUp(this, new KeyEventArgs(Keys.Down));
                            lstPlaca_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                            txtPlaca_KeyUp(this, new KeyEventArgs((Keys.Enter)));

                            txtCarreta.Text = "";
                            txtCarreta_KeyUp(this, new KeyEventArgs((Keys.Escape)));
                            lstCarreta.Select();
                            lstCarreta_KeyUp(this, new KeyEventArgs(Keys.Down));
                            lstCarreta_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                            txtCarreta_KeyUp(this, new KeyEventArgs((Keys.Enter)));

                            txtConductor.Tag = open.idConductor;
                            txtConductor.Text = open.nombresCompletos;
                            txtLicencia.Text = open.licencia;
                            txtDocIdentidad.Text = open.documento;
                            txtTipoDocumentoIdentidad.Text = open.tipoDocumento;
                            txtTipoDocumentoIdentidad.Tag = open.codigoDocumento;
                            tabContingencia.SelectedTab = tabPage2;
                        }

                        if (TipoTrasladoProgramado == true)
                        {
                            txtPlaca2.Text = placaTercero;
                            txtPlaca2_KeyUp(this, new KeyEventArgs(Keys.Escape));
                            lstPlaca2.Select();
                            lstPlaca2_KeyUp(this, new KeyEventArgs(Keys.Down));
                            lstPlaca2_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                            txtPlaca2_KeyUp(this, new KeyEventArgs(Keys.Enter));

                            txtCarreta2.Text = placaTercero;
                            txtCarreta2_KeyUp(this, new KeyEventArgs(Keys.Escape));
                            lstCarreta2.Select();
                            lstCarreta2_KeyUp(this, new KeyEventArgs(Keys.Down));
                            lstCarreta2_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                            txtCarreta2_KeyUp(this, new KeyEventArgs(Keys.Enter));

                            entGuiaRemitente.idconductor = open.idConductor;
                            entNuevoConductor.entGRR_Conductor_Nombres_M = open.nombres;
                            entNuevoConductor.entGRR_Conductor_Apellidos_M = open.apellidos;
                            entNuevoConductor.entGRR_Conductor_Licencia_M = open.licencia;
                            entNuevoConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M = open.documento;
                            entNuevoConductor.entGRR_Conductor_TipoDocumentoIdentidad_M = open.codigoDocumento;
                            entGuiaRemitente.entConductor = entNuevoConductor;

                            txtConductor2.Tag = open.idConductor;
                            txtConductor2.Text = open.nombresCompletos;
                            txtLicencia2.Text = open.licencia;
                            txtDocIdentidad2.Text = open.documento;
                            txtTipoDocumento2.Text = open.tipoDocumento;
                            txtTipoDocumento2.Tag = open.codigoDocumento;
                            tabContingencia.SelectedTab = tabPage4;
                        }
                    }
                }
            }
        }

        private void dtpFechaTraslado_ValueChanged_1(object sender, EventArgs e)
        {
            if (TipoOperacion == Utilitario.TipoOperacion.Registrar) { dtpFechaTraslado.MinDate = DateTime.Now; }
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        public void FrmGuiaElectronicaRemitente_FormClosed(object sender, FormClosedEventArgs e)
        {
             //CargarListaRemitente(this);
        }

        private void lstEmpresaRemitente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtEmpresaRemitente, ref  lstEmpresaRemitente, clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica))
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
                        txtEmpresaRemitente.Text = entGuiaRemitente.entGRR_Remitente_RazonSocial_M;
                        DataTable dtClienteProgramacion = clsOperacionesBL.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(Convert.ToInt32(txtEmpresaRemitente.Tag));
                        entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["DocumentoFiscal"].ToString();
                        entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["Codigo"].ToString();

                        dtDireccionesRuta = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtEmpresaRemitente.Tag));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstUbigeoPartida_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem ItemActual;
                ItemActual = lstUbigeoPartida.SelectedItems[0];
                txtUbigeoPartida.Tag = Convert.ToString(ItemActual.Text);
                txtUbigeoPartida.Text = ItemActual.SubItems[1].Text;
                lstUbigeoPartida.Visible = false;
                
                txtDireccionPartida.Focus();
                
                entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M = txtUbigeoPartida.Tag.ToString();// ubigeo partida
                
                if (txtUbigeoPartida.Tag == null) { txtUbigeoPartida.Clear(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstDireccionPartida_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstDireccionPartida.SelectedItems[0];
            txtDireccionPartida.Tag = Convert.ToString(ItemActual.Text);
            txtDireccionPartida.Text = ItemActual.SubItems[1].Text;
            lstDireccionPartida.Visible = false;

            entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M = txtDireccionPartida.Text;
            groupUbigeoLlegada.Select();
            txtUbigeoLlegada.Focus();

        }

        private void lstEmpresaDestinatario_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstEmpresaDestinatario.SelectedItems[0];
            txtEmpresaDestinatario.Tag = Convert.ToString(ItemActual.Text);
            txtEmpresaDestinatario.Text = ItemActual.SubItems[1].Text;
            lstEmpresaDestinatario.Visible = false;

            try
            {
                ItemActual = lstEmpresaDestinatario.SelectedItems[0];
                txtDocIdentidadDesti.Text = ItemActual.SubItems[2].Text;
                entGuiaRemitente.idcliente = Convert.ToInt32(txtEmpresaDestinatario.Tag);
                //dgvDocumentosRelacionados.Rows[0].Cells["NumeroDocumentoIdentidad"].Value = ItemActual.SubItems[2].Text;
                //dgvDocumentosRelacionados.Rows[0].Cells["TipoDocumentoIdentidad"].Value = ItemActual.SubItems[3].Text;
                
                dtDireccionesRuta2 = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(Convert.ToInt32(txtEmpresaDestinatario.Tag));
                dtCorreos = clsOperacionesBL.Instancia.ReportesApp_ListarCorreos_Master(Convert.ToInt32(txtEmpresaDestinatario.Tag),txtDireccionDestino.Text);

                    if (dtCorreos.Rows.Count > 0)
                    {
                        IEnumerable<DataRow> ieRegistro = from fila in dtCorreos.AsEnumerable()
                                                          where fila.Field<bool>("Principal") == true
                                                          select fila;

                        if (ieRegistro.Any())
                        {
                            DataTable dtcorreoPrincipal = ieRegistro.CopyToDataTable();
                            entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M = dtcorreoPrincipal.Rows[0]["Correo"].ToString();
                            entGuiaRemitente.entGRR_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtcorreoPrincipal.Rows[0]["idCorreo"]);
                            txtCorreoPrimario.Text = dtcorreoPrincipal.Rows[0]["Correo"].ToString();

                            //CORREOS SECUNDARIOS
                            IEnumerable<DataRow> ieCorreoSecundario = from fila in dtCorreos.AsEnumerable()
                                                                      where fila.Field<bool>("Principal") == false
                                                                      select fila;

                            if (ieCorreoSecundario.Any())
                            {
                                DataTable dtCorreoSecundario = ieCorreoSecundario.CopyToDataTable();
                                entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreoSecundario);
                            }
                        }
                        else
                        {
                            entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M = dtCorreos.Rows[0]["Correo"].ToString();
                            entGuiaRemitente.entGRR_Remitente_Otorga_idCorreoPrincial_M = Convert.ToInt32(dtCorreos.Rows[0]["idCorreo"]);
                            txtCorreoPrimario.Text = dtCorreos.Rows[0]["Correo"].ToString();
                            dtCorreos.Rows.RemoveAt(0);

                            if (dtCorreos.Rows.Count > 0) { entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario = Utilitario.Instancia.DatatableToXml(dtCorreos); }
                        }
                    }
                    else
                    {
                        entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M = "";
                        txtCorreoPrimario.Clear();
                    }

                    entGuiaRemitente.entGRR_Destinatario_RazonSocial_M = txtEmpresaDestinatario.Text;
                    DataTable dtClienteProgramacion = clsOperacionesBL.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(Convert.ToInt32(txtEmpresaDestinatario.Tag));
                    if (dtClienteProgramacion.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron datos de la empresa destino, verificar si empresa se encuentra registrada  correctamente en el Spring", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["DocumentoFiscal"].ToString();
                    entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M = dtClienteProgramacion.Rows[0]["Codigo"].ToString();

                    if (esEstablecimientoPropio == false)
                    {
                        txtDireccionDestino.Enabled = true;
                        txtDireccionPartida.Enabled = true;
                        txtUbigeoLlegada.Enabled = true;
                        txtUbigeoPartida.Enabled = true;
                        groupUbigeoLlegada.Select();
                        txtUbigeoLlegada.Focus();
                    }
                    else
                    {
                        tabContingencia.SelectedTab = tabPage3;
                        groupEstablecimientoOrigen.Select();
                        txtEstablecimientoOrigen.Focus();
                    }

                    if (txtEmpresaDestinatario.Tag == null)
                    {
                        txtDireccionDestino.Clear();
                        txtDireccionDestino.Enabled = false;
                        txtDireccionPartida.Enabled = false;
                    }

                    HabilitarBotonCorreo();
            }
            catch (Exception ex ) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstUbigeoLlegada_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem ItemActual;
                ItemActual = lstUbigeoLlegada.SelectedItems[0];
                txtUbigeoLlegada.Tag = Convert.ToString(ItemActual.Text);
                txtUbigeoLlegada.Text = ItemActual.SubItems[1].Text;
                lstUbigeoLlegada.Visible = false;

                groupDireccionDestino.Select();
                txtDireccionDestino.Focus();
                entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M = txtUbigeoLlegada.Tag.ToString(); // ubigeo destino

                if (txtUbigeoLlegada.Tag == null) { txtUbigeoLlegada.Clear(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstDireccionDestino_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem ItemActual;
                ItemActual = lstDireccionDestino.SelectedItems[0];
                txtDireccionDestino.Tag = Convert.ToString(ItemActual.Text);
                txtDireccionDestino.Text = ItemActual.SubItems[1].Text;
                lstDireccionDestino.Visible = false;

                groupDireccionDestino.Select();
                txtDireccionDestino.Focus();
                entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M = txtDireccionDestino.Text;

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
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

