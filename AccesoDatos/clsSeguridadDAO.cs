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
    public class clsSeguridadDAO
    {

        private readonly static clsSeguridadDAO instancia = new clsSeguridadDAO();

        public static clsSeguridadDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataListaFaltaConducta(string nombre)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Seguridad_Lista_Faltas_Cometidas_por_Nombre", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CONDUCTOR", nombre));
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListaFaltaConducta()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Seguridad_Lista_Faltas_Cometidas_por_Conductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                comando.CommandTimeout = 0;
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataFichaEmpleados(string transpesa, string bra, string altra, string amt, string aduanas, char estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_Reporte_Ficha_Empleados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TRANSPESA", transpesa));
                comando.Parameters.Add(new SqlParameter("@BRA", bra));
                comando.Parameters.Add(new SqlParameter("@ALTRA", altra));
                comando.Parameters.Add(new SqlParameter("@AMT", amt));
                comando.Parameters.Add(new SqlParameter("@ADUANAS", aduanas));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListaCalificativoConductores(string fecha,string OPERACION)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_CalificativoConductores_ListaDiaria", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@fecha", fecha));
                comando.Parameters.Add(new SqlParameter("@Operacion", OPERACION));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataCargarExcelCalificativo(string cadena,string periodo, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_CalificativoConductores_ExportarExcel", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@DataXML", cadena));
                comando.Parameters.Add(new SqlParameter("@Periodo", periodo));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }



        public DataTable GetListarDetalleCalificativoCoductores(string FechaInicioDetalle, string FechaFinDetalle, int _IdPersonaDetalle)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_CalificativoConductoresDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdConductor", _IdPersonaDetalle));
                comando.Parameters.Add(new SqlParameter("@FechIni", FechaInicioDetalle));
                comando.Parameters.Add(new SqlParameter("@FechFin", FechaFinDetalle));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_ListarComboTiposEPPS()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ListarComboTiposEPPS", conexion);
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
        public Boolean ReportesApp_Seguridad_Registrar_Actializar_AnularEPPS(string CodInterno, int idTipoEpps, int operacion, string areaProceso, string area, int idEPPS)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Seguridad_Registrar_Actializar_AnularEPPS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CodInterno", CodInterno);
                comando.Parameters.AddWithValue("@idTipoEPPS", idTipoEpps);
                comando.Parameters.AddWithValue("@areaProceso", areaProceso);
                comando.Parameters.AddWithValue("@area", area);
                comando.Parameters.AddWithValue("@Operacion", operacion);
                if (idEPPS == 0)
                {
                    comando.Parameters.AddWithValue("@idEPPS", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@idEPPS", idEPPS);
                }

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

        public DataTable ReportesApp_ListarEPPS(string filtroNombre, string FechaInicio, string FechaFin, int idTipoEPPS)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ListarEPPS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@filtroNombre", filtroNombre);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.Parameters.AddWithValue("@idTipoEPPS", idTipoEPPS);
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

        public DataTable GetRegistrarTipoEPPS(int accion, int TipoEPPS, string Nombre, byte Estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_RegistrarTipoEPPS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                comando.Parameters.Add(new SqlParameter("@TipoEPPS", TipoEPPS));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Accion", accion));


                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarTipoEPPS()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_ListarTipoEPPS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarPuestos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_ListarPuestosPersonal", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetRegistrarVidaUtilEPPS(int TipoEPPS, string areaProceso, string area, int cantidadMeses)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_IngresarVidaUtilEPPS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TipoEPPS", TipoEPPS));
                comando.Parameters.Add(new SqlParameter("@areaProceso", areaProceso));
                comando.Parameters.Add(new SqlParameter("@cantidadMeses", cantidadMeses));
                comando.Parameters.Add(new SqlParameter("@area", area));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetListarVidaUtilEPPS()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ListarVidaUtilEPPS", conexion);
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

        public DataTable FiltrarVidaUtilEPPS(string area)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_FiltrarVidaUtilEPPS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Puesto", area));
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

        public DataTable GetEditarVidaUtilEPPS(int idVidaUtil, int TipoEPPS, string areaProceso, string area, int cantidadMeses)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_EditarVidaUtilEPPS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                if (idVidaUtil == 0)
                {
                    comando.Parameters.AddWithValue("@idVidaUtil", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@idVidaUtil", idVidaUtil);
                }
                comando.Parameters.AddWithValue("@TipoEPPS", TipoEPPS);
                comando.Parameters.AddWithValue("@areaProceso", areaProceso);
                comando.Parameters.AddWithValue("@area", area);
                comando.Parameters.AddWithValue("@cantidadMeses", cantidadMeses);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Seguridad_ListarEmpleados(string filtroNombre)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ListarEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@filtroNombre", filtroNombre));
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
        public DataTable ReportesApp_Seguridad_InsertarEPPSPersonal(int idPersonal, string xml, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_InsertarEPPSPersonal", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idPersonal", idPersonal));
                comando.Parameters.Add(new SqlParameter("@xml", xml));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Seguridad_ListarEPPSxPersona(string filtroNombre, string FechaInicio, string FechaFin, int idTipoEPPS)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ListarEPPSxPersona", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@filtroNombre", filtroNombre));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idTipoEPPS", idTipoEPPS));
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

        public DataTable ReportesApp_Seguridad_EliminarEPPSxPersona(int idEPPSPersonal, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_EliminarEPPSxPersona", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                if (idEPPSPersonal == 0)
                {
                    comando.Parameters.AddWithValue("@idEPPSPersonal", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@idEPPSPersonal", idEPPSPersonal);
                }
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public Boolean ReportesApp_Seguridad_Registrar_Elimina_EditarGastoReten(int idGasto, string Nombre, int idPersona, string NroPlanilla, DateTime fechaRegistro, decimal importe, string destino, string observacion, int tipoOperacion, string esVale)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Seguridad_Registrar_Elimina_EditarGastosReten", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                if (idGasto == 0)
                {
                    comando.Parameters.AddWithValue("@idGasto", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@idGasto", idGasto);
                }

                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@idPersona", idPersona);
                comando.Parameters.AddWithValue("@NroPlanilla", NroPlanilla);
                comando.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);
                comando.Parameters.AddWithValue("@Importe", importe);
                comando.Parameters.AddWithValue("@Destino", destino);
                comando.Parameters.AddWithValue("@Observacion", observacion);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@TipoOperacion", tipoOperacion);
                comando.Parameters.AddWithValue("@esVale", esVale);
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

        public DataTable ReportesApp_Seguridad_ListarRetenes(string fecha,string FechaFin, string nombrePersonal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ListarRetenes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaFiltro", fecha));
                cmd.Parameters.Add(new SqlParameter("@FechaFiltroFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@NombrePersona", nombrePersonal));
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

        public DataTable ReportesApp_Seguridad_BuscarPersonal(string nombre)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Combustible_BuscarPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Filtro", nombre));
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

        public DataTable ReportesApp_Seguridad_EditarFechaAsignacion(int idEPPSPersonal, DateTime nuevaFecha, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_EditarFechaAsignacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEPPSPersonal", idEPPSPersonal);
                comando.Parameters.AddWithValue("@nuevaFecha", nuevaFecha);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Seguridad_SuspenderTempEPPS(int idEPPSPersonal, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Seguridad_SuspenderTempEPPS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEPPSPersonal", idEPPSPersonal);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }
        public DataTable ReportesApp_Seguridad_ListarHistorialRetenes(string fecha, string FechaFin, string nombrePersonal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ListarHistorialRetenes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaFiltro", fecha));
                cmd.Parameters.Add(new SqlParameter("@FechaFiltroFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@NombrePersona", nombrePersonal));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos(int Opcion, int idGrupo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasGrupos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_AsignarGrupos(int Persona, int idGrupo, int idOperacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_AsignarGrupos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                cmd.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_RegistrarAsistente(int Opcion, int idAsistente, int idCapacitacion, int idPersona, int idGrupo, int idArea, int Nota, string Condicion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_RegistrarAsistente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idAsistente", idAsistente));
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                cmd.Parameters.Add(new SqlParameter("@idGrupo", idGrupo));
                cmd.Parameters.Add(new SqlParameter("@idArea", idArea));
                cmd.Parameters.Add(new SqlParameter("@Nota", Nota));
                cmd.Parameters.Add(new SqlParameter("@Condicion", Condicion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarAsistentes(int idCapacitacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarAsistentes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_InsertarModificarCapacitacion(int Opcion, int idCapacitacion, string Titulo, int idTipo, int idLugar, DateTime Fecha, decimal Horas,
                                                                                                   string Instructor, int idProgramacion, int idDetalle, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_InsertarModificarCapacitacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@Titulo", Titulo));
                cmd.Parameters.Add(new SqlParameter("@idTipo", idTipo));
                cmd.Parameters.Add(new SqlParameter("@idLugar", idLugar));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@Horas", Horas));
                cmd.Parameters.Add(new SqlParameter("@Instructor", Instructor));
                cmd.Parameters.Add(new SqlParameter("@idProgramacion", idProgramacion));
                cmd.Parameters.Add(new SqlParameter("@idPDetalle", idDetalle));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarPersonal(string Filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Filtro", Filtro));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarCapacitaciones(string Capacitacion, string Instructor, string Personal, string FechaInicio, string FechaFin, int BuscarFecha, string Operacion, int Faltantes)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarCapacitaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Capacitacion", Capacitacion));
                cmd.Parameters.Add(new SqlParameter("@Instructor", Instructor));
                cmd.Parameters.Add(new SqlParameter("@Personal", Personal));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@BuscarFecha", BuscarFecha));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@Faltantes", Faltantes));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_FiltrarCapacitacion(int idCapacitacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_FiltrarCapacitacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasPermisos(string Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasPermisos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarOperacionGrupo()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarOperacionGrupo", conexion);
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarCombo(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarCombo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ProgramarCapacitacion(int Opcion, string Tema, int idProceso, string L, string M, string X, string J, string V, string S, int idMes, DateTime HoraInicio,
                                                                                           DateTime HoraFin, int idLugar, string xml, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ProgramarCapacitacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Tema", Tema));
                cmd.Parameters.Add(new SqlParameter("@idProceso", idProceso));
                cmd.Parameters.Add(new SqlParameter("@L", L));
                cmd.Parameters.Add(new SqlParameter("@M", M));
                cmd.Parameters.Add(new SqlParameter("@X", X));
                cmd.Parameters.Add(new SqlParameter("@J", J));
                cmd.Parameters.Add(new SqlParameter("@V", V));
                cmd.Parameters.Add(new SqlParameter("@S", S));
                cmd.Parameters.Add(new SqlParameter("@idMes", idMes));
                cmd.Parameters.Add(new SqlParameter("@HoraInicio", HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@HoraFin", HoraFin));
                cmd.Parameters.Add(new SqlParameter("@idLugar", idLugar));
                cmd.Parameters.Add(new SqlParameter("@xml", xml));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ListarProgramaciones(int TipoProg, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ListarProgramaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@TipoProg", TipoProg));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_EliminarProgramacion(int TipoProg, int idProgramacion, int Nro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_EliminarProgramacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@TipoProg", TipoProg));
                cmd.Parameters.Add(new SqlParameter("@idProgramacion", idProgramacion));
                cmd.Parameters.Add(new SqlParameter("@idPDetalle", Nro));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_FiltrarProgramaciones(string Tema)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_FiltrarProgramaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Tema", Tema));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_EliminarCapacitacion(int idCapacitacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_EliminarCapacitacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_InsertarTitulo(int Especifico, string Titulo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_InsertarTitulo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Especifico", Especifico));
                cmd.Parameters.Add(new SqlParameter("@Titulo", Titulo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_FiltrarTemas(string Titulo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_FiltrarTemas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Tema", Titulo));
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

        public DataTable ReportesApp_Seguridad_ControlCapacitaciones_ProgramarFechas(int idPersonal, int idTitulo, DateTime FechaProgramada, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_ControlCapacitaciones_ProgramarFechas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idPersonal", idPersonal));
                cmd.Parameters.Add(new SqlParameter("@idTitulo", idTitulo));
                cmd.Parameters.Add(new SqlParameter("@FechaProgramada", FechaProgramada));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
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

        public DataTable ReportesApp_Seguridad_AlcoholTest_BuscarSede(string Usuario, int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_AlcoholTest_BuscarSede", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_AlcoholTest_BuscarPersonal(string DNI)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_AlcoholTest_BuscarPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@DNI", DNI));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_AlcoholTest_IngresarResultado(int idPersona, int ResultadoTest, string Horario, string Sede, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_AlcoholTest_IngresarResultado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                cmd.Parameters.Add(new SqlParameter("@ResultadoTest", ResultadoTest));
                cmd.Parameters.Add(new SqlParameter("@Horario", Horario));
                cmd.Parameters.Add(new SqlParameter("@Sede", Sede));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_AlcoholTest_ListarResultados(string Sede, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_AlcoholTest_ListarResultados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Sede", Sede));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_AlcoholTest_EliminarResultados(int Opcion, int idAlcoholTest, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_AlcoholTest_EliminarResultados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idAlcoholTest", idAlcoholTest));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_AlcoholTest_AgregarImagen(int idAlcoholTest, byte[] Imagen, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_AlcoholTest_AgregarImagen", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idAlcoholTest", idAlcoholTest));
                if (Imagen == null) { cmd.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@Imagen", Imagen); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }
        
        public DataTable ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades(int Opcion, string Descripcion, string xml)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@xml", xml));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(int Opcion, string Descripcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades(int Opcion, int idAO)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idAO", idAO));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_DesvincularObjetivosActividades(int Opcion, int idObjetivo, int idActividad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_DesvincularObjetivosActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idObjetivo", idObjetivo));
                cmd.Parameters.Add(new SqlParameter("@idActividad", idActividad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_AsignarObjetivosActividades(int Opcion, int idObjetivo, string xml)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_AsignarObjetivosActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idOO", idObjetivo));
                cmd.Parameters.Add(new SqlParameter("@xml", xml));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(int Opcion, string Filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_ListarPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Filtro", Filtro));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_GenerarGestionActividades(int Persona, int idActividad, decimal Peso, string Validacion, string Cronograma, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_GenerarGestionActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@idActividad", idActividad));
                cmd.Parameters.Add(new SqlParameter("@Peso", Peso));
                cmd.Parameters.Add(new SqlParameter("@Validacion", Validacion));
                cmd.Parameters.Add(new SqlParameter("@Cronograma", Cronograma));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarGestionActividades(string Actividad, string Area, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_ListarGestionActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Actividad", Actividad));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarPersonalResponsable(int idGestionActividad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_ListarPersonalResponsable", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idGestionActividad", idGestionActividad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_AsignarPersonalResponsable(int idGestionActividad, int Persona, decimal Peso)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_AsignarPersonalResponsable", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idGestionActividad", idGestionActividad));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@Peso", Peso));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_BuscarPersonalResponsable(int Opcion, int idResponsable, int idGestionActividad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_BuscarPersonalResponsable", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idResponsable", idResponsable));
                cmd.Parameters.Add(new SqlParameter("@idGestionActividad", idGestionActividad));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ValidarUsuario(int idGestionActividad, string Validacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_ValidarUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idGestionActividad", idGestionActividad));
                cmd.Parameters.Add(new SqlParameter("@Validacion", Validacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_AsignarCronograma(int idCronograma, int idGestionActividad, DateTime FechaCronograma, decimal Meta, decimal Meta2, string TipoCronograma)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_AsignarCronograma", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCronograma", idCronograma));
                cmd.Parameters.Add(new SqlParameter("@idGestionActividad", idGestionActividad));
                cmd.Parameters.Add(new SqlParameter("@FechaCronograma", FechaCronograma));
                cmd.Parameters.Add(new SqlParameter("@Meta", Meta));
                cmd.Parameters.Add(new SqlParameter("@Meta2", Meta2));
                cmd.Parameters.Add(new SqlParameter("@TipoCronograma", TipoCronograma));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ListarCronogramaActividades(int idGestionActividad, string FechaInicio, string FechaFin, string TipoCronograma)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_ListarCronogramaActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idGestionActividad", idGestionActividad));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@TipoCronograma", TipoCronograma));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_GestionSeguridad_ModificarCronograma(int Opcion, int idGestionActividad, int idCronograma, decimal MetaUsuario, string TipoCronograma, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_GestionSeguridad_ModificarCronograma", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idGestionActividad", idGestionActividad));
                cmd.Parameters.Add(new SqlParameter("@idCronograma", idCronograma));
                cmd.Parameters.Add(new SqlParameter("@MetaUsuario", MetaUsuario));
                cmd.Parameters.Add(new SqlParameter("@TipoCronograma", TipoCronograma));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ListarAccidentes(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_ListarAccidentes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias(int Opcion, int idRegistroInc, int idPersona, string Persona, string Area, string Sede,
                                                                   string Grado, string Fecha, string Hora, int TipoIncidente, string TipoDanio, string Tracto, string Carreta, string Operacion,
                                                                   string DescripcionDanio, string Observacion, byte[] Incidente, byte[] IAdicional, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistroInc", idRegistroInc));
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@Sede", Sede));
                cmd.Parameters.Add(new SqlParameter("@Grado", Grado));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@Hora", Hora));
                cmd.Parameters.Add(new SqlParameter("@TipoIncidente", TipoIncidente));
                cmd.Parameters.Add(new SqlParameter("@TipoDanio", TipoDanio));
                cmd.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                cmd.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@DescripcionDanio", DescripcionDanio));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                if (Incidente == null) { cmd.Parameters.AddWithValue("@Incidente", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@Incidente", Incidente); }
                if (IAdicional == null) { cmd.Parameters.AddWithValue("@IAdicional", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@IAdicional", IAdicional); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ListarIncidencias(string TipoIncidente, string Sede, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_ListarIncidencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@TipoIncidente", TipoIncidente));
                cmd.Parameters.Add(new SqlParameter("@Sede", Sede));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_InsertarIncidente(string Incidente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroIncidencias_InsertarIncidente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Incidente", Incidente));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroEMO_ListarPersonal(string Filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroEMO_ListarPersonal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Filtro", Filtro));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroEMO_ListarEMO(string Conductor, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroEMO_ListarEMO", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroEMO_RegistrarEMO(int Opcion, int idPersona, string TipoSangre, string TipoEnfermedad, DateTime FechaInicioValidez,
                                                                        DateTime FechaFinValidez, string Categoria, string Usuario, string RutaLocal)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroEMO_RegistrarEMO", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                cmd.Parameters.Add(new SqlParameter("@TipoSangre", TipoSangre));
                cmd.Parameters.Add(new SqlParameter("@TipoEnfermedad", TipoEnfermedad));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioValidez", FechaInicioValidez));
                cmd.Parameters.Add(new SqlParameter("@FechaFinValidez", FechaFinValidez));
                cmd.Parameters.Add(new SqlParameter("@Categoria", Categoria));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.Parameters.Add(new SqlParameter("@RutaLocal", RutaLocal));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroEMO_RegistrarEMOResultado(int Opcion, int idPersona, string TipoSangre, string TipoEnfermedad, DateTime FechaInicioValidez,
                                                                        DateTime FechaFinValidez, string Categoria, string Usuario, string RutaLocal, string Resultados, string Resultados2, string Resultados3)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroEMO_RegistrarEMOResultado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idPersona", idPersona));
                cmd.Parameters.Add(new SqlParameter("@TipoSangre", TipoSangre));
                cmd.Parameters.Add(new SqlParameter("@TipoEnfermedad", TipoEnfermedad));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioValidez", FechaInicioValidez));
                cmd.Parameters.Add(new SqlParameter("@FechaFinValidez", FechaFinValidez));
                cmd.Parameters.Add(new SqlParameter("@Categoria", Categoria));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.Parameters.Add(new SqlParameter("@RutaLocal", RutaLocal));
                cmd.Parameters.Add(new SqlParameter("@Resultados", Resultados));
                cmd.Parameters.Add(new SqlParameter("@Resultados2", Resultados2));
                cmd.Parameters.Add(new SqlParameter("@Resultados3", Resultados3));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_RegistroEMO_ListarHistorialEMO(string Conductor, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_RegistroEMO_ListarHistorialEMO", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_BuscarUsuarios(string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_DocumentosSIG_BuscarUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_ListarAreas(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_DocumentosSIG_ListarAreas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_RegistrarEditarDocumentos(int Opcion, int idDocumentoSIG, byte[] Archivo, string Titulo,
                                                                                       string Area, string Proceso, int Version, string Extension, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_DocumentosSIG_RegistrarEditarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idDocumentoSIG", idDocumentoSIG));

                SqlParameter pArchivo = new SqlParameter("@Archivo", SqlDbType.VarBinary, -1);
                if (Archivo == null) { pArchivo.Value = DBNull.Value; }
                else { pArchivo.Value = Archivo; }
                cmd.Parameters.Add(pArchivo);

                cmd.Parameters.Add(new SqlParameter("@Titulo", Titulo));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@Proceso", Proceso));
                cmd.Parameters.Add(new SqlParameter("@Version", Version));
                cmd.Parameters.Add(new SqlParameter("@Extension", Extension));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_ListarDocumentos(string Titulo, string Area, string Proceso, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_DocumentosSIG_ListarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Titulo", Titulo));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@Proceso", Proceso));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_EliminarDocumentos(int Opcion, int idDocumentoSIG)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_DocumentosSIG_EliminarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idDocumentoSIG", idDocumentoSIG));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(int Opcion, int idDocumentoSIG)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idDocumentoSIG", idDocumentoSIG));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_DocumentosSIG_ListarOperaciones(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_DocumentosSIG_ListarOperaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_PersonalExterno_InsertarEliminarNombres(int Opcion, int Nro, string Apellidos, string Nombres, string DNI)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_PersonalExterno_InsertarEliminarNombres", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Nro", Nro));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Nombres", Nombres));
                cmd.Parameters.Add(new SqlParameter("@DNI", DNI));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_PersonalExterno_ListarNombres()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_PersonalExterno_ListarNombres", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_PersonalExterno_RegistrarExterno(int Opcion, string EmpresaExt, byte[] Archivo, string TituloArchivo, string ExtensionArchivo,
                                                                                int PersonaResp, string Area, string Motivo, DateTime FechaIngreso, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_PersonalExterno_RegistrarExterno", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@EmpresaExt", EmpresaExt));

                SqlParameter pArchivo = new SqlParameter("@Archivo", SqlDbType.VarBinary, -1);
                if (Archivo == null) { pArchivo.Value = DBNull.Value; }
                else { pArchivo.Value = Archivo; }
                cmd.Parameters.Add(pArchivo);

                cmd.Parameters.Add(new SqlParameter("@TituloArchivo", TituloArchivo));
                cmd.Parameters.Add(new SqlParameter("@ExtensionArchivo", ExtensionArchivo));
                cmd.Parameters.Add(new SqlParameter("@PersonaResp", PersonaResp));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@FechaIngreso", FechaIngreso));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_PersonalExterno_ListarExterno(string FechaInicio, string FechaFin, string Area, string PersonalExt, string EmpresaExt)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_PersonalExterno_ListarExterno", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@PersonalExt", PersonalExt));
                cmd.Parameters.Add(new SqlParameter("@EmpresaExt", EmpresaExt));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos(int Opcion, int idRegistroExt, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistroExt", idRegistroExt));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Seguridad_PersonalExterno_IngresarTiempos(int idRegistroExt, DateTime FechaIngreso, DateTime FechaSalida, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Seguridad_PersonalExterno_IngresarTiempos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistroExt", idRegistroExt));
                cmd.Parameters.Add(new SqlParameter("@FechaIngreso", FechaIngreso));
                cmd.Parameters.Add(new SqlParameter("@FechaSalida", FechaSalida));
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
