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
    public class clsFinanzasDAO
    {
        private readonly static clsFinanzasDAO instancia = new clsFinanzasDAO();

        public static clsFinanzasDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetFacturacionDiaria(string fini, string ffin, string tipofecha, string transpesa, string bra, string filtro, string sucursal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Reporte_FacturacionDiaria", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@TIPO_FECHA", tipofecha));
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@FILTRO", filtro));
                comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPagoMasivo(string compania, string fini, string ffin, string banco, string moneda, string cuenta, string prepago)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                if (moneda == "Soles")
                {
                    comando = new SqlCommand("ReportesApp_Finanzas_Pago_Masivo_Soles", conexion);
                }
                else
                {
                    comando = new SqlCommand("ReportesApp_Finanzas_Pago_Masivo_Dolares", conexion);
                }
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compania));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@BANCO", banco));
                comando.Parameters.Add(new SqlParameter("@CUENTA", cuenta));
                comando.Parameters.Add(new SqlParameter("@PREPAGO", prepago));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetSaldosProveedoresFinan(string fini, string ffin, string proveedor)
        {
            try
            {
                //int prov = -1;
                //if (proveedor != "") 
                //{
                //    prov = Convert.ToInt32(proveedor);
                //}
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Saldos_Proveedores_Finan", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                //comando.Parameters.Add(new SqlParameter("@PROVEEDOR", prov));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetSaldosProveedoresCont(string fini, string ffin, string proveedor)
        {
            try
            {
                //int prov = -1;
                //if (proveedor != "")
                //{
                //    prov = Convert.ToInt32(proveedor);
                //}
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Saldos_Proveedores_Cont", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.CommandTimeout = 0;
                //comando.Parameters.Add(new SqlParameter("@PROVEEDOR", prov));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetMovimientos(string compania, string cuenta, string fini, string ffin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_MovimientosDiarios", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPANIA", compania));
                comando.Parameters.Add(new SqlParameter("@CUENTA", cuenta));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetEstadoCuenta(string compañia, string fechaini, string fechafin, int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_EstadoCuenta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
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

        public DataTable GetFacturacionContado(string fechaini, string fechafin, char sucursal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_FacturacionContado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetFacturacionContadoActualizado(string fechaini, string fechafin, char sucursal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_FacturacionContadoActualizado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDetallePendientePagos(string fechaini, string fechafin, int Proveedor, string Empresa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_DetallePagos_Pendientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", Proveedor));
                comando.Parameters.Add(new SqlParameter("@EMPRESA", Empresa));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDetallePagos(string fechaini, string fechafin, int Proveedor, string Empresa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_DetallePagos_Pagados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", Proveedor));
                comando.Parameters.Add(new SqlParameter("@EMPRESA", Empresa));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable ObtenerLlenadoControlReporteDetallePagos()
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {

                conexion.Open();
                SqlCommand comando = new SqlCommand("ReporteApp.Reporte_LlenadoControl_Finanzas_DetallePagos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

            }
            catch
            {
                dtTemp = null;
                dtTemp.Dispose();
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }



        public DataTable GetListaProveedores()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT NombreCompleto as 'PROVEEDORES',DocumentoFiscal as 'RUC', CuentaMonedaLocal as 'CUENTA BANCARIA SOLES', CuentaMonedaExtranjera as 'CUENTA BANCARIA DOLARES',Telefono AS 'TELEFONO',CorreoElectronico AS 'CORREO ELECTRONICO' " +
                "FROM PersonaMast WHERE CuentaMonedaLocal " +
                "IS NOT NULL and EsProveedor='S' and NombreCompleto IS NOT NULL and Estado='A'";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListaClientes()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT NombreCompleto as 'CLIENTES', DocumentoFiscal as 'RUC', CorreoElectronico as 'CORREO ELECTRONICO', Telefono as 'TELEFONO'" +
                "FROM PersonaMast WHERE Estado='A'and EsCliente='S' and DocumentoFiscal is not NULL and CorreoElectronico is not NULL";
                comando.CommandType = CommandType.Text;
                comando.Connection = conexion;
                conexion.Open();
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public void GetUpdateFechaRecepcion(DateTime fecha, string documento)
        {
            string limpio = Regex.Replace(fecha.ToString(), @"[a.m.p.m.]", "");
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            string query = "";
            if (fecha != null)
            {
                query = "UPDATE CO_Documento SET FechaRecepcion='" + limpio + "'" +
                    " WHERE NumeroDocumento LIKE '%" + documento + "%' AND FechaRecepcion IS NULL";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            else
            {
                Console.Write("El documento " + documento + "ya tiene fecha de recepcion");
            }
            //query = "UPDATE CO_Documento SET FechaRecepcion='" + limpio + "'"+
            //        " WHERE NumeroDocumento LIKE '%" + documento + "%' AND FechaRecepcion=NULL";
            //SqlCommand comando = new SqlCommand(query, conexion);
            //comando.ExecuteNonQuery();
            //conexion.Close();
        }


        public void GetModificarFechaRecepcion(DateTime fecha, string documento)
        {
            string limpio = Regex.Replace(fecha.ToString(), @"[a.m.p.m.]", "");
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            string query = "";
            if (fecha != null)
            {
                query = "UPDATE CO_Documento SET FechaRecepcion='" + limpio + "'" +
                    " WHERE NumeroDocumento LIKE '%" + documento + "%' ";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            else
            {
                Console.Write("El documento " + documento + "ya tiene fecha de recepcion");
            }
            //query = "UPDATE CO_Documento SET FechaRecepcion='" + limpio + "'"+
            //        " WHERE NumeroDocumento LIKE '%" + documento + "%' AND FechaRecepcion=NULL";
            //SqlCommand comando = new SqlCommand(query, conexion);
            //comando.ExecuteNonQuery();
            //conexion.Close();
        }

        public DataTable GetListaCobranzasxClientes(string fechaini, string fechafin, string cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand("Select P.NombreCompleto AS 'CLIENTE','AÑO' = YEAR(CB.FechaCobranza), 'MES' = CASE MONTH(CB.FechaCobranza) WHEN '01' THEN '01 ENERO' WHEN '02' THEN '02 FEBRERO' WHEN '03' THEN '03 MARZO' WHEN '04' THEN '04 ABRIL' WHEN '05' THEN '05 MAYO' WHEN '06' THEN '06 JUNIO' WHEN '07' THEN '07 JULIO' WHEN '08' THEN '08 AGOSTO' WHEN '09' THEN '09 SETIEMBRE' WHEN '10' THEN '10 OCTUBRE' WHEN '11' THEN '11 NOVIEMBRE' ELSE '12 DICIEMBRE' END, DAY(CB.FechaCobranza) AS 'DIA', DC.MontoPagado AS MONTO from CO_Cobranza CB INNER JOIN PersonaMast P ON CB.Cliente=P.Persona INNER JOIN CO_DocumentoCobranza DC ON DC.CobranzaNumero=CB.CobranzaNumero INNER JOIN CO_Documento D ON D.NumeroDocumento=DC.NumeroDocumento Where CB.Estado NOT IN ('AN') AND CB.FechaCobranza Between '" + fechaini + "' AND '" + fechafin + "' AND P.NombreCompleto LIKE '%" + cliente + "%'", conexion);
                conexion.Open();
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListaCobranzasxBancos(string fechaini, string fechafin, string banco)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Cobranzas_Bancos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@BANCO", banco));
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                //SqlCommand comando = new SqlCommand("Select B.DescripcionCorta AS 'BANCO',CUB.Descripcion AS 'DESCRIPCION','AÑO' = YEAR(CB.FechaCobranza), 'MES' = CASE MONTH(CB.FechaCobranza) WHEN '01' THEN '01 ENERO' WHEN '02' THEN '02 FEBRERO' WHEN '03' THEN '03 MARZO' WHEN '04' THEN '04 ABRIL' WHEN '05' THEN '05 MAYO' WHEN '06' THEN '06 JUNIO' WHEN '07' THEN '07 JULIO' WHEN '08' THEN '08 AGOSTO' WHEN '09' THEN '09 SETIEMBRE' WHEN '10' THEN '10 OCTUBRE' WHEN '11' THEN '11 NOVIEMBRE' ELSE '12 DICIEMBRE' END, DAY(CB.FechaCobranza) AS 'DIA',DC.MontoPagado AS MONTO,D.NumeroDocumento from CO_Cobranza CB INNER JOIN PersonaMast P ON CB.Cliente=P.Persona INNER JOIN CO_DocumentoCobranza DC ON DC.CobranzaNumero=CB.CobranzaNumero INNER JOIN CO_Documento D ON D.NumeroDocumento=DC.NumeroDocumento INNER JOIN CO_CobranzaDetalle COD ON COD.CobranzaNumero=DC.CobranzaNumero INNER JOIN CuentaBancaria CUB ON CUB.CuentaBancaria=COD.CuentaBancariaPropia INNER JOIN Banco B ON B.Banco=CUB.Banco Where CB.Estado NOT IN ('AN') AND D.TipoDocumento='FC' AND CB.FechaCobranza Between '" + fechaini + "' AND '" + fechafin + "' AND B.DescripcionCorta LIKE '%" + banco + "%'", conexion);
                //SqlCommand comando = new SqlCommand("Select B.DescripcionCorta AS 'BANCO','AÑO' = YEAR(CB.FechaCobranza), 'MES' = CASE MONTH(CB.FechaCobranza) WHEN '01' THEN '01 ENERO' WHEN '02' THEN '02 FEBRERO' WHEN '03' THEN '03 MARZO' WHEN '04' THEN '04 ABRIL' WHEN '05' THEN '05 MAYO' WHEN '06' THEN '06 JUNIO' WHEN '07' THEN '07 JULIO' WHEN '08' THEN '08 AGOSTO' WHEN '09' THEN '09 SETIEMBRE' WHEN '10' THEN '10 OCTUBRE' WHEN '11' THEN '11 NOVIEMBRE' ELSE '12 DICIEMBRE' END, DAY(CB.FechaCobranza) AS 'DIA',D.MontoTotal AS MONTO,D.NumeroDocumento from CO_Cobranza CB INNER JOIN PersonaMast P ON CB.Cliente=P.Persona INNER JOIN CO_DocumentoCobranza DC ON DC.CobranzaNumero=CB.CobranzaNumero INNER JOIN CO_Documento D ON D.NumeroDocumento=DC.NumeroDocumento INNER JOIN CO_CobranzaDetalle COD ON COD.CobranzaNumero=DC.CobranzaNumero INNER JOIN CuentaBancaria CUB ON CUB.CuentaBancaria=COD.CuentaBancariaPropia INNER JOIN Banco B ON B.Banco=CUB.Banco Where CB.Estado NOT IN ('AN') AND D.TipoDocumento='FC' AND CB.FechaCobranza Between '" + fechaini + "' AND '" + fechafin + "' AND P.NombreCompleto LIKE '%" + banco + "%'", conexion);
                conexion.Open();
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetClientes(string cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                //SqlCommand comando = new SqlCommand("Select Busqueda from PersonaMast Where EsCliente='S' AND Estado='A' ORDER BY NombreCompleto", conexion);
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Clientes_Cobranzas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetBancos(string banco)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                //SqlCommand comando = new SqlCommand("Select Busqueda from PersonaMast Where EsCliente='S' AND Estado='A' ORDER BY NombreCompleto", conexion);
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Bancos_Cobranzas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@BANCO", banco));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetListaClientesCobranzaDetallado(string fechaini, string fechafin, string cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                //SqlCommand comando = new SqlCommand("Select Busqueda from PersonaMast Where EsCliente='S' AND Estado='A' ORDER BY NombreCompleto", conexion);
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Cobranzas_Clientes_Detallado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListaBancosCobranzaDetallado(string fechaini, string fechafin, string banco)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                //SqlCommand comando = new SqlCommand("Select Busqueda from PersonaMast Where EsCliente='S' AND Estado='A' ORDER BY NombreCompleto", conexion);
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Cobranzas_Bancos_Detallado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@BANCO", banco));
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetProyeccionCobranza(string fechaini, string fechafin, int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Proyeccion_Cobranzas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@PERIODOINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@PERIODOFIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //public DataTable GetProyeccionCobranza2(string compañia, string transpesa, string bra, string altra, string fechaini, string fechafin, int cliente)
        public DataTable GetProyeccionCobranza2(string compañia, string fechaini, string fechafin, int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Proyeccion_Cobranzas2", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
                //comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                //comando.Parameters.Add(new SqlParameter("@BRA", bra));
                //comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@PERIODOINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@PERIODOFIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetProyeccionCobranza2Resumen(string compañia, string fechaini, string fechafin, int cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Finanzas_Proyeccion_Cobranzas2_Resumen", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPAÑIA", compañia));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@PERIODOINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@PERIODOFIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListadoDeFacturas(string compania, string fechaini, string fechafin, string tipodocumento, int proveedor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ListarFacturas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPANIA", compania));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@TipoDocumento", tipodocumento));
                comando.Parameters.Add(new SqlParameter("@proveedor", proveedor));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetDESENLAZARRFACTURAS(string DocumentoRelacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ListarFacturasEnlazadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@DocumentoRelacion", DocumentoRelacion));
            



                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetModificarFactura(int opcion, string factura, string idviaje, string guia1, string codigoViaje, string Fechadocumento, string FechaVencimiento,
                                               decimal TramaFormaPago, string CompaniaSocio, string TipoDocumento, string comentario, string ubigeopartida,
                                                string ubigeollegada, string dirpartida, string dirllegada, decimal montoDetraccion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ModificarFactura", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));
                comando.Parameters.Add(new SqlParameter("@Factura", factura));
                comando.Parameters.Add(new SqlParameter("@Idviaje", idviaje));
                comando.Parameters.Add(new SqlParameter("@Guia1", guia1));
                comando.Parameters.Add(new SqlParameter("@CodigoViaje", codigoViaje));
                comando.Parameters.Add(new SqlParameter("@Fechadocumento", Fechadocumento));
                comando.Parameters.Add(new SqlParameter("@FechaVencimiento", FechaVencimiento));
                comando.Parameters.Add(new SqlParameter("@TramaFormaPago", TramaFormaPago));
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", CompaniaSocio));
                comando.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));
                comando.Parameters.Add(new SqlParameter("@Comentario", comentario));
                comando.Parameters.Add(new SqlParameter("@UbigeoPartida", ubigeopartida));
                comando.Parameters.Add(new SqlParameter("@UbigeoLlegada", ubigeollegada));
                comando.Parameters.Add(new SqlParameter("@DirPartida", dirpartida));
                comando.Parameters.Add(new SqlParameter("@DirLlegada", dirllegada));
                comando.Parameters.Add(new SqlParameter("@MontoDetraccion", montoDetraccion));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarFacturasFechas(string CompaniaSocio, string factura, string TipoDocumento)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ListarFacturasFechas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", CompaniaSocio));
                comando.Parameters.Add(new SqlParameter("@factura", factura));
                comando.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarFacturasEliminadas(string compania, string factura, string tipodocumento)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ListarFacturasEliminada", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", compania));
                comando.Parameters.Add(new SqlParameter("@factura  ", factura));
                comando.Parameters.Add(new SqlParameter("@TipoDocumento  ", tipodocumento));


                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarCompania(string CompaniaSocio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ListarCompania", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CompaniaSocio", CompaniaSocio));
               


                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetRegistrarAcesosxFactura(string Factura, string serie, string compania, string Usuario, int Descenlace, int ModificarMonto,
                                                    int EliminarFactura)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ListarFacturas_LiberarAccesoModificar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", compania));
                comando.Parameters.Add(new SqlParameter("@Serie", serie));
                comando.Parameters.Add(new SqlParameter("@Factura", Factura));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.Parameters.Add(new SqlParameter("@Descenlace", Descenlace));
                comando.Parameters.Add(new SqlParameter("@ModificarMonto", ModificarMonto));
                comando.Parameters.Add(new SqlParameter("@EliminarFactura", EliminarFactura));
               
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAccesosxFacturas(int Opcion, string factura, string compania, string tipodocumento, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ListarFacturas_ListarAccesos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Compania", compania));
                comando.Parameters.Add(new SqlParameter("@Serie", tipodocumento));
                comando.Parameters.Add(new SqlParameter("@Factura", factura));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarHistoricoFacturas(string FechaInicio, string fechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Facturacion_ListarHsitorico", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Desde", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@Hasta", fechaFin));               
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataSet ReportesApp_Costos_ControlPresupuestal_Listar(int Opcion, string Periodo, string CentroCosto, int Documento, int Inicio, int Final)
        {
            try
            {
                DataSet dtTemp = new DataSet();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                comando.Parameters.Add(new SqlParameter("@Documento", Documento));
                comando.Parameters.Add(new SqlParameter("@Inicio", Inicio));
                comando.Parameters.Add(new SqlParameter("@Final", Final));
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dtTemp);
                return dtTemp;
            }
            catch { return new DataSet(); }
        }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarCuadroComparativo(int Opcion, string Periodo, string CentroCosto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_ListarCuadroComparativo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dtTemp);
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarCentroCosto(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_ListarCentroCosto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarDocumentos(int Opcion, string Periodo, string TipoDocumento, string NumeroDocumento, string Voucher)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_ListarDocumentos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
                comando.Parameters.Add(new SqlParameter("@NumeroDocumento", NumeroDocumento));
                comando.Parameters.Add(new SqlParameter("@Voucher", Voucher));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_ControlPresupuestal_GenerarPresupuesto(string xmlPresupuesto, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_GenerarPresupuesto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@xmlPresupuesto", xmlPresupuesto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarPresupuestos(int Opcion, string Periodo, string CentroCosto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_ListarPresupuestos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_ControlPresupuestal_EliminarPresupuestos(int idPresupuesto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_EliminarPresupuestos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idPresupuesto", idPresupuesto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataSet ReportesApp_Costos_ControlPresupuestal_ListarPresupuestosCC(int Opcion, string Periodo, string CentroCosto)
        {
            try
            {
                DataSet dtTemp = new DataSet();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_ListarPresupuestosCC", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dtTemp);
                return dtTemp;
            }
            catch { return new DataSet(); }
        }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(int Opcion, string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_Cotizaciones_AgregarListarImplementos(int Opcion, string Tipo, string Descripcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_Cotizaciones_AgregarListarImplementos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Tipo", Tipo));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_Cotizaciones_DetalleInsertar(int Opcion, int idCotizacionC, int idImplemento, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_Cotizaciones_DetalleInsertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCotizacionC", idCotizacionC));
                comando.Parameters.Add(new SqlParameter("@idImplemento", idImplemento));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_Cotizaciones_DetalleListar(int idCotizacionC)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_Cotizaciones_DetalleListar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idCotizacionC", idCotizacionC));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion(int Opcion, int idCotizacionC, int CO, int P, int T, int CI, int O, int idRuta,
                         string TipoViaje, string PuntoInicio, string PuntoFin, decimal Frecuencia, string Producto, string RUCCliente, decimal ValorProducto, string Telefono,
                         string Contacto, string Embalaje, string Responsable, DateTime Duracion, DateTime ContratoInicio, DateTime ContratoFin, string Permisos,
                         decimal Tonelaje, string PermisosAdicionales, DateTime HorarioIni, DateTime HorarioFin, string Flota, decimal Mermas,
                         string Standby, string Politicas, DateTime FechaInicio, int NroConductor, string Origen, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCotizacionC", idCotizacionC));
                comando.Parameters.Add(new SqlParameter("@CO", CO));
                comando.Parameters.Add(new SqlParameter("@P", P));
                comando.Parameters.Add(new SqlParameter("@T", T));
                comando.Parameters.Add(new SqlParameter("@CI", CI));
                comando.Parameters.Add(new SqlParameter("@O", O));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@TipoViaje", TipoViaje));
                comando.Parameters.Add(new SqlParameter("@PuntoInicio", PuntoInicio));
                comando.Parameters.Add(new SqlParameter("@PuntoFin", PuntoFin));
                comando.Parameters.Add(new SqlParameter("@Frecuencia", Frecuencia));
                comando.Parameters.Add(new SqlParameter("@Producto", Producto));
                comando.Parameters.Add(new SqlParameter("@RUCCliente", RUCCliente));
                comando.Parameters.Add(new SqlParameter("@ValorProducto", ValorProducto));
                comando.Parameters.Add(new SqlParameter("@Telefono", Telefono));
                comando.Parameters.Add(new SqlParameter("@Contacto", Contacto));
                comando.Parameters.Add(new SqlParameter("@Embalaje", Embalaje));
                comando.Parameters.Add(new SqlParameter("@Responsable", Responsable));
                comando.Parameters.Add(new SqlParameter("@Duracion", Duracion));
                comando.Parameters.Add(new SqlParameter("@ContratoInicio", ContratoInicio));
                comando.Parameters.Add(new SqlParameter("@ContratoFin", ContratoFin));
                comando.Parameters.Add(new SqlParameter("@Permisos", Permisos));
                comando.Parameters.Add(new SqlParameter("@Tonelaje", Tonelaje));
                comando.Parameters.Add(new SqlParameter("@PermisosAdicionales", PermisosAdicionales));
                comando.Parameters.Add(new SqlParameter("@HorarioIni", HorarioIni));
                comando.Parameters.Add(new SqlParameter("@HorarioFin", HorarioFin));
                comando.Parameters.Add(new SqlParameter("@Flota", Flota));
                comando.Parameters.Add(new SqlParameter("@Mermas", Mermas));
                comando.Parameters.Add(new SqlParameter("@Standby", Standby));
                comando.Parameters.Add(new SqlParameter("@Politicas", Politicas));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@NroConductor", NroConductor));
                comando.Parameters.Add(new SqlParameter("@Origen", Origen));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_Cotizaciones_ListarRegistroCotizaciones(string FechaInicio, string FechaFin, string Cliente, string Flota, string Estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_Cotizaciones_ListarRegistroCotizaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                comando.Parameters.Add(new SqlParameter("@Flota", Flota));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones(int Opcion, int idCotizacionC, string Historial, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCotizacionC", idCotizacionC));
                comando.Parameters.Add(new SqlParameter("@Historial", Historial));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Costos_Cotizaciones_ListarDatos(int Opcion, string Ruta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Costos_Cotizaciones_ListarDatos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }
    }
}
