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
    public class clsCombustibleDAO
    {
        private readonly static clsCombustibleDAO instancia = new clsCombustibleDAO();

        public static clsCombustibleDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataDespachos(string fini, string ffin, string conductor, string tracto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Reporte_Despachos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));
                comando.Parameters.Add(new SqlParameter("@CONDUCTOR", conductor));
                comando.Parameters.Add(new SqlParameter("@TRACTO", tracto));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataRendimiento(string fini, string ffin, string placa, string cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Reporte_Detallado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", ffin));
                comando.Parameters.Add(new SqlParameter("@PLACA", placa));
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

        public DataTable GetDataRendimientoUnidad(string fini, string ffin, string tipo, string subtipo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Rendimiento_Unidades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", ffin));
                comando.Parameters.Add(new SqlParameter("@TIPOVEHICULO", tipo));
                comando.Parameters.Add(new SqlParameter("@SUBTIPOVEHICULO", subtipo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataDespachosDiarios(string fini, string ffin, string conductor, string tracto, int filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Reporte_Diario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));
                comando.Parameters.Add(new SqlParameter("@CONDUCTOR", conductor));
                comando.Parameters.Add(new SqlParameter("@TRACTO", tracto));
                comando.Parameters.Add(new SqlParameter("@FILTRO", filtro));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ObtenerReporteSurtidorAnexadoAlKardex(string fini, string ffin, string conductor, string tracto, int filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Reporte_Diario_Surtidor_Kardex", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));
                comando.Parameters.Add(new SqlParameter("@CONDUCTOR", conductor));
                comando.Parameters.Add(new SqlParameter("@TRACTO", tracto));
                comando.Parameters.Add(new SqlParameter("@FILTRO", filtro));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ObtenerReporteProgramacionesAnulados(string fini, string ffin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previaje_Anulados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ObtenerReporteKardex_Surtidor_Otros(string Periodo, int filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Reporte_Diario_Kardex_Surtidor_Otros", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO", Periodo));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetCombustible_StatusUnidades(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Viajes_StatusUnidades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetCombustible_StatusUnidadesPorRegiones(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Viajes_StatusUnidades_PorRegion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
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
        public DataTable GetCombustible_StatusUnidadesPorCiudad(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Viajes_StatusUnidades_PorCiudad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
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
        public DataTable GetCombustible_StatusUnidadesPorEstado(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Viajes_StatusUnidades_PorEstado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
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
        public DataTable GetCombustible_StatusUnidadesPorVelocidad(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Viajes_StatusUnidades_PorVelocidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
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
        public DataTable GetCombustible_PreciosDiarios(string fecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_PreciosDiarios_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Fecha", fecha));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetCombustible_PreciosDiarios_Nuevo_Modifa_elimina(int accion, string fecha, decimal precio, string user)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_PreciosDiarios_Nuevo_Modifa_elimina", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Accion", accion));
                comando.Parameters.Add(new SqlParameter("@Fecha", fecha));
                comando.Parameters.Add(new SqlParameter("@Precio", precio));
                comando.Parameters.Add(new SqlParameter("@Usuario", user));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetDataProgramacionPorPlaca(string Placa, string fini, string ffin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Reporte_ProgramacionPorUnidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fini));
                comando.Parameters.Add(new SqlParameter("@FechaFin", ffin));               

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }



        public DataTable GetDataDespachosDetalleDiarios(int idconductor,string FechaInicioDetalle, string FechaFinDetalle, string ConductorDetalle)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Reporte_DiarioBonosDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idconductor));
                comando.Parameters.Add(new SqlParameter("@FECHINI", FechaInicioDetalle));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", FechaFinDetalle));
                comando.Parameters.Add(new SqlParameter("@CONDUCTOR", ConductorDetalle));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetCombustible_RegularizarTicktesalKardex(int tipo, int ticket, int codigopreviaje, string placa, string dni, string conductor, decimal odometro,
                                                                    string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Kardex_RegularizarTickets", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Tipo", tipo));
                comando.Parameters.Add(new SqlParameter("@Ticket", ticket));
                comando.Parameters.Add(new SqlParameter("@Previaje", codigopreviaje));
                comando.Parameters.Add(new SqlParameter("@Placa", placa));
                comando.Parameters.Add(new SqlParameter("@Dni", dni));
                comando.Parameters.Add(new SqlParameter("@Conductor", conductor));
                comando.Parameters.Add(new SqlParameter("@Odometro", odometro));
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

        public DataTable Reporte_Combustible_Detalle_Viaje_Diario(int IdViaje, String Codigo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_Detalle_Viaje_Diario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdViaje", IdViaje);
                comando.Parameters.AddWithValue("@Codigo", Codigo);
                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);


                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getRegistrarBloqueoUnidadxRuta(int accion, int idTracto, string xmlRuta, string MotivoBloqueo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combusible_Registrar_UnidadesBloqueadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Accion", accion);
                comando.Parameters.AddWithValue("@idTracto", idTracto);
                comando.Parameters.AddWithValue("@xmlRuta", xmlRuta);
                comando.Parameters.AddWithValue("@MotivoBloqueo", MotivoBloqueo);
                comando.Parameters.AddWithValue("@Usuario", Usuario);

                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);

                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getListarUnidadesBloqueadas()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combusible_Listar_UnidadesBloqueadas", conexion);
                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetBuscarUnidadesBloqueadas(string BuscarPlaca)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combusible_Buscar_UnidadesBloqueadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Placa", BuscarPlaca);
                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Combusible_ListarRegistro_UnidadesBloqueadas(int idTracto, string Motivo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combusible_ListarRegistro_UnidadesBloqueadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdTracto", idTracto);
                comando.Parameters.AddWithValue("@MotivoBloqueo", Motivo);
                comando.CommandTimeout = 0;
                SqlDataReader dr = comando.ExecuteReader();
                dtTemp.Load(dr);
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Combusible_ListarHistorial_UnidadesBloqueadas(string fini, string ffin, string Placa)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combusible_ListarHistorial_UnidadesBloqueadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", ffin));
                comando.Parameters.Add(new SqlParameter("@TRACTO", Placa));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Combusible_EliminarRuta_UnidadesBloqueadas(int idRuta, int idTracto, string Motivo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combusible_EliminarRuta_UnidadesBloqueadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@IdTracto", idTracto));
                comando.Parameters.Add(new SqlParameter("@MotivoBloqueo", Motivo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        //GERARDO - 10/11
        public DataTable ReportesApp_Combustible_MetasRendimiento_ListarCombo(int Opcion, string Marca)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_MetasRendimiento_ListarCombo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Marca", Marca));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_ListarMetas(int IdOperacion, string TituloRuta, string Marca)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_MetasRendimiento_ListarMetas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.Parameters.Add(new SqlParameter("@TituloRuta", TituloRuta));
                comando.Parameters.Add(new SqlParameter("@Marca", Marca));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //GERARDO - 16/12
        public DataTable ReportesApp_Combustible_MetasRendimiento_Registrar(int IdOperacion, int idRegion, string TituloRuta, string Marca, int idModelo,
                                                                            decimal Meta, string xmlRuta, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_MetasRendimiento_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.Parameters.Add(new SqlParameter("@idRegion", idRegion));
                comando.Parameters.Add(new SqlParameter("@Titulo", TituloRuta));
                comando.Parameters.Add(new SqlParameter("@Marca", Marca));
                comando.Parameters.Add(new SqlParameter("@idModelo", idModelo));
                comando.Parameters.Add(new SqlParameter("@Meta", Meta));
                comando.Parameters.Add(new SqlParameter("@xmlRuta", xmlRuta));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_ListarRutasRendimiento(string Descripcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_MetasRendimiento_ListarRutasRendimiento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }
        //GERARDO - 16/12

        //GERARDO - 22/11
        public DataTable ReportesApp_Combustible_MetasRendimiento_FiltrarMetas(int idMetaRendimiento, int Anio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_MetasRendimiento_FiltrarMetas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idMetaRendimiento", idMetaRendimiento));
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_EliminarMetas(int idMetaRendimiento, int Anio, int idRutaEnlazada)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_MetasRendimiento_EliminarMetas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idMetaRendimiento", idMetaRendimiento));
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.Parameters.Add(new SqlParameter("@idRutaEnlazada", idRutaEnlazada));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_ActualizarMetas(int idMetaRendimiento, int Anio, decimal Meta, decimal Promedio, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_MetasRendimiento_ActualizarMetas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idMetaRendimiento", idMetaRendimiento));
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.Parameters.Add(new SqlParameter("@Meta", Meta));
                comando.Parameters.Add(new SqlParameter("@RendPromedio", Promedio));
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
        //GERARDO - 22/11

        public bool ReportesApp_Combustible_RegistrarUreaPorSucursal(string sucursal, int idplaca, string placa, string fecha, string cantidad,int conductor)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Combustible_RegistrarUreaPorAlmacen", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodAlmacen", sucursal));
                comando.Parameters.Add(new SqlParameter("@idPlaca", idplaca));
                comando.Parameters.Add(new SqlParameter("@Placa", placa));
                comando.Parameters.Add(new SqlParameter("@Fecha", fecha));
                comando.Parameters.Add(new SqlParameter("@Cantidad", cantidad));
                comando.Parameters.Add(new SqlParameter("@idConductor", conductor));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);
            
                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Combustible_ListarUreaPorAlmacenes(string fechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_ListarUreaPorAlmacen", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public bool ReportesApp_Combustible_AnularUrea(string notasalida)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Combustible_AnularUrea", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NotaSalida", notasalida));

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque(int Opcion, int idInspeccionT, int idVehiculo, string TipoAlerta, DateTime Fecha, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idInspeccionT", idInspeccionT));
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                comando.Parameters.Add(new SqlParameter("@TipoAlerta", TipoAlerta));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Combustible_InspeccionTanque_ListarInspeccionTanque(string Vehiculo, string FechaInicio, string FechaFin, string TipoInspeccion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_InspeccionTanque_ListarInspeccionTanque", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Vehiculo", Vehiculo));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@TipoInspeccion", TipoInspeccion));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Combustible_InspeccionTanque_BuscarInspeccion(int idVehiculo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Combustible_InspeccionTanque_BuscarInspeccion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }
    }
}
