using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;
using Entidades;

namespace Negocio
{
    public class clsOperacionesBL
    {
        private readonly static clsOperacionesBL instancia = new clsOperacionesBL();

        public static clsOperacionesBL Instancia
        {
            get { return instancia; }
        }
        
        public DataTable GetDataConsolidado(string fini,string ffin,Int32 detallado,Int32 todos,Int32 porfacturar,bool fechaprog) 
        {
            return clsOperacionesDAO.Instancia.GetDataConsolidado(fini, ffin,detallado, todos, porfacturar,fechaprog);
        }

        public DataTable GetDataBonos(int periodo,int tipo)
        {
            return clsOperacionesDAO.Instancia.GetDataBonos(periodo,tipo);
        }

        public DataTable GetDataProduccDiaria(int periodo,string sucursal)
        {
            return clsOperacionesDAO.Instancia.GetDataProduccDiaria(periodo,sucursal);
        }

        public DataTable GetAllDataProduccDiaria(int periodo)
        {
            return clsOperacionesDAO.Instancia.GetAllDataProduccDiaria(periodo);
        }

        public DataTable GetPeriodoBonos() 
        {
            return clsOperacionesDAO.Instancia.GetPeriodoBonos();
        }

        public DataTable GetPeriodoGuiasxEstado()
        {
            return clsOperacionesDAO.Instancia.GetPeriodoGuiasxEstado();
        }

        public DataTable GetPeriodoProduccDiaria()
        {
            return clsOperacionesDAO.Instancia.GetPeriodoProduccDiaria();
        }

        public DataTable GetDataSeguimientoGuias(string fechin, string fechfin, string ser, string num, string est, int tipopara, string param)
        {
            return clsOperacionesDAO.Instancia.GetDataSeguimientoGuias(fechin, fechfin, ser, num, est, tipopara, param);
        }
       
        public DataTable GetDataGuiasxEstadoResumido(int idper, string est)
        {
            return clsOperacionesDAO.Instancia.GetDataGuiasxEstadoResumido(idper, est);
        }

        public DataTable GetDataGuiasxEstadoDetalle(int idper, string est)
        {
            return clsOperacionesDAO.Instancia.GetDataGuiasxEstadoDetalle(idper,est);
        }
        //public DataSet GetDataVencimientoDocs()
        //{
        //    return clsOperacionesDAO.Instancia.GetDataVencimientoDocs();
        //}

        //public DataTable GetDataGastosViajes()
        //{
        //    return clsOperacionesDAO.Instancia.GetDataGastosViajes();
        //}

        public DataTable GetDataAdelantosPlanillas()
        {
            return clsOperacionesDAO.Instancia.GetDataAdelantosPlanillas();
        }
        public DataTable GetGuiasxEntregar()
        {
            return clsOperacionesDAO.Instancia.GetGuiasxEntregar();
        }
        public DataTable GetListaConductores(char activos)
        {
            return clsOperacionesDAO.Instancia.GetListaConductores(activos);
        }

        public DataTable GetOperaciones_Previajes_ListarImpresora(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_ListarImpresora(Usuario);
        }
        
        public DataTable GetOperaciones_Previajes_ListarConductores()
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_ListarConductores();
        }

        public DataTable GetFacturas(string fechaini, string fechafin, string serie,string tipofecha)
        {
            return clsOperacionesDAO.Instancia.GetDataFacturas(fechaini,fechafin,serie,tipofecha);
        }

        public bool UpdateFactura(string nrofactura, int finanzas,int opfinan, int contabilidad,int opconta) 
        {
            return clsOperacionesDAO.Instancia.UpdateFactura(nrofactura, finanzas,opfinan, contabilidad, opconta);
        }

        public DataTable GetFacturasLindley(string fechaini, string fechafin,char tipofecha)
        {
            return clsOperacionesDAO.Instancia.GetFacturasLindley(fechaini, fechafin,tipofecha);
        }

        public DataTable GetViajesTerceros(string fechaini, string fechafin)
        {
            return clsOperacionesDAO.Instancia.GetViajesTerceros(fechaini, fechafin);
        }


        public DataTable GetOperaciones_ActualizarFechaOts(int accion, string ot ,string fecha, string usuario,string FechanAnterior)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ActualizarFechaOts(accion,ot, fecha, usuario, FechanAnterior);
        }

        public DataTable GetUbicacionConductores(string fechaini, string fechafin, string tipo)
        {
            if (tipo == "Trujillo")
            {
                return clsOperacionesDAO.Instancia.GetUbicacionConductoresTrujillo(fechaini, fechafin);
            }
            else
            {
                return clsOperacionesDAO.Instancia.GetUbicacionConductoresFueraTrujillo(fechaini, fechafin);
            }
            
        }

      /*  public DataTable GetUbicacionConductoresFueraTrujillo()
        {
            return clsOperacionesDAO.Instancia.GetUbicacionConductoresFueraTrujillo();
        }*/

        public DataTable GetUbicacionConductoresNoViajaron(string fechaini, string fechafin)
        {
            return clsOperacionesDAO.Instancia.GetUbicacionConductoresNoViajaron(fechaini, fechafin);
        }

        //LISTA DE UNIDADES DISPONIBLES - TRACTOS OPERATIVOS
        public DataTable GetLista_Tractos(string fecha)
        {
            return clsOperacionesDAO.Instancia.GetLista_Tractos(fecha);
        }

        //LISTA DE CONDUCTORES CON TRACTOS Y FECHA PROGRAMADA DE VIAJE
        public DataTable GetLista_Consulta_Tractos(string fecha)
        {
             return clsOperacionesDAO.Instancia.GetLista_Consulta_Tracto_Conductores(fecha);
        }
        /*
        public DataTable GetLista_Consulta_Tractos(string fecha, string tipo)
        {
            if (tipo == "disponible")
            {
                return clsOperacionesDAO.Instancia.GetLista_Consulta_Tracto_Mantenimiento(fecha);
            }
            else
            {
                return clsOperacionesDAO.Instancia.GetLista_Consulta_Tracto_Conductores(fecha);
            }
        }
        */
        public DataTable GetLista_Consultar_Tractos_No_Programados()
        {
            return clsOperacionesDAO.Instancia.GetLista_Consultar_Tractos_No_Programados();
        }        

        public DataTable GetLista_Consulta_Tracto_Mantenimiento(string fecha)
        {
            return clsOperacionesDAO.Instancia.GetLista_Consulta_Tracto_Mantenimiento(fecha);
        }

        public DataTable GetLista_Consultar_Tractos_Tiempo(string fecha)
        {
            return clsOperacionesDAO.Instancia.GetLista_Consultar_Tractos_Tiempo(fecha);
        }

        /*public DataTable GetUnidades_Transito(string fecha)
        {
            return clsOperacionesDAO.Instancia.GetUnidades_Transito(fecha);
        }*/
        public DataTable GetUnidades_Transito(string fechaini, string fechafin)
        {
            return clsOperacionesDAO.Instancia.GetUnidades_Transito(fechaini, fechafin);
        }

        public DataTable GetLista_Viajes(string periodo, string cliente,string ruta, char reporte)
        {
            return clsOperacionesDAO.Instancia.GetLista_Viajes(periodo, cliente, ruta, reporte);
        }

        public DataTable GetLista_Viajes_Detalle(string cliente, string ruta, string fini, string ffin, string producto, string unidad)
        {
            return clsOperacionesDAO.Instancia.GetLista_Viajes_Detalle(cliente, ruta, fini, ffin, producto, unidad);
        }

        public DataTable GetLista_Viajes_Lindley(string cliente, string ruta, string fini, string ffin, string producto, string unidad)
        {
            return clsOperacionesDAO.Instancia.GetLista_Viajes_Lindley(cliente, ruta, fini, ffin, producto, unidad);
        }

        public DataTable GetLista_Operacion_Lindley(string cliente, string ruta, string fini, string ffin, string producto, string unidad)
        {
            return clsOperacionesDAO.Instancia.GetLista_Operacion_Lindley(cliente, ruta, fini, ffin, producto, unidad);
        }

        public DataTable GetLista_Viajes_Por_Periodo(string periodo, string cliente, string ruta, char reporte)
        {
            return clsOperacionesDAO.Instancia.GetLista_Viajes_Por_Periodo(periodo, cliente, ruta, reporte);
        }
        public DataTable GetLista_ListarPeriodosOperaciones()
        {
            return clsOperacionesDAO.Instancia.GetLista_ListarPeriodosOperaciones();
        }

        public DataTable GetViajes_Detallado(string cliente, string ruta, string fini, string ffin, string transporte, string producto, string unidad, int estado_viaje, int TipoTransporte)
        {
            return clsOperacionesDAO.Instancia.GetViajes_Detallado(cliente, ruta, fini, ffin, transporte, producto, unidad, estado_viaje, TipoTransporte);
        }

        public DataTable GetViajes_Detallado_Tipo_Transporte(string cliente, string ruta, string fini, string ffin, string transporte, string producto, string unidad)
        {
            return clsOperacionesDAO.Instancia.GetViajes_Detallado_Tipo_Transporte(cliente, ruta, fini, ffin, transporte, producto, unidad);
        }

        public DataTable GetCantidad_Viajes(string fechaini, string fechafin, string cliente)
        {
            return clsOperacionesDAO.Instancia.GetCantidad_Viajes(fechaini, fechafin, cliente);
        }
        public DataTable GetImpresion_Desembarque_SerieGuias(int Opcion, string Serie)
        {
            return clsOperacionesDAO.Instancia.GetImpresion_Desembarque_SerieGuias(Opcion,Serie);
        }
        public DataTable GetImpresion_Desembarque_GuardaGuiaMasiva(string Serie, string remite_razon, string remite_ruc, string remite_direccion, string remite_distrito, string remite_provincia,
                                                                   string remite_departamento, string Destino_razon, string Destino_ruc, string Destino_direccion, string Destino_distrito
                                                                   , string Destino_provincia, string Destino_departamento, string confvehiculo, string producto, string nave)
        {
            return clsOperacionesDAO.Instancia.GetImpresion_Desembarque_GuardaGuiaMasiva(Serie,  remite_razon,  remite_ruc,  remite_direccion,  remite_distrito,  remite_provincia,
                                                                    remite_departamento,  Destino_razon,  Destino_ruc,  Destino_direccion,  Destino_distrito
                                                                   ,  Destino_provincia,  Destino_departamento,  confvehiculo,  producto,  nave);
        }
        public DataTable GetImpresion_Desembarque_ConsultaRUC(string RUC)
         {
            return clsOperacionesDAO.Instancia.GetImpresion_Desembarque_ConsultaRUC(RUC);
        }
        public DataTable GetOperaciones_GuiasSalaverry_Importar(int Opcion, string FInicio, string FFin,int VerTerceros)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_GuiasSalaverry_Importar(Opcion, FInicio, FFin, VerTerceros);
        }
        public DataTable GetOperaciones_GuiasSalaverry_CreaViaje(string ID, string OT,string partida,string llegada, string USUARIO)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_GuiasSalaverry_CreaViaje(ID, OT,partida,llegada, USUARIO);
        }
        public DataTable GetOperaciones_Programaciones_CreaViaje(int ID, string OT, string partida, string llegada, string USUARIO, decimal montoOst, string ANIO, int TipoProgramacion)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Programaciones_CreaViaje(ID, OT, partida, llegada, USUARIO, montoOst, ANIO, TipoProgramacion);
        }

        public DataTable GetOperaciones_Programaciones_GuardaViajeEstado(int ID, int estado, string codigoviaje, string Comentario, string USUARIO, string anio)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Programaciones_GuardaViajeEstado(ID, estado, codigoviaje,Comentario, USUARIO,anio);
        }               

         public DataTable GetOperaciones_Programaciones_GenerarConsolidados(int codviaje,int idpro, int idot, int dirpartida, int dirllegada, string user, 
                                                                            decimal cantidad,string serie,string numero,string guiaremitente,int idproducto,int idcliente,string medida,int anio)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Programaciones_GenerarConsolidados(codviaje, idpro, idot, dirpartida, dirllegada, user , cantidad
                                                                                                ,serie,numero,guiaremitente,idproducto, idcliente, medida, anio);
        }
         public DataTable GetOperaciones_Programaciones_GuardarConsolidados(int codviaje, int idpro, int idot, int idruta, int idproducto, int idcliente, string unidadMedida, string user,
                                                                           decimal cantidad,string anio)
         {
             return clsOperacionesDAO.Instancia.GetOperaciones_Programaciones_GuardarConsolidados(codviaje, idpro, idot, idruta, idproducto, idcliente,unidadMedida, user,
                                                                            cantidad,anio);
         }
         public DataTable GetViajes_Conductor_BloqueaDesbloquea(int Opcion, int IdBloqueo, int IDPersona, string Motivo, string Usuario, int IdMotivo, int IdDesbloqueo)
         {
             return clsOperacionesDAO.Instancia.GetViajes_Conductor_BloqueaDesbloquea(Opcion, IdBloqueo, IDPersona, Motivo, Usuario, IdMotivo, IdDesbloqueo);
         }
        public DataTable GetViajes_Conductor_VerificaPermisoBloquear(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetViajes_Conductor_VerificaPermisoBloquear(Usuario);
        }

        public DataTable GetViajes_Conductor_VerificaPermisoCompromisoMemo(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetViajes_Conductor_VerificaPermisoCompromisoMemo(Usuario);
        }

        public DataTable GetViajes_Conductor_VerificaPermisoModificarOperacion(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetViajes_Conductor_VerificaPermisoModificarOperacion(Usuario);
        }
        public DataTable GetViajes_Conductor_ListarConductoresBloqueados()
        {
            return clsOperacionesDAO.Instancia.GetViajes_Conductor_ListarConductoresBloqueados();
        }
        public DataTable GetViajes_Conductor_ListarAdministrativos()
        {
            return clsOperacionesDAO.Instancia.GetViajes_Conductor_ListarAdministrativos();
        }

        public DataTable GetOperaciones_ImportarGuiasExcel01(string xml, string usuario,string codigo)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ImportarGuiasExcel01(xml, usuario,codigo);
        }

        public DataTable GetOperaciones_ImportarProgramaciones(string xml, string usuario,string codproga)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ImportarProgramaciones(xml, usuario, codproga);
        }

        public DataTable GetOperaciones_Operaciones_AnexarViajes(string xml, string usuario, string codproga)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Operaciones_AnexarViajes(xml, usuario, codproga);
        }

        

        public DataTable GetOperaciones_Guias_ver(int Opcion)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Guias_ver(Opcion);
        }        
        public DataTable UpdateGuias_Operaciones(string ticket, string guiaRemitente, string observacion,decimal peso)
        {
            return clsOperacionesDAO.Instancia.UpdateGuias_Operaciones(ticket, guiaRemitente, observacion, peso);
        }

        public DataTable GetOperaciones_ListarPreviajeOperaciones(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarPreviajeOperaciones(Usuario);
        }

        public DataTable GetOperaciones_ListarPreviajeOperacionesAccesos()
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarPreviajeOperacionesAccesos();
        }

        

        public DataTable GetOperaciones_ListarViajesConTolvaz(int idtracto,int idconductor, string codigo)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarViajesConTolvaz(idtracto, idconductor, codigo);
        }
        public DataTable GetOperaciones_ListarConsolidados(int codigo)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarConsolidados(codigo);
        }

        public DataTable GetOperaciones_Previajes_ListarColumnas(string usuario)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_ListarColumnas(usuario);
        }

        public DataTable GetOperaciones_ListarDireccionesxOts(int OTS)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarDireccionesxOts(OTS);
        }

        public DataTable GetOperaciones_ListarDatosOts(int OTS)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarDatosOts(OTS);
        }

        public DataTable GetOperaciones_Previaje_ListarColumnasSeleccionadasxUsuario(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Previaje_ListarColumnasSeleccionadasxUsuario(Usuario);
        }

        public DataTable GetOperaciones_Registro_Previajes(int Accion,int IdProgramacion,string Sucursal,int TipoProgramacion, string FechaProgramacion, string FechaInicio, string FechaFin, int idTracto
                                                            , int idSemirremolque, int IdConductor, int IdConductorApoyo, int IdCliente, int IdRuta, string Destino, int Producto,
                                                           int Estado, string HoraSalida, string HoraLlegada, string FechaDescarga, decimal PesoAlmacen, decimal PesoCliente, decimal Merma
                                                            , string CodViaje, string Planilla, string Observacion, string UsuarioCrea,string Serie, string Numero, 
                                                            string GuiaRemitente,string Zona,string Turno,string ConductorInicio,string PrimerCambio, string SegundoCambio,
                                                            string TercerCambio, int Ot, decimal Viaticos, string OrdenServicio, string UnidadApoyo, string guiaEntrega,
                                                            decimal montoOrdenServicio, int dia, int EsInterna, int idPartida, int IdLlegada, string anio, string PlacaMaquinaria,
                                                            string codigoTolvaz,string programacionOrigen,string lineaConsolidado,string NombrePartida, string NombreDestino)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Registro_Previajes(Accion,IdProgramacion,Sucursal, TipoProgramacion, FechaProgramacion, FechaInicio, FechaFin, idTracto
                                                            , idSemirremolque, IdConductor, IdConductorApoyo, IdCliente, IdRuta, Destino ,  Producto,
                                                            Estado, HoraSalida, HoraLlegada, FechaDescarga,  PesoAlmacen, PesoCliente, Merma
                                                            ,CodViaje, Planilla, Observacion, UsuarioCrea,Serie,Numero, GuiaRemitente,Zona,Turno, ConductorInicio, PrimerCambio,
                                                            SegundoCambio, TercerCambio, Ot, Viaticos, OrdenServicio, UnidadApoyo, guiaEntrega, montoOrdenServicio,dia,EsInterna,
                                                            idPartida, IdLlegada, anio, PlacaMaquinaria,codigoTolvaz,programacionOrigen,lineaConsolidado, NombrePartida, NombreDestino);
        }
        public DataTable GetOperaciones_ListarProgramaciones(string sucursal,string FechaPrograInicio,string FechaProgFin, int pro1,int prog2,int prog3,int prog4,int prog5)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarProgramaciones(sucursal, FechaPrograInicio, FechaProgFin, pro1, prog2, prog3, prog4, prog5);
        }

        public DataTable GetOperaciones_ListarProgramacionesPorProgramacion(string sucursal, string FechaPrograInicio, string FechaProgFin, int pro1)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarProgramacionesPorProgramacion(sucursal, FechaPrograInicio, FechaProgFin, pro1);
        }

        public DataTable GetOperaciones_ListarSucursalPreviajes(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarSucursalPreviajes(Usuario);
        }

        public DataTable GetOperaciones_Operaciones_ListarViajesAltra(string codigo)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Operaciones_ListarViajesAltra(codigo);
        }
        
        public DataTable GetOperaciones_ListarCodigosVicncular()
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarCodigosVicncular();
        }
        public DataTable GetOperaciones_Previajes_PrograPendientes(string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_PrograPendientes(Usuario);
        }

        
        public DataTable GetOperaciones_Previajes_AnularViajes(string idviaje, int idProg, string Observacion, string Usuario,string anio, int tipoPro)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_AnularViajes(idviaje, idProg, Observacion, Usuario, anio, tipoPro);
        }

        public DataTable GetOperaciones_Programaciones_ValidarUnidad(int idUnidad,string fechainicio)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Programaciones_ValidarUnidad(idUnidad, fechainicio);
        }

        public DataTable GetOperaciones_Programaciones_DcunetosVencidos(int IDRELACION, string TIPORELACION)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(IDRELACION, TIPORELACION);
        }
        
       public DataTable GetOperaciones_ListarUbicacionGps(string placa)
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_ListarUbicacionGps(placa);
        }


       public DataTable GetOperaciones_Previajes_ListarAccesos()
        {
            return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_ListarAccesos();
        }
       public DataTable GetOperaciones_PreviajesAgregaPeso(int Accion, int idProgPeso, decimal Peso, string Observacion,string anio)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_PreviajesAgregaPesoObservacion( Accion,  idProgPeso,  Peso, Observacion,anio);
       }
       public DataTable GetOperaciones_PreviajesAgregaAcceso(string user, int programacion, string sucursal, int accesonivel)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_PreviajesAgregaAcceso( user,  programacion,  sucursal,  accesonivel);
       }

       public DataTable GetOperaciones_Conductor_AsignarOperacion(int IdPersona, int IdOperacion)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_Conductor_AsignarOperacion(IdPersona, IdOperacion);
       }

       public DataTable GetOperaciones_ListarUnidadesActivas()
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_ListarUnidadesActivas();
       }

       public DataTable GetOperaciones_ListarUnidadesBloqueadas(string Placa, string TipoVehiculo, string Operacion)
       { return clsOperacionesDAO.Instancia.GetOperaciones_ListarUnidadesBloqueadas(Placa, TipoVehiculo, Operacion); }

       public DataTable GetOperaciones_Previajes_ListarPrecios(int idvehiculo,int ruta, int producto, string sucursal)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_ListarPrecios(idvehiculo,ruta, producto, sucursal);
       }

       public DataTable GetOperaciones_ListarPreviajeOtsDisponibles()
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_ListarPreviajeOtsDisponibles();
       }
       public DataTable GetOperaciones_Programaciones_GuardarDestino(int opcion, int idot, string destino, decimal km, int dia, string zona, int idRuta, int idTipoProgramacion)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_Programaciones_GuardarDestino(opcion, idot, destino, km, dia, zona, idRuta, idTipoProgramacion);
       }

       public DataTable GetOperaciones_PreviajesCambiarPosicion(int IdProgramaCambiar,int CodigoCambiar,int CodigoCambiaRecibe,int IdProgramaCambiaRecibe)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_PreviajesCambiarPosicion(IdProgramaCambiar, CodigoCambiar, CodigoCambiaRecibe, IdProgramaCambiaRecibe);
       }


       public DataTable Obtener_Lista_ConductoresBloqueados()
       {
           return clsOperacionesDAO.Instancia.Obtener_Lista_ConductoresBloqueados();
       }

       public DataTable GetOperaciones_ListarPreviajeImpreso(string CODIGO)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_ListarPreviajeImpreso(CODIGO);
       }

       public DataTable ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(int Opcion, int TipoVehiculo)
       { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(Opcion, TipoVehiculo); }

       public DataTable GetOperaciones_ListarUnidadesConductor(int TipoVehiculo, int SubTipoVehiculo, int idOperacion, string Placa)
       { return clsOperacionesDAO.Instancia.GetOperaciones_ListarUnidadesConductor(TipoVehiculo, SubTipoVehiculo, idOperacion, Placa); }

       public DataTable ReportesApp_Operaciones_ConductorUnidades_FiltrarUnidad(int IdVehiculo)
       { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ConductorUnidades_FiltrarUnidad(IdVehiculo); }

       public DataTable GetOperaciones_Operaciones_UnidadesConductor_CreaModifica(int Accion, int nroregisrto, int idunidad, int Operacion, string Observacion, string Usuario, int Mochila,
                        int tomafuerza, int urea, int manguera, int Senaletica, int LlaveOriginal, int LlaveDuplicada, int Camaras, string Transmision, decimal Peso, decimal Galones, string TipoCortina,
                        string ModeloChasis, string Nivel, string TipoNivel, string Suspension, string Piso, string MaterialPiso, string Piso2, string MaterialPiso2, int NroLlantas, string Planos,
                        string Vitacora, string Bocamaza)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_Operaciones_UnidadesConductor_CreaModifica(Accion, nroregisrto, idunidad, Operacion, Observacion, Usuario, Mochila, tomafuerza, urea, manguera,
                                              Senaletica, LlaveOriginal, LlaveDuplicada, Camaras, Transmision, Peso, Galones, TipoCortina, ModeloChasis, Nivel, TipoNivel, Suspension, Piso, MaterialPiso, Piso2,
                                              MaterialPiso2, NroLlantas, Planos, Vitacora, Bocamaza);
       }

       public DataTable Obtener_Lista_HistoricoDesbloqueos(string fechaini, string fechafin, string persona, int ValFecha, int ValMotivo, int IdMotivo, int filtro)
       {
           return clsOperacionesDAO.Instancia.Obtener_Lista_HistoricoDesbloqueos(fechaini, fechafin, persona, ValFecha, ValMotivo, IdMotivo, filtro);
       }

       public DataTable Obtener_Lista_ProgramacionesAnulados(string fechaini, string fechafin)
       {
           return clsOperacionesDAO.Instancia.Obtener_Lista_ProgramacionesAnulados(fechaini, fechafin);
       }

       public DataTable Llenar_ControlesPreViajes()
       {
           return clsOperacionesDAO.Instancia.Llenar_ControlesPreViajes();
       }

       public DataTable Cambiar_PosicionPreviaje(int IdPreviajeActual, int IdPreviajeNuevo, int EsMenorMayor, string Usuario,string anio)
       {
           return clsOperacionesDAO.Instancia.Cambiar_PosicionPreviaje(IdPreviajeActual, IdPreviajeNuevo, EsMenorMayor, Usuario,anio);
       }

       public DataTable Cambiar_Observacion_Previajes(int IdPreviaje, string Observacion,  string Usuario)
       {
           return clsOperacionesDAO.Instancia.Cambiar_Observacion_Previajes(IdPreviaje, Observacion, Usuario);
       }

       public DataTable GetOperaciones_Previajes_ActualizarImpresion(string codigo)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_ActualizarImpresion(codigo);
       }
        
       public DataTable GetOperaciones_PreviajesAgregaGuias(int idprogramacion,string guiatrans, string guiaremision, string guientrega,string usuario,string anio,
                                                            int ot, int tipoprogramacion,int dirPartida , int dirDestino,decimal pesoCarga,decimal PesoCliente,
                                                            decimal PesoCombustible, string transportista2, bool esRetorno)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_PreviajesAgregaGuias(idprogramacion, guiatrans, guiaremision, guientrega, usuario, anio, ot,
                                              tipoprogramacion, dirPartida, dirDestino, pesoCarga, PesoCliente, PesoCombustible, transportista2, esRetorno);
       }

       public DataTable GetOperaciones_Previajes_RegistroFiltroColumnas(string cadena, string usuario)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_Previajes_RegistroFiltroColumnas(cadena, usuario);
       }

       public DataTable GetOperaciones_ListarTipoProgramaciones()
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_ListarTipoProgramaciones();
       }

       public DataTable GetOperaciones_ListarUnidades(string Placa, string TipoVehiculo, string Operacion)
       { return clsOperacionesDAO.Instancia.GetOperaciones_ListarUnidades(Placa, TipoVehiculo, Operacion); }

       public DataTable GetOperaciones_ListarMotivoBloqueo(string Usuario)
       {
           return clsOperacionesDAO.Instancia.GetOperaciones_ListarMotivoBloqueo(Usuario);
       }

       public DataTable GetLista_Consulta_Permiso_BloqueoDesbloqueo(string Usuario)
       {
           return clsOperacionesDAO.Instancia.GetLista_Consulta_Permiso_BloqueoDesbloqueo(Usuario);
       }

       public DataTable GetLista_Consulta_Permiso_AsignarUnidadesConductor(string Usuario)
       {
           return clsOperacionesDAO.Instancia.GetLista_Consulta_Permiso_AsignarUnidadesConductor(Usuario);
       }

       public DataTable GetLista_Operaciones_Clientes_Listar(char tipo,int IdPersona)
       {
           return clsOperacionesDAO.Instancia.GetLista_Operaciones_Clientes_Listar(tipo, IdPersona);
       }

       public DataTable GetLista_Operaciones_Clientes_ListarContactos(char tipo, int IdPersona, int IdCompania)
       {
           return clsOperacionesDAO.Instancia.GetLista_Operaciones_Clientes_ListarContactos(tipo, IdPersona, IdCompania);
       }

       public DataTable GetLista_Operaciones_Clientes_ContactosListar(int IdPersona, char tipo, int Opcion)
       {
           return clsOperacionesDAO.Instancia.GetLista_Operaciones_Clientes_ContactosListar(IdPersona, tipo, Opcion);
       }

         public DataTable GetLista_Operaciones_Clientes_Contactos_AgregarModificarQuitar(int accion,char tipo,int idregistro,int idpersona,int idcontacto,int ContactoDetalle,string nombre,string cargo,
                                                                                        string telefono,string celular,string correo,string direccion,string lugar,string Usuario)
        {
            return clsOperacionesDAO.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarModificarQuitar(accion, tipo, idregistro, idpersona, idcontacto, ContactoDetalle, nombre, cargo,
                                                                                         telefono, celular, correo, direccion, lugar, Usuario);
        }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos(int Accion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos(Accion);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContrato(int Accion, char Tipo, int IdContrato, int IdPersona, int IdContacto, int IdContactoDetalle, int IdTipoContrato, string Titulo, string Descripcion, string Consideracion,
                                                                                             string Beneficios, DateTime FechaInicio, DateTime FechaFin, int DiasAlerta, int Todos, string xml, string RutaEnlace, Byte Validacion, string Usuario)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContrato(Accion, Tipo, IdContrato, IdPersona, IdContacto, IdContactoDetalle, IdTipoContrato, Titulo, Descripcion, Consideracion, Beneficios,
                                                                                                                   FechaInicio, FechaFin, DiasAlerta, Todos, xml, RutaEnlace, Validacion, Usuario);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarContrato(int IdContrato)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarContrato(IdContrato);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarAreasInvolucradas(int IdContrato)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarAreasInvolucradas(IdContrato);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_FiltrarContratos(char TipoPersonal, string NombrePersonal, string FechaInicio, string FechaFin, int IdCompania, Byte Validacion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_FiltrarContratos(TipoPersonal, NombrePersonal, FechaInicio, FechaFin, IdCompania, Validacion);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_AnularContratos(int IdContrato, byte Validacion, string Usuario)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_AnularContratos(IdContrato, Validacion, Usuario);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_InsertarAdenda(int IdContrato, string Titulo, string Descripcion, string Consideracion, string Beneficios, DateTime FechaInicio, DateTime FechaFin, int DiasAlerta, string RutaEnlace, string Usuario)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_InsertarAdenda(IdContrato, Titulo, Descripcion, Consideracion, Beneficios, FechaInicio, FechaFin, DiasAlerta, RutaEnlace, Usuario);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_ListarAdendas(int Opcion, int IdContrato)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarAdendas(Opcion, IdContrato);
         }

         public DataTable ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas(int Opcion, int idAdenda, int IdContrato)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas(Opcion, idAdenda, IdContrato);
         }

         public DataTable GetViajes_ClienteProveedor_VerificaPermisoModificar(string Usuario)
         {
             return clsOperacionesDAO.Instancia.GetViajes_ClienteProveedor_VerificaPermisoModificar(Usuario);
         }

         public DataTable GetLista_Operaciones_Clientes_Contactos_AgregarCabcera(int accion, char tipo, int idregistro, int idpersona, int IdCompania, string Observacion, string Usuario)
         {
             return clsOperacionesDAO.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarCabcera(accion, tipo, idregistro, idpersona, IdCompania, Observacion, Usuario);
         }

         public DataTable GetDataListarUsuariocrearMemos(string Usuario)
         {
             return clsOperacionesDAO.Instancia.GetDataListarUsuariocrearMemos(Usuario);
         }

         public DataTable GetPeriodoRRHH()
         {
             return clsOperacionesDAO.Instancia.GetPeriodoRRHH();
         }


         public DataTable GetVerMermasxConductor(int IdConductorDetalle, string FechaInicioDetalle, string FechaFinDetalle)
         {
             return clsOperacionesDAO.Instancia.GetVerMermasxConductor(IdConductorDetalle, FechaInicioDetalle, FechaFinDetalle);
         
         }

         public DataTable GetMoverProgramaciones(int idProgramacion, int tipoProgramacion,int tipoPrograMover, string Usuario)
         {
             return clsOperacionesDAO.Instancia.GetMoverProgramaciones(idProgramacion, tipoProgramacion, tipoPrograMover, Usuario);
         }

         public DataTable GetOperacionesMover()
         {
             return clsOperacionesDAO.Instancia.GetOperacionesMover();
         }
        ///tracking-03-11-2022
         public DataTable GetPreviajes_ListarTickets(string NroTicketPreViaje)
         {
             return clsOperacionesDAO.Instancia.GetPreviajes_ListarTickets(NroTicketPreViaje);
         }

         public DataTable GetPreviajes_ListarEstadoPorOperacion(int tipoOperacion)
         {
             return clsOperacionesDAO.Instancia.GetPreviajes_ListarEstadoPorOperacion(tipoOperacion);
         }

         public DataTable GetPreviajes_ListarEventoPorOperacion(int TipoBusqueda, int tipoOperacion,int idprogramacion,int anio,string IdProducto)
         {
             return clsOperacionesDAO.Instancia.GetPreviajes_ListarEventoPorOperacion(TipoBusqueda, tipoOperacion, idprogramacion, anio, IdProducto);
         }
         public DataTable GetPreviajes_RegistrarTracking(int accion, string Usuario, int idprogramacion, int IdTipoProgramacion, string RegistroFechaEvento, 
                                                        string anio, string tipocarga, string ubicacion, string codigoevento, string estadotracking, int idtracking,
                                                        string ubicacionGPS, string UbicacionGeo, int idpunto, int TipoViaje, string IdProducto, int SeguimientoxProducto, string Item)
         {
             return clsOperacionesDAO.Instancia.GetPreviajes_RegistrarTracking(accion, Usuario, idprogramacion, IdTipoProgramacion, RegistroFechaEvento, anio, tipocarga, 
                                                                                            ubicacion, codigoevento, estadotracking, idtracking, ubicacionGPS, UbicacionGeo, 
                                                                                            idpunto, TipoViaje, IdProducto,  SeguimientoxProducto, Item);
         }

         public DataTable GetOperaciones_ListarUbicacion(string Unidad, string fecharegistro)
         {
             return clsOperacionesDAO.Instancia.GetOperaciones_ListarUbicacion(Unidad, fecharegistro);
         }
         public DataTable GetOperaciones_ListarTrackingEditar(int idTracking)
         {
             return clsOperacionesDAO.Instancia.GetOperaciones_ListarTrackingEditar(idTracking);
         }
         public DataTable GetPreviajes_ListarTracking(int var_IdProg,string anio)
         {
             return clsOperacionesDAO.Instancia.GetPreviajes_ListarTracking(var_IdProg,anio);
         }

         public DataTable GetPuntosPorRutas(string _punto)
         {
             return clsOperacionesDAO.Instancia.GetPuntosPorRutas(_punto);
         }

         public DataTable GetListarDetalleProgramacion(int idprogramacion)
         {
             return clsOperacionesDAO.Instancia.GetListarDetalleProgramacion(idprogramacion);
         }

         public DataTable GetOperaciones_ListarPorCliente(int IdOt,int Anio,int idProgramacion)
         {
             return clsOperacionesDAO.Instancia.GetOperaciones_ListarPorCliente(IdOt, Anio, idProgramacion);
         }
        ///tracking-03-11-2022

         public DataTable GetGuiasxEntregarConductor(int opcion)
         {
             return clsOperacionesDAO.Instancia.GetGuiasxEntregarConductor(opcion);
         }

         public DataTable GetOperaciones_ListarReporteTracking(int idprogramacion,string FechaInicio,string FechaFin )
         {
             return clsOperacionesDAO.Instancia.GetOperaciones_ListarReporteTracking( idprogramacion,FechaInicio ,FechaFin );
         }


         public DataTable GetDataPermisoRegitroExcepciones(string Usuario)
         {
             return clsOperacionesDAO.GetDataPermisoRegitroExcepciones(Usuario);
         }

         public DataTable GetLista_Operaciones_Destinos(int idruta)
         {
             return clsOperacionesDAO.GetLista_Operaciones_Destinos(idruta);
         }

         public DataTable GetLista_Operaciones_Previajes_PorCompletar(int Opcion, string Usuario)
         {
             return clsOperacionesDAO.GetLista_Operaciones_Previajes_PorCompletar(Opcion, Usuario);
         }

         public DataTable GetDataActualizarOrdenDestino(string cadena, string Usuario, int idoperacion)
         {
             return clsOperacionesDAO.Instancia.GetDataActualizarOrdenDestino(cadena, Usuario, idoperacion);
         }

         public DataTable GetLista_Operaciones_DestinosListar(int IdOperacion)
         {
             return clsOperacionesDAO.Instancia.GetLista_Operaciones_DestinosListar(IdOperacion);
         }


         public DataTable GetDataReporteAsistenciaCondutores(string FechaInicio, string FechaFin, int Cesados)
         {
             return clsOperacionesDAO.Instancia.GetDataReporteAsistenciaCondutores(FechaInicio, FechaFin, Cesados);
         }


         public DataTable GetDataReporteAsistenciaxCompensar(int Cesados)
         {
             return clsOperacionesDAO.Instancia.GetDataReporteAsistenciaxCompensar(Cesados);
         }

         public DataTable GetDataReporteAsistenciaVacaciones(string TipoPermiso)
         {
             return clsOperacionesDAO.Instancia.GetDataReporteAsistenciaVacaciones(TipoPermiso);
         }

         public DataTable GetDataBuscarGuiasModificar(int busqueda,string guiaregistrada, int idticket, string usuario)
         {
             return clsOperacionesDAO.Instancia.GetDataBuscarGuiasModificar(busqueda,guiaregistrada, idticket, usuario);
         }




         public static DataTable GetDataGuardarGuia(int ID, string guiaregistrada, string Compania, string Placa, string Carreta, string Ruc, string DniConductor, int importado, 
                                                    string Serie, string Numero, string Gr, string CodProgram, string Ticket, string FhTicket, decimal PesoPuerto, string UNM, 
                                                    decimal CantidadBase, string Observacion, int Ot, string Usuario)
         {
             return clsOperacionesDAO.Instancia.GetDataBuscarGuiasModificar( ID,  guiaregistrada,  Compania,  Placa,  Carreta,  Ruc,  DniConductor,  importado, 
                                                     Serie,  Numero,  Gr,  CodProgram,  Ticket,  FhTicket,  PesoPuerto,  UNM, 
                                                     CantidadBase,  Observacion,  Ot,  Usuario);
         }

         public DataTable GetOperaciones_Programaciones_ValidardatoGuias(string dato, string tipo)
         {
             return clsOperacionesDAO.Instancia.GetDataReporteAsistenciaVacaciones(dato, tipo);
         }

        #region Guias Electronicas
         public DataTable ListarTipoGuiaElectronica()
         {
             return clsOperacionesDAO.Instancia.ListarTipoGuiaElectronica();
         }

         public DataTable ReportesApp_ListarEmpresasGrupo()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarEmpresasGrupo();
         }

         public DataTable ReportesApp_ListarClientes_GuiaElectronica(String NombreEmpresa)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarClientes_GuiaElectronica(NombreEmpresa);
         }

         public DataSet ReportesApp_Listar_Departamentos_Provincias_Ciudades()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Listar_Departamentos_Provincias_Ciudades();
         }

         public DataTable ReportesApp_ListarCorreos_Master(int Persona,string direccionDestino)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarCorreos_Master(Persona,direccionDestino);
         }

         public Boolean ReportesApp_Nuevo_Editar_Anular_Correo_Empresa_GuiasElectronicas(int Persona, string correo, int tipoOperacion, string DireccionDestino, bool principal, int idcorreo = -1)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_NuevoCorreo_Empresa_GuiasElectronicas(Persona, correo, tipoOperacion, DireccionDestino, principal,idcorreo);
         }

         public DataTable ReportesApp_ListarUnidadMedida_Sunat()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarUnidadMedida_Sunat();
         }

         public DataTable ReportesApp_Informacion_Conductor_GuiaElectronica(int idConductor)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Informacion_Conductor_GuiaElectronica(idConductor);
         }


         public DataTable ReportesApp_Listar_TipoDocumentoFiscal_GuiaElectronica()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Listar_TipoDocumentoFiscal_GuiaElectronica();
         }


         public DataTable ReportesApp_Listar_SerieGuiasElectronicas(string TipoGuia)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Listar_SerieGuiasElectronicas(TipoGuia);
         }

         public DataTable ReportesApp_ListarRuta_GuiaElectronica(string nombreRuta)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarRuta_GuiaElectronica(nombreRuta);
         }

         public DataTable ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(int idEmpresa)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarDireccionesEmpresa_GuiaEelectronica(idEmpresa);
         }

         public DataTable ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(int idEmpresa, int idRuta,int idRemitente, int idDestinatario)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(idEmpresa, idRuta, idRemitente, idDestinatario);
         }


         public DataTable ReportesApp_ListarOtsXProgramacion(int idProgramacion, int Anio, string TipoProgramacion,int idViaje)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarOtsXProgramacion(idProgramacion, Anio, TipoProgramacion,idViaje);
         }

         public DataTable ReportesApp_ListarInformacion_ClienteProgramacion(int idCliente)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarInformacion_ClienteProgramacion(idCliente);
         }

         public DataTable ReportesApp_TipoServicioGuiaElectronica(string tipoGuia)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_TipoServicioGuiaElectronica(tipoGuia);
         }

         public Boolean ReportesApp_RegistrarGuiaElectronica(ref clsGRT entGuiaTransportista)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_RegistrarGuiaElectronica(ref entGuiaTransportista);
         }

         public Boolean ReportesApp_RegistrarGuiaElectronica(ref clsGRR entGuiaTransportista)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_RegistrarGuiaElectronica(ref entGuiaTransportista);
         }


         public DataTable ObtenerCorrelativoGuiaTransportistas(string empresa, string tipoGuia, string serie)
         {
             return clsOperacionesDAO.Instancia.ObtenerCorrelativoGuiaTransportistas(empresa, tipoGuia, serie);
         }

         public DataTable ObtenerFechaHoraServidorGuiaElectronica()
         {
             return clsOperacionesDAO.Instancia.ObtenerFechaHoraServidorGuiaElectronica();
         }

         public Boolean GuardarRespuestaSunat(clsGRT entGuiaTransportista)
         {
             return clsOperacionesDAO.Instancia.GuardarRespuestaSunat(entGuiaTransportista);
         }

         public Boolean GuardarRespuestaSunat(clsGRR entGuiaRemitente)
         {
             return clsOperacionesDAO.Instancia.GuardarRespuestaSunat(entGuiaRemitente);
         }

         public DataTable ReportesApp_BuscarPlaca_GuiaElectronica(string placa)
         { return clsOperacionesDAO.Instancia.ReportesApp_BuscarPlaca_GuiaElectronica(placa); }

         public DataTable ReportesApp_BuscarPlaca_TarjetaCirculacion(string Placa, int TipoDocumento)
         { return clsOperacionesDAO.Instancia.ReportesApp_BuscarPlaca_TarjetaCirculacion(Placa, TipoDocumento); }

         public DataTable ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(int idPlaca)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_BuscarTarjetaCirculacion_GuiaElectronica(idPlaca);
         }

         public DataTable ReportesApp_ListarGuiasElectronicas(string tipoGuia, string fechaInicio, string fechaFin, string Serie, string Numero, bool todos, bool estadoAprobado, bool estadoRevertido, bool estadoRechazado ,string viaje,string Cliente)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarGuiasElectronicas(tipoGuia, fechaInicio, fechaFin, Serie, Numero, todos, estadoAprobado, estadoRevertido, estadoRechazado, viaje, Cliente);
         }

         public Boolean ReportesApp_GuardarReversion(clsGRT ent_Transportista)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_GuardarReversion(ent_Transportista);
         }

         public DataTable ReportesApp_ListarUbigeo_GuiaElectronica(string ciudad)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarUbigeo_GuiaElectronica(ciudad);
         }

         public DataTable ReportesApp_BuscarProducto_GuiaElectronica(string producto)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_BuscarProducto_GuiaElectronica(producto);
         }

         public DataTable ReportesApp_ListarMotivoTraslado_GuiaElectronica()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarMotivoTraslado_GuiaElectronica();
         }

         public DataTable ReportesApp_ListarModalidadTransporte()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarModalidadTransporte();
         }

         public DataTable ReportesApp_Listar_TipoDocumentoIdentidad_GuiaElectronica()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Listar_TipoDocumentoPersona_Guia_Electronica();
         }

         public bool ReportesApp_GuardarReversionRemitente(clsGRR entGuiaRemitente)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_GuardarReversionRemitente(entGuiaRemitente);
         }

         public DataTable ReportesApp_ListarEstablecimientos_Anexos(string establecimiento)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarEstablecimientos_Anexos(establecimiento);
         }

        #endregion  

         public DataTable ReportesApp_ListarHistorialRespuestaSunat_GuiasElectronicas(string empresa, int idCliente, int idOt, string TipoGuia, int idGuiaElectronica)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarHistorialRespuestaSunat_GuiasElectronicas(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica);
         }

         public bool Reportesapp_Operaciones_Reimprimir_GuiaElectronica(string empresa, int idCliente, int idOt, string TipoGuia, int idGuiaElectronica)
         {
             return clsOperacionesDAO.Instancia.Reportesapp_Operaciones_Reimprimir_GuiaElectronica(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica);
         }

         public bool ReportesApp_Operaciones_RegistrarAnularSerieGuiaElectronica(string tipoguia, string serie, string empresa, string descripcion, int accion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RegistrarAnularSerieGuiaElectronica(tipoguia, serie, empresa, descripcion, accion);
         }

         public DataTable ReportesApp_Operaciones_ListarMaestroSeriesGuiasElectronicas(bool estado)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarMaestroSeriesGuiasElectronicas(estado);
         }

         public bool ReportesApp_ConfirmarAnulacionSunat(string empresa, int idCliente, int idOt, string TipoGuia, int idGuiaElectronica)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ConfirmarAnulacionSunat( empresa,  idCliente,  idOt,  TipoGuia,  idGuiaElectronica);
         }

         public DataTable ReportesApp_Listar_Series_Por_Establecimientos_Anexos()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Listar_Series_Por_Establecimientos_Anexos();
         }

         public DataTable ReportesApp_Listar_Establecimientos_Vinculados_Series(string serie, string tipoguia, string empresa)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Listar_Establecimientos_Vinculados_Series(serie, tipoguia, empresa);
         }

         public bool ReportesApp_Actualizar_EstablecimientosAnexados_Serie(string serie, string tipoguia, string empresa, string codigo, string descripcion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Actualizar_EstablecimientosAnexados_Serie(serie, tipoguia, empresa, codigo, descripcion);
         }

         public bool ReportesApp_Operaciones_VincularSeriesPorUsuario(string empresa, string tipoguia, string serie, int idUsuario, string Nombre, int Accion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_VincularSeriesPorUsuario(empresa, tipoguia, serie, idUsuario, Nombre, Accion);
         }

         public DataTable ReportesApp_Operaciones_ListarUsuariosVinculadosxSeries(string serie, string empresa, string tipoguia)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarUsuariosVinculadosxSeries(serie, empresa, tipoguia);
         }

         public DataTable ReportesApp_ListarMotivoBloqueo()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarMotivoBloqueo();
         }

         public DataTable ReportesApp_ListarEstadoDesbloqueo()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_ListarEstadoDesbloqueo();
         }

         public bool ReportespApp_Operaciones_PlacaTercero_GuiaElectronica_RegistrarEliminar(int idCliente, string nombreCLiente, string placa, string tarjetaCirculacion, string TipoVehiculo, int accion)
         {
             return clsOperacionesDAO.Instancia.ReportespApp_Operaciones_PlacaTercero_GuiaElectronica_RegistrarEliminar(idCliente, nombreCLiente, placa, tarjetaCirculacion, TipoVehiculo, accion);
         }

         public DataTable ReportesApp_Operaciones_ListarUnidadesTerceros_GuiaElectronica()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarUnidadesTerceros_GuiaElectronica();
         }

         public DataTable ReportesApp_Operaciones_ListarPlacasTercero_porCliente(int idCliente)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarPlacasTercero_porCliente(idCliente);
         }

         public bool ReportesAPP_RegistrarAnular_ConductoresTerceros(int idcliente, string nombres, string apellidos, int CodigotipoDocumento, string nombreTipoDocumento, string txtDocumento, string txtLiencencia, int accion)
         {
             return clsOperacionesDAO.Instancia.ReportesAPP_RegistrarAnular_ConductoresTerceros(idcliente, nombres, apellidos, CodigotipoDocumento, nombreTipoDocumento, txtDocumento, txtLiencencia, accion);
         }

         public DataTable ReportesApp_Operaciones_ListarConductoresTercerosGuiaElectronica(int idCliente)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarConductoresTercerosGuiaElectronica(idCliente);
         }

         public bool ReportesApp_Operaciones_RegistrarEmpresaxCodigoMTC(int idCliente, string nombreCliente, string numerMTC,int accion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RegistrarEmpresaxCodigoMTC(idCliente, nombreCliente, numerMTC,accion);
         }

         public DataTable ReportesApp_Operaciones_ListarEmpresaxCodigoMTC()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarEmpresaxCodigoMTC();
         }

         public bool Reportesapp_Operaciones_EliminarRegistroGuiasElectronicas(string empresa, int idCliente, int idOt, string TipoGuia, int idGuiaElectronica, string serie)
         {
             return clsOperacionesDAO.Instancia.Reportesapp_Operaciones_EliminarRegistroGuiasElectronicas(empresa, idCliente, idOt, TipoGuia, idGuiaElectronica,serie);
         }

         public DataTable ReportesApp_Operaciones_ListarMotivo()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarMotivo();
         }

         public DataTable ReportesApp_Operaciones_ListarAsume()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarAsume();
         }

         public DataTable ReportesApp_Operaciones_ListarEstado()
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarEstado();
         }

         public DataTable ReportesApp_Operaciones_ListarClientes(string Filtro)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarClientes(Filtro);
         }

         public DataTable ReportesApp_Operaciones_ListarClientesReclamos(string Filtro)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarClientesReclamos(Filtro);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Insertar(int NroProgramacion, string Usuario, int idMotivo)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Insertar(NroProgramacion, Usuario, idMotivo);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro(int NroProgramacion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro(NroProgramacion);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Listar(string NombreConductor, string FechaInicio, string FechaFin, int idEstado)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Listar(NombreConductor, FechaInicio, FechaFin, idEstado);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ListarTodos(string NombreConductor, string FechaInicio, string FechaFin)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarTodos(NombreConductor, FechaInicio, FechaFin);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ListarPreviaje(int NroProgramacion)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarPreviaje(NroProgramacion);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Modificar(int NroProgramacion, string Descripcion, string Factura, int idAsume, decimal Monto, string Moneda, int idEstado, string Comentarios, string Usuario)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Modificar(NroProgramacion, Descripcion, Factura, idAsume, Monto, Moneda, idEstado, Comentarios, Usuario);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_InsertarFaltanteSinViaje(DateTime FechaIncidente, string Cliente, int idMotivo, string Usuario)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_InsertarFaltanteSinViaje(FechaIncidente, Cliente, idMotivo, Usuario);
         }

         public DataTable ReportesApp_Operaciones_Previajes_Faltantes_Cerrar(int NroProgramacion, string Usuario)
         {
             return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Cerrar(NroProgramacion, Usuario);
         }

        /*
         public bool Reportesapp_Operaciones_Registrar_Editar_Eliminar_Tolvas(clsPreviajeTolvas tolvas)
         {
             return clsOperacionesDAO.Instancia.Reportesapp_Operaciones_Registrar_Editar_Eliminar_Tolvas(tolvas);
         }
         */

         public bool ReportesaApp_Operaciones_MaestroClienteRuta_GuiaElectronica(string idCliente, string Cliente,string idRemitente,string Remitente, string idDestinatario, string Destinatario, string idRuta, string Ruta, string DireccionPartida, string DirecconDeestino, string ubigeoPartida, string ubigeoDestino,int SecuenciaOrigen,int SecuenciaDestino, int TipoOperacion)
         {
             return clsOperacionesDAO.Instancia.ReportesaApp_Operaciones_MaestroClienteRuta_GuiaElectronica(idCliente, Cliente,idRemitente,Remitente, idDestinatario, Destinatario, idRuta, Ruta, DireccionPartida, DirecconDeestino, ubigeoPartida, ubigeoDestino, SecuenciaOrigen, SecuenciaDestino, TipoOperacion);

         }

        public DataTable ReportesApp_Operaciones_ListarMaestroClienteDestinatarioRuta(string remitente,string ruta)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarMaestroClienteDestinatarioRuta(remitente,ruta);
        }

        public DataTable ReportesApp_Operaciones_CompletarDestinatario_Direcciones(int idcliente, int idruta)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_CompletarDestinatario_Direcciones(idcliente, idruta);
        }

        public DataTable ReportesApp_ListarClientesDestinatario_GuiaElectronica(string cliente,int idRuta)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_ListarClientesDestinatario_GuiaElectronica(cliente, idRuta);
        }

        public bool ReportesApp_Operaciones_DesvincularViaje_Previaje_GuiaElectronica(string empresa, int idCliente, int idOt, string TipoGuia, ref int idGuiaElectronica, string NroTicket, ref int idViaje, ref string Viaje, int idProgramacion, int anioProgramacion, ref string serieGuia, ref string numeroGuia, int LineaOT, string Peso)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_DesvincularViaje_Previaje_GuiaElectronica(empresa, idCliente, idOt, TipoGuia, ref idGuiaElectronica, NroTicket, ref idViaje, ref Viaje, idProgramacion, anioProgramacion, ref serieGuia, ref numeroGuia,  LineaOT, Peso);

        }
        public DataTable ReportesApp_BuscarProgramacionPreviajeLibre(int operacion, string fechaInicio, string fechaFin, string previaje)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_BuscarProgramacionPreviajeLibre(operacion, fechaInicio, fechaFin, previaje);
        }

        public bool ReportesApp_Operaciones_RegistrarGuiaDeEvento_Electronica(clsGRT entGuiaTransportista)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RegistrarGuiaDeEvento_Electronica(entGuiaTransportista);
        }

        public DataTable ReportesaApp_Operaciones_ListarGuiasEvento(string Serie, string Numero)
        {
            return clsOperacionesDAO.Instancia.ReportesaApp_Operaciones_ListarGuiasEvento(Serie, Numero);
        }

        public Boolean ReportesApp_Operaciones_Previajes_Tolvas_Insertar(ref clsPreviajeTolvas entPreviajeTolvas, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_Insertar(ref entPreviajeTolvas, Usuario);
        }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_ListarOperaciones()
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_ListarOperaciones(); }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_ImportarReporteSumarizado(int NroTicket, string FechaIni, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_ImportarReporteSumarizado(NroTicket,  FechaIni,  FechaFin); }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_Listar(string CodPreviaje, string Remitente, string fechaInicio, string fechaFin)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_Listar(CodPreviaje, Remitente, fechaInicio, fechaFin);
        }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_SeleccionarPreviaje(int idPreviajeTolvas, int Anio)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_SeleccionarPreviaje(idPreviajeTolvas, Anio);
        }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_CerrarOperacion(int idPreviajeTolvas, int Anio, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_CerrarOperacion(idPreviajeTolvas, Anio, Usuario);
        }

        public bool ReportesApp_Operaciones_Previajes_Tolvas_Modificar(int idPreviajeTolvas, int Anio, ref clsPreviajeTolvas entPreviajeTolvas, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_Modificar(idPreviajeTolvas, Anio, ref entPreviajeTolvas, Usuario);
        }

        public DataTable ReportesApp_ListarClientes_GuiaElectronica_Transportista(string cliente)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_ListarClientes_GuiaElectronica_Transportista(cliente);
        }

        public bool ReportesApp_OperacionesVerificarPermisoTransmision(string UsuarioModulo)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_OperacionesVerificarPermisoTransmision(UsuarioModulo);
        }

        public bool ReportesApp_Operaciones_RegistrarTransmision(int idpersona, string transmision)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RegistrarTransmision(idpersona, transmision);
        }

        public DataTable ReportesApp_Operaciones_ListarViajesTolvas(string NroTicket,int idPreviajeTolvas,int anio,string placa,string conductor,int anulados)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarViajesTolvas(NroTicket, idPreviajeTolvas, anio, placa, conductor,anulados);
        }

        public bool ReportesApp_RegistrarPesoNroTicketClienteTolvas(string Operacion,string pesoCliente, string NroTicketPeso, int idViaje, string Serie, string Numero)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_RegistrarPesoNroTicketClienteTolvas(Operacion,pesoCliente, NroTicketPeso, idViaje,Serie,Numero);
        }

        public bool ReportesApp_GenerarViajesTolvas_GuiasElectronicas(string xmlPreviajes)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_GenerarViajesTolvas_GuiasElectronicas(xmlPreviajes);
        }

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_BuscarFaltanteSinViaje(int NroProgramacion)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_BuscarFaltanteSinViaje(NroProgramacion);
        }

        public DataTable ReportesApp_Operaciones_Previajes_Faltantes_ModificarSinViaje(int NroProgramacion, DateTime FechaIncidente, string Cliente, int idMotivo, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ModificarSinViaje(NroProgramacion, FechaIncidente, Cliente, idMotivo, Usuario);
        }

        public bool ReportesApp_AnularViajeTolvas(int idPreviajeTolvas, int anio, int idViaje, string MotivoAnula)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_AnularViajeTolvas(idPreviajeTolvas, anio, idViaje,MotivoAnula);
        }

        public bool ReportesApp_ActualizarDatosGuiaSalaverry(string tipo, string ticket, string dato)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_ActualizarDatosGuiaSalaverry(tipo, ticket, dato);
        }

        public DataTable ReportesApp_Operaciones_ConsultarEstadoGuiaReporteador(string Serie, int Numero)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ConsultarEstadoGuiaReporteador(Serie, Numero);
        }

        public bool ReportesApp_Operaciones_ImportarGuiasTerceros(string cadena, string usuario, int idProgramacion, int anioProgamacion )
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ImportarGuiasTerceros(cadena, usuario, idProgramacion, anioProgamacion);
        }

        public bool ReportesApp_Operacones_VincularGuiasFisicas_Consolidado(clsGRT openGenerarGuia, string SerieTran , string NumeroTran, string SerieRem , string NumeroRem)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operacones_VincularGuiasFisicas_Consolidado(openGenerarGuia,SerieTran,NumeroTran,SerieRem,NumeroRem);
        }

        public DataTable Reportesapp_Operaciones_GenerarReporteGuiasRetorno(string fechaInicio,string fechaFin)
        {
            return clsOperacionesDAO.Instancia.Reportesapp_Operaciones_GenerarReporteGuiasRetorno(fechaInicio, fechaFin);
        }

        public bool ReportesApp_Operaciones_GuardarRutasDesbloqueadas(string xmlConductores, string xmlRutas)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_GuardarRutasDesbloqueadas(xmlConductores, xmlRutas);
        }

        public DataTable Reportesapp_Operaciones_ListarRutasDesbloqueadasXConductor()
        {
            return clsOperacionesDAO.Instancia.Reportesapp_Operaciones_ListarRutasDesbloqueadasXConductor();
        }

        public bool ReportesApp_Operaciones_EliminarRutaXConductor(int idConductor, int idRuta)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EliminarRutaXConductor(idConductor, idRuta);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarTipoGasto()
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarTipoGasto();
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarOperaciones()
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarGastoDetalle(int idTipoGasto, string Descripcion, decimal Gasto, int idTiempo, int Adicional, int idPeaje)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarGastoDetalle(idTipoGasto, Descripcion, Gasto, idTiempo, Adicional, idPeaje); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoDetalle(int IdRuta, int IdOperacion)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoDetalle(IdRuta, IdOperacion);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarGastoDetalle(int idGastoxRutaD)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarGastoDetalle(idGastoxRutaD);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarGasto(int IdRuta, int IdOperacion, int TotalDias, decimal GastoTotal, int Detalle)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarGasto(IdRuta, IdOperacion, TotalDias, GastoTotal, Detalle);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGasto(int IdRuta, int IdOperacion)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGasto(IdRuta, IdOperacion);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto(int NroProgramacion, int idGastoxRutaC, decimal TotalEntregado, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto(NroProgramacion, idGastoxRutaC, TotalEntregado, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ImprimirPago(int NroProgramacion, int idGastoxRutaC, string Planilla, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ImprimirPago(NroProgramacion, idGastoxRutaC, Planilla, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(int IdRuta, int IdOperacion, int idTiempo)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(IdRuta, IdOperacion, idTiempo);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarRegistro(string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarGasto(int NroProgramacion)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGasto(NroProgramacion);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarConductores(string Filtro)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarConductores(Filtro);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarViajes(int IdConductor, string Ruta)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarViajes(IdConductor, Ruta);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillas(int Programacion)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillas(Programacion);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(int Accion, string CodGasto)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto(Accion, CodGasto);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarNombreRUC(string NroRUC)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC(NroRUC);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarLiquidaciones(string CodGasto, DateTime FechaLiquidacion,
                         char TipoImpuesto, string ConceptoGasto, string DescripcionGasto, string NroRUC, string NombreCompleto, string CodigoDocumento,
            string NroDocumento, decimal MontoAfecto, decimal MontoNoAfecto, decimal MontoImpuestos, decimal MontoPagado, string MotivoGasto)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarLiquidaciones(CodGasto, FechaLiquidacion, TipoImpuesto, ConceptoGasto,
                   DescripcionGasto, NroRUC, NombreCompleto, CodigoDocumento, NroDocumento, MontoAfecto, MontoNoAfecto, MontoImpuestos, MontoPagado, MotivoGasto);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarLiquidaciones(int idNroLiquidacion, string CodGasto)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarLiquidaciones(idNroLiquidacion, CodGasto);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_LiquidarTicketGasto(int idConductor, int Programacion, string Placa, DateTime FechaLiquidacion,
                         int Planilla, decimal Gasto, decimal Total, decimal Reintegro, string DescripcionRG, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_LiquidarTicketGasto(idConductor, Programacion, Placa, FechaLiquidacion,
                         Planilla, Gasto, Total, Reintegro, DescripcionRG, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasLiquidadas(int IdOperacion, string fini, string ffin, string conductor, string planilla, string Ruta, int estado)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasLiquidadas(IdOperacion, fini, ffin, conductor, planilla, Ruta, estado); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarRecibosLiquidados(string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRecibosLiquidados(CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarViaticos(int NroTicket, string CodGasto, int idConductor, DateTime Fecha, int idTipoViatico, decimal Monto, string Descripcion, string Motivo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarViaticos(NroTicket, CodGasto, idConductor, Fecha, idTipoViatico, Monto, Descripcion, Motivo, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarViaticos(int NroTicket, int idViatico, int idConductor, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarViaticos(NroTicket, idViatico, idConductor, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarViaticos(int Opcion, int idConductor)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarViaticos(Opcion, idConductor);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarRegistroViaticos(int Opcion, string fini, string ffin, string conductor)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistroViaticos(Opcion, fini, ffin, conductor);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ActualizarRecibosLiquidados(string CodGasto, decimal ImporteTotal)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ActualizarRecibosLiquidados(CodGasto, ImporteTotal);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarPlanilla(int Programacion, string CodGasto, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarPlanilla(Programacion, CodGasto, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(int idPeaje, int Opcion, string NroRUC)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje(idPeaje, Opcion, NroRUC); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarTiempos()
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarTiempos(); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarCambioRutas(string Planilla)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarCambioRutas(Planilla); }

        public DataTable ReportesApp_Operaciones_BuscarGuiaIndividual(string TipoGuia, string SerieGuia, string NumeroGuia)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_BuscarGuiaIndividual(TipoGuia,SerieGuia,NumeroGuia);
        }

        public bool ReportesApp_Operaciones_Registrar_SolicitudCambios_GuiaElectronica(ref string NroSolicitud, string SerieGuia, string NumeroGuia, string Campo, string NuevoValor, string MotivoSolicitud)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Registrar_SolicitudCambios_GuiaElectronica(ref NroSolicitud, SerieGuia, NumeroGuia, Campo, NuevoValor, MotivoSolicitud);
        }

        public DataTable ReportesApp_operaciones_ListarSolicitudes_CambioDatosGuia(string Serie, string Numero)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_operaciones_ListarSolicitudes_CambioDatosGuia(Serie, Numero);
        }

        public bool ReportesApp_Operaciones_ActualizarDatosGuia(string nroSolicitud, int idconductor, string nombresConductor)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ActualizarDatosGuia(nroSolicitud,idconductor,nombresConductor);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoRuta(int IdOperacion, string Ruta)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoRuta(IdOperacion, Ruta);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_AgregarDiferencialRuta(int NroProgramacion, int idGastoxRutaC, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_AgregarDiferencialRuta(NroProgramacion, idGastoxRutaC, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ImprimirDiferencial(int NroProgramacion, string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ImprimirDiferencial(NroProgramacion, CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_PagarViaticos(int NroProgramacion, string Usuario, string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_PagarViaticos(NroProgramacion, Usuario, CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ComprobanteLiquidaciones(int idNroLiquidacion, string CodGasto, DateTime FechaLiquidacion,
                         char TipoImpuesto, string NroRUC, string NombreCompleto, string CodigoDocumento, string NroDocumento, decimal MontoAfecto,
                         decimal MontoNoAfecto, decimal MontoImpuestos, decimal MontoPagado)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ComprobanteLiquidaciones(idNroLiquidacion, CodGasto, FechaLiquidacion,
                         TipoImpuesto, NroRUC, NombreCompleto, CodigoDocumento, NroDocumento, MontoAfecto, MontoNoAfecto, MontoImpuestos, MontoPagado);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarViaticos(int idConductor, DateTime FechaViatico, string Comprobante, decimal Monto, string NroRUC, string Planilla)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarViaticos(idConductor, FechaViatico, Comprobante, Monto, NroRUC, Planilla); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ViaticoSinProg(int idConductor, DateTime Fecha, int idTipoViatico, decimal Monto, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ViaticoSinProg(idConductor, Fecha, idTipoViatico, Monto, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarViaticoProg(int idConductor)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarViaticoProg(idConductor);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_AdjuntarPlanilla(string CodGasto, int idConductor, int NroProgramacion, int idOperacion, int idGastoxRutaC, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_AdjuntarPlanilla(CodGasto, idConductor, NroProgramacion, idOperacion, idGastoxRutaC, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarReintegro(int idConductor, int Programacion, string Placa, DateTime FechaLiquidacion,
                         int Planilla, decimal Gasto, decimal Total, decimal Reintegro, string DescripcionRG, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarReintegro(idConductor, Programacion, Placa, FechaLiquidacion,
                         Planilla, Gasto, Total, Reintegro, DescripcionRG, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_PagarSinViaje(int idConductor, int Planilla, decimal monto, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_PagarSinViaje(idConductor, Planilla, monto, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_PagarViaticosTolvas(int idConductor, int Planilla, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_PagarViaticosTolvas(idConductor, Planilla, Usuario); }

        public DataTable ReportesApp_Operaciones_ListarDireccionesCliente(int idCliente)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarDireccionesCliente(idCliente);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje(string Conductor, string Ruta)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje(Conductor, Ruta);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ActualizarPlanilla(int NroProgramacion, string Planilla, string Usuario)//, int idConductor, decimal Gasto)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ActualizarPlanilla(NroProgramacion, Planilla, Usuario);//, idConductor, Gasto);
        }

        public bool ReportesApp_Operaciones_ActualizarFechaGuias(string fecha, string xmlGuias,string tipofecha)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ActualizarFechaGuias(fecha, xmlGuias,tipofecha);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_AutorizarReintegro(string Planilla, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_AutorizarReintegro(Planilla, Usuario);
        }

        public bool ReportesApp_Operaciones_RegistrarFechaTermino(int _codProgramacion, string _anio, string fechaTermino)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RegistrarFechaTermino(_codProgramacion, _anio, fechaTermino);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarReporteGasto(string Planilla, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarReporteGasto(Planilla, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(int Opcion, int IdRuta, int IdOperacion, int idGastoxRutaD)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(Opcion, IdRuta, IdOperacion, idGastoxRutaD); }

        public DataTable ReportesApp_Operaciones_RutaZona_ListarZonas()
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RutaZona_ListarZonas(); }

        public DataTable ReportesApp_Operaciones_RutaZona_AsignarEditarZonas(int Opcion, int IdRuta, int IdOperacion, int idZona, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RutaZona_AsignarEditarZonas(Opcion, IdRuta, IdOperacion, idZona, Usuario); }

        public DataTable ReportesApp_Operaciones_RutaZona_ListarRegistroZonas(int IdOperacion, string Ruta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RutaZona_ListarRegistroZonas(IdOperacion, Ruta); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasPendientes(string Conductor, int IdOperacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasPendientes(Conductor, IdOperacion); }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaTolvas(int idConductor, int idTracto, int idCarreta, int idRuta, int idGastoXRutaC,
                                                                                     decimal TotalEntregado, DateTime FechaViaje, int TotalDias, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaTolvas(idConductor, idTracto, idCarreta, idRuta, idGastoXRutaC,
                                                                                                           TotalEntregado, FechaViaje, TotalDias, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas(string CodGasto, int idViatico)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas(CodGasto, idViatico); }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas2(string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas2(CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPlanillasTolvas(string FechaInicio, string FechaFin, string Conductor, string Planilla, int Pendientes)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasTolvas(FechaInicio, FechaFin, Conductor, Planilla, Pendientes); }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarPlanillaTolvas(string CodGasto, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarPlanillaTolvas(CodGasto, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarViaticosTolvas(int NroTicket, string CodGasto, int idConductor, DateTime Fecha, int idTipoViatico, decimal Monto, string Descripcion, string Motivo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarViaticosTolvas(NroTicket, CodGasto, idConductor, Fecha, idTipoViatico, Monto, Descripcion, Motivo, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_EliminarGastosTolvas(int idViatico, int idConductor, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarGastosTolvas(idViatico, idConductor, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarProveedor(string Proveedor)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarProveedor(Proveedor); }

        public DataTable ReportesApp_Operaciones_ObtenerUltimoCorrelativoGuiaTransportista(string serie)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ObtenerUltimoCorrelativoGuiaTransportista(serie);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarModificarPeajes(int idPeaje, decimal Peaje)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarModificarPeajes(idPeaje, Peaje); }

        public DataTable ReportesApp_Operaciones_TicketGasto_FiltrarPeaje(int Planilla, int IdRuta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_FiltrarPeaje(Planilla, IdRuta); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPeajes()
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPeajes(); }

        public DataTable ReportesApp_Liquidacion_PlanillasPendientesConsolidado(string fini, string ffin, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Liquidacion_PlanillasPendientesConsolidado(fini, ffin, Usuario); }

        public bool ReportesApp_OperacionesVerificarPermisoRutaxConductor(string UsuarioModulo)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_OperacionesVerificarPermisoRutaxConductor(UsuarioModulo);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_ModificarFechaViatico(int idListaViatico, int idConductor, string Planilla, string NroComprobante, DateTime FechaViatico)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ModificarFechaViatico(idListaViatico, idConductor, Planilla, NroComprobante, FechaViatico); }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarReportePlanillas(string Sucursal, string FechaInicio, string FechaFin, int Detalle)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarReportePlanillas(Sucursal, FechaInicio, FechaFin, Detalle); }

        public DataTable ReportesApp_Operaciones_ListarGuiasViaje(int idViaje,string serie,string numero)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarGuiasViaje(idViaje,serie,numero);
        }


        public bool ReportesApp_Operaciones_ReemplzarGuias(string idGuia,string SerieAnterior,string NumeroAnterior, string Serie, string Numero,int idviaje,string remitente,string guiasotros,string idOT,int idDireccionPartida,int idDireccionLlegada,int LineaOT)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ReemplzarGuias(idGuia, SerieAnterior, NumeroAnterior, Serie, Numero, idviaje, remitente, guiasotros, idOT, idDireccionPartida, idDireccionLlegada,LineaOT);
        }

        public bool ReportesApp_Operaciones_ConsultarGuiaExiste(string Serie,string Numero)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ConsultarGuiaExiste(Serie, Numero);
        }

        public bool ReportesApp_Operaciones_ConfirmarDesconfirmar(int Confirmar1,string xml)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ConfirmarDesconfirmar(Confirmar1, xml);
        }

        public DataTable ReportesApp_ListarGuiasElectronicasRecepcionadas(string p1, string p2, string p3, string p4, string p5, bool p6, bool p7, bool p8, bool p9, string p10, string p11)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_ListarGuiasElectronicasRecepcionadas(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
        }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_InsertarActividad(int Persona, string Descripcion, string Nivel, DateTime FechaInicio, DateTime FProyectada1,
                                                                                     string Seguimiento, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_PendientesDiarios_InsertarActividad(Persona, Descripcion, Nivel, FechaInicio, FProyectada1, Seguimiento, Usuario); }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_ListarActividades(int FechaP, string Responsable, string Area, string Estado, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_PendientesDiarios_ListarActividades(FechaP, Responsable, Area, Estado, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_FiltrarActividades(int idActividad)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_PendientesDiarios_FiltrarActividades(idActividad); }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_ModificarActividades(int Opcion, int idActividad, int Persona, string Estado, string Seguimiento, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_PendientesDiarios_ModificarActividades(Opcion, idActividad, Persona, Estado, Seguimiento, Usuario); }

        public DataTable ReportesApp_Operaciones_PendientesDiarios_ReprogramarActividades(int idActividad, DateTime FechaReprog, int Contador, string Seguimiento, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_PendientesDiarios_ReprogramarActividades(idActividad, FechaReprog, Contador, Seguimiento, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes(int idConductor, int idProgramacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes(idConductor, idProgramacion); }

        public bool ReportesApp_Operaciones_VincularGuiasNoEnlazadas(int idviaje, string viaje, string Serie, string Numero, int LineaOT,string ticket)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_VincularGuiasNoEnlazadas(idviaje, viaje, Serie, Numero, LineaOT, ticket);
        }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTractos(int Opcion, string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(Opcion, Periodo); }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearTractos(int IdUnidad, string TipoUnidad, string Periodo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_MapearTractos(IdUnidad, TipoUnidad, Periodo, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearCondiciones(int idCondicion, string Periodo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_MapearCondiciones(idCondicion, Periodo, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_QuitarTractos(int IdUnidad, string TipoUnidad, string Periodo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_QuitarTractos(IdUnidad, TipoUnidad, Periodo, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadTractos(string Periodo, string Placa, string Programacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadTractos(Periodo, Placa, Programacion); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaOperatividad(string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividad(Periodo); }

        public DataTable ReportesApp_Operaciones_Operatividad_RegistrarPlacas(string Periodo, string xmlOperatividad, int idCondicion, int idOperacion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_RegistrarPlacas(Periodo, xmlOperatividad, idCondicion, idOperacion, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearCondicionesCarretas(int idCondicion, string Periodo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_MapearCondicionesCarretas(idCondicion, Periodo, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadCarreta(string Periodo, string Placa, string Programacion, string Carreta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadCarreta(Periodo, Placa, Programacion, Carreta); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadCarreta(string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCarreta(Periodo); }

        public DataTable ReportesApp_Operaciones_Operatividad_RegistrarCarretas(string Periodo, string xmlOperatividad, int idCondicion, int idOperacion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_RegistrarCarretas(Periodo, xmlOperatividad, idCondicion, idOperacion, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTractosOP(int Opcion, int idCondicion, DateTime Fecha, string TipoUnidad, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosOP(Opcion, idCondicion, Fecha, TipoUnidad, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_IngresarComentarios(int idOperatividad, string TipoUnidad, string Comentario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_IngresarComentarios(idOperatividad, TipoUnidad, Comentario); }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarItemBotiquin(string Descripcion, string Codigo, int Cantidad, int Duracion, string TipoBotiquin, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarItemBotiquin(Descripcion, Codigo, Cantidad, Duracion, TipoBotiquin, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarItemsAsignados(int Opcion, int idVehiculo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarItemsAsignados(Opcion, idVehiculo); }

        public DataTable ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin(int Opcion, int idBotiquinUnidadC, int idItemBotiquin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin(Opcion, idBotiquinUnidadC, idItemBotiquin); }

        public DataTable ReportesApp_Operaciones_ControlItems_AsignarBotiquin(int idVehiculo, string TipoBotiquin, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_AsignarBotiquin(idVehiculo, TipoBotiquin, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarBotiquines(string Placa, string Operacion, string TipoBotiquin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarBotiquines(Placa, Operacion, TipoBotiquin); }

        public DataTable ReportesApp_Operaciones_ControlItems_CrearModificarBotiquin(int idBotiquinUnidadC, int idItemBotiquin, int Cantidad,
                                                                                     DateTime FechaVencimiento, string Observacion, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_CrearModificarBotiquin(idBotiquinUnidadC, idItemBotiquin, Cantidad,
                                               FechaVencimiento, Observacion, Usuario);
        }

        public DataTable ReportesApp_Operaciones_ControlItems_GenerarRequerimiento(int idBotiquinUnidadC, int idBotiquinUnidadD, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_GenerarRequerimiento(idBotiquinUnidadC, idBotiquinUnidadD, Usuario); }

        public DataTable ReportesApp_ListarUnidadMedidaCargaTotal_Sunat()
        {
            return clsOperacionesDAO.Instancia.ReportesApp_ListarUnidadMedidaCargaTotal_Sunat();
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ListarMotivos(int Opcion, string Conductor)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarMotivos(Opcion, Conductor); }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarConstancia(int Opcion, int idConstancia, int IdTracto, int IdCarreta, int IdConductorAnt, int IdConductorNuevo,
                                                                                         string FechaSolicitud, string HoraSolicitud, string Motivo, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarConstancia(Opcion, idConstancia, IdTracto, IdCarreta, IdConductorAnt, IdConductorNuevo,
                                                                                                               FechaSolicitud, HoraSolicitud, Motivo, Usuario);
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ListarTicket(string CodConstancia)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarTicket(CodConstancia); }

        public DataTable ReportesApp_Mantenimiento_EntregaUnidad_ListarConstancia(string CodConstancia, string Placa, string FechaInicio, string FechaFin, string Estado, string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Mantenimiento_EntregaUnidad_ListarConstancia(CodConstancia, Placa, FechaInicio, FechaFin, Estado, Operacion); }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(int Opcion, string CodConstancia, DateTime HoraInicio, DateTime HoraFin, string Observacion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(Opcion, CodConstancia, HoraInicio, HoraFin, Observacion, Usuario); }

        public DataTable ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(int idTracto, int idConductor, int idRuta, int idOperacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(idTracto, idConductor, idRuta, idOperacion); }

        public DataTable ReportesApp_ListarUnidadMedida_SunatTotal()
        {
            return clsOperacionesDAO.Instancia.ReportesApp_ListarUnidadMedida_SunatTotal();
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(int idTracto, int idRuta, string Sucursal)
        { return clsOperacionesDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(idTracto, idRuta, Sucursal); }

        public bool ReportesApp_Operaciones_ListarDatosViajesPorFecha_Viaje(string xmlTarifa)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarDatosViajesPorFecha_Viaje(xmlTarifa); }

        public DataTable ReportesApp_Operaciones_DatosOT_ListarGuiasViaje(int Opcion, string CodViaje)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_DatosOT_ListarGuiasViaje(Opcion, CodViaje); }

        public DataTable ReportesApp_Operaciones_DatosOT_EditarGuiasViaje(int Opcion, string CodViaje, int OT, string GuiaT, string GuiaR)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_DatosOT_EditarGuiasViaje(Opcion, CodViaje, OT, GuiaT, GuiaR); }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarReporteReintegros(string Sucursal, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarReporteReintegros(Sucursal, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarGastoXConcepto(string Sucursal, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoXConcepto(Sucursal, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(int IdVehiculo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(IdVehiculo); }

        public DataTable ReportesApp_Operaciones_ListarReporteGuias(string FechaInicio, string FechaFin, string Serie, string Numero, string Tracto, string Carreta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ListarReporteGuias(FechaInicio, FechaFin, Serie, Numero, Tracto, Carreta); }

        public DataTable ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado(int idConductor, DateTime FechaTraslado)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_VerificarConductorBloqueado(idConductor, FechaTraslado); }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarKitNeumatico(int Opcion, string Empleado, string Herramienta, string Tracto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarKitNeumatico(Opcion, Empleado, Herramienta, Tracto); }

        public DataTable ReportesApp_Operaciones_Previajes_CalcularRendProm(string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_CalcularRendProm(Operacion); }

        public DataTable ReportesApp_Operaciones_ControlDocumentos_GenerarRequerimiento(int idDocumento, string TipoDocumento, int idUnidad, string Placa, string CentroCosto, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlDocumentos_GenerarRequerimiento(idDocumento, TipoDocumento, idUnidad, Placa, CentroCosto, Usuario); }

        public DataTable ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(int IDRelacion, string TipoRelacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(IDRelacion, TipoRelacion); }

        public DataTable ReportesApp_Operaciones_ControlTarifas_InsertarTarifas(string xmlTarifas, string Moneda, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlTarifas_InsertarTarifas(xmlTarifas, Moneda, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlTarifas_ListarTarifas(string Ruta, string FechaInicio, string FechaFin, string Cliente)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlTarifas_ListarTarifas(Ruta, FechaInicio, FechaFin, Cliente); }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarExtintor(int Opcion, int idExtintor, int IdTracto, string Codigo, DateTime FechaVenc,
                                                                                                decimal Peso, string UnidadPeso, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarExtintor(Opcion, idExtintor, IdTracto, Codigo, FechaVenc, Peso, UnidadPeso, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarExtintores(string Placa, string Estado, string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarExtintores(Placa, Estado, Operacion); }

        public DataTable ReportesApp_Operaciones_Previajes_ActualizarViajeEstado(int IDTicket, string Anio, int IDTracto, int Estado, int TipoProgramacion,
                                                                                 DateTime FInicio, DateTime FFin, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ActualizarViajeEstado(IDTicket, Anio, IDTracto, Estado, TipoProgramacion,
                                                                                 FInicio, FFin, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_DesbloqueoConductores(int Opcion, int idDesbloqueo, int idConductor, DateTime FechaCompromiso, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_DesbloqueoConductores(Opcion, idDesbloqueo, idConductor, FechaCompromiso, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarConductoresDesbloqueados(string Conductor, string FechaCompromiso)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConductoresDesbloqueados(Conductor, FechaCompromiso); }

        public DataTable ReportesApp_Operaciones_Programacion_UltimoViajeCompletado(int IDRelacion, int TipoOperacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Programacion_UltimoViajeCompletado(IDRelacion, TipoOperacion); }

        public DataTable ReportesApp_Operaciones_Operatividad_InsertarIngresosOperacion(int idOperacion, string Periodo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_InsertarIngresosOperacion(idOperacion, Periodo, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTablaIngresos(string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaIngresos(Periodo); }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(int Opcion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(Opcion); }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_AsignarGrupo(int idRuta, int idGrupoViaje)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_CumplimientoViajes_AsignarGrupo(idRuta, idGrupoViaje); }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(int Opcion, int idGrupoViaje, DateTime FechaCumplimiento, int Disponibles,
                                                                                                int Proyectados, string Detalle, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(Opcion, idGrupoViaje, FechaCumplimiento, Disponibles, Proyectados, Detalle, Usuario); }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(int idGrupoViaje, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(idGrupoViaje, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_CumplimientoViajes_EliminarGrupo(int idGrupoViajeD, int idGrupoViaje)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_CumplimientoViajes_EliminarGrupo(idGrupoViajeD, idGrupoViaje); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarPuntosParada(int Opcion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarPuntosParada(Opcion); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarPuntoParada(string Descripcion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarPuntoParada(Descripcion); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarEliminarParada(int Opcion, int idParada, string Trafico, int idRuta, string PuntoInicio,
                                                                                string PuntoParada, DateTime Horas, string UsuarioCreacion)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarEliminarParada(Opcion, idParada, Trafico, idRuta, PuntoInicio,
                                                                                        PuntoParada, Horas, UsuarioCreacion);
        }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarParadasRutas(int Opcion, string Ruta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarParadasRutas(Opcion, Ruta); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidado(int Opcion, int idConsolidado, int NroPreviaje, DateTime FechaProg, int idTracto, int idCarreta,
                                                                                       int idRuta, int idConductor, DateTime FechaViaje, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidado(Opcion, idConsolidado, NroPreviaje, FechaProg, idTracto, idCarreta, idRuta, idConductor, FechaViaje, Usuario); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidado(string FechaInicio, string FechaFin, string Vehiculo, string Conductor, string Ruta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidado(FechaInicio, FechaFin, Vehiculo, Conductor, Ruta); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidadoDetalle(int idConsolidado)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidadoDetalle(idConsolidado); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidadoDetalle(int idConsolidado, int idParada, DateTime HoraDuracion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidadoDetalle(idConsolidado, idParada, HoraDuracion); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_EliminarConsolidadoDetalle(int idConsolidado, int idParada)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_EliminarConsolidadoDetalle(idConsolidado, idParada); }

        public DataTable ReportesApp_Operaciones_ItinerarioViajes_ListarFechaTermino(int idConsolidado)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ItinerarioViajes_ListarFechaTermino(idConsolidado); }

        public DataTable ReportesApp_Operaciones_Previajes_Tolvas_ActualizarPeso(string SerieT, string NumeroT, int idRuta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_Tolvas_ActualizarPeso(SerieT, NumeroT, idRuta); }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarKAD(int Opcion, int idKAD, int IdTracto, string KitAsignado, string Observacion, byte[] Imagen, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarKAD(Opcion, idKAD, IdTracto, KitAsignado, Observacion, Imagen, Usuario); }
        
        public DataTable ReportesApp_Operaciones_ControlItems_ListarKAD(string Placa, string Estado, string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarKAD(Placa, Estado, Operacion); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadCisternas(string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCisternas(Periodo); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadCortineras(string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCortineras(Periodo); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadPlataformas(string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadPlataformas(Periodo); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarOperatividadTolvas(string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadTolvas(Periodo); }

        public bool ReportesApp_OperacionesVerificarTipoBreveteAdicional(string UsuarioModulo)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_OperacionesVerificarTipoBreveteAdicional(UsuarioModulo);
        }

        public bool ReportesApp_Operaciones_RegistrarBreveteAdicional(int IdPersonaOperacion ,string adicionar)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_RegistrarBreveteAdicional(IdPersonaOperacion, adicionar);
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarObservacionesI(int Opcion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarObservacionesI(Opcion); }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarEditarTanques(int Opcion, int idRegistroTC, string Programacion, DateTime FechaRevision, int idTracto, int idCarreta,
                                                                                     int PersonaConductor, string LugarInspeccion, int PersonaInspector, string Observacion, byte[] ImagenHallazgo,
                                                                                     byte[] ImagenHallazgo2, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarTanques(Opcion, idRegistroTC, Programacion, FechaRevision, idTracto, idCarreta, PersonaConductor,
                                                                                                           LugarInspeccion, PersonaInspector, Observacion, ImagenHallazgo, ImagenHallazgo2, Usuario);
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarTanquesCombustible(string Placa, string Operacion, string Conductor, string Estado, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarTanquesCombustible(Placa, Operacion, Conductor, Estado, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarReparacion(int idRegistroTC, int PersonaTecnico, int PersonaSeguimiento, DateTime FechaReparacion, byte[] ImagenReparacion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarReparacion(idRegistroTC, PersonaTecnico, PersonaSeguimiento, FechaReparacion, ImagenReparacion, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarEditarAsientos(int Opcion, int idRegistroTA, string Programacion, string TipoAT, DateTime FechaRevision, int idTracto, int PersonaConductor,
                                                                                      string Tapizado, string Reclinable, string Corredizo, string Radio, string LugarInspeccion, int PersonaInspector,
                                                                                      byte[] ImagenHallazgo, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarAsientos(Opcion, idRegistroTA, Programacion, TipoAT, FechaRevision, idTracto, PersonaConductor, Tapizado,
                                                                                                            Reclinable, Corredizo, Radio, LugarInspeccion, PersonaInspector, ImagenHallazgo, Usuario);
        }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarAsientosTimones(string TipoAT, string Placa, string Operacion, string Conductor, string Estado, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarAsientosTimones(TipoAT, Placa, Operacion, Conductor, Estado, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_ControlItems_RepararAsientos(int idRegistroTA, int PersonaProveedor, int PersonaSeguimiento, DateTime FechaReparacion, byte[] ImagenReparacion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RepararAsientos(idRegistroTA, PersonaProveedor, PersonaSeguimiento, FechaReparacion, ImagenReparacion, Usuario); }

        public DataTable ReportesApp_Operaciones_Previajes_ListarTiemposViajes(string Previaje, string FechaInicio, string FechaFin, string Ruta, string Programacion, string Placa, string Conductor)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ListarTiemposViajes(Previaje, FechaInicio, FechaFin, Ruta, Programacion, Placa, Conductor); }

        public DataTable ReportesApp_Operaciones_Previajes_FiltrarTiemposViajes(int NroTicket)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_FiltrarTiemposViajes(NroTicket); }

        public DataTable ReportesApp_Operaciones_Previajes_ModificarTiempoViajes(int NroTicket, string EstadoV, string RutaViaje, string EstadoViaje, string Ubicacion, decimal PorcTransito, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ModificarTiempoViajes(NroTicket, EstadoV, RutaViaje, EstadoViaje, Ubicacion, PorcTransito, Usuario); }

        public DataTable ReportesApp_Operaciones_Previajes_ListarUbicaciones(int Opcion, string Operacion, string Estado, string RutaViaje)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(Opcion, Operacion, Estado, RutaViaje); }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(int Opcion, int NroTicket, string EstadoV, DateTime LlegadaPlanta, DateTime IngresoPlanta, DateTime InicioAtencion, DateTime FinAtencion, DateTime EntregaGuia, DateTime SalidaPlanta,
                         DateTime SalidaRuta, DateTime LlegadaCDA, DateTime InicioDescarga, DateTime FinDescarga, DateTime InicioRuta, DateTime LlegadaCDA2, DateTime InicioDescarga2, DateTime FinDescarga2, DateTime LlegadaBase, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(Opcion, NroTicket, EstadoV, LlegadaPlanta, IngresoPlanta, InicioAtencion, FinAtencion, EntregaGuia, SalidaPlanta, SalidaRuta,
                                               LlegadaCDA, InicioDescarga, FinDescarga, InicioRuta, LlegadaCDA2, InicioDescarga2, FinDescarga2, LlegadaBase, Usuario);
        }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas(int Opcion, int NroTicket, string EstadoV, DateTime SalidaBase, DateTime LlegadaCarga, DateTime Carga, DateTime SalidaPlanta, DateTime LlegadaDescarga,
                         DateTime InicioDescarga, DateTime SalidaDescarga, DateTime LlegadaBase, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas(Opcion, NroTicket, EstadoV, SalidaBase, LlegadaCarga, Carga, SalidaPlanta, LlegadaDescarga, InicioDescarga, SalidaDescarga,
                                               LlegadaBase, Usuario);
        }

        public DataTable ReportesApp_Operaciones_Previajes_ImportarTiempoViajes(int Opcion, string xmlDetalle, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ImportarTiempoViajes(Opcion, xmlDetalle, Usuario); }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte(int Opcion, int NroTicket, int idPernocte, DateTime FechaInicio, DateTime FechaFin, string Ubicacion, string TipoPernocte, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte(Opcion, NroTicket, idPernocte, FechaInicio, FechaFin, Ubicacion, TipoPernocte, Usuario); }

        public DataTable ReportesApp_Operaciones_Previajes_FiltrarTiemposPernoctes(int NroTicket)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_FiltrarTiemposPernoctes(NroTicket); }

        public DataTable ReportesApp_Operaciones_Previajes_ListarTiemposPernocte(string Previaje, string FechaInicio, string FechaFin, string Ruta, string Programacion, string Placa, string Conductor)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ListarTiemposPernocte(Previaje, FechaInicio, FechaFin, Ruta, Programacion, Placa, Conductor); }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarTiempoAtencion(int Opcion, int idTiempoAtencion, string Destino, string HorarioLV, string HorarioS,
                                                                                   DateTime TiempoAtencion, int idOperacion, int idRuta, string UsuarioCrea)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_RegistrarTiempoAtencion(Opcion, idTiempoAtencion, Destino, HorarioLV, HorarioS, TiempoAtencion, idOperacion, idRuta, UsuarioCrea); }

        public DataTable ReportesApp_Operaciones_Previajes_ListarTiempoAtencion(string Destino)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ListarTiempoAtencion(Destino); }

        public DataTable ReportesApp_Operaciones_ControlItems_AsignarKitNeumatico(int Opcion, int IDHerramienta, int Persona, int Tracto, string UserRegistra)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_AsignarKitNeumatico(Opcion, IDHerramienta, Persona, Tracto, UserRegistra); }

        public DataTable ReportesApp_Operaciones_ControlItems_ModificarKitNeumatico(int PersonaAnterior, int PersonaNueva, int Tracto, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ModificarKitNeumatico(PersonaAnterior, PersonaNueva, Tracto, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_RevisarKitNeumatico(int Tracto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RevisarKitNeumatico(Tracto); }

        public DataTable ReportesApp_Operaciones_ReporteGuiasRRHH(string fechaInicio, string fechaFin)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ReporteGuiasRRHH(fechaInicio, fechaFin);
        }

        public DataTable ReportesApp_Operaciones_Operatividad_MapearIndicadores(string Periodo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_MapearIndicadores(Periodo, Usuario); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarIndicadores(int Opcion, string Periodo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarIndicadores(Opcion, Periodo); }

        public DataTable ReportesApp_Operaciones_Operatividad_ListarTractosTotal(int Mes, int Anio)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosTotal(Mes, Anio); }

        public DataTable ReportesApp_Operaciones_Programacion_RevisarAsignaciones(int idTracto, int idConductor, int TipoOperacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Programacion_RevisarAsignaciones(idTracto, idConductor, TipoOperacion); }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_BuscarConductor(int IdTracto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EntregaUnidad_BuscarConductor(IdTracto); }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion(int Opcion, string CodConstanciaA, int IdTracto, int UltimoPreviaje,
                         int IdConductorAnt, int IdConductorNuevo, string Observacion, string Motivo, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion(Opcion, CodConstanciaA, IdTracto, UltimoPreviaje,
                                               IdConductorAnt, IdConductorNuevo, Observacion, Motivo, Usuario);
        }

        public DataTable ReportesApp_Operaciones_EntregaUnidad_ListarAsignaciones(string Conductor, string Placa, string FechaInicio, string FechaFin, string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarAsignaciones(Conductor, Placa, FechaInicio, FechaFin, Operacion); }

        public DataTable ReportesApp_Operaciones_Previajes_ListarDisponibles(string TipoUnidad, string Placa)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_ListarDisponibles(TipoUnidad, Placa); }


        public DataTable ReportesApp_Operaciones_ReporteViajesPendientes_Tolvas(string fechaini, string fechafin)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ReporteViajesPendientes_Tolvas(fechaini, fechafin); 
        }

        public DataTable ReportesApp_Operaciones_Consolidado_Pesos_Operacion(string fechaIni, string fechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Consolidado_Pesos_Operacion(fechaIni, fechaFin); }

        public DataTable ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(int Opcion, int Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(Opcion, Operacion); }

        public DataTable ReportesApp_Operacion_ControlDocumentos_InsertarEliminarVencimiento(int Opcion, int idVencimiento, int idMRelacion, int TipoDocumento, int Vencimiento,
                                                                                             string Area, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operacion_ControlDocumentos_InsertarEliminarVencimiento(Opcion, idVencimiento, idMRelacion, TipoDocumento, Vencimiento, Area, Usuario); }

        public DataTable ReportesApp_Operacion_ControlDocumentos_ListarVencimientos(string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarVencimientos(Operacion); }

        public DataTable ReportesApp_Operacion_ControlDocumentos_ListarDocumentos(int Opcion, int idRelacion, int idOperacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarDocumentos(Opcion, idRelacion, idOperacion); }

        public bool ReportesApp_Combustible_CargarGasboy(string xmlgasboy)
        { return clsOperacionesDAO.Instancia.ReportesApp_Combustible_CargarGasboy(xmlgasboy); }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarPendientes(int IdVehiculo)
        { return clsOperacionesDAO.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarPendientes(IdVehiculo); }

        public DataTable ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(int idTracto, int idRuta, string Programacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(idTracto, idRuta, Programacion); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarViaticosTicket(int Opcion, int NroTicket, int idRuta)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarViaticosTicket(Opcion, NroTicket, idRuta); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarConductorAnterior(string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarConductorAnterior(CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento(string Planilla, int NroTicket, int TipoProgramacion, int idRuta,
                                                                                   int IdConductor, decimal TotalEntregado, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento(Planilla, NroTicket, TipoProgramacion, idRuta,
                                                                                                         IdConductor, TotalEntregado, Usuario);
        }

        public DataTable ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas(string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas(CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarAdelantosPlanilla(string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarAdelantosPlanilla(CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarAdelantosAdicionales(int IdOperacion, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarAdelantosAdicionales(IdOperacion, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarVuelto(string Planilla, int RG, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarVuelto(Planilla, RG, Usuario); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarConductores(int OpcionVC, string Filtro)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarConductores(OpcionVC, Filtro); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor(int OpcionVC, int Persona)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor(OpcionVC, Persona); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarAsistencias(string Periodo, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                   DateTime FechaFin, string Codigo, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarAsistencias(Periodo, Persona, DiasPendientes, FechaInicio, FechaFin, Codigo, Usuario); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarVacaciones(string Periodo, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                    DateTime FechaFin, string Codigo, int FechaRetorno, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarVacaciones(Periodo, Persona, DiasPendientes, FechaInicio, FechaFin, Codigo, FechaRetorno, Usuario); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarCompensaciones(string Periodo, int Persona, int DiasPendientes, DateTime FechaInicio,
                                                                                        DateTime FechaFin, string Codigo, int FechaRetorno, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarCompensaciones(Periodo, Persona, DiasPendientes, FechaInicio, FechaFin, Codigo, FechaRetorno, Usuario); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionesVC(int OpcionVC, string Periodo, string Conductor, string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionesVC(OpcionVC, Periodo, Conductor, Operacion); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC(string Periodo, string Conductor, string Operacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC(Periodo, Conductor, Operacion); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionesVC(int OpcionVC, string Periodo, int idProgVC, int IDPersona, DateTime FechaRetorno)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionesVC(OpcionVC, Periodo, idProgVC, IDPersona, FechaRetorno); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionesVC(int OpcionVC, string Periodo, int idProgVC, int IDPersona, DateTime FechaRetorno, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionesVC(OpcionVC, Periodo, idProgVC, IDPersona, FechaRetorno, Usuario); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionConductor(int OpcionVC, int idProgVC, int IDPersona, DateTime FechaRetorno)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionConductor(OpcionVC, idProgVC, IDPersona, FechaRetorno); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionSPRING(int OpcionVC, string Periodo, int idProgVC, int IDPersona, DateTime FechaRetorno, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionSPRING(OpcionVC, Periodo, idProgVC, IDPersona, FechaRetorno, Usuario); }

        public DataTable ReportesApp_Operaciones_ProgramacionVC_ListarRutasXConductor(int Persona)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ProgramacionVC_ListarRutasXConductor(Persona); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems(int Opcion, string CodigoItem)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems(Opcion, CodigoItem); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarImplemento(int Opcion, int idItemV, string CodigoItemAlmacen, string CodigoInterno, int IDCategoria, string Descripcion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarImplemento(Opcion, idItemV, CodigoItemAlmacen, CodigoInterno, IDCategoria, Descripcion, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems(int Opcion, int NumeroItem, int Persona, int idTracto, int idCarreta, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems(Opcion, NumeroItem, Persona, idTracto, idCarreta, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitVolcan_ListarRegistros(string Tracto, string Carreta, string Empleado, string Implemento)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_ListarRegistros(Tracto, Carreta, Empleado, Implemento); }

        public DataTable ReportesApp_Operaciones_Previajes_RegistrarDespacho(int CodigoPreviaje, DateTime Fecha, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Previajes_RegistrarDespacho(CodigoPreviaje, Fecha, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarPreviajeR(int NroTicket)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPreviajeR(NroTicket); }

        public DataTable ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaR(int NroTicket, string Planilla, int idConductorR, decimal GastoRuta, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaR(NroTicket, Planilla, idConductorR, GastoRuta, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_RegistrarEditarCT(int Opcion, int idRegistroCT, int idTracto, DateTime FechaRevision, string TipoCT, int Cantidad, string Estado,
                                                                                      string LugarRevision, int PersonaR, byte[] ImagenHallazgo, string Usuario)
        {
            return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarCT(Opcion, idRegistroCT, idTracto, FechaRevision, TipoCT, Cantidad, Estado, LugarRevision,
                                                                                                      PersonaR, ImagenHallazgo, Usuario);
        }

        public DataTable ReportesApp_Operaciones_ControlItems_RepararCT(int idRegistroCT, DateTime FechaReparacion, byte[] ImagenReparacion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_RepararCT(idRegistroCT, FechaReparacion, ImagenReparacion, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarConosTacos(string FechaInicio, string FechaFin, string Placa, string TipoCT, string Estado)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarConosTacos(FechaInicio, FechaFin, Placa, TipoCT, Estado); }

        public DataTable ReportesApp_Operaciones_Programacion_EliminarConsolidados(string OT, int CodViaje)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Programacion_EliminarConsolidados(OT, CodViaje); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems(int Opcion, string CodigoItem)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems(Opcion, CodigoItem); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarImplemento(int Opcion, int idItemL, string CodigoItemAlmacen, string CodigoInterno, int IDCategoria, string Descripcion, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarImplemento(Opcion, idItemL, CodigoItemAlmacen, CodigoInterno, IDCategoria, Descripcion, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems(int Opcion, int NumeroItem, int Persona, int idTracto, int idCarreta, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems(Opcion, NumeroItem, Persona, idTracto, idCarreta, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_KitLimagas_ListarRegistros(string Tracto, string Carreta, string Empleado, string Implemento)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_ListarRegistros(Tracto, Carreta, Empleado, Implemento); }

        public DataTable ReportesApp_Operaciones_ControlItems_DesbloqueoTractoXRuta(int Opcion, int idDesbloqueo, int idTracto, DateTime FechaCompromiso, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_DesbloqueoTractoXRuta(Opcion, idDesbloqueo, idTracto, FechaCompromiso, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarUnidadesDesbloqueadas(string Tracto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarUnidadesDesbloqueadas(Tracto); }

        public DataTable ReportesApp_Operaciones_ControlItems_EditarKitNeumatico(int idTracto, int Persona, byte[] ImagenKN1, byte[] ImagenKN2, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_EditarKitNeumatico(idTracto, Persona, ImagenKN1, ImagenKN2, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCortinera(int Opcion, int idCortinera, int IdTracto, string Tecle, decimal Cantidad, decimal Capacidad, byte[] Imagen, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCortinera(Opcion, idCortinera, IdTracto, Tecle, Cantidad, Capacidad, Imagen, Usuario); }

        public DataTable ReportesApp_Operaciones_ControlItems_ListarCortinera(string Opcion, string Placa, string Estado)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_ListarCortinera(Opcion, Placa, Estado); }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarSogas(int Opcion, int idCortineraS, int IdTracto, string EstadoSoga, int Cantidad, byte[] Imagen, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarSogas(Opcion, idCortineraS, IdTracto, EstadoSoga, Cantidad, Imagen, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_InsertarViaticosRuta(string CodGasto, decimal GastoOriginal, int idRutaNueva, decimal NuevoGasto, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_InsertarViaticosRuta(CodGasto, GastoOriginal, idRutaNueva, NuevoGasto, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_BuscarPlanillaRutas(string CodGasto)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_BuscarPlanillaRutas(CodGasto); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ListarHistorialRutas(string Ruta, string FechaInicio, string FechaFin, int IdOperacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ListarHistorialRutas(Ruta, FechaInicio, FechaFin, IdOperacion); }

        public DataTable ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCamaras(int Opcion, int idCortineraC, int IdTracto, string EstadoCamara, int Cantidad, byte[] Imagen, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCamaras(Opcion, idCortineraC, IdTracto, EstadoCamara, Cantidad, Imagen, Usuario); }

        public DataTable ReportesApp_Operaciones_TicketGasto_ActualizarLiquidaciones(string CodGasto, string NroRUC, string NroDocumento, decimal MontoAfecto, decimal MontoNoAfecto, decimal MontoImpuestos)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_TicketGasto_ActualizarLiquidaciones(CodGasto, NroRUC, NroDocumento, MontoAfecto, MontoNoAfecto, MontoImpuestos); }

        public DataTable ReportesApp_Operaciones_Capacitaciones_RegistrarEditarDocumentos(int Opcion, int idCapacitacion, string Capacitacion, DateTime Fecha, string Base,
                                                                                          string Instructor, byte[] Archivo, string Titulo, string Extension, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Capacitaciones_RegistrarEditarDocumentos(Opcion, idCapacitacion, Capacitacion, Fecha, Base, Instructor, Archivo, Titulo, Extension, Usuario); }

        public DataTable ReportesApp_Operaciones_Capacitaciones_RegistrarAsistentes(int Opcion, int idCapacitacion, int idCapacitacionD, int idConductor, int Examen, string Estado,
                                                                                    int Teoria, int Revision, int Circuito)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Capacitaciones_RegistrarAsistentes(Opcion, idCapacitacion, idCapacitacionD, idConductor, Examen, Estado, Teoria, Revision, Circuito); }

        public DataTable ReportesApp_Operaciones_Capacitaciones_ListarDocumentos(string Titulo, string Conductor, string FechaInicio, string FechaFin)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Capacitaciones_ListarDocumentos(Titulo, Conductor, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Operaciones_Capacitaciones_ListarAsistentes(int idCapacitacion)
        { return clsOperacionesDAO.Instancia.ReportesApp_Operaciones_Capacitaciones_ListarAsistentes(idCapacitacion); }

        public DataTable ReportesApp_ListarGuiasElectronicas_ListarPendientes(string fechaInicio, string fechaFin, string viaje, string Cliente)
        { return clsOperacionesDAO.Instancia.ReportesApp_ListarGuiasElectronicas_ListarPendientes(fechaInicio, fechaFin, viaje, Cliente); }

        public DataTable ReportesApp_Listar_SerieGuiasManuales()
        { return clsOperacionesDAO.Instancia.ReportesApp_Listar_SerieGuiasManuales(); }

        public DataTable ReportesApp_Listar_GuiasManuales(string FechaInicio, string FechaFin, string Serie, string Numero, string Estado)
        { return clsOperacionesDAO.Instancia.ReportesApp_Listar_GuiasManuales(FechaInicio, FechaFin, Serie, Numero, Estado); }

        public DataTable ReportesApp_GuiasManuales_SolicitarAnularGuias(int Opcion, int idSolicitud, string Serie, string Numero, string Usuario)
        { return clsOperacionesDAO.Instancia.ReportesApp_GuiasManuales_SolicitarAnularGuias(Opcion, idSolicitud, Serie, Numero, Usuario); }
    }
}
