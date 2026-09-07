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
    public class clsDetalleOrdenCompraDAO
    {
        private clsDetalleOrdenCompraDAO()
        {
          
        }

        private readonly static clsDetalleOrdenCompraDAO instancia = new clsDetalleOrdenCompraDAO();

        public static clsDetalleOrdenCompraDAO Instancia
        {
            get { return instancia; }
        }

        public List<clsDetalleOrdenCompra> consultar_OrdenCompra_Por_Mes_Anio(String nroOrden)
        {
            SqlConnection cnn = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            cnn.Open();

            SqlCommand cmd = new SqlCommand("ReportesApp_OrdenCompraDetalle_Consultar_Por_NroOrdenCompra", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nroOrdenCompra", SqlDbType.VarChar, 15).Value = nroOrden;         
            SqlDataReader lector = cmd.ExecuteReader();
            List<clsDetalleOrdenCompra> coleccion = new List<clsDetalleOrdenCompra>();

            Decimal c4 = 0, c5 = 0;

            while (lector.Read())
            {
                clsDetalleOrdenCompra obj = new clsDetalleOrdenCompra();

                obj.c1 = lector.GetString(0);
                obj.c2 = lector.GetString(1);
                obj.c3 = Math.Round(lector.GetDecimal(2), 0).ToString("#,###,##0.000", CultureInfo.InvariantCulture);
                obj.c4 = Math.Round(lector.GetDecimal(3), 3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);
                obj.c5 = Math.Round(lector.GetDecimal(4), 3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);

                c4 += lector.GetDecimal(3);
                c5 += lector.GetDecimal(4);

                coleccion.Add(obj);
            }

            clsDetalleOrdenCompra objeto = new clsDetalleOrdenCompra();
            objeto.c1 = "IGV";
            objeto.c2 = "-";
            objeto.c3 = "-";
            objeto.c4 = "-";
            objeto.c5 = Math.Round((c5 * (decimal)0.18), 3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);
            coleccion.Add(objeto);

            objeto = new clsDetalleOrdenCompra();
            objeto.c1 = "TOTAL";
            objeto.c2 = "-";
            objeto.c3 = "-";
            objeto.c4 = "-";
            objeto.c5 = Math.Round((c5 * (decimal)1.18), 3).ToString("#,###,##0.000", CultureInfo.InvariantCulture);
            coleccion.Add(objeto);

            cnn.Close();
            cnn.Dispose();

            return coleccion;
        }
    }
}
