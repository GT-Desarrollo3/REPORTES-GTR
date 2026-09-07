using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsAlmacenBL 
    {

        private readonly static clsAlmacenBL instancia = new clsAlmacenBL();

        public static clsAlmacenBL Instancia 
        {
            get { return instancia; }
        }
        public string Mensaje()
        {
            return clsAlmacenDAO.Instancia.Mensaje();
        }

        public DataTable GetOrdenesRetiro(string fechaini, string fechafin, string cliente, string producto, string lote)
        {
            return clsAlmacenDAO.Instancia.GetDataOrdenesRetiro(fechaini, fechafin, cliente, producto, lote);
        }

        public DataTable GetIngresos(string fechaini, string fechafin, string cliente, string producto, string lote)
        {
            return clsAlmacenDAO.Instancia.GetDataIngresos(fechaini, fechafin, cliente, producto, lote);
        }

        public DataTable GetDespachos(string fechaini, string fechafin, string cliente, string producto, string lote, string modo)
        {
            return clsAlmacenDAO.Instancia.GetDataDespachos(fechaini, fechafin, cliente, producto, lote, modo);
        }

        public DataTable GetSaldos(string fechaini, string fechafin, string cliente, string producto, string lote,int grid)
        {
            return clsAlmacenDAO.Instancia.GetDataSaldos(fechaini, fechafin, cliente, producto, lote,grid);
        }

        public DataTable GetFacturacionAlmacen(string fechaini, string fechafin, char tipofecha, char reporte) 
        {
            return clsAlmacenDAO.Instancia.GetFacturacionAlmacen(fechaini,fechafin,tipofecha,reporte);
        }
        public DataTable GetData_Operaciones_Consultas(int TipoConsulta, int IDCliente, int IDContrato, int IDOT,string Dato)
        {
            return clsAlmacenDAO.Instancia.GetData_Operaciones_Consultas(TipoConsulta, IDCliente, IDContrato, IDOT, Dato);
        }

        public DataTable GetData_Operaciones_GrabarOperacion(int IDCliente, string Descripcion, int TipoOP,string Motonave, string Usuario)
        {
            return clsAlmacenDAO.Instancia.GetData_Operaciones_GrabarOperacion(IDCliente, Descripcion, TipoOP, Motonave, Usuario);
        }
        public DataTable GetData_Operaciones_GrabarEnlace_OP_OT(int AnioOP, int NroOP, int IDOT, int IDCliente, decimal TotalRetiroTN, string Usuario)
        {
            return clsAlmacenDAO.Instancia.GetData_Operaciones_GrabarEnlace_OP_OT( AnioOP,  NroOP,  IDOT,  IDCliente,TotalRetiroTN,  Usuario);
        }
      


        public DataTable GetListarTicket(string fechinicio, string fechfin)
        {
            return clsAlmacenDAO.Instancia.GetListarTicket(fechinicio, fechfin);
        }

        public DataTable GetListarTicketTop30()
        {
            return clsAlmacenDAO.Instancia.GetListarTicketTop30();
        }

        public DataTable GetModificarFecha(string Fechainicio, string FechaFin, string NroTicket, string Usuario)
        {
            return clsAlmacenDAO.Instancia.GetModificarFecha(Fechainicio, FechaFin, NroTicket, Usuario);
        }

        public DataTable GetModificarTicket(int ot, string NumeroPesaje, decimal PesoInicial, decimal PesoFinal, decimal CantidadBase, decimal CantidadUso, string GuiaC, string GuiaT, string Usuario, string fechaInicio, string fechaFin, string Lote, string Ubicacion)
        {
            return clsAlmacenDAO.Instancia.GetModificarTicket(ot, NumeroPesaje, PesoInicial, PesoFinal, CantidadBase, CantidadUso, GuiaC, GuiaT, Usuario, fechaInicio, fechaFin, Lote, Ubicacion);
        }

        public DataTable GetListarTickets(int ot, string NumeroPeaje)
        {
            return clsAlmacenDAO.Instancia.GetListarTickets(ot, NumeroPeaje);
        }

        public DataTable GetListarClientes(int codigo, string RazonSocial,string DocumentoFiscal)
        {
            return clsAlmacenDAO.Instancia.GetListarClientes(codigo, RazonSocial, DocumentoFiscal);
        }

        public DataTable GetTransferirCliente(int Persona, string Usuario)
        {
            return clsAlmacenDAO.Instancia.GetTransferirCliente(Persona, Usuario);
        }

        public DataTable GetListarUltimoCliente()
        {
            return clsAlmacenDAO.Instancia.GetListarUltimoCliente();
        }


        public DataTable GetListarClientesSalaverry(int codigo, string RazonSocial, string DocumentoFiscal)
        {
            return clsAlmacenDAO.Instancia.GetListarClientesSalaverry(codigo, RazonSocial, DocumentoFiscal);
        }

        public DataTable GetRegistrarConductor(string Documento, string usuario)
        {
            return clsAlmacenDAO.Instancia.GetRegistrarConductor(Documento, usuario);
        }

        public DataTable GetListarUltimoConductor()
        {
            return clsAlmacenDAO.Instancia.GetListarUltimoConductor();
        }

        public DataTable GetListarConductoressSalaverry(string Documento)
        {
            return clsAlmacenDAO.Instancia.GetListarConductoressSalaverry(Documento);
        }


        public DataTable ReportesApp_Almacen_ListarConductoresAlmacen(string txtconductor)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_ListarConductoresAlmacen(txtconductor);
        }

        public DataTable ReportesApp_Almacen_ListarClientesAlmacen(string cliente)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_ListarClientesAlmacen(cliente);
        }

        public DataTable ReportesApp_Almacen_ListarVehiculoAlmacen(string vehiculo)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_ListarVehiculoAlmacen(vehiculo);
        }

        public bool ReportesApp_Almacen_Registrar_FechaIngreso(string OrdenCliente, int idCliente, string Cliente, int idConductor, string Conductor, int idVehiculo, string vehiculo, string FechaIngreso, string peso,string grupo,ref string nroOrden,ref string codigo,ref string transportista)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_Registrar_FechaIngreso(OrdenCliente, idCliente, Cliente, idConductor, Conductor, idVehiculo, vehiculo, FechaIngreso, peso,grupo,ref nroOrden,ref codigo,ref transportista);
        }


        public DataTable ReportesApp_Almacen_ListarIngresoSalida(string fechaInicio,string fechafin)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_ListarIngresoSalida(fechaInicio, fechafin);
        }


        public bool ReportesApp_Almacen_EnPesajeIngresoSalida(string codigo, string tipo)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_EnPesajeIngresoSalida(codigo, tipo);
        }

        public bool ReportesApp_Almacen_Registrar_Correos(string cliente, int idCliente,string correo1 , string correo)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_Registrar_Correos(cliente, idCliente, correo1, correo);
        }

        public DataTable ReportesApp_Almacen_Registrar_ListarCorreos()
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_Registrar_ListarCorreos();
        }

        public bool ReportesApp_Almacen_Eliminar_Correos(int idCorreo)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_Eliminar_Correos(idCorreo);
        }

        public DataTable ReportesApp_Almacen_Salaverry_ListarTransformaciones(string FechaInicio, string FechaFin, string Cliente, string Producto, string Lote)
        { return clsAlmacenDAO.Instancia.ReportesApp_Almacen_Salaverry_ListarTransformaciones(FechaInicio, FechaFin, Cliente, Producto, Lote); }

        public DataTable ReportesApp_Almacen_ListarAlmacenes()
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_ListarAlmacenes();
        }

        public DataTable ReportesApp_Almacen_Salaverry_ListarKardex(string FechaInicio, string FechaFin, string Cliente, string Producto, string Lote)
        { return clsAlmacenDAO.Instancia.ReportesApp_Almacen_Salaverry_ListarKardex(FechaInicio, FechaFin, Cliente, Producto, Lote); }



        public DataTable ReportesApp_Almacen_Movimientos_Almacen(string fechaInI, string fechaFin, string Cliente, bool anulado,int bd)
        {
            return clsAlmacenDAO.Instancia.ReportesApp_Almacen_Movimientos_Almacen(fechaInI, fechaFin, Cliente,anulado,bd);
        }
    }
}
