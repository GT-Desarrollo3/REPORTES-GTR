using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class clsRecursosHumanos
    {
        private readonly static clsOperacionesDAO instancia = new clsOperacionesDAO();
        public static clsRecursosHumanos Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataConsolidado(string fini, string ffin, Int32 det, Int32 todos, Int32 xfact, bool fechaprog)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                if (fechaprog == true)
                {
                    comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Consolidado_FechasProg2", conexion);
                }
                else
                {
                    comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Consolidado_FechasCreac2", conexion);
                }
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@DETALLADO", det));
                comando.Parameters.Add(new SqlParameter("@TODOS", todos));
                comando.Parameters.Add(new SqlParameter("@POR_FACTURAR", xfact));
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
