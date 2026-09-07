using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using Comun;

namespace AccesoDatos
{
    public class clsLogisticaDAO
    {
        private readonly static clsLogisticaDAO instancia = new clsLogisticaDAO();

        public static clsLogisticaDAO Instancia
        {
            get { return instancia; }
        }
        public DataTable GetDataServicios(string compania, string descripcion, string fechaini, string fechafin,int proveedor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_Servicios", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@USUARIO", Utilitario.Instancia.SesionUsuario.usuario));
                comando.Parameters.Add(new SqlParameter("@COMPANIA", compania));
                comando.Parameters.Add(new SqlParameter("@DESCRIPCION", descripcion));
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", proveedor));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataCompras(string compania, string periodoini, string periodofin,int proveedor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_Compras", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@COMPANIA", compania));
                comando.Parameters.Add(new SqlParameter("@PERIODO_INI", periodoini));
                comando.Parameters.Add(new SqlParameter("@PERIODO_FIN", periodofin));
                comando.Parameters.Add(new SqlParameter("@PROVEEDOR", proveedor));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataProdcutosxRotacion(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_ProductosxRotacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@PERIODO_FIN", fechafin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetLogistica_InventarioValorizadoPeriodoCerrado(string Compania, string Almacen, string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_InventarioValorizadoPeriodoCerrado_043_Reporte", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", Compania));
                comando.Parameters.Add(new SqlParameter("@AlmacenCodigo", Almacen));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Logistica_ListarAlmacenesInventario(int Opcion, string Compania)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_ListarAlmacenesInventario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Compania", Compania));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable GetListarPreciosCombustibleTerceros(int idProveedor,string lugar)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_PreciosTerceros", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdCliente", idProveedor));
                comando.Parameters.Add(new SqlParameter("@Lugar", lugar));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarPreciosCombustibleTerceros_Registrar(int opcion, string fecha, int idProveedor, decimal precio, string lugar, string Producto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_PreciosTerceros_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));
                comando.Parameters.Add(new SqlParameter("@IdFecha", fecha));
                comando.Parameters.Add(new SqlParameter("@IdCliente", idProveedor));
                comando.Parameters.Add(new SqlParameter("@Precio", precio));
                comando.Parameters.Add(new SqlParameter("@Lugar", lugar));
                comando.Parameters.Add(new SqlParameter("@Producto", Producto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable GetModificarOST(int opcion, string ost, decimal montoactual, decimal montoModificado, string Usuario, string TipoMoneda)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_ModficarOST", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", opcion));
                comando.Parameters.Add(new SqlParameter("@ost", ost));
                comando.Parameters.Add(new SqlParameter("@montoactual", montoactual));
                comando.Parameters.Add(new SqlParameter("@montoModificado", montoModificado));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));
                comando.Parameters.Add(new SqlParameter("@TipoMoneda", TipoMoneda));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        #region requerimientos Masivos
        // INSERTAR REQUERIMIENTOS MASIVOS 12/11/2022 - Sem Chavez
        public Boolean ReportesApp_Reporte_Importe_Masivo_Requerimientos(String xml, String xmlDetalle, String xmlCotizacion, string igv,string  tipo)
        {
            Boolean respuesta = false;
            SqlCommand cmd = null;

            try
            {

                SqlConnection cn = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

                if (tipo == "Gps")
                {
                    if (igv == "1.18")
                    {
                        cmd = new SqlCommand("ReportesApp_Reporte_Importe_Masivo_Requerimientos", cn);  // IGV 18%
                    }
                    else
                    {
                        cmd = new SqlCommand("[ReportesApp_Reporte_Importe_Masivo_Requerimientos_IGV]", cn); // IGV  10%
                    }
                }
                else // Peaje - Almuerzos
                {
                    cmd = new SqlCommand("ReportesApp_Reporte_Importe_Masivo_Requerimientos_Peajes", cn);  // IGV 18%
                }
            
               
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0; 
                cmd.Parameters.Add(new SqlParameter("@xml", xml));
                cmd.Parameters.Add(new SqlParameter("@xmlDetalle", xmlDetalle));
                cmd.Parameters.Add(new SqlParameter("@xmlCotizacion", xmlCotizacion));
                cmd.Parameters.Add(new SqlParameter("@igv", @igv));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));
                cn.Open();

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

        #endregion 

        
        public DataTable ReportesApp_Logistica_ListarItems(string filtro, string CodigoAlmacen)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_AlertaStock_ListarItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@filtro", filtro));
                cmd.Parameters.Add(new SqlParameter("@CodigoAlmacen", CodigoAlmacen));
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

        public String ReportesApp_Logistica_FiltrarItem(string filtro)
        {
            SqlCommand cmd = null;
            String respuesta = "";
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_FiltrarItem", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@filtro", filtro));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    respuesta = Convert.ToString(dr["Respuesta"]);
                }
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { cmd.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Logistica_InsertarAlertaStock(string Item, string DescripcionCompleta, int StockMinimo, int TiempoAlerta, string CodigoAlmacen)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Logistica_AlertaStock_Insertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Item", Item));
                comando.Parameters.Add(new SqlParameter("@DescripcionCompleta", DescripcionCompleta));
                comando.Parameters.Add(new SqlParameter("@StockMinimo", StockMinimo));
                comando.Parameters.Add(new SqlParameter("@TiempoAlerta", TiempoAlerta));
                comando.Parameters.Add(new SqlParameter("@CodigoAlmacen", CodigoAlmacen));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Logistica_ListarAlertasStock(string filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_AlertaStock_ListarAlertas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@filtro", filtro));
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

        public DataTable ReportesApp_Logistica_EliminarAlertaStock(int IdAlerta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Logistica_AlertaStock_Eliminar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                if (IdAlerta == 0)
                {
                    comando.Parameters.AddWithValue("@IdAlerta", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@IdAlerta", IdAlerta);
                }
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Logistica_AlertaStock_ListarAlmacenes()
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            SqlCommand comando;
            comando = new SqlCommand("ReportesApp_Logistica_AlertaStock_ListarAlmacenes", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            dtTemp.Load(comando.ExecuteReader());
            return dtTemp;
        }

        public DataTable ReportesApp_Mantenimiento_Reporte_RequerimientosALogistica(string fechaini, string fechafin, string Usuario, bool checkResumen)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_Reporte_RequerimientosALogistica", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FECHA_INICIAL", fechaini));
                cmd.Parameters.Add(new SqlParameter("@FECHA_FINAL", fechafin));
                cmd.Parameters.Add(new SqlParameter("@Usuario", fechaini));
                cmd.Parameters.Add(new SqlParameter("@checkResumen", checkResumen));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReportesApp_Logistica_RegistrarReclamoCliente(int idCliente, string Cliente, string Correo, string Telefono, string Reclamo, string contacto, string DetalleReclamo, byte[] imagen, string nombreImagen, string fechaIncidente, string observaciones, string txtNombrePDF, byte[] byteArrayPDF)
        {
          
            SqlCommand comando = null;
            bool respuesta = false;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Logistica_RegistrarReclamoClientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                comando.Parameters.AddWithValue("@Cliente", Cliente);
                comando.Parameters.AddWithValue("@Correo", Correo);
                comando.Parameters.AddWithValue("@Telefono", Telefono);
                comando.Parameters.AddWithValue("@Contacto", contacto);
                comando.Parameters.AddWithValue("@Reclamo", Reclamo);
               
                comando.Parameters.AddWithValue("@DetalleReclamo", DetalleReclamo);
                if (imagen == null) { comando.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { comando.Parameters.AddWithValue("@Imagen", imagen); }
                comando.Parameters.AddWithValue("@nombreImagen", nombreImagen);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@fechaIncidente", fechaIncidente);
                comando.Parameters.AddWithValue("@Observaciones", observaciones);
                comando.Parameters.AddWithValue("@nombrePDF", txtNombrePDF);
                comando.Parameters.AddWithValue("@pdf", byteArrayPDF);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }

            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Logistica_ListarReclamos(string fechaInicio, string fechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_ListarReclamos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
     
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReportesApp_Logistica_RegistrarDesargoCliente(string fechaProyectado, string descargo, int idReclamo, string responsable, byte[] byteArrayPDF)
        {
            SqlCommand comando = null;
            bool respuesta = false;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Logistica_RegistrarDesargoCliente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@fechaProyectado", fechaProyectado);
                comando.Parameters.AddWithValue("@descargo", descargo);
                comando.Parameters.AddWithValue("@idReclamo", idReclamo);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@responsable", responsable);
                //comando.Parameters.AddWithValue("@responsable", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@pdf", byteArrayPDF);
              
                

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }

            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public bool ReportesApp_Logistica_RegistrarSolucionCliente(string solucion, int idReclamo)
        {
            SqlCommand comando = null;
            bool respuesta = false;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Logistica_RegistrarSolucionCliente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@solucion", solucion);
                comando.Parameters.AddWithValue("@idReclamo", idReclamo);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }

            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public bool ReportesApp_Logistica_RegistrarNoSolucionCliente(string Respuesta, int idReclamo)
        {
            SqlCommand comando = null;
            bool respuesta = false;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Logistica_RegistrarNoSolucionCliente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@observacion", Respuesta);
                comando.Parameters.AddWithValue("@idReclamo", idReclamo);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }

            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Logistica_Listar_Req_CentroCostos(string FECHAINI, string FECHAFIN, string CentroCosto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Costos_Listar_Req_CentroCostos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaIni", FECHAINI));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FECHAFIN));
                if (CentroCosto == "")
                {
                    cmd.Parameters.Add(new SqlParameter("@CentroCosto", DBNull.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                }
              
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_CentroCostos()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Costos_CentroCostos_Maestro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_Listar_Req_CentroCostos_grafico(string FECHAINI, string FECHAFIN)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Costos_Listar_Req_CentroCostos_grafico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaIni", FECHAINI));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FECHAFIN));
        
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ListarTransacciones(string NumeroOT, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_TransaccionesMtto_ListarTransacciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroOT", NumeroOT));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ListarDetalleTransaccion(int Opcion, string NumeroOT)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_TransaccionesMtto_ListarDetalleTransaccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NumeroOT", NumeroOT));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_InsertarEliminarItem(int Opcion, string CodigoItem, string DescripcionItem, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_TransaccionesMtto_InsertarEliminarItem", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@CodigoItem", CodigoItem));
                cmd.Parameters.Add(new SqlParameter("@DescripcionItem", DescripcionItem));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ImprimirTicket(string NumeroOT, string xmlItem, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_TransaccionesMtto_ImprimirTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroOT", NumeroOT));
                cmd.Parameters.Add(new SqlParameter("@xmlItem", xmlItem));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ListarRegistroOT(string NumeroOT, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_TransaccionesMtto_ListarRegistroOT", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroOT", NumeroOT));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_EliminarTicketsImpresos(int idRegistroOT, string CodigoItem, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_TransaccionesMtto_EliminarTicketsImpresos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistroOT", idRegistroOT));
                cmd.Parameters.Add(new SqlParameter("@CodigoItem", CodigoItem));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_ItemsPendientes_Listar(string FechaInicio, string FechaFin, string CentroCosto, string Item, int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_ItemsPendientes_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                cmd.Parameters.Add(new SqlParameter("@Item", Item));
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_DetalleInsertar(int idEvaluacionC, string Proveedor, decimal PrecioUnitario, decimal FormaPago,
                                                                                 decimal PrecioTotal, decimal CostoFinanciero, decimal PrecioEquivalente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_EvaluacionOfertas_DetalleInsertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idEvaluacionC", idEvaluacionC));
                cmd.Parameters.Add(new SqlParameter("@Proveedor", Proveedor));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", PrecioUnitario));
                cmd.Parameters.Add(new SqlParameter("@FormaPago", FormaPago));
                cmd.Parameters.Add(new SqlParameter("@PrecioTotal", PrecioTotal));
                cmd.Parameters.Add(new SqlParameter("@CostoFinanciero", CostoFinanciero));
                cmd.Parameters.Add(new SqlParameter("@PrecioEquivalente", PrecioEquivalente));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_DetalleListar(int idEvaluacionC)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_EvaluacionOfertas_DetalleListar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idEvaluacionC", idEvaluacionC));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar(int Opcion, int idEvaluacionC, int idEvaluacionD, decimal Puntaje)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idEvaluacionC", idEvaluacionC));
                cmd.Parameters.Add(new SqlParameter("@idEvaluacionD", idEvaluacionD));
                cmd.Parameters.Add(new SqlParameter("@Puntaje", Puntaje));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_InsertarEvaluacion(int Opcion, int idEvaluacionC, string Servicio, string Unidad, decimal Cantidad,
                                                                                    decimal PrecioUnitario, decimal PrecioTotal, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_EvaluacionOfertas_InsertarEvaluacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idEvaluacionC", idEvaluacionC));
                cmd.Parameters.Add(new SqlParameter("@Servicio", Servicio));
                cmd.Parameters.Add(new SqlParameter("@Unidad", Unidad));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", PrecioUnitario));
                cmd.Parameters.Add(new SqlParameter("@PrecioTotal", PrecioTotal));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_ListarEvaluaciones(string FechaInicio, string FechaFin, string Codigo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_EvaluacionOfertas_ListarEvaluaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_AgregarAdicionales(int Opcion, int idEvaluacionC, decimal Descuento, string Conclusiones)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_EvaluacionOfertas_AgregarAdicionales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idEvaluacionC", idEvaluacionC));
                cmd.Parameters.Add(new SqlParameter("@Descuento", Descuento));
                cmd.Parameters.Add(new SqlParameter("@Conclusiones", Conclusiones));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataSet ReportesApp_Logistica_EvaluacionOfertas_ExportarExcel(int idEvaluacionC)
        {
            try
            {
                DataSet dtTemp = new DataSet();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Logistica_EvaluacionOfertas_ExportarExcel", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idEvaluacionC", idEvaluacionC));
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dtTemp);
                return dtTemp;
            }
            catch { return new DataSet(); }
        }

        public DataTable ReportesApp_Logistica_ReporteConsumibles(string fechaini, string fechafin)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
       
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica_ReporteConsumibles", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaini));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", fechafin));
        
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }
    }
}
