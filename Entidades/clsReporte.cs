using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class clsReporte
    {
        public clsReporte()
        {
            this.objArea = new clsArea();
            this.objFormulario = new clsFormulario();
        }

        public int idReporte { get; set; }
        public String codigo { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public clsArea objArea { get; set; }
        public clsFormulario objFormulario { get; set; }
        public Boolean estado { get; set; }
        public String formula { get; set; }
    }
}
