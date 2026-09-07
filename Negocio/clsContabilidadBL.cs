using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;


namespace Negocio
{
    public class clsContabilidadBL
    {
        private readonly static clsContabilidadBL instancia = new clsContabilidadBL();

        public static clsContabilidadBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetDataMayorDetallado(string perini, string perfin, string comp, string documento)
        {
            return clsContabilidadDAO.Instancia.GetDataMayorDetallado(perini, perfin, comp, documento);
        }
        
        public DataTable GetDataSumarizado(string fechaini,string fechafin,string tipofecha) 
        {
            return clsContabilidadDAO.Instancia.GetDataSumarizado(fechaini, fechafin, tipofecha);
        }
        //Clase Resumen De Sumarizado con Importe de Viajes Completados
        public DataTable GetDataSumarizadoImporte(string fechaini, string fechafin, string filtro)
        {
            return clsContabilidadDAO.Instancia.GetDataSumarizadoImporte(fechaini, fechafin,filtro);
        }

        public DataTable GetDataLibro(int tipo,String fechaPeriodo, String codigoEmpresa)
        {
            return clsContabilidadDAO.Instancia.GetDataLibro(tipo,fechaPeriodo, codigoEmpresa);
        }

        public DataTable GetCajaChicaRepGastos(string compañia, string uni_rep, string tipodoc, string uni_neg, string fechaini, string fechafin,
            int docdesde, int dochasta, string concepto, string estado, int beneficiario, int proveedor, string centrocostos)
        {
            return clsContabilidadDAO.Instancia.GetDataCajaChicaRepGastos(compañia, uni_rep, tipodoc, uni_neg, fechaini, fechafin,
            docdesde,dochasta,concepto,estado,beneficiario,proveedor,centrocostos);
        }

        public DataTable GetViajesPorFacturar(string fechaini, string fechafin,int cliente)
        {
            return clsContabilidadDAO.Instancia.GetViajesPorFacturar(fechaini, fechafin,cliente);
        }
        public DataTable GetViajesPorFacturar_Resumen(string fechaini)
        {
            return clsContabilidadDAO.Instancia.GetViajesPorFacturar_Resumen(fechaini);
        }

        public void UpdateFechaGuia(DateTime fechaguia, string viaje)
        {
            clsContabilidadDAO.Instancia.UpdateFechaGuia(fechaguia, viaje);
        }

        public void UpdateFechaGuia_Sumarizado(DateTime fechaguia, string viaje, string detalle)
        {
            clsContabilidadDAO.Instancia.UpdateFechaGuia_Sumarizado(fechaguia, viaje, detalle);
        }

        public bool UpdateViajePF(string viaje, string situacion, string detalle)
        {
            return clsContabilidadDAO.Instancia.UpdateViajePF(viaje, situacion, detalle);
        }

        public DataTable GetFacturas(string compania,string fechaini, string fechafin, int cliente, string busqueda, int Filtro)
        {
            return clsContabilidadDAO.Instancia.GetFacturas(compania, fechaini, fechafin, cliente, busqueda, Filtro);
        }
        public DataTable GetReporte19(string perini, string perfin, string comp, string cuentaini, string cuentafin)
        {
            return clsContabilidadDAO.Instancia.GetReporte19(perini, perfin, comp, cuentaini, cuentafin);
        }

        public DataTable GetAdelantosAplicados(string fechaini, string fechafin)
        {
            return clsContabilidadDAO.Instancia.GetAdelantosAplicados(fechaini,fechafin);
        }

        public DataTable GetTipoServicios(string fechaini, string fechafin, string tipofecha) 
        {
            return clsContabilidadDAO.Instancia.GetTipoServicios(fechaini, fechafin, tipofecha);
        }

        public DataTable GetTipoServiciosCompañia(string fechaini, string fechafin, string tipofecha, string compañia)
        {
            return clsContabilidadDAO.Instancia.GetTipoServiciosCompañia(fechaini, fechafin, tipofecha,compañia);
        }

        public DataTable GetDataRepGastosViaje(string compañia, string uni_rep, string uni_neg, string fechaini, string fechafin,
            int docdesde, int dochasta, string concepto, string estado, int beneficiario, int proveedor, string centrocostos)
        {
            return clsContabilidadDAO.Instancia.GetDataRepGastosViaje(compañia, uni_rep, uni_neg, fechaini, fechafin,
            docdesde, dochasta, concepto, estado, beneficiario, proveedor, centrocostos);
        }

        public DataTable GetAnalisisGV(string periodoini, string periodofin, string cuentaini, string cuentafin)
        {
            return clsContabilidadDAO.Instancia.GetAnalisisGV(periodoini, periodofin, cuentaini, cuentafin);
        }

        public DataTable GetValidacionAdelantos(string fechaini, string fechafin, string periodoini, string periodofin)
        {
            return clsContabilidadDAO.Instancia.GetValidacionAdelantos(fechaini, fechafin, periodoini, periodofin);
        }

        public DataTable GetValidacioCuentasxCobrar(string periodoini, int cliente)
        {
            return clsContabilidadDAO.Instancia.GetValidacioCuentasxCobrar(periodoini, cliente);
        }

        public DataTable GetComercialvsContabilidad(string periodoini, string periodofin)
        {
            return clsContabilidadDAO.Instancia.GetComercialvsContabilidad(periodoini, periodofin);
        }

        public DataTable GetCListaObligaciones(string fechaini, string fechafin, int cliente)
        {
            return clsContabilidadDAO.Instancia.GetCListaObligaciones(fechaini, fechafin, cliente);
        }

        public DataTable GetCListaPlanillas(string fechaini, string fechafin, string tipo, string usuario, int cliente)
        {
            if (tipo == "Por_Aprobar")
            {
                return clsContabilidadDAO.Instancia.GetCListaObligaciones(fechaini, fechafin, cliente);
            }
            else
            {
                return clsContabilidadDAO.Instancia.GetCListaObligacionesAprobadas(fechaini, fechafin, usuario, cliente);
            }
        }

        public DataTable GetGuardaObligaciones(string documento, string proveedor, string fecha, string usuario)
        {
            return clsContabilidadDAO.Instancia.GetGuardaObligaciones(documento, proveedor, fecha, usuario);
        }

        public DataTable GetListaAdelantos(string obligacion, string proveedor)
        {
            return clsContabilidadDAO.Instancia.GetListaAdelantos(obligacion, proveedor);
        }

        public DataTable GetVoucherPeriodo(string CajaChica)
        {
            return clsContabilidadDAO.Instancia.GetVoucherPeriodo(CajaChica);
        }

        public void GetCambiarPeriodoVoucher(string obligacion, string periodo)
        {
            clsContabilidadDAO.Instancia.GetCambiarPeriodoVoucher(obligacion, periodo);
        }

        public DataTable GetControlFacturas(string compañia, string fechaini, string fechafin, int cliente, string facturas)
        {
            return clsContabilidadDAO.Instancia.GetControlFacturas(compañia, fechaini, fechafin, cliente, facturas);
        }
        public DataTable GetPeajes_ImportarDataExcel01(string xml, string usuario)
        {
            return clsContabilidadDAO.Instancia.GetPeajes_ImportarDataExcel01(xml, usuario);
        }
        public DataTable GetPeajes_Ver(int Opcion, string DatoFiltro1, string DatoFiltro2, string DatoFiltro3)
        {
            return clsContabilidadDAO.Instancia.GetPeajes_Ver(Opcion, DatoFiltro1, DatoFiltro2, DatoFiltro3);
        }
        public DataTable GetPeajes_Vincular(string xmlpeaje, string xmlviaje, int IDPeaje, string CodigoViaje, string Ruta, string Usuario)
        {
            return clsContabilidadDAO.Instancia.GetPeajes_Vincular(xmlpeaje, xmlviaje, IDPeaje, CodigoViaje, Ruta, Usuario);
        }
        public DataTable GetPeajes_Desvincular(int IDPeaje, string Usuario)
        {
            return clsContabilidadDAO.Instancia.GetPeajes_Desvincular(IDPeaje, Usuario);
        }


        public DataTable GetLista_KpiTransporte(string anio)
        {
            return clsContabilidadDAO.Instancia.GetLista_KpiTransporte(anio);
        }      

        //
        public DataTable ReportesApp_ListarViajes_AnexarGuias(string fechaInicio, string fechaFin, string viaje, string idConductor, string idRuta, int idEstadoViaje)
        {
            return clsContabilidadDAO.Instancia.ReportesApp_ListarViajes_AnexarGuias(fechaInicio, fechaFin, viaje, idConductor, idRuta, idEstadoViaje);
        }

        public DataTable ReportesApp_ListarOT_Viaje(int idViaje, int idConductor, string FechaProgramacion)
        {
            return clsContabilidadDAO.Instancia.ReportesApp_ListarOT_Viaje(idViaje, idConductor, FechaProgramacion);
        }

        public DataTable ReportesApp_ListarGuiasOT_Viaje(int idOT, int idViaje, string FechaProgramacion)
        {
            return clsContabilidadDAO.Instancia.ReportesApp_ListarGuiasOT_Viaje(idOT, idViaje, FechaProgramacion);
        }

        public Boolean ReportesApp_Actualizar_GuiaPorViaje(int idGuia, string CodViaje, string GuiaRemitente, string GuiaOtro, string Observaciones,string Peso,string guiaTransportista,string Serie,string Numero)
        {
            return clsContabilidadDAO.Instancia.ReportesApp_Actualizar_GuiaPorViaje(idGuia, CodViaje, GuiaRemitente, GuiaOtro, Observaciones,Peso, guiaTransportista,Serie,Numero);
        }

        public Boolean ReportesApp_Desvncular_GuiaRetorno(int idGuia)
        {
            return clsContabilidadDAO.Instancia.ReportesApp_Desvncular_GuiaRetorno(idGuia);
        }

        public DataTable ReportesApp_ListarComprobantesSire(string empresa,string Check_compras_ventas, string periodo)
        {
            return clsContabilidadDAO.Instancia.ReportesApp_ListarComprobantesSire(empresa,Check_compras_ventas,periodo);
        }

        public DataTable ReportesApp_Contabilidad_AbrirCerrarPeriodos(int Opcion, string Compania, string Periodo, string Usuario)
        { return clsContabilidadDAO.Instancia.ReportesApp_Contabilidad_AbrirCerrarPeriodos(Opcion, Compania, Periodo, Usuario); }

        public DataTable ReportesApp_Contabilidad_ListarPeriodosAbiertos(string Compania, string Periodo)
        { return clsContabilidadDAO.Instancia.ReportesApp_Contabilidad_ListarPeriodosAbiertos(Compania, Periodo); }

        public DataTable ReportesApp_Contabilidad_AbrirCerrarPeriodo(string Compania, string Modulo, string Estado, string Periodo, string Usuario)
        { return clsContabilidadDAO.Instancia.ReportesApp_Contabilidad_AbrirCerrarPeriodo(Compania, Modulo, Estado, Periodo, Usuario); }
    }
}
