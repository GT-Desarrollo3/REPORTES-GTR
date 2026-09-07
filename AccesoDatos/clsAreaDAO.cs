using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;


namespace AccesoDatos
{
    public class clsAreaDAO
    {
        private clsAreaDAO()
        {

        }

        private readonly static clsAreaDAO instancia = new clsAreaDAO();

        public static clsAreaDAO Instancia
        {
            get { return instancia; }
        }

        public List<clsArea> consulta_Area_Activas()
        {          
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Area_ConsultaActivos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader lector = comando.ExecuteReader();
                List<clsArea> coleccion = new List<clsArea>();

                while (lector.Read())
                {
                    clsArea obj = new clsArea();

                    obj.idArea = lector.GetString(0).ToString().Trim();
                    obj.descripcion = lector.GetString(1).ToString().Trim();

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

        public DataTable AreasPorCodigo(string Areas)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Areas_Por_Codigo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Descripcion", Areas));
                comando.CommandTimeout = 0;
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
