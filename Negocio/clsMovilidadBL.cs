using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using AccesoDatos;

namespace Negocio
{
    public class clsMovilidadBL
    {
        private clsMovilidadBL()
        {

        }

        private readonly static clsMovilidadBL instancia = new clsMovilidadBL();

        public static clsMovilidadBL Instancia
        {
            get { return instancia; }
        }

        public List<clsMovilidad> consultar_movilidad_porBeneficiario(String periodo, String beneficiario)
        {
            return clsMovilidadDAO.Instancia.consultar_movilidad_porBeneficiario(periodo,beneficiario);
        }

        public List<clsMovilidad> consultar_movilidad(String periodo)
        {
            return clsMovilidadDAO.Instancia.consultar_movilidad(periodo);
        }
    }
}
