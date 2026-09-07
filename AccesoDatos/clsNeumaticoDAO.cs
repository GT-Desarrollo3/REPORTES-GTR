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
    public class clsNeumaticoDAO
    {
        private readonly static clsNeumaticoDAO instancia = new clsNeumaticoDAO();
        public static clsNeumaticoDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable GetNeumaticoConsumo(string fini, string ffin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Neumatico_Reporte_Consumo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.CommandTimeout = 900000;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

	    public DataTable ReportesApp_Neumatico_Consultar_KMVehiculos(string fechaInicio, string fechaFin, string placa, int filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_Consultar_KMVehiculos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FECHINI", fechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FECHFIN", fechaFin));
                cmd.Parameters.Add(new SqlParameter("@PLACA", placa));
                cmd.Parameters.Add(new SqlParameter("@FILTRO", filtro));
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

        public DataTable ReportesApp_Neumatico_ListarKMUnidades()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ListarKMUnidades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_InsertarKMUnidades(string placa, string fechaInicio, string fechaFin, string TipoVehiculo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_InsertarKMUnidades", conexion);
                cmd.CommandTimeout = 6000;
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoVehiculo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_Listar(int Opcion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_Listar_SegundoUso", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", Opcion);
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_Registrar_Neumatico_SegundoUso(int CodNeumatico, string Medida, string Marca, string Proveedor, DateTime FechaEnvio,
                                                                              string Estado, string DocumentoEvaluacion, string GRR, byte[] imagen)
        {
            DataTable respuesta = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_Maestro_SegundoUso_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CodNeumatico", CodNeumatico);
                comando.Parameters.AddWithValue("@Medida", Medida);
                comando.Parameters.AddWithValue("@Marca", Marca);
                comando.Parameters.AddWithValue("@Proveedor", Proveedor);
                comando.Parameters.AddWithValue("@FechaEnvio", FechaEnvio);
                comando.Parameters.AddWithValue("@Estado", Estado);
                comando.Parameters.AddWithValue("@DocumentoEvaluacion", DocumentoEvaluacion);
                comando.Parameters.AddWithValue("@GRR", GRR);
                if (imagen == null) { comando.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { comando.Parameters.AddWithValue("@Imagen", imagen); }
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();
                respuesta.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Neumatico_ListarReencauche_SegundoUso(string Codigo, string Medida, string Observacion, string FechaInicio, string FechaFin)
        {
            DataTable respuesta = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_ListarReencauche_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Codigo", Codigo);
                comando.Parameters.AddWithValue("@Medida", Medida);
                comando.Parameters.AddWithValue("@Observacion", Observacion);
                comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                SqlDataReader dr = comando.ExecuteReader();
                respuesta.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Neumatico_Maestro_SegundoUso_EliminarNeumaticos(int idReencauche)
        {
            DataTable respuesta = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_Maestro_SegundoUso_EliminarNeumaticos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idReencauche", idReencauche);
                SqlDataReader dr = comando.ExecuteReader();
                respuesta.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Neumatico_SegundoUso_InsertarReencauchados(int idReencauche, DateTime FechaRecepcion, string Estado, string GRR, string Disenio, Decimal Costo)
        {
            DataTable respuesta = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_SegundoUso_InsertarReencauchados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idReencauche", idReencauche);
                comando.Parameters.AddWithValue("@FechaRecepcion", FechaRecepcion);
                comando.Parameters.AddWithValue("@Estado", Estado);
                comando.Parameters.AddWithValue("@GRR", GRR);
                comando.Parameters.AddWithValue("@Disenio", Disenio);
                comando.Parameters.AddWithValue("@Costo", Costo);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();
                respuesta.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Neumatico_FiltrarNeumatico_SegundoUso(string CodigoNeu)
        {
            DataTable respuesta = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_FiltrarNeumatico_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@CodigoNeu", CodigoNeu);
                SqlDataReader dr = comando.ExecuteReader();
                respuesta.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public bool ReportesApp_Neumatico_RegistrarIngreso_SegundoUso(string GRR, DateTime FechaIngreso, int Cantidad, string Item)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_RegistrarIngreso_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@GRR", GRR);
                comando.Parameters.AddWithValue("@FechaIngreso", FechaIngreso);
                comando.Parameters.AddWithValue("@Cantidad", Cantidad);
                comando.Parameters.AddWithValue("@Item", Item);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read()) { respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia); }
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Neumatico_ListarIngreso_SegundoUso(int Opcion, string nombreIngreso, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ListarIngreso_SegundoUso", conexion);
                cmd.Parameters.AddWithValue("@Opcion", Opcion);
                cmd.Parameters.AddWithValue("@Nombre", nombreIngreso);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_SegundoUso_ListarIngreso(int Opcion, string GRR, string Neumatico, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_SegundoUso_ListarIngresos", conexion);
                cmd.Parameters.AddWithValue("@Opcion", Opcion);
                cmd.Parameters.AddWithValue("@Guia", GRR);
                cmd.Parameters.AddWithValue("@Neumatico", Neumatico);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_ListarOT_SegundoUso(string NumeroOrden)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ListarOT_SegundoUso", conexion);
                cmd.Parameters.AddWithValue("@NumeroOrden", NumeroOrden);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_RegistrarSalida_SegundoUso(int idIngreso, string orden, int cantidad, DateTime FechaSalida)
        {
            DataTable dt = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_RegistrarSalida_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idIngreso", idIngreso);
                comando.Parameters.AddWithValue("@OT", orden);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@FechaSalida", FechaSalida);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_AnularSalida_SegundoUso(int idRegistro)
        {
            DataTable dt = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_AnularSalida_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idRegistro", idRegistro);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_RegistrarRetorno_SegundoUso(int idSalida, int idIngresoNeu, int Cantidad)
        {
            DataTable dt = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_RegistrarRetorno_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idSalida", idSalida);
                comando.Parameters.AddWithValue("@idIngresoNeu", idIngresoNeu);
                comando.Parameters.AddWithValue("@Cantidad", Cantidad);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_RegistrarReclamo_SegundoUso(int idIngreso, string Motivo, DateTime FechaReclamo, int CantidadReclamo, string GRReclamo)
        {
            DataTable dt = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_RegistrarReclamo_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idRegistro", idIngreso);
                comando.Parameters.AddWithValue("@Motivo", Motivo);
                comando.Parameters.AddWithValue("@FechaReclamo", FechaReclamo);
                comando.Parameters.AddWithValue("@CantidadReclamo", CantidadReclamo);
                comando.Parameters.AddWithValue("@GRReclamo", GRReclamo);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_QuitarReclamo_SegundoUso(int idIngreso, int idReclamo)
        {
            DataTable dt = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_QuitarReclamo_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idRegistro", idIngreso);
                comando.Parameters.AddWithValue("@idReclamo", idReclamo);
                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_ActualizarReclamo_SegundoUso(int idReclamo, string Estado)
        {
            DataTable dt = new DataTable();
            SqlCommand comando = null;
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Neumatico_ActualizarReclamo_SegundoUso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idReclamo", idReclamo);
                comando.Parameters.AddWithValue("@Estado", Estado);
                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros(int Opcion, string Placa, string TipoMaquina, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoMaquina", TipoMaquina));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ActualizarRegistros(int idRegistro, DateTime UltimaFecha, decimal UltimoKM, int L1E1, int L1E2,
                                                                                     int L1E3, int L2E1, int L2E2, int L2E3, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ControlNeumaticos_ActualizarRegistros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistro", idRegistro));
                cmd.Parameters.Add(new SqlParameter("@UltimaFecha", UltimaFecha));
                cmd.Parameters.Add(new SqlParameter("@UltimoKM", UltimoKM));
                cmd.Parameters.Add(new SqlParameter("@L1E1", L1E1));
                cmd.Parameters.Add(new SqlParameter("@L1E2", L1E2));
                cmd.Parameters.Add(new SqlParameter("@L1E3", L1E3));
                cmd.Parameters.Add(new SqlParameter("@L2E1", L2E1));
                cmd.Parameters.Add(new SqlParameter("@L2E2", L2E2));
                cmd.Parameters.Add(new SqlParameter("@L2E3", L2E3));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_GenerarCumplimiento(string Placa, string Operacion, string TipoUnidad, string Marca, string Alineamiento, DateTime FechaProgramada,
                         DateTime FCInicio, DateTime FCFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ControlNeumaticos_GenerarCumplimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                cmd.Parameters.Add(new SqlParameter("@Marca", Marca));
                cmd.Parameters.Add(new SqlParameter("@Alineamiento", Alineamiento));
                cmd.Parameters.Add(new SqlParameter("@FechaProgramada", FechaProgramada));
                cmd.Parameters.Add(new SqlParameter("@FCInicio", FCInicio));
                cmd.Parameters.Add(new SqlParameter("@FCFin", FCFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ListarCumplimiento(int Anio, int NroSemana)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ControlNeumaticos_ListarCumplimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                cmd.Parameters.Add(new SqlParameter("@NroSemana", NroSemana));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ProgramarCumplimiento(int Opcion, int Nro, DateTime FechaCump, string Estado, string Observacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Neumatico_ControlNeumaticos_ProgramarCumplimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Nro", Nro));
                cmd.Parameters.Add(new SqlParameter("@FechaCump", FechaCump));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
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
