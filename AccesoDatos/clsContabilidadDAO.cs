using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using Comun;

namespace AccesoDatos
{
    public class clsContabilidadDAO
    {
        private readonly static clsContabilidadDAO instancia = new clsContabilidadDAO();

        public static clsContabilidadDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataSumarizado(string fechaini, string fechafin, string tipofecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte_Sumarizado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@TIPOFECHA", tipofecha));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

       //Store Resumen de Sumarizado con Importe de los Viajes Completados
        public DataTable GetDataSumarizadoImporte(string fechaini, string fechafin, string filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte_Importe", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@FILTRO", filtro));
                //comando.Parameters.Add(new SqlParameter("@TIPOFECHA", tipofecha));
                //comando.Parameters.Add(new SqlParameter("@TIPOFECHA", filtro));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetDataMayorDetallado (string perini,string perfin, string percomp, string documento ) 
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte_MayorDetallado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO_INICIO", perini));
                comando.Parameters.Add(new SqlParameter("@PERIODO_FIN", perfin));
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", percomp));
                comando.Parameters.Add(new SqlParameter("@NUMERODOC", documento));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader()); 
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetReporte19(string perini, string perfin, string comp, string cuentaini, string cuentafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte19", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODOINI", perini));
                comando.Parameters.Add(new SqlParameter("@PERIODOFIN", perfin));
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", comp));
                comando.Parameters.Add(new SqlParameter("@CUENTAINI", cuentaini));
                comando.Parameters.Add(new SqlParameter("@CUENTAFIN", cuentafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataCajaChicaRepGastos(string compañia, string uni_rep, string tipodoc, string uni_neg, string fechaini, string fechafin,
            int docdesde,int dochasta,string concepto,string estado,int beneficiario,int proveedor,string centrocostos)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte_CajaChicayRepGastos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
                comando.Parameters.Add(new SqlParameter("@UNIDAD_REP", uni_rep));
                comando.Parameters.Add(new SqlParameter("@TIPO_DOC", tipodoc));
                comando.Parameters.Add(new SqlParameter("@UNIDAD_NEG", uni_neg));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@DOC_DESDE", docdesde));
                comando.Parameters.Add(new SqlParameter("@DOC_HASTA", dochasta));
                comando.Parameters.Add(new SqlParameter("@CONCEPTO", concepto));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                comando.Parameters.Add(new SqlParameter("@BENEFICIARIO", beneficiario));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", proveedor));
                comando.Parameters.Add(new SqlParameter("@CENTROCOSTOS", centrocostos));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataRepGastosViaje(string compañia, string uni_rep, string uni_neg, string fechaini, string fechafin,
            int docdesde, int dochasta, string concepto, string estado, int beneficiario, int proveedor, string centrocostos)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte_GV", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
                comando.Parameters.Add(new SqlParameter("@UNIDAD_REP", uni_rep));
                comando.Parameters.Add(new SqlParameter("@UNIDAD_NEG", uni_neg));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@DOC_DESDE", docdesde));
                comando.Parameters.Add(new SqlParameter("@DOC_HASTA", dochasta));
                comando.Parameters.Add(new SqlParameter("@CONCEPTO", concepto));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                comando.Parameters.Add(new SqlParameter("@BENEFICIARIO", beneficiario));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", proveedor));
                comando.Parameters.Add(new SqlParameter("@CENTROCOSTOS", centrocostos));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataLibro(int tipo,String fechaPeriodo, String codigoEmpresa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand();
                switch(tipo)
                {
                    case 0: //compras
                        comando = new SqlCommand("ReportesApp_Contabilidad_RegistroCompra", conexion);
                        break;
                    case 1: //ventas
                        comando = new SqlCommand("ReportesApp_Contabilidad_RegistroVenta", conexion);
                        break;
                    case 2: //mayor
                        comando = new SqlCommand("ReportesApp_Contabilidad_LibroMayor", conexion);
                        break;
                    case 3: //diario
                        comando = new SqlCommand("ReportesApp_Contabilidad_LibroDiario", conexion);
                        break;
                    case 4: //diario detalle
                        comando = new SqlCommand("ReportesApp_Contabilidad_LibroDiarioDetalle", conexion);
                        break;
                }
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@periodo", SqlDbType.NVarChar, 20).Value = fechaPeriodo;
                comando.Parameters.Add("@empresa", SqlDbType.NVarChar, 20).Value = codigoEmpresa;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public DataTable GetViajesPorFacturar(string fechaini, string fechafin,int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Viajes_PorFacturar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetViajesPorFacturar_Resumen(string fechaini)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Viajes_PorFacturar_Resumen", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public void UpdateFechaGuia(DateTime fechaguia, string viaje)
        {
            string limpio = Regex.Replace(fechaguia.ToString(), @"[a.m.p.m.]", "");
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            string query = "";
            if (fechaguia != null)
            {
                query = "UPDATE OP_TR_VIAJE SET FechaEnvioGuia='" + limpio +
                        "' WHERE Codigo = '" + viaje + "' AND FechaEnvioGuia IS NULL";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
        }

        public void UpdateFechaGuia_Sumarizado(DateTime fechaguia, string viaje,string detalle)
        {
            string limpio = Regex.Replace(fechaguia.ToString(), @"[a.m.p.m.]", "");
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            string query = "";
            if (fechaguia != null)
            {
                query = "UPDATE OP_TR_VIAJE SET FechaEnvioGuia='" + limpio +"' ,DetallePF='" + detalle +
                        "' WHERE Codigo = '" + viaje + "' AND FechaEnvioGuia IS NULL";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
        }

        public bool UpdateViajePF(string viaje, string situacion, string detalle)
        {
            try
            {

                bool resultado;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                string query = "";
                query = "UPDATE OP_TR_VIAJE SET SituacionPF='" + situacion + "',DetallePF='" + detalle +
                        "' WHERE Codigo = '" + viaje + "'";
                SqlCommand comando = new SqlCommand(query, conexion);
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                conexion.Close();
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetFacturas(string compania, string fechaini, string fechafin, int cliente, string busqueda, int Filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_BuscarFactura", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compania));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@BUSQUEDA", busqueda));
                comando.Parameters.Add(new SqlParameter("@FILTRO", Filtro));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAdelantosAplicados(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_AdelantosAplicados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compania));
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                //comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                //comando.Parameters.Add(new SqlParameter("@BUSQUEDA", busqueda));
                //comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //Resumen de Tipos de Servicio 
        public DataTable GetTipoServicios(string fechaini, string fechafin, string tipofecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Tipo_Servicios", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@TIPOFECHA", tipofecha));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetTipoServiciosCompañia(string fechaini, string fechafin, string tipofecha, string compañia)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Tipo_Servicios_Compañia", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@TIPOFECHA", tipofecha));
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAnalisisGV(string periodoini, string periodofin, string cuentaini, string cuentafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte_Analisis_GV", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODOINI", periodoini));
                comando.Parameters.Add(new SqlParameter("@PERIODOFIN", periodofin));
                comando.Parameters.Add(new SqlParameter("@CUENTAINI", cuentaini));
                comando.Parameters.Add(new SqlParameter("@CUENTAFIN", cuentafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetValidacionAdelantos(string fechaini, string fechafin, string periodoini, string periodofin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Reporte_Adelantos_vs_CxP", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@PERIODOINI", periodoini));
                comando.Parameters.Add(new SqlParameter("@PERIODOFIN", periodofin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetValidacioCuentasxCobrar(string periodoini, int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Validacion_CuentasxCobrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodoini));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetComercialvsContabilidad(string periodoini, string periodofin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_AnticuamientovsReporte22", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODOINI", periodoini));
                comando.Parameters.Add(new SqlParameter("@PERIODOFIN", periodofin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetCListaObligaciones(string fechaini, string fechafin, int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Lista_Obligaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetCListaObligacionesAprobadas(string fechaini, string fechafin,string usuario, int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Lista_Obligaciones_Aprobadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@USUARIO", usuario));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetGuardaObligaciones(string documento, string proveedor, string fecha,string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Guarda_Obligaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@DOCUMENTO", documento));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", proveedor));
                comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                comando.Parameters.Add(new SqlParameter("@USUARIO", usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListaAdelantos(string obligacion, string proveedor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Lista_Adelantos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@OBLIGACION", obligacion));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", proveedor));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetVoucherPeriodo(string CajaChica)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("Select TOP 1 SUBSTRING(VoucherLiquidacion,1,6) AS Voucher from AP_CajaChicaDetalle Where CajaChicaNumero='"+CajaChica+"'", conexion);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public bool UpdatePeriodo(string documento,string periodo)
        {
            try
            {

                bool resultado;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                string query = "";
                query = "UPDATE Obligaciones SET Voucher='" + periodo + 
                        "' WHERE NumeroDocumento = '" + documento + "'";
                SqlCommand comando = new SqlCommand(query, conexion);
                int rowsaffected = comando.ExecuteNonQuery();
                if (rowsaffected > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                conexion.Close();
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public void GetCambiarPeriodoVoucher(string obligacion, string periodo)
        {
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Cambia_Periodo_Obligacion", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@OBLIGACION", obligacion));
            comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
            comando.CommandTimeout = 0;
            comando.ExecuteNonQuery();
        }

        public DataTable GetControlFacturas(string compañia, string fechaini, string fechafin, int cliente, string facturas)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Control_Facturas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@FACTURAS", facturas));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPeajes_ImportarDataExcel01(string xml, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Peajes_ImportaData", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@DataXML", xml));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPeajes_Ver(int Opcion, string DatoFiltro1, string DatoFiltro2, string DatoFiltro3)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Peajes_Ver", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@DatoFiltro1", DatoFiltro1));
                comando.Parameters.Add(new SqlParameter("@DatoFiltro2", DatoFiltro2));
                comando.Parameters.Add(new SqlParameter("@DatoFiltro3", DatoFiltro3));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetPeajes_Vincular(string xmlpeaje, string xmlviaje, int IDPeaje, string CodigoViaje, string Ruta, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Peajes_Vincular", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@xmlpeaje", xmlpeaje));
                comando.Parameters.Add(new SqlParameter("@xmlviaje", xmlviaje));
                comando.Parameters.Add(new SqlParameter("@IDPeaje", IDPeaje));
                comando.Parameters.Add(new SqlParameter("@CodigoViaje", CodigoViaje));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPeajes_Desvincular(int IDPeaje, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Contabilidad_Peajes_Desvincular", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDPeaje", IDPeaje));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetLista_KpiTransporte(string anio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Reporte_KPITransporte", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Anio", anio));                
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //sem pasó por aqui
        public DataTable ReportesApp_ListarViajes_AnexarGuias(string fechaInicio, string fechaFin, string viaje, string idConductor, string idRuta, int idEstadoViaje)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarViajes_AnexarGuias", conexion);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                if (viaje.Length == 0)
                {
                    cmd.Parameters.AddWithValue("@Viaje", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Viaje", viaje);
                }

                if (idConductor == null)
                {
                    cmd.Parameters.AddWithValue("@idConductor", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idConductor", Convert.ToInt32(idConductor));
                }
                if (idRuta == null)
                {
                    cmd.Parameters.AddWithValue("@idRuta", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idRuta", Convert.ToInt32(idRuta));
                }

                if (idEstadoViaje == -1)
                {
                    cmd.Parameters.AddWithValue("@idEstadoViaje", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idEstadoViaje", Convert.ToInt32(idEstadoViaje));
                }


                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);



            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }

            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_ListarOT_Viaje(int idViaje, int idConductor, string fecha)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarOT_Viaje", conexion);
                cmd.Parameters.AddWithValue("@idViaje", idViaje);
                cmd.Parameters.AddWithValue("@idConductor", idConductor);
                cmd.Parameters.AddWithValue("@FechaProgramacion", fecha);

                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);



            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }

            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_ListarGuiasOT_Viaje(int idOT, int idViaje, string FechaProgramacion)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarGuiasOT_Viaje", conexion);
                cmd.Parameters.AddWithValue("@idOT", idOT);
                cmd.Parameters.AddWithValue("@idViaje", idViaje);
                cmd.Parameters.AddWithValue("@FechaProgramacion", FechaProgramacion);

                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);



            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }

            finally { cmd.Connection.Close(); }
            return dt;
        }

        public Boolean ReportesApp_Actualizar_GuiaPorViaje(int idGuia, string CodViaje, string GuiaRemitente, string GuiaOtro, string Observaciones,string Peso,string guiaTransportista,string serie, string numero)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            Boolean respuesta = false;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Actualizar_GuiaPorViaje", conexion);
                cmd.Parameters.AddWithValue("@idGuia", idGuia);
                if (CodViaje.Length == 0)
                {
                    cmd.Parameters.AddWithValue("@CodViaje", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CodViaje", CodViaje);
                }
                if (GuiaRemitente.Length == 0)
                {
                    cmd.Parameters.AddWithValue("@GuiaRemitente", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GuiaRemitente", GuiaRemitente);
                }
                if (GuiaOtro.Length == 0)
                {
                    cmd.Parameters.AddWithValue("@GuiaOtro", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GuiaOtro", GuiaOtro);
                }
                if (Observaciones.Length == 0)
                {
                    cmd.Parameters.AddWithValue("@Observaciones", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Observaciones", Observaciones);
                }

                cmd.Parameters.AddWithValue("@Peso",Peso );
                cmd.Parameters.AddWithValue("@GuiaTransportista", guiaTransportista);
                cmd.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@Numero", numero);
                





                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }

            finally { cmd.Connection.Close(); }
            return respuesta;
        }

        public Boolean ReportesApp_Desvncular_GuiaRetorno(int idGuia)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            Boolean respuesta = false;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Desvncular_GuiaRetorno", conexion);
                if (idGuia == 0)
                {
                    cmd.Parameters.AddWithValue("@idGuia", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idGuia", idGuia);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }


            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }

            finally { cmd.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_ListarComprobantesSire(string empresa,string Check_compras_ventas, string periodo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
  
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Contabilidad_ListarComprobantesSire", conexion);
                cmd.Parameters.AddWithValue("@Empresa", empresa);
                cmd.Parameters.AddWithValue("@Tipo", Check_compras_ventas);
                cmd.Parameters.AddWithValue("@Periodo", periodo);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Contabilidad_AbrirCerrarPeriodos(int Opcion, string Compania, string Periodo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Contabilidad_AbrirCerrarPeriodos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Compania", Compania));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Contabilidad_ListarPeriodosAbiertos(string Compania, string Periodo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Contabilidad_ListarPeriodosAbiertos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Compania", Compania));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Contabilidad_AbrirCerrarPeriodo(string Compania, string Modulo, string Estado, string Periodo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Contabilidad_AbrirCerrarPeriodo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Compania", Compania));
                cmd.Parameters.Add(new SqlParameter("@Modulo", Modulo));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }
    }

}


