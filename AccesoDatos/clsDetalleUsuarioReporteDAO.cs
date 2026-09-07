using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Entidades;
using Comun;

namespace AccesoDatos
{
    public class clsDetalleUsuarioReporteDAO
    {
        private clsDetalleUsuarioReporteDAO()
        {

        }

        private readonly static clsDetalleUsuarioReporteDAO instancia = new clsDetalleUsuarioReporteDAO();

        public static clsDetalleUsuarioReporteDAO Instancia
        {
            get { return instancia; }
        }

        public String registrar_DetalleReporte(List<clsDetalleUsuarioReporte> lista)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                foreach (clsDetalleUsuarioReporte item in lista)
                {
                    SqlCommand comando = new SqlCommand("ReportesApp_DetalleReporte_Registrar", conexion);
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@idUsuario", SqlDbType.Char, 20).Value = item.objUsuario.usuario;
                    comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = item.objReporte.idReporte;

                    comando.ExecuteNonQuery();
                }

                conexion.Close();
                conexion.Dispose();

                return "Registrado";
            }
            catch (Exception e)
            {
                return "ERROR: " + e.Message + e.StackTrace;
            }
        }

        // por aquí pasó sem 
        public List<clsDetalleUsuarioReporte> consulta_DetalleReporte_Por_Usuario(string usuario)
        {

            try
            {
                DataTable dtPermisos = new DataTable();

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
               // Utilitario.Instancia.TextoMenuServidor = "Servidor: " + clsConexion.Funciones.valorServidor + " - Base Datos: " + clsConexion.Funciones.valorBaseDatos;
               /* Utilitario.Instancia.ipServidor = clsConexion.Funciones.valorServidor;
                Utilitario.Instancia.BaseDatos = clsConexion.Funciones.valorBaseDatos;
                Utilitario.Instancia.UsuarioConexion = clsConexion.Funciones.valorUsuario;
                Utilitario.Instancia.ClaveConexion = clsConexion.Funciones.valorClave;*/
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_DetalleReporte_ConsultarPorUsuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@usuario", SqlDbType.NVarChar, 20).Value = usuario;

                SqlDataReader lector = comando.ExecuteReader();
                dtPermisos.Load(lector);

                List<clsDetalleUsuarioReporte> coleccion = new List<clsDetalleUsuarioReporte>();


                if (dtPermisos.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPermisos.Rows.Count; i++)
                    {
                        clsDetalleUsuarioReporte objclsDetalleReporte = new clsDetalleUsuarioReporte();
                        objclsDetalleReporte.objReporte.idReporte = Convert.ToInt32(dtPermisos.Rows[i]["idReporte"]);
                        objclsDetalleReporte.objReporte.nombre = Convert.ToString(dtPermisos.Rows[i]["nombre"]).Trim();
                        objclsDetalleReporte.objReporte.objArea.idArea = Convert.ToString(dtPermisos.Rows[i]["department"]).Trim();
                        objclsDetalleReporte.objReporte.objArea.descripcion = Convert.ToString(dtPermisos.Rows[i]["description"]).Trim();
                        objclsDetalleReporte.objUsuario.usuario = Convert.ToString(dtPermisos.Rows[i]["Usuario"]).Trim();
                        objclsDetalleReporte.objUsuario.nombres = Convert.ToString(dtPermisos.Rows[i]["Nombre"]).Trim();
                        objclsDetalleReporte.objReporte.objFormulario.idFormulario = Convert.ToInt32(dtPermisos.Rows[i]["idFormulario"]);
                        objclsDetalleReporte.objReporte.objFormulario.nombre = Convert.ToString(dtPermisos.Rows[i]["nombrefrm"]).Trim();
                        /*objclsDetalleReporte.objUsuario.nuevo = Convert.ToBoolean(dtPermisos.Rows[i]["Nuevo"]);
                        objclsDetalleReporte.objUsuario.editar = Convert.ToBoolean(dtPermisos.Rows[i]["Editar"]);
                        objclsDetalleReporte.objUsuario.leer = Convert.ToBoolean(dtPermisos.Rows[i]["Leer"]);
                        objclsDetalleReporte.objUsuario.anular = Convert.ToBoolean(dtPermisos.Rows[i]["Anular"]);
                        objclsDetalleReporte.objUsuario.permisoEspecial = Convert.ToBoolean(dtPermisos.Rows[i]["PermisoEspecial"]);*/
                        coleccion.Add(objclsDetalleReporte);
                    }
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

        public List<clsDetalleUsuarioReporte> consulta_Reporte_Todos()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Reporte_Todos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader lector = comando.ExecuteReader();
                List<clsDetalleUsuarioReporte> coleccion = new List<clsDetalleUsuarioReporte>();

                while (lector.Read())
                {
                    clsDetalleUsuarioReporte objclsDetalleReporte = new clsDetalleUsuarioReporte();

                    objclsDetalleReporte.objReporte.idReporte = lector.GetInt32(0);
                    objclsDetalleReporte.objReporte.nombre = lector.GetString(1).ToString().Trim();

                    objclsDetalleReporte.objReporte.objArea.idArea = lector.GetString(2).ToString().Trim();
                    objclsDetalleReporte.objReporte.objArea.descripcion = lector.GetString(3).ToString().Trim();

                    objclsDetalleReporte.objReporte.objFormulario.idFormulario = lector.GetInt16(4);
                    objclsDetalleReporte.objReporte.objFormulario.nombre = lector.GetString(5).Trim();

                    objclsDetalleReporte.objReporte.descripcion = lector.GetString(6).Trim();
                    objclsDetalleReporte.objReporte.formula = lector.GetString(7).Trim();

                    coleccion.Add(objclsDetalleReporte);
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

        public void consulta_DetalleReporte_Por_Usuario_PermisosFormlario(string usuario)
        {

            try
            {
                DataTable dtPermisos = new DataTable();

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_consulta_DetalleReporte_Por_Usuario_PermisosFormlario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@usuario", SqlDbType.NVarChar, 20).Value = usuario;
                SqlDataReader lector = comando.ExecuteReader();
                dtPermisos.Load(lector);
                if (dtPermisos.Rows.Count > 0)
                {
                    Utilitario.Instancia.ListaPermisos = dtPermisos; //inserto la data traida del sp a un datable 
                }


                conexion.Close();
                conexion.Dispose();




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
