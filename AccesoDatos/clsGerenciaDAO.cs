using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class clsGerenciaDAO
    {
        private readonly static clsGerenciaDAO instancia = new clsGerenciaDAO();

        public static clsGerenciaDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetRetrasos(string fini, string ffin,int criterio,int retraso,int tipo)
        {
            if (tipo == 1)
            {
                try
                {
                    DataTable dtTemp = new DataTable();
                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("ReportesApp_Gerencia_Retrasos_Viajes_Nacionales", conexion);
                    comando.CommandTimeout = 0;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                    comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                    comando.Parameters.Add(new SqlParameter("@CRITERIO", criterio));
                    comando.Parameters.Add(new SqlParameter("@RETRASO", retraso));
                    dtTemp.Load(comando.ExecuteReader());
                    return dtTemp;
                }
                catch
                {
                    return new DataTable();
                }
            }
            else
            {
                try
                {
                    DataTable dtTemp = new DataTable();
                    SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("ReportesApp_Gerencia_Retrasos_Viajes_Locales", conexion);
                    comando.CommandTimeout = 0;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                    comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                    comando.Parameters.Add(new SqlParameter("@CRITERIO", criterio));
                    comando.Parameters.Add(new SqlParameter("@RETRASO", retraso));
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
}
