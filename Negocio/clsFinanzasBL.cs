using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsFinanzasBL
    {
        private readonly static clsFinanzasBL instancia = new clsFinanzasBL();
        public static clsFinanzasBL Instancia
        {
            get { return instancia; }
        }
        public DataTable GetFacturacionDiaria(string fini, string ffin, string tipofecha, string transpesa, string bra, string filtro, string sucursal)
        {
            return clsFinanzasDAO.Instancia.GetFacturacionDiaria(fini, ffin, tipofecha, transpesa, bra, filtro, sucursal);
        }
        public DataTable GetPagoMasivo(string compania, string fini, string ffin, string banco, string moneda, string cuenta, string prepago)
        {
            return clsFinanzasDAO.Instancia.GetPagoMasivo(compania, fini, ffin, banco, moneda, cuenta, prepago);
        }
        public DataTable GetSaldosProveedoresFinan(string fini, string ffin, string proveedor)
        {
            return clsFinanzasDAO.Instancia.GetSaldosProveedoresFinan(fini, ffin, proveedor);
        }
        public DataTable GetSaldosProveedoresCont(string fini, string ffin, string proveedor)
        {
            return clsFinanzasDAO.Instancia.GetSaldosProveedoresCont(fini, ffin, proveedor);
        }

        public DataTable GetMovimientos(string compania, string cuenta, string fini, string ffin)
        {
            return clsFinanzasDAO.Instancia.GetMovimientos(compania, cuenta, fini, ffin);
        }

        public DataTable GetEstadoCuenta(string compañia, string fechaini, string fechafin, int cliente)
        {
            return clsFinanzasDAO.Instancia.GetEstadoCuenta(compañia, fechaini, fechafin, cliente);
        }

        public DataTable GetFacturacionContado(string fechaini, string fechafin, char sucursal)
        {
            return clsFinanzasDAO.Instancia.GetFacturacionContado(fechaini, fechafin, sucursal);
        }

        public DataTable GetFacturacionContadoActualizado(string fechaini, string fechafin, char sucursal)
        {
            return clsFinanzasDAO.Instancia.GetFacturacionContadoActualizado(fechaini, fechafin, sucursal);
        }
        public DataTable GetDetallePendientePagos(string fechaini, string fechafin, int Proveedor, string Empresa)
        {
            return clsFinanzasDAO.Instancia.GetDetallePendientePagos(fechaini, fechafin, Proveedor, Empresa);
        }
        public DataTable GetDetallePagos(string fechaini, string fechafin, int Proveedor, string Empresa)
        {
            return clsFinanzasDAO.Instancia.GetDetallePagos(fechaini, fechafin, Proveedor, Empresa);
        }

        public DataTable ObtenerLlenadoControlReporteDetallePagos()
        {
            DataTable dt = new DataTable();
            dt = clsFinanzasDAO.Instancia.ObtenerLlenadoControlReporteDetallePagos();
            return dt;
        }

        public DataTable GetListaProveedores()
        {
            return clsFinanzasDAO.Instancia.GetListaProveedores();
        }

        public DataTable GetListaClientes()
        {
            return clsFinanzasDAO.Instancia.GetListaClientes();
        }

        public void GetUpdateFechaRecepcion(DateTime fecha, string documento)
        {
            clsFinanzasDAO.Instancia.GetUpdateFechaRecepcion(fecha, documento);
        }

        public void GetModificarFechaRecepcion(DateTime fecha, string documento)
        {
            clsFinanzasDAO.Instancia.GetModificarFechaRecepcion(fecha, documento);
        }

        public DataTable GetListaCobranzasxClientes(string fechaini, string fechafin, string cliente)
        {
            return clsFinanzasDAO.Instancia.GetListaCobranzasxClientes(fechaini, fechafin, cliente);
        }

        public DataTable GetListaCobranzasxBancos(string fechaini, string fechafin, string banco)
        {
            return clsFinanzasDAO.Instancia.GetListaCobranzasxBancos(fechaini, fechafin, banco);
        }

        public DataTable GetClientes(string cliente)
        {
            return clsFinanzasDAO.Instancia.GetClientes(cliente);
        }

        public DataTable GetBancos(string banco)
        {
            return clsFinanzasDAO.Instancia.GetBancos(banco);
        }

        public DataTable GetListaClientesCobranzaDetallado(string fechaini, string fechafin, string cliente)
        {
            return clsFinanzasDAO.Instancia.GetListaClientesCobranzaDetallado(fechaini, fechafin, cliente);
        }

        public DataTable GetListaBancosCobranzaDetallado(string fechaini, string fechafin, string banco)
        {
            return clsFinanzasDAO.Instancia.GetListaBancosCobranzaDetallado(fechaini, fechafin, banco);
        }

        public DataTable GetProyeccionCobranza(string fechaini, string fechafin, int cliente)
        {
            return clsFinanzasDAO.Instancia.GetProyeccionCobranza(fechaini, fechafin, cliente);
        }

        public DataTable GetProyeccionCobranza2(string compañia, string fechaini, string fechafin, int cliente)
        {
            return clsFinanzasDAO.Instancia.GetProyeccionCobranza2(compañia, fechaini, fechafin, cliente);
        }

        public DataTable GetProyeccionCobranza2Resumen(string compañia, string fechaini, string fechafin, int cliente)
        {
            return clsFinanzasDAO.Instancia.GetProyeccionCobranza2Resumen(compañia, fechaini, fechafin, cliente);
        }

        //public DataTable GetProyeccionCobranza2(string transpesa, string bra, string altra, string fechaini, string fechafin, int cliente)
        //{
        //    return clsFinanzasDAO.Instancia.GetProyeccionCobranza2(transpesa, bra, altra, fechaini, fechafin, cliente);
        //}
        //public DataTable GetListaClientesCobranzaDetallado(string banco, string fechaini, string fechafin)
        //{
        //    return clsFinanzasDAO.Instancia.GetListaClientesCobranzaDetallado(banco, fechaini, fechafin);
        //}

        //public DataTable GetListaBancosCobranzaDetallado(string banco, string fechaini, string fechafin)
        //{
        //    return clsFinanzasDAO.Instancia.GetListaBancosCobranzaDetallado(banco, fechaini, fechafin);
        //}
        public DataTable GetListadoDeFacturas(string compania, string fechaini, string fechafin, string tipodocumento, int proveedor)
        {

            return clsFinanzasDAO.Instancia.GetListadoDeFacturas(compania, fechaini, fechafin, tipodocumento, proveedor);
        }

        public DataTable GetDESENLAZARRFACTURAS(string DocumentoRelacion)
        {
            return clsFinanzasDAO.Instancia.GetDESENLAZARRFACTURAS(DocumentoRelacion);
        }

        public DataTable GetModificarFactura(int opcion, string factura, string idviaje, string guia1, string codigoViaje, string Fechadocumento,
                                            string FechaVencimiento, decimal TramaFormaPago, string CompaniaSocio, string TipoDocumento, string comentario,
                                            string ubigeopartida, string ubigeollegada,string dirpartida,string dirllegada,decimal montoDetraccion)
        {

            return clsFinanzasDAO.Instancia.GetModificarFactura(opcion, factura, idviaje, guia1, codigoViaje, Fechadocumento, FechaVencimiento,
                                                                    TramaFormaPago,CompaniaSocio,TipoDocumento,comentario, ubigeopartida,
                                                                     ubigeollegada, dirpartida, dirllegada, montoDetraccion);
        }
        public DataTable GetListarFacturasFechas(string CompaniaSocio, string factura, string TipoDocumento)
        {
            return clsFinanzasDAO.Instancia.GetListarFacturasFechas(CompaniaSocio, factura, TipoDocumento);

        }


        public DataTable GetListarFacturasEliminadas(string compania, string factura, string tipodocumento)
        {
            return clsFinanzasDAO.Instancia.GetListarFacturasEliminadas(compania, factura, tipodocumento);

        }

        public DataTable GetOperaciones_ListarCompania(string CompaniaSocio)
        {
            return clsFinanzasDAO.Instancia.GetOperaciones_ListarCompania(CompaniaSocio);

        }

        public DataTable GetRegistrarAcesosxFactura(string Factura, string serie, string compania, string Usuario, int Descenlace, int ModificarMonto,
                                                    int EliminarFactura)
        {
            return clsFinanzasDAO.Instancia.GetRegistrarAcesosxFactura(Factura, serie, compania, Usuario,  Descenlace,  ModificarMonto, EliminarFactura);
        }

        public DataTable GetAccesosxFacturas(int Opcion,string factura, string compania, string tipodocumento,string Usuario)
        {
            return clsFinanzasDAO.Instancia.GetAccesosxFacturas(Opcion, factura, compania, tipodocumento, Usuario);
        
        }

        public DataTable GetListarHistoricoFacturas(string FechaInicio, string fechaFin)
        {
            return clsFinanzasDAO.Instancia.GetListarHistoricoFacturas(FechaInicio, fechaFin);
        }

        public DataSet ReportesApp_Costos_ControlPresupuestal_Listar(int Opcion, string Periodo, string CentroCosto, int Documento, int Inicio, int Final)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_Listar(Opcion, Periodo, CentroCosto, Documento, Inicio, Final); }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarCuadroComparativo(int Opcion, string Periodo, string CentroCosto)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarCuadroComparativo(Opcion, Periodo, CentroCosto); }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarCentroCosto(string Usuario)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarCentroCosto(Usuario); }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarDocumentos(int Opcion, string Periodo, string TipoDocumento, string NumeroDocumento, string Voucher)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarDocumentos(Opcion, Periodo, TipoDocumento, NumeroDocumento, Voucher); }

        public DataTable ReportesApp_Costos_ControlPresupuestal_GenerarPresupuesto(string xmlPresupuesto, string Usuario)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_GenerarPresupuesto(xmlPresupuesto, Usuario); }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarPresupuestos(int Opcion, string Periodo, string CentroCosto)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarPresupuestos(Opcion, Periodo, CentroCosto); }

        public DataTable ReportesApp_Costos_ControlPresupuestal_EliminarPresupuestos(int idPresupuesto)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_EliminarPresupuestos(idPresupuesto); }

        public DataSet ReportesApp_Costos_ControlPresupuestal_ListarPresupuestosCC(int Opcion, string Periodo, string CentroCosto)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarPresupuestosCC(Opcion, Periodo, CentroCosto); }

        public DataTable ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(int Opcion, string Periodo)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(Opcion, Periodo); }

        public DataTable ReportesApp_Costos_Cotizaciones_AgregarListarImplementos(int Opcion, string Tipo, string Descripcion)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_Cotizaciones_AgregarListarImplementos(Opcion, Tipo, Descripcion); }

        public DataTable ReportesApp_Costos_Cotizaciones_DetalleInsertar(int Opcion, int idCotizacionC, int idImplemento, string Usuario)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_Cotizaciones_DetalleInsertar(Opcion, idCotizacionC, idImplemento, Usuario); }

        public DataTable ReportesApp_Costos_Cotizaciones_DetalleListar(int idCotizacionC)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_Cotizaciones_DetalleListar(idCotizacionC); }


        public DataTable ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion(int Opcion, int idCotizacionC, int CO, int P, int T, int CI, int O, int idRuta,
                         string TipoViaje, string PuntoInicio, string PuntoFin, decimal Frecuencia, string Producto, string RUCCliente, decimal ValorProducto, string Telefono,
                         string Contacto, string Embalaje, string Responsable, DateTime Duracion, DateTime ContratoInicio, DateTime ContratoFin, string Permisos,
                         decimal Tonelaje, string PermisosAdicionales, DateTime HorarioIni, DateTime HorarioFin, string Flota, decimal Mermas,
                         string Standby, string Politicas, DateTime FechaInicio, int NroConductor, string Origen, string Usuario)
        {
            return clsFinanzasDAO.Instancia.ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion(Opcion, idCotizacionC, CO, P, T, CI, O, idRuta, TipoViaje, PuntoInicio,
                         PuntoFin, Frecuencia, Producto, RUCCliente, ValorProducto, Telefono, Contacto, Embalaje, Responsable, Duracion, ContratoInicio, ContratoFin,
                         Permisos, Tonelaje, PermisosAdicionales, HorarioIni, HorarioFin, Flota, Mermas, Standby, Politicas, FechaInicio, NroConductor,
                         Origen, Usuario);
        }

        public DataTable ReportesApp_Costos_Cotizaciones_ListarRegistroCotizaciones(string FechaInicio, string FechaFin, string Cliente, string Flota, string Estado)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_Cotizaciones_ListarRegistroCotizaciones(FechaInicio, FechaFin, Cliente, Flota, Estado); }

        public DataTable ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones(int Opcion, int idCotizacionC, string Historial, string Usuario)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones(Opcion, idCotizacionC, Historial, Usuario); }

        public DataTable ReportesApp_Costos_Cotizaciones_ListarDatos(int Opcion, string Ruta)
        { return clsFinanzasDAO.Instancia.ReportesApp_Costos_Cotizaciones_ListarDatos(Opcion, Ruta); }
    }
}
