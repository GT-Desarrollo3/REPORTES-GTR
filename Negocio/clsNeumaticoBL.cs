using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsNeumaticoBL
    {
        private clsNeumaticoBL()
        {

        }

        private readonly static clsNeumaticoBL instancia = new clsNeumaticoBL();

        public static clsNeumaticoBL Instancia
        {
            get { return instancia; }
        }

        public DataTable GetNeumaticoConsumo(string fini,string ffin)
        {
            return clsNeumaticoDAO.Instancia.GetNeumaticoConsumo(fini, ffin);
        }

	    public DataTable ReportesApp_Neumatico_Consultar_KMVehiculos(string fechaInicio, string fechaFin, string placa, int filtro)
        {
            return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_Consultar_KMVehiculos(fechaInicio, fechaFin, placa, filtro);
        }

        public DataTable ReportesApp_Neumatico_ListarKMUnidades()
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ListarKMUnidades(); }

        public DataTable ReportesApp_Neumatico_InsertarKMUnidades(string placa, string fechaInicio, string fechaFin, string TipoVehiculo)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_InsertarKMUnidades(placa, fechaInicio, fechaFin, TipoVehiculo); }

        public DataTable ReportesApp_Neumatico_Listar(int Opcion)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_Listar(Opcion); }

        public DataTable ReportesApp_Neumatico_Registrar_Neumatico_SegundoUso(int CodNeumatico, string Medida, string Marca, string Proveedor, DateTime FechaEnvio,
                                                                              string Estado, string DocumentoEvaluacion, string GRR, byte[] imagen)
        {
            return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_Registrar_Neumatico_SegundoUso(CodNeumatico, Medida, Marca, Proveedor, FechaEnvio, Estado,
                                                                                                  DocumentoEvaluacion, GRR, imagen);
        }

        public DataTable ReportesApp_Neumatico_ListarReencauche_SegundoUso(string Codigo, string Medida, string Observacion, string FechaInicio, string FechaFin)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ListarReencauche_SegundoUso(Codigo, Medida, Observacion, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Neumatico_Maestro_SegundoUso_EliminarNeumaticos(int idReencauche)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_Maestro_SegundoUso_EliminarNeumaticos(idReencauche); }

        public DataTable ReportesApp_Neumatico_SegundoUso_InsertarReencauchados(int idReencauche, DateTime FechaRecepcion, string Estado, string GRR, string Disenio, Decimal Costo)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_SegundoUso_InsertarReencauchados(idReencauche, FechaRecepcion, Estado, GRR, Disenio, Costo); }

        public DataTable ReportesApp_Neumatico_FiltrarNeumatico_SegundoUso(string CodigoNeu)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_FiltrarNeumatico_SegundoUso(CodigoNeu); }

        public bool ReportesApp_Neumatico_RegistrarIngreso_SegundoUso(string GRR, DateTime FechaIngreso, int Cantidad, string Item)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_RegistrarIngreso_SegundoUso(GRR, FechaIngreso, Cantidad, Item); }

        public DataTable ReportesApp_Neumatico_ListarIngreso_SegundoUso(int Opcion, string nombreIngreso, string FechaInicio, string FechaFin)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ListarIngreso_SegundoUso(Opcion, nombreIngreso, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Neumatico_SegundoUso_ListarIngreso(int Opcion, string GRR, string Neumatico, string FechaInicio, string FechaFin)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_SegundoUso_ListarIngreso(Opcion, GRR, Neumatico, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Neumatico_ListarOT_SegundoUso(string NumeroOrden)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ListarOT_SegundoUso(NumeroOrden); }

        public DataTable ReportesApp_Neumatico_RegistrarSalida_SegundoUso(int idIngreso, string orden, int cantidad, DateTime FechaSalida)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_RegistrarSalida_SegundoUso(idIngreso, orden, cantidad, FechaSalida); }

        public DataTable ReportesApp_Neumatico_AnularSalida_SegundoUso(int idRegistro)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_AnularSalida_SegundoUso(idRegistro); }

        public DataTable ReportesApp_Neumatico_RegistrarRetorno_SegundoUso(int idSalida, int idIngresoNeu, int Cantidad)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_RegistrarRetorno_SegundoUso(idSalida, idIngresoNeu, Cantidad); }

        public DataTable ReportesApp_Neumatico_RegistrarReclamo_SegundoUso(int idIngreso, string Motivo, DateTime FechaReclamo, int CantidadReclamo, string GRReclamo)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_RegistrarReclamo_SegundoUso(idIngreso, Motivo, FechaReclamo, CantidadReclamo, GRReclamo); }

        public DataTable ReportesApp_Neumatico_QuitarReclamo_SegundoUso(int idIngreso, int idReclamo)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_QuitarReclamo_SegundoUso(idIngreso, idReclamo); }

        public DataTable ReportesApp_Neumatico_ActualizarReclamo_SegundoUso(int idReclamo, string Estado)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ActualizarReclamo_SegundoUso(idReclamo, Estado); }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros(int Opcion, string Placa, string TipoMaquina, string FechaInicio, string FechaFin)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros(Opcion, Placa, TipoMaquina, FechaInicio, FechaFin); }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ActualizarRegistros(int idRegistro, DateTime UltimaFecha, decimal UltimoKM, int L1E1, int L1E2,
                                                                                     int L1E3, int L2E1, int L2E2, int L2E3, string Usuario)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ActualizarRegistros(idRegistro, UltimaFecha, UltimoKM, L1E1, L1E2, L1E3, L2E1, L2E2, L2E3, Usuario); }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_GenerarCumplimiento(string Placa, string Operacion, string TipoUnidad, string Marca, string Alineamiento, DateTime FechaProgramada,
                         DateTime FCInicio, DateTime FCFin)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ControlNeumaticos_GenerarCumplimiento(Placa, Operacion, TipoUnidad, Marca, Alineamiento, FechaProgramada, FCInicio, FCFin); }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ListarCumplimiento(int Anio, int NroSemana)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ListarCumplimiento(Anio, NroSemana); }

        public DataTable ReportesApp_Neumatico_ControlNeumaticos_ProgramarCumplimiento(int Opcion, int Nro, DateTime FechaCump, string Estado, string Observacion, string Usuario)
        { return clsNeumaticoDAO.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ProgramarCumplimiento(Opcion, Nro, FechaCump, Estado, Observacion, Usuario); }
    }
}
