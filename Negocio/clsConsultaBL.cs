using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsConsultaBL
    {
        private readonly static clsConsultaBL instancia = new clsConsultaBL();

        public static clsConsultaBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetCompañias()
        {
            return clsConsultaDAO.Instancia.GetDataCompañias();
        }

        public DataTable GetUnidadesNegocio()
        {
            return clsConsultaDAO.Instancia.GetDataUnidadesNegocio();
        }

        public DataTable GetUnidadesReplicacion()
        {
            return clsConsultaDAO.Instancia.GetDataUnidadesReplicacion();
        }

        public DataTable GetConceptos()
        {
            return clsConsultaDAO.Instancia.GetDataConceptos();
        }

        public DataTable GetPersona(string nombre)
        {
            return clsConsultaDAO.Instancia.GetDataPersona(nombre);
        }

        public DataTable GetDataPersonaOperaciones(string nombre)
        {
            return clsConsultaDAO.Instancia.GetDataPersonaOperaciones(nombre);
        }
        

        public DataTable GetPersona2(string IDPersona)
        {
            return clsConsultaDAO.Instancia.GetDataPersona2(IDPersona);
        }

        public DataTable GetPersona3(string IDPersona)
        {
            return clsConsultaDAO.Instancia.GetDataPersona3(IDPersona);
        }

        public DataTable GetPersona4(string DNI)
        {
            return clsConsultaDAO.Instancia.GetDataPersona4(DNI);
        }

        public DataTable GetPersona5(string nombre)
        {
            return clsConsultaDAO.Instancia.GetDataPersona5(nombre);
        }


        public DataTable GetCentroCosto(string nombre)
        {
            return clsConsultaDAO.Instancia.GetDataCentroCostos(nombre);
        }

        public DataTable GetConductores(string nombre)
        {
            return clsConsultaDAO.Instancia.GetDataConductores(nombre);
        }

        public DataTable GetUnidades(string Placa)
        {
            return clsConsultaDAO.Instancia.GetUnidades(Placa);
        }
        public DataTable GetUnidadesMaquinarias(string Placa)
        {
            return clsConsultaDAO.Instancia.GetUnidadesMaquinarias(Placa);
        }

        public DataTable GetProductos(string Descripcion)
        {
            return clsConsultaDAO.Instancia.GetProductos(Descripcion);
        }
        
             public DataTable GetRutas(string Descripcion)
        {
            return clsConsultaDAO.Instancia.GetRutas(Descripcion);
        }

        //public DataTable Llenar_ControlesPreViajes()
        //{
        //    return clsOperacionesDAO.Instancia.Llenar_ControlesPreViajes();
        //}

        public DataTable GetRazonSocial(string Razonsocial)
        {
            return clsConsultaDAO.Instancia.GetRazonSocial(Razonsocial);
        }
        public DataTable GetRutasActivas(string Descripcion)
        {
            return clsConsultaDAO.Instancia.GetRutasActivas(Descripcion);
        }

        public DataTable GetUnidadesActivas(string Unidad)
        {
            return clsConsultaDAO.Instancia.GetUnidadesActivas(Unidad);
        }
        public DataTable GetEmpleado(string nombre)
        {
            return clsConsultaDAO.Instancia.GetDataEmpleado(nombre);
        }



        public bool ReportesApp_Permisos_CopiarPermiso(string Nombre, string UsuarioOrigen, int idPersonaOrigen, string NombrePersonaNuevo, int idPersonaNuevo)
        {
            return clsConsultaDAO.Instancia.ReportesApp_Permisos_CopiarPermiso(Nombre, UsuarioOrigen, idPersonaOrigen, NombrePersonaNuevo, idPersonaNuevo);
        }

        public DataTable ReportesApp_CargarPersonalSinUsuario()
        {
            return clsConsultaDAO.Instancia.ReportesApp_CargarPersonalSinUsuario();
        }
    }
}
