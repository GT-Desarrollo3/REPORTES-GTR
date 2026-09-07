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
    public class clsUsuarioBL
    {
        private clsUsuarioBL()
        {

        }

        private readonly static clsUsuarioBL instancia = new clsUsuarioBL();

        public static clsUsuarioBL Instancia
        {
            get { return instancia; }
        }

        public List<clsUsuario> consulta_Usuarios_Activos()
        {
            return clsUsuarioDAO.Instancia.consulta_Usuarios_Activos();
        }

        public Boolean validar_usuario(String usuario, String password)
        {
            return clsUsuarioDAO.Instancia.validar_usuario(usuario, password);
        }

        public DataTable GetUsuarios()
        {
            return clsUsuarioDAO.Instancia.GetUsuarios();
        }

        public DataTable GetListaUsuariosModulo(string Usuario)
        {
            return clsUsuarioDAO.Instancia.GetListaUsuariosModulo(Usuario);
        }



        public DataTable GetUsuariosActivos()
        {
            return clsUsuarioDAO.Instancia.GetUsuariosActivos();
        }

        // SEM PASÓ POR AQUÍ
        public DataTable GetPermisosUsuariosActivos(string usuario)
        {
            return clsUsuarioDAO.Instancia.GetPermisosUsuariosActivos(usuario);
        }

        public DataTable GetPermisos(string usuario)
        {
            return clsUsuarioDAO.Instancia.GetPermisos(usuario);
        }
        public DataTable UpdatePermisos(string idusuario, string idReportes, string UsuarioModifica)
        {
            return clsUsuarioDAO.Instancia.UpdatePermisos(idusuario, idReportes, UsuarioModifica);
        }


        public DataTable GetListaUsuarios(string user)
        {
            return clsUsuarioDAO.Instancia.GestListaUsuarios(user);
        }

        public DataTable GestUsuariosDB(string users)
        {
            return clsUsuarioDAO.Instancia.GestUsuariosDB(users);
        }
        public DataTable RegistrarPermisosEspeciales(int idReporte, bool Estado, int Accion, int idPermisoEspecial, string NombrePermiso, string UsuarioModifica, bool HabilitarEstado)
        {
            return clsUsuarioDAO.Instancia.GetRegistrarPermisosEspeciales(idReporte, Estado, Accion, idPermisoEspecial, NombrePermiso, UsuarioModifica, HabilitarEstado);
        }

        public DataTable ReportesApp_Master_ListarPermisosEspeciales(int idReporte)
        {
            return clsUsuarioDAO.Instancia.ReportesApp_Master_ListarPermisosEspeciales(idReporte);
        }
        public DataTable ReportesApp_ListarPermisosEspeciales(string idUsuario, int idReporte)
        {
            return clsUsuarioDAO.Instancia.ReportesApp_ListarPermisosEspeciales(idUsuario, idReporte);
        }

        public DataTable Registrar_PermisosEspecialesXUsuario(int idReporte, int accion, string NombrePermiso, int idPermisoEspecial, string idUsuario, bool Activo, string UsuarioModifica)
        {
            return clsUsuarioDAO.Instancia.Registrar_PermisosEspecialesXUsuario(idReporte, accion, NombrePermiso, idPermisoEspecial, idUsuario, Activo, UsuarioModifica);

        }

        public DataTable GetVerificarExistenciaFormularioPorUsuario(string USUARIO, int idReporte)
        {
            return clsUsuarioDAO.Instancia.VerificarExistenciaFormularioPorUsuario(USUARIO, idReporte);
        }

    }
}
