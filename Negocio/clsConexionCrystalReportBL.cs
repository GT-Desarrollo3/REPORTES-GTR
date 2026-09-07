using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccesoDatos;

namespace Negocio
{
    public class clsConexionCrystalReportBL
    {
        

        private readonly static clsConexionCrystalReportBL instancia = new clsConexionCrystalReportBL();

        public static clsConexionCrystalReportBL Instancia
        {
            get { return instancia; }
        }

        public String clsConexionCrystalReportBL_Usuario()
        {
            return clsConexion.Funciones.valorUsuario;
        }
        public String clsConexionCrystalReportBL_Clave()
        {
            return clsConexion.Funciones.valorClave;
        }
        public String clsConexionCrystalReportBL_BaseDatos()
        {
            return clsConexion.Funciones.valorBaseDatos;
        }
        public String clsConexionCrystalReportBL_Servidor()
        {
            return clsConexion.Funciones.valorServidor;
        }
      
        //public void conectarCrystalReport(ReportDocument oRpt)
        //{
        //    clsConexionCrystalReportDAO.Instancia.conectarCrystalReport(oRpt);
        //}
    }
}
