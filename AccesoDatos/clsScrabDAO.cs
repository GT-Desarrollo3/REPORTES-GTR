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
    public class clsScrabDAO
    {
        private clsScrabDAO()
        {
            meses.Add("ENERO");
            meses.Add("FEBRERO");
            meses.Add("MARZO");
            meses.Add("ABRIL");
            meses.Add("MAYO");
            meses.Add("JUNIO");
            meses.Add("JULIO");
            meses.Add("AGOSTO");
            meses.Add("SETIEMBRE");
            meses.Add("OCTUBRE");
            meses.Add("NOVIEMBRE");
            meses.Add("DICIEMBRE");
        }

        List<String> meses = new List<string>();

        private readonly static clsScrabDAO instancia = new clsScrabDAO();
        
        public static clsScrabDAO Instancia
        {
            get { return instancia; }
        }

        public List<clsScrab> consultar_scrab_porAnio(String anio, float totalFloat)
        {
            SqlConnection cnn = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            cnn.Open();
            SqlCommand cmd = new SqlCommand("ReportesApp_Neumatico_Consultar_Scrab_PorAnio", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@anio", SqlDbType.VarChar,4).Value = anio;
            cmd.Parameters.Add("@totalFlota", SqlDbType.Float).Value = totalFloat;

            SqlDataReader lector = cmd.ExecuteReader();
            List<clsScrab> coleccion = new List<clsScrab>();

            Decimal c2 = 0, c3 = 0;

            while (lector.Read())
            {               

                for (int i = 0, j=0; i < meses.Count; i++, j+=2)
                {
                    clsScrab obj = new clsScrab();

                    obj.c1 = meses[i];
                    obj.c2 = lector.GetInt32(j).ToString();
                    obj.c3 = Math.Round(lector.GetDecimal(j + 1), 3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);

                    c2 += lector.GetInt32(j);
                    c3 += lector.GetDecimal(j + 1);

                    coleccion.Add(obj);
                }
            }

            clsScrab objeto = new clsScrab();

            objeto.c1 = "PROMEDIO";
            objeto.c2 = Math.Round((c2 / 12), 3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);
            objeto.c3 = Math.Round((c3 / 12), 3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);

            coleccion.Add(objeto);

            cnn.Close();
            cnn.Dispose();

            return coleccion; 
        }
    }
}
