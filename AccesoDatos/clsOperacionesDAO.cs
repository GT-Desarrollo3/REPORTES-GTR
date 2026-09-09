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
    public class clsOperacionesDAO
    {
        private readonly static clsOperacionesDAO instancia = new clsOperacionesDAO();

        public static clsOperacionesDAO Instancia
        {
            get { return instancia; }
        }
        
        public DataTable GetDataConsolidado(string fini, string ffin,Int32 det,Int32 todos,Int32 xfact,bool fechaprog) 
        {
            try
            {
                DataTable dtTemp = new DataTable();                
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                if (fechaprog == true)
                {
                    comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Consolidado_FechasProg2", conexion);
                }
                else
                {
                    comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Consolidado_FechasCreac2", conexion);
                }
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@DETALLADO", det));
                comando.Parameters.Add(new SqlParameter("@TODOS", todos));
                comando.Parameters.Add(new SqlParameter("@POR_FACTURAR", xfact));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetImpresion_Desembarque_SerieGuias(int Opcion, string Serie)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Impresion_Desembarque_Series_Datos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Serie", Serie));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetOperaciones_GuiasSalaverry_Importar(int Opcion, string FInicio, string FFin,int VerTerceros)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_AltraSLV_Importar", conexion);
                comando.CommandTimeout = 180;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FFin));               
                comando.Parameters.Add(new SqlParameter("@VerTerceros", VerTerceros));  
                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        
        public DataTable GetOperaciones_GuiasSalaverry_CreaViaje(string ID, string OT,string partida,string llegada, string USUARIO)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_CrearViajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idticket", ID));
                comando.Parameters.Add(new SqlParameter("@IDOts", OT));
                comando.Parameters.Add(new SqlParameter("@DirPartida", partida));
                comando.Parameters.Add(new SqlParameter("@DirLlegada", llegada));
                comando.Parameters.Add(new SqlParameter("@UserCrea", USUARIO));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetOperaciones_Programaciones_CreaViaje(int ID, string OT, string partida, string llegada, string USUARIO, decimal montoOst, string anio, int TipoProgramacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_GenerarViajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idticket", ID));
                comando.Parameters.Add(new SqlParameter("@IDOts", OT));
                comando.Parameters.Add(new SqlParameter("@DirPartida", partida));
                comando.Parameters.Add(new SqlParameter("@DirLlegada", llegada));
                comando.Parameters.Add(new SqlParameter("@UserCrea", USUARIO));
                comando.Parameters.Add(new SqlParameter("@montoOst",montoOst));
                comando.Parameters.Add(new SqlParameter("@anio", anio));
                comando.Parameters.Add(new SqlParameter("@TipoProgramacion", TipoProgramacion));
          
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetOperaciones_Programaciones_GuardaViajeEstado(int ID, int estado, string codigoviaje, string Comentario, string USUARIO, string anio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_GuardarViajeEstado", conexion);
                comando.CommandTimeout = 10;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idticket", ID));
                comando.Parameters.Add(new SqlParameter("@Estado", estado));
                comando.Parameters.Add(new SqlParameter("@Codviaje", codigoviaje));
                comando.Parameters.Add(new SqlParameter("@Comentario", Comentario));
                comando.Parameters.Add(new SqlParameter("@UserCrea", USUARIO));
                comando.Parameters.Add(new SqlParameter("@anio", anio));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Programaciones_GenerarConsolidados(int codviaje, int idpro, int idot, int dirpartida, int dirllegada, string user,
                                                                            decimal cantidad, string serie, string numero, string guiaremitente ,int idproducto, int idcliente, string medida, int anio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Programacion_GenerarConsolidados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CODVIAJE",Convert.ToString(codviaje)));
                comando.Parameters.Add(new SqlParameter("@idticket", idpro));
                comando.Parameters.Add(new SqlParameter("@IDOts", idot));
                comando.Parameters.Add(new SqlParameter("@DirPartida", dirpartida));
                comando.Parameters.Add(new SqlParameter("@DirLlegada", dirllegada));
                comando.Parameters.Add(new SqlParameter("@UserCrea", user));
                comando.Parameters.Add(new SqlParameter("@Cantidad", cantidad));
                comando.Parameters.Add(new SqlParameter("@Serie", serie));
                comando.Parameters.Add(new SqlParameter("@Numero", numero));
                comando.Parameters.Add(new SqlParameter("@Guiaremite", guiaremitente));
                comando.Parameters.Add(new SqlParameter("@idproducto", idproducto));
                comando.Parameters.Add(new SqlParameter("@idcliente", idcliente));
                comando.Parameters.Add(new SqlParameter("@UnidadMedida", medida));
                comando.Parameters.Add(new SqlParameter("@anio", anio));
          
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Programaciones_GuardarConsolidados(int codviaje, int idpro, int idot, int idruta, int idproducto, int idcliente, string unidadMedida, string user,
                                                                         decimal cantidad,string anio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Programacion_RegistrarConsolidado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CODVIAJE", Convert.ToString(codviaje)));
                comando.Parameters.Add(new SqlParameter("@idprog", idpro));
                comando.Parameters.Add(new SqlParameter("@IDOts", idot));
                comando.Parameters.Add(new SqlParameter("@idruta", idruta));
                comando.Parameters.Add(new SqlParameter("@idproducto", idproducto));
                comando.Parameters.Add(new SqlParameter("@idcliente", idcliente));
                comando.Parameters.Add(new SqlParameter("@UnidadMedida", unidadMedida));
                comando.Parameters.Add(new SqlParameter("@user", user));
                comando.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                comando.Parameters.Add(new SqlParameter("@anio", anio));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetImpresion_Desembarque_GuardaGuiaMasiva(string Serie, string remite_razon, string remite_ruc, string remite_direccion, string remite_distrito, string remite_provincia,
                                                                    string remite_departamento, string Destino_razon, string Destino_ruc, string Destino_direccion, string Destino_distrito
                                                                    , string Destino_provincia, string Destino_departamento, string confvehiculo, string producto, string nave)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_ImpresionGuiaDesembarque_Update", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@serie", Serie));
                comando.Parameters.Add(new SqlParameter("@remite_razon", remite_razon));
                comando.Parameters.Add(new SqlParameter("@remite_ruc", remite_ruc));
                comando.Parameters.Add(new SqlParameter("@remite_direccion", remite_direccion));
                comando.Parameters.Add(new SqlParameter("@remite_distrito", remite_distrito));
                comando.Parameters.Add(new SqlParameter("@remite_provincia", remite_provincia));
                comando.Parameters.Add(new SqlParameter("@remite_departamento", remite_departamento));
                comando.Parameters.Add(new SqlParameter("@Destino_razon", Destino_razon));
                comando.Parameters.Add(new SqlParameter("@Destino_ruc", Destino_ruc));
                comando.Parameters.Add(new SqlParameter("@Destino_direccion", Destino_direccion));
                comando.Parameters.Add(new SqlParameter("@Destino_distrito", Destino_distrito));
                comando.Parameters.Add(new SqlParameter("@Destino_provincia", Destino_provincia));
                comando.Parameters.Add(new SqlParameter("@Destino_departamento", Destino_departamento));
                comando.Parameters.Add(new SqlParameter("@confvehiculo", confvehiculo));
                comando.Parameters.Add(new SqlParameter("@producto", producto));
                comando.Parameters.Add(new SqlParameter("@nave", nave));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetImpresion_Desembarque_ConsultaRUC(string RUC)
        {

            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_ImpresionGuiaDesembarque_ConsultaRUC", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@RUC", RUC));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataBonos(int idperiodo,int tipo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                if (tipo == 1)
                {
                    //1 detallado
                    comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Bonos2", conexion);
                }
                else //2 resumido
                {
                    comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Bonos", conexion);
                }
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO_ID", idperiodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataProduccDiaria(int idperiodo,string sucursal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Produccion_Diaria", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO_ID", idperiodo));
                comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetAllDataProduccDiaria(int idperiodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Produccion_Diaria_Todo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO_ID", idperiodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPeriodoBonos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());               
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT Id,Descripcion " +
                "FROM ReportesApp_Periodos WHERE Reporte_Formulario " + 
                "LIKE 'Operaciones_Reporte_Bonificacion'";
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

        public DataTable GetPeriodoGuiasxEstado()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT Id,Descripcion " +
                "FROM ReportesApp_Periodos WHERE Reporte_Formulario " +
                "LIKE 'Operaciones_Reporte_GuiasxEstado'";
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

        public DataTable GetPeriodoProduccDiaria()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT Id,Descripcion " +
                "FROM ReportesApp_Periodos WHERE Reporte_Formulario " +
                "LIKE 'Operaciones_Reporte_Produccion_Diaria'";
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


        public DataTable GetLista_Consulta_Tracto_Mantenimiento(string fecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Consultar_Tractos_Mantenimiento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                //comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetLista_Consultar_Tractos_No_Programados()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Consultar_Tractos_No_Programados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                //comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
            
       //public DataTable GetDataSeguimientoGuias(string fini, string ffin, string serie, string numero, string estado, string parametro)
       /* public DataTable GetDataSeguimientoGuias()
          {
            try
            {
                DataTable dtTemp = new DataTable();
               SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
               comando.CommandText = "SELECT 	OP_TR_GUIA.SERIE AS 'SERIE',OP_TR_GUIA.NUMERO AS 'NUMERO', OP_TR_GUIA.FECHAEMISION AS 'FECHA DE EMISION' " +
        "CASE WHEN OP_TR_GUIA.Estado = 1 THEN 'PREPARADO'"+
        	"WHEN OP_TR_GUIA.Estado = 3 THEN 'ASIGNADO'"+
		"WHEN OP_TR_GUIA.Estado = 9 THEN 'ANULADO'"+
		"WHEN OP_TR_GUIA.Estado = 2 THEN"   +
        "CASE	WHEN OP_TR_GUIA.Situacion = 4 THEN 'COMPLETADO/ENTREGADO'"+
				"WHEN OP_TR_GUIA.Situacion = 5 THEN 'COMPLETADO/RECEPCIONADO'"+
				"ELSE 'COMPLETADO' END END AS 'ESTADO',"+
        "ISNULL((SELECT R.Descripcion FROM OP_TR_RUTA R INNER JOIN OP_TR_VIAJE V "+
       "ON R.IdRuta = V.IdRuta WHERE V.IdViaje = OP_TR_GUIA.IdViaje),'') AS 'RUTA',"+
		"ISNULL((SELECT BUSQUEDA FROM PersonaMast WHERE Persona = (SELECT IdClienteFacturacion FROM OP_GE_OT WHERE IdOT=OP_TR_GUIA.IdOT)),'') AS 'CLIENTE',"+
		"ISNULL(OP_TR_CONDUCTOR.NOMBRE,'') AS 'CONDUCTOR',"+
        "OP_TR_Guia.UsuarioModificacion AS 'USUARIO'"+
        "FROM 	OP_TR_GUIA WITH(NOLOCK) LEFT JOIN PERSONAMAST AS CLIENTEPARTIDA WITH(NOLOCK) ON CLIENTEPARTIDA.PERSONA = OP_TR_GUIA.IDCLIENTEPARTIDA"+
		"LEFT JOIN PERSONAMAST AS CLIENTELLEGADA WITH(NOLOCK) ON CLIENTELLEGADA.PERSONA = OP_TR_GUIA.IDCLIENTELLEGADA"+
		"LEFT JOIN OP_TR_VIAJE WITH(NOLOCK) ON OP_TR_VIAJE.IDVIAJE = OP_TR_GUIA.IDVIAJE"+
		"LEFT JOIN OP_TR_VEHICULO WITH(NOLOCK) ON OP_TR_VEHICULO.IDVEHICULO = OP_TR_VIAJE.IDVEHICULO"+
		"LEFT JOIN OP_TR_CONDUCTOR WITH(NOLOCK) ON OP_TR_CONDUCTOR.IDCONDUCTOR = OP_TR_GUIA.IDCONDUCTOR"+
	"LEFT JOIN OP_GE_OT WITH(NOLOCK) ON OP_GE_OT.IDOT = OP_TR_GUIA.IDOT";
                comando.Connection = conexion;
                conexion.Open();
               // SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Seguimiento_Guias", conexion);
               // comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
               // comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
               // comando.Parameters.Add(new SqlParameter("@SERIE", serie));
               // comando.Parameters.Add(new SqlParameter("@NUMERO", numero));
                //comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                //comando.Parameters.Add(new SqlParameter("@TIPOPARAM", tipoparam));
               // comando.Parameters.Add(new SqlParameter("@PARAMETRO", parametro));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        */
        public DataTable GetDataSeguimientoGuias(string fini, string ffin, string serie, string numero, string estado, int tipoparam, string parametro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Seguimiento_Guias", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@SERIE", serie));
                comando.Parameters.Add(new SqlParameter("@NUMERO", numero));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                comando.Parameters.Add(new SqlParameter("@TIPOPARAM", tipoparam));
                comando.Parameters.Add(new SqlParameter("@PARAMETRO", parametro));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataGuiasxEstadoResumido(int idperiodo, string estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_GuiasxEstado_Resumen", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO", idperiodo));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataGuiasxEstadoDetalle(int idperiodo, string estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_GuiasxEstado_Detalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO", idperiodo));
                comando.Parameters.Add(new SqlParameter("@ESTADO", estado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataAdelantosPlanillas()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Adelantos_Planillas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetGuiasxEntregar()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_GuiasxEntregar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //comando.CommandText = "select V.Codigo AS 'VIAJE',R.Descripcion as 'RUTA', " +
                //"V.FechaProgramada as 'FECHA PROGRAMADA', " +
                //"C.Nombre AS 'CONDUCTOR', " +
                //"CASE WHEN C.IndTercero = 'P' THEN 'PROPIO' " +
                //     "WHEN C.IndTercero = 'T' THEN 'TERCERO' " +
                //     "END AS 'TIPO', " +
                //"P.Busqueda AS 'CLIENTE', " +
                //"T.NumeroPlaca AS 'TRACTO', " + 
                // "(SELECT SUB.GT [text()] FROM " +
                // "(SELECT Serie + '-' + Numero + '/ ' AS GT " +
                // "FROM OP_TR_Guia " +
                // "WHERE IdConductor = C.IDCONDUCTOR "+
                // "AND Estado NOT IN (2,9))SUB FOR XML PATH('')) AS 'PENDIENTES' "+
                //"from OP_TR_Viaje V INNER JOIN " +
                //"OP_TR_Ruta R ON V.IdRuta = R.IdRuta INNER JOIN " +
                //"OP_TR_Conductor C ON V.IdConductor = C.IdConductor INNER JOIN " +
                //"OP_TR_Vehiculo T ON V.IdVehiculo = T.IdVehiculo INNER JOIN " +
                //"OP_GE_OTDetalle D ON V.IdViaje = D.IdViaje INNER JOIN " +
                //"OP_GE_OT OT ON D.IdOT = OT.IdOT INNER JOIN " +
                //"PersonaMast P ON OT.IdClienteFacturacion = P.Persona " +
                //"where V.Estado = 2 and V.Codigo not like 'C%' " +
                //"AND DATEADD(HOUR,R.LeadTime,V.FechaProgramada) < GETDATE() " +
                //"order by FechaProgramada desc";
                //comando.CommandType = CommandType.Text;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetTiemposLindley()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TiemposLindley", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetListaConductores(char activos)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_FichaConductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@ACTIVO", activos));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataFacturas(string fechaini, string fechafin, string serie,string tipofecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Facturas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@SERIE", serie));
                comando.Parameters.Add(new SqlParameter("@TIPO_FECHA", tipofecha));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public bool UpdateFactura(string nrofactura, int finanzas,int opfinan, int contabilidad, int opconta) 
        {
            try
            {

                bool resultado;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                string query = "";
                query = "UPDATE CO_Documento SET vistoFinanzas=" + finanzas + ",vistoOPFinanzas=" + opfinan +
                        ",vistoContabilidad=" + contabilidad + ",vistoOPContabilidad=" + opconta +
                        " WHERE NumeroDocumento LIKE '%" + nrofactura + "%'";
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

        public DataTable GetFacturasLindley(string fechaini, string fechafin, char tipofecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_FacturasLindley", conexion);
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

        public DataTable GetViajesTerceros(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ViajesTerceros", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ActualizarFechaOts(int accion,string ot, string fechaini, string fechafin, string FechanAnterior)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ModificarFechaOTS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Accion", accion));
                comando.Parameters.Add(new SqlParameter("@OT", ot));
                comando.Parameters.Add(new SqlParameter("@FECHA", fechaini));
                comando.Parameters.Add(new SqlParameter("@USUARIO", fechafin));
                comando.Parameters.Add(new SqlParameter("@FECHAANTERIOR", FechanAnterior));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        

        public DataTable GetUbicacionConductoresTrujillo(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Ubicacion_de_Conductores_Trujillo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetUbicacionConductoresFueraTrujillo(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Ubicacion_de_Conductores_Fuera_de_Trujillo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetUbicacionConductoresNoViajaron(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Reporte_Ubicacion_de_Conductores_No_Viajaron", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        //Consulta de Tractos Libres sin mantenimiento 
        public DataTable GetLista_Tractos(string fecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Consultar_Tractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                //comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        //Consulta de conductores y tractos con fecha programada de viaje 
        
        public DataTable GetLista_Consulta_Tracto_Conductores(string fecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Consultar_Tractos_Conductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                //comando.Parameters.Add(new SqlParameter("@FECHAFIN", fechafin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        //UNIDADES EN TRANSITO 
        public DataTable GetUnidades_Transito(string fechaini, string fechafin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Consultar_Unidades_Transito", conexion);
                comando.CommandType = CommandType.StoredProcedure;
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


        //CONSULTAR LOS TRACTOS CON SU TIEMPO DE VIAJE 

        public DataTable GetLista_Consultar_Tractos_Tiempo(string fecha)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Consultar_Tractos_Tiempo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA", fecha));
                //comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetLista_Viajes(string periodo, string cliente, string ruta, char reporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Cantidad_Viajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@RUTA", ruta));
                comando.Parameters.Add(new SqlParameter("@REPORTE", reporte));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetLista_Viajes_Detalle(string fini, string ffin, string cliente, string ruta, string producto, string unidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Cantidad_Viajes_Detalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@RUTA", ruta));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@UNIDAD", unidad));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetLista_Viajes_Lindley(string fini, string ffin, string cliente, string ruta, string producto, string unidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Cantidad_Viajes_Lindley", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@RUTA", ruta));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@UNIDAD", unidad));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetLista_Operacion_Lindley(string fini, string ffin, string cliente, string ruta, string producto, string unidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Lista_Viajes_Lindley", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                //comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                //comando.Parameters.Add(new SqlParameter("@RUTA", ruta));
                //comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                //comando.Parameters.Add(new SqlParameter("@UNIDAD", unidad));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetLista_Viajes_Por_Periodo(string periodo, string cliente, string ruta, char reporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Lista_Viajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@PERIODO", periodo));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@RUTA", ruta));
                comando.Parameters.Add(new SqlParameter("@REPORTE", reporte));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetLista_ListarPeriodosOperaciones()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ConsultarPeriodosOperacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetViajes_Detallado(string fini, string ffin, string transporte,string cliente, string ruta, string producto, string unidad, int estado_viaje,int TipoTransporte)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Lista_Viajes_Detallado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                //comando.Parameters.Add(new SqlParameter("@TRANSPORTE", transporte));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                comando.Parameters.Add(new SqlParameter("@RUTA", ruta));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@UNIDAD", unidad));
                comando.Parameters.Add(new SqlParameter("@ESTADO_VIAJE", estado_viaje));
                comando.Parameters.Add(new SqlParameter("@TIPO_TRANSPORTE", TipoTransporte));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetViajes_Detallado_Tipo_Transporte(string fini, string ffin, string transporte, string cliente, string ruta, string producto, string unidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Lista_Viajes_Detallado_Tipo_Transporte", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("FECHA_INI", fini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", ffin));
                comando.Parameters.Add(new SqlParameter("@TRANSPORTE", transporte));
                //comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                //comando.Parameters.Add(new SqlParameter("@RUTA", ruta));
                //comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                //comando.Parameters.Add(new SqlParameter("@UNIDAD", unidad));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetCantidad_Viajes(string fechaini, string fechafin, string cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Cantidad_Viajes_Clientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
                comando.Parameters.Add(new SqlParameter("@CLIENTE", cliente));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }



        }
        public DataTable GetViajes_Conductor_BloqueaDesbloquea(int Opcion, int IdBloqueo, int IDPersona, string Motivo, string Usuario, int IdMotivo, int IdDesbloqueo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Conductor_BloquearDesbloquear", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IDBloqueo", IdBloqueo));
                comando.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@User", Usuario));
                comando.Parameters.Add(new SqlParameter("@IdMotivo", IdMotivo));
                comando.Parameters.Add(new SqlParameter("@IdDesbloqueo", IdDesbloqueo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetViajes_Conductor_VerificaPermisoBloquear(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Conductor_VerificaPermisoBloqueoConductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@User", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetViajes_Conductor_VerificaPermisoCompromisoMemo(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_VerificaPermisoCompromisoMemo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@User", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetViajes_Conductor_VerificaPermisoModificarOperacion(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Conductor_VerificaPermiso_ModificarOperacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@User", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetViajes_Conductor_ListarConductoresBloqueados()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Conductor_Bloqueados_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetViajes_Conductor_ListarAdministrativos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Conductor_ListarAdministrativos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }



        //public DataSet GetDataVencimientoDocs()
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //        conexion.Open();
        //        SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM Venc_doc_conduct", conexion);
        //        adap.Fill(ds, "Venc_doc_conduct");
        //        return ds;
        //    }
        //    catch
        //    {
        //        return new DataSet();
        //    }
        //}

        //public DataTable GetDataGastosViajes()
        //{
        //    try
        //    {
        //        DataTable dtTemp = new DataTable();
        //        SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
        //        SqlCommand comando = new SqlCommand();

        //        comando.CommandText = "SELECT * from OP_TR_Gastos";
        //        comando.CommandType = CommandType.Text;
        //        comando.Connection = conexion;
        //        conexion.Open();

        //        dtTemp.Load(comando.ExecuteReader());
        //        return dtTemp;
        //    }
        //    catch
        //    {
        //        return new DataTable();
        //    }
        //}

        public DataTable GetOperaciones_ImportarGuiasExcel01(string xml, string usuario,string codigo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ImportarGuias_Excel", conexion);
                comando.CommandTimeout = 300;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@DataXML", xml));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", usuario));
                comando.Parameters.Add(new SqlParameter("@Codigo", codigo));
                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ImportarProgramaciones(string xml, string usuario, string codproga)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Programaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@DataXML", xml));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", usuario));
                comando.Parameters.Add(new SqlParameter("@codigo", codproga));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Operaciones_AnexarViajes(string xml, string usuario, string codproga)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_VincularTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@DataXML", xml));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", usuario));
                comando.Parameters.Add(new SqlParameter("@codigo", codproga));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }      

        public DataTable GetOperaciones_Guias_ver(int Opcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Guias _ver", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
       
        public DataTable UpdateGuias_Operaciones(string ticket, string guiaRemitente, string observacion,decimal Peso)            
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ActualizarGuias", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idticket", ticket));
                comando.Parameters.Add(new SqlParameter("@Guiaremision", guiaRemitente));
                comando.Parameters.Add(new SqlParameter("@Observacion", observacion));
                comando.Parameters.Add(new SqlParameter("@Peso", Peso));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetOperaciones_ListarPreviajeOperaciones(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ListarPreviajeProgramacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@user", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarPreviajeOperacionesAccesos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ListarPreviajeProgramacionAccesos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }        

        public DataTable GetOperaciones_ListarViajesConTolvaz(int idtracto, int idconductor, string codigo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarViajesXProgramacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDTRACTO", idtracto));
                comando.Parameters.Add(new SqlParameter("@IDCONDUCTOR", idconductor));
                comando.Parameters.Add(new SqlParameter("@CODIGOENLACE", codigo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarConsolidados(int viaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_PreviajesListarConsolidado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@viaje", viaje));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Previajes_ListarColumnas(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_TemporalColumnas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }        

        public DataTable GetOperaciones_ListarDireccionesxOts(int OTS)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ListarDireccionxOTS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@ots", OTS));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetOperaciones_ListarDatosOts(int OTS)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ListarDatosxOTS", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@ots", OTS));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Previaje_ListarColumnasSeleccionadasxUsuario(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarColumnasxUsuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }        

        public DataTable GetOperaciones_Registro_Previajes(int Accion, int IdProgramacion, string Sucursal, int TipoProgramacion, string FechaProgramacion, string FechaInicio, string FechaFin, int idTracto
                                                            , int idSemirremolque, int IdConductor, int IdConductorApoyo, int IdCliente, int IdRuta, string Destino, int Producto,
                                                           int Estado, string HoraSalida, string HoraLlegada, string FechaDescarga, decimal PesoAlmacen, decimal PesoCliente, decimal Merma
                                                            , string CodViaje, string Planilla, string Observacion, string UsuarioCrea, string Serie, string Numero,
                                                            string GuiaRemitente, string Zona, string Turno, string ConductorInicio, string PrimerCambio, string SegundoCambio,
                                                            string TercerCambio, int Ot, decimal Viaticos, string OrdenServicio, string UnidadApoyo, string guiaEntrega,
                                                            decimal montoOrdenServicio, int dia, int EsInterna, int idPartida, int IdLlegada,string anio,string PlacaMaquinaria,
                                                            string CodigoTolvaz, string programacionOrigen,string lineaConsolidado,string NombrePartida,string NombreDestino)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Registrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Accion", Accion));
                comando.Parameters.Add(new SqlParameter("@IdProgramacion", IdProgramacion));
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                comando.Parameters.Add(new SqlParameter("@TipoProgramacion", TipoProgramacion));
                comando.Parameters.Add(new SqlParameter("@FechaProgramacion", FechaProgramacion));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                comando.Parameters.Add(new SqlParameter("@idSemirremolque", idSemirremolque));
                comando.Parameters.Add(new SqlParameter("@IdConductor", IdConductor));
                comando.Parameters.Add(new SqlParameter("@IdConductorApoyo", IdConductorApoyo));
                comando.Parameters.Add(new SqlParameter("@IdCliente", IdCliente));
                comando.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                comando.Parameters.Add(new SqlParameter("@Destino", Destino));
                comando.Parameters.Add(new SqlParameter("@Producto", Producto));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@HoraSalida", HoraSalida));
                comando.Parameters.Add(new SqlParameter("@HoraLlegada", HoraLlegada));
                comando.Parameters.Add(new SqlParameter("@FechaDescarga", FechaDescarga));
                comando.Parameters.Add(new SqlParameter("@PesoAlmacen", PesoAlmacen));
                comando.Parameters.Add(new SqlParameter("@PesoCliente", PesoCliente));
                comando.Parameters.Add(new SqlParameter("@Merma", Merma));               
                comando.Parameters.Add(new SqlParameter("@CodViaje", CodViaje));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                comando.Parameters.Add(new SqlParameter("@UserCrea", UsuarioCrea));
                comando.Parameters.Add(new SqlParameter("@Serie", Serie));
                comando.Parameters.Add(new SqlParameter("@Numero", Numero));
                comando.Parameters.Add(new SqlParameter("@GuiaRemitente", GuiaRemitente));
                comando.Parameters.Add(new SqlParameter("@Zona", Zona));
                comando.Parameters.Add(new SqlParameter("@Turno", Turno));
                comando.Parameters.Add(new SqlParameter("@ConductorInicio", ConductorInicio));
                comando.Parameters.Add(new SqlParameter("@PrimerCambio", PrimerCambio));
                comando.Parameters.Add(new SqlParameter("@SegundoCambio", SegundoCambio));
                comando.Parameters.Add(new SqlParameter("@TercerCambio", TercerCambio));
                comando.Parameters.Add(new SqlParameter("@OT", Ot));
                comando.Parameters.Add(new SqlParameter("@Viaticos", Viaticos));
                comando.Parameters.Add(new SqlParameter("@OrdenServicio",OrdenServicio));
                comando.Parameters.Add(new SqlParameter("@UnidadApoyo", UnidadApoyo));
                comando.Parameters.Add(new SqlParameter("@guiaEntrega", guiaEntrega));
                comando.Parameters.Add(new SqlParameter("@MontoOrdServ", montoOrdenServicio));
                comando.Parameters.Add(new SqlParameter("@dia", dia));
                comando.Parameters.Add(new SqlParameter("@EsInterna", EsInterna));
                comando.Parameters.Add(new SqlParameter("@Partida", idPartida));
                comando.Parameters.Add(new SqlParameter("@Llegada", IdLlegada));
                comando.Parameters.Add(new SqlParameter("@anio", anio));
                comando.Parameters.Add(new SqlParameter("@PlacaMaquinaria", PlacaMaquinaria));
                comando.Parameters.Add(new SqlParameter("@CodigoTolvaz", CodigoTolvaz));
                comando.Parameters.Add(new SqlParameter("@ProgramacionOrigen", programacionOrigen ));
                comando.Parameters.Add(new SqlParameter("@LineaConsolidado", lineaConsolidado));
                comando.Parameters.Add(new SqlParameter("@NombreDireccionPartida", NombrePartida));
                comando.Parameters.Add(new SqlParameter("@NombreDireccionDestino", NombreDestino));

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarProgramaciones(string sucursal, string FechaPrograInicio, string FechaProgFin, int pro1, int prog2, int prog3, int prog4, int prog5)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;

                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Listar",conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@sucursal", sucursal));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaPrograInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaProgFin));
                comando.Parameters.Add(new SqlParameter("@prog1", pro1));
                comando.Parameters.Add(new SqlParameter("@prog2", prog2));
                comando.Parameters.Add(new SqlParameter("@prog3", prog3));
                comando.Parameters.Add(new SqlParameter("@prog4", prog4));
                comando.Parameters.Add(new SqlParameter("@prog5", prog5));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarProgramacionesPorProgramacion(string sucursal, string FechaPrograInicio, string FechaProgFin, int pro1)
        {

            SqlCommand comando = null;
            DataTable dtTemp = new DataTable();
            try
            {
                
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                

                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarPorProgramacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@sucursal", sucursal));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaPrograInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaProgFin));
                comando.Parameters.Add(new SqlParameter("@prog1", pro1));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = "No se encontraron Datos";
            }

            finally { comando.Connection.Close(); }
            return dtTemp;
        }

        public DataTable GetOperaciones_ListarSucursalPreviajes(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ListarSucursalPreviaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@user", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarCodigosVicncular()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ListarCodigosVicncular", conexion);
                comando.CommandType = CommandType.StoredProcedure;
               // comando.Parameters.Add(new SqlParameter("@user", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Operaciones_ListarViajesAltra(string codigo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarViajesAltra", conexion);
                comando.CommandType = CommandType.StoredProcedure;
               comando.Parameters.Add(new SqlParameter("@codigo", codigo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        
        public DataTable GetOperaciones_Previajes_PrograPendientes(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ConsultaProgPendientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@USER", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Previajes_AnularViajes(string idviaje, int idProg, string Observacion, string Usuario, string Anio, int tipoPro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Programacion_AnularViajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdViaje", idviaje));
                comando.Parameters.Add(new SqlParameter("@idticket",idProg));
                comando.Parameters.Add(new SqlParameter("@observacion",Observacion)); 
                comando.Parameters.Add(new SqlParameter("@UserCrea",Usuario));
                comando.Parameters.Add(new SqlParameter("@anio", Anio));
                comando.Parameters.Add(new SqlParameter("@tipoPro", tipoPro));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Programaciones_ValidarUnidad(int idUnidad, string fechainicio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Programacion_ValidarUnidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                comando.Parameters.Add(new SqlParameter("@fechaIni", fechainicio));
                //comando.Parameters.Add(new SqlParameter("@fechaFin", fechafin));  
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_Programaciones_DcunetosVencidos(int IDRELACION, string TIPORELACION)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_ListarDocumentosPorVencerYVencidos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TipoRelacion ", TIPORELACION));
                comando.Parameters.Add(new SqlParameter("@IDRelacion ", IDRELACION));
                //comando.Parameters.Add(new SqlParameter("@fechaFin", fechafin));  
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarUbicacionGps(string Unidad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_VerUbicacionUnidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Unidad", Unidad));               
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        
        public DataTable GetOperaciones_PreviajesAgregaPeso(int idProgPeso,decimal Peso)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_AgregarPeso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idPrograPeso", idProgPeso));
                comando.Parameters.Add(new SqlParameter("@PesoCliente", Peso));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetOperaciones_ListarUnidadesActivas()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarUnidadesActivas", conexion);
                comando.CommandType = CommandType.StoredProcedure;                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarUnidadesBloqueadas(string Placa, string TipoVehiculo, string Operacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarUnidadesBloqueadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@TipoVehiculo", TipoVehiculo));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }
        

        public DataTable GetOperaciones_Previajes_ListarAccesos()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarAccesos", conexion);
                comando.CommandType = CommandType.StoredProcedure;                
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        

        public DataTable GetOperaciones_Previajes_ListarPrecios(int idvehiculo, int ruta, int producto, string sucursal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarPrecios", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idvehiculo", idvehiculo));
                comando.Parameters.Add(new SqlParameter("@PRODUCTO", producto));
                comando.Parameters.Add(new SqlParameter("@IDRUTA", ruta));
                comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarPreviajeOtsDisponibles()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarOtsDisponibles", conexion);
                comando.CommandType = CommandType.StoredProcedure;
             
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetOperaciones_PreviajesAgregaPesoObservacion(int Accion, int idProgPeso, decimal Peso, string Observacion,string anio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_AgregarObservacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Accion", Accion));
                comando.Parameters.Add(new SqlParameter("@idPrograPeso", idProgPeso));
                comando.Parameters.Add(new SqlParameter("@PesoCliente", Peso));
                comando.Parameters.Add(new SqlParameter("@Comentario", Observacion));
                comando.Parameters.Add(new SqlParameter("@anio", anio));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_PreviajesAgregaAcceso(string user, int programacion, string sucursal, int accesonivel)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_AgregarAccesos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@USUARIO", user));
                comando.Parameters.Add(new SqlParameter("@PROGRMACION", programacion));
                comando.Parameters.Add(new SqlParameter("@SUCURSAL", sucursal));
                comando.Parameters.Add(new SqlParameter("@ACCESO", accesonivel));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

          public DataTable GetOperaciones_Conductor_AsignarOperacion(int IdPersona, int IdOperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Conductor_AsignarOperacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdPersona", IdPersona));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }


          public DataTable GetOperaciones_Programaciones_GuardarDestino(int opcion, int idot, string destino, decimal km, int dia, string zona, int idRuta, int idTipoProgramacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_AgregarDestino", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@opcion", opcion));
                comando.Parameters.Add(new SqlParameter("@idot", idot));
                comando.Parameters.Add(new SqlParameter("@destino", destino));
                comando.Parameters.Add(new SqlParameter("@km", km));
                comando.Parameters.Add(new SqlParameter("@dia", dia));
                comando.Parameters.Add(new SqlParameter("@zona", zona));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@idTipoProgramacion", idTipoProgramacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_PreviajesCambiarPosicion(int IdProgramaCambiar, int CodigoCambiar, int CodigoCambiaRecibe, int IdProgramaCambiaRecibe)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_CambiarPosicion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdProgramaCambiar", IdProgramaCambiar));
                comando.Parameters.Add(new SqlParameter("@CodigoCambiar", CodigoCambiar));
                comando.Parameters.Add(new SqlParameter("@CodigoCambiaRecibe", CodigoCambiaRecibe));
                comando.Parameters.Add(new SqlParameter("@IdProgramaCambiaRecibe", IdProgramaCambiaRecibe));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarPreviajeImpreso(string CODIGO)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_VerificarImpresion", conexion);
                comando.Parameters.Add(new SqlParameter("@NroTicket", CODIGO));
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();            
            }

            return dtTemp;
        }

        public DataTable Obtener_Lista_ConductoresBloqueados()
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {              
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ConductoresBloqueados", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
            }
            catch(Exception )
            {
                dtTemp = null;

            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(int Opcion, int TipoVehiculo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@TipoVehiculo", TipoVehiculo));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable GetOperaciones_ListarUnidadesConductor(int TipoVehiculo, int SubTipoVehiculo, int idOperacion, string Placa)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ConductorUnidades_Listar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TipoVehiculo", TipoVehiculo));
                comando.Parameters.Add(new SqlParameter("@SubTipoVehiculo", SubTipoVehiculo));
                comando.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception) { dtTemp = null; }
            finally { conexion.Close(); }
            return dtTemp;
        }

        public DataTable ReportesApp_Operaciones_ConductorUnidades_FiltrarUnidad(int IdVehiculo)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ConductorUnidades_FiltrarUnidad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdVehiculo", IdVehiculo));
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception) { dtTemp = null; }
            finally { conexion.Close(); }
            return dtTemp;
        }

        public DataTable GetOperaciones_Operaciones_UnidadesConductor_CreaModifica(int Accion, int nroregisrto, int idunidad, int Operacion, string Observacion, string Usuario, int Mochila,
                         int tomafuerza, int urea, int manguera, int Senaletica, int LlaveOriginal, int LlaveDuplicada, int Camaras, string Transmision, decimal Peso, decimal Galones, string TipoCortina,
                         string ModeloChasis, string Nivel, string TipoNivel, string Suspension, string Piso, string MaterialPiso, string Piso2, string MaterialPiso2, int NroLlantas, string Planos,
                         string Vitacora, string Bocamaza)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ConductorUnidades_CreaModifica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Accion", Accion);
                comando.Parameters.AddWithValue("@IdRegistro", nroregisrto);
                comando.Parameters.AddWithValue("@Idunidad", idunidad);
                comando.Parameters.AddWithValue("@IdOperacion", Operacion);
                comando.Parameters.AddWithValue("@Observacion", Observacion);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                comando.Parameters.AddWithValue("@Mochila", Mochila);
                comando.Parameters.AddWithValue("@Tomafuerza", tomafuerza);
                comando.Parameters.AddWithValue("@Urea", urea);
                comando.Parameters.AddWithValue("@Manguera", manguera);
                comando.Parameters.AddWithValue("@Senaletica", Senaletica);
                comando.Parameters.AddWithValue("@LlaveOriginal", LlaveOriginal);
                comando.Parameters.AddWithValue("@LlaveDuplicada", LlaveDuplicada);
                comando.Parameters.AddWithValue("@Camaras", Camaras);
                comando.Parameters.AddWithValue("@Transmision", Transmision);
                comando.Parameters.AddWithValue("@Peso", Peso);
                comando.Parameters.AddWithValue("@Galones", Galones);
                comando.Parameters.AddWithValue("@TipoCortina", TipoCortina);
                comando.Parameters.AddWithValue("@ModeloChasis", ModeloChasis);
                comando.Parameters.AddWithValue("@Nivel", Nivel);
                comando.Parameters.AddWithValue("@TipoNivel", TipoNivel);
                comando.Parameters.AddWithValue("@Suspension", Suspension);
                comando.Parameters.AddWithValue("@Piso", Piso);
                comando.Parameters.AddWithValue("@MaterialPiso", MaterialPiso);
                comando.Parameters.AddWithValue("@Piso2", Piso2);
                comando.Parameters.AddWithValue("@MaterialPiso2", MaterialPiso2);
                comando.Parameters.AddWithValue("@NroLlantas", NroLlantas);
                comando.Parameters.AddWithValue("@Planos", Planos);
                comando.Parameters.AddWithValue("@Bitacora", Vitacora);
                comando.Parameters.AddWithValue("@Bocamaza", Bocamaza);
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception) { dtTemp = null; }
            finally { conexion.Close(); }
            return dtTemp;
        }

        public DataTable Obtener_Lista_HistoricoDesbloqueos(string fechaini, string fechafin, string persona, int ValFecha, int ValMotivo, int IdMotivo, int filtro)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Persona_RptListaHistoricaBloqueosPersona", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaIni", fechaini));
                comando.Parameters.Add(new SqlParameter("@FechaFin", fechafin));
                comando.Parameters.Add(new SqlParameter("@Persona", persona));
                comando.Parameters.Add(new SqlParameter("@ValFecha", ValFecha));
                comando.Parameters.Add(new SqlParameter("@ValMotivo", ValMotivo));
                comando.Parameters.Add(new SqlParameter("@IdMotivo", IdMotivo));
                comando.Parameters.Add(new SqlParameter("@Filtro", filtro));
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception)
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }
            return dtTemp;
        }

        public DataTable Obtener_Lista_ProgramacionesAnulados(string fechaini, string fechafin)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {

                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_RptProgramacionesAnulados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHINI", fechaini));
                comando.Parameters.Add(new SqlParameter("@FECHFIN", fechafin));
                dtTemp.Load(comando.ExecuteReader());

            }
            catch (Exception )
            {
                dtTemp = null;

            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable ReportesApp_ListarMotivoBloqueo()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarMotivoBloqueo", conexion);
                cmd.Parameters.Add(new SqlParameter("@User", Utilitario.Instancia.SesionUsuario.usuario));
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

        public DataTable ReportesApp_ListarEstadoDesbloqueo()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarEstadoDesbloqueo", conexion);
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
        public DataTable Llenar_ControlesPreViajes()
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {

                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_LlenadoControlesFormularioPreViajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());

            }
            catch(Exception )
            {
                dtTemp = null;

            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable Cambiar_PosicionPreviaje(int IdPreviajeActual, int IdPreviajeNuevo, int EsMenorMayor, string Usuario, string anio)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {

                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_CambiarPosicionPreViajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdPreviajeActual", IdPreviajeActual);
                comando.Parameters.AddWithValue("@IdPreviajeNuevo", IdPreviajeNuevo);
                comando.Parameters.AddWithValue("@EsMenorMayor", EsMenorMayor);
                comando.Parameters.AddWithValue("@UserCrea", Usuario);
                comando.Parameters.AddWithValue("@anio", anio);

                dtTemp.Load(comando.ExecuteReader());

            }
            catch(Exception )
            {
                dtTemp = null;

            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable Cambiar_Observacion_Previajes(int IdPreviaje, string Observacion, string Usuario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_CambiarObservacionPreViajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdPreviaje", IdPreviaje);
                comando.Parameters.AddWithValue("@Observacion", Observacion);
                comando.Parameters.AddWithValue("@UserCrea", Usuario);
                dtTemp.Load(comando.ExecuteReader());

            }
            catch(Exception )
            {
                dtTemp = null;

            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }


        public DataTable GetOperaciones_Previajes_ActualizarImpresion(string Codigo)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_BloaquearImpresion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@NroTicket", Codigo);
            
                dtTemp.Load(comando.ExecuteReader());

            }
            catch(Exception )
            {
                dtTemp = null;

            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }
        public DataTable GetOperaciones_PreviajesAgregaGuias(int idprogramacion, string guiatrans, string guiaremi, string guiaentrega, string Usuario, string anio,
                                                             int ot, int tipoprogramacion, int dirPartida, int dirDestino,decimal pesoCarga,decimal pesoCliente,
                                                             decimal PesoCombustible, string transportista2, bool esRetorno)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_AgregarGuia", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idprogrmacion", idprogramacion);
                comando.Parameters.AddWithValue("@guiatransportista", guiatrans);
                comando.Parameters.AddWithValue("@guiaremitente", guiaremi);
                comando.Parameters.AddWithValue("@guiaotros", guiaentrega);
                comando.Parameters.AddWithValue("@UserCrea", Usuario);
                comando.Parameters.AddWithValue("@anio", anio);
                comando.Parameters.AddWithValue("@ot", ot);
                comando.Parameters.AddWithValue("@tipoprogramacion", tipoprogramacion);
                comando.Parameters.AddWithValue("@direccionPartida", dirPartida);
                comando.Parameters.AddWithValue("@direccionDestino", dirDestino);
                comando.Parameters.AddWithValue("@pesoCarga", pesoCarga);
                comando.Parameters.AddWithValue("@pesoCliente", pesoCliente);
                comando.Parameters.AddWithValue("@PesoCombustible", PesoCombustible);
                
                if (transportista2.Length > 0)
                { comando.Parameters.AddWithValue("@guiatransportista2", transportista2); }
                else
                { comando.Parameters.AddWithValue("@guiatransportista2", DBNull.Value); }

                comando.Parameters.AddWithValue("@esRetorno", esRetorno);
                dtTemp.Load(comando.ExecuteReader());

            }
            catch(Exception) { dtTemp = null; }
            finally { conexion.Close(); }

            return dtTemp;
        }

        public DataTable GetOperaciones_Previajes_RegistroFiltroColumnas(string cadena,string Usuario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_RegistrarFiltroColumnas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@datosxml", cadena);
                comando.Parameters.AddWithValue("@usuario", Usuario);
                dtTemp.Load(comando.ExecuteReader());

            }
            catch(Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable GetOperaciones_Previajes_ListarImpresora(string Usuario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarImpresora", conexion);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                comando.CommandType = CommandType.StoredProcedure;               
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }
        public DataTable GetOperaciones_Previajes_ListarConductores()
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarConductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;               
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable GetOperaciones_ListarTipoProgramaciones()
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());

            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarOperaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;               
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable GetOperaciones_ListarUnidades(string Placa, string TipoVehiculo, string Operacion)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_ListarUnidades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@TipoVehiculo", TipoVehiculo));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception) { dtTemp = null; }
            finally { conexion.Close(); }
            return dtTemp;
        }

        public DataTable GetOperaciones_ListarMotivoBloqueo(string Usuario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Unidades_MotivoBloqueo", conexion);
                comando.Parameters.AddWithValue("@usuario", Usuario);
                comando.CommandType = CommandType.StoredProcedure;               
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }
        public DataTable GetLista_Consulta_Permiso_BloqueoDesbloqueo(string Usuario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Conductor_VerificaPermisoBloqueoUnidades", conexion);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                comando.CommandType = CommandType.StoredProcedure;               
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

        public DataTable GetLista_Consulta_Permiso_AsignarUnidadesConductor(string Usuario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ConductorUnidades_VerificaPermisoAsisgnar", conexion);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

         public DataTable GetLista_Operaciones_Clientes_Listar(char tipo, int IdPersona)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_listar", conexion);
                comando.Parameters.AddWithValue("@Tipo", tipo);
                comando.Parameters.AddWithValue("@IdPersona", IdPersona);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

         public DataTable GetLista_Operaciones_Clientes_ListarContactos(char tipo, int IdPersona, int IdCompania)
         {
             DataTable dtTemp = new DataTable();
             SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
             try
             {
                 conexion.Open();
                 SqlCommand comando;
                 comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_CabeceraListar", conexion);
                 comando.Parameters.AddWithValue("@Tipo", tipo);
                 comando.Parameters.AddWithValue("@IdPersona", IdPersona);
                 comando.Parameters.AddWithValue("@IdCompania", IdCompania);
                 comando.CommandType = CommandType.StoredProcedure;
                 dtTemp.Load(comando.ExecuteReader());
             }
             catch (Exception )
             {
                 dtTemp = null;
             }
             finally
             {
                 conexion.Close();
             }

             return dtTemp;
         }

         public DataTable GetLista_Operaciones_Clientes_ContactosListar(int IdPersona, char tipo, int Opcion)
         {
             DataTable dtTemp = new DataTable();
             SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
             try
             {
                 conexion.Open();
                 SqlCommand comando;
                 comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_ContactosListar", conexion);
                 comando.Parameters.AddWithValue("@IdPersona", IdPersona);
                 comando.Parameters.AddWithValue("@tipo", tipo);
                 comando.Parameters.AddWithValue("@Opcion", Opcion);
                 comando.CommandType = CommandType.StoredProcedure;
                 dtTemp.Load(comando.ExecuteReader());
             }
             catch (Exception)
             {
                 dtTemp = null;
             }

             finally
             {
                 conexion.Close();
             }

             return dtTemp;
         }

          public DataTable GetLista_Operaciones_Clientes_Contactos_AgregarModificarQuitar(int accion, char tipo, int idregistro, int idpersona, int idcontacto, int ContactoDetalle, string nombre, string cargo,
                                                                                        string telefono,string celular,string correo,string direccion,string lugar,string Usuario)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
            try
            {
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContacto", conexion);
                comando.Parameters.AddWithValue("@Accion", accion);
                comando.Parameters.AddWithValue("@TIPO", tipo);
                comando.Parameters.AddWithValue("@idregistro", idregistro);
                comando.Parameters.AddWithValue("@idpersona", idpersona);
                comando.Parameters.AddWithValue("@idcontacto",idcontacto );
                comando.Parameters.AddWithValue("@idcontactodetalle", ContactoDetalle);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@cargo", cargo);
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@celular", celular);
                comando.Parameters.AddWithValue("@correo", correo);
                comando.Parameters.AddWithValue("@direccion", direccion);
                comando.Parameters.AddWithValue("@lugar", lugar);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
            }
            catch (Exception )
            {
                dtTemp = null;
            }
            finally
            {
                conexion.Close();
            }

            return dtTemp;

        }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos(int Accion)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos", conexion);
                  comando.Parameters.AddWithValue("@Accion", Accion);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }
              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContrato(int Accion, char Tipo, int IdContrato, int IdPersona, int IdContacto, int IdContactoDetalle, int IdTipoContrato, string Titulo, string Descripcion, string Consideracion,
                                                                                              string Beneficios, DateTime FechaInicio, DateTime FechaFin, int DiasAlerta, int Todos, string xml, string RutaEnlace, Byte Validacion, string Usuario)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContrato", conexion);
                  comando.Parameters.AddWithValue("@Accion", Accion);
                  comando.Parameters.AddWithValue("@Tipo", Tipo);
                  comando.Parameters.AddWithValue("@IdContrato", IdContrato);
                  comando.Parameters.AddWithValue("@IdPersona", IdPersona);
                  comando.Parameters.AddWithValue("@IdContacto", IdContacto);
                  comando.Parameters.AddWithValue("@IdContactoDetalle", IdContactoDetalle);
                  comando.Parameters.AddWithValue("@IdTipoContrato", IdTipoContrato);
                  comando.Parameters.AddWithValue("@Titulo", Titulo);
                  comando.Parameters.AddWithValue("@Descripcion", Descripcion);
                  comando.Parameters.AddWithValue("@Consideracion", Consideracion);
                  comando.Parameters.AddWithValue("@Beneficio", Beneficios);
                  comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                  comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                  comando.Parameters.AddWithValue("@DiasAlerta", DiasAlerta);
                  comando.Parameters.AddWithValue("@Todos", Todos);
                  comando.Parameters.AddWithValue("@xml", xml);
                  comando.Parameters.AddWithValue("@RutaEnlace", RutaEnlace);
                  comando.Parameters.AddWithValue("@Validacion", Validacion);
                  comando.Parameters.AddWithValue("@Usuario", Usuario);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }
              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarContrato(int IdContrato)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_ListarContrato", conexion);
                  comando.Parameters.AddWithValue("@IdContrato", IdContrato);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }

              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarAreasInvolucradas(int IdContrato)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_ListarAreasInvolucradas", conexion);
                  comando.Parameters.AddWithValue("@IdContrato", IdContrato);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }

              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_FiltrarContratos(char TipoPersonal, string NombrePersonal, string FechaInicio, string FechaFin, int IdCompania, Byte Validacion)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_FiltrarContratos", conexion);
                  comando.Parameters.AddWithValue("@TipoPersonal", TipoPersonal);
                  comando.Parameters.AddWithValue("@NombrePersonal", NombrePersonal);
                  comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                  comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                  comando.Parameters.AddWithValue("@IdCompania", IdCompania);
                  comando.Parameters.AddWithValue("@Valido", Validacion);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }

              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_AnularContratos(int IdContrato, byte Validacion, string Usuario)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_AnularContratos", conexion);
                  comando.Parameters.AddWithValue("@IdContrato", IdContrato);
                  comando.Parameters.AddWithValue("@Validacion", Validacion);
                  comando.Parameters.AddWithValue("@Usuario", Usuario);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }

              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_InsertarAdenda(int IdContrato, string Titulo, string Descripcion, string Consideracion, string Beneficios, DateTime FechaInicio, DateTime FechaFin, int DiasAlerta, string RutaEnlace, string Usuario)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_InsertarAdenda", conexion);
                  comando.Parameters.AddWithValue("@IdContrato", IdContrato);
                  comando.Parameters.AddWithValue("@Titulo", Titulo);
                  comando.Parameters.AddWithValue("@Descripcion", Descripcion);
                  comando.Parameters.AddWithValue("@Consideracion", Consideracion);
                  comando.Parameters.AddWithValue("@Beneficios", Beneficios);
                  comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                  comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                  comando.Parameters.AddWithValue("@DiasAlerta", DiasAlerta);
                  comando.Parameters.AddWithValue("@RutaEnlace", RutaEnlace);
                  comando.Parameters.AddWithValue("@Usuario", Usuario);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }

              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarAdendas(int Opcion, int IdContrato)
          {
              try
              {
                  DataTable dtTemp = new DataTable();
                  SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                  conexion.Open();
                  SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_ListarAdendas", conexion);
                  comando.CommandType = CommandType.StoredProcedure;
                  comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                  comando.Parameters.Add(new SqlParameter("@IdContrato", IdContrato));
                  comando.CommandTimeout = 0;
                  dtTemp.Load(comando.ExecuteReader());
                  return dtTemp;
              }
              catch
              {
                  return new DataTable();
              }
          }

          public DataTable ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas(int Opcion, int idAdenda, int IdContrato)
          {
              try
              {
                  DataTable dtTemp = new DataTable();
                  SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                  conexion.Open();
                  SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas", conexion);
                  comando.CommandType = CommandType.StoredProcedure;
                  comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                  comando.Parameters.Add(new SqlParameter("@idAdenda", idAdenda));
                  comando.Parameters.Add(new SqlParameter("@IdContrato", IdContrato));
                  comando.CommandTimeout = 0;
                  dtTemp.Load(comando.ExecuteReader());
                  return dtTemp;
              }
              catch
              {
                  return new DataTable();
              }
          }

          public DataTable GetLista_Operaciones_Clientes_Contactos_AgregarCabcera(int accion, char tipo, int idregistro, int idpersona, int IdCompania, string Observacion, string Usuario)
          {
              DataTable dtTemp = new DataTable();
              SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
              try
              {
                  conexion.Open();
                  SqlCommand comando;
                  comando = new SqlCommand("ReportesApp_Operaciones_ClientesProveedores_AgregaCabecera", conexion);
                  comando.Parameters.AddWithValue("@Accion", accion);
                  comando.Parameters.AddWithValue("@TIPO", tipo);
                  comando.Parameters.AddWithValue("@idregistro", idregistro);
                  comando.Parameters.AddWithValue("@idpersona", idpersona);
                  comando.Parameters.AddWithValue("@IdCompania", IdCompania);
                  comando.Parameters.AddWithValue("@Observacion", Observacion);
                  comando.Parameters.AddWithValue("@Usuario", Usuario);
                  comando.CommandType = CommandType.StoredProcedure;
                  dtTemp.Load(comando.ExecuteReader());
              }
              catch (Exception)
              {
                  dtTemp = null;
              }
              finally
              {
                  conexion.Close();
              }

              return dtTemp;
          }
  
        public DataTable GetViajes_ClienteProveedor_VerificaPermisoModificar(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_ClienteProveedor_VeriPermisoEditar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@User", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataListarUsuariocrearMemos(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_FichaConductores_CarpetaMemo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@User", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetPeriodoRRHH()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT DISTINCT 1 id, LEFT([Periodo],6) Descripcion FROM [spring].[dbo].[PR_ProcesoPeriodo] WHERE ESTADO='A' ORDER BY LEFT([Periodo],6) asc";
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
        public DataTable GetVerMermasxConductor(int IdConductorDetalle, string FechaInicioDetalle, string FechaFinDetalle)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_BonoConductores_MermasDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdConductor", IdConductorDetalle));
                comando.Parameters.Add(new SqlParameter("@FechaIni", FechaInicioDetalle));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFinDetalle));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetMoverProgramaciones(int NroPreviaje, int tipoProgramacion, int tipoPrograMover, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_MoverOperaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroPreviaje", NroPreviaje));
                comando.Parameters.Add(new SqlParameter("@TipoPreviaje", tipoProgramacion));
                comando.Parameters.Add(new SqlParameter("@TipoPreviajeMover", tipoPrograMover));
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

        public DataTable GetOperacionesMover()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT IdOperacion,Descripcion FROM ReportesApp_Operacion_Previaje_Operaciones where IdOperacion IN (1,2,3,4,8,9,10,11)";
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

        ///tracking 03-11-202

        public DataTable GetPreviajes_ListarTickets(string NroTicketPreViaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarTickets", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicketPreViaje", NroTicketPreViaje));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPreviajes_ListarEstadoPorOperacion(int tipoOperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarEstadoPorOperacion", conexion);
                comando.Parameters.Add(new SqlParameter("@tipoOperacion ", tipoOperacion));
                comando.CommandType = CommandType.StoredProcedure;

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }

        }
        public DataTable GetPreviajes_ListarEventoPorOperacion(int TipoBusqueda, int tipoOperacion, int idprogramacion, int anio, string IdProducto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarPorEvento", conexion);
                comando.Parameters.Add(new SqlParameter("@Opcion ", TipoBusqueda));
                comando.Parameters.Add(new SqlParameter("@tipoOperacion ", tipoOperacion));
                comando.Parameters.Add(new SqlParameter("@IdProgramacion ", idprogramacion));
                comando.Parameters.Add(new SqlParameter("@Anio ", anio));
                comando.Parameters.Add(new SqlParameter("@IdProducto ", IdProducto));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }


        public DataTable GetPreviajes_RegistrarTracking(int accion, string Usuario, int idprogramacion, int IdTipoProgramacion, string RegistroFechaEvento, string anio,
                                                        string tipocarga,string ubicacion, string codigoevento, string estadotracking, int idtracking, string ubicacionGPS,
                                                        string UbicacionGeo, int idpunto, int TipoViaje, string IdProducto, int SeguimientoxProducto, string Item)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previaje_RegistrarTracking", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Accion", accion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.Parameters.Add(new SqlParameter("@idprogramacion", idprogramacion));
                comando.Parameters.Add(new SqlParameter("@idTipoProgramacion", IdTipoProgramacion));
                comando.Parameters.Add(new SqlParameter("@fecha", RegistroFechaEvento));
                comando.Parameters.Add(new SqlParameter("@anio", anio));
                comando.Parameters.Add(new SqlParameter("@tipocarga", tipocarga));
                comando.Parameters.Add(new SqlParameter("@ubicacion", ubicacion));
                comando.Parameters.Add(new SqlParameter("@codigoevento", codigoevento));
                comando.Parameters.Add(new SqlParameter("@estadotracking", estadotracking));
                comando.Parameters.Add(new SqlParameter("@idtracking", idtracking));
                comando.Parameters.Add(new SqlParameter("@ubicacionGPS", ubicacionGPS));
                comando.Parameters.Add(new SqlParameter("@UbicacionGeo", UbicacionGeo));
                comando.Parameters.Add(new SqlParameter("@idpunto", idpunto));
                comando.Parameters.Add(new SqlParameter("@tipoviaje", TipoViaje));
                comando.Parameters.Add(new SqlParameter("@IdProducto", IdProducto));
                comando.Parameters.Add(new SqlParameter("@SeguimientoxProducto", SeguimientoxProducto));
                comando.Parameters.Add(new SqlParameter("@Item", Item));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarUbicacion(string Unidad, string fecharegistro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Combustible_OdometroUT_Ubicacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Unidad", Unidad));
                comando.Parameters.Add(new SqlParameter("@fecharegistro", fecharegistro));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPreviajes_ListarTracking(int var_IdProg,string anio)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previaje_ListarTracking", conexion);
                comando.Parameters.Add(new SqlParameter("@idprogramacion", var_IdProg));
                comando.Parameters.Add(new SqlParameter("@Anio", anio));
                comando.CommandType = CommandType.StoredProcedure;

                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }
        
        public DataTable GetOperaciones_ListarTrackingEditar(int idTracking)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previaje_ListarTrackingEditar", conexion);
                comando.Parameters.Add(new SqlParameter("@idtracking", idTracking));
                comando.CommandType = CommandType.StoredProcedure;

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }
        public DataTable GetPuntosPorRutas(string _punto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT  idPuntos, descripcion FROM ReportesApp_Operacion_Previaje_Puntos  where  descripcion like '" + _punto + "%'";
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

        public DataTable GetListarDetalleProgramacion(int idprogramacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previaje_Tracking_ListarDetalleProgramacion", conexion);
                comando.Parameters.Add(new SqlParameter("@idCodigoProgramacion", idprogramacion));
                comando.CommandType = CommandType.StoredProcedure;

                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarPorCliente(int IdOt, int Anio, int IdProgramacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previaje_ListarConsolidado", conexion);                
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.Parameters.Add(new SqlParameter("@IdProgramacion", IdProgramacion));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetGuiasxEntregarConductor(int opcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_GuiasxEntregarConductor", conexion);
                comando.Parameters.Add(new SqlParameter("@opcion", opcion));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetOperaciones_ListarReporteTracking(int idprogramacion, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previaje_Traking_ReporteOperacion", conexion);
                comando.Parameters.Add(new SqlParameter("@TipoProgramacion", idprogramacion));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public static DataTable GetDataPermisoRegitroExcepciones(string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_BonoConductores_PermisoRegistrarExcepciones", conexion);
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public static DataTable GetLista_Operaciones_Destinos(int idruta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_VerDestino", conexion);
                comando.Parameters.Add(new SqlParameter("@idruta", idruta));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }               

        public static DataTable GetLista_Operaciones_Previajes_PorCompletar(int Opcion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_PorCompletar", conexion);
                comando.Parameters.Add(new SqlParameter("@OPCION", Opcion));
                comando.Parameters.Add(new SqlParameter("@USUARIO", Usuario));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataActualizarOrdenDestino(string cadena, string Usuario, int idoperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ActualizarOrdenDestino", conexion);
                comando.Parameters.Add(new SqlParameter("@XmlDatos", cadena));
                comando.Parameters.Add(new SqlParameter("@USUARIO", Usuario));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", idoperacion));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetLista_Operaciones_DestinosListar(int IdOperacion)
        {
            try
            {
               /* DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ActualizarOrdenDestino", conexion);                
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;*/

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando = new SqlCommand();

                comando.CommandText = "SELECT DISTINCT R.DESTINO, R.ORDEN " +
                                        " FROM ReportesApp_Operacion_PrevDestinos R "+ 
	                                    " INNER JOIN OP_TR_Ruta RD ON R.IdRuta = RD.IdRuta "+
                                        " WHERE IdOperacion = "+ IdOperacion +" ORDER BY ORDEN ASC";
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

        public DataTable GetDataReporteAsistenciaCondutores(string FechaInicio, string FechaFin, int Cesados)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_AsistenciasReportexFechas", conexion);
                comando.Parameters.Add(new SqlParameter("@FechaFiltroInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFiltroFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@cesados", Cesados));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataReporteAsistenciaxCompensar(int Cesados)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_AsistenciasReportexFechas_porCompensar", conexion);
                //comando.Parameters.Add(new SqlParameter("@FechaFiltroInicio", FechaInicio));
                //comando.Parameters.Add(new SqlParameter("@FechaFiltroFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@cesados", Cesados));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataReporteAsistenciaVacaciones(string TipoPermiso)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_RRHH_AsistenciasReportexFechas_Vacaciones", conexion);
                //comando.Parameters.Add(new SqlParameter("@FechaFiltroInicio", FechaInicio));
                //comando.Parameters.Add(new SqlParameter("@FechaFiltroFin", FechaFin));
                if (TipoPermiso == "VER CONDUCTORES")
                {
                    comando.Parameters.Add(new SqlParameter("@TipoPermiso", "CONDUCTOR"));
                }
                else
                {
                    comando.Parameters.Add(new SqlParameter("@TipoPermiso", DBNull.Value));
                }

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataBuscarGuiasModificar(int busqueda,string guiaregistrada, int idticket, string usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_BuscarGuiasAltra", conexion);
                comando.Parameters.Add(new SqlParameter("@Guiaregistrada", guiaregistrada));
                comando.Parameters.Add(new SqlParameter("@IdTicket", idticket));
                comando.Parameters.Add(new SqlParameter("@Usuario", usuario));
                comando.Parameters.Add(new SqlParameter("@busqueda", busqueda));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataBuscarGuiasModificar(int ID, string guiaregistrada, string Compania, string Placa, string Carreta, string Ruc, string DniConductor, int importado,
                                                    string Serie, string Numero, string Gr, string CodProgram, string Ticket, string FhTicket, decimal PesoPuerto, string UNM,
                                                    decimal CantidadBase, string Observacion, int Ot, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_GuiasModificar", conexion);
                comando.Parameters.Add(new SqlParameter("@ID", ID));
                comando.Parameters.Add(new SqlParameter("@GuiaRegistrada", guiaregistrada));
                comando.Parameters.Add(new SqlParameter("@Compania", Compania));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                comando.Parameters.Add(new SqlParameter("@Ruc", Ruc));
                comando.Parameters.Add(new SqlParameter("@Dni", DniConductor));
                comando.Parameters.Add(new SqlParameter("@Importado", importado));
                comando.Parameters.Add(new SqlParameter("@Serie", Serie));
                comando.Parameters.Add(new SqlParameter("@Numero", Numero));
                comando.Parameters.Add(new SqlParameter("@Gr", Gr));
                comando.Parameters.Add(new SqlParameter("@CodProgram", CodProgram));
                comando.Parameters.Add(new SqlParameter("@Ticket", Ticket));
                comando.Parameters.Add(new SqlParameter("@FhTicket", FhTicket));
                comando.Parameters.Add(new SqlParameter("@PesoPuerto", PesoPuerto));
                comando.Parameters.Add(new SqlParameter("@UNM", UNM));
                comando.Parameters.Add(new SqlParameter("@CantidadBase", CantidadBase));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                comando.Parameters.Add(new SqlParameter("@Ot", Ot));
                comando.Parameters.Add(new SqlParameter("@usuario", Usuario));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetDataReporteAsistenciaVacaciones(string dato, string tipo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_GuiaValidarDato", conexion);
                comando.Parameters.Add(new SqlParameter("@Dato", dato));
                comando.Parameters.Add(new SqlParameter("@Tipo", tipo));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }

            catch
            {
                return new DataTable();
            }
        }


        #region GUIAS ELECTRONICAS

        public DataTable ListarTipoGuiaElectronica()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListaTiposGuiasElectronicas", conexion);
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

        // sem pasó por aquí

        public DataTable ReportesApp_ListarEmpresasGrupo()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarEmpresasGrupo", conexion);
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


        public DataTable ReportesApp_ListarClientes_GuiaElectronica(String NombreEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarClientes_GuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@Cliente", NombreEmpresa);
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


        public DataTable ReportesApp_ListarClientes_GuiaElectronica_Transportista(String NombreEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarClientes_GuiaElectronica_Transportista", conexion);
                cmd.Parameters.AddWithValue("@Cliente", NombreEmpresa);
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


        public DataSet ReportesApp_Listar_Departamentos_Provincias_Ciudades()
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_Departamentos_Provincias_Ciudades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "Departamento";
                    ds.Tables[1].TableName = "Provincia";
                    ds.Tables[2].TableName = "Ciudad";
                }


            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }

            finally { cmd.Connection.Close(); }
            return ds;
        }

        public DataTable ReportesApp_ListarCorreos_Master(int Persona,string DireccionDestino)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarCorreos_Master", conexion);
                cmd.Parameters.AddWithValue("@Persona", Persona);
                if (DireccionDestino.Length == 0)
                {
                    cmd.Parameters.AddWithValue("@DireccionDestino", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DireccionDestino", DireccionDestino);
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

        public Boolean ReportesApp_NuevoCorreo_Empresa_GuiasElectronicas(int Persona, string Correo, int TipoOperacion, string DireccionDestino, bool principal, int idCorreo)
        {

            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Nuevo_Editar_Anular_Correo_Empresa_GuiasElectronicas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Persona", Persona);
                comando.Parameters.AddWithValue("@Correo", Correo);
                comando.Parameters.AddWithValue("@TipoOperacion", TipoOperacion);
                if (DireccionDestino.Length == 0)
                {
                    comando.Parameters.AddWithValue("@DireccionDestino", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@DireccionDestino", DireccionDestino);
                }
      
                if (idCorreo == -1)
                {
                    comando.Parameters.AddWithValue("@idCorreo", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@idCorreo", idCorreo);
                }
                comando.Parameters.AddWithValue("@Principal", principal);

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


        public DataTable ReportesApp_ListarUnidadMedida_Sunat()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarUnidadMedida_Sunat", conexion);
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

        public DataTable ReportesApp_Informacion_Conductor_GuiaElectronica(int idConductor)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Informacion_Conductor_GuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@idConductor", idConductor);
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


        public DataTable ReportesApp_Listar_TipoDocumentoFiscal_GuiaElectronica()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_TipoDocumentoFiscal_GuiaElectronica", conexion);
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


        public DataTable ReportesApp_Listar_SerieGuiasElectronicas(string TipoGuia)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_SerieGuiasElectronicas", conexion);
                cmd.Parameters.AddWithValue("@TipoGuia", TipoGuia);
                cmd.Parameters.AddWithValue("@idUsuario", Utilitario.Instancia.SesionUsuario.idUsuario);
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

        public DataTable ReportesApp_ListarRuta_GuiaElectronica(string nombreRuta)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarRuta_GuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@NombreRuta", nombreRuta);
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

        public DataTable ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(int idEmpresa)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica", conexion);
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);


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

        public DataTable ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(int idEmpresa,int idRuta, int idRemitente,int idDestinatario)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica", conexion);
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                cmd.Parameters.AddWithValue("@idRuta", idRuta);
                if (idRemitente == 0)
                {
                    cmd.Parameters.AddWithValue("@idRemitente", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idRemitente", idRemitente);
                }
                if (idDestinatario == 0)
                {
                    cmd.Parameters.AddWithValue("@idDestinatario", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idDestinatario", idDestinatario);
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


        public DataTable ReportesApp_ListarOtsXProgramacion(int idProgramacion, int Anio, string TipoProgramacion, int idViaje)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarOtsXProgramacion", conexion);
                cmd.Parameters.AddWithValue("@idProgramacion", idProgramacion);
                cmd.Parameters.AddWithValue("@Anio", Anio);
                cmd.Parameters.AddWithValue("@TipoProgramacion", TipoProgramacion);
                if (idViaje == 0)
                {
                    cmd.Parameters.AddWithValue("@idViaje", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idViaje", idViaje);
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

        public DataTable ReportesApp_ListarInformacion_ClienteProgramacion(int idCliente)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarInformacion_ClienteProgramacion", conexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
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

        public DataTable ReportesApp_TipoServicioGuiaElectronica(string tipoGuia)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_TipoServicioGuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@TipoGuia", tipoGuia);
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



        #region Guia Remision Transportista
        // GUIA REMISION TRANSPORTISTA
        public Boolean ReportesApp_RegistrarGuiaElectronica(ref clsGRT entGuiaTransportista)
        {

            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_RegistrarGuiaElectronica_Transportista", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                comando.Parameters.AddWithValue("@Empresa", entGuiaTransportista.compania);
                comando.Parameters.AddWithValue("@idCliente", entGuiaTransportista.idcliente);
                comando.Parameters.AddWithValue("@IdOT", entGuiaTransportista.idOT);
                comando.Parameters.AddWithValue("@LineaOT", entGuiaTransportista.LineaOT);
                comando.Parameters.AddWithValue("@TipoGuia", entGuiaTransportista.TipoGuia);
                comando.Parameters.AddWithValue("@SerieGuia", entGuiaTransportista.entGRT_Generales_Serie_M);

                comando.Parameters.AddWithValue("@NumeroGuia", entGuiaTransportista.entGRT_Generales_Numero_M);
                comando.Parameters.AddWithValue("@idViaje", entGuiaTransportista.idviaje);
                comando.Parameters.AddWithValue("@Viaje", entGuiaTransportista.viaje);

                comando.Parameters.AddWithValue("@Cliente", entGuiaTransportista.Cliente);
                comando.Parameters.AddWithValue("@FechaTraslado", entGuiaTransportista.entGRT_Generales_FechaIncioTraslado_M);
                comando.Parameters.AddWithValue("@ObservacionGuia", entGuiaTransportista.entGRT_Generales_Observacion);
                comando.Parameters.AddWithValue("@CorreoPrincipal", entGuiaTransportista.entGRT_Remitente_Otorga_CorreoPrincial_M);
                comando.Parameters.AddWithValue("@idCorreoPrincipal", entGuiaTransportista.entGRT_Remitente_Otorga_idCorreoPrincial_M);
                if (entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario != null)
                {
                    comando.Parameters.AddWithValue("@xmlCorreosSecundarios", entGuiaTransportista.xml_entGRT_Remitente_Otorga_CorreoSecundario);
                }
                else
                {
                    comando.Parameters.AddWithValue("@xmlCorreosSecundarios", DBNull.Value);
                }
                comando.Parameters.AddWithValue("@idRemitente", entGuiaTransportista.idRemitente);
                comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Remitente", entGuiaTransportista.entGRT_Remitente_NumeroDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Remitente", entGuiaTransportista.entGRT_Remitente_TipoDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@RazonSocial_Remitente", entGuiaTransportista.entGRT_Remitente_RazonSocial_M);
                comando.Parameters.AddWithValue("@idDestinatario", entGuiaTransportista.idDestinatario);
                comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Destinatario", entGuiaTransportista.entGRT_Destinatario_NumeroDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Destinatario", entGuiaTransportista.entGRT_Destinatario_TipoDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@RazonSocial_Destinatario", entGuiaTransportista.entGRT_Destinatario_RazonSocial_M);

                if (entGuiaTransportista.TipoFleteTercero)
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Contratista", entGuiaTransportista.entGRT_Contratista_NumeroDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Contratista", entGuiaTransportista.entGRT_Contratista_TipoDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@RazonSocial_Contratista", entGuiaTransportista.entGRT_Contratista_RazonSocial);
                }
                else
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Contratista", DBNull.Value);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Contratista", DBNull.Value);
                    comando.Parameters.AddWithValue("@RazonSocial_Contratista", DBNull.Value);
                }

                if (entGuiaTransportista.TipoTransoporteSubcontratado)
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Subcontratado", entGuiaTransportista.entGRT_SubContratista_NumeroDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Subcontratado", entGuiaTransportista.entGRT_SubContratista_TipoDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@RazonSocial_Subcontratado", entGuiaTransportista.entGRT_SubContratista_RazonSocial);
                }
                else
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Subcontratado", DBNull.Value);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Subcontratado", DBNull.Value);
                    comando.Parameters.AddWithValue("@RazonSocial_Subcontratado", DBNull.Value);
                }
                if (entGuiaTransportista.xml_entGRT_DocumentosRelacion == null)
                {
                    comando.Parameters.AddWithValue("@xmlDocumentosRelacionados", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@xmlDocumentosRelacionados", entGuiaTransportista.xml_entGRT_DocumentosRelacion);
                }
               
                comando.Parameters.AddWithValue("@xmlIndicadorServicio", entGuiaTransportista.xml_entGRT_TipoServicio);
                comando.Parameters.AddWithValue("@PesoTotalCarga", entGuiaTransportista.entGTR_PesoBruto_PesoTotal_M);
                comando.Parameters.AddWithValue("@CodUnidadMedida", entGuiaTransportista.entGRT_PesoBruto_CodigoUnidadMedida_M);

                if (entGuiaTransportista.TipoTrasladoTotaldeBienes)
                {
                    comando.Parameters.AddWithValue("@xmlProductos", entGuiaTransportista.xml_entGRT_Productos_Bienes);
                    comando.Parameters.AddWithValue("@Descripcion_AdicionalPeso", entGuiaTransportista.entGRT_PesoBruto_DescripcionAdicional);
                }
                else
                {
                    comando.Parameters.AddWithValue("@xmlProductos", entGuiaTransportista.xml_entGRT_Productos_Bienes);
                    comando.Parameters.AddWithValue("@Descripcion_AdicionalPeso", DBNull.Value);
                }

                comando.Parameters.AddWithValue("@Ubigeo_Partida", entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M);
                comando.Parameters.AddWithValue("@DireccionCompleta_Partida", entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M);
                comando.Parameters.AddWithValue("@DireccionSecuenciaPartida", entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia);
                comando.Parameters.AddWithValue("@Ubigeo_Llegada", entGuiaTransportista.entGRT_PuntoDestino_Ubigeo_M);
                comando.Parameters.AddWithValue("@DireccionCompleta_Llegada", entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M);
                comando.Parameters.AddWithValue("@DireccionSecuenciaLlegada", entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia);
                comando.Parameters.AddWithValue("@idRuta", entGuiaTransportista.idRuta);

                if (entGuiaTransportista.TipoTrasladoProgramado)
                {
                    comando.Parameters.AddWithValue("@NumeroPlaca", DBNull.Value);
                    comando.Parameters.AddWithValue("@idPlaca", DBNull.Value);
                    comando.Parameters.AddWithValue("@xmlConductores", entGuiaTransportista.xml_entGTR_Conductor_M);
                    comando.Parameters.AddWithValue("@idConductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@NumeroDocumentoIdentidad_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@Licencia_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@Nombre_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@Apellido_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@idCarreta", DBNull.Value);
                    comando.Parameters.AddWithValue("@Carreta", DBNull.Value);
                    comando.Parameters.AddWithValue("@PlacaTarjetaCircula", DBNull.Value);
                    comando.Parameters.AddWithValue("@CarretaTarjetaCircula", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@NumeroPlaca", entGuiaTransportista.entGRT_Vehiculo_NumeroPlaca_M);
                    comando.Parameters.AddWithValue("@idPlaca", entGuiaTransportista.idtracto);
                    comando.Parameters.AddWithValue("@xmlConductores", DBNull.Value);
                    comando.Parameters.AddWithValue("@idConductor", entGuiaTransportista.idconductor);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Conductor", entGuiaTransportista.entConductor.entGRT_Conductor_TipoDocumentoIdentidad_M);
                    comando.Parameters.AddWithValue("@NumeroDocumentoIdentidad_Conductor", entGuiaTransportista.entConductor.entGRT_Conductor_NumeroDocumentoIdentidad_M);
                    comando.Parameters.AddWithValue("@Licencia_Conductor", entGuiaTransportista.entConductor.entGRT_Conductor_Licencia_M);
                    comando.Parameters.AddWithValue("@Nombre_Conductor", entGuiaTransportista.entConductor.entGRT_Conductor_Nombres_M);
                    comando.Parameters.AddWithValue("@Apellido_Conductor", entGuiaTransportista.entConductor.entGRT_Conductor_Apellidos_M);
                    comando.Parameters.AddWithValue("@idCarreta", entGuiaTransportista.idCarreta);
                    comando.Parameters.AddWithValue("@Carreta", entGuiaTransportista.carreta);
                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion == null)
                    {
                        comando.Parameters.AddWithValue("@PlacaTarjetaCircula", DBNull.Value);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@PlacaTarjetaCircula", entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacion);
                    }
                    if (entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta == null)
                    {
                        comando.Parameters.AddWithValue("@CarretaTarjetaCircula", DBNull.Value);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@CarretaTarjetaCircula", entGuiaTransportista.entGRT_Vehiculo_TarjetaCirculacionCarreta);
                    }


                }

                if (entGuiaTransportista.entGRT_GrupoInformacionAdicional_Titulo != null && entGuiaTransportista.entGRT_GrupoInformacionAdicional_Etiqueta != null && entGuiaTransportista.entGRT_GrupoInformacionAdicional_Valor != null)
                {
                    comando.Parameters.AddWithValue("@GrupoAdicional_Titulo", entGuiaTransportista.entGRT_GrupoInformacionAdicional_Titulo);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Etiqueta", entGuiaTransportista.entGRT_GrupoInformacionAdicional_Titulo);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Valor", entGuiaTransportista.entGRT_GrupoInformacionAdicional_Titulo);

                }
                else
                {
                    comando.Parameters.AddWithValue("@GrupoAdicional_Titulo", DBNull.Value);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Etiqueta", DBNull.Value);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Valor", DBNull.Value);
                }
                comando.Parameters.AddWithValue("@EstadoGuia", entGuiaTransportista.entGRT_Respuesta_EstadoGuardado);
                if (entGuiaTransportista.entGRT_Respuesta_EstadoSunat == null)
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", "PENDIENTE");
                }
                else
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", entGuiaTransportista.entGRT_Respuesta_EstadoSunat);
                }

                if(entGuiaTransportista.esConsolidado)
                {
                    comando.Parameters.AddWithValue("@lineaConsolidado", entGuiaTransportista.lineaConsolidado );
                }
                else
                {
                    comando.Parameters.AddWithValue("@lineaConsolidado", DBNull.Value);
                }
                
                comando.Parameters.AddWithValue("@Impeso", 1);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@idProgramacion", entGuiaTransportista.idProgramacion);
                comando.Parameters.AddWithValue("@NroTicketProgramacion", entGuiaTransportista.CodigoProgramacion);
                comando.Parameters.AddWithValue("@idTipoProgramacion", entGuiaTransportista.idTipoProgramacion);
                comando.Parameters.AddWithValue("@EstadoProgramacion", entGuiaTransportista.idEstadoProgramacion);
                comando.Parameters.AddWithValue("@AnioProgramacion", entGuiaTransportista.AnioProgramacion);
                comando.Parameters.AddWithValue("@TipoViaje", entGuiaTransportista.TipoViaje);
                comando.Parameters.AddWithValue("@TipoOperacion", entGuiaTransportista.TipoOperacion);
                comando.Parameters.AddWithValue("@idGuiaElectronica_Editar", entGuiaTransportista.idGuiaElectronica);
                comando.Parameters.AddWithValue("@GenerarCompletadoViaje", entGuiaTransportista.CompletadoViaje);

                
                SqlDataReader dr = comando.ExecuteReader();


                if (dr.Read())
                {

                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                    if (respuesta)
                    {
                        if (entGuiaTransportista.TipoOperacion == Utilitario.TipoOperacion.Registrar)
                        {
                            entGuiaTransportista.entGRT_Generales_Numero_M = Convert.ToInt32(dr["NumeroGuia"]);
                            entGuiaTransportista.viaje = Convert.ToString(dr["Viaje"]);
                            entGuiaTransportista.idviaje = Convert.ToInt32(dr["idViaje"]);
                            entGuiaTransportista.idGuiaElectronica = Convert.ToInt32(dr["idGuiaElectronica"]);
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { comando.Connection.Close(); }
            return respuesta;
        }




        //GUIA REMISION TRANSPORTISTA
        public Boolean GuardarRespuestaSunat(clsGRT entGuiaTransportista)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Registrar_RespuestaSunatGuiasElectronicas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEmpresaGrupo", entGuiaTransportista.compania);
                comando.Parameters.AddWithValue("@idCliente", entGuiaTransportista.idcliente);
                comando.Parameters.AddWithValue("@idOT", entGuiaTransportista.idOT);
                comando.Parameters.AddWithValue("@TipoGuia", entGuiaTransportista.TipoGuia);
                comando.Parameters.AddWithValue("@idGuiaElectronica", entGuiaTransportista.idGuiaElectronica);
                comando.Parameters.AddWithValue("@CodigoMensaje", entGuiaTransportista.entGRT_Respuesta_CodigoMensaje == null ? "" : entGuiaTransportista.entGRT_Respuesta_CodigoMensaje);
                comando.Parameters.AddWithValue("@MensajeResultado", entGuiaTransportista.entGRT_Respuesta_MensajeResultado == null ? "" : entGuiaTransportista.entGRT_Respuesta_MensajeResultado);
                comando.Parameters.AddWithValue("@ConsultaIndividualEstado", entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado == null ? "" : entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado);
                comando.Parameters.AddWithValue("@FechaGeneracion", entGuiaTransportista.entGRT_Respuesta_FechaGeneracion == null ? "" : entGuiaTransportista.entGRT_Respuesta_FechaGeneracion);
                comando.Parameters.AddWithValue("@FechaTransmision", entGuiaTransportista.entGRT_Respuesta_FechaTransmision == null ? "" : entGuiaTransportista.entGRT_Respuesta_FechaTransmision);
                comando.Parameters.AddWithValue("@FechaOtorgamiento", entGuiaTransportista.entGRT_Respuesta_FechaOtorgamiento == null ? "" : entGuiaTransportista.entGRT_Respuesta_FechaOtorgamiento);
                comando.Parameters.AddWithValue("@EstadoGuardado", entGuiaTransportista.entGRT_Respuesta_EstadoGuardado);
                if (entGuiaTransportista.entGRT_Respuesta_EstadoSunat == null)
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", "PENDIENTE");
                }
                else
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", entGuiaTransportista.entGRT_Respuesta_EstadoSunat == null ? "" : entGuiaTransportista.entGRT_Respuesta_EstadoSunat);
                }

                comando.Parameters.AddWithValue("@Xml_CDR", entGuiaTransportista.entGRT_Respuesta_Xml_CDR == null ? "" : entGuiaTransportista.entGRT_Respuesta_Xml_CDR);
                comando.Parameters.AddWithValue("@Fecha_CDR ", entGuiaTransportista.entGRT_Respuesta_Fecha_CDR == null ? "" : entGuiaTransportista.entGRT_Respuesta_Fecha_CDR);
                comando.Parameters.AddWithValue("@XML_Archivo ", entGuiaTransportista.entGRT_Respuesta_XML_Archivo == null ? "" : entGuiaTransportista.entGRT_Respuesta_XML_Archivo);
                comando.Parameters.AddWithValue("@CodigoHash", entGuiaTransportista.entGRT_Respuesta_CodigoHash == null ? "" : entGuiaTransportista.entGRT_Respuesta_CodigoHash);
                comando.Parameters.AddWithValue("@Serie", entGuiaTransportista.entGRT_Generales_Serie_M == null ? "" : entGuiaTransportista.entGRT_Generales_Serie_M);
                comando.Parameters.AddWithValue("@Numero", entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") == null ? "" : entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8"));
                comando.Parameters.AddWithValue("@URL_Sunat_PDF", entGuiaTransportista.entGRT_Respuesta_URL_GuiaSunat == null ? "" : entGuiaTransportista.entGRT_Respuesta_URL_GuiaSunat);

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

        #endregion


        #region Guia Remision Remitente
        //GUIA REMISION REMITENTE
        public Boolean ReportesApp_RegistrarGuiaElectronica(ref clsGRR entGuiaRemitente)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_RegistrarGuiaElectronica_Remitente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                comando.Parameters.AddWithValue("@Empresa", entGuiaRemitente.compania);
                comando.Parameters.AddWithValue("@idCliente", entGuiaRemitente.idcliente);
                comando.Parameters.AddWithValue("@IdOT", 0);
                comando.Parameters.AddWithValue("@TipoGuia", "R");
                comando.Parameters.AddWithValue("@SerieGuia", entGuiaRemitente.entGRR_Generales_Serie_M);
                comando.Parameters.AddWithValue("@NumeroGuia", entGuiaRemitente.entGRR_Generales_Numero_M);
                comando.Parameters.AddWithValue("@Cliente", entGuiaRemitente.entGRR_Destinatario_RazonSocial_M);
                comando.Parameters.AddWithValue("@FechaTraslado", entGuiaRemitente.entGRR_Generales_FechaIncioTraslado);
                comando.Parameters.AddWithValue("@CodModalidad", entGuiaRemitente.entGRR_Generales_Modalidad_M);
                comando.Parameters.AddWithValue("@CodMotivo", entGuiaRemitente.entGRR_Generales_CodigoMotivo_M);
                comando.Parameters.AddWithValue("@MotivoTraslado", entGuiaRemitente.entGRR_Generales_DescripcionMotivo);
                comando.Parameters.AddWithValue("@ObservacionGuia", entGuiaRemitente.entGRR_Generales_Observacion);
                comando.Parameters.AddWithValue("@CorreoPrincipal", entGuiaRemitente.entGRR_Remitente_Otorga_CorreoPrincial_M);
                comando.Parameters.AddWithValue("@idCorreoPrincipal", entGuiaRemitente.entGRR_Remitente_Otorga_idCorreoPrincial_M);
                
                if (entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario != null)
                { comando.Parameters.AddWithValue("@xmlCorreosSecundarios", entGuiaRemitente.xml_entGRR_Remitente_Otorga_CorreoSecundario); }
                else { comando.Parameters.AddWithValue("@xmlCorreosSecundarios", DBNull.Value); }

                comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Remitente", entGuiaRemitente.entGRR_Remitente_NumeroDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Remitente", entGuiaRemitente.entGRR_Remitente_TipoDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@RazonSocial_Remitente", entGuiaRemitente.entGRR_Remitente_RazonSocial_M);

                comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Destinatario", entGuiaRemitente.entGRR_Destinatario_NumeroDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Destinatario", entGuiaRemitente.entGRR_Destinatario_TipoDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@RazonSocial_Destinatario", entGuiaRemitente.entGRR_Destinatario_RazonSocial_M);

                if (entGuiaRemitente.ConTransportista)
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Trans", entGuiaRemitente.entGRR_Transportista_NumeroDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Trans", entGuiaRemitente.entGRR_Transportista_TipoDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@RazonSocial_Trans", entGuiaRemitente.entGRR_Transportista_RazonSocial);
                }
                else
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Trans", DBNull.Value);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Trans", DBNull.Value);
                    comando.Parameters.AddWithValue("@RazonSocial_Trans", DBNull.Value);
                }

                if (entGuiaRemitente.ConProveedor)
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Proveedor", entGuiaRemitente.entGRR_Proveedor_NumeroDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Proveedor", entGuiaRemitente.entGRR_Proveedor_TipoDocumentoIdentidad);
                    comando.Parameters.AddWithValue("@RazonSocial_Proveedor", entGuiaRemitente.entGRR_Proveedor_RazonSocial);
                }
                else
                {
                    comando.Parameters.AddWithValue("@NroDocumentoIdentidad_Proveedor", DBNull.Value);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Proveedor", DBNull.Value);
                    comando.Parameters.AddWithValue("@RazonSocial_Proveedor", DBNull.Value);
                }

                if (entGuiaRemitente.xml_entGRR_DocumentosRelacion == null)
                { comando.Parameters.AddWithValue("@xmlDocumentosRelacionados", DBNull.Value); }
                else { comando.Parameters.AddWithValue("@xmlDocumentosRelacionados", entGuiaRemitente.xml_entGRR_DocumentosRelacion); }

                if (entGuiaRemitente.xml_entGRR_TipoServicio != null)
                {
                    if (entGuiaRemitente.xml_entGRR_TipoServicio.Length > 0)
                    { comando.Parameters.AddWithValue("@xmlIndicadorServicio", entGuiaRemitente.xml_entGRR_TipoServicio); }
                    else { comando.Parameters.AddWithValue("@xmlIndicadorServicio", DBNull.Value); }
                }
                else { comando.Parameters.AddWithValue("@xmlIndicadorServicio", DBNull.Value); }

                comando.Parameters.AddWithValue("@PesoTotalCarga", entGuiaRemitente.entGRR_PesoBruto_PesoTotal_M);
                comando.Parameters.AddWithValue("@CodUnidadMedida", entGuiaRemitente.entGRR_PesoBruto_CodigoUnidadMedida_M);
                comando.Parameters.AddWithValue("@xmlProductos", entGuiaRemitente.xml_entGRR_Productos_Bienes);

                comando.Parameters.AddWithValue("@Ubigeo_Partida", entGuiaRemitente.entGRR_PuntoPartida_Ubigeo_M);
                comando.Parameters.AddWithValue("@DireccionCompleta_Partida", entGuiaRemitente.entGRR_PuntoPartida_DireccionCompleta_M);
                comando.Parameters.AddWithValue("@Ubigeo_Llegada", entGuiaRemitente.entGRR_PuntoDestino_Ubigeo_M);
                comando.Parameters.AddWithValue("@DireccionCompleta_Llegada", entGuiaRemitente.entGRR_PuntoDestino_DireccionCompleta_M);


                if (entGuiaRemitente.ConVehiculo)
                {
                    if (entGuiaRemitente.TipoTrasladoProgramado)
                    {
                        comando.Parameters.AddWithValue("@NumeroPlaca", DBNull.Value);
                        comando.Parameters.AddWithValue("@idPlaca", DBNull.Value);
                        comando.Parameters.AddWithValue("@NumeroCarreta", DBNull.Value);
                        comando.Parameters.AddWithValue("@idCarreta", DBNull.Value);
                        comando.Parameters.AddWithValue("@xmlConductores", entGuiaRemitente.xml_entGRR_Conductor_M);
                        comando.Parameters.AddWithValue("@idConductor", DBNull.Value);
                        comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Conductor", DBNull.Value);
                        comando.Parameters.AddWithValue("@NumeroDocumentoIdentidad_Conductor", DBNull.Value);
                        comando.Parameters.AddWithValue("@Licencia_Conductor", DBNull.Value);
                        comando.Parameters.AddWithValue("@Nombre_Conductor", DBNull.Value);
                        comando.Parameters.AddWithValue("@Apellido_Conductor", DBNull.Value);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@NumeroPlaca", entGuiaRemitente.entGRR_Vehiculo_NumeroPlaca_M);
                        comando.Parameters.AddWithValue("@idPlaca", entGuiaRemitente.idtracto);
                        comando.Parameters.AddWithValue("@NumeroCarreta", entGuiaRemitente.entGRR_Vehiculo_NumeroCarreta_M);
                        comando.Parameters.AddWithValue("@idCarreta", entGuiaRemitente.idcarreta);
                        comando.Parameters.AddWithValue("@xmlConductores", DBNull.Value);
                        comando.Parameters.AddWithValue("@idConductor", entGuiaRemitente.idconductor);
                        comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Conductor", entGuiaRemitente.entConductor.entGRR_Conductor_TipoDocumentoIdentidad_M);
                        comando.Parameters.AddWithValue("@NumeroDocumentoIdentidad_Conductor", entGuiaRemitente.entConductor.entGRR_Conductor_NumeroDocumentoIdentidad_M);
                        comando.Parameters.AddWithValue("@Licencia_Conductor", entGuiaRemitente.entConductor.entGRR_Conductor_Licencia_M);
                        comando.Parameters.AddWithValue("@Nombre_Conductor", entGuiaRemitente.entConductor.entGRR_Conductor_Nombres_M);
                        comando.Parameters.AddWithValue("@Apellido_Conductor", entGuiaRemitente.entConductor.entGRR_Conductor_Apellidos_M);
                    }
                }
                else
                {
                    comando.Parameters.AddWithValue("@NumeroPlaca", DBNull.Value);
                    comando.Parameters.AddWithValue("@idPlaca", DBNull.Value);
                    comando.Parameters.AddWithValue("@NumeroCarreta", DBNull.Value);
                    comando.Parameters.AddWithValue("@idCarreta", DBNull.Value);
                    comando.Parameters.AddWithValue("@xmlConductores", DBNull.Value);
                    comando.Parameters.AddWithValue("@idConductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@TipoDocumentoIdentidad_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@NumeroDocumentoIdentidad_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@Licencia_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@Nombre_Conductor", DBNull.Value);
                    comando.Parameters.AddWithValue("@Apellido_Conductor", DBNull.Value);
                }

                if (entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion == null)
                { comando.Parameters.AddWithValue("@PlacaTarjetaCircula", DBNull.Value); }
                else { comando.Parameters.AddWithValue("@PlacaTarjetaCircula", entGuiaRemitente.entGRR_Vehiculo_TarjetaCirculacion); }

                if (entGuiaRemitente.entGRR_Carreta_TarjetaCirculacion == null)
                { comando.Parameters.AddWithValue("@CarretaTarjetaCircula", DBNull.Value); }
                else { comando.Parameters.AddWithValue("@CarretaTarjetaCircula", entGuiaRemitente.entGRR_Carreta_TarjetaCirculacion); }

                if (entGuiaRemitente.entGRR_GrupoInformacionAdicional_Titulo != null && entGuiaRemitente.entGRR_GrupoInformacionAdicional_Etiqueta != null && entGuiaRemitente.entGRR_GrupoInformacionAdicional_Valor != null)
                {
                    comando.Parameters.AddWithValue("@GrupoAdicional_Titulo", entGuiaRemitente.entGRR_GrupoInformacionAdicional_Titulo);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Etiqueta", entGuiaRemitente.entGRR_GrupoInformacionAdicional_Titulo);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Valor", entGuiaRemitente.entGRR_GrupoInformacionAdicional_Titulo);
                }
                else
                {
                    comando.Parameters.AddWithValue("@GrupoAdicional_Titulo", DBNull.Value);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Etiqueta", DBNull.Value);
                    comando.Parameters.AddWithValue("@GrupoAdicional_Valor", DBNull.Value);
                }

                comando.Parameters.AddWithValue("@EstadoGuia", entGuiaRemitente.entGRR_Respuesta_EstadoGuardado);

                if (entGuiaRemitente.entGRR_Respuesta_EstadoSunat == null)
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", "PENDIENTE");
                }
                else
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", entGuiaRemitente.entGRR_Respuesta_EstadoSunat);
                }


                comando.Parameters.AddWithValue("@Impeso", 1);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@TipoOperacion", entGuiaRemitente.TipoOperacion);
                comando.Parameters.AddWithValue("@idGuiaElectronica_Editar", entGuiaRemitente.idGuiaElectronica);
                comando.Parameters.AddWithValue("@idRemitente", entGuiaRemitente.idRemitente);
                comando.Parameters.AddWithValue("@idDestinatario", entGuiaRemitente.idDestinatario);
                comando.Parameters.AddWithValue("@checkTercero", entGuiaRemitente.checkTercero);

                if (entGuiaRemitente.CodigoEstablecimientoOrigen == null)
                {
                    comando.Parameters.AddWithValue("@CodigoEstablecimientoOrigen", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@CodigoEstablecimientoOrigen", entGuiaRemitente.CodigoEstablecimientoOrigen);
                }

                if (entGuiaRemitente.CodigoEstablecimientoDestino == null)
                {
                    comando.Parameters.AddWithValue("@CodigoEstablecimientoDestino", DBNull.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@CodigoEstablecimientoDestino", entGuiaRemitente.CodigoEstablecimientoDestino);
                }




                SqlDataReader dr = comando.ExecuteReader();


                if (dr.Read())
                {

                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                    if (respuesta)
                    {
                        if (entGuiaRemitente.TipoOperacion == Utilitario.TipoOperacion.Registrar)
                        {
                            entGuiaRemitente.entGRR_Generales_Numero_M = Convert.ToInt32(dr["NumeroGuia"]);
                            entGuiaRemitente.idGuiaElectronica = Convert.ToInt32(dr["idGuiaElectronica"]);
                        }


                    }


                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { comando.Connection.Close(); }
            return respuesta;
        }



        // GUIA REMISION REMITENTE
        public Boolean GuardarRespuestaSunat(clsGRR entGuiaRemitente)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Registrar_RespuestaSunatGuiasElectronicas_Remitente", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEmpresaGrupo", entGuiaRemitente.compania);
                comando.Parameters.AddWithValue("@idCliente", entGuiaRemitente.idcliente);
                comando.Parameters.AddWithValue("@idOT", 0);
                comando.Parameters.AddWithValue("@TipoGuia", "R");
                comando.Parameters.AddWithValue("@idGuiaElectronica", entGuiaRemitente.idGuiaElectronica);
                comando.Parameters.AddWithValue("@CodigoMensaje", entGuiaRemitente.entGRR_Respuesta_CodigoMensaje == null ? "" : entGuiaRemitente.entGRR_Respuesta_CodigoMensaje);
                comando.Parameters.AddWithValue("@MensajeResultado", entGuiaRemitente.entGRR_Respuesta_MensajeResultado == null ? "" : entGuiaRemitente.entGRR_Respuesta_MensajeResultado);
                comando.Parameters.AddWithValue("@ConsultaIndividualEstado", entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado == null ? "" : entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado);
                comando.Parameters.AddWithValue("@FechaGeneracion", entGuiaRemitente.entGRR_Respuesta_FechaGeneracion == null ? "" : entGuiaRemitente.entGRR_Respuesta_FechaGeneracion);
                comando.Parameters.AddWithValue("@FechaTransmision", entGuiaRemitente.entGRR_Respuesta_FechaTransmision == null ? "" : entGuiaRemitente.entGRR_Respuesta_FechaTransmision);
                comando.Parameters.AddWithValue("@FechaOtorgamiento", entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento == null ? "" : entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento);
                comando.Parameters.AddWithValue("@EstadoGuardado", entGuiaRemitente.entGRR_Respuesta_EstadoGuardado);

                if (entGuiaRemitente.entGRR_Respuesta_EstadoSunat == null)
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", "PENDIENTE");
                }
                else
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", entGuiaRemitente.entGRR_Respuesta_EstadoSunat == null ? "" : entGuiaRemitente.entGRR_Respuesta_EstadoSunat);
                }

                comando.Parameters.AddWithValue("@Xml_CDR", entGuiaRemitente.entGRR_Respuesta_Xml_CDR == null ? "" : entGuiaRemitente.entGRR_Respuesta_Xml_CDR);
                comando.Parameters.AddWithValue("@Fecha_CDR ", entGuiaRemitente.entGRR_Respuesta_Fecha_CDR == null ? "" : entGuiaRemitente.entGRR_Respuesta_Fecha_CDR);
                comando.Parameters.AddWithValue("@XML_Archivo ", entGuiaRemitente.entGRR_Respuesta_XML_Archivo == null ? "" : entGuiaRemitente.entGRR_Respuesta_XML_Archivo);
                comando.Parameters.AddWithValue("@CodigoHash", entGuiaRemitente.entGRR_Respuesta_CodigoHash == null ? "" : entGuiaRemitente.entGRR_Respuesta_CodigoHash);
                comando.Parameters.AddWithValue("@Serie", entGuiaRemitente.entGRR_Generales_Serie_M == null ? "" : entGuiaRemitente.entGRR_Generales_Serie_M);
                comando.Parameters.AddWithValue("@Numero", entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8") == null ? "" : entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8"));
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@URL_Sunat_PDF", entGuiaRemitente.entGRR_Respuesta_URL_GuiaSunat == null ? "" : entGuiaRemitente.entGRR_Respuesta_URL_GuiaSunat);

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

        #endregion Guia Remision Transportista


        public DataTable ObtenerCorrelativoGuiaTransportistas(string empresa, string tipoGuia, string serie)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Obtener_UltimoCorrelativGuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@idEmpresaGrupo", empresa);
                cmd.Parameters.AddWithValue("@TipoGuia", tipoGuia);
                cmd.Parameters.AddWithValue("@SerieGuia", serie);
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

        public DataTable ObtenerFechaHoraServidorGuiaElectronica()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ObtenerFechaHoraServidor_GuiaElectronica", conexion);
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


        public DataTable ReportesApp_BuscarPlaca_GuiaElectronica(string placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_BuscarPlaca_GuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@Placa", placa);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_BuscarPlaca_TarjetaCirculacion(string Placa, int TipoDocumento)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_BuscarPlaca_TarjetaCirculacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(int idplaca)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@idPlaca", idplaca);
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

        public DataTable ReportesApp_ListarGuiasElectronicas(string tipoGuia, string fechaInicio, string fechaFin, string Serie, string Numero, bool todos, bool estadoAprobado, bool estadoRevertido, bool estadoRechazado, string viaje, string Cliente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarGuiasElectronicas", conexion);
                cmd.Parameters.AddWithValue("@tipoGuia", tipoGuia);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                if (Serie == "" || Serie == "Todos")
                {
                    cmd.Parameters.AddWithValue("@Serie", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Serie", Serie);
                }
                if (Numero == "")
                {
                    cmd.Parameters.AddWithValue("@Numero", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Numero", Numero);
                }
                
                if (todos)
                {
                    if (estadoAprobado)
                    {
                        cmd.Parameters.AddWithValue("@Aceptado", "ACEPTADO");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Aceptado", DBNull.Value);
                    }

                    if (estadoRevertido)
                    {
                        cmd.Parameters.AddWithValue("@Reversion", "REVERSION");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Reversion", DBNull.Value);
                    }

                    if (estadoRechazado)
                    {
                        cmd.Parameters.AddWithValue("@Rechazado", "RECHAZADO");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Rechazado", DBNull.Value);
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Aceptado", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Reversion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Rechazado", DBNull.Value);
                }
                if (viaje == "")
                {
                    cmd.Parameters.AddWithValue("@Viaje", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Viaje", viaje);
                }
                if (Cliente == "")
                {
                    cmd.Parameters.AddWithValue("@Cliente", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cliente", Cliente);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
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

        public Boolean ReportesApp_GuardarReversion(clsGRT entGuiaTransportista)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_GuardarReversion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEmpresaGrupo", entGuiaTransportista.compania);
                comando.Parameters.AddWithValue("@idCliente", entGuiaTransportista.idcliente);
                comando.Parameters.AddWithValue("@idOT", entGuiaTransportista.idOT);
                comando.Parameters.AddWithValue("@TipoGuia", entGuiaTransportista.TipoGuia);
                comando.Parameters.AddWithValue("@idGuiaElectronica", entGuiaTransportista.idGuiaElectronica);
                comando.Parameters.AddWithValue("@LineaOT", entGuiaTransportista.LineaOT);
                comando.Parameters.AddWithValue("@Serie", entGuiaTransportista.entGRT_Generales_Serie_M == null ? "" : entGuiaTransportista.entGRT_Generales_Serie_M);
                comando.Parameters.AddWithValue("@Numero", entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8") == null ? "" : entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8"));
                comando.Parameters.AddWithValue("@CodReversion", entGuiaTransportista.CodReversion);
                comando.Parameters.AddWithValue("@NumeroDocIdentidad", entGuiaTransportista.entGRT_Emisor_NumroDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@RazonSocial", entGuiaTransportista.entGRT_Emisor_RazonSocial_M);
                comando.Parameters.AddWithValue("@FechaEmision", entGuiaTransportista.entGRT_Generales_FechaEmision_M);
                comando.Parameters.AddWithValue("@TipoComprobante", entGuiaTransportista.entGRT_Emisor_TipoComprobante);
                comando.Parameters.AddWithValue("@MotivoReversion", entGuiaTransportista.MotivoReversion);
                comando.Parameters.AddWithValue("@MensajeResultado", entGuiaTransportista.entGRT_Respuesta_MensajeResultado == null ? "" : entGuiaTransportista.entGRT_Respuesta_MensajeResultado);
                comando.Parameters.AddWithValue("@CodigoHash", entGuiaTransportista.entGRT_Respuesta_CodigoHash == null ? "" : entGuiaTransportista.entGRT_Respuesta_CodigoHash);
                comando.Parameters.AddWithValue("@UsuarioReversion", Utilitario.Instancia.SesionUsuario.usuario == null ? "" : Utilitario.Instancia.SesionUsuario.usuario);



                if (entGuiaTransportista.entGRT_Respuesta_EstadoSunat == null)
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", "PENDIENTE");
                }
                else
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", entGuiaTransportista.entGRT_Respuesta_EstadoSunat == null ? "" : entGuiaTransportista.entGRT_Respuesta_EstadoSunat);
                }
                comando.Parameters.AddWithValue("@CodigoMensaje", entGuiaTransportista.entGRT_Respuesta_CodigoMensaje == null ? "" : entGuiaTransportista.entGRT_Respuesta_CodigoMensaje);
                comando.Parameters.AddWithValue("@ConsultaIndividualEstado", entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado == null ? "" : entGuiaTransportista.entGRT_Respuesta_ConsultaIndividualEstado);
                comando.Parameters.AddWithValue("@FechaGeneracion", entGuiaTransportista.entGRT_Respuesta_FechaGeneracion == null ? "" : entGuiaTransportista.entGRT_Respuesta_FechaGeneracion);
                comando.Parameters.AddWithValue("@FechaTransmision", entGuiaTransportista.entGRT_Respuesta_FechaTransmision == null ? "" : entGuiaTransportista.entGRT_Respuesta_FechaTransmision);
                comando.Parameters.AddWithValue("@FechaOtorgamiento", entGuiaTransportista.entGRT_Respuesta_FechaOtorgamiento == null ? "" : entGuiaTransportista.entGRT_Respuesta_FechaOtorgamiento);
                comando.Parameters.AddWithValue("@EstadoGuardado", "REVERSION");
                comando.Parameters.AddWithValue("@Xml_CDR", entGuiaTransportista.entGRT_Respuesta_Xml_CDR == null ? "" : entGuiaTransportista.entGRT_Respuesta_Xml_CDR);
                comando.Parameters.AddWithValue("@Fecha_CDR ", entGuiaTransportista.entGRT_Respuesta_Fecha_CDR == null ? "" : entGuiaTransportista.entGRT_Respuesta_Fecha_CDR);
                comando.Parameters.AddWithValue("@XML_Archivo ", entGuiaTransportista.entGRT_Respuesta_XML_Archivo == null ? "" : entGuiaTransportista.entGRT_Respuesta_XML_Archivo);
             





                SqlDataReader dr = comando.ExecuteReader();
                
                if (dr.Read())
                {
                    string x = Convert.ToString(dr["Mensaje"]);
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

        public DataTable ReportesApp_ListarUbigeo_GuiaElectronica(string ciudad)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarUbigeo_GuiaElectronica", conexion);

                cmd.Parameters.AddWithValue("@ciudad", ciudad);



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



        public DataTable ReportesApp_BuscarProducto_GuiaElectronica(string producto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_BuscarProducto_GuiaElectronica", conexion);
                cmd.Parameters.AddWithValue("@producto", producto);
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

        public DataTable ReportesApp_ListarMotivoTraslado_GuiaElectronica()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarMotivoTraslado_GuiaElectronica", conexion);
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

        public DataTable ReportesApp_ListarModalidadTransporte()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarModalidadTransporte", conexion);
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

        public DataTable ReportesApp_Listar_TipoDocumentoPersona_Guia_Electronica()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_TipoDocumentoPersona_Guia_Electronica", conexion);
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

        public bool ReportesApp_GuardarReversionRemitente(clsGRR entGuiaRemitente)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_GuardarReversion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEmpresaGrupo", entGuiaRemitente.compania);
                comando.Parameters.AddWithValue("@idCliente", entGuiaRemitente.idcliente);
                comando.Parameters.AddWithValue("@idOT", 0);
                comando.Parameters.AddWithValue("@LineaOT", 0);
                comando.Parameters.AddWithValue("@TipoGuia", entGuiaRemitente.TipoGuia);
                comando.Parameters.AddWithValue("@idGuiaElectronica", entGuiaRemitente.idGuiaElectronica);
                comando.Parameters.AddWithValue("@Serie", entGuiaRemitente.entGRR_Generales_Serie_M == null ? "" : entGuiaRemitente.entGRR_Generales_Serie_M);
                comando.Parameters.AddWithValue("@Numero", entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8") == null ? "" : entGuiaRemitente.entGRR_Generales_Numero_M.ToString("D8"));
                comando.Parameters.AddWithValue("@CodReversion", entGuiaRemitente.CodReversion);
                comando.Parameters.AddWithValue("@NumeroDocIdentidad", entGuiaRemitente.entGRR_Emisor_NumroDocumentoIdentidad_M);
                comando.Parameters.AddWithValue("@RazonSocial", entGuiaRemitente.entGRR_Emisor_RazonSocial_M);
                comando.Parameters.AddWithValue("@FechaEmision", entGuiaRemitente.entGRR_Generales_FechaEmision_M);
                comando.Parameters.AddWithValue("@TipoComprobante", entGuiaRemitente.entGRR_Emisor_TipoComprobante);
                comando.Parameters.AddWithValue("@MotivoReversion", entGuiaRemitente.MotivoReversion);
                comando.Parameters.AddWithValue("@MensajeResultado", entGuiaRemitente.entGRR_Respuesta_MensajeResultado == null ? "" : entGuiaRemitente.entGRR_Respuesta_MensajeResultado);
                comando.Parameters.AddWithValue("@CodigoHash", entGuiaRemitente.entGRR_Respuesta_CodigoHash == null ? "" : entGuiaRemitente.entGRR_Respuesta_CodigoHash);
                comando.Parameters.AddWithValue("@UsuarioReversion", Utilitario.Instancia.SesionUsuario.usuario == null ? "" : Utilitario.Instancia.SesionUsuario.usuario);


                //ACTUALIZO EN LA TRZABILIDAD DE LOS ESTADOS DE LA GUIA

                if (entGuiaRemitente.entGRR_Respuesta_EstadoSunat == null)
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", "PENDIENTE");
                }
                else
                {
                    comando.Parameters.AddWithValue("@EstadoSunat", entGuiaRemitente.entGRR_Respuesta_EstadoSunat == null ? "" : entGuiaRemitente.entGRR_Respuesta_EstadoSunat);
                }
                comando.Parameters.AddWithValue("@CodigoMensaje", entGuiaRemitente.entGRR_Respuesta_CodigoMensaje == null ? "" : entGuiaRemitente.entGRR_Respuesta_CodigoMensaje);
                comando.Parameters.AddWithValue("@ConsultaIndividualEstado", entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado == null ? "" : entGuiaRemitente.entGRR_Respuesta_ConsultaIndividualEstado);
                comando.Parameters.AddWithValue("@FechaGeneracion", entGuiaRemitente.entGRR_Respuesta_FechaGeneracion == null ? "" : entGuiaRemitente.entGRR_Respuesta_FechaGeneracion);
                comando.Parameters.AddWithValue("@FechaTransmision", entGuiaRemitente.entGRR_Respuesta_FechaTransmision == null ? "" : entGuiaRemitente.entGRR_Respuesta_FechaTransmision);
                comando.Parameters.AddWithValue("@FechaOtorgamiento", entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento == null ? "" : entGuiaRemitente.entGRR_Respuesta_FechaOtorgamiento);
                comando.Parameters.AddWithValue("@EstadoGuardado", "REVERSION");
                comando.Parameters.AddWithValue("@Xml_CDR", entGuiaRemitente.entGRR_Respuesta_Xml_CDR == null ? "" : entGuiaRemitente.entGRR_Respuesta_Xml_CDR);
                comando.Parameters.AddWithValue("@Fecha_CDR ", entGuiaRemitente.entGRR_Respuesta_Fecha_CDR == null ? "" : entGuiaRemitente.entGRR_Respuesta_Fecha_CDR);
                comando.Parameters.AddWithValue("@XML_Archivo ", entGuiaRemitente.entGRR_Respuesta_XML_Archivo == null ? "" : entGuiaRemitente.entGRR_Respuesta_XML_Archivo);
               

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



        public DataTable ReportesApp_ListarEstablecimientos_Anexos(string establecimiento)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarEstablecimientos_Anexos", conexion);
                cmd.Parameters.AddWithValue("@Establecimiento", establecimiento);
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

        #endregion 

    
        public DataTable ReportesApp_ListarHistorialRespuestaSunat_GuiasElectronicas(string empresa, int idCliente, int idOt, string TipoGuia, int idGuiaElectronica)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarHistorialRespuestaSunat_GuiasElectronicas", conexion);
                cmd.Parameters.AddWithValue("@idEmpresa", empresa);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@idOt", idOt);
                cmd.Parameters.AddWithValue("@TipoGuia", TipoGuia);
                cmd.Parameters.AddWithValue("@idGuiaElectronica", idGuiaElectronica);

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

        public bool Reportesapp_Operaciones_Reimprimir_GuiaElectronica(string empresa, int idCliente, int idOt, string TipoGuia, int idGuiaElectronica)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_Reimprimir_GuiaElectronica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEmpresaGrupo", empresa);
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                comando.Parameters.AddWithValue("@idOT", idOt);
                comando.Parameters.AddWithValue("@TipoGuia", TipoGuia);
                comando.Parameters.AddWithValue("@idGuiaElectronica", idGuiaElectronica);

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

        public bool ReportesApp_Operaciones_RegistrarAnularSerieGuiaElectronica(string tipoguia, string serie, string empresa, string descripcion, int accion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarAnularSerieGuiaElectronica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@TipoGuia", tipoguia);
                comando.Parameters.AddWithValue("@Serie", serie);
                comando.Parameters.AddWithValue("@EmpresaGrupo", empresa);
                comando.Parameters.AddWithValue("@Descripcion", descripcion);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@Accion",accion);

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

        public DataTable ReportesApp_Operaciones_ListarMaestroSeriesGuiasElectronicas(bool estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarMaestroSeriesGuiasElectronicas", conexion);
                cmd.Parameters.AddWithValue("@Estado", estado);
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

        public bool ReportesApp_ConfirmarAnulacionSunat(string empresa, int idCliente, int idOt, string TipoGuia, int idGuiaElectronica)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_ConfirmarAnulacionSunat", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                comando.Parameters.AddWithValue("@idOT", idOt);
                comando.Parameters.AddWithValue("@TipoGuia", TipoGuia);
                comando.Parameters.AddWithValue("@idGuiaElectronica", idGuiaElectronica);
                comando.Parameters.AddWithValue("@UsuarioAnula", Utilitario.Instancia.SesionUsuario.usuario);
               

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

        public DataTable ReportesApp_Listar_Series_Por_Establecimientos_Anexos()
        {

            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Listar_Series_Por_Establecimientos_Anexos", conexion);

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



        public DataTable ReportesApp_Listar_Establecimientos_Vinculados_Series(string serie, string tipoguia, string empresa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_Operaciones_Establecimientos_Vinculados_Series", conexion);
                cmd.Parameters.AddWithValue("@Empresa", empresa);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@TipoGuia", tipoguia);
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

        public bool ReportesApp_Actualizar_EstablecimientosAnexados_Serie(string serie, string tipoguia, string empresa, string codigo,string descripcion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_Actualizar_EstablecimientosAnexados_Serie", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Empresa", empresa);
                comando.Parameters.AddWithValue("@Serie", serie);
                comando.Parameters.AddWithValue("@TipoGuia", tipoguia);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Descripcion", descripcion);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);


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

        public bool ReportesApp_Operaciones_VincularSeriesPorUsuario(string empresa, string tipoguia, string serie, int idUsuario, string Nombre, int Accion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_VincularSeriesPorUsuario", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Empresa", empresa);
                comando.Parameters.AddWithValue("@Serie", serie);
                comando.Parameters.AddWithValue("@TipoGuia", tipoguia);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@Accion", Accion);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);


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

        public DataTable ReportesApp_Operaciones_ListarUsuariosVinculadosxSeries(string serie, string empresa, string tipoguia)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarUsuariosVinculadosxSeries", conexion);
                cmd.Parameters.AddWithValue("@Empresa", empresa);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@TipoGuia", tipoguia);
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




        public bool ReportespApp_Operaciones_PlacaTercero_GuiaElectronica_RegistrarEliminar(int idCliente, string nombreCLiente, string placa, string tarjetaCirculacion, string TipoVehiculo, int accion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarEliminar_PlacaTercero", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                comando.Parameters.AddWithValue("@NombreCLiente", nombreCLiente);
                comando.Parameters.AddWithValue("@Placa", placa);
                comando.Parameters.AddWithValue("@TarjetaCirculacion", tarjetaCirculacion);
                comando.Parameters.AddWithValue("@NombreTipoVehiculo", TipoVehiculo);
                comando.Parameters.AddWithValue("@Accion", accion);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);


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

        public  DataTable ReportesApp_Operaciones_ListarUnidadesTerceros_GuiaElectronica()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarUnidadesTerceros_GuiaElectronica", conexion);
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

        public  DataTable ReportesApp_Operaciones_ListarPlacasTercero_porCliente(int idCliente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarPlacasTercero_porCliente", conexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
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


        public bool ReportesAPP_RegistrarAnular_ConductoresTerceros(int idcliente, string nombres, string apellidos, int CodigotipoDocumento, string nombreTipoDocumento, string documento, string liencencia, int accion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarAnular_ConductoresTerceros_GuiaElectronica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idCliente", idcliente);
                comando.Parameters.AddWithValue("@nombres", nombres);
                comando.Parameters.AddWithValue("@apellidos", apellidos);
                comando.Parameters.AddWithValue("@CodigotipoDocumento", CodigotipoDocumento);
                comando.Parameters.AddWithValue("@nombreTipoDocumento", nombreTipoDocumento);
                comando.Parameters.AddWithValue("@documento", documento);
                comando.Parameters.AddWithValue("@licencia", liencencia); 
                comando.Parameters.AddWithValue("@Accion", accion);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);


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

        public DataTable ReportesApp_Operaciones_ListarConductoresTercerosGuiaElectronica(int idCliente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarConductoresTercerosGuiaElectronica", conexion);
                if (idCliente < 0)
                {
                    cmd.Parameters.AddWithValue("@idCliente", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
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

        public bool ReportesApp_Operaciones_RegistrarEmpresaxCodigoMTC(int idCliente, string nombreCliente, string numerMTC , int accion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarEmpresaxCodigoMTC", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                comando.Parameters.AddWithValue("@NombreEmpresa", nombreCliente);
                comando.Parameters.AddWithValue("@NumeroMTC", numerMTC);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@accion", accion);

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

        public DataTable ReportesApp_Operaciones_ListarEmpresaxCodigoMTC()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarEmpresaxCodigoMTC", conexion);
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

        public bool Reportesapp_Operaciones_EliminarRegistroGuiasElectronicas(string empresa, int idcliente, int idOt, string TipoGuia, int idGuiaElectronica,string Serie)
        {

            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_EliminarRegistroGuiasElectronicas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@idCliente", idcliente);
                comando.Parameters.AddWithValue("@idOT", idOt);
                comando.Parameters.AddWithValue("@TipoGuia", TipoGuia);
                comando.Parameters.AddWithValue("@idGuiaElectronica", idGuiaElectronica);
                comando.Parameters.AddWithValue("@SerieGuia", Serie);

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

        public DataTable ReportesApp_Operaciones_ListarMotivo()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ListarMotivo", conexion);
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

        public DataTable ReportesApp_Operaciones_ListarAsume()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ListarAsume", conexion);
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

        public DataTable ReportesApp_Operaciones_ListarEstado()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ListarEstado", conexion);
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

        public DataTable ReportesApp_Operaciones_ListarClientes(string Filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ListarClientes", conexion);
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

        public DataTable ReportesApp_Operaciones_ListarClientesReclamos(string Filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Logistica__ListarClientes_Reclamos", conexion);
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

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Insertar(int NroProgramacion, string Usuario, int idMotivo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_Insertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.Parameters.Add(new SqlParameter("@idMotivo", idMotivo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Listar(string NombreConductor, string FechaInicio, string FechaFin, int idEstado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NombreConductor", NombreConductor));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@idEstado", idEstado));
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

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ListarTodos(string NombreConductor, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ListarTodos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NombreConductor", NombreConductor));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
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

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro(int NroProgramacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
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

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ListarPreviaje(int NroProgramacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ListarPreviaje", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
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

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Modificar(int NroProgramacion, string Descripcion, string Factura, int idAsume, decimal Monto, string Moneda, int idEstado, string Comentarios, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_Modificar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Factura", Factura));
                comando.Parameters.Add(new SqlParameter("@idAsume", idAsume));
                comando.Parameters.Add(new SqlParameter("@Monto", Monto));
                comando.Parameters.Add(new SqlParameter("@Moneda", Moneda));
                comando.Parameters.Add(new SqlParameter("@idEstado", idEstado));
                comando.Parameters.Add(new SqlParameter("@Comentarios", Comentarios));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_InsertarFaltanteSinViaje(DateTime FechaIncidente, string Cliente, int idMotivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_InsertarFaltanteSinViaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaIncidente", FechaIncidente));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                comando.Parameters.Add(new SqlParameter("@idMotivo", idMotivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Cerrar(int NroProgramacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_Cerrar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }
        //NUEVO

        /*
        public bool Reportesapp_Operaciones_Registrar_Editar_Eliminar_Tolvas(clsPreviajeTolvas tolvas)
        {

            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {
            

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("Reportesapp_Operaciones_Registrar_Editar_Eliminar_PreviajesTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdPreviaje", tolvas.idPreviaje);
                comando.Parameters.AddWithValue("@AnioPreviaje", tolvas.anioPreviaje);
                comando.Parameters.AddWithValue("@Item", tolvas.item);
                comando.Parameters.AddWithValue("@CodigoPreviaje", tolvas.codigoPreviaje);
                comando.Parameters.AddWithValue("@idOT", tolvas.idOT);
                comando.Parameters.AddWithValue("@Remitente", tolvas.remitente);
                comando.Parameters.AddWithValue("@IdRemitente", tolvas.idRemitente);
                comando.Parameters.AddWithValue("@DireccionPartida", tolvas.direccionPartida);
                comando.Parameters.AddWithValue("@Destinatario", tolvas.destinatario);
                comando.Parameters.AddWithValue("@IdDestinatario", tolvas.idDestinatario);
                comando.Parameters.AddWithValue("@DireccionDestino", tolvas.direccionDestino);
                comando.Parameters.AddWithValue("@Cliente", tolvas.cliente);
                comando.Parameters.AddWithValue("@IdCliente", tolvas.idCliente);
                comando.Parameters.AddWithValue("@Producto", tolvas.producto);
                comando.Parameters.AddWithValue("@IdProducto", tolvas.idProducto);
                comando.Parameters.AddWithValue("@Ruta", tolvas.ruta);
                comando.Parameters.AddWithValue("@IdRuta", tolvas.idRuta);


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
        */

        public bool ReportesaApp_Operaciones_MaestroClienteRuta_GuiaElectronica(string idCliente, string Cliente,string idRemitente,string Remitente, string idDestinatario, string Destinatario, string idRuta, string Ruta, string DireccionPartida, string DirecconDestino, string ubigeoPartida, string ubigeoDestino,int SecuenciaOrigen, int SecuenciaDestino, int tipoOperacion)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesaApp_Operaciones_Registrar_Maestro_ClienteDestinoRuta_GuiaElectronica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idCliente", Convert.ToInt32(idCliente));
                comando.Parameters.AddWithValue("@Cliente", Cliente);
                comando.Parameters.AddWithValue("@idRemitente", Convert.ToInt32(idRemitente));
                comando.Parameters.AddWithValue("@Remitente", Remitente);
                comando.Parameters.AddWithValue("@idDestinatario", Convert.ToInt32(idDestinatario));
                comando.Parameters.AddWithValue("@Destinatario", Destinatario);
                comando.Parameters.AddWithValue("@idRuta", Convert.ToInt32(idRuta));
                comando.Parameters.AddWithValue("@Ruta", Ruta);
                comando.Parameters.AddWithValue("@DireccionPartida", DireccionPartida);
                comando.Parameters.AddWithValue("@DirecconDestino", DirecconDestino);
                comando.Parameters.AddWithValue("@ubigeoPartida", ubigeoPartida);
                comando.Parameters.AddWithValue("@ubigeoDestino", ubigeoDestino);
                comando.Parameters.AddWithValue("@SecuenciaOrigen", SecuenciaOrigen);
                comando.Parameters.AddWithValue("@SecuenciaDestino", SecuenciaDestino);
                comando.Parameters.AddWithValue("@TipoOperacion", tipoOperacion);


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

        public DataTable ReportesApp_Operaciones_ListarMaestroClienteDestinatarioRuta(string remitente, string ruta)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarMaestroClienteDestinatarioRuta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                if (remitente == "")
                {
                    cmd.Parameters.Add(new SqlParameter("@Remitente", DBNull.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@Remitente", remitente));
                }

                if (ruta == "")
                {
                    cmd.Parameters.Add(new SqlParameter("@Ruta", DBNull.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@Ruta", ruta));
                }
                
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

        public DataTable ReportesApp_Operaciones_CompletarDestinatario_Direcciones(int idcliente, int idruta)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_CompletarDestinatario_Direcciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@idcliente", idcliente);
                cmd.Parameters.AddWithValue("@idruta", idruta);

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

        public DataTable ReportesApp_ListarClientesDestinatario_GuiaElectronica(string cliente,int idRuta)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarClientesDestinatario_GuiaElectronica", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@Cliente", cliente);
                cmd.Parameters.AddWithValue("@idRuta", idRuta);
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

        public bool ReportesApp_Operaciones_DesvincularViaje_Previaje_GuiaElectronica(string empresa, int idCliente, int idOt, string TipoGuia,ref int idGuiaElectronica, string NroTicket, ref int idViaje,ref string Viaje, int idProgramacion, int anioProgramacion,ref string SerieGuia,ref string NumeroGuia, int LineaOT, string Peso)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_DesvincularViaje_Previaje_GuiaElectronica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                comando.Parameters.AddWithValue("@idOT", idOt);
                comando.Parameters.AddWithValue("@LineaOT", LineaOT);
                comando.Parameters.AddWithValue("@TipoGuia", TipoGuia);
                comando.Parameters.AddWithValue("@idGuiaElectronica", idGuiaElectronica);
                comando.Parameters.AddWithValue("@NuevoNroTicket", NroTicket);
                comando.Parameters.AddWithValue("@idViaje", idViaje);
                comando.Parameters.AddWithValue("@Viaje", Viaje);
                comando.Parameters.AddWithValue("@idProgramacion", idProgramacion);
                comando.Parameters.AddWithValue("@anioProgramacion", anioProgramacion);
                comando.Parameters.AddWithValue("@Serieguia", SerieGuia);
                comando.Parameters.AddWithValue("@NumeroGuia", NumeroGuia);
                comando.Parameters.AddWithValue("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario);
                comando.Parameters.AddWithValue("@Peso", Peso);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                    if (respuesta)
                    {
                            SerieGuia = Convert.ToString(dr["SerieGuia"]);
                            NumeroGuia = Convert.ToString(dr["NumeroGuia"]);
                            Viaje = Convert.ToString(dr["Viaje"]);
                            idViaje = Convert.ToInt32(dr["idViaje"]);
                            idGuiaElectronica = Convert.ToInt32(dr["idGuiaElectronica"]);
                    }

                }

            }
            catch (Exception ex)
            {
                Utilitario.Instancia.Advertencia = ex.Message;
            }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_BuscarProgramacionPreviajeLibre(int operacion, string fechaInicio, string fechaFin, string previaje)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_BuscarProgramacionPreviajeLibre", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@idProgramacion", operacion);
                if (fechaInicio == "")
                {
                    cmd.Parameters.AddWithValue("@fechaInicio",DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaFin", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                }

                if (previaje == "")
                {
                    cmd.Parameters.AddWithValue("@previaje", DBNull.Value);
                }
                else 
                {
                    cmd.Parameters.AddWithValue("@previaje", previaje);
                }
               

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


        public bool ReportesApp_Operaciones_RegistrarGuiaDeEvento_Electronica(clsGRT entGuiaTransportista)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarGuiaDeEvento_Electronica", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idOT", entGuiaTransportista.idOT);
          
                comando.Parameters.AddWithValue("@TipoEvento", entGuiaTransportista.TipoEvento);
                comando.Parameters.AddWithValue("@TipoGuiaTrans", entGuiaTransportista.TipoGuia);
                comando.Parameters.AddWithValue("@SerieGuiaTrans", entGuiaTransportista.entGRT_Generales_Serie_M);
                comando.Parameters.AddWithValue("@NumeroGuiaTrans", entGuiaTransportista.entGRT_Generales_Numero_M.ToString("D8"));
                comando.Parameters.AddWithValue("@RemitenteTrans", entGuiaTransportista.entGRT_Remitente_RazonSocial_M);
                comando.Parameters.AddWithValue("@idRemitenteTrans", entGuiaTransportista.idcliente);
                comando.Parameters.AddWithValue("@DestinatarioTrans", entGuiaTransportista.entGRT_Destinatario_RazonSocial_M);
                comando.Parameters.AddWithValue("@idDestinatarioTrans", entGuiaTransportista.idDestinatario);
                comando.Parameters.AddWithValue("@DireccionPartida", entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M);
                comando.Parameters.AddWithValue("@SecuenciaPartida", entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia);
                comando.Parameters.AddWithValue("@DireccionDestino", entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M);
                comando.Parameters.AddWithValue("@SecuenciaDestino", entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia);
                comando.Parameters.AddWithValue("@xmlConductores",entGuiaTransportista.xml_entGTR_Conductor_M);
                comando.Parameters.AddWithValue("@NroTicketProgramacion", entGuiaTransportista.CodigoProgramacion);
                comando.Parameters.AddWithValue("@idViaje", entGuiaTransportista.idviaje);
                comando.Parameters.AddWithValue("@Viaje", entGuiaTransportista.viaje);
                comando.Parameters.AddWithValue("@LineaOT", entGuiaTransportista.LineaOT);
                comando.Parameters.AddWithValue("@idRuta", entGuiaTransportista.idRuta);
                comando.Parameters.AddWithValue("@Ruta", entGuiaTransportista.ruta);
                comando.Parameters.AddWithValue("@UbigeoPartida", entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M);
                comando.Parameters.AddWithValue("@UbigeoDestino", entGuiaTransportista.entGRT_PuntoPartida_Ubigeo_M);
                comando.Parameters.AddWithValue("@GuiaEventoTransportista", entGuiaTransportista.GuiaEventoTransportista);
                comando.Parameters.AddWithValue("@GuiaEventoRemitente", entGuiaTransportista.GuiaEventoRemitente == null ? "" : entGuiaTransportista.GuiaEventoRemitente);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);

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

        public DataTable ReportesaApp_Operaciones_ListarGuiasEvento(string Serie, string Numero)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesaApp_Operaciones_ListarGuiasEvento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
               
                if (Serie == "")
                {
                    cmd.Parameters.AddWithValue("@Serie", DBNull.Value);
                 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Serie", Serie);
               
                }

                if (Numero == "")
                {
                    cmd.Parameters.AddWithValue("@Numero", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Numero", Numero);
                }


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

        public Boolean ReportesApp_Operaciones_Previajes_Tolvas_Insertar(ref clsPreviajeTolvas entPreviajeTolvas, string Usuario)
        {
            try
            {
                bool respuesta = false; ;
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_Insertar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdOT", entPreviajeTolvas.IdOT));
                comando.Parameters.Add(new SqlParameter("@Tarifa", entPreviajeTolvas.Tarifa));
                comando.Parameters.Add(new SqlParameter("@idCliente", entPreviajeTolvas.idClinte));
                comando.Parameters.Add(new SqlParameter("@Cliente", entPreviajeTolvas.Cliente));
                comando.Parameters.Add(new SqlParameter("@idRemitente", entPreviajeTolvas.idRemitente));
                comando.Parameters.Add(new SqlParameter("@Remitente", entPreviajeTolvas.Remitente));
                comando.Parameters.Add(new SqlParameter("@idPartida", entPreviajeTolvas.idPartida));
                comando.Parameters.Add(new SqlParameter("@DireccionPartida", entPreviajeTolvas.DireccionPartida));
                comando.Parameters.Add(new SqlParameter("@idDestinatario", entPreviajeTolvas.idDestinatario));
                comando.Parameters.Add(new SqlParameter("@Destinatario", entPreviajeTolvas.Destinatario));
                comando.Parameters.Add(new SqlParameter("@idDestino", entPreviajeTolvas.idDestino));
                comando.Parameters.Add(new SqlParameter("@DireccionDestino", entPreviajeTolvas.DireccionDestino));
                comando.Parameters.Add(new SqlParameter("@FechaProgramacion", entPreviajeTolvas.FechaProgramacion));
                comando.Parameters.Add(new SqlParameter("@idProducto", entPreviajeTolvas.idProducto));
                comando.Parameters.Add(new SqlParameter("@Producto", entPreviajeTolvas.Producto));
                comando.Parameters.Add(new SqlParameter("@Ruta", entPreviajeTolvas.Ruta));
                comando.Parameters.Add(new SqlParameter("@idRuta", entPreviajeTolvas.idRuta));
                comando.Parameters.Add(new SqlParameter("@xmlPlacaConductor", entPreviajeTolvas.xmlPlacaConductor));
                comando.Parameters.Add(new SqlParameter("@UMUso", entPreviajeTolvas.UMUso));
                comando.Parameters.Add(new SqlParameter("@Tiempo", entPreviajeTolvas.Tiempo));
                comando.Parameters.Add(new SqlParameter("@Distancia", entPreviajeTolvas.Distancia));
                comando.Parameters.Add(new SqlParameter("@TipoProceso", entPreviajeTolvas.TipoProceso));
                comando.Parameters.Add(new SqlParameter("@Motonave", entPreviajeTolvas.Motonave));
                comando.Parameters.Add(new SqlParameter("@Tonelaje", entPreviajeTolvas.Tonelaje));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                conexion.Open();
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read()) { respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["exito"]), ref Utilitario.Instancia.Advertencia); }

                if (respuesta)
                {
                    if (entPreviajeTolvas.TipoOperacion == Utilitario.TipoOperacion.Registrar)
                    {
                        entPreviajeTolvas.idPreviajeTolvas = Convert.ToInt32(dr["idPreviajeTolvas"]);
                        entPreviajeTolvas.anio = Convert.ToInt32(dr["Anio"]);
                        entPreviajeTolvas.Estado = Convert.ToString(dr["Estado"]);
                        entPreviajeTolvas.NroTicket = Convert.ToString(dr["NroTicket"]);
                    }
                }

                return respuesta;
            }
            catch { return false; }
        }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_ListarOperaciones()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_ListarOperaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch { return new DataTable(); }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_ImportarReporteSumarizado(int NroTicket, string FechaIni, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_ImportarReporteSumarizado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@Fechainicio", FechaIni));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader(); 
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_Listar(string CodPreviaje, string Remitente, string fechaInicio, string fechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodPreviaje", CodPreviaje));
                cmd.Parameters.Add(new SqlParameter("@Remitente", Remitente));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
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

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_SeleccionarPreviaje(int idPreviajeTolvas, int Anio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_SeleccionarPreviaje", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idPreviajeTolvas", idPreviajeTolvas));
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
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

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_CerrarOperacion(int idPreviajeTolvas, int Anio, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_CerrarOperacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idPreviajeTolvas", idPreviajeTolvas));
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
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

        public Boolean ReportesApp_Operaciones_Previajes_Tolvas_Modificar(int idPreviajeTolvas, int Anio, ref clsPreviajeTolvas entPreviajeTolvas, string Usuario)
        {

            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {
               
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                

                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_Modificar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idPreviajeTolvas", idPreviajeTolvas));
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.Parameters.Add(new SqlParameter("@idOT", entPreviajeTolvas.IdOT));
                comando.Parameters.Add(new SqlParameter("@FechaProgramacion", entPreviajeTolvas.FechaProgramacion));
                comando.Parameters.Add(new SqlParameter("@xmlPlacaConductor", Utilitario.Instancia.QuitarTildes(entPreviajeTolvas.xmlPlacaConductor)));
                comando.Parameters.Add(new SqlParameter("@idCliente", entPreviajeTolvas.idClinte));
                comando.Parameters.Add(new SqlParameter("@Cliente", entPreviajeTolvas.Cliente));
                comando.Parameters.Add(new SqlParameter("@idRemitente", entPreviajeTolvas.idRemitente));
                comando.Parameters.Add(new SqlParameter("@Remitente", entPreviajeTolvas.Remitente));

                comando.Parameters.Add(new SqlParameter("@idDestinatario", entPreviajeTolvas.idDestinatario));
                comando.Parameters.Add(new SqlParameter("@Destinatario", entPreviajeTolvas.Destinatario));

                comando.Parameters.Add(new SqlParameter("@idPartida", entPreviajeTolvas.idPartida));
                comando.Parameters.Add(new SqlParameter("@DireccionPartida", entPreviajeTolvas.DireccionPartida));
                comando.Parameters.Add(new SqlParameter("@idDestino", entPreviajeTolvas.idDestino));
                comando.Parameters.Add(new SqlParameter("@DireccionDestino", entPreviajeTolvas.DireccionDestino));
                comando.Parameters.Add(new SqlParameter("@idProducto", entPreviajeTolvas.idProducto));
                comando.Parameters.Add(new SqlParameter("@Producto", entPreviajeTolvas.Producto));
                comando.Parameters.Add(new SqlParameter("@idRuta", entPreviajeTolvas.idRuta));
                comando.Parameters.Add(new SqlParameter("@Ruta", entPreviajeTolvas.Ruta));
                comando.Parameters.Add(new SqlParameter("@UMUso", entPreviajeTolvas.UMUso));
                comando.Parameters.Add(new SqlParameter("@Distancia", entPreviajeTolvas.Distancia));
                comando.Parameters.Add(new SqlParameter("@Tiempo", entPreviajeTolvas.Tiempo));
                comando.Parameters.Add(new SqlParameter("@Tarifa", entPreviajeTolvas.Tarifa));
                comando.Parameters.Add(new SqlParameter("@Tonelaje", entPreviajeTolvas.Tonelaje));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));


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

        public bool ReportesApp_OperacionesVerificarPermisoTransmision(string UsuarioModulo)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_OperacionesVerificarPermisoTransmision", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Usuario", UsuarioModulo);
     
          

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

        public bool ReportesApp_Operaciones_RegistrarTransmision(int idpersona, string transmision)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarTransmision", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idpersona", idpersona);
                comando.Parameters.AddWithValue("@transmision", transmision);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);


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

        public DataTable ReportesApp_Operaciones_ListarViajesTolvas(string NroTicket, int idPreviajeTolvas, int anio, string placa, string conductor, int anulados)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarViajesTolvas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", Convert.ToInt32(NroTicket)));
                cmd.Parameters.Add(new SqlParameter("@idPreviajeTolvas", idPreviajeTolvas));
                cmd.Parameters.Add(new SqlParameter("@anio", Convert.ToInt32(anio)));
                
                if (placa.Length == 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@placa", DBNull.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@placa", placa));
                }
                if (conductor.Length == 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@conductor", DBNull.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@conductor", conductor));
                }
                cmd.Parameters.Add(new SqlParameter("@anulados", anulados));

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

        public bool ReportesApp_RegistrarPesoNroTicketClienteTolvas(string Operacion,string pesoCliente, string NroTicketPeso,int idViaje,string Serie,string Numero)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_RegistrarPesoNroTicketClienteTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@pesoCliente", pesoCliente);
                comando.Parameters.AddWithValue("@NroTicketPeso", NroTicketPeso);
                comando.Parameters.AddWithValue("@idViaje", idViaje);
                comando.Parameters.AddWithValue("@Operacion", Operacion);
                comando.Parameters.AddWithValue("@Serie", Serie);
                comando.Parameters.AddWithValue("@Numero", Numero);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);


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

        public bool ReportesApp_GenerarViajesTolvas_GuiasElectronicas(string xmlPreviajes)
        {

            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_GenerarViajesTolvas_GuiasElectronicas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@xmlPreviajes", xmlPreviajes);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
                comando.CommandTimeout = 0;
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

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_BuscarFaltanteSinViaje(int NroProgramacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_BuscarFaltanteSinViaje", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
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

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ModificarSinViaje(int NroProgramacion, DateTime FechaIncidente, string Cliente, int idMotivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Faltantes_ModificarSinViaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@FechaIncidente", FechaIncidente));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                comando.Parameters.Add(new SqlParameter("@idMotivo", idMotivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public bool ReportesApp_AnularViajeTolvas(int idPreviajeTolvas, int anio, int idViaje, string MotivoAnula)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_AnularViajeTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idPreviajeTolvas", idPreviajeTolvas);
                comando.Parameters.AddWithValue("@anio", anio);
                comando.Parameters.AddWithValue("@idViaje", idViaje);
                comando.Parameters.AddWithValue("@MotivoAnula", MotivoAnula);
                comando.Parameters.AddWithValue("@UsuarioAnula", Utilitario.Instancia.SesionUsuario.usuario);
                
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

        public bool ReportesApp_ActualizarDatosGuiaSalaverry(string tipo, string ticket, string dato)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_ActualizarDatosGuiaSalaverry", conexion);
                comando.CommandTimeout = 300;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.Parameters.AddWithValue("@ticket", ticket);
                comando.Parameters.AddWithValue("@dato", dato);


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

        public DataTable ReportesApp_Operaciones_ConsultarEstadoGuiaReporteador(string Serie, int Numero)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ConsultarEstadoGuiaReporteador", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Serie", Serie));
                cmd.Parameters.Add(new SqlParameter("@Numero", Numero.ToString("D8")));
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

        public  bool ReportesApp_Operaciones_ImportarGuiasTerceros(string cadena, string usuario,int idProgramacion, int anioProgamacion)
        {
            SqlCommand cmd = null;
            bool respuesta = false;
           
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ImportarGuiasTerceros_Tolvas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@DataXML", cadena));
                cmd.Parameters.Add(new SqlParameter("@idProgramacion", idProgramacion));
                cmd.Parameters.Add(new SqlParameter("@anioProgramacion", anioProgamacion));
                cmd.Parameters.Add(new SqlParameter("@UsuarioCrea", usuario));
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

        public bool ReportesApp_Operacones_VincularGuiasFisicas_Consolidado(clsGRT openGenerarGuia, string SerieTran, string NumeroTran, string SerieRem, string NumeroRem)
        {
            SqlCommand cmd = null;
            bool respuesta = false;

            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operacones_VincularGuiasFisicas_Consolidado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idOT", openGenerarGuia.idOT));
                cmd.Parameters.Add(new SqlParameter("@lineaConsolidado", openGenerarGuia.lineaConsolidado));
                cmd.Parameters.Add(new SqlParameter("@Viaje", openGenerarGuia.viaje));
                cmd.Parameters.Add(new SqlParameter("@idRuta", openGenerarGuia.idRuta));
                cmd.Parameters.Add(new SqlParameter("@CodigoProgramacion", openGenerarGuia.CodigoProgramacion));
                cmd.Parameters.Add(new SqlParameter("@idProgramacion", openGenerarGuia.idProgramacion));
                cmd.Parameters.Add(new SqlParameter("@AnioProgramacion", openGenerarGuia.AnioProgramacion));
                cmd.Parameters.Add(new SqlParameter("@idConductor", openGenerarGuia.idconductor));
                cmd.Parameters.Add(new SqlParameter("@SerieTran", SerieTran));
                cmd.Parameters.Add(new SqlParameter("@NumeroTran", NumeroTran));
                cmd.Parameters.Add(new SqlParameter("@SerieRem", SerieRem));
                cmd.Parameters.Add(new SqlParameter("@NumeroRem", NumeroRem));
                cmd.Parameters.Add(new SqlParameter("@LineaOT", openGenerarGuia.LineaOT));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));

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

        public DataTable Reportesapp_Operaciones_GenerarReporteGuiasRetorno(string fechaInicio, string fechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("Reportesapp_Operaciones_GenerarReporteGuiasRetorno", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
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

        public bool ReportesApp_Operaciones_GuardarRutasDesbloqueadas(string xmlConductores, string xmlRutas)
        {
            SqlCommand cmd = null;
            bool respuesta = false;

            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_GuardarRutasDesbloqueadas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@xmlConductores", xmlConductores));
                cmd.Parameters.Add(new SqlParameter("@xmlRutas", xmlRutas));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));

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

        public DataTable Reportesapp_Operaciones_ListarRutasDesbloqueadasXConductor()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("Reportesapp_Operaciones_ListarRutasDesbloqueadasXConductor", conexion);
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

        public bool ReportesApp_Operaciones_EliminarRutaXConductor(int idConductor, int idRuta)
        {
            SqlCommand cmd = null;
            bool respuesta = false;

            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_EliminarRutaXConductor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                cmd.Parameters.Add(new SqlParameter("@idRuta", idRuta));


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

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarTipoGasto()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarTipoGasto", conexion);
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

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarOperaciones()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarOperaciones", conexion);
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

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarGastoDetalle(int idTipoGasto, string Descripcion, decimal Gasto, int idTiempo, int Adicional, int idPeaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_InsertarGastoDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idTipoGasto", idTipoGasto));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Gasto", Gasto));
                comando.Parameters.Add(new SqlParameter("@idTiempo", idTiempo));
                comando.Parameters.Add(new SqlParameter("@Adicional", Adicional));
                comando.Parameters.Add(new SqlParameter("@idPeaje", idPeaje));
                comando.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoDetalle(int IdRuta, int IdOperacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarGastoDetalle", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                cmd.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarGastoDetalle(int idGastoxRutaD)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_EliminarGastoDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idGastoxRutaD", idGastoxRutaD));
                comando.Parameters.Add(new SqlParameter("@UsuarioElimina", Utilitario.Instancia.SesionUsuario.usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarGasto(int IdRuta, int IdOperacion, int TotalDias, decimal GastoTotal, int Detalle)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_InsertarGasto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.Parameters.Add(new SqlParameter("@TotalDias", TotalDias));
                comando.Parameters.Add(new SqlParameter("@GastoTotal", GastoTotal));
                comando.Parameters.Add(new SqlParameter("@Detalle", Detalle));
                comando.Parameters.Add(new SqlParameter("@UsuarioCrea", Utilitario.Instancia.SesionUsuario.usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGasto(int IdRuta, int IdOperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarGasto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto(int NroProgramacion, int idGastoxRutaC, decimal TotalEntregado, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@idGastoxRutaC", idGastoxRutaC));
                comando.Parameters.Add(new SqlParameter("@TotalEntregado", TotalEntregado));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ImprimirPago(int NroProgramacion, int idGastoxRutaC, string Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ImprimirPago", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@idGastoxRutaC", idGastoxRutaC));
                comando.Parameters.Add(new SqlParameter("@CodGasto", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarGasto(int NroProgramacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarGasto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(int IdRuta, int IdOperacion, int idTiempo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                cmd.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                cmd.Parameters.Add(new SqlParameter("@idTiempo", idTiempo));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarRegistro(string CodGasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarRegistro", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarConductores(string Filtro)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarConductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Filtro", Filtro));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarViajes(int IdConductor, string Ruta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarViajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdConductor", IdConductor));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillas(int Programacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarPlanillas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(int Accion, string CodGasto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Accion", Accion));
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarNombreRUC(string NroRUC)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarNombreRUC", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@NroRUC", NroRUC));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarLiquidaciones(string CodGasto, DateTime FechaLiquidacion,
                         char TipoImpuesto, string ConceptoGasto, string DescripcionGasto, string NroRUC, string NombreCompleto, string CodigoDocumento,
                         string NroDocumento, decimal MontoAfecto, decimal MontoNoAfecto, decimal MontoImpuestos, decimal MontoPagado, string MotivoGasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_InsertarLiquidaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@FechaLiquidacion", FechaLiquidacion));
                comando.Parameters.Add(new SqlParameter("@TipoImpuesto", TipoImpuesto));
                comando.Parameters.Add(new SqlParameter("@ConceptoGasto", ConceptoGasto));
                comando.Parameters.Add(new SqlParameter("@DescripcionGasto", DescripcionGasto));
                comando.Parameters.Add(new SqlParameter("@NroRUC", NroRUC));
                comando.Parameters.Add(new SqlParameter("@NombreCompleto", NombreCompleto));
                comando.Parameters.Add(new SqlParameter("@CodigoDocumento", CodigoDocumento));
                comando.Parameters.Add(new SqlParameter("@NroDocumento", NroDocumento));
                comando.Parameters.Add(new SqlParameter("@MontoAfecto", MontoAfecto));
                comando.Parameters.Add(new SqlParameter("@MontoNoAfecto", MontoNoAfecto));
                comando.Parameters.Add(new SqlParameter("@MontoImpuestos", MontoImpuestos));
                comando.Parameters.Add(new SqlParameter("@MontoPagado", MontoPagado));
                comando.Parameters.Add(new SqlParameter("@MotivoGasto", MotivoGasto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarLiquidaciones(int idNroLiquidacion, string CodGasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_EliminarLiquidaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idNroLiquidacion", idNroLiquidacion));
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_LiquidarTicketGasto(int idConductor, int Programacion, string Placa, DateTime FechaLiquidacion,
                         int Planilla, decimal Gasto, decimal Total, decimal Reintegro, string DescripcionRG, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_LiquidarTicketGasto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@FechaLiquidacion", FechaLiquidacion));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Gasto", Gasto));
                comando.Parameters.Add(new SqlParameter("@Total", Total));
                comando.Parameters.Add(new SqlParameter("@Reintegro", Reintegro));
                comando.Parameters.Add(new SqlParameter("@DescripcionRG", DescripcionRG));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasLiquidadas(int IdOperacion, string fini, string ffin, string conductor, string planilla, string Ruta, int estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarPlanillasLiquidadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fini));
                comando.Parameters.Add(new SqlParameter("@FechaFin", ffin));
                comando.Parameters.Add(new SqlParameter("@NombreConductor", conductor));
                comando.Parameters.Add(new SqlParameter("@Planilla", planilla));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                comando.Parameters.Add(new SqlParameter("@estado", estado));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarRecibosLiquidados(string CodGasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarRecibosLiquidados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarViaticos(int NroTicket, string CodGasto, int idConductor, DateTime Fecha, int idTipoViatico, decimal Monto, string Descripcion, string Motivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_InsertarViaticos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@idTipoViatico", idTipoViatico));
                comando.Parameters.Add(new SqlParameter("@Monto", Monto));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarViaticos(int NroTicket, int idViatico, int idConductor, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_EliminarViaticos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                comando.Parameters.Add(new SqlParameter("@idViatico", idViatico));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarViaticos(int Opcion, int idConductor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarViaticos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarRegistroViaticos(int Opcion, string fini, string ffin, string conductor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarRegistroViaticos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fini));
                comando.Parameters.Add(new SqlParameter("@FechaFin", ffin));
                comando.Parameters.Add(new SqlParameter("@NombreConductor", conductor));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ActualizarRecibosLiquidados(string CodGasto, decimal ImporteTotal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ActualizarRecibosLiquidados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@ImporteTotal", ImporteTotal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarPlanilla(int Programacion, string CodGasto, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_EliminarPlanilla", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroProgramacion", Programacion));
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(int idPeaje, int Opcion, string NroRUC)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idPeaje", idPeaje));
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@NroRUC", NroRUC));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarTiempos()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarTiempos", conexion);
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

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarCambioRutas(string Planilla)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarCambioRutas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_BuscarGuiaIndividual(string TipoGuia, string SerieGuia, string NumeroGuia)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_BuscarGuiaIndividual", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TipoGuia", TipoGuia));
                cmd.Parameters.Add(new SqlParameter("@SerieGuia", SerieGuia));
                cmd.Parameters.Add(new SqlParameter("@NumeroGuia", NumeroGuia));
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReportesApp_Operaciones_Registrar_SolicitudCambios_GuiaElectronica(ref string NroSolicitud, string SerieGuia, string NumeroGuia, string Campo, string NuevoValor, string MotivoSolicitud)
        {
            SqlCommand cmd = null;
            bool respuesta = false;

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Registrar_SolicitudCambios_GuiaElectronica", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NumeroSolicitud", NroSolicitud));
                cmd.Parameters.Add(new SqlParameter("@SerieGuia", SerieGuia));
                cmd.Parameters.Add(new SqlParameter("@NumeroGuia", NumeroGuia));
                cmd.Parameters.Add(new SqlParameter("@CampoGuia", Campo));
                cmd.Parameters.Add(new SqlParameter("@NuevoValor", NuevoValor));
                cmd.Parameters.Add(new SqlParameter("@MotivoSolicitud", MotivoSolicitud));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Utilitario.Instancia.SesionUsuario.usuario));

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia);

                    if (respuesta) { NroSolicitud = Convert.ToString(dr["NumeroSolicitud"]); }
                }
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_operaciones_ListarSolicitudes_CambioDatosGuia(string Serie, string Numero)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_operaciones_ListarSolicitudes_CambioDatosGuia", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TipoGuia", "T"));
                
                if (Serie == "") { cmd.Parameters.Add(new SqlParameter("@SerieGuia", DBNull.Value)); }
                else { cmd.Parameters.Add(new SqlParameter("@SerieGuia", Serie)); }

                if (Numero == "") { cmd.Parameters.Add(new SqlParameter("@NumeroGuia", DBNull.Value)); }
                else { cmd.Parameters.Add(new SqlParameter("@NumeroGuia", Numero)); }
                
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReportesApp_Operaciones_ActualizarDatosGuia(string nroSolicitud,int idConductor,string nombresConductor)
        {
            SqlCommand cmd = null;
            bool respuesta = false;

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ActualizarDatosGuiaElectronica", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroSolicitud", nroSolicitud));
                cmd.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                cmd.Parameters.Add(new SqlParameter("@NombresConductor", nombresConductor));
                cmd.Parameters.Add(new SqlParameter("@UsuarioAprueba", Utilitario.Instancia.SesionUsuario.usuario));

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                { respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia); }
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoRuta(int IdOperacion, string Ruta)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarGastoRuta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idOperacion", IdOperacion));
                cmd.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_AgregarDiferencialRuta(int NroProgramacion, int idGastoxRutaC, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_AgregarDiferencialRuta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                cmd.Parameters.Add(new SqlParameter("@idGastoxRutaC", idGastoxRutaC));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_ImprimirDiferencial(int NroProgramacion, string CodGasto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ImprimirDiferencial", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_PagarViaticos(int NroProgramacion, string Usuario, string CodGasto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_PagarViaticos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ComprobanteLiquidaciones(int idNroLiquidacion, string CodGasto, DateTime FechaLiquidacion,
                         char TipoImpuesto, string NroRUC, string NombreCompleto, string CodigoDocumento, string NroDocumento, decimal MontoAfecto,
                         decimal MontoNoAfecto, decimal MontoImpuestos, decimal MontoPagado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ComprobanteLiquidaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idNroLiquidacion", idNroLiquidacion));
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                cmd.Parameters.Add(new SqlParameter("@FechaLiquidacion", FechaLiquidacion));
                cmd.Parameters.Add(new SqlParameter("@TipoImpuesto", TipoImpuesto));
                cmd.Parameters.Add(new SqlParameter("@NroRUC", NroRUC));
                cmd.Parameters.Add(new SqlParameter("@NombreCompleto", NombreCompleto));
                cmd.Parameters.Add(new SqlParameter("@CodigoDocumento", CodigoDocumento));
                cmd.Parameters.Add(new SqlParameter("@NroDocumento", NroDocumento));
                cmd.Parameters.Add(new SqlParameter("@MontoAfecto", MontoAfecto));
                cmd.Parameters.Add(new SqlParameter("@MontoNoAfecto", MontoNoAfecto));
                cmd.Parameters.Add(new SqlParameter("@MontoImpuestos", MontoImpuestos));
                cmd.Parameters.Add(new SqlParameter("@MontoPagado", MontoPagado));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarViaticos(int idConductor, DateTime FechaViatico, string Comprobante, decimal Monto, string NroRUC, string Planilla)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_RegistrarViaticos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@FechaViatico", FechaViatico));
                comando.Parameters.Add(new SqlParameter("@Comprobante", Comprobante));
                comando.Parameters.Add(new SqlParameter("@Monto", Monto));
                comando.Parameters.Add(new SqlParameter("@NroRUC", NroRUC));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ViaticoSinProg(int idConductor, DateTime Fecha, int idTipoViatico, decimal Monto, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ViaticoSinProg", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@idTipoViatico", idTipoViatico));
                comando.Parameters.Add(new SqlParameter("@Monto", Monto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarViaticoProg(int idConductor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarViaticoProg", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_AdjuntarPlanilla(string CodGasto, int idConductor, int NroProgramacion, int idOperacion, int idGastoxRutaC, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_AdjuntarPlanilla", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                comando.Parameters.Add(new SqlParameter("@idGastoxRutaC", idGastoxRutaC));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarReintegro(int idConductor, int Programacion, string Placa, DateTime FechaLiquidacion,
                         int Planilla, decimal Gasto, decimal Total, decimal Reintegro, string DescripcionRG, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_GenerarReintegro", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@FechaLiquidacion", FechaLiquidacion));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Gasto", Gasto));
                comando.Parameters.Add(new SqlParameter("@Total", Total));
                comando.Parameters.Add(new SqlParameter("@Reintegro", Reintegro));
                comando.Parameters.Add(new SqlParameter("@DescripcionRG", DescripcionRG));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_PagarViaticosTolvas(int idConductor, int Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_PagarViaticosTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }
       
        public DataTable ReportesApp_Operaciones_TicketGasto_PagarSinViaje(int idConductor, int Planilla, decimal monto, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_PagarSinViaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Monto", monto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ListarDireccionesCliente(int idCliente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarDireccionesCliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCliente", idCliente));

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

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje(string Conductor, string Ruta)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                cmd.Parameters.Add(new SqlParameter("@Ruta", Ruta));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_ActualizarPlanilla(int NroProgramacion, string Planilla, string Usuario)//, int idConductor, decimal Gasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ActualizarPlanilla", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroProgramacion));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                //comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                //comando.Parameters.Add(new SqlParameter("@Gasto", Gasto));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public bool ReportesApp_Operaciones_ActualizarFechaGuias(string fecha, string xmlGuias,string tipofecha)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {
                
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_ActualizarFechaGuias", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NuevaFecha", fecha));
                comando.Parameters.Add(new SqlParameter("@xml", xmlGuias));
                comando.Parameters.Add(new SqlParameter("@tipofecha", tipofecha));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_AutorizarReintegro(string Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_AutorizarReintegro", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public bool ReportesApp_Operaciones_RegistrarFechaTermino(int _codProgramacion, string _anio, string fechaTermino)
        {
            bool respuesta = false;
            SqlCommand comando = null;

            try
            {

                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarFechaTermino", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idprogramacion", _codProgramacion));
                comando.Parameters.Add(new SqlParameter("@anio", _anio));
                comando.Parameters.Add(new SqlParameter("@fechaTermino", fechaTermino));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarReporteGasto(string Planilla, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarReporteGasto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(int Opcion, int IdRuta, int IdOperacion, int idGastoxRutaD)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.Parameters.Add(new SqlParameter("@idGastoxRutaD", idGastoxRutaD));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_RutaZona_ListarZonas()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_RutaZona_ListarZonas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_RutaZona_AsignarEditarZonas(int Opcion, int IdRuta, int IdOperacion, int idZona, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_RutaZona_AsignarEditarZonas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.Parameters.Add(new SqlParameter("@idZona", idZona));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_RutaZona_ListarRegistroZonas(int IdOperacion, string Ruta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_RutaZona_ListarRegistroZonas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasPendientes(string Conductor, int IdOperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarPlanillasPendientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaTolvas(int idConductor, int idTracto, int idCarreta, int idRuta, int idGastoXRutaC,
                                                                                     decimal TotalEntregado, DateTime FechaViaje, int TotalDias, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                comando.Parameters.Add(new SqlParameter("@idCarreta", idCarreta));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@idGastoxRutaC", idGastoXRutaC));
                comando.Parameters.Add(new SqlParameter("@TotalEntregado", TotalEntregado));
                comando.Parameters.Add(new SqlParameter("@FechaViaje", FechaViaje));
                comando.Parameters.Add(new SqlParameter("@TotalDias", TotalDias));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas(string CodGasto, int idViatico)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@idViatico", idViatico));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas2(string CodGasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas2", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasTolvas(string FechaInicio, string FechaFin, string Conductor, string Planilla, int Pendientes)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarPlanillasTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@NombreConductor", Conductor));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@Pendientes", Pendientes));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarPlanillaTolvas(string CodGasto, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_EliminarPlanillaTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarViaticosTolvas(int NroTicket, string CodGasto, int idConductor, DateTime Fecha, int idTipoViatico, decimal Monto, string Descripcion, string Motivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_InsertarViaticosTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@idTipoViatico", idTipoViatico));
                comando.Parameters.Add(new SqlParameter("@Monto", Monto));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarGastosTolvas(int idViatico, int idConductor, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_EliminarGastosTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idViatico", idViatico));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarProveedor(string Proveedor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarProveedor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Proveedor", Proveedor));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ObtenerUltimoCorrelativoGuiaTransportista(string serie)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Serie", serie));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPeajes()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarPeajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarModificarPeajes(int idPeaje, decimal Peaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_InsertarModificarPeajes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idPeaje", idPeaje));
                comando.Parameters.Add(new SqlParameter("@Peaje", Peaje));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_FiltrarPeaje(int Planilla, int IdRuta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_FiltrarPeaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@IdRuta", IdRuta));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Liquidacion_PlanillasPendientesConsolidado(string fini, string ffin, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Liquidacion_PlanillasPendientesConsolidado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", fini));
                comando.Parameters.Add(new SqlParameter("@FechaFin", ffin));
                comando.Parameters.Add(new SqlParameter("@USUARIO", Usuario));
                comando.CommandTimeout = 10000000;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public bool ReportesApp_OperacionesVerificarPermisoRutaxConductor(string UsuarioModulo)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_OperacionesVerificarPermisoRutaxConductor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Usuario", UsuarioModulo);



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

        public DataTable ReportesApp_Operaciones_TicketGasto_ModificarFechaViatico(int idListaViatico, int idConductor, string Planilla, string NroComprobante, DateTime FechaViatico)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ModificarFechaViatico", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idListaViatico", idListaViatico));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                comando.Parameters.Add(new SqlParameter("@NroComprobante", NroComprobante));
                comando.Parameters.Add(new SqlParameter("@FechaViatico", FechaViatico));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarReportePlanillas(string Sucursal, string FechaInicio, string FechaFin, int Detalle)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_GenerarReportePlanillas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Detalle", Detalle));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ListarGuiasViaje(int idViaje,string serie, string numero)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ListarGuias_Viaje", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idViaje", idViaje));
                cmd.Parameters.Add(new SqlParameter("@serie", serie));
                cmd.Parameters.Add(new SqlParameter("@numero", numero));
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

        public bool ReportesApp_Operaciones_ReemplzarGuias(string idGuia, string SerieAnterior, string NumeroAnterior, string Serie, string Numero, int idviaje, string remitente, string guiasotros, string Idot, int idDireccionPartida, int idDireccionLlegada, int LineaOT)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_ReemplzarGuias", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idGuia", idGuia);
                comando.Parameters.AddWithValue("@SerieAnterior", SerieAnterior);
                comando.Parameters.AddWithValue("@NumeroAnterior", NumeroAnterior);
                comando.Parameters.AddWithValue("@Serie", Serie);
                comando.Parameters.AddWithValue("@Numero", Numero);
                comando.Parameters.AddWithValue("@idviaje", idviaje);
                comando.Parameters.AddWithValue("@Remitente", remitente);
                comando.Parameters.AddWithValue("@GuiasOtros", guiasotros);
                comando.Parameters.AddWithValue("@idOT", Idot);
                comando.Parameters.AddWithValue("@idDireccionPartida", idDireccionPartida);
                comando.Parameters.AddWithValue("@idDireccionLlegada", idDireccionLlegada);
                comando.Parameters.AddWithValue("@LineaOT", LineaOT);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);

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

        public bool ReportesApp_Operaciones_ConsultarGuiaExiste(string Serie, string Numero)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_ConsultarGuiaExiste", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Serie", Serie);
                comando.Parameters.AddWithValue("@Numero", Numero);

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

        public bool ReportesApp_Operaciones_ConfirmarDesconfirmar(int ConfirmarDesconfirmar,string xml)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_ConfirmarGuiaRecibidas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@ConfirmarDesconfimar", 1);
                comando.Parameters.AddWithValue("@xml", xml);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);

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

        public DataTable ReportesApp_ListarGuiasElectronicasRecepcionadas(string tipoGuia, string fechaInicio, string fechaFin, string Serie, string Numero, bool todos, bool estadoAprobado, bool estadoRevertido, bool estadoRechazado, string viaje, string Cliente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarGuiasPendienteRecepcion", conexion);

                cmd.Parameters.AddWithValue("@tipoGuia", tipoGuia);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                if (Serie == "" || Serie == "Todos")
                {
                    cmd.Parameters.AddWithValue("@Serie", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Serie", Serie);
                }
                if (Numero == "")
                {
                    cmd.Parameters.AddWithValue("@Numero", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Numero", Numero);
                }

                if (todos)
                {
                    if (estadoAprobado)
                    {
                        cmd.Parameters.AddWithValue("@Aceptado", "ACEPTADO");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Aceptado", DBNull.Value);
                    }

                    if (estadoRevertido)
                    {
                        cmd.Parameters.AddWithValue("@Reversion", "REVERSION");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Reversion", DBNull.Value);
                    }

                    if (estadoRechazado)
                    {
                        cmd.Parameters.AddWithValue("@Rechazado", "RECHAZADO");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Rechazado", DBNull.Value);
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Aceptado", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Reversion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Rechazado", DBNull.Value);
                }
                if (viaje == "")
                {
                    cmd.Parameters.AddWithValue("@Viaje", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Viaje", viaje);
                }
                if (Cliente == "")
                {
                    cmd.Parameters.AddWithValue("@Cliente", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cliente", Cliente);
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

        public DataTable ReportesApp_Operaciones_PendientesDiarios_InsertarActividad(int Persona, string Descripcion, string Nivel, DateTime FechaInicio, DateTime FProyectada1,
                                                                                     string Seguimiento, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_PendientesDiarios_InsertarActividad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Nivel", Nivel));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FProyectada1", FProyectada1));
                comando.Parameters.Add(new SqlParameter("@Seguimiento", Seguimiento));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_ListarActividades(int FechaP, string Responsable, string Area, string Estado, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_PendientesDiarios_ListarActividades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaP", FechaP));
                comando.Parameters.Add(new SqlParameter("@Responsable", Responsable));
                comando.Parameters.Add(new SqlParameter("@Area", Area));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_FiltrarActividades(int idActividad)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_PendientesDiarios_FiltrarActividades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idActividad", idActividad));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_ModificarActividades(int Opcion, int idActividad, int Persona, string Estado, string Seguimiento, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_PendientesDiarios_ModificarActividades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idActividad", idActividad));
                comando.Parameters.Add(new SqlParameter("@Persona", Persona));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Seguimiento", Seguimiento));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_ReprogramarActividades(int idActividad, DateTime FechaReprog, int Contador, string Seguimiento, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_PendientesDiarios_ReprogramarActividades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idActividad", idActividad));
                comando.Parameters.Add(new SqlParameter("@FechaReprog", FechaReprog));
                comando.Parameters.Add(new SqlParameter("@Contador", Contador));
                comando.Parameters.Add(new SqlParameter("@Seguimiento", Seguimiento));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes(int idConductor, int idProgramacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@idProgramacion", idProgramacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public bool ReportesApp_Operaciones_VincularGuiasNoEnlazadas(int idviaje, string viaje, string Serie, string Numero,int LineaOT, string Ticket)
        {
             
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_VincularGuiasNoEnlazadas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Viaje", viaje);
                comando.Parameters.AddWithValue("@idViaje", idviaje);
                comando.Parameters.AddWithValue("@Serie", Serie);
                comando.Parameters.AddWithValue("@Numero", Numero);
                comando.Parameters.AddWithValue("@LineaOT", LineaOT);
                comando.Parameters.AddWithValue("@Ticket", Ticket);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);

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

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTractos(int Opcion, string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarTractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearTractos(int IdUnidad, string TipoUnidad, string Periodo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_MapearTractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdUnidad", IdUnidad));
                comando.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearCondiciones(int idCondicion, string Periodo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_MapearCondiciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idCondicion", idCondicion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_QuitarTractos(int IdUnidad, string TipoUnidad, string Periodo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_QuitarTractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdUnidad", IdUnidad));
                comando.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadTractos(string Periodo, string Placa, string Programacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadTractos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaOperatividad(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarTablaOperatividad", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_RegistrarPlacas(string Periodo, string xmlOperatividad, int idCondicion, int idOperacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_RegistrarPlacas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@xmlOperatividad", xmlOperatividad));
                comando.Parameters.Add(new SqlParameter("@idCondicion", idCondicion));
                comando.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearCondicionesCarretas(int idCondicion, string Periodo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_MapearCondicionesCarretas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idCondicion", idCondicion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadCarreta(string Periodo, string Placa, string Programacion, string Carreta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadCarreta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                comando.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadCarreta(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarOperatividadCarreta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_RegistrarCarretas(string Periodo, string xmlOperatividad, int idCondicion, int idOperacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_RegistrarCarretas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@xmlOperatividad", xmlOperatividad));
                comando.Parameters.Add(new SqlParameter("@idCondicion", idCondicion));
                comando.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTractosOP(int Opcion, int idCondicion, DateTime Fecha, string TipoUnidad, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarTractosOP", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCondicion", idCondicion));
                comando.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                comando.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_IngresarComentarios(int idOperatividad, string TipoUnidad, string Comentario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_IngresarComentarios", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idOperatividad", idOperatividad));
                comando.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                comando.Parameters.Add(new SqlParameter("@Comentario", Comentario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarItemBotiquin(string Descripcion, string Codigo, int Cantidad, int Duracion, string TipoBotiquin, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_RegistrarItemBotiquin", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                comando.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                comando.Parameters.Add(new SqlParameter("@Duracion", Duracion));
                comando.Parameters.Add(new SqlParameter("@TipoBotiquin", TipoBotiquin));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarItemsAsignados(int Opcion, int idVehiculo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarItemsAsignados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin(int Opcion, int idBotiquinUnidadC, int idItemBotiquin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idBotiquinUnidadC", idBotiquinUnidadC));
                comando.Parameters.Add(new SqlParameter("@idItemBotiquin", idItemBotiquin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_AsignarBotiquin(int idVehiculo, string TipoBotiquin, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_AsignarBotiquin", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idVehiculo", idVehiculo));
                comando.Parameters.Add(new SqlParameter("@TipoBotiquin", TipoBotiquin));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarBotiquines(string Placa, string Operacion, string TipoBotiquin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarBotiquines", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                comando.Parameters.Add(new SqlParameter("@TipoBotiquin", TipoBotiquin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_CrearModificarBotiquin(int idBotiquinUnidadC, int idItemBotiquin, int Cantidad,
                                                                                     DateTime FechaVencimiento, string Observacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_CrearModificarBotiquin", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idBotiquinUnidadC", idBotiquinUnidadC));
                comando.Parameters.Add(new SqlParameter("@idItemBotiquin", idItemBotiquin));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                comando.Parameters.Add(new SqlParameter("@FechaVencimiento", FechaVencimiento));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_ListarUnidadMedidaCargaTotal_Sunat()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarUnidadMedidaCargaTotal_Sunat", conexion);
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

        public DataTable ReportesApp_Operaciones_ControlItems_GenerarRequerimiento(int idBotiquinUnidadC, int idBotiquinUnidadD, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_GenerarRequerimiento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idBotiquinUnidadC", idBotiquinUnidadC));
                comando.Parameters.Add(new SqlParameter("@idBotiquinUnidadD", idBotiquinUnidadD));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ListarMotivos(int Opcion, string Conductor)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_EntregaUnidad_ListarMotivos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarConstancia(int Opcion, int idConstancia, int IdTracto, int IdCarreta, int IdConductorAnt, int IdConductorNuevo,
                                                                                         string FechaSolicitud, string HoraSolicitud, string Motivo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarConstancia", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idConstancia", idConstancia));
                comando.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                comando.Parameters.Add(new SqlParameter("@IdCarreta", IdCarreta));
                comando.Parameters.Add(new SqlParameter("@IdConductorAnt", IdConductorAnt));
                comando.Parameters.Add(new SqlParameter("@IdConductorNuevo", IdConductorNuevo));
                comando.Parameters.Add(new SqlParameter("@FechaSolicitud", FechaSolicitud));
                comando.Parameters.Add(new SqlParameter("@HoraSolicitud", HoraSolicitud));
                comando.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ListarTicket(string CodConstancia)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_EntregaUnidad_ListarTicket", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodConstancia", CodConstancia));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Mantenimiento_EntregaUnidad_ListarConstancia(string CodConstancia, string Placa, string FechaInicio, string FechaFin, string Estado, string Operacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_EntregaUnidad_ListarConstancia", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodConstancia", CodConstancia));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(int Opcion, string CodConstancia, DateTime HoraInicio, DateTime HoraFin, string Observacion, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@CodConstancia", CodConstancia));
                comando.Parameters.Add(new SqlParameter("@HoraInicio", HoraInicio));
                comando.Parameters.Add(new SqlParameter("@HoraFin", HoraFin));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(int idTracto, int idConductor, int idRuta, int idOperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_ListarUnidadMedida_SunatTotal()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarUnidadMedidaCargaTotal_Sunat", conexion);
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

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(int idTracto, int idRuta, string Sucursal)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public bool ReportesApp_Operaciones_ListarDatosViajesPorFecha_Viaje(string tarifa)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_DatosOT_ActualizarTarifa", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@xmlTarifa", tarifa);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read()) { respuesta = Utilitario.CodigoRetorno(Convert.ToString(dr["Mensaje"]), ref Utilitario.Instancia.Advertencia); }
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { comando.Connection.Close(); }
            return respuesta;
        }

        public DataTable ReportesApp_Operaciones_DatosOT_ListarGuiasViaje(int Opcion, string CodViaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_DatosOT_ListarGuiasViaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@CodViaje", CodViaje));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_DatosOT_EditarGuiasViaje(int Opcion, string CodViaje, int OT, string GuiaT, string GuiaR)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_DatosOT_EditarGuiasViaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@CodViaje", CodViaje));
                comando.Parameters.Add(new SqlParameter("@OT", OT));
                comando.Parameters.Add(new SqlParameter("@GuiaT", GuiaT));
                comando.Parameters.Add(new SqlParameter("@GuiaR", GuiaR));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarReporteReintegros(string Sucursal, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_GenerarReporteReintegros", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoXConcepto(string Sucursal, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarGastoXConcepto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Sucursal", Sucursal));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(int IdVehiculo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdVehiculo", IdVehiculo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ListarReporteGuias(string FechaInicio, string FechaFin, string Serie, string Numero, string Tracto, string Carreta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ListarReporteGuias", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Serie", Serie));
                comando.Parameters.Add(new SqlParameter("@Numero", Numero));
                comando.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                comando.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado(int idConductor, DateTime FechaTraslado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IdConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@FechaTraslado", FechaTraslado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarKitNeumatico(int Opcion, string Empleado, string Herramienta, string Tracto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarKitNeumatico", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                comando.Parameters.Add(new SqlParameter("@Herramienta", Herramienta));
                comando.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Previajes_CalcularRendProm(string Operacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_CalcularRendProm", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlDocumentos_GenerarRequerimiento(int idDocumento, string TipoDocumento, int idUnidad, string Placa, string CentroCosto, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlDocumentos_GenerarRequerimiento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
                comando.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
                comando.Parameters.Add(new SqlParameter("@idUnidad", idUnidad));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@CentroCosto", CentroCosto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(int IDRelacion, string TipoRelacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDRelacion", IDRelacion));
                comando.Parameters.Add(new SqlParameter("@TipoRelacion", TipoRelacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlTarifas_InsertarTarifas(string xmlTarifas, string Moneda, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlTarifas_InsertarTarifas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@xmlTarifas", xmlTarifas));
                comando.Parameters.Add(new SqlParameter("@Moneda", Moneda));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlTarifas_ListarTarifas(string Ruta, string FechaInicio, string FechaFin, string Cliente)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlTarifas_ListarTarifas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarExtintor(int Opcion, int idExtintor, int IdTracto, string Codigo, DateTime FechaVenc,
                                                                                                decimal Peso, string UnidadPeso, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarExtintor", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idExtintor", idExtintor));
                comando.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                comando.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                comando.Parameters.Add(new SqlParameter("@FechaVenc", FechaVenc));
                comando.Parameters.Add(new SqlParameter("@Peso", Peso));
                comando.Parameters.Add(new SqlParameter("@UnidadPeso", UnidadPeso));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarExtintores(string Placa, string Estado, string Operacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarExtintores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Previajes_ActualizarViajeEstado(int IDTicket, string Anio, int IDTracto, int Estado, int TipoProgramacion,
                                                                                 DateTime FInicio, DateTime FFin, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_ActualizarViajeEstado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDTicket", IDTicket));
                comando.Parameters.Add(new SqlParameter("@Anio", Anio));
                comando.Parameters.Add(new SqlParameter("@IDTracto", IDTracto));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@TipoProgramacion", TipoProgramacion));
                comando.Parameters.Add(new SqlParameter("@FInicio", FInicio));
                comando.Parameters.Add(new SqlParameter("@FFin", FFin));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_DesbloqueoConductores(int Opcion, int idDesbloqueo, int idConductor, DateTime FechaCompromiso, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_DesbloqueoConductores", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idDesbloqueo", idDesbloqueo));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@FechaCompromiso", FechaCompromiso));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarConductoresDesbloqueados(string Conductor, string FechaCompromiso)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarConductoresDesbloqueados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                comando.Parameters.Add(new SqlParameter("@FechaCompromiso", FechaCompromiso));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Programacion_UltimoViajeCompletado(int IDRelacion, int TipoOperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Programacion_UltimoViajeCompletado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@IDRelacion", IDRelacion));
                comando.Parameters.Add(new SqlParameter("@TipoOperacion", TipoOperacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_InsertarIngresosOperacion(int idOperacion, string Periodo, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_InsertarIngresosOperacion", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaIngresos(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarTablaIngresos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(int Opcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_AsignarGrupo(int idRuta, int idGrupoViaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_CumplimientoViajes_AsignarGrupo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@idGrupoViaje", idGrupoViaje));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(int Opcion, int idGrupoViaje, DateTime FechaCumplimiento, int Disponibles,
                                                                                                int Proyectados, string Detalle, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idGrupoViaje", idGrupoViaje));
                comando.Parameters.Add(new SqlParameter("@FechaCumplimiento", FechaCumplimiento));
                comando.Parameters.Add(new SqlParameter("@Disponibles", Disponibles));
                comando.Parameters.Add(new SqlParameter("@Proyectados", Proyectados));
                comando.Parameters.Add(new SqlParameter("@Detalle", Detalle));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(int idGrupoViaje, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idGrupoViaje", idGrupoViaje));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_EliminarGrupo(int idGrupoViajeD, int idGrupoViaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_CumplimientoViajes_EliminarGrupo", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idGrupoViajeD", idGrupoViajeD));
                comando.Parameters.Add(new SqlParameter("@idGrupoViaje", idGrupoViaje));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarPuntosParada(int Opcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_ListarPuntosParada", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarPuntoParada(string Descripcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_RegistrarPuntoParada", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarEliminarParada(int Opcion, int idParada, string Trafico, int idRuta, string PuntoInicio,
                                                                                string PuntoParada, DateTime Horas, string UsuarioCreacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_RegistrarEliminarParada", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idParada", idParada));
                comando.Parameters.Add(new SqlParameter("@Trafico", Trafico));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@PuntoInicio", PuntoInicio));
                comando.Parameters.Add(new SqlParameter("@PuntoParada", PuntoParada));
                comando.Parameters.Add(new SqlParameter("@Horas", Horas));
                comando.Parameters.Add(new SqlParameter("@UsuarioCreacion", UsuarioCreacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarParadasRutas(int Opcion, string Ruta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_ListarParadasRutas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidado(int Opcion, int idConsolidado, int NroPreviaje, DateTime FechaProg, int idTracto, int idCarreta,
                                                                                       int idRuta, int idConductor, DateTime FechaViaje, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idConsolidado", idConsolidado));
                comando.Parameters.Add(new SqlParameter("@NroPreviaje", NroPreviaje));
                comando.Parameters.Add(new SqlParameter("@FechaProg", FechaProg));
                comando.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                comando.Parameters.Add(new SqlParameter("@idCarreta", idCarreta));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@FechaViaje", FechaViaje));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidado(string FechaInicio, string FechaFin, string Vehiculo, string Conductor, string Ruta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidado", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Vehiculo", Vehiculo));
                comando.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidadoDetalle(int idConsolidado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidadoDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConsolidado", idConsolidado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidadoDetalle(int idConsolidado, int idParada, DateTime HoraDuracion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidadoDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConsolidado", idConsolidado));
                comando.Parameters.Add(new SqlParameter("@idParada", idParada));
                comando.Parameters.Add(new SqlParameter("@HoraDuracion", HoraDuracion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_EliminarConsolidadoDetalle(int idConsolidado, int idParada)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_EliminarConsolidadoDetalle", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConsolidado", idConsolidado));
                comando.Parameters.Add(new SqlParameter("@idParada", idParada));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarFechaTermino(int idConsolidado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ItinerarioViajes_ListarFechaTermino", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@idConsolidado", idConsolidado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_ActualizarPeso(string SerieT, string NumeroT, int idRuta)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Previajes_Tolvas_ActualizarPeso", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@SerieT", SerieT));
                comando.Parameters.Add(new SqlParameter("@NumeroT", NumeroT));
                comando.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarKAD(int Opcion, int idKAD, int IdTracto, string KitAsignado, string Observacion, byte[] Imagen, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarKAD", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idKAD", idKAD));
                comando.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                comando.Parameters.Add(new SqlParameter("@KitAsignado", KitAsignado));
                comando.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                if (Imagen == null) { comando.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { comando.Parameters.AddWithValue("@Imagen", Imagen); }
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarKAD(string Placa, string Estado, string Operacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarKAD", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadCisternas(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarOperatividadCisternas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadCortineras(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarOperatividadCortineras", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadPlataformas(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarOperatividadPlataformas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadTolvas(string Periodo)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarOperatividadTolvas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public bool ReportesApp_OperacionesVerificarTipoBreveteAdicional(string UsuarioModulo)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_OperacionesVerificarTipoBreveteAdicional", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Usuario", UsuarioModulo);

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

        public bool ReportesApp_Operaciones_RegistrarBreveteAdicional(int IdPersonaOperacion, string adicionar)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;

            try
            {

                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_RegistrarBrevete_Adicional", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idPersona", IdPersonaOperacion);
                comando.Parameters.AddWithValue("@Adicional", adicionar);

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

        public DataTable ReportesApp_Operaciones_ControlItems_ListarObservacionesI(int Opcion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarObservacionesI", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarEditarTanques(int Opcion, int idRegistroTC, string Programacion, DateTime FechaRevision, int idTracto, int idCarreta,
                                                                                     int PersonaConductor, string LugarInspeccion, int PersonaInspector, string Observacion, byte[] ImagenHallazgo,
                                                                                     byte[] ImagenHallazgo2, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_RegistrarEditarTanques", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistroTC", idRegistroTC));
                cmd.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                cmd.Parameters.Add(new SqlParameter("@FechaRevision", FechaRevision));
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@idCarreta", idCarreta));
                cmd.Parameters.Add(new SqlParameter("@PersonaConductor", PersonaConductor));
                cmd.Parameters.Add(new SqlParameter("@LugarInspeccion", LugarInspeccion));
                cmd.Parameters.Add(new SqlParameter("@PersonaInspector", PersonaInspector));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                if (ImagenHallazgo == null) { cmd.Parameters.AddWithValue("@ImagenHallazgo", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenHallazgo", ImagenHallazgo); }
                if (ImagenHallazgo2 == null) { cmd.Parameters.AddWithValue("@ImagenHallazgo2", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenHallazgo2", ImagenHallazgo2); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarTanquesCombustible(string Placa, string Operacion, string Conductor, string Estado, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarTanquesCombustible", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                comando.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarReparacion(int idRegistroTC, int PersonaTecnico, int PersonaSeguimiento, DateTime FechaReparacion, byte[] ImagenReparacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_RegistrarReparacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistroTC", idRegistroTC));
                cmd.Parameters.Add(new SqlParameter("@PersonaTecnico", PersonaTecnico));
                cmd.Parameters.Add(new SqlParameter("@PersonaSeguimiento", PersonaSeguimiento));
                cmd.Parameters.Add(new SqlParameter("@FechaReparacion", FechaReparacion));
                if (ImagenReparacion == null) { cmd.Parameters.AddWithValue("@ImagenReparacion", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenReparacion", ImagenReparacion); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarEditarAsientos(int Opcion, int idRegistroTA, string Programacion, string TipoAT, DateTime FechaRevision, int idTracto, int PersonaConductor,
                                                                                      string Tapizado, string Reclinable, string Corredizo, string Radio, string LugarInspeccion, int PersonaInspector,
                                                                                      byte[] ImagenHallazgo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_RegistrarEditarAsientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistroTA", idRegistroTA));
                cmd.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                cmd.Parameters.Add(new SqlParameter("@TipoAT", TipoAT));
                cmd.Parameters.Add(new SqlParameter("@FechaRevision", FechaRevision));
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@PersonaConductor", PersonaConductor));
                cmd.Parameters.Add(new SqlParameter("@Tapizado", Tapizado));
                cmd.Parameters.Add(new SqlParameter("@Reclinable", Reclinable));
                cmd.Parameters.Add(new SqlParameter("@Corredizo", Corredizo));
                cmd.Parameters.Add(new SqlParameter("@Radio", Radio));
                cmd.Parameters.Add(new SqlParameter("@LugarInspeccion", LugarInspeccion));
                cmd.Parameters.Add(new SqlParameter("@PersonaInspector", PersonaInspector));
                if (ImagenHallazgo == null) { cmd.Parameters.AddWithValue("@ImagenHallazgo", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenHallazgo", ImagenHallazgo); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarAsientosTimones(string TipoAT, string Placa, string Operacion, string Conductor, string Estado, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarAsientosTimones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@TipoAT", TipoAT));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                comando.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RepararAsientos(int idRegistroTA, int PersonaProveedor, int PersonaSeguimiento, DateTime FechaReparacion, byte[] ImagenReparacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_RepararAsientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistroTA", idRegistroTA));
                cmd.Parameters.Add(new SqlParameter("@PersonaProveedor", PersonaProveedor));
                cmd.Parameters.Add(new SqlParameter("@PersonaSeguimiento", PersonaSeguimiento));
                cmd.Parameters.Add(new SqlParameter("@FechaReparacion", FechaReparacion));
                if (ImagenReparacion == null) { cmd.Parameters.AddWithValue("@ImagenReparacion", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenReparacion", ImagenReparacion); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_ListarTiemposViajes(string Previaje, string FechaInicio, string FechaFin, string Ruta, string Programacion, string Placa, string Conductor)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarTiemposViajes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Previaje", Previaje));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                cmd.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_FiltrarTiemposViajes(int NroTicket)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_FiltrarTiemposViajes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(int Opcion, int NroTicket, string EstadoV, DateTime LlegadaPlanta, DateTime IngresoPlanta, DateTime InicioAtencion, DateTime FinAtencion, DateTime EntregaGuia, DateTime SalidaPlanta,
                         DateTime SalidaRuta, DateTime LlegadaCDA, DateTime InicioDescarga, DateTime FinDescarga, DateTime InicioRuta, DateTime LlegadaCDA2, DateTime InicioDescarga2, DateTime FinDescarga2, DateTime LlegadaBase, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@EstadoV", EstadoV));
                cmd.Parameters.Add(new SqlParameter("@LlegadaPlanta", LlegadaPlanta));
                cmd.Parameters.Add(new SqlParameter("@IngresoPlanta", IngresoPlanta));
                cmd.Parameters.Add(new SqlParameter("@InicioAtencion", InicioAtencion));
                cmd.Parameters.Add(new SqlParameter("@FinAtencion", FinAtencion));
                cmd.Parameters.Add(new SqlParameter("@EntregaGuia", EntregaGuia));
                cmd.Parameters.Add(new SqlParameter("@SalidaPlanta", SalidaPlanta));
                cmd.Parameters.Add(new SqlParameter("@SalidaRuta", SalidaRuta));
                cmd.Parameters.Add(new SqlParameter("@LlegadaCDA", LlegadaCDA));
                cmd.Parameters.Add(new SqlParameter("@InicioDescarga", InicioDescarga));
                cmd.Parameters.Add(new SqlParameter("@FinDescarga", FinDescarga));
                cmd.Parameters.Add(new SqlParameter("@InicioRuta", InicioRuta));
                cmd.Parameters.Add(new SqlParameter("@LlegadaCDA2", LlegadaCDA2));
                cmd.Parameters.Add(new SqlParameter("@InicioDescarga2", InicioDescarga2));
                cmd.Parameters.Add(new SqlParameter("@FinDescarga2", FinDescarga2));
                cmd.Parameters.Add(new SqlParameter("@LlegadaBase", LlegadaBase));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas(int Opcion, int NroTicket, string EstadoV, DateTime SalidaBase, DateTime LlegadaCarga, DateTime Carga, DateTime SalidaPlanta, DateTime LlegadaDescarga,
                         DateTime InicioDescarga, DateTime SalidaDescarga, DateTime LlegadaBase, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@EstadoV", EstadoV));
                cmd.Parameters.Add(new SqlParameter("@SalidaBase", SalidaBase));
                cmd.Parameters.Add(new SqlParameter("@LlegadaCarga", LlegadaCarga));
                cmd.Parameters.Add(new SqlParameter("@Carga", Carga));
                cmd.Parameters.Add(new SqlParameter("@SalidaPlanta", SalidaPlanta));
                cmd.Parameters.Add(new SqlParameter("@LlegadaDescarga", LlegadaDescarga));
                cmd.Parameters.Add(new SqlParameter("@InicioDescarga", InicioDescarga));
                cmd.Parameters.Add(new SqlParameter("@SalidaDescarga", SalidaDescarga));
                cmd.Parameters.Add(new SqlParameter("@LlegadaBase", LlegadaBase));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_ImportarTiempoViajes(int Opcion, string xmlDetalle, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_ImportarTiempoViajes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@xmlDetalle", xmlDetalle));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_ModificarTiempoViajes(int NroTicket, string EstadoV, string RutaViaje, string EstadoViaje, string Ubicacion, decimal PorcTransito, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_ModificarTiempoViajes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@EstadoV", EstadoV));
                cmd.Parameters.Add(new SqlParameter("@RutaViaje", RutaViaje));
                cmd.Parameters.Add(new SqlParameter("@EstadoViaje", EstadoViaje));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@PorcTransito", PorcTransito));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_ListarUbicaciones(int Opcion, string Operacion, string Estado, string RutaViaje)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarUbicaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@RutaViaje", RutaViaje));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte(int Opcion, int NroTicket, int idPernocte, DateTime FechaInicio, DateTime FechaFin, string Ubicacion,
                                                                                     string TipoPernocte, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@idPernocte", idPernocte));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Ubicacion", Ubicacion));
                cmd.Parameters.Add(new SqlParameter("@TipoPernocte", TipoPernocte));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_FiltrarTiemposPernoctes(int NroTicket)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_FiltrarTiemposPernoctes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_ListarTiemposPernocte(string Previaje, string FechaInicio, string FechaFin, string Ruta, string Programacion, string Placa, string Conductor)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarTiemposPernocte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Previaje", Previaje));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                cmd.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarTiempoAtencion(int Opcion, int idTiempoAtencion, string Destino, string HorarioLV, string HorarioS,
                                                                                   DateTime TiempoAtencion, int idOperacion, int idRuta, string UsuarioCrea)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_RegistrarTiempoAtencion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idTiempoAtencion", idTiempoAtencion));
                cmd.Parameters.Add(new SqlParameter("@Destino", Destino));
                cmd.Parameters.Add(new SqlParameter("@HorarioLV", HorarioLV));
                cmd.Parameters.Add(new SqlParameter("@HorarioS", HorarioS));
                cmd.Parameters.Add(new SqlParameter("@TiempoAtencion", TiempoAtencion));
                cmd.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                cmd.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                cmd.Parameters.Add(new SqlParameter("@UsuarioCrea", UsuarioCrea));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_ListarTiempoAtencion(string Destino)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarTiempoAtencion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Destino", Destino));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_AsignarKitNeumatico(int Opcion, int IDHerramienta, int Persona, int Tracto, string UserRegistra)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_AsignarKitNeumatico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@IDHerramienta", IDHerramienta));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                cmd.Parameters.Add(new SqlParameter("@UserRegistra", UserRegistra));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ModificarKitNeumatico(int PersonaAnterior, int PersonaNueva, int Tracto, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_ModificarKitNeumatico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@PersonaAnterior", PersonaAnterior));
                cmd.Parameters.Add(new SqlParameter("@PersonaNueva", PersonaNueva));
                cmd.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RevisarKitNeumatico(int Tracto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_RevisarKitNeumatico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idTracto", Tracto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ReporteGuiasRRHH(string fechaInicio, string fechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ReporteGuiasRRHH", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));

                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearIndicadores(string Periodo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Operatividad_MapearIndicadores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarIndicadores(int Opcion, string Periodo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarIndicadores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTractosTotal(int Mes, int Anio)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Operatividad_ListarTractosTotal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Mes", Mes));
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Programacion_RevisarAsignaciones(int idTracto, int idConductor, int TipoOperacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Programacion_RevisarAsignaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                cmd.Parameters.Add(new SqlParameter("@TipoOperacion", TipoOperacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_BuscarConductor(int IdTracto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_EntregaUnidad_BuscarConductor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion(int Opcion, string CodConstanciaA, int IdTracto, int UltimoPreviaje,
                         int IdConductorAnt, int IdConductorNuevo, string Observacion, string Motivo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@CodConstanciaA", CodConstanciaA));
                cmd.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                cmd.Parameters.Add(new SqlParameter("@UltimoPreviaje", UltimoPreviaje));
                cmd.Parameters.Add(new SqlParameter("@IdConductorAnt", IdConductorAnt));
                cmd.Parameters.Add(new SqlParameter("@IdConductorNuevo", IdConductorNuevo));
                cmd.Parameters.Add(new SqlParameter("@Observacion", Observacion));
                cmd.Parameters.Add(new SqlParameter("@Motivo", Motivo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ListarAsignaciones(string Conductor, string Placa, string FechaInicio, string FechaFin, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_EntregaUnidad_ListarAsignaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_ListarDisponibles(string TipoUnidad, string Placa)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_ListarDisponibles", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@TipoUnidad", TipoUnidad));
                cmd.Parameters.Add(new SqlParameter("@Placa", Placa));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ReporteViajesPendientes_Tolvas(string fechaini, string fechafin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ReporteViajesPendientes_Tolvas", conexion);
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

        public DataTable ReportesApp_Operaciones_Consolidado_Pesos_Operacion(string fechaini, string fechafin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Consolidado_Pesos_Operacion", conexion);
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

        public DataTable ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(int Opcion, int Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operacion_ControlDocumentos_ListarRelaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operacion_ControlDocumentos_InsertarEliminarVencimiento(int Opcion, int idVencimiento, int idMRelacion, int TipoDocumento, int Vencimiento,
                                                                                             string Area, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operacion_ControlDocumentos_InsertarEliminarVencimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idVencimiento", idVencimiento));
                cmd.Parameters.Add(new SqlParameter("@idMRelacion", idMRelacion));
                cmd.Parameters.Add(new SqlParameter("@TipoDocumento", TipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@Vencimiento", Vencimiento));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operacion_ControlDocumentos_ListarVencimientos(string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operacion_ControlDocumentos_ListarVencimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operacion_ControlDocumentos_ListarDocumentos(int Opcion, int idRelacion, int idOperacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operacion_ControlDocumentos_ListarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRelacion", idRelacion));
                cmd.Parameters.Add(new SqlParameter("@idOperacion", idOperacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public bool ReportesApp_Combustible_CargarGasboy(string xmlgasboy)
        {
            Boolean respuesta = false;
            SqlCommand comando = null;
            try
            {


                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                comando = new SqlCommand("ReportesApp_Operaciones_CargarGasboy", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@xmlgasboy", xmlgasboy);
                comando.Parameters.AddWithValue("@Usuario", Utilitario.Instancia.SesionUsuario.usuario);
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

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarPendientes(int IdVehiculo)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Mantenimiento_MttoCorrectivo_ListarPendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdVehiculo", IdVehiculo));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(int idTracto, int idRuta, string Programacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                cmd.Parameters.Add(new SqlParameter("@Programacion", Programacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarViaticosTicket(int Opcion, int NroTicket, int idRuta)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarViaticosTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarConductorAnterior(string CodGasto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarConductorAnterior", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento(string Planilla, int NroTicket, int TipoProgramacion, int idRuta,
                                                                                   int IdConductor, decimal TotalEntregado, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@TipoProgramacion", TipoProgramacion));
                cmd.Parameters.Add(new SqlParameter("@idRuta", idRuta));
                cmd.Parameters.Add(new SqlParameter("@IdConductor", IdConductor));
                cmd.Parameters.Add(new SqlParameter("@TotalEntregado", TotalEntregado));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas(string CodGasto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarAdelantosPlanilla(string CodGasto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarAdelantosPlanilla", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarAdelantosAdicionales(int IdOperacion, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarAdelantosAdicionales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarVuelto(string Planilla, int RG, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_RegistrarVuelto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                cmd.Parameters.Add(new SqlParameter("@RG", RG));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarConductores(int OpcionVC, string Filtro)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_ListarConductores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionVC", OpcionVC));
                cmd.Parameters.Add(new SqlParameter("@Filtro", Filtro));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor(int OpcionVC, int Persona)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionVC", OpcionVC));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarAsistencias(string Periodo, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                    DateTime FechaFin, string Codigo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_RegistrarAsistencias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@DiasPendientes", DiasPendientes));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarVacaciones(string Periodo, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                    DateTime FechaFin, string Codigo, int FechaRetorno, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_RegistrarVacaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@DiasPendientes", DiasPendientes));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@FechaRetorno", FechaRetorno));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarCompensaciones(string Periodo, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                        DateTime FechaFin, string Codigo, int FechaRetorno, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_RegistrarCompensaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@DiasPendientes", DiasPendientes));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                cmd.Parameters.Add(new SqlParameter("@FechaRetorno", FechaRetorno));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionesVC(int OpcionVC, string Periodo, string Conductor, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionesVC", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionVC", OpcionVC));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC(string Periodo, string Conductor, string Operacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Conductor", Conductor));
                cmd.Parameters.Add(new SqlParameter("@Operacion", Operacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionesVC(int OpcionVC, string Periodo, int idProgVC, int IDPersona, DateTime FechaRetorno)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionesVC", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionVC", OpcionVC));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@idProgVC", idProgVC));
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaRetorno", FechaRetorno));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionesVC(int OpcionVC, string Periodo, int idProgVC, int IDPersona, DateTime FechaRetorno, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionesVC", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionVC", OpcionVC));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@idProgVC", idProgVC));
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaRetorno", FechaRetorno));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionConductor(int OpcionVC, int idProgVC, int IDPersona, DateTime FechaRetorno)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionConductor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionVC", OpcionVC));
                cmd.Parameters.Add(new SqlParameter("@idProgVC", idProgVC));
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaRetorno", FechaRetorno));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionSPRING(int OpcionVC, string Periodo, int idProgVC, int IDPersona, DateTime FechaRetorno, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionSPRING", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@OpcionVC", OpcionVC));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@idProgVC", idProgVC));
                cmd.Parameters.Add(new SqlParameter("@IDPersona", IDPersona));
                cmd.Parameters.Add(new SqlParameter("@FechaRetorno", FechaRetorno));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarRutasXConductor(int Persona)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ProgramacionVC_ListarRutasXConductor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems(int Opcion, string CodigoItem)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@CodigoItem", CodigoItem));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarImplemento(int Opcion, int idItemV, string CodigoItemAlmacen, string CodigoInterno, int IDCategoria, string Descripcion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarImplemento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idItemV", idItemV));
                cmd.Parameters.Add(new SqlParameter("@CodigoItemAlmacen", CodigoItemAlmacen));
                cmd.Parameters.Add(new SqlParameter("@CodigoInterno", CodigoInterno));
                cmd.Parameters.Add(new SqlParameter("@IDCategoria", IDCategoria));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems(int Opcion, int NumeroItem, int Persona, int idTracto, int idCarreta, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NumeroItem", NumeroItem));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@idCarreta", idCarreta));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_ListarRegistros(string Tracto, string Carreta, string Empleado, string Implemento)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitVolcan_ListarRegistros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                cmd.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                cmd.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                cmd.Parameters.Add(new SqlParameter("@Implemento", Implemento));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarDespacho(int CodigoPreviaje, DateTime Fecha, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Previajes_RegistrarDespacho", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@CodigoPreviaje", CodigoPreviaje));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPreviajeR(int NroTicket)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarPreviajeR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
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

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaR(int NroTicket, string Planilla, int idConductorR, decimal GastoRuta, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@NroTicket", NroTicket));
                cmd.Parameters.Add(new SqlParameter("@Planilla", Planilla));
                cmd.Parameters.Add(new SqlParameter("@idConductorR", idConductorR));
                cmd.Parameters.Add(new SqlParameter("@GastoRuta", GastoRuta));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarEditarCT(int Opcion, int idRegistroCT, int idTracto, DateTime FechaRevision,
        string TipoCT, int Cantidad, string Estado, string LugarRevision, int PersonaR, byte[] ImagenHallazgo, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_RegistrarEditarCT", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idRegistroCT", idRegistroCT));
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@FechaRevision", FechaRevision));
                cmd.Parameters.Add(new SqlParameter("@TipoCT", TipoCT));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                cmd.Parameters.Add(new SqlParameter("@LugarRevision", LugarRevision));
                cmd.Parameters.Add(new SqlParameter("@PersonaR", PersonaR));
                if (ImagenHallazgo == null) { cmd.Parameters.AddWithValue("@ImagenHallazgo", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenHallazgo", ImagenHallazgo); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RepararCT(int idRegistroCT, DateTime FechaReparacion, byte[] ImagenReparacion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_RepararCT", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idRegistroCT", idRegistroCT));
                cmd.Parameters.Add(new SqlParameter("@FechaReparacion", FechaReparacion));
                if (ImagenReparacion == null) { cmd.Parameters.AddWithValue("@ImagenReparacion", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenReparacion", ImagenReparacion); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarConosTacos(string FechaInicio, string FechaFin, string Placa, string TipoCT, string Estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarConosTacos", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@TipoCT", TipoCT));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Programacion_EliminarConsolidados(string OT, int CodViaje)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Programacion_EliminarConsolidados", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@OT", OT));
                comando.Parameters.Add(new SqlParameter("@CodViaje", CodViaje));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems(int Opcion, string CodigoItem)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@CodigoItem", CodigoItem));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarImplemento(int Opcion, int idItemL, string CodigoItemAlmacen, string CodigoInterno, int IDCategoria, string Descripcion, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarImplemento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idItemL", idItemL));
                cmd.Parameters.Add(new SqlParameter("@CodigoItemAlmacen", CodigoItemAlmacen));
                cmd.Parameters.Add(new SqlParameter("@CodigoInterno", CodigoInterno));
                cmd.Parameters.Add(new SqlParameter("@IDCategoria", IDCategoria));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems(int Opcion, int NumeroItem, int Persona, int idTracto, int idCarreta, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@NumeroItem", NumeroItem));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@idCarreta", idCarreta));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_ListarRegistros(string Tracto, string Carreta, string Empleado, string Implemento)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_KitLimagas_ListarRegistros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                cmd.Parameters.Add(new SqlParameter("@Carreta", Carreta));
                cmd.Parameters.Add(new SqlParameter("@Empleado", Empleado));
                cmd.Parameters.Add(new SqlParameter("@Implemento", Implemento));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_DesbloqueoTractoXRuta(int Opcion, int idDesbloqueo, int idTracto, DateTime FechaCompromiso, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_DesbloqueoTractoXRuta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idDesbloqueo", idDesbloqueo));
                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@FechaCompromiso", FechaCompromiso));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarUnidadesDesbloqueadas(string Tracto)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarUnidadesDesbloqueadas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Tracto", Tracto));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_EditarKitNeumatico(int idTracto, int Persona, byte[] ImagenKN1, byte[] ImagenKN2, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_ControlItems_EditarKitNeumatico", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@idTracto", idTracto));
                cmd.Parameters.Add(new SqlParameter("@Persona", Persona));
                if (ImagenKN1 == null) { cmd.Parameters.AddWithValue("@ImagenKN1", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenKN1", ImagenKN1); }
                if (ImagenKN2 == null) { cmd.Parameters.AddWithValue("@ImagenKN2", System.Data.SqlTypes.SqlBinary.Null); }
                else { cmd.Parameters.AddWithValue("@ImagenKN2", ImagenKN2); }
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCortinera(int Opcion, int idCortinera, int IdTracto, string Tecle, decimal Cantidad, decimal Capacidad, byte[] Imagen, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCortinera", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCortinera", idCortinera));
                comando.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                comando.Parameters.Add(new SqlParameter("@Tecle", Tecle));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                comando.Parameters.Add(new SqlParameter("@Capacidad", Capacidad));
                if (Imagen == null) { comando.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { comando.Parameters.AddWithValue("@Imagen", Imagen); }
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarCortinera(string Opcion, string Placa, string Estado)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_ListarCortineras", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@Placa", Placa));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarSogas(int Opcion, int idCortineraS, int IdTracto, string EstadoSoga, int Cantidad, byte[] Imagen, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarSogas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCortineraS", idCortineraS));
                comando.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                comando.Parameters.Add(new SqlParameter("@EstadoSoga", EstadoSoga));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                if (Imagen == null) { comando.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { comando.Parameters.AddWithValue("@Imagen", Imagen); }
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarViaticosRuta(string CodGasto, decimal GastoOriginal, int idRutaNueva, decimal NuevoGasto, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_InsertarViaticosRuta", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@GastoOriginal", GastoOriginal));
                comando.Parameters.Add(new SqlParameter("@idRutaNueva", idRutaNueva));
                comando.Parameters.Add(new SqlParameter("@NuevoGasto", NuevoGasto));
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillaRutas(string CodGasto)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_BuscarPlanillaRutas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarHistorialRutas(string Ruta, string FechaInicio, string FechaFin, int IdOperacion)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ListarHistorialRutas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Ruta", Ruta));
                comando.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                comando.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                comando.Parameters.Add(new SqlParameter("@IdOperacion", IdOperacion));
                comando.CommandTimeout = 0;
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch
            { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCamaras(int Opcion, int idCortineraC, int IdTracto, string EstadoCamara, int Cantidad, byte[] Imagen, string Usuario)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCamaras", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCortineraC", idCortineraC));
                comando.Parameters.Add(new SqlParameter("@IdTracto", IdTracto));
                comando.Parameters.Add(new SqlParameter("@EstadoCamara", EstadoCamara));
                comando.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                if (Imagen == null) { comando.Parameters.AddWithValue("@Imagen", System.Data.SqlTypes.SqlBinary.Null); }
                else { comando.Parameters.AddWithValue("@Imagen", Imagen); }
                comando.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ActualizarLiquidaciones(string CodGasto, string NroRUC, string NroDocumento, decimal MontoAfecto, decimal MontoNoAfecto, decimal MontoImpuestos)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_TicketGasto_ActualizarLiquidaciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@CodGasto", CodGasto));
                comando.Parameters.Add(new SqlParameter("@NroRUC", NroRUC));
                comando.Parameters.Add(new SqlParameter("@NroDocumento", NroDocumento));
                comando.Parameters.Add(new SqlParameter("@MontoAfecto", MontoAfecto));
                comando.Parameters.Add(new SqlParameter("@MontoNoAfecto", MontoNoAfecto));
                comando.Parameters.Add(new SqlParameter("@MontoImpuestos", MontoImpuestos));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Capacitaciones_RegistrarEditarDocumentos(int Opcion, int idCapacitacion, string Capacitacion, DateTime Fecha, string Base,
                                                                                          string Instructor, byte[] Archivo, string Titulo, string Extension, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Capacitaciones_RegistrarEditarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
                cmd.Parameters.Add(new SqlParameter("@Capacitacion", Capacitacion));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));
                cmd.Parameters.Add(new SqlParameter("@Base", Base));
                cmd.Parameters.Add(new SqlParameter("@Instructor", Instructor));

                SqlParameter pArchivo = new SqlParameter("@Archivo", SqlDbType.VarBinary, -1);
                if (Archivo == null) { pArchivo.Value = DBNull.Value; }
                else { pArchivo.Value = Archivo; }
                cmd.Parameters.Add(pArchivo);

                cmd.Parameters.Add(new SqlParameter("@Titulo", Titulo));
                cmd.Parameters.Add(new SqlParameter("@Extension", Extension));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Operaciones_Capacitaciones_RegistrarAsistentes(int Opcion, int idCapacitacion, int idCapacitacionD, int idConductor, int Examen, string Estado,
                                                                                    int Teoria, int Revision, int Circuito)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                conexion.Open();
                SqlCommand comando;
                comando = new SqlCommand("ReportesApp_Operaciones_Capacitaciones_RegistrarAsistentes", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                comando.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
                comando.Parameters.Add(new SqlParameter("@idCapacitacionD", idCapacitacionD));
                comando.Parameters.Add(new SqlParameter("@idConductor", idConductor));
                comando.Parameters.Add(new SqlParameter("@Examen", Examen));
                comando.Parameters.Add(new SqlParameter("@Estado", Estado));
                comando.Parameters.Add(new SqlParameter("@Teoria", Teoria));
                comando.Parameters.Add(new SqlParameter("@Revision", Revision));
                comando.Parameters.Add(new SqlParameter("@Circuito", Circuito));
                dtTemp.Load(comando.ExecuteReader());
                return dtTemp;
            }
            catch { return new DataTable(); }
        }

        public DataTable ReportesApp_Operaciones_Capacitaciones_ListarDocumentos(string Titulo, string Conductor, string FechaInicio, string FechaFin)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Capacitaciones_ListarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Titulo", Titulo));
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

        public DataTable ReportesApp_Operaciones_Capacitaciones_ListarAsistentes(int idCapacitacion)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Operaciones_Capacitaciones_ListarAsistentes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@idCapacitacion", idCapacitacion));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_ListarGuiasElectronicas_ListarPendientes(string fechaInicio, string fechaFin, string viaje, string Cliente)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_ListarGuiasElectronicas_ListarPendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaInicio));
                cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaFin));
                cmd.Parameters.Add(new SqlParameter("@Viaje", viaje));
                cmd.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Listar_SerieGuiasManuales()
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_SerieGuiasManuales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_Listar_GuiasManuales(string FechaInicio, string FechaFin, string Serie, string Numero, string Estado)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_Listar_GuiasManuales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@Serie", Serie));
                cmd.Parameters.Add(new SqlParameter("@Numero", Numero));
                cmd.Parameters.Add(new SqlParameter("@Estado", Estado));
                SqlDataReader dr = cmd.ExecuteReader();
                cmd.CommandTimeout = 0;
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }

        public DataTable ReportesApp_GuiasManuales_SolicitarAnularGuias(int Opcion, int idSolicitud, string Serie, string Numero, string Usuario)
        {
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(clsConexion.Instancia.cadenaConexionLocal());
                cmd = new SqlCommand("ReportesApp_GuiasManuales_SolicitarAnularGuias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Opcion", Opcion));
                cmd.Parameters.Add(new SqlParameter("@idSolicitud", idSolicitud));
                cmd.Parameters.Add(new SqlParameter("@Serie", Serie));
                cmd.Parameters.Add(new SqlParameter("@Numero", Numero));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                SqlDataReader dr = cmd.ExecuteReader();
                cmd.CommandTimeout = 0;
                dt.Load(dr);
            }
            catch (Exception ex) { Utilitario.Instancia.Advertencia = ex.Message; }
            finally { cmd.Connection.Close(); }
            return dt;
        }
    }
}



