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
    public class clsReporteBL
    {
        private clsReporteBL()
        {

        }

        private readonly static clsReporteBL instancia = new clsReporteBL();

        public static clsReporteBL Instancia
        {
            get { return instancia; }
        }

        public String registrar_Reporte(clsReporte obj)
        {
            return clsReporteDAO.Instancia.registrar_Reporte(obj);
        }

        public String modificar_Reporte(clsReporte obj)
        {
            return clsReporteDAO.Instancia.modificar_Reporte(obj);
        }

        public List<clsReporte> consulta_Reporte_Por_IdArea(String idArea)
        {
            return clsReporteDAO.Instancia.consulta_Reporte_Por_IdArea(idArea);
        }

        public List<clsReporte> consulta_Reporte_Por_Codigo_Nombre(String codigoReporte, String nombreReporte)
        {
            return clsReporteDAO.Instancia.consulta_Reporte_Por_Codigo_Nombre(codigoReporte,nombreReporte);
        }

        public void InsertRegistros(string IdReporte, string usuario, string formulario, string area)
        {
            clsReporteDAO.Instancia.Registros(IdReporte, usuario, formulario, area);
        }

        public void GuardaRegistrosForms(string usuario, string formulario)
        {
            clsReporteDAO.Instancia.GuardaRegistrosForms(usuario, formulario);
        }

        public DataTable ListaReporteForms()
        {
            return clsReporteDAO.Instancia.ListaReporteForms();
        }

        public DataTable ListaReporteFormsUsuario()
        {
            return clsReporteDAO.Instancia.ListaReporteFormsUsuario();
        }

        public void Registrar_Nuevo_Reporte(string formulario, string reporte, string areas, string idarea, string DescripcionReporte)
        {
            clsReporteDAO.Instancia.Registrar_Nuevo_Reporte(formulario, reporte, areas, idarea, DescripcionReporte);
        }

        public DataTable ListaPeriodos(string reporte)
        {
            return clsReporteDAO.Instancia.ListaPeriodos(reporte);
        }

        public DataTable ComboPeriodos(string formulario)
        {
            return clsReporteDAO.Instancia.ComboPeriodos(formulario);        
        }

        public DataTable ComboPorPeriodos(string formulario, string periodo)
        {
            return clsReporteDAO.Instancia.ComboPorPeriodos(formulario, periodo);
        }

        public void Registrar_Nuevo_Periodo(string formulario, string fechaini, string fechafin)
        {
            clsReporteDAO.Instancia.Registrar_Nuevo_Periodo(formulario, fechaini, fechafin);
        }

        public DataTable ListaReportes(string usuario, string nombres,char estado)
        {
            return clsReporteDAO.Instancia.ListaReportes(usuario, nombres, estado);
        }
    }
}
