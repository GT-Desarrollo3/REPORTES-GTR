using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using AccesoDatos;

namespace Negocio
{
    public class clsScrabBL
    {
         private clsScrabBL()
        {

        }

        private readonly static clsScrabBL instancia = new clsScrabBL();
        
        public static clsScrabBL Instancia
        {
            get { return instancia; }
        }

        public List<clsScrab> consultar_scrab_porAnio(String anio, float totalFloat)
        {
            return clsScrabDAO.Instancia.consultar_scrab_porAnio(anio, totalFloat);
        }
    }
}
