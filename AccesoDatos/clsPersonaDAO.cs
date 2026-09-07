using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class clsPersonaDAO
    {
        private clsPersonaDAO()
        {

        }

        private readonly static clsPersonaDAO instancia = new clsPersonaDAO();

        public static clsPersonaDAO Instancia
        {
            get { return instancia; }
        }

        public List<clsEmpleado> ConsultarEmpleadosActivos()
        {          
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Persona_ConsultarEmpleadosActivos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader lector = comando.ExecuteReader();
                List<clsEmpleado> coleccion = new List<clsEmpleado>();

                while (lector.Read())
                {
                    clsEmpleado obj = new clsEmpleado();

                    obj.idPersona = lector.GetInt32(0).ToString().Trim();
                    obj.Nombres = lector.GetString(1).ToString().Trim();

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

        public List<clsEmpleado> consultarEmpleadosIngresantes_PorMesAnio(String mes, String anio)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Consulta_Ingresantes_PorMesAnio", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@anio", SqlDbType.Char, 4).Value = anio;
                comando.Parameters.Add("@mes", SqlDbType.Char, 2).Value = mes;

                SqlDataReader lector = comando.ExecuteReader();
                List<clsEmpleado> coleccion = new List<clsEmpleado>();

                int i = 1;

                while (lector.Read())
                {
                    clsEmpleado obj = new clsEmpleado();

                    obj.item = i.ToString();
                    obj.idPersona = lector.GetInt32(0).ToString();
                    obj.Nombres = lector.GetString(1).Trim();
                    obj.cargo = lector.GetString(2).Trim();
                    obj.empresa = lector.GetString(3).Trim();
                    obj.fechaIngreso = lector.GetDateTime(4).ToString("dd/MM/yyyy");

                    coleccion.Add(obj);
                    i++;
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

        public List<clsEmpleado> consultarEmpleadosCesantes_PorMesAnio(String mes, String anio)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Consulta_Cesantes_PorMesAnio", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@anio", SqlDbType.VarChar, 4).Value = anio;
                comando.Parameters.Add("@mes", SqlDbType.VarChar, 2).Value = mes;

                SqlDataReader lector = comando.ExecuteReader();
                List<clsEmpleado> coleccion = new List<clsEmpleado>();

                int i = 1;

                while (lector.Read())
                {
                    clsEmpleado obj = new clsEmpleado();

                    obj.item = i.ToString();
                    obj.idPersona = lector.GetInt32(0).ToString();
                    obj.Nombres = lector.GetString(1).Trim();
                    obj.cargo = lector.GetString(2).Trim();
                    obj.empresa = lector.GetString(3).Trim();
                    obj.fechaCese = obj.fechaIngreso = lector.GetDateTime(4).ToString("dd/MM/yyyy");

                    coleccion.Add(obj);
                    i++;
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

        public DataTable GetProveedor(string parametro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "select top 15 Persona,Busqueda,Documento from PersonaMast "+ 
                "where EsProveedor = 'S' and Estado = 'A' "+
                "and Busqueda like '%" + parametro + "%' ORDER BY Busqueda";
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

    }
}
