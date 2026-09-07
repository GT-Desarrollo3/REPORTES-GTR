using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsGRR
    {


        public string compania { get; set; }
        public int idproveedor { get; set; }
        public int idcliente { get; set; }
        public string tracto { get; set; }
        public string idtracto { get; set; }
        public string carreta { get; set; }
        public string idcarreta { get; set; }
        public int idconductor { get; set; }
        public string conductor { get; set; }
        public string TipoGuia { get; set; }
        public int idGuiaElectronica { get; set; }
        public string TipoViaje { get; set; }
        public int idGuiaSpring { get; set; }
        public string Cliente { get; set; }
        public string correoCliente { get; set; }
        public int idDocumentoRelacion { get; set; }
        public int idIndicadoresServicio { get; set; }
        public int idConductoresGuia { get; set; }
        public int idProductosTraslado { get; set; }
        public int impreso { get; set; }
        public int idRemitente { get; set; }
        public int idDestinatario { get; set; }
        public int checkTercero { get; set; }



        //************************** DATOS GUIA TCI ****************************

        
        public bool entGRR_ControlOtorgamiento_Estado_M { get; set; } // Indica si el otorgamiento será manual o automático //False: Manual True: Automático
        public string entGRR_Emisor_NumroDocumentoIdentidad_M { get; set; }
        public string entGRR_Emisor_RazonSocial_M { get; set; }
        public string entGRR_Emisor_NombreComercial { get; set; }
        public string entGRR_Emisor_NumeroMTC { get; set; }
        public string entGRR_Emisor_NumeroAutorizado { get; set; }
        public string entGRR_Emisor_CodigoAutorizado { get; set; }
        public string entGRR_Emisor_Telefono { get; set; }
        public string entGRR_Emisor_CorreoContacto { get; set; }
        public string entGRR_Emisor_SitioWeb { get; set; }
        public string entGRR_Emisor_Ubigeo_M { get; set; }
        public string entGRR_Emisor_DireccionDetallada { get; set; }
        public string entGRR_Emisor_Urbanizacion { get; set; }
        public string entGRR_Emisor_Provincia { get; set; }
        public string entGRR_Emisor_Departamento { get; set; }
        public string entGRR_Emisor_Distrito { get; set; }
        public string entGRR_Emisor_CodigoPais_M { get; set; }
        public string entGRR_Emisor_TipoComprobante { get; set; } // guia transportista o remitente
        public int entGRR_Remitente_Otorga_idCorreoPrincial_M { get; set; }
        public string entGRR_Remitente_Otorga_CorreoPrincial_M { get; set; }
        public string xml_entGRR_Remitente_Otorga_CorreoSecundario { get; set; }
        public string entGRR_Remitente_NumeroDocumentoIdentidad_M { get; set; }
        public string entGRR_Remitente_TipoDocumentoIdentidad_M { get; set; }
        public string entGRR_Remitente_RazonSocial_M { get; set; }
        public string entGRR_Destinatario_NumeroDocumentoIdentidad_M { get; set; }
        public string entGRR_Destinatario_TipoDocumentoIdentidad_M { get; set; }
        public string entGRR_Destinatario_RazonSocial_M { get; set; }
        public string entGRR_Transportista_TipoDocumentoIdentidad { get; set; }
        public string entGRR_Transportista_NumeroDocumentoIdentidad { get; set; }
        public string entGRR_Transportista_RazonSocial { get; set; }
        public string entGRR_Transportista_NroMTC { get; set; }
        public string entGRR_Proveedor_TipoDocumentoIdentidad { get; set; }
        public string entGRR_Proveedor_NumeroDocumentoIdentidad { get; set; }
        public string entGRR_Proveedor_RazonSocial { get; set; }
        public string entGRR_Generales_FechaEmision_M { get; set; }
        public string entGRR_Generales_HoraEmision_M { get; set; }
        public string entGRR_Generales_FechaIncioTraslado { get; set; }
        public string entGRR_Generales_Modalidad_M { get; set; } 
        public string entGRR_Generales_Serie_M { get; set; } // para transportista la serie empieza con la letra 'V'
        public int entGRR_Generales_Numero_M { get; set; } // se envia como entero ejemplo nro 40 , 1 ,200
        public string entGRR_Generales_Observacion { get; set; }
        public string entGRR_Generales_CodigoMotivo_M { get; set; }
        public string entGRR_Generales_IndicadorMotivo { get; set; }
        public string entGRR_Generales_DescripcionMotivo { get; set; }
       
        public string xml_entGRR_DocumentosRelacion { get; set; } // puedo no enviarlo , pero de no enviarlo tengo que llenar todo los items de los productos de la guia de remision remitente
        public string xml_entGRR_TipoServicio { get; set; }
        public string entGRR_PesoBruto_CodigoUnidadMedida_M { get; set; }  // KGM = KILOGRAMDO , TNE = TONELADAS - mas 4 digitos
        public decimal entGRR_PesoBruto_PesoTotal_M { get; set; } // logitud maxima de 12 enteros , 3 decimales
        public string entGRR_PesoBruto_DescripcionAdicional { get; set; }
        public string entGRR_PuntoPartida_Ubigeo_M { get; set; }
        public string entGRR_PuntoPartida_DireccionCompleta_M { get; set; }

        public string entGRR_PuntoPartida_NombreUbigeo { get; set; }
        public string entGRR_PuntoPartida_Urbanizacion { get; set; } // este campo no es necesario llenar
        public string entGRR_PuntoPartida_Provincia { get; set; } // este campo no es necesario llenar
        public string entGRR_PuntoPartida_Departamento { get; set; }// este campo no es necesario llenar
        public string entGRR_PuntoPartida_Distrito { get; set; }// este campo no es necesario llenar
        public decimal entGRR_PuntoPartida_Latitud { get; set; }// este campo no es necesario llenar 
        public decimal entGRR_PuntoPartida_Longitud { get; set; } // este campo no es necesario llenar
        public string entGRR_PuntoDestino_Ubigeo_M { get; set; }
        public string entGRR_PuntoDestino_DireccionCompleta_M { get; set; }
        public string entGRR_PuntoDestino_NombreUbigeo { get; set; }
        public string entGRR_PuntoDestino_Urbanizacion { get; set; } // este campo no es necesario llenar
        public string entGRR_PuntoDestino_Provincia { get; set; } // este campo no es necesario llenar
        public string entGRR_PuntoDestino_Departamento { get; set; }// este campo no es necesario llenar
        public string entGRR_PuntoDestino_Distrito { get; set; }// este campo no es necesario llenar
        public decimal entGRR_PuntoDestino_Latitud { get; set; }// este campo no es necesario llenar 
        public decimal entGRR_PuntoDestino_Longitud { get; set; } // este campo no es necesario llenar
        public string entGRR_Vehiculo_NumeroPlaca_M { get; set; }
        public string entGRR_Vehiculo_NumeroCarreta_M { get; set; }
        public string entGRR_Vehiculo_TarjetaCirculacion { get; set; }
        public string entGRR_Carreta_TarjetaCirculacion { get; set; }
        public string entGRR_Vehiculo_NumeroAutentificacion { get; set; }
        public string entGRR_Vehiculo_CodigoAutentificacion { get; set; }
        public string xml_entGRR_Productos_Bienes { get; set; }
        public string xml_entGRR_Conductor_M { get; set; }
        public entConductorR entConductor { get; set; }
        public string entGRR_GrupoInformacionAdicional_Titulo { get; set; }
        public string entGRR_GrupoInformacionAdicional_Etiqueta { get; set; }
        public string entGRR_GrupoInformacionAdicional_Valor { get; set; }
        public string entGRR_Autentificacion_Ruc { get; set; }
        public string entGRR_Autentificacion_Clave { get; set; }
        public string entGRR_Respuesta_XML_Archivo { get; set; }
        public string entGRR_Respuesta_Xml_CDR { get; set; }
        public string entGRR_Respuesta_Fecha_CDR { get; set; }
        public string entGRR_Respuesta_NombreGuia_XML { get; set; }
        public string entGRR_Respuesta_Fecha_XML { get; set; }
        public string entGRR_Respuesta_CodigoHash { get; set; }
        public string entGRR_Respuesta_FechaGeneracion { get; set; }
        public string entGRR_Respuesta_FechaReversion { get; set; }
        public string entGRR_Respuesta_FechaTransmision { get; set; } // representa la fecha en que el comprobante fue transmitiro a ePortal
        public string entGRR_Respuesta_FechaOtorgamiento { get; set; }
        public string entGRR_Respuesta_FechaLeido { get; set; }
        public byte[] entGRR_Respuesta_ArchivoPDF { get; set; }
        public string entGRR_Respuesta_NombreRI { get; set; }
        public string entGRR_Respuesta_FechaRI { get; set; }
        public int entGRR_Respuesta_NivelResultado { get; set; }
        public string entGRR_Respuesta_ConsultaIndividualEstado { get; set; }
        public string entGRR_Respuesta_MensajeResultado { get; set; }
        public string entGRR_Respuesta_CodigoMensaje { get; set; }
        public string entGRR_Respuesta_FechaRespuesta { get; set; }
        public string entGRR_Respuesta_Guardar_Sunat { get; set; }
        public string entGRR_Respuesta_EstadoGuardado { get; set; }
        public string entGRR_Respuesta_EstadoSunat { get; set; }
        public string CodReversion { get; set; }
        public string MotivoReversion { get; set; }
        public bool ConTransportista { get; set; }
        public bool ConProveedor { get; set; }
        public bool TipoTrasladoProgramado { get; set; }
        public bool Publico { get; set; }
        public bool Privado { get; set; }
        public bool ConVehiculo { get; set; }
        public string CodigoEstablecimientoOrigen { get; set; }
        public string CodigoEstablecimientoDestino { get; set; }
        public string NombreEstablecimientoOrigen { get; set; }
        public string NombreEstablecimientoDestino { get; set; }
        public int TipoOperacion { get; set; }
        public string entGRR_Respuesta_URL_GuiaSunat { get; set; }

    }

    public class entConductorR
    {
        public string entGRR_Conductor_TipoDocumentoIdentidad_M { get; set; }
        public string entGRR_Conductor_NumeroDocumentoIdentidad_M { get; set; }
        public string entGRR_Conductor_Licencia_M { get; set; }
        public string entGRR_Conductor_Nombres_M { get; set; }
        public string entGRR_Conductor_Apellidos_M { get; set; }

    }
}
