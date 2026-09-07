using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using AccesoDatos;

namespace Negocio
{
    public class clsDetalleReporteUsuarioBL
    {
        private clsDetalleReporteUsuarioBL()
        {

        }

        private readonly static clsDetalleReporteUsuarioBL instancia = new clsDetalleReporteUsuarioBL();

        public static clsDetalleReporteUsuarioBL Instancia
        {
            get { return instancia; }
        }

        public String registrar_DetalleReporte(List<clsDetalleUsuarioReporte> lista)
        {
            return clsDetalleUsuarioReporteDAO.Instancia.registrar_DetalleReporte(lista);
        }

        public List<clsDetalleUsuarioReporte> consulta_DetalleReporte_Por_Usuario(String usuario)
        {
            return clsDetalleUsuarioReporteDAO.Instancia.consulta_DetalleReporte_Por_Usuario(usuario);
        }

        public List<clsDetalleUsuarioReporte> consulta_Reporte_Todos()
        {
            return clsDetalleUsuarioReporteDAO.Instancia.consulta_Reporte_Todos();
        }

        // sem pasó por aqui
        public void consulta_DetalleReporte_Por_Usuario_PermisosFormlario(string usuario)
        {
            clsDetalleUsuarioReporteDAO.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(usuario);
        }
    }
}
