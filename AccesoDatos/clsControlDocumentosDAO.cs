using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;



namespace AccesoDatos
{
    public class clsControlDocumentosDAO
    {
        private readonly static clsControlDocumentosDAO instancia = new clsControlDocumentosDAO();

        public static clsControlDocumentosDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable getDocumentos_LlenarControlesDocumentos(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_LlenadoControlesControlDocumentos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Usuario", usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable getDocumentos_ListarDocumentos(string compania, int tipoDocumento, int tipoFiltro, string filtro, int tipoUnidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", compania));
                comando.Parameters.Add(new SqlParameter("@TipoDocumento", tipoDocumento));
                comando.Parameters.Add(new SqlParameter("@TipoFiltro", tipoFiltro));
                comando.Parameters.Add(new SqlParameter("@Filtro", filtro));
                comando.Parameters.Add(new SqlParameter("@TipoUnidad", tipoUnidad));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        //GERARDO - 01/09
        public DataTable getDocumento_RegistrarDocumento(string compania, string sucursal, string tipoRelacion, int idRelacion, int idTipoDocumento, string codigo, string centroCosto, 
                                                         string situacionRenovacion, string fechaEmision, string fechaInicioValidez, string fechaFinValidez, int esVencimiento,
                                                         string moneda, decimal montoTotal, int esAfectoValidacion, string usuarioCrea, string observacion, string categoria, string RutaLocal, string RutaNube)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Compania", compania);
                comando.Parameters.AddWithValue("@Sucursal", sucursal);
                comando.Parameters.AddWithValue("@TipoRelacion", tipoRelacion);
                comando.Parameters.AddWithValue("@IdRelacion", idRelacion);
                comando.Parameters.AddWithValue("@IdTipoDocumento", idTipoDocumento);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@CentroCosto", centroCosto);
                comando.Parameters.AddWithValue("@SituacionRenovacion", situacionRenovacion);
                comando.Parameters.AddWithValue("@FechaEmision", fechaEmision);
                comando.Parameters.AddWithValue("@FechaInicioValidez", fechaInicioValidez);
                comando.Parameters.AddWithValue("@FechaFinValidez", fechaFinValidez);
                comando.Parameters.AddWithValue("@EsVencimiento", esVencimiento);
                comando.Parameters.AddWithValue("@Moneda", moneda);
                comando.Parameters.AddWithValue("@MontoLN", montoTotal);
                comando.Parameters.AddWithValue("@EsAfectoValidacion", esAfectoValidacion);       
                comando.Parameters.AddWithValue("@UsuarioCrea", usuarioCrea);
                comando.Parameters.AddWithValue("@Observacion", observacion);
                comando.Parameters.AddWithValue("@Categoria", categoria);
                comando.Parameters.AddWithValue("@DirectorioLocal", RutaLocal);
                comando.Parameters.AddWithValue("@DirectorioNube", RutaNube);
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getDocumento_ActualizarDocumento(int idDocumento, string compania, string sucursal, string tipoRelacion, int idRelacion, int idTipoDocumento, string codigo, string centroCosto,
                                                         string situacionRenovacion, string fechaEmision, string fechaInicioValidez, string fechaFinValidez, int esVencimiento,
                                                         string moneda, decimal montoTotal, int esAfectoValidacion, string usuarioActualiza, string observacion, string categoria, string RutaLocal, string RutaNube)
        {
            try
            {
               DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_Actualizar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdDocumento", idDocumento);
                comando.Parameters.AddWithValue("@Compania", compania);
                comando.Parameters.AddWithValue("@Sucursal", sucursal);
                comando.Parameters.AddWithValue("@TipoRelacion", tipoRelacion);
                comando.Parameters.AddWithValue("@IdRelacion", idRelacion);
                comando.Parameters.AddWithValue("@IdTipoDocumento", idTipoDocumento);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@CentroCosto", centroCosto);
                comando.Parameters.AddWithValue("@SituacionRenovacion", situacionRenovacion);
                comando.Parameters.AddWithValue("@FechaEmision", fechaEmision);
                comando.Parameters.AddWithValue("@FechaInicioValidez", fechaInicioValidez);
                comando.Parameters.AddWithValue("@FechaFinValidez", fechaFinValidez);
                comando.Parameters.AddWithValue("@EsVencimiento", esVencimiento);
                comando.Parameters.AddWithValue("@Moneda", moneda);
                comando.Parameters.AddWithValue("@MontoLN", montoTotal);
                comando.Parameters.AddWithValue("@EsAfectoValidacion", esAfectoValidacion); 
                comando.Parameters.AddWithValue("@UsuarioActualiza", usuarioActualiza);
                comando.Parameters.AddWithValue("@Observacion", observacion);
                comando.Parameters.AddWithValue("@Categoria", categoria);
                comando.Parameters.AddWithValue("@DirectorioLocal", RutaLocal);
                comando.Parameters.AddWithValue("@DirectorioNube", RutaNube);
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getDocumento_RenovarDocumento(int idDocumento, string compania, string sucursal, string tipoRelacion, int idRelacion, int idTipoDocumento, string codigo, string centroCosto,
                                                         string situacionRenovacion, string fechaEmision, string fechaInicioValidez, string fechaFinValidez, int esVencimiento,
                                                         string moneda, decimal montoTotal, int esAfectoValidacion, string usuarioActualiza, string observacion, string categoria, string RutaLocal, string RutaNube)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_Actualizar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdDocumento", idDocumento);
                comando.Parameters.AddWithValue("@Compania", compania);
                comando.Parameters.AddWithValue("@Sucursal", sucursal);
                comando.Parameters.AddWithValue("@TipoRelacion", tipoRelacion);
                comando.Parameters.AddWithValue("@IdRelacion", idRelacion);
                comando.Parameters.AddWithValue("@IdTipoDocumento", idTipoDocumento);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@CentroCosto", centroCosto);
                comando.Parameters.AddWithValue("@SituacionRenovacion", situacionRenovacion);
                comando.Parameters.AddWithValue("@FechaEmision", fechaEmision);
                comando.Parameters.AddWithValue("@FechaInicioValidez", fechaInicioValidez);
                comando.Parameters.AddWithValue("@FechaFinValidez", fechaFinValidez);
                comando.Parameters.AddWithValue("@EsVencimiento", esVencimiento);
                comando.Parameters.AddWithValue("@Moneda", moneda);
                comando.Parameters.AddWithValue("@MontoLN", montoTotal);
                comando.Parameters.AddWithValue("@EsAfectoValidacion", esAfectoValidacion);
                comando.Parameters.AddWithValue("@UsuarioActualiza", usuarioActualiza);
                comando.Parameters.AddWithValue("@Observacion", observacion);
                comando.Parameters.AddWithValue("@Categoria", categoria);
                comando.Parameters.AddWithValue("@DirectorioLocal", RutaLocal);
                comando.Parameters.AddWithValue("@DirectorioNube", RutaNube);
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        //GERARDO - 01/09
        
        public DataTable getDocumento_AnularDocumento(int idDocumento, string usuarioAnula)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_Anular", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdDocumento", idDocumento);
                comando.Parameters.AddWithValue("@UsuarioAnula", usuarioAnula);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());


                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }

        }

        public DataTable getDocumento_VerDocumentosVencidos(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_VerDocumentosVencidos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Usuario", usuario);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }

        }


        public DataTable getDocumento_BuscarRelacion(int tipoRelacion, string filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_BuscarRelacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@TipoRelacion", tipoRelacion);
                comando.Parameters.AddWithValue("@FiltroRelacion", filtro);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }

        }


        public DataTable getDocumento_BuscarCentroCosto(string filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_BuscarCentroCosto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Filtro", filtro);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }

        }


        public DataTable getDocumento_BuscarTipoDocumento(string tipoRelacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_BuscarTipoDocumento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@TipoRelacion", tipoRelacion);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());


                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }

        }


        public DataTable getDocumentos_ListarTiposDocumentos(string Relacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TiposDocumentos_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Relacion", Relacion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable getDocumento_TipoDocumento_Registrar(string TipoRelacion, string Nemonico, string Descripcion, int dias, string usuarioCrea)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TiposDocumentos_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@TipoRelacion", TipoRelacion);
                comando.Parameters.AddWithValue("@Nemonico", Nemonico);
                comando.Parameters.AddWithValue("@Descripcion", Descripcion);
                comando.Parameters.AddWithValue("@Dias", dias);
                comando.Parameters.AddWithValue("@UsuarioCrea", usuarioCrea);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getDocumento_TipoDocumento_Actualizar(int idTipoDocumento, string TipoRelacion, string Nemonico, string Descripcion, int dias, string usuarioActualiza)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TiposDocumentos_Actualizar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdTipoDocumento", idTipoDocumento);
                comando.Parameters.AddWithValue("@TipoRelacion", TipoRelacion);
                comando.Parameters.AddWithValue("@Nemonico", Nemonico);
                comando.Parameters.AddWithValue("@Descripcion", Descripcion);
                comando.Parameters.AddWithValue("@Dias", dias);
                comando.Parameters.AddWithValue("@UsuarioActualiza", usuarioActualiza);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable getDocumentos_ReporteConductoresSinEMO(string compania, string filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_ReporteConductoresSinEMO", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Compania", compania));
                comando.Parameters.Add(new SqlParameter("@Filtro", filtro));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        // GERARDO - 19/09/23
        public DataTable ReportesApp_Operaciones_ControlDocumentos_ListarHistorial(int Opcion, string Nombre, string FInicio, string FFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_ListarHistorial", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@NombreConductor", Nombre));
                comando.Parameters.Add(new SqlParameter("@FECHINI", FInicio));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", FFin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        // GERARDO - 19/09/23
    }
}
