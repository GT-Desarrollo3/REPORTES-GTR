using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsPreviajeTolvas
    {
        public int idPreviajeTolvas { get; set; }
        
        public int anio { get; set; }
        public int idClinte { get; set; }
        public string Cliente { get; set; }
        public string NroTicket { get; set; }
        public int IdOT { get; set; }
        public decimal Tarifa { get; set; }
        public int idRemitente { get; set; }
        public string Remitente { get; set; }
        public int idPartida { get; set; }
        public string DireccionPartida { get; set; }
        public int idDestinatario { get; set; }
        public string Destinatario { get; set; }
        public int idDestino { get; set; }
        public string DireccionDestino { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public string Producto { get; set; }
        public int idProducto { get; set; }
        public string Ruta { get; set; }
        public int idRuta { get; set; }
        public string UMUso { get ; set;}
        public string Distancia { get  ;set ;}
        public string Tiempo { get ; set;}
        public string xmlPlacaConductor { get; set; }
        public int TipoOperacion { get; set; }
        public string Estado { get ; set;}
        public string TipoProceso { get; set; }
        public string Motonave { get; set; }
        public decimal Tonelaje { get; set; }

    }
}
