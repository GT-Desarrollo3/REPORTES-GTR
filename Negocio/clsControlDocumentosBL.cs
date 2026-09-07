using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsControlDocumentosBL
    {
        private readonly static clsControlDocumentosBL instancia = new clsControlDocumentosBL();

        public static clsControlDocumentosBL Instancia
        {
            get { return instancia; }
        }

        public DataTable getDocumentos_LlenarControlesDocumentos(string usuario)
        {
            return clsControlDocumentosDAO.Instancia.getDocumentos_LlenarControlesDocumentos(usuario);
        }

        public DataTable getDocumentos_ListarDocumentos(string compania, int tipoDocumento, int tipoFiltro, string filtro, int tipoUnidad)
        {
            return clsControlDocumentosDAO.Instancia.getDocumentos_ListarDocumentos(compania, tipoDocumento, tipoFiltro, filtro, tipoUnidad);
        }


        //GERARDO - 01/09
        public DataTable getDocumento_RegistrarDocumento(string compania, string sucursal, string tipoRelacion, int idRelacion, int idTipoDocumento, string codigo, string centroCosto,
                                                         string situacionRenovacion, string fechaEmision, string fechaInicioValidez, string fechaFinValidez, int esVencimiento,
                                                         string moneda, decimal montoTotal, int esAfectoValidacion, string usuarioCrea, string observacion, string categoria, string RutaLocal, string RutaNube)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_RegistrarDocumento(compania, sucursal, tipoRelacion, idRelacion, idTipoDocumento, codigo, centroCosto,
                                                                                     situacionRenovacion, fechaEmision, fechaInicioValidez, fechaFinValidez, esVencimiento,
                                                                                     moneda, montoTotal, esAfectoValidacion, usuarioCrea, observacion, categoria, RutaLocal, RutaNube);
        }

        public DataTable getDocumento_ActualizarDocumento(int idDocumento, string compania, string sucursal, string tipoRelacion, int idRelacion, int idTipoDocumento, string codigo, string centroCosto,
                                                         string situacionRenovacion, string fechaEmision, string fechaInicioValidez, string fechaFinValidez, int esVencimiento,
                                                         string moneda, decimal montoTotal, int esAfectoValidacion, string usuarioActualiza, string observacion, string categoria, string RutaLocal, string RutaNube)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_ActualizarDocumento(idDocumento, compania, sucursal, tipoRelacion, idRelacion, idTipoDocumento, codigo, centroCosto,
                                                                                     situacionRenovacion, fechaEmision, fechaInicioValidez, fechaFinValidez, esVencimiento,
                                                                                     moneda, montoTotal, esAfectoValidacion, usuarioActualiza, observacion, categoria, RutaLocal, RutaNube);
        }

        public DataTable getDocumento_RenovarDocumento(int idDocumento, string compania, string sucursal, string tipoRelacion, int idRelacion, int idTipoDocumento, string codigo, string centroCosto,
                                                         string situacionRenovacion, string fechaEmision, string fechaInicioValidez, string fechaFinValidez, int esVencimiento,
                                                         string moneda, decimal montoTotal, int esAfectoValidacion, string usuarioActualiza, string observacion, string categoria, string RutaLocal, string RutaNube)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_RenovarDocumento(idDocumento, compania, sucursal, tipoRelacion, idRelacion, idTipoDocumento, codigo, centroCosto,
                                                                                     situacionRenovacion, fechaEmision, fechaInicioValidez, fechaFinValidez, esVencimiento,
                                                                                     moneda, montoTotal, esAfectoValidacion, usuarioActualiza, observacion, categoria, RutaLocal, RutaNube);
        }
        //GERARDO - 01/09


        public DataTable getDocumento_AnularDocumento(int idDocumento, string usuarioAnula)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_AnularDocumento(idDocumento, usuarioAnula);
        }

        public DataTable getDocumento_VerDocumentosVencidos(string usuario)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_VerDocumentosVencidos(usuario);
        }


        public DataTable getDocumento_BuscarRelacion(int tipoRelacion, string filtro)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_BuscarRelacion(tipoRelacion, filtro);
        }

        public DataTable getDocumento_BuscarCentroCosto(string filtro)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_BuscarCentroCosto(filtro);
        }

        public DataTable getDocumento_BuscarTipoDocumento(string tipoRelacion)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_BuscarTipoDocumento(tipoRelacion);
        }

        public DataTable getDocumentos_ListarTiposDocumentos(string tipoRelacion)
        {
            return clsControlDocumentosDAO.Instancia.getDocumentos_ListarTiposDocumentos(tipoRelacion);
        }


        public DataTable getDocumento_TipoDocumento_Registrar(string TipoRelacion, string Nemonico, string Descripcion, int dias, string usuarioCrea)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_TipoDocumento_Registrar(TipoRelacion, Nemonico, Descripcion, dias, usuarioCrea);
        }

        public DataTable getDocumento_TipoDocumento_Actualizar(int idTipoDocumento, string TipoRelacion, string Nemonico, string Descripcion, int dias, string usuarioCrea)
        {
            return clsControlDocumentosDAO.Instancia.getDocumento_TipoDocumento_Actualizar(idTipoDocumento, TipoRelacion, Nemonico, Descripcion, dias, usuarioCrea);
        }

        public DataTable getDocumentos_ReporteConductoresSinEMO(string compania,  string filtro)
        {
            return clsControlDocumentosDAO.Instancia.getDocumentos_ReporteConductoresSinEMO(compania, filtro);
        }

        // GERARDO - 19/09/23
        public DataTable ReportesApp_Operaciones_ControlDocumentos_ListarHistorial(int Opcion, string Nombre, string FInicio, string FFin)
        {
            return clsControlDocumentosDAO.Instancia.ReportesApp_Operaciones_ControlDocumentos_ListarHistorial(Opcion, Nombre, FInicio, FFin);
        }
        // GERARDO - 19/09/23
    }
}
