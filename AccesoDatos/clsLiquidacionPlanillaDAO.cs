using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class clsLiquidacionPlanillaDAO
    {

        #region "Patron Singleton"

        private readonly static clsLiquidacionPlanillaDAO instancia = new clsLiquidacionPlanillaDAO();

        public static clsLiquidacionPlanillaDAO Instancia
        {
            get { return instancia; }
        }
        #endregion

        SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());


        public DataTable LlenarControlFormLiquidacionPlanilla()
        {
            DataTable dt = new DataTable();

            try
            {
                
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Liquidaciones_LlenadoControlesFormularioLiquidaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dt.Load(comando.ExecuteReader());
            
            }
            catch
            {
                dt = null; 
            }
            finally
            {
                conexion.Close();
            }

            return dt;

        }


        public DataTable ListarConductores(string CompaniaSocio, string Filtro)
        {
            DataTable dt = new DataTable();

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Liquidaciones_ListadoDeCoductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CompaniaSocio", CompaniaSocio);
                comando.Parameters.AddWithValue("@Filtro", Filtro);
                
                dt.Load(comando.ExecuteReader());

            }
            catch
            {
                dt = null;
            }
            finally
            {
                conexion.Close();
            }

            return dt;

        }

        public DataTable ListarGastosAdelantoPorConductor(string CompaniaSocio, int CodConductor, string Estado)
        {
            DataTable dt = new DataTable();

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Liquidaciones_PlanillaGastosAdelantoPorCoductor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CompaniaSocio", CompaniaSocio);
                comando.Parameters.AddWithValue("@CodConductor", CodConductor);
                comando.Parameters.AddWithValue("@Estado", Estado);
                dt.Load(comando.ExecuteReader());
            }
            catch
            {
                dt = null;
            }
            finally
            {
                conexion.Close();
            }

            return dt;

        }


        public DataTable ListarProgramacionViajePorConductor(int CodConductor)
        {
            DataTable dt = new DataTable();

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Liquidaciones_ProgramacionViajesPorCoductor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CodConductor", CodConductor);
                dt.Load(comando.ExecuteReader());
            }
            catch
            {
                dt = null;
            }
            finally
            {
                conexion.Close();
            }

            return dt;

        }


        public DataTable ListarPlanillasLiquidadasPorConductor(string CompaniaSocio, int CodConductor)
        {
            DataTable dt = new DataTable();

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Liquidaciones_PlanillasLiquidadasPorCoductor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CompaniaSocio", CompaniaSocio);
                comando.Parameters.AddWithValue("@CodConductor", CodConductor);
                dt.Load(comando.ExecuteReader());
            }
            catch
            {
                dt = null;
            }
            finally
            {
                conexion.Close();
            }

            return dt;

        }


        public DataTable ReportarLiquidacionesDiario(int Dia, int Mes, int Anio, string Usuario)
        {
            DataTable dt = new DataTable();

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Liquidaciones_ReporteDiario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Dia", Dia);
                comando.Parameters.AddWithValue("@Mes", Mes);
                comando.Parameters.AddWithValue("@Anio", Anio);
                comando.Parameters.AddWithValue("@User", Usuario);

                dt.Load(comando.ExecuteReader());
            }
            catch
            {
                dt = null;
            }
            finally
            {
                conexion.Close();
            }

            return dt;

        }


        public DataTable ReportarPlanillasPendientes(string Usuario)
        {
            DataTable dt = new DataTable();

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Liquidacion_PlanillasPendientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@USUARIO", Usuario);

                dt.Load(comando.ExecuteReader());
            }
            catch
            {
                dt = null;
            }
            finally
            {
                conexion.Close();
            }

            return dt;

        }

    }
}
