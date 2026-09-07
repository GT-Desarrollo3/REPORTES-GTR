using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;
using Entidades;

namespace Negocio
{
    public class clsPersonaBL
    {
        private clsPersonaBL()
        {

        }

        private readonly static clsPersonaBL instancia = new clsPersonaBL();

        public static clsPersonaBL Instancia
        {
            get { return instancia; }
        }

        public List<clsEmpleado> ConsultarEmpleadosActivos()
        {
            return clsPersonaDAO.Instancia.ConsultarEmpleadosActivos();
        }

        public List<clsEmpleado> consultarEmpleadosIngresantes_PorMesAnio(String mes, String anio)
        {
            return clsPersonaDAO.Instancia.consultarEmpleadosIngresantes_PorMesAnio(mes, anio);
        }

        public List<clsEmpleado> consultarEmpleadosCesantes_PorMesAnio(String mes, String anio)
        {
            return clsPersonaDAO.Instancia.consultarEmpleadosCesantes_PorMesAnio(mes, anio);
        }
        public DataTable GetProveedor(string param)
        {
            return clsPersonaDAO.Instancia.GetProveedor(param);
        }
    }
}
