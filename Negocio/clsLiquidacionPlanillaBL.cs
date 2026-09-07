using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsLiquidacionPlanillaBL
    {
        #region "Patron Singleton"

        private readonly static clsLiquidacionPlanillaBL instancia = new clsLiquidacionPlanillaBL();

        public static clsLiquidacionPlanillaBL Instancia
        {
            get { return instancia; }
        }

        #endregion


        public DataTable LlenarControlFormLiquidacionPlanilla()
        {
            DataTable dt = clsLiquidacionPlanillaDAO.Instancia.LlenarControlFormLiquidacionPlanilla();
            return dt;
        }

        public DataTable ListarConductores(string CompaniaSocio, string Filtro)
        {
            DataTable dt = clsLiquidacionPlanillaDAO.Instancia.ListarConductores(CompaniaSocio, Filtro);
            return dt;
        }

        public DataTable ListarGastosAdelantoPorConductor(string CompaniaSocio, int CodConductor, string Estado)
        {
            DataTable dt = clsLiquidacionPlanillaDAO.Instancia.ListarGastosAdelantoPorConductor(CompaniaSocio, CodConductor, Estado);
            return dt;
        }

        public DataTable ListarProgramacionViajePorConductor(int CodConductor)
        {
            DataTable dt = clsLiquidacionPlanillaDAO.Instancia.ListarProgramacionViajePorConductor(CodConductor);
            return dt;
        }

        public DataTable ListarPlanillasLiquidadasPorConductor(string CompaniaSocio, int CodConductor)
        {
            DataTable dt = clsLiquidacionPlanillaDAO.Instancia.ListarPlanillasLiquidadasPorConductor(CompaniaSocio, CodConductor);
            return dt;
        }

        public DataTable ReportarLiquidacionesDiario(int Dia, int Mes, int Anio, string Usuario)
        {
            DataTable dt = clsLiquidacionPlanillaDAO.Instancia.ReportarLiquidacionesDiario(Dia, Mes, Anio, Usuario);
            return dt;
        }

        public DataTable ReportarPlanillasPendientes(string Usuario)
        {
            DataTable dt = clsLiquidacionPlanillaDAO.Instancia.ReportarPlanillasPendientes(Usuario);
            return dt;
        }


    }
}
