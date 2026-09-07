using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;


namespace Negocio
{
    public class clsLogisticaBL
    {
        private readonly static clsLogisticaBL instancia = new clsLogisticaBL();

        public static clsLogisticaBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetServicios(string compania, string descripcion, string fechaini, string fechafin, int proveedor)
        {
            return clsLogisticaDAO.Instancia.GetDataServicios(compania, descripcion, fechaini, fechafin, proveedor);
        }
        public DataTable GetCompras(string compania, string periodoini, string periodofin, int proveedor)
        {
            return clsLogisticaDAO.Instancia.GetDataCompras(compania, periodoini, periodofin, proveedor);
        }

        public DataTable GetProductosxRotacion(string fechaini, string fechafin)
        {
            return clsLogisticaDAO.Instancia.GetDataProdcutosxRotacion(fechaini, fechafin);
        }
        public DataTable GetLogistica_InventarioValorizadoPeriodoCerrado(string Compania, string Almacen, string Periodo)
        {
            return clsLogisticaDAO.Instancia.GetLogistica_InventarioValorizadoPeriodoCerrado(Compania, Almacen, Periodo);
        }

        public DataTable ReportesApp_Logistica_ListarAlmacenesInventario(int Opcion, string Compania)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_ListarAlmacenesInventario(Opcion, Compania); }

        public DataTable GetListarPreciosCombustibleTerceros(int idProveedor,string lugar)
        {
            return clsLogisticaDAO.Instancia.GetListarPreciosCombustibleTerceros(idProveedor,lugar);
        }

        public DataTable GetListarPreciosCombustibleTerceros_Registrar(int opcion, string fecha, int idProveedor, decimal precio, string Lugar, string Producto)
        { return clsLogisticaDAO.Instancia.GetListarPreciosCombustibleTerceros_Registrar(opcion, fecha, idProveedor, precio, Lugar, Producto); }


        public DataTable GetModificarOST(int opcion, string ost, decimal montoactual, decimal montoModificado, string Usuario,string TipoMoneda)
        {
            return clsLogisticaDAO.Instancia.GetModificarOST(opcion, ost, montoactual, montoModificado, Usuario,TipoMoneda);
        }

        #region Sem requerimientos y cotizacion
        public Boolean ReportesApp_Reporte_Importe_Masivo_Requerimientos(String xml, String xmlDetalle, String xmlCotizacion,string igv,string tipo)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Reporte_Importe_Masivo_Requerimientos(xml, xmlDetalle, xmlCotizacion,igv,tipo);

        }

        #endregion 

        public DataTable ReportesApp_Logistica_ListarItems(string filtro, string CodigoAlmacen)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_ListarItems(filtro, CodigoAlmacen); }

        public String ReportesApp_Logistica_FiltrarItem(string filtro)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_FiltrarItem(filtro);
        }

        public DataTable ReportesApp_Logistica_InsertarAlertaStock(string Item, string DescripcionCompleta, int StockMinimo, int TiempoAlerta, string CodigoAlmacen)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_InsertarAlertaStock(Item, DescripcionCompleta, StockMinimo, TiempoAlerta, CodigoAlmacen); }

        public DataTable ReportesApp_Logistica_ListarAlertasStock(string filtro)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_ListarAlertasStock(filtro);
        }

        public DataTable ReportesApp_Logistica_EliminarAlertaStock(int IdAlerta)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EliminarAlertaStock(IdAlerta);
        }

        public DataTable ReportesApp_Logistica_AlertaStock_ListarAlmacenes()
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_AlertaStock_ListarAlmacenes(); }

        public DataTable ReportesApp_Mantenimiento_Reporte_RequerimientosALogistica(string fechaini, string fechafin, string Usuario, bool checkResumen)
        { return clsLogisticaDAO.Instancia.ReportesApp_Mantenimiento_Reporte_RequerimientosALogistica(fechaini, fechafin, Usuario, checkResumen); }

        public bool ReportesApp_Logistica_RegistrarReclamoCliente(int idCliente, string Cliente, string Correo, string Telefono, string Reclamo, string Contacto ,string DetalleReclamo, byte[] byteArrayImagen, string nombreImagen , string fechaIncidente, string observaciones, string txtNombrePDF,byte[] byteArrayPDF)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_RegistrarReclamoCliente(idCliente, Cliente, Correo, Telefono, Reclamo, Contacto , DetalleReclamo, byteArrayImagen, nombreImagen , fechaIncidente, observaciones,txtNombrePDF,byteArrayPDF);
        }

        public DataTable ReportesApp_Logistica_ListarReclamos(string fechaInicio, string fechaFin)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_ListarReclamos(fechaInicio, fechaFin);
        }

        public bool ReportesApp_Logistica_RegistrarDesargoCliente(string fechaProyectado, string descargo, int idReclamo, string responsable, byte[] byteArrayPDF)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_RegistrarDesargoCliente(fechaProyectado,descargo, idReclamo, responsable,byteArrayPDF);
        }

        public bool ReportesApp_Logistica_RegistrarSolucionCliente(string solucion, int idReclamo)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_RegistrarSolucionCliente(solucion, idReclamo);
        }

        public bool ReportesApp_Logistica_RegistrarNoSolucionCliente(string Respuesta, int idReclamo)
        {
           return clsLogisticaDAO.Instancia.ReportesApp_Logistica_RegistrarNoSolucionCliente(Respuesta, idReclamo);
        }

        public DataTable ReportesApp_Logistica_CentroCostos()
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_CentroCostos();
        }

        public DataTable ReportesApp_Logistica_Listar_Req_CentroCostos(string FECHAINI, string FECHAFIN, string CentroCosto)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_Listar_Req_CentroCostos(FECHAINI, FECHAFIN, CentroCosto);
        }

        public DataTable ReportesApp_Logistica_Listar_Req_CentroCostos_Grafico(string FECHAINI, string FECHAFIN)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_Listar_Req_CentroCostos_grafico(FECHAINI, FECHAFIN);
        }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ListarTransacciones(string NumeroOT, string FechaInicio, string FechaFin)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_TransaccionesMtto_ListarTransacciones(NumeroOT, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ListarDetalleTransaccion(int Opcion, string NumeroOT)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_TransaccionesMtto_ListarDetalleTransaccion(Opcion, NumeroOT); }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_InsertarEliminarItem(int Opcion, string CodigoItem, string DescripcionItem, string Usuario)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_TransaccionesMtto_InsertarEliminarItem(Opcion, CodigoItem, DescripcionItem, Usuario); }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ImprimirTicket(string NumeroOT, string xmlItem, string Usuario)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_TransaccionesMtto_ImprimirTicket(NumeroOT, xmlItem, Usuario); }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_ListarRegistroOT(string NumeroOT, string FechaInicio, string FechaFin)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_TransaccionesMtto_ListarRegistroOT(NumeroOT, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Logistica_TransaccionesMtto_EliminarTicketsImpresos(int idRegistroOT, string CodigoItem, string Usuario)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_TransaccionesMtto_EliminarTicketsImpresos(idRegistroOT, CodigoItem, Usuario); }

        public DataTable ReportesApp_Logistica_ItemsPendientes_Listar(string FechaInicio, string FechaFin, string CentroCosto, string Item, int Opcion)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_ItemsPendientes_Listar(FechaInicio, FechaFin, CentroCosto, Item, Opcion); }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_DetalleInsertar(int idEvaluacionC, string Proveedor, decimal PrecioUnitario, decimal FormaPago,
                                                                                 decimal PrecioTotal, decimal CostoFinanciero, decimal PrecioEquivalente)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleInsertar(idEvaluacionC, Proveedor, PrecioUnitario, FormaPago, PrecioTotal,
                                                                                                     CostoFinanciero, PrecioEquivalente);
        }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_DetalleListar(int idEvaluacionC)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleListar(idEvaluacionC); }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar(int Opcion, int idEvaluacionC, int idEvaluacionD, decimal Puntaje)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar(Opcion, idEvaluacionC, idEvaluacionD, Puntaje); }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_InsertarEvaluacion(int Opcion, int idEvaluacionC, string Servicio, string Unidad, decimal Cantidad,
                                                                                    decimal PrecioUnitario, decimal PrecioTotal, string Usuario)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EvaluacionOfertas_InsertarEvaluacion(Opcion, idEvaluacionC, Servicio, Unidad, Cantidad, PrecioUnitario, PrecioTotal, Usuario); }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_ListarEvaluaciones(string FechaInicio, string FechaFin, string Codigo)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EvaluacionOfertas_ListarEvaluaciones(FechaInicio, FechaFin, Codigo); }

        public DataTable ReportesApp_Logistica_EvaluacionOfertas_AgregarAdicionales(int Opcion, int idEvaluacionC, decimal Descuento, string Conclusiones)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EvaluacionOfertas_AgregarAdicionales(Opcion, idEvaluacionC, Descuento, Conclusiones); }

        public DataSet ReportesApp_Logistica_EvaluacionOfertas_ExportarExcel(int idEvaluacionC)
        { return clsLogisticaDAO.Instancia.ReportesApp_Logistica_EvaluacionOfertas_ExportarExcel(idEvaluacionC); }

        public DataTable ReportesApp_Logistica_ReporteConsumibles(string fechaini, string fechafin)
        {
            return clsLogisticaDAO.Instancia.ReportesApp_Logistica_ReporteConsumibles(fechaini, fechafin);
        }
    }
}
