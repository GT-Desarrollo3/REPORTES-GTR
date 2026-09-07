using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsAreaBL
    {
        private clsAreaBL()
        {

        }

        private readonly static clsAreaBL instancia = new clsAreaBL();

        public static clsAreaBL Instancia
        {
            get { return instancia; }
        }

        public List<clsArea> consulta_Area_Activas()
        {
            return clsAreaDAO.Instancia.consulta_Area_Activas();
        }

        public DataTable AreasPorCodigo(string Areas)
        {
            return clsAreaDAO.Instancia.AreasPorCodigo(Areas);
        }
    }
}
