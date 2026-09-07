using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsGerenciaBL
    {
        private readonly static clsGerenciaBL instancia = new clsGerenciaBL();

        public static clsGerenciaBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetRetrasos(string ini, string fin, int crit,int retr,int tipo)
        {
            return clsGerenciaDAO.Instancia.GetRetrasos(ini, fin, crit, retr,tipo);
        }
    }
}
