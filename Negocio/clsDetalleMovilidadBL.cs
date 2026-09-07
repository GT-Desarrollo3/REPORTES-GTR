using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using AccesoDatos;
using System.Data;

namespace Negocio
{
    public class clsDetalleMovilidadBL
    {
        private clsDetalleMovilidadBL()
        {

        }

        private readonly static clsDetalleMovilidadBL instancia = new clsDetalleMovilidadBL();

        public static clsDetalleMovilidadBL Instancia
        {
            get { return instancia; }
        }

        //public DataTable consultar_DetalleMovilidad_PorPersona(String periodo, String beneficiario, String persona)
        //{
        //    return clsDetalleMovilidadDAO.Instancia.consultar_DetalleMovilidad_PorPersona(periodo, beneficiario, persona);
        //}

        public List<clsDetalleMovilidad> consultar_DetalleMovilidad_PorPersona(String periodo, String beneficiario, String persona)
        {
            return clsDetalleMovilidadDAO.Instancia.consultar_DetalleMovilidad_PorPersona(periodo, beneficiario, persona);
        }
    }
}
