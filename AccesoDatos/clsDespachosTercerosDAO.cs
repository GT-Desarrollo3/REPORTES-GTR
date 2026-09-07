using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;



namespace AccesoDatos
{
    public class clsDespachosTercerosDAO
    {
        private readonly static clsDespachosTercerosDAO instancia = new clsDespachosTercerosDAO();

        public static clsDespachosTercerosDAO Instancia
        {
            get { return instancia; }
        }

        public DataTable getDespachosTerceros_LlenarControlesDespachoTerceros(string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_LlenadoControlesFrm", conexion);
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


        public DataTable getDespachosTerceros_Listar(string periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", periodo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getDespachosTerceros_Registrar(string codigo, string placa, string fechaDespacho, string hora, decimal cantidad, decimal precio, string producto, 
                                                         string lugar,int IdEmpresa, string empresa, string dni, string chofer, decimal kilometraje,int CodigoPreviaje,
                                                        string usuarioCrea, decimal Totalizador)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Placa", placa);
                comando.Parameters.AddWithValue("@FechaDespacho", fechaDespacho);
                comando.Parameters.AddWithValue("@Hora", hora);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@Precio", precio);
                comando.Parameters.AddWithValue("@Producto", producto);
                comando.Parameters.AddWithValue("@lugar", lugar);
                comando.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                comando.Parameters.AddWithValue("@Empresa", empresa);
                comando.Parameters.AddWithValue("@Dni", dni);
                comando.Parameters.AddWithValue("@Chofer", chofer);
                comando.Parameters.AddWithValue("@Kilometraje", kilometraje);
                comando.Parameters.AddWithValue("@NroTicketPreviaje", CodigoPreviaje);
                comando.Parameters.AddWithValue("@UsuarioCrea", usuarioCrea);
                comando.Parameters.AddWithValue("@Totalizador", Totalizador);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getDespachosTerceros_Actualizar(int ticket, string codigo, string placa, string fechaDespacho, string hora, decimal cantidad, decimal precio, string producto,
                                                        string lugar, string empresa, string dni, string chofer, decimal kilometraje, int CodigoPreviaje, string usuarioActualiza,
                                                        decimal Totalizador)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_Actualizar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Ticket", codigo);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Placa", placa);
                comando.Parameters.AddWithValue("@FechaDespacho", fechaDespacho);
                comando.Parameters.AddWithValue("@Hora", hora);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@Precio", precio);
                comando.Parameters.AddWithValue("@Producto", producto);
                comando.Parameters.AddWithValue("@lugar", lugar);
                comando.Parameters.AddWithValue("@Empresa", empresa);
                comando.Parameters.AddWithValue("@Dni", dni);
                comando.Parameters.AddWithValue("@Chofer", chofer);
                comando.Parameters.AddWithValue("@Kilometraje", kilometraje);
                comando.Parameters.AddWithValue("@NroTicketPreviaje", CodigoPreviaje);
                comando.Parameters.AddWithValue("@UsuarioActualiza", usuarioActualiza);
                comando.Parameters.AddWithValue("@Totalizador", Totalizador);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());


                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable getDespachosTerceros_Anular(int ticket, int codigo, string placa, string fechaDespacho, string hora, decimal cantidad, decimal precio, string chofer,
                                                     int notaSalida, string Producto, string Proveedor, string Lugar, string usuarioAnula)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_Anular", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Ticket", ticket);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Placa", placa);
                comando.Parameters.AddWithValue("@FechaDespacho", fechaDespacho);
                comando.Parameters.AddWithValue("@Hora", hora);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@Precio", precio);
                comando.Parameters.AddWithValue("@Chofer", chofer);
                comando.Parameters.AddWithValue("@NotaSalida", notaSalida);
                comando.Parameters.AddWithValue("@Producto", Producto);
                comando.Parameters.AddWithValue("@Proveedor", Proveedor);
                comando.Parameters.AddWithValue("@Lugar", Lugar);
                comando.Parameters.AddWithValue("@UsuarioAnula", usuarioAnula);

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());

                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable getDocumento_BuscarRelacion(int tipoRelacion, string filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_BuscarRelacion", conexion);
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

        public DataTable getDespachosTerceros_ObtenerOdometroPlaca(string placa, string fecha, string hora)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_CargarOdometroUnidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Placa", placa);
                comando.Parameters.AddWithValue("@Fecha", fecha);
                comando.Parameters.AddWithValue("@Hora", hora);

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

        public DataTable getDocumento_ListarLugarXproveedor(int preoveedor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachoTercerosListarLugarxProveedor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idproveedor", preoveedor);

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

        public DataTable ReportesApp_Operaciones_DespachosTerceros_ListarOdometro(string placa, string fecha, string hora)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_DespachosTerceros_ListarOdometro", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Placa", placa);
                comando.Parameters.AddWithValue("@Fecha", fecha);
                comando.Parameters.AddWithValue("@Hora", hora);
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }
    }
}
