using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsMantenimientoBL
    {
        private clsMantenimientoBL()
        {

        }

        private readonly static clsMantenimientoBL instancia = new clsMantenimientoBL();

        public static clsMantenimientoBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataConsumo(string fini, string ffin, string Grupo, string TipoMaquina, string Placa)
        { return clsMantenimientoDAO.Instancia.GetDataConsumo(fini, ffin, Grupo, TipoMaquina, Placa); }

        public DataTable GetDataCosto_Unidades()
        {
            return clsMantenimientoDAO.Instancia.GetDataCosto_Unidades();
        }

        public DataTable GetListaTractos()
        {
            return clsMantenimientoDAO.Instancia.GetDataTractos();
        }

        public DataTable GetKilometraje(string tracto)
        {
            return clsMantenimientoDAO.Instancia.GetKilometraje(tracto);
        }

        public DataTable GetMaestroMantenimiento()
        {
            return clsMantenimientoDAO.Instancia.GetMaestroMantenimiento();
        }

        public DataTable GetMantenimientoM1()
        {
            return clsMantenimientoDAO.Instancia.GetMantenimientoM1();
        }

        public DataTable GetMantenimientoM2()
        {
            return clsMantenimientoDAO.Instancia.GetMantenimientoM2();
        }

        public DataTable GetMantenimientoM3()
        {
            return clsMantenimientoDAO.Instancia.GetMantenimientoM3();
        }

        public DataTable GetMarcaTracto(string tracto)
        {
            return clsMantenimientoDAO.Instancia.GetMarcaTracto(tracto);
        }

        public DataTable GetInsertMantenimientoTracto(string tracto, string AceitMotor, string FiltComb, string Boyas, string Baterias, string CalibMotor, string Filtro1, string Filtro2, string FiltroAire, string Intercooler, string PuenteDelantero, string Alineamiento, string Sensores, string Alternador, string Arrancador, string TemplFajaAlternador, string TemplFajaVentilador, string TemplFajaBombaAgua, string Aceitaja_de_Cambios, string AceitCajaDirec, string MantBocamasa, string AceitCorona, string KMCambioAceiteMotor, string ViajesRestante, string ActUnidadOperativa, string Marca, string Modelo)
        {
            return clsMantenimientoDAO.Instancia.GetInsertMantenimientoTracto(tracto, AceitMotor, FiltComb, Boyas, Baterias, CalibMotor, Filtro1, Filtro2, FiltroAire, Intercooler, PuenteDelantero, Alineamiento, Sensores, Alternador, Arrancador, TemplFajaAlternador, TemplFajaVentilador, TemplFajaBombaAgua, Aceitaja_de_Cambios, AceitCajaDirec, MantBocamasa, AceitCorona, KMCambioAceiteMotor, ViajesRestante, ActUnidadOperativa, Marca, Modelo);
        }
        public DataTable GetPlanTracto()
        {
            return clsMantenimientoDAO.Instancia.GetPlanTracto();
        }
        public DataTable GetDataMantenimientos(int tipo, int mantenimiento, string tracto)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimientos(tipo, mantenimiento, tracto);
        }
        public DataTable GetActividadxUnidadOperativa(string AceitMotor, string FiltComb, string Boyas, string Baterias, string CalibMotor, string Filtro1, string Filtro2, string FiltroAire, string Intercooler, string PuenteDelantero, string Alineamiento, string Sensores, string Alternador, string Arrancador, string TemplFajaAlternador, string TemplFajaVentilador, string TemplFajaBombaAgua, string Aceitaja_de_Cambios, string AceitCajaDirec, string MantBocamasa, string AceitCorona)
        {
            return clsMantenimientoDAO.Instancia.GetActividadxUnidadOperativa(AceitMotor, FiltComb, Boyas, Baterias, CalibMotor, Filtro1, Filtro2, FiltroAire, Intercooler, PuenteDelantero, Alineamiento, Sensores, Alternador, Arrancador, TemplFajaAlternador, TemplFajaVentilador, TemplFajaBombaAgua, Aceitaja_de_Cambios, AceitCajaDirec, MantBocamasa, AceitCorona);
        }
        public DataTable GetDataMantenimiento_ControlHerramientas_CategoriaRegistra(string Categoria)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaRegistra(Categoria);
        }
        public DataTable GetDataMantenimiento_ControlHerramientas_CategoriaModificaElimina(int IDCategoria, string Categoria, int Opcion, string User)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaModificaElimina(IDCategoria, Categoria, Opcion, User);
        }
        public DataTable GetDataMantenimiento_ControlHerramientas_CategoriaListar()
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaListar();
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_ItemsAlmacenListar(string Item)
        { return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_ItemsAlmacenListar(Item); }

        public DataTable ReportesApp_Mantenimiento_ControlHerramientas_ListarHerramientasXAlmacen(string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_ListarHerramientasXAlmacen(Usuario);
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_HerramientaRegistro(string Descripcion, string ItemAlmacen, string CodidoInterno, int EsDeAlmacen, int IDCategoria, string user)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_HerramientaRegistro(Descripcion, ItemAlmacen, CodidoInterno, EsDeAlmacen, IDCategoria, user);
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_HerramientaElimina(int IDHerramienta)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_HerramientaElimina(IDHerramienta);
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_Herramientas_Listar(int Opcion, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_Herramientas_Listar(Opcion, Usuario);
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(int IDHerramienta, int Persona, string User, int Opcion)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(IDHerramienta, Persona, User, Opcion);
        }

        public DataTable GetDataMantenimiento_ControlHerramientas_Herramientas_PersonaHta_Listar(int Persona, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.GetDataMantenimiento_ControlHerramientas_Herramientas_PersonaHta_Listar(Persona, Usuario);
        }

        public DataTable GetMantenimiento_BloqueoUnidades(int Accion,int IdBloqueo, int previaje, int tipopreviaje, int idunidad, string Motivo,
                                                                        string Area, string Descripcion, string Usuario, int predeterminado, string fechaInicio, string FechaFin) 
        {
            return clsMantenimientoDAO.Instancia.GetMantenimiento_BloqueoUnidades(Accion, IdBloqueo, previaje, tipopreviaje, idunidad, Motivo, 
                                                                                    Area, Descripcion, Usuario,predeterminado, fechaInicio,FechaFin);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio()
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio();
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarRegistro(int NroProgramacion)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarRegistro(NroProgramacion);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarSistema(string NuevoSistema)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarSistema(NuevoSistema);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_Insertar(int NroTicket, int idTipoAuxilio, string TipoFalla, string Motivo, DateTime FechaInicio, DateTime HoraInicio, string Ubicacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_Insertar(NroTicket, idTipoAuxilio, TipoFalla, Motivo, FechaInicio, HoraInicio, Ubicacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ModificarFalla(int idFalla, int idTipoAuxilio, string TipoFalla, string Motivo, string Ubicacion, string UnidadAfectada, string Validacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ModificarFalla(idFalla, idTipoAuxilio, TipoFalla, Motivo, Ubicacion, UnidadAfectada, Validacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas(string NumeroPlaca, string FechaInicio, string FechaFin, int? idEstadoFalla, string Validacion, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas(NumeroPlaca, FechaInicio, FechaFin, idEstadoFalla, Validacion, Operacion); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarFallaEditar(int idFalla)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarFallaEditar(idFalla);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_EliminarFalla(int idFalla)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_EliminarFalla(idFalla);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion(int idFalla, string NombreTercero, string TelefonoTercero, int idTipoRecibo, string Comprobante, decimal MontoComprobante, string Tecnico, string idPlaca, decimal Galones, decimal PrecioUnitario, decimal PrecioTotal, decimal Monto, int idSistemaVehiculo, int idSubSistema, string Solucion, string Usuario, int MttoCorrectivo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion(idFalla, NombreTercero, TelefonoTercero, idTipoRecibo, Comprobante, MontoComprobante, Tecnico, idPlaca, Galones, PrecioUnitario, PrecioTotal, Monto, idSistemaVehiculo, idSubSistema, Solucion, Usuario, MttoCorrectivo); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucion(int idFalla)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucion(idFalla);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(string Filtro)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(Filtro);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte(string NroPlaca)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte(NroPlaca);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte2(string NroPlaca)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte2(NroPlaca); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarRecibos()
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarRecibos();
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarLiquidacion()
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarLiquidacion();
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos()
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos();
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos(int idSistemaVehiculo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos(idSistemaVehiculo); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanica(int idFalla, int valorSalida, string EstadoAuxilio, DateTime Fecha, DateTime Hora, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanica(idFalla, valorSalida, EstadoAuxilio, Fecha, Hora, Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanicaTerceros(int idFalla, DateTime FechaTermino, DateTime HoraTermino, DateTime FechaLlegada, DateTime HoraLlegada, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanicaTerceros(idFalla, FechaTermino, HoraTermino, FechaLlegada, HoraLlegada, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_LiquidarFallaMecanica(int idFalla, DateTime FechaLiquidacion, int idEstadoLiquidacion, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_LiquidarFallaMecanica(idFalla, FechaLiquidacion, idEstadoLiquidacion, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarMontoDetalle(string Descripcion, decimal Gasto, int TipoRecibo, string Comprobante, DateTime FechaGasto)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarMontoDetalle(Descripcion, Gasto, TipoRecibo, Comprobante, FechaGasto);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarMontoDetalle(int idFalla)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarMontoDetalle(idFalla);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_EliminarMontoDetalle(int idGastoDetalle)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_EliminarMontoDetalle(idGastoDetalle);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarMonto(int idFalla)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarMonto(idFalla);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarPrecioPetroleo()
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPrecioPetroleo();
        }
        // 01/06/2023

        public DataTable ReportesApp_Mantenimiento_ListarRepuestosParaSegundoUso(string item)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ListarRepuestosParaSegundoUso(item);
        }

        public DataTable ReportesApp_Mantenimiento_ListarItemsInventarioSegundoUso(string item, string Sucursal, int verInactivos)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ListarItemsInventarioSegundoUso(item, Sucursal, verInactivos); }

        public DataTable ReportesApp_Mantenimiento_Listar_Empleados_SegundoUso(string empleado)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Listar_Empleados_SegundoUso(empleado);
        }

        public bool ReportesApp_Mantenimiento_Quitar_Editar_ActivoSegundoUSo(int tipo, string idActivo, string Nombre, string unidadMedida, decimal cantidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Quitar_Editar_ActivoSegundoUSo(tipo, idActivo, Nombre, unidadMedida, cantidad); }

        public bool ReportesApp_Mantenimiento_RegistrarActivosSegundoUso(string nombre, string codigo, string cantidad, string unidadmedida, string Sucursal)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistrarActivosSegundoUso(nombre, codigo, cantidad, unidadmedida, Sucursal); }

        public bool ReportesApp_Mantenimiento_AsignarItemsAlmacen_ActivoSegundoUso_Empleado(string xmlItemsAlmacen, int idActivoSegundoUso, int idEmpleado,string ot,string vale,decimal cantidadUso, string placa, bool consumible)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignarItemsAlmacen_ActivoSegundoUso_Empleado(xmlItemsAlmacen, idActivoSegundoUso, idEmpleado,ot,vale,cantidadUso, placa, consumible);
        }

        public DataTable ReportesApp_Mantenimiento_Listar_VinculoRepuesto_ActivoSegundoUso_Empleado(string FechaInicio, string FechaFin, string Sucursal, string Activo, string OT, string Operacion, string Empleado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Listar_VinculoRepuesto_ActivoSegundoUso_Empleado(FechaInicio, FechaFin, Sucursal, Activo, OT, Operacion, Empleado); }

        public bool ReportesApp_Mantenimiento_DesvincularActivoSegundoUso_Repuesto_Empleado(string idActivo, string codigoRepuesto, int idEmpleado, string Motivo,string ot,string tipoOperacion)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_DesvincularActivoSegundoUso_Repuesto_Empleado(idActivo, codigoRepuesto, idEmpleado, Motivo, ot, tipoOperacion);
        }

        public DataTable Reportesapp_Mantenimiento_ListarOrdenesTrabajo(string ot)
        {
            return clsMantenimientoDAO.Instancia.Reportesapp_Mantenimiento_ListarOrdenesTrabajo(ot);
        }

        public DataTable ReportesApp_Mantenimiento_Historial_SegundoUso_Desvinculados()
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Historial_SegundoUso_Desvinculados();
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(string Placa)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(Placa);
        }

        /*
        public DataTable ReportesApp_Mantenimiento_Solicitudes_BuscarViajes(string Placa)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarViajes(Placa);
        }
        */

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(int Opcion, int idComponente)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarComponentes(Opcion, idComponente);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar(int idComponente, int idComponenteDetalle, int idPosicionLlanta, string Observacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar(idComponente, idComponenteDetalle, idPosicionLlanta, Observacion); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar2(int idSolicitud, int idComponente, int idComponenteDetalle, int idPosicionLlanta, string Observacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar2(idSolicitud, idComponente, idComponenteDetalle, idPosicionLlanta, Observacion); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Modificar(int idSolicitud, int idSolicitudDetalle, int idComponente, int idComponenteDetalle, int idPosicionLlanta, string Observacion)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Modificar(idSolicitud, idSolicitudDetalle, idComponente, idComponenteDetalle, idPosicionLlanta, Observacion);
        }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_Listar(int Opcion, int idSolicitud, int idSolicitudDetalle)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Listar(Opcion, idSolicitud, idSolicitudDetalle);
        }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_Editar(int Opcion, int idSolicitudDetalle, int IDC, string OT)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_Editar(Opcion, idSolicitudDetalle, IDC, OT);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_FiltrarOT(string Placa)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_FiltrarOT(Placa);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Insertar(int idTracto, int TipoProgramacion, string TipoMtto, string TipoTrabajo, decimal Kilometraje,
                                                                        int idCisterna, string UnidadFalla, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_Insertar(idTracto, TipoProgramacion, TipoMtto, TipoTrabajo, Kilometraje,
                                                                                                idCisterna, UnidadFalla, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Listar(string NumeroPlaca, string FechaInicio, string FechaFin, int idBase, string EstadoFalla, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_Listar(NumeroPlaca, FechaInicio, FechaFin, idBase, EstadoFalla, Operacion); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarSolicitud(int idSolicitud)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarSolicitud(idSolicitud);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Modificar(int idSolicitud, int Opcion, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_Modificar(idSolicitud, Opcion, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado(string Placa)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado(Placa);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Programar(int Opcion, int idSolicitud, DateTime FechaProgramacion, string Motivo, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_Programar(Opcion, idSolicitud, FechaProgramacion, Motivo, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_Pedidos(int Opcion, int idSolicitud, string Requerimiento, string Descripcion, DateTime FechaLlegadaP)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_Pedidos(Opcion, idSolicitud, Requerimiento, Descripcion, FechaLlegadaP); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarPedido(int idSolicitud)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarPedido(idSolicitud);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_BuscarNotaIngreso(int Opcion, int idSolicitud, string Requerimiento)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarNotaIngreso(Opcion, idSolicitud, Requerimiento); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarTalleres(string Taller)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarTalleres(Taller); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(string Taller)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(Taller); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarBases(int Opcion)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarBases(Opcion);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarEstado(string NumeroPlaca, string FechaInicio, string FechaFin, int Ubicacion, string Estado, string EstadoProg)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarEstado(NumeroPlaca, FechaInicio, FechaFin, Ubicacion, Estado, EstadoProg); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarComboRubros(int Opcion)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarComboRubros(Opcion);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarContacto(int Opcion, int idContacto, string Nombre, string Contacto, string Ubicacion, int idRubro, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarContacto(Opcion, idContacto, Nombre, Contacto, Ubicacion, idRubro, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarContactos(int idRubro, string Ubicacion)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarContactos(idRubro, Ubicacion);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_BuscarContactos(int idContacto)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_BuscarContactos(idContacto);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_EliminarContacto(int idContacto)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_EliminarContacto(idContacto);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarFechaEstimada(int OpcionFecha, int idSolicitud, DateTime FechaEstimada, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarFechaEstimada(OpcionFecha, idSolicitud, FechaEstimada, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarFechaRecepcion(int OpcionU, int idSolicitud, DateTime FechaRecepcion, int idBase, string UbicacionTaller, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarFechaRecepcion(OpcionU, idSolicitud, FechaRecepcion, idBase, UbicacionTaller, Usuario); } 

        public object ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(string ot,string codigoItem)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(ot, codigoItem);
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos(string Placa)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos(Placa);
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_RegistrarModificarBaterias(int opcion, int idBateria, string CodBateria, string Marca, string Modelo, int idVehiculo,
                                                                                              DateTime FechaInicio, int Duracion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_RegistrarModificarBaterias(opcion, idBateria, CodBateria, Marca, Modelo, idVehiculo, FechaInicio, Duracion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_ListarBaterias(int FiltroFechas, string TipoFecha, string CodBateria, string Placa, string FechaInicio, string FechaFin, int Estado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_ListarBaterias(FiltroFechas, TipoFecha, CodBateria, Placa, FechaInicio, FechaFin, Estado); }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_GenerarInspeccion(int idBateria, string CodBateria, int Intervalo, DateTime FechaCambio, DateTime FechaInspeccion,
                                                                                     decimal NivelCarga, decimal EstadoB, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_GenerarInspeccion(idBateria, CodBateria, Intervalo, FechaCambio, FechaInspeccion, NivelCarga, EstadoB, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria(int Opcion, int idBateria, string Motivo, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria(Opcion, idBateria, Motivo, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_FiltrarBaterias(int idBateria)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_FiltrarBaterias(idBateria);
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_RegistrarTraspaso(int idBateria, int idVehiculoAnt, int idVehiculoAct, string Motivo, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_RegistrarTraspaso(idBateria, idVehiculoAnt, idVehiculoAct, Motivo, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_ControlBaterias_ListarTraspasos(int Opcion, string CodBateria, string Placa, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlBaterias_ListarTraspasos(Opcion, CodBateria, Placa, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_ListarDefectuosos(string Placa, string FechaInicio, string FechaFin, string EstadoFalla)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_ListarDefectuosos(Placa, FechaInicio, FechaFin, EstadoFalla);
        }

        public DataTable ReportesApp_Mantenimiento_Consumo_ListarGrupoTipoMaquina(int Opcion, int idGrupo)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Consumo_ListarGrupoTipoMaquina(Opcion, idGrupo);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarTractos(string Placa, int TipoUnidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarTractos(Placa, TipoUnidad); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto(string Aceite)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto(Aceite); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarOperaciones(int idMttoOP)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarOperaciones(idMttoOP); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMtto(int Opcion, int idRegistro, int idVehiculo, int idTipoVehiculo, string Aceite, int Frecuencia, string Ubigeo,
                                                                                       DateTime UltimaFecha, decimal UltimoKM, string TipoMantenimiento, int idMttoOP, int PS, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMtto(Opcion, idRegistro, idVehiculo, idTipoVehiculo, Aceite, Frecuencia, Ubigeo,
                                                                                       UltimaFecha, UltimoKM, TipoMantenimiento, idMttoOP, PS, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMttos(string Placa, int idTipoVehiculo, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMttos(Placa, idTipoVehiculo, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_FiltrarMttos(int Opcion, int idRegistro)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_FiltrarMttos(Opcion, idRegistro); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMtto(string Placa, string FechaInicio, string FechaFin, int idTipoVehiculo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMtto(Placa, FechaInicio, FechaFin, idTipoVehiculo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControl(int idVehiculo, int idAccesorio, decimal KMCambio, DateTime FechaCambio, decimal Intervalo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControl(idVehiculo, idAccesorio, KMCambio, FechaCambio, Intervalo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto(int Opcion, int idVehiculo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto(Opcion, idVehiculo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesos(int idVehiculo, int idAccesorio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesos(idVehiculo, idAccesorio); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesos(int Opcion, int idProcesoMtto, int idVehiculo, decimal Kilometraje, decimal Intervalo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesos(Opcion, idProcesoMtto, idVehiculo, Kilometraje, Intervalo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_InsertarAccesorio(string Accesorio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_InsertarAccesorio(Accesorio); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKM(string Placa, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKM(Placa, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMUnidad(int idVehiculo, DateTime Fecha, decimal UltKM)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMUnidad(idVehiculo, Fecha, UltKM); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_InsertarIncidencias(int idFalla, string EstadoUnidad, string Descripcion, string TipoDanio, string Danio, string DescripcionDanio,
                                                                                           string GPS, string Recursos, string Observacion, byte[] imagen, byte[] falla, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_InsertarIncidencias(idFalla, EstadoUnidad, Descripcion, TipoDanio, Danio, DescripcionDanio, GPS,
                                                                                             Recursos, Observacion, imagen, falla, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ListarIncidencias(string Placa, int idOperacion, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ListarIncidencias(Placa, idOperacion, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(int Opcion, int idIncidenteC)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(Opcion, idIncidenteC); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems(string Item)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems(Item); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_RegistrarPresupuesto(int Opcion, int idIncidenteC, string TipoMaterial, string Material, string Unidad, decimal PrecioUnitario,
                                                                                            decimal Cantidad, decimal ImporteTotal)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_RegistrarPresupuesto(Opcion, idIncidenteC, TipoMaterial, Material, Unidad, PrecioUnitario, Cantidad, ImporteTotal); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_EliminarPresupuesto(int Opcion, int idIncidenteC, int idIncidenteD)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_EliminarPresupuesto(Opcion, idIncidenteC, idIncidenteD); }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ActualizarMontoSSOMAC(int idIncidenteC, decimal SubTotal, decimal IGV, decimal MontoTotal, string RutaLocal, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ActualizarMontoSSOMAC(idIncidenteC, SubTotal, IGV, MontoTotal, RutaLocal, Usuario); }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ListarEstadoIncidencias(int idIncidenteC)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ListarEstadoIncidencias(idIncidenteC); }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias(int Opcion, int idRegistroInc, string Estado, string Observacion,
                                                                                               byte[] imagen, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias(Opcion, idRegistroInc, Estado, Observacion, imagen, Usuario); }

        public DataTable ReportesApp_Seguridad_RegistroIncidencias_DesbloquearDescargo(int idIncidenteC, string RutaLocal, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Seguridad_RegistroIncidencias_DesbloquearDescargo(idIncidenteC, RutaLocal, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarMonto(int idFalla, decimal SubTotal, decimal IGV, decimal MontoTotal, string RutaLocal, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarMonto(idFalla, SubTotal, IGV, MontoTotal, RutaLocal, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarReporte(int idIncidenteC, string RutaLocal, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarReporte(idIncidenteC, RutaLocal, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_InsertarKMUnidad(string xmlDetalleSinTildes, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_InsertarKMUnidad(xmlDetalleSinTildes, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ModificarFrecuencia(int Opcion, int idVehiculo, string MaquinaCodigo, int Frecuencia)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ModificarFrecuencia(Opcion, idVehiculo, MaquinaCodigo, Frecuencia); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarOTProgramadas(string Placa, string Descripcion, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarOTProgramadas(Placa, Descripcion, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_InsertarMecanico(int Opcion, int Persona, string Codigo, string NombreCompleto, string Turno, string Compania)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_InsertarMecanico(Opcion, Persona, Codigo, NombreCompleto, Turno, Compania); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(int Opcion, string NumeroOrden)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico(Opcion, NumeroOrden); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico(int Opcion, int Persona, string NumeroOrden)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico(Opcion, Persona, NumeroOrden); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarHistorial(string Nombre, string FechaInicio, string FechaFin, int HorasExtra, string Estado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarHistorial(Nombre, FechaInicio, FechaFin, HorasExtra, Estado); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_PausarOTMecanico(int Persona, string NumeroOrden, string MotivoPausa)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_PausarOTMecanico(Persona, NumeroOrden, MotivoPausa); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarTiempoExtra(int Persona, string NumeroOrden, DateTime FechaActual)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarTiempoExtra(Persona, NumeroOrden, FechaActual); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AprobarHorasExtra(int idHistorial, int Estado, string NFechaIni, string NHoraIni, string NFechaFin, string NHoraFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AprobarHorasExtra(idHistorial, Estado, NFechaIni, NHoraIni, NFechaFin, NHoraFin); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarHorasExtra(int Persona, string NumeroOrden, string Turno, string NFechaIni, string NHoraIni, string NFechaFin, string NHoraFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarHorasExtra(Persona, NumeroOrden, Turno, NFechaIni, NHoraIni, NFechaFin, NHoraFin); }

        public DataSet ReportesApp_Mantenimiento_AsignacionOT_ListarRegistros(int Anio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarRegistros(Anio); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarAuxilios(string Placa, string Descripcion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarAuxilios(Placa, Descripcion); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_AsignarAuxilio(int Opcion, int Persona, int idFalla, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_AsignarAuxilio(Opcion, Persona, idFalla, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(int Opcion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(Opcion); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarMaquinas(int Opcion, string Placa, string TipoMaquina)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarMaquinas(Opcion, Placa, TipoMaquina); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMaquina(int Opcion, int idRegistroM, string MaquinaCodigo, string Aceite, int Frecuencia, string Dueno, string Ubicacion,
                                                                                          DateTime UltimaFecha, decimal UltimoKM, string TipoMantenimiento, int idMttoOP, int PS, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMaquina(Opcion, idRegistroM, MaquinaCodigo, Aceite, Frecuencia, Dueno, Ubicacion, UltimaFecha,
                                                                                       UltimoKM, TipoMantenimiento, idMttoOP, PS, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosMaquinas(string Placa, string TipoMaquina, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosMaquinas(Placa, TipoMaquina, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKMMaquina(string Placa, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKMMaquina(Placa, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMMaquinas(string MaquinaCodigo, DateTime Fecha, decimal UltKM)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMMaquinas(MaquinaCodigo, Fecha, UltKM); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMaquinarias(string Placa, string FechaInicio, string FechaFin, string TipoMaquina)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMaquinarias(Placa, FechaInicio, FechaFin, TipoMaquina); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlMaquinas(string MaquinaCodigo, int idAccesorio, decimal KMCambio, DateTime FechaCambio, decimal Intervalo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlMaquinas(MaquinaCodigo, idAccesorio, KMCambio, FechaCambio, Intervalo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoMaquinas(int Opcion, string MaquinaCodigo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoMaquinas(Opcion, MaquinaCodigo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosMaquinas(string MaquinaCodigo, int idAccesorio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosMaquinas(MaquinaCodigo, idAccesorio); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosMaquinas(int Opcion, int idProcesoMtto, string MaquinaCodigo, decimal Kilometraje, decimal Intervalo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosMaquinas(Opcion, idProcesoMtto, MaquinaCodigo, Kilometraje, Intervalo); }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_BuscarMaquinas(string Placa)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_BuscarMaquinas(Placa); }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket(string MaquinaCodigo, DateTime FechaProg, string Operacion, string TipoLavado, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket(MaquinaCodigo, FechaProg, Operacion, TipoLavado, Usuario); }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_ListarTicket(string CodLavado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_ListarTicket(CodLavado); }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_Listar(string CodLavado, string Placa, string FechaInicio, string FechaFin, string Estado, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_Listar(CodLavado, Placa, FechaInicio, FechaFin, Estado, Operacion); }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_EliminarTicket(string CodLavado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_EliminarTicket(CodLavado); }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_ActualizarTicket(string CodLavado, DateTime FechaProg, string Estado, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_ActualizarTicket(CodLavado, FechaProg, Estado, Usuario); }

        public DataTable ReportesApp_Mantenimiento_TicketsLavadero_MarcarValidacion(string CodLavado, Byte Validacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_MarcarValidacion(CodLavado, Validacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ListarHistorial_UnidadesBloqueadas(string fini, string ffin, string Placa)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ListarHistorial_UnidadesBloqueadas(fini, ffin, Placa); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarUbicacion(string Ubicacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarUbicacion(Ubicacion); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto(string Periodo, string Placa, string Programacion, string TipoUnidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto(Periodo, Placa, Programacion, TipoUnidad); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto(string Periodo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto(Periodo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursos(int Opcion, int idRecursoMtto, int idProcesoMtto, int idVehiculo,
                                                                                        string Item, string Descripcion, decimal Cantidad, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursos(Opcion, idRecursoMtto, idProcesoMtto, idVehiculo, Item, Descripcion, Cantidad, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorio(int idProcesoMtto, int idVehiculo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorio(idProcesoMtto, idVehiculo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursos(string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Actividad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursos(FechaInicio, FechaFin, Placa, TipoMaquina, Actividad); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursosMaquina(int Opcion, int idRecursoMtto, int idProcesoMtto, string MaquinaCodigo,
                                                                                               string Item, string Descripcion, decimal Cantidad, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursosMaquina(Opcion, idRecursoMtto, idProcesoMtto, MaquinaCodigo, Item, Descripcion, Cantidad, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_RegistrarEliminarRecursos(int Opcion, int idRecursoMtto, int idProcesoMtto, int idVehiculo, string Placa,
                                                                                            string Item, string Descripcion, decimal Cantidad, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_RegistrarEliminarRecursos(Opcion, idRecursoMtto, idProcesoMtto, idVehiculo, Placa, Item, Descripcion, Cantidad, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorioMaquina(int idProcesoMtto, string MaquinaCodigo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorioMaquina(idProcesoMtto, MaquinaCodigo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarResumenRecursos(string FechaInicio, string FechaFin, string Item)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarResumenRecursos(FechaInicio, FechaFin, Item); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarHEMecanico(string Nombre)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarHEMecanico(Nombre); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp(int Opcion, string CodigoComp, int Persona, string CFechaIni, string CHoraIni,
                                                                                     string CFechaFin, string CHoraFin, string TotalHE, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp(Opcion, CodigoComp, Persona, CFechaIni, CHoraIni, CFechaFin, CHoraFin, TotalHE, Usuario); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarCompensaciones(int Persona)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarCompensaciones(Persona); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_IngresarEliminarDesbloqueo(int Opcion, int idDesbloqueo, int idVehiculo, int idRuta, DateTime FechaCompromiso, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_IngresarEliminarDesbloqueo(Opcion, idDesbloqueo, idVehiculo, idRuta, FechaCompromiso, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarDesbloqueoUnidades(string Placa, string Ruta)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarDesbloqueoUnidades(Placa, Ruta); }

        public DataTable ReportesApp_Mantenimiento_Herramientas_IngresarMaletas(int Opcion, string CodMaleta, string TipoMaleta, int idPersona, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Herramientas_IngresarMaletas(Opcion, CodMaleta, TipoMaleta, idPersona, Usuario); }

        public DataTable ReportesApp_Mantenimiento_Herramientas_ListarMaletas(int Opcion, string Empleado, int idMaletaC, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Herramientas_ListarMaletas(Opcion, Empleado, idMaletaC, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta(int Opcion, int IDHerramienta, int idMaletaC, string CodMaleta, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta(Opcion, IDHerramienta, idMaletaC, CodMaleta, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlHerramientas_AsignarDevolverMaleta(int Opcion, int idMaletaC, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_AsignarDevolverMaleta(Opcion, idMaletaC, Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarTolvas(string Tracto, string Carreta, DateTime FechaViaje, int PersonaC, int idRuta, int idTipoAuxilio, string TipoFalla,
                                                                                  string Motivo, DateTime FechaInicio, DateTime HoraInicio, string Ubicacion, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarTolvas(Tracto, Carreta, FechaViaje, PersonaC, idRuta, idTipoAuxilio, TipoFalla, Motivo,
                                                                                                          FechaInicio, HoraInicio, Ubicacion, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_BloquearConductorMtto(int idConductor, string Motivo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_BloquearConductorMtto(idConductor, Motivo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_BuscarIncidencias(int idIncidenteC)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_BuscarIncidencias(idIncidenteC); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarIncidencias(int idIncidenteC, int idTipoAuxilio, string EstadoUnidad, string TipoFalla, DateTime FechaInicio, DateTime HoraInicio,
                                                                                             string Ubicacion, string Motivo, string Descripcion, string TipoDanio, string Danio, string GPS, string Recursos,
                                                                                             string Observacion, byte[] imagen, byte[] falla, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarIncidencias(idIncidenteC, idTipoAuxilio, EstadoUnidad, TipoFalla, FechaInicio, HoraInicio, Ubicacion,
                                                                                              Motivo, Descripcion, TipoDanio, Danio, GPS, Recursos, Observacion, imagen, falla, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMtto(string Periodo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMtto(Periodo); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMttoFecha(string FechaInicio, string FechaFin, string Placa, string Programacion, string TipoUnidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMttoFecha(FechaInicio, FechaFin, Placa, Programacion, TipoUnidad); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMttoFechas(string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMttoFechas(FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarActividades(string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Actividad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarActividades(FechaInicio, FechaFin, Placa, TipoMaquina, Actividad); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems(string Item)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems(Item); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion(string Placa, string Tipo, string SubTipo, string Operacion, string Marca, string Modelo, DateTime FechaProyectada, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion(Placa, Tipo, SubTipo, Operacion, Marca, Modelo, FechaProyectada, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(int Opcion, string Placa, int idInspeccionC)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(Opcion, Placa, idInspeccionC); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(int Opcion, int idInspeccionC, int idInspeccionD, string Estado, string Observacion, int Mecanico, int Electrico, int Neumatico, int Soldador,
                                                                                       DateTime FechaInicio, DateTime FechaFin, string Turno, string Sucursal)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(Opcion, idInspeccionC, idInspeccionD, Estado, Observacion, Mecanico, Electrico, Neumatico, Soldador, FechaInicio, FechaFin, Turno, Sucursal); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones(int Opcion, string Fecha, string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Sucursal)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones(Opcion, Fecha, FechaInicio, FechaFin, Placa, TipoMaquina, Sucursal); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarPedidosInspecciones(int idInspeccionC)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarPedidosInspecciones(idInspeccionC); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_InsertarPedidosInspecciones(int Opcion, int idNroPedido, int idInspeccionC, string Item, string Descripcion, decimal Cantidad, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_InsertarPedidosInspecciones(Opcion, idNroPedido, idInspeccionC, Item, Descripcion, Cantidad, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RequerimientoServicio_ListarCCosto(string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RequerimientoServicio_ListarCCosto(Usuario); }

        public DataTable ReportesApp_Mantenimiento_RequerimientoServicio_ListarReqServicio(string CentroCosto, string Descripcion, string Placa, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RequerimientoServicio_ListarReqServicio(CentroCosto, Descripcion, Placa, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_RegistroIncidencias_GenerarReporte(int Opcion, int FiltroFechas, string Periodo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_GenerarReporte(Opcion, FiltroFechas, Periodo); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarCanaletas(int Opcion, string Sucursal)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarCanaletas(Opcion, Sucursal); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarProgramacion(int Opcion, int idCanaletaProg, int idCanaleta, DateTime FechaProg, string Estado, string Observacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarProgramacion(Opcion, idCanaletaProg, idCanaleta, FechaProg, Estado, Observacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_EliminarProgramacion(int Opcion, int idCanaletaProg)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_EliminarProgramacion(Opcion, idCanaletaProg); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarProgramacion(string Sucursal, string Estado, string Canaletas, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarProgramacion(Sucursal, Estado, Canaletas, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarCambios(int Opcion, int idCCambio, int idCanaleta, DateTime FechaProg, string Observacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarCambios(Opcion, idCCambio, idCanaleta, FechaProg, Observacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarCambios(string Sucursal, string Canaleta, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarCambios(Sucursal, Canaleta, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_RegistrarCambioDetalle(int Opcion, int idCCambio, int idCCambioDetalle, DateTime FechaCambio, decimal LongitudCambio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarCambioDetalle(Opcion, idCCambio, idCCambioDetalle, FechaCambio, LongitudCambio); }

        public DataTable ReportesApp_Mantenimiento_ControlCanaletas_ListarCambiosDetalle(int idCCambio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarCambiosDetalle(idCCambio); }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarRegistros(string Placa, string Descripcion, string FechaInicio, string FechaFin, string Origen, string Estado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarRegistros(Placa, Descripcion, FechaInicio, FechaFin, Origen, Estado); }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto(int Opcion, int idMttoC, DateTime FechaProg, string OT, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto(Opcion, idMttoC, FechaProg, OT, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarOTProgramadas(string Placa, string OT)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarOTProgramadas(Placa, OT); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos(string NroReq)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos(NroReq); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_IngresarRequerimientos(string NroReq, string CentroCosto, string Proyecto, string Descripcion, DateTime FechaProgramada, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_IngresarRequerimientos(NroReq, CentroCosto, Proyecto, Descripcion, FechaProgramada, Usuario); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarTiposMtto(int Opcion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarTiposMtto(Opcion); }

        public DataTable ReportesApp_Mantenimiento_SolicitudDetalle_ListarActividades(string Placa, string Actividad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_SolicitudDetalle_ListarActividades(Placa, Actividad); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_InsertarTarea(int Opcion, int idSolicitudDetalle, int idSolicitud, string CodTarea, DateTime FechaInicio, DateTime FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarTarea(Opcion, idSolicitudDetalle, idSolicitud, CodTarea, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_RegistrarOrdenTrabajo(int idSolicitud, string Placa, string TipoMtto, string Clasificacion, string Ubicacion, string Descripcion,
                                                                                     DateTime FechaInicio, DateTime FechaFin, int Mecanico, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_RegistrarOrdenTrabajo(idSolicitud, Placa, TipoMtto, Clasificacion, Ubicacion, Descripcion, FechaInicio,
                                                                                                             FechaFin, Mecanico, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos(int Opcion, string Periodo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos(Opcion, Periodo); }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_MapearMecanicos(int IDPersona, string Periodo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_MapearMecanicos(IDPersona, Periodo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_QuitarMecánicos(int IDPersona, string Periodo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_QuitarMecánicos(IDPersona, Periodo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_ListarTablaMecanicos(string Periodo, string Nombre, string Puesto)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ListarTablaMecanicos(Periodo, Nombre, Puesto); }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_IngresarEstadoAsistencia(string Codigo, string Estado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_IngresarEstadoAsistencia(Codigo, Estado); }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_RegistrarAsistencia(string Periodo, string xmlAsistencia, string Codigo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_RegistrarAsistencia(Periodo, xmlAsistencia, Codigo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_EliminarAsistencia(string Periodo, string xmlAsistencia, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_EliminarAsistencia(Periodo, xmlAsistencia, Usuario); }

        public DataTable ReportesApp_Mantenimiento_RoosterProyectado_ImportarAsistencia(string Periodo, string xmlAsistencia, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ImportarAsistencia(Periodo, xmlAsistencia, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MovimientoC_RegistrarEditarComponentes(int Opcion, int idMovimientoC, int idSistemaVehiculo, int idSubSistema, string Descripcion,
                         int idUnidadProcedencia, int idUnidadDestino, DateTime FechaEjecucion, string Motivo, int PersonaAutorizada, string Requerimiento, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MovimientoC_RegistrarEditarComponentes(Opcion, idMovimientoC, idSistemaVehiculo, idSubSistema, Descripcion,
                                                 idUnidadProcedencia, idUnidadDestino, FechaEjecucion, Motivo, PersonaAutorizada, Requerimiento, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MovimientoC_ListarComponentes(int FiltroS, string NumeroPlaca, string FechaInicio, string FechaFin, int idSistema, int idSubSistema)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MovimientoC_ListarComponentes(FiltroS, NumeroPlaca, FechaInicio, FechaFin, idSistema, idSubSistema); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumplimiento(string Placa, string Operacion, string TipoUnidad, string Marca, string Ubigeo, string MttoPreventivo,
                                                                                      DateTime FechaProgramada, DateTime FCInicio, DateTime FCFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumplimiento(Placa, Operacion, TipoUnidad, Marca, Ubigeo, MttoPreventivo, FechaProgramada, FCInicio, FCFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarCumplimiento(int Anio, int NroSemana, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarCumplimiento(Anio, NroSemana, Operacion); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(int Opcion, int Nro, string TipoMtto, DateTime FechaProgramada, string Estado, string Observacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(Opcion, Nro, TipoMtto, FechaProgramada, Estado, Observacion); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento(int Opcion, int Nro)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento(Opcion, Nro); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarMttosProgramados(int Anio, int NroSemana)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarMttosProgramados(Anio, NroSemana); }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarCalendarioMtto(string Periodo, string Placa, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarCalendarioMtto(Periodo, Placa, Operacion); }

        public DataTable ReportesApp_Mantenimiento_MttoCorrectivo_ListarMttoProgramado(string Placa, DateTime FechaProg)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarMttoProgramado(Placa, FechaProg); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(int Opcion, int Anio, int NroSemana)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(Opcion, Anio, NroSemana); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgruparOperacion(int Opcion, int Anio, int NroSemana)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparOperacion(Opcion, Anio, NroSemana); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_GenerarSolicitud(int PSolicitud, DateTime FechaRequerida, string Direccion, string Detalle, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_GenerarSolicitud(PSolicitud, FechaRequerida, Direccion, Detalle, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_Listar(string FechaInicio, string FechaFin, string Ticket, string Conductor, string Area, string Estado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_Listar(FechaInicio, FechaFin, Ticket, Conductor, Area, Estado); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_CrearTicket(string xmlTicket, int PConductor, int Unidad, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_CrearTicket(xmlTicket, PConductor, Unidad, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_ListarTickets(string FechaInicio, string FechaFin, string Ticket, string Conductor)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_ListarTickets(FechaInicio, FechaFin, Ticket, Conductor); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_IngresarKMs(int idTicketC, decimal KMSalida, decimal KMIngreso, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_IngresarKMs(idTicketC, KMSalida, KMIngreso, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_EliminarSolicitudes(int Opcion, string CodOperativo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_EliminarSolicitudes(Opcion, CodOperativo); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_AsignarConductor(string CodOperativo, int PConductor, int Unidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_AsignarConductor(CodOperativo, PConductor, Unidad); }

        public DataTable ReportesApp_Mantenimiento_ControlOperativos_Filtrar(string CodOperativo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlOperativos_Filtrar(CodOperativo); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarUnidadTaller(string NumeroPlaca)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidadTaller(NumeroPlaca); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ModificarUbicacion(int idSolicitud, int idBase, string Taller, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ModificarUbicacion(idSolicitud, idBase, Taller, Usuario); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarEmpleados(string Empleado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarEmpleados(Empleado); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpInspeccion(string Placa, string Operacion, string TipoUnidad, string Marca, DateTime ProxInspeccion,
                         DateTime FCInicio, DateTime FCFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpInspeccion(Placa, Operacion, TipoUnidad, Marca, ProxInspeccion, FCInicio, FCFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpInspeccion(int Anio, int NroSemana, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpInspeccion(Anio, NroSemana, Operacion); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpInspecciones(int Anio, int NroSemana)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpInspecciones(Anio, NroSemana); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpInspeccion(int Opcion, int Nro, DateTime FechaCump, string Estado, string Observacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpInspeccion(Opcion, Nro, FechaCump, Estado, Observacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpActividad(string Placa, string Operacion, string TipoUnidad, string Actividad, DateTime ProxFecha,
                          DateTime FCInicio, DateTime FCFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpActividad(Placa, Operacion, TipoUnidad, Actividad, ProxFecha, FCInicio, FCFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpActividad(int Anio, int NroSemana, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpActividad(Anio, NroSemana, Operacion); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpActividades(int Anio, int NroSemana)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpActividades(Anio, NroSemana); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpActividades(int Opcion, int Nro, DateTime FechaCump, string Estado, string Observacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpActividades(Opcion, Nro, FechaCump, Estado, Observacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosH(string Periodo, string Placa, string TipoUnidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosH(Periodo, Placa, TipoUnidad); }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ActualizarHorasDisp()
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ActualizarHorasDisp(); }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosP(string Periodo, string Placa, string TipoUnidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosP(Periodo, Placa, TipoUnidad); }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_ActualizarPorcDisp()
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ActualizarPorcDisp(); }

        public DataTable ReportesApp_Mantenimiento_Disponibilidad_PromedioMes(string Periodo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Disponibilidad_PromedioMes(Periodo); }

        public DataTable ReportesApp_Mantenimiento_ControlInventario_ListarSedeArea(int Opcion, string Sede)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlInventario_ListarSedeArea(Opcion, Sede); }

        public DataTable ReportesApp_Mantenimiento_ControlInventario_ListarSillas(string Periodo, string Placa, string TipoUnidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlInventario_ListarSillas(Periodo, Placa, TipoUnidad); }

        public DataTable ReportesApp_Mantenimiento_ControlInventario_InsertarModificar(int Opcion, int idTicket, string TipoActivo, string Sede, string Area, int idPersona,
                                                                                       string NombrePersona, string Observacion, byte[] CodigoBarras, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlInventario_InsertarModificar(Opcion, idTicket, TipoActivo, Sede, Area, idPersona, NombrePersona,
                                                                                                               Observacion, CodigoBarras, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMO(int Opcion, int idManoObra, int idProcesoMtto, int idVehiculo,
                                                                                              string Especialidad, string Tiempo, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMO(Opcion, idManoObra, idProcesoMtto, idVehiculo, Especialidad, Tiempo, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMOMaquinas(int Opcion, int idManoObra, int idProcesoMtto, string MaquinaCodigo,
                                                                                                      string Especialidad, string Tiempo, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMOMaquinas(Opcion, idManoObra, idProcesoMtto, MaquinaCodigo, Especialidad, Tiempo, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_ManoObra_ListarManoObra(string FechaInicio, string FechaFin, string Placa, string TipoMaquina, string Especialidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ManoObra_ListarManoObra(FechaInicio, FechaFin, Placa, TipoMaquina, Especialidad); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_ListarResumen(string FechaInicio, string FechaFin, string Especialidad)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_ListarResumen(FechaInicio, FechaFin, Especialidad); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_CrearAnalisisFallas(int idFalla, string Tracto, string Carreta, string Operacion, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_CrearAnalisisFallas(idFalla, Tracto, Carreta, Operacion, Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas(int idFalla, int Contador, string Respuesta, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas(idFalla, Contador, Respuesta, Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarAnalisisFallas(string NumeroPlaca, string Operacion, string FechaInicio, string FechaFin, string Estado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarAnalisisFallas(NumeroPlaca, Operacion, FechaInicio, FechaFin, Estado); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_InsertarEliminarRaizFalla(int Opcion, int idAnalisisFalla, string RaizFalla, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarEliminarRaizFalla(Opcion, idAnalisisFalla, RaizFalla, Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucionAuxilio(int idFalla, string Servicio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucionAuxilio(idFalla, Servicio); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(Usuario); }

        public DataTable ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo(string Placa)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo(Placa); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarEquipos(int Opcion, int idRegistro, string Codigo, int Periodo, string TipoMaquina, string EquipoNombre,
                                                                                          string Equipo, string Ubicacion, string Marca, string Modelo, DateTime UltimaFecha, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarEquipos(Opcion, idRegistro, Codigo, Periodo, TipoMaquina, EquipoNombre, Equipo,
                                                                                                                  Ubicacion, Marca, Modelo, UltimaFecha, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosEquipos(string Equipo, string Tipo, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosEquipos(Equipo, Tipo, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialEquipos(string Equipo, string FechaInicio, string FechaFin, string TipoMaquina)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialEquipos(Equipo, FechaInicio, FechaFin, TipoMaquina); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlEquipos(int idRegistro, int idAccesorio, DateTime FechaCambio, int Periodo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlEquipos(idRegistro, idAccesorio, FechaCambio, Periodo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoEquipos(int idRegistro)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoEquipos(idRegistro); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosEquipos(int idRegistro, int idAccesorio)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosEquipos(idRegistro, idAccesorio); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosEquipos(int Opcion, int idProcesoMtto, int idRegistro, int Periodo)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosEquipos(Opcion, idProcesoMtto, idRegistro, Periodo); }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema(int Opcion, int idTecnica)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema(Opcion, idTecnica); }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_RegistrarModificar(string Placa, int idTecnica, int idSistema, DateTime Fecha, string TipoAceite,
                                                                                     string Recomendacion, string Estado, string DirectorioPDF, string Usuario)
        {
            return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_RegistrarModificar(Placa, idTecnica, idSistema, Fecha, TipoAceite, Recomendacion,
                                                                                                             Estado, DirectorioPDF, Usuario);
        }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_ListarRegistro(string Placa, int TipoUnidad, int idTecnica, int idSistema)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarRegistro(Placa, TipoUnidad, idTecnica, idSistema); }

        public DataTable ReportesApp_Mantenimiento_MttoPredictivo_ListarHistorial(string FechaInicio, string FechaFin, string Placa, int TipoUnidad, int idTecnica, int idSistema)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarHistorial(FechaInicio, FechaFin, Placa, TipoUnidad, idTecnica, idSistema); }

        public DataTable ReportesApp_Mantenimiento_CompDesgaste_CrearComponente(int idPlaca, int idComponente, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_CompDesgaste_CrearComponente(idPlaca, idComponente, Usuario); }

        public DataSet ReportesApp_Mantenimiento_CompDesgaste_ListarComponentes(int Opcion, string Placa, string Operacion, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_CompDesgaste_ListarComponentes(Opcion, Placa, Operacion, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteC(int idPlaca, int idComponente, DateTime FechaAnterior, decimal Milimetro, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteC(idPlaca, idComponente, FechaAnterior, Milimetro, Usuario); }

        public DataTable ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteT(int idPlaca, int idComponente, DateTime FechaAnterior, string Actividad, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteT(idPlaca, idComponente, FechaAnterior, Actividad, Usuario); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarReprogramacion(string Placa, string Operacion, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarReprogramacion(Placa, Operacion, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_Solicitudes_ListarUbicaciones(string Placa, string Operacion, string FechaInicio, string FechaFin)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUbicaciones(Placa, Operacion, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorInspeccion(int Opcion, DateTime Fecha, string FechaInicio, string FechaFin, string Placa, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorInspeccion(Opcion, Fecha, FechaInicio, FechaFin, Placa, Operacion); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorUnidades(int Opcion, DateTime Fecha, string Operacion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorUnidades(Opcion, Fecha, Operacion); }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos(int Opcion)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos(Opcion); }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_RegistrarEditarNeumaticos(int Opcion, string Codigo, string DOT, string Marca, string Medida, string Modelo, string Tipo,
                         decimal NSK, decimal KM, decimal Precio, DateTime FechaInicio, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_RegistrarEditarNeumaticos(Opcion, Codigo, DOT, Marca, Medida, Modelo, Tipo, NSK, KM, Precio, FechaInicio, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_ListarNeumaticos(string Codigo, string Marca, string Estado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarNeumaticos(Codigo, Marca, Estado); }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_InstalarDesinstalar(int Opcion, int idMovimientoN, string Codigo, string Marca, int idVehiculo,
                         string Tipo, int Posicion, DateTime Fecha, decimal KM, decimal NSK, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_InstalarDesinstalar(Opcion, idMovimientoN, Codigo, Marca, idVehiculo, Tipo, Posicion, Fecha, KM, NSK, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ControlNeumaticos_ListarMovimientos(string FechaInicio, string FechaFin, string Codigo, string Placa)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ControlNeumaticos_ListarMovimientos(FechaInicio, FechaFin, Codigo, Placa); }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimiento(string Requerimiento)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimiento(Requerimiento); }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientoDetalle(string Requerimiento, string CodigoItem)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientoDetalle(Requerimiento, CodigoItem); }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_RegistrarRequerimientos(string xmlItemsAlmacen, int idActivoSegundoUso, int idEmpleado, string Requerimiento, decimal cantidadUso, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_RegistrarRequerimientos(xmlItemsAlmacen, idActivoSegundoUso, idEmpleado, Requerimiento, cantidadUso, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientos(string FechaInicio, string FechaFin, string Sucursal, string Activo, string Requerimiento, string Empleado)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_ListarRequerimientos(FechaInicio, FechaFin, Sucursal, Activo, Requerimiento, Empleado); }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_DesvincularRequerimientos(int idActivo, int idEmpleado, string Requerimiento, string Motivo, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_DesvincularRequerimientos(idActivo, idEmpleado, Requerimiento, Motivo, Usuario); }

        public DataTable ReportesApp_Mantenimiento_ActivoSegundoUso_HistorialRequerimientos()
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_HistorialRequerimientos(); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(int Opcion, int idCapacitacionD, int idCapacitacionC, string Estado, int Persona, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion(Opcion, idCapacitacionD, idCapacitacionC, Estado, Persona, Usuario); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_ListarAsistentes(int idCapacitacionC)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarAsistentes(idCapacitacionC); }

        public DataTable ReportesApp_Mantenimiento_AsignacionOT_RegistrarCapacitacion(int Opcion, int idCapacitacionC, string Empresa, string Capacitador, 
                                                                                      string Tema, DateTime FechaInicio, DateTime FechaFin, string Usuario)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_RegistrarCapacitacion(Opcion, idCapacitacionC, Empresa, Capacitador, Tema, FechaInicio, FechaFin, Usuario); }

        public DataSet ReportesApp_Mantenimiento_AsignacionOT_ListarCapacitaciones(string FechaInicio, string FechaFin, string Estado, string Tema, string Asistente)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarCapacitaciones(FechaInicio, FechaFin, Estado, Tema, Asistente); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarDesviacion(int Anio, int NroSemana, string Operacion, string TipoMtto)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarDesviacion(Anio, NroSemana, Operacion, TipoMtto); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_ListarSolicitudes(string NroPlaca)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarSolicitudes(NroPlaca); }

        public DataTable ReportesApp_Mantenimiento_MttoPreventivo_VincularSolicitudes(int Opcion, int Nro, int idSolicitud)
        { return clsMantenimientoDAO.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_VincularSolicitudes(Opcion, Nro, idSolicitud); }
    }
}
