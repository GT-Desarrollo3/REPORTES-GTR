using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Data.OleDb;

namespace AccesoDatos
{
    public class clsDetalleMovilidadDAO
    {
        private clsDetalleMovilidadDAO()
        {

        }

        private readonly static clsDetalleMovilidadDAO instancia = new clsDetalleMovilidadDAO();

        public static clsDetalleMovilidadDAO Instancia
        {
            get { return instancia; }
        }

        //public DataTable consultar_DetalleMovilidad_PorPersona(String periodo, String beneficiario, String persona)
        //{
        //    SqlConnection cnn = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());            
        //    cnn.Open();

        //    //DataTable dtDatos = new DataTable();

        //    SqlCommand cmd = new SqlCommand("ReportesApp_Consulta_Movilidad_Detalle", cnn);
        //    //da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    //da.SelectCommand.Parameters.Add("@periodo", SqlDbType.VarChar, 12).Value = periodo;
        //    //da.SelectCommand.Parameters.Add("@beneficiario", SqlDbType.VarChar, 12).Value = beneficiario;
        //    //da.SelectCommand.Parameters.Add("@persona", SqlDbType.VarChar, 12).Value = persona;          

        //    //OleDbConnection cnn = new OleDbConnection(clsConexion.Instancia.cadenaConexionLocal());
        //    //cnn.Open();

        //   // OleDbCommand cmd = new OleDbCommand("ReportesApp_Consulta_Movilidad_Detalle", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;            

        //    cmd.Parameters.Add("@periodo", SqlDbType.VarChar, 12).Value = periodo;
        //    cmd.Parameters.Add("@beneficiario", SqlDbType.VarChar, 12).Value = beneficiario;
        //    cmd.Parameters.Add("@persona", SqlDbType.VarChar, 12).Value = persona;

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dtDatos = new DataTable();

        //    da.Fill(dtDatos);

        //    cnn.Close();
        //    cnn.Dispose();

        //    return dtDatos;
        //}

        public List<clsDetalleMovilidad> consultar_DetalleMovilidad_PorPersona(String periodo, String beneficiario, String persona)
        {
            SqlConnection cnn = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            cnn.Open();

            SqlCommand cmd = new SqlCommand("ReportesApp_Consulta_Movilidad_Detalle", cnn);
           
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@periodo", SqlDbType.VarChar, 12).Value = periodo;
            //cmd.Parameters.Add("@beneficiario", SqlDbType.VarChar, 12).Value = beneficiario;
            cmd.Parameters.Add("@persona", SqlDbType.VarChar, 12).Value = persona;

            SqlDataReader lector = cmd.ExecuteReader();
            List<clsDetalleMovilidad> lista = new List<clsDetalleMovilidad>();

            while(lector.Read())
            {
                clsDetalleMovilidad obj = new clsDetalleMovilidad();

                obj.c1 = lector.GetString(0);
                obj.c2 = lector.GetString(1);
                obj.c3 = lector.GetString(2);
                obj.c4 = lector.GetString(3);
                obj.c5 = lector.GetString(4);
                obj.c6 = lector.GetString(5);
                obj.c7 = lector.GetDecimal(6).ToString("#,###,##0.000", CultureInfo.InvariantCulture);

                lista.Add(obj);
            }

            cnn.Close();
            cnn.Dispose();

            return lista;
        }
    }
}
