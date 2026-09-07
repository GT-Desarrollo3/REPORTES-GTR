using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class clsDetalleUsuarioReporte
    {
        public clsDetalleUsuarioReporte()
        {
            this.objReporte = new clsReporte();
            this.objUsuario = new clsUsuario();
        }

        public clsUsuario objUsuario { get; set; }
        public clsReporte objReporte { get; set; }
        public Boolean estado { get; set; }
    }
}
