using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AccesoDatos;

namespace Negocio
{
    public class clsDespachosTercerosBL
    {
        private readonly static clsDespachosTercerosBL instancia = new clsDespachosTercerosBL();

        public static clsDespachosTercerosBL Instancia
        {
            get { return instancia; }
        }

        public DataTable getDespachosTerceros_LlenarControlesDespachosTerceros(string usuario)
        {
            return clsDespachosTercerosDAO.Instancia.getDespachosTerceros_LlenarControlesDespachoTerceros(usuario);
        }

        public DataTable getDespachosTerceros_Listar(string periodo)
        {
            return clsDespachosTercerosDAO.Instancia.getDespachosTerceros_Listar(periodo);
        }

        public DataTable getDocumento_ListarLugarXproveedor(int preoveedor)
        {
            return clsDespachosTercerosDAO.Instancia.getDocumento_ListarLugarXproveedor(preoveedor);
        }
        
        public DataTable getDespachosTerceros_Registrar(string codigo, string placa, string fechaDespacho, string hora, decimal cantidad, decimal precio, string producto, 
                                                        string lugar, int IdEmpresa, string empresa, string dni, string chofer, decimal kilometraje, int CodigoPreviaje,
                                                        string usuarioCrea,decimal Totalizador)
        {
            return clsDespachosTercerosDAO.Instancia.getDespachosTerceros_Registrar(codigo, placa, fechaDespacho, hora, cantidad, precio, producto,
                                                                                     lugar, IdEmpresa, empresa, dni, chofer, kilometraje, CodigoPreviaje, usuarioCrea, Totalizador);
        }

        public DataTable getDespachosTerceros_Actualizar(int ticket, string codigo, string placa, string fechaDespacho, string hora, decimal cantidad, decimal precio, string producto,
                                                         string lugar, string empresa, string dni, string chofer, decimal kilometraje, int CodigoPreviaje, string usuarioCrea,
                                                            decimal Totalizador)
        {
            return clsDespachosTercerosDAO.Instancia.getDespachosTerceros_Actualizar(ticket, codigo, placa, fechaDespacho, hora, cantidad, precio, producto,
                                                                                    lugar, empresa, dni, chofer, kilometraje, CodigoPreviaje, usuarioCrea,Totalizador);
        }

        public DataTable getDespachosTerceros_Anular(int ticket, int codigo, string placa, string fechaDespacho, string hora, decimal cantidad, decimal precio, string chofer, int notaSalida,
                                                     string Producto, string Proveedor, string Lugar, string usuarioAnula)
        { return clsDespachosTercerosDAO.Instancia.getDespachosTerceros_Anular(ticket, codigo, placa, fechaDespacho, hora, cantidad, precio, chofer, notaSalida, Producto, Proveedor, Lugar, usuarioAnula); }

        public DataTable getDespachosTerceros_ObtenerOdometroPlaca(string placa, string fecha, string hora)
        {
            return clsDespachosTercerosDAO.Instancia.getDespachosTerceros_ObtenerOdometroPlaca(placa, fecha, hora);
        }

        public DataTable getDocumento_BuscarRelacion(int tipoRelacion, string filtro)
        {
            return clsDespachosTercerosDAO.Instancia.getDocumento_BuscarRelacion(tipoRelacion, filtro);
        }


        public DataTable getDocumentos_ListarTiposDocumentos(string tipoRelacion)
        {
            return clsControlDocumentosDAO.Instancia.getDocumentos_ListarTiposDocumentos(tipoRelacion);
        }

        public DataTable ReportesApp_Operaciones_DespachosTerceros_ListarOdometro(string placa, string fecha, string hora)
        { return clsDespachosTercerosDAO.Instancia.ReportesApp_Operaciones_DespachosTerceros_ListarOdometro(placa, fecha, hora); }
    }
}
