using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Entidades;
using Negocio;
using ReportesTranspesa.ServiceGRT_QA;
using ReportesTranspesa.ServiceGRR_QA;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors;
using DevExpress.Utils.Win;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ReportesTranspesa.Properties;
using System.Runtime.Serialization.Formatters.Binary;
//using PdfViewer;
using ReportesTranspesa.ServiceGRT_QA_Reversion;
using ReportesTranspesa.ServiceGRR_QA_Reversion;
using Entidades;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing.Printing;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraPdfViewer;
using DevExpress.Pdf;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes
{
    public partial class FrmListaGuiasElectronicas : Form
    {
        public DataTable dtListRespuestaError = new DataTable();
        public Boolean esRespuesta = false;
        public Boolean esReversion = false;
        private CancellationTokenSource _ctsGuias;

        //TRANSPORTISTA
        public ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient requestTransportista;
        public ServiceGRT_QA.ens_ConsultarComprobanteIndividual consultaIndividual;
        public ServiceGRT_QA.ene_ConsultarComprobanteIndividual respuestaSunat;
        public ServiceGRT_QA.ens_ResultadoRI resultadoRiTransportista;


        //REMITENTE
        public ServiceGRR_QA.ServicioGuiaRemisionRemitenteClient requestRemitente;
        public ServiceGRR_QA.ens_ConsultarComprobanteIndividual consultaIndividualRemitente;
        public ServiceGRR_QA.ene_ConsultarComprobanteIndividual respuestaSunatRemitente;
        public ServiceGRR_QA.ens_ResultadoRI resultadoRiRemitente;


        public DataTable dtRespuestaSunat;
        public DataTable dtLista;
        public string CodTipoGuia = string.Empty;
        public DataTable dtFechaHora;
        public DataTable dtRespuestaXML_CDR;
        public string CDR_Respuesta_Descripcion;

        //Entidades SOAP TRANSPORTISTA

        public ReportesTranspesa.ServiceGRT_QA_Reversion.ene_ResumenReversion ent_ResumenReversion;
        public ReportesTranspesa.ServiceGRT_QA_Reversion.en_Emisor ent_Emisor;
        public ReportesTranspesa.ServiceGRT_QA_Reversion.en_DatoResumenReversion ent_DatoResumenReversion;
        public ReportesTranspesa.ServiceGRT_QA_Reversion.en_CabeceraResumenReversion ent_CabeceraResumenReversion;
        public ReportesTranspesa.ServiceGRT_QA_Reversion.ArrayOfEn_ComprobantesRevertidos listComprobantesRevertidos;
        public ReportesTranspesa.ServiceGRT_QA_Reversion.en_ComprobantesRevertidos ent_ComprobantesRevertidos;
        public ServiceGRT_QA_Reversion.ens_Respuesta response;
        public ServiceGRT_QA.ene_ConsultarXML consultarXML_CDR;


        //Entidades SOAP REMITENTE
        public ReportesTranspesa.ServiceGRR_QA_Reversion.ene_ResumenReversion ent_ResumenReversionRE;
        public ReportesTranspesa.ServiceGRR_QA_Reversion.en_Emisor ent_EmisorRE;
        public ReportesTranspesa.ServiceGRR_QA_Reversion.en_DatoResumenReversion ent_DatoResumenReversionRE;
        public ReportesTranspesa.ServiceGRR_QA_Reversion.en_CabeceraResumenReversion ent_CabeceraResumenReversionRE;
        public ReportesTranspesa.ServiceGRR_QA_Reversion.ArrayOfEn_ComprobantesRevertidos listComprobantesRevertidosRE;
        public ReportesTranspesa.ServiceGRR_QA_Reversion.en_ComprobantesRevertidos ent_ComprobantesRevertidosRE;
        public ServiceGRR_QA.ene_ConsultarXML consultarXML_CDRRemitente;
        public ServiceGRR_QA_Reversion.ens_Respuesta responseRE;



        public ServiceGRT_QA.en_ResultadoEstadoComprobanteGR respuestaCorreo;
    
        //estado correo
        ServiceGRT_QA.ene_ConsultarEstado consultarEstadoCorreo;

        public ServiceGRT_QA.ens_ConsultarEstadoGR consultarOtorgamiento;
        // Entidad Local
        public clsGRT entGuiaTransportista = new clsGRT();
        public clsGRR entGuiaRemitente = new clsGRR();

        public int TipoOperacion = -1;
        bool registrarGuia = false;
        int posicionFila = 0;
        int posicionColumna = 0;
        public string nroSerie = "";
        public int OpcionGE = 0;


        public string CarpetaAlacenamientoLogErrores = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\LogErrores\";
        public string CarpetaLogSoapError = @"\\192.168.4.237\ReportesTranspesa2\LogGuiasElectronicas\SOAP\";

        public FrmListaGuiasElectronicas()
        {
            InitializeComponent();
            cbxSerieFiltro.SelectedValueChanged -= cbxSerieFiltro_SelectedValueChanged;
            g_viaje.Visible = false;
        }

        private void FrmListaGuiasElectronicas_Load(object sender, EventArgs e)
        {
            try
            {
                VerificarPermisosFormulario();
                CargarTipoGuia();
                cbxSerieFiltro.SelectedValueChanged += cbxSerieFiltro_SelectedValueChanged;
                CargarSeries();
                //ListarGuiasElectronicas();
                if (TipoOperacion == 1)
                {
                    cbxTipoGuia.SelectedIndex = 0;
                    btnBuscar.PerformClick();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void VerificarPermisosFormulario()
        {
            clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(Utilitario.Instancia.SesionUsuario.usuario); //Trae los permisos del usuario
            DataTable dtPermisosEspeciales = null;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("FrmListaGuiasElectronicas");

            nuevaGuiaRemitenteToolStripMenuItem.Visible = false;
            nuevaGuiaTransportistaToolStripMenuItem.Visible = false;
            registrarSeriesToolStripMenuItem.Visible = false;
            verDetalleContext.Visible = false;
            reversionToolStripMenuItem.Visible = false;
            confirmarAnulacionSUNATToolStripMenuItem.Visible = false;
            eliminarRegistroToolStripMenuItem.Visible = false;

            if (dtPermisos.Rows.Count > 0)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                }

                if (dtPermisosEspeciales != null)
                {
                    if (dtPermisosEspeciales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                        {
                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registrar GR")
                            { nuevaGuiaRemitenteToolStripMenuItem.Visible = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registrar GT")
                            {
                                nuevaGuiaTransportistaToolStripMenuItem.Visible = true;
                                registrarGuia = true;
                            }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registrar Serie")
                            { registrarSeriesToolStripMenuItem.Visible = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Editar")
                            { verDetalleContext.Visible = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Reversion")
                            { reversionToolStripMenuItem.Visible = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Confirmar Anulacion Sunat")
                            { confirmarAnulacionSUNATToolStripMenuItem.Visible = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Eliminar Registro")
                            { eliminarRegistroToolStripMenuItem.Visible = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Tolvas")
                            { tolvasToolStripMenuItem.Enabled = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Reimprimir Ticket")
                            { reimprimirTicketToolStripMenuItem.Visible = true; }

                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Confirmar Recibido")
                            {
                                btnRecibidos.Visible = true;
                                btnCancelar.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        private void CargarTipoGuia()
        {
            if (chkEstadoGuia.Checked == false) { groupEstadoGuia.Enabled = false; }

            DataTable dtTipoGuias = clsOperacionesBL.Instancia.ListarTipoGuiaElectronica();
            if (dtTipoGuias.Rows.Count > 0)
            {
                cbxTipoGuia.DataSource = dtTipoGuias;
                cbxTipoGuia.DisplayMember = "NombreTipoGuia";
                cbxTipoGuia.ValueMember = "TipoGuia";
                cbxTipoGuia.SelectedIndex = 0;
            }
            else
            {
                btnBuscar.Enabled = false;
                MessageBox.Show("Combobox de Tipo Guia no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private async void ListarGuiasElectronicas()
        {
            if (cbxSerieFiltro.SelectedValue == null)
            {
                MessageBox.Show("Combobox de Series no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_ctsGuias != null)
            {
                _ctsGuias.Cancel();
                _ctsGuias.Dispose();
            }
            _ctsGuias = new CancellationTokenSource();

            this.Cursor = Cursors.WaitCursor;
            btnBuscar.Enabled = false;

            dtLista = new DataTable();
            dtgListaGuiasTransportista.DataSource = null;

            string tipoGuiaSeleccionado = cbxTipoGuia.SelectedValue != null ? cbxTipoGuia.SelectedValue.ToString().TrimEnd() : "";
            string fechaInicio = dtpFechaInicio.Text;
            string fechaFin = dtpFechaFin.Text;
            string serie = cbxSerieFiltro.Text;
            string numero = txtNumeroFiltro.Text;
            bool todos = chkEstadoGuia.Checked;
            bool aprobado = rbtAprobado.Checked;
            bool revertido = rbtRevertido.Checked;
            bool rechazado = rbtRechazado.Checked;
            string viaje = txtviaje.Text;
            string cliente = txtClienteFiltro.Text;
            CancellationToken token = _ctsGuias.Token;

            try
            {
                await Task.Run(() =>
                {
                    clsOperacionesBL.Instancia.ReportesApp_ListarGuiasElectronicas_Streaming(
                        tipoGuiaSeleccionado,
                        fechaInicio,
                        fechaFin,
                        serie,
                        numero,
                        todos,
                        aprobado,
                        revertido,
                        rechazado,
                        viaje,
                        cliente,
                        (colNames, colTypes) =>
                        {
                            if (this.IsDisposed || !this.IsHandleCreated) return;
                            this.Invoke(new Action(() =>
                            {
                                if (this.IsDisposed) return;
                                dtLista = new DataTable();
                                for (int i = 0; i < colNames.Length; i++)
                                {
                                    dtLista.Columns.Add(colNames[i], colTypes[i]);
                                }
                                dtgListaGuiasTransportista.DataSource = dtLista;
                                dgvListaGuiaTraspExpressVista.PopulateColumns();
                                ConfigurarVisibilidadColumnasGrid(tipoGuiaSeleccionado);
                            }));
                        },
                        (rowValues) =>
                        {
                            if (this.IsDisposed || !this.IsHandleCreated) return;
                            this.BeginInvoke(new Action(() =>
                            {
                                if (this.IsDisposed || dtLista == null) return;
                                dtLista.Rows.Add(rowValues);
                            }));
                        },
                        token
                    );
                }, token);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (!this.IsDisposed)
                {
                    btnBuscar.Enabled = true;
                    this.Cursor = Cursors.Default;
                    dgvListaGuiaTraspExpressVista.RefreshData();
                }
            }
        }

        private void ConfigurarVisibilidadColumnasGrid(string tipoGuia)
        {
            string[] columnasOcultas = {
                "TipoGuia", "idEmpresaGrupo", "idGuiaElectronica", "idGuiaSpring", "idViaje", "idOT", 
                "idCliente", "idDocumentosRelacion", "idIndicadoresServicio", "idProgramacion", 
                "idTipoProgramacion", "idConductoresGuia", "idProductosTraslado", "UbigeoPuntoPartida", 
                "UbigeoPuntoLlegada", "Ubigeo_Emisor", "idRuta", "GrupoInfoAdicional", "EtiquetaInfoAdicional", 
                "ValorInfoAdicional", "RazonSocial_Emisor", "NombreComercial_Emisor", "NumeroMTC_Emisor", 
                "Correo_Emisor", "SitioWeb_Emisor", "Telefono_Emisor", "DireccionDetallada_Emisor", 
                "Provincia_Emisor", "Departamento_Emisor", "Distrito_Emisor", "CodigoPais_Emisor", 
                "TipoDocIdentidad_Rem", "TipoDocIdentidad_Dest", "TipoDocIdentidad_Subcontra", 
                "TipoDocIdentidad_Contra", "NumeroDocIdentidad_Dest", "NumeroDocIdentidad_Contra", 
                "NumeroDocIdentidad_Subcontra", "SERVICIOS", "CodMotivo", "CodModalidad", 
                "xml_DocumentosRelacion", "xml_Conductores", "xml_Productos", "CodEstableOrigen", 
                "CodEstableDestino", "IdVehiculo", "idCarreta", "AnioProgramacion", "idRemitente", 
                "idDestinatario", "idOTEvento", "LineaOTEvento", "idRutaEvento", "CodigoHash"
            };

            foreach (string col in columnasOcultas)
            {
                if (dgvListaGuiaTraspExpressVista.Columns[col] != null)
                {
                    dgvListaGuiaTraspExpressVista.Columns[col].Visible = false;
                }
            }

            if (dgvListaGuiaTraspExpressVista.Columns["HoraEmision"] != null)
            {
                dgvListaGuiaTraspExpressVista.Columns["HoraEmision"].Visible = true;
            }

            if (tipoGuia == "T")
            {
                if (dgvListaGuiaTraspExpressVista.Columns["idOT"] != null) dgvListaGuiaTraspExpressVista.Columns["idOT"].Visible = true;
                if (dgvListaGuiaTraspExpressVista.Columns["LineaOT"] != null) dgvListaGuiaTraspExpressVista.Columns["LineaOT"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["NombreEstableOrigen"] != null) dgvListaGuiaTraspExpressVista.Columns["NombreEstableOrigen"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["NombreEstableDestino"] != null) dgvListaGuiaTraspExpressVista.Columns["NombreEstableDestino"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Proveedor"] != null) dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Proveedor"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Proveedor"] != null) dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Proveedor"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["RazonSocial_Proveedor"] != null) dgvListaGuiaTraspExpressVista.Columns["RazonSocial_Proveedor"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["Modalidad"] != null) dgvListaGuiaTraspExpressVista.Columns["Modalidad"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["MotivoTraslado"] != null) dgvListaGuiaTraspExpressVista.Columns["MotivoTraslado"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["GuiaEvento"] != null) dgvListaGuiaTraspExpressVista.Columns["GuiaEvento"].Visible = true;
                if (dgvListaGuiaTraspExpressVista.Columns["RutaEvento"] != null) dgvListaGuiaTraspExpressVista.Columns["RutaEvento"].Visible = true;
                if (dgvListaGuiaTraspExpressVista.Columns["GuiaEventoT"] != null) dgvListaGuiaTraspExpressVista.Columns["GuiaEventoT"].Visible = true;
                if (dgvListaGuiaTraspExpressVista.Columns["GuiaEventoRem"] != null) dgvListaGuiaTraspExpressVista.Columns["GuiaEventoRem"].Visible = true;
            }

            if (tipoGuia == "R")
            {
                if (dgvListaGuiaTraspExpressVista.Columns["idOT"] != null) dgvListaGuiaTraspExpressVista.Columns["idOT"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["DescripcionAdicional_Peso"] != null) dgvListaGuiaTraspExpressVista.Columns["DescripcionAdicional_Peso"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["Viaje"] != null) dgvListaGuiaTraspExpressVista.Columns["Viaje"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Trans"] != null) dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Trans"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Trans"] != null) dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Trans"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Proveedor"] != null) dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Proveedor"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Subcontra"] != null) dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Subcontra"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Subcontra"] != null) dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Subcontra"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["RazonSocial_Subcontra"] != null) dgvListaGuiaTraspExpressVista.Columns["RazonSocial_Subcontra"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Contra"] != null) dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Contra"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Contra"] != null) dgvListaGuiaTraspExpressVista.Columns["TipoDocIdentidad_Contra"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["RazonSocial_Contra"] != null) dgvListaGuiaTraspExpressVista.Columns["RazonSocial_Contra"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["NroTicketProgramacion"] != null) dgvListaGuiaTraspExpressVista.Columns["NroTicketProgramacion"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["AnioProgramacion"] != null) dgvListaGuiaTraspExpressVista.Columns["AnioProgramacion"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["TipoViaje"] != null) dgvListaGuiaTraspExpressVista.Columns["TipoViaje"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["MotivoTraslado"] != null) dgvListaGuiaTraspExpressVista.Columns["MotivoTraslado"].Visible = true;
                if (dgvListaGuiaTraspExpressVista.Columns["Modalidad"] != null) dgvListaGuiaTraspExpressVista.Columns["Modalidad"].Visible = true;
                if (dgvListaGuiaTraspExpressVista.Columns["GuiaEvento"] != null) dgvListaGuiaTraspExpressVista.Columns["GuiaEvento"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["RutaEvento"] != null) dgvListaGuiaTraspExpressVista.Columns["RutaEvento"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["GuiaEventoT"] != null) dgvListaGuiaTraspExpressVista.Columns["GuiaEventoT"].Visible = false;
                if (dgvListaGuiaTraspExpressVista.Columns["GuiaEventoRem"] != null) dgvListaGuiaTraspExpressVista.Columns["GuiaEventoRem"].Visible = false;
            }

            dgvListaGuiaTraspExpressVista.BestFitColumns();
            if (dgvListaGuiaTraspExpressVista.Columns["FechaInicio_Traslado"] != null) dgvListaGuiaTraspExpressVista.Columns["FechaInicio_Traslado"].Width = 80;
            if (dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Emisor"] != null) dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Emisor"].Width = 80;
            if (dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Rem"] != null) dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Rem"].Width = 80;
            if (dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Dest"] != null) dgvListaGuiaTraspExpressVista.Columns["NumeroDocIdentidad_Dest"].Width = 80;
            dgvListaGuiaTraspExpressVista.RefreshData();
        }

        private void CargarSeries()
        {
            DataTable dtSerieGuia = clsOperacionesBL.Instancia.ReportesApp_Listar_SerieGuiasElectronicas(cbxTipoGuia.SelectedValue.ToString());

            if (dtSerieGuia.Rows.Count > 0)
            {
                cbxSerieFiltro.DataSource = dtSerieGuia;
                cbxSerieFiltro.DisplayMember = "SerieGuia";
                cbxSerieFiltro.ValueMember = "SerieGuia";
                cbxSerieFiltro.SelectedIndex = 0;

                if (nroSerie.Length > 0) { cbxSerieFiltro.Text = nroSerie; }
                //cbxSerieFiltro.SelectedValueChanged += cbxSerieFiltro_SelectedValueChanged;
            }
        }

        private void ConsultarGuiaIndividualTransportista(string NumroDocumentoIdentidad, string Serie, int Numero)
        {
            //INSTANCIO LA CLASE NECESARIA PARA INDICARLE A SUNAT QUE GUIA QUIERO CONSULTAR (SERIE , NUEMRO)
            respuestaSunat = new ServiceGRT_QA.ene_ConsultarComprobanteIndividual();
            respuestaSunat.at_NumeroDocumentoIdentidad = NumroDocumentoIdentidad;
            respuestaSunat.at_Serie = Serie;
            respuestaSunat.at_Numero = Numero;

            // INSTANCIO LA CLASE QUE ALMACENARÁ LA RESPUESTA DE SUNAT
            consultaIndividual = new ServiceGRT_QA.ens_ConsultarComprobanteIndividual();
            requestTransportista = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
            consultaIndividual = requestTransportista.ConsultaIndividualGRT(respuestaSunat); // AQUI OBTENGO LA RESPUESTA SUNAT "consultaIndividual"
        }

        private void ConsultarGuiaIndividualRemitente(string NumroDocumentoIdentidad, string Serie, int Numero)
        {
            //INSTANCIO LA CLASE NECESARIA PARA INDICARLE A SUNAT QUE GUIA QUIERO CONSULTAR (SERIE , NUEMRO)
            respuestaSunatRemitente = new ServiceGRR_QA.ene_ConsultarComprobanteIndividual();
            respuestaSunatRemitente.at_NumeroDocumentoIdentidad = NumroDocumentoIdentidad;
            respuestaSunatRemitente.at_Serie = Serie;
            respuestaSunatRemitente.at_Numero = Numero;

            // INSTANCIO LA CLASE QUE ALMACENARÁ LA RESPUESTA DE SUNAT
            consultaIndividualRemitente = new ServiceGRR_QA.ens_ConsultarComprobanteIndividual();
            requestRemitente = new ServiceGRR_QA.ServicioGuiaRemisionRemitenteClient();
            consultaIndividualRemitente = requestRemitente.ConsultaIndividualGRR(respuestaSunatRemitente); // AQUI OBTENGO LA RESPUESTA SUNAT "consultaIndividual"
        }

        private void Cargando()
        {
            while (esRespuesta == false)
            {
                //consultaIndividual = requestTransportista.ConsultaIndividualGRT(respuestaSunat);
                dtRespuestaSunat = obtenerListaRespuestaSunat(consultaIndividual.ent_InformacionComprobante.l_respuestas);

                imgCargando.Visible = true;

                if (dtRespuestaSunat != null)
                {
                    if (dtRespuestaSunat.Rows.Count > 0)
                    {
                        esRespuesta = true;
                        imgCargando.Visible = false;

                        if (dtRespuestaSunat.Rows[0]["CodigoRespuesta"].ToString() == "2" || dtRespuestaSunat.Rows[0]["CodigoRespuesta"].ToString() == "1")
                        {

                            MessageBox.Show(dtRespuestaSunat.Rows[0]["Descripcion"].ToString(), "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(dtRespuestaSunat.Rows[0]["Descripcion"].ToString(), "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    entGuiaTransportista.entGRT_Respuesta_Xml_CDR = Utilitario.Instancia.DatatableToXml(dtRespuestaSunat);
                }
            }

        }


        private void AbrirPDFTransportista(ServiceGRT_QA.ens_ResultadoRI resultadoRI)
        {
            Stream stream = new MemoryStream(resultadoRI.ent_Resultado.at_ArchivoRI);
            string Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
            string Numero = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia")).ToString("D8");
            DataTable dtConductor = Utilitario.Instancia.ConvertirXMLaDatatable(Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Conductores")));

            AbrirPdf open = new AbrirPdf();
            open.pdfViewer1.LoadDocument(stream);

            //open.pdfViewer1.DefaultDocumentDirectory = "D:\\"+Serie + "-" + Numero + " " + dtConductor.Rows[0]["Apellidos_Conductor"].ToString() + " " + dtConductor.Rows[0]["Nombres_Conductor"].ToString();
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            /*saveFileDialog1.FileName = 
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "PDF files (.pdf)|.pdf|All files (.)|.";*/
            //saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //open.pdfViewer1.SaveDocument(saveFileDialog1.FileName);
            open.ShowDialog();
        }

        private void AbrirPDFRemitente(ServiceGRR_QA.ens_ResultadoRI resultadoRI)
        {
            Stream stream = new MemoryStream(resultadoRI.ent_Resultado.at_ArchivoRI);
            string Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
            string Numero = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia")).ToString("D8");

            AbrirPdf open = new AbrirPdf();
            open.pdfViewer1.LoadDocument(stream);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = Serie + "-" + Numero;
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "PDF files (.pdf)|.pdf|All files (.)|.";
            //saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            open.ShowDialog();
        }



        private void CargarGuiaEnXML()
        {
            ServiceGRT_QA.ene_ConsultarXML consultarXML = new ServiceGRT_QA.ene_ConsultarXML();
            consultarXML.ent_ComprobanteConsultarXML = new ServiceGRT_QA.en_ComprobanteConsultarXML();
            consultarXML.ent_ComprobanteConsultarXML.at_Serie = cbxSerieFiltro.Text;
            consultarXML.ent_ComprobanteConsultarXML.at_Numero = Convert.ToInt32(txtNumeroFiltro.Text);
            ServiceGRT_QA.ens_ConsultarXML responseXML = new ServiceGRT_QA.ens_ConsultarXML();
            //responseXML = requestTransportista.ConsultarXMLGRT(consultarXML);
            entGuiaTransportista.entGRT_Respuesta_XML_Archivo = Encoding.UTF8.GetString(responseXML.ent_ResultadoXML.at_XML);
        }

        private DataTable obtenerListaRespuestaSunat(ServiceGRT_QA.ArrayOfEn_Respuestas lista)
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta.Columns.Add("NroRespuesta", typeof(String));
            dtRespuesta.Columns.Add("CodigoRespuesta", typeof(String));
            dtRespuesta.Columns.Add("Descripcion", typeof(String));
            dtRespuesta.Columns.Add("FechaSunat", typeof(String));

            if (lista.Count > 0)
            {
                foreach (ServiceGRT_QA.en_Respuestas item in lista)
                { dtRespuesta.Rows.Add(item.at_NroRespuesta, item.at_CodigoRespuesta, item.at_Descripcion, item.at_FechaSunat); }
            }
            else { dtRespuesta = null; }

            return dtRespuesta;
        }




        private void cbxSerieFiltro_SelectedValueChanged(object sender, EventArgs e)
        {


        }

        private void cbxSerieFiltro_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = true; }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString() == "T" && dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString() != ("REVERSION"))
                {
                    FrmGuiaElectronicaTransportista open = new FrmGuiaElectronicaTransportista();
                    open.entGuiaTransportista.compania = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo").ToString();
                    open.entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                    open.entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT").ToString());
                    open.entGuiaTransportista.TipoGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                    open.entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica").ToString());
                    open.entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "LineaOT").ToString());
                    open.entGuiaTransportista.entGRT_Generales_Serie_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                    open.entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                    open.entGuiaTransportista.idGuiaSpring = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaSpring").ToString());
                    open.entGuiaTransportista.idviaje = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idViaje"));
                    open.entGuiaTransportista.viaje = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje").ToString();
                    open.entGuiaTransportista.Cliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Cliente").ToString();
                    open.entGuiaTransportista.idRemitente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRemitente"));
                    open.entGuiaTransportista.entGRT_Generales_FechaEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaEmision").ToString();
                    open.entGuiaTransportista.entGRT_Generales_HoraEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "HoraEmision").ToString();
                    open.entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                    open.entGuiaTransportista.entGRT_Generales_Observacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "ObservacionGuia").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_NombreComercial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreComercial_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_NumeroMTC = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroMTC_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_Telefono = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Telefono_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_CorreoContacto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Correo_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_SitioWeb = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SitioWeb_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ubigeo_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_DireccionDetallada = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionDetallada_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_Provincia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Provincia_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_Departamento = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Departamento_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_Distrito = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Distrito_Emisor").ToString();
                    open.entGuiaTransportista.entGRT_Emisor_CodigoPais_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodigoPais_Emisor").ToString();
                    open.entGuiaTransportista.correoCliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CorreoPrincipal_Cliente").ToString();
                    open.entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Rem").ToString();
                    open.entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Rem").ToString();
                    open.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Rem").ToString();
                    open.entGuiaTransportista.entGRT_Destinatario_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Dest").ToString();
                    open.entGuiaTransportista.entGRT_Destinatario_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Dest").ToString();
                    open.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Dest").ToString();
                    open.entGuiaTransportista.entGRT_Contratista_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Contra").ToString();
                    open.entGuiaTransportista.entGRT_Contratista_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Contra").ToString();
                    open.entGuiaTransportista.entGRT_Contratista_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Contra").ToString();
                    open.entGuiaTransportista.entGRT_SubContratista_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Subcontra").ToString();
                    open.entGuiaTransportista.entGRT_SubContratista_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Subcontra").ToString();
                    open.entGuiaTransportista.entGRT_SubContratista_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Subcontra").ToString();
                    open.entGuiaTransportista.idDocumentoRelacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idDocumentosRelacion"));
                    open.entGuiaTransportista.idIndicadoresServicio = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idIndicadoresServicio"));
                    open.entGuiaTransportista.xml_entGRT_TipoServicio = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SERVICIOS").ToString();
                    open.entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                    open.entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M = Convert.ToDecimal(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "PesoBruto"));
                    open.entGuiaTransportista.entGRT_PesoBruto_CodigoUnidadMedida_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodUnidadMedida_Peso").ToString();
                    open.entGuiaTransportista.entGRT_PesoBruto_DescripcionAdicional = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DescripcionAdicional_Peso").ToString();
                    open.entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoPartida").ToString();
                    open.entGuiaTransportista.entGRT_PuntoPartida_Nombre_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreUbigeoPartida").ToString();
                    open.entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoPartida").ToString();
                    open.entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoLlegada").ToString();
                    open.entGuiaTransportista.entGRT_PuntoDestino_Nombre_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreUbigeoLlegada").ToString();
                    open.entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoLlegada").ToString();
                    open.entGuiaTransportista.idRuta = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRuta"));
                    open.entGuiaTransportista.ruta = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta").ToString();
                   
                    open.entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroPlaca").ToString();
                    open.entGuiaTransportista.idtracto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdVehiculo").ToString();
                    open.entGuiaTransportista.carreta = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Carreta").ToString();
                    open.entGuiaTransportista.idCarreta = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCarreta").ToString();
                    open.entGuiaTransportista.idConductoresGuia = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idConductoresGuia"));
                    open.entGuiaTransportista.idProductosTraslado = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProductosTraslado"));
                    open.entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString();
                    open.entGuiaTransportista.entGRT_Respuesta_EstadoSunat = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString();
                    open.entGuiaTransportista.impreso = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Impreso"));
                    open.entGuiaTransportista.idProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProgramacion") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProgramacion"));
                    open.entGuiaTransportista.AnioProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "AnioProgramacion") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "AnioProgramacion"));
                    open.entGuiaTransportista.CodigoProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion").ToString() == "" ? "" : Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion"));
                    open.entGuiaTransportista.idTipoProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idTipoProgramacion") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idTipoProgramacion"));
                    open.entGuiaTransportista.TipoViaje = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoViaje").ToString();
                    open.entGuiaTransportista.xml_entGRT_DocumentosRelacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_DocumentosRelacion").ToString();
                    open.entGuiaTransportista.xml_entGTR_Conductor_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Conductores").ToString();
                    open.entGuiaTransportista.xml_entGRT_Productos_Bienes = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Productos").ToString();

                    open.entGuiaTransportista.TipoOperacion = Utilitario.TipoOperacion.Editar;
                    open.btnGuardar.Text = "Actualizar";

                    if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Cliente").ToString() == "LIMA GAS S A")
                    {
                        open.txtEmpresaDestinatario.Enabled = false;
                        open.txtEmpresaRemitente.Enabled = false;
                    }

                    if (Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idTipoProgramacion")) == 1) // tolvas
                    { open.TipoProgramacion = "TOLVAS"; }

                    if (open.entGuiaTransportista.idTipoProgramacion == 2)
                    { open.TipoProgramacion = "LINDLEY"; }

                    if (open.entGuiaTransportista.idTipoProgramacion == 4)
                    { open.TipoProgramacion = "GENERAL"; }

                    if (open.entGuiaTransportista.idTipoProgramacion == 9)
                    { open.TipoProgramacion = "LOCAL"; }

                    if (open.entGuiaTransportista.idTipoProgramacion == 10)
                    { open.TipoProgramacion = "VOLCAN"; }

                    if (open.entGuiaTransportista.idTipoProgramacion == 11)
                    { open.TipoProgramacion = "SOLGAS"; }

                    if (open.entGuiaTransportista.idTipoProgramacion == 13)
                    { open.TipoProgramacion = "SOLGAS GNL"; }

                    if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString().Equals("APROBADO") || dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString().Equals("ANULADO"))
                    {
                        open.btnGuardar.Enabled = false;
                    }

                    open.TipoOperacion = Utilitario.TipoOperacion.Editar;
                    open.CargarListaTolvas += new FrmGuiaElectronicaTransportista.CargarListaTolvasEventHandler(CargarRespuesta);
                    open.Show();

                }
                else if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString() == "T")
                {
                    MessageBox.Show("Guia se encuentra en estado reversion no es posible editar", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString() == "R" && dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString() != ("REVERSION"))
                {

                    FrmGuiaElectronicaRemitente openRemitente = new FrmGuiaElectronicaRemitente();
                    openRemitente.entGuiaRemitente.compania = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo").ToString();
                    openRemitente.entGuiaRemitente.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));

                    openRemitente.entGuiaRemitente.TipoGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                    openRemitente.entGuiaRemitente.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica").ToString());
                    openRemitente.entGuiaRemitente.entGRR_Generales_Serie_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                    openRemitente.entGuiaRemitente.Cliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Cliente").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_FechaEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaEmision").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_HoraEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "HoraEmision").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_FechaIncioTraslado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_Observacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "ObservacionGuia").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_NombreComercial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreComercial_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_NumeroMTC = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroMTC_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_Telefono = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Telefono_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_CorreoContacto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Correo_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_SitioWeb = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SitioWeb_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ubigeo_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_DireccionDetallada = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionDetallada_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_Provincia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Provincia_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_Departamento = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Departamento_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_Distrito = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Distrito_Emisor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Emisor_CodigoPais_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodigoPais_Emisor").ToString();
                    openRemitente.entGuiaRemitente.correoCliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CorreoPrincipal_Cliente").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Rem").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Rem").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Remitente_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Rem").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Dest").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Dest").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Destinatario_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Dest").ToString();
                    openRemitente.entGuiaRemitente.idDocumentoRelacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idDocumentoRelacion"));
                    openRemitente.entGuiaRemitente.idIndicadoresServicio = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idIndicadoresServicio"));
                    openRemitente.entGuiaRemitente.xml_entGRR_TipoServicio = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SERVICIOS").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_FechaIncioTraslado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "PesoBruto"));
                    openRemitente.entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodUnidadMedida_Peso").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PesoBruto_DescripcionAdicional = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DescripcionAdicional_Peso").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PuntoPartida_NombreUbigeo = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreUbigeoPartida").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoPartida").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoPartida").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PuntoDestino_NombreUbigeo = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreUbigeoLlegada").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoLlegada").ToString();
                    openRemitente.entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoLlegada").ToString();

                    openRemitente.entGuiaRemitente.CodigoEstablecimientoOrigen = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodEstableOrigen").ToString();
                    openRemitente.entGuiaRemitente.NombreEstablecimientoOrigen = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreEstableOrigen").ToString();
                    openRemitente.entGuiaRemitente.CodigoEstablecimientoDestino = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodEstableDestino").ToString();
                    openRemitente.entGuiaRemitente.NombreEstablecimientoDestino = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreEstableDestino").ToString();

                    openRemitente.entGuiaRemitente.entGRR_Vehiculo_NumeroPlaca_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroPlaca").ToString();
                    openRemitente.entGuiaRemitente.idtracto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdVehiculo").ToString();
                    openRemitente.entGuiaRemitente.idConductoresGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idConductoresGuia") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idConductoresGuia"));
                    openRemitente.entGuiaRemitente.idProductosTraslado = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProductosTraslado"));
                    openRemitente.entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Respuesta_EstadoSunat = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString();
                    openRemitente.entGuiaRemitente.impreso = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Impreso"));

                    openRemitente.entGuiaRemitente.entGRR_Proveedor_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Proveedor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Proveedor_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Proveedor").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Proveedor_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Proveedor").ToString();

                    openRemitente.entGuiaRemitente.entGRR_Transportista_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Trans").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Transportista_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Trans").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Transportista_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Proveedor_Trans").ToString();

                    openRemitente.entGuiaRemitente.entGRR_Generales_CodigoMotivo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodMotivo").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_Modalidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodModalidad").ToString();
                    openRemitente.entGuiaRemitente.entGRR_Generales_DescripcionMotivo = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "MotivoTraslado").ToString();


                    openRemitente.entGuiaRemitente.TipoViaje = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoViaje").ToString();
                    openRemitente.entGuiaRemitente.xml_entGRR_DocumentosRelacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_DocumentosRelacion").ToString();
                    openRemitente.entGuiaRemitente.xml_entGRR_Conductor_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Conductores").ToString();
                    openRemitente.entGuiaRemitente.xml_entGRR_Productos_Bienes = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Productos").ToString();
                    openRemitente.entGuiaRemitente.TipoOperacion = Utilitario.TipoOperacion.Editar;
                    openRemitente.btnGuardar.Text = "Enviar";

                    if (!dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString().Equals("REVERSION") && !dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString().Equals("APROBADO"))
                    {
                        openRemitente.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        openRemitente.entGuiaRemitente.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        openRemitente.CargarListaRemitente += new FrmGuiaElectronicaRemitente.CargarListaRemitenteEventHandler(CargarListaRemitente);
                        openRemitente.Show();
                    }
                    else { MessageBox.Show("Estado de guia no cumple con las condiciones.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                }
                else if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString() == "R")
                { MessageBox.Show("Guia se encuentra en estado reversion no es posible editar", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void reversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxTipoGuia.SelectedValue.ToString() == "T")
                {
                    esReversion = true;
                    ReversionGuiaTransportista();

                    /*
                    if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString() == "APROBADO")
                    {
                        esReversion = true;
                        ReversionGuiaTransportista();
                    }
                    else { MessageBox.Show("El estado de la guia tiene que estar aprobada para generar Reversion", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    */
                }

                if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString() == "RECHAZADO" && dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString() == "RECHAZADO") 
                {
                    esReversion = true;
                    ReversionGuiaTransportista();
                }

                if (cbxTipoGuia.SelectedValue.ToString() == "R")
                {
                    esReversion = true;
                    ReversionGuiaRemitente();

                    /*
                    if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString() == "APROBADO")
                    {
                        esReversion = true;
                        ReversionGuiaRemitente();
                    }
                    else { MessageBox.Show("El estado de la guia tiene que estar aprobada para generar Reversion", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    */ 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ReversionGuiaRemitente()
        {
            ServiceGRR_QA_Reversion.ServicioReversionesClient requestRE = new ServiceGRR_QA_Reversion.ServicioReversionesClient();

            String Respuesta = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Motivo", "Motivo Reversion");
            if (Respuesta.Length > 100)
            {
                MessageBox.Show("Usted a ingresado demaciado caracteres,", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Respuesta.Length > 0)
            {
                entGuiaRemitente.MotivoReversion = Respuesta;
                dtFechaHora = clsOperacionesBL.Instancia.ObtenerFechaHoraServidorGuiaElectronica();
                entGuiaRemitente.compania = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo").ToString();
                entGuiaRemitente.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente").ToString());
                entGuiaRemitente.TipoGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                entGuiaRemitente.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica").ToString());
                entGuiaRemitente.entGRR_Generales_Serie_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                entGuiaRemitente.entGRR_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia").ToString());
                entGuiaRemitente.entGRR_Emisor_TipoComprobante = cbxTipoGuia.SelectedValue.ToString().TrimEnd() == "T" ? "31" : "09";
                entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor").ToString();
                entGuiaRemitente.entGRR_Emisor_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Emisor").ToString();
                entGuiaRemitente.entGRR_Generales_FechaEmision_M = Convert.ToDateTime(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaEmision")).ToString("yyyy-MM-dd");
                entGuiaTransportista.LineaOT = 0;

                //Correlativo del codigo Unico de la Reversion

                entGuiaRemitente.CodReversion = ("RV-" + dtFechaHora.Rows[0]["FechaServidor"].ToString() + " " + dtFechaHora.Rows[0]["HoraServidor"].ToString() + "-" + entGuiaRemitente.compania + "-" + entGuiaRemitente.TipoGuia + "-" + entGuiaRemitente.entGRR_Generales_Serie_M).Replace(":", "-");

                ent_ResumenReversionRE = new ReportesTranspesa.ServiceGRR_QA_Reversion.ene_ResumenReversion();
                ent_ResumenReversionRE.ent_Emisor = new ReportesTranspesa.ServiceGRR_QA_Reversion.en_Emisor();
                ent_ResumenReversionRE.ent_Emisor.at_NumeroDocumentoIdentidad = entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M;
                ent_ResumenReversionRE.ent_Emisor.at_RazonSocial = entGuiaRemitente.entGRR_Emisor_RazonSocial_M;


                ent_ResumenReversionRE.ent_DatoResumenReversion = new ReportesTranspesa.ServiceGRR_QA_Reversion.en_DatoResumenReversion();
                ent_ResumenReversionRE.ent_DatoResumenReversion.ent_CabeceraResumenReversion = new ReportesTranspesa.ServiceGRR_QA_Reversion.en_CabeceraResumenReversion();
                ent_ResumenReversionRE.ent_DatoResumenReversion.ent_CabeceraResumenReversion.at_FechaComprobante = entGuiaRemitente.entGRR_Generales_FechaEmision_M;
                ent_ResumenReversionRE.ent_DatoResumenReversion.ent_CabeceraResumenReversion.at_FechaGeneracion = dtFechaHora.Rows[0]["FechaServidor"].ToString();
                ent_ResumenReversionRE.ent_DatoResumenReversion.ent_CabeceraResumenReversion.at_IdentificadorUnico = entGuiaRemitente.CodReversion.Replace(":", "-");

                ent_ComprobantesRevertidosRE = new ReportesTranspesa.ServiceGRR_QA_Reversion.en_ComprobantesRevertidos();
                ent_ComprobantesRevertidosRE.at_TipoComprobante = entGuiaRemitente.entGRR_Emisor_TipoComprobante;
                ent_ComprobantesRevertidosRE.at_Serie = entGuiaRemitente.entGRR_Generales_Serie_M;
                ent_ComprobantesRevertidosRE.at_Numero = entGuiaRemitente.entGRR_Generales_Numero_M;
                ent_ComprobantesRevertidosRE.at_MotivoReversion = entGuiaRemitente.MotivoReversion;


                ent_ResumenReversionRE.ent_DatoResumenReversion.ent_CabeceraResumenReversion.l_ComprobantesRevertidos = new ReportesTranspesa.ServiceGRR_QA_Reversion.ArrayOfEn_ComprobantesRevertidos();
                ent_ResumenReversionRE.ent_DatoResumenReversion.ent_CabeceraResumenReversion.l_ComprobantesRevertidos.Add(ent_ComprobantesRevertidosRE);
                responseRE = requestRE.RegistrarResumenReversion(ent_ResumenReversionRE);

                //SerializarSOAP_Remision(ent_ResumenReversionRE);

                if (responseRE.at_NivelResultado)
                {
                    entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "APROBADO";
                    entGuiaRemitente.entGRR_Respuesta_MensajeResultado = responseRE.at_MensajeResultado;
                    entGuiaRemitente.entGRR_Respuesta_CodigoHash = responseRE.at_CodigoHash;

                    ConsultarGuiaIndividualRemitente(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M);
                    
                    if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "2" || consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "1")
                    { entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "APROBADO"; }
                    else
                    { entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "RECHAZADO"; }

                    CargarArchivoXMLRemitente();
                    CargarRespuestaSunatCDRRemitente();
                    entGuiaRemitente.entGRR_Respuesta_FechaGeneracion = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaGeneracion;
                    entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaOtorgamiento;
                    entGuiaRemitente.entGRR_Respuesta_FechaTransmision = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaTransmision;
                    entGuiaRemitente.entGRR_Respuesta_FechaReversion = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaReversion;
                    entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta;

                    if (clsOperacionesBL.Instancia.ReportesApp_GuardarReversionRemitente(entGuiaRemitente))
                    {
                        posicionFila = dgvListaGuiaTraspExpressVista.FocusedRowHandle;
                        posicionColumna = dgvListaGuiaTraspExpressVista.FocusedColumn.AbsoluteIndex;

                        MessageBox.Show("SUNAT: " + responseRE.at_MensajeResultado + "\n SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarGuiasElectronicas();

                        dgvListaGuiaTraspExpressVista.FocusedColumn.ColumnHandle = posicionColumna;
                        dgvListaGuiaTraspExpressVista.FocusedRowHandle = posicionFila;
                        dgvListaGuiaTraspExpressVista.TopRowIndex = posicionFila;
                    }
                    else
                    {
                        posicionFila = dgvListaGuiaTraspExpressVista.FocusedRowHandle;
                        posicionColumna = dgvListaGuiaTraspExpressVista.FocusedColumn.AbsoluteIndex;

                        MessageBox.Show("SUNAT: " + responseRE.at_MensajeResultado + "\n SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ListarGuiasElectronicas();

                        dgvListaGuiaTraspExpressVista.FocusedColumn.ColumnHandle = posicionColumna;
                        dgvListaGuiaTraspExpressVista.FocusedRowHandle = posicionFila;
                        dgvListaGuiaTraspExpressVista.TopRowIndex = posicionFila;
                    }
                }
                else
                {
                    MessageBox.Show(responseRE.at_MensajeResultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListarGuiasElectronicas();
                }
            }
        }

        private void ReversionGuiaTransportista()
        {
            ServiceGRT_QA_Reversion.ServicioReversionesClient request = new ReportesTranspesa.ServiceGRT_QA_Reversion.ServicioReversionesClient();

            String Respuesta = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Motivo", "Motivo Reversion");
            if (Respuesta.Length > 100)
            {
                MessageBox.Show("Usted a ingresado demaciado caracteres,", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Respuesta.Length > 0)
            {
                entGuiaTransportista.MotivoReversion = Respuesta;
                dtFechaHora = clsOperacionesBL.Instancia.ObtenerFechaHoraServidorGuiaElectronica();
                entGuiaTransportista.compania = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo").ToString();
                entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente").ToString());
                entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT").ToString());
                entGuiaTransportista.TipoGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica").ToString());
                entGuiaTransportista.entGRT_Generales_Serie_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia").ToString());
                entGuiaTransportista.entGRT_Emisor_TipoComprobante = cbxTipoGuia.SelectedValue.ToString().TrimEnd() == "T" ? "31" : "09";
                entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor").ToString();
                entGuiaTransportista.entGRT_Emisor_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Emisor").ToString();
                entGuiaTransportista.entGRT_Generales_FechaEmision_M = Convert.ToDateTime(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaEmision")).ToString("yyyy-MM-dd");
                entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "LineaOT").ToString());
                //Correlativo del codigo Unico de la Reversion

                entGuiaTransportista.CodReversion = ("RV-" + dtFechaHora.Rows[0]["FechaServidor"].ToString() + " " + dtFechaHora.Rows[0]["HoraServidor"].ToString() + "-" + entGuiaTransportista.compania + "-" + entGuiaTransportista.TipoGuia + "-" + entGuiaTransportista.entGRT_Generales_Serie_M).Replace(":", "-");

                ent_ResumenReversion = new ReportesTranspesa.ServiceGRT_QA_Reversion.ene_ResumenReversion();
                ent_ResumenReversion.ent_Emisor = new ReportesTranspesa.ServiceGRT_QA_Reversion.en_Emisor();
                ent_ResumenReversion.ent_Emisor.at_NumeroDocumentoIdentidad = entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M;
                ent_ResumenReversion.ent_Emisor.at_RazonSocial = entGuiaTransportista.entGRT_Emisor_RazonSocial_M;

                ent_ResumenReversion.ent_DatoResumenReversion = new ReportesTranspesa.ServiceGRT_QA_Reversion.en_DatoResumenReversion();
                ent_ResumenReversion.ent_DatoResumenReversion.ent_CabeceraResumenReversion = new ReportesTranspesa.ServiceGRT_QA_Reversion.en_CabeceraResumenReversion();
                ent_ResumenReversion.ent_DatoResumenReversion.ent_CabeceraResumenReversion.at_FechaComprobante = entGuiaTransportista.entGRT_Generales_FechaEmision_M;
                ent_ResumenReversion.ent_DatoResumenReversion.ent_CabeceraResumenReversion.at_FechaGeneracion = dtFechaHora.Rows[0]["FechaServidor"].ToString();
                ent_ResumenReversion.ent_DatoResumenReversion.ent_CabeceraResumenReversion.at_IdentificadorUnico = entGuiaTransportista.CodReversion.Replace(":", "-");

                ent_ComprobantesRevertidos = new ReportesTranspesa.ServiceGRT_QA_Reversion.en_ComprobantesRevertidos();
                ent_ComprobantesRevertidos.at_TipoComprobante = entGuiaTransportista.entGRT_Emisor_TipoComprobante;
                ent_ComprobantesRevertidos.at_Serie = entGuiaTransportista.entGRT_Generales_Serie_M;
                ent_ComprobantesRevertidos.at_Numero = entGuiaTransportista.entGRT_Generales_Numero_M;
                ent_ComprobantesRevertidos.at_MotivoReversion = entGuiaTransportista.MotivoReversion;

                ent_ResumenReversion.ent_DatoResumenReversion.ent_CabeceraResumenReversion.l_ComprobantesRevertidos = new ReportesTranspesa.ServiceGRT_QA_Reversion.ArrayOfEn_ComprobantesRevertidos();
                ent_ResumenReversion.ent_DatoResumenReversion.ent_CabeceraResumenReversion.l_ComprobantesRevertidos.Add(ent_ComprobantesRevertidos);
                this.Cursor = Cursors.WaitCursor;
                response = request.RegistrarResumenReversion(ent_ResumenReversion);

                //SerializarSOAP_Transportista(ent_ResumenReversion);

                if (response.at_NivelResultado) //true: si se generó reversion -- false: no se genero reversion
                {
                    entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "APROBADO";
                    entGuiaTransportista.entGRT_Respuesta_MensajeResultado = response.at_MensajeResultado;
                    entGuiaTransportista.entGRT_Respuesta_CodigoHash = response.at_CodigoHash;

                    // GENERA EL CODIGO PARA GUARDAR RESPUESTA SUNAT TRAZABILIDAD
                    ConsultarGuiaIndividualTransportista(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);
                    if (consultaIndividual.ent_InformacionComprobante != null)
                    {
                        if (consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "2" || consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "1")
                        { entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "APROBADO"; }
                        else
                        { entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "RECHAZADO"; }

                        CargarArchivoXML();
                        CargarRespuestaSunatCDR();

                        entGuiaTransportista.entGRT_Respuesta_FechaGeneracion = consultaIndividual.ent_InformacionComprobante.at_FechaGeneracion;
                        entGuiaTransportista.entGRT_Respuesta_FechaOtorgamiento = consultaIndividual.ent_InformacionComprobante.at_FechaOtorgamiento;
                        entGuiaTransportista.entGRT_Respuesta_FechaTransmision = consultaIndividual.ent_InformacionComprobante.at_FechaTransmision;
                        entGuiaTransportista.entGRT_Respuesta_FechaReversion = consultaIndividual.ent_InformacionComprobante.at_FechaReversion;
                        entGuiaTransportista.entGRT_Respuesta_CodigoMensaje = consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta;

                        entGuiaTransportista.entGRT_Respuesta_CodigoHash = consultaIndividual.ent_InformacionComprobante.at_CodigoHash;
                        entGuiaTransportista.compania = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                        entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                        entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                        entGuiaTransportista.TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                        entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));
                        entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";

                        if (clsOperacionesBL.Instancia.ReportesApp_GuardarReversion(entGuiaTransportista))
                        {
                            posicionFila = dgvListaGuiaTraspExpressVista.FocusedRowHandle;
                            posicionColumna = dgvListaGuiaTraspExpressVista.FocusedColumn.AbsoluteIndex;

                            MessageBox.Show("SUNAT: " + response.at_MensajeResultado + "\n SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarGuiasElectronicas();

                            dgvListaGuiaTraspExpressVista.FocusedColumn.ColumnHandle = posicionColumna;
                            dgvListaGuiaTraspExpressVista.FocusedRowHandle = posicionFila;
                            dgvListaGuiaTraspExpressVista.TopRowIndex = posicionFila;
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            posicionFila = dgvListaGuiaTraspExpressVista.FocusedRowHandle;
                            posicionColumna = dgvListaGuiaTraspExpressVista.FocusedColumn.AbsoluteIndex;

                            MessageBox.Show("SUNAT: " + response.at_MensajeResultado + "\n SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ListarGuiasElectronicas();

                            dgvListaGuiaTraspExpressVista.FocusedColumn.ColumnHandle = posicionColumna;
                            dgvListaGuiaTraspExpressVista.FocusedRowHandle = posicionFila;
                            dgvListaGuiaTraspExpressVista.TopRowIndex = posicionFila;
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        MessageBox.Show(response.at_MensajeResultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        ListarGuiasElectronicas();
                    }
                }
                else
                {
                    if (response.at_CodigoError == 953) //es cuando ya existe reversion
                    {
                        entGuiaTransportista.entGRT_Respuesta_MensajeResultado = response.at_MensajeResultado;
                        entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "REVERSION";
                        entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "APROBADO";

                        if (clsOperacionesBL.Instancia.ReportesApp_GuardarReversion(entGuiaTransportista))
                        {
                            MessageBox.Show("TCI: " + response.at_MensajeResultado + ", SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Cursor = Cursors.Default;
                            ListarGuiasElectronicas();

                        }
                        else
                        {
                            MessageBox.Show("TCI: " + response.at_MensajeResultado + ", SQL: " + Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            ListarGuiasElectronicas();
                        }
                    }
                    else
                    {
                        MessageBox.Show("TCI: " + response.at_MensajeResultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        ListarGuiasElectronicas();
                    }
                }
            }
        }

        private void SerializarSOAP_Transportista(ReportesTranspesa.ServiceGRT_QA_Reversion.ene_ResumenReversion registrar)
        {
            var path = CarpetaLogSoapError + "Reversion-" + entGuiaTransportista.CodReversion.Replace(":", "-");
            System.IO.FileStream file = System.IO.File.Create(path);
            XmlSerializer serializador = new XmlSerializer(typeof(ReportesTranspesa.ServiceGRT_QA_Reversion.ene_ResumenReversion));
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            serializador.Serialize(file, registrar);
            file.Close();
        }

        private void SerializarSOAP_Remision(ReportesTranspesa.ServiceGRR_QA_Reversion.ene_ResumenReversion registrar)
        {
            var path = CarpetaLogSoapError + "Reversion-" + entGuiaRemitente.CodReversion.Replace(":", "-");
            System.IO.FileStream file = System.IO.File.Create(path);
            XmlSerializer serializador = new XmlSerializer(typeof(ReportesTranspesa.ServiceGRR_QA_Reversion.ene_ResumenReversion));
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            serializador.Serialize(file, registrar);
            file.Close();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmGuiaElectronicaRemitente open = new FrmGuiaElectronicaRemitente();
                open.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                open.entGuiaRemitente.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                open.CargarListaRemitente += new FrmGuiaElectronicaRemitente.CargarListaRemitenteEventHandler(CargarListaRemitente);
                open.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void verificarLeidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { VerificarOtoradoLeidoCorreo(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void VerificarOtoradoLeidoCorreo()
        {
            if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "R")
            {
                String EstadoLeido = "";

                // ********* CONSULTAR MASIVO ESTADO GUIAS **********
                ServiceGRR_QA.ene_ConsultarEstado consultarEstado = new ServiceGRR_QA.ene_ConsultarEstado();
                consultarEstado.at_CantidadConsultar = 999;
                consultarEstado.at_NumeroDocumentoIdentidad = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor")); ;

                // ********** CONFIRMAR ESTADO ***********
                ServiceGRR_QA.ene_ConfirmarEstado confirmarEstado = new ServiceGRR_QA.ene_ConfirmarEstado();
                confirmarEstado.at_NumeroDocumentoIdentidad = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor")); ;

                // ************ CONFIRMAR GUIA ESPECIFICA ******************
                ServiceGRR_QA.en_ComprobanteConfirmarEstado comprobanteEstado = new ServiceGRR_QA.en_ComprobanteConfirmarEstado();
                comprobanteEstado.at_Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
                comprobanteEstado.at_Numero = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));

                confirmarEstado.l_Comprobante = new ServiceGRR_QA.ArrayOfEn_ComprobanteConfirmarEstado();
                confirmarEstado.l_Comprobante.Add(comprobanteEstado);

                // TRAER RESPUESTA DE CONSLTA (DATOS DE GUIA)
                requestRemitente = new ServiceGRR_QA.ServicioGuiaRemisionRemitenteClient();
                ServiceGRR_QA.ens_ConsultarEstadoGR responseConsulta = new ServiceGRR_QA.ens_ConsultarEstadoGR();
                responseConsulta = requestRemitente.ConsultarEstadoGRR(consultarEstado);

                // APLICAR CONFIRMACION
                ServiceGRR_QA.ens_ConfirmarEstado response = requestRemitente.ConfirmarEstadoGRR(confirmarEstado);

                if (responseConsulta.at_NivelResultado > 0)
                {
                    if (responseConsulta.l_ResultadoEstadoComprobante[0].ent_EstadoLeido.at_FechaLeido.ToString() == "")
                    { EstadoLeido = "Guia Aun no Leida por el Destinatario"; }
                    else
                    { EstadoLeido = responseConsulta.l_ResultadoEstadoComprobante[0].ent_EstadoLeido.at_FechaLeido.ToString(); }

                    MessageBox.Show("La Guia " + comprobanteEstado.at_Serie.ToString() + "-" + comprobanteEstado.at_Numero.ToString() + " Fue enviada Correctamente al correo del Destinatario el dia: " + responseConsulta.l_ResultadoEstadoComprobante[0].ent_EstadoOtorgado.at_FechaOtorgado.ToString() + " | " + EstadoLeido, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (responseConsulta.at_NivelResultado == 0) { MessageBox.Show("La Guia ya fue consultada"); }
                else if (responseConsulta.at_NivelResultado < 0) { MessageBox.Show("Error en consulta"); }
            }

            if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "T")
            {
                String EstadoLeido = "";

                // ********* CONSULTAR MASIVO ESTADO GUIAS **********

                ServiceGRT_QA.ene_ConsultarEstado consultarEstado = new ServiceGRT_QA.ene_ConsultarEstado();
                consultarEstado.at_CantidadConsultar = 999;
                consultarEstado.at_NumeroDocumentoIdentidad = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor"));

                // ********** CONFIRMAR ESTADO ***********

                ServiceGRT_QA.ene_ConfirmarEstado confirmarEstado = new ServiceGRT_QA.ene_ConfirmarEstado();
                confirmarEstado.at_NumeroDocumentoIdentidad = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor"));

                // ************ CONFIRMAR GUIA ESPECIFICA ******************
                ServiceGRT_QA.en_ComprobanteConfirmarEstado comprobanteEstado = new ServiceGRT_QA.en_ComprobanteConfirmarEstado();
                comprobanteEstado.at_Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
                comprobanteEstado.at_Numero = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));

                confirmarEstado.l_Comprobante = new ServiceGRT_QA.ArrayOfEn_ComprobanteConfirmarEstado();
                confirmarEstado.l_Comprobante.Add(comprobanteEstado);

                // TRAER RESPUESTA DE CONSLTA (DATOS DE GUIA)
                requestTransportista = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
                ServiceGRT_QA.ens_ConsultarEstadoGR responseConsulta = new ServiceGRT_QA.ens_ConsultarEstadoGR();
                responseConsulta = requestTransportista.ConsultarEstadoGRT(consultarEstado);

                ServiceGRT_QA.ens_ConfirmarEstado response = requestTransportista.ConfirmarEstadoGRT(confirmarEstado);

                if (responseConsulta.at_NivelResultado > 0)
                {
                    if (responseConsulta.l_ResultadoEstadoComprobante[0].ent_EstadoLeido.at_FechaLeido.ToString() == "")
                    { EstadoLeido = "Guia Aun no Leida por el Destinatario"; }
                    else
                    { EstadoLeido = responseConsulta.l_ResultadoEstadoComprobante[0].ent_EstadoLeido.at_FechaLeido.ToString(); }

                    MessageBox.Show("La Guia " + comprobanteEstado.at_Serie.ToString() + "-" + comprobanteEstado.at_Numero.ToString() + " Fue enviada Correctaemnte al correo del Destinatario el dia: " + responseConsulta.l_ResultadoEstadoComprobante[0].ent_EstadoOtorgado.at_FechaOtorgado.ToString() + " | " + EstadoLeido, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (responseConsulta.at_NivelResultado == 0) { MessageBox.Show("La Guia ya fue consultada"); }
                else if (responseConsulta.at_NivelResultado < 0) { MessageBox.Show("Error en consulta"); }
            }
        }

        private void cbxTipoGuia_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                CargarSeries();

                if (cbxTipoGuia.SelectedValue.ToString() == "T") { g_viaje.Visible = true; }
                else { g_viaje.Visible = false; }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void actualizarEstadoSunatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            esReversion = false;

            try
            {
                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "T")
                {
                    ActualizarEstadoSunatTransportista();
                    ListarGuiasElectronicas();
                }
                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "R")
                {
                    ActualizarEstadoSunatRemitente();
                    ListarGuiasElectronicas();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ActualizarEstadoSunatRemitente()
        {
            entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor"));
            entGuiaRemitente.entGRR_Generales_Serie_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
            entGuiaRemitente.entGRR_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));

            this.Cursor = Cursors.WaitCursor;
            ConsultarGuiaIndividualRemitente(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M);

            if (consultaIndividualRemitente.ent_InformacionComprobante != null && consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas.Count > 0)
            {
                if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas.Count == 0)
                {
                    MessageBox.Show("Problemas con el servicio de SUNAT" + consultaIndividual.at_MensajeResultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "2" || consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "1")
                { entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "APROBADO"; }
                else
                { entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "RECHAZADO"; }

                CargarArchivoXMLRemitente();
                CargarRespuestaSunatCDRRemitente();
                entGuiaRemitente.entGRR_Respuesta_MensajeResultado = consultaIndividualRemitente.at_MensajeResultado;
                entGuiaRemitente.entGRR_Respuesta_FechaGeneracion = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaGeneracion;
                entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaOtorgamiento;
                entGuiaRemitente.entGRR_Respuesta_FechaTransmision = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaTransmision;
                entGuiaRemitente.entGRR_Respuesta_FechaReversion = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaReversion;
                entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta;

                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia")) == "REVERSION")
                { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "REVERSION"; }
                else
                {
                    if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta != "3")
                    {
                        if (consultaIndividualRemitente.ent_InformacionComprobante.at_FechaReversion == "")
                        { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "ACEPTADO"; }
                        else
                        { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "REVERSION"; }
                    }
                    else if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "3")
                    { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "RECHAZADO"; }
                }

                entGuiaRemitente.entGRR_Respuesta_CodigoHash = consultaIndividualRemitente.ent_InformacionComprobante.at_CodigoHash;
                entGuiaRemitente.compania = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                entGuiaRemitente.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                entGuiaRemitente.TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                entGuiaRemitente.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));
                entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";

                if (clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaRemitente))
                {
                    if (esReversion != true)
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            else
            { MessageBox.Show("Problemas con el servicio de SUNAT, Comunicarse con TI.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            this.Cursor = Cursors.Default;
        }

        private void ActualizarEstadoSunatTransportista()
        {
            entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor"));
            entGuiaTransportista.entGRT_Generales_Serie_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
            entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));

            this.Cursor = Cursors.WaitCursor;
            ConsultarGuiaIndividualTransportista(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);

            if (consultaIndividual.ent_InformacionComprobante != null)
            {
                if (consultaIndividual.ent_InformacionComprobante.l_respuestas.Count == 0)
                {
                    MessageBox.Show("Problemas con el servicio de SUNAT: " + consultaIndividual.at_MensajeResultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < consultaIndividual.ent_InformacionComprobante.l_respuestas.Count; i++)
                {
                    if (consultaIndividual.ent_InformacionComprobante.l_respuestas[i].at_CodigoRespuesta == "2" || consultaIndividual.ent_InformacionComprobante.l_respuestas[i].at_CodigoRespuesta == "1")
                    { entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "APROBADO"; }
                    else
                    { entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "RECHAZADO"; }
                }

                CargarArchivoXML();
                CargarRespuestaSunatCDR();

                if (consultaIndividual.ent_InformacionComprobante != null)
                {
                    entGuiaTransportista.entGRT_Respuesta_MensajeResultado = CDR_Respuesta_Descripcion; //consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_Descripcion;
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

                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia")) == "REVERSION")
                { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "REVERSION"; }
                else
                {
                    if (consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta != "3")
                    {
                        if (consultaIndividual.ent_InformacionComprobante.at_FechaReversion == "")
                        { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "ACEPTADO"; }
                        else
                        { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "REVERSION"; }
                    }
                    else if (consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "3")
                    { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "RECHAZADO"; }
                }

                entGuiaTransportista.entGRT_Respuesta_CodigoHash = consultaIndividual.ent_InformacionComprobante.at_CodigoHash;
                entGuiaTransportista.compania = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                entGuiaTransportista.TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));
                entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";

                if (clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaTransportista))
                {
                    if (esReversion != true)
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            else
            { MessageBox.Show(consultaIndividual.at_MensajeResultado.ToString(), "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            this.Cursor = Cursors.Default;
        }

        private void CargarArchivoXML()
        {
            ServiceGRT_QA.ens_ConsultarXML Archivo_XML = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, 0);

            while (Archivo_XML.at_NivelResultado == 0)
            {
                Archivo_XML = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, 0);
            }
            entGuiaTransportista.entGRT_Respuesta_XML_Archivo = Encoding.UTF8.GetString(Archivo_XML.ent_ResultadoXML.at_XML);
        }

        private void CargarArchivoXMLRemitente()
        {
            ServiceGRR_QA.ens_ConsultarXML Archivo_XML = CargarGuiaEnXMLRemitente(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M, 0);

            while (Archivo_XML.at_NivelResultado == 0)
            {
                Archivo_XML = CargarGuiaEnXMLRemitente(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M, 0);
            }
            entGuiaRemitente.entGRR_Respuesta_XML_Archivo = Encoding.UTF8.GetString(Archivo_XML.ent_ResultadoXML.at_XML);
        }

        private ServiceGRT_QA.ens_ConsultarXML CargarGuiaEnXML(string NumroDocumentoIdentidad, string Serie, int Numero, int NumeroRespuesta = 0)
        {
            consultarXML_CDR = new ServiceGRT_QA.ene_ConsultarXML();
            consultarXML_CDR.ent_ComprobanteConsultarXML = new ServiceGRT_QA.en_ComprobanteConsultarXML();
            consultarXML_CDR.at_NumeroDocumentoIdentidad = NumroDocumentoIdentidad;
            consultarXML_CDR.ent_ComprobanteConsultarXML.at_Serie = Serie;
            consultarXML_CDR.ent_ComprobanteConsultarXML.at_Numero = Numero;
            consultarXML_CDR.ent_ComprobanteConsultarXML.at_NumeroRespuesta = NumeroRespuesta;
            ServiceGRT_QA.ens_ConsultarXML Respuesta_XML_CDR = new ServiceGRT_QA.ens_ConsultarXML();
            Respuesta_XML_CDR = requestTransportista.ConsultarXMLGRT(consultarXML_CDR);

            return Respuesta_XML_CDR;
        }

        private ServiceGRR_QA.ens_ConsultarXML CargarGuiaEnXMLRemitente(string NumroDocumentoIdentidad, string Serie, int Numero, int NumeroRespuesta = 0)
        {
            consultarXML_CDRRemitente = new ServiceGRR_QA.ene_ConsultarXML();
            consultarXML_CDRRemitente.ent_ComprobanteConsultarXML = new ServiceGRR_QA.en_ComprobanteConsultarXML();
            consultarXML_CDRRemitente.at_NumeroDocumentoIdentidad = NumroDocumentoIdentidad;
            consultarXML_CDRRemitente.ent_ComprobanteConsultarXML.at_Serie = Serie;
            consultarXML_CDRRemitente.ent_ComprobanteConsultarXML.at_Numero = Numero;
            consultarXML_CDRRemitente.ent_ComprobanteConsultarXML.at_NumeroRespuesta = NumeroRespuesta;
            ServiceGRR_QA.ens_ConsultarXML Respuesta_XML_CDR = new ServiceGRR_QA.ens_ConsultarXML();
            Respuesta_XML_CDR = requestRemitente.ConsultarXMLGRR(consultarXML_CDRRemitente);

            return Respuesta_XML_CDR;
        }

        private void CargarRespuestaSunatCDR()
        {
            if (consultaIndividual.at_NivelResultado == 1) // UNO SIGNIFICA QUE SI ENCONTRO LA RESPUESTA DEL CDR
            {
                ServiceGRT_QA.ens_ConsultarXML Respuesta_XML_CDR = CargarGuiaEnXML(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M, Convert.ToInt32(consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_NroRespuesta));
                String xmlCDR = "";
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
                else { xmlCDR = "No hay XML para consultar"; }

                if (xmlCDR.Length > 0)
                {
                    /* using (StreamWriter log = new StreamWriter(CarpetaAlacenamientoLogErrores + "CDR=Guia_Serie_" + entGuiaTransportista.entGRT_Generales_Serie_M + "_Numero_" + entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt"))
                     {
                         log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + "Mensaje: " + consultaIndividual.at_MensajeResultado + " Detalle: " + xmlCDR);
                     }
                     */
                    entGuiaTransportista.entGRT_Respuesta_Xml_CDR = Encoding.UTF8.GetString(Respuesta_XML_CDR.ent_ResultadoXML.at_XML);
                    entGuiaTransportista.entGRT_Respuesta_Fecha_CDR = Respuesta_XML_CDR.ent_ResultadoXML.at_FechaXML;
                }
            }
        }

        private void CargarRespuestaSunatCDRRemitente()
        {
            if (consultaIndividualRemitente.at_NivelResultado == 1) // UNO SIGNIFICA QUE SI ENCONTRO LA RESPUESTA DEL CDR
            {
                ServiceGRR_QA.ens_ConsultarXML Respuesta_XML_CDR = CargarGuiaEnXMLRemitente(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M, Convert.ToInt32(consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_NroRespuesta));
                String xmlCDR = "";
                if (Respuesta_XML_CDR.at_MensajeResultado != "No hay XML para consultar")
                {
                    dtRespuestaXML_CDR = Utilitario.Instancia.ObtenerNodoXML(Encoding.UTF8.GetString((Respuesta_XML_CDR.ent_ResultadoXML.at_XML)));
                    entGuiaRemitente.entGRR_Respuesta_URL_GuiaSunat = dtRespuestaXML_CDR.Rows[0]["Descripcion"].ToString();
                    if (dtRespuestaXML_CDR != null)
                    {
                        if (dtRespuestaXML_CDR.Rows.Count > 0)
                        {
                            xmlCDR = Utilitario.Instancia.DatatableToXml(dtRespuestaXML_CDR);
                        }
                        else
                        {
                            xmlCDR = "ERROR AL TARER ALGUN DATO: " + " METODO= Utilitario.Instancia.ObtenerNodoXML(Encoding.UTF8.GetString((Respuesta_XML_CDR.ent_ResultadoXML.at_XML))";
                        }
                    }
                }
                else { xmlCDR = "No hay XML para consultar"; }

                if (xmlCDR.Length > 0)
                {
                    /*if (Directory.Exists(CarpetaAlacenamientoLogErrores))
                    {
                        using (StreamWriter log = new StreamWriter(CarpetaAlacenamientoLogErrores + "CDR=Guia_Serie_" + entGuiaRemitente.entGRR_Generales_Serie_M + "_Numero_" + entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt"))
                        {
                            log.WriteLine("Usuario: " + Utilitario.Instancia.SesionUsuario.usuario + "Mensaje: " + consultaIndividualRemitente.at_MensajeResultado + " Detalle: " + xmlCDR);
                        }
                    }*/

                    entGuiaRemitente.entGRR_Respuesta_Xml_CDR = Encoding.UTF8.GetString(Respuesta_XML_CDR.ent_ResultadoXML.at_XML);
                    entGuiaRemitente.entGRR_Respuesta_Fecha_CDR = Respuesta_XML_CDR.ent_ResultadoXML.at_FechaXML;
                }
            }
        }

        private void verDetalleEstadoSunatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));

                DataTable dt = clsOperacionesBL.Instancia.ReportesApp_ListarHistorialRespuestaSunat_GuiasElectronicas(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica);

                if (dt.Rows.Count > 0)
                {
                    frmGuiasElectronicas_HsitorialEstadoSUNAT openHistorial = new frmGuiasElectronicas_HsitorialEstadoSUNAT();
                    openHistorial.dt = dt;
                    openHistorial.ShowDialog();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void reimprimirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));

                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "T")
                {
                    if (clsOperacionesBL.Instancia.Reportesapp_Operaciones_Reimprimir_GuiaElectronica(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        CargarGuiaEnPDFTransportista();
                        this.Cursor = Cursors.Default;
                    }
                }
                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "R")
                {
                    if (clsOperacionesBL.Instancia.Reportesapp_Operaciones_Reimprimir_GuiaElectronica(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        CargarGuiaEnPDFRemitente();
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CargarGuiaEnPDFTransportista()
        {
            //if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat")) == "APROBADO")
            //{
            ServiceGRT_QA.ene_ConsultarRI consultaRI = new ServiceGRT_QA.ene_ConsultarRI();
            consultaRI.at_NumeroDocumentoIdentidad = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor"));
            consultaRI.ent_Comprobante = new ServiceGRT_QA.en_ComprobanteConsultarRI();
            consultaRI.ent_Comprobante.at_Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
            consultaRI.ent_Comprobante.at_Numero = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
            resultadoRiTransportista = new ServiceGRT_QA.ens_ResultadoRI();
            requestTransportista = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
            resultadoRiTransportista = requestTransportista.ConsultarRI_GRT(consultaRI);
    
            int contador = 0;
                    
            while (resultadoRiTransportista.at_NivelResultado == 0)
            {
                resultadoRiTransportista = requestTransportista.ConsultarRI_GRT(consultaRI);

                if (resultadoRiTransportista.at_NivelResultado != 0)
                {
                    entGuiaTransportista.entGRT_Respuesta_ArchivoPDF = resultadoRiTransportista.ent_Resultado.at_ArchivoRI;
                    entGuiaTransportista.entGRT_Respuesta_NombreRI = resultadoRiTransportista.ent_Resultado.at_NombreRI;
                    entGuiaTransportista.entGRT_Respuesta_FechaRI = resultadoRiTransportista.ent_Resultado.at_FechaGenerado;
                    break;
                }

                if (contador >= 300) { break; }

                contador++;
            }

            if (resultadoRiTransportista.ent_Resultado != null)
            {
                if (resultadoRiTransportista.at_NivelResultado != 0)
                {
                    entGuiaTransportista.entGRT_Respuesta_ArchivoPDF = resultadoRiTransportista.ent_Resultado.at_ArchivoRI;
                    entGuiaTransportista.entGRT_Respuesta_NombreRI = resultadoRiTransportista.ent_Resultado.at_NombreRI;
                    entGuiaTransportista.entGRT_Respuesta_FechaRI = resultadoRiTransportista.ent_Resultado.at_FechaGenerado;
                }

                AbrirPDFTransportista(resultadoRiTransportista);
            }
            else
            { MessageBox.Show("Se Guardó Correctamente, pero el PDF no se pudo generar: " + resultadoRiTransportista.at_MensajeResultado, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void CargarGuiaEnPDFRemitente()
        {
            //if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat")) == "APROBADO")
            // {

            ServiceGRR_QA.ene_ConsultarRI consultaRI = new ServiceGRR_QA.ene_ConsultarRI();

            consultaRI.at_NumeroDocumentoIdentidad = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor"));
            consultaRI.ent_Comprobante = new ServiceGRR_QA.en_ComprobanteConsultarRI();
            consultaRI.ent_Comprobante.at_Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
            consultaRI.ent_Comprobante.at_Numero = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
            resultadoRiRemitente = new ServiceGRR_QA.ens_ResultadoRI();
            requestRemitente = new ServiceGRR_QA.ServicioGuiaRemisionRemitenteClient();
            resultadoRiRemitente = requestRemitente.ConsultarRI_GRR(consultaRI);
            int contador = 0;
            while (resultadoRiRemitente.at_NivelResultado == 0)
            {
                resultadoRiRemitente = requestRemitente.ConsultarRI_GRR(consultaRI);

                if (resultadoRiRemitente.at_NivelResultado != 0)
                {
                    entGuiaRemitente.entGRR_Respuesta_ArchivoPDF = resultadoRiRemitente.ent_Resultado.at_ArchivoRI;
                    entGuiaRemitente.entGRR_Respuesta_NombreRI = resultadoRiRemitente.ent_Resultado.at_NombreRI;
                    entGuiaRemitente.entGRR_Respuesta_FechaRI = resultadoRiRemitente.ent_Resultado.at_FechaGenerado;
                    break;
                }

                if (contador >= 300) { break; }

                contador++;
            }

            if (resultadoRiRemitente.ent_Resultado != null)
            {
                if (resultadoRiRemitente.at_NivelResultado != 0)
                {
                    entGuiaRemitente.entGRR_Respuesta_ArchivoPDF = resultadoRiRemitente.ent_Resultado.at_ArchivoRI;
                    entGuiaRemitente.entGRR_Respuesta_NombreRI = resultadoRiRemitente.ent_Resultado.at_NombreRI;
                    entGuiaRemitente.entGRR_Respuesta_FechaRI = resultadoRiRemitente.ent_Resultado.at_FechaGenerado;
                }

                AbrirPDFRemitente(resultadoRiRemitente);
            }
            else
            { MessageBox.Show("Se Guardó Correctamente, pero el PDF no se pudo generar: " + resultadoRiTransportista.at_MensajeResultado, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            // }
        }

        private void registrarSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmGestionarSeries openSeries = new frmGestionarSeries();
                openSeries.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsActualizarGuias_Click(object sender, EventArgs e)
        {
            try
            {
                dtgListaPendientes.DataSource = null;
                dgvListaPendientesVista.Columns.Clear();
                
                DataTable dtGuiasPendientes = clsOperacionesBL.Instancia.ReportesApp_ListarGuiasElectronicas_ListarPendientes(dtpFechaInicio.Text, dtpFechaFin.Text, txtviaje.Text, txtClienteFiltro.Text);
                dtgListaPendientes.DataSource = dtGuiasPendientes;
                if (dtGuiasPendientes.Rows.Count > 0) { dgvListaPendientesVista.BestFitColumns(); }

                int totalFilas = dgvListaPendientesVista.DataRowCount;

                if (totalFilas > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    for (int i = 0; i < totalFilas; i++)
                    {
                        int rowHandle = dgvListaPendientesVista.GetVisibleRowHandle(i);
                        
                        if (rowHandle < 0) continue;
                        
                        esReversion = false;
                        string TipoGuia = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "TipoGuia"));
                        if (TipoGuia == "T")
                        {
                            entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "NumeroDocIdentidad_Emisor"));
                            entGuiaTransportista.entGRT_Generales_Serie_M = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "SerieGuia"));
                            entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(rowHandle, "NumeroGuia"));
                            
                            ConsultarGuiaIndividualTransportista(entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M, entGuiaTransportista.entGRT_Generales_Serie_M, entGuiaTransportista.entGRT_Generales_Numero_M);
                            
                            if (consultaIndividual.ent_InformacionComprobante != null)
                            {
                                for (int j = 0; j < consultaIndividual.ent_InformacionComprobante.l_respuestas.Count; j++)
                                {
                                    if (consultaIndividual.ent_InformacionComprobante.l_respuestas[j].at_CodigoRespuesta == "2" || consultaIndividual.ent_InformacionComprobante.l_respuestas[j].at_CodigoRespuesta == "1")
                                    { entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "APROBADO"; }
                                    else
                                    { entGuiaTransportista.entGRT_Respuesta_EstadoSunat = "RECHAZADO"; }
                                }
                                
                                CargarArchivoXML();
                                CargarRespuestaSunatCDR();
                                
                                if (consultaIndividual.ent_InformacionComprobante != null)
                                {
                                    entGuiaTransportista.entGRT_Respuesta_MensajeResultado = CDR_Respuesta_Descripcion;
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

                                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(rowHandle, "EstadoGuia")) == "REVERSION")
                                { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "REVERSION"; }
                                else
                                {
                                    if (consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta != "3")
                                    {
                                        if (consultaIndividual.ent_InformacionComprobante.at_FechaReversion == "")
                                        { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "ACEPTADO"; }
                                        else
                                        { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "REVERSION"; }
                                    }
                                    else if (consultaIndividual.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "3")
                                    { entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = "RECHAZADO"; }
                                }
                                
                                entGuiaTransportista.entGRT_Respuesta_CodigoHash = consultaIndividual.ent_InformacionComprobante.at_CodigoHash;
                                entGuiaTransportista.compania = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "idEmpresaGrupo"));
                                entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(rowHandle, "idCliente"));
                                entGuiaTransportista.idOT = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(rowHandle, "idOT"));
                                entGuiaTransportista.TipoGuia = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "TipoGuia"));
                                entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(rowHandle, "idGuiaElectronica"));
                                entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";
                                
                                if (clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaTransportista))
                                {
                                    if (esReversion != true)
                                    { /*MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);*/ }
                                }
                                else
                                { /*MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/ }
                            }
                            else { MessageBox.Show(consultaIndividual.at_MensajeResultado.ToString(), "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        }
                        
                        if (TipoGuia == "R")
                        {
                            entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "NumeroDocIdentidad_Emisor"));
                            entGuiaRemitente.entGRR_Generales_Serie_M = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "SerieGuia"));
                            entGuiaRemitente.entGRR_Generales_Numero_M = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(rowHandle, "NumeroGuia"));
                            
                            ConsultarGuiaIndividualRemitente(entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M, entGuiaRemitente.entGRR_Generales_Serie_M, entGuiaRemitente.entGRR_Generales_Numero_M);
                            
                            if (consultaIndividualRemitente.ent_InformacionComprobante != null && consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas.Count > 0)
                            {
                                if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "2" || consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "1")
                                { entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "APROBADO"; }
                                else
                                { entGuiaRemitente.entGRR_Respuesta_EstadoSunat = "RECHAZADO"; }
                                
                                CargarArchivoXMLRemitente();
                                CargarRespuestaSunatCDRRemitente();
                                
                                entGuiaRemitente.entGRR_Respuesta_MensajeResultado = consultaIndividualRemitente.at_MensajeResultado;
                                entGuiaRemitente.entGRR_Respuesta_FechaGeneracion = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaGeneracion;
                                entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaOtorgamiento;
                                entGuiaRemitente.entGRR_Respuesta_FechaTransmision = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaTransmision;
                                entGuiaRemitente.entGRR_Respuesta_FechaReversion = consultaIndividualRemitente.ent_InformacionComprobante.at_FechaReversion;
                                entGuiaRemitente.entGRR_Respuesta_CodigoMensaje = consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta;
                                
                                if (Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "EstadoGuia")) == "REVERSION")
                                { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "REVERSION"; }
                                else
                                {
                                    if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta != "3")
                                    {
                                        if (consultaIndividualRemitente.ent_InformacionComprobante.at_FechaReversion == "")
                                        { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "ACEPTADO"; }
                                        else
                                        { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "REVERSION"; }
                                    }
                                    else if (consultaIndividualRemitente.ent_InformacionComprobante.l_respuestas[0].at_CodigoRespuesta == "3")
                                    { entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = "RECHAZADO"; }
                                }
                                
                                entGuiaRemitente.entGRR_Respuesta_CodigoHash = consultaIndividualRemitente.ent_InformacionComprobante.at_CodigoHash;
                                entGuiaRemitente.compania = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "idEmpresaGrupo"));
                                entGuiaRemitente.idcliente = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(rowHandle, "idCliente"));
                                entGuiaRemitente.TipoGuia = Convert.ToString(dgvListaPendientesVista.GetRowCellValue(rowHandle, "TipoGuia"));
                                entGuiaRemitente.idGuiaElectronica = Convert.ToInt32(dgvListaPendientesVista.GetRowCellValue(rowHandle, "idGuiaElectronica"));
                                entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado = "ENCONTRADO";
                                
                                if (clsOperacionesBL.Instancia.GuardarRespuestaSunat(entGuiaRemitente))
                                {
                                    if (esReversion != true)
                                    { /* MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); */ }
                                }
                                else
                                { /* MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); */ }
                            }
                            else
                            { MessageBox.Show("Problemas con el servicio de SUNAT, Comunicarse con TI.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        }
                    }

                    MessageBox.Show("Se actualizó el estado de las guías pendientes.", "Proceso terminado", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    dtgListaPendientes.DataSource = null;
                    dgvListaPendientesVista.Columns.Clear();
                    this.Cursor = Cursors.Default;
                    ListarGuiasElectronicas();
                }
                else
                {
                    MessageBox.Show("No hay guías pendientes para actualizar.","Resultado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                }
            }
            catch
            {
                MessageBox.Show("Se produjo un error al actualizar el estado de las guías.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
        }

        private void confirmarAnulacionSUNATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));

                if (clsOperacionesBL.Instancia.ReportesApp_ConfirmarAnulacionSunat(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarGuiasElectronicas();
                }
                else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }

               /*
                if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString() == "REVERSION")
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_ConfirmarAnulacionSunat(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarGuiasElectronicas();
                    }
                    else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No es posible confirmar Anulacion, Guia no se encuentra revertida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                */ 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvListaGuiaTraspExpressVista_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) { }

        private void dgvListaGuiaTraspExpressVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "EstadoGuia")
            {
                if (e.CellValue.ToString() == "ACEPTADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(129, 199, 132);
                }

            }

            if (e.Column.FieldName == "EstadoGuia")
            {
                if (e.CellValue.ToString() == "REVERSION")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 155, 155);
                }

            }
            if (e.Column.FieldName == "EstadoSunat")
            {
                if (e.CellValue.ToString() == "APROBADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(77, 208, 225);
                }

            }
            if (e.Column.FieldName == "EstadoSunat")
            {
                if (e.CellValue.ToString() == "ANULADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(240, 98, 146);
                }

            }
            if (e.Column.FieldName == "EstadoSunat")
            {
                if (e.CellValue.ToString() == "RECHAZADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 87, 87);
                }

            }
            if (e.Column.FieldName == "EstadoGuia")
            {
                if (e.CellValue.ToString() == "PENDIENTE")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 183, 77);
                }

            }
        }

        private void chkEstadoGuia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstadoGuia.Checked)
            {
                groupEstadoGuia.Enabled = true;
                cbxSerieFiltro.Text = "Todos";


            }
            if (chkEstadoGuia.Checked == false)
            {
                groupEstadoGuia.Enabled = false;
                CargarSeries();
            }

        }

        private void maestroUnidadesTercerosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMestroUnidadesTercerosGuiaRemitente open = new frmMestroUnidadesTercerosGuiaRemitente();
                open.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void maestroConductoresTercerosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMaestroConductorTerceroGuiaElectronica open = new frmMaestroConductorTerceroGuiaElectronica();
                open.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void maestroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMaestroNumerosMTCxEmpresa open = new frmMaestroNumerosMTCxEmpresa();
                open.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));
                string serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));

                if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje").ToString().Length == 0)
                {
                    if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString() == "PENDIENTE" || dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString() == "ACEPTADO")
                    {
                        if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString() == "PENDIENTE" || dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString() == "RECHAZADO")
                        {
                            if (clsOperacionesBL.Instancia.Reportesapp_Operaciones_EliminarRegistroGuiasElectronicas(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica, serie))
                            {
                                MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ListarGuiasElectronicas();
                            }
                            else
                            {
                                MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ListarGuiasElectronicas();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Guia Vinculada a Viaje , no se puede borrar el registro", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tolvasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPreviajeTolvasPlantillas open = new FrmPreviajeTolvasPlantillas();
                open.registrarGuia = registrarGuia;
                open.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void maestroRutaClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMaestroRutaClienteTransportista open = new FrmMaestroRutaClienteTransportista();
                open.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void desvincularViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat")) != "APROBADO" && Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia")) != "ACEPTADO"
                    && Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "T" && Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje")).Length >0 )
                {
                    string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo"));
                    int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                    int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                    string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia"));
                    int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica"));

                    int idProgramacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProgramacion"));
                    int AnioProgramacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "AnioProgramacion"));
                    string SerieGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
                    string NumeroGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                    int idViaje = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idViaje"));
                    string Viaje = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje"));
                    int LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "LineaOT"));

                    FrmListarPreviajesLibres open = new FrmListarPreviajesLibres();
                    open.txtTicketActual.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion"));
                    open.txtGuiaTransportsta.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia")) + "-" + Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                    //open.idTipoProgramacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idTipoProgramacion"));

                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_DesvincularViaje_Previaje_GuiaElectronica(empresa, idCliente, idOt, TipoGuia, ref idGuiaElectronica, open.NuevoNroTicket, ref idViaje, ref Viaje, idProgramacion, AnioProgramacion, ref SerieGuia, ref NumeroGuia, LineaOT, open.Respuesta))
                        {
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarGuiasElectronicas();
                        }
                        else
                        {
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ListarGuiasElectronicas();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void neuvoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void generarGuiaDeEventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat")) == "APROBADO"
                    && (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia")) == "T"))
                {
                    FrmGuiaDeEvento open = new FrmGuiaDeEvento();
                    open.txtSerie.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
                    open.entGuiaTransportista.entGRT_Generales_Serie_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia"));
                    open.txtNumero.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                    open.entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                    open.txtRemitente.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Rem"));
                    open.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Rem"));
                    open.txtRemitente.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                    open.entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente")); ;
                    open.txtDestinatario.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Dest"));
                    open.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Dest"));

                    open.entGuiaTransportista.xml_entGTR_Conductor_M = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Conductores"));
                    open.entGuiaTransportista.CodigoProgramacion = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion"));
                    open.entGuiaTransportista.idviaje = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idViaje"));
                    open.entGuiaTransportista.viaje = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje"));

                    if (Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "GuiaEvento")) == "SI")
                    {
                        open.entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "LineaOTEvento"));
                        open.txtOT.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOTEvento"));
                        open.txtOTOriginal.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                        open.entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOTEvento"));
                        open.txtRuta.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RutaEvento"));
                        open.txtRuta.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRutaEvento"));

                        open.txtRutaOrigen.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                        open.txtRutaOrigen.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRuta"));

                        open.entGuiaTransportista.ruta = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RutaEvento"));
                        open.entGuiaTransportista.idRuta = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRutaEvento"));
                    }
                    else
                    {
                        open.entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "LineaOT"));
                        open.txtOT.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                        open.txtOTOriginal.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));
                        open.entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT"));

                        open.txtRutaOrigen.Text = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                        open.txtRutaOrigen.Tag = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRuta"));

                        open.entGuiaTransportista.ruta = Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta"));
                        open.entGuiaTransportista.idRuta = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRuta"));
                    }

                    open.NroPreviaje = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion"));
                    open.idTipoProgramacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idTipoProgramacion"));

                    DataTable dt = clsOperacionesBL.Instancia.ReportesApp_ListarClientes_GuiaElectronica(Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Dest")));
                    if (dt.Rows.Count <= 0) { MessageBox.Show("No se obtuvo pk del destinatario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    open.txtDestinatario.Tag = dt.Rows[0]["Persona"].ToString();
                    open.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Para generar Guia de Evento, la Guia  de Transportista tiene que estar en estado Aprobado y ser la operacion de LIMAGAS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void listaGuiasEventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmListarGuiaEvento open = new FrmListarGuiaEvento();
                open.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaGuiasTransportista_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvListaGuiaTraspExpressVista.RowCount > 0)
                {
                    if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString() == "T" /*&& dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje").ToString().Length > 0*/)
                    {

                        FrmGuiaElectronicaTransportista open = new FrmGuiaElectronicaTransportista();
                        open.entGuiaTransportista.compania = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo").ToString();
                        open.entGuiaTransportista.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));
                        open.entGuiaTransportista.idOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idOT").ToString());
                        open.entGuiaTransportista.TipoGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                        open.entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica").ToString());
                        open.entGuiaTransportista.LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "LineaOT").ToString());
                        open.entGuiaTransportista.entGRT_Generales_Serie_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                        open.entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                        open.entGuiaTransportista.idGuiaSpring = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaSpring").ToString());
                        open.entGuiaTransportista.idviaje = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idViaje") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idViaje"));
                        open.entGuiaTransportista.viaje = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Viaje").ToString();
                        open.entGuiaTransportista.Cliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Cliente").ToString();
                        open.entGuiaTransportista.idRemitente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRemitente"));
                        open.entGuiaTransportista.entGRT_Generales_FechaEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaEmision").ToString();
                        open.entGuiaTransportista.entGRT_Generales_HoraEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "HoraEmision").ToString();
                        open.entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                        open.entGuiaTransportista.entGRT_Generales_Observacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "ObservacionGuia").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_NombreComercial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreComercial_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_NumeroMTC = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroMTC_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_Telefono = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Telefono_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_CorreoContacto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Correo_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_SitioWeb = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SitioWeb_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ubigeo_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_DireccionDetallada = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionDetallada_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_Provincia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Provincia_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_Departamento = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Departamento_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_Distrito = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Distrito_Emisor").ToString();
                        open.entGuiaTransportista.entGRT_Emisor_CodigoPais_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodigoPais_Emisor").ToString();
                        open.entGuiaTransportista.correoCliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CorreoPrincipal_Cliente").ToString();
                        open.entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Rem").ToString();
                        open.entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Rem").ToString();
                        open.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Rem").ToString();
                        open.entGuiaTransportista.entGRT_Destinatario_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Dest").ToString();
                        open.entGuiaTransportista.entGRT_Destinatario_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Dest").ToString();
                        open.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Dest").ToString();
                        open.entGuiaTransportista.entGRT_Contratista_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Contra").ToString();
                        open.entGuiaTransportista.entGRT_Contratista_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Contra").ToString();
                        open.entGuiaTransportista.entGRT_Contratista_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Contra").ToString();
                        open.entGuiaTransportista.entGRT_SubContratista_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Subcontra").ToString();
                        open.entGuiaTransportista.entGRT_SubContratista_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Subcontra").ToString();
                        open.entGuiaTransportista.entGRT_SubContratista_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Subcontra").ToString();
                        open.entGuiaTransportista.idDocumentoRelacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idDocumentosRelacion"));
                        open.entGuiaTransportista.idIndicadoresServicio = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idIndicadoresServicio"));
                        open.entGuiaTransportista.xml_entGRT_TipoServicio = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SERVICIOS").ToString();
                        open.entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                        open.entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "PesoBruto"));
                        open.entGuiaTransportista.entGRT_PesoBruto_CodigoUnidadMedida_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodUnidadMedida_Peso").ToString();
                        open.entGuiaTransportista.entGRT_PesoBruto_DescripcionAdicional = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DescripcionAdicional_Peso").ToString();
                        open.entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoPartida").ToString();
                        open.entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoPartida").ToString();
                        open.entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoLlegada").ToString();
                        open.entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoLlegada").ToString();
                        open.entGuiaTransportista.idRuta = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idRuta"));
                        open.entGuiaTransportista.ruta = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ruta").ToString();
                        open.entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroPlaca").ToString();
                        open.entGuiaTransportista.idtracto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdVehiculo").ToString();
                        open.entGuiaTransportista.carreta = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Carreta").ToString();
                        open.entGuiaTransportista.idCarreta = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCarreta").ToString();
                        open.entGuiaTransportista.idConductoresGuia = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idConductoresGuia"));
                        open.entGuiaTransportista.idProductosTraslado = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProductosTraslado"));
                        open.entGuiaTransportista.entGRT_Respuesta_EstadoGuardado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString();
                        open.entGuiaTransportista.entGRT_Respuesta_EstadoSunat = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString();
                        open.entGuiaTransportista.impreso = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Impreso"));
                        open.entGuiaTransportista.idProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProgramacion") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProgramacion"));
                        open.entGuiaTransportista.AnioProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "AnioProgramacion") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "AnioProgramacion"));
                        open.entGuiaTransportista.CodigoProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion").ToString() == "" ? "" : Convert.ToString(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NroTicketProgramacion"));
                        open.entGuiaTransportista.idTipoProgramacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idTipoProgramacion") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idTipoProgramacion"));
                        open.entGuiaTransportista.TipoViaje = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoViaje").ToString();
                        open.entGuiaTransportista.xml_entGRT_DocumentosRelacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_DocumentosRelacion").ToString();
                        open.entGuiaTransportista.xml_entGTR_Conductor_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Conductores").ToString();
                        open.entGuiaTransportista.xml_entGRT_Productos_Bienes = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Productos").ToString();
                        open.entGuiaTransportista.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        open.btnGuardar.Text = "VER";

                        open.TipoProgramacion = "LINDLEY";
                        open.dgvProductosGuia.Enabled = true;
                        open.dgvProductosGuia.ReadOnly = false;

                        open.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        open.btnGuardar.Enabled = false;
                        open.tabControl1.Enabled = false;

                        open.CargarListaTolvas += new FrmGuiaElectronicaTransportista.CargarListaTolvasEventHandler(CargarRespuesta);
                        open.Show();
                    }

                    if (dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString() == "R")
                    {
                        FrmGuiaElectronicaRemitente openRemitente = new FrmGuiaElectronicaRemitente();
                        openRemitente.entGuiaRemitente.compania = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idEmpresaGrupo").ToString();
                        openRemitente.entGuiaRemitente.idcliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idCliente"));

                        openRemitente.entGuiaRemitente.TipoGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoGuia").ToString();
                        openRemitente.entGuiaRemitente.idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idGuiaElectronica").ToString());
                        openRemitente.entGuiaRemitente.entGRR_Generales_Serie_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SerieGuia").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_Numero_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroGuia"));
                        openRemitente.entGuiaRemitente.Cliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Cliente").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_FechaEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaEmision").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_HoraEmision_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "HoraEmision").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_FechaIncioTraslado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_Observacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "ObservacionGuia").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_NombreComercial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreComercial_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_NumeroMTC = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroMTC_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_Telefono = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Telefono_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_CorreoContacto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Correo_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_SitioWeb = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SitioWeb_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Ubigeo_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_DireccionDetallada = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionDetallada_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_Provincia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Provincia_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_Departamento = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Departamento_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_Distrito = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Distrito_Emisor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Emisor_CodigoPais_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodigoPais_Emisor").ToString();
                        openRemitente.entGuiaRemitente.correoCliente = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CorreoPrincipal_Cliente").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Rem").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Rem").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Remitente_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Rem").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Dest").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Dest").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Destinatario_RazonSocial_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Dest").ToString();
                        openRemitente.entGuiaRemitente.idDocumentoRelacion = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idDocumentoRelacion"));
                        openRemitente.entGuiaRemitente.idIndicadoresServicio = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idIndicadoresServicio"));
                        openRemitente.entGuiaRemitente.xml_entGRR_TipoServicio = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "SERVICIOS").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_FechaIncioTraslado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "FechaInicio_Traslado").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "PesoBruto"));
                        openRemitente.entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodUnidadMedida_Peso").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PesoBruto_DescripcionAdicional = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DescripcionAdicional_Peso").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PuntoPartida_NombreUbigeo = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreUbigeoPartida").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoPartida").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoPartida").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PuntoDestino_NombreUbigeo = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreUbigeoLlegada").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "UbigeoPuntoLlegada").ToString();
                        openRemitente.entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "DireccionPuntoLlegada").ToString();

                        openRemitente.entGuiaRemitente.CodigoEstablecimientoOrigen = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodEstableOrigen").ToString();
                        openRemitente.entGuiaRemitente.NombreEstablecimientoOrigen = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreEstableOrigen").ToString();
                        openRemitente.entGuiaRemitente.CodigoEstablecimientoDestino = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodEstableDestino").ToString();
                        openRemitente.entGuiaRemitente.NombreEstablecimientoDestino = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NombreEstableDestino").ToString();

                        openRemitente.entGuiaRemitente.entGRR_Vehiculo_NumeroPlaca_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroPlaca").ToString();
                        openRemitente.entGuiaRemitente.idtracto = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "IdVehiculo").ToString();
                        openRemitente.entGuiaRemitente.idConductoresGuia = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idConductoresGuia") == DBNull.Value ? 0 : Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idConductoresGuia"));
                        openRemitente.entGuiaRemitente.idProductosTraslado = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "idProductosTraslado"));
                        openRemitente.entGuiaRemitente.entGRR_Respuesta_EstadoGuardado = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoGuia").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Respuesta_EstadoSunat = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "EstadoSunat").ToString();
                        openRemitente.entGuiaRemitente.impreso = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "Impreso"));

                        openRemitente.entGuiaRemitente.entGRR_Proveedor_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Proveedor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Proveedor_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Proveedor").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Proveedor_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Proveedor").ToString();

                        openRemitente.entGuiaRemitente.entGRR_Transportista_NumeroDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "NumeroDocIdentidad_Trans").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Transportista_TipoDocumentoIdentidad = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoDocIdentidad_Trans").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Transportista_RazonSocial = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "RazonSocial_Proveedor_Trans").ToString();

                        openRemitente.entGuiaRemitente.entGRR_Generales_CodigoMotivo_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodMotivo").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_Modalidad_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "CodModalidad").ToString();
                        openRemitente.entGuiaRemitente.entGRR_Generales_DescripcionMotivo = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "MotivoTraslado").ToString();

                        openRemitente.entGuiaRemitente.TipoViaje = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "TipoViaje").ToString();
                        openRemitente.entGuiaRemitente.xml_entGRR_DocumentosRelacion = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_DocumentosRelacion").ToString();
                        openRemitente.entGuiaRemitente.xml_entGRR_Conductor_M = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Conductores").ToString();
                        openRemitente.entGuiaRemitente.xml_entGRR_Productos_Bienes = dgvListaGuiaTraspExpressVista.GetRowCellValue(dgvListaGuiaTraspExpressVista.FocusedRowHandle, "xml_Productos").ToString();
                        openRemitente.entGuiaRemitente.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        openRemitente.btnGuardar.Text = "VER";

                        openRemitente.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        openRemitente.entGuiaRemitente.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        //openRemitente.dgvProductosGuia.Enabled = false;
                        openRemitente.txtEmpresaRemitente.Enabled = false;
                        openRemitente.txtEmpresaDestinatario.Enabled = false;
                        openRemitente.txtPlaca.Enabled = false;
                        openRemitente.txtCarreta.Enabled = false;
                        openRemitente.txtConductor.Enabled = false;
                        openRemitente.txtPlaca2.Enabled = false;
                        openRemitente.txtCarreta2.Enabled = false;
                        openRemitente.txtConductor2.Enabled = false;
                        openRemitente.txtObservaciones.Enabled = false;
                        openRemitente.groupDireccionDestino.Enabled = false;
                        openRemitente.groupDireccionPartida.Enabled = false;
                        openRemitente.groupUbigeoLlegada.Enabled = false;
                        openRemitente.groupUbigeoPartida.Enabled = false;
                        openRemitente.txtUbigeoPartida.Enabled = false;
                        openRemitente.txtUbigeoLlegada.Enabled = false;
                        openRemitente.txtDireccionPartida.Enabled = false;
                        openRemitente.txtDireccionDestino.Enabled = false;
                        openRemitente.txtRazonSocialProveedor.Enabled = false;
                        openRemitente.txtRazonSocialTransportista.Enabled = false;
                        openRemitente.txtEstablecimientoOrigen.Enabled = false;
                        openRemitente.txtEstablecimientoDestino.Enabled = false;
                        openRemitente.btnGuardar.Enabled = false;
                        openRemitente.btnCorreos.Enabled = false;
                        openRemitente.btnAgregarProgramacion.Enabled = false;
                        openRemitente.checkTercero.Enabled = false;
                        openRemitente.txtPesoTotal.Enabled = false;
                        openRemitente.TipoProgramacion = "LINDLEY";
                        openRemitente.dgvProductosGuia.Enabled = true;
                        openRemitente.dgvProductosGuia.ReadOnly = false;

                        openRemitente.TipoOperacion = Utilitario.TipoOperacion.Editar;
                        openRemitente.btnGuardar.Enabled = false;

                        openRemitente.CargarListaRemitente += new FrmGuiaElectronicaRemitente.CargarListaRemitenteEventHandler(CargarListaRemitente);
                        openRemitente.Show();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CargarRespuesta(bool esRegisteroExitoso, FrmGuiaElectronicaTransportista transportista)
        {
            transportista.FormClosed -= transportista.FrmGuiaElectronicaTransportista_FormClosed;
            transportista.Close();
            ListarGuiasElectronicas();
        }

        private void CargarListaRemitente(FrmGuiaElectronicaRemitente remitente)
        {
            remitente.FormClosed -= remitente.FrmGuiaElectronicaRemitente_FormClosed;
            remitente.Close();
            ListarGuiasElectronicas();
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (dgvListaGuiaTraspExpressVista.RowCount == 0) { contextMenuStrip1.Enabled = false; }
            else { contextMenuStrip1.Enabled = true; }
        }

        private void btnBuscarGuia_Click(object sender, EventArgs e)
        {
            try { ListarGuiasElectronicas(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGuiasElectronicas(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarGuiasElectronicas(); }
        }

        private void reimprimirTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int[] SelectedRowHandles = dgvListaGuiaTraspExpressVista.GetSelectedRows();

                for (int i = 0; i < dgvListaGuiaTraspExpressVista.SelectedRowsCount; i++)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idEmpresaGrupo"]);
                    int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idCliente"]);
                    int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idOT"]);
                    string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["TipoGuia"]);
                    int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idGuiaElectronica"]);

                    if (clsOperacionesBL.Instancia.Reportesapp_Operaciones_Reimprimir_GuiaElectronica(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica))
                    {
                        if (dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["EstadoSunat"].ToString() != "ANULADO")
                        { TicketTolvas(i, SelectedRowHandles); }
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void TicketTolvas(int i,int[] SelectedRowHandles)
        {
            PrintDocument printDoc = new PrintDocument();
            Ticket ticket = new Ticket();
            ticket.MaxChar = 40;
            ticket.MaxCharDescription = 30;
            ticket.AddSubHeaderLine2("GRUPO TRANSPESA S.A.C.");
            ticket.FontCodigo = 12;
            ticket.AddSubHeaderLine("        GUÍA DE TRANSPORTISTA" + "                                                               ");
            ticket.FontSize = 8;
            ticket.AddSubHeaderLine("GUIA NRO: " + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["SerieGuia"].ToString() + "-" + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["NumeroGuia"].ToString() + "                          ");
            ticket.AddSubHeaderLine("FECHA EMISION: " + Convert.ToDateTime(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["FechaEmision"]).ToShortDateString() + " " + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["HoraEmision"].ToString() + "                          ");
            ticket.AddSubHeaderLine("FECHA TRASLADO: " + dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("FechaInicio_Traslado").ToString() + "                          ");
            ticket.AddSubHeaderLine("PUNTO PARTIDA: " + "");
            ticket.AddSubHeaderLine(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["DireccionPuntoPartida"].ToString() + "                    ");
            ticket.AddSubHeaderLine("                                         ");
            ticket.AddSubHeaderLine("PUNTO LLEGADA: " + "");
            ticket.AddSubHeaderLine(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["DireccionPuntoLlegada"].ToString() + "                    ");
            ticket.AddSubHeaderLine("                                         ");
            ticket.AddSubHeaderLine("RUC REMITENTE: " + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["NumeroDocIdentidad_Rem"].ToString()  + "                                         ");
            //ticket.AddSubHeaderLine("========================================");
            ticket.AddSubHeaderLine("REMITENTE: " + dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("RazonSocial_Rem").ToString() + "                                   ");

            DataTable dtDocumentos = Utilitario.Instancia.ConvertirXMLaDatatable(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["xml_DocumentosRelacion"].ToString() );
            if (dtDocumentos.Rows.Count > 0)
            { ticket.AddSubHeaderLine("GUIA NRO REMITENTE: " + dtDocumentos.Rows[0]["NumeroComprobante_Relacion"].ToString() + "                                   "); }

            ticket.AddSubHeaderLine("RUC DESTINATARIO: " +  dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["NumeroDocIdentidad_Dest"].ToString()  + "                                   ");
            ticket.AddSubHeaderLine("DESTINATARIO: " + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["RazonSocial_Dest"].ToString());
            //ticket.AddSubHeaderLine("========================================");
            ticket.AddSubHeaderLine("TRACTO: " + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["NumeroPlaca"].ToString() + "                                   ");
            ticket.AddSubHeaderLine("CARRETA: " + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["Carreta"].ToString() + "                                   ");

            DataTable dtConductor = Utilitario.Instancia.ConvertirXMLaDatatable(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["xml_Conductores"].ToString());
            ticket.AddSubHeaderLine("CONDUCTOR: " + dtConductor.Rows[0]["Apellidos_Conductor"].ToString() + ", " + dtConductor.Rows[0]["Nombres_Conductor"].ToString() + "                                   ");


            ticket.AddSubHeaderLine("BREVETE: " +  dtConductor.Rows[0]["Licencia_Conductor"].ToString() + "                                   ");
            //ticket.AddSubHeaderLine("========================================");
            ticket.AddSubHeaderLine("                                         ");
            DataTable dtProducto = Utilitario.Instancia.ConvertirXMLaDatatable(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("xml_Productos").ToString());
            ticket.AddSubHeaderLine("PRODUCTO: ");
            ticket.AddSubHeaderLine(dtProducto.Rows[0]["Descripcion_Producto"].ToString() + "                                   ");

            /* for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
             {
                 ticket.AddItem(dgvProductosGuia.Rows[i].Cells["Cantidad"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Codigo"].Value.ToString());
                       
             }*/

            ticket.AddSubHeaderLine("PESO BRUTO: " +   dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["PesoBruto"].ToString() + "                                   ");
            ticket.HeaderImage = Resources.Codigo_QR;
            ticket.AddFooterLine("      " + dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["CodigoHash"].ToString() );
            ticket.AddFooterLine("                                   ");
            ticket.AddFooterLine("        ** VIAJA CON CUIDADO **");

            if (Utilitario.Instancia.SesionUsuario.Equals("SRUIZ") || Utilitario.Instancia.SesionUsuario.Equals("TDELGAD") || Utilitario.Instancia.SesionUsuario.Equals("LGRADOS"))
            { ticket.copias = 2; }

            int Encontrado = 0;

            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (printer == "POS-80-Series")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                    break;
                }

                if (printer == "POS-80-Series (1)")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                    break;
                }

                if (printer == "POS-80-Series (2)")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                    break;
                }
            }

            if (Encontrado == 0) { ticket.PrintTicket(printDoc.PrinterSettings.PrinterName); }
        }

        private void nuevaGuiaTransportistaToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void solicitarCambioDeDatosEnGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmListaSolicitudDeCambios frmSolicitar = new frmListaSolicitudDeCambios();
                frmSolicitar.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }

        private void remplazarGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRemplazarGuias rg = new frmRemplazarGuias();
                rg.idViaje = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("idViaje"));
                rg.Viaje = Convert.ToString(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("Viaje"));
                rg.Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("SerieGuia"));
                rg.Numero = Convert.ToString(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("NumeroGuia"));
                rg.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            btnCancelar.Visible = true;
            btnRecibidos.Visible = false;
            btnBuscar.Enabled = false;
            dgvRecibidos.Dock = DockStyle.Fill;
            dgvRecibidos.Visible = true;
            listarguiaspendietesderecibir();
            dgvRecibidosVista.SelectAll();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            btnBuscar.Enabled = true;
            btnRecibidos.Enabled = true;
            btnCancelar.Visible = false;
            btnRecibidos.Visible = true;
            dgvRecibidos.Dock = DockStyle.None;
            dgvRecibidos.Visible = false;
            dgvRecibidos.DataSource = null;
        }

        public void listarguiaspendietesderecibir()
        {
            dgvRecibidos.DataSource = null;
            this.Cursor = Cursors.WaitCursor;
            DataTable dtLista = clsOperacionesBL.Instancia.ReportesApp_ListarGuiasElectronicasRecepcionadas("T", dtpFechaInicio.Text, dtpFechaFin.Text, cbxSerieFiltro.Text, txtNumeroFiltro.Text, chkEstadoGuia.Checked, rbtAprobado.Checked, rbtRevertido.Checked, rbtRechazado.Checked, txtviaje.Text, txtClienteFiltro.Text);
            dgvRecibidosVista.OptionsSelection.MultiSelect = true;
            dgvRecibidosVista.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            if (dtLista != null)
            {
                if (dtLista.Rows.Count > 0)
                {
                    dgvRecibidos.DataSource = dtLista;

                    dgvRecibidosVista.BestFitColumns();
                    /* dgvRecibidosVista.Columns["FechaInicio_Traslado"].Width = 80;
                     dgvRecibidosVista.Columns["NumeroDocIdentidad_Emisor"].Width = 80;
                     dgvRecibidosVista.Columns["NumeroDocIdentidad_Rem"].Width = 80;
                     dgvRecibidosVista.Columns["NumeroDocIdentidad_Dest"].Width = 80;*/
                    dgvRecibidosVista.RefreshData();

                }
                else { dgvRecibidos.DataSource = null; }
                
                this.Cursor = Cursors.Default;
            }
        }

        private void confirmarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvRecibidosVista.RowCount > 0)
            {
                DataTable dtConfirmar = new DataTable();
                dtConfirmar.Columns.Add("Serie", typeof(String));
                dtConfirmar.Columns.Add("Numero", typeof(String));
                string xml;
                int[] SelectedRowHandles = dgvRecibidosVista.GetSelectedRows();

                for (int i = 0; i < dgvRecibidosVista.SelectedRowsCount; i++)
                { dtConfirmar.Rows.Add(dgvRecibidosVista.GetDataRow(SelectedRowHandles[i])["SerieGuia"].ToString(), dgvRecibidosVista.GetDataRow(SelectedRowHandles[i])["NumeroGuia"].ToString()); }

                xml = Utilitario.Instancia.DatatableToXml(dtConfirmar);

                try
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConfirmarDesconfirmar(1, xml))
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        private void desconfirmarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRecibidosVista.RowCount > 0)
            {
                DataTable dtConfirmar = new DataTable();
                dtConfirmar.Columns.Add("Serie", typeof(String));
                dtConfirmar.Columns.Add("Numero", typeof(String));
                string xml;
                int[] SelectedRowHandles = dgvRecibidosVista.GetSelectedRows();

                for (int i = 0; i < dgvRecibidosVista.SelectedRowsCount; i++)
                { dtConfirmar.Rows.Add(dgvRecibidosVista.GetDataRow(SelectedRowHandles[i])["SerieGuia"].ToString(), dgvRecibidosVista.GetDataRow(SelectedRowHandles[i])["NumeroGuia"].ToString()); }

                xml = Utilitario.Instancia.DatatableToXml(dtConfirmar);

                try
                {
                    if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConfirmarDesconfirmar(0, xml))
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else
                    { MessageBox.Show(Utilitario.Instancia.Advertencia, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }



        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.SesionUsuario.usuario == "BVIGO" || Utilitario.Instancia.SesionUsuario.usuario == "SESCOBEDO")
                {
                    DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ReporteGuiasRRHH(dtpFechaInicio.Text, dtpFechaFin.Text);
                    dgvRecibidos.DataSource = dt;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Guias" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvRecibidos.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Guias" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgListaGuiasTransportista.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void actualizarViajeConGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idviaje = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("idViaje"));
                string viaje = Convert.ToString(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("Viaje"));
                string Serie = Convert.ToString(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("SerieGuia"));
                string Numero = Convert.ToString(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("NumeroGuia"));
                int LineaOT = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("LineaOT"));
                string Ticket = Convert.ToString(dgvListaGuiaTraspExpressVista.GetFocusedRowCellValue("NroTicketProgramacion"));

                if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_VincularGuiasNoEnlazadas(idviaje, viaje, Serie, Numero, LineaOT, Ticket))
                { MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else
                { MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }


        private void ImprimirMasivo(int[] total, int i)
        {
            ServiceGRT_QA.ene_ConsultarRI consultaRI = new ServiceGRT_QA.ene_ConsultarRI();
            consultaRI.at_NumeroDocumentoIdentidad = Convert.ToString(dgvRecibidosVista.GetDataRow(total[i])["NumeroDocIdentidad_Emisor"]);
            consultaRI.ent_Comprobante = new ServiceGRT_QA.en_ComprobanteConsultarRI();
            consultaRI.ent_Comprobante.at_Serie = Convert.ToString(dgvRecibidosVista.GetDataRow(total[i])["Serie"]);
            consultaRI.ent_Comprobante.at_Numero = Convert.ToInt32(dgvRecibidosVista.GetDataRow(total[i])["Numero"]);
            resultadoRiTransportista = new ServiceGRT_QA.ens_ResultadoRI();
            requestTransportista = new ServiceGRT_QA.ServicioGuiaRemisionTransportistaClient();
            resultadoRiTransportista = requestTransportista.ConsultarRI_GRT(consultaRI);

            ServiceGRT_QA.ene_ConsultarXML consultarXML = new ServiceGRT_QA.ene_ConsultarXML();
            consultarXML.ent_ComprobanteConsultarXML = new ServiceGRT_QA.en_ComprobanteConsultarXML();
            consultarXML.at_NumeroDocumentoIdentidad = Convert.ToString(dgvRecibidosVista.GetDataRow(total[i])["NumeroDocIdentidad_Emisor"]);
            consultarXML.ent_ComprobanteConsultarXML.at_Serie = Convert.ToString(dgvRecibidosVista.GetDataRow(total[i])["Serie"]);
            consultarXML.ent_ComprobanteConsultarXML.at_Numero = Convert.ToInt32(dgvRecibidosVista.GetDataRow(total[i])["Numero"]);
            ServiceGRT_QA.ens_ConsultarXML responseXML = new ServiceGRT_QA.ens_ConsultarXML();
            responseXML = requestTransportista.ConsultarXMLGRT(consultarXML);

            if (resultadoRiTransportista.ent_Resultado != null)
            {
                Stream stream = new MemoryStream(resultadoRiTransportista.ent_Resultado.at_ArchivoRI);
                PdfViewer pdfViewer1 = new PdfViewer();

                pdfViewer1.LoadDocument(stream);
                pdfViewer1.SaveDocument(@"\\192.168.4.237\ReportesTranspesa2\ImportExcel\GuiasPDF\" + Convert.ToString(dgvRecibidosVista.GetDataRow(total[i])["Serie"]) + "-" + Convert.ToString(dgvRecibidosVista.GetDataRow(total[i])["Numero"]) + ".pdf");
                pdfViewer1.CloseDocument();

                Stream streamXML = new MemoryStream(responseXML.ent_ResultadoXML.at_XML);
            }

            if (responseXML.ent_ResultadoXML != null)
            {
                XmlDocument document = new XmlDocument();
                MemoryStream ms = new MemoryStream(responseXML.ent_ResultadoXML.at_XML);
                document.Load(ms);
                document.Save(@"\\192.168.4.237\ReportesTranspesa2\ImportExcel\GuiasPDF\" + responseXML.ent_ResultadoXML.at_NombreXML.ToString() + ".xml");
            }
        }


        private void reimprimirMasivoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                int[] SelectedRowHandles = dgvListaGuiaTraspExpressVista.GetSelectedRows();

                for (int i = 0; i < dgvListaGuiaTraspExpressVista.SelectedRowsCount; i++)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idEmpresaGrupo"]);
                    int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idCliente"]);
                    int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idOT"]);
                    string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["TipoGuia"]);
                    int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idGuiaElectronica"]);

                    if (clsOperacionesBL.Instancia.Reportesapp_Operaciones_Reimprimir_GuiaElectronica(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica))
                    {
                        ImprimirMasivo(SelectedRowHandles, i);
                        //CargarGuiaEnPDFTransportista();
                    }
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void descargarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int[] SelectedRowHandles = dgvRecibidosVista.GetSelectedRows();

                for (int i = 0; i < dgvRecibidosVista.SelectedRowsCount; i++)
                {
                    this.Cursor = Cursors.WaitCursor;
                    /*string empresa = Convert.ToString(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idEmpresaGrupo"]);
                    int idCliente = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idCliente"]);
                    int idOt = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idOT"]);
                    string TipoGuia = Convert.ToString(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["TipoGuia"]);
                    int idGuiaElectronica = Convert.ToInt32(dgvListaGuiaTraspExpressVista.GetDataRow(SelectedRowHandles[i])["idGuiaElectronica"]);
                     */



                    ImprimirMasivo(SelectedRowHandles, i);
                    //CargarGuiaEnPDFTransportista();




                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
