using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Comun;

namespace AccesoDatos
{
    public class clsUsuarioDAO
    {
        private clsUsuarioDAO()
        {

        }

        private readonly static clsUsuarioDAO instancia = new clsUsuarioDAO();

        public static clsUsuarioDAO Instancia
        {
            get { return instancia; }
        }

        public List<clsUsuario> consulta_Usuarios_Activos()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Usuario_ConsultaActivos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader lector = comando.ExecuteReader();
                List<clsUsuario> coleccion = new List<clsUsuario>();

                while (lector.Read())
                {
                    clsUsuario obj = new clsUsuario();

                    obj.usuario = lector.GetString(0).ToString();
                    obj.nombres = lector.GetString(1).ToString();

                    coleccion.Add(obj);
                }

                conexion.Close();
                conexion.Dispose();


                if (coleccion.Count <= 0)
                    return null;

                return coleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean validar_usuario(String usuario, String password)
        {
            try
            {
                Boolean respuesta = false;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Usuario_validar_Reporteador", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@usuario", SqlDbType.Char, 20).Value = usuario;
                comando.Parameters.Add("@password", SqlDbType.Char, 20).Value = password;
                SqlDataReader lector = comando.ExecuteReader();
                clsUsuario obj = null;

                if (lector.Read())
                {

                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(lector["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                    if (respuesta)
                    {
                        obj = new clsUsuario();
                        obj.usuario = lector.GetString(1).ToString().Trim();
                        obj.nombres = lector.GetString(2).ToString().Trim();
                        obj.idUsuario = lector.GetInt32(3);
                        Utilitario.Instancia.SesionUsuario = obj;
                    }


                }

                conexion.Close();
                conexion.Dispose();

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetUsuarios()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT LTRIM(RTRIM(U.Usuario)) AS 'USUARIO',LTRIM(RTRIM(U.NOMBRE)) AS 'EMPLEADO' , E.Empleado as 'idEmpleado'"  +
                "FROM Usuario U INNER JOIN EmpleadoMast E ON U.Usuario=E.CodigoUsuario WHERE U.Estado = 'A' AND E.Estado = 'A' and UsuarioPerfil = 'US' ";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetListaUsuariosModulo(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand("ReportesApp_Usuario_Modulo", conexion);
                comando.Parameters.Add(new SqlParameter("Usuario", Usuario));
                comando.CommandType = CommandType.StoredProcedure;
                comando.Connection = conexion;
                conexion.Open();

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetUsuariosActivos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand("ReportesApp_Usuario_Activos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Connection = conexion;
                conexion.Open();

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        // POR AQÍ PASÓ SEM
        public DataTable GetPermisosUsuariosActivos(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand("ReportesApp_Usuario_Permisos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@usuario", usuario));
                comando.Connection = conexion;
                conexion.Open();

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPermisos(string idusuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand("ReportesApp_ListarPermisos_Menu_Formulario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idusuario", idusuario));
                comando.Connection = conexion;
                conexion.Open();

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable UpdatePermisos(string idusuario, string idReportes, string UsuarioModifica)
        {

            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_UpdatePermisos_Usuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idUsuario", idusuario));
                comando.Parameters.Add(new SqlParameter("@idReportes", idReportes));
                comando.Parameters.Add(new SqlParameter("@UsuarioModifica", UsuarioModifica));
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GestListaUsuarios(string user)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Lista_Usuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@usuario", user));
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GestUsuariosDB(string users)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Lista_Usuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@usuario", users));
                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetRegistrarPermisosEspeciales(int idReporte, bool Estado, int Accion, int idPermisoEspecial, string NombrePermiso, string UsuarioModifica, bool HabilitarEstado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReporteApp_PermisosEspeciales_Master_Registra_Actualiza", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idReporte", idReporte));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Accion", Accion));
                comando.Parameters.Add(new SqlParameter("@idPermisoEspecial", idPermisoEspecial));
                comando.Parameters.Add(new SqlParameter("@NombrePermiso", NombrePermiso));
                comando.Parameters.Add(new SqlParameter("@UsuarioModifica", UsuarioModifica));
                //comando.Parameters.Add(new SqlParameter("@HabilitarEstado", HabilitarEstado));

                dtTemp.Load(comando.ExecuteReader());
                conexion.Close();
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Master_ListarPermisosEspeciales(int idReporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Master_ListarPermisosEspeciales", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idReporte", idReporte));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_ListarPermisosEspeciales(string idUsuario, int idReporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_PermisosEspeciales_Listar", conexion);
                comando.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                comando.Parameters.Add(new SqlParameter("@idReporte", idReporte));
                //comando.Parameters.Add(new SqlParameter("@idReporte", idReporte));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable Registrar_PermisosEspecialesXUsuario(int idReporte, int accion, string NombrePermiso, int idPermisoEspecial, string idUsuario, bool Activo, string UsuarioModifica)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Registrar_PermisosEspecialesXUsuario", conexion);
                comando.Parameters.Add(new SqlParameter("@idReporte", idReporte));
                comando.Parameters.Add(new SqlParameter("@NombrePermiso", NombrePermiso));
                comando.Parameters.Add(new SqlParameter("@accion", accion));
                comando.Parameters.Add(new SqlParameter("@idPermisoEspecial", idPermisoEspecial));
                comando.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                comando.Parameters.Add(new SqlParameter("@Activo", Activo));
                comando.Parameters.Add(new SqlParameter("@UsuarioModifica", UsuarioModifica));

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable VerificarExistenciaFormularioPorUsuario(string USUARIO, int idReporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Verificar_ExistenciaFormularioPorUsuario", conexion);
                comando.Parameters.Add(new SqlParameter("@idReporte", idReporte));
                comando.Parameters.Add(new SqlParameter("@idUsuario", USUARIO));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        // FIN 

    }
}
