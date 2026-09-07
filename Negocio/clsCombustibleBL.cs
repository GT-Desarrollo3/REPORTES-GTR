using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsCombustibleBL
    {
        private readonly static clsCombustibleBL instancia = new clsCombustibleBL();

        public static clsCombustibleBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataDespachos(string fini, string ffin, string conductor, string tracto)
        {
            return clsCombustibleDAO.Instancia.GetDataDespachos(fini, ffin, conductor, tracto);
        }

        public DataTable GetDataRendimiento(string fini, string ffin, string placa, string cliente)
        {
            return clsCombustibleDAO.Instancia.GetDataRendimiento(fini, ffin, placa, cliente);
        }

        public DataTable GetDataRendimientoUnidad(string fini, string ffin, string tipo, string subtipo)
        {
            return clsCombustibleDAO.Instancia.GetDataRendimientoUnidad(fini, ffin, tipo, subtipo);
        }

        public DataTable GetDataDespachosDiarios(string fini, string ffin, string conductor, string tracto, int filtro)
        {
            return clsCombustibleDAO.Instancia.GetDataDespachosDiarios(fini, ffin, conductor, tracto, filtro);
        }

        public DataTable ObtenerReporteSurtidorAnexadoAlKardex(string fini, string ffin, string conductor, string tracto, int filtro)
        {
            return clsCombustibleDAO.Instancia.ObtenerReporteSurtidorAnexadoAlKardex(fini, ffin, conductor, tracto, filtro);
        }

        public DataTable ObtenerReporteKardex_Surtidor_Otros(string Periodo,  int filtro)
        {
            return clsCombustibleDAO.Instancia.ObtenerReporteKardex_Surtidor_Otros(Periodo, filtro);
        }

        public DataTable GetCombustible_StatusUnidades(string usuario)
        {
            return clsCombustibleDAO.Instancia.GetCombustible_StatusUnidades(usuario);
        }
        public DataTable GetCombustible_StatusUnidadesPorRegiones(string usuario)
        {
            return clsCombustibleDAO.Instancia.GetCombustible_StatusUnidadesPorRegiones(usuario);
        }
        public DataTable GetCombustible_StatusUnidadesPorCiudad(string usuario)
        {
            return clsCombustibleDAO.Instancia.GetCombustible_StatusUnidadesPorCiudad(usuario);
        }
        public DataTable GetCombustible_StatusUnidadesPorEstado(string usuario)
        {
            return clsCombustibleDAO.Instancia.GetCombustible_StatusUnidadesPorEstado(usuario);
        }
        public DataTable GetCombustible_StatusUnidadesPorVelocidad(string usuario)
        {
            return clsCombustibleDAO.Instancia.GetCombustible_StatusUnidadesPorVelocidad(usuario);
        }

        public DataTable GetCombustible_PreciosDiarios(string fecha )
        {
            return clsCombustibleDAO.Instancia.GetCombustible_PreciosDiarios(fecha);
        }

        public DataTable GetCombustible_PreciosDiarios_Nuevo_Modifa_elimina(int accion,string fecha, decimal precio,string user)
        {
            return clsCombustibleDAO.Instancia.GetCombustible_PreciosDiarios_Nuevo_Modifa_elimina(accion,fecha, precio, user);
        }

        public DataTable GetDataProgramacionPorPlaca(string Placa, string fini, string ffin)
        {
            return clsCombustibleDAO.Instancia.GetDataProgramacionPorPlaca(Placa, fini, ffin);
        }



        public DataTable GetDataDespachosDetalleDiarios(int idconductor,string FechaInicioDetalle, string FechaFinDetalle, string ConductorDetalle)
        {
            return clsCombustibleDAO.Instancia.GetDataDespachosDetalleDiarios(idconductor,FechaInicioDetalle, FechaFinDetalle, ConductorDetalle);
        }

        public DataTable GetCombustible_RegularizarTicktesalKardex(int tipo,int ticket, int codigopreviaje, string placa, string dni, string conductor, decimal 
                                                                                    odometro, string Usuario)
        {
            return clsCombustibleDAO.Instancia.GetCombustible_RegularizarTicktesalKardex(tipo,ticket, codigopreviaje, placa, dni, conductor, odometro, Usuario);
        }

        public DataTable Reporte_Combustible_Detalle_Viaje_Diario(int IdViaje, String Codigo)
        {
            return clsCombustibleDAO.Instancia.Reporte_Combustible_Detalle_Viaje_Diario(IdViaje, Codigo);
        }

        public DataTable getRegistrarBloqueoUnidadxRuta(int accion, int idTracto, string xmlRuta, string MotivoBloqueo, string Usuario)
        {
            return clsCombustibleDAO.Instancia.getRegistrarBloqueoUnidadxRuta(accion, idTracto, xmlRuta, MotivoBloqueo, Usuario);
        }

        public DataTable getListarUnidadesBloqueadas()
        {
            return clsCombustibleDAO.Instancia.getListarUnidadesBloqueadas();
        }

        public DataTable GetBuscarUnidadesBloqueadas(string BuscarPlaca)
        {
            return clsCombustibleDAO.Instancia.GetBuscarUnidadesBloqueadas(BuscarPlaca);
        }

        public DataTable ReportesApp_Combusible_ListarRegistro_UnidadesBloqueadas(int idTracto, string Motivo)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combusible_ListarRegistro_UnidadesBloqueadas(idTracto, Motivo);
        }

        public DataTable ReportesApp_Combusible_ListarHistorial_UnidadesBloqueadas (string fini, string ffin, string Placa)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combusible_ListarHistorial_UnidadesBloqueadas(fini, ffin, Placa);
        }

        public DataTable ReportesApp_Combusible_EliminarRuta_UnidadesBloqueadas(int idRuta, int idTracto, string Motivo)
        { return clsCombustibleDAO.Instancia.ReportesApp_Combusible_EliminarRuta_UnidadesBloqueadas(idRuta, idTracto, Motivo); }

        //GERARDO - 10/11
        public DataTable ReportesApp_Combustible_MetasRendimiento_ListarCombo(int Opcion, string Marca)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarCombo(Opcion, Marca);
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_ListarMetas(int IdOperacion, string TituloRuta, string Marca)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarMetas(IdOperacion, TituloRuta, Marca);
        }

        //GERARDO - 16/12
        public DataTable ReportesApp_Combustible_MetasRendimiento_Registrar(int IdOperacion, int idRegion, string TituloRuta, string Marca, int idModelo,
                                                                            decimal Meta, string xmlRuta, string Usuario)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_MetasRendimiento_Registrar(IdOperacion, idRegion, TituloRuta, Marca, idModelo, Meta, xmlRuta, Usuario);
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_ListarRutasRendimiento(string Descripcion)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarRutasRendimiento(Descripcion);
        }
        //GERARDO - 16/12

        //GERARDO - 22/11
        public DataTable ReportesApp_Combustible_MetasRendimiento_FiltrarMetas(int idMetaRendimiento, int Anio)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_MetasRendimiento_FiltrarMetas(idMetaRendimiento, Anio);
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_EliminarMetas(int idMetaRendimiento, int Anio, int idRutaEnlazada)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_MetasRendimiento_EliminarMetas(idMetaRendimiento, Anio, idRutaEnlazada);
        }

        public DataTable ReportesApp_Combustible_MetasRendimiento_ActualizarMetas(int idMetaRendimiento, int Anio, decimal Meta, decimal Promedio, string Usuario)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_MetasRendimiento_ActualizarMetas(idMetaRendimiento, Anio, Meta, Promedio, Usuario);
        }
        //GERARDO - 22/11



        public bool ReportesApp_Combustible_RegistrarUreaPorSucursal(string sucursal, int idplaca, string placa, string fecha, string cantidad, int idconductor)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_RegistrarUreaPorSucursal(sucursal, idplaca, placa, fecha, cantidad,idconductor);
        }

        public DataTable ReportesApp_Combustible_ListarUreaPorAlmacenes(string fechaInicio, string FechaFin)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_ListarUreaPorAlmacenes(fechaInicio, FechaFin);
        }

        public bool ReportesApp_Combustible_AnularUrea(string notasalida)
        {
            return clsCombustibleDAO.Instancia.ReportesApp_Combustible_AnularUrea(notasalida);
        }

        public DataTable ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque(int Opcion, int idInspeccionT, int idVehiculo, string TipoAlerta, DateTime Fecha, string Usuario)
        { return clsCombustibleDAO.Instancia.ReportesApp_Combustible_InspeccionTanque_IngresarInspeccionTanque(Opcion, idInspeccionT, idVehiculo, TipoAlerta, Fecha, Usuario); }

        public DataTable ReportesApp_Combustible_InspeccionTanque_ListarInspeccionTanque(string Vehiculo, string FechaInicio, string FechaFin, string TipoInspeccion)
        { return clsCombustibleDAO.Instancia.ReportesApp_Combustible_InspeccionTanque_ListarInspeccionTanque(Vehiculo, FechaInicio, FechaFin, TipoInspeccion); }

        public DataTable ReportesApp_Combustible_InspeccionTanque_BuscarInspeccion(int idVehiculo)
        { return clsCombustibleDAO.Instancia.ReportesApp_Combustible_InspeccionTanque_BuscarInspeccion(idVehiculo); }
    }
}
