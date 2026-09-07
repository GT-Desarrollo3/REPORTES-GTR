using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class clsFormularioDAO
    {
        private clsFormularioDAO()
        {

        }

        private readonly static clsFormularioDAO instancia = new clsFormularioDAO();

        public static clsFormularioDAO Instancia
        {
            get { return instancia; }
        }

        public List<clsFormulario> consulta_Formularios_NoAsignados()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();

                SqlCommand comando = new SqlCommand("ReportesApp_Formulario_Consulta_NoAsignados", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader lector = comando.ExecuteReader();
                List<clsFormulario> coleccion = new List<clsFormulario>();

                while (lector.Read())
                {
                    clsFormulario obj = new clsFormulario();

                    obj.idFormulario = lector.GetInt16(0);
                    obj.nombre = lector.GetString(1).ToString().Trim();
                    obj.esAsignado = lector.GetBoolean(2);                    

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
    }
}
