using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsGRT
    {

        /************** DATOS FORMULARIO ************/

        public string compania { get; set; }
        public int item { get; set; }
        public int idproveedor { get; set; }
        public int idcliente { get; set; }
        public int idOT  { get; set; }
        public int LineaOT { get; set; }
        public int idviaje { get; set; }
        public string viaje  { get; set; }
        public string tracto { get; set; }
        public string idtracto { get; set; }
        public int idconductor { get; set; }
        public string conductor { get; set; }
        public string estadoviaje { get; set; }
        public int idRuta { get; set; }
        public string ruta { get; set; }
        public string idCarreta { get; set; }
        public string carreta { get; set; }
        public string destino { get; set; }
        public string FechaProgramacion { get; set; }
        public string CodigoProgramacion { get; set; }
        public int idProgramacion { get; set; }
        public string TipoGuia {get ; set;}
        public bool TipoTrasladoTotaldeBienes { get; set; }
        public bool TipoTrasladoProgramado { get; set; }
        public bool TipoTransoporteSubcontratado { get; set; }
        public bool TipoFleteTercero { get; set; }
        public int idTipoProgramacion { get; set; }
        public string estadoProgramacion { get; set; }
        public int idEstadoProgramacion { get; set; }
        public int idGuiaElectronica { get; set; }
        public string TipoViaje { get; set; }
        public int idGuiaSpring { get; set; }
        public string Cliente { get; set; }
        public string correoCliente { get; set; }
        public int idDocumentoRelacion { get ; set; }
        public int idIndicadoresServicio { get; set; }
        public int idConductoresGuia { get; set; }
        public int idProductosTraslado { get; set; }
        public int impreso { get; set; }
        public int lineaConsolidado { get; set; }
        public int AnioProgramacion { get; set; }
        public bool esConsolidado  {get ; set;}
        public string pesoGuia { get; set;  }

        //************************** DATOS GUIA TCI ****************************

        public bool entGRT_ControlOtorgamiento_Estado_M { get; set; } // Indica si el otorgamiento será manual o automático //False: Manual True: Automático
        public string entGRT_Emisor_NumroDocumentoIdentidad_M { get; set; }
        public string entGRT_Emisor_RazonSocial_M { get; set; }
        public string entGRT_Emisor_NombreComercial { get; set; }
        public string entGRT_Emisor_NumeroMTC { get; set; }
        public string entGRT_Emisor_NumeroAutorizado { get; set; }
        public string entGRT_Emisor_CodigoAutorizado { get; set; }
        public string entGRT_Emisor_Telefono { get; set; }
        public string entGRT_Emisor_CorreoContacto { get; set; }
        public string entGRT_Emisor_SitioWeb{ get; set; }
        public string entGRT_Emisor_Ubigeo_M { get; set; }
        public string entGRT_Emisor_DireccionDetallada { get; set; }
        public string entGRT_Emisor_Urbanizacion { get; set; }
        public string entGRT_Emisor_Provincia { get; set; }
        public string entGRT_Emisor_Departamento { get; set; }
        public string entGRT_Emisor_Distrito { get; set; }
        public string entGRT_Emisor_CodigoPais_M { get; set; }
        public string entGRT_Emisor_TipoComprobante { get; set; } // guia transportista o remitente
        public int entGRT_Remitente_Otorga_idCorreoPrincial_M { get; set; }
        public string entGRT_Remitente_Otorga_CorreoPrincial_M { get; set; }

        public int idRemitente;
        public string xml_entGRT_Remitente_Otorga_CorreoSecundario { get; set; }
        public string entGRT_Remitente_NumeroDocumentoIdentidad_M { get; set; }
        public string entGRT_Remitente_TipoDocumentoIdentidad_M { get; set; }
        public string entGRT_Remitente_RazonSocial_M { get; set; }
        public int idDestinatario { get; set; }
        public string entGRT_Destinatario_NumeroDocumentoIdentidad_M { get; set; }
        public string entGRT_Destinatario_TipoDocumentoIdentidad_M { get; set; }
        public string entGRT_Destinatario_RazonSocial_M { get; set; }
        public string entGRT_Contratista_TipoDocumentoIdentidad { get; set; }
        public string entGRT_Contratista_NumeroDocumentoIdentidad { get; set; }
        public string entGRT_Contratista_RazonSocial{ get; set; }
        public string entGRT_SubContratista_TipoDocumentoIdentidad { get; set; }
        public string entGRT_SubContratista_NumeroDocumentoIdentidad { get; set; }
        public string entGRT_SubContratista_RazonSocial { get; set; }
        public string entGRT_Generales_FechaEmision_M { get; set; }
        public string entGRT_Generales_HoraEmision_M { get; set; }
        public string entGRT_Generales_FechaIncioTraslado_M { get; set; }
        public string entGRT_Generales_Serie_M { get; set; } // para transportista la serie empieza con la letra 'V'
        public int entGRT_Generales_Numero_M { get; set; } // se envia como entero ejemplo nro 40 , 1 ,200
        public string entGRT_Generales_Observacion { get; set; }
        public string xml_entGRT_DocumentosRelacion { get; set; } // puedo no enviarlo , pero de no enviarlo tengo que llenar todo los items de los productos de la guia de remision remitente
        public string xml_entGRT_TipoServicio { get; set; }
        public string entGRT_PesoBruto_CodigoUnidadMedida_M { get; set; }  // KGM = KILOGRAMDO , TNE = TONELADAS - mas 4 digitos
        public decimal entGTR_PesoBruto_PesoTotal_M { get; set; } // logitud maxima de 12 enteros , 3 decimales
        public string entGRT_PesoBruto_DescripcionAdicional { get; set; }
        public string entGRT_PuntoPartida_Ubigeo_M { get; set; }
        public string entGRT_PuntoPartida_Nombre_Ubigeo_M { get; set; }
        public string entGRT_PuntoPartida_DireccionCompleta_M { get; set; }
        public int entGRT_PuntoPartida_Direccion_Secuencia { get; set; }
        public string entGRT_PuntoPartida_Urbanizacion { get; set; } // este campo no es necesario llenar
        public string entGRT_PuntoPartida_Provincia { get; set; } // este campo no es necesario llenar
        public string entGRT_PuntoPartida_Departamento { get; set; }// este campo no es necesario llenar
        public string entGRT_PuntoPartida_Distrito { get; set; }// este campo no es necesario llenar
        public string entGRT_PuntoDestino_DireccionCompleta_M { get; set; }
        public decimal entGRT_PuntoPartida_Latitud { get; set; }// este campo no es necesario llenar 
        public decimal entGRT_PuntoPartida_Longitud { get; set; } // este campo no es necesario llenar
        public string entGRT_PuntoDestino_Ubigeo_M { get; set; }
        public string entGRT_PuntoDestino_Nombre_Ubigeo_M { get; set; }
        public int entGRT_PuntoDestino_Direccion_Secuencia { get; set; }
        public string entGRT_PuntoDestino_Urbanizacion { get; set; } // este campo no es necesario llenar
        public string entGRT_PuntoDestino_Provincia { get; set; } // este campo no es necesario llenar
        public string entGRT_PuntoDestino_Departamento { get; set; }// este campo no es necesario llenar
        public string entGRT_PuntoDestino_Distrito { get; set; }// este campo no es necesario llenar
        public decimal entGRT_PuntoDestino_Latitud { get; set; }// este campo no es necesario llenar 
        public decimal entGRT_PuntoDestino_Longitud { get; set; } // este campo no es necesario llenar
        public string entGRT_Vehiculo_NumeroPlaca_M {get ; set; }
        public string entGRT_Vehiculo_TarjetaCirculacion { get; set; }
        public string entGRT_Vehiculo_TarjetaCirculacionCarreta { get; set; }
        public string entGRT_Vehiculo_NumeroAutentificacion { get; set; }
        public string entGRT_Vehiculo_CodigoAutentificacion { get; set; }
        public string xml_entGRT_Productos_Bienes { get; set; } 
        public string xml_entGTR_Conductor_M { get; set;}
        public entConductor entConductor { get; set; }
        public string entGRT_GrupoInformacionAdicional_Titulo { get; set; }
        public string entGRT_GrupoInformacionAdicional_Etiqueta { get; set; }
        public string entGRT_GrupoInformacionAdicional_Valor { get; set; }
        public string entGRT_Autentificacion_Ruc { get; set; }
        public string entGRT_Autentificacion_Clave { get; set; }
        public string entGRT_Respuesta_XML_Archivo { get; set; }
        public  string entGRT_Respuesta_Xml_CDR { get; set; }
        public string entGRT_Respuesta_Fecha_CDR { get; set; }
        public string entGRT_Respuesta_NombreGuia_XML { get; set; }
        public string entGRT_Respuesta_Fecha_XML { get; set; }
        public string entGRT_Respuesta_CodigoHash { get; set; }
        public string entGRT_Respuesta_FechaGeneracion { get; set; }
        public string entGRT_Respuesta_FechaReversion { get; set; }
        public string entGRT_Respuesta_FechaTransmision { get; set; } // representa la fecha en que el comprobante fue transmitiro a ePortal
        public string entGRT_Respuesta_FechaOtorgamiento { get; set; }
        public string entGRT_Respuesta_FechaLeido { get; set; }
        public byte[] entGRT_Respuesta_ArchivoPDF { get; set; }
        public string entGRT_Respuesta_NombreRI { get; set; }
        public string entGRT_Respuesta_FechaRI { get; set; }
        public int entGRT_Respuesta_NivelResultado { get; set; }
        public string entGRT_Respuesta_ConsultaIndividualEstado { get; set; }
        public string entGRT_Respuesta_MensajeResultado { get; set; }
        public string entGRT_Respuesta_CodigoMensaje { get; set; }
        public string entGRT_Respuesta_FechaRespuesta { get; set; }
        public string entGRT_Respuesta_Guardar_Sunat { get; set; }
        public string entGRT_Respuesta_EstadoGuardado { get; set; }
        public string entGRT_Respuesta_EstadoSunat { get; set; }
        public string CodReversion { get; set; }
        public string MotivoReversion { get; set; }
        public int TipoOperacion { get; set; }
        public int CompletadoViaje { get; set; }
        public string entGRT_Respuesta_URL_GuiaSunat { get; set; }
        public string TipoEvento { get; set; }
        public string GuiaEventoTransportista { get; set; }
        public string GuiaEventoRemitente { get; set; }
    }

    public class RespuestaSUNATInformacion
    {
        public string entGRT_NumeroRespuesta { get; set; }
        public string entGRT_CodigoRespuesta { get; set; }
        public string entGRT_DescripcionRespuesta { get; set; }
        public string entGRT_FechaSunat { get; set; }

    }

  /*  public class TipoServicio 
    {
        public string entGRT_Servicio_01_TrasbordoProgramado_M { get; set; }
        public string entGRT_Servicio_02_RetornoEnvasesVacios_M { get; set; }
        public string entGRT_Servicio_03_RetornoVehiculoVacio_M { get; set; }
        public string entGRT_Servicio_04_SubContratado_M { get; set; }
        public string entGRT_Servicio_05_FleteRemitente_M { get; set; }
        public string entGRT_Servicio_06_SubContratista_M { get; set; }
        public string entGRT_Servicio_07_FleteTercero_M { get; set; }
        public string entGRT_Servicio_08_TrasladoTotalBienes_M { get; set; }
    }
    */
        


    public class entConductor
    {
        public string entGRT_Conductor_TipoDocumentoIdentidad_M { get; set; }
        public string entGRT_Conductor_NumeroDocumentoIdentidad_M { get; set; }
        public string entGRT_Conductor_Licencia_M { get; set; }
        public string entGRT_Conductor_Nombres_M { get; set; }
        public string entGRT_Conductor_Apellidos_M { get; set; }

    }
   /* public class entDocumentosRelacion
    {
        public string entGRT_DocRelacion_NumeroComprobante { get; set; }
        public string entGRT_Generales_TipoComprobante { get; set; }
        public string entGRT_Generales_NombreComprobante { get; set; }
        public string entGRT_Generales_NumeroDocumentoIdentidad { get; set; }
        public string entGRT_Generales_TipoDocumentoIdentidad { get; set; }

    }*/

    public class entProductos
    {
        public decimal entGTR_ProductoBienes_Cantidad { get; set; }
        public string entGTR_ProductoBienes_UnidadMedida { get; set; }
        public string entGTR_ProductoBienes_Descripcion { get; set; }
        public string entGTR_ProductoBienes_Codigo { get; set; }
        public string entGTR_ProductoBienes_CodigoSunat { get; set; }
        public string entGTR_ProductoBienes_CodigoGTIN { get; set; }
        public string entGTR_ProductoBienes_EstructuraGTIN { get; set; }
        public string entGTR_ProductoBienes_PartidaAranceleria { get; set; }
        public string entGTR_ProductoBienes_IndicadorNormalizado { get; set; } // indicador de bien normalizado Numeracion de la DAM "0" FALSO , 1 "VERDADERO"

        public int idProducto { get; set; }
    }
}
