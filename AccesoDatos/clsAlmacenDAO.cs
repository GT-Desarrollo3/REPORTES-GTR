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

    public class clsAlmacenDAO
    {
        private readonly static clsAlmacenDAO instancia = new clsAlmacenDAO();
        string Msj;

        public static clsAlmacenDAO Instancia
        {
            get { return instancia; }
        }
        public string Mensaje()
        {
            return Msj;
        }
        public DataTable GetDataOrdenesRetiro(string fechaini, string fechafin, string cliente, string producto, string lote)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_OrdenRetiro", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@LOTE", lote));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataIngresos(string fechaini, string fechafin, string cliente, string producto, string lote)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Ingresos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@LOTE", lote));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataDespachos(string fechaini, string fechafin, string cliente, string producto, string lote, string modo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Despachos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@LOTE", lote));
                comando.Parameters.Add(new SqlParameter("@MODO", modo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataSaldos(string fechaini, string fechafin, string cliente, string producto, string lote, int grid)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Saldos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@LOTE", lote));
                comando.Parameters.Add(new SqlParameter("@GRID", grid));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetFacturacionAlmacen(string fini, string ffin, char tipofecha, char reporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Facturacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@TIPO_FECHA", tipofecha));
                comando.Parameters.Add(new SqlParameter("@REPORTE", reporte));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetData_Operaciones_Consultas(int TipoConsulta, int IDCliente, int IDContrato, int IDOT, string Dato)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Operaciones_Consulta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TipoConsulta", TipoConsulta));
                comando.Parameters.Add(new SqlParameter("@IDCliente", IDCliente));
                comando.Parameters.Add(new SqlParameter("@IDContrato", IDContrato));
                comando.Parameters.Add(new SqlParameter("@IDOT", IDOT));
                comando.Parameters.Add(new SqlParameter("@Dato", Dato));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetData_Operaciones_GrabarOperacion(int IDCliente, string Descripcion, int TipoOP, string Motonave, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Operaciones_CreaOperacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDCliente", IDCliente));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@IDTipoOP", TipoOP));
                comando.Parameters.Add(new SqlParameter("@Motonave", Motonave));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetData_Operaciones_GrabarEnlace_OP_OT(int AnioOP, int NroOP, int IDOT, int IDCliente, decimal TotalRetiroTN, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Operaciones_Enlazar_OP_OT", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@AnioOP", AnioOP));
                comando.Parameters.Add(new SqlParameter("@NroOP", NroOP));
                comando.Parameters.Add(new SqlParameter("@IDOT", IDOT));
                comando.Parameters.Add(new SqlParameter("@IDCliente", IDCliente));
                comando.Parameters.Add(new SqlParameter("@TotalRetiroTN", TotalRetiroTN));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", Usuario));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        //    public DataTable GetClientes()
        //    {
        //        try
        //        {
        //            DataTable dtTemp = new DataTable();
        //            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //            SqlCommand comando = new SqlCommand();

        //            comando.CommandText = "SELECT DISTINCT " +
        //            "CLIENTEFACTURACION.PERSONA, CLIENTEFACTURACION.NOMBRECOMPLETO " +
        //            "FROM OP_GE_OTDetalle " +
        //            "INNER JOIN OP_GE_Servicio ON OP_GE_Servicio.Codigo = OP_GE_OTDetalle.Servicio " +
        //            "INNER JOIN OP_AL_Producto ON OP_AL_Producto.Codigo = OP_GE_OTDetalle.Producto " +
        //            "INNER JOIN OP_GE_OTProducto ON OP_GE_OTDetalle.Producto = OP_GE_OTProducto.Producto AND OP_GE_OTDetalle.IdOT = OP_GE_OTProducto.IdOT " +
        //            "INNER JOIN OP_GE_OT ON OP_GE_OTDetalle.IdOT = OP_GE_OT.IDOT " +
        //            "INNER JOIN PERSONAMAST AS CLIENTEFACTURACION ON CLIENTEFACTURACION.PERSONA = OP_GE_OT.IDCLIENTEFACTURACION " +
        //            "WHERE OP_AL_Producto.Estado = 2 "+
        //            //AND CLIENTEFACTURACION.Busqueda LIKE '%" + nombre + "%' " +
        //            "AND OP_GE_OT.TIPOMOVIMIENTO IN ('ENT','SAL')";

        //            //    "SELECT "+
        //            //"PERSONAMAST.PERSONA, PERSONAMAST.NOMBRECOMPLETO "+
        //            //"FROM PERSONAMAST WITH(NOLOCK) WHERE PERSONAMAST.BUSQUEDA LIKE '%"+nombre+"%' "+
        //            //"AND PERSONAMAST.ESTADO = 'A' AND PERSONAMAST.ESCLIENTE = 'S' ";
        //            comando.CommandType = CommandType.Text;
        //            comando.Connection = conexion;
        //            conexion.Open();

        //            dtTemp.Load(comando.ExecuteReader());
        //            return dtTemp;
        //        }
        //        catch
        //        {
        //            return new DataTable();
        //        }
        //    }

        //    public DataTable GetProductosEnt(string idcliente)
        //    {
        //        try
        //        {
        //            DataTable dtTemp = new DataTable();
        //            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //            SqlCommand comando = new SqlCommand();

        //            comando.CommandText = "SELECT DISTINCT OP_AL_Producto.Codigo,OP_AL_Producto.Nombre " +
        //            "FROM OP_GE_OTDetalle " +
        //            "INNER JOIN OP_GE_Servicio ON OP_GE_Servicio.Codigo = OP_GE_OTDetalle.Servicio " +
        //            "INNER JOIN OP_AL_Producto ON OP_AL_Producto.Codigo = OP_GE_OTDetalle.Producto " +
        //            "INNER JOIN OP_GE_OTProducto ON OP_GE_OTDetalle.Producto = OP_GE_OTProducto.Producto AND OP_GE_OTDetalle.IdOT = OP_GE_OTProducto.IdOT " +
        //            "INNER JOIN OP_GE_OT ON OP_GE_OTDetalle.IdOT = OP_GE_OT.IDOT " +
        //            "INNER JOIN PERSONAMAST AS CLIENTEFACTURACION ON CLIENTEFACTURACION.PERSONA = OP_GE_OT.IDCLIENTEFACTURACION " +
        //            "WHERE OP_AL_Producto.Estado = 2 AND CLIENTEFACTURACION.PERSONA =" + idcliente +
        //            " AND OP_GE_OT.TIPOMOVIMIENTO = 'ENT' ORDER BY OP_AL_Producto.Nombre";
        //            comando.CommandType = CommandType.Text;
        //            comando.Connection = conexion;
        //            conexion.Open();

        //            dtTemp.Load(comando.ExecuteReader());
        //            return dtTemp;
        //        }
        //        catch
        //        {
        //            return new DataTable();
        //        }
        //    }

        //    public DataTable GetProductosSal(string idcliente)
        //    {
        //        try
        //        {
        //            DataTable dtTemp = new DataTable();
        //            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //            SqlCommand comando = new SqlCommand();

        //            comando.CommandText = "SELECT DISTINCT OP_AL_Producto.Codigo,OP_AL_Producto.Nombre " +
        //            "FROM OP_GE_OTDetalle " +
        //            "INNER JOIN OP_GE_Servicio ON OP_GE_Servicio.Codigo = OP_GE_OTDetalle.Servicio " +
        //            "INNER JOIN OP_AL_Producto ON OP_AL_Producto.Codigo = OP_GE_OTDetalle.Producto " +
        //            "INNER JOIN OP_GE_OTProducto ON OP_GE_OTDetalle.Producto = OP_GE_OTProducto.Producto AND OP_GE_OTDetalle.IdOT = OP_GE_OTProducto.IdOT " +
        //            "INNER JOIN OP_GE_OT ON OP_GE_OTDetalle.IdOT = OP_GE_OT.IDOT " +
        //            "INNER JOIN PERSONAMAST AS CLIENTEFACTURACION ON CLIENTEFACTURACION.PERSONA = OP_GE_OT.IDCLIENTEFACTURACION " +
        //            "WHERE OP_AL_Producto.Estado = 2 AND CLIENTEFACTURACION.PERSONA =" + idcliente +
        //            " AND OP_GE_OT.TIPOMOVIMIENTO = 'SAL' ORDER BY OP_AL_Producto.Nombre";
        //            comando.CommandType = CommandType.Text;
        //            comando.Connection = conexion;
        //            conexion.Open();

        //            dtTemp.Load(comando.ExecuteReader());
        //            return dtTemp;
        //        }
        //        catch
        //        {
        //            return new DataTable();
        //        }
        //    }

        //    public DataTable GetLotesEnt(string producto,string idcliente)
        //    {
        //        try
        //        {
        //            DataTable dtTemp = new DataTable();
        //            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //            SqlCommand comando = new SqlCommand();

        //            comando.CommandText = "SELECT DISTINCT OP_GE_OTDetalle.Lote FROM OP_GE_OT WITH (NOLOCK) " +
        //            "INNER JOIN OP_GE_OTProducto WITH (NOLOCK) " +
        //            "ON OP_GE_OTProducto.IdOT = OP_GE_OT.IdOT " +
        //            "INNER JOIN OP_GE_OTDetalle WITH (NOLOCK) " +
        //            "ON OP_GE_OTDetalle.IdOT = OP_GE_OTProducto.IdOT " +
        //            "AND OP_GE_OTDetalle.LineaProducto = OP_GE_OTProducto.Linea " +
        //            "AND OP_GE_OTDetalle.GrupoServicioOP = 'A' " +
        //            "AND OP_GE_OTDetalle.Estado = 2 " +
        //            "WHERE " +
        //            "OP_GE_OTProducto.Producto LIKE '"+producto+"' " +
        //            "AND OP_GE_OT.IdClienteFacturacion LIKE '" + idcliente + "' AND OP_GE_OT.TIPOMOVIMIENTO = 'ENT' " +
        //            "GROUP BY OP_GE_OTProducto.IdContrato, OP_GE_OTProducto.LineaContrato, " +
        //            "OP_GE_OTProducto.Producto, OP_GE_OTDetalle.Lote ";
        //            comando.CommandType = CommandType.Text;
        //            comando.Connection = conexion;
        //            conexion.Open();

        //            dtTemp.Load(comando.ExecuteReader());
        //            return dtTemp;
        //        }
        //        catch
        //        {
        //            return new DataTable();
        //        }
        //    }

        //    public DataTable GetLotesSal(string producto, string idcliente)
        //    {
        //        try
        //        {
        //            DataTable dtTemp = new DataTable();
        //            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //            SqlCommand comando = new SqlCommand();

        //            comando.CommandText = "SELECT DISTINCT OP_GE_OTDetalle.Lote FROM OP_GE_OT WITH (NOLOCK) " +
        //            "INNER JOIN OP_GE_OTProducto WITH (NOLOCK) " +
        //            "ON OP_GE_OTProducto.IdOT = OP_GE_OT.IdOT " +
        //            "INNER JOIN OP_GE_OTDetalle WITH (NOLOCK) " +
        //            "ON OP_GE_OTDetalle.IdOT = OP_GE_OTProducto.IdOT " +
        //            "AND OP_GE_OTDetalle.LineaProducto = OP_GE_OTProducto.Linea " +
        //            "AND OP_GE_OTDetalle.GrupoServicioOP = 'A' " +
        //            "AND OP_GE_OTDetalle.Estado = 2 " +
        //            "WHERE " +
        //            "OP_GE_OTProducto.Producto LIKE '" + producto + "' " +
        //            "AND OP_GE_OT.IdClienteFacturacion LIKE '" + idcliente + "' AND OP_GE_OT.TIPOMOVIMIENTO = 'SAL' " +
        //            "GROUP BY OP_GE_OTProducto.IdContrato, OP_GE_OTProducto.LineaContrato, " +
        //            "OP_GE_OTProducto.Producto, OP_GE_OTDetalle.Lote ";
        //            comando.CommandType = CommandType.Text;
        //            comando.Connection = conexion;
        //            conexion.Open();

        //            dtTemp.Load(comando.ExecuteReader());
        //            return dtTemp;
        //        }
        //        catch
        //        {
        //            return new DataTable();
        //        }
        //    }
        //}


        public DataTable GetListarTicket(string fechinicio, string fechfin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_SistemaAntiguo_ReporteTickets", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAINI", fechinicio));
                comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechfin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarTicketTop30()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_SistemaAntiguo_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
            

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetModificarFecha(string Fechainicio, string FechaFin, string NroTicket, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_SistemaAntiguo_ModFecha", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Fechainicio", Fechainicio));
                comando.Parameters.Add(new SqlParameter("@Fechasalida", FechaFin));
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetModificarTicket(int ot, string NumeroPesaje,  decimal PesoInicial, decimal PesoFinal, decimal CantidadBase, decimal CantidadUso, string GuiaC, string GuiaT, string Usuario, string fechaInicio, string fechaFin,string Lote, string Ubicacion)
        {
            try
            {
                    
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_ModificarDatosTicket", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@id", ot));
                comando.Parameters.Add(new SqlParameter("@NumeroPesaje", NumeroPesaje));
                comando.Parameters.Add(new SqlParameter("@PesoIncial", PesoInicial));
                comando.Parameters.Add(new SqlParameter("@PesoFinal", PesoFinal));
                comando.Parameters.Add(new SqlParameter("@CantBase", CantidadBase));
                comando.Parameters.Add(new SqlParameter("@CantUso", CantidadUso));
                comando.Parameters.Add(new SqlParameter("@Guia2", GuiaC));//Cliente
                comando.Parameters.Add(new SqlParameter("@Guia1", GuiaT));//Transportista
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
                comando.Parameters.Add(new SqlParameter("@Lote", Lote));
                comando.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarTickets(int ot, string NumeroPeaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_ListarTicket", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@id", ot));
                comando.Parameters.Add(new SqlParameter("@NumeroPesaje", NumeroPeaje));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarClientes(int codigo, string RazonSocial,string DocumentoFiscal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_ListarCliente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Codigo", codigo));
                comando.Parameters.Add(new SqlParameter("@RazonSocial", RazonSocial));
                comando.Parameters.Add(new SqlParameter("@DocumentoFiscal", DocumentoFiscal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetTransferirCliente(int Persona, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_TransferirCliente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarUltimoCliente()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_ListarUltimoCliente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarClientesSalaverry(int codigo, string RazonSocial, string DocumentoFiscal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_ListarCliente_BDSalaverry", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Codigo", codigo));
                comando.Parameters.Add(new SqlParameter("@RazonSocial", RazonSocial));
                comando.Parameters.Add(new SqlParameter("@DocumentoFiscal", DocumentoFiscal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetRegistrarConductor(string Documento, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_RegistrarConductor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Documento", Documento));
                comando.Parameters.Add(new SqlParameter("@User", usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable    GetListarUltimoConductor()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_Listar_UltimoConductor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListarConductoressSalaverry(string Documento)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Altra_ListarConductores_BDSalaverry", conexion);
                comando.Parameters.Add(new SqlParameter("@Documento", Documento));
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Almacen_ListarConductoresAlmacen(string txtconductor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_ListarConductoresAlmacen", conexion);
                comando.Parameters.Add(new SqlParameter("@Conductor", txtconductor));
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Almacen_ListarClientesAlmacen(string cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_ListarClientesAlmacen", conexion);
                comando.Parameters.Add(new SqlParameter("@Cliente", cliente));
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Almacen_ListarVehiculoAlmacen(string vehiculo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_ListarVehiculoAlmacen", conexion);
                comando.Parameters.Add(new SqlParameter("@vehiculo", vehiculo));
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public bool ReportesApp_Almacen_Registrar_FechaIngreso(string OrdenCliente, int idCliente, string Cliente, int idConductor, string Conductor, int idVehiculo, string vehiculo, string FechaIngreso, string peso,string grupo,ref string nroOrden,ref string codigo,ref string transportista)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Almacen_RegistrarIngreso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@OrdenCliente", OrdenCliente));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                comando.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                comando.Parameters.Add(new SqlParameter("@idConductor",  idConductor));
                comando.Parameters.Add(new SqlParameter("@Vehiculo", vehiculo));
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                comando.Parameters.Add(new SqlParameter("@PesoGuia", peso ));
                comando.Parameters.Add(new SqlParameter("@FechaIngreso", FechaIngreso));
                comando.Parameters.Add(new SqlParameter("@Grupo", grupo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);
                    codigo = Convert.ToString(dr["Codigo"]);
                    transportista = Convert.ToString(dr["Transportista"]);
                    nroOrden = Convert.ToString(dr["NroOrden"]);
                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Almacen_ListarIngresoSalida(string fechaInicio, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_ListarIngresoSalida", conexion);
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", fechafin));
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;

            }
            catch
            {
                return new DataTable();
            }
        }

        public bool ReportesApp_Almacen_EnPesajeIngresoSalida(string codigo, string tipo)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Almacen_EnPesajeIngresoSalida", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Codigo", codigo));
                comando.Parameters.Add(new SqlParameter("@Tipo", tipo));
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

        public bool ReportesApp_Almacen_Registrar_Correos(string cliente, int idCliente, string correo1, string correo2)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Almacen_Registrar_Correos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idCliente", idCliente));
                comando.Parameters.Add(new SqlParameter("@Cliente", idCliente));
                comando.Parameters.Add(new SqlParameter("@Correo1", correo1));
                comando.Parameters.Add(new SqlParameter("@Correo2", correo2));
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

        public DataTable ReportesApp_Almacen_Registrar_ListarCorreos()
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            conexion.Open();
            SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Registrar_ListarCorreos", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            dtTemp.Load(comando.ExecuteReader());
            return dtTemp;

        }

        public bool ReportesApp_Almacen_Eliminar_Correos(int idCorreo)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Almacen_Eliminar_Correos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idCorreo", idCorreo));
 

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

        public DataTable ReportesApp_Almacen_Salaverry_ListarTransformaciones(string FechaInicio, string FechaFin, string Cliente, string Producto, string Lote)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Salaverry_ListarTransformaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                comando.Parameters.Add(new SqlParameter("@Producto", Producto));
                comando.Parameters.Add(new SqlParameter("@Lote", Lote));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Almacen_ListarAlmacenes()
        {

            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_ListarAlmacenes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Almacen_Salaverry_ListarKardex(string FechaInicio, string FechaFin, string Cliente, string Producto, string Lote)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_Salaverry_ListarKardex", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                comando.Parameters.Add(new SqlParameter("@Producto", Producto));
                comando.Parameters.Add(new SqlParameter("@Lote", Lote));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Almacen_Movimientos_Almacen(string fechaInI, string fechaFin, string Cliente, bool Anulado, int bd)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Almacen_MovimientosAlmacen", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaIni", fechaInI));
                comando.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                if (Anulado == false){
                    comando.Parameters.Add(new SqlParameter("@Anulado", 8));
                }else{
                     comando.Parameters.Add(new SqlParameter("@Anulado", 9));
                }
                comando.Parameters.Add(new SqlParameter("@bd", bd));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }
    }
}
