using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace AccesoDatos
{
    public class clsMovilidadDAO
    {
        private clsMovilidadDAO()
        {

        }

        private readonly static clsMovilidadDAO instancia = new clsMovilidadDAO();

        public static clsMovilidadDAO Instancia
        {
            get { return instancia; }
        }

        public List<clsMovilidad> consultar_movilidad_porBeneficiario(String periodo, String beneficiario)
        {
            SqlConnection cnn = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            cnn.Open();
            SqlCommand cmd = new SqlCommand("ReportesApp_Consulta_Movilidad", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@periodo", SqlDbType.VarChar, 12).Value = periodo;            
            cmd.Parameters.Add("@beneficiario", SqlDbType.VarChar, 12).Value = beneficiario;

            SqlDataReader lector = cmd.ExecuteReader();
            List<clsMovilidad> coleccion = new List<clsMovilidad>();

            while (lector.Read())
            {
                clsMovilidad obj = new clsMovilidad();

                obj.c1 = lector.GetString(0).ToString().Trim();
                obj.c2 = lector.GetString(1).ToString().Trim();
                obj.c3 = lector.GetInt32(2).ToString();
                obj.c4 = lector.GetDecimal(3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);
                obj.c5 = lector.GetInt32(4).ToString();

                coleccion.Add(obj);
            }

            cnn.Close();
            cnn.Dispose();

            return coleccion;
        }

        public List<clsMovilidad> consultar_movilidad(String periodo)
        {
            SqlConnection cnn = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            cnn.Open();
            SqlCommand cmd = new SqlCommand("ReportesApp_Consulta_Movilidad", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@periodo", SqlDbType.VarChar, 12).Value = periodo;

            SqlDataReader lector = cmd.ExecuteReader();
            List<clsMovilidad> coleccion = new List<clsMovilidad>();

            while (lector.Read())
            {
                clsMovilidad obj = new clsMovilidad();

                obj.c1 = lector.GetString(0).ToString().Trim();
                obj.c2 = lector.GetString(1).ToString().Trim();
                obj.c3 = lector.GetInt32(2).ToString();
                obj.c4 = lector.GetDecimal(3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);
                obj.c5 = lector.GetInt32(4).ToString();

                coleccion.Add(obj);
            }

            cnn.Close();
            cnn.Dispose();

            return coleccion;
        }
    }
}
